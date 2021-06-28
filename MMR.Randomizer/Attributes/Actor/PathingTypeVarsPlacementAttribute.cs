using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Attributes.Actor
{
    class PathingTypeVarsPlacementAttribute : Attribute
    {
        public int Mask;
        public int Shift;

        public PathingTypeVarsPlacementAttribute(int mask, int shift)
        {
            Mask = mask;
            Shift = shift;
        }
    }
}
