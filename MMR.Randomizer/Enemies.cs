﻿using Microsoft.Toolkit.HighPerformance.Extensions;
using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Models.Vectors;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// dotnet 4.5 req
using System.Runtime.CompilerServices;

// todo rename this actorutils.cs and move to MMR.Randomizer/Utils/


namespace MMR.Randomizer
{
    [System.Diagnostics.DebuggerDisplay("{OldV} -> {NewV}")]

    public class ValueSwap
    {
        // these are indexes of objects
        public int OldV;
        public int NewV;
        public int ChosenV; // Copy of NewV, first pass result, but we might change NewV to something else if duplicate
    }

    [System.Diagnostics.DebuggerDisplay("0x{actorID.ToString(\"X3\")}:{fileID}")]
    public class InjectedActor
    {
        // when we inject a new actor theres some data we need
        // and some adjustments we need to make based on where it gets placed in vram
        public int actorID = 0;
        public int objID   = 0;
        public int fileID  = 0;

        // if all new actor, we meed to know where the old vram start was when we shift VRAM for the actor
        public uint buildVramStart = 0;
        // init vars are located somewhere in .data, we want to know where exactly because its hard coded in overlay table
        public uint initVarsLocation = 0;

        public List<int> groundVariants = new List<int>();
        public List<int> respawningVariants = new List<int>();
        // variants with max
        public List<VariantsWithRoomMax> limitedVariants = new List<VariantsWithRoomMax>();
        public UnkillableAllVariantsAttribute unkillableAttr = null;
        public OnlyOneActorPerRoom onlyOnePerRoom = null;

        // should only be stored here if new actor
        public byte[] overlayBin;
        public string filename = ""; // debugging
    }

    public class Enemies
    {
        public static List<InjectedActor> InjectedActors = new List<InjectedActor>();
        const int SMALLEST_OBJ = 0xF3;

        private static List<GameObjects.Actor> VanillaEnemyList { get; set; }
        private static List<Actor> ReplacementCandidateList { get; set; }
        private static List<Actor> FreeCandidateList { get; set; }
        private static List<Actor> FreeOnlyCandidateList { get; set; } // not worthy by themselves, only if object was already selected
        private static Mutex EnemizerLogMutex = new Mutex();
        private static bool ACTORSENABLED = true;
        private static Random seedrng;

        public static void PrepareEnemyLists()
        {

            // list of slots to use
            VanillaEnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.ObjectIndex() > 3
                                && (u.IsEnemyRandomized() || (ACTORSENABLED && u.IsActorRandomized()))) // both
                            .ToList();

            /* var EmemiesOnly = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.ObjectIndex() > 3
                                && (u.IsEnemyRandomized()))
                            .ToList();
            //*/

            // list of replacement actors we can use to replace with
            // for now they are the same, in the future players will control how they load
            ReplacementCandidateList = new List<Actor>();
            //foreach (var actor in EmemiesOnly)
            foreach (var actor in VanillaEnemyList)
            {
                ReplacementCandidateList.Add(new Actor(actor));
            }

            var freeCandidates = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                                .Where(u => u.ObjectIndex() <= 3
                                && (u.IsEnemyRandomized() || (ACTORSENABLED && u.IsActorRandomized())))
                                .ToList();


            // because this list needs to be re-evaluated per scene, start smaller here once
            FreeCandidateList = freeCandidates.Select(u => new Actor(u)).ToList();

            var freeOnlyCandidates = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                    .Where(u => ACTORSENABLED && u.IsActorFreeOnly())
                    .ToList();

            // because this list needs to be re-evaluated per scene, start smaller here once
            FreeOnlyCandidateList = freeOnlyCandidates.Select(u => new Actor(u)).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReplacementListContains(GameObjects.Actor actor)
        {
            return ReplacementCandidateList.Find(u => u.ActorEnum == actor) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReplacementListRemove(List<Actor> replaceList, GameObjects.Actor actor)
        {
            // might be an easier one liner but this could get used a lot
            var removeActor = replaceList.Find(u => u.ActorEnum == actor);
            if (removeActor != null)
            {
                replaceList.Remove(removeActor);
            }
        }

        #region Read and Write Scene Actors and Objects

        public static List<Actor> GetSceneEnemyActors(Scene scene)
        {
            /// this is separate from object because actors and objects are a different list in the scene/room data

            // I prefer foreach, but in benchmarks it's considerably slower, and enemizer has performance issues

            var SceneEnemyList = new List<Actor>();
            for (int mapNumber = 0; mapNumber < scene.Maps.Count; ++mapNumber)
            {
                for (int actorNumber = 0; actorNumber < scene.Maps[mapNumber].Actors.Count; ++actorNumber) // (var mapActor in scene.Maps[mapNumber].Actors)
                {
                    var mapActor = scene.Maps[mapNumber].Actors[actorNumber];
                    var matchingEnemy = VanillaEnemyList.Find(u => (int)u == mapActor.ActorID);
                    if (matchingEnemy > 0) {
                        var listOfAcceptableVariants = matchingEnemy.AllVariants();
                        //var listOfAcceptableVariants = matchingEnemy.vVariants;
                        if (!matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum)
                            && listOfAcceptableVariants.Contains(mapActor.OldVariant))
                        {
                            // since not all actors are usable, save doing some of this work only for actors we actually want to modify
                            // do this only after we know this is an actor we want
                            mapActor.Name = mapActor.ActorEnum.ToString();
                            mapActor.ObjectSize = ObjUtils.GetObjSize(mapActor.ActorEnum.ObjectIndex());
                            mapActor.MustNotRespawn = scene.SceneEnum.IsClearEnemyPuzzleRoom(mapNumber)
                                                   || scene.SceneEnum.IsFairyDroppingEnemy(mapNumber, actorNumber);
                            mapActor.RoomActorIndex = scene.Maps[mapNumber].Actors.IndexOf(mapActor);
                            mapActor.Type = matchingEnemy.GetType(mapActor.OldVariant);
                            mapActor.AllVariants = Actor.BuildVariantList(matchingEnemy);
                            SceneEnemyList.Add(mapActor);
                        }
                    }
                }
            }
            return SceneEnemyList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
            /// this is separate from actor because actors and objects are a different list in the scene/room data

            List<int> objList = new List<int>();
            foreach (var sceneMap in scene.Maps)
            {
                foreach (var mapObject in sceneMap.Objects)
                {
                    var matchingEnemy = VanillaEnemyList.Find(u => u.ObjectIndex() == mapObject);
                    if (matchingEnemy > 0                                               // exists in the list
                       && !objList.Contains(matchingEnemy.ObjectIndex())                     // not already extracted from this scene
                       && !matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum)) // not excluded from being extracted from this scene
                    {
                        objList.Add(matchingEnemy.ObjectIndex());
                    }
                }
            }
            return objList;
        }

        // depreciated if the following works
        public static void SetSceneEnemyObjects(Scene scene, List<ValueSwap> newObjects)
        {
            foreach (var sceneMap in scene.Maps)
            {
                for (int sceneObjIndex = 0; sceneObjIndex < sceneMap.Objects.Count; sceneObjIndex++)
                {
                    var valueSwapObject = newObjects.Find(u => u.OldV == sceneMap.Objects[sceneObjIndex]);
                    if (valueSwapObject != null)
                    {
                        sceneMap.Objects[sceneObjIndex] = valueSwapObject.NewV;
                    }
                }
            }
        }

        public static void SetSceneEnemyObjects(Scene scene, List<List<int>> newObjectsPerMap)
        {
            for (var map = 0; map < scene.Maps.Count; map++)// sceneMap in scene.Maps)
            {
                var objectsPerMap = newObjectsPerMap[map];
                var sceneMap = scene.Maps[map];
                for (int sceneObjIndex = 0; sceneObjIndex < objectsPerMap.Count; sceneObjIndex++)
                {
                    sceneMap.Objects[sceneObjIndex] = objectsPerMap[sceneObjIndex];
                }
            }
        }

        #endregion

        #region Static Enemizer Changes and Fixes

        private static void EnemizerEarlyFixes()
        {
            // changes before randomization
            FixSpecificLikeLikeTypes();
            EnableDampeHouseWallMaster();
            EnableTwinIslandsSpringSkullfish();
            FixSouthernSwampDekuBaba();
            FixRoadToSouthernSwampBadBat();
            NudgeFlyingEnemiesForTingle();
            FixScarecrowTalk();
            EnablePoFusenAnywhere();

            FixSpawnLocations();
            ExtendGrottoDirectIndexByte();
            ShortenChickenPatience();
            //FixThornTraps();
            FixSeth2();
            AllowGuruGuruOutside();
            RemoveSTTUnusedPoe();
            FixSilverIshi();
            FixBabaAndDragonflyShadows();
            AddGrottoVariety();

            Shinanigans();
        }

        public static void EnemizerLateFixes()
        {
            // changes after randomization
            FixDekuPalaceReceptionGuards();
        }

        public static void FixSpawnLocations()
        {
            /// in Enemizer some spawn locations are noticably buggy
            ///   example: one of the eeno in north termina field is too high above the ground, 
            ///    we never notice because it falls to the ground before we can get there normally
            ///    but if its a stationary enemy, like a dekubaba, it hovers in the air

            var terminafieldScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TerminaField.FileID());
            terminafieldScene.Maps[0].Actors[144].Position.y = -245; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[16].Position.y = -209; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[17].Position.y = -185; // fixes the eeno that is too high above ground (bombchu explode)
            terminafieldScene.Maps[0].Actors[60].Position.y = -60; // fixes the blue bubble that is too high
            terminafieldScene.Maps[0].Actors[107].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[110].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[121].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[153].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)

            // the two wolfos spawn in twin islands spawn off scew, 
            //   redead falls through the floor when you approach them with this skew
            var twinislandsRoom0FID = GameObjects.Scene.TwinIslands.FileID() + 1;
            RomUtils.CheckCompressed(twinislandsRoom0FID);
            var twinislandsScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TwinIslands.FileID());
            ActorUtils.FlattenPitchRoll(twinislandsScene.Maps[0].Actors[26]);
            ActorUtils.FlattenPitchRoll(twinislandsScene.Maps[0].Actors[27]);

            // in STT, move the bombchu in the first room 
            //   backward several feet from the chest, so replacement cannot block the chest
            var stonetowertempleScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.StoneTowerTemple.FileID());
            stonetowertempleScene.Maps[0].Actors[3].Position.z = -630;
            // biobaba in the right room spawns under the bridge, if octarock it pops up through the tile, move to the side of the bridge
            stonetowertempleScene.Maps[3].Actors[19].Position.x = 1530;

            // in WFT, the dinofos spawn is near the roof, lower
            // TODO: do secret shrine too maybe if we randomize
            var woodfalltempleScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.WoodfallTemple.FileID());
            woodfalltempleScene.Maps[7].Actors[0].Position.y = -1208;

            // in OSH, the storage room bo spawns in the air in front of the mirror, 
            //  but as a land enemy it should be placed on the ground for its replacements
            var oceanspiderhouseScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.OceanSpiderHouse.FileID());
            var storageroomBo = oceanspiderhouseScene.Maps[5].Actors[2];
            // lower to the floor 
            storageroomBo.Position = new vec16(-726, -118, -1651);

            // in GBT, the bombchus on the pipes are in bad spots to be replaced by something unpassable,
            // but most people dont notice where their original spawn even is so move them
            var greatbaytempleScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.GreatBayTemple.FileID());
            // the bombchu along the green pipe in the double seesaw room needs to be moved in case its an unmovable enemy
            greatbaytempleScene.Maps[10].Actors[3].Position = new vec16(3525, -180, 630);
            // the bombchu along the red pipe in the pre-wart room needs the same kind of moving
            greatbaytempleScene.Maps[6].Actors[7].Position = new vec16(-1840, -570, -870);

            var linkTrialScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.LinkTrial.FileID());
            linkTrialScene.Maps[1].Actors[0].Position.y = 1; // up high dinofos spawn, red bubble would spawn in the air, lower to ground

            if (ACTORSENABLED)
            {
                var dekuPalaceScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.DekuPalace.FileID());
                var torchRotation = dekuPalaceScene.Maps[2].Actors[26].Rotation.z;
                torchRotation = (short)ActorUtils.MergeRotationAndFlags(rotation: 180, flags: torchRotation); // reverse, so replacement isn't nose into the wall

                // change the torch in pirates fort exterior to all day, remove second one, or free 
                var piratesExteriorScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.PiratesFortressExterior.FileID());
                var nightTorch = piratesExteriorScene.Maps[0].Actors[15];
                nightTorch.Rotation.x |= 0x7F; // always spawn flags
                nightTorch.Rotation.z |= 0x7F;

                // day torch
                piratesExteriorScene.Maps[0].Actors[13].ChangeActor(GameObjects.Actor.Empty, modifyOld: true); // dangeon object so no grotto, empty for now
                // todo: 14/16 are also torches, we dont really need both here

                // anju's actor spawns behind the inn door, move her to be visible in sct
                var eastclocktownScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.EastClockTown.FileID());
                var anju = eastclocktownScene.Maps[0].Actors[0];
                anju.Position = new vec16(-101, 5, 180);
                //anju.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 270, flags: anju.Rotation.y); // rotate to away from us

                // move next to mayors building
                // bug this is not next to mayor building for some reason, next to inn
                var gorman = eastclocktownScene.Maps[0].Actors[4];
                gorman.Position = new vec16(1026, 200, -1947);

                // if actors are rando'd then the carpenters proabbly are too
                var southClockTownScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.SouthClockTown.FileID());
                var carpenterSound = southClockTownScene.Maps[0].Actors[49];
                carpenterSound.ChangeActor(GameObjects.Actor.Carpenter, vars: 1, modifyOld: true); // non-pathing type
                carpenterSound.Position.z = 55; // move forward to muto placement
                // we can also hear the noises in west/east, those actors should also be removed
                eastclocktownScene.Maps[0].Actors[63].ChangeActor(GameObjects.Actor.Empty, modifyOld: true); // FREE ACTOR ?

                // should we rando the tower?
            }
        }



        private static void Shinanigans()
        {
            var grottosScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.Grottos.FileID());
            grottosScene.Maps[13].Actors[1].Variants[0] = 1; // change the grass in peahat grotto to drop items like TF grass

            if (ACTORSENABLED)
            {

                //turn around this torch, because if its bean man hes facing into the wall and it hurts me
                var laundryPoolScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.LaundryPool.FileID());
                laundryPoolScene.Maps[0].Actors[2].Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 135, flags: 0x7F);
                laundryPoolScene.Maps[0].Actors[2].Rotation.x = 0x7F;
                laundryPoolScene.Maps[0].Actors[2].Rotation.z = 0x7F;
                //laundryPoolScene.Maps[0].Actors[1].Rotation.z = (short)ActorUtils.MergeRotationAndFlags(rotation: laundryPoolScene.Maps[0].Actors[1].Rotation.z, flags: 0x7F);

                // it was two torches, turn the other into a secret grotto, at least for now
                var randomGrotto = new List<ushort> { 0x6033, 0x603B, 0x6018, 0x605C, 0x8000, 0xA000, 0x7000, 0xC000, 0xE000, 0xF000, 0xD000 };
                var hiddenGrottos = new List<ushort> { 0x6233, 0x623B, 0x6218, 0x625C, 0x8200, 0xA200, 0x7200, 0xC200, 0xE200, 0xF200, 0xD200 };
                laundryPoolScene.Maps[0].Actors[1].ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[seedrng.Next(randomGrotto.Count)], modifyOld: true);
                laundryPoolScene.Maps[0].Actors[1].Rotation = new vec16(0x7f, 0x7f, 0x7f);
                laundryPoolScene.Maps[0].Actors[1].Position = new vec16(-1502, 35, 555); // old: new vec16(-1872, -120, 229);

                // winter village has a gossip stone actor, but no object, lets use the non-used flying darmani ghost object and add it to enemizer
                var winterVillage = RomData.SceneList.Find(u => u.File == GameObjects.Scene.MountainVillage.FileID());
                winterVillage.Maps[0].Objects[5] = GameObjects.Actor.GossipStone.ObjectIndex();
                winterVillage.Maps[0].Actors[57].Variants[0] = 0x67; // the vars is for milkroad, change to a moon vars so it gets randomized
                winterVillage.Maps[0].Actors[57].Position.y = -15; // floating a bit in the air, lower to ground

                // now that darmani ghost is gone, lets re=use the actor for secret grotto
                winterVillage.Maps[0].Actors[2].ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[seedrng.Next(randomGrotto.Count)] & 0xFCFF, modifyOld: true);
                //winterVillage.Maps[0].Actors[2].ChangeActor(GameObjects.Actor.GrottoHole, vars: 0x4000, modifyOld:true);
                winterVillage.Maps[0].Actors[2].Position = new vec16(504, 365, 800);

                var terminafieldScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TerminaField.FileID());
                var elf6grotto = terminafieldScene.Maps[0].Actors[2];
                elf6grotto.Position = new vec16(-5539, -275, -701);
                elf6grotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: hiddenGrottos[seedrng.Next(hiddenGrottos.Count)], modifyOld: true);

                // one of the torches in palace is facing into the wall, actors replacing it also face the same way, bad
                // one of these is not required and does nothing
                var dekuPalaceScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.DekuPalace.FileID());
                dekuPalaceScene.Maps[2].Actors[25].Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 180, flags: 0x7F);
                dekuPalaceScene.Maps[2].Actors[26].Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 180, flags: dekuPalaceScene.Maps[2].Actors[26].Rotation.y);

                // RecreateFishing();
            }

            // testing why zrotation can be so broken for grottos
            /* 
            var testScene = GameObjects.Scene.TerminaField;
            var grottoSceneIndex = RomData.SceneList.FindIndex(u => u.File == testScene.FileID());
            var grottoSceneActorAddr = RomData.SceneList[grottoSceneIndex].Maps[0].ActorAddr;
            int actorNumber = 211;
            // set actor value
            //RomData.MMFileList[grottoRoom0FID].Data[grottoSceneActorAddr + (actorNumber * 16) + 1] = 0x55; // set actor to grotto
            RomData.SceneList[grottoSceneIndex].Maps[0].Actors[actorNumber].ActorEnum = GameObjects.Actor.GrottoHole;
            RomData.SceneList[grottoSceneIndex].Maps[0].Actors[actorNumber].ActorID = (int)GameObjects.Actor.GrottoHole;
            //RomData.SceneList[grottoSceneIndex].Maps[0].Actors[actorNumber].Variants[0] = 0x625C; // working, hidden generic grotto with mystery woods grotto chest
            RomData.SceneList[grottoSceneIndex].Maps[0].Actors[actorNumber].Variants[0] = 0x8200; // hidden jgrotto

            RomData.SceneList[grottoSceneIndex].Maps[0].Actors[actorNumber].Rotation.z = 0x0200; // ignored if top nibble is set to > 0
            // */

            // I like secrets
            var twinislandsScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TwinIslands.FileID());
            //twinislandsScene.Maps[0].Actors[1].Position = new vec16(-583, 140, -20); // place: next to tree, testing
            twinislandsScene.Maps[0].Actors[1].Position = new vec16(349, -196, 970); // place: under the ice, sneaky like teh crabb
            //twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x60CB; // set to unk check
            // 300 is back to mountain village
            // 303 is empty, it takes us to mayors office, which might mean we can put an address tehre 
            twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x0303; // set to spring goron race?
            //twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x7200; // invisible

            // spring has ONE exit, which means pad space is free realestate
            RomUtils.CheckCompressed(GameObjects.Scene.TwinIslands.FileID());
            var twinislandsSceneData = RomData.MMFileList[GameObjects.Scene.TwinIslands.FileID()].Data;
            twinislandsSceneData[0xD6] = 0xAE;
            twinislandsSceneData[0xD7] = 0x50; // 50 is behind the waterfall 

            //PrintActorValues();
        }

        public static void DisableAllLocationRestrictions()
        {
            // experimental, we can eliminate player restrictions in areas
            //var restrictionTableVRAMStart = 0x801BF6C0; // 0xC55C00 -> DC4 // offset: 119C00
            var tableOffset = 0x119C00;
            var codeFile = RomData.MMFileList[31].Data;
            while (tableOffset < 0x119DC4)
            {
                // 0 offset is the scene value (why though)
                codeFile[tableOffset + 1] = 0x00;
                codeFile[tableOffset + 2] = 0x00;
                codeFile[tableOffset + 3] = 0x00;
                tableOffset += 4;
            }
        }

        private static void FixScarecrowTalk()
        {
            /// scarecrow breaks if you try to teach him a song anywhere where he normally does not exist
            if (!ReplacementListContains(GameObjects.Actor.Scarecrow))
            {
                return;
            }

            var scarecrowFID = GameObjects.Actor.Scarecrow.FileListIndex();
            RomUtils.CheckCompressed(scarecrowFID);
            var scarecrowFile = RomData.MMFileList[scarecrowFID].Data;

            // song teaching scarecrow gets stuck after song is done
            // the kakasi code tries to start a cutscene in stages per frame
            // first frame: tell game you want to start cutscene, second frame check if cs available to start... we never succeed here
            // so the code repeats going to the same spot over and over, never advancing
            // instead, we can just branch from that spot to the finish code

            ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x11E0, 0x1000000F); // branch F down past the if (if state == 1)

            // however thats not the only issue, if you teach a song in TF before breaking the ice block, it triggers the ice break cutscene
            // so we have to stop the cutscenes call
            // cutscene call for songteaching camera swinging
            // ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x1100, 0x00000000); // NOP the ActorCutscene_SetIntentToPlay

            // cutscene call after twirl
            // ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x1100, 0x00000000); // NOP the ActorCutscene_SetIntentToPlay

            // UNFINISHED: TODO keep going, I think I have to change one of the function straight to digging away and skip dialogue because that function is long
        }

        /// <summary>
        /// Moves the deku baba in southern swamp
        ///   why? beacuse they are positioned in the elbow and its visually jarring when they spawn/despawn on room swap
        ///   its already noticable in vanilla, but with mixed enemy rando it can cause whole new enemies to pop in and out
        /// </summary>
        public static void FixSouthernSwampDekuBaba()
        {
            Scene southernswampScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.SouthernSwamp.FileID());

            // because this room is already borderline lag fest, turn one into a lillypad
            // actor 7 is the furthest back in the cave, unreachable
            var newLilyPad = southernswampScene.Maps[0].Actors[6];
            newLilyPad.ChangeActor(GameObjects.Actor.Lilypad, vars: 0, modifyOld: true);
            newLilyPad.Position = new vec16(561, 0, 790); // placement: toward back wall behind tourist center

            var movedToTree = southernswampScene.Maps[0].Actors[4];
            movedToTree.Position = new vec16(2020, 22, 300); // placement: to the right as you approach witches, next to tree
            // rotation normal to wall behind it, turn to the right 90deg
            movedToTree.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 270, flags: southernswampScene.Maps[0].Actors[4].Rotation.y);

            // witch area babas
            var movedToGrass = southernswampScene.Maps[2].Actors[2];
            movedToGrass.Position = new vec16(2910, 14, -1075); // placement: between the bushes along the wall
            // rotation normal to wall behind it, turn to the left 90deg
            movedToGrass.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 90, flags: southernswampScene.Maps[2].Actors[2].Rotation.y);

            var movedToWaterFall = southernswampScene.Maps[2].Actors[3];
            movedToWaterFall.Position = new vec16(4240, -2, -1270); // placement: near waterfall
        }

        private static void FixRoadToSouthernSwampBadBat()
        {
            /// bad bat can randomize as a wall enemy or flying enemy, 
            ///   so move all flying ones to places where they can fit in as wall enemies or fly off

            var roadtoswampScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.RoadToSouthernSwamp.FileID());
            // move tree top bat down the tree vines
            var movedDownTreeBat = roadtoswampScene.Maps[0].Actors[7];
            //movedDownTreeBat.Position = new vec16(-420, -40, 2059); // placement: lower along the tree like the other bat
            movedDownTreeBat.Position = new vec16(927, -29, 2542); // placement: along the south east corner

            // match rotation with the other tree sitting bat
            movedDownTreeBat.Rotation.y = 90;
            ActorUtils.FlattenPitchRoll(roadtoswampScene.Maps[0].Actors[7]);

            // move corridor bat to the short cliff wall near swamp shooting galery
            var movedToCliffBat = roadtoswampScene.Maps[0].Actors[6];
            movedToCliffBat.Position = new vec16(2432, -40, 2871);
            // match rotation with the other tree sitting bat
            movedToCliffBat.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 90, flags: roadtoswampScene.Maps[0].Actors[6].Rotation.y);

            // because the third bat was moved out of center corridor back, move one of the baba forward, we're basically swapping them
            var movedForwardDekuBaba = roadtoswampScene.Maps[0].Actors[14];
            movedForwardDekuBaba.Position.x = 1990;
            movedForwardDekuBaba.Position.z = 2594;
            movedForwardDekuBaba.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(rotation: 195, flags: roadtoswampScene.Maps[0].Actors[14].Rotation.y);
        }

        private static void FixSpecificLikeLikeTypes()
        {
            /// some likelikes dont follow the normal water/ground type variety, we want detection to correctly ID them
            ///  here we switch their types to match for replacement in enemizer auto-detection

            var coastScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.GreatBayCoast.FileID());
            // coast: shallow water likelike along the pillars is ground, should be water
            coastScene.Maps[0].Actors[21].Variants[0] = 2;
            // coast: bottom of the ocean east is ground, should be water
            coastScene.Maps[0].Actors[24].Variants[0] = 2;
            // coast: tidepool likelike is water
            coastScene.Maps[0].Actors[20].Variants[0] = 2;

            // cleared coast likelikes
            coastScene.Maps[1].Actors[43].Variants[0] = 2;
            coastScene.Maps[1].Actors[44].Variants[0] = 2;
            coastScene.Maps[1].Actors[46].Variants[0] = 2;
        }

        private static void EnableDampeHouseWallMaster()
        {
            /// dampe's house wallmaster is an enounter actor, not a regular wallmaster,
            ///  we have to switch it to regular enemy for enemizer shuffle to find and replace it

            var dampehouseScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.DampesHouse.FileID());
            var wallmaster = dampehouseScene.Maps[0].Actors[0];
            // move to center of the main room,
            wallmaster.Position.z = 0x40;
            // previous encounter actor used rotation as parameters, flatten rotation now for replacement
            ActorUtils.FlattenPitchRoll(wallmaster);
            // change actor to wallmaster proper for enemizer detection
            wallmaster.ChangeActor(newActorType: GameObjects.Actor.WallMaster, vars: 0x1, modifyOld: true);
        }

        private static void EnableTwinIslandsSpringSkullfish()
        {
            /// the skullfish in twinislands spring are an encounter actor, not regular skullfish actors
            ///  we have to switch them to regular skullfish for enemizer shuffle to find and replace them
            /// also we move them out of the cave in case its a water surface enemy, and to spread them out
            ///  default they are all stacked on top of the cave chest 

            var twinislandsspringScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            var encounter1 = twinislandsspringScene.Maps[0].Actors[21];
            encounter1.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter1); // flatten encounter rotation (rotation parameters
            // move to just outside cave (east)
            encounter1.Position = new vec16(-317, 0, -881);

            var encounter2 = twinislandsspringScene.Maps[0].Actors[27];
            encounter2.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter2); // flatten encounter rotation (rotation parameters
            // move to just outside cave (west)
            encounter2.Position = new vec16(-200, 0, -890);

            var encounter3 = twinislandsspringScene.Maps[0].Actors[28];
            encounter3.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter3); // flatten encounter rotation (rotation parameters
            // move to near chest on the south side
            encounter3.Position = new vec16(300, 0, 700);
        }

        public static void NudgeFlyingEnemiesForTingle()
        {
            /// if tingle can be randomized, he can end up on any flying enemy in scenes that don't already have a tingle
            /// some of these scenes would drop him into water or off the cliff where he cannot be reached
            if (!ReplacementListContains(GameObjects.Actor.Tingle))
            {
                return;
            }

            var woodfallexteriorScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.Woodfall.FileID());
            var firstDragonfly = woodfallexteriorScene.Maps[0].Actors[4];
            firstDragonfly.Position.x = 990; // over a deku scrub
            firstDragonfly.Position.z = 690;

            var secondDragonfly = woodfallexteriorScene.Maps[0].Actors[5];
            secondDragonfly.Position.x = 615; // over a lillypad
            secondDragonfly.Position.z = -495;

            var lilypad = woodfallexteriorScene.Maps[0].Actors[37];
            lilypad.Position.x = 615; // move lilypad over
            lilypad.Position.z = -495;

            var coastScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.GreatBayCoast.FileID());
            coastScene.Maps[0].Actors[17].Position.z = 3033; // edge the guay over the land just a bit

            // to prevent him from falling to abyss
            var snowheadKeese = RomData.SceneList.Find(u => u.File == GameObjects.Scene.Snowhead.FileID()).Maps[0].Actors[0];
            snowheadKeese.Position.x = -758;
        }

        private static void ExtendGrottoDirectIndexByte()
        {
            /// in MM the top nibble of the grotto variable is never used, 
            /// but in the vanilla code it be detected and used as a grotto warp index of the static grottos entrances array (-1)
            /// MM normally uses the z rotation instead to index warp, but we can use either or
            /// however, only the 3 lower bits of this nibble are used, the code ANDS with 7
            /// why? the fourth bit isn't ever used by any grotto, and looking at the code shows it is never used
            /// so here, we set the ANDI 7 to F instead, allowing us extended access to the entrance array
            var grotholeFID = GameObjects.Actor.GrottoHole.FileListIndex();
            RomUtils.CheckCompressed(grotholeFID);
            RomData.MMFileList[grotholeFID].Data[0x2FF] = 0xF; // ANDI 0x7 -> ANDI 0xF
        }

        private static void EnablePoFusenAnywhere()
        {
            /// the flying poe baloon romani uses to play her game doesn't spawn unless
            ///  1) it has an explosion fuse timer OR
            ///  2) it detects romani actor in the scene, so it can count baloon pops
            /// but the code that blocks the baloon if neither of these are true is nop-able,
            ///   and the rest of the code is designed to work without issue in this case

            if (!ReplacementListContains(GameObjects.Actor.PoeBalloon))
            {
                return;
            }

            var enPoFusenFID = GameObjects.Actor.PoeBalloon.FileListIndex();
            RomUtils.CheckCompressed(enPoFusenFID);

            // nops the MarkForDeath function call, stops them from de-spawning
            ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[enPoFusenFID].Data, Dest: 0xF4, val: 0x00000000);

            // because they can now show up in weird places, they need to be poppable more ways
            RomData.MMFileList[enPoFusenFID].Data[0xB5D] = 0xF1; // stick
            RomData.MMFileList[enPoFusenFID].Data[0xB5F] = 0xF1; // bombs
            RomData.MMFileList[enPoFusenFID].Data[0xB60] = 0xF1; // zora fins
            RomData.MMFileList[enPoFusenFID].Data[0xB63] = 0xF1; // hookshot
            RomData.MMFileList[enPoFusenFID].Data[0xB65] = 0xF1; // swords
            RomData.MMFileList[enPoFusenFID].Data[0xB6C] = 0xF1; // deku bubble
            RomData.MMFileList[enPoFusenFID].Data[0xB6F] = 0xF1; // zora barier
            RomData.MMFileList[enPoFusenFID].Data[0xB72] = 0xF1; // bush throw
            RomData.MMFileList[enPoFusenFID].Data[0xB73] = 0xF1; // zora karate
            RomData.MMFileList[enPoFusenFID].Data[0xB75] = 0xF1; // fd beam
        }

        public static void ShortenChickenPatience()
        {
            /// Cuccos take too many hits before they get mad, let's shrink this
            /// niw health is `rand(0-9.9) + 10.0` (10-20 hits), lets replace with 0-2 + 1

            if (!ReplacementListContains(GameObjects.Actor.FriendlyCucco))
            {
                return;
            }

            RomUtils.CheckCompressed(GameObjects.Actor.FriendlyCucco.FileListIndex());
            var niwData = RomData.MMFileList[GameObjects.Actor.FriendlyCucco.FileListIndex()].Data;
            // both of these changes made in EnNiw_Init
            ReadWriteUtils.Arr_WriteU32(niwData, 0x24A8, 0x40000000); // 9.9 -> 2 in f32 (in rodata)
            ReadWriteUtils.Arr_WriteU16(niwData, 0x156, 0x3F80); // 10 -> 1 in f32 (first short only as literal hardcoded)
        }

        public static void FixThornTraps()
        {
            // this is incomplete, fixing thorn traps will likely take rewriting code not just removing

            /// in thorn traps init code it checks if a path has only 2 nodes, if it has more or less than 2 it dies

            // let's just remove that jal
            var location = 0x3A8;// 234 * 4;
            RomUtils.CheckCompressed(GameObjects.Actor.ThornTrap.FileListIndex());
            var thornData = RomData.MMFileList[GameObjects.Actor.ThornTrap.FileListIndex()].Data;

            ReadWriteUtils.Arr_WriteU32(thornData, location, 0x00000000);
            ReadWriteUtils.Arr_WriteU32(thornData, 0x378, 0x00000000);
        }

        public static void FixSeth2()
        {
            /// seth 2, the guy waving his arms in the termina field telescope, like oot spiderhouse
            /// his init code checks for a value, and does not spawn if the value is different than expected
            if (!ReplacementListContains(GameObjects.Actor.Seth2))
            {
                return;
            }

            var sethFid = GameObjects.Actor.Seth2.FileListIndex();
            RomUtils.CheckCompressed(sethFid);
            var sethData = RomData.MMFileList[sethFid].Data;
            //nopping the mark for death
            ReadWriteUtils.Arr_WriteU32(sethData, 0x88, 0x00000000);
            //nopping the early return
            ReadWriteUtils.Arr_WriteU32(sethData, 0x90, 0x00000000);

            //weirdly, even though the the telescope is a different SCENE, seth2 is found in the regular gamplay scene, his code just kills him
            // until I move him hes in a bad spot on top of grottos, for now just kill him
            // TODO: Free actor slots? 
            var tfScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TerminaField.FileID());
            tfScene.Maps[0].Actors[28].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Actors[29].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Objects[21] = GameObjects.Actor.Empty.ObjectIndex();
            //var map = tfScene.Maps[0];
        }

        private static void FixDekuPalaceReceptionGuards()
        {
            /// if we randomize the patrolling guards in deku palace:
            /// we end up removing the object the front guards require to spawn
            /// however there is a (as far as I can tell) unused object in this scene we can swap
            /// object_dns which is the object used by the dancing deku guards in the king's chamber
            /// nothing seems to use their object in the regular palace scene, no idea why the object is there
            if (!ReplacementListContains(GameObjects.Actor.DekuPatrolGuard))
            {
                return;
            }

            var frontGuardOID = GameObjects.Actor.DekuPatrolGuard.ObjectIndex();
            var dekuPalaceScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TerminaField.FileID());

            if (!dekuPalaceScene.Maps[0].Objects.Contains(frontGuardOID))
            {
                // scene has already been written at this point, need to romhack it, faster than re-writing the whole scene file
                var dekuPalaceRoom1FID = GameObjects.Scene.DekuPalace.FileID() + 1;
                var dekuPalaceRoom1File = RomData.MMFileList[dekuPalaceRoom1FID].Data;
                ReadWriteUtils.Arr_WriteU16(dekuPalaceRoom1File, Dest: 0x4E, (ushort)frontGuardOID);
            }
        }

        private static void AllowGuruGuruOutside()
        {
            /// guruguru's actor spawns or kills itself based on time flags, ignoring that the spawn points themselves have timeflags
            /// if we want guruguru to be placed in the world without being restricted to day/night only (which is lame) we have to stop this
            if (!ReplacementListContains(GameObjects.Actor.GuruGuru))
            {
                return;
            }

            var guruFid = GameObjects.Actor.GuruGuru.FileListIndex();
            RomUtils.CheckCompressed(guruFid);
            var guruData = RomData.MMFileList[guruFid].Data;
            ReadWriteUtils.Arr_WriteU32(guruData, Dest: 0x104, val: 0x00000000); // BNE (if day, and not type 1, die) -> NOP

            // funny enough, type 0 (talkable during day) and type 2 (creates music through the walls)
            //  both are already time flag'd to not show up at night in the inn... so why did the code care?

            // BUT EVEN MORE FUNNY, this funny guy, he CHECKS NIGHT in his update function too WTF
            // jeez just branch past all that noise
            ReadWriteUtils.Arr_WriteU32(guruData, Dest: 0x9BC, val: 0x10000013); // BNEL (test night checks) -> B past it all to actionfunc
        }

        public static void RemoveSTTUnusedPoe()
        {
            /// not inverted, REGULAR stone tower has a poe object... why?
            /// we can recover some headroom by removing it
            ///   remember to delete this if I ever get free objects working instead

            var stonetowertempleScene = RomData.SceneList.Find(u => u.SceneEnum == GameObjects.Scene.StoneTowerTemple);
            for (int i = 0; i < stonetowertempleScene.Maps.Count; ++i)
            {
                var room = stonetowertempleScene.Maps[i];
                var poeIndex = room.Objects.FindIndex(u => u == 0x1C3);
                if (poeIndex > 0)
                {
                    room.Objects[poeIndex] = SMALLEST_OBJ;
                }
            }
        }

        public static void FixSilverIshi()
        {
            /// in MM the silver boulders that are pickupable by goron are ishi in field_keep object
            /// however, these boulders always check the scene flags and set the flags when destroyed, so you cannot respawn them
            ///   considering nothing in vanilla needs these, and because
            ///   I'm worried about setting flags for something else, lets remove that

            var ishiFid = GameObjects.Actor.Rock.FileListIndex();
            RomUtils.CheckCompressed(ishiFid);
            var ishiData = RomData.MMFileList[ishiFid].Data;
            ReadWriteUtils.Arr_WriteU32(ishiData, Dest: 0x12CC, val: 0x00000000); // JAL (Actor_SetSwitchFlag) -> NOP
        }

        public static void FixBabaAndDragonflyShadows()
        {
            /// En_Bba_01 is an unused actor who appears to be the grandma from the bomb proprieters shop
            /// however she uses an expensive and barely used shadow draw function that makes a custom shadow to match her body shape
            /// we need to remove it since its totally broken, its the primary reason dragon flies lag so much
            /// also should make dragonfly better so do that too, since 99% of the time we cant see its shadow as its at y=0 (bug)

            var dragonflyFid = GameObjects.Actor.DragonFly.FileListIndex();
            RomUtils.CheckCompressed(dragonflyFid);
            var dragonflyData = RomData.MMFileList[dragonflyFid].Data;
            // similar to baba, we see a loop followed by a finishing function, we want to skip both in the main draw function
            ReadWriteUtils.Arr_WriteU32(dragonflyData, Dest: 0x2498, val: 0x10000018); // <irrelevant code> -> Jump to 24E4

            if (!ReplacementListContains(GameObjects.Actor.BabaIsUnused))
            {
                return;
            }

            var babaFid = GameObjects.Actor.BabaIsUnused.FileListIndex();
            RomUtils.CheckCompressed(babaFid);
            var babaData = RomData.MMFileList[babaFid].Data;
            // the end of the draw function must be skipped, so we branch past all of it to the end of the function
            ReadWriteUtils.Arr_WriteU32(babaData, Dest: 0xB34, val: 0x10000024); // <irrelevant code> -> Jump to 0xBC8 (beginning of register re-load)
        }

        /* private static void RecreateFishing()
        {

            /// fishing testing

            // to place in spring, we remove some  other actors and objects to get fishing working, as its huge

            var springTwinIslandsScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            var springTwinIsleMap = springTwinIslandsScene.Maps[0];
            // wolfos
            //springTwinIsleMap.Actors[0].ChangeActor(GameObjects.Actor.Empty); // woflos one, we want him to become fisherman
            springTwinIsleMap.Actors[0].Position = new vec16(199, 100, 809); // move fisherman to spot in the lake -50
            springTwinIsleMap.Actors[0].Rotation.y = (short) ActorUtils.MergeRotationAndFlags(-270, 0x7F);
            springTwinIsleMap.Actors[0].ChangeActor(GameObjects.Actor.OOTFishing, 0x200); // 0xFFFF is the whole thing
            springTwinIsleMap.Objects[9] = GameObjects.Actor.OOTFishing.ObjectIndex();

            springTwinIsleMap.Actors[1].ChangeActor(GameObjects.Actor.Empty); // worthless one
            springTwinIsleMap.Actors[1].OldActorEnum = GameObjects.Actor.OOTFishing;

            // tektite
            springTwinIsleMap.Actors[2].ChangeActor(GameObjects.Actor.Empty); // one whole tek
            springTwinIsleMap.Objects[1] = GameObjects.Actor.Empty.ObjectIndex();

            // goron son
            springTwinIsleMap.Actors[20].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[6] = GameObjects.Actor.Empty.ObjectIndex();

            // guay
            springTwinIsleMap.Actors[5].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[6].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[7] = GameObjects.Actor.Empty.ObjectIndex();
            // keese // why is there a keese object here?
            springTwinIsleMap.Objects[0] = 0x1AB; // either empty or we could try to spawn the proprietor
            // skullfish encounter
            springTwinIsleMap.Actors[21].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[27].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[28].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[8] = GameObjects.Actor.Empty.ObjectIndex();

            // nothing left for enemizer to do so it wont write the scene, we have to do that here
            SceneUtils.UpdateScene(springTwinIslandsScene);

        } // */

        private static void AddGrottoVariety()
        {
            /// turns out the grottos have unused objects, some of them can be swapped
            ///   without affecting the original enemy placement, and gives us some variety

            var grottosScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.Grottos.FileID());

            // dodongo grotto has a blue icecycle object, switch to BO so we can get bo actors from jp grotto
            var dodongoGrottoObjectList = grottosScene.Maps[7].Objects;
            dodongoGrottoObjectList[2] = GameObjects.Actor.Bo.ObjectIndex();

            // peahat grotto has a deku baba object, switch to BO so we can get bo actors from jp grotto
            var peahatGrottoObjecList = grottosScene.Maps[13].Objects;
            peahatGrottoObjecList[2] = GameObjects.Actor.Bo.ObjectIndex();
            // there is a worthless mushroom here, lets make TWO peahats :]
            var newPeahat = grottosScene.Maps[13].Actors[3];
            newPeahat.ChangeActor(GameObjects.Actor.Peahat, vars: 0, modifyOld: true);
            newPeahat.Position = new vec16(5010, -20, 600); // move over near peahat one

            // straight jp grotto has only one object, padding of scene data means there is space for an object right behind it that we can use
            //  we can use the second object to give this area a chest by taking one of the useless mushrooms and changing it
            // expand object list to have both of our new objects, change dekubaba to dodongo to increase likelyhood of killable
            grottosScene.Maps[6].Objects = new List<int> { GameObjects.Actor.Peahat.ObjectIndex(),
                                                           GameObjects.Actor.TreasureChest.ObjectIndex() };
            // change dekubaba to dodongo so its killable to get the new chest
            grottosScene.Maps[6].Actors[2].ChangeActor(GameObjects.Actor.Peahat, vars: 0, modifyOld: true);
            // we have to tell the room to load the extra object though
            var straightJPGrottoRoomFile = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 7];
            straightJPGrottoRoomFile.Data[0x29] = 0x2; // setting object header object count from 1 to 2

            var newChestActor = grottosScene.Maps[6].Actors[7];
            // chest params: should be invisible until you kill the enemy, should not collide with any other chest flags in the scene, item: dont know
            // flag 1D, type 7, item 6D (unknown)
            newChestActor.ChangeActor(GameObjects.Actor.TreasureChest, 0x26ED, modifyOld: true);
            newChestActor.Position = new vec16(-230, 0, 1130); // move into the grass area
            newChestActor.Rotation.y = (short)ActorUtils.MergeRotationAndFlags(90, grottosScene.Maps[6].Actors[7].Rotation.y); // rotate to face the center
            // turn the other useless mushroom into another buterfly for ambiance
            grottosScene.Maps[6].Actors[8].ChangeActor(GameObjects.Actor.Butterfly, 0x5324, modifyOld: true);
            grottosScene.Maps[6].Actors[8].Position.y = 58; // dont want spawning in the ground, we want flying around

            // biobaba grotto has a worthless dekubaba object, lets swap it for the ice block object so we can freeze the water
            grottosScene.Maps[11].Objects[3] = 0x1E7; // iceflowe
        }

        #endregion

        public static List<GameObjects.Actor> GetSceneFairyDroppingEnemyTypes(SceneEnemizerData thisSceneData)
        {
            /// reads the list of specific actors of fairies, checks the list of actors we read from the scene, gets the actor types for GetMatches
            /// why? because our object focused code needs to whittle the list of actors for a enemy replacement, 
            ///   but has to know if even one enemy is used for fairies that it cannot be unkillable
            /// doing that last second per-enemy would be expensive, so we need to check per-scene
            /// we COULD hard code these types into the scene data, but if someone in the distant future
            ///   doesn't realize they have to add both, might be a hard bug to find

            var actorsThatDropFairies = thisSceneData.scene.SceneEnum.GetSceneFairyDroppingEnemies();
            var returnActorTypes = new List<GameObjects.Actor>();
            var actorList = thisSceneData.actors;
            for (int actorNum = 0; actorNum < actorList.Count; ++actorNum)
            {
                for (int fairyRoom = 0; fairyRoom < actorsThatDropFairies.Count; ++fairyRoom)
                {
                    if (thisSceneData.actors[actorNum].Room == actorsThatDropFairies[fairyRoom].roomNumber
                      && actorsThatDropFairies[fairyRoom].actorNumbers.Contains(actorList[actorNum].RoomActorIndex))
                    {
                        returnActorTypes.Add((GameObjects.Actor)actorList[actorNum].ActorID);
                    }
                }
            }
            return returnActorTypes;
        }


        public static void SetupGrottoActor(Actor enemy, int newVariant)
        {
            /// grottos can get their address index from an array, where the index can be their Z rotation
            ///   so we re-encoded variants to hold the data we want, check out the actor enum entry for more info
            ///   the lower two byes are used to set the chest, but we have a chest grotto with upper byte index, so reuse for rotation here
            ///   the game does not use the top two bits of the second byte, so we use one as a flag for rotation type grottos
            ///   we also set the time flags to always, because it makes no sense for a hole to only exist day or night, holes are forever
            enemy.ChangeActor(GameObjects.Actor.GrottoHole, vars: newVariant);
            //if ((newVariant & 0x0400) != 0) // grotto that uses rotation to set value
            {
                int newIndex = newVariant & 0xF; // in vanilla the array is only 15 long
                enemy.Rotation.x = (short)ActorUtils.MergeRotationAndFlags(rotation: 0, flags: 0x7F);
                enemy.Rotation.z = (short)ActorUtils.MergeRotationAndFlags(rotation: newIndex, flags: 0x7F);//: enemy.Rotation.z);
            }
        }

        public static void FixPatrollingEnemyVars(List<Actor> chosenReplacementEnemies)
        {
            /// fixes the patrolling enemy paths to make sure it matches the previous actor path

            // this also sets actor kickout address index to 0 (if they have one),
            // because they use different systems which are not compatible

            // find all pathing enemies
            for (int i = 0; i < chosenReplacementEnemies.Count; i++)
            {
                Actor actor = chosenReplacementEnemies[i];
                var newType = actor.ActorEnum.GetType(actor.Variants[0]);

                if (actor.Type == GameObjects.ActorType.Pathing // set on scene actor load
                  && newType == GameObjects.ActorType.Pathing)  // pulled from replacement vars
                {
                    var oldPathBehaviorAttr = actor.OldActorEnum.GetAttribute<PathingTypeVarsPlacementAttribute>();
                    var newdoldPathBehaviorAttr = actor.ActorEnum.GetAttribute<PathingTypeVarsPlacementAttribute>();
                    if (oldPathBehaviorAttr == null || newdoldPathBehaviorAttr == null)
                    {
                        continue; // this enemy doesn't need it
                    }

                    // need to get the path value from the old variant
                    var oldVariant = actor.OldVariant;
                    var oldPathShifted = (oldVariant & (oldPathBehaviorAttr.Mask)) >> oldPathBehaviorAttr.Shift;

                    // clear the old path from this vars
                    var newVarsWithoutPath = actor.Variants[0] & ~newdoldPathBehaviorAttr.Mask;

                    // in addition, enemies with kickout addresses need their vars changed too
                    // ... so it turns out they dont use the same indexing system
                    // fornow, pass ZERO to both actors (use the main exit)
                    // it should give us a basic entrance to work with that wont crash anywhere where pathing enemies can exist
                    var newKickoutAttr = actor.ActorEnum.GetAttribute<PathingKickoutAddrVarsPlacementAttribute>();
                    if (newKickoutAttr != null) // new actor has kick out address, need to read the old one
                    {
                        int kickoutMask; // separate for debuging
                        int kickoutAddr = 0; // safest bet, there should always be at least one exit address per scene

                        // erase the kick location from the old vars
                        kickoutMask = newKickoutAttr.Mask << newKickoutAttr.Shift;
                        newVarsWithoutPath &= ~(kickoutMask);
                        // replace with new address
                        newVarsWithoutPath |= (kickoutAddr << newKickoutAttr.Shift);
                    }

                    // shift the path into the new location
                    var newPath = oldPathShifted << newdoldPathBehaviorAttr.Shift;

                    // set variant from cleaned old variant ored against the new path
                    actor.Variants[0] = newVarsWithoutPath | newPath;
                }
            }
        }

        

        public static void ShuffleObjects(SceneEnemizerData thisSceneData)
        {
            /// find replacement objects for objects that actors depend on

            thisSceneData.chosenReplacementObjects = new List<ValueSwap>();
            int newObjectSize = 0;
            var newActorList = new List<int>();

            for (int objectIndex = 0; objectIndex < thisSceneData.objects.Count; objectIndex++)
            {
                #region Object Forcing Debug
                //////////////////////////////////////////////////////
                ///////// debugging: force an object (enemy) /////////
                //////////////////////////////////////////////////////
                #if DEBUG

                bool TestHardSetObject(GameObjects.Scene targetScene, GameObjects.Actor target, GameObjects.Actor replacement)
                {
                    if (thisSceneData.scene.File == targetScene.FileID() && thisSceneData.objects[objectIndex] == target.ObjectIndex())
                    {
                        thisSceneData.chosenReplacementObjects.Add(new ValueSwap()
                        {
                            OldV = thisSceneData.objects[objectIndex],
                            NewV = replacement.ObjectIndex(),
                            ChosenV = replacement.ObjectIndex()
                        });
                        return true;
                    }
                    return false;
                }
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Clock, GameObjects.Actor.BombFlower)) continue;
                if (TestHardSetObject(GameObjects.Scene.TwinIslands, GameObjects.Actor.Wolfos, GameObjects.Actor.BigPoe)) continue;
                if (TestHardSetObject(GameObjects.Scene.TerminaField, GameObjects.Actor.Leever, GameObjects.Actor.DragonFly)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthClockTown, GameObjects.Actor.Carpenter, GameObjects.Actor.BombFlower)) continue;

                //TestHardSetObject(GameObjects.Scene.ClockTowerInterior, GameObjects.Actor.HappyMaskSalesman, GameObjects.Actor.FlyingPot);
                #endif
                #endregion

                var reducedCandidateList = thisSceneData.candidatesPerObject[objectIndex].ToList();
                foreach (var objectSwap in thisSceneData.chosenReplacementObjects)
                {
                    // remove previously used objects, remove copies to increase variety
                    reducedCandidateList.RemoveAll(u => u.ObjectID == objectSwap.NewV);
                }
                if (reducedCandidateList.Count == 0) // rarely, there are no available objects left
                {
                    newObjectSize += 2 ^ 30; // should always error in the object size section
                    continue; // this enemy was starved by previous options, force error and try again
                }

                // get random enemy from the possible random enemy matches
                Actor randomEnemy = reducedCandidateList[thisSceneData.rng.Next(reducedCandidateList.Count)];

                // keep track of sizes between this new enemy combo and what used to be in this scene
                if (randomEnemy.ObjectID >= 4) // object 1 is gameplay keep, 3 is dungeon keep
                {
                    newObjectSize += randomEnemy.ObjectSize;
                }
                if (!newActorList.Contains(randomEnemy.ActorID))
                {
                    newActorList.Add(randomEnemy.ActorID);
                }

                // add random enemy to list
                thisSceneData.chosenReplacementObjects.Add(new ValueSwap()
                {
                    OldV = thisSceneData.objects[objectIndex],
                    ChosenV = randomEnemy.ObjectID,
                    NewV = randomEnemy.ObjectID
                });
            } // end for for each object
        }

        public static void ShuffleActors(SceneEnemizerData thisSceneData, int objectIndex, List<Actor> subMatches, List<Actor> previouslyAssignedCandidates, List<Actor> temporaryMatchEnemyList)
        {
            for (int actorIndex = 0; actorIndex < thisSceneData.actorsPerObject[objectIndex].Count(); actorIndex++)
            {
                var oldActor = thisSceneData.actorsPerObject[objectIndex][actorIndex];
                var actorsPerRoomCount = thisSceneData.actorsPerObject[objectIndex].FindAll(u => u.Room == oldActor.Room).Count();

                // this isn't really a loop, 99% of the time it matches on the first loop
                // leaving this for now because its faster than shuffling the list even if it looks stupid
                // eventually: replace with .Single().Where(conditions)
                Actor testActor;
                while (true)
                {
                    /// looking for a list of objects for the actors we chose that fit the actor types
                    //|| (oldEnemy.Type == subMatches[testActor].Type && rng.Next(5) == 0)
                    //  //&& oldEnemy.Stationary == subMatches[testActor].Stationary)
                    testActor = subMatches[thisSceneData.rng.Next(subMatches.Count)];

                    if (testActor.IsCompanion && (oldActor.MustNotRespawn || actorsPerRoomCount <= 2))
                    {
                        // so far all companions are unkillable, so we cannot put them in these rooms
                        // also if the room has no space for companions, dont use them here
                        continue;
                    }

                    break;
                }

                oldActor.ChangeActor(testActor, vars: testActor.Variants[thisSceneData.rng.Next(testActor.Variants.Count)]);

                temporaryMatchEnemyList.Add(oldActor);
                var testSearch = previouslyAssignedCandidates.Find(u => u.ActorID == oldActor.ActorID);
                if (testSearch == null)
                {
                    previouslyAssignedCandidates.Add(testActor);
                }
            } // end foreach
        } // end function

        public static void GenerateActorCandidates(SceneEnemizerData thisSceneData, List<GameObjects.Actor> fairyDroppingActors)
        {
            /// get a matching set of possible replacement objects and enemies that we can use

            thisSceneData.actorsPerObject = new List<List<Actor>>();
            for (int objectIndex = 0; objectIndex < thisSceneData.objects.Count; objectIndex++)
            {
                // get a list of all enemies (in this room) that have the same OBJECT as our object that have an actor we also have
                thisSceneData.actorsPerObject.Add(thisSceneData.actors.FindAll(u => u.OldObjectID == thisSceneData.objects[objectIndex]));
                // get a list of matching actors that can fit in the place of the previous actor
                var objectHasFairyDroppingEnemy = fairyDroppingActors.Any(u => u.ObjectIndex() == thisSceneData.objects[objectIndex]);
                //var newCandiateList = GetMatchPool(thisSceneData, objectHasFairyDroppingEnemy).ToList(); // why the extra convert?
                var newCandiateList = GetMatchPool(thisSceneData, objectHasFairyDroppingEnemy);
                var candidateTest = newCandiateList.Find(u => u.Variants.Count == 0);
                if (candidateTest != null)
                {
                    throw new Exception("GenActorCandidatees: zero variants detected");
                }
                if (newCandiateList == null || newCandiateList.Count == 0)
                {
                    throw new Exception("GenActorCandidatees: no candidates detected");
                }

                thisSceneData.candidatesPerObject.Add(newCandiateList);
            }
        }

        // TODO clean up
        public static List<Actor> GetMatchPool(SceneEnemizerData thisSceneData, bool containsFairyDroppingEnemy)
        {
            var reducedCandidateList = Actor.CopyActorList(thisSceneData.appectableCandidates);
            var oldActors = thisSceneData.actors;
            List<Actor> enemyMatchesPool = new List<Actor>();

            // we cannot currently swap out specific enemies, so if ONE must be killable, all shared enemies must
            //  eg: one of the dragonflies in woodfall must be killable in the map room, so all in the dungeon must since we cannot isolate
            bool MustBeKillable = oldActors.Any(u => u.MustNotRespawn);

            // moved up higher, because we can scan once per scene
            if (containsFairyDroppingEnemy) // scene.SceneEnum.GetSceneFairyDroppingEnemies().Contains((GameObjects.Actor) oldActors[0].Actor))
            {
                /// special case: armos does not drop stray fairies, and I dont know why
                ReplacementListRemove(reducedCandidateList, GameObjects.Actor.Armos);
                MustBeKillable = true; // we dont want respawning or unkillable enemies here either
            }

            // this could be per-enemy, but right now its only used where enemies and objects match, so to save cpu cycles do it once per object not per enemy
            foreach (var enemy in thisSceneData.scene.SceneEnum.GetBlockedReplacementActors((GameObjects.Actor)oldActors[0].ActorID))
            {
                ReplacementListRemove(reducedCandidateList, enemy);
            }


            // todo does this NEED to be a double loop? does anything change per enemy copy that we should worry about?
            foreach (var oldEnemy in oldActors) // this is all copies of an enemy in a scene, so all bo or all guay
            {
                // the enemy we got from the scene has the specific variant number, the general game object has all
                foreach (var candidateEnemy in reducedCandidateList)
                {
                    var enemy = candidateEnemy.ActorEnum;
                    var compatibleVariants = oldEnemy.CompatibleVariants(candidateEnemy, oldEnemy.OldVariant, thisSceneData.rng);
                    if (compatibleVariants == null || compatibleVariants.Count == 0)
                    {
                        continue;
                    }

                    // TODO here would be a great place to test if the requirements to kill an enemy are met with given items

                    if (!enemyMatchesPool.Any(u => u.ActorID == candidateEnemy.ActorID))
                    {
                        var newEnemy = candidateEnemy.CopyActor();
                        if (MustBeKillable)
                        {
                            //newEnemy.Variants = enemy.KillableVariants(compatibleVariants); // reduce to available
                            newEnemy.Variants = candidateEnemy.KillableVariants(compatibleVariants); // reduce to available
                            if (newEnemy.Variants.Count == 0)
                            {
                                continue; // can't put this enemy here: it has no non-respawning variants
                            }
                        }
                        else
                        {
                            newEnemy.Variants = compatibleVariants;
                        }
                        enemyMatchesPool.Add(newEnemy);

                        if (newEnemy.Variants.Count == 0)
                        {
                            throw new Exception("WTF3"); // weird debug
                        }

                    }
                } // for each candidate end
            } // for each slot end

            return enemyMatchesPool;
        }

        public static void TrimAllActors(SceneEnemizerData thisSceneData, List<Actor> previouslyAssignedCandidates, List<Actor> temporaryMatchEnemyList, int freeActorRate)
        {
            // enemies can have max per room variants, if these show up we should cull the extra over the max
            var restrictedActors = previouslyAssignedCandidates.FindAll(u => u.HasVariantsWithRoomLimits() || u.OnlyOnePerRoom != null);
            for (int i = 0; i < restrictedActors.Count; ++i)
            {
                var problemActor = restrictedActors[i];

                // we need to split enemies per room
                for (int roomIndex = 0; roomIndex < thisSceneData.scene.Maps.Count; ++roomIndex)
                {
                    var roomActors = temporaryMatchEnemyList.FindAll(u => u.Room == roomIndex && u.ActorID == problemActor.ActorID);
                    var roomIsClearPuzzleRoom = thisSceneData.scene.SceneEnum.IsClearEnemyPuzzleRoom(roomIndex);
                    //var roomFreeActors = sceneFreeActors.ToList(); // works but too small
                    var roomFreeActors = GetRoomFreeActors(thisSceneData.scene, thisSceneData.chosenReplacementObjectsPerMap[roomIndex], thisSceneData.sceneFreeActors);

                    if (problemActor.OnlyOnePerRoom != null)
                    {
                        TrimSpecificActor(problemActor, roomActors, roomFreeActors, roomIsClearPuzzleRoom, thisSceneData.rng, randomRate: freeActorRate);
                    }
                    else
                    {
                        var limitedVariants = problemActor.Variants.FindAll(u => problemActor.VariantMaxCountPerRoom(u) >= 0);
                        foreach (var variant in limitedVariants)
                        {
                            TrimSpecificActor(problemActor, roomActors, roomFreeActors, roomIsClearPuzzleRoom, thisSceneData.rng, variant: variant, randomRate: freeActorRate);
                        }
                    }
                }
            } // end for trim restricted actors
        }

        public static void TrimSpecificActor(Actor actorType, List<Actor> roomEnemies, List<Actor> roomFreeActors,
                                           bool roomIsClearPuzzleRoom, Random rng, int variant = -1, int randomRate = 0x50)
        {
            /// actors with maximum counts have their extras trimmed off, replaced with free or empty actors, depending on randomRate

            List<Actor> roomEnemiesWithVariant;
            if (actorType.OnlyOnePerRoom != null)
            {
                roomEnemiesWithVariant = roomEnemies;
            }
            else
            {
                roomEnemiesWithVariant = roomEnemies.FindAll(u => u.Variants[0] == variant);
            }

            if (roomEnemiesWithVariant != null && roomEnemiesWithVariant.Count > 1)
            {
                int max = actorType.VariantMaxCountPerRoom(variant);
                int removed = 0;
                if (roomIsClearPuzzleRoom) // clear enemy room, only one enemy has to be killable
                {
                    // weirdly there isn't a single room in the game that has both a clear enemy to get item puzzle and a fairy dropping enemy, so we can reuse
                    var randomEnemy = roomEnemiesWithVariant[rng.Next(roomEnemiesWithVariant.Count)];
                    roomEnemiesWithVariant.Remove(randomEnemy); // leave at least one enemy alone
                    removed++;
                }
                else
                {
                    // if not a clear room, all protected enemies are fairy enemies or specific enemies, cannot remove any
                    foreach (var protectedEnemy in roomEnemiesWithVariant.Where(u => u.MustNotRespawn == true).ToList())
                    {
                        roomEnemiesWithVariant.Remove(protectedEnemy);
                        removed++;
                    }
                }

                // remove random enemies until max for variant is reached
                for (int i = removed; i < max && i < roomEnemiesWithVariant.Count; ++i)
                {
                    roomEnemiesWithVariant.Remove(roomEnemiesWithVariant[rng.Next(roomEnemiesWithVariant.Count)]);
                }

                // if the actor being trimmed is a free actor, remove from possible replacements
                var search = roomFreeActors.Find(u => u.ActorID == actorType.ActorID);
                if (search != null)
                {
                    roomFreeActors.Remove(search);
                }

                // kill the rest of variant X since max is reached
                foreach (var enemy in roomEnemiesWithVariant)
                {
                    var enemyIndex = roomEnemies.IndexOf(enemy);
                    EmptyOrFreeActor(enemy, rng, roomEnemies, roomFreeActors, roomIsClearPuzzleRoom, randomRate);
                }
            }
        }

        public static List<List<int>> TrimObjectList(SceneEnemizerData thisSceneData, StringBuilder log)
        {
            /// this function generates our enemizer chosenReplacementObjectsPerMap from our chosenReplacementObjects
            ///   also trims duplicate objects, replacing them with SMALLEST_OBJ

            var replacedObjects = new List<int>();
            var objectsPerMap = new List<List<int>>();
            var actors = thisSceneData.actors;
            var scene = thisSceneData.scene;

            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];
                var objList = map.Objects.ToList(); // copy the old list, were modifying

                // first pass: generate a list of all objects per map, and replace objects as we go from the swaps
                for (int swapIndex = 0; swapIndex < thisSceneData.chosenReplacementObjects.Count; swapIndex++)
                {
                    var swap = thisSceneData.chosenReplacementObjects[swapIndex];
                    var searchIndex = map.Objects.FindIndex(u => u == swap.OldV); // search original list so we dont catch the previous changes
                    if (searchIndex != -1) // not all rooms will have the object
                    {
                        objList[searchIndex] = swap.NewV;
                    }
                }

                // find all objects that have duplicates
                var uniqueObjects = objList.Distinct().ToList();

                // if they are the same size, no duplicates, keep going to next map
                if (objList.Count() != uniqueObjects.Count())
                {
                    // couldnt find a simple way to get a list of duplicates, so instead starting with list of unique values

                    // second pass: remove all duplicates
                    for (int u = 0; u < uniqueObjects.Count(); u++)
                    {
                        var uniqueObj = uniqueObjects[u];
                        if (objList.Count(u => u == uniqueObj) > 1) // more than one exists, remove
                        {
                            // just remove first one, not sure if there is an advantage of changing one over the other
                            var firstIndex = objList.FindIndex(u => u == uniqueObj);
                            objList[firstIndex] = SMALLEST_OBJ;
                        }
                    }
                }

                objectsPerMap.Add(objList);
            }

            if (replacedObjects.Count > 0)
            {
                var objectAsHexString = replacedObjects.Select(u => u.ToString("X3"));
                log.AppendLine($"Duplicate Objects: [{String.Join(", ", objectAsHexString)}]");
            }

            thisSceneData.chosenReplacementObjectsPerMap = objectsPerMap;
            return objectsPerMap;
        }

        public static List<Actor> GetSceneFreeActors(Scene scene)
        {
            /// some actors don't require unique objects, they can use objects that are generally loaded, we can use these almost anywhere
            ///  any actor that is object type 1 (gameplay_keep) is free to use anywhere
            ///  scenes can have a special object loaded by themselves, this is either dangeon_keep or field_keep, or none

            var sceneIsDungeon = scene.HasDungeonObject();
            var sceneIsField = scene.HasFieldObject();
            // todo: replace enum.IsEnemyRandomized
            //var sceneFreeActors = ReplacementCandidateList.Where(u => (u.ObjectID == 1
            var sceneFreeActors = FreeCandidateList.Where(u => (u.ObjectID == 1
                                                    || (sceneIsField && u.ObjectID == (int)Scene.SceneSpecialObject.FieldKeep)
                                                    || (sceneIsDungeon && u.ObjectID == (int)Scene.SceneSpecialObject.DungeonKeep))
                                                 && !(u.BlockedScenes != null && u.BlockedScenes.Contains(scene.SceneEnum))).ToList();

            // TODO: search all untouched objects and add those actors too

            return sceneFreeActors;
        }

        public static List<Actor> GetRoomFreeActors(Scene scene, List<int> objectList, List<Actor> sceneFreeActors = null)
        {
            if (sceneFreeActors == null)
            {
                sceneFreeActors = GetSceneEnemyActors(scene); // should never get this far
            }

            var roomFreeActors = ReplacementCandidateList.Where(u => u.ObjectID >= 3
                                     && objectList.Contains(u.ObjectID)
                                     && !(u.BlockedScenes != null && u.BlockedScenes.Contains(scene.SceneEnum))
                                     ).ToList();

            var freeOnlyActors = FreeOnlyCandidateList.Where(u => objectList.Contains(u.ObjectID)
                                     && !(u.BlockedScenes != null && u.BlockedScenes.Contains(scene.SceneEnum))
                                     ).ToList();

            return sceneFreeActors.Union(roomFreeActors).Union(freeOnlyActors).ToList();
        }

        public static void EmptyOrFreeActor(Actor targetActor, Random rng, List<Actor> currentRoomActorList,
                                            List<Actor> acceptableFreeActors, bool roomIsClearPuzzleRoom = false, int randomRate = 0x50)
        {
            /// returns an actor that is either an empty actor or a free actor that can be placed here beacuse it doesn't require a new unique object

            // roll dice: either get a free actor, or empty
            if (rng.Next(100) < randomRate) // for now a static chance
            {
                // pick random replacement by selecting random start of array and traversing sequentially until we find a match
                int randomStart = rng.Next(acceptableFreeActors.Count);
                for (int matchAttempt = 0; matchAttempt < acceptableFreeActors.Count; ++matchAttempt)
                {
                    // check the old enemy for available co-actors,
                    // remove if those already exist in the list at max size

                    int listIndex = (randomStart + matchAttempt) % acceptableFreeActors.Count;
                    var testEnemy = acceptableFreeActors[listIndex];
                    var testEnemyCompatibleVariants = targetActor.CompatibleVariants(testEnemy, targetActor.OldVariant, rng);
                    if (testEnemyCompatibleVariants == null)
                    {
                        continue;  // no type compatibility, skip
                    }
                    var respawningVariants = testEnemy.RespawningVariants;
                    if ((targetActor.MustNotRespawn || roomIsClearPuzzleRoom) && respawningVariants != null)
                    {
                        testEnemyCompatibleVariants.RemoveAll(u => respawningVariants.Contains(u));
                    }
                    if (testEnemyCompatibleVariants.Count == 0)
                    {
                        continue;  // cannot use respawning enemies here, skip
                    }

                    var enemyHasMaximums = testEnemy.HasVariantsWithRoomLimits();
                    var acceptableVariants = new List<int>();

                    if (enemyHasMaximums)
                    {
                        var enemiesInRoom = currentRoomActorList.FindAll(u => u.ActorID == testEnemy.ActorID);
                        if (enemiesInRoom.Count > 0)  // only test for specific variants if there are already some in the room
                        {
                            // find variant that is not maxed out
                            foreach (var variant in testEnemyCompatibleVariants)
                            {
                                // if the varient limit has not been reached
                                var variantMax = testEnemy.VariantMaxCountPerRoom(variant);
                                var variantCount = enemiesInRoom.Count(u => u.OldVariant == variant);
                                if (variantCount < variantMax)
                                {
                                    acceptableVariants.Add(variant);
                                }
                            }
                        }
                        else
                        {
                            acceptableVariants = testEnemyCompatibleVariants;
                        }
                    }
                    else
                    {
                        acceptableVariants = testEnemyCompatibleVariants;
                    }

                    if (acceptableVariants.Count > 0)
                    {
                        int randomVariant = acceptableVariants[rng.Next(acceptableVariants.Count)];
                        if (testEnemy.ActorEnum == GameObjects.Actor.GrottoHole)
                        {
                            SetupGrottoActor(targetActor, randomVariant);
                        }
                        else
                        {
                            targetActor.ChangeActor(testEnemy, vars: randomVariant);
                        }
                        return;
                    }
                }
            }
            //else: empty actor

            targetActor.ChangeActor(GameObjects.Actor.Empty, vars: 0);
        }

        public static void AddCompanionsToCandidates(SceneEnemizerData thisSceneData, int objectIndex, List<Actor> candidates)
        {
            // for actors that have companions, add them now
            // todo change this into a function
            foreach (var actor in candidates.ToList())
            {
                var companionAttrs = actor.ActorEnum.GetAttributes<CompanionActorAttribute>();
                if (companionAttrs != null)
                {
                    // if 4 or fewer, no companions, not enough regular actors anyway
                    // reminder: these are companions that fully mix into the actor list
                    if (thisSceneData.actorsPerObject[objectIndex].Count <= 3) continue;

                    foreach (var companion in companionAttrs)
                    {
                        var cObj = companion.Companion.ObjectIndex();
                        if (cObj == 1 || cObj == actor.ObjectID)    // todo: add object search across other actors chosen
                        {
                            var newCompanion = new Actor(companion.Companion);
                            newCompanion.Variants = companion.Variants;
                            newCompanion.IsCompanion = true;
                            candidates.Add(newCompanion);
                        }
                    }
                }
            }
        }

        public static void MoveAlignedCompanionActors(List<Actor> changedEnemies, Random rng, StringBuilder log)
        {
            /// companion actors can sometimes be alligned, to increase immersion
            /// example: putting hidden grottos inside of a stone circle
            /// example2: putting butterflies over bushes

            var actorsWithCompanions = changedEnemies.FindAll(u => ((GameObjects.Actor)u.ActorID).HasOptionalCompanions())
                                                     .OrderBy(x => rng.Next()) // randomize list
                                                     .ToList();

            if (actorsWithCompanions.Count <= 2) return;

            for (int i = 0; i < actorsWithCompanions.Count; ++i)
            {
                var mainActor = actorsWithCompanions[i];
                var mainActorEnum = (GameObjects.Actor)mainActor.ActorID;
                var companions = mainActorEnum.GetAttributes<AlignedCompanionActorAttribute>().ToList();
                foreach (var companion in companions)
                {
                    var actorEnum = companion.Companion;
                    // todo detection of ourVars too
                    // scan for companions that can be moved
                    // for now, assume all previously used companions must be left untouched, no shuffling
                    var eligibleCompanions = changedEnemies.FindAll(u => u.ActorID == (int)actorEnum        // correct actor
                                                            && u.previouslyMovedCompanion == false          // not already used
                                                            && companion.Variants.Contains(u.Variants[0])); // correct variant

                    if (eligibleCompanions != null && eligibleCompanions.Count > 0)
                    {
                        var randomCompanion = eligibleCompanions[rng.Next(eligibleCompanions.Count)];
                        // first move on top, then adjust
                        randomCompanion.Position.x = mainActor.Position.x;
                        randomCompanion.Position.y = (short)(actorsWithCompanions[i].Position.y + companion.RelativePosition.y);
                        randomCompanion.Position.z = mainActor.Position.z;

                        // todo: use x and z, with actor rotation, to figure out where to move the actors to
                        log.AppendLine(" Moved companion: [" + randomCompanion.ActorEnum.ToString()
                                    + "][" + randomCompanion.Variants[0].ToString("X2")
                                    + "] to actor: [" + mainActor.ActorEnum.ToString()
                                    + "][" + randomCompanion.Variants[0].ToString("X2")
                                    + "] at cords: [" + randomCompanion.Position.x + ","
                                                    + randomCompanion.Position.y + ","
                                                    + randomCompanion.Position.z + "]");
                        randomCompanion.previouslyMovedCompanion = true;
                    }
                }
            }
        }

        private static void SplitSceneLikeLikesIntoTwoActorObjects(SceneEnemizerData thisSceneData)
        {
            /// special case: likelikes need to be split into two objects because ground and water share one object 
            /// but no other enemies work as dual replacement

            if ((thisSceneData.scene.File == GameObjects.Scene.ZoraCape.FileID() || thisSceneData.scene.File == GameObjects.Scene.GreatBayCoast.FileID())
                && thisSceneData.objects.Contains(GameObjects.Actor.LikeLike.ObjectIndex()))
            {
                // add shield object to list of objects we can swap out
                thisSceneData.objects.Add(GameObjects.Actor.LikeLikeShield.ObjectIndex());
                // generate a candidate list for the second likelike
                for (int i = 0; i < thisSceneData.actors.Count; ++i)
                {
                    // update object for all of the second likelikes, so they will use the second object
                    if (thisSceneData.actors[i].ActorID == (int)GameObjects.Actor.LikeLike
                        && GameObjects.Actor.LikeLike.IsGroundVariant(thisSceneData.actors[i].OldVariant))
                    {
                        var newLikeLike = thisSceneData.actors[i];
                        newLikeLike.OldObjectID = newLikeLike.ObjectID = GameObjects.Actor.LikeLikeShield.ObjectIndex();
                    }
                }
            }
        }

        private static void CullOptionalActors(Scene scene, List<ValueSwap> objList, int loopCount)
        {
            /// issue: sometimes some of the big scenes get stuck in a weird spot where they can't find any actor combos that fit
            /// one day I will figure out this bug, for now, attempt to remove some actors/objects to make it fit
            /// The actors we remove are all forgotten or forgettable, none of them can be required to beat a seed

            // medium goron, unused object, size: 0x10
            // alternative: tanron1 is also size 0x10

            // road to ikana: there is a scarecrow that leads to a fairy pot unknown to most players, we can remove both
            if (scene.SceneEnum == GameObjects.Scene.RoadToIkana)
            {
                scene.Maps[0].Actors[76].ChangeActor(GameObjects.Actor.Empty);
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0x11D, // Scarecrow
                        NewV = SMALLEST_OBJ
                    }
                );
                scene.Maps[0].Actors[30].ChangeActor(GameObjects.Actor.Empty);
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0xF9, // Clay pot
                        NewV = SMALLEST_OBJ
                    }
                );
            }

            // mountain village spring: there is a scarecrow on top of the blacksmith leading to a clay fairy pot
            if (scene.SceneEnum == GameObjects.Scene.MountainVillageSpring)
            {
                scene.Maps[0].Actors[3].ChangeActor(GameObjects.Actor.Empty);
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0x11D, // Scarecrow
                        NewV = SMALLEST_OBJ
                    }
                );
                scene.Maps[0].Actors[46].ChangeActor(GameObjects.Actor.Empty);
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0xF9, // Clay pot
                        NewV = SMALLEST_OBJ
                    }
                );
            }

            // Ikanacanyon: in rando nobody bothers with garo ghosts
            if (scene.SceneEnum == GameObjects.Scene.IkanaCanyon)
            {
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0x155, // Garo master
                        NewV = SMALLEST_OBJ
                    }
                );
                // for now dont remove the encounters as players expect the tatl notification
            }

            // termina field: there is a single pot on a pilllar to the east with a fairy
            if (scene.SceneEnum == GameObjects.Scene.TerminaField)
            {
                objList.Add(
                    new ValueSwap()
                    {
                        OldV = 0xF9, // Jar
                        NewV = SMALLEST_OBJ
                    }
                );
            }
        }


        public class SceneEnemizerData
        {
            // more and more of this stuff needs to be passed to each function, if I want to tame the big mess that is SwapSceneEnemies
            // All common data we have/use in randomizing actors in one scene
            public Scene scene;
            public StringBuilder log;
            public Random rng;
            public DateTime startTime;
            public List<Actor> actors;
            public List<Actor> sceneFreeActors;
            public List<int> objects;
            public List<ValueSwap> chosenReplacementObjects;
            public List<List<int>> chosenReplacementObjectsPerMap;
            public List<Actor> appectableCandidates;
            // outer layer is per object
            public List<List<Actor>> actorsPerObject = new List<List<Actor>>();   
            public List<List<Actor>> candidatesPerObject = new List<List<Actor>>();
            public ActorsCollection actorCollection = null;

            public SceneEnemizerData(Scene scene)
            {
                this.startTime = DateTime.Now;
                this.scene = scene;
                this.log = new StringBuilder();
            }
        }

        public static void SwapSceneEnemies(OutputSettings settings, Scene scene, int seed)
        {
            /// randomize all enemies/actors in a single scene

            SceneEnemizerData thisSceneData = new SceneEnemizerData(scene);

            #region Log Handling functions
            // spoiler log already written by this point, for now making a brand new one instead of appending
            void WriteOutput(string str, StringBuilder altLog = null)
            {
                if (altLog != null)
                    altLog.AppendLine(str);
                else
                    thisSceneData.log.AppendLine(str);
            }
            void FlushLog()
            {
                EnemizerLogMutex.WaitOne(); // with paralel, thread safety
                using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.Write(thisSceneData.log);
                }
                EnemizerLogMutex.ReleaseMutex();
            }

            string GET_TIME(DateTime log)
            {
                return ((DateTime.Now).Subtract(log).TotalMilliseconds).ToString();
            }

            #endregion

            thisSceneData.actors = GetSceneEnemyActors(scene);
            if (thisSceneData.actors.Count == 0)
            {
                return; // if no enemies, no point in continuing
            }
            WriteOutput("time to get scene enemies: " + GET_TIME(thisSceneData.startTime) + "ms");

            thisSceneData.objects = GetSceneEnemyObjects(scene);
            var sceneObjectLimit = SceneUtils.GetSceneObjectBankSize(scene.SceneEnum);
            WriteOutput(" time to get scene objects: " + GET_TIME(thisSceneData.startTime) + "ms");

            WriteOutput("For Scene: [" + scene.ToString() + "] with fid: " + scene.File + ", with sid: 0x"+ scene.Number.ToString("X2"));
            WriteOutput(" time to find scene name: " + GET_TIME(thisSceneData.startTime) + "ms");

            // if actor doesn't exist but object does, probably spawned by something else, remove from actors to randomize
            // TODO check for side objects that no longer need to exist and replace with possible alt objects
            // example: dinofos has a second object: dodongo, just for the fire breath dlist
            foreach (int obj in thisSceneData.objects.ToList())
            {
                if ( ! (VanillaEnemyList.FindAll(u => u.ObjectIndex() == obj)).Any( u => thisSceneData.actors.Any( w => w.ActorID == (int) u)))
                {
                    thisSceneData.objects.Remove(obj);
                }
            }

            SplitSceneLikeLikesIntoTwoActorObjects(thisSceneData);

            WriteOutput(" time to finish removing unnecessary objects: " + GET_TIME(thisSceneData.startTime) + "ms");

            // some scenes are blocked from having enemies, do this ONCE before GetMatchPool, which would do it per-enemy
            //var sceneAcceptableEnemies = ReplacementCandidateList.FindAll( u => ! u.ActorEnum.BlockedScenes().Contains(scene.SceneEnum));
            thisSceneData.appectableCandidates = ReplacementCandidateList.FindAll(u => !u.ActorEnum.BlockedScenes().Contains(scene.SceneEnum))
                                                                         .FindAll(u => !u.NoPlacableVariants());

            // issue: this function is called in paralel, if the order is different the Random object will be different and not seed-reproducable
            // instead of passing the Random instance, we pass seed and add it to the unique scene number to get a replicatable, but random, seed
            thisSceneData.rng = new Random(seed + scene.File);

            // we want to check for actor types that contain fairies per-scene for speed
            var fairyDroppingActors = GetSceneFairyDroppingEnemyTypes(thisSceneData);
            // we group enemies with objects because some objects can be reused for multiple enemies, potential minor boost to variety

            GenerateActorCandidates(thisSceneData, fairyDroppingActors);
            WriteOutput(" time to generate candidate list: " + GET_TIME(thisSceneData.startTime) + "ms");

            int loopsCount = 0;
            int freeEnemyRate = 75; // starting value, as attempts increase this shrinks which lowers budget
            var previousyAssignedCandidate = new List<Actor>();
            var chosenReplacementEnemies = new List<Actor>(); // TODO chopping block
            thisSceneData.sceneFreeActors = GetSceneFreeActors(scene);

            // keeping track of ram space usage is getting ugly, try some OO to clean it up
            thisSceneData.actorCollection = new ActorsCollection(scene);

            WriteOutput(" time to separate map/time actors: " + GET_TIME(thisSceneData.startTime) + "ms");

            var bogoLog = new StringBuilder();
            DateTime bogoStartTime = DateTime.Now;
            while (true) /// bogo sort, try to find an actor/object combos that fits in the space we took it out of
            {
                #region loopCounting
                bogoLog.Clear();
                bogoStartTime = DateTime.Now;

                // if we've tried 5 seeds and no results, re-shuffle the candidate lists, maybe the rng was bad
                loopsCount++;
                if (loopsCount % 5 == 0)
                {
                    // reinit actorCandidatesLists because this RNG is bad
                    GenerateActorCandidates(thisSceneData, fairyDroppingActors);
                    WriteOutput(" re-generate candidates time: " + ((DateTime.Now).Subtract(bogoStartTime).TotalMilliseconds).ToString() + "ms", bogoLog);
                }

                if (loopsCount >= 900) // inf loop catch
                {
                    var error = " No enemy combo could be found to fill this scene: " + scene.SceneEnum.ToString() + " w sid:" + scene.Number.ToString("X2");
                    WriteOutput(error);
                    WriteOutput("Failed Candidate List:");
                    foreach (var list in thisSceneData.candidatesPerObject)
                    {
                        WriteOutput(" Enemy:");
                        foreach (var match in list)
                        {
                            WriteOutput("  Enemytype candidate: " + match.Name + " with vars: " + match.Variants[0].ToString("X2"));
                        }
                    }
                    thisSceneData.actorCollection.PrintCombineRatioNewOldz(thisSceneData.log);
                    FlushLog();
                    throw new Exception(error);
                }
                if (loopsCount > 50 && freeEnemyRate > 0) // reduce free enemy rate 1 percentage per loop over 50
                {
                    freeEnemyRate--; 
                }
                #endregion

                ShuffleObjects(thisSceneData);
                WriteOutput(" objects pick time: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                // reset early if obviously too large
                /* thisactors.SetNewActors(scene, chosenReplacementObjects);
                if ( ! thisactors.isObjectSizeAcceptable())
                {
                    // at this point objects are as small as possible, if too big, we should reset now
                    // before the slow free actor section
                    continue;
                } // */
                // TODO write GetWhole RamSize here so we can get this info from our memory size list now that we have it

                // enemizer is not smart enough if the new chosen objects are copies, and the game allows objects to load twice
                // for now, remove them here after actors are chosen to reduce object size
                StringBuilder objectReplacementLog = new StringBuilder();
                TrimObjectList(thisSceneData, objectReplacementLog);
                WriteOutput(" object trim time: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                // for each object, attempt to change actors 
                for (int objectIndex = 0; objectIndex < thisSceneData.chosenReplacementObjects.Count; objectIndex++)
                {
                    // todo consider attempting to make this multithreaded at this upper level
                    //   issues: we would need to do a final actor trim pass after

                    var temporaryMatchEnemyList = new List<Actor>();
                    List<Actor> subMatches = thisSceneData.candidatesPerObject[objectIndex].FindAll(u => u.ObjectID == thisSceneData.chosenReplacementObjects[objectIndex].ChosenV);

                    AddCompanionsToCandidates(thisSceneData, objectIndex, subMatches);
                    WriteOutput("  companions adding time: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                    ShuffleActors(thisSceneData, objectIndex, subMatches, previousyAssignedCandidate, temporaryMatchEnemyList);
                    WriteOutput("  match time: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                    TrimAllActors(thisSceneData, previousyAssignedCandidate, temporaryMatchEnemyList, freeEnemyRate);
                    WriteOutput("  trim/free time: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                    // add temp list back to chosenRepalcementEnemies
                    //chosenReplacementEnemies.AddRange(temporaryMatchEnemyList);
                    previousyAssignedCandidate.Clear();
                } // end for actors per object

                // todo after all object enemies placed, do another TrimAllActors Pass to catch free actors being added above max
                // todo we need a list of actors that are NOT randomized, left alone, they still exist, and we can ignore new duplicates

                if (loopsCount >= 100)
                {
                    // if we are taking a really long time to find replacements, remove a couple optional actors/objects
                    CullOptionalActors(scene, thisSceneData.chosenReplacementObjects, loopsCount);
                    WriteOutput(" cull optionals: " + GET_TIME(bogoStartTime) + "ms", bogoLog);
                }

                // set objects and actors for isSizeAcceptable to use, and our debugging output
                thisSceneData.actorCollection.SetNewActors(scene, thisSceneData.chosenReplacementObjects);

                WriteOutput(" set for size check: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                if (thisSceneData.actorCollection.isSizeAcceptable())
                {
                    WriteOutput(" after issizeacceptable: " + GET_TIME(bogoStartTime) + "ms", bogoLog);

                    thisSceneData.log.Append(objectReplacementLog);
                    thisSceneData.log.Append(bogoLog);
                    thisSceneData.actorCollection.PrintCombineRatioNewOldz(thisSceneData.log);
                    break; // done, break loop
                }
                // else: not small enough; reset loop and try again

            } // end while searching for compatible object/actors

            WriteOutput(" Loops used for match candidate: " + loopsCount);
            /////////////////////////////
            ///////   DEBUGGING   ///////
            /////////////////////////////
            #if DEBUG
            if (scene.SceneEnum == GameObjects.Scene.AstralObservatory) // force specific actor/variant for debugging
            {
                thisSceneData.actors[1].ChangeActor(GameObjects.Actor.FlyingPot, vars: 0x001); // first torc

            }
            /////////////////////////////
            #endif
            /////////////////////////////

            // any patrolling types need their vars fixed
            FixPatrollingEnemyVars(thisSceneData.actors);

            // print debug actor locations
            for (int i = 0; i < thisSceneData.actors.Count; i++)
            {
                WriteOutput($"Old Enemy actor:[{thisSceneData.actors[i].OldName}] " +
                    $"map [{thisSceneData.actors[i].Room.ToString("D2")}] " +
                    $"was replaced by new enemy: [{thisSceneData.actors[i].Variants[0].ToString("X4")}]" +
                    $"[{thisSceneData.actors[i].Name}]");
            }

            // realign all scene companion actors
            MoveAlignedCompanionActors(thisSceneData.actors, thisSceneData.rng, thisSceneData.log);

            SetSceneEnemyObjects(scene, thisSceneData.chosenReplacementObjectsPerMap);
            SceneUtils.UpdateScene(scene);
            WriteOutput( " time to complete randomizing scene: " + GET_TIME(thisSceneData.startTime) + "ms");
            FlushLog();
        }

        #region Actor Injection

        public static InjectedActor ParseMMRAMeta(string metaFile)
        {
            /// every MMRA comes with one meta file per bin, this contains metadata
            var vanillaActors = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList();
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
                    var newGroundVariantsShort = newGroundVariants.Select(u => Convert.ToInt32(u.Trim(), 16)).ToList();

                    newInjectedActor.groundVariants = newGroundVariantsShort;
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

                var value = Convert.ToInt32(valueStr, fromBase: 16);
                if (command == "actor_id")
                {
                    newInjectedActor.actorID = value;
                }
                if (command == "obj_id")
                {
                    newInjectedActor.objID = value;
                }
                else if (command == "file_id")
                {
                    newInjectedActor.fileID = Convert.ToInt32(valueStr, fromBase: 10);
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
            var actorGameObj = vanillaActors.Find(u =>u.FileListIndex() == newInjectedActor.fileID);
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

        public static void ScanForMMRA(string directory)
        {
            // decomp lets us more easily modify actors now
            // for now, until cat/zoey figure out how to directly integrate the projects
            //   I will, instead, compile with decomp, and then extract the binaries and inject here
            // MMRA files: Majora Mask Rando Actor files, just zip files that contain binaries and extras later
            // ideas for extras: notes to tell rando where sound effects are to be replaced
            // function pointers to interconnect the code

            if ( ! Directory.Exists(directory)) return;

            InjectedActors.Clear();

            foreach (string filePath in Directory.GetFiles(directory, "*.mmra"))
            {
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
                            int newBinLen = ((int) binFile.Length) + ((int) binFile.Length % 0x10); // dma padding
                            var overlayData = new byte[newBinLen];
                            binFile.Open().Read(overlayData, 0, overlayData.Length);

                            // the binary filename convention will be NOTES_name.bin

                            //var binFilenameSplit = binFile.Name.Split('_'); // everything before _ is a comment, readability, discard here
                            //var fileIDtext = binFilenameSplit.Length > 1 ? binFilenameSplit[binFilenameSplit.Length - 1] : binFile.Name;
                           
                            // read the associated meta file
                            var metaFileEntry = zip.GetEntry(filename + ".meta");
                            if (metaFileEntry == null) // meta not found
                            {
                                throw new Exception($"Could not find a meta for actor bin [{binFile.Name}]\n   in [{filePath}]");
                            }

                            var injectedActor = ParseMMRAMeta(new StreamReader(metaFileEntry.Open(), Encoding.Default).ReadToEnd());

                            injectedActor.filename = filePath; // debugging

                            InjectedActors.Add(injectedActor);

                            // behavior now differs between replacement actors and brand new
                            var replacementActorSearch = ReplacementCandidateList.Find(u => u.ActorID == injectedActor.actorID);
                            if (replacementActorSearch != null) // previous actor
                            {
                                replacementActorSearch.UpdateActor(injectedActor);
                            }
                            else
                            {
                                replacementActorSearch = new Actor(injectedActor, filename);
                                ReplacementCandidateList.Add(replacementActorSearch);
                            }

                            // this is separate from the above because this lets us modify files not found in ReplacementCandidateList
                            // like demo_kankyo, which is a free actor and not a regular candidate
                            var newFID = (int)injectedActor.fileID;
                            if (newFID == 0)
                            {
                                injectedActor.overlayBin = overlayData; // save bin for now
                            }
                            else
                            {
                                /// overwrite the file now
                                RomData.MMFileList[newFID].Data = overlayData;
                                RomData.MMFileList[newFID].End = RomData.MMFileList[newFID].Addr + newBinLen;
                                RomData.MMFileList[newFID].WasEdited = true;
                            }
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
            /// overlay c code is compiled with VRAM addresses already baked in,
            /// these get adjusted when the overlay is loaded into RAM, to match the RAM locations
            /// but when we inject this new overlay we move its VRAM to a different place, so its wrong
            /// so now, we must re-apply the VRAM addresses so when the game shifts them into RAM it will have the correct values

            var relocSize = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
            // the table pointer at the end is an offset from the end, we need to swap it
            int tableOffset = (int)(file.Data.Length - relocSize);
            int relocEntryCountLocation = (int)(tableOffset + (4 * 4)); // first four ints are section sizes
            // we need the difference between the old VRAM and new VRAM starting locations to re-align our vram


            // traverse the whole relocation section, parse the changes, apply
            var relocEntryLoc = relocEntryCountLocation + 4; // first overlayEntry immediately after count
            uint relocEntryCount = ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryCountLocation);
            var relocEntryEndLoc = relocEntryLoc + (relocEntryCount * 4);
            while (relocEntryLoc < relocEntryEndLoc)
            {
                // each overlayEntry in reloc is one nibble of shifted section, one nible of type, and 3 bytes of address
                // text section starts at 1 not 0
                var section = ((file.Data[relocEntryLoc] & 0xC0) >> 6) - 1;
                var sectionOffset = sectionOffsets[section];

                // where address is an offset of the section
                var commandType = (file.Data[relocEntryLoc] & 0xF);
                // double command look ahead for LUI/ADDIU
                var commandTypeNext = (file.Data[relocEntryLoc + 4] & 0xF);

                if (commandType == 0x5 && commandTypeNext == 0x6) // LUI/ADDIU combo
                {
                    int luiLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;
                    // reminder: addu treats the last two bytes of our pointer as signed
                    // to fix this, the LUI command is given a carry over bit to fix it, we need to read and write knowing this

                    // combine the halves from asm back into one pointer
                    uint pointer = 0;
                    pointer |= ((uint)ReadWriteUtils.Arr_ReadU16(file.Data, addiuLoc + 2));
                    int LUIDecr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0;
                    pointer |= ((uint)(ReadWriteUtils.Arr_ReadU16(file.Data, luiLoc + 2) - LUIDecr) << 16) ;
                    pointer += newVRAMOffset;
                    // separate the pointer again into halves and put back
                    // are we sure about this? that we are doing it right?
                    int LUIIncr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0; // if the lower half is too big we have to add one to LUI
                    ushort luiPart = (ushort)(((pointer & 0xFFFF0000) >> 16) + LUIIncr);
                    ushort adduPart = (ushort)(pointer & 0xFFFF);
                    ReadWriteUtils.Arr_WriteU16(file.Data, luiLoc   + 2, luiPart);
                    ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, adduPart);

                    relocEntryLoc += 8;
                }
                else if (commandType == 0x4) // JAL function calls
                {
                    int jalLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint jal = ReadWriteUtils.Arr_ReadU32(file.Data, jalLoc) & 0x00FFFFFF;
                    uint shiftedJal = jal << 2;
                    shiftedJal += newVRAMOffset;
                    shiftedJal = shiftedJal >> 2;
                    ReadWriteUtils.Arr_WriteU32(file.Data, jalLoc, 0x0C000000 | shiftedJal);

                    relocEntryLoc += 4;
                }
                else if (commandType == 0x2) // Hard pointer (init/destroy/update/draw pointers can be here, also actual ptr in rodata)
                {
                    int ptrLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint ptrValue = ReadWriteUtils.Arr_ReadU32(file.Data, ptrLoc);
                    ptrValue += newVRAMOffset;
                    ReadWriteUtils.Arr_WriteU32(file.Data, ptrLoc, ptrValue);

                    relocEntryLoc += 4;
                }
                else // unknown command? supposidly Z64 only uses these three although it could support more
                {
                    throw new Exception($"UpdateOverlayVRAMReloc: unknown reloc overlayEntry value:\n" +
                        $" {ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc).ToString("X")}");
                }
            }
        }

        public static void UpdateActorOverlayTable()
        {
            // todo: check if enemizer is set, return if not

            // this is called from romutils.cs right before we build the rom
            /// if overlays have grown, we need to modify their overlay table to use the right values for the new files
            /// every time you move an overlay you need to relocate the vram addresses, so instead of shifting all of them
            ///  we just move the new larger files to the end and leave a hole behind for now

            //const uint theEndOfTakenVRAM = 0x80C27000; // 0x80C260A0 <- actual
            const uint theEndOfTakenVRAM = 0x80CA0000; // changed NOTHING
            const int  theEndOfTakenVROM = 0x03100000; // 0x02EE7XXX <- actual

            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);

            // the overlay table exists inside of another file, we need the offset to the table
            //int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;

            // generate a list of actors sorted by fid
            var actorList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList();
            actorList.Remove(GameObjects.Actor.Empty);
            actorList.Remove(GameObjects.Actor.NULL);
            actorList.RemoveAll(u => u.FileListIndex() < 38);
            var fidSortedActors = actorList.OrderBy(x => x.FileListIndex()).ToList();

            uint previousLastVRAMEnd = theEndOfTakenVRAM;
            int previousLastVROMEnd = theEndOfTakenVROM;

            foreach (var injectedActor in InjectedActors)
            {
                var actorID = injectedActor.actorID;
                var fileID = injectedActor.fileID;
                MMFile file = RomData.MMFileList[fileID];

                int entryLoc = actorOvlTblOffset + (actorID * 32); // overlay table is sorted by actorID

                uint oldVROMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x0);
                uint oldVROMEnd   = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x4);

                // if build knows where VRAM used to start for this actor, use that
                // else, use the old VRAM build for the given actor in this slot
                uint oldVRAMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x08);
                oldVRAMStart = (injectedActor.buildVramStart != 0) ? (injectedActor.buildVramStart) : (oldVRAMStart);
                
                // if it was edited, its not compressed, get new filesize, else diff old address values
                var uncompresedVROMSize = (file.WasEdited) ? (file.Data.Length) : (file.End - file.Addr);

                // for now since we have the space, just move all injected actors to the end, even if they are smaller
                // TODO make a list of previously free holes we can stick stuff into and check that
                file.Addr = previousLastVROMEnd;
                file.End  = previousLastVROMEnd + uncompresedVROMSize;
                previousLastVROMEnd = file.End;
                ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0, (uint) file.Addr);
                ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x4, (uint) file.End);

                // we know where in the overlay pointers exist that need to be updated for VROM->VRAM
                // .reloc stores this info for us as a table of words that contain enough info to help us update
                // the very last byte in the overlay is (from end) offset
                //   of the table that declares size of text/data/rodata/bss
                // following those is a count of the reloc entries, followed by the actual entries
                var relocSize = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
                // the table pointer at the end is an offset from the end, we need to swap it
                int tableOffset = (int)(file.Data.Length - relocSize);

                // the section table only contains section sizes, we need to walk it to know the offsets
                var sectionOffsets = new int[4];
                sectionOffsets[0] = 0; // text (always at the start for our overlay system)
                sectionOffsets[1] = sectionOffsets[0] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 0); // data
                sectionOffsets[2] = sectionOffsets[1] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 4); // rodata
                sectionOffsets[3] = sectionOffsets[2] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 8); // bss

                // have to move the overlay vram location assume its bigger
                // calculate the new VRAM and offset for our new overlay VRAM location
                var  newVRAMSize   = sectionOffsets[3] + relocSize;
                // TODO check if we can place it in an old hole left behind by a previously moved actor
                var newVRAMStart = previousLastVRAMEnd;
                var newVRAMEnd    = (uint)(newVRAMStart + newVRAMSize);
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
            }// end Foreach overlay in overlaylist
        } // end UpdateOverlayTable

        public static void InjectNewActors()
        {
            /// this might get merged back in with scan, and/or the pieces get moved back here
            /// we need to build an Actor from our injected actor, and finish injected actor conversions

            if (InjectedActors.Count == 0)  return;

            var freeOverlaySlots = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                        .Where(u => u.ToString().Contains("Empty")).ToList();

            // in case DMA is restricted, start with a list of known bunk files
            var freeFileSlots = new List<int>
            {
                // these files at the end of the vanilla DMA are unused in USA
                1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551,
                // unused actors or objects:
                GameObjects.Actor.Obj_Toudai.FileListIndex(),
                GameObjects.Actor.Obj_Ocarinalift.FileListIndex(),
                GameObjects.Actor.UnusedStoneTowerPlatform.FileListIndex(),
                GameObjects.Actor.En_Boj_01.FileListIndex(),  // empty actors with nothing in them
                GameObjects.Actor.En_Boj_02.FileListIndex(),
                GameObjects.Actor.En_Boj_03.FileListIndex(),
                GameObjects.Actor.En_Boj_04.FileListIndex(),
                GameObjects.Actor.En_Boj_05.FileListIndex(),
                GameObjects.Actor.En_Stream.FileListIndex(), // is this really unused?
                GameObjects.Actor.SariaSongOcarinaEffects.FileListIndex(), // should be lower down as we might need to use it later
            };

            int GetUnusedFileID(InjectedActor injActor)
            {
                if (freeFileSlots.Count > 0)
                {
                    var f = freeFileSlots[0];
                    freeFileSlots.RemoveAt(0);
                    return f;
                } else // we have run out of known free file slots to use
                {
                    // back up, its broken though
                    //return RomUtils.AppendFile(injActor.overlayBin)
                    throw new Exception("We have run out of actors space to inject, please disable an actor in /actors");
                }
            }

            foreach (var injectedActor in InjectedActors.FindAll(u => u.actorID == 0))
            {
                /// brand new actors, not replacement
                if (injectedActor.buildVramStart == 0)
                {
                    throw new Exception("new actor missing starting vram:\n " + injectedActor.filename);
                }

                var newFileID = GetUnusedFileID(injectedActor); // todo change this back into hardcoded, its a static rom
                //var newFileID = RomUtils.AppendFile(injectedActor.overlayBin); // broken, wants to put our actor outside of romspace
                injectedActor.fileID = newFileID;
                injectedActor.actorID = (int)freeOverlaySlots[0];
                freeOverlaySlots.RemoveAt(0);
                var file = RomData.MMFileList[newFileID];
                file.Data = injectedActor.overlayBin;
                file.WasEdited = true;
                file.IsCompressed = true; // assumption: all actors are compressed

                // update actor ID in overlay init vars, now that we know the new actor ID value
                ReadWriteUtils.Arr_WriteU16(file.Data, (int)injectedActor.initVarsLocation, (ushort)injectedActor.actorID);

                var filenameSplit = injectedActor.filename.Split("\\");
                var newActorName = filenameSplit[filenameSplit.Length - 1];

                RomData.MMFileList[newFileID] = file;
                ReplacementCandidateList.Add(new Actor(injectedActor, newActorName));

                // TODO inject objects too, for actors that have custom objects

            } // end for each injected actor
        }

        #endregion

        public static void ShuffleEnemies(OutputSettings settings, Random random)
        {
            try
            {
                seedrng = random;
                DateTime enemizerStartTime = DateTime.Now;

                // for dingus that want moonwarp, re-enable dekupalace
                var SceneSkip = new GameObjects.Scene[] {
                    GameObjects.Scene.GreatBayCutscene,
                    GameObjects.Scene.SwampShootingGallery,
                    GameObjects.Scene.TownShootingGallery,
                    GameObjects.Scene.GiantsChamber,
                    GameObjects.Scene.SakonsHideout,
                    GameObjects.Scene.CutsceneMap,
                    GameObjects.Scene.MilkBar };// , GameObjects.Scene.DekuPalace };

                PrepareEnemyLists();
                SceneUtils.ReadSceneTable();
                SceneUtils.GetMaps();
                SceneUtils.GetMapHeaders();
                SceneUtils.GetActors();
                EnemizerEarlyFixes();
                ScanForMMRA(directory: "actors");
                InjectNewActors();

                var newSceneList = RomData.SceneList;
                newSceneList.RemoveAll(u => SceneSkip.Contains(u.SceneEnum) );

                // if using parallel, move biggest scenes to the front so that we dont get stuck waiting at the end for one big scene with multiple dead cores idle
                // LIFO, biggest scenes at the back of this list of big scenes
                // this should be all scenes that took > 500ms on Isghj's computer during alpha ~dec 2020
                //  this is old, should be re-evaluated with different code
                foreach (var sceneIndex in new int[]{ 1442, 1353, 1258, 1358, 1449, 1291, 1224,  1522, 1388, 1165, 1421, 1431, 1241, 1222, 1330, 1208, 1451, 1332, 1446, 1310 }){
                    var item = newSceneList.Find(u => u.File == sceneIndex);
                    newSceneList.Remove(item);
                    newSceneList.Insert(0, item);
                }
                int seed = random.Next(); // order is up to the cpu scheduler, to keep these matching the seed, set them all to start at the same value

                Parallel.ForEach(newSceneList.AsParallel().AsOrdered(), scene =>
                //foreach (var scene in newSceneList) // sequential for debugging only
                {
                    var previousThreadPriority = Thread.CurrentThread.Priority;
                    Thread.CurrentThread.Priority = ThreadPriority.Lowest; // do not SLAM
                    SwapSceneEnemies(settings, scene, seed);
                    Thread.CurrentThread.Priority = previousThreadPriority;
                });
                //}

                EnemizerLateFixes();
                //LowerEnemiesResourceLoad();
                if (ACTORSENABLED)
                {
                    DisableAllLocationRestrictions();  //experimental
                }

                // write the final time and version last
                using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.WriteLine("Enemizer final completion time: " + ((DateTime.Now).Subtract(enemizerStartTime).TotalMilliseconds).ToString() + "ms ");
                    sw.Write("Enemizer version: Isghj's Enemizer Test 32.0\n");
                }
            }
            catch (Exception e)
            {
                string innerExceptions = e.InnerException != null ? e.InnerException.ToString() : "";
                throw new Exception("Enemizer failed for this seed, please try another seed.\n\n" + e.Message + "\n" + innerExceptions);
            }
        }

    }

    public class BaseEnemiesCollection
    {
        // sum of overlay code per actortype in this collection
        public int OverlayRamSize;
        // sum of all enemy instances struct ram requirements
        public int ActorInstanceSum;
        // sum of object size
        public int ObjectRamSize;
        public int[] objectSizes; //debug
        // list of enemies that were used to make this
        public List<Actor> oldActorList = null;

        public BaseEnemiesCollection(List<Actor> actorList, List<int> objList, Scene s)
        {
            oldActorList = actorList;
            var distinctActors = actorList.Select(u => u).DistinctBy(u => u);
            OverlayRamSize = distinctActors.Select(x => ActorUtils.GetOvlCodeRamSize(x.ActorID)).Sum();
            ActorInstanceSum = actorList.Select(u => u.ActorID)
                                        .Select(x => ActorUtils.GetOvlInstanceRamSize(x, Enemies.InjectedActors)).Sum();
            // untested for accuracy, actors without correct objects might be inccorectly sized
            objectSizes = objList.Select(x => ObjUtils.GetObjSize(x)).ToArray();
            //this.ObjectRamSize = objList.Select(x => ObjUtils.GetObjSize(x)).Sum();
            this.ObjectRamSize = objectSizes.Sum();


            CalculateDefaultObjectUse(s);
        }

        public void CalculateDefaultObjectUse(Scene s)
        {
            // now that we know the hard object bank limits, we need ALL data
            // in addition to the scene objects, we need the objects that are always loaded
            this.ObjectRamSize += 0x925E0; // gameplay_keep
            this.ObjectRamSize += 0x1E250; // the biggest link form object (child)
            // scenes can have special scene objects, which arent included in actor objects
            if (s.SpecialObject == Scene.SceneSpecialObject.FieldKeep)
            {
                this.ObjectRamSize += 0x9290; // field keep object
                /// I still dont know why epona sometimes spawns before the objects from scene are loaded, assumption its field
                if (s.SceneEnum != GameObjects.Scene.IkanaCanyon)
                {
                    this.ObjectRamSize += 0xE4F0; // epona
                }
            }
            else if (s.SpecialObject == Scene.SceneSpecialObject.DungeonKeep)
            {
                this.ObjectRamSize += 0x23280;
            }
        }

    }

    public class MapEnemiesCollection
    {
        public BaseEnemiesCollection day = null;
        public BaseEnemiesCollection night = null;

        public MapEnemiesCollection(List<Actor> actorList, List<int> objList, Scene scene)
        {
            // split enemies into day and night, init two types
            int dayFlagMask = 0x2AA; // nigth is just shifted to the right by one

            day = new BaseEnemiesCollection(actorList.FindAll(u => (u.GetTimeFlags() & dayFlagMask) > 0), objList, scene);
            night = new BaseEnemiesCollection(actorList.FindAll(u => (u.GetTimeFlags() & (dayFlagMask >> 1)) > 0), objList, scene);
        }
    }

    public class ActorsCollection
    {
        // per scene: per old and new: per room : per night and day: an object size, an actor inst size, and a actor code size
        // for each scene we need to check all of them, this is getting complicated

        public List<MapEnemiesCollection> oldMapList;
        public List<MapEnemiesCollection> newMapList;
        public Scene Scene;
        public int sceneObjectLimit;


        public ActorsCollection(Scene scene)
        {
            this.Scene = scene;
            this.oldMapList = new List<MapEnemiesCollection>();
            this.sceneObjectLimit = SceneUtils.GetSceneObjectBankSize(scene.SceneEnum);
            for (int i = 0; i < scene.Maps.Count; ++i)
            {
                var map = scene.Maps[i];
                this.oldMapList.Add(new MapEnemiesCollection(map.Actors, map.Objects, scene));
            }
        }

        // init for new replacements
        public void SetNewActors(Scene scene, List<ValueSwap> newObjChanges)
        {
            // this is the slowest part of our bogo sort, we need to try speeding it up

            this.newMapList = new List<MapEnemiesCollection>();
            // I like foreach better but its waaaay slower
            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];

                if (newObjChanges == null)
                {
                    throw new Exception("SetNewActors: empty object list");
                }
                {
                    var newObjList = map.Objects.ToList(); // copy
                    // probably a way to search for this with a lambda, can't think of it right now
                    for (int v = 0; v < newObjChanges.Count; ++v)
                    {
                        for (int o = 0; o < newObjList.Count; ++o)
                        {
                            if (newObjChanges[v].OldV == newObjList[o])
                            {
                                newObjList[o] = newObjChanges[v].NewV;
                            }
                        }
                    }
                    this.newMapList.Add(new MapEnemiesCollection(map.Actors, newObjList, scene));
                }
            }
        }

        public bool isSizeAcceptable()
        {
            // is the overall size for all maps of night and day equal

            var objectTest = isObjectSizeAcceptable();
            if (objectTest == false)
            {
                return false;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                // pos diff is smaller
                var sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].day, newMapList[map].day);
                if (sizeTest == false) {
                    return false;
                }

                sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].night, newMapList[map].night);
                if (sizeTest == false) {
                    return false;
                }

            }
            return true; // all of them passed size test
        }

        public bool CompareRamRequirements(Scene scene, BaseEnemiesCollection oldCollection, BaseEnemiesCollection newCollection)
        {
            var dayOvlDiff  = oldCollection.OverlayRamSize   - newCollection.OverlayRamSize;
            var dayInstDiff = oldCollection.ActorInstanceSum - newCollection.ActorInstanceSum;
            //var dayObjDiff  = oldCollection.ObjectRamSize    - newCollection.ObjectRamSize;

            // if the new size is smaller than the old size we should be dandy, if not...
            if (dayOvlDiff + dayInstDiff <= -0x100)
            {
                // SCT is 0x4FF90
                if (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x4FFFF) // need to find new safe values
                {
                    return false;
                }
                // I can't rule out halucination scrubs are or are not the issue, their skeleton->action is broken, that sounds like corrupted heap
                if (scene.SceneEnum == GameObjects.Scene.DekuPalace && newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x22000) // need to find new safe values
                {
                    return false;
                }
            }

            return true;
        }

        public bool isObjectSizeAcceptable()
        {
            // separated so I can call it twice to avoid extra work if its obviously too big too early

            for (int map = 0; map < oldMapList.Count; ++map)
            {
                if (newMapList[map].day.ObjectRamSize > sceneObjectLimit || newMapList[map].night.ObjectRamSize > sceneObjectLimit)
                {
                    return false;
                }
            }
            return true;
        }

        public List<int> GetUpdateObjectList(ValueSwap newChosenObjects )
        {
            //var newObjectList = this.Scene.Maps.
            // change it, return it

            return null;
        }

        // print to log function
        public void PrintCombineRatioNewOldz(StringBuilder log)
        {
            void PrintCombineRatioNewOld(string text, int newv, int oldv){
                log.AppendLine(text + " ratio: [" + ((float) newv / (float) oldv).ToString("F4")
                    + "] newsize: [" + newv.ToString("X6") + "] oldsize: [" + oldv.ToString("X6") + "]");
            }

            if (newMapList == null)
            {
                log.AppendLine(" ERROR: New list was dead!");
                return;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                var newDTotal = newMapList[map].day.OverlayRamSize + newMapList[map].day.ActorInstanceSum;
                var oldDTotal = oldMapList[map].day.OverlayRamSize + oldMapList[map].day.ActorInstanceSum;
                var newNTotal = newMapList[map].night.OverlayRamSize + newMapList[map].night.ActorInstanceSum;
                var oldNTotal = oldMapList[map].night.OverlayRamSize + oldMapList[map].night.ActorInstanceSum;

                if (newDTotal - oldDTotal + newNTotal - oldNTotal == 0) continue; // map was untouched, dont print

                log.AppendLine(" ======( Map " + map.ToString("X2") + " )======");

                PrintCombineRatioNewOld("  day:    overlay ", newMapList[map].day.OverlayRamSize,   oldMapList[map].day.OverlayRamSize);
                PrintCombineRatioNewOld("  day:    struct  ", newMapList[map].day.ActorInstanceSum, oldMapList[map].day.ActorInstanceSum);
                PrintCombineRatioNewOld("  day:    total  =", newDTotal, oldDTotal);
                PrintCombineRatioNewOld("  day:    object  ", newMapList[map].day.ObjectRamSize, oldMapList[map].day.ObjectRamSize);

                PrintCombineRatioNewOld("  night:  overlay ", newMapList[map].night.OverlayRamSize,   oldMapList[map].night.OverlayRamSize);
                PrintCombineRatioNewOld("  night:  struct  ", newMapList[map].night.ActorInstanceSum, oldMapList[map].night.ActorInstanceSum);
                PrintCombineRatioNewOld("  night:  total  =", newNTotal, oldNTotal);
                PrintCombineRatioNewOld("  night:  object  ", newMapList[map].night.ObjectRamSize, oldMapList[map].night.ObjectRamSize);

                // print map objects size
                var hexString = "";
                for(int i = 0; i < newMapList[map].day.objectSizes.Length; i++)
                {
                    hexString += "0x" + newMapList[map].day.objectSizes[i].ToString("X") + " ";
                }
                var size = newMapList[map].day.objectSizes.Sum().ToString("X");
                var allSize = newMapList[map].day.ObjectRamSize.ToString("X");
                log.AppendLine($" object sizes: [ {hexString}]");
                log.AppendLine($"    sum: [0x{size}] allsize: [0x{allSize}]");
                log.AppendLine($" ------------------------------------------------- ");

            }
        } // end PrintCombineRatioNewOldz
    } // end actorsCollection
}
