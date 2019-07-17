using MMRando.Models.Rom;
using MMRando.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMRando
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
            EnemyList = new List<Enemy>();
            string[] lines = Properties.Resources.ENEMIES.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
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
                foreach (var variable in lines[i + 2].Split(','))
                {
                    e.Variables.Add(Convert.ToInt32(variable, 16));
                }
                e.Type = Convert.ToInt32(lines[i + 3], 16);
                e.Stationary = Convert.ToInt32(lines[i + 4], 16);
                if (lines[i + 5] != "")
                {
                    foreach (var exclude in lines[i + 5].Split(','))
                    {
                        e.ExcludeMapOrSceneFile.Add(Convert.ToInt32(exclude));
                    }
                }
                EnemyList.Add(e);
                i += 6;
            }
        }

        public static List<int> GetSceneEnemyActors(Scene scene)
        {
            List<int> ActorList = new List<int>();
            foreach (var map in scene.Maps)
            {
                foreach (var actor in map.Actors)
                {
                    var enemy = EnemyList.FirstOrDefault(e => e.Actor == actor.n);
                    if (enemy != null && !enemy.ExcludeMapOrSceneFile.Contains(map.File) && !enemy.ExcludeMapOrSceneFile.Contains(scene.File))
                    {
                        ActorList.Add(enemy.Actor);
                    }
                }
            }
            return ActorList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
            List<int> ObjList = new List<int>();
            foreach (var map in scene.Maps)
            {
                foreach (var obj in map.Objects)
                {
                    var enemy = EnemyList.FirstOrDefault(e => e.Object == obj);
                    if (enemy != null && !ObjList.Contains(obj) && !enemy.ExcludeMapOrSceneFile.Contains(map.File) && !enemy.ExcludeMapOrSceneFile.Contains(scene.File))
                    {
                        ObjList.Add(enemy.Object);
                    }
                }
            }
            return ObjList;
        }

        public static void SetSceneEnemyActors(Scene scene, List<ValueSwap[]> A)
        {
            foreach (var map in scene.Maps)
            {
                foreach (var actor in map.Actors)
                {
                    var updatedValues = A.FirstOrDefault(vs => vs[0].OldV == actor.n);
                    if (updatedValues != null)
                    {
                        actor.n = updatedValues[0].NewV;
                        actor.v = updatedValues[1].NewV;
                        A.Remove(updatedValues);
                    }
                }
            }
        }

        public static void SetSceneEnemyObjects(Scene scene, List<ValueSwap> O)
        {
            foreach (var map in scene.Maps)
            {
                for (var i = 0; i < map.Objects.Count; i++)
                {
                    var obj = map.Objects[i];
                    var updatedValue = O.FirstOrDefault(vs => vs.OldV == obj);
                    if (updatedValue != null)
                    {
                        map.Objects[i] = updatedValue.NewV;
                    }
                }
            }
        }

        public static List<Enemy> GetMatchPool(List<Enemy> Actors, Random R)
        {
            return EnemyList.Where(e => Actors.Any(a => a.Type == e.Type && (a.Stationary == e.Stationary || R.Next(5) == 0))).ToList();
        }

        public static void SwapSceneEnemies(Scene scene, Random rng)
        {
            List<int> Actors = GetSceneEnemyActors(scene);
            if (Actors.Count == 0)
            {
                return;
            }
            List<int> Objects = GetSceneEnemyObjects(scene);
            if (Objects.Count == 0)
            {
                return;
            }
            // if actor doesn't exist but object does, probably spawned by something else
            List<int> ObjRemove = new List<int>();
            foreach (int o in Objects)
            {
                List<Enemy> ObjectMatch = EnemyList.FindAll(u => u.Object == o);
                bool exists = false;
                for (int i = 0; i < ObjectMatch.Count; i++)
                {
                    exists |= Actors.Contains(ObjectMatch[i].Actor);
                }
                if (!exists)
                {
                    ObjRemove.Add(o); ;
                }
            }
            foreach (int o in ObjRemove)
            {
                Objects.Remove(o);
            }
            List<ValueSwap[]> ActorsUpdate = new List<ValueSwap[]>();
            List<ValueSwap> ObjsUpdate;
            List<List<Enemy>> Updates;
            List<List<Enemy>> Matches;
            while (true)
            {
                ObjsUpdate = new List<ValueSwap>();
                Updates = new List<List<Enemy>>();
                Matches = new List<List<Enemy>>();
                int oldsize = 0;
                int newsize = 0;
                for (int i = 0; i < Objects.Count; i++)
                {
                    Updates.Add(EnemyList.FindAll(u => ((u.Object == Objects[i]) && (Actors.Contains(u.Actor)))));
                    Matches.Add(GetMatchPool(Updates[i], rng));
                    int k = rng.Next(Matches[i].Count);
                    int newobj = Matches[i][k].Object;
                    newsize += Matches[i][k].ObjectSize;
                    oldsize += Updates[i][0].ObjectSize;
                    ValueSwap NewObject = new ValueSwap();
                    NewObject.OldV = Objects[i];
                    NewObject.NewV = newobj;
                    ObjsUpdate.Add(NewObject);
                }
                if (newsize <= oldsize)
                {
                    //this should take into account map/scene size and size of all loaded actors...
                    //not really accurate but *should* work for now to prevent crashing
                    break;
                }
            }
            for (int i = 0; i < ObjsUpdate.Count; i++)
            {
                int j = 0;
                while (j != Actors.Count)
                {
                    Enemy Old = Updates[i].Find(u => u.Actor == Actors[j]);
                    if (Old != null)
                    {
                        List<Enemy> SubMatches = Matches[i].FindAll(u => u.Object == ObjsUpdate[i].NewV);
                        int l;
                        while (true)
                        {
                            l = rng.Next(SubMatches.Count);
                            if ((Old.Type == SubMatches[l].Type) && (Old.Stationary == SubMatches[l].Stationary))
                            {
                                break;
                            }
                            else
                            {
                                if ((Old.Type == SubMatches[l].Type) && (rng.Next(5) == 0))
                                {
                                    break;
                                }
                            }
                            if (SubMatches.FindIndex(u => u.Type == Old.Type) == -1)
                            {
                                break;
                            }
                        }
                        ValueSwap NewActor = new ValueSwap();
                        NewActor.OldV = Actors[j];
                        NewActor.NewV = SubMatches[l].Actor;
                        ValueSwap NewVar = new ValueSwap();
                        NewVar.NewV = SubMatches[l].Variables[rng.Next(SubMatches[l].Variables.Count)];
                        ActorsUpdate.Add(new ValueSwap[] { NewActor, NewVar });
                        Actors.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }
            }
            SetSceneEnemyActors(scene, ActorsUpdate);
            SetSceneEnemyObjects(scene, ObjsUpdate);
            SceneUtils.UpdateScene(scene);
        }

        public static void ShuffleEnemies(Random random)
        {
            int[] ExcludeSceneFile = new int[] 
            {
                1520, // Cutscene Map
                1239, // Town Shooting Gallery
                1274, // Swamp Shooting Gallery
                1414, // Sakon's Hideout
                1504, // Giant's Chamber
            };
            ReadEnemyList();
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            SceneUtils.GetActors();
            foreach (var scene in RomData.SceneList)
            {
                if (!ExcludeSceneFile.Contains(scene.File))
                {
                    SwapSceneEnemies(scene, random);
                }
            }
        }

    }

}