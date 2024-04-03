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
        public int Triangles;
        public int Verticies;

        public DynaAttributes(int triangles, int verticies)
        {
            Triangles = triangles;
            Verticies = verticies;
        }
    }
}
