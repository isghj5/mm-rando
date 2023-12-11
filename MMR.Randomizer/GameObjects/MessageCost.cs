using MMR.Randomizer.Models;
using System.Collections.Generic;

namespace MMR.Randomizer.GameObjects
{
    public class MessageCost
    {
        public string Name { get; set; }
        public IList<(ushort messageId, byte costIndex)> MessageIds { get; set; } = new List<(ushort, byte)>();
        public IList<Item> LocationsAffected { get; set; } = new List<Item>();
        public IList<Item> ItemsAffected { get; set; } = new List<Item>();
        public IList<int> PriceAddresses { get; set; } = new List<int>();
        public IList<int> SubtractPriceAddresses { get; set; } = new List<int>();
        public PriceMode Category { get; set; } = PriceMode.Purchases;
        public ushort Cost { get; set; }

        public static MessageCost[] MessageCosts = new MessageCost[]
        {
            // TODO Zora Jar Game Cost, update ASM

            // Honey and Darling
            new MessageCost
            {
                Name = "Honey and Darling",
                MessageIds = { (10353, 0) },
                LocationsAffected = { Item.HeartPieceHoneyAndDarling, Item.MundaneItemHoneyAndDarlingPurpleRupee },
                SubtractPriceAddresses = { 0xDD53B2 },
                PriceAddresses = { 0xDD539A },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Town Archery
            new MessageCost
            {
                Name = "Town Archery",
                MessageIds = { (1014, 0) },
                LocationsAffected = { Item.UpgradeBigQuiver, Item.HeartPieceTownArchery },
                SubtractPriceAddresses = { 0xE38A26 },
                PriceAddresses = { 0xE3897A },
                Category = PriceMode.Minigames,
                Cost = 20,
            },

            // Night Withdraw
            new MessageCost
            {
                Name = "Night Withdraw",
                MessageIds = { (1143, 0) },
                Category = PriceMode.Misc,
                Cost = 4,
            },

            // Bomb Shop Keg
            new MessageCost
            {
                Name = "Bomb Shop Keg",
                MessageIds = { (1647, 0), (1648, 0), (1656, 0), (1657, 0), (1658, 0) },
                ItemsAffected = { Item.ItemPowderKeg },
                Cost = 50,
            },

            // TCG Deku
            new MessageCost
            {
                MessageIds = { (1904, 0) },
                LocationsAffected = { Item.MundaneItemTreasureChestGameDekuNuts },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // TCG Human
            new MessageCost
            {
                MessageIds = { (1905, 0) },
                LocationsAffected = { Item.MundaneItemTreasureChestGamePurpleRupee },
                Category = PriceMode.Minigames,
                Cost = 20,
            },

            // TCG Goron
            new MessageCost
            {
                MessageIds = { (1906, 0) },
                LocationsAffected = { Item.HeartPieceTreasureChestGame },
                Category = PriceMode.Minigames,
                Cost = 30,
            },

            // TCG Zora
            new MessageCost
            {
                MessageIds = { (1907, 0) },
                LocationsAffected = { Item.MundaneItemTreasureChestGameRedRupee },
                Category = PriceMode.Minigames,
                Cost = 5,
            },

            // Boat Ride Adult
            new MessageCost
            {
                Name = "Boat Ride Adult",
                MessageIds = { (2139, 0) },
                Category = PriceMode.Misc,
                Cost = 50,
            },

            // Boat Ride Child
            new MessageCost
            {
                Name = "Boat Ride Child",
                MessageIds = { (2139, 1), (2142, 0) },
                SubtractPriceAddresses = { 0xEBADB0 },
                PriceAddresses = { 0xEBADA6 },
                Category = PriceMode.Misc,
                Cost = 10,
            },

            // Boat Archery
            new MessageCost
            {
                MessageIds = { (2162, 0) },
                LocationsAffected = { Item.HeartPieceBoatArchery },
                SubtractPriceAddresses = { 0xEBAE3A },
                PriceAddresses = { 0xEBAE2E },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Magic Bean
            new MessageCost
            {
                Name = "Bean Salesman",
                MessageIds = { (2355, 0) },
                LocationsAffected = { Item.OtherLimitlessBeans },
                SubtractPriceAddresses = { 0xDC572E },
                PriceAddresses = { 0xDC56A6 },
                Cost = 10,
            },

            // Swamp Archery
            new MessageCost
            {
                Name = "Swamp Archery",
                MessageIds = { (2602, 0) },
                LocationsAffected = { Item.UpgradeBiggestQuiver, Item.HeartPieceSwampArchery },
                SubtractPriceAddresses = { 0xE3801E },
                PriceAddresses = { 0xE37FC2 },
                Category = PriceMode.Minigames,
                Cost = 20,
            },

            // Smithy 1
            new MessageCost
            {
                MessageIds = { (3131, 0), (3131, 1), (3135, 0) },
                LocationsAffected = { Item.UpgradeRazorSword },
                Cost = 100,
            },

            // Powder Keg
            new MessageCost
            {
                Name = "Goron Village Powder Keg",
                MessageIds = { (3212, 0) },
                SubtractPriceAddresses = { 0xE800C5 },
                PriceAddresses = { 0xE800B9 },
                Cost = 100,
            },

            // Fisherman Game
            new MessageCost
            {
                MessageIds = { (4246, 0), (4250, 0), (4251, 0) },
                LocationsAffected = { Item.HeartPieceFishermanGame },
                SubtractPriceAddresses = { 0x1079672 },
                PriceAddresses = { 0x107964A },
                Category = PriceMode.Minigames,
                Cost = 20,
            },

            // Swamp Scrub Purchase
            new MessageCost
            {
                MessageIds = { (5609, 0), (5619, 0) },
                LocationsAffected = { Item.ShopItemBusinessScrubMagicBean },
                SubtractPriceAddresses = { 0x01051B70 + 0x672 },
                PriceAddresses = { 0x01051B70 + 0x65A },
                Cost = 10,
            },

            // Mountain Scrub Purchase
            new MessageCost
            {
                MessageIds = { (5632, 0), (5638, 0) },
                LocationsAffected = { Item.UpgradeBiggestBombBag },
                SubtractPriceAddresses = { 0x1052262 },
                PriceAddresses = { 0x105226A },
                Cost = 200,
            },

            // Ocean Scrub Purchase
            new MessageCost
            {
                MessageIds = { (5650, 0), (5655, 0) },
                LocationsAffected = { Item.ShopItemBusinessScrubGreenPotion },
                SubtractPriceAddresses = { 0x01051B70 + 0x74A },
                PriceAddresses = { 0x01051B70 + 0x752 },
                Cost = 40,
            },

            // Canyon Scrub Purchase
            new MessageCost
            {
                MessageIds = { (5670, 0), (5677, 0) },
                LocationsAffected = { Item.ShopItemBusinessScrubBluePotion },
                SubtractPriceAddresses = { 0x01051B70 + 0x7AA },
                PriceAddresses = { 0x01051B70 + 0x7B2 },
                Cost = 100,
            },

            // Business Scrub Purchase 1
            new MessageCost
            {
                Name = "Business Scrub Purchase 1",
                MessageIds = { (5682, 0) },
                LocationsAffected = { Item.HeartPieceTerminaBusinessScrub },
                PriceAddresses = { 0x1030852, 0x10316CA, 0x102FEF6 },
                Cost = 150,
            },

            // Business Scrub Purchase 2
            new MessageCost
            {
                Name = "Business Scrub Purchase 2",
                MessageIds = { (5684, 0) },
                LocationsAffected = { Item.HeartPieceTerminaBusinessScrub },
                PriceAddresses = { 0x102FF02 },
                Cost = 100,
            },

            // Tingle Town in Town
            new MessageCost
            {
                MessageIds = { (7441, 0) },
                LocationsAffected = { Item.ItemTingleMapTownInTown },
                Cost = 5,
            },

            // Tingle Swamp in Town
            new MessageCost
            {
                MessageIds = { (7441, 1) },
                LocationsAffected = { Item.ItemTingleMapWoodfallInTown },
                Cost = 40,
            },

            // Tingle Swamp in Swamp
            new MessageCost
            {
                MessageIds = { (7442, 0) },
                LocationsAffected = { Item.ItemTingleMapWoodfallInSwamp },
                Cost = 20,
            },

            // Tingle Mountain in Swamp
            new MessageCost
            {
                MessageIds = { (7442, 1) },
                LocationsAffected = { Item.ItemTingleMapSnowheadInSwamp },
                Cost = 40,
            },

            // Tingle Mountain in Mountain
            new MessageCost
            {
                MessageIds = { (7443, 0) },
                LocationsAffected = { Item.ItemTingleMapSnowheadInMountain },
                Cost = 20,
            },

            // Tingle Ranch in Mountain
            new MessageCost
            {
                MessageIds = { (7443, 1) },
                LocationsAffected = { Item.ItemTingleMapRanchInMountain },
                Cost = 40,
            },

            // Tingle Ranch in Ranch
            new MessageCost
            {
                MessageIds = { (7444, 0) },
                LocationsAffected = { Item.ItemTingleMapRanchInRanch },
                Cost = 20,
            },

            // Tingle Ocean in Ranch
            new MessageCost
            {
                MessageIds = { (7444, 1) },
                LocationsAffected = { Item.ItemTingleMapGreatBayInRanch },
                Cost = 40,
            },

            // Tingle Ocean in Ocean
            new MessageCost
            {
                MessageIds = { (7445, 0) },
                LocationsAffected = { Item.ItemTingleMapGreatBayInOcean },
                Cost = 20,
            },

            // Tingle Canyon in Ocean
            new MessageCost
            {
                MessageIds = { (7445, 1) },
                LocationsAffected = { Item.ItemTingleMapStoneTowerInOcean },
                Cost = 40,
            },

            // Tingle Canyon in Canyon
            new MessageCost
            {
                MessageIds = { (7446, 0) },
                LocationsAffected = { Item.ItemTingleMapStoneTowerInCanyon },
                Cost = 20,
            },

            // Tingle Town in Canyon
            new MessageCost
            {
                MessageIds = { (7446, 1) },
                LocationsAffected = { Item.ItemTingleMapTownInCanyon },
                Cost = 40,
            },

            // Sword School Novice
            new MessageCost
            {
                Name = "Swordsman's School Novice",
                MessageIds = { (10006, 0) },
                Category = PriceMode.Minigames,
                Cost = 1,
            },

            // Sword School HP
            new MessageCost
            {
                MessageIds = { (10006, 1) },
                LocationsAffected = { Item.HeartPieceSwordsmanSchool },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Postman Game
            new MessageCost
            {
                Name = "Postman Game Retry",
                MessageIds = { (10126, 0), (10137, 0), (10138, 0), (10143, 0) },
                //LocationsAffected = { Item.HeartPieceNotebookPostman },
                Category = PriceMode.Minigames,
                Cost = 2,
            },

            // Deku Playground
            new MessageCost
            {
                Name = "Deku Playground",
                MessageIds = { (10210, 0) },
                LocationsAffected = { Item.OtherPlayDekuPlayground },
                SubtractPriceAddresses = { 0xF4FBBE },
                PriceAddresses = { 0xF4FB8E },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Milk Bar Milk
            new MessageCost
            {
                MessageIds = { (11019, 0) },
                LocationsAffected = { Item.ShopItemMilkBarMilk },
                SubtractPriceAddresses = { 0x10469F9, 0x104698D },
                PriceAddresses = { 0x10469CC, 0x1046960 },
                Cost = 20,
            },

            // Milk Bar Chateau
            new MessageCost
            {
                MessageIds = { (11019, 1), (0x2AFA, 0) },
                LocationsAffected = { Item.ShopItemMilkBarChateau },
                SubtractPriceAddresses = { 0x10469EA, 0x104697E },
                PriceAddresses = { 0x10469D4, 0x1046968 },
                Cost = 200,
            },

            // Lottery
            new MessageCost
            {
                MessageIds = { (11100, 0), (11101, 0) },
                LocationsAffected = { Item.MundaneItemLotteryPurpleRupee},
                SubtractPriceAddresses = { 0x1015CE6 },
                PriceAddresses = { 0x1015CAA },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Gorman Milk
            new MessageCost
            {
                MessageIds = { (13414, 0), (13456, 0) },
                LocationsAffected = { Item.ShopItemGormanBrosMilk },
                Cost = 50,
            },

            // Gorman Race
            new MessageCost
            {
                MessageIds = { (13423, 0), (13425, 0), (13427, 0), (13432, 0), (13442, 0), (13463, 0), (13477, 0) },
                LocationsAffected = { Item.MaskGaro },
                Category = PriceMode.Minigames,
                Cost = 10,
            },

            // Bomb Shop
            new MessageCost
            {
                MessageIds = { (1616, 0), (1617, 0) },
                LocationsAffected = { Item.ShopItemBombsBomb10 },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1618, 0), (1619, 0) },
                LocationsAffected = { Item.ShopItemBombsBombchu10 },
                Cost = 40,
            },
            new MessageCost
            {
                MessageIds = { (1620, 0), (1621, 0) },
                LocationsAffected = { Item.ItemBombBag },
                Cost = 50,
            },
            new MessageCost
            {
                MessageIds = { (1622, 0), (1623, 0) },
                LocationsAffected = { Item.UpgradeBigBombBag },
                Cost = 90,
            },

            // Trading Post Main Guy
            new MessageCost
            {
                MessageIds = { (1708, 0), (1709, 0) },
                LocationsAffected = { Item.ShopItemTradingPostRedPotion },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1710, 0), (1711, 0) },
                LocationsAffected = { Item.ShopItemTradingPostShield },
                Cost = 80,
            },
            new MessageCost
            {
                MessageIds = { (1712, 0), (1713, 0) },
                LocationsAffected = { Item.ShopItemTradingPostNut10 },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1714, 0), (1715, 0) },
                LocationsAffected = { Item.ShopItemTradingPostGreenPotion },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1716, 0), (1717, 0) },
                LocationsAffected = { Item.ShopItemTradingPostStick },
                Cost = 10,
            },
            new MessageCost
            {
                MessageIds = { (1718, 0), (1719, 0) },
                LocationsAffected = { Item.ShopItemTradingPostFairy },
                Cost = 50,
            },
            new MessageCost
            {
                MessageIds = { (1720, 0), (1721, 0) },
                LocationsAffected = { Item.ShopItemTradingPostArrow30 },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1722, 0), (1723, 0) },
                LocationsAffected = { Item.ShopItemTradingPostArrow50 },
                Cost = 40,
            },

            // Trading Post Part Timer
            new MessageCost
            {
                MessageIds = { (1737, 0), (1738, 0) },
                LocationsAffected = { Item.ShopItemTradingPostRedPotion },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1739, 0), (1740, 0) },
                LocationsAffected = { Item.ShopItemTradingPostShield },
                Cost = 80,
            },
            new MessageCost
            {
                MessageIds = { (1741, 0), (1742, 0) },
                LocationsAffected = { Item.ShopItemTradingPostNut10 },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1743, 0), (1744, 0) },
                LocationsAffected = { Item.ShopItemTradingPostGreenPotion },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1745, 0), (1746, 0) },
                LocationsAffected = { Item.ShopItemTradingPostStick },
                Cost = 10,
            },
            new MessageCost
            {
                MessageIds = { (1747, 0), (1748, 0) },
                LocationsAffected = { Item.ShopItemTradingPostFairy },
                Cost = 50,
            },
            new MessageCost
            {
                MessageIds = { (1749, 0), (1750, 0) },
                LocationsAffected = { Item.ShopItemTradingPostArrow30 },
                Cost = 30,
            },
            new MessageCost
            {
                MessageIds = { (1751, 0), (1752, 0) },
                LocationsAffected = { Item.ShopItemTradingPostArrow50 },
                Cost = 40,
            },

            // Witch Shop
            new MessageCost
            {
                MessageIds = { (2111, 0), (2112, 0) },
                LocationsAffected = { Item.ShopItemWitchRedPotion },
                Cost = 20,
            },
            new MessageCost
            {
                MessageIds = { (2113, 0), (2114, 0) },
                LocationsAffected = { Item.ShopItemWitchGreenPotion },
                Cost = 10,
            },
            new MessageCost
            {
                Name = "Witch Shop Blue Potion",
                MessageIds = { (2115, 0), (2116, 0), (2176, 0) },
                Cost = 60,
                // Blue Potion logic not affected because of free sample
            },

            // Goron Shop (Winter)
            new MessageCost
            {
                MessageIds = { (3013, 0), (3014, 0) },
                LocationsAffected = { Item.ShopItemGoronBomb10InWinter },
                Cost = 40,
            },
            new MessageCost
            {
                MessageIds = { (3015, 0), (3016, 0) },
                LocationsAffected = { Item.ShopItemGoronArrow10InWinter },
                Cost = 40,
            },
            new MessageCost
            {
                MessageIds = { (3017, 0), (3018, 0) },
                LocationsAffected = { Item.ShopItemGoronRedPotionInWinter },
                Cost = 80,
            },

            // Goron Shop (Spring)
            new MessageCost
            {
                MessageIds = { (3019, 0), (3020, 0) },
                LocationsAffected = { Item.ShopItemGoronBomb10InSpring },
                Cost = 10,
            },
            new MessageCost
            {
                MessageIds = { (3021, 0), (3022, 0) },
                LocationsAffected = { Item.ShopItemGoronArrow10InSpring },
                Cost = 20,
            },
            new MessageCost
            {
                MessageIds = { (3023, 0), (3024, 0) },
                LocationsAffected = { Item.ShopItemGoronRedPotionInSpring },
                Cost = 50,
            },

            // Zora Shop
            new MessageCost
            {
                MessageIds = { (4827, 0), (4828, 0) },
                LocationsAffected = { Item.ShopItemZoraShield },
                Cost = 90,
            },
            new MessageCost
            {
                MessageIds = { (4829, 0), (4830, 0) },
                LocationsAffected = { Item.ShopItemZoraArrow10 },
                Cost = 20,
            },
            new MessageCost
            {
                MessageIds = { (4831, 0), (4832, 0) },
                LocationsAffected = { Item.ShopItemZoraRedPotion },
                Cost = 60,
            },

            // Curiosity Shop
            new MessageCost
            {
                MessageIds = { (10713, 0), (10714, 0) },
                LocationsAffected = { Item.MaskAllNight, Item.NotebookPurchaseCuriosityShopItem },
                Cost = 500,
            },
            new MessageCost
            {
                Name = "Curiosity Shop Big Bomb Bag",
                MessageIds = { (10715, 0), (10716, 0) },
                Cost = 100,
                // Big Bomb Bag logic not affected due to time of day logic
            },
            new MessageCost
            {
                // Nice Sword
                Name = "Curiosity Shop Nice Sword",
                MessageIds = { (10738, 0), (10739, 0) },
                Cost = 100,
            },
            new MessageCost
            {
                // Good Sword
                Name = "Curiosity Shop Good Sword",
                MessageIds = { (10740, 0), (10741, 0) },
                Cost = 50,
            },
            new MessageCost
            {
                // Cool Bottle
                Name = "Curiosity Shop Cool Bottle",
                MessageIds = { (10744, 0), (10745, 0) },
                Cost = 20,
            },

            // Poe Hut
            new MessageCost
            {
                Name = "Poe Hut",
                MessageIds = { (5332, 0), (5349, 0) },
                LocationsAffected = { Item.HeartPiecePoeHut },
                PriceAddresses = { 0xF748F0 + 0x132, 0xF748F0 + 0x18E, 0xF748F0 + 0xA3E },
                Category = PriceMode.Minigames,
                Cost = 30,
            },

            // Healed Poe Hut
            new MessageCost
            {
                Name = "Healed Poe Hut",
                MessageIds = { (5349, 1) },
                // LocationsAffected = { Item.HeartPiecePoeHut }, // TODO handle logic. This should be a separate logic entry.
                PriceAddresses = { 0xF748F0 + 0x18A, 0xF748F0 + 0x936 },
                Category = PriceMode.Minigames,
                Cost = 10,
            },
        };
    }
}
