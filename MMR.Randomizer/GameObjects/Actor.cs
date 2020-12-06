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
        [ActorVariants(0xFF00)]
        [EnemyType(ActorType.Water)]
        [EnemizerScenesExcluded(0x13,0x49)]
        Octarok = 46,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B30)]
        [ActorListIndex(0xA)]
        [ObjectListIndex(0x9)]
        [ActorVariants(0x01)]
        [EnemyType(ActorType.Ground)]
        WallMaster = 48,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A40)]
        [ActorListIndex(0xB)]
        [ObjectListIndex(0xA)]
        [ActorVariants(0x01,0)]
        [EnemyType(ActorType.Ground)]
        Dodongo = 49,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [ActorListIndex(0xC)]
        [ObjectListIndex(0xB)]
        [ActorVariants(0x8003,0x04,0x0)]
        [EnemyType(ActorType.Flying)]
        Keese = 50,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32C0)]
        [ActorListIndex(0x12)]
        [ObjectListIndex(0x12)]
        [ActorVariants(0xFFFD,0xFFFE,0xFFFF)]
        [EnemyType(ActorType.Ground)]
        Tektite = 55,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [ActorListIndex(0x14)]
        [ObjectListIndex(0x14)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)] // spawns in ground, not air
        [EnemizerScenesExcluded(0x7)]
        Peahat = 56,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [ActorListIndex(0x19)]
        [ObjectListIndex(0x17)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x1B,0x21,0x60,0x66)]
        Dinofos = 58,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [ActorListIndex(0x1D)]
        [ObjectListIndex(0xE)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Flying)]
        [EnemizerScenesExcluded(0x69)]
        Shabom = 62, // the flying bubbles from Jabu Jabu

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [ActorListIndex(0x24)]
        [ObjectListIndex(0x20)]
        [ActorVariants(0x00EF,0x007F,04)]
        [EnemyType(ActorType.Flying)] // used to be respawn type, going to assume air
        [EnemizerScenesExcluded(0x1B, 0x27, 0x28, 0x40)]
        Skulltula = 67,

        //[EnemizerEnabled]  //crash, link to paths?
        [ActorInitVarOffset(0x1CC0)]
        [ActorListIndex(0x2D)]
        [ObjectListIndex(0x1D)]
        [ActorVariants(03,01,02,04,07)]
        [EnemyType(ActorType.Flying)]
        [EnemizerScenesExcluded(0x01)]
        DeathArmos = 71,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)]
        [ActorListIndex(0x32)]
        [ObjectListIndex(0x30)]
        [ActorVariants(0xFFFF)]
        [EnemyType(ActorType.Ground)]
        Armos = 73,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [ActorListIndex(0x33)]
        [ObjectListIndex(0x31)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x1B)]
        DekuBaba = 74,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D30)]
        [ActorListIndex(0x3B)]
        [ObjectListIndex(0x40)]
        [ActorVariants(0xFF02,0xFF00)]
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x46)]
        MadShrub = 81,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1AF0)]
        [ActorListIndex(0x3C)]
        [ObjectListIndex(0x51)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        RedBubble = 82,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [ActorListIndex(0x3E)]
        [ObjectListIndex(0x51)]
        [ActorVariants(0xFFFF)]
        [EnemyType(ActorType.Flying)] //used to be respawn type, but its in the air
        [RespawningVarients(0xFFFF)]
        BlueBubble = 84, // cursed

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [ActorListIndex(0x47)]
        [ObjectListIndex(0x6A)]
        [ActorVariants(0x0600,0x0800,0x0500,0xFF00,0x0300)]
        [EnemyType(ActorType.Ground)]
        Beamos = 89,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3200)]
        [ActorListIndex(0x4A)]
        [ObjectListIndex(0x9)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        FloorMaster = 92,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32A0)]
        [ActorListIndex(0x4C)]
        [ObjectListIndex(0x75)]
        [ActorVariants(0x7F07,0x7F05,0x7F06)]
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x4b,0x13)]
        ReDead = 93,

        [ActorInitVarOffset(0x3080)]
        [ActorListIndex(0x1E7)]
        [ObjectListIndex(0x1)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Flying)] // uh, wall type, but that doesnt exist
        [EnemizerScenesExcluded(0x0)]
        SkullWallTula = 96,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [ActorListIndex(0x64)]
        [ObjectListIndex(0x8E)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Water)]
        Shellblade = 106,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [ActorListIndex(0x66)]
        [ObjectListIndex(0x31)]
        [ActorVariants(01,02)]
        [EnemyType(ActorType.Ground)] // used to be respawn type, but spawns on the ground
        [RespawningVarients(01,02)]
        DekuBabaWithered = 108,

        //[EnemizerEnabled]
        [ActorInitVarOffset(0x2330)]
        [ActorListIndex(0x6C)]
        [ObjectListIndex(0xAB)]
        [ActorVariants(0,02,03)]
        [EnemyType(ActorType.Ground)] // if we had multiple types, this could be water
        LikeLike = 112,

        [EnemizerEnabled]
        [ActorListIndex(0x84)]
        [ObjectListIndex(0xD8)]
        [ActorVariants(0xFF03,0xFF02,0xFF01)]
        [EnemyType(ActorType.Ground)]
        IronKnuckle = 127,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [ActorListIndex(0x8F)]
        [ObjectListIndex(0xE4)]
        [ActorVariants(02,0x2001,0x300F,0x100F)]
        [EnemyType(ActorType.Ground)]
        Freezard = 134,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3E40)]
        [ActorListIndex(0xEC)]
        [ObjectListIndex(0x141)]
        [ActorVariants(0xFF01,0xFF81,0xFF00)]
        [EnemyType(ActorType.Ground)]
        Wolfos = 222,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D60)]
        [ActorListIndex(0xED)]
        [ObjectListIndex(0x142)]
        [ActorVariants(0x0022,0x0032)] // 0x0042 is swinging from tree, looks stupid if spawns in the ground
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x43,0x28)]
        //[RespawningVarients(0x32)] the ones that circle the tombs, but dont respawn if placed anywhere else it seems, ignore
        Stalchild = 223,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [ActorListIndex(0xF1)]
        [ObjectListIndex(0x6)]
        [ActorVariants(0)]
        [EnemyType(ActorType.Flying)] // used to be respawn type, but spawns in the air
        [RespawningVarients(0)]
        Guay = 226,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [ActorListIndex(0x109)]
        [ObjectListIndex(0x14E)]
        [ActorVariants(0,02,03)]
        [EnemyType(ActorType.Flying)]
        DragonFly = 241,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3688)]
        [ActorListIndex(0x113)]
        [ObjectListIndex(0x155)]
        [EnemyType(ActorType.Ground)]
        Garo = 248,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3760)]
        [ActorListIndex(0x12D)]
        [ObjectListIndex(0x15E)]
        [ActorVariants(04,02,01,0)]
        [EnemyType(ActorType.Water)]
        [EnemizerScenesExcluded(0x49)]
        BioDekuBaba = 271,

        [ActorListIndex(0x13A)]
        [ObjectListIndex(0x161)]
        [EnemyType(ActorType.Water)]
        Raft = 278, // carniverous raft, woodfall

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [ActorListIndex(0x14A)]
        [ObjectListIndex(0x16A)]
        [ActorVariants(0x0C01,0x1402,0xFF03,0xFF01,0xFF00,0x0A01,0x0202,0x801,0xFF02)]
        [EnemyType(ActorType.Ground)] // used to be respawning type, but spawns on the ground
        [EnemizerScenesExcluded(0x49)]
        // termina field, ff00 gbt waterchu, 
        [RespawningVarients(0xFF03, 0xFF01, 0xFF00,  0x0C01,0x1402,0x0A01,0x0202,0x801,0xFF02)]
        ChuChu = 296,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [ActorListIndex(0x14B)]
        [ObjectListIndex(0x16B)]
        [ActorVariants(0x0F00,0x0300)]
        [EnemyType(ActorType.Water)]
        Desbreko = 297, // dead fish swarm from pirates fortress

        [ActorInitVarOffset(0x1250)]
        [ActorListIndex(0x155)]
        [ObjectListIndex(0x171)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        Nejiron = 307, // Rolling exploding rock in Ikana

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [ActorListIndex(0x15B)]
        [ObjectListIndex(0x172)]
        [ActorVariants(0xFF34,0xFF02,0xFF03,0xFF9F,0x019F,0x0102,0x0103)]
        [EnemyType(ActorType.Flying)]
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
        [ActorVariants(0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0x9605, 0x3205, 0x6405, 0xFF01, 0xFF05, 0xC200, 0xFA00, 0xFA01, 0x8C05)]
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [RespawningVarients(0x6404, 0x7804,   0x9605, 0x3205, 0x6405,    0x8C05)]
        [EnemyType(ActorType.Ground)]// used to be respawning type, but can spawn in the air and on the ground
        [EnemizerScenesExcluded(0x1B)]
        Bo = 322,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [ActorListIndex(0x16F)]
        [ObjectListIndex(0x181)]
        [ActorVariants(0x008C, 0x0028, 0x003C, 0x0046, 0x0032, 0x0001, 0x8023, 0x0005, 0x0014, 0x8028, 0x8014)]
        [EnemyType(ActorType.Ground)]
        RealBombchu = 331,

        // big octo goes here

        //[EnemizerEnabled]
        [ActorInitVarOffset(0x1C6C)]
        [ActorListIndex(0x180)]
        [ObjectListIndex(0x16B)]
        [EnemyType(ActorType.Water)]
        SkullFish = 346,

        [ActorInitVarOffset(0x445C)]
        [ActorListIndex(0x184)]
        [ObjectListIndex(0x18D)]
        [EnemyType(ActorType.Ground)]
        Eyegore = 250, // walking laser cyclops in inverted stone tower

        [EnemizerEnabled]
        [ActorInitVarOffset(0x21C0)]
        [ActorListIndex(0x1BA)]
        [ObjectListIndex(0x1A6)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Ground)]
        [EnemizerScenesExcluded(0x1B)]
        Snapper = 406,

        [ActorInitVarOffset(0x1FD0)]
        [ActorListIndex(0x1D1)]
        [ObjectListIndex(0x1B5)]
        [EnemyType(ActorType.Water)]
        Dexihand = 426,  // ???'s water logged brother

        [ActorInitVarOffset(0x2940)] // combat music disable does not work
        [ActorListIndex(0x1DA)]
        [ObjectListIndex(0x75)]
        [EnemyType(ActorType.Ground)]
        GibdoWell = 435,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [ActorListIndex(0x1E6)]
        [ObjectListIndex(0x1C4)]
        [ActorVariants(0xFF00,0xFF01,00,01)]
        [EnemyType(ActorType.Ground)] // used to be classified as respawn, which... is wrong? maybe one variety respawns
        Eeno = 446,  // Snowmen in showhead

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3794)]
        [ActorListIndex(0x1E9)]
        [ObjectListIndex(0x1C6)]
        [ActorVariants(0,0x0101)]
        [EnemyType(ActorType.Ground)]
        Hiploop = 449, // Charging beetle in Woodfall

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [ActorListIndex(0x1F3)]
        [ObjectListIndex(0x1C3)]
        [ActorVariants(0x00FF)]
        [EnemyType(ActorType.Flying)]
        [EnemizerScenesExcluded(0x16,0x18)]
        Poe = 459,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [ActorListIndex(0x204)]
        [ObjectListIndex(0x1EB)]
        [ActorVariants(0x0)]
        [EnemyType(ActorType.Flying)]
        [EnemizerScenesExcluded(0x23)]
        GiantBeee = 475,

        //[EnemizerEnabled]
        [ActorInitVarOffset(0x1C50)]
        [ActorListIndex(0x216)]
        [ObjectListIndex(0x201)]
        [EnemyType(ActorType.Ground)]
        Leever = 493,

        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [ActorListIndex(0x235)]
        [ObjectListIndex(0x75)]
        [EnemyType(ActorType.Ground)]
        GibdoIkana = 524,

        [ActorInitVarOffset(0x2E30)]
        [ActorListIndex(0x291)]
        [ObjectListIndex(0x22)]
        [EnemyType(ActorType.Flying)]
        Takkuri = 616, // the birds at the bird house like to steal shiney things

        //flying pot is missing
        // 8D 3

        //bombflower
        // 2F 2A

        // player
        // 0 0
    }
}
