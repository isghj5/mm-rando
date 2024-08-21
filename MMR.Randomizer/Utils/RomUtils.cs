using Be.IO;
using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using MMR.Common.Extensions;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Attributes;
using System.Numerics;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.Utils
{
    using Yaz = Yaz.Yaz;

    public static class RomUtils
    {
        const int FILE_TABLE = 0x1A500;
        const int SIGNATURE_ADDRESS = 0x1A4D0;
        public static void SetStrings(byte[] hack, string ver, string setting)
        {
            ResourceUtils.ApplyHack(hack);
            int veraddr = 0xC44E30;
            int settingaddr = 0xC44E70;
            string verstring = $"MM Rando {ver}\x00";

            #if DEBUG
            string settingstring = $"{setting} + DEBUG BUILD\x00";
            #else
            string settingstring = $"{setting} + Isghj's Actorizer Test 73.8\x00";
            #endif
            int f = GetFileIndexForWriting(veraddr);
            var file = RomData.MMFileList[f];

            byte[] buffer = Encoding.ASCII.GetBytes(verstring);
            int addr = veraddr - file.Addr;
            ReadWriteUtils.Arr_Insert(buffer, 0, buffer.Length, file.Data, addr);

            buffer = Encoding.ASCII.GetBytes(settingstring);
            addr = settingaddr - file.Addr;
            ReadWriteUtils.Arr_Insert(buffer, 0, buffer.Length, file.Data, addr);
        }

        public static int AddNewFile(byte[] content)
        {
            int index = RomUtils.AppendFile(content);
            return RomData.MMFileList[index].Addr;
        }

        public static int AddrToFile(int RAddr)
        {
            return RomData.MMFileList.FindIndex(
                file => RAddr >= file.Addr && RAddr < file.End);
        }

        public static int GetFIDFromVROM(int actorStartVROM, int startID = 3, int endID = 1550)
        {
            /// search for File ID from VROM address in DMA table
            /// WARNING: this assumes you are searching vanilla files only, which are default sorted by VROM

            var dmaFID = 2;
            var dmaData = RomData.MMFileList[dmaFID].Data;

            int Linear(int start)
            {
                for (int i = start; i < 1550; ++i)
                {
                    // xxxxxxxx yyyyyyyy aaaaaaaa bbbbbbbb <- DMA table entry
                    // x and y should be start and end VROM addresses of each file
                    var dmaStartingAddr = ReadWriteUtils.Arr_ReadU32(dmaData, 16 * i);
                    if (dmaStartingAddr == actorStartVROM)
                    {
                        return i;
                    }
                }

                throw new Exception($"GetFIDFromVROM: could not find the file at VROM start addr:\n" +
                                    $"    [0x{actorStartVROM.ToString("X")}]");
                //return -1;
            }

            var size = endID - startID;
            if (size < 15) // if dma range < 15 (12.1 at 8 layers deep): search the rest of the way with linear
            {
                return Linear(startID);
            }
            else // else: recursive down to smaller size
            {
                int middle = (int)(size / 2.0) + startID;
                var middleAddr = ReadWriteUtils.Arr_ReadU32(dmaData, 16 * middle);
                if (actorStartVROM >= middleAddr)
                {
                    return GetFIDFromVROM(actorStartVROM, middle, endID);
                }
                else
                {
                    return GetFIDFromVROM(actorStartVROM, startID, middle - 1);
                }
            }
        }

        public static void CheckCompressed(int fileIndex, List<MMFile> mmFileList = null)
        {
            if (mmFileList == null)
            {
                mmFileList = RomData.MMFileList;
            }
            var file = mmFileList[fileIndex];
            if (file.Data == null || file.Data.Length == 0)
            {
                return;
            }
            if (file.IsCompressed && !file.WasEdited)
            {
                file.Data = Yaz.Decode(file.Data);
                file.WasEdited = true;
            }
        }

        public static List<byte[]> GetFilesFromArchive(int fileIndex)
        {
            CheckCompressed(fileIndex);
            var data = RomData.MMFileList[fileIndex].Data;
            var headerLength = ReadWriteUtils.Arr_ReadS32(data, 0);
            var pointer = headerLength;
            var files = new List<byte[]>();
            for (var i = 4; i < headerLength; i += 4)
            {
                var nextFileOffset = headerLength + ReadWriteUtils.Arr_ReadS32(data, i);
                var fileLength = nextFileOffset - pointer;
                var dest = new byte[fileLength];
                ReadWriteUtils.Arr_Insert(data, pointer, fileLength, dest, 0);
                pointer += fileLength;
                var decompressed = Yaz.Decode(dest);
                files.Add(decompressed);
            }
            return files;
        }

        public static int GetFileIndexForWriting(int rAddr)
        {
            int index = AddrToFile(rAddr);
            CheckCompressed(index);
            return index;
        }

        public static int ByteswapROM(string filename)
        {
            using (BinaryReader ROM = new BinaryReader(File.OpenRead(filename)))
            {
                if (ROM.BaseStream.Length % 4 != 0)
                {
                    return -1;
                }

                byte[] buffer = new byte[4];
                ROM.Read(buffer, 0, 4);
                // very hacky
                ROM.BaseStream.Seek(0, 0);
                if (buffer[0] == 0x80)
                {
                    return 1;
                }
                else if (buffer[1] == 0x80)
                {
                    using (BinaryWriter newROM = new BinaryWriter(File.Open(filename + ".z64", FileMode.Create)))
                    {
                        while (ROM.BaseStream.Position < ROM.BaseStream.Length)
                        {
                            newROM.Write(ReadWriteUtils.Byteswap16(ReadWriteUtils.ReadU16(ROM)));
                        }
                    }
                    return 0;
                }
                else if (buffer[3] == 0x80)
                {
                    using (BinaryWriter newROM = new BinaryWriter(File.Open(filename + ".z64", FileMode.Create)))
                    {
                        while (ROM.BaseStream.Position < ROM.BaseStream.Length)
                        {
                            newROM.Write(ReadWriteUtils.Byteswap32(ReadWriteUtils.ReadU32(ROM)));
                        }
                    }
                    return 0;
                }
            }
            return -1;
        }

        private static void UpdateDMAFileTable(byte[] ROM)
        {
            /// the DMA filetable stores the decompressed and compressed file start/end for every file
            /// AAAAAAAA BBBBBBBB CCCCCCCC DDDDDDDD size = 0x10
            /// where A and B are the start of VROM, where the files are located in decompressed space in-game and decompressed rom
            /// C and D are start/end of where the compressed version of the file exists in the compressed rom

            for (int i = 0; i < RomData.MMFileList.Count; i++)
            {
                int offset = FILE_TABLE + (i * 16);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 0x0, (uint)RomData.MMFileList[i].Addr);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 0x4, (uint)RomData.MMFileList[i].End);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 0x8, (uint)RomData.MMFileList[i].Cmp_Addr);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 0xC, (uint)RomData.MMFileList[i].Cmp_End);
            }
        }

        public static void WriteROM(string fileName, byte[] ROM)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(ROM, 0, ROM.Length);
            }
        }

        public static void CompressMMFiles()
        {
            /// Re-Compressing the files back into a compressed rom is the most expensive job during seed creation.
            /// To speed up, we compress files in parallel with a sorted list to reduce idle threads at the end.

            var startTime = DateTime.Now;

            // sorting the list with .Where() => OrderByDescending().ToList only takes (~ 0.400 miliseconds) on Isghj's computer
            //  but the time required to compress mostly scales with size:
            //  by sorting biggest files first, we are less likely to be stuck waiting for one big file at the end
            var sortedCompressibleFiles = RomData.MMFileList
                .Where(file => file.IsCompressed && file.WasEdited)
                .OrderByDescending(file => file.Data.Length)
                .ToList();

            // Debug.WriteLine($" sort the list with Sort() : [{(DateTime.Now).Subtract(startTime).TotalMilliseconds} (ms)]");

            // lower priority so that the rando can't lock a badly scheduled CPU by using 100%
            var previousThreadPriority = Thread.CurrentThread.Priority;
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            // yaz0 encode all of the files for the rom
            Parallel.ForEach(sortedCompressibleFiles.AsParallel().AsOrdered(), file =>
            {
                //var yazTime = DateTime.Now;
                file.Data = Yaz.EncodeAndCopy(file.Data);
                file.WasEdited = false;
                //Debug.WriteLine($" size: [{file.Data.Length}] time to complete compression : [{(DateTime.Now).Subtract(yazTime).TotalMilliseconds} (ms)]");
            });
            // this thread is borrowed, we don't want it to always be the lowest priority, return to previous state
            Thread.CurrentThread.Priority = previousThreadPriority;

            // uncompressed files need to have their compressed rom end address zero'd, this tells the game its not compressed
            foreach(var uncompressedFile in RomData.MMFileList.Where(file => !file.IsCompressed))
            {
                uncompressedFile.Cmp_End = 0x0;
            }

            Debug.WriteLine($" compress all files time : [{(DateTime.Now).Subtract(startTime).TotalMilliseconds} (ms)]");
        }

        private static void SetFilesToRemainDecompressed(OutputSettings settings)
        {
            /// Now that files can remain uncompressed and be expected to work, lets leave some commonly accessed files de-compressed so they don't cost as much to load

            if (settings.OutputVC)
            {
                return; // this does not work with wiivc right now
            }


            var listOfFiles = new List<int>()
            {
                //scenes
                //GameObjects.Scene.TerminaField.FileID(),
                //GameObjects.Scene.TerminaField.FileID() + 1, // room 0
                //GameObjects.Scene.SouthClockTown.FileID(),
                //GameObjects.Scene.SouthClockTown.FileID() + 1, // room 0
                //GameObjects.Scene.WestClockTown.FileID(),
                //GameObjects.Scene.WestClockTown.FileID() + 1, // room 0
                //GameObjects.Scene.EastClockTown.FileID(),
                //GameObjects.Scene.EastClockTown.FileID() + 1, // room 0
                //GameObjects.Scene.NorthClockTown.FileID(),
                //GameObjects.Scene.NorthClockTown.FileID() + 1, // room 0
                //GameObjects.Scene.Grottos.FileID(),
                //GameObjects.Scene.Grottos.FileID() + 4, // room 4 : regular chest grotto

                // THESE TWO are the highest priority, huge imporovement 
                38, // player overlay
                37, // pause menu

                // actor overlays
                GameObjects.Actor.Fairy.FileListIndex(),
                GameObjects.Actor.Arrow.FileListIndex(),
                GameObjects.Actor.BombAndKeg.FileListIndex(),
                //GameObjects.Actor.TallGrass.FileListIndex(),
                GameObjects.Actor.En_Clear_Tag.FileListIndex(), // bomb effects and such
                GameObjects.Actor.Arms_Hook.FileListIndex(), // hookshot tip
                GameObjects.Actor.ZoraFinBoomerang.FileListIndex(),

                // objects
                649, // gameplay_keep
                650, // field_keep
                651, // dangeon_keep
                654, // object_child_link (regular link)
                655, // goron 
                656, // zora
                657, // nuts

            };

            foreach (var fileId in listOfFiles)
            {
                var file = RomData.MMFileList[fileId];
                RomUtils.CheckCompressed(fileId); // if they werent preiviously modified they might still be compressed, decompress now
                //Debug.Assert(file.IsCompressed);

                file.IsCompressed = false;
            }
        }

        public static byte[] BuildROM(OutputSettings settings)
        {
            SetFilesToRemainDecompressed(settings);

            CompressMMFiles();

            byte[] ROM = new byte[0x2000000];

            int ROMAddr = 0;
            // write all files to rom
            for (int i = 0; i < RomData.MMFileList.Count; i++)
            {
                if (RomData.MMFileList[i].Cmp_Addr == -1) // empty file slot, ignore
                {
                    continue;
                }
                var file = RomData.MMFileList[i];

                file.Cmp_Addr = ROMAddr;

                int fileLength = file.Data.Length;
                if (file.IsCompressed)
                {
                    file.Cmp_End = ROMAddr + fileLength;
                }
                if (ROMAddr + fileLength > ROM.Length) // rom too small
                {
                    // assuming the largest file isn't the last one, we still want some extra space for further files
                    //  padding will reduce the requirements for further resizes
                    int expansionIncrementSize = 0x40000; // 1mb might be too large, not sure if there is a hardware compatiblity issue here
                    int expansionLength = (((ROMAddr + fileLength - ROM.Length) / expansionIncrementSize) + 1) * expansionIncrementSize;
                    byte[] newROM = new byte[ROM.Length + expansionLength];
                    Buffer.BlockCopy(ROM, 0, newROM, 0, ROM.Length);
                    Buffer.BlockCopy(new byte[expansionLength], 0, newROM, ROM.Length, expansionLength);
                    ROM = newROM;
                    Debug.WriteLine("*** Expanding rom to size 0x" + ROM.Length.ToString("X2") + "***");
                }

                ReadWriteUtils.Arr_Insert(RomData.MMFileList[i].Data, 0, fileLength, ROM, ROMAddr);
                ROMAddr += fileLength;
            }

            // should this be moved up now that I split up the file updated values?
            SequenceUtils.UpdateBankInstrumentPointers(ROM);

            UpdateDMAFileTable(ROM);
            SignROM(ROM);
            FixCRC(ROM);

            return ROM;
        }

        private static void SignROM(byte[] ROM)
        {
            var values = new List<string>
            {
                "MajoraRando",
                DateTime.UtcNow.ToString("yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                "\x01\x02" // versionCode
            };
            var signature = string.Join('\x00', values);
            for (var i = 0; i < signature.Length && i < 0x30; i++)
            {
                ROM[SIGNATURE_ADDRESS + i] = (byte)signature[i];
            }
        }

        private static void FixCRC(byte[] ROM)
        {
            // reference: http://n64dev.org/n64crc.html
            uint[] CRC = new uint[2];
            uint seed = 0xDF26F436;
            uint t1, t2, t3, t4, t5, t6, r, d;
            int i = 0x1000;
            t1 = t2 = t3 = t4 = t5 = t6 = seed;
            while (i < 0x101000)
            {
                d = ReadWriteUtils.Arr_ReadU32(ROM, i);
                if ((t6 + d) < t6) { t4++; }
                t6 += d;
                t3 ^= d;
                r = (d << (byte)(d & 0x1F)) | (d >> (byte)(32 - (d & 0x1F)));
                t5 += r;
                if (t2 < d)
                {
                    t2 ^= (t6 ^ d);
                }
                else
                {
                    t2 ^= r;
                }
                t1 += (ReadWriteUtils.Arr_ReadU32(ROM, 0x750 + (i & 0xFF)) ^ d);
                i += 4;
            }
            CRC[0] = t6 ^ t4 ^ t3;
            CRC[1] = t5 ^ t2 ^ t1;
            ReadWriteUtils.Arr_WriteU32(ROM, 16, CRC[0]);
            ReadWriteUtils.Arr_WriteU32(ROM, 20, CRC[1]);
        }

        private static void ExtractAll(BinaryReader ROM)
        {
            for (int i = 0; i < RomData.MMFileList.Count; i++)
            {
                if (RomData.MMFileList[i].Cmp_Addr == -1) { continue; }
                ROM.BaseStream.Seek(RomData.MMFileList[i].Cmp_Addr, 0);
                if (RomData.MMFileList[i].IsCompressed)
                {
                    byte[] CmpFile = new byte[RomData.MMFileList[i].Cmp_End - RomData.MMFileList[i].Cmp_Addr];
                    ROM.Read(CmpFile, 0, CmpFile.Length);
                    RomData.MMFileList[i].Data = CmpFile;
                }
                else
                {
                    var buffer = new byte[RomData.MMFileList[i].End - RomData.MMFileList[i].Addr];
                    ROM.Read(buffer, 0, buffer.Length);
                    RomData.MMFileList[i].Data = buffer;
                }
            }
        }

        public static void ReadFileTable(BinaryReader ROM)
        {
            RomData.MMFileList = new List<MMFile>();
            ROM.BaseStream.Seek(FILE_TABLE, SeekOrigin.Begin);
            while (true)
            {
                MMFile Current_File = new MMFile
                {
                    Addr = ReadWriteUtils.ReadS32(ROM),
                    End = ReadWriteUtils.ReadS32(ROM),
                    Cmp_Addr = ReadWriteUtils.ReadS32(ROM),
                    Cmp_End = ReadWriteUtils.ReadS32(ROM)
                };
                Current_File.IsCompressed = Current_File.Cmp_End != 0;
                if (Current_File.Addr == Current_File.End)
                {
                    break;
                }
                RomData.MMFileList.Add(Current_File);
            }
            ExtractAll(ROM);
        }

        public static bool CheckOldCRC(BinaryReader ROM)
        {
            ROM.BaseStream.Seek(16, 0);
            uint CRC1 = ReadWriteUtils.ReadU32(ROM);
            uint CRC2 = ReadWriteUtils.ReadU32(ROM);
            return (CRC1 == 0x5354631C) && (CRC2 == 0x03A2DEF0);
        }

        public static bool ValidateROM(string FileName)
        {
            bool res = false;
            using (BinaryReader ROM = new BinaryReader(File.OpenRead(FileName)))
            {
                if (ROM.BaseStream.Length == 0x2000000)
                {
                    res = CheckOldCRC(ROM);
                }
            }
            return res;
        }

        /// <summary>
        /// Get the index of the tail-most <see cref="MMFile"/> which does not use a static virtual address.
        /// </summary>
        /// <returns>Index</returns>
        public static int GetTailFileIndex()
        {
            var index = RomData.MMFileList.FindLastIndex(file => !file.IsStatic);
            var result = index >= 0 ? (int?)index : (int?)null;
            return result.Value;
        }

        /// <summary>
        /// Append a <see cref="MMFile"/> without a static virtual address to the end of the list.
        /// </summary>
        /// <param name="data">File data</param>
        /// <param name="isCompressed">Is file compressed</param>
        /// <returns>File index</returns>
        public static int AppendFile(byte[] data, bool isCompressed = false)
        {
            var index = GetTailFileIndex();
            var tail = RomData.MMFileList[index];
            return AppendFile(tail.End, data, isCompressed);
        }

        /// <summary>
        /// Append a <see cref="MMFile"/> to the list.
        /// </summary>
        /// <param name="addr">File address</param>
        /// <param name="data">File data</param>
        /// <param name="isCompressed">Is file compressed</param>
        /// <param name="isStatic">Is file address static</param>
        /// <returns>File index</returns>
        public static int AppendFile(int addr, byte[] data, bool isCompressed = false, bool isStatic = false)
        {
            var file = new MMFile
            {
                Addr = addr,
                End = addr + data.Length,
                IsCompressed = isCompressed,
                Data = data,
                IsStatic = isStatic,
            };

            return AppendFile(file);
        }

        /// <summary>
        /// Append a <see cref="MMFile"/> to the list.
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>File index</returns>
        public static int AppendFile(MMFile file)
        {
            if (!file.IsStatic)
            {
                // Insert before static files
                var index = GetTailFileIndex() + 1;
                RomData.MMFileList.Insert(index, file);
                return index;
            }
            else
            {
                RomData.MMFileList.Add(file);
                return RomData.MMFileList.Count - 1;
            }
        }
    }

}
