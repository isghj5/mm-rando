using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
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
            // but also used for actor variable values, ergo any int values that need to be swapped
            public int OldV;
            public int NewV;
        }
         
        //private static List<Enemy> EnemyList { get; set; }
        private static List<GameObjects.Actor> EnemyList { get; set; }
        private static Mutex EnemizerLogSemaphore = new Mutex();

        public static void ReadEnemyList()
        {
            EnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.IsEnemyRandomized() || u.IsActorRandomized()) // both
                            //.Where(u => u.IsEnemyRandomized()) // enemizer only
                            .ToList();
        }

        public static List<Enemy> GetSceneEnemyActors(Scene scene)
        {
            /// this is separate from object because actors and objects are a different list in the scene data

            var enemyList = new List<Enemy>();
            for (int mapNumber = 0; mapNumber < scene.Maps.Count; mapNumber++ ) // var sceneMap in scene.Maps)
            {
                foreach (var mapActor in scene.Maps[mapNumber].Actors)
                {
                    var matchingEnemy = EnemyList.Find(u => (int) u == mapActor.n);
                    if (matchingEnemy > 0) {
                        var listOfAcceptableVariants = matchingEnemy.Variants();
                        if ( !matchingEnemy.ScenesRandomizationExcluded().Contains(scene.Number)
                            && listOfAcceptableVariants.Contains(mapActor.v))
                        {
                            var newEnemy = matchingEnemy.ToEnemy();
                            //mapActor.objectIndex = 
                            newEnemy.Variables = new List<int> { mapActor.v };
                            //var matchingScene = ((GameObjects.Scene[])Enum.GetValues(typeof(GameObjects.Scene))).ToList().Find(u => u.Id() == scene.Number);
                            newEnemy.MustNotRespawn = scene.SceneEnum.IsClearEnemyPuzzleRoom(mapNumber);
                            newEnemy.Room = mapNumber;
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
                       && !matchingEnemy.ScenesRandomizationExcluded().Contains(scene.Number)) // not excluded from being extracted from this scene
                    {
                        objList.Add(matchingEnemy.ObjectIndex());
                    }
                }
            }
            return objList;
        }

        public static void SetSceneEnemyActors(Scene scene, List<ValueSwap[]> A)
        {
            foreach (var sceneRoom in scene.Maps)
            foreach (var roomActor in sceneRoom.Actors)
            { 
                var valueSwapActor = A.Find(u => u[0].OldV == roomActor.n);
                if (valueSwapActor != null)
                {
                    // todo: rewrite n and v, since I have no idea what those are or why they are exactly teh same
                    roomActor.n = valueSwapActor[0].NewV;
                    roomActor.v = valueSwapActor[1].NewV;
                    A.Remove(valueSwapActor);
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

        // adjust spawn cords

        public static void FixSpawnLocations()
        {
            /// in Enemizer some spawn locations suck, we notice them being wrong only when we change them

            // maybe break this up a bit
            void FlattenPitchRoll(int fid, int actorAddr, int actorIndex)
            {
                // the bottom 3 bits are time flags
                // I like how CM lists only three bits, but pretends like theres nothing in the gap
                // obviously theres something related to time spawns there
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 8] = 0; // x rot
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 9] &= 0x7F; // x rot
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 12] = 0; // z rot
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 13] &= 0x7F; // z rot
            }
            void SetHeight(int fid, int actorAddr, int actorIndex, int height)
            {
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 4] = (byte) ((height >> 8) & 0xFF); // y pos
                RomData.MMFileList[fid].Data[actorAddr + (actorIndex * 16) + 5] = (byte)( height & 0xFF);        // y pos
            }

            // TODO have to fix the stonetower bombchu to not spawn right in front of the chest

            var terminaFieldRool0FID = GameObjects.Scene.TerminaField.FileID() + 1;
            RomUtils.CheckCompressed(terminaFieldRool0FID); // safety first
            var terminaFieldSceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.TerminaField.FileID());
            var terminaFieldActorAddr = RomData.SceneList[terminaFieldSceneIndex].Maps[0].ActorAddr;
            // room 0 actor 145: Y to -245
            // currently broken: too high
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 144, -245);  // fixes the eeno that is way too high above ground
            // room 0 actor 61: Y to -60
            SetHeight(terminaFieldRool0FID, terminaFieldActorAddr, 60, -60);  // fixes the blue bubble that is too high


            // have to fix the two wolfos spawn in twin islands that spawn off scew
            // room 0, actors 27 and 28
            // 27: y:141, r(xyz): 0 0 0a
            // 28: y:196, r: 0 0 0
            var twinIslandsRoom0FID = GameObjects.Scene.TwinIslands.FileID() + 1;
            RomUtils.CheckCompressed(twinIslandsRoom0FID); // safety first
            var sceneIndex = RomData.SceneList.FindIndex(u => u.File == GameObjects.Scene.TwinIslands.FileID());
            var twinIslandsActorAddr = RomData.SceneList[sceneIndex].Maps[0].ActorAddr;
            FlattenPitchRoll(twinIslandsRoom0FID, twinIslandsActorAddr, 26);  // fixes redead falling through the floor
            FlattenPitchRoll(twinIslandsRoom0FID, twinIslandsActorAddr, 27);  // fixes redead falling through the floor

        }

        public static List<Enemy> GetMatchPool(List<Enemy> oldActors, Random random, Scene scene)
        {
            List<Enemy> enemyMatchesPool = new List<Enemy>();

            // in the future I hope we can change single enemies, but for now
            // this exists up here and not in the loop because woodfall:
            //  in woodfall one dragonfly is needed for a clear-all-enemies room, but this loop handles all unique
            bool MustNotRespawn = oldActors.Any(u => u.MustNotRespawn);
            
            // todo does this NEED to be a double loop? does anything change per enemy copy that we should worry about?
            foreach (var oldEnemy in oldActors) // this is all copies of an enemy in a scene, so all bo or all guay
            {
                // the enemy we got from the scene has the specific variant number, the general game object has all
                //var enemyMatch = EnemyList.Find(u => (int) u == oldEnemy.Actor);
                var enemyMatch = (GameObjects.Actor) oldEnemy.Actor;
                foreach (var enemy in EnemyList)
                {
                    var compatibleVariants = enemyMatch.CompatibleVariants(enemy, random, oldEnemy.Variables[0]);
                    if (compatibleVariants == null)
                    {
                        continue;
                    }

                    // if peathat replaces snowhead red bubble, it lags the whole dungeon, also its hot get out of there deku
                    if (scene.File == 1241 && oldEnemy.Actor == (int) GameObjects.Actor.RedBubble
                        && (enemy == GameObjects.Actor.Peahat || enemy == GameObjects.Actor.MadShrub))
                    //if (oldEnemy.Actor == (int)GameObjects.Actor.RedBubble && scene.Number == GameObjects.Scene.SnowheadTemple.Id() && enemy == GameObjects.Actor.Peahat)
                    {
                        continue;
                    }

                    // TODO here would be a great place to test if the requirements to kill an enemy are met with given items

                    // TODO re-enable and test stationary, which is currently missing
                    //&& (enemy.Stationary == enemyMatch.Stationary)&& )
                    if ( ! enemyMatchesPool.Any(u => u.Actor == (int) enemy))
                    {
                        var newEnemy = enemy.ToEnemy();
                        if (MustNotRespawn)
                        {
                            newEnemy.Variables = enemy.NonRespawningVariants(compatibleVariants); // reduce to available
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

            // TODO rewrite so we dont need this hardcoded
            // desbrekos, the giant skelefish swarm will lag southern swamp horribly
            if (scene.File == 1358)
            {
                enemyMatchesPool.Remove(GameObjects.Actor.Desbreko.ToEnemy());
            }
            // if dinofos replaces iron knuckle, it crashes (or at least crashed for me)
            if (scene.File == 1145)
            {
                enemyMatchesPool.Remove(GameObjects.Actor.Dinofos.ToEnemy());
            }
            // chuchu in cleared swamp is crash if you approach the witch shop
            if (scene.File == 1137)
            {
                enemyMatchesPool.Remove(GameObjects.Actor.ChuChu.ToEnemy());
            }
            // if Hiploop gets replaced with with RedBubble in woodfall you can see their models below the bridges
            if (scene.File == 1362)
            {
                enemyMatchesPool.Remove(GameObjects.Actor.RedBubble.ToEnemy());
            }

            return enemyMatchesPool;
        }

        public static void SwapSceneEnemies(OutputSettings settings, Scene scene, int seed)
        {
            Random rng = new Random(seed);
            DateTime startTime = DateTime.Now;
            // spoiler log already written by this point, for now making a brand new one instead of appending
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str);
                log.AppendLine(str);
            }

            List<Enemy> sceneEnemies = GetSceneEnemyActors(scene);
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

            List<List<Enemy>> originalEnemiesPerObject = new List<List<Enemy>>(); ; // outer layer is per object
            List<List<Enemy>> matchingCandidatesLists = new List<List<Enemy>>();
            List<ValueSwap[]> chosenReplacementActors = new List<ValueSwap[]>();
            List<ValueSwap>   chosenReplacementObjects;

            // get a matching set of possible replacement objects and enemies that we can use
            // moving out of loop, this should be static except for RNG changes, which we can leave static per seed
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                // get a list of all enemies (in this room) from enemylist that have the same OBJECT as our object that have an actor we also have
                originalEnemiesPerObject.Add(sceneEnemies.FindAll(u => u.Object == sceneObjects[i]));
                // get a list of matching actors that can fit in the place of the previous actor
                matchingCandidatesLists.Add(GetMatchPool(originalEnemiesPerObject[i], rng, scene));
            }
            WriteOutput(" time to generate candidate list: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            int loopsCount = 0;
            while (true)
            {

                /// bogo sort, try to find an actor/object combos that fits in the space we took it out of
                if (loopsCount >= 700) // 10000 when we do hit 10k it just delays retry, something really wrong happens if you make it past 400
                {
                    var error = " No enemy combo could be found to fill this scene: " + scene.SceneEnum.ToString() + " w sid:" + scene.Number.ToString("X2");
                    WriteOutput(error);
                    WriteOutput("Failed Candidate List:");
                    foreach (var list in matchingCandidatesLists)
                    {
                        WriteOutput("enemy:");
                        foreach (var match in list)
                        {
                            WriteOutput(" Enemytype candidate: " + match.Name + " with vars: " + match.Variables[0]);
                        }
                    }
                    using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                    {
                        sw.WriteLine(""); // spacer
                        sw.Write(log);
                    }
                    throw new Exception(error);
                }

                chosenReplacementObjects = new List<ValueSwap>();
                int oldsize = 0;
                int newsize = 0;
                for (int i = 0; i < sceneObjects.Count; i++)
                {
                    // get random enemy from the possible random enemy matches
                    Enemy randomEnemy = matchingCandidatesLists[i][rng.Next(matchingCandidatesLists[i].Count)];
                    // keep track of sizes between this new enemy combo and what used to be in this scene
                    if (randomEnemy.Object != 1) // if always loaded, dont count it if an actor needs it
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

                // testing: force enemy redead, bombchus dont fall if you want to test position
                //if (scene.File == GameObjects.Scene.TwinIslands.FileID() && !chosenReplacementObjects.Any(u => u.NewV == 0x75)) //75 = redead
                /*if (scene.File == GameObjects.Scene.GreatBayCoast.FileID() 
                    && !chosenReplacementObjects.Any(u => u.NewV == 0x211)) // bombflower exists, 2A = bombflower
                    //&& chosenReplacementObjects[3].NewV != 0x2A) // eeno replaced with bombflower
                {
                    continue;
                }*/

                loopsCount += 1;
                if (newsize <= oldsize || newsize < scene.SceneEnum.GetSceneObjLimit() ) // DEBUG turn off for size based generation
                {
                    //this should take into account map/scene size and size of all loaded actors...
                    //not really accurate but *should* work for now to prevent crashing
                    WriteOutput("Ratio of new to old scene object volume: " + ((float)newsize / (float)oldsize) + " size:" + newsize.ToString("X2"));
                    break;
                }

            }
            WriteOutput(" Loops used for match candidate: " + loopsCount);
            WriteOutput(" time to finish finding matching population: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            Enemy emptyEnemy = GameObjects.Actor.Empty.ToEnemy();
            emptyEnemy.Variables = new List<int> { 0 };

            for (int i = 0; i < chosenReplacementObjects.Count; i++)
            {
                foreach (var oldEnemy in originalEnemiesPerObject[i].ToList())
                {
                    int randomSubmatch;
                    List<Enemy> subMatches = matchingCandidatesLists[i].FindAll(u => u.Object == chosenReplacementObjects[i].NewV);

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

                    // temp method to cut leevers down a bit, since there are FOURTYFIVE on TF
                    if (oldEnemy.Actor == (int)GameObjects.Actor.Leever
                        && (rng.Next(5) <= 3))
                    {
                        subMatches[randomSubmatch] = emptyEnemy;
                    }

                    //this is temporary, I need to think of a better way to reduce enemies intelegently per-enemy, per-room, per-event
                    if (( ! oldEnemy.MustNotRespawn && originalEnemiesPerObject[i].Count >= 5
                          && (subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.GossipStone || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.Scarecrow
                           || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.Dodongo || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.PoeSisters
                           || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.Peahat || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.BeanSeller
                           || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.Postbox || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.ButlersSon
                           || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.BigPoe || subMatches[randomSubmatch].Actor == (int) GameObjects.Actor.Dinofos))
                        && (rng.Next(5) <= 2))
                    {
                        subMatches[randomSubmatch] = emptyEnemy;
                    }

                    // actors and objects are handled with value swaps
                    // todo: turn this into something other than an array of valueswaps
                    ValueSwap newActor = new ValueSwap();
                    newActor.OldV = oldEnemy.Actor;
                    newActor.NewV = subMatches[randomSubmatch].Actor;
                    ValueSwap newVariant = new ValueSwap();
                    var randomVariantValue = rng.Next(subMatches[randomSubmatch].Variables.Count);
                    newVariant.NewV = subMatches[randomSubmatch].Variables[randomVariantValue];
                    chosenReplacementActors.Add(new ValueSwap[] { newActor, newVariant });

                    // print what enemy was and now is as debug for a scene
                    WriteOutput("Old Enemy actor:[" + oldEnemy.Name + "] with id [" + newActor.OldV +  "] was replaced by new enemy: [" + subMatches[randomSubmatch].Name + "] with variant: [" + newVariant.NewV.ToString("X2") + "]");
                }
            }

            SetSceneEnemyActors(scene, chosenReplacementActors);
            SetSceneEnemyObjects(scene, chosenReplacementObjects);
            SceneUtils.UpdateScene(scene);
            WriteOutput( " time to complete randomizing scene: " + ((DateTime.Now).Subtract(startTime).TotalMilliseconds).ToString() + "ms");

            EnemizerLogSemaphore.WaitOne(); // with paralel, thread safety
            using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename +  "_EnemizerLog.txt", append: true))
            {
                sw.WriteLine(""); // spacer
                sw.Write(log);
            }
            EnemizerLogSemaphore.ReleaseMutex();
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
                var newSceneList = RomData.SceneList;
                // if using parallel, move biggest scenes to the front so that we dont get stuck waiting at the end for one big scene with multiple dead cores doing nothing
                // biggest is on the right, because its put at the front last
                // this should be all scenes that took > 500ms on Isghj's computer during alpha
                foreach (var sceneIndex in new int[]{ 1442, 1353, 1258, 1358, 1449, 1291, 1224,  1522, 1388, 1165, 1421, 1431, 1241, 1222, 1330, 1208, 1451, 1332, 1446, 1310 }){
                    var item = newSceneList.Find(u => u.File == sceneIndex);
                    newSceneList.Remove(item);
                    newSceneList.Insert(0, item);
                }
                int seed = random.Next(); // order is up to the processor, to keep these matching the seed, set them all to start at the same value

                //foreach (var scene in RomData.SceneList) if (!SceneSkip.Contains(scene.Number))
                Parallel.ForEach(newSceneList.AsParallel().AsOrdered(), scene =>
                {
                    if (!SceneSkip.Contains(scene.Number))
                    {
                        var previousThreadPriority = Thread.CurrentThread.Priority;
                        Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;// do not SLAM
                        SwapSceneEnemies(settings, scene, seed);
                        Thread.CurrentThread.Priority = previousThreadPriority;
                    }
                });

                void PrintActorInitFlags(string name, byte[] dataBlob, int actorInitLoc){
                    Debug.WriteLine("Printing actor: " + name);
                    Debug.WriteLine(dataBlob[actorInitLoc].ToString("X2") + " < actor id");
                    Debug.WriteLine(dataBlob[actorInitLoc + 1].ToString("X2") + " < actor id");
                    Debug.WriteLine(dataBlob[actorInitLoc + 2].ToString("X2") + " < type?");
                    Debug.WriteLine(dataBlob[actorInitLoc + 3].ToString("X2") + " < type?");
                    Debug.WriteLine(dataBlob[actorInitLoc + 4].ToString("X2") + " < first byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 5].ToString("X2"));
                    Debug.WriteLine(dataBlob[actorInitLoc + 6].ToString("X2"));
                    Debug.WriteLine(dataBlob[actorInitLoc + 7].ToString("X2") + " < last byte");
                    Debug.WriteLine(dataBlob[actorInitLoc + 8].ToString("X2") + " < object id");
                    Debug.WriteLine(dataBlob[actorInitLoc + 9].ToString("X2") + " < object id");
                }

                // bits (from 0) 4 should be always update, 5 should be always draw, 6 should be no cull
                //RomData.MMFileList[actor.FileListIndex()].Data[actor.ActorInitOffset() + 7] |= 0x80; // "invisible" is weirder than you think


                foreach (var a2 in Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(u => u.IsEnemyRandomized() &&  u.ActorInitOffset() > 100)
                            .ToList())
                {
                    RomUtils.CheckCompressed(a2.FileListIndex());
                    PrintActorInitFlags(a2.ToString(), RomData.MMFileList[a2.FileListIndex()].Data, a2.ActorInitOffset());
                    // I'm just assuming that I can do this for all enemies, because I did it to all enemies I knew were issues
                    // and... I'm not even sure anything changed at all
                    RomData.MMFileList[a2.FileListIndex()].Data[a2.ActorInitOffset() + 7] &= 0x8F; // redeuce CPU test, kill bits 4-6
                    // to test invisibility in all enemies
                    //RomData.MMFileList[a2.FileListIndex()].Data[a2.ActorInitOffset() + 7] |= 0x80; // test invisible
                }


                // testing add hookshot flag to items
                var actor = GameObjects.Actor.Postbox;
                RomUtils.CheckCompressed(actor.FileListIndex());
                PrintActorInitFlags(actor.ToString(), RomData.MMFileList[actor.FileListIndex()].Data, actor.ActorInitOffset());
                RomData.MMFileList[actor.FileListIndex()].Data[actor.ActorInitOffset() + 6] |= 0x02; // test hookshotable

                //actor = GameObjects.Actor.Dinofos;
                //RomUtils.CheckCompressed(actor.FileListIndex());
                //PrintActorInitFlags(actor.ToString(), RomData.MMFileList[actor.FileListIndex()].Data, actor.ActorInitOffset());
                //RomData.MMFileList[actor.FileListIndex()].Data[actor.ActorInitOffset() + 7] |= 0x80; // test invisible

                FixSpawnLocations(); // some spawns need to be fixed, enemizer brings out their bugginess
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
                // 	[ori t3,t1,0x0005] which is [35 2B 00 05] becomes [00 00 00 00]
                ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[actorFileID].Data, 0x12BC, 0x00000000);
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
                GameObjects.Actor.SkullWallTula,
                GameObjects.Actor.ReDead,
                GameObjects.Actor.Peahat,
                GameObjects.Actor.Dodongo,
                GameObjects.Actor.Takkuri,
                GameObjects.Actor.Eyegore,
                GameObjects.Actor.IronKnuckle,
                GameObjects.Actor.Garo
            }.ToList();

            var disableList = weakEnemiesOnly ? weakEnemyList : weakEnemyList.Concat(annoyingEnemyList);

            foreach (var enemy in disableList)
            {
                DisableCombatMusicOnEnemy(enemy);
            }
        }
    }

}