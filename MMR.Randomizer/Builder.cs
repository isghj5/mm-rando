using MMR.Common.Extensions;
using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Constants;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Colors;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Models.SoundEffects;
using MMR.Randomizer.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Point = SixLabors.Primitives.Point;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Color = System.Drawing.Color;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Formats.Png;
using System.Security.Cryptography;
//using System.Windows.Forms;

namespace MMR.Randomizer
{
    public class Builder
    {
        private RandomizedResult _randomized;
        private CosmeticSettings _cosmeticSettings;
        private MessageTable _messageTable;
        private ExtendedObjects _extendedObjects;
        private List<MessageEntry> _extraMessages;
        private Dictionary<int, ItemGraphic> _graphicOverrides;

        public Builder(RandomizedResult randomized, CosmeticSettings cosmeticSettings)
        {
            _randomized = randomized;
            _cosmeticSettings = cosmeticSettings;
            _messageTable = null;
            _extendedObjects = null;
            _extraMessages = new List<MessageEntry>();
            _graphicOverrides = new Dictionary<int, ItemGraphic>();
        }

        #region Sequences, sounds and BGM

        // this function decides which songs get shuffled, choosing song -> slot
        //  the audioseq file gets rearanged/built in SequenceUtils::RebuildAudioSeq
        private void BGMShuffle(Random random, OutputSettings _settings)
        {
            // spoiler log output
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str);
                log.AppendLine(str);
            }
            string GetSpacedString(string Start, int Width = 50, string DebugText = "")
            {
                // formating for spoiler log
                int padding = Start.Length <= Width ? Width - Start.Length : 0 + DebugText.Length;
                return Start + new String(' ', padding - DebugText.Length) + DebugText;
            }

            void AssignSequenceSlot(SequenceInfo SlotSequence, SequenceInfo ReplacementSequence, List<SequenceInfo> RemainingSongs, string DebugCharacters = "")
            {
                // if the song has a custom instrument set, lock the sequence, update inst set value, debug output
                if (ReplacementSequence.SequenceBinaryList != null && ReplacementSequence.SequenceBinaryList[0] != null && ReplacementSequence.SequenceBinaryList[0].InstrumentSet != null)
                {
                    ReplacementSequence.Instrument = ReplacementSequence.SequenceBinaryList[0].InstrumentSet.BankSlot; // update to the one we want to use
                    if (RomData.InstrumentSetList[ReplacementSequence.Instrument].Modified > 0)
                    {
                        RomData.InstrumentSetList[ReplacementSequence.Instrument].Modified += 1;
                        WriteOutput(" -- v -- Instrument set number " + ReplacementSequence.Instrument.ToString("X2") + " is being reused -- v --");
                    }
                    else
                    {
                        RomData.InstrumentSetList[ReplacementSequence.Instrument] = ReplacementSequence.SequenceBinaryList[0].InstrumentSet;
                        RomData.InstrumentSetList[ReplacementSequence.Instrument].InstrumentSamples = ReplacementSequence.InstrumentSamples;
                        WriteOutput(" -- v -- Instrument set number " + ReplacementSequence.Instrument.ToString("X2") + " has been claimed -- v --");
                    }
                    ReplacementSequence.SequenceBinaryList = new List<SequenceBinaryData> { ReplacementSequence.SequenceBinaryList[0] }; // lock the one we want
                }
                ReplacementSequence.Replaces = SlotSequence.Replaces; // tells the rando later what song to put into slot_seq
                WriteOutput(GetSpacedString(SlotSequence.Name, Width: 50, DebugCharacters) + " -> " + ReplacementSequence.Name);
                RemainingSongs.Remove(ReplacementSequence);
            }

            // these are places the player may never visit, if they do they are visited very briefly, and very little music is heard
            // 0F:sharp kills you, 05:clock tower, 7C:giantsleave, 04:skullkid theme
            // 42:gormon brothers, 27:musicbox house, 31:mayor's office, 45:kaepora's theme
            // 72:wagonride, 0E:boatcruise, 29:zelda, 2D:giants, 
            // 2E:guruguru, 7B:maskreveal(gaints summon cutscene), 73:keaton, 70:calling giants
            List<int> LowUseMusicSlots = new List<int> { 0x0F, 0x05, 0x7C, 0x04, 0x42, 0x27, 0x31, 0x45, 0x72, 0x0E, 0x29, 0x2D, 0x2E, 0x7B, 0x73, 0x70}; 

            // we randomize both slots and songs because if we're low on variety, and we don't sort slots
            //   then all the variety can be dried up for the later slots
            // the biggest example is MM-only, many songs are action/boss but the boss slots are later
            //  as a result boss music is often used up early placed into early action slots
            // if we don't randomize remaining, then we only get upper alphabetical, same every seed
            List<SequenceInfo> Unassigned = RomData.SequenceList.FindAll(u => u.Replaces == -1);
            Unassigned = Unassigned.OrderBy(x => random.Next()).ToList();                           // random ordered songs
            RomData.TargetSequences = RomData.TargetSequences.OrderBy(x => random.Next()).ToList(); // random ordered slots
            WriteOutput(" Randomizing " + RomData.TargetSequences.Count + " song slots, with " + Unassigned.Count + " available songs:");

            // if we have lots of music, let's randomize skulltula house and ikana well to have something unique that isn't cave music
            if (RomData.SequenceList.Count > 80 &&RomData.SequenceList.FindAll(u => u.Type.Contains(2)).Count >= 8 + 2){ // tested by asking for all targetseq that have a category of 2, counted (8)
                SequenceUtils.ReassignSongSlots();
            }

            // DEBUG: if the user has a test sequence it always get put into fileselect, ctd1, and combat for testing
            SequenceInfo TestSequenceFileselect = RomData.SequenceList.Find(u => u.Name.Contains("songtest") == true);
            if (TestSequenceFileselect != null)
            {
                SequenceInfo TestSequenceCombat = TestSequenceFileselect.SequenceCopy();
                SequenceInfo TestSequenceCTD1 = TestSequenceFileselect.SequenceCopy();
                SequenceInfo TargetSlot = RomData.TargetSequences.Find(u => u.Name.Contains("mm-fileselect"));
                AssignSequenceSlot(TargetSlot, TestSequenceFileselect, Unassigned, "SONGTEST"); // file select
                List<SequenceInfo> AllRegularSongs = RomData.SequenceList.FindAll(u =>  u.Type.Intersect(TestSequenceFileselect.Type).Any());
                SequenceUtils.ConvertSequenceSlotToPointer(0x76, 0x18);  // titlescreen
                foreach (SequenceInfo songslot in AllRegularSongs)
                {
                    SequenceUtils.ConvertSequenceSlotToPointer(songslot.MM_seq, 0x18);
                }
                RomData.TargetSequences.Remove(TargetSlot);
            }

            // music plando, user has selected they want an easier time specifying where and what songs are placed in specific spots
            var plandoPlacements = PlandoUtils.GetRandomizedSongPlacements(random, log);
            foreach ((var song, var slot) in plandoPlacements)
            {
                AssignSequenceSlot(slot, song, Unassigned, "PLANDO");
                RomData.TargetSequences.Remove(slot);
            }

            // MORE DEBUG: if the user wants to force a song to always show up each seed, but in random slots
            List<SequenceInfo> ForcedSequences = RomData.SequenceList.FindAll(u => u.Name.Contains("songforce") == true).OrderBy(x => random.Next()).ToList();
            if (ForcedSequences != null && ForcedSequences.Count > 0)
            {
                foreach(SequenceInfo seq in ForcedSequences)
                {
                    WriteOutput("Forcing song (" + seq.Name + ") to top of the song pool");
                    Unassigned.Remove(seq);
                    Unassigned.Insert(0, seq);
                }
            }

            foreach (SequenceInfo targetSequence in RomData.TargetSequences)
            {
                bool foundValidReplacement = false; // would really have liked for/else but C# doesn't have it seems

                // we could replace this with a findall(compatible types) but then we lose the small chance of random category music
                for (int i = 0; i < Unassigned.Count; i++)
                {
                    SequenceInfo testSeq = Unassigned[i];
                    // increases chance of getting non-mm music, but only if we have lots of music remaining
                    if (Unassigned.Count > 77 && testSeq.Name.StartsWith("mm") && (random.Next(100) < 40))
                        continue;

                    // test if the testSeq can be used with available instrument set slots
                    if (testSeq.SequenceBinaryList != null && testSeq.SequenceBinaryList.Count > 0 && testSeq.SequenceBinaryList.Any(u => u.InstrumentSet != null))
                    {
                        // randomize instrument sets last second, so the early banks don't get ravaged based on order
                        if (testSeq.SequenceBinaryList.Count > 1)
                        {
                            testSeq.SequenceBinaryList.OrderBy(x => random.Next()).ToList();
                        }

                        // clear the sequence list of sequences we cannot use
                        //  only keep sequences that don't need a custom instrument set, or sets that have one and its open, or sets that share one already used
                        testSeq.SequenceBinaryList = testSeq.SequenceBinaryList.FindAll(u => u.InstrumentSet == null
                                                                                          || (RomData.InstrumentSetList[u.InstrumentSet.BankSlot].Modified == 0
                                                                                            || (u.InstrumentSet.Hash != 0
                                                                                              && u.InstrumentSet.Hash == RomData.InstrumentSetList[u.InstrumentSet.BankSlot].Hash)));
                        if (testSeq.SequenceBinaryList.Count == 0) // all removed, song is dead.
                        {
                            WriteOutput(GetSpacedString(testSeq.Name) + " cannot be used because it requires custom audiobank(s) already claimed ");
                            Unassigned.Remove(testSeq);
                            continue;
                        }

                        // if the slot we are checking is a rarely used slot, and this song requires a custom instrument set
                        //  skip so we don't waste precious instrument set slots on rarely heard music
                        if (LowUseMusicSlots.Contains(targetSequence.Replaces) && ! testSeq.SequenceBinaryList.Any(u => u.InstrumentSet == null))
                        {
                            if(targetSequence.Type[0] < 8) // to reduce spam, limit this only to the regular categories
                            {
                                WriteOutput(GetSpacedString(testSeq.Name) + " skipped for slot " + targetSequence.Replaces.ToString("X2") + " because it's a low use slot and requires a custom bank");
                            }

                            continue;
                        }
                    }

                    // do the target slot and the possible match seq share a category?
                    if (testSeq.Type.Intersect(targetSequence.Type).Any()){
                        AssignSequenceSlot(targetSequence, testSeq, Unassigned, "");
                        foundValidReplacement = true;
                        break;
                    }

                    // Deathbasket wanted there to be a small chance of getting out of category music
                    //  but not put fanfares into bgm, or visa versa
                    // also restrict this nature to when there is plenty of music to work with
                    // (testSeq.Type.Count > targetSequence.Type.Count) DBs code, maybe thought to be safer?
                    else if (Unassigned.Count > 30
                        && testSeq.Type.Count > targetSequence.Type.Count
                        && random.Next(30) == 0
                        && targetSequence.Type[0] <= 16
                        && (testSeq.Type[0] & 8) == (targetSequence.Type[0] & 8)
                        && testSeq.Type.Contains(0x10) == targetSequence.Type.Contains(0x10)
                        && !testSeq.Type.Contains(0x16))
                    {
                        AssignSequenceSlot(targetSequence, testSeq, Unassigned, "LUCK");
                        foundValidReplacement = true;
                        break;
                    }
                }

                if (foundValidReplacement == false) // no available songs fit in this slot category
                {
                    // just add one of the remaining songs,
                    //  so long as bgm and fanfares are kept separate, should still be fine
                    WriteOutput("No song fits in " + targetSequence.Name + " slot, with categories: " + String.Join(", ", targetSequence.Type.Select(x => "0x" + x.ToString("X2"))));

                    // the first category of the type is the MAIN type, the rest are secondary
                    SequenceInfo replacementSong = null;
                    if (targetSequence.Type[0] <= 7 || targetSequence.Type[0] == 16)  // bgm or cutscene
                    {
                        replacementSong = Unassigned.Find(u => u.Type[0] <= 7 || u.Type[0] == 16 && u.SequenceBinaryList == null);
                    }
                    else //if (targetSequence.Type[0] <= 8)                           // fanfares
                    {
                        replacementSong = Unassigned.Find(u => u.Type[0] >= 8 && u.SequenceBinaryList == null);
                    }

                    if (replacementSong != null)
                    {
                        WriteOutput(" * generalized replacement with " + replacementSong.Name + " song, with categories: " + String.Join(", ", replacementSong.Type.Select(x => "0x" + x.ToString("X2"))));
                        replacementSong.Replaces = targetSequence.Replaces;
                        WriteOutput(GetSpacedString(targetSequence.Name, Width: 50, "APROX") + " -> " + replacementSong.Name);
                        Unassigned.Remove(replacementSong);
                    }
                    else
                    {
                        // last attempt, copy a song already used
                        replacementSong = RomData.SequenceList.Find(u => u.Type[0] >= targetSequence.Type[0]);
                        if (replacementSong != null)
                        {
                            RomData.SequenceList.Add
                            (
                                new SequenceInfo
                                {
                                    Name                = replacementSong.Name,
                                    Directory           = replacementSong.Directory,
                                    MM_seq              = replacementSong.MM_seq,
                                    Type                = replacementSong.Type,
                                    Instrument          = replacementSong.Instrument,
                                    SequenceBinaryList  = replacementSong.SequenceBinaryList,
                                    PreviousSlot        = replacementSong.PreviousSlot,
                                    Replaces            = targetSequence.Replaces
                                }
                            );

                            WriteOutput(" * double dipping with song " + replacementSong.Name + ", with categories: " + String.Join(", ", replacementSong.Type.Select(x => "0x" + x.ToString("X2"))));
                            WriteOutput(GetSpacedString(targetSequence.Name, Width: 50, "COPY") + " -> " + replacementSong.Name);
                        }
                        else
                        {
                            WriteOutput(" out of remaining songs:");
                            foreach (SequenceInfo RemainingSong in Unassigned)
                            {
                                WriteOutput(" - " + RemainingSong.Name + " with categories " + String.Join(",", RemainingSong.Type));
                            }
                            throw new Exception("Cannot randomize music on this seed with available music");
                        }
                    }
                }
            }

            RomData.SequenceList.RemoveAll(u => u.Replaces == -1); // this still gets used in SequenceUtils.cs::RebuildAudioSeq

            if (_cosmeticSettings.Music == Music.Random && _settings.GenerateSpoilerLog)
            {
                String dir = Path.GetDirectoryName(_settings.OutputROMFilename);
                String path = $"{Path.GetFileNameWithoutExtension(_settings.OutputROMFilename)}";
                // spoiler log should already be written by the time we reach this far
                if (File.Exists(Path.Combine(dir, path + "_SpoilerLog.txt")))
                    path += "_SpoilerLog.txt";
                else // TODO add HTML log compatibility
                    path += "_SongLog.txt";

                using (StreamWriter sw = new StreamWriter(Path.Combine(dir, path), append: true))
                {
                    sw.WriteLine(""); // spacer
                    sw.Write(log);
                }
            }
        }
        #endregion

        private void WriteAudioSeq(Random random, OutputSettings _settings)
        {
            if (_cosmeticSettings.Music == Music.None)
            {
                return;
            }

            RomData.PointerizedSequences = new List<SequenceInfo>();
            SequenceUtils.ReadSequenceInfo();
            SequenceUtils.ReadInstrumentSetList();
            if (_cosmeticSettings.Music == Music.Random)
            {
                SequenceUtils.PointerizeSequenceSlots();
                BGMShuffle(random, _settings);
            }

            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-music");
            ResourceUtils.ApplyHack(Values.ModsDirectory, "inst24-swap-guitar");
            SequenceUtils.RebuildAudioSeq(RomData.SequenceList, _settings);
            SequenceUtils.WriteNewSoundSamples(RomData.InstrumentSetList);
            SequenceUtils.RebuildAudioBank(RomData.InstrumentSetList);
        }

        private void WriteMuteMusic()
        {
            if (_cosmeticSettings.Music == Music.None)
            {
                // Traverse the audioseq index table to get the locations of all sequences
                byte[] audioseq_table = RomData.MMFileList[RomUtils.GetFileIndexForWriting(Addresses.SeqTable)].Data;
                // turns out the randomizer doesn't consider the table to be its own file, we need the offset
                int audioseq_table_baseaddr = RomData.MMFileList[RomUtils.GetFileIndexForWriting(Addresses.SeqTable)].Addr;
                byte[] audioseq = RomData.MMFileList[RomUtils.GetFileIndexForWriting(0x00046AF0)].Data; // 46AF0 is audioseq starting location
                // for each sequence, search for the master volume byte and change to zero
                for (int seq = 2; seq < 128; seq += 1){
                    if (seq == 0x54) // It was requested that the bar band minigame not be silenced
                        continue;
                    int seq_location_offset = (int)ReadWriteUtils.Arr_ReadU32(audioseq_table, (Addresses.SeqTable + seq * 16) - audioseq_table_baseaddr);
                    for (int byte_iter = 3; byte_iter < 128; byte_iter++){
                        if (audioseq[seq_location_offset + byte_iter] == 0xDB){
                            audioseq[seq_location_offset + byte_iter + 1] = 0x0;
                            continue;
                        }
                    }
                }
            }
        }

        private void WriteEnemyCombatMusicMute()
        {
            if (_cosmeticSettings.DisableCombatMusic == CombatMusic.Normal)
            {
                return;
            }

            Enemies.DisableEnemyCombatMusic(_cosmeticSettings.DisableCombatMusic == CombatMusic.WeakEnemies);
        }

        private void WritePlayerModel()
        {
            if (_randomized.Settings.Character == Character.LinkMM)
            {
                return;
            }

            if (_randomized.Settings.Character == Character.AdultLink)
            {
                PlayerModelUtils.ApplyAdultLinkPatches();
                return;
            }

            int characterIndex = (int)_randomized.Settings.Character;

            using (var b = new BinaryReader(File.OpenRead(Path.Combine(Values.ObjsDirectory, $"link-{characterIndex}"))))
            {
                var obj = new byte[b.BaseStream.Length];
                b.Read(obj, 0, obj.Length);
                ResourceUtils.ApplyHack(Values.ModsDirectory, $"fix-link-{characterIndex}");
                ObjUtils.InsertObj(obj, 0x11);
            }

            if (_randomized.Settings.Character == Character.Kafei)
            {
                using (var b = new BinaryReader(File.OpenRead(Path.Combine(Values.ObjsDirectory, "kafei"))))
                {
                    var obj = new byte[b.BaseStream.Length];
                    b.Read(obj, 0, obj.Length);
                    ObjUtils.InsertObj(obj, 0x1C);
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-kafei");
                }

                using (var b = new BinaryReader(File.OpenRead(Path.Combine(Values.ObjsDirectory, "link-mask"))))
                {
                    var obj = new byte[b.BaseStream.Length];
                    b.Read(obj, 0, obj.Length);
                    ObjUtils.InsertObj(obj, 0x1FF);
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "update-kafei-mask-icon");
                }

                using (var b = new BinaryReader(File.OpenRead(Path.Combine(Values.ObjsDirectory, "gi-link-mask"))))
                {
                    var obj = new byte[b.BaseStream.Length];
                    b.Read(obj, 0, obj.Length);
                    ObjUtils.InsertObj(obj, 0x258);
                }
            }
        }

        private void WriteTunicColor()
        {
            Color t = _cosmeticSettings.TunicColor;
            byte[] color = { t.R, t.G, t.B };

            var otherTunics = ResourceUtils.GetAddresses(Values.AddrsDirectory, "tunic-forms");
            TunicUtils.UpdateFormTunics(otherTunics, _cosmeticSettings.TunicColor);

            var playerModel = DeterminePlayerModel();
            var characterIndex = (int)playerModel;          
            if (playerModel == Character.Kafei)
            {
                var objectData = ObjUtils.GetObjectData(0x11);
                TunicUtils.UpdateKafeiTunic(ref objectData, t);
                ObjUtils.InsertObj(objectData, 0x11);
            }
            else
            {
                var locations = ResourceUtils.GetAddresses(Values.AddrsDirectory, $"tunic-{characterIndex}");
                var objectData = ObjUtils.GetObjectData(0x11);
                for (int j = 0; j < locations.Count; j++)
                {
                    ReadWriteUtils.WriteFileAddr(locations[j], color, objectData);
                }
                ObjUtils.InsertObj(objectData, 0x11);
            };
        }

        private void WriteMiscellaneousChanges()
        {
            if (_cosmeticSettings.EnableHoldZTargeting)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "ztargetinghold");
            }

            if (_cosmeticSettings.EnableNightBGM)
            {
                SceneUtils.ReenableNightBGM();
            }

        }

        private void WriteMiscText()
        {
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3108,
                Header = null,
                Message = "Say...Did you come to have some\u0011items fashioned?\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3130,
                Header = null,
                Message = "Gabora, fetch our customer some\u0011coffee, quick-like.\u0011\u0013\u0012Now then, let me take a look at\u0011our offers.\u0011\u0013\u0012Hmmm...\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3131,
                Header = null,
                Message = "All right... For today's \u0001hot deal\u0000,\u0011it will cost you \u0006100 Rupees\0. It'll\u0011be ready at \u0001sunrise.\u0011\0\u0012So how about it? Wanna grab a\u0011hot item for \u0006100 Rupees\u0000?\u0011\u0002\u00C2I'll do it\u0011No thanks\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3133,
                Header = null,
                Message = "This is a secret, but I'll let you in\u0011on it... The strongest sword out\u0011there was forged using \u0001gold\u0011dust\u0000.... I made it! Me!\u00E0\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3134,
                Header = null,
                Message = "Wanna grab a deal? \u0011\u0002 \u0011\u00C2Yes\u0011No thanks\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3140,
                Header = null,
                Message = "Hey! It's gonna be ready \u0001tomorrow\u0011morning\0. We'll take good care of\u0011it, so don't worry.\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3141,
                Header = null,
                Message = "Hey! For today's special product\u0011we'll need to get hold of some \u0011\u0001gold dust\u0000.\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3142,
                Header = null,
                Message = "Why, if it isn't \u0001gold dust\0! And it's\u0011even top quality!!!\u0011\u0013\u0012Why, even if I use it to craft\u0011a nifty item, there'll still be some\u0011left...\u0011\u0012All right! Just for you, I'll do this\u0011for free. But don't tell anyone!\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3147,
                Header = null,
                Message = "To make our item for you today,\u0011I'll need \u0001gold dust\0, which just so\u0011happens to be first prize at the\u0011Goron racetrack.\u0010If I can just get some gold dust...\u0011and this is just between us...I can\u0011make you the \u0001hottest item\u0011in the lands\u0000... Really!!\u00E0\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3150,
                Header = null,
                Message = "Huh? \u001f\0\nLook, I'm working on\u0011making this item for you. I'm\u0011busy, so don't bother me.\u00E0\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3153,
                Header = null,
                Message = "Behold! My skills in craftsmanship\u0011are truly unrivalled!\u0019\u00BF"
            });
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 3155,
                Header = null,
                Message = "Ah! My finest work!\u0011The look in your eye, I can\u0011tell you really wanted this!!\u0019\u00BF"
            });
        }

        private Character DeterminePlayerModel()
        {
            var data = ObjUtils.GetObjectData(0x11);
            var hash = MD5.Create().ComputeHash(data);

            if (hash.SequenceEqual(PlayerModelHash.LinkMM))
            {
                return Character.LinkMM;
            }
            else if (hash.SequenceEqual(PlayerModelHash.LinkOOT))
            {
                return Character.LinkOOT;
            }
            else if (hash.SequenceEqual(PlayerModelHash.AdultLink))
            {
                return Character.AdultLink;
            }
            else if (hash.SequenceEqual(PlayerModelHash.Kafei))
            {
                return Character.Kafei;
            }
            else
            {
                throw new Exception("Unable to determine player's model.");
            }
        }

        private void SetTatlColour(Random random)
        {
            if (_cosmeticSettings.TatlColorSchema == TatlColorSchema.Random)
            {
                for (int i = 0; i < 10; i++)
                {
                    byte[] c = new byte[4];
                    random.NextBytes(c);

                    if ((i % 2) == 0)
                    {
                        c[0] = 0xFF;
                    }
                    else
                    {
                        c[0] = 0;
                    }

                    Values.TatlColours[4, i] = BitConverter.ToUInt32(c, 0);
                }
            }
        }

        private void WriteTatlColour(Random random)
        {
            if (_cosmeticSettings.TatlColorSchema != TatlColorSchema.Rainbow)
            {
                SetTatlColour(random);
                var selectedColorSchemaIndex = (int)_cosmeticSettings.TatlColorSchema;
                byte[] c = new byte[8];
                List<int[]> locs = ResourceUtils.GetAddresses(Values.AddrsDirectory, "tatl-colour");
                for (int i = 0; i < locs.Count; i++)
                {
                    ReadWriteUtils.Arr_WriteU32(c, 0, Values.TatlColours[selectedColorSchemaIndex, i << 1]);
                    ReadWriteUtils.Arr_WriteU32(c, 4, Values.TatlColours[selectedColorSchemaIndex, (i << 1) + 1]);
                    ReadWriteUtils.WriteROMAddr(locs[i], c);
                }
            }
            else
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "rainbow-tatl");
            }
        }

        private void Add5NutsToField(int replacement_slot = 0xC444B8)
        {
            // add 5 x single nuts to drop table for the termina field
            // replacement drop slot  will become deku nut, gives us 1/16 chance of a nut
            int fid = RomUtils.GetFileIndexForWriting(replacement_slot);
            RomUtils.CheckCompressed(fid);
            int offset = replacement_slot - RomData.MMFileList[fid].Addr;
            RomData.MMFileList[fid].Data[offset] = 0x0C; // 0x0C is deku nut
            RomData.MMFileList[fid].Data[offset+0x110] = 0x05; // this should change the amount dropped to 5
        }

        private void AddSingleStickToField(int replacement_slot = 0xC444C1)
        {
            // add single nut to drop table for the termina field
            // replacement drop slot will become single stick, gives us 1/16 chance of a stick
            int fid = RomUtils.GetFileIndexForWriting(replacement_slot);
            RomUtils.CheckCompressed(fid);
            int offset = replacement_slot - RomData.MMFileList[fid].Addr;
            RomData.MMFileList[fid].Data[offset] = 0x0D; // 0x0D is deku stick
            RomData.MMFileList[fid].Data[offset + 0x110] = 0x02; // this should change the amount dropped to 5
        }

        private void WriteQuickText()
        {
            if (_randomized.Settings.QuickTextEnabled)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "quick-text");
            }
        }

        private void WriteCutscenes()
        {
            if (_randomized.Settings.ShortenCutscenes)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "short-cutscenes");
            //}
            // if (_randomized.Settings.RemoveTatlInterrupts)
            //{
                ResourceUtils.ApplyHack(Values.ModsDirectory, "remove-tatl-interrupts");
            }
        }

        private void WriteEntrances(OutputSettings settings)
        {
            if (_randomized.Settings.LogicMode == LogicMode.Vanilla || _randomized.Settings.EntranceLogicMode == LogicMode.Vanilla || !_randomized.Settings.RandomizedEntrances.Any())
            {

                StringBuilder log = new StringBuilder();
                void WriteOutput(string str)
                {
                    Debug.WriteLine(str);
                    log.AppendLine(str);
                }

                // how could this happen

                // EntranceSwapUtils.WriteNewEntrance(item.NewLocation.Value, item.Item);
                SceneUtils.ReadSceneTable();
                SceneUtils.GetMaps();

                // if you somehow talk to bass guy and do his thing, you get a secret path to the moon
                // I just think secrets are neat
                //EntranceSwapUtils.WriteNewEntrance(Item.EntranceZoraHallRoomsJapasRoomFromJapasRoom, Item.EntranceTheMoonFromLinkTrial);
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceZoraHallRoomsJapasRoomFromJapasRoom, Item.EntranceZoraTrialFromTheMoon);

                // hardcode actual dungeons, randomize the rest
                // woodfall
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceDekuPalaceGardenEastFromPalaceVinesGrotto, Item.EntranceWoodfallFromWoodfallTempleEntrance);
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceWoodfallTempleFromWoodfall, Item.EntranceGrottoPalaceVinesFromDekuPalaceLower); //fake out ent
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceWoodfallFromWoodfallTempleEntrance, Item.EntranceRomaniRanchFromRanchHouse);
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceRanchHouseFromRomaniRanch, Item.EntranceWoodfallTempleFromWoodfall);            // actual ent

                //GBT
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceGreatBayTempleFromZoraCape, Item.EntrancePiratesFortressFromTelescope);         //fake out ent
                EntranceSwapUtils.WriteNewEntrance(Item.EntrancePiratesFortressSewerFromTelescope, Item.EntranceZoraCapeFromGreatBayTemple);
                EntranceSwapUtils.WriteNewEntrance(Item.EntrancePiratesFortressExteriorFromPiratesFortressBalcony, Item.EntranceGreatBayTempleFromZoraCape); // actual ent
                EntranceSwapUtils.WriteNewEntrance(Item.EntranceZoraCapeFromGreatBayTemple, Item.EntrancePiratesFortressFromPiratesFortressExteriorBalcony);

                //    Item.EntranceGreatBayCoastFromPinnacleRock, Item.EntrancePinnacleRockFromGreatBayCoast,                                            
                // not doing clocktower because its the easist to hit by accident, also the entrance is default 
                // if I had to choose special places, I would put GBT->pirates balcony and WF->WFGF or WF->
                var enlist = new List<Item>() { Item.EntranceZoraHallFromMikauTijosRoom, Item.EntranceZoraHallRoomsMikauTijosRoomFromZoraHall,
                                                Item.EntranceFairysFountainFromWoodfall, Item.EntranceWoodfallFromFairysFountain,
                                                Item.EntranceSnowheadFromFairysFountain, Item.EntranceFairysFountainFromSnowhead,
                                                Item.EntranceZoraCapeFromFairysFountain, Item.EntranceFairysFountainFromZoraCape,
                                                Item.EntranceFairysFountainFromIkanaCanyon, Item.EntranceIkanaCanyonFromFairysFountain,
                                                Item.EntranceLotteryShopFromWestClockTown, Item.EntranceWestClockTownFromLotteryShop,
                                                Item.EntranceZoraHallRoomsMikauTijosRoomFromZoraHall, Item.EntranceZoraHallFromMikauTijosRoom,
                                                Item.EntranceSakonsHideoutFromIkanaCanyon, Item.EntranceIkanaCanyonFromSakonsHideout,
                                                Item.EntranceGrottoPalaceStraightFromDekuPalaceB, Item.EntranceDekuPalaceGardenWestFromPalaceStraightGrotto,
                                                Item.EntranceIkanaGraveyardFromDampesHouse, Item.EntranceDampesHouseFromIkanaGraveyardDoor,
                                                Item.EntranceCuriosityShopFromKafeisHideout, Item.EntranceKafeisHideoutFromCuriosityShop,
                                                Item.EntranceBeneaththeWellFromIkanaCanyon, Item.EntranceIkanaCanyonFromBeneaththeWell,
                                                Item.EntranceGormanTrackFromMilkRoadGated, Item.EntranceMilkRoadFromGormanRacetrackTrack,
                                                Item.EntranceMusicBoxHouseFromIkanaCanyon, Item.EntranceIkanaCanyonFromMusicBoxHouse,
                                                Item.EntranceIkanaCanyonFromSpringWaterCave, Item.EntranceSpringWaterCaveFromIkanaCanyon,
                                                Item.EntranceClockTowerInteriorFromBeforethePortaltoTermina, Item.EntranceBeforethePortaltoTerminaFromClockTowerInterior,
                                                Item.EntranceCuriosityShopFromKafeisHideout, Item.EntranceKafeisHideoutFromCuriosityShop

                };
                //EntranceSwapUtils.WriteNewEntrance(Item.EntranceNorthClockTownFromDekuScrubPlayground, Item.EntranceLostWoodsFrom);

                var rand = new Random();
                var exlist = enlist.OrderBy(x => rand.Next()).ToList();

                for(int i = 0; i < enlist.Count; i++)
                {
                    EntranceSwapUtils.WriteNewEntrance(enlist[i], exlist[i]);
                    string important = exlist[i].ToString().Contains("Temple") ? " ****" : " ";
                    WriteOutput("Entrando :" + enlist[i].ToString() + " -> " + exlist[i].ToString() + important);
                }

                using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename + "_PlandoLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer
                    sw.Write(log);
                }

                return;
            }

            var newSpawn = _randomized.ItemList.Single(io => io.NewLocation == Item.EntranceSouthClockTownFromClockTowerInterior).Item;
            EntranceSwapUtils.WriteSpawnToROM(newSpawn);

            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();

            foreach (var item in _randomized.ItemList.Where(io => io.Item.IsEntrance() && io.IsRandomized))
            {
                EntranceSwapUtils.WriteNewEntrance(item.NewLocation.Value, item.Item);
            }
            EntranceSwapUtils.WriteOwlRegionNameTable( _randomized.ItemList );

            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-drown-timer");
            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-skyboxes");
            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-spring-lens-cave-spawn");
            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-poisoned-woodfall-spawns");
            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-song-of-soaring-exits"); // todo maybe NOP all the code, instead of just the SH commands.

            if (_randomized.ItemList.Any(io => io.IsRandomized && io.Item.IsEntrance() && io.Item.Region() == Region.TheMoon))
            {
                SceneUtils.SetSceneTimeSettingsToDefault(GameObjects.Scene.TheMoon);
                SceneUtils.SetSceneTimeSettingsToDefault(GameObjects.Scene.DekuTrial);
                SceneUtils.SetSceneTimeSettingsToDefault(GameObjects.Scene.GoronTrial);
                SceneUtils.SetSceneTimeSettingsToDefault(GameObjects.Scene.ZoraTrial);
                SceneUtils.SetSceneTimeSettingsToDefault(GameObjects.Scene.LinkTrial);
            }
        }

        private void WriteDungeons()
        {
            if ((_randomized.Settings.LogicMode == LogicMode.Vanilla) || (!_randomized.Settings.RandomizeDungeonEntrances))
            {
                return;
            }

            EntranceUtils.WriteEntrances(Values.OldEntrances.ToArray(), _randomized.NewEntrances);
            EntranceUtils.WriteEntrances(Values.OldExits.ToArray(), _randomized.NewExits);
            byte[] li = new byte[] { 0x24, 0x02, 0x00, 0x00 };
            List<int[]> addr = new List<int[]>();
            addr = ResourceUtils.GetAddresses(Values.AddrsDirectory, "d-check");
            for (int i = 0; i < addr.Count; i++)
            {
                li[3] = (byte)_randomized.NewExitIndices[i];
                ReadWriteUtils.WriteROMAddr(addr[i], li);
            }

            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-dungeons");
            addr = ResourceUtils.GetAddresses(Values.AddrsDirectory, "d-exit");

            for (int i = 0; i < addr.Count; i++)
            {
                if (i == 2)
                {
                    ReadWriteUtils.WriteROMAddr(addr[i], new byte[] {
                        (byte)((Values.OldExits[_randomized.NewDestinationIndices[i + 1]] & 0xFF00) >> 8),
                        (byte)(Values.OldExits[_randomized.NewDestinationIndices[i + 1]] & 0xFF) });
                }
                else
                {
                    ReadWriteUtils.WriteROMAddr(addr[i], new byte[] {
                        (byte)((Values.OldExits[_randomized.NewDestinationIndices[i]] & 0xFF00) >> 8),
                        (byte)(Values.OldExits[_randomized.NewDestinationIndices[i]] & 0xFF) });
                }
            }

            addr = ResourceUtils.GetAddresses(Values.AddrsDirectory, "dc-flagload");
            for (int i = 0; i < addr.Count; i++)
            {
                ReadWriteUtils.WriteROMAddr(addr[i], new byte[] { (byte)((_randomized.NewDCFlags[i] & 0xFF00) >> 8), (byte)(_randomized.NewDCFlags[i] & 0xFF) });
            }

            addr = ResourceUtils.GetAddresses(Values.AddrsDirectory, "dc-flagmask");
            for (int i = 0; i < addr.Count; i++)
            {
                ReadWriteUtils.WriteROMAddr(addr[i], new byte[] {
                    (byte)((_randomized.NewDCMasks[i] & 0xFF00) >> 8),
                    (byte)(_randomized.NewDCMasks[i] & 0xFF) });
            }
        }

        private void WriteSpeedUps()
        {
            if (_randomized.Settings.SpeedupBeavers)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "speedup-beavers");
                _messageTable.UpdateMessages(new MessageEntry
                {
                    Id = 0x10D6,
                    Header = null,
                    Message = "\u001E\u0029\u001AThere's a total of \u000125 rings\u0000. You must swim through them in the right order for it to count. Swim through the ring that's \u0001flashing\u0000.".Wrap(35, "\u0011") + "\u0010My big brother will show you the way, so follow him and don't get separated!\u00BF".Wrap(35, "\u0011")
                });
                _messageTable.UpdateMessages(new MessageEntry
                {
                    Id = 0x10FA,
                    Header = null,
                    Message = "\u001E\u0029\u0019This time, the limit is \u00011:50\u0000.".EndTextbox() + "Don't fall behind!\u00BF"
                });
                _messageTable.UpdateMessages(new MessageEntry
                {
                    Id = 0x1107,
                    Header = null,
                    Message = "\u001E\u0029\u0019This time around, you have to beat \u00011:40\u0000.".Wrap(35, "\u0011").EndTextbox() + "Don't fall behind!\u00BF"
                });
            }

            if (_randomized.Settings.SpeedupDampe)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "speedup-dampe");
            }

            if (_randomized.Settings.SpeedupLabFish)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "speedup-labfish");
            }

            if (_randomized.Settings.SpeedupDogRace)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "speedup-dograce");
            }

            if (_randomized.Settings.SpeedupBank)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "speedup-bank");
                _messageTable.UpdateMessages(new MessageEntry
                {
                    Id = 0x45C,
                    Header = null,
                    Message = "\u0017What's this? You've already saved\u0011up \u0001500 Rupees\u0000!?!\u0018\u0011\u0013\u0012Well, little guy, here's your special\u0011gift. Take it!\u00E0\u00BF",
                });
                _messageTable.UpdateMessages(new MessageEntry
                {
                    Id = 0x45D,
                    Header = null,
                    Message = "\u0017What's this? You've already saved\u0011up \u00011000 Rupees\u0000?!\u0018\u0011\u0013\u0012Well, little guy, I can't take any\u0011more deposits. Sorry, but this is\u0011all I can give you.\u00E0\u00BF",
                });
            }
        }

        private void WriteGimmicks()
        {
            int damageMultiplier = (int)_randomized.Settings.DamageMode;
            if (damageMultiplier > 0)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "dm-" + damageMultiplier.ToString());
            }

            int damageEffect = (int)_randomized.Settings.DamageEffect;
            if (damageEffect > 0)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "de-" + damageEffect.ToString());
            }

            int gravityType = (int)_randomized.Settings.MovementMode;
            if (gravityType > 0)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "movement-" + gravityType.ToString());
            }

            int floorType = (int)_randomized.Settings.FloorType;
            if (floorType > 0)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "floor-" + floorType.ToString());
            }

            if (_randomized.Settings.ClockSpeed != ClockSpeed.Default)
            {
                WriteClockSpeed(_randomized.Settings.ClockSpeed);
            }

            if (_randomized.Settings.HideClock)
            {
                WriteHideClock();
            }

            if (_randomized.Settings.BlastMaskCooldown != BlastMaskCooldown.Default)
            {
                WriteBlastMaskCooldown();
            }

            if (_randomized.Settings.EnableSunsSong)
            {
                WriteSunsSong();
            }

            if (_randomized.Settings.AllowFierceDeityAnywhere)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fierce-deity-anywhere");
            }

            if (_randomized.Settings.ByoAmmo)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "byo-ammo");
            }

            if (_randomized.Settings.DeathMoonCrash)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "death-moon-crash");
            }
        }

        private void WriteSunsSong()
        {
            _messageTable.UpdateMessages(new MessageEntry
            {
                Id = 0x1B7D,
                Header = new byte[11] { 0x03, 0x00, 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                Message = $"You played the {TextCommands.ColorYellow}Sun's Song{TextCommands.ColorWhite}!\xBF"
            });

            ResourceUtils.ApplyHack(Values.ModsDirectory, "enable-sunssong");
        }

        private void WriteBlastMaskCooldown()
        {
            ushort value;
            switch (_randomized.Settings.BlastMaskCooldown)
            {
                default:
                case BlastMaskCooldown.Default:
                    value = 0x136; // 310 frames 
                    break;
                case BlastMaskCooldown.Instant:
                    value = 0x1; // 1 frame
                    break;
                case BlastMaskCooldown.VeryShort:
                    value = 0x20; // 32 frames
                    break;
                case BlastMaskCooldown.Short:
                    value = 0x80; // 128 frames
                    break;
                case BlastMaskCooldown.Long:
                    value = 0x200; // 512 frames
                    break;
                case BlastMaskCooldown.VeryLong:
                    value = 0x400; // 1024 frames
                    break;
            }

            var codeFileAddress = 0x00CA7F00;
            var offset = 0x002766;
            ReadWriteUtils.WriteToROM(codeFileAddress + offset, value);
        }

        private void WriteHideClock()
        {
            var codeFileAddress = 0xB3C000;
            var offset = 0x73B7C; // branch for UI is time hasn't changed
            ReadWriteUtils.WriteToROM(codeFileAddress + offset, 0x10); // change to always branch
        }

        /// <summary>
        /// Overwrite the clockspeed (see Settings.ClockSpeed for details)
        /// </summary>
        /// <param name="clockSpeed"></param>
        private void WriteClockSpeed(ClockSpeed clockSpeed)
        {
            byte speed;
            short invertedModifier;
            switch (clockSpeed)
            {
                default:
                case ClockSpeed.Default:
                    speed = 3;
                    invertedModifier = -2;
                    break;
                case ClockSpeed.VerySlow:
                    speed = 1;
                    invertedModifier = 0;
                    break;
                case ClockSpeed.Slow:
                    speed = 2;
                    invertedModifier = -1;
                    break;
                case ClockSpeed.Fast:
                    speed = 6;
                    invertedModifier = -4;
                    break;
                case ClockSpeed.VeryFast:
                    speed = 9;
                    invertedModifier = -6;
                    break;
                case ClockSpeed.SuperFast:
                    speed = 18;
                    invertedModifier = -12;
                    break;
            }

            ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-clock-speed");

            var codeFileAddress = 0xB3C000;
            var hackAddressOffset = 0x8A674;
            var modificationOffset = 0x1B;
            ReadWriteUtils.WriteToROM(codeFileAddress + hackAddressOffset + modificationOffset, speed);

            var invertedModifierOffsets = new List<int>
            {
                0xB1B8E,
                0x7405E
            };
            foreach (var offset in invertedModifierOffsets)
            {
                ReadWriteUtils.WriteToROM(codeFileAddress + offset, (ushort)invertedModifier);
            }
        }

        /// <summary>
        /// Update the gossip stone actor to not check mask of truth
        /// </summary>
        private void WriteFreeHints()
        {
            int address = 0x00E0A810 + 0x378;
            byte val = 0x00;
            ReadWriteUtils.WriteToROM(address, val);
        }

        private void WriteSoundEffects(Random random)
        {
            if (!_cosmeticSettings.RandomizeSounds)
            {
                return;
            }

            var shuffledSoundEffects = new Dictionary<SoundEffect, SoundEffect>();

            var replacableSounds = SoundEffects.Replacable();
            foreach (var sound in replacableSounds)
            {
                var soundPool = SoundEffects.FilterByTags(sound.ReplacableByTags());

                if (soundPool.Count > 0)
                {
                    shuffledSoundEffects[sound] = soundPool.Random(random);
                }
            }

            foreach (var sounds in shuffledSoundEffects)
            {
                var oldSound = sounds.Key;
                var newSound = sounds.Value;

                if (oldSound.IsReplacableInMessage())
                {
                    oldSound.ReplaceInMessageWith(newSound, _messageTable);
                }
                else
                {
                    oldSound.ReplaceWith(newSound);
                }
                Debug.WriteLine($"Writing SFX {newSound} --> {oldSound}");
            }
        }

        private void WriteMutedLowHeartBeep()
        {
            if (_cosmeticSettings.DisableLowHealthBeep)
            {
                SoundEffect.LowHealthBeep.ReplaceWith(SoundEffect.EmptySFX);
            }
        }

        private void SoundEffectShuffle()
        {
        }

        private void WriteEnemies()
        {
            if (_randomized.Settings.RandomizeEnemies)
            {
                Enemies.ShuffleEnemies(new Random(_randomized.Seed));
            }
        }

        private void PutOrCombine(Dictionary<int, byte> dictionary, int key, byte value, bool add = false)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = 0;
            }
            dictionary[key] = add ? (byte)(dictionary[key] + value) : (byte)(dictionary[key] | value);
        }

        private void WriteFreeItems(params Item[] items)
        {
            Dictionary<int, byte> startingItems = new Dictionary<int, byte>();
            if (_randomized.Settings.EnableSunsSong)
            {
                PutOrCombine(startingItems, 0xC5CE71, 0x02);
            }

            var itemList = items.ToList();

            if (_randomized.Settings.CustomStartingItemList != null)
            {
                itemList.AddRange(_randomized.Settings.CustomStartingItemList);
            }

            itemList.Add(Item.StartingHeartContainer1);
            while (itemList.Count(item => item.Name() == "Piece of Heart") >= 4)
            {
                itemList.Add(Item.StartingHeartContainer1);
                for (var i = 0; i < 4; i++)
                {
                    var heartPiece = itemList.First(item => item.Name() == "Piece of Heart");
                    itemList.Remove(heartPiece);
                }
            }

            itemList = itemList
                .GroupBy(item => ItemUtils.ForbiddenStartTogether.FirstOrDefault(fst => fst.Contains(item)))
                .SelectMany(g => g.Key == null ? g.ToList() : g.OrderByDescending(item => g.Key.IndexOf(item)).Take(1))
                .ToList();

            foreach (var item in itemList)
            {
                var startingItemValues = item.GetAttributes<StartingItemAttribute>();
                if (!startingItemValues.Any() && !_randomized.Settings.NoStartingItems)
                {
                    throw new Exception($@"Invalid starting item ""{item}""");
                }
                foreach (var startingItem in startingItemValues)
                {
                    PutOrCombine(startingItems, startingItem.Address, startingItem.Value, startingItem.IsAdditional);
                }
            }

            foreach (var kvp in startingItems)
            {
                ReadWriteUtils.WriteToROM(kvp.Key, kvp.Value);
            }

            if (itemList.Count(item => item.Name() == "Heart Container") == 1)
            {
                ReadWriteUtils.WriteToROM(0x00B97E8F, 0x0C); // reduce low health beep threshold
            }
        }

        private void WriteItems()
        {
            var freeItems = new List<Item>();
            if (_randomized.Settings.LogicMode == LogicMode.Vanilla)
            {
                freeItems.Add(Item.FairyMagic);
                freeItems.Add(Item.MaskDeku);
                freeItems.Add(Item.SongHealing);
                freeItems.Add(Item.StartingSword);
                freeItems.Add(Item.StartingShield);
                freeItems.Add(Item.StartingHeartContainer1);
                freeItems.Add(Item.StartingHeartContainer2);

                if (_randomized.Settings.ShortenCutscenes)
                {
                    //giants cs were removed
                    freeItems.Add(Item.SongOath);
                }

                WriteFreeItems(freeItems.ToArray());

                return;
            }

            //write free item (start item default = Deku Mask)
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.MaskDeku).Item);
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.SongHealing).Item);
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.StartingSword).Item);
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.StartingShield).Item);
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.StartingHeartContainer1).Item);
            freeItems.Add(_randomized.ItemList.Find(u => u.NewLocation == Item.StartingHeartContainer2).Item);
            WriteFreeItems(freeItems.ToArray());

            //write everything else
            ItemSwapUtils.ReplaceGetItemTable();
            ItemSwapUtils.InitItems();

            // Write extended object indexes to Get-Item list entries.
            WriteExtendedObjects();

            if (_randomized.Settings.FixEponaSword)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-epona");
            }
            if (_randomized.Settings.PreventDowngrades)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-downgrades");
            }
            if (_randomized.Settings.AddCowMilk)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-cow-bottle-check");
            }

            ResourceUtils.ApplyHack(Values.ModsDirectory, "update-trade-scrubs");

            var newMessages = new List<MessageEntry>();
            foreach (var item in _randomized.ItemList)
            {
                // Unused item
                if (item.NewLocation == null)
                {
                    continue;
                }

                if (item.Item.ToString().StartsWith("OwlActivation"))
                {
                    continue;
                }

                if (item.Item.IsEntrance())
                {
                    continue;
                }

                if (item.Item.IsBottleCatchContent())
                {
                    ItemSwapUtils.WriteNewBottle(item.NewLocation.Value, item.Item);
                }
                else
                {
                    ChestTypeAttribute.ChestType? overrideChestType = null;
                    if ((item.Item.Name().Contains("Bombchu") || item.Item.Name().Contains("Shield")) && _randomized.Logic.Any(il => il.RequiredItemIds?.Contains(item.ID) == true || il.ConditionalItemIds?.Any(c => c.Contains(item.ID)) == true))
                    {
                        overrideChestType = ChestTypeAttribute.ChestType.LargeGold;
                    }
                    ItemSwapUtils.WriteNewItem(
                        item.NewLocation.Value,
                        item.Item, newMessages,
                        _randomized.Settings.UpdateShopAppearance,
                        _randomized.Settings.PreventDowngrades,
                        _randomized.Settings.UpdateChests && item.IsRandomized,
                        item.Mimic?.ChestType ?? overrideChestType,
                        _randomized.Settings.CustomStartingItemList.Contains(item.Item),
                        _randomized.Settings.QuestItemStorage,
                        item.Mimic
                    );
                }
            }

            var copyRupeesRegex = new Regex(": [0-9]+ Rupees");
            foreach (var newMessage in newMessages)
            {
                var oldMessage = _messageTable.GetMessage(newMessage.Id);
                if (oldMessage != null)
                {
                    var cost = copyRupeesRegex.Match(oldMessage.Message).Value;
                    newMessage.Message = copyRupeesRegex.Replace(newMessage.Message, cost);
                }
            }

            if (_randomized.Settings.UpdateShopAppearance)
            {
                // update tingle shops
                foreach (var messageShopText in Enum.GetValues(typeof(MessageShopText)).Cast<MessageShopText>())
                {
                    var messageShop = messageShopText.GetAttribute<MessageShopAttribute>();
                    var item1 = _randomized.ItemList.First(io => io.NewLocation == messageShop.Items[0]);
                    var item2 = _randomized.ItemList.First(io => io.NewLocation == messageShop.Items[1]);
                    newMessages.Add(new MessageEntry
                    {
                        Id = (ushort)messageShopText,
                        Header = null,
                        Message = string.Format(messageShop.MessageFormat, item1.DisplayName + " ", messageShop.Prices[0], item2.DisplayName + " ", messageShop.Prices[1])
                    });
                }

                // update business scrub
                var businessScrubItem = _randomized.ItemList.First(io => io.NewLocation == Item.HeartPieceTerminaBusinessScrub);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1631,
                    Header = null,
                    Message = $"\x1E\x3A\xD2Please! I'll sell you {businessScrubItem.GetArticle()}\u0001{businessScrubItem.DisplayName}\u0000 if you just keep this place a secret...\x19\xBF".Wrap(35, "\u0011")
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1632,
                    Header = null,
                    Message = $"\u0006150 Rupees\u0000 for{businessScrubItem.GetPronounOrAmount().ToLower()}!\u0011 \u0011\u0002\u00C2I'll buy {businessScrubItem.GetPronoun()}\u0011No thanks\u00BF"
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1634,
                    Header = null,
                    Message = $"What about{businessScrubItem.GetPronounOrAmount("").ToLower()} for \u0006100 Rupees\u0000?\u0011 \u0011\u0002\u00C2I'll buy {businessScrubItem.GetPronoun()}\u0011No thanks\u00BF"
                });

                // update biggest bomb bag purchase
                var biggestBombBagItem = _randomized.ItemList.First(io => io.NewLocation == Item.UpgradeBiggestBombBag);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x15F5,
                    Header = null,
                    Message = $"I sell {biggestBombBagItem.GetArticle()}\u0001{biggestBombBagItem.GetAlternateName()}\u0000, but I'm focusing my marketing efforts on \u0001Gorons\u0000.".Wrap(35, "\u0011").EndTextbox() + "What I'd really like to do is go back home and do business where I'm surrounded by trees and grass.\u0019\u00BF".Wrap(35, "\u0011")
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x15FF,
                    Header = null,
                    Message = $"\x1E\x39\x8CRight now, I've got a \u0001special\u0011\u0000offer just for you.\u0019\u00BF"
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1600,
                    Header = null,
                    Message = $"\x1E\x38\x81I'll give you {biggestBombBagItem.GetArticle("my ")}\u0001{biggestBombBagItem.DisplayName}\u0000, regularly priced at \u00061000 Rupees\u0000...".Wrap(35, "\u0011").EndTextbox() + "In return, you'll give me just\u0011\u0006200 Rupees\u0000!\u0019\u00BF"
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1606,
                    Header = null,
                    Message = $"\x1E\x38\x81I'll give you {biggestBombBagItem.GetArticle("my ")}\u0001{biggestBombBagItem.DisplayName}\u0000, regularly priced at \u00061000 Rupees\u0000, for just \u0006200 Rupees\u0000!\u0019\u00BF".Wrap(35, "\u0011")
                });

                // update swamp scrub purchase
                var magicBeanItem = _randomized.ItemList.First(io => io.NewLocation == Item.ShopItemBusinessScrubMagicBean);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x15E1,
                    Header = null,
                    Message = $"\x1E\x39\xA7I'm selling {magicBeanItem.GetArticle()}\u0001{magicBeanItem.GetAlternateName()}\u0000 to Deku Scrubs, but I'd really like to leave my hometown.".Wrap(35, "\u0011").EndTextbox() + "I'm hoping to find some success in a livelier place!\u0019\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x15E9,
                    Header = null,
                    Message = $"\x1E\x3A\u00D2Do you know what {magicBeanItem.GetArticle()}\u0001{magicBeanItem.GetAlternateName()}\u0000 {magicBeanItem.GetVerb()}, sir?".Wrap(35, "\u0011") + $"\u0011I'll sell you{magicBeanItem.GetPronounOrAmount().ToLower()} for \u000610 Rupees\u0000.\u0019\u00BF"
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x15F3,
                    Header = null,
                    Message = $"\x1E\x3A\u00D2Do you know what {magicBeanItem.GetArticle()}\u0001{magicBeanItem.GetAlternateName()}\u0000 {magicBeanItem.GetVerb()}?".Wrap(35, "\u0011") + $"\u0011I'll sell you{magicBeanItem.GetPronounOrAmount().ToLower()} for \u000610 Rupees\u0000.\u0019\u00BF"
                });

                // update ocean scrub purchase
                var greenPotionItem = _randomized.ItemList.First(io => io.NewLocation == Item.ShopItemBusinessScrubGreenPotion);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1608,
                    Header = null,
                    Message = $"\x1E\x39\xA7I'm selling {greenPotionItem.GetArticle()}\u0001{greenPotionItem.GetAlternateName()}\u0000, but I'm focusing my marketing efforts on Zoras.".Wrap(35, "\u0011").EndTextbox() + "Actually, I'd like to do business someplace where it's cooler and the air is clean.\u0019\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x1612,
                    Header = null,
                    Message = $"\x1E\x39\x8CI'll sell you {greenPotionItem.GetArticle()}\u0001{greenPotionItem.DisplayName}\u0000 for \u000640 Rupees\u0000!\u00E0\u00BF".Wrap(35, "\u0011")
                });

                var coldifyRegex = new Regex("([A-Z])");
                var coldItemName = coldifyRegex.Replace(greenPotionItem.DisplayName, "$1-$1");
                newMessages.Add(new MessageEntry
                {
                    Id = 0x1617,
                    Header = null,
                    Message = $"\x1E\x39\x8CI'll s-sell you {greenPotionItem.GetArticle()}\u0001{coldItemName}\u0000 for \u000640 Rupees\u0000.\u00E0\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x1618,
                    Header = null,
                    Message = $"D-Do we have a deal?\u0011 \u0011\u0002\u00C2Yes\u0011No\u00BF"
                });

                // update canyon scrub purchase
                var bluePotionItem = _randomized.ItemList.First(io => io.NewLocation == Item.ShopItemBusinessScrubBluePotion);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x161C,
                    Header = null,
                    Message = $"\x1E\x39\xA7I'm here to sell {bluePotionItem.GetArticle()}\u0001{bluePotionItem.GetAlternateName()}\u0000.".Wrap(35, "\u0011").EndTextbox() + "Actually, I want to do business in the sea breeze while listening to the sound of the waves.\u0019\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x1626,
                    Header = null,
                    Message = $"\x1E\x3A\u00D2Don't you need {bluePotionItem.GetArticle()}\u0001{bluePotionItem.GetAlternateName()}\u0000? I'll sell you{bluePotionItem.GetPronounOrAmount().ToLower()} for \u0006100 Rupees\u0000.\u0019\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x162D,
                    Header = null,
                    Message = $"\x1E\x39\x8CI'll sell you {bluePotionItem.GetArticle()}\u0001{bluePotionItem.DisplayName}\u0000 for \u0006100 Rupees\u0000.\u00E0\u00BF".Wrap(35, "\u0011")
                });

                newMessages.Add(new MessageEntry
                {
                    Id = 0x15EA,
                    Header = null,
                    Message = $"Do we have a deal?\u0011 \u0011\u0002\u00C2Yes\u0011No\u00BF"
                });

                // update gorman bros milk purchase
                var gormanBrosMilkItem = _randomized.ItemList.First(io => io.NewLocation == Item.ShopItemGormanBrosMilk);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x3463,
                    Header = null,
                    Message = $"Won'tcha buy {gormanBrosMilkItem.GetArticle()}\u0001{gormanBrosMilkItem.GetAlternateName()}\u0000?\u0019\u00BF".Wrap(35, "\u0011")
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x3466,
                    Header = null,
                    Message = $"\u000650 Rupees\u0000 will do ya for{gormanBrosMilkItem.GetPronounOrAmount().ToLower()}.\u0011 \u0011\u0002\u00C2I'll buy {gormanBrosMilkItem.GetPronoun()}\u0011No thanks\u00BF"
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x346B,
                    Header = null,
                    Message = $"Buyin' {gormanBrosMilkItem.GetArticle()}\u0001{gormanBrosMilkItem.GetAlternateName()}\u0000?\u0019\u00BF".Wrap(35, "\u0011")
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x348F,
                    Header = null,
                    Message = $"Seems like we're the only ones who have {gormanBrosMilkItem.GetArticle()}\u0001{gormanBrosMilkItem.GetAlternateName()}\u0000. Hyuh, hyuh. If you like, I'll sell you{gormanBrosMilkItem.GetPronounOrAmount().ToLower()}.\u0019\u00BF".Wrap(35, "\u0011")
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x3490,
                    Header = null,
                    Message = $"\u000650 Rupees\u0000 will do you for{gormanBrosMilkItem.GetPronounOrAmount().ToLower()}!\u0011 \u0011\u0002\u00C2I'll buy {gormanBrosMilkItem.GetPronoun()}\u0011No thanks\u00BF"
                });

                // update lottery message
                var lotteryItem = _randomized.ItemList.First(io => io.NewLocation == Item.MundaneItemLotteryPurpleRupee);
                newMessages.Add(new MessageEntry
                {
                    Id = 0x2B5C,
                    Header = null,
                    Message = $"Would you like the chance to buy your dreams for \u000610 Rupees\u0000?".Wrap(35, "\u0011").EndTextbox() + $"Pick any three numbers, and if those are picked, you'll win {lotteryItem.GetArticle()}\u0001{lotteryItem.DisplayName}\u0000. It's only for the \u0001first\u0000 person!\u0019\u00BF".Wrap(35, "\u0011")
                });

                // Update messages to match updated world models.
                if (_randomized.Settings.UpdateWorldModels)
                {
                    // Update Moon's Tear messages.
                    var moonsTearItem = _randomized.ItemList.First(io => io.NewLocation == Item.TradeItemMoonTear);
                    if (moonsTearItem.Item != Item.TradeItemMoonTear)
                    {
                        newMessages.Add(new MessageEntry
                        {
                            Id = 0x5E3,
                            Header = null,
                            Message = $"That is one of the lunar objects\u0011that has been blazing from the\u0011surface of the moon lately.".EndTextbox() + $"They fall from what looks to be the moon's eye, I call {moonsTearItem.GetPronoun()} {moonsTearItem.GetArticle()}\u0001{moonsTearItem.DisplayName}\u0000.".Wrap(35, "\u0011").EndTextbox() + $"They are rare, valued by many\u0011in town.\u00BF"
                        });
                        newMessages.Add(new MessageEntry
                        {
                            Id = 0x5ED,
                            Header = null,
                            Message = $"That ill-mannered troublemaker\u0011from the other day said he'd\u0011break my instruments...".EndTextbox() + $"He said he'd steal my \u0001{moonsTearItem.DisplayName}\u0000... There was no stopping him.\u0019\u00BF".Wrap(35, "\u0011")
                        });
                        newMessages.Add(new MessageEntry
                        {
                            Id = 0x5F2,
                            Header = null,
                            Message = $"Well, did you find that\u0011\u0001troublemaker\u0000? And that loud\u0011noise...What was that?".EndTextbox() + $"Perhaps another \u0001{moonsTearItem.DisplayName}\u0000 has fallen nearby...Go through that door and take a look outside.\u0019\u00BF".Wrap(35, "\u0011")
                        });
                    }

                    // Update Seahorse messages.
                    var seahorseItem = _randomized.ItemList.First(io => io.NewLocation == Item.MundaneItemSeahorse);
                    if (seahorseItem.Item != Item.MundaneItemSeahorse)
                    {
                        newMessages.Add(new MessageEntry
                        {
                            Id = 0x106F,
                            Header = null,
                            Message = $"\u001E\u0069\u004CAre you interested in that?".EndTextbox() + $"It's rare, isn't it? It's called {seahorseItem.GetArticle()}\u0001{seahorseItem.DisplayName}\u0000.\u0019\u00BF".Wrap(35, "\u0011")
                        });
                        newMessages.Add(new MessageEntry
                        {
                            Id = 0x1074,
                            Header = null,
                            Message = $"If you want that \u0001{seahorseItem.DisplayName}\u0000, bring me a \u0001pictograph\u0000 of a \u0001female pirate\u0000.\u0019\u00BF".Wrap(35, "\u0011")
                        });
                    }
                }
            }

            // replace "Razor Sword is now blunt" message with get-item message for Kokiri Sword.
            newMessages.Add(new MessageEntry
            {
                Id = 0xF9,
                Header = new byte[11] { 0x06, 0x00, 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                Message = $"You got the \x01Kokiri Sword\x00!\u0011This is a hidden treasure of\u0011the Kokiri, but you can borrow it\u0011for a while.\u00BF",
            });

            // replace Magic Power message
            newMessages.Add(new MessageEntry
            {
                Id = 0xC8,
                Header = null,
                Message = $"\u0017You've been granted \u0002Magic Power\u0000!\u0018\u0011Replenish it with \u0001Magic Jars\u0000\u0011and \u0001Potions\u0000.\u00BF",
            });

            if (_randomized.Settings.AddSkulltulaTokens)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-skulltula-tokens");

                newMessages.Add(new MessageEntry
                {
                    Id = 0x51,
                    Header = new byte[11] { 0x02, 0x00, 0x52, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                    Message = $"\u0017You got an \u0005Ocean Gold Skulltula\u0011Spirit\0!\u0018\u001F\u0000\u0010 This is your \u0001\u000D\u0000 one!\u00BF",
                });
                newMessages.Add(new MessageEntry
                {
                    Id = 0x52,
                    Header = new byte[11] { 0x02, 0x00, 0x52, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                    Message = $"\u0017You got a \u0006Swamp Gold Skulltula\u0011Spirit\0!\u0018\u001F\u0000\u0010 This is your \u0001\u000D\u0000 one!\u00BF",
                });
            }

            if (_randomized.Settings.AddStrayFairies)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-fairies");
            }

            var dungeonItemMessageIds = new byte[] {
                0x3C, 0x3D, 0x3E, 0x3F, 0x74,
                0x40, 0x4D, 0x4E, 0x53, 0x75,
                0x54, 0x61, 0x64, 0x6E, 0x76,
                0x70, 0x71, 0x72, 0x73, 0x77,
            };

            var dungeonNames = new string[]
            {
                "\u0006Woodfall Temple\u0000",
                "\u0002Snowhead Temple\u0000",
                "\u0005Great Bay Temple\u0000",
                "\u0004Stone Tower Temple\u0000"
            };

            var dungeonItemMessages = new string[]
            {
                "\u0017You found a \u0001Small Key\u0000 for\u0011{0}!\u0018\u00BF",
                "\u0017You found the \u0001Boss Key\u0000 for\u0011{0}!\u0018\u00BF",
                "\u0017You found the \u0001Dungeon Map\u0000 for\u0011{0}!\u0018\u00BF",
                "\u0017You found the \u0001Compass\u0000 for\u0011{0}!\u0018\u00BF",
                "\u0017You found a \u0001Stray Fairy\u0000 from\u0011{0}!\u0018\u001F\u0000\u0010\u0011This is your \u0001\u000C\u0000 one!\u00BF",
            };

            var dungeonItemIcons = new byte[]
            {
                0x3C, 0x3D, 0x3E, 0x3F, 0xFE
            };

            for (var i = 0; i < dungeonItemMessageIds.Length; i++)
            {
                var messageId = dungeonItemMessageIds[i];
                var icon = dungeonItemIcons[i % 5];
                var dungeonName = dungeonNames[i / 5];
                var message = string.Format(dungeonItemMessages[i % 5], dungeonName);

                newMessages.Add(new MessageEntry
                {
                    Id = messageId,
                    Header = new byte[11] { 0x02, 0x00, icon, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                    Message = message
                });
            }

            _messageTable.UpdateMessages(newMessages);

            if (_randomized.Settings.AddShopItems)
            {
                ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-shop-checks");
            }
        }

        private void WriteGossipQuotes()
        {
            if (_randomized.Settings.LogicMode == LogicMode.Vanilla)
            {
                return;
            }

            if (_randomized.Settings.FreeHints)
            {
                WriteFreeHints();
            }

            if (_randomized.Settings.GossipHintStyle != GossipHintStyle.Default)
            {
                _messageTable.UpdateMessages(_randomized.GossipQuotes);
            }
        }

        private void WriteTitleScreen()
        {
            var titleScreen = ResourceUtils.ReadHack(Values.ModsDirectory, "title-screen");

            int rot = _randomized.TitleLogoColor;
            Color l;
            float h;
            for (int i = 0; i < 144 * 64; i++)
            {
                int p = (i * 4) + 8;
                l = Color.FromArgb(titleScreen[p + 3], titleScreen[p], titleScreen[p + 1], titleScreen[p + 2]);
                h = l.GetHue();
                h += rot;
                h %= 360f;
                l = ColorUtils.FromAHSB(l.A, h, l.GetSaturation(), l.GetBrightness());
                titleScreen[p] = l.R;
                titleScreen[p + 1] = l.G;
                titleScreen[p + 2] = l.B;
                titleScreen[p + 3] = l.A;
            }
            l = Color.FromArgb(titleScreen[0x1FE72], titleScreen[0x1FE73], titleScreen[0x1FE76]);
            h = l.GetHue();
            h += rot;
            h %= 360f;
            l = ColorUtils.FromAHSB(255, h, l.GetSaturation(), l.GetBrightness());
            titleScreen[0x1FE72] = l.R;
            titleScreen[0x1FE73] = l.G;
            titleScreen[0x1FE76] = l.B;

            ResourceUtils.ApplyHack(titleScreen);
        }


        private void WriteFileSelect()
        {
            ResourceUtils.ApplyHack(Values.ModsDirectory, "file-select");
            byte[] SkyboxDefault = new byte[] { 0x91, 0x78, 0x9B, 0x28, 0x00, 0x28 };
            List<int[]> Addrs = ResourceUtils.GetAddresses(Values.AddrsDirectory, "skybox-init");
            for (int i = 0; i < 2; i++)
            {
                Color c = Color.FromArgb(SkyboxDefault[i * 3], SkyboxDefault[i * 3 + 1], SkyboxDefault[i * 3 + 2]);
                float h = c.GetHue();
                h += _randomized.FileSelectSkybox;
                h %= 360f;
                c = ColorUtils.FromAHSB(c.A, h, c.GetSaturation(), c.GetBrightness());
                SkyboxDefault[i * 3] = c.R;
                SkyboxDefault[i * 3 + 1] = c.G;
                SkyboxDefault[i * 3 + 2] = c.B;
            }

            for (int i = 0; i < 3; i++)
            {
                ReadWriteUtils.WriteROMAddr(Addrs[i], new byte[] { SkyboxDefault[i * 2], SkyboxDefault[i * 2 + 1] });
            }

            byte[] FSDefault = new byte[] { 0x64, 0x96, 0xFF, 0x96, 0xFF, 0xFF, 0x64, 0xFF, 0xFF };
            Addrs = ResourceUtils.GetAddresses(Values.AddrsDirectory, "fs-colour");
            for (int i = 0; i < 3; i++)
            {
                Color c = Color.FromArgb(FSDefault[i * 3], FSDefault[i * 3 + 1], FSDefault[i * 3 + 2]);
                float h = c.GetHue();
                h += _randomized.FileSelectColor;
                h %= 360f;
                c = ColorUtils.FromAHSB(c.A, h, c.GetSaturation(), c.GetBrightness());
                FSDefault[i * 3] = c.R;
                FSDefault[i * 3 + 1] = c.G;
                FSDefault[i * 3 + 2] = c.B;
            }
            for (int i = 0; i < 9; i++)
            {
                if (i < 6)
                {
                    ReadWriteUtils.WriteROMAddr(Addrs[i], new byte[] { 0x00, FSDefault[i] });
                }
                else
                {
                    ReadWriteUtils.WriteROMAddr(Addrs[i], new byte[] { FSDefault[i] });
                }
            }
        }

        private void WriteStartupStrings()
        {
            if (_randomized.Settings.LogicMode == LogicMode.Vanilla)
            {
                //ResourceUtils.ApplyHack(ModsDir + "postman-testing");
                return;
            }
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            RomUtils.SetStrings(Values.ModsDirectory, "logo-text", $"v{v}", string.Empty);
        }

        private void WriteShopObjects()
        {
            RomUtils.CheckCompressed(1325); // trading post
            var data = RomData.MMFileList[1325].Data.ToList();
            data.RemoveRange(0x15C, 4); // reduce end padding from actors list
            data.InsertRange(0x62, new byte[] { 0x00, 0xC1, 0x00, 0xAF }); // add extra objects
            data[0x29] += 2; // increase object count by 2
            data[0x37] += 4; // add 4 to actor list address
            RomData.MMFileList[1325].Data = data.ToArray();

            RomUtils.CheckCompressed(1503); // bomb shop
            RomData.MMFileList[1503].Data[0x53] = 0x98; // add extra objects
            RomData.MMFileList[1503].Data[0x29] += 1; // increase object count by 1

            RomUtils.CheckCompressed(1142); // witch shop
            data = RomData.MMFileList[1142].Data.ToList();
            data.RemoveRange(0x78, 4); // reduce end padding from actors list
            data.InsertRange(0x48, new byte[] { 0x00, 0xC1, 0x00, 0xC1 }); // add extra objects
            data[0x29] += 2; // increase object count by 2
            data[0x37] += 4; // add 4 to actor list address
            RomData.MMFileList[1142].Data = data.ToArray();
        }

        public void OutputHashIcons(IEnumerable<byte> iconFileIndices, string filename)
        {
            var iconFiles = RomUtils.GetFilesFromArchive(19);
            var numberOfHashIcons = iconFileIndices.Count();
            var margin = 8;
            using (var image = new Image<Argb32>(32 * numberOfHashIcons + margin * (numberOfHashIcons - 1), 32))
            {
                var i = 0;
                foreach (var iconFileIndex in iconFileIndices)
                {
                    using (var icon = Image.LoadPixelData<Rgba32>(iconFiles[iconFileIndex], 32, 32))
                    {
                        image.Mutate(o => o.DrawImage(icon, new Point(i * 32 + i * margin, 0), 1f));
                    }
                    i++;
                }
                image.Save(filename, new PngEncoder());
            }
        }

        private void WriteAsmPatch(AsmContext asm)
        {
            // Load the symbols and use them to apply the patch data
            var options = _randomized.Settings.AsmOptions;

            // Finalize Misc configuration.
            options.MiscConfig.FinalizeSettings(_randomized.Settings);

            asm.ApplyPatch(options);

            if (_extendedObjects != null)
            {
                // Add extended objects file and write addresses to table in ROM
                var extended = _extendedObjects;
                var fileIndex = RomUtils.AppendFile(extended.Bundle.GetFull());
                var file = RomData.MMFileList[fileIndex];
                var baseAddr = (uint)file.Addr;
                asm.Symbols.WriteExtendedObjects(extended.GetAddresses(baseAddr));
            }

            // Add extra messages to message table.
            asm.ExtraMessages.AddMessage(_extraMessages.ToArray());
            asm.WriteExtMessageTable();

            // Add item graphics to table and write to ROM.
            asm.MimicTable.Update(_graphicOverrides);
            asm.WriteMimicItemTable();
        }

        private void WriteAsmConfig(AsmContext asm, byte[] hash)
        {
            UpdateHudColorOverrides(hash);

            // Apply Asm configuration (after hash has been calculated)
            var options = _cosmeticSettings.AsmOptions;
            options.Hash = hash;
            asm.ApplyPostConfiguration(options, false);
        }

        private void WriteAsmConfigPostPatch(AsmContext asm, byte[] hash)
        {
            UpdateHudColorOverrides(hash);

            // Apply current configuration on top of existing Asm patch file
            var options = _cosmeticSettings.AsmOptions;
            options.Hash = hash;
            asm.ApplyPostConfiguration(options, true);
        }

        /// <summary>
        /// Update the HUD colors override options.
        /// </summary>
        /// <param name="hash">Hash which is used with <see cref="Random"/></param>
        private void UpdateHudColorOverrides(byte[] hash)
        {
            var config = _cosmeticSettings.AsmOptions.HudColorsConfig;
            var random = new Random(BitConverter.ToInt32(hash, 0));

            // Update override for heart colors
            if (_cosmeticSettings.HeartsSelection != null)
                config.HeartsOverride = ColorSelectionManager.Hearts.GetItems().FirstOrDefault(csi => csi.Name == _cosmeticSettings.HeartsSelection)?.GetColors(random);
            else
                config.HeartsOverride = null;

            // Update override for magic meter colors
            if (_cosmeticSettings.MagicSelection != null)
                config.MagicOverride = ColorSelectionManager.MagicMeter.GetItems().FirstOrDefault(csi => csi.Name == _cosmeticSettings.HeartsSelection)?.GetColors(random);
            else
                config.MagicOverride = null;
        }

        /// <summary>
        /// Build <see cref="ExtendedObjects"/> and write object indexes to Get-Item list entries.
        /// </summary>
        private void WriteExtendedObjects()
        {
            var addFairies = _randomized.Settings.AddStrayFairies;
            var addSkulltulas = _randomized.Settings.AddSkulltulaTokens;
            var extended = _extendedObjects = ExtendedObjects.Create(addFairies, addSkulltulas);

            foreach (var e in RomData.GetItemList.Values)
            {
                // Update gi-table for Skulltula Tokens.
                if (e.ItemGained == 0x6E && e.Object == 0x125 && extended.Indexes.Skulltula != null)
                {
                    var index = e.Message == 0x51 ? 1 : 0;
                    e.Object = (short)(extended.Indexes.Skulltula.Value + index);
                }

                // Update gi-table for Stray Fairies.
                if (e.ItemGained == 0x9D && e.Object == 0x13A && extended.Indexes.Fairies != null)
                {
                    var index = e.Type >> 4;
                    e.Object = (short)(extended.Indexes.Fairies.Value + index);
                }

                // Update gi-table for Double Defense.
                if (e.ItemGained == 0x9E && e.Object == 0x96 && extended.Indexes.DoubleDefense != null)
                {
                    e.Object = extended.Indexes.DoubleDefense.Value;
                }

                // Update gi-table for Notes.
                if (((e.ItemGained >= 0x66 && e.ItemGained <= 0x6C) || e.ItemGained == 0x62) && e.Object == 0x8F && extended.Indexes.MusicNotes != null)
                {
                    e.Object = extended.Indexes.MusicNotes.Value;
                }
            }
        }

        /// <summary>
        /// Write data related to ice traps to ROM.
        /// </summary>
        public void WriteIceTraps()
        {
            // Add mimic graphic to graphic overrides table.
            foreach (var item in _randomized.IceTraps)
            {
                var newLocation = item.NewLocation.Value;
                if (newLocation.IsVisible() || newLocation.IsShop())
                {
                    var giIndex = item.NewLocation.Value.GetItemIndex().Value;
                    var graphic = item.Mimic.ResolveGraphic();
                    _graphicOverrides.Add(giIndex, graphic);
                }
            }

            // Add "You are a FOOL!" message to extra messages table.
            var entry = new MessageEntry(
                Item.IceTrap.ExclusiveItemEntry().Message,
                Item.IceTrap.ExclusiveItemMessage());
            _extraMessages.Add(entry);
        }

        public void MakeROM(OutputSettings outputSettings, IProgressReporter progressReporter)
        {
            using (BinaryReader OldROM = new BinaryReader(File.OpenRead(outputSettings.InputROMFilename)))
            {
                RomUtils.ReadFileTable(OldROM);
                _messageTable = MessageTable.ReadDefault();
            }

            var originalMMFileList = RomData.MMFileList.Select(file => file.Clone()).ToList();

            byte[] hash;
            AsmContext asm;
            if (!string.IsNullOrWhiteSpace(outputSettings.InputPatchFilename))
            {
                progressReporter.ReportProgress(50, "Applying patch...");
                hash = RomUtils.ApplyPatch(outputSettings.InputPatchFilename);

                // Parse Symbols data from the ROM (specific MMFile)
                asm = AsmContext.LoadFromROM();

                // Apply Asm configuration post-patch
                WriteAsmConfigPostPatch(asm, hash);
            }
            else
            {
                progressReporter.ReportProgress(55, "Writing player model...");
                WritePlayerModel();

                if (_randomized.Settings.LogicMode != LogicMode.Vanilla)
                {
                    progressReporter.ReportProgress(60, "Applying hacks...");
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "title-screen");
                    WriteTitleScreen();
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "misc-changes");
                    WriteMiscText();
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "cm-cs");
                    ResourceUtils.ApplyHack(Values.ModsDirectory, "fix-song-of-healing");
                    WriteFileSelect();
                }
                ResourceUtils.ApplyHack(Values.ModsDirectory, "init-file");

                progressReporter.ReportProgress(61, "Writing quick text...");
                WriteQuickText();

                progressReporter.ReportProgress(62, "Writing cutscenes...");
                WriteCutscenes();

                progressReporter.ReportProgress(63, "Writing dungeons...");
                WriteDungeons();

                progressReporter.ReportProgress(64, "Writing gimmicks...");
                WriteGimmicks();

                progressReporter.ReportProgress(65, "Writing speedups...");
                WriteSpeedUps();

                progressReporter.ReportProgress(66, "Writing enemies...");
                WriteEnemies();

                // if shop should match given items
                {
                    WriteShopObjects();
                }

                progressReporter.ReportProgress(67, "Writing items...");
                WriteItems();

                Add5NutsToField();
                Add5NutsToField(0xC444B7);
                Add5NutsToField(0xC444BB);
                AddSingleStickToField();
                AddSingleStickToField(0xC444C0);
                AddSingleStickToField(0xC444BF);

                progressReporter.ReportProgress(67, "Writing entrances...");
                WriteEntrances(outputSettings);

                progressReporter.ReportProgress(68, "Writing messages...");
                WriteGossipQuotes();

                MessageTable.WriteDefault(_messageTable, _randomized.Settings.QuickTextEnabled);

                progressReporter.ReportProgress(69, "Writing startup...");
                WriteStartupStrings();

                // Overwrite existing items with ice traps.
                if (_randomized.Settings.IceTraps != IceTraps.None)
                {
                    progressReporter.ReportProgress(70, "Writing ice traps...");
                    WriteIceTraps();
                }

                // Load Asm data from internal resource files and apply
                asm = AsmContext.LoadInternal();
                progressReporter.ReportProgress(71, "Writing ASM patch...");
                WriteAsmPatch(asm);
                
                progressReporter.ReportProgress(72, outputSettings.GeneratePatch ? "Generating patch..." : "Computing hash...");
                hash = RomUtils.CreatePatch(outputSettings.GeneratePatch ? outputSettings.OutputROMFilename : null, originalMMFileList);

                // Write subset of Asm config post-patch
                WriteAsmConfig(asm, hash);

                if (_randomized.Settings.DrawHash || outputSettings.GeneratePatch)
                {
                    var iconStripIcons = asm.Symbols.ReadHashIconsTable();
                    OutputHashIcons(ImageUtils.GetIconIndices(hash).Select(index => iconStripIcons[index]), Path.ChangeExtension(outputSettings.OutputROMFilename, "png"));
                }
            }
            WriteMiscellaneousChanges();

            progressReporter.ReportProgress(72, "Writing cosmetics...");
            WriteTatlColour(new Random(BitConverter.ToInt32(hash, 0)));
            WriteTunicColor();

            progressReporter.ReportProgress(73, "Writing music...");
            WriteAudioSeq(new Random(BitConverter.ToInt32(hash, 0)), outputSettings);
            WriteMuteMusic();
            WriteEnemyCombatMusicMute();

            progressReporter.ReportProgress(74, "Writing sound effects...");
            WriteSoundEffects(new Random(BitConverter.ToInt32(hash, 0)));

            WriteMutedLowHeartBeep();

            if (outputSettings.GenerateROM || outputSettings.OutputVC)
            {
                progressReporter.ReportProgress(75, "Building ROM...");

                byte[] ROM = RomUtils.BuildROM();

                if (outputSettings.GenerateROM)
                {
                    if (ROM.Length > 0x4000000) // over 64mb
                    {
                        throw new ROMOverflow("64 MB,hardware (Everdrive)");
                    }
                    progressReporter.ReportProgress(85, "Writing ROM...");
                    RomUtils.WriteROM(outputSettings.OutputROMFilename, ROM);
                }

                if (outputSettings.OutputVC)
                {
                    if (ROM.Length > 0x2000000) // over 32mb
                    {
                        throw new ROMOverflow("32 MB,WiiVC");
                    }
                    progressReporter.ReportProgress(90, "Writing VC...");
                    VCInjectionUtils.BuildVC(ROM, _cosmeticSettings.AsmOptions.DPadConfig, Values.VCDirectory, Path.ChangeExtension(outputSettings.OutputROMFilename, "wad"));
                }
            }
            progressReporter.ReportProgress(100, "Done!");

        }

    }

}
