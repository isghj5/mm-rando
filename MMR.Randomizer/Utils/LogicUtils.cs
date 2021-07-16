using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.LogicMigrator;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MMR.Randomizer.Utils
{
    public static class LogicUtils
    {
        public static LogicFile ReadRulesetFromResources(LogicMode mode, string userLogicFileName)
        {
            if (mode == LogicMode.Casual)
            {
                return LogicFile.FromJson(Properties.Resources.REQ_CASUAL);
            }
            else if (mode == LogicMode.Glitched)
            {
                return LogicFile.FromJson(Properties.Resources.REQ_GLITCH);
            }
            else if (mode == LogicMode.UserLogic)
            {
                using (StreamReader Req = new StreamReader(File.OpenRead(userLogicFileName)))
                {
                    var logic = Req.ReadToEnd();

                    return LogicFile.FromJson(logic);

                    // TODO handle logic within settings file
                }
            }

            return null;
        }

        /// <summary>
        /// Populates the item list using the lines from a logic file, processes them 4 lines per item. 
        /// </summary>
        /// <param name="data">The lines from a logic file</param>
        public static ItemList PopulateItemListFromLogicData(LogicFile logicFile)
        {
            var itemList = new ItemList();
            if (logicFile.Version != Migrator.CurrentVersion)
            {
                throw new Exception("Logic file is out of date or invalid. Open it in the Logic Editor to bring it up to date.");
            }

            var logic = logicFile.Logic;
            for (var i = 0; i < logic.Count; i++)
            {
                var logicItem = logic[i];
                itemList.Add(new ItemObject
                {
                    ID = i,
                    Name = logicItem.Id,
                    TimeNeeded = (int)logicItem.TimeNeeded,
                    TimeAvailable = logicItem.TimeAvailable > 0 ? (int)logicItem.TimeAvailable : 63,
                    IsTrick = logicItem.IsTrick,
                    TrickTooltip = logicItem.TrickTooltip,
                    DependsOnItems = logicItem.RequiredItems.Select(item => (Item)logic.FindIndex(li => li.Id == item)).ToList(),
                    Conditionals = logicItem.ConditionalItems.Select(c => c.Select(item => (Item)logic.FindIndex(li => li.Id == item)).ToList()).ToList(),
                });
            }

            foreach (var io in itemList)
            {
                if (io.DependsOnItems.Any(item => itemList[item].IsTrick))
                {
                    throw new Exception($"Dependencies of {io.Name} are not valid. Cannot have tricks as Dependencies.");
                }
                if (io.Conditionals.Any() && io.Conditionals.All(c => c.Any(item => itemList[item].IsTrick)))
                {
                    throw new Exception($"Conditionals of {io.Name} are not valid. Must have at least one conditional that isn't a trick.");
                }
            }

            return itemList;
        }

        /// <summary>
        /// Populates item list without logic. Default TimeAvailable = 63
        /// </summary>
        public static ItemList PopulateItemListWithoutLogic()
        {
            var itemList = new ItemList();
            foreach (var item in Enum.GetValues<Item>())
            {
                if (item < 0)
                {
                    continue;
                }

                var currentItem = new ItemObject
                {
                    ID = (int)item,
                    Name = item.Name() ?? item.ToString(),
                    TimeAvailable = 63
                };

                itemList.Add(currentItem);
            }
            return itemList;
        }

        public static Dictionary<GossipQuote, ReadOnlyCollection<Item>> GetGossipStoneRequirements(ItemList itemList, List<ItemLogic> logic, GameplaySettings settings)
        {
            return Enum.GetValues(typeof(GossipQuote))
                .Cast<GossipQuote>()
                .Where(gq => gq.HasAttribute<GossipStoneAttribute>())
                .ToDictionary(gq => gq, gq => GetGossipStoneRequirement(gq, itemList, logic, settings));
        }

        public static ReadOnlyCollection<Item> GetGossipStoneRequirement(GossipQuote gossipQuote, ItemList itemList, List<ItemLogic> logic, GameplaySettings settings)
        {
            var gossipStoneItem = gossipQuote.GetAttribute<GossipStoneAttribute>().Item;
            return GetImportantItems(itemList, settings, gossipStoneItem, logic).Required;
        }

        public class LogicPaths
        {
            public ReadOnlyCollection<Item> Required { get; set; }
            public ReadOnlyCollection<Item> Important { get; set; }
        }

        public static LogicPaths GetImportantItems(ItemList itemList, GameplaySettings settings, Item item, List<ItemLogic> itemLogic, List<Item> logicPath = null, Dictionary<Item, LogicPaths> checkedItems = null, params Item[] exclude)
        {
            if (settings.CustomStartingItemList.Contains(item))
            {
                return new LogicPaths();
            }
            if (logicPath == null)
            {
                logicPath = new List<Item>();
            }
            if (logicPath.Contains(item))
            {
                return null;
            }
            if (exclude.Contains(item))
            {
                if (settings.AddSongs || !ItemUtils.IsSong(item) || logicPath.Any(i => !i.IsFake() && itemList[i].IsRandomized && !ItemUtils.IsRegionRestricted(settings, i) && !ItemUtils.IsSong(i)))
                {
                    return null;
                }
            }
            logicPath.Add(item);
            if (checkedItems == null)
            {
                checkedItems = new Dictionary<Item, LogicPaths>();
            }
            if (checkedItems.ContainsKey(item))
            {
                if (logicPath.Intersect(checkedItems[item].Required).Any())
                {
                    return null;
                }
                return checkedItems[item];
            }
            var itemObject = itemList[item];
            var locationId = itemObject.NewLocation.HasValue ? itemObject.NewLocation.Value : item;
            var locationLogic = itemLogic[(int)locationId];
            var required = new List<Item>();
            var important = new List<Item>();
            if (locationLogic.RequiredItemIds != null && locationLogic.RequiredItemIds.Any())
            {
                foreach (var requiredItemId in locationLogic.RequiredItemIds.Cast<Item>())
                {
                    if (itemList[requiredItemId].Item != requiredItemId)
                    {
                        continue;
                    }

                    var childPaths = GetImportantItems(itemList, settings, requiredItemId, itemLogic, logicPath.ToList(), checkedItems, exclude);
                    if (childPaths == null)
                    {
                        return null;
                    }

                    required.Add(requiredItemId);
                    if (childPaths.Required != null)
                    {
                        required.AddRange(childPaths.Required);
                    }
                    if (childPaths.Important != null)
                    {
                        important.AddRange(childPaths.Important);
                    }
                }
            }
            if (locationLogic.ConditionalItemIds != null && locationLogic.ConditionalItemIds.Any())
            {
                var logicPaths = new List<LogicPaths>();
                foreach (var conditions in locationLogic.ConditionalItemIds)
                {
                    var conditionalRequired = new List<Item>();
                    var conditionalImportant = new List<Item>();
                    foreach (var conditionalItemId in conditions.Cast<Item>())
                    {
                        if (itemList[conditionalItemId].Item != conditionalItemId)
                        {
                            continue;
                        }

                        var childPaths = GetImportantItems(itemList, settings, conditionalItemId, itemLogic, logicPath.ToList(), checkedItems, exclude);
                        if (childPaths == null)
                        {
                            conditionalRequired = null;
                            conditionalImportant = null;
                            break;
                        }

                        conditionalRequired.Add(conditionalItemId);
                        if (childPaths.Required != null)
                        {
                            conditionalRequired.AddRange(childPaths.Required);
                        }
                        if (childPaths.Important != null)
                        {
                            conditionalImportant.AddRange(childPaths.Important);
                        }
                    }

                    if (conditionalRequired != null && conditionalImportant != null)
                    {
                        logicPaths.Add(new LogicPaths
                        {
                            Required = conditionalRequired.AsReadOnly(),
                            Important = conditionalImportant.AsReadOnly()
                        });
                    }
                }
                if (!logicPaths.Any())
                {
                    return null;
                }
                required.AddRange(logicPaths.Select(lp => lp.Required.AsEnumerable()).Aggregate((a, b) => a.Intersect(b)));
                important.AddRange(logicPaths.SelectMany(lp => lp.Required.Union(lp.Important)).Distinct());
            }
            var result = new LogicPaths
            {
                Required = required.Distinct().ToList().AsReadOnly(),
                Important = important.Union(required).Distinct().ToList().AsReadOnly()
            };
            if (!item.IsFake())
            {
                checkedItems[item] = result;
            }
            return result;
        }
    }
}
