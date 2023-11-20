using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class RegionExtensions
    {
        public static string Name(this Region region)
        {
            return region.GetAttribute<RegionNameAttribute>()?.Name;
        }

        public static string Preposition(this Region region)
        {
            return region.GetAttribute<RegionNameAttribute>()?.Prepositsion;
        }

        public static RegionArea? RegionArea(this Region region)
        {
            return region.GetAttribute<RegionAreaAttribute>()?.RegionArea;
        }
    }
}
