using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMRando.GameObjects;

namespace MMRando.Attributes
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
