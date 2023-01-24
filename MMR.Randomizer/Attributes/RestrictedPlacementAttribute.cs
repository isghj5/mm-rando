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

        public Func<Item, Item, ItemList, bool> RestrictPlacement { get; }

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

        private bool KeepWithinRegion(Item item, Item location, ItemList itemList)
        {
            return item.Region(itemList) == location.Region(itemList);
        }

        private bool KeepWithinOverworld(Item item, Item location, ItemList itemList)
        {
            return !location.Region(itemList).HasValue || !_dungeonRegions.Contains(location.Region(itemList).Value);
        }

        private bool KeepWithinArea(Item item, Item location, ItemList itemList)
        {
            var regionAreaDungeonEntrance = new Dictionary<RegionArea, Item>
            {
                { RegionArea.Swamp, Item.AreaWoodFallTempleAccess },
                { RegionArea.Mountain, Item.AreaSnowheadTempleAccess },
                { RegionArea.Ocean, Item.AreaGreatBayTempleAccess },
                { RegionArea.Canyon, Item.AreaInvertedStoneTowerTempleAccess },
            };

            var dungeonEntranceRegionArea = regionAreaDungeonEntrance.ToDictionary(x => x.Value, x => x.Key);

            RegionArea? getNewRegionArea(Item check, ItemList itemList)
            {
                var regionArea = check.RegionArea(itemList);
                if (regionArea.HasValue && regionAreaDungeonEntrance.ContainsKey(regionArea.Value))
                {
                    var dungeonEntranceToFind = regionAreaDungeonEntrance[regionArea.Value];
                    var dungeonNewEntrance = itemList[dungeonEntranceToFind].NewLocation ?? dungeonEntranceToFind;
                    regionArea = dungeonEntranceRegionArea[dungeonNewEntrance];
                }
                return regionArea;
            }

            return getNewRegionArea(item, itemList) == (_dungeonRegions.Contains(location.Region(itemList)) ? getNewRegionArea(location, itemList) : location.RegionArea(itemList));
        }
    }
}
