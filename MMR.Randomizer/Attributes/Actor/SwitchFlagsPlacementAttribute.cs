using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Attributes.Actor
{
    class SwitchFlagsPlacementXRotAttribute : Attribute { }

    class SwitchFlagsPlacementZRotAttribute : Attribute { }

    class SwitchFlagsPlacementAttribute : Attribute
    {
        public int Mask;
        public int Shift;

        public SwitchFlagsPlacementAttribute(int mask, int shift) {
            Mask = mask;
            Shift = shift;
        }
    }
}
