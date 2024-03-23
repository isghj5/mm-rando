using MMR.Randomizer.Attributes.Enemy;

namespace MMR.Randomizer.GameObjects
{
    public enum Enemy
    {
        [ActorId(0x0008)]
        [ObjectId(0x0005)]
        [Variable(0xff00)]
        [ActorType(ActorTypeAttribute.ActorType.Water)]
        [ForbidFromScene(Scene.IkanaCanyon)] // needed to freeze
        [ForbidFromScene(Scene.GreatBayTemple)] // needed to freeze
        Octorok,

        [ActorId(0x000a)]
        [ObjectId(0x0009)]
        [Variable(0x0001)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Wallmaster,

        [ActorId(0x000b)]
        [ObjectId(0x000a)]
        [Variable(0x0001), Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Dodongo,

        [ActorId(0x000c)]
        [ObjectId(0x000b)]
        [Variable(0x8003), Variable(0x0004), Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        Keese,

        [ActorId(0x0012)]
        [ObjectId(0x0012)]
        [Variable(0xfffd), Variable(0xfffe), Variable(0xffff)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Tektite,

        [ActorId(0x0014)]
        [ObjectId(0x0014)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        [ForbidFromScene(Scene.Grottos)] // ?
        Peahat,

        [ActorId(0x0019)]
        [ObjectId(0x0017)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        [ForbidFromScene(Scene.WoodfallTemple)] // spawns too high
        [ForbidFromScene(Scene.SnowheadTemple)] // ?
        [ForbidFromScene(Scene.SecretShrine)] // spawns too high
        [ForbidFromScene(Scene.LinkTrial)] // spawns too high
        [ForbidToScene(Scene.BeneathGraveyard)] // Dinolfos can crash in graveyard
        Dinolfos,

        [ActorId(0x001d)]
        [ObjectId(0x000e)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        [ForbidFromScene(Scene.GiantsChamber)] // scene effect
        Shabom,

        [ActorId(0x0024)]
        [ObjectId(0x0020)]
        [Variable(0x003f), Variable(0x007f)]
        [ActorType(ActorTypeAttribute.ActorType.Other)]
        [ForbidFromScene(Scene.WoodfallTemple)] // ? because of the stray fairy?
        [ForbidFromScene(Scene.SwampSpiderHouse)] // Golden Skulltulas use the same object
        [ForbidFromScene(Scene.OceanSpiderHouse)] // Golden Skulltulas use the same object
        [ForbidFromScene(Scene.GreatBayTemple)] // ? because of the stray fairy?
        Skulltula,

        //[ActorId(0x002d)]
        //[ObjectId(0x001d)]
        //[Variable(0x0003, 0x0001, 0x0002, 0x0004, 0x0007)]
        //[ActorType(ActorTypeAttribute.ActorType.Air)]
        //Deatharmos, //  - crash, link to paths?,

        [ActorId(0x0032)]
        [ObjectId(0x0030)]
        [Variable(0xffff)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        Armos,

        [ActorId(0x0033)]
        [ObjectId(0x0031)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [ForbidFromScene(Scene.WoodfallTemple)] // ? because of the stray fairy?
        DekuBaba,

        [ActorId(0x003b)]
        [ObjectId(0x0040)]
        [Variable(0xff02), Variable(0xff00)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [ForbidFromScene(Scene.Woodfall)] // needed to spawn deku flowers to cross the area
        MadScrub,

        [ActorId(0x003c)]
        [ObjectId(0x0051)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        RedBubble,

        [ActorId(0x003e)]
        [ObjectId(0x0051)]
        [Variable(0xffff)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        BlueBubble,

        [ActorId(0x0047)]
        [ObjectId(0x006a)]
        [Variable(0x0600), Variable(0x0800), Variable(0x0500), Variable(0xff00), Variable(0x0300)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        Beamos,

        [ActorId(0x004a)]
        [ObjectId(0x0009)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Floormaster,

        [ActorId(0x004c)]
        [ObjectId(0x0075)]
        [Variable(0x7f07), Variable(0x7f05), Variable(0x7f06)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [ForbidFromScene(Scene.BeneathTheWell)] // Gibdos use the same object
        [ForbidFromScene(Scene.IkanaCanyon)] // Gibdos use the same object
        Redead,

        //[ActorId(0x0050)]
        //[ObjectId(0x0020)]
        //[Variable(0x0000)]
        //[ActorType(ActorTypeAttribute.ActorType.Other)]
        //Skullwalltula,

        [ActorId(0x0064)]
        [ObjectId(0x008e)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Water)]
        [IsMoving]
        Shellblade,

        [ActorId(0x0066)]
        [ObjectId(0x0031)]
        [Variable(0x0002), Variable(0x0001)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        MiniBaba,

        //[ActorId(0x006c)]
        //[ObjectId(0x00ab)]
        //[Variable(0x0003, 0x0002, 0x0000)]
        //[ActorType(ActorTypeAttribute.ActorType.Ground)]
        //LikeLike,

        [ActorId(0x0084)]
        [ObjectId(0x00d8)]
        [Variable(0xff02), Variable(0xff03), Variable(0xff01)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        IronKnuckle,

        [ActorId(0x008f)]
        [ObjectId(0x00e4)]
        [Variable(0x0002), Variable(0x2001), Variable(0x300f), Variable(0x100f)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        Freezard,

        [ActorId(0x00ec)]
        [ObjectId(0x0141)]
        [Variable(0xff01), Variable(0xff81), Variable(0xff00)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Wolfos,

        [ActorId(0x00ed)]
        [ObjectId(0x0142)]
        [Variable(0x0042), Variable(0x0022), Variable(0x0032)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        [ForbidFromScene(Scene.IkanaGraveyard)] // needed to open graves
        [ForbidFromScene(Scene.OceanSpiderHouse)] // needed to get the mask code
        Stalchild,

        [ActorId(0x00f1)]
        [ObjectId(0x0006)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        Guay,

        [ActorId(0x0109)]
        [ObjectId(0x014e)]
        [Variable(0x0000), Variable(0x0002), Variable(0x0003)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        Dragonfly,

        [ActorId(0x012d)]
        [ObjectId(0x015e)]
        [Variable(0x0002), Variable(0x0000), Variable(0x0004), Variable(0x0001)]
        [ActorType(ActorTypeAttribute.ActorType.Water)]
        [ForbidFromScene(Scene.GreatBayTemple)] // needed to get to the compass chest?
        BioBaba,

        [ActorId(0x014a)]
        [ObjectId(0x016a)]
        [Variable(0x0c01), Variable(0x1402), Variable(0xff03), Variable(0xff01), Variable(0xff00), Variable(0x0a01), Variable(0x0202), Variable(0x0801), Variable(0xff02)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        [ForbidFromScene(Scene.GreatBayTemple)] // needed to freeze
        [ForbidToScene(Scene.SouthernSwampClear)]
        Chuchu,

        [ActorId(0x014b)]
        [ObjectId(0x016b)]
        [Variable(0x0f00), Variable(0x0300)]
        [ActorType(ActorTypeAttribute.ActorType.Water)]
        [IsMoving]
        Desbreko,

        [ActorId(0x0155)]
        [ObjectId(0x0171)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Nejiron,

        [ActorId(0x015b)]
        [ObjectId(0x0172)]
        [Variable(0x0134), Variable(0x0101), Variable(0x019f), Variable(0x0102), Variable(0x0103)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        BadBat,

        [ActorId(0x0164)]
        [ObjectId(0x017a)]
        [Variable(0xff00), Variable( 0x6404), Variable( 0x7804), Variable( 0x7800), Variable( 0x2800), Variable( 0x3200), Variable( 0x9605), Variable( 0x3205), Variable( 0x6405), Variable( 0xff01), Variable( 0xff05), Variable( 0xc200), Variable( 0xfa00), Variable( 0xfa01), Variable( 0x8c05)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        [ForbidFromScene(Scene.WoodfallTemple)] // Boes in woodfall temple shouldn't respawn because of the dark room fairy
        Boe,

        [ActorId(0x016f)]
        [ObjectId(0x0181)]
        [Variable(0x008c), Variable( 0x0028), Variable( 0x003c), Variable( 0x0046), Variable( 0x0032), Variable( 0x0001), Variable( 0x8023), Variable( 0x0005), Variable( 0x0014), Variable( 0x8028), Variable( 0x8014)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        RealBombchu,

        //[ActorId(0x01a8)]
        //[ObjectId(0x019e)]
        //[Variable(0x007f)]
        //[ActorType(ActorTypeAttribute.ActorType.Water)]
        //[SceneExclude(Scene.SouthernSwampClear, Scene.SouthernSwamp)]
        //BigOcto,

        [ActorId(0x01ba)]
        [ObjectId(0x01a6)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        [ForbidFromScene(Scene.WoodfallTemple)] // miniboss uses the same object?
        Snapper,

        [ActorId(0x01e6)]
        [ObjectId(0x01c4)]
        [Variable(0xff00), Variable( 0xff01), Variable( 0x0000), Variable( 0x0001)]
        [ActorType(ActorTypeAttribute.ActorType.Respawn)]
        [IsMoving]
        Eeno,

        [ActorId(0x01e9)]
        [ObjectId(0x01c6)]
        [Variable(0x0000), Variable( 0x0101)]
        [ActorType(ActorTypeAttribute.ActorType.Ground)]
        [IsMoving]
        Hiploop,

        [ActorId(0x01f3)]
        [ObjectId(0x01c3)]
        [Variable(0x00ff)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        [ForbidFromScene(Scene.StoneTowerTemple)] // ?
        [ForbidFromScene(Scene.InvertedStoneTowerTemple)] // Only place to get a poe
        Poe,

        [ActorId(0x0204)]
        [ObjectId(0x01eb)]
        [Variable(0x0000)]
        [ActorType(ActorTypeAttribute.ActorType.Air)]
        [IsMoving]
        [ForbidFromScene(Scene.PiratesFortressRooms)] // Needed to chase pirates away?
        Hornet,
    }
}
