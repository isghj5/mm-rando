using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Constants;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.LogicMigrator;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MMR.Randomizer
{
    public class Randomizer
    {
        public static readonly string AssemblyVersion = typeof(Randomizer).Assembly.GetName().Version.ToString();

        private Random Random { get; set; }

        public ItemList ItemList { get; set; }

        #region Dependence and Conditions

        private class Dependence
        {
            public Item[] Items { get; set; }
            public DependenceType Type { get; set; }

            public static Dependence Dependent => new Dependence { Type = DependenceType.Dependent };
            public static Dependence NotDependent => new Dependence { Type = DependenceType.NotDependent };
            public static Dependence Circular(params Item[] items) => new Dependence { Items = items, Type = DependenceType.Circular };
        }

        private enum DependenceType
        {
            Dependent,
            NotDependent,
            Circular
        }

        // Starting items should not be replaced by trade items, or items that can be downgraded.
        private readonly List<Item> ForbiddenStartingItems = new List<Item>
            {
                // Starting with Magic Bean or Powder Keg doesn't actually give you one,
                // nor do you get one when you play Song of Time.
                Item.ItemMagicBean,
                Item.ItemPowderKeg,
            }
            .Concat(Enumerable.Range((int)Item.TradeItemMoonTear, Item.TradeItemMamaLetter - Item.TradeItemMoonTear + 1).Cast<Item>())
            .Concat(Enumerable.Range((int)Item.ItemBottleWitch, Item.ItemBottleMadameAroma - Item.ItemBottleWitch + 1).Cast<Item>())
            .ToList();

        private readonly Dictionary<Item, List<Item>> ForbiddenReplacedBy = new Dictionary<Item, List<Item>>
        {
            // Keaton_Mask and Mama_Letter are obtained one directly after another
            // Keaton_Mask cannot be replaced by items that may be overwritten by item obtained at Mama_Letter
            {
                Item.MaskKeaton, ItemUtils.OverwritableItems().ToList()
            },
        };

        private readonly Dictionary<Item, List<Item>> ForbiddenPlacedAt = new Dictionary<Item, List<Item>>
        {
        };

        #endregion

        private GameplaySettings _settings;
        private int _seed;
        private RandomizedResult _randomized;

        public Randomizer(GameplaySettings settings, int seed)
        {
            _settings = settings;
            _seed = seed;
            if (!_settings.PreventDowngrades)
            {
                ForbiddenReplacedBy[Item.MaskKeaton].AddRange(ItemUtils.DowngradableItems());
                ForbiddenStartingItems.AddRange(ItemUtils.DowngradableItems());
            }
        }

        //rando functions

        #region Gossip quotes

        private void MakeGossipQuotes()
        {
            _randomized.GossipQuotes = MessageUtils.MakeGossipQuotes(_randomized);
        }

        #endregion

        private void DungeonShuffle()
        {
            var newDCFlags = new int[] { -1, -1, -1, -1 };
            var newDCMasks = new int[] { -1, -1, -1, -1 };
            var newEntranceIndices = new int[] { -1, -1, -1, -1 };
            var newExitIndices = new int[] { -1, -1, -1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int n;
                do
                {
                    n = Random.Next(4);
                } while (newEntranceIndices.Contains(n));

                newEntranceIndices[i] = n;
                newExitIndices[n] = i;
            }

            var areaAccessObjects = new ItemObject[] {
                ItemList[Item.AreaWoodFallTempleAccess],
                ItemList[Item.AreaSnowheadTempleAccess],
                ItemList[Item.AreaInvertedStoneTowerTempleAccess],
                ItemList[Item.AreaGreatBayTempleAccess]
            };

            var areaAccessObjectIndexes = new int[] {
                (int)Item.AreaWoodFallTempleAccess,
                (int)Item.AreaSnowheadTempleAccess,
                (int)Item.AreaInvertedStoneTowerTempleAccess,
                (int)Item.AreaGreatBayTempleAccess
            };

            for (int i = 0; i < 4; i++)
            {
                //Debug.WriteLine($"Entrance {Item.ITEM_NAMES[areaAccessObjectIndexes[newEntranceIndices[i]]]} placed at {Item.ITEM_NAMES[areaAccessObjects[i].ID]}.");
                areaAccessObjects[i].IsRandomized = true;
                ItemList[areaAccessObjectIndexes[newEntranceIndices[i]]] = areaAccessObjects[i];
            }

            var areaClearObjects = new ItemObject[] {
                ItemList[Item.AreaWoodFallTempleClear],
                ItemList[Item.AreaSnowheadTempleClear],
                ItemList[Item.AreaStoneTowerClear],
                ItemList[Item.AreaGreatBayTempleClear]
            };

            var areaClearObjectIndexes = new int[] {
                (int)Item.AreaWoodFallTempleClear,
                (int)Item.AreaSnowheadTempleClear,
                (int)Item.AreaStoneTowerClear,
                (int)Item.AreaGreatBayTempleClear
            };

            for (int i = 0; i < 4; i++)
            {
                ItemList[areaClearObjectIndexes[i]] = areaClearObjects[newEntranceIndices[i]];
            }

            var newEntrances = new int[] { -1, -1, -1, -1 };
            var newExits = new int[] { -1, -1, -1, -1 };

            for (int i = 0; i < 4; i++)
            {
                newEntrances[i] = Values.OldEntrances[newEntranceIndices[i]];
                newExits[i] = Values.OldExits[newExitIndices[i]];
                newDCFlags[i] = Values.OldDCFlags[newExitIndices[i]];
                newDCMasks[i] = Values.OldMaskFlags[newExitIndices[i]];
            }

            _randomized.NewEntrances = newEntrances;
            _randomized.NewDestinationIndices = newEntranceIndices;
            _randomized.NewExits = newExits;
            _randomized.NewExitIndices = newExitIndices;
            _randomized.NewDCFlags = newDCFlags;
            _randomized.NewDCMasks = newDCMasks;
        }

        private void OwlShuffle(bool hidden)
        {
            int size = 12;
            int poolSize = size;
            _randomized.OwlStatueList = new int[size];
            for (int i = 0; i < _randomized.OwlStatueList.Length; i++)
            {
                _randomized.OwlStatueList[i] = -1;
            }
            if (!hidden)
            {
                _randomized.OwlStatueList[0] = 0;
                _randomized.OwlStatueList[8] = 8;
                _randomized.OwlStatueList[10] = 10;
            }
            int owl = 0;
            while (owl < _randomized.OwlStatueList.Length)
            {
                if (_randomized.OwlStatueList[owl] == -1)
                {
                    int n;
                    do
                    {
                        n = Random.Next(_randomized.OwlStatueList.Length);
                    } while (_randomized.OwlStatueList.Contains(n));

                    _randomized.OwlStatueList[owl] = n;
                    _randomized.OwlStatueList[n] = owl;
                }
                owl++;
            }
        }

        private void UpdateLogicForSettings()
        {
            foreach (var itemObject in ItemList)
            {
                if (_settings.CustomStartingItemList != null)
                {
                    itemObject.DependsOnItems?.RemoveAll(item => _settings.CustomStartingItemList.Contains(item));
                    itemObject.Conditionals?.ForEach(c => c.RemoveAll(item => _settings.CustomStartingItemList.Contains(item)));
                }

                if (itemObject.Conditionals != null)
                {
                    itemObject.Conditionals.RemoveAll(c => c.Any(item => ItemList[item].IsTrick && !_settings.EnabledTricks.Contains((int)item)));
                }
            }

            if (_settings.AddShopItems)
            {
                ItemList[Item.ShopItemWitchBluePotion].DependsOnItems?.Remove(Item.BottleCatchMushroom);
            }

            if (_settings.RandomizeBottleCatchContents && _settings.LogicMode == LogicMode.Casual)
            {
                var anyBottleIndex = ItemList.FindIndex(io => io.Name == "Any Bottle");
                var twoBottlesIndex = ItemList.FindIndex(io => io.Name == "2 Bottles");
                if (anyBottleIndex >= 0 && twoBottlesIndex >= 0)
                {
                    ItemList[Item.BottleCatchPrincess].DependsOnItems.Remove((Item)anyBottleIndex);
                    ItemList[Item.BottleCatchPrincess].DependsOnItems.Add((Item)twoBottlesIndex);
                }
            }

            if (_settings.ByoAmmo && _settings.LogicMode != LogicMode.NoLogic)
            {
                var arrows40 = new ItemObject
                {
                    ID = ItemList.Count,
                    TimeAvailable = 63,
                    Conditionals = new List<List<Item>>
                    {
                        new List<Item>
                        {
                            Item.UpgradeBigQuiver,
                        },
                        new List<Item>
                        {
                            Item.UpgradeBiggestQuiver,
                        },
                    },
                };
                ItemList.Add(arrows40);

                ItemList[Item.ChestInvertedStoneTowerBombchu10].TimeNeeded = 1;
                ItemList[Item.ChestLinkTrialBombchu10].TimeNeeded = 1;
                ItemList[Item.ShopItemBombsBombchu10].TimeNeeded = 1;
                var bombchu10 = new ItemObject
                {
                    ID = ItemList.Count,
                    TimeAvailable = 63,
                    Conditionals = new List<List<Item>>
                    {
                        new List<Item>
                        {
                            Item.ChestInvertedStoneTowerBombchu10,
                        },
                        new List<Item>
                        {
                            Item.ChestLinkTrialBombchu10,
                        },
                        new List<Item>
                        {
                            Item.ShopItemBombsBombchu10,
                        },
                    },
                };
                ItemList.Add(bombchu10);

                ItemList[Item.UpgradeBigQuiver].DependsOnItems.Add(arrows40.Item);
                ItemList[Item.UpgradeBiggestQuiver].DependsOnItems.Add(arrows40.Item);
                ItemList[Item.HeartPieceSwampArchery].DependsOnItems.Add(arrows40.Item);
                ItemList[Item.HeartPieceTownArchery].DependsOnItems.Add(Item.UpgradeBiggestQuiver);
                ItemList[Item.HeartPieceHoneyAndDarling].DependsOnItems.Add(bombchu10.Item);
                
                var escortCremia = new ItemObject
                {
                    ID = ItemList.Count,
                    TimeAvailable = 63,
                    Conditionals = new List<List<Item>>
                    {
                        new List<Item>
                        {
                            Item.OtherArrow,
                        },
                        new List<Item>
                        {
                            Item.MaskCircusLeader,
                        },
                    },
                };
                ItemList.Add(escortCremia);
                ItemList[Item.MaskRomani].DependsOnItems.Add(escortCremia.Item);
            }

            // todo handle progressive upgrades here.
        }

        private void PrepareRulesetItemData()
        {
            var logicMode = _settings.LogicMode;
            if (logicMode == LogicMode.NoLogic)
            {
                logicMode = _settings.EntranceLogicMode;
            }

            if (logicMode == LogicMode.Casual
                || logicMode == LogicMode.Glitched
                || logicMode == LogicMode.UserLogic)
            {
                string[] data = LogicUtils.ReadRulesetFromResources(logicMode, _settings.UserLogicFileName);
                ItemList = LogicUtils.PopulateItemListFromLogicData(data);
            }
            else
            {
                ItemList = LogicUtils.PopulateItemListWithoutLogic();
            }

            if (_settings.UseCustomItemList)
            {
                UpdateCustomItemListSettings();
            }

            UpdateLogicForSettings();

            ItemUtils.PrepareJunkItems(ItemList);
            if (_settings.CustomJunkLocations.Count > ItemUtils.JunkItems.Count)
            {
                throw new Exception($"Too many Enforced Junk Locations. Select up to {ItemUtils.JunkItems.Count}.");
            }
        }

        private void UpdateCustomItemListSettings()
        {
            if (_settings.CustomItemList.Contains(-1))
            {
                throw new Exception("Invalid custom item string.");
            }

            // Keep shop items vanilla, unless custom item list contains a shop item
            _settings.AddShopItems = false;

            // Keep cows vanilla, unless custom item list contains a cow
            _settings.AddCowMilk = false;

            // Keep skulltula tokens vanilla, unless custom item list contains a token
            _settings.AddSkulltulaTokens = false;

            // Keep stray fairies vanilla, unless custom item list contains a fairy
            _settings.AddStrayFairies = false;

            // Keep scoops vanilla, unless custom item list contains a scoop
            _settings.RandomizeBottleCatchContents = false;

            foreach (var item in _settings.CustomItemList.Select(ItemUtils.AddItemOffset).Cast<Item>())
            {
                if (ItemUtils.IsShopItem(item))
                {
                    _settings.AddShopItems = true;
                }

                if (ItemUtils.IsCowItem(item))
                {
                    _settings.AddCowMilk = true;
                }

                if (ItemUtils.IsSkulltulaToken(item))
                {
                    _settings.AddSkulltulaTokens = true;
                }

                if (ItemUtils.IsStrayFairy(item))
                {
                    _settings.AddStrayFairies = true;
                }

                if (item.IsBottleCatchContent())
                {
                    _settings.RandomizeBottleCatchContents = true;
                }
            }
        }

        private void SeedRNG()
        {
            Random = new Random(_seed);
        }

        private Dependence CheckDependence(Item currentItem, Item target, List<Item> dependencyPath, Dictionary<Item, Dependence> dependenciesChecked, List<int[]> conditionsToRemove, bool isEntrancePair = false)
        {
            Debug.WriteLine($"CheckDependence({currentItem}, {target})");
            var currentItemObject = ItemList[currentItem];
            var currentTargetObject = ItemList[target];

            if (currentTargetObject.IsTrick && !_settings.EnabledTricks.Contains(currentTargetObject.ID))
            {
                return Dependence.Dependent;
            }

            if (currentItemObject.TimeNeeded == 0 && ItemUtils.IsJunk(currentItem))
            {
                return Dependence.NotDependent;
            }

            if (currentItem.IsEntrance() && currentTargetObject.Name == "Inaccessible" && !isEntrancePair) // todo handle this more nicely with other logic.
            {
                return Dependence.Dependent;
            }

            if (target.IsEntrance()) // fuck let me meme
            {
                return Dependence.NotDependent;
            }


            //check timing
            if (currentItemObject.TimeNeeded != 0 && dependencyPath.Skip(1).All(p => p.IsFake() || ItemList.Single(i => i.NewLocation == p).Item.IsTemporary()))
            {
                if ((currentItemObject.TimeNeeded & currentTargetObject.TimeAvailable) == 0)
                {
                    Debug.WriteLine($"{currentItem} is needed at {currentItemObject.TimeNeeded} but {target} is only available at {currentTargetObject.TimeAvailable}");
                    return Dependence.Dependent;
                }
            }

            if (currentTargetObject.Conditionals.Any())
            {
                if (currentTargetObject.Conditionals.All(u => u.Contains(currentItem)))
                {
                    Debug.WriteLine($"All conditionals of {target} contains {currentItem}");
                    return Dependence.Dependent;
                }

                foreach (var cannotRequireItem in currentItemObject.CannotRequireItems)
                {
                    if (currentTargetObject.Conditionals.All(u => u.Contains(cannotRequireItem) || u.Contains(currentItem)))
                    {
                        Debug.WriteLine($"All conditionals of {target} cannot be required by {currentItem}");
                        return Dependence.Dependent;
                    }
                }

                int k = 0;
                var circularDependencies = new List<Item>();
                for (int i = 0; i < currentTargetObject.Conditionals.Count; i++)
                {
                    bool match = false;
                    for (int j = 0; j < currentTargetObject.Conditionals[i].Count; j++)
                    {
                        var d = currentTargetObject.Conditionals[i][j];
                        if (!d.IsFake() && !ItemList[d].NewLocation.HasValue && d != currentItem)
                        {
                            continue;
                        }

                        int[] check = new int[] { (int)target, i, j };

                        if (ItemList[d].NewLocation.HasValue)
                        {
                            d = ItemList[d].NewLocation.Value;
                        }
                        if (d == currentItem)
                        {
                            dependenciesChecked[d] = Dependence.Dependent;
                        }
                        else
                        {
                            if (dependencyPath.Contains(d))
                            {
                                dependenciesChecked[d] = Dependence.Circular(d);
                            }
                            if (!dependenciesChecked.ContainsKey(d) || (dependenciesChecked[d].Type == DependenceType.Circular && !dependenciesChecked[d].Items.All(id => dependencyPath.Contains(id))))
                            {
                                var childPath = dependencyPath.ToList();
                                childPath.Add(d);
                                dependenciesChecked[d] = CheckDependence(currentItem, d, childPath, dependenciesChecked, conditionsToRemove, isEntrancePair);
                            }
                        }

                        if (dependenciesChecked[d].Type != DependenceType.NotDependent)
                        {
                            if (!dependencyPath.Contains(d) && dependenciesChecked[d].Type == DependenceType.Circular && dependenciesChecked[d].Items.All(id => id == d))
                            {
                                dependenciesChecked[d] = Dependence.Dependent;
                            }
                            if (dependenciesChecked[d].Type == DependenceType.Dependent)
                            {
                                if (!conditionsToRemove.Any(c => c.SequenceEqual(check)))
                                {
                                    conditionsToRemove.Add(check);
                                }
                            }
                            else
                            {
                                circularDependencies = circularDependencies.Union(dependenciesChecked[d].Items).ToList();
                            }
                            if (!match)
                            {
                                k++;
                                match = true;
                            }
                        }
                    }
                }

                if (k == currentTargetObject.Conditionals.Count)
                {
                    if (circularDependencies.Any())
                    {
                        return Dependence.Circular(circularDependencies.ToArray());
                    }
                    Debug.WriteLine($"All conditionals of {target} failed dependency check for {currentItem}.");
                    return Dependence.Dependent;
                }
            }

            if (currentTargetObject.DependsOnItems == null)
            {
                return Dependence.NotDependent;
            }

            foreach (var cannotRequireItem in currentItemObject.CannotRequireItems)
            {
                if (currentTargetObject.DependsOnItems.Contains(cannotRequireItem))
                {
                    Debug.WriteLine($"Dependence {cannotRequireItem} of {target} cannot be required by {currentItem}");
                    return Dependence.Dependent;
                }
            }

            //cycle through all things
            foreach (var dependency in currentTargetObject.DependsOnItems)
            {
                if (!currentItem.IsTemporary() && target == Item.MaskBlast && (dependency == Item.TradeItemKafeiLetter || dependency == Item.TradeItemPendant))
                {
                    // Permanent items ignore Kafei Letter and Pendant on Blast Mask check.
                    continue;
                }
                if (dependency == currentItem)
                {
                    Debug.WriteLine($"{target} has direct dependence on {currentItem}");
                    return Dependence.Dependent;
                }

                if (dependency.IsFake()
                    || ItemList[dependency].NewLocation.HasValue)
                {
                    var location = ItemList[dependency].NewLocation ?? dependency;

                    if (dependencyPath.Contains(location))
                    {
                        dependenciesChecked[location] = Dependence.Circular(location);
                        return dependenciesChecked[location];
                    }
                    if (!dependenciesChecked.ContainsKey(location) || (dependenciesChecked[location].Type == DependenceType.Circular && !dependenciesChecked[location].Items.All(id => dependencyPath.Contains(id))))
                    {
                        var childPath = dependencyPath.ToList();
                        childPath.Add(location);
                        dependenciesChecked[location] = CheckDependence(currentItem, location, childPath, dependenciesChecked, conditionsToRemove, isEntrancePair);
                    }
                    if (dependenciesChecked[location].Type != DependenceType.NotDependent)
                    {
                        if (dependenciesChecked[location].Type == DependenceType.Circular && dependenciesChecked[location].Items.All(id => id == location))
                        {
                            dependenciesChecked[location] = Dependence.Dependent;
                        }
                        Debug.WriteLine($"{currentItem} is dependent on {location}");
                        return dependenciesChecked[location];
                    }
                }
            }

            return Dependence.NotDependent;
        }

        private void FinalizeRemoveConditionals()
        {
            foreach (var itemObject in ItemList)
            {
                itemObject.Conditionals.RemoveAll(u => u == null);
            }

        }

        private void PrepairRemoveConditionals(Item currentItem, List<int[]> conditionsToRemove)
        {
            foreach (var conditionRemove in conditionsToRemove)
            {
                int x = conditionRemove[0];
                int y = conditionRemove[1];
                int z = conditionRemove[2];
                if (ItemList[x].Conditionals.Count(c => c != null) > 1)
                {
                    ItemList[x].Conditionals[y] = null;
                }
            }
            
            foreach (var targetRemovals in conditionsToRemove.Select(cr => ItemList[cr[0]]))
            {
                foreach (var conditionals in targetRemovals.Conditionals)
                {
                    if (conditionals != null)
                    {
                        foreach (var d in conditionals)
                        {
                            if (!ItemList[d].CannotRequireItems.Contains(currentItem))
                            {
                                ItemList[d].CannotRequireItems.Add(currentItem);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateConditionals(Item currentItem, Item target)
        {
            var targetItemObject = ItemList[target];
            if (!targetItemObject.Conditionals.Any())
            {
                return;
            }

            if (targetItemObject.Conditionals.Count == 1)
            {
                foreach (var conditionalItem in targetItemObject.Conditionals[0])
                {
                    if (!targetItemObject.DependsOnItems.Contains(conditionalItem))
                    {
                        targetItemObject.DependsOnItems.Add(conditionalItem);
                    }
                    if (!ItemList[conditionalItem].CannotRequireItems.Contains(currentItem))
                    {
                        ItemList[conditionalItem].CannotRequireItems.Add(currentItem);
                    }
                }
                targetItemObject.Conditionals.RemoveAt(0);
            }
            else
            {
                //check if all conditions have a common item
                var commonConditionals = targetItemObject.Conditionals[0].Where(c => targetItemObject.Conditionals.All(cs => cs.Contains(c))).ToList();
                foreach (var commonConditional in commonConditionals)
                {
                    // require this item and remove from conditions
                    if (!targetItemObject.DependsOnItems.Contains(commonConditional))
                    {
                        targetItemObject.DependsOnItems.Add(commonConditional);
                    }
                    foreach (var conditional in targetItemObject.Conditionals)
                    {
                        conditional.Remove(commonConditional);
                    }
                    if (targetItemObject.Conditionals.Any(cs => !cs.Any()))
                    {
                        targetItemObject.Conditionals.Clear();
                    }
                }
            };
        }

        private void AddConditionals(Item target, Item currentItem, int d)
        {
            var targetId = (int)target;
            var baseConditionals = ItemList[targetId].Conditionals;

            if (baseConditionals == null)
            {
                baseConditionals = new List<List<Item>>();
            }

            ItemList[targetId].Conditionals = new List<List<Item>>();
            foreach (var conditions in ItemList[d].Conditionals)
            {
                if (!conditions.Contains(currentItem))
                {
                    var newConditional = new List<List<Item>>();
                    if (baseConditionals.Count == 0)
                    {
                        newConditional.Add(conditions);
                    }
                    else
                    {
                        foreach (var baseConditions in baseConditionals)
                        {
                            newConditional.Add(baseConditions.Concat(conditions).ToList());
                        }
                    }

                    ItemList[targetId].Conditionals.AddRange(newConditional);
                }
            }
        }

        private void CheckConditionals(Item currentItem, Item target, List<Item> dependencyPath, List<Item> conditionsChecked)
        {
            var targetItemObject = ItemList[target];
            if (target == Item.MaskBlast)
            {
                if (!currentItem.IsTemporary())
                {
                    targetItemObject.DependsOnItems?.Remove(Item.TradeItemKafeiLetter);
                    targetItemObject.DependsOnItems?.Remove(Item.TradeItemPendant);
                }
            }

            conditionsChecked.Add(target);
            UpdateConditionals(currentItem, target);

            foreach (var dependency in targetItemObject.DependsOnItems)
            {
                var dependencyObject = ItemList[dependency];
                if (!dependencyObject.CannotRequireItems.Contains(currentItem))
                {
                    dependencyObject.CannotRequireItems.Add(currentItem);
                }
                if (dependency.IsFake() || dependencyObject.NewLocation.HasValue)
                {
                    var location = dependencyObject.NewLocation ?? dependency;

                    if (!conditionsChecked.Contains(location))
                    {
                        var childPath = dependencyPath.ToList();
                        childPath.Add(location);
                        CheckConditionals(currentItem, location, childPath, conditionsChecked);
                    }
                }
                else if (ItemList[currentItem].TimeNeeded != 0 && dependency.IsTemporary() && dependencyPath.Skip(1).All(p => p.IsFake() || ItemList.Single(j => j.NewLocation == p).Item.IsTemporary()))
                {
                    if (dependencyObject.TimeNeeded == 0)
                    {
                        dependencyObject.TimeNeeded = ItemList[currentItem].TimeNeeded;
                    }
                    else
                    {
                        dependencyObject.TimeNeeded &= ItemList[currentItem].TimeNeeded;
                    }
                }
            }

            // todo double check this
            //ItemList[target].DependsOnItems.RemoveAll(u => u == -1);
        }

        private bool CheckMatch(Item currentItem, Item target)
        {
            if (ItemList.Any(io => io.NewLocation == target))
            {
                return false;
            }

            if (_settings.CustomStartingItemList.Contains(currentItem))
            {
                return true;
            }

            if (ItemUtils.IsStartingLocation(target) && ForbiddenStartingItems.Contains(currentItem))
            {
                Debug.WriteLine($"{currentItem} cannot be a starting item.");
                return false;
            }

            if (!_settings.DecoupleEntrances && target == Item.EntranceSouthClockTownFromClockTowerInterior)
            {
                var pair = currentItem.Pair();
                if (!pair.HasValue)
                {
                    Debug.WriteLine($"{currentItem} cannot be a starting location.");
                    return false;
                }
            }

            if (currentItem.IsEntrance() && _settings.LogicMode == LogicMode.NoLogic && _settings.EntranceLogicMode == LogicMode.NoLogic)
            {
                return true;
            }

            if (!currentItem.IsEntrance() && _settings.LogicMode == LogicMode.NoLogic)
            {
                return true;
            }

            if (_settings.CustomJunkLocations.Contains(target) && !ItemUtils.IsJunk(currentItem))
            {
                return false;
            }

            if (ForbiddenPlacedAt.ContainsKey(currentItem)
                && ForbiddenPlacedAt[currentItem].Contains(target))
            {
                Debug.WriteLine($"{currentItem} forbidden from being placed at {target}");
                return false;
            }

            if (ForbiddenReplacedBy.ContainsKey(target) && ForbiddenReplacedBy[target].Contains(currentItem))
            {
                Debug.WriteLine($"{target} forbids being replaced by {currentItem}");
                return false;
            }

            if (currentItem.IsTemporary() && (ItemUtils.IsMoonLocation(target) || target == Item.ItemOcarina || target == Item.SongTime))
            {
                Debug.WriteLine($"{currentItem} cannot be placed on the moon.");
                return false;
            }

            //check direct dependence
            var conditionsRemoved = false;
            var conditionsToRemove = new List<int[]>();
            var dependenciesChecked = new Dictionary<Item, Dependence> { { target, new Dependence { Type = DependenceType.Dependent } } };
            var dependencyPath = new List<Item> { target };



            if (   CheckDependence(currentItem, target, dependencyPath, dependenciesChecked, conditionsToRemove).Type != DependenceType.NotDependent)
            {
                return false;
            }

            if (!_settings.DecoupleEntrances)
            {
                var currentEntrancePair = currentItem.Pair();
                var targetPair = target.Pair();

                if (currentEntrancePair != null && targetPair != null)
                {
                    ItemList[currentItem].NewLocation = target;

                    var pairItem = targetPair.Value;
                    var pairTarget = currentEntrancePair.Value;
                    var pairPath = new List<Item> { pairTarget };
                    var pairConditionsToRemove = new List<int[]>();
                    var pairDependenciesChecked = new Dictionary<Item, Dependence> { { pairTarget, new Dependence { Type = DependenceType.Dependent } } };
                    var pairDependence = CheckDependence(pairItem, pairTarget, pairPath, pairDependenciesChecked, pairConditionsToRemove, true);
                    ItemList[currentItem].NewLocation = null;

                    if (pairDependence.Type != DependenceType.NotDependent)
                    {
                        return false;
                    }

                    PrepairRemoveConditionals(pairItem, pairConditionsToRemove);
                    PrepairRemoveConditionals(currentItem, conditionsToRemove);
                    FinalizeRemoveConditionals();
                    conditionsRemoved = true;

                    var pairConditionsChecked = new List<Item>();
                    CheckConditionals(pairItem, pairTarget, pairPath, pairConditionsChecked);
                }
            }

            if (!conditionsRemoved)
            {
                PrepairRemoveConditionals(currentItem, conditionsToRemove);
                FinalizeRemoveConditionals();
            }

            //check conditional dependence
            var conditionsChecked = new List<Item>();
            CheckConditionals(currentItem, target, dependencyPath, conditionsChecked);

            return true;
        }

        private void PlaceItem(Item currentItem, List<Item> targets)
        {
            var currentItemObject = ItemList[currentItem];
            if (currentItem.IsFake())
            {
                foreach (var requiredItem in currentItemObject.DependsOnItems.Where(item => item.IsSameType(currentItem)))
                {
                    PlaceItem(requiredItem, targets);
                }
                return;
            }
            if (currentItemObject.NewLocation.HasValue)
            {
                return;
            }

            targets = targets.FindAll(u => ! u.IsEntrance() );

            var availableItems = targets.ToList();
            if (!currentItem.HasAttribute<StartingItemAttribute>())
            {
                availableItems.RemoveAll(item => ItemUtils.IsStartingLocation(item));
            }

            while (true)
            {
                if (availableItems.Count == 0)
                {
                    throw new RandomizationException($"Unable to place {currentItem.Name() ?? currentItem.Entrance() ?? currentItem.ToString()} anywhere.");
                }

                var targetLocation = availableItems.Random(Random);// Random.Next(availableItems.Count);

                Debug.WriteLine($"----Attempting to place {currentItem.Name()} at {targetLocation.Location()}.---");

                if (CheckMatch(currentItem, targetLocation))
                {
                    currentItemObject.NewLocation = targetLocation;
                    currentItemObject.IsRandomized = true;

                    Debug.WriteLine($"----Placed {currentItem.Name()} at {targetLocation.Location()}----");

                    targets.Remove(targetLocation);

                    if (!_settings.DecoupleEntrances)
                    {
                        var currentEntrancePair = currentItem.Pair();
                        var targetEntrancePair = targetLocation.Pair();
                        if (currentEntrancePair != null && targetEntrancePair != null)
                        {
                            var targetPairItemObject = ItemList[targetEntrancePair.Value];
                            targetPairItemObject.NewLocation = currentEntrancePair;
                            targetPairItemObject.IsRandomized = true;
                            targets.Remove(currentEntrancePair.Value);
                        }
                    }
                    break;
                }
                else
                {
                    Debug.WriteLine($"----Failed to place {currentItem.Name()} at {targetLocation.Location()}----");
                    availableItems.Remove(targetLocation);
                }
            }

            if (!currentItem.IsEntrance() && targets.Any() && !ItemUtils.IsJunk(currentItem))
            {
                var location = ItemList[currentItemObject.NewLocation.Value];
                foreach (var requiredItem in location.DependsOnItems.Where(item => item.IsSameType(currentItem)))
                {
                    PlaceItem(requiredItem, targets);
                }
                var conditional = location.Conditionals.RandomOrDefault(Random);
                if (conditional != null)
                {
                    foreach (var item in conditional.Where(item => item.IsSameType(currentItem)))
                    {
                        PlaceItem(item, targets);
                    }
                }
            }
        }

        private void RandomizeItems()
        {
            if (_settings.UseCustomItemList)
            {
                SetupCustomItems();
            }
            else
            {
                Setup();
            }

            if (_settings.SwapMajoraAndCallGiants)
            {
                ItemList[Item.EntranceMajorasLairFromTheMoon].IsRandomized = true;
                ItemList[Item.EntranceMajorasLairFromTheMoon].NewLocation = _settings.RandomizedEntrances.Contains(Item.EntranceMajorasLairFromTheMoon)
                    ? (Item?)null
                    : Item.EntranceTheMoonFromClockTowerRooftop;

                ItemList[Item.EntranceTheMoonFromClockTowerRooftop].IsRandomized = true;
                ItemList[Item.EntranceTheMoonFromClockTowerRooftop].NewLocation = _settings.RandomizedEntrances.Contains(Item.EntranceTheMoonFromClockTowerRooftop)
                    ? (Item?)null
                    : Item.EntranceMajorasLairFromTheMoon;
            }

            PreserveOwlActivations();

            UpdateLogicForSettings();

            PlaceUnimplementedEntrances();

            PlaceOneWayEntrances();

            if (!_settings.DecoupleEntrances)
            {
                PlacePairedEntrances();
            }
            //PreserveEntrances(); // the fuck is this

            var itemPool = new List<Item>();

            AddAllItems(itemPool);

            PlacePlandoItems(itemPool);
            PlaceFreeItems(itemPool);
            PlaceOcarinaAndSongOfTime(itemPool);
            PlaceQuestItems(itemPool);
            PlaceTradeItems(itemPool);
            PlaceDungeonItems(itemPool);
            PlaceStartingItems(itemPool);
            PlaceUpgrades(itemPool);
            PlaceSongs(itemPool);
            PlaceMasks(itemPool);
            PlaceRegularItems(itemPool);
            PlaceSkulltulaTokens(itemPool);
            PlaceStrayFairies(itemPool);
            PlaceMundaneRewards(itemPool);
            PlaceShopItems(itemPool);
            PlaceCowMilk(itemPool);
            PlaceMoonItems(itemPool);
            PlaceHeartpieces(itemPool);
            PlaceOther(itemPool);
            PlaceTingleMaps(itemPool);

            _randomized.ItemList = ItemList;
        }

        private void PlaceUnimplementedEntrances()
        {
            var entrancesToPlace = ItemUtils.AllEntrances()
                .Where(item => !item.IsEntranceImplemented())
                .ToList();
            var entrancePool = entrancesToPlace.ToList();
            foreach (var entrance in entrancesToPlace)
            {
                ItemList[entrance].NewLocation = entrance;
                ItemList[entrance].IsRandomized = false;
                if (!_settings.DecoupleEntrances)
                {
                    var pair = ItemList[entrance].NewLocation?.Pair();
                    if (pair != null)
                    {
                        ItemList[pair.Value].NewLocation = pair;
                        ItemList[pair.Value].IsRandomized = false;
                    }
                }
            }
        }

        private void PlaceOneWayEntrances()
        {
            var allOneWays = ItemUtils.AllEntrances()
                .Where(item => item.IsEntranceImplemented())
                .Where(item => _settings.DecoupleEntrances || !item.Pair().HasValue).ToList();
            var entrancesToPlace = allOneWays.Where(item => ItemList[item].NewLocation == null).ToList();
            var entrancePool = allOneWays.Where(location => !ItemList.Any(io => io.NewLocation == location)).ToList();
            // todo remove already-placed entrances
            foreach (var entrance in entrancesToPlace)
            {
                PlaceItem(entrance, entrancePool);
            }
        }

        private List<Item> GetUnconnectedEntrances(Item item, List<Item> checkedEntrances = null)
        {
            if (checkedEntrances == null)
            {
                checkedEntrances = new List<Item>();
            }
            if (checkedEntrances.Contains(item))
            {
                return new List<Item>();
            }
            checkedEntrances.Add(item);
            var itemObject = ItemList[item];
            return (itemObject.DependsOnItems?.SelectMany(d =>
            {
                if (d.IsEntrance())
                {
                    return new List<Item> { d };
                }
                if (d.IsFake())
                {
                    return GetUnconnectedEntrances(d, checkedEntrances);
                }
                return Enumerable.Empty<Item>();
            }) ?? new List<Item>())
            .Concat(itemObject.Conditionals?.SelectMany(cs => cs.SelectMany(d =>
            {
                if (d.IsEntrance())
                {
                    return new List<Item> { d };
                }
                if (d.IsFake())
                {
                    return GetUnconnectedEntrances(d, checkedEntrances);
                }
                return Enumerable.Empty<Item>();
            })) ?? new List<Item>()).Distinct().ToList();
        }

        private void PlacePairedEntrances()
        {
            var allPairedEntrances = ItemUtils.AllEntrances()
                .Where(item => item.Pair().HasValue)
                .Where(item => item.IsEntranceImplemented() && item.Pair().Value.IsEntranceImplemented())
                .ToList();
            var entrancesToPlace = allPairedEntrances.Where(item => ItemList[item].NewLocation == null).ToList();
            var entrancePool = allPairedEntrances.Where(location => !ItemList.Any(io => io.NewLocation == location)).ToList();

            entrancePool.Remove(Item.EntranceSouthClockTownFromClockTowerInterior);

            var unconnectedEntrances = entrancesToPlace.ToDictionary(item => item, item => GetUnconnectedEntrances(item));

            var inaccessibleEntrances = unconnectedEntrances.Where(kvp => kvp.Value.Contains(Item.EntranceMajorasLairFromTheMoon)).Select(kvp => kvp.Key).ToList();

            foreach (var entrance in inaccessibleEntrances)
            {
                PlaceEntrance(entrance.Pair().Value, entrancePool, unconnectedEntrances);
            }

            foreach (var kvp in unconnectedEntrances)
            {
                kvp.Value.RemoveAll(item => ItemList[item].NewLocation != null);
            }

            var disconnectedEntrances = entrancesToPlace.Where(item => !unconnectedEntrances.Any(kvp => kvp.Value.Contains(item))).ToList();
            disconnectedEntrances.Remove(Item.EntranceClockTowerInteriorFromSouthClockTown);

            while (disconnectedEntrances.Any() && entrancePool.Count > 1)
            {
                var currentEntrance = disconnectedEntrances.Random(Random);

                PlaceEntrance(currentEntrance, entrancePool, unconnectedEntrances);

                var pair = ItemList[currentEntrance].NewLocation?.Pair().Value;

                disconnectedEntrances.Remove(currentEntrance);
                if (pair.HasValue)
                {
                    disconnectedEntrances.Remove(pair.Value);
                }
            }

            while (unconnectedEntrances.SelectMany(kvp => kvp.Value).Distinct().Count() > 1)
            {
                var currentEntrance = unconnectedEntrances.Where(g => g.Value.Count > 0).OrderBy(g => g.Value.Count).First().Value.Random(Random);

                PlaceEntrance(currentEntrance, entrancePool, unconnectedEntrances);
            }

            // place spawn point
            PlaceItem(Item.EntranceClockTowerInteriorFromSouthClockTown, entrancePool);
        }

        private void PlaceEntrance(Item entrance, List<Item> entrancePool, Dictionary<Item, List<Item>> unconnectedEntrances)
        {
            if (entrance != Item.EntranceClockTowerInteriorFromSouthClockTown)
            {
                PlaceItem(entrance, entrancePool);
            }

            var pair = ItemList[entrance].NewLocation?.Pair().Value;
            foreach (var kvp in unconnectedEntrances)
            {
                kvp.Value.Remove(entrance);
                if (!_settings.DecoupleEntrances)
                {
                    if (pair.HasValue)
                    {
                        kvp.Value.Remove(pair.Value);
                    }
                }
            }
            unconnectedEntrances = unconnectedEntrances.Where(kvp => kvp.Value.Count > 0).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Places plando items in the randomization pool
        ///   re-implements PlaceItem for plando
        ///   reasons: we need pass a different list of checks, but itemPool still needs to be decremented
        ///            we want a different error, stating plando failed not just any randomization
        ///            for plando, we want the ability to bypass logic
        ///            for skipped logic, we still need to make sure certain items are not placed in starting slots
        /// </summary>
        /// <param name="itemPool"></param>
        private void PlacePlandoItems(List<Item> itemPool)
        {

            // place special ice traps first
            //create new ice trap
            // place trap in location
            // remove other ice trap from the list of traps already created?


            List<PlandoItemCombo> plandoItemCombos = PlandoUtils.ReadAllItemPlandoFiles(itemPool);
            if (plandoItemCombos != null)
            {
                foreach (PlandoItemCombo pic in plandoItemCombos)
                {
                    var itemCombo = PlandoUtils.CleanItemCombo(pic, Random, itemPool, ItemList);
                    if (itemCombo == null)
                    {
                        throw new Exception("Error: Plando failed to build with this seed, combo: " + pic.Name);
                    }

                    int drawCount = 0;
                    for(int itemCount = 0; drawCount < itemCombo.ItemDrawCount && itemCount < itemCombo.ItemList.Count; itemCount++)
                    {
                        /// for all items, attempt to add; count successes
                        Item item = itemCombo.ItemList[itemCount];
                        foreach (Item check in itemCombo.CheckList)
                        {
                            if (itemCombo.SkipLogic && ItemUtils.IsStartingLocation(check) && ForbiddenStartingItems.Contains(item))
                            {
                                Debug.WriteLine("Cannot place forbidden item in starting location: " + item.Name());
                                continue;
                            }

                            if (itemCombo.SkipLogic || CheckMatch(item, check))
                            {
                                ItemList[item].NewLocation = check;
                                ItemList[item].IsRandomized = true;

                                Debug.WriteLine($"----Plando Placed {item.Name()} at {check.Location()}----");

                                itemPool.Remove(check);
                                itemCombo.CheckList.Remove(check);
                                drawCount++;
                                break;
                            }
                        }
                    }
                    if (drawCount < itemCombo.ItemDrawCount)
                    {
                        throw new Exception("Error: Plando could not find enough checks to match this plandos items with this seed: " + itemCombo.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Places starting items in the randomization pool.
        /// </summary>
        private void PlaceStartingItems(List<Item> itemPool)
        {
            for (var i = Item.StartingSword; i <= Item.StartingHeartContainer2; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places moon items in the randomization pool.
        /// </summary>
        private void PlaceMoonItems(List<Item> itemPool)
        {
            for (var i = Item.HeartPieceDekuTrial; i <= Item.ChestLinkTrialBombchu10; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places tingle maps in the randomization pool.
        /// </summary>
        private void PlaceTingleMaps(List<Item> itemPool)
        {
            for (var i = Item.ItemTingleMapTown; i <= Item.ItemTingleMapStoneTower; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places skulltula tokens in the randomization pool.
        /// </summary>
        private void PlaceSkulltulaTokens(List<Item> itemPool)
        {
            for (var i = Item.CollectibleSwampSpiderToken1; i <= Item.CollectibleOceanSpiderToken30; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places stray fairies in the randomization pool.
        /// </summary>
        private void PlaceStrayFairies(List<Item> itemPool)
        {
            for (var i = Item.CollectibleStrayFairyClockTown; i <= Item.CollectibleStrayFairyStoneTower15; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places mundane rewards in the randomization pool.
        /// </summary>
        private void PlaceMundaneRewards(List<Item> itemPool)
        {
            for (var i = Item.MundaneItemLotteryPurpleRupee; i <= Item.MundaneItemSeahorse; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places other chests and grottos in the randomization pool.
        /// </summary>
        /// <param name="itemPool"></param>
        private void PlaceOther(List<Item> itemPool)
        {
            for (var i = Item.ChestLensCaveRedRupee; i <= Item.ChestSouthClockTownPurpleRupee; i++)
            {
                PlaceItem(i, itemPool);
            }

            PlaceItem(Item.ChestToGoronRaceGrotto, itemPool);
            PlaceItem(Item.IkanaScrubGoldRupee, itemPool);
            PlaceItem(Item.ChestPreClocktownDekuNut, itemPool);
        }

        /// <summary>
        /// Places heart pieces in the randomization pool. Includes rewards/chests, as well as standing heart pieces.
        /// </summary>
        private void PlaceHeartpieces(List<Item> itemPool)
        {
            // Rewards/chests
            for (var i = Item.HeartPieceNotebookMayor; i <= Item.HeartPieceKnuckle; i++)
            {
                PlaceItem(i, itemPool);
            }

            // Bank reward
            PlaceItem(Item.HeartPieceBank, itemPool);

            // Standing heart pieces
            for (var i = Item.HeartPieceSouthClockTown; i <= Item.HeartContainerStoneTower; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places shop items in the randomization pool
        /// </summary>
        private void PlaceShopItems(List<Item> itemPool)
        {
            for (var i = Item.ShopItemTradingPostRedPotion; i <= Item.ShopItemZoraRedPotion; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places cow milk in the randomization pool
        /// </summary>
        private void PlaceCowMilk(List<Item> itemPool)
        {
            for (var i = Item.ItemRanchBarnMainCowMilk; i <= Item.ItemCoastGrottoCowMilk2; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places dungeon items in the randomization pool
        /// </summary>
        private void PlaceDungeonItems(List<Item> itemPool)
        {
            for (var i = Item.ItemWoodfallMap; i <= Item.ItemStoneTowerKey4; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places songs in the randomization pool
        /// </summary>
        private void PlaceSongs(List<Item> itemPool)
        {
            for (var i = Item.SongHealing; i <= Item.SongOath; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places masks in the randomization pool
        /// </summary>
        private void PlaceMasks(List<Item> itemPool)
        {
            for (var i = Item.MaskPostmanHat; i <= Item.MaskZora; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places upgrade items in the randomization pool
        /// </summary>
        private void PlaceUpgrades(List<Item> itemPool)
        {
            for (var i = Item.UpgradeRazorSword; i <= Item.UpgradeGiantWallet; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places regular items in the randomization pool
        /// </summary>
        private void PlaceRegularItems(List<Item> itemPool)
        {
            for (var i = Item.MaskDeku; i <= Item.ItemNotebook; i++)
            {
                PlaceItem(i, itemPool);
            }

            PlaceItem(Item.ChestInvertedStoneTowerBean, itemPool);
            PlaceItem(Item.ShopItemBusinessScrubMagicBean, itemPool);
        }

        /// <summary>
        /// Replace starting deku mask and song of healing with free items if not already replaced.
        /// </summary>
        private void PlaceFreeItems(List<Item> itemPool)
        {
            var freeItemLocations = new List<Item>
            {
                Item.MaskDeku,
                Item.SongHealing,
                Item.StartingShield,
                Item.StartingSword,
                Item.StartingHeartContainer1,
                Item.StartingHeartContainer2,
            };
            var availableStartingItems = (_settings.NoStartingItems
                ? ItemUtils.AllRupees()
                : ItemUtils.StartingItems())
                .Where(item => !ItemList[item].NewLocation.HasValue && !ForbiddenStartingItems.Contains(item))
                .Cast<Item?>()
                .ToList();
            foreach (var location in freeItemLocations)
            {
                var placedItem = ItemList.FirstOrDefault(item => item.NewLocation == location)?.Item;
                if (placedItem == null)
                {
                    placedItem = availableStartingItems.RandomOrDefault(Random);
                    if (placedItem == null)
                    {
                        throw new Exception("Failed to replace a starting item.");
                    }
                    ItemList[placedItem.Value].NewLocation = location;
                    ItemList[placedItem.Value].IsRandomized = true;
                    itemPool.Remove(location);
                    availableStartingItems.Remove(placedItem.Value);
                }


                var forbiddenStartTogether = ItemUtils.ForbiddenStartTogether.FirstOrDefault(list => list.Contains(placedItem.Value));
                if (forbiddenStartTogether != null)
                {
                    availableStartingItems.RemoveAll(item => forbiddenStartTogether.Contains(item.Value));
                }
            }
        }

        /// <summary>
        /// Adds all items into the randomization pool (excludes area/other and items that already have placement)
        /// </summary>
        private void AddAllItems(List<Item> itemPool)
        {
            itemPool.AddRange(ItemUtils.AllLocations().Where(location => !ItemList.Any(io => io.NewLocation == location)));
        }

        private void PlaceOcarinaAndSongOfTime(List<Item> itemPool)
        {
            PlaceItem(Item.ItemOcarina, itemPool);
            PlaceItem(Item.SongTime, itemPool);
        }

        private void AddAllEntrances(List<Item> itemPool)
        {
            itemPool.AddRange(ItemUtils.AllEntrances().Where(location => !ItemList.Any(io => io.NewLocation == location)));
        }

        /// <summary>
        /// Places quest items in the randomization pool
        /// </summary>
        private void PlaceQuestItems(List<Item> itemPool)
        {
            for (var i = Item.TradeItemRoomKey; i <= Item.TradeItemMamaLetter; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Places trade items in the randomization pool
        /// </summary>
        private void PlaceTradeItems(List<Item> itemPool)
        {
            for (var i = Item.TradeItemMoonTear; i <= Item.TradeItemOceanDeed; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Adds items to randomization pool based on settings.
        /// </summary>
        private void Setup()
        {
            if (_settings.ExcludeSongOfSoaring)
            {
                ItemList[Item.SongSoaring].NewLocation = Item.SongSoaring;
            }

            if (!_settings.AddSongs)
            {
                ShuffleSongs();
            }

            if (!_settings.AddDungeonItems)
            {
                PreserveDungeonItems();
            }

            if (!_settings.AddShopItems)
            {
                PreserveShopItems();
            }

            if (!_settings.AddOther)
            {
                PreserveOther();
            }

            if (_settings.RandomizeBottleCatchContents)
            {
                AddBottleCatchContents();
            }
            else
            {
                PreserveBottleCatchContents();
            }

            if (!_settings.AddMoonItems)
            {
                PreserveMoonItems();
            }

            if (!_settings.AddFairyRewards)
            {
                PreserveFairyRewards();
            }

            if (!_settings.AddNutChest 
                || (_settings.LogicMode == LogicMode.Casual 
                    && !_settings.RandomizedEntrances.Contains(Item.EntranceBeforethePortaltoTerminaFromBeforethePortaltoTermina)
                    && !_settings.RandomizedEntrances.Contains(Item.EntranceBeforethePortaltoTerminaFromClockTowerInterior)))
            {
                PreserveNutChest();
            }

            if (!_settings.CrazyStartingItems)
            {
                PreserveStartingItems();
            }

            if (!_settings.AddCowMilk)
            {
                PreserveCowMilk();
            }

            if (!_settings.AddSkulltulaTokens)
            {
                PreserveSkulltulaTokens();
            }

            if (!_settings.AddStrayFairies)
            {
                PreserveStrayFairies();
            }

            if (!_settings.AddMundaneRewards)
            {
                PreserveMundaneRewards();
            }

            if (_settings.LogicMode == LogicMode.Casual && ItemList[Item.ItemRanchBarnOtherCowMilk2].Conditionals.Count == 1)
            {
                PreserveGlitchedCowMilk();
            }

            if (!_settings.AddOcarinaAndSongOfTime)
            {
                PreserveOcarinaAndSongOfTime();
            }
        }

        /// <summary>
        /// Keeps bottle catch contents vanilla
        /// </summary>
        private void PreserveBottleCatchContents()
        {
            for (var i = Item.BottleCatchFairy; i <= Item.BottleCatchMushroom; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Randomizes bottle catch contents
        /// </summary>
        private void AddBottleCatchContents()
        {
            var itemPool = new List<Item>();
            for (var i = Item.BottleCatchFairy; i <= Item.BottleCatchMushroom; i++)
            {
                if (ItemList[i].NewLocation.HasValue)
                {
                    continue;
                }
                itemPool.Add(i);
            }

            for (var i = Item.BottleCatchFairy; i <= Item.BottleCatchMushroom; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Keeps other vanilla
        /// </summary>
        private void PreserveOther()
        {
            for (var i = Item.ChestLensCaveRedRupee; i <= Item.IkanaScrubGoldRupee; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Keeps shop items vanilla
        /// </summary>
        private void PreserveShopItems()
        {
            for (var i = Item.ShopItemTradingPostRedPotion; i <= Item.ShopItemZoraRedPotion; i++)
            {
                ItemList[i].NewLocation = i;
            }

            ItemList[Item.ItemBombBag].NewLocation = Item.ItemBombBag;
            ItemList[Item.UpgradeBigBombBag].NewLocation = Item.UpgradeBigBombBag;
            ItemList[Item.MaskAllNight].NewLocation = Item.MaskAllNight;

            ItemList[Item.ShopItemMilkBarChateau].NewLocation = Item.ShopItemMilkBarChateau;
            ItemList[Item.ShopItemMilkBarMilk].NewLocation = Item.ShopItemMilkBarMilk;
            ItemList[Item.ShopItemBusinessScrubMagicBean].NewLocation = Item.ShopItemBusinessScrubMagicBean;
            ItemList[Item.ShopItemBusinessScrubGreenPotion].NewLocation = Item.ShopItemBusinessScrubGreenPotion;
            ItemList[Item.ShopItemBusinessScrubBluePotion].NewLocation = Item.ShopItemBusinessScrubBluePotion;
            ItemList[Item.ShopItemGormanBrosMilk].NewLocation = Item.ShopItemGormanBrosMilk;
        }

        /// <summary>
        /// Keeps dungeon items vanilla
        /// </summary>
        private void PreserveDungeonItems()
        {
            for (var i = Item.ItemWoodfallMap; i <= Item.ItemStoneTowerKey4; i++)
            {
                ItemList[i].NewLocation = i;
            };
        }

        /// <summary>
        /// Keeps moon items vanilla
        /// </summary>
        private void PreserveMoonItems()
        {
            for (var i = Item.HeartPieceDekuTrial; i <= Item.ChestLinkTrialBombchu10; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Keeps great fairy rewards vanilla
        /// </summary>
        private void PreserveFairyRewards()
        {
            for (var i = Item.FairyMagic; i <= Item.ItemFairySword; i++)
            {
                ItemList[i].NewLocation = i;
            }
            ItemList[Item.MaskGreatFairy].NewLocation = Item.MaskGreatFairy;
        }

        /// <summary>
        /// Keeps nut chest vanilla
        /// </summary>
        private void PreserveNutChest()
        {
            ItemList[Item.ChestPreClocktownDekuNut].NewLocation = Item.ChestPreClocktownDekuNut;
        }

        /// <summary>
        /// Keeps regular starting items vanilla
        /// </summary>
        private void PreserveStartingItems()
        {
            for (var i = Item.StartingSword; i <= Item.StartingHeartContainer2; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Keeps cow milk vanilla
        /// </summary>
        private void PreserveCowMilk()
        {
            for (var i = Item.ItemRanchBarnMainCowMilk; i <= Item.ItemCoastGrottoCowMilk2; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Keeps skulltula tokens vanilla
        /// </summary>
        private void PreserveSkulltulaTokens()
        {
            for (var i = Item.CollectibleSwampSpiderToken1; i <= Item.CollectibleOceanSpiderToken30; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        /// <summary>
        /// Keeps stray fairies vanilla
        /// </summary>
        private void PreserveStrayFairies()
        {
            for (var i = Item.CollectibleStrayFairyClockTown; i <= Item.CollectibleStrayFairyStoneTower15; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        private void PreserveMundaneRewards()
        {
            for (var i = Item.MundaneItemLotteryPurpleRupee; i <= Item.MundaneItemSeahorse; i++)
            {
                if (!ItemUtils.IsShopItem(i))
                {
                    ItemList[i].NewLocation = i;
                }
            }
        }

        /// <summary>
        /// Keeps glitched cow milk vanilla
        /// </summary>
        private void PreserveGlitchedCowMilk()
        {
            ItemList[Item.ItemRanchBarnOtherCowMilk2].NewLocation = Item.ItemRanchBarnOtherCowMilk2;
        }

        private void PreserveOwlActivations()
        {
            for (var i = Item.OwlActivationGreatBayCoast; i <= Item.OwlActivationStoneTower; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        private void PreserveEntrances()
        {
            for (var i = Item.EntranceMayorsResidenceFromEastClockTown; i <= Item.EntranceStoneTowerInvertedFromStoneTowerTempleInverted; i++)
            {
                ItemList[i].NewLocation = i;
            }
        }

        private void PreserveOcarinaAndSongOfTime()
        {
            ItemList[Item.ItemOcarina].NewLocation = Item.ItemOcarina;
            ItemList[Item.SongTime].NewLocation = Item.SongTime;
        }

        /// <summary>
        /// Randomizes songs with other songs
        /// </summary>
        private void ShuffleSongs()
        {
            var itemPool = new List<Item>();
            for (var i = Item.SongTime; i <= Item.SongOath; i++)
            {
                if (ItemList[i].NewLocation.HasValue)
                {
                    continue;
                }
                itemPool.Add(i);
            }

            for (var i = Item.SongTime; i <= Item.SongOath; i++)
            {
                PlaceItem(i, itemPool);
            }
        }

        /// <summary>
        /// Adds custom item list to randomization. NOTE: keeps area and other vanilla, randomizes bottle catch contents
        /// </summary>
        private void SetupCustomItems()
        {
            // Make all items vanilla, and override using custom item list
            MakeAllItemsVanilla();

            // Should these be vanilla by default? Why not check settings.
            ApplyCustomItemList();

            // Should these be randomized by default? Why not check settings.
            AddBottleCatchContents();

            if (!_settings.AddSongs)
            {
                ShuffleSongs();
            }
        }

        /// <summary>
        /// Mark all items as replacing themselves (i.e. vanilla)
        /// </summary>
        private void MakeAllItemsVanilla()
        {
            foreach (var location in ItemUtils.AllLocations())
            {
                ItemList[location].NewLocation = location;
            }
            foreach (var location in ItemUtils.AllEntrances())
            {
                ItemList[location].NewLocation = location;
            }
        }

        /// <summary>
        /// Adds items specified from the Custom Item List to the randomizer pool, while keeping the rest vanilla
        /// </summary>
        private void ApplyCustomItemList()
        {
            if (_settings.CustomItemList.Contains(-1))
            {
                throw new Exception("Invalid custom item string.");
            }
            for (int i = 0; i < _settings.CustomItemList.Count; i++)
            {
                int selectedItem = _settings.CustomItemList[i];

                selectedItem = ItemUtils.AddItemOffset(selectedItem);

                int selectedItemIndex = ItemList.FindIndex(u => u.ID == selectedItem);

                if (selectedItemIndex != -1)
                {
                    ItemList[selectedItemIndex].NewLocation = null;
                }
            }
            foreach (var entrance in _settings.RandomizedEntrances)
            {
                ItemList[entrance].NewLocation = null;
            }
            if (!_settings.DecoupleEntrances)
            {
                foreach (var io in ItemList)
                {
                    if (io.NewLocation.HasValue)
                    {
                        var pair = io.Item.Pair();
                        if (pair.HasValue)
                        {
                            ItemList[pair.Value].NewLocation = pair;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Overwrite junk items with ice traps.
        /// </summary>
        /// <param name="iceTraps">Ice traps amount setting</param>
        /// <param name="appearance">Ice traps appearance setting</param>
        public void AddIceTraps(IceTraps iceTraps, IceTrapAppearance appearance)
        {
            var random = this.Random;

            // Select replaceable junk items of specified amount.
            var items = IceTrapUtils.SelectJunkItems(_randomized.ItemList, iceTraps, random);

            // Dynamically generate appearance set for ice traps.
            // Only mimic song items if they are included in the main randomization pool (not in their own pool).
            var mimics = IceTrapUtils.BuildIceTrapMimicSet(_randomized.ItemList, appearance, _randomized.Settings.AddSongs)
                .ToArray();

            //if (!items.Any(u => u.NewLocation == Item.HeartPieceNorthClockTown))
            //    first = items.items.ToList().Find(u => u.NewLocation == Item.HeartPieceNorthClockTown);
            var new_items = new ItemObject[3];
            new_items[0] = ItemList.Find(u => u.NewLocation == Item.HeartPieceNorthClockTown);
            new_items[1] = ItemList.Find(u => u.NewLocation == Item.HeartPieceToSwamp);
            new_items[2] = ItemList.Find(u => u.NewLocation == Item.ShopItemTradingPostNut10);
            int items_length = items.Length;
            items = items.ToList().Concat(new_items.ToList()).ToArray();

            var list = new List<ItemObject>();
            foreach (var item in items)
            {
                // If check is visible (can be seen via world model), add "graphic override" for imitating other item.
                var mimic = mimics[random.Next(mimics.Length)];
                item.ID = (int)Item.IceTrap;
                item.Mimic = mimic;

                var newLocation = item.NewLocation.Value;
                if (newLocation.IsVisible() || newLocation.IsShop() || newLocation.IsPurchaseable())
                {
                    // Store name override for logging in HTML tracker.
                    item.NameOverride = $"{Item.IceTrap.Name()} ({mimic.Item.Name()})";

                    // If ice trap quirks enabled and placed as a shop item, use a fake shop item name.
                    if (_settings.IceTrapQuirks && (newLocation.IsShop() || newLocation.IsPurchaseable()))
                    {
                        item.Mimic.FakeName = FakeNameUtils.CreateFakeName(item.Mimic.Item.Name(), random);
                    }
                }

                if (_randomized.Settings.UpdateChests)
                {
                    // Choose chest type for ice trap appearance.
                    item.Mimic.ChestType = IceTrapUtils.GetIceTrapChestTypeOverride(appearance, random);
                }

                list.Add(item);
            }
            items[items_length   ].Mimic    = mimics[22]; // tree is sword
            items[items_length +1].Mimic    = mimics[81]; // swamp is FD
            items[items_length +2].Mimic    = mimics[42]; // shop is blast mask
            items[items_length +2].Mimic.FakeName    = "Best Mask";

            _randomized.IceTraps = list.AsReadOnly();
        }

        /// <summary>
        /// Randomizes the ROM with respect to the configured ruleset.
        /// </summary>
        public RandomizedResult Randomize(IProgressReporter progressReporter)
        {
            SeedRNG();

            _randomized = new RandomizedResult(_settings, _seed);

            if (_settings.LogicMode != LogicMode.Vanilla)
            {
                progressReporter.ReportProgress(5, "Preparing ruleset...");
                PrepareRulesetItemData();

                if (_settings.RandomizeDungeonEntrances)
                {
                    progressReporter.ReportProgress(10, "Shuffling dungeons...");
                    DungeonShuffle();
                }

                _randomized.Logic = ItemList.Select(io => new ItemLogic(io)).ToList();

                progressReporter.ReportProgress(30, "Shuffling items...");
                RandomizeItems();

                // Replace junk items with ice traps according to settings.
                AddIceTraps(_randomized.Settings.IceTraps, _randomized.Settings.IceTrapAppearance);
                
                var freeItemIds = _settings.CustomStartingItemList
                    .Cast<int>()
                    .Union(ItemList.Where(io => io.NewLocation.HasValue && ItemUtils.IsStartingLocation(io.NewLocation.Value)).Select(io => io.ID))
                    .ToList();

                bool updated;
                do
                {
                    updated = false;
                    foreach (var itemLogic in _randomized.Logic.Where(il => ((Item)il.ItemId).IsFake() && !freeItemIds.Contains(il.ItemId)))
                    {
                        if ((itemLogic.RequiredItemIds?.All(id => freeItemIds.Contains(id)) != false)
                            && (itemLogic.ConditionalItemIds?.Any(c => c.All(id => freeItemIds.Contains(id))) != false)
                            && (itemLogic.RequiredItemIds != null || itemLogic.ConditionalItemIds != null))
                        {
                            freeItemIds.Add(itemLogic.ItemId);
                            updated = true;
                        }
                    }
                } while (updated);

                foreach (var itemLogic in _randomized.Logic)
                {
                    if (_settings.CustomStartingItemList.Contains((Item)itemLogic.ItemId) && !ItemList[itemLogic.ItemId].IsRandomized)
                    {
                        itemLogic.Acquired = true;
                    }

                    var keep = new List<int>();
                    for (var i = 0; itemLogic.ConditionalItemIds != null && i < itemLogic.ConditionalItemIds.Count; i++)
                    {
                        if (itemLogic.ConditionalItemIds[i].All(freeItemIds.Contains))
                        {
                            keep.Add(i);
                        }
                    }
                    if (keep.Count > 0)
                    {
                        for (var i = itemLogic.ConditionalItemIds.Count - 1; i >= 0; i--)
                        {
                            if (!keep.Contains(i))
                            {
                                itemLogic.ConditionalItemIds.RemoveAt(i);
                            }
                        }
                    }
                }

                var logicForRequiredItems = _settings.LogicMode == LogicMode.Casual && _settings.GossipHintStyle == GossipHintStyle.Competitive
                    ? _randomized.Logic.Select(il =>
                    {
                        var itemLogic = new ItemLogic(il);

                        // prevent Giant's Mask from being Way of the Hero.
                        itemLogic.RequiredItemIds.Remove((int)Item.MaskGiant);

                        return itemLogic;
                    }).ToList()
                    : _randomized.Logic;

                //_randomized.ItemsRequiredForMoonAccess = new List<Item>();
                //_randomized.ImportantItems = LogicUtils.GetImportantItems(Item.EntranceMajorasLairFromTheMoon, _randomized.Logic)?.Important.Where(item => !item.IsFake() && !item.IsEntrance()).ToList().AsReadOnly();
                /*_randomized.ImportantItems = LogicUtils.GetImportantItems(ItemList, _settings, Item.AreaMoonAccess, _randomized.Logic)?.Important.Where(item => !item.IsFake()).ToList().AsReadOnly();
                if (_randomized.ImportantItems == null)
                {
                    throw new RandomizationException("Moon Access is unobtainable.");
                }
                var itemsRequiredForMoonAccess = new List<Item>();
                foreach (var item in _randomized.ImportantItems)
                {
                    //var checkPaths = GetImportantItems(Item.AreaMoonAccess, logicForRequiredItems, exclude: item);
                    var checkPaths = LogicUtils.GetImportantItems(ItemList, _settings, Item.AreaMoonAccess, logicForRequiredItems, exclude: item);
                    if (checkPaths == null)
                    {
                        itemsRequiredForMoonAccess.Add(item);
                    }
                }*/
                //_randomized.ItemsRequiredForMoonAccess = itemsRequiredForMoonAccess.AsReadOnly();

                if (_settings.GossipHintStyle != GossipHintStyle.Default)
                {
                    progressReporter.ReportProgress(35, "Making gossip quotes...");

                    //gossip
                    SeedRNG();
                    MakeGossipQuotes();
                }
                else
                {
                    SeedRNG();
                }

                _randomized.FileSelectSkybox = Random.Next(360);
                _randomized.FileSelectColor = Random.Next(360);
                _randomized.TitleLogoColor = Random.Next(360);
            }

            return _randomized;
        }
    }

}