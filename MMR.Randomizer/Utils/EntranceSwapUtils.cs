using MMR.Randomizer.Constants;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System.Collections.Generic;

namespace MMR.Randomizer.Utils
{
    public class EntranceSwapUtils
    {
        private static Dictionary<int, int> sceneSync = new Dictionary<int, int>()
            {
                { 69, 0 },     // swamp
                { 80, 90 },     // mountain village
                { 93, 94 },     // twin islands
                { 77, 72 }      // goron village
            };
        internal static void WriteNewEntrance(Item exit, Item newSpawn)
        {
            var spawnId = newSpawn.SpawnId();
            foreach (var exitInfo in exit.ExitIndices())
            {
                var sceneNumber = exitInfo.Item1;
                var exitIndex = exitInfo.Item2;
                EntranceUtils.WriteSceneExits(sceneNumber, exitIndex, spawnId);
                if (sceneSync.ContainsKey(sceneNumber))
                {
                    EntranceUtils.WriteSceneExits(sceneSync[sceneNumber], exitIndex, spawnId);
                }
            }
            foreach (var cutsceneExitInfo in exit.ExitCutscenes())
            {
                var sceneNumber = cutsceneExitInfo.Item1;
                var setupIndex = cutsceneExitInfo.Item2;
                var cutsceneIndex = cutsceneExitInfo.Item3;
                EntranceUtils.WriteCutsceneExits(sceneNumber, setupIndex, cutsceneIndex, spawnId);
                if (sceneSync.ContainsKey(sceneNumber))
                {
                    EntranceUtils.WriteCutsceneExits(sceneSync[sceneNumber], setupIndex, cutsceneIndex, 0x0200);
                }
            }
            foreach (var address in exit.ExitAddresses())
            {
                ReadWriteUtils.WriteToROM(address, spawnId);
            }

            // special cases
            if (exit != newSpawn)
            {
                if (exit == Item.EntranceTerminaFieldFromAstralObservatoryTelescope)
                {
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-telescope-music");
                }
            }
        }

        internal static void WriteSpawnToROM(Item newSpawn)
        {
            var spawnAddress = newSpawn.SpawnId();
            ReadWriteUtils.WriteToROM(0xBDB882, spawnAddress);
            ReadWriteUtils.WriteToROM(0x02E90FD4, spawnAddress);
            ReadWriteUtils.WriteToROM(0x02E90FDC, spawnAddress);
        }

        //private static void FinalizeEntrances()
        //{
        //    ShuffledEntranceList = new Dictionary<int, List<ushort>>();
        //    ExitListIndices = new Dictionary<int, List<int>>();
        //    Dictionary<int, int> sceneSync = new Dictionary<int, int>()
        //    {
        //        { 69, 0 },     // swamp
        //        { 80, 90 },     // mountain village
        //        { 93, 94 },     // twin islands
        //        { 77, 72 }      // goron village
        //    };
        //    ushort spawnAddress;
        //    int sceneIndex;
        //    foreach (Exit exit in TerminaMapData.exits)
        //    {
        //        spawnAddress = TerminaMapData.SpawnAddress(exit.SpawnName);
        //        sceneIndex = TerminaMapData.SceneIndex(exit.RegionName);
        //        if (spawnAddress != 0xFFFF && sceneIndex != -1)
        //        {
        //            FinalizeExit(spawnAddress, sceneIndex, exit);
        //            if (sceneSync.ContainsKey(sceneIndex))
        //            {
        //                if (!"Goron Village: Lens Grotto".Equals(exit.ExitName))
        //                {
        //                    FinalizeExit(spawnAddress, sceneSync[sceneIndex], exit);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine($"{exit.ExitName} not placed: {spawnAddress}, {sceneIndex}");
        //        }
        //    }
        //    // correct spring lens grotto to return to the void out spawn instead of above nothing
        //    foreach( List<ushort> exitSpawns in ShuffledEntranceList.Values)
        //    {
        //        while( exitSpawns.Contains(0x8A30))
        //        {
        //            int i = exitSpawns.IndexOf(0x8A30);
        //            exitSpawns[i] = 0x8A40;
        //        }
        //    }
        //}
    }
}
