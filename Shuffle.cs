using MMRando.Models;
using MMRando.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MMRando
{

    public partial class MainRandomizerForm
    {

        Random RNG;


        public class ItemObject
        {
            public int ID { get; set; }
            public List<int> DependsOnItems { get; set; } = new List<int>();
            public List<List<int>> Conditionals { get; set; } = new List<List<int>>();
            public List<int> CannotRequireItems { get; set; } = new List<int>();
            public int TimeNeeded { get; set; }
            public int TimeAvailable { get; set; }
            public int ReplacesItemId { get; set; } = -1;

            public bool ReplacesAnotherItem => ReplacesItemId != -1;
            public bool HasConditionals => Conditionals != null && Conditionals.Count > 0;
            public bool HasDependencies => DependsOnItems != null
                && DependsOnItems.Count > 0;
            public bool HasCannotRequireItems => CannotRequireItems != null
                && CannotRequireItems.Count > 0;
        }

        public class SequenceInfo
        {
            public string Name { get; set; }
            public int Replaces { get; set; } = -1;
            public int MM_seq { get; set; } = -1;
            public List<int> Type { get; set; } = new List<int>();
            public int Instrument { get; set; }
        }

        public class Gossip
        {
            public string[] SourceMessage { get; set; }
            public string[] DestinationMessage { get; set; }
        }

        List<ItemObject> ItemList { get; set; }
        List<SequenceInfo> SequenceList { get; set; }
        List<SequenceInfo> TargetSequences { get; set; }
        List<Gossip> GossipList { get; set; }

        List<int> ConditionsChecked { get; set; }
        Dictionary<int, Dependence> DependenceChecked { get; set; }
        List<int[]> ConditionRemoves { get; set; }
        List<string> GossipQuotes { get; set; }

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

        //rando functions
        #region Entrances and Dungeons and Owl Statues

        private int[] _newEntrances = new int[] { -1, -1, -1, -1 };
        private int[] _newExits = new int[] { -1, -1, -1, -1 };
        private int[] _newDCFlags = new int[] { -1, -1, -1, -1 };
        private int[] _newDCMasks = new int[] { -1, -1, -1, -1 };
        private int[] _newEnts = new int[] { -1, -1, -1, -1 };
        private int[] _newExts = new int[] { -1, -1, -1, -1 };
        private int[] _randomizedOwls;

        private void DungeonShuffle()
        {
            int poolSize = Values.OldEntrances.Count;
            _newEntrances = new int[poolSize];
            _newExits = new int[poolSize];
            _newDCFlags = new int[poolSize];
            _newDCMasks = new int[poolSize];
            _newEnts = new int[poolSize];
            _newExts = new int[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                _newEntrances[i] = -1;
                _newExits[i] = -1;
                _newDCFlags[i] = -1;
                _newDCMasks[i] = -1;
                _newEnts[i] = -1;
                _newExts[i] = -1;
            }

            for (int i = 0; i < poolSize; i++)
            {
                int n;
                do
                {
                    n = RNG.Next(poolSize);
                } while (_newEnts.Contains(n));

                _newEnts[i] = n;
                _newExts[n] = i;
            };

            ItemObject[] DE = new ItemObject[] {
                ItemList[Items.AreaWoodFallTempleAccess],
                ItemList[Items.AreaSnowheadTempleAccess],
                ItemList[Items.AreaInvertedStoneTowerTempleAccess],
                ItemList[Items.AreaGreatBayTempleAccess]
            };

            int[] DI = new int[] {
                Items.AreaWoodFallTempleAccess,
                Items.AreaSnowheadTempleAccess,
                Items.AreaInvertedStoneTowerTempleAccess,
                Items.AreaGreatBayTempleAccess
            };

            for (int i = 0; i < poolSize; i++)
            {
                Debug.WriteLine($"Entrance {DI[_newEnts[i]]} placed at {DE[i].ID}.");
                ItemList[DI[_newEnts[i]]] = DE[i];
            };

            DE = new ItemObject[] {
                ItemList[Items.AreaWoodFallTempleClear],
                ItemList[Items.AreaSnowheadTempleClear],
                ItemList[Items.AreaStoneTowerClear],
                ItemList[Items.AreaGreatBayTempleClear]
            };

            DI = new int[] {
                Items.AreaWoodFallTempleClear,
                Items.AreaSnowheadTempleClear,
                Items.AreaStoneTowerClear,
                Items.AreaGreatBayTempleClear
            };

            for (int i = 0; i < poolSize; i++)
            {
                ItemList[DI[i]] = DE[_newEnts[i]];
            };

            for (int i = 0; i < poolSize; i++)
            {
                _newEntrances[i] = Values.OldEntrances[_newEnts[i]];
                _newExits[i] = Values.OldExits[_newExts[i]];
                _newDCFlags[i] = Values.OldDCFlags[_newExts[i]];
                _newDCMasks[i] = Values.OldMaskFlags[_newExts[i]];
            };
        }

        private class Spawn
        {
            public Spawn(string Name, int Address, string Scene)
            {
                this.Name = Name;
                this.SpawnAddress = Address;
                this.Scene = Scene;
                this.ShuffledAddress = Address;
            }
            public string Scene;
            public int SpawnAddress;
            public string Name;
            public int ShuffledAddress;
            public Spawn Exit;
            public string Type;
        }

        private class CollectionState
        {
            List<string> Inventory = new List<string>();
            public void Collect(string Item)
            {
                Inventory.Add(Item);
            }
            public bool Has(string Item)
            {
                return Inventory.Contains(Item);
            }
        }

        Dictionary<string, List<Spawn>> TerminaMap { get; set; }
        Dictionary<string, Predicate<CollectionState>> TerminaLogic;
        int[] _OriginalEntrances { get; set; }
        int[] _ShuffledEntrances { get; set; }
        List<string> _EntranceSpoilers { get; set; } = new List<string>();

        private void OwlShuffle(bool hidden)
        {
            int size = 12;
            int poolSize = size;
            _randomizedOwls = new int[size];
            for (int i = 0; i < _randomizedOwls.Length; i++)
            {
                _randomizedOwls[i] = -1;
            }
            if (!hidden)
            {
                _randomizedOwls[0] = 0;
                _randomizedOwls[8] = 9;
                _randomizedOwls[10] = 11;
            }
            int owl = 0;
            while (owl < _randomizedOwls.Length)
            {
                if (_randomizedOwls[owl] == -1)
                {
                    int n;
                    do
                    {
                        n = RNG.Next(_randomizedOwls.Length);
                    } while (_randomizedOwls.Contains(n));

                    _randomizedOwls[owl] = n;
                    _randomizedOwls[n] = owl;
                }
                owl++;
            }
        }
        private void EntranceShuffle()
        {
            TerminaMap = new Dictionary<string, List<Spawn>>();
            TerminaLogic = new Dictionary<string, Predicate<CollectionState>>();
            GetVanillaTerminaMap();
            ConstructTerminaLogic();
            ShuffleEntrances();
            //TestEntrances();
            FinalizeEntrances();
        }

        private void GetVanillaTerminaMap()
        {
            AddSceneSpawns(new string[] {
                "Clock Tower", "Termina Field", "East Clock Town",
                "West Clock Town", "North Clock Town", "South West Connection",
                "Laundry Pool", "South East Connection" },
                    0xD8, "South Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "East Clock Town", "South Clock Town", "Clock Town Fairy", "Deku Playground" },
                    0xD6, "North Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "South West Connection", "South Clock Town",
                "Swordsman's School", "Curiosity Shop", "Trading Post", "Bomb Shop",
                "Post Office", "Lottery Shop"},
                    0xD4, "West Clock Town");
            AddSceneSpawns(new string[] {
                "Termina Field", "South East Connection", "Observatory", "South Clock Town",
                "Treasure Chest Game", "North Clock Town", "Honey & Darling", "Mayor's Residence",
                "Town Shooting Gallery", "Stock Pot Inn", "Stock Pot Roof", "Milk Bar"},
                    0xD2, "East Clock Town");
            ConnectInteriors(
                new string[] {
                    "Bomb Shop", "Trading Post", "Swordsman's School", "Curiosity Shop",
                    "Post Office", "Lottery Shop", "Treasure Chest Game", "Honey & Darling",
                    "Mayor's Residence", "Town Shooting Gallery", "Stock Pot Inn", "Stock Pot Roof",
                    "Milk Bar", "Deku Playground" },
                new UInt16[] {
                    0xCA00, 0x6200, 0xA200, 0x0E00,
                    0x5600, 0x6C00, 0x2800, 0x0800,
                    0x0000, 0x3A00, 0xBC00, 0xBC10,
                    0x2400, 0x3600 });
            GetSpawn("Stock Pot Roof").Scene = "Stock Pot Inn";
            PairSpawns("South Clock Town: South West Connection", "West Clock Town: South West Connection", "Overworld");
            PairSpawns("South Clock Town: South East Connection", "East Clock Town: South East Connection", "Overworld");

            AddSceneSpawns(new string[] { "South Clock Town", "Curiosity Shop Backroom" }, 0xDA, "Laundry Pool");

            AddSpawn("Curiosity Shop Backroom: Laundry Pool", 0x0E10, "Curiosity Shop Backroom");
            AddSpawn("Curiosity Shop Backroom: Telescope", 0x0E20, "Curiosity Shop Backroom");
            AddSpawn("Telescope: Curiosity Shop Backroom", 0x0E30, "Curiosity Shop Backroom");

            AddSceneSpawns(new string[] { "East Clock Town", "Termina Field", "Telescope" }, 0x4C, "Observatory");

            AddSceneSpawns(new string[] {
                "West Clock Town", "Swamp Path", "Great Bay Coast",
                "Mountain Path", "Ikana Path", "Milk Road", "South Clock Town",
                "East Clock Town", "North Clock Town", "Observatory", "Telescope" },
                    0x54, "Termina Field");

            AddSceneSpawns(new string[] { "Termina Field", "Southern Swamp", "Swamp Shooting Gallery" }, 0x7A, "Swamp Path");
            string[] duplicateSceneSpawns = new string[] {
                "Swamp Path", "Tourist Center", "Woodfall", "Deku Palace",
                "Deku Shortcut", "Potion Shop", "", "Woods of Mystery",
                "Swamp Spider House", "Ikana Canyon", "Owl Warp"};
            AddSceneSpawns(duplicateSceneSpawns, 0x84, "Southern Swamp");
            //AddSceneSpawns(duplicateSceneSpawns, 0x0C, "Southern Swamp Healed");

            AddSceneSpawns(new string[] {
                "Southern Swamp", "Getting Caught", "Deku King Chamber", "Sonata Monkey",
                "Deku Shortcut", "", "", "", "Bean Seller", ""
            }, 0x50, "Deku Palace");
            GetSpawn("Deku Palace: Getting Caught").Type = "Special";
            AddSceneSpawns(new string[] { "", "Deku Palace", "Sonata Monkey", "Monkey Freedom" }, 0x76, "Deku King Chamber");
            GetSpawn("Deku King Chamber: Monkey Freedom").Type = "Special";
            ConnectInteriors(
                new string[] { "Potion Shop", "Swamp Shooting Gallery", "Swamp Spider House", "Woods of Mystery", "Tourist Center" },
                new ushort[] { 0x0400, 0x4200, 0x4800, 0xC200, 0xA800 });
            AddSceneSpawns(new string[] { "Southern Swamp", "Temple", "Woodfall Fairy",
                "Deku Princess", "Owl Warp" }, 0x86, "Woodfall");

            AddSceneSpawns(new string[] { "Termina Field", "Mountain Village" }, 0x32, "Mountain Path");
            duplicateSceneSpawns = new string[] {
                "", "Smithy", "Twin Islands", "Goron Grave",
                "Snowhead Path", "", "Mountain Path", "", "Owl Warp" };
            AddSceneSpawns(duplicateSceneSpawns, 0x9A, "Mountain Village");
            duplicateSceneSpawns = new string[] {
                "Mountain Village", "Goron Village", "Goron Racetrack" };
            AddSceneSpawns(duplicateSceneSpawns, 0xB4, "Twin Islands");
            duplicateSceneSpawns = new string[] {
                "Twin Islands", "", "Goron Shrine", "Lens Grotto" };
            AddSceneSpawns(duplicateSceneSpawns, 0x94, "Goron Village");
            duplicateSceneSpawns = new string[] {
                "Twin Islands", "", "Goron Shrine" };
            ConnectInteriors(
                new string[] { "Goron Grave", "Smithy", "Goron Racetrack", "Lens Grotto", "Goron Shrine" },
                new ushort[] { 0x960, 0x5200, 0xD000, 0x1500, 0x5E00 });
            AddSpawn("Goron Shop: Goron Shrine", 0x7400, "Goron Shop");
            AddSpawn("Goron Shrine: Goron Shop", 0x5E10, "Goron Shrine");
            AddSpawn("Goron Racetrack: Race", 0xD010, "Goron: Racetrack");
            GetSpawn("Goron Racetrack: Race").Type = "Special";
            AddSceneSpawns(new string[] { "Mountain Village", "Snowhead" }, 0xB0, "Snowhead Path");
            AddSceneSpawns(new string[] { "Snowhead Path", "Temple", "Snowhead Fairy", "Owl Warp" }, 0xB2, "Snowhead");

            //todo: finish Great Bay/ Romani Ranch
            AddSceneSpawns(new string[] { "Termina Field", "Romani Ranch", "", "Gorman Track", "Owl Warp" }, 0x3E, "Milk Road");
            AddSceneSpawns(new string[] {
                "Milk Road", "", "Stable", "Ranch House", "Cucco Shack", "Doggy Racetrack",
                "", "", "", "", "", ""}, 0x64, "Romani Ranch");
            ConnectInteriors(
                new string[] { "Stable", "Ranch House", "Cucco Shack", "Doggy Racetrack", "Gorman Track" },
                new ushort[] {  0x0600, 0x0610, 0x7E00, 0x7C00, 0xCE00 });

            AddSceneSpawns(new string[] {
                "Termina Field", "Zora Cape", "", "Pinnacle Rock", "Fisherman's Hut",
                "Pirate's Fortress", "", "Marine Lab", "Ocean Spider House", "", "", "Owl Warp", "Caught" }, 0x68, "Great Bay Coast");
            ConnectInteriors(
                new string[] { "Pinnacle Rock", "Fisherman's Hut", "Marine Lab", "Ocean Spider House" },
                new ushort[] { 0x4400, 0x7200, 0x5800, 0x4A00 });

            AddSceneSpawns(new string[] {
                "Great Bay Coast", "Pirate's Fortress", "Pirate Tunnel", "Water Jet Exit",
                "Caught", "Pirate Platform", "Telescope Room" }, 0x70, "Outside PF" );
            AddSceneSpawns(new string[] {
                "Outside PF", "Hookshot Room", "Hookshot Room Upper", "Well Guarded Room", "Well Guarded Exit",
                "Barrel Room", "Barrel Room Exit", "Twin Barrel Room", "Twin Barrel Room Exit",
                "", "Telescope", "", "Pirate Platform"}, 0x22, "Pirate's Fortress");
            AddSpawns("Hookshot Room",
                new string[] { "Main", "Upper" },
                new ushort[] { 0x4000, 0x4010 });
            AddSpawns("Well Guarded Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4020, 0x4030 });
            AddSpawns("Barrel Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4040, 0x4050 });
            AddSpawns("Twin Barrel Room",
                new string[] { "Entrance", "Exit" },
                new ushort[] { 0x4060, 0x4070 });
            AddSpawns("Pirate Tunnel",
                new string[] { "Telescope", "Entrance", "Exit" },
                new ushort[] { 0x4080, 0x4090, 0x40A0 });
            PairSpawns("Outside PF: Great Bay Coast", "Great Bay Coast: Pirate's Fortress", "Water");
            PairPirateEntrance("Pirate's Fortress: Hookshot Room", "Hookshot Room: Main");
            PairPirateEntrance("Pirate's Fortress: Hookshot Room Upper", "Hookshot Room: Upper");
            PairPirateEntrance("Pirate's Fortress: Well Guarded Room", "Well Guarded Room: Entrance");
            PairPirateEntrance("Pirate's Fortress: Well Guarded Exit", "Well Guarded Room: Exit");
            PairPirateEntrance("Pirate's Fortress: Barrel Room", "Barrel Room: Entrance");
            PairPirateEntrance("Pirate's Fortress: Barrel Room Exit", "Barrel Room: Exit");
            PairPirateEntrance("Pirate's Fortress: Twin Barrel Room", "Twin Barrel Room: Entrance");
            PairPirateEntrance("Pirate's Fortress: Twin Barrel Room Exit", "Twin Barrel Room: Exit");
            PairPirateEntrance("Outside PF: Pirate Tunnel", "Pirate Tunnel: Entrance");
            PairPirateEntrance("Outside PF: Telescope Room", "Pirate Tunnel: Exit");

            AddSceneSpawns(new string[] {
                "Great Bay Coast", "Zora Hall Water", "Zora Hall",
                "", "Waterfall Rapids", "Great Bay Fairy", "Owl Warp"}, 0x6A, "Zora Cape");
            AddSceneSpawns(new string[] {
                "Water", "Zora Cape", "Zora Shop", "Lulu's Room",
                "Evan's Room", "Japas's Room", "Mikau's Room" }, 0x60, "Zora Hall");
            ConnectInteriors(
                new string[] {
                    "Waterfall Rapids", "Zora Shop", "Lulu's Room",
                    "Evan's Room", "Japas's Room", "Mikau's Room" },
                new ushort[] {
                    0x8E00, 0x9250, 0x9220,
                    0x9230, 0x9210, 0x9200});

            AddSceneSpawns(new string[] { "Termina Field", "Ikana Canyon", "Ikana Graveyard" }, 0xA0, "Ikana Path");
            AddSceneSpawns(new string[] {
                "Ikana Path", "Night 3 Grave", "Night 2 Grave",
                "Night 1 Grave", "Dampe's House", "Defeat Skull Keeta" }, 0x80, "Ikana Graveyard");
            ConnectInteriors(
                new string[] { "Night 2 Grave", "Night 1 Grave", "Night 3 Grave", "Dampe's House" },
                new ushort[] { 0x0A00, 0x0A10, 0x5A00, 0x5A10 });

            AddSceneSpawns(new string[] {
                "Ikana Path", "Poe Hut", "Music Box", "Stone Tower", "Owl Warp", "Well",
                "Sakon's Hideout", "", "Ikana Castle", "", "", "Stone Tower Fairy", "Secret Shrine" }, 0x20, "Ikana Canyon");
            AddSceneSpawns(new string[] { "Well", "Ikana Canyon", "", "", "", "", "Igos" }, 0x34, "Ikana Castle");
            AddSceneSpawns(new string[] { "Ikana Canyon", "Ikana Castle" }, 0x90, "Well");
            AddSceneSpawns(new string[] { "Ikana Canyon", "Inverted Stone Tower", "Stone Tower Temple", "Owl Warp" }, 0xAA, "Stone Tower");
            AddSceneSpawns(new string[] { "Stone Tower", "Temple" }, 0xAC, "Inverted Stone Tower");
            ConnectInteriors(
                new string[] {
                    "Music Box", "Igos", "Secret Shrine", "Poe Hut",
                    "Stone Tower Temple", "Sakon's Hideout" },
                new ushort[] {
                    0xA400, 0xA600, 0xBA00, 0x9C00,
                    0x2600, 0x9800 });

            ConnectInteriors(
                new string[] { "Clock Town Fairy", "Woodfall Fairy", "Snowhead Fairy", "Great Bay Fairy", "Stone Tower Fairy" },
                new ushort[] { 0x4600, 0x4610, 0x4620, 0x4630, 0x4640 });

            AddSpawn("Moon", 0xC800, "Moon");
            AddSpawn("Clock Tower: South Clock Town", 0xC010, "Clock Tower");
            AddSpawns("Moon",
                new string[] { "Woodfall Trial", "Snowhead Trial", "Great Bay Trial", "Stone Tower Trial" },
                new UInt16[] { 0x4E00, 0x7800, 0x8800, 0xC600 }
            );
            AddSpawn("Majora Fight", 0x0200, "Moon");
            ConnectSpawnPoints();
            ConnectTelescope("Telescope: Curiosity Shop Backroom", "Curiosity Shop Backroom: Telescope");
            ConnectTelescope("Observatory: Telescope", "Termina Field: Telescope");
            ConnectTelescope("Pirate Tunnel: Telescope", "Pirate's Fortress: Telescope");

        }

        private void ConnectTelescope(string SpawnPoint, string Telescope)
        {
            GetSpawn(Telescope).Exit = GetSpawn(SpawnPoint);
            GetSpawn(Telescope).Type = "Telescope";
        }

        private void PairPirateEntrance(string OutdoorEntrance, string IndoorEntrance)
        {
            PairSpawns(OutdoorEntrance, IndoorEntrance, "Interior");
            GetSpawn(OutdoorEntrance).Type = "Interior Exit";

        }
        private void ConnectInteriors(string[] Scene, ushort[] Address)
        {
            for (int i = 0; i < Scene.Length; i++)
            {
                string SpawnName = Scene[i];
                AddSpawn(SpawnName, Address[i], Scene[i]);
                string To = "";
                foreach (Spawn S in GetSpawns())
                {
                    if (!SpawnName.Equals(S.Name) && S.Name.Contains(SpawnName))
                    {
                        PairSpawns(SpawnName, S.Name, "Interior");
                        S.Type = "Interior Exit";
                    }
                }
                if (!To.Equals(""))
                {
                }
            }
        }

        private void ConstructTerminaLogic()
        {
        }

        private void EntranceOverrides()
        {
            // going into clock tower goes to moon
            // talking to skull kid returns to twisted hallway
            AddSpawn("Twisted Hallway: Clock Tower", 0x2E10, "Twisted Hallway");
            AddSpawn("Clock Tower: Twisted Hallway", 0xC000, "Clock Tower");
            PairSpawns("South Clock Town: Clock Tower", "Clock Tower: Twisted Hallway", "OW");
            PairSpawns("Moon", "Majora Fight", "OW");
            ConnectEntrances("Clock Tower: Front", "Moon", true);
            // sets the starting location
            ConnectEntrances("South Clock Town: Clock Tower", "Mountain Village Spring: Owl Warp", false);
            // test to see if warping to MV before and after Goht yields a different spot
            ConnectEntrances("Mountain Village Spring: Owl Warp", "Woodfall Trial", false);
            ConnectEntrances("Mountain Village Spring: Owl Warp", "Snowhead Trial", false);
            AddSceneSpawns(new string[] {
                "Great Bay Gossip", "Woodfall Gossip", "Stone Tower Gossip", "Snowhead Gossip",
                "", "Spring Water", "", "Dodongo", "", "Scrub Haggle", "Cow", "Beehive", "Bean Seller",
                "Peahat" },
                0x14, "Grotto");
            AddSpawn("Boss Chamber: Odolwa", 0x3800, "Odolwa");
            AddSpawn("Boss Chamber: Goht", 0x8200, "Goht");
            AddSpawn("Boss Chamber: Gyorg", 0xB800, "Gyorg");
            AddSpawn("Boss Chamber: Twinmold", 0x6600, "Twinmold");
            AddSpawn("Moon Crash", 0x54C0, "Bad Ideas");
            ConnectEntrances("Clock Tower: South Clock Town", "Moon Crash", false);
        }

        private void TestEntrances()
        {
            SwapEntrances("South Clock Town: South West Connection", "Moon");
            SwapEntrances("South Clock Town: Clock Tower", "South Clock Town: Laundry Pool");
            SwapEntrances("East Clock Town: South Clock Town", "Moon: Stone Tower Trial");
            SwapEntrances("West Clock Town: South Clock Town", "Moon: Great Bay Trial");
            SwapEntrances("North Clock Town: South Clock Town", "Moon: Snowhead Trial");
            SwapEntrances("Termina Field: South Clock Town", "Moon: Woodfall Trial");
        }

        private void ShuffleEntrances()
        {
            List<Dictionary<string, bool>> SpawnSet = new List<Dictionary<string, bool>>();
            bool ShuffleInteriors = Settings.RandomizeInteriorEntrances;
            bool ShuffleOverworld = Settings.RandomizeOverworldEntrances;
            bool ShuffleOneWay = Settings.RandomizeSpecialEntrances || Settings.RandomizeOwlWarps;
            bool MixEntrances = Settings.MixEntrances;
            if( MixEntrances)
            {
                SpawnSet.Add(new Dictionary<string, bool>());
            }
            else
            {
                if( ShuffleOverworld)
                {
                    SpawnSet.Add(new Dictionary<string, bool>());
                }
                if (ShuffleInteriors)
                {
                    SpawnSet.Add(new Dictionary<string, bool>());
                }
                if (ShuffleOneWay)
                {
                    SpawnSet.Add(new Dictionary<string, bool>());
                }
            }
            string[] poolScenes = new string[]{ "South Clock Town", "Ikana Canyon", "Termina Field" };
            int chosenPool = 0;
            foreach (Spawn S in GetSpawns())
            {
                if (!S.Name.Contains("Temple") ) {
                    if( MixEntrances)
                    {
                        chosenPool = 0;
                    }
                    else
                    {
                        if (S.Exit != null)
                        {
                            if (S.Type == "Overworld" || S.Type == "Water")
                            {
                                if (!ShuffleOverworld)
                                {
                                    continue;
                                }
                                else
                                {
                                    chosenPool = 0;
                                }
                            }
                            else
                            {
                                if( !ShuffleInteriors)
                                {
                                    continue;
                                }
                                else
                                {
                                    chosenPool = ShuffleOverworld ? 1 : 0;
                                }
                            }
                        }
                        else
                        {
                            if( !ShuffleOneWay)
                            {
                                continue;
                            }
                            else
                            {
                                chosenPool = ShuffleOverworld && ShuffleInteriors ? 2 : ShuffleOverworld || ShuffleInteriors ? 1 : 0;
                            }
                        }

                    }
                }
                SpawnSet[chosenPool].Add(S.Name, true);
            }
            List<string> TempInaccessible = new List<string>();
            List<string> FillWorld = new List<string>();
            CollectionState Inventory = new CollectionState();
            int pool = 0;
            Predicate<Spawn> CanAdd = S => SpawnSet[pool].ContainsKey(S.Name) && SpawnSet[pool][S.Name] && (S.Type == null || (S.Type != null && S.Type != null));
            CanAdd = S => true;
            CanAdd = S => S != null && SpawnSet[pool].ContainsKey(S.Name) && SpawnSet[pool][S.Name];
            Spawn To, From;
            string TempExit;
            FillWorld.Add("South Clock Town: Clock Tower");
            while (FillWorld.Count > 0)
            {
                From = GetSpawn(FillWorld[0]);
                if (CanAdd.Invoke(From))
                {
                    To = ChooseNextEntrance(SpawnSet[pool], From, CanAdd);
                    if (To != null)
                    {
                        SpawnSet[pool][From.Name] = false;
                        FillWorld.RemoveAll(S => S == FillWorld[0]);
                        ConnectEntrances(From.Name, To.Name, true);
                        if (To.Exit != null && SpawnSet[pool].ContainsKey(To.Exit.Name))
                        {
                            SpawnSet[pool][To.Exit.Name] = false;
                            if (FillWorld.Contains(To.Exit.Name))
                            {
                                FillWorld.RemoveAll(S => S == To.Exit.Name);
                            }
                        }
                        if (TerminaMap.ContainsKey(To.Scene) && CheckEntranceLogic(To.Scene, Inventory))
                        {
                            foreach (Spawn SceneSpawn in TerminaMap[To.Scene])
                            {
                                if (SceneSpawn.Exit != null)
                                {
                                    TempExit = SceneSpawn.Exit.Name;
                                    if (SpawnSet[pool].ContainsKey(TempExit) && SpawnSet[pool][TempExit] && CheckEntranceLogic(TempExit, Inventory))
                                    {
                                        FillWorld.Add(TempExit);
                                    }
                                    else if (!TempInaccessible.Contains(TempExit))
                                    {
                                        TempInaccessible.Add(TempExit);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // there was no available entrance to connect to
                        SpawnSet[pool][From.Name] = false;
                        Console.WriteLine("Could Not Place: {0}", From.Name);
                        FillWorld.RemoveAt(0);
                    }
                }
                else
                {
                    // somehow the entrance being placed is no longer allowed to be placed
                    // not sure what code execution path let's that happen tho?
                    FillWorld.RemoveAt(0);
                }
                // if we've run out of connected places to put things, and still have more entrances to place
                // start adding entrances from a new starting point
                // eventually want to tie this in to the owl statues to pick out an accessible owl statue
                if (FillWorld.Count == 0)
                {
                    for (int i = 0; i < SpawnSet.Count; i++)
                    {
                        pool = (pool + 1) % SpawnSet.Count;
                        List<KeyValuePair<string, bool>> Available = SpawnSet[pool].Where(S => S.Value).ToList();
                        if (Available.Count > 0)
                        {
                            int n = RNG.Next(Available.Count);
                            FillWorld.Add(Available[n].Key);
                            break;
                        }
                    }
                }
            }
        }

        private bool CheckEntranceLogic(string Name, CollectionState Inventory)
        {
            Predicate<CollectionState> IsAccessible = TerminaLogic[Name];
            return IsAccessible.Invoke(Inventory);
        }

        private Spawn ChooseNextEntrance(Dictionary<string, bool> SpawnSet, Spawn Departure, Predicate<Spawn> CanAdd)
        {
            List<string> candidates = new List<string>();
            bool AllowTelescopes = true;
            if (Departure.Name.Equals("South Clock Town: Clock Tower"))
            {
                // choose from the hub areas first
                foreach (string s in new string[] { "South Clock Town", "North Clock Town", "Termina Field", "Ikana Canyon", "Zora Hall" })
                {
                    if (TerminaMap.ContainsKey(s))
                    {
                        foreach (Spawn S in TerminaMap[s])
                        {
                            if (CanAdd.Invoke(S))
                            {
                                if (Departure.Type == S.Type)
                                {
                                    candidates.Add(S.Name);
                                }
                                if (AllowTelescopes)
                                {
                                    if (Departure.Type == "Interior" && S.Type == "Telescope")
                                    {
                                        candidates.Add(S.Name);
                                    }
                                    if (Departure.Type == "Telescope" && S.Type == "Interior")
                                    {
                                        candidates.Add(S.Name);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (string SpawnName in SpawnSet.Keys)
                {
                    if (SpawnSet[SpawnName])
                    {
                        Spawn S = GetSpawn(SpawnName);
                        if (CanAdd.Invoke(S))
                        {
                            if (Departure.Type == S.Type)
                            {
                                candidates.Add(S.Name);
                            }
                        }
                    }
                }
            }
            int n = RNG.Next(candidates.Count);
            if (candidates.Count > 1)
            {
                return GetSpawn(candidates[n]);
                //return GetSpawn(candidates[0]);
            }
            return null;
        }

        private void AddSceneSpawns(string[] Spawns, UInt16 Scene, string SceneName)
        {
            for (int i = 0; i < Spawns.Length; i++)
            {
                if (!Spawns[i].Equals(""))
                {
                    AddSpawn(SceneName + ": " + Spawns[i], (Scene << 8) + (i << 4), SceneName);
                }
            }
        }

        private void AddSpawns(string parent, string[] sceneName, ushort[] sceneAddress)
        {
            for (int i = 0; i < sceneName.Length; i++)
            {
                AddSpawn(parent + ": " + sceneName[i], sceneAddress[i], parent);
            }
        }

        private void AddSpawn(string Name, int Address, string Scene)
        {
            if (!TerminaMap.ContainsKey(Scene))
            {
                TerminaMap.Add(Scene, new List<Spawn>());
            }
            List<Spawn> sceneSpawns = TerminaMap[Scene];
            sceneSpawns.Add(new Spawn(Name, Address, Scene));
            TerminaLogic[Name] = s => true;
            TerminaLogic[Scene] = s => true;
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

        private void PairSpawns(string from, string to, string type)
        {
            Spawn f = GetSpawn(from);
            Spawn t = GetSpawn(to);
            f.Exit = t;
            t.Exit = f;
            f.Type = type;
            t.Type = type;
        }

        private void ConnectEntrances(string from, string to, bool connectReverse)
        {
            Spawn f = GetSpawn(from);
            Spawn t = GetSpawn(to);
            if (f != null && t != null)
            {
                f.ShuffledAddress = t.SpawnAddress;
                if (connectReverse && f.Exit != null && t.Exit != null)
                {
                    t.Exit.ShuffledAddress = f.Exit.SpawnAddress;
                }
            }
        }

        private void SwapEntrances(string ReplacingEntrance, string NewEntrance)
        {
            string OldValue = GetSpawns().Find(S => S.SpawnAddress == GetSpawn(ReplacingEntrance).ShuffledAddress).Name;
            string NewValue = GetSpawns().Find(S => S.SpawnAddress == GetSpawn(NewEntrance).ShuffledAddress).Name;
            ConnectEntrances(ReplacingEntrance, NewEntrance, true);
            ConnectEntrances(OldValue, NewValue, true);
            Console.WriteLine("Swap {0} > {1} : {2} > {3}", ReplacingEntrance, NewEntrance, OldValue, NewValue);
        }

        private void ConnectSpawnPoints()
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
                if (SpawnPoint.ContainsKey(SpawnSet[i]) && SpawnExit.ContainsKey(SpawnSet[i]))
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
                        PairSpawns(SpawnSet[i].Name, SpawnSet[j].Name, "Overworld");
                    }
                }
            }
        }

        private int SpawnTotal()
        {
            int t = 0;
            foreach (List<Spawn> Scene in TerminaMap.Values)
            {
                t += Scene.Count;
            }
            return t;
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
            List<Spawn> Entrances = GetSpawns();
            _OriginalEntrances = new int[Entrances.Count];
            _ShuffledEntrances = new int[Entrances.Count];
            _EntranceSpoilers = new List<string>();
            int i = 0;
            Spawn ShuffledSpawn;
            foreach (Spawn Spawn in Entrances)
            {
                _OriginalEntrances[i] = Spawn.SpawnAddress;
                _ShuffledEntrances[i] = Spawn.ShuffledAddress;
                ShuffledSpawn = Entrances.Find(S => S.SpawnAddress == Spawn.ShuffledAddress);
                _EntranceSpoilers.Add(Spawn.Name + " -> " + ShuffledSpawn.Name);
                i++;
            }
        }
        #endregion

        #region Gossip quotes

        private void MakeGossipQuotes()
        {
            GossipQuotes = new List<string>();
            ReadAndPopulateGossipList();

            for (int itemIndex = 0; itemIndex < ItemList.Count; itemIndex++)
            {
                if (ItemList[itemIndex].ReplacesItemId == -1)
                {
                    continue;
                };

                // Skip hints for vanilla bottle content
                if ((!Settings.RandomizeBottleCatchContents)
                    && ItemUtils.IsBottleCatchContent(itemIndex))
                {
                    continue;
                };

                // Skip hints for vanilla shop items
                if ((!Settings.AddShopItems)
                    && ItemUtils.IsShopItem(itemIndex))
                {
                    continue;
                };

                // Skip hints for vanilla dungeon items
                if (!Settings.AddDungeonItems
                    && ItemUtils.IsDungeonItem(itemIndex))
                {
                    continue;
                };

                int sourceItemId = ItemList[itemIndex].ReplacesItemId;
                sourceItemId -= ItemUtils.GetItemOffset(sourceItemId);

                int toItemId = itemIndex;
                toItemId -= ItemUtils.GetItemOffset(toItemId);

                // 5% chance of being fake
                bool isFake = (RNG.Next(100) < 5);
                if (isFake)
                {
                    sourceItemId = RNG.Next(GossipList.Count);
                };

                int sourceMessageLength = GossipList[sourceItemId]
                    .SourceMessage
                    .Length;

                int destinationMessageLength = GossipList[toItemId]
                    .DestinationMessage
                    .Length;

                // Randomize messages
                string sourceMessage = GossipList[sourceItemId]
                    .SourceMessage[RNG.Next(sourceMessageLength)];

                string destinationMessage = GossipList[toItemId]
                    .DestinationMessage[RNG.Next(destinationMessageLength)];

                // Sound differs if hint is fake
                string soundAddress;
                if (isFake)
                {
                    soundAddress = "\x1E\x69\x0A";
                }
                else
                {
                    soundAddress = "\x1E\x69\x0C";
                };

                var quote = BuildGossipQuote(soundAddress, sourceMessage, destinationMessage);

                GossipQuotes.Add(quote);
            };

            for (int i = 0; i < Values.JunkGossipMessages.Count; i++)
            {
                GossipQuotes.Add(Values.JunkGossipMessages[i]);
            };
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
            };
        }

        public string BuildGossipQuote(string soundAddress, string sourceMessage, string destinationMessage)
        {
            int randomMessageStartIndex = RNG.Next(Values.GossipMessageStartSentences.Count);
            int randomMessageMidIndex = RNG.Next(Values.GossipMessageMidSentences.Count);

            var quote = new StringBuilder();

            quote.Append(soundAddress);
            quote.Append(Values.GossipMessageStartSentences[randomMessageStartIndex]);
            quote.Append("\x01" + sourceMessage + "\x00\x11");
            quote.Append(Values.GossipMessageMidSentences[randomMessageMidIndex]);
            quote.Append("\x06" + destinationMessage + "\x00" + "...\xBF");

            return quote.ToString();
        }

        #endregion

        #region Sequences and BGM

        private void ReadSeqInfo()
        {
            SequenceList = new List<SequenceInfo>();
            TargetSequences = new List<SequenceInfo>();

            string[] lines = Properties.Resources.SEQS
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int i = 0;
            while (i < lines.Length)
            {
                var sourceName = lines[i];
                var sourceType = Array.ConvertAll(lines[i + 1].Split(','), int.Parse).ToList();
                var sourceInstrument = Convert.ToInt32(lines[i + 2], 16);

                var targetName = lines[i];
                var targetType = Array.ConvertAll(lines[i + 1].Split(','), int.Parse).ToList();
                var targetInstrument = Convert.ToInt32(lines[i + 2], 16);

                SequenceInfo sourceSequence = new SequenceInfo
                {
                    Name = sourceName,
                    Type = sourceType,
                    Instrument = sourceInstrument
                };

                SequenceInfo targetSequence = new SequenceInfo
                {
                    Name = targetName,
                    Type = targetType,
                    Instrument = targetInstrument
                };

                if (sourceSequence.Name.StartsWith("mm-"))
                {
                    targetSequence.Replaces = Convert.ToInt32(lines[i + 3], 16);
                    sourceSequence.MM_seq = Convert.ToInt32(lines[i + 3], 16);
                    TargetSequences.Add(targetSequence);
                    i += 4;
                }
                else
                {
                    if (sourceSequence.Name == "mmr-f-sot")
                    {
                        sourceSequence.Replaces = 0x33;
                    };

                    i += 3;
                };

                if (sourceSequence.MM_seq != 0x18)
                {
                    SequenceList.Add(sourceSequence);
                };
            };
        }

        private void BGMShuffle()
        {
            while (TargetSequences.Count > 0)
            {
                List<SequenceInfo> Unassigned = SequenceList.FindAll(u => u.Replaces == -1);

                int targetIndex = RNG.Next(TargetSequences.Count);
                while (true)
                {
                    int unassignedIndex = RNG.Next(Unassigned.Count);

                    if (Unassigned[unassignedIndex].Name.StartsWith("mm")
                        & (RNG.Next(100) < 50))
                    {
                        continue;
                    };

                    for (int i = 0; i < Unassigned[unassignedIndex].Type.Count; i++)
                    {
                        if (TargetSequences[targetIndex].Type.Contains(Unassigned[unassignedIndex].Type[i]))
                        {
                            Unassigned[unassignedIndex].Replaces = TargetSequences[targetIndex].Replaces;
                            Debug.WriteLine(Unassigned[unassignedIndex].Name + " -> " + TargetSequences[targetIndex].Name);
                            TargetSequences.RemoveAt(targetIndex);
                            break;
                        }
                        else if (i + 1 == Unassigned[unassignedIndex].Type.Count)
                        {
                            if ((RNG.Next(30) == 0)
                                && ((Unassigned[unassignedIndex].Type[0] & 8) == (TargetSequences[targetIndex].Type[0] & 8))
                                && (Unassigned[unassignedIndex].Type.Contains(10) == TargetSequences[targetIndex].Type.Contains(10))
                                && (!Unassigned[unassignedIndex].Type.Contains(16)))
                            {
                                Unassigned[unassignedIndex].Replaces = TargetSequences[targetIndex].Replaces;
                                Debug.WriteLine(Unassigned[unassignedIndex].Name + " -> " + TargetSequences[targetIndex].Name);
                                TargetSequences.RemoveAt(targetIndex);
                                break;
                            };
                        };
                    };

                    if (Unassigned[unassignedIndex].Replaces != -1)
                    {
                        break;
                    };
                };
            };

            SequenceList.RemoveAll(u => u.Replaces == -1);
        }

        private void SortBGM()
        {
            if (!Settings.RandomizeBGM)
            {
                return;
            };
            ReadSeqInfo();
            BGMShuffle();
        }

        #endregion

        private void SetTatlColour()
        {
            if (Settings.TatlColorSchema == TatlColorSchema.Rainbow)
            {
                for (int i = 0; i < 10; i++)
                {
                    byte[] c = new byte[4];
                    RNG.NextBytes(c);

                    if ((i % 2) == 0)
                    {
                        c[0] = 0xFF;
                    }
                    else
                    {
                        c[0] = 0;
                    };

                    Values.TatlColours[4, i] = BitConverter.ToUInt32(c, 0);
                };
            };
        }

        private void CreateTextSpoilerLog(Spoiler spoiler, string path)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine($"{"Version:",-17} {spoiler.Version}");
            log.AppendLine($"{"Settings String:",-17} {spoiler.SettingsString}");
            log.AppendLine($"{"Seed:",-17} {spoiler.Seed}");
            log.AppendLine();

            if (spoiler.RandomizeDungeonEntrances)
            {
                log.AppendLine($" {"Entrance",-21}    {"Destination"}");
                log.AppendLine();
                string[] destinations = new string[] { "Woodfall", "Snowhead", "Inverted Stone Tower", "Great Bay" };
                for (int i = 0; i < 4; i++)
                {
                    log.AppendLine($"{destinations[i],-21} >> {destinations[_newEnts[i]]}");
                }
                log.AppendLine("");
            }

            log.AppendLine($" {"Item",-40}    {"Location"}");
            foreach (var item in spoiler.ItemList)
            {
                string name = Items.ITEM_NAMES[item.ID];
                string replaces = Items.ITEM_NAMES[item.ReplacesItemId];
                log.AppendLine($"{name,-40} >> {replaces}");
            }

            log.AppendLine();
            log.AppendLine();

            log.AppendLine($" {"Item",-40}    {"Location"}");
            foreach (var item in spoiler.ItemList.OrderBy(item => item.ReplacesItemId))
            {
                string name = Items.ITEM_NAMES[item.ID];
                string replaces = Items.ITEM_NAMES[item.ReplacesItemId];
                log.AppendLine($"{name,-40} >> {replaces}");
            }

            log.AppendLine();
            log.AppendLine();

            foreach (string entrance in spoiler.EntranceList)
            {
                log.AppendLine(entrance);
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(log.ToString());
            }
        }


        private void PrepareRulesetItemData()
        {
            ItemList = new List<ItemObject>();

            if (Settings.LogicMode == LogicMode.Casual
                || Settings.LogicMode == LogicMode.Glitched
                || Settings.LogicMode == LogicMode.UserLogic)
            {
                string[] data = ReadRulesetFromResources();
                PopulateItemListFromLogicData(data);
            }
            else
            {
                PopulateItemListWithoutLogic();
            }

            AddRequirementsForSongOath();
        }

        private void AddRequirementsForSongOath()
        {
            int[] OathReq = new int[] { 100, 103, 108, 113 };
            ItemList[Items.SongOath].DependsOnItems = new List<int>();
            ItemList[Items.SongOath].DependsOnItems.Add(OathReq[RNG.Next(4)]);
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
                        };
                        break;
                };

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
                };
                currentItem.Conditionals = conditional;
            };
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
                };
                currentItem.DependsOnItems = dependencies;
            };
        }

        private string[] ReadRulesetFromResources()
        {
            string[] lines = null;
            var mode = Settings.LogicMode;

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
                using (StreamReader Req = new StreamReader(File.Open(openLogic.FileName, FileMode.Open)))
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
                    targetItem = RNG.Next(1, availableItems.Count);
                }
                else
                {
                    targetItem = RNG.Next(availableItems.Count);
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

        private void ItemShuffle()
        {
            if (Settings.UseCustomItemList)
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

            int freeItem = RNG.Next(Items.SongOath + 1);
            if (ForbiddenReplacedBy.ContainsKey(Items.MaskDeku))
            {
                while (ItemList[freeItem].ReplacesItemId != -1
                    || ForbiddenReplacedBy[Items.MaskDeku].Contains(freeItem))
                {
                    freeItem = RNG.Next(Items.SongOath + 1);
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
            if (Settings.ExcludeSongOfSoaring)
            {
                ItemList[Items.SongSoaring].ReplacesItemId = Items.SongSoaring;
            }

            if (!Settings.AddSongs)
            {
                ShuffleSongs();
            }

            if (!Settings.AddDungeonItems)
            {
                PreserveDungeonItems();
            }

            if (!Settings.AddShopItems)
            {
                PreserveShopItems();
            }

            if (!Settings.AddOther)
            {
                PreserveOther();
            }

            if (Settings.RandomizeBottleCatchContents)
            {
                AddBottleCatchContents();
            }
            else
            {
                PreserveBottleCatchContents();
            }

            if (!Settings.AddMoonItems)
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
                itemPool.Add(i);
            };

            for (int i = Items.BottleCatchFairy; i <= Items.BottleCatchMushroom; i++)
            {
                PlaceItem(i, itemPool);
            };
        }

        /// <summary>
        /// Keeps other vanilla
        /// </summary>
        private void PreserveOther()
        {
            for (int i = Items.ChestLensCaveRedRupee; i <= Items.ChestToGoronRaceGrotto; i++)
            {
                ItemList[i].ReplacesItemId = i;
            };
        }

        /// <summary>
        /// Keeps shop items vanilla
        /// </summary>
        private void PreserveShopItems()
        {
            for (int i = Items.ShopItemTradingPostRedPotion; i <= Items.ShopItemZoraRedPotion; i++)
            {
                ItemList[i].ReplacesItemId = i;
            };

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
            };
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
            Settings.AddShopItems = false;

            // Make all items vanilla, and override using custom item list
            MakeAllItemsVanilla();
            PreserveAreasAndOther();

            // Should these be vanilla by default? Why not check settings.
            ApplyCustomItemList();

            // Should these be randomized by default? Why not check settings.
            AddBottleCatchContents();

            if (!Settings.AddSongs)
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
            for (int i = 0; i < fItemEdit.selected_items.Count; i++)
            {
                int selectedItem = fItemEdit.selected_items[i];

                if (selectedItem > Items.SongOath)
                {
                    // Skip entries describing areas and other
                    selectedItem += Values.NumberOfAreasAndOther;
                }
                int selectedItemIndex = ItemList.FindIndex(u => u.ID == selectedItem);

                if (selectedItemIndex != -1)
                {
                    ItemList[selectedItemIndex].ReplacesItemId = -1;
                }

                if (ItemUtils.IsShopItem(selectedItem))
                {
                    Settings.AddShopItems = true;
                }
            }
        }

        /// <summary>
        /// Keeps area and other vanilla
        /// </summary>
        private void PreserveAreasAndOther()
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemUtils.IsAreaOrOther(i)
                    || ItemUtils.IsOutOfRange(i))
                {
                    continue;
                }

                ItemList[i].ReplacesItemId = i;
            };
        }
    }

}