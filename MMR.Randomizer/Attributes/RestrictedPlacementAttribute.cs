using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;
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
            KeepWithinTemples,
            KeepWithinArea,
            KeepWithinOverworld,
            GreatFairyRewards,
        }

        public Func<Item, Item, ItemList, bool> RestrictPlacement { get; }

        private readonly IReadOnlyCollection<Region?> _templeRegions = new List<Region?> { Region.WoodfallTemple, Region.SnowheadTemple, Region.GreatBayTemple, Region.StoneTowerTemple }.AsReadOnly();

        public RestrictedPlacementAttribute(RestrictionType restrictionType)
        {
            switch (restrictionType)
            {
                case RestrictionType.KeepWithinTemples:
                    RestrictPlacement = KeepWithinTemples;
                    break;
                case RestrictionType.KeepWithinArea:
                    RestrictPlacement = KeepWithinArea;
                    break;
                case RestrictionType.KeepWithinOverworld:
                    RestrictPlacement = KeepWithinOverworld;
                    break;
                case RestrictionType.GreatFairyRewards:
                    RestrictPlacement = GreatFairyRewards;
                    break;
            }
        }

        private bool GreatFairyRewards(Item item, Item location, ItemList itemList)
        {
            return ItemUtils.GreatFairyRewards().Contains(location);
        }

        private bool KeepWithinTemples(Item item, Item location, ItemList itemList)
        {
            return location == Item.SongOath || (location.Region(itemList).HasValue && _templeRegions.Contains(location.Region(itemList).Value));
        }

        private bool KeepWithinOverworld(Item item, Item location, ItemList itemList)
        {
            return location != Item.SongOath && (!location.Region(itemList).HasValue || !_templeRegions.Contains(location.Region(itemList).Value));
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

            return getNewRegionArea(item, itemList) == (_templeRegions.Contains(location.Region(itemList)) ? getNewRegionArea(location, itemList) : location.RegionArea(itemList));
        }
    }
}
