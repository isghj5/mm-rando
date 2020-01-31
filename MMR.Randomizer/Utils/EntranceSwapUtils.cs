using MMR.Randomizer.Constants;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MMR.Randomizer.Utils
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
            List<Entrance> entranceList = new List<Entrance>();
            Entrance ent;
            TerminaMapData.entrances.ForEach(e => {
                ent = new Entrance(e.EntranceName, e.ExitName, e.ReturnExitName, e.Type)
                {
                    SpawnName = e.ReturnSpawnName,
                    ReturnSpawnName = e.SpawnName,
                    Properties = e.Properties
                };
                entranceList.Add(ent);
            });
            TerminaMapData.entrances = entranceList;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            string spawnJson = JsonConvert.SerializeObject(TerminaMapData, Formatting.Indented);
            Debug.WriteLine(spawnJson);
            using (StreamWriter file = new StreamWriter(Values.MainDirectory + @"\Resources\ENTRANCES.json"))
            {
                file.Write(spawnJson);
            }
        }

        internal static Exit LookupItemExitName(Item item)
        {
            Exit exit = TerminaMapData.exits.Find(x => (int)item == x.LogicIndex);
            return (exit != null && exit.LogicIndex != 0) ? exit : null;
        }

        internal static bool IsEntranceAvailable(Item item)
        {
            Exit exit = TerminaMapData.exits.Find(x => (int)item == x.LogicIndex);
            return exit != null && exit.LogicIndex != 0;
        }

        internal static string LookupItemSpawnName(Item item)
        {
            Exit exit = TerminaMapData.exits.Find(x => (int)item == x.LogicIndex);
            if (exit != null && exit.LogicIndex != 0)
            {
                Entrance ent = TerminaMapData.entrances.Find(e => exit.ExitName.Equals(e.ExitName));
                if (ent != null)
                {
                    return ent.ReturnSpawnName;
                }
            }
            return "";
        }

        internal static void WriteNewEntrance(Item value, Item item)
        {
            Exit x = LookupItemExitName(value);
            string chosenEntrance = LookupItemSpawnName(item);
            if (x != null && !"".Equals(chosenEntrance))
            {
                x.SpawnName = chosenEntrance;
            }
        }

        internal static void WriteEntrancesToROM()
        {
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            FinalizeEntrances();
            foreach (int sceneIndex in ShuffledEntranceList.Keys)
            {
                if (ExitListIndices.ContainsKey(sceneIndex))
                {
                    EntranceUtils.WriteSceneExits(sceneIndex, ShuffledEntranceList[sceneIndex], ExitListIndices[sceneIndex]);
                }
            }
        }

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
            // correct spring lens grotto to return to the void out spawn instead of above nothing
            foreach( List<ushort> exitSpawns in ShuffledEntranceList.Values)
            {
                while( exitSpawns.Contains(0x8A30))
                {
                    int i = exitSpawns.IndexOf(0x8A30);
                    exitSpawns[i] = 0x8A40;
                }
            }
        }
    }
}
