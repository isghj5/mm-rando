using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    public enum Scene
    {
        // base enumerator value matches the ST column, SceneInternal ID matches ID column (F)
        // https://docs.google.com/spreadsheets/d/1J-4OwmZzOKEv2hZ7wrygOpMm0YcRnephEo3Q2FooF6E/edit#gid=1593589171

        [FileID(1160)]
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

        [FileID(1145)]
        [ClearEnemyPuzzleRooms(1,2,4)]
        [SceneInternalId(0x0C)]
        BeneathGraveyard = 0x05,

        [FileID(1137)]
        [SceneInternalId(0x00)]
        SouthernSwampClear = 0x06,

        [SceneInternalId(0x0D)]
        CuriosityShop = 0x07,

        // TestMap = 0x08,

        // Unused = 0x09,

        [FileID(1522)]
        [SceneInternalId(0x07)]
        [ClearEnemyPuzzleRooms(7,13)] //7:dodongo, 13:peahat
        Grottos = 0x0A,

        // Unused = 0x0B,

        // Unused = 0x0C,

        // Unused = 0x0D,

        // CutsceneMap = 0x0E,

        // Unused = 0x0F,

        [FileID(1165)]
        [SceneInternalId(0x13)]
        IkanaCanyon = 0x10,

        [SceneInternalId(0x14)]
        PiratesFortress = 0x11,

        [SceneInternalId(0x15)]
        MilkBar = 0x12,

        [FileID(1175)]
        [SceneInternalId(0x16)]
        [ClearEnemyPuzzleRooms(4,7)]// basement lava
        StoneTowerTemple = 0x13,

        [SceneInternalId(0x17)]
        TreasureChestShop = 0x14,

        [FileID(1190)]
        [SceneInternalId(0x18)]
        [ClearEnemyPuzzleRooms(4)]// wizrobe room
        InvertedStoneTowerTemple = 0x15,

        [SceneInternalId(0x19)]
        ClockTowerRoof = 0x16,

        [SceneInternalId(0x1A)]
        BeforeThePortalToTermina = 0x17,

        [FileID(1208)]
        [SceneInternalId(0x1B)]
        [ClearEnemyPuzzleRooms(0, 1, 3, 4, 6, 7, 8, 9)]
        WoodfallTemple = 0x18,

        [FileID(1222)]
        [SceneInternalId(0x1C)]
        PathToMountainVillage = 0x19,

        [FileID(1224)]
        [SceneInternalId(0x1D)]
        IkanaCastle = 0x1A,

        [SceneInternalId(0x1E)]
        DekuPlayground = 0x1B,

        [FileID(1237)]
        [SceneInternalId(0x1F)]
        OdolwasLair = 0x1C,

        [SceneInternalId(0x20)]
        TownShootingGallery = 0x1D,

        [FileID(1241)]
        [SceneInternalId(0x21)]
        [ClearEnemyPuzzleRooms(1, 2, 5, 6, 9, 12 )]
        SnowheadTemple = 0x1E,

        [SceneInternalId(0x22)]
        MilkRoad = 0x1F,

        [FileID(1258)]
        [SceneInternalId(0x23)]
        [ClearEnemyPuzzleRooms(0,1,2)] // three pirate minibosses
        PiratesFortressRooms = 0x20,

        [SceneInternalId(0x24)]
        SwampShootingGallery = 0x21,

        [SceneInternalId(0x25)]
        PinnacleRock = 0x22,

        [SceneInternalId(0x26)]
        FairyFountain = 0x23,

        [SceneInternalId(0x27)]
        SwampSpiderHouse = 0x24,

        [FileID(1291)]
        [SceneInternalId(0x28)]
        OceanSpiderHouse = 0x25,

        [FileID(1298)]
        [SceneInternalId(0x29)]
        AstralObservatory = 0x26, // and sewer leading to it

        [FileID(1301)]
        [SceneInternalId(0x2A)]
        DekuTrial = 0x27,

        [FileID(1304)]
        [SceneInternalId(0x2B)]
        DekuPalace = 0x28,

        [SceneInternalId(0x2C)]
        MountainSmithy = 0x29,

        [FileID(1310)]
        [SceneInternalId(0x2D)]
        TerminaField = 0x2A,

        [SceneInternalId(0x2E)]
        PostOffice = 0x2B,

        [SceneInternalId(0x2F)]
        MarineLab = 0x2C,

        [SceneInternalId(0x30)]
        // [ClearEnemyPuzzleRooms(   )] // is big poe reward a clear room reward?
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

        [FileID(1330)]
        [SceneInternalId(0x37)]
        GreatBayCoast = 0x34,

        [FileID(1332)]
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

        [FileID(1344)]
        [SceneInternalId(0x3F)]
        GoronTrial = 0x3C,

        [FileID(1347)]
        [SceneInternalId(0x40)]
        RoadToSouthernSwamp = 0x3D,

        [SceneInternalId(0x41)]
        DoggyRacetrack = 0x3E,

        [SceneInternalId(0x42)]
        CuccoShack = 0x3F,

        [FileID(1347)]
        [SceneInternalId(0x43)]
        IkanaGraveyard = 0x40,

        [SceneInternalId(0x44)]
        GohtsLair = 0x41,

        [FileID(1358)]
        [SceneInternalId(0x45)]
        SouthernSwamp = 0x42,

        [FileID(1362)]
        [SceneInternalId(0x46)]
        Woodfall = 0x43,

        [FileID(1364)]
        [SceneInternalId(0x47)]
        ZoraTrial = 0x44,

        [SceneInternalId(0x48)]
        GoronVillageSpring = 0x45,

        [FileID(1369)]
        [SceneInternalId(0x49)]
        [ClearEnemyPuzzleRooms(3,4,5,7,8)]
        GreatBayTemple = 0x46,

        [SceneInternalId(0x4A)]
        WaterfallRapids = 0x47,

        [FileID(1388)]
        [SceneInternalId(0x4B)]
        //[ClearEnemyPuzzleRooms( unk )] // need to revisit to know, big poe and 2 chests?
        BeneathTheWell = 0x48,

        [SceneInternalId(0x4C)]
        ZoraHallRooms = 0x49,

        [FileID(1409)]
        [SceneInternalId(0x4D)]
        GoronVillage = 0x4A,

        [SceneInternalId(0x4E)]
        GoronGrave = 0x4B,

        [SceneInternalId(0x4F)]
        //[ClearEnemyPuzzleRooms( unk )] // ignored by enemizer right now anyway
        SakonsHideout = 0x4C,

        [SceneInternalId(0x50)]
        MountainVillage = 0x4D,

        [SceneInternalId(0x51)]
        //[ClearEnemyPuzzleRooms(1)]  // ignored by enemizer right now anyway
        PoeHut = 0x4E,

        [FileID(1421)]
        [SceneInternalId(0x52)]
        DekuShrine = 0x4F,

        [FileID(1431)]
        [SceneInternalId(0x53)]
        RoadToIkana = 0x50,

        [SceneInternalId(0x54)]
        SwordsmansSchool = 0x51,

        [SceneInternalId(0x55)]
        MusicBoxHouse = 0x52,

        [SceneInternalId(0x56)]
        //[ClearEnemyPuzzleRooms( unk )] // ignored by enemizer right now anyway
        IgosDuIkanasLair = 0x53,

        [SceneInternalId(0x57)]
        TouristCenter = 0x54,

        [FileID(1442)]
        [SceneInternalId(0x58)]
        StoneTower = 0x55,

        [FileID(1444)]
        [SceneInternalId(0x59)]
        InvertedStoneTower = 0x56,

        [FileID(1446)]
        [SceneInternalId(0x5A)]
        MountainVillageSpring = 0x57,

        [FileID(1449)]
        [SceneInternalId(0x5B)]
        PathToSnowhead = 0x58,

        [FileID(1451)]
        [SceneInternalId(0x5C)]
        Snowhead = 0x59,

        [FileID(1453)]
        [SceneInternalId(0x5D)]
        TwinIslands = 0x5A,

        [FileID(1455)]
        [SceneInternalId(0x5E)]
        TwinIslandsSpring = 0x5B,

        [FileID(1457)]
        [SceneInternalId(0x5F)]
        GyorgsLair = 0x5C,

        [FileID(1459)]
        [SceneInternalId(0x60)]
        //[ClearEnemyPuzzleRooms( unk )] // ignored by enemizer right now anyway
        SecretShrine = 0x5D,

        [FileID(1466)]
        [SceneInternalId(0x61)]
        StockPotInn = 0x5E,

        [FileID(1472)]
        [SceneInternalId(0x62)]
        GreatBayCutscene = 0x5F,

        [FileID(1474)]
        [SceneInternalId(0x63)]
        ClockTowerInterior = 0x60,

        [FileID(1477)]
        [SceneInternalId(0x64)]
        WoodsOfMystery = 0x61,

        [FileID(1487)]
        [SceneInternalId(0x65)]
        LostWoods = 0x62,

        [FileID(1491)]
        [SceneInternalId(0x66)]
        [ClearEnemyPuzzleRooms(1, 2)] // 1 dinofos, 2 is iron knuckle
        LinkTrial = 0x63,

        [FileID(1500)]
        [SceneInternalId(0x67)]
        TheMoon = 0x64,

        [FileID(1502)]
        [SceneInternalId(0x68)]
        BombShop = 0x65,

        // GiantsChamber = 0x66,

        [FileID(1506)]
        [SceneInternalId(0x6A)]
        GormanTrack = 0x67,

        [FileID(1508)]
        [SceneInternalId(0x6B)]
        GoronRacetrack = 0x68,

        [FileID(1510)]
        [SceneInternalId(0x6C)]
        EastClockTown = 0x69,

        [FileID(1512)]
        [SceneInternalId(0x6D)]
        WestClockTown = 0x6A,

        [FileID(1514)]
        [SceneInternalId(0x6E)]
        NorthClockTown = 0x6B,

        [FileID(1516)]
        [SceneInternalId(0x6F)]
        SouthClockTown = 0x6C,

        [FileID(1516)]
        [SceneInternalId(0x70)]
        LaundryPool = 0x6D,
    }

}
