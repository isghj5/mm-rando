using System;
using System.Collections.Generic;
using System.Linq;

using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.Enemizer
{
    /// <summary>
    ///  Actorizer needs to know if an actor can lead to a check, and should be locked for logic, this is more complicated than IsJunk() can handle
    /// </summary>

    class JunkDetection
    {
        private static Models.RandomizedResult _randomizedResult;
        private static List<GameObjects.ItemCategory> ActorizerKnownJunkCategories { get; set; }
        // outer list is item.category indexed, inner list is items
        private static List<List<GameObjects.Item>> ActorizerKnownJunkItems { get; set; }

        private static void AddAllCategory(List<GameObjects.Item> ItemList, GameObjects.ItemCategory ItemCategory, string SearchToken)
        {
            var searchedItems = ItemList.FindAll(token => token.Name().Contains(SearchToken)).ToList();
            ActorizerKnownJunkItems[ (int) ItemCategory].AddRange(searchedItems);
        }

        private static void PrepareJunkSpiderTokens(List<ItemLocationPair> allSphereItems) // tag: spiderhouse
        {
            /// TODO this can be simplified, it was more complex before I realized spheres are kinda useless

            if ((_randomizedResult.Settings.VictoryMode & VictoryMode.SkullTokens) > 0)
                return; // victory mode for fairies is enabled, none are junk: leave early

            List<GameObjects.Item> allSpiderTokens = _randomizedResult.ItemList.FindAll(item => item.Item.ItemCategory() == GameObjects.ItemCategory.SkulltulaTokens).Select(u => u.Item).ToList();

            // some items we didnt consider junk for short depth checks, are junk here just because these are really late game
            var extendedJunkCategories = ActorizerKnownJunkCategories.ToList();
            extendedJunkCategories.Add(GameObjects.ItemCategory.Milk);
            extendedJunkCategories.Add(GameObjects.ItemCategory.SilverRupees);
            var swampSkullJunk = false;

            if (_randomizedResult.Settings.LogicMode != LogicMode.Casual)
            {
                var swampSkullReward = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MaskTruth).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(swampSkullReward.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    AddAllCategory( allSpiderTokens, GameObjects.ItemCategory.SkulltulaTokens, "Swamp");
                    swampSkullJunk = true;
                }

                var oceanSkullReward1 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.UpgradeGiantWallet).Item;
                var oceanSkullReward2 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay2PurpleRupee).Item;
                var oceanSkullReward3 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay3RedRupee).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(oceanSkullReward1.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward2.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward3.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    AddAllCategory(allSpiderTokens, GameObjects.ItemCategory.SkulltulaTokens, "Ocean");

                    if (swampSkullJunk) // both were junk, we can consider all of the tokens to be junk
                    {
                        ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.SkulltulaTokens);
                    }
                }

            }
            else // casual
            {
                // we have logic, just use the logic spheres

                var swampSkullReward = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MaskTruth).Item;
                if (extendedJunkCategories.Contains(swampSkullReward.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    var swampTokenImportantSearch = allSphereItems.Any(u => u.Item == "Swamp Skulltula Spirit");
                    if (!swampTokenImportantSearch)
                    {
                        AddAllCategory(allSpiderTokens, GameObjects.ItemCategory.SkulltulaTokens, "Swamp");
                        swampSkullJunk = true;
                    }
                }

                var oceanSkullReward1 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.UpgradeGiantWallet).Item;
                var oceanSkullReward2 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay2PurpleRupee).Item;
                var oceanSkullReward3 = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay3RedRupee).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(oceanSkullReward1.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward2.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward3.ItemCategory() ?? GameObjects.ItemCategory.None))
                {

                    var oceanTokenImportantSearch = allSphereItems.Any(u => u.Item == "Ocean Skulltula Spirit");
                    if (!oceanTokenImportantSearch)
                    {
                        AddAllCategory(allSpiderTokens, GameObjects.ItemCategory.SkulltulaTokens, "Ocean");
                        if (swampSkullJunk) // both were junk, we can consider all of the tokens to be junk
                        {
                            ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.SkulltulaTokens);
                        }
                    }
                }
            }
        }

        private static void PrepareJunkStrayFairies(List<ItemLocationPair> allSphereItems) // tag: strayfairy
        {
            if ((_randomizedResult.Settings.VictoryMode & VictoryMode.Fairies) > 0)
                return; // victory mode for fairies is enabled, none are junk: leave early

            var allFaires = _randomizedResult.ItemList.FindAll(item => item.Item.ClassicCategory() == GameObjects.ClassicCategory.StrayFairies).Select(u => u.Item).ToList();

            /* void AddFairies(string tokenSearch)
            {
                var fairySearched = allFaires.FindAll(token => token.Name().Contains(tokenSearch)).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.StrayFairies].AddRange(fairySearched);
            } */

            if (_randomizedResult.Settings.LogicMode != LogicMode.Casual)
            {
                var extendedJunkCategories = ActorizerKnownJunkCategories.ToList();
                extendedJunkCategories.Add(GameObjects.ItemCategory.Milk);
                extendedJunkCategories.Add(GameObjects.ItemCategory.SilverRupees);

                void AddBasedOnResult(GameObjects.Item item, string str)
                {
                    var reward = _randomizedResult.ItemList.Find(i => i.NewLocation == item).Item;
                    // check if reward is junk, if so add all fairies 
                    if (extendedJunkCategories.Contains(reward.ItemCategory() ?? GameObjects.ItemCategory.None))
                    {
                        AddAllCategory(allFaires, GameObjects.ItemCategory.StrayFairies, str);

                    }
                }

                AddBasedOnResult(GameObjects.Item.FairySpinAttack, "Woodfall");
                AddBasedOnResult(GameObjects.Item.FairyDoubleMagic, "Snowhead");
                AddBasedOnResult(GameObjects.Item.FairyDoubleDefense, "Great Bay");
                AddBasedOnResult(GameObjects.Item.ItemFairySword, "Stone Tower");
            }
            else // casual logic
            {
                // I used to do this but now that we have sphere its faster because the sphere list is smaller datasize
                //var woodfallFairyReward = _randomizedResult.ItemList.Find(item => item.NewLocation == GameObjects.Item.FairySpinAttack).Item;
                //if (ItemUtils.IsJunk(woodfallFairyReward))

                void AddBasedOnSphere(string testToken, string searchToken)
                {
                    var search = allSphereItems.Any(u => u.Item == testToken);
                    // check if any of the fairies are considered important, if they aren't then they are junk 
                    if (!search)
                    {
                        AddAllCategory(allFaires, GameObjects.ItemCategory.StrayFairies, searchToken);
                    }
                }

                AddBasedOnSphere("Woodfall Stray Fairy", "Woodfall");
                AddBasedOnSphere("Snowhead Stray Fairy", "Snowhead");
                AddBasedOnSphere("Great Bay Stray Fairy", "Great Bay");
                AddBasedOnSphere("Stone Tower Stray Fairy", "Stone Tower");
            }

            // test if all fairies are junk if so add to categories
            // TODO remove this and just use the item list, we have the item list after all
            var junkFairies = ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.StrayFairies];
            bool allFairiesJunk = junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyWoodfall1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairySnowhead1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyGreatBay1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyStoneTower1);
            if (allFairiesJunk)
            {
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.StrayFairies);
            }
        }

        private static void PrepareJunkNotebookEntries(List<ItemLocationPair> allSphereItems)
        {
            /// Notebook entries are junk IF the settings do not specify getting all notebook is required to beat the seed

            if ((_randomizedResult.Settings.VictoryMode & VictoryMode.Notebook) > 0)
                return; // victory mode for notebook entries is enabled, none are junk: leave early

            // if not required for victory, the entries themselves are always junk
            ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.NotebookEntries);

            var entryChecks = _randomizedResult.ItemList.FindAll(i => i.NewLocation.ToString().Contains("Notebook"));
            List<ItemObject> junkEntries = new List<ItemObject>();
            var nonJunkCount = 0;
            for (int i = 0; i < entryChecks.Count(); i++)
            {
                var item = entryChecks[i].Item; // the check being filled
                // where, if the item NewLocation is null, it was removed for traps I think, consider it any other junk item
                //GameObjects.Item check = entryChecks[i].NewLocation ?? GameObjects.Item.RecoveryHeart; // the item being placed
                //var locationCategory = check.ItemCategory() ?? GameObjects.ItemCategory.None;
                //var itemCategory = item.ItemCategory() ?? GameObjects.ItemCategory.None;
                if (!IsActorizerJunk(item)) // not yet considered junk
                {
                    // we dont need to add the entries themselves they are already added to the junk list per-category
                    //   this is just for notebook itself
                    nonJunkCount++;
                    break; // we don't need to count, this is only to check if the notebook leads to at least one non-junk, we don't add entries per-each anymore
                }
            }

            if (nonJunkCount == 0) // no non-junk entries means the notebook only leads to junk: we can junk it
            {
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.MainInventory].Add(GameObjects.Item.ItemNotebook);
            }
        }

        private static void PrepareKegEntry(List<ItemLocationPair> allSphereItems)
        {
            // dnf, could not think of a good way to write with existing logic
        }

        private static void PrepareJunkScoopList(List<ItemLocationPair> allSphereItems)
        {
            // if the scoops are vanilla they can never be considered junk
            if (_randomizedResult.Settings.LogicMode == LogicMode.Vanilla) return;
            // currently, we cannot discern if scoops are important or not in no logic
            if (_randomizedResult.Settings.LogicMode == LogicMode.NoLogic) return;

            //Debug.Assert(allSpehere )

            var importantBottleItems = allSphereItems.FindAll(item => item.Item.Contains("Bottle:"));

            // get all bottles as items that are not randomized for now we have to assume they are important
            var bottleCatches = _randomizedResult.ItemList
                    .Where(item => item.DisplayName() != null && item.DisplayName().Contains("Bottle:"))
                    .ToList();
            var unrandomizedBottles = bottleCatches.Where(item => !item.IsRandomized).ToList();
            // add that list to importantBottleItems
            foreach (var itemstring in unrandomizedBottles)
            {
                importantBottleItems.Add(new ItemLocationPair
                {
                    Item = "",
                    Location = itemstring.DisplayName()
                });
            }

            // scoops are a special case, they dont count as junk items above since they are all in one category handle separatly
            // for all items in list of items that are scoop types
            //   check if each and every one is an important item
            var scoopItems = _randomizedResult.ItemList.FindAll(item => item.Item.ItemCategory() == GameObjects.ItemCategory.ScoopedItems);
            List<ItemObject> unImportantScoopIOs = scoopItems.FindAll(scoop => importantBottleItems.Count(important => important.Item == scoop.Item.Name()) == 0);
            List<GameObjects.Item> unimportantScoops = unImportantScoopIOs.Select(itemObj => itemObj.Item).ToList();

            ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.ScoopedItems].AddRange(unimportantScoops);

            if (unimportantScoops.Count() == bottleCatches.Count()) // if ALL scoops are unimportant
            {
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.ScoopedItems);
            }
        }

        private static void PrepareJunkHeartPieces()
        {
            // if not casual logic, we want to add these since those crazy people think hearts are junk
            if (((_randomizedResult.Settings.VictoryMode & VictoryMode.Hearts) == 0) // hearts are NOT required win condition
               && (_randomizedResult.Settings.LogicMode == LogicMode.NoLogic || _randomizedResult.Settings.LogicMode == LogicMode.Glitched))
            {
                var heartPieces = _randomizedResult.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.PiecesOfHeart).Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.PiecesOfHeart].AddRange(heartPieces);

                var recoveryHearts = _randomizedResult.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.RecoveryHearts).Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.RecoveryHearts].AddRange(recoveryHearts);
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.PiecesOfHeart);
            }
        }

        private static void PrepareJunkRedRupee()
        {
            var redRupees = _randomizedResult.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.RedRupees).Select(itemObj => itemObj.Item).ToList();
            redRupees.Remove(GameObjects.Item.CollectableIkanaGraveyardDay2Bats1);
            ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.RedRupees].AddRange(redRupees);
            ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.RedRupees);
        }

        private static void PrepareJunkMapAndCompass()
        {
            // this does not work, without me knowing when they are junk or not TODO
            /// if the player does not get hints from these, they should count as junk, but dont know if thats a setting I can look up

            if (_randomizedResult.Settings.LogicMode == LogicMode.Vanilla
                || _randomizedResult.Settings.LogicMode == LogicMode.Casual)
            {
                return;
            }

            // with 2.0, entrando, these settings values no longer exist, don't know how to change them, for now just disable this and assume all compass/map are not-junk
            /* 
            if (_randomizedResult.Settings.RandomizeBossRooms == false)
            {
                var compass = _randomizedResult.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.Navigation
                                                                    && itemObj.Item.ToString().Contains("Compass"))
                                                  .Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.Navigation].AddRange(compass);
            }

            if (_randomizedResult.Settings.RandomizeDungeonEntrances == false)
            {
                var maps = _randomizedResult.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.Navigation
                                                                    && itemObj.Item.ToString().Contains("Map"))
                                                  .Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.Navigation].AddRange(maps);
            }
            // */
        }

        public static void PrepareJunkItems(Models.RandomizedResult settings)
        {
            /// Prepare all junk item lists and populate junk categories for actorizer junk consideration

            _randomizedResult = settings;

            var addedJunkItems = new List<GameObjects.Item>();

            // probably a better way to init a list of list to size, but not known
            ActorizerKnownJunkCategories = _actorizerDefaultJunkCategories.ToList(); // copy
            ActorizerKnownJunkItems = new List<List<GameObjects.Item>>(); // init
            foreach (var category in Enum.GetValues(typeof(GameObjects.ItemCategory)))
            {
                ActorizerKnownJunkItems.Add(new List<GameObjects.Item>());
            }

            var allSphereItems = new List<ItemLocationPair>();
            if (_randomizedResult.Settings.LogicMode == LogicMode.Casual || _randomizedResult.Settings.LogicMode == LogicMode.Glitched)
            {
                allSphereItems = _randomizedResult.Spheres.SelectMany(u => u).ToList();
            }

            PrepareJunkHeartPieces(); // no-logic only
            PrepareJunkRedRupee(); // crimson rupee counts as junk in IsJunk(), and thats stupid and not fair, dedicated function to work around
            PrepareJunkScoopList(allSphereItems);
            PrepareJunkNotebookEntries(allSphereItems);
            // bug: because these lists are generated in linear, fairies dont know if spiders are junk
            //   currently spiders are put later just because junked fairy leads only to great fairies being randomized,
            //   which is often ignored, spiders are not
            PrepareJunkStrayFairies(allSphereItems);
            PrepareJunkSpiderTokens(allSphereItems);
            // all transformation and non-transofrmation mask <- already not considered junk
            // all boss remains <- already not considered junk

            // koume? painful because she can lead to the boat ride to deku palace, that's a lot of logic

        }

        // if one of these already exists somewhere in the logic I did not find it
        public static readonly List<GameObjects.ItemCategory> _actorizerDefaultJunkCategories = new List<GameObjects.ItemCategory>{
            GameObjects.ItemCategory.GreenRupees,
            GameObjects.ItemCategory.BlueRupees,
            //GameObjects.ItemCategory.RedRupees, // crimson rup in this list, removed by building into our own list
            GameObjects.ItemCategory.PurpleRupees,
            GameObjects.ItemCategory.Arrows,
            GameObjects.ItemCategory.Bombs,
            GameObjects.ItemCategory.DekuSticks,
            GameObjects.ItemCategory.DekuNuts,
            GameObjects.ItemCategory.Fairy,
            GameObjects.ItemCategory.GreenPotions,
            GameObjects.ItemCategory.None, // think this was mostly used for traps
            GameObjects.ItemCategory.MagicJars
        };

        public static bool IsActorizerCheckJunk(GameObjects.Item check)
        {
            /// tests if the item in a check is junk
            // I kept calling this instead of IsActorizerJunk by accident, might as well make it rather than do this every time

            var item = _randomizedResult.ItemList.Single(item => item.NewLocation != null && item.NewLocation == check).Item;
            return IsActorizerJunk(item);
        }


        public static bool IsActorizerJunk(GameObjects.Item itemInCheck)
        {
            /// checks if an item is junk for actorizer
            /// ergo: is a spider token considered junk in this seed? no the spider token reward is oath

            // first check categories, category lists are tiny compared to each item
            var category = itemInCheck.ItemCategory() ?? GameObjects.ItemCategory.None;
            var intCategory = (int)category;
            // zero is None, recovery heart is below zero
            if (intCategory <= 0 || ActorizerKnownJunkCategories.Contains(category))
                return true;

            var specificCategoryList = ActorizerKnownJunkItems[intCategory];
            if (specificCategoryList.Contains(itemInCheck))
                return true;

            return false;
        }

    }
}
