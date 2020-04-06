using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    public enum Scene
    {
        [SceneInternalId(0x12)]
        MayorsResidence = 0x00,

        [SceneInternalId(0x0B)]
        MajorasLair = 0x01,

        [SceneInternalId(0x0A)]
        PotionShop = 0x02,

        [SceneInternalId(0x10)]
        RanchBuildings = 0x03,

        [SceneInternalId(0x11)]
        HoneyDarling = 0x04,

        [SceneInternalId(0x0C)]
        BeneathGraveyard = 0x05,

        [SceneInternalId(0x00)]
        SouthernSwampClear = 0x06,

        [SceneInternalId(0x0D)]
        CuriosityShop = 0x07,

        // TestMap = 0x08,

        // Unused = 0x09,

        [SceneInternalId(0x07)]
        Grottos = 0x0A,

        // Unused = 0x0B,

        // Unused = 0x0C,

        // Unused = 0x0D,

        // CutsceneMap = 0x0E,

        // Unused = 0x0F,

        [SceneInternalId(0x13)]
        IkanaCanyon = 0x10,

        [SceneInternalId(0x14)]
        PiratesFortress = 0x11,

        [SceneInternalId(0x15)]
        MilkBar = 0x12,

        [SceneInternalId(0x16)]
        StoneTowerTemple = 0x13,

        [SceneInternalId(0x17)]
        TreasureChestShop = 0x14,

        [SceneInternalId(0x18)]
        InvertedStoneTowerTemple = 0x15,

        [SceneInternalId(0x19)]
        ClockTowerRoof = 0x16,

        [SceneInternalId(0x1A)]
        BeforeThePortalToTermina = 0x17,

        [SceneInternalId(0x1B)]
        WoodfallTemple = 0x18,

        [SceneInternalId(0x1C)]
        PathToMountainVillage = 0x19,

        [SceneInternalId(0x1D)]
        IkanaCastle = 0x1A,

        [SceneInternalId(0x1E)]
        DekuPlayground = 0x1B,

        [SceneInternalId(0x1F)]
        OdolwasLair = 0x1C,

        [SceneInternalId(0x20)]
        TownShootingGallery = 0x1D,

        [SceneInternalId(0x21)]
        SnowheadTemple = 0x1E,

        [SceneInternalId(0x22)]
        MilkRoad = 0x1F,

        [SceneInternalId(0x23)]
        PiratesFortressRooms = 0x20,

        [SceneInternalId(0x24)]
        SwampShootingGallery = 0x21,

        [SceneInternalId(0x25)]
        PinnacleRock = 0x22,

        [SceneInternalId(0x26)]
        FairyFountain = 0x23,

        [SceneInternalId(0x27)]
        SwampSpiderHouse = 0x24,

        [SceneInternalId(0x28)]
        OceanSpiderHouse = 0x25,

        [SceneInternalId(0x29)]
        AstralObservatory = 0x26,

        [SceneInternalId(0x2A)]
        DekuTrial = 0x27,

        [SceneInternalId(0x2B)]
        DekuPalace = 0x28,

        [SceneInternalId(0x2C)]
        MountainSmithy = 0x29,

        [SceneInternalId(0x2D)]
        TerminaField = 0x2A,

        [SceneInternalId(0x2E)]
        PostOffice = 0x2B,

        [SceneInternalId(0x2F)]
        MarineLab = 0x2C,

        [SceneInternalId(0x30)]
        DampesHouse = 0x2D,

        // Unused = 0x2E,

        [SceneInternalId(0x32)]
        GoronShrine = 0x2F,

        [SceneInternalId(0x33)]
        ZoraHall = 0x30,

        [SceneInternalId(0x34)]
        TradingPost = 0x31,

        [SceneInternalId(0x35)]
        RomaniRanch = 0x32,

        [SceneInternalId(0x36)]
        TwinmoldsLair = 0x33,

        [SceneInternalId(0x37)]
        GreatBayCoast = 0x34,

        [SceneInternalId(0x38)]
        ZoraCape = 0x35,

        [SceneInternalId(0x39)]
        LotteryShop = 0x36,

        // Unused = 0x37,

        [SceneInternalId(0x3B)]
        PiratesFortressExterior = 0x38,

        [SceneInternalId(0x3C)]
        FishermansHut = 0x39,

        [SceneInternalId(0x3D)]
        GoronShop = 0x3A,

        [SceneInternalId(0x3E)]
        DekuKingChamber = 0x3B,

        [SceneInternalId(0x3F)]
        GoronTrial = 0x3C,

        [SceneInternalId(0x40)]
        RoadToSouthernSwamp = 0x3D,

        [SceneInternalId(0x41)]
        DoggyRacetrack = 0x3E,

        [SceneInternalId(0x42)]
        CuccoShack = 0x3F,

        [SceneInternalId(0x43)]
        IkanaGraveyard = 0x40,

        [SceneInternalId(0x44)]
        GohtsLair = 0x41,

        [SceneInternalId(0x45)]
        SouthernSwamp = 0x42,

        [SceneInternalId(0x46)]
        Woodfall = 0x43,

        [SceneInternalId(0x47)]
        ZoraTrial = 0x44,

        [SceneInternalId(0x48)]
        GoronVillageSpring = 0x45,

        [SceneInternalId(0x49)]
        GreatBayTemple = 0x46,

        [SceneInternalId(0x4A)]
        WaterfallRapids = 0x47,

        [SceneInternalId(0x4B)]
        BeneathTheWell = 0x48,

        [SceneInternalId(0x4C)]
        ZoraHallRooms = 0x49,

        [SceneInternalId(0x4D)]
        GoronVillage = 0x4A,

        [SceneInternalId(0x4E)]
        GoronGrave = 0x4B,

        [SceneInternalId(0x4F)]
        SakonsHideout = 0x4C,

        [SceneInternalId(0x50)]
        MountainVillage = 0x4D,

        [SceneInternalId(0x51)]
        PoeHut = 0x4E,

        [SceneInternalId(0x52)]
        DekuShrine = 0x4F,

        [SceneInternalId(0x53)]
        RoadToIkana = 0x50,

        [SceneInternalId(0x54)]
        SwordsmansSchool = 0x51,

        [SceneInternalId(0x55)]
        MusicBoxHouse = 0x52,

        [SceneInternalId(0x56)]
        IgosDuIkanasLair = 0x53,

        [SceneInternalId(0x57)]
        TouristCenter = 0x54,

        [SceneInternalId(0x58)]
        StoneTower = 0x55,

        [SceneInternalId(0x59)]
        InvertedStoneTower = 0x56,

        [SceneInternalId(0x5A)]
        MountainVillageSpring = 0x57,

        [SceneInternalId(0x5B)]
        PathToSnowhead = 0x58,

        [SceneInternalId(0x5C)]
        Snowhead = 0x59,

        [SceneInternalId(0x5D)]
        TwinIslands = 0x5A,

        [SceneInternalId(0x5E)]
        TwinIslandsSpring = 0x5B,

        [SceneInternalId(0x5F)]
        GyorgsLair = 0x5C,

        [SceneInternalId(0x60)]
        SecretShrine = 0x5D,

        [SceneInternalId(0x61)]
        StockPotInn = 0x5E,

        [SceneInternalId(0x62)]
        GreatBayCutscene = 0x5F,

        [SceneInternalId(0x63)]
        ClockTowerInterior = 0x60,

        [SceneInternalId(0x64)]
        WoodsOfMystery = 0x61,

        // LostWoods = 0x62,

        [SceneInternalId(0x66)]
        LinkTrial = 0x63,

        [SceneInternalId(0x67)]
        TheMoon = 0x64,

        [SceneInternalId(0x68)]
        BombShop = 0x65,

        // GiantsChamber = 0x66,

        [SceneInternalId(0x6A)]
        GormanTrack = 0x67,

        [SceneInternalId(0x6B)]
        GoronRacetrack = 0x68,

        [SceneInternalId(0x6C)]
        EastClockTown = 0x69,

        [SceneInternalId(0x6D)]
        WestClockTown = 0x6A,

        [SceneInternalId(0x6E)]
        NorthClockTown = 0x6B,

        [SceneInternalId(0x6F)]
        SouthClockTown = 0x6C,

        [SceneInternalId(0x70)]
        LaundryPool = 0x6D,
    }
}
