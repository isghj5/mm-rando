using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    //[System.Diagnostics.DebuggerDisplay("{ToString()}")] // useless for enums
    public enum Scene
    {
        // base enumerator value matches the ST column, SceneInternal ID matches ID column (F)
        // https://docs.google.com/spreadsheets/d/1J-4OwmZzOKEv2hZ7wrygOpMm0YcRnephEo3Q2FooF6E/edit#gid=1593589171

        [FileID(1160)]
        [SceneInternalId(0x12)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Toto,
            Actor.Dexihand, Actor.LikeLike)] // hand can stop you talking to mother
        [EnemizerSceneEnemyReplacementBlock(Actor.Secretary,
            Actor.LikeLike)] // big one can block you from reaching mother, cycle 0 check
        [EnemizerSceneEnemyReplacementBlock(Actor.Gorman,
            Actor.LikeLike)] // likelike can grab and spit you before you can face it
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
        [EnemizerSceneEnemyReplacementBlock(Actor.Octarok,
            Actor.Wolfos, // can attack you off the boat
            Actor.LikeLike)] // can grab you on the boat ride
        SouthernSwampClear = 0x06,

        [FileID(1151)]
        [SceneInternalId(0x0D)]
        CuriosityShop = 0x07,

        //[FileID(1520)]
        //[SceneInternalId(0x0D)]
        //[SceneInternalId(0x0E)]
        //[SceneInternalId(0x08)]
        //TEST01 = 0x08, // maybe it is JP only

        // Unused = 0x09,

        [FileID(1522)]
        [SceneInternalId(0x07)]
        [ClearEnemyPuzzleRooms(7, 13)] // 7:dodongo, 13:peahat
        [EnemizerSceneEnemyReplacementBlock(Actor.Peahat, // hidden or very weak enemies suck here, but they are very common in this slot
            Actor.Beamos, // beamos is just because bomb locking this check early is prime seed killer
            Actor.Bo, Actor.Leever, // annoying boring enemies, need to spawn like 10
            //Actor.Nejiron, Actor.RedBubble, 
            Actor.RegularIceBlock)] // blocking actors
        [EnemizerSceneEnemyReplacementBlock(Actor.DekuBabaWithered, // grottos are common, this can get silly
            Actor.Peahat, Actor.Beamos, Actor.LikeLike, Actor.Freezard//, Actor.BomberHideoutGuard // annoying
            //Actor.Bumper, Actor.UnusedStoneTowerStoneElevator, Actor.UnusedStoneTowerPlatform, Actor.RegularIceBlock,
            /*Actor.ClocktowerGearsAndOrgan /*, Actor.PatrollingPirate */ )]
        [EnemizerSceneBlockSensitive(Actor.DekuBabaWithered, -1)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BioDekuBaba,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // they can extend so far they can block the door leading out
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GoldSkulltula,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can get the player locked behind them near the grotto stones
        Grottos = 0x0A,

        // Unused = 0x0B,

        // Unused = 0x0C,

        // Unused = 0x0D,

        [FileID(1520)]
        [SceneInternalId(0x08)]
        SPOT00 = 0x0E, // cutscene map

        // Unused = 0x0F,

        [FileID(1165)]
        [SceneInternalId(0x13)]
        IkanaCanyon = 0x10,

        [FileID(1171)]
        [SceneInternalId(0x14)]
        // TODO come up with a way to make sure that one spot isn't blocking without hardcoding
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.PatrollingPirate,
            Actor.Obj_Iceblock)] // can block the stairs
        PiratesFortress = 0x11,

        [FileID(1173)]
        [SceneInternalId(0x15)]
        MilkBar = 0x12,

        [FileID(1175)]
        [SceneInternalId(0x16)]
        [ClearEnemyPuzzleRooms(4, 7)]// basement lava
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 2)] // eygore
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RealBombchu,
        //    Actor.WarpDoor)]
        [EnemizerSceneBlockSensitive(Actor.RealBombchu, -1)] // chicken holder leads to a chest
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Beamos,
            Actor.IkanaGravestone, Actor.Bumper, Actor.En_Ani)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Hiploop,
        //    Actor.En_Ani, Actor.Bumper, Actor.Tijo)]
        [EnemizerSceneBlockSensitive(Actor.Hiploop, -1)]
        StoneTowerTemple = 0x13,

        [FileID(1188)]
        [SceneInternalId(0x17)]
        TreasureChestShop = 0x14,

        [FileID(1190)]
        [SceneInternalId(0x18)]
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 3)] // eygore 
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 1)] // wizrobe
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
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 4, 34)] // wooden flower room, deku baba and stray fairy in bubble
        [FairyDroppingEnemies(roomNumber: 3, actorNumber: 3)] // west wing, skulltula:3
        [FairyDroppingEnemies(roomNumber: 5, actorNumber: 22)] // east wing, beehive:22
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Dinofos, // weak enemies are kinda lame here
            Actor.Leever, Actor.ChuChu, Actor.DekuBabaWithered)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Skulltula, 
            Actor.BigPoe)] // I think this was an issue? other than being annoying I mean
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bo,
            Actor.GibdoWell, Actor.DeathArmos, // Rarely Killable
            Actor.Keese // can bug out and fly out-of-bounds, difficult to kill
            /*Actor.RegularIceBlock, Actor.Bombiwa, Actor.ClocktowerGearsAndOrgan */)] // blocking
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DragonFly,
            Actor.GiantBeee)] // issue being that the one that spins around and doesnt agro hard requires a ranged weapon because the spawn is so high
        [EnemizerSceneBlockSensitive(Actor.Bo, -1)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Snapper,
        //    Actor.WarpDoor, Actor.ClocktowerGearsAndOrgan)] // Snapper spawns just on top of its chest, its possible a non-killable actor is placed int he wya
        [EnemizerSceneBlockSensitive(Actor.Snapper, -1)]
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
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Wolfos, // can cover a switch, don't allow problem actors
        //    Actor.WarpDoor, Actor.WarpToTrialEntrance, Actor.ClocktowerGearsAndOrgan, Actor.Bumper, Actor.IkanaGravestone, Actor.Tijo)]
        [EnemizerSceneBlockSensitive(Actor.Wolfos, -1)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RedBubble, // spawns in hot lava, keep wood enemies out
            Actor.Peahat, Actor.MadShrub, Actor.Postbox, Actor.DekuBaba, Actor.DekuBabaWithered, Actor.Freezard, Actor.Eeno, Actor.Wolfos, Actor.Dinofos, Actor.Snapper)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bo, // spawns in hot lava, keep wood enemies out
            Actor.Bombiwa /* Actor.RegularIceBlock, Actor.IkanaCanyonHookshotStump, */ )] // could block the fairy bubble
        [EnemizerSceneBlockSensitive(Actor.Bo, -1)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Freezard,
            Actor.PoeSisters, // weird behavior, if the killing blow of meg at long range can stop chests from spawning
            Actor.CircleOfFire, // if it gets placed on the one on top of a chest the player is screwed
            Actor.DragonFly, // if you kill it at long range or such that its dying body falls to first floor it wont count
            Actor.WarpDoor, // Cannot walk through them to get to the chest under
            Actor.Wolfos)] // wolfos: ice wolfos can push the regular actual dog backwards through the wall
        [EnemizerSceneBlockSensitive(Actor.Freezard, 5)] // can block access to the elevator
        [FairyDroppingEnemies(roomNumber: 11, actorNumber: 2, 3)] // dinofos 
        SnowheadTemple = 0x1E,

        [FileID(1256)]
        [SceneInternalId(0x22)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Carpenter,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)]
        MilkRoad = 0x1F,

        [FileID(1258)]
        [SceneInternalId(0x23)]
        [ClearEnemyPuzzleRooms(0, 1, 2)] // three pirate minibosses
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SpikedMine,
            Actor.LabFish)] // crash unknown reason, float math error
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ZoraEgg,
        //    Actor.Tijo, Actor.Bombiwa, Actor.Bumper)] // blocking a chest
        [EnemizerSceneBlockSensitive(Actor.ZoraEgg, -1)]
        PiratesFortressRooms = 0x20, // tag: Sewer

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
            Actor.Bombiwa //Actor.StockpotBell, Actor.IkanaGravestone,  Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            /*Actor.Bumper, Actor.ClocktowerGearsAndOrgan*/)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Bombiwa, // blocking a few skulltulla
            Actor.Lulu //Actor.StockpotBell, Actor.IkanaGravestone,  Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            /*Actor.Bumper, Actor.ClocktowerGearsAndOrgan*/)]
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        [EnemizerSceneBlockSensitive(Actor.Bombiwa, -1)]
        // old, should no longer be needed: Actor.En_Ani, Actor.GoronElder, Actor.Cow, Actor.Tijo , Actor.Postbox,
        SwampSpiderHouse = 0x24,

        [FileID(1291)]
        [SceneInternalId(0x28)]
        [EnemizerSceneEnemyReplacementBlock(Actor.SkulltulaDummy,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // Can block the ability to reach rafters to get a skulltoken
        OceanSpiderHouse = 0x25,

        [FileID(1298)]
        [SceneInternalId(0x29)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Scarecrow, // can block the stairs
            Actor.ClocktowerGearsAndOrgan)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.Torch, // can block the stairs
        //    Actor.ClocktowerGearsAndOrgan)]
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        AstralObservatory = 0x26, // and sewer leading to it

        [FileID(1301)]
        [SceneInternalId(0x2A)]
        DekuTrial = 0x27,

        [FileID(1304)]
        [SceneInternalId(0x2B)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch, // can block the stairs
            //Actor.RegularIceBlock,  // the big one can be too big
            Actor.Dexihand)] // if it grabs you as you fall into a grotto hole it can hardlock
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        DekuPalace = 0x28,

        [FileID(1308)]
        [SceneInternalId(0x2C)]
        MountainSmithy = 0x29,

        [FileID(1310)]
        [SceneInternalId(0x2D)]
        // this actor is mostly ignored, player might not even notice, dont waste lots of object budget on this thing
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ClayPot,
            Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DekuBaba,
            Actor.LikeLike)] // can grab you on grotto exit and softlock with only one heart, TODO make special code instead moving them?
        // these actors are only seen in the credits, we should block all large object actors from these spots to save generation time
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ViscenMoonLeaveCutscene,
            Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        ///*
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.MutoMoonLeaveCutscene,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.AnjusGrandmaCredits,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.AnjuMotherWedding,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CuriosityShopMan,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        //*/
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

        // inside of the goron village main building, with the goron shop and merrygoround
        [FileID(1319)]
        [SceneInternalId(0x32)]
        [EnemizerSceneBlockSensitive(Actor.GoGoron, -1)] // ice block can block shop
        GoronShrine = 0x2F,

        [FileID(1322)]
        [SceneInternalId(0x33)]
        ZoraHall = 0x30,

        [FileID(1324)]
        [SceneInternalId(0x34)]
        TradingPost = 0x31,

        [FileID(1326)]
        [SceneInternalId(0x35)]
        RomaniRanch = 0x32,

        [FileID(1328)]
        [SceneInternalId(0x36)]
        TwinmoldsLair = 0x33,

        [FileID(1330)]
        [SceneInternalId(0x37)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Seagulls,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can stop ting from falling
        GreatBayCoast = 0x34,

        [FileID(1332)]
        [SceneInternalId(0x38)]
        [EnemizerSceneEnemyReplacementBlock(Actor.LikeLike,
                    Actor.Japas, Actor.Bombiwa, Actor.BronzeBoulder, Actor.Mimi, Actor.TreasureChest)] // small blocking
         //    Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator, Actor.Tijo,
        //    Actor.Bombiwa, Actor.BronzeBoulder, Actor.CircleOfFire,
        //    Actor.RegularZora, Actor.SwimmingZora, Actor.WarpDoor)]
        [EnemizerSceneBlockSensitive(Actor.LikeLike, -1)]
        ZoraCape = 0x35,

        [FileID(1334)]
        [SceneInternalId(0x39)]
        LotteryShop = 0x36,

        // Unused = 0x37,

        [FileID(1336)]
        [SceneInternalId(0x3B)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch,
            Actor.ClocktowerGearsAndOrgan, Actor.RegularIceBlock)]
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
            Actor.Bo, Actor.StoneTowerMirror, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator, Actor.SpiderWeb)]
        [EnemizerSceneBlockSensitive(Actor.BadBat, -1)]
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
        [EnemizerSceneEnemyReplacementBlock(Actor.Dampe,
            Actor.Treee)]// for some reason big poe in the first room can cause camera to lock, unknown reason
        [EnemizerSceneBlockSensitive(Actor.Dampe, -1)]
        IkanaGraveyard = 0x40,

        [FileID(1356)]
        [SceneInternalId(0x44)]
        GohtsLair = 0x41,

        [FileID(1358)]
        [SceneInternalId(0x45)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.DekuBabaWithered, // bit annoying 
        //    Actor.Peahat, Actor.LikeLike, Actor.Freezard)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.DragonFly, // blocks deku flying 
        //    Actor.UnusedStoneTowerPlatform, Actor.UnusedPirateElevator)]
        [EnemizerSceneBlockSensitive(Actor.DragonFly, -1)]
        SouthernSwamp = 0x42,

        [FileID(1362)]
        [SceneInternalId(0x46)]
        //  we want the hiploop to be non-blocking actors, making them killable with this flag does the job
        [FairyDroppingEnemies(roomNumber: 24, actorNumber: 25, 26)] // hiploops
        [EnemizerSceneEnemyReplacementBlock(Actor.Hiploop, // respawning bo can show up here, but I dont want to mark the whole room to not place respawning enemies
            Actor.Peahat, Actor.BabaIsUnused //Actor.Seth1, Actor.Tijo, Actor.ArmosStatue, Actor.ClocktowerGearsAndOrgan, // blocking bridges
            /* Actor.Wolfos */ )] // wolfos:iceblock
        [EnemizerSceneBlockSensitive(Actor.Hiploop, -1)]
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
            Actor.BigPoe)] // for some reason big poe in the first room can cause camera to lock, unknown reason
        [EnemizerSceneEnemyReplacementBlock(Actor.Dexihand,
            Actor.Bumper)] // can block the water channel
        [FairyDroppingEnemies(roomNumber: 8, actorNumber: 7)] // skulltula in first room
        [EnemizerSceneBlockSensitive(Actor.Dexihand, -1)]
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
        [ClearEnemyPuzzleRooms( 0x1 )] // the guantlet is only one big room
        SakonsHideout = 0x4C,

        [FileID(1417)]
        [SceneInternalId(0x50)]
        MountainVillage = 0x4D,

        [FileID(1419)]
        [SceneInternalId(0x51)]
        PoeHut = 0x4E, // Ghost Hut

        [FileID(1421)]
        [SceneInternalId(0x52)]
        [EnemizerSceneBlockSensitive(Actor.MadShrub, -1)]
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
        [EnemizerSceneEnemyReplacementBlock(Actor.Beamos,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the whole assension
        StoneTower = 0x55,

        [FileID(1444)]
        [SceneInternalId(0x59)]
        [EnemizerSceneBlockSensitive(Actor.BlueBubble, -1)]
        InvertedStoneTower = 0x56,

        [FileID(1446)]
        [SceneInternalId(0x5A)]
        MountainVillageSpring = 0x57,

        [FileID(1449)]
        [SceneInternalId(0x5B)]
        PathToSnowhead = 0x58,

        [FileID(1451)]
        [SceneInternalId(0x5C)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Bo,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the twisted path into snowhead
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
            /* Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire,*/ Actor.LikeLike)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Anju,
        //    Actor.AnjusGrandma, Actor.AnjusGrandmaCredits)] // this this was just cutscenes, not the same bug
        [EnemizerSceneBlockSensitive(Actor.Gorman, -1)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RosaSisters,
        //    Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire, Actor.LightBlock,
        //   Actor.Eyegore)]
        [EnemizerSceneBlockSensitive(Actor.RosaSisters, -1)]
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
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GoGoron,
            Actor.PatrollingPirate)]
        BombShop = 0x65,

        [FileID(1504)]
        [SceneInternalId(0x69)]
        GiantsChamber = 0x66,

        [FileID(1506)]
        [SceneInternalId(0x6A)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Treee,
            Actor.PoeBalloon, Actor.BigPoe, // Actor.FloorMaster,
            //Actor.SleepingScrub, // too much dyna, spawns too many flowers and flower companions
            Actor.GrassRockCluster // can spawn too many rocks
            //Actor.GibdoIkana, Actor.ReDead, Actor.GibdoWell,
            /* Actor.CircleOfFire, Actor.LightBlock, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator */)]
        [EnemizerSceneBlockSensitive(Actor.Treee, -1)]
        GormanTrack = 0x67,

        [FileID(1508)]
        [SceneInternalId(0x6B)]
        [EnemizerSceneBlockSensitive(Actor.BombFlower, -1)]
        [EnemizerSceneBlockSensitive(Actor.UglyTree, -1)]
        GoronRacetrack = 0x68,

        [FileID(1510)]
        [SceneInternalId(0x6C)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Gorman,
        //    Actor.ClocktowerGearsAndOrgan, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // organ is huge, covers the mayor's door
        [EnemizerSceneBlockSensitive(Actor.Gorman, -1)] // was moved next to mayors door, large bodies can actually block this
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BomberHideoutGuard,
                Actor.Peahat, Actor.Tijo, Actor.ArmosStatue, Actor.ClocktowerGearsAndOrgan, Actor.CircleOfFire, Actor.GibdoWell, Actor.RegularIceBlock, // worried about big blocking actors
                Actor.Wolfos)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BombersYouChase,
        //Actor.RegularIceBlock)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.LikeLike)] // If you start with one heart this can be a softlock
        [EnemizerSceneBlockSensitive(Actor.BombersYouChase, -1)] // chicken holder leads to a chest
        EastClockTown = 0x69,

        [FileID(1512)]
        [SceneInternalId(0x6D)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
        //    Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.LikeLike)] // If you start with one heart this can be a softlock
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        WestClockTown = 0x6A,

        [FileID(1514)]
        [SceneInternalId(0x6E)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
        //    Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.LikeLike)] // If you start with one heart this can be a softlock
        NorthClockTown = 0x6B,

        [FileID(1516)]
        [SceneInternalId(0x6F)]
        /*[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Carpenter,
            Actor.RegularIceBlock, Actor.Bumper, Actor.CircleOfFire, Actor.GoronElder, Actor.LightBlock, // can block day 3 chest, TODO move it so we can re-enable
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator, // chest can raise to match height,putting it out of reach
            Actor.PatrollingPirate)] // could be annoying, hard to leave
        */
        [EnemizerSceneBlockSensitive(Actor.Carpenter, -1)] // TODO figure out which one is the issue one
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.PatrollingPirate, // could be annoying, hard to leave
            Actor.LikeLike )] // If you start with one heart this can be a softlock
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        [EnemizerSceneBlockSensitive(Actor.Kafei, -1)]
        SouthClockTown = 0x6C,

        [FileID(1518)]
        [SceneInternalId(0x70)]
        LaundryPool = 0x6D,
    }

}
