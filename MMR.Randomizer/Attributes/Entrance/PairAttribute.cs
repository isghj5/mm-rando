using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class PairAttribute : Attribute
    {
        public Item Pair { get; set; }

        public PairAttribute(Item pair)
        {
            Pair = pair;
        }
    }
}
