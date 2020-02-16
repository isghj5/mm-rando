﻿using MMRando.Constants;
using MMRando.Models;
using MMRando.Models.Rom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;

namespace MMRando.Utils
{
    public class SequenceUtils
    {

        public static void ReadSequenceInfo()
        {
            RomData.SequenceList = new List<SequenceInfo>();
            RomData.TargetSequences = new List<SequenceInfo>();

            // if file exists, we read the file instead of the resource
            string[] lines = null;
            if (File.Exists(Values.MusicDirectory + "SEQS.txt"))
            {
                Debug.WriteLine("We found a user SEQS.txt file that we can use");
                var list = new List<string>();
                string line;
                using (StreamReader sr = new StreamReader(Values.MusicDirectory + "SEQS.txt")){
                    while ((line = sr.ReadLine()) != null){
                        list.Add(line);
                    }
                }
                lines = list.ToArray();
            }else // load SEQS.txt from source memory
                lines = Properties.Resources.SEQS.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);


            int i = 0;
            while (i < lines.Length)
            {
                string sourceName = lines[i];
                List<int> sourceType = new List<int>();
                foreach (String part in lines[i + 1].Split(','))
                    sourceType.Add(Convert.ToInt32(part, 16));
                var sourceInstrument = Convert.ToInt32(lines[i + 2], 16);

                SequenceInfo sourceSequence = new SequenceInfo
                {
                    Name = sourceName,
                    Type = sourceType,
                    Instrument = sourceInstrument
                };

                SequenceInfo targetSequence = new SequenceInfo
                {
                    Name = sourceName,
                    Type = sourceType,
                    Instrument = sourceInstrument
                };

                if (sourceSequence.Name.StartsWith("mm-"))
                {
                    targetSequence.Replaces = Convert.ToInt32(lines[i + 3], 16);
                    sourceSequence.MM_seq = Convert.ToInt32(lines[i + 3], 16);
                    RomData.TargetSequences.Add(targetSequence);
                    if (lines[i+4] == "no-recycle"){
                        Debug.WriteLine("Player does not want to reuse song: " + sourceSequence.Name);
                        sourceSequence.Name = "drop";
                        i += 1;
                    }
                    i += 4;
                }
                else
                {
                    if (sourceSequence.Name == "mmr-f-sot")
                    {
                        sourceSequence.Replaces = 0x33;
                    }

                    i += 3;

                    // if file doesn't exist, was removed by user, ignore
                    if (File.Exists(Values.MusicDirectory + sourceName) == false)
                    {
                        continue;
                    }

                };

                if (sourceSequence.MM_seq != 0x18 && sourceSequence.Name != "drop")
                {
                    RomData.SequenceList.Add(sourceSequence);
                };
            }; // end while (i < lines.Length)

            ScanZSEQUENCE(Values.MusicDirectory);                    // scan for base zseq in music folder
            ScanForMMRS(Values.MusicDirectory); // scan for base mmrs in music folder

        }

        public static void ScanZSEQUENCE(string directory) // TODO make this folder identifiable, add directory and list of banks from scanned directory to this
        {
            // check if files were added by user to directory
            // we're not going to check for non-zseq here until I find an easy way to do that
            //  Just going to trust users aren't stupid enough to think renaming a mp3 to zseq will work
            // format: FILENAME_InstrumentSet_Categories-separated-by-commas.zseq
            //  where the filename, instrumentset, and categories are separated by single underscore
            foreach (String filePath in Directory.GetFiles(directory, "*.zseq"))
            {
                String filename = Path.GetFileName(filePath);

                // test if file has enough delimiters to separate data into name_bank_formats
                String[] pieces = filename.Split('_');
                if (pieces.Length != 3)
                {
                    continue;
                }

                byte[] input_seq_data = null;
                using (BinaryReader seq_reader = new BinaryReader(File.Open(directory + filename, FileMode.Open)))
                {
                    input_seq_data = new byte[((int)seq_reader.BaseStream.Length)];
                    seq_reader.Read(input_seq_data, 0, (int)seq_reader.BaseStream.Length);
                }


                SequenceBinaryData sequence_data = null;
                InstrumentSetInfo instrumnet_info = null;
                if (pieces[1].Contains("x")) //temporary, we can remove this now that we have MMRS, but maybe don't remove just yet
                {
                    // load the custom bank for this file
                    byte[] input_bank_data = null;
                    String bank_name = filename.Substring(0, filename.LastIndexOf(".zseq")) + ".zbank";
                    using (BinaryReader bank_reader = new BinaryReader(File.Open(directory + bank_name, FileMode.Open)))
                    {
                        input_bank_data = new byte[((int)bank_reader.BaseStream.Length)];
                        bank_reader.Read(input_bank_data, 0, (int)bank_reader.BaseStream.Length);
                    }

                    byte[] meta_data = new byte[8];
                    using (BinaryReader meta_reader = new BinaryReader(File.Open(directory + bank_name.Substring(0, bank_name.LastIndexOf(".zbank")) + ".bankmeta", FileMode.Open)))
                        meta_reader.Read(meta_data, 0, meta_data.Length);

                    pieces[1] = pieces[1].Replace("x", "");

                    instrumnet_info = new InstrumentSetInfo()
                    {
                        BankBinary = input_bank_data,
                        BankMetaData = meta_data,
                        BankSlot = Convert.ToInt32(pieces[1], 16),
                        Modified = true
                    };
                }

                sequence_data = new SequenceBinaryData
                {
                    SequenceBinary = input_seq_data,
                    InstrumentSet = instrumnet_info
                };


                var sourceName = filename;
                var sourceTypeString = pieces[2].Substring(0, pieces[2].Length - 5);
                var sourceInstrument = Convert.ToInt32(pieces[1], 16);
                //var sourceType = Array.ConvertAll(sourceTypeString.Split('-'), int.Parse).ToList();
                List<int> sourceType = new List<int>();
                foreach (String part in sourceTypeString.Split('-'))
                    sourceType.Add(Convert.ToInt32(part, 16));

                SequenceInfo sourceSequence = new SequenceInfo
                {
                    Name = sourceName,
                    Type = sourceType,
                    Instrument = sourceInstrument,
                    SequenceBinaryList = new List<SequenceBinaryData>() { sequence_data }
                };


                RomData.SequenceList.Add(sourceSequence);
            }

        }

        public static void ScanForMMRS(string directory)
        {
            // check if user has added mmrs packed sequence files to the music folder
            //  mmrs is just a zip that has all the small files:
            //  the sequence itself, the categories, and the instrument set value
            //    if the song requires a custom audiobank, the bank and bank meta data are also here
            //  the user should be able to pack the archive with multiple sequences and multiple banks to match, 
            //   where the redundancy increases likley hood of a song being able to be placed in a free audiobank slot

            foreach (String filePath in Directory.GetFiles(directory, "*.mmrs"))
            {
                try
                {
                    using (ZipArchive zip = ZipFile.Open(filePath, ZipArchiveMode.Read))
                    {
                        List<string> sequences = new List<string>();
                        SequenceInfo new_song = new SequenceInfo(); ;
                        //String[] split_filepath = filePath.Split('\\');
                        //new_song.Name = split_filepath[split_filepath.Length - 1];
                        new_song.Name = Path.GetFileName(filePath);

                        //read categories file
                        ZipArchiveEntry categories_entry = zip.GetEntry("categories.txt");
                        if (categories_entry != null)
                        {
                            List<int> category_list = new List<int>();
                            String category_string = new StreamReader(categories_entry.Open(), Encoding.Default).ReadToEnd();
                            char delimter = ',';
                            if (category_string.Contains("-")) // someone will mess this up, its an easy thing to check for here tho
                                delimter = '-';
                            else if (category_string.Contains("\n"))
                                delimter = '\n';
                            foreach (String line in category_string.Split(delimter))
                                category_list.Add(Convert.ToInt32(line, 16));
                            new_song.Type = category_list;
                        }
                        else  // there should always be one, if not, print error and skip
                        {
                            Debug.WriteLine("ERROR: cannot find a categories file for " + new_song.Name);
                            continue;
                        }

                        foreach (ZipArchiveEntry zip_item in zip.Entries)
                        {
                            if (zip_item.Name.Contains("zseq"))
                            {
                                // read sequence binary file
                                byte[] seq_data = new byte[zip_item.Length];
                                zip_item.Open().Read(seq_data, 0, seq_data.Length);

                                SequenceBinaryData new_sequence_data = new SequenceBinaryData() { SequenceBinary = seq_data };

                                string file_name_extensionless = zip_item.Name.Substring(0, zip_item.Name.LastIndexOf(".zseq"));
                                new_song.Instrument = Convert.ToInt32(file_name_extensionless, 16); // assuming we're naming with inst.zseq and inst.bank


                                ZipArchiveEntry bank_entry = zip.GetEntry(file_name_extensionless + ".zbank");
                                if (bank_entry != null) // custom bank detected
                                {
                                    // read bank file
                                    byte[] bank_data = new byte[bank_entry.Length];
                                    bank_entry.Open().Read(bank_data, 0, bank_data.Length);

                                    // read bank meta data file
                                    ZipArchiveEntry meta_entry = zip.GetEntry(file_name_extensionless + ".bankmeta");
                                    byte[] meta_data = new byte[meta_entry.Length];
                                    meta_entry.Open().Read(meta_data, 0, meta_data.Length);

                                    new_sequence_data.InstrumentSet = new InstrumentSetInfo()
                                    {
                                        BankBinary = bank_data,
                                        BankSlot = new_song.Instrument,
                                        BankMetaData = meta_data,
                                        Modified = true
                                    };
                                }//if requires bank

                                // multiple seq possible, add depending on if first or not
                                if (new_song.SequenceBinaryList == null)
                                    new_song.SequenceBinaryList = new List<SequenceBinaryData> { new_sequence_data };
                                else
                                    new_song.SequenceBinaryList.Add(new_sequence_data);
                            } //is file zseq
                        } // foreach zip entry

                        if (new_song != null && new_song.SequenceBinaryList != null)
                        {
                            RomData.SequenceList.Add(new_song);
                        }

                    }// zip as file
                }// for each zip
                catch (Exception e) // log it, continue with other songs
                {
                    Debug.WriteLine("Error attempting to read archive: " + filePath + " -- " + e);
                }
            }
        }

        // gets passed RomData.SequenceList in Builder.cs::WriteAudioSeq
        public static void RebuildAudioSeq(List<SequenceInfo> SequenceList)
        {
            List<MMSequence> OldSeq = new List<MMSequence>();
            int f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            int basea = RomData.MMFileList[f].Addr;

            for (int i = 0; i < 128; i++)
            {
                MMSequence entry = new MMSequence();
                if (i == 0x1E) // intro music when link gets ambushed
                {
                    entry.Addr = 2;
                    entry.Size = 0;
                    OldSeq.Add(entry);
                    continue;
                }

                int entryaddr = Addresses.SeqTable + (i * 16);
                entry.Addr = (int)ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, entryaddr - basea);
                entry.Size = (int)ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, (entryaddr - basea) + 4);
                if (entry.Size > 0)
                {
                    entry.Data = new byte[entry.Size];
                    Array.Copy(RomData.MMFileList[4].Data, entry.Addr, entry.Data, 0, entry.Size);
                }
                else
                {
                    int j = SequenceList.FindIndex(u => u.Replaces == i);
                    if (j != -1)
                    {
                        if ((entry.Addr > 0) && (entry.Addr < 128))
                        {
                            if (SequenceList[j].Replaces != 0x28) // 28 (fairy fountain)
                            {
                                SequenceList[j].Replaces = entry.Addr;
                            }
                            else
                            {
                                entry.Data = OldSeq[0x18].Data;
                                entry.Size = OldSeq[0x18].Size;
                            }
                        }
                    }
                }
                OldSeq.Add(entry);
            }

            List<MMSequence> NewSeq = new List<MMSequence>();
            int addr = 0;
            byte[] NewAudioSeq = new byte[0];
            for (int i = 0; i < 128; i++)
            {
                MMSequence newentry = new MMSequence();
                if (OldSeq[i].Size == 0)
                {
                    newentry.Addr = OldSeq[i].Addr;
                }
                else
                {
                    newentry.Addr = addr;
                }

                int p = RomData.PointerizedSequences.FindIndex(u => u.PreviousSlot == i);
                int j = SequenceList.FindIndex(u => u.Replaces == i);
                if (p != -1) // found song we want to pointerize
                {
                    Debug.WriteLine("Sequence slot " + i.ToString("X") + " *->  " + RomData.PointerizedSequences[p].Replaces.ToString("X"));
                    newentry.Addr = RomData.PointerizedSequences[p].Replaces;
                    newentry.Size = 0;
                    // isn't there like 8 bytes of zeros here? where does that go?
                }
                else if (j != -1) // new song to replace old slot found
                {
                    if (SequenceList[j].MM_seq != -1) // old mm song, just copy over
                    {
                        newentry.Size = OldSeq[SequenceList[j].MM_seq].Size;
                        newentry.Data = OldSeq[SequenceList[j].MM_seq].Data;
                    }
                    else if (SequenceList[j].SequenceBinaryList != null && SequenceList[j].SequenceBinaryList[0] != null)
                    {
                        if (SequenceList[j].SequenceBinaryList.Count == 0)
                            throw new Exception("Reached music write without a song to write");
                        if (SequenceList[j].SequenceBinaryList.Count > 1)
                            Debug.WriteLine("Warning: writing song with multiple sequences possible, selecting at random");
                        newentry.Size = SequenceList[j].SequenceBinaryList[0].SequenceBinary.Length;
                        newentry.Data = SequenceList[j].SequenceBinaryList[0].SequenceBinary;
                    }

                    else // non mm, load file and add
                    {
                        BinaryReader sequence = new BinaryReader(File.Open(SequenceList[j].Name, FileMode.Open));
                        byte[] data = new byte[(int)sequence.BaseStream.Length];
                        sequence.Read(data, 0, data.Length);
                        sequence.Close();

                        // if the sequence is not padded to 16 bytes, the DMA fails
                        //  music can stop from playing and on hardware it will just straight crash
                        if (data.Length % 0x10 != 0)
                            data = data.Concat(new byte[0x10 - (data.Length % 0x10)]).ToArray();

                        // I think this checks if the sequence type is correct for MM
                        //  because DB ripped sequences from SF64/SM64/MK64 without modifying them
                        if (data[1] != 0x20)
                        {
                            data[1] = 0x20;
                        }

                        newentry.Size = data.Length;
                        newentry.Data = data;
                    }
                }
                else // not found, song wasn't touched by rando, just transfer over
                {
                    newentry.Size = OldSeq[i].Size;
                    newentry.Data = OldSeq[i].Data;
                }

                NewSeq.Add(newentry);
                // TODO is there not a better way to write this?
                if (newentry.Data != null)
                {
                    NewAudioSeq = NewAudioSeq.Concat(newentry.Data).ToArray();
                }

                addr += newentry.Size;
            }

            // if (addr > (RomData.MMFileList[4].End - RomData.MMFileList[4].Addr))
            if ( true ) // TODO: figure out why leaving the audioseq in its orginal spot causes garbage audio
            {
                int index = RomUtils.AppendFile(NewAudioSeq);
                ResourceUtils.ApplyHack(Values.ModsDirectory + "reloc-audio");
                RelocateSeq(index);
                RomData.MMFileList[4].Data = new byte[0];
                RomData.MMFileList[4].Cmp_Addr = -1;
                RomData.MMFileList[4].Cmp_End = -1;
            }
            else
            {
                RomData.MMFileList[4].Data = NewAudioSeq;
            }

            //update pointer table
            f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            for (int i = 0; i < 128; i++)
            {
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, (Addresses.SeqTable + (i * 16)) - basea, (uint)NewSeq[i].Addr);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, 4 + (Addresses.SeqTable + (i * 16)) - basea, (uint)NewSeq[i].Size);
            }

            //update inst sets
            f = RomUtils.GetFileIndexForWriting(Addresses.InstSetMap);
            basea = RomData.MMFileList[f].Addr;
            for (int i = 0; i < 128; i++)
            {
                int paddr = (Addresses.InstSetMap - basea) + (i * 2) + 2;
                int j = -1;

                if (NewSeq[i].Size == 0)
                {
                    j = SequenceList.FindIndex(u => u.Replaces == NewSeq[i].Addr);
                }
                else
                {
                    j = SequenceList.FindIndex(u => u.Replaces == i);
                }

                if (j != -1)
                {
                    RomData.MMFileList[f].Data[paddr] = (byte)SequenceList[j].Instrument;
                }

            }
        }

        /// <summary>
        /// Patch instructions to use new sequence data file.
        /// </summary>
        /// <param name="f">File index</param>
        /// <remarks>
        /// In memory: 0x80190E5C
        /// Replaces:
        ///   lui     a1, 0x0004
        ///   addiu   a1, a1, 0x6AF0
        /// With:
        ///   lui     t0, 0x800A
        ///   lw      a1, offset (t0)
        /// Note: File table in memory starts at 0x8009F8B0.
        /// </remarks>
        private static void RelocateSeq(int f)
        {
            var fileTable = 0xF8B0;
            var offset = (fileTable + (f * 0x10) + 8) & 0xFFFF;
            ReadWriteUtils.WriteToROM(0x00C2739C, new byte[] { 0x3C, 0x08, 0x80, 0x0A, 0x8D, 0x05, (byte) (offset >> 8), (byte)(offset & 0xFF) });
        }

        public static bool ReassignSceneMusic(int scene_fid, byte previous_seq_index, byte sequence_index)
        {
            // the BGM for a scene is specified by a single byte in the scene headers

            // to modify the scene header, which is in the scene, we need the scene as a file
            //  we can get this from the Romdata.SceneList but this only gets populated on enemizer
            //  and we don't NEED to populate it since vanilla scenes are static, we can just hard code it here 
            //  at re-encode, we'll have fewer decoded files to re-encode too

            // scan the files for the header that contains scene music (0x15 first byte)
            // 15xx0000 0000yyzz where zz is the sequence pointer byte
            RomUtils.CheckCompressed(scene_fid);
            for (int b = 0; b < 0x10 * 100; b += 8)
            {
                if (RomData.MMFileList[scene_fid].Data[b] == 0x15
                    && RomData.MMFileList[scene_fid].Data[b + 0x7] == previous_seq_index)
                {
                    RomData.MMFileList[scene_fid].Data[b + 0x7] = sequence_index;
                    return true;
                }
            }
            return false;
        }

        public static void ReassignSkulltulaHousesMusic(byte replacement_slot = 0x75)
        {
            // changes the skulltulla house BGM to a separate slot so it plays a new music that isn't generic cave music (overused)
            //  replacement slot is the new song slot we want to use for just this new BGM

            int swamp_spider_house_fid = 1284; // taken from ultimate MM spreadsheet (US File list -> A column)
            ReassignSceneMusic(swamp_spider_house_fid, 0x3b, replacement_slot); // 0x3b is cave music

            int ocean_spider_house_fid = 1291; 
            ReassignSceneMusic(ocean_spider_house_fid, 0x3b, replacement_slot);

            SequenceInfo new_music_slot = new SequenceInfo
            {
                Name = "mm-spiderhouse-replacement",
                MM_seq = replacement_slot,
                Replaces = replacement_slot,
                Type = new List<int> { 2 }
            };

            RomData.TargetSequences.Add(new_music_slot);

        }

        public static void ReassignPinnacleRockMusic(byte replacement_slot = 0x1e)
        {
            // changes the pinacle rock music to use a different BGM than regular great bay music
            //  replacement slot is the new song slot we want to use for just this new BGM

            int pinacle_rock_fid = 1276; // taken from ultimate MM spreadsheet (US File list -> A column)
            bool success = ReassignSceneMusic(pinacle_rock_fid, 0x10, replacement_slot); // 0x10 is great bay

            if (!success)
            {
                Debug.WriteLine("Could not find the music byte at pinaccle rock to replace");
                return;
            }

            SequenceInfo new_music_slot = new SequenceInfo
            {
                Name = "mm-pinnacle-rock-replacement",
                MM_seq = replacement_slot,
                Replaces = replacement_slot,
                Type = new List<int> { 2 }
            };

            RomData.TargetSequences.Add(new_music_slot);

        }

        public static void ReadInstrumentSetList()
        {
            // traverse the whole audiobank index and grab details about every bank
            //  use those details to generate a list from the vanilla game that we can modify as needed
            List<InstrumentSetInfo> InstrumentSets = new List<InstrumentSetInfo>();
            for (int inst_set_num = 0; inst_set_num <= 0x28; ++inst_set_num)
            {
                // each bank has one 16 byte sentence of data, first word is address, second length, last 8 bytes metadata
                int table_pointer_addr = Addresses.AudiobankTable + (16 * inst_set_num);
                int bank_location = (ReadWriteUtils.ReadU16(table_pointer_addr) << 16) + ReadWriteUtils.ReadU16(table_pointer_addr + 2);
                int bank_length = (ReadWriteUtils.ReadU16(table_pointer_addr + 4) << 16) + ReadWriteUtils.ReadU16(table_pointer_addr + 6);
                byte[] metadata = new byte[8];
                for (int b = 0; b < 8; ++b)
                    metadata[b] = ReadWriteUtils.Read(table_pointer_addr + 8 + b);
                byte[] bank_data = new byte[bank_length];
                for (int b = 0; b < bank_length; ++b)
                    bank_data[b] = ReadWriteUtils.Read(Addresses.Audiobank + bank_location + b);

                InstrumentSetInfo new_bank = new InstrumentSetInfo
                {
                    BankSlot = inst_set_num,
                    BankMetaData = metadata,
                    BankBinary = bank_data
                };

                InstrumentSets.Add(new_bank);
            }
            RomData.InstrumentSetList = InstrumentSets;
        }

        public static void RebuildAudioBank(List<InstrumentSetInfo> InstrumentSetList)
        {

            // get index for the old audiobank, we're putting it back int the same spot but letting it expand into audioseq's spot, because the later is no longer there
            int f = RomUtils.GetFileIndexForWriting(Addresses.AudiobankTable);
            // the DMA table doesn't point directly to the table on the rom, its part of a larger yaz0 file, we have to use an offset to get the address in the file
            int audiobank_table_adjusted_addr = Addresses.AudiobankTable - RomData.MMFileList[RomUtils.GetFileIndexForWriting(Addresses.AudiobankTable)].Addr;

            int current_audiobank_addr = 0;
            byte[] new_audiobank = new byte[0];

            // for each bank, concat onto the new bank byte object, update the table to match the new instrument sets
            for (int inst_set_num = 0; inst_set_num <= 0x28; ++inst_set_num)
            {
                InstrumentSetInfo current_bank  = InstrumentSetList[inst_set_num]; // not sure lists are sequential in C#
                int bank_len                    = current_bank.BankBinary.Length;
                new_audiobank                   = new_audiobank.Concat(current_bank.BankBinary).ToArray();

                // update address of the bank in the table
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16]     = (byte)((current_audiobank_addr & 0xFF000000) >> 24);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 1] = (byte)((current_audiobank_addr & 0xFF0000) >> 16);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 2] = (byte)((current_audiobank_addr & 0xFF00) >> 8);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 3] = (byte)(current_audiobank_addr & 0xFF);

                // adjust the address for the next sequence to use
                current_audiobank_addr += bank_len;
                int remainder = bank_len % 16;
                if (remainder > 0)
                {              // in the event the user made an audiobank instrument set that isn't padded
                    new_audiobank = new_audiobank.Concat(new byte[remainder + 0x10]).ToArray(); // padding with a spare 16 byte line sounds cheap enough to try
                    Debug.WriteLine("Bank " + inst_set_num + " wasn't padded to 16 bytes:" + bank_len.ToString("X"));
                    current_audiobank_addr += remainder;
                }

                // update length of the bank in the table
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 4] = (byte)((bank_len & 0xFF000000) >> 24);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 5] = (byte)((bank_len & 0xFF0000) >> 16);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 6] = (byte)((bank_len & 0xFF00) >> 8);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 7] = (byte)(bank_len & 0xFF);

                // update metadata of the bank in the table
                for (int meta_iter = 0; meta_iter < 8; ++meta_iter)
                    RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + (inst_set_num * 16) + 8 + meta_iter] = current_bank.BankMetaData[meta_iter];
            }

            // write audioseq as a new file
            f = RomUtils.GetFileIndexForWriting(Addresses.Audiobank);
            RomData.MMFileList[f].Data = new_audiobank;
        }


    }
}
