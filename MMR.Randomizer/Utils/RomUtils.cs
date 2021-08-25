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
using MMR.Randomizer.Extensions;
using System.Numerics;

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
            string settingstring = $"{setting} + Isghj's Enemizer Test 22.4 + Cogsy's Lunar Contingency\x00";
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

        public static void CheckCompressed(int fileIndex, List<MMFile> mmFileList = null)
        {
            if (mmFileList == null)
            {
                mmFileList = RomData.MMFileList;
            }
            var file = mmFileList[fileIndex];
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

        private static void UpdateVirtualFileAddresses()
        {
            /// shift the decompressed virtual addresses to the requirements of the new files

            int previousLastVROM;
            int lastDecompressedAddr = 0;
            //for (int i = 0; i < RomData.MMFileList.Count; i++)
            for (int i = 39; i < RomData.MMFileList.Count; i++)
            {
                var file = RomData.MMFileList[i];
                if (file.Cmp_Addr == -1) // file slot is empty, ignore
                {
                    continue;
                }

                //Debug.WriteLine($"  file [{i}] old [{file.Addr.ToString("X")}] [{file.End.ToString("X")}]");

                // new attempt at getting rom shifting of oversized overlays working
                // since all files are shifted after the first shift, would it make sense to just always update all files?
                if (file.Addr < lastDecompressedAddr)
                {
                    // Debug.WriteLine($"FileID [{i}] need moving from {file.Addr.ToString("X")} to {lastDecompressedAddr.ToString("X")}");
                    var diff = lastDecompressedAddr - file.Addr;
                    file.Addr = lastDecompressedAddr;
                    // too late to get the size of decompressed file unless we keep it as a value, but we know how far off we should be
                    file.End += diff;
                }

                lastDecompressedAddr = file.End;
                //Debug.WriteLine($"    new          [{file.Addr.ToString("X")}] [{(file.End).ToString("X")}]");

            }
        }

        public static void UpdateOverlayTable(int actorOvlTblFID, int actorOvlTblOffset)
        {
            /// if overlays have grown, we need to modify their overlay table to use the right values for the new files
            /// every time you move an overlay you need to relocate the vram addresses, so instead of shifting all of them
            ///  we just move the new larger files to the end and leave a hole behind for now

            uint theEndOfTakenVRAM = 0x80C27000; // 0x80C260A0 <- actual

            // the overlay table exists inside of another file, we need the offset to the table
            //int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;

            // generate a list of actors sorted by fid
            var actorList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList();
            actorList.Remove(GameObjects.Actor.Empty);
            actorList.Remove(GameObjects.Actor.Player);
            actorList.RemoveAll(u => u.FileListIndex() < 38);
            var fidSortedActors = actorList.OrderBy(x => x.FileListIndex()).ToList();

            // I don't know HOW vram is started, let's assume for now nothing before overlays is shifted,
            // we need to start where the old vram ended
            // read the vram start entry (0x8) from index 0x1, player's values are empty for some reason
            // from that point on, keep the last acceptable spot
            uint previousLastVRAMEnd = theEndOfTakenVRAM;
            int shift = 0;

            foreach (var entry in fidSortedActors)
            {
                var actorID = (int) entry;
                int entryLoc = actorOvlTblOffset + (actorID * 32); // overlay table is sorted by actorID

                uint oldVROMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x0);
                uint oldVROMEnd   = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x4);

                if (oldVROMStart == 0) // empty file ignore
                {
                    continue;
                }

                var fileID = entry.FileListIndex();
                var file = RomData.MMFileList[fileID];
                uint oldVRAMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x8);
                uint oldVRAMEnd   = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0xC);
                uint oldInitAddr  = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x14);
                uint oldVROMSize = oldVROMEnd - oldVROMStart;
                uint oldVRAMSize = oldVRAMEnd - oldVRAMStart;

                // if it was edited, its not compressed, get new filesize, else diff old address values
                var uncompresedVROMSize = (file.WasEdited) ? (file.Data.Length) : (file.End - file.Addr);
                int newVROMDiff = uncompresedVROMSize - (int) oldVROMSize;

                // always update VROM, as it always moves in DMA table now
                ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0, (uint) file.Addr);
                ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x4, (uint) file.End);

                // files have grown in size, these are the files we need to move
                if (newVROMDiff != 0)
                {
                    var newActorMeta = Enemies.InjectedActors.Find(u => u.fileID == fileID);

                    if (newActorMeta == null)
                    {
                        throw new Exception("UpdateOverlayTable: Meta missing for injected actor");
                    }

                    // TODO check if we can place it in an old hole left behind by a previously moved actor
                    var newVRAMStart = previousLastVRAMEnd;

                    // we need the difference between the old VRAM and new VRAM starting locations to re-align our vram
                    uint newVRAMOffset = newVRAMStart - oldVRAMStart;

                    // we know where in the overlay pointers exist that need to be updated for VROM->VRAM
                    // .reloc stores this info for us as a table of words that contain enough info to help us update
                    // the very last byte in the overlay is (from end) offset
                    //   of the table that declares size of text/data/rodata/bss
                    // following those is a count of the reloc entries, followed by the actual entries
                    var relocSize = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
                    // the table pointer at the end is an offset from the end, we need to swap it
                    int tableOffset = (int)(file.Data.Length - relocSize);
                    int relocEntryCountLocation = (int)(tableOffset + (4 * 4));
                    uint relocEntryCount = ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryCountLocation);

                    // each entry in reloc is one nibble of shifted section, one nible of type, and 3 bytes of address
                    // where address is an offset of the section, section starts at 1 for some reason, and there are only three types in Z64

                    // the section table only contains section sizes, we need to walk it to know the offsets
                    var sectionOffsets = new int[4];
                    sectionOffsets[0] = 0; // text (always at the start for our overlay system)
                    sectionOffsets[1] = sectionOffsets[0] + (int) ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset); // data
                    sectionOffsets[2] = sectionOffsets[1] + (int) ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 4); // rodata
                    sectionOffsets[3] = sectionOffsets[2] + (int) ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 8); // bss

                    // from what I can tell, if you sum the section sizes and then the reloc total (final byte)
                    // you should get the total vram size. Works for boyo so far ...
                    var newVramSize  = sectionOffsets[3] + relocSize;
                    var newVRAMEnd = (uint)(newVRAMStart + newVramSize);

                    var relocEntryLoc = relocEntryCountLocation + 4; // first entry immediately after count
                    var relocEntryEndLoc = relocEntryLoc + (relocEntryCount * 4);
                    while (relocEntryLoc < relocEntryEndLoc)
                    {
                        // for some reason text starts at 1, and so we cant reach bss unless we wrap around to 00?
                        var section = ((file.Data[relocEntryLoc] & 0xC0) >> 6) - 1;
                        var sectionOffset = sectionOffsets[section];

                        var commandType     = (file.Data[relocEntryLoc] & 0xF);
                        var commandTypeNext = (file.Data[relocEntryLoc + 4] & 0xF);

                        // LUI/ADDIU combo
                        if (commandType == 0x5 && commandTypeNext == 0x6)
                        {
                            int luiLoc   = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                            int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;
                            // combine the halves from asm back into one pointer
                            uint pointer = 0;
                            pointer |= ((uint)ReadWriteUtils.Arr_ReadU16(file.Data, luiLoc   + 2) << 16);
                            pointer |= ((uint)ReadWriteUtils.Arr_ReadU16(file.Data, addiuLoc + 2));
                            // offset the pointer to our new VRAM
                            pointer += newVRAMOffset;
                            // separate the pointer again into halves and put back
                            int LUIAdd = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0; // if the lower half is too big we have to add one to LUI
                            ReadWriteUtils.Arr_WriteU16(file.Data, luiLoc   + 2, (ushort)(((pointer & 0xFFFF0000) >> 16) + LUIAdd));
                            ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, (ushort)(pointer & 0xFFFF));

                            relocEntryLoc += 8;
                        }
                        // JAL function calls
                        else if (commandType == 0x4)
                        {
                            int jalLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                            uint jal = ReadWriteUtils.Arr_ReadU32(file.Data, jalLoc) & 0x00FFFFFF;
                            uint shiftedJal = jal << 2;
                            shiftedJal += newVRAMOffset;
                            shiftedJal = shiftedJal >> 2;

                            // might be another kind that isn't 0C, so far so good
                            ReadWriteUtils.Arr_WriteU32(file.Data, jalLoc, 0x0C000000 | shiftedJal);

                            relocEntryLoc += 4;
                        }
                        // Hard pointer (init vars actor funcs like init for instance)
                        else if (commandType == 0x2)
                        {
                            // issue: this is also used for jump table pointers but those work different I guess? 

                            int ptrLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                            uint ptrValue = ReadWriteUtils.Arr_ReadU32(file.Data, ptrLoc);
                            ptrValue += newVRAMOffset;
                            ReadWriteUtils.Arr_WriteU32(file.Data, ptrLoc, ptrValue);

                            relocEntryLoc += 4;
                        }
                        // unknown command? supposidly Z64 only uses these three although it could support more
                        else
                        {
                            throw new Exception($"UpdateOverlayTable: unknown reloc entry value:\n" +
                                $" {ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc).ToString("X")}");
                        }
                    }

                    uint newInitVarAddr = (uint)(newVRAMStart + newActorMeta.initVarsLocation);

                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x8, newVRAMStart);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0xC, newVRAMEnd);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x14, newInitVarAddr);

                    if (newVROMDiff > 0)
                    {
                        shift += newVROMDiff;
                        Debug.WriteLine($" + old[{oldVROMSize.ToString("X")}]:  new[{file.Data.Length.ToString("X")}] has shifted: {newVROMDiff.ToString("X")}");
                    }

                    Debug.WriteLine($" Actor aid[{actorID.ToString("X")}]:  fid[{fileID}]");
                    Debug.WriteLine($"  file [{fileID}] old [{oldVRAMStart.ToString("X")}] [{oldVRAMEnd.ToString("X")}]");
                    Debug.WriteLine($"    new          [{newVRAMStart.ToString("X")}] [{(newVRAMEnd).ToString("X")}]");
                    Debug.WriteLine($"    INIT         [{oldInitAddr.ToString("X")}] [{(newInitVarAddr).ToString("X")}]");

                    Debug.WriteLine($"    shift [{shift}] diff [{newVROMDiff}] init-diff []");

                    previousLastVRAMEnd = newVRAMEnd;
                }
            }
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
                ReadWriteUtils.Arr_WriteU32(ROM, offset, (uint)RomData.MMFileList[i].Addr);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 4, (uint)RomData.MMFileList[i].End);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 8, (uint)RomData.MMFileList[i].Cmp_Addr);
                ReadWriteUtils.Arr_WriteU32(ROM, offset + 12, (uint)RomData.MMFileList[i].Cmp_End);
            }
        }

        public static void WriteROM(string fileName, byte[] ROM)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(ROM, 0, ROM.Length);
            }
        }

        public static byte[] BuildROM()
        {
            // get the last data we need to find the overlay table

            // our code that looks up the location in old-rom space of an address looks at file locations
            //  the following function (UpdateVirtualFileAddresses) changes these so we need to get these first
            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);

            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;

            //boyo crashes if you enter a scene without updating VROM OR overlay, but the rest of the game works
            // if you update VROM, crash after n64 logo

            // all of our files need their addresses shifted
            UpdateVirtualFileAddresses();
            // we need to update overlay table if actors changed size
            UpdateOverlayTable(actorOvlTblFID, actorOvlTblOffset);

            // lower priority so that the rando can't lock a badly scheduled CPU by using 100%
            var previousThreadPriority = Thread.CurrentThread.Priority;
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            // yaz0 encode all of the files for the rom
            Parallel.ForEach(RomData.MMFileList, file =>
            {
                if (file.IsCompressed && file.WasEdited){
                    file.Data = Yaz.EncodeAndCopy(file.Data);
                }
            });
            // this thread is borrowed, we don't want it to always be the lowest priority, return to previous state
            Thread.CurrentThread.Priority = previousThreadPriority;

            //UpdateCompressedDMAAddresses(); // wait we dont need to separate anymore

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
            string VersionString = "MajoraRando"; // ??????
            string DateString = DateTime.UtcNow.ToString("yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            for (int i = 0; i < VersionString.Length; i++)
            {
                ROM[SIGNATURE_ADDRESS + i] = (byte)VersionString[i];
            }
            for (int i = 0; i < DateString.Length; i++)
            {
                ROM[SIGNATURE_ADDRESS + i + 12] = (byte)DateString[i];
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
