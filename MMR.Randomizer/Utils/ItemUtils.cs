using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using System;
using System.Linq;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Attributes;
using System.Collections.ObjectModel;
using MMR.Randomizer.Models;
using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.Utils
{
    public static class ItemUtils
    {
        public static bool IsShopItem(Item item)
        {
            return (item >= Item.ShopItemTradingPostRedPotion
                    && item <= Item.ShopItemZoraRedPotion)
                    || item == Item.ItemBombBag
                    || item == Item.UpgradeBigBombBag
                    || item == Item.MaskAllNight
                    || item == Item.ShopItemMilkBarChateau
                    || item == Item.ShopItemMilkBarMilk
                    || item == Item.ShopItemBusinessScrubMagicBean
                    || item == Item.ShopItemBusinessScrubGreenPotion
                    || item == Item.ShopItemBusinessScrubBluePotion
                    || item == Item.ShopItemGormanBrosMilk;
        }

        public static bool IsCowItem(Item item)
        {
            return (item >= Item.ItemRanchBarnMainCowMilk && item <= Item.ItemCoastGrottoCowMilk2);
        }

        public static bool IsSkulltulaToken(Item item)
        {
            return item >= Item.CollectibleSwampSpiderToken1 && item <= Item.CollectibleOceanSpiderToken30;
        }

        public static bool IsStrayFairy(Item item)
        {
            return item >= Item.CollectibleStrayFairyClockTown && item <= Item.CollectibleStrayFairyStoneTower15;
        }

        public static int AddItemOffset(int itemId)
        {
            if (itemId >= (int)Item.AreaSouthAccess)
            {
                itemId += Items.NumberOfAreasAndOther;
            }
            if (itemId >= (int)Item.OtherOneMask)
            {
                itemId += 5;
            }
            return itemId;
        }

        public static int SubtractItemOffset(int itemId)
        {
            if (itemId >= (int)Item.OtherOneMask)
            {
                itemId -= 5;
            }
            if (itemId >= (int)Item.AreaSouthAccess)
            {
                itemId -= Items.NumberOfAreasAndOther;
            }
            return itemId;
        }

        public static bool IsMoonLocation(Item location)
        {
            return location >= Item.HeartPieceDekuTrial && location <= Item.ChestLinkTrialBombchu10;
        }

        public static bool IsStartingLocation(Item location)
        {
            return location == Item.MaskDeku || location == Item.SongHealing
                || (location >= Item.StartingSword && location <= Item.StartingHeartContainer2);
        }

        public static bool IsSong(Item item)
        {
            return item >= Item.SongHealing
                && item <= Item.SongOath;
        }

        // todo cache
        public static IEnumerable<Item> DowngradableItems()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.IsDowngradable());
        }

        // todo cache
        public static IEnumerable<Item> OverwritableItems()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.IsOverwritable());
        }

        // todo cache
        public static IEnumerable<Item> StartingItems()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.HasAttribute<StartingItemAttribute>());
        }

        // todo cache
        public static IEnumerable<Item> AllRupees()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.Name()?.Contains("Rupee") == true);
        }
        
        private static List<Item> _allLocations;
        public static IEnumerable<Item> AllLocations()
        {
            return _allLocations ?? (_allLocations = Enum.GetValues(typeof(Item)).Cast<Item>().Where(item => item.HasAttribute<GetItemIndexAttribute>() || item.HasAttribute<GetBottleItemIndicesAttribute>()).ToList());
        }

        private static List<Item> _allEntrances;
        public static IEnumerable<Item> AllEntrances()
        {
            return _allEntrances ?? (_allEntrances = Enum.GetValues(typeof(Item)).Cast<Item>().Where(item => item.HasAttribute<EntranceAttribute>()).ToList());
        }

        // todo cache
        public static IEnumerable<int> AllGetItemIndices()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.HasAttribute<GetItemIndexAttribute>())
                .Select(item => item.GetAttribute<GetItemIndexAttribute>().Index);
        }

        // todo cache
        public static IEnumerable<int> AllGetBottleItemIndices()
        {
            return Enum.GetValues(typeof(Item))
                .Cast<Item>()
                .Where(item => item.HasAttribute<GetBottleItemIndicesAttribute>())
                .SelectMany(item => item.GetAttribute<GetBottleItemIndicesAttribute>().Indices);
        }

        public static List<Item> JunkItems { get; private set; }
        public static void PrepareJunkItems(List<ItemObject> itemList)
        {
            JunkItems = itemList.Where(io => io.Item.GetAttribute<ChestTypeAttribute>()?.Type == ChestTypeAttribute.ChestType.SmallWooden && !itemList.Any(other => (other.DependsOnItems?.Contains(io.Item) ?? false) || (other.Conditionals?.Any(c => c.Contains(io.Item)) ?? false))).Select(io => io.Item).ToList();
        }
        public static bool IsJunk(Item item)
        {
            return JunkItems.Contains(item);
        }

        public static bool IsRequired(Item item, RandomizedResult randomizedResult)
        {
            return randomizedResult.ItemsRequiredForMoonAccess != null
                && randomizedResult.ItemsRequiredForMoonAccess.Contains(item)
                && !item.Name().Contains("Heart")
                && !IsStrayFairy(item)
                && !IsSkulltulaToken(item);
        }

        public static bool IsImportant(Item item, RandomizedResult randomizedResult)
        {
            return randomizedResult.ImportantItems != null
                && randomizedResult.ImportantItems.Contains(item)
                && !item.Name().Contains("Heart");
        }

        public static readonly ReadOnlyCollection<ReadOnlyCollection<Item>> ForbiddenStartTogether = new List<List<Item>>()
        {
            new List<Item>
            {
                Item.ItemBow,
                Item.UpgradeBigQuiver,
                Item.UpgradeBiggestQuiver,
            },
            new List<Item>
            {
                Item.ItemBombBag,
                Item.UpgradeBigBombBag,
                Item.UpgradeBiggestBombBag,
            },
            new List<Item>
            {
                Item.UpgradeAdultWallet,
                Item.UpgradeGiantWallet,
            },
            new List<Item>
            {
                Item.StartingSword,
                Item.UpgradeRazorSword,
                Item.UpgradeGildedSword,
            },
            new List<Item>
            {
                Item.StartingShield,
                Item.ShopItemTradingPostShield,
                Item.ShopItemZoraShield,
                Item.UpgradeMirrorShield,
            },
            new List<Item>
            {
                Item.FairyMagic,
                Item.FairyDoubleMagic,
            },
        }.Select(list => list.AsReadOnly()).ToList().AsReadOnly();

        public static List<int> ConvertStringToIntList(string c)
        {
            var result = new List<int>();
            if (string.IsNullOrWhiteSpace(c))
            {
                return result;
            }
            try
            {
                result.Clear();
                string[] v = c.Split('-');
                int[] vi = new int[13];
                if (v.Length != vi.Length)
                {
                    return null;
                }
                for (int i = 0; i < 13; i++)
                {
                    if (v[12 - i] != "")
                    {
                        vi[i] = Convert.ToInt32(v[12 - i], 16);
                    }
                }
                for (int i = 0; i < 32 * 13; i++)
                {
                    int j = i / 32;
                    int k = i % 32;
                    if (((vi[j] >> k) & 1) > 0)
                    {
                        if (i >= ItemUtils.AllLocations().Count())
                        {
                            throw new IndexOutOfRangeException();
                        }
                        result.Add(i);
                    }
                }
            }
            catch
            {
                return null;
            }
            return result;
        }

        public static List<Item> ConvertStringToItemList(List<Item> baseItemList, string c)
        {
            if (string.IsNullOrWhiteSpace(c))
            {
                return new List<Item>();
            }
            var sectionCount = (int)Math.Ceiling(baseItemList.Count / 32.0);
            var result = new List<Item>();
            if (string.IsNullOrWhiteSpace(c))
            {
                return result;
            }
            try
            {
                string[] v = c.Split('-');
                int[] vi = new int[sectionCount];
                if (v.Length != vi.Length)
                {
                    return null;
                }
                for (int i = 0; i < sectionCount; i++)
                {
                    if (v[sectionCount - 1 - i] != "")
                    {
                        vi[i] = Convert.ToInt32(v[sectionCount - 1 - i], 16);
                    }
                }
                for (int i = 0; i < 32 * sectionCount; i++)
                {
                    int j = i / 32;
                    int k = i % 32;
                    if (((vi[j] >> k) & 1) > 0)
                    {
                        if (i >= baseItemList.Count)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        result.Add(baseItemList[i]);
                    }
                }
            }
            catch
            {
                return null;
            }
            return result;
        }

        public static string ConvertItemListToString(List<Item> baseItemList, List<Item> itemList)
        {
            var groupCount = (int)Math.Ceiling(baseItemList.Count / 32.0);
            int[] n = new int[groupCount];
            string[] ns = new string[groupCount];
            foreach (var item in itemList)
            {
                var i = baseItemList.IndexOf(item);
                int j = i / 32;
                int k = i % 32;
                n[j] |= (int)(1 << k);
                ns[j] = Convert.ToString(n[j], 16);
            }

            return string.Join("-", ns.Reverse());
        }
    }
}
