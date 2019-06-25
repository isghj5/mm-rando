using MMRando.Models.Rom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMRando.Utils
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

        public static void WriteSceneExits(int sceneNumber, List<ushort> originalExits, ushort[] shuffledExits)
        {
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            RomUtils.CheckCompressed(f);
            int exitAddress, shuffledIndex;
            ushort tempExit;
            exitAddress = scene.ExitAddr;
            Dictionary<int, ushort> shuffledExitValues = new Dictionary<int, ushort>();
            for (int i = 0; i < shuffledExits.Length; i++)
            {
                tempExit = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, (int)exitAddress + i * 2);
                System.Diagnostics.Debug.WriteLine($"{tempExit.ToString("X4")} : {originalExits[i].ToString("X4")} : {shuffledExits[i].ToString("X4")}");
                
                if ( originalExits.Contains( tempExit ))
                {
                    shuffledIndex = originalExits.FindIndex(x => x == tempExit);
                    System.Diagnostics.Debug.WriteLine($"{shuffledIndex}: [{tempExit.ToString("X4")} -> {shuffledExits[i].ToString("X4")}]");
                    if (shuffledExits[i] != 0xFFFF)
                    {
                        shuffledExitValues.Add((int)exitAddress + shuffledIndex * 2, shuffledExits[i]);
                    }
                }
            }
            foreach (int address in shuffledExitValues.Keys)
            {
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, address, shuffledExitValues[address]);
            }
        }
    }
}