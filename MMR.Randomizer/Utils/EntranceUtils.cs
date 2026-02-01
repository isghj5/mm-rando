using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    public static class EntranceUtils
    {
        public static void WriteSceneExits(int sceneNumber, byte exitIndex, ushort spawnId)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            if (scene.Setups.Count > 1)
            {
                Debug.WriteLine(scene.Number);
            }
            foreach (var setup in scene.Setups)
            {
                if (setup.ExitListAddress == null)
                {
                    continue;
                }
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, setup.ExitListAddress.Value + exitIndex * 2, spawnId);
            }
        }

        public static void WritePolygonTypeExitIndex(byte sceneNumber, byte polygonType, IEnumerable<byte> newExitIndices, ushort spawnId)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            foreach (var setup in scene.Setups)
            {
                if (setup.PolygonTypeDefinitionsAddress == null)
                {
                    continue;
                }
                var polygonTypeData0 = ReadWriteUtils.Arr_ReadS32(RomData.MMFileList[f].Data, setup.PolygonTypeDefinitionsAddress.Value + polygonType * 8);
                //var polygonTypeData1 = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, setup.PolygonTypeDefinitionsAddress.Value + polygonType * 8 + 4);

                var sceneExitIndex = ((polygonTypeData0 & 0x1F00) >> 8) - 1;
                if (sceneExitIndex < 0)
                {
                    throw new Exception("Attempt to write exit to non-exit polygon type.");
                }
                if (setup.ExitListAddress == null)
                {
                    continue;
                }
                var exitValue = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, setup.ExitListAddress.Value + sceneExitIndex * 2);
                if (exitValue == spawnId)
                {
                    continue;
                }

                ushort? newExitIndex = null;
                foreach (var testExitIndex in newExitIndices)
                {
                    var testExitValue = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, setup.ExitListAddress.Value + testExitIndex * 2);
                    if (testExitValue == spawnId || testExitValue == 0)
                    {
                        newExitIndex = testExitIndex;
                        break;
                    }
                }

                if (!newExitIndex.HasValue)
                {
                    throw new Exception("Could not find available exit index.");
                }

                var newPolygonTypeData0 = (polygonTypeData0 & ~0x1F00) | (((newExitIndex.Value + 1) & 0x1F) << 8);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, setup.PolygonTypeDefinitionsAddress.Value + polygonType * 8, (uint)newPolygonTypeData0);

                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, setup.ExitListAddress.Value + newExitIndex.Value * 2, spawnId);
            }
        }

        public static void WriteSceneActorParams(byte sceneNumber, byte setupIndex, byte roomIndex, byte actorIndex, ushort actorId, ushort param, ushort paramMask, ushort? rotXParam, ushort? rotYParam, ushort? rotZParam)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            var room = scene.Setups[setupIndex].Rooms[roomIndex];
            int f = room.File;
            if (!room.ActorListAddress.HasValue)
            {
                return;
            }

            var actorEntryAddress = room.ActorListAddress.Value + actorIndex * 0x10;
            var actorEntryId = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress);
            if ((actorEntryId & 0x1FFF) != actorId)
            {
                throw new Exception("Attempt to write to incorrect actor entry.");
            }
            var actorEntryParam = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xE);
            var newParam = (ushort)((actorEntryParam & ~paramMask) | (param & paramMask));
            ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xE, newParam);

            if (rotXParam.HasValue)
            {
                var actorRotX = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0x8);
                var newRotX = (ushort)((actorRotX & 0x7F) | ((rotXParam.Value & 0x1FF) << 7));
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, actorEntryAddress + 0x8, newRotX);
            }

            if (rotYParam.HasValue)
            {
                var actorRotY = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xA);
                var newRotY = (ushort)((actorRotY & 0x7F) | ((rotYParam.Value & 0x1FF) << 7));
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xA, newRotY);
            }

            if (rotZParam.HasValue)
            {
                var actorRotZ = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xC);
                var newRotZ = (ushort)((actorRotZ & 0x7F) | ((rotZParam.Value & 0x1FF) << 7));
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xC, newRotZ);
            }
        }

        public static void AssertSceneActorParams(byte sceneNumber, byte setupIndex, byte roomIndex, byte actorIndex, ushort actorId, ushort param, ushort paramMask, ushort? rotXParam, ushort? rotYParam, ushort? rotZParam)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            var room = scene.Setups[setupIndex].Rooms[roomIndex];
            int f = room.File;
            if (!room.ActorListAddress.HasValue)
            {
                return;
            }

            var actorEntryAddress = room.ActorListAddress.Value + actorIndex * 0x10;
            var expectedActorId = actorId;

            if (rotXParam.HasValue)
            {
                var actorRotX = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0x8) >> 7;
                if (rotXParam.Value != actorRotX)
                {
                    throw new Exception("Incorrect RotXParam");
                }
                expectedActorId |= 0x4000;
            }

            if (rotYParam.HasValue)
            {
                var actorRotY = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xA) >> 7;
                if (rotYParam.Value != actorRotY)
                {
                    throw new Exception("Incorrect RotYParam");
                }
                expectedActorId |= 0x8000;
            }

            if (rotZParam.HasValue)
            {
                var actorRotZ = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xC) >> 7;
                if (rotZParam.Value != actorRotZ)
                {
                    throw new Exception("Incorrect RotZParam");
                }
                expectedActorId |= 0x2000;
            }

            var actorEntryId = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress);
            if (actorEntryId != expectedActorId)
            {
                throw new Exception("Incorrect actor id.");
            }
            var actorEntryParam = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, actorEntryAddress + 0xE);
            if ((actorEntryParam & paramMask) != (param & paramMask))
            {
                throw new Exception("Incorrect param.");
            }
        }

        public static void WriteCutsceneExits(int sceneNumber, byte setupIndex, byte cutsceneIndex, ushort spawnId)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            var setup = scene.Setups[setupIndex];
            if (setup.CutsceneListAddress == null)
            {
                return;
            }
            ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, setup.CutsceneListAddress.Value + cutsceneIndex * 8 + 4, spawnId);
        }
    }
}
