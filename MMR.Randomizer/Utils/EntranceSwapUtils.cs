using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

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
        internal static void WriteNewEntrance(Entrance exit, Entrance newSpawn)
        {
            foreach (var hackContent in exit.HackContent(exit == newSpawn))
            {
                ResourceUtils.ApplyHack(hackContent);
            }

            var spawnId = newSpawn.SpawnId();
            if (spawnId.HasValue)
            {
                var exitPolygonType = exit.ExitPolygonType();
                if (exitPolygonType != null)
                {
                    EntranceUtils.WritePolygonTypeExitIndex(exitPolygonType.SceneId, exitPolygonType.PolygonType, exitPolygonType.NewExitIndices, spawnId.Value);
                }
                foreach (var exitInfo in exit.ExitIndices())
                {
                    var sceneNumber = exitInfo.Item1;
                    var exitIndex = exitInfo.Item2;
                    EntranceUtils.WriteSceneExits(sceneNumber, exitIndex, spawnId.Value);
                    if (sceneSync.ContainsKey(sceneNumber))
                    {
                        EntranceUtils.WriteSceneExits(sceneSync[sceneNumber], exitIndex, spawnId.Value);
                    }
                }
                foreach (var cutsceneExitInfo in exit.ExitCutscenes())
                {
                    var sceneNumber = cutsceneExitInfo.Item1;
                    var setupIndex = cutsceneExitInfo.Item2;
                    var cutsceneIndex = cutsceneExitInfo.Item3;
                    EntranceUtils.WriteCutsceneExits(sceneNumber, setupIndex, cutsceneIndex, spawnId.Value);
                    if (sceneSync.ContainsKey(sceneNumber))
                    {
                        EntranceUtils.WriteCutsceneExits(sceneSync[sceneNumber], setupIndex, cutsceneIndex, spawnId.Value);
                    }
                }
                foreach (var address in exit.ExitAddresses())
                {
                    ReadWriteUtils.WriteToROM(address, spawnId.Value);
                }
            }
            else
            {
                if (!exit.ExitActorParams().Any())
                {
                    throw new Exception($"Failed to make any changes for {exit} leading to {newSpawn}.");
                }
            }
            var spawnActorParams = newSpawn.SpawnActorParams();
            foreach (var exitActorParams in exit.ExitActorParams())
            {
                EntranceUtils.WriteSceneActorParams(exitActorParams.SceneId, exitActorParams.SetupIndex, exitActorParams.RoomIndex, exitActorParams.ActorIndex, spawnActorParams.ActorId, spawnActorParams.Param, spawnActorParams.ParamMask, spawnActorParams.RotXParam, spawnActorParams.RotYParam, spawnActorParams.RotZParam);
            }
        }

        //internal static void WriteSpawnToROM(Entrance newSpawn)
        //{
        //    var spawnAddress = newSpawn.SpawnId();
        //    ReadWriteUtils.WriteToROM(0xBDB882, spawnAddress);
        //    ReadWriteUtils.WriteToROM(0x02E90FD4, spawnAddress);
        //    ReadWriteUtils.WriteToROM(0x02E90FDC, spawnAddress);
        //}
    }
}
