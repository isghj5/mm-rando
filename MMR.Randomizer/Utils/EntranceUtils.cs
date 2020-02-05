using MMR.Randomizer.Models.Rom;
using System.Collections.Generic;
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

        public static ushort ReadSceneExit(int sceneNumber, int exitNumber)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            RomUtils.CheckCompressed(f);
            foreach (int exitAddress in scene.ExitAddr)
            {
                return ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, (int)exitAddress + exitNumber * 2);
            }
            return 0xFFFF;
        }

        public static void ReadSceneExits(int sceneNumber, int count)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            RomUtils.CheckCompressed(f);
            Debug.WriteLine($"Scene Number: {sceneNumber.ToString("X2")}");
            ushort tempExit;
            bool isSceneRead = false;
            foreach (int exitAddress in scene.ExitAddr)
            {
                for (int i = 0; i <= count; i++)
                {
                    if (!isSceneRead)
                    {
                        tempExit = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, (int)exitAddress + i * 2);
                        System.Diagnostics.Debug.WriteLine($"{i}: {tempExit.ToString("X4")}");
                    }
                }
                isSceneRead = true;
            }
        }

        public static void WriteSceneExits(int sceneNumber, List<ushort> shuffledExits, List<int> shuffledIndexes)
        {
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            RomUtils.CheckCompressed(f);
            foreach( int exitAddress in scene.ExitAddr)
            {
                for (int i = 0; i < shuffledExits.Count; i++)
                {
                    ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, (int)exitAddress + shuffledIndexes[i] * 2, shuffledExits[i]);
                }
            }
        }
    }
}