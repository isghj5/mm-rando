using MMR.Randomizer.Attributes.Actor;

namespace MMR.Randomizer.GameObjects
{
    public enum Actor
    {
        // the main enumator value is the MMFileList index
        // todo finish adding ENEMIES.txt content here so we can rid of it

        [ActorInitVarOffset(0x2A60)]
        [ActorListIndex(0x8)]
        [ObjectListIndex(0x5)]
        Octarok = 46,

        [ActorInitVarOffset(0x1B30)]
        [ActorListIndex(0xA)]
        [ObjectListIndex(0x9)]
        WallMaster = 48,

        [ActorInitVarOffset(0x2A40)]
        [ActorListIndex(0xB)]
        [ObjectListIndex(0xA)]
        Dodongo = 49, 

        [ActorInitVarOffset(0x1D60)]
        [ActorListIndex(0xC)]
        [ObjectListIndex(0xB)]
        Keese = 50,

        [ActorInitVarOffset(0x32C0)]
        [ActorListIndex(0x12)]
        [ObjectListIndex(0x12)]
        Tektite = 55,

        [ActorInitVarOffset(0x24E0)]
        [ActorListIndex(0x14)]
        [ObjectListIndex(0x14)]
        Peahat = 56,

        [ActorInitVarOffset(0x3A70)]
        [ActorListIndex(0x19)]
        [ObjectListIndex(0x17)]
        Dinofos = 58,

        [ActorInitVarOffset(0x5C8)]
        [ActorListIndex(0x1D)]
        [ObjectListIndex(0xE)]
        Shabom = 62, // the flying bubbles from Jabu Jabu

        [ActorInitVarOffset(0x2540)]
        [ActorListIndex(0x24)]
        [ObjectListIndex(0x20)]
        Skulltula = 67,

        [ActorInitVarOffset(0x1CC0)]
        [ActorListIndex(0x2D)]
        [ObjectListIndex(0x1D)]
        DeathArmos = 71,

        [ActorInitVarOffset(0x1380)]
        [ActorListIndex(0x30)]
        [ObjectListIndex(0x32)]
        Armos = 73,

        [ActorInitVarOffset(0x3A10)]
        [ActorListIndex(0x33)]
        [ObjectListIndex(0x31)]
        DekuBaba = 74,

        [ActorInitVarOffset(0x1D30)]
        [ActorListIndex(0x3B)]
        [ObjectListIndex(0x40)]
        MadShrub = 81,

        [ActorInitVarOffset(0x1AF0)]
        [ActorListIndex(0x3C)]
        [ObjectListIndex(0x51)]
        RedBubble = 82,

        [ActorInitVarOffset(0x1A40)]
        [ActorListIndex(0x3E)]
        [ObjectListIndex(0x51)]
        BlueBubble = 84,

        [ActorInitVarOffset(0x1240)]
        [ActorListIndex(0x47)]
        [ObjectListIndex(0x6A)]
        Beamos = 89,

        [ActorInitVarOffset(0x3200)]
        [ActorListIndex(0x4A)]
        [ObjectListIndex(0x9)]
        FloorMaster = 92,

        [ActorInitVarOffset(0x32A0)]
        [ActorListIndex(0x4C)]
        [ObjectListIndex(0x75)]
        ReDead = 93,

        [ActorInitVarOffset(0x3080)]
        [ActorListIndex(0x1E7)]
        [ObjectListIndex(0x1)]
        SkullWallTula = 96,

        [ActorInitVarOffset(0xF00)]
        [ActorListIndex(0x64)]
        [ObjectListIndex(0x8E)]
        Shellblade = 106,

        [ActorInitVarOffset(0x1B80)]
        [ActorListIndex(0x66)]
        [ObjectListIndex(0x31)]
        DekuBabaWithered = 108,

        [ActorInitVarOffset(0x2330)]
        [ActorListIndex(0x6C)]
        [ObjectListIndex(0xAB)]
        LikeLike = 112,

        [ActorListIndex(0x84)]
        [ObjectListIndex(0xD8)]
        IronKnuckle = 127,

        [ActorInitVarOffset(0x2240)]
        [ActorListIndex(0x8F)]
        [ObjectListIndex(0xE4)]
        Freezard = 134,

        [ActorInitVarOffset(0x3E40)]
        [ActorListIndex(0xEC)]
        [ObjectListIndex(0x141)]
        Wolfos = 222,

        [ActorInitVarOffset(0x2D60)]
        [ActorListIndex(0xED)]
        [ObjectListIndex(0x142)]
        Stalchild = 223,

        [ActorInitVarOffset(0x1520)]
        [ActorListIndex(0xF1)]
        [ObjectListIndex(0x6)]
        Guay = 226,

        [ActorInitVarOffset(0x2A7C)]
        [ActorListIndex(0x109)]
        [ObjectListIndex(0x14E)]
        DragonFly = 241,

        [ActorInitVarOffset(0x3688)]
        [ActorListIndex(0x113)]
        [ObjectListIndex(0x155)]
        Garo = 248,

        [ActorInitVarOffset(0x3760)]
        [ActorListIndex(0x13A)]
        [ObjectListIndex(0x161)]
        BioDekuBaba = 271, // carniverous raft

        [ActorInitVarOffset(0x2D30)]
        [ActorListIndex(0x14A)]
        [ObjectListIndex(0x16A)]
        ChuChu = 296,

        [ActorInitVarOffset(0x16C4)]
        [ActorListIndex(0x155)]
        [ObjectListIndex(0x16B)]
        Desbreko = 297, // dead fish swarm from pirates fortress

        [ActorInitVarOffset(0x1250)]
        [ActorListIndex(0x155)]
        [ObjectListIndex(0x171)]
        Nejiron = 307, // Rolling exploding rock in Ikana

        [ActorInitVarOffset(0x1500)]
        [ActorListIndex(0x15B)]
        [ObjectListIndex(0x172)]
        BadBat = 313,

        [ActorInitVarOffset(0x37D0)]
        [ActorListIndex(0x15D)]
        [ObjectListIndex(0x178)]
        Wizrobe = 315,

        [ActorInitVarOffset(0x1830)]
        [ActorListIndex(0x164)]
        [ObjectListIndex(0x17A)]
        Bo = 322,

        [ActorInitVarOffset(0x2290)]
        [ActorListIndex(0x16F)]
        [ObjectListIndex(0x181)]
        RealBombchu = 331,

        [ActorInitVarOffset(0x1C6C)]
        [ActorListIndex(0x180)]
        [ObjectListIndex(0x16B)]
        SkullFish = 346,

        [ActorInitVarOffset(0x445C)]
        [ActorListIndex(0x184)]
        [ObjectListIndex(0x18D)]
        Eyegore = 250,

        [ActorInitVarOffset(0x21C0)]
        [ActorListIndex(0x1BA)]
        [ObjectListIndex(0x1A6)]
        Snapper = 406,

        [ActorInitVarOffset(0x1FD0)]
        [ActorListIndex(0x1D1)]
        [ObjectListIndex(0x1B5)]
        Dexihand = 426,

        [ActorInitVarOffset(0x2940)] // combat music disable does not work
        [ActorListIndex(0x1DA)]
        [ObjectListIndex(0x75)]
        GibdoWell = 435,

        [ActorInitVarOffset(0x2EE0)]
        [ActorListIndex(0x1E6)]
        [ObjectListIndex(0x1C4)]
        Eeno = 446,  // Snowmen in showhead

        [ActorInitVarOffset(0x3794)]
        [ActorListIndex(0x1E9)]
        [ObjectListIndex(0x1C6)]
        Hiploop = 449, // Charging beetle in Woodfall

        [ActorInitVarOffset(0x2F70)]
        [ActorListIndex(0x1F3)]
        [ObjectListIndex(0x1C3)]
        Poe = 459,

        [ActorInitVarOffset(0xAD4)]
        [ActorListIndex(0x204)]
        [ObjectListIndex(0x1EB)]
        GiantBeee = 475,

        [ActorInitVarOffset(0x1C50)]
        [ActorListIndex(0x216)]
        [ObjectListIndex(0x201)]
        Leever = 493,

        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [ActorListIndex(0x235)]
        [ObjectListIndex(0x75)]
        GibdoIkana = 524,

        [ActorInitVarOffset(0x2E30)]
        [ActorListIndex(0x291)]
        [ObjectListIndex(0x22)]
        Takkuri = 616,

        //flying pot is missing
        // 8D 3

        //bombflower
        // 2F 2A

        // player
        // 0 0
    }
}
