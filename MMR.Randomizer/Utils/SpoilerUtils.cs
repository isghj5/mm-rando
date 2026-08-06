using MMR.Common.Utils;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System; // changed for needing enum? why did enum stop working by itself, dotnet version?

namespace MMR.Randomizer.Utils
{
    public static class SpoilerUtils
    {
        public static void CreateSpoilerLog(RandomizedResult randomized, GameplaySettings settings, OutputSettings outputSettings)
        {
            var itemList = randomized.ItemList
                .Where(io => io.Item.Entrance() == null)
                .Where(io => (io.IsRandomized && io.NewLocation.Value.Region(randomized.ItemList).HasValue) || (io.Item.MainLocation().HasValue && randomized.ItemList[io.Item.MainLocation().Value].IsRandomized))
                .Select(io => new {
                    ItemObject = io.Item.MainLocation().HasValue ? randomized.ItemList.Find(x => x.NewLocation == io.Item.MainLocation().Value) : io,
                    LocationForImportance = io.NewLocation ?? io.Item,
                    Region = io.IsRandomized ? io.NewLocation.Value.Region(randomized.ItemList).Value : io.Item.Region(randomized.ItemList).Value,
                })
                .Select(u => new SpoilerItem(
                    u.ItemObject,
                    u.Region,
                    ItemUtils.IsRequired(u.ItemObject.Item, u.LocationForImportance, randomized),
                    ItemUtils.IsImportant(u.ItemObject.Item, u.LocationForImportance, randomized),
                    ItemUtils.IsLocationJunk(u.LocationForImportance, randomized.Settings),
                    randomized.RequiredSongLocations?.Contains(u.LocationForImportance) == true,
                    settings.ProgressiveUpgrades,
                    randomized.ItemList
                ));

            randomized.Logic.ForEach((il) =>
            {
                if (il.ItemId >= 0)
                {
                    var io = randomized.ItemList[il.ItemId];
                    il.ShouldAutoAcquire = !io.IsRandomized || il.IsFakeItem;
                    il.IsItemRemoved = io.ItemOverride.HasValue;
                }
            });

            Dictionary<Item, Item> dungeonEntrances = new Dictionary<Item, Item>();
            var entrances = new List<Item>();
            if (settings.EntranceMode.HasFlag(EntranceMode.DungeonEntrances))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Dungeon));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.BossRooms))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Boss));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.Grottos))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Grotto));
            }
            if (settings.EntranceMode.HasFlag(EntranceMode.SimpleInteriors))
            {
                entrances.AddRange(Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Interior));
            }
            foreach (var entrance in entrances.OrderBy(e => entrances.IndexOf(randomized.ItemList[e].NewLocation.Value)))
            {
                dungeonEntrances.Add(randomized.ItemList[entrance].NewLocation.Value, entrance);
            }

            var settingsString = settings.ToString();

            var directory = Path.GetDirectoryName(outputSettings.OutputROMFilename);
            var filename = $"{Path.GetFileNameWithoutExtension(outputSettings.OutputROMFilename)}";

            var plainTextRegex = new Regex("[^a-zA-Z0-9' .\\-]+");
            Spoiler spoiler = new Spoiler()
            {
                Version = Randomizer.AssemblyVersion + " + Isghj's Actorizer Test 100.0A4",
                SettingsString = settingsString,
                Seed = randomized.Seed,
                Entrances = dungeonEntrances.Select(kvp => new SpoilerDungeonEntrance(kvp)).ToList(),
                ItemList = itemList.ToList(),
                Logic = randomized.Logic,
                BlitzExtraItems = randomized.BlitzExtraItems.AsReadOnly(),
                RandomStartingItems = randomized.RandomStartingItems.AsReadOnly(),
                Spheres = randomized.Spheres,
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
                    return plainTextRegex.Replace(message.Replace("\x11", " ").Replace("\x10", " "), "");
                }),
                MessageCosts = randomized.MessageCosts.Select((mc, i) =>
                {
                    if (!mc.HasValue)
                    {
                        return null;
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
                    return new NameCostPair
                    {
                        Name = name,
                        Cost = mc.Value,
                    };
                }).Where(mc => mc != null).ToList(),
                GibdoRequirements = settings.RandomizeGibdoRequirements ? randomized.GibdoRequirements.Where(g => g.LogicEntry.HasValue).ToList() : new List<GibdoRequirement>(),
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

            if (outputSettings.GenerateSpoilerLogJson)
            {
                var spoilerJson = new SpoilerOutputJson
                {
                    Settings = settings,
                    Seed = spoiler.Seed,
                    Version = spoiler.Version,
                    DungeonEntrances = spoiler.Entrances,
                    Items = spoiler.ItemList.GroupBy(item => item.Region).OrderBy(g => g.Key).ToDictionary(g => g.Key.Name(), g => g.ToList()),
                    BlitzExtraItems = spoiler.BlitzExtraItems.Select(item => item.Name()).ToList().AsReadOnly(),
                    RandomStartingItems = spoiler.RandomStartingItems.Select(item => item.Name()).ToList().AsReadOnly(),
                    Playthrough = spoiler.Spheres,
                    GossipHints = spoiler.GossipHints,
                    Prices = spoiler.MessageCosts,
                    GibdoRequirements = spoiler.GibdoRequirements.Select(gibdoRequirement => new GibdoRequirementPair
                    {
                        Gibdo = gibdoRequirement.LogicEntry.Value.ToString(),
                        Requirement = $"{gibdoRequirement.ItemRequired.ToString().AddSpaces()}" + (gibdoRequirement.Amount > 1 ? $" ({gibdoRequirement.Amount})" : ""),
                    }).ToList(),
                };
                File.WriteAllText(Path.Combine(directory, filename + "_SpoilerLog.json"), JsonSerializer.Serialize(spoilerJson));
            }
        }

        public static void CreateSettingsJson(int seed, GameplaySettings settings, OutputSettings outputSettings)
        {
            var directory = Path.GetDirectoryName(outputSettings.OutputROMFilename);
            var filename = $"{Path.GetFileNameWithoutExtension(outputSettings.OutputROMFilename)}";

            var settingsJson = new SettingsOutputJson
            {
                Settings = settings,
                Seed = seed,
                Version = Randomizer.AssemblyVersion,
            };
            File.WriteAllText(Path.Combine(directory, filename + "_Settings.json"), JsonSerializer.Serialize(settingsJson));
        }

        private static void CreateTextSpoilerLog(Spoiler spoiler, string path)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"{"Version:",-17} {spoiler.Version}");
            log.AppendLine($"{"Settings:",-17} {spoiler.SettingsString}");
            log.AppendLine($"{"Seed:",-17} {spoiler.Seed}");
            log.AppendLine();

            if (spoiler.BlitzExtraItems.Any())
            {
                log.AppendLine(" Blitz Starting Items");
                foreach (var item in spoiler.BlitzExtraItems)
                {
                    log.AppendLine(item.Name());
                }
                log.AppendLine("");
            }

            if (spoiler.RandomStartingItems.Any())
            {
                log.AppendLine(" Random Starting Items");
                foreach (var item in spoiler.RandomStartingItems)
                {
                    log.AppendLine(item.Name());
                }
                log.AppendLine("");
            }

            if (spoiler.Entrances.Any())
            {
                log.AppendLine();
                log.AppendLine($" {"Entrance",-30}    {"Destination"}");
                foreach (var entranceType in spoiler.Entrances.GroupBy(entrance => entrance.EntranceType).OrderBy(g => g.Key))
                {
                    log.AppendLine();
                    log.AppendLine($" {entranceType.Key}");

                    foreach (var kvp in entranceType)
                    {
                        log.AppendLine($"{kvp.Entrance,-30} -> {kvp.Destination}");
                    }
                }
                log.AppendLine("");
            }

            log.AppendLine($" {"Location",-50}    {"Item"}");
            foreach (var region in spoiler.ItemList.GroupBy(item => item.Region).OrderBy(g => g.Key))
            {
                log.AppendLine();
                log.AppendLine($" {region.Key.Name()}");
                foreach (var item in region.GroupBy(item => new { item.NewLocationName, item.IsImportant, item.IsRequired, item.IsImportantSong, item.IsLocationJunked }).Select(g => g.First()).OrderBy(item => item.NewLocationName))
                {
                    if (item.IsLocationJunked)
                    {
                        log.Append("- ");
                    }
                    log.AppendLine($"{item.NewLocationName,-50} -> {item.Name}" + (item.IsImportant ? "*" : "") + (item.IsRequired ? "*" : item.IsImportantSong ? "^" : ""));
                }
            }

            if (spoiler.MessageCosts.Count > 0)
            {
                log.AppendLine();
                log.AppendLine($" {"Name", -50}    Cost");
                foreach (var price in spoiler.MessageCosts)
                {
                    log.AppendLine($"{price.Name,-50} -> {price.Cost}");
                }
            }

            if (spoiler.GibdoRequirements.Count > 0)
            {
                log.AppendLine();
                log.AppendLine($" {"Gibdo",-50}    Required Item");
                foreach (var gibdoRequirement in spoiler.GibdoRequirements)
                {
                    log.AppendLine($"{gibdoRequirement.LogicEntry.Value.ToString().AddSpaces(),-50} -> {gibdoRequirement.ItemRequired.ToString().AddSpaces()}" + (gibdoRequirement.Amount > 1 ? $" ({gibdoRequirement.Amount})" : ""));
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


            if (spoiler.Spheres != null && spoiler.Spheres.Any())
            {
                log.AppendLine();
                log.AppendLine();
                log.AppendLine(" Playthrough");

                log.AppendLine($"{"Sphere", -10} {"Location",-50}    {"Item"}");
                var i = 0;
                foreach (var sphere in spoiler.Spheres)
                {
                    foreach (var pair in sphere)
                    {
                        log.AppendLine($"{i,-10} {pair.Location,-50} -> {pair.Item}");
                    }
                    log.AppendLine();
                    i++;
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(log.ToString());
            }
        }
    }
}
