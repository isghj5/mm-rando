using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Attributes.Enemy;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Enemy = MMR.Randomizer.Models.Rom.Enemy;

namespace MMR.Randomizer
{
    public class Enemies
    {
        public class ValueSwap
        {
            public int OldV;
            public int NewV;
        }

        private static List<Enemy> EnemyList { get; set; }

        public static void ReadEnemyList()
        {
            EnemyList = Enum.GetValues<GameObjects.Enemy>().Select(e =>
            {
                var enemy = new Enemy
                {
                    Actor = e.GetAttribute<ActorIdAttribute>().Id,
                    Object = e.GetAttribute<ObjectIdAttribute>().Id,
                    Stationary = !e.HasAttribute<IsMovingAttribute>(),
                    Type = (int)e.GetAttribute<ActorTypeAttribute>().Type,
                    Variables = e.GetAttributes<VariableAttribute>().Select(va => va.Variable).ToList(),
                    ForbidFromScene = e.GetAttributes<ForbidFromSceneAttribute>().Select(sea => sea.Scene.Id()).ToList(),
                    ForbidToScene = e.GetAttributes<ForbidToSceneAttribute>().Select(sea => sea.Scene.Id()).ToList(),
                };
                enemy.ObjectSize = ObjUtils.GetObjSize(enemy.Object);
                return enemy;
            }).ToList();
        }

        public static List<int> GetSceneEnemyActors(Scene scene)
        {
            var actorList = new List<int>();
            foreach (var map in scene.Maps)
            {
                foreach (var actor in map.Actors)
                {
                    var enemy = EnemyList.FirstOrDefault(u => u.Actor == actor.n);
                    if (enemy != null)
                    {
                        if (!enemy.ForbidFromScene.Contains(scene.Number))
                        {
                            actorList.Add(enemy.Actor);
                        }
                    }
                }
            }
            return actorList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
            var objList = new List<int>();
            foreach (var map in scene.Maps)
            {
                foreach (var obj in map.Objects)
                {
                    var enemy = EnemyList.FirstOrDefault(u => u.Object == obj);
                    if (enemy != null)
                    {
                        if (!objList.Contains(enemy.Object))
                        {
                            if (!enemy.ForbidFromScene.Contains(scene.Number))
                            {
                                objList.Add(enemy.Object);
                            }
                        }
                    }
                }
            }
            return objList;
        }

        public static void SetSceneEnemyActors(Scene scene, List<ValueSwap[]> A)
        {
            foreach (var map in scene.Maps)
            {
                foreach (var actor in map.Actors)
                {
                    int k = A.FindIndex(u => u[0].OldV == actor.n);
                    if (k != -1)
                    {
                        actor.n = A[k][0].NewV;
                        actor.v = A[k][1].NewV;
                        A.RemoveAt(k);
                    }
                }
            }
        }

        public static void SetSceneEnemyObjects(Scene scene, List<ValueSwap> O)
        {
            foreach (var map in scene.Maps)
            {
                for (int j = 0; j < map.Objects.Count; j++)
                {
                    var swap = O.FirstOrDefault(u => u.OldV == map.Objects[j]);
                    if (swap != null)
                    {
                        map.Objects[j] = swap.NewV;
                    }
                }
            }
        }

        public static List<Enemy> GetMatchPool(List<Enemy> sceneObjectEnemies, List<Enemy> allowedEnemies, Random R)
        {
            var pool = new List<Enemy>();
            foreach (var sceneObjectEnemy in sceneObjectEnemies)
            {
                foreach (var enemy in allowedEnemies)
                {
                    if ((enemy.Type == sceneObjectEnemy.Type) && (enemy.Stationary == sceneObjectEnemy.Stationary))
                    {
                        if (!pool.Contains(enemy))
                        {
                            pool.Add(enemy);
                        }
                    }
                    else if ((enemy.Type == sceneObjectEnemy.Type) && (R.Next(5) == 0))
                    {
                        if (!pool.Contains(enemy))
                        {
                            pool.Add(enemy);
                        }
                    }
                }
            }
            return pool;
        }

        public static void SwapSceneEnemies(Scene scene, Random rng)
        {
            var sceneActors = GetSceneEnemyActors(scene);
            if (sceneActors.Count == 0)
            {
                return;
            }
            var sceneObjects = GetSceneEnemyObjects(scene);
            if (sceneObjects.Count == 0)
            {
                return;
            }
            // if actor doesn't exist but object does, probably spawned by something else
            var objRemove = new List<int>();
            foreach (var sceneObject in sceneObjects)
            {
                var objectMatch = EnemyList.FindAll(u => u.Object == sceneObject);
                if (!objectMatch.Any(o => sceneActors.Contains(o.Actor)))
                {
                    objRemove.Add(sceneObject);
                }
            }
            foreach (var o in objRemove)
            {
                sceneObjects.Remove(o);
            }
            var actorsUpdate = new List<ValueSwap[]>();
            List<ValueSwap> objUpdates;
            List<List<Enemy>> updates;
            List<List<Enemy>> matches;
            while (true)
            {
                objUpdates = new List<ValueSwap>();
                updates = new List<List<Enemy>>();
                matches = new List<List<Enemy>>();
                var oldsize = 0;
                var newsize = 0;
                foreach (var sceneObject in sceneObjects)
                {
                    var sceneObjectEnemies = EnemyList.FindAll(u => u.Object == sceneObject && sceneActors.Contains(u.Actor));
                    var allowedEnemies = EnemyList.FindAll(u => !u.ForbidToScene.Contains(scene.Number));
                    var matchPool = GetMatchPool(sceneObjectEnemies, allowedEnemies, rng);
                    var randomMatch = matchPool.Random(rng);
                    var newobj = randomMatch.Object;
                    newsize += randomMatch.ObjectSize;
                    oldsize += sceneObjectEnemies[0].ObjectSize;
                    var newObject = new ValueSwap();
                    newObject.OldV = sceneObject;
                    newObject.NewV = newobj;

                    objUpdates.Add(newObject);
                    updates.Add(sceneObjectEnemies);
                    matches.Add(matchPool);
                }
                if (newsize <= oldsize)
                {
                    //this should take into account map/scene size and size of all loaded actors...
                    //not really accurate but *should* work for now to prevent crashing
                    break;
                }
            }
            for (var i = 0; i < objUpdates.Count; i++)
            {
                var objUpdate = objUpdates[i];
                var update = updates[i];
                var match = matches[i];
                var j = 0;
                while (j != sceneActors.Count)
                {
                    var sceneActor = sceneActors[j];
                    var old = update.Find(u => u.Actor == sceneActor);
                    if (old != null)
                    {
                        var subMatches = match.FindAll(u => u.Object == objUpdate.NewV);
                        Enemy enemy;
                        while (true)
                        {
                            enemy = subMatches.Random(rng);
                            if (old.Type == enemy.Type && old.Stationary == enemy.Stationary)
                            {
                                break;
                            }
                            else
                            {
                                if (old.Type == enemy.Type && rng.Next(5) == 0)
                                {
                                    break;
                                }
                            }
                            if (!subMatches.Any(u => u.Type == old.Type))
                            {
                                break;
                            }
                        }
                        var newActor = new ValueSwap();
                        newActor.OldV = sceneActor;
                        newActor.NewV = enemy.Actor;
                        var newVar = new ValueSwap();
                        newVar.NewV = enemy.Variables.Random(rng);
                        actorsUpdate.Add(new ValueSwap[] { newActor, newVar });
                        sceneActors.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            SetSceneEnemyActors(scene, actorsUpdate);
            SetSceneEnemyObjects(scene, objUpdates);
            SceneUtils.UpdateScene(scene);
        }

        public static void ShuffleEnemies(Random random)
        {
            int[] sceneSkip = new int[] { 0x08, 0x20, 0x24, 0x4F, 0x69 };
            ReadEnemyList();
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            SceneUtils.GetActors();
            for (int i = 0; i < RomData.SceneList.Count; i++)
            {
                if (!sceneSkip.Contains(RomData.SceneList[i].Number))
                {
                    SwapSceneEnemies(RomData.SceneList[i], random);
                }
            }
        }

        public static void DisableCombatMusicOnEnemy(GameObjects.Actor actor)
        {
            int actorInitVarRomAddr = actor.GetAttribute<ActorInitVarOffsetAttribute>().Offset;
            /// each enemy actor has actor init variables, 
            /// if they have combat music is determined if a flag is set in the seventh byte
            /// disabling combat music means disabling this bit for most enemies
            int actorFileID = (int)actor;
            RomUtils.CheckCompressed(actorFileID);
            int actorFlagLocation = (actorInitVarRomAddr + 7);// - RomData.MMFileList[ActorFID].Addr; // file offset
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
