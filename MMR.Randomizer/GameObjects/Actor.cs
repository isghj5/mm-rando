using MMR.Randomizer.Attributes.Actor;

namespace MMR.Randomizer.GameObjects
{
    public enum Actor
    {
        // the main enumator value is the MMFileList index
        // todo finish adding ENEMIES.txt content here so we can rid of it

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A60)]
        [ActorListIndex(0x8)]
        [ObjectListIndex(0x5)]
        [WaterVariants(0xFF00)]
        [EnemizerScenesExcluded(0x13,0x49)]
        Octarok = 46,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B30)]
        [ActorListIndex(0xA)]
        [ObjectListIndex(0x9)]
        [GroundVariants(1)]
        [FlyingVariants(1)] // why not tho
        WallMaster = 48,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A40)]
        [ActorListIndex(0xB)]
        [ObjectListIndex(0xA)]
        [GroundVariants(1,0)]
        Dodongo = 49,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [ActorListIndex(0xC)]
        [ObjectListIndex(0xB)]
        [FlyingVariants(0x8003,0x04,0)]
        Keese = 50,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32C0)]
        [ActorListIndex(0x12)]
        [ObjectListIndex(0x12)]
        [GroundVariants(0xFFFD,0xFFFE,0xFFFF)]
        [WaterVariants(0xFFFE)] 
        Tektite = 55,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [ActorListIndex(0x14)]
        [ObjectListIndex(0x14)]
        [GroundVariants(0)]
        //[EnemizerScenesExcluded(0x7)] // I think this only existed because respawn detection was poor
        Peahat = 56,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [ActorListIndex(0x19)]
        [ObjectListIndex(0x17)]
        [GroundVariants(0)]
        [EnemizerScenesExcluded(0x1B,0x21,0x60,0x66)]
        Dinofos = 58,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [ActorListIndex(0x1D)]
        [ObjectListIndex(0xE)]
        [FlyingVariants(0)]
        [EnemizerScenesExcluded(0x69)]
        Shabom = 62, // the flying bubbles from Jabu Jabu

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [ActorListIndex(0x24)]
        [ObjectListIndex(0x20)]
        [FlyingVariants(0xEF,0x7F,4)]
        [GroundVariants(0xEF,0x7F,4)] // because they almost always show up indoors
        [EnemizerScenesExcluded(0x1B, 0x27, 0x28, 0x40)]
        Skulltula = 67,

        //[EnemizerEnabled]  //crash, link to paths?
        [ActorInitVarOffset(0x1CC0)]
        [ActorListIndex(0x2D)]
        [ObjectListIndex(0x1D)]
        [FlyingVariants(3,1,2,4,7)]
        [EnemizerScenesExcluded(0x01)]
        DeathArmos = 71,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)]
        [ActorListIndex(0x32)]
        [ObjectListIndex(0x30)]
        [GroundVariants(0xFFFF)]
        Armos = 73,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [ActorListIndex(0x33)]
        [ObjectListIndex(0x31)]
        [GroundVariants(0)]
        [EnemizerScenesExcluded(0x1B)]
        DekuBaba = 74,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D30)]
        [ActorListIndex(0x3B)]
        [ObjectListIndex(0x40)]
        [GroundVariants(0xFF02,0xFF00)]
        [EnemizerScenesExcluded(0x46)]
        MadShrub = 81,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1AF0)]
        [ActorListIndex(0x3C)]
        [ObjectListIndex(0x51)]
        [GroundVariants(0)]
        RedBubble = 82,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [ActorListIndex(0x3E)]
        [ObjectListIndex(0x51)]
        [FlyingVariants(0xFFFF)]
        [RespawningVarients(0xFFFF)]
        BlueBubble = 84, // cursed

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [ActorListIndex(0x47)]
        [ObjectListIndex(0x6A)]
        [GroundVariants(0x600,0x800,0x500,0xFF00,0x300)]
        Beamos = 89,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3200)]
        [ActorListIndex(0x4A)]
        [ObjectListIndex(0x9)]
        [GroundVariants(0)]
        FloorMaster = 92,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32A0)]
        [ActorListIndex(0x4C)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0x7F07,0x7F05,0x7F06)]
        [EnemizerScenesExcluded(0x4b,0x13)]
        ReDead = 93,

        [ActorInitVarOffset(0x3080)]
        [ActorListIndex(0x1E7)]
        [ObjectListIndex(1)] // uh, this might not be a valid object either
        [FlyingVariants(0)]// uh, wall type, but that doesnt exist
        [EnemizerScenesExcluded(0)]
        SkullWallTula = 96,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [ActorListIndex(0x64)]
        [ObjectListIndex(0x8E)]
        [WaterVariants(0)]
        Shellblade = 106,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [ActorListIndex(0x66)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1,2)]
        [RespawningVarients(01,02)]
        DekuBabaWithered = 108,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2330)]
        [ActorListIndex(0x6C)]
        [ObjectListIndex(0xAB)]
        [WaterVariants(0,2,3)]
        [GroundVariants(0,2,3)]
        LikeLike = 112,

        [EnemizerEnabled]
        //[ActorInitVarOffset( )]
        [ActorListIndex(0x84)]
        [ObjectListIndex(0xD8)]
        [GroundVariants(0xFF03,0xFF02,0xFF01)]
        IronKnuckle = 127,

        //[EnemizerEnabled] //broken: object 3
        [ActorListIndex(0x8D)]
        [ObjectListIndex(3)] // special case value, not real object
        [GroundVariants(0x3F7F)] // what if water type too? bottom of the ocean pot
        FlyingPot = 132,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [ActorListIndex(0x8F)]
        [ObjectListIndex(0xE4)]
        [GroundVariants(02,0x2001,0x300F,0x100F)]
        Freezard = 134,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3E40)]
        [ActorListIndex(0xEC)]
        [ObjectListIndex(0x141)]
        [GroundVariants(0xFF01,0xFF81,0xFF00)]
        Wolfos = 222,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D60)]
        [ActorListIndex(0xED)]
        [ObjectListIndex(0x142)]
        // 0x0042 is swinging from tree, looks stupid if spawns in the ground
        [GroundVariants(0x0022,0x0032)]
        [EnemizerScenesExcluded(0x43,0x28)]
        //[RespawningVarients(0x32)] the ones that circle the tombs, but dont respawn if placed anywhere else it seems, ignore
        Stalchild = 223,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [ActorListIndex(0xF1)]
        [ObjectListIndex(0x6)]
        [FlyingVariants(0)]
        [RespawningVarients(0)]
        Guay = 226,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [ActorListIndex(0x109)]
        [ObjectListIndex(0x14E)]
        [FlyingVariants(0,02,03)]
        DragonFly = 241,

        [ActorInitVarOffset(0x3688)]
        [ActorListIndex(0x113)]
        [ObjectListIndex(0x155)]
        Garo = 248,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3760)]
        [ActorListIndex(0x12D)]
        [ObjectListIndex(0x15E)]
        [WaterVariants(04,02,01,0)]
        [EnemizerScenesExcluded(0x49)]
        BioDekuBaba = 271,

        //[EnemizerEnabled] // todo: try randomizing
        [ActorListIndex(0x13A)]
        [ObjectListIndex(0x161)]
        Raft = 278, // carniverous raft, woodfall

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [ActorListIndex(0x14A)]
        [ObjectListIndex(0x16A)]
        [GroundVariants(0x0C01,0x1402,0xFF03,0xFF01,0xFF00,0x0A01,0x0202,0x801,0xFF02)]
        [EnemizerScenesExcluded(0x49)]
        // termina field, ff00 gbt waterchu, the rest are assumed respawn until proven otherwise
        [RespawningVarients(0xFF03, 0xFF01, 0xFF00,  0x0C01,0x1402,0x0A01,0x0202,0x801,0xFF02)]
        ChuChu = 296,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [ActorListIndex(0x14B)]
        [ObjectListIndex(0x16B)]
        [WaterVariants(0x0F00,0x0300)]
        Desbreko = 297, // dead fish swarm from pirates fortress

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1250)]
        [ActorListIndex(0x155)]
        [ObjectListIndex(0x171)]
        [GroundVariants(0)]
        Nejiron = 307, // Rolling exploding rock in Ikana

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [ActorListIndex(0x15B)]
        [ObjectListIndex(0x172)]
        [FlyingVariants(0xFF34,0xFF02,0xFF03,0xFF9F,0x019F,0x0102,0x0103)]
        [EnemizerScenesExcluded(0x18)]
        BadBat = 313,

        [ActorInitVarOffset(0x37D0)]
        [ActorListIndex(0x15D)]
        [ObjectListIndex(0x178)]
        Wizrobe = 315,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1830)]
        [ActorListIndex(0x164)]
        [ObjectListIndex(0x17A)]
        [FlyingVariants(0x9605, 0x3205, 0x6405, 0x8C05)]
        [GroundVariants(0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0x9605, 0x3205, 0x6405, 0xFF01, 0xFF05, 0xC200, 0xFA00, 0xFA01, 0x8C05)]
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [RespawningVarients(0x6404, 0x7804,   0x9605, 0x3205, 0x6405,    0x8C05)]
        //[EnemizerScenesExcluded(0x1B)] // think they were only excluded because respawn detection was weak
        Bo = 322,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [ActorListIndex(0x16F)]
        [ObjectListIndex(0x181)]
        // the snowhead version that doesnt aggro is 01
        [GroundVariants(0x8C,0x28,0x3C,0x46,0x32,0x1,0x8023,0x5,0x14,0x8028,0x8014)]
        // the values on the left are tested respawning, the values on the right are untested, conservative assume respawning
        [RespawningVarients(0x8014,0x8028,0x8023,   0x0032,0x0005,0x0014)] 
        RealBombchu = 331,

        [ActorListIndex(0x1A8)]
        [ObjectListIndex(0x19E)]
        //[WaterVariants( unk )]
        BigOcto = 383,

        //[EnemizerEnabled]
        [ActorInitVarOffset(0x1C6C)]
        [ActorListIndex(0x180)]
        [ObjectListIndex(0x16B)]
        //[WaterVariants( unk )]
        SkullFish = 346,

        [ActorInitVarOffset(0x445C)]
        [ActorListIndex(0x184)]
        [ObjectListIndex(0x18D)]
        //[GroundVariants( unk ))]
        Eyegore = 250, // walking laser cyclops in inverted stone tower

        [EnemizerEnabled]
        [ActorInitVarOffset(0x21C0)]
        [ActorListIndex(0x1BA)]
        [ObjectListIndex(0x1A6)]
        [GroundVariants(0x0)]
        [EnemizerScenesExcluded(0x1B)]
        Snapper = 406,

        [ActorInitVarOffset(0x1FD0)]
        [ActorListIndex(0x1D1)]
        [ObjectListIndex(0x1B5)]
        Dexihand = 426,  // ???'s water logged brother

        //[EnemizerEnabled] // failure to randomize
        [ActorInitVarOffset(0x2940)] // combat music disable does not work
        [ActorListIndex(0x1DA)]
        [ObjectListIndex(0x75)]
        [GroundVariants( 0x1E9, 0x1F4, 0x208, 0x214, 0x224, 0x236, 0x247, 0x253, 0x262, 0x275, 0x283, 0x291, 0x2A0 )]
        GibdoWell = 435,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [ActorListIndex(0x1E6)]
        [ObjectListIndex(0x1C4)]
        [GroundVariants(0xFF00,0xFF01,00,01)]
        // dont know why it was marked as respawn type in earlier versions none of them seem to respawn
        Eeno = 446,  // Snowmen in showhead

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3794)]
        [ActorListIndex(0x1E9)]
        [ObjectListIndex(0x1C6)]
        [GroundVariants(0,0x0101)]
        Hiploop = 449, // Charging beetle in Woodfall

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [ActorListIndex(0x1F3)]
        [ObjectListIndex(0x1C3)]
        [FlyingVariants(0x00FF)]
        [EnemizerScenesExcluded(0x16,0x18)] // clock tower roof? what?
        Poe = 459,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [ActorListIndex(0x204)]
        [ObjectListIndex(0x1EB)]
        [FlyingVariants(0x0)]
        [GroundVariants(0x0)] // spawns really low to ground in vanilla
        [EnemizerScenesExcluded(0x23)] // pirate beehive cutscene
        GiantBeee = 475,

        //[EnemizerEnabled] // only randomizes amongst itself... bleh
        [ActorInitVarOffset(0x1C50)]
        [ActorListIndex(0x216)]
        [ObjectListIndex(0x201)]
        [GroundVariants(0xFF,0x80FF)]
        [RespawningVarients(0xFF,0x80FF)] // dont know if they are respawning or not, siding on caution
        Leever = 493,

        //[EnemizerEnabled] // failure to randomize
        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [ActorListIndex(0x235)]
        [ObjectListIndex(0x75)]
        [GroundVariants( 0, 0x81, 0x82, 0x83, 0x84, 0x85 )]
        GibdoIkana = 524,

        [ActorInitVarOffset(0x2E30)]
        [ActorListIndex(0x291)]
        [ObjectListIndex(0x22)]
        //[FlyingVariants(0, 1)] // two? one that steals and one that doesn't?
        Takkuri = 616,

        //flying pot is missing
        // 8D 3

        //bombflower
        // 2F 2A

        // player
        // 0 0
    }
}
