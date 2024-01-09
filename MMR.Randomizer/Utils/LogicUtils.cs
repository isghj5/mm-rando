using DynamicExpresso;
using MMR.Common.Extensions;
using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.LogicMigrator;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
            else if (mode == LogicMode.UserLogic && File.Exists(userLogicFileName))
            {
                using (StreamReader Req = new StreamReader(File.OpenRead(userLogicFileName)))
                {
                    var logic = Req.ReadToEnd();

                    var logicConfiguration = Configuration.FromJson(logic);
                    if (logicConfiguration.GameplaySettings != null)
                    {
                        logic = logicConfiguration.GameplaySettings.Logic;
                    }

                    return LogicFile.FromJson(logic);
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
                    TimeSetup = (int)logicItem.TimeSetup,
                    IsTrick = logicItem.IsTrick,
                    TrickTooltip = logicItem.TrickTooltip,
                    DependsOnItems = logicItem.RequiredItems.Select(item => (Item)logic.FindIndex(li => li.Id == item)).ToList(),
                    Conditionals = logicItem.ConditionalItems.Select(c => c.Select(item => (Item)logic.FindIndex(li => li.Id == item)).ToList()).ToList(),
                    TrickCategory = logicItem.TrickCategory,
                    TrickUrl = logicItem.TrickUrl,
                    SettingExpression = logicItem.SettingExpression,
                });
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

        public static Dictionary<GossipQuote, ReadOnlyCollection<Item>> GetGossipStoneRequirements(IEnumerable<GossipQuote> gossipQuotes, ItemList itemList, List<ItemLogic> logic, GameplaySettings settings, Dictionary<Item, LogicPaths> checkedLocations)
        {
            return gossipQuotes
                .Where(gq => gq.HasAttribute<GossipStoneAttribute>())
                .ToDictionary(gq => gq, gq => GetGossipStoneRequirement(gq, itemList, logic, settings, checkedLocations));
        }

        public static ReadOnlyCollection<Item> GetGossipStoneRequirement(GossipQuote gossipQuote, ItemList itemList, List<ItemLogic> logic, GameplaySettings settings, Dictionary<Item, LogicPaths> checkedLocations)
        {
            var gossipStoneItem = gossipQuote.GetAttribute<GossipStoneAttribute>().Item;
            return GetImportantLocations(itemList, settings, gossipStoneItem, logic, checkedLocations: checkedLocations)?.Required;
        }

        public class LogicPaths
        {
            public ReadOnlyCollection<Item> Required { get; set; }
            public ReadOnlyCollection<Item> Important { get; set; }
            public ReadOnlyCollection<Item> RequiredSongLocations { get; set; }
        }

        public static void Simplify<T>(List<T> logicPaths) where T : IEnumerable<Item>
        {
            Simplify(logicPaths, (list) => list);
        }

        public static void Simplify<T, T2>(List<T> logicPaths, Func<T, T2> selector) where T2 : IEnumerable<Item>
        {
            if (logicPaths.Count > 1)
            {
                for (var i = logicPaths.Count - 1; i >= 0; i--)
                {
                    var currentLogicPath = selector(logicPaths[i]);
                    for (var j = i - 1; j >= 0; j--)
                    {
                        var otherLogicPath = selector(logicPaths[j]);
                        if (!otherLogicPath.Except(currentLogicPath).Any())
                        {
                            logicPaths.RemoveAt(i);
                            break;
                        }
                        if (!currentLogicPath.Except(otherLogicPath).Any())
                        {
                            logicPaths.RemoveAt(j);
                            i--;
                        }
                    }
                }
            }
        }

        public static LogicPaths GetImportantLocations(ItemList itemList, GameplaySettings settings, Item location, List<ItemLogic> itemLogic, List<Item> logicPath = null, Dictionary<Item, LogicPaths> checkedLocations = null, Dictionary<Item, ItemObject> itemsByLocation = null, CancellationTokenSource cts = null, params Item[] exclude)
        {
            if (settings.LogicMode == LogicMode.NoLogic)
            {
                return new LogicPaths();
            }
            cts?.Token.ThrowIfCancellationRequested();
            if (itemsByLocation == null)
            {
                itemsByLocation = itemList.ToDictionary(io => io.NewLocation ?? io.Item);
            }
            var itemObject = itemsByLocation[location];
            if (logicPath == null)
            {
                logicPath = new List<Item>();
            }
            if (logicPath.Contains(location))
            {
                return null;
            }
            var requiredSongLocations = new List<Item>();
            if (exclude.Contains(location))
            {
                if (!location.IsPlacementHighlyRestricted(settings))
                {
                    return null;
                }
                requiredSongLocations.Add(location);
                if (logicPath.Any(i => !i.IsFake() && itemList[i].IsRandomized && !ItemUtils.IsRegionRestricted(settings, itemsByLocation[i].Item) && !i.IsPlacementHighlyRestricted(settings)))
                {
                    return null;
                }
            }
            logicPath.Add(location);
            if (checkedLocations == null)
            {
                checkedLocations = new Dictionary<Item, LogicPaths>();
            }
            if (checkedLocations.ContainsKey(location))
            {
                if (logicPath.Intersect(checkedLocations[location].Required).Any())
                {
                    return null;
                }
                return checkedLocations[location];
            }
            var locationLogic = itemLogic[(int)location];
            var required = new List<Item>();
            var important = new List<Item>();
            if (locationLogic.RequiredItemIds != null && locationLogic.RequiredItemIds.Any())
            {
                if (locationLogic.RequiredItemIds.Contains((int)Item.OtherCredits))
                {
                    return null;
                }

                foreach (var requiredItemId in locationLogic.RequiredItemIds.Cast<Item>())
                {
                    if (itemList[requiredItemId].Item != requiredItemId)
                    {
                        continue;
                    }

                    var requiredLocation = itemList[requiredItemId].NewLocation ?? requiredItemId;

                    var childPaths = GetImportantLocations(itemList, settings, requiredLocation, itemLogic, logicPath.ToList(), checkedLocations, itemsByLocation, cts, exclude);
                    if (childPaths == null)
                    {
                        return null;
                    }

                    if (logicPath[0] != Item.OtherCredits || !requiredLocation.IsPlacementHighlyRestricted(settings) || logicPath.Any(i => !i.IsFake() && itemList[i].IsRandomized && !ItemUtils.IsRegionRestricted(settings, itemsByLocation[i].Item) && !i.IsPlacementHighlyRestricted(settings)))
                    {
                        required.Add(requiredLocation);
                    }
                    important.Add(requiredLocation);
                    if (childPaths.Required != null)
                    {
                        required.AddRange(childPaths.Required);
                    }
                    if (childPaths.Important != null)
                    {
                        important.AddRange(childPaths.Important);
                    }
                    if (childPaths.RequiredSongLocations != null)
                    {
                        requiredSongLocations.AddRange(childPaths.RequiredSongLocations);
                    }
                }
            }
            if (locationLogic.ConditionalItemIds != null && locationLogic.ConditionalItemIds.Any())
            {
                var logicPaths = new List<LogicPaths>();
                foreach (var conditions in locationLogic.ConditionalItemIds)
                {
                    if (conditions.Contains((int)Item.OtherCredits))
                    {
                        continue;
                    }

                    var conditionalRequired = new List<Item>();
                    var conditionalImportant = new List<Item>();
                    var conditionalRequiredSongLocations = new List<Item>();
                    foreach (var conditionalItemId in conditions.Cast<Item>())
                    {
                        if (itemList[conditionalItemId].Item != conditionalItemId)
                        {
                            continue;
                        }

                        var conditionalLocation = itemList[conditionalItemId].NewLocation ?? conditionalItemId;

                        var childPaths = GetImportantLocations(itemList, settings, conditionalLocation, itemLogic, logicPath.ToList(), checkedLocations, itemsByLocation, cts, exclude);
                        if (childPaths == null)
                        {
                            conditionalRequired = null;
                            conditionalImportant = null;
                            break;
                        }

                        if (logicPath[0] != Item.OtherCredits || !conditionalLocation.IsPlacementHighlyRestricted(settings) || logicPath.Any(i => !i.IsFake() && itemList[i].IsRandomized && !ItemUtils.IsRegionRestricted(settings, itemsByLocation[i].Item) && !i.IsPlacementHighlyRestricted(settings)))
                        {
                            conditionalRequired.Add(conditionalLocation);
                        }
                        conditionalImportant.Add(conditionalLocation);
                        if (childPaths.Required != null)
                        {
                            conditionalRequired.AddRange(childPaths.Required);
                        }
                        if (childPaths.Important != null)
                        {
                            conditionalImportant.AddRange(childPaths.Important);
                        }
                        if (childPaths.RequiredSongLocations != null)
                        {
                            conditionalRequiredSongLocations.AddRange(childPaths.RequiredSongLocations);
                        }
                    }

                    if (conditionalRequired != null && conditionalImportant != null)
                    {
                        logicPaths.Add(new LogicPaths
                        {
                            Required = conditionalRequired.AsReadOnly(),
                            Important = conditionalImportant.AsReadOnly(),
                            RequiredSongLocations = conditionalRequiredSongLocations.AsReadOnly()
                        });
                    }
                }
                if (!logicPaths.Any())
                {
                    return null;
                }

                Simplify(logicPaths, lp => lp.Important.Except(important));

                required.AddRange(logicPaths.Select(lp => lp.Required.AsEnumerable()).Aggregate((a, b) => a.Intersect(b)));
                important.AddRange(logicPaths.SelectMany(lp => lp.Required.Union(lp.Important)).Distinct());
                requiredSongLocations.AddRange(logicPaths.SelectMany(lp => lp.RequiredSongLocations).Distinct());
            }
            var result = new LogicPaths
            {
                Required = required.Distinct().ToList().AsReadOnly(),
                Important = important.Union(required).Distinct().ToList().AsReadOnly(),
                RequiredSongLocations = requiredSongLocations.Distinct().ToList().AsReadOnly()
            };
            if (location.Region(itemList).HasValue && location.Entrance() == null)
            {
                checkedLocations[location] = result;
            }
            return result;
        }

        public static Expression<Func<GameplaySettings, bool>> ParseSettingExpression(string expression)
        {
            return Interpreter.ParseAsExpression<Func<GameplaySettings, bool>>(expression, "settings");
        }

        public static bool IsSettingEnabled(GameplaySettings settings, string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return true;
            }
            return ParseSettingExpression(expression).Compile()(settings);
        }

        private static Interpreter Interpreter;

        static LogicUtils()
        {
            Interpreter = new Interpreter()
                .EnableAssignment(AssignmentOperators.None);

            ReferenceEnums(Interpreter, typeof(GameplaySettings));
        }

        private static void ReferenceEnums(Interpreter interpreter, Type type)
        {
            void ProcessPropertyType(Type propertyType)
            {
                if (propertyType == typeof(string) || propertyType == typeof(AsmOptionsGameplay))
                {

                }
                else if (propertyType.IsGenericType)
                {
                    if (propertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        var keyType = propertyType.GetGenericArguments()[0];
                        var valueType = propertyType.GetGenericArguments()[1];
                        ProcessPropertyType(keyType);
                        ProcessPropertyType(valueType);
                    }
                    else if (propertyType.IsAssignableTo(typeof(IEnumerable)))
                    {
                        var itemType = propertyType.GetGenericArguments()[0];
                        ProcessPropertyType(itemType);
                    }
                }
                else if (propertyType.IsEnum)
                {
                    interpreter.Reference(propertyType);
                }
                else if (propertyType.IsClass)
                {
                    ReferenceEnums(interpreter, propertyType);
                }
            }

            foreach (var property in type.GetProperties())
            {
                ProcessPropertyType(property.PropertyType);
            }
        }
    }
}
