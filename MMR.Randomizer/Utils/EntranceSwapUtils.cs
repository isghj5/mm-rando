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
            // special cases
            if (exit != newSpawn)
            {
                if (exit == Item.EntranceTerminaFieldFromAstralObservatoryTelescope)
                {
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-telescope-music");
                }
                if (exit == Item.EntranceDekuPalaceFromDekuPalace)
                {
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-deku-patrol-exit");
                }
                if (exit == Item.EntranceZoraCapeFromGreatBayTempleClear)
                {
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-greatbay-clear-exit");
                }
                if (exit == Item.EntranceIkanaCanyonFromIkanaClear)
                {
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-ikana-clear-exit");
                }
            }

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
                    EntranceUtils.WriteCutsceneExits(sceneSync[sceneNumber], setupIndex, cutsceneIndex, spawnId);
                }
            }
            foreach (var address in exit.ExitAddresses())
            {
                ReadWriteUtils.WriteToROM(address, spawnId);
            }
        }

        internal static void WriteSpawnToROM(Item newSpawn)
        {
            var spawnAddress = newSpawn.SpawnId();
            ReadWriteUtils.WriteToROM(0xBDB882, spawnAddress);
            ReadWriteUtils.WriteToROM(0x02E90FD4, spawnAddress);
            ReadWriteUtils.WriteToROM(0x02E90FDC, spawnAddress);
        }

        internal static void WriteOwlRegionNameTable()
        {
            int soarTextTableAddress = 0x00C66C54;
            int soarTextSizeTableAddress = 0x00C66D04;
            int soarFile = RomUtils.GetFileIndexForWriting(soarTextTableAddress);
            List<string> newNames = new List<string>
            {
                "Boss Lair", "Pirate's Fortress", "The Moon", "Ikana Graveyard", "West Clock Town", 
                "Zora Hall", "Pinnacle Rock", "Stock Pot Inn", "Lottery Shop", "Termina Field",
            };
            for( int owl = 0; owl < 10; owl ++)
            {
                byte[] nameData = new byte[16];
                string name = newNames[owl];
                for(int i = 0; i< 16;i++)
                {
                    byte t;
                    if( i < name.Length)
                    {
                        t = (byte)name[i];
                    } else
                    {
                        t = 0;
                    }
                    nameData[i] = t;
                }
                ReadWriteUtils.WriteToROM(soarTextTableAddress + (owl << 4), nameData);
                ReadWriteUtils.WriteToROM(soarTextSizeTableAddress + (owl << 1), (ushort)newNames[owl].Length);
            }
            ReadOwlRegionNameTable();
        }

        internal static void ReadOwlRegionNameTable()
        {
            int soarNameOffset = 0x00C66C54;
            int soarSizeOffset = 0x00C66D04;
            List<string> tableNames = new List<string>(10);
            for (int owl = 0; owl < 10; owl++)
            {
                tableNames.Add("");
                byte[] name = ReadWriteUtils.ReadBytes(soarNameOffset + (owl << 4), 16);
                for (int i = 0; i < 0x10; i++)
                {
                    tableNames[owl] += name[i]== 0 ? '?' : System.Convert.ToChar(name[i]);
                }
                ushort t = ReadWriteUtils.ReadU16(soarSizeOffset + (owl << 1));
                System.Diagnostics.Debug.WriteLine("'" + tableNames[owl] + "' = " + t);
            }
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
