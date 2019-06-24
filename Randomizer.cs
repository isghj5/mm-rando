using MMRando.Constants;
using MMRando.LogicMigrator;
using MMRando.Models;
using MMRando.Models.Rom;
using MMRando.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MMRando
{

    public class Randomizer
    {
        private Random _random { get; set; }
        public Random Random
        {
            get => _random;
            set => _random = value;
        }

        public List<ItemObject> ItemList { get; set; }

        List<Gossip> GossipList { get; set; }

        #region Dependence and Conditions
        List<int> ConditionsChecked { get; set; }
        Dictionary<int, Dependence> DependenceChecked { get; set; }
        List<int[]> ConditionRemoves { get; set; }

        private class Dependence
        {
            public int[] ItemIds { get; set; }
            public DependenceType Type { get; set; }

            public static Dependence Dependent => new Dependence { Type = DependenceType.Dependent };
            public static Dependence NotDependent => new Dependence { Type = DependenceType.NotDependent };
            public static Dependence Circular(params int[] itemIds) => new Dependence { ItemIds = itemIds, Type = DependenceType.Circular };
        }

        private enum DependenceType
        {
            Dependent,
            NotDependent,
            Circular
        }

        Dictionary<int, List<int>> ForbiddenReplacedBy = new Dictionary<int, List<int>>
        {
            // Deku_Mask should not be replaced by trade items, or items that can be downgraded.
            {
                Items.MaskDeku, new List<int>
                {
                    Items.UpgradeGildedSword,
                    Items.UpgradeMirrorShield,
                    Items.UpgradeBiggestQuiver,
                    Items.UpgradeBigBombBag,
                    Items.UpgradeBiggestBombBag,
                    Items.UpgradeGiantWallet
                }
                .Concat(Enumerable.Range(Items.TradeItemMoonTear, Items.TradeItemMamaLetter - Items.TradeItemMoonTear + 1))
                .Concat(Enumerable.Range(Items.ItemBottleWitch, Items.ItemBottleMadameAroma - Items.ItemBottleWitch + 1))
                .ToList()
            },

            // Keaton_Mask and Mama_Letter are obtained one directly after another
            // Keaton_Mask cannot be replaced by items that may be overwritten by item obtained at Mama_Letter
            {
                Items.MaskKeaton,
                new List<int> {
                    Items.UpgradeGiantWallet,
                    Items.UpgradeGildedSword,
                    Items.UpgradeMirrorShield,
                    Items.UpgradeBiggestQuiver,
                    Items.UpgradeBigBombBag,
                    Items.UpgradeBiggestBombBag,
                    Items.TradeItemMoonTear,
                    Items.TradeItemLandDeed,
                    Items.TradeItemSwampDeed,
                    Items.TradeItemMountainDeed,
                    Items.TradeItemOceanDeed,
                    Items.TradeItemRoomKey,
                    Items.TradeItemMamaLetter,
                    Items.TradeItemKafeiLetter,
                    Items.TradeItemPendant
                }
            },
        };

        Dictionary<int, List<int>> ForbiddenPlacedAt = new Dictionary<int, List<int>>
        {
        };

        #endregion

        private Settings _settings;
        private RandomizedResult _randomized;

        public Randomizer(Settings settings)
        {
            _settings = settings;
        }

        //rando functions

        #region Gossip quotes

        private void MakeGossipQuotes()
        {
            var gossipQuotes = new List<string>();
            ReadAndPopulateGossipList();

            for (int itemIndex = 0; itemIndex < ItemList.Count; itemIndex++)
            {
                if (!ItemList[itemIndex].ReplacesAnotherItem)
                {
                    continue;
                }

                // Skip hints for vanilla bottle content
                if ((!_settings.RandomizeBottleCatchContents)
                    && ItemUtils.IsBottleCatchContent(itemIndex))
                {
                    continue;
                }

                // Skip hints for vanilla shop items
                if ((!_settings.AddShopItems)
                    && ItemUtils.IsShopItem(itemIndex))
                {
                    continue;
                }

                // Skip hints for vanilla dungeon items
                if (!_settings.AddDungeonItems
                    && ItemUtils.IsDungeonItem(itemIndex))
                {
                    continue;
                }

                // Skip hint for song of soaring
                if (_settings.ExcludeSongOfSoaring && itemIndex == Items.SongSoaring)
                {
                    continue;
                }

                // Skip hints for moon items
                if (!_settings.AddMoonItems
                    && ItemUtils.IsMoonItem(itemIndex))
                {
                    continue;
                }

                // Skip hints for other items
                if (!_settings.AddOther
                    && ItemUtils.IsOtherItem(itemIndex))
                {
                    continue;
                }

                int sourceItemId = ItemList[itemIndex].ReplacesItemId;
                sourceItemId = ItemUtils.SubtractItemOffset(sourceItemId);

                int toItemId = itemIndex;
                toItemId = ItemUtils.SubtractItemOffset(toItemId);

                // 5% chance of being fake
                bool isFake = (Random.Next(100) < 5);
                if (isFake)
                {
                    sourceItemId = Random.Next(GossipList.Count);
                }

                int sourceMessageLength = GossipList[sourceItemId]
                    .SourceMessage
                    .Length;

                int destinationMessageLength = GossipList[toItemId]
                    .DestinationMessage
                    .Length;

                // Randomize messages
                string sourceMessage = GossipList[sourceItemId]
                    .SourceMessage[Random.Next(sourceMessageLength)];

                string destinationMessage = GossipList[toItemId]
                    .DestinationMessage[Random.Next(destinationMessageLength)];

                // Sound differs if hint is fake
                ushort soundEffectId = (ushort)(isFake ? 0x690A : 0x690C);

                var quote = BuildGossipQuote(soundEffectId, sourceMessage, destinationMessage);

                gossipQuotes.Add(quote);
            }

            for (int i = 0; i < Gossip.JunkMessages.Count; i++)
            {
                gossipQuotes.Add(Gossip.JunkMessages[i]);
            }

            _randomized.GossipQuotes = gossipQuotes;
        }

        private void ReadAndPopulateGossipList()
        {
            GossipList = new List<Gossip>();

            string[] gossipLines = Properties.Resources.GOSSIP
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < gossipLines.Length; i += 2)
            {
                var sourceMessage = gossipLines[i].Split(';');
                var destinationMessage = gossipLines[i + 1].Split(';');
                var nextGossip = new Gossip
                {
                    SourceMessage = sourceMessage,
                    DestinationMessage = destinationMessage
                };

                GossipList.Add(nextGossip);
            }
        }

        public string BuildGossipQuote(ushort soundEffectId, string sourceMessage, string destinationMessage)
        {
            int startIndex = Random.Next(Gossip.MessageStartSentences.Count);
            int midIndex = Random.Next(Gossip.MessageMidSentences.Count);
            string start = Gossip.MessageStartSentences[startIndex];
            string mid = Gossip.MessageMidSentences[midIndex];

            string sfx = $"{(char)((soundEffectId >> 8) & 0xFF)}{(char)(soundEffectId & 0xFF)}";

            return $"\x1E{sfx}{start} \x01{sourceMessage}\x00\x11{mid} \x06{destinationMessage}\x00" + "...\xBF";
        }

        #endregion

        private void DungeonShuffle()
        {
            var newDCFlags = new int[] { -1, -1, -1, -1 };
            var newDCMasks = new int[] { -1, -1, -1, -1 };
            var newEntranceIndices = new int[] { -1, -1, -1, -1 };
            var newExitIndices = new int[] { -1, -1, -1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int n;
                do
                {
                    n = Random.Next(4);
                } while (newEntranceIndices.Contains(n));

                newEntranceIndices[i] = n;
                newExitIndices[n] = i;
            }

            var areaAccessObjects = new ItemObject[] {
                ItemList[Items.AreaWoodFallTempleAccess],
                ItemList[Items.AreaSnowheadTempleAccess],
                ItemList[Items.AreaInvertedStoneTowerTempleAccess],
                ItemList[Items.AreaGreatBayTempleAccess]
            };

            var areaAccessObjectIndexes = new int[] {
                Items.AreaWoodFallTempleAccess,
                Items.AreaSnowheadTempleAccess,
                Items.AreaInvertedStoneTowerTempleAccess,
                Items.AreaGreatBayTempleAccess
            };

            for (int i = 0; i < 4; i++)
            {
                Debug.WriteLine($"Entrance {Items.ITEM_NAMES[areaAccessObjectIndexes[newEntranceIndices[i]]]} placed at {Items.ITEM_NAMES[areaAccessObjects[i].ID]}.");
                ItemList[areaAccessObjectIndexes[newEntranceIndices[i]]] = areaAccessObjects[i];
            }

            var areaClearObjects = new ItemObject[] {
                ItemList[Items.AreaWoodFallTempleClear],
                ItemList[Items.AreaSnowheadTempleClear],
                ItemList[Items.AreaStoneTowerClear],
                ItemList[Items.AreaGreatBayTempleClear]
            };

            var areaClearObjectIndexes = new int[] {
                Items.AreaWoodFallTempleClear,
                Items.AreaSnowheadTempleClear,
                Items.AreaStoneTowerClear,
                Items.AreaGreatBayTempleClear
            };

            for (int i = 0; i < 4; i++)
            {
                ItemList[areaClearObjectIndexes[i]] = areaClearObjects[newEntranceIndices[i]];
            }

            var newEntrances = new int[] { -1, -1, -1, -1 };
            var newExits = new int[] { -1, -1, -1, -1 };

            for (int i = 0; i < 4; i++)
            {
                newEntrances[i] = Values.OldEntrances[newEntranceIndices[i]];
                newExits[i] = Values.OldExits[newExitIndices[i]];
                newDCFlags[i] = Values.OldDCFlags[newExitIndices[i]];
                newDCMasks[i] = Values.OldMaskFlags[newExitIndices[i]];
            }

            _randomized.NewEntrances = newEntrances;
            _randomized.NewDestinationIndices = newEntranceIndices;
            _randomized.NewExits = newExits;
            _randomized.NewExitIndices = newExitIndices;
            _randomized.NewDCFlags = newDCFlags;
            _randomized.NewDCMasks = newDCMasks;
        }

        private void OwlShuffle(bool hidden)
        {
            int size = 12;
            int poolSize = size;
            _randomized.OwlStatueList = new int[size];
            for (int i = 0; i < _randomized.OwlStatueList.Length; i++)
            {
                _randomized.OwlStatueList[i] = -1;
            }
            if (!hidden)
            {
                _randomized.OwlStatueList[0] = 0;
                _randomized.OwlStatueList[8] = 8;
                _randomized.OwlStatueList[10] = 10;
            }
            int owl = 0;
            while (owl < _randomized.OwlStatueList.Length)
            {
                if (_randomized.OwlStatueList[owl] == -1)
                {
                    int n;
                    do
                    {
                        n = _random.Next(_randomized.OwlStatueList.Length);
                    } while (_randomized.OwlStatueList.Contains(n));

                    _randomized.OwlStatueList[owl] = n;
                    _randomized.OwlStatueList[n] = owl;
                }
                owl++;
            }
        }
        
        #region Entrance Rando
        Dictionary<string, List<Spawn>> TerminaMap { get; set; }
        Dictionary<string, List<Spawn>> ShuffledMap { get; set; }
        Dictionary<ushort, List<string>> SceneNamesByIndex { get; set; }
        private void EntranceShuffle()
        {
            TerminaMap = new Dictionary<string, List<Spawn>>();
            ShuffledMap = new Dictionary<string, List<Spawn>>();
            SceneNamesByIndex = new Dictionary<ushort, List<string>>();
            GetVanillaTerminaMap();
            ShuffleEntrances();
            CheckEntrances();
            FinalizeEntrances();
            WriteMapData();
        }

        private void WriteMapData()
        {
            
        }

        private void GetVanillaTerminaMap()
        {
            AddSpawn("Clock Tower: South Clock Town", 0xC010, "Clock Tower");
            // Assign Clock Tower Scene a name
            AddSceneSpawns(new string[] {
                "Clock Tower", "Termina Field", "East Clock Town",
                "West Clock Town", "North Clock Town", "South West Connection",
                "Laundry Pool", "South East Connection", "Clock Tower Roof", "Owl Warp"
            }, 0xD8, 0x6F, "South Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "East Clock Town", "South Clock Town", "Clock Town Fairy", "Deku Playground"
            }, 0xD6, 0x6E, "North Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "South West Connection", "South Clock Town",
                "Swordsman's School", "Curiosity Shop", "Trading Post", "Bomb Shop",
                "Post Office", "Lottery Shop"
            }, 0xD4, 0x6D, "West Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "South East Connection", "Observatory", "South Clock Town",
                "Treasure Chest Game", "North Clock Town", "Honey & Darling", "Mayor's Residence",
                "Town Shooting Gallery", "Stock Pot Inn", "Stock Pot Roof", "Milk Bar"
            }, 0xD2, 0x6C, "East Clock Town");
            AddSceneSpawns(new string[] {
                "South Clock Town"
            }, 0xDA, 0x70, "Laundry Pool");
            PairSpawns(
                "South Clock Town: South West Connection",
                "West Clock Town: South West Connection",
                "Overworld");
            PairSpawns(
                "South Clock Town: South East Connection",
                "East Clock Town: South East Connection",
                "Overworld");

            AddPairedInteriors(
                new string[] {
                    "Bomb Shop", "Trading Post", "Swordsman's School", "Curiosity Shop",
                    "Post Office", "Lottery Shop", "Treasure Chest Game", "Honey & Darling",
                    "Mayor's Residence", "Town Shooting Gallery", "Stock Pot Inn", "Milk Bar",
                    "Deku Playground", "Clock Tower Roof" },
                new ushort[] {
                    0xCA00, 0x6200, 0xA200, 0x0E00,
                    0x5600, 0x6C00, 0x2800, 0x0800,
                    0x0000, 0x3A00, 0xBC00, 0x2400,
                    0x3600, 0x2C00 },
                new ushort[] {
                    0x68, 0x34, 0x54, 0x0D,
                    0x2E, 0x39, 0x17, 0x11,
                    0x12, 0x20, 0x61, 0x15,
                    0x1E, 0x19 }
                );
            AddSpawn("Stock Pot Roof", 0xBC10, "Stock Pot Inn");
            PairInteriorEntrance("East Clock Town: Stock Pot Roof", "Stock Pot Roof");

            PairSingleInterior("Curiosity Shop Backroom", "Laundry Pool", 0x0E10, 0xDA10);
            AddSpawn("Curiosity Shop: Telescope", 0x0E20, "Curiosity Shop Backroom");
            AddSpawn("Curiosity Shop Backroom: Telescope", 0x0E30, "Curiosity Shop Backroom");

            AddSceneSpawns(new string[] {
                "West Clock Town", "Swamp Path", "Great Bay Coast",
                "Mountain Path", "Ikana Path", "Milk Road", "South Clock Town",
                "East Clock Town", "North Clock Town", "Observatory", "Telescope"
            }, 0x54, 0x2D, "Termina Field");
            AddGrottos(new string[] {
                "Great Bay Gossip", "Woodfall Gossip", "Stone Tower Gossip",
                "Snowhead Gossip", "Dodongo", "Scrub Haggle",
                "Cow", "Beehive", "Peahat"},
            new ushort[] {
                0x1400, 0x1410, 0x1420,
                0x1430, 0x1470, 0x1490,
                0x14A0, 0x14B0, 0x14D0
                },
            "Termina Field");

            AddSceneSpawns(new string[] {
                "East Clock Town", "Termina Field", "Telescope"
            }, 0x4C, 0x29,"Observatory");
            PairInteriorEntrance("East Clock Town: Observatory", "Observatory: East Clock Town");
            PairInteriorEntrance("Termina Field: Observatory", "Observatory: Termina Field");

            AddSceneSpawns(new string[] {
                "Termina Field", "Southern Swamp", "Swamp Shooting Gallery"
            }, 0x7A, 0x40, "Swamp Path");
            AddDuplicateSceneSpawns(new string[] {
                "Swamp Path", "Tourist Center", "Woodfall", "Deku Palace",
                "Deku Shortcut", "Potion Shop", "", "Woods of Mystery",
                "Swamp Spider House", "Ikana Canyon", "Owl Warp"
            }, "Southern Swamp", 0x84, 0x45, "Healed", 0x0C, 0x00);

            AddSceneSpawns(new string[] {
                "Southern Swamp", "Caught", "Deku King Chamber", "Sonata Monkey",
                "Deku Shrine", "Deku Shortcut", "", "", "", "Bean Seller", ""
            }, 0x50, 0x2B, "Deku Palace");
            AddGrottos(new string[] { "Bean Seller" }, new ushort[] { 0x14C0 }, "Deku Palace");
            PairInteriorEntrance("Deku Palace: Bean Seller", "Grotto: Bean Seller");

            AddPairedInteriors(
                new string[] {
                    "Potion Shop", "Swamp Shooting Gallery", "Swamp Spider House",
                    "Woods of Mystery", "Tourist Center", "Deku King Chamber",
                    "Sonata Monkey", "Deku Shrine" },
                new ushort[] {
                    0x0400, 0x4200, 0x4800,
                    0xC200, 0xA800, 0x7600,
                    0x7610, 0x9E00 },
                new ushort[] {
                    0x0A, 0x24, 0x27,
                    0x64, 0x57, 0x3E,
                    0x3E, 0x52 });
            PairSpawns(
                "Southern Swamp: Deku Shortcut",
                "Deku Palace: Deku Shortcut",
                "Overworld");
            PairSpawns(
                "Southern Swamp Healed: Deku Shortcut",
                "Deku Palace: Deku Shortcut",
                "Overworld");

            AddSceneSpawns(new string[] {
                "Southern Swamp", "Temple", "Woodfall Fairy", "Deku Princess Room", "Owl Warp"
            }, 0x86, 0x46, "Woodfall");
            AddPairedInteriors(new string[] {
                "Deku Princess Room"
            }, new ushort[] { 0x3020 }, new ushort[] { 0x1B });

            AddSceneSpawns(new string[] {
                "Termina Field", "Mountain Village"
            }, 0x32, 0x1C, "Mountain Path");
            AddDuplicateSceneSpawns(new string[] {
                "", "Smithy", "Twin Islands", "Goron Grave",
                "Snowhead Path", "", "Mountain Path", "", "Owl Warp"
            }, "Mountain Village", 0x9A, 0x50, "Spring", 0xAE, 0x5A);
            AddDuplicateSceneSpawns(new string[] {
                "Mountain Village", "Goron Village", "Goron Racetrack"
            }, "Twin Islands", 0xB4, 0x5D, "Spring", 0xB6, 0x5E);
            AddDuplicateSceneSpawns(new string[] {
                "Twin Islands", "", "Goron Shrine"
            }, "Goron Village", 0x94, 0x4D, "Spring", 0x8A, 0x48);

            AddPairedInteriors(
                new string[] {
                    "Goron Grave", "Smithy", "Goron Racetrack", "Goron Shrine" },
                new ushort[] {
                    0x9600, 0x5200, 0xD000, 0x5E00 },
                new ushort[] {
                    0x4E, 0x2C, 0x6B, 0x32 });
            PairSingleInterior("Lens Grotto", "Goron Village", 0x1500, 0x9430);
            // override manually
            PairSingleInterior("Goron Shop", "Goron Shrine", 0x7400, 0x5E10);

            AddSpawn("Goron Racetrack: Minigame", 0xD010, "Goron Racetrack");
            AddSpawn("Goron Racetrack: Minigame Over", 0xD020, "Goron Racetrack");
            PairSingleSpawn("Goron Racetrack: Minigame", "Goron Racetrack: Minigame Over", "Minigame");

            AddGrottos(new string[] { "Hot Spring Water Winter" }, new ushort[] { 0x1450 }, "Twin Islands");
            AddGrottos(new string[] { "Hot Spring Water Spring" }, new ushort[] { 0x1450 }, "Twin Islands Spring");

            AddSceneSpawns(new string[] {
                "Mountain Village", "Snowhead"
            }, 0xB0, 0x5B, "Snowhead Path");
            AddSceneSpawns(new string[] {
                "Snowhead Path", "Temple", "Snowhead Fairy", "Owl Warp"
            }, 0xB2, 0x5C, "Snowhead");

            AddSceneSpawns(new string[] {
                "Termina Field", "Romani Ranch", "", "Gorman Track", "Owl Warp"
            }, 0x3E, 0x22, "Milk Road");
            AddSceneSpawns(new string[] {
                "Milk Road", "", "Stable", "Ranch House", "Cucco Shack", "Doggy Racetrack",
                "", "", "", "", "", ""
            }, 0x64, 0x35, "Romani Ranch");
            AddPairedInteriors(
                new string[] {
                    "Stable", "Ranch House", "Cucco Shack", "Doggy Racetrack", "Gorman Track" },
                new ushort[] {
                    0x0600, 0x0610, 0x7E00, 0x7C00, 0xCE00 },
                new ushort[] {
                    0x10, 0x10, 0x42, 0x41, 0x6A });

            AddSceneSpawns(new string[] {
                "Termina Field", "Zora Cape", "", "Pinnacle Rock", "Fisherman's Hut",
                "Pirate's Fortress", "", "Marine Lab", "Ocean Spider House", "", "", "Owl Warp", "Caught"
            }, 0x68, 0x37, "Great Bay Coast");
            AddPairedInteriors(
                new string[] {
                    "Pinnacle Rock", "Fisherman's Hut", "Marine Lab", "Ocean Spider House" },
                new ushort[] {
                    0x4400, 0x7200, 0x5800, 0x4A00 },
                new ushort[] {
                    0x25, 0x3C, 0x2F, 0x28 });
            GetSpawn("Great Bay Coast: Pinnacle Rock").Type = "Water";
            GetSpawn("Pinnacle Rock").Type = "Water";

            AddSceneSpawns(new string[] {
                "Great Bay Coast", "Pirate's Fortress", "Pirate Tunnel", "Water Jet Exit",
                "Caught", "Pirate Platform", "Telescope Room"
            }, 0x70, 0x3B, "Outside PF");
            AddSceneSpawns(new string[] {
                "Outside PF", "Hookshot Room", "Hookshot Room Upper", "Well Guarded Room", "Well Guarded Exit",
                "Barrel Room", "Barrel Room Exit", "Twin Barrel Room", "Twin Barrel Room Exit",
                "", "Telescope", "", "Pirate Platform"
            }, 0x22, 0x14, "Pirate's Fortress");
            PairSpawns("Outside PF: Great Bay Coast", "Great Bay Coast: Pirate's Fortress", "Water");
            PairInteriorEntrance("Pirate's Fortress: Pirate Platform", "Outside PF: Pirate Platform");

            AddSpawns("Hookshot Room",
                new string[] { "Main", "Upper" },
                new ushort[] { 0x4000, 0x4010 },
                new ushort[] { 0x23, 0x23 });
            AddSpawns("Well Guarded Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4020, 0x4030 },
                new ushort[] { 0x23, 0x23 });
            AddSpawns("Barrel Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4040, 0x4050 },
                new ushort[] { 0x23, 0x23 });
            AddSpawns("Twin Barrel Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4060, 0x4070 },
                new ushort[] { 0x23, 0x23 });
            AddSpawns("Pirate Tunnel",
                new string[] { "Telescope", "Entrance", "Exit" },
                new ushort[] { 0x4080, 0x4090, 0x40A0 },
                new ushort[] { 0x23, 0x23, 0x23 });
            PairInteriorEntrance("Pirate's Fortress: Hookshot Room", "Hookshot Room: Main");
            PairInteriorEntrance("Pirate's Fortress: Hookshot Room Upper", "Hookshot Room: Upper");
            PairInteriorEntrance("Pirate's Fortress: Well Guarded Room", "Well Guarded Room: Entrance");
            PairInteriorEntrance("Pirate's Fortress: Well Guarded Exit", "Well Guarded Room: Exit");
            PairInteriorEntrance("Pirate's Fortress: Barrel Room", "Barrel Room: Entrance");
            PairInteriorEntrance("Pirate's Fortress: Barrel Room Exit", "Barrel Room: Exit");
            PairInteriorEntrance("Pirate's Fortress: Twin Barrel Room", "Twin Barrel Room: Entrance");
            PairSpawns("Pirate's Fortress: Twin Barrel Room Exit", "Twin Barrel Room: Exit", "Permanent");
            PairInteriorEntrance("Outside PF: Pirate Tunnel", "Pirate Tunnel: Entrance");
            PairInteriorEntrance("Outside PF: Telescope Room", "Pirate Tunnel: Exit");

            AddSceneSpawns(new string[] {
                "Great Bay Coast", "Zora Hall Water", "Zora Hall",
                "", "Waterfall Rapids", "Great Bay Fairy", "Owl Warp", "Temple"
            }, 0x6A, 0x38,"Zora Cape");
            AddSceneSpawns(new string[] {
                "Water", "Zora Cape", "Zora Shop", "Lulu's Room",
                "Evan's Room", "Japas's Room", "Mikau's Room"
            }, 0x60, 0x33, "Zora Hall");
            AddPairedInteriors(
                new string[] {
                    "Waterfall Rapids", "Zora Shop", "Lulu's Room",
                    "Evan's Room", "Japas's Room", "Mikau's Room" },
                new ushort[] {
                    0x8E00, 0x9250, 0x9220,
                    0x9230, 0x9210, 0x9200},
                new ushort[] {
                    0x4C, 0x4C, 0x4C,
                    0x4C, 0x4C, 0x4C});
            PairSpawns("Zora Cape: Zora Hall Water", "Zora Hall: Water", "Water");

            AddSceneSpawns(new string[] {
                "Termina Field", "Ikana Canyon", "Ikana Graveyard"
            }, 0xA0, 0x53, "Ikana Path");
            AddSceneSpawns(new string[] {
                "Ikana Path", "Night 3 Grave", "Night 2 Grave",
                "Night 1 Grave", "Dampe's House", "Defeat Skull Keeta"
            }, 0x80, 0x43, "Ikana Graveyard");
            AddPairedInteriors(
                new string[] {
                    "Night 2 Grave", "Night 1 Grave", "Night 3 Grave", "Dampe's House" },
                new ushort[] {
                    0x0A00, 0x0A10, 0x5A00, 0x5A10 },
                new ushort[] {
                    0x0C, 0x0C, 0x30, 0x30 });
            GetSpawn("Dampe's House").Type = "Permanent";
            GetSpawn("Ikana Graveyard: Night 3 Grave").Type = "Permanent";

            AddSceneSpawns(new string[] {
                "Ikana Path", "Poe Hut", "Music Box", "Stone Tower", "Owl Warp",
                "Well", "Sakon's Hideout", "", "Ikana Castle", "", "",
                "Stone Tower Fairy", "Secret Shrine", "Cursed Area Exit", "Cursed Area"
            }, 0x20, 0x13, "Ikana Canyon");
            PairInteriorEntrance("Ikana Canyon: Cursed Area Exit", "Ikana Canyon: Cursed Area");
            AddSceneSpawns(new string[] {
                "Well", "Ikana Canyon", "", "", "", "", "Igos"
            }, 0x34, 0x1D, "Ikana Castle");
            AddSceneSpawns(new string[] {
                "Ikana Canyon", "Ikana Castle"
            }, 0x90, 0x4B, "Well");
            AddSceneSpawns(new string[] {
                "Ikana Canyon", "Inverted Stone Tower", "Stone Tower Temple", "Owl Warp"
            }, 0xAA, 0x58, "Stone Tower");
            AddSceneSpawns(new string[] {
                "Stone Tower", "Temple"
            }, 0xAC, 0x59, "Inverted Stone Tower");
            AddPairedInteriors(
                new string[] {
                    "Music Box", "Igos", "Secret Shrine", "Poe Hut",
                    "Stone Tower Temple", "Sakon's Hideout" },
                new ushort[] {
                    0xA400, 0xA600, 0xBA00, 0x9C00,
                    0x2600, 0x9800 },
                new ushort[] {
                    0x55, 0x56, 0x60, 0x51,
                    0x16, 0x4F });

            AddPairedInteriors(
                new string[] {
                    "Clock Town Fairy", "Woodfall Fairy", "Snowhead Fairy",
                    "Great Bay Fairy", "Stone Tower Fairy" },
                new ushort[] {
                    0x4600, 0x4610, 0x4620,
                    0x4630, 0x4640 },
                new ushort[] {
                    0x26, 0x26, 0x26,
                    0x26,0x26 });

            AddSpawn("Woodfall Temple", 0xFFFF, "Woodfall Temple");
            AddSpawn("Snowhead Temple", 0xFFFF, "Snowhead Temple");
            AddSpawn("Great Bay Temple", 0xFFFF, "Great Bay Temple");
            AddSpawn("Inverted Stone Tower Temple", 0xFFFF, "Inverted Stone Tower Temple");
            PairDungeonSpawns();
            AddSpawn("Boss Chamber: Odolwa", 0x3800, "Woodfall Temple");
            AddSpawn("Boss Chamber: Goht", 0x8200, "Snowhead Temple");
            AddSpawn("Boss Chamber: Gyorg", 0xB800, "Great Bay Temple");
            AddSpawn("Boss Chamber: Twinmold", 0x6600, "Stone Tower Temple");
            AddSpawn("Dungeon Clear: Odolwa", 0x3010, "Deku Princess Room");
            AddSpawn("Dungeon Clear: Goht", 0xAE00, "Mountain Village Spring");
            AddSpawn("Dungeon Clear: Gyorg", 0x6A90, "Zora Cape");
            AddSpawn("Dungeon Clear: Twinmold", 0x2070, "Ikana Canyon");
            foreach (string s in new string[] { "Odolwa", "Goht", "Gyorg", "Twinmold" })
            {
                PairSpawns("Dungeon Clear: " + s, "Boss Chamber: " + s, "Boss");
            }
            AddSpawn("Moon", 0xC800, "Moon");
            AddSpawns("Moon",
                new string[] { "Woodfall Trial", "Snowhead Trial", "Great Bay Trial", "Stone Tower Trial" },
                new ushort[] { 0x4E00, 0x7800, 0x8800, 0xC600 },
                new ushort[] { 0x2A, 0x3F, 0x47, 0x66 }
            );
            PairMoonSpawns();
            AddSpawn("Majora Fight", 0x0200, "Moon");

            AddSpawn("Moon Crash Cutscene", 0x54C0, "Clock Tower");
            AddSpawn("Moon Crash Spawn", 0xC030, "Clock Tower");
            PairSpawns("Moon Crash Cutscene", "Moon Crash Spawn", "Special");
            AddSpawn("Twisted Hallway: Clock Tower", 0x2E10, "Twisted Hallway");
            AddSpawn("Clock Tower: Twisted Hallway", 0xC000, "Clock Tower");
            PairSpawns("Twisted Hallway: Clock Tower", "Clock Tower: Twisted Hallway", "Permanent");

            PairOverworldSpawns();
            PairDuplicateSpawns("Southern Swamp", "Healed");
            PairDuplicateSpawns("Mountain Village", "Spring");
            PairDuplicateSpawns("Twin Islands", "Spring");
            PairDuplicateSpawns("Goron Village", "Spring");
            PairTelescope("Curiosity Shop Backroom: Telescope", "Curiosity Shop: Telescope");
            PairTelescope("Observatory: Telescope", "Termina Field: Telescope");
            PairTelescope("Pirate Tunnel: Telescope", "Pirate's Fortress: Telescope");
        }

        private bool CanReturn(Dictionary<string, bool> AllowedSpawn, Spawn S)
        {
            bool result = true;
            foreach (Spawn X in S.ExitSpawn)
            {
                if (!AllowedSpawn.ContainsKey(X.Name) || !AllowedSpawn[X.Name])
                {
                    result = true;
                }
            }
            return result;
        }

        private Dictionary<string, List<string>> GetEntranceTypes()
        {
            Dictionary<string, List<string>> SpawnTypeSet= new Dictionary<string, List<string>>();
            bool ShuffleInteriors = _settings.RandomizeInteriorEntrances;
            bool ShuffleOverworld = _settings.RandomizeOverworldEntrances;
            bool ShuffleOwls = _settings.RandomizeOwlWarps;
            bool ShuffleOneWay = _settings.RandomizeOneWayEntrances;
            bool ShuffleMoon = _settings.RandomizeMoonTrials;
            bool ShuffleGrotto = _settings.RandomizeGrottoEntrances;
            bool ShuffleSpecial = false;
            bool MixEntrances = _settings.RandomizeEntranceInsanity;
            string SpawnType;
            foreach (Spawn S in GetSpawns())
            {
                if (S.Address == 0xFFFF) { continue; }
                SpawnType = S.Type == null ? "None" : S.Type;

                if (S.Name.Contains("Boss Chamber") && _settings.RandomizeDungeonEntrances)
                { SpawnType = "Interior"; }
                if (S.Name.Contains("Dungeon Clear") && _settings.RandomizeDungeonEntrances)
                { SpawnType = "Interior Exit"; }

                if (SpawnType == "Interior Exit" && ShuffleOneWay)
                { SpawnType = "Interior"; }
                if (SpawnType == "Telescope Spawn" && ShuffleOneWay)
                { SpawnType = "Telescope"; }
                if (SpawnType == "Moon" && ShuffleMoon)
                { SpawnType = "Interior"; }

                if (S.Name.Contains("Owl Warp"))
                { SpawnType = ShuffleOwls ? "Owl Warp" : "Permanent"; }
                if (S.Name.Contains("Grotto"))
                { SpawnType = ShuffleGrotto ? "One Way" : "Permanent"; }
                if (S.Type == null && S.ExitSpawn.Count == 0)
                { SpawnType = "One Way"; }

                if (!ShuffleInteriors && (SpawnType == "Interior" || SpawnType == "Telescope" || SpawnType == "Interior Exit" || SpawnType == "Telescope Spawn"))
                { SpawnType = "Permanent"; }
                if (!ShuffleOverworld && (SpawnType == "Overworld" || SpawnType == "Water"))
                { SpawnType = "Permanent"; }
                if (!ShuffleSpecial && SpawnType == "Special")
                { SpawnType = "Permanent"; }
                if (!ShuffleOneWay && SpawnType == "One Way")
                { SpawnType = "Permanent"; }

                if (MixEntrances && SpawnType != "Permanent" && SpawnType != "Dungeon")
                { SpawnType = "Insanity"; }

                foreach (string SpawnName in new List<string> { "Majora Fight" })
                {
                    if (S.Name == SpawnName)
                    {
                        SpawnType = "Permanent";
                    }
                }

                if (!SpawnTypeSet.ContainsKey(SpawnType))
                {
                    SpawnTypeSet.Add(SpawnType, new List<string>());
                }
                SpawnTypeSet[SpawnType].Add(S.Name);
            }
            return SpawnTypeSet;
        }

        private List<Dictionary<string,bool>> GetEntrancePools(Dictionary<string, List<string>> SpawnTypeSet)
        {
            List<Dictionary<string, bool>> SpawnSet = new List<Dictionary<string, bool>>();
            
            if (SpawnTypeSet.ContainsKey("Permanent"))
            {
                foreach (string SpawnName in SpawnTypeSet["Permanent"])
                {
                    ConnectEntrances(SpawnName, SpawnName, false);
                }
            }
            Dictionary<string, bool> TempPool;
            foreach (string Type in new List<string>() {
                "Overworld",
                "Water",
                "Interior",
                "Telescope",
                "One Way",
                "Owl Warp",
                "Insanity",
            })
            {
                if (SpawnTypeSet.ContainsKey(Type))
                {
                    TempPool = new Dictionary<string, bool>();
                    if (SpawnTypeSet[Type].Count > 1)
                    {
                        foreach (string SpawnName in SpawnTypeSet[Type])
                        {
                            TempPool.Add(SpawnName, true);
                        }
                        SpawnSet.Add(TempPool);
                    }
                    else if (SpawnTypeSet[Type].Count > 0)
                    {
                        ConnectEntrances(SpawnTypeSet[Type][0], SpawnTypeSet[Type][0], false);
                    }
                }
            }
            return SpawnSet;
        }

        private void ShuffleEntrances()
        {
            Dictionary<string, List<string>> SpawnTypeSet = GetEntranceTypes();
            List<Dictionary<string, bool>> SpawnSet = GetEntrancePools(SpawnTypeSet);
            List<Dictionary<string, bool>> ChosenSet = GetEntrancePools(SpawnTypeSet);
            int pool = 0;
            List<string> FillWorld = new List<string>(), SpawnTypePool = null;
            Predicate<Spawn> CanAdd = S => S != null && SpawnSet[pool].ContainsKey(S.Name) && SpawnSet[pool][S.Name];
            Predicate<Spawn> CanChoose = S =>
                S != null && ChosenSet[pool].ContainsKey(S.Name) && ChosenSet[pool][S.Name] &&
                (S.ExitSpawn.Count == 0 || S.ExitSpawn.Count > 0 && CanReturn(SpawnSet[pool], S));
            Spawn To, From;
            while (pool < SpawnSet.Count)
            {
                foreach (string Spawn in SpawnSet[pool].Keys)
                {
                    if (SpawnSet[pool][Spawn])
                    {
                        FillWorld.Add(Spawn);
                    }
                }
                while (FillWorld.Count > 0)
                {
                    From = GetSpawn(FillWorld[0]);
                    if (CanAdd.Invoke(From))
                    {
                        SpawnTypeSet.Values.ToList().ForEach(
                            TypeSet => { if (TypeSet.Contains(From.Name)) { SpawnTypePool = TypeSet; } });
                        To = ChooseNextEntrance(ChosenSet[pool], From.Type, SpawnTypePool, CanChoose);
                        if (To != null)
                        {
                            SpawnSet[pool][From.Name] = false;
                            ChosenSet[pool][To.Name] = false;
                            FillWorld.RemoveAt(0);
                            if (From.Name == "South Clock Town: Clock Tower")
                            {
                                ConnectEntrances(From.Name, To.Name, false);
                            }
                            else
                            {
                                ConnectEntrances(From.Name, To.Name, true);
                                if (To.ExitSpawn.Count > 0)
                                {
                                    foreach (Spawn Exit in To.ExitSpawn)
                                    {
                                        SpawnSet[pool][Exit.Name] = false;
                                        if (FillWorld.Contains(Exit.Name))
                                        {
                                            FillWorld.Remove(Exit.Name);
                                        }
                                    }
                                    foreach (Spawn Exit in From.ExitSpawn)
                                    {
                                        ChosenSet[pool][Exit.Name] = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Nowhere Left For '{0}' To Go", FillWorld[0]);
                            FillWorld.RemoveAt(0);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Not Allowed To Place {0}", FillWorld[0]);
                        FillWorld.RemoveAt(0);
                    }
                }
                pool++;
            }
        }

        private Spawn ChooseNextEntrance(Dictionary<string, bool> SpawnSet, string DepartureType, List<string> DeparturePool, Predicate<Spawn> CanAdd)
        {
            List<string> candidates = new List<string>();
            foreach (string SpawnName in SpawnSet.Keys)
            {
                if (SpawnSet[SpawnName])
                {
                    Spawn S = GetSpawn(SpawnName);
                    if (CanAdd.Invoke(S))
                    {
                        // need to keep this up to date with the section above
                        if (DepartureType == S.Type)
                        {
                            candidates.Add(S.Name);
                        }
                        else if (DeparturePool != null && DeparturePool.Contains(S.Name))
                        {
                            candidates.Add(S.Name);
                        }
                    }
                }
            }
            int n = _random.Next(candidates.Count);
            if (candidates.Count > 0)
            {
                return GetSpawn(candidates[n]);
            }
            return null;
        }

        private void AddEntranceToPath(Dictionary<string,List<string[]>> Paths, List<string> CurrentPath, string EntranceName)
        {
            string[] TempPath;
            if (!Paths.ContainsKey(EntranceName))
            {
                Paths.Add(EntranceName, new List<string[]>());
            }
            CurrentPath.Add(EntranceName);
            TempPath = new string[CurrentPath.Count];
            CurrentPath.CopyTo(TempPath);
            Paths[EntranceName].Add(TempPath);
            Debug.WriteLine(EntranceName);
        }

        private void CheckEntrances()
        {
            Stack<string> FillWorld = new Stack<string>();
            Dictionary<string, int> Visited = new Dictionary<string, int>();
            Dictionary<string, List<string[]>> Paths = new Dictionary<string, List<string[]>>();
            List<string> CurrentPath = new List<string>();
            foreach (Spawn S in GetSpawns())
            {
                Visited[S.Name] = 0;
            }
            foreach( string Scene in TerminaMap.Keys)
            {
                Visited[Scene] = 0;
            }
            Spawn Next;
            List<KeyValuePair<string, int>> Remaining;
            int group = 0;
            FillWorld.Push(GetShuffledSpawn("South Clock Town: Clock Tower").Name);
            AddEntranceToPath(Paths, CurrentPath, "Start");
            do
            {
                group++;
                while (FillWorld.Count > 0)
                {
                    // Add a Scene
                    if (ShuffledMap.Keys.Contains(FillWorld.Peek()))
                    {
                        string SceneName = FillWorld.Pop();
                        // Already Visited this scene, so remove the loop it causes
                        if (Visited.ContainsKey(SceneName) && Visited[SceneName] > 0 && CurrentPath.Contains(SceneName))
                        {
                            for( int p = CurrentPath.Count-1; p > CurrentPath.LastIndexOf(SceneName) ; p--)
                            {
                                Debug.WriteLine($"Already Visited: {SceneName} Remove: {CurrentPath[p]}");
                                CurrentPath.RemoveAt(p);
                            }
                        }
                        // Visit the Scene
                        else if (Visited.ContainsKey(SceneName))
                        {
                            Visited[SceneName] = group;
                            AddEntranceToPath(Paths, CurrentPath, SceneName);
                            if(ShuffledMap.ContainsKey(SceneName))
                            {
                                foreach (Spawn SpawnPoint in ShuffledMap[SceneName])
                                {
                                    if (SpawnPoint != null)
                                    {
                                        foreach (Spawn Exit in SpawnPoint.ExitSpawn)
                                        {
                                            if (Visited.ContainsKey(Exit.Name) && Visited[Exit.Name] == 0)
                                            {
                                                FillWorld.Push(Exit.Name);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Add a Scene Spawn
                    else if (Visited.ContainsKey(FillWorld.Peek()) && Visited[FillWorld.Peek()] == 0 && GetSpawn(FillWorld.Peek()) != null)
                    {
                        Next = GetSpawn(FillWorld.Pop());
                        Visited[Next.Name] = group;
                        if (Next != null)
                        {
                            foreach (Spawn Exit in Next.ExitSpawn)
                            {
                                AddEntranceToPath(Paths, CurrentPath, Next.Name + " -> " + Exit.Name);
                            }
                            FillWorld.Push(Next.SceneName);
                        }
                    }
                    else
                    {
                        FillWorld.Pop();
                    }
                }
                Remaining = Visited.Where(S => S.Value == 0)?.ToList();
                CurrentPath.Clear();
                if (Remaining.Count > 0)
                {
                    FillWorld.Push(Remaining[0].Key);
                }
            } while (FillWorld.Count > 0);
            Dictionary<string, List<string[]>> Connections = new Dictionary<string, List<string[]>>();
            Paths.Where(S=>TerminaMap.Keys.Contains(S.Key)).ToList().ForEach(S=>Connections.Add(S.Key,S.Value));
            foreach(string Scene in Connections.Keys)
            {
                Debug.WriteLine( Scene + ": " );
                int i = 1;
                foreach(string[] Path in Connections[Scene])
                {
                    Debug.WriteLine($"{i}");
                    foreach ( string S in Path)
                    {
                        Debug.WriteLine($"\t{S}");
                    }
                    i++;
                }
            }
        }

        private void AddSceneNameIndex(string sceneName, ushort sceneIndex)
        {
            if( !SceneNamesByIndex.ContainsKey(sceneIndex))
            {
                SceneNamesByIndex[sceneIndex] = new List<string>();
            }
            SceneNamesByIndex[sceneIndex].Add(sceneName);

        }

        private void AddDuplicateSceneSpawns(string[] SceneSpawns, string SceneName, ushort AddressPrefix, ushort SceneIndex, string SceneSuffix, ushort DuplicateAddressPrefix, ushort DuplicateSceneIndex)
        {
            AddSceneSpawns(SceneSpawns, AddressPrefix, SceneIndex, SceneName);
            AddSceneSpawns(SceneSpawns, DuplicateAddressPrefix, DuplicateSceneIndex, SceneName + " " + SceneSuffix);
        }

        private void AddSceneSpawns(string[] spawnName, ushort scenePrefix, ushort sceneIndex, string sceneName)
        {
            for (int i = 0; i < spawnName.Length; i++)
            {
                if (!spawnName[i].Equals(""))
                {
                    AddSpawn(sceneName + ": " + spawnName[i], (ushort)((scenePrefix << 8) + (i << 4)), sceneName);
                }
            }
            AddSceneNameIndex(sceneName, sceneIndex);
        }

        private void AddSpawns(string parent, string[] sceneName, ushort[] sceneSpawnAddress, ushort[] sceneIndex)
        {
            for (int i = 0; i < sceneName.Length; i++)
            {
                AddSpawn(parent + ": " + sceneName[i], sceneSpawnAddress[i], parent);
                AddSceneNameIndex(sceneName[i], sceneIndex[i]);
            }
        }

        private void AddSpawn(string Name, ushort Address, string Scene)
        {
            if (!TerminaMap.ContainsKey(Scene))
            {
                TerminaMap.Add(Scene, new List<Spawn>());
                ShuffledMap.Add(Scene, new List<Spawn>());
            }
            List<Spawn> sceneSpawns = TerminaMap[Scene];
            Spawn newSpawn = new Spawn(Name, Address, Scene);
            sceneSpawns.Add(newSpawn);
            sceneSpawns = ShuffledMap[Scene];
            sceneSpawns.Add(null);
        }

        private Spawn GetSpawn(string Name)
        {
            Spawn temp;
            foreach (List<Spawn> SceneSpawns in TerminaMap.Values)
            {
                temp = SceneSpawns.Find(u => Name.Equals(u.Name));
                if (temp != null)
                {
                    return temp;
                }
            }
            return null;
        }

        private void PairInteriorEntrance(string OutdoorEntrance, string IndoorEntrance)
        {
            PairSpawns(OutdoorEntrance, IndoorEntrance, "Interior");
            GetSpawn(OutdoorEntrance).Type = "Interior Exit";

        }

        private void PairSingleSpawn(string From, string To, string Type)
        {
            Spawn F = GetSpawn(From);
            Spawn T = GetSpawn(To);
            if (F != null && T != null)
            {
                F.Type = Type;
                T.Type = Type;
                if (F.ExitSpawn != null && T.ExitSpawn != null)
                {
                    if (!F.ExitSpawn.Contains(T))
                    {
                        F.ExitSpawn.Add(T);
                    }
                }
            }
        }

        private void PairSpawns(string From, string To, string Type)
        {
            PairSingleSpawn(From, To, Type);
            PairSingleSpawn(To, From, Type);
        }

        private void PairDuplicateSpawns(string Scene, string DuplicateSuffix)
        {
            List<Spawn> SceneSpawns = new List<Spawn>();
            foreach(Spawn Spawn in TerminaMap[Scene])
            {
                if( Spawn.ExitSpawn.Count == 1)
                {
                    SceneSpawns.Add(Spawn.ExitSpawn[0]);
                }
            }
            foreach(Spawn Spawn in SceneSpawns)
            {
                if (Spawn.ExitSpawn.Count == 1)
                {
                    string Original = Spawn.ExitSpawn[0].Name;
                    string Duplicate = Original.Insert(Original.IndexOf(":"), " " + DuplicateSuffix);
                    PairSingleSpawn(Spawn.Name, Duplicate, Spawn.Type);
                }
            }
        }

        private void PairSingleInterior(string InteriorScene, string OuterScene, ushort InteriorAddress, ushort OuterAddress)
        {
            AddSpawn(OuterScene + ": " + InteriorScene, OuterAddress, OuterScene);
            AddPairedInteriors(new string[] { InteriorScene }, new ushort[] { InteriorAddress }, new ushort[] { 0xFFFF });
        }

        private void PairTelescope(string SpawnPoint, string Telescope)
        {
            Spawn Room = GetSpawn(SpawnPoint);
            Spawn Scope = GetSpawn(Telescope);
            if (Room != null && Scope != null)
            {
                Scope.Type = "Telescope";
                Room.Type = "Telescope Spawn";
                if (!Scope.ExitSpawn.Contains(Room))
                {
                    Scope.ExitSpawn.Add(Room);
                }
            }
        }

        private void AddPairedInteriors(string[] scene, ushort[] spawnAddress, ushort[] sceneAddress)
        {
            for (int i = 0; i < scene.Length; i++)
            {
                string spawnName = scene[i];
                AddSpawn(spawnName, spawnAddress[i], scene[i]);
                if( sceneAddress[i] != 0xFFFF)
                {
                    AddSceneNameIndex(spawnName, sceneAddress[i]);
                }
                string to = "";
                foreach (Spawn s in GetSpawns())
                {
                    if (!spawnName.Equals(s.Name) && s.Name.Contains(spawnName))
                    {
                        PairInteriorEntrance(s.Name, spawnName);
                    }
                }
                if (!to.Equals(""))
                {
                }
            }
        }

        private void AddGrottos(string[] GrottoName, ushort[] Address, string Scene)
        {
            for (int i = 0; i < GrottoName.Length; i++)
            {
                string SpawnName = "Grotto: " + GrottoName[i];
                AddSpawn(SpawnName, Address[i], Scene);
            }
        }

        private void PairOverworldSpawns()
        {
            List<Spawn> SpawnSet = GetSpawns();
            Dictionary<Spawn, string> SpawnPoint = new Dictionary<Spawn, string>();
            Dictionary<Spawn, string> SpawnExit = new Dictionary<Spawn, string>();
            int sep;
            foreach (Spawn S in SpawnSet)
            {
                sep = S.Name.IndexOf(':');
                if (sep != -1)
                {
                    SpawnPoint[S] = S.Name.Substring(0, sep);
                    SpawnExit[S] = S.Name.Substring(sep + 2);
                }
            }
            int j;
            for (int i = 0; i < SpawnSet.Count; i++)
            {
                if (SpawnSet[i].ExitSpawn.Count == 0 && SpawnPoint.ContainsKey(SpawnSet[i]) && SpawnExit.ContainsKey(SpawnSet[i]))
                {
                    j = SpawnSet.FindIndex((S) =>
                    {
                        if (!SpawnPoint.ContainsKey(S) || !SpawnExit.ContainsKey(S))
                        {
                            return false;
                        }
                        return SpawnPoint[S].Equals(SpawnExit[SpawnSet[i]]) && SpawnExit[S].Equals(SpawnPoint[SpawnSet[i]]);
                    });
                    if (j != -1)
                    {
                        if (SpawnSet[i].Type != "" || SpawnSet[j].Type != "")
                        {
                            PairSpawns(SpawnSet[i].Name, SpawnSet[j].Name, "Overworld");
                        }
                    }
                }
            }
        }

        private void PairDungeonSpawns()
        {
            int[] DungeonOrder = _randomized.NewDestinationIndices;
            string[] dungeons = new string[] { "Woodfall", "Snowhead", "Inverted Stone Tower", "Great Bay" };
            string[] areas = new string[] { "Woodfall", "Snowhead", "Inverted Stone Tower", "Zora Cape" };
            int j;
            for(int i = 0; i < dungeons.Length; i++)
            {
                j = _settings.RandomizeDungeonEntrances ? DungeonOrder[i] : i;
                PairSpawns(areas[i] + ": Temple", dungeons[j] + " Temple", "Dungeon");
                ConnectEntrances(areas[i] + ": Temple", dungeons[j] + " Temple", true);
            }
        }

        private void PairMoonSpawns()
        {
            foreach(Spawn S in TerminaMap["Moon"])
            {
                if(S.Name != "Moon")
                {
                    PairSingleSpawn(S.Name, "Moon", "Moon");
                }
            }
        }

        private Spawn GetShuffledSpawn(string Spawn)
        {
            Spawn S = GetSpawn(Spawn);
            int i;
            foreach (string SceneSpawns in TerminaMap.Keys)
            {
                if (TerminaMap[SceneSpawns].Contains(S))
                {
                    i = TerminaMap[SceneSpawns].FindIndex(T => T == S);
                    return ShuffledMap[SceneSpawns][i];
                }
            }
            return null;
        }

        private void SetShuffledSpawn(Spawn f, Spawn t)
        {
            int shuffleIndex = -1;
            List<Spawn> temp = TerminaMap[f.SceneName];
            if (temp != null && temp.Contains(f))
            {
                shuffleIndex = temp.FindIndex(S => S == f);
                temp = ShuffledMap[f.SceneName];
            }
            else
            {
                temp = null;
                foreach (string SceneSpawns in TerminaMap.Keys)
                {
                    if (TerminaMap[SceneSpawns].Contains(f))
                    {
                        temp = ShuffledMap[SceneSpawns];
                        shuffleIndex = temp.FindIndex(S => S == f);
                        break;
                    }
                }
            }
            if (temp != null && shuffleIndex != -1)
            {
                temp[shuffleIndex] = t;
            }
        }

        private void ConnectEntrances(string from, string to, bool connectReverse)
        {
            Spawn f = GetSpawn(from);
            Spawn t = GetSpawn(to);
            if (f != null && t != null)
            {
                SetShuffledSpawn(f, t);
                if (connectReverse && f.ExitSpawn.Count > 0 && t.ExitSpawn.Count > 0)
                {
                    foreach (Spawn ToExit in t.ExitSpawn)
                    {
                        foreach (Spawn FromExit in f.ExitSpawn)
                        {
                            SetShuffledSpawn(ToExit, FromExit);
                        }
                    }
                }
            }
        }

        private List<Spawn> GetSpawns()
        {
            List<Spawn> Spawns = new List<Spawn>();
            foreach (List<Spawn> Scene in TerminaMap.Values)
            {
                Spawns.AddRange(Scene);
            }
            return Spawns;
        }

        public void FinalizeEntrances()
        {
            List<Spawn> OriginalScene;
            Dictionary<Spawn, Spawn> EntranceShuffle;
            _randomized.EntranceList = new Dictionary<int, ushort[]>();
            _randomized.ShuffledEntranceList = new Dictionary<int, ushort[]>();
            _randomized.EntranceSpoilers = new List<SpoilerEntrance>();

            Spawn Spawn, Exit, ShuffledExit;
            bool WasPlaced;
            ushort[] SceneExitList, ShuffledSceneExitList;
            foreach (ushort sceneIndex in SceneNamesByIndex.Keys)
            {
                EntranceShuffle = new Dictionary<Spawn, Spawn>();
                foreach (string sceneName in SceneNamesByIndex[sceneIndex])
                {
                    if( TerminaMap.ContainsKey(sceneName))
                    {
                        OriginalScene = TerminaMap[sceneName];
                        for (int s = 0; s < OriginalScene.Count; s++)
                        {
                            Spawn = OriginalScene[s];
                            if (Spawn.ExitSpawn.Count == 1)
                            {
                                Exit = Spawn.ExitSpawn[0];
                                ShuffledExit = GetShuffledSpawn(Exit.Name);
                                if (Spawn.Address != 0xFFFF)
                                {
                                    WasPlaced = ShuffledExit != null && ShuffledExit.Address != 0xFFFF;
                                    if (WasPlaced)
                                    {
                                        EntranceShuffle.Add(Exit, ShuffledExit);
                                    }
                                    else
                                    {
                                        EntranceShuffle.Add(Exit, Exit);
                                    }
                                    _randomized.EntranceSpoilers.Add(new SpoilerEntrance(Exit, ShuffledExit, WasPlaced));
                                }
                            }
                        }
                    }
                }

                SceneExitList = new ushort[EntranceShuffle.Keys.Count];
                _randomized.EntranceList[sceneIndex] = SceneExitList;
                ShuffledSceneExitList = new ushort[EntranceShuffle.Keys.Count];
                _randomized.ShuffledEntranceList[sceneIndex] = ShuffledSceneExitList;
                int i = 0;
                foreach (Spawn Entrance in EntranceShuffle.Keys)
                {
                    SceneExitList[i] = Entrance.Address;
                    ShuffledSceneExitList[i] = EntranceShuffle[Entrance].Address;
                    i++;
                }
            }
        }
        #endregion

        #region Sequences and BGM

        private void BGMShuffle()
        {
            while (RomData.TargetSequences.Count > 0)
            {
                List<SequenceInfo> Unassigned = RomData.SequenceList.FindAll(u => u.Replaces == -1);

                int targetIndex = Random.Next(RomData.TargetSequences.Count);
                var targetSequence = RomData.TargetSequences[targetIndex];

                while (true)
                {
                    int unassignedIndex = Random.Next(Unassigned.Count);

                    if (Unassigned[unassignedIndex].Name.StartsWith("mm")
                        & (Random.Next(100) < 50))
                    {
                        continue;
                    }

                    for (int i = 0; i < Unassigned[unassignedIndex].Type.Count; i++)
                    {
                        if (targetSequence.Type.Contains(Unassigned[unassignedIndex].Type[i]))
                        {
                            Unassigned[unassignedIndex].Replaces = targetSequence.Replaces;
                            Debug.WriteLine(Unassigned[unassignedIndex].Name + " -> " + targetSequence.Name);
                            RomData.TargetSequences.RemoveAt(targetIndex);
                            break;
                        }
                        else if (i + 1 == Unassigned[unassignedIndex].Type.Count)
                        {
                            if ((Random.Next(30) == 0)
                                && ((Unassigned[unassignedIndex].Type[0] & 8) == (targetSequence.Type[0] & 8))
                                && (Unassigned[unassignedIndex].Type.Contains(10) == targetSequence.Type.Contains(10))
                                && (!Unassigned[unassignedIndex].Type.Contains(16)))
                            {
                                Unassigned[unassignedIndex].Replaces = targetSequence.Replaces;
                                Debug.WriteLine(Unassigned[unassignedIndex].Name + " -> " + targetSequence.Name);
                                RomData.TargetSequences.RemoveAt(targetIndex);
                                break;
                            }
                        }
                    }

                    if (Unassigned[unassignedIndex].Replaces != -1)
                    {
                        break;
                    }
                }
            }

            RomData.SequenceList.RemoveAll(u => u.Replaces == -1);
        }

        private void SortBGM()
        {
            if (!_settings.RandomizeBGM)
            {
                return;
            }

            SequenceUtils.ReadSequenceInfo();
            BGMShuffle();
        }

        #endregion

        private void SetTatlColour()
        {
            if (_settings.TatlColorSchema == TatlColorSchema.Rainbow)
            {
                for (int i = 0; i < 10; i++)
                {
                    byte[] c = new byte[4];
                    Random.NextBytes(c);

                    if ((i % 2) == 0)
                    {
                        c[0] = 0xFF;
                    }
                    else
                    {
                        c[0] = 0;
                    }

                    Values.TatlColours[4, i] = BitConverter.ToUInt32(c, 0);
                };
            };
        }

        private void PrepareRulesetItemData()
        {
            ItemList = new List<ItemObject>();

            if (_settings.LogicMode == LogicMode.Casual
                || _settings.LogicMode == LogicMode.Glitched
                || _settings.LogicMode == LogicMode.UserLogic)
            {
                string[] data = ReadRulesetFromResources();
                PopulateItemListFromLogicData(data);
            }
            else
            {
                PopulateItemListWithoutLogic();
            }
        }

        /// <summary>
        /// Populates item list without logic. Default TimeAvailable = 63
        /// </summary>
        private void PopulateItemListWithoutLogic()
        {
            for (var i = 0; i < Items.TotalNumberOfItems; i++)
            {
                var currentItem = new ItemObject
                {
                    ID = i,
                    TimeAvailable = 63
                };

                ItemList.Add(currentItem);
            }
        }

        /// <summary>
        /// Populates the item list using the lines from a logic file, processes them 4 lines per item. 
        /// </summary>
        /// <param name="data">The lines from a logic file</param>
        private void PopulateItemListFromLogicData(string[] data)
        {
            if (Migrator.GetVersion(data.ToList()) != Migrator.CurrentVersion)
            {
                throw new InvalidDataException("Logic file is out of date. Open it in the Logic Editor to bring it up to date.");
            }

            int itemId = 0;
            int lineNumber = 0;

            var currentItem = new ItemObject();

            // Process lines in groups of 4
            foreach (string line in data)
            {
                if (line.Contains("-"))
                {
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
                }

                lineNumber++;

                if (lineNumber == 4)
                {
                    currentItem.ID = itemId;
                    ItemList.Add(currentItem);

                    currentItem = new ItemObject();

                    itemId++;
                    lineNumber = 0;
                }
            }
        }

        private void ProcessConditionalsForItem(ItemObject currentItem, string line)
        {
            List<List<int>> conditional = new List<List<int>>();

            if (line == "")
            {
                currentItem.Conditionals = null;
            }
            else
            {
                foreach (string conditions in line.Split(';'))
                {
                    int[] conditionaloption = Array.ConvertAll(conditions.Split(','), int.Parse);
                    conditional.Add(conditionaloption.ToList());
                }
                currentItem.Conditionals = conditional;
            }
        }

        private void ProcessDependenciesForItem(ItemObject currentItem, string line)
        {
            List<int> dependencies = new List<int>();

            if (line == "")
            {
                currentItem.DependsOnItems = null;
            }
            else
            {
                foreach (string dependency in line.Split(','))
                {
                    dependencies.Add(Convert.ToInt32(dependency));
                }
                currentItem.DependsOnItems = dependencies;
            }
        }

        public void SeedRNG()
        {
            Random = new Random(_settings.Seed);
        }

        private string[] ReadRulesetFromResources()
        {
            string[] lines = null;
            var mode = _settings.LogicMode;

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
                using (StreamReader Req = new StreamReader(File.Open(_settings.UserLogicFileName, FileMode.Open)))
                {
                    lines = Req.ReadToEnd().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                }
            }

            return lines;
        }

        private Dependence CheckDependence(int CurrentItem, int Target, List<int> dependencyPath)
        {
            Debug.WriteLine($"CheckDependence({CurrentItem}, {Target})");
            if (ItemList[CurrentItem].TimeNeeded == 0
                && !ItemList.Any(io => (io.Conditionals?.Any(c => c.Contains(CurrentItem)) ?? false) || (io.DependsOnItems?.Contains(CurrentItem) ?? false)))
            {
                return Dependence.NotDependent;
            }

            // permanent items ignore dependencies of Blast Mask check
            if (Target == Items.MaskBlast && !ItemUtils.IsTemporaryItem(CurrentItem))
            {
                return Dependence.NotDependent;
            }

            //check timing
            if (ItemList[CurrentItem].TimeNeeded != 0 && dependencyPath.Skip(1).All(p => ItemUtils.IsFakeItem(p) || ItemUtils.IsTemporaryItem(ItemList.Single(i => i.ReplacesItemId == p).ID)))
            {
                if ((ItemList[CurrentItem].TimeNeeded & ItemList[Target].TimeAvailable) == 0)
                {
                    Debug.WriteLine($"{CurrentItem} is needed at {ItemList[CurrentItem].TimeNeeded} but {Target} is only available at {ItemList[Target].TimeAvailable}");
                    return Dependence.Dependent;
                }
            }

            if (ItemList[Target].HasConditionals)
            {
                if (ItemList[Target].Conditionals
                    .FindAll(u => u.Contains(CurrentItem)).Count == ItemList[Target].Conditionals.Count)
                {
                    Debug.WriteLine($"All conditionals of {Target} contains {CurrentItem}");
                    return Dependence.Dependent;
                }

                if (ItemList[CurrentItem].HasCannotRequireItems)
                {
                    for (int i = 0; i < ItemList[CurrentItem].CannotRequireItems.Count; i++)
                    {
                        if (ItemList[Target].Conditionals
                            .FindAll(u => u.Contains(ItemList[CurrentItem].CannotRequireItems[i])
                            || u.Contains(CurrentItem)).Count == ItemList[Target].Conditionals.Count)
                        {
                            Debug.WriteLine($"All conditionals of {Target} cannot be required by {CurrentItem}");
                            return Dependence.Dependent;
                        }
                    }
                }

                int k = 0;
                var circularDependencies = new List<int>();
                for (int i = 0; i < ItemList[Target].Conditionals.Count; i++)
                {
                    bool match = false;
                    for (int j = 0; j < ItemList[Target].Conditionals[i].Count; j++)
                    {
                        int d = ItemList[Target].Conditionals[i][j];
                        if (!ItemUtils.IsFakeItem(d) && !ItemList[d].ReplacesAnotherItem && d != CurrentItem)
                        {
                            continue;
                        }

                        int[] check = new int[] { Target, i, j };

                        if (ItemList[d].ReplacesAnotherItem)
                        {
                            d = ItemList[d].ReplacesItemId;
                        }
                        if (d == CurrentItem)
                        {
                            DependenceChecked[d] = Dependence.Dependent;
                        }
                        else
                        {
                            if (dependencyPath.Contains(d))
                            {
                                DependenceChecked[d] = Dependence.Circular(d);
                            }
                            if (!DependenceChecked.ContainsKey(d) || (DependenceChecked[d].Type == DependenceType.Circular && !DependenceChecked[d].ItemIds.All(id => dependencyPath.Contains(id))))
                            {
                                var childPath = dependencyPath.ToList();
                                childPath.Add(d);
                                DependenceChecked[d] = CheckDependence(CurrentItem, d, childPath);
                            }
                        }

                        if (DependenceChecked[d].Type != DependenceType.NotDependent)
                        {
                            if (!dependencyPath.Contains(d) && DependenceChecked[d].Type == DependenceType.Circular && DependenceChecked[d].ItemIds.All(id => id == d))
                            {
                                DependenceChecked[d] = Dependence.Dependent;
                            }
                            if (DependenceChecked[d].Type == DependenceType.Dependent)
                            {
                                if (!ConditionRemoves.Any(c => c.SequenceEqual(check)))
                                {
                                    ConditionRemoves.Add(check);
                                }
                            }
                            else
                            {
                                circularDependencies = circularDependencies.Union(DependenceChecked[d].ItemIds).ToList();
                            }
                            if (!match)
                            {
                                k++;
                                match = true;
                            }
                        }
                    }
                }

                if (k == ItemList[Target].Conditionals.Count)
                {
                    if (circularDependencies.Any())
                    {
                        return Dependence.Circular(circularDependencies.ToArray());
                    }
                    Debug.WriteLine($"All conditionals of {Target} failed dependency check for {CurrentItem}.");
                    return Dependence.Dependent;
                }
            }

            if (ItemList[Target].DependsOnItems == null)
            {
                return Dependence.NotDependent;
            }

            //cycle through all things
            for (int i = 0; i < ItemList[Target].DependsOnItems.Count; i++)
            {
                int dependency = ItemList[Target].DependsOnItems[i];
                if (dependency == CurrentItem)
                {
                    Debug.WriteLine($"{Target} has direct dependence on {CurrentItem}");
                    return Dependence.Dependent;
                }

                if (ItemList[CurrentItem].HasCannotRequireItems)
                {
                    for (int j = 0; j < ItemList[CurrentItem].CannotRequireItems.Count; j++)
                    {
                        if (ItemList[Target].DependsOnItems.Contains(ItemList[CurrentItem].CannotRequireItems[j]))
                        {
                            Debug.WriteLine($"Dependence {ItemList[CurrentItem].CannotRequireItems[j]} of {Target} cannot be required by {CurrentItem}");
                            return Dependence.Dependent;
                        }
                    }
                }

                if (ItemUtils.IsFakeItem(dependency)
                    || ItemList[dependency].ReplacesAnotherItem)
                {
                    if (ItemList[dependency].ReplacesAnotherItem)
                    {
                        dependency = ItemList[dependency].ReplacesItemId;
                    }

                    if (dependencyPath.Contains(dependency))
                    {
                        DependenceChecked[dependency] = Dependence.Circular(dependency);
                        return DependenceChecked[dependency];
                    }
                    if (!DependenceChecked.ContainsKey(dependency) || (DependenceChecked[dependency].Type == DependenceType.Circular && !DependenceChecked[dependency].ItemIds.All(id => dependencyPath.Contains(id))))
                    {
                        var childPath = dependencyPath.ToList();
                        childPath.Add(dependency);
                        DependenceChecked[dependency] = CheckDependence(CurrentItem, dependency, childPath);
                    }
                    if (DependenceChecked[dependency].Type != DependenceType.NotDependent)
                    {
                        if (DependenceChecked[dependency].Type == DependenceType.Circular && DependenceChecked[dependency].ItemIds.All(id => id == dependency))
                        {
                            DependenceChecked[dependency] = Dependence.Dependent;
                        }
                        Debug.WriteLine($"{CurrentItem} is dependent on {dependency}");
                        return DependenceChecked[dependency];
                    }
                }
            }

            return Dependence.NotDependent;
        }

        private void RemoveConditionals(int CurrentItem)
        {
            for (int i = 0; i < ConditionRemoves.Count; i++)
            {
                int x = ConditionRemoves[i][0];
                int y = ConditionRemoves[i][1];
                int z = ConditionRemoves[i][2];
                ItemList[x].Conditionals[y] = null;
            }

            for (int i = 0; i < ConditionRemoves.Count; i++)
            {
                int x = ConditionRemoves[i][0];
                int y = ConditionRemoves[i][1];
                int z = ConditionRemoves[i][2];

                for (int j = 0; j < ItemList[x].Conditionals.Count; j++)
                {
                    if (ItemList[x].Conditionals[j] != null)
                    {
                        for (int k = 0; k < ItemList[x].Conditionals[j].Count; k++)
                        {
                            int d = ItemList[x].Conditionals[j][k];

                            if (!ItemList[x].HasCannotRequireItems)
                            {
                                ItemList[x].CannotRequireItems = new List<int>();
                            }
                            if (!ItemList[d].CannotRequireItems.Contains(CurrentItem))
                            {
                                ItemList[d].CannotRequireItems.Add(CurrentItem);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].Conditionals != null)
                {
                    ItemList[i].Conditionals.RemoveAll(u => u == null);
                }
            }

            /*
            for (int i = 0; i < ConditionRemoves.Count; i++)
            {
                for (int j = 0; j < ItemList[ConditionRemoves[i][0]].Conditional[ConditionRemoves[i][1]].Count; j++)
                {
                    int d = ItemList[ConditionRemoves[i][0]].Conditional[ConditionRemoves[i][1]][j];
                    if (ItemList[d].Cannot_Require == null)
                    {
                        ItemList[d].Cannot_Require = new List<int>();
                    };
                    ItemList[d].Cannot_Require.Add(CurrentItem);
                    if (ItemList[ConditionRemoves[i][0]].Dependence == null)
                    {
                        ItemList[ConditionRemoves[i][0]].Dependence = new List<int>();
                    };
                    ItemList[ConditionRemoves[i][0]].Dependence.Add(d);
                };
                ItemList[ConditionRemoves[i][0]].Conditional[ConditionRemoves[i][1]] = null;
            };
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].Conditional != null)
                {
                    if (ItemList[i].Conditional.Contains(null))
                    {
                        ItemList[i].Conditional = null;
                    };
                };
            };
            */
        }

        private void UpdateConditionals(int CurrentItem, int Target)
        {
            if (!ItemList[Target].HasConditionals)
            {
                return;
            }

            //if ((Target == 114) || (Target == 115))
            //{
            //    return;
            //};
            /*
            if (ItemList[Target].Cannot_Require != null)
            {
                for (int i = 0; i < ItemList[CurrentItem].Cannot_Require.Count; i++)
                {
                    ItemList[Target].Conditional.RemoveAll(u => u.Contains(ItemList[CurrentItem].Cannot_Require[i]));
                };
            };
            ItemList[Target].Conditional.RemoveAll(u => u.Contains(CurrentItem));
            if (ItemList[Target].Conditional.Count == 0)
            {
                return;
            };
            */
            if (ItemList[Target].Conditionals.Count == 1)
            {
                for (int i = 0; i < ItemList[Target].Conditionals[0].Count; i++)
                {
                    if (!ItemList[Target].HasDependencies)
                    {
                        ItemList[Target].DependsOnItems = new List<int>();
                    }

                    int j = ItemList[Target].Conditionals[0][i];
                    if (!ItemList[Target].DependsOnItems.Contains(j))
                    {
                        ItemList[Target].DependsOnItems.Add(j);
                    }
                    if (!ItemList[j].HasCannotRequireItems)
                    {
                        ItemList[j].CannotRequireItems = new List<int>();
                    }
                    if (!ItemList[j].CannotRequireItems.Contains(CurrentItem))
                    {
                        ItemList[j].CannotRequireItems.Add(CurrentItem);
                    }
                }
                ItemList[Target].Conditionals.RemoveAt(0);
            }
            else
            {
                //check if all conditions have a common item
                for (int i = 0; i < ItemList[Target].Conditionals[0].Count; i++)
                {
                    int testitem = ItemList[Target].Conditionals[0][i];
                    if (ItemList[Target].Conditionals.FindAll(u => u.Contains(testitem)).Count == ItemList[Target].Conditionals.Count)
                    {
                        // require this item and remove from conditions
                        if (!ItemList[Target].HasDependencies)
                        {
                            ItemList[Target].DependsOnItems = new List<int>();
                        }
                        if (!ItemList[Target].DependsOnItems.Contains(testitem))
                        {
                            ItemList[Target].DependsOnItems.Add(testitem);
                        }
                        for (int j = 0; j < ItemList[Target].Conditionals.Count; j++)
                        {
                            ItemList[Target].Conditionals[j].Remove(testitem);
                        }

                        break;
                    }
                }
                //for (int i = 0; i < ItemList[Target].Conditional.Count; i++)
                //{
                //    for (int j = 0; j < ItemList[Target].Conditional[i].Count; j++)
                //    {
                //        int k = ItemList[Target].Conditional[i][j];
                //        if (ItemList[k].Cannot_Require == null)
                //        {
                //            ItemList[k].Cannot_Require = new List<int>();
                //        };
                //        ItemList[k].Cannot_Require.Add(CurrentItem);
                //    };
                //};
            };
        }

        private void AddConditionals(int target, int currentItem, int d)
        {
            List<List<int>> baseConditionals = ItemList[target].Conditionals;

            if (baseConditionals == null)
            {
                baseConditionals = new List<List<int>>();
            }

            ItemList[target].Conditionals = new List<List<int>>();
            foreach (List<int> conditions in ItemList[d].Conditionals)
            {
                if (!conditions.Contains(currentItem))
                {
                    List<List<int>> newConditional = new List<List<int>>();
                    if (baseConditionals.Count == 0)
                    {
                        newConditional.Add(conditions);
                    }
                    else
                    {
                        foreach (List<int> baseConditions in baseConditionals)
                        {
                            newConditional.Add(baseConditions.Concat(conditions).ToList());
                        }
                    }

                    ItemList[target].Conditionals.AddRange(newConditional);
                }
            }
        }

        private void CheckConditionals(int currentItem, int target, List<int> dependencyPath)
        {
            if (target == Items.MaskBlast)
            {
                if (!ItemUtils.IsTemporaryItem(currentItem))
                {
                    ItemList[target].DependsOnItems = null;
                }
            }

            ConditionsChecked.Add(target);
            UpdateConditionals(currentItem, target);

            if (!ItemList[target].HasDependencies)
            {
                return;
            }

            for (int i = 0; i < ItemList[target].DependsOnItems.Count; i++)
            {
                int dependency = ItemList[target].DependsOnItems[i];
                if (!ItemList[dependency].HasCannotRequireItems)
                {
                    ItemList[dependency].CannotRequireItems = new List<int>();
                }
                if (!ItemList[dependency].CannotRequireItems.Contains(currentItem))
                {
                    ItemList[dependency].CannotRequireItems.Add(currentItem);
                }
                if (ItemUtils.IsFakeItem(dependency) || ItemList[dependency].ReplacesAnotherItem)
                {
                    if (ItemList[dependency].ReplacesAnotherItem)
                    {
                        dependency = ItemList[dependency].ReplacesItemId;
                    }

                    if (!ConditionsChecked.Contains(dependency))
                    {
                        var childPath = dependencyPath.ToList();
                        childPath.Add(dependency);
                        CheckConditionals(currentItem, dependency, childPath);
                    }
                }
                else if (ItemList[currentItem].TimeNeeded != 0 && ItemUtils.IsTemporaryItem(dependency) && dependencyPath.Skip(1).All(p => ItemUtils.IsFakeItem(p) || ItemUtils.IsTemporaryItem(ItemList.Single(j => j.ReplacesItemId == p).ID)))
                {
                    ItemList[dependency].TimeNeeded &= ItemList[currentItem].TimeNeeded;
                }
            }

            ItemList[target].DependsOnItems.RemoveAll(u => u == -1);
        }

        private bool CheckMatch(int currentItem, int target)
        {
            if (ForbiddenPlacedAt.ContainsKey(currentItem)
                && ForbiddenPlacedAt[currentItem].Contains(target))
            {
                Debug.WriteLine($"{currentItem} forbidden from being placed at {target}");
                return false;
            }

            if (ForbiddenReplacedBy.ContainsKey(target) && ForbiddenReplacedBy[target].Contains(currentItem))
            {
                Debug.WriteLine($"{target} forbids being replaced by {currentItem}");
                return false;
            }

            if (ItemUtils.IsTemporaryItem(currentItem) && ItemUtils.IsMoonItem(target))
            {
                Debug.WriteLine($"{currentItem} cannot be placed on the moon.");
                return false;
            }

            //check direct dependence
            ConditionRemoves = new List<int[]>();
            DependenceChecked = new Dictionary<int, Dependence> { { target, new Dependence { Type = DependenceType.Dependent } } };
            var dependencyPath = new List<int> { target };

            if (CheckDependence(currentItem, target, dependencyPath).Type != DependenceType.NotDependent)
            {
                return false;
            }

            //check conditional dependence
            RemoveConditionals(currentItem);
            ConditionsChecked = new List<int>();
            CheckConditionals(currentItem, target, dependencyPath);
            return true;
        }

        private void PlaceItem(int currentItem, List<int> targets)
        {
            if (ItemList[currentItem].ReplacesAnotherItem)
            {
                return;
            }

            var availableItems = targets.ToList();

            while (true)
            {
                if (availableItems.Count == 0)
                {
                    throw new Exception($"Unable to place {Items.ITEM_NAMES[currentItem]} anywhere.");
                }

                int targetItem = 0;
                if (currentItem > Items.SongOath && availableItems.Contains(0))
                {
                    targetItem = Random.Next(1, availableItems.Count);
                }
                else
                {
                    targetItem = Random.Next(availableItems.Count);
                }

                Debug.WriteLine($"----Attempting to place {Items.ITEM_NAMES[currentItem]} at {Items.ITEM_NAMES[availableItems[targetItem]]}.---");

                if (CheckMatch(currentItem, availableItems[targetItem]))
                {
                    ItemList[currentItem].ReplacesItemId = availableItems[targetItem];

                    Debug.WriteLine($"----Placed {Items.ITEM_NAMES[currentItem]} at {Items.ITEM_NAMES[ItemList[currentItem].ReplacesItemId]}----");

                    targets.Remove(availableItems[targetItem]);
                    return;
                }
                else
                {
                    Debug.WriteLine($"----Failed to place {Items.ITEM_NAMES[currentItem]} at {Items.ITEM_NAMES[availableItems[targetItem]]}----");
                    availableItems.RemoveAt(targetItem);
                }
            }
        }

        private void RandomizeItems()
        {
            if (_settings.UseCustomItemList)
            {
                SetupCustomItems();
            }
            else
            {
                Setup();
            }

            var itemPool = new List<int>();

            AddAllItems(itemPool);

            PlaceQuestItems(itemPool);
            PlaceTradeItems(itemPool);
            PlaceDungeonItems(itemPool);
            PlaceFreeItem(itemPool);
            PlaceUpgrades(itemPool);
            PlaceSongs(itemPool);
            PlaceMasks(itemPool);
            PlaceRegularItems(itemPool);
            PlaceShopItems(itemPool);
            PlaceMoonItems(itemPool);
            PlaceHeartpieces(itemPool);
            PlaceOther(itemPool);
            PlaceTingleMaps(itemPool);

            _randomized.ItemList = ItemList;
        }

        /// <summary>
        /// Places moon items in the randomization pool.
        /// </summary>
        private void PlaceMoonItems(List<int> itemPool)
        {
            for (int i = Items.HeartPieceDekuTrial; i <= Items.MaskFierceDeity; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places tingle maps in the randomization pool.
        /// </summary>
        private void PlaceTingleMaps(List<int> itemPool)
        {
            for (int i = Items.ItemTingleMapTown; i <= Items.ItemTingleMapStoneTower; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places other chests and grottos in the randomization pool.
        /// </summary>
        /// <param name="itemPool"></param>
        private void PlaceOther(List<int> itemPool)
        {
            for (int i = Items.ChestLensCaveRedRupee; i <= Items.ChestSouthClockTownPurpleRupee; i++)
            {
                PlaceItem(i, itemPool);
            }

            PlaceItem(Items.ChestToGoronRaceGrotto, itemPool);
        }

        /// <summary>
        /// Places heart pieces in the randomization pool. Includes rewards/chests, as well as standing heart pieces.
        /// </summary>
        private void PlaceHeartpieces(List<int> itemPool)
        {
            // Rewards/chests
            for (int i = Items.HeartPieceNotebookMayor; i <= Items.HeartPieceKnuckle; i++)
            {
                PlaceItem(i, itemPool);
            }

            // Bank reward
            PlaceItem(Items.HeartPieceBank, itemPool);

            // Standing heart pieces
            for (int i = Items.HeartPieceSouthClockTown; i <= Items.HeartContainerStoneTower; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places shop items in the randomization pool
        /// </summary>
        private void PlaceShopItems(List<int> itemPool)
        {
            for (int i = Items.ShopItemTradingPostRedPotion; i <= Items.ShopItemZoraRedPotion; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places dungeon items in the randomization pool
        /// </summary>
        private void PlaceDungeonItems(List<int> itemPool)
        {
            for (int i = Items.ItemWoodfallMap; i <= Items.ItemStoneTowerKey4; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places songs in the randomization pool
        /// </summary>
        private void PlaceSongs(List<int> itemPool)
        {
            for (int i = Items.SongSoaring; i <= Items.SongOath; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places masks in the randomization pool
        /// </summary>
        private void PlaceMasks(List<int> itemPool)
        {
            for (int i = Items.MaskPostmanHat; i <= Items.MaskZora; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places upgrade items in the randomization pool
        /// </summary>
        private void PlaceUpgrades(List<int> itemPool)
        {
            for (int i = Items.UpgradeRazorSword; i <= Items.UpgradeGiantWallet; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places regular items in the randomization pool
        /// </summary>
        private void PlaceRegularItems(List<int> itemPool)
        {
            for (int i = Items.MaskDeku; i <= Items.ItemNotebook; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Replace starting deku mask with free item if not already replaced.
        /// </summary>
        private void PlaceFreeItem(List<int> itemPool)
        {
            if (ItemList.FindIndex(item => item.ReplacesItemId == Items.MaskDeku) != -1)
            {
                return;
            }

            int freeItem = Random.Next(Items.SongOath + 1);
            if (ForbiddenReplacedBy.ContainsKey(Items.MaskDeku))
            {
                while (ItemList[freeItem].ReplacesItemId != -1
                    || ForbiddenReplacedBy[Items.MaskDeku].Contains(freeItem))
                {
                    freeItem = Random.Next(Items.SongOath + 1);
                }
            }
            ItemList[freeItem].ReplacesItemId = Items.MaskDeku;
            itemPool.Remove(Items.MaskDeku);
        }

        /// <summary>
        /// Adds all items into the randomization pool (excludes area/other and items that already have placement)
        /// </summary>
        private void AddAllItems(List<int> itemPool)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                // Skip item if its in area and other, is out of range or has placement
                if ((ItemUtils.IsAreaOrOther(i)
                    || ItemUtils.IsOutOfRange(i))
                    || (ItemList[i].ReplacesAnotherItem))
                {
                    continue;
                }

                itemPool.Add(i);
            }
        }

        /// <summary>
        /// Places quest items in the randomization pool
        /// </summary>
        private void PlaceQuestItems(List<int> itemPool)
        {
            for (int i = Items.TradeItemRoomKey; i <= Items.TradeItemMamaLetter; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places trade items in the randomization pool
        /// </summary>
        private void PlaceTradeItems(List<int> itemPool)
        {
            for (int i = Items.TradeItemMoonTear; i <= Items.TradeItemOceanDeed; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Adds items to randomization pool based on settings.
        /// </summary>
        private void Setup()
        {
            if (_settings.ExcludeSongOfSoaring)
            {
                ItemList[Items.SongSoaring].ReplacesItemId = Items.SongSoaring;
            }

            if (!_settings.AddSongs)
            {
                ShuffleSongs();
            }

            if (!_settings.AddDungeonItems)
            {
                PreserveDungeonItems();
            }

            if (!_settings.AddShopItems)
            {
                PreserveShopItems();
            }

            if (!_settings.AddOther)
            {
                PreserveOther();
            }

            if (_settings.RandomizeBottleCatchContents)
            {
                AddBottleCatchContents();
            }
            else
            {
                PreserveBottleCatchContents();
            }

            if (!_settings.AddMoonItems)
            {
                PreserveMoonItems();
            }
        }

        /// <summary>
        /// Keeps bottle catch contents vanilla
        /// </summary>
        private void PreserveBottleCatchContents()
        {
            for (int i = Items.BottleCatchFairy; i <= Items.BottleCatchMushroom; i++)
            {
                ItemList[i].ReplacesItemId = i;
            }
        }

        /// <summary>
        /// Randomizes bottle catch contents
        /// </summary>
        private void AddBottleCatchContents()
        {
            var itemPool = new List<int>();
            for (int i = Items.BottleCatchFairy; i <= Items.BottleCatchMushroom; i++)
            {
                if (ItemList[i].ReplacesAnotherItem)
                {
                    continue;
                }
                itemPool.Add(i);
            }

            for (int i = Items.BottleCatchFairy; i <= Items.BottleCatchMushroom; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Keeps other vanilla
        /// </summary>
        private void PreserveOther()
        {
            for (int i = Items.ChestLensCaveRedRupee; i <= Items.ChestToGoronRaceGrotto; i++)
            {
                ItemList[i].ReplacesItemId = i;
            }
        }

        /// <summary>
        /// Keeps shop items vanilla
        /// </summary>
        private void PreserveShopItems()
        {
            for (int i = Items.ShopItemTradingPostRedPotion; i <= Items.ShopItemZoraRedPotion; i++)
            {
                ItemList[i].ReplacesItemId = i;
            }

            ItemList[Items.ItemBombBag].ReplacesItemId = Items.ItemBombBag;
            ItemList[Items.UpgradeBigBombBag].ReplacesItemId = Items.UpgradeBigBombBag;
            ItemList[Items.MaskAllNight].ReplacesItemId = Items.MaskAllNight;
        }

        /// <summary>
        /// Keeps dungeon items vanilla
        /// </summary>
        private void PreserveDungeonItems()
        {
            for (int i = Items.ItemWoodfallMap; i <= Items.ItemStoneTowerKey4; i++)
            {
                ItemList[i].ReplacesItemId = i;
            };
        }

        /// <summary>
        /// Keeps moon items vanilla
        /// </summary>
        private void PreserveMoonItems()
        {
            for (int i = Items.HeartPieceDekuTrial; i <= Items.MaskFierceDeity; i++)
            {
                ItemList[i].ReplacesItemId = i;
            }
        }

        /// <summary>
        /// Randomizes songs with other songs
        /// </summary>
        private void ShuffleSongs()
        {
            var itemPool = new List<int>();
            for (int i = Items.SongSoaring; i <= Items.SongOath; i++)
            {
                if (ItemList[i].ReplacesAnotherItem)
                {
                    continue;
                }
                itemPool.Add(i);
            }

            for (int i = Items.SongSoaring; i <= Items.SongOath; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Adds custom item list to randomization. NOTE: keeps area and other vanilla, randomizes bottle catch contents
        /// </summary>
        private void SetupCustomItems()
        {
            // Keep shop items vanilla, unless custom item list contains a shop item
            _settings.AddShopItems = false;

            // Make all items vanilla, and override using custom item list
            MakeAllItemsVanilla();

            // Should these be vanilla by default? Why not check settings.
            ApplyCustomItemList();

            // Should these be randomized by default? Why not check settings.
            AddBottleCatchContents();

            if (!_settings.AddSongs)
            {
                ShuffleSongs();
            }
        }

        /// <summary>
        /// Mark all items as replacing themselves (i.e. vanilla)
        /// </summary>
        private void MakeAllItemsVanilla()
        {
            for (int item = 0; item < ItemList.Count; item++)
            {
                if (ItemUtils.IsAreaOrOther(item)
                    || ItemUtils.IsOutOfRange(item))
                {
                    continue;
                }

                ItemList[item].ReplacesItemId = item;
            }
        }

        /// <summary>
        /// Adds items specified from the Custom Item List to the randomizer pool, while keeping the rest vanilla
        /// </summary>
        private void ApplyCustomItemList()
        {
            for (int i = 0; i < _settings.CustomItemList.Count; i++)
            {
                int selectedItem = _settings.CustomItemList[i];

                selectedItem = ItemUtils.AddItemOffset(selectedItem);

                int selectedItemIndex = ItemList.FindIndex(u => u.ID == selectedItem);

                if (selectedItemIndex != -1)
                {
                    ItemList[selectedItemIndex].ReplacesItemId = -1;
                }

                if (ItemUtils.IsShopItem(selectedItem))
                {
                    _settings.AddShopItems = true;
                }
            }
        }

        /// <summary>
        /// Randomizes the ROM with respect to the configured ruleset.
        /// </summary>
        public RandomizedResult Randomize(BackgroundWorker worker, DoWorkEventArgs e)
        {
            SeedRNG();

            _randomized = new RandomizedResult(_settings, Random);

            if (_settings.LogicMode != LogicMode.Vanilla)
            {
                worker.ReportProgress(5, "Preparing ruleset...");
                PrepareRulesetItemData();

                if (_settings.RandomizeDungeonEntrances)
                {
                    worker.ReportProgress(10, "Shuffling dungeons...");
                    DungeonShuffle();
                }

                if (_settings.EntranceRandoActivated())
                {
                    worker.ReportProgress(15, "Shuffling entrances...");
                    EntranceShuffle();
                }

                if (_settings.RandomizeOwlStatues)
                {
                    worker.ReportProgress(25, "Shuffling owl statues...");
                    OwlShuffle(false);
                }

                _randomized.Logic = ItemList.Select(io => new ItemLogic(io)).ToList();

                worker.ReportProgress(30, "Shuffling items...");
                RandomizeItems();


                if (_settings.EnableGossipHints)
                {
                    worker.ReportProgress(35, "Making gossip quotes...");
                }

                //gossip
                SeedRNG();
                MakeGossipQuotes();
            }

            worker.ReportProgress(40, "Coloring Tatl...");

            //Randomize tatl colour
            SeedRNG();
            SetTatlColour();

            worker.ReportProgress(45, "Randomizing Music...");

            //Sort BGM
            SeedRNG();
            SortBGM();

            return _randomized;
        }
    }

}