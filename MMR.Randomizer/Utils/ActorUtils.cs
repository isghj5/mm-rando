﻿using System.Collections.Generic;
using System.Linq;

using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using System.Diagnostics;

using System.Runtime.CompilerServices;


namespace MMR.Randomizer.Utils
{
    class ActorUtils
    {
        public static void LowerEnemiesResourceLoad(List<GameObjects.Actor> VanillaEnemyList)
        {
            /// some enemies are really hard on the CPU/RSP, we can change some of them to behave nicer with flags
            /// this might be placebo, and not have a major effect on us at all

            var actorList = VanillaEnemyList.Where(u => u.ActorInitOffset() > 0).ToList();
            var dinofos = GameObjects.Actor.Dinofos;

            // separated because for some reason this can cause cutscene dinofos to delay and even softlock
            RomUtils.CheckCompressed(dinofos.FileListIndex());
            RomData.MMFileList[dinofos.FileListIndex()].Data[dinofos.ActorInitOffset() + 7] &= 0xBF;// 4 flag is the issue
            actorList.Remove(dinofos); // breaks his camera
            actorList.Remove(GameObjects.Actor.Eyegore);

            foreach (var enemy in actorList)
            {
                /// bit flags 4-6 according to crookedpoe: Always Run update, Always Draw, Never Cull
                var fid = GetFID((int)enemy);
                RomUtils.CheckCompressed(fid);
                RomData.MMFileList[fid].Data[enemy.ActorInitOffset() + 7] &= 0x8F;
            }
        }

        public static int GetFID(int actorID)
        {
            var fidLookup = ((GameObjects.Actor)actorID).FileListIndex();
            if (fidLookup != -1)
            {
                return fidLookup;
            }

            // if we want to know the file ID of an actor, we need to look up the VROM addr from the overlay table
            // and match against a file in DMA, because nintendo removed the FID from the overlay table
            // all actors should have their FID coded in the enum now, this is depreciated but left as backup
            return RomUtils.GetFIDFromVROM(GetOvlActorVROMStart(actorID));
        }

        public static int GetOvlActorVROMStart(int actorOvlTblIndex)
        {
            /// we might want to look up the file vrom address for an actor so we can find its FID
            /// dont look at me like that, it's nintendo's fault the retail cart doesn't have this in the overlay table, they removed it

            /// actor's vrom addr exists in the overlay table so the code can load the file
            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);

            // the overlay table exists inside of another file, we need the offset to the table
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            // xxxxxxxx yyyyyyyy aaaaaaaa bbbbbbbb pppppppp iiiiiiii nnnnnnnn ???? cc ??
            // x should be our vrom address start
            return (int)(ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 0));
        }


        public static int GetOvlCodeRamSize(int actorOvlTblIndex)
        {
            /// this is the size of overlay (actor) code in ram
            /// to get it, we can just diff the

            if (actorOvlTblIndex == (int)GameObjects.Actor.Empty || actorOvlTblIndex == 0)
            {
                return 0;
            }

            /// actor overlay size already exists in the actor overlay table, just look it up from the index
            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);
            // the overlay table exists inside of another file, we need the offset to the table
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            // xxxxxxxx yyyyyyyy aaaaaaaa bbbbbbbb pppppppp iiiiiiii nnnnnnnn ???? cc ??
            // A and B should be start and end of vram address, which is what we want as we want the ram size
            return (int)(ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 12)
                       - ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 8));
        }

        public static int GetOvlInstanceRamSize(int actorOvlTblIndex, List<InjectedActor> injectedActors)
        {
            /// this is the size of the actor's struct instance in ram
            if (actorOvlTblIndex == -1) return 0; // GameObjects.Actor.Empty;

            // to get this, we either need to save it or read it from the overlay's init vars
            var attr = ((GameObjects.Actor)actorOvlTblIndex).GetAttribute<ActorInstanceSizeAttribute>();
            if (attr != null)
            {
                return attr.Size;
            }

            // if its an injected actor, we get from the actor not the vanilla rom
            InjectedActor injectedActor = injectedActors.Find(u => u.actorID == actorOvlTblIndex);
            if (injectedActor != null && injectedActor.overlayBin != null)
            {
                // E/F are the actor's instance size
                // no check compressed: user has to submit uncompressed actor binary
                return ReadWriteUtils.Arr_ReadU16(injectedActor.overlayBin, (int)injectedActor.initVarsLocation + 0xE);
            }

            // if we didn't pre-save it, we need to extract it
            var ovlFID = ActorUtils.GetFID(actorOvlTblIndex); // attempt to get it from the DMA table
            if (ovlFID == -1)
            {
                return 0xABCD; // conservative estimate
            }

            var offset = GetOvlActorInit(actorOvlTblIndex);
            if (offset <= 0)
            {
                return 0x1001;
            }
            RomUtils.CheckCompressed(ovlFID);
            var ovlData = RomData.MMFileList[ovlFID].Data;
            return ReadWriteUtils.Arr_ReadU16(ovlData, offset + 0xE); // E/F are the actor's instance size

        }

        public static int GetOvlActorInit(int actorOvlTblIndex)
        {
            /// this is the offset location of the actor's init variables

            /// even though the overlay table says it has the init vars location, it doesn't get populated until after 

            // we have some of them hardcoded, if that exists return that instead because scanning files sucks
            var actor = (GameObjects.Actor)actorOvlTblIndex;
            var actorAttr = actor.GetAttribute<ActorInitVarOffsetAttribute>();
            if (actorAttr != null)
            {
                return actorAttr.Offset;
            }

            // just look it up in the actor overlay table, initvars - vram start is the offset
            var actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            // dont need to check for compression, code has been un-compressed for ages
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            // xxxxxxxx yyyyyyyy aaaaaaaa bbbbbbbb pppppppp iiiiiiii nnnnnnnn ???? cc ??
            // A should be the start of vram address, i is init vars location in vram, take diff to get offset in overlay file
            //var initloc = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 20);
            //var vramloc = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 8);
            return (int)(ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 20)
                       - ReadWriteUtils.Arr_ReadU32(actorOvlTblData, actorOvlTblOffset + (actorOvlTblIndex * 32) + 8));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int MergeRotationAndFlags(int rotation, int flags)
        {
            /// in a map's actor list: actors spawn rotation is merged with their flags,
            ///   so that the 7 most right bits are flags
            /// bits: XXXX XXXX XFFF FFFF where X is rotation, F is flags
            /// where rotation is 1 = 1 degree, 360 is 0x168, so it does use all 9 bits
            ///  looks to me like rotation increases in a counter-clockwise direction
            return ((rotation & 0x1FF) << 7) | (flags & 0x7F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FlattenPitchRoll(Actor actor)
        {
            actor.Rotation.x = (short)MergeRotationAndFlags(rotation: 0, flags: actor.Rotation.x);
            actor.Rotation.z = (short)MergeRotationAndFlags(rotation: 0, flags: actor.Rotation.z);
        }

        #region Debug Printing

        public static void PrintDataBytes(byte[] data, int location)
        {
            Debug.WriteLine("Printing data at: " + location + " hex: " + location.ToString("X2"));
            Debug.WriteLine(data[location + 0].ToString("X2"));
            Debug.WriteLine(data[location + 1].ToString("X2") + " < unk ?");
            Debug.WriteLine(data[location + 2].ToString("X2") + " < ");
            Debug.WriteLine(data[location + 3].ToString("X2") + " < ");
            Debug.WriteLine("folowing words: " + location + " hex: " + location.ToString("X2"));
            Debug.WriteLine(data[location + 4].ToString("X2"));
        }

        // temporary debugging purposes
        private static void PrintActorValues()
        {
            /*
            for (var i = 1; i < 0x2B2; ++i)
            {
                var actor = (GameObjects.Actor) i;
                var actorName = "";// actor.ToActorModel().Name;
                if (actorName.Contains("Empty")) {
                    Debug.WriteLine($"[EMPTY]");
                    continue;
                }

                // we nee to make sure ram and overlay are the same
                // read Init offset from overlay table
                var initVarString = "";
                var objVarString = "";
                var actorStructSize = GetOvlInstanceRamSize(i);
                if (actorStructSize != -1)
                {
                    initVarString = " and instance: 0x" + GetOvlInstanceRamSize(i).ToString("X5");
                }
                var objectIndex = actor.ObjectIndex();
                if (objectIndex  > 3)
                {
                    objVarString = " and obj: 0x" + ObjUtils.GetObjSize(actor.ObjectIndex()).ToString("X5");
                }
                else if (objectIndex <= 3 && objectIndex >= 0)
                {
                    objVarString = " using special obj";
                }
                else
                {
                    throw new Exception("oh no");
                }

                var filename = actorName.PadLeft(16, ' ');
                var fid = GetFID(i).ToString("D3");
                var aid = i.ToString("X3");
                var codeSize = GetOvlCodeRamSize(i).ToString("X5");

                Debug.WriteLine($"[{filename}] FID:[{fid}] AID:[{aid}] codesize: 0x{codeSize}" + initVarString + objVarString);
            } // */

            for (int i = 0; i < 0x283; ++i)
            {
                //print object id and size
                Debug.WriteLine(" obj [" + i.ToString("X3") + "] has size: " + ObjUtils.GetObjSize(i).ToString("X5"));

            } // */

            int breakpointherestupid = 0;
        }

        #endregion

        #region (Per-actor)Combat Music Disable

        // these aren't used anymore, Zoey switched us to changing combat music code to bypass the check for a flag instead
        // this is mostly for Isghj who plays with only disabling half enemy music

        public static void DisableCombatMusicOnEnemy(GameObjects.Actor actor)
        {
            int actorInitVarRomAddr = actor.GetAttribute<ActorInitVarOffsetAttribute>().Offset;
            /// each enemy actor has actor init variables, 
            /// if they have combat music is determined if a flag is set in the seventh byte
            /// disabling combat music means disabling this bit for most enemies
            int actorFileID = actor.FileListIndex();
            RomUtils.CheckCompressed(actorFileID);
            int actorFlagLocation = (actorInitVarRomAddr + 7);
            byte flagByte = RomData.MMFileList[actorFileID].Data[actorFlagLocation];
            RomData.MMFileList[actorFileID].Data[actorFlagLocation] = (byte)(flagByte & 0xFB);

            if (actor == GameObjects.Actor.DekuBabaWithered) // special case: when they regrow music returns
            {
                // when they finish regrowing their combat music bit is reset, we need to no-op this to stop it
                // 	[ori t3,t1,0x0005] which is [35 2B 00 05] becomes [35 2B 00 01] as the 4 bit is combat music, 1 is R-targetable
                RomData.MMFileList[actorFileID].Data[0x12BF] = 0x01;
            }
        }

        public static void DisableEnemyCombatMusic(bool weakEnemiesOnly = true)
        {
            /// each enemy has one int flag that contains a single bit that enables combat music
            /// to get these values I used the starting rom addr of the enemy actor
            ///  searched the ram for the actor overlay table that has rom and ram per actor,
            ///  there it lists the actor init var ram and actor ram locations, diff, apply to rom start
            ///  the combat music flag is part of the seventh byte of the actor init Variants, but our fuction knows this

            // we always disable wizrobe because he's a miniboss, 
            // but when you enter his room you hear regular combat music for a few frames before his fight really starts
            // this isn't noticed in vanilla because vanilla combat starts slow
            DisableCombatMusicOnEnemy(GameObjects.Actor.Wizrobe);

            var weakEnemyList = new GameObjects.Actor[]
            {
                GameObjects.Actor.ChuChu,
                GameObjects.Actor.SkullFish,
                GameObjects.Actor.DekuBaba,
                GameObjects.Actor.DekuBabaWithered,
                GameObjects.Actor.BioDekuBaba,
                GameObjects.Actor.RealBombchu,
                GameObjects.Actor.Guay,
                GameObjects.Actor.Wolfos,
                GameObjects.Actor.Keese,
                GameObjects.Actor.Leever,
                GameObjects.Actor.Bo,
                GameObjects.Actor.DekuBaba,
                GameObjects.Actor.Shellblade,
                GameObjects.Actor.Tektite,
                GameObjects.Actor.BadBat,
                GameObjects.Actor.Eeno,
                GameObjects.Actor.MadShrub,
                GameObjects.Actor.Nejiron,
                GameObjects.Actor.Hiploop,
                GameObjects.Actor.Octarok,
                GameObjects.Actor.Shabom,
                GameObjects.Actor.Dexihand,
                GameObjects.Actor.Freezard,
                GameObjects.Actor.Armos,
                GameObjects.Actor.Snapper,
                GameObjects.Actor.Desbreko,
                GameObjects.Actor.Poe,
                GameObjects.Actor.GibdoIkana,
                GameObjects.Actor.GibdoWell,
                GameObjects.Actor.RedBubble,
                GameObjects.Actor.Stalchild
            }.ToList();

            var annoyingEnemyList = new GameObjects.Actor[]
            {
                GameObjects.Actor.BlueBubble,
                GameObjects.Actor.LikeLike,
                GameObjects.Actor.Beamos,
                GameObjects.Actor.DeathArmos,
                GameObjects.Actor.Dinofos,
                GameObjects.Actor.DragonFly,
                GameObjects.Actor.GiantBeee,
                GameObjects.Actor.WallMaster,
                GameObjects.Actor.FloorMaster,
                GameObjects.Actor.Skulltula,
                GameObjects.Actor.ReDead,
                GameObjects.Actor.Peahat,
                GameObjects.Actor.Dodongo,
                GameObjects.Actor.Takkuri,
                GameObjects.Actor.Eyegore,
                GameObjects.Actor.IronKnuckle,
                GameObjects.Actor.PoeSisters
            }.ToList();

            var disableList = weakEnemiesOnly ? weakEnemyList : weakEnemyList.Concat(annoyingEnemyList);

            foreach (var enemy in disableList)
            {
                DisableCombatMusicOnEnemy(enemy);
            }
        }

        #endregion

    } // end class
} // end namespace
