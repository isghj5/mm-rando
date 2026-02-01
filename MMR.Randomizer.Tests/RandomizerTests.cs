using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using NUnit.Framework;

namespace MMR.Randomizer.Tests
{
    public class RandomizerTests
    {
        string tempLogicFile;
        private GameplaySettings _settings;
        private const int seed = 0;

        private void CreateLogic(Action<ItemList> updateItemList)
        {
            tempLogicFile = Path.GetTempFileName();
            var itemList = LogicUtils.PopulateItemListWithoutLogic();
            updateItemList(itemList);
            File.WriteAllText(tempLogicFile, new LogicFile
            {
                Version = LogicMigrator.Migrator.CurrentVersion,
                Logic = itemList.Select(itemObject => new JsonFormatLogicItem
                {
                    Id = itemObject.Name,
                    TimeNeeded = (TimeOfDay)itemObject.TimeNeeded,
                    TimeAvailable = (TimeOfDay)itemObject.TimeAvailable,
                    TimeSetup = (TimeOfDay)itemObject.TimeSetup,
                    IsTrick = itemObject.IsTrick,
                    TrickTooltip = itemObject.TrickTooltip,
                    RequiredItems = itemObject.DependsOnItems.Select(item => itemList[item].Name).ToList(),
                    ConditionalItems = itemObject.Conditionals.Select(c => c.Select(item => itemList[item].Name).ToList()).ToList(),
                    TrickCategory = itemObject.TrickCategory,
                    TrickUrl = itemObject.TrickUrl,
                    SettingExpression = itemObject.SettingExpression,
                }).ToList()
            }.ToString());
            _settings.LogicMode = LogicMode.UserLogic;
            _settings.UserLogicFileName = tempLogicFile;
        }

        [SetUp]
        public void SetupRandomizerTests()
        {
            _settings = new GameplaySettings
            {
                LogicMode = LogicMode.NoLogic,
            };
        }

        [Test]
        public void ShouldResultInAsManyMessageCostsAsThereAreMessageCosts()
        {
            _settings.PriceMode = PriceMode.None;
            var randomizer = new Randomizer(_settings, seed);
            var result = randomizer.Randomize(new NoProgressReporter());

            Assert.AreEqual(MessageCost.MessageCosts.Length, result.MessageCosts.Count);
        }

        [Test]
        [TestCase(PriceMode.Purchases)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames)]
        [TestCase(PriceMode.Purchases | PriceMode.Misc)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Minigames)]
        [TestCase(PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Misc)]
        [TestCase(PriceMode.None)]
        public void ShouldHaveCorrespondingNonNullMessageCostsForEachMessageCostPurchasesDependingOnPriceMode(PriceMode priceMode)
        {
            _settings.PriceMode = priceMode;
            var randomizer = new Randomizer(_settings, seed);
            var result = randomizer.Randomize(new NoProgressReporter());

            var purchaseCount = MessageCost.MessageCosts.Count(mc => priceMode.HasFlag(mc.Category));

            for (var i = 0; i < MessageCost.MessageCosts.Length; i++)
            {
                Assert.AreEqual(priceMode.HasFlag(MessageCost.MessageCosts[i].Category), result.MessageCosts[i] != null);
            }
        }

        [Test]
        [TestCase(PriceMode.Purchases)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames)]
        [TestCase(PriceMode.Purchases | PriceMode.Misc)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Minigames)]
        [TestCase(PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Misc)]
        [TestCase(PriceMode.None)]
        public void ShouldResultInMessageCostsBeingSequentiallyEquivalentToVanillaCostsIfPriceModeHasShuffleOnly(PriceMode priceMode)
        {
            _settings.PriceMode = priceMode | PriceMode.ShuffleOnly;
            var randomizer = new Randomizer(_settings, seed);
            var result = randomizer.Randomize(new NoProgressReporter());

            var vanillaCosts = MessageCost.MessageCosts.Where(mc => priceMode.HasFlag(mc.Category)).Select(mc => mc.Cost).ToList();

            CollectionAssert.AreEquivalent(vanillaCosts, result.MessageCosts.Where(c => c != null));
        }

        [Test]
        public void ShouldResultInMessageCostsBeingTwiceAsLargeAsVanillaCostsIfPriceModeIsAccountForRoyalWallet()
        {
            _settings.PriceMode = PriceMode.AccountForRoyalWallet;
            _settings.CustomItemList.Add(Item.UpgradeRoyalWallet);
            _settings.CustomItemList.Add(Item.ChestTerminaGrassRedRupee);
            _settings.CustomItemList.Add(Item.ChestTerminaGrottoRedRupee);

            var randomizer = new Randomizer(_settings, seed);
            var result = randomizer.Randomize(new NoProgressReporter());

            for (var i = 0; i < MessageCost.MessageCosts.Length; i++)
            {
                var expectedCost = MessageCost.MessageCosts[i].Cost << 1;
                if (expectedCost > 999)
                {
                    expectedCost = 999;
                }
                Assert.AreEqual(expectedCost, result.MessageCosts[i]);
            }
        }

        [Test]
        [TestCase(PriceMode.Purchases)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames)]
        [TestCase(PriceMode.Purchases | PriceMode.Misc)]
        [TestCase(PriceMode.Purchases | PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Minigames)]
        [TestCase(PriceMode.Minigames | PriceMode.Misc)]
        [TestCase(PriceMode.Misc)]
        [TestCase(PriceMode.None)]
        public void ShouldResultInMessageCostsBeingTwiceAsLargeAsVanillaCostsIfPriceModeHasAccountForRoyalWalletAndShuffleOnly(PriceMode priceMode)
        {
            _settings.PriceMode = priceMode | PriceMode.AccountForRoyalWallet | PriceMode.ShuffleOnly;
            _settings.CustomItemList.Add(Item.UpgradeRoyalWallet);
            _settings.CustomItemList.Add(Item.ChestTerminaGrassRedRupee);
            _settings.CustomItemList.Add(Item.ChestTerminaGrottoRedRupee);

            var randomizer = new Randomizer(_settings, seed);
            var result = randomizer.Randomize(new NoProgressReporter());

            var shuffledCosts = MessageCost.MessageCosts
                .Where(mc => priceMode.HasFlag(mc.Category))
                .Select(mc =>
                {
                    var cost = mc.Cost << 1;
                    if (cost > 999)
                    {
                        cost = 999;
                    }
                    return cost;
                })
                .ToList();

            var shuffledResults = result.MessageCosts.Where((c, i) => priceMode.HasFlag(MessageCost.MessageCosts[i].Category));

            CollectionAssert.AreEquivalent(shuffledCosts, shuffledResults);

            for (var i = 0; i < MessageCost.MessageCosts.Length; i++)
            {
                var messageCost = MessageCost.MessageCosts[i];
                if (priceMode.HasFlag(messageCost.Category))
                {
                    continue;
                }
                var expectedCost = MessageCost.MessageCosts[i].Cost << 1;
                if (expectedCost > 999)
                {
                    expectedCost = 999;
                }
                Assert.AreEqual(expectedCost, result.MessageCosts[i], "Cost {0} was incorrect.", messageCost.Name);
            }
        }

        [Test]
        public void ShouldPreventTemporaryItemsBeingPlacedInLocationsOnlyReachableAtALaterTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.MaskAllNight].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.MaskAllNight);
            _settings.CustomStartingItemList.Add(Item.MaskAllNight);
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);

            var randomizer = new Randomizer(_settings, seed);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Letter to Kafei anywhere.", exception.Message);
        }

        [Test]
        public void ShouldNotPreventPermanentItemsBeingPlacedInLocationsOnlyReachableAtALaterTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.MaskKafei].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.MaskAllNight].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
            });
            _settings.CustomItemList.Add(Item.MaskKafei);
            _settings.CustomItemList.Add(Item.MaskAllNight);
            _settings.CustomStartingItemList.Add(Item.MaskAllNight);
            _settings.CustomJunkLocations.Add(Item.MaskKafei);

            var randomizer = new Randomizer(_settings, seed);

            Assert.DoesNotThrow(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
        }

        [Test]
        public void ShouldPreventEntrancesBeingPlacedInLocationsOnlyReachableAtALaterTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.GrottoDekuPlayground].TimeNeeded = (int)TimeOfDay.Day1;
                foreach (var grotto in Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Grotto))
                {
                    itemList[grotto].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
                }
            });
            _settings.EntranceMode = EntranceMode.Grottos;

            var randomizer = new Randomizer(_settings, seed);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Deku Playground anywhere.", exception.Message);
        }

        [Test]
        public void ShouldPreventTemporaryItemsBeingPlacedInLocationsWhoseEntranceIsOnlyReachableAtALaterTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.ChestWoodsGrotto].DependsOnItems.Add(Item.GrottoGenericWoodsOfMystery);
                itemList[Item.GrottoGenericWoodsOfMystery].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.ChestWoodsGrotto);
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);

            var randomizer = new Randomizer(_settings, seed);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Letter to Kafei anywhere.", exception.Message);
        }

        [Test]
        public void ShouldNotPreventTemporaryItemsBeingPlacedInLocationsWhoseEntranceIsReachableAtAnyTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.ChestWoodsGrotto].DependsOnItems.Add(Item.GrottoGenericWoodsOfMystery);
                itemList[Item.GrottoGenericWoodsOfMystery].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.ChestWoodsGrotto);
            _settings.CustomItemList.Add(Item.ChestLensCavePurpleRupee); // this ensures "purple rupee" isn't uniquely randomized
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);
            _settings.CustomJunkLocations.Add(Item.ChestLensCavePurpleRupee);
            _settings.EntranceMode = EntranceMode.Grottos;

            // TODO flaky - relies on seed not randomizing woods of mystery grotto to vanilla
            var randomizer = new Randomizer(_settings, seed);
            RandomizedResult randomizedResult = null;

            Assert.DoesNotThrow(() =>
            {
                randomizedResult = randomizer.Randomize(new NoProgressReporter());
            });

            Assert.AreNotEqual(Item.GrottoGenericWoodsOfMystery, randomizedResult.ItemList[Item.GrottoGenericWoodsOfMystery].NewLocation);
        }

        [Test]
        public void ShouldPreventTemporaryItemsBeingPlacedInLocationsWhoseEntranceChainIsOnlyReachableAtALaterTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.ChestWoodsGrotto].DependsOnItems.Add(Item.GrottoGenericWoodsOfMystery);
                itemList[Item.GrottoGenericWoodsOfMystery].DependsOnItems.Add(Item.InteriorWoodsOfMystery);
                foreach (var grotto in Enum.GetValues<Item>().Where(item => item.EntranceType() == EntranceType.Interior))
                {
                    itemList[grotto].TimeAvailable = (int)(TimeOfDay.Night1 | TimeOfDay.Day2 | TimeOfDay.Night2 | TimeOfDay.Day3 | TimeOfDay.Night3);
                }
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.ChestWoodsGrotto);
            _settings.CustomItemList.Add(Item.ChestLensCavePurpleRupee); // this ensures "purple rupee" isn't uniquely randomized
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);
            _settings.CustomJunkLocations.Add(Item.ChestLensCavePurpleRupee);
            _settings.EntranceMode = EntranceMode.SimpleInteriors;

            var randomizer = new Randomizer(_settings, seed);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Letter to Kafei anywhere.", exception.Message);
        }

        [Test]
        public void ShouldPreventTemporaryItemsBeingPlacedInLocationsThatTakeOnAConditionalTimeOfDayRequirement()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.CollectableRanchHouseBarnBarnItem1].DependsOnItems.Add(Item.InteriorRanchBarn);
                itemList[Item.InteriorRanchBarn].DependsOnItems.Add(Item.OtherEpona);
                itemList[Item.ItemPowderKeg].DependsOnItems.Add(Item.TradeItemKafeiLetter);
                var dayThreeItemObject = new ItemObject
                {
                    ID = itemList.Count,
                    Name = "Day Three",
                    TimeAvailable = (int)(TimeOfDay.Day3 | TimeOfDay.Night3)
                };
                itemList.Add(dayThreeItemObject);
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { Item.ItemPowderKeg });
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { dayThreeItemObject.Item });
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.CollectableRanchHouseBarnBarnItem1);
            _settings.CustomItemList.Add(Item.CollectableRanchHouseBarnBarnItem2); // this ensures "blue rupee" isn't uniquely randomized
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);
            _settings.CustomJunkLocations.Add(Item.CollectableRanchHouseBarnBarnItem2);

            var randomizer = new Randomizer(_settings, seed);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Letter to Kafei anywhere.", exception.Message);
        }

        [Test]
        public void ShouldNotPreventTemporaryItemsBeingPlacedInLocationsThatCanBeAcquiredByAnItemInsteadOfTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.CollectableRanchHouseBarnBarnItem1].DependsOnItems.Add(Item.InteriorRanchBarn);
                itemList[Item.CollectableRanchHouseBarnBarnItem1].TimeAvailable = (int)TimeOfDay.Day1;
                itemList[Item.InteriorRanchBarn].DependsOnItems.Add(Item.OtherEpona);
                var dayThreeItemObject = new ItemObject
                {
                    ID = itemList.Count,
                    Name = "Day Three",
                    TimeAvailable = (int)(TimeOfDay.Day3 | TimeOfDay.Night3)
                };
                itemList.Add(dayThreeItemObject);
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { Item.ItemPowderKeg });
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { dayThreeItemObject.Item });
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.ItemPowderKeg);
            _settings.CustomItemList.Add(Item.CollectableRanchHouseBarnBarnItem1);
            _settings.CustomItemList.Add(Item.CollectableTerminaFieldInvisibleItem7); // this ensures "blue rupee" isn't uniquely randomized
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);
            _settings.CustomJunkLocations.Add(Item.ItemPowderKeg);

            // TODO flaky - relies on seed placing letter to kafei on barn item 1
            var randomizer = new Randomizer(_settings, 1);
            RandomizedResult randomizedResult = null;

            Assert.DoesNotThrow(() =>
            {
                randomizedResult = randomizer.Randomize(new NoProgressReporter());
            });

            Assert.AreEqual(Item.CollectableRanchHouseBarnBarnItem1, randomizedResult.ItemList[Item.TradeItemKafeiLetter].NewLocation);
            Assert.AreEqual(Item.CollectableTerminaFieldInvisibleItem7, randomizedResult.ItemList[Item.ItemPowderKeg].NewLocation);
        }

        [Test]
        public void ShouldAllowTemporaryItemsToBePlacedViaRequiringAnItemAndThatItemToRequireTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.TradeItemKafeiLetter].TimeNeeded = (int)TimeOfDay.Day1;
                itemList[Item.CollectableRanchHouseBarnBarnItem1].DependsOnItems.Add(Item.InteriorRanchBarn);
                itemList[Item.CollectableRanchHouseBarnBarnItem2].DependsOnItems.Add(Item.InteriorRanchBarn);
                itemList[Item.InteriorRanchBarn].DependsOnItems.Add(Item.OtherEpona);
                var dayThreeItemObject = new ItemObject
                {
                    ID = itemList.Count,
                    Name = "Day Three",
                    TimeAvailable = (int)(TimeOfDay.Day3 | TimeOfDay.Night3)
                };
                itemList.Add(dayThreeItemObject);
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { Item.ItemPowderKeg });
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { dayThreeItemObject.Item });
            });
            _settings.CustomItemList.Add(Item.TradeItemKafeiLetter);
            _settings.CustomItemList.Add(Item.ItemPowderKeg);
            _settings.CustomItemList.Add(Item.CollectableRanchHouseBarnBarnItem1);
            _settings.CustomItemList.Add(Item.CollectableRanchHouseBarnBarnItem2); // this ensures "blue rupee" isn't uniquely randomized
            _settings.CustomJunkLocations.Add(Item.TradeItemKafeiLetter);
            _settings.CustomJunkLocations.Add(Item.ItemPowderKeg);

            // TODO flaky - relies on seed placing letter to kafei on barn item 1
            var randomizer = new Randomizer(_settings, 1);
            RandomizedResult randomizedResult = null;

            Assert.DoesNotThrow(() =>
            {
                randomizedResult = randomizer.Randomize(new NoProgressReporter());
            });

            Assert.AreEqual(Item.CollectableRanchHouseBarnBarnItem1, randomizedResult.ItemList[Item.TradeItemKafeiLetter].NewLocation);
            Assert.AreEqual(Item.CollectableRanchHouseBarnBarnItem2, randomizedResult.ItemList[Item.ItemPowderKeg].NewLocation);
        }

        [Test]
        public void ShouldNotPlaceItemsInLocationsThatRequireThemAsAnAlternativeToTimeOfDay()
        {
            CreateLogic(itemList =>
            {
                itemList[Item.CollectableTerminaFieldInvisibleItem2].DependsOnItems.Add(Item.SongHealing);
                itemList[Item.SongEpona].TimeAvailable = (int)TimeOfDay.Day1;
                itemList[Item.SongEpona].DependsOnItems.Add(Item.OtherEpona);
                var dayThreeItemObject = new ItemObject
                {
                    ID = itemList.Count,
                    Name = "Day Three",
                    TimeAvailable = (int)(TimeOfDay.Day3 | TimeOfDay.Night3)
                };
                itemList.Add(dayThreeItemObject);
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { Item.ItemPowderKeg });
                itemList[Item.OtherEpona].Conditionals.Add(new List<Item> { dayThreeItemObject.Item });
            });
            _settings.CustomStartingItemList.Add(Item.SongEpona);
            _settings.CustomItemList.Add(Item.SongEpona);
            _settings.CustomItemList.Add(Item.ItemPowderKeg);
            _settings.CustomItemList.Add(Item.SongHealing);
            _settings.CustomItemList.Add(Item.CollectableTerminaFieldInvisibleItem2);
            _settings.CustomItemList.Add(Item.CollectableTerminaFieldInvisibleItem3); // ensures green rupee is not uniquely randomized
            _settings.CustomJunkLocations.Add(Item.ItemPowderKeg);
            _settings.CustomJunkLocations.Add(Item.SongHealing);
            _settings.CustomJunkLocations.Add(Item.CollectableTerminaFieldInvisibleItem3);

            var randomizer = new Randomizer(_settings, 1);

            var exception = Assert.Throws<RandomizationException>(() =>
            {
                randomizer.Randomize(new NoProgressReporter());
            });
            Assert.AreEqual("Unable to place Powder Keg anywhere.", exception.Message);
        }

        [TearDown]
        public void Cleanup()
        {
            if (tempLogicFile != null && File.Exists(tempLogicFile))
            {
                File.Delete(tempLogicFile);
            }
        }
    }
}
