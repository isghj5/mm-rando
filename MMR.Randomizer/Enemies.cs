﻿using Microsoft.Toolkit.HighPerformance.Extensions;
using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMR.Randomizer
{
    public class Enemies
    {
        public class ValueSwap
        {
            // these are indexes of objects
            public int OldV;
            public int NewV;
        }
         
        //private static List<Enemy> EnemyList { get; set; }
        private static List<GameObjects.Actor> EnemyList { get; set; }
        private static Mutex EnemizerLogMutex = new Mutex();
        private static bool ACTORSENABLED = true;

        public static void ReadEnemyList()
        {
            //.Where(u => u.IsEnemyRandomized()) // enemizer only
            EnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.ObjectIndex() > 3
                                && (u.IsEnemyRandomized() || (ACTORSENABLED && u.IsActorRandomized()))) // both
                            .ToList();
        }

        public static List<Enemy> GetSceneEnemyActors(Scene scene)
        {
            /// this is separate from object because actors and objects are a different list in the scene data
            
            // I prefer foreach, but in benchmarks its considerably slower, and enemizer has performance issues

            var enemyList = new List<Enemy>();
            for (int mapNumber = 0; mapNumber < scene.Maps.Count; ++mapNumber)
            {
                for (int actorNumber = 0; actorNumber < scene.Maps[mapNumber].Actors.Count; ++actorNumber) // (var mapActor in scene.Maps[mapNumber].Actors)
                {
                    var mapActor = scene.Maps[mapNumber].Actors[actorNumber];
                    var matchingEnemy = EnemyList.Find(u => (int) u == mapActor.n);
                    if (matchingEnemy > 0) {
                        var listOfAcceptableVariants = matchingEnemy.Variants();
                        if ( !matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum)
                            && listOfAcceptableVariants.Contains(mapActor.v))
                        {
                            var newEnemy = matchingEnemy.ToEnemy();
                            newEnemy.Variables = new List<int> { mapActor.v };
                            newEnemy.MustNotRespawn = scene.SceneEnum.IsClearEnemyPuzzleRoom(mapNumber)
                                                   || scene.SceneEnum.IsFairyDroppingEnemy(mapNumber, actorNumber);
                            newEnemy.Room = mapNumber;
                            newEnemy.RoomActorIndex = scene.Maps[mapNumber].Actors.IndexOf(mapActor);
                            newEnemy.Rotation = mapActor.r;
                            enemyList.Add(newEnemy);
                        }
                    }
                }
            }
            return enemyList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
            /// this is separate from actor because actors and objects are a different list in the scene data

            List<int> objList = new List<int>();
            foreach (var sceneMap in scene.Maps)
            {
                foreach (var mapObject in sceneMap.Objects)
                {
                    var matchingEnemy = EnemyList.Find(u => u.ObjectIndex() == mapObject);
                    if (   matchingEnemy > 0                                               // exists in the list
                       && !objList.Contains(matchingEnemy.ObjectIndex())                     // not already extracted from this scene
                       && !matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum)) // not excluded from being extracted from this scene
                    {
                        objList.Add(matchingEnemy.ObjectIndex());
                    }
                }
            }
            return objList;
        }

        public static void SetSceneEnemyActors(Scene scene, List<Enemy> enemies)
        {
            for (int roomIndex = 0; roomIndex < scene.Maps.Count; ++roomIndex)
            {
                var roomEnemyList = enemies.FindAll(u => u.Room == roomIndex);
                var sceneRoom = scene.Maps[roomIndex];
                foreach( var roomEnemy in roomEnemyList)
                {
                    int actorIndex = roomEnemy.RoomActorIndex;
                    sceneRoom.Actors[actorIndex].n = roomEnemy.Actor;
                    sceneRoom.Actors[actorIndex].v = roomEnemy.Variables[0];
                    sceneRoom.Actors[actorIndex].r = roomEnemy.Rotation;
                }
            }
        }

        public static void SetSceneEnemyObjects(Scene scene, List<ValueSwap> newObjects)
        {
            foreach (var sceneMap in scene.Maps)
            for (int sceneObjIndex = 0; sceneObjIndex < sceneMap.Objects.Count; sceneObjIndex++)
            {
                var valueSwapObject = newObjects.Find(u => u.OldV == sceneMap.Objects[sceneObjIndex]);
                if (valueSwapObject != null)
                {
                    sceneMap.Objects[sceneObjIndex] = valueSwapObject.NewV;
                }
            }
        }

        public static List<GameObjects.Actor> GetSceneFairyDroppingEnemyTypes(Scene scene, List<Enemy> listOfReadEnemies)
        {
            // reads the list of specific actors of fairies, checks the list of actors we read from the scene, gets the actor types for GetMatches
            // why? because our object focused code needs to whittle the list of actors for a enemy replacement, 
            //   but has to know if even one enemy is used for fairies that it cannot be unkillable
            // doing that last second per-enemy would be expensive, so we need to check per-scene
            // we COULD hard code these types into the scene data, but if someone in the distant future doesn't realize they have to add both, might be a hard bug to find

            var actorsThatDropFairies = scene.SceneEnum.GetSceneFairyDroppingEnemies();
            var returnActorTypes = new List<GameObjects.Actor>();
            for(int actorNum = 0; actorNum < listOfReadEnemies.Count; ++actorNum) // var enemy in listOfReadEnemies)
            {
                for(int fairyRoom = 0; fairyRoom < actorsThatDropFairies.Count; ++fairyRoom)
                {
                    if ( listOfReadEnemies[actorNum].Room == actorsThatDropFairies[fairyRoom].roomNumber
                      && actorsThatDropFairies[fairyRoom].actorNumbers.Contains(listOfReadEnemies[actorNum].RoomActorIndex) )
                    {
                        returnActorTypes.Add((GameObjects.Actor)listOfReadEnemies[actorNum].Actor);
                    }
                }
            }
            return returnActorTypes;
        }


        public static void LowerEnemiesResourceLoad()
        {
            /// some enemies are really hard on the CPU/RSP, we can fix some of them to behave nicer with flags
            foreach (var enemy in EnemyList.Where(u => u.ActorInitOffset() > 0).ToList())
            {
                /// bit flags 4-6 according to crookedpoe: Always Run update, Always Draw, Never Cull
                RomUtils.CheckCompressed(enemy.FileListIndex());
                RomData.MMFileList[enemy.FileListIndex()].Data[enemy.ActorInitOffset() + 7] &= 0x8F; 
            }
        }


        public static void FlattenPitchRoll(int roomFID, int actorAddr, int actorIndex)
        {
            // the bottom 3 bits are time flags
            // I like how CM lists only three bits, but pretends like theres nothing in the gap
            // obviously theres something related to time spawns there
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 8] = 0; // x rot
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 9] &= 0x7F; // x rot
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 12] = 0; // z rot
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 13] &= 0x7F; // z rot
        }
        public static void SetX(int roomFID, int actorAddr, int actorIndex, int x)
        {
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 2] = (byte)((x >> 8) & 0xFF); // x pos
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 3] = (byte)(x & 0xFF);        // x pos
        }
        public static void SetHeight(int roomFID, int actorAddr, int actorIndex, int height)
        {
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 4] = (byte)((height >> 8) & 0xFF); // y pos
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 5] = (byte)(height & 0xFF);        // y pos
        }
        public static void SetZ(int roomFID, int actorAddr, int actorIndex, int z)
        {
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 6] = (byte)((z >> 8) & 0xFF); // x pos
            RomData.MMFileList[roomFID].Data[actorAddr + (actorIndex * 16) + 7] = (byte)(z & 0xFF);        // x pos
        }

        public static void FixSpawnLocations()
        {
            /// in Enemizer some spawn locations are noticably buggy
            ///   example: one of the eeno in north termina field is too high, 
            ///    we never notice because it falls to the ground before we can get there normally
            ///    but if its a stationary enemy, like a dekubaba, it hovers in the air

            // todo: rewrite this so functions take care of actoraddr and index

            var terminaFieldRool0FID = GameObjects.Scene.TerminaField.FileID() + 1;
            RomUtils.CheckCompressed(terminaFieldRool0FID); // safety first
            var terminaFieldSceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.TerminaField.FileID());
            var terminaFieldActorAddr = RomData.SceneList[terminaFieldSceneIndex].Maps[0].ActorAddr;
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 144, -245);  // fixes the eeno that is way too high above ground
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 16, -209);  // fixes the eeno that is too high above ground (bombchu explode)
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 17, -185);  // fixes the eeno that is too high above ground (bombchu explode)
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 60, -60);  // fixes the blue bubble that is too high
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 107, -280);  // fixes the leever spawn is too low (bombchu explode)
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 110, -280);  // fixes the leever spawn is too low (bombchu explode)
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 121, -280);  // fixes the leever spawn is too low (bombchu explode)
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 153, -280);  // fixes the leever spawn is too low (bombchu explode)

            // have to fix the two wolfos spawn in twin islands that spawn off scew, 
            //   redead falls through the floor otherwise
            // room 0, actors 27 and 28
            var twinIslandsRoom0FID = GameObjects.Scene.TwinIslands.FileID() + 1;
            RomUtils.CheckCompressed(twinIslandsRoom0FID);
            var sceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.TwinIslands.FileID());
            var twinIslandsActorAddr = RomData.SceneList[sceneIndex].Maps[0].ActorAddr;
            FlattenPitchRoll(twinIslandsRoom0FID, twinIslandsActorAddr, 26);
            FlattenPitchRoll(twinIslandsRoom0FID, twinIslandsActorAddr, 27);

            // the dinofos spawn is near the roof in woodfall and secret shrine
            var woodfallRoom7FID = GameObjects.Scene.WoodfallTemple.FileID() + 8;
            RomUtils.CheckCompressed(woodfallRoom7FID);
            sceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.WoodfallTemple.FileID());
            var woodfallActorAddr = RomData.SceneList[sceneIndex].Maps[7].ActorAddr;
            SetHeight(woodfallRoom7FID, woodfallActorAddr, 0, -1208);

            var stoneTowerTempleRoom0FID = GameObjects.Scene.StoneTowerTemple.FileID() + 1;
            RomUtils.CheckCompressed(stoneTowerTempleRoom0FID);
            var stoneTowerTempleSceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.StoneTowerTemple.FileID());
            var stoneTowerTempleActorAddr = RomData.SceneList[stoneTowerTempleSceneIndex].Maps[0].ActorAddr;
            // move the bombchu in the first stonetowertemple room 
            //   backward several feet from the chest, so replacement cannot block the chest
            SetZ(stoneTowerTempleRoom0FID, stoneTowerTempleActorAddr, 3, -630);
            // biobaba in the right room spawns under the bridge, if octarock it pops up through the tile, move to the side of the bridge
            SetX(stoneTowerTempleRoom0FID + 3, stoneTowerTempleActorAddr, 19, 1530);

            // testing using the given objects instead of the functions, add height to postboxes in SCT
            //var sctRoom0FID = GameObjects.Scene.SouthClockTown.FileID();
            var sctscene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.SouthClockTown.FileID());
            //sctscene.Maps[0].Actors[47].p.y += 100; // broken, does not work
            //SetHeight(GameObjects.Scene.SouthClockTown.FileID() + 1, sctscene.Maps[0].ActorAddr, 47, 100); // works, so the actor values aren't being saved either this late or at all

        }

        private static void FixScarecrowTalk()
        {
            /// scarecrow breaks if you try to teach him a song anywhere where he normally does not exist
            if (!EnemyList.Contains(GameObjects.Actor.Scarecrow))
            {
                return;
            }

            // turn this one jal into zeros
            //RomData.MMFileList[GameObjects.Actor.Scarecrow.FileListIndex()].Data[0x1980] = 0;
            //RomData.MMFileList[GameObjects.Actor.Scarecrow.FileListIndex()].Data[0x1981] = 0;
            //RomData.MMFileList[GameObjects.Actor.Scarecrow.FileListIndex()].Data[0x1982] = 0;
            //RomData.MMFileList[GameObjects.Actor.Scarecrow.FileListIndex()].Data[0x1983] = 0;
        }

        /// <summary>
        /// Moves the deku baba in souther swamp
        ///   why? beacuse they are positioned in the elbow and its really obvious when they spawn/despawn on room swap
        ///   its already weird how they aren't aligned in vanilla, imo
        /// </summary>
        /// <param name="scene"></param>
        public static void FixSouthernSwampDekuBaba(Scene scene)
        {
            if (scene.SceneEnum != GameObjects.Scene.SouthernSwamp)
            {
                return;
            }

            // because this room is already borderline lag fest, turn one into a lillypad
            // actor 7 is the furthest back in the cave, unreachable
            scene.Maps[0].Actors[6].n = 0x1B9; // lilypad actor
            scene.Maps[0].Actors[6].actor = GameObjects.Actor.Lillypad; // lilypad actor
            scene.Maps[0].Actors[6].p.x = 581; // placement: toward back wall behind tourist center
            scene.Maps[0].Actors[6].p.y = 0;
            scene.Maps[0].Actors[6].p.z = 790;
            scene.Maps[0].Actors[6].v = 0;

            scene.Maps[0].Actors[4].p.x = 2020; // placement: to the right as you approach witches, next to tree
            scene.Maps[0].Actors[4].p.y = 22;
            scene.Maps[0].Actors[4].p.z = 300;

            scene.Maps[2].Actors[2].p.x = 2910; // placement: between the bushes along the wall
            scene.Maps[2].Actors[2].p.y = 14;
            scene.Maps[2].Actors[2].p.z = -1075;

            scene.Maps[2].Actors[3].p.x = 4240; // placement: near waterfall
            scene.Maps[2].Actors[3].p.y = -2;
            scene.Maps[2].Actors[3].p.z = -1270;

        }

        public static void EmptyOrFreeActor(Enemy enemy, Random rng, List<Enemy> currentRoomActorList, List<GameObjects.Actor> sceneAcceptableReplacements)
        {
            /// returns an actor that is either an empty actor or a free actor that can be placed here beacuse it doesn't require a new unique object

            // check the old enemy for available co-actors,
            // remove if those already exist in the list at max size
            var acceptableReplacements = sceneAcceptableReplacements;

            // if we roll a dice, we get a free actor, an actor that can already exists in memory
            //   else, its empty, replace with empty actor
            if (rng.Next(10) < 5) // 50% chance for now of considering adding, later user set or per-enemy type even
            {
                // randomly sort list of available enemies
                // check the enemy does not already exist in limit
                // place enemy
                int randomStart = rng.Next(acceptableReplacements.Count);
                for (int attempt = 0; attempt < acceptableReplacements.Count; ++attempt)
                {
                    int listIndex = (randomStart + attempt) % acceptableReplacements.Count;
                    var testEnemy = acceptableReplacements[listIndex];
                    var testEnemeyCompatibleVariants = ((GameObjects.Actor) enemy.Actor).CompatibleVariants(testEnemy, rng, enemy.Variables[0]);
                    if (testEnemeyCompatibleVariants == null)
                    {
                        continue;  // no way to use this enemy, skip
                    }

                    var enemyHasMaximums = testEnemy.HasVariantsWithRoomLimits();
                    var acceptableVariants = new List<int>();

                    if (enemyHasMaximums)
                    {
                        var enemiesInRoom = currentRoomActorList.FindAll(u => u.Actor == (int)testEnemy);
                        if (enemiesInRoom.Count > 0)  // only test for specific variants if there are already some in the room
                        {
                            // find variant that is not maxed out
                            foreach (var variant in testEnemeyCompatibleVariants)
                            {
                                // if the varient limit has not been reached
                                var variantMax = testEnemy.VariantMaxCountPerRoom(variant);
                                var variantCount = enemiesInRoom.Count(u => u.Variables[0] == variant); ;
                                if (variantCount < variantMax)
                                {
                                    acceptableVariants.Add(variant);
                                }
                            }
                        }
                        else
                        {
                            acceptableVariants = testEnemeyCompatibleVariants;
                        }
                    }
                    else
                    {
                        acceptableVariants = testEnemeyCompatibleVariants;
                    }

                    if (acceptableVariants.Count > 0)
                    {
                        int randomVariant = acceptableVariants[rng.Next(acceptableVariants.Count)];
                        enemy.Actor = (int)testEnemy;
                        enemy.Variables[0] = randomVariant;
                        return;
                    }
                }
            }

            enemy.Actor = (int) GameObjects.Actor.Empty;
            enemy.Variables[0] = 0;
        }

        public static void FixSpecificLikeLikeTypes()
        {
            /// some likelikes dont follow the normal water/ground type variety, so they should be switched to match for replacement

            var coastScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.GreatBayCoast.FileID());
            // coast: shallow water likelike along the pillars is ground, should be water
            coastScene.Maps[0].Actors[21].v = 2;
            // coast: bottom of the ocean east is ground, should be water
            coastScene.Maps[0].Actors[24].v = 2;
            // coast: tidepool likelike is water, and also too shallow for water enemy
            coastScene.Maps[0].Actors[20].v = 2;
        }

        public static List<Enemy> GetMatchPool(List<Enemy> oldActors, Random random, Scene scene, List<GameObjects.Actor> reducedCandidateList, bool containsFairyDroppingEnemy)
        {
            List<Enemy> enemyMatchesPool = new List<Enemy>();

            // we cannot currently swap out specific enemies, so if ONE must be killable, all shared enemies must
            //  eg: one of the dragonflies in woodfall must be killable in the map room, so all in the dungeon must since we cannot isolate
            bool MustBeKillable = oldActors.Any(u => u.MustNotRespawn);

            // moved up higher, because we can scan once per scene
            if (containsFairyDroppingEnemy) // scene.SceneEnum.GetSceneFairyDroppingEnemies().Contains((GameObjects.Actor) oldActors[0].Actor))
            {
                /// special case: armos does not drop stray fairies, and I dont know why
                reducedCandidateList.Remove(GameObjects.Actor.Armos);
                MustBeKillable = true; // we dont want respawning or unkillable enemies here either
            }

            // this could be per-enemy, but right now its only used where enemies and objects match, so to save cpu cycles do it once per object not per enemy
            foreach(var enemy in scene.SceneEnum.GetBlockedReplacementActors((GameObjects.Actor)oldActors[0].Actor))
            {
                reducedCandidateList.Remove(enemy);
            }

            // todo does this NEED to be a double loop? does anything change per enemy copy that we should worry about?
            foreach (var oldEnemy in oldActors) // this is all copies of an enemy in a scene, so all bo or all guay
            {
                // the enemy we got from the scene has the specific variant number, the general game object has all
                var enemyMatch = (GameObjects.Actor) oldEnemy.Actor;
                foreach (var enemy in reducedCandidateList)
                {
                    var compatibleVariants = enemyMatch.CompatibleVariants(enemy, random, oldEnemy.Variables[0]);
                    if (compatibleVariants == null)
                    {
                        continue;
                    }

                    // TODO here would be a great place to test if the requirements to kill an enemy are met with given items

                    // TODO re-enable and test stationary, which is currently missing
                    //&& (enemy.Stationary == enemyMatch.Stationary)&& )
                    if ( ! enemyMatchesPool.Any(u => u.Actor == (int) enemy))
                    {
                        var newEnemy = enemy.ToEnemy();
                        if (MustBeKillable)
                        {
                            newEnemy.Variables = enemy.KillableVariants(compatibleVariants); // reduce to available
                            if (newEnemy.Variables.Count == 0)
                            {
                                continue; // can't put this enemy here: it has no non-respawning variants
                            }
                        }
                        else
                        {
                            newEnemy.Variables = compatibleVariants;
                        }
                        enemyMatchesPool.Add(newEnemy);
                    }
                }
            }

            return enemyMatchesPool;
        }

        public static void SwapSceneEnemies(OutputSettings settings, Scene scene, int seed)
        {
            // spoiler log already written by this point, for now making a brand new one instead of appending
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str);
                log.AppendLine(str);
            }
            void FlushLog()
            {
                EnemizerLogMutex.WaitOne(); // with paralel, thread safety
                using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.Write(log);
                }
                EnemizerLogMutex.ReleaseMutex();
            }

            FixSouthernSwampDekuBaba(scene);

            DateTime startTime = DateTime.Now;

            var sceneEnemies = GetSceneEnemyActors(scene);
            if (sceneEnemies.Count == 0)
            {
                return; // if no enemies, no point in continuing
            }

            WriteOutput("time to get scene enemies: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");
            List<int>   sceneObjects = GetSceneEnemyObjects(scene);
            WriteOutput(" time to get scene objects: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            WriteOutput("For Scene: [" + scene.SceneEnum.ToString() + "] with fid: " + scene.File + ", with sid: 0x"+ scene.Number.ToString("X2"));
            WriteOutput(" time to find scene name: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            // if actor doesn't exist but object does, probably spawned by something else, remove from actors to randomize
            // TODO check for side objects that no longer need to exist and replace with possible alt objects
            foreach (int obj in sceneObjects.ToList())
            {
                if ( ! (EnemyList.FindAll(u => u.ObjectIndex() == obj)).Any( u => sceneEnemies.Any( w => w.Actor == (int) u)))
                { 
                    sceneObjects.Remove(obj);
                }
            }
            WriteOutput(" time to finish removing unnecessary objects: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            // special case: likelikes need to be split into two objects because ground and water share one object 
            // but no other enemeies work as dual replacement
            if ((scene.File == GameObjects.Scene.ZoraCape.FileID() || scene.File == GameObjects.Scene.GreatBayCoast.FileID())
                && sceneObjects.Contains(GameObjects.Actor.LikeLike.ObjectIndex()))
            {
                // add shield object to list of objects we can swap out
                sceneObjects.Add(GameObjects.Actor.LikeLikeShield.ObjectIndex());
                // generate a a candidate list for the second likelike
                for( int i = 0; i < sceneEnemies.Count; ++i)
                {
                    if ( sceneEnemies[i].Actor == (int)GameObjects.Actor.LikeLike
                        && GameObjects.Actor.LikeLike.IsGroundVariant(sceneEnemies[i].Variables[0]))
                    {
                        sceneEnemies[i].Object = GameObjects.Actor.LikeLikeShield.ObjectIndex();
                    }
                }
            }

            // some scenes are blocked from having enemies, do this ONCE before GetMatchPool, which would do it per-enemy
            var sceneAcceptableEnemies = EnemyList.FindAll( u => ! u.BlockedScenes().Contains(scene.SceneEnum));
            var rng = new Random(seed + scene.File);

            // we want to check for actor types that contain fairies per-scene for speed
            var fairyDroppingActors = GetSceneFairyDroppingEnemyTypes(scene, sceneEnemies);
            // we group enemies with objects because some objects can be reused for multiple enemies, potential minor boost to variety
            var originalEnemiesPerObject = new List<List<Enemy>>(); ; // outer layer is per object
            var actorCandidatesLists = new List<List<Enemy>>();
            List<ValueSwap> chosenReplacementObjects;

            // get a matching set of possible replacement objects and enemies that we can use
            // moving out of loop, this should be static except for RNG changes, which we can leave static per seed
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                // get a list of all enemies (in this room) from enemylist that have the same OBJECT as our object that have an actor we also have
                originalEnemiesPerObject.Add(sceneEnemies.FindAll(u => u.Object == sceneObjects[i]));
                // get a list of matching actors that can fit in the place of the previous actor
                var objectHasFairyDroppingEnemy = fairyDroppingActors.Any(u => u.ObjectIndex() == sceneObjects[i]);
                actorCandidatesLists.Add(GetMatchPool(originalEnemiesPerObject[i], rng, scene, sceneAcceptableEnemies.ToList(), objectHasFairyDroppingEnemy));
            }
            WriteOutput(" time to generate candidate list: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            int loopsCount = 0;
            while (true)
            {
                /// bogo sort, try to find an actor/object combos that fits in the space we took it out of
                
                chosenReplacementObjects = new List<ValueSwap>();
                int oldsize = 0;
                int newsize = 0;
                for (int i = 0; i < sceneObjects.Count; i++)
                {
                    //////////////////////////////////////////////////////
                    ///////// debugging: force an object (enemy) /////////
                    //////////////////////////////////////////////////////
                    /*if (scene.File == GameObjects.Scene.TerminaField.FileID()
                        && sceneObjects[i] == GameObjects.Actor.Leever.ObjectIndex())
                        //&& i == 2) // actor object number X
                    {
                        chosenReplacementObjects.Add(new ValueSwap()
                        {
                            OldV = sceneObjects[i],
                            //NewV = GameObjects.Actor.BombFlower.ObjectIndex() // good for visual
                            //NewV = GameObjects.Actor.RealBombchu.ObjectIndex() // good for detection explosion
                            NewV = GameObjects.Actor.Eeno.ObjectIndex() // good for detection explosion
                        });
                        oldsize += originalEnemiesPerObject[i][0].ObjectSize;
                        continue;
                    }*/

                    var reducedCandidateList = actorCandidatesLists[i].ToList();
                    foreach (var objectSwap in chosenReplacementObjects)
                    {
                        // remove previously used objects, remove copies to increase variety
                        reducedCandidateList.RemoveAll(u => u.Object == objectSwap.NewV);
                    }
                    if (reducedCandidateList.Count == 0) // rarely, there are no available objects left
                    {
                        newsize += 0x1000000; // should always error in the object size section
                        continue; // this enemy was starved by previous options, force error and try again
                    }

                    // get random enemy from the possible random enemy matches
                    Enemy randomEnemy = reducedCandidateList[rng.Next(reducedCandidateList.Count)];

                    // keep track of sizes between this new enemy combo and what used to be in this scene
                    if (randomEnemy.Object >= 4) // object 1 is gameplay keep, 3 is dungeon keep
                    {
                        newsize += randomEnemy.ObjectSize;
                    }
                    oldsize += originalEnemiesPerObject[i][0].ObjectSize;
                    // add random enemy to list
                    chosenReplacementObjects.Add( new ValueSwap() 
                    { 
                        OldV = sceneObjects[i],
                        NewV = randomEnemy.Object
                    });
                }

                loopsCount += 1;
                if (loopsCount >= 750) // inf loop catch
                {
                    var error = " No enemy combo could be found to fill this scene: " + scene.SceneEnum.ToString() + " w sid:" + scene.Number.ToString("X2");
                    WriteOutput(error);
                    WriteOutput("Failed Candidate List:");
                    foreach (var list in actorCandidatesLists)
                    {
                        WriteOutput("enemy:");
                        foreach (var match in list)
                        {
                            WriteOutput(" Enemytype candidate: " + match.Name + " with vars: " + match.Variables[0].ToString("X2"));
                        }
                    }
                    FlushLog();
                    throw new Exception(error);
                }

                if (newsize <= oldsize || newsize < scene.SceneEnum.GetSceneObjLimit()) 
                {
                    //this should take into account map/scene size and size of all loaded actors...
                    //not really accurate but *should* work for now to prevent crashing
                    WriteOutput("Ratio of new to old scene object volume: " + ((float)newsize / (float)oldsize) + " size:" + newsize.ToString("X2"));
                    break;
                }

            }
            WriteOutput(" Loops used for match candidate: " + loopsCount);
            WriteOutput(" time to finish finding matching population: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            // some actors don't require unique objects, they can use objects that are generally loaded, we can use these almost anywhere
            var sceneIsDungeon = scene.SceneEnum.IsDungeon();
            var sceneFreeActors = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                                      .Where(u => (u.ObjectIndex() == 1 || (!sceneIsDungeon && u.ObjectIndex() == 2) || (sceneIsDungeon && u.ObjectIndex() == 3))
                                               && ! u.BlockedScenes().Contains(scene.SceneEnum)
                                               && (u.IsEnemyRandomized() || (ACTORSENABLED && u.IsActorRandomized())))
                                               .ToList();

            var chosenReplacementEnemies = new List<Enemy>();
            var previousyAssignedActor = new List<GameObjects.Actor>();

            for (int objCount = 0; objCount < chosenReplacementObjects.Count; objCount++)
            {
                var temporaryMatchEnemyList = new List<Enemy>();
                foreach (var oldEnemy in originalEnemiesPerObject[objCount].ToList())
                {
                    int randomSubmatch;
                    List<Enemy> subMatches = actorCandidatesLists[objCount].FindAll(u => u.Object == chosenReplacementObjects[objCount].NewV);

                    // this isn't really a loop, 99% of the time it matches on the first loop
                    // leaving this for now because its faster than shuffling the list even if it looks stupid
                    // eventually: replace with .Single().Where(conditions)
                    while (true)
                    {
                        loopsCount += 1;
                        if (loopsCount >= 1000) // inf loop check
                        {
                            throw new Exception(" No enemy combo could be found to fill this scene: " + scene.File);
                        }

                        /// looking for a list of objects for the actors we chose that fit the actor types
                        //|| (oldEnemy.Type == subMatches[randomSubmatch].Type && rng.Next(5) == 0)
                        //  //&& oldEnemy.Stationary == subMatches[randomSubmatch].Stationary)
                        randomSubmatch = rng.Next(subMatches.Count);
                        if (oldEnemy.Type == subMatches[randomSubmatch].Type || (subMatches.FindIndex(u => u.Type == oldEnemy.Type) == -1))
                        {
                            break;
                        }
                    }

                    var newEnemy = oldEnemy;
                    newEnemy.Actor = subMatches[randomSubmatch].Actor;
                    newEnemy.Variables[0] = subMatches[randomSubmatch].Variables[rng.Next(subMatches[randomSubmatch].Variables.Count)];

                    // setting grottos testing
                    /*if (newEnemy.Actor == (int)GameObjects.Actor.GrottoHole)
                    {
                        //newEnemy.Rotation.x = 0;
                        //newEnemy.Rotation.y = 0;
                        newEnemy.Rotation.z = 0x301;
                    }*/

                    temporaryMatchEnemyList.Add(newEnemy);
                    if ( ! previousyAssignedActor.Contains((GameObjects.Actor)newEnemy.Actor))
                    {
                        previousyAssignedActor.Add((GameObjects.Actor)newEnemy.Actor);
                    }
                }

                // enemies can have max per room variants, if these show up we should cull the extra over the max
                var restrictedEnemies = previousyAssignedActor.FindAll(u => u.HasVariantsWithRoomLimits());
                foreach( var problemEnemy in restrictedEnemies)
                {
                    // we need to split enemies per room
                    for (int roomIndex = 0; roomIndex < scene.Maps.Count; ++roomIndex)
                    {
                        var roomEnemies = temporaryMatchEnemyList.FindAll(u => u.Room == roomIndex);
                        var roomIsClearPuzzleRoom = scene.SceneEnum.IsClearEnemyPuzzleRoom(roomIndex);
                        var enemyCullList = new List<Enemy>();

                        // for limited variants, reduce size
                        var limitedVariants = problemEnemy.Variants().FindAll(u => problemEnemy.VariantMaxCountPerRoom(u) >= 0);
                        foreach (var variant in limitedVariants)
                        {
                            var roomEnemiesWithVariant = roomEnemies.FindAll(u => u.Variables[0] == variant);
                            if (roomEnemiesWithVariant != null && roomEnemiesWithVariant.Count > 0)
                            {
                                int max = problemEnemy.VariantMaxCountPerRoom(variant);
                                int removed = 0;
                                if (roomIsClearPuzzleRoom) // make sure any enemy that has to drop a fairy is removed first
                                {
                                    // weirdly there isn't a single room in the game that has both a clear enemy to get item puzzle and a fairy dropping enemy, so we can reuse
                                    var randomEnemy = roomEnemiesWithVariant[rng.Next(roomEnemiesWithVariant.Count)];
                                    roomEnemiesWithVariant.Remove(randomEnemy); // leave at least one enemy alone
                                    removed++;
                                    WriteOutput("Clear room: removing random enemy to prevent room being empty: " + randomEnemy.Name + ", index" + randomEnemy.RoomActorIndex);
                                }
                                else
                                {
                                    // if not a clear room, all protected enemies are fairy enemies or specific enemies, cannot remove any
                                    foreach (var protectedEnemy in roomEnemiesWithVariant.Where(u => u.MustNotRespawn == true).ToList())
                                    {
                                        roomEnemiesWithVariant.Remove(protectedEnemy);
                                        removed++;
                                        WriteOutput("Protected Enemy: " + protectedEnemy.Name + ", index:" + protectedEnemy.RoomActorIndex);
                                    }
                                }
                                // remove random enemies until max for variant is reached
                                for (int i = removed; i < max && i < roomEnemiesWithVariant.Count; ++i)
                                {
                                    roomEnemiesWithVariant.Remove(roomEnemiesWithVariant[rng.Next(roomEnemiesWithVariant.Count)]);
                                }
                                // kill the rest of variant X since max is reached
                                foreach (var enemy in roomEnemiesWithVariant)
                                {
                                    var enemyIndex = temporaryMatchEnemyList.IndexOf(enemy);
                                    EmptyOrFreeActor(enemy, rng, temporaryMatchEnemyList, sceneFreeActors);
                                }
                                WriteOutput(" in room " + roomIndex + ", removing extra variants: " + variant.ToString("X2") + " for enemy: " + problemEnemy.ToString());
                            }
                        }
                    }
                }
                // this was here because I believed it was double dipping
                // TODO: check if its doing anything
                previousyAssignedActor.Clear();

                // print debug enemy locations
                // moved here later because room limits can change the values
                for (int i = 0; i < temporaryMatchEnemyList.Count; i++)
                {
                    WriteOutput("Old Enemy actor:[" 
                        + originalEnemiesPerObject[objCount][i].Name 
                        + "] was replaced by new enemy: [" 
                        + ((GameObjects.Actor)temporaryMatchEnemyList[i].Actor).ToString() 
                        + "] with variant: [" 
                        + temporaryMatchEnemyList[i].Variables[0].ToString("X2") + "]");
                }

                // add temp list back to chosenRepalcementEnemies
                chosenReplacementEnemies.AddRange(temporaryMatchEnemyList);
            }

            // special case test: woodfall snapper rando kills gecko, lets see if we can fix
            //var woodfallScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.WoodfallTemple.FileID());
            // does not work, probably needs a snapper actor too not just object
            // if we need A snapper actor/object, we could force that condition, snapper could be on dinofos, snapper, or bo, dekubaba if I can figure out what that even means
            /*if (scene.SceneEnum == GameObjects.Scene.WoodfallTemple)
            {
                
                // I dont know if this is working, as actor + obj spawns nothing
                // todo try unrandomizing the snapper object to snapper, whatever enemy we need does NOT need that object in here
                scene.Maps[8].Objects[2]  = 0x1A6; // blue warp obj to snapper
                scene.Maps[8].Objects[17] = 0x1A6; // old snapper spot, whatever new enemy exists wont be in this room anyway
                scene.Maps[8].Objects[16] = 0x1A6; // dekubaba

                scene.Maps[8].Objects[16] = 0x1A6; // why is there a fucking skullwalla in this room????
                scene.Maps[8].Objects[13] = 0x1A6; // giant bee? redundant just in case for testing, this might kill the hive if it does not exist

                // I know the actor changes are working because I could add a snapper to this room if snapper was still in the rest of the temple
                //scene.Maps[8].Actors[3].n = 0x1BA; // one of the extra flowers to snapper
                //scene.Maps[8].Actors[3].v = 0; // regular snapper variety

                scene.Maps[8].Actors[3].n = 0x144; // one of the extra flowers to snapper mini0-boss
                scene.Maps[8].Actors[3].v = 0; // vars of miniboss unknown
            }*/

            /*if (scene.File == GameObjects.Scene.WoodfallTemple.FileID())
            {
                foreach (var room in scene.Maps)
                {
                    for (int objIndex = 0; objIndex < room.Objects.Count; objIndex++)
                    {
                        if (room.Objects[objIndex] == 0x1EB)
                        {
                            room.Objects[objIndex] = 0x1A6; // swap to snapper
                        }
                    }
                }
            }*/

            // problem: if we remove snapper as an obj gekko does not spawn
            // attempted fix: replace one of the other unused obj in that room with snapper to he still spawns
            // in room 9
            //var woodfallScene = RomData.SceneList.Find(u => u.File == GameObjects.Scene.WoodfallTemple.FileID());
            /*foreach (var room in woodfallScene.Maps)
            {
                for (int objIndex = 0; objIndex < room.Objects.Count; objIndex++)
                {
                    if (room.Objects[objIndex] == 0x1EB)
                    {
                        room.Objects[objIndex] = 0x1A6; // swap to snapper
                    }
                }
            }*/
            // set 
            //woodfallScene.Maps[8].Objects[13] = 0x1A6;
            //woodfallScene.Maps[8].ActorAddr[13] = 0x1A6;

            SetSceneEnemyActors(scene, chosenReplacementEnemies);
            SetSceneEnemyObjects(scene, chosenReplacementObjects);
            SceneUtils.UpdateScene(scene);
            WriteOutput( " time to complete randomizing scene: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");
            FlushLog();
        }

        public static void ShuffleEnemies(OutputSettings settings,Random random)
        {
            try
            {
                // these are: cutscene map, town and swamp shooting gallery, 
                // sakons hideout, and giants chamber (shabom)
                // adding ocean spiderhouse because its always bo, nothing else fits, but it can lag enemizer
                int[] SceneSkip = new int[] { 0x08, 0x20, 0x24, 0x4F, 0x69, 0x28 };

                ReadEnemyList();
                SceneUtils.ReadSceneTable();
                SceneUtils.GetMaps();
                SceneUtils.GetMapHeaders();
                SceneUtils.GetActors();
                FixSpecificLikeLikeTypes();

                // if using parallel, move biggest scenes to the front so that we dont get stuck waiting at the end for one big scene with multiple dead cores idle
                // biggest is on the right, because its put at the front last
                // this should be all scenes that took > 500ms on Isghj's computer during alpha ~dec15
                //  this is old, should be re-evaluated with different code
                var newSceneList = RomData.SceneList;
                foreach (var sceneIndex in new int[]{ 1442, 1353, 1258, 1358, 1449, 1291, 1224,  1522, 1388, 1165, 1421, 1431, 1241, 1222, 1330, 1208, 1451, 1332, 1446, 1310 }){
                    var item = newSceneList.Find(u => u.File == sceneIndex);
                    newSceneList.Remove(item);
                    newSceneList.Insert(0, item);
                }
                int seed = random.Next(); // order is up to the cpu scheduler, to keep these matching the seed, set them all to start at the same value

                Parallel.ForEach(newSceneList.AsParallel().AsOrdered(), scene =>
                //foreach (var scene in RomData.SceneList) if (!SceneSkip.Contains(scene.Number))
                {
                    if (!SceneSkip.Contains(scene.Number))
                    {
                        var previousThreadPriority = Thread.CurrentThread.Priority;
                        Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;// do not SLAM
                        SwapSceneEnemies(settings, scene, seed);
                        Thread.CurrentThread.Priority = previousThreadPriority;
                    }
                });

                LowerEnemiesResourceLoad();

                void PrintActorInitFlags(string name, byte[] dataBlob, int actorInitLoc){
                    Debug.WriteLine("Printing actor: " + name);
                    Debug.WriteLine(dataBlob[actorInitLoc].ToString("X2") + 
                                    dataBlob[actorInitLoc + 1].ToString("X2") + " < actor id");
                    Debug.WriteLine(dataBlob[actorInitLoc + 2].ToString("X2") + " < type");
                    Debug.WriteLine(dataBlob[actorInitLoc + 3].ToString("X2") + " < unk ?");
                    Debug.WriteLine(dataBlob[actorInitLoc + 4].ToString("X2") + " < init var first byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 5].ToString("X2") + " < init var second byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 6].ToString("X2") + " < init var third byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 7].ToString("X2") + " < init var last byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 8].ToString("X2") +
                                    dataBlob[actorInitLoc + 9].ToString("X2") + " < object id");
                }

                // bits (from 0) 4 should be always update, 5 should be always draw, 6 should be no cull
                //RomData.MMFileList[actor.FileListIndex()].Data[actor.ActorInitOffset() + 7] |= 0x80; // "invisible" is weirder than you think

                /*foreach (var a2 in Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.IsEnemyRandomized() &&  u.ActorInitOffset() > 100)
                            .ToList())
                {
                    RomUtils.CheckCompressed(a2.FileListIndex());
                    // to test invisibility in all enemies
                    //RomData.MMFileList[a2.FileListIndex()].Data[a2.ActorInitOffset() + 7] |= 0x80; // test invisible
                }*/

                // todo: bombiwa
                var actor = GameObjects.Actor.PoeSisters;
                RomUtils.CheckCompressed(actor.FileListIndex());
                PrintActorInitFlags(actor.ToString(), RomData.MMFileList[actor.FileListIndex()].Data, actor.ActorInitOffset());
                //RomData.MMFileList[actor.FileListIndex()].Data[actor.ActorInitOffset() + 7] |= 0x80; // test invisible

                FixSpawnLocations(); // some spawns need to be fixed, enemizer brings out their bugginess
                FixScarecrowTalk();

            }
            catch (Exception e)
            {
                string innerExceptions = e.InnerException != null ? e.InnerException.ToString() : "";
                throw new Exception("Enemizer failed for this seed, please try another seed.\n\n" + e.Message + "\n" + innerExceptions);
            }
        }

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


        public static void DisableEnemyCombatMusic(bool weakEnemiesOnly = false)
        {
            /// each enemy has one int flag that contains a single bit that enables combat music
            /// to get these values I used the starting rom addr of the enemy actor
            ///  searched the ram for the actor overlay table that has rom and ram per actor,
            ///  there it lists the actor init var ram and actor ram locations, diff, apply to rom start
            ///  the combat music flag is part of the seventh byte of the actor init variables, but our fuction knows this

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
                GameObjects.Actor.Garo,
                GameObjects.Actor.PoeSisters
            }.ToList();

            var disableList = weakEnemyList;// Only ? weakEnemyList : weakEnemyList.Concat(annoyingEnemyList);

            foreach (var enemy in disableList)
            {
                DisableCombatMusicOnEnemy(enemy);
            }
        }
    }

}