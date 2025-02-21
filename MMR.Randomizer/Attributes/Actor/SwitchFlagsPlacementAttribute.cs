using System;

namespace MMR.Randomizer.Attributes.Actor
{
    public enum SwitchTrigger
    {
        DoNotUse = 0,
        Sends,
        Receives,
        SendsAndRecieves,
        Death
    }

    /// <summary>
    ///  Placement of Switch flags (0x80) in an Actor's X Rotation
    /// </summary>
    class SwitchFlagsPlacementXRotAttribute : Attribute { }

    /// <summary>
    ///  Placement of Switch flags (0x80) in an Actor's Z Rotation
    /// </summary>
    class SwitchFlagsPlacementZRotAttribute : Attribute { }

    /// <summary>
    ///  Placement of Switch flags (0x80) in an Actor's params/variant
    /// </summary>
    class SwitchFlagsPlacementAttribute : Attribute
    {
        public SwitchTrigger flagType = SwitchTrigger.DoNotUse;
        public int Size; // the width in bits of the data in the actor params/vars
        public int Shift;

        public SwitchFlagsPlacementAttribute(int size, int shift) {
            this.Size = size;
            this.Shift = shift;
        }

        public SwitchFlagsPlacementAttribute(SwitchTrigger type, int size, int shift)
        {
            this.flagType = type;
            this.Size = size;
            this.Shift = shift;
        }

    }
}
