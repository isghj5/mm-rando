using MMR.Randomizer.Constants;
using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Compression;
using System.Security.Cryptography;

namespace MMR.Randomizer.Utils
{
    public class SequenceUtils
    {
        // these are places the player may never visit, if they do they are visited very briefly, and very little music is heard
        // 0F:sharp kills you, 05:clock tower, 7C:giantsleave, 04:skullkid theme
        // 42:gormon brothers, 27:musicbox house, 31:mayor's office, 45:kaepora's theme
        // 72:wagonride, 0E:boatcruise, 29:zelda, 2D:giants, 
        // 2E:guruguru, 7B:maskreveal(gaints summon cutscene), 73:keaton, 70:calling giants
        // 7D is reunion, 0x50 is sword school
        public static List<int> lowUseMusicSlots = new List<int> { 0x0F, 0x05, 0x7C, 0x04, 0x42, 0x27, 0x31, 0x45, 0x72, 0x0E, 0x29, 0x2D, 0x2E, 0x7B, 0x73, 0x70, 0x7D, 0x50 };

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
            var directories = new List<string>();
            if (!Directory.Exists(Values.MusicDirectory))
            {
                Directory.CreateDirectory(Values.MusicDirectory); // we still need for MM-only
            }
            directories.AddRange(Directory.GetDirectories(Values.MusicDirectory).ToList());
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
                    try
                    {
                        string sourceName = lines[i];
                        List<int> sourceType = new List<int>();
                        foreach (String part in lines[i + 1].Split(','))
                        {
                            sourceType.Add(Convert.ToInt32(part, 16));
                        }

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

                        if (sourceSequence.Name.StartsWith("mm-"))
                        {
                            targetSequence.Replaces = Convert.ToInt32(lines[i + 3], 16);
                            sourceSequence.MM_seq = Convert.ToInt32(lines[i + 3], 16);
                            if (lines[i + 4] == "no-recycle")
                            {
                                //Debug.WriteLine("Player does not want to reuse song: " + sourceSequence.Name);
                                sourceSequence.Name = "drop";
                                i += 1;
                            }
                            i += 4;
                            if (RomData.TargetSequences.Find(u => u.Name == sourceName) != null)
                            {
                                continue; //old already have it
                            }
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
                    }
                    catch (Exception e)
                    {
                        string aboveLines = "";
                        string nl = "\n";
                        if (i > 3)
                        {
                            aboveLines += lines[i - 3] + nl + lines[i - 2] + nl + lines[i - 1] + nl;
                        }
                        throw new Exception( "Error while reading SEQS.txt:\n"
                                           + e.Message + "\n\n"
                                           + "Caused by the line with the arrow:\n\n"
                                           + aboveLines
                                           + lines[i] + "  <--\n"
                                           + lines[i+1] + nl + lines[i+2] + nl + lines[i+3]);
                    }
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
                    {
                        sourceType.Add(Convert.ToInt32(part, 16));
                    }

                    SequenceInfo sourceSequence = new SequenceInfo
                    {
                        Name = filename,
                        Directory = directory,
                        Type = sourceType,
                        Instrument = sourceInstrument
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
            //    if the song requires custom instrument samples, those are also here
            //  the user should be able to pack the archive with multiple sequences and multiple banks to match,
            //   where the redundancy increases likley hood of a song being able to be placed in a free audiobank slot
            MD5 md5lib = MD5.Create();

            foreach (string filePath in Directory.GetFiles(directory, "*.mmrs"))
            {
                try
                {
                    using (ZipArchive zip = ZipFile.OpenRead(filePath))
                    {
                        var currentSong = new SequenceInfo(); ;
                        var splitFilePath = filePath.Split('\\');
                        currentSong.Name = splitFilePath[splitFilePath.Length - 1];

                        //read categories file
                        ZipArchiveEntry categoriesFileEntry = zip.GetEntry("categories.txt");
                        if (categoriesFileEntry != null)
                        {
                            var categoriesList = new List<int>();
                            var categoryData = new StreamReader(categoriesFileEntry.Open(), Encoding.Default).ReadToEnd();
                            var delimitingChar = ',';
                            if (categoryData.Contains("-")) // someone will mess this up, its an easy thing to check for here tho
                            {
                                delimitingChar = '-';
                            }
                            else if (categoryData.Contains("\n"))
                            {
                                delimitingChar = '\n';
                            }
                            foreach (var line in categoryData.Split(delimitingChar))
                            {
                                categoriesList.Add(Convert.ToInt32(line, 16));
                            }
                            currentSong.Type = categoriesList;
                        }
                        else  // there should always be one, if not, print error and skip
                        {
                            Debug.WriteLine("ERROR: cannot find a categories file for " + currentSong.Name);
                            continue;
                        }

                        // read list of sound samples
                        var samplesList = new List<SequenceSoundSampleBinaryData>();
                        foreach (ZipArchiveEntry zSoundFile in zip.Entries.Where(e => e.Name.Contains("zsound")))
                        {
                            var sampleData = new byte[zSoundFile.Length];
                            zSoundFile.Open().Read(sampleData, 0, sampleData.Length);
                            var sampleNameSplit = zSoundFile.Name.Split('_'); // everything before _ is a comment, readability, discard here
                            var sampleName = sampleNameSplit.Length > 1 ? sampleNameSplit[sampleNameSplit.Length - 1] : zSoundFile.Name;
                            sampleName = sampleName.Split('.')[0];        // we don't need the filetype after here either at this point
                            uint sampleNameMarker = Convert.ToUInt32(sampleName, 16);
                            samplesList.Add(
                                new SequenceSoundSampleBinaryData()
                                {
                                    BinaryData = sampleData,
                                    Addr = sampleNameMarker,
                                    Marker = sampleNameMarker,
                                    Hash = BitConverter.ToInt64(md5lib.ComputeHash(sampleData), 0)
                                }
                            );
                        }
                        currentSong.InstrumentSamples = samplesList;

                        // per sequence, read sequence and banks if needed
                        foreach (ZipArchiveEntry zSeqFile in zip.Entries.Where(e => e.Name.Contains("zseq")))
                        {
                            // read sequence binary file
                            var zSeqData = new byte[zSeqFile.Length];
                            zSeqFile.Open().Read(zSeqData, 0, zSeqData.Length);
                            var zSeq = new SequenceBinaryData() { SequenceBinary = zSeqData };

                            // zseq filename is the instrument set
                            var filename = zSeqFile.Name.Substring(0, zSeqFile.Name.LastIndexOf(".zseq"));
                            var commentSplit = filename.Split('_'); // everything before _ is a comment, readability, discard here
                            var fileNameInstrumentSet = commentSplit.Length > 1 ? commentSplit[commentSplit.Length - 1] : filename;
                            currentSong.Instrument = Convert.ToInt32(fileNameInstrumentSet, 16);

                            var bankFileEntry = zip.GetEntry(filename + ".zbank");
                            if (bankFileEntry != null) // custom bank detected
                            {
                                // read bank file
                                byte[] zBankData = new byte[bankFileEntry.Length];
                                bankFileEntry.Open().Read(zBankData, 0, zBankData.Length);

                                // read bankmeta file
                                var bankmetaFileEntry = zip.GetEntry(filename + ".bankmeta");
                                var bankmetaData = new byte[bankmetaFileEntry.Length];
                                bankmetaFileEntry.Open().Read(bankmetaData, 0, bankmetaData.Length);

                                zSeq.InstrumentSet = new InstrumentSetInfo()
                                {
                                    BankBinary = zBankData,
                                    BankSlot = currentSong.Instrument,
                                    BankMetaData = bankmetaData,
                                    Modified = 1,
                                    Hash = BitConverter.ToInt64(md5lib.ComputeHash(zBankData), 0),
                                };
                            }//if requires bank

                            // multiple seq possible, add depending on if first or not
                            if (currentSong.SequenceBinaryList == null)
                            {
                                currentSong.SequenceBinaryList = new List<SequenceBinaryData> { zSeq };
                            }
                            else
                            {
                                currentSong.SequenceBinaryList.Add(zSeq);
                            }
                        } // foreach zip entry

                        if (currentSong != null && currentSong.SequenceBinaryList != null)
                        {
                            RomData.SequenceList.Add(currentSong);
                        }

                    }// zip as file
                }// for each zip
                catch (Exception e) // log it, continue with other songs
                {
                    Debug.WriteLine("Error attempting to read archive: " + filePath + " -- " + e);
                }
            }
        }

        public static void PointerizeSequenceSlots()
        {
            // if music availablilty is low, pointerize some slots
            // why? because in Z64 fairy fountain and fileselect are the same song,
            //  with one being a pointer at the other, so we have 78 slots and 77 songs, not enough
            //  also some categories can get exhausted leaving slots unfillable with remaining music,
            // several slots that players will never/rarely hear are nullified (pointed at another song)
            // this "fills" those slots, now we have fewer slots to fill with remaining music (77 fills 73)
            //  so pointerized slots play the same music, and don't waste a song
            //  but if the player does find this music in-game, it still plays sufficiently random music
            ConvertSequenceSlotToPointer(0x29, 0x0B); // point zelda(SOTime get cs) at healed

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
            bool shortenedCutscenes = RomData.MMFileList[1472].Data != null && RomData.MMFileList[1472].Data[0xD48 + 3] != 0x94;

            if (shortenedCutscenes)
            {
                // these cutcscene songs are never heard if shorten cutscenes is enabled, just pointerize it
                ConvertSequenceSlotToPointer(0x04, 0x45); // point skullkid's theme, during skullkid's backstory cutscene, at kaepora
                ConvertSequenceSlotToPointer(0x72, 0x45); // point wagonride at kaeopora 
                ConvertSequenceSlotToPointer(0x2D, 0x3A); // point giants world (oath get cutscene) at observatory
                ConvertSequenceSlotToPointer(0x70, 0x0B); // point call the giants( cutscene confronting skullkid) at healed
                ConvertSequenceSlotToPointer(0x7B, 0x0D); // point maskreveal, the song that plays when the mask shows its alive during moon cutscene, at aliens
                ConvertSequenceSlotToPointer(0x7D, 0x05); // point reunion at clocktower
                ConvertSequenceSlotToPointer(0x0B, 0x05); // point healing cutscene at clocktower
            }

            if (RomData.TargetSequences.Count + 30 > RomData.SequenceList.Count)
            {
                ConvertSequenceSlotToPointer(0x76, 0x15); // point titlescreen at clocktownday1
                ConvertSequenceSlotToPointer(0x08, 0x09); // point chasefail(skullkid chase) at fail
                ConvertSequenceSlotToPointer(0x19, 0x78); // point clearshort(epona get cs) at dungeonclearshort
            }

            // create some pointerized slots that are otherwise ignored, beacuse this pool gets re-used later for new song slots
            RomData.PointerizedSequences.Add(new SequenceInfo() { Name = "mm-introcutscene1", MM_seq = 0x1E, PreviousSlot = 0x1E, Replaces = 0x76 });
        }

        public static void ConvertSequenceSlotToPointer(int seqSlotIndex, int substituteSlotIndex)
        {
            // turns the sequence slot into a pointer, which points at another song, at SubstituteSlotIndex
            // the slot at SeqSlotIndex is marked such that, instead of a new sequence being put there
            //  a pointer to another song, at SubstituteSlotIndex, is used instead.
            // this frees up a song slot but its not completely empty if someone finds it
            //  this is the same concept DB used to nulify the intro song
            var targetSeq = RomData.TargetSequences.Find(u => u.Replaces == seqSlotIndex);
            var substituteSeq = RomData.TargetSequences.Find(u => u.Replaces == substituteSlotIndex);
            if (targetSeq != null && substituteSeq != null)
            {
                targetSeq.PreviousSlot = targetSeq.Replaces; // we'll need at audioseq build
                targetSeq.Replaces = substituteSeq.Replaces; // point the target at the substitute
                RomData.PointerizedSequences.Add(targetSeq); // save the sequence for audioseq
                RomData.TargetSequences.Remove(targetSeq);   // close the slot
            }
            else
            {
                //throw new IndexOutOfRangeException("Could not convert slot to pointer:" + SeqSlotIndex.ToString("X2"));
                Debug.WriteLine("Cannot pointerize a songslot that does not exist: " + seqSlotIndex.ToString("X") + " and " + substituteSlotIndex.ToString("X"));
            }
        }


        // gets passed RomData.SequenceList in Builder.cs::WriteAudioSeq
        public static void RebuildAudioSeq(List<SequenceInfo> sequenceList)
        {
            // spoiler log output DEBUG
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str); // we still want debug output though
                log.AppendLine(str);
            }

            List<MMSequence> oldSeq = new List<MMSequence>();
            int f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            int basea = RomData.MMFileList[f].Addr;

            for (int i = 0; i < 128; i++)
            {
                MMSequence entry = new MMSequence();

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
                    int j = sequenceList.FindIndex(u => u.Replaces == i);
                    if (j != -1)
                    {
                        if ((entry.Addr > 0) && (entry.Addr < 128))
                        {
                            if (sequenceList[j].Replaces != 0x28) // 28 (fairy fountain)
                            {
                                sequenceList[j].Replaces = entry.Addr;
                            }
                            else
                            {
                                entry.Data = oldSeq[0x18].Data;
                            }
                        }
                    }
                }
                oldSeq.Add(entry);
            }

            List<MMSequence> newSeq = new List<MMSequence>();
            int addr = 0;
            byte[] newAudioSeq = new byte[0];
            for (int i = 0; i < 128; i++)
            {
                MMSequence newentry = new MMSequence();
                if (oldSeq[i].Size == 0)
                {
                    newentry.Addr = oldSeq[i].Addr;
                }
                else
                {
                    newentry.Addr = addr;
                }

                if (sequenceList.FindAll(u => u.Replaces == i).Count > 1)
                {
                    WriteOutput("Error: Slot " + i.ToString("X") + " has multiple songs pointing at it!");
                }

                int p = RomData.PointerizedSequences.FindIndex(u => u.PreviousSlot == i);
                int j = sequenceList.FindIndex(u => u.Replaces == i);
                if (p != -1)
                {
                    // found song we want to pointerize
                    newentry.Addr = RomData.PointerizedSequences[p].Replaces;
                }
                else if (j != -1)
                {
                    // new song to replace old slot found
                    if (sequenceList[j].MM_seq != -1)
                    {
                        newentry.Data = oldSeq[sequenceList[j].MM_seq].Data;
                        WriteOutput("Slot " + i.ToString("X2") + " -> " + sequenceList[j].Name);

                    }
                    else if (sequenceList[j].SequenceBinaryList != null && sequenceList[j].SequenceBinaryList.Count > 0)
                    {
                        if (sequenceList[j].SequenceBinaryList.Count > 1)
                        {
                            WriteOutput("Warning: writing song with multiple sequence/bank combos, selecting first available");
                        }
                        newentry.Data = sequenceList[j].SequenceBinaryList[0].SequenceBinary;
                        WriteOutput("Slot " + i.ToString("X2") + " := " + sequenceList[j].Name + " *");

                    }
                    else // non mm, load file and add
                    {
                        byte[] data;
                        if (File.Exists(sequenceList[j].Filename))
                        {
                            using (var reader = new BinaryReader(File.OpenRead(sequenceList[j].Filename)))
                            {
                                data = new byte[(int)reader.BaseStream.Length];
                                reader.Read(data, 0, data.Length);
                            }
                        }
                        else if (sequenceList[j].Name == nameof(Properties.Resources.mmr_f_sot))
                        {
                            data = Properties.Resources.mmr_f_sot;
                        }
                        else
                        {
                            throw new Exception("Music not found as file or built-in resource." + sequenceList[j].Filename);
                        }

                        // I think this checks if the sequence type is correct for MM
                        //  because DB ripped sequences from SF64/SM64/MK64 without modifying them
                        if (data[1] != 0x20)
                        {
                            data[1] = 0x20;
                        }

                        newentry.Data = data;
                        WriteOutput("Slot " + i.ToString("X2") + " := " + sequenceList[j].Name);

                    }
                }
                else // not found, song wasn't touched by rando, just transfer over
                {
                    newentry.Data = oldSeq[i].Data;
                }

                // if the sequence is not padded to 16 bytes, the DMA fails
                //  music can stop from playing and on hardware it will just straight crash
                var padding = 0x10 - newentry.Size % 0x10;
                if (padding != 0x10)
                {
                    newentry.Data = newentry.Data.Concat(new byte[padding]).ToArray();
                }

                newSeq.Add(newentry);
                // TODO is there not a better way to write this?
                if (newentry.Data != null)
                {
                    newAudioSeq = newAudioSeq.Concat(newentry.Data).ToArray();
                }

                addr += newentry.Size;
            }

            // discovered when MM-only music was fixed, if the audioseq is left in it's old spot
            // audio quality is garbage, sounds like static
            //if (addr > (RomData.MMFileList[4].End - RomData.MMFileList[4].Addr))
            //else
            //RomData.MMFileList[4].Data = NewAudioSeq;

            int index = RomUtils.AppendFile(newAudioSeq);
            ResourceUtils.ApplyHack(Resources.mods.reloc_audio);
            RelocateSeq(index);
            RomData.MMFileList[4].Data = new byte[0];
            RomData.MMFileList[4].Cmp_Addr = -1;
            RomData.MMFileList[4].Cmp_End = -1;

            //update sequence index pointer table
            f = RomUtils.GetFileIndexForWriting(Addresses.SeqTable);
            for (int i = 0; i < 128; i++)
            {
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, (Addresses.SeqTable + (i * 16)) - basea, (uint)newSeq[i].Addr);
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[f].Data, 4 + (Addresses.SeqTable + (i * 16)) - basea, (uint)newSeq[i].Size);
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
                if (newSeq[i].Size == 0) // pointer, we need to copy the instrumnet set from the destination
                {
                    j = sequenceList.FindIndex(u => u.Replaces == newSeq[i].Addr);
                }
                else
                {
                    j = sequenceList.FindIndex(u => u.Replaces == i);
                }

                if (j != -1)
                {
                    RomData.MMFileList[f].Data[paddr] = (byte)sequenceList[j].Instrument;
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
            ReadWriteUtils.WriteToROM(0x00C2739C, new byte[] { 0x3C, 0x08, 0x80, 0x0A, 0x8D, 0x05, (byte)(offset >> 8), (byte)(offset & 0xFF) });
        }

        public static bool TestIfAvailableBanks(SequenceInfo testSeq, SequenceInfo targetSlot, StringBuilder log, Random rng, List<SequenceInfo> unassignedSequences)
        {
            /// test if the testSeq can be used with available instrument set slots

            // check if custom instrument sets exist for this sequence
            if (testSeq.SequenceBinaryList != null && testSeq.SequenceBinaryList.Count > 0 && testSeq.SequenceBinaryList.Any(u => u.InstrumentSet != null))
            {
                // randomize instrument sets last second, so the early banks don't get ravaged based on order
                if (testSeq.SequenceBinaryList.Count > 1)
                {
                    testSeq.SequenceBinaryList.OrderBy(x => rng.Next()).ToList();
                }

                testSeq.ClearUnavailableBanks(); // clear the sequence list of {bank/sequence} we cannot use

                if (testSeq.SequenceBinaryList.Count == 0) // all removed, song is dead.
                {
                    log.AppendLine($"{ testSeq.Name,-50}  cannot be used because it requires custom audiobank(s) already claimed ");
                    unassignedSequences.Remove(testSeq);
                    return false;
                }

                // some slots are rarely heard in-game, dont waste a custom instrument set on them, check if this slot is one of them
                if (IsBlockedByLowUse(testSeq, targetSlot, log))
                {
                    return false;
                }
            }
            return true; // sequences with banks, or without needing banks, available
        }

        public static bool IsBlockedByLowUse(SequenceInfo testSeq, SequenceInfo targetSlot, StringBuilder log)
        {
            /// if the slot we are checking is a rarely used slot, and this song requires a custom instrument
            ///  skip so we don't waste precious instrument set slots on rarely heard music
            /// exception: if the song uses the unique song category, not general purpose; author wanted it to be in this slot, bypass
            var uniqueCategory = targetSlot.Replaces + 0x100;
            if (lowUseMusicSlots.Contains(targetSlot.Replaces) && !testSeq.Type.Contains(uniqueCategory)
                && !testSeq.SequenceBinaryList.Any(u => u.InstrumentSet == null))
            {
                var previouslyUsedBanks = testSeq.SequenceBinaryList.FindAll(u => (u.InstrumentSet.Hash != 0 && u.InstrumentSet.Hash == RomData.InstrumentSetList[u.InstrumentSet.BankSlot].Hash));
                if (previouslyUsedBanks.Count >= 1)
                {
                    // exception: if another song already chosen has claimed a bank this song can use
                    log.AppendLine($"{ testSeq.Name,-50}  was accepted despite low use because a usable bank was already selected from another song");
                    return false;
                }

                if (targetSlot.Type[0] < 8) // to reduce spam, limit logging this only to the regular categories
                {
                    log.AppendLine($"{testSeq.Name,-50}  skipped for slot {targetSlot.Replaces.ToString("X2")} because it's a low use slot and requires a custom bank");
                }

                return true;
            }

            return false;
        }

        public static void TryBackupSongPlacement(SequenceInfo targetSlot, StringBuilder log, List<SequenceInfo> unassignedSequences)
        {
            /// sometimes, the remaining song slots can't find a compatible song, here we loosen restrictions until we can build a seed

            // first attempt: just merge BGM and fanfare into super categories and attempt to find replacement
            // the first category of the type is the MAIN type, the rest are secondary
            SequenceInfo replacementSong = null;
            if (targetSlot.Type[0] <= 7 || targetSlot.Type[0] == 16)  // bgm or cutscene
            {
                replacementSong = unassignedSequences.Find(u => u.Type[0] <= 7 || u.Type[0] == 16 && u.SequenceBinaryList == null);
            }
            else //if (targetSlot.Type[0] <= 8) // fanfares
            {
                replacementSong = unassignedSequences.Find(u => u.Type[0] >= 8 && u.SequenceBinaryList == null);
            }

            if (replacementSong != null)
            {
                log.AppendLine(" * generalized replacement with " + replacementSong.Name + " song, with categories: " + String.Join(", ", replacementSong.Type.Select(x => "0x" + x.ToString("X2"))));
                replacementSong.Replaces = targetSlot.Replaces;
                log.AppendLine($"{targetSlot.Name,-50} {"APROX",+10} -> " + replacementSong.Name);
                unassignedSequences.Remove(replacementSong);
                return;
            }

            // second attempt, copy a song already used
            replacementSong = RomData.SequenceList.Find(u => u.Type[0] == targetSlot.Type[0]);
            if (replacementSong != null)
            {
                RomData.SequenceList.Add
                (
                    new SequenceInfo
                    {
                        Name = replacementSong.Name,
                        Directory = replacementSong.Directory,
                        MM_seq = replacementSong.MM_seq,
                        Type = replacementSong.Type,
                        Instrument = replacementSong.Instrument,
                        SequenceBinaryList = replacementSong.SequenceBinaryList,
                        PreviousSlot = replacementSong.PreviousSlot,
                        Replaces = targetSlot.Replaces
                    }
                );

                log.AppendLine(" * double dipping with song " + replacementSong.Name + ", with categories: " + String.Join(", ", replacementSong.Type.Select(x => "0x" + x.ToString("X2"))));
                log.AppendLine($"{targetSlot.Name,-40} {"COPY",+10} -> " + replacementSong.Name);
                return;
            }

            // should not make it this far, throw error
            log.AppendLine(" out of remaining songs:");
            foreach (SequenceInfo RemainingSong in unassignedSequences)
            {
                log.AppendLine(" - " + RemainingSong.Name + " with categories " + String.Join(",", RemainingSong.Type));
            }
            throw new Exception("Cannot randomize music on this seed with available music");
        }

        public static void AssignSequenceSlot(SequenceInfo slotSequence, SequenceInfo replacementSequence, List<SequenceInfo> remainingSequences, string debugString, StringBuilder log)
        {
            // if the song has a custom instrument set, lock the sequence, update inst set value, debug output
            if (replacementSequence.SequenceBinaryList != null && replacementSequence.SequenceBinaryList[0] != null && replacementSequence.SequenceBinaryList[0].InstrumentSet != null)
            {
                replacementSequence.Instrument = replacementSequence.SequenceBinaryList[0].InstrumentSet.BankSlot; // update to the one we want to use
                if (RomData.InstrumentSetList[replacementSequence.Instrument].Modified > 0)
                {
                    RomData.InstrumentSetList[replacementSequence.Instrument].Modified += 1;
                    log.AppendLine(" -- v -- Instrument set number " + replacementSequence.Instrument.ToString("X2") + " is being reused -- v --");
                }
                else
                {
                    RomData.InstrumentSetList[replacementSequence.Instrument] = replacementSequence.SequenceBinaryList[0].InstrumentSet;
                    RomData.InstrumentSetList[replacementSequence.Instrument].InstrumentSamples = replacementSequence.InstrumentSamples;
                    log.AppendLine(" -- v -- Instrument set number " + replacementSequence.Instrument.ToString("X2") + " has been claimed -- v --");
                }
                replacementSequence.SequenceBinaryList = new List<SequenceBinaryData> { replacementSequence.SequenceBinaryList[0] }; // lock the one we want
            }
            replacementSequence.Replaces = slotSequence.Replaces; // tells the rando later what song to put into slot_seq
            //the -40 and +10 pad out the text to allign on the same middle section for visual clarity
            log.AppendLine($"{slotSequence.Name,-40} {debugString,+10} -> " + replacementSequence.Name);
            remainingSequences.Remove(replacementSequence);
        }

        public static void CheckSongTest(List<SequenceInfo> sequences, StringBuilder log)
        {
            /// For song makers: songtest is a debug token found in the song filename that specifies to flood the seed with the music for testing
            SequenceInfo testSequenceFileselect = RomData.SequenceList.Find(u => u.Name.Contains("songtest") == true);
            if (testSequenceFileselect != null)
            {
                // we always put songtest music on fileselect, titlescreen, ctd1, and combat music
                SequenceInfo targetSlot = RomData.TargetSequences.Find(u => u.Name.Contains("mm-fileselect"));
                AssignSequenceSlot(targetSlot, testSequenceFileselect, sequences, "SONGTEST", log); // file select

                // rather than copy the song, we point the songslots at file select so they play the same songtest sequence
                ConvertSequenceSlotToPointer(0x76, 0x18);  // titlescreen
                ConvertSequenceSlotToPointer(0x15, 0x18);  // clocktown 1
                ConvertSequenceSlotToPointer(0x1a, 0x18);  // combat

                // in addition, we take all song slots that share a category with the song and add those too
                List<SequenceInfo> allRegularSongs = RomData.SequenceList.FindAll(u => u.Type.Intersect(testSequenceFileselect.Type).Any());
                foreach (SequenceInfo songslot in allRegularSongs)
                {
                    ConvertSequenceSlotToPointer(songslot.MM_seq, 0x18);
                }
                RomData.TargetSequences.Remove(targetSlot);
            }

        }

        public static void CheckSongForce(List<SequenceInfo> sequences, StringBuilder log, Random rng)
        {
            /// songs with 'songforce' are higher priorty and get shuffled to the top of the previously randomized list of sequences
            List<SequenceInfo> forcedSequences = RomData.SequenceList.FindAll(u => u.Name.Contains("songforce") == true).OrderBy(x => rng.Next()).ToList();
            if (forcedSequences != null && forcedSequences.Count > 0)
            {
                foreach (SequenceInfo seq in forcedSequences)
                {
                    log.AppendLine("Forcing song (" + seq.Name + ") to top of the song pool");
                    sequences.Remove(seq);
                    sequences.Insert(0, seq);
                }
            }
        }

        public static bool SearchForValidSongReplacement(List<SequenceInfo> unassignedSequences, SequenceInfo targetSlot, Random rng, StringBuilder log)
        {
            // we could replace this with a findall(compatible types) but then we lose the small chance of random category music
            foreach (var testSeq in unassignedSequences.ToList())
            {
                // increases chance of getting non-mm music, but only if we have lots of music remaining
                // disable until we can modify this in UI, as there is now enough music it feels unnecessary
                //if (unassigned.Count > 77 && testSeq.Name.StartsWith("mm") && testSeq.Type[0] < 0x100 && (random.Next(100) < 40))
                //    continue;

                // check if this song still has available banks or sequences, if not remove song and continue
                var songNotStarved = TestIfAvailableBanks(testSeq, targetSlot, log, rng, unassignedSequences);
                if (songNotStarved == false)
                {
                    continue; // song unacceptable, continue
                }

                // do the target slot and the possible match seq share a category?
                if (testSeq.Type.Intersect(targetSlot.Type).Any())
                {
                    AssignSequenceSlot(targetSlot, testSeq, unassignedSequences, "", log);
                    return true;
                }

                // Deathbasket wanted there to be a small chance of getting out of category music
                //  but not put fanfares into bgm, or visa versa
                // also restrict this nature to when there is plenty of music to work with
                // (testSeq.Type.Count > targetSlot.Type.Count) DBs code, maybe thought to be safer?
                else if (unassignedSequences.Count > 30
                    && testSeq.Type.Count > targetSlot.Type.Count
                    && rng.Next(30) == 0
                    && targetSlot.Type[0] <= 16
                    && (testSeq.Type[0] & 8) == (targetSlot.Type[0] & 8)
                    && testSeq.Type.Contains(0x10) == targetSlot.Type.Contains(0x10)
                    && !testSeq.Type.Contains(0x16))
                {
                    AssignSequenceSlot(targetSlot, testSeq, unassignedSequences, "LUCK", log);
                    return true;
                }
            }
            return false; // ran out of songs to try
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
            /// traverse the whole audiobank index and grab details about every bank
            ///  use those details to generate a list from the vanilla game that we can modify as needed
            RomData.InstrumentSetList = new List<InstrumentSetInfo>();
            for (int audiobankIndex = 0; audiobankIndex <= 0x28; ++audiobankIndex)
            {
                // each bank has one 16 byte sentence of data, first word is address, second is length, last 2 words metadata
                int audiobankIndexAddr = Addresses.AudiobankTable + (audiobankIndex * 0x10);
                int audiobankBankOffset = (ReadWriteUtils.ReadU16(audiobankIndexAddr) << 16) + ReadWriteUtils.ReadU16(audiobankIndexAddr + 2);
                int bankLength = (ReadWriteUtils.ReadU16(audiobankIndexAddr + 4) << 16) + ReadWriteUtils.ReadU16(audiobankIndexAddr + 6);

                byte[] bankMetadata = new byte[8];
                for (int b = 0; b < 8; ++b)
                {
                    bankMetadata[b] = ReadWriteUtils.Read(audiobankIndexAddr + 8 + b);
                }

                byte[] bankData = new byte[bankLength];
                for (int b = 0; b < bankLength; ++b)
                {
                    bankData[b] = ReadWriteUtils.Read(Addresses.Audiobank + audiobankBankOffset + b);
                }

                var newInstrumentSet = new InstrumentSetInfo
                {
                    BankSlot = audiobankIndex,
                    BankMetaData = bankMetadata,
                    BankBinary = bankData
                };

                RomData.InstrumentSetList.Add(newInstrumentSet);
            }
        }

        public static void UpdateBankInstrumentPointers(byte[] ROM)
        {
            /// the audiobank and new samples are already written to rom, now we need to go back and update all the pointers
            ///   since we cannot know where those samples are on the rom until A) the soundbank is written, and B) the sample file is written
            ///   because the pointer is an offset of the soundbank rom location, and both can shift in BuildRom()

            if (RomData.InstrumentSetList == null)
            {
                return;
            }

            int soundbankAddr = RomData.MMFileList[5].Cmp_Addr; // in vanilla it's 0x97f70 but MMR can shift it up because AudioSeq gets re-located
            int audiobankInstSetAddr = RomData.MMFileList[3].Cmp_Addr; // pointer to specific instrument set, starting with 0, updating per loop
            foreach (var instrumentset in RomData.InstrumentSetList)
            {
                if (instrumentset.InstrumentSamples != null && instrumentset.InstrumentSamples.Count > 0)
                {
                    foreach (var sample in instrumentset.InstrumentSamples)
                    {
                        // for each sample if the bank uses sample, lookup new rom for the sample
                        //   get soundbank offset for said address: address of collection + offset of specific sample in collection, minus soundbank
                        int soundbankSampleOffset = RomData.MMFileList[RomData.SamplesFileID].Cmp_Addr
                                                      + (int)RomData.ListOfSamples.Find(u => u.Hash == sample.Hash).Addr
                                                      - soundbankAddr;
                        byte[] sampleOffsetBytes = BitConverter.GetBytes(soundbankSampleOffset);
                        byte[] markerBytes = BitConverter.GetBytes(sample.Marker);

                        // find the location in the bank where the Sample Marker is
                        //   per byte, replace with the bytes from SampleOffsetBytes
                        for (int i = 0; i < (instrumentset.BankBinary.Length - 4); i += 1)
                        {
                            if (ROM[audiobankInstSetAddr + i + 0] == markerBytes[3]
                                && ROM[audiobankInstSetAddr + i + 1] == markerBytes[2]
                                && ROM[audiobankInstSetAddr + i + 2] == markerBytes[1]
                                && ROM[audiobankInstSetAddr + i + 3] == markerBytes[0])
                            {
                                ROM[audiobankInstSetAddr + i + 0] = sampleOffsetBytes[3];
                                ROM[audiobankInstSetAddr + i + 1] = sampleOffsetBytes[2];
                                ROM[audiobankInstSetAddr + i + 2] = sampleOffsetBytes[1];
                                ROM[audiobankInstSetAddr + i + 3] = sampleOffsetBytes[0];
                            }
                        }
                    }
                }
                audiobankInstSetAddr += instrumentset.BankBinary.Length;
            }
        }


        public static void WriteNewSoundSamples(List<InstrumentSetInfo> InstrumentSetList)
        {
            /// Writing all of our new samples in a single file at the end
            //  in the event we run out of MMFile DMA indexes: These files dont need to be a hard file in the filesystem
            //  they can be placed anywhere after the soundbank starting address on rom, instrument sample lookup doesnt use file system
            //  adding to the file system is just useful for shifting in BuildROM()

            // issue: we don't know right now where the samples will be written to, because BuildRom will shift the files
            //  for now we write samples, after audiobank/soundbank/samples are written, we'll update audiobank pointers
            // save extra soundsamples fid, and the samples with their data, for later (UpdateBankInstrumentPointers())
            int fid = RomData.SamplesFileID = RomUtils.AppendFile(new byte[0x0]);
            RomData.ListOfSamples = new List<SequenceSoundSampleBinaryData>();

            // for each custom instrument set that needs a custom sample
            foreach (InstrumentSetInfo instrumentSet in InstrumentSetList)
            {
                if (instrumentSet.InstrumentSamples != null && instrumentSet.InstrumentSamples.Count > 0)
                {
                    foreach (SequenceSoundSampleBinaryData sample in instrumentSet.InstrumentSamples)
                    {
                        // test if sample was already added by another song (OOT instruments for instance)
                        var previouslyWrittenSample = RomData.ListOfSamples.Find(u => sample.Hash == u.Hash);
                        if (previouslyWrittenSample == null) // if sample not already written once before
                        {
                            // get the rom addr of our new file, our file will start at the end of the last file
                            sample.Addr = (uint)RomData.MMFileList[fid].Data.Length;
                            // concat our sample to sample collection file
                            RomData.MMFileList[fid].Data = RomData.MMFileList[fid].Data.Concat(sample.BinaryData).ToArray();
                            // I don't know if samples need to be padded to 0x10 like sequences but might as well
                            int paddingRemainder = RomData.MMFileList[fid].Data.Length % 0x10;
                            if (paddingRemainder > 0)
                            {
                                RomData.MMFileList[fid].Data = RomData.MMFileList[fid].Data.Concat(new byte[paddingRemainder]).ToArray();
                            }
                            RomData.ListOfSamples.Add(sample);
                        }
                        else // get address of previously used sample
                        {
                            sample.Addr = previouslyWrittenSample.Addr;
                        }
                    }
                }
            }

        }

        public static void RebuildAudioBank(List<InstrumentSetInfo> InstrumentSetList)
        {
            // get index for the old audiobank, we're putting it back in the same spot but letting it expand into audioseq's spot, which was moved to the end
            int fid = RomUtils.GetFileIndexForWriting(Addresses.AudiobankTable);
            // the DMA table doesn't point directly to the indextable on the rom, its part of a larger yaz0 file, we have to use an offset to get the address in the file
            int audiobankIndexOffset = Addresses.AudiobankTable - RomData.MMFileList[RomUtils.GetFileIndexForWriting(Addresses.AudiobankTable)].Addr;

            int audiobankBankOffset = 0;
            var audiobankData = new byte[0];

            // for each bank, concat onto the new bank byte object, update the table to match the new instrument sets
            for (int audiobankIndex = 0; audiobankIndex <= 0x28; ++audiobankIndex)
            {
                var currentBank = InstrumentSetList[audiobankIndex];
                audiobankData = audiobankData.Concat(currentBank.BankBinary).ToArray();

                // update address of the bank in the index table
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 0] = (byte)((audiobankBankOffset & 0xFF000000) >> 24);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 1] = (byte)((audiobankBankOffset & 0xFF0000) >> 16);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 2] = (byte)((audiobankBankOffset & 0xFF00) >> 8);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 3] = (byte)(audiobankBankOffset & 0xFF);

                // update length of the bank in the table
                int currentBankLength = currentBank.BankBinary.Length;
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 4] = (byte)((currentBankLength & 0xFF000000) >> 24);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 5] = (byte)((currentBankLength & 0xFF0000) >> 16);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 6] = (byte)((currentBankLength & 0xFF00) >> 8);
                RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 7] = (byte)(currentBankLength & 0xFF);

                // update metadata of the bank in the table
                for (int metadataIter = 0; metadataIter < 8; ++metadataIter)
                {
                    RomData.MMFileList[fid].Data[audiobankIndexOffset + (audiobankIndex * 16) + 8 + metadataIter] = currentBank.BankMetaData[metadataIter];
                }

                // adjust the address for the next bank to use
                audiobankBankOffset += currentBankLength;
                int paddingRemainder = currentBankLength % 0x10;
                if (paddingRemainder > 0) // in the event the user made an audiobank instrument set that isn't padded
                {
                    audiobankData = audiobankData.Concat(new byte[paddingRemainder + 0x10]).ToArray(); // padding with a spare 16 byte line sounds cheap enough to try
                    audiobankBankOffset += paddingRemainder;
                }
            }

            // write new audiobank back to file
            RomData.MMFileList[RomUtils.GetFileIndexForWriting(Addresses.Audiobank)].Data = audiobankData;
        }
    }
}
