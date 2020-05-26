using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
            }
        }

        public static List<int> GetSceneEnemyActors(Scene scene)
        {
            List<int> ActorList = new List<int>();
            for (int i = 0; i < scene.Maps.Count; i++)
            {
                for (int j = 0; j < scene.Maps[i].Actors.Count; j++)
                {
                    int k = EnemyList.FindIndex(u => u.Actor == scene.Maps[i].Actors[j].n);
                    if (k != -1)
                    {
                        if (!EnemyList[k].SceneExclude.Contains(scene.Number))
                        {
                            ActorList.Add(EnemyList[k].Actor);
                        }
                    }
                }
            }
            return ActorList;
        }

        public static List<int> GetSceneEnemyObjects(Scene scene)
        {
            List<int> ObjList = new List<int>();
            for (int i = 0; i < scene.Maps.Count; i++)
            {
                for (int j = 0; j < scene.Maps[i].Objects.Count; j++)
                {
                    int k = EnemyList.FindIndex(u => u.Object == scene.Maps[i].Objects[j]);
                    if (k != -1)
                    {
                        if (!ObjList.Contains(EnemyList[k].Object))
                        {
                            if (!EnemyList[k].SceneExclude.Contains(scene.Number))
                            {
                                ObjList.Add(EnemyList[k].Object);
                            }
                        }
                    }
                }
            }
            return ObjList;
        }

        public static void SetSceneEnemyActors(Scene scene, List<ValueSwap[]> A)
        {
            for (int i = 0; i < scene.Maps.Count; i++)
            {
                for (int j = 0; j < scene.Maps[i].Actors.Count; j++)
                {
                    int k = A.FindIndex(u => u[0].OldV == scene.Maps[i].Actors[j].n);
                    if (k != -1)
                    {
                        scene.Maps[i].Actors[j].n = A[k][0].NewV;
                        scene.Maps[i].Actors[j].v = A[k][1].NewV;
                        A.RemoveAt(k);
                    }
                }
            }
        }

        public static void SetSceneEnemyObjects(Scene scene, List<ValueSwap> O)
        {
            for (int i = 0; i < scene.Maps.Count; i++)
            {
                for (int j = 0; j < scene.Maps[i].Objects.Count; j++)
                {
                    int k = O.FindIndex(u => u.OldV == scene.Maps[i].Objects[j]);
                    if (k != -1)
                    {
                        scene.Maps[i].Objects[j] = O[k].NewV;
                    }
                }
            }
        }

        public static List<Enemy> GetMatchPool(List<Enemy> Actors, Random R)
        {
            List<Enemy> Pool = new List<Enemy>();
            for (int i = 0; i < Actors.Count; i++)
            {
                Enemy E = EnemyList.Find(u => u.Actor == Actors[i].Actor);
                for (int j = 0; j < EnemyList.Count; j++)
                {
                    if ((EnemyList[j].Type == E.Type) && (EnemyList[j].Stationary == E.Stationary))
                    {
                        if (!Pool.Contains(EnemyList[j]))
                        {
                            Pool.Add(EnemyList[j]);
                        }
                    }
                    else if ((EnemyList[j].Type == E.Type) && (R.Next(5) == 0))
                    {
                        if (!Pool.Contains(EnemyList[j]))
                        {
                            Pool.Add(EnemyList[j]);
                        }
                    }
                }
            }
            return Pool;
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
            int[] SceneSkip = new int[] { 0x08, 0x20, 0x24, 0x4F, 0x69 };
            ReadEnemyList();
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            SceneUtils.GetActors();
            for (int i = 0; i < RomData.SceneList.Count; i++)
            {
                if (!SceneSkip.Contains(RomData.SceneList[i].Number))
                {
                    SwapSceneEnemies(RomData.SceneList[i], random);
                }
            }
        }

        public static void DisableCombatMusicOnEnemy(GameObjects.Actor actor)
        {
            int ActorInitVarRomAddr = actor.GetAttribute<ActorInitVarOffsetAttribute>().Offset;
            /// each enemy actor has actor init variables, 
            /// if they have combat music is determined if a flag is set in the seventh byte
            /// disabling combat music means disabling this bit for most enemies
            int ActorFID = (int)actor;
            RomUtils.CheckCompressed(ActorFID);
            int ActorFlagLocation = (ActorInitVarRomAddr + 7);// - RomData.MMFileList[ActorFID].Addr; // file offset
            byte FlagByte = RomData.MMFileList[ActorFID].Data[ActorFlagLocation];
            RomData.MMFileList[ActorFID].Data[ActorFlagLocation] = (byte)(FlagByte & 0xFB);
        }


        public static void DisableEnemyCombatMusic(bool WeakEnemiesOnly = false)
        {
            /// each enemy has one int flag that contains a single bit that enables combat music
            /// to get these values I used the starting rom addr of the enemy actor
            ///  searched the ram for the actor overlay table that has rom and ram per actor,
            ///  there it lists the actor init var ram and actor ram locations, diff, apply to rom start
            ///  the combat music flag is part of the seventh byte of the actor init variables, but our fuction knows this
             
            var WeakEnemyList = new GameObjects.Actor[]
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

            }.ToList();

            var AnnoyingEnemyList = new GameObjects.Actor[]
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

            }.ToList();

            var WholeList = WeakEnemiesOnly ? WeakEnemyList : WeakEnemyList.Concat(AnnoyingEnemyList);

            foreach (GameObjects.Actor enemy in WholeList)
            {
                DisableCombatMusicOnEnemy(enemy);
            }
        }
    }

}