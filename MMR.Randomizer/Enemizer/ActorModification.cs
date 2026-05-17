using MMR.Randomizer.Extensions;
using MMR.Randomizer.Utils;


namespace MMR.Randomizer.Enemizer
{
    class ActorModification
    {
        /// <summary>
        ///  This is for all asm based actor modifications Enemizer/Actorizer uses
        /// </summary>

        private static void FixScarecrowTalk()
        {
            /// scarecrow breaks if you try to teach him a song anywhere where he normally does not exist
            if (!Enemies.ReplacementListContains(GameObjects.Actor.Scarecrow)) return;

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

            // TODO now that we have new actors working, just fix as a new actor, so much easier than manually re-writing asm
        }

        private static void ExtendGrottoDirectIndexByte()
        {
            /// in MM the top nibble of the grotto variable is never used (0xF000)
            /// but in the vanilla code it be detected and used as a grotto warp index of the static grottos entrances array (-1)
            ///  MM normally uses the z rotation instead to index warp, but we can use either or
            /// however, only the 3 lower bits of this nibble are used, the code ANDS with 7
            /// why? the fourth bit isn't ever used by any grotto, and looking at the code shows it is never used
            /// so here, we set the ANDI 7 to F instead, allowing us extended access to the entrance array
            // TODO and by 0xF800 and shift less to get more range, requires re-writting all variants in actor list
            var grotholeFID = GameObjects.Actor.GrottoHole.FileListIndex();
            RomUtils.CheckCompressed(grotholeFID);
            RomData.MMFileList[grotholeFID].Data[0x2FF] = 0xF; // ANDI 0x7 -> ANDI 0xF
        }

        // grotto entrance list is extra, lets add some

        private static void EnablePoFusenAnywhere()
        {
            /// the flying poe baloon romani uses to play her game doesn't spawn unless
            ///  1) it has an explosion fuse timer OR
            ///  2) it detects romani actor in the scene, so it can count baloon pops
            /// but the code that blocks the baloon if neither of these are true is nop-able,
            ///   and the rest of the code is designed to work without issue in this case

            if (!Enemies.ReplacementListContains(GameObjects.Actor.PoeBalloon)) return;

            var enPoFusenFID = GameObjects.Actor.PoeBalloon.FileListIndex();
            RomUtils.CheckCompressed(enPoFusenFID);

            // nops the MarkForDeath function call, stops them from de-spawning
            ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[enPoFusenFID].Data, Dest: 0xF4, val: 0x00000000);

            // because they can now show up in weird places, they need to be poppable more ways
            // I mean.. its a baloon, it should have always been really easy to pop
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

            RomUtils.CheckCompressed(GameObjects.Actor.FriendlyCucco.FileListIndex());
            var niwData = RomData.MMFileList[GameObjects.Actor.FriendlyCucco.FileListIndex()].Data;
            // both of these changes made in EnNiw_Init
            ReadWriteUtils.Arr_WriteU32(niwData, 0x24A8, 0x40000000); // 9.9 -> 2 in f32 (in rodata)
            ReadWriteUtils.Arr_WriteU16(niwData, 0x156, 0x3F80); // 10 -> 1 in f32 (first short only as literal hardcoded)
        }

        public static void ModifyFireflyKeeseForPerching()
        {
            /// keese only have two params: type 0x7FFF and the 0x8000 flag which is lens sensitive
            /// except, I need to be able to tell rando which ones are perching and which are on the "wall"
            /// so I am changing the params erase code in init to & 0xF from & 0x7FFF for now since we only have 4 types anyway

            var fireflyFid = GameObjects.Actor.Keese.FileListIndex();
            RomUtils.CheckCompressed(fireflyFid);
            var fireflyData = RomData.MMFileList[fireflyFid].Data;

            // andi $t3, $t2, 0x7FFF
            fireflyData[0xC6] = 0x00; // 0x7F -> 00
            fireflyData[0xC7] = 0x0F; // 0xFF -> 0F
        }


        public static void FixThornTraps()
        {
            // dnf: never went back to thorn enemies, was waiting for decomp
        }

        public static void FixSeth2()
        {
            /// seth 2, the guy waving his arms in the termina field telescope, like oot spiderhouse
            /// his init code checks for a value, and does not spawn if the value is different than expected
            if (!Enemies.ReplacementListContains(GameObjects.Actor.Seth2)) return;

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
            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            tfScene.Maps[0].Actors[28].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Actors[29].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Objects[21] = GameObjects.Actor.Empty.ObjectIndex();
            //var map = tfScene.Maps[0];
        }

        public static void FixCuccoChicks()
        {
            /// this now gets overwritten by a rewritten cucco chick actor,
            /// this is left over in case the player does not have that actor

            // stop chicks from despawning if there is no object_niw (adult cucco) object
            var cuccoChickFID = GameObjects.Actor.CuccoChick.FileListIndex();
            RomUtils.CheckCompressed(cuccoChickFID);
            var cuccoChickData = RomData.MMFileList[cuccoChickFID].Data;
            // we need to branch past both the mark for death and the return (return before actor_update will just break the whole actor)
            ReadWriteUtils.Arr_WriteU32(cuccoChickData, 0x30, 0x10000005); // BGEZ -> B (branch always)
        }


        private static void FixBomberKidsGameFinishWarp()
        {
            /// for some weird reason, their warp is calculated in real time based on the player's position,
            /// the code is unknown, but.. it should always go to the same spot so we should be able to just replace it
            /// the saving kids warp is 0x6D50

            var bombjimbFid = GameObjects.Actor.BombersYouChase.FileListIndex();
            RomUtils.CheckCompressed(bombjimbFid);
            var bombjimbData = RomData.MMFileList[bombjimbFid].Data;
            // we want to replace the Entrance_CreateFromSpawn function call,
            // which would load the old entrance address into v0, with a manual load v0 with our warp
            ReadWriteUtils.Arr_WriteU32(bombjimbData, 0x1E88, 0x2402D650); // Jal Entrance_CreateFromSpawn -> Addiu V0, R0, 0xD650
            // sometimes uses the other entrance calculation where it gets it from the exit list
            // lets just jump past that
            ReadWriteUtils.Arr_WriteU32(bombjimbData, 0x1E28, 0x10000016); // BNEZ BREQ -> J to L80C02D24
        }

        private static void FixInjuredKoume()
        {
            /// Injured koume in the woods of mystery, her code checks if she is in the woods of mystery and self culls

            var koumeFID = GameObjects.Actor.InjuredKoume.FileListIndex();
            RomUtils.CheckCompressed(koumeFID);
            var koumeData = RomData.MMFileList[koumeFID].Data;
            // the code check is entrance, and then moves to kill, so just remove the branch
            ReadWriteUtils.Arr_WriteU32(koumeData, 0x2D38, 0x00000000); // BNE to actor kill -> NOP
        }

        public static void FixArmosSpawnPos()
        {
            /// for some reason armos changes its home and world position based on y rotation in init
            //
            // this->actor.home.pos.x -= 9.0f * Math_SinS(this->actor.shape.rot.y);
            // this->actor.home.pos.z -= 9.0f * Math_CosS(this->actor.shape.rot.y);
            // this->actor.world.pos.x = this->actor.home.pos.x;
            // this->actor.world.pos.z = this->actor.home.pos.z;
            /// and it makes no sense, breaks strayfairies because they need to be at the same spot, removing

            RomUtils.CheckCompressed(GameObjects.Actor.Armos.FileListIndex());
            var armosData = RomData.MMFileList[GameObjects.Actor.Armos.FileListIndex()].Data;

            // the four writes (home.x home.z world.x, world.z)
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0E0, val: 0x0000000); // reminder: all zero instruction is NOP
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x104, val: 0x0000000);
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0FC, val: 0x0000000);
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x110, val: 0x0000000);

            // for good measure, lets nop some of these expensive floating instructions leading to the save too
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0D4, val: 0x0000000); // mul.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0D8, val: 0x0000000); // sub.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0F4, val: 0x0000000); // mul.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x100, val: 0x0000000); // sub.s

            // god this compiler sucks, it LOADS the value it just stored to re-save it to a new location,
            // instead of reusing the already populated register
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0CC, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0EC, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0F0, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x108, val: 0x0000000); // lwc
        }

        private static void AllowGuruGuruOutside()
        {
            /// guruguru's actor spawns or kills itself based on time flags, ignoring that the spawn points themselves have timeflags
            /// if we want guruguru to be placed in the world without being restricted to day/night only (which is lame) we have to stop this
            if (!Enemies.ReplacementListContains(GameObjects.Actor.GuruGuru)) return;

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

        public static void FixSilverIshi()
        {
            /// in MM the silver boulders that are pickupable by goron are ishi in field_keep object
            /// however, these boulders always check the scene SwitchFlags and set the flags when destroyed, so you cannot respawn them
            ///   considering nothing in vanilla needs these, and because
            ///   I'm worried about setting flags for something else, lets remove switch flag behavior entirely

            var ishiFid = GameObjects.Actor.IshiRock.FileListIndex();
            RomUtils.CheckCompressed(ishiFid);
            var ishiData = RomData.MMFileList[ishiFid].Data;
            ReadWriteUtils.Arr_WriteU32(ishiData, Dest: 0x12CC, val: 0x00000000); // JAL (Actor_SetSwitchFlag) -> NOP
            // there is code to stop the boulder from dropping random good shit, remove
            ReadWriteUtils.Arr_WriteU32(ishiData, Dest: 0x8CC, val: 0x00000000); // BNEZ (If ! ishi param & 1) -> NOP
        }

        public static void FixBabaShadows()
        {
            /// En_Bba_01 is an unused actor who appears to be the grandma from the bomb proprieters shop
            /// however she uses an expensive and barely used shadow draw function that makes a custom shadow to match her body shape
            /// we need to remove it since its totally broken, it always draws the shadow at y=0 and it lags

            if (!Enemies.ReplacementListContains(GameObjects.Actor.BabaIsUnused)) return;

            var babaFid = GameObjects.Actor.BabaIsUnused.FileListIndex();
            RomUtils.CheckCompressed(babaFid);
            var babaData = RomData.MMFileList[babaFid].Data;
            // the end of the draw function must be skipped, so we branch past all of it to the end of the function
            ReadWriteUtils.Arr_WriteU32(babaData, Dest: 0xB34, val: 0x10000024); // <irrelevant code> -> Jump to 0xBC8 (beginning of register re-load)
        }

        private static void FixDragonFlyShadows()
        {
            /// Same as Bba above, this shadow draw function is both broken and expensive on the graphics engine
            /// this is the source of the lag for this actor, dubbed "Lag-on-fly"
            /// note: this was moved to a MMRA file (and a regular shadow was re-added),
            ///   left here incase they play without the actor file

            var dragonflyFid = GameObjects.Actor.DragonFly.FileListIndex();
            RomUtils.CheckCompressed(dragonflyFid);
            var dragonflyData = RomData.MMFileList[dragonflyFid].Data;
            // similar to baba, we see a loop followed by a finishing function, we want to skip both in the main draw function
            ReadWriteUtils.Arr_WriteU32(dragonflyData, Dest: 0x2498, val: 0x10000018); // <irrelevant code> -> Jump to 24E4
        }

        private static void FixShellBladeCollider()
        {
            // Shell blade, the clam enemy from OOT water temple, only shows up in one place in MM
            // this actor, in it's update function, checks if the player is swimming before checking the colliders, which means on land it cannot attack you
            // this code does not exist in OOT

            var sbFid = GameObjects.Actor.Shellblade.FileListIndex();
            RomUtils.CheckCompressed(sbFid);
            var shellBladeData = RomData.MMFileList[sbFid].Data;
            // null the branch
            ReadWriteUtils.Arr_WriteU32(shellBladeData, Dest: 0xCC4, val: 0x00000000); // BGZ (past the code we want) -> NOP
        }

        private static void FixDexihandDamage()
        {
            // dexihand takes no damage from a lot of different attacks on land
            // because in vanilla you only fight them underwater
            // this is un-immersive on land

            var dexihandFID = GameObjects.Actor.Dexihand.FileListIndex();
            RomUtils.CheckCompressed(dexihandFID);
            var dexihandData = RomData.MMFileList[dexihandFID].Data;
            // damage table offset is 0x2104, every entry is one byte, with the upper nibble being effect
            var damageTableOffset = 0x2104;
            // default damage is 1 with no effect, `0x01`
            //dexihandData[damageTableOffset + 0x1] = 0x1; // stick
            dexihandData[damageTableOffset + 0x0] = 0x1; // nuts was requested
            dexihandData[damageTableOffset + 0x2] = 0x1; // horse trample
            dexihandData[damageTableOffset + 0x9] = 0x1; // sword, if you can get lucky enough to hit before getting grabbed you deserve it
            dexihandData[damageTableOffset + 0xA] = 0x1; // goron pound
            dexihandData[damageTableOffset + 0xC] = 0x1; // ice arrow (there is no ice effect)
            dexihandData[damageTableOffset + 0xD] = 0x1; // light arrow (there is no light effect or drop)
            dexihandData[damageTableOffset + 0xE] = 0x1; // goron spike
            dexihandData[damageTableOffset + 0xF] = 0x1; // deku spin, if you can hit before he grabs with such small reach that should be fine
            dexihandData[damageTableOffset + 0x12] = 0x1; // deku flower launch, again, rare but should count it can kill lots
            dexihandData[damageTableOffset + 0x19] = 0x1; // spinattack
        }


        public static void ModifyActors()
        {
            EnablePoFusenAnywhere();
            FixScarecrowTalk();
            ShortenChickenPatience();
            ModifyFireflyKeeseForPerching();
            //FixSeth2();
            FixCuccoChicks();
            //FixStreamSfxVolume();
            FixBomberKidsGameFinishWarp();
            ExtendGrottoDirectIndexByte();
            FixInjuredKoume();
            FixArmosSpawnPos();
            AllowGuruGuruOutside();
            FixSilverIshi();
            FixBabaShadows();
            FixDragonFlyShadows();
            FixShellBladeCollider();
            FixDexihandDamage();
        }
    }
}
