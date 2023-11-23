using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Models;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.GameObjects
{
    public enum Item
    {
        // free
        [Repeatable]
        [StartingItem(0xC5CE41, 0x32)]
        [ItemName("Deku Mask"), LocationName("Starting Item"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a woodland spirit")]
        [ShopText("Wear it to assume Deku form.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x78), ItemPool(ItemCategory.Masks, LocationCategory.StartingItems, ClassicCategory.BaseItemPool)]
        MaskDeku,

        // items
        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x01)]
        [Overwritable(OverwritableAttribute.ItemSlot.Quiver, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Hero's Bow"), LocationName("Hero's Bow Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a projectile", "a ranged weapon")]
        [ShopText("Use it to shoot arrows.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02223000 + 0xAA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x22), ItemPool(ItemCategory.MainInventory, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        ItemBow,

        [Repeatable]
        [StartingItem(0xC5CE26, 0x02)]
        [ItemName("Fire Arrow"), LocationName("Fire Arrow Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("the power of fire", "a magical item")]
        [ShopText("Arm your bow with arrows that burst into flame.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02336000 + 0xCA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x25), ItemPool(ItemCategory.MainInventory, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        ItemFireArrow,

        [Repeatable]
        [StartingItem(0xC5CE27, 0x03)]
        [ItemName("Ice Arrow"), LocationName("Ice Arrow Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("the power of ice", "a magical item")]
        [ShopText("Arm your bow with arrows that freeze.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0292F000 + 0x11E, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x26), ItemPool(ItemCategory.MainInventory, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        ItemIceArrow,

        [Repeatable]
        [StartingItem(0xC5CE28, 0x04)]
        [ItemName("Light Arrow"), LocationName("Light Arrow Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("the power of light", "a magical item")]
        [ShopText("Arm your bow with arrows infused with sacred light.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0212B000 + 0xB2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02192000 + 0x8E)]
        [GetItemIndex(0x27), ItemPool(ItemCategory.MainInventory, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        ItemLightArrow,

        [Repeatable]
        [Progressive]
        [Purchaseable, Visible]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x08)]
        [Overwritable(OverwritableAttribute.ItemSlot.BombBag, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Bomb Bag"), LocationName("Bomb Bag Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 0)]
        [ShopText("This can hold up to a maximum of 20 bombs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1B), ItemPool(ItemCategory.MainInventory, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ItemBombBag,

        [Repeatable, Temporary]
        [StartingItemId(0x0A)]
        [ItemName("Magic Bean"), LocationName("Bean Man"), Region(Region.DekuPalace)]
        [GossipLocationHint("a hidden merchant", "a gorging merchant"), GossipItemHint("a plant seed")]
        [ShopText("Plant it in soft soil.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x11E), ItemPool(ItemCategory.MainInventory, LocationCategory.Purchases, ClassicCategory.BaseItemPool)]
        ItemMagicBean,

        [Repeatable, Purchaseable]
        [StartingItemId(0x0C)]
        [ItemName("Powder Keg"), LocationName("Powder Keg Challenge"), Region(Region.GoronVillage)]
        [GossipLocationHint("a large goron"), GossipItemHint("gunpowder", "a dangerous item", "an explosive barrel")]
        [ShopText("Both its power and its size are immense!")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [HackContent(nameof(Resources.mods.fix_keg_check))]
        [GetItemIndex(0x123), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemPowderKeg,

        [Repeatable]
        [StartingItem(0xC5CE31, 0x0D)]
        [ItemName("Pictograph Box"), LocationName("Koume"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a witch"), GossipItemHint("a light recorder", "a capture device")]
        [ShopText("Use it to snap pictographs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x43), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemPictobox,

        [Repeatable]
        [StartingItem(0xC5CE32, 0x0E)]
        [ItemName("Lens of Truth"), LocationName("Lens of Truth Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("eyeglasses", "the truth", "focused vision")]
        [ShopText("Uses magic to see what the naked eye cannot.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02EB8000 + 0x9A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x42), ItemPool(ItemCategory.MainInventory, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        ItemLens,

        [Repeatable]
        [StartingItem(0xC5CE33, 0x0F)]
        [ItemName("Hookshot"), LocationName("Hookshot Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a chain and grapple")]
        [ShopText("Use it to grapple objects.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0238B000 + 0x14A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x41), ItemPool(ItemCategory.MainInventory, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        ItemHookshot,

        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CDED, 0x30)]
        [StartingItem(0xC5CDF4, 0x01)]
        [ItemName("Magic Power"), LocationName("Town Great Fairy Non-Human"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a magical being"), GossipItemHint("magic power")]
        [ShopText("Grants the ability to use magic.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12C), ItemPool(ItemCategory.MagicPowers, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        FairyMagic,

        [Repeatable]
        [StartingItemId(0xA6)]
        [ItemName("Spin Attack Mastery"), LocationName("Woodfall Great Fairy"), Region(Region.Woodfall)]
        [GossipLocationHint("a magical being"), GossipItemHint("a magic attack"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false, nameof(GameplaySettings.StrayFairyMode), (int)StrayFairyMode.ChestsOnly, false)]
        [ShopText("Increases the power of your spin attack.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12D), ItemPool(ItemCategory.MagicPowers, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        FairySpinAttack,

        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CDED, 0x60)]
        [StartingItem(0xC5CDF4, 0x01)]
        [StartingItem(0xC5CDF5, 0x01)]
        [ItemName("Extended Magic Power"), LocationName("Snowhead Great Fairy"), Region(Region.Snowhead)]
        [GossipLocationHint("a magical being"), GossipItemHint("magic power"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false, nameof(GameplaySettings.StrayFairyMode), (int)StrayFairyMode.ChestsOnly, false)]
        [ShopText("Grants the ability to use lots of magic.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12E), ItemPool(ItemCategory.MagicPowers, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        FairyDoubleMagic,

        [Repeatable]
        [StartingItem(0xC5CDF6, 0x01)]
        [StartingItem(0xC5CE87, 0x14)]
        [ItemName("Double Defense"), LocationName("Ocean Great Fairy"), Region(Region.ZoraCape)]
        [GossipLocationHint("a magical being"), GossipItemHint("magical defense"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false, nameof(GameplaySettings.StrayFairyMode), (int)StrayFairyMode.ChestsOnly, false)]
        [ShopText("Take half as much damage from enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12F), ItemPool(ItemCategory.MagicPowers, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        FairyDoubleDefense,

        [Repeatable]
        [StartingItem(0xC5CE34, 0x10)]
        [ItemName("Great Fairy's Sword"), LocationName("Ikana Great Fairy"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a magical being"), GossipItemHint("a black rose", "a powerful blade"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false, nameof(GameplaySettings.StrayFairyMode), (int)StrayFairyMode.ChestsOnly, false)]
        [ShopText("The most powerful sword has black roses etched in its blade.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x130), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        ItemFairySword,

        [StartingItemId(0x11)]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)] // specially handled to turn into Red Potion on subsequent times
        [ItemName("Bottle of Red Potion"), LocationName("Kotake"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("the sleeping witch"), GossipItemHint("a vessel of health", "bottled fortitude")]
        [ShopText("Replenishes your life energy.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x59), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemBottleWitch,

        [StartingItemId(0x18)]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)] // specially handled to turn into Milk on subsequent times
        [ItemName("Milk Bottle"), LocationName("Aliens Defense"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch girl", "a good deed"), GossipItemHint("a dairy product", "the produce of cows"), GossipCompetitiveHint(-2)]
        [ShopText("Recover five hearts with one drink. Contains two helpings.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x60), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemBottleAliens,

        [RupeeRepeatable]
        [StartingItemId(0x22)]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)] // specially handled to turn into Gold Dust on subsequent times
        [ItemName("Bottle of Gold Dust"), LocationName("Goron Race"), Region(Region.TwinIslands)]
        [GossipLocationHint("a sporting event"), GossipItemHint("a gleaming powder"), GossipCompetitiveHint(-2)]
        [ShopText("It's very high quality.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x6A), ItemPool(ItemCategory.MainInventory, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        ItemBottleGoronRace,

        [StartingItemId(0x12)]
        [ItemName("Empty Bottle"), LocationName("Beaver Race #1"), Region(Region.ZoraCape)]
        [GossipLocationHint("a river dweller"), GossipItemHint("an empty vessel", "a glass container"), GossipCompetitiveHint(-2, nameof(GameplaySettings.UpdateNPCText), false)]
        [ShopText("Carry various items in this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x5A), ItemPool(ItemCategory.MainInventory, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        ItemBottleBeavers,

        [StartingItemId(0x12)]
        [ItemName("Empty Bottle"), LocationName("Dampe Digging"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a fearful basement"), GossipItemHint("an empty vessel", "a glass container"), GossipCompetitiveHint(0, nameof(GameplaySettings.UpdateNPCText), false)]
        [ShopText("Carry various items in this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0261E000 + 0x1FE, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x64), ItemPool(ItemCategory.MainInventory, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        ItemBottleDampe,

        [StartingItemId(0x25)]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)] // specially handled to turn into Chateau Romani on subsequent times
        [ItemName("Bottle of Chateau Romani"), LocationName("Madame Aroma in Bar"), Region(Region.EastClockTown)]
        [GossipLocationHint("an important lady"), GossipItemHint("a dairy product", "an adult beverage")]
        [ShopText("Drink it to get lasting stamina for your magic power.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x6F), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemBottleMadameAroma,

        [Repeatable]
        [StartingItem(0xC5CE71, 0x04)]
        [ItemName("Bombers' Notebook"), LocationName("Bombers' Hide and Seek"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a group of children", "a town game"), GossipItemHint("a handy notepad", "a quest logbook")]
        [ShopText("Allows you to keep track of people's schedules.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x50), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        ItemNotebook,

        //upgrades
        [Repeatable]
        [Progressive]
        [Downgradable]
        [Visible]
        [StartingItem(0xC5CE21, 0x02)]
        [StartingItem(0xC5CE00, 0x4E)]
        [Overwritable(OverwritableAttribute.ItemSlot.Sword, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Razor Sword"), LocationName("Mountain Smithy Day 1"), Region(Region.MountainVillage)]
        [GossipLocationHint("the mountain smith"), GossipItemHint("a sharp blade")]
        [ShopText("A sharp sword forged at the smithy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x38), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        UpgradeRazorSword,

        [Repeatable]
        [Progressive]
        [Downgradable]
        [Visible]
        [StartingItem(0xC5CE21, 0x03)]
        [StartingItem(0xC5CE00, 0x4F)]
        [Overwritable(OverwritableAttribute.ItemSlot.Sword, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Gilded Sword"), LocationName("Mountain Smithy Day 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("the mountain smith"), GossipItemHint("a sharp blade")]
        [ShopText("A very sharp sword forged from gold dust.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x39), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        UpgradeGildedSword,

        [Repeatable]
        [Downgradable]
        [StartingItem(0xC5CE21, 0x20)]
        [Overwritable(OverwritableAttribute.ItemSlot.Shield, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Mirror Shield"), LocationName("Mirror Shield Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a reflective guard", "echoing protection")]
        [ShopText("It can reflect certain rays of light.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x029FE000 + 0x1AA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x33), ItemPool(ItemCategory.MainInventory, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        UpgradeMirrorShield,

        [Repeatable]
        [RupeeRepeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x02)]
        [Overwritable(OverwritableAttribute.ItemSlot.Quiver, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Large Quiver"), LocationName("Town Archery #1"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town activity"), GossipItemHint("a projectile", "a ranged weapon"), GossipCompetitiveHint(-3)]
        [ShopText("This can hold up to a maximum of 40 arrows.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x23), ItemPool(ItemCategory.MainInventory, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        UpgradeBigQuiver,

        [Repeatable]
        [RupeeRepeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x03)]
        [Overwritable(OverwritableAttribute.ItemSlot.Quiver, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Largest Quiver"), LocationName("Swamp Archery #1"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a projectile", "a ranged weapon"), GossipCompetitiveHint(-3)]
        [ShopText("This can hold up to a maximum of 50 arrows.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x24), ItemPool(ItemCategory.MainInventory, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        UpgradeBiggestQuiver,

        [Repeatable]
        [Progressive]
        [Downgradable]
        [Purchaseable, Visible]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x10)]
        [Overwritable(OverwritableAttribute.ItemSlot.BombBag, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Big Bomb Bag"), LocationName("Big Bomb Bag Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.CuriosityShop, 2)]
        [ShopText("This can hold up to a maximum of 30 bombs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1C), ItemPool(ItemCategory.MainInventory, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        UpgradeBigBombBag,

        [Repeatable]
        [Progressive]
        [Downgradable, Purchaseable]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x18)]
        [Overwritable(OverwritableAttribute.ItemSlot.BombBag, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Biggest Bomb Bag"), LocationName("Biggest Bomb Bag Purchase"), MultiLocation(UpgradeBiggestBombBagInMountain, UpgradeBiggestBombBagInSwamp)]
        [GossipLocationHint("a northern merchant"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopText("This can hold up to a maximum of 40 bombs.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1D), ItemPool(ItemCategory.MainInventory, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        UpgradeBiggestBombBag,

        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CE6E, 0x10)]
        [Overwritable(OverwritableAttribute.ItemSlot.Wallet, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Adult Wallet"), LocationName("Bank Reward #1"), Region(Region.WestClockTown)]
        [GossipLocationHint("a keeper of wealth"), GossipItemHint("a coin case", "great wealth")]
        [ShopText("This can hold up to a maximum of 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x08), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        UpgradeAdultWallet,

        [Repeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE6E, 0x20)]
        [Overwritable(OverwritableAttribute.ItemSlot.Wallet, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Giant Wallet"), LocationName("Ocean Spider House Day 1 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("a coin case", "great wealth"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false, nameof(GameplaySettings.UpdateNPCText), false)]
        [ShopText("This can hold up to a maximum of 500 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x09), ItemPool(ItemCategory.MainInventory, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        UpgradeGiantWallet,

        [Repeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE6E, 0x30)]
        [Overwritable(OverwritableAttribute.ItemSlot.Wallet, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Royal Wallet"), LocationName("Removed by Royal Wallet"), Region(Region.Misc)]
        [GossipItemHint("a coin case", "great wealth")]
        [ShopText("This can hold up to a maximum of 999 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x44D), ItemPool(ItemCategory.RoyalWallet, LocationCategory.Fake, ClassicCategory.RoyalWallet)]
        UpgradeRoyalWallet,

        //trades
        [Visible]
        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.Trade, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Moon's Tear"), LocationName("Astronomy Telescope"), Region(Region.TerminaField)]
        [GossipLocationHint("a falling star"), GossipItemHint("a lunar teardrop", "celestial sadness")]
        [ShopText("A shining stone from the moon.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x96), ItemPool(ItemCategory.TradeItems, LocationCategory.Events, ClassicCategory.BaseItemPool)]
        TradeItemMoonTear,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.Trade, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Land Title Deed"), LocationName("Clock Town Scrub Trade"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x97), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemLandDeed,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.Trade, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Swamp Title Deed"), LocationName("Swamp Scrub Trade"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a southern merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Southern Swamp.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x98), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemSwampDeed,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.Trade, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Mountain Title Deed"), LocationName("Mountain Scrub Trade"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower near Goron Village.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x99), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemMountainDeed,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.Trade, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Ocean Title Deed"), LocationName("Ocean Scrub Trade"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Zora Hall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x9A), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemOceanDeed,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.KeyExpress, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Room Key"), LocationName("Inn Reservation"), Region(Region.StockPotInn)]
        [GossipLocationHint("checking in", "check-in"), GossipItemHint("a door opener", "a lock opener")]
        [ShopText("With this, you can go in and out of the Stock Pot Inn at night.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xA0), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemRoomKey,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.PendantKafei, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Letter to Kafei"), LocationName("Midnight Meeting"), Region(Region.StockPotInn)]
        [GossipLocationHint("a late meeting"), GossipItemHint("a lover's plight", "a lover's letter")]
        [ShopText("A love letter from Anju to Kafei.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xAA), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemKafeiLetter,

        [Visible]
        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.PendantKafei, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Pendant of Memories"), LocationName("Kafei"), Region(Region.LaundryPool)]
        [GossipLocationHint("a posted letter"), GossipItemHint("a cherished necklace", "a symbol of trust")]
        [ShopText("Kafei's symbol of trust for Anju.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xAB), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemPendant,

        [Repeatable, Overwritable(OverwritableAttribute.ItemSlot.KeyExpress, nameof(GameplaySettings.QuestItemStorage), false)]
        [Temporary(nameof(GameplaySettings.KeepQuestTradeThroughTime), false), Returnable(nameof(GameplaySettings.KeepQuestTradeThroughTime), true)]
        [ItemName("Letter to Mama"), LocationName("Curiosity Shop Man #2"), Region(Region.LaundryPool)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("an important note", "a special delivery")]
        [ShopText("It's a parcel for Kafei's mother.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xA1), ItemPool(ItemCategory.TradeItems, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        TradeItemMamaLetter,

        //notebook hp
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Mayor"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town leader", "an upstanding figure"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x03), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookMayor,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Postman's Game"), Region(Region.WestClockTown)]
        [GossipLocationHint("a hard worker", "a delivery person"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xCE), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookPostman,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Rosa Sisters"), Region(Region.WestClockTown)]
        [GossipLocationHint("traveling sisters", "twin entertainers"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookRosa,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Toilet Hand"), Region(Region.StockPotInn)]
        [GossipLocationHint("a mystery appearance", "a strange palm"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookHand,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Grandma Short Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("an old lady"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookGran1,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Grandma Long Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("an old lady"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceNotebookGran2,

        //other hp
        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Keaton Quiz"), MultiLocation(HeartPieceKeatonQuizInNCT, HeartPieceKeatonQuizInMilkRoad, HeartPieceKeatonQuizInMountainVillage)]
        [GossipLocationHint("the ghost of a fox", "a mysterious fox"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceKeatonQuiz,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Playground Three Days"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceDekuPlayground,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Town Archery #2"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1, nameof(GameplaySettings.DoubleArcheryRewards), false)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x90), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceTownArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Honey and Darling Three Days"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x94), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceHoneyAndDarling,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swordsman's School"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x9F), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceSwordsmanSchool,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Postbox"), MultiLocation(HeartPiecePostBoxInECT, HeartPiecePostBoxInNCT, HeartPiecePostBoxInSCT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("an information carrier", "a correspondence box"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA2), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPiecePostBox,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Gossip Stones"), Region(Region.TerminaField)]
        [GossipLocationHint("mysterious stones"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA3), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceTerminaGossipStones,

        [Purchaseable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Business Scrub Purchase"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden merchant"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA5), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Purchases, ClassicCategory.BaseItemPool)]
        HeartPieceTerminaBusinessScrub,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swamp Archery #2"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1, nameof(GameplaySettings.DoubleArcheryRewards), false)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA6), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceSwampArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Pictograph Contest Winner"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA7), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPiecePictobox,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Boat Archery"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health"), GossipCompetitiveHint]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA8), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceBoatArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Frog Choir"), Region(Region.MountainVillage)]
        [GossipLocationHint("a reunion", "a chorus", "an amphibian choir"), GossipItemHint("a segment of health"), GossipCompetitiveHint(3)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAC), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceChoir,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Beaver Race #2"), Region(Region.ZoraCape)]
        [GossipLocationHint("a river dweller", "a race in the water"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAD), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceBeaverRace,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Seahorses"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a reunion"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAE), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceSeaHorse,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Fisherman Game"), Region(Region.GreatBayCoast), GossipCompetitiveHint(-2)]
        [GossipLocationHint("an ocean game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAF), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceFishermanGame,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Evan"), Region(Region.ZoraHall)]
        [GossipLocationHint("a muse", "a composition", "a musician", "plagiarism"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB0), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        HeartPieceEvan,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Dog Race"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting event"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB1), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceDogRace,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Poe Hut"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a game of ghosts"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB2), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        HeartPiecePoeHut,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Treasure Chest Game Goron"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x17), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        HeartPieceTreasureChestGame,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Peahat Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ED3000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x18), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        HeartPiecePeahat,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Dodongo Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EBD000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x20), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        HeartPieceDodong,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Woodfall Bridge Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("the swamp lands", "an exposed chest"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x252, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA52)]
        [GetItemIndex(0x29), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        HeartPieceWoodFallChest,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Twin Islands Underwater Ramp Chest"), Region(Region.TwinIslands)]
        [GossipLocationHint("a spring treasure", "a defrosted land", "a submerged chest"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C23000 + 0x2B6, ChestAttribute.AppearanceType.Normal, 0x02C34000 + 0x19A)]
        [GetItemIndex(0x2E), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        HeartPieceTwinIslandsChest,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ocean Spider House Chest"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("the strange masks", "coloured faces"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x024DB000 + 0x76, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x14), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        HeartPieceOceanSpiderHouse,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Iron Knuckle Chest"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x01FAB000 + 0xBA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x44), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        HeartPieceKnuckle,

        //mask
        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE3C, 0x3E)]
        [ItemName("Postman's Hat"), LocationName("Postman's Freedom Reward"), Region(Region.EastClockTown)]
        [GossipLocationHint("a special delivery", "one last job"), GossipItemHint("a hard worker's hat")]
        [ShopText("You can look into mailboxes when you wear this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x84), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskPostmanHat,

        [Repeatable]
        [Purchaseable, Visible]
        [StartingItem(0xC5CE3D, 0x38)]
        [ItemName("All-Night Mask"), LocationName("All-Night Mask Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("insomnia"), GossipCompetitiveHint(0, nameof(GameplaySettings.UpdateShopAppearance), false)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.CuriosityShop, 0)]
        [ShopText("When you wear it you don't get sleepy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7E), ItemPool(ItemCategory.Masks, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        MaskAllNight,

        [Repeatable]
        [StartingItem(0xC5CE3E, 0x47)]
        [ItemName("Blast Mask"), LocationName("Old Lady"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a good deed", "an old lady's struggle"), GossipItemHint("a dangerous mask")]
        [ShopText("Wear it and detonate it...")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8D), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskBlast,

        [Repeatable]
        [StartingItem(0xC5CE3F, 0x45)]
        [ItemName("Stone Mask"), LocationName("Invisible Soldier"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a hidden soldier", "a stone circle"), GossipItemHint("inconspicuousness")]
        [ShopText("Become as plain as stone so you can blend into your surroundings.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8B), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskStone,

        [Repeatable]
        [StartingItem(0xC5CE40, 0x40)]
        [ItemName("Great Fairy's Mask"), LocationName("Town Great Fairy"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a magical being"), GossipItemHint("a friend of fairies")]
        [ShopText("The mask's hair will shimmer when you're close to a Stray Fairy.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x131), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.GreatFairyRewards)]
        MaskGreatFairy,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE42, 0x3A)]
        [ItemName("Keaton Mask"), LocationName("Curiosity Shop Man #1"), Region(Region.LaundryPool)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a popular mask", "a fox's mask")]
        [ShopText("The mask of the ghost fox, Keaton.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x80), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskKeaton,

        [Repeatable]
        [StartingItem(0xC5CE43, 0x46)]
        [ItemName("Bremen Mask"), LocationName("Guru Guru"), Region(Region.LaundryPool)]
        [GossipLocationHint("a musician", "an entertainer"), GossipItemHint("a mask of leadership", "a bird's mask")]
        [ShopText("Wear it so young animals will mistake you for their leader.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8C), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskBremen,

        [Repeatable]
        [StartingItem(0xC5CE44, 0x39)]
        [ItemName("Bunny Hood"), LocationName("Grog"), Region(Region.RomaniRanch)]
        [GossipLocationHint("an ugly but kind heart", "a lover of chickens"), GossipItemHint("the ears of the wild", "a rabbit's hearing")]
        [ShopText("Wear it to be filled with the speed and hearing of the wild.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7F), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskBunnyHood,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE45, 0x42)]
        [ItemName("Don Gero's Mask"), LocationName("Hungry Goron"), Region(Region.MountainVillage)]
        [GossipLocationHint("a hungry goron", "a person in need"), GossipItemHint("a conductor's mask", "an amphibious mask")]
        [ShopText("When you wear it, you can call the Frog Choir members together.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x88), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskDonGero,

        [Repeatable]
        [RupeeRepeatable]
        [StartingItem(0xC5CE46, 0x48)]
        [ItemName("Mask of Scents"), LocationName("Butler"), Region(Region.DekuPalace)]
        [GossipLocationHint("a servant of royalty", "the royal servant"), GossipItemHint("heightened senses", "a pig's mask"), GossipCompetitiveHint(-1)]
        [ShopText("Wear it to heighten your sense of smell.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8E), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskScents,

        [Repeatable]
        [StartingItem(0xC5CE48, 0x3C)]
        [ItemName("Romani's Mask"), LocationName("Cremia"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch lady", "an older sister"), GossipItemHint("proof of membership", "a cow's mask"), GossipCompetitiveHint]
        [ShopText("Wear it to show you're a member of the Milk Bar, Latte.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x82), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskRomani,

        [Repeatable]
        [StartingItem(0xC5CE49, 0x3D)]
        [ItemName("Circus Leader's Mask"), LocationName("Gorman"), Region(Region.EastClockTown)]
        [GossipLocationHint("an entertainer", "a miserable leader"), GossipItemHint("a mask of sadness")]
        [ShopText("People related to Gorman will react to this.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x83), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskCircusLeader,

        [Repeatable]
        [StartingItem(0xC5CE4A, 0x37)]
        [ItemName("Kafei's Mask"), LocationName("Madame Aroma in Office"), Region(Region.EastClockTown)]
        [GossipLocationHint("an important lady", "an esteemed woman"), GossipItemHint("the mask of a missing one", "a son's mask")]
        [ShopText("Wear it to inquire about Kafei's whereabouts.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8F), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskKafei,

        [Repeatable]
        [StartingItem(0xC5CE4B, 0x3F)]
        [ItemName("Couple's Mask"), LocationName("Anju and Kafei"), Region(Region.StockPotInn)]
        [GossipLocationHint("a reunion", "a lovers' reunion"), GossipItemHint("a sign of love", "the mark of a couple"), GossipCompetitiveHint(3)]
        [ShopText("When you wear it, you can soften people's hearts.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x85), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskCouple,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE4C, 0x36)]
        [ItemName("Mask of Truth"), LocationName("Swamp Spider House Reward"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a gold spider"), GossipItemHint("a piercing gaze"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false, nameof(GameplaySettings.UpdateWorldModels), false)]
        [ShopText("Wear it to read the thoughts of Gossip Stones and animals.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8A), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskTruth,

        [Repeatable]
        [StartingItem(0xC5CE4E, 0x43)]
        [ItemName("Kamaro's Mask"), LocationName("Kamaro"), Region(Region.TerminaField)]
        [GossipLocationHint("a ghostly dancer", "a dancer"), GossipItemHint("dance moves")]
        [ShopText("Wear this to perform a mysterious dance.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x89), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskKamaro,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE4F, 0x41)]
        [ItemName("Gibdo Mask"), LocationName("Pamela's Father"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a healed spirit", "a lost father"), GossipItemHint("a mask of monsters")]
        [ShopText("Even a real Gibdo will mistake you for its own kind.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x87), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskGibdo,

        [Repeatable]
        [Visible]
        [RupeeRepeatable]
        [StartingItem(0xC5CE50, 0x3B)]
        [ItemName("Garo's Mask"), LocationName("Gorman Bros Race"), Region(Region.MilkRoad)]
        [GossipLocationHint("a sporting event"), GossipItemHint("the mask of spies")]
        [ShopText("This mask can summon the hidden Garo ninjas.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x81), ItemPool(ItemCategory.Masks, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        MaskGaro,

        [Repeatable]
        [StartingItem(0xC5CE51, 0x44)]
        [ItemName("Captain's Hat"), LocationName("Captain Keeta's Chest"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a ghostly battle", "a skeletal leader"), GossipItemHint("a commanding presence")]
        [ShopText("Wear it to pose as Captain Keeta.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0280D000 + 0x392, ChestAttribute.AppearanceType.Normal, 0x0280D000 + 0x6FA)]
        [GetItemIndex(0x7C), ItemPool(ItemCategory.Masks, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        MaskCaptainHat,

        [Repeatable]
        [StartingItem(0xC5CE52, 0x49)]
        [ItemName("Giant's Mask"), LocationName("Giant's Mask Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a growth spurt")]
        [ShopText("If you wear it in a certain room, you'll grow into a giant.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x020F1000 + 0x1C2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x19E)]
        [GetItemIndex(0x7D), ItemPool(ItemCategory.Masks, LocationCategory.Chests, ClassicCategory.BaseItemPool)]
        MaskGiant,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE47, 0x33)]
        [ItemName("Goron Mask"), LocationName("Darmani"), Region(Region.MountainVillage)]
        [GossipLocationHint("a healed spirit", "the lost champion"), GossipItemHint("a mountain spirit")]
        [ShopText("Wear it to assume Goron form.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x79), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskGoron,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE4D, 0x34)]
        [ItemName("Zora Mask"), LocationName("Mikau"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a healed spirit", "a fallen guitarist"), GossipItemHint("an ocean spirit")]
        [ShopText("Wear it to assume Zora form.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7A), ItemPool(ItemCategory.Masks, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        MaskZora,

        //song
        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE24, 0x00)]
        [ItemName("Ocarina of Time"), LocationName("Skull Kid"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a stolen possession"), GossipItemHint("a musical instrument")]
        [ShopText("This musical instrument is filled with memories of Princess Zelda.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x4C), ItemPool(ItemCategory.TimeTravel, LocationCategory.BossFights, ClassicCategory.OcarinaAndSongOfTime)]
        ItemOcarina,

        [Repeatable]
        [StartingItem(0xC5CE72, 0x10)]
        [ItemName("Song of Time"), LocationName("Skull Kid Song"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a distant memory"), GossipItemHint("a forgotten melody")]
        [ShopText("This melody is a song of memories of Princess Zelda.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x44C), ItemPool(ItemCategory.TimeTravel, LocationCategory.BossFights, ClassicCategory.OcarinaAndSongOfTime)]
        SongTime,

        [Repeatable]
        [StartingItem(0xC5CE72, 0x20)]
        [ItemName("Song of Healing"), LocationName("Starting Song"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a soothing melody")]
        [ShopText("This melody will soothe restless spirits.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x124), ItemPool(ItemCategory.Songs, LocationCategory.StartingItems, ClassicCategory.BaseItemPool)]
        SongHealing,

        [Repeatable]
        [StartingItem(0xC5CE72, 0x80)]
        [ItemName("Song of Soaring"), LocationName("Swamp Music Statue"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a stone tablet"), GossipItemHint("white wings")]
        [ShopText("This melody sends you to a stone bird statue in an instant.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x70), ItemPool(ItemCategory.SongOfSoaring, LocationCategory.NpcRewards, ClassicCategory.SongOfSoaring)]
        SongSoaring,

        [Repeatable]
        [RupeeRepeatable]
        [StartingItem(0xC5CE72, 0x40)]
        [ItemName("Epona's Song"), LocationName("Romani's Game"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a reunion"), GossipItemHint("a horse's song", "a song of the field")]
        [ShopText("This melody calls your horse, Epona.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x71), ItemPool(ItemCategory.Songs, LocationCategory.Minigames, ClassicCategory.BaseItemPool)]
        SongEpona,

        [Repeatable]
        [StartingItem(0xC5CE71, 0x01)]
        [ItemName("Song of Storms"), LocationName("Day 1 Grave Tablet"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a hollow ground", "a stone tablet"), GossipItemHint("rain and thunder", "stormy weather")]
        [ShopText("This melody is the turbulent tune that blows curses away.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x72), ItemPool(ItemCategory.Songs, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        SongStorms,

        [Repeatable]
        [StartingItem(0xC5CE73, 0x40)]
        [ItemName("Sonata of Awakening"), LocationName("Imprisoned Monkey"), Region(Region.DekuPalace)]
        [GossipLocationHint("a prisoner", "a false imprisonment"), GossipItemHint("a royal song", "an awakening melody")]
        [ShopText("This melody awakens those who have fallen into a deep sleep.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x73), ItemPool(ItemCategory.Songs, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        SongSonata,

        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CE73, 0x80)]
        [ItemName("Goron Lullaby"), LocationName("Baby Goron"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely child", "an elder's son"), GossipItemHint("a sleepy melody", "a father's lullaby")]
        [ShopText("This melody blankets listeners in calm while making eyelids grow heavy.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x74), ItemPool(ItemCategory.Songs, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        SongLullaby,

        [Repeatable]
        [StartingItem(0xC5CE72, 0x01)]
        [ItemName("New Wave Bossa Nova"), LocationName("Baby Zoras"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("the lost children", "the pirates' loot"), GossipItemHint("an ocean roar", "a song of newborns"), GossipCompetitiveHint(2, nameof(GameplaySettings.AddSongs), true)]
        [ShopText("It's the melody taught by the Zora children that invigorates singing voices.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x75), ItemPool(ItemCategory.Songs, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        SongNewWaveBossaNova,

        [Repeatable]
        [StartingItem(0xC5CE72, 0x02)]
        [ItemName("Elegy of Emptiness"), LocationName("Ikana King"), Region(Region.IkanaCastle)]
        [GossipLocationHint("a fallen king", "a battle in darkness"), GossipItemHint("empty shells", "skin shedding")]
        [ShopText("It's a mystical song that allows you to shed a shell shaped in your current image.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1CB), ItemPool(ItemCategory.Songs, LocationCategory.BossFights, ClassicCategory.BaseItemPool)] // 0x76 is a special value used for ice traps in chests
        SongElegy,

        [Repeatable]
        [Visible]
        [StartingItem(0xC5CE72, 0x04)]
        [ItemName("Oath to Order"), LocationName("Boss Blue Warp"), MultiLocation(SongOathInWFT, SongOathInSHT, SongOathInGBT, SongOathInISTT)]
        [GossipLocationHint("cleansed evil", "a fallen evil"), GossipItemHint("a song of summoning", "a song of giants")]
        [ShopText("This melody will call the giants at the right moment.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x77), ItemPool(ItemCategory.Songs, LocationCategory.BossFights, ClassicCategory.BaseItemPool)]
        SongOath,

        //areas/other
        AreaSouthAccess,

        [EntranceName("Woodfall")]
        [DungeonEntrance(DungeonEntrance.EntranceWoodfallTempleFromWoodfall, DungeonEntrance.EntranceWoodfallFromWoodfallTempleEntrance)]
        AreaWoodFallTempleAccess,

        [DungeonEntrance(DungeonEntrance.EntranceWoodfallTemplePrisonFromOdolwasLair)]
        AreaWoodFallTempleClear,
        AreaNorthAccess,

        [EntranceName("Snowhead")]
        [DungeonEntrance(DungeonEntrance.EntranceSnowheadTempleFromSnowhead, DungeonEntrance.EntranceSnowheadFromSnowheadTemple)]
        AreaSnowheadTempleAccess,

        [DungeonEntrance(DungeonEntrance.EntranceMountainVillageFromSnowheadClear)]
        AreaSnowheadTempleClear,
        OtherEpona,
        AreaWestAccess,
        AreaPiratesFortressAccess,

        [EntranceName("Great Bay")]
        [DungeonEntrance(DungeonEntrance.EntranceGreatBayTempleFromZoraCape, DungeonEntrance.EntranceZoraCapeFromGreatBayTemple)]
        AreaGreatBayTempleAccess,

        [DungeonEntrance(DungeonEntrance.EntranceZoraCapeFromGreatBayTempleClear)]
        AreaGreatBayTempleClear,
        AreaEastAccess,
        AreaIkanaCanyonAccess,
        AreaStoneTowerTempleAccess,

        [EntranceName("Inverted Stone Tower")]
        [DungeonEntrance(DungeonEntrance.EntranceStoneTowerTempleInvertedFromStoneTowerInverted, DungeonEntrance.EntranceStoneTowerInvertedFromStoneTowerTempleInverted)]
        AreaInvertedStoneTowerTempleAccess,

        [DungeonEntrance(DungeonEntrance.EntranceIkanaCanyonFromIkanaClear)]
        AreaStoneTowerClear,
        OtherExplosive,
        OtherArrow,
        OtherMagicBean,
        OtherLimitlessBeans,
        OtherPlayDekuPlayground,
        OtherTimeTravel,
        OtherInaccessible,

        [EntranceName("Odolwa's Lair"), Region(Region.WoodfallTemple)]
        [DungeonEntrance(DungeonEntrance.EntranceOdolwasLairFromWoodfallTemple)]
        AreaOdolwasLair,

        [EntranceName("Goht's Lair"), Region(Region.SnowheadTemple)]
        [DungeonEntrance(DungeonEntrance.EntranceGohtsLairFromSnowheadTemple)]
        AreaGohtsLair,

        [EntranceName("Gyorg's Lair"), Region(Region.GreatBayTemple)]
        [DungeonEntrance(DungeonEntrance.EntranceGyorgsLairFromGreatBayTemple)]
        AreaGyorgsLair,

        [EntranceName("Twinmold's Lair"), Region(Region.StoneTowerTemple)]
        [DungeonEntrance(DungeonEntrance.EntranceTwinmoldsLairFromStoneTowerTempleInverted)]
        AreaTwinmoldsLair,

        OtherKillOdolwa,
        OtherKillGoht,
        OtherKillGyorg,
        OtherKillTwinmold,

        //keysanity items
        [Repeatable]
        [StartingItem(0xC5CE74, 0x04)]
        [ItemName("Woodfall Map"), LocationName("Woodfall Map Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Woodfall Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x0221F000 + 0x12A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x3E), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemWoodfallMap,

        [Repeatable]
        [StartingItem(0xC5CE74, 0x02)]
        [ItemName("Woodfall Compass"), LocationName("Woodfall Compass Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Woodfall Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02215000 + 0xFA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x3F), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemWoodfallCompass,

        [Repeatable, Temporary(nameof(GameplaySettings.BossKeyMode), (int)BossKeyMode.KeepThroughTime, false)]
        [ItemName("Woodfall Boss Key"), LocationName("Woodfall Boss Key Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Woodfall Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02227000 + 0x11A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x3D), ItemPool(ItemCategory.DungeonKeys, LocationCategory.BossFights, ClassicCategory.DungeonItems)]
        ItemWoodfallBossKey,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Woodfall Small Key"), LocationName("Woodfall Small Key Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Woodfall Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02218000 + 0x1CA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x3C), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemWoodfallKey1,

        [Repeatable]
        [StartingItem(0xC5CE75, 0x04)]
        [ItemName("Snowhead Map"), LocationName("Snowhead Map Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Snowhead Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02346000 + 0x13A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x54), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemSnowheadMap,

        [Repeatable]
        [StartingItem(0xC5CE75, 0x02)]
        [ItemName("Snowhead Compass"), LocationName("Snowhead Compass Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Snowhead Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x022F2000 + 0x1BA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x57), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemSnowheadCompass,

        [Repeatable, Temporary(nameof(GameplaySettings.BossKeyMode), (int)BossKeyMode.KeepThroughTime, false)]
        [ItemName("Snowhead Boss Key"), LocationName("Snowhead Boss Key Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Snowhead Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x0230C000 + 0x57A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x4E), ItemPool(ItemCategory.DungeonKeys, LocationCategory.BossFights, ClassicCategory.DungeonItems)]
        ItemSnowheadBossKey,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Block Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02306000 + 0x12A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x46), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemSnowheadKey1,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Icicle Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0233A000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x47), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemSnowheadKey2,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Bridge Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x022F9000 + 0x19A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x48), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemSnowheadKey3,

        [Repeatable]
        [StartingItem(0xC5CE76, 0x04)]
        [ItemName("Great Bay Map"), LocationName("Great Bay Map Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Great Bay Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02905000 + 0x19A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x55), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemGreatBayMap,

        [Repeatable]
        [StartingItem(0xC5CE76, 0x02)]
        [ItemName("Great Bay Compass"), LocationName("Great Bay Compass Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Great Bay Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02914000 + 0x21A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x58), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemGreatBayCompass,

        [Repeatable, Temporary(nameof(GameplaySettings.BossKeyMode), (int)BossKeyMode.KeepThroughTime, false)]
        [ItemName("Great Bay Boss Key"), LocationName("Great Bay Boss Key Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Great Bay Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02914000 + 0x1FA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x4F), ItemPool(ItemCategory.DungeonKeys, LocationCategory.BossFights, ClassicCategory.DungeonItems)]
        ItemGreatBayBossKey,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Great Bay Small Key"), LocationName("Great Bay Small Key Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Great Bay Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02914000 + 0x20A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x40), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemGreatBayKey1,

        [Repeatable]
        [StartingItem(0xC5CE77, 0x04)]
        [ItemName("Stone Tower Map"), LocationName("Stone Tower Map Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Stone Tower Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x0210F000 + 0x222, ChestAttribute.AppearanceType.Normal, 0x02182000 + 0x21E)]
        [GetItemIndex(0x56), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerMap,

        [Repeatable]
        [StartingItem(0xC5CE77, 0x02)]
        [ItemName("Stone Tower Compass"), LocationName("Stone Tower Compass Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Stone Tower Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02104000 + 0x292, ChestAttribute.AppearanceType.Normal, 0x02177000 + 0x2DE)]
        [GetItemIndex(0x6C), ItemPool(ItemCategory.Navigation, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerCompass,

        [Repeatable, Temporary(nameof(GameplaySettings.BossKeyMode), (int)BossKeyMode.KeepThroughTime, false)]
        [ItemName("Stone Tower Boss Key"), LocationName("Stone Tower Boss Key Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Stone Tower Temple.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02130000 + 0x82, ChestAttribute.AppearanceType.Normal, 0x02198000 + 0xCE)]
        [GetItemIndex(0x53), ItemPool(ItemCategory.DungeonKeys, LocationCategory.BossFights, ClassicCategory.DungeonItems)]
        ItemStoneTowerBossKey,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Armos Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x202, ChestAttribute.AppearanceType.AppearsSwitch, 0x02182000 + 0x1FE)]
        [GetItemIndex(0x49), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerKey1,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Eyegore Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1D2, ChestAttribute.AppearanceType.Normal, 0x02164000 + 0x1AE)]
        [GetItemIndex(0x4A), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerKey2,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Updraft Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x282, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2CE)]
        [GetItemIndex(0x4B), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerKey3,

        [Repeatable, Temporary(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, false), Returnable(nameof(GameplaySettings.SmallKeyMode), (int)SmallKeyMode.KeepThroughTime, true)]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Death Armos Maze Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020FC000 + 0x252, ChestAttribute.AppearanceType.Normal, 0x0216E000 + 0x1CE)]
        [GetItemIndex(0x4D), ItemPool(ItemCategory.DungeonKeys, LocationCategory.Chests, ClassicCategory.DungeonItems)]
        ItemStoneTowerKey4,

        //shop items
        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Red Potion"), LocationName("Trading Post Red Potion"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 7)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 7)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xCD), ItemPool(ItemCategory.RedPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostRedPotion,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Green Potion"), LocationName("Trading Post Green Potion"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a magic potion", "a green drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 2)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 3)]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xBB), ItemPool(ItemCategory.GreenPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostGreenPotion,

        [Purchaseable, Visible]
        [Repeatable]
        [Overwritable(OverwritableAttribute.ItemSlot.Shield, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Hero's Shield"), LocationName("Trading Post Hero's Shield"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a basic guard", "protection")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 3)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 6)]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0xBC), ItemPool(ItemCategory.Shields, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostShield,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Fairy"), LocationName("Trading Post Fairy"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a winged friend", "a healer")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 0)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 0)]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xBD), ItemPool(ItemCategory.Fairy, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostFairy,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("Deku Stick"), LocationName("Trading Post Deku Stick"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 4)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 5)]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xBE), ItemPool(ItemCategory.DekuSticks, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostStick,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("30 Arrows"), LocationName("Trading Post 30 Arrows"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 5)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xBF), ItemPool(ItemCategory.Arrows, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostArrow30,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Trading Post 10 Deku Nuts"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a flashing impact")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 6)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 4)]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC0), ItemPool(ItemCategory.DekuNuts, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostNut10,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("50 Arrows"), LocationName("Trading Post 50 Arrows"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 2)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC1), ItemPool(ItemCategory.Arrows, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemTradingPostArrow50,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Blue Potion"), LocationName("Witch Shop Blue Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("consumable strength", "a magic potion", "a blue drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 2)]
        [ShopText("Replenishes both life energy and magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_witch_bluepotion))]
        [GetItemIndex(0xC2), ItemPool(ItemCategory.BluePotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemWitchBluePotion,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Red Potion"), LocationName("Witch Shop Red Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 0)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC3), ItemPool(ItemCategory.RedPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemWitchRedPotion,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Green Potion"), LocationName("Witch Shop Green Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("a magic potion", "a green drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 1)]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC4), ItemPool(ItemCategory.GreenPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemWitchGreenPotion,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("10 Bombs"), LocationName("Bomb Shop 10 Bombs"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("explosives")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 3)]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC5), ItemPool(ItemCategory.Bombs, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemBombsBomb10,

        [Purchaseable, Visible]
        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("10 Bombchu"), LocationName("Bomb Shop 10 Bombchu"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("explosives")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 2)]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC6), ItemPool(ItemCategory.Bombchu, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemBombsBombchu10,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("10 Bombs"), LocationName("Goron Shop 10 Bombs"), MultiLocation(ShopItemGoronBomb10InWinter, ShopItemGoronBomb10InSpring), RegionArea(RegionArea.Mountain)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("explosives")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 0)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 0)]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC7), ItemPool(ItemCategory.Bombs, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemGoronBomb10,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("10 Arrows"), LocationName("Goron Shop 10 Arrows"), MultiLocation(ShopItemGoronArrow10InWinter, ShopItemGoronArrow10InSpring), RegionArea(RegionArea.Mountain)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC8), ItemPool(ItemCategory.Arrows, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemGoronArrow10,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Red Potion"), LocationName("Goron Shop Red Potion"), MultiLocation(ShopItemGoronRedPotionInWinter, ShopItemGoronRedPotionInSpring), RegionArea(RegionArea.Mountain)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 2)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 2)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC9), ItemPool(ItemCategory.RedPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemGoronRedPotion,

        [Purchaseable, Visible]
        [Repeatable]
        [Overwritable(OverwritableAttribute.ItemSlot.Shield, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Hero's Shield"), LocationName("Zora Shop Hero's Shield"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("a basic guard", "protection")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 0)]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0xCA), ItemPool(ItemCategory.Shields, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemZoraShield,

        [Purchaseable, Visible]
        [Repeatable, Temporary]
        [ItemName("10 Arrows"), LocationName("Zora Shop 10 Arrows"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xCB), ItemPool(ItemCategory.Arrows, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemZoraArrow10,

        [Purchaseable, Visible]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Red Potion"), LocationName("Zora Shop Red Potion"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 2)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xCC), ItemPool(ItemCategory.RedPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemZoraRedPotion,

        //bottle catch
        [ItemName("Bottle: Fairy"), LocationName("Bottle: Fairy"), Region(Region.BottleCatch)]
        [GossipLocationHint("a wandering healer"), GossipItemHint("a winged friend", "a healer")]
        [GetBottleItemIndices(0x00, 0x0D), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchFairy,

        [ItemName("Bottle: Deku Princess"), LocationName("Bottle: Deku Princess"), Region(Region.BottleCatch)]
        [GossipLocationHint("a captured royal", "an imprisoned daughter"), GossipItemHint("a princess", "a woodland royal")]
        [GetBottleItemIndices(0x08), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchPrincess,

        [ItemName("Bottle: Fish"), LocationName("Bottle: Fish"), Region(Region.BottleCatch)]
        [GossipLocationHint("a swimming creature", "a water dweller"), GossipItemHint("something fresh")]
        [GetBottleItemIndices(0x01), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchFish,

        [ItemName("Bottle: Bug"), LocationName("Bottle: Bug"), Region(Region.BottleCatch)]
        [GossipLocationHint("an insect", "a scuttling creature"), GossipItemHint("an insect", "a scuttling creature")]
        [GetBottleItemIndices(0x02, 0x03), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchBug,

        [ItemName("Bottle: Poe"), LocationName("Bottle: Poe"), Region(Region.BottleCatch)]
        [GossipLocationHint("a wandering ghost"), GossipItemHint("a captured spirit")]
        [GetBottleItemIndices(0x0B), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchPoe,

        [ItemName("Bottle: Big Poe"), LocationName("Bottle: Big Poe"), Region(Region.BottleCatch)]
        [GossipLocationHint("a huge ghost"), GossipItemHint("a captured spirit")]
        [GetBottleItemIndices(0x0C), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchBigPoe,

        [ItemName("Bottle: Spring Water"), LocationName("Bottle: Spring Water"), Region(Region.BottleCatch)]
        [GossipLocationHint("a common liquid"), GossipItemHint("a common liquid", "a fresh drink")]
        [GetBottleItemIndices(0x04), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchSpringWater,

        [ItemName("Bottle: Hot Spring Water"), LocationName("Bottle: Hot Spring Water"), Region(Region.BottleCatch)]
        [GossipLocationHint("a hot liquid", "a boiling liquid"), GossipItemHint("a boiling liquid", "a hot liquid")]
        [GetBottleItemIndices(0x05, 0x06), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchHotSpringWater,

        [ItemName("Bottle: Zora Egg"), LocationName("Bottle: Zora Egg"), Region(Region.BottleCatch)]
        [GossipLocationHint("a lost child"), GossipItemHint("a lost child")]
        [GetBottleItemIndices(0x07), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchEgg,

        [ItemName("Bottle: Mushroom"), LocationName("Bottle: Mushroom"), Region(Region.BottleCatch)]
        [GossipLocationHint("a strange fungus"), GossipItemHint("a strange fungus")]
        [GetBottleItemIndices(0x0A), ItemPool(ItemCategory.ScoopedItems, LocationCategory.Scoops, ClassicCategory.CaughtBottleContents)]
        BottleCatchMushroom,

        //other chests and grottos
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Lens Cave Invisible Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EB8000 + 0xAA, ChestAttribute.AppearanceType.Invisible)]
        [GetItemIndex(0xDD), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestLensCaveRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Lens Cave Rock Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EB8000 + 0xDA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF4), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestLensCavePurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Bean Grotto"), Region(Region.DekuPalace)]
        [GossipLocationHint("a merchant's cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ECC000 + 0xFA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xDE), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestBeanGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Hot Spring Water Grotto"), Region(Region.TwinIslands)]
        [GossipLocationHint("a steaming cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ED7000 + 0xC6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xDF), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestHotSpringGrottoRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Day 1 Grave Bats"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a cloud of bats"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x01F97000 + 0xCE, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xF5), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestBadBatsGrottoPurpleRupee,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("5 Bombchu"), LocationName("Secret Shrine Grotto"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a waterfall cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02080000 + 0x93, 0x02080000 + 0x1E3, 0x02080000 + 0x2EB)]
        [GetItemIndex(0xD1), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestIkanaSecretShrineGrotto,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Lower Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x020A2000 + 0x256, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE0), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPiratesFortressRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Upper Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x020A2000 + 0x266, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE1), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPiratesFortressRedRupee2,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Tank Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023B7000 + 0x66, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE2), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInsidePiratesFortressTankRedRupee,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Pirates' Fortress Interior Guard Room Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023BB000 + 0x56, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFB), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInsidePiratesFortressGuardSilverRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Cage Room Shallow Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023E6000 + 0x24E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE3), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInsidePiratesFortressHeartPieceRoomRedRupee,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Cage Room Deep Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023E6000 + 0x25E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x105), ItemPool(ItemCategory.BlueRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInsidePiratesFortressHeartPieceRoomBlueRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Maze Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023F0000 + 0xDE, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE4), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInsidePiratesFortressMazeRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pinnacle Rock Lower Chest"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a marine trench"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02428000 + 0x24E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE5), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPinacleRockRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pinnacle Rock Upper Chest"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a marine trench"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02428000 + 0x25E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE6), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPinacleRockRedRupee2,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Bombers' Hideout Chest"), Region(Region.EastClockTown)]
        [GossipLocationHint("a secret hideout"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x024F1000 + 0x1DE, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFC), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestBomberHideoutSilverRupee,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("Bombchu"), LocationName("Termina Field Pillar Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow pillar"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x025C5000 + 0x583)]
        [GetItemIndex(0xD7), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestTerminaGrottoBombchu,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Grass Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a grassy cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x025C5000 + 0x593)]
        [GetItemIndex(0xDC), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestTerminaGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Underwater Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a sunken chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD52, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE7), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestTerminaUnderwaterRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Grass Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a grassy chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD62, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE8), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestTerminaGrassRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Stump Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a tree's chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD72, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE9), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestTerminaStumpRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Coast Grotto"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a beach cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x026DE000 + 0x43F, 0x026DE000 + 0xFE3)]
        [GetItemIndex(0xD4), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGreatBayCoastGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Zora Cape Ledge Without Tree Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a high place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x42A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB16)]
        [GetItemIndex(0xEA), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGreatBayCapeLedge1, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Zora Cape Ledge With Tree Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a high place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x43A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB26)]
        [GetItemIndex(0xEB), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGreatBayCapeLedge2, //contents? 

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("Bombchu"), LocationName("Zora Cape Grotto"), Region(Region.ZoraCape)]
        [GossipLocationHint("a beach cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02715000 + 0x45B, 0x02715000 + 0xB47)]
        [GetItemIndex(0xD2), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGreatBayCapeGrotto, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Zora Cape Underwater Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a sunken chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x48A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB56)]
        [GetItemIndex(0xF6), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGreatBayCapeUnderwater, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Log Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x196, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xEC), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPiratesFortressEntranceRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Sand Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x1A6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xED), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPiratesFortressEntranceRedRupee2,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Corner Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x1B6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xEE), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestPiratesFortressEntranceRedRupee3,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Swamp Grotto"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a southern cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x027C1000 + 0x33B)]
        [GetItemIndex(0xDB), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToSwampGrotto, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Doggy Racetrack Roof Chest"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a day at the races"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x027D4000 + 0xB6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF7), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestDogRacePurpleRupee,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("5 Bombchu"), LocationName("Ikana Graveyard Grotto"), Region(Region.IkanaGraveyard)]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [GossipLocationHint("a circled cave"), GossipItemHint("explosive mice")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x0280D000 + 0x353, 0x0280D000 + 0x54B)]
        [GetItemIndex(0xD5), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestGraveyardGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Near Swamp Spider House Grotto"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a southern cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x01F3A000 + 0x227, 0x02855000 + 0x2AF)]
        [GetItemIndex(0xDA), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestSwampGrotto,  //contents? 

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Behind Woodfall Owl Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("a swamp chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x232, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA62)]
        [GetItemIndex(0x106), ItemPool(ItemCategory.BlueRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestWoodfallBlueRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Entrance to Woodfall Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("a swamp chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x242, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA32)]
        [GetItemIndex(0xEF), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestWoodfallRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Well Right Path Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a frightful exchange"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x029EA000 + 0xE6, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0xF8), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestWellRightPurpleRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Well Left Path Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a frightful exchange"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x029F0000 + 0x106, ChestAttribute.AppearanceType.Invisible)]
        [GetItemIndex(0xF9), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestWellLeftPurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Waterfall Chest"), Region(Region.MountainVillage)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BDD000 + 0x2E2, ChestAttribute.AppearanceType.Invisible, 0x02BDD000 + 0x946)]
        [GetItemIndex(0xF0), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestMountainVillage, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Spring Grotto"), Region(Region.MountainVillage)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02BFC000 + 0x1F3, 0x02BFC000 + 0x2B3)]
        [GetItemIndex(0xD8), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestMountainVillageGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Ikana Pillar Chest"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a high chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02B34000 + 0x442, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF1), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToIkanaRedRupee,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("Bombchu"), LocationName("Path to Ikana Grotto"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a blocked cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02B34000 + 0x523)]
        [GetItemIndex(0xD3), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToIkanaGrotto, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inverted Stone Tower Right Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BC9000 + 0x236, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFD), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInvertedStoneTowerSilverRupee,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("10 Bombchu"), LocationName("Inverted Stone Tower Middle Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BC9000 + 0x246, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x10A), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInvertedStoneTowerBombchu10,

        [Repeatable, Temporary]
        [StartingItemId(0x0A)]
        [ItemName("Magic Bean"), LocationName("Inverted Stone Tower Left Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("a plant seed")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02BC9000 + 0x256, ChestAttribute.AppearanceType.Normal)]
        [ShopText("Plant it in soft soil.")]
        [GetItemIndex(0x109), ItemPool(ItemCategory.MainInventory, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInvertedStoneTowerBean,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Snowhead Grotto"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a snowy cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02C04000 + 0xAF, 0x02C04000 + 0x487)]
        [GetItemIndex(0xD0), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToSnowheadGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Twin Islands Cave Chest"), Region(Region.TwinIslands)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C34000 + 0x13A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF2), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToGoronVillageRedRupee,

        // TODO rename to match HeartPiece item enum naming style (HeartPieceSecretShrineChest ?)
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Secret Shrine Final Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C57000 + 0xB6, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x107), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        ChestSecretShrineHeartPiece,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Dinolfos Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C61000 + 0x9A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xFE), ItemPool(ItemCategory.SilverRupees, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        ChestSecretShrineDinoGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Wizzrobe Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C69000 + 0xB2, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xFF), ItemPool(ItemCategory.SilverRupees, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        ChestSecretShrineWizzGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Wart Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C71000 + 0xA6, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x100), ItemPool(ItemCategory.SilverRupees, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        ChestSecretShrineWartGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Garo Master Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C75000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x101), ItemPool(ItemCategory.SilverRupees, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        ChestSecretShrineGaroGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inn Staff Room Chest"), Region(Region.StockPotInn)]
        [GossipLocationHint("an employee room"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02CAB000 + 0x10E, ChestAttribute.AppearanceType.Normal, 0x02CAB000 + 0x242)]
        [GetItemIndex(0x102), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInnStaffRoom, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inn Guest Room Chest"), Region(Region.StockPotInn)]
        [GossipLocationHint("a guest bedroom"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02CB1000 + 0xDA, ChestAttribute.AppearanceType.Normal, 0x02CB1000 + 0x212)]
        [GetItemIndex(0x103), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestInnGuestRoom, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Mystery Woods Grotto"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a mystery cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02CFC000 + 0x5B)]
        [GetItemIndex(0xD9), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestWoodsGrotto, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("East Clock Town Chest"), Region(Region.EastClockTown)]
        [GossipLocationHint("a shop roof"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02DE4000 + 0x442, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x104), ItemPool(ItemCategory.SilverRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestEastClockTownSilverRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("South Clock Town Straw Roof Chest"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a straw roof"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02E5C000 + 0x342, ChestAttribute.AppearanceType.Normal, 0x02E5C000 + 0x806)]
        [GetItemIndex(0xF3), ItemPool(ItemCategory.RedRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestSouthClockTownRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("South Clock Town Final Day Chest"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a carnival tower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02E5C000 + 0x352, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFA), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestSouthClockTownPurpleRupee,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Bank Reward #3"), Region(Region.WestClockTown)]
        [GossipLocationHint("being rich"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x108), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.EverythingElse)]
        HeartPieceBank,

        //standing HPs
        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Clock Tower Entrance"), Region(Region.SouthClockTown)]
        [GossipLocationHint("the tower doors"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10B), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceSouthClockTown,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("North Clock Town Tree"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a town playground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10C), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceNorthClockTown,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Path to Swamp Tree"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a tree of bats"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10D), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceToSwamp,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swamp Tourist Center Roof"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a tourist centre"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10E), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceSwampScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Palace West Garden"), Region(Region.DekuPalace)]
        [GossipLocationHint("the home of scrubs"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10F), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceDekuPalace,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Goron Village Ledge"), Region(Region.GoronVillage)]
        [GossipLocationHint("a cold ledge"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x110), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceGoronVillageScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Bio Baba Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a beehive"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x111), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Beehives, ClassicCategory.EverythingElse)]
        HeartPieceZoraGrotto,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Lab Fish"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("feeding the fish"), GossipItemHint("a segment of health"), GossipCompetitiveHint(0, nameof(GameplaySettings.SpeedupLabFish), false)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x112), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.NpcRewards, ClassicCategory.EverythingElse)]
        HeartPieceLabFish,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Zora Cape Like-Like"), Region(Region.ZoraCape)]
        [GossipLocationHint("a shield eater"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x113), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.EnemySpawn, ClassicCategory.EverythingElse)]
        HeartPieceGreatBayCapeLikeLike,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Pirates' Fortress Cage"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("a timed door"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x114), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPiecePiratesFortress,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Lulu's Room Ledge"), Region(Region.ZoraHall)]
        [GossipLocationHint("the singer's room"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x115), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceZoraHallScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Path to Snowhead Pillar"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a cold platform"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x116), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceToSnowhead,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Great Bay Coast Ledge"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a rock face"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x117), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceGreatBayCoast,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ikana Canyon Ledge"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a thief's doorstep"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x118), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceIkana,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ikana Castle Pillar"), Region(Region.IkanaCastle)]
        [GossipLocationHint("a fiery pillar"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x119), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.Freestanding, ClassicCategory.EverythingElse)]
        HeartPieceCastle,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Woodfall Heart Container"), Region(AreaOdolwasLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11A), ItemPool(ItemCategory.HeartContainers, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        HeartContainerWoodfall,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Snowhead Heart Container"), Region(AreaGohtsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11B), ItemPool(ItemCategory.HeartContainers, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        HeartContainerSnowhead,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Great Bay Heart Container"), Region(AreaGyorgsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11C), ItemPool(ItemCategory.HeartContainers, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        HeartContainerGreatBay,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Stone Tower Heart Container"), Region(AreaTwinmoldsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11D), ItemPool(ItemCategory.HeartContainers, LocationCategory.BossFights, ClassicCategory.EverythingElse)]
        HeartContainerStoneTower,

        //maps
        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Town)]
        [ItemName("Map of Clock Town"), LocationName("Clock Town Map Purchase"), MultiLocation(ItemTingleMapTownInTown, ItemTingleMapTownInCanyon)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB4), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapTown,

        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Swamp)]
        [ItemName("Map of Woodfall"), LocationName("Woodfall Map Purchase"), MultiLocation(ItemTingleMapWoodfallInSwamp, ItemTingleMapWoodfallInTown)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the south.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB5), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapWoodfall,

        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Mountain)]
        [ItemName("Map of Snowhead"), LocationName("Snowhead Map Purchase"), MultiLocation(ItemTingleMapSnowheadInMountain, ItemTingleMapSnowheadInSwamp)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the north.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB6), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapSnowhead,

        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Ranch)]
        [ItemName("Map of Romani Ranch"), LocationName("Romani Ranch Map Purchase"), MultiLocation(ItemTingleMapRanchInRanch, ItemTingleMapRanchInMountain)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the ranch.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB7), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapRanch,

        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Ocean)]
        [ItemName("Map of Great Bay"), LocationName("Great Bay Map Purchase"), MultiLocation(ItemTingleMapGreatBayInOcean, ItemTingleMapGreatBayInRanch)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the west.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB8), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapGreatBay,

        [Repeatable]
        [Purchaseable]
        [StartingTingleMap(TingleMap.Canyon)]
        [ItemName("Map of Stone Tower"), LocationName("Stone Tower Map Purchase"), MultiLocation(ItemTingleMapStoneTowerInCanyon, ItemTingleMapStoneTowerInOcean)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the east.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB9), ItemPool(ItemCategory.Navigation, LocationCategory.Purchases, ClassicCategory.EverythingElse)]
        ItemTingleMapStoneTower,

        //oops I forgot one
        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("Bombchu"), LocationName("Goron Racetrack Grotto"), Region(Region.TwinIslands)]
        [GossipLocationHint("a hidden cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02C23000 + 0x2D7, 0x02C34000 + 0x1DB)]
        [GetItemIndex(0xD6), ItemPool(ItemCategory.Bombchu, LocationCategory.Chests, ClassicCategory.EverythingElse)]
        ChestToGoronRaceGrotto, //contents?

        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Canyon Scrub Trade"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("an eastern merchant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x125), ItemPool(ItemCategory.GoldRupees, LocationCategory.NpcRewards, ClassicCategory.EverythingElse)]
        IkanaScrubGoldRupee,

        //moon items
        OtherOneMask,
        OtherTwoMasks,
        OtherThreeMasks,
        OtherFourMasks,
        AreaMoonAccess,
        OtherKillMajora,
        OtherCredits,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x11F), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        HeartPieceDekuTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Goron Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x120), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        HeartPieceGoronTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Zora Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x121), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        HeartPieceZoraTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Link Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x122), ItemPool(ItemCategory.PiecesOfHeart, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        HeartPieceLinkTrial,

        [Repeatable]
        [StartingItem(0xC5CE53, 0x35)]
        [ItemName("Fierce Deity's Mask"), LocationName("Majora Child"), Region(Region.TheMoon)]
        [GossipLocationHint("the lonely child"), GossipItemHint("the wrath of a god")]
        [ShopText("A mask that contains the merits of all masks.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [HackContent(nameof(Resources.mods.fix_fd_mask_reset))]
        [GetItemIndex(0x7B), ItemPool(ItemCategory.Masks, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        MaskFierceDeity,

        [Repeatable, Temporary]
        [ItemName("30 Arrows"), LocationName("Link Trial Garo Master Chest"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02D4B000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x126), ItemPool(ItemCategory.Arrows, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        ChestLinkTrialArrow30,

        [Repeatable, Temporary(nameof(GameplaySettings.BombchuDrops), false)]
        [ItemName("10 Bombchu"), LocationName("Link Trial Iron Knuckle Chest"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02D4E000 + 0xC6, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x127), ItemPool(ItemCategory.Bombchu, LocationCategory.MoonItems, ClassicCategory.MoonItems)]
        ChestLinkTrialBombchu10,

        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Pre-Clocktown Chest"), Region(Region.BeneathClocktown)]
        [GossipLocationHint("the first chest"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x021D2000 + 0x102, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x128), ItemPool(ItemCategory.DekuNuts, LocationCategory.GlitchesRequired, ClassicCategory.GlitchesRequired)]
        ChestPreClocktownDekuNut,

        [Repeatable]
        [Progressive]
        [StartingItem(0xC5CE21, 0x01)]
        [StartingItem(0xC5CE00, 0x4D)]
        [Overwritable(OverwritableAttribute.ItemSlot.Sword, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Kokiri Sword"), LocationName("Starting Sword"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a forest blade")]
        [ShopText("A sword created by forest folk.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [HackContent(nameof(Resources.mods.fix_sword_song_of_time))]
        [GetItemIndex(0x37), ItemPool(ItemCategory.MainInventory, LocationCategory.StartingItems, ClassicCategory.CrazyStartingItems)]
        StartingSword,

        [Repeatable]
        [StartingItem(0xC5CE21, 0x10)]
        [Overwritable(OverwritableAttribute.ItemSlot.Shield, nameof(GameplaySettings.PreventDowngrades), false)]
        [ItemName("Hero's Shield"), LocationName("Starting Shield"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a basic guard", "protection")]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x129), ItemPool(ItemCategory.Shields, LocationCategory.StartingItems, ClassicCategory.CrazyStartingItems)]
        StartingShield,

        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Starting Heart Container #1"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12A), ItemPool(ItemCategory.HeartContainers, LocationCategory.StartingItems, ClassicCategory.CrazyStartingItems)]
        StartingHeartContainer1,

        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Starting Heart Container #2"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12B), ItemPool(ItemCategory.HeartContainers, LocationCategory.StartingItems, ClassicCategory.CrazyStartingItems)]
        StartingHeartContainer2,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Ranch Cow #1"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x132), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemRanchBarnMainCowMilk,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Ranch Cow #2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x182), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemRanchBarnOtherCowMilk1,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Ranch Cow #3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A2), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemRanchBarnOtherCowMilk2,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Cow Beneath the Well"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x135), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemWellCowMilk,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Termina Grotto Cow #1"), Region(Region.TerminaField)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x136), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemTerminaGrottoCowMilk1,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Termina Grotto Cow #2"), Region(Region.TerminaField)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x137), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemTerminaGrottoCowMilk2,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Great Bay Coast Grotto Cow #1"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x138), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemCoastGrottoCowMilk1,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Milk"), LocationName("Great Bay Coast Grotto Cow #2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x139), ItemPool(ItemCategory.Milk, LocationCategory.NpcRewards, ClassicCategory.CowMilk)]
        ItemCoastGrottoCowMilk2,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Near Ceiling"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13A), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken1,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Near Ceiling"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13B), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken2,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Torch"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13C), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken3,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Pillar"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13E), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken4,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Jar"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13F), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Jars, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken5,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Grass 1"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x140), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken6,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Grass 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x141), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken7,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Water"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x142), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken8,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Lower Left Soft Soil"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x143), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.SoftSoil, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken9,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Crate 1"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x144), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Crates, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken10,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Upper Soft Soil"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x145), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.SoftSoil, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken11,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Lower Right Soft Soil"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x146), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.SoftSoil, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken12,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Lower Wall"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x147), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken13,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room On Monument"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x148), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken14,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Pillar"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x149), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken15,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Pot 1"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14A), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken16,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Pot 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14B), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken17,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Hive"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14C), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Beehives, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken18,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Upper Pillar"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14D), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken19,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Behind Vines"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14E), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken20,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 1"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14F), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken21,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Wall"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x150), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken22,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Hive 1"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x151), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Beehives, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken23,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x152), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken24,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Wall"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x153), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken25,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Hive"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x154), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Beehives, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken26,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Crate 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x155), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Crates, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken27,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Hive 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x156), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Beehives, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken28,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 3"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x157), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken29,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Jar"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x158), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Jars, ClassicCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken30,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Behind Boat"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x159), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken1,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Hole Behind Picture"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15A), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken2,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Hole Behind Cabinet"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15B), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken3,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library On Corner Bookshelf"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15C), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken4,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Ceiling Edge"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15D), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken5,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Ceiling Plank"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15E), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken6,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Ceiling Edge"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15F), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken7,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Ceiling Edge"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x160), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken8,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Ceiling Web"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x161), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken9,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Behind Crate"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x162), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken10,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Jar"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x163), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Jars, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken11,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Right Wall"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x164), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken12,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Left Wall"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x165), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken13,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Webbed Hole"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x166), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken14,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Web"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x167), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken15,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 1"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x168), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken16,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x169), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken17,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 3"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16A), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken18,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Behind Picture"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16B), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken19,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Picture"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16C), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken20,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Bookcase 1"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16D), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken21,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Crate"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16E), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Crates, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken22,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Webbed Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16F), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken23,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Upper Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x170), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken24,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x171), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken25,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Jar"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x172), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.Jars, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken26,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Lower Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x173), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken27,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Bookcase 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x174), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken28,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Behind Skull 1"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x175), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken29,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Behind Skull 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x176), ItemPool(ItemCategory.SkulltulaTokens, LocationCategory.EnemySpawn, ClassicCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken30,

        [Visible]
        [ItemName("Clock Town Stray Fairy"), LocationName("Clock Town Stray Fairy"), MultiLocation(CollectibleStrayFairyClockTownInLaundryPool, CollectibleStrayFairyClockTownInECT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Return it to the Fairy Fountain in North Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_town_fairy_text))]
        [GetItemIndex(0x3B), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        //[GetItemIndex(0x1A1)] // used as a flag to track if the actual fairy has been collected.
        CollectibleStrayFairyClockTown,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Lower Right Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x177), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall1,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Entrance Fairy"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x178), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall2,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Upper Left Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x179), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall3,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Pillar Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17A), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall4,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Deku Baba"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17B), ItemPool(ItemCategory.StrayFairies, LocationCategory.EnemySpawn, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall5,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Poison Water Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17C), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall6,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Main Room Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17D), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall7,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Skulltula"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17E), ItemPool(ItemCategory.StrayFairies, LocationCategory.EnemySpawn, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall8,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Upper Right Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17F), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall9,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Main Room Switch"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x021FB000 + 0x28A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x184), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall10,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Entrance Platform"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02204000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x185), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall11,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Dark Room"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0222E000 + 0x1AA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x186), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall12,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Jar Fairy"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x189), ItemPool(ItemCategory.StrayFairies, LocationCategory.Jars, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall13,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Bridge Room Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18A), ItemPool(ItemCategory.StrayFairies, LocationCategory.Beehives, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall14,

        [Visible]
        [StartingItem(0xC5CE88, 0x01, true)]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Platform Room Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18B), ItemPool(ItemCategory.StrayFairies, LocationCategory.Beehives, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall15,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Snow Room Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18C), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead1,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Ceiling Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18D), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead2,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Dinolfos 1"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18E), ItemPool(ItemCategory.StrayFairies, LocationCategory.EnemySpawn, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead3,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Bridge Room Ledge Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x190), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead4,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Bridge Room Pillar Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x191), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead5,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Dinolfos 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x192), ItemPool(ItemCategory.StrayFairies, LocationCategory.EnemySpawn, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead6,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Map Room Fairy"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x193), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead7,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Map Room Ledge"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02346000 + 0x12A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x194), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead8,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Basement"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0230C000 + 0x56A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x195), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead9,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Twin Block"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02306000 + 0x11A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x196), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead10,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Icicle Room Wall"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0233A000 + 0x22A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x197), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead11,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Main Room Wall"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0230C000 + 0x58A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x198), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead12,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Pillar Freezards"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0232E000 + 0x20A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x199), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead13,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Ice Puzzle"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x022F2000 + 0x1AA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x19A), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead14,

        [Visible]
        [StartingItem(0xC5CE89, 0x01, true)]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Crate"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x19F), ItemPool(ItemCategory.StrayFairies, LocationCategory.Crates, ClassicCategory.StrayFairies)]
        CollectibleStrayFairySnowhead15,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Skulltula"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A7), ItemPool(ItemCategory.StrayFairies, LocationCategory.EnemySpawn, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay1,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Pre-Boss Room Underwater Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A4), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay2,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Water Control Room Underwater Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A5), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay3,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Pre-Boss Room Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A6), ItemPool(ItemCategory.StrayFairies, LocationCategory.Freestanding, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay4,

        // A8 empty

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Waterwheel Room Upper"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02940000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1A9), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay5,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Green Valve"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02959000 + 0x18E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AA), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay6,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Seesaw Room"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02945000 + 0x24A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AB), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay7,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Waterwheel Room Lower"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02940000 + 0x24A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AC), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay8,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Entrance Torches"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02962000 + 0x1F2, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1AD), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay9,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Bio Babas"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02911000 + 0xDA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x1AE), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay10,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Underwater Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1AF), ItemPool(ItemCategory.StrayFairies, LocationCategory.Barrels, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay11,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Whirlpool Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B0), ItemPool(ItemCategory.StrayFairies, LocationCategory.Jars, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay12,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Whirlpool Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B1), ItemPool(ItemCategory.StrayFairies, LocationCategory.Barrels, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay13,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Dexihands Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B2), ItemPool(ItemCategory.StrayFairies, LocationCategory.Jars, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay14,

        [Visible]
        [StartingItem(0xC5CE8A, 0x01, true)]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Ledge Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B3), ItemPool(ItemCategory.StrayFairies, LocationCategory.Jars, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay15,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Mirror Sun Block"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02119000 + 0x282, ChestAttribute.AppearanceType.Normal, 0x0218B000 + 0x8A)]
        [GetItemIndex(0x1B4), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower1,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Eyegore"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1A2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x17E)]
        [GetItemIndex(0x1B5), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower2,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Lava Room Fire Ring"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02122000 + 0x1F6, ChestAttribute.AppearanceType.Normal, 0x02191000 + 0x7A)]
        [GetItemIndex(0x1B6), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower3,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Updraft Fire Ring"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x252, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x29E)]
        [GetItemIndex(0x1B7), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower4,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Mirror Sun Switch"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02119000 + 0x272, ChestAttribute.AppearanceType.AppearsSwitch, 0x0218B000 + 0x7A)]
        [GetItemIndex(0x1B8), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower5,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Entrance Sun Switch"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x162, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0xFA)]
        [GetItemIndex(0x1B9), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower6,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Wizzrobe"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x1F2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02182000 + 0x1EE)]
        [GetItemIndex(0x1BA), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower7,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Death Armos"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x172, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0x10A)]
        [GetItemIndex(0x1BB), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower8,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Updraft Frozen Eye"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x262, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2AE)]
        [GetItemIndex(0x1BC), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower9,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Thin Bridge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0211D000 + 0x1E2, ChestAttribute.AppearanceType.AppearsSwitch, 0x0218C000 + 0x25E)]
        [GetItemIndex(0x1BD), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower10,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Basement Ledge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x212, ChestAttribute.AppearanceType.Normal, 0x02182000 + 0x20E)]
        [GetItemIndex(0x1BE), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower11,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Statue Eye"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x182, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0x11A)]
        [GetItemIndex(0x1BF), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower12,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Underwater"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x272, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2BE)]
        [GetItemIndex(0x1C0), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower13,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Bridge Crystal"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1B2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x18E)]
        [GetItemIndex(0x1C1), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower14,

        [Visible]
        [StartingItem(0xC5CE8B, 0x01, true)]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Lava Room Ledge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02122000 + 0x206, ChestAttribute.AppearanceType.Normal, 0x02191000 + 0x8A)]
        [GetItemIndex(0x1C2), ItemPool(ItemCategory.StrayFairies, LocationCategory.Chests, ClassicCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower15,

        [Purchaseable]
        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Lottery"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x86), ItemPool(ItemCategory.PurpleRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemLotteryPurpleRupee,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bank Reward #2"), Region(Region.WestClockTown)]
        [GossipLocationHint("interest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x13D), ItemPool(ItemCategory.BlueRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemBankBlueRupee,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle), Purchaseable]
        [ItemName("Chateau Romani"), LocationName("Milk Bar Chateau"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("a dairy product", "an adult beverage")]
        [ShopText("Drink it to get lasting stamina for your magic power.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_milkbar))]
        [GetItemIndex(0x180), ItemPool(ItemCategory.Chateau, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemMilkBarChateau,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle), Purchaseable]
        [ItemName("Milk"), LocationName("Milk Bar Milk"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_milkbar))]
        [GetItemIndex(0x181), ItemPool(ItemCategory.Milk, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemMilkBarMilk,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Deku Playground Any Day"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(-3)]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x133), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Minigames, ClassicCategory.MundaneRewards)]
        MundaneItemDekuPlaygroundPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Honey and Darling Any Day"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(-3)]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x183), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Minigames, ClassicCategory.MundaneRewards)]
        MundaneItemHoneyAndDarlingPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Kotake Mushroom Sale"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x187), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemKotakeMushroomSaleRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pictograph Contest Standard Photo"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x188), ItemPool(ItemCategory.BlueRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemPictographContestBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pictograph Contest Good Photo"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x18F), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemPictographContestRedRupee,

        [Repeatable, Temporary, Purchaseable]
        [StartingItemId(0x0A)]
        [ItemName("Magic Bean"), LocationName("Swamp Scrub Purchase"), MultiLocation(ShopItemBusinessScrubMagicBeanInSwamp, ShopItemBusinessScrubMagicBeanInTown)]
        [GossipLocationHint("a southern merchant"), GossipItemHint("a plant seed")]
        [ShopText("Plant it in soft soil.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_businessscrub_magicbean), false)]
        [GetItemIndex(0x19B), ItemPool(ItemCategory.MainInventory, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemBusinessScrubMagicBean,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle), Purchaseable]
        [ItemName("Green Potion"), LocationName("Ocean Scrub Purchase"), MultiLocation(ShopItemBusinessScrubGreenPotionInOcean, ShopItemBusinessScrubGreenPotionInMountain)]
        [GossipLocationHint("a western merchant"), GossipItemHint("a magic potion", "a green drink")]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_businessscrub_greenpotion))]
        [GetItemIndex(0x19C), ItemPool(ItemCategory.GreenPotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemBusinessScrubGreenPotion,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle), Purchaseable]
        [ItemName("Blue Potion"), LocationName("Canyon Scrub Purchase"), MultiLocation(ShopItemBusinessScrubBluePotionInCanyon, ShopItemBusinessScrubBluePotionInOcean)]
        [GossipLocationHint("an eastern merchant"), GossipItemHint("consumable strength", "a magic potion", "a blue drink")]
        [ShopText("Replenishes both life energy and magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_businessscrub_bluepotion))]
        [GetItemIndex(0x19D), ItemPool(ItemCategory.BluePotions, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemBusinessScrubBluePotion,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Zora Hall Stage Lights"), Region(Region.ZoraHall)]
        [GossipLocationHint("a good deed"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x19E), ItemPool(ItemCategory.BlueRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemZoraStageLightsBlueRupee,

        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle), Purchaseable]
        [ItemName("Milk"), LocationName("Gorman Bros Milk Purchase"), Region(Region.MilkRoad)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_shop_gorman_milk))]
        [GetItemIndex(0x1A0), ItemPool(ItemCategory.Milk, LocationCategory.Purchases, ClassicCategory.ShopItems)]
        ShopItemGormanBrosMilk,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Ocean Spider House Day 2 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false, nameof(GameplaySettings.UpdateNPCText), false)]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x134), ItemPool(ItemCategory.PurpleRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemOceanSpiderHouseDay2PurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ocean Spider House Day 3 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false, nameof(GameplaySettings.UpdateNPCText), false)]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1A3), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemOceanSpiderHouseDay3RedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bad Pictograph of Lulu"), Region(Region.ZoraHall)]
        [GossipLocationHint("a fan"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1A8), ItemPool(ItemCategory.BlueRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemLuluBadPictographBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Good Pictograph of Lulu"), Region(Region.ZoraHall)]
        [GossipLocationHint("a fan"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C3), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemLuluGoodPictographRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Treasure Chest Game Human"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFA8, ChestAttribute.AppearanceType.AppearsSwitch, 0x00F43F10 + 0xFB0)]
        [GetItemIndex(0x1C4), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Minigames, ClassicCategory.MundaneRewards)]
        MundaneItemTreasureChestGamePurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Treasure Chest Game Zora"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAC, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1C5), ItemPool(ItemCategory.RedRupees, LocationCategory.Minigames, ClassicCategory.MundaneRewards)]
        MundaneItemTreasureChestGameRedRupee,

        [RupeeRepeatable]
        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Treasure Chest Game Deku"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAE, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1C6), ItemPool(ItemCategory.DekuNuts, LocationCategory.Minigames, ClassicCategory.MundaneRewards)]
        MundaneItemTreasureChestGameDekuNuts,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Curiosity Shop Blue Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C7), ItemPool(ItemCategory.BlueRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemCuriosityShopBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Curiosity Shop Red Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C8), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemCuriosityShopRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Curiosity Shop Purple Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C9), ItemPool(ItemCategory.PurpleRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemCuriosityShopPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Curiosity Shop Gold Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CA), ItemPool(ItemCategory.GoldRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemCuriosityShopGoldRupee,

        [Visible, Purchaseable]
        [Repeatable, Temporary, Overwritable(OverwritableAttribute.ItemSlot.Bottle)]
        [ItemName("Seahorse"), LocationName("Fisherman Pictograph"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a fisherman"), GossipItemHint("a sea creature")]
        [ShopText("It wants to go back home to Pinnacle Rock.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_fisherman))]
        [GetItemIndex(0x95), ItemPool(ItemCategory.Seahorse, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        MundaneItemSeahorse,

        //[GetItemIndex(0x1A1)]

        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Castle Courtyard Grass"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CC), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEB7)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Castle Courtyard Grass 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CD), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEBB)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 1 Grave Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CE), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x623)]
        CollectableBeneathTheGraveyardMainAreaPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 2 Grave Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CF), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x625)]
        CollectableBeneathTheGraveyardInvisibleRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 1 Grave Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D0), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x629)]
        CollectableBeneathTheGraveyardBadBatRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Cucco Shack Crate"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a chicken crate"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D1), ItemPool(ItemCategory.Arrows, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x210A)]
        CollectableCuccoShackWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D2), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1807)]
        CollectableDampesHouseBasementPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D3), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x180A)]
        CollectableDampesHouseBasementPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D4), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x180C)]
        CollectableDampesHouseBasementPot3,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D5), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x180D)]
        CollectableDampesHouseBasementPot4,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Goron Village Small Snowball"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D6), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26B3)]
        CollectableGoronVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Goron Village Small Snowball 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D7), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26B4)]
        CollectableGoronVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D8), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BA4)]
        CollectableGreatBayCoastPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D9), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BA6)]
        CollectableGreatBayCoastPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DA), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BA8)]
        CollectableGreatBayCoastPot3,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 4"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DB), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BAA)]
        CollectableGreatBayCoastPot4,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Temple Red Valve Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DC), ItemPool(ItemCategory.Arrows, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24A1)]
        CollectableGreatBayTempleBlueChuchuValveRoomBarrel1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DD), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B20)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DE), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B23)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Entry Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DF), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B2E)]
        CollectableIgosDuIkanaSLairPreBossRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Entry Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E0), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B2F)]
        CollectableIgosDuIkanaSLairPreBossRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Graveyard Grass"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E1), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A9)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Entrance Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E2), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1430)]
        CollectableOceansideSpiderHouseEntrancePot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Entrance Pot 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E3), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1431)]
        CollectableOceansideSpiderHouseEntrancePot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Sewer Gate Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E4), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11A5)]
        CollectablePiratesFortressInteriorWaterCurrentRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Guarded Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E5), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11AA)]
        CollectablePiratesFortressInterior100RupeeEggRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Barrel Maze Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E6), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11AB)]
        CollectablePiratesFortressInteriorBarrelRoomEggPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Sewer Exit Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E7), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11AC)]
        CollectablePiratesFortressInteriorTelescopeRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Secret Shrine Underwater Pot"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E8), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x307C)]
        CollectableSecretShrineMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Secret Shrine Underwater Pot 2"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E9), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x307D)]
        CollectableSecretShrineMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Snowhead Temple Icicle Room Snowball"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EA), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A0)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Snowhead Temple Icicle Room Snowball 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EB), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A1)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Stone Tower Upper Scarecrow Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EC), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C2F)]
        CollectableStoneTowerPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Stone Tower Upper Scarecrow Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1ED), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C33)]
        CollectableStoneTowerPot2,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Coast Pot 5"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EE), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BAE)]
        CollectableGreatBayCoastPot5,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Temple Seesaw Room Pot"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EF), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24A0)]
        CollectableGreatBayTempleSeesawRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Temple Green Pump Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F0), ItemPool(ItemCategory.Arrows, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24A4)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ikana Canyon Grass"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F1), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9C8)]
        CollectableIkanaCanyonMainAreaGrass1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Milk Road Grass"), Region(Region.MilkRoad)]
        [GossipLocationHint("a roadside plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F2), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1121)]
        CollectableMilkRoadGrass1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Mountain Village Spring Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F3), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2D2A)]
        CollectableMountainVillageSpringSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Mountain Village Winter Small Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F4), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2835)]
        CollectableMountainVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Pirates' Fortress Lone Guard Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F5), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11A9)]
        CollectablePiratesFortressInteriorTwinBarrelEggRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Pirates' Fortress Cage Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F6), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1188)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ranch Crate"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a ranch container"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F7), ItemPool(ItemCategory.Arrows, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1AFF)]
        CollectableRomaniRanchWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Snowhead Small Snowball"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F8), ItemPool(ItemCategory.Arrows, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E21), CollectableIndex(0x3920)]
        CollectableSnowheadSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Owl Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F9), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C38)]
        CollectableStoneTowerPot3,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Zora Cape Owl Pot"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FA), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1C23)]
        CollectableZoraCapePot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Observatory Scarecrow Pot"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FB), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x14A4)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Observatory Scarecrow Pot 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FC), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x14A5)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FD), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1590)]
        CollectableDekuPalaceWestInnerGardenItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FE), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1591)]
        CollectableDekuPalaceEastInnerGardenItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FF), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1592)]
        CollectableDekuPalaceEastInnerGardenItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x200), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x158E)]
        CollectableDekuPalaceWestInnerGardenItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x201), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x158F)]
        CollectableDekuPalaceWestInnerGardenItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x202), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2081)]
        CollectableDoggyRacetrackPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x203), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2082)]
        CollectableDoggyRacetrackPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x204), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2083)]
        CollectableDoggyRacetrackPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x205), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2084)]
        CollectableDoggyRacetrackPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x206), ItemPool(ItemCategory.BlueRupees, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A0)]
        CollectableGoronVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x207), ItemPool(ItemCategory.BlueRupees, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A2)]
        CollectableGoronVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x208), ItemPool(ItemCategory.BlueRupees, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A4)]
        CollectableGoronVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x209), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1B83)]
        CollectableGreatBayCoastPot6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20A), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1B84)]
        CollectableGreatBayCoastPot7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20B), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1B86)]
        CollectableGreatBayCoastPot8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Temple Water Control Room Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2482)]
        CollectableGreatBayTempleWaterControlRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Temple Water Control Room Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20D), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2483)]
        CollectableGreatBayTempleWaterControlRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bio Baba Grotto Hive"), Region(Region.TerminaField)]
        [GossipLocationHint("an underground hive"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20E), ItemPool(ItemCategory.BlueRupees, LocationCategory.Beehives, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x383)]
        CollectableGrottosOceanHeartPieceGrottoBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Laundry Pool Crate"), Region(Region.LaundryPool)]
        [GossipLocationHint("a town crate"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20F), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3820)]
        CollectableLaundryPoolWoodenCrateSmall1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Mountain Village Day 3 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x210), ItemPool(ItemCategory.BlueRupees, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2828)]
        CollectableMountainVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Mountain Village Day 2 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x211), ItemPool(ItemCategory.BlueRupees, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2829)]
        CollectableMountainVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x212), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2E81), CollectableIndex(0x2F01)]
        CollectablePathToGoronVillageWinterItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x213), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2E82), CollectableIndex(0x2F02)]
        CollectablePathToGoronVillageWinterItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x214), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2E83), CollectableIndex(0x2F03)]
        CollectablePathToGoronVillageWinterItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x215), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2E84), CollectableIndex(0x2F04)]
        CollectablePathToGoronVillageWinterItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Barrel Maze Egg Pot 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x216), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11A3)]
        CollectablePiratesFortressInteriorBarrelRoomEggPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x217), ItemPool(ItemCategory.BlueRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1185)]
        CollectablePiratesFortressInteriorTelescopeRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x218), ItemPool(ItemCategory.BlueRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1186)]
        CollectablePiratesFortressInteriorTelescopeRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel 3"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x219), ItemPool(ItemCategory.BlueRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x118A)]
        CollectablePiratesFortressInteriorTelescopeRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Cage Room Barrel"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21A), ItemPool(ItemCategory.BlueRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x118B)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Barn Hay Item"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a bale of hay"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21B), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x801)]
        CollectableRanchHouseBarnBarnItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Barn Hay Item 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a bale of hay"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x802)]
        CollectableRanchHouseBarnBarnItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21D), ItemPool(ItemCategory.BlueRupees, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A2)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 4"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21E), ItemPool(ItemCategory.BlueRupees, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A3)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 5"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21F), ItemPool(ItemCategory.BlueRupees, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A4)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x220), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A8)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x221), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A9)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x222), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10AA)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 4"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x223), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10AB)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 5"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x224), ItemPool(ItemCategory.BlueRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10AC)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Safety Bridge Pot"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x225), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10AD)]
        CollectableSnowheadTempleMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Safety Bridge Pot 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x226), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10AE)]
        CollectableSnowheadTempleMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Cleared Swamp Potion Shop Pot"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x227), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21)]
        CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Near Frog Item"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp flower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x228), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2295), CollectableIndex(0x15)]
        CollectableSouthernSwampPoisonedCentralSwampItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Near Frog Item 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp flower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x229), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2296), CollectableIndex(0x16)]
        CollectableSouthernSwampPoisonedCentralSwampItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Potion Shop Pot"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22A), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22A6)]
        CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Lava Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22B), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB05)]
        CollectableStoneTowerTempleLavaRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Lava Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB06)]
        CollectableStoneTowerTempleLavaRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22D), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB07)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22E), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB08)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22F), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB09)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 4"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x230), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0A)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 5"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x231), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0B)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 6"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x232), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0C)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 7"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x233), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0D)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 8"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x234), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0E)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x235), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC01)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Closest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x236), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC10)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Closest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x237), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC11)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x238), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC07)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x239), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC08)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23A), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC09)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Furthest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23B), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0A)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Furthest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0B)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23D), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0C)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23E), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0D)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Closest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23F), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0E)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Closest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x240), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC0F)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem11,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x241), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2A01)]
        CollectableSwordsmanSSchoolPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 2"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x242), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2A03)]
        CollectableSwordsmanSSchoolPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 3"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x243), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2A04)]
        CollectableSwordsmanSSchoolPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 4"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x244), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2A05)]
        CollectableSwordsmanSSchoolPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 5"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x245), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2A06)]
        CollectableSwordsmanSSchoolPot5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Item"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned stump"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x246), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2301)]
        CollectableWoodfallItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Entrance Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x247), ItemPool(ItemCategory.BlueRupees, LocationCategory.Beehives, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA0)]
        CollectableWoodfallTempleEntranceRoomBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x248), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA6)]
        CollectableWoodfallTempleGekkoRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 2"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x249), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA7)]
        CollectableWoodfallTempleGekkoRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 3"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24A), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA8)]
        CollectableWoodfallTempleGekkoRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 4"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24B), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA9)]
        CollectableWoodfallTempleGekkoRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD83)]
        CollectableWoodfallTemplePreBossRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 2"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24D), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD84)]
        CollectableWoodfallTemplePreBossRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 3"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24E), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD85)]
        CollectableWoodfallTemplePreBossRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 4"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24F), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD86)]
        CollectableWoodfallTemplePreBossRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x250), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x25A0)]
        CollectableBeneathTheWellBugAndBombRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 2"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x251), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x25A1)]
        CollectableBeneathTheWellBugAndBombRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 3"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x252), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x25A2)]
        CollectableBeneathTheWellBugAndBombRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 4"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x253), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x25A3)]
        CollectableBeneathTheWellBugAndBombRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 5"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x254), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x25A4)]
        CollectableBeneathTheWellBugAndBombRoomPot5,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Goron Village Small Snowball 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x255), ItemPool(ItemCategory.Bombs, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26AB)]
        CollectableGoronVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Goron Village Small Snowball 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x256), ItemPool(ItemCategory.Bombs, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26B0)]
        CollectableGoronVillageWinterSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Coast Pot 6"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x257), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BAD)]
        CollectableGreatBayCoastPot9,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Temple Red Valve Barrel 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x258), ItemPool(ItemCategory.Bombs, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24A2)]
        CollectableGreatBayTempleBlueChuchuValveRoomBarrel2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Temple Green Pump Barrel 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x259), ItemPool(ItemCategory.Bombs, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24A5)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Ikana Canyon Grass 2"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25A), ItemPool(ItemCategory.Bombs, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9C7)]
        CollectableIkanaCanyonMainAreaGrass2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Mountain Village Spring Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25B), ItemPool(ItemCategory.Bombs, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2D29)]
        CollectableMountainVillageSpringSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Mountain Village Winter Small Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25C), ItemPool(ItemCategory.Bombs, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2834)]
        CollectableMountainVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Snowhead Small Snowball 2"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25D), ItemPool(ItemCategory.Bombs, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E20), CollectableIndex(0x3929)]
        CollectableSnowheadSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Stone Tower Owl Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25E), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C37)]
        CollectableStoneTowerPot4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Inverted Stone Tower Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25F), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2CBA)]
        CollectableStoneTowerInvertedStoneTowerFlippedPot1,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Zora Cape Owl Pot 2"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x260), ItemPool(ItemCategory.Bombs, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1C22)]
        CollectableZoraCapePot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Ikana Castle Left Staircase Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x261), ItemPool(ItemCategory.DekuNuts, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEC4)]
        CollectableAncientCastleOfIkana1FWestStaircasePot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Goron Village Small Snowball 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x262), ItemPool(ItemCategory.DekuNuts, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26AD)]
        CollectableGoronVillageWinterSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Goron Village Small Snowball 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x263), ItemPool(ItemCategory.DekuNuts, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26B1)]
        CollectableGoronVillageWinterSmallSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Pirates' Fortress Sewer Exit Pot 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x264), ItemPool(ItemCategory.DekuNuts, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11A8)]
        CollectablePiratesFortressInteriorTelescopeRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Woodfall Pot"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned platform"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x265), ItemPool(ItemCategory.DekuNuts, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2322)]
        CollectableWoodfallPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x266), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1960)]
        CollectableGoronShrineGoronKidSRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x267), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1961)]
        CollectableGoronShrineGoronKidSRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x268), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1963)]
        CollectableGoronShrineMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x269), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1966)]
        CollectableGoronShrineMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26A), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x196A)]
        CollectableGoronShrineMainRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Village Small Snowball 7"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26B), ItemPool(ItemCategory.DekuSticks, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26AC)]
        CollectableGoronVillageWinterSmallSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Village Small Snowball 8"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26C), ItemPool(ItemCategory.DekuSticks, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26AF)]
        CollectableGoronVillageWinterSmallSnowball8,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Cleared Swamp Owl Grass"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26D), ItemPool(ItemCategory.DekuSticks, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26)]
        CollectableSouthernSwampClearCentralSwampGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Southern Swamp Owl Grass"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26E), ItemPool(ItemCategory.DekuSticks, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22AC)]
        CollectableSouthernSwampPoisonedCentralSwampGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Woodfall Pot 2"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned platform"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26F), ItemPool(ItemCategory.DekuSticks, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2320)]
        CollectableWoodfallPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x270), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1805)]
        CollectableDampesHouseBasementPot5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x271), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1806)]
        CollectableDampesHouseBasementPot6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x272), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1809)]
        CollectableDampesHouseBasementPot7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x273), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1581)]
        CollectableDekuPalaceEastInnerGardenItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x274), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1582)]
        CollectableDekuPalaceEastInnerGardenItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 8"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x275), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1583)]
        CollectableDekuPalaceEastInnerGardenItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 9"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x276), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1584)]
        CollectableDekuPalaceEastInnerGardenItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 10"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x277), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1585)]
        CollectableDekuPalaceEastInnerGardenItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 11"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x278), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1586)]
        CollectableDekuPalaceEastInnerGardenItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 12"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x279), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1587)]
        CollectableDekuPalaceWestInnerGardenItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 13"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1588)]
        CollectableDekuPalaceWestInnerGardenItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 14"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1589)]
        CollectableDekuPalaceWestInnerGardenItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Out of Bounds Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a hidden royal treasure"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27C), ItemPool(ItemCategory.GreenRupees, LocationCategory.GlitchesRequired, ClassicCategory.GlitchesRequired), CollectableIndex(0x158A)]
        CollectableDekuPalaceWestInnerGardenItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 15"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x158B)]
        CollectableDekuPalaceWestInnerGardenItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 16"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x158C)]
        CollectableDekuPalaceWestInnerGardenItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 17"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x158D)]
        CollectableDekuPalaceWestInnerGardenItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x280), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2920)]
        CollectableDekuShrineGiantRoomFloor1Item1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 2"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x281), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2921)]
        CollectableDekuShrineGiantRoomFloor1Item2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 3"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x282), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2922)]
        CollectableDekuShrineGiantRoomFloor1Item3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 4"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x283), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2923)]
        CollectableDekuShrineGiantRoomFloor1Item4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 5"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x284), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2924)]
        CollectableDekuShrineGiantRoomFloor1Item5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 6"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x285), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2925)]
        CollectableDekuShrineGiantRoomFloor1Item6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x286), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2926)]
        CollectableDekuShrineWaterRoomWithPlatformsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 2"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x287), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2927)]
        CollectableDekuShrineWaterRoomWithPlatformsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 3"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x288), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2928)]
        CollectableDekuShrineWaterRoomWithPlatformsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 4"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x289), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2929)]
        CollectableDekuShrineWaterRoomWithPlatformsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 5"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292A)]
        CollectableDekuShrineWaterRoomWithPlatformsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 6"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292B)]
        CollectableDekuShrineWaterRoomWithPlatformsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28C), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292C)]
        CollectableDekuShrineRoomBeforeFlameWallsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 2"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292D)]
        CollectableDekuShrineRoomBeforeFlameWallsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 3"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292E)]
        CollectableDekuShrineRoomBeforeFlameWallsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 4"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x292F)]
        CollectableDekuShrineRoomBeforeFlameWallsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 5"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x290), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2930)]
        CollectableDekuShrineRoomBeforeFlameWallsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 6"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x291), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2931)]
        CollectableDekuShrineRoomBeforeFlameWallsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x292), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2932)]
        CollectableDekuShrineDekuButlerSRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 2"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x293), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2933)]
        CollectableDekuShrineDekuButlerSRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 3"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x294), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2934)]
        CollectableDekuShrineDekuButlerSRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 4"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x295), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2935)]
        CollectableDekuShrineDekuButlerSRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 5"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x296), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2936)]
        CollectableDekuShrineDekuButlerSRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 6"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x297), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2937)]
        CollectableDekuShrineDekuButlerSRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 7"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x298), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2938)]
        CollectableDekuShrineDekuButlerSRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 8"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x299), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2939)]
        CollectableDekuShrineDekuButlerSRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 9"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x293A)]
        CollectableDekuShrineDekuButlerSRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 10"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x293B)]
        CollectableDekuShrineDekuButlerSRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Dual Pot"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29C), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x297F)]
        CollectableDekuShrineGreyBoulderRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Crate"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town crate"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3601)]
        CollectableEastClockTownWoodenCrateSmall1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Great Bay Temple Water Control Room Item 3"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2484)]
        CollectableGreatBayTempleWaterControlRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Great Bay Temple Water Control Room Item 4"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2485)]
        CollectableGreatBayTempleWaterControlRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Grass 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A0), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A5)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Potion Shop Item"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a shop corner"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A1), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x501)]
        CollectableMagicHagsPotionShopItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A2), ItemPool(ItemCategory.GreenRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1181)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 3"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A3), ItemPool(ItemCategory.GreenRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1183)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 4"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A4), ItemPool(ItemCategory.GreenRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1184)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 5"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A5), ItemPool(ItemCategory.GreenRupees, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1187)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A6), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3032)]
        CollectableSecretShrineEntranceRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 2"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A7), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3033)]
        CollectableSecretShrineEntranceRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 3"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A8), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3034)]
        CollectableSecretShrineEntranceRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 4"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A9), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3035)]
        CollectableSecretShrineEntranceRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 5"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AA), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3036)]
        CollectableSecretShrineEntranceRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 6"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AB), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3037)]
        CollectableSecretShrineEntranceRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 7"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AC), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3038)]
        CollectableSecretShrineEntranceRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 8"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AD), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3039)]
        CollectableSecretShrineEntranceRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 9"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AE), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303A)]
        CollectableSecretShrineEntranceRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 10"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AF), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303B)]
        CollectableSecretShrineEntranceRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 11"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B0), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303C)]
        CollectableSecretShrineEntranceRoomItem11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 12"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B1), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303D)]
        CollectableSecretShrineEntranceRoomItem12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 13"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B2), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303E)]
        CollectableSecretShrineEntranceRoomItem13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 14"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B3), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x303F)]
        CollectableSecretShrineEntranceRoomItem14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 15"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B4), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3040)]
        CollectableSecretShrineEntranceRoomItem15,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 16"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B5), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3041)]
        CollectableSecretShrineEntranceRoomItem16,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 17"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B6), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3042)]
        CollectableSecretShrineEntranceRoomItem17,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cleared Swamp Potion Shop Pot 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B7), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22)]
        CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Potion Shop Pot 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B8), ItemPool(ItemCategory.GreenRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22A7)]
        CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B9), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB02)]
        CollectableStoneTowerTempleLavaRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 4"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BA), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB03)]
        CollectableStoneTowerTempleLavaRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 5"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BB), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB04)]
        CollectableStoneTowerTempleLavaRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BC), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC03)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BD), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xCC0)]
        CollectableClockTowerRooftopPot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 2"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BE), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xCC1)]
        CollectableClockTowerRooftopPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 3"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BF), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xCC2)]
        CollectableClockTowerRooftopPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 4"), Region(Region.ClockTowerRoof)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C0), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xCC3)]
        CollectableClockTowerRooftopPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C1), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A0)]
        CollectableGoronRacetrackPot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 2"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C2), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A1)]
        CollectableGoronRacetrackPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 3"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C3), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A2)]
        CollectableGoronRacetrackPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 4"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C4), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A3)]
        CollectableGoronRacetrackPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 5"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C5), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A4)]
        CollectableGoronRacetrackPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 6"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C6), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A5)]
        CollectableGoronRacetrackPot6,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 7"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C7), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A6)]
        CollectableGoronRacetrackPot7,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 8"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C8), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A7)]
        CollectableGoronRacetrackPot8,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 9"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C9), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A8)]
        CollectableGoronRacetrackPot9,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 10"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CA), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35A9)]
        CollectableGoronRacetrackPot10,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 11"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CB), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AA)]
        CollectableGoronRacetrackPot11,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 12"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CC), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AB)]
        CollectableGoronRacetrackPot12,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 13"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CD), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AC)]
        CollectableGoronRacetrackPot13,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 14"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CE), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AD)]
        CollectableGoronRacetrackPot14,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 15"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CF), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AE)]
        CollectableGoronRacetrackPot15,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 16"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D0), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35AF)]
        CollectableGoronRacetrackPot16,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 17"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D1), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B0)]
        CollectableGoronRacetrackPot17,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 18"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D2), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B1)]
        CollectableGoronRacetrackPot18,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 19"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D3), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B2)]
        CollectableGoronRacetrackPot19,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 20"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D4), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B3)]
        CollectableGoronRacetrackPot20,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 21"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D5), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B4)]
        CollectableGoronRacetrackPot21,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 22"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D6), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B5)]
        CollectableGoronRacetrackPot22,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 23"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D7), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B6)]
        CollectableGoronRacetrackPot23,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 24"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D8), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B7)]
        CollectableGoronRacetrackPot24,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 25"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D9), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B8)]
        CollectableGoronRacetrackPot25,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 26"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DA), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35B9)]
        CollectableGoronRacetrackPot26,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 27"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DB), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35BD)]
        CollectableGoronRacetrackPot27,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DC), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1962)]
        CollectableGoronShrineGoronKidSRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 7"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DD), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1967)]
        CollectableGoronShrineMainRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 8"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DE), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1968)]
        CollectableGoronShrineMainRoomPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 9"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DF), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1969)]
        CollectableGoronShrineMainRoomPot6,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Great Bay Coast Pot 7"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E0), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BAC)]
        CollectableGreatBayCoastPot10,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Great Bay Temple Red Valve Crate"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E1), ItemPool(ItemCategory.MagicJars, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24B2)]
        CollectableGreatBayTempleBlueChuchuValveRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Ikana King Pot 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E2), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B21)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Ikana Canyon Grass 3"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E3), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9C6)]
        CollectableIkanaCanyonMainAreaGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Milk Road Grass 2"), Region(Region.MilkRoad)]
        [GossipLocationHint("a roadside plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E4), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1122)]
        CollectableMilkRoadGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Mountain Village Spring Snowball 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E5), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2D2C)]
        CollectableMountainVillageSpringSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Graveyard Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a high snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E6), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x282D)]
        CollectableMountainVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Graveyard Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a high snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E7), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x282E)]
        CollectableMountainVillageWinterSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Mountain Village Winter Small Snowball 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E8), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2837)]
        CollectableMountainVillageWinterSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Snowhead Small Snowball 3"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E9), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E23), CollectableIndex(0x3922)]
        CollectableSnowheadSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Stone Tower Owl Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EA), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C3A)]
        CollectableStoneTowerPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Inverted Stone Tower Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EB), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2CB8)]
        CollectableStoneTowerInvertedStoneTowerFlippedPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EC), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3320)]
        CollectableTheMoonLinkTrialEntrancePot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2ED), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3321)]
        CollectableTheMoonLinkTrialEntrancePot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 3"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EE), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3322)]
        CollectableTheMoonLinkTrialEntrancePot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 4"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EF), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x3323)]
        CollectableTheMoonLinkTrialEntrancePot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Zora Cape Owl Pot 3"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F0), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1C25)]
        CollectableZoraCapePot3,


        [Visible]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Dampe's Basement Pot 8"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F1), ItemPool(ItemCategory.PurpleRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x180B)]
        CollectableDampesHouseBasementPot8,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F2), ItemPool(ItemCategory.RecoveryHearts, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xA20)]
        CollectablePiratesFortressItem1,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F3), ItemPool(ItemCategory.RecoveryHearts, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xA21)]
        CollectablePiratesFortressItem2,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F4), ItemPool(ItemCategory.RecoveryHearts, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xA22)]
        CollectablePiratesFortressItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Butler Race Pillar Item 7"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F5), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x293C)]
        CollectableDekuShrineGiantRoomFloor1Item7,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Butler Race Pillar Item 8"), Region(Region.ButlerRaceItems)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F6), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x293D)]
        CollectableDekuShrineGiantRoomFloor1Item8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Water Control Room Item 5"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F7), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2481)]
        CollectableGreatBayTempleWaterControlRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Dexihand Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F8), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2486)]
        CollectableGreatBayTempleCompassBossKeyRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Dexihand Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F9), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2487)]
        CollectableGreatBayTempleCompassBossKeyRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Green Pump Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FA), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2488)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Green Pump Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FB), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x2489)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FC), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3803)]
        CollectableLaundryPoolItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item 2"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FD), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3804)]
        CollectableLaundryPoolItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item 3"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FE), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x3805)]
        CollectableLaundryPoolItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Village Spring Stair Item"), Region(Region.MountainVillage)]
        [GossipLocationHint("an item under the stairs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FF), ItemPool(ItemCategory.RedRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2809), CollectableIndex(0x2D09)]
        CollectableMountainVillageWinterMountainVillageSpringItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x300), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1081)]
        CollectableSnowheadTempleIceBlockRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x301), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1082)]
        CollectableSnowheadTempleIceBlockRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x302), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1083)]
        CollectableSnowheadTempleIceBlockRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Swamp Near Frog Hive"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp hive"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x303), ItemPool(ItemCategory.RedRupees, LocationCategory.Beehives, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22A8)]
        CollectableSouthernSwampPoisonedCentralSwampBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Lava Room Item 6"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x304), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB01)]
        CollectableStoneTowerTempleLavaRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Eyegore Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x305), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB10)]
        CollectableStoneTowerTempleEyegoreRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Mirror Room Crate"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x306), ItemPool(ItemCategory.RedRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xB25)]
        CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Mirror Room Crate 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x307), ItemPool(ItemCategory.RedRupees, LocationCategory.Crates, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xB26)]
        CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Eyegore Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x308), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xB0F)]
        CollectableStoneTowerTempleEyegoreRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x309), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC02)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Updraft Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30A), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC04)]
        CollectableStoneTowerTempleInvertedAirRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Updraft Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30B), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xC05)]
        CollectableStoneTowerTempleInvertedAirRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Pillar Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a pillar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30C), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0x1682)]
        CollectableTerminaFieldItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Woodfall Temple Pre-Boss Left Pillar Item"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30D), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD81)]
        CollectableWoodfallTemplePreBossRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Woodfall Temple Pre-Boss Right Pillar Item"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30E), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), CollectableIndex(0xD82)]
        CollectableWoodfallTemplePreBossRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Courtyard Grass 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30F), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEB4)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Courtyard Grass 4"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x310), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEB6)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Fire Ceiling Room Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x311), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEC1)]
        CollectableAncientCastleOfIkanaFireCeilingRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Hole Room Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x312), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xECF)]
        CollectableAncientCastleOfIkanaHoleRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Hole Room Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x313), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEDA)]
        CollectableAncientCastleOfIkanaHoleRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Balloon Pot"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x314), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x14A1)]
        CollectableAstralObservatorySewerPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Balloon Pot 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x315), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x14A2)]
        CollectableAstralObservatorySewerPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Scarecrow Pot 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x316), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x14A6)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Night 2 Grave Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x317), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x62C)]
        CollectableBeneathTheGraveyardMainAreaPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Deku Palace Pot"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal pathway", "the home of scrubs"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x318), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x15B2)]
        CollectableDekuPalaceEastInnerGardenPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Deku Palace Pot 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal pathway", "the home of scrubs"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x319), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x15B3)]
        CollectableDekuPalaceEastInnerGardenPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 28"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31A), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35BA)]
        CollectableGoronRacetrackPot28,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 29"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31B), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35BB)]
        CollectableGoronRacetrackPot29,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 30"), Region(Region.GoronRaceItems)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31C), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x35BC)]
        CollectableGoronRacetrackPot30,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Shrine Pot 10"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31D), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1964)]
        CollectableGoronShrineMainRoomPot7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Shrine Pot 11"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31E), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1965)]
        CollectableGoronShrineMainRoomPot8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31F), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A1)]
        CollectableGoronVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x320), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A3)]
        CollectableGoronVillageWinterLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x321), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26A5)]
        CollectableGoronVillageWinterLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Small Snowball 9"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x322), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26AE)]
        CollectableGoronVillageWinterSmallSnowball9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Small Snowball 10"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x323), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x26B2)]
        CollectableGoronVillageWinterSmallSnowball10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana King Entry Pot 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x324), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2B30)]
        CollectableIgosDuIkanaSLairPreBossRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Graveyard Grass 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x325), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A6)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Winter Small Snowball 4"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x326), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2821)]
        CollectableMountainVillageWinterSmallSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Winter Small Snowball 5"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x327), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2827)]
        CollectableMountainVillageWinterSmallSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Day 1 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x328), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x282A)]
        CollectableMountainVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Day 2 Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x329), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x282C)]
        CollectableMountainVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Main Room Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32A), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1426)]
        CollectableOceansideSpiderHouseMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Entrance Pot 3"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32B), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x142E)]
        CollectableOceansideSpiderHouseEntrancePot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Main Room Pot 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32C), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1433)]
        CollectableOceansideSpiderHouseMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Storage Room Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32D), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1439)]
        CollectableOceansideSpiderHouseStorageRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32E), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA0)]
        CollectablePathToGoronVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32F), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA2)]
        CollectablePathToGoronVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x330), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA3)]
        CollectablePathToGoronVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x331), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA4)]
        CollectablePathToGoronVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 5"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x332), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA5)]
        CollectablePathToGoronVillageWinterLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x333), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EA8)]
        CollectablePathToGoronVillageWinterLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x334), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EAA)]
        CollectablePathToGoronVillageWinterLargeSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x335), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EAB)]
        CollectablePathToGoronVillageWinterLargeSnowball8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x336), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EAC)]
        CollectablePathToGoronVillageWinterLargeSnowball9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x337), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EAE)]
        CollectablePathToGoronVillageWinterLargeSnowball10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x338), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB0)]
        CollectablePathToGoronVillageWinterLargeSnowball11,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x339), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB1)]
        CollectablePathToGoronVillageWinterLargeSnowball12,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33A), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB3)]
        CollectablePathToGoronVillageWinterLargeSnowball13,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 5"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33B), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB5)]
        CollectablePathToGoronVillageWinterLargeSnowball14,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Small Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33C), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB6)]
        CollectablePathToGoronVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Small Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33D), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB8)]
        CollectablePathToGoronVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Ramp Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33E), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2EB9)]
        CollectablePathToGoronVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Mountain Village Small Snowball"), Region(Region.PathToMountainVillage)]
        [GossipLocationHint("a foothill snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33F), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xE23)]
        CollectablePathToMountainVillageSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x340), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DE0)]
        CollectablePathToSnowheadLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 2"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x341), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DE1)]
        CollectablePathToSnowheadLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 3"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x342), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DE2)]
        CollectablePathToSnowheadLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 4"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x343), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DE3)]
        CollectablePathToSnowheadLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x344), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x12A0)]
        CollectablePinnacleRockPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 2"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x345), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x12A6)]
        CollectablePinnacleRockPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 3"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x346), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x12AB)]
        CollectablePinnacleRockPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 4"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x347), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x12AD)]
        CollectablePinnacleRockPot4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Secret Shrine Underwater Pot 3"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x348), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x307B)]
        CollectableSecretShrineMainRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Secret Shrine Underwater Pot 4"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x349), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x307F)]
        CollectableSecretShrineMainRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34A), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E24)]
        CollectableSnowheadLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 2"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34B), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E25)]
        CollectableSnowheadLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 3"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34C), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E26), CollectableIndex(0x3925)]
        CollectableSnowheadLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 4"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34D), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E27)]
        CollectableSnowheadLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 5"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34E), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E28), CollectableIndex(0x3927)]
        CollectableSnowheadLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 6"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34F), ItemPool(ItemCategory.MagicJars, LocationCategory.LargeSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E29), CollectableIndex(0x3928)]
        CollectableSnowheadLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x350), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C20)]
        CollectableStoneTowerPot6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x351), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C22)]
        CollectableStoneTowerPot7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Upper Scarecrow Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x352), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C2E)]
        CollectableStoneTowerPot8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Upper Scarecrow Pot 4"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x353), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C31)]
        CollectableStoneTowerPot9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x354), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C34)]
        CollectableStoneTowerPot10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Zora Cape Waterfall Pot"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x355), ItemPool(ItemCategory.MagicJars, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1C21)]
        CollectableZoraCapePot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Fence Item"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x356), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Fence Item 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x357), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x358), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x359), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 5"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35A), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 6"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35B), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Above Cow Grotto Invisible Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35D), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35F), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x363), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 4"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x364), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 5"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x365), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 6"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x366), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 7"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x367), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 8"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x368), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Northern Ramp Invisible Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x369), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem9,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 10"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36A), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem10,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 11"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36B), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x371), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x372), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item 3"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x373), ItemPool(ItemCategory.GreenRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Spider House Invisible Item 4"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x374), ItemPool(ItemCategory.BlueRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Swamp Spider House Invisible Item 5"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x375), ItemPool(ItemCategory.RedRupees, LocationCategory.InvisibleItems, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Tree Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a tree"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x360), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldTreeItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Pillar Spawned Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a pillar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x361), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldPillarItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Telescope Guay"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x362), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldTelescopeGuay1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman School Gong"), Region(Region.WestClockTown)]
        [GossipLocationHint("timekeeping"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35C), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableSwordsmanSchoolGong1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bean Grotto Soft Soil"), Region(Region.DekuPalace)]
        [GossipLocationHint("underground soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35E), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableBeanGrottoSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Soft Soil"), Region(Region.DekuPalace)]
        [GossipLocationHint("royal soil", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36C), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableDekuPalaceSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Doggy Racetrack Soft Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36D), ItemPool(ItemCategory.RedRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableDoggyRacetrackSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Coast Soft Soil"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("ocean soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36E), ItemPool(ItemCategory.RedRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableGreatBayCoastSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ranch Day 1 Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("early soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x376), ItemPool(ItemCategory.Arrows, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableRomaniRanchSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Day 2 or 3 Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("late soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x370), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableRomaniRanchSoftSoil2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Secret Shrine Soft Soil"), Region(Region.SecretShrine)]
        [GossipLocationHint("secret soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36F), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableSecretShrineSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Soft Soil Lower"), Region(Region.StoneTower)]
        [GossipLocationHint("high soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x377), ItemPool(ItemCategory.Arrows, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableStoneTowerSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Soft Soil Upper"), Region(Region.StoneTower)]
        [GossipLocationHint("high soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x378), ItemPool(ItemCategory.Arrows, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableStoneTowerSoftSoil2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Spider House Rock Soft Soil"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("rock soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x379), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableSwampSpiderHouseSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Swamp Spider House Gold Room Soft Soil"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("gold soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37A), ItemPool(ItemCategory.Arrows, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableSwampSpiderHouseSoftSoil2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Stump Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37B), ItemPool(ItemCategory.RedRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableTerminaFieldSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Observatory Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37C), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableTerminaFieldSoftSoil2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field South Wall Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("wall soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37D), ItemPool(ItemCategory.BlueRupees, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableTerminaFieldSoftSoil3,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Pillar Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37E), ItemPool(ItemCategory.Arrows, LocationCategory.SoftSoil, ClassicCategory.SoftSoil), NullableItem]
        CollectableTerminaFieldSoftSoil4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #1"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #2"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x380), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #3"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x381), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #4"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x382), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #5"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x383), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #6"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x384), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #7"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x385), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #8"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x386), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #9"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x387), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #10"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x388), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #11"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x389), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #12"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #13"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #14"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38C), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #15"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay15,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #16"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay16,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #17"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay17,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #18"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x390), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay18,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #19"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x391), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay19,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Guay #20"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x392), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay20,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #5a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x393), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay21,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #10a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x394), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay22,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #15a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x395), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldGuay23,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x396), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x397), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x398), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x399), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster6,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Deku Palace Rupee Cluster 7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39C), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A0), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A1), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A2), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster6,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard Rupee Cluster 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A3), ItemPool(ItemCategory.RedRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster7,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A4), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A5), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A6), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A7), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A8), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A9), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AA), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AB), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AC), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall9,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall 4 / 20"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AD), ItemPool(ItemCategory.RedRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall10,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 4 / 20 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AE), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall11,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 4 / 20 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AF), ItemPool(ItemCategory.BlueRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B0), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B1), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B2), ItemPool(ItemCategory.GreenRupees, LocationCategory.Events, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldSongWall15,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B3), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 2"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B4), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 3"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B5), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 4"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B6), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 5"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B7), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 2 Item 6"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B8), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B9), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 2"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BA), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 3"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BB), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 4"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BC), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 5"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BD), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem11,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 1 Item 6"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BE), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BF), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 2"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C0), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 3"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C1), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem15,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 4"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C2), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem16,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 3 Item 5"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C3), ItemPool(ItemCategory.BlueRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem17,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 6"), Region(Region.DekuPlaygroundItems)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C4), ItemPool(ItemCategory.GreenRupees, LocationCategory.Freestanding, ClassicCategory.FreestandingRupees), NullableItem]
        CollectableDekuPlaygroundItem18,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C5), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C6), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C7), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C8), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C9), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CA), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CB), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CC), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CD), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CE), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 2"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CF), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 3"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D0), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 4"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D1), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 5"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D2), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 6"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D3), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 7"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D4), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 8"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D5), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 9"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D6), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 10"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D7), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 11"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D8), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 12"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D9), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSwampSpiderHouseHitTag12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DA), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DB), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 3"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DC), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 4"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DD), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 5"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DE), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 6"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DF), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 7"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E0), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 8"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E1), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 9"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E2), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableOceansideSpiderHouseHitTag9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E3), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam 2"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E4), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam 3"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E5), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E6), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E7), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E8), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E9), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EA), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EB), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableTerminaFieldHitTag9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EC), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3ED), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EE), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EF), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 5"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F0), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 6"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F1), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableCuccoShackHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F2), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F3), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F4), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F5), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F6), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F7), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F8), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 8"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F9), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 9"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FA), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 10"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FB), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 11"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FC), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 12"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FD), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableIkanaGraveyardHitTag12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FE), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableStockPotInnHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask 2"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FF), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableStockPotInnHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask 3"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x400), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableStockPotInnHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x401), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x402), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x403), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 4"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x404), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 5"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x405), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 6"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x406), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x407), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x408), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x409), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableEastClockTownHitTag9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40A), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSouthClockTownHitTag1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock 2"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40B), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSouthClockTownHitTag2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock 3"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40C), ItemPool(ItemCategory.GreenRupees, LocationCategory.HitSpots, ClassicCategory.HiddenRupees), NullableItem]
        CollectableSouthClockTownHitTag3,


        [Visible]
        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Takkuri"), Region(Region.TerminaField)]
        [GossipLocationHint("a thief"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40D), ItemPool(ItemCategory.GoldRupees, LocationCategory.EnemySpawn, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableTerminaFieldEnemy1,



        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Hookshot Room Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40E), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11AD)]
        CollectablePiratesFortressInteriorHookshotRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Hookshot Room Pot 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40F), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x11AE)]
        CollectablePiratesFortressInteriorHookshotRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Rock"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x410), ItemPool(ItemCategory.Arrows, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1694)]
        CollectableTerminaFieldRock1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Rock 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x411), ItemPool(ItemCategory.Arrows, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1695)]
        CollectableTerminaFieldRock2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard Highest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x412), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A0)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard Lowest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x413), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A1)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard 2nd Lowest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x414), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A4)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x415), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1690)]
        CollectableTerminaFieldRock3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 4"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x416), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1691)]
        CollectableTerminaFieldRock4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 5"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x417), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1692)]
        CollectableTerminaFieldRock5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 6"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x418), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1696)]
        CollectableTerminaFieldRock6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 7"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x419), ItemPool(ItemCategory.BlueRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x168F)]
        CollectableTerminaFieldRock7,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard 2nd Highest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41A), ItemPool(ItemCategory.RedRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A2)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock4,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard Middle Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41B), ItemPool(ItemCategory.RedRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x21A3)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Rock 8"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41C), ItemPool(ItemCategory.RedRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x168D)]
        CollectableTerminaFieldRock8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Rock 9"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41D), ItemPool(ItemCategory.RedRupees, LocationCategory.Rocks, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x168E)]
        CollectableTerminaFieldRock9,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass1,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 2"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41F), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass2,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 3"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x420), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass3,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 4"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x421), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass4,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 5"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x422), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass5,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 6"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x423), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 7"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x424), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 8"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x425), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Milk Road Keaton Grass 9"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x426), ItemPool(ItemCategory.RedRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMilkRoadKeatonGrass9,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x427), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass1,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 2"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x428), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass2,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 3"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x429), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass3,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 4"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42A), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass4,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 5"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42B), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass5,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 6"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42C), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 7"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42D), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 8"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42E), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("North Clock Town Keaton Grass 9"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42F), ItemPool(ItemCategory.RedRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableNorthClockTownKeatonGrass9,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x430), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass1,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x431), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass2,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x432), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass3,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 4"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x433), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass4,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 5"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x434), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass5,


        [Visible]
        [Repeatable]
        [BlockBombTrapPlacement]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 6"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x435), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 7"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x436), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 8"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x437), ItemPool(ItemCategory.GreenRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Village Spring Keaton Grass 9"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x438), ItemPool(ItemCategory.RedRupees, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), NullableItem]
        CollectableMountainVillageSpringKeatonGrass9,



        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Mask Room Pot"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x439), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x143A)]
        CollectableOceansideSpiderHouseMaskRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Mask Room Pot 2"), Region(Region.OceanSpiderHouseItems)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43A), ItemPool(ItemCategory.Arrows, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x143B)]
        CollectableOceansideSpiderHouseMaskRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ikana Canyon Cleared Grass"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("uncursed grass"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43B), ItemPool(ItemCategory.Arrows, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9A2)]
        CollectableIkanaCanyonMainAreaGrass4,



        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Ikana Canyon Cleared Grass 2"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("uncursed grass"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43C), ItemPool(ItemCategory.Bombs, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9A1)]
        CollectableIkanaCanyonMainAreaGrass5,



        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Ikana Canyon Cleared Grass 3"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("uncursed grass"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43D), ItemPool(ItemCategory.MagicJars, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9A0)]
        CollectableIkanaCanyonMainAreaGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Path to Snowhead Spring Snowball"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43E), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DA0)]
        CollectablePathToSnowheadSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Path to Snowhead Spring Snowball 2"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43F), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DA1)]
        CollectablePathToSnowheadSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Path to Snowhead Spring Snowball 3"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x440), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DA2)]
        CollectablePathToSnowheadSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Path to Snowhead Spring Snowball 4"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x441), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2DA3)]
        CollectablePathToSnowheadSmallSnowball4,



        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Mountain Village Spring Snowball"), Region(Region.PathToMountainVillage)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x442), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x38A1)]
        CollectablePathToMountainVillageSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Mountain Village Spring Snowball 2"), Region(Region.PathToMountainVillage)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x443), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x38A2)]
        CollectablePathToMountainVillageSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Mountain Village Spring Snowball 3"), Region(Region.PathToMountainVillage)]
        [GossipLocationHint("a melting snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x444), ItemPool(ItemCategory.MagicJars, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x38A5)]
        CollectablePathToMountainVillageSmallSnowball4,


        [Purchaseable]
        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Zora Cape Jar Game"), Region(Region.ZoraCape)]
        [GossipLocationHint("an ocean game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x445), ItemPool(ItemCategory.SilverRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        CollectableZoraCapeJarGame1,


        [Visible]
        [Repeatable]
        [ItemName("Crimson Rupee"), LocationName("Ikana Graveyard Day 2 Bats"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a swarm of bats"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 30 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x446), ItemPool(ItemCategory.RedRupees, LocationCategory.NpcRewards, ClassicCategory.MundaneRewards)]
        [ExclusiveItemMessage(0x9001, "\u0017You got a \u0001Crimson Rupee\u0000!\u0018\u0011It's worth \u000130 Rupees\u0000!\u0011What a pleasant surprise!\u00BF")]
        CollectableIkanaGraveyardDay2Bats1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Cucco Shack Potted Plant"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a potted plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x447), ItemPool(ItemCategory.BlueRupees, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x210B)]
        CollectableCuccoShackPottedPlant1,


        [Visible]
        [StartingItem(0xC5CE73, 0x01)]
        [ItemName("Odolwa's Remains"), LocationName("Woodfall Boss Warp"), Region(AreaOdolwasLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("an evil mask")]
        [ShopText("The remains of the boss in Woodfall Temple.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x448), ItemPool(ItemCategory.BossRemains, LocationCategory.BossFights, ClassicCategory.BossRemains)]
        RemainsOdolwa,


        [Visible]
        [StartingItem(0xC5CE73, 0x02)]
        [ItemName("Goht's Remains"), LocationName("Snowhead Boss Warp"), Region(AreaGohtsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("an evil mask")]
        [ShopText("The remains of the boss in Snowhead Temple.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x449), ItemPool(ItemCategory.BossRemains, LocationCategory.BossFights, ClassicCategory.BossRemains)]
        RemainsGoht,


        [Visible]
        [StartingItem(0xC5CE73, 0x04)]
        [ItemName("Gyorg's Remains"), LocationName("Great Bay Boss Warp"), Region(AreaGyorgsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("an evil mask")]
        [ShopText("The remains of the boss in Great Bay Temple.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x44A), ItemPool(ItemCategory.BossRemains, LocationCategory.BossFights, ClassicCategory.BossRemains)]
        RemainsGyorg,


        [Visible]
        [StartingItem(0xC5CE73, 0x08)]
        [ItemName("Twinmold's Remains"), LocationName("Stone Tower Boss Warp"), Region(AreaTwinmoldsLair)]
        [GossipLocationHint("a masked evil"), GossipItemHint("an evil mask")]
        [ShopText("The remains of the boss in Stone Tower Temple.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x44B), ItemPool(ItemCategory.BossRemains, LocationCategory.BossFights, ClassicCategory.BossRemains)]
        RemainsTwinmold,

        // TODO handle time of day stuff with regard to item importance calculation
        // TODO move this to be with the other songs. Need to write a settings migrator first though.
        [Progressive]
        [StartingItem(0xC5CE70, 0x01)]
        [ItemName("Goron Lullaby Intro"), LocationName("Goron Elder"), MultiLocation(SongLullabyIntroInMountainVillage, SongLullabyIntroInTwinIslands), RegionArea(RegionArea.Mountain)]
        [GossipLocationHint("a thoughtful father", "an elder"), GossipItemHint("a soothing melody", "a father's lullaby")]
        [ShopText("The soothing melody of a thoughtful father.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x44E), ItemPool(ItemCategory.Songs, LocationCategory.NpcRewards, ClassicCategory.BaseItemPool)]
        SongLullabyIntro,



        [ItemName("Notebook: Bombers"), LocationName("Notebook Meeting: The Bombers"), MultiLocation(NotebookMeetBombersInNCT, NotebookMeetBombersInECT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a group of children", "a town game"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Bombers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x44F), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetBombers,


        [ItemName("Notebook: Anju"), LocationName("Notebook Meeting: Anju"), MultiLocation(NotebookMeetAnjuInInn, NotebookMeetAnjuInLaundryPool, NotebookMeetAnjuInRanch)]
        [GossipLocationHint("a lady in town"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Anju.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x450), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetAnju,


        [ItemName("Notebook: Kafei"), LocationName("Notebook Meeting: Kafei"), MultiLocation(NotebookMeetKafeiInLaundryPool, NotebookMeetKafeiInIkanaCanyon, NotebookMeetKafeiInInn)]
        [GossipLocationHint("a cursed man"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x451), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetKafei,


        [ItemName("Notebook: Man from Curiosity Shop"), LocationName("Notebook Meeting: The Curiosity Shop Man"), MultiLocation(NotebookMeetCuriosityShopManInWCT, NotebookMeetCuriosityShopManInLaundryPool), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Man from Curiosity Shop.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x452), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetCuriosityShopMan,


        [ItemName("Notebook: Old Lady from Bomb Shop"), LocationName("Notebook Meeting: The Old Lady"), MultiLocation(NotebookMeetOldLadyInNCT, NotebookMeetOldLadyInWCT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("an old lady"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Old Lady from Bomb Shop.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x453), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetOldLady,


        [ItemName("Notebook: Romani"), LocationName("Notebook Meeting: Romani"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch girl"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Romani.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x454), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetRomani,


        [ItemName("Notebook: Cremia"), LocationName("Notebook Meeting: Cremia"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch lady"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Cremia.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x455), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetCremia,


        [ItemName("Notebook: Mr. Dotour"), LocationName("Notebook Meeting: Mayor Dotour"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town leader", "an upstanding figure"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Mayor Dotour.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x456), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetMayorDotour,


        [ItemName("Notebook: Madame Aroma"), LocationName("Notebook Meeting: Madame Aroma"), Region(Region.EastClockTown)]
        [GossipLocationHint("an important lady", "an esteemed woman"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Madame Aroma.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x457), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetMadameAroma,


        [ItemName("Notebook: Toto"), LocationName("Notebook Meeting: Toto"), Region(Region.EastClockTown)]
        [GossipLocationHint("a band manager"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Toto.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x458), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetToto,


        [ItemName("Notebook: Gorman"), LocationName("Notebook Meeting: Gorman"), MultiLocation(NotebookMeetGormanInECT, NotebookMeetGormanInInn), RegionArea(RegionArea.Town)]
        [GossipLocationHint("an entertainer", "a miserable leader"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Gorman.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x459), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetGorman,


        [ItemName("Notebook: Postman"), LocationName("Notebook Meeting: The Postman"), MultiLocation(NotebookMeetPostmanInWCT, NotebookMeetPostmanInSCT, NotebookMeetPostmanInNCT, NotebookMeetPostmanInECT, NotebookMeetPostmanInInn, NotebookMeetPostmanInLaundryPool), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a hard worker", "a delivery person"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Postman.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45A), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetPostman,


        [ItemName("Notebook: Rosa Sisters"), LocationName("Notebook Meeting: The Rosa Sisters"), MultiLocation(NotebookMeetRosaSistersInWCT, NotebookMeetRosaSistersInInn), RegionArea(RegionArea.Town)]
        [GossipLocationHint("traveling sisters", "twin entertainers"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Rosa Sisters.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45B), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetRosaSisters,


        [ItemName("Notebook: ???"), LocationName("Notebook Meeting: The Toilet Hand"), Region(Region.StockPotInn)]
        [GossipLocationHint("a mystery appearance", "a strange palm"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Toilet Hand.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45C), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetToiletHand,


        [ItemName("Notebook: Anju's Grandmother"), LocationName("Notebook Meeting: Anju's Grandmother"), MultiLocation(NotebookMeetAnjusGrandmotherInInn, NotebookMeetAnjusGrandmotherInRanch)]
        [GossipLocationHint("an old lady"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Anju's Grandmother.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45D), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetAnjusGrandmother,


        [ItemName("Notebook: Kamaro"), LocationName("Notebook Meeting: Kamaro"), Region(Region.TerminaField)]
        [GossipLocationHint("a ghostly dancer", "a dancer"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Kamaro.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45E), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetKamaro,


        [ItemName("Notebook: Grog"), LocationName("Notebook Meeting: Grog"), Region(Region.RomaniRanch)]
        [GossipLocationHint("an ugly but kind heart", "a lover of chickens"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Grog.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x45F), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetGrog,


        [ItemName("Notebook: Gorman Brothers"), LocationName("Notebook Meeting: The Gorman Brothers"), Region(Region.MilkRoad)]
        [GossipLocationHint("shady brothers"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Gorman Brothers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x460), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetGormanBrothers,


        [ItemName("Notebook: Shiro"), LocationName("Notebook Meeting: Shiro"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a hidden soldier", "a stone circle"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Shiro.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x461), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetShiro,


        [ItemName("Notebook: Guru-Guru"), LocationName("Notebook Meeting: Guru-Guru"), MultiLocation(NotebookMeetGuruGuruInInn, NotebookMeetGuruGuruInLaundryPool), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a musician", "an entertainer"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting Guru-Guru.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x462), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMeetGuruGuru,


        [ItemName("Notebook: Received Room Key"), LocationName("Inn Reservation"), Region(Region.StockPotInn)]
        [GossipLocationHint("checking in", "check-in"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for the Inn Reservation.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x463), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookInnReservation,


        [ItemName("Notebook: Secret Night Meeting"), LocationName("Setting up Midnight Meeting"), Region(Region.StockPotInn)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising to meet Anju.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x464), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseAnjuMeeting,


        [ItemName("Notebook: Promised to meet Kafei"), LocationName("Midnight Meeting"), Region(Region.StockPotInn)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising Anju that you'll meet Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x465), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseAnjuDelivery,


        //GetItemIndex(0x466) // see frogs


        [ItemName("Notebook: Deposit Letter to Kafei"), LocationName("Depositing the Letter to Kafei"), MultiLocation(NotebookDepositLetterToKafeiInSCT, NotebookDepositLetterToKafeiInNCT, NotebookDepositLetterToKafeiInECT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for depositing the Letter to Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x467), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookDepositLetterToKafei,


        [ItemName("Notebook: Pendant of Memories"), LocationName("Kafei"), Region(Region.LaundryPool)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising Kafei that you'll return to Anju.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x468), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseKafei,


        [ItemName("Notebook: Delivered Pendant"), LocationName("Deliver the Pendant of Memories"), Region(Region.StockPotInn)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for delivering the Pendant of Memories.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x469), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookDeliverPendant,


        [ItemName("Notebook: Escaped from Sakon's Hideout"), LocationName("Escaping from Sakon's Hideout"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for escaping Sakon's Hideout.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46A), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookEscapeFromSakonSHideout,


        [ItemName("Notebook: Became ranch hand"), LocationName("Romani's Game"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising Romani that you'll help defend against the 'them'.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46B), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseRomani,


        [ItemName("Notebook: Saved cows from \"them\""), LocationName("Aliens Defense"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry"), GossipCompetitiveHint(-2)]
        [ShopText("The Bombers' Notebook entry for saving the cows.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46C), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookSaveTheCows,


        //GetItemIndex(0x46D) // see frogs


        [ItemName("Notebook: Protected milk delivery"), LocationName("Cremia"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry"), GossipCompetitiveHint]
        [ShopText("The Bombers' Notebook entry for protecting Cremia.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46E), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookProtectMilkDelivery,


        //GetItemIndex(0x46F) // see frogs


        [ItemName("Notebook: Keaton Mask"), LocationName("Curiosity Shop Man #1"), Region(Region.LaundryPool)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for speaking with the Curiosity Shop man about Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x470), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookCuriosityShopManSGift,


        [ItemName("Notebook: Letter to Mama"), LocationName("Curiosity Shop Man #2"), Region(Region.LaundryPool)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising to deliver the Express Mail.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x471), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseCuriosityShopMan,


        [ItemName("Notebook: Chateau Romani"), LocationName("Madame Aroma in Bar"), Region(Region.EastClockTown)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for delivering the Letter to Mama.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x472), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookDeliverLetterToMama,


        //GetItemIndex(0x473) // see frogs


        [ItemName("Notebook: Bombers' Notebook"), LocationName("Bombers' Hide and Seek"), MultiLocation(NotebookLearnBombersCodeInNCT, NotebookLearnBombersCodeInECT), RegionArea(RegionArea.Town)]
        [GossipLocationHint("a group of children", "a town game"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for the Bombers' Hideout code.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x474), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookLearnBombersCode,


        [ItemName("Notebook: Dotour's Thanks"), LocationName("Mayor"), Region(Region.EastClockTown)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for ending the Mayor's meeting.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x475), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookDotoursThanks,


        [ItemName("Notebook: Rosa Sisters' Thanks"), LocationName("Rosa Sisters"), Region(Region.WestClockTown)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for teaching the Rosa Sisters to dance.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x476), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookRosaSistersThanks,


        [ItemName("Notebook: Thanks for the paper"), LocationName("Toilet Hand"), Region(Region.StockPotInn)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for giving paper to the hand.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x477), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookToiletHandSThanks,


        [ItemName("Notebook: Reading Prize 1"), LocationName("Grandma Short Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for staying awake for 2 hours.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x478), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookGrandmaShortStory,


        [ItemName("Notebook: Reading Prize 2"), LocationName("Grandma Long Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for staying awake until morning.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x479), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookGrandmaLongStory,


        [ItemName("Notebook: Training Award"), LocationName("Postman's Game"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for counting to 10 seconds.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x47A), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPostmansGame,


        [ItemName("Notebook: Kafei's Mask"), LocationName("Madame Aroma in Office"), Region(Region.EastClockTown)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for promising to find Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [HackContent(nameof(Resources.mods.fix_notebook_madame_aroma), false)]
        [GetItemIndex(0x47B), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseMadameAroma,


        [ItemName("Notebook: All-Night Mask"), LocationName("All-Night Mask Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady deal"), GossipItemHint("a diary entry"), GossipCompetitiveHint]
        [ShopText("The Bombers' Notebook entry for buying the item from the Curiosity Shop on the final night.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x47C), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPurchaseCuriosityShopItem,


        [ItemName("Notebook: Bunny Hood"), LocationName("Grog"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for growing the baby cuccos.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x47D), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookGrogsThanks,


        [ItemName("Notebook: Garo's Mask"), LocationName("Gorman Bros Race"), Region(Region.MilkRoad)]
        [GossipLocationHint("a sporting event"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for defeating the Gorman Brothers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x47E), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookDefeatGormanBrothers,


        [ItemName("Notebook: Circus Leader's Mask"), LocationName("Gorman"), Region(Region.EastClockTown)]
        [GossipLocationHint("a moving performance"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for moving Gorman with a performance.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x47F), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookMovingGorman,


        [ItemName("Notebook: Postman's Hat"), LocationName("Postman's Freedom Reward"), Region(Region.EastClockTown)]
        [GossipLocationHint("a special delivery", "one last job"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for allowing the Postman to flee.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x480), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPostmansFreedom,


        [ItemName("Notebook: Couple's Mask"), LocationName("Anju and Kafei"), Region(Region.StockPotInn)]
        [GossipLocationHint("a reunion", "a lovers' reunion"), GossipItemHint("a diary entry"), GossipCompetitiveHint(3)]
        [ShopText("The Bombers' Notebook entry for reuniting Anju and Kafei.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x481), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookUniteAnjuAndKafei,


        [ItemName("Notebook: Blast Mask"), LocationName("Old Lady"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for saving the Old Lady.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x482), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookSaveOldLady,


        [ItemName("Notebook: Kamaro's Mask"), LocationName("Kamaro"), Region(Region.TerminaField)]
        [GossipLocationHint("a promise"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Bombers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x483), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookPromiseKamaro,


        [ItemName("Notebook: Stone Mask"), LocationName("Invisible Soldier"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a good deed"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Bombers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x484), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookSaveInvisibleSoldier,


        [ItemName("Notebook: Bremen Mask"), LocationName("Guru-Guru"), Region(Region.LaundryPool)]
        [GossipLocationHint("a musician", "an entertainer"), GossipItemHint("a diary entry")]
        [ShopText("The Bombers' Notebook entry for meeting the Bombers.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x485), ItemPool(ItemCategory.NotebookEntries, LocationCategory.NotebookEntries, ClassicCategory.NotebookEntries)]
        NotebookGuruGuru,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Castle Courtyard Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x486), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xEBE)]
        CollectableAncientCastleOfIkanaCastleExteriorPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Castle Hole Room Pot 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x487), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xECE)]
        CollectableAncientCastleOfIkanaHoleRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Castle Hole Room Pot 4"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x488), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xED0)]
        CollectableAncientCastleOfIkanaHoleRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Great Bay Coast Pot 8"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x489), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1BAB)]
        CollectableGreatBayCoastPot11,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Great Bay Temple Entrance Room Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48A), ItemPool(ItemCategory.Fairy, LocationCategory.Barrels, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x24B3)]
        CollectableGreatBayTempleEntranceRoomBarrel1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Canyon Cleared Grass 4"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48B), ItemPool(ItemCategory.Fairy, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9A3)]
        CollectableIkanaCanyonMainAreaGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Canyon Grass 4"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48C), ItemPool(ItemCategory.Fairy, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x9C9)]
        CollectableIkanaCanyonMainAreaGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Milk Road Grass 3"), Region(Region.MilkRoad)]
        [GossipLocationHint("a roadside plant"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48D), ItemPool(ItemCategory.Fairy, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1120)]
        CollectableMilkRoadGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Pot"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48E), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2D26)]
        CollectableMountainVillageSpringPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Snowball 4"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x48F), ItemPool(ItemCategory.Fairy, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2D2B)]
        CollectableMountainVillageSpringSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Winter Pot"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x490), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2820)]
        CollectableMountainVillageWinterPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Winter Small Snowball 6"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x491), ItemPool(ItemCategory.Fairy, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2836)]
        CollectableMountainVillageWinterSmallSnowball8,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Road to Ikana Pot"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a jar on a rock"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x492), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x29A0)]
        CollectableRoadToIkanaPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Secret Shrine Underwater Pot 5"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x493), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x307A)]
        CollectableSecretShrineMainRoomPot5,



        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Snowhead Small Snowball 4"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x494), ItemPool(ItemCategory.Fairy, LocationCategory.SmallSnowballs, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2E22)]
        CollectableSnowheadSmallSnowball10,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Snowhead Main Room Pot"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x495), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x10A7)]
        CollectableSnowheadTempleMainRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Cleared Swamp Owl Grass 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x496), ItemPool(ItemCategory.Fairy, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x27)]
        CollectableSouthernSwampClearCentralSwampGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Southern Swamp Owl Grass 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x497), ItemPool(ItemCategory.Fairy, LocationCategory.Grass, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x22AD)]
        CollectableSouthernSwampPoisonedCentralSwampGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Stone Tower Lower Scarecrow Pot 4"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x498), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C23)]
        CollectableStoneTowerPot11,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Stone Tower Lower Scarecrow Pot 5"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x499), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C25)]
        CollectableStoneTowerPot12,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Stone Tower Upper Scarecrow Pot 5"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x49A), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C2C)]
        CollectableStoneTowerPot13,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Stone Tower Owl Pot 4"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x49B), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2C39)]
        CollectableStoneTowerPot14,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Inverted Stone Tower Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x49C), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2CB9)]
        CollectableStoneTowerInvertedStoneTowerFlippedPot3,

        // [GetItemIndex(0x49D)] // Removed


        // [GetItemIndex(0x49E)] // Removed


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Stone Tower Temple Wizzrobe Pot"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x49F), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xC21)]
        CollectableStoneTowerTempleInvertedWizzrobeRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field Pillar Pot"), Region(Region.TerminaField)]
        [GossipLocationHint("a jar on a pillar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A0), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x16A0)]
        CollectableTerminaFieldPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Woodfall Pot 3"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned platform"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A1), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x2321)]
        CollectableWoodfallPot3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Woodfall Temple Entrance Pot"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A2), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0xDA1)]
        CollectableWoodfallTempleEntranceRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Cape Owl Pot 4"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A3), ItemPool(ItemCategory.Fairy, LocationCategory.Jars, ClassicCategory.FixedMinorItemDrops), CollectableIndex(0x1C24)]
        CollectableZoraCapePot5,



        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Cucco Shack Gossip Fairy"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A4), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableCuccoShackGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Doggy Racetrack Gossip Fairy"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A5), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableDoggyRacetrackGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Great Bay Coast Gossip Fairy"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A6), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableGreatBayCoastGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Gossip Grotto Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A7), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableGrottosOceanGossipStonesGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Canyon Dock Gossip Fairy"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A8), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableIkanaCanyonMainAreaGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Canyon Spirit House Gossip Fairy"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4A9), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableIkanaCanyonMainAreaGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ikana Canyon Ravine Gossip Fairy"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AA), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableIkanaCanyonSakonSHideoutAreaGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Milk Road Gossip Fairy"), Region(Region.MilkRoad)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AB), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableMilkRoadGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Path Gossip Fairy"), Region(Region.MountainVillage)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AC), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableMountainVillageSpringPathToGoronGraveyardGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Frog Gossip Fairy"), Region(Region.MountainVillage)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AD), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableMountainVillageSpringGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Path to Mountain Village Gossip Fairy"), Region(Region.PathToMountainVillage)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AE), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectablePathToMountainVillageGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Road to Ikana Gossip Fairy"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4AF), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableRoadToIkanaGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Road to Southern Swamp Gossip Fairy"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B0), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableRoadToSouthernSwampGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ranch Entrance Gossip Fairy"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B1), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableRomaniRanchGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ranch Tree Gossip Fairy"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B2), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableRomaniRanchGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Ranch Barn Gossip Fairy"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B3), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableRomaniRanchGossipFairy3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Southern Swamp Gossip Fairy"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B4), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Swamp Spider House Gossip Fairy"), Region(Region.SwampSpiderHouseItems)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B5), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableSwampSpiderHouseTreeRoomGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field Observatory Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B6), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field East Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B7), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field South Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B8), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field North Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4B9), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field West Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BA), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy5,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field Milk Gossip Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BB), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTerminaFieldGossipFairy6,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Deku Trial Left Closer Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BC), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonDekuTrialDekuTrialGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Deku Trial Right Closer Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BD), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonDekuTrialDekuTrialGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Deku Trial Left Middle Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BE), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonDekuTrialDekuTrialGossipFairy3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Deku Trial Right Middle Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4BF), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonDekuTrialDekuTrialGossipFairy4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Deku Trial Right Further Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C0), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonDekuTrialDekuTrialGossipFairy5,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Goron Trial End Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C1), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonGoronTrialGoronTrialGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Goron Trial Second Set Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C2), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonGoronTrialGoronTrialGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Goron Trial Second Set Gossip Fairy 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C3), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonGoronTrialGoronTrialGossipFairy3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Goron Trial First Set Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C4), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonGoronTrialGoronTrialGossipFairy4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Goron Trial First Set Gossip Fairy 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C5), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonGoronTrialGoronTrialGossipFairy5,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Link Trial First Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C6), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonLinkTrialGossipStoneRoom1GossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Link Trial Second Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C7), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonLinkTrialGossipStoneRoom2GossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Link Trial Iron Knuckle Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C8), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonLinkTrialIronKnuckleBattleGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Link Trial Iron Knuckle Fairy 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4C9), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonLinkTrialIronKnuckleBattleGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Link Trial Last Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CA), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonLinkTrialPieceOfHeartRoomGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Trial Gossip Fairy"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CB), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonZoraTrialZoraTrialGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Trial Gossip Fairy 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CC), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonZoraTrialZoraTrialGossipFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Trial Gossip Fairy 3"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CD), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonZoraTrialZoraTrialGossipFairy3,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Trial Gossip Fairy 4"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CE), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonZoraTrialZoraTrialGossipFairy4,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Trial Gossip Fairy 5"), Region(Region.TheMoon)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4CF), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableTheMoonZoraTrialZoraTrialGossipFairy5,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Zora Cape Gossip Fairy"), Region(Region.ZoraCape)]
        [GossipLocationHint("a summoning"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D0), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableZoraCapeGossipFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Great Bay Coast Butterfly Fairy"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D1), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableGreatBayCoastButterflyFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field West Gossip Grotto Butterfly Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D2), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableGrottosOceanGossipStonesButterflyFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Bean Man Butterfly Fairy"), Region(Region.DekuPalace)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D3), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableGrottosMagicBeanSellerSGrottoButterflyFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Termina Field Cow Grotto Butterfly Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D4), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableGrottosCowGrottoButterflyFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Great Bay Coast Cow Grotto Butterfly Fairy"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D5), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableGrottosCowGrottoButterflyFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Butterfly Fairy"), Region(Region.MountainVillage)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D6), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableMountainVillageWinterMountainVillageSpringButterflyFairy1,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Mountain Village Spring Butterfly Fairy 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D7), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableMountainVillageWinterMountainVillageSpringButterflyFairy2,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Near Peahat Grotto Butterfly Fairy"), Region(Region.TerminaField)]
        [GossipLocationHint("a transformation"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D8), ItemPool(ItemCategory.Fairy, LocationCategory.Butterflies, ClassicCategory.Butterflies), NullableItem]
        CollectableTerminaFieldButterflyFairy1,

        [Repeatable, Temporary]
        [ItemName("Cyan Frog"), LocationName("Frog in Woodfall Temple"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("an amphibian"), GossipItemHint("a choir member")]
        [ShopText("The cyan frog from Don Gero's frog choir.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x466), ItemPool(ItemCategory.Frogs, LocationCategory.Frogs, ClassicCategory.Frogs), NullableItem]
        [ExclusiveItemMessage(0x9003, "\u0017You found the \u0005Cyan Frog\u0000!\u0018\u0011It will wait for you in the mountains.\u00BF")]
        FrogWoodfallTemple,

        [Repeatable, Temporary]
        [ItemName("Pink Frog"), LocationName("Frog in Great Bay Temple"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("an amphibian"), GossipItemHint("a choir member")]
        [ShopText("The pink frog from Don Gero's frog choir.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46D), ItemPool(ItemCategory.Frogs, LocationCategory.Frogs, ClassicCategory.Frogs), NullableItem]
        [ExclusiveItemMessage(0x9004, "\u0017You found the \u0006Pink Frog\u0000!\u0018\u0011It will wait for you in the mountains.\u00BF")]
        FrogGreatBayTemple,

        [Repeatable, Temporary]
        [ItemName("Blue Frog"), LocationName("Frog in the Swamp"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("an amphibian"), GossipItemHint("a choir member")]
        [ShopText("The blue frog from Don Gero's frog choir.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x46F), ItemPool(ItemCategory.Frogs, LocationCategory.Frogs, ClassicCategory.Frogs), NullableItem]
        [ExclusiveItemMessage(0x9005, "\u0017You found the \u0003Blue Frog\u0000!\u0018\u0011It will wait for you in the mountains.\u00BF")]
        FrogSwamp,

        [Repeatable, Temporary]
        [ItemName("White Frog"), LocationName("Frog in the Laundry Pool"), Region(Region.LaundryPool)]
        [GossipLocationHint("an amphibian"), GossipItemHint("a choir member")]
        [ShopText("The white frog from Don Gero's frog choir.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x473), ItemPool(ItemCategory.Frogs, LocationCategory.Frogs, ClassicCategory.Frogs), NullableItem]
        [ExclusiveItemMessage(0x9006, "\u0017You found the \u0001White Frog\u0000!\u0018\u0011It will wait for you in the mountains.\u00BF")]
        FrogLaundryPool,


        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 1"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4D9), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy1,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 2"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DA), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy2,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 3"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DB), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy3,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 4"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DC), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy4,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 5"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DD), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy5,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 6"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DE), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy6,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 7"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4DF), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy7,

        [Visible]
        [Repeatable]
        [ItemName("Fairy"), LocationName("Well Fountain Fairy 8"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a fountain"), GossipItemHint("a winged friend", "a healer")]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x4E0), ItemPool(ItemCategory.Fairy, LocationCategory.Fairies, ClassicCategory.Fairies), NullableItem]
        CollectableWellFountainFairy8,




        //multilocation items
        [MainLocation(ItemTingleMapTown), Region(Region.NorthClockTown)]
        ItemTingleMapTownInTown,

        [MainLocation(ItemTingleMapTown), Region(Region.IkanaCanyon)]
        ItemTingleMapTownInCanyon,

        [MainLocation(ItemTingleMapWoodfall), Region(Region.RoadToSouthernSwamp)]
        ItemTingleMapWoodfallInSwamp,

        [MainLocation(ItemTingleMapWoodfall), Region(Region.NorthClockTown)]
        ItemTingleMapWoodfallInTown,

        [MainLocation(ItemTingleMapSnowhead), Region(Region.TwinIslands)]
        ItemTingleMapSnowheadInMountain,

        [MainLocation(ItemTingleMapSnowhead), Region(Region.RoadToSouthernSwamp)]
        ItemTingleMapSnowheadInSwamp,

        [MainLocation(ItemTingleMapRanch), Region(Region.MilkRoad)]
        ItemTingleMapRanchInRanch,

        [MainLocation(ItemTingleMapRanch), Region(Region.TwinIslands)]
        ItemTingleMapRanchInMountain,

        [MainLocation(ItemTingleMapGreatBay), Region(Region.GreatBayCoast)]
        ItemTingleMapGreatBayInOcean,

        [MainLocation(ItemTingleMapGreatBay), Region(Region.MilkRoad)]
        ItemTingleMapGreatBayInRanch,

        [MainLocation(ItemTingleMapStoneTower), Region(Region.IkanaCanyon)]
        ItemTingleMapStoneTowerInCanyon,

        [MainLocation(ItemTingleMapStoneTower), Region(Region.GreatBayCoast)]
        ItemTingleMapStoneTowerInOcean,

        [MainLocation(HeartPiecePostBox), Region(Region.SouthClockTown)]
        HeartPiecePostBoxInSCT,

        [MainLocation(HeartPiecePostBox), Region(Region.NorthClockTown)]
        HeartPiecePostBoxInNCT,

        [MainLocation(HeartPiecePostBox), Region(Region.EastClockTown)]
        HeartPiecePostBoxInECT,

        [MainLocation(HeartPieceKeatonQuiz), Region(Region.NorthClockTown)]
        HeartPieceKeatonQuizInNCT,

        [MainLocation(HeartPieceKeatonQuiz), Region(Region.MilkRoad)]
        HeartPieceKeatonQuizInMilkRoad,

        [MainLocation(HeartPieceKeatonQuiz), Region(Region.MountainVillage)]
        HeartPieceKeatonQuizInMountainVillage,

        [MainLocation(SongOath), Region(AreaOdolwasLair)]
        SongOathInWFT,

        [MainLocation(SongOath), Region(AreaGohtsLair)]
        SongOathInSHT,

        [MainLocation(SongOath), Region(AreaGyorgsLair)]
        SongOathInGBT,

        [MainLocation(SongOath), Region(AreaTwinmoldsLair)]
        SongOathInISTT,

        [MainLocation(ShopItemGoronBomb10), Region(Region.GoronVillage)]
        ShopItemGoronBomb10InWinter,

        [MainLocation(ShopItemGoronBomb10), Region(Region.GoronVillage)]
        ShopItemGoronBomb10InSpring,

        [MainLocation(ShopItemGoronArrow10), Region(Region.GoronVillage)]
        ShopItemGoronArrow10InWinter,

        [MainLocation(ShopItemGoronArrow10), Region(Region.GoronVillage)]
        ShopItemGoronArrow10InSpring,

        [MainLocation(ShopItemGoronRedPotion), Region(Region.GoronVillage)]
        ShopItemGoronRedPotionInWinter,

        [MainLocation(ShopItemGoronRedPotion), Region(Region.GoronVillage)]
        ShopItemGoronRedPotionInSpring,

        [MainLocation(ShopItemBusinessScrubMagicBean), Region(Region.SouthernSwamp)]
        ShopItemBusinessScrubMagicBeanInSwamp,

        [MainLocation(ShopItemBusinessScrubMagicBean), Region(Region.SouthClockTown)]
        ShopItemBusinessScrubMagicBeanInTown,

        [MainLocation(UpgradeBiggestBombBag), Region(Region.GoronVillage)]
        UpgradeBiggestBombBagInMountain,

        [MainLocation(UpgradeBiggestBombBag), Region(Region.SouthernSwamp)]
        UpgradeBiggestBombBagInSwamp,

        [MainLocation(ShopItemBusinessScrubGreenPotion), Region(Region.ZoraHall)]
        ShopItemBusinessScrubGreenPotionInOcean,

        [MainLocation(ShopItemBusinessScrubGreenPotion), Region(Region.GoronVillage)]
        ShopItemBusinessScrubGreenPotionInMountain,

        [MainLocation(ShopItemBusinessScrubBluePotion), Region(Region.IkanaCanyon)]
        ShopItemBusinessScrubBluePotionInCanyon,

        [MainLocation(ShopItemBusinessScrubBluePotion), Region(Region.ZoraHall)]
        ShopItemBusinessScrubBluePotionInOcean,

        [MainLocation(CollectibleStrayFairyClockTown), Region(Region.LaundryPool)]
        CollectibleStrayFairyClockTownInLaundryPool,

        [MainLocation(CollectibleStrayFairyClockTown), Region(Region.EastClockTown)]
        CollectibleStrayFairyClockTownInECT,

        [MainLocation(SongLullabyIntro), Region(Region.MountainVillage)]
        SongLullabyIntroInMountainVillage,

        [MainLocation(SongLullabyIntro), Region(Region.TwinIslands)]
        SongLullabyIntroInTwinIslands,


        [MainLocation(NotebookMeetBombers), Region(Region.NorthClockTown)]
        NotebookMeetBombersInNCT,

        [MainLocation(NotebookMeetBombers), Region(Region.EastClockTown)]
        NotebookMeetBombersInECT,



        [MainLocation(NotebookMeetAnju), Region(Region.StockPotInn)]
        NotebookMeetAnjuInInn,

        [MainLocation(NotebookMeetAnju), Region(Region.LaundryPool)]
        NotebookMeetAnjuInLaundryPool,

        [MainLocation(NotebookMeetAnju), Region(Region.RomaniRanch)]
        NotebookMeetAnjuInRanch,



        [MainLocation(NotebookMeetKafei), Region(Region.LaundryPool)]
        NotebookMeetKafeiInLaundryPool,

        [MainLocation(NotebookMeetKafei), Region(Region.IkanaCanyon)]
        NotebookMeetKafeiInIkanaCanyon,

        [MainLocation(NotebookMeetKafei), Region(Region.StockPotInn)]
        NotebookMeetKafeiInInn,



        [MainLocation(NotebookMeetCuriosityShopMan), Region(Region.WestClockTown)]
        NotebookMeetCuriosityShopManInWCT,

        [MainLocation(NotebookMeetCuriosityShopMan), Region(Region.LaundryPool)]
        NotebookMeetCuriosityShopManInLaundryPool,



        [MainLocation(NotebookMeetOldLady), Region(Region.NorthClockTown)]
        NotebookMeetOldLadyInNCT,

        [MainLocation(NotebookMeetOldLady), Region(Region.WestClockTown)]
        NotebookMeetOldLadyInWCT,



        [MainLocation(NotebookMeetGorman), Region(Region.EastClockTown)]
        NotebookMeetGormanInECT,

        [MainLocation(NotebookMeetGorman), Region(Region.StockPotInn)]
        NotebookMeetGormanInInn,



        [MainLocation(NotebookMeetPostman), Region(Region.WestClockTown)]
        NotebookMeetPostmanInWCT,

        [MainLocation(NotebookMeetPostman), Region(Region.SouthClockTown)]
        NotebookMeetPostmanInSCT,

        [MainLocation(NotebookMeetPostman), Region(Region.NorthClockTown)]
        NotebookMeetPostmanInNCT,

        [MainLocation(NotebookMeetPostman), Region(Region.EastClockTown)]
        NotebookMeetPostmanInECT,

        [MainLocation(NotebookMeetPostman), Region(Region.StockPotInn)]
        NotebookMeetPostmanInInn,

        [MainLocation(NotebookMeetPostman), Region(Region.LaundryPool)]
        NotebookMeetPostmanInLaundryPool,



        [MainLocation(NotebookMeetRosaSisters), Region(Region.WestClockTown)]
        NotebookMeetRosaSistersInWCT,

        [MainLocation(NotebookMeetRosaSisters), Region(Region.StockPotInn)]
        NotebookMeetRosaSistersInInn,



        [MainLocation(NotebookMeetAnjusGrandmother), Region(Region.StockPotInn)]
        NotebookMeetAnjusGrandmotherInInn,

        [MainLocation(NotebookMeetAnjusGrandmother), Region(Region.RomaniRanch)]
        NotebookMeetAnjusGrandmotherInRanch,



        [MainLocation(NotebookMeetGuruGuru), Region(Region.StockPotInn)]
        NotebookMeetGuruGuruInInn,

        [MainLocation(NotebookMeetGuruGuru), Region(Region.LaundryPool)]
        NotebookMeetGuruGuruInLaundryPool,



        [MainLocation(NotebookDepositLetterToKafei), Region(Region.SouthClockTown)]
        NotebookDepositLetterToKafeiInSCT,

        [MainLocation(NotebookDepositLetterToKafei), Region(Region.NorthClockTown)]
        NotebookDepositLetterToKafeiInNCT,

        [MainLocation(NotebookDepositLetterToKafei), Region(Region.EastClockTown)]
        NotebookDepositLetterToKafeiInECT,



        [MainLocation(NotebookLearnBombersCode), Region(Region.NorthClockTown)]
        NotebookLearnBombersCodeInNCT,

        [MainLocation(NotebookLearnBombersCode), Region(Region.EastClockTown)]
        NotebookLearnBombersCodeInECT,



        GossipTerminaSouth,
        GossipSwampPotionShop,
        GossipMountainSpringPath,
        GossipMountainPath,
        GossipOceanZoraGame,
        GossipCanyonRoad,
        GossipCanyonDock,
        GossipCanyonSpiritHouse,
        GossipTerminaMilk,
        GossipTerminaWest,
        GossipTerminaNorth,
        GossipTerminaEast,
        GossipRanchTree,
        GossipRanchBarn,
        GossipMilkRoad,
        GossipOceanFortress,
        GossipSwampRoad,
        GossipTerminaObservatory,
        GossipRanchCuccoShack,
        GossipRanchRacetrack,
        GossipRanchEntrance,
        GossipCanyonRavine,
        GossipMountainSpringFrog,
        GossipSwampSpiderHouse,
        GossipTerminaGossipLarge,
        GossipTerminaGossipGuitar,
        GossipTerminaGossipPipes,
        GossipTerminaGossipDrums,

        HintGaroCanyonLower1,
        HintGaroCanyonLower2,
        HintGaroWithIgosDefeated,
        HintGaroCanyonUpper1,
        HintGaroCanyonUpper2,
        HintGaroCanyonUpper3,
        HintGaroCanyonUpper4,
        HintGaroCanyonUpper1WithStorms,
        HintGaroCanyonUpper2WithStorms,
        HintGaroCanyonUpper3WithStorms,
        HintGaroCanyonUpper4WithStorms,
        HintGaroCastleLower1,
        HintGaroCastleLower2,
        HintGaroCastleLower3,
        HintGaroCastleUpper,
        HintGaroMaster,

        [Repeatable]
        [ItemName("Ice Trap")]
        [GossipItemHint("a cold surprise", "an icy breeze")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [ExclusiveItem(0xB0)]
        [ExclusiveItemGraphic(0, 0)]
        [ExclusiveItemMessage(0x9000, "\u0017You are a \u0003FOOL\u0000!\u0018\u00BF")]
        IceTrap = -1,

        [ItemName("Recovery Heart")]
        [GossipItemHint("health")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [ShopText("Replenishes a small amount of your life energy.")]
        [GetItemIndex(0x0A), ItemPool(ItemCategory.Fake, LocationCategory.Fake, ClassicCategory.Fake)]
        RecoveryHeart = -2,

        [Repeatable]
        [ItemName("Bomb Trap")]
        [GossipItemHint("a startling kaboom")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [ExclusiveItem(0xB3)]
        [ExclusiveItemGraphic(0, 0)]
        [ExclusiveItemMessage(0x8FFF, "\u0017get rekt\u0018\u00BF")]
        BombTrap = -3,

        [Repeatable]
        [ItemName("Rupoor")]
        [GossipItemHint("a sad surprise")]
        [ShopText("This is worth -10 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [ExclusiveItem(0xB5)]
        [ExclusiveItemGraphic(0xAB, 0x13F)]
        [ExclusiveItemMessage(0x8FFE, "\u0017You picked up a \u0007Rupoor\u0000!\u0018\u0011That means you've lost \u000110 Rupees\u0000.\u0011And that's a little bit sad.\u00BF")]
        Rupoor = -4,

        [Repeatable]
        [ItemName("Nothing")]
        [GossipItemHint("nothing")]
        [ShopText("Literally nothing.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [ExclusiveItem(0xB6, type: 2)]
        [ExclusiveItemGraphic(0, 0)]
        [ExclusiveItemMessage(0x8FFD, "\u0017You found \u0001literally nothing\u0000!\u0018\u00BF")]
        Nothing = -5,

    }
}
