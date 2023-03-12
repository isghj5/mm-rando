using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class RegionAreaExtensions
    {
        public static string Name(this RegionArea regionArea)
        {
            return regionArea.GetAttribute<RegionNameAttribute>()?.Name;
        }

        public static string Preposition(this RegionArea regionArea)
        {
            return regionArea.GetAttribute<RegionNameAttribute>()?.Prepositsion;
        }
    }
}
