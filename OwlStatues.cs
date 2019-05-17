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
            public ValueSwap Swap;
        }

        private static List<ActorLookup> GetOwlActors()
        {
            List<ActorLookup> Actors = new List<ActorLookup>();
            ActorLookup O;
            ValueSwap Swap;
            for (int s = 0; s < SceneList.Count; s++)
            {
                for (int m = 0; m < SceneList[s].Maps.Count; m++)
                {
                    O = new ActorLookup();
                    for (int a = 0; a < SceneList[s].Maps[m].Actors.Count; a++)
                    {
                        if (SceneList[s].Maps[m].Actors[a].n == 0x0223)
                        {
                            O.Scene = s;
                            O.Actor = a;
                            Swap = new ValueSwap();
                            Swap.OldV = SceneList[s].Maps[m].Actors[a].v;
                            O.Swap = Swap;
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
                if (s >= 0 && s < SceneList.Count)
                {
                    foreach (Map M in SceneList[s].Maps)
                    {
                        if (O.Actor < M.Actors.Count && M.Actors[O.Actor].n == 0x0223)
                        {
                            M.Actors[O.Actor].v = O.Swap.NewV;
                        }
                    }

                }
                UpdateScene(SceneList[s]);
            }
        }

        public static void SwapOwlStatues(int[] newOwlLocations)
        {
            ReadSceneTable();
            GetMaps();
            GetMapHeaders();
            GetActors();
            List<ActorLookup> owlStatues = GetOwlActors();
            for (int i = 0; i < newOwlLocations.Length; i++)
            {
                Console.WriteLine(owlStatues[i].Scene);
                owlStatues[i].Swap.NewV = owlStatues[newOwlLocations[i]].Swap.OldV;
            }
            SetOwlActors(owlStatues);
        }
    }
}
