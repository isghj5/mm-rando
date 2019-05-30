using MMRando.Models.Rom;
using MMRando.Utils;
using System;
using System.Collections.Generic;

namespace MMRando
{
    public partial class ROMFuncs
    {
        private class ActorLookup
        {
            public int Scene = new int();
            public int Actor = new int();
            public int OldV;
            public int NewV;
        }

        private static List<ActorLookup> GetOwlActors()
        {
            List<ActorLookup> Actors = new List<ActorLookup>();
            ActorLookup O;
            for (int s = 0; s < RomData.SceneList.Count; s++)
            {
                for (int m = 0; m < RomData.SceneList[s].Maps.Count; m++)
                {
                    O = new ActorLookup();
                    for (int a = 0; a < RomData.SceneList[s].Maps[m].Actors.Count; a++)
                    {
                        if (RomData.SceneList[s].Maps[m].Actors[a].n == 0x0223)
                        {
                            O.Scene = s;
                            O.Actor = a;
                            O.OldV = RomData.SceneList[s].Maps[m].Actors[a].v;
                            Actors.Add(O);
                        }
                    }
                    if (O.Scene != 0 && O.Actor != 0)
                    {
                        break;
                    }
                }
            }
            return Actors;
        }

        private static void SetOwlActors(List<ActorLookup> Owls)
        {
            foreach (ActorLookup O in Owls)
            {
                int s = O.Scene;
                if (s >= 0 && s < RomData.SceneList.Count)
                {
                    foreach (Map M in RomData.SceneList[s].Maps)
                    {
                        if (O.Actor < M.Actors.Count && M.Actors[O.Actor].n == 0x0223)
                        {
                            M.Actors[O.Actor].v = O.NewV;
                        }
                    }

                }
                SceneUtils.UpdateScene(RomData.SceneList[s]);
            }
        }

        public static void SwapOwlStatues(int[] newOwlLocations)
        {
            SceneUtils.ReadSceneTable();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            SceneUtils.GetActors();
            List<ActorLookup> owlStatues = GetOwlActors();
            for (int i = 0; i < newOwlLocations.Length; i++)
            {
                Console.WriteLine(owlStatues[i].Scene);
                owlStatues[i].NewV = owlStatues[newOwlLocations[i]].OldV;
            }
            SetOwlActors(owlStatues);
        }
    }
}
