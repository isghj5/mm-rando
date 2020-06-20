using MMR.Randomizer.Constants;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;
using MMR.Randomizer.Models.Settings;
using System.Security.Cryptography;


namespace MMR.Randomizer.Utils
{
    public class SequenceUtils
    {

        public static void ReadSequenceInfo()
        {
            RomData.SequenceList = new List<SequenceInfo>();
            RomData.TargetSequences = new List<SequenceInfo>();

            // if file exists, we read the file instead of the resource
            string[] lines;
            if (File.Exists(Path.Combine(Values.MusicDirectory, "SEQS.txt")))
            {
                Debug.WriteLine("We found a user SEQS.txt file that we can use");
                var list = new List<string>();
                string line;
                using (StreamReader sr = new StreamReader(Path.Combine(Values.MusicDirectory, "SEQS.txt")))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                lines = list.ToArray();
            }
            else
            {
                // load SEQS.txt from source memory
                lines = Properties.Resources.SEQS.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }

            // multiple directory search
            List<String> directories = Directory.GetDirectories(Values.MusicDirectory).ToList();
            foreach (string d in directories.ToList()) // another layer deep to be safe
            {
                List<String> deeper_directories = Directory.GetDirectories(d).ToList();
                directories.AddRange(deeper_directories);
            }
            directories.Add(Values.MusicDirectory);

            foreach (String directory in directories)
            {
                int i = 0;
                while (i < lines.Length)
                {
                    string sourceName = lines[i];
                    List<int> sourceType = new List<int>();
                    foreach (String part in lines[i + 1].Split(','))
                        sourceType.Add(Convert.ToInt32(part, 16));

                    int sourceInstrument = Convert.ToInt32(lines[i + 2], 16);

                    var targetName = lines[i];
                    var targetType = sourceType;
                    var targetInstrument = Convert.ToInt32(lines[i + 2], 16);

                    SequenceInfo sourceSequence = new SequenceInfo
                    {
                        Name = sourceName,
                        Type = sourceType,
                        Instrument = sourceInstrument
                    };

                    SequenceInfo targetSequence = new SequenceInfo
                    {
                        Name = targetName,
                        Type = targetType,
                        Instrument = targetInstrument
                    };

                    if ( sourceSequence.Name.StartsWith("mm-") )
                    {
                        targetSequence.Replaces = Convert.ToInt32(lines[i + 3], 16);
                        sourceSequence.MM_seq = Convert.ToInt32(lines[i + 3], 16);
                        if (lines[i+4] == "no-recycle"){
                            //Debug.WriteLine("Player does not want to reuse song: " + sourceSequence.Name);
                            sourceSequence.Name = "drop";
                            i += 1;
                        }
                        i += 4;
                        if (RomData.TargetSequences.Find(u => u.Name == sourceName) != null)
                            continue; //old already have it
                        RomData.TargetSequences.Add(targetSequence);
                    }
                    else
                    {
                        i += 3;
                        if (File.Exists(Path.Combine(directory, sourceName)) == false)
                        {
                            // if sequence file doesn't exist, was removed by user, ignore it
                            continue;
                        }
                        sourceSequence.Directory = directory;
                    };

                    if (sourceSequence.MM_seq != 0x18 && sourceSequence.Name != "drop")
                    {
                        RomData.SequenceList.Add(sourceSequence);
                    };
                } // end while (i < lines.Length)

                RomData.SequenceList.Add(new SequenceInfo
                {
                    Name = nameof(Properties.Resources.mmr_f_sot),
                    Type = new List<int> { 8 },
                    Instrument = 3,
                    Replaces = 0x75,
                });

                ScanZSEQUENCE(directory); // scan for base zseq in music folder
                ScanForMMRS(directory); // scan for base mmrs in music folder
            }
        }

        public static void ScanZSEQUENCE(string directory) // TODO make this folder identifiable, add directory and list of banks from scanned directory to this
        {
            // check if files were added by user to directory
            // we're not going to check for non-zseq here until I find an easy way to do that
            //  Just going to trust users aren't stupid enough to think renaming a mp3 to zseq will work
            // format: FILENAME_InstrumentSet_Categories-separated-by-commas.zseq
            //  where the filename, instrumentset, and categories are separated by single underscore
            // This method of adding music is deprecated, MMRS has rendered this filetype unnecessary, however leaving in to make debugging faster
            foreach (String filePath in Directory.GetFiles(directory, "*.zseq"))
            {
                String filename = Path.GetFileName(filePath);
                try
                {
                    // test if file has enough delimiters to separate data into name_bank_formats
                    String[] pieces = filename.Split('_');
                    if (pieces.Length != 3)
                    {
                        continue;
                    }

                    var sourceName = filename;
                    // for zseq, categories/instrument are part of the filename, we need to extract
                    string sourceTypeString = pieces[2].Substring(0, pieces[2].Length - 5);
                    int sourceInstrument = Convert.ToInt32(pieces[1], 16);
                    List<int> sourceType = new List<int>();
                    foreach (String part in sourceTypeString.Split('-'))
                        sourceType.Add(Convert.ToInt32(part, 16));

                    SequenceInfo sourceSequence = new SequenceInfo
                    {
                        Name                = filename,
                        Directory           = directory,
                        Type                = sourceType,
                        Instrument          = sourceInstrument
                    };

                    RomData.SequenceList.Add(sourceSequence);
                }
                catch (FormatException)
                {
                    throw new Exception("Music: Filename is unparsable: " + filename);
                }
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
                try{
                    using (ZipArchive zip = ZipFile.OpenRead(filePath))
                    {
                        List<string> sequences = new List<string>();
                        SequenceInfo new_song = new SequenceInfo(); ;
                        String[] split_filepath = filePath.Split('\\');
                        new_song.Name = split_filepath[split_filepath.Length - 1];

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
                            Debug.WriteLine("ERRROR: cannot find a categories file for " + new_song.Name);
                            continue;
                        }

                        MD5 md5lib = MD5.Create();
                        foreach (ZipArchiveEntry zip_item in zip.Entries) {
                            if (zip_item.Name.Contains("zseq"))
                            {
                                // read sequence binary file
                                byte[] seq_data = new byte[zip_item.Length];
                                zip_item.Open().Read(seq_data, 0, seq_data.Length);

                                SequenceBinaryData new_sequence_data = new SequenceBinaryData(){SequenceBinary = seq_data};

                                string file_name_extensionless = zip_item.Name.Substring(0, zip_item.Name.LastIndexOf(".zseq"));
                                new_song.Instrument = Convert.ToInt32(file_name_extensionless, 16); // assuming we're naming with inst.zseq and inst.bank


                                ZipArchiveEntry bank_entry = zip.GetEntry(zip_item.Name.Substring(0, zip_item.Name.LastIndexOf(".zseq")) + ".zbank");
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
                                        Modified = 1,
                                        Hash = BitConverter.ToInt64(md5lib.ComputeHash(bank_data), 0)
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
                    Debug.WriteLine("Error attempting to read archive: " + filePath  + " -- " + e);
                }
            }
        }

        public static void PointerizeSequenceSlots()
        {
            // if music availablilty is low, pointerize some slots
            // why? because in MM fairy fountain and fileselect are the same song,
            //  with one being a pointer at the other, so we have 78 slots and 77 songs, not enough
            //  also some categories can get exhausted leaving slots unfillable with remaining music,
            // several slots that players will never/rarely hear are nullified (pointed at another song)
            // this "fills" those slots, now we have fewer slots to fill with remaining music (73 fits in 77)
            //  so pointerized slots play the same music, and don't waste a song
            //  but if the player does find this music in-game, it still plays sufficiently random music
            ConvertSequenceSlotToPointer(0x29, 0x7d); // point zelda(SOTime get cs) at reunion

            // with shortened cutscenes, we pointerize more slots that the player would not hear
            // if using a patch, _randomized is not set, lookup a shortened cutscene byte instead
            // =========================================================
            // File: 0x02CBF000, Address: 0x02CBFD48, Offset: 0x00000D48
            // Name: Z2_KONPEKI_ENT::Great Bay(Cutscene) -Scene File
            // =========================================================
            // Replaces:
            //   .dw 0x00010294  94
            // .orga 0x02CBFD48     ->
            //   .dw 0x00010000        00
            // checking if not 94 instead if 00 because 94 is vanilla and 00 is replacement
            //  thinking ahead, it's possible the adjusted value will change one day, but vanilla is static
            // if the file's data is null, nothing in that file was changed and therefore it is vanilla.
            bool ShortenedCutscenes = RomData.MMFileList[1472].Data != null && RomData.MMFileList[1472].Data[0xD48 + 3] != 0x94;

            if (ShortenedCutscenes)
            {
                // these cutcscene songs are never heard if shorten cutscenes is enabled, just pointerize it
                ConvertSequenceSlotToPointer(0x04, 0x45); // point skullkid's theme, during skullkid's backstory cutscene, at kaepora
                ConvertSequenceSlotToPointer(0x72, 0x45); // point wagonride at kaeopora 
                ConvertSequenceSlotToPointer(0x2D, 0x3A); // point giants world (oath get cutscene) at observatory
                ConvertSequenceSlotToPointer(0x70, 0x7D); // point call the giants( cutscene confronting skullkid) at reunion
                ConvertSequenceSlotToPointer(0x7B, 0x0D); // point maskreveal, the song that plays when the mask shows its alive during moon cutscene, at aliens
            }

            if (RomData.TargetSequences.Count + 30 > RomData.SequenceList.Count )
            {
                ConvertSequenceSlotToPointer(0x76, 0x15); // point titlescreen at clocktownday1
                ConvertSequenceSlotToPointer(0x08, 0x09); // point chasefail(skullkid chase) at fail
                ConvertSequenceSlotToPointer(0x19, 0x78); // point clearshort(epona get cs) at dungeonclearshort
            }
        }

        public static void ConvertSequenceSlotToPointer(int SeqSlotIndex, int SubstituteSlotIndex)
        {
            // turns the sequence slot into a pointer, which points at another song, at SubstituteSlotIndex
            // the slot at SeqSlotIndex is marked such that, instead of a new sequence being put there
            //  a pointer to another song, at SubstituteSlotIndex, is used instead.
            // this frees up a song slot but its not completely empty if someone finds it
            //  this is the same concept DB used to nulify the intro song
            var TargetSeq = RomData.TargetSequences.Find(u => u.Replaces == SeqSlotIndex);
            var SubstituteSeq = RomData.TargetSequences.Find(u => u.Replaces == SubstituteSlotIndex);
            if (TargetSeq != null && SubstituteSeq != null)
            {
                TargetSeq.PreviousSlot = TargetSeq.Replaces; // we'll need at audioseq build
                TargetSeq.Replaces = SubstituteSeq.Replaces; // point the target at the substitute
                RomData.PointerizedSequences.Add(TargetSeq); // save the sequence for audioseq
                RomData.TargetSequences.Remove(TargetSeq);   // close the slot
            }
            else
            {
                throw new IndexOutOfRangeException("Could not convert slot to pointer:" + SeqSlotIndex.ToString("X2"));
            }
        }


        // gets passed RomData.SequenceList in Builder.cs::WriteAudioSeq
        public static void RebuildAudioSeq(List<SequenceInfo> SequenceList, OutputSettings _settings)
        {
            // spoiler log output DEBUG
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str); // we still want debug output though
                log.AppendLine(str);
            }

            List<MMSequence> OldSeq = new List<MMSequence>();
            int f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            int basea = RomData.MMFileList[f].Addr;

            for (int i = 0; i < 128; i++)
            {
                MMSequence entry = new MMSequence();
                if (i == 0x1E) // intro music when link gets ambushed
                {
                    entry.Addr = 2;
                    OldSeq.Add(entry);
                    continue;
                }

                int entryaddr = Addresses.SeqTable + (i * 16);
                entry.Addr = (int)ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, entryaddr - basea);
                var size = (int)ReadWriteUtils.Arr_ReadU32(RomData.MMFileList[f].Data, (entryaddr - basea) + 4);
                if (size > 0)
                {
                    entry.Data = new byte[size];
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

                if (SequenceList.FindAll(u => u.Replaces == i).Count > 1)
                {
                    WriteOutput("Error: Slot " + i.ToString("X") + " has multiple songs pointing at it!");
                }

                int p = RomData.PointerizedSequences.FindIndex(u => u.PreviousSlot == i);
                int j = SequenceList.FindIndex(u => u.Replaces == i);
                if (p != -1)
                {
                    // found song we want to pointerize
                    newentry.Addr = RomData.PointerizedSequences[p].Replaces;
                }
                else if (j != -1)
                {
                    // new song to replace old slot found
                    if (SequenceList[j].MM_seq != -1)
                    {
                        newentry.Data = OldSeq[SequenceList[j].MM_seq].Data;
                        WriteOutput("Slot " + i.ToString("X2") + " -> " + SequenceList[j].Name);

                    }
                    else if (SequenceList[j].SequenceBinaryList != null && SequenceList[j].SequenceBinaryList.Count > 0)
                    {
                        if (SequenceList[j].SequenceBinaryList.Count > 1)
                        {
                            WriteOutput("Warning: writing song with multiple sequence/bank combos, selecting first available");
                        }
                        newentry.Data = SequenceList[j].SequenceBinaryList[0].SequenceBinary;
                        WriteOutput("Slot " + i.ToString("X2") + " := " + SequenceList[j].Name + " *");

                    }
                    else // non mm, load file and add
                    {
                        byte[] data;
                        if (File.Exists(SequenceList[j].Filename))
                        {
                            using (var reader = new BinaryReader(File.OpenRead(SequenceList[j].Filename)))
                            {
                                data = new byte[(int)reader.BaseStream.Length];
                                reader.Read(data, 0, data.Length);
                            }
                        }
                        else if (SequenceList[j].Name == nameof(Properties.Resources.mmr_f_sot))
                        {
                            data = Properties.Resources.mmr_f_sot;
                        }
                        else
                        {
                            throw new Exception("Music not found as file or built-in resource." + SequenceList[j].Filename);
                        }

                        // I think this checks if the sequence type is correct for MM
                        //  because DB ripped sequences from SF64/SM64/MK64 without modifying them
                        if (data[1] != 0x20)
                        {
                            data[1] = 0x20;
                        }

                        newentry.Data = data;
                        WriteOutput("Slot " + i.ToString("X2") + " := " + SequenceList[j].Name);

                    }
                }
                else // not found, song wasn't touched by rando, just transfer over
                {
                    newentry.Data = OldSeq[i].Data;
                }

                // if the sequence is not padded to 16 bytes, the DMA fails
                //  music can stop from playing and on hardware it will just straight crash
                var Padding = 0x10 - newentry.Size % 0x10;
                if (Padding != 0x10)
                {
                    newentry.Data = newentry.Data.Concat(new byte[Padding]).ToArray();
                }

                NewSeq.Add(newentry);
                // TODO is there not a better way to write this?
                if (newentry.Data != null)
                {
                    NewAudioSeq = NewAudioSeq.Concat(newentry.Data).ToArray();
                }

                addr += newentry.Size;
            }

            // discovered when MM-only music was fixed, if the audioseq is left in it's old spot
            // audio quality is garbage, sounds like static
            //if (addr > (RomData.MMFileList[4].End - RomData.MMFileList[4].Addr))
            //else
                //RomData.MMFileList[4].Data = NewAudioSeq;

            int index = RomUtils.AppendFile(NewAudioSeq);
            ResourceUtils.ApplyHack(Values.ModsDirectory, "reloc-audio");
            RelocateSeq(index);
            RomData.MMFileList[4].Data = new byte[0];
            RomData.MMFileList[4].Cmp_Addr = -1;
            RomData.MMFileList[4].Cmp_End = -1;

            //update sequence index pointer table
            f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            for (int i = 0; i < 128; i++)
            {
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, (Addresses.SeqTable + (i * 16)) - basea, (uint)NewSeq[i].Addr);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, 4 + (Addresses.SeqTable + (i * 16)) - basea, (uint)NewSeq[i].Size);
            }

            //update inst sets used by each new seq
            // this is NOT the audiobank, its the complementary instrument set value for each sequence
            //   IE, sequence 7 uses instrument set "10", we replaced it with sequnece ae which needs bank "23"
            f = RomUtils.GetFileIndexForWriting(Addresses.InstSetMap);
            basea = RomData.MMFileList[f].Addr;
            for (int i = 0; i < 128; i++)
            {
                // huh? paddr? pointer? padding?
                int paddr = (Addresses.InstSetMap - basea) + (i * 2) + 2;

                int j = -1;
                if (NewSeq[i].Size == 0) // pointer, we need to copy the instrumnet set from the destination
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

            //// DEBUG spoiler log output
            //String dir = Path.GetDirectoryName(_settings.OutputROMFilename);
            //String path = $"{Path.GetFileNameWithoutExtension(_settings.OutputROMFilename)}";
            //// spoiler log should already be written by the time we reach this far
            //if (File.Exists(Path.Combine(dir, path + "_SpoilerLog.txt")))
            //    path += "_SpoilerLog.txt";
            //else // TODO add HTML log compatibility
            //    path += "_SongLog.txt";

            //using (StreamWriter sw = new StreamWriter(Path.Combine(dir, path), append: true))
            //{
            //    sw.WriteLine(""); // spacer
            //    sw.Write(log);
            //}
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

        public static void ReassignSkulltulaHousesMusic(byte replacement_slot = 0x75)
        {
            // changes the skulltulla house BGM to a separate slot so it plays a new music that isn't generic cave music (overused)
            // the BGM for a scene is specified by a single byte in the scene headers

            // to modify the scene header, which is in the scene, we need the scene as a file
            //  we can get this from the Romdata.SceneList but this only gets populated on enemizer
            //  and we don't NEED to populate it since vanilla scenes are static, we can just hard code it here
            //  at re-encode, we'll have fewer decoded files to re-encode too
            int swamp_spider_house_fid = 1284; // taken from ultimate MM spreadsheet (US File list -> A column)

            // scan the files for the header that contains scene music (0x15 first byte)
            // 15xx0000 0000yyzz where zz is the sequence pointer byte
            RomUtils.CheckCompressed(swamp_spider_house_fid);
            for (int b = 0; b < 0x10 * 70; b += 8)
            {
                if (RomData.MMFileList[swamp_spider_house_fid].Data[b] == 0x15
                    && RomData.MMFileList[swamp_spider_house_fid].Data[b + 0x7] == 0x3B)
                {
                    RomData.MMFileList[swamp_spider_house_fid].Data[b + 0x7] = replacement_slot;
                    break;
                }
            }

            int ocean_spider_house_fid = 1291; // taken from ultimate MM spreadsheet
            RomUtils.CheckCompressed(ocean_spider_house_fid);
            for (int b = 0; b < 0x10 * 70; b += 8)
            {
                if (RomData.MMFileList[ocean_spider_house_fid].Data[b] == 0x15
                    && RomData.MMFileList[ocean_spider_house_fid].Data[b + 0x7] == 0x3B)
                {
                    RomData.MMFileList[ocean_spider_house_fid].Data[b + 0x7] = replacement_slot;
                    break;
                }
            }


            SequenceInfo new_music_slot = new SequenceInfo
            {
                Name = "mm-spiderhouse-replacement",
                MM_seq = replacement_slot,
                Replaces = replacement_slot,
                Type = new List<int> { 2 },
                Instrument = 3
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
            for (int inst_set_num = 0; inst_set_num <= 0x28; ++inst_set_num){
                InstrumentSetInfo current_bank   = InstrumentSetList[inst_set_num]; // not sure lists are sequential in C#
                int bank_len                     = current_bank.BankBinary.Length;
                new_audiobank                    = new_audiobank.Concat(current_bank.BankBinary).ToArray();

                // update address of the bank in the table
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16]     = (byte)((current_audiobank_addr & 0xFF000000) >> 24);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 1] = (byte)((current_audiobank_addr & 0xFF0000) >> 16);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 2] = (byte)((current_audiobank_addr & 0xFF00) >> 8);
                RomData.MMFileList[f].Data[audiobank_table_adjusted_addr + inst_set_num * 16 + 3] = (byte)(current_audiobank_addr & 0xFF);

                // adjust the address for the next sequence to use
                current_audiobank_addr += bank_len;
                int remainder = bank_len % 16;
                if (remainder > 0){              // in the event the user made an audiobank instrument set that isn't padded
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
