using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.LogicMigrator;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MMR.Randomizer.Utils
{
    public static class LogicUtils
    {
        public static string[] ReadRulesetFromResources(LogicMode mode, string userLogicFileName)
        {
            string[] lines = null;

            if (mode == LogicMode.Casual)
            {
                lines = Properties.Resources.REQ_CASUAL.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (mode == LogicMode.Glitched)
            {
                lines = Properties.Resources.REQ_GLITCH.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }
            else if (mode == LogicMode.UserLogic)
            {
                using (StreamReader Req = new StreamReader(File.OpenRead(userLogicFileName)))
                {
                    var logic = Req.ReadToEnd();
                    if (logic.StartsWith("{"))
                    {
                        var configurationLogic = Configuration.FromJson(logic);
                        logic = configurationLogic.GameplaySettings.Logic;
                    }
                    lines = logic.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                }
            }

            return lines;
        }

        /// <summary>
        /// Populates the item list using the lines from a logic file, processes them 4 lines per item. 
        /// </summary>
        /// <param name="data">The lines from a logic file</param>
        public static ItemList PopulateItemListFromLogicData(string[] data)
        {
            var itemList = new ItemList();
            if (Migrator.GetVersion(data.ToList()) != Migrator.CurrentVersion)
            {
                throw new Exception("Logic file is out of date or invalid. Open it in the Logic Editor to bring it up to date.");
            }

            int itemId = 0;
            int lineNumber = 0;

            var currentItem = new ItemObject();

            // Process lines in groups of 4
            foreach (string line in data)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("-"))
                {
                    currentItem.Name = line.Substring(2);
                    continue;
                }

                switch (lineNumber)
                {
                    case 0:
                        //dependence
                        ProcessDependenciesForItem(currentItem, line);
                        break;
                    case 1:
                        //conditionals
                        ProcessConditionalsForItem(currentItem, line);
                        break;
                    case 2:
                        //time needed
                        currentItem.TimeNeeded = Convert.ToInt32(line);
                        break;
                    case 3:
                        //time available
                        currentItem.TimeAvailable = Convert.ToInt32(line);
                        if (currentItem.TimeAvailable == 0)
                        {
                            currentItem.TimeAvailable = 63;
                        }
                        break;
                    case 4:
                        var trickInfo = line.Split(new char[] { ';' }, 2);
                        currentItem.IsTrick = trickInfo.Length > 1;
                        currentItem.TrickTooltip = currentItem.IsTrick ? trickInfo[1] : null;
                        if (string.IsNullOrWhiteSpace(currentItem.TrickTooltip))
                        {
                            currentItem.TrickTooltip = null;
                        }
                        break;
                }

                lineNumber++;

                if (lineNumber == 5)
                {
                    currentItem.ID = itemId;
                    itemList.Add(currentItem);

                    currentItem = new ItemObject();

                    itemId++;
                    lineNumber = 0;
                }
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

        private static void ProcessConditionalsForItem(ItemObject currentItem, string line)
        {
            foreach (string conditions in line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                currentItem.Conditionals.Add(Array.ConvertAll(conditions.Split(','), int.Parse).Select(i => (Item)i).ToList());
            }
        }

        private static void ProcessDependenciesForItem(ItemObject currentItem, string line)
        {
            foreach (string dependency in line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                currentItem.DependsOnItems.Add((Item)Convert.ToInt32(dependency));
            }
        }

        /// <summary>
        /// Populates item list without logic. Default TimeAvailable = 63
        /// </summary>
        public static ItemList PopulateItemListWithoutLogic()
        {
            var itemList = new ItemList();
            foreach (var item in Enum.GetValues(typeof(Item)).Cast<Item>())
            {
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
    }
}
