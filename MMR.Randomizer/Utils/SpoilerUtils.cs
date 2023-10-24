﻿using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MMR.Randomizer.Utils
{
    public static class SpoilerUtils
    {
        public static void CreateSpoilerLog(RandomizedResult randomized, GameplaySettings settings, OutputSettings outputSettings)
        {
            var itemList = randomized.ItemList
                .Where(io => (io.IsRandomized && io.NewLocation.Value.Region().HasValue) || (io.Item.MainLocation().HasValue && randomized.ItemList[io.Item.MainLocation().Value].IsRandomized))
                .Select(io => new {
                    ItemObject = io.Item.MainLocation().HasValue ? randomized.ItemList.Find(x => x.NewLocation == io.Item.MainLocation().Value) : io,
                    LocationForImportance = io.NewLocation ?? io.Item,
                    Region = io.IsRandomized ? io.NewLocation.Value.Region().Value : io.Item.Region().Value,
                })
                .Select(u => new SpoilerItem(
                    u.ItemObject,
                    u.Region,
                    ItemUtils.IsRequired(u.ItemObject.Item, u.LocationForImportance, randomized),
                    ItemUtils.IsImportant(u.ItemObject.Item, u.LocationForImportance, randomized),
                    randomized.ImportantSongLocations?.Contains(u.LocationForImportance) == true,
                    settings.ProgressiveUpgrades
                ));

            randomized.Logic.ForEach((il) =>
            {
                if (il.ItemId >= 0)
                {
                    var io = randomized.ItemList[il.ItemId];
                    il.IsFakeItem = !io.IsRandomized || (io.Item.IsFake() && io.Item.Entrance() == null);
                    il.IsItemRemoved = io.ItemOverride.HasValue;
                }
            });

            Dictionary<Item, Item> dungeonEntrances = new Dictionary<Item, Item>();
            if (settings.RandomizeDungeonEntrances)
            {
                var entrances = new List<Item>
                {
                    Item.AreaWoodFallTempleAccess,
                    Item.AreaSnowheadTempleAccess,
                    Item.AreaGreatBayTempleAccess,
                    Item.AreaInvertedStoneTowerTempleAccess,
                };
                foreach (var entrance in entrances.OrderBy(e => entrances.IndexOf(randomized.ItemList[e].NewLocation.Value)))
                {
                    dungeonEntrances.Add(randomized.ItemList[entrance].NewLocation.Value, entrance);
                }
            }
            var settingsString = settings.ToString();

            var directory = Path.GetDirectoryName(outputSettings.OutputROMFilename);
            var filename = $"{Path.GetFileNameWithoutExtension(outputSettings.OutputROMFilename)}";

            var plainTextRegex = new Regex("[^a-zA-Z0-9' .\\-]+");
            Spoiler spoiler = new Spoiler()
            {
                Version = Randomizer.AssemblyVersion + " + Isghj's Enemizer Test 53.3",
                SettingsString = settingsString,
                Seed = randomized.Seed,
                DungeonEntrances = dungeonEntrances,
                ItemList = itemList.ToList(),
                Logic = randomized.Logic,
                GossipHints = randomized.GossipQuotes?.ToDictionary(me => (GossipQuote) me.Id, (me) =>
                {
                    var message = me.Message.Substring(1);
                    var soundEffect = message.Substring(0, 2);
                    message = message.Substring(2);
                    if (soundEffect == "\x69\x0C")
                    {
                        // real
                    }
                    else if (soundEffect == "\x69\x0A")
                    {
                        // fake
                        message = "FAKE - " + message;
                    }
                    else
                    {
                        // junk
                        message = "JUNK - " + message;
                    }
                    return plainTextRegex.Replace(message.Replace("\x11", " "), "");
                }),
                MessageCosts = randomized.MessageCosts.Select((mc, i) =>
                {
                    if (!mc.HasValue)
                    {
                        return ((string, ushort)?) null;
                    }
                    var messageCost = MessageCost.MessageCosts[i];

                    var name = messageCost.Name;
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        if (messageCost.LocationsAffected.Count > 0)
                        {
                            var location = messageCost.LocationsAffected[0];
                            var mainLocation = location.MainLocation();
                            if (mainLocation.HasValue)
                            {
                                name = $"{mainLocation.Value.Location()} ({location.ToString().Replace(mainLocation.Value.ToString(), "")})";
                            }
                            else
                            {
                                name = location.Location();
                            }
                        }
                        else
                        {
                            name = $"Message Cost [{i}]";
                        }
                    }
                    return (name, mc.Value);
                }).Where(mc => mc != null).Select(mc => mc.Value).ToList(),
            };

            if (outputSettings.GenerateHTMLLog)
            {
                using (StreamWriter newlog = new StreamWriter(Path.Combine(directory, filename + "_Tracker.html")))
                {
                    Templates.HtmlSpoiler htmlspoiler = new Templates.HtmlSpoiler(spoiler);
                    newlog.Write(htmlspoiler.TransformText());
                }
            }
            
            if (outputSettings.GenerateSpoilerLog)
            {
                CreateTextSpoilerLog(spoiler, Path.Combine(directory, filename + "_SpoilerLog.txt"));
            }
        }

        private static void CreateTextSpoilerLog(Spoiler spoiler, string path)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"{"Version:",-17} {spoiler.Version}");
            log.AppendLine($"{"Settings:",-17} {spoiler.SettingsString}");
            log.AppendLine($"{"Seed:",-17} {spoiler.Seed}");
            log.AppendLine();

            if (spoiler.DungeonEntrances.Any())
            {
                log.AppendLine($" {"Entrance",-21}    {"Destination"}");
                log.AppendLine();
                foreach (var kvp in spoiler.DungeonEntrances)
                {
                    log.AppendLine($"{kvp.Key.Entrance(),-21} -> {kvp.Value.Entrance()}");
                }
                log.AppendLine("");
            }

            log.AppendLine($" {"Location",-50}    {"Item"}");
            foreach (var region in spoiler.ItemList.GroupBy(item => item.Region).OrderBy(g => g.Key))
            {
                log.AppendLine();
                log.AppendLine($" {region.Key.Name()}");
                foreach (var item in region.OrderBy(item => item.NewLocationName))
                {
                    log.AppendLine($"{item.NewLocationName,-50} -> {item.Name}" + (item.IsImportant ? "*" : "") + (item.IsRequired ? "*" : item.IsImportantSong ? "^" : ""));
                }
            }

            if (spoiler.MessageCosts.Count > 0)
            {
                log.AppendLine();
                log.AppendLine($" {"Name", -50}    Cost");
                foreach (var (name, cost) in spoiler.MessageCosts)
                {
                    log.AppendLine($"{name,-50} -> {cost}");
                }
            }


            if (spoiler.GossipHints != null && spoiler.GossipHints.Any())
            {
                log.AppendLine();
                log.AppendLine();

                log.AppendLine($" {"Gossip Stone",-25}    {"Message"}");
                foreach (var hint in spoiler.GossipHints.OrderBy(h => h.Key.ToString()))
                {
                    log.AppendLine($"{hint.Key,-25} -> {hint.Value}");
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(log.ToString());
            }
        }
    }
}
