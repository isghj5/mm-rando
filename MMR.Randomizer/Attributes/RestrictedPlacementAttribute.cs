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
            KeepFairyWithinArea,
            KeepWithinOverworld,
            GreatFairyRewards,
        }

        public Func<Item, Item, ItemList, bool> RestrictPlacement { get; }

        private readonly IReadOnlyCollection<Region?> _templeRegions = new List<Region?>
        {
            Region.WoodfallTemple,
            Region.SnowheadTemple,
            Region.GreatBayTemple,
            Region.StoneTowerTemple
        }.AsReadOnly();

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
                case RestrictionType.KeepFairyWithinArea:
                    RestrictPlacement = KeepFairyWithinArea;
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
            return item.RegionAreaOfTemple(itemList) == (_templeRegions.Contains(location.Region(itemList)) ? location.RegionAreaOfTemple(itemList) : location.RegionArea(itemList));
        }

        private bool KeepFairyWithinArea(Item item, Item location, ItemList itemList)
        {
            RegionArea fairyRegionArea;
            switch (item.RegionArea(itemList))
            {
                case RegionArea.Swamp:
                    fairyRegionArea = Item.FairySpinAttack.RegionArea(itemList).Value;
                    break;
                case RegionArea.Mountain:
                    fairyRegionArea = Item.FairyDoubleMagic.RegionArea(itemList).Value;
                    break;
                case RegionArea.Ocean:
                    fairyRegionArea = Item.FairyDoubleDefense.RegionArea(itemList).Value;
                    break;
                case RegionArea.Canyon:
                    fairyRegionArea = Item.ItemFairySword.RegionArea(itemList).Value;
                    break;
                default:
                    fairyRegionArea = RegionArea.None;
                    break;
            }
            return fairyRegionArea == (_templeRegions.Contains(location.Region(itemList)) ? location.RegionAreaOfTemple(itemList) : location.RegionArea(itemList));
        }
    }
}
