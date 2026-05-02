using MMR.Randomizer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Compression;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;

using ActorEnum = MMR.Randomizer.GameObjects.Actor;
using ActorInst = MMR.Randomizer.Models.Rom.Actor;

namespace MMR.Randomizer.Enemizer
{
    /// <summary>
    ///  Injecting binary actors as replacement overlays
    ///    New actors in new actor slots is still broken: corrupts player code (?)
    /// </summary>

    [System.Diagnostics.DebuggerDisplay("[{filename}] 0x{ActorId.ToString(\"X3\")}:{fileID}")]
    public class InjectedActor
    {
        // when we inject a new actor theres some data we need
        // and some adjustments we need to make based on where it gets placed in vram
        public int ActorId = 0;
        public int ObjectId = 0;
        public int fileID = 0;
        public int ObjectFid = 0;
        public (int poly, int vert) DynaLoad = (-1, -1); // Does the actor use the dyna system, which is a limited buffer size we cannot overflow

        // if all new actor, we meed to know where the old vram start was when we shift VRAM for the actor
        public uint buildVramStart = 0;
        // init vars are located somewhere in .data, we want to know where exactly because its hard coded in overlay table
        public uint initVarsLocation = 0;

        // TODO make this match regular actors??
        public List<int> groundVariants = new List<int>();
        public List<int> flyingVariants = new List<int>();
        public List<int> waterVariants = new List<int>();
        public List<int> waterTopVariants = new List<int>();
        public List<int> waterBottomVariants = new List<int>();
        public List<int> perchingVariants = new List<int>();
        public List<int> wallVariants = new List<int>();
        public List<int> respawningVariants = new List<int>();
        // variants with max
        public List<VariantsWithRoomMax> limitedVariants = new List<VariantsWithRoomMax>();
        public UnkillableAllVariantsAttribute unkillableAttr = null;
        public OnlyOneActorPerRoom onlyOnePerRoom = null;

        // should only be stored here if new actor
        public byte[] overlayBin;
        public uint overlayBinLen;
        public string filename = ""; // debugging
    }

    class ActorInjection
    {
        public static InjectedActor ParseMMRAMeta(string metaFile)
        {
            /// every MMRA comes with one meta file per bin, this contains metadata
            var vanillaActors = Enum.GetValues(typeof(ActorEnum)).Cast<ActorEnum>().ToList();
            var newInjectedActor = new InjectedActor();

            foreach (var line in metaFile.Split('\n'))
            {
                var asignment = line.Split('#')[0].Trim(); // remove comments

                if (asignment.Length == 0) // comment or empty line: ignore
                {
                    continue;
                }

                var asignmentSplit = asignment.Split('=');
                var command = asignmentSplit[0].Trim();
                if (command == "unkillable")
                {
                    newInjectedActor.unkillableAttr = new UnkillableAllVariantsAttribute();
                    continue;
                }
                if (command == "only_one_per_room")
                {
                    newInjectedActor.onlyOnePerRoom = new OnlyOneActorPerRoom();
                    continue;
                }

                string valueStr = asignmentSplit[1].Trim();

                if (command == "ground_variants")
                {
                    var newGroundVariants = valueStr.Split(",").ToList();
                    var newGroundVariantsShort = newGroundVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.groundVariants = newGroundVariantsShort;
                    continue;
                }
                if (command == "flying_variants")
                {
                    var newFlyingVariants = valueStr.Split(",").ToList();
                    var newFlyingVariantsShort = newFlyingVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.flyingVariants = newFlyingVariantsShort;
                    continue;
                }
                if (command == "water_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "watertop_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterTopVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "waterbottom_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterBottomVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "perching_variants")
                {
                    var newPerchingVariants = valueStr.Split(",").ToList();
                    var newPerchingVariantsShort = newPerchingVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.perchingVariants = newPerchingVariantsShort;
                    continue;
                }
                if (command == "wall_variants")
                {
                    var newWallVariants = valueStr.Split(",").ToList();
                    var newWallVariantsShort = newWallVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.wallVariants = newWallVariantsShort;
                    continue;
                }
                if (command == "respawning_variants")
                {
                    var newRespawningVariants = valueStr.Split(",").ToList();
                    var newRespawningVariantsShort = newRespawningVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.respawningVariants = newRespawningVariantsShort;
                    continue;
                }

                if (command == "variant_with_max")
                {
                    var newLimitedVariant = valueStr.Split(",").ToList();
                    int max = Convert.ToInt32(newLimitedVariant[1].Trim(), 10);
                    int variant = Convert.ToInt32(newLimitedVariant[0].Trim(), 16);

                    newInjectedActor.limitedVariants.Add(new VariantsWithRoomMax(max, variant));
                    continue;
                }
                if (command == "dyna_load")
                {
                    var newDynaValuePair = valueStr.Split(",").ToList();
                    var intBase = newDynaValuePair[0].Contains("0x") ? 16 : 10;
                    newInjectedActor.DynaLoad.poly = Convert.ToInt32(newDynaValuePair[0].Trim(), intBase);
                    intBase = newDynaValuePair[0].Contains("0x") ? 16 : 10;
                    newInjectedActor.DynaLoad.vert = Convert.ToInt32(newDynaValuePair[1].Trim(), intBase);
                    continue;
                }


                var value = Convert.ToInt32(valueStr, fromBase: 16);
                if (command == "actor_id")
                {
                    newInjectedActor.ActorId = value;
                }
                else if (command == "obj_id")
                {
                    newInjectedActor.ObjectId = value;
                }
                else if (command == "file_id" || command == "actor_fid")
                {
                    newInjectedActor.fileID = Convert.ToInt32(valueStr, fromBase: 10);
                }
                else if (command == "object_fid")
                {
                    newInjectedActor.ObjectFid = Convert.ToInt32(valueStr, fromBase: 10);
                }

                var uvalue = Convert.ToUInt32(valueStr, fromBase: 16);

                if (command == "initvars_offset")
                {
                    newInjectedActor.initVarsLocation = uvalue;
                }
                else if (command == "vram_start")
                {
                    newInjectedActor.buildVramStart = uvalue;
                }
            } // for each line end

            // update actor init vars in our actor
            var actorGameObj = vanillaActors.Find(act => act.FileListIndex() == newInjectedActor.fileID);
            if (actorGameObj != 0)
            {
                var initVarsAttr = actorGameObj.GetAttribute<ActorInitVarOffsetAttribute>();
                if (initVarsAttr != null) // had one before, change now
                {
                    // untested, might not work
                    initVarsAttr.Offset = (int)newInjectedActor.initVarsLocation;
                }
            }

            return newInjectedActor;
        }

        public static List<string> GenerateMMRAFileList(string directory)
        {
            var directories = new List<string> { };

            directories.AddRange(Directory.GetDirectories(directory).ToList()); // depth 1
            foreach (string d in directories.ToList()) // another layer deep to be safe
            {
                List<String> deeperDirectories = Directory.GetDirectories(d).ToList();
                directories.AddRange(deeperDirectories); // depth 2
            }
            directories.Add(directory); // added after to avoid contamination

            var files = new List<string> { };

            foreach (var dir in directories)
            {
                files.AddRange(Directory.GetFiles(dir, "*.mmra"));
            }

            return files;
        }

        public static void ScanForMMRA(string directory, GameplaySettings settings)
        {
            // decomp lets us more easily modify actors now
            // for now, until cat/zoey figure out how to directly integrate the projects
            //   I will, instead, compile with decomp, and then extract the binaries and inject here
            // MMRA files: Majora Mask Rando ActorInst files, just zip files that contain binaries and extras later
            // ideas for extras: notes to tell rando where sound effects are to be replaced
            // function pointers to interconnect the code

            if (!Directory.Exists(directory)) return;
            // if actorizer is off, we need to not read any of these
            if (settings.ActorMode == ActorMode.Default) return; // right now actorizer/enemizer is the only system that uses this

            uint END_VANILLA_OBJ_SEGMENT = 0x01E5E600;

            Enemies.InjectedActors.Clear(); // from last gen
            var codeFile = RomData.MMFileList[31].Data;
            var objectTableOffset = 0x11CC80;

            foreach (string filePath in GenerateMMRAFileList(directory))
            {
                // this is a list of broken actors that we cannot use, they have been removed but I don't trust users to remove the file and not just overwrite a previous directory
                if (filePath.Contains("SafeBoat.mmra")
                 || filePath.Contains("FairySpot.mmra") // is missing a variant, and was not working, not even sure what it was doing, TODo
                 || filePath.Contains("BabaIsLoaded.mmra") // talk locking, lost the code, have to disable because no time to rewrite
                 || filePath.Contains("HairyGrog.mmra") // my code overrites zoeys item changes, I can't fix without breaking my code for enemies
                 || filePath.Contains("Dinofos"))
                {
                    //throw new Exception("SafeBoat.mmra no longer works in actorizer 1.16, \n remove the file from MMR/actors and start a new seed.");
                    continue;
                }

                if (settings.Character == Character.AdultLink && filePath.Contains("Anope.mmra"))
                {
                    continue; // this OOT epona replacement actor does not work with adult oot link mod because it replaces horse assets
                }

                try
                {
                    using (ZipArchive zip = ZipFile.OpenRead(filePath))
                    {

                        if (zip.Entries.Where(e => e.Name.Contains(".bin")).Count() == 0)
                        {
                            throw new Exception($"ERROR: cannot find a single binary actor in file {filePath}");
                        }

                        // per binary, since MMRA should support multiple binaries
                        foreach (ZipArchiveEntry binFile in zip.Entries.Where(e => e.Name.Contains(".bin")))
                        {
                            var filename = binFile.Name.Substring(0, binFile.Name.LastIndexOf(".bin"));

                            // read overlay binary data
                            int newBinLen = ((int)binFile.Length) + ((int)binFile.Length % 0x10); // dma padding
                            var overlayData = new byte[newBinLen];
                            binFile.Open().Read(overlayData, 0, overlayData.Length);

                            // the binary filename convention will be NOTES_name.bin

                            //var binFilenameSplit = binFile.Name.Split('_'); // everything before _ is a comment, readability, discard here
                            //var fileIDtext = binFilenameSplit.Length > 1 ? binFilenameSplit[binFilenameSplit.Length - 1] : binFile.Name;

                            // read the associated meta file
                            var metaFileEntry = zip.GetEntry(filename + ".meta");
                            if (metaFileEntry == null) // meta not found
                                throw new Exception($"Could not find a meta for actor bin [{binFile.Name}]\n   in [{filePath}]");

                            var injectedActor = ParseMMRAMeta(new StreamReader(metaFileEntry.Open(), Encoding.Default).ReadToEnd());
                            injectedActor.filename = filePath; // debugging

                            if (injectedActor.fileID != 0) 
                            {
                                // check for duplicate actor
                                var copyOvlFileSearch = Enemies.InjectedActors.Find(act => act.fileID == injectedActor.fileID);
                                if (copyOvlFileSearch != null)
                                {
                                    throw new Exception("\n\n" +
                                        "ERROR (Actor Inject):\n" +
                                        " Two separate actor files are trying to overwrite the same file.\n" +
                                        "File 1: " + injectedActor.filename + "\n" +
                                        "File 2: " + copyOvlFileSearch.filename + "\n\n" +
                                        "Please remove one before building another seed.\n");
                                }
                            }

                            // we need to inject actors if we find them
                            // TODO move this to a "load all objects" separate function where we rank them by size
                            // so we can re-use some old spots instead of just extending
                            // NOTE: this does not work
                            /* var objectFileEntry = zip.GetEntry(filename + ".object");
                            if (objectFileEntry != null) // object included
                            {
                                newBinLen = ((int)objectFileEntry.Length) + ((int)objectFileEntry.Length % 0x10); // dma padding
                                var objectData = new byte[newBinLen];
                                objectFileEntry.Open().Read(objectData, 0, objectData.Length);

                                RomData.MMFileList[injectedActor.ObjectFid].Data = objectData;
                                RomData.MMFileList[injectedActor.ObjectFid].WasEdited = true;

                                // we need to update the object table with the size of the new object
                                uint newSegmentROMStart = END_VANILLA_OBJ_SEGMENT;
                                uint newSegmentROMEnd = newSegmentROMStart + (uint) objectData.Length;
                                if (newSegmentROMEnd > 0x02000000)
                                {
                                    throw new Exception("Object segment overflow, reduce your actors that use custom objects");
                                }
                                END_VANILLA_OBJ_SEGMENT = newSegmentROMEnd;
                                ReadWriteUtils.Arr_WriteU32(codeFile, (objectTableOffset + (2 * 4 * injectedActor.ObjectId)), newSegmentROMStart);
                                ReadWriteUtils.Arr_WriteU32(codeFile, (objectTableOffset + (2 * 4 * injectedActor.ObjectId + 4)), newSegmentROMEnd);
                            } // */


                            Enemies.InjectedActors.Add(injectedActor);

                            // we have to add the changes to our list of actors we are going to use in enemizer/actorizer
                            // behavior now differs between replacement actors and brand new
                            var replacementEnemySearch = Enemies.ReplacementCandidateList.Find(act => act.ActorId == injectedActor.ActorId);
                            //var replacementListSearch = Enum.GetValues(typeof(ActorEnum)).Cast<ActorEnum>().ToList().Find(act => (int) act == injectedActor.ActorId);
                            if (replacementEnemySearch != null) // previous actor
                            {
                                replacementEnemySearch.UpdateActor(injectedActor);
                            }
                            //else (injectedActor.)
                            /* else if (injectedActor.fileID != 0)
                            {
                                // sometimes we want to inject an actor that wont be used by actorizer/enemizer,
                                // so it wont be in the list above, but its not marked as a new actor either
                                replacementEnemySearch = null;
                            } // */
                            else
                            {
                                replacementEnemySearch = new ActorInst(injectedActor, Path.GetFileName(filePath));
                                Enemies.ReplacementCandidateList.Add(replacementEnemySearch);
                            }

                            if (injectedActor.ObjectId <= 3)
                            {
                                var freeCandidateSearch = Enemies.FreeCandidateList.Find(act => act.ActorId == injectedActor.ActorId);
                                if (freeCandidateSearch == null)
                                {
                                    Enemies.FreeCandidateList.Add(new ActorInst(injectedActor, filename));
                                }
                                else
                                {
                                    freeCandidateSearch.UpdateActor(injectedActor);
                                }
                            }

                            // experiment: lets not re-compress our actor and see what happens

                            // this is separate from the above because this lets us modify files not found in Enemies.ReplacementCandidateList
                            // like demo_kankyo, which is a free actor and not a regular candidate
                            var newFID = (int)injectedActor.fileID;
                            injectedActor.overlayBinLen = (uint)overlayData.Length;
                            if (newFID == 0)
                            {
                                injectedActor.overlayBin = overlayData; // save bin for now
                            }
                            else
                            {
                                /// overwrite the file now
                                RomData.MMFileList[newFID].Data = overlayData;
                                // we CANNOT update the .end because it breaks MMR's romaddr->file+offset calculations
                                //   MMR will attempt to write romhacks for the following actor to our new bigger actor
                                //   we would have to rewrite half of rando to get around that
                                // thankfully, this updating end isn't actually necessary it seems, we can leave this vanilla
                                //RomData.MMFileList[newFID].End = RomData.MMFileList[newFID].Addr + newBinLen;
                                RomData.MMFileList[newFID].WasEdited = true;
                                RomData.MMFileList[newFID].IsReadOnly = true;
                                // injectedActor.overlayBin = overlayData; // we dont save bin if its a previous file
                            }

                            // wait isnt this bad? we dont compress the actor again? is this just a work around?
                            RomData.MMFileList[newFID].IsCompressed = false;

                        } // foreach bin entry

                    }// zip as file end
                } // try end
                catch (Exception e)
                {
                    throw new Exception($"Error attempting to read archive: {filePath} -- \n" + e);
                }

            } // for each mmra end
        }

        public static void UpdateOverlayVRAMReloc(MMFile file, int[] sectionOffsets, uint newVRAMOffset)
        {
            /// Reloc: overlay c code is compiled with VRAM addresses already baked in,
            ///  these get adjusted when the overlay is loaded into RAM, to match the RAM locations
            ///  but when we inject this new overlay we move its VRAM to a different place, so its wrong
            ///  so now, we must re-apply the VRAM addresses so when the game shifts them into RAM it will have the correct values

            var relocSize = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
            // the table pointer at the end is an offset from the end, we need to swap it
            int tableOffset = (int)(file.Data.Length - relocSize);
            int relocEntryCountLocation = (int)(tableOffset + (4 * 4)); // first four ints are section sizes

            uint relocEntryCount = ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryCountLocation);
            var relocEntryLoc = relocEntryCountLocation + 4; // first overlayEntry immediately after reloc count
            var relocEntryEndLoc = relocEntryLoc + (relocEntryCount * 4);
            // traverse the whole relocation section, parse the changes, apply

            uint pointer = 0; // save outside of loop incase of multiple combos

            while (relocEntryLoc < relocEntryEndLoc)
            {
                // each overlayEntry in reloc is one nibble of shifted section, one nible of type, and 3 bytes of address
                // text section starts at 1 not 0
                var section = ((file.Data[relocEntryLoc] & 0xC0) >> 6) - 1;
                var sectionOffset = sectionOffsets[section];

                var commandType = (file.Data[relocEntryLoc] & 0xF);
                var commandTypeLookahead = (file.Data[relocEntryLoc + 4] & 0xF); // double command for LUI/ADDIU

                if (commandType == 0x5 /* R_MIPS_HI16 */ && commandTypeLookahead == 0x6) // LUI/ADDIU combo
                {
                    int luiLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;

                    // addu treats the last two bytes of our pointer as signed
                    // to fix this, the LUI command is given a carry over bit to fix it, we need to read and write knowing this
                    // combine the halves from asm back into one pointer
                    pointer = 0;
                    pointer = ((uint)ReadWriteUtils.Arr_ReadU16(file.Data, addiuLoc + 2));
                    int LUIDecr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0;
                    uint oldLuiData = ReadWriteUtils.Arr_ReadU16(file.Data, luiLoc + 2);
                    pointer |= ((uint)(oldLuiData - LUIDecr) << 16);

                    pointer += newVRAMOffset;

                    // separate the pointer again into halves and put back
                    int LUIIncr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0; // if the lower half is too big we have to add one to LUI
                    ushort luiPart = (ushort)(((pointer & 0xFFFF0000) >> 16) + LUIIncr);
                    ushort adduPart = (ushort)(pointer & 0xFFFF);
                    ReadWriteUtils.Arr_WriteU16(file.Data, luiLoc + 2, luiPart);
                    ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, adduPart);

                    relocEntryLoc += 8;
                }
                else if (commandType == 0x6 /* R_MIPS_LO16 */) // another ADDIU after the first combo 
                {
                    int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;
                    ushort adduPart = (ushort)(pointer & 0xFFFF);
                    ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, adduPart);

                    relocEntryLoc += 4; // another
                }
                else if (commandType == 0x4 /* R_MIPS_26 */) // JAL function calls
                {
                    int jalLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint jal = ReadWriteUtils.Arr_ReadU32(file.Data, jalLoc) & 0x00FFFFFF;
                    uint shiftedJal = jal << 2;
                    shiftedJal += newVRAMOffset;
                    shiftedJal = shiftedJal >> 2;
                    ReadWriteUtils.Arr_WriteU32(file.Data, jalLoc, 0x0C000000 | shiftedJal);

                    relocEntryLoc += 4;
                }
                else if (commandType == 0x2 /* R_MIPS_32 */) // Hard pointer (init/destroy/update/draw pointers can be here, also actual ptr in rodata)
                {
                    int ptrLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint ptrValue = ReadWriteUtils.Arr_ReadU32(file.Data, ptrLoc);
                    ptrValue += newVRAMOffset;
                    ReadWriteUtils.Arr_WriteU32(file.Data, ptrLoc, ptrValue);

                    relocEntryLoc += 4;
                }
                else // unknown command? supposidly Z64 only uses these four although it could support more
                {
                    throw new Exception($"UpdateOverlayVRAMReloc: unknown reloc overlayEntry value:\n" +
                        $" {ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc).ToString("X")}");
                }
            } // end while (we havent reached the end of reloc)
        } // end UpdateOverlayVRAMReloc

        public static void UpdateActorOverlayTable()
        {
            // this is called from romutils.cs right before we build the rom
            /// if overlays have grown, we need to modify their overlay table to use the right values for the new files
            /// every time you move an overlay you need to relocate the vram addresses, so instead of shifting all of them
            ///  we just move the new larger files to the end and leave a hole behind for now

            // TODO can we _detect_ this value by looking at rando is already doing?
            // 0x80C260A0 <- known last vanilla vram value
            const uint theEndOfTakenVRAM = 0x80D00000; // changed to make it visually obvious this is a new actor
            // can't even remember why I raised it
            //const uint theEndOfTakenVRAM = 0x80CA0000; // TODO change back to lower
            //const int theEndOfTakenVROM = 0x03100000; // 0x02EE7XXX <- actual
            // maybe if I set it longer away I can skip the extra samples getting corrupted, probably not
            //const int theEndOfTakenVROM = 0x03400000; // stable, was used for like 30 actorizer versions
            const int theEndOfTakenVROM = 0x05000000; // stable, was used for like 30 actorizer versions
            // WARNING: 0x03880000 is above us, which is Rebbacus's overlay file that was moved, we need to keep that in mind

            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);

            // the overlay table exists inside of another file, we need the offset to the table
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;

            // generate a list of actors sorted by fid
            var actorList = Enum.GetValues(typeof(ActorEnum)).Cast<ActorEnum>().ToList();
            actorList.Remove(ActorEnum.Empty);
            actorList.Remove(ActorEnum.NULL);
            actorList.RemoveAll(act => act.FileListIndex() < 38);
            var fidSortedActors = actorList.OrderBy(x => x.FileListIndex()).ToList();

            uint previousLastVRAMEnd = theEndOfTakenVRAM;
            int previousLastVROMEnd = theEndOfTakenVROM;

            foreach (var injectedActor in Enemies.InjectedActors)
            {
                // TODO: where does actorid get set for new inject (whihc is currently busted)
                var ActorId = injectedActor.ActorId;
                var fileID = injectedActor.fileID;
                MMFile file = RomData.MMFileList[fileID];

                try
                {
                    int entryLoc = actorOvlTblOffset + (ActorId * 32); // overlay table is sorted by ActorId

                    uint oldVROMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x0);
                    uint oldVROMEnd = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x4);

                    // if build knows where VRAM used to start for this actor, use that
                    // else, use the old VRAM build for the given actor in this slot
                    uint oldVRAMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x08);
                    oldVRAMStart = (injectedActor.buildVramStart != 0) ? (injectedActor.buildVramStart) : (oldVRAMStart);

                    // if it was edited, its not compressed, get new filesize, else diff old address values
                    var uncompresedVROMSize = (file.WasEdited) ? (file.Data.Length) : (file.End - file.Addr);

                    // for now since we have the space, just move all injected actors to the end, even if they are smaller
                    // TODO make a list of previously free holes we can stick stuff into and check that first before using the end
                    // could even do a hermit crab sort to get a list of smaller actors first and do this out of order
                    file.Addr = previousLastVROMEnd;
                    file.End = previousLastVROMEnd + uncompresedVROMSize;
                    previousLastVROMEnd = file.End;

                    // update VROM we have those values now
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0, (uint)file.Addr);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x4, (uint)file.End);

                    // now to update the reloc values of the overlay to match our new vrom location
                    // we know where in the overlay pointers exist that need to be updated for VROM->VRAM
                    // .reloc stores this info for us as a table of words that contain enough info to help us update
                    // the very last byte in the overlay is (from end) offset
                    //   of the table that declares size of text/data/rodata/bss
                    // following those is a count of the reloc entries, followed by the actual entries
                    var fileTableEndOffset = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
                    // the table pointer at the end is an offset from the end, we need to swap it
                    int tableOffset = (int)(file.Data.Length - fileTableEndOffset);

                    // the section table only contains section sizes, we need to walk it to know the offsets
                    var sectionOffsets = new int[4];
                    sectionOffsets[0] = 0; // text (always at the start for our overlay system)
                    sectionOffsets[1] = sectionOffsets[0] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 0); // data
                    sectionOffsets[2] = sectionOffsets[1] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 4); // rodata
                    var bssSize = (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 8);
                    sectionOffsets[3] = sectionOffsets[2] + bssSize;

                    // have to move the overlay vram location assume its bigger
                    // calculate the new VRAM and offset for our new overlay VRAM location
                    //var newVRAMSize = sectionOffsets[3] + relocSize; // what the fuck is this
                    // the only increase in size of the vram is the BSS so just go with that
                    var newVRAMSize = injectedActor.overlayBinLen + bssSize;
                    // TODO check if we can place it in an old hole left behind by a previously moved actor
                    var newVRAMStart = previousLastVRAMEnd;
                    var newVRAMEnd = (uint)(newVRAMStart + newVRAMSize);
                    var newVRAMOffset = newVRAMStart - oldVRAMStart;

                    // all the pointers and vram locations in the file need to be updated too
                    UpdateOverlayVRAMReloc(file, sectionOffsets, newVRAMOffset);

                    uint newInitVarAddr = newVRAMStart + injectedActor.initVarsLocation;

                    // write the VRAM sections of the overlay table entry
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x08, newVRAMStart);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0C, newVRAMEnd);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x14, newInitVarAddr);

                    previousLastVRAMEnd = newVRAMEnd + (newVRAMEnd % 0x10); // not sure if dma padding matters here
                    RomData.MMFileList[fileID] = file;

                }
                catch (Exception e)
                {
                    throw new Exception($"Error during actor overlay table reorder of" +
                        $"  actor {ActorId} file {fileID}:\n" +
                        e.ToString());
                }
            }// end Foreach overlay in overlaylist
        } // end UpdateOverlayTable

        public static void InjectNewActors(StreamWriter log)
        {
            /// this might get merged back in with scan, and/or the pieces get moved back here
            /// we need to build an ActorInst from our injected actor, and finish injected actor conversions

            if (Enemies.InjectedActors.Count == 0) return;

            var freeOverlaySlots = Enum.GetValues(typeof(ActorEnum)).Cast<ActorEnum>()
                        .Where(act => act.ToString().Contains("Empty")).ToList();

            // in case DMA is restricted, start with a list of known bunk files
            var freeFileSlots = new List<int>
            {
                // these files at the end of the vanilla DMA are unused in USA
                // but MMR might use them, do not
                // 1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551,
                // unused actors or objects:
                //ActorEnum.UnusedClockTowerSpotlight.FileListIndex(),
                //ActorEnum.Obj_Ocarinalift.FileListIndex(),
                //ActorEnum.UnusedStoneTowerPlatform.FileListIndex(),
                ActorEnum.Unused_En_Boj_01.FileListIndex(),  // empty actors with nothing in them
                ActorEnum.Unused_En_Boj_02.FileListIndex(),
                ActorEnum.Unused_En_Boj_03.FileListIndex(),
                //ActorEnum.En_Boj_04.FileListIndex(), // future grotto spawner
                //ActorEnum.En_Boj_05.FileListIndex(),
                
                //ActorEnum.SariaSongOcarinaEffects.FileListIndex(), // should be lower down as we might need to use it later
                806, // OoT potion shop man (the first object, not the updated one they used in their unused actor)
                692, // OoT Child zelda (the first object, not the updated one they used in their 3 minute cutscene actor)

                // using lost woods for debugging, same crash, it doesn't seem to be file collision
                //GameObjects.Scene.LostWoods.FileID(), // testing, we know these are useless in rando, and they will never be loaded in termina field where the known crash is
                //GameObjects.Scene.LostWoods.FileID() + 1,
                //GameObjects.Scene.LostWoods.FileID() + 2,
                //GameObjects.Scene.LostWoods.FileID() + 3,
            };

            int GetUnusedFileID(InjectedActor injActor)
            {
                if (freeFileSlots.Count > 0)
                {
                    var f = freeFileSlots[0];
                    freeFileSlots.RemoveAt(0);
                    return f;
                }
                else // we have run out of known free file slots to use
                {
                    // back up, its broken though
                    //return RomUtils.AppendFile(injActor.overlayBin)
                    throw new Exception("We have run out of actors space to inject, please disable an actor in /actors");
                }
            }


            // note: this code wasn't working 2023, might be working again 2026 after years of inactivity, needs testing
            foreach (var injectedActor in Enemies.InjectedActors.FindAll(act => act.ActorId == (int)ActorEnum.NULL))
            {
                /// brand new actors, not replacement
                if (injectedActor.buildVramStart == 0)
                {
                    throw new Exception("new actor missing starting vram:\n " + injectedActor.filename);
                }

                var newFileID = GetUnusedFileID(injectedActor); // todo change this back into hardcoded, its a static rom
                //var newFileID = RomUtils.AppendFile(injectedActor.overlayBin); // broken, wants to put our actor outside of romspace
                injectedActor.fileID = newFileID;
                injectedActor.ActorId = (int)freeOverlaySlots[0];
                freeOverlaySlots.RemoveAt(0);
                var file = RomData.MMFileList[newFileID];
                file.Data = injectedActor.overlayBin;
                file.WasEdited = true;
                //file.IsCompressed = true; // assumption: all actors are compressed
                file.IsCompressed = false; // leaving true was removed under suspicion of injected actor breaking
                file.Cmp_End = 0x0;

                // update actor ID in overlay init vars, now that we know the new actor ID value
                ReadWriteUtils.Arr_WriteU16(file.Data, (int)injectedActor.initVarsLocation, (ushort)injectedActor.ActorId);

                var filenameSplit = injectedActor.filename.Split("\\");
                var newActorName = filenameSplit[filenameSplit.Length - 1];

                RomData.MMFileList[newFileID] = file;
                Enemies.ReplacementCandidateList.Add(new ActorInst(injectedActor, newActorName));

                log.WriteLine($"New actor [{injectedActor.filename}] injected at actorId [0x{injectedActor.ActorId.ToString("X")}] fid [{injectedActor.fileID}]");

                // TODO inject objects too, for actors that have custom objects

            } // end for each injected actor
        }
    }
}
