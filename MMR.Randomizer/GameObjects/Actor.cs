using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Actor;

namespace MMR.Randomizer.GameObjects
{
    public enum Actor
    {
        // the main enumator value is the actor list ID

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A60)]
        [FileID(46)]
        [ObjectListIndex(0x5)]
        [WaterVariants(0xFF00)]
        [EnemizerScenesExcluded(0x13,0x49)]
        Octarok = 0x8,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B30)]
        [FileID(48)]
        [ObjectListIndex(0x9)]
        //[GroundVariants(1)] //issue: used for replacement, puts ground enemies in air
        [FlyingVariants(1)] 
        WallMaster = 0xA,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A40)]
        [FileID(49)]
        [ObjectListIndex(0xA)]
        [GroundVariants(1,0)]
        [DoublePerRoomMax(1,0)] // which one is the big one?
        Dodongo = 0xB,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [FileID(50)]
        [ObjectListIndex(0xB)]
        [FlyingVariants(0x8003,0x04,0)]
        Keese = 0xC,

        [ActorizerEnabled]
        [FileID(54)]
        [ObjectListIndex(0xF)]
        [GroundVariants(0xFFFF)]
        [RespawningVarients(0xFFFF)] // killing one not possible
        // I would like a flying variant, but they seem to drop like a rock instead of float down
        [EnemizerScenesExcluded(0x15, 0x29, 0x35, 0x42, 0x10)]
        FriendlyCucco = 0x11,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32C0)]
        [FileID(55)]
        [ObjectListIndex(0x12)]
        [GroundVariants(0xFFFD,0xFFFE,0xFFFF)] // FF does not exist in MM vanilla, red variety
        [WaterVariants(0xFFFE)] 
        Tektite = 0x12,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [FileID(56)]
        [ObjectListIndex(0x14)]
        [GroundVariants(0)]
        Peahat = 0x14,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [FileID(58)]
        [ObjectListIndex(0x17)]
        [GroundVariants(0)]
        [DoublePerRoomMax(0)]
        [EnemizerScenesExcluded(0x60)] // issue: spawn is too high, needs to be lowered
        [EnemizerScenesPlacementBlock(Scene.BeneathGraveyard)] // crash if placed on dinofos
        Dinofos = 0x19,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [FileID(62)]
        [ObjectListIndex(0xE)]
        [FlyingVariants(0)] // 0 works, but OOT used FFFF
        [EnemizerScenesExcluded(0x69)]
        Shabom = 0x1D,// the flying bubbles from Jabu Jabu, exist only in giants cutscenes

        //[ActorizerEnabled] // crashes, goddamnit
        [FileID(65)]
        [ObjectListIndex(0x1)]
        [GroundVariants(0)] // if any work, unknown
        Ben = 0x21,

        [ActorizerEnabled]
        [ObjectListIndex(0xBC)]
        // the frogs that show up when you kill hte other frog, those are unknown vars because they are spawned by the dead miniboss
        [GroundVariants(3,4)] // 3 is southern swamp, 4 is laundry pool
        [RespawningVarients(3,4)]
        Frog1 = 0x22,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [FileID(67)]
        [ObjectListIndex(0x20)]
        [FlyingVariants(0xEF,0x7F,4,0x3F)]
        //[GroundVariants(0xEF,0x7F,4)] // remember, this works for _spawns_
        [EnemizerScenesExcluded(0x1B, 0x27, 0x28, 0x40)]
        Skulltula = 0x24,

        //[EnemizerEnabled] // sometimes crash, cause unknown
        [ActorInitVarOffset(0x1CC0)]
        [FileID(71)]
        [ObjectListIndex(0x1D)]
        // 2 worked in snowhead, seems to fly in a straight line though, pathing?
        [FlyingVariants(2)] // 3 works, 1+4 crashes, assuming 7 also crashes because probably a flag
        [SinglePerRoomMax(2)]
        [RespawningVarients(2)] // they do NOT respawn, this is temporary: light arrow req makes them difficult to kill early in the game
        [EnemizerScenesExcluded(0x18)] // 0x18 is ISTT
        // scenes that seem fine: path to snowhead, grottos, well, road to southern swampm ikana canyon, spring twin islands
        // graveyard doesnt crash, but he doesn't spawn here either? its just an empty sky
        [EnemizerScenesPlacementBlock(Scene.AstralObservatory, Scene.RoadToIkana, 
            Scene.Woodfall, Scene.PathToMountainVillage, Scene.IkanaCastle,
            Scene.MountainVillageSpring, Scene.BeneathGraveyard,
            Scene.IkanaGraveyard )] // known crash locations
        DeathArmos = 0x2D,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [FileID(72)]
        [ObjectListIndex(0x2A)]
        [GroundVariants(0xFFFF)]
        [RespawningVarients(0xFFFF)] // cannot kill
        [EnemizerScenesExcluded(0x1F, 0x6B)]
        BombFlower = 0x2F,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)]
        [FileID(73)]
        [ObjectListIndex(0x30)]
        [GroundVariants(0xFFFF)]
        Armos = 0x32,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [FileID(74)]
        [ObjectListIndex(0x31)]
        [GroundVariants(0)]
        //[EnemizerScenesExcluded(0x1B)] // pretty sure this was just because respawning type
        DekuBaba = 0x33,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D30)]
        [FileID(81)]
        [ObjectListIndex(0x40)]
        [GroundVariants(0xFF02,0xFF00,  0xFF01)]
        [EnemizerScenesExcluded(0x46, 0x2B)]
        MadShrub = 0x3B,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1AF0)]
        [FileID(82)]
        [ObjectListIndex(0x51)]
        [GroundVariants(0)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall)] // visible waiting below the bridges
        RedBubble = 0x3C,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [FileID(84)]
        [ObjectListIndex(0x51)]
        [FlyingVariants(0xFFFF)]
        [RespawningVarients(0xFFFF)]
        BlueBubble = 0x3E,// cursed

        [EnemizerEnabled] //hardcoded values for his entrance spawn make the camera wonky
        [ObjectListIndex(0x52)]
        [GroundVariants(0)]
        [SinglePerRoomMax(0)] // only fight her if you fight only one
        [EnemizerScenesExcluded(0x18)] // lets not randomize his normal spawn
        Gomess = 0x43,

        [EnemizerEnabled]
        //[ItemsReqRemove(Item.ItemBombBag)]
        [ActorInitVarOffset(0x1240)]
        [FileID(89)]
        [ObjectListIndex(0x6A)]
        [GroundVariants(0x600,0x800,0x500,0xFF00,0x300)]
        Beamos = 0x47,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3200)]
        [FileID(92)]
        [ObjectListIndex(0x9)]
        [GroundVariants(0)]
        FloorMaster = 0x4A,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32A0)]
        [FileID(93)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0x7F07,0x7F05,0x7F06)]
        [EnemizerScenesExcluded(0x4b,0x13)] // gibdo locations, but share the same object so gets detected
        ReDead = 0x4C,

        //[ActorizerEnabled] // broken: crash
        [FileID(102)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(1)]
        MajoraBalloonSewer = 0x5F,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [FileID(106)]
        [ObjectListIndex(0x8E)]
        [WaterVariants(0)]
        Shellblade = 0x64,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [FileID(108)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1,2)]
        [RespawningVarients(01,02)]
        DekuBabaWithered = 0x66,

        //[ActorizerEnabled] // works but her object is huge, and you cant talk or interact with her
        [FileID(204)]
        [ObjectListIndex(0xA2)]
        [GroundVariants(0)]
        [WaterVariants(0)]
        [SinglePerRoomMax(0)]
        Ruto = 0x69,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2330)]
        [FileID(112)]
        [ObjectListIndex(0xAB)]
        // 2 is ocean bottom, 0 is one in shallow shore water, 3 is land and one in shallow water
        [WaterVariants(0,2)]
        [GroundVariants(3)]
        LikeLike = 0x6C,

        //[EnemizerEnabled] // we dont actually want this detected automatically, this will be added per-likelike manually
        //[ObjectListIndex(0xBE)] // this is really the shield, we're using it as the second likelike object
        [ObjectListIndex(0xB3)] // this is really the shield, we're using it as the second likelike object
        // 2 is ocean bottom, 0 is one in shallow shore water, 3 is land and one in shallow water
        //[GroundVariants(3)]
        LikeLikeShield = 0x28E, // 28E is a dummy actor ID, we only use it because it will never conflict with enemizer

        [EnemizerEnabled]
        [ActorInitVarOffset(0x26EC)]
        [FileID(127)]
        [ObjectListIndex(0xD8)]
        [GroundVariants(0xFF03,0xFF02,0xFF01)]
        IronKnuckle = 0x84,

        //[EnemizerEnabled] //broken: object 3
        [FileID(132)]
        //[ObjectListIndex(0xF8)] // special case value, not real object
        // and object 3 is so massive it never gets chosen even if we try to shove it into the object list
        [GroundVariants(0x3F7F)] // what if water type too? bottom of the ocean pot
        FlyingPot = 0x8D,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [FileID(134)]
        [ObjectListIndex(0xE4)]
        [GroundVariants(02,0x2001,0x300F,0x100F)]
        Freezard = 0x8F,

        [ActorizerEnabled]
        [FileID(137)]
        [ObjectListIndex(0x12A)]
        [GroundVariants(0x807F, 0x8004, 0x8002)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [RespawningVarients(0x807F, 0x8004, 0x8002)] // dont spawn where you can cause trouble
        [EnemizerScenesExcluded(0x38, 0x2D)]
        Bombiwa = 0x92,

        //[ActorizerEnabled] // does not spawn
        [FileID(144)]
        [ObjectListIndex(0xF2)]
        [GroundVariants(0x0FFF)]
        [RespawningVarients(0x0FFF)] // cannot kill
        [EnemizerScenesExcluded(0x42)]
        CuccoChick = 0x9D,

        [ActorizerEnabled]
        [FileID(151)]
        [ObjectListIndex(0xF4)]
        [GroundVariants(0)]
        [RespawningVarients(0)] // cannot kill
        [EnemizerScenesExcluded(0x07)]
        BeanSeller = 0xA5,

        [ActorizerEnabled] // want a chance to meet him randomly visiting the world
        [FileID(172)]
        [ObjectListIndex(0x11D)]
        //0x1200, 0x1B00, 0x2800 spawns from the ground after you play a song
        // versions: 1200, 1B00, 2800 shows up a lot, 2D00 stonetower, 3200 zora cape
        // trading post version is 1
        // wish I could spawn the ones that dance so they are always dancing when the player gets there
        [GroundVariants(1)]
        [RespawningVarients(0x1200, 0x1B00, 0x2800, 1)] // killing one not possible
        // twinislands 0x5D snowhead 0x21, observatory 0x29, zora hall 0x33, trade 0x34, 0x48 goron village
        [EnemizerScenesExcluded(0x5D, 0x21, 0x29, 0x33, 0x34, 0x37, 0x48, 0x4D, 0x50, 0x38, 0x5B, 0x53, 0x58, 0x5A, 0x5E)] 
        Scarecrow = 0xCA,

        [ActorizerEnabled]
        [FileID(215)]
        [ObjectListIndex(0x132)]
        // 0xFFFF does not spawn
        // 0xA9F and the 0x29F crashes, but... was there ever a dog in that house?
        // 3FF is SCT dog, 0x22BF is spiderhouse dog, makes no sense if use mask
        // 0xD9F is old ranch dog WORKS, racetrack dogs unknown, spawned by the game
        [GroundVariants(0x3FF, 0x22BF)]
        [RespawningVarients(0x3FF, 0x22BF)] // killing one not possible
        [EnemizerScenesExcluded(0x6F, 0x10, 0x27, 0x35)]
        Dog = 0xE2,

        [ObjectListIndex(0x133)]
        //[GroundVariants(0x1E,0x2A02,0x2C0A,0x320F)]
        //[RespawningVarients(0x3FF, 0x22BF)] // killing one not possible
        CrateLarge = 0xE5,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3E40)]
        [FileID(222)]
        [ObjectListIndex(0x141)]
        [GroundVariants(0xFF01,0xFF81,0xFF00)]
        Wolfos = 0xEC,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D60)]
        [FileID(223)]
        [ObjectListIndex(0x142)]
        // 0x0042 is swinging from tree, looks stupid if spawns in the ground, 22 is sitting on the edge of a bookcase, looks weird on the ground
        [GroundVariants(0x0032)]
        [EnemizerScenesExcluded(0x43,0x28)]
        //[RespawningVarients(0x32)] the ones that circle the tombs, but dont respawn if placed anywhere else it seems, ignore
        Stalchild = 0xED,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x28F0)]
        [FileID(224)]
        [ObjectListIndex(0x143)]
        [GroundVariants(0x46,0x67,0x88,0xA9,0xCA, 0x4B,0x6C,0x8D,0xAE,0xCF, 0x50,0x71,0x92,0xB3,0xD4, 0x83,0xA4,0xC5,0x41,0x62)]
        [RespawningVarients(0x46,0x67,0x88,0xA9,0xCA, 0x4B,0x6C,0x8D,0xAE,0xCF, 0x50,0x71,0x92,0xB3,0xD4, 0x83,0xA4,0xC5,0x41,0x62)] // cannot kill
        [EnemizerScenesExcluded(0x66, 0x47, 0x3F, 0x2A)]
        GossipStone = 0xEF,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [FileID(226)]
        [ObjectListIndex(0x6)]
        [FlyingVariants(0)]
        [RespawningVarients(0)]
        Guay = 0xF1,

        [ActorizerEnabled]
        [FileID(227)]
        [ObjectListIndex(0x146)]
        [GroundVariants(0,2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [RespawningVarients(0,2)] // cannot kill
        [EnemizerScenesExcluded(0x35, 0x4B, 0x07, 0x10)]
        Cow = 0xF3,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [FileID(241)]
        [ObjectListIndex(0x14E)]
        [FlyingVariants(0,02,03)]
        DragonFly = 0x109,

        //[EnemizerEnabled] //crash
        [ActorInitVarOffset(0x3688)]
        [FileID(248)]
        [ObjectListIndex(0x155)]
        [GroundVariants(0x2243)]
        Garo = 0x112, //113 is the garo, but 112 is the encounter to get garo

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3760)]
        [FileID(271)]
        [ObjectListIndex(0x15E)]
        [WaterVariants(04,02,01,0)]
        [EnemizerScenesExcluded(0x49)]
        BioDekuBaba = 0x12D,

        //[EnemizerEnabled] // todo: try randomizing
        [FileID(278)]
        [ObjectListIndex(0x161)]
        Raft = 0x13A,// carniverous raft, woodfall

        //[EnemizerEnabled] // does not spawn, tcrf can get it to spawn but it does nothing
        //[ObjectListIndex(0x161)]
        //[GroundVariants(0)]
        //[RespawningVarients(0)] // cannot kill
        //Frog2 = 0x147, // this doesn't exist in the actors by area list, not vanilla

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [FileID(296)]
        [ObjectListIndex(0x16A)]
        [GroundVariants(0x0C01,0x1402,0xFF03,0xFF01,0xFF00,0x0A01,0x0202,0x801,0xFF02)]
        [EnemizerScenesExcluded(0x49)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwampClear)] // crash transitioning witch shop room
        // termina field, ff00 gbt waterchu, the rest are assumed respawn until proven otherwise
        [RespawningVarients(0xFF03,0xFF01,0xFF00,    0x0C01,0x1402,0x0A01,0x0202,0x801,0xFF02)]
        ChuChu = 0x14A,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [FileID(297)]
        [ObjectListIndex(0x16B)]
        [WaterVariants(0x0F00,0x0300)]
        [SinglePerRoomMax(0x0F00, 0x0300)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp)] // massive lag
        Desbreko = 0x14B, // dead fish swarm from pirates fortress

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1250)]
        [FileID(307)]
        [ObjectListIndex(0x171)]
        [GroundVariants(0)]
        Nejiron = 0x155,// Rolling exploding rock in Ikana

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [FileID(313)]
        [ObjectListIndex(0x172)]
        [FlyingVariants(0xFF34,0xFF02,0xFF03,0xFF9F,0x019F,0x0102,0x0103)]
        [EnemizerScenesExcluded(0x18)]
        BadBat = 0x15B,

        [ActorInitVarOffset(0x37D0)]
        [FileID(315)]
        [ObjectListIndex(0x178)]
        Wizrobe = 0x15D,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1830)]
        [FileID(322)]
        [ObjectListIndex(0x17A)]
        [FlyingVariants(0x9605, 0x3205, 0x6405, 0x8C05)]
        // this variety is slow spawn, meaning you have to walk up to it: 0x2800, 0x3200, 0xC200, 0xFA00
        [GroundVariants(0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0xFF01, 0xFF05, 0xC200, 0xFA00, 0xFA01)] 
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [RespawningVarients(0x6404,0x7804, 0x9605,0x3205,0x6405,  0x8C05, 0xFF05)]
        Bo = 0x164, //boe

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [FileID(331)]
        [ObjectListIndex(0x181)]
        // the snowhead version that doesnt aggro is 01
        [GroundVariants(0x8C,0x28,0x3C,0x46,0x32,0x1,0x8023,0x5,0x14,0x8028,0x8014)]
        // the values on the left are tested respawning, the values on the right are untested, conservative assume respawning
        [RespawningVarients(0x8014,0x8028,0x8023,   0x0032,0x0005,0x0014)] 
        RealBombchu = 0x16F,

        [EnemizerEnabled] // biggest issue: they dont really attack, this isn't the version that spawns over and over
        [ActorInitVarOffset(0x1C6C)]
        [FileID(346)]
        [ObjectListIndex(0x16B)]
        // A2 might be the ones that respawn over and over, the "Encounter"
        // 82 and 62 are found in the map room, both just kinda spin, never engages
        // zora cape has 000, same as the other two
        [WaterVariants( 0xA2, 0x82, 0x62, 0 )]
        SkullFish = 0x180,

        //[ActorizerEnabled] // crash
        [FileID(349)]
        [ObjectListIndex(1)]
        //[GroundVariants(0x7F, 0x17F)] // both crash for some reason
        [RespawningVarients(0x7F, 0x17F)] // cannot kill
        // ikana castle, deku palace, nct, woodfall, woodfall temple
        [EnemizerScenesExcluded(0x1B)] 
        DekuFlower = 0x183,

        //[EnemizerEnabled] // AI gets confused, backwalks forever
        [ActorInitVarOffset(0x445C)]
        [FileID(250)]
        [ObjectListIndex(0x18D)]
        [GroundVariants(0x700)]
        [SinglePerRoomMax(0x700)]
        Eyegore = 0x184,// walking laser cyclops in inverted stone tower

        //[ActorizerEnabled] // broken: 0 doesnt spawn, and the rest explode almost instantly
        [FileID(381)]
        [ObjectListIndex(0x19B)]
        //[FlyingVariants(0)] // doesn't spawn unless in the ranch for some reason, shame too this is the one in the ranch
        [FlyingVariants(0x8050,0x805A,0x8064)] // explodes randomly when entering scene
        [RespawningVarients(0x8050, 0x8064, 0x805A)]
        [EnemizerScenesExcluded(0x35)] // dont replace actual romani balloons
        PoeBalloon = 0x1A6,

        [FileID(0x1A8)]
        [ObjectListIndex(0x19E)]
        //[WaterVariants( unk )]
        BigOcto = 0x1A8,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x21C0)]
        [FileID(406)]
        [ObjectListIndex(0x1A6)]
        [GroundVariants(0x0)]
        Snapper = 0x1BA,

        [ActorInitVarOffset(0x1FD0)]
        [FileID(426)]
        [ObjectListIndex(0x1B5)]
        // good candidate for wall enemy
        Dexihand = 0x1D1,// ???'s water logged brother

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2940)] // combat music disable does not work
        [FileID(435)]
        [ObjectListIndex(0x75)]
        [GroundVariants( 0x1E9, 0x1F4, 0x208, 0x214, 0x224, 0x236, 0x247, 0x253, 0x262, 0x275, 0x283, 0x291, 0x2A0 )]
        [EnemizerScenesExcluded(0x13, 0x4B)] // needed for well exploration
        GibdoWell = 0x1DA,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [FileID(446)]
        [ObjectListIndex(0x1C4)]
        [GroundVariants(0xFF00,0xFF01,00,01)]
        Eeno = 0x1E6,// Snowmen in showhead

        [ActorInitVarOffset(0x3080)]
        [FileID(96)]
        [ObjectListIndex(1)] // uh, this might not be a valid object either
        [FlyingVariants(0)]// uh, wall type, but that doesnt exist
        [EnemizerScenesExcluded(0)]
        SkullWallTula = 0x1E7,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x36A0)]
        [FileID(448)]
        [ObjectListIndex(0x1C5)]
        //0x100 is red, 0x200 is blue, 0x300 is green, 00 is purple, however, its difficult to fight more than 2
        [FlyingVariants(0x300, 0x200, 0x100)]
        [GroundVariants(0x300, 0x200, 0x100, 0)]
        [SinglePerRoomMax(0)] // only fight her if you fight only one
        [DoublePerRoomMax(0x300, 0x200, 0x100)] // can be more common
        // no scene exclusion necessary, get spawned by the poe sisters minigame but they aren't actors in the scene to be randomized
        PoeSisters = 0x1E8,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3794)]
        [FileID(449)]
        [ObjectListIndex(0x1C6)]
        [GroundVariants(0,0x0101)]
        Hiploop = 0x1E9,// Charging beetle in Woodfall

        [ActorizerEnabled]
        [ActorInitVarOffset(0xC68)]
        [FileID(458)]
        [ObjectListIndex(0x1CB)]
        [GroundVariants(0,1,2,3)]
        [RespawningVarients(0,1,2,3)] // cannot kill
        [EnemizerScenesExcluded(0x6C, 0x6D, 0x6E, 0x6F)] // all of clocktown
        Postbox = 0x1F2,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [FileID(459)]
        [ObjectListIndex(0x1C3)]
        [FlyingVariants(0x00FF)]
        [GroundVariants(0x00FF)] // this is fine because the only vanilla instance is excluded, so this doesn't describe spawns too
        [EnemizerScenesExcluded(0x16,0x18)] // inverted stonetower and... clock tower roof? what?
        Poe = 0x1F3,

        [ActorizerEnabled] //4x can spawn without issue
        [ObjectListIndex(0x1DF)]
        [GroundVariants(0x1400)] // all other versions are 0x13** or 0x1402
        [EnemizerScenesExcluded(0x32)]
        [SinglePerRoomMax(0x1400)]
        GoronKid = 0x201,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [FileID(475)]
        [ObjectListIndex(0x1EB)]
        [FlyingVariants(0x0)]
        [GroundVariants(0x0)] // spawns really low to ground in vanilla
        [EnemizerScenesExcluded(0x23)] // pirate beehive cutscene
        GiantBeee = 0x204,

        [EnemizerEnabled]
        [FileID(479)]
        [ObjectListIndex(0x1F1)]
        [FlyingVariants(0xFF00,1,0x102)] // 1 is a possible type? well: ff00
        [EnemizerScenesExcluded(0x4B, 0x30)] // well and dampe house must be vanilla for scoopsanity
        [DoublePerRoomMax(0xFF00, 1, 0x102)]
        BigPoe = 0x208,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1C50)]
        [FileID(493)]
        [ObjectListIndex(0x201)]
        [GroundVariants(0xFF,0x80FF)]
        Leever = 0x216,

        //[ActorizerEnabled] // does not spawn? boo
        [ObjectListIndex(0x211)]
        [GroundVariants(1)]
        [WaterVariants(1)]
        [RespawningVarients(1)] // cannot kill
        [EnemizerScenesExcluded(0x33)]
        [SinglePerRoomMax(1)]
        Japas = 0x231,

        //[EnemizerEnabled] // failure to randomize
        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [FileID(524)]
        [ObjectListIndex(0x75)]
        [GroundVariants( 0, 0x81, 0x82, 0x83, 0x84, 0x85 )]
        [EnemizerScenesExcluded(0x13,0x4B)] // dont replace the train
        GibdoIkana = 0x235,

        //[ActorizerEnabled] // unless I write dayonly/nightonly, this is too flukey
        [ObjectListIndex(0xFF)]
        // 00 is the version from the inn, "dont talk to her shes thinking" meaning the rosa sister
        // 2 doesn't ever seen to spawn, day or night, think its a fluke
        [GroundVariants(0x2)] // 01 is laundry pool, but he only spawns at night, ignoring actor time spawn settings for a scene
        [RespawningVarients(0,1,2)] // cannot kill
        [EnemizerScenesExcluded(0x15, 0x70, 0x61)]
        [DoublePerRoomMax(0, 1, 2)]
        GuruGuru = 0x248,

        //[ActorizerEnabled] // doesn't spawn with a flower, looks silly
        [FileID(566)]
        [ObjectListIndex(0x12B)]
        [GroundVariants(0x584)]
        [RespawningVarients(0x584)]
        SleepingScrub = 0x25F,

        //[ActorizerEnabled] // spawned for me, but not streamers? weird time dependencies?
        [FileID(267)]
        [ObjectListIndex(0x23F)]
        [FlyingVariants(7, 5)]
        [RespawningVarients(7, 5)]
        [EnemizerScenesExcluded(0x37, 0x38)]
        Seagulls = 0x267,

        //[ActorizerEnabled] // broken: if you pop it, it locks you in a never ending cutscene
        [FileID(601)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(0)]
        MajoraBalloonNCT = 0x282,

        [ActorizerEnabled] // shows up too much tho, because small object
        [FileID(608)]
        [ObjectListIndex(0x25B)]
        [GroundVariants(0)]
        [RespawningVarients(0)]
        [SinglePerRoomMax(0)]
        ButlersSon = 0x289,

        [ActorInitVarOffset(0x2E30)]
        [FileID(616)]
        [ObjectListIndex(0x22)]
        //[FlyingVariants(0, 1)] // two? one that steals and one that doesn't?
        //[SinglePerRoomMax(0)]
        Takkuri = 0x291,

        [FileID(1114)]
        [ObjectListIndex(0)]
        Empty = -1

    }
}
