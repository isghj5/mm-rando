using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// Dynapoly actors have a limited space to work with, we need to measure these
    /// </summary>

    class DynaAttributes : Attribute
    {
        public int Polygons;
        public int Verticies;
        public List<int> Variants;

        public DynaAttributes(int triangles, int verticies, int variant = -1, params int[] additionalVariants)
        {
            Polygons = triangles;
            Verticies = verticies;

            var v = new List<int> { variant };
            if (additionalVariants.Length > 0)
            {
                v.AddRange(additionalVariants);
            }
            Variants = v;
        }
    }
}
