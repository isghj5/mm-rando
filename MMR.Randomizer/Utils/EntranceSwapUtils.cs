using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Constants;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        internal static void WriteOwlRegionNameTable(IEnumerable<ItemObject> allLocations)
        {
            var owlStatues = new List<Item> {
                    Item.EntranceGreatBayCoastFromOwlStatue, Item.EntranceZoraCapeFromOwlStatue,
                    Item.EntranceSnowheadFromOwlStatue, Item.EntranceMountainVillageFromOwlStatue,
                    Item.EntranceSouthClockTownFromOwlStatue,Item.EntranceMilkRoadFromOwlStatue,
                    Item.EntranceWoodfallFromOwlStatue, Item.EntranceSouthernSwampFromOwlStatue,
                    Item.EntranceIkanaCanyonFromOwlStatue, Item.EntranceStoneTowerFromOwlStatue
                };
            var owlLocations = allLocations.Where(io => io.NewLocation.HasValue && owlStatues.Contains(io.NewLocation.Value)).ToList();
            var soarTextTableAddress = 0x00C66C54;
            var soarTextSizeTableAddress = 0x00C66D04;

            var newNames = new List<string>();
            foreach (var owl in owlStatues)
            {
                var item = owlLocations.Single(io => io.NewLocation.Value == owl);
                var spawn = item.Item.GetAttribute<SpawnAttribute>();
                newNames.Add(spawn.Scene.ToString().AddSpaces());
            }
            for (var owl = 0; owl < 10; owl ++)
            {
                byte[] nameData = new byte[16];
                string name = newNames[owl];
                for(var i = 0; i< 16;i++)
                {
                    byte t;
                    if(i < name.Length)
                    {
                        t = (byte)name[i];
                    }
                    else
                    {
                        t = 0;
                    }
                    nameData[i] = t;
                }
                ReadWriteUtils.WriteToROM(soarTextTableAddress + (owl << 4), nameData);
                ReadWriteUtils.WriteToROM(soarTextSizeTableAddress + (owl << 1), (ushort)Math.Min(newNames[owl].Length, 16));
            }
            ReadOwlRegionNameTable();
        }

        internal static void ReadOwlRegionNameTable()
        {
            int soarNameOffset = 0x00C66C54;
            int soarSizeOffset = 0x00C66D04;
            var tableNames = new List<string>(10);
            for (var owl = 0; owl < 10; owl++)
            {
                tableNames.Add("");
                var name = ReadWriteUtils.ReadBytes(soarNameOffset + (owl << 4), 16);
                for (var i = 0; i < 0x10; i++)
                {
                    tableNames[owl] += name[i]== 0 ? '?' : System.Convert.ToChar(name[i]);
                }
                var t = ReadWriteUtils.ReadU16(soarSizeOffset + (owl << 1));
                System.Diagnostics.Debug.WriteLine("'" + tableNames[owl] + "' = " + t);
            }
        }
    }
}
