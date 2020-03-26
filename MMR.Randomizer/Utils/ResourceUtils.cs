using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MMR.Randomizer.Utils
{

    public class ResourceUtils
    {
        public static byte[] ReadHack(string path, string name)
        {
            using (var reader = new BinaryReader(File.OpenRead(Path.Combine(path, name))))
            {
                int hack_len = (int)reader.BaseStream.Length;
                byte[] hack_content = new byte[hack_len];
                reader.Read(hack_content, 0, hack_len);
                return hack_content;
            }
        }

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

        public static void ApplyHack(string path, string name)
        {
            var hack = ReadHack(path, name);
            ApplyHack(hack);
        }

        public static List<int[]> GetAddresses(string path, string name)
        {
            List<int[]> Addrs = new List<int[]>();
            byte[] a;
            using (BinaryReader AddrFile = new BinaryReader(File.OpenRead(Path.Combine(path, name))))
            {
                a = new byte[AddrFile.BaseStream.Length];
                AddrFile.Read(a, 0, a.Length);
            }
            int i = 0;
            while (a[i] != 0xFF)
            {
                int count = (int)ReadWriteUtils.Arr_ReadU32(a, i);
                int[] alist = new int[count];
                i += 4;
                for (int j = 0; j < count; j++)
                {
                    alist[j] = (int)ReadWriteUtils.Arr_ReadU32(a, i);
                    i += 4;
                }
                Addrs.Add(alist);
            }
            return Addrs;
        }

    }

}