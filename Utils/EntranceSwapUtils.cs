using MMRando.Constants;
using MMRando.GameObjects;
using MMRando.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MMRando.Utils
{
    class EntranceSwapUtils
    {
        static EntranceData TerminaMapData { get; set; }
        static Dictionary<int, List<int>> ExitListIndices { get; set; }
        static Dictionary<int, List<ushort>> ShuffledEntranceList { get; set; }

        internal static void ReadMapData()
        {
            StreamReader file = new StreamReader(Values.MainDirectory + @"\Resources\ENTRANCES.json");
            string spawnJson = file.ReadToEnd();
            TerminaMapData = JsonConvert.DeserializeObject<EntranceData>(spawnJson);
            file.Close();
        }

        internal static void WriteMapData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            string spawnJson = JsonConvert.SerializeObject(TerminaMapData, Formatting.Indented);
            Debug.WriteLine(spawnJson);
            using (StreamWriter file = new StreamWriter(Values.MainDirectory + @"\Resources\ENTRANCES.json"))
            {
                file.Write(spawnJson);
            }
        }

        private List<List<string>> GetEntrancePools()
        {
            Dictionary<string, List<string>> entrancesByType = new Dictionary<string, List<string>>();
            Dictionary<string, bool> entranceUsed = new Dictionary<string, bool>();
            TerminaMapData.entrances.ForEach(ent => {
                string entType = ent.Type;
                if (!entrancesByType.ContainsKey(entType)) { entrancesByType.Add(entType, new List<string>()); }
                List<string> entSet = entrancesByType[entType];
                entSet.Add(ent.EntranceName);
                entranceUsed.Add(ent.EntranceName, false);
            });
            List<List<string>> typePools = new List<List<string>>
            {
                new List<string> { "Overworld", "Interior", "Interior Exit", "Dungeon", "Dungeon Exit", "Trial", "Boss", "Boss Exit" },
                //new List<string> { "Overworld: Water", "Interior: Water", "Interior Exit: Water"},
                //new List<string> { "Overworld", "Dungeon" },
                //new List<string> { "Interior", "Boss", "Trial" },
                //new List<string> { "Interior: 2" },
                //new List<string> { "Interior: 1" },
                //new List<string> { "Boss", "Trial" },
            };
            List<List<string>> pools = new List<List<string>>();
            List<string> pool;
            List<string> poolProps;
            string type;
            int entCount;
            foreach (List<string> typePool in typePools)
            {
                pool = new List<string>();
                foreach (string poolName in typePool)
                {
                    poolProps = poolName.Split(':').ToList();
                    poolProps.ForEach(p => p = p.Trim());
                    if (poolProps.Count > 0)
                    {
                        type = poolProps[0];
                        if (poolProps.Count > 1)
                        {
                            poolProps = poolProps[1].Split(' ').ToList();
                        }
                        else
                        {
                            poolProps = new List<string> { };
                        }
                        if (entrancesByType.ContainsKey(type))
                        {
                            entrancesByType[type].ForEach(e => {
                                bool valid = true;
                                foreach (string prop in poolProps)
                                {
                                    if (int.TryParse(prop, out entCount))
                                    {
                                        valid = TerminaMapData.EntranceRegionTypeCount(e, new List<string> { type, type + " Exit" }, entCount);
                                    }
                                    else
                                    {
                                        valid = TerminaMapData.EntranceHasProperty(e, prop);
                                    }
                                }
                                if (valid)
                                {
                                    if (entranceUsed.ContainsKey(e) && !entranceUsed[e])
                                    {
                                        pool.Add(e);
                                        entranceUsed[e] = true;
                                    }
                                }
                            });
                        }
                    }
                }
                pools.Add(pool);
            };
            return pools;
        }

        internal static string LookupItemExitName(Item item)
        {
            Exit exit = TerminaMapData.exits.Find(x => (int)item == x.LogicIndex);
            if(exit != null && exit.LogicIndex != 0)
            {
                Entrance ent = TerminaMapData.entrances.Find(e => exit.ExitName.Equals(e.ExitName));
                if(ent != null)
                {
                    return ent.EntranceName;
                }
            }
            return "";
        }

        internal static void WriteNewEntrance(Item value, Item item)
        {
            string selectionEntrance = LookupItemExitName(item);
            string chosenEntrance = LookupItemExitName(value);
            if (!"".Equals(selectionEntrance) && !"".Equals(chosenEntrance))
            {
                string s = TerminaMapData.ReverseEntrance(selectionEntrance);
                string c = TerminaMapData.ReverseEntrance(chosenEntrance);
                if (!"".Equals(s) && !"".Equals(c)) { bool success = TerminaMapData.ConnectEntrance(s, c); }
            }
        }

        internal static void WriteEntrancesToROM()
        {
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            // boss lairs
            foreach (int i in new int[] { 31, 68, 95, 54 }) EntranceUtils.ReadSceneExits(i, 16);
            // pirate's fortress
            foreach (int i in new int[] { 20, 35, 59 }) EntranceUtils.ReadSceneExits(i, 16);
            foreach (int i in new int[] { 8 }) EntranceUtils.ReadSceneExits(i, 16);
            FinalizeEntrances();
            foreach (int sceneIndex in ShuffledEntranceList.Keys)
            {
                if (ExitListIndices.ContainsKey(sceneIndex))
                {
                    EntranceUtils.WriteSceneExits(sceneIndex, ShuffledEntranceList[sceneIndex], ExitListIndices[sceneIndex]);
                }
            }
        }

        //private void ShuffleEntrances()
        //{
        //    List<List<string>> entrancePools = GetEntrancePools();
        //    List<string> sourceEntrances, destEntrances;
        //    Dictionary<string, int> entranceUsed = new Dictionary<string, int>();
        //    foreach (List<string> entrancePool in entrancePools)
        //    {
        //        sourceEntrances = new List<string>();
        //        destEntrances = new List<string>();
        //        foreach (string entrance in entrancePool)
        //        {
        //            sourceEntrances.Add(entrance);
        //            destEntrances.Add(entrance);
        //            entranceUsed[entrance] = 0;
        //        }
        //        bool success;
        //        int n;
        //        string selectionEntrance, chosenEntrance, reverseSelectionEntrance, reverseChosenEntrance;
        //        // all this does is reverse entrances within pools
        //        // we'll want to pluck each entrance and it's opposite off the pool as we go
        //        while (destEntrances.Count > 0)
        //        {
        //            selectionEntrance = sourceEntrances[0];
        //            reverseSelectionEntrance = TerminaMapData.ReverseEntrance(selectionEntrance);
        //            if (entranceUsed.ContainsKey(reverseSelectionEntrance)) { entranceUsed[reverseSelectionEntrance]++; }
        //            n = _random.Next(destEntrances.Count);
        //            chosenEntrance = destEntrances[n];
        //            reverseChosenEntrance = TerminaMapData.ReverseEntrance(chosenEntrance);
        //            if (entranceUsed.ContainsKey(chosenEntrance)) { entranceUsed[chosenEntrance]++; }

        //            if (selectionEntrance.Equals(reverseSelectionEntrance))
        //            {
        //                Debug.WriteLine($"{selectionEntrance} does not appear to have an opposite");
        //            }
        //            if (chosenEntrance.Equals(reverseChosenEntrance))
        //            {
        //                Debug.WriteLine($"{selectionEntrance} does not appear to have an opposite");
        //            }
        //            sourceEntrances.Remove(selectionEntrance);
        //            destEntrances.Remove(chosenEntrance);
        //            // these entrances have implicitly been set, so remove them from the selection pool
        //            if (!selectionEntrance.Equals(reverseSelectionEntrance) && !chosenEntrance.Equals(reverseChosenEntrance))
        //            {
        //                sourceEntrances.Remove(reverseChosenEntrance);
        //                destEntrances.Remove(reverseSelectionEntrance);
        //            }
        //            success = TerminaMapData.ConnectEntrance(selectionEntrance, chosenEntrance);
        //            if (success)
        //            {
        //                EntranceSpoilers.Add(new SpoilerEntrance(selectionEntrance, chosenEntrance));
        //                if ("Overworld".Equals(TerminaMapData.EntranceType(selectionEntrance)) && !selectionEntrance.Equals(reverseSelectionEntrance) && !chosenEntrance.Equals(reverseChosenEntrance))
        //                {
        //                    EntranceSpoilers.Add(new SpoilerEntrance(reverseChosenEntrance, reverseSelectionEntrance));
        //                }
        //            }
        //        }
        //        foreach (string entrance in entranceUsed.Keys)
        //        {
        //            Debug.WriteLine($"{entrance}: {entranceUsed[entrance]}");
        //        }
        //    }

        //}

        private static void FinalizeExit(ushort spawnAddress, int sceneIndex, Exit exit)
        {
            if (exit.ExitIndex == 255) { return; }
            List<ushort> sceneExitSpawns;
            List<int> sceneExitIndices;
            if (!ShuffledEntranceList.ContainsKey(sceneIndex))
            {
                ShuffledEntranceList[sceneIndex] = new List<ushort>();
            }
            sceneExitSpawns = ShuffledEntranceList[sceneIndex];
            if (!ExitListIndices.ContainsKey(sceneIndex))
            {
                ExitListIndices[sceneIndex] = new List<int>();
            }
            sceneExitIndices = ExitListIndices[sceneIndex];
            sceneExitSpawns.Add(spawnAddress);
            sceneExitIndices.Add(exit.ExitIndex);
            Debug.WriteLine($"[{exit.RegionName}:{sceneIndex}] Exit {exit.ExitIndex} ({exit.ExitName}): {exit.SpawnName} [{spawnAddress.ToString("X4")}]");

        }

        private static void FinalizeEntrances()
        {
            ShuffledEntranceList = new Dictionary<int, List<ushort>>();
            ExitListIndices = new Dictionary<int, List<int>>();
            Dictionary<int, int> sceneSync = new Dictionary<int, int>()
            {
                { 69, 0 },     // swamp
                { 80, 90 },     // mountain village
                { 93, 94 },     // twin islands
                { 77, 72 }      // goron village
            };
            ushort spawnAddress;
            int sceneIndex;
            foreach (Exit exit in TerminaMapData.exits)
            {
                spawnAddress = TerminaMapData.SpawnAddress(exit.SpawnName);
                sceneIndex = TerminaMapData.SceneIndex(exit.RegionName);
                if (spawnAddress != 0xFFFF && sceneIndex != -1)
                {
                    FinalizeExit(spawnAddress, sceneIndex, exit);
                    if (sceneSync.ContainsKey(sceneIndex))
                    {
                        if (!"Goron Village: Lens Grotto".Equals(exit.ExitName))
                        {
                            FinalizeExit(spawnAddress, sceneSync[sceneIndex], exit);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine($"{exit.ExitName} not placed: {spawnAddress}, {sceneIndex}");
                }
            }
        }
    }
}
