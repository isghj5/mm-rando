using MMRando.Constants;
using MMRando.Extensions;
using MMRando.LogicMigrator;
using MMRando.Models;
using MMRando.Models.Rom;
using MMRando.Models.Settings;
using MMRando.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Starting items should not be replaced by trade items, or items that can be downgraded.
        private readonly List<int> ForbiddenStartingItems = new List<int>
            {
                Items.ChestMountainVillageGrottoBottle,

                // Starting with Magic Bean or Powder Keg doesn't actually give you one,
                // nor do you get one when you play Song of Time.
                Items.ItemMagicBean,
                Items.ItemPowderKeg,
            }
            .Concat(Enumerable.Range(Items.TradeItemMoonTear, Items.TradeItemMamaLetter - Items.TradeItemMoonTear + 1))
            .Concat(Enumerable.Range(Items.ItemBottleWitch, Items.ItemBottleMadameAroma - Items.ItemBottleWitch + 1))
            .ToList();
        private readonly ReadOnlyCollection<ReadOnlyCollection<int>> ForbiddenStartTogether = new List<List<int>>()
        {
            new List<int>
            {
                Items.ItemBow,
                Items.UpgradeBigQuiver,
                Items.UpgradeBiggestQuiver,
            },
            new List<int>
            {
                Items.ItemBombBag,
                Items.UpgradeBigBombBag,
                Items.UpgradeBiggestBombBag,
            },
            new List<int>
            {
                Items.UpgradeAdultWallet,
                Items.UpgradeGiantWallet,
            },
            new List<int>
            {
                Items.UpgradeRazorSword,
                Items.UpgradeGildedSword,
            },
        }.Select(list => list.AsReadOnly()).ToList().AsReadOnly();

        private readonly Dictionary<int, List<int>> ForbiddenReplacedBy = new Dictionary<int, List<int>>
        {
            // Keaton_Mask and Mama_Letter are obtained one directly after another
            // Keaton_Mask cannot be replaced by items that may be overwritten by item obtained at Mama_Letter
            {
                Items.MaskKeaton,
                new List<int> {
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

        private readonly Dictionary<int, List<int>> ForbiddenPlacedAt = new Dictionary<int, List<int>>
        {
        };

        #endregion

        private SettingsObject _settings;
        private RandomizedResult _randomized;

        public Randomizer(SettingsObject settings)
        {
            _settings = settings;
            if (!_settings.PreventDowngrades)
            {
                ForbiddenReplacedBy[Items.MaskKeaton].AddRange(Items.DOWNGRADABLE_ITEMS);
                ForbiddenStartingItems.AddRange(Items.DOWNGRADABLE_ITEMS);
            }
        }

        //rando functions

        #region Gossip quotes

        private void MakeGossipQuotes()
        {
            _randomized.GossipQuotes = MessageUtils.MakeGossipQuotes(_randomized);
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
        Dictionary<string, List<Exit>> TerminaMap { get; set; }
        Dictionary<string, List<Exit>> ShuffledMap { get; set; }
        Dictionary<ushort, List<string>> SceneNamesByIndex { get; set; }
        private void EntranceShuffle()
        {
            TerminaMap = new Dictionary<string, List<Exit>>();
            ShuffledMap = new Dictionary<string, List<Exit>>();
            SceneNamesByIndex = new Dictionary<ushort, List<string>>();
            ReadTerminaMap();
            ShuffleEntrances();
            //CheckEntrances();
            FinalizeEntrances();
            //WriteMapData();
        }

        private void ConnectPairedEntrances()
        {
            List<Exit> allSpawns = GetSpawns();
            foreach (List<Exit> spawns in TerminaMap.Values)
            {
                foreach (Exit spawn in spawns)
                {
                    if (spawn.ExitId > 0 && spawn.ExitId < allSpawns.Count)
                    {
                        Exit match = allSpawns.Find(s => s.ID == spawn.ExitId);
                        spawn.ExitSpawn = match;
                    }
                }
            }
        }

        private void WriteMapData()
        {
            List<Exit> spawns = new List<Exit>();
            Dictionary<string, bool> added = new Dictionary<string, bool>();
            int spawnIndex = 1;
            foreach (ushort sceneIndex in SceneNamesByIndex.Keys)
            {
                foreach (string sceneName in SceneNamesByIndex[sceneIndex])
                {
                    if (TerminaMap.ContainsKey(sceneName))
                    {
                        foreach (Exit s in TerminaMap[sceneName].Select(spawn => new Exit()
                        {
                            //ID = spawn.ID,
                            //SceneName = spawn.SceneName,
                            //SceneId = sceneIndex,
                            //SpawnName = spawn.SpawnName,
                            //SpawnAddress = spawn.SpawnAddress,
                            //SpawnAddressString = spawn.SpawnAddress.ToString("X4"),
                            //SpawnType = spawn.SpawnType,
                            //ExitId = spawn.ExitId,
                            //ExitName = spawn.ExitSpawn == null ? "" : spawn.ExitSpawn.SpawnName,
                            //ExitIndex = (spawn.ExitSpawn == null) ? 0 : (spawn.ExitSpawn.SpawnAddress & 0xF0) >> 4,
                            ID = spawn.ID,
                            SceneName = spawn.SceneName,
                            SceneId = spawn.SceneId,
                            SpawnName = spawn.SpawnName,
                            SpawnAddress = spawn.SpawnAddress,
                            SpawnAddressString = spawn.SpawnAddressString,
                            SpawnType = spawn.SpawnType,
                            ExitId = spawn.ExitId,
                            ExitName = spawn.ExitName,
                            ExitIndex = spawn.ExitIndex
                        }))
                        {
                            if (!added.ContainsKey(s.SpawnName))
                            {
                                spawns.Add(s);
                                spawnIndex++;
                                added[s.SpawnName] = true;
                            }
                        }
                    }
                }
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            string spawnJson = JsonConvert.SerializeObject(spawns,Formatting.Indented);
            Debug.WriteLine(spawnJson);
            using (StreamWriter file = new StreamWriter(Values.MainDirectory + @"\Resources\ENTRANCES.json"))
            {
                file.Write(spawnJson);
            }
        }

        private void ReadTerminaMap()
        {
            StreamReader file = new StreamReader(Values.MainDirectory + @"\Resources\ENTRANCES.json");
            string spawnJson = file.ReadToEnd();
            List<Exit> spawnData = JsonConvert.DeserializeObject<List<Exit>>(spawnJson);
            file.Close();
            foreach (Exit spawn in spawnData)
            {
                AddExitSpawn(spawn);
            }
            ConnectPairedEntrances();
        }

        private bool CanReturn(Dictionary<string, bool> AllowedSpawn, Exit S)
        {
            bool result = true;
            if (S.ExitSpawn != null)
            {
                if (!AllowedSpawn.ContainsKey(S.ExitSpawn.SpawnName) || !AllowedSpawn[S.ExitSpawn.SpawnName])
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
            foreach (Exit S in GetSpawns())
            {
                if (S.SpawnAddress == 0xFFFF) { continue; }
                SpawnType = S.SpawnType == null ? "None" : S.SpawnType;

                if (S.SpawnName.Contains("Boss Chamber") && _settings.RandomizeDungeonEntrances)
                { SpawnType = "Interior"; }
                if (S.SpawnName.Contains("Dungeon Clear") && _settings.RandomizeDungeonEntrances)
                { SpawnType = "Interior Exit"; }

                if (SpawnType == "Interior Exit" && ShuffleOneWay)
                { SpawnType = "Interior"; }
                if (SpawnType == "Telescope Spawn" && ShuffleOneWay)
                { SpawnType = "Telescope"; }
                if (SpawnType == "Moon" && ShuffleMoon)
                { SpawnType = "Interior"; }

                if (S.SpawnName.Contains("Owl Warp"))
                { SpawnType = ShuffleOwls ? "Owl Warp" : "Permanent"; }
                if (S.SpawnName.Contains("Grotto"))
                { SpawnType = ShuffleGrotto ? "Grotto" : "Permanent"; }
                if (S.SpawnType == null && S.ExitSpawn == null)
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
                    if (S.SpawnName == SpawnName)
                    {
                        SpawnType = "Permanent";
                    }
                }

                if (!SpawnTypeSet.ContainsKey(SpawnType))
                {
                    SpawnTypeSet.Add(SpawnType, new List<string>());
                }
                SpawnTypeSet[SpawnType].Add(S.SpawnName);
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
                "Grotto",
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
            Predicate<Exit> CanAdd = S => S != null && SpawnSet[pool].ContainsKey(S.SpawnName) && SpawnSet[pool][S.SpawnName];
            Predicate<Exit> CanChoose = S =>
                S != null && ChosenSet[pool].ContainsKey(S.SpawnName) && ChosenSet[pool][S.SpawnName] &&
                (S.ExitSpawn == null || S.ExitSpawn != null && CanReturn(SpawnSet[pool], S));
            Exit To, From;
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
                            TypeSet => { if (TypeSet.Contains(From.SpawnName)) { SpawnTypePool = TypeSet; } });
                        To = ChooseNextEntrance(ChosenSet[pool], From.SpawnType, SpawnTypePool, CanChoose);
                        if (To != null)
                        {
                            SpawnSet[pool][From.SpawnName] = false;
                            ChosenSet[pool][To.SpawnName] = false;
                            FillWorld.RemoveAt(0);
                            if (From.SpawnName == "South Clock Town: Clock Tower")
                            {
                                ConnectEntrances(From.SpawnName, To.SpawnName, false);
                            }
                            else
                            {
                                ConnectEntrances(From.SpawnName, To.SpawnName, true);
                                if (To.ExitSpawn != null )
                                {
                                    SpawnSet[pool][To.ExitSpawn.SpawnName] = false;
                                    if (FillWorld.Contains(To.ExitSpawn.SpawnName))
                                    {
                                        FillWorld.Remove(To.ExitSpawn.SpawnName);
                                    }
                                    if( From.ExitSpawn != null )
                                    {
                                        ChosenSet[pool][From.ExitSpawn.SpawnName] = false;
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

        private Exit ChooseNextEntrance(Dictionary<string, bool> SpawnSet, string DepartureType, List<string> DeparturePool, Predicate<Exit> CanAdd)
        {
            Dictionary<string, string> spawnTypeMap = new Dictionary<string, string>()
            {
                { "Interior", "Interior Exit" },
                {"Interior Exit", "interior" }
            };
            string matchType = spawnTypeMap.ContainsKey(DepartureType) ? spawnTypeMap[DepartureType] : DepartureType;
            List<string> candidates = new List<string>();
            foreach (string SpawnName in SpawnSet.Keys)
            {
                if (SpawnSet[SpawnName])
                {
                    Exit S = GetSpawn(SpawnName);
                    if (CanAdd.Invoke(S))
                    {
                        // need to keep this up to date with the section above
                        if (matchType == S.SpawnType)
                        {
                            candidates.Add(S.SpawnName);
                        }
                        else if (DeparturePool != null && DeparturePool.Contains(S.SpawnName))
                        {
                            candidates.Add(S.SpawnName);
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
            foreach (Exit S in GetSpawns())
            {
                Visited[S.SpawnName] = 0;
            }
            foreach( string Scene in TerminaMap.Keys)
            {
                Visited[Scene] = 0;
            }
            Exit Next;
            List<KeyValuePair<string, int>> Remaining;
            int group = 0;
            FillWorld.Push(GetShuffledSpawn("South Clock Town: Clock Tower").SpawnName);
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
                                foreach (Exit SpawnPoint in ShuffledMap[SceneName])
                                {
                                    if (SpawnPoint != null && SpawnPoint.ExitSpawn != null)
                                        {
                                            if (Visited.ContainsKey(SpawnPoint.ExitSpawn.SpawnName) && Visited[SpawnPoint.ExitSpawn.SpawnName] == 0)
                                            {
                                                FillWorld.Push(SpawnPoint.ExitSpawn.SpawnName);
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
                        Visited[Next.SpawnName] = group;
                        if (Next != null)
                        {
                            if (Next.ExitSpawn != null)
                            {
                                AddEntranceToPath(Paths, CurrentPath, Next.SpawnName + " -> " + Next.ExitSpawn.SpawnName);
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
                TerminaMap.Add(Scene, new List<Exit>());
                ShuffledMap.Add(Scene, new List<Exit>());
            }
            List<Exit> sceneSpawns = TerminaMap[Scene];
            Exit newSpawn = new Exit(Name, Address, Scene);
            sceneSpawns.Add(newSpawn);
            sceneSpawns = ShuffledMap[Scene];
            sceneSpawns.Add(null);
        }

        private void AddExitSpawn(Exit spawn)
        {
            string scene = spawn.SceneName;
            if (!TerminaMap.ContainsKey(scene))
            {
                TerminaMap.Add(scene, new List<Exit>());
                ShuffledMap.Add(scene, new List<Exit>());
            }
            List<Exit> sceneSpawns = TerminaMap[scene];
            sceneSpawns.Add(spawn);
            sceneSpawns = ShuffledMap[scene];
            sceneSpawns.Add(null);
            AddSceneNameIndex(spawn.SceneName, spawn.SceneId);
        }

        private Exit GetSpawn(string Name)
        {
            Exit temp;
            foreach (List<Exit> SceneSpawns in TerminaMap.Values)
            {
                temp = SceneSpawns.Find(u => Name.Equals(u.SpawnName));
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
            GetSpawn(OutdoorEntrance).SpawnType = "Interior Exit";

        }

        private void PairSingleSpawn(string From, string To, string Type)
        {
            Exit F = GetSpawn(From);
            Exit T = GetSpawn(To);
            if (F != null && T != null)
            {
                F.SpawnType = Type;
                T.SpawnType = Type;
                if (F.ExitSpawn == null)
                {
                    F.ExitSpawn = T;
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
        }

        private void PairSingleInterior(string InteriorScene, string OuterScene, ushort InteriorAddress, ushort OuterAddress)
        {
            AddSpawn(OuterScene + ": " + InteriorScene, OuterAddress, OuterScene);
            AddPairedInteriors(new string[] { InteriorScene }, new ushort[] { InteriorAddress }, new ushort[] { 0xFFFF });
        }

        private void PairTelescope(string SpawnPoint, string Telescope)
        {
            Exit Room = GetSpawn(SpawnPoint);
            Exit Scope = GetSpawn(Telescope);
            if (Room != null && Scope != null)
            {
                Scope.SpawnType = "Telescope";
                Room.SpawnType = "Telescope Spawn";
                Scope.ExitSpawn = Room;
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
                foreach (Exit s in GetSpawns())
                {
                    if (!spawnName.Equals(s.SpawnName) && s.SpawnName.Contains(spawnName))
                    {
                        PairInteriorEntrance(s.SpawnName, spawnName);
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
            List<Exit> SpawnSet = GetSpawns();
            Dictionary<Exit, string> SpawnPoint = new Dictionary<Exit, string>();
            Dictionary<Exit, string> SpawnExit = new Dictionary<Exit, string>();
            int sep;
            foreach (Exit S in SpawnSet)
            {
                sep = S.SpawnName.IndexOf(':');
                if (sep != -1)
                {
                    SpawnPoint[S] = S.SpawnName.Substring(0, sep);
                    SpawnExit[S] = S.SpawnName.Substring(sep + 2);
                }
            }
            int j;
            for (int i = 0; i < SpawnSet.Count; i++)
            {
                if (SpawnSet[i].ExitSpawn == null && SpawnPoint.ContainsKey(SpawnSet[i]) && SpawnExit.ContainsKey(SpawnSet[i]))
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
                        if (SpawnSet[i].SpawnType != "" || SpawnSet[j].SpawnType != "")
                        {
                            PairSpawns(SpawnSet[i].SpawnName, SpawnSet[j].SpawnName, "Overworld");
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
            foreach(Exit S in TerminaMap["Moon"])
            {
                if(S.SpawnName != "Moon")
                {
                    PairSingleSpawn(S.SpawnName, "Moon", "Moon");
                }
            }
        }

        private Exit GetShuffledSpawn(string Spawn)
        {
            Exit S = GetSpawn(Spawn);
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

        private void SetShuffledSpawn(Exit f, Exit t)
        {
            int shuffleIndex = -1;
            List<Exit> temp = TerminaMap[f.SceneName];
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
            Exit f = GetSpawn(from);
            Exit t = GetSpawn(to);
            if (f != null && t != null)
            {
                SetShuffledSpawn(f, t);
                if (connectReverse && f.ExitSpawn != null && t.ExitSpawn != null)
                {
                    SetShuffledSpawn(t.ExitSpawn, f.ExitSpawn);
                }
            }
        }

        private List<Exit> GetSpawns()
        {
            List<Exit> Spawns = new List<Exit>();
            foreach (List<Exit> Scene in TerminaMap.Values)
            {
                Spawns.AddRange(Scene);
            }
            return Spawns;
        }

        public void FinalizeEntrances()
        {
            Dictionary<ushort, List<Exit>> EntranceShuffle = new Dictionary<ushort, List<Exit>>();
            _randomized.EntranceList = new Dictionary<int, ushort[]>();
            _randomized.ShuffledEntranceList = new Dictionary<int, ushort[]>();
            _randomized.ExitListIndices = new Dictionary<int, int[]>();
            _randomized.EntranceSpoilers = new List<SpoilerEntrance>();
            ushort[] sceneExitList, shuffledSceneExitList;
            int[] sceneExitIndices;
            Exit ShuffledExit;
            bool WasPlaced;
            int numExits, currExit;

            foreach ( Exit s in GetSpawns().Where(x=>x.SpawnType!="Owl Warp"))
            {
                if(!EntranceShuffle.ContainsKey(s.SceneId))
                {
                    EntranceShuffle[s.SceneId] = new List<Exit>();
                }
                EntranceShuffle[s.SceneId].Add(s);
            }

            foreach (ushort sceneIndex in EntranceShuffle.Keys)
            {
                numExits = EntranceShuffle[sceneIndex].Count;
                sceneExitList = new ushort[numExits];
                _randomized.EntranceList[sceneIndex] = sceneExitList;
                shuffledSceneExitList = new ushort[numExits];
                _randomized.ShuffledEntranceList[sceneIndex] = shuffledSceneExitList;
                sceneExitIndices = new int[numExits];
                _randomized.ExitListIndices[sceneIndex] = sceneExitIndices;
                currExit = 0;
                foreach (Exit Exit in EntranceShuffle[sceneIndex])
                {
                    ShuffledExit = GetShuffledSpawn(Exit.SpawnName);
                    sceneExitList[currExit] = Exit.SpawnAddress;
                    sceneExitIndices[currExit] = Exit.ExitIndex;
                    if (Exit.SpawnAddress != 0xFFFF)
                    {
                        WasPlaced = ShuffledExit != null && ShuffledExit.SpawnAddress != 0xFFFF;
                        if (WasPlaced)
                        {
                            shuffledSceneExitList[currExit] = ShuffledExit.SpawnAddress;
                            _randomized.EntranceSpoilers.Add(new SpoilerEntrance(Exit, ShuffledExit, WasPlaced));
                        } else {
                            shuffledSceneExitList[currExit] = Exit.SpawnAddress;
                            _randomized.EntranceSpoilers.Add(new SpoilerEntrance(Exit, Exit, WasPlaced));
                        }
                    }
                    currExit++;
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
                }
            }
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

            if (ItemUtils.IsStartingItem(target) && ForbiddenStartingItems.Contains(currentItem))
            {
                Debug.WriteLine($"{currentItem} cannot be a starting item.");
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
            if (currentItem > Items.SongOath)
            {
                availableItems.Remove(Items.MaskDeku);
                availableItems.Remove(Items.SongHealing);
            }

            while (true)
            {
                if (availableItems.Count == 0)
                {
                    throw new Exception($"Unable to place {Items.ITEM_NAMES[currentItem]} anywhere.");
                }

                int targetItem = Random.Next(availableItems.Count);

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
            PlaceFreeItems(itemPool);
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
            PlaceItem(Items.IkanaScrubGoldRupee, itemPool);
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
            for (int i = Items.SongHealing; i <= Items.SongOath; i++)
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
        /// Replace starting deku mask and song of healing with free items if not already replaced.
        /// </summary>
        private void PlaceFreeItems(List<int> itemPool)
        {
            var forbiddenStartingItems = ForbiddenStartingItems.ToList();
            if (ItemList.FindIndex(item => item.ReplacesItemId == Items.MaskDeku) == -1)
            {
                int freeItem = Random.Next(Items.SongOath + 1);
                while (ItemList[freeItem].ReplacesItemId != -1
                    || forbiddenStartingItems.Contains(freeItem))
                {
                    freeItem = Random.Next(Items.SongOath + 1);
                }
                ItemList[freeItem].ReplacesItemId = Items.MaskDeku;
                itemPool.Remove(Items.MaskDeku);

                var forbiddenStartTogether = ForbiddenStartTogether.FirstOrDefault(list => list.Contains(freeItem));
                if (forbiddenStartTogether != null)
                {
                    forbiddenStartingItems.AddRange(forbiddenStartTogether);
                }
            }
            if (ItemList.FindIndex(item => item.ReplacesItemId == Items.SongHealing) == -1)
            {
                int freeItem = Random.Next(Items.SongOath + 1);
                while (ItemList[freeItem].ReplacesItemId != -1
                    || forbiddenStartingItems.Contains(freeItem))
                {
                    freeItem = Random.Next(Items.SongOath + 1);
                }
                ItemList[freeItem].ReplacesItemId = Items.SongHealing;
                itemPool.Remove(Items.SongHealing);
            }
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
            for (int i = Items.ChestLensCaveRedRupee; i <= Items.IkanaScrubGoldRupee; i++)
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
            for (int i = Items.SongHealing; i <= Items.SongOath; i++)
            {
                if (ItemList[i].ReplacesAnotherItem)
                {
                    continue;
                }
                itemPool.Add(i);
            }

            for (int i = Items.SongHealing; i <= Items.SongOath; i++)
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

        private ReadOnlyCollection<MoonPathItem> GetRequiredItems(int itemId, List<ItemLogic> itemLogic, List<int> logicPath = null, Dictionary<int, ReadOnlyCollection<MoonPathItem>> checkedItems = null, int depth = 0)
        {
            if (logicPath == null)
            {
                logicPath = new List<int>();
            }
            if (logicPath.Contains(itemId))
            {
                return null;
            }
            logicPath.Add(itemId);
            if (checkedItems == null)
            {
                checkedItems = new Dictionary<int, ReadOnlyCollection<MoonPathItem>>();
            }
            if (checkedItems.ContainsKey(itemId))
            {
                var oldMinDepth = checkedItems[itemId].Min(t => (int?)t.Depth) ?? 0;
                return checkedItems[itemId].Select(mpi => new MoonPathItem(mpi.Depth - oldMinDepth + depth, mpi.ItemId)).ToList().AsReadOnly();
            }
            var itemObject = ItemList[itemId];
            var locationId = itemObject.ReplacesAnotherItem ? itemObject.ReplacesItemId : itemId;
            var locationLogic = itemLogic[locationId];
            var result = new List<MoonPathItem>();
            if (locationLogic.RequiredItemIds != null)
            {
                foreach (var requiredItemId in locationLogic.RequiredItemIds)
                {
                    var requiredChildren = GetRequiredItems(requiredItemId, itemLogic, logicPath.ToList(), checkedItems, depth + 1);
                    if (requiredChildren == null)
                    {
                        return null;
                    }
                    result.Add(new MoonPathItem(depth, requiredItemId));
                    result.AddRange(requiredChildren);
                }
            }
            if (locationLogic.ConditionalItemIds != null)
            {
                List<MoonPathItem> lowestRequirements = null;
                foreach (var conditions in locationLogic.ConditionalItemIds)
                {
                    var conditionalRequirements = new List<MoonPathItem>();
                    foreach (var conditionalItemId in conditions)
                    {
                        var requiredChildren = GetRequiredItems(conditionalItemId, itemLogic, logicPath.ToList(), checkedItems, depth + 1);
                        if (requiredChildren == null)
                        {
                            conditionalRequirements = null;
                            break;
                        }

                        conditionalRequirements.Add(new MoonPathItem(depth, conditionalItemId));
                        conditionalRequirements.AddRange(requiredChildren);
                    }
                    conditionalRequirements = conditionalRequirements?.DistinctBy(mpi => mpi.ItemId).ToList();
                    if (conditionalRequirements != null && (lowestRequirements == null || conditionalRequirements.Count < lowestRequirements.Count))
                    {
                        lowestRequirements = conditionalRequirements;
                    }
                }
                if (lowestRequirements == null)
                {
                    return null;
                }
                result.AddRange(lowestRequirements);
            }
            var readonlyResult = result.DistinctBy(mpi => mpi.ItemId).ToList().AsReadOnly();
            checkedItems[itemId] = readonlyResult;
            return readonlyResult;
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

                if (true)
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

                _randomized.RequiredItemsForMoonAccess = GetRequiredItems(Items.AreaMoonAccess, _randomized.Logic);
                if (_randomized.RequiredItemsForMoonAccess == null)
                {
                    throw new Exception("Moon Access is unobtainable.");
                }

                if (_settings.GossipHintStyle != GossipHintStyle.Default)
                {
                    worker.ReportProgress(35, "Making gossip quotes...");

                    //gossip
                    SeedRNG();
                    MakeGossipQuotes();
                }
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