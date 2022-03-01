﻿using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public enum Scene
    {
        // base enumerator value matches the ST column, SceneInternal ID matches ID column (F)
        // https://docs.google.com/spreadsheets/d/1J-4OwmZzOKEv2hZ7wrygOpMm0YcRnephEo3Q2FooF6E/edit#gid=1593589171

        [FileID(1160)]
        [SceneInternalId(0x12)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Toto,
            Actor.Dexihand)] // hand can stop you talking to mother
        [EnemizerSceneEnemyReplacementBlock(Actor.Secretary,
            Actor.LikeLike)] // big one can block you from reaching mother, cycle 0 check
        MayorsResidence = 0x00,

        [FileID(1143)]
        [SceneInternalId(0x0B)]
        MajorasLair = 0x01,

        [FileID(1141)]
        [SceneInternalId(0x0A)]
        PotionShop = 0x02,

        [FileID(1154)]
        [SceneInternalId(0x10)]
        RanchBuildings = 0x03,

        [FileID(1158)]
        [SceneInternalId(0x11)]
        HoneyDarling = 0x04,

        [FileID(1145)]
        [ClearEnemyPuzzleRooms(1, 2, 4)]
        [SceneInternalId(0x0C)]
        [EnemizerSceneEnemyReplacementBlock(Actor.IronKnuckle,
            Actor.Hiploop, // hiploop dies if he touches water? happens in day 2 iron knuckle
            Actor.GibdoWell)] // bit mean to go that far to find a gibdo you have to kill, could be softlock too if no soaring/sot
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
        [ClearEnemyPuzzleRooms(7, 13)] //7:dodongo, 13:peahat
        [EnemizerSceneEnemyReplacementBlock(Actor.Peahat, // hidden or very weak enemies suck here, but they are very common in this slot
            Actor.Bo, Actor.Nejiron, Actor.RedBubble, Actor.Leever, Actor.Wolfos, Actor.Beamos)] // beamos is just because bomb locking this check early is prime seed killer
        [EnemizerSceneEnemyReplacementBlock(Actor.DekuBabaWithered, // grottos are common, this can get silly
            Actor.Peahat, Actor.Beamos, Actor.LikeLike, Actor.Freezard, Actor.Bumper)]
        Grottos = 0x0A,

        // Unused = 0x0B,

        // Unused = 0x0C,

        // Unused = 0x0D,

        [FileID(1520)]
        [SceneInternalId(0x12)]
        CutsceneMap = 0x0E,

        // Unused = 0x0F,

        [FileID(1165)]
        [SceneInternalId(0x13)]
        IkanaCanyon = 0x10,

        [FileID(1171)]
        [SceneInternalId(0x14)]
        PiratesFortress = 0x11,

        [FileID(1173)]
        [SceneInternalId(0x15)]
        MilkBar = 0x12,

        [FileID(1175)]
        [SceneInternalId(0x16)]
        [ClearEnemyPuzzleRooms(4, 7)]// basement lava
        [FairyDroppingEnemies(1, 2)] // eygore
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Beamos,
            Actor.IkanaGravestone, Actor.Bumper, Actor.En_Ani)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Hiploop,
            Actor.En_Ani, Actor.Bumper, Actor.Tijo)]

        StoneTowerTemple = 0x13,

        [FileID(1188)]
        [SceneInternalId(0x17)]
        TreasureChestShop = 0x14,

        [FileID(1190)]
        [SceneInternalId(0x18)]
        [FairyDroppingEnemies(1, 3)] // eygore 
        [FairyDroppingEnemies(1, 1)] // wizrobe
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DeathArmos,
            Actor.PatrollingPirate)] // casual, causes a need for stone mask to procede through the temple
        InvertedStoneTowerTemple = 0x15,

        [FileID(1203)]
        [SceneInternalId(0x19)]
        ClockTowerRoof = 0x16,

        [FileID(1205)]
        [SceneInternalId(0x1A)]
        BeforeThePortalToTermina = 0x17,

        [FileID(1208)]
        [SceneInternalId(0x1B)]
        [ClearEnemyPuzzleRooms(4, 6, 7, 8, 9)] // 4: mapchest, 6: snapper room, 7: bow room, 8: BK, 9:dark
        //[FairyDroppingEnemies(0,  )] 
        [FairyDroppingEnemies(1, 4, 34)] // wooden flower room, deku baba and stray fairy in bubble
        [FairyDroppingEnemies(3, 3)] // west wing, skulltula:3
        [FairyDroppingEnemies(5, 22)] // east wing, beehive:22
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Dinofos, // weak enemies are kinda lame here
            Actor.Leever, Actor.ChuChu, Actor.DekuBabaWithered)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Skulltula, // weak enemies are kinda lame here
            Actor.BigPoe)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bo,
            Actor.GibdoWell, Actor.Peahat)] // We consider them killable, but killing this many...
        WoodfallTemple = 0x18,

        [FileID(1222)]
        [SceneInternalId(0x1C)]
        PathToMountainVillage = 0x19,

        [FileID(1224)]
        [SceneInternalId(0x1D)]
        IkanaCastle = 0x1A,

        [FileID(1235)]
        [SceneInternalId(0x1E)]
        DekuPlayground = 0x1B,

        [FileID(1237)]
        [SceneInternalId(0x1F)]
        OdolwasLair = 0x1C,

        [FileID(1239)]
        [SceneInternalId(0x20)]
        TownShootingGallery = 0x1D,

        [FileID(1241)]
        [SceneInternalId(0x21)]
        // 11 dinofos room, 6/12 wizrobe
        [ClearEnemyPuzzleRooms(1, 2, 5, 6, 9)] // 1:wolfos room, 2: east freezard, 5: north freezard, 6: wizr1, 9:chu room
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RedBubble, // spawns in hot lava, keep wood enemies out
            Actor.Peahat, Actor.MadShrub, Actor.Postbox, Actor.DekuBaba, Actor.DekuBabaWithered, Actor.Freezard, Actor.Eeno, Actor.Wolfos, Actor.Dinofos, Actor.Snapper)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Freezard,
            Actor.PoeSisters, // weird behavior, if the killing blow of meg at long range can stop chests from spawning
            Actor.CircleOfFire, // if it gets placed on the one on top of a chest the player is screwed
            Actor.DragonFly, // if you kill it at long range or such that its dying body falls to first floor it wont count
            Actor.Wolfos)] // wolfos: ice wolfos can push the regular actual dog backwards through the wall
        [FairyDroppingEnemies(11, 2, 3)] // dinofos 
        SnowheadTemple = 0x1E,

        [FileID(1256)]
        [SceneInternalId(0x22)]
        MilkRoad = 0x1F,

        [FileID(1258)]
        [SceneInternalId(0x23)]
        [ClearEnemyPuzzleRooms(0, 1, 2)] // three pirate minibosses
        PiratesFortressRooms = 0x20,

        [FileID(1276)]
        [SceneInternalId(0x24)]
        SwampShootingGallery = 0x21,

        [FileID(1276)]
        [SceneInternalId(0x25)]
        PinnacleRock = 0x22,

        [FileID(1278)]
        [SceneInternalId(0x26)]
        FairyFountain = 0x23, // great fairy

        [FileID(1284)]
        [SceneInternalId(0x27)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch, // blocking a few skulltulla
            Actor.StockpotBell, Actor.Cow, Actor.IkanaGravestone, Actor.Bombiwa, Actor.Bumper, Actor.Postbox, Actor.En_Ani, Actor.Tijo)]
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

        [FileID(1308)]
        [SceneInternalId(0x2C)]
        MountainSmithy = 0x29,

        [FileID(1310)]
        [SceneInternalId(0x2D)]
        TerminaField = 0x2A,

        [FileID(1312)]
        [SceneInternalId(0x2E)]
        PostOffice = 0x2B,

        [FileID(1314)]
        [SceneInternalId(0x2F)]
        MarineLab = 0x2C,

        [FileID(1316)]
        [SceneInternalId(0x30)]
        // [ClearEnemyPuzzleRooms(   )] // is big poe reward a clear room reward?
        DampesHouse = 0x2D,

        // Unused = 0x2E,

        [FileID(1319)]
        [SceneInternalId(0x32)]
        GoronShrine = 0x2F,

        [FileID(1322)]
        [SceneInternalId(0x33)]
        ZoraHall = 0x30,

        [FileID(1324)]
        [SceneInternalId(0x34)]
        TradingPost = 0x31,

        [FileID(1326)]
        [SceneInternalId(0x35)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.Treee, Actor.Carpenter)]
        RomaniRanch = 0x32,

        [FileID(1328)]
        [SceneInternalId(0x36)]
        TwinmoldsLair = 0x33,

        [FileID(1330)]
        [SceneInternalId(0x37)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Leever, Actor.PoeBalloon)]
        GreatBayCoast = 0x34,

        [FileID(1332)]
        [SceneInternalId(0x38)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Leever, Actor.PoeBalloon)]
        ZoraCape = 0x35,

        [FileID(1334)]
        [SceneInternalId(0x39)]
        LotteryShop = 0x36,

        // Unused = 0x37,

        [FileID(1336)]
        [SceneInternalId(0x3B)]
        PiratesFortressExterior = 0x38,

        [FileID(1338)]
        [SceneInternalId(0x3C)]
        FishermansHut = 0x39,

        [FileID(1340)]
        [SceneInternalId(0x3D)]
        GoronShop = 0x3A,

        [FileID(1342)]
        [SceneInternalId(0x3E)]
        DekuKingChamber = 0x3B,

        [FileID(1344)]
        [SceneInternalId(0x3F)]
        GoronTrial = 0x3C,

        [FileID(1347)]
        [SceneInternalId(0x40)]
        // respawning bo can show up here, but I dont want to mark the whole room to not place respawning enemies
        // mirror blocks climbing
        [EnemizerSceneEnemyReplacementBlock(Actor.BadBat,
            Actor.Bo, Actor.StoneTowerMirror)]
        RoadToSouthernSwamp = 0x3D,

        [FileID(1349)]
        [SceneInternalId(0x41)]
        DoggyRacetrack = 0x3E,

        [FileID(1351)]
        [SceneInternalId(0x42)]
        [ClearEnemyPuzzleRooms(0, 1)] // respawning enemies can break chick round-up
        CuccoShack = 0x3F,

        [FileID(1353)]
        [SceneInternalId(0x43)]
        IkanaGraveyard = 0x40,

        [FileID(1356)]
        [SceneInternalId(0x44)]
        GohtsLair = 0x41,

        [FileID(1358)]
        [SceneInternalId(0x45)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.DekuBabaWithered, // bit annoying 
        //    Actor.Peahat, Actor.LikeLike, Actor.Freezard)]
        SouthernSwamp = 0x42,

        [FileID(1362)]
        [SceneInternalId(0x46)]
        //  we want the hiploop to be non-blocking actors, making them killable with this flag does the job
        [FairyDroppingEnemies(24, 25, 26)] // hiploops
        [EnemizerSceneEnemyReplacementBlock(Actor.Hiploop, // respawning bo can show up here, but I dont want to mark the whole room to not place respawning enemies
            Actor.Wolfos, Actor.Peahat, Actor.Seth1, Actor.Tijo)] // blocking enemies (wolfos:iceblock)
        Woodfall = 0x43,

        [FileID(1364)]
        [SceneInternalId(0x47)]
        ZoraTrial = 0x44,

        [FileID(1366)]
        [SceneInternalId(0x48)]
        GoronVillageSpring = 0x45,

        [FileID(1369)]
        [SceneInternalId(0x49)]
        //3: clear the biobabas, 5 is gekko, 8 is wart
        [ClearEnemyPuzzleRooms(3, 5, 7)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Skulltula,
            Actor.BigPoe)]// for some reason big poe in the first room can cause camera to lock, unknown reason
        [FairyDroppingEnemies(8, 7)] // skulltula in first room 
        GreatBayTemple = 0x46,

        [FileID(1386)]
        [SceneInternalId(0x4A)]
        WaterfallRapids = 0x47,

        [FileID(1388)]
        [SceneInternalId(0x4B)]
        [ClearEnemyPuzzleRooms( 12 )] // 12 is big poe
        BeneathTheWell = 0x48,

        [FileID(1403)]
        [SceneInternalId(0x4C)]
        ZoraHallRooms = 0x49,

        [FileID(1409)]
        [SceneInternalId(0x4D)]
        GoronVillage = 0x4A,

        [FileID(1412)]
        [SceneInternalId(0x4E)]
        GoronGrave = 0x4B,

        [FileID(1414)]
        [SceneInternalId(0x4F)]
        //[ClearEnemyPuzzleRooms( unk )] // ignored by enemizer right now anyway
        SakonsHideout = 0x4C,

        [FileID(1417)]
        [SceneInternalId(0x50)]
        MountainVillage = 0x4D,

        [FileID(1419)]
        [SceneInternalId(0x51)]
        PoeHut = 0x4E, // Ghost Hut

        [FileID(1421)]
        [SceneInternalId(0x52)]
        DekuShrine = 0x4F,

        [FileID(1431)]
        [SceneInternalId(0x53)]
        RoadToIkana = 0x50,

        [FileID(1433)]
        [SceneInternalId(0x54)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Clock,
            Actor.BadBat, Actor.GoldSkulltula, Actor.RealBombchu)] // z-targetable can be annoying in the sword test
        SwordsmansSchool = 0x51,

        [FileID(1434)]
        [SceneInternalId(0x55)]
        MusicBoxHouse = 0x52,

        [FileID(1437)]
        [SceneInternalId(0x56)]
        IgosDuIkanasLair = 0x53, // 

        [FileID(1440)]
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
        [ClearEnemyPuzzleRooms(2,3,4,5)]
        SecretShrine = 0x5D,

        [FileID(1466)]
        [SceneInternalId(0x61)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Gorman,
            Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire, Actor.LikeLike)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RosaSisters,
            Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire, Actor.Eyegore)]
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
        [ClearEnemyPuzzleRooms(1, 2, 3)] // 1 dinofos, 3 is iron knuckle
        LinkTrial = 0x63,

        [FileID(1500)]
        [SceneInternalId(0x67)]
        TheMoon = 0x64,

        [FileID(1502)]
        [SceneInternalId(0x68)]
        BombShop = 0x65,

        [FileID(1504)]
        [SceneInternalId(0x69)]
        GiantsChamber = 0x66,

        [FileID(1506)]
        [SceneInternalId(0x6A)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Treee,
            Actor.PoeBalloon, Actor.BigPoe, Actor.CircleOfFire, Actor.FloorMaster, Actor.En_Ani)]
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

        [FileID(1518)]
        [SceneInternalId(0x70)]
        LaundryPool = 0x6D,
    }

}
