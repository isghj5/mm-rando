using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class RegionAreaAttribute : Attribute
    {
        public RegionArea RegionArea { get; }

        public RegionAreaAttribute(RegionArea regionArea)
        {
            RegionArea = regionArea;
        }
    }
}
