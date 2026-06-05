using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Vectors;
using MMR.Randomizer.Utils;
using ObjectEnum = MMR.Randomizer.GameObjects.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Enemizer
{
    class SceneModification
    {

        /// <summary>
        /// Moves the deku baba in southern swamp
        ///   why? beacuse they are positioned in the elbow and its visually jarring when they spawn/despawn on room swap
        ///   its already noticeable in vanilla, but with mixed enemy rando it can cause whole new enemies to pop in and out
        /// </summary>
        public static void FixSouthernSwampDekuBaba(Random rng)
        {
            Scene southernswampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());

            var movedToFlower = southernswampScene.Maps[0].Actors[6];
            movedToFlower.Position = new vec16(2781, 57, 2390);
            movedToFlower.ChangeYRotation(45);

            var movedToTree = southernswampScene.Maps[0].Actors[4];
            movedToTree.Position = new vec16(2020, 22, 300); // placement: to the right as you approach witches, next to tree
            // rotation normal to wall behind it, turn to the right 90deg
            movedToTree.ChangeYRotation(270);

            // this actor normally faces the big oct, have them face away from the wall
            var nearSoaringStone = southernswampScene.Maps[0].Actors[44];
            nearSoaringStone.ChangeYRotation(90);

            // witch area babas
            var movedToGrass = southernswampScene.Maps[2].Actors[2];
            movedToGrass.Position = new vec16(2910, 14, -1075); // placement: between the bushes along the wall
            // rotation normal to wall behind it, turn to the left 90deg
            movedToGrass.ChangeYRotation(90);

            var movedToWaterFall = southernswampScene.Maps[2].Actors[3];
            movedToWaterFall.Position = new vec16(4240, -2, -1270); // placement: near waterfall

            // moving the clear swamp versions
            Scene clearSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwampClear.FileID());
            clearSwampScene.Maps[0].Actors[4].Position = new vec16(1686, 23, 416); // moved to pier
            clearSwampScene.Maps[0].Actors[6].Position = new vec16(1663, 5, -103); // moved out front a big
            // witch room
            clearSwampScene.Maps[2].Actors[2].Position = new vec16(3001, 8, -1070);
            clearSwampScene.Maps[2].Actors[3].Position = new vec16(4288, 11, -1312);

            var octarok = southernswampScene.Maps[0].Actors[3];
            if (rng.Next() % 100 >= 50) // chance of watersurface vs waterbottom
            {
                // move the southern swamp octorok to the surface 
                octarok.Position.y = 0; // set to water height
            }
            else
            {
                // leave on the bottom but change actor type to a water bottom actor
                octarok.ChangeActor(GameObjects.Actor.LikeLike, vars: 0, modifyOld: true);
                octarok.OldName = octarok.Name = "Octarok(Floor)";
                // have to update the objects too
                foreach (var map in southernswampScene.Maps)
                {
                    var objectLoc = map.Objects.FindIndex(obj => obj == GameObjects.Actor.Octarok.ObjectIndex());
                    map.Objects[objectLoc] = GameObjects.Actor.LikeLike.ObjectIndex();
                }
            }
        }

        private static void FixRoadToSouthernSwampBadBat()
        {
            /// bad bat can randomize as a wall enemy or flying enemy, 
            ///   so move all flying ones to places where they can fit in as wall enemies or fly off
            ///   EXCEPT: right now I have an issue where they can be spiders with path because they can be wall enemies,
            ///   so for now change them to wall only

            // the bat at the top of the tree is in the way (takes off flies around)
            // move them to the further wall as a wall/flying enemy
            var roadtoswampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RoadToSouthernSwamp.FileID());
            var movedDownTreeBat = roadtoswampScene.Maps[0].Actors[7];
            movedDownTreeBat.Position = new vec16(927, -29, 2542); // placement: along the south east corner
            // match rotation with the wall
            movedDownTreeBat.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 225, flags: movedDownTreeBat.Rotation.y); ;
            ActorUtils.FlattenPitchRoll(movedDownTreeBat);
            movedDownTreeBat.ChangeVariant(0xFF9F); // change to perched on wall type

            // the bad bad on the tree is just far enough from the tree to cause a bombchu explosion, move closer
            var movedCloserToTreeBat = roadtoswampScene.Maps[0].Actors[8];
            movedCloserToTreeBat.Position.x = 422;

            // move corridor bat to the short cliff wall near swamp shooting galery
            var movedToCliffBat = roadtoswampScene.Maps[0].Actors[6];
            movedToCliffBat.Position = new vec16(2432, -40, 2871);
            // match rotation with the other tree sitting bat
            movedToCliffBat.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: movedToCliffBat.Rotation.y);
            movedDownTreeBat.ChangeVariant(0xFF9F); // change to perched on wall type

            // because the third bat was moved out of center corridor back, move one of the baba forward, we're basically swapping them
            var movedForwardDekuBaba = roadtoswampScene.Maps[0].Actors[14];
            movedForwardDekuBaba.Position.x = 1990;
            movedForwardDekuBaba.Position.z = 2594;
            movedForwardDekuBaba.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 195, flags: movedForwardDekuBaba.Rotation.y);
        }

        public static void NudgeFlyingEnemiesForTingle()
        {
            /// if tingle can be randomized, he can end up on any flying enemy in scenes that don't already have a tingle
            /// some of these scenes would drop him into water or off the cliff where he cannot be reached
            if (!Enemies.ReplacementListContains(GameObjects.Actor.Tingle)) return;

            var woodfallexteriorScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Woodfall.FileID());
            var firstDragonfly = woodfallexteriorScene.Maps[0].Actors[4];
            firstDragonfly.Position.x = 990; // over a deku scrub
            firstDragonfly.Position.z = 690;

            var secondDragonfly = woodfallexteriorScene.Maps[0].Actors[5];
            secondDragonfly.Position.x = 615; // over a lillypad
            secondDragonfly.Position.z = -495;

            var lilypad = woodfallexteriorScene.Maps[0].Actors[37];
            lilypad.Position.x = 615; // move lilypad over
            lilypad.Position.z = -495;

            var coastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
            coastScene.Maps[0].Actors[17].Position.z = 3033; // edge the guay over the land just a bit

            // to prevent him from falling to abyss
            var snowheadKeese = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Snowhead.FileID()).Maps[0].Actors[0];
            snowheadKeese.Position.x = -758;
        }

        private static void DistinguishLogicRequiredDekuFlowers()
        {
            // for objectless actorizer, some deku flowers must be held back because they require logic, but all deku flowers use the same params
            // but the 0xFF param space is unused, so we dont have to worry about changing it to mark our requirements

            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            var aboveCowGrottoFlower = tfScene.Maps[0].Actors[48];
            aboveCowGrottoFlower.OldVariant = aboveCowGrottoFlower.Variants[0] = 0x0077;
        }

        private static void DuplicateObjectForTorchInButlerRace()
        {
            /// the butler race area seems to have a completely unnecessary extra object: deku palace guard
            /// going to try replacing it with torches so we can randomize the torches

            var dekuShrineScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuShrine.FileID());

            foreach (var map in dekuShrineScene.Maps)
            {
                var objLoc = map.Objects.FindIndex(obj => obj == GameObjects.Actor.DekuPatrolGuard.ObjectIndex());
                map.Objects[objLoc] = GameObjects.Actor.Torch.ObjectIndex();
            }

            // gotta change the torch variant to non-vanilla so actorizer doesnt touch it
            var torch = dekuShrineScene.Maps[0].Actors[19]; // 0x287F default vars
            torch.OldVariant = 0x28FF; // setting group to 1 insted of zero is the best I think I can do here? it doesnt crash kz
        }

        private static void FixTerminaFieldActorPosRot()
        {
            ///   some of the Eeno and Leever spawns in north termina field is too high above the ground, 
            ///    we never notice because it falls to the ground before we can get there normally
            ///    but if its a stationary enemy, like a dekubaba, it hovers in the air

            // too high or low, move to ground 
            var terminafieldScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            terminafieldScene.Maps[0].Actors[144].Position.y = -245; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[16].Position.y = -209; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[17].Position.y = -185; // fixes the eeno that is too high above ground (bombchu explode)
            terminafieldScene.Maps[0].Actors[60].Position.y = -60;  // fixes the blue bubble that is too high
            terminafieldScene.Maps[0].Actors[107].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[110].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[121].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[153].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)

            // the south field dekubaba to the east is facing south, because in vanilla its direction does not matter
            // rotate to face out of the field
            var southDekubaba = terminafieldScene.Maps[0].Actors[45];
            southDekubaba.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: southDekubaba.Rotation.y);
            southDekubaba = terminafieldScene.Maps[0].Actors[44];
            southDekubaba.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: southDekubaba.Rotation.y);

        }

        private static void FixWoodfallTemplePosRot()
        {
            // in WFT, the dinofos spawn is near the roof, lower to ground
            var woodfalltempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WoodfallTemple.FileID());
            woodfalltempleScene.Maps[7].Actors[0].Position.y = -1208;

            // one of the snappers is right in front of the chest, if actorizer, that actor could be something that doesnt have to be killable, could block the chest
            woodfalltempleScene.Maps[6].Actors[1].Position.z = -55; // room 7, z was -25, 
        }

        private static void FixDekuPalacePosRot()
        {
            var dekuPalace = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());

            // the torches are really close to the bean hole, we can spread them wider a bit
            dekuPalace.Maps[1].Actors[26].Position.z -= 10; // left
            dekuPalace.Maps[1].Actors[28].Position.z -= 10; // left
            dekuPalace.Maps[1].Actors[27].Position.z += 10; // right
            dekuPalace.Maps[1].Actors[25].Position.z += 10; // right

            // deku bean torches north, rotate 
            dekuPalace.Maps[1].Actors[25].ChangeYRotation(270);
            dekuPalace.Maps[1].Actors[26].ChangeYRotation(270);
            dekuPalace.Maps[1].Actors[27].ChangeYRotation(270);
            dekuPalace.Maps[1].Actors[28].ChangeYRotation(270);
            // west side hp torches face... north? turn them to face the player
            dekuPalace.Maps[2].Actors[33].ChangeYRotation(180);
            dekuPalace.Maps[2].Actors[34].ChangeYRotation(180);

            // green rup torches face north as well
            dekuPalace.Maps[2].Actors[29].ChangeYRotation(270);
            dekuPalace.Maps[2].Actors[30].ChangeYRotation(270);
            dekuPalace.Maps[2].Actors[31].ChangeYRotation(270);
            dekuPalace.Maps[2].Actors[32].ChangeYRotation(270);
        }

        private static void FixTradingPostPosRot()
        {
            // the "trees" in trading post including bushes are in weird places, move them around the fire and the table
            var tradingPost = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TradingPost.FileID());

            var firstBush = tradingPost.Maps[0].Actors[2]; // first right bush
            firstBush.ChangeYRotation(80);

            var secondBush = tradingPost.Maps[0].Actors[4]; // next to table to fish case
            secondBush.ChangeYRotation(90);

            var thirdBush = tradingPost.Maps[0].Actors[5]; // behind table should be facing table
            thirdBush.ChangeYRotation(210);

            var tradingPostPot = tradingPost.Maps[0].Actors[8];
            tradingPostPot.ChangeYRotation(270); // rotate right toward player away from front wall
        }

        public static void FixSwampSpiderHousePosRot()
        {
            // this torch is too close to spider, constantly actors get stuck, move slightly out of the way
            var swampSpiderHouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SwampSpiderHouse.FileID());
            swampSpiderHouseScene.Maps[3].Actors[3].Position.x = -480;

            // rotate torches so replacements dont face the wall
            var spiderTorch2 = swampSpiderHouseScene.Maps[3].Actors[2];
            spiderTorch2.ChangeYRotation(135);
            var spidertorch3 = swampSpiderHouseScene.Maps[5].Actors[1];
            spidertorch3.ChangeYRotation( 180 - 45);
            var spidertorch4 = swampSpiderHouseScene.Maps[5].Actors[4];
            spidertorch4.ChangeYRotation(180 - 45);
            var spidertorch5 = swampSpiderHouseScene.Maps[5].Actors[2];
            spidertorch5.ChangeYRotation(45);
            var spidertorch6 = swampSpiderHouseScene.Maps[5].Actors[3];
            spidertorch6.ChangeYRotation(180 + 90 + 45);

        }

        // TODO this is too big, shrink
        public static void FixSpawnLocations(bool ACTORSENABLED)
        {
            /// in Enemizer some spawn locations are noticably buggy

            FixTerminaFieldActorPosRot();
            FixWoodfallTemplePosRot();
            FixDekuPalacePosRot();
            FixTradingPostPosRot();
            FixSwampSpiderHousePosRot();

            // in STT, move the bombchu in the first room 
            //   backward several feet from the chest, so replacement cannot block the chest
            var stonetowertempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StoneTowerTemple.FileID());
            stonetowertempleScene.Maps[0].Actors[3].Position.z = -630;
            // biobaba in the right room spawns under the bridge, if octarock it pops up through the tile, move to the side of the bridge
            stonetowertempleScene.Maps[3].Actors[19].Position.x = 1530;


            // same in secret shrine, all three dinofos are in the air
            var secretShrineScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SecretShrine.FileID());
            secretShrineScene.Maps[2].Actors[0].Position.y = 0;
            secretShrineScene.Maps[2].Actors[1].Position.y = 0;
            secretShrineScene.Maps[2].Actors[2].Position.y = 0;

            var linkTrialScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LinkTrial.FileID());
            linkTrialScene.Maps[1].Actors[0].Position.y = 1; // up high dinofos spawn, red bubble would spawn in the air, lower to ground

            // in OSH, the storage room bo spawns in the air in front of the mirror, 
            //  but as a land enemy it should be placed on the ground for its replacements
            var oceanspiderhouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.OceanSpiderHouse.FileID());
            var storageroomBo = oceanspiderhouseScene.Maps[5].Actors[2];
            // lower to the floor 
            storageroomBo.Position = new vec16(-726, -118, -1651);

            // in GBT, the bombchus on the pipes are in bad spots to be replaced by something unpassable,
            // but most people dont notice where their original spawn even is so move them
            var greatbaytempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayTemple.FileID());
            // the bombchu along the green pipe in the double seesaw room needs to be moved in case its an unmovable enemy
            greatbaytempleScene.Maps[10].Actors[3].Position.z = 344; // new vec16(3525, -180, 630); // this was hard to open if chest
            // the bombchu along the red pipe in the pre-wart room needs the same kind of moving
            greatbaytempleScene.Maps[6].Actors[7].Position = new vec16(-1840, -570, -870);

            var piratesFortressCourtyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortress.FileID());
            piratesFortressCourtyardScene.Maps[0].Actors[17].Position.x = 1267; // the pirate at the top of ladder, needs to be moved further into the bridge
            piratesFortressCourtyardScene.Maps[0].Actors[17].Position.y = 319;
            piratesFortressCourtyardScene.Maps[0].Actors[20].Position.y = -200; // too high, can cause bombchu to explode

            // in pre-clocktown there is a keaton grass, but it doesn't work because there is no keaton object, but we can fix that
            var beforeClockTownFID = GameObjects.Scene.BeforeThePortalToTermina.FileID();
            var preclocktownScene = RomData.SceneList.Find(scene => scene.File == beforeClockTownFID);
            preclocktownScene.Maps[0].Objects.Add(GameObjects.Actor.Keaton.ObjectIndex());
            var clocktownroomData = RomData.MMFileList[beforeClockTownFID + 1].Data;
            clocktownroomData[0x31] = (byte)preclocktownScene.Maps[0].Objects.Count();

            if (ACTORSENABLED)
            {
                var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
                var torchRotation = dekuPalaceScene.Maps[2].Actors[26].Rotation.z;
                torchRotation = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: torchRotation); // reverse, so replacement isn't nose into the wall

                // torch near the hp is facing the wall, actors replacing it also face the same way, bad
                // one of these is not required and does nothing
                dekuPalaceScene.Maps[2].Actors[25].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: 0x7F);
                //dekuPalaceScene.Maps[2].Actors[26].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: dekuPalaceScene.Maps[2].Actors[26].Rotation.y);


                // change the torch in pirates fort exterior to all day, remove second one, or free 
                var piratesExteriorScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortressExterior.FileID());
                var nightTorch = piratesExteriorScene.Maps[0].Actors[15];
                nightTorch.Rotation.x |= 0x7F; // always spawn flags
                nightTorch.Rotation.z |= 0x7F;

                // day torch
                piratesExteriorScene.Maps[0].Actors[13].ChangeActor(GameObjects.Actor.Empty, modifyOld: true); // dangeon object so no grotto, empty for now
                // todo: 14/16 are also torches, we dont really need both here


                // Jim the bomber actually spawns within the tree to the north... move is spawn over a bit
                var northClockTown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.NorthClockTown.FileID());
                var jimDuringTheGame = northClockTown.Maps[0].Actors[26];
                jimDuringTheGame.Position.x = -740;
                jimDuringTheGame.Position.z = -1790;
                // and rotate to face outwards not toward the wall
                jimDuringTheGame.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: (180 - 20), flags: jimDuringTheGame.Rotation.y);

                // the tree itself needs to be rotated as its facing the wall
                northClockTown.Maps[0].Actors[21].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 135, northClockTown.Maps[0].Actors[21].Rotation.y);

                // jimbo in east clock town giving you the book is in an odd spot, move to the poster
                var eastClockTown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
                eastClockTown.Maps[0].Actors[46].Position = new vec16(1335, 203, -1639);

               

                // we cannot randomize gorman brothers without randomizing their chasing horse counterparts
                // except, this scene has an almost unused object: kanban, for the square sign you can only access if you go through the second fence
                // what if we turn that into the same actor as the tree, and turn the second object into a second ingo
                var gormanTrack = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GormanRaceTrack.FileID());
                gormanTrack.Maps[0].Objects[11] = GameObjects.Actor.GormanBros.ObjectIndex();
                gormanTrack.Maps[0].Actors[75].ChangeActor(GameObjects.Actor.Treee, vars: 0xFF02, modifyOld: true);

                // sakon in the curiosity shop can block the door, which must be avoided
                var curiosityShop = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.CuriosityShop.FileID());
                var sutari = curiosityShop.Maps[0].Actors[1];
                sutari.Position = new vec16(51, 3, -17); // move over to the side of the talking grate
                sutari.ChangeYRotation(90 + 15);

                // laundrypool wooden box is facing into the wall
                var laundrypoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LaundryPool.FileID());
                var woodenBox = laundrypoolScene.Maps[0].Actors[7];
                woodenBox.ChangeYRotation(180);

                var mayorsResitenceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MayorsResidence.FileID());
                var gormanInResidence = mayorsResitenceScene.Maps[0].Actors[1];
                gormanInResidence.Position = new vec16(77, 15, 148);
                gormanInResidence.ChangeYRotation(180 + 90);

                // this one is facing the door which is odd, turn to face madam
                gormanInResidence = mayorsResitenceScene.Maps[2].Actors[1];
                gormanInResidence.ChangeYRotation(180 + 90);

                // bombers hideout torch is facing a funny way
                var bombersHideoutScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.AstralObservatory.FileID());
                var blastWallTorch = bombersHideoutScene.Maps[0].Actors[15];
                blastWallTorch.ChangeYRotation(270); // face the bombable wall
                // and move a bit away from the far wall
                blastWallTorch.Position.z -= 40;

                // the gibdos in ikana canyon, two of them are basically on top of each other can lead to weird shinanigans
                var ikanaCanyonScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCanyon.FileID());
                var doubledGibdo = ikanaCanyonScene.Maps[0].Actors[64];
                doubledGibdo.Position = new vec16(-602, 400, 972);

                var milkbarScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
                milkbarScene.Maps[0].Objects[10] = GameObjects.Actor.ArcheryMiniGameMan.ObjectIndex();

                // the ceiling water drip effect actor was placed too close to the door, can softlock if it knocks the player away (skulltula)
                var underGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.BeneathGraveyard.FileID());
                underGraveyardScene.Maps[0].Actors[1].Position.x = 20; // facing door from hole, move back toward door
                underGraveyardScene.Maps[0].Actors[1].Position.z = 251; // facing door from hole, move left toward day 2

                // in blacksmith building, there are two pots that need to be rotated
                var mountainSmithyScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainSmithy.FileID());
                var leftSmithyPot = mountainSmithyScene.Maps[0].Actors[4];
                leftSmithyPot.ChangeYRotation(180);
                var rightSmithyPot = mountainSmithyScene.Maps[0].Actors[8];
                rightSmithyPot.ChangeYRotation(180);
                rightSmithyPot.Position.x = -70;
                rightSmithyPot.Position.z = 288;

                var mountainVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillage.FileID());
                var leftMountainVillagePot = mountainVillageScene.Maps[0].Actors[35];
                leftMountainVillagePot.ChangeYRotation(270);
                var rightMountainPot = mountainVillageScene.Maps[0].Actors[36];
                rightMountainPot.ChangeYRotation(270);


                // there is a mushroom spawn at the base of the tree in road to swamp, move it to the south side of the tree
                var roadToSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RoadToSouthernSwamp.FileID());
                var roadToSwampMushroom = roadToSwampScene.Maps[0].Actors[43];
                roadToSwampMushroom.Position = new vec16(366, -182, 2200);

                // in spring there are two torches on top of each other, which is weird, move the other one to face the first one
                //var mountainVillageSpring = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillageSpring.FileID());
                //var secondTorch = mountainVillageSpring.Maps[0].Actors[13];
                //secondTorch.Rotation.y = ActorUtils.MergeRotationAndFlags(180, secondTorch.Rotation.y);
                //secondTorch.Position.z -= 50;
            }
        }


        private static void RotateTalkSpotsAndHitSpots()
        {
            // lots of talk spots and hit spots have no rotation and need to be adjusted or they are half stuck in the wall weirdly

            var stockpotInnScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StockPotInn.FileID());
            var stockpotinnmaskHitSpot = stockpotInnScene.Maps[0].Actors[14];
            ActorUtils.ClearActorRotationRestrictions(stockpotinnmaskHitSpot);
            stockpotinnmaskHitSpot.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: stockpotinnmaskHitSpot.Rotation.y);

            // if the clocktown talk points are randomized, we want to rotate them as they dont have set rotation
            // this shit does nothing because something funky is going on, the rotation is not what it is in vanilla and its being ignored????
            var westClocktownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WestClockTown.FileID());
            var curiosityshopSign = westClocktownScene.Maps[0].Actors[22];
            curiosityshopSign.ChangeYRotation(180 - 27);
            ActorUtils.ClearActorRotationRestrictions(curiosityshopSign);
            var tradingpostSign = westClocktownScene.Maps[0].Actors[9];
            tradingpostSign.Rotation.y = ActorUtils.MergeRotationAndFlags(180 - 45, flags: tradingpostSign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(tradingpostSign);
            var bombshopSign = westClocktownScene.Maps[0].Actors[2];
            bombshopSign.Rotation.y = ActorUtils.MergeRotationAndFlags(180 - 71, flags: bombshopSign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(bombshopSign);
            //bombshopSign.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING
            var lotterySign = westClocktownScene.Maps[0].Actors[25];
            lotterySign.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: lotterySign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(lotterySign);

            var eastClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());

            // TODO now that I have simple rotation functions, go through these and replace them
            var treasurePoster = eastClockTownScene.Maps[0].Actors[20]; // east side
            treasurePoster.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: treasurePoster.Rotation.y);
            treasurePoster.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: treasurePoster.Rotation.x);
            ActorUtils.ClearActorRotationRestrictions(treasurePoster);
            var treasurePosterNorth = eastClockTownScene.Maps[0].Actors[20]; // east side
            treasurePosterNorth.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: treasurePosterNorth.Rotation.y);
            treasurePosterNorth.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: treasurePosterNorth.Rotation.x);
            ActorUtils.ClearActorRotationRestrictions(treasurePosterNorth);
            var constructionPoster = eastClockTownScene.Maps[0].Actors[17];
            constructionPoster.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: constructionPoster.Rotation.y);
            constructionPoster.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: constructionPoster.Rotation.x);
            ActorUtils.ClearActorRotationRestrictions(constructionPoster);
            var zoraPoster1 = eastClockTownScene.Maps[0].Actors[14];
            zoraPoster1.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: zoraPoster1.Rotation.x);
            zoraPoster1.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zoraPoster1.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster1);
            var zoraPoster2 = eastClockTownScene.Maps[0].Actors[15];
            zoraPoster2.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: zoraPoster1.Rotation.x);
            zoraPoster2.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zoraPoster2.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster2);
            var zoraPoster3 = eastClockTownScene.Maps[0].Actors[16];
            zoraPoster3.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: zoraPoster3.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster3);

            var hitspotLeft = eastClockTownScene.Maps[0].Actors[42];
            hitspotLeft.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: hitspotLeft.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(hitspotLeft);

            var hitspotRight = eastClockTownScene.Maps[0].Actors[43];
            hitspotRight.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: hitspotRight.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(hitspotRight);

            var basketSpot = eastClockTownScene.Maps[0].Actors[22];
            basketSpot.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: basketSpot.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(basketSpot);

            var archerySign = eastClockTownScene.Maps[0].Actors[24];
            archerySign.ChangeYRotation(270 - 45);
            archerySign.ChangeXRotation(0);
            ActorUtils.ClearActorRotationRestrictions(archerySign);

            var soldierSign = eastClockTownScene.Maps[0].Actors[21];
            soldierSign.ChangeYRotation(270);
            ActorUtils.FlattenPitchRoll(soldierSign);
            ActorUtils.ClearActorRotationRestrictions(soldierSign);

            var southclocktownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var recruitmentPoster = southclocktownScene.Maps[0].Actors[9];
            recruitmentPoster.ChangeYRotation(270);
            ActorUtils.FlattenPitchRoll(recruitmentPoster);
            ActorUtils.ClearActorRotationRestrictions(recruitmentPoster);

            var bankPoster = southclocktownScene.Maps[0].Actors[10];
            bankPoster.ChangeYRotation(90);
            ActorUtils.FlattenPitchRoll(bankPoster);
            ActorUtils.ClearActorRotationRestrictions(bankPoster);
        }

        private static void FixSpecificLikeLikeVariants()
        {
            /// some likelikes dont follow the normal water/ground type variety, we want detection to correctly ID them
            ///  here we switch their types to match for replacement in enemizer auto-detection

            var coastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
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

        private static void FixSpecificTektiteTypes()
        {
            var twinIslandsSpring = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            twinIslandsSpring.Maps[0].Actors[2].Variants[0] = 0xFFFD;
        }

        public static void RemoveSTTUnusedPoe()
        {
            /// regular stone tower, not inverted, has a unused poe object
            /// we can recover some object buffer headroom by removing it
            ///   remember to delete this if I ever get free objects working instead

            var stonetowertempleScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.StoneTowerTemple);
            for (int i = 0; i < stonetowertempleScene.Maps.Count; ++i)
            {
                var room = stonetowertempleScene.Maps[i];
                var poeIndex = room.Objects.FindIndex(obj => obj == GameObjects.Actor.Poe.ObjectIndex());
                if (poeIndex > 0)
                {
                    room.Objects[poeIndex] = (int) ObjectEnum.SmallestObj;
                }
            }
        }

        public static void RandomlySwapOutZoraBandMember(Random rng)
        {
            /// almost all zora in zora hall use the same object, so we cant swap any out without hitting them all
            /// except, all band member objects are present all the time even though they only show up outside for the concert
            /// so randomly choose one to turn into a duplicate zora object, so we can change one and leave the other for door zora
            ///   since most rando players dont care about the concert anyway, and wouldnt even notice one member missing
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.RegularZora)) return;

            // 2:japas, 3:evan, 5:tijo, can't remove lulu or the concert is completely broken? meh
            var replacableBandObj = new int[] { 2, 3, 5, 4 };
            var randomObjListIndex = replacableBandObj[rng.Next(replacableBandObj.Length)];
            var zoraHallScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHall.FileID());
            var previousObject = zoraHallScene.Maps[0].Objects[randomObjListIndex];
            zoraHallScene.Maps[0].Objects[randomObjListIndex] = GameObjects.Actor.RegularZora.ObjectIndex();

            // we should also swap out the actor with the object to a zora too
            foreach (var actor in zoraHallScene.Maps[0].Actors)
            {
                if (actor.ObjectId == previousObject)
                {
                    actor.ChangeActor(GameObjects.Actor.RegularZora, vars: 0xFC08, modifyOld: true);
                    actor.OldName = "ZoraBandStandIn";
                }
            }

            // because of this change, the whole string of watchers are all active before the dungeon too,
            //   move some down below so its not so crowded
            zoraHallScene.Maps[0].Actors[29].Position = new vec16(376, 2, 676); // down by the water
            zoraHallScene.Maps[0].Actors[27].Position = new vec16(-448, 2, -408); // behind the water fall near lulu
            zoraHallScene.Maps[0].Actors[28].Position = new vec16(-1002, 179, 1089); // near front door

            // TODO we really need to break the zora into multiple types, pathing, perching and standing? thats crazy

            // beacuse the zora band members are randomized, lulu can show up right on top of the regular zora guy,
            var cordinationZora = zoraHallScene.Maps[0].Actors[21];
            cordinationZora.Position = new vec16(-223, 46, -312); // moved to the left, toward the left speaker
        }

        public static void SplitOceanSpiderhouseSpiderObject()
        {
            /// in the ocean spiderhouse there are two actors using the same object: gold skulltula and skulltula (big spider)
            /// we cannot randomize one without the other because they both use the same object
            /// except... if we change the actor and object out for dummy, we can trick rando to allow us to change them

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Skulltula)) return;

            var grottoScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.OceanSpiderHouse.FileID());
            var spiderChestRoom = grottoScene.Maps[4];

            // object 6 is Bo, its not the spider object but I think thats is safer to replace in this spot
            spiderChestRoom.Objects[6] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderChestRoom.Actors[0].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 1, modifyOld: true);
            spiderChestRoom.Actors[0].OldName = spiderChestRoom.Actors[0].Name = "SkullTulla";

            var spiderStorageRoom = grottoScene.Maps[5];

            // object 9 is Stalchild, its not the spider object but I think thats is safer to replace in this spot
            spiderStorageRoom.Objects[9] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderStorageRoom.Actors[1].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 1, modifyOld: true);
            spiderStorageRoom.Actors[1].OldName = spiderStorageRoom.Actors[1].Name = "SkullTulla";
        }

        private static void FixDekuPalaceReceptionGuards()
        {
            /// if we randomize the patrolling guards in deku palace:
            /// we end up removing the object the front guards require to spawn
            /// however there is a (as far as I can tell) unused object in this scene we can swap
            /// object_dns which is the object used by the dancing deku guards in the king's chamber
            /// nothing seems to use their object in the regular palace scene, no idea why the object is there
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.DekuPatrolGuard)) return;

            var frontGuardOID = GameObjects.Actor.DekuPatrolGuard.ObjectIndex();
            var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());

            /*if (!dekuPalaceScene.Maps[0].Objects.Contains(frontGuardOID))
            {
                // scene has already been written at this point, need to romhack it, faster than re-writing the whole scene file
                var dekuPalaceRoom1FID = GameObjects.Scene.DekuPalace.FileID() + 1;
                var dekuPalaceRoom1File = RomData.MMFileList[dekuPalaceRoom1FID].Data;
                ReadWriteUtils.Arr_WriteU16(dekuPalaceRoom1File, Dest: 0x4E, (ushort)frontGuardOID);
            } // */
            dekuPalaceScene.Maps[0].Objects[7] = frontGuardOID;
            dekuPalaceScene.Maps[1].Objects[7] = frontGuardOID;
            dekuPalaceScene.Maps[2].Objects[7] = frontGuardOID;
        }


        private static void SwapShopActors()
        {
            /// the smaller shop actor (3 items for sale) can have one of three separate objects:
            ///   zora for zora shop, goron for goron shop, and the old man in the bomb shop
            /// actor rando wont randomize them without their objects and their actors both being in the same place,
            ///   changing the scene object to match is good enough

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.ShopSeller)) return;

            // even if the object is left alone, I have to move him (cannot see)
            var bombShopScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.BombShop.FileID());
            var isBombShopObjectRestricted = Enemies.ObjectIsCheckBlocked(GameObjects.Scene.BombShop, GameObjects.Actor.ShopSeller);
            if (isBombShopObjectRestricted == null)
            {
                var bombshopMan = bombShopScene.Maps[0].Actors[0];
                bombshopMan.Position = new vec16(198, -30, -15); // his vanilla position is behind the rocked on the left, cannot see his replacement actor at all
                bombShopScene.Maps[0].Objects[1] = (int) ObjectEnum.SmallestObj; // chu
                bombShopScene.Maps[0].Objects[2] = (int) ObjectEnum.SmallestObj; // bomb
                bombShopScene.Maps[0].Objects[4] = (int) ObjectEnum.SmallestObj; // bombbag
            }

            var zoraShopScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHallRooms.FileID());
            var isZoraShopObjectRestricted = Enemies.ObjectIsCheckBlocked(GameObjects.Scene.ZoraHallRooms, GameObjects.Actor.ShopSeller);
            if (isZoraShopObjectRestricted == null)
            {
                zoraShopScene.Maps[4].Objects[1] = GameObjects.Actor.ShopSeller.ObjectIndex(); // main object
                // unused shop objects, shrink to give us more space
                zoraShopScene.Maps[4].Objects[2] = (int) ObjectEnum.SmallestObj; // arrows
                zoraShopScene.Maps[4].Objects[3] = (int) ObjectEnum.SmallestObj; // potions
                zoraShopScene.Maps[4].Objects[5] = (int) ObjectEnum.SmallestObj; // shield
            }

            var goronShopScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronShop.FileID());
            var isGoronShopObjectRestricted = Enemies.ObjectIsCheckBlocked(GameObjects.Scene.GoronShop, GameObjects.Actor.ShopSeller);
            if (isGoronShopObjectRestricted == null)
            {
                goronShopScene.Maps[0].Objects[1] = GameObjects.Actor.ShopSeller.ObjectIndex();
                goronShopScene.Maps[0].Objects[2] = (int) ObjectEnum.SmallestObj; // arrows
                goronShopScene.Maps[0].Objects[3] = (int) ObjectEnum.SmallestObj; // potion
                goronShopScene.Maps[0].Objects[4] = (int) ObjectEnum.SmallestObj; // bombs
            }

        }

        public static void FixSouthernSwampLensBehavior()
        {
            /// The southern swamp has inverted lens behavior, meaning lens items are invisible until you use lens to see them
            // except, is there a reason for this? seems like an after thought of using lens to find mushrooms but they switched to masks
            var poisonSwampRoom0Data = RomData.MMFileList[GameObjects.Scene.SouthernSwamp.FileID() + 1].Data;
            poisonSwampRoom0Data[0xE] = 0x10; // was 11 in vanilla, the 1 changes lens behavior
            // weirdly, its only the first room, the other rooms have regular lens behavior
        }

        public static void FixSouthernSwampGossipStoneObjectPlacement()
        {
            /// When an object changes position between rooms, some actor code gets confused because it asumes
            ///   the object will always be loaded in the same spot of the object list, and that memory locations are static
            /// Southern swamp switches the last object spot for the witch room from the regular room with the gossip stone object
            ///   this causes some actors to glitch out if they were replacing dekubaba
            var poisonSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());
            var witchMap = poisonSwampScene.Maps[2];
            witchMap.Objects[23] = GameObjects.Actor.TallGrass.ObjectIndex();
            witchMap.Objects[24] = GameObjects.Actor.GossipStone.ObjectIndex();

            // similar mis-ordered stuff happens in clear swamp
            // kotake object, which might not even be used at all, is last slot, but the gossip stone and torch object swap places at slot -5
            var clearSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwampClear.FileID());
            var mainRoom = clearSwampScene.Maps[0];
            var witchRoom = clearSwampScene.Maps[1];
            var backRoom = clearSwampScene.Maps[2];
            mainRoom.Objects[14] = witchRoom.Objects[14] = backRoom.Objects[14] = GameObjects.Actor.KotakeOnBroom.ObjectIndex();
            mainRoom.Objects[18] = backRoom.Objects[18] = GameObjects.Actor.Torch.ObjectIndex();
            witchRoom.Objects[18] = GameObjects.Actor.GossipStone.ObjectIndex();

            // and main area has tall grass and squaresign swapped
            mainRoom.Objects[15] = GameObjects.Actor.SquareSign.ObjectIndex();
            mainRoom.Objects[16] = GameObjects.Actor.TallGrass.ObjectIndex();
        }



        private static void ChangeIkanaCanyonCreditsActors(Random rng)
        {
            /// there are extra dead trees in the credits when pamela and her father are playing
            /// i want to change these

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.IkanaCanyonHookshotStump)) return;

            var ikanaCanyonScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCanyon.FileID());

            var creditsMainRoomLayer = ikanaCanyonScene.Maps[8];

            foreach (var act in creditsMainRoomLayer.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.IkanaCanyonHookshotStump))
            {
                if (rng.Next(100) < 40) // chance to instead become a flying second actor
                {
                    act.ChangeActor(GameObjects.Actor.BlueBubble, vars: 0xFFFF, modifyOld: true);
                    act.Position.y += 50;
                    act.OldName = "CreditsBlueBubble(Changling)";

                }
                else  // stay ground
                {
                    act.ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
                    act.OldName = "CreditsHookshotTree";
                }
            }

            creditsMainRoomLayer.Actors[2].ChangeActor(GameObjects.Actor.IkanaGravestone, vars: 0xFF00, modifyOld: true);
            creditsMainRoomLayer.Actors[2].OldName = "CreditsOwlStatue";

            // change objects to match
            creditsMainRoomLayer.Objects[3] = GameObjects.Actor.Bombiwa.ObjectIndex(); // from stump
            creditsMainRoomLayer.Objects[1] = GameObjects.Actor.BlueBubble.ObjectIndex(); // from ice block object ( we cant shoot ice arrows here)
            creditsMainRoomLayer.Objects[2] = GameObjects.Actor.IkanaGravestone.ObjectIndex(); // from owl object

            // most of these stumps are out of camera shot, they literally are never seen
            // move 9 to the north away from the castle
            creditsMainRoomLayer.Actors[9].Position = new vec16(-242, 203, 3783);

            // the three stumps on the upper terrace are not visible at all in any of the three camera shots
            // move this one from the furthest upper terace to the tree on the right side of the third camera shot
            creditsMainRoomLayer.Actors[10].Position = new vec16(-864, 600, 1933);
        }



        private static void RandomizeMonkeyActors()
        {
            /// randomizing monkeys can be annoying, change positions so replacemnets dont block or instantly hit player

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Monkey)) return;

            // we normally cannot randomize just the song monkey in the deku king chamber scene
            // because the object is needed for multiple monkeys
            // but the scene uses 5 objects, and since they come in pairs that means there is a free space we can add another object, adding the monkey back in

            var dekuKingScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuKingChamber.FileID());
            dekuKingScene.Maps[0].Objects.Add(GameObjects.Actor.Monkey.ObjectIndex());
            // we have to tell the room to load the extra object though
            var dekuKingSceneMap0FileData = RomData.MMFileList[GameObjects.Scene.DekuKingChamber.FileID() + 1].Data;
            dekuKingSceneMap0FileData[0x31] = 0x6; // updating object header object count from 5 to 6

            // monk facing the wrong way, turn
            var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
            dekuPalaceScene.Maps[0].Actors[11].Position = new vec16(-74, 0, 1466);
            dekuPalaceScene.Maps[0].Actors[11].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 45, flags: dekuPalaceScene.Maps[0].Actors[11].Rotation.y);

            // changing swamp monkeys into multiple different actor types for variety means different objects per room, which can corrupt objects

            // swamp monkey are annoying, we want to move them so they dont block things
            var southernSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());
            southernSwampScene.Maps[2].Actors[10].Position = new vec16(3826, 15, -1320); // those near witch, moved to the porch
            southernSwampScene.Maps[2].Actors[11].Position = new vec16(3729, 15, -1358);
            southernSwampScene.Maps[2].Actors[12].Position = new vec16(3619, 15, -1367);

            southernSwampScene.Maps[0].Actors[35].OldName = "Monkey(Near Road)";
            southernSwampScene.Maps[0].Actors[35].Position = new vec16(380, 64, -950); // near entrance
            southernSwampScene.Maps[0].Actors[35].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 0 + 30, flags: southernSwampScene.Maps[0].Actors[35].Rotation.y);
            //southernSwampScene.Maps[0].Actors[35].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);

            southernSwampScene.Maps[0].Actors[36].OldName = "Monkey(Near Road)";
            southernSwampScene.Maps[0].Actors[36].Position = new vec16(499, 58, -890);
            southernSwampScene.Maps[0].Actors[36].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270 - 30, flags: southernSwampScene.Maps[0].Actors[36].Rotation.y);
            //southernSwampScene.Maps[0].Actors[36].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);

            southernSwampScene.Maps[0].Actors[37].OldName = "Monkey(Near Road)";
            southernSwampScene.Maps[0].Actors[37].Position = new vec16(399, 46, -828); // this one is weirdly alright as is for rotation
            //southernSwampScene.Maps[0].Actors[37].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);

            // because we changed the monkey to bombiwa actor, we need to change the object to so that they will respond correctly
            //southernSwampScene.Maps[0].Objects[2] = GameObjects.Actor.Bombiwa.ObjectIndex();

            // same with monkey near the deku palace entrance
            southernSwampScene.Maps[1].Actors[34].OldName = "Monkey(Palace Entrance)";
            southernSwampScene.Maps[1].Actors[34].Position = new vec16(-681, 32, 4142);
            //southernSwampScene.Maps[1].Actors[34].ChangeActor(GameObjects.Actor.Snapper, vars: 0x0, modifyOld: true);
            //southernSwampScene.Maps[1].Objects[2] = GameObjects.Actor.Bombiwa.ObjectIndex();

            // if I do come back to this with multiple objects, I should make sure that the object is moved to the end of the list,
            // so other stuff doesnt shuffle around it
        }

        private static void FixSwordSchoolPotRandomization()
        {
            /// we cannot randomize the pots in swordschool because its dungeon keep object pots,
            ///   that means those pots require dungeon keep which we cannot swap out, and actorizer quits early when it cannot find the object for these
            /// however the pots just need a regular pot object, its a small scene with space for one, and the object list has 7 objects
            ///   which means we can expand the list and add another pot object

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.ClayPot)) return;

            var swordSchoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SwordsmansSchool.FileID());
            swordSchoolScene.Maps[0].Objects.Add(GameObjects.Actor.ClayPot.ObjectIndex()); // add clay pot object

            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 8
            var swordSchoolRoom0 = RomData.MMFileList[GameObjects.Scene.SwordsmansSchool.FileID() + 1].Data; // 1434
            swordSchoolRoom0[0x29] = 8; // increase object list to 8
        }

        private static void SplitSceneSnowballIntoTwoActorObjects()
        {
            /// because the large snowballs in road to mountain village count as a logic gate, we dont want them randomized
            /// but not randomizing them means we never randomize the small snowballs, this is lame
            /// so we take the snapper object in the same room and replace it with another large snowball object
            /// actorizer will randomize one and leave the other, allowing us to randomize what we want and leave the snowballs we want

            // if small snowball is randomized
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.SmallSnowball)) return;

            var roadToMountainVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PathToMountainVillage.FileID());

            roadToMountainVillageScene.Maps[0].Objects[3] = GameObjects.Actor.LargeSnowball.ObjectIndex();

            // the other large snowballs that are not part of the roadblock can be randomized,
            // we just need to turn them into small snowballs so rando finds them
            var largeSnowballsToConvert = new List<int> { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 41, 42, 43, 44, 45, 46, 47, 48, };
            foreach (var index in largeSnowballsToConvert)
            {
                var snowball = roadToMountainVillageScene.Maps[0].Actors[index];
                snowball.ChangeActor(GameObjects.Actor.SmallSnowball, vars: 0x7F3F, modifyOld: true);
                snowball.OldName = snowball.Name = "RandomizedLargeSnowball";
            }

            var snowheadScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Snowhead.FileID());
            var snowheadWinter = snowheadScene.Maps[0];
            snowheadWinter.Objects[3] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // unused stalagtite icicle
            // TODO randomize the unused iceicle and clay pot too
            // TODO 25% chance of goro-iwa randomization too
            var snowheadSpring = snowheadScene.Maps[1];
            // again there are multiple unused objects, there is also a treasure chest and clay pot object
            snowheadSpring.Objects[3] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // unused eeno here previously

            var pathToSnowheadScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PathToSnowhead.FileID());
            pathToSnowheadScene.Maps[0].Objects[5] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // (winter largesnowball) previously gaebora
            pathToSnowheadScene.Maps[1].Objects[7] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // (spring smallsnowball) previously gaebora

            /// twin islands has three three objects here, snowball, tektite, and snapper, which dont exist if we remove the snowballs anyway

            var twinislandsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslands.FileID());
            twinislandsScene.Maps[0].Objects[4] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // snapper

            // mountain village
            var mountainVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillage.FileID());
            mountainVillageScene.Maps[0].Objects[8] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // wolfos

            // goron village
            var goronVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronVillage.FileID());
            // this is a test, not sure if we can really do this
            goronVillageScene.Maps[0].Objects[8] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // previously heart piece
            goronVillageScene.Maps[1].Objects[8] = GameObjects.Actor.LargeSnowball.ObjectIndex(); // previously heart piece (bigsmoth room, likey have to match
        }

        private static void RearangeSecretShrineObjects(bool ACTORSENABLED, Random rng)
        {
            /// Secret shrine objects are WILD
            /// every single room has unnecessary objects, I want to change these to make replacement enemies more interesting

            var secretShrineScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SecretShrine.FileID());

            int PopObject(List<Actor> candidates)
            {
                var randomIndex = rng.Next(candidates.Count());
                var newObject = candidates[randomIndex].ObjectId;
                candidates.RemoveAt(randomIndex);
                return newObject;
            }

            var possibleGroundActors = Enemies.ReplacementCandidateList.FindAll(act => act.GetGroundVariants().Count > 0);
            var possibleWaterActors = Enemies.ReplacementCandidateList.FindAll(act => act.GetWaterVariants().Count > 0);
            var possibleWaterBottomActors = Enemies.ReplacementCandidateList.FindAll(act => act.GetWaterBottomVariants().Count > 0);
            var possibleFlyingActors = Enemies.ReplacementCandidateList.FindAll(act => act.GetFlyingVariants().Count > 0);
            var possibleCeilingActors = Enemies.ReplacementCandidateList.FindAll(act => act.GetCeilingVariants().Count > 0);

            // WARNING: lots of actors crash if they exist in two rooms, but their objects have moved in ram position, so we need to juggle the ram too
            // because of this, we're fundamentally changing some positions:
            //   wooden crate and gold torch objects are completely unused, we can use those as always-loaded slots for the objects we need to keep between three rooms
            //   spirit house object likely is likely not required to be loaded for every room
            //   treasure chest could probably be moved around, as I dont see any static data references in the code
            // meanwhile
            //   the water drip and tall grass objects are always shuffled around but they are always loaded
            //   real bombchu, dinofos exist in three rooms
            //   dekubaba exists in main room and a sub-room
            // having moved up two objects from every room, we should count 5 and 6 as new always-loaded slots

            // 5 and 6 objects
            var alwaysGroundObject = PopObject(possibleGroundActors);
            var alwaysFlyingObject = PopObject(possibleFlyingActors);


            foreach (var map in secretShrineScene.Maps)
            {
                map.Objects[3] = GameObjects.Actor.CeilingSpawner.ObjectIndex(); // previously golden torch
                map.Objects[4] = 0xF8; // previously wooden crate becomes tall-grass
                map.Objects[5] = alwaysGroundObject; // slots 5 and 6 are available for re-using every room, just move one actor out of the way and were good
                map.Objects[6] = alwaysFlyingObject;

                // needs testing, but also not required right now?
                //map.Objects[2] = 0xF8; // previously spirit house man
            }

            // lobby
            if (ACTORSENABLED)
            {

                // floating bean plant is only used in this room, move down to old lair grass object
                var lobby = secretShrineScene.Maps[0];
                lobby.Objects[10] = GameObjects.Actor.SoftSoilAndBeans.ObjectIndex(); // previous tall grass slot

                lobby.Objects[7] = PopObject(possibleGroundActors); // previously floating bean slot
                lobby.Objects[8] = PopObject(possibleGroundActors); // real bombchu slot 
                lobby.Objects[11] = PopObject(possibleFlyingActors); // previous deku nut slot
                // was there another spot? I was accidentally blasting soils, need to find the map I made
            }

            // center room
            if (ACTORSENABLED)
            {
                var centerRoom = secretShrineScene.Maps[1];

                centerRoom.Objects[7] = PopObject(possibleWaterActors); // previously water drip slot  
                centerRoom.Objects[8] = PopObject(possibleWaterActors); // real bombchu slot 
                centerRoom.Objects[9] = PopObject(possibleWaterBottomActors); // previous heart piece slot
                centerRoom.Objects[10] = PopObject(possibleCeilingActors); // previous tall grass slot
            }

            // dinofos room
            {
                var dinoRoom = secretShrineScene.Maps[2];

                dinoRoom.Objects[7] = PopObject(possibleGroundActors); // skulltula slot  
                dinoRoom.Objects[9] = PopObject(possibleGroundActors); // water drop slot 
                dinoRoom.Objects[10] = PopObject(possibleFlyingActors); // real bombchu slot                
            }

            // wizrobe room
            {
                var wizrobeRoom = secretShrineScene.Maps[3];

                wizrobeRoom.Objects[8] = PopObject(possibleGroundActors); // water drip slot  
                wizrobeRoom.Objects[9] = PopObject(possibleGroundActors); // lair grass slot 
            }

            // wart room
            if (ACTORSENABLED)
            {
                var wartRoom = secretShrineScene.Maps[4];

                wartRoom.Objects[7] = PopObject(possibleGroundActors); // dinofos slot  
                wartRoom.Objects[9] = PopObject(possibleGroundActors); // water drip slot 
                wartRoom.Objects[10] = PopObject(possibleFlyingActors); // lair grass slot 
            }

            // garo master room
            if (ACTORSENABLED)
            {
                var garoRoom = secretShrineScene.Maps[5];
                garoRoom.Objects[7] = PopObject(possibleGroundActors); // dinofos slot  
                garoRoom.Objects[8] = PopObject(possibleGroundActors); // water drip slot 
                garoRoom.Objects[10] = PopObject(possibleFlyingActors); // lair grass slot 
            }
        }

        private static void SwapIntroActors()
        {
            /// during the pre-file select cutscenes

            SwapIntroSeth();
            SwapIntroBlueKids();
            SwapIntroLinkTheGoroAndAnju();
        }


        private static void SwapIntroSeth()
        {
            /// for actorizer, seth is a very visible part of the intro and we want to randomize
            ///  but we do not want to randomize the actual seth in sct because he hints the rewards for the spiderhouse, which is kinda important

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Seth1)) return;

            var sctScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var introSeth = sctScene.Maps[3].Actors[2];
            introSeth.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
            introSeth.OldName = "IntroSeth";

            // change object
            sctScene.Maps[3].Objects[14] = GameObjects.Actor.DekuBaba.ObjectIndex();
        }

        private static void SwapIntroBlueKids()
        {
            /// for intro cutscene its nice to see weird actors, but blue kids are often required to stick around

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.BombersYouChase)) return;

            var ectScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
            for (int i = 26; i < 26 + 5; i++) // for all bombers kid in ect, lucky they are sequential
            {
                var bomber = ectScene.Maps[1].Actors[i];
                bomber.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
                bomber.OldName = "Bombers(Intro)";
            }

            // change object
            ectScene.Maps[1].Objects[5] = GameObjects.Actor.DekuBaba.ObjectIndex();
        }

        private static void SwapIntroLinkTheGoroAndAnju()
        {
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Anju)) return;

            var stockpotInnScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StockPotInn.FileID());
            var linkTheGoro = stockpotInnScene.Maps[5].Actors[16]; // map 0 setup 1

            linkTheGoro.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
            linkTheGoro.OldName = "LinkTheGoro(Intro)";
            linkTheGoro.Position = new vec16(196, 0, 106); // even in the intro cutscene he spawns behind the door....
            linkTheGoro.ChangeYRotation(270);
            stockpotInnScene.Maps[5].Objects[1] = GameObjects.Actor.DekuBaba.ObjectIndex();
            ActorUtils.SetActorSpawnTimeFlags(linkTheGoro);

            // if we remove him anju doesnt spawn, as it seems this is a cutscene within a cutscene
            var anju = stockpotInnScene.Maps[5].Actors[19];
            anju.ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
            anju.OldName = "Anju(Intro)";
            stockpotInnScene.Maps[5].Objects[0] = GameObjects.Actor.Bombiwa.ObjectIndex();
            ActorUtils.SetActorSpawnTimeFlags(anju);
        }

        private static void SwapPiratesFortressBgBreakwall()
        {
            /// BgBreakwall is an amalgamash actor that can use 10 different objects, its crazy
            /// in pirates fortress center courtyard, its used to make multiple un-breakable crates
            /// because of the multi-object behavior its easier to change the type here to match the crate,
            /// esp since we can't remove the breakwall object its used for doors here

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.LargeWoodenCrate)) return;

            var piratesFortressScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortress.FileID());
            for (int m = 0; m < piratesFortressScene.Maps.Count; m++)
            {
                var map = piratesFortressScene.Maps[m];
                for (int a = 0; a < map.Actors.Count; a++)
                {
                    var actor = map.Actors[a];
                    if (actor.ActorEnum == GameObjects.Actor.Bg_Breakwall)
                    {
                        actor.ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
                        actor.OldName = "BgBreakwall";
                    }
                }
                // every scene setup has double largebox object, which I assume was meant for bgbreakwall, we can change the second one
                map.Objects[8] = GameObjects.Actor.Bombiwa.ObjectIndex();
            }
        }

        private static void ReplaceStonetowerFunenObject()
        {
            // stone tower (exterior) has an unused object: object_funen (0x161)
            // now that actorizer can inject actors, we want some unused object/actor file slots to inject actors into,
            // we need this object to be removed from the two scenes that still load it

            var stonetowerScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StoneTower.FileID());

            stonetowerScene.Maps[0].Objects[2] = GameObjects.Actor.ClayPot.ObjectIndex();

            var stonetowerInvertedScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.InvertedStoneTower.FileID());

            stonetowerInvertedScene.Maps[0].Objects[2] = GameObjects.Actor.ClayPot.ObjectIndex();
        }


        private static void EnableDampeHouseWallMaster()
        {
            /// dampe's house wallmaster is an enounter actor, not a regular wallmaster,
            ///  we have to switch it to regular enemy for enemizer shuffle to find and replace it

            var dampehouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DampesHouse.FileID());
            var wallmaster = dampehouseScene.Maps[0].Actors[0];
            // move to center of the main room,
            wallmaster.Position.z = 0x40;
            // previous encounter actor used rotation as parameters, flatten rotation now for replacement
            ActorUtils.FlattenPitchRoll(wallmaster);
            // change actor to wallmaster proper for enemizer detection
            wallmaster.ChangeActor(newActorType: GameObjects.Actor.WallMaster, vars: 0x1, modifyOld: true);
        }

        private static void ModifyAllGraveyardBatsToFly()
        {
            /// some graveyard bats are wall types, and MMR enemizer still gets confused by multiple types,
            /// so we want to swap all of them to flying type

            // TODO this is stil busted, I sometimes find perching and wall types in the air

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Dampe)) return;

            // single flying bat, visible
            var newVariant = 0x0101;

            var ikanaGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaGraveyard.FileID());

            ikanaGraveyardScene.Maps[0].Actors[22].ChangeVariant(newVariant);
            ikanaGraveyardScene.Maps[0].Actors[23].ChangeVariant(newVariant);
        }

        private static void EnableTwinIslandsSpringSkullfish()
        {
            /// the skullfish in twinislands spring are an encounter actor, not regular skullfish actors
            ///  we have to switch them to regular skullfish for enemizer shuffle to find and replace them
            /// also we move them out of the cave in case its a water surface enemy, and to spread them out
            ///  default they are all stacked on top of the cave chest 

            var twinislandsspringScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
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

        private static void SwitchGBTEncounterForSkullfish()
        {
            /// Skullfish can be summoned by an actor EnEncount1
            /// if this happens, the ones in GBT are pathed, they are supposed to swim into the room through the water passeges

            // if skullfish are randomized; always currently

            var greatbaytempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayTemple.FileID());

            var gearRoom = greatbaytempleScene.Maps[9];

            var fish1 = gearRoom.Actors[3];
            fish1.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            fish1.Position = new vec16(3164, -832, -642);

            var fish2 = gearRoom.Actors[4];
            fish2.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            fish2.Position = new vec16(3175, -912, -919);

            var fish3 = gearRoom.Actors[5];
            fish3.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            fish3.Position = new vec16(2790, -884, -690);
        }

        private static void SwitchZoraCapeEncounterForSkullfish()
        {
            /// Skullfish can be summoned by an actor EnEncount1
            /// this can make the cape seem even more empty if we completely remove the skullfish
            /// should I change it to a regular skullfish for detection, or should I add a skullfish object

            var zoracapeScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraCape.FileID());

            zoracapeScene.Maps[0].Objects.Add((int)GameObjects.Actor.SkullFish.ObjectIndex());

            // expand the object list by 1 size, so the game loads the new object
            var zoraCapeRoomFile = RomData.MMFileList[GameObjects.Scene.ZoraCape.FileID() + 1].Data;
            zoraCapeRoomFile[0x31] = 0x18; // was 0x17
        }

        

        private static void AddExtraObjectToPiratesInterior(bool ACTORSENABLED, Random rng)
        {
            /// With enemizer/actorizer pirates interior is actually kinda dry and boring
            /// the scene has 11 objects, we can add another object to the scene to give actorizer some more free-object actors it can place
            /// also the scene has an unused object (that doesn't get used in enemizer now) we can swap out for something random

            List<GameObjects.Actor> listOfReplacementCandidates = new List<GameObjects.Actor> {
                    GameObjects.Actor.PatrollingPirate,  GameObjects.Actor.LargeWoodenCrate,
                    GameObjects.Actor.Bombiwa,
                    //GameObjects.Actor.Bg_Breakwall, // we change all of the breakwalls into Bombiwa above, object locked because needed for doors
            };
            List<GameObjects.Actor> listOfShuffledGroundActors = new List<GameObjects.Actor>();
            foreach (var candidate in listOfReplacementCandidates)
            {
                if (Enemies.VanillaEnemyList.Contains(candidate))
                    listOfShuffledGroundActors.Add(candidate);
            }

            if (listOfShuffledGroundActors.Count == 0) return; // nothing to change out, leave early

            // because we have two objects, I want one to be default flying or wall
            //   so we have an extra object for hitspots to become something, and I want both of them
            GameObjects.Actor groundActor = GameObjects.Actor.DekuBaba; // both share a variant so I dont have to keep a tuple
            GameObjects.Actor flyingActor = GameObjects.Actor.Keese;
            var piratesFortressInteriorScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.PiratesFortress);

            void RandomlyShufflePirateFortressActors(List<Actor> actorsToRandomlyShuffle)
            {
                for (int i = 0; i < actorsToRandomlyShuffle.Count; i++)
                {
                    var actor = actorsToRandomlyShuffle[i];
                    if (rng.Next(100) < 40)
                    {
                        var oldName = actor.OldName;
                        var newActor = (rng.Next(100) < 35) ? (flyingActor) : (groundActor);

                        actor.ChangeActor(newActor, vars: 0x0000, modifyOld: true);
                        actor.OldName = oldName + "(Changling)";
                    }
                }
            }

            // generate list of candidate slots
            var mainRoomMap = piratesFortressInteriorScene.Maps[0];
            var mainRoomActorsToShuffle = mainRoomMap.Actors.FindAll(act => listOfShuffledGroundActors.Contains(act.ActorEnum));
            RandomlyShufflePirateFortressActors(mainRoomActorsToShuffle);

            // have to update the scene data to load a larger object list in the game
            var pirateRoomData = RomData.MMFileList[GameObjects.Scene.PiratesFortress.FileID() + 1].Data;
            pirateRoomData[0x31] = 12;

            mainRoomMap.Objects.Add(groundActor.ObjectIndex());
            mainRoomMap.Objects[3] = flyingActor.ObjectIndex(); // kaizoku, the pirate captain, unused out here
            // todo we can probably do the heart object too

            if (ACTORSENABLED)
            {
                // because people care about seeing funny actors in intro and credits,
                //   I should randomize the actors and objects in the other scenes too

                var creditsRoomMap = piratesFortressInteriorScene.Maps[1];
                creditsRoomMap.Objects.Add(groundActor.ObjectIndex());
                creditsRoomMap.Objects[9] = flyingActor.ObjectIndex(); // heart piece, if its there at all rando doesnt use it
                pirateRoomData[0x449] = 12; // I don't know which object list is which, but it doesnt matter we increase all of them

                var creditsRoomActorsToShuffle = creditsRoomMap.Actors.FindAll(act => listOfShuffledGroundActors.Contains(act.ActorEnum));
                RandomlyShufflePirateFortressActors(creditsRoomActorsToShuffle);

                // the moon isnt there in the intro lol
                var introRoomMap = piratesFortressInteriorScene.Maps[2];
                introRoomMap.Objects.Add(groundActor.ObjectIndex());
                introRoomMap.Objects[9] = flyingActor.ObjectIndex(); // heart piece, if its there at all rando doesnt use it
                pirateRoomData[0x659] = 12;

                var introRoomActorsToShuffle = introRoomMap.Actors.FindAll(act => listOfShuffledGroundActors.Contains(act.ActorEnum));
                RandomlyShufflePirateFortressActors(introRoomActorsToShuffle);
            }
        }

        private static void SwapCreditsCremia()
        {
            /// cremia in the credits is in the ranch, and the ranch cremia randomization is tied to actual checks
            /// we want to swap the cremia actor in the credits for variety, we have to change the actor and object to not confuse actorizer with the regular cremias

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Cremia)) return;

            var ranchScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RomaniRanch.FileID());
            var creditsCremia = ranchScene.Maps[2].Actors[11];
            creditsCremia.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
            creditsCremia.OldName = "CreditsCremia";

            // and change the object in just that map to match
            ranchScene.Maps[2].Objects[5] = GameObjects.Actor.DekuBaba.ObjectIndex();
        }

        public static void ExpandGoronShineObjects()
        {
            /// we cannot randomize any goron in the shrine because they all use the same object
            ///   and for some reason it crashes if there isnt one there at all, unknown reason
            /// except both rooms use the same 5 objects, and object list is padded to word length
            ///   so there is a space object space in the list we can use, we can add a second goron object which we leave alone
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.GoronSGoro)) return;

            var goronShrine = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronShrine.FileID());
            goronShrine.Maps[0].Objects = new List<int> {
                GameObjects.Actor.GoronSGoro.ObjectIndex(),
                GameObjects.Actor.GoronKid.ObjectIndex(),
                GameObjects.Actor.FishingGameTorch.ObjectIndex(),
                GameObjects.Actor.GoronShrineChandelier.ObjectIndex(),
                GameObjects.Actor.ClayPot.ObjectIndex(),
                GameObjects.Actor.GoGoron.ObjectIndex() // add a second Generic Goron
            };
            goronShrine.Maps[1].Objects = goronShrine.Maps[0].Objects.ToList(); // think this needs a copy or its a pointer to the same list

            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 6
            var goronShrineRoom0Data = RomData.MMFileList[GameObjects.Scene.GoronShrine.FileID() + 1].Data; // 1320
            var goronShrineRoom1Data = RomData.MMFileList[GameObjects.Scene.GoronShrine.FileID() + 2].Data;
            goronShrineRoom0Data[0x31] = 6;
            goronShrineRoom1Data[0x31] = 6;
        }


        public static void ExpandGoronRaceObjects()
        {
            /// we cannot randomize any goron in the racetrack because they all use the same object
            ///   this breaks the race because the racegorons cannot load their assets if their object is missing
            /// except the one room uses 7 objects, odd number, and objects are padded in the room files to dma, so we can add one more
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.GoGoron)) return;

            var goronRace = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronRacetrack.FileID());
            goronRace.Maps[0].Objects.Add(GameObjects.Actor.GoGoron.ObjectIndex()); // add a second Generic Goron
            // spring is a different setup, both need the same objects
            goronRace.Maps[1].Objects.Add(GameObjects.Actor.GoGoron.ObjectIndex()); // add a second Generic Goron


            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 6
            var goronRaceRoom0Data = RomData.MMFileList[GameObjects.Scene.GoronRacetrack.FileID() + 1].Data; // 1508
            goronRaceRoom0Data[0x31] = 8; // increase object list to 8
            // the second setup in this scene has a different object list, need to modify that onne too (690 is headers)
            goronRaceRoom0Data[0x6B9] = 8; // increase object list to 8
        }

        public static void FixWoodfallTempleGekkoMiniboss()
        {
            /// we cannot randomize the snapper in woodfall temple without breaking the gekko miniboss
            /// beacuse he spawns a special snapper in this fight and he will de-spawn if he detects the object is missing
            /// add a second snapper object to the room so there is still one there

            var woodfallScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WoodfallTemple.FileID());
            var gekkoRoom = woodfallScene.Maps[8];
            // we cannot remove the woodflower object used by the giant flower, it breaks the door, so probably used by the door for textures
            gekkoRoom.Objects[2] = 0x1A6; // previously: boss blue warp, now snapper

            // since we're changing objects and that will reload the whole list both ways anyway,
            //   might as well shrink it to reduce chances of overflow
            gekkoRoom.Objects[14] = (int) ObjectEnum.SmallestObj; // previously: bo
            gekkoRoom.Objects[15] = (int) ObjectEnum.SmallestObj; // previously: dragonfly
            gekkoRoom.Objects[16] = (int) ObjectEnum.SmallestObj; // previously: skulltula
        }


        public static void SplitSpiderGrottoSkulltulaObject()
        {
            /// in the spider grotto, we have a skullwalltula on the web and a skulltula hanging from the ceiling
            /// this scene room has 3 objects, one is dekubaba, wasted
            /// in order to split the actor, however, I have to change the actor to something else and give it a different object

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Skulltula)) return;

            var grottoScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            var spiderRoom = grottoScene.Maps[1];

            spiderRoom.Objects[2] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderRoom.Actors[1].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 0, modifyOld: true);

            // lens cave too
            var lensGrottoRoom = grottoScene.Maps[5];
            lensGrottoRoom.Objects[2] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            lensGrottoRoom.Actors[3].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 0, modifyOld: true);
        }

        public static void SplitPirateSewerMines()
        {
            /// The mines in the pirate fort sewer are dual type, in room 10/11 they are underwater mines,
            /// in room 9 there are ceiling hanging mines
            /// right now, actorizer cannot handle them properly in this form (we get water types in the air or air types in the water)
            /// we need to split into two separate actors and two separate objects
            /// turning the ceiling mines into fake skulltula (ceiling type) and changing the object in that room to match

            var sewerScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortressRooms.FileID());
            var actors = sewerScene.Maps[9].Actors;

            foreach (var actor in actors)
            {
                if (actor.ActorEnum == GameObjects.Actor.SpikedMine)
                {
                    actor.ChangeActor(GameObjects.Actor.SkulltulaDummy, 0, modifyOld: true);
                    actor.OldName = actor.Name = "HangingMine";
                }
            }

            sewerScene.Maps[9].Objects[5] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
        }

        private static void SwapSwampSpiderhouseRock()
        {
            // the swamp spiderhouse is the only place where we find the regular rock object with regular rocks to be randomized, these are used for bugs normally
            var swampSpiderhouseScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.SwampSpiderHouse);

            // if bugs arent required for anything in here, lets randomize the rocks
            if (!(JunkDetection.IsActorizerCheckJunk(GameObjects.Item.CollectibleSwampSpiderToken9) && JunkDetection.IsActorizerCheckJunk(GameObjects.Item.CollectibleSwampSpiderToken11)
                   && JunkDetection.IsActorizerCheckJunk(GameObjects.Item.CollectibleSwampSpiderToken12))
               )
            {
                return; // bugs are not junk, dont randomize
            }

            void ChangeRockToReplacement(int map, int actorId)
            {
                swampSpiderhouseScene.Maps[map].Actors[actorId].ChangeActor(GameObjects.Actor.Nejiron, 0, modifyOld: true);
                swampSpiderhouseScene.Maps[map].Actors[actorId].OldName = "BugRock";

            }
            ChangeRockToReplacement(0, 3); // entrance two rocks
            ChangeRockToReplacement(0, 4);
            ChangeRockToReplacement(4, 5); // pot room upper terrace

            foreach (var m in swampSpiderhouseScene.Maps)
            {
                var index = m.Objects.FindIndex(obj => obj == 0x1F6); // object_ishi
                m.Objects[index] = GameObjects.Actor.Nejiron.ObjectIndex();
            }
        }

        private static void EnableSethSwampSpiderhouse()
        {
            /// seth from the spiderhouse that gives you the face mask is a different seth with a different object,
            /// we randomize his spider form but thats a different actor
            /// if hes not randomized, we want to randomize the og too though, as at least the object is worthless

            if (!JunkDetection.IsActorizerCheckJunk(GameObjects.Item.MaskTruth)) return;

            var spiderhouse = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.SwampSpiderHouse);

            var oldseth = spiderhouse.Maps[0].Actors[0];
            oldseth.ChangeActor(GameObjects.Actor.BeanSeller, 0x0, modifyOld: true);
            oldseth.OldName = oldseth.Name = "SethHisEyeWideOpen";

            foreach (var map in spiderhouse.Maps)
            {
                // replacing OOT bearded man, which is acually used for seth in this case
                map.Objects[15] = GameObjects.Actor.BeanSeller.ObjectIndex();
            }
        }

        public static void RepositionClockTownActors()
        {
            // if actors are rando'd then the carpenters probably are too, remove their sounds
            var southClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var carpenterSound = southClockTownScene.Maps[0].Actors[49];
            carpenterSound.ChangeActor(GameObjects.Actor.Carpenter, vars: 1, modifyOld: true); // non-pathing type

            // move to standing in front of the sign
            carpenterSound.Position.x = -423;
            carpenterSound.Position.z = -174; // move forward to muto placement
            // rotation toward the sign
            carpenterSound.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: carpenterSound.Rotation.y);
            // set time flags so that only shows on night 1 and day 4 (rotation was already x:0,z:0)
            carpenterSound.Rotation.x = 0x6; // all day 0
            carpenterSound.Rotation.z = 0x3 | 0x4 | 0x40; // all day 4, night 3, night 1

            // we can also hear the noises in west/east, those actors should also be removed
            var eastClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
            var carpenterSound2 = eastClockTownScene.Maps[0].Actors[63];

            // change into a cremia actor, his object is here wasted and unused, we could rando it
            carpenterSound2.ChangeActor(GameObjects.Actor.Cremia, vars: 0, modifyOld: true);
            carpenterSound2.Position = new vec16(1329, 102, -429);
            carpenterSound2.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: carpenterSound2.Rotation.y);
            // set time flags so that only shows on night 1 and day 4 (rotation was already x:0,z:0)
            carpenterSound2.Rotation.x = 0x6; // all day 0
            carpenterSound2.Rotation.z = 0x3 | 0x10; // all day 4, night 2

            // however, while the cremia object and actor exist in setup 3, they do not in setup 1
            // thankfully there is a free space in the object list because odd count, one free space because of padding
            eastClockTownScene.Maps[0].Objects.Add(GameObjects.Actor.Cremia.ObjectIndex());
            var ECTData = RomData.MMFileList[eastClockTownScene.File + 1];
            ECTData.Data[0x31] = 0x1A; // increase objectlist number, how many it loads, by one

            // should we rando the tower?

            // anju's actor spawns behind the inn door, move her to be visible in sct
            var anju = eastClockTownScene.Maps[0].Actors[0];
            anju.Position = new vec16(153, 3, 246);
            anju.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: anju.Rotation.y); // rotate to away from us

            // move next to mayors building
            // TODO bug this is not next to mayor building for some reason, next to inn
            var gorman = eastClockTownScene.Maps[0].Actors[4];
            gorman.Position = new vec16(1026, 200, -1947);
        }
        private static void SplitSnowheadTempleBo()
        {
            /// the bo in SHT are in two locations: floor in the entrance and hanging from the ceiling,
            /// this is an issue because there are almost no candidates that are dual type
            /// split the two into different enemies for better type control

            var shtScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SnowheadTemple.FileID());
            var boActors = shtScene.Maps[8].Actors; // thankfully they are easilly split per room

            foreach (var actor in boActors)
            {
                if (actor.ActorEnum == GameObjects.Actor.Bo)
                {
                    actor.ChangeActor(GameObjects.Actor.SkulltulaDummy, 0, modifyOld: true);
                    actor.OldName = actor.Name = "CeilingBo";
                }
            }

            // we need to change the object to match skulltula in our code so rando knows to change the object
            shtScene.Maps[8].Objects[15] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();

        }

        private static void MoveCreditsPostmanPath()
        {
            /// credits postman ignores his own path and does his own thing
            /// our replacement actor will use the path that exists, but its way over in leever land where we never see it    

            // postman is actually walking through the credits
            //var terminafFieldCreditsRoom = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID()).Maps[9];
            var terminaFiledCreditsRoomData = RomData.MMFileList[GameObjects.Scene.TerminaField.FileID()].Data;

            List<(short x, short y, short z)> path = new List<(short x, short y, short z)>{
                (2866,  52, -252),
                (2558,  50,  308),
                (2387,  14,  622),
                (2153, -34, 1024),
            };

            // according to decomp, this path is in the scene file, where it's defined by the layer not room, at location 0x01B0F4
            var pathOffset = 0x1B0F4;
            // path point 1
            for (int i = 0; i < path.Count; i++)
            {
                var pathPointLoc = pathOffset + 6 * (i);
                var pathPoint = path[i];
                ReadWriteUtils.Arr_WriteU16(terminaFiledCreditsRoomData, pathPointLoc + 0, (ushort)pathPoint.x);
                ReadWriteUtils.Arr_WriteU16(terminaFiledCreditsRoomData, pathPointLoc + 4, (ushort)pathPoint.y);
                ReadWriteUtils.Arr_WriteU16(terminaFiledCreditsRoomData, pathPointLoc + 8, (ushort)pathPoint.z);
            }

            // TODO move it closer to the camera if the actor has bad culling

            // when we randomized HMS things got complicated for the camera because there replacement doesnt move and doesnt respond to the cutscene
            // I found an unused actor I think
            var terminaFieldScene = RomData.SceneList.Find(s => s.SceneEnum == GameObjects.Scene.TerminaField);
            var newHMS = terminaFieldScene.Maps[7].Actors[13];
            newHMS.ChangeActor(GameObjects.Actor.HappyMaskSalesman, vars: 3, modifyOld: true);
            newHMS.OldName = "HappyMaskSalesmanClone";
            newHMS.Position = new vec16(643, -165, 2855); // moved to where HMS stands when talking about saying farewell
            newHMS.ChangeYRotation(45);
        }

        private static void AddGrottoVariety(Random rng)
        {
            /// turns out the grottos have unused objects, some of them can be swapped
            ///   without affecting the original enemy placement, and gives us some variety

            SplitSpiderGrottoSkulltulaObject();
            ChangeHotwaterGrottoDekuBabaIntoSomethingElse(rng);
            RandomizeGrottoGossipStonesPerGrotto();


            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());

            // dodongo grotto has a useless blue icicle object. switch to Bo object so we can get Bo actors from jp grotto
            var straightDekubabaReplacement = GameObjects.Actor.MadShrub;
            var dodongoGrottoObjectList = grottosScene.Maps[7].Objects;
            dodongoGrottoObjectList[2] = straightDekubabaReplacement.ObjectIndex();
            var randomActorChangeRoll = rng.Next(100);
            if (randomActorChangeRoll < 35)
            {
                var randomIndex = randomActorChangeRoll < 12 ? (0) : (1); // TODO fix
                var secondDodongo = grottosScene.Maps[7].Actors[randomIndex];
                secondDodongo.ChangeActor(straightDekubabaReplacement, vars: 0xFF02, modifyOld: true); // match actor to object
                secondDodongo.OldName = secondDodongo.Name = "Dodongo 2";
            }

            // TODO do the same for peahat grotto

            // peahat grotto has a deku baba object, switch to BO so we can get bo actors from jp grotto
            var peahatGrottoObjecList = grottosScene.Maps[13].Objects;
            peahatGrottoObjecList[2] = GameObjects.Actor.Bo.ObjectIndex();
            // there is a worthless mushroom here, lets make TWO peahats :]
            var newPeahat = grottosScene.Maps[13].Actors[3];
            newPeahat.ChangeActor(GameObjects.Actor.Peahat, vars: 0, modifyOld: true);
            //newPeahat.Position = new vec16(5010, -20, 600); // move over near peahat one
            newPeahat.Position = new vec16(5010, -20, 600); // move over near peahat one

            // biobaba grotto has a worthless dekubaba object, lets swap it for the ice block object so we can freeze the water
            grottosScene.Maps[11].Objects[3] = 0x1E7; // iceflowe
        }

        private static List<ushort> sickEntrances = new List<ushort>() {
            0x0010, // infont of mayor
            0x0C60, // clear swamp
            0x22A0, 0x22B0, // pirates fortress
            0x2C10, // clock tower roof
            0x3400, 0x3440, // ikana castle
            0x3C00, 0x3000, 0x2600, 0x2A00,  // dungeons
            0x9000, // WELL
            0x5050, // deku palace (the boring years)
            0x54B0, // termina field
            0xAE50, // (spring) mountain village
            0xBC40, // stockpot inn
            0xBE00, // gbt?
            0xC050, // clock tower interior
            0xC410, // lost woods
            0xC800, // clock tower
            0xD2A0, // east clock town
        };


        private static void FixJPGrottos(Random rng, StringBuilder log)
        {
            /// JP grottos are unused, but we can summon them for actorizer
            /// however, they have unique exits in the grotto scene exit table that always return to deku palace
            ///   we can change the table to make some of the exits generic exists

            // exit table starts at 234, 0:0xFFFF, 1:lens grotto
            // vanilla vines grotto is   50A0 <- (lower:1480) <=> (upper: 14F0) -> 5060
            // vanilla straight grotto is   5080 <- (brighter"A":1460) <=> (darker"B": 14E0) -> 5070

            var grottoSceneData = RomData.MMFileList[GameObjects.Scene.Grottos.FileID()].Data;
            ReadWriteUtils.Arr_WriteU16(grottoSceneData, 0x23C, 0xFFFF); // replace vines lower with generic exit

            // straight grotto: I want the player to enter from A side because its brighter and looks better
            // but B exit is boring compared to A exit, so I want to swap the B exit to exit to old A exit
            ReadWriteUtils.Arr_WriteU16(grottoSceneData, 0x238, 0xFFFF); // replace straight A with generic exit
            ReadWriteUtils.Arr_WriteU16(grottoSceneData, 0x23A, 0x5080); // replace straight B with old straight A exit

            // lets change one of the JP entrances at random to some other place
            var randomGrottoExitAddress = (rng.Next(2) == 1) ? (0x23A) : (0x23E); // the two exits in the grotto scene exit list
            var randomSickEntrance = sickEntrances[rng.Next(sickEntrances.Count())];
            log.AppendLine($"randomized jp_grotto exit address: [{randomSickEntrance.ToString("X4")}]");

            ReadWriteUtils.Arr_WriteU16(grottoSceneData, randomGrottoExitAddress, randomSickEntrance);

            var grottosScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.Grottos);

            // straight jp grotto has only one object, padding of scene data means there is space for an object right behind it that we can use
            //  we can use the second object to give this area a chest by taking one of the useless mushrooms and changing it
            // expand object list to have both of our new objects, change dekubaba to dodongo to increase likelyhood of killable
            var straightDekubabaReplacement = GameObjects.Actor.MadShrub;
            grottosScene.Maps[6].Objects = new List<int> { straightDekubabaReplacement.ObjectIndex(),
                                                           GameObjects.Actor.TreasureChest.ObjectIndex() };
            // we have to tell the room to load the extra object though
            var straightJPGrottoRoomFile = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 7];
            straightJPGrottoRoomFile.Data[0x29] = 0x2; // setting object header object count from 1 to 2
            // change dekubaba to madscrub so its killable to get the new chest
            var straightJGrottoEnemy = grottosScene.Maps[6].Actors[2];
            straightJGrottoEnemy.ChangeActor(straightDekubabaReplacement, vars: 0xFF02, modifyOld: true);
            straightJGrottoEnemy.OldName = straightJGrottoEnemy.Name = "JpGrottoEnemy";

            var newChestActor = grottosScene.Maps[6].Actors[7];
            // chest params: should be invisible until you kill the enemy, should not collide with any other chest flags in the scene, item: dont know
            // flag 1D, type 7, item 6D (unknown)
            newChestActor.ChangeActor(GameObjects.Actor.TreasureChest, 0x26ED, modifyOld: true);
            newChestActor.Position = new vec16(-230, 0, 1130); // move into the grass area
            newChestActor.Rotation.y = ActorUtils.MergeRotationAndFlags(90, grottosScene.Maps[6].Actors[7].Rotation.y); // rotate to face the center
            // turn the other useless mushroom into another buterfly for ambiance
            grottosScene.Maps[6].Actors[8].ChangeActor(GameObjects.Actor.Butterfly, 0x5324, modifyOld: true);
            grottosScene.Maps[6].Actors[8].Position.y = 58; // dont want spawning in the ground, we want flying around

            // the bo that fall in the JP grotto are ceiling location, but regular bo types,
            // so I need to change them but I dont think there are free params to use
            // just change to a ceiling only actor, then change the objects

            foreach (var bo in grottosScene.Maps[8].Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.Bo))
            {
                bo.OldVariant = bo.Variants[0] = 0xFA00;
            }

            // Spider gosip grotto: the spider object is needed for the small spider in the web and the skulltula in the air
            // the object list for this scene is 3, so there is space for another object without expanding the scene
            // lets re-use the other ceiling actor object in the spider grotto so we can have some more variety
            var jGrottoRoomData = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 8].Data;
            grottosScene.Maps[8].Objects.Add(GameObjects.Actor.SkulltulaDummy.ObjectIndex());
            jGrottoRoomData[0x29] = 4; // expand the list size officially
        }

        private static void Shinanigans(bool ACTORSENABLED, Random rng, StringBuilder log)
        {
            // the peahat grass drops NOTHING, this has bothered me for ages, here I change it
            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            grottosScene.Maps[13].Actors[1].ChangeActor(GameObjects.Actor.NaturalPatchOfGrass, vars: 0x1, modifyOld: true);

            if (ACTORSENABLED)
            {
                //turn around this torch, because if its bean man hes facing into the wall and it hurts me
                var laundryPoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LaundryPool.FileID());
                var laundryPoolTorch = laundryPoolScene.Maps[0].Actors[2];
                laundryPoolTorch.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 135, flags: 0x7F);
                ActorUtils.SetActorSpawnTimeFlags(laundryPoolTorch);
                //laundryPoolScene.Maps[0].Actors[1].Rotation.z = ActorUtils.MergeRotationAndFlags(rotation: laundryPoolScene.Maps[0].Actors[1].Rotation.z, flags: 0x7F);

                // it was two torches, turn the other into a secret grotto, at least for now
                var randomGrotto = new List<ushort> { 0x6033, 0x603B, 0x6018, 0x605C, 0x8000, 0xA000, 0x7000, 0xC000, 0xE000, 0xF000, 0xD000 };
                var hiddenGrottos = new List<ushort> { 0x6233, 0x623B, 0x6218, 0x625C, 0x8200, 0xA200, 0x7200, 0xC200, 0xE200, 0xF200, 0xD200 };
                laundryPoolScene.Maps[0].Actors[1].ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[rng.Next(randomGrotto.Count)], modifyOld: true);
                laundryPoolScene.Maps[0].Actors[1].Rotation = new vec16(0x7F, 0x7F, 0x7F);
                laundryPoolScene.Maps[0].Actors[1].Position = new vec16(-1502, 35, 555); // old: new vec16(-1872, -120, 229);

                // winter village has a gossip stone actor, but no object, lets use the non-used flying darmani ghost object and add it to enemizer
                var winterVillage = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillage.FileID());
                winterVillage.Maps[0].Objects[5] = GameObjects.Actor.GossipStone.ObjectIndex();
                winterVillage.Maps[0].Actors[57].Variants[0] = 0x67; // the vars is for milkroad, change to a moon vars so it gets randomized
                winterVillage.Maps[0].Actors[57].Position.y = -15; // floating a bit in the air, lower to ground
                // note: if we need to add the ghost back in, the scene is using 13 objects so we can add one more back in

                // now that darmani ghost is gone, lets reuse the actor for secret grotto
                var newGrotto = winterVillage.Maps[0].Actors[2];
                newGrotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[rng.Next(randomGrotto.Count)] & 0xFCFF, modifyOld: true);
                newGrotto.Position = new vec16(504, 365, 800);

                var terminafieldScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
                var elf6grotto = terminafieldScene.Maps[0].Actors[2];
                elf6grotto.Position = new vec16(-5539, -275, -701);
                elf6grotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: hiddenGrottos[rng.Next(hiddenGrottos.Count)], modifyOld: true);

                Scene dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
                ActorUtils.SetActorSpawnTimeFlags(dekuPalaceScene.Maps[2].Actors[25]); // set other torch to always spawn, you wont notice the night one missing
                var freeTorch = dekuPalaceScene.Maps[2].Actors[26];
                freeTorch.ChangeActor(GameObjects.Actor.GrottoHole, vars: hiddenGrottos[rng.Next(hiddenGrottos.Count)], modifyOld: true);
                ActorUtils.SetActorSpawnTimeFlags(freeTorch);
                freeTorch.Position = new vec16(24, -12, 675);

                var newJpGrotto = dekuPalaceScene.Maps[0].Actors[9];
                newJpGrotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: 0x8000, modifyOld: true);
                ActorUtils.SetActorSpawnTimeFlags(newJpGrotto);
                newJpGrotto.Position = new vec16(1873, 1, 711);

                var randomizedEntrances = sickEntrances.ToList();
                var doorAnaData = RomData.MMFileList[GameObjects.Actor.GrottoHole.FileListIndex()].Data;
                var firstPullLocation = rng.Next(randomizedEntrances.Count);
                var entrance1 = randomizedEntrances[firstPullLocation];
                randomizedEntrances.RemoveAt(firstPullLocation);
                var entrance2 = randomizedEntrances[rng.Next(randomizedEntrances.Count)];
                ReadWriteUtils.Arr_WriteU16(doorAnaData, 0x60A, entrance1); // E
                ReadWriteUtils.Arr_WriteU16(doorAnaData, 0x60C, entrance2); // F
                log.AppendLine($"grotto list added address 1: [{entrance1.ToString("X4")}]");
                log.AppendLine($"grotto list added address 2: [{entrance2.ToString("X4")}]");

                if (rng.Next() % 10 >= 5)
                {
                    // I like secrets
                    var twinislandsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslands.FileID());
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
                }

                // in actorizer 77, the bugs at the back of doggy race are broken, they get skipped by rando and I dont know why
                // doggy race has MULTIPLE unused objects wtf// TODO figure out what else we can do with them
                var doggyRaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DoggyRacetrack.FileID());
                //doggyRaceScene.Maps[0].Objects[4] for now we dont need to change the tree object, just change the bugs to tree
                var troublesomeBugs = doggyRaceScene.Maps[0].Actors[3];
                troublesomeBugs.ChangeActor(GameObjects.Actor.Treee, vars: 0xF, modifyOld: true);
                troublesomeBugs.ChangeYRotation(90 + 45);
                troublesomeBugs.OldName = "BugsInTheBack";
                // while Im here, might as well move the second soil actor (bean) to the side a bit
                var secondBean = doggyRaceScene.Maps[0].Actors[7];
                if (secondBean.ActorEnum != secondBean.OldActorEnum)
                {
                    secondBean.Position = new vec16(-4328, 146, 1664);
                }


                // */
                // RecreateFishing();

                // can we just boost the dynapoly memory size?
                // data locations:
                // default 23000 is an ORI at 3da8, a4 for tope byte
                // IsSmallMemScene is F000 at 3d58
                // termina field is in data at sSceneMemList, not sure exact space
                //ReadWriteUtils.Arr_WriteU32(codeFile, 0x3DA8, 0x2);
                /*
                List<Actor> sorted = new List<Actor>();
                foreach (var actor in Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>())
                {
                    sorted.Add(new Actor(actor));
                }
                foreach ( var a in sorted.OrderBy(u => u.ObjectSize))
                {
                    Debug.WriteLine($"Actor {a.Name} has object size: {a.ObjectSize.ToString("X6")}");
                }
                int i = 4; */

            }

            // attempt faster breman march, testing
            //glabel D_8085E5A0
            // 030B10 8085E5A0 3ECCCCCD  .float 0.4
            RomUtils.CheckCompressed(38);
            var playerCodeFile = RomData.MMFileList[38].Data;
            // 0x40000000, 
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x030B10, val: 0x3FF33333); // change to 1.9, almost double running speed

            // what if all minor hats were as fast as bunny?
            // except without adding code we can only modify one line of code
            //  if (this->currentMask == PLAYER_MASK_BUNNY) {speedTarget *= 1.5f;
            // the closest I can think of is & 0xF which gets most but not all of them, which does shuffle some code around tho
            // 0x1D59C ofset == 0xCC5490 hard romaddr
            /*
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D59C, val: 0xC7A4002C);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A0, val: 0x3C013FC0);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A4, val: 0x3319000F);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A8, val: 0x13200005);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5AC, val: 0x3C08801F);

            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5C0, val: 0x8D08);
            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5CC, val: 0x8509);
            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5D8, val: 0x4489);
            */

            // removing because its suspicious that we are having slime crash in this spot
            /*
            // can we remove an object from ikana to increase object budget to have more stuff?
            var ikanaScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCanyon.FileID());
            // nobody follows kafei its fine to remove his object from the main room
            ikanaScene.Maps[0].Objects[10] = ObjectEnum.SmallestObj; // kafei
            ikanaScene.Maps[0].Objects[13] = ObjectEnum.SmallestObj; // piece of heart, used in the east side but not here, we dont need here
            ikanaScene.Maps[0].Objects[18] = ObjectEnum.SmallestObj; // flying scrub ( dont think it matters remove it from this area for most people)
            // */

            // if we remove the woodfall object from terminafield, we have more space for noticible actors and not a static backdrop woodfall
            // so far this has been here over a month and nobody has noticed I removed woodfall lol
            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            tfScene.Maps[0].Objects[0] = (int) ObjectEnum.SmallestObj;

            // HEAVY BOMB
            // you idiot, this is both kegs and regular bombs, you need to set the flag for just kegs with a code change or bombs are heavy too
            //RomUtils.CheckCompressed(GameObjects.Actor.PowderKeg.FileListIndex());
            //var kegFile = RomData.MMFileList[GameObjects.Actor.PowderKeg.FileListIndex()].Data;
            //kegFile[0x1FF5] |= 0x02; // add ACTOR_FLAG_20000, makes it heavy 

            // regular po cannot be hit by zora lightning, but can take arrow damage? this feelslike an oversight
            RomUtils.CheckCompressed(GameObjects.Actor.Poe.FileListIndex());
            var pohData = RomData.MMFileList[GameObjects.Actor.Poe.FileListIndex()].Data;
            pohData[0x3003] = 0x2;

            // to pointerize milk bar we have to change the obj_sound actor in themilkbar
            //SequenceUtils.ConvertSequenceSlotToPointer(seqSlotIndex: 0x56, substituteSlotIndex:0x1F, "mm-milk-bar-pointer"); // house
            // TODO is this even doing anything anymore? I thought I had to do all of this in music rando code now
            //var milkbarScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
            //milkbarScene.Maps[0].Actors[17].Variants[0] = 0x13C; // from 0x156, the pointer, to 3C the actual milkbar song

            // dinofos deserves a red dot on the minimap
            RomUtils.CheckCompressed(GameObjects.Actor.Dinofos.FileListIndex());
            var dinofosData = RomData.MMFileList[GameObjects.Actor.Dinofos.FileListIndex()].Data;
            dinofosData[0x3A74] |= 0x80; // set the 0x80000000 actor flag to enabled red dot on the minimap

            // bigpo deserves a red dot on the minimap
            RomUtils.CheckCompressed(GameObjects.Actor.BigPoe.FileListIndex());
            var bigpoData = RomData.MMFileList[GameObjects.Actor.BigPoe.FileListIndex()].Data;
            bigpoData[0x3A14] |= 0x80; // set the 0x80000000 actor flag to enabled red dot on the minimap

            //LightShinanigans();


            //PrintActorValues();
        }

        public static void LightShinanigans()
        {
            // tales of light and fox
            var scenesAsFiles = new List<Scene>();

            // grab all scenes
            for (int fileId = 0; fileId < RomData.MMFileList.Count; fileId++)
            {
                var search = RomData.SceneList.Find(scene => scene.File == fileId);
                if (search != null)
                {
                    scenesAsFiles.Add(search);
                }
            }

            // assumption: this is after enemizer so all scenes are decompressed already

            foreach (var scene in scenesAsFiles)
            {
                // for each scene change something 
                // scenes have LightSettings[] which are type EnvLightSettings[]
                // 0x0F  u8 fogColor[3];
                // 0x12 s16 fogNear; // ranges from 0-1000 (0: starts immediately, 1000: no fog), but is clamped to ENV_FOGNEAR_MAX
                // 0x14 s16 zFar; // Max depth (render distance) of the view as a whole. fogFar will always match zFar
                // except unless the scene list is a list per-room, which is odd we have room files, this shouldnt explain the dark room
                // there are two lights in the env light list that have a fog color of zero
                // 1 {  0x00, 0x00, 0x00, 0x45, 0x45, 0x45, 0x00, 0x00, 0x00, 0xBB, 0xBB, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                // 0x07E6, 0x1A90 },

                // 2 {0x00, 0x00, 0x00, 0x45, 0x45, 0x45, 0x00, 0x00, 0x00, 0xBB, 0xBB, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                //0x0FE6, 0x1A90 },


                var sceneFid = scene.File;
                var sceneData = RomData.MMFileList[sceneFid].Data;
                var roomCount = 0;
                // scan scene header for values
                for (int i = 0; i < sceneData.Length; i += 0x8)
                {
                    // TODO abstract function that scans headers for an offset value as I keep making this shit
                    if (sceneData[i] == 0x14) break; // end of headers

                    if (sceneData[i] == 0x4) // room data, get room count
                        roomCount = sceneData[i + 1];

                    if (sceneData[i] == 0xF) // env light settings
                    {
                        // 16 each
                        var envLightCount = sceneData[i + 1];
                        var envLightCountOffset = ReadWriteUtils.Arr_ReadU32(sceneData, i + 4) & 0x00FFFFFF; // segment offset, dont need the 0x02
                        for (int light = 0; light < envLightCount; light++)
                        {
                            var offset = envLightCountOffset + (light * 0x16);
                            // start by changing the fog color and intesity make sure this shit is working
                            // AA 15 00 // and orange redish color that seems like glover
                            // 1 {  0x00, 0x00, 0x00, 0x45,
                            // 0x45, 0x45, 0x00, 0x00,
                            // 0x00, 0xBB, 0xBB, 0xBB,
                            // 0x00, 0x00, 0x00, 0x00,
                            // 0x00, 0x00,
                            // 0x07E6, 0x1A90 },

                            // write the exact same light as darkroom
                            // its dark but tatl alone is not enough to brighten it up, and the far fog is waaaay too low
                            // also need to fix the skybox during day, maybe fix tatl so she stays out near your head
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset, 0x00000045);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 4, 0x45454500);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 8, 0x00BBBBBB);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 0xC, 0x00000000);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 0x10, 0x000007E6);
                            ReadWriteUtils.Arr_WriteU16(sceneData, (int)offset + 0x14, 0x1A90);

                            /*sceneData[offset + 0xF + 0] = 0; 
                            sceneData[offset + 0xF + 1] = 0;
                            sceneData[offset + 0xF + 2] = 0;

                            sceneData[offset + 0x12] = 0x12; // "fognear"
                            sceneData[offset + 0x13] = 0x12;

                            sceneData[offset + 0x14] = 0x66; // "zfar"
                            sceneData[offset + 0x15] = 0x66; */

                        }
                    }
                }

                // for each room in scene change those too
                // there arent any in room 09, there is room behavior but not room light
                // SCENE_CMD_ROOM_BEHAVIOR(curRoomUnk3, curRoomUnk2, curRoomUnk5, msgCtxunk12044, enablePosLights, kankyoContextUnkE2)
                // SCENE_CMD_ROOM_BEHAVIOR(0x01, 0x00, 0, 0, true, 0), // actually used in room 09
                for (int roomNum = 0; roomNum < roomCount; roomNum++)
                {
                    var roomData = RomData.MMFileList[sceneFid + roomNum].Data;
                    for (int i = 0; i < roomData.Length; i += 0x8)
                    {
                        // TODO abstract function that scans headers for an offset value as I keep making this shit
                        if (roomData[i] == 0x14) break; // end of headers

                        if (roomData[i] == 0x8)
                        {
                            // set point lights for all
                            roomData[i + 6] |= 0x8;

                            // set room behavior to match the dark room in woodfall
                            roomData[i + 1] = 0x01; // flag 1
                            roomData[i + 7] = 0x00; // flag 2 & 0xFF
                            break;
                        }
                    }
                }

            }
        }

        /* private static void RecreateFishing()
        {

            /// fishing testing

            // to place in spring, we remove some  other actors and objects to get fishing working, as its huge

            var springTwinIslandsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
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

        public static void ChangeHotwaterGrottoDekuBabaIntoSomethingElse(Random rng)
        {
            /// I want more variety, so I want the hot spring water grotto to have a different actor in it than regular grottos
            // using likelike as a replacement, sometimes rando will put water and sometimes land, and mikau can give us water surface actors

            // we want both ground or water types, so we are going to use multiple actors
            int randomValue = rng.Next(shallowWaterReplacements.Count);
            var coinTossResultActor = shallowWaterReplacements[randomValue];

            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            var hotspringDekuBaba = grottosScene.Maps[14].Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.DekuBabaWithered);
            foreach (var baba in hotspringDekuBaba)
            {
                baba.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                baba.OldName = "HotSpringBaba";
            }

            // from the perspective of the door
            var farEntry = hotspringDekuBaba[0];
            var leftEntry = hotspringDekuBaba[1];
            var rightEntry = hotspringDekuBaba[2];

            // move them into water
            farEntry.Position = new vec16(6936, -22, 824);
            leftEntry.Position = new vec16(6935, -24, 1072);
            rightEntry.Position = new vec16(7160, -24, 916);
            if (farEntry.ActorEnum == GameObjects.Actor.Mikau) // surface type, move up to water top
            {
                farEntry.Position.y = 0;
                leftEntry.Position.y = 0;
                rightEntry.Position.y = 0;
            }

            // baba have no face, so they don't get a rotation normally, they would all face the same direction,
            // turn them to face the center of the pool and each other
            // zero y rotation is facing the door
            farEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: farEntry.Rotation.y);
            leftEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(90 + 60, flags: leftEntry.Rotation.y);
            rightEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(360 - 45 - 30, flags: rightEntry.Rotation.y);

            // change object in the room to match new fake actors
            grottosScene.Maps[14].Objects[2] = (coinTossResultActor.actor).ObjectIndex();
        }

        private static void RandomizeGrottoGossipStonesPerGrotto()
        {
            /// each gossip grotto gossip stone has enough object space to add or switch an object
            /// and then randomize three of the gossip stones to something new and random
            /// should be doable without breaking the gossip stone quest

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.GossipStone)) return;

            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());

            void ChangeStones(Map map, int[] actorSlots, GameObjects.Actor actorType, int actorParam, string name)
            {
                for (int i = 0; i < actorSlots.Length; i++)
                {
                    var actor = map.Actors[actorSlots[i]];
                    actor.ChangeActor(actorType, actorParam, modifyOld: true);
                    actor.OldName = name;
                }
            }

            // west butterfly/comb grotto (middle right stone)
            var westGrotto = grottosScene.Maps[0];
            westGrotto.Objects[3] = GameObjects.Actor.Leever.ObjectIndex(); // unused deku baba object here, we can override
            int[] westStoneSlots = { 9, 10, 12 };
            ChangeStones(westGrotto, westStoneSlots, GameObjects.Actor.Leever, actorParam: 0xFF, "GossipStoneWest");

            // south spider grotto (far left stone)
            var southGrotto = grottosScene.Maps[1];
            southGrotto.Objects.Add(GameObjects.Actor.Armos.ObjectIndex()); // three objects in this scene, because of padding there is a fourth free spot without scene expansion
            var southGrottoRoomFile = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 2].Data; // room file
            southGrottoRoomFile[0x29] = 4; // update object list to load all four objects in-game
            int[] southGrottoStones = { 4, 5, 6 };
            ChangeStones(southGrotto, southGrottoStones, GameObjects.Actor.Armos, actorParam: 0x7F, "GossipStoneSouth");

            // east sandy grotto (far right stone)
            var eastGrotto = grottosScene.Maps[2];
            eastGrotto.Objects[1] = GameObjects.Actor.Wolfos.ObjectIndex(); // unused deku baba slot can be reused
            int[] eastStoneSlots = { 5, 6, 7 };
            ChangeStones(eastGrotto, eastStoneSlots, GameObjects.Actor.Wolfos, actorParam: 0xFF80, "GossipStoneEast");

            // north flooded grotto (middle left stone)
            var northGrotto = grottosScene.Maps[3];
            northGrotto.Objects[1] = GameObjects.Actor.Snapper.ObjectIndex(); // unused deku baba slot can be reused
            int[] northGrottoSlots = { 1, 3, 4 };
            ChangeStones(northGrotto, northGrottoSlots, GameObjects.Actor.Snapper, actorParam: 0, "GossipStoneNorth");

            void ChangeGossipHintType(Actor stone, int newHint)
            {
                stone.Variants[0] &= 0xFFF0; // remove previous bottom (text offset)
                stone.Variants[0] |= newHint;
            }

            /// the hint given by the big gossip stone is always the same hint, we have to change the hint variable
            /// where, the hint offset is +4 from the type 2 (regular hints) to use the same hint IDs with big type
            /// so hints 0, 1, 2 become 4, 5, 6
            //ChangeGossipHintType(southGrotto.Actors[3], 0x2); // already far left, leave alone
            ChangeGossipHintType(northGrotto.Actors[2], 4); // middel left
            ChangeGossipHintType(westGrotto.Actors[11], 5); // middle right
            ChangeGossipHintType(eastGrotto.Actors[8], 6); // far right
        }

        private static List<(GameObjects.Actor actor, ushort vars)> shallowWaterReplacements = new List<(GameObjects.Actor actor, ushort vars)>
        {
            (GameObjects.Actor.LikeLike, 0x2),   // water bottom type
            (GameObjects.Actor.Octarok, 0xFF00), // water surface type
            (GameObjects.Actor.GoGoron, 0x7FC1)  // ground type (race track goron, stretching)
        };


        private static void SwapGreatFairies(RandomizedResult result, Random rng)
        {
            /// actorizer is currently a little silly in that, if an actor/enemy is replaced, we replace the objects in other rooms of the same scene
            ///   which normally prevents us randomizing only one fairy since all fairy fountains are in the same scene they would all get dinged
            /// in order to randomize just one great fairy we need to do it piecemeal


            if (result.Settings.VictoryMode.HasFlag(Models.Settings.VictoryMode.Fairies)) return; // they are needed for hints if you need all fairies

            var greatfairyFountainScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.FairyFountain.FileID());

            void ChangeGreatFairyActors(int mapIndex, int objectIndex, int actorIndex1, int actorIndex2, int actorIndex3, string fairyName,
                vec16 pos1, vec16 pos2, vec16 pos3)
            {
                // shallow bath water means we have options for what to replace with, pick one
                int randomValue = rng.Next(shallowWaterReplacements.Count);
                var coinTossResultActor = shallowWaterReplacements[randomValue];

                var map = greatfairyFountainScene.Maps[mapIndex];
                var dyYosei = map.Actors[actorIndex1]; // placed to the left
                dyYosei.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                dyYosei.OldName = fairyName;
                dyYosei.Position = pos1;
                dyYosei.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: dyYosei.Rotation.y); // turn to face right

                var elfgroup = map.Actors[actorIndex2]; // placed to the right
                elfgroup.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                elfgroup.OldName = fairyName + "Cloud";
                elfgroup.Position = pos2;
                elfgroup.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: dyYosei.Rotation.y); // turn to face left
                ActorUtils.FlattenPitchRoll(elfgroup);

                if (actorIndex3 != -1) // there isnt always a talk spot to randomize, only in ikana and town
                {
                    var talkalot = map.Actors[actorIndex3]; // placed in the back facing forward
                    talkalot.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                    talkalot.OldName = fairyName + "TalkSpot";
                    talkalot.Position = pos3;
                    ActorUtils.FlattenPitchRoll(talkalot);
                }

                map.Objects[objectIndex] = coinTossResultActor.actor.ObjectIndex();
            }

            if (JunkDetection.IsActorizerCheckJunk(GameObjects.Item.MaskGreatFairy) && JunkDetection.IsActorizerCheckJunk(GameObjects.Item.FairyMagic))
            {
                ChangeGreatFairyActors(mapIndex: 0, objectIndex: 0,
                            actorIndex1: 1, 2, 4,
                            "TownFairy",
                            pos1: new vec16(2289, -30, -750), new vec16(2523, -30, -750), new vec16(2412, -30, -929));
            }
            if (JunkDetection.IsActorizerCheckJunk(GameObjects.Item.FairySpinAttack))
            {
                ChangeGreatFairyActors(mapIndex: 1, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "WoodfallFairy",
                            pos1: new vec16(1095, -30, -750), new vec16(1294, -30, -750), new vec16(2412, -30, -929));
            }
            if (JunkDetection.IsActorizerCheckJunk(GameObjects.Item.FairyDoubleMagic))
            {
                ChangeGreatFairyActors(mapIndex: 2, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "SnowheadFairy",
                            pos1: new vec16(-102, -30, -750), new vec16(93, -30, -750), new vec16(2412, -30, -929));
            }
            if (JunkDetection.IsActorizerCheckJunk(GameObjects.Item.FairyDoubleDefense))
            {
                ChangeGreatFairyActors(mapIndex: 3, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "GreatbayFairy",
                            pos1: new vec16(-1299, -30, -750), new vec16(-1098, -30, -750), new vec16(2412, -30, -929));
            }
            if (JunkDetection.IsActorizerJunk(GameObjects.Item.ItemFairySword))
            {
                ChangeGreatFairyActors(mapIndex: 4, objectIndex: 0,
                            actorIndex1: 0, 1, 3,
                            "IkanaFairy",
                            pos1: new vec16(-2481, -30, -750), new vec16(-2319, -30, -750), new vec16(-2407, -30, -872));
            }

        }

        private static void RandomizePinnacleRockSigns()
        {
            /// these signs use gameplay_keep, so there is no Object to associate with them
            /// HOWEVER, there is a bombiwa object in the object list that doesnt seem to do anything, we can randomize it


            var listOfSignIds = new List<int> { 14, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43 };

            var pinnacleSceneActors = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PinnacleRock.FileID()).Maps[0].Actors;

            foreach (var aId in listOfSignIds)
            {
                pinnacleSceneActors[aId].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x77, true);
                pinnacleSceneActors[aId].OldName = "WaypointSign"; // so the log doesnt say they are bombiwa, rename here
            }

            // just because I dont want to make a separate function for one thing:
            // there is a single clay pot in pinnacle rock that always drops one green rupee, this one is shared with ikana graveyard pots
            // that is a problem: we need to be able to specify this one pot as either water bottom or ground
            // so for now, I'm swapping this one to a different water bottom type
            pinnacleSceneActors[10].OldVariant = pinnacleSceneActors[10].Variants[0] = 0xFF0B; // this is non vanilla type, we create it and use it for non-vanilla placement, reusing
        }

        private static void RandomizeDekuPalaceBombiwaSigns()
        {
            /// In deku palace, there are signs pointing you to the left and right across lilipads, on top of bombiwa
            /// leaving the signs while randomizing the bombiwa would be weird, so I am going to move the signs and turn them into bombiwa to add immersion

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Bombiwa)) return;

            var dekuPalaceActors = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID()).Maps[0].Actors;

            dekuPalaceActors[23].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x8077, modifyOld: true);
            dekuPalaceActors[23].OldName = "WayPointSignRight";
            dekuPalaceActors[23].Position = new vec16(1429, -40, 1583); // west to the right side

            dekuPalaceActors[24].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x8077, modifyOld: true);
            dekuPalaceActors[24].OldName = "WayPointSignLeft ";
            dekuPalaceActors[24].Position = new vec16(-1297, -40, 1529); // east to the left side

            // not sure why, in scenetatl they have no ratation, but in-rando they have x/z rotations which is messing up the actors
            ActorUtils.FlattenPitchRoll(dekuPalaceActors[23]);
            ActorUtils.FlattenPitchRoll(dekuPalaceActors[24]);

            // actual bombiwa are really low in the water, raise to just below surface
            dekuPalaceActors[19].Position.y = -40;
            dekuPalaceActors[20].Position.y = -40;
        }

        private static void RandomizeGreatbayCoastSurfaceTypes(Random rng)
        {
            /// the silver boulders in greatbaycoast are gameplay_field actors, and have no object themselves
            ///  this means there is little variety for what to replace them with
            /// however, there are two(sometimes three) unused objects in this scene we can swap out for more object variety for them
            /// note: this does not change cleared greatbay at all, mostly because it gets ignored by players 99% of the time and im lazy

            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.IshiRock)) return;

            var greatbayCoast = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.GreatBayCoast);
            List<Actor> replacementCandidates = Enemies.ReplacementCandidateList.FindAll(act => act.GetWaterVariants().Count() > 0); // start with water only

            if (rng.Next(100) < 40) // some chance of turning into water surface instead of bottom
            {
                var replacementWaterTopCandidates = Enemies.ReplacementCandidateList.FindAll(act => act.GetWaterTopVariants().Count() > 0);
                replacementCandidates.AddRange(replacementWaterTopCandidates);

                // where 0x32 is on sandy beach
                var allIshi = greatbayCoast.Maps[0].Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.IshiRock && act.OldVariant != 0x32);
                // however, most of the ocean top replacements are either water or dyna, especially with actorizer,
                // so we're only going to randomize and change half of them, or we risk putting 2-3 dyna actors on the surface and _nothing else_
                allIshi = allIshi.OrderBy(x => rng.Next()).ToList();

                for (int i = 0; i < allIshi.Count() / 2; i++)
                {
                    var ishi = allIshi[i];
                    // change them all to Octarok to trick rando
                    ishi.ChangeActor(GameObjects.Actor.Octarok, vars: 0xFF00, modifyOld: true);
                    ishi.OldName = "FormerOceanBottomBoulder";
                    ishi.Position.y = 0; // move to water surface
                }

                greatbayCoast.Maps[0].Objects[3] = GameObjects.Actor.Octarok.ObjectIndex(); // unused guay object
            }
            else // we want them to stay as water bottom, but let's at least change one of the unused objects to something we can use for more variety
            {
                List<Actor> replacementWaterBottomCandidates = Enemies.ReplacementCandidateList.FindAll(act => act.GetWaterBottomVariants().Count() > 0);
                replacementCandidates.AddRange(replacementWaterBottomCandidates);

                var randomIndex = rng.Next(replacementCandidates.Count());
                var randomGuayReplacement = replacementCandidates[randomIndex];
                replacementCandidates.RemoveAt(randomIndex);
                greatbayCoast.Maps[0].Objects[3] = randomGuayReplacement.ObjectId; // unused guay object
            }

            // 6 is also an unused object: skullfish
            var randomSkullFishIndex = rng.Next(replacementCandidates.Count());
            var randomSkullFishReplacement = replacementCandidates[randomSkullFishIndex];
            replacementCandidates.RemoveAt(randomSkullFishIndex);
            greatbayCoast.Maps[0].Objects[6] = randomSkullFishReplacement.ObjectId; // skullfish

            if (Enemies.ObjectIsCheckBlocked(GameObjects.Scene.GreatBayCoast, GameObjects.Actor.Mikau) == null)
            {
                /// we can use the mikau zora mask object too if rando isn't using it because mikau was randomized
                var randomMikauMaskIndex = rng.Next(replacementCandidates.Count());
                var randomMikauMaskReplacement = replacementCandidates[randomMikauMaskIndex];
                greatbayCoast.Maps[0].Objects[4] = randomMikauMaskReplacement.ObjectId; // cutscene mask object
            }
        }

        private static void IncreaseWoodsOfMysteryVariety(Random rng)
        {
            /// there is an extra object slot in each room, we can add a new object for more variety

            var woodsOfMysteryScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.WoodsOfMystery);

            var newActor = GameObjects.Actor.DekuBaba;
            List<GameObjects.Actor> listOfShuffledGroundActors = null;
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.NaturalPatchOfGrass)) // actors enabled
            {
                // without jump scare, this is all we can do in this setting
                listOfShuffledGroundActors = new List<GameObjects.Actor> { GameObjects.Actor.Snapper };
            }
            else
            {
                listOfShuffledGroundActors = new List<GameObjects.Actor> {
                      //GameObjects.Actor.Snapper, // for now, I dont want to replace them because this is too high of a chance
                      GameObjects.Actor.SquareSign, GameObjects.Actor.TallGrass,
                      GameObjects.Actor.MushroomCloud, GameObjects.Actor.DekuFlower
                    };
            }

            // expand the list for every room
            for (var roomId = 0; roomId < 9; roomId++)
            {
                // add object to the object list for the room
                var thisRoomMap = woodsOfMysteryScene.Maps[roomId];
                thisRoomMap.Objects = thisRoomMap.Objects.Append(newActor.ObjectIndex()).ToList();

                // specify to the room object header that the object list is larger and load the extra object
                var roomFileId = GameObjects.Scene.WoodsOfMystery.FileID() + roomId + 1;
                var roomData = RomData.MMFileList[roomFileId].Data;
                // search the headers for the objectlist, change the byte for the value
                for (int headerOffset = 0; headerOffset < 0x300; headerOffset += 0x8)
                {
                    var headerByte = roomData[headerOffset];
                    if (headerByte == 0x14) throw new Exception("this woods of mystery room was supposed to have an object list");

                    if (headerByte == 0x0B) // object list found
                    {
                        // increaese the count of the objects in the object list to be loaded into  memory
                        roomData[headerOffset + 1] = 6;
                        break;
                    }
                    if (headerOffset >= 0x2F8) throw new Exception("out of bounds");
                }

                /// search for actors we might randomly change into our new random enemy

                // generate list of candidate slots
                var actorsToRandomlyShuffle = thisRoomMap.Actors.FindAll(act => listOfShuffledGroundActors.Contains(act.ActorEnum));
                for (int i = 0; i < thisRoomMap.Actors.Count; i++)
                {
                    var actor = thisRoomMap.Actors[i];

                    if (listOfShuffledGroundActors.Contains(actor.OldActorEnum) && rng.Next(100) < 30)
                    {
                        var oldName = actor.OldName;
                        actor.ChangeActor(newActor, 0, modifyOld: true);
                        actor.OldName = oldName + "(Changling)";
                    }
                }
            }

        }

        private static void SplitLikeLikesIntoTwoActorObjects()
        {
            /// Special case: likelikes need to be split into two objects because ground and water share one object 
            /// the dual replacement is a problem as there are almost no enemies that fit this requirement

            // TODO check if this actor is randomized, currently they are always randomized

            void ReplaceAllLikelikes(List<Actor> likes)
            {
                for (int i = 0; i < likes.Count; ++i)
                {
                    var like = likes[i];
                    // update object for all of the second likelikes, so they will use the second object
                    if (like.ActorId == (int)GameObjects.Actor.LikeLike
                        && GameObjects.Actor.LikeLike.IsGroundVariant(like.OldVariant))
                    {
                        like.ChangeActor(GameObjects.Actor.LikeLikeShieldDummy, vars: 0, modifyOld: true);
                        like.OldName = "LikeLike(Sand)";
                        //newLikeLike.OldObjectId = newLikeLike.ObjectId = GameObjects.Actor.LikeLikeShield.ObjectIndex();
                    }
                }

            }

            var coastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
            ReplaceAllLikelikes(coastScene.Maps[0].Actors);
            ReplaceAllLikelikes(coastScene.Maps[1].Actors);
            var capeScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraCape.FileID());
            ReplaceAllLikelikes(capeScene.Maps[0].Actors);
            ReplaceAllLikelikes(capeScene.Maps[1].Actors);

        }



        private static void AddCoastFlavor(Random rng)
        {
            // I want some flavor, I want to move npcs to the towels/kayak under the umbrellas to show they are enjoying the beach
            // the dev that made tatl that can spot zora added like 9 talk spot actors at the beach we can use, not removing all just yet

            // scan through all leavers and turn them 270, or randomily redirect them toward compass directions

            var greatbaycoastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
            var towelLeever = greatbaycoastScene.Maps[0].Actors[138]; // standing very close to another in an area the player wouldnt notice the extra density
            ActorUtils.SetActorSpawnTimeFlags(towelLeever); // by default, this one is night only
            ActorUtils.FlattenPitchRoll(towelLeever);
            towelLeever.ChangeYRotation(270);
            towelLeever.Position = new vec16(-18, 80, 3734); // moved to center towel
            towelLeever.ChangeActor(GameObjects.Actor.Leever, vars: 0xFF, modifyOld: true);

            var towel2LikeLike = greatbaycoastScene.Maps[0].Actors[143];
            towel2LikeLike.Position = new vec16(-79, 82, 3975);
            ActorUtils.FlattenPitchRoll(towel2LikeLike); // uses crazy things
            ActorUtils.SetActorSpawnTimeFlags(towel2LikeLike);
            towel2LikeLike.ChangeYRotation(270);
            towel2LikeLike.ChangeActor(GameObjects.Actor.LikeLike, 3, modifyOld: true);

            var extraKayakRearActor = greatbaycoastScene.Maps[0].Actors[149];
            extraKayakRearActor.ChangeActor(GameObjects.Actor.Leever, vars: 0xFF, modifyOld: true);
            extraKayakRearActor.OldName = "KayakReeba";
            extraKayakRearActor.Position = new vec16(-101, 103, 4900); // kayak 1 rear
            ActorUtils.FlattenPitchRoll(extraKayakRearActor); // uses crazy things
            extraKayakRearActor.ChangeYRotation(270);
            ActorUtils.SetActorSpawnTimeFlags(extraKayakRearActor);

            var extraKayakRearActor2 = greatbaycoastScene.Maps[0].Actors[145];
            extraKayakRearActor2.ChangeActor(GameObjects.Actor.Leever, vars: 0xFF, modifyOld: true);
            extraKayakRearActor2.OldName = "KayakReeba";
            extraKayakRearActor2.Position = new vec16(-185, 93, 4900); // kayak 1 front
            ActorUtils.FlattenPitchRoll(extraKayakRearActor2); // uses crazy things
            extraKayakRearActor2.ChangeYRotation(270);
            ActorUtils.SetActorSpawnTimeFlags(extraKayakRearActor2);

            var extraBeachLeever1 = greatbaycoastScene.Maps[0].Actors[146];
            extraBeachLeever1.Position = new vec16(-521, 30, 3094);
            ActorUtils.FlattenPitchRoll(extraBeachLeever1); // uses crazy things
            extraBeachLeever1.ChangeYRotation(270);
            ActorUtils.SetActorSpawnTimeFlags(extraBeachLeever1);
            extraBeachLeever1.ChangeActor(GameObjects.Actor.Leever, vars: 0xFF, modifyOld: true);
            //extraBeachLeever1.OldName = "Leaver";

            // a lot of the likelikes are night only, this can make greatbay coast north rocky area too boring during the day
            // 21-24 are night likes
            for (int i = 21; i < 25; i++)
            {
                var nightlike = greatbaycoastScene.Maps[0].Actors[i];
                if (rng.Next(100) < 65)
                {
                    ActorUtils.SetActorDaySpawnFlags(nightlike);
                }
            }
        }


        // tag meta
        public static void ModifyScenesForEnemizer(RandomizedResult result, bool ACTORSENABLED, Random rng, StringBuilder log)
        {
            /// Modify scene actors, the objects, the paths, sometimes other data

            // modfiying actor spawns to fix weird behavior
            FixSpawnLocations(ACTORSENABLED); // TODO break this, so we can separate these
            FixSouthernSwampLensBehavior();
            FixSouthernSwampGossipStoneObjectPlacement();

            FixSouthernSwampDekuBaba(rng);
            FixRoadToSouthernSwampBadBat();

            if (ACTORSENABLED)
            {
                // changes to get actorizer working
                RandomizeMonkeyActors();
                SplitSceneSnowballIntoTwoActorObjects();
                RearangeSecretShrineObjects(ACTORSENABLED, rng); // todo remove actors flag
                SwapPiratesFortressBgBreakwall();
                SwapShopActors();
                SplitPirateSewerMines();
                SwapSwampSpiderhouseRock();
                EnableSethSwampSpiderhouse();
                RepositionClockTownActors();
                ReplaceStonetowerFunenObject();

                // tweaking actor spawns to improve actorizer actor compatibility
                RotateTalkSpotsAndHitSpots();
                NudgeFlyingEnemiesForTingle();
                DistinguishLogicRequiredDekuFlowers();
                DuplicateObjectForTorchInButlerRace();

                // tweaks to add actorizer variety
                RandomlySwapOutZoraBandMember(rng);
                RandomizePinnacleRockSigns();
                RandomizeDekuPalaceBombiwaSigns();
                RandomizeGreatbayCoastSurfaceTypes(rng);
                AddCoastFlavor(rng);

                // modify cutscene actors to be more interesting instead of broken
                SwapIntroActors();
                MoveCreditsPostmanPath();
                SwapCreditsCremia();
                ChangeIkanaCanyonCreditsActors(rng);
                SwapGreatFairies(result, rng);
            }

            // changing enemy variants for enemizer to work
            FixSpecificLikeLikeVariants();
            FixSpecificTektiteTypes();
            SplitOceanSpiderhouseSpiderObject();
            SplitLikeLikesIntoTwoActorObjects();
            EnableTwinIslandsSpringSkullfish();
            SwitchGBTEncounterForSkullfish();
            SwitchZoraCapeEncounterForSkullfish();
            EnableDampeHouseWallMaster();
            ModifyAllGraveyardBatsToFly();

            // modifications for better variety
            FixRoadToSouthernSwampBadBat();
            IncreaseWoodsOfMysteryVariety(rng);
            AddGrottoVariety(rng);
            FixJPGrottos(rng, log);


            // changing scene object lists
            RemoveSTTUnusedPoe();
            FixDekuPalaceReceptionGuards();
            FixSwordSchoolPotRandomization();
            SplitSnowheadTempleBo();
            AddExtraObjectToPiratesInterior(ACTORSENABLED, rng);
            ExpandGoronShineObjects();
            ExpandGoronRaceObjects();
            FixWoodfallTempleGekkoMiniboss();

            // moving scene transitions to be more fair for no-hit
            MoveTheISTTTunnelTransitionBack();
            MoveThePFSTunnelTransitionBack();

            Shinanigans(ACTORSENABLED, rng, log);

        }

        private static void RotateSignActors()
        {
            var milkroadScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkRoad.FileID()); ;
            var milkroadPointedSign = milkroadScene.Maps[0].Actors[21];
            if (milkroadPointedSign.ActorEnum != GameObjects.Actor.PointedSign)
            {
                // vanilla angle faces the wall, which is especially bad if its a grotto it locks the player by facing them into wall they fall back in
                milkroadPointedSign.ChangeYRotation(180 - 15);
            }
            SceneUtils.UpdateScene(milkroadScene);

        }


        // TODO shrink and re-organize this
        public static void MoveActorsIfRandomized(bool ACTORSENABLED)
        {

            /// if ossan in trading post was randomized we want to move one of them, as there are two of the, assumed for late night
            var tradingpostScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TradingPost.FileID());
            var secondOssan = tradingpostScene.Maps[0].Actors[1];
            if (secondOssan.ActorEnum != GameObjects.Actor.TradingPostShop)
            {
                secondOssan.Position = new vec16(-35, 25, -154);
                SceneUtils.UpdateScene(tradingpostScene);
            }

            // if we randomize the bombiwa in the swamp spiderhouse, replacements with colliders can block bugs
            // for now, decided to just un-randomize

            // if we randomize cremia in the branch, the uma cart can crash, we need to change its type from ranch to termina field
            var romaniRanchScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RomaniRanch.FileID());
            var cremia = romaniRanchScene.Maps[0].Actors[2];
            if (cremia.ActorEnum != GameObjects.Actor.Cremia)
            {
                var cariageHorse = romaniRanchScene.Maps[0].Actors[34];
                //cariageHorse.Variants[0] = 0x0; // same as termina field, which doesnt have cremia on it
                var ranchRoom0Data = RomData.MMFileList[GameObjects.Scene.RomaniRanch.FileID() + 1].Data; // 1327
                //have to erase this actor directly
                ranchRoom0Data[0x2A4] = 0xFF; // this works, although would be cool if we could just change type
                ranchRoom0Data[0x2A5] = 0xFF;
                //ranchRoom0Data[0x2B2] = 0x0; // attempted change of variant type to zero, this does not work, best to remove the whole actor for now
                //ranchRoom0Data[0x2B3] = 0x0;

                // now that the cariage is gone we should try to remove the objects to make space for other things in the scene
                ReadWriteUtils.Arr_WriteU16(ranchRoom0Data, 0x74, (ushort) ObjectEnum.SmallestObj); // carriage
                ReadWriteUtils.Arr_WriteU16(ranchRoom0Data, 0x72, (ushort) ObjectEnum.SmallestObj); // object_ha is the donkey the cart uses
            }

            var terminaField = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            var terminaFieldScopeNuts = terminaField.Maps[0].Actors[210]; // buisness scrub
            if (terminaFieldScopeNuts.ActorEnum != GameObjects.Actor.FlyingFieldScrub)
            {
                terminaFieldScopeNuts.Position = new vec16(780, 760, 615); // move closer to the edge of ect so the player can see it
            }

            var terminaFieldWestGossipBombiwa = terminaField.Maps[0].Actors[198];
            if (terminaFieldWestGossipBombiwa.ActorEnum != GameObjects.Actor.Bombiwa) // assumption: currently both have to be randomized at the same time
            {
                terminaFieldWestGossipBombiwa.Position.z = -1727; // move back from sitting right on top of the grotto
                terminaField.Maps[0].Actors[199].Position.z = -642; // move back from sitting right on top of the grotto
            }
            SceneUtils.UpdateScene(terminaField);

            var roadToIkanaCanyonScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RoadToIkana.FileID());
            var roadToIkanaRedHamishi = roadToIkanaCanyonScene.Maps[0].Actors[5];
            if (roadToIkanaRedHamishi.ActorEnum != GameObjects.Actor.BronzeBoulder) // assumption: currently both have to be randomized at the same time
            {
                roadToIkanaRedHamishi.Position.z = -413; // move back from sitting right on top of the grotto
                // TODO change rotation?
            }
            SceneUtils.UpdateScene(roadToIkanaCanyonScene);

            var snowheadTempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SnowheadTemple.FileID());
            var snowheadTempleFireArrowWiz = snowheadTempleScene.Maps[6].Actors[0];
            if (snowheadTempleFireArrowWiz.ActorEnum != GameObjects.Actor.Wizrobe)
            {
                snowheadTempleFireArrowWiz.Position.x = -1300; // move back to center of the room, not sure why this guy is so close to the door normally
            }
            // if snowhead temple wizrobe the second is randomized, his spawn is in a bad spot for enemizer
            var snowheadSecondWizrobe = snowheadTempleScene.Maps[12].Actors[0];
            if (snowheadSecondWizrobe.ActorEnum != GameObjects.Actor.Wizrobe)
            {
                snowheadSecondWizrobe.Position = new vec16(1377, 1800, 0); // moved to other size of room, not next to the door
            }
            SceneUtils.UpdateScene(snowheadTempleScene);

            var capeScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraCape.FileID());
            var capeHpLikelike = capeScene.Maps[0].Actors[18];
            if (capeHpLikelike.ActorEnum != GameObjects.Actor.LikeLike)
            {
                var newUnkillableVariants = capeHpLikelike.ActorEnum.UnkillableVariants();
                if (newUnkillableVariants != null && newUnkillableVariants.Contains(capeHpLikelike.Variants[0]))
                {
                    capeHpLikelike.Position.z = 4405; // move back from sitting on hp
                }
            }


            if (ACTORSENABLED)
            {
                var capeGrottoBombiwa = capeScene.Maps[0].Actors[44];
                if (capeGrottoBombiwa.ActorEnum != GameObjects.Actor.Bombiwa)
                {
                    var newUnkillableVariants = capeGrottoBombiwa.ActorEnum.UnkillableVariants();
                    if (newUnkillableVariants != null && newUnkillableVariants.Contains(capeGrottoBombiwa.Variants[0]))
                    {
                        capeGrottoBombiwa.Position.x = -463; // move to the left somewhat, worried its not pointed at the hole but not sure its worth the bother
                        capeGrottoBombiwa.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: capeGrottoBombiwa.Rotation.y);
                    }
                }
                SceneUtils.UpdateScene(capeScene);

                var ikanaGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaGraveyard.FileID());
                var graveyardGrottoRockCircle = ikanaGraveyardScene.Maps[1].Actors[44];
                if (graveyardGrottoRockCircle.ActorEnum != GameObjects.Actor.GrassRockCluster)
                {
                    graveyardGrottoRockCircle.Position.z = -1877; // move back from sitting right on top of the grotto
                }
                SceneUtils.UpdateScene(ikanaGraveyardScene);

                RotateSignActors();

                // both gorman and postman start behind the door if they are randomized, which puts then out of sight and if likelike can grab you through the door
                var milkbarScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
                /*var milkbarPostman = milkbarScene.Maps[0].Actors[12];
                // this does nothing: something is overrideing this data, not sure if itemizer
                if (milkbarPostman.ActorEnum != GameObjects.Actor.PostMan)
                {
                    milkbarPostman.Position = new vec16(0, 0, 0);
                    ActorUtils.SetActorSpawnTimeFlags(milkbarPostman);
                } // */
                var milkbarGorman = milkbarScene.Maps[0].Actors[7];
                if (milkbarGorman.ActorEnum != GameObjects.Actor.Gorman)
                {
                    milkbarGorman.Position = new vec16(61, 0, -162);
                }
                SceneUtils.UpdateScene(milkbarScene);

                var nctScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.NorthClockTown.FileID());
                var nctHP = nctScene.Maps[0].Actors[4];
                var nctUglyTree = nctScene.Maps[0].Actors[7];
                if (nctUglyTree.ActorEnum != GameObjects.Actor.UglyTree)
                {
                    nctHP.Position = new vec16(141, 375, -2336); // moved to post
                }
                SceneUtils.UpdateScene(nctScene);

                var ikanaCastleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCastle.FileID());
                var ceilingSkulltula1 = ikanaCastleScene.Maps[4].Actors[0];
                var killableVariants = ceilingSkulltula1.ActorEnum.KillableVariants();
                if (killableVariants == null || killableVariants.Count == 0)
                {
                    ceilingSkulltula1.Position = new vec16(979, -1038, -2203); // moved left and up
                }
                SceneUtils.UpdateScene(ikanaCastleScene);

                var roadToMountainsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PathToMountainVillage.FileID());
                // some winter snowballs are sitting on top of each other, they should be moved
                // snowballs 24 and 48 are duplicates, sitting on top of each other, move them so they arent
                roadToMountainsScene.Maps[0].Actors[48].Position.z = 6227;
                ActorUtils.FlattenPitchRoll(roadToMountainsScene.Maps[0].Actors[48]);
                // snowball 29 and 32 are on top of each other
                roadToMountainsScene.Maps[0].Actors[32].Position.x = 568;
                roadToMountainsScene.Maps[0].Actors[32].Position.z = 6418;
                ActorUtils.FlattenPitchRoll(roadToMountainsScene.Maps[0].Actors[32]);
                // snowball 28 and 30 are on top of each other
                roadToMountainsScene.Maps[0].Actors[30].Position.x = 1790;
                roadToMountainsScene.Maps[0].Actors[30].Position.z = 6841;
                ActorUtils.FlattenPitchRoll(roadToMountainsScene.Maps[0].Actors[30]);
                // snowball 26 and 44 are on top of each other
                roadToMountainsScene.Maps[0].Actors[44].Position.x = 2000;
                roadToMountainsScene.Maps[0].Actors[44].Position.z = 6612;
                ActorUtils.FlattenPitchRoll(roadToMountainsScene.Maps[0].Actors[44]);
                SceneUtils.UpdateScene(roadToMountainsScene);

                var stockpotInnScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StockPotInn.FileID());
                stockpotInnScene.Maps[4].Actors[4].ChangeYRotation(270); // mushroom was facing the right wall
                var mailmanActor = stockpotInnScene.Maps[0].Actors[22];
                if (mailmanActor.ActorEnum != GameObjects.Actor.PostMan)
                {
                    mailmanActor.Position = new vec16(185, 0, 107); // moved in front of anju, surely that wont be a problem
                    mailmanActor.ChangeYRotation(270); // turn to face left towards anju
                }
                SceneUtils.UpdateScene(stockpotInnScene);

                var oceanspiderhouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.OceanSpiderHouse.FileID());
                var oshSeth = oceanspiderhouseScene.Maps[0].Actors[2];
                if (oshSeth.ActorEnum != GameObjects.Actor.Seth1)
                {
                    oshSeth.Position.x = -113;
                    oshSeth.Position.z = 325;
                    oshSeth.ChangeYRotation(45);
                }
                SceneUtils.UpdateScene(oceanspiderhouseScene);

                var curiosistyShopScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.CuriosityShop.FileID());
                var kafeiInShop = curiosistyShopScene.Maps[1].Actors[1];
                if (kafeiInShop.ActorEnum != GameObjects.Actor.Kafei)
                {
                    kafeiInShop.Position = new vec16(-189, 21, -425);
                    // does he need rotation?
                }
                SceneUtils.UpdateScene(curiosistyShopScene);


            }

            FixEvanRotationIfRandomized();
            MoveShopScrubsIfRandomized();
            MovePostmanIfRandomized(terminaField);
            MoveLaundryPoolBellTalkSpotIfRandomized();

        }

        private static void FixEvanRotationIfRandomized()
        {
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Evan)) return;

            // if evan is randomized, then his replacement is staring at the wall
            var zorahallRoomsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHallRooms.FileID());
            var evan = zorahallRoomsScene.Maps[3].Actors[0];

            /// if ossan in trading post was randomized we want to move one of them, as there are two of the, assumed for late night
            if (evan.ActorEnum != GameObjects.Actor.Evan)
            {
                evan.Rotation.y = ActorUtils.MergeRotationAndFlags(180 + 90 + 15, flags: evan.Rotation.y);
                SceneUtils.UpdateScene(zorahallRoomsScene);
            }

        }

        private static void MoveShopScrubsIfRandomized()
        {
            /// if we randomize the shop scrubs, then we have two of them sitting on top of each other, which is weird
            var southernSwamp = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());
            var swampScrub = southernSwamp.Maps[0].Actors[0]; // first is zero
            if (swampScrub.ActorEnum != GameObjects.Actor.BuisnessScrub)
            {
                var stationaryScrub = southernSwamp.Maps[0].Actors[1]; // needs to be rotated, naturally faces left down the swamp
                stationaryScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: stationaryScrub.Rotation.y);

                stationaryScrub.Position = new vec16(115, 170, 26);
                SceneUtils.UpdateScene(southernSwamp);
            }
            // TODO cleared swamp

            var goronvillage = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronVillage.FileID());
            var gvScrub = goronvillage.Maps[0].Actors[4]; // first is 3
            if (gvScrub.ActorEnum != GameObjects.Actor.BuisnessScrub)
            {
                gvScrub.Position = new vec16(168, -200, 400);
                gvScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(0, flags: gvScrub.Rotation.y); // turn back around to face the other guy
                SceneUtils.UpdateScene(goronvillage);
            }

            var zoraHallrooms = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHallRooms.FileID());
            var zorahallScrub = zoraHallrooms.Maps[2].Actors[1]; // first is zero
            if (zorahallScrub.ActorEnum != GameObjects.Actor.BuisnessScrub)
            {
                var stationaryScrub = zoraHallrooms.Maps[2].Actors[0]; // needs to be rotated, naturally faces the door
                stationaryScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: stationaryScrub.Rotation.y);

                zorahallScrub.Position = new vec16(-2113, 40, -71);
                zorahallScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zorahallScrub.Rotation.y);
                SceneUtils.UpdateScene(zoraHallrooms);
            }

            // 42, 41, 4 (flower)
            var southclocktownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var dekuFlowerUnderTheScrub = southclocktownScene.Maps[0].Actors[4];
            if (dekuFlowerUnderTheScrub.ActorEnum != GameObjects.Actor.DekuFlower)
            {
                var stationaryScrub = southclocktownScene.Maps[0].Actors[0];
                stationaryScrub.Position = new vec16(-629, 0, -348);
                stationaryScrub.ChangeYRotation(270);
            }
        }

        public static void SwitchSkullfishBackToEncount1(Enemies.SceneEnemizerData thisSceneData)
        {
            /// in order for the ocean skullfish to spawn over and over and harrass the player
            /// we have to use a different spawning actor, EnEncount1
            /// here, we take a special case of parameters I made up, and switch it back
            /// also, skullfish parameters use rotation heavily, modify them here too

            // TODO why is this a last second change? this could have been a pre-fix in Earlyfixes I think? re-examine

            if (!thisSceneData.Objects.Contains((int)GameObjects.Actor.SkullFish.ObjectIndex())) return;

            for (int a = 0; a < thisSceneData.Actors.Count; a++)
            {
                var actor = thisSceneData.Actors[a];

                if (actor.ActorEnum == GameObjects.Actor.SkullFish)
                {
                    var validDropIds = new int[] {
                        0x3, 0x11, 0x7, // stone tower temple
                        0, 0xE, 0x7, 0xD, // gbt
                        0x1, // encount in gbt
                        // cape is always drop nothing, lame
                    };
                    var nextDropId = validDropIds[thisSceneData.RNG.Next(validDropIds.Length)];

                    if (actor.Variants[0] == 0xFFFF) // encount swap
                    {
                        actor.ChangeActor(GameObjects.Actor.En_Encount1, vars: 0x105E);
                        actor.ChangeXRotation(0x38); // x rotation is the rate at which they re-spawn, 0x28 is the fast one near the cape, 0x6Xsomething other places
                        actor.ChangeYRotation((nextDropId - 1));    // y rotation is item drop pool index
                        actor.ChangeZRotation(0x32); // z rotation is agro range, cape is 0x32

                    }
                    else
                    {
                        // z rotation for non encount types (PR2 type 2) is drop table
                        // reminder, its (z-rot -1) to get index, as zero is ignore case
                        actor.ChangeZRotation(nextDropId - 1);
                    }

                    // in order for an actor to get the rotation raw, instead of converted, we need to set a flag for each parameter
                    actor.ActorIdFlags |= 0x8000 | 0x4000 | 0x2000;
                }
            }
        }

        private static void MovePostmanIfRandomized(Scene terminaField)
        {

            var westclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WestClockTown.FileID());
            var westclocktownPostMan = westclocktown.Maps[0].Actors[18];
            if (westclocktownPostMan.ActorEnum != GameObjects.Actor.PostMan)
            {
                westclocktownPostMan.Position = new vec16(-1523, 200, -1376); // move outside of the door
                SceneUtils.UpdateScene(westclocktown);

            }

            var eastclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
            var eastclocktownPostman = eastclocktown.Maps[0].Actors[12];
            if (eastclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                eastclocktownPostman.Position = new vec16(1150, 200, -1405); // move outside of the door
                // rot zero faces mostly to the east wall and a touch south, turn to face mayors
                eastclocktownPostman.Rotation.y = ActorUtils.MergeRotationAndFlags(90 + 45 + 30, flags: eastclocktownPostman.Rotation.y);
                SceneUtils.UpdateScene(eastclocktown);
            }

            var southclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var southclocktownPostman = southclocktown.Maps[0].Actors[6];
            if (southclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                southclocktownPostman.Position = new vec16(-1548, 200, -1097); // move into the visible
                SceneUtils.UpdateScene(southclocktown);
            }

            var northclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.NorthClockTown.FileID());
            var northclocktownPostman = northclocktown.Maps[0].Actors[20];
            if (northclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                northclocktownPostman.Position = new vec16(-31, 205, -1883); // move into the visible
                SceneUtils.UpdateScene(northclocktown);
            }

            // milkbar
            var milkbar = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
            var milkbarPostman = milkbar.Maps[0].Actors[12];
            if (milkbarPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                milkbarPostman.Position = new vec16(55, 2, -172); // move next to the bar
                // his time flags are to be there on all days, but this might be confusing? do we reduce to night 3 and day0/4?
                milkbarPostman.Rotation.x &= ~1; // remove the day 1 day spawn flag
                milkbarPostman.Rotation.z &= ~0x78; // remove the day 1 night flag, and all of day 2 flags, and day 3 day flag, but not day 4 or day 3 night flags
                // turn slightly right to face bar
                milkbarPostman.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: milkbarPostman.Rotation.y);

                SceneUtils.UpdateScene(milkbar);
            }

            var milkbarGorman = milkbar.Maps[0].Actors[7];
            if (milkbarGorman.ActorEnum != GameObjects.Actor.Gorman)
            {
                milkbarGorman.Position = new vec16(57, 2, -87); // move next to the bar
                // turn slightly right to face bar
                milkbarGorman.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: milkbarGorman.Rotation.y);

                SceneUtils.UpdateScene(milkbar);
            }

        }

        private static void MoveLaundryPoolBellTalkSpotIfRandomized()
        {
            /// would just float in the air its weird

            var laundryPoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LaundryPool.FileID());
            var lpBell = laundryPoolScene.Maps[0].Actors[11];
            var lpBellTalkSpot = laundryPoolScene.Maps[0].Actors[10];
            ActorUtils.ClearActorRotationRestrictions(lpBellTalkSpot);

            if (lpBell.ActorEnum != GameObjects.Actor.LaundryPoolBell)
            {
                lpBellTalkSpot.Position = new vec16(-1961, -56, 627);
                lpBellTalkSpot.ChangeYRotation(180);
            }
            else
            {
                // change rotation to match bell at least
                lpBellTalkSpot.ChangeYRotation(90); // zero faces into the near wall
            }
            SceneUtils.UpdateScene(laundryPoolScene);
        }


        private static void MoveTheISTTTunnelTransitionBack()
        {
            /// the room tranition for the scene is very close to the edge of the dexihand
            /// this presents a problem for enemizer if playing no hit rules

            var isttSceneData = RomData.MMFileList[GameObjects.Scene.InvertedStoneTowerTemple.FileID()].Data;
            isttSceneData[0xD7] = 0xBC; // 294 -> 2BC, from pos.x = 660 to 700
            var sceneClass = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.InvertedStoneTowerTemple.FileID());
            // move the switch a little up the hallway
            sceneClass.Maps[3].Actors[28].Position.x = 800;
        }

        private static void MoveThePFSTunnelTransitionBack()
        {
            /// the pirates fortress sewer transition, from the underwater maze to the mine tunnel, is too close to the mines and is dangerous for enemizer no-hit

            // room 13, but the door is part of the scene
            var piratesSewerData = RomData.MMFileList[GameObjects.Scene.PiratesFortressRooms.FileID()].Data;
            // doors are E header, this is door #16
            // we need to know where the door details are
            piratesSewerData[0x253] = 0x5E; // from Z rot 300 (12C) to 350 (15E)

            // we also want to move the mines a bit further back just a bit
            var sewerScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortressRooms.FileID());
            var mazeMap = sewerScene.Maps[12];
            var tunnelMap = sewerScene.Maps[10];

            mazeMap.Actors[0].Position.z -= 50; // both mines
            mazeMap.Actors[1].Position.z -= 50;
            tunnelMap.Actors[1].Position.z -= 50; // both mines
            tunnelMap.Actors[2].Position.z -= 50;
        }

        public static void FixKafeiPlacements()
        {
            if (!Enemies.VanillaEnemyList.Contains(GameObjects.Actor.Kafei)) return;

            /// if Kafei is randomized, his default placements are silly, move them to be more natural
            var southClockTown = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.SouthClockTown);
            var sctKafei = southClockTown.Maps[0].Actors[2];
            if (sctKafei.ActorEnum != GameObjects.Actor.Kafei)
            {
                // move to the bench so hes not lurking out of sight behind the laundry room area
                sctKafei.Position = new vec16(-615, 16, 425);
                sctKafei.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: sctKafei.Rotation.y);
                SceneUtils.UpdateScene(southClockTown);
            }

            var eastClockTown = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.EastClockTown);
            var ectKafei = eastClockTown.Maps[0].Actors[2];
            if (ectKafei.ActorEnum != GameObjects.Actor.Kafei)
            {
                // sitting just outside of town door, move inwards a bit
                ectKafei.Position = new vec16(1475, 60, -747);
                ectKafei.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: ectKafei.Rotation.y);
                SceneUtils.UpdateScene(eastClockTown);
            }

            var laundryPool = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.LaundryPool);
            var lpKafei = laundryPool.Maps[0].Actors[9];
            if (lpKafei.ActorEnum != GameObjects.Actor.Kafei)
            {
                // sitting beyond the path back to SCT, move to bridge
                lpKafei.Position = new vec16(-2080, -95, 582);
                SceneUtils.UpdateScene(laundryPool);
            }

            var ikanaCanyon = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.IkanaCanyon);
            var ikanaKafei = ikanaCanyon.Maps[4].Actors[9];
            if (ikanaKafei.ActorEnum != GameObjects.Actor.Kafei)
            {
                // move to his favorite rock
                ikanaKafei.Position = new vec16(2523, -160, 5080);
                SceneUtils.UpdateScene(ikanaCanyon);
            }

        }

        public static void FixWaterPostboxes(Enemies.SceneEnemizerData thisSceneData)
        {
            /// makes underwater post boxes have the correct vars
            /// this probably shouldnt be its own code I just want underwater postboxes without the vanilla vars thinking they can be water
            /// and un-willing right now to re-write the parameter system to specify vanilla or not

            //if ( ! thisSceneData.Objects.Contains(GameObjects.Actor.Postbox.ObjectIndex())) return;
            // that doesnt work if we are borrowing a vanilla un-touched object
            // lets skip short circuit, it's not that much faster to search all objects when we can search all actors, most areas have small lists

            foreach (var box in thisSceneData.Actors.FindAll(a => a.ActorId == (int)GameObjects.Actor.Postbox))
            {
                var oldVariant = box.Variants[0];
                if (box.Variants[0] > 4) // non-vanilla is greater than 4, but vanilla requires specific numbers
                    box.Variants[0] &= 0x4; // revert to vanilla after choosing
            }
        }

        public static void FixSnowballActorSpawns(Enemies.SceneEnemizerData thisSceneData)
        {
            /// The large snowballs can sometimes spawn an actor when you break them,
            /// but they are too stupid to handle the possibility of the actor object missing, crash
            /// but we cannot block them from spawning based on params because params is not used to specify
            /// instead, the parameter that controls snowball type is rotation.y, so we nullify it here per-scene where we add them

            var largeSnowballs = thisSceneData.Actors.FindAll(actor => actor.ActorEnum == GameObjects.Actor.LargeSnowball);
            if (largeSnowballs.Count > 0)
            {
                for (int i = 0; i < largeSnowballs.Count; i++)
                {
                    var snowball = largeSnowballs[i];
                    // where zero rotation (type 0) just drops an item, no actor
                    snowball.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 0, flags: snowball.Rotation.y);
                }
            }
        }

        public static void FixNewGrottoZRotation(Enemies.SceneEnemizerData thisSceneData)
        {
            /// grottos that have an upper byte of 0x0 with a type of 0x000 or 0x200 are z rotation reading types,
            /// because they use the lower byte for item/chest characteristic (en_torch)
            /// so we have to update the zrotation to match

            var allGrottos = thisSceneData.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.GrottoHole);
            for (int a = 0; a < allGrottos.Count(); a++)
            {
                var actor = allGrottos[a];
                var variant = actor.Variants[0];
                var type = (variant & 0x300) >> 8;
                var upperAddress = variant & 0xF000;
                if (upperAddress == 0 && (type == 0 || type == 2))
                {
                    var lowerByte = variant & 0xFF; // item/chest passed to regular grotto
                                                    // 0xFF lower byte is cow grotto, no items passed, we want address A
                                                    // else: regular grotto, we want address 4
                    var newRotation = 0;
                    switch (lowerByte)
                    {
                        case 0xFF:
                            newRotation = 0xA; // cow grotto needs entrance A
                            break;
                        case 0x1F:             // tf grottos, shouldnt be placed because we dont know the rotation they used, this is backup
                            newRotation = 0x1;
                            break;
                        default:
                            newRotation = 0x4; // other assumes generic grotto
                            break;
                    }

                    actor.ChangeZRotation(newRotation);
                    actor.ActorIdFlags |= 0x2000; // do not convert Z rotation into 360 angle, leave alone to use as parameter
                }
            }
        }
    }
}
