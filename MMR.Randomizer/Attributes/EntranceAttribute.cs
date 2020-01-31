using System;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes
{
    public class EntranceAttribute : Attribute
    {
        public Item? Pair { get; private set; }

        public EntranceAttribute(Item pair)
        {
            Pair = pair;
        }

        public EntranceAttribute()
        {

        }
    }
}
