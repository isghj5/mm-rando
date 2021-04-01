using System;

namespace MMR.Randomizer.Attributes
{
    public class CollectableIndexAttribute : Attribute
    {
        public ushort Index { get; }

        public CollectableIndexAttribute(ushort index)
        {
            Index = index;
        }
    }
}
