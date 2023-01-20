using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RestrictedPlacementAttribute : Attribute
    {
        public enum RestrictionType
        {
            KeepWithinRegion,
            KeepWithinArea,
            KeepWithinOverworld,
        }

        public Func<Item, Item, bool> RestrictPlacement { get; }

        private static Func<Item, Item> _getNewDungeonEntrance;
        private readonly IReadOnlyCollection<Region?> _dungeonRegions = new List<Region?> { Region.WoodfallTemple, Region.SnowheadTemple, Region.GreatBayTemple, Region.StoneTowerTemple }.AsReadOnly();

        public RestrictedPlacementAttribute(RestrictionType restrictionType)
        {
            switch (restrictionType)
            {
                case RestrictionType.KeepWithinRegion:
                    RestrictPlacement = KeepWithinRegion;
                    break;
                case RestrictionType.KeepWithinArea:
                    RestrictPlacement = KeepWithinArea;
                    break;
                case RestrictionType.KeepWithinOverworld:
                    RestrictPlacement = KeepWithinOverworld;
                    break;
            }
        }

        public static void SetDungeonEntranceFunction(Func<Item, Item> getNewDungeonEntrance)
        {
            _getNewDungeonEntrance = getNewDungeonEntrance;
        }

        private bool KeepWithinRegion(Item item, Item location)
        {
            return item.Region() == location.Region();
        }

        private bool KeepWithinOverworld(Item item, Item location)
        {
            return !location.Region().HasValue || !_dungeonRegions.Contains(location.Region().Value);
        }

        private bool KeepWithinArea(Item item, Item location)
        {
            if (_getNewDungeonEntrance == null)
            {
                throw new InvalidOperationException($"Must call {nameof(SetDungeonEntranceFunction)} before checking item restrictions.");
            }

            var regionAreaDungeonEntrance = new Dictionary<RegionArea, Item>
            {
                { RegionArea.Swamp, Item.AreaWoodFallTempleAccess },
                { RegionArea.Mountain, Item.AreaSnowheadTempleAccess },
                { RegionArea.Ocean, Item.AreaGreatBayTempleAccess },
                { RegionArea.Canyon, Item.AreaInvertedStoneTowerTempleAccess },
            };

            var dungeonEntranceRegionArea = regionAreaDungeonEntrance.ToDictionary(x => x.Value, x => x.Key);

            RegionArea? getNewRegionArea(Item check)
            {
                var regionArea = check.RegionArea();
                if (regionArea.HasValue && regionAreaDungeonEntrance.ContainsKey(regionArea.Value))
                {
                    var dungeonEntranceToFind = regionAreaDungeonEntrance[regionArea.Value];
                    var dungeonNewEntrance = _getNewDungeonEntrance(dungeonEntranceToFind);
                    regionArea = dungeonEntranceRegionArea[dungeonNewEntrance];
                }
                return regionArea;
            }

            return getNewRegionArea(item) == (_dungeonRegions.Contains(location.Region()) ? getNewRegionArea(location) : location.RegionArea());
        }
    }
}
