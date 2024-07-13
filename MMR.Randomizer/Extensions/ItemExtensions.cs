using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;
using MMR.Common.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Attributes.Entrance;
using System.Collections.Generic;
using System.Linq;
using MMR.Randomizer.Models.Settings;
using System;
using MMR.Randomizer.Models;

namespace MMR.Randomizer.Extensions
{
    public static class ItemExtensions
    {
        public static ushort? GetItemIndex(this Item item)
        {
            return item.GetAttribute<GetItemIndexAttribute>()?.Index;
        }

        public static IEnumerable<ushort> GetCollectableIndices(this Item item)
        {
            return item.GetAttributes<CollectableIndexAttribute>().Select(x => x.Index);
        }

        public static bool IsNullableItem(this Item item)
        {
            return item.HasAttribute<NullableItemAttribute>();
        }

        public static int[] GetBottleItemIndices(this Item item)
        {
            return item.GetAttribute<GetBottleItemIndicesAttribute>()?.Indices;
        }

        private static IReadOnlyDictionary<Item, string> _names = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<ItemNameAttribute>()?.Name);
        public static string Name(this Item item)
        {
            return _names.GetValueOrDefault(item);
        }

        public static string ProgressiveUpgradeName(this Item item, bool progressiveUpgradesEnabled)
        {
            if (progressiveUpgradesEnabled)
            {
                if (item == Item.StartingSword || item == Item.UpgradeRazorSword || item == Item.UpgradeGildedSword)
                {
                    return "Sword Upgrade";
                }
                if (item == Item.FairyMagic || item == Item.FairyDoubleMagic)
                {
                    return "Magic Power Upgrade";
                }
                if (item == Item.UpgradeAdultWallet || item == Item.UpgradeGiantWallet || item == Item.UpgradeRoyalWallet)
                {
                    return "Wallet Upgrade";
                }
                if (item == Item.ItemBombBag || item == Item.UpgradeBigBombBag || item == Item.UpgradeBiggestBombBag)
                {
                    return "Bomb Bag Upgrade";
                }
                if (item == Item.ItemBow || item == Item.UpgradeBigQuiver || item == Item.UpgradeBiggestQuiver)
                {
                    return "Bow Upgrade";
                }
                if (item == Item.SongLullaby || item == Item.SongLullabyIntro)
                {
                    return "Goron Lullaby Upgrade";
                }
            }
            return item.Name();
        }

        private static IReadOnlyDictionary<Item, string> _locations = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<LocationNameAttribute>()?.Name);
        public static string Location(this Item item, ItemList itemList = null)
        {
            var location = _locations.GetValueOrDefault(item);
            if (location == null)
            {
                return null;
            }
            if (itemList == null)
            {
                return location;
            }

            var reference = item.GetAttribute<RegionAttribute>()?.Reference;
            if (reference == null)
            {
                return location;
            }

            var referenceNewLocation = itemList[reference.Value].NewLocation ?? reference.Value;
            var itemCategory = item.GetAttribute<ItemPoolAttribute>()?.ItemCategory;

            var alteredLocation = Enum.GetValues<Item>()
                .Where(x => x.GetAttribute<ItemPoolAttribute>()?.ItemCategory == itemCategory)
                .FirstOrDefault(x => x.GetAttribute<RegionAttribute>().Reference == referenceNewLocation);

            return alteredLocation.Location();
        }

        private static IReadOnlyDictionary<Item, RegionAttribute> _regionAttributes = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<RegionAttribute>());
        public static Region? Region(this Item item, ItemList itemList)
        {
            var regionAttribute = _regionAttributes.GetValueOrDefault(item);
            if (regionAttribute == null)
            {
                return null;
            }
            if (regionAttribute.Region != null)
            {
                return regionAttribute.Region;
            }
            if (regionAttribute.Reference != null)
            {
                var reference = regionAttribute.Reference.Value;
                var newLocation = itemList?[reference].NewLocation ?? reference;
                return newLocation.Region(itemList);
            }
            throw new System.Exception($"{nameof(RegionAttribute)} must have either {nameof(RegionAttribute.Region)} or {nameof(RegionAttribute.Reference)}");
        }

        public static List<Region> Regions(this Item location)
        {
            var multiRegionLocations = location.GetAttribute<MultiLocationAttribute>()?.Locations ?? new Item[] { location };
            return multiRegionLocations.Select(loc => loc.Region(null).Value).ToList();
        }

        public static Region RegionForDirectHint(this Item location, ItemList itemList)
        {
            Item[] multiRegionLocations;
            if ((multiRegionLocations = location.GetAttribute<MultiLocationAttribute>()?.Locations) != null)
            {
                location = multiRegionLocations[0];
            }

            return location.Region(itemList).Value;
        }

        public static RegionArea? RegionArea(this Item item, ItemList itemList)
        {
            return item.GetAttribute<RegionAreaAttribute>()?.RegionArea ?? item.Region(itemList)?.RegionArea();
        }

        private static IReadOnlyDictionary<Item, Item?> _mainLocations = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<MainLocationAttribute>()?.Location);
        public static Item? MainLocation(this Item item)
        {
            return _mainLocations.GetValueOrDefault(item);
        }

        private static IReadOnlyDictionary<Item, string> _entrances = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<EntranceNameAttribute>()?.Name);
        public static string Entrance(this Item item)
        {
            return _entrances.GetValueOrDefault(item);
        }

        public static ShopTextAttribute ShopTexts(this Item item)
        {
            return item.GetAttribute<ShopTextAttribute>();
        }

        public static string[] ItemHints(this Item item)
        {
            return item.GetAttribute<GossipItemHintAttribute>().Values;
        }

        public static string[] LocationHints(this Item item)
        {
            return item.GetAttribute<GossipLocationHintAttribute>().Values;
        }

        public static bool IsRepeatable(this Item item, GameplaySettings settings = null)
        {
            var isTemporaryOrCantBe = settings == null || item.GetAttribute<TemporaryAttribute>()?.Condition(settings) != false;
            return item.HasAttribute<RepeatableAttribute>() && isTemporaryOrCantBe;
        }

        public static bool IsReturnable(this Item item, GameplaySettings settings)
        {
            return item.GetAttribute<ReturnableAttribute>()?.Condition(settings) ?? false;
        }

        public static bool IsRupeeRepeatable(this Item item)
        {
            return item.HasAttribute<RupeeRepeatableAttribute>();
        }

        public static bool IsDowngradable(this Item item)
        {
            return item.HasAttribute<DowngradableAttribute>();
        }

        public static void PrepareSettings(GameplaySettings settings)
        {
            _isTemporary = Enum.GetValues<Item>().ToDictionary(x => x, x => x.GetAttribute<TemporaryAttribute>()?.Condition(settings) ?? false);
            _overwriteableSlots = Enum.GetValues<Item>().ToDictionary(x => x, x =>
            {
                var overwriteableAttribute = x.GetAttribute<OverwritableAttribute>();
                if (overwriteableAttribute?.Condition(settings) == true)
                {
                    return overwriteableAttribute.Slot;
                }
                return OverwritableAttribute.ItemSlot.None;
            });
        }

        private static IReadOnlyDictionary<Item, bool> _isTemporary;
        public static bool IsTemporary(this Item item)
        {
            return _isTemporary.GetValueOrDefault(item);
        }

        public static ClassicCategory? ClassicCategory(this Item item)
        {
            return item.GetAttribute<ItemPoolAttribute>()?.ClassicCategory;
        }

        public static ItemCategory? ItemCategory(this Item item)
        {
            return item.GetAttribute<ItemPoolAttribute>()?.ItemCategory;
        }

        public static LocationCategory? LocationCategory(this Item item)
        {
            return item.GetAttribute<ItemPoolAttribute>()?.LocationCategory;
        }

        public static bool IsFake(this Item item)
        {
            return item.Name() == null;
        }

        public static bool CanBeStartedWith(this Item item)
        {
            return item.HasAttribute<StartingTingleMapAttribute>()
                || item.HasAttribute<StartingItemIdAttribute>()
                || item.HasAttribute<StartingItemAttribute>();
        }

        public static IList<DungeonEntrance> DungeonEntrances(this Item item)
        {
            if (!item.HasAttribute<DungeonEntranceAttribute>())
            {
                return null;
            }
            var result = new List<DungeonEntrance>();
            var attr = item.GetAttribute<DungeonEntranceAttribute>();
            result.Add(attr.Entrance);
            if (attr.Pair.HasValue)
            {
                result.Add(attr.Pair.Value);
            }
            return result;
        }

        private static IReadOnlyDictionary<Item, OverwritableAttribute.ItemSlot> _overwriteableSlots;
        public static OverwritableAttribute.ItemSlot OverwriteableSlot(this Item item)
        {
            return _overwriteableSlots.GetValueOrDefault(item);
        }

        public static bool IsSong(this Item item)
        {
            return (Item.SongTime <= item && item <= Item.SongOath)
                || item == Item.SongLullabyIntro
                || (item.MainLocation().HasValue && item.MainLocation().Value.IsSong());
        }

        public static bool IsPlacementHighlyRestricted(this Item item, GameplaySettings settings)
        {
            if (!settings.AddSongs && item.IsSong())
            {
                return true;
            }
            if (settings.BossRemainsMode.HasFlag(BossRemainsMode.ShuffleOnly) && item.ItemCategory() == GameObjects.ItemCategory.BossRemains)
            {
                return true;
            }
            return false;
        }

        public static ChestTypeAttribute.ChestType ChestType(this Item item)
        {
            return item.GetAttribute<ChestTypeAttribute>().Type;
        }

        public static bool IsTextVisible(this Item item, GameplaySettings settings)
        {
            return item.GetAttributes<TextVisibleAttribute>().Any(a => a.Condition(settings));
        }

        public static bool IsModelVisible(this Item item, GameplaySettings settings)
        {
            return item.GetAttribute<ModelVisibleAttribute>()?.Condition(settings) == true;
        }

        public static bool IsShopModelVisible(this Item item)
        {
            return item.HasAttribute<ShopModelVisibleAttribute>();
        }

        public static bool IsExclusiveItem(this Item item)
        {
            return item.HasAttribute<ExclusiveItemAttribute>();
        }

        public static GetItemEntry ExclusiveItemEntry(this Item item)
        {
            return new GetItemEntry
            {
                ItemGained = item.GetAttribute<ExclusiveItemAttribute>().Item,
                Flag = item.GetAttribute<ExclusiveItemAttribute>().Flags,
                Index = item.GetAttribute<ExclusiveItemGraphicAttribute>().Graphic,
                Type = item.GetAttribute<ExclusiveItemAttribute>().Type,
                Message = item.GetAttribute<ExclusiveItemMessageAttribute>().Id,
                Object = item.GetAttribute<ExclusiveItemGraphicAttribute>().Object,
            };
        }

        public static string ExclusiveItemMessage(this Item item)
        {
            return item.GetAttribute<ExclusiveItemMessageAttribute>().Message;
        }

        public static bool IsBottleCatchContent(this Item item)
        {
            return item >= Item.BottleCatchFairy
                   && item <= Item.BottleCatchMushroom;
        }

        public static bool IsSameType(this Item item, Item other)
        {
            return item.IsBottleCatchContent() == other.IsBottleCatchContent();
        }

        public static bool IsBlockingBombTrapPlacement(this Item item)
        {
            return item.HasAttribute<BlockBombTrapPlacementAttribute>();
        }
    }
}
