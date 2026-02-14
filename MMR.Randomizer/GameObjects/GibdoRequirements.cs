using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Gibdo;

namespace MMR.Randomizer.GameObjects
{
    public class GibdoRequirement
    {
        public GibdoRequirementItem ItemRequired { get; }
        public byte Amount { get; set; }
        public Item? LogicEntry { get; }

        public GibdoRequirement(GibdoRequirementItem itemRequired, Item? logicEntry = null)
        {
            ItemRequired = itemRequired;
            LogicEntry = logicEntry;
        }

        public byte[] ToByteArray(ushort messageId)
        {
            var data = ItemRequired.GetAttribute<ItemGibdoAttribute>().Data;
            return new byte[]
            {
                ItemRequired.GetAttribute<ItemGibdoAttribute>().ItemAction,
                ItemRequired.GetAttribute<ItemGibdoAttribute>().Item,
                Amount,
                (byte)(ItemRequired.GetAttribute<ItemGibdoRequestTypeAttribute>()?.RequestType ?? 0),
                (byte)(messageId >> 8),
                (byte)(messageId & 0xFF),
                (byte)(data >> 8),
                (byte)(data & 0xFF),
            };
        }

        public static GibdoRequirement[] GibdoRequirements = new GibdoRequirement[]
        {
            new GibdoRequirement(GibdoRequirementItem.BluePotion, Item.GibdoEntranceLeft),
            new GibdoRequirement(GibdoRequirementItem.MagicBeans, Item.GibdoEntranceRight),
            new GibdoRequirement(GibdoRequirementItem.SpringWater, Item.GibdoToBombPots),
            new GibdoRequirement(GibdoRequirementItem.Fish, Item.GibdoToHotWater),
            new GibdoRequirement(GibdoRequirementItem.Bugs, Item.GibdoToFairyFountain),
            new GibdoRequirement(GibdoRequirementItem.DekuNuts, Item.GibdoToCowAndMiniboss),
            new GibdoRequirement(GibdoRequirementItem.Bombs, Item.GibdoToMiniBoss),
            new GibdoRequirement(GibdoRequirementItem.HotSpringWater, Item.GibdoToCow),
            new GibdoRequirement(GibdoRequirementItem.BigPoe, Item.GibdoToFinalWallmaster),
            new GibdoRequirement(GibdoRequirementItem.Milk, Item.GibdoToMirrorShield),
            new GibdoRequirement(GibdoRequirementItem.Bugs, Item.GibdoToLeftChest),
            new GibdoRequirement(GibdoRequirementItem.Bugs, Item.GibdoToRightChest),
            new GibdoRequirement(GibdoRequirementItem.Fish, Item.GibdoToBlackBoes),
            new GibdoRequirement(GibdoRequirementItem.Bugs),
            new GibdoRequirement(GibdoRequirementItem.Bugs),
            new GibdoRequirement(GibdoRequirementItem.Bugs),
        };

        public enum GibdoRequirementItem
        {
            None,

            [ItemGibdo(9, 1, CustomMessage = "Leeeeaave me that which flies straight and strikes hard!"), ItemGibdoAmount(15, 1, 50)]
            [ItemGibdoLogicRequirements(1, 30, Item.OtherArrow)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBigQuiver)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBiggestQuiver)]
            [ItemGibdoLogicRequirements(41, 50, Item.UpgradeBiggestQuiver)]
            Arrows,

            [ItemGibdo(10, 1, CustomMessage = "Leeeeaave me that which burns from afar!"), ItemGibdoAmount(15, 1, 50)]
            [ItemGibdoLogicRequirements(Item.ItemFireArrow)]
            [ItemGibdoLogicRequirements(1, 30, Item.OtherArrow)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBigQuiver)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBiggestQuiver)]
            [ItemGibdoLogicRequirements(41, 50, Item.UpgradeBiggestQuiver)]
            FireArrows,

            [ItemGibdo(11, 1, CustomMessage = "Leeeeaave me that which freezes from afar!"), ItemGibdoAmount(15, 1, 50)]
            [ItemGibdoLogicRequirements(Item.ItemIceArrow)]
            [ItemGibdoLogicRequirements(1, 30, Item.OtherArrow)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBigQuiver)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBiggestQuiver)]
            [ItemGibdoLogicRequirements(41, 50, Item.UpgradeBiggestQuiver)]
            IceArrows,

            [ItemGibdo(12, 1, CustomMessage = "Leeeeaave me the light that pierces the dark!"), ItemGibdoAmount(15, 1, 50)]
            [ItemGibdoLogicRequirements(Item.ItemLightArrow)]
            [ItemGibdoLogicRequirements(1, 30, Item.OtherArrow)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBigQuiver)]
            [ItemGibdoLogicConditional(31, 40, Item.UpgradeBiggestQuiver)]
            [ItemGibdoLogicRequirements(41, 50, Item.UpgradeBiggestQuiver)]
            LightArrows,

            [ItemGibdo(14, 6, CustomMessage = "Leeeeaave me [AMOUNT] refreshing blasts!", MessageId = 0x1392), ItemGibdoAmount(10, 2, 40)]
            [ItemGibdoLogicRequirements(1, 20, Item.OtherAnyBombBag)]
            [ItemGibdoLogicConditional(21, 30, Item.UpgradeBigBombBag)]
            [ItemGibdoLogicConditional(21, 30, Item.UpgradeBiggestBombBag)]
            [ItemGibdoLogicRequirements(31, 40, Item.UpgradeBiggestBombBag)]
            Bombs,

            [ItemGibdo(16, 7, CustomMessage = "Leeeeaave me the thing that creeps and blasts!"), ItemGibdoAmount(10, 1, 40)]
            [ItemGibdoLogicConditional(1, 20, Item.ItemBombBag)]
            [ItemGibdoLogicConditional(1, 20, Item.UpgradeBigBombBag)]
            [ItemGibdoLogicConditional(1, 20, Item.UpgradeBiggestBombBag)]
            [ItemGibdoLogicConditional(21, 30, Item.UpgradeBigBombBag)]
            [ItemGibdoLogicConditional(21, 30, Item.UpgradeBiggestBombBag)]
            [ItemGibdoLogicRequirements(31, 40, Item.UpgradeBiggestBombBag)]
            [ItemGibdoLogicRequirements(Item.OtherAnyBombchuPack)]
            Bombchu,

            [ItemGibdo(7, 8, CustomMessage = "Leeeeaave me the weapon grown from bank and time!"), ItemGibdoAmount(5, 1, 10)]
            [ItemGibdoLogicRequirements()]
            DekuSticks,

            [ItemGibdo(18, 9, CustomMessage = "Leeeeaave me something that makes a blinding flash!", MessageId = 0x1391), ItemGibdoAmount(10, 1, 20)]
            [ItemGibdoLogicRequirements()]
            DekuNuts,

            [ItemGibdo(46, 10, CustomMessage = "Leeeeaave me something delicious to chomp on. Something that sprouts when it's watered.", MessageId = 0x138D), ItemGibdoAmount(5, 1, 20)]
            [ItemGibdoLogicRequirements(Item.OtherLimitlessBeans)]
            MagicBeans,

            [ItemGibdo(15, 12, CustomMessage = "Leeeeaave me the blast that breaks mountains!")]
            [ItemGibdoLogicRequirements(Item.ItemPowderKeg)]
            PowderKeg,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the land where fog sleeps on poison!", Data = 1 << 1)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfTheSwamp,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of white-fur calling for help!", Data = (1 << 1) | (1 << 2))]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfAMonkey,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the beast that waits in still water!", Data = (1 << 1) | (1 << 3))]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfABigOcto,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of no more than a smear of scales!", Data = 1 << 4)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            [ItemGibdoLogicReference(Item.CollectableZoraCapePot1)]
            PhotoOfLuluBad,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the singer who forgot her own song!", Data = (1 << 4) | (1 << 5) | (1 << 6))]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            [ItemGibdoLogicReference(Item.CollectableZoraCapePot1)]
            PhotoOfLuluGood,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the dancer with no soul, yet endless cheer!", Data = 1 << 7)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfScarecrow,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the man who thinks he flies!", Data = 1 << 8)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfTingle,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the warrior who walks with the sea's fury in her eyes!", Data = 1 << 9)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicReference(Item.MundaneItemSeahorse)]
            PhotoOfPirateGood,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of the one who rules with noise, not knowledge!", Data = 1 << 10)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicRequirements(Item.ItemPictobox)]
            PhotoOfDekuKing,

            [ItemGibdo(19, 13, CustomMessage = "Leeeeaave me the image of a pirate, but let her not see me!", Data = 1 << 11)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Photo)]
            [ItemGibdoLogicReference(Item.MundaneItemSeahorse)]
            PhotoOfPirateBad,

            [ItemGibdo(35, 19, CustomMessage = "Leeeeaave me something red that bestows health!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.OtherAnyRedPotion)]
            RedPotion,

            [ItemGibdo(37, 20, CustomMessage = "Leeeeaave me something green that bestows magic!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.OtherAnyGreenPotion)]
            GreenPotion,

            [ItemGibdo(36, 21, MessageId = 0x138C)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.OtherAnyBluePotion)]
            BluePotion,

            [ItemGibdo(41, 22, CustomMessage = "Leeeeaave me the warm light with wings...")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements()]
            Fairy,

            [ItemGibdo(26, 23, CustomMessage = "Leeeeaave me the royalty of the green forest!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchPrincess)]
            DekuPrincess,

            [ItemGibdo(38, 24, MessageId = 0x1395)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.OtherAnyMilk)]
            Milk,

            [ItemGibdo(39, 25, CustomMessage = "Leeeeaave me what is half-sipped, forgotten!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.OtherAnyMilk)]
            HalfMilk,

            [ItemGibdo(22, 26, MessageId = 0x138F)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchFish)]
            Fish,

            [ItemGibdo(32, 27, MessageId = 0x1390)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchBug)]
            Bugs,

            [ItemGibdo(33, 29, CustomMessage = "Leeeeaave me the ghost that roams and vanishes!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchPoe)]
            Poe,

            [ItemGibdo(34, 30, MessageId = 0x1394)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchBigPoe)]
            BigPoe,

            [ItemGibdo(23, 31, MessageId = 0x138E)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicConditional(Item.BottleCatchSpringWater)]
            [ItemGibdoLogicConditional(Item.BottleCatchHotSpringWater)]
            SpringWater,

            [ItemGibdo(24, 32, MessageId = 0x1393)]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicReference(Item.GibdoToCow)]
            HotSpringWater,

            [ItemGibdo(25, 33, CustomMessage = "Leeeeaave me the unborn, sealed in its shell!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchEgg)]
            ZoraEgg,

            [ItemGibdo(27, 34, CustomMessage = "Leeeeaave me the gold that falls through time!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.ItemBottleGoronRace)]
            GoldDust,

            [ItemGibdo(30, 35, CustomMessage = "Leeeeaave me the cap that brews the blue elixir!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.BottleCatchMushroom)]
            Mushroom,

            [ItemGibdo(29, 36, CustomMessage = "Leeeeaave me the creature that dances with the tide!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicRequirements(Item.MundaneItemSeahorse)]
            Seahorse,

            [ItemGibdo(40, 37, CustomMessage = "Leeeeaave me the liquid of endless power!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Bottle)]
            [ItemGibdoLogicConditional(Item.ShopItemMilkBarChateau)]
            [ItemGibdoLogicConditional(Item.ItemBottleMadameAroma)]
            Chateau,

            [ItemGibdo(42, 40, CustomMessage = "Leeeeaave me the jewel that falls from the sky!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemMoonTear)]
            MoonTear,

            [ItemGibdo(43, 41, CustomMessage = "Leeeeaave me the claim to the ground where the clock ticks!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemLandDeed)]
            LandDeed,

            [ItemGibdo(47, 42, CustomMessage = "Leeeeaave me the claim to the ground where roots drink deep!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemSwampDeed)]
            SwampDeed,

            [ItemGibdo(48, 43, CustomMessage = "Leeeeaave me the claim to the ground where breath turns to frost!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemMountainDeed)]
            MountainDeed,

            [ItemGibdo(49, 44, CustomMessage = "Leeeeaave me the claim to the ground where waves bury all!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemOceanDeed)]
            OceanDeed,

            [ItemGibdo(44, 45, CustomMessage = "Leeeeaave me the right to stay for the carnival!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemRoomKey)]
            RoomKey,

            [ItemGibdo(51, 46, CustomMessage = "Leeeeaave me the letter that carries a son's last words!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemMamaLetter)]
            LetterToMama,

            [ItemGibdo(45, 47, CustomMessage = "Leeeeaave me the letter sealed with trembling hands!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemKafeiLetter)]
            LetterToKafei,

            [ItemGibdo(54, 48, CustomMessage = "Leeeeaave me the token that binds one heart to another!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.QuestTrade)]
            [ItemGibdoLogicRequirements(Item.TradeItemPendant)]
            PendantOfMemories,

            [ItemGibdo(0x3A, 0x36, CustomMessage = "Leeeeaave me one last look at the knowing face that even the grave cannot deceive!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskTruth)]
            MaskTruth,

            [ItemGibdo(0x3B, 0x37, CustomMessage = "Leeeeaave me one last look at the boy lost in time's cruel trick!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskKafei)]
            MaskKafei,

            [ItemGibdo(0x3C, 0x38, CustomMessage = "Leeeeaave me one last look at the curse that denies even dreams to the weary!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskAllNight)]
            MaskAllNight,

            [ItemGibdo(0x3D, 0x39, CustomMessage = "Leeeeaave me one last look at the joy that once outran death itself!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskBunnyHood)]
            MaskBunnyHood,

            [ItemGibdo(0x3E, 0x3A, CustomMessage = "Leeeeaave me one last look at the yellow trickster who still plays in forgotten fields!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskKeaton)]
            MaskKeaton,

            [ItemGibdo(0x3F, 0x3B, CustomMessage = "Leeeeaave me one last look at the mask of those who served death before they tasted it!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskGaro)]
            MaskGaro,

            [ItemGibdo(0x40, 0x3C, CustomMessage = "Leeeeaave me one last look at the mask of maturity!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskRomani)]
            MaskRomani,

            [ItemGibdo(0x41, 0x3D, CustomMessage = "Leeeeaave me one last look at the leader who smiles through a heart that's long forgotten how to laugh!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskCircusLeader)]
            MaskCircusLeader,

            [ItemGibdo(0x42, 0x3E, CustomMessage = "Leeeeaave me one last look at the hat that marks a life lived in routine, never to find its rest!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskPostmanHat)]
            MaskPostmanHat,

            [ItemGibdo(0x43, 0x3F, CustomMessage = "Leeeeaave me one last look at the symbol of love, now a relic of something I can never touch again!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskCouple)]
            MaskCouple,

            [ItemGibdo(0x44, 0x40, CustomMessage = "Leeeeaave me one last look at the goddess who was shattered into pieces!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskGreatFairy)]
            MaskGreatFairy,

            //[GibdoItem(0x45, 0x41, CustomMessage = "Leeeeaave me one last look at")]
            //[ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            //[ItemGibdoLogicRequirements(Item.MaskGibdo)]
            //MaskGibdo,

            [ItemGibdo(0x46, 0x42, CustomMessage = "Leeeeaave me one last look at the conductor's face, his notes never reaching the ears of the dead!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskDonGero)]
            MaskDonGero,

            [ItemGibdo(0x47, 0x43, CustomMessage = "Leeeeaave me one last look at the mask that once danced with movement, now shackled by death's quiet grasp!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskKamaro)]
            MaskKamaro,

            [ItemGibdo(0x48, 0x44, CustomMessage = "Leeeeaave me one last look at the face that once commanded armies, now lost in the silence of a forgotten war!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskCaptainHat)]
            MaskCaptainHat,

            [ItemGibdo(0x49, 0x45, CustomMessage = "Leeeeaave me one last look at the mask that hides in plain sight, never to be seen again!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskStone)]
            MaskStone,

            [ItemGibdo(0x4A, 0x46, CustomMessage = "Leeeeaave me one last look at the face that once led the march!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskBremen)]
            MaskBremen,

            [ItemGibdo(0x4B, 0x47, CustomMessage = "Leeeeaave me one last look at the face of destruction, forever scarred by the blast that echoes in silence!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskBlast)]
            MaskBlast,

            [ItemGibdo(0x4C, 0x48, CustomMessage = "Leeeeaave me one last look at the mask that picks up even the faintest trace of fragrance!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskScents)]
            MaskScents,

            [ItemGibdo(0x4D, 0x49, CustomMessage = "Leeeeaave me one last look at the face that towers above all others!")]
            [ItemGibdoRequestType(ItemGibdoRequestTypeAttribute.GibdoRequestType.Mask)]
            [ItemGibdoLogicRequirements(Item.MaskGiant)]
            MaskGiant,
        }
    }
}
