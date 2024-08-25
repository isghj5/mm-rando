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
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(Actor.Toto,
            Actor.Dexihand, Actor.LikeLike)] // hand can stop you talking to mother
        [EnemizerSceneEnemyReplacementBlock(Actor.Secretary,
            Actor.LikeLike)] // big one can block you from reaching mother, cycle 0 check
        [EnemizerSceneEnemyReplacementBlock(Actor.Gorman,
            Actor.ClocktowerGearsAndOrgan, // can block access to madam
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
        [EnemizerSceneEnemyReplacementBlock(Actor.CeilingSpawner,
            Actor.GBTFreezableWaterfall, // blocking platforming
            Actor.UnusedFallingBridge, // can void the player because it forces crushing with the ceiling
            Actor.UnusedStoneTowerPlatform, // same thing, should be disabled earlier but maybe it isnt
            Actor.UnusedStoneTowerStoneElevator, // can void the player because it forces crushing with the ceiling
            Actor.UnusedPirateElevator // can void the player because it forces crushing with the ceiling
        )]
        [EnemizerSceneEnemyReplacementBlock(Actor.IronKnuckle,
            Actor.Hiploop, // hiploop dies if he touches water? happens in day 2 iron knuckle
            Actor.GibdoWell)] // bit mean to go that far to find a gibdo you have to kill, could be softlock too if no soaring/sot
        BeneathGraveyard = 0x05,

        [FileID(1137)]
        [SceneInternalId(0x00)]
        [DynaHeadroom(12, 9)] // unknown, I know two organs is crash, two in different rooms so one budget per room is too much
        [EnemizerSceneEnemyReplacementBlock(Actor.Octarok,
            Actor.Wolfos, // can attack you off the boat
            Actor.LikeLike)] // can grab you on the boat ride
        [EnemizerSceneEnemyReplacementBlock(Actor.Lilypad,
            Actor.Desbreko)] // heavy lag
        SouthernSwampClear = 0x06,

        [FileID(1151)]
        [SceneInternalId(0x0D)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy:Actor.Sakon,
        //    Actor.SleepingScrub, // Can block the text interaction
        //    Actor.En_Ani)]
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
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Peahat, // hidden or very weak enemies suck here, but they are very common in this slot
            Actor.Beamos, // beamos is just because bomb locking this check early is prime seed killer
            Actor.Bo, Actor.Leever, // annoying boring enemies, need to spawn like 10
                                    //Actor.Nejiron, Actor.RedBubble, 
            Actor.RegularIceBlock)] // blocking actors
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DekuBabaWithered, // grottos are common, this can get silly
            Actor.Peahat, Actor.Beamos, Actor.LikeLike, Actor.Freezard, //, Actor.BomberHideoutGuard // annoying
            Actor.Seagulls, // with new height adjust its basically invisible
            Actor.Hiploop// water causes instant death
                         //Actor.Bumper, Actor.UnusedStoneTowerStoneElevator, Actor.UnusedStoneTowerPlatform, Actor.RegularIceBlock,
            /*Actor.ClocktowerGearsAndOrgan /*, Actor.PatrollingPirate */ )]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.TallGrass, // grottos are common, this can get silly
            Actor.RegularIceBlock, // big one can block the door or even block the whole hallway
            Actor.Beamos, Actor.LikeLike, Actor.Freezard, //, Actor.BomberHideoutGuard // annoying
            Actor.Seagulls, // with new height adjust its basically invisible
            Actor.Hiploop// water causes instant death
        )]
        [EnemizerSceneBlockSensitive(Actor.DekuBabaWithered, -1)] // can block the chest
        [EnemizerSceneBlockSensitive(Actor.DekuBaba, -1)] // this this is required to keep it off of withered as well
        [EnemizerSceneBlockSensitive(Actor.Wolfos, -1)] // if actorizer, one gossip stone is left alone the rest are randomized (this actor is used as placeholder)
        [EnemizerSceneBlockSensitive(Actor.Snapper, -1)] // if actorizer, one gossip stone is left alone the rest are randomized (this actor is used as placeholder)
        [EnemizerSceneBlockSensitive(Actor.Leever, -1)] // if actorizer, one gossip stone is left alone the rest are randomized (this actor is used as placeholder)
        [EnemizerSceneBlockSensitive(Actor.Armos, -1)] // if actorizer, one gossip stone is left alone the rest are randomized (this actor is used as placeholder)
        [EnemizerSceneBlockSensitive(Actor.Bombiwa, -1)] // chests under it in bomb grotto and hot spring grotto
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BioDekuBaba,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // they can extend so far they can block the door leading out
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GoldSkulltula,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can get the player locked behind them near the grotto stones
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.HoneyComb,
            Actor.Seagulls, // weird
            Actor.UnusedFallingBridge, // might block ability to enter the grotto
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator // can get the player locked behind them near the grotto stones
        )]
        Grottos = 0x0A,

        // Unused = 0x0B,

        // Unused = 0x0C,

        // Unused = 0x0D,

        [FileID(1520)]
        [SceneInternalId(0x08)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.HappyMaskSalesman,
            Actor.IkanaGravestone // crashes on n64 because there is no floor below it to matrix rotate to
        )]
        SPOT00 = 0x0E, // cutscene map

        // Unused = 0x0F,

        [FileID(1165)]
        [SceneInternalId(0x13)]
        IkanaCanyon = 0x10,

        [FileID(1171)]
        [SceneInternalId(0x14)]
        // TODO come up with a way to make sure that one spot isn't blocking without hardcoding
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.PatrollingPirate,
            //Actor.RegularIceBlock // should be covered by block sensitive now
            // Actor.LargeCrate, .SmalActorlWoodenBox, Actor.WoodenBarrel,  // these should only be free actors lets let them show up again
            //Actor.ClocktowerGearsAndOrgan, // blocking
            Actor.RegularIceBlock, // temporary, can block the bridge and the bottom code isnt working perfectly
            Actor.Bombiwa, Actor.Torch,  // boring
            Actor.CuccoChick, Actor.En_Ani, Actor.IkanaGravestone // boring
            )]
        [EnemizerSceneBlockSensitive(Actor.PatrollingPirate,
            0x14EA, // bottom of ladder
            0x18EA, // bridge to chest room
            0xEA)] // top of ladder -> bridge
        PiratesFortress = 0x11,

        [FileID(1173)]
        [SceneInternalId(0x15)]
        [EnemizerSceneEnemyReplacementBlock(Actor.GuruGuru,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        [EnemizerSceneEnemyReplacementBlock(Actor.Gorman,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        [EnemizerSceneEnemyReplacementBlock(Actor.HoneyAndDarling,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        [EnemizerSceneEnemyReplacementBlock(Actor.Gorman,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        MilkBar = 0x12,

        [FileID(1175)]
        [SceneInternalId(0x16)]
        [ClearEnemyPuzzleRooms(4, 7, // basement lava
            10)] // garo master
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 2)] // eygore
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RealBombchu,
        //    Actor.WarpDoor)]
        [EnemizerSceneBlockSensitive(Actor.RealBombchu, -1)] // chicken holder leads to a chest
        [EnemizerSceneBlockSensitive(Actor.SpikedMine, -1)] // the underwater spiked mines surrounded a switch
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Beamos,
            Actor.IkanaGravestone, Actor.Bumper, Actor.En_Ani)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CeilingSpawner,
            Actor.Shabom)] // get's stuck in the ceiling where you cannot kill them
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DragonFly,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the breakable floor under them
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
        [EnemizerSceneBlockSensitive(Actor.BlueBubble, -1)]
        [ClearEnemyPuzzleRooms(4)] // wizrobe room is a clear all room
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Poe,
           Actor.Bo)] // they just fall down to the "floor" and its awkward
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DeathArmos,
            Actor.PatrollingPirate)] // casual, causes a need for stone mask to procede through the temple
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CeilingSpawner,
           Actor.Shabom)] // can clip through the ceiling becoming impossible to attack
        InvertedStoneTowerTemple = 0x15,

        [FileID(1203)]
        [SceneInternalId(0x19)]
        ClockTowerRoof = 0x16,

        [FileID(1205)]
        [SceneInternalId(0x1A)]
        BeforeThePortalToTermina = 0x17,

        [FileID(1208)]
        [SceneInternalId(0x1B)]
        [DynaHeadroom(48, 48)]  // low default to start
        [DynaHeadroom(128, 128, room: 6)] // enemy kill rooms can be limited so far it trims all mimi, leaving an unkillable room
        [DynaHeadroom(128, 128, room: 7)]
        [ClearEnemyPuzzleRooms(4, 6, 7, 8, 9)] // 4: mapchest, 6: snapper room, 7: bow room, 8: BK, 9:dark
        [FairyDroppingEnemies(roomNumber: 1, actorNumber: 4, 34)] // wooden flower room, deku baba and stray fairy in bubble
        [FairyDroppingEnemies(roomNumber: 3, actorNumber: 3)] // west wing, skulltula:3
        [FairyDroppingEnemies(roomNumber: 5, actorNumber: 22)] // east wing, beehive:22
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Dinofos, // weak enemies are kinda lame here
            Actor.Leever, Actor.ChuChu, Actor.DekuBabaWithered,
            Actor.Hiploop)] // dies instantly in the water
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Snapper,
            Actor.Hiploop)] // dies instantly in the water
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Skulltula,
            Actor.BigPoe)] // I think this was an issue? other than being annoying I mean
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.HoneyComb,
            Actor.BigPoe)] // we've gotten a crash beacuse param changed, type change to zero, but we dont know why
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bo,
            Actor.GibdoWell, Actor.DeathArmos, // Rarely Killable
            Actor.Keese // can bug out and fly out-of-bounds, difficult to kill
            /*Actor.RegularIceBlock, Actor.Bombiwa, Actor.ClocktowerGearsAndOrgan */)] // blocking
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DragonFly,
            Actor.GiantBeee)] // issue being that the one that spins around and doesnt agro hard requires a ranged weapon because the spawn is so high
        [EnemizerSceneBlockSensitive(Actor.Bo, -1)]
        [EnemizerSceneBlockSensitive(Actor.CuttableIvyWall, -1)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Snapper,
        //    Actor.WarpDoor, Actor.ClocktowerGearsAndOrgan)] // Snapper spawns just on top of its chest, its possible a non-killable actor is placed int he wya
        [EnemizerSceneBlockSensitive(Actor.Snapper, -1)]
        WoodfallTemple = 0x18,

        [FileID(1222)]
        [SceneInternalId(0x1C)]
        [EnemizerSceneBlockSensitive(Actor.LargeSnowball, -1)]
        PathToMountainVillage = 0x19,

        [FileID(1224)]
        [SceneInternalId(0x1D)]
        [ClearEnemyPuzzleRooms(5)] // wizrobe room
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Skulltula,
            Actor.Bombiwa)] // can block jumping
        IkanaCastle = 0x1A,

        [FileID(1235)]
        [SceneInternalId(0x1E)]
        DekuPlayground = 0x1B,

        [FileID(1237)]
        [SceneInternalId(0x1F)]
        OdolwasLair = 0x1C,

        // tag: archery
        [FileID(1239)]
        [SceneInternalId(0x20)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Clock,
            Actor.Keese, Actor.Takkuri)]
        TownShootingGallery = 0x1D,

        [FileID(1241)]
        [SceneInternalId(0x21)]
        // 11 dinofos room, 6/12 wizrobe
        [ClearEnemyPuzzleRooms(1, 2, 5, 6, 9, 12)] // 1:wolfos room, 2: east freezard, 5: north freezard, 6: wizr1, 9:chu room, 12: bk wizrob
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Wolfos, // can cover a switch, don't allow problem actors
        //    Actor.WarpDoor, Actor.WarpToTrialEntrance, Actor.ClocktowerGearsAndOrgan, Actor.Bumper, Actor.IkanaGravestone, Actor.Tijo)]
        [EnemizerSceneBlockSensitive(Actor.Wolfos, -1)]
        [EnemizerSceneBlockSensitive(Actor.LargeSnowball, -1)]
        [EnemizerSceneBlockSensitive(Actor.SmallSnowball, -1)] // iceicle room
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RedBubble, // spawns in hot lava, keep wood enemies out
            Actor.Peahat, Actor.MadShrub, Actor.Postbox, Actor.DekuBaba, Actor.DekuBabaWithered,
            Actor.Freezard, Actor.RegularIceBlock, Actor.Eeno, Actor.Wolfos, Actor.Dinofos, Actor.Snapper)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bo,
            Actor.Bombiwa /* Actor.RegularIceBlock, Actor.IkanaCanyonHookshotStump, */ )] // could block the fairy bubble
        [EnemizerSceneBlockSensitive(Actor.Bo, -1)]
        [EnemizerSceneBlockSensitive(Actor.IceCavernStelagtite, -1)] // can block the door
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Freezard,
            Actor.PoeSisters, // weird behavior, if the killing blow of meg at long range can stop chests from spawning
            Actor.CircleOfFire, // if it gets placed on the one on top of a chest the player is screwed
            Actor.DragonFly, // if you kill it at long range or such that its dying body falls to first floor it wont count
            Actor.WarpDoor, // Cannot walk through them to get to the chest under
            Actor.Wolfos)] // wolfos: ice wolfos can push the regular actual dog backwards through the wall
        [EnemizerSceneBlockSensitive(Actor.Freezard,
            2, // can block the chest
            5)] // can block access to the elevator
        [FairyDroppingEnemies(roomNumber: 11, actorNumber: 2, 3)] // dinofos 
        SnowheadTemple = 0x1E,

        [FileID(1256)]
        [SceneInternalId(0x22)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Carpenter,
        //Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)]
        MilkRoad = 0x1F,

        // this is both the sewer and all of the smaller rooms up top
        [FileID(1258)]
        [SceneInternalId(0x23)]
        // room 9/10 is the virtical water column elevator in the sewers
        // room 10/11 is the side channel hallway with the mine traps (between rooms)
        // room 11/12 is the timed cage prison room
        // room 12/13 is the first sewer underwater maze
        [ClearEnemyPuzzleRooms(0, 1, 2)] // three pirate minibosses
        // TODO need to re-test this
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SpikedMine,
            Actor.LabFish)] // crash unknown reason, float math error
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.PirateTelescope,
            Actor.ClocktowerGearsAndOrgan)] // can block the player into the wall
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.WoodenBarrel,
            Actor.TreasureChest)] // can block the player into the wall
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ZoraEgg,
            Actor.Evan)] // can block the treasurechest
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ZoraEgg,
        //    Actor.Tijo, Actor.Bombiwa, Actor.Bumper)] // blocking a chest
        [EnemizerSceneBlockSensitive(Actor.ZoraEgg, -1)]
        [EnemizerSceneBlockSensitive(Actor.WoodenBarrel, -1)] // in the cage room, can block the ability to hit the switch to leave
        PiratesFortressRooms = 0x20, // tag: Sewer

        // tag: archery
        [FileID(1276)]
        [SceneInternalId(0x24)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Clock,
            Actor.Keese, Actor.Takkuri)]
        SwampShootingGallery = 0x21,

        [FileID(1276)]
        [SceneInternalId(0x25)]
        [DynaHeadroom(64, 64)]  // low default to start
        // to randomize the signs, I added another object and changed the signs to bombiwa, the code expects bombiwa
        //[EnemizerSceneBlockSensitive(Actor.Bombiwa, -1)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Bombiwa, // blocking a few skulltulla
            Actor.RegularIceBlock, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            Actor.Bumper, Actor.ClocktowerGearsAndOrgan)]
        PinnacleRock = 0x22,

        [FileID(1278)]
        [SceneInternalId(0x26)]
        FairyFountain = 0x23, // great fairy

        [FileID(1284)]
        [SceneInternalId(0x27)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch, // blocking a few skulltulla
            Actor.Bombiwa //Actor.StockpotBell, Actor.IkanaGravestone,  Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            /*Actor.Bumper, Actor.ClocktowerGearsAndOrgan*/)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Bombiwa, // blocking a few skulltulla
            Actor.Lulu //Actor.StockpotBell, Actor.IkanaGravestone,  Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            /*Actor.Bumper, Actor.ClocktowerGearsAndOrgan*/)]
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        [EnemizerSceneBlockSensitive(Actor.Bombiwa, -1)]
        [EnemizerSceneBlockSensitive(Actor.ClayPot, -1)] // the upper floor main room can block the softsoil
        [EnemizerSceneBlockSensitive(Actor.CuttableIvyWall, -1)]
        // old, should no longer be needed: Actor.En_Ani, Actor.GoronElder, Actor.Cow, Actor.Tijo , Actor.Postbox,
        SwampSpiderHouse = 0x24,

        [FileID(1291)]
        [SceneInternalId(0x28)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(Actor.SkulltulaDummy,
            Actor.UnusedFallingBridge, // dyna crash loop on player entering
            Actor.GBTFreezableWaterfall,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // Can block the ability to reach rafters to get a skulltoken
        OceanSpiderHouse = 0x25,

        [FileID(1298)]
        [SceneInternalId(0x29)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(Actor.Scarecrow,
            Actor.ClocktowerGearsAndOrgan)] // can block the stairs
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch,
            Actor.DeathArmos)] // might be too much
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        AstralObservatory = 0x26, // and sewer leading to it

        [FileID(1301)]
        [SceneInternalId(0x2A)]
        DekuTrial = 0x27,

        [FileID(1304)]
        [SceneInternalId(0x2B)]
        [DynaHeadroom(24, 20)]  // low default to start, untested
        [EnemizerSceneEnemyReplacementBlock(Actor.Torch, // too close to grotto
            Actor.Dexihand)] // if it grabs you as you fall into a grotto hole it can hardlock
        [EnemizerSceneEnemyReplacementBlock(Actor.Monkey,
            Actor.PalaceGuardDeku)] // if placed behind regular guards, they will pop up to look at you and the other guards will also rise
        [EnemizerSceneEnemyReplacementBlock(Actor.SquareSign,
            Actor.PalaceGuardDeku)] // if it grabs you as you fall into a grotto hole it can hardlock
        //[EnemizerSceneEnemyReplacementBlock(Actor.Bombiwa,
        //    Actor.ClocktowerGearsAndOrgan)] // likely dynacrash if other actors have them too
        [EnemizerSceneBlockSensitive(Actor.Torch, -1)]
        [EnemizerSceneBlockSensitive(Actor.Monkey, -1)] // giant ice block, unused stone stuff at least
        DekuPalace = 0x28,

        [FileID(1308)]
        [SceneInternalId(0x2C)]
        [EnemizerSceneBlockSensitive(Actor.PottedPlant, -1)] // large actors could block the door
        MountainSmithy = 0x29,

        [FileID(1310)]
        [SceneInternalId(0x2D)]
        //[DynaHeadroom(0, 0)] // unknown but it seems higher than most 0x64/0x3D is safe
        //[DynaHeadroom(0x64, 0x3D)] // tested safe
        /*
         *   day:    dyna poly   delta: [634] newsize: [00029E] oldsize: [000024] <- was over the limit (by 14), holy shit there is lots of dyna space here
              day:    dyna vert   delta: [446] newsize: [0001D6] oldsize: [000018]
              night:  dyna poly   delta: [587] newsize: [00026F] oldsize: [000024]
              night:  dyna vert   delta: [410] newsize: [0001B2] oldsize: [000018]
         */
        // except the above was tested with a busted counter, needs retesting, 532 was 4 oversized
        // hmm 498 is still broken, oversized by 2, thinking we have something mis-sized
        // I checked every actor that spawns, their dyna matches what we should have...
        // now I can get a crash with 471, which is spooky how is it this far off? (and in-game it says its over 50 off)
        // measurement of 446 was a pass? hmm, until I can find the cause of the measuremnet descrepency
        [DynaHeadroom(400, 400)] // acceptably tiny risk
        // more testing: 458 recorded was 548 (+4) in the crash screen thats 90 OFF
        //[DynaHeadroom(490, 475)]
        // this actor is mostly ignored, player might not even notice, dont waste lots of object budget on this thing
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ClayPot,
            Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Bombiwa,
            Actor.LikeLike)] // can grab you as you LEAVE and hardlock the game
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BugsFishButterfly, // peahat grotto butterfly
            Actor.UnusedStoneTowerStoneElevator, Actor.UnusedStoneTowerPlatform)] // can cover the peahat grotto
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.DekuBaba,
            Actor.RegularIceBlock, // I dont want to block on all of them, but the big one is a problem for peahat grotto
            Actor.LikeLike)] // can grab you on grotto exit and softlock with only one heart, TODO make special code instead moving them?
        // these actors are only seen in the credits, we should block all large object actors from these spots to save generation time
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ViscenMoonLeaveCutscene,
            Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.MutoMoonLeaveCutscene,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.AnjusGrandmaCredits,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.AnjuMotherWedding,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CuriosityShopMan,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CarpentersFromCutscene,
                        Actor.HappyMaskSalesman, Actor.IronKnuckle, Actor.CutsceneZelda, Actor.ClayPot, Actor.RomaniYts, Actor.GoronElder)]
        //*/
        TerminaField = 0x2A, // keikoku, c800 dyna size

        [FileID(1312)]
        [SceneInternalId(0x2E)]
        PostOffice = 0x2B,

        [FileID(1314)]
        [SceneInternalId(0x2F)]
        MarineLab = 0x2C,

        [FileID(1316)]
        [SceneInternalId(0x30)]
        [EnemizerSceneBlockSensitive(Actor.BigPoe, -1)]
        [EnemizerSceneBlockSensitive(Actor.Dampe, -1)] // not sure which one it is, but if its a big thing they cant get past the entrance
        // [ClearEnemyPuzzleRooms(   )] // is big poe reward a clear room reward?
        DampesHouse = 0x2D,

        // Unused = 0x2E,

        // inside of the goron village main building, with the goron shop and merrygoround
        [FileID(1319)]
        [SceneInternalId(0x32)]
        // 128,100 was fine
        [DynaHeadroom(128, 100)]  // limit not found
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GoronSGoro,
                        Actor.GoronWithGeroMask)] // if the sirloin drops on top of him its broken
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Torch,
                        Actor.GoronWithGeroMask)] // if the sirloin drops on top of him its broken
        [EnemizerSceneBlockSensitive(Actor.GoGoron, -1)] // ice block, tall bombiwa, can block shop
        [EnemizerSceneBlockSensitive(Actor.GoronSGoro, -1)] // ice block, tall bombiwa, can block shop
        GoronShrine = 0x2F,

        [FileID(1322)]
        [SceneInternalId(0x33)]
        // 350, 300 was fine 
        [DynaHeadroom(350, 300)]  // limit not found
        ZoraHall = 0x30,

        [FileID(1324)]
        [SceneInternalId(0x34)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Treee,
                        Actor.DekuKing, // if close to scarecrow can hardlock clock skip
                        Actor.Hiploop)] // water explosion
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.ClayPot,
                        Actor.DekuKing)] // if close to scarecrow can hardlock clock skip
        TradingPost = 0x31,

        // the only "small" dyna poly scene... but why
        [FileID(1326)]
        [SceneInternalId(0x35)]
        // we know nothing here, TODO recheck
        // 234, 162 was fine holy shit
        [DynaHeadroom(235, 200)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Cremia,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        [EnemizerSceneEnemyReplacementBlock(Actor.LargeWoodenCrate,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors can break credits
        RomaniRanch = 0x32, // F01, 0xF000 dyna size

        [FileID(1328)]
        [SceneInternalId(0x36)]
        TwinmoldsLair = 0x33,

        [FileID(1330)]
        [SceneInternalId(0x37)]
        // 350,224 was okay at night time
        // ^- this might be old, pre-realization that our counting is off
        // 342poly crashes room 1
        [DynaHeadroom(350, 300, room: 0)] // limit not found
        [DynaHeadroom(250, 250, room: 1)] // 342, X was too big, limit not found (annoying to test)
        //[DynaHeadroom(16,12, room:0)] // we know 16/12 is safe, that might be too conservative
        [EnemizerSceneBlockSensitive(Actor.SquareSign,
            0x21, // too close to fisherman door
            0x23)] // too close to lab door
        [EnemizerSceneEnemyReplacementBlock(Actor.Seagulls,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can stop ting from falling
        GreatBayCoast = 0x34,

        [FileID(1332)]
        [SceneInternalId(0x38)]
        // 132,88 was working fine
        [DynaHeadroom(132, 100)] // limit not found
        [EnemizerSceneEnemyReplacementBlock(Actor.Bombiwa,
                    Actor.LikeLike)] // can hard lock if the player leaves and gets instant-grabbed, TODO consider making a likelike that has a switch flag
        [EnemizerSceneBlockSensitive(Actor.LikeLike, -1)]
        ZoraCape = 0x35,

        [FileID(1334)]
        [SceneInternalId(0x39)]
        LotteryShop = 0x36,

        // Unused = 0x37,

        [FileID(1336)]
        [SceneInternalId(0x3B)]
        //[DynaHeadroom(64, 64)]  // since we place almost nothing here, assume no limit for now until we know what it is
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
        [EnemizerSceneEnemyReplacementBlock(Actor.DekuKing,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors
        [EnemizerSceneEnemyReplacementBlock(Actor.DekuPrincess,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors
        [EnemizerSceneEnemyReplacementBlock(Actor.Butler,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors
        [EnemizerSceneEnemyReplacementBlock(Actor.PalaceGuardDeku,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors
        [EnemizerSceneEnemyReplacementBlock(Actor.Monkey,
            Actor.IronKnuckle, Actor.GuruGuru, Actor.RomaniYts, Actor.CutsceneZelda, Actor.Japas, Actor.Tijo, Actor.Evan)] // singing/audio actors
        DekuKingChamber = 0x3B,

        [FileID(1344)]
        [SceneInternalId(0x3F)]
        GoronTrial = 0x3C,

        [FileID(1347)]
        [SceneInternalId(0x40)]
        // 60, 95 was acceptable, also got 80,109 and that worked
        [DynaHeadroom(109, 109)] // limit not found, pain with so many actors
        // respawning bo can show up here, but I dont want to mark the whole room to not place respawning enemies
        // mirror blocks climbing
        [EnemizerSceneEnemyReplacementBlock(Actor.BadBat,
            Actor.DeathArmos, // light arrow requirement is a bit much, no logical way right now to check if important item behind light arrow
            Actor.Bo, Actor.StoneTowerMirror,
            Actor.MothSwarm, // can block
            Actor.HookshotWallAndPillar, // can block climbing TODO fix in the code since this is silly
            Actor.BronzeBoulder, // doesn't stay in spot, falls to floor, blocks climbing start
            Actor.SpiderWeb)] // TODO would be cool if we could allow this if the item was junk, or logic require fire arrows
        [EnemizerSceneBlockSensitive(Actor.BadBat, -1)] // giant ice block, unused stone stuff at least
        [EnemizerSceneBlockSensitive(Actor.PottedPlant, -1)] // right next to swamp shooting gallery door
        RoadToSouthernSwamp = 0x3D,

        [FileID(1349)]
        [SceneInternalId(0x41)]
        [EnemizerSceneEnemyReplacementBlock(Actor.ClayPot,
            Actor.RegularIceBlock)] // the big one can reach through the ceiling into the chest, blocking the chest
        DoggyRacetrack = 0x3E,

        [FileID(1351)]
        [SceneInternalId(0x42)]
        [ClearEnemyPuzzleRooms(0, 1)] // respawning enemies can break chick round-up
        CuccoShack = 0x3F,

        [FileID(1353)]
        [SceneInternalId(0x43)]
        [DynaHeadroom(0, 0)] // seems very low, for now disable
        [EnemizerSceneEnemyReplacementBlock(Actor.Dampe,
            Actor.Treee)]// for some reason big poe in the first room can cause camera to lock, unknown reason
        [EnemizerSceneEnemyReplacementBlock(Actor.OrangeGraveyardFlower,
            Actor.En_Ani, Actor.SwampTouristGuide, Actor.Secretary, Actor.Scientist, Actor.Takaraya,
            Actor.BombersBlueHat, Actor.BomberHideoutGuard)] // talking actors can stop gave clipping, requested blocking 
        [EnemizerSceneBlockSensitive(Actor.Dampe, -1)]
        [EnemizerSceneBlockSensitive(Actor.OrangeGraveyardFlower, -1)]
        IkanaGraveyard = 0x40,

        [FileID(1356)]
        [SceneInternalId(0x44)]
        GohtsLair = 0x41,

        [FileID(1358)]
        [SceneInternalId(0x45)]
        // has to be smaller than 24, 16 as one ikana stump in room zero was an issue
        //... but one 28,16 swlift was working fine???
        // one lily(12, 8) plus one darmani grave (10, 8) and one ice platform(22,13) was too much tho (43,29)
        //[DynaHeadroom(28, 16, room: 0)]
        //[DynaHeadroom(28, 16, room: 2)]
        //[DynaHeadroom(10, 8)] // hotfix: lower to avoid dyna collider while I wait for better data as to issue
        [DynaHeadroom(0, 0)] // sheesh, just get it over with
        //[EnemizerSceneEnemyReplacementBlock(Actor.DekuBabaWithered, // bit annoying 
        //    Actor.Peahat, Actor.LikeLike, Actor.Freezard)]
        //[EnemizerSceneEnemyReplacementBlock(Actor.DragonFly, // blocks deku flying 
        //    Actor.UnusedStoneTowerPlatform, Actor.UnusedPirateElevator)]
        [EnemizerSceneEnemyReplacementBlock(Actor.TallGrass,
            Actor.ClocktowerGearsAndOrgan, Actor.RegularIceBlock)] // suspected too large and can block the owl
        [EnemizerSceneEnemyReplacementBlock(Actor.Octarok,
            Actor.Obj_Boat, Actor.SwampBoat)] // dyna crashing from just one boat and nothing else
        [EnemizerSceneBlockSensitive(Actor.DragonFly, -1)]
        [EnemizerSceneBlockSensitive(Actor.En_Owl, -1)]
        SouthernSwamp = 0x42,

        [FileID(1362)]
        [SceneInternalId(0x46)]
        // 16 lilypads: 192,128,
        // replaced by 12 platforms and one elevator
        // (12x28,16) + (12,8) = 348, and that is 20 over the limit
        // 348 - 192 - 22 = 134 max poly
        [DynaHeadroom(130, 116, room: 2)]  // 116 is estimate
        [EnemizerSceneEnemyReplacementBlock(Actor.ClayPot,
            Actor.En_Ani, Actor.GaboraBlacksmith, Actor.BomberHideoutGuard, // their talk box is so big they can dialogue block the flower
            Actor.Takaraya, Actor.Secretary, Actor.DekuKing, Actor.InjuredKoume, Actor.GoronElder, Actor.GoronKid, Actor.MadamAroma,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator, // assume they will block deku flower pop-up
            Actor.RegularIceBlock,
            Actor.ClocktowerGearsAndOrgan // blocking the flower
        )]
        [EnemizerSceneEnemyReplacementBlock(Actor.Hiploop, // respawning bo can show up here, but I dont want to mark the whole room to not place respawning enemies
                                                           //Actor.Peahat, // big ground type blocks the bridge at night, can't separate the big one and the small ones
            Actor.BabaIsUnused, // blocks the bridges
            Actor.Grog // still blocks the bridges
            /* Actor.Wolfos */ )] // wolfos:iceblock
        [EnemizerSceneBlockSensitive(Actor.Hiploop, -1)]
        [EnemizerSceneBlockSensitive(Actor.ClayPot, -1)]
        Woodfall = 0x43,

        [FileID(1364)]
        [SceneInternalId(0x47)]
        ZoraTrial = 0x44,

        [FileID(1366)]
        [SceneInternalId(0x48)]
        GoronVillageSpring = 0x45,

        [FileID(1369)]
        [SceneInternalId(0x49)]
        // 8 biobabas in the ceiling caused crash in room 8, at 10x8 and we went over by 7, so 72 should be safe
        [DynaHeadroom(68, 68)]
        //3: clear the biobabas, 5 is gekko, 8 is wart
        [ClearEnemyPuzzleRooms(3, 5, 7)]
        [EnemizerSceneEnemyReplacementBlock(Actor.Skulltula,
            Actor.BigPoe)] // for some reason big poe in the first room can cause camera to lock, unknown reason
        [EnemizerSceneEnemyReplacementBlock(Actor.Dexihand,
            Actor.Bumper)] // can block the water channel
        [EnemizerSceneEnemyReplacementBlock(Actor.SkullFish,
            Actor.Desbreko)] // lag
        [FairyDroppingEnemies(roomNumber: 8, actorNumber: 7)] // skulltula in first room
        [EnemizerSceneBlockSensitive(Actor.Dexihand, -1)]
        GreatBayTemple = 0x46,

        // tag: beavers
        [FileID(1386)]
        [SceneInternalId(0x4A)]
        WaterfallRapids = 0x47,

        [FileID(1388)]
        [SceneInternalId(0x4B)]
        [ClearEnemyPuzzleRooms(12)] // 12 is big poe
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
        [ClearEnemyPuzzleRooms(0x1)] // the guantlet is only one big room
        SakonsHideout = 0x4C,

        [FileID(1417)]
        [SceneInternalId(0x50)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SmallSnowball,
            Actor.RealBombchu, Actor.Snapper, Actor.Beamos)] // can hit you as you are climbing up blocking assension
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GoGoron,
            Actor.RealBombchu)] // can hit you as you are climbing up blocking assension
        MountainVillage = 0x4D,

        [FileID(1419)]
        [SceneInternalId(0x51)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SpiritHouseOwner,
            Actor.Torch, Actor.IshiRock)] // very lame
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
        //[DynaHeadroom(64, 64)]  // small scene, assume we have lots of budget until proven otherwise
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Clock,
            Actor.BadBat, Actor.GoldSkulltula, Actor.RealBombchu)] // z-targetable can be annoying in the sword test
        [EnemizerSceneBlockSensitive(Actor.KendoSensei, -1)]
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
        // one swlift, 0x1C/0x10: works
        // one swlift, 3 cuttable ivy, 0x22/0x1C, works
        // two swlift was fine?? how did I even get crashes previously??
        [DynaHeadroom(34, 32)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Keese,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the whole assension
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Beamos,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the whole assension
        StoneTower = 0x55,

        [FileID(1444)]
        [SceneInternalId(0x59)]
        InvertedStoneTower = 0x56, // NOT TEMPLE

        [FileID(1446)]
        [SceneInternalId(0x5A)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(Actor.HoneyComb,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // until I change the combs to be wall or flying only this is weird
        [EnemizerSceneEnemyReplacementBlock(Actor.BronzeBoulder,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator,
            Actor.RegularIceBlock, Actor.ClocktowerGearsAndOrgan, Actor.Bumper, Actor.Bombiwa, Actor.PushableBlock, Actor.Bg_Heavy_Block,
            Actor.LostWoodsCutsceneTrees, Actor.Treee)] // tall blocks door
        MountainVillageSpring = 0x57,

        [FileID(1449)]
        [SceneInternalId(0x5B)]
        [EnemizerSceneBlockSensitive(originalEnemy: Actor.SmallSnowball, -1)] // can block the grotto, TODO see about just moving them instead
        [EnemizerSceneBlockSensitive(originalEnemy: Actor.LargeSnowball, -1)] // can block the ramps
        PathToSnowhead = 0x58,

        [FileID(1451)]
        [SceneInternalId(0x5C)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneBlockSensitive(originalEnemy: Actor.Flagpole, -1)] // ice block can stop access to the whole dungeon
        [EnemizerSceneEnemyReplacementBlock(Actor.Bo,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // can block the twisted path into snowhead temple
        Snowhead = 0x59,

        [FileID(1453)]
        [SceneInternalId(0x5D)]
        [EnemizerSceneBlockSensitive(originalEnemy: Actor.SmallSnowball, -1)] // can block the grotto, TODO see about just moving them instead
        // todo test after snowball merge
        [DynaHeadroom(126, 126)]  // limit not seed, but this is fine in spring
        TwinIslands = 0x5A, // winter

        [FileID(1455)]
        [SceneInternalId(0x5E)]
        // 126,88 was fine
        [DynaHeadroom(126, 126)]  // limit not seen
        TwinIslandsSpring = 0x5B,

        [FileID(1457)]
        [SceneInternalId(0x5F)]
        GyorgsLair = 0x5C,

        [FileID(1459)]
        [SceneInternalId(0x60)]
        [ClearEnemyPuzzleRooms(2, 3, 4, 5)] // the miniboss rooms
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.CeilingSpawner,
            Actor.Shabom)] // report of them not spawning or falling out of bounds and softlock
        SecretShrine = 0x5D,

        [FileID(1466)]
        [SceneInternalId(0x61)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.MysteryHand,
            Actor.StockpotBell // so big it goes through the back of the stairs and blocks the stairs
            )]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Gorman,
            /* Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire,*/ Actor.LikeLike)]
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Anju,
        //    Actor.AnjusGrandma, Actor.AnjusGrandmaCredits)] // this this was just cutscenes, not the same bug
        [EnemizerSceneBlockSensitive(Actor.Gorman, -1)]
        [EnemizerSceneBlockSensitive(Actor.MushroomCloud, -1)] // specifically the one in the lavatory
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.RosaSisters,
        //    Actor.StockpotBell, Actor.Bumper, Actor.CircleOfFire, Actor.LightBlock,
        //   Actor.Eyegore)]
        [EnemizerSceneBlockSensitive(Actor.RosaSisters, -1)]
        [EnemizerSceneBlockSensitive(Actor.MysteryHand, -1)]
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
        // TODO explore this one
        //[DynaHeadroom(28, 16)] // this is NOT safe, got a crash with unkown actor/values
        // +4,0 with two large crates was fine, +2 elevator-1crate was also fine,
        //   +3hookshotpillars-1box-3poles is also fine, recorded as 20,4 in the log, so that might be closer to the limit
        [DynaHeadroom(20, 20)] // should be safe
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.LargeWoodenCrate,
            Actor.LikeLike // can grab and spit you from behind the fense
        )]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Treee,
            Actor.PoeBalloon, Actor.BigPoe, // Actor.FloorMaster,
            //Actor.SleepingScrub, // too much dyna, spawns too many flowers and flower companions
            Actor.GrassRockCluster // can spawn too many rocks
            //Actor.GibdoIkana, Actor.ReDead, Actor.GibdoWell,
            /* Actor.CircleOfFire, Actor.LightBlock, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator */)]
        [EnemizerSceneBlockSensitive(Actor.Treee, -1)]
        GormanRaceTrack = 0x67, // tag gormantrack

        [FileID(1508)]
        [SceneInternalId(0x6B)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneBlockSensitive(Actor.BombFlower, -1)]
        [EnemizerSceneBlockSensitive(Actor.UglyTree, -1)]
        GoronRacetrack = 0x68,

        [FileID(1510)]
        [SceneInternalId(0x6C)]
        // 147 111 seen working fine
        [DynaHeadroom(145,145)]  // limit not seen
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Gorman,
        //    Actor.ClocktowerGearsAndOrgan, Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // organ is huge, covers the mayor's door
        [EnemizerSceneBlockSensitive(Actor.Gorman, -1)] // was moved next to mayors door, large bodies can actually block this
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Gorman,
            Actor.LikeLike // can instant grab the player from the door, softlocking on low health builds
        )]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.Peahat, // small peahat can instant kill 1 heart hero
            Actor.LikeLike,
            Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BomberHideoutGuard,
                Actor.Peahat, Actor.Tijo
        )]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Jugglers,
                                                           Actor.UnusedStoneTowerPlatform)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Anju,
            Actor.GaroMaster)] // if spawning in sct partial, can fall through the floor and dissapear but leave their annoying music
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Postbox,
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator)] // Flying can block the roof leading to the chest
        [EnemizerSceneBlockSensitive(Actor.BombersYouChase, -1)] // chicken holder leads to a chest
        [EnemizerSceneBlockSensitive(Actor.BomberHideoutGuard, -1)] // leads to a whole area with like 4 things
        EastClockTown = 0x69, // ect

        [FileID(1512)]
        [SceneInternalId(0x6D)]
        [DynaHeadroom(64, 64)]  // low default to start
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
        //    Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.Peahat, // small peahat can instant kill 1 heart hero
            Actor.LikeLike)] // If you start with one heart this can be a softlock
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.BombersYouChase,
            Actor.UnusedStoneTowerStoneElevator)]//, Actor.UnusedStoneTowerPlatform)]
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        WestClockTown = 0x6A, // wct

        [FileID(1514)]
        [SceneInternalId(0x6E)]
        //[DynaHeadroom(64, 64)]  // low default to start
        //[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
        //    Actor.PatrollingPirate, Actor.ClocktowerGearsAndOrgan)] // could be annoying, hard to leave
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        [EnemizerSceneBlockSensitive(Actor.SquareSign, -1)]
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.Peahat, // small peahat can instant kill 1 heart hero
            Actor.LikeLike)] // If you start with one heart this can be a softlock
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Sakon,
            /*Actor.UnusedStoneTowerPlatform,*/ Actor.UnusedStoneTowerStoneElevator)] // can hide the grass weirdly
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SquareSign,
            Actor.Beamos)] // can one shot the player as they leave the grotto with 1 heart
        NorthClockTown = 0x6B,

        [FileID(1516)]
        [SceneInternalId(0x6F)]
        [DynaHeadroom(64, 64)]  // low default to start
        /*[EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.Carpenter,
            Actor.RegularIceBlock, Actor.Bumper, Actor.CircleOfFire, Actor.GoronElder, Actor.LightBlock, // can block day 3 chest, TODO move it so we can re-enable
            Actor.UnusedStoneTowerPlatform, Actor.UnusedStoneTowerStoneElevator, // chest can raise to match height,putting it out of reach
            Actor.PatrollingPirate)] // could be annoying, hard to leave
        */
        [EnemizerSceneBlockSensitive(Actor.Carpenter, -1)] // TODO figure out which one is the issue one
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.GateSoldier,
            Actor.Peahat, // small peahat can instant kill 1 heart hero
            Actor.PatrollingPirate, // could be annoying, hard to leave
            Actor.LikeLike )] // If you start with one heart this can be a softlock
        [EnemizerSceneBlockSensitive(Actor.GateSoldier, -1)]
        [EnemizerSceneBlockSensitive(Actor.Kafei, -1)]
        SouthClockTown = 0x6C,

        [FileID(1518)]
        [SceneInternalId(0x70)]
        [DynaHeadroom(64, 64)]  // low default to start
        [EnemizerSceneEnemyReplacementBlock(originalEnemy: Actor.SmallWoodenBox,
            /*Actor.UnusedStoneTowerPlatform,*/ Actor.UnusedStoneTowerStoneElevator)] // can block guruguru and maybe more
        LaundryPool = 0x6D,
    }

}
