using MMRando.Models.Rom;
using System;
using System.Collections.Generic;

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

        public static void WriteSceneExits(int SceneNumber, uint[] OriginalExit, uint[] ShuffledExit)
        {
            int StartScene = Math.Min(SceneNumber, RomData.SceneList.Count-1);
            for (int i = StartScene; i >= 0; i--)
            {
                if( RomData.SceneList[i].Number == SceneNumber)
                {
                    int f = RomData.SceneList[i].File;
                    RomUtils.CheckCompressed(f);
                    int k = 0, s = 0;
                    uint exitListAddress;
                    ushort Temp;
                    byte cmd;
                    do
                    {
                        cmd = RomData.MMFileList[f].Data[k];
                        if (cmd == 0x13)
                        {
                            exitListAddress = ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, k + 4) & 0xFFFFFF;
                            for (int j = 0; j < OriginalExit.Length; j++)
                            {
                                s = j;
                                Temp = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, (int)exitListAddress + s * 2);
                                System.Diagnostics.Debug.WriteLine($"{Temp.ToString("X4")} : {OriginalExit[j].ToString("X4")} : {ShuffledExit[j].ToString("X4")}");
                                if (Temp != OriginalExit[j])
                                {
                                    s = -1;
                                    do
                                    {
                                        s++;
                                        Temp = ReadWriteUtils.Arr_ReadU16(RomData.MMFileList[f].Data, (int)exitListAddress + s * 2);
                                    } while (s < OriginalExit.Length && Temp != OriginalExit[j]);
                                }
                                if (Temp == OriginalExit[j])
                                {
                                    System.Diagnostics.Debug.WriteLine(Temp.ToString("X4"));
                                }
                            }
                        }
                        k += 8;
                    } while (cmd != 0x14);
                }
            }
        }
    }
}