using MMR.Randomizer.Models.Rom;
using System.Diagnostics;
using System.Linq;

namespace MMR.Randomizer.Utils
{

    public static class EntranceUtils
    {
        private static int GetEntranceAddr(int ent)
        {
            int offset = ((ent >> 9) * 12) + 0xC5BC64;
            int f = RomUtils.GetFileIndexForWriting(offset);

            offset -= RomData.MMFileList[f].Addr;
            uint p1 = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, offset);
            offset = ((ent >> 4) & 0x1F) * 4;

            p1 = (uint)((p1 & 0xFFFFFF) + 0xA96540 - RomData.MMFileList[f].Addr);
            p1 = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, (int)(p1 + offset));
            p1 = (uint)((p1 & 0xFFFFFF) + 0xA96540 - RomData.MMFileList[f].Addr);

            offset = (ent & 0xF) << 2;
            return (int)p1 + offset;
        }
        public static void WriteEntrances(int[] olde, int[] newe)
        {
            int f = RomUtils.GetFileIndexForWriting(0xC5BC64);
            uint[] data = new uint[newe.Length];

            for (int i = 0; i < newe.Length; i++)
            {
                data[i] = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, GetEntranceAddr(newe[i]));
            }

            for (int i = 0; i < newe.Length; i++)
            {
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, GetEntranceAddr(olde[i]), data[i]);
            }
        }

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