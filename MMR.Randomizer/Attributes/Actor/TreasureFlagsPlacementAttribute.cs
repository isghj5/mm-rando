using System;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  Placement of Treasure flags (32 bits per scene) in an Actor's params/variant
    /// </summary>
    class TreasureFlagsPlacementAttribute : Attribute
    {
        public int Mask;
        public int Shift;

        public TreasureFlagsPlacementAttribute(int mask, int shift)
        {
            Mask = mask;
            Shift = shift;
        }
    }
}
