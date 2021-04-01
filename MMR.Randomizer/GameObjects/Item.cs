using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.GameObjects
{
    public enum Item
    {
        // free
        [StartingItem(0xC5CE41, 0x32)]
        [ItemName("Deku Mask"), LocationName("Starting Item"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a woodland spirit")]
        [ShopText("Wear it to assume Deku form.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x78), ItemCategory(ItemCategory.None)]
        MaskDeku,

        // items
        [Progressive]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x01)]
        [ItemName("Hero's Bow"), LocationName("Hero's Bow Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a projectile", "a ranged weapon")]
        [ShopText("Use it to shoot arrows.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02223000 + 0xAA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x22), ItemCategory(ItemCategory.None)]
        ItemBow,

        [StartingItem(0xC5CE26, 0x02)]
        [ItemName("Fire Arrow"), LocationName("Fire Arrow Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("the power of fire", "a magical item")]
        [ShopText("Arm your bow with arrows that burst into flame.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02336000 + 0xCA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x25), ItemCategory(ItemCategory.None)]
        ItemFireArrow,

        [StartingItem(0xC5CE27, 0x03)]
        [ItemName("Ice Arrow"), LocationName("Ice Arrow Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("the power of ice", "a magical item")]
        [ShopText("Arm your bow with arrows that freeze.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0292F000 + 0x11E, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x26), ItemCategory(ItemCategory.None)]
        ItemIceArrow,

        [StartingItem(0xC5CE28, 0x04)]
        [ItemName("Light Arrow"), LocationName("Light Arrow Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("the power of light", "a magical item")]
        [ShopText("Arm your bow with arrows infused with sacred light.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0212B000 + 0xB2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02192000 + 0x8E)]
        [GetItemIndex(0x27), ItemCategory(ItemCategory.None)]
        ItemLightArrow,

        [Progressive]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x08)]
        [ItemName("Bomb Bag"), LocationName("Bomb Bag Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopRoom(ShopRoomAttribute.Room.BombShop, 0x48)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 0)]
        [ShopText("This can hold up to a maximum of 20 bombs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1B), ItemCategory(ItemCategory.ShopItems)]
        ItemBombBag,

        [Repeatable, Temporary]
        //[StartingItem(0xC5CE2E, 0x0A)]
        [ItemName("Magic Bean"), LocationName("Bean Man"), Region(Region.DekuPalace)]
        [GossipLocationHint("a hidden merchant", "a gorging merchant"), GossipItemHint("a plant seed")]
        [ShopText("Plant it in soft soil.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11E), ItemCategory(ItemCategory.None)]
        ItemMagicBean,

        [Repeatable]
        //[StartingItem(0xC5CE30, 0x0C)]
        [ItemName("Powder Keg"), LocationName("Powder Keg Challenge"), Region(Region.GoronVillage)]
        [GossipLocationHint("a large goron"), GossipItemHint("gunpowder", "a dangerous item", "an explosive barrel")]
        [ShopText("Both its power and its size are immense!")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x123), ItemCategory(ItemCategory.None)]
        ItemPowderKeg,

        [StartingItem(0xC5CE31, 0x0D)]
        [ItemName("Pictobox"), LocationName("Koume"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a witch"), GossipItemHint("a light recorder", "a capture device")]
        [ShopText("Use it to snap pictographs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x43), ItemCategory(ItemCategory.None)]
        ItemPictobox,

        [StartingItem(0xC5CE32, 0x0E)]
        [ItemName("Lens of Truth"), LocationName("Lens of Truth Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("eyeglasses", "the truth", "focused vision")]
        [ShopText("Uses magic to see what the naked eye cannot.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02EB8000 + 0x9A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x42), ItemCategory(ItemCategory.None)]
        ItemLens,

        [StartingItem(0xC5CE33, 0x0F)]
        [ItemName("Hookshot"), LocationName("Hookshot Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a chain and grapple")]
        [ShopText("Use it to grapple objects.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0238B000 + 0x14A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x41), ItemCategory(ItemCategory.None)]
        ItemHookshot,

        [Progressive]
        [StartingItem(0xC5CDED, 0x30)]
        [StartingItem(0xC5CDF4, 0x01)]
        [ItemName("Magic Power"), LocationName("Town Great Fairy Non-Human"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a magical being"), GossipItemHint("magic power")]
        [ShopText("Grants the ability to use magic.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12C), ItemCategory(ItemCategory.GreatFairyRewards)]
        FairyMagic,
        
        [StartingItemId(0x9C)]
        [ItemName("Spin Attack"), LocationName("Woodfall Great Fairy"), Region(Region.Woodfall)]
        [GossipLocationHint("a magical being"), GossipItemHint("a magic attack"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false)]
        [ShopText("Increases the power of your spin attack.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12D), ItemCategory(ItemCategory.GreatFairyRewards)]
        FairySpinAttack,

        [Progressive]
        [StartingItem(0xC5CDED, 0x60)]
        [StartingItem(0xC5CDF4, 0x01)]
        [StartingItem(0xC5CDF5, 0x01)]
        [ItemName("Extended Magic Power"), LocationName("Snowhead Great Fairy"), Region(Region.Snowhead)]
        [GossipLocationHint("a magical being"), GossipItemHint("magic power"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false)]
        [ShopText("Grants the ability to use lots of magic.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12E), ItemCategory(ItemCategory.GreatFairyRewards)]
        FairyDoubleMagic,

        [StartingItem(0xC5CDF6, 0x01)]
        [StartingItem(0xC5CE87, 0x14)]
        [ItemName("Double Defense"), LocationName("Ocean Great Fairy"), Region(Region.ZoraCape)]
        [GossipLocationHint("a magical being"), GossipItemHint("magical defense"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false)]
        [ShopText("Take half as much damage from enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x12F), ItemCategory(ItemCategory.GreatFairyRewards)]
        FairyDoubleDefense,

        [StartingItem(0xC5CE34, 0x10)]
        [ItemName("Great Fairy's Sword"), LocationName("Ikana Great Fairy"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a magical being"), GossipItemHint("a black rose", "a powerful blade"), GossipCompetitiveHint(4, ItemCategory.StrayFairies, false)]
        [ShopText("The most powerful sword has black roses etched in its blade.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x130), ItemCategory(ItemCategory.GreatFairyRewards)]
        ItemFairySword,

        //[StartingItem(0xC5CE36, 0x12)]
        [Repeatable, Temporary, Overwritable] // specially handled to turn into Red Potion on subsequent times
        [ItemName("Bottle with Red Potion"), LocationName("Kotake"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("the sleeping witch"), GossipItemHint("a vessel of health", "bottled fortitude")]
        [ShopText("Replenishes your life energy.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x59), ItemCategory(ItemCategory.None)]
        ItemBottleWitch,

        //[StartingItem(0xC5CE37, 0x12)]
        [Repeatable, Temporary, Overwritable] // specially handled to turn into Milk on subsequent times
        [ItemName("Bottle with Milk"), LocationName("Aliens Defense"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch girl", "a good deed"), GossipItemHint("a dairy product", "the produce of cows")]
        [GossipCombineOrder(0), GossipCombine(MaskRomani, "Ranch Sisters Defense")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x60), ItemCategory(ItemCategory.None)]
        ItemBottleAliens,

        [RupeeRepeatable]
        //[StartingItem(0xC5CE38, 0x12)]
        [Repeatable, Temporary, Overwritable] // specially handled to turn into Gold Dust on subsequent times
        [ItemName("Bottle with Gold Dust"), LocationName("Goron Race"), Region(Region.TwinIslands)]
        [GossipLocationHint("a sporting event"), GossipItemHint("a gleaming powder"), GossipCompetitiveHint(-2)]
        [ShopText("It's very high quality.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x6A), ItemCategory(ItemCategory.None)]
        ItemBottleGoronRace,

        //[StartingItem(0xC5CE39, 0x12)]
        [ItemName("Empty Bottle"), LocationName("Beaver Race #1"), Region(Region.ZoraCape)]
        [GossipLocationHint("a river dweller"), GossipItemHint("an empty vessel", "a glass container"), GossipCompetitiveHint(-2)]
        [GossipCombineOrder(0), GossipCombine(HeartPieceBeaverRace, "Beaver Races")]
        [ShopText("Carry various items in this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x5A), ItemCategory(ItemCategory.None)]
        ItemBottleBeavers,

        //[StartingItem(0xC5CE3A, 0x12)]
        [ItemName("Empty Bottle"), LocationName("Dampe Digging"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a fearful basement"), GossipItemHint("an empty vessel", "a glass container"), GossipCompetitiveHint]
        [ShopText("Carry various items in this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0261E000 + 0x1FE, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x64), ItemCategory(ItemCategory.None)]
        ItemBottleDampe,

        //[StartingItem(0xC5CE3B, 0x12)]
        [Repeatable, Temporary, Overwritable] // specially handled to turn into Chateau Romani on subsequent times
        [ItemName("Bottle with Chateau Romani"), LocationName("Madame Aroma in Bar"), Region(Region.EastClockTown)]
        [GossipLocationHint("an important lady"), GossipItemHint("a dairy product", "an adult beverage")]
        [ShopText("Drink it to get lasting stamina for your magic power.\u0009\u0001\u0000\u0000 Comes with an Empty Bottle.\u0009\u0002")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x6F), ItemCategory(ItemCategory.None)]
        ItemBottleMadameAroma,

        [StartingItem(0xC5CE71, 0x04)]
        [ItemName("Bombers' Notebook"), LocationName("Bombers' Hide and Seek"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a group of children", "a town game"), GossipItemHint("a handy notepad", "a quest logbook")]
        [ShopText("Allows you to keep track of people's schedules.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x50), ItemCategory(ItemCategory.None)]
        ItemNotebook,

        //upgrades
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE21, 0x02)]
        [StartingItem(0xC5CE00, 0x4E)]
        [ItemName("Razor Sword"), LocationName("Mountain Smithy Day 1"), Region(Region.MountainVillage)]
        [GossipLocationHint("the mountain smith"), GossipItemHint("a sharp blade")]
        [ShopText("A sharp sword forged at the smithy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x38), ItemCategory(ItemCategory.None)]
        UpgradeRazorSword,

        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE21, 0x03)]
        [StartingItem(0xC5CE00, 0x4F)]
        [ItemName("Gilded Sword"), LocationName("Mountain Smithy Day 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("the mountain smith"), GossipItemHint("a sharp blade")]
        [ShopText("A very sharp sword forged from gold dust.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x39), ItemCategory(ItemCategory.None)]
        UpgradeGildedSword,

        [Downgradable]
        [StartingItem(0xC5CE21, 0x20)]
        [ItemName("Mirror Shield"), LocationName("Mirror Shield Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a reflective guard", "echoing protection")]
        [ShopText("It can reflect certain rays of light.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x029FE000 + 0x1AA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x33), ItemCategory(ItemCategory.None)]
        UpgradeMirrorShield,

        [RupeeRepeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x02)]
        [ItemName("Large Quiver"), LocationName("Town Archery #1"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town activity"), GossipItemHint("a projectile", "a ranged weapon")]
        [GossipCombineOrder(0), GossipCombine(HeartPieceTownArchery, "Town Archery")]
        [ShopText("This can hold up to a maximum of 40 arrows.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x23), ItemCategory(ItemCategory.None)]
        UpgradeBigQuiver,

        [RupeeRepeatable]
        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE25, 0x01)]
        [StartingItem(0xC5CE6F, 0x03)]
        [ItemName("Largest Quiver"), LocationName("Swamp Archery #1"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a projectile", "a ranged weapon")]
        [GossipCombineOrder(0), GossipCombine(HeartPieceSwampArchery, "Swamp Archery")]
        [ShopText("This can hold up to a maximum of 50 arrows.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x24), ItemCategory(ItemCategory.None)]
        UpgradeBiggestQuiver,

        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x10)]
        [ItemName("Big Bomb Bag"), LocationName("Big Bomb Bag Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopRoom(ShopRoomAttribute.Room.BombShop, 0x52)]
        [ShopRoom(ShopRoomAttribute.Room.CuriosityShop, 0x44)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.CuriosityShop, 2)]
        [ShopText("This can hold up to a maximum of 30 bombs.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1C), ItemCategory(ItemCategory.ShopItems)]
        UpgradeBigBombBag,

        [Progressive]
        [Downgradable, Purchaseable]
        [StartingItem(0xC5CE2A, 0x06)]
        [StartingItem(0xC5CE6F, 0x18)]
        [ItemName("Biggest Bomb Bag"), LocationName("Biggest Bomb Bag Purchase"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant"), GossipItemHint("an item carrier", "a vessel of explosives")]
        [ShopText("This can hold up to a maximum of 40 bombs.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1D), ItemCategory(ItemCategory.ShopItems)]
        UpgradeBiggestBombBag,

        [Progressive]
        [StartingItem(0xC5CE6E, 0x10)]
        [ItemName("Adult Wallet"), LocationName("Bank Reward #1"), Region(Region.WestClockTown)]
        [GossipLocationHint("a keeper of wealth"), GossipItemHint("a coin case", "great wealth")]
        [ShopText("This can hold up to a maximum of 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x08), ItemCategory(ItemCategory.None)]
        UpgradeAdultWallet,

        [Progressive]
        [Downgradable]
        [StartingItem(0xC5CE6E, 0x20)]
        [ItemName("Giant Wallet"), LocationName("Ocean Spider House Day 1 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("a coin case", "great wealth"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false)]
        [GossipCombineOrder(0), GossipCombine(MundaneItemOceanSpiderHouseDay2PurpleRupee, "Ocean Spider House"), GossipCombine(MundaneItemOceanSpiderHouseDay3RedRupee, "Ocean Spider House")]
        [ShopText("This can hold up to a maximum of 500 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x09), ItemCategory(ItemCategory.None)]
        UpgradeGiantWallet,

        //trades
        [Visible]
        [Repeatable, Temporary, Overwritable]
        [ItemName("Moon's Tear"), LocationName("Astronomy Telescope"), Region(Region.TerminaField)]
        [GossipLocationHint("a falling star"), GossipItemHint("a lunar teardrop", "celestial sadness")]
        [ShopText("A shining stone from the moon.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x96), ItemCategory(ItemCategory.None)]
        TradeItemMoonTear,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Land Title Deed"), LocationName("Clock Town Scrub Trade"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x97), ItemCategory(ItemCategory.None)]
        TradeItemLandDeed,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Swamp Title Deed"), LocationName("Swamp Scrub Trade"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a southern merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Southern Swamp.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x98), ItemCategory(ItemCategory.None)]
        TradeItemSwampDeed,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Mountain Title Deed"), LocationName("Mountain Scrub Trade"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower near Goron Village.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x99), ItemCategory(ItemCategory.None)]
        TradeItemMountainDeed,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Ocean Title Deed"), LocationName("Ocean Scrub Trade"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant"), GossipItemHint("a property deal")]
        [ShopText("The title deed to the Deku Flower in Zora Hall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x9A), ItemCategory(ItemCategory.None)]
        TradeItemOceanDeed,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Room Key"), LocationName("Inn Reservation"), Region(Region.StockPotInn)]
        [GossipLocationHint("checking in", "check-in"), GossipItemHint("a door opener", "a lock opener")]
        [ShopText("With this, you can go in and out of the Stock Pot Inn at night.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xA0), ItemCategory(ItemCategory.None)]
        TradeItemRoomKey,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Letter to Kafei"), LocationName("Midnight Meeting"), Region(Region.StockPotInn)]
        [GossipLocationHint("a late meeting"), GossipItemHint("a lover's plight", "a lover's letter")]
        [ShopText("A love letter from Anju to Kafei.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xAA), ItemCategory(ItemCategory.None)]
        TradeItemKafeiLetter,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Pendant of Memories"), LocationName("Kafei"), Region(Region.LaundryPool)]
        [GossipLocationHint("a posted letter"), GossipItemHint("a cherished necklace", "a symbol of trust")]
        [ShopText("Kafei's symbol of trust for Anju.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xAB), ItemCategory(ItemCategory.None)]
        TradeItemPendant,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Letter to Mama"), LocationName("Curiosity Shop Man #2"), Region(Region.LaundryPool)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("an important note", "a special delivery")]
        [ShopText("It's a parcel for Kafei's mother.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xA1), ItemCategory(ItemCategory.None)]
        TradeItemMamaLetter,

        //notebook hp
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Mayor"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town leader", "an upstanding figure"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x03), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookMayor,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Postman's Game"), Region(Region.WestClockTown)]
        [GossipLocationHint("a hard worker", "a delivery person"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xCE), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookPostman,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Rosa Sisters"), Region(Region.WestClockTown)]
        [GossipLocationHint("traveling sisters", "twin entertainers"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookRosa,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Toilet Hand"), Region(Region.StockPotInn)]
        [GossipLocationHint("a mystery appearance", "a strange palm"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookHand,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Grandma Short Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("an old lady"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookGran1,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Grandma Long Story"), Region(Region.StockPotInn)]
        [GossipLocationHint("an old lady"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNotebookGran2,

        //other hp
        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Keaton Quiz"), Region(Region.NorthClockTown)]
        [GossipLocationHint("the ghost of a fox", "a mysterious fox"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceKeatonQuiz,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Playground Three Days"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint]
        [GossipCombineOrder(1), GossipCombine(MundaneItemDekuPlaygroundPurpleRupee, "Deku Playground")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceDekuPlayground,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Town Archery #2"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1)]
        [GossipCombineOrder(1), GossipCombine(UpgradeBigQuiver, "Town Archery")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x90), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceTownArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Honey and Darling Three Days"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [GossipCombineOrder(1), GossipCombine(MundaneItemHoneyAndDarlingPurpleRupee, "Honey and Darling")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x94), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceHoneyAndDarling,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swordsman's School"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x9F), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceSwordsmanSchool,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Postbox"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an information carrier", "a correspondence box"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA2), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPiecePostBox,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Gossip Stones"), Region(Region.TerminaField)]
        [GossipLocationHint("mysterious stones"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA3), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceTerminaGossipStones,

        [Purchaseable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Business Scrub Purchase"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden merchant"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA5), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceTerminaBusinessScrub,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swamp Archery #2"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1)]
        [GossipCombineOrder(1), GossipCombine(UpgradeBiggestQuiver, "Swamp Archery")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA6), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceSwampArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Pictograph Contest Winner"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA7), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPiecePictobox,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Boat Archery"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("a segment of health"), GossipCompetitiveHint]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xA8), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceBoatArchery,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Frog Choir"), Region(Region.MountainVillage)]
        [GossipLocationHint("a reunion", "a chorus", "an amphibian choir"), GossipItemHint("a segment of health"), GossipCompetitiveHint(3)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAC), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceChoir,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Beaver Race #2"), Region(Region.ZoraCape)]
        [GossipLocationHint("a river dweller", "a race in the water"), GossipItemHint("a segment of health"), GossipCompetitiveHint(1)]
        [GossipCombineOrder(1), GossipCombine(ItemBottleBeavers, "Beaver Races")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAD), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceBeaverRace,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Seahorses"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a reunion"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAE), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceSeaHorse,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Fisherman Game"), Region(Region.GreatBayCoast), GossipCompetitiveHint(-2)]
        [GossipLocationHint("an ocean game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xAF), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceFishermanGame,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Evan"), Region(Region.ZoraHall)]
        [GossipLocationHint("a muse", "a composition", "a musician", "plagiarism"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB0), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceEvan,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Dog Race"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting event"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB1), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceDogRace,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Poe Hut"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a game of ghosts"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB2), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPiecePoeHut,

        [RupeeRepeatable]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Treasure Chest Game Goron"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x17), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceTreasureChestGame,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Peahat Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ED3000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x18), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPiecePeahat,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Dodongo Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EBD000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x20), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceDodong,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Woodfall Bridge Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("the swamp lands", "an exposed chest"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x252, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA52)]
        [GetItemIndex(0x29), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceWoodFallChest,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Twin Islands Underwater Ramp Chest"), Region(Region.TwinIslands)]
        [GossipLocationHint("a spring treasure", "a defrosted land", "a submerged chest"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C23000 + 0x2B6, ChestAttribute.AppearanceType.Normal, 0x02C34000 + 0x19A)]
        [GetItemIndex(0x2E), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceTwinIslandsChest,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ocean Spider House Chest"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("the strange masks", "coloured faces"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x024DB000 + 0x76, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x14), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceOceanSpiderHouse,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Iron Knuckle Chest"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a hollow ground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x01FAB000 + 0xBA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x44), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceKnuckle,

        //mask
        [StartingItem(0xC5CE3C, 0x3E)]
        [ItemName("Postman's Hat"), LocationName("Postman's Freedom Reward"), Region(Region.EastClockTown)]
        [GossipLocationHint("a special delivery", "one last job"), GossipItemHint("a hard worker's hat")]
        [ShopText("You can look into mailboxes when you wear this.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x84), ItemCategory(ItemCategory.None)]
        MaskPostmanHat,

        [StartingItem(0xC5CE3D, 0x38)]
        [ItemName("All Night Mask"), LocationName("All Night Mask Purchase"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("insomnia"), GossipCompetitiveHint(0, nameof(GameplaySettings.UpdateShopAppearance), false)]
        [ShopRoom(ShopRoomAttribute.Room.CuriosityShop, 0x54)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.CuriosityShop, 0)]
        [ShopText("When you wear it you don't get sleepy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7E), ItemCategory(ItemCategory.None)]
        MaskAllNight,

        [StartingItem(0xC5CE3E, 0x47)]
        [ItemName("Blast Mask"), LocationName("Old Lady"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a good deed", "an old lady's struggle"), GossipItemHint("a dangerous mask")]
        [ShopText("Wear it and detonate it...")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8D), ItemCategory(ItemCategory.None)]
        MaskBlast,

        [StartingItem(0xC5CE3F, 0x45)]
        [ItemName("Stone Mask"), LocationName("Invisible Soldier"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a hidden soldier", "a stone circle"), GossipItemHint("inconspicuousness")]
        [ShopText("Become as plain as stone so you can blend into your surroundings.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8B), ItemCategory(ItemCategory.None)]
        MaskStone,

        [StartingItem(0xC5CE40, 0x40)]
        [ItemName("Great Fairy's Mask"), LocationName("Town Great Fairy"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a magical being"), GossipItemHint("a friend of fairies")]
        [ShopText("The mask's hair will shimmer when you're close to a Stray Fairy.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x131), ItemCategory(ItemCategory.GreatFairyRewards)]
        MaskGreatFairy,

        [StartingItem(0xC5CE42, 0x3A)]
        [ItemName("Keaton Mask"), LocationName("Curiosity Shop Man #1"), Region(Region.LaundryPool)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a popular mask", "a fox's mask")]
        [ShopText("The mask of the ghost fox, Keaton.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x80), ItemCategory(ItemCategory.None)]
        MaskKeaton,

        [StartingItem(0xC5CE43, 0x46)]
        [ItemName("Bremen Mask"), LocationName("Guru Guru"), Region(Region.LaundryPool)]
        [GossipLocationHint("a musician", "an entertainer"), GossipItemHint("a mask of leadership", "a bird's mask")]
        [ShopText("Wear it so young animals will mistake you for their leader.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8C), ItemCategory(ItemCategory.None)]
        MaskBremen,

        [StartingItem(0xC5CE44, 0x39)]
        [ItemName("Bunny Hood"), LocationName("Grog"), Region(Region.RomaniRanch)]
        [GossipLocationHint("an ugly but kind heart", "a lover of chickens"), GossipItemHint("the ears of the wild", "a rabbit's hearing")]
        [ShopText("Wear it to be filled with the speed and hearing of the wild.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7F), ItemCategory(ItemCategory.None)]
        MaskBunnyHood,

        [StartingItem(0xC5CE45, 0x42)]
        [ItemName("Don Gero's Mask"), LocationName("Hungry Goron"), Region(Region.MountainVillage)]
        [GossipLocationHint("a hungry goron", "a person in need"), GossipItemHint("a conductor's mask", "an amphibious mask")]
        [ShopText("When you wear it, you can call the Frog Choir members together.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x88), ItemCategory(ItemCategory.None)]
        MaskDonGero,

        [RupeeRepeatable]
        [StartingItem(0xC5CE46, 0x48)]
        [ItemName("Mask of Scents"), LocationName("Butler"), Region(Region.DekuPalace)]
        [GossipLocationHint("a servant of royalty", "the royal servant"), GossipItemHint("heightened senses", "a pig's mask"), GossipCompetitiveHint(-1)]
        [ShopText("Wear it to heighten your sense of smell.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8E), ItemCategory(ItemCategory.None)]
        MaskScents,

        [StartingItem(0xC5CE48, 0x3C)]
        [ItemName("Romani's Mask"), LocationName("Cremia"), Region(Region.RomaniRanch)]
        [GossipLocationHint("the ranch lady", "an older sister"), GossipItemHint("proof of membership", "a cow's mask"), GossipCompetitiveHint]
        [GossipCombineOrder(1), GossipCombine(ItemBottleAliens, "Ranch Sisters Defense")]
        [ShopText("Wear it to show you're a member of the Milk Bar, Latte.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x82), ItemCategory(ItemCategory.None)]
        MaskRomani,

        [StartingItem(0xC5CE49, 0x3D)]
        [ItemName("Circus Leader's Mask"), LocationName("Gorman"), Region(Region.EastClockTown)]
        [GossipLocationHint("an entertainer", "a miserable leader"), GossipItemHint("a mask of sadness")]
        [ShopText("People related to Gorman will react to this.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x83), ItemCategory(ItemCategory.None)]
        MaskCircusLeader,

        [StartingItem(0xC5CE4A, 0x37)]
        [ItemName("Kafei's Mask"), LocationName("Madame Aroma in Office"), Region(Region.EastClockTown)]
        [GossipLocationHint("an important lady", "an esteemed woman"), GossipItemHint("the mask of a missing one", "a son's mask")]
        [ShopText("Wear it to inquire about Kafei's whereabouts.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8F), ItemCategory(ItemCategory.None)]
        MaskKafei,

        [StartingItem(0xC5CE4B, 0x3F)]
        [ItemName("Couple's Mask"), LocationName("Anju and Kafei"), Region(Region.StockPotInn)]
        [GossipLocationHint("a reunion", "a lovers' reunion"), GossipItemHint("a sign of love", "the mark of a couple"), GossipCompetitiveHint(3)]
        [ShopText("When you wear it, you can soften people's hearts.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x85), ItemCategory(ItemCategory.None)]
        MaskCouple,

        [StartingItem(0xC5CE4C, 0x36)]
        [ItemName("Mask of Truth"), LocationName("Swamp Spider House Reward"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a gold spider"), GossipItemHint("a piercing gaze"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false)]
        [ShopText("Wear it to read the thoughts of Gossip Stones and animals.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x8A), ItemCategory(ItemCategory.None)]
        MaskTruth,

        [StartingItem(0xC5CE4E, 0x43)]
        [ItemName("Kamaro's Mask"), LocationName("Kamaro"), Region(Region.TerminaField)]
        [GossipLocationHint("a ghostly dancer", "a dancer"), GossipItemHint("dance moves")]
        [ShopText("Wear this to perform a mysterious dance.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x89), ItemCategory(ItemCategory.None)]
        MaskKamaro,

        [StartingItem(0xC5CE4F, 0x41)]
        [ItemName("Gibdo Mask"), LocationName("Pamela's Father"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a healed spirit", "a lost father"), GossipItemHint("a mask of monsters")]
        [ShopText("Even a real Gibdo will mistake you for its own kind.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x87), ItemCategory(ItemCategory.None)]
        MaskGibdo,

        [RupeeRepeatable]
        [StartingItem(0xC5CE50, 0x3B)]
        [ItemName("Garo's Mask"), LocationName("Gorman Bros Race"), Region(Region.MilkRoad)]
        [GossipLocationHint("a sporting event"), GossipItemHint("the mask of spies")]
        [ShopText("This mask can summon the hidden Garo ninjas.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x81), ItemCategory(ItemCategory.None)]
        MaskGaro,

        [StartingItem(0xC5CE51, 0x44)]
        [ItemName("Captain's Hat"), LocationName("Captain Keeta's Chest"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a ghostly battle", "a skeletal leader"), GossipItemHint("a commanding presence")]
        [ShopText("Wear it to pose as Captain Keeta.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x0280D000 + 0x392, ChestAttribute.AppearanceType.Normal, 0x0280D000 + 0x6FA)]
        [GetItemIndex(0x7C), ItemCategory(ItemCategory.None)]
        MaskCaptainHat,

        [StartingItem(0xC5CE52, 0x49)]
        [ItemName("Giant's Mask"), LocationName("Giant's Mask Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a growth spurt")]
        [ShopText("If you wear it in a certain room, you'll grow into a giant.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x020F1000 + 0x1C2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x19E)]
        [GetItemIndex(0x7D), ItemCategory(ItemCategory.None)]
        MaskGiant,

        [StartingItem(0xC5CE47, 0x33)]
        [ItemName("Goron Mask"), LocationName("Darmani"), Region(Region.MountainVillage)]
        [GossipLocationHint("a healed spirit", "the lost champion"), GossipItemHint("a mountain spirit")]
        [ShopText("Wear it to assume Goron form.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x79), ItemCategory(ItemCategory.None)]
        MaskGoron,

        [StartingItem(0xC5CE4D, 0x34)]
        [ItemName("Zora Mask"), LocationName("Mikau"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a healed spirit", "a fallen guitarist"), GossipItemHint("an ocean spirit")]
        [ShopText("Wear it to assume Zora form.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7A), ItemCategory(ItemCategory.None)]
        MaskZora,

        //song
        [StartingItem(0xC5CE72, 0x20)]
        [ItemName("Song of Healing"), LocationName("Starting Song"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a soothing melody")]
        [ShopText("This melody will soothe restless spirits.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x124), ItemCategory(ItemCategory.None)]
        SongHealing,

        [StartingItem(0xC5CE72, 0x80)]
        [ItemName("Song of Soaring"), LocationName("Swamp Music Statue"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a stone tablet"), GossipItemHint("white wings")]
        [ShopText("This melody sends you to a stone bird statue in an instant.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x70), ItemCategory(ItemCategory.SongOfSoaring)]
        SongSoaring,

        [RupeeRepeatable]
        [StartingItem(0xC5CE72, 0x40)]
        [ItemName("Epona's Song"), LocationName("Romani's Game"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a reunion"), GossipItemHint("a horse's song", "a song of the field")]
        [ShopText("This melody calls your horse, Epona.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x71), ItemCategory(ItemCategory.None)]
        SongEpona,

        [StartingItem(0xC5CE71, 0x01)]
        [ItemName("Song of Storms"), LocationName("Day 1 Grave Tablet"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a hollow ground", "a stone tablet"), GossipItemHint("rain and thunder", "stormy weather")]
        [ShopText("This melody is the turbulent tune that blows curses away.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x72), ItemCategory(ItemCategory.None)]
        SongStorms,

        [StartingItem(0xC5CE73, 0x40)]
        [ItemName("Sonata of Awakening"), LocationName("Imprisoned Monkey"), Region(Region.DekuPalace)]
        [GossipLocationHint("a prisoner", "a false imprisonment"), GossipItemHint("a royal song", "an awakening melody")]
        [ShopText("This melody awakens those who have fallen into a deep sleep.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x73), ItemCategory(ItemCategory.None)]
        SongSonata,

        [StartingItem(0xC5CE73, 0x80)]
        [ItemName("Goron Lullaby"), LocationName("Baby Goron"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely child", "an elder's son"), GossipItemHint("a sleepy melody", "a father's lullaby")]
        [ShopText("This melody blankets listeners in calm while making eyelids grow heavy.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x74), ItemCategory(ItemCategory.None)]
        SongLullaby,

        [StartingItem(0xC5CE72, 0x01)]
        [ItemName("New Wave Bossa Nova"), LocationName("Baby Zoras"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("the lost children", "the pirates' loot"), GossipItemHint("an ocean roar", "a song of newborns"), GossipCompetitiveHint(2, nameof(GameplaySettings.AddSongs), true)]
        [ShopText("It's the melody taught by the Zora children that invigorates singing voices.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x75), ItemCategory(ItemCategory.None)]
        SongNewWaveBossaNova,

        [StartingItem(0xC5CE72, 0x02)]
        [ItemName("Elegy of Emptiness"), LocationName("Ikana King"), Region(Region.IkanaCastle)]
        [GossipLocationHint("a fallen king", "a battle in darkness"), GossipItemHint("empty shells", "skin shedding")]
        [ShopText("It's a mystical song that allows you to shed a shell shaped in your current image.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x1CB), ItemCategory(ItemCategory.None)] // 0x76 is a special value used for ice traps in chests
        SongElegy,

        [Visible]
        [StartingItem(0xC5CE72, 0x04)]
        [ItemName("Oath to Order"), LocationName("Boss Blue Warp"), Region(Region.Misc)]
        [GossipLocationHint("cleansed evil", "a fallen evil"), GossipItemHint("a song of summoning", "a song of giants")]
        [ShopText("This melody will call the giants at the right moment.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x77), ItemCategory(ItemCategory.None)]
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
        AreaWoodfallNew,
        AreaSnowheadNew,
        AreaGreatBayNew,
        AreaLANew, // ??
        AreaInvertedStoneTowerNew, // Seemingly not used

        //keysanity items
        [StartingItem(0xC5CE74, 0x04)]
        [ItemName("Woodfall Map"), LocationName("Woodfall Map Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Woodfall Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x0221F000 + 0x12A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x3E), ItemCategory(ItemCategory.None)]
        ItemWoodfallMap,

        [StartingItem(0xC5CE74, 0x02)]
        [ItemName("Woodfall Compass"), LocationName("Woodfall Compass Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Woodfall Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02215000 + 0xFA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x3F), ItemCategory(ItemCategory.None)]
        ItemWoodfallCompass,

        [Repeatable, Temporary]
        [ItemName("Woodfall Boss Key"), LocationName("Woodfall Boss Key Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Woodfall Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02227000 + 0x11A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x3D), ItemCategory(ItemCategory.DungeonKeys)]
        ItemWoodfallBossKey,

        [Repeatable, Temporary]
        [ItemName("Woodfall Small Key"), LocationName("Woodfall Small Key Chest"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Woodfall Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02218000 + 0x1CA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x3C), ItemCategory(ItemCategory.DungeonKeys)]
        ItemWoodfallKey1,

        [StartingItem(0xC5CE75, 0x04)]
        [ItemName("Snowhead Map"), LocationName("Snowhead Map Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02346000 + 0x13A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x54), ItemCategory(ItemCategory.None)]
        ItemSnowheadMap,

        [StartingItem(0xC5CE75, 0x02)]
        [ItemName("Snowhead Compass"), LocationName("Snowhead Compass Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x022F2000 + 0x1BA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x57), ItemCategory(ItemCategory.None)]
        ItemSnowheadCompass,

        [Repeatable, Temporary]
        [ItemName("Snowhead Boss Key"), LocationName("Snowhead Boss Key Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x0230C000 + 0x57A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x4E), ItemCategory(ItemCategory.DungeonKeys)]
        ItemSnowheadBossKey,

        [Repeatable, Temporary]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Block Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02306000 + 0x12A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x46), ItemCategory(ItemCategory.DungeonKeys)]
        ItemSnowheadKey1,

        [Repeatable, Temporary]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Icicle Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0233A000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x47), ItemCategory(ItemCategory.DungeonKeys)]
        ItemSnowheadKey2,

        [Repeatable, Temporary]
        [ItemName("Snowhead Small Key"), LocationName("Snowhead Bridge Room Chest"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Snowhead Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x022F9000 + 0x19A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x48), ItemCategory(ItemCategory.DungeonKeys)]
        ItemSnowheadKey3,

        [StartingItem(0xC5CE76, 0x04)]
        [ItemName("Great Bay Map"), LocationName("Great Bay Map Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Great Bay Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02905000 + 0x19A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x55), ItemCategory(ItemCategory.None)]
        ItemGreatBayMap,

        [StartingItem(0xC5CE76, 0x02)]
        [ItemName("Great Bay Compass"), LocationName("Great Bay Compass Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Great Bay Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02914000 + 0x21A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x58), ItemCategory(ItemCategory.None)]
        ItemGreatBayCompass,

        [Repeatable, Temporary]
        [ItemName("Great Bay Boss Key"), LocationName("Great Bay Boss Key Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Great Bay Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02914000 + 0x1FA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x4F), ItemCategory(ItemCategory.DungeonKeys)]
        ItemGreatBayBossKey,

        [Repeatable, Temporary]
        [ItemName("Great Bay Small Key"), LocationName("Great Bay Small Key Chest"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Great Bay Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02914000 + 0x20A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x40), ItemCategory(ItemCategory.DungeonKeys)]
        ItemGreatBayKey1,

        [StartingItem(0xC5CE77, 0x04)]
        [ItemName("Stone Tower Map"), LocationName("Stone Tower Map Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a navigation aid", "a paper guide")]
        [ShopText("The Dungeon Map for Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x0210F000 + 0x222, ChestAttribute.AppearanceType.Normal, 0x02182000 + 0x21E)]
        [GetItemIndex(0x56), ItemCategory(ItemCategory.None)]
        ItemStoneTowerMap,

        [StartingItem(0xC5CE77, 0x02)]
        [ItemName("Stone Tower Compass"), LocationName("Stone Tower Compass Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("a navigation aid", "a magnetic needle")]
        [ShopText("The Compass for Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02104000 + 0x292, ChestAttribute.AppearanceType.Normal, 0x02177000 + 0x2DE)]
        [GetItemIndex(0x6C), ItemCategory(ItemCategory.None)]
        ItemStoneTowerCompass,

        [Repeatable, Temporary]
        [ItemName("Stone Tower Boss Key"), LocationName("Stone Tower Boss Key Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("an important key", "entry to evil's lair")]
        [ShopText("The key for the boss room in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.BossKey), Chest(0x02130000 + 0x82, ChestAttribute.AppearanceType.Normal, 0x02198000 + 0xCE)]
        [GetItemIndex(0x53), ItemCategory(ItemCategory.DungeonKeys)]
        ItemStoneTowerBossKey,

        [Repeatable, Temporary]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Armor Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x202, ChestAttribute.AppearanceType.AppearsSwitch, 0x02182000 + 0x1FE)]
        [GetItemIndex(0x49), ItemCategory(ItemCategory.DungeonKeys)]
        ItemStoneTowerKey1,

        [Repeatable, Temporary]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Eyegore Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1D2, ChestAttribute.AppearanceType.Normal, 0x02164000 + 0x1AE)]
        [GetItemIndex(0x4A), ItemCategory(ItemCategory.DungeonKeys)]
        ItemStoneTowerKey2,

        [Repeatable, Temporary]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Updraft Room Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x282, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2CE)]
        [GetItemIndex(0x4B), ItemCategory(ItemCategory.DungeonKeys)]
        ItemStoneTowerKey3,

        [Repeatable, Temporary]
        [ItemName("Stone Tower Small Key"), LocationName("Stone Tower Death Armos Maze Chest"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("access to a locked door", "a useful key")]
        [ShopText("A small key for use in Stone Tower Temple.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020FC000 + 0x252, ChestAttribute.AppearanceType.Normal, 0x0216E000 + 0x1CE)]
        [GetItemIndex(0x4D), ItemCategory(ItemCategory.DungeonKeys)]
        ItemStoneTowerKey4,

        //shop items
        [Repeatable, Temporary, Overwritable]
        [ItemName("Red Potion"), LocationName("Trading Post Red Potion"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x42)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 7)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 7)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xCD), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostRedPotion,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Green Potion"), LocationName("Trading Post Green Potion"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a magic potion", "a green drink")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x62)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 2)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 3)]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xBB), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostGreenPotion,

        [Repeatable]
        [ItemName("Hero's Shield"), LocationName("Trading Post Hero's Shield"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a basic guard", "protection")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x44)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 3)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 6)]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0xBC), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostShield,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Fairy"), LocationName("Trading Post Fairy"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a winged friend", "a healer")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x5C)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 0)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 0)]
        [ShopText("Recovers life energy. If you run out of life energy you'll automatically use this.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xBD), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostFairy,

        [Repeatable, Temporary]
        [ItemName("Deku Stick"), LocationName("Trading Post Deku Stick"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x48)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 4)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 5)]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xBE), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostStick,

        [Repeatable, Temporary]
        [ItemName("30 Arrows"), LocationName("Trading Post 30 Arrows"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x4A)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 5)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xBF), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostArrow30,

        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Trading Post 10 Deku Nuts"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a flashing impact")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x46)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 6)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 4)]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC0), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostNut10,

        [Repeatable, Temporary]
        [ItemName("50 Arrows"), LocationName("Trading Post 50 Arrows"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant", "a convenience store", "a market"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopRoom(ShopRoomAttribute.Room.TradingPost, 0x64)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostMain, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.TradingPostPartTimer, 2)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC1), ItemCategory(ItemCategory.ShopItems)]
        ShopItemTradingPostArrow50,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Blue Potion"), LocationName("Witch Shop Blue Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("consumable strength", "a magic potion", "a blue drink")]
        [ShopRoom(ShopRoomAttribute.Room.WitchShop, 0x42)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 2)]
        [ShopText("Replenishes both life energy and magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC2), ItemCategory(ItemCategory.ShopItems)]
        ShopItemWitchBluePotion,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Red Potion"), LocationName("Witch Shop Red Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopRoom(ShopRoomAttribute.Room.WitchShop, 0x48)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 0)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC3), ItemCategory(ItemCategory.ShopItems)]
        ShopItemWitchRedPotion,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Green Potion"), LocationName("Witch Shop Green Potion"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("a magic potion", "a green drink")]
        [ShopRoom(ShopRoomAttribute.Room.WitchShop, 0x4A)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.WitchShop, 1)]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC4), ItemCategory(ItemCategory.ShopItems)]
        ShopItemWitchGreenPotion,

        [Repeatable, Temporary]
        [ItemName("10 Bombs"), LocationName("Bomb Shop 10 Bombs"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("explosives")]
        [ShopRoom(ShopRoomAttribute.Room.BombShop, 0x44)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 3)]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC5), ItemCategory(ItemCategory.ShopItems)]
        ShopItemBombsBomb10,

        [Repeatable, Temporary]
        [ItemName("10 Bombchu"), LocationName("Bomb Shop 10 Bombchu"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town merchant"), GossipItemHint("explosives")]
        [ShopRoom(ShopRoomAttribute.Room.BombShop, 0x42)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.BombShop, 2)]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC6), ItemCategory(ItemCategory.ShopItems)]
        ShopItemBombsBombchu10,

        [Repeatable, Temporary]
        [ItemName("10 Bombs"), LocationName("Goron Shop 10 Bombs"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("explosives")]
        [ShopRoom(ShopRoomAttribute.Room.GoronShop, 0x48)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 0)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 0)]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC7), ItemCategory(ItemCategory.ShopItems)]
        ShopItemGoronBomb10,

        [Repeatable, Temporary]
        [ItemName("10 Arrows"), LocationName("Goron Shop 10 Arrows"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopRoom(ShopRoomAttribute.Room.GoronShop, 0x44)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 1)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xC8), ItemCategory(ItemCategory.ShopItems)]
        ShopItemGoronArrow10,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Red Potion"), LocationName("Goron Shop Red Potion"), Region(Region.GoronVillage)]
        [GossipLocationHint("a northern merchant", "a bored goron"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopRoom(ShopRoomAttribute.Room.GoronShop, 0x46)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShop, 2)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.GoronShopSpring, 2)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xC9), ItemCategory(ItemCategory.ShopItems)]
        ShopItemGoronRedPotion,

        [Repeatable]
        [ItemName("Hero's Shield"), LocationName("Zora Shop Hero's Shield"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("a basic guard", "protection")]
        [ShopRoom(ShopRoomAttribute.Room.ZoraShop, 0x4A)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 0)]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0xCA), ItemCategory(ItemCategory.ShopItems)]
        ShopItemZoraShield,

        [Repeatable, Temporary]
        [ItemName("10 Arrows"), LocationName("Zora Shop 10 Arrows"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopRoom(ShopRoomAttribute.Room.ZoraShop, 0x44)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 1)]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xCB), ItemCategory(ItemCategory.ShopItems)]
        ShopItemZoraArrow10,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Red Potion"), LocationName("Zora Shop Red Potion"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant", "an aquatic shop"), GossipItemHint("consumable strength", "a hearty drink", "a red drink")]
        [ShopRoom(ShopRoomAttribute.Room.ZoraShop, 0x46)]
        [ShopInventory(ShopInventoryAttribute.ShopKeeper.ZoraShop, 2)]
        [ShopText("Replenishes your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0xCC), ItemCategory(ItemCategory.ShopItems)]
        ShopItemZoraRedPotion,

        //bottle catch
        [ItemName("Bottle: Fairy"), LocationName("Bottle: Fairy"), Region(Region.BottleCatch)]
        [GossipLocationHint("a wandering healer"), GossipItemHint("a winged friend", "a healer")]
        [GetBottleItemIndices(0x00, 0x0D), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchFairy,

        [ItemName("Bottle: Deku Princess"), LocationName("Bottle: Deku Princess"), Region(Region.BottleCatch)]
        [GossipLocationHint("a captured royal", "an imprisoned daughter"), GossipItemHint("a princess", "a woodland royal")]
        [GetBottleItemIndices(0x08), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchPrincess,

        [ItemName("Bottle: Fish"), LocationName("Bottle: Fish"), Region(Region.BottleCatch)]
        [GossipLocationHint("a swimming creature", "a water dweller"), GossipItemHint("something fresh")]
        [GetBottleItemIndices(0x01), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchFish,

        [ItemName("Bottle: Bug"), LocationName("Bottle: Bug"), Region(Region.BottleCatch)]
        [GossipLocationHint("an insect", "a scuttling creature"), GossipItemHint("an insect", "a scuttling creature")]
        [GetBottleItemIndices(0x02, 0x03), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchBug,

        [ItemName("Bottle: Poe"), LocationName("Bottle: Poe"), Region(Region.BottleCatch)]
        [GossipLocationHint("a wandering ghost"), GossipItemHint("a captured spirit")]
        [GetBottleItemIndices(0x0B), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchPoe,

        [ItemName("Bottle: Big Poe"), LocationName("Bottle: Big Poe"), Region(Region.BottleCatch)]
        [GossipLocationHint("a huge ghost"), GossipItemHint("a captured spirit")]
        [GetBottleItemIndices(0x0C), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchBigPoe,

        [ItemName("Bottle: Spring Water"), LocationName("Bottle: Spring Water"), Region(Region.BottleCatch)]
        [GossipLocationHint("a common liquid"), GossipItemHint("a common liquid", "a fresh drink")]
        [GetBottleItemIndices(0x04), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchSpringWater,

        [ItemName("Bottle: Hot Spring Water"), LocationName("Bottle: Hot Spring Water"), Region(Region.BottleCatch)]
        [GossipLocationHint("a hot liquid", "a boiling liquid"), GossipItemHint("a boiling liquid", "a hot liquid")]
        [GetBottleItemIndices(0x05, 0x06), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchHotSpringWater,

        [ItemName("Bottle: Zora Egg"), LocationName("Bottle: Zora Egg"), Region(Region.BottleCatch)]
        [GossipLocationHint("a lost child"), GossipItemHint("a lost child")]
        [GetBottleItemIndices(0x07), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchEgg,

        [ItemName("Bottle: Mushroom"), LocationName("Bottle: Mushroom"), Region(Region.BottleCatch)]
        [GossipLocationHint("a strange fungus"), GossipItemHint("a strange fungus")]
        [GetBottleItemIndices(0x0A), ItemCategory(ItemCategory.CaughtBottleContents)]
        BottleCatchMushroom,

        //other chests and grottos
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Lens Cave Invisible Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EB8000 + 0xAA, ChestAttribute.AppearanceType.Invisible)]
        [GetItemIndex(0xDD), ItemCategory(ItemCategory.RedRupees)]
        ChestLensCaveRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Lens Cave Rock Chest"), Region(Region.GoronVillage)]
        [GossipLocationHint("a lonely peak"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02EB8000 + 0xDA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF4), ItemCategory(ItemCategory.PurpleRupees)]
        ChestLensCavePurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Bean Grotto"), Region(Region.DekuPalace)]
        [GossipLocationHint("a merchant's cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ECC000 + 0xFA, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xDE), ItemCategory(ItemCategory.RedRupees)]
        ChestBeanGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Hot Spring Water Grotto"), Region(Region.TwinIslands)]
        [GossipLocationHint("a steaming cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02ED7000 + 0xC6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xDF), ItemCategory(ItemCategory.RedRupees)]
        ChestHotSpringGrottoRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Day 1 Grave Bats"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a cloud of bats"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x01F97000 + 0xCE, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xF5), ItemCategory(ItemCategory.PurpleRupees)]
        ChestBadBatsGrottoPurpleRupee,

        [Repeatable, Temporary]
        [ItemName("5 Bombchu"), LocationName("Secret Shrine Grotto"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a waterfall cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02080000 + 0x93, 0x02080000 + 0x1E3, 0x02080000 + 0x2EB)]
        [GetItemIndex(0xD1), ItemCategory(ItemCategory.Bombchu)]
        ChestIkanaSecretShrineGrotto,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Lower Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x020A2000 + 0x256, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE0), ItemCategory(ItemCategory.RedRupees)]
        ChestPiratesFortressRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Upper Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x020A2000 + 0x266, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE1), ItemCategory(ItemCategory.RedRupees)]
        ChestPiratesFortressRedRupee2,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Interior Tank Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023B7000 + 0x66, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE2), ItemCategory(ItemCategory.RedRupees)]
        ChestInsidePiratesFortressTankRedRupee,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Pirates' Fortress Interior Guard Room Chest"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023BB000 + 0x56, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFB), ItemCategory(ItemCategory.SilverRupees)]
        ChestInsidePiratesFortressGuardSilverRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Cage Room Shallow Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023E6000 + 0x24E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE3), ItemCategory(ItemCategory.RedRupees)]
        ChestInsidePiratesFortressHeartPieceRoomRedRupee,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Cage Room Deep Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023E6000 + 0x25E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x105), ItemCategory(ItemCategory.BlueRupees)]
        ChestInsidePiratesFortressHeartPieceRoomBlueRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Maze Chest"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x023F0000 + 0xDE, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE4), ItemCategory(ItemCategory.RedRupees)]
        ChestInsidePiratesFortressMazeRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pinnacle Rock Lower Chest"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a marine trench"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02428000 + 0x24E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE5), ItemCategory(ItemCategory.RedRupees)]
        ChestPinacleRockRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pinnacle Rock Upper Chest"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a marine trench"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02428000 + 0x25E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE6), ItemCategory(ItemCategory.RedRupees)]
        ChestPinacleRockRedRupee2,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Bombers' Hideout Chest"), Region(Region.EastClockTown)]
        [GossipLocationHint("a secret hideout"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x024F1000 + 0x1DE, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFC), ItemCategory(ItemCategory.SilverRupees)]
        ChestBomberHideoutSilverRupee,

        [Repeatable, Temporary]
        [ItemName("Bombchu"), LocationName("Termina Field Pillar Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a hollow pillar"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x025C5000 + 0x583)]
        [GetItemIndex(0xD7), ItemCategory(ItemCategory.Bombchu)]
        ChestTerminaGrottoBombchu,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Grass Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a grassy cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x025C5000 + 0x593)]
        [GetItemIndex(0xDC), ItemCategory(ItemCategory.RedRupees)]
        ChestTerminaGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Underwater Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a sunken chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD52, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE7), ItemCategory(ItemCategory.RedRupees)]
        ChestTerminaUnderwaterRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Grass Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a grassy chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD62, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE8), ItemCategory(ItemCategory.RedRupees)]
        ChestTerminaGrassRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Stump Chest"), Region(Region.TerminaField)]
        [GossipLocationHint("a tree's chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x025C5000 + 0xD72, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xE9), ItemCategory(ItemCategory.RedRupees)]
        ChestTerminaStumpRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Coast Grotto"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a beach cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x026DE000 + 0x43F, 0x026DE000 + 0xFE3)]
        [GetItemIndex(0xD4), ItemCategory(ItemCategory.RedRupees)]
        ChestGreatBayCoastGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Zora Cape Ledge Without Tree Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a high place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x42A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB16)]
        [GetItemIndex(0xEA), ItemCategory(ItemCategory.RedRupees)]
        ChestGreatBayCapeLedge1, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Zora Cape Ledge With Tree Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a high place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x43A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB26)]
        [GetItemIndex(0xEB), ItemCategory(ItemCategory.RedRupees)]
        ChestGreatBayCapeLedge2, //contents? 

        [Repeatable, Temporary]
        [ItemName("Bombchu"), LocationName("Zora Cape Grotto"), Region(Region.ZoraCape)]
        [GossipLocationHint("a beach cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02715000 + 0x45B, 0x02715000 + 0xB47)]
        [GetItemIndex(0xD2), ItemCategory(ItemCategory.Bombchu)]
        ChestGreatBayCapeGrotto, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Zora Cape Underwater Chest"), Region(Region.ZoraCape)]
        [GossipLocationHint("a sunken chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02715000 + 0x48A, ChestAttribute.AppearanceType.Normal, 0x02715000 + 0xB56)]
        [GetItemIndex(0xF6), ItemCategory(ItemCategory.PurpleRupees)]
        ChestGreatBayCapeUnderwater, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Log Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x196, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xEC), ItemCategory(ItemCategory.RedRupees)]
        ChestPiratesFortressEntranceRedRupee1,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Sand Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x1A6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xED), ItemCategory(ItemCategory.RedRupees)]
        ChestPiratesFortressEntranceRedRupee2,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pirates' Fortress Exterior Corner Chest"), Region(Region.PiratesFortressExterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02740000 + 0x1B6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xEE), ItemCategory(ItemCategory.RedRupees)]
        ChestPiratesFortressEntranceRedRupee3,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Swamp Grotto"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a southern cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x027C1000 + 0x33B)]
        [GetItemIndex(0xDB), ItemCategory(ItemCategory.RedRupees)]
        ChestToSwampGrotto, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Doggy Racetrack Roof Chest"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a day at the races"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x027D4000 + 0xB6, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF7), ItemCategory(ItemCategory.PurpleRupees)]
        ChestDogRacePurpleRupee,

        [Repeatable, Temporary]
        [ItemName("5 Bombchu"), LocationName("Ikana Graveyard Grotto"), Region(Region.IkanaGraveyard)]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [GossipLocationHint("a circled cave"), GossipItemHint("explosive mice")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x0280D000 + 0x353, 0x0280D000 + 0x54B)]
        [GetItemIndex(0xD5), ItemCategory(ItemCategory.Bombchu)]
        ChestGraveyardGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Near Swamp Spider House Grotto"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a southern cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x01F3A000 + 0x227, 0x02855000 + 0x2AF)]
        [GetItemIndex(0xDA), ItemCategory(ItemCategory.RedRupees)]
        ChestSwampGrotto,  //contents? 

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Behind Woodfall Owl Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("a swamp chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x232, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA62)]
        [GetItemIndex(0x106), ItemCategory(ItemCategory.BlueRupees)]
        ChestWoodfallBlueRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Entrance to Woodfall Chest"), Region(Region.Woodfall)]
        [GossipLocationHint("a swamp chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02884000 + 0x242, ChestAttribute.AppearanceType.Normal, 0x02884000 + 0xA32)]
        [GetItemIndex(0xEF), ItemCategory(ItemCategory.RedRupees)]
        ChestWoodfallRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Well Right Path Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a frightful exchange"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x029EA000 + 0xE6, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0xF8), ItemCategory(ItemCategory.PurpleRupees)]
        ChestWellRightPurpleRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Well Left Path Chest"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a frightful exchange"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x029F0000 + 0x106, ChestAttribute.AppearanceType.Invisible)]
        [GetItemIndex(0xF9), ItemCategory(ItemCategory.PurpleRupees)]
        ChestWellLeftPurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Waterfall Chest"), Region(Region.MountainVillage)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BDD000 + 0x2E2, ChestAttribute.AppearanceType.Invisible, 0x02BDD000 + 0x946)]
        [GetItemIndex(0xF0), ItemCategory(ItemCategory.RedRupees)]
        ChestMountainVillage, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Spring Grotto"), Region(Region.MountainVillage)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02BFC000 + 0x1F3, 0x02BFC000 + 0x2B3)]
        [GetItemIndex(0xD8), ItemCategory(ItemCategory.RedRupees)]
        ChestMountainVillageGrottoRedRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Ikana Pillar Chest"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a high chest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02B34000 + 0x442, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF1), ItemCategory(ItemCategory.RedRupees)]
        ChestToIkanaRedRupee,

        [Repeatable, Temporary]
        [ItemName("Bombchu"), LocationName("Path to Ikana Grotto"), Region(Region.RoadToIkana)]
        [GossipLocationHint("a blocked cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02B34000 + 0x523)]
        [GetItemIndex(0xD3), ItemCategory(ItemCategory.Bombchu)]
        ChestToIkanaGrotto, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inverted Stone Tower Right Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BC9000 + 0x236, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFD), ItemCategory(ItemCategory.SilverRupees)]
        ChestInvertedStoneTowerSilverRupee,

        [Repeatable, Temporary]
        [ItemName("10 Bombchu"), LocationName("Inverted Stone Tower Middle Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02BC9000 + 0x246, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x10A), ItemCategory(ItemCategory.Bombchu)]
        ChestInvertedStoneTowerBombchu10,

        [Repeatable, Temporary]
        [ItemName("Magic Bean"), LocationName("Inverted Stone Tower Left Chest"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("a plant seed")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold), Chest(0x02BC9000 + 0x256, ChestAttribute.AppearanceType.Normal)]
        [ShopText("Plant it in soft soil.")]
        [GetItemIndex(0x109), ItemCategory(ItemCategory.Ammo)]
        ChestInvertedStoneTowerBean,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Path to Snowhead Grotto"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a snowy cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02C04000 + 0xAF, 0x02C04000 + 0x487)]
        [GetItemIndex(0xD0), ItemCategory(ItemCategory.RedRupees)]
        ChestToSnowheadGrotto, //contents? 

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Twin Islands Cave Chest"), Region(Region.TwinIslands)]
        [GossipLocationHint("the springtime"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C34000 + 0x13A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xF2), ItemCategory(ItemCategory.RedRupees)]
        ChestToGoronVillageRedRupee,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Secret Shrine Final Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C57000 + 0xB6, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x107), ItemCategory(ItemCategory.PiecesOfHeart)]
        ChestSecretShrineHeartPiece,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Dinolfos Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C61000 + 0x9A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xFE), ItemCategory(ItemCategory.SilverRupees)]
        ChestSecretShrineDinoGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Wizzrobe Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C69000 + 0xB2, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0xFF), ItemCategory(ItemCategory.SilverRupees)]
        ChestSecretShrineWizzGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Wart Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C71000 + 0xA6, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x100), ItemCategory(ItemCategory.SilverRupees)]
        ChestSecretShrineWartGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Secret Shrine Garo Master Chest"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02C75000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x101), ItemCategory(ItemCategory.SilverRupees)]
        ChestSecretShrineGaroGrotto,

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inn Staff Room Chest"), Region(Region.StockPotInn)]
        [GossipLocationHint("an employee room"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02CAB000 + 0x10E, ChestAttribute.AppearanceType.Normal, 0x02CAB000 + 0x242)]
        [GetItemIndex(0x102), ItemCategory(ItemCategory.SilverRupees)]
        ChestInnStaffRoom, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("Inn Guest Room Chest"), Region(Region.StockPotInn)]
        [GossipLocationHint("a guest bedroom"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02CB1000 + 0xDA, ChestAttribute.AppearanceType.Normal, 0x02CB1000 + 0x212)]
        [GetItemIndex(0x103), ItemCategory(ItemCategory.SilverRupees)]
        ChestInnGuestRoom, //contents? 

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Mystery Woods Grotto"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a mystery cave"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02CFC000 + 0x5B)]
        [GetItemIndex(0xD9), ItemCategory(ItemCategory.PurpleRupees)]
        ChestWoodsGrotto, //contents? 

        [Repeatable]
        [ItemName("Silver Rupee"), LocationName("East Clock Town Chest"), Region(Region.EastClockTown)]
        [GossipLocationHint("a shop roof"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 100 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02DE4000 + 0x442, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x104), ItemCategory(ItemCategory.SilverRupees)]
        ChestEastClockTownSilverRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("South Clock Town Straw Roof Chest"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a straw roof"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02E5C000 + 0x342, ChestAttribute.AppearanceType.Normal, 0x02E5C000 + 0x806)]
        [GetItemIndex(0xF3), ItemCategory(ItemCategory.RedRupees)]
        ChestSouthClockTownRedRupee,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("South Clock Town Final Day Chest"), Region(Region.SouthClockTown)]
        [GossipLocationHint("a carnival tower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02E5C000 + 0x352, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0xFA), ItemCategory(ItemCategory.PurpleRupees)]
        ChestSouthClockTownPurpleRupee,

        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Bank Reward #3"), Region(Region.WestClockTown)]
        [GossipLocationHint("being rich"), GossipItemHint("a segment of health"), GossipCompetitiveHint(-2)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x108), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceBank,

        //standing HPs
        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Clock Tower Entrance"), Region(Region.SouthClockTown)]
        [GossipLocationHint("the tower doors"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10B), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceSouthClockTown,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("North Clock Town Tree"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a town playground"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10C), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceNorthClockTown,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Path to Swamp Tree"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a tree of bats"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10D), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceToSwamp,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Swamp Tourist Center Roof"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a tourist centre"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10E), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceSwampScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Palace West Garden"), Region(Region.DekuPalace)]
        [GossipLocationHint("the home of scrubs"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x10F), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceDekuPalace,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Goron Village Ledge"), Region(Region.GoronVillage)]
        [GossipLocationHint("a cold ledge"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x110), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceGoronVillageScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Bio Baba Grotto"), Region(Region.TerminaField)]
        [GossipLocationHint("a beehive"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x111), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceZoraGrotto,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Lab Fish"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("feeding the fish"), GossipItemHint("a segment of health"), GossipCompetitiveHint(0, nameof(GameplaySettings.SpeedupLabFish), false)]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x112), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceLabFish,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Zora Cape Like-Like"), Region(Region.ZoraCape)]
        [GossipLocationHint("a shield eater"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x113), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceGreatBayCapeLikeLike,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Pirates' Fortress Cage"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("a timed door"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x114), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPiecePiratesFortress,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Lulu's Room Ledge"), Region(Region.ZoraHall)]
        [GossipLocationHint("the singer's room"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x115), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceZoraHallScrub,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Path to Snowhead Pillar"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a cold platform"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x116), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceToSnowhead,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Great Bay Coast Ledge"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a rock face"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x117), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceGreatBayCoast,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ikana Canyon Ledge"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("a thief's doorstep"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x118), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceIkana,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Ikana Castle Pillar"), Region(Region.IkanaCastle)]
        [GossipLocationHint("a fiery pillar"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x119), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceCastle,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Odolwa Heart Container"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11A), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartContainerWoodfall,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Goht Heart Container"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11B), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartContainerSnowhead,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Gyorg Heart Container"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11C), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartContainerGreatBay,

        [Visible]
        [StartingItem(0xC5CDE9, 0x10, true)] // add max health
        [StartingItem(0xC5CDEB, 0x10, true)] // add current health
        [StartingItem(0xC40E1B, 0x10, true)] // add respawn health
        [StartingItem(0xBDA683, 0x10, true)] // add minimum Song of Time health
        [StartingItem(0xBDA68F, 0x10, true)] // add minimum Song of Time health
        [ItemName("Heart Container"), LocationName("Twinmold Heart Container"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a masked evil"), GossipItemHint("increased life")]
        [ShopText("Permanently increases your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x11D), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartContainerStoneTower,

        //maps
        [Purchaseable]
        [StartingTingleMap(TingleMap.Town)]
        [ItemName("Map of Clock Town"), LocationName("Clock Town Map Purchase"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB4), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapTown,

        [Purchaseable]
        [StartingTingleMap(TingleMap.Swamp)]
        [ItemName("Map of Woodfall"), LocationName("Woodfall Map Purchase"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the south.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB5), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapWoodfall,

        [Purchaseable]
        [StartingTingleMap(TingleMap.Mountain)]
        [ItemName("Map of Snowhead"), LocationName("Snowhead Map Purchase"), Region(Region.RoadToSouthernSwamp)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the north.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB6), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapSnowhead,

        [Purchaseable]
        [StartingTingleMap(TingleMap.Ranch)]
        [ItemName("Map of Romani Ranch"), LocationName("Romani Ranch Map Purchase"), Region(Region.MilkRoad)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the ranch.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB7), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapRanch,

        [Purchaseable]
        [StartingTingleMap(TingleMap.Ocean)]
        [ItemName("Map of Great Bay"), LocationName("Great Bay Map Purchase"), Region(Region.MilkRoad)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the west.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB8), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapGreatBay,

        [Purchaseable]
        [StartingTingleMap(TingleMap.Canyon)]
        [ItemName("Map of Stone Tower"), LocationName("Stone Tower Map Purchase"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a map maker", "a forest fairy"), GossipItemHint("a world map")]
        [ShopText("Map of the east.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0xB9), ItemCategory(ItemCategory.ShopItems)]
        ItemTingleMapStoneTower,

        //oops I forgot one
        [Repeatable, Temporary]
        [ItemName("Bombchu"), LocationName("Goron Racetrack Grotto"), Region(Region.TwinIslands)]
        [GossipLocationHint("a hidden cave"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bomb that is practical, sleek and self-propelled.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), GrottoChest(0x02C23000 + 0x2D7, 0x02C34000 + 0x1DB)]
        [GetItemIndex(0xD6), ItemCategory(ItemCategory.Bombchu)]
        ChestToGoronRaceGrotto, //contents?

        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Canyon Scrub Trade"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("an eastern merchant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x125), ItemCategory(ItemCategory.GoldRupees)]
        IkanaScrubGoldRupee,

        //moon items
        OtherOneMask,
        OtherTwoMasks,
        OtherThreeMasks,
        OtherFourMasks,
        AreaMoonAccess,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Deku Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x11F), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceDekuTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Goron Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x120), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceGoronTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Zora Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x121), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceZoraTrial,

        [Visible]
        [StartingItem(0xC5CE70, 0x10, true)]
        [ItemName("Piece of Heart"), LocationName("Link Trial Bonus"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a segment of health")]
        [ShopText("Collect four to assemble a new Heart Container.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x122), ItemCategory(ItemCategory.PiecesOfHeart)]
        HeartPieceLinkTrial,

        [StartingItem(0xC5CE53, 0x35)]
        [ItemName("Fierce Deity's Mask"), LocationName("Majora Child"), Region(Region.TheMoon)]
        [GossipLocationHint("the lonely child"), GossipItemHint("the wrath of a god")]
        [ShopText("A mask that contains the merits of all masks.", isDefinite: true)]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x7B), ItemCategory(ItemCategory.None)]
        MaskFierceDeity,

        [Repeatable, Temporary]
        [ItemName("30 Arrows"), LocationName("Link Trial Garo Master Chest"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02D4B000 + 0x76, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x126), ItemCategory(ItemCategory.Ammo)]
        ChestLinkTrialArrow30,

        [Repeatable, Temporary]
        [ItemName("10 Bombchu"), LocationName("Link Trial Iron Knuckle Chest"), Region(Region.TheMoon)]
        [GossipLocationHint("a masked child's game"), GossipItemHint("explosive mice")]
        [ShopText("Mouse-shaped bombs that are practical, sleek and self-propelled.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x02D4E000 + 0xC6, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x127), ItemCategory(ItemCategory.Bombchu)]
        ChestLinkTrialBombchu10,

        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Pre-Clocktown Chest"), Region(Region.BeneathClocktown)]
        [GossipLocationHint("the first chest"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x021D2000 + 0x102, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x128), ItemCategory(ItemCategory.GlitchesRequired)]
        ChestPreClocktownDekuNut,

        [Progressive]
        [StartingItem(0xC5CE21, 0x01)]
        [StartingItem(0xC5CE00, 0x4D)]
        [ItemName("Kokiri Sword"), LocationName("Starting Sword"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a forest blade")]
        [ShopText("A sword created by forest folk.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x37), ItemCategory(ItemCategory.CrazyStartingItems)]
        StartingSword,

        [Repeatable]
        [StartingItem(0xC5CE21, 0x10)]
        [ItemName("Hero's Shield"), LocationName("Starting Shield"), Region(Region.Misc)]
        [GossipLocationHint("a new file", "a quest's inception"), GossipItemHint("a basic guard", "protection")]
        [ShopText("Use it to defend yourself.")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [GetItemIndex(0x129), ItemCategory(ItemCategory.CrazyStartingItems)]
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
        [GetItemIndex(0x12A), ItemCategory(ItemCategory.CrazyStartingItems)]
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
        [GetItemIndex(0x12B), ItemCategory(ItemCategory.CrazyStartingItems)]
        StartingHeartContainer2,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Ranch Cow #1"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x132), ItemCategory(ItemCategory.CowMilk)]
        ItemRanchBarnMainCowMilk,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Ranch Cow #2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x182), ItemCategory(ItemCategory.CowMilk)]
        ItemRanchBarnOtherCowMilk1,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Ranch Cow #3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A2), ItemCategory(ItemCategory.CowMilk)]
        ItemRanchBarnOtherCowMilk2,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Cow Beneath the Well"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x135), ItemCategory(ItemCategory.CowMilk)]
        ItemWellCowMilk,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Termina Grotto Cow #1"), Region(Region.TerminaField)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x136), ItemCategory(ItemCategory.CowMilk)]
        ItemTerminaGrottoCowMilk1,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Termina Grotto Cow #2"), Region(Region.TerminaField)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x137), ItemCategory(ItemCategory.CowMilk)]
        ItemTerminaGrottoCowMilk2,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Great Bay Coast Grotto Cow #1"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x138), ItemCategory(ItemCategory.CowMilk)]
        ItemCoastGrottoCowMilk1,

        [Repeatable, Temporary, Overwritable]
        [ItemName("Milk"), LocationName("Great Bay Coast Grotto Cow #2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x139), ItemCategory(ItemCategory.CowMilk)]
        ItemCoastGrottoCowMilk2,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Near Ceiling"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13A), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken1,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Near Ceiling"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13B), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken2,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Torch"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13C), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken3,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Pillar"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13E), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken4,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Jar"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x13F), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken5,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Grass 1"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x140), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken6,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Grass 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x141), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken7,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Water"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x142), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken8,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Lower Left Soft Soil"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x143), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken9,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Crate 1"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x144), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken10,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Upper Soft Soil"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x145), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken11,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Lower Right Soft Soil"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x146), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken12,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Lower Wall"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x147), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken13,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room On Monument"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x148), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken14,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Pillar"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x149), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken15,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Pot 1"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14A), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken16,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Pot 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14B), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken17,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Hive"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14C), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken18,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Upper Pillar"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14D), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken19,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Behind Vines"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14E), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken20,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 1"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x14F), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken21,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Wall"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x150), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken22,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Hive 1"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x151), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken23,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x152), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken24,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Gold Room Wall"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x153), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken25,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Hive"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x154), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken26,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Monument Room Crate 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x155), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken27,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Pot Room Hive 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x156), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken28,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Tree Room Tree 3"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x157), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken29,

        [Visible]
        [ItemName("Swamp Skulltula Spirit"), LocationName("Swamp Skulltula Main Room Jar"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the swamp spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x158), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleSwampSpiderToken30,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Behind Boat"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x159), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken1,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Hole Behind Picture"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15A), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken2,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Hole Behind Cabinet"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15B), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken3,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library On Corner Bookshelf"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15C), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken4,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Ceiling Edge"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15D), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken5,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Ceiling Plank"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15E), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken6,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Ceiling Edge"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x15F), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken7,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Ceiling Edge"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x160), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken8,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Ceiling Web"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x161), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken9,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Behind Crate"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x162), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken10,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Jar"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x163), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken11,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Right Wall"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x164), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken12,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Left Wall"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x165), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken13,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Webbed Hole"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x166), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken14,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Entrance Web"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x167), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken15,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 1"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x168), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken16,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x169), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken17,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Chandelier 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16A), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken18,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Behind Picture"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16B), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken19,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Picture"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16C), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken20,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Bookcase 1"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16D), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken21,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Crate"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16E), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken22,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Webbed Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x16F), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken23,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Upper Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x170), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken24,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Colored Skulls Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x171), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken25,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Storage Room Jar"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x172), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken26,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Lower Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x173), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken27,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula Library Behind Bookcase 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x174), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken28,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Behind Skull 1"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x175), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken29,

        [Visible]
        [ItemName("Ocean Skulltula Spirit"), LocationName("Ocean Skulltula 2nd Room Behind Skull 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a golden spider"), GossipItemHint("a golden token")]
        [ShopText("Collect 30 to lift the curse in the ocean spider house.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x176), ItemCategory(ItemCategory.SkulltulaTokens)]
        CollectibleOceanSpiderToken30,

        [Visible]
        [ItemName("Clock Town Stray Fairy"), LocationName("Clock Town Stray Fairy"), Region(Region.LaundryPool)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Return it to the Fairy Fountain in North Clock Town.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x3B), ItemCategory(ItemCategory.StrayFairies)]
        //[GetItemIndex(0x1A1)] // used as a flag to track if the actual fairy has been collected.
        CollectibleStrayFairyClockTown,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Lower Right Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x177), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall1,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Entrance Fairy"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x178), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall2,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Upper Left Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x179), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall3,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Pillar Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17A), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall4,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Deku Baba"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17B), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall5,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Poison Water Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17C), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall6,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Main Room Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17D), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall7,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Skulltula"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17E), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall8,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Pre-Boss Upper Right Bubble"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x17F), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall9,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Main Room Switch"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x021FB000 + 0x28A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x184), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall10,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Entrance Platform"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02204000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x185), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall11,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Dark Room"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0222E000 + 0x1AA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x186), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall12,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Jar Fairy"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x189), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall13,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Bridge Room Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18A), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall14,

        [Visible]
        [ItemName("Woodfall Stray Fairy"), LocationName("Woodfall Platform Room Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Woodfall.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18B), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyWoodfall15,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Snow Room Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18C), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead1,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Ceiling Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18D), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead2,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Dinolfos 1"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x18E), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead3,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Bridge Room Ledge Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x190), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead4,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Bridge Room Pillar Bubble"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x191), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead5,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Dinolfos 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x192), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead6,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Map Room Fairy"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x193), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead7,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Map Room Ledge"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02346000 + 0x12A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x194), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead8,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Basement"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0230C000 + 0x56A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x195), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead9,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Twin Block"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02306000 + 0x11A, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x196), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead10,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Icicle Room Wall"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0233A000 + 0x22A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x197), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead11,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Main Room Wall"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0230C000 + 0x58A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x198), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead12,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Pillar Freezards"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0232E000 + 0x20A, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x199), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead13,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Ice Puzzle"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x022F2000 + 0x1AA, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x19A), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead14,

        [Visible]
        [ItemName("Snowhead Stray Fairy"), LocationName("Snowhead Crate"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Snowhead.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x19F), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairySnowhead15,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Skulltula"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A7), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay1,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Pre-Boss Room Underwater Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A4), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay2,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Water Control Room Underwater Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A5), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay3,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Pre-Boss Room Bubble"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A6), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay4,

        // A8 empty

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Waterwheel Room Upper"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02940000 + 0x23A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1A9), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay5,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Green Valve"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02959000 + 0x18E, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AA), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay6,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Seesaw Room"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02945000 + 0x24A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AB), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay7,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Waterwheel Room Lower"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02940000 + 0x24A, ChestAttribute.AppearanceType.Normal)]
        [GetItemIndex(0x1AC), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay8,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Entrance Torches"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02962000 + 0x1F2, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1AD), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay9,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Bio Babas"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02911000 + 0xDA, ChestAttribute.AppearanceType.AppearsClear)]
        [GetItemIndex(0x1AE), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay10,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Underwater Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1AF), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay11,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Whirlpool Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B0), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay12,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Whirlpool Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B1), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay13,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Dexihands Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B2), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay14,

        [Visible]
        [ItemName("Great Bay Stray Fairy"), LocationName("Great Bay Ledge Jar"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Great Bay.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1B3), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyGreatBay15,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Mirror Sun Block"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02119000 + 0x282, ChestAttribute.AppearanceType.Normal, 0x0218B000 + 0x8A)]
        [GetItemIndex(0x1B4), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower1,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Eyegore"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1A2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x17E)]
        [GetItemIndex(0x1B5), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower2,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Lava Room Fire Ring"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02122000 + 0x1F6, ChestAttribute.AppearanceType.Normal, 0x02191000 + 0x7A)]
        [GetItemIndex(0x1B6), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower3,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Updraft Fire Ring"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x252, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x29E)]
        [GetItemIndex(0x1B7), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower4,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Mirror Sun Switch"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02119000 + 0x272, ChestAttribute.AppearanceType.AppearsSwitch, 0x0218B000 + 0x7A)]
        [GetItemIndex(0x1B8), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower5,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Boss Warp"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x162, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0xFA)]
        [GetItemIndex(0x1B9), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower6,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Wizzrobe"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x1F2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02182000 + 0x1EE)]
        [GetItemIndex(0x1BA), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower7,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Death Armos"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x172, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0x10A)]
        [GetItemIndex(0x1BB), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower8,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Updraft Frozen Eye"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x262, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2AE)]
        [GetItemIndex(0x1BC), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower9,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Thin Bridge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0211D000 + 0x1E2, ChestAttribute.AppearanceType.AppearsSwitch, 0x0218C000 + 0x25E)]
        [GetItemIndex(0x1BD), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower10,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Basement Ledge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x0210F000 + 0x212, ChestAttribute.AppearanceType.Normal, 0x02182000 + 0x20E)]
        [GetItemIndex(0x1BE), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower11,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Statue Eye"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020E2000 + 0x182, ChestAttribute.AppearanceType.AppearsSwitch, 0x02156000 + 0x11A)]
        [GetItemIndex(0x1BF), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower12,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Underwater"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02104000 + 0x272, ChestAttribute.AppearanceType.AppearsSwitch, 0x02177000 + 0x2BE)]
        [GetItemIndex(0x1C0), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower13,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Bridge Crystal"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x020F1000 + 0x1B2, ChestAttribute.AppearanceType.AppearsSwitch, 0x02164000 + 0x18E)]
        [GetItemIndex(0x1C1), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower14,

        [Visible]
        [ItemName("Stone Tower Stray Fairy"), LocationName("Stone Tower Lava Room Ledge"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("a lost creature"), GossipItemHint("a lost fairy")]
        [ShopText("Collect 15 and return them to the Fairy Fountain in Stone Tower.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold), Chest(0x02122000 + 0x206, ChestAttribute.AppearanceType.Normal, 0x02191000 + 0x8A)]
        [GetItemIndex(0x1C2), ItemCategory(ItemCategory.StrayFairies)]
        CollectibleStrayFairyStoneTower15,

        [Purchaseable]
        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Lottery"), Region(Region.WestClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x86), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemLotteryPurpleRupee,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bank Reward #2"), Region(Region.WestClockTown)]
        [GossipLocationHint("interest"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x13D), ItemCategory(ItemCategory.BlueRupees)]
        MundaneItemBankBlueRupee,

        [Repeatable, Temporary, Overwritable, Purchaseable]
        [ItemName("Chateau Romani"), LocationName("Milk Bar Chateau"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("a dairy product", "an adult beverage")]
        [ShopText("Drink it to get lasting stamina for your magic power.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x180), ItemCategory(ItemCategory.ShopItems)]
        ShopItemMilkBarChateau,

        [Repeatable, Temporary, Overwritable, Purchaseable]
        [ItemName("Milk"), LocationName("Milk Bar Milk"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town shop"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x181), ItemCategory(ItemCategory.ShopItems)]
        ShopItemMilkBarMilk,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Deku Playground Any Day"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [GossipCombineOrder(0), GossipCombine(HeartPieceDekuPlayground, "Deku Playground")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x133), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemDekuPlaygroundPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Honey and Darling Any Day"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [GossipCombineOrder(0), GossipCombine(HeartPieceHoneyAndDarling, "Honey and Darling")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x183), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemHoneyAndDarlingPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Kotake Mushroom Sale"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a sleeping witch", "a southern merchant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x187), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemKotakeMushroomSaleRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pictograph Contest Standard Photo"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x188), ItemCategory(ItemCategory.BlueRupees)]
        MundaneItemPictographContestBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Pictograph Contest Good Photo"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x18F), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemPictographContestRedRupee,

        [Repeatable, Temporary, Purchaseable]
        [ItemName("Magic Bean"), LocationName("Swamp Scrub Purchase"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a southern merchant"), GossipItemHint("a plant seed")]
        [ChestType(ChestTypeAttribute.ChestType.LargeGold)]
        [ShopText("Plant it in soft soil.")]
        [GetItemIndex(0x19B), ItemCategory(ItemCategory.ShopItems)]
        ShopItemBusinessScrubMagicBean,

        [Repeatable, Temporary, Overwritable, Purchaseable]
        [ItemName("Green Potion"), LocationName("Ocean Scrub Purchase"), Region(Region.ZoraHall)]
        [GossipLocationHint("a western merchant"), GossipItemHint("a magic potion", "a green drink")]
        [ShopText("Replenishes your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x19C), ItemCategory(ItemCategory.ShopItems)]
        ShopItemBusinessScrubGreenPotion,

        [Repeatable, Temporary, Overwritable, Purchaseable]
        [ItemName("Blue Potion"), LocationName("Canyon Scrub Purchase"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("an eastern merchant"), GossipItemHint("consumable strength", "a magic potion", "a blue drink")]
        [ShopText("Replenishes both life energy and magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x19D), ItemCategory(ItemCategory.ShopItems)]
        ShopItemBusinessScrubBluePotion,

        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Zora Hall Stage Lights"), Region(Region.ZoraHall)]
        [GossipLocationHint("a good deed"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x19E), ItemCategory(ItemCategory.BlueRupees)]
        MundaneItemZoraStageLightsBlueRupee,

        [Repeatable, Temporary, Overwritable, Purchaseable]
        [ItemName("Milk"), LocationName("Gorman Bros Milk Purchase"), Region(Region.MilkRoad)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("a dairy product", "the produce of cows")]
        [ShopText("Recover five hearts with one drink. Contains two helpings.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x1A0), ItemCategory(ItemCategory.ShopItems)]
        ShopItemGormanBrosMilk,

        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Ocean Spider House Day 2 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false)]
        [GossipCombineOrder(1), GossipCombine(UpgradeGiantWallet, "Ocean Spider House"), GossipCombine(MundaneItemOceanSpiderHouseDay3RedRupee, "Ocean Spider House")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x134), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemOceanSpiderHouseDay2PurpleRupee,

        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ocean Spider House Day 3 Reward"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a gold spider"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff"), GossipCompetitiveHint(0, ItemCategory.SkulltulaTokens, false)]
        [GossipCombineOrder(2), GossipCombine(MundaneItemOceanSpiderHouseDay2PurpleRupee, "Ocean Spider House"), GossipCombine(UpgradeGiantWallet, "Ocean Spider House")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1A3), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemOceanSpiderHouseDay3RedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bad Pictograph of Lulu"), Region(Region.ZoraHall)]
        [GossipLocationHint("a fan"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1A8), ItemCategory(ItemCategory.BlueRupees)]
        MundaneItemLuluBadPictographBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Good Pictograph of Lulu"), Region(Region.ZoraHall)]
        [GossipLocationHint("a fan"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C3), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemLuluGoodPictographRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Treasure Chest Game Human"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFA8, ChestAttribute.AppearanceType.AppearsSwitch, 0x00F43F10 + 0xFB0)]
        [GetItemIndex(0x1C4), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemTreasureChestGamePurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Treasure Chest Game Zora"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAC, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1C5), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemTreasureChestGameRedRupee,

        [RupeeRepeatable]
        [Repeatable, Temporary]
        [ItemName("10 Deku Nuts"), LocationName("Treasure Chest Game Deku"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town game"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden), Chest(0x00F43F10 + 0xFAE, ChestAttribute.AppearanceType.AppearsSwitch)]
        [GetItemIndex(0x1C6), ItemCategory(ItemCategory.Ammo)]
        MundaneItemTreasureChestGameDekuNuts,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Curiosity Shop Blue Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C7), ItemCategory(ItemCategory.BlueRupees)]
        MundaneItemCuriosityShopBlueRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Curiosity Shop Red Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C8), ItemCategory(ItemCategory.RedRupees)]
        MundaneItemCuriosityShopRedRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Curiosity Shop Purple Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1C9), ItemCategory(ItemCategory.PurpleRupees)]
        MundaneItemCuriosityShopPurpleRupee,

        [RupeeRepeatable]
        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Curiosity Shop Gold Rupee"), Region(Region.WestClockTown)]
        [GossipLocationHint("a shady gentleman", "a dodgy seller", "a shady dealer"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CA), ItemCategory(ItemCategory.GoldRupees)]
        MundaneItemCuriosityShopGoldRupee,

        [Visible, Purchaseable]
        [Repeatable, Temporary, Overwritable]
        [ItemName("Seahorse"), LocationName("Fisherman Pictograph"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a fisherman"), GossipItemHint("a sea creature")]
        [ShopText("It wants to go back home to Pinnacle Rock.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallGold)]
        [GetItemIndex(0x95), ItemCategory(ItemCategory.Misc)]
        MundaneItemSeahorse,

        //[GetItemIndex(0x1A1)]

        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Castle Courtyard Grass"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CC), ItemCategory(ItemCategory.Ammo), CollectableIndex(0xEB7)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Castle Courtyard Grass 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CD), ItemCategory(ItemCategory.Ammo), CollectableIndex(0xEBB)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 1 Grave Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CE), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x623)]
        CollectableBeneathTheGraveyardMainAreaPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 2 Grave Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1CF), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x625)]
        CollectableBeneathTheGraveyardInvisibleRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Night 1 Grave Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D0), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x629)]
        CollectableBeneathTheGraveyardBadBatRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Cucco Shack Crate"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a chicken crate"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D1), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x210A)]
        CollectableCuccoShackWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D2), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1807)]
        CollectableDampéSHouseBasementPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D3), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x180A)]
        CollectableDampéSHouseBasementPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D4), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x180C)]
        CollectableDampéSHouseBasementPot3,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Dampe's Basement Pot 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D5), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x180D)]
        CollectableDampéSHouseBasementPot4,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Goron Village Small Snowball"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D6), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26B3)]
        CollectableGoronVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Goron Village Small Snowball 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D7), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26B4)]
        CollectableGoronVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D8), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BA4)]
        CollectableGreatBayCoastPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1D9), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BA6)]
        CollectableGreatBayCoastPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DA), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BA8)]
        CollectableGreatBayCoastPot3,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Coast Pot 4"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DB), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BAA)]
        CollectableGreatBayCoastPot4,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Great Bay Temple Red Valve Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DC), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x24A1)]
        CollectableGreatBayTempleBlueChuchuValveRoomBarrel1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DD), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2B20)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DE), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2B23)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Entry Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1DF), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2B2E)]
        CollectableIgosDuIkanaSLairPreBossRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana King Entry Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E0), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2B2F)]
        CollectableIgosDuIkanaSLairPreBossRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Ikana Graveyard Grass"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E1), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x21A9)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Entrance Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E2), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1430)]
        CollectableOceansideSpiderHouseEntrancePot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Entrance Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E3), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1431)]
        CollectableOceansideSpiderHouseEntrancePot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Sewer Gate Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E4), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11A5)]
        CollectablePiratesFortressInteriorWaterCurrentRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Guarded Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E5), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11AA)]
        CollectablePiratesFortressInterior100RupeeEggRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Barrel Maze Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E6), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11AB)]
        CollectablePiratesFortressInteriorBarrelRoomEggPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Pirates' Fortress Sewer Exit Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E7), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11AC)]
        CollectablePiratesFortressInteriorTelescopeRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Secret Shrine Underwater Pot"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E8), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x307C)]
        CollectableSecretShrineMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Secret Shrine Underwater Pot 2"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1E9), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x307D)]
        CollectableSecretShrineMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Snowhead Temple Icicle Room Snowball"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EA), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x10A0)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Snowhead Temple Icicle Room Snowball 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EB), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x10A1)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Stone Tower Upper Scarecrow Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EC), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2C2F)]
        CollectableStoneTowerPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Stone Tower Upper Scarecrow Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1ED), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2C33)]
        CollectableStoneTowerPot2,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Coast Pot 5"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EE), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BAE)]
        CollectableGreatBayCoastPot5,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Temple Seesaw Room Pot"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1EF), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x24A0)]
        CollectableGreatBayTempleSeesawRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Great Bay Temple Green Pump Barrel"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F0), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x24A4)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ikana Canyon Grass"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F1), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x9C8)]
        CollectableIkanaCanyonMainAreaGrass1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Milk Road Grass"), Region(Region.MilkRoad)]
        [GossipLocationHint("a roadside plant"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F2), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1121)]
        CollectableMilkRoadGrass1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Mountain Village Spring Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F3), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2D2A)]
        CollectableMountainVillageSpringSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Mountain Village Winter Small Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F4), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2835)]
        CollectableMountainVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Pirates' Fortress Lone Guard Egg Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F5), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11A9)]
        CollectablePiratesFortressInteriorTwinBarrelEggRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Pirates' Fortress Cage Pot"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F6), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1188)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartPot1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ranch Crate"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a ranch container"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F7), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1AFF)]
        CollectableRomaniRanchWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Snowhead Small Snowball"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F8), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2E21)]
        CollectableSnowheadSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Owl Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1F9), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2C38)]
        CollectableStoneTowerPot3,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Zora Cape Owl Pot"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FA), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1C23)]
        CollectableZoraCapePot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Observatory Scarecrow Pot"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FB), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x14A4)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Observatory Scarecrow Pot 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FC), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x14A5)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FD), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1590)]
        CollectableDekuPalaceWestInnerGardenItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FE), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1591)]
        CollectableDekuPalaceEastInnerGardenItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x1FF), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1592)]
        CollectableDekuPalaceEastInnerGardenItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x200), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x158E)]
        CollectableDekuPalaceWestInnerGardenItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x201), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x158F)]
        CollectableDekuPalaceWestInnerGardenItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x202), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2081)]
        CollectableDoggyRacetrackPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x203), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2082)]
        CollectableDoggyRacetrackPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x204), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2083)]
        CollectableDoggyRacetrackPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Doggy Racetrack Pot 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x205), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2084)]
        CollectableDoggyRacetrackPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x206), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x26A0)]
        CollectableGoronVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x207), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x26A2)]
        CollectableGoronVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Goron Village Large Snowball 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x208), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x26A4)]
        CollectableGoronVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x209), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1B83)]
        CollectableGreatBayCoastPot6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20A), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1B84)]
        CollectableGreatBayCoastPot7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Coast Ledge Pot 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a high ocean jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20B), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1B86)]
        CollectableGreatBayCoastPot8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Temple Water Control Room Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20C), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2482)]
        CollectableGreatBayTempleWaterControlRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Great Bay Temple Water Control Room Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20D), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2483)]
        CollectableGreatBayTempleWaterControlRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bio Baba Grotto Hive"), Region(Region.TerminaField)]
        [GossipLocationHint("an underground hive"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20E), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x383)]
        CollectableGrottosOceanHeartPieceGrottoBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Laundry Pool Crate"), Region(Region.LaundryPool)]
        [GossipLocationHint("a town crate"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x20F), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x3820)]
        CollectableLaundryPoolWoodenCrateSmall1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Mountain Village Day 3 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x210), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2828)]
        CollectableMountainVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Mountain Village Day 2 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x211), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2829)]
        CollectableMountainVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x212), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2E81)]
        CollectablePathToGoronVillageWinterItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x213), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2E82)]
        CollectablePathToGoronVillageWinterItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x214), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2E83)]
        CollectablePathToGoronVillageWinterItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Twin Islands Item 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a frozen lake"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x215), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2E84)]
        CollectablePathToGoronVillageWinterItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Barrel Maze Egg Pot 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x216), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x11A3)]
        CollectablePiratesFortressInteriorBarrelRoomEggPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x217), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1185)]
        CollectablePiratesFortressInteriorTelescopeRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x218), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1186)]
        CollectablePiratesFortressInteriorTelescopeRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Sewer Exit Barrel 3"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x219), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x118A)]
        CollectablePiratesFortressInteriorTelescopeRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Pirates' Fortress Cage Room Barrel"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21A), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x118B)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Barn Hay Item"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a bale of hay"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21B), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x801)]
        CollectableRanchHouseBarnBarnItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Barn Hay Item 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a bale of hay"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21C), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x802)]
        CollectableRanchHouseBarnBarnItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21D), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10A2)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 4"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21E), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10A3)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Icicle Room Snowball 5"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x21F), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10A4)]
        CollectableSnowheadTempleIceBlockRoomSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x220), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10A8)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x221), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10A9)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x222), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10AA)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 4"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x223), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10AB)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Elevator Room Crate 5"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x224), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10AC)]
        CollectableSnowheadTempleMapRoomWoodenCrateLarge5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Safety Bridge Pot"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x225), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10AD)]
        CollectableSnowheadTempleMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Snowhead Temple Safety Bridge Pot 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x226), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x10AE)]
        CollectableSnowheadTempleMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Cleared Swamp Potion Shop Pot"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x227), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x21)]
        CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Near Frog Item"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp flower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x228), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2295)]
        CollectableSouthernSwampPoisonedCentralSwampItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Near Frog Item 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp flower"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x229), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2296)]
        CollectableSouthernSwampPoisonedCentralSwampItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Potion Shop Pot"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22A), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x22A6)]
        CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Lava Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22B), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB05)]
        CollectableStoneTowerTempleLavaRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Lava Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22C), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB06)]
        CollectableStoneTowerTempleLavaRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22D), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB07)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22E), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB08)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x22F), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB09)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 4"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x230), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB0A)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 5"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x231), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB0B)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 6"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x232), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB0C)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 7"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x233), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB0D)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Stone Tower Temple Thin Bridge Item 8"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x234), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xB0E)]
        CollectableStoneTowerTempleRoomAfterLightArrowsItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x235), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC01)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Closest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x236), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC10)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Closest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x237), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC11)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x238), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC07)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x239), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC08)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23A), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC09)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Furthest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23B), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0A)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Furthest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23C), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0B)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23D), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0C)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Furthest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23E), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0D)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss Closest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x23F), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0E)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Inverted Stone Tower Temple Pre-Boss 2nd Closest Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x240), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xC0F)]
        CollectableStoneTowerTempleInvertedPreBossRoomItem11,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x241), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2A01)]
        CollectableSwordsmanSSchoolPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 2"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x242), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2A03)]
        CollectableSwordsmanSSchoolPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 3"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x243), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2A04)]
        CollectableSwordsmanSSchoolPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 4"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x244), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2A05)]
        CollectableSwordsmanSSchoolPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman's School Pot 5"), Region(Region.WestClockTown)]
        [GossipLocationHint("cowering"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x245), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2A06)]
        CollectableSwordsmanSSchoolPot5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Item"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned stump"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x246), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x2301)]
        CollectableWoodfallItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Entrance Hive"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x247), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xDA0)]
        CollectableWoodfallTempleEntranceRoomBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x248), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xDA6)]
        CollectableWoodfallTempleGekkoRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 2"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x249), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xDA7)]
        CollectableWoodfallTempleGekkoRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 3"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24A), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xDA8)]
        CollectableWoodfallTempleGekkoRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Gekko Room Pot 4"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24B), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xDA9)]
        CollectableWoodfallTempleGekkoRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24C), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xD83)]
        CollectableWoodfallTemplePreBossRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 2"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24D), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xD84)]
        CollectableWoodfallTemplePreBossRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 3"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24E), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xD85)]
        CollectableWoodfallTemplePreBossRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Woodfall Temple Pre-Boss Platform Item 4"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x24F), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0xD86)]
        CollectableWoodfallTemplePreBossRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x250), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x25A0)]
        CollectableBeneathTheWellBugAndBombRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 2"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x251), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x25A1)]
        CollectableBeneathTheWellBugAndBombRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 3"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x252), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x25A2)]
        CollectableBeneathTheWellBugAndBombRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 4"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x253), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x25A3)]
        CollectableBeneathTheWellBugAndBombRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Well Left Path Pot 5"), Region(Region.BeneathTheWell)]
        [GossipLocationHint("a cursed pot"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x254), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x25A4)]
        CollectableBeneathTheWellBugAndBombRoomPot5,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Goron Village Small Snowball 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x255), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26AB)]
        CollectableGoronVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Goron Village Small Snowball 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x256), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26B0)]
        CollectableGoronVillageWinterSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Coast Pot 6"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x257), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1BAD)]
        CollectableGreatBayCoastPot9,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Temple Red Valve Barrel 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x258), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x24A2)]
        CollectableGreatBayTempleBlueChuchuValveRoomBarrel2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Great Bay Temple Green Pump Barrel 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x259), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x24A5)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Ikana Canyon Grass 2"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25A), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x9C7)]
        CollectableIkanaCanyonMainAreaGrass2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Mountain Village Spring Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25B), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2D29)]
        CollectableMountainVillageSpringSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Mountain Village Winter Small Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25C), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2834)]
        CollectableMountainVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Snowhead Small Snowball 2"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25D), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2E20)]
        CollectableSnowheadSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Stone Tower Owl Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25E), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2C37)]
        CollectableStoneTowerPot4,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Inverted Stone Tower Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x25F), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2CBA)]
        CollectableStoneTowerInvertedStoneTowerFlippedPot1,


        [Visible]
        [Repeatable]
        [ItemName("5 Bombs"), LocationName("Zora Cape Owl Pot 2"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("explosives")]
        [ShopText("Explosives. You need a Bomb Bag to carry them.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x260), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1C22)]
        CollectableZoraCapePot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Ikana Castle Left Staircase Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x261), ItemCategory(ItemCategory.Ammo), CollectableIndex(0xEC4)]
        CollectableAncientCastleOfIkana1FWestStaircasePot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Goron Village Small Snowball 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x262), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26AD)]
        CollectableGoronVillageWinterSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Goron Village Small Snowball 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x263), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26B1)]
        CollectableGoronVillageWinterSmallSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Pirates' Fortress Sewer Exit Pot 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x264), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11A8)]
        CollectablePiratesFortressInteriorTelescopeRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("10 Deku Nuts"), LocationName("Woodfall Pot"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned platform"), GossipItemHint("a flashing impact")]
        [ShopText("Its flash blinds enemies.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x265), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2322)]
        CollectableWoodfallPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x266), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1960)]
        CollectableGoronShrineGoronKidSRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 2"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x267), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1961)]
        CollectableGoronShrineGoronKidSRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 3"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x268), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1963)]
        CollectableGoronShrineMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x269), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1966)]
        CollectableGoronShrineMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Shrine Pot 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26A), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x196A)]
        CollectableGoronShrineMainRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Village Small Snowball 7"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26B), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26AC)]
        CollectableGoronVillageWinterSmallSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Goron Village Small Snowball 8"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26C), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26AF)]
        CollectableGoronVillageWinterSmallSnowball8,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Cleared Swamp Owl Grass"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26D), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x26)]
        CollectableSouthernSwampClearCentralSwampGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Southern Swamp Owl Grass"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("swamp grass"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26E), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x22AC)]
        CollectableSouthernSwampPoisonedCentralSwampGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Deku Stick"), LocationName("Woodfall Pot 2"), Region(Region.Woodfall)]
        [GossipLocationHint("a poisoned platform"), GossipItemHint("a flammable weapon", "a flimsy weapon")]
        [ShopText("Deku Sticks burn well. You can only carry 10.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x26F), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x2320)]
        CollectableWoodfallPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x270), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1805)]
        CollectableDampéSHouseBasementPot5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x271), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1806)]
        CollectableDampéSHouseBasementPot6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Dampe's Basement Pot 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x272), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1809)]
        CollectableDampéSHouseBasementPot7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x273), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1581)]
        CollectableDekuPalaceEastInnerGardenItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x274), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1582)]
        CollectableDekuPalaceEastInnerGardenItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 8"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x275), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1583)]
        CollectableDekuPalaceEastInnerGardenItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 9"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x276), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1584)]
        CollectableDekuPalaceEastInnerGardenItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 10"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x277), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1585)]
        CollectableDekuPalaceEastInnerGardenItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 11"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x278), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1586)]
        CollectableDekuPalaceEastInnerGardenItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 12"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x279), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1587)]
        CollectableDekuPalaceWestInnerGardenItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 13"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27A), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1588)]
        CollectableDekuPalaceWestInnerGardenItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 14"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27B), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1589)]
        CollectableDekuPalaceWestInnerGardenItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Out of Bounds Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a hidden royal treasure"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27C), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x158A)]
        CollectableDekuPalaceWestInnerGardenItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 15"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27D), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x158B)]
        CollectableDekuPalaceWestInnerGardenItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 16"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27E), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x158C)]
        CollectableDekuPalaceWestInnerGardenItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Item 17"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal garden", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x27F), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x158D)]
        CollectableDekuPalaceWestInnerGardenItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x280), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2920)]
        CollectableDekuShrineGiantRoomFloor1Item1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x281), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2921)]
        CollectableDekuShrineGiantRoomFloor1Item2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x282), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2922)]
        CollectableDekuShrineGiantRoomFloor1Item3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x283), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2923)]
        CollectableDekuShrineGiantRoomFloor1Item4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x284), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2924)]
        CollectableDekuShrineGiantRoomFloor1Item5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Pillar Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x285), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2925)]
        CollectableDekuShrineGiantRoomFloor1Item6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x286), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2926)]
        CollectableDekuShrineWaterRoomWithPlatformsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x287), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2927)]
        CollectableDekuShrineWaterRoomWithPlatformsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x288), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2928)]
        CollectableDekuShrineWaterRoomWithPlatformsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x289), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2929)]
        CollectableDekuShrineWaterRoomWithPlatformsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28A), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292A)]
        CollectableDekuShrineWaterRoomWithPlatformsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race River Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28B), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292B)]
        CollectableDekuShrineWaterRoomWithPlatformsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28C), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292C)]
        CollectableDekuShrineRoomBeforeFlameWallsItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28D), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292D)]
        CollectableDekuShrineRoomBeforeFlameWallsItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28E), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292E)]
        CollectableDekuShrineRoomBeforeFlameWallsItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x28F), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x292F)]
        CollectableDekuShrineRoomBeforeFlameWallsItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x290), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2930)]
        CollectableDekuShrineRoomBeforeFlameWallsItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Right Path Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x291), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2931)]
        CollectableDekuShrineRoomBeforeFlameWallsItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x292), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2932)]
        CollectableDekuShrineDekuButlerSRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x293), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2933)]
        CollectableDekuShrineDekuButlerSRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x294), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2934)]
        CollectableDekuShrineDekuButlerSRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x295), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2935)]
        CollectableDekuShrineDekuButlerSRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x296), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2936)]
        CollectableDekuShrineDekuButlerSRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x297), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2937)]
        CollectableDekuShrineDekuButlerSRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x298), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2938)]
        CollectableDekuShrineDekuButlerSRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 8"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x299), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2939)]
        CollectableDekuShrineDekuButlerSRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 9"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29A), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x293A)]
        CollectableDekuShrineDekuButlerSRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Final Room Item 10"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29B), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x293B)]
        CollectableDekuShrineDekuButlerSRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Butler Race Dual Pot"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29C), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x297F)]
        CollectableDekuShrineGreyBoulderRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Crate"), Region(Region.EastClockTown)]
        [GossipLocationHint("a town crate"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29D), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3601)]
        CollectableEastClockTownWoodenCrateSmall1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Great Bay Temple Water Control Room Item 3"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29E), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2484)]
        CollectableGreatBayTempleWaterControlRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Great Bay Temple Water Control Room Item 4"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x29F), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x2485)]
        CollectableGreatBayTempleWaterControlRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Grass 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A0), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x21A5)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Potion Shop Item"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a shop corner"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A1), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x501)]
        CollectableMagicHagsPotionShopItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 2"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A2), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1181)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 3"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A3), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1183)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 4"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A4), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1184)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Cage Room Barrel 5"), Region(Region.PiratesFortressSewer)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A5), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x1187)]
        CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A6), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3032)]
        CollectableSecretShrineEntranceRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 2"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A7), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3033)]
        CollectableSecretShrineEntranceRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 3"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A8), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3034)]
        CollectableSecretShrineEntranceRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 4"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2A9), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3035)]
        CollectableSecretShrineEntranceRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 5"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AA), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3036)]
        CollectableSecretShrineEntranceRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 6"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AB), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3037)]
        CollectableSecretShrineEntranceRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 7"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AC), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3038)]
        CollectableSecretShrineEntranceRoomItem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 8"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AD), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3039)]
        CollectableSecretShrineEntranceRoomItem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 9"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AE), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303A)]
        CollectableSecretShrineEntranceRoomItem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 10"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2AF), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303B)]
        CollectableSecretShrineEntranceRoomItem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 11"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B0), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303C)]
        CollectableSecretShrineEntranceRoomItem11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 12"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B1), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303D)]
        CollectableSecretShrineEntranceRoomItem12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 13"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B2), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303E)]
        CollectableSecretShrineEntranceRoomItem13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 14"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B3), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x303F)]
        CollectableSecretShrineEntranceRoomItem14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 15"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B4), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3040)]
        CollectableSecretShrineEntranceRoomItem15,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 16"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B5), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3041)]
        CollectableSecretShrineEntranceRoomItem16,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Secret Shrine Floating Item 17"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B6), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x3042)]
        CollectableSecretShrineEntranceRoomItem17,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cleared Swamp Potion Shop Pot 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B7), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x22)]
        CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Potion Shop Pot 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B8), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0x22A7)]
        CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2B9), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0xB02)]
        CollectableStoneTowerTempleLavaRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 4"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BA), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0xB03)]
        CollectableStoneTowerTempleLavaRoomItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stone Tower Temple Lava Room Item 5"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BB), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0xB04)]
        CollectableStoneTowerTempleLavaRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BC), ItemCategory(ItemCategory.GreenRupees), CollectableIndex(0xC03)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot"), Region(Region.TheMoon)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BD), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xCC0)]
        CollectableClockTowerRooftopPot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BE), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xCC1)]
        CollectableClockTowerRooftopPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 3"), Region(Region.TheMoon)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2BF), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xCC2)]
        CollectableClockTowerRooftopPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Clock Tower Rooftop Pot 4"), Region(Region.TheMoon)]
        [GossipLocationHint("a rooftop pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C0), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xCC3)]
        CollectableClockTowerRooftopPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C1), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A0)]
        CollectableGoronRacetrackPot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C2), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A1)]
        CollectableGoronRacetrackPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C3), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A2)]
        CollectableGoronRacetrackPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C4), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A3)]
        CollectableGoronRacetrackPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 5"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C5), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A4)]
        CollectableGoronRacetrackPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 6"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C6), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A5)]
        CollectableGoronRacetrackPot6,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 7"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C7), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A6)]
        CollectableGoronRacetrackPot7,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 8"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C8), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A7)]
        CollectableGoronRacetrackPot8,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 9"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2C9), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A8)]
        CollectableGoronRacetrackPot9,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 10"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CA), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35A9)]
        CollectableGoronRacetrackPot10,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 11"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CB), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AA)]
        CollectableGoronRacetrackPot11,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 12"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CC), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AB)]
        CollectableGoronRacetrackPot12,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 13"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CD), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AC)]
        CollectableGoronRacetrackPot13,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 14"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CE), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AD)]
        CollectableGoronRacetrackPot14,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 15"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2CF), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AE)]
        CollectableGoronRacetrackPot15,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 16"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D0), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35AF)]
        CollectableGoronRacetrackPot16,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 17"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D1), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B0)]
        CollectableGoronRacetrackPot17,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 18"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D2), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B1)]
        CollectableGoronRacetrackPot18,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 19"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D3), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B2)]
        CollectableGoronRacetrackPot19,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 20"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D4), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B3)]
        CollectableGoronRacetrackPot20,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 21"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D5), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B4)]
        CollectableGoronRacetrackPot21,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 22"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D6), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B5)]
        CollectableGoronRacetrackPot22,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 23"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D7), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B6)]
        CollectableGoronRacetrackPot23,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 24"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D8), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B7)]
        CollectableGoronRacetrackPot24,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 25"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2D9), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B8)]
        CollectableGoronRacetrackPot25,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 26"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DA), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35B9)]
        CollectableGoronRacetrackPot26,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Racetrack Pot 27"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DB), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35BD)]
        CollectableGoronRacetrackPot27,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DC), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1962)]
        CollectableGoronShrineGoronKidSRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 7"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DD), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1967)]
        CollectableGoronShrineMainRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 8"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DE), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1968)]
        CollectableGoronShrineMainRoomPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Shrine Pot 9"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2DF), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1969)]
        CollectableGoronShrineMainRoomPot6,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Great Bay Coast Pot 7"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("an ocean jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E0), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1BAC)]
        CollectableGreatBayCoastPot10,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Great Bay Temple Red Valve Crate"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E1), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x24B2)]
        CollectableGreatBayTempleBlueChuchuValveRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Ikana King Pot 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E2), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2B21)]
        CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Ikana Canyon Grass 3"), Region(Region.IkanaCanyon)]
        [GossipLocationHint("cursed grass"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E3), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x9C6)]
        CollectableIkanaCanyonMainAreaGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Milk Road Grass 2"), Region(Region.MilkRoad)]
        [GossipLocationHint("a roadside plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E4), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1122)]
        CollectableMilkRoadGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Mountain Village Spring Snowball 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a spring snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E5), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2D2C)]
        CollectableMountainVillageSpringSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Graveyard Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a high snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E6), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x282D)]
        CollectableMountainVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Goron Graveyard Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a high snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E7), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x282E)]
        CollectableMountainVillageWinterSmallSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Mountain Village Winter Small Snowball 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E8), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2837)]
        CollectableMountainVillageWinterSmallSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Snowhead Small Snowball 3"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2E9), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E23)]
        CollectableSnowheadSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Stone Tower Owl Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EA), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C3A)]
        CollectableStoneTowerPot5,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Inverted Stone Tower Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a sky below"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EB), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2CB8)]
        CollectableStoneTowerInvertedStoneTowerFlippedPot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EC), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x3320)]
        CollectableTheMoonLinkTrialEntrancePot1,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 2"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2ED), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x3321)]
        CollectableTheMoonLinkTrialEntrancePot2,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 3"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EE), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x3322)]
        CollectableTheMoonLinkTrialEntrancePot3,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Link Trial Pot 4"), Region(Region.TheMoon)]
        [GossipLocationHint("a trial jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2EF), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x3323)]
        CollectableTheMoonLinkTrialEntrancePot4,


        [Visible]
        [Repeatable]
        [ItemName("Large Magic Jar"), LocationName("Zora Cape Owl Pot 3"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a large amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F0), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1C25)]
        CollectableZoraCapePot3,


        [Visible]
        [Repeatable]
        [ItemName("Purple Rupee"), LocationName("Dampe's Basement Pot 8"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 50 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F1), ItemCategory(ItemCategory.PurpleRupees), CollectableIndex(0x180B)]
        CollectableDampéSHouseBasementPot8,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F2), ItemCategory(ItemCategory.RecoveryHearts), CollectableIndex(0xA20)]
        CollectablePiratesFortressItem1,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F3), ItemCategory(ItemCategory.RecoveryHearts), CollectableIndex(0xA21)]
        CollectablePiratesFortressItem2,


        [Visible]
        [Repeatable]
        [ItemName("Recovery Heart"), LocationName("Pirates' Fortress Item 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("health")]
        [ShopText("Replenishes a small amount of your life energy.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F4), ItemCategory(ItemCategory.RecoveryHearts), CollectableIndex(0xA22)]
        CollectablePiratesFortressItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Butler Race Pillar Item 7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F5), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x293C)]
        CollectableDekuShrineGiantRoomFloor1Item7,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Butler Race Pillar Item 8"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal race"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F6), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x293D)]
        CollectableDekuShrineGiantRoomFloor1Item8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Water Control Room Item 5"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F7), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2481)]
        CollectableGreatBayTempleWaterControlRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Dexihand Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F8), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2486)]
        CollectableGreatBayTempleCompassBossKeyRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Dexihand Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2F9), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2487)]
        CollectableGreatBayTempleCompassBossKeyRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Green Pump Item"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FA), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2488)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Temple Green Pump Item 2"), Region(Region.GreatBayTemple)]
        [GossipLocationHint("Great Bay Temple", "the ocean temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FB), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2489)]
        CollectableGreatBayTempleTopmostRoomWithGreenValveItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FC), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x3803)]
        CollectableLaundryPoolItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item 2"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FD), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x3804)]
        CollectableLaundryPoolItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Laundry Pool Item 3"), Region(Region.LaundryPool)]
        [GossipLocationHint("a floating item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FE), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x3805)]
        CollectableLaundryPoolItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Village Spring Stair Item"), Region(Region.MountainVillage)]
        [GossipLocationHint("an item under the stairs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x2FF), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x2809)]
        CollectableMountainVillageWinterMountainVillageSpringItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x300), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x1081)]
        CollectableSnowheadTempleIceBlockRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item 2"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x301), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x1082)]
        CollectableSnowheadTempleIceBlockRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Snowhead Temple Icicle Room Frozen Item 3"), Region(Region.SnowheadTemple)]
        [GossipLocationHint("Snowhead Temple", "an icy gale"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x302), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x1083)]
        CollectableSnowheadTempleIceBlockRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Swamp Near Frog Hive"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a swamp hive"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x303), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x22A8)]
        CollectableSouthernSwampPoisonedCentralSwampBeehive1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Lava Room Item 6"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x304), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xB01)]
        CollectableStoneTowerTempleLavaRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Eyegore Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x305), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xB10)]
        CollectableStoneTowerTempleEyegoreRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Mirror Room Crate"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x306), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xB25)]
        CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Mirror Room Crate 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x307), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xB26)]
        CollectableStoneTowerTempleMirrorRoomWoodenCrateLarge2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Stone Tower Temple Eyegore Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x308), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xB0F)]
        CollectableStoneTowerTempleEyegoreRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Dexihand Item 3"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x309), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xC02)]
        CollectableStoneTowerTempleInvertedEyegoreRoomItem3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Updraft Room Item"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30A), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xC04)]
        CollectableStoneTowerTempleInvertedAirRoomItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Inverted Stone Tower Temple Updraft Room Item 2"), Region(Region.StoneTowerTemple)]
        [GossipLocationHint("Stone Tower Temple", "the cursed temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30B), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xC05)]
        CollectableStoneTowerTempleInvertedAirRoomItem2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Pillar Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a pillar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30C), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x1682)]
        CollectableTerminaFieldItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Woodfall Temple Pre-Boss Pillar Item"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30D), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xD81)]
        CollectableWoodfallTemplePreBossRoomItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Woodfall Temple Pre-Boss Pillar Item 2"), Region(Region.WoodfallTemple)]
        [GossipLocationHint("Woodfall Temple", "the sleeping temple"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30E), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0xD82)]
        CollectableWoodfallTemplePreBossRoomItem6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Courtyard Grass 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x30F), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xEB4)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Courtyard Grass 4"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient plant"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x310), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xEB6)]
        CollectableAncientCastleOfIkanaCastleExteriorGrass4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Fire Ceiling Room Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x311), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xEC1)]
        CollectableAncientCastleOfIkanaFireCeilingRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Hole Room Pot"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x312), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xECF)]
        CollectableAncientCastleOfIkanaHoleRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Castle Hole Room Pot 2"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x313), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xEDA)]
        CollectableAncientCastleOfIkanaHoleRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Balloon Pot"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x314), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x14A1)]
        CollectableAstralObservatorySewerPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Balloon Pot 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x315), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x14A2)]
        CollectableAstralObservatorySewerPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Observatory Scarecrow Pot 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an underground jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x316), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x14A6)]
        CollectableAstralObservatoryObservatoryBombersHideoutPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Night 2 Grave Pot 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x317), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x62C)]
        CollectableBeneathTheGraveyardMainAreaPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Deku Palace Pot"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal pathway", "the home of scrubs"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x318), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x15B2)]
        CollectableDekuPalaceEastInnerGardenPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Deku Palace Pot 2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal pathway", "the home of scrubs"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x319), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x15B3)]
        CollectableDekuPalaceEastInnerGardenPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 28"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31A), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35BA)]
        CollectableGoronRacetrackPot28,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 29"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31B), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35BB)]
        CollectableGoronRacetrackPot29,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Racetrack Pot 30"), Region(Region.TwinIslands)]
        [GossipLocationHint("a racetrack jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31C), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x35BC)]
        CollectableGoronRacetrackPot30,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Shrine Pot 10"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31D), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1964)]
        CollectableGoronShrineMainRoomPot7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Shrine Pot 11"), Region(Region.GoronVillage)]
        [GossipLocationHint("a crying child's jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31E), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1965)]
        CollectableGoronShrineMainRoomPot8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 4"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x31F), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x26A1)]
        CollectableGoronVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 5"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x320), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x26A3)]
        CollectableGoronVillageWinterLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Large Snowball 6"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x321), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x26A5)]
        CollectableGoronVillageWinterLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Small Snowball 9"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x322), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x26AE)]
        CollectableGoronVillageWinterSmallSnowball9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Goron Village Small Snowball 10"), Region(Region.GoronVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x323), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x26B2)]
        CollectableGoronVillageWinterSmallSnowball10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana King Entry Pot 3"), Region(Region.IkanaCastle)]
        [GossipLocationHint("an ancient jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x324), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2B30)]
        CollectableIgosDuIkanaSLairPreBossRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Ikana Graveyard Grass 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("unholy grass"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x325), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x21A6)]
        CollectableIkanaGraveyardIkanaGraveyardLowerGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Winter Small Snowball 4"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x326), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2821)]
        CollectableMountainVillageWinterSmallSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Winter Small Snowball 5"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x327), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2827)]
        CollectableMountainVillageWinterSmallSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Day 1 Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x328), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x282A)]
        CollectableMountainVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Mountain Village Day 2 Snowball 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a village snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x329), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x282C)]
        CollectableMountainVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Main Room Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32A), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1426)]
        CollectableOceansideSpiderHouseMainRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Entrance Pot 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32B), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x142E)]
        CollectableOceansideSpiderHouseEntrancePot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Main Room Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32C), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1433)]
        CollectableOceansideSpiderHouseMainRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Oceanside Spider House Storage Room Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32D), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1439)]
        CollectableOceansideSpiderHouseStorageRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32E), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA0)]
        CollectablePathToGoronVillageWinterLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x32F), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA2)]
        CollectablePathToGoronVillageWinterLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x330), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA3)]
        CollectablePathToGoronVillageWinterLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x331), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA4)]
        CollectablePathToGoronVillageWinterLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 3 Snowball 5"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x332), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA5)]
        CollectablePathToGoronVillageWinterLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x333), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EA8)]
        CollectablePathToGoronVillageWinterLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x334), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EAA)]
        CollectablePathToGoronVillageWinterLargeSnowball7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x335), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EAB)]
        CollectablePathToGoronVillageWinterLargeSnowball8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 2 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x336), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EAC)]
        CollectablePathToGoronVillageWinterLargeSnowball9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x337), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EAE)]
        CollectablePathToGoronVillageWinterLargeSnowball10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x338), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB0)]
        CollectablePathToGoronVillageWinterLargeSnowball11,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 3"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x339), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB1)]
        CollectablePathToGoronVillageWinterLargeSnowball12,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 4"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33A), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB3)]
        CollectablePathToGoronVillageWinterLargeSnowball13,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Day 1 Snowball 5"), Region(Region.TwinIslands)]
        [GossipLocationHint("a travelling snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33B), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB5)]
        CollectablePathToGoronVillageWinterLargeSnowball14,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Small Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33C), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB6)]
        CollectablePathToGoronVillageWinterSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Small Snowball 2"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33D), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB8)]
        CollectablePathToGoronVillageWinterSmallSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Twin Islands Ramp Snowball"), Region(Region.TwinIslands)]
        [GossipLocationHint("a lake snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33E), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2EB9)]
        CollectablePathToGoronVillageWinterSmallSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Mountain Village Small Snowball"), Region(Region.MountainVillage)]
        [GossipLocationHint("a foothill snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x33F), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0xE23)]
        CollectablePathToMountainVillageSmallSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x340), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2DE0)]
        CollectablePathToSnowheadLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 2"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x341), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2DE1)]
        CollectablePathToSnowheadLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 3"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x342), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2DE2)]
        CollectablePathToSnowheadLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Path to Snowhead Large Snowball 4"), Region(Region.PathToSnowhead)]
        [GossipLocationHint("a treacherous snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x343), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2DE3)]
        CollectablePathToSnowheadLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x344), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x12A0)]
        CollectablePinnacleRockPot1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 2"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x345), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x12A6)]
        CollectablePinnacleRockPot2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 3"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x346), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x12AB)]
        CollectablePinnacleRockPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Pinnacle Rock Pot 4"), Region(Region.PinnacleRock)]
        [GossipLocationHint("a deep jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x347), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x12AD)]
        CollectablePinnacleRockPot4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Secret Shrine Underwater Pot 3"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x348), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x307B)]
        CollectableSecretShrineMainRoomPot3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Secret Shrine Underwater Pot 4"), Region(Region.SecretShrine)]
        [GossipLocationHint("a secret place"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x349), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x307F)]
        CollectableSecretShrineMainRoomPot4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34A), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E24)]
        CollectableSnowheadLargeSnowball1,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 2"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34B), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E25)]
        CollectableSnowheadLargeSnowball2,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 3"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34C), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E26)]
        CollectableSnowheadLargeSnowball3,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 4"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34D), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E27)]
        CollectableSnowheadLargeSnowball4,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 5"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34E), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E28)]
        CollectableSnowheadLargeSnowball5,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Snowhead Large Snowball 6"), Region(Region.Snowhead)]
        [GossipLocationHint("a mountain-top snowball"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x34F), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2E29)]
        CollectableSnowheadLargeSnowball6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x350), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C20)]
        CollectableStoneTowerPot6,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot 2"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x351), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C22)]
        CollectableStoneTowerPot7,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Upper Scarecrow Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x352), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C2E)]
        CollectableStoneTowerPot8,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Upper Scarecrow Pot 4"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x353), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C31)]
        CollectableStoneTowerPot9,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Stone Tower Lower Scarecrow Pot 3"), Region(Region.StoneTower)]
        [GossipLocationHint("a high tower"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x354), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x2C34)]
        CollectableStoneTowerPot10,


        [Visible]
        [Repeatable]
        [ItemName("Small Magic Jar"), LocationName("Zora Cape Waterfall Pot"), Region(Region.ZoraCape)]
        [GossipLocationHint("a cape jar"), GossipItemHint("a magic refill")]
        [ShopText("Replenishes a small amount of your magic power.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x355), ItemCategory(ItemCategory.MagicJars), CollectableIndex(0x1C21)]
        CollectableZoraCapePot4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Fence Item"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x356), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Fence Item 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x357), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x358), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x359), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 5"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35A), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ranch Fence Item 6"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a fence"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35B), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableRomaniRanchInvisibleItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Above Cow Grotto Invisible Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35D), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35F), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x363), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 4"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x364), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 5"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x365), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Invisible Item 6"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x366), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 7"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x367), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem7,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 8"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x368), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem8,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Northern Ramp Invisible Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x369), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem9,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 10"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36A), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem10,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Invisible Item 11"), Region(Region.TerminaField)]
        [GossipLocationHint("a hidden item"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36B), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldInvisibleItem11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x371), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x372), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Invisible Item 3"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x373), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Spider House Invisible Item 4"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x374), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem4,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Swamp Spider House Invisible Item 5"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a large jar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x375), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableSwampSpiderHouseInvisibleItem5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Tree Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a tree"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x360), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldTreeItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Pillar Spawned Item"), Region(Region.TerminaField)]
        [GossipLocationHint("a pillar"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x361), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldPillarItem1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Telescope Guay"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x362), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldTelescopeGuay1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swordsman School Gong"), Region(Region.WestClockTown)]
        [GossipLocationHint("timekeeping"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35C), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableSwordsmanSchoolGong1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Bean Grotto Soft Soil"), Region(Region.DekuPalace)]
        [GossipLocationHint("underground soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x35E), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableBeanGrottoSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Palace Soft Soil"), Region(Region.DekuPalace)]
        [GossipLocationHint("royal soil", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36C), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableDekuPalaceSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Doggy Racetrack Soft Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a sporting arena"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36D), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableDoggyRacetrackSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Great Bay Coast Soft Soil"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("ocean soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36E), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableGreatBayCoastSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ranch Day 1 Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("early soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x370), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableRomaniRanchSoftSoilDay11,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Ranch Day 2 or 3 Soil"), Region(Region.RomaniRanch)]
        [GossipLocationHint("late soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x376), ItemCategory(ItemCategory.Ammo), NullableItem]
        CollectableRomaniRanchSoftSoilDay2Or31,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Secret Shrine Soft Soil"), Region(Region.SecretShrine)]
        [GossipLocationHint("secret soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x36F), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableSecretShrineSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Soft Soil Lower"), Region(Region.StoneTower)]
        [GossipLocationHint("high soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x377), ItemCategory(ItemCategory.Ammo), NullableItem]
        CollectableStoneTowerSoftSoilLower1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Stone Tower Soft Soil Upper"), Region(Region.StoneTower)]
        [GossipLocationHint("high soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x378), ItemCategory(ItemCategory.Ammo), NullableItem]
        CollectableStoneTowerSoftSoilUpper1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Swamp Spider House Rock Soft Soil"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("rock soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x379), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableSwampSpiderHouseRockSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Swamp Spider House Gold Room Soft Soil"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("gold soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37A), ItemCategory(ItemCategory.Ammo), NullableItem]
        CollectableSwampSpiderHouseGoldRoomSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Stump Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37B), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldStumpSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Observatory Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37C), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldObservatorySoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field South Wall Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("wall soil"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37D), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSouthWallSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Pillar Soft Soil"), Region(Region.TerminaField)]
        [GossipLocationHint("field soil"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37E), ItemCategory(ItemCategory.Ammo), NullableItem]
        CollectableTerminaFieldPillarSoftSoil1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #1"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x37F), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #2"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x380), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay21,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #3"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x381), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay31,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #4"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x382), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay41,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #5"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x383), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay51,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #6"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x384), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay61,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #7"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x385), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay71,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #8"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x386), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay81,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #9"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x387), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay91,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #10"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x388), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay101,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #11"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x389), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay111,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #12"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38A), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay121,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #13"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38B), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay131,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #14"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38C), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay141,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #15"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38D), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay151,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #16"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38E), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay161,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #17"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x38F), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay171,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #18"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x390), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay181,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Guay #19"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x391), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldGuay191,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Guay #20"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x392), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldGuay201,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #5a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x393), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldGuay5A1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #10a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x394), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldGuay10A1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Guay #15a"), Region(Region.TerminaField)]
        [GossipLocationHint("bird droppings"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x395), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldGuay15A1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #1"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x396), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #2"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x397), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster21,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #3"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x398), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster31,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #4"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x399), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster41,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #5"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39A), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster51,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Palace Rupee Cluster #6"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39B), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster61,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Deku Palace Rupee Cluster #7"), Region(Region.DekuPalace)]
        [GossipLocationHint("a royal circle", "the home of scrubs"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39C), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableDekuPalaceRupeeCluster71,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39D), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39E), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster21,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x39F), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster31,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster41,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster51,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Rupee Cluster 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster61,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard Rupee Cluster 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("an unholy circle"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A3), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableBeneathTheGraveyardRupeeCluster71,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A4), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldSongWallDawn1,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A5), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldSongWallDawn2,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall Dawn 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A6), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldSongWallDawn3,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall 4 / 20"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A7), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldSongWall4201,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall 4 / 20 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A8), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSongWall4202,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall 4 / 20 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3A9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSongWall4203,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AA), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSongWall0812161,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AB), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSongWall0812162,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 0 / 8 / 12 / 16 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AC), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSongWall0812163,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AD), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableTerminaFieldSongWall2101418221,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AE), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSongWall2101418222,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Song Wall 2 / 10 / 14 / 18 / 22 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3AF), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableTerminaFieldSongWall2101418223,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSongWallOddHours1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours 2"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSongWallOddHours2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Song Wall Odd Hours 3"), Region(Region.TerminaField)]
        [GossipLocationHint("musical notes"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSongWallOddHours3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B3), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay21,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 2"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B4), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay22,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 3"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B5), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay23,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 4"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B6), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay24,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 2 Item 5"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B7), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay25,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 2 Item 6"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B8), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay26,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3B9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 2"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BA), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 3"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BB), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay13,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 4"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BC), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay14,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 1 Item 5"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BD), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay15,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 1 Item 6"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BE), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay16,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3BF), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay31,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 2"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay32,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 3"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay33,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 4"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay34,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Deku Playground Day 3 Item 5"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C3), ItemCategory(ItemCategory.BlueRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay35,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Deku Playground Day 3 Item 6"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a game for scrubs", "a playground", "a town game"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C4), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableDekuPlaygroundPlatformDay36,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C5), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagLeftEye1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C6), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagLeftEye2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Left Eye 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C7), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagLeftEye3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C8), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagRightEye1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3C9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagRightEye2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Pirates' Fortress Skull Flag Right Eye 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CA), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressSkullFlagRightEye3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CB), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomSkullFlagForehead1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CC), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomSkullFlagForehead2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Hookshot Room Skull Flag Forehead 3"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("a pirate flag"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CD), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectablePiratesFortressInteriorHookshotRoomSkullFlagForehead3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CE), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 2"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3CF), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 3"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 4"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 5"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 6"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D3), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 7"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D4), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 8"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D5), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 9"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D6), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 10"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D7), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 11"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D8), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Swamp Spider House Blue Gem 12"), Region(Region.SouthernSwamp)]
        [GossipLocationHint("a blue gem"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3D9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSwampSpiderHouseBlueGem12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DA), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DB), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 3"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DC), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 4"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DD), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 5"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DE), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 6"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3DF), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 7"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 8"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Oceanside Spider House Mask 9"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableOceansideSpiderHouseMask9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E3), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldClam1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam 2"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E4), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldClam2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Clam 3"), Region(Region.TerminaField)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E5), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldClam3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E6), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldWall1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E7), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldWall2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Wall 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E8), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldWall3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3E9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSkullKidDrawing1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EA), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSkullKidDrawing2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Termina Field Skull Kid Drawing 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a drawing"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EB), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableTerminaFieldSkullKidDrawing3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EC), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 2"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3ED), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 3"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EE), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 4"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3EF), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 5"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F0), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Cucco Shack Diamond Hole 6"), Region(Region.RomaniRanch)]
        [GossipLocationHint("a diamond gap"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F1), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableCuccoShackDiamondHole6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F2), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 2"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F3), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 3"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F4), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 4"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F5), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 5"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F6), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 6"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F7), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 7"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F8), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 8"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3F9), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern8,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 9"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FA), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 10"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FB), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern10,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 11"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FC), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern11,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Ikana Graveyard Lantern 12"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a lantern"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FD), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableIkanaGraveyardLantern12,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FE), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableStockPotInnMask1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask 2"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x3FF), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableStockPotInnMask2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Stock Pot Inn Mask 3"), Region(Region.StockPotInn)]
        [GossipLocationHint("a town mask"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x400), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableStockPotInnMask3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x401), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x402), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x403), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 4"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x404), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 5"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x405), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Target 6"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x406), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownTarget6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x407), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownBasket1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket 2"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x408), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownBasket2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("East Clock Town Basket 3"), Region(Region.EastClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x409), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableEastClockTownBasket3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40A), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSouthClockTownClock1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock 2"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40B), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSouthClockTownClock2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Clock Tower Clock 3"), Region(Region.SouthClockTown)]
        [GossipLocationHint("an accurate shot"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40C), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableSouthClockTownClock3,


        [Visible]
        [Repeatable]
        [ItemName("Gold Rupee"), LocationName("Takkuri"), Region(Region.TerminaField)]
        [GossipLocationHint("a thief"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 200 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40D), ItemCategory(ItemCategory.GoldRupees), NullableItem]
        CollectableTakkuri1,



        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Hookshot Room Pot"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40E), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11AD)]
        CollectablePiratesFortressInteriorHookshotRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Hookshot Room Pot 2"), Region(Region.PiratesFortressInterior)]
        [GossipLocationHint("the home of pirates"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x40F), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x11AE)]
        CollectablePiratesFortressInteriorHookshotRoomPot2,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Rock"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x410), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1694)]
        CollectableTerminaFieldRock1,


        [Visible]
        [Repeatable]
        [ItemName("30 Arrows"), LocationName("Termina Field Rock 2"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x411), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x1695)]
        CollectableTerminaFieldRock2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard Highest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x412), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x21A0)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock1,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard Lowest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x413), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x21A1)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock2,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Ikana Graveyard 2nd Lowest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x414), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x21A4)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 3"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x415), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1690)]
        CollectableTerminaFieldRock3,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 4"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x416), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1691)]
        CollectableTerminaFieldRock4,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 5"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x417), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1692)]
        CollectableTerminaFieldRock5,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 6"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x418), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x1696)]
        CollectableTerminaFieldRock6,


        [Visible]
        [Repeatable]
        [ItemName("Blue Rupee"), LocationName("Termina Field Rock 7"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 5 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x419), ItemCategory(ItemCategory.BlueRupees), CollectableIndex(0x168F)]
        CollectableTerminaFieldRock7,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard 2nd Highest Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41A), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x21A2)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock4,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Ikana Graveyard Middle Rock"), Region(Region.IkanaGraveyard)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41B), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x21A3)]
        CollectableIkanaGraveyardIkanaGraveyardUpperRock5,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Rock 8"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41C), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x168D)]
        CollectableTerminaFieldRock8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Termina Field Rock 9"), Region(Region.TerminaField)]
        [GossipLocationHint("a rock on a wall"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41D), ItemCategory(ItemCategory.RedRupees), CollectableIndex(0x168E)]
        CollectableTerminaFieldRock9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41E), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 2"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x41F), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 3"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x420), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 4"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x421), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 5"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x422), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 6"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x423), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 7"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x424), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Milk Road Keaton Grass 8"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x425), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMilkRoadKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Milk Road Keaton Grass 9"), Region(Region.MilkRoad)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x426), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableMilkRoadKeatonGrass9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x427), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 2"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x428), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 3"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x429), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 4"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42A), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 5"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42B), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 6"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42C), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 7"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42D), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("North Clock Town Keaton Grass 8"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42E), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("North Clock Town Keaton Grass 9"), Region(Region.NorthClockTown)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x42F), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableNorthClockTownKeatonGrass9,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x430), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass1,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 2"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x431), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass2,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 3"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x432), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass3,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 4"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x433), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass4,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 5"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x434), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass5,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 6"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x435), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass6,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 7"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x436), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass7,


        [Visible]
        [Repeatable]
        [ItemName("Green Rupee"), LocationName("Mountain Village Spring Keaton Grass 8"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 1 rupee.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x437), ItemCategory(ItemCategory.GreenRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass8,


        [Visible]
        [Repeatable]
        [ItemName("Red Rupee"), LocationName("Mountain Village Spring Keaton Grass 9"), Region(Region.MountainVillage)]
        [GossipLocationHint("a living plant"), GossipItemHint("currency", "money", "cash", "wealth", "riches and stuff")]
        [ShopText("This is worth 20 rupees.")]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x438), ItemCategory(ItemCategory.RedRupees), NullableItem]
        CollectableMountainVillageSpringKeatonGrass9,



        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Mask Room Pot"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x439), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x143A)]
        CollectableOceansideSpiderHouseMaskRoomPot1,


        [Visible]
        [Repeatable]
        [ItemName("10 Arrows"), LocationName("Oceanside Spider House Mask Room Pot 2"), Region(Region.GreatBayCoast)]
        [GossipLocationHint("a creepy basement pot"), GossipItemHint("a quiver refill", "a bundle of projectiles")]
        [ShopText("Ammo for your bow.", isMultiple: true)]
        [ChestType(ChestTypeAttribute.ChestType.SmallWooden)]
        [GetItemIndex(0x43A), ItemCategory(ItemCategory.Ammo), CollectableIndex(0x143B)]
        CollectableOceansideSpiderHouseMaskRoomPot2,



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
        [GetItemIndex(0x0A), ItemCategory(ItemCategory.Fake)]
        RecoveryHeart = -2,
    }
}
