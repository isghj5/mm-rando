using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class MultiLocationAttribute : Attribute
    {
        public Item[] Locations { get; }

        public MultiLocationAttribute(params Item[] locations)
        {
            Locations = locations;
        }
    }
}
