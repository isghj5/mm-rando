using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class RegionAttribute : Attribute
    {
        public Region? Region { get; }
        public Item? Reference { get; }

        public RegionAttribute(Region region)
        {
            Region = region;
        }

        public RegionAttribute(Item reference)
        {
            Reference = reference;
        }
    }
}
