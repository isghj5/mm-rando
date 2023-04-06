using MMR.Randomizer.Constants;
using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using System;
using System.Linq;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Attributes;
using System.Collections.ObjectModel;
using MMR.Randomizer.Models;
using MMR.Common.Extensions;
using MMR.Randomizer.Models.Settings;

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

        public static bool IsBottleCatchContent(Item item)
        {
            return item >= Item.BottleCatchFairy
                   && item <= Item.BottleCatchMushroom;
        }

        public static bool IsRegionRestricted(GameplaySettings settings, Item item)
        {
            if (settings.SmallKeyMode.HasFlag(SmallKeyMode.KeepWithinTemples) && SmallKeys().Contains(item))
            {
                return true;
            }

            if (settings.BossKeyMode.HasFlag(BossKeyMode.KeepWithinTemples) && BossKeys().Contains(item))
            {
                return true;
            }

            if (settings.StrayFairyMode.HasFlag(StrayFairyMode.KeepWithinTemples) && DungeonStrayFairies().Contains(item))
            {
                return true;
            }

            if (settings.BossRemainsMode.HasFlag(BossRemainsMode.KeepWithinTemples) && BossRemains().Contains(item))
            {
                return true;
            }

            if (settings.BossRemainsMode.HasFlag(BossRemainsMode.GreatFairyRewards) && BossRemains().Contains(item))
            {
                return true;
            }

            if (settings.DungeonNavigationMode.HasFlag(DungeonNavigationMode.KeepWithinTemples) && DungeonNavigation().Contains(item))
            {
                return true;
            }

            return false;
        }

        public static bool IsHinted(GameplaySettings settings, Item item, Item location)
        {
            if (settings.UpdateNPCText)
            {
                var npcTextHintedLocations = new List<Item>
                {
                    Item.UpgradeAdultWallet, // Hinted by the Banker
                    Item.SongEpona, // Hinted by the Keaton Quiz. TODO Keaton Quiz might be locked by this check
                    Item.ShopItemMilkBarChateau, // Hinted by the Keaton Quiz and by the Mr. Barten.
                    Item.UpgradeBigBombBag, // Hinted by the Bomb Shop and by the Old Lady
                    Item.ItemBottleBeavers, // Hinted by the Beavers and the Zora next to the jar game
                    Item.HeartPieceBeaverRace, // Hinted by the Beavers
                    Item.SongStorms, // Hinted by the grave
                    Item.ItemBottleDampe, // Hinted by the grave
                    Item.HeartPieceKnuckle, // Hinted by the grave
                };

                var npcTextHintedItems = new List<Item>
                {
                    Item.CollectibleStrayFairyClockTown, // Hinted by the Town Fairy Fountain
                    Item.SongOath, // Hinted by the Giants
                    Item.ItemPowderKeg, // Hinted by the Bomb Shop Goron
                    Item.ItemBottleGoronRace, // Hinted by the Smithy
                    Item.ItemBottleBeavers, // Hinted by Evan
                    Item.MaskGaro, // Hinted by the Road to Ikana ghost
                    Item.SongTime, // Hinted by the Scarecrow
                };
                npcTextHintedItems.AddRange(BossRemains()); // Hinted by Tatl and Tael

                if (npcTextHintedItems.Contains(item) || npcTextHintedLocations.Contains(location))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsStartingLocation(Item location)
        {
            return location == Item.MaskDeku || location == Item.SongHealing
                || (location >= Item.StartingSword && location <= Item.StartingHeartContainer2);
        }

        public static IEnumerable<Item> SmallKeys()
        {
            return new List<Item>
            {
                Item.ItemWoodfallKey1,
                Item.ItemSnowheadKey1,
                Item.ItemSnowheadKey2,
                Item.ItemSnowheadKey3,
                Item.ItemGreatBayKey1,
                Item.ItemStoneTowerKey1,
                Item.ItemStoneTowerKey2,
                Item.ItemStoneTowerKey3,
                Item.ItemStoneTowerKey4,
            }.AsEnumerable();
        }

        public static IEnumerable<Item> BossKeys()
        {
            return new List<Item>
            {
                Item.ItemWoodfallBossKey,
                Item.ItemSnowheadBossKey,
                Item.ItemGreatBayBossKey,
                Item.ItemStoneTowerBossKey,
            }.AsEnumerable();
        }

        public static IEnumerable<Item> DungeonStrayFairies()
        {
            return Enumerable.Range((int)Item.CollectibleStrayFairyWoodfall1, 60).Cast<Item>();
        }

        public static IEnumerable<Item> DungeonNavigation()
        {
            return new List<Item>
            {
                Item.ItemWoodfallMap,
                Item.ItemWoodfallCompass,
                Item.ItemSnowheadMap,
                Item.ItemSnowheadCompass,
                Item.ItemGreatBayMap,
                Item.ItemGreatBayCompass,
                Item.ItemStoneTowerMap,
                Item.ItemStoneTowerCompass,
            }.AsEnumerable();
        }

        public static IEnumerable<Item> BossRemains()
        {
            return Enumerable.Range((int)Item.RemainsOdolwa, 4).Cast<Item>();
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
                .Where(item => item.HasAttribute<StartingItemAttribute>()
                    || item.HasAttribute<StartingTingleMapAttribute>()
                    || item.HasAttribute<StartingItemIdAttribute>());
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
            return _allLocations ?? (_allLocations = Enum.GetValues<Item>().Where(item => item.Location() != null).ToList());
        }

        private static Dictionary<ClassicCategory, List<Item>> _itemsByClassicCategory;
        public static Dictionary<ClassicCategory, List<Item>> ItemsByClassicCategory()
        {
            return _itemsByClassicCategory ?? (_itemsByClassicCategory = AllLocations()
                .GroupBy(item => item.ClassicCategory())
                .Where(g => g.Key.HasValue)
                .ToDictionary(kvp => kvp.Key.Value, kvp => kvp.ToList()));
        }

        private static Dictionary<ItemCategory, List<Item>> _itemsByItemCategory;
        public static Dictionary<ItemCategory, List<Item>> ItemsByItemCategory()
        {
            return _itemsByItemCategory ?? (_itemsByItemCategory = AllLocations()
                .GroupBy(item => item.ItemCategory())
                .Where(g => g.Key.HasValue)
                .ToDictionary(kvp => kvp.Key.Value, kvp => kvp.ToList()));
        }

        private static Dictionary<LocationCategory, List<Item>> _itemsByLocationCategory;
        public static Dictionary<LocationCategory, List<Item>> ItemsByLocationCategory()
        {
            return _itemsByLocationCategory ?? (_itemsByLocationCategory = AllLocations()
                .GroupBy(item => item.LocationCategory())
                .Where(g => g.Key.HasValue)
                .ToDictionary(kvp => kvp.Key.Value, kvp => kvp.ToList()));
        }

        // todo cache
        public static IEnumerable<ushort> AllGetItemIndices()
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

        public static ReadOnlyCollection<Item> JunkItems { get; private set; }
        public static ReadOnlyCollection<Item> LogicallyJunkItems { get; private set; }
        public static void PrepareJunkItems(List<ItemObject> itemList)
        {
            LogicallyJunkItems = itemList
                .Where(io => !itemList.Any(other => (other.DependsOnItems?.Contains(io.Item) ?? false) || (other.Conditionals?.Any(c => c.Contains(io.Item)) ?? false)))
                .Select(io => io.Item)
                .ToList()
                .AsReadOnly();
            JunkItems = itemList.Where(io => io.Item != Item.CollectableIkanaGraveyardDay2Bats1
                                          && io.Item.IsRepeatable()
                                          && io.Item.GetAttribute<ChestTypeAttribute>()?.Type == ChestTypeAttribute.ChestType.SmallWooden
                                          && LogicallyJunkItems.Contains(io.Item)
                                      ).Select(io => io.Item).ToList().AsReadOnly();
        }

        public static bool IsJunk(Item item)
        {
            return item == Item.RecoveryHeart || item == Item.IceTrap || JunkItems.Contains(item);
        }

        public static bool IsLogicallyJunk(Item item)
        {
            return item == Item.RecoveryHeart || item == Item.IceTrap || LogicallyJunkItems.Contains(item);
        }

        private static List<Item> HintedJunkLocations;

        public static void PrepareHintedJunkLocations(GameplaySettings settings, Random random)
        {
            if (settings.OverrideHintPriorities != null && settings.OverrideHintItemCaps != null)
            {
                HintedJunkLocations = settings.OverrideHintPriorities.SelectMany((tier, i) =>
                {
                    var cap = settings.OverrideHintItemCaps.ElementAtOrDefault(i);
                    if (cap > 0)
                    {
                        var groupedLocations = tier.GroupBy(location => ItemCombinableHints.GetValueOrDefault(location).name ?? location.ToString())
                            .ToList();
                        var numberOfLocationsToJunk = groupedLocations.Count - cap;
                        return groupedLocations
                            .Random(numberOfLocationsToJunk, random)
                            .SelectMany(g => g)
                            .ToList();
                    }
                    return new List<Item>();
                }).ToList();
            }
            else
            {
                HintedJunkLocations = new List<Item>();
            }
        }

        public static bool IsLocationJunk(Item location, GameplaySettings settings)
        {
            return settings.CustomJunkLocations.Contains(location) || (HintedJunkLocations?.Contains(location) ?? false);
        }

        public static bool CanBeRequired(Item item)
        {
            return !item.Name().Contains("Heart")
                && !IsStrayFairy(item)
                && !IsSkulltulaToken(item)
                && item != Item.IceTrap;
        }

        public static bool IsRequired(Item item, Item locationForImportance, RandomizedResult randomizedResult, bool anythingCanBeRequired = false)
        {
            return (anythingCanBeRequired || CanBeRequired(item)) && randomizedResult.LocationsRequiredForMoonAccess?.Contains(locationForImportance) == true;
        }

        public static bool IsImportant(Item item, Item locationForImportance, RandomizedResult randomizedResult)
        {
            return !item.Name().Contains("Heart")
                && item != Item.IceTrap
                && randomizedResult.ImportantLocations?.Contains(locationForImportance) == true;
        }

        public static readonly ReadOnlyDictionary<string, ReadOnlyCollection<Item>> CombinableHints = new ReadOnlyDictionary<string, ReadOnlyCollection<Item>>(new Dictionary<string, ReadOnlyCollection<Item>>
        {
            {
                "Ranch Sisters Defense", new List<Item>
                {
                    Item.ItemBottleAliens,
                    Item.NotebookSaveTheCows,
                    Item.MaskRomani,
                    Item.NotebookProtectMilkDelivery
                }.AsReadOnly()
            },
            {
                "Beaver Races", new List<Item>
                {
                    Item.ItemBottleBeavers,
                    Item.HeartPieceBeaverRace
                }.AsReadOnly()
            },
            {
                "Bombers' Hide and Seek", new List<Item>
                {
                    Item.ItemNotebook,
                    Item.NotebookMeetBombers,
                    Item.NotebookLearnBombersCode,
                }.AsReadOnly()
            },
            {
                "Town Archery", new List<Item>
                {
                    Item.UpgradeBigQuiver,
                    Item.HeartPieceTownArchery
                }.AsReadOnly()
            },
            {
                "Swamp Archery", new List<Item>
                {
                    Item.UpgradeBiggestQuiver,
                    Item.HeartPieceSwampArchery
                }.AsReadOnly()
            },
            {
                "Ocean Spider House", new List<Item>
                {
                    Item.UpgradeGiantWallet,
                    Item.MundaneItemOceanSpiderHouseDay2PurpleRupee,
                    Item.MundaneItemOceanSpiderHouseDay3RedRupee
                }.AsReadOnly()
            },
            {
                "Midnight Meeting", new List<Item>
                {
                    Item.TradeItemKafeiLetter,
                    Item.NotebookPromiseAnjuDelivery
                }.AsReadOnly()
            },
            {
                "Letter to Kafei Delivery", new List<Item>
                {
                    Item.NotebookDepositLetterToKafei,
                    Item.TradeItemPendant,
                    Item.NotebookMeetKafei,
                    Item.NotebookPromiseKafei,
                    Item.MaskKeaton,
                    Item.TradeItemMamaLetter,
                    Item.NotebookCuriosityShopManSGift,
                    Item.NotebookPromiseCuriosityShopMan
                }.AsReadOnly()
            },
            {
                "Deku Playground", new List<Item>
                {
                    Item.HeartPieceDekuPlayground,
                    Item.MundaneItemDekuPlaygroundPurpleRupee
                }.AsReadOnly()
            },
            {
                "Honey and Darling", new List<Item>
                {
                    Item.HeartPieceHoneyAndDarling,
                    Item.MundaneItemHoneyAndDarlingPurpleRupee
                }.AsReadOnly()
            },
            {
                "Invisible Soldier", new List<Item>
                {
                    Item.MaskStone,
                    Item.NotebookMeetShiro,
                    Item.NotebookSaveInvisibleSoldier
                }.AsReadOnly()
            },
            {
                "Romani's Game", new List<Item>
                {
                    Item.SongEpona,
                    Item.NotebookPromiseRomani
                }.AsReadOnly()
            },
            {
                "Letter to Mama Delivery", new List<Item>
                {
                    Item.ItemBottleMadameAroma,
                    Item.NotebookDeliverLetterToMama
                }.AsReadOnly()
            },
            {
                "All-Night Mask Purchase", new List<Item>
                {
                    Item.MaskAllNight,
                    Item.NotebookPurchaseCuriosityShopItem
                }.AsReadOnly()
            },
        });

        public static readonly ReadOnlyDictionary<Item, (string name, ReadOnlyCollection<Item> locations)> ItemCombinableHints = new ReadOnlyDictionary<Item, (string, ReadOnlyCollection<Item>)>(CombinableHints.SelectMany(kvp => kvp.Value.Select(x => new { Name = kvp.Key, Locations = kvp.Value, Location = x })).ToDictionary(x => x.Location, x => (x.Name, x.Locations)));

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
                Item.UpgradeRoyalWallet,
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
