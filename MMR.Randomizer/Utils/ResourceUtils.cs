using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MMR.Randomizer.Utils
{

    public class ResourceUtils
    {
        public static void ApplyHack(byte[] hack_content)
        {
            int addr = 0;
            while (hack_content[addr] != 0xFF)
            {
                //Debug.WriteLine(addr.ToString("X4"));
                uint dest = ReadWriteUtils.Arr_ReadU32(hack_content, addr);
                addr += 4;
                uint len = ReadWriteUtils.Arr_ReadU32(hack_content, addr);
                addr += 4;
                int f = RomUtils.GetFileIndexForWriting((int)dest);
                dest -= (uint)RomData.MMFileList[f].Addr;
                ReadWriteUtils.Arr_Insert(hack_content, addr, (int)len, RomData.MMFileList[f].Data, (int)dest);
                addr += (int)len;
            }
        }

        public static void ApplyIndexedHack(int index, params byte[][] hacks)
        {
            ApplyHack(hacks[index]);
        }

        public static List<int[]> GetAddresses(byte[] addresses_content)
        {
            List<int[]> Addrs = new List<int[]>();
            int i = 0;
            while (addresses_content[i] != 0xFF)
            {
                int count = (int)ReadWriteUtils.Arr_ReadU32(addresses_content, i);
                int[] alist = new int[count];
                i += 4;
                for (int j = 0; j < count; j++)
                {
                    alist[j] = (int)ReadWriteUtils.Arr_ReadU32(addresses_content, i);
                    i += 4;
                }
                Addrs.Add(alist);
            }
            return Addrs;
        }

        public static List<int[]> GetIndexedAddresses(int index, params byte[][] addresses_contents)
        {
            return GetAddresses(addresses_contents[index]);
        }

    }

}