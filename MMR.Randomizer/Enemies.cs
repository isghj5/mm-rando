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

        public static void ReadEnemyList()
        {
            /// read enemies.txt enemies description file
            /// todo: delete this after we move all of enemies.txt to enemies.cs enum
            EnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().Where(u => u.IsEnemyRandomized()).ToList();

            /*string[] lines = Properties.Resources.ENEMIES.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int i = 0;
            while (i < lines.Length)
            {
                if (lines[i].StartsWith("-"))
                {
                    i++;
                    continue;
                }
                Enemy e = new Enemy();
                e.Actor = Convert.ToInt32(lines[i], 16);
                e.Object = Convert.ToInt32(lines[i + 1], 16);
                e.ObjectSize = ObjUtils.GetObjSize(e.Object);
                string[] varlist = lines[i + 2].Split(',');
                for (int j = 0; j <  varlist.Length; j++)
                {
                    e.Variables.Add(Convert.ToInt32(varlist[j], 16));
                }
                e.Type = Convert.ToInt32(lines[i + 3], 16);
                e.Stationary = Convert.ToInt32(lines[i + 4], 16);
                if (lines[i + 5] != "")
                {
                    string[] selist = lines[i + 5].Split(',');
                    for (int j = 0; j < selist.Length; j++)
                    {
                        e.SceneExclude.Add(Convert.ToInt32(selist[j], 16));
                    }
                }
                EnemyList.Add(e);
                i += 6;
            }*/
        }

        public static List<Enemy> GetSceneEnemyActors(Scene scene)
        {
            var enemyList = new List<Enemy>();
            foreach (var sceneMap in scene.Maps)
            {
                foreach (var mapActor in sceneMap.Actors)
                {
                    // check if this actor should be scene excluded from this scene (not replaced)
                    //   if not, add
                    var matchingEnemy = EnemyList.Find(u => u.ActorIndex() == mapActor.n);
                    if (matchingEnemy > 0 && ! matchingEnemy.ScenesRandomizationExcluded().Contains(scene.Number))
                    {
                        var newEnemy = matchingEnemy.ToEnemy();
                        newEnemy.Variables = new List<int> { mapActor.v };
                        enemyList.Add(newEnemy);
                    }
                }
            }
            return enemyList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
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
            foreach (var sceneMap in scene.Maps)
            {
                foreach (var mapActor in sceneMap.Actors)
                {
                    var valueSwapActor = A.Find(u => u[0].OldV == mapActor.n);
                    if (valueSwapActor != null)
                    {
                        // todo: rewrite n and v, since I have no idea what those are or why they are exactly teh same
                        mapActor.n = valueSwapActor[0].NewV;
                        mapActor.v = valueSwapActor[1].NewV;
                        A.Remove(valueSwapActor);
                    }
                }
            }
        }

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

        public static List<Enemy> GetMatchPool(List<Enemy> oldActors, Random random, Scene scene)
        {
            List<Enemy> enemyMatchesPool = new List<Enemy>();
            foreach (var oldEnemy in oldActors)
            {
                // unknown why we do this, I have to assume that we want an enemy type 
                //  without the objects and variables for some reason
                var enemyMatch = EnemyList.Find(u => u.ActorIndex() == oldEnemy.Actor);
                foreach (var enemy in EnemyList)
                {
                    /*
                    // desbrekos, the giant skelefish swarm will lag southern swamp horribly
                    if (scene.File == 1358 && enemy == EnemyList[27]) // southern swamp and desbrekos
                    {
                        continue;
                    }
                    // if dinofos replaces iron knuckle, it crashes (or at least crashed for me)
                    if (scene.File == 1145 && enemy == EnemyList[06]) // graveyard but not dinofos
                    {
                        continue;
                    }
                    // if dinofos replaces iron knuckle, it crashes (or at least crashed for me)
                    if (scene.File == 1137 && enemy == EnemyList[26]) // no chuchu in cleared swamp
                    {
                        continue;
                    }
                    // if peathat in snowhead, it lags the whole dungeon
                    if (oldEnemy.Actor == (int)GameObjects.Actor.RedBubble && scene.File == 1241 && enemy == EnemyList[05]) // snowhead temple but not peahat in red bubble lava
                    {
                        continue;
                    }
                    // issue: type respawning and type flying: enemies can be both but enemies.txt can only have one per enemy
                    // so some enemies like boes and blue bubbles can be bombchus because they are listed as respawn
                    // but they can also be fly, and chu explodes instantly above ground
                    // no chus replacing: guay, blue bubble
                    if ((oldEnemy.Actor == (int)GameObjects.Actor.BlueBubble || oldEnemy.Actor == (int)GameObjects.Actor.Guay) && enemy == EnemyList[31])
                    {
                        continue;
                    }
                    // no bo replacement with bombchu in path to mountain village or snowhead exterior
                    if ((scene.File == 1451 || scene.File == 1222) && (oldEnemy.Actor == (int)GameObjects.Actor.Bo) && enemy == EnemyList[31])
                    {
                        continue;
                    }
                    */
                    // TODO here would be a great place to test if the requirements to kill an enemy are met with given items

                    // TODO re-enable and test stationary, which is currently missing
                    if ((enemy.EnemyType() == enemyMatch.EnemyType())) //&& (enemy.Stationary == enemyMatch.Stationary)&& ! enemyMatchesPool.Contains(enemy))
                    {
                        enemyMatchesPool.Add(enemyMatch.ToEnemy());
                    }

                    // random chance of not caring about stationary state
                    else if ((enemy.EnemyType() == enemyMatch.EnemyType()) 
                          && (random.Next(5) == 0) 
                          && ! enemyMatchesPool.Contains(enemyMatch.ToEnemy()))
                    {
                        enemyMatchesPool.Add(enemyMatch.ToEnemy());
                    }
                }
            }
            return enemyMatchesPool;
        }

        public static void SwapSceneEnemies(OutputSettings settings, Scene scene, Random rng)
        {
            // spoiler log already written by this point, if you want a log make a new one
            StringBuilder log = new StringBuilder();
            void WriteOutput(string str)
            {
                Debug.WriteLine(str);
                log.AppendLine(str);
            }

            var    allScenes = ((GameObjects.Scene[])Enum.GetValues(typeof(GameObjects.Scene))).ToList();
            string sceneName = allScenes.Find(u => u.GetAttribute<SceneInternalIdAttribute>().InternalId == scene.Number).ToString();
            WriteOutput("For Scene: [" + sceneName + "] with fid: " + scene.File);//

            List<Enemy> sceneEnemies  = GetSceneEnemyActors(scene);
            List<int>   sceneObjects = GetSceneEnemyObjects(scene);
            if (sceneEnemies.Count == 0 || sceneObjects.Count == 0)
            {
                return;
            }

            // if actor doesn't exist but object does, probably spawned by something else, remove from actors to randomize
            foreach (int obj in sceneObjects.ToList())
            {
                //List<Enemy> ObjectMatch = EnemyList.FindAll(u => u.Object == obj);bool exists = false;foreach (var enemy in ObjectMatch){exists |= sceneEnemies.Contains(enemy.Actor);};if (!exists)
                if ( ! (EnemyList.FindAll(u => u.ObjectIndex() == obj)).Any( u => sceneEnemies.Any( w => w.Actor == u.ActorIndex()))) //sceneEnemies.Contains(u.ActorIndex())))
                { 
                    sceneObjects.Remove(obj);
                }
            }

            List<ValueSwap[]> chosenReplacementActors = new List<ValueSwap[]>();
            List<ValueSwap>   chosenReplacementObjects;
            List<Enemy>       originalEnemies;
            List<List<Enemy>> matchingCandidates;

            // attempt random enemy combos until a combo that fits in the old slot is chosen
            int loopsCount = 0;
            while (true)
            {
                /// bogo sort, try to find an actor/object combos that fits in the space we took it out of
                chosenReplacementObjects = new List<ValueSwap>();
                originalEnemies = new List<Enemy>();
                matchingCandidates  = new List<List<Enemy>>();
                int oldsize = 0;
                int newsize = 0;
                for (int i = 0; i < sceneObjects.Count; i++)
                {
                    // get a list of all enemies from enemylist that have the same OBJECT as our object that have an actor we also have
                    //originalEnemies.Add(EnemyList.FindAll(u => ((u.ObjectIndex() == sceneObjects[i]) && (sceneEnemies.Contains(u.ActorIndex())))));
                    originalEnemies = sceneEnemies.FindAll(u => u.Object == sceneObjects[i]);  // && sceneEnemies.Any(w => w.) ); // not sure checking if the actor exists is necessary by this point
                    // get a list of matching actors that can fit in the place of the previous actor
                    matchingCandidates.Add(GetMatchPool(originalEnemies, rng, scene));
                    // get random enemy from the possible random enemy matches
                    Enemy randomEnemmy = matchingCandidates[i][rng.Next(matchingCandidates[i].Count)];
                    // keep track of sizes between this new enemy combo and what used to be in this scene
                    newsize += randomEnemmy.ObjectSize;
                    oldsize += originalEnemies[0].ObjectSize;
                    // add random enemy to list
                    //ValueSwap newObject = new ValueSwap();newObject.OldV = sceneObjects[i];newObject.NewV = randomEnemmy.Object;chosenReplacementObjects.Add(newObject);
                    chosenReplacementObjects.Add( new ValueSwap() { 
                        OldV = sceneObjects[i],
                        NewV = randomEnemmy.Object
                    });
                }

                if (newsize <= oldsize)
                {
                    //this should take into account map/scene size and size of all loaded actors...
                    //not really accurate but *should* work for now to prevent crashing
                    break;
                }
                loopsCount += 1;
                if (loopsCount >= 10000)
                {
                    throw new Exception(" No enemy combo could be found to fill this scene: " + scene.File);
                }
            }

            loopsCount = 0;
            for (int i = 0; i < chosenReplacementObjects.Count; i++)
            {
                // why does j only increase when the actor is NOT modified...?
                int j = 0;

                //foreach (var oldEnemy in sceneEnemies.ToList())
                while (j != sceneEnemies.Count)
                {
                    //Enemy oldEnemy = originalEnemies[i].Find(u => u.Actor == sceneEnemies[j].Actor);
                    Enemy oldEnemy = originalEnemies.Find(u => u.Actor == sceneEnemies[j].Actor);
                    if (oldEnemy != null)
                    {
                        loopsCount += 1;
                        if (loopsCount >= 10000) // inf loop check
                        {
                            throw new Exception(" No enemy combo could be found to fill this scene: " + scene.File);
                        }

                        List<Enemy> subMatches = matchingCandidates[i].FindAll(u => u.Object == chosenReplacementObjects[i].NewV);

                        // why must everything be bogo sort?
                        // why not randomize the list and then pick a random start and sequentially traverse
                        int randomSubmatch;
                        while (true)
                        {
                            /// looking for a list of objects for the actors we chose that fit the actor types
                            // why do we settle on a match if we can't find a type match with their old type? (last option)
                            randomSubmatch = rng.Next(subMatches.Count);
                            if ( (oldEnemy.Type == subMatches[randomSubmatch].Type && oldEnemy.Stationary == subMatches[randomSubmatch].Stationary)
                              || (oldEnemy.Type == subMatches[randomSubmatch].Type && rng.Next(5) == 0)
                              || (subMatches.FindIndex(u => u.Type == oldEnemy.Type) == -1))
                            {
                                break;
                            }
                        }

                        ValueSwap newActor = new ValueSwap();
                        newActor.OldV = oldEnemy.Actor;// sceneEnemies[j].Variables[0];
                        newActor.NewV = subMatches[randomSubmatch].Actor;
                        ValueSwap newValueSwap = new ValueSwap();
                        newValueSwap.NewV = subMatches[randomSubmatch].Variables[rng.Next(subMatches[randomSubmatch].Variables.Count)];
                        chosenReplacementActors.Add(new ValueSwap[] { newActor, newValueSwap });
                        sceneEnemies.RemoveAt(j);

                        // print what enemy was and now is as debug for a scene
                        //var allEnemys = ((GameObjects.Actor[])Enum.GetValues(typeof(GameObjects.Actor))).ToList();
                        string oldEnemyName = EnemyList.Find(u => u.ActorIndex() == newActor.OldV).ToString();
                        string newEnemyName = EnemyList.Find(u => u.ActorIndex() == newActor.NewV).ToString();
                        WriteOutput("Old Enemy actor:[" + oldEnemyName + "] with id [" + newActor.OldV +  "] was replaced by new enemy: [" + newEnemyName + "] with variant: [" + newValueSwap.NewV.ToString("X2") + "]");
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            SetSceneEnemyActors(scene, chosenReplacementActors);
            SetSceneEnemyObjects(scene, chosenReplacementObjects);
            SceneUtils.UpdateScene(scene);
            using (StreamWriter sw = new StreamWriter(settings.OutputROMFilename +  "_EnemizerLog.txt", append: true))
            {
                sw.WriteLine(""); // spacer
                sw.Write(log);
            }

        }

        public static void ShuffleEnemies(OutputSettings settings,Random random)
        {
            try
            { 
                // these are: cutscene map, town and swamp shooting gallery, 
                // sakons hideout, and giants chamber (what)
                int[] SceneSkip = new int[] { 0x08, 0x20, 0x24, 0x4F, 0x69 };
                ReadEnemyList();
                SceneUtils.ReadSceneTable();
                SceneUtils.GetMaps();
                SceneUtils.GetMapHeaders();
                SceneUtils.GetActors();
                foreach (var scene in RomData.SceneList) if (!SceneSkip.Contains(scene.Number))
                {
                    SwapSceneEnemies(settings, scene, random);
                }
            } catch (Exception e)
            {
                throw new Exception("Enemizer failed for this seed, please try another seed.\n\n" + e.Message);
            }
        }

    }

}