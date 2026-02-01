using System;
using System.Collections.Generic;
using System.Linq;
using MMR.Common.Extensions;
using MMR.Common.Utils;

namespace MMR.Randomizer.LogicMigrator
{
    public static partial class Migrator
    {
        public const int CurrentVersion = 30;

        public static string ApplyMigrations(string logic)
        {
            JsonFormatLogic logicObject;
            try
            {
                logicObject = JsonSerializer.Deserialize<JsonFormatLogic>(logic);
            }
            catch (System.Text.Json.JsonException)
            {
                var lines = logic.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                if (GetVersion(lines) < 0)
                {
                    AddVersionNumber(lines);
                }

                if (GetVersion(lines) < 1)
                {
                    AddItemNames(lines);
                }

                if (GetVersion(lines) < 2)
                {
                    AddMoonItems(lines);
                }

                if (GetVersion(lines) < 3)
                {
                    AddRequirementsForSongOath(lines);
                }

                if (GetVersion(lines) < 4)
                {
                    AddSongOfHealing(lines);
                }

                if (GetVersion(lines) < 5)
                {
                    AddIkanaScrubGoldRupee(lines);
                }

                if (GetVersion(lines) < 6)
                {
                    AddPreClocktownChestLinkTrialChestsAndStartingItems(lines);
                }

                if (GetVersion(lines) < 7)
                {
                    AddGreatFairies(lines);
                }

                if (GetVersion(lines) < 8)
                {
                    AddMagicRequirements(lines);
                }

                if (GetVersion(lines) < 9)
                {
                    AddCows(lines);
                }

                if (GetVersion(lines) < 10)
                {
                    AddSkulltulaTokens(lines);
                }

                if (GetVersion(lines) < 11)
                {
                    AddStrayFairies(lines);
                }

                if (GetVersion(lines) < 12)
                {
                    AddMundaneRewards(lines);
                }

                if (GetVersion(lines) < 13)
                {
                    RemoveGormanBrosRaceDayThree(lines);
                }

                if (GetVersion(lines) < 14)
                {
                    AddTricks(lines);
                }

                if (GetVersion(lines) < 15)
                {
                    AddGossipStones(lines);
                }

                if (GetVersion(lines) < 16)
                {
                    AddRupeesAndFixedDrops(lines);
                }

                if (GetVersion(lines) < 17)
                {
                    AddRupeesAndFixedDrops2(lines);
                }

                if (GetVersion(lines) < 18)
                {
                    AddRupeesAndFixedDrops3(lines);
                }

                if (GetVersion(lines) < 19)
                {
                    AddRupeesAndFixedDrops4(lines);
                }

                logicObject = ConvertToJson(lines);
            }

            if (logicObject.Version < 1)
            {
                throw new FormatException("Unexpected logic version number.");
            }

            if (logicObject.Version < 2)
            {
                AddBossRemains(logicObject);
            }

            if (logicObject.Version < 3)
            {
                AddOcarinaAndSongOfTime(logicObject);
            }

            if (logicObject.Version < 4)
            {
                AddMultiLocations(logicObject);
            }

            if (logicObject.Version < 5)
            {
                AddMultiLocationGoronShop(logicObject);
            }

            if (logicObject.Version < 6)
            {
                AddOtherMagicBean(logicObject);
            }

            if (logicObject.Version < 7)
            {
                AddMultiLocationBusinessScrubs(logicObject);
            }

            if (logicObject.Version < 8)
            {
                AddOtherTimeTravel(logicObject);
            }

            if (logicObject.Version < 9)
            {
                AddBeansAndDekuPlayground(logicObject);
            }

            if (logicObject.Version < 10)
            {
                AddRoyalWallet(logicObject);
            }

            if (logicObject.Version < 11)
            {
                AddMultiLocationClockTownFairy(logicObject);
            }

            if (logicObject.Version < 12)
            {
                AddGaroHints(logicObject);
            }

            if (logicObject.Version < 13)
            {
                AddBossDoors(logicObject);
            }

            if (logicObject.Version < 14)
            {
                AddLullabyIntro(logicObject);
            }

            if (logicObject.Version < 15)
            {
                AddNotebookEntries(logicObject);
            }

            if (logicObject.Version < 16)
            {
                AddFairies(logicObject);
            }

            if (logicObject.Version < 17)
            {
                AddFrogs(logicObject);
            }

            if (logicObject.Version < 18)
            {
                AddSettings(logicObject);
            }

            if (logicObject.Version < 19)
            {
                AddWellFairies(logicObject);
            }

            if (logicObject.Version < 20)
            {
                AddInaccessible(logicObject);
            }

            if (logicObject.Version < 21)
            {
                ReplaceSettingsWithExpressions(logicObject);
            }

            if (logicObject.Version < 22)
            {
                AddOtherCredits(logicObject);
            }

            if (logicObject.Version < 23)
            {
                RemoveStoneTowerTemplePot(logicObject);
            }

            if (logicObject.Version < 24)
            {
                AddOtherKillMajora(logicObject);
            }

            if (logicObject.Version < 25)
            {
                AddMoonFairies(logicObject);
            }

            if (logicObject.Version < 26)
            {
                AddPalmTrees(logicObject);
            }

            if (logicObject.Version < 27)
            {
                AddGibdos(logicObject);
            }

            if (logicObject.Version < 28)
            {
                AddGrottos(logicObject);
            }

            if (logicObject.Version < 29)
            {
                AddInteriors(logicObject);
            }

            if (logicObject.Version < 30)
            {
                AddZoraEggs(logicObject);
            }

            return JsonSerializer.Serialize(logicObject);
        }

        public static int GetVersion(List<string> lines)
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("-version"))
                {
                    return int.Parse(line.Split(' ')[1]);
                }
                else
                {
                    break;
                }
            }
            return -1;
        }

        private static void AddVersionNumber(List<string> lines)
        {
            lines.Insert(0, "-version 0");
        }

        private static void AddItemNames(List<string> lines)
        {
            if (lines[1] == "- Deku Mask")
            {
                lines[0] = "-version 1";
                return;
            }
            lines.RemoveAll(line => line.StartsWith("-"));
            var itemNames = new string[] {"Deku Mask", "Hero's Bow", "Fire Arrow", "Ice Arrow", "Light Arrow", "Bomb Bag (20)", "Magic Bean",
                "Powder Keg", "Pictobox", "Lens of Truth", "Hookshot", "Great Fairy's Sword", "Witch Bottle", "Aliens Bottle", "Gold Dust",
                "Beaver Race Bottle", "Dampe Bottle", "Chateau Bottle", "Bombers' Notebook", "Razor Sword", "Gilded Sword", "Mirror Shield",
                "Town Archery Quiver (40)", "Swamp Archery Quiver (50)", "Town Bomb Bag (30)", "Mountain Bomb Bag (40)", "Town Wallet (200)", "Ocean Wallet (500)", "Moon's Tear",
                "Land Title Deed", "Swamp Title Deed", "Mountain Title Deed", "Ocean Title Deed", "Room Key", "Letter to Kafei", "Pendant of Memories",
                "Letter to Mama", "Mayor Dotour HP", "Postman HP", "Rosa Sisters HP", "??? HP", "Grandma Short Story HP", "Grandma Long Story HP",
                "Keaton Quiz HP", "Deku Playground HP", "Town Archery HP", "Honey and Darling HP", "Swordsman's School HP", "Postbox HP",
                "Termina Field Gossips HP", "Termina Field Business Scrub HP", "Swamp Archery HP", "Pictograph Contest HP", "Boat Archery HP",
                "Frog Choir HP", "Beaver Race HP", "Seahorse HP", "Fisherman Game HP", "Evan HP", "Dog Race HP", "Poe Hut HP",
                "Treasure Chest Game HP", "Peahat Grotto HP", "Dodongo Grotto HP", "Woodfall Chest HP", "Twin Islands Chest HP",
                "Ocean Spider House HP", "Graveyard Iron Knuckle HP", "Postman's Hat", "All Night Mask", "Blast Mask", "Stone Mask", "Great Fairy's Mask",
                "Keaton Mask", "Bremen Mask", "Bunny Hood", "Don Gero's Mask", "Mask of Scents", "Romani Mask", "Circus Leader's Mask", "Kafei's Mask",
                "Couple's Mask", "Mask of Truth", "Kamaro's Mask", "Gibdo Mask", "Garo Mask", "Captain's Hat", "Giant's Mask", "Goron Mask", "Zora Mask",
                "Song of Soaring", "Epona's Song", "Song of Storms", "Sonata of Awakening", "Goron Lullaby", "New Wave Bossa Nova",
                "Elegy of Emptiness", "Oath to Order", "Poison swamp access", "Woodfall Temple access", "Woodfall clear", "North access", "Snowhead Temple access",
                "Snowhead clear", "Epona access", "West access", "Pirates' Fortress access", "Great Bay Temple access", "Great Bay clear", "East access",
                "Ikana Canyon access", "Stone Tower Temple access", "Inverted Stone Tower Temple access", "Ikana clear", "Explosives", "Arrows", "(Unused)", "(Unused)",
                "(Unused)", "(Unused)", "(Unused)",
                "Woodfall Map", "Woodfall Compass", "Woodfall Boss Key", "Woodfall Key 1", "Snowhead Map", "Snowhead Compass", "Snowhead Boss Key",
                "Snowhead Key 1 - block room", "Snowhead Key 2 - icicle room", "Snowhead Key 3 - bridge room", "Great Bay Map", "Great Bay Compass", "Great Bay Boss Key", "Great Bay Key 1",
                "Stone Tower Map", "Stone Tower Compass", "Stone Tower Boss Key", "Stone Tower Key 1 - armos room", "Stone Tower Key 2 - eyegore room", "Stone Tower Key 3 - updraft room",
                "Stone Tower Key 4 - death armos maze", "Trading Post Red Potion", "Trading Post Green Potion", "Trading Post Shield", "Trading Post Fairy",
                "Trading Post Stick", "Trading Post Arrow 30", "Trading Post Nut 10", "Trading Post Arrow 50", "Witch Shop Blue Potion",
                "Witch Shop Red Potion", "Witch Shop Green Potion", "Bomb Shop Bomb 10", "Bomb Shop Chu 10", "Goron Shop Bomb 10", "Goron Shop Arrow 10",
                "Goron Shop Red Potion", "Zora Shop Shield", "Zora Shop Arrow 10", "Zora Shop Red Potion", "Bottle: Fairy", "Bottle: Deku Princess",
                "Bottle: Fish", "Bottle: Bug", "Bottle: Poe", "Bottle: Big Poe", "Bottle: Spring Water", "Bottle: Hot Spring Water", "Bottle: Zora Egg",
                "Bottle: Mushroom", "Lens Cave 20r", "Lens Cave 50r", "Bean Grotto 20r", "HSW Grotto 20r", "Graveyard Bad Bats", "Ikana Grotto",
                "PF 20r Lower", "PF 20r Upper", "PF Tank 20r", "PF Guard Room 100r", "PF HP Room 20r", "PF HP Room 5r", "PF Maze 20r", "PR 20r (1)", "PR 20r (2)",
                "Bombers' Hideout 100r", "Termina Bombchu Grotto", "Termina 20r Grotto", "Termina Underwater 20r", "Termina Grass 20r", "Termina Stump 20r",
                "Great Bay Coast Grotto", "Great Bay Cape Ledge (1)", "Great Bay Cape Ledge (2)", "Great Bay Cape Grotto", "Great Bay Cape Underwater",
                "PF Exterior 20r (1)", "PF Exterior 20r (2)", "PF Exterior 20r (3)", "Path to Swamp Grotto", "Doggy Racetrack 50r", "Graveyard Grotto",
                "Swamp Grotto", "Woodfall 5r", "Woodfall 20r", "Well Right Path 50r", "Well Left Path 50r", "Mountain Village Chest (Spring)",
                "Mountain Village Grotto Bottle (Spring)", "Path to Ikana 20r", "Path to Ikana Grotto", "Stone Tower 100r", "Stone Tower Bombchu 10",
                "Stone Tower Magic Bean", "Path to Snowhead Grotto", "Twin Islands 20r", "Secret Shrine HP", "Secret Shrine Dinolfos",
                "Secret Shrine Wizzrobe", "Secret Shrine Wart", "Secret Shrine Garo Master", "Inn Staff Room", "Inn Guest Room", "Mystery Woods Grotto",
                "East Clock Town 100r", "South Clock Town 20r", "South Clock Town 50r", "Bank HP", "South Clock Town HP", "North Clock Town HP",
                "Path to Swamp HP", "Swamp Scrub HP", "Deku Palace HP", "Goron Village Scrub HP", "Bio Baba Grotto HP", "Lab Fish HP", "Great Bay Like-Like HP",
                "Pirates' Fortress HP", "Zora Hall Scrub HP", "Path to Snowhead HP", "Great Bay Coast HP", "Ikana Scrub HP", "Ikana Castle HP",
                "Odolwa Heart Container", "Goht Heart Container", "Gyorg Heart Container", "Twinmold Heart Container", "Map: Clock Town", "Map: Woodfall",
                "Map: Snowhead", "Map: Romani Ranch", "Map: Great Bay", "Map: Stone Tower", "Goron Racetrack Grotto" };
            for (var i = 0; i < itemNames.Length; i++)
            {
                lines.Insert(i * 5, $"- {itemNames[i]}");
            }
            lines.Insert(0, "-version 1");
        }

        private static void AddMoonItems(List<string> lines)
        {
            lines[0] = "-version 2";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 255,
                    Conditionals = Enumerable.Range(68, 20).Select(i => new List<int> { i }).ToList()
                },
                new MigrationItem
                {
                    ID = 256,
                    Conditionals = Enumerable.Range(68, 20).Combinations(2).Select(a => a.ToList()).ToList()
                },
                new MigrationItem
                {
                    ID = 257,
                    Conditionals = Enumerable.Range(68, 20).Combinations(3).Select(a => a.ToList()).ToList()
                },
                new MigrationItem
                {
                    ID = 258,
                    Conditionals = Enumerable.Range(68, 20).Combinations(4).Select(a => a.ToList()).ToList()
                },
                new MigrationItem
                {
                    ID = 259,
                    DependsOnItems = new List<int>
                    {
                        97, 100, 103, 108, 113
                    }
                },
                new MigrationItem
                {
                    ID = 260,
                    DependsOnItems = new List<int>
                    {
                        259, 0, 255
                    }
                },
                new MigrationItem
                {
                    ID = 261,
                    DependsOnItems = new List<int>
                    {
                        259, 88, 256
                    }
                },
                new MigrationItem
                {
                    ID = 262,
                    DependsOnItems = new List<int>
                    {
                        259, 89, 257
                    }
                },
                new MigrationItem
                {
                    ID = 263,
                    DependsOnItems = new List<int>
                    {
                        259, 258, 114, 115, 2, 10
                    }
                },
                new MigrationItem
                {
                    ID = 264,
                    DependsOnItems = new List<int>
                    {
                        259, 0, 88, 89, 114, 115, 2, 10
                    }
                    .Concat(Enumerable.Range(68, 20))
                    .ToList()
                }
            };
            var itemNames = new string[]
            {
                "One Mask", "Two Masks", "Three Masks", "Four Masks", "Moon Access", "Deku Trial HP", "Goron Trial HP", "Zora Trial HP", "Link Trial HP", "Fierce Deity's Mask"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id => 
                        {
                            var itemId = int.Parse(id);
                            if (itemId >= 255)
                            {
                                itemId += newItems.Length;
                            }
                            return itemId;
                        }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 255]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddRequirementsForSongOath(List<string> lines)
        {
            lines[0] = "-version 3";
            var oathIndex = lines.FindIndex(s => s == "- Oath to Order");
            lines[oathIndex + 1] = "";
            lines[oathIndex + 2] = $"100;103;108;113";
            lines[oathIndex + 3] = "0";
            lines[oathIndex + 4] = "0";
        }

        private static void AddSongOfHealing(List<string> lines)
        {
            lines[0] = "-version 4";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 90
                }
            };
            var itemNames = new string[]
            {
                "Song of Healing"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 90)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 90]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
            var requireSongOfHealing = new int[] { 83, 84, 88, 89 }; // kamaro, gidbo, goron, zora masks
            foreach (var id in requireSongOfHealing)
            {
                lines[id * 5 + 2] = lines[id * 5 + 2].Length == 0 ? "90" : "90," + lines[id * 5 + 2];
            }
        }

        private static void AddIkanaScrubGoldRupee(List<string> lines)
        {
            lines[0] = "-version 5";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 256,
                    DependsOnItems = new List<int> { 110, 89, 32 } // east access, zora mask, ocean deed
                }
            };
            var itemNames = new string[]
            {
                "Ikana Scrub Gold Rupee"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 256)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 256]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddPreClocktownChestLinkTrialChestsAndStartingItems(List<string> lines)
        {
            lines[0] = "-version 6";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 267,
                    DependsOnItems = new List<int> { 261, 260, 10 }
                },
                new MigrationItem
                {
                    ID = 268,
                    DependsOnItems = new List<int> { 261, 260, 10 }
                },
                new MigrationItem
                {
                    ID = 269,
                    DependsOnItems = new List<int> { 261 }
                },
                new MigrationItem
                {
                    ID = 270,
                },
                new MigrationItem
                {
                    ID = 271,
                },
                new MigrationItem
                {
                    ID = 272,
                },
                new MigrationItem
                {
                    ID = 273,
                },
            };
            var itemNames = new string[]
            {
                "Link Trial 30 Arrows",
                "Link Trial 10 Bombchu",
                "Pre-Clocktown 10 Deku Nuts",
                "Starting Sword",
                "Starting Shield",
                "Starting Heart 1",
                "Starting Heart 2",
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 267)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 267]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddGreatFairies(List<string> lines)
        {
            lines[0] = "-version 7";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 11,
                    Conditionals = new List<List<int>>
                    {
                        new List<int> { 0 },
                        new List<int> { 92 },
                        new List<int> { 93 },
                    }
                },
                new MigrationItem
                {
                    ID = 12,
                    DependsOnItems = new List<int> { 104 },
                },
                new MigrationItem
                {
                    ID = 13,
                    DependsOnItems = new List<int> { 107 },
                },
                new MigrationItem
                {
                    ID = 14,
                    DependsOnItems = new List<int> { 112 },
                },
            };
            var itemNames = new string[]
            {
                "Great Fairy Magic Meter",
                "Great Fairy Spin Attack",
                "Great Fairy Extended Magic",
                "Great Fairy Double Defense"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || (i % 5 != 2 && i % 5 != 3))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 11)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 11]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }

            var updateItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 76, // Great Fairy's Mask
                    // remove requirements
                }
            };
            foreach (var item in updateItems)
            {
                lines[item.ID * 5 + 2] = string.Join(",", item.DependsOnItems);
                lines[item.ID * 5 + 3] = string.Join(";", item.Conditionals.Select(c => string.Join(",", c)));
            }
        }

        private static void AddMagicRequirements(List<string> lines)
        {
            lines[0] = "-version 8";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 278,
                    Conditionals = new List<List<int>>
                    {
                        new List<int> { 11 },
                        new List<int> { 13 },
                    }
                },
            };
            var itemNames = new string[]
            {
                "Magic Meter"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 278)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 278]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
            
            var requireMagic = new int[] { 2, 3, 4, 9 }; // fire arrow, ice arrow, light arrow, lens of truth
            for (var i = 0; i < lines.Count; i++)
            {
                if (i%5 != 2 && i%5 != 3)
                {
                    continue;
                }
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section =>
                    {
                        if (section.Split(',').Select(int.Parse).Intersect(requireMagic).Any())
                        {
                            section += ",278";
                        }
                        return section;
                    }).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
        }

        private static void AddCows(List<string> lines)
        {
            lines[0] = "-version 9";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 278,
                    DependsOnItems = new List<int> { 96, 109 } // epona's song, epona access
                },
                new MigrationItem
                {
                    ID = 279,
                    DependsOnItems = new List<int> { 96, 109 } // epona's song, epona access
                },
                new MigrationItem
                {
                    ID = 280,
                    DependsOnItems = new List<int> { 265 } // moon access / unaccessible
                },
                new MigrationItem
                {
                    ID = 281,
                    DependsOnItems = new List<int> { 96, 115, 88, 173 }, // epona's song, ikana canyon access, gibdo mask, hot spring water, 
                    Conditionals = new List<List<int>>
                    {
                        new List<int> { 4 }, // light arrow
                        new List<int> { 0, 103 }, // deku mask, poison swamp access
                    },
                },
                new MigrationItem
                {
                    ID = 282,
                    DependsOnItems = new List<int> { 96, 119 } // epona's song, explosives
                },
                new MigrationItem
                {
                    ID = 283,
                    DependsOnItems = new List<int> { 96, 119 } // epona's song, explosives
                },
                new MigrationItem
                {
                    ID = 284,
                    DependsOnItems = new List<int> { 96, 110, 10 } // epona's song, west access, hookshot
                },
                new MigrationItem
                {
                    ID = 285,
                    DependsOnItems = new List<int> { 96, 110, 10 } // epona's song, west access, hookshot
                },
            };
            var itemNames = new string[]
            {
                "Ranch Cow #1 Milk",
                "Ranch Cow #2 Milk",
                "Ranch Cow #3 Milk",
                "Well Cow Milk",
                "Termina Grotto Cow #1 Milk",
                "Termina Grotto Cow #2 Milk",
                "Great Bay Coast Grotto Cow #1 Milk",
                "Great Bay Coast Grotto Cow #2 Milk",
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 278)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 278]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, "0");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddSkulltulaTokens(List<string> lines)
        {
            lines[0] = "-version 10";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 286,
                    DependsOnItems = new List<int> { 103 }, // Poison swamp access
                },
                new MigrationItem
                {
                    ID = 287,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 288,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 289,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 290,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 291,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 292,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 293,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 294,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 295,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 296,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 297,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 298,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 299,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 300,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 301,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 302,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 303,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 304,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 305,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 306,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 307,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 308,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 309,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 310,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 311,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 312,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 313,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 314,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 315,
                    DependsOnItems = new List<int> { 103 },
                },
                new MigrationItem
                {
                    ID = 316,
                    DependsOnItems = new List<int> { 110, 119 }, // West access, Explosives
                },
                new MigrationItem
                {
                    ID = 317,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 318,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 319,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 320,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 321,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 322,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 323,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 324,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 325,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 326,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 327,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 328,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 329,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 330,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 331,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 332,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 333,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 334,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 335,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 336,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 337,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 338,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 339,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 340,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 341,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 342,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 343,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 344,
                    DependsOnItems = new List<int> { 110, 119 },
                },
                new MigrationItem
                {
                    ID = 345,
                    DependsOnItems = new List<int> { 110, 119 },
                },
            };
            var itemNames = new string[]
            {
                "Swamp Skulltula Main Room Near Ceiling", "Swamp Skulltula Gold Room Near Ceiling", "Swamp Skulltula Monument Room Torch", "Swamp Skulltula Gold Room Pillar", "Swamp Skulltula Pot Room Jar",

                "Swamp Skulltula Tree Room Grass 1", "Swamp Skulltula Tree Room Grass 2", "Swamp Skulltula Main Room Water", "Swamp Skulltula Main Room Lower Left Soft Soil", "Swamp Skulltula Monument Room Crate 1",

                "Swamp Skulltula Main Room Upper Soft Soil", "Swamp Skulltula Main Room Lower Right Soft Soil", "Swamp Skulltula Monument Room Lower Wall", "Swamp Skulltula Monument Room On Monument", "Swamp Skulltula Main Room Pillar",

                "Swamp Skulltula Pot Room Pot 1", "Swamp Skulltula Pot Room Pot 2", "Swamp Skulltula Gold Room Hive", "Swamp Skulltula Main Room Upper Pillar", "Swamp Skulltula Pot Room Behind Vines",

                "Swamp Skulltula Tree Room Tree 1", "Swamp Skulltula Pot Room Wall", "Swamp Skulltula Pot Room Hive 1", "Swamp Skulltula Tree Room Tree 2", "Swamp Skulltula Gold Room Wall",

                "Swamp Skulltula Tree Room Hive", "Swamp Skulltula Monument Room Crate 2", "Swamp Skulltula Pot Room Hive 2", "Swamp Skulltula Tree Room Tree 3", "Swamp Skulltula Main Room Jar",

                "Ocean Skulltula Storage Room Behind Boat", "Ocean Skulltula Library Hole Behind Picture", "Ocean Skulltula Library Hole Behind Cabinet", "Ocean Skulltula Library On Corner Bookshelf", "Ocean Skulltula 2nd Room Ceiling Edge",

                "Ocean Skulltula 2nd Room Ceiling Plank", "Ocean Skulltula Colored Skulls Ceiling Edge", "Ocean Skulltula Library Ceiling Edge", "Ocean Skulltula Storage Room Ceiling Web", "Ocean Skulltula Storage Room Behind Crate",

                "Ocean Skulltula 2nd Room Jar", "Ocean Skulltula Entrance Right Wall", "Ocean Skulltula Entrance Left Wall", "Ocean Skulltula 2nd Room Webbed Hole", "Ocean Skulltula Entrance Web",

                "Ocean Skulltula Colored Skulls Chandelier 1", "Ocean Skulltula Colored Skulls Chandelier 2", "Ocean Skulltula Colored Skulls Chandelier 3", "Ocean Skulltula Colored Skulls Behind Picture", "Ocean Skulltula Library Behind Picture",

                "Ocean Skulltula Library Behind Bookcase 1", "Ocean Skulltula Storage Room Crate", "Ocean Skulltula 2nd Room Webbed Pot", "Ocean Skulltula 2nd Room Upper Pot", "Ocean Skulltula Colored Skulls Pot",

                "Ocean Skulltula Storage Room Jar", "Ocean Skulltula 2nd Room Lower Pot", "Ocean Skulltula Library Behind Bookcase 2", "Ocean Skulltula 2nd Room Behind Skull 1", "Ocean Skulltula 2nd Room Behind Skull 2"
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 286)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 286]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddStrayFairies(List<string> lines)
        {
            lines[0] = "-version 11";
            var newItems = new MigrationItem[]
            {
                new MigrationItem
                {
                    ID = 346,
                },
                new MigrationItem
                {
                    ID = 347,
                },
                new MigrationItem
                {
                    ID = 348,
                },
                new MigrationItem
                {
                    ID = 349,
                },
                new MigrationItem
                {
                    ID = 350,
                },
                new MigrationItem
                {
                    ID = 351,
                },
                new MigrationItem
                {
                    ID = 352,
                },
                new MigrationItem
                {
                    ID = 353,
                },
                new MigrationItem
                {
                    ID = 354,
                },
                new MigrationItem
                {
                    ID = 355,
                },
                new MigrationItem
                {
                    ID = 356,
                },
                new MigrationItem
                {
                    ID = 357,
                },
                new MigrationItem
                {
                    ID = 358,
                },
                new MigrationItem
                {
                    ID = 359,
                },
                new MigrationItem
                {
                    ID = 360,
                },
                new MigrationItem
                {
                    ID = 361,
                },
                new MigrationItem
                {
                    ID = 362,
                },
                new MigrationItem
                {
                    ID = 363,
                },
                new MigrationItem
                {
                    ID = 364,
                },
                new MigrationItem
                {
                    ID = 365,
                },
                new MigrationItem
                {
                    ID = 366,
                },
                new MigrationItem
                {
                    ID = 367,
                },
                new MigrationItem
                {
                    ID = 368,
                },
                new MigrationItem
                {
                    ID = 369,
                },
                new MigrationItem
                {
                    ID = 370,
                },
                new MigrationItem
                {
                    ID = 371,
                },
                new MigrationItem
                {
                    ID = 372,
                },
                new MigrationItem
                {
                    ID = 373,
                },
                new MigrationItem
                {
                    ID = 374,
                },
                new MigrationItem
                {
                    ID = 375,
                },
                new MigrationItem
                {
                    ID = 376,
                },
                new MigrationItem
                {
                    ID = 377,
                },
                new MigrationItem
                {
                    ID = 378,
                },
                new MigrationItem
                {
                    ID = 379,
                },
                new MigrationItem
                {
                    ID = 380,
                },
                new MigrationItem
                {
                    ID = 381,
                },
                new MigrationItem
                {
                    ID = 382,
                },
                new MigrationItem
                {
                    ID = 383,
                },
                new MigrationItem
                {
                    ID = 384,
                },
                new MigrationItem
                {
                    ID = 385,
                },
                new MigrationItem
                {
                    ID = 386,
                },
                new MigrationItem
                {
                    ID = 387,
                },
                new MigrationItem
                {
                    ID = 388,
                },
                new MigrationItem
                {
                    ID = 389,
                },
                new MigrationItem
                {
                    ID = 390,
                },
                new MigrationItem
                {
                    ID = 391,
                },
                new MigrationItem
                {
                    ID = 392,
                },
                new MigrationItem
                {
                    ID = 393,
                },
                new MigrationItem
                {
                    ID = 394,
                },
                new MigrationItem
                {
                    ID = 395,
                },
                new MigrationItem
                {
                    ID = 396,
                },
                new MigrationItem
                {
                    ID = 397,
                },
                new MigrationItem
                {
                    ID = 398,
                },
                new MigrationItem
                {
                    ID = 399,
                },
                new MigrationItem
                {
                    ID = 400,
                },
                new MigrationItem
                {
                    ID = 401,
                },
                new MigrationItem
                {
                    ID = 402,
                },
                new MigrationItem
                {
                    ID = 403,
                },
                new MigrationItem
                {
                    ID = 404,
                },
                new MigrationItem
                {
                    ID = 405,
                },
                new MigrationItem
                {
                    ID = 406,
                },
            };
            var itemNames = new string[]
            {
                "Clock Town Stray Fairy",
                "Woodfall Pre-Boss Room Bubble 1",
                "Woodfall Entrance Fairy",
                "Woodfall Pre-Boss Room Bubble 2",
                "Woodfall Pre-Boss Room Bubble 3",
                "Woodfall Deku Baba",
                "Woodfall Poison Water Bubble",
                "Woodfall Main Room Bubble",
                "Woodfall Skulltula",
                "Woodfall Pre-Boss Room Bubble 4",
                "Woodfall Main Room Switch",
                "Woodfall Entrance Platform",
                "Woodfall Dark Room",
                "Woodfall Jar Fairy",
                "Woodfall Bridge Room Hive",
                "Woodfall Platform Room Hive",
                "Snowhead Snow Room Bubble",
                "Snowhead Ceiling Bubble",
                "Snowhead Dinolfos 1",
                "Snowhead Bridge Room Bubble 1",
                "Snowhead Bridge Room Bubble 2",
                "Snowhead Dinolfos 2",
                "Snowhead Map Room Fairy",
                "Snowhead Map Room Ledge",
                "Snowhead Basement",
                "Snowhead Twin Block",
                "Snowhead Icicle Room Wall",
                "Snowhead Main Room Wall",
                "Snowhead Torches",
                "Snowhead Ice Puzzle",
                "Snowhead Crate",
                "Great Bay Skulltula",
                "Great Bay Pre-Boss Room Underwater Bubble",
                "Great Bay Water Control Room Underwater Bubble",
                "Great Bay Pre-Boss Room Bubble",
                "Great Bay Waterwheel Room",
                "Great Bay Green Valve",
                "Great Bay Seesaw Room",
                "Great Bay Waterwheel Room",
                "Great Bay Entrance Torches",
                "Great Bay Bio Babas",
                "Great Bay Underwater Barrel",
                "Great Bay Whirlpool Jar",
                "Great Bay Whirlpool Barrel",
                "Great Bay Dexihands Jar",
                "Great Bay Ledge Jar",
                "Stone Tower Mirror Sun Block",
                "Stone Tower Eyegore",
                "Stone Tower Lava Room Fire Ring", // todo check location name
                "Stone Tower Updraft Fire Ring",
                "Stone Tower Mirror Sun Switch",
                "Stone Tower Boss Warp",
                "Stone Tower Wizzrobe",
                "Stone Tower Death Armos",
                "Stone Tower Updraft Frozen Eye",
                "Stone Tower Thin Bridge",
                "Stone Tower Basement Ledge",
                "Stone Tower Statue Eye",
                "Stone Tower Underwater",
                "Stone Tower Bridge Crystal",
                "Stone Tower Lava Room Ledge", // todo check location name
            };
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 346)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 346]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void AddMundaneRewards(List<string> lines)
        {
            lines[0] = "-version 12";
            var itemNames = new string[]
            {
                "Lottery 50r", "Bank 5r", "Milk Bar Chateau", "Milk Bar Milk", "Deku Playground 50r", "Honey and Darling 50r", "Kotake Mushroom Sale 20r", "Pictograph Contest 5r",
                "Pictograph Contest 20r", "Swamp Scrub Magic Bean", "Ocean Scrub Green Potion", "Canyon Scrub Blue Potion", "Zora Hall Stage Lights 5r", "Gorman Bros Purchase Milk",
                "Gorman Bros Race Milk", "Ocean Spider House 50r", "Ocean Spider House 20", "Lulu Pictograph 5r", "Lulu Pictograph 20r", "Treasure Chest Game 50r", "Treasure Chest Game 20r",
                "Treasure Chest Game Deku Nuts", "Curiosity Shop 5r", "Curiosity Shop 20r", "Curiosity Shop 50r", "Curiosity Shop 200r", "Seahorse",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = 407 + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 407)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 5 + 1, $"- {itemNames[item.ID - 407]}");
                lines.Insert(item.ID * 5 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 5 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 5 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 5 + 5, "0");
            }
        }

        private static void RemoveGormanBrosRaceDayThree(List<string> lines)
        {
            lines[0] = "-version 13";

            var itemsToRemove = new List<int>
            {
                421
            };

            foreach (var removeId in itemsToRemove.OrderByDescending(id => id))
            {
                for (var i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    var updatedItemSections = line
                        .Split(';')
                        .Select(section => section.Split(',').Select(int.Parse).Where(id => id != removeId).Select(id =>
                        {
                            if (id > removeId)
                            {
                                id--;
                            }
                            return id;
                        }).ToList()).Where(section => section.Any()).ToList();
                    lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
                }

                lines.RemoveRange(removeId * 5 + 1, 5);
            }
        }

        private static void AddTricks(List<string> lines)
        {
            lines[0] = "-version 14";

            for (var i = 1; i < lines.Count; i += 6)
            {
                lines.Insert(i + 5, "");
            }
        }

        private static void AddGossipStones(List<string> lines)
        {
            lines[0] = "-version 15";
            var itemNames = new string[]
            {
                "GossipTerminaSouth",
                "GossipSwampPotionShop",
                "GossipMountainSpringPath",
                "GossipMountainPath",
                "GossipOceanZoraGame",
                "GossipCanyonRoad",
                "GossipCanyonDock",
                "GossipCanyonSpiritHouse",
                "GossipTerminaMilk",
                "GossipTerminaWest",
                "GossipTerminaNorth",
                "GossipTerminaEast",
                "GossipRanchTree",
                "GossipRanchBarn",
                "GossipMilkRoad",
                "GossipOceanFortress",
                "GossipSwampRoad",
                "GossipTerminaObservatory",
                "GossipRanchCuccoShack",
                "GossipRanchRacetrack",
                "GossipRanchEntrance",
                "GossipCanyonRavine",
                "GossipMountainSpringFrog",
                "GossipSwampSpiderHouse",
                "GossipTerminaGossipLarge",
                "GossipTerminaGossipGuitar",
                "GossipTerminaGossipPipes",
                "GossipTerminaGossipDrums",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = 433 + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 433)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 6 + 1, $"- {itemNames[item.ID - 433]}");
                lines.Insert(item.ID * 6 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 6 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 6 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 6 + 5, "0");
                lines.Insert(item.ID * 6 + 6, "");
            }
        }

        private static void AddRupeesAndFixedDrops(List<string> lines)
        {
            lines[0] = "-version 16";
            var itemNames = new string[]
            {
                "Ikana Castle Courtyard Grass",
                "Ikana Castle Courtyard Grass 2",
                "Night 1 Grave Pot",
                "Night 2 Grave Pot",
                "Night 1 Grave Pot 2",
                "Cucco Shack Crate",
                "Dampe's Basement Pot",
                "Dampe's Basement Pot 2",
                "Dampe's Basement Pot 3",
                "Dampe's Basement Pot 4",
                "Goron Village Small Snowball",
                "Goron Village Small Snowball 2",
                "Great Bay Coast Pot",
                "Great Bay Coast Pot 2",
                "Great Bay Coast Pot 3",
                "Great Bay Coast Pot 4",
                "Great Bay Temple Red Valve Barrel",
                "Ikana King Pot",
                "Ikana King Pot 2",
                "Ikana King Entry Pot",
                "Ikana King Entry Pot 2",
                "Ikana Graveyard Grass",
                "Oceanside Spider House Entrance Pot",
                "Oceanside Spider House Entrance Pot 2",
                "Pirates' Fortress Sewer Gate Pot",
                "Pirates' Fortress Guarded Egg Pot",
                "Pirates' Fortress Barrel Maze Egg Pot",
                "Pirates' Fortress Sewer Exit Pot",
                "Secret Shrine Underwater Pot",
                "Secret Shrine Underwater Pot 2",
                "Snowhead Temple Icicle Room Snowball",
                "Snowhead Temple Icicle Room Snowball 2",
                "Stone Tower Upper Scarecrow Pot",
                "Stone Tower Upper Scarecrow Pot 2",
                "Great Bay Coast Pot 5",
                "Great Bay Temple Seesaw Room Pot",
                "Great Bay Temple Green Pump Barrel",
                "Ikana Canyon Grass",
                "Milk Road Grass",
                "Mountain Village Spring Snowball",
                "Mountain Village Winter Small Snowball",
                "Pirates' Fortress Lone Guard Egg Pot",
                "Pirates' Fortress Cage Pot",
                "Ranch Crate",
                "Snowhead Small Snowball",
                "Stone Tower Owl Pot",
                "Zora Cape Owl Pot",
                "Observatory Scarecrow Pot",
                "Observatory Scarecrow Pot 2",
                "Deku Palace Item",
                "Deku Palace Item 2",
                "Deku Palace Item 3",
                "Deku Palace Item 4",
                "Deku Palace Item 5",
                "Doggy Racetrack Pot",
                "Doggy Racetrack Pot 2",
                "Doggy Racetrack Pot 3",
                "Doggy Racetrack Pot 4",
                "Goron Village Large Snowball",
                "Goron Village Large Snowball 2",
                "Goron Village Large Snowball 3",
                "Great Bay Coast Ledge Pot",
                "Great Bay Coast Ledge Pot 2",
                "Great Bay Coast Ledge Pot 3",
                "Great Bay Temple Water Control Room Item",
                "Great Bay Temple Water Control Room Item 2",
                "Bio Baba Grotto Hive",
                "Laundry Pool Crate",
                "Mountain Village Day 3 Snowball",
                "Mountain Village Day 2 Snowball",
                "Twin Islands Item",
                "Twin Islands Item 2",
                "Twin Islands Item 3",
                "Twin Islands Item 4",
                "Pirates' Fortress Barrel Maze Egg Pot 2",
                "Pirates' Fortress Sewer Exit Barrel",
                "Pirates' Fortress Sewer Exit Barrel 2",
                "Pirates' Fortress Sewer Exit Barrel 3",
                "Pirates' Fortress Cage Room Barrel",
                "Ranch Barn Hay Item",
                "Ranch Barn Hay Item 2",
                "Snowhead Temple Icicle Room Snowball 3",
                "Snowhead Temple Icicle Room Snowball 4",
                "Snowhead Temple Icicle Room Snowball 5",
                "Snowhead Temple Elevator Room Crate",
                "Snowhead Temple Elevator Room Crate 2",
                "Snowhead Temple Elevator Room Crate 3",
                "Snowhead Temple Elevator Room Crate 4",
                "Snowhead Temple Elevator Room Crate 5",
                "Snowhead Temple Safety Bridge Pot",
                "Snowhead Temple Safety Bridge Pot 2",
                "Cleared Swamp Potion Shop Pot",
                "Swamp Near Frog Item",
                "Swamp Near Frog Item 2",
                "Potion Shop Pot",
                "Stone Tower Temple Lava Room Item",
                "Stone Tower Temple Lava Room Item 2",
                "Stone Tower Temple Thin Bridge Item",
                "Stone Tower Temple Thin Bridge Item 2",
                "Stone Tower Temple Thin Bridge Item 3",
                "Stone Tower Temple Thin Bridge Item 4",
                "Stone Tower Temple Thin Bridge Item 5",
                "Stone Tower Temple Thin Bridge Item 6",
                "Stone Tower Temple Thin Bridge Item 7",
                "Stone Tower Temple Thin Bridge Item 8",
                "Inverted Stone Tower Temple Dexihand Item",
                "Inverted Stone Tower Temple Pre-Boss Closest Item",
                "Inverted Stone Tower Temple Pre-Boss 2nd Closest Item",
                "Inverted Stone Tower Temple Pre-Boss Item",
                "Inverted Stone Tower Temple Pre-Boss Item 2",
                "Inverted Stone Tower Temple Pre-Boss Item 3",
                "Inverted Stone Tower Temple Pre-Boss Furthest Item",
                "Inverted Stone Tower Temple Pre-Boss Furthest Item 2",
                "Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item",
                "Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item 2",
                "Inverted Stone Tower Temple Pre-Boss Closest Item 2",
                "Inverted Stone Tower Temple Pre-Boss 2nd Closest Item 2",
                "Swordsman's School Pot",
                "Swordsman's School Pot 2",
                "Swordsman's School Pot 3",
                "Swordsman's School Pot 4",
                "Swordsman's School Pot 5",
                "Woodfall Item",
                "Woodfall Temple Entrance Hive",
                "Woodfall Temple Gekko Room Pot",
                "Woodfall Temple Gekko Room Pot 2",
                "Woodfall Temple Gekko Room Pot 3",
                "Woodfall Temple Gekko Room Pot 4",
                "Woodfall Temple Pre-Boss Platform Item",
                "Woodfall Temple Pre-Boss Platform Item 2",
                "Woodfall Temple Pre-Boss Platform Item 3",
                "Woodfall Temple Pre-Boss Platform Item 4",
                "Well Left Path Pot",
                "Well Left Path Pot 2",
                "Well Left Path Pot 3",
                "Well Left Path Pot 4",
                "Well Left Path Pot 5",
                "Goron Village Small Snowball 3",
                "Goron Village Small Snowball 4",
                "Great Bay Coast Pot 6",
                "Great Bay Temple Red Valve Barrel 2",
                "Great Bay Temple Green Pump Barrel 2",
                "Ikana Canyon Grass 2",
                "Mountain Village Spring Snowball 2",
                "Mountain Village Winter Small Snowball 2",
                "Snowhead Small Snowball 2",
                "Stone Tower Owl Pot 2",
                "Inverted Stone Tower Pot",
                "Zora Cape Owl Pot 2",
                "Ikana Castle Left Staircase Pot",
                "Goron Village Small Snowball 5",
                "Goron Village Small Snowball 6",
                "Pirates' Fortress Sewer Exit Pot 2",
                "Woodfall Pot",
                "Goron Shrine Pot",
                "Goron Shrine Pot 2",
                "Goron Shrine Pot 3",
                "Goron Shrine Pot 4",
                "Goron Shrine Pot 5",
                "Goron Village Small Snowball 7",
                "Goron Village Small Snowball 8",
                "Cleared Swamp Owl Grass",
                "Southern Swamp Owl Grass",
                "Woodfall Pot 2",
                "Dampe's Basement Pot 5",
                "Dampe's Basement Pot 6",
                "Dampe's Basement Pot 7",
                "Deku Palace Item 6",
                "Deku Palace Item 7",
                "Deku Palace Item 8",
                "Deku Palace Item 9",
                "Deku Palace Item 10",
                "Deku Palace Item 11",
                "Deku Palace Item 12",
                "Deku Palace Item 13",
                "Deku Palace Item 14",
                "Deku Palace Out of Bounds Item",
                "Deku Palace Item 15",
                "Deku Palace Item 16",
                "Deku Palace Item 17",
                "Butler Race Pillar Item",
                "Butler Race Pillar Item 2",
                "Butler Race Pillar Item 3",
                "Butler Race Pillar Item 4",
                "Butler Race Pillar Item 5",
                "Butler Race Pillar Item 6",
                "Butler Race River Item",
                "Butler Race River Item 2",
                "Butler Race River Item 3",
                "Butler Race River Item 4",
                "Butler Race River Item 5",
                "Butler Race River Item 6",
                "Butler Race Right Path Item",
                "Butler Race Right Path Item 2",
                "Butler Race Right Path Item 3",
                "Butler Race Right Path Item 4",
                "Butler Race Right Path Item 5",
                "Butler Race Right Path Item 6",
                "Butler Race Final Room Item",
                "Butler Race Final Room Item 2",
                "Butler Race Final Room Item 3",
                "Butler Race Final Room Item 4",
                "Butler Race Final Room Item 5",
                "Butler Race Final Room Item 6",
                "Butler Race Final Room Item 7",
                "Butler Race Final Room Item 8",
                "Butler Race Final Room Item 9",
                "Butler Race Final Room Item 10",
                "Butler Race Dual Pot",
                "East Clock Town Crate",
                "Great Bay Temple Water Control Room Item 3",
                "Great Bay Temple Water Control Room Item 4",
                "Ikana Graveyard Grass 2",
                "Potion Shop Item",
                "Pirates' Fortress Cage Room Barrel 2",
                "Pirates' Fortress Cage Room Barrel 3",
                "Pirates' Fortress Cage Room Barrel 4",
                "Pirates' Fortress Cage Room Barrel 5",
                "Secret Shrine Floating Item",
                "Secret Shrine Floating Item 2",
                "Secret Shrine Floating Item 3",
                "Secret Shrine Floating Item 4",
                "Secret Shrine Floating Item 5",
                "Secret Shrine Floating Item 6",
                "Secret Shrine Floating Item 7",
                "Secret Shrine Floating Item 8",
                "Secret Shrine Floating Item 9",
                "Secret Shrine Floating Item 10",
                "Secret Shrine Floating Item 11",
                "Secret Shrine Floating Item 12",
                "Secret Shrine Floating Item 13",
                "Secret Shrine Floating Item 14",
                "Secret Shrine Floating Item 15",
                "Secret Shrine Floating Item 16",
                "Secret Shrine Floating Item 17",
                "Cleared Swamp Potion Shop Pot 2",
                "Potion Shop Pot 2",
                "Stone Tower Temple Lava Room Item 3",
                "Stone Tower Temple Lava Room Item 4",
                "Stone Tower Temple Lava Room Item 5",
                "Inverted Stone Tower Temple Dexihand Item 2",
                "Clock Tower Rooftop Pot",
                "Clock Tower Rooftop Pot 2",
                "Clock Tower Rooftop Pot 3",
                "Clock Tower Rooftop Pot 4",
                "Goron Racetrack Pot",
                "Goron Racetrack Pot 2",
                "Goron Racetrack Pot 3",
                "Goron Racetrack Pot 4",
                "Goron Racetrack Pot 5",
                "Goron Racetrack Pot 6",
                "Goron Racetrack Pot 7",
                "Goron Racetrack Pot 8",
                "Goron Racetrack Pot 9",
                "Goron Racetrack Pot 10",
                "Goron Racetrack Pot 11",
                "Goron Racetrack Pot 12",
                "Goron Racetrack Pot 13",
                "Goron Racetrack Pot 14",
                "Goron Racetrack Pot 15",
                "Goron Racetrack Pot 16",
                "Goron Racetrack Pot 17",
                "Goron Racetrack Pot 18",
                "Goron Racetrack Pot 19",
                "Goron Racetrack Pot 20",
                "Goron Racetrack Pot 21",
                "Goron Racetrack Pot 22",
                "Goron Racetrack Pot 23",
                "Goron Racetrack Pot 24",
                "Goron Racetrack Pot 25",
                "Goron Racetrack Pot 26",
                "Goron Racetrack Pot 27",
                "Goron Shrine Pot 6",
                "Goron Shrine Pot 7",
                "Goron Shrine Pot 8",
                "Goron Shrine Pot 9",
                "Great Bay Coast Pot 7",
                "Great Bay Temple Red Valve Crate",
                "Ikana King Pot 3",
                "Ikana Canyon Grass 3",
                "Milk Road Grass 2",
                "Mountain Village Spring Snowball 3",
                "Goron Graveyard Snowball",
                "Goron Graveyard Snowball 2",
                "Mountain Village Winter Small Snowball 3",
                "Snowhead Small Snowball 3",
                "Stone Tower Owl Pot 3",
                "Inverted Stone Tower Pot 2",
                "Link Trial Pot",
                "Link Trial Pot 2",
                "Link Trial Pot 3",
                "Link Trial Pot 4",
                "Zora Cape Owl Pot 3",
                "Dampe's Basement Pot 8",
                "Pirates' Fortress Item",
                "Pirates' Fortress Item 2",
                "Pirates' Fortress Item 3",
                "Butler Race Pillar Item 7",
                "Butler Race Pillar Item 8",
                "Great Bay Temple Water Control Room Item 5",
                "Great Bay Temple Dexihand Item",
                "Great Bay Temple Dexihand Item 2",
                "Great Bay Temple Green Pump Item",
                "Great Bay Temple Green Pump Item 2",
                "Laundry Pool Item",
                "Laundry Pool Item 2",
                "Laundry Pool Item 3",
                "Mountain Village Spring Stair Item",
                "Snowhead Temple Icicle Room Frozen Item",
                "Snowhead Temple Icicle Room Frozen Item 2",
                "Snowhead Temple Icicle Room Frozen Item 3",
                "Swamp Near Frog Hive",
                "Stone Tower Temple Lava Room Item 6",
                "Stone Tower Temple Eyegore Room Item",
                "Stone Tower Temple Mirror Room Crate",
                "Stone Tower Temple Mirror Room Crate 2",
                "Stone Tower Temple Eyegore Room Item 2",
                "Inverted Stone Tower Temple Dexihand Item 3",
                "Inverted Stone Tower Temple Updraft Room Item",
                "Inverted Stone Tower Temple Updraft Room Item 2",
                "Termina Field Pillar Item",
                "Woodfall Temple Pre-Boss Left Pillar Item",
                "Woodfall Temple Pre-Boss Right Pillar Item",
                "Ikana Castle Courtyard Grass 3",
                "Ikana Castle Courtyard Grass 4",
                "Ikana Castle Fire Ceiling Room Pot",
                "Ikana Castle Hole Room Pot",
                "Ikana Castle Hole Room Pot 2",
                "Observatory Balloon Pot",
                "Observatory Balloon Pot 2",
                "Observatory Scarecrow Pot 3",
                "Night 2 Grave Pot 2",
                "Deku Palace Pot",
                "Deku Palace Pot 2",
                "Goron Racetrack Pot 28",
                "Goron Racetrack Pot 29",
                "Goron Racetrack Pot 30",
                "Goron Shrine Pot 10",
                "Goron Shrine Pot 11",
                "Goron Village Large Snowball 4",
                "Goron Village Large Snowball 5",
                "Goron Village Large Snowball 6",
                "Goron Village Small Snowball 9",
                "Goron Village Small Snowball 10",
                "Ikana King Entry Pot 3",
                "Ikana Graveyard Grass 3",
                "Mountain Village Winter Small Snowball 4",
                "Mountain Village Winter Small Snowball 5",
                "Mountain Village Day 1 Snowball",
                "Mountain Village Day 2 Snowball 2",
                "Oceanside Spider House Main Room Pot",
                "Oceanside Spider House Entrance Pot 3",
                "Oceanside Spider House Main Room Pot 2",
                "Oceanside Spider House Storage Room Pot",
                "Twin Islands Day 3 Snowball",
                "Twin Islands Day 3 Snowball 2",
                "Twin Islands Day 3 Snowball 3",
                "Twin Islands Day 3 Snowball 4",
                "Twin Islands Day 3 Snowball 5",
                "Twin Islands Day 2 Snowball",
                "Twin Islands Day 2 Snowball 2",
                "Twin Islands Day 2 Snowball 3",
                "Twin Islands Day 2 Snowball 4",
                "Twin Islands Day 1 Snowball",
                "Twin Islands Day 1 Snowball 2",
                "Twin Islands Day 1 Snowball 3",
                "Twin Islands Day 1 Snowball 4",
                "Twin Islands Day 1 Snowball 5",
                "Twin Islands Small Snowball",
                "Twin Islands Small Snowball 2",
                "Twin Islands Ramp Snowball",
                "Path to Mountain Village Small Snowball",
                "Path to Snowhead Large Snowball",
                "Path to Snowhead Large Snowball 2",
                "Path to Snowhead Large Snowball 3",
                "Path to Snowhead Large Snowball 4",
                "Pinnacle Rock Pot",
                "Pinnacle Rock Pot 2",
                "Pinnacle Rock Pot 3",
                "Pinnacle Rock Pot 4",
                "Secret Shrine Underwater Pot 3",
                "Secret Shrine Underwater Pot 4",
                "Snowhead Large Snowball",
                "Snowhead Large Snowball 2",
                "Snowhead Large Snowball 3",
                "Snowhead Large Snowball 4",
                "Snowhead Large Snowball 5",
                "Snowhead Large Snowball 6",
                "Stone Tower Lower Scarecrow Pot",
                "Stone Tower Lower Scarecrow Pot 2",
                "Stone Tower Upper Scarecrow Pot 3",
                "Stone Tower Upper Scarecrow Pot 4",
                "Stone Tower Lower Scarecrow Pot 3",
                "Zora Cape Waterfall Pot",
                "Ranch Fence Item",
                "Ranch Fence Item 2",
                "Ranch Fence Item 3",
                "Ranch Fence Item 4",
                "Ranch Fence Item 5",
                "Ranch Fence Item 6",
                "Termina Field Above Cow Grotto Invisible Item",
                "Termina Field Invisible Item 2",
                "Termina Field Invisible Item 3",
                "Termina Field Invisible Item 4",
                "Termina Field Invisible Item 5",
                "Termina Field Invisible Item 6",
                "Termina Field Invisible Item 7",
                "Termina Field Invisible Item 8",
                "Termina Field Northern Ramp Invisible Item",
                "Termina Field Invisible Item 10",
                "Termina Field Invisible Item 11",
                "Swamp Spider House Invisible Item",
                "Swamp Spider House Invisible Item 2",
                "Swamp Spider House Invisible Item 3",
                "Swamp Spider House Invisible Item 4",
                "Swamp Spider House Invisible Item 5",
                "Termina Field Tree Item",
                "Termina Field Pillar Spawned Item",
                "Termina Field Telescope Guay",
                "Swordsman School Gong",
                "Bean Grotto Soft Soil",
                "Deku Palace Soft Soil",
                "Doggy Racetrack Soft Soil",
                "Great Bay Coast Soft Soil",
                "Ranch Day 1 Soil",
                "Ranch Day 2 or 3 Soil",
                "Secret Shrine Soft Soil",
                "Stone Tower Soft Soil Lower",
                "Stone Tower Soft Soil Upper",
                "Swamp Spider House Rock Soft Soil",
                "Swamp Spider House Gold Room Soft Soil",
                "Termina Field Stump Soft Soil",
                "Termina Field Observatory Soft Soil",
                "Termina Field South Wall Soft Soil",
                "Termina Field Pillar Soft Soil",
                "Termina Field Guay #1",
                "Termina Field Guay #2",
                "Termina Field Guay #3",
                "Termina Field Guay #4",
                "Termina Field Guay #5",
                "Termina Field Guay #6",
                "Termina Field Guay #7",
                "Termina Field Guay #8",
                "Termina Field Guay #9",
                "Termina Field Guay #10",
                "Termina Field Guay #11",
                "Termina Field Guay #12",
                "Termina Field Guay #13",
                "Termina Field Guay #14",
                "Termina Field Guay #15",
                "Termina Field Guay #16",
                "Termina Field Guay #17",
                "Termina Field Guay #18",
                "Termina Field Guay #19",
                "Termina Field Guay #20",
                "Termina Field Guay #5a",
                "Termina Field Guay #10a",
                "Termina Field Guay #15a",
                "Deku Palace Rupee Cluster #1",
                "Deku Palace Rupee Cluster #2",
                "Deku Palace Rupee Cluster #3",
                "Deku Palace Rupee Cluster #4",
                "Deku Palace Rupee Cluster #5",
                "Deku Palace Rupee Cluster #6",
                "Deku Palace Rupee Cluster #7",
                "Ikana Graveyard Rupee Cluster",
                "Ikana Graveyard Rupee Cluster 2",
                "Ikana Graveyard Rupee Cluster 3",
                "Ikana Graveyard Rupee Cluster 4",
                "Ikana Graveyard Rupee Cluster 5",
                "Ikana Graveyard Rupee Cluster 6",
                "Ikana Graveyard Rupee Cluster 7",
                "Termina Field Song Wall Dawn",
                "Termina Field Song Wall Dawn 2",
                "Termina Field Song Wall Dawn 3",
                "Termina Field Song Wall 0 / 8 / 12 / 16",
                "Termina Field Song Wall 0 / 8 / 12 / 16 2",
                "Termina Field Song Wall 0 / 8 / 12 / 16 3",
                "Termina Field Song Wall 2 / 10 / 14 / 18 / 22",
                "Termina Field Song Wall 2 / 10 / 14 / 18 / 22 2",
                "Termina Field Song Wall 2 / 10 / 14 / 18 / 22 3",
                "Termina Field Song Wall 4 / 20",
                "Termina Field Song Wall 4 / 20 2",
                "Termina Field Song Wall 4 / 20 3",
                "Termina Field Song Wall Odd Hours",
                "Termina Field Song Wall Odd Hours 2",
                "Termina Field Song Wall Odd Hours 3",
                "Deku Playground Day 2 Item",
                "Deku Playground Day 2 Item 2",
                "Deku Playground Day 2 Item 3",
                "Deku Playground Day 2 Item 4",
                "Deku Playground Day 2 Item 5",
                "Deku Playground Day 2 Item 6",
                "Deku Playground Day 1 Item",
                "Deku Playground Day 1 Item 2",
                "Deku Playground Day 1 Item 3",
                "Deku Playground Day 1 Item 4",
                "Deku Playground Day 1 Item 5",
                "Deku Playground Day 1 Item 6",
                "Deku Playground Day 3 Item",
                "Deku Playground Day 3 Item 2",
                "Deku Playground Day 3 Item 3",
                "Deku Playground Day 3 Item 4",
                "Deku Playground Day 3 Item 5",
                "Deku Playground Day 3 Item 6",
                "Pirates' Fortress Skull Flag Left Eye",
                "Pirates' Fortress Skull Flag Left Eye 2",
                "Pirates' Fortress Skull Flag Left Eye 3",
                "Pirates' Fortress Skull Flag Right Eye",
                "Pirates' Fortress Skull Flag Right Eye 2",
                "Pirates' Fortress Skull Flag Right Eye 3",
                "Hookshot Room Skull Flag Forehead",
                "Hookshot Room Skull Flag Forehead 2",
                "Hookshot Room Skull Flag Forehead 3",
                "Swamp Spider House Blue Gem",
                "Swamp Spider House Blue Gem 2",
                "Swamp Spider House Blue Gem 3",
                "Swamp Spider House Blue Gem 4",
                "Swamp Spider House Blue Gem 5",
                "Swamp Spider House Blue Gem 6",
                "Swamp Spider House Blue Gem 7",
                "Swamp Spider House Blue Gem 8",
                "Swamp Spider House Blue Gem 9",
                "Swamp Spider House Blue Gem 10",
                "Swamp Spider House Blue Gem 11",
                "Swamp Spider House Blue Gem 12",
                "Oceanside Spider House Mask",
                "Oceanside Spider House Mask 2",
                "Oceanside Spider House Mask 3",
                "Oceanside Spider House Mask 4",
                "Oceanside Spider House Mask 5",
                "Oceanside Spider House Mask 6",
                "Oceanside Spider House Mask 7",
                "Oceanside Spider House Mask 8",
                "Oceanside Spider House Mask 9",
                "Termina Field Clam",
                "Termina Field Clam 2",
                "Termina Field Clam 3",
                "Termina Field Wall",
                "Termina Field Wall 2",
                "Termina Field Wall 3",
                "Termina Field Skull Kid Drawing",
                "Termina Field Skull Kid Drawing 2",
                "Termina Field Skull Kid Drawing 3",
                "Cucco Shack Diamond Hole",
                "Cucco Shack Diamond Hole 2",
                "Cucco Shack Diamond Hole 3",
                "Cucco Shack Diamond Hole 4",
                "Cucco Shack Diamond Hole 5",
                "Cucco Shack Diamond Hole 6",
                "Ikana Graveyard Lantern",
                "Ikana Graveyard Lantern 2",
                "Ikana Graveyard Lantern 3",
                "Ikana Graveyard Lantern 4",
                "Ikana Graveyard Lantern 5",
                "Ikana Graveyard Lantern 6",
                "Ikana Graveyard Lantern 7",
                "Ikana Graveyard Lantern 8",
                "Ikana Graveyard Lantern 9",
                "Ikana Graveyard Lantern 10",
                "Ikana Graveyard Lantern 11",
                "Ikana Graveyard Lantern 12",
                "Stock Pot Inn Mask",
                "Stock Pot Inn Mask 2",
                "Stock Pot Inn Mask 3",
                "East Clock Town Target",
                "East Clock Town Target 2",
                "East Clock Town Target 3",
                "East Clock Town Target 4",
                "East Clock Town Target 5",
                "East Clock Town Target 6",
                "East Clock Town Basket",
                "East Clock Town Basket 2",
                "East Clock Town Basket 3",
                "Clock Tower Clock",
                "Clock Tower Clock 2",
                "Clock Tower Clock 3",
                "Takkuri",
                "Hookshot Room Pot",
                "Hookshot Room Pot 2",
                "Termina Field Rock",
                "Termina Field Rock 2",
                "Ikana Graveyard Highest Rock",
                "Ikana Graveyard Lowest Rock",
                "Ikana Graveyard 2nd Lowest Rock",
                "Termina Field Rock 3",
                "Termina Field Rock 4",
                "Termina Field Rock 5",
                "Termina Field Rock 6",
                "Termina Field Rock 7",
                "Ikana Graveyard 2nd Highest Rock",
                "Ikana Graveyard Middle Rock",
                "Termina Field Rock 8",
                "Termina Field Rock 9",
                "Milk Road Keaton Grass",
                "Milk Road Keaton Grass 2",
                "Milk Road Keaton Grass 3",
                "Milk Road Keaton Grass 4",
                "Milk Road Keaton Grass 5",
                "Milk Road Keaton Grass 6",
                "Milk Road Keaton Grass 7",
                "Milk Road Keaton Grass 8",
                "Milk Road Keaton Grass 9",
                "North Clock Town Keaton Grass",
                "North Clock Town Keaton Grass 2",
                "North Clock Town Keaton Grass 3",
                "North Clock Town Keaton Grass 4",
                "North Clock Town Keaton Grass 5",
                "North Clock Town Keaton Grass 6",
                "North Clock Town Keaton Grass 7",
                "North Clock Town Keaton Grass 8",
                "North Clock Town Keaton Grass 9",
                "Mountain Village Spring Keaton Grass",
                "Mountain Village Spring Keaton Grass 2",
                "Mountain Village Spring Keaton Grass 3",
                "Mountain Village Spring Keaton Grass 4",
                "Mountain Village Spring Keaton Grass 5",
                "Mountain Village Spring Keaton Grass 6",
                "Mountain Village Spring Keaton Grass 7",
                "Mountain Village Spring Keaton Grass 8",
                "Mountain Village Spring Keaton Grass 9",
                "Oceanside Spider House Mask Room Pot",
                "Oceanside Spider House Mask Room Pot 2",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = 433 + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= 433)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 6 + 1, $"- {itemNames[item.ID - 433]}");
                lines.Insert(item.ID * 6 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 6 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 6 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 6 + 5, "0");
                lines.Insert(item.ID * 6 + 6, "");
            }
        }

        private static void AddRupeesAndFixedDrops2(List<string> lines)
        {
            const int startIndex = 1056;
            lines[0] = "-version 17";
            var itemNames = new string[]
            {
                "Ikana Canyon Cleared Grass",
                "Ikana Canyon Cleared Grass 2",
                "Ikana Canyon Cleared Grass 3",
                "Path to Snowhead Spring Snowball",
                "Path to Snowhead Spring Snowball 2",
                "Path to Snowhead Spring Snowball 3",
                "Path to Snowhead Spring Snowball 4",
                "Path to Mountain Village Spring Snowball",
                "Path to Mountain Village Spring Snowball 2",
                "Path to Mountain Village Spring Snowball 3",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = startIndex + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= startIndex)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 6 + 1, $"- {itemNames[item.ID - startIndex]}");
                lines.Insert(item.ID * 6 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 6 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 6 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 6 + 5, "0");
                lines.Insert(item.ID * 6 + 6, "");
            }
        }

        private static void AddRupeesAndFixedDrops3(List<string> lines)
        {
            const int startIndex = 1066;
            lines[0] = "-version 18";
            var itemNames = new string[]
            {
                "Zora Cape Jar Game",
                "Ikana Graveyard Day 2 Bats",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = startIndex + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= startIndex)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 6 + 1, $"- {itemNames[item.ID - startIndex]}");
                lines.Insert(item.ID * 6 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 6 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 6 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 6 + 5, "0");
                lines.Insert(item.ID * 6 + 6, "");
            }
        }

        private static void AddRupeesAndFixedDrops4(List<string> lines)
        {
            const int startIndex = 1068;
            lines[0] = "-version 19";
            var itemNames = new string[]
            {
                "Cucco Shack Potted Plant",
            };
            var newItems = itemNames.Select((itemName, index) => new MigrationItem
            {
                ID = startIndex + index
            }).ToArray();
            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                if (line.StartsWith("-") || string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                {
                    continue;
                }
                var updatedItemSections = line
                    .Split(';')
                    .Select(section => section.Split(',').Select(id =>
                    {
                        var itemId = int.Parse(id);
                        if (itemId >= startIndex)
                        {
                            itemId += newItems.Length;
                        }
                        return itemId;
                    }).ToList()).ToList();
                lines[i] = string.Join(";", updatedItemSections.Select(section => string.Join(",", section)));
            }
            foreach (var item in newItems)
            {
                lines.Insert(item.ID * 6 + 1, $"- {itemNames[item.ID - startIndex]}");
                lines.Insert(item.ID * 6 + 2, string.Join(",", item.DependsOnItems));
                lines.Insert(item.ID * 6 + 3, string.Join(";", item.Conditionals.Select(c => string.Join(",", c))));
                lines.Insert(item.ID * 6 + 4, $"{item.TimeNeeded}");
                lines.Insert(item.ID * 6 + 5, "0");
                lines.Insert(item.ID * 6 + 6, "");
            }
        }

        private static JsonFormatLogic ConvertToJson(List<string> lines)
        {
            var itemNames = new List<string>
            {
                "MaskDeku",
                "ItemBow",
                "ItemFireArrow",
                "ItemIceArrow",
                "ItemLightArrow",
                "ItemBombBag",
                "ItemMagicBean",
                "ItemPowderKeg",
                "ItemPictobox",
                "ItemLens",
                "ItemHookshot",
                "FairyMagic",
                "FairySpinAttack",
                "FairyDoubleMagic",
                "FairyDoubleDefense",
                "ItemFairySword",
                "ItemBottleWitch",
                "ItemBottleAliens",
                "ItemBottleGoronRace",
                "ItemBottleBeavers",
                "ItemBottleDampe",
                "ItemBottleMadameAroma",
                "ItemNotebook",
                "UpgradeRazorSword",
                "UpgradeGildedSword",
                "UpgradeMirrorShield",
                "UpgradeBigQuiver",
                "UpgradeBiggestQuiver",
                "UpgradeBigBombBag",
                "UpgradeBiggestBombBag",
                "UpgradeAdultWallet",
                "UpgradeGiantWallet",
                "TradeItemMoonTear",
                "TradeItemLandDeed",
                "TradeItemSwampDeed",
                "TradeItemMountainDeed",
                "TradeItemOceanDeed",
                "TradeItemRoomKey",
                "TradeItemKafeiLetter",
                "TradeItemPendant",
                "TradeItemMamaLetter",
                "HeartPieceNotebookMayor",
                "HeartPieceNotebookPostman",
                "HeartPieceNotebookRosa",
                "HeartPieceNotebookHand",
                "HeartPieceNotebookGran1",
                "HeartPieceNotebookGran2",
                "HeartPieceKeatonQuiz",
                "HeartPieceDekuPlayground",
                "HeartPieceTownArchery",
                "HeartPieceHoneyAndDarling",
                "HeartPieceSwordsmanSchool",
                "HeartPiecePostBox",
                "HeartPieceTerminaGossipStones",
                "HeartPieceTerminaBusinessScrub",
                "HeartPieceSwampArchery",
                "HeartPiecePictobox",
                "HeartPieceBoatArchery",
                "HeartPieceChoir",
                "HeartPieceBeaverRace",
                "HeartPieceSeaHorse",
                "HeartPieceFishermanGame",
                "HeartPieceEvan",
                "HeartPieceDogRace",
                "HeartPiecePoeHut",
                "HeartPieceTreasureChestGame",
                "HeartPiecePeahat",
                "HeartPieceDodong",
                "HeartPieceWoodFallChest",
                "HeartPieceTwinIslandsChest",
                "HeartPieceOceanSpiderHouse",
                "HeartPieceKnuckle",
                "MaskPostmanHat",
                "MaskAllNight",
                "MaskBlast",
                "MaskStone",
                "MaskGreatFairy",
                "MaskKeaton",
                "MaskBremen",
                "MaskBunnyHood",
                "MaskDonGero",
                "MaskScents",
                "MaskRomani",
                "MaskCircusLeader",
                "MaskKafei",
                "MaskCouple",
                "MaskTruth",
                "MaskKamaro",
                "MaskGibdo",
                "MaskGaro",
                "MaskCaptainHat",
                "MaskGiant",
                "MaskGoron",
                "MaskZora",
                "SongHealing",
                "SongSoaring",
                "SongEpona",
                "SongStorms",
                "SongSonata",
                "SongLullaby",
                "SongNewWaveBossaNova",
                "SongElegy",
                "SongOath",
                "AreaSouthAccess",
                "AreaWoodFallTempleAccess",
                "AreaWoodFallTempleClear",
                "AreaNorthAccess",
                "AreaSnowheadTempleAccess",
                "AreaSnowheadTempleClear",
                "OtherEpona",
                "AreaWestAccess",
                "AreaPiratesFortressAccess",
                "AreaGreatBayTempleAccess",
                "AreaGreatBayTempleClear",
                "AreaEastAccess",
                "AreaIkanaCanyonAccess",
                "AreaStoneTowerTempleAccess",
                "AreaInvertedStoneTowerTempleAccess",
                "AreaStoneTowerClear",
                "OtherExplosive",
                "OtherArrow",
                "AreaWoodfallNew",
                "AreaSnowheadNew",
                "AreaGreatBayNew",
                "AreaLANew",
                "AreaInvertedStoneTowerNew",
                "ItemWoodfallMap",
                "ItemWoodfallCompass",
                "ItemWoodfallBossKey",
                "ItemWoodfallKey1",
                "ItemSnowheadMap",
                "ItemSnowheadCompass",
                "ItemSnowheadBossKey",
                "ItemSnowheadKey1",
                "ItemSnowheadKey2",
                "ItemSnowheadKey3",
                "ItemGreatBayMap",
                "ItemGreatBayCompass",
                "ItemGreatBayBossKey",
                "ItemGreatBayKey1",
                "ItemStoneTowerMap",
                "ItemStoneTowerCompass",
                "ItemStoneTowerBossKey",
                "ItemStoneTowerKey1",
                "ItemStoneTowerKey2",
                "ItemStoneTowerKey3",
                "ItemStoneTowerKey4",
                "ShopItemTradingPostRedPotion",
                "ShopItemTradingPostGreenPotion",
                "ShopItemTradingPostShield",
                "ShopItemTradingPostFairy",
                "ShopItemTradingPostStick",
                "ShopItemTradingPostArrow30",
                "ShopItemTradingPostNut10",
                "ShopItemTradingPostArrow50",
                "ShopItemWitchBluePotion",
                "ShopItemWitchRedPotion",
                "ShopItemWitchGreenPotion",
                "ShopItemBombsBomb10",
                "ShopItemBombsBombchu10",
                "ShopItemGoronBomb10",
                "ShopItemGoronArrow10",
                "ShopItemGoronRedPotion",
                "ShopItemZoraShield",
                "ShopItemZoraArrow10",
                "ShopItemZoraRedPotion",
                "BottleCatchFairy",
                "BottleCatchPrincess",
                "BottleCatchFish",
                "BottleCatchBug",
                "BottleCatchPoe",
                "BottleCatchBigPoe",
                "BottleCatchSpringWater",
                "BottleCatchHotSpringWater",
                "BottleCatchEgg",
                "BottleCatchMushroom",
                "ChestLensCaveRedRupee",
                "ChestLensCavePurpleRupee",
                "ChestBeanGrottoRedRupee",
                "ChestHotSpringGrottoRedRupee",
                "ChestBadBatsGrottoPurpleRupee",
                "ChestIkanaSecretShrineGrotto",
                "ChestPiratesFortressRedRupee1",
                "ChestPiratesFortressRedRupee2",
                "ChestInsidePiratesFortressTankRedRupee",
                "ChestInsidePiratesFortressGuardSilverRupee",
                "ChestInsidePiratesFortressHeartPieceRoomRedRupee",
                "ChestInsidePiratesFortressHeartPieceRoomBlueRupee",
                "ChestInsidePiratesFortressMazeRedRupee",
                "ChestPinacleRockRedRupee1",
                "ChestPinacleRockRedRupee2",
                "ChestBomberHideoutSilverRupee",
                "ChestTerminaGrottoBombchu",
                "ChestTerminaGrottoRedRupee",
                "ChestTerminaUnderwaterRedRupee",
                "ChestTerminaGrassRedRupee",
                "ChestTerminaStumpRedRupee",
                "ChestGreatBayCoastGrotto",
                "ChestGreatBayCapeLedge1",
                "ChestGreatBayCapeLedge2",
                "ChestGreatBayCapeGrotto",
                "ChestGreatBayCapeUnderwater",
                "ChestPiratesFortressEntranceRedRupee1",
                "ChestPiratesFortressEntranceRedRupee2",
                "ChestPiratesFortressEntranceRedRupee3",
                "ChestToSwampGrotto",
                "ChestDogRacePurpleRupee",
                "ChestGraveyardGrotto",
                "ChestSwampGrotto",
                "ChestWoodfallBlueRupee",
                "ChestWoodfallRedRupee",
                "ChestWellRightPurpleRupee",
                "ChestWellLeftPurpleRupee",
                "ChestMountainVillage",
                "ChestMountainVillageGrottoRedRupee",
                "ChestToIkanaRedRupee",
                "ChestToIkanaGrotto",
                "ChestInvertedStoneTowerSilverRupee",
                "ChestInvertedStoneTowerBombchu10",
                "ChestInvertedStoneTowerBean",
                "ChestToSnowheadGrotto",
                "ChestToGoronVillageRedRupee",
                "ChestSecretShrineHeartPiece",
                "ChestSecretShrineDinoGrotto",
                "ChestSecretShrineWizzGrotto",
                "ChestSecretShrineWartGrotto",
                "ChestSecretShrineGaroGrotto",
                "ChestInnStaffRoom",
                "ChestInnGuestRoom",
                "ChestWoodsGrotto",
                "ChestEastClockTownSilverRupee",
                "ChestSouthClockTownRedRupee",
                "ChestSouthClockTownPurpleRupee",
                "HeartPieceBank",
                "HeartPieceSouthClockTown",
                "HeartPieceNorthClockTown",
                "HeartPieceToSwamp",
                "HeartPieceSwampScrub",
                "HeartPieceDekuPalace",
                "HeartPieceGoronVillageScrub",
                "HeartPieceZoraGrotto",
                "HeartPieceLabFish",
                "HeartPieceGreatBayCapeLikeLike",
                "HeartPiecePiratesFortress",
                "HeartPieceZoraHallScrub",
                "HeartPieceToSnowhead",
                "HeartPieceGreatBayCoast",
                "HeartPieceIkana",
                "HeartPieceCastle",
                "HeartContainerWoodfall",
                "HeartContainerSnowhead",
                "HeartContainerGreatBay",
                "HeartContainerStoneTower",
                "ItemTingleMapTown",
                "ItemTingleMapWoodfall",
                "ItemTingleMapSnowhead",
                "ItemTingleMapRanch",
                "ItemTingleMapGreatBay",
                "ItemTingleMapStoneTower",
                "ChestToGoronRaceGrotto",
                "IkanaScrubGoldRupee",
                "OtherOneMask",
                "OtherTwoMasks",
                "OtherThreeMasks",
                "OtherFourMasks",
                "AreaMoonAccess",
                "HeartPieceDekuTrial",
                "HeartPieceGoronTrial",
                "HeartPieceZoraTrial",
                "HeartPieceLinkTrial",
                "MaskFierceDeity",
                "ChestLinkTrialArrow30",
                "ChestLinkTrialBombchu10",
                "ChestPreClocktownDekuNut",
                "StartingSword",
                "StartingShield",
                "StartingHeartContainer1",
                "StartingHeartContainer2",
                "ItemRanchBarnMainCowMilk",
                "ItemRanchBarnOtherCowMilk1",
                "ItemRanchBarnOtherCowMilk2",
                "ItemWellCowMilk",
                "ItemTerminaGrottoCowMilk1",
                "ItemTerminaGrottoCowMilk2",
                "ItemCoastGrottoCowMilk1",
                "ItemCoastGrottoCowMilk2",
                "CollectibleSwampSpiderToken1",
                "CollectibleSwampSpiderToken2",
                "CollectibleSwampSpiderToken3",
                "CollectibleSwampSpiderToken4",
                "CollectibleSwampSpiderToken5",
                "CollectibleSwampSpiderToken6",
                "CollectibleSwampSpiderToken7",
                "CollectibleSwampSpiderToken8",
                "CollectibleSwampSpiderToken9",
                "CollectibleSwampSpiderToken10",
                "CollectibleSwampSpiderToken11",
                "CollectibleSwampSpiderToken12",
                "CollectibleSwampSpiderToken13",
                "CollectibleSwampSpiderToken14",
                "CollectibleSwampSpiderToken15",
                "CollectibleSwampSpiderToken16",
                "CollectibleSwampSpiderToken17",
                "CollectibleSwampSpiderToken18",
                "CollectibleSwampSpiderToken19",
                "CollectibleSwampSpiderToken20",
                "CollectibleSwampSpiderToken21",
                "CollectibleSwampSpiderToken22",
                "CollectibleSwampSpiderToken23",
                "CollectibleSwampSpiderToken24",
                "CollectibleSwampSpiderToken25",
                "CollectibleSwampSpiderToken26",
                "CollectibleSwampSpiderToken27",
                "CollectibleSwampSpiderToken28",
                "CollectibleSwampSpiderToken29",
                "CollectibleSwampSpiderToken30",
                "CollectibleOceanSpiderToken1",
                "CollectibleOceanSpiderToken2",
                "CollectibleOceanSpiderToken3",
                "CollectibleOceanSpiderToken4",
                "CollectibleOceanSpiderToken5",
                "CollectibleOceanSpiderToken6",
                "CollectibleOceanSpiderToken7",
                "CollectibleOceanSpiderToken8",
                "CollectibleOceanSpiderToken9",
                "CollectibleOceanSpiderToken10",
                "CollectibleOceanSpiderToken11",
                "CollectibleOceanSpiderToken12",
                "CollectibleOceanSpiderToken13",
                "CollectibleOceanSpiderToken14",
                "CollectibleOceanSpiderToken15",
                "CollectibleOceanSpiderToken16",
                "CollectibleOceanSpiderToken17",
                "CollectibleOceanSpiderToken18",
                "CollectibleOceanSpiderToken19",
                "CollectibleOceanSpiderToken20",
                "CollectibleOceanSpiderToken21",
                "CollectibleOceanSpiderToken22",
                "CollectibleOceanSpiderToken23",
                "CollectibleOceanSpiderToken24",
                "CollectibleOceanSpiderToken25",
                "CollectibleOceanSpiderToken26",
                "CollectibleOceanSpiderToken27",
                "CollectibleOceanSpiderToken28",
                "CollectibleOceanSpiderToken29",
                "CollectibleOceanSpiderToken30",
                "CollectibleStrayFairyClockTown",
                "CollectibleStrayFairyWoodfall1",
                "CollectibleStrayFairyWoodfall2",
                "CollectibleStrayFairyWoodfall3",
                "CollectibleStrayFairyWoodfall4",
                "CollectibleStrayFairyWoodfall5",
                "CollectibleStrayFairyWoodfall6",
                "CollectibleStrayFairyWoodfall7",
                "CollectibleStrayFairyWoodfall8",
                "CollectibleStrayFairyWoodfall9",
                "CollectibleStrayFairyWoodfall10",
                "CollectibleStrayFairyWoodfall11",
                "CollectibleStrayFairyWoodfall12",
                "CollectibleStrayFairyWoodfall13",
                "CollectibleStrayFairyWoodfall14",
                "CollectibleStrayFairyWoodfall15",
                "CollectibleStrayFairySnowhead1",
                "CollectibleStrayFairySnowhead2",
                "CollectibleStrayFairySnowhead3",
                "CollectibleStrayFairySnowhead4",
                "CollectibleStrayFairySnowhead5",
                "CollectibleStrayFairySnowhead6",
                "CollectibleStrayFairySnowhead7",
                "CollectibleStrayFairySnowhead8",
                "CollectibleStrayFairySnowhead9",
                "CollectibleStrayFairySnowhead10",
                "CollectibleStrayFairySnowhead11",
                "CollectibleStrayFairySnowhead12",
                "CollectibleStrayFairySnowhead13",
                "CollectibleStrayFairySnowhead14",
                "CollectibleStrayFairySnowhead15",
                "CollectibleStrayFairyGreatBay1",
                "CollectibleStrayFairyGreatBay2",
                "CollectibleStrayFairyGreatBay3",
                "CollectibleStrayFairyGreatBay4",
                "CollectibleStrayFairyGreatBay5",
                "CollectibleStrayFairyGreatBay6",
                "CollectibleStrayFairyGreatBay7",
                "CollectibleStrayFairyGreatBay8",
                "CollectibleStrayFairyGreatBay9",
                "CollectibleStrayFairyGreatBay10",
                "CollectibleStrayFairyGreatBay11",
                "CollectibleStrayFairyGreatBay12",
                "CollectibleStrayFairyGreatBay13",
                "CollectibleStrayFairyGreatBay14",
                "CollectibleStrayFairyGreatBay15",
                "CollectibleStrayFairyStoneTower1",
                "CollectibleStrayFairyStoneTower2",
                "CollectibleStrayFairyStoneTower3",
                "CollectibleStrayFairyStoneTower4",
                "CollectibleStrayFairyStoneTower5",
                "CollectibleStrayFairyStoneTower6",
                "CollectibleStrayFairyStoneTower7",
                "CollectibleStrayFairyStoneTower8",
                "CollectibleStrayFairyStoneTower9",
                "CollectibleStrayFairyStoneTower10",
                "CollectibleStrayFairyStoneTower11",
                "CollectibleStrayFairyStoneTower12",
                "CollectibleStrayFairyStoneTower13",
                "CollectibleStrayFairyStoneTower14",
                "CollectibleStrayFairyStoneTower15",
                "MundaneItemLotteryPurpleRupee",
                "MundaneItemBankBlueRupee",
                "ShopItemMilkBarChateau",
                "ShopItemMilkBarMilk",
                "MundaneItemDekuPlaygroundPurpleRupee",
                "MundaneItemHoneyAndDarlingPurpleRupee",
                "MundaneItemKotakeMushroomSaleRedRupee",
                "MundaneItemPictographContestBlueRupee",
                "MundaneItemPictographContestRedRupee",
                "ShopItemBusinessScrubMagicBean",
                "ShopItemBusinessScrubGreenPotion",
                "ShopItemBusinessScrubBluePotion",
                "MundaneItemZoraStageLightsBlueRupee",
                "ShopItemGormanBrosMilk",
                "MundaneItemOceanSpiderHouseDay2PurpleRupee",
                "MundaneItemOceanSpiderHouseDay3RedRupee",
                "MundaneItemLuluBadPictographBlueRupee",
                "MundaneItemLuluGoodPictographRedRupee",
                "MundaneItemTreasureChestGamePurpleRupee",
                "MundaneItemTreasureChestGameRedRupee",
                "MundaneItemTreasureChestGameDekuNuts",
                "MundaneItemCuriosityShopBlueRupee",
                "MundaneItemCuriosityShopRedRupee",
                "MundaneItemCuriosityShopPurpleRupee",
                "MundaneItemCuriosityShopGoldRupee",
                "MundaneItemSeahorse",
                "CollectableAncientCastleOfIkanaCastleExteriorGrass1",
                "CollectableAncientCastleOfIkanaCastleExteriorGrass2",
                "CollectableBeneathTheGraveyardMainAreaPot1",
                "CollectableBeneathTheGraveyardInvisibleRoomPot1",
                "CollectableBeneathTheGraveyardBadBatRoomPot1",
                "CollectableCuccoShackWoodenCrateLarge1",
                "CollectableDampesHouseBasementPot1",
                "CollectableDampesHouseBasementPot2",
                "CollectableDampesHouseBasementPot3",
                "CollectableDampesHouseBasementPot4",
                "CollectableGoronVillageWinterSmallSnowball1",
                "CollectableGoronVillageWinterSmallSnowball2",
                "CollectableGreatBayCoastPot1",
                "CollectableGreatBayCoastPot2",
                "CollectableGreatBayCoastPot3",
                "CollectableGreatBayCoastPot4",
                "CollectableGreatBayTempleBlueChuchuValveRoomBarrel1",
                "CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot1",
                "CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot2",
                "CollectableIgosDuIkanaSLairPreBossRoomPot1",
                "CollectableIgosDuIkanaSLairPreBossRoomPot2",
                "CollectableIkanaGraveyardIkanaGraveyardLowerGrass1",
                "CollectableOceansideSpiderHouseEntrancePot1",
                "CollectableOceansideSpiderHouseEntrancePot2",
                "CollectablePiratesFortressInteriorWaterCurrentRoomPot1",
                "CollectablePiratesFortressInterior100RupeeEggRoomPot1",
                "CollectablePiratesFortressInteriorBarrelRoomEggPot1",
                "CollectablePiratesFortressInteriorTelescopeRoomPot1",
                "CollectableSecretShrineMainRoomPot1",
                "CollectableSecretShrineMainRoomPot2",
                "CollectableSnowheadTempleIceBlockRoomSmallSnowball1",
                "CollectableSnowheadTempleIceBlockRoomSmallSnowball2",
                "CollectableStoneTowerPot1",
                "CollectableStoneTowerPot2",
                "CollectableGreatBayCoastPot5",
                "CollectableGreatBayTempleSeesawRoomPot1",
                "CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel1",
                "CollectableIkanaCanyonMainAreaGrass1",
                "CollectableMilkRoadGrass1",
                "CollectableMountainVillageSpringSmallSnowball1",
                "CollectableMountainVillageWinterSmallSnowball1",
                "CollectablePiratesFortressInteriorTwinBarrelEggRoomPot1",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartPot1",
                "CollectableRomaniRanchWoodenCrateLarge1",
                "CollectableSnowheadSmallSnowball1",
                "CollectableStoneTowerPot3",
                "CollectableZoraCapePot1",
                "CollectableAstralObservatoryObservatoryBombersHideoutPot1",
                "CollectableAstralObservatoryObservatoryBombersHideoutPot2",
                "CollectableDekuPalaceWestInnerGardenItem1",
                "CollectableDekuPalaceEastInnerGardenItem1",
                "CollectableDekuPalaceEastInnerGardenItem2",
                "CollectableDekuPalaceWestInnerGardenItem2",
                "CollectableDekuPalaceWestInnerGardenItem3",
                "CollectableDoggyRacetrackPot1",
                "CollectableDoggyRacetrackPot2",
                "CollectableDoggyRacetrackPot3",
                "CollectableDoggyRacetrackPot4",
                "CollectableGoronVillageWinterLargeSnowball1",
                "CollectableGoronVillageWinterLargeSnowball2",
                "CollectableGoronVillageWinterLargeSnowball3",
                "CollectableGreatBayCoastPot6",
                "CollectableGreatBayCoastPot7",
                "CollectableGreatBayCoastPot8",
                "CollectableGreatBayTempleWaterControlRoomItem1",
                "CollectableGreatBayTempleWaterControlRoomItem2",
                "CollectableGrottosOceanHeartPieceGrottoBeehive1",
                "CollectableLaundryPoolWoodenCrateSmall1",
                "CollectableMountainVillageWinterLargeSnowball1",
                "CollectableMountainVillageWinterLargeSnowball2",
                "CollectablePathToGoronVillageWinterItem1",
                "CollectablePathToGoronVillageWinterItem2",
                "CollectablePathToGoronVillageWinterItem3",
                "CollectablePathToGoronVillageWinterItem4",
                "CollectablePiratesFortressInteriorBarrelRoomEggPot2",
                "CollectablePiratesFortressInteriorTelescopeRoomItem1",
                "CollectablePiratesFortressInteriorTelescopeRoomItem2",
                "CollectablePiratesFortressInteriorTelescopeRoomItem3",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem1",
                "CollectableRanchHouseBarnBarnItem1",
                "CollectableRanchHouseBarnBarnItem2",
                "CollectableSnowheadTempleIceBlockRoomSmallSnowball3",
                "CollectableSnowheadTempleIceBlockRoomSmallSnowball4",
                "CollectableSnowheadTempleIceBlockRoomSmallSnowball5",
                "CollectableSnowheadTempleMapRoomWoodenCrateLarge1",
                "CollectableSnowheadTempleMapRoomWoodenCrateLarge2",
                "CollectableSnowheadTempleMapRoomWoodenCrateLarge3",
                "CollectableSnowheadTempleMapRoomWoodenCrateLarge4",
                "CollectableSnowheadTempleMapRoomWoodenCrateLarge5",
                "CollectableSnowheadTempleMainRoomPot1",
                "CollectableSnowheadTempleMainRoomPot2",
                "CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot1",
                "CollectableSouthernSwampPoisonedCentralSwampItem1",
                "CollectableSouthernSwampPoisonedCentralSwampItem2",
                "CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot1",
                "CollectableStoneTowerTempleLavaRoomItem1",
                "CollectableStoneTowerTempleLavaRoomItem2",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem1",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem2",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem3",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem4",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem5",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem6",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem7",
                "CollectableStoneTowerTempleRoomAfterLightArrowsItem8",
                "CollectableStoneTowerTempleInvertedEyegoreRoomItem1",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem1",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem2",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem3",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem4",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem5",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem6",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem7",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem8",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem9",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem10",
                "CollectableStoneTowerTempleInvertedPreBossRoomItem11",
                "CollectableSwordsmanSSchoolPot1",
                "CollectableSwordsmanSSchoolPot2",
                "CollectableSwordsmanSSchoolPot3",
                "CollectableSwordsmanSSchoolPot4",
                "CollectableSwordsmanSSchoolPot5",
                "CollectableWoodfallItem1",
                "CollectableWoodfallTempleEntranceRoomBeehive1",
                "CollectableWoodfallTempleGekkoRoomPot1",
                "CollectableWoodfallTempleGekkoRoomPot2",
                "CollectableWoodfallTempleGekkoRoomPot3",
                "CollectableWoodfallTempleGekkoRoomPot4",
                "CollectableWoodfallTemplePreBossRoomItem1",
                "CollectableWoodfallTemplePreBossRoomItem2",
                "CollectableWoodfallTemplePreBossRoomItem3",
                "CollectableWoodfallTemplePreBossRoomItem4",
                "CollectableBeneathTheWellBugAndBombRoomPot1",
                "CollectableBeneathTheWellBugAndBombRoomPot2",
                "CollectableBeneathTheWellBugAndBombRoomPot3",
                "CollectableBeneathTheWellBugAndBombRoomPot4",
                "CollectableBeneathTheWellBugAndBombRoomPot5",
                "CollectableGoronVillageWinterSmallSnowball3",
                "CollectableGoronVillageWinterSmallSnowball4",
                "CollectableGreatBayCoastPot9",
                "CollectableGreatBayTempleBlueChuchuValveRoomBarrel2",
                "CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel2",
                "CollectableIkanaCanyonMainAreaGrass2",
                "CollectableMountainVillageSpringSmallSnowball2",
                "CollectableMountainVillageWinterSmallSnowball2",
                "CollectableSnowheadSmallSnowball2",
                "CollectableStoneTowerPot4",
                "CollectableStoneTowerInvertedStoneTowerFlippedPot1",
                "CollectableZoraCapePot2",
                "CollectableAncientCastleOfIkana1FWestStaircasePot1",
                "CollectableGoronVillageWinterSmallSnowball5",
                "CollectableGoronVillageWinterSmallSnowball6",
                "CollectablePiratesFortressInteriorTelescopeRoomPot2",
                "CollectableWoodfallPot1",
                "CollectableGoronShrineGoronKidSRoomPot1",
                "CollectableGoronShrineGoronKidSRoomPot2",
                "CollectableGoronShrineMainRoomPot1",
                "CollectableGoronShrineMainRoomPot2",
                "CollectableGoronShrineMainRoomPot3",
                "CollectableGoronVillageWinterSmallSnowball7",
                "CollectableGoronVillageWinterSmallSnowball8",
                "CollectableSouthernSwampClearCentralSwampGrass1",
                "CollectableSouthernSwampPoisonedCentralSwampGrass1",
                "CollectableWoodfallPot2",
                "CollectableDampesHouseBasementPot5",
                "CollectableDampesHouseBasementPot6",
                "CollectableDampesHouseBasementPot7",
                "CollectableDekuPalaceEastInnerGardenItem3",
                "CollectableDekuPalaceEastInnerGardenItem4",
                "CollectableDekuPalaceEastInnerGardenItem5",
                "CollectableDekuPalaceEastInnerGardenItem6",
                "CollectableDekuPalaceEastInnerGardenItem7",
                "CollectableDekuPalaceEastInnerGardenItem8",
                "CollectableDekuPalaceWestInnerGardenItem4",
                "CollectableDekuPalaceWestInnerGardenItem5",
                "CollectableDekuPalaceWestInnerGardenItem6",
                "CollectableDekuPalaceWestInnerGardenItem7",
                "CollectableDekuPalaceWestInnerGardenItem8",
                "CollectableDekuPalaceWestInnerGardenItem9",
                "CollectableDekuPalaceWestInnerGardenItem10",
                "CollectableDekuShrineGiantRoomFloor1Item1",
                "CollectableDekuShrineGiantRoomFloor1Item2",
                "CollectableDekuShrineGiantRoomFloor1Item3",
                "CollectableDekuShrineGiantRoomFloor1Item4",
                "CollectableDekuShrineGiantRoomFloor1Item5",
                "CollectableDekuShrineGiantRoomFloor1Item6",
                "CollectableDekuShrineWaterRoomWithPlatformsItem1",
                "CollectableDekuShrineWaterRoomWithPlatformsItem2",
                "CollectableDekuShrineWaterRoomWithPlatformsItem3",
                "CollectableDekuShrineWaterRoomWithPlatformsItem4",
                "CollectableDekuShrineWaterRoomWithPlatformsItem5",
                "CollectableDekuShrineWaterRoomWithPlatformsItem6",
                "CollectableDekuShrineRoomBeforeFlameWallsItem1",
                "CollectableDekuShrineRoomBeforeFlameWallsItem2",
                "CollectableDekuShrineRoomBeforeFlameWallsItem3",
                "CollectableDekuShrineRoomBeforeFlameWallsItem4",
                "CollectableDekuShrineRoomBeforeFlameWallsItem5",
                "CollectableDekuShrineRoomBeforeFlameWallsItem6",
                "CollectableDekuShrineDekuButlerSRoomItem1",
                "CollectableDekuShrineDekuButlerSRoomItem2",
                "CollectableDekuShrineDekuButlerSRoomItem3",
                "CollectableDekuShrineDekuButlerSRoomItem4",
                "CollectableDekuShrineDekuButlerSRoomItem5",
                "CollectableDekuShrineDekuButlerSRoomItem6",
                "CollectableDekuShrineDekuButlerSRoomItem7",
                "CollectableDekuShrineDekuButlerSRoomItem8",
                "CollectableDekuShrineDekuButlerSRoomItem9",
                "CollectableDekuShrineDekuButlerSRoomItem10",
                "CollectableDekuShrineGreyBoulderRoomPot1",
                "CollectableEastClockTownWoodenCrateSmall1",
                "CollectableGreatBayTempleWaterControlRoomItem3",
                "CollectableGreatBayTempleWaterControlRoomItem4",
                "CollectableIkanaGraveyardIkanaGraveyardLowerGrass2",
                "CollectableMagicHagsPotionShopItem1",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem2",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem3",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem4",
                "CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem5",
                "CollectableSecretShrineEntranceRoomItem1",
                "CollectableSecretShrineEntranceRoomItem2",
                "CollectableSecretShrineEntranceRoomItem3",
                "CollectableSecretShrineEntranceRoomItem4",
                "CollectableSecretShrineEntranceRoomItem5",
                "CollectableSecretShrineEntranceRoomItem6",
                "CollectableSecretShrineEntranceRoomItem7",
                "CollectableSecretShrineEntranceRoomItem8",
                "CollectableSecretShrineEntranceRoomItem9",
                "CollectableSecretShrineEntranceRoomItem10",
                "CollectableSecretShrineEntranceRoomItem11",
                "CollectableSecretShrineEntranceRoomItem12",
                "CollectableSecretShrineEntranceRoomItem13",
                "CollectableSecretShrineEntranceRoomItem14",
                "CollectableSecretShrineEntranceRoomItem15",
                "CollectableSecretShrineEntranceRoomItem16",
                "CollectableSecretShrineEntranceRoomItem17",
                "CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot2",
                "CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot2",
                "CollectableStoneTowerTempleLavaRoomItem3",
                "CollectableStoneTowerTempleLavaRoomItem4",
                "CollectableStoneTowerTempleLavaRoomItem5",
                "CollectableStoneTowerTempleInvertedEyegoreRoomItem2",
                "CollectableClockTowerRooftopPot1",
                "CollectableClockTowerRooftopPot2",
                "CollectableClockTowerRooftopPot3",
                "CollectableClockTowerRooftopPot4",
                "CollectableGoronRacetrackPot1",
                "CollectableGoronRacetrackPot2",
                "CollectableGoronRacetrackPot3",
                "CollectableGoronRacetrackPot4",
                "CollectableGoronRacetrackPot5",
                "CollectableGoronRacetrackPot6",
                "CollectableGoronRacetrackPot7",
                "CollectableGoronRacetrackPot8",
                "CollectableGoronRacetrackPot9",
                "CollectableGoronRacetrackPot10",
                "CollectableGoronRacetrackPot11",
                "CollectableGoronRacetrackPot12",
                "CollectableGoronRacetrackPot13",
                "CollectableGoronRacetrackPot14",
                "CollectableGoronRacetrackPot15",
                "CollectableGoronRacetrackPot16",
                "CollectableGoronRacetrackPot17",
                "CollectableGoronRacetrackPot18",
                "CollectableGoronRacetrackPot19",
                "CollectableGoronRacetrackPot20",
                "CollectableGoronRacetrackPot21",
                "CollectableGoronRacetrackPot22",
                "CollectableGoronRacetrackPot23",
                "CollectableGoronRacetrackPot24",
                "CollectableGoronRacetrackPot25",
                "CollectableGoronRacetrackPot26",
                "CollectableGoronRacetrackPot27",
                "CollectableGoronShrineGoronKidSRoomPot3",
                "CollectableGoronShrineMainRoomPot4",
                "CollectableGoronShrineMainRoomPot5",
                "CollectableGoronShrineMainRoomPot6",
                "CollectableGreatBayCoastPot10",
                "CollectableGreatBayTempleBlueChuchuValveRoomWoodenCrateLarge1",
                "CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot3",
                "CollectableIkanaCanyonMainAreaGrass3",
                "CollectableMilkRoadGrass2",
                "CollectableMountainVillageSpringSmallSnowball3",
                "CollectableMountainVillageWinterSmallSnowball3",
                "CollectableMountainVillageWinterSmallSnowball4",
                "CollectableMountainVillageWinterSmallSnowball5",
                "CollectableSnowheadSmallSnowball3",
                "CollectableStoneTowerPot5",
                "CollectableStoneTowerInvertedStoneTowerFlippedPot2",
                "CollectableTheMoonLinkTrialEntrancePot1",
                "CollectableTheMoonLinkTrialEntrancePot2",
                "CollectableTheMoonLinkTrialEntrancePot3",
                "CollectableTheMoonLinkTrialEntrancePot4",
                "CollectableZoraCapePot3",
                "CollectableDampesHouseBasementPot8",
                "CollectablePiratesFortressItem1",
                "CollectablePiratesFortressItem2",
                "CollectablePiratesFortressItem3",
                "CollectableDekuShrineGiantRoomFloor1Item7",
                "CollectableDekuShrineGiantRoomFloor1Item8",
                "CollectableGreatBayTempleWaterControlRoomItem5",
                "CollectableGreatBayTempleCompassBossKeyRoomItem1",
                "CollectableGreatBayTempleCompassBossKeyRoomItem2",
                "CollectableGreatBayTempleTopmostRoomWithGreenValveItem1",
                "CollectableGreatBayTempleTopmostRoomWithGreenValveItem2",
                "CollectableLaundryPoolItem1",
                "CollectableLaundryPoolItem2",
                "CollectableLaundryPoolItem3",
                "CollectableMountainVillageWinterMountainVillageSpringItem1",
                "CollectableSnowheadTempleIceBlockRoomItem1",
                "CollectableSnowheadTempleIceBlockRoomItem2",
                "CollectableSnowheadTempleIceBlockRoomItem3",
                "CollectableSouthernSwampPoisonedCentralSwampBeehive1",
                "CollectableStoneTowerTempleLavaRoomItem6",
                "CollectableStoneTowerTempleEyegoreRoomItem1",
                "CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge1",
                "CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge2",
                "CollectableStoneTowerTempleEyegoreRoomItem2",
                "CollectableStoneTowerTempleInvertedEyegoreRoomItem3",
                "CollectableStoneTowerTempleInvertedAirRoomItem1",
                "CollectableStoneTowerTempleInvertedAirRoomItem2",
                "CollectableTerminaFieldItem1",
                "CollectableWoodfallTemplePreBossRoomItem5",
                "CollectableWoodfallTemplePreBossRoomItem6",
                "CollectableAncientCastleOfIkanaCastleExteriorGrass3",
                "CollectableAncientCastleOfIkanaCastleExteriorGrass4",
                "CollectableAncientCastleOfIkanaFireCeilingRoomPot1",
                "CollectableAncientCastleOfIkanaHoleRoomPot1",
                "CollectableAncientCastleOfIkanaHoleRoomPot2",
                "CollectableAstralObservatorySewerPot1",
                "CollectableAstralObservatorySewerPot2",
                "CollectableAstralObservatoryObservatoryBombersHideoutPot3",
                "CollectableBeneathTheGraveyardMainAreaPot2",
                "CollectableDekuPalaceEastInnerGardenPot1",
                "CollectableDekuPalaceEastInnerGardenPot2",
                "CollectableGoronRacetrackPot28",
                "CollectableGoronRacetrackPot29",
                "CollectableGoronRacetrackPot30",
                "CollectableGoronShrineMainRoomPot7",
                "CollectableGoronShrineMainRoomPot8",
                "CollectableGoronVillageWinterLargeSnowball4",
                "CollectableGoronVillageWinterLargeSnowball5",
                "CollectableGoronVillageWinterLargeSnowball6",
                "CollectableGoronVillageWinterSmallSnowball9",
                "CollectableGoronVillageWinterSmallSnowball10",
                "CollectableIgosDuIkanaSLairPreBossRoomPot3",
                "CollectableIkanaGraveyardIkanaGraveyardLowerGrass3",
                "CollectableMountainVillageWinterSmallSnowball6",
                "CollectableMountainVillageWinterSmallSnowball7",
                "CollectableMountainVillageWinterLargeSnowball3",
                "CollectableMountainVillageWinterLargeSnowball4",
                "CollectableOceansideSpiderHouseMainRoomPot1",
                "CollectableOceansideSpiderHouseEntrancePot3",
                "CollectableOceansideSpiderHouseMainRoomPot2",
                "CollectableOceansideSpiderHouseStorageRoomPot1",
                "CollectablePathToGoronVillageWinterLargeSnowball1",
                "CollectablePathToGoronVillageWinterLargeSnowball2",
                "CollectablePathToGoronVillageWinterLargeSnowball3",
                "CollectablePathToGoronVillageWinterLargeSnowball4",
                "CollectablePathToGoronVillageWinterLargeSnowball5",
                "CollectablePathToGoronVillageWinterLargeSnowball6",
                "CollectablePathToGoronVillageWinterLargeSnowball7",
                "CollectablePathToGoronVillageWinterLargeSnowball8",
                "CollectablePathToGoronVillageWinterLargeSnowball9",
                "CollectablePathToGoronVillageWinterLargeSnowball10",
                "CollectablePathToGoronVillageWinterLargeSnowball11",
                "CollectablePathToGoronVillageWinterLargeSnowball12",
                "CollectablePathToGoronVillageWinterLargeSnowball13",
                "CollectablePathToGoronVillageWinterLargeSnowball14",
                "CollectablePathToGoronVillageWinterSmallSnowball1",
                "CollectablePathToGoronVillageWinterSmallSnowball2",
                "CollectablePathToGoronVillageWinterSmallSnowball3",
                "CollectablePathToMountainVillageSmallSnowball1",
                "CollectablePathToSnowheadLargeSnowball1",
                "CollectablePathToSnowheadLargeSnowball2",
                "CollectablePathToSnowheadLargeSnowball3",
                "CollectablePathToSnowheadLargeSnowball4",
                "CollectablePinnacleRockPot1",
                "CollectablePinnacleRockPot2",
                "CollectablePinnacleRockPot3",
                "CollectablePinnacleRockPot4",
                "CollectableSecretShrineMainRoomPot3",
                "CollectableSecretShrineMainRoomPot4",
                "CollectableSnowheadLargeSnowball1",
                "CollectableSnowheadLargeSnowball2",
                "CollectableSnowheadLargeSnowball3",
                "CollectableSnowheadLargeSnowball4",
                "CollectableSnowheadLargeSnowball5",
                "CollectableSnowheadLargeSnowball6",
                "CollectableStoneTowerPot6",
                "CollectableStoneTowerPot7",
                "CollectableStoneTowerPot8",
                "CollectableStoneTowerPot9",
                "CollectableStoneTowerPot10",
                "CollectableZoraCapePot4",
                "CollectableRomaniRanchInvisibleItem1",
                "CollectableRomaniRanchInvisibleItem2",
                "CollectableRomaniRanchInvisibleItem3",
                "CollectableRomaniRanchInvisibleItem4",
                "CollectableRomaniRanchInvisibleItem5",
                "CollectableRomaniRanchInvisibleItem6",
                "CollectableTerminaFieldInvisibleItem1",
                "CollectableTerminaFieldInvisibleItem2",
                "CollectableTerminaFieldInvisibleItem3",
                "CollectableTerminaFieldInvisibleItem4",
                "CollectableTerminaFieldInvisibleItem5",
                "CollectableTerminaFieldInvisibleItem6",
                "CollectableTerminaFieldInvisibleItem7",
                "CollectableTerminaFieldInvisibleItem8",
                "CollectableTerminaFieldInvisibleItem9",
                "CollectableTerminaFieldInvisibleItem10",
                "CollectableTerminaFieldInvisibleItem11",
                "CollectableSwampSpiderHouseInvisibleItem1",
                "CollectableSwampSpiderHouseInvisibleItem2",
                "CollectableSwampSpiderHouseInvisibleItem3",
                "CollectableSwampSpiderHouseInvisibleItem4",
                "CollectableSwampSpiderHouseInvisibleItem5",
                "CollectableTerminaFieldTreeItem1",
                "CollectableTerminaFieldPillarItem1",
                "CollectableTerminaFieldTelescopeGuay1",
                "CollectableSwordsmanSchoolGong1",
                "CollectableBeanGrottoSoftSoil1",
                "CollectableDekuPalaceSoftSoil1",
                "CollectableDoggyRacetrackSoftSoil1",
                "CollectableGreatBayCoastSoftSoil1",
                "CollectableRomaniRanchSoftSoil1",
                "CollectableRomaniRanchSoftSoil2",
                "CollectableSecretShrineSoftSoil1",
                "CollectableStoneTowerSoftSoil1",
                "CollectableStoneTowerSoftSoil2",
                "CollectableSwampSpiderHouseSoftSoil1",
                "CollectableSwampSpiderHouseSoftSoil2",
                "CollectableTerminaFieldSoftSoil1",
                "CollectableTerminaFieldSoftSoil2",
                "CollectableTerminaFieldSoftSoil3",
                "CollectableTerminaFieldSoftSoil4",
                "CollectableTerminaFieldGuay1",
                "CollectableTerminaFieldGuay2",
                "CollectableTerminaFieldGuay3",
                "CollectableTerminaFieldGuay4",
                "CollectableTerminaFieldGuay5",
                "CollectableTerminaFieldGuay6",
                "CollectableTerminaFieldGuay7",
                "CollectableTerminaFieldGuay8",
                "CollectableTerminaFieldGuay9",
                "CollectableTerminaFieldGuay10",
                "CollectableTerminaFieldGuay11",
                "CollectableTerminaFieldGuay12",
                "CollectableTerminaFieldGuay13",
                "CollectableTerminaFieldGuay14",
                "CollectableTerminaFieldGuay15",
                "CollectableTerminaFieldGuay16",
                "CollectableTerminaFieldGuay17",
                "CollectableTerminaFieldGuay18",
                "CollectableTerminaFieldGuay19",
                "CollectableTerminaFieldGuay20",
                "CollectableTerminaFieldGuay21",
                "CollectableTerminaFieldGuay22",
                "CollectableTerminaFieldGuay23",
                "CollectableDekuPalaceRupeeCluster1",
                "CollectableDekuPalaceRupeeCluster2",
                "CollectableDekuPalaceRupeeCluster3",
                "CollectableDekuPalaceRupeeCluster4",
                "CollectableDekuPalaceRupeeCluster5",
                "CollectableDekuPalaceRupeeCluster6",
                "CollectableDekuPalaceRupeeCluster7",
                "CollectableBeneathTheGraveyardRupeeCluster1",
                "CollectableBeneathTheGraveyardRupeeCluster2",
                "CollectableBeneathTheGraveyardRupeeCluster3",
                "CollectableBeneathTheGraveyardRupeeCluster4",
                "CollectableBeneathTheGraveyardRupeeCluster5",
                "CollectableBeneathTheGraveyardRupeeCluster6",
                "CollectableBeneathTheGraveyardRupeeCluster7",
                "CollectableTerminaFieldSongWall1",
                "CollectableTerminaFieldSongWall2",
                "CollectableTerminaFieldSongWall3",
                "CollectableTerminaFieldSongWall4",
                "CollectableTerminaFieldSongWall5",
                "CollectableTerminaFieldSongWall6",
                "CollectableTerminaFieldSongWall7",
                "CollectableTerminaFieldSongWall8",
                "CollectableTerminaFieldSongWall9",
                "CollectableTerminaFieldSongWall10",
                "CollectableTerminaFieldSongWall11",
                "CollectableTerminaFieldSongWall12",
                "CollectableTerminaFieldSongWall13",
                "CollectableTerminaFieldSongWall14",
                "CollectableTerminaFieldSongWall15",
                "CollectableDekuPlaygroundItem1",
                "CollectableDekuPlaygroundItem2",
                "CollectableDekuPlaygroundItem3",
                "CollectableDekuPlaygroundItem4",
                "CollectableDekuPlaygroundItem5",
                "CollectableDekuPlaygroundItem6",
                "CollectableDekuPlaygroundItem7",
                "CollectableDekuPlaygroundItem8",
                "CollectableDekuPlaygroundItem9",
                "CollectableDekuPlaygroundItem10",
                "CollectableDekuPlaygroundItem11",
                "CollectableDekuPlaygroundItem12",
                "CollectableDekuPlaygroundItem13",
                "CollectableDekuPlaygroundItem14",
                "CollectableDekuPlaygroundItem15",
                "CollectableDekuPlaygroundItem16",
                "CollectableDekuPlaygroundItem17",
                "CollectableDekuPlaygroundItem18",
                "CollectablePiratesFortressHitTag1",
                "CollectablePiratesFortressHitTag2",
                "CollectablePiratesFortressHitTag3",
                "CollectablePiratesFortressHitTag4",
                "CollectablePiratesFortressHitTag5",
                "CollectablePiratesFortressHitTag6",
                "CollectablePiratesFortressInteriorHookshotRoomHitTag1",
                "CollectablePiratesFortressInteriorHookshotRoomHitTag2",
                "CollectablePiratesFortressInteriorHookshotRoomHitTag3",
                "CollectableSwampSpiderHouseHitTag1",
                "CollectableSwampSpiderHouseHitTag2",
                "CollectableSwampSpiderHouseHitTag3",
                "CollectableSwampSpiderHouseHitTag4",
                "CollectableSwampSpiderHouseHitTag5",
                "CollectableSwampSpiderHouseHitTag6",
                "CollectableSwampSpiderHouseHitTag7",
                "CollectableSwampSpiderHouseHitTag8",
                "CollectableSwampSpiderHouseHitTag9",
                "CollectableSwampSpiderHouseHitTag10",
                "CollectableSwampSpiderHouseHitTag11",
                "CollectableSwampSpiderHouseHitTag12",
                "CollectableOceansideSpiderHouseHitTag1",
                "CollectableOceansideSpiderHouseHitTag2",
                "CollectableOceansideSpiderHouseHitTag3",
                "CollectableOceansideSpiderHouseHitTag4",
                "CollectableOceansideSpiderHouseHitTag5",
                "CollectableOceansideSpiderHouseHitTag6",
                "CollectableOceansideSpiderHouseHitTag7",
                "CollectableOceansideSpiderHouseHitTag8",
                "CollectableOceansideSpiderHouseHitTag9",
                "CollectableTerminaFieldHitTag1",
                "CollectableTerminaFieldHitTag2",
                "CollectableTerminaFieldHitTag3",
                "CollectableTerminaFieldHitTag4",
                "CollectableTerminaFieldHitTag5",
                "CollectableTerminaFieldHitTag6",
                "CollectableTerminaFieldHitTag7",
                "CollectableTerminaFieldHitTag8",
                "CollectableTerminaFieldHitTag9",
                "CollectableCuccoShackHitTag1",
                "CollectableCuccoShackHitTag2",
                "CollectableCuccoShackHitTag3",
                "CollectableCuccoShackHitTag4",
                "CollectableCuccoShackHitTag5",
                "CollectableCuccoShackHitTag6",
                "CollectableIkanaGraveyardHitTag1",
                "CollectableIkanaGraveyardHitTag2",
                "CollectableIkanaGraveyardHitTag3",
                "CollectableIkanaGraveyardHitTag4",
                "CollectableIkanaGraveyardHitTag5",
                "CollectableIkanaGraveyardHitTag6",
                "CollectableIkanaGraveyardHitTag7",
                "CollectableIkanaGraveyardHitTag8",
                "CollectableIkanaGraveyardHitTag9",
                "CollectableIkanaGraveyardHitTag10",
                "CollectableIkanaGraveyardHitTag11",
                "CollectableIkanaGraveyardHitTag12",
                "CollectableStockPotInnHitTag1",
                "CollectableStockPotInnHitTag2",
                "CollectableStockPotInnHitTag3",
                "CollectableEastClockTownHitTag1",
                "CollectableEastClockTownHitTag2",
                "CollectableEastClockTownHitTag3",
                "CollectableEastClockTownHitTag4",
                "CollectableEastClockTownHitTag5",
                "CollectableEastClockTownHitTag6",
                "CollectableEastClockTownHitTag7",
                "CollectableEastClockTownHitTag8",
                "CollectableEastClockTownHitTag9",
                "CollectableSouthClockTownHitTag1",
                "CollectableSouthClockTownHitTag2",
                "CollectableSouthClockTownHitTag3",
                "CollectableTerminaFieldEnemy1",
                "CollectablePiratesFortressInteriorHookshotRoomPot1",
                "CollectablePiratesFortressInteriorHookshotRoomPot2",
                "CollectableTerminaFieldRock1",
                "CollectableTerminaFieldRock2",
                "CollectableIkanaGraveyardIkanaGraveyardUpperRock1",
                "CollectableIkanaGraveyardIkanaGraveyardUpperRock2",
                "CollectableIkanaGraveyardIkanaGraveyardUpperRock3",
                "CollectableTerminaFieldRock3",
                "CollectableTerminaFieldRock4",
                "CollectableTerminaFieldRock5",
                "CollectableTerminaFieldRock6",
                "CollectableTerminaFieldRock7",
                "CollectableIkanaGraveyardIkanaGraveyardUpperRock4",
                "CollectableIkanaGraveyardIkanaGraveyardUpperRock5",
                "CollectableTerminaFieldRock8",
                "CollectableTerminaFieldRock9",
                "CollectableMilkRoadKeatonGrass1",
                "CollectableMilkRoadKeatonGrass2",
                "CollectableMilkRoadKeatonGrass3",
                "CollectableMilkRoadKeatonGrass4",
                "CollectableMilkRoadKeatonGrass5",
                "CollectableMilkRoadKeatonGrass6",
                "CollectableMilkRoadKeatonGrass7",
                "CollectableMilkRoadKeatonGrass8",
                "CollectableMilkRoadKeatonGrass9",
                "CollectableNorthClockTownKeatonGrass1",
                "CollectableNorthClockTownKeatonGrass2",
                "CollectableNorthClockTownKeatonGrass3",
                "CollectableNorthClockTownKeatonGrass4",
                "CollectableNorthClockTownKeatonGrass5",
                "CollectableNorthClockTownKeatonGrass6",
                "CollectableNorthClockTownKeatonGrass7",
                "CollectableNorthClockTownKeatonGrass8",
                "CollectableNorthClockTownKeatonGrass9",
                "CollectableMountainVillageSpringKeatonGrass1",
                "CollectableMountainVillageSpringKeatonGrass2",
                "CollectableMountainVillageSpringKeatonGrass3",
                "CollectableMountainVillageSpringKeatonGrass4",
                "CollectableMountainVillageSpringKeatonGrass5",
                "CollectableMountainVillageSpringKeatonGrass6",
                "CollectableMountainVillageSpringKeatonGrass7",
                "CollectableMountainVillageSpringKeatonGrass8",
                "CollectableMountainVillageSpringKeatonGrass9",
                "CollectableOceansideSpiderHouseMaskRoomPot1",
                "CollectableOceansideSpiderHouseMaskRoomPot2",
                "CollectableIkanaCanyonMainAreaGrass4",
                "CollectableIkanaCanyonMainAreaGrass5",
                "CollectableIkanaCanyonMainAreaGrass6",
                "CollectablePathToSnowheadSmallSnowball1",
                "CollectablePathToSnowheadSmallSnowball2",
                "CollectablePathToSnowheadSmallSnowball3",
                "CollectablePathToSnowheadSmallSnowball4",
                "CollectablePathToMountainVillageSmallSnowball2",
                "CollectablePathToMountainVillageSmallSnowball3",
                "CollectablePathToMountainVillageSmallSnowball4",
                "CollectableZoraCapeJarGame1",
                "CollectableIkanaGraveyardDay2Bats1",
                "CollectableCuccoShackPottedPlant1",
                "GossipTerminaSouth",
                "GossipSwampPotionShop",
                "GossipMountainSpringPath",
                "GossipMountainPath",
                "GossipOceanZoraGame",
                "GossipCanyonRoad",
                "GossipCanyonDock",
                "GossipCanyonSpiritHouse",
                "GossipTerminaMilk",
                "GossipTerminaWest",
                "GossipTerminaNorth",
                "GossipTerminaEast",
                "GossipRanchTree",
                "GossipRanchBarn",
                "GossipMilkRoad",
                "GossipOceanFortress",
                "GossipSwampRoad",
                "GossipTerminaObservatory",
                "GossipRanchCuccoShack",
                "GossipRanchRacetrack",
                "GossipRanchEntrance",
                "GossipCanyonRavine",
                "GossipMountainSpringFrog",
                "GossipSwampSpiderHouse",
                "GossipTerminaGossipLarge",
                "GossipTerminaGossipGuitar",
                "GossipTerminaGossipPipes",
                "GossipTerminaGossipDrums",
            };

            var itemNameIndex = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("- "))
                {
                    if (itemNameIndex >= itemNames.Count)
                    {
                        var itemName = line.Substring(2);
                        itemNames.Add(itemName);
                    }
                    itemNameIndex++;
                }
            }

            var items = new List<JsonFormatLogicItem>();

            int i = 0;
            while (true)
            {
                if (i >= lines.Count)
                {
                    break;
                }
                if (lines[i].StartsWith("#"))
                {
                    i++;
                    continue;
                }
                if (lines[i].Contains("-"))
                {
                    i++;
                    continue;
                }
                else
                {
                    var item = new JsonFormatLogicItem();
                    item.RequiredItems = new List<string>();
                    if (lines[i] != "")
                    {
                        foreach (var j in lines[i].Split(','))
                        {
                            item.RequiredItems.Add(itemNames[Convert.ToInt32(j)]);
                        }
                    }
                    item.ConditionalItems = new List<List<string>>();
                    if (lines[i + 1] != "")
                    {
                        foreach (var j in lines[i + 1].Split(';'))
                        {
                            var conditionals = new List<string>();
                            foreach (var k in j.Split(','))
                            {
                                conditionals.Add(itemNames[Convert.ToInt32(k)]);
                            }
                            item.ConditionalItems.Add(conditionals);
                        }
                    }
                    item.TimeNeeded = (TimeOfDay)Convert.ToInt32(lines[i + 2]);
                    item.TimeAvailable = (TimeOfDay)Convert.ToInt32(lines[i + 3]);
                    var trickInfo = lines[i + 4].Split(new char[] { ';' }, 2);
                    item.IsTrick = trickInfo.Length > 1;
                    item.TrickTooltip = item.IsTrick ? trickInfo[1] : null;
                    if (string.IsNullOrWhiteSpace(item.TrickTooltip))
                    {
                        item.TrickTooltip = null;
                    }
                    item.Id = itemNames[items.Count];
                    items.Add(item);
                    i += 5;
                }
            }

            //items.Remove("AreaWoodfallNew");
            //items.Remove("AreaSnowheadNew");
            //items.Remove("AreaGreatBayNew");
            //items.Remove("AreaLANew");
            //items.Remove("AreaInvertedStoneTowerNew");

            return new JsonFormatLogic
            {
                Version = 1,
                Logic = items,
            };
        }

        private static void AddBossRemains(JsonFormatLogic logicObject)
        {
            const int startIndex = 1068;
            var itemNames = new string[]
            {
                "RemainsOdolwa",
                "RemainsGoht",
                "RemainsGyorg",
                "RemainsTwinmold",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 2;
        }

        private static void AddOcarinaAndSongOfTime(JsonFormatLogic logicObject)
        {
            const int startIndex = 94;
            var itemNames = new string[]
            {
                "ItemOcarina",
                "SongTime",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 3;
        }

        private static void AddMultiLocations(JsonFormatLogic logicObject)
        {
            const int startIndex = 1075;
            var itemNames = new string[]
            {
                "ItemTingleMapTownInTown",
                "ItemTingleMapTownInCanyon",
                "ItemTingleMapWoodfallInSwamp",
                "ItemTingleMapWoodfallInTown",
                "ItemTingleMapSnowheadInMountain",
                "ItemTingleMapSnowheadInSwamp",
                "ItemTingleMapRanchInRanch",
                "ItemTingleMapRanchInMountain",
                "ItemTingleMapGreatBayInOcean",
                "ItemTingleMapGreatBayInRanch",
                "ItemTingleMapStoneTowerInCanyon",
                "ItemTingleMapStoneTowerInOcean",
                "HeartPiecePostBoxInSCT",
                "HeartPiecePostBoxInNCT",
                "HeartPiecePostBoxInECT",
                "HeartPieceKeatonQuizInNCT",
                "HeartPieceKeatonQuizInMilkRoad",
                "HeartPieceKeatonQuizInMountainVillage",
                "SongOathInWFT",
                "SongOathInSHT",
                "SongOathInGBT",
                "SongOathInISTT",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 4;
        }

        private static void AddMultiLocationGoronShop(JsonFormatLogic logicObject)
        {
            const int startIndex = 1097;
            var itemNames = new string[]
            {
                "ShopItemGoronBomb10InWinter",
                "ShopItemGoronBomb10InSpring",
                "ShopItemGoronArrow10InWinter",
                "ShopItemGoronArrow10InSpring",
                "ShopItemGoronRedPotionInWinter",
                "ShopItemGoronRedPotionInSpring",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 5;
        }

        private static void AddOtherMagicBean(JsonFormatLogic logicObject)
        {
            const int startIndex = 123;
            var itemNames = new string[]
            {
                "OtherMagicBean",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 6;
        }

        private static void AddMultiLocationBusinessScrubs(JsonFormatLogic logicObject)
        {
            const int startIndex = 1104;
            var itemNames = new string[]
            {
                "ShopItemBusinessScrubMagicBeanInSwamp",
                "ShopItemBusinessScrubMagicBeanInTown",
                "UpgradeBiggestBombBagInMountain",
                "UpgradeBiggestBombBagInSwamp",
                "ShopItemBusinessScrubGreenPotionInOcean",
                "ShopItemBusinessScrubGreenPotionInMountain",
                "ShopItemBusinessScrubBluePotionInCanyon",
                "ShopItemBusinessScrubBluePotionInOcean",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 7;
        }

        private static void AddOtherTimeTravel(JsonFormatLogic logicObject)
        {
            const int startIndex = 124;
            var itemNames = new string[]
            {
                "OtherTimeTravel",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 8;
        }

        private static void AddBeansAndDekuPlayground(JsonFormatLogic logicObject)
        {
            const int startIndex = 124;
            var itemNames = new string[]
            {
                "OtherLimitlessBeans",
                "OtherPlayDekuPlayground",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 9;
        }

        private static void AddRoyalWallet(JsonFormatLogic logicObject)
        {
            const int startIndex = 32;
            var itemNames = new string[]
            {
                "UpgradeRoyalWallet",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 10;
        }

        private static void AddMultiLocationClockTownFairy(JsonFormatLogic logicObject)
        {
            const int startIndex = 1116;
            var itemNames = new string[]
            {
                "CollectibleStrayFairyClockTownInLaundryPool",
                "CollectibleStrayFairyClockTownInECT",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 11;
        }

        private static void AddGaroHints(JsonFormatLogic logicObject)
        {
            const int startIndex = 1146;
            var itemNames = new string[]
            {
                "HintGaroCanyonLower1",
                "HintGaroCanyonLower2",
                "HintGaroWithIgosDefeated",
                "HintGaroCanyonUpper1",
                "HintGaroCanyonUpper2",
                "HintGaroCanyonUpper3",
                "HintGaroCanyonUpper4",
                "HintGaroCanyonUpper1WithStorms",
                "HintGaroCanyonUpper2WithStorms",
                "HintGaroCanyonUpper3WithStorms",
                "HintGaroCanyonUpper4WithStorms",
                "HintGaroCastleLower1",
                "HintGaroCastleLower2",
                "HintGaroCastleLower3",
                "HintGaroCastleUpper",
                "HintGaroMaster",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 12;
        }

        private static void AddBossDoors(JsonFormatLogic logicObject)
        {
            logicObject.Logic.RemoveRange(128, 5);

            const int startIndex = 128;
            var itemNames = new string[]
            {
                "AreaOdolwasLair",
                "AreaGohtsLair",
                "AreaGyorgsLair",
                "AreaTwinmoldsLair",
                "OtherKillOdolwa",
                "OtherKillGoht",
                "OtherKillGyorg",
                "OtherKillTwinmold",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 13;
        }

        private static void AddLullabyIntro(JsonFormatLogic logicObject)
        {
            logicObject.Logic.Insert(1083, new JsonFormatLogicItem
            {
                Id = "SongLullabyIntro",
                RequiredItems = new List<string>(),
                ConditionalItems = new List<List<string>>
                {
                    new List<string> { "SongLullabyIntroInMountainVillage" },
                    new List<string> { "SongLullabyIntroInTwinIslands" },
                },
            });

            const int startIndex = 1122;
            var itemNames = new string[]
            {
                "SongLullabyIntroInMountainVillage",
                "SongLullabyIntroInTwinIslands",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 14;
        }

        private static void AddNotebookEntries(JsonFormatLogic logicObject)
        {
            const int startIndex = 1084;
            var itemNames = new string[]
            {
                "NotebookMeetBombers",
                "NotebookMeetAnju",
                "NotebookMeetKafei",
                "NotebookMeetCuriosityShopMan",
                "NotebookMeetOldLady",
                "NotebookMeetRomani",
                "NotebookMeetCremia",
                "NotebookMeetMayorDotour",
                "NotebookMeetMadameAroma",
                "NotebookMeetToto",
                "NotebookMeetGorman",
                "NotebookMeetPostman",
                "NotebookMeetRosaSisters",
                "NotebookMeetToiletHand",
                "NotebookMeetAnjusGrandmother",
                "NotebookMeetKamaro",
                "NotebookMeetGrog",
                "NotebookMeetGormanBrothers",
                "NotebookMeetShiro",
                "NotebookMeetGuruGuru",
                "NotebookInnReservation",
                "NotebookPromiseAnjuMeeting",
                "NotebookPromiseAnjuDelivery",
                "NotebookDepositLetterToKafei",
                "NotebookPromiseKafei",
                "NotebookDeliverPendant",
                "NotebookEscapeFromSakonSHideout",
                "NotebookPromiseRomani",
                "NotebookSaveTheCows",
                "NotebookProtectMilkDelivery",
                "NotebookCuriosityShopManSGift",
                "NotebookPromiseCuriosityShopMan",
                "NotebookDeliverLetterToMama",
                "NotebookLearnBombersCode",
                "NotebookDotoursThanks",
                "NotebookRosaSistersThanks",
                "NotebookToiletHandSThanks",
                "NotebookGrandmaShortStory",
                "NotebookGrandmaLongStory",
                "NotebookPostmansGame",
                "NotebookPromiseMadameAroma",
                "NotebookPurchaseCuriosityShopItem",
                "NotebookGrogsThanks",
                "NotebookDefeatGormanBrothers",
                "NotebookMovingGorman",
                "NotebookPostmansFreedom",
                "NotebookUniteAnjuAndKafei",
                "NotebookSaveOldLady",
                "NotebookPromiseKamaro",
                "NotebookSaveInvisibleSoldier",
                "NotebookGuruGuru",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));

            const int startMultiLocationIndex = 1175;
            itemNames = new string[]
            {
                "NotebookMeetBombersInNCT",
                "NotebookMeetBombersInECT",
                "NotebookMeetAnjuInInn",
                "NotebookMeetAnjuInLaundryPool",
                "NotebookMeetAnjuInRanch",
                "NotebookMeetKafeiInLaundryPool",
                "NotebookMeetKafeiInIkanaCanyon",
                "NotebookMeetKafeiInInn",
                "NotebookMeetCuriosityShopManInWCT",
                "NotebookMeetCuriosityShopManInLaundryPool",
                "NotebookMeetOldLadyInNCT",
                "NotebookMeetOldLadyInWCT",
                "NotebookMeetGormanInECT",
                "NotebookMeetGormanInInn",
                "NotebookMeetPostmanInWCT",
                "NotebookMeetPostmanInSCT",
                "NotebookMeetPostmanInNCT",
                "NotebookMeetPostmanInECT",
                "NotebookMeetPostmanInInn",
                "NotebookMeetPostmanInLaundryPool",
                "NotebookMeetRosaSistersInWCT",
                "NotebookMeetRosaSistersInInn",
                "NotebookMeetAnjusGrandmotherInInn",
                "NotebookMeetAnjusGrandmotherInRanch",
                "NotebookMeetGuruGuruInInn",
                "NotebookMeetGuruGuruInLaundryPool",
                "NotebookDepositLetterToKafeiInSCT",
                "NotebookDepositLetterToKafeiInNCT",
                "NotebookDepositLetterToKafeiInECT",
                "NotebookLearnBombersCodeInNCT",
                "NotebookLearnBombersCodeInECT",
            };

            logicObject.Logic.InsertRange(startMultiLocationIndex, GetLogicItems(itemNames));
            logicObject.Version = 15;
        }

        private static void AddFairies(JsonFormatLogic logicObject)
        {
            void addFairySummon(JsonFormatLogicItem item)
            {
                item.RequiredItems.Add("Summon Fairy");
            }

            void addFairySummonAndRemoveMaskOfTruth(JsonFormatLogicItem item)
            {
                item.RequiredItems.Add("Summon Fairy");
                item.RequiredItems.Remove("MaskTruth");
            }

            void removeGoronMask(JsonFormatLogicItem item)
            {
                item.RequiredItems.Remove("MaskGoron");
            }

            void removeEponasSong(JsonFormatLogicItem item)
            {
                if (!item.RequiredItems.Remove("Play Epona's Song"))
                {
                    item.RequiredItems.Remove("SongEpona");
                    item.RequiredItems.Remove("ItemOcarina");
                }
            }

            const int startIndex = 1135;
            const int newItemCount = 82;
            var itemNames = new (string name, int? reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("CollectableAncientCastleOfIkanaCastleExteriorPot1", 443, null),
                ("CollectableAncientCastleOfIkanaHoleRoomPot3", 769, null),
                ("CollectableAncientCastleOfIkanaHoleRoomPot4", 769, null),
                ("CollectableGreatBayCoastPot11", 455, null),
                ("CollectableGreatBayTempleEntranceRoomBarrel1", 507, null),
                ("CollectableIkanaCanyonMainAreaGrass7", 480, null),
                ("CollectableIkanaCanyonMainAreaGrass8", 480, null),
                ("CollectableMilkRoadGrass3", 481, null),
                ("CollectableMountainVillageSpringPot1", 482, (item) =>
                {
                    addConditional(item, "OtherArrow");
                    addConditional(item, "MaskZora");
                    addConditional(item, "ItemHookshot");
                    addConditionalIfExists(logicObject, item, "Clever Bombchu Usage");
                    addConditionalIfExists(logicObject, item, "Bomb Hovering");
                    addConditionalIfExists(logicObject, item, "Long Stick");
                }),
                ("CollectableMountainVillageSpringSmallSnowball4", 482, null),
                ("CollectableMountainVillageWinterPot1", 483, (item) =>
                {
                    addConditionalIfExists(logicObject, item, "Short Ranged Weapon");
                    addConditionalIfExists(logicObject, item, "Any Bomb Bag");
                    addConditionalIfExists(logicObject, item, "Powder Kegs as Explosives", "ItemPowderKeg");
                    addConditionalIfExists(logicObject, item, "Long Stick");
                }),
                ("CollectableMountainVillageWinterSmallSnowball8", 483, null),
                ("CollectableRoadToIkanaPot1", 464, (item) =>
                {
                    addConditional(item, "OtherArrow");
                    addConditional(item, "ItemHookshot");
                    addConditionalIfExists(logicObject, item, "Bomb Hovering");
                    addConditionalIfExists(logicObject, item, "Long Stick");
                }),
                ("CollectableSecretShrineMainRoomPot5", 471, null),
                ("CollectableSnowheadSmallSnowball10", 487, null),
                ("CollectableSnowheadTempleMainRoomPot3", 380, removeGoronMask),
                ("CollectableSouthernSwampClearCentralSwampGrass2", 604, null),
                ("CollectableSouthernSwampPoisonedCentralSwampGrass2", 605, null),
                ("CollectableStoneTowerPot11", 831, null),
                ("CollectableStoneTowerPot12", 831, null),
                ("CollectableStoneTowerPot13", 475, null),
                ("CollectableStoneTowerPot14", 488, null),
                ("CollectableStoneTowerInvertedStoneTowerFlippedPot3", 590, null),
                ("CollectableStoneTowerTempleRoomAfterLightArrowsPot1", 0, (item) =>
                {
                    item.RequiredItems.Add("AreaStoneTowerTempleAccess");
                    addRequiredIfExists(logicObject, item, "STT Garo Master Access");
                    addRequiredIfExists(logicObject, item, "STT Thin Bridge Exit");
                }),
                ("CollectableStoneTowerTempleInvertedWizzrobeRoomPot1", 408, (item) =>
                {
                    if (removeConditional(item, "ISTT Lightless", "ISTT Cross Poe Gap"))
                    {
                        addConditionalIfExists(logicObject, item, "ISTT Lightless", "ISTT Cross Poe Gap", "OtherArrow");
                        addConditionalIfExists(logicObject, item, "ISTT Lightless", "ISTT Cross Poe Gap", "ItemHookshot");
                        addConditionalIfExists(logicObject, item, "ISTT Lightless", "ISTT Cross Poe Gap", "MaskZora", "Gainer");
                        addConditionalIfExists(logicObject, item, "ISTT Lightless", "ISTT Cross Poe Gap", "Fierce Deity's Mask Anywhere");
                        addConditionalIfExists(logicObject, item, "ISTT Lightless", "ISTT Cross Poe Gap", "Clever Bombchu Usage");
                    }
                    else
                    {
                        addTodo(item); // user must handle it themselves
                    }
                }),
                ("CollectableTerminaFieldPot1", 0, (item) =>
                {
                    addConditionalOrTodo(logicObject, item, "OtherMagicBean", "Water for Magic Bean");
                    addConditionalOrTodo(logicObject, item, "Short Ranged Weapon");
                    addConditionalIfExists(logicObject, item, "Clever Bombchu Usage");
                    addConditionalIfExists(logicObject, item, "Bomb Hovering");
                    addConditionalIfExists(logicObject, item, "Powder Kegs as Explosives", "ItemPowderKeg", "Fewer Item Requirements");
                    addConditionalIfExists(logicObject, item, "Long Stick");
                    addConditionalIfExists(logicObject, item, "FD Jumps", "Jump Slash Take Downs");
                }
                ),
                ("CollectableWoodfallPot3", 596, null),
                ("CollectableWoodfallTempleEntranceRoomPot1", 0, (item) => item.RequiredItems.Add("AreaWoodFallTempleAccess")),
                ("CollectableZoraCapePot5", 489, null),
                ("CollectableCuccoShackGossipFairy1", 448, addFairySummon),
                ("CollectableDoggyRacetrackGossipFairy1", 497, addFairySummon),
                ("CollectableGreatBayCoastGossipFairy1", 455, addFairySummon),
                ("CollectableGrottosOceanGossipStonesGossipFairy1", 0, addFairySummon),
                ("CollectableIkanaCanyonMainAreaGossipFairy1", 191, addFairySummon),
                ("CollectableIkanaCanyonMainAreaGossipFairy2", 480, addFairySummon),
                ("CollectableIkanaCanyonSakonSHideoutAreaGossipFairy1", 257, addFairySummon),
                ("CollectableMilkRoadGossipFairy1", 0, addFairySummon),
                ("CollectableMountainVillageSpringPathToGoronGraveyardGossipFairy1", 224, addFairySummon),
                ("CollectableMountainVillageSpringGossipFairy1", 1055, addFairySummon),
                ("CollectablePathToMountainVillageGossipFairy1", 1292 - newItemCount, addFairySummonAndRemoveMaskOfTruth),
                ("CollectableRoadToIkanaGossipFairy1", 1294 - newItemCount, addFairySummonAndRemoveMaskOfTruth),
                ("CollectableRoadToSouthernSwampGossipFairy1", 1305 - newItemCount, addFairySummonAndRemoveMaskOfTruth),
                ("CollectableRomaniRanchGossipFairy1", 486, addFairySummon),
                ("CollectableRomaniRanchGossipFairy2", 486, addFairySummon),
                ("CollectableRomaniRanchGossipFairy3", 486, addFairySummon),
                ("CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorGossipFairy1", 537, addFairySummon),
                ("CollectableSwampSpiderHouseTreeRoomGossipFairy1", 301, addFairySummon),
                ("CollectableTerminaFieldGossipFairy1", 1306 - newItemCount, addFairySummonAndRemoveMaskOfTruth),
                ("CollectableTerminaFieldGossipFairy2", 0, addFairySummon),
                ("CollectableTerminaFieldGossipFairy3", 0, addFairySummon),
                ("CollectableTerminaFieldGossipFairy4", 0, addFairySummon),
                ("CollectableTerminaFieldGossipFairy5", 0, addFairySummon),
                ("CollectableTerminaFieldGossipFairy6", 0, addFairySummon),
                ("CollectableTheMoonDekuTrialDekuTrialGossipFairy1", 276, addFairySummon),
                ("CollectableTheMoonDekuTrialDekuTrialGossipFairy2", 276, addFairySummon),
                ("CollectableTheMoonDekuTrialDekuTrialGossipFairy3", 276, addFairySummon),
                ("CollectableTheMoonDekuTrialDekuTrialGossipFairy4", 276, addFairySummon),
                ("CollectableTheMoonDekuTrialDekuTrialGossipFairy5", 276, addFairySummon),
                ("CollectableTheMoonGoronTrialGoronTrialGossipFairy1", 277, addFairySummon),
                ("CollectableTheMoonGoronTrialGoronTrialGossipFairy2", 277, addFairySummon),
                ("CollectableTheMoonGoronTrialGoronTrialGossipFairy3", 277, addFairySummon),
                ("CollectableTheMoonGoronTrialGoronTrialGossipFairy4", 277, addFairySummon),
                ("CollectableTheMoonGoronTrialGoronTrialGossipFairy5", 277, addFairySummon),
                ("CollectableTheMoonLinkTrialGossipStoneRoom1GossipFairy1", 731, addFairySummon),
                ("CollectableTheMoonLinkTrialGossipStoneRoom2GossipFairy1", 281, addFairySummon),
                ("CollectableTheMoonLinkTrialIronKnuckleBattleGossipFairy1", 279, addFairySummon),
                ("CollectableTheMoonLinkTrialIronKnuckleBattleGossipFairy2", 279, addFairySummon),
                ("CollectableTheMoonLinkTrialPieceOfHeartRoomGossipFairy1", 279, addFairySummon),
                ("CollectableTheMoonZoraTrialZoraTrialGossipFairy1", 278, addFairySummon),
                ("CollectableTheMoonZoraTrialZoraTrialGossipFairy2", 278, addFairySummon),
                ("CollectableTheMoonZoraTrialZoraTrialGossipFairy3", 278, addFairySummon),
                ("CollectableTheMoonZoraTrialZoraTrialGossipFairy4", 278, addFairySummon),
                ("CollectableTheMoonZoraTrialZoraTrialGossipFairy5", 278, addFairySummon),
                ("CollectableZoraCapeGossipFairy1", 836, addFairySummon),
                ("CollectableGreatBayCoastButterflyFairy1", 207, null),
                ("CollectableGrottosOceanGossipStonesButterflyFairy1", 0, (item) =>
                {
                    addConditionalIfExists(logicObject, item, "Termina Field Boulder Clip");
                    addConditional(item, "MaskGoron");
                    addConditional(item, "OtherExplosive");
                }),
                ("CollectableGrottosMagicBeanSellerSGrottoButterflyFairy1", 6, null),
                ("CollectableGrottosCowGrottoButterflyFairy1", 292, removeEponasSong),
                ("CollectableGrottosCowGrottoButterflyFairy2", 294, removeEponasSong),
                ("CollectableMountainVillageWinterMountainVillageSpringButterflyFairy1", 1055, addDayOnly),
                ("CollectableMountainVillageWinterMountainVillageSpringButterflyFairy2", 1055, addDayOnly),
                ("CollectableTerminaFieldButterflyFairy1", 0, null),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames));

            var summonFairy = new JsonFormatLogicItem
            {
                Id = "Summon Fairy",
                RequiredItems = new List<string>(),
                ConditionalItems = new List<List<string>>(),
            };

            if (!addConditionalIfExists(logicObject, summonFairy, "Play Epona's Song"))
            {
                addConditional(summonFairy, "ItemOcarina", "SongEpona");
            }

            if (!addConditionalIfExists(logicObject, summonFairy, "Play Song of Healing"))
            {
                addConditional(summonFairy, "ItemOcarina", "SongHealing");
            }

            logicObject.Logic.Add(summonFairy);

            logicObject.Version = 16;
        }

        private static void AddFrogs(JsonFormatLogic logicObject)
        {
            const int startIndex = 1217;

            void addDonGeroMask(JsonFormatLogicItem item)
            {
                item.RequiredItems.Add("TODO");
            }
            var itemNames = new (string, int? reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("FrogWoodfallTemple", 59, null),
                ("FrogGreatBayTemple", 59, null),
                ("FrogSwamp", null, addDonGeroMask),
                ("FrogLaundryPool", null, addDonGeroMask),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames));

            var frogChoirLogic = logicObject.Logic[59];
            frogChoirLogic.RequiredItems = new List<string>
            {
                "AreaSnowheadTempleClear",
                "MaskDonGero",
                "FrogWoodfallTemple",
                "FrogGreatBayTemple",
                "FrogSwamp",
                "FrogLaundryPool",
            };
            frogChoirLogic.ConditionalItems = new List<List<string>>();

            logicObject.Version = 17;
        }

        private static void AddSettings(JsonFormatLogic logicObject)
        {
            const int startIndex = 136;
            var itemNames = new (string name, string referenceName, Action<JsonFormatLogicItem> modify)[]
            {
                ("SettingCloseCows", "Close Cows", null),
                ("SettingContinuousDekuHopping", null, null),
                ("SettingIronGoron", null, null),
                ("SettingClimbMostSurfaces", null, null),
                ("SettingFreeScarecrow", null, null),
                ("SettingGiantMaskAnywhere", null, null),
                //("SettingSaferGlitches", null, null),
                ("SettingBombchuDrops", null, null),
                ("SettingInstantTransform", null, null),
                ("SettingBombArrows", null, null),
                ("SettingNotFewerHealthDrops", null, null),
                ("SettingNotRandomizeEnemies", null, null),
                ("SettingStrayFairyModeChestsOnly", null, null),
                ("SettingNotStrayFairyModeChestsOnly", null, null),
                ("SettingNotRandomizedItemGreatBayBossKey", null, null),
                ("SettingNotRandomizedBottleCatchHotSpringWater", "Unrandomized Bottled Contents", null),
                ("SettingDamageModeDefault", "Take Damage", null),
                ("SettingNotDamageModeDouble", null, null),
                ("SettingNotDamageModeQuadruple", null, null),
                ("SettingNotDamageModeOHKO", null, null),
                ("SettingNotDamageModeDoom", null, null),
                ("SettingDamageEffectDefault", null, null),
                ("SettingNotDamageEffectFire", null, null),
                ("SettingNotDamageEffectIce", null, null),
                ("SettingNotDamageEffectShock", null, null),
                ("SettingNotDamageEffectKnockdown", null, null),
                ("SettingNotDamageEffectRandom", null, null),
                ("SettingMovementModeDefault", null, null),
                ("SettingMovementModeSuperLowGravity", null, null),
                ("SettingMovementModeLowGravity", null, null),
                ("SettingNotMovementModeHighSpeed", null, null),
                ("SettingNotMovementModeHighGravity", null, null),
                ("SettingFloorTypeDefault", null, null),
                ("SettingNotFloorTypeSand", null, null),
                ("SettingNotFloorTypeIce", null, null),
                ("SettingNotFloorTypeSnow", null, null),
                ("SettingNotFloorTypeRandom", null, null),
                ("SettingClockSpeedDefault", null, null),
                ("SettingNotClockSpeedFast", null, null),
                ("SettingNotClockSpeedVeryFast", null, null),
                ("SettingNotClockSpeedSuperFast", null, null),
                ("SettingBlastMaskCooldownInstant", "Instant Blast Mask Cooldown", null),
                ("SettingBlastMaskCooldownVeryShort", null, null),
                ("SettingEnableSunsSong", null, null),
                ("SettingAllowFierceDeityAnywhere", "Fierce Deity's Mask Anywhere", null),
                ("SettingNotByoAmmo", null, null),
                ("SettingNotDeathMoonCrash", "Death Warp", null),
                ("SettingHookshotAnySurface", null, null),
                ("SettingCharacterAdultLink", null, null),
                ("SettingNotCharacterAdultLink", null, null),
                ("SettingNotFixEponaSword", null, null)
                //("SettingSpeedupBank", "Faster Bank", null),
                //("SettingNotSpeedupBank", null, null),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames, true));

            foreach (var conditionals in logicObject.Logic.SelectMany(item => item.ConditionalItems))
            {
                if (conditionals.Contains("SettingNotDeathMoonCrash") && conditionals.Contains("SettingDamageModeDefault"))
                {
                    conditionals.Remove("SettingDamageModeDefault");
                }
            }

            foreach (var data in itemNames)
            {
                if (data.referenceName != null)
                {
                    void replaceInList(List<string> list)
                    {
                        var index = list.FindIndex(x => x == data.referenceName);
                        if (index != -1)
                        {
                            list[index] = data.name;
                        }
                    }

                    foreach (var item in logicObject.Logic)
                    {
                        replaceInList(item.RequiredItems);
                        item.ConditionalItems.ForEach(replaceInList);
                    }
                }
            }

            // delete entries marked for deletion
            logicObject.Logic.RemoveAll(item => item.Id == null);

            logicObject.Version = 18;
        }

        private static void AddWellFairies(JsonFormatLogic logicObject)
        {
            const int startIndex = 1269;

            void setWaterConditionals(JsonFormatLogicItem item)
            {
                item.ConditionalItems = new List<List<string>>
                {
                    new List<string>
                    {
                        "BottleCatchSpringWater"
                    },
                    new List<string>
                    {
                        "BottleCatchHotSpringWater"
                    }
                };
            }

            var itemNames = new (string name, int? reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("CollectableWellFountainFairy1", 270, setWaterConditionals),
                ("CollectableWellFountainFairy2", 270, setWaterConditionals),
                ("CollectableWellFountainFairy3", 270, setWaterConditionals),
                ("CollectableWellFountainFairy4", 270, setWaterConditionals),
                ("CollectableWellFountainFairy5", 270, setWaterConditionals),
                ("CollectableWellFountainFairy6", 270, setWaterConditionals),
                ("CollectableWellFountainFairy7", 270, setWaterConditionals),
                ("CollectableWellFountainFairy8", 270, setWaterConditionals),
            };

            var reference = logicObject.Logic[270];

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames));
            logicObject.Version = 19;
        }

        private static void AddInaccessible(JsonFormatLogic logicObject)
        {
            const int startIndex = 128;
            var itemNames = new string[]
            {
                "OtherInaccessible",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 20;
        }

        private static void ReplaceSettingsWithExpressions(JsonFormatLogic logicObject)
        {
            var mapping = new Dictionary<string, string>
            {
                { "SettingCloseCows", "settings.CloseCows" },
                { "SettingContinuousDekuHopping", "settings.ContinuousDekuHopping" },
                { "SettingIronGoron", "settings.IronGoron" },
                { "SettingClimbMostSurfaces", "settings.ClimbMostSurfaces" },
                { "SettingFreeScarecrow", "settings.FreeScarecrow" },
                { "SettingGiantMaskAnywhere", "settings.GiantMaskAnywhere" },
                { "SettingBombchuDrops", "settings.BombchuDrops" },
                { "SettingInstantTransform", "settings.InstantTransform" },
                { "SettingBombArrows", "settings.BombArrows" },
                { "SettingNotFewerHealthDrops", "!settings.FewerHealthDrops" },
                { "SettingNotRandomizeEnemies", "!settings.RandomizeEnemies" },
                { "SettingStrayFairyModeChestsOnly", "settings.StrayFairyMode.HasFlag(StrayFairyMode.ChestsOnly)" },
                { "SettingNotStrayFairyModeChestsOnly", "!settings.StrayFairyMode.HasFlag(StrayFairyMode.ChestsOnly)" },
                { "SettingNotRandomizedItemGreatBayBossKey", "!settings.CustomItemList.Contains(Item.ItemGreatBayBossKey)" },
                { "SettingNotRandomizedBottleCatchHotSpringWater", "!settings.CustomItemList.Contains(Item.BottleCatchHotSpringWater)" },
                { "SettingDamageModeDefault", "settings.DamageMode == DamageMode.Default" },
                { "SettingNotDamageModeDouble", "settings.DamageMode != DamageMode.Double" },
                { "SettingNotDamageModeQuadruple", "settings.DamageMode != DamageMode.Quadruple" },
                { "SettingNotDamageModeOHKO", "settings.DamageMode != DamageMode.OHKO" },
                { "SettingNotDamageModeDoom", "settings.DamageMode != DamageMode.Doom" },
                { "SettingDamageEffectDefault", "settings.DamageEffect == DamageEffect.Default" },
                { "SettingNotDamageEffectFire", "settings.DamageEffect != DamageEffect.Fire" },
                { "SettingNotDamageEffectIce", "settings.DamageEffect != DamageEffect.Ice" },
                { "SettingNotDamageEffectShock", "settings.DamageEffect != DamageEffect.Shock" },
                { "SettingNotDamageEffectKnockdown", "settings.DamageEffect != DamageEffect.Knockdown" },
                { "SettingNotDamageEffectRandom", "settings.DamageEffect != DamageEffect.Random" },
                { "SettingMovementModeDefault", "settings.MovementMode == MovementMode.Default" },
                { "SettingMovementModeSuperLowGravity", "settings.MovementMode == MovementMode.SuperLowGravity" },
                { "SettingMovementModeLowGravity", "settings.MovementMode == MovementMode.LowGravity" },
                { "SettingNotMovementModeHighSpeed", "settings.MovementMode != MovementMode.HighSpeed" },
                { "SettingNotMovementModeHighGravity", "settings.MovementMode != MovementMode.HighGravity" },
                { "SettingFloorTypeDefault", "settings.FloorType == FloorType.Default" },
                { "SettingNotFloorTypeSand", "settings.FloorType != FloorType.Sand" },
                { "SettingNotFloorTypeIce", "settings.FloorType != FloorType.Ice" },
                { "SettingNotFloorTypeSnow", "settings.FloorType != FloorType.Snow" },
                { "SettingNotFloorTypeRandom", "settings.FloorType != FloorType.Random" },
                { "SettingClockSpeedDefault", "settings.ClockSpeed == ClockSpeed.Default" },
                { "SettingNotClockSpeedFast", "settings.ClockSpeed != ClockSpeed.Fast" },
                { "SettingNotClockSpeedVeryFast", "settings.ClockSpeed != ClockSpeed.VeryFast" },
                { "SettingNotClockSpeedSuperFast", "settings.ClockSpeed != ClockSpeed.SuperFast" },
                { "SettingBlastMaskCooldownInstant", "settings.BlastMaskCooldown == BlastMaskCooldown.Instant" },
                { "SettingBlastMaskCooldownVeryShort", "settings.BlastMaskCooldown == BlastMaskCooldown.VeryShort" },
                { "SettingEnableSunsSong", "settings.EnableSunsSong" },
                { "SettingAllowFierceDeityAnywhere", "settings.AllowFierceDeityAnywhere" },
                { "SettingNotByoAmmo", "!settings.ByoAmmo" },
                { "SettingNotDeathMoonCrash", "!settings.DeathMoonCrash" },
                { "SettingHookshotAnySurface", "settings.HookshotAnySurface" },
                { "SettingCharacterAdultLink", "settings.Character == Character.AdultLink" },
                { "SettingNotCharacterAdultLink", "settings.Character != Character.AdultLink" },
                { "SettingNotFixEponaSword", "!settings.FixEponaSword" },
            };

            foreach (var (oldSetting, expression) in mapping)
            {
                if (logicObject.Logic.Any(x => x.RequiredItems.Contains(oldSetting) || x.ConditionalItems.Any(c => c.Contains(oldSetting))))
                {
                    var reference = logicObject.Logic.Single(x => x.Id == oldSetting);
                    logicObject.Logic.Add(new JsonFormatLogicItem
                    {
                        Id = oldSetting,
                        RequiredItems = reference.RequiredItems.ToList(),
                        ConditionalItems = reference.ConditionalItems.Select(c => c.ToList()).ToList(),
                        TimeAvailable = reference.TimeAvailable,
                        TimeNeeded = reference.TimeNeeded,
                        TimeSetup = reference.TimeSetup,
                        SettingExpression = expression,
                    });
                }
            }

            logicObject.Logic.RemoveRange(137, mapping.Count);

            logicObject.Version = 21;
        }

        private static void AddOtherCredits(JsonFormatLogic logicObject)
        {
            const int startIndex = 277;
            var itemNames = new (string name, Action<JsonFormatLogicItem> modify)[]
            {
                ("OtherCredits", (item) => item.RequiredItems = new List<string>() { "AreaMoonAccess" }),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 22;
        }

        private static void RemoveStoneTowerTemplePot(JsonFormatLogic logicObject)
        {
            logicObject.Logic.RemoveAt(1160);
            logicObject.Version = 23;
        }

        private static void AddOtherKillMajora(JsonFormatLogic logicObject)
        {
            const int startIndex = 277;
            var itemNames = new string[]
            {
                "OtherKillMajora",
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));

            logicObject.Logic[278].RequiredItems.Add("OtherKillMajora");
            logicObject.Version = 24;
        }

        private static void AddMoonFairies(JsonFormatLogic logicObject)
        {
            void addMoonAccess(JsonFormatLogicItem item)
            {
                item.RequiredItems = new List<string>() { "AreaMoonAccess" };
            }

            const int startIndex = 1219;
            var itemNames = new (string name, Action<JsonFormatLogicItem> modify)[]
            {
                ("CollectableMoonButterflyFairy1", addMoonAccess),
                ("CollectableMoonButterflyFairy2", addMoonAccess),
                ("CollectableMoonButterflyFairy3", addMoonAccess),
                ("CollectableMoonButterflyFairy4", addMoonAccess),
                ("CollectableMoonButterflyFairy5", addMoonAccess),
                ("CollectableMoonButterflyFairy6", addMoonAccess),
                ("CollectableMoonButterflyFairy7", addMoonAccess),
                ("CollectableMoonButterflyFairy8", addMoonAccess),
                ("CollectableMoonButterflyFairy9", addMoonAccess),
                ("CollectableMoonButterflyFairy10", addMoonAccess),
                ("CollectableMoonButterflyFairy11", addMoonAccess),
                ("CollectableMoonButterflyFairy12", addMoonAccess),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(itemNames));
            logicObject.Version = 25;
        }

        private static void AddPalmTrees(JsonFormatLogic logicObject)
        {
            void removeStrayFairiesAndExplosives(JsonFormatLogicItem item)
            {
                item.RequiredItems.RemoveAll((req) => req.Contains("StrayFairy"));
                item.RequiredItems.Remove("OtherExplosives");
            }

            void updateLowerTrees(JsonFormatLogicItem item)
            {
                removeStrayFairiesAndExplosives(item);
                if (item.ConditionalItems.RemoveAll((c) => c.Contains("Clever Ice Platforms")) > 0)
                {
                    item.ConditionalItems.Add(new List<string> { "Clever Ice Platforms" });
                }
                if (item.ConditionalItems.RemoveAll((c) => c.Contains("Ocean Great Fairy With Ice Arrows")) > 0)
                {
                    item.ConditionalItems.Add(new List<string> { "Zora Cape Lower Palm Trees With Ice Arrows" });
                }
            }

            var trickReference = logicObject.Logic.FirstOrDefault((item) => item.Id == "Ocean Great Fairy With Ice Arrows");
            if (trickReference != null)
            {
                var lowerTreesTrick = new JsonFormatLogicItem
                {
                    Id = "Zora Cape Lower Palm Trees With Ice Arrows",
                    RequiredItems = trickReference.RequiredItems.ToList(),
                    ConditionalItems = new List<List<string>>(),
                    IsTrick = trickReference.IsTrick,
                    TrickCategory = trickReference.TrickCategory,
                    TrickTooltip = trickReference.IsTrick ? "Create ice platforms with Ice Arrows and jump to the two lower platforms with the palm trees." : null,
                };
                logicObject.Logic.Add(lowerTreesTrick);
            }

            const int startIndex = 1243;
            var itemNames = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("CollectableGreatBayCoastPalmTree1", "CollectableGreatBayCoastPot1", null),
                ("CollectableGreatBayCoastPalmTree2", "CollectableGreatBayCoastPot1", null),
                ("CollectableGreatBayCoastPalmTree3", "CollectableGreatBayCoastPot1", null),
                ("CollectableZoraCapeShorePalmTree1", "CollectableZoraCapePot4", null),
                ("CollectableZoraCapeShorePalmTree2", "CollectableZoraCapePot4", null),
                ("CollectableZoraCapeRockPalmTree", "FairyDoubleDefense", removeStrayFairiesAndExplosives),
                ("CollectableZoraCapeScarecrowPalmTree", "FairyDoubleDefense", updateLowerTrees),
                ("CollectableZoraCapeLonePalmTree", "FairyDoubleDefense", updateLowerTrees),
            };

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames));
            logicObject.Version = 26;
        }

        private static void AddGibdos(JsonFormatLogic logicObject)
        {
            const int newGroupingsIndex = 124;
            var groupings = new (string name, Action<JsonFormatLogicItem> modify)[]
            {
                ("OtherAnyBombBag", (item) =>
                {
                    addConditional(item, "ItemBombBag");
                    addConditional(item, "UpgradeBigBombBag");
                    addConditional(item, "UpgradeBiggestBombBag");
                }
                ),
                ("OtherAnyBombchuPack", (item) =>
                {
                    addConditional(item, "ShopItemBombsBombchu10");
                    addConditional(item, "ChestInvertedStoneTowerBombchu10");
                    addConditional(item, "ChestLinkTrialBombchu10");
                }
                ),
                ("OtherAnyBottle", (item) =>
                {
                    addConditional(item, "ItemBottleWitch");
                    addConditional(item, "ItemBottleAliens");
                    addConditional(item, "ItemBottleBeavers");
                    addConditional(item, "ItemBottleDampe");
                    addConditional(item, "ItemBottleMadameAroma");
                    addConditional(item, "ItemBottleGoronRace");
                }
                ),
                ("OtherAnyRedPotion", (item) =>
                {
                    addConditional(item, "ItemBottleWitch");
                    addConditional(item, "OtherAnyBottle", "ShopItemTradingPostRedPotion");
                    addConditional(item, "OtherAnyBottle", "ShopItemWitchRedPotion");
                    addConditional(item, "OtherAnyBottle", "ShopItemGoronRedPotion");
                    addConditional(item, "OtherAnyBottle", "ShopItemZoraRedPotion");
                }
                ),
                ("OtherAnyGreenPotion", (item) =>
                {
                    addRequired(item, "OtherAnyBottle");
                    addConditional(item, "ShopItemTradingPostGreenPotion");
                    addConditional(item, "ShopItemWitchGreenPotion");
                    addConditional(item, "ShopItemBusinessScrubGreenPotion");
                }
                ),
                ("OtherAnyBluePotion", (item) =>
                {
                    addRequired(item, "OtherAnyBottle");
                    addConditional(item, "ShopItemWitchBluePotion");
                    addConditional(item, "ShopItemBusinessScrubBluePotion");
                }
                ),
                ("OtherAnyMilk", (item) =>
                {
                    addRequired(item, "OtherAnyBottle");
                    addConditional(item, "ItemBottleAliens");
                    addConditional(item, "ItemRanchBarnMainCowMilk");
                    addConditional(item, "ItemRanchBarnOtherCowMilk1");
                    addConditional(item, "ItemRanchBarnOtherCowMilk2");
                    addConditional(item, "ItemWellCowMilk");
                    addConditional(item, "ItemTerminaGrottoCowMilk1");
                    addConditional(item, "ItemTerminaGrottoCowMilk2");
                    addConditional(item, "ItemCoastGrottoCowMilk1");
                    addConditional(item, "ItemCoastGrottoCowMilk2");
                    addConditional(item, "ShopItemMilkBarMilk");
                    addConditional(item, "ShopItemGormanBrosMilk");
                }
                ),
            };

            var groupingItems = GetLogicItems(groupings);
            foreach (var groupingItem in groupingItems)
            {
                var existingGrouping = logicObject.Logic.FirstOrDefault(item => item.RequiredItems.SequenceEqualIgnoreOrder(groupingItem.RequiredItems)
                    && item.ConditionalItems.Count == groupingItem.ConditionalItems.Count
                    && item.ConditionalItems.All(c => groupingItem.ConditionalItems.Any(gc => c.SequenceEqualIgnoreOrder(gc))));
                if (existingGrouping != null)
                {
                    logicObject.Logic.Remove(existingGrouping);
                    foreach (var item in logicObject.Logic)
                    {
                        if (item.RequiredItems.Remove(existingGrouping.Id))
                        {
                            item.RequiredItems.Add(groupingItem.Id);
                        }

                        item.ConditionalItems.ForEach(c =>
                        {
                            if (c.Remove(existingGrouping.Id))
                            {
                                c.Add(groupingItem.Id);
                            }
                        });
                    }
                }
            }

            logicObject.Logic.InsertRange(newGroupingsIndex, GetLogicItems(groupings));

            const int startIndex = 1373;
            var itemNames = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("GibdoEntranceLeft", null, (item) => addRequired(item, "OtherAnyBluePotion")),
                ("GibdoEntranceRight", null, (item) => addRequired(item, "OtherLimitlessBeans")),
                ("GibdoToBombPots", null, (item) =>
                {
                    addConditional(item, "BottleCatchSpringWater");
                    addConditional(item, "BottleCatchHotSpringWater");
                }),
                ("GibdoToHotWater", null, (item) => addRequired(item, "BottleCatchFish")),
                ("GibdoToFairyFountain", null, (item) => addRequired(item, "BottleCatchBug")),
                ("GibdoToCowAndMiniboss", null, null),
                ("GibdoToMiniBoss", null, (item) => addRequired(item, "OtherAnyBombBag")),
                ("GibdoToCow", null, (item) => addRequired(item, "BottleCatchHotSpringWater")),
                ("GibdoToFinalWallmaster", null, (item) => addRequired(item, "BottleCatchBigPoe")),
                ("GibdoToMirrorShield", null, (item) => addRequired(item, "OtherAnyMilk")),
                ("GibdoToLeftChest", null, (item) => addRequired(item, "BottleCatchBug")),
                ("GibdoToRightChest", null, (item) => addRequired(item, "BottleCatchBug")),
                ("GibdoToBlackBoes", null, (item) => addRequired(item, "BottleCatchFish")),
            };

            foreach (var item in logicObject.Logic)
            {
                void checkList(List<string> items)
                {
                    if (items.Remove("BottleCatchBigPoe"))
                    {
                        items.Add("GibdoToFinalWallmaster");
                        if (items.Remove("BottleCatchFish"))
                        {
                            items.Add("GibdoToBlackBoes");
                        }
                    }
                    if (items.Remove("OtherAnyMilk"))
                    {
                        items.Add("GibdoToMirrorShield");
                    }
                    if (items.Remove("OtherLimitlessBeans"))
                    {
                        items.Add("GibdoEntranceRight");
                        if (items.Remove("BottleCatchFish"))
                        {
                            items.Add("GibdoToBlackBoes");
                        }
                    }
                    if (items.Remove("OtherAnyBombBag"))
                    {
                        items.Add("GibdoToMiniBoss");
                        items.Add("GibdoToCowAndMiniboss");
                    }
                    if (items.Remove("OtherAnyBluePotion"))
                    {
                        items.Add("GibdoEntranceLeft");
                        if (items.Remove("BottleCatchFish"))
                        {
                            items.Add("GibdoToHotWater");
                        }
                    }
                    if (items.Remove("BottleCatchHotSpringWater"))
                    {
                        items.Add("GibdoToCow");
                        items.Add("GibdoToCowAndMiniboss");
                    }
                }
                if (item.RequiredItems.Contains("MaskGibdo"))
                {
                    checkList(item.RequiredItems);
                }
                foreach (var conditionals in item.ConditionalItems)
                {
                    if (conditionals.Contains("MaskGibdo") || item.RequiredItems.Contains("MaskGibdo"))
                    {
                        var waterCheck = conditionals.ToList();
                        if (waterCheck.Remove("BottleCatchSpringWater"))
                        {
                            var hotWaterCheck = item.ConditionalItems.FirstOrDefault(c => c.Except(waterCheck).SequenceEqual(new List<string> { "BottleCatchHotSpringWater" }));
                            if (hotWaterCheck != null)
                            {
                                conditionals.Remove("BottleCatchSpringWater");
                                conditionals.Add("GibdoToBombPots");
                                hotWaterCheck.Clear();
                            }
                        }
                        checkList(conditionals);
                    }
                }
                item.ConditionalItems.RemoveAll(c => !c.Any());
                if (item.ConditionalItems.Any())
                {
                    var commonConditionals = item.ConditionalItems.Aggregate((a, b) => a.Intersect(b).ToList()).ToList();
                    if (commonConditionals.Any())
                    {
                        item.ConditionalItems.ForEach(cs => cs.RemoveAll(c => commonConditionals.Contains(c)));
                        item.RequiredItems.AddRange(commonConditionals);
                    }
                }
                item.ConditionalItems.RemoveAll(c => !c.Any());
                if (item.Id == "ChestWellRightPurpleRupee")
                {
                    if (item.RequiredItems.Remove("BottleCatchBug"))
                    {
                        item.RequiredItems.Add("GibdoToRightChest");
                    }
                }
                if (item.Id == "ChestWellLeftPurpleRupee")
                {
                    if (item.RequiredItems.Remove("BottleCatchBug"))
                    {
                        item.RequiredItems.Add("GibdoToLeftChest");
                    }
                }
                if (item.Id.StartsWith("CollectableWellFountainFairy"))
                {
                    if (item.RequiredItems.Remove("BottleCatchBug"))
                    {
                        item.RequiredItems.Add("GibdoToFairyFountain");
                    }
                }
            }

            logicObject.Logic.InsertRange(startIndex, GetLogicItems(logicObject, itemNames));
            logicObject.Version = 27;
        }

        private static void AddGrottos(JsonFormatLogic logicObject)
        {
            var itemNames = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("GrottoGossipOcean", "CollectableGrottosOceanGossipStonesButterflyFairy1", null),
                ("GrottoGossipSwamp", null, null),
                ("GrottoGossipCanyon", null, null),
                ("GrottoGossipMountain", null, null),
                ("GrottoGenericGreatBayCoast", "ChestGreatBayCoastGrotto", null),
                ("GrottoGenericMountainVillageSpring", "ChestMountainVillageGrottoRedRupee", null),
                ("GrottoGenericSouthernSwamp", "ChestSwampGrotto", null),
                ("GrottoGenericRoadToSwamp", null, null),
                ("GrottoGenericTerminaFieldGrass", null, null),
                ("GrottoGenericIkanaCanyon", "ChestIkanaSecretShrineGrotto", null),
                ("GrottoGenericWoodsOfMystery", null, (item) => item.addTimeAvailable(TimeOfDay.Day2 | TimeOfDay.Night2)),
                ("GrottoGenericZoraCape", "ChestGreatBayCapeGrotto", null),
                ("GrottoGenericRoadToIkana", "ChestToIkanaGrotto", null),
                ("GrottoGenericTerminaFieldPillar", null, null),
                ("GrottoGenericIkanaGraveyard", "ChestGraveyardGrotto", null),
                ("GrottoGenericPathToSnowhead", "ChestToSnowheadGrotto", null),
                ("GrottoGenericTwinIslands", "ChestToGoronRaceGrotto", null),
                ("GrottoHotSpring", "ChestHotSpringGrottoRedRupee", (logicItem) =>
                {
                    removeConditionalItemsContainingText(logicItem, "Explosive");
                    removeConditionalItemsContainingText(logicItem, "Bomb");
                    removeConditionalItemsContainingText(logicItem, "Blast");
                    removeConditionalItemsContainingText(logicItem, "Keg");
                    removeConditionalItemsContainingText(logicItem, "Boulder");
                    cleanUpConditionals(logicItem);
                }
                ),
                ("GrottoDodongo", null, null),
                ("GrottoDekuMerchant", null, null),
                ("GrottoCowGreatBayCoast", "CollectableGrottosCowGrottoButterflyFairy2", null),
                ("GrottoCowTerminaField", "CollectableGrottosCowGrottoButterflyFairy1", null),
                ("GrottoBioBaba", "CollectableGrottosOceanGossipStonesButterflyFairy1", null),
                ("GrottoBeanSeller", "ItemMagicBean", null),
                ("GrottoPeahat", null, (item) => item.addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("GrottoDekuPlayground", null, (item) => item.addTimeNeeded(TimeOfDay.Day1)),
            };

            logicObject.Logic.InsertRange(144, GetLogicItems(logicObject, itemNames));

            JsonFormatLogicItem getItem(string id) => logicObject.Logic.Single(logicItem => logicItem.Id == id);

            void replaceWithRequiredItems(JsonFormatLogicItem logicItem, params string[] requiredItems)
            {
                logicItem.RequiredItems.Clear();
                logicItem.ConditionalItems.Clear();
                addRequired(logicItem, requiredItems);
            }

            void replaceRequiredItems(JsonFormatLogicItem logicItem, params string[] requiredItems)
            {
                logicItem.RequiredItems.Clear();
                addRequired(logicItem, requiredItems);
            }

            void clearConditionalsAndAddRequiredItems(JsonFormatLogicItem logicItem, params string[] requiredItems)
            {
                logicItem.ConditionalItems.Clear();
                addRequired(logicItem, requiredItems);
            }

            replaceWithRequiredItems(getItem("CollectableGrottosOceanGossipStonesButterflyFairy1"), "GrottoGossipOcean");
            replaceWithRequiredItems(getItem("ChestGreatBayCoastGrotto"), "GrottoGenericGreatBayCoast");
            replaceWithRequiredItems(getItem("ChestMountainVillageGrottoRedRupee"), "GrottoGenericMountainVillageSpring");
            replaceWithRequiredItems(getItem("ChestSwampGrotto"), "GrottoGenericSouthernSwamp");
            replaceWithRequiredItems(getItem("ChestToSwampGrotto"), "GrottoGenericRoadToSwamp");
            replaceWithRequiredItems(getItem("ChestTerminaGrottoRedRupee"), "GrottoGenericTerminaFieldGrass");
            replaceWithRequiredItems(getItem("ChestIkanaSecretShrineGrotto"), "GrottoGenericIkanaCanyon");
            replaceWithRequiredItems(getItem("ChestWoodsGrotto"), "GrottoGenericWoodsOfMystery");
            getItem("ChestWoodsGrotto").TimeAvailable = TimeOfDay.None;
            replaceWithRequiredItems(getItem("ChestGreatBayCapeGrotto"), "GrottoGenericZoraCape");
            replaceWithRequiredItems(getItem("ChestToIkanaGrotto"), "GrottoGenericRoadToIkana");
            replaceWithRequiredItems(getItem("ChestTerminaGrottoBombchu"), "GrottoGenericTerminaFieldPillar");
            replaceWithRequiredItems(getItem("ChestGraveyardGrotto"), "GrottoGenericIkanaGraveyard");
            replaceWithRequiredItems(getItem("ChestToSnowheadGrotto"), "GrottoGenericPathToSnowhead");
            replaceWithRequiredItems(getItem("ChestToGoronRaceGrotto"), "GrottoGenericTwinIslands");
            var hotSpringGrottoChest = getItem("ChestHotSpringGrottoRedRupee");
            hotSpringGrottoChest.RequiredItems.Clear();
            addRequired(hotSpringGrottoChest, "GrottoHotSpring");
            removeConditionalsContainingText(hotSpringGrottoChest, "Challenge");
            removeConditionalItemsContainingText(hotSpringGrottoChest, "Water");
            removeConditionalItemsContainingText(hotSpringGrottoChest, "Soaring");
            removeConditionalItemsContainingText(hotSpringGrottoChest, "Temple");
            removeConditionalItemsContainingText(hotSpringGrottoChest, "Fire");
            removeConditionalsContainingText(hotSpringGrottoChest, "Action Swap");
            removeConditionalsContainingText(hotSpringGrottoChest, "Recoil Flip");
            cleanUpConditionals(hotSpringGrottoChest);
            addRequired(getItem("HeartPieceDodong"), "GrottoDodongo");
            addRequired(getItem("HeartPieceTerminaBusinessScrub"), "GrottoDekuMerchant");
            clearConditionalsAndAddRequiredItems(getItem("ItemCoastGrottoCowMilk1"), "GrottoCowGreatBayCoast");
            clearConditionalsAndAddRequiredItems(getItem("ItemCoastGrottoCowMilk2"), "GrottoCowGreatBayCoast");
            replaceWithRequiredItems(getItem("CollectableGrottosCowGrottoButterflyFairy2"), "GrottoCowGreatBayCoast");
            replaceWithRequiredItems(getItem("ItemTerminaGrottoCowMilk1"), "GrottoCowTerminaField", "Play Epona's Song");
            replaceWithRequiredItems(getItem("ItemTerminaGrottoCowMilk2"), "GrottoCowTerminaField", "Play Epona's Song");
            replaceWithRequiredItems(getItem("CollectableGrottosCowGrottoButterflyFairy1"), "GrottoCowTerminaField");
            replaceRequiredItems(getItem("HeartPieceZoraGrotto"), "GrottoBioBaba");
            replaceRequiredItems(getItem("CollectableGrottosOceanHeartPieceGrottoBeehive1"), "GrottoBioBaba");
            replaceRequiredItems(getItem("ItemMagicBean"), "GrottoBeanSeller");
            replaceRequiredItems(getItem("ChestBeanGrottoRedRupee"), "GrottoBeanSeller");
            replaceRequiredItems(getItem("CollectableBeanGrottoSoftSoil1"), "GrottoBeanSeller", "BottleCatchBug");
            replaceRequiredItems(getItem("CollectableGrottosMagicBeanSellerSGrottoButterflyFairy1"), "GrottoBeanSeller");
            addRequired(getItem("HeartPiecePeahat"), "GrottoPeahat");
            addRequired(getItem("HeartPieceDekuPlayground"), "GrottoDekuPlayground");
            addRequired(getItem("MundaneItemDekuPlaygroundPurpleRupee"), "GrottoDekuPlayground");

            Action<JsonFormatLogicItem> updateGossipStones = (logicItem) =>
            {
                logicItem.RequiredItems.Clear();
                addRequired(logicItem, "GrottoGossipOcean", "GrottoGossipSwamp", "GrottoGossipCanyon", "GrottoGossipMountain");
            };

            var newMultilocations = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("HeartPieceTerminaGossipStonesInSwampGossipGrotto", "HeartPieceTerminaGossipStones", updateGossipStones),
                ("HeartPieceTerminaGossipStonesInMountainGossipGrotto", "HeartPieceTerminaGossipStones", updateGossipStones),
                ("HeartPieceTerminaGossipStonesInOceanGossipGrotto", "HeartPieceTerminaGossipStones", updateGossipStones),
                ("HeartPieceTerminaGossipStonesInCanyonGossipGrotto", "HeartPieceTerminaGossipStones", updateGossipStones),
                ("CollectableGrottosOceanGossipStonesGossipFairy1InSwampGossipGrotto", "CollectableGrottosOceanGossipStonesGossipFairy1", (logicItem) => addRequired(logicItem, "GrottoGossipSwamp")),
                ("CollectableGrottosOceanGossipStonesGossipFairy1InMountainGossipGrotto", "CollectableGrottosOceanGossipStonesGossipFairy1", (logicItem) => addRequired(logicItem, "GrottoGossipMountain")),
                ("CollectableGrottosOceanGossipStonesGossipFairy1InOceanGossipGrotto", "CollectableGrottosOceanGossipStonesGossipFairy1", (logicItem) => addRequired(logicItem, "GrottoGossipOcean")),
                ("CollectableGrottosOceanGossipStonesGossipFairy1InCanyonGossipGrotto", "CollectableGrottosOceanGossipStonesGossipFairy1", (logicItem) => addRequired(logicItem, "GrottoGossipCanyon")),
            };

            logicObject.Logic.InsertRange(1299, GetLogicItems(logicObject, newMultilocations));

            var gossipStoneHp = getItem("HeartPieceTerminaGossipStones");
            var gossipStoneHpFairy = getItem("CollectableGrottosOceanGossipStonesGossipFairy1");

            gossipStoneHp.RequiredItems.Clear();
            gossipStoneHp.ConditionalItems.Clear();
            addConditional(gossipStoneHp, "HeartPieceTerminaGossipStonesInSwampGossipGrotto");
            addConditional(gossipStoneHp, "HeartPieceTerminaGossipStonesInMountainGossipGrotto");
            addConditional(gossipStoneHp, "HeartPieceTerminaGossipStonesInOceanGossipGrotto");
            addConditional(gossipStoneHp, "HeartPieceTerminaGossipStonesInCanyonGossipGrotto");

            gossipStoneHpFairy.RequiredItems.Clear();
            gossipStoneHpFairy.ConditionalItems.Clear();
            addConditional(gossipStoneHpFairy, "CollectableGrottosOceanGossipStonesGossipFairy1InSwampGossipGrotto");
            addConditional(gossipStoneHpFairy, "CollectableGrottosOceanGossipStonesGossipFairy1InMountainGossipGrotto");
            addConditional(gossipStoneHpFairy, "CollectableGrottosOceanGossipStonesGossipFairy1InOceanGossipGrotto");
            addConditional(gossipStoneHpFairy, "CollectableGrottosOceanGossipStonesGossipFairy1InCanyonGossipGrotto");

            getItem("GossipTerminaGossipLarge")
                .addConditional("GrottoGossipSwamp")
                .addConditional("GrottoGossipMountain")
                .addConditional("GrottoGossipOcean")
                .addConditional("GrottoGossipCanyon");

            getItem("GossipTerminaGossipGuitar")
                .addConditional("GrottoGossipSwamp")
                .addConditional("GrottoGossipMountain")
                .addConditional("GrottoGossipOcean")
                .addConditional("GrottoGossipCanyon");

            getItem("GossipTerminaGossipPipes")
                .addConditional("GrottoGossipSwamp")
                .addConditional("GrottoGossipMountain")
                .addConditional("GrottoGossipOcean")
                .addConditional("GrottoGossipCanyon");

            getItem("GossipTerminaGossipDrums")
                .addConditional("GrottoGossipSwamp")
                .addConditional("GrottoGossipMountain")
                .addConditional("GrottoGossipOcean")
                .addConditional("GrottoGossipCanyon");

            logicObject.Version = 28;
        }

        private static void AddInteriors(JsonFormatLogic logicObject)
        {
            var newGroupings = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("TimeDayThreeNightThree", null, (item) => item.addTimeAvailable(TimeOfDay.Day3 | TimeOfDay.Night3)),
                ("OtherMilkBarDay", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("OtherMilkBarNight1Or2", null, (item) => item.addRequired("MaskRomani").addTimeAvailable(TimeOfDay.Night1 | TimeOfDay.Night2)),
                ("OtherMilkBarNight3", null, (item) => item.addTimeAvailable(TimeOfDay.Night3)),
                ("OtherPostOfficeNormal", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Night1 | TimeOfDay.Night3)),
                ("OtherPostOfficeDay2", null, (item) => item.addRequired("TradeItemKafeiLetter").addTimeAvailable(TimeOfDay.Day2 | TimeOfDay.Night2)),
                ("OtherRomaniInHouseNight1", null, (item) => item.addTimeAvailable(TimeOfDay.Night1)),
                ("OtherRomaniInHouseNight2Or3", "ItemBottleAliens", (item) => item.addTimeAvailable(TimeOfDay.Night2 | TimeOfDay.Night3)),
            };

            logicObject.Logic.InsertRange(122, GetLogicItems(logicObject, newGroupings));

            JsonFormatLogicItem getItem(string id) => logicObject.Logic.SingleOrDefault(logicItem => logicItem.Id == id);

            getItem("OtherEpona").addConditional("TimeDayThreeNightThree");

            var itemNames = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("InteriorMayorsResidence", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3).addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Day2)),
                ("InteriorPotionShop", null, (item) => item.addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Night1)),
                ("InteriorRanchBarn", null, (item) => item.addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3)),
                ("InteriorRanchHouse", null, (item) => item.addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorHoneyAndDarling", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3).addTimeNeeded(TimeOfDay.Day1)),
                ("InteriorCuriosityShop", null, (item) => item.addTimeAvailable(TimeOfDay.Night1 | TimeOfDay.Night2 | TimeOfDay.Night3).addTimeNeeded(TimeOfDay.Night1 | TimeOfDay.Night2 | TimeOfDay.Night3)),
                ("InteriorMilkBar", null, (item) => item.addConditional("OtherMilkBarDay").addConditional("OtherMilkBarNight1Or2").addConditional("OtherMilkBarNight3")),
                ("InteriorTreasureChestShop", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3).addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorTownShootingGallery", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorSwampShootingGallery", null, (item) => item.addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorMountainSmithy", "CollectableMountainVillageWinterSmallSnowball1", (item) => item.addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Night1)),
                ("InteriorPostOffice", null, (item) => item.addConditional("OtherPostOfficeNormal").addConditional("OtherPostOfficeDay2").addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Night1)),
                ("InteriorMarineResearchLab", "CollectableGreatBayCoastPot1", null),
                ("InteriorTradingPost", null, null),
                ("InteriorLotteryShop", null, (item) => item.addTimeNeeded(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorDoggyRacetrack", null, (item) => item.addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorCuccoShack", null, (item) => item.addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3)),
                ("InteriorMikauTijoRoom", null, (item) => item.addRequired("AreaWestAccess").addRequired("MaskZora")),
                ("InteriorJapasRoom", null, (item) => item.addRequired("AreaWestAccess").addRequired("MaskZora")),
                ("InteriorLuluRoom", null, (item) => item.addRequired("AreaWestAccess").addRequired("MaskZora")),
                ("InteriorEvanRoom", null, (item) => item.addRequired("AreaWestAccess").addRequired("MaskZora")),
                ("InteriorZoraShop", "ShopItemZoraShield", null),
                ("InteriorSwordsmanSchool", null, null),
                ("InteriorMusicBoxHouse", "MaskGibdo", (JsonFormatLogicItem item) =>
                {
                    item.ConditionalItems.ForEach(cs =>
                    {
                        if (cs.Any(x => x.Contains("Storms")))
                        {
                            cs.Add("InteriorIkanaPoolCave");
                        }
                    });
                    if (getItem("Play Song of Storms") != null)
                    {
                        if (item.RequiredItems.Remove("Play Song of Storms"))
                        {
                            item.addConditional("Play Song of Storms", "InteriorIkanaPoolCave");
                        }
                    }
                    else
                    {
                        if (item.RequiredItems.Remove("SongStorms") && item.RequiredItems.Remove("ItemOcarina"))
                        {
                            item.addConditional("ItemOcarina", "SongStorms", "InteriorIkanaPoolCave");
                        }
                    }
                    if (getItem("Play Song of Healing") != null)
                    {
                        item.RequiredItems.Remove("Play Song of Healing");
                        item.addConditional("Play Song of Healing", "InteriorMusicBoxHouse");
                    }
                    else
                    {
                        item.RequiredItems.Remove("SongHealing");
                        item.RequiredItems.Remove("ItemOcarina");
                        item.addConditional("SongHealing", "ItemOcarina", "InteriorMusicBoxHouse");
                    }
                    item.addConditional("AreaStoneTowerClear", "InteriorMusicBoxHouse");
                }),
                ("InteriorBombShop", null, null),
                ("InteriorLensCave", "ItemLens", null),
                ("InteriorIkanaPoolCave", "CollectableIkanaCanyonMainAreaGrass1", null),
                ("InteriorPinnacleRock", "CollectableGreatBayCoastPot1", null),
                ("InteriorFairyFountainTown", null, null),
                ("InteriorFairyFountainWoodfall", "FairySpinAttack", (item) => item.RequiredItems.RemoveAll((req) => req.Contains("StrayFairy"))),
                ("InteriorFairyFountainSnowhead", "FairyDoubleMagic", (item) => item.RequiredItems.RemoveAll((req) => req.Contains("StrayFairy"))),
                ("InteriorFairyFountainZoraCape", "FairyDoubleDefense", (item) => item.RequiredItems.RemoveAll((req) => req.Contains("StrayFairy"))),
                ("InteriorFairyFountainIkanaCanyon", "ItemFairySword", (item) => item.RequiredItems.RemoveAll((req) => req.Contains("StrayFairy"))),
                ("InteriorFishermanHut", "GrottoGenericGreatBayCoast", null),
                ("InteriorGoronShop", "ShopItemGoronRedPotionInWinter", null),
                ("InteriorWaterfallRapids", "ItemBottleBeavers", (item) => item.RequiredItems.Remove("MaskZora")),
                ("InteriorGoronGraveyard", "CollectableMountainVillageWinterSmallSnowball3", null),
                ("InteriorPoeHut", "CollectableIkanaCanyonMainAreaGrass1", null),
                ("InteriorDekuShrine", "CollectableDekuPalaceRupeeCluster1", (item) =>
                {
                    var conditionals = false;
                    if (getItem("Kill Deku Shrine Big Octo") != null && getItem("SettingDamageModeDefault") != null)
                    {
                        conditionals = true;
                        item.addConditional("Kill Deku Shrine Big Octo", "OtherArrow", "SettingDamageModeDefault");
                        item.addConditional("Kill Deku Shrine Big Octo", "MaskZora", "SettingDamageModeDefault");
                        item.addConditional("Kill Deku Shrine Big Octo", "ItemHookshot", "SettingDamageModeDefault");
                    }

                    if (getItem("Hookshot Clip") != null)
                    {
                        conditionals = true;
                        item.addConditional("Hookshot Clip");
                    }

                    if (conditionals)
                    {
                        item.addConditional("AreaWoodFallTempleClear");
                    }
                    else
                    {
                        item.addRequired("AreaWoodFallTempleClear");
                    }
                }),
                ("InteriorSecretShrine", "GrottoGenericIkanaCanyon", null),
                ("InteriorWoodsOfMystery", null, null),
                ("InteriorGoronRacetrack", "CollectableGoronRacetrackPot1", null),
                ("InteriorStoneTowerTemple", "AreaStoneTowerTempleAccess", null),
                ("InteriorSwampSpiderHouse", "AreaSwampSpiderAccess", null),
                ("InteriorOceanSpiderHouse", "GrottoGenericGreatBayCoast", null),
            };

            logicObject.Logic.InsertRange(170 + newGroupings.Length, GetLogicItems(logicObject, itemNames));

            var newMultilocations1 = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("NotebookMeetRomaniInRanch", null, (item) => item.addRequired("ItemNotebook").addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day3)),
                ("NotebookMeetRomaniInHouse", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorRanchHouse").addConditional("OtherRomaniInHouseNight1").addConditional("OtherRomaniInHouseNight2Or3")),
                ("NotebookMeetRomaniInBarn", "ItemBottleAliens", (item) => item.addRequired("ItemNotebook").addRequired("InteriorRanchBarn").addTimeAvailable(TimeOfDay.Day2 | TimeOfDay.Night3)),
                ("NotebookMeetCremiaInRanch", null, (item) => item.addRequired("ItemNotebook").addRequired("OtherEpona").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Night2 | TimeOfDay.Day3)),
                ("NotebookMeetCremiaInHouse", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorRanchHouse").addTimeAvailable(TimeOfDay.Night1 | TimeOfDay.Night2 | TimeOfDay.Night3)),
                ("NotebookMeetCremiaInBarn", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorRanchBarn").addTimeAvailable(TimeOfDay.Day2 | TimeOfDay.Day3 | TimeOfDay.Night3)),
                ("NotebookMeetMadameAromaInOffice", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMayorsResidence").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2)),
                ("NotebookMeetMadameAromaInBar", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMilkBar").addTimeAvailable(TimeOfDay.Night3)),
                ("NotebookMeetTotoInOffice", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMayorsResidence").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Day2)),
                ("NotebookMeetTotoInBar", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMilkBar").addTimeAvailable(TimeOfDay.Night1 | TimeOfDay.Night2)),
            };

            logicObject.Logic.InsertRange(1389 + newGroupings.Length, GetLogicItems(logicObject, newMultilocations1));

            var newMultilocations2 = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("NotebookMeetGormanInOffice", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMayorsResidence").addTimeAvailable(TimeOfDay.Day1)),
                ("NotebookMeetGormanInBar", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMilkBar").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Night1 | TimeOfDay.Night2)),
            };

            logicObject.Logic.InsertRange(1401 + newGroupings.Length, GetLogicItems(logicObject, newMultilocations2));

            var newMultilocations3 = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("NotebookMeetPostmanInPostOffice", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorPostOffice")),
                ("NotebookMeetPostmanInMilkBar", null, (item) => item.addRequired("ItemNotebook").addRequired("InteriorMilkBar").addTimeAvailable(TimeOfDay.Night3)),
            };

            logicObject.Logic.InsertRange(1409 + newGroupings.Length, GetLogicItems(logicObject, newMultilocations3));

            var newMultilocations4 = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("ItemBottleWitchInWoodsOfMystery", null, (item) => item.addRequired("InteriorWoodsOfMystery").addTimeAvailable(TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3)),
                ("ItemBottleWitchInPotionShop", null, (item) => item.addRequired("InteriorPotionShop").addRequired("InteriorWoodsOfMystery").addTimeAvailable(TimeOfDay.Day1 | TimeOfDay.Night1)),
                ("UpgradeBigBombBagInBombShop", "UpgradeBigBombBag", (item) => item.addRequired("InteriorBombShop")),
                ("UpgradeBigBombBagInCuriosityShop", null, (item) => item.addRequired("InteriorCuriosityShop").addRequired("Any Wallet").addTimeAvailable(TimeOfDay.Night3)),
            };

            logicObject.Logic.InsertRange(1422 + newGroupings.Length, GetLogicItems(logicObject, newMultilocations4));

            getItem("NotebookMeetRomani")
                .clear()
                .addConditional("NotebookMeetRomaniInRanch")
                .addConditional("NotebookMeetRomaniInHouse")
                .addConditional("NotebookMeetRomaniInBarn");
            getItem("NotebookMeetCremia")
                .clear()
                .addConditional("NotebookMeetCremiaInRanch")
                .addConditional("NotebookMeetCremiaInHouse")
                .addConditional("NotebookMeetCremiaInBarn");
            getItem("NotebookMeetMadameAroma")
                .clear()
                .addConditional("NotebookMeetMadameAromaInOffice")
                .addConditional("NotebookMeetMadameAromaInBar");
            getItem("NotebookMeetToto")
                .clear()
                .addConditional("NotebookMeetTotoInOffice")
                .addConditional("NotebookMeetTotoInBar");
            getItem("NotebookMeetGorman")
                .addConditional("NotebookMeetGormanInOffice")
                .addConditional("NotebookMeetGormanInBar");
            getItem("NotebookMeetPostman")
                .addConditional("NotebookMeetPostmanInPostOffice")
                .addConditional("NotebookMeetPostmanInMilkBar");
            getItem("ItemBottleWitch")
                .addConditional("ItemBottleWitchInWoodsOfMystery")
                .addConditional("ItemBottleWitchInPotionShop");
            getItem("UpgradeBigBombBag")
                .clear()
                .addConditional("UpgradeBigBombBagInBombShop")
                .addConditional("UpgradeBigBombBagInCuriosityShop");

            getItem("HeartPieceNotebookMayor").addRequired("InteriorMayorsResidence");
            getItem("NotebookMeetMayorDotour").addRequired("InteriorMayorsResidence");
            getItem("NotebookDotoursThanks").addRequired("InteriorMayorsResidence");
            getItem("MaskKafei").addRequired("InteriorMayorsResidence");
            getItem("NotebookPromiseMadameAroma").addRequired("InteriorMayorsResidence");

            getItem("ShopItemWitchBluePotion").addRequired("InteriorPotionShop");
            getItem("ShopItemWitchRedPotion").addRequired("InteriorPotionShop");
            getItem("ShopItemWitchGreenPotion").addRequired("InteriorPotionShop");
            getItem("MundaneItemKotakeMushroomSaleRedRupee").addRequired("InteriorPotionShop");
            getItem("CollectableMagicHagsPotionShopItem1").addRequired("InteriorPotionShop");

            getItem("ItemRanchBarnMainCowMilk").removeRequired("OtherEpona").addRequired("InteriorRanchBarn").addConditional("OtherRomaniInHouseNight1").addConditional("OtherRomaniInHouseNight2Or3");
            getItem("ItemRanchBarnOtherCowMilk1").removeRequired("OtherEpona").addRequired("InteriorRanchBarn").addConditional("OtherRomaniInHouseNight1").addConditional("OtherRomaniInHouseNight2Or3");
            var cornerCow = getItem("ItemRanchBarnOtherCowMilk2").removeRequired("OtherEpona").addRequired("InteriorRanchBarn");
            if (!cornerCow.ConditionalItems.Any())
            {
                cornerCow.ConditionalItems.Add(new List<string>());
            }
            cornerCow.ConditionalItems = cornerCow.ConditionalItems.SelectMany(cs => new List<string> { "OtherRomaniInHouseNight1", "OtherRomaniInHouseNight2Or3" }.Select(x => cs.Append(x).ToList())).ToList();
            getItem("CollectableRanchHouseBarnBarnItem1").clear().addRequired("InteriorRanchBarn");
            getItem("CollectableRanchHouseBarnBarnItem2").clear().addRequired("InteriorRanchBarn");

            getItem("NotebookMeetAnjusGrandmotherInRanch").addRequired("InteriorRanchHouse");

            getItem("HeartPieceHoneyAndDarling").addRequired("InteriorHoneyAndDarling");
            getItem("MundaneItemHoneyAndDarlingPurpleRupee").addRequired("InteriorHoneyAndDarling");

            getItem("MaskAllNight").addRequired("InteriorCuriosityShop");
            getItem("MundaneItemCuriosityShopBlueRupee").addRequired("InteriorCuriosityShop");
            getItem("MundaneItemCuriosityShopRedRupee").addRequired("InteriorCuriosityShop");
            getItem("MundaneItemCuriosityShopPurpleRupee").addRequired("InteriorCuriosityShop");
            getItem("MundaneItemCuriosityShopGoldRupee").addRequired("InteriorCuriosityShop");
            getItem("NotebookPurchaseCuriosityShopItem").addRequired("InteriorCuriosityShop");
            getItem("NotebookMeetCuriosityShopManInWCT").addRequired("InteriorCuriosityShop");

            getItem("ItemBottleMadameAroma").addRequired("InteriorMilkBar");
            getItem("MaskCircusLeader").removeRequired("MaskRomani").addRequired("InteriorMilkBar");
            getItem("ShopItemMilkBarChateau").addRequired("InteriorMilkBar");
            getItem("ShopItemMilkBarMilk").addRequired("InteriorMilkBar");
            getItem("NotebookDeliverLetterToMama").addRequired("InteriorMilkBar");
            getItem("NotebookMovingGorman").removeRequired("MaskRomani").addRequired("InteriorMilkBar");

            getItem("HeartPieceTreasureChestGame").addRequired("InteriorTreasureChestShop");
            getItem("MundaneItemTreasureChestGamePurpleRupee").addRequired("InteriorTreasureChestShop");
            getItem("MundaneItemTreasureChestGameRedRupee").addRequired("InteriorTreasureChestShop");
            getItem("MundaneItemTreasureChestGameDekuNuts").addRequired("InteriorTreasureChestShop");

            getItem("UpgradeBigQuiver").addRequired("InteriorTownShootingGallery");
            getItem("HeartPieceTownArchery").addRequired("InteriorTownShootingGallery");

            getItem("UpgradeBiggestQuiver").addRequired("InteriorSwampShootingGallery");
            getItem("HeartPieceSwampArchery").addRequired("InteriorSwampShootingGallery");

            getItem("UpgradeRazorSword").removeRequired("AreaNorthAccess").addRequired("InteriorMountainSmithy");
            getItem("UpgradeGildedSword").removeRequired("AreaNorthAccess").addRequired("InteriorMountainSmithy");

            getItem("HeartPieceNotebookPostman").addRequired("InteriorPostOffice");
            getItem("NotebookPostmansGame").addRequired("InteriorPostOffice");

            getItem("SongNewWaveBossaNova").removeRequired("AreaWestAccess").addRequired("InteriorMarineResearchLab");
            getItem("HeartPieceLabFish").removeRequired("AreaWestAccess").addRequired("InteriorMarineResearchLab");

            getItem("ShopItemTradingPostRedPotion").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostGreenPotion").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostShield").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostFairy").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostStick").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostArrow30").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostNut10").addRequired("InteriorTradingPost");
            getItem("ShopItemTradingPostArrow50").addRequired("InteriorTradingPost");

            getItem("MundaneItemLotteryPurpleRupee").addRequired("InteriorLotteryShop");

            getItem("HeartPieceDogRace").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("ChestDogRacePurpleRupee").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackPot1").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackPot2").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackPot3").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackPot4").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackSoftSoil1").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableDoggyRacetrackGossipFairy1").addRequired("InteriorDoggyRacetrack").TimeAvailable = TimeOfDay.None;
            getItem("GossipRanchRacetrack").addRequired("InteriorDoggyRacetrack");

            getItem("MaskBunnyHood").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackWoodenCrateLarge1").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag1").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag2").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag3").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag4").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag5").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackHitTag6").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackPottedPlant1").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("NotebookMeetGrog").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("NotebookGrogsThanks").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("CollectableCuccoShackGossipFairy1").addRequired("InteriorCuccoShack").TimeAvailable = TimeOfDay.None;
            getItem("GossipRanchCuccoShack").addRequired("InteriorCuccoShack");

            getItem("TradeItemOceanDeed").removeRequired("AreaWestAccess").removeRequired("MaskZora").addRequired("InteriorLuluRoom");
            getItem("HeartPieceZoraHallScrub").removeRequired("AreaWestAccess").removeRequired("MaskZora").addRequired("InteriorLuluRoom");
            getItem("ShopItemBusinessScrubGreenPotionInOcean").removeRequired("AreaWestAccess").addRequired("InteriorLuluRoom");
            getItem("ShopItemBusinessScrubBluePotionInOcean").removeRequired("AreaWestAccess").addRequired("InteriorLuluRoom");

            getItem("HeartPieceEvan").removeRequired("AreaWestAccess").removeRequired("MaskZora").addRequired("InteriorEvanRoom");

            getItem("ShopItemZoraShield").clear().addRequired("InteriorZoraShop");
            getItem("ShopItemZoraArrow10").clear().addRequired("InteriorZoraShop");
            getItem("ShopItemZoraRedPotion").clear().addRequired("InteriorZoraShop");

            getItem("HeartPieceSwordsmanSchool").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSSchoolPot1").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSSchoolPot2").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSSchoolPot3").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSSchoolPot4").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSSchoolPot5").addRequired("InteriorSwordsmanSchool");
            getItem("CollectableSwordsmanSchoolGong1").addRequired("InteriorSwordsmanSchool");

            if (getItem("Play Song of Healing") != null)
            {
                getItem("MaskGibdo").clear().addRequired("InteriorMusicBoxHouse", "Play Song of Healing");
            }
            else
            {
                getItem("MaskGibdo").clear().addRequired("InteriorMusicBoxHouse", "ItemOcarina", "SongHealing");
            }

            getItem("ItemBombBag").addRequired("InteriorBombShop");
            getItem("ShopItemBombsBomb10").addRequired("InteriorBombShop");
            getItem("ShopItemBombsBombchu10").addRequired("InteriorBombShop");
            getItem("NotebookMeetOldLadyInWCT").addRequired("InteriorBombShop");

            getItem("ItemLens").clear().addRequired("InteriorLensCave");
            if (getItem("Use Lens of Truth") != null)
            {
                getItem("ChestLensCaveRedRupee").clear().addRequired("InteriorLensCave").addConditional("Use Lens of Truth").addConditional("Lensless Chests");
            }
            else
            {
                getItem("ChestLensCaveRedRupee").clear().addRequired("InteriorLensCave").addConditional("ItemLens", "Magic Meter").addConditional("Lensless Chests");
            }
            getItem("ChestLensCavePurpleRupee").clear().addRequired("InteriorLensCave").addRequired("OtherExplosive");

            getItem("Pinnacle Rock Access").removeRequired("AreaWestAccess").addRequired("InteriorPinnacleRock");

            getItem("FairyMagic").addRequired("InteriorFairyFountainTown");
            getItem("MaskGreatFairy").addRequired("InteriorFairyFountainTown");

            getItem("FairySpinAttack").removeRequiredExcept("CollectibleStrayFairy").clearConditionals().addRequired("InteriorFairyFountainWoodfall");

            getItem("FairyDoubleMagic").removeRequiredExcept("CollectibleStrayFairy").clearConditionals().addRequired("InteriorFairyFountainSnowhead");

            getItem("FairyDoubleDefense").removeRequiredExcept("CollectibleStrayFairy").clearConditionals().addRequired("InteriorFairyFountainZoraCape");

            getItem("ItemFairySword").removeRequiredExcept("CollectibleStrayFairy").clearConditionals().addRequired("InteriorFairyFountainIkanaCanyon");

            getItem("MundaneItemSeahorse").addRequired("InteriorFishermanHut");

            getItem("ShopItemGoronBomb10InWinter").clear().addRequired("InteriorGoronShop");
            getItem("ShopItemGoronBomb10InSpring").addRequired("InteriorGoronShop");
            getItem("ShopItemGoronArrow10InWinter").clear().addRequired("InteriorGoronShop");
            getItem("ShopItemGoronArrow10InSpring").addRequired("InteriorGoronShop");
            getItem("ShopItemGoronRedPotionInWinter").clear().addRequired("InteriorGoronShop");
            getItem("ShopItemGoronRedPotionInSpring").addRequired("InteriorGoronShop");

            getItem("ItemBottleBeavers").clear().addRequired("InteriorWaterfallRapids", "MaskZora");
            getItem("HeartPieceBeaverRace").clear().addRequired("InteriorWaterfallRapids", "MaskZora");

            getItem("MaskGoron").removeRequired("AreaNorthAccess").addRequired("InteriorGoronGraveyard");

            getItem("HeartPiecePoeHut").removeRequired("AreaIkanaCanyonAccess").addRequired("InteriorPoeHut");

            getItem("Butler Race").copy(getItem("CollectableDekuPalaceRupeeCluster1")).addRequired("InteriorDekuShrine", "BottleCatchPrincess", "MaskDeku");
            if (getItem("Deliver Deku Princess Without Deku Mask") != null && getItem("Bomb Hovering") != null)
            {
                getItem("Butler Race")
                    .copy(getItem("CollectableDekuPalaceRupeeCluster1"))
                    .addRequired("InteriorDekuShrine")
                    .addConditional("BottleCatchPrincess", "MaskDeku")
                    .addConditional("Deliver Deku Princess Without Deku Mask", "Bomb Hovering");
            }
            else
            {
                getItem("Butler Race")
                    .copy(getItem("CollectableDekuPalaceRupeeCluster1"))
                    .addRequired("InteriorDekuShrine", "BottleCatchPrincess", "MaskDeku");
            }

            if (getItem("SecretShrineAccess") != null)
            {
                getItem("Secret Shrine Access").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
            }
            else
            {
                getItem("ChestSecretShrineHeartPiece").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("ChestSecretShrineDinoGrotto").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("ChestSecretShrineWizzGrotto").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("ChestSecretShrineWartGrotto").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("ChestSecretShrineGaroGrotto").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineMainRoomPot1").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineMainRoomPot2").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineMainRoomPot3").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineMainRoomPot4").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineMainRoomPot5").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
            }

            if (getItem("Secret Shrine Floating Items") != null)
            {
                getItem("Secret Shrine Floating Items").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");
            }
            else
            {
                getItem("CollectableSecretShrineEntranceRoomItem1").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem2").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem3").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem4").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem5").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem6").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem7").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem8").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem9").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem10").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem11").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem12").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem13").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem14").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem15").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem16").addRequired("InteriorSecretShrine");
                getItem("CollectableSecretShrineEntranceRoomItem17").addRequired("InteriorSecretShrine");
            }
            getItem("CollectableSecretShrineSoftSoil1").removeRequired("AreaEastAccess").addRequired("InteriorSecretShrine");

            getItem("GrottoGenericWoodsOfMystery").addRequired("InteriorWoodsOfMystery");

            getItem("ItemBottleGoronRace").addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot1").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot2").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot3").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot4").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot5").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot6").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot7").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot8").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot9").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot10").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot11").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot12").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot13").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot14").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot15").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot16").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot17").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot18").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot19").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot20").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot21").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot22").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot23").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot24").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot25").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot26").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot27").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot28").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot29").clear().addRequired("InteriorGoronRacetrack");
            getItem("CollectableGoronRacetrackPot30").clear().addRequired("InteriorGoronRacetrack");

            getItem("AreaStoneTowerTempleAccess").clear().addRequired("InteriorStoneTowerTemple");

            getItem("AreaSwampSpiderAccess").clear().addRequired("InteriorSwampSpiderHouse");

            getItem("UpgradeGiantWallet").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("MundaneItemOceanSpiderHouseDay2PurpleRupee").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("MundaneItemOceanSpiderHouseDay3RedRupee").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("Ocean Skulltulas").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("CollectibleOceanSpiderToken12").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("CollectibleOceanSpiderToken13").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");
            getItem("CollectibleOceanSpiderToken15").removeRequired("AreaWestAccess").addRequired("InteriorOceanSpiderHouse");

            logicObject.Version = 29;
        }

        private static void AddZoraEggs(JsonFormatLogic logicObject)
        {
            var itemInfo = new (string name, string reference, Action<JsonFormatLogicItem> modify)[]
            {
                ("ZoraEggPinnacleRock1", "ChestPinacleRockRedRupee1", null),
                ("ZoraEggPinnacleRock2", "ChestPinacleRockRedRupee1", null),
                ("ZoraEggPinnacleRock3", "ChestPinacleRockRedRupee1", null),
                ("ZoraEggPiratesFortressHookshotRoom", "ChestInsidePiratesFortressTankRedRupee", (JsonFormatLogicItem item) =>
                {
                    item.addRequired("Hookshot Room Beehive");
                }),
                ("ZoraEggPiratesFortressThreeGuardsRoom", "ChestInsidePiratesFortressTankRedRupee", null),
                ("ZoraEggPiratesFortressBarrelMazeRoom", "ChestInsidePiratesFortressTankRedRupee", null),
                ("ZoraEggPiratesFortressLoneGuardRoom", "ChestInsidePiratesFortressTankRedRupee", null),
                ("ZoraEggAll", null, (item) => item.addRequired(
                    "ZoraEggPinnacleRock1",
                    "ZoraEggPinnacleRock2",
                    "ZoraEggPinnacleRock3",
                    "ZoraEggPiratesFortressHookshotRoom",
                    "ZoraEggPiratesFortressThreeGuardsRoom",
                    "ZoraEggPiratesFortressBarrelMazeRoom",
                    "ZoraEggPiratesFortressLoneGuardRoom"
                )),
                ("ZoraEggAny", null, (item) => item
                    .addConditional("ZoraEggPinnacleRock1")
                    .addConditional("ZoraEggPinnacleRock2")
                    .addConditional("ZoraEggPinnacleRock3")
                    .addConditional("ZoraEggPiratesFortressHookshotRoom")
                    .addConditional("ZoraEggPiratesFortressThreeGuardsRoom")
                    .addConditional("ZoraEggPiratesFortressBarrelMazeRoom")
                    .addConditional("ZoraEggPiratesFortressLoneGuardRoom")
                ),
            };

            logicObject.Logic.InsertRange(144, GetLogicItems(logicObject, itemInfo));

            JsonFormatLogicItem getItem(string id) => logicObject.Logic.SingleOrDefault(logicItem => logicItem.Id == id);

            if (getItem("Hookshot Room Beehive") == null)
            {
                var hookshotRoomBeehive = new JsonFormatLogicItem
                {
                    Id = "Hookshot Room Beehive",
                    RequiredItems = new List<string>(),
                    ConditionalItems = new List<List<string>>(),
                };
                hookshotRoomBeehive.addConditional("OtherArrow");
                if (getItem("Deku Bubbles") != null)
                {
                    hookshotRoomBeehive.addConditional("Deku Bubbles");
                }
                else if (getItem("Magic Meter") != null)
                {
                    hookshotRoomBeehive.addConditional("MaskDeku", "Magic Meter");
                }
                else
                {
                    hookshotRoomBeehive.addConditional("MaskDeku", "FairyMagic");
                    hookshotRoomBeehive.addConditional("MaskDeku", "FairyDoubleMagic");
                }
                logicObject.Logic.Add(hookshotRoomBeehive);
            }

            getItem("BottleCatchEgg")
                .clearConditionals()
                .addConditional("ZoraEggAll")
                .addConditional("BottleCatchEgg", "BottleCatchFish", "ZoraEggAny");

            DeleteUnusedLogicItems(logicObject, 1500);

            logicObject.Version = 30;
        }

        private static void DeleteUnusedLogicItems(JsonFormatLogic logicObject, int minimumIndex)
        {
            bool updated;
            do
            {
                updated = false;
                for (var i = logicObject.Logic.Count - 1; i >= minimumIndex; i--)
                {
                    var item = logicObject.Logic[i];
                    if (!logicObject.Logic.Any(x => x.RequiredItems.Contains(item.Id) || x.ConditionalItems.Any(c => c.Contains(item.Id))))
                    {
                        logicObject.Logic.RemoveAt(i);
                        updated = true;
                    }
                }
            } while (updated);
        }

        private static List<JsonFormatLogicItem> GetLogicItems(IEnumerable<string> itemNames)
        {
            return GetLogicItems(itemNames.Select((name) => (name, (JsonFormatLogicItem) null, (Action<JsonFormatLogicItem>)null, false)));
        }

        private static List<JsonFormatLogicItem> GetLogicItems(JsonFormatLogic logicObject, IEnumerable<(string name, int? reference, Action<JsonFormatLogicItem> modify)> itemInfo)
        {
            return GetLogicItems(itemInfo.Select(data => (data.name, data.reference.HasValue ? logicObject.Logic[data.reference.Value] : null, data.modify, false)));
        }

        private static List<JsonFormatLogicItem> GetLogicItems(JsonFormatLogic logicObject, IEnumerable<(string name, string reference, Action<JsonFormatLogicItem> modify)> itemInfo, bool deleteReference = false)
        {
            return GetLogicItems(itemInfo.Select(data => (data.name, logicObject.Logic.FirstOrDefault(item => item.Id == data.reference), data.modify, deleteReference)));
        }

        private static List<JsonFormatLogicItem> GetLogicItems(IEnumerable<(string name, Action<JsonFormatLogicItem> modify)> itemInfo)
        {
            return GetLogicItems(itemInfo.Select(data => (data.name, (JsonFormatLogicItem)null, data.modify, false)));
        }

        private static List<JsonFormatLogicItem> GetLogicItems(IEnumerable<(string name, JsonFormatLogicItem reference, Action<JsonFormatLogicItem> modify, bool deleteReference)> itemInfo)
        {
            return itemInfo.Select(data =>
            {
                var logicItem = new JsonFormatLogicItem
                {
                    Id = data.name,
                    RequiredItems = new List<string>(),
                    ConditionalItems = new List<List<string>>(),
                };

                if (data.reference != null)
                {
                    logicItem.copy(data.reference);

                    if (data.deleteReference)
                    {
                        data.reference.Id = null; // Mark for deletion
                    }
                }

                if (data.modify != null)
                {
                    data.modify(logicItem);
                }

                return logicItem;
            }).ToList();
        }

        private static void addTodo(JsonFormatLogicItem item)
        {
            item.RequiredItems.Add("TODO");
        }

        private static bool addRequiredIfExists(JsonFormatLogic logicObject, JsonFormatLogicItem item, params string[] value)
        {
            if (value.All(r => logicObject.Logic.Any(x => x.Id == r)))
            {
                addRequired(item, value);
                return true;
            }
            return false;
        }

        private static bool addConditionalIfExists(JsonFormatLogic logicObject, JsonFormatLogicItem item, params string[] conditionals)
        {
            if (conditionals.All(c => logicObject.Logic.Any(x => x.Id == c)))
            {
                addConditional(item, conditionals);
                return true;
            }
            return false;
        }

        private static void addConditionalOrTodo(JsonFormatLogic logicObject, JsonFormatLogicItem item, params string[] conditionals)
        {
            if (!addConditionalIfExists(logicObject, item, conditionals))
            {
                addTodo(item);
            }
        }

        private static bool removeConditional(JsonFormatLogicItem item, params string[] values)
        {
            return item.ConditionalItems.RemoveAll(c => c.SequenceEqual(values)) > 0;
        }

        private static void removeConditionalItemsContainingText(JsonFormatLogicItem item, string text)
        {
            item.ConditionalItems.ForEach(c => c.RemoveAll(v => v.Contains(text)));
        }

        private static void removeConditionalsContainingText(JsonFormatLogicItem item, string text)
        {
            item.ConditionalItems.RemoveAll(c => c.Any(v => v.Contains(text)));
        }

        private static void cleanUpConditionals(JsonFormatLogicItem logicItem)
        {
            logicItem.ConditionalItems.RemoveAll(c => !c.Any());
            for (var i = 0; i < logicItem.ConditionalItems.Count; i++)
            {
                logicItem.ConditionalItems.RemoveAll(c => c != logicItem.ConditionalItems[i] && c.SequenceEqualIgnoreOrder(logicItem.ConditionalItems[i]));
            }
        }

        private static void addDayOnly(JsonFormatLogicItem item)
        {
            item.TimeAvailable = TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3;
            item.TimeSetup = TimeOfDay.Day1 | TimeOfDay.Day2 | TimeOfDay.Day3;
        }

        private static JsonFormatLogicItem clear(this JsonFormatLogicItem item)
        {
            item.RequiredItems.Clear();
            item.ConditionalItems.Clear();
            item.TimeNeeded = TimeOfDay.None;
            item.TimeAvailable = TimeOfDay.None;
            item.TimeSetup = TimeOfDay.None;
            return item;
        }

        private static JsonFormatLogicItem clearConditionals(this JsonFormatLogicItem item)
        {
            item.ConditionalItems.Clear();
            return item;
        }

        private static JsonFormatLogicItem removeRequiredExcept(this JsonFormatLogicItem item, string value)
        {
            item.RequiredItems.RemoveAll(x => !x.Contains(value));
            return item;
        }

        private static JsonFormatLogicItem addTimeAvailable(this JsonFormatLogicItem item, TimeOfDay timeAvailable)
        {
            item.TimeAvailable = timeAvailable;
            return item;
        }

        private static JsonFormatLogicItem addTimeNeeded(this JsonFormatLogicItem item, TimeOfDay timeNeeded)
        {
            item.TimeNeeded = timeNeeded;
            return item;
        }

        private static JsonFormatLogicItem addRequired(this JsonFormatLogicItem item, params string[] value)
        {
            item.RequiredItems.AddRange(value);
            return item;
        }

        private static JsonFormatLogicItem removeRequired(this JsonFormatLogicItem item, string value)
        {
            item.RequiredItems.Remove(value);
            return item;
        }

        private static JsonFormatLogicItem addConditional(this JsonFormatLogicItem item, params string[] conditionals)
        {
            item.ConditionalItems.Add(conditionals.ToList());
            return item;
        }

        private static JsonFormatLogicItem copy(this JsonFormatLogicItem item, JsonFormatLogicItem reference)
        {
            item.RequiredItems = reference.RequiredItems.ToList();
            item.ConditionalItems = reference.ConditionalItems.Select(c => c.ToList()).ToList();
            item.TimeAvailable = reference.TimeAvailable;
            item.TimeNeeded = reference.TimeNeeded;
            item.TimeSetup = reference.TimeSetup;
            return item;
        }

        private class MigrationItem
        {
            public int ID;
            public List<List<int>> Conditionals = new List<List<int>>();
            public List<int> DependsOnItems = new List<int>();
            public int TimeNeeded = 0;
        }

        private class JsonFormatLogic
        {
            public int Version { get; set; }
            public List<JsonFormatLogicItem> Logic { get; set; }
        }

        private class JsonFormatLogicItem
        {
            public string Id { get; set; }
            public List<string> RequiredItems { get; set; }
            public List<List<string>> ConditionalItems { get; set; }
            public TimeOfDay TimeNeeded { get; set; }
            public TimeOfDay TimeAvailable { get; set; }
            public TimeOfDay TimeSetup { get; set; }
            public bool IsTrick { get; set; }
            public string TrickTooltip { get; set; }
            public string TrickCategory { get; set; }
            public string SettingExpression { get; set; }
        }

        [Flags]
        private enum TimeOfDay
        {
            None = 0,
            Day1 = 1,
            Night1 = 2,
            Day2 = 4,
            Night2 = 8,
            Day3 = 16,
            Night3 = 32,
        }
    }
}
