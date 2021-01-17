using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.GameObjects
{
    public enum Actor
    {
        // the main enumator value is the actor list ID

        [EnemizerEnabled]
        [ObjectListIndex(0x51)] // gameplay_keep obj 1
        // 0x83F0 is tiny candle light
        //[GroundVariants(0x83F0, 0x27F5)] // TODO finish checking the rest of possible variations
        [GroundVariants(0x7F4)] // 0x7F4 is the bright yellow light of the graveyard smash
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Woodfall)] //woodfall
        Flame = 0x4, //En_Light

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A60)]
        [FileID(46)]
        [ObjectListIndex(0x5)]
        [WaterVariants(0xFF00)]
        [EnemizerScenesExcluded(Scene.IkanaCanyon, Scene.GreatBayTemple)]
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
        [GroundVariants(1, 0)]
        [DoublePerRoomMax(1)] // pretty sure 1 is the larger one
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // too big, can block the butler race
        Dodongo = 0xB,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [FileID(50)]
        [ObjectListIndex(0xB)]
        [FlyingVariants(0x8003,0x04,0)] // which ones are fire and ice?
        Keese = 0xC,

        [ActorizerEnabled]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1, place beside butler son
        // 4 is group of fairies out of a fountain, 7 is large healing fairy, 9 is yellow fairy
        [GroundVariants(4, 7, 9)]
        [FlyingVariants(4, 7, 9)]
        [SinglePerRoomMax(4)]
        [DoublePerRoomMax(7)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.BeneathTheWell)] // dont remove from well
        Fairy = 0x10,

        [ActorizerEnabled]
        [FileID(54)]
        [ObjectListIndex(0xF)]
        [GroundVariants(0xFFFF)]
        [UnkillableAllVariants]
        // I would like a flying variant, but they seem to drop like a rock instead of float down
        //[EnemizerScenesExcluded(0x15, Scene.AstralObservatory, 0x35, 0x42, 0x10)]
        [EnemizerScenesExcluded(Scene.AstralObservatory, Scene.RomaniRanch, Scene.CuccoShack, Scene.MilkBar)]
        FriendlyCucco = 0x11,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32C0)]
        [FileID(55)]
        [ObjectListIndex(0x12)]
        [GroundVariants(0xFFFD, 0xFFFE, 0xFFFF)] // FF does not exist in MM vanilla, red variety
        [WaterVariants(0xFFFE)] 
        Tektite = 0x12,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [FileID(56)]
        [ObjectListIndex(0x14)]
        // yo wait how did we get the flying variety once? was that spawned in air or was that a variety that was missing?
        [GroundVariants(0)]
        [FlyingVariants(0)] // there's space in that hall for flying enemiess, what happens if we spawn in the air? does it sink?
        //[DoublePerRoomMax(0)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // too big, can block the butler race
        Peahat = 0x14,

        [ActorizerEnabled]
        [ObjectListIndex(0x16B)] // gameplay keep obj 1
        [WaterVariants(2,0)] // 2 is the lab fish
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.MarineLab)]
        Fish = 0x17,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [FileID(58)]
        [ObjectListIndex(0x17)]
        [GroundVariants(0)]
        [DoublePerRoomMax(0)]
        [EnemizerScenesExcluded(Scene.SecretShrine)] // issue: spawn is too high, needs to be lowered
        [EnemizerScenesPlacementBlock(Scene.BeneathGraveyard, Scene.DekuShrine)] // crash in graveyard
        Dinofos = 0x19,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [FileID(62)]
        [ObjectListIndex(0xE)]
        [FlyingVariants(0)] // 0 works, but OOT used FFFF
        [GroundVariants(0)] // 0 works, but OOT used FFFF
        [WaterVariants(0)] // 0 works, but OOT used FFFF
        //[EnemizerScenesExcluded(0x69)]
        Shabom = 0x1D,// the flying bubbles from Jabu Jabu, exist only in giants cutscenes

        
        [ActorizerEnabled]
        [FileID(65)]
        [ObjectListIndex(1)] // gameplay_keep obj 1
        [GroundVariants(0)] // 0 is ben
        [UnkillableAllVariants]
        // Ben seems to be cursed, if you enter a scene with him from a grotto it can crash (~90% chance?) 
        // but entering those same scenes from horizontal loading zones is fine
        [EnemizerScenesPlacementBlock(Scene.TerminaField,
            Scene.WoodsOfMystery, Scene.RoadToSouthernSwamp, Scene.SouthernSwamp, Scene.SouthernSwampClear,
            Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.MountainVillageSpring, Scene.PathToSnowhead,
            Scene.GreatBayCoast, Scene.ZoraCape,
            Scene.RoadToIkana, Scene.IkanaGraveyard, Scene.IkanaCanyon)]
        Ben = 0x21,

        [ActorizerEnabled]
        [ObjectListIndex(0xBC)]
        // the frogs that show up when you kill hte other frog, those are unknown vars because they are spawned by the dead miniboss
        [GroundVariants(1,2,3,4)] // 3 is southern swamp, 4 is laundry pool, the versions in teh mountaion have the F flag, think the rest are numbered
        [UnkillableAllVariants]
        [SinglePerRoomMax(1,2,3,4)]
        [EnemizerScenesExcluded(Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.LaundryPool)] // clear and poison swamp, laundrypool
        Frog1 = 0x22,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [FileID(67)]
        [ObjectListIndex(0x20)]
        [FlyingVariants(0xEF,0x7F,4,0x3F)]
        //[GroundVariants(0xEF,0x7F,4)] // remember, this works for _spawns_
        //[EnemizerScenesExcluded(0x27, 0x28)] // huh? what are these? why were they not randomized in road to ss?
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.Snowhead, 
            Scene.MountainVillageSpring, Scene.TwinIslandsSpring)] // not a problem, just weird seeing them fly like that
        Skulltula = 0x24,

        //[EnemizerEnabled] // sometimes crash, cause unknown
        [ActorInitVarOffset(0x1CC0)]
        [FileID(71)]
        [ObjectListIndex(0x1D)]
        // 2 worked in snowhead, seems to fly in a straight line though, pathing?
        [PatrolVariants(2)] // 3 works, 1+4 crashes, assuming 7 also crashes because probably a flag
        [SinglePerRoomMax(2)]
        [UnkillableVariants(2)] // they do NOT respawn, this is temporary: light arrow req makes them difficult to kill early in the game
        //[EnemizerScenesExcluded(0x18, 0x16)] // 0x18 is ISTT
        [EnemizerScenesExcluded(Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        // scenes that seem fine: path to snowhead, grottos, well, road to southern swampm ikana canyon, spring twin islands
        // graveyard doesnt crash, but he doesn't spawn here either? its just an empty sky
        [EnemizerScenesPlacementBlock(Scene.AstralObservatory, Scene.RoadToIkana, 
            Scene.Woodfall, Scene.PathToMountainVillage, Scene.IkanaCastle,
            Scene.MountainVillageSpring, Scene.BeneathGraveyard, Scene.DekuShrine,
            Scene.IkanaGraveyard )] // known crash locations
        DeathArmos = 0x2D,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [FileID(72)]
        [ObjectListIndex(0x2A)]
        [GroundVariants(0xFFFF)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.GoronRacetrack, Scene.OdolwasLair)]
        BombFlower = 0x2F,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)]
        [FileID(73)]
        [ObjectListIndex(0x30)]
        [GroundVariants(0xFFFF)]
        //[UnkillableVariants(0xFFFF)] // does not respawn, but they do not drop fairies when killed, so marked here
        Armos = 0x32,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [FileID(74)]
        [ObjectListIndex(0x31)]
        [GroundVariants(0)]
        //[EnemizerScenesExcluded(0x1B)] // asside from armos not dropping a fairy, this seems safe now
        DekuBaba = 0x33,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D30)]
        [FileID(81)]
        [ObjectListIndex(0x40)]
        [GroundVariants(0xFF02, 0xFF00, 0xFF01)]
        [UnkillableVariants(0xFF01)]
        [EnemizerScenesExcluded(Scene.Woodfall, Scene.DekuPalace)]
        MadShrub = 0x3B,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1AF0)]
        [FileID(82)]
        [ObjectListIndex(0x51)]
        [GroundVariants(0)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // visible waiting below the bridges
        RedBubble = 0x3C,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [FileID(84)]
        [ObjectListIndex(0x51)]
        [FlyingVariants(0xFFFF)]
        [UnkillableAllVariants] // rspawning
        BlueBubble = 0x3E,// cursed

        [EnemizerEnabled] //hardcoded values for his entrance spawn make the camera wonky
        [ObjectListIndex(0x52)]
        [GroundVariants(0)] // can fly, but weirdly is very bad at changing height if you fight in a multi-level area
        [SinglePerRoomMax(0)] // only fight her if you fight only one
        [UnkillableAllVariants] // is NOT unkillable, but assume never have light arrows until the last second of a run, do not place where can block an item
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple)] // lets not randomize his normal spawn
        // good candidate for night and dungeon spawn only
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.RoadToIkana,
            Scene.SouthernSwamp, Scene.WoodsOfMystery, Scene.Woodfall, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.PathToSnowhead, 
            Scene.Snowhead, Scene.GoronVillage, Scene.DekuShrine)]
        Gomess = 0x43,

        [EnemizerEnabled]
        //[ItemsReqRemove(Item.ItemBombBag)]
        [ActorInitVarOffset(0x1240)]
        [FileID(89)]
        [ObjectListIndex(0x6A)]
        //[GroundVariants(0x600, 0x800, 0x500, 0xFF00, 0x300)] // all working varieties
        [GroundVariants(0x600)] // reduce for lag
        [UnkillableAllVariants] // not unkillable, but for now, stops them from showing up blocking clear to get checks, and fairies
        [DoublePerRoomMax(0x600, 0x800, 0x500, 0xFF00, 0x300)]
        Beamos = 0x47,

        [ActorizerEnabled]
        [ObjectListIndex(0x1)]// gameplay_keep obj 1
        [FlyingVariants(0)] // there are two other vars untested
        [GroundVariants(0)] // there are two other vars untested
        [UnkillableAllVariants]
        [SinglePerRoomMax(0)]
        // variety 0 crashes scenes with snow weather, but not rain, weird
        [EnemizerScenesPlacementBlock(Scene.Snowhead, Scene.TwinIslands, Scene.MountainVillage, Scene.GoronVillage)]
        Demo_Kankyo = 0x49, // lost woods living fairy dust

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
        [EnemizerScenesExcluded(Scene.IkanaCanyon, Scene.BeneathTheWell)] // gibdo locations, but share the same object so gets detected
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows us down too much
        ReDead = 0x4C,

        [EnemizerEnabled]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        [GroundVariants(0x3323, 0x2324, 0x4324)] // bettles on the floor
        [FlyingVariants(0x2324, 0x4324)] // butterlies in the air
        [WaterVariants(0x6322)] // fish swimming in the water
        [UnkillableAllVariants]
        [DoublePerRoomMax(0x3323, 0x2324, 0x4324)]
        Bugs = 0x4F,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3080)]
        [FileID(96)]
        [ObjectListIndex(0x20)] // uh, this might not be a valid object either
        // swamphouse: 0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F
        // ocean spiderhouse: 0xFF3F, 0x1B, 0x317, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0x777, 0x57B, 0xFF0B, 0xFF0F, 0x223, 0x11F,
        // some spiders crash, probably because it follows a path, 223, 113, 1B, 909
        // regular wall skulltula that attacks you: FFFC
        // issue: cannot know what the vars for the ones in the pots/hives/trees/bonk are, dirt is weirdly visible in scenetatl
        // known fine: FF53, FF3B, FF76
        [WallVariants(0xFF53, 0xFF07, 0xFF56, 0xFF62, 0xFF76, 0xFF03,
            0xFF3F, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B, 0xFF0F,
            0xFFFC)]
        [SinglePerRoomMax(0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F,
            0xFF3F, 0x317, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0x777, 0x57B, 0xFF0B, 0xFF0F, 0x11F)]
        [EnemizerScenesExcluded(Scene.SwampSpiderHouse, Scene.OceanSpiderHouse)] // dont remove old spiders, the new ones might not be gettable
        GoldSkullTula = 0x50, // for now, only randomize the gold skulltulas

        [ActorizerEnabled]
        [ObjectListIndex(2)] // overworld_keep, obj 2
        // 3D is swamp grotto, 304 is deku playground, 5C is mystery woods
        // FF/299 is HSG, 233 is path to snowhead, 3B is mountain village spring grot
        // 96 is goron rock grotto, 218/2B8? is graveyard grotto, 3E is road to swamp
        // 301 is ranch grotto? 
        // 214 is log cow grotto
        // okay these dont work as static behavior, but lets see what happens if we just add some randomly, what happens?
        // 2B8 is hidden bombable, leads to spring gossip, 201 was hidden and spring
        // 5C 96, 01 are visible, lead to spring
        // 301 takes me to a different nearby scene, 304 takes me to a differnt one
        // okay, anything below 100 seems to take us to spring, 0x200 flag is hidden, 0x300 is related travel
        // 310 crashed though in one instance, probably too high of a 300 number
        // x000 is the grotto index, except 1000 breaks for some reason, while 0,2,3,4 work for the gossip grottos
        // 7000 is hot spring water grotto, 6k is A regular chest grotto
        // dont think the 8k bit is used, it wasn't in OOT
        // but seriously thats still a small number of grottos
        // zoey says the lower byte used to be for chest contents and things but we dont use in rando
        // TODO rewrite grotto to take a bigger list of entrances
        [GroundVariants(0, 0x7000)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(0, 0x6000, 0x7000)]
        [EnemizerScenesExcluded(Scene.RoadToIkana, Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.PathToSnowhead)]
        // as its obj is 2, shouldn't be available in dungeons, maybe not indoors either
        [EnemizerScenesPlacementBlock(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        GrottoHole = 0x55,

        //[ActorizerEnabled] // broken: crash
        [FileID(102)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(1)]
        MajoraBalloonSewer = 0x5F,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [FileID(106)]
        [ObjectListIndex(0x8E)]
        [WaterVariants(0)] // works on ground too but cannot add ground without ground enemies showing up in fish tank
        Shellblade = 0x64,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [FileID(108)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1,2)]
        [UnkillableAllVariants] // they grow back, dont count as killable
        DekuBabaWithered = 0x66,

        [ActorizerEnabled] // works but her object is huge, and you cant talk or interact with her
        [FileID(204)]
        [ObjectListIndex(0xA2)]
        //[GroundVariants(0)]
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
        //[GroundVariants(3)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows down the race
        LikeLike = 0x6C,

        //[EnemizerEnabled] // we dont actually want this detected automatically, this will be added per-likelike manually
        //[ObjectListIndex(0xBE)] // this is really the shield, we're using it as the second likelike object
        [ObjectListIndex(0xB3)] // this is really the shield, we're using it as the second likelike object
        // 2 is ocean bottom, 0 is one in shallow shore water, 3 is land and one in shallow water
        //[GroundVariants(3)]
        LikeLikeShield = 0x28E, // 28E is a dummy actor ID, we only use it because it will never conflict with enemizer

        [ActorizerEnabled]
        // this is marked 2 and not 1 because 0x100 pots dont spawn in dungeons
        [ObjectListIndex(0x2)] // gameplay_keep, obj 1
        [FileID(126)]
        // according to CM, 0x100 is available everywhere as a pot, where 0x3F defines the drop item
        // so 1F is arrows, F is magic, B is three small rups? 7 is huge 200 rup, 17 is empty
        // 0xA is one rup, 1A and 14 are empty 04 is 20 rupes, 
        // 100 is empty, 110 is fairy, 10E is stick//////
        // 115 is 5 bombs, 105 is tall dodongo 50 rup, 106 is empty, 116 is empty
        // 101 is one rup, 111 SKULL TOKEN POT??!? 102 was 5 rups 112 empty
        // 103 empty, 113 is 10 deku nuts, 104 is red rup, 114 is empty
        //[GroundVariants(0x110)] // testing // 115 101 106 10E 10F
        [GroundVariants(0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)]
        //[DoublePerRoomMax(0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)] // prob not necessary
        [UnkillableAllVariants]
        ClayPot = 0x82,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x26EC)]
        [FileID(127)]
        [ObjectListIndex(0xD8)]
        [GroundVariants(0xFF03, 0xFF02, 0xFF01)]
        IronKnuckle = 0x84,

        [EnemizerEnabled]
        [FileID(132)]
        [ObjectListIndex(3)] // dungeon_keep, obj 3
        // and object 3 is so massive it never gets chosen even if we try to shove it into the object list
        //[GroundVariants(0x4015)]
        // 0 works, always empty
        [GroundVariants(0x115, 0x101, 0x106, 0x10E, 0x10F)] // actually spawns thank god, only in dungeons though, but outside its just an empty space so thats fine
        [UnkillableAllVariants]
        FlyingPot = 0x8D,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [FileID(134)]
        [ObjectListIndex(0xE4)]
        [GroundVariants(02, 0x2001, 0x300F, 0x100F)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // Slowing enemies
        Freezard = 0x8F,

        [EnemizerEnabled]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        // 1 creates a grass circle in termina field, 0 is grotto grass single
        [GroundVariants(0, 1)]
        [UnkillableVariants(0, 1)] // not enemy actor group
        [EnemizerScenesExcluded(Scene.Grottos)] // dont remove from peahat grotto
        GrassBush = 0x90,

        [ActorizerEnabled]
        [FileID(137)]
        [ObjectListIndex(0x12A)]
        [GroundVariants(0x807F, 0x8004, 0x8002)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [FlyingVariants(0x807F, 0x8004, 0x8002)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        //[UnkillableVariants(0x807F,0x8002)] // tested true
        [EnemizerScenesExcluded(Scene.TerminaField, Scene.GreatBayCoast, Scene.Grottos)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // blocking enemies
        Bombiwa = 0x92,

        //[ActorizerEnabled] // does not spawn
        [FileID(144)]
        [ObjectListIndex(0xF2)]
        [GroundVariants(0x0FFF)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.CuccoShack)]
        CuccoChick = 0x9D,

        [ActorizerEnabled]
        [FileID(151)]
        [ObjectListIndex(0xF4)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Grottos)]
        BeanSeller = 0xA5,

        [EnemizerEnabled]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1 // the rocks are free, you can take them home
        //6a does not load
        [GroundVariants(0x1F2)]
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        [EnemizerScenesExcluded(Scene.TerminaField)] // dont replace them in TF
        Rock = 0xB0, // rock

        //[EnemizerEnabled] // does not load, wrong vars?
        [ObjectListIndex(0x1)] // gamplaykeep obj 1
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.GoronShrine)]
        RockSirloin = 0xB9,

        [ActorizerEnabled]
        [FileID(172)]
        [ObjectListIndex(0x11D)]
        //0x1200, 0x1B00, 0x2800 spawns from the ground after you play a song
        // versions: 1200, 1B00, 2800 shows up a lot, 2D00 stonetower, 3200 zora cape
        // trading post version is 1
        // wish I could spawn the ones that dance so they are always dancing when the player gets there
        [GroundVariants(1)]
        [UnkillableAllVariants]
        // twinislands 0x5D snowhead 0x21, observatory 0x29, zora hall 0x33, trade 0x34, 0x48 goron village
        //[EnemizerScenesExcluded(0x5D, 0x21, 0x29, 0x33, 0x34, 0x37, 0x48, 0x4D, 0x50, 0x38, 0x5B, 0x53, 0x58, 0x5A, 0x5E)] 
        [EnemizerScenesExcluded(Scene.AstralObservatory, Scene.TradingPost)]
        Scarecrow = 0xCA,

        [ActorizerEnabled]
        [FileID(215)]
        [ObjectListIndex(0x132)]
        // 0xFFFF does not spawn
        // 0xA9F and the 0x29F crashes, but... was there ever a dog in that house?
        // 3FF is SCT dog, 0x22BF is spiderhouse dog, makes no sense if use mask
        // 0xD9F is old ranch dog WORKS, racetrack dogs unknown, spawned by the game
        //[GroundVariants(0x3FF, 0x22BF, 0xD9F)]
        // these two work in some scenes, crash in others: 0xD9F, 22BF,
        [GroundVariants(0x3FF)]
        //[PatrolVariants(0xD9F)] // didn't crash when put in deku palace
        [UnkillableAllVariants]
        [DoublePerRoomMax(0x3FF)]
        //[EnemizerScenesExcluded(0x6F, 0x10, 0x27, 0x35)]
        [EnemizerScenesExcluded(Scene.RanchBuildings, Scene.RomaniRanch, Scene.SouthClockTown, Scene.SwampSpiderHouse)]
        Dog = 0xE2,

        [ObjectListIndex(0x133)]
        //[GroundVariants(0x1E,0x2A02,0x2C0A,0x320F)]
        //[UnkillableVariants(0x3FF, 0x22BF)] // killing one not possible
        LargeCrate = 0xE5,

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
        [EnemizerScenesExcluded(Scene.IkanaGraveyard, Scene.OceanSpiderHouse)]
        //[UnkillableVariants(0x32)] the ones that circle the tombs, but dont respawn if placed anywhere else it seems, ignore
        Stalchild = 0xED,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x28F0)]
        [FileID(224)]
        [ObjectListIndex(0x143)]
        [GroundVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62)]
        [WallVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.SouthernSwamp, Scene.MilkRoad,
            Scene.RomaniRanch, Scene.IkanaCanyon)]
        GossipStone = 0xEF,

        [UnkillableAllVariants]
        SoundEffects2 = 0xF0,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [FileID(226)]
        [ObjectListIndex(0x6)]
        [FlyingVariants(0)]
        [UnkillableVariants(0)] // wait this one respawns?
        Guay = 0xF1,

        [ActorizerEnabled]
        [FileID(227)]
        [ObjectListIndex(0x146)]
        [GroundVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [WallVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RanchBuildings, Scene.RomaniRanch, Scene.Grottos, Scene.BeneathTheWell)]
        Cow = 0xF3,

        [ActorizerEnabled]
        [ObjectListIndex(0x1CB)] // gameplay_keep obj 1
        [GroundVariants(0)]
        [UnkillableAllVariants]
        LetterToPostman = 0xFE,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [FileID(241)]
        [ObjectListIndex(0x14E)]
        [FlyingVariants(0,2,3)]
        [DoublePerRoomMax(0,2,3)]
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
        //[EnemizerScenesExcluded(Scene.GreatBayTemple)] // think this was just here because you respawn detection
        BioDekuBaba = 0x12D,

        //[EnemizerEnabled] // todo: try randomizing
        [FileID(278)]
        [ObjectListIndex(0x161)]
        Raft = 0x13A,// carniverous raft, woodfall

        [ObjectListIndex(0x165)]
        PottedPlant = 0x13E,

        //[EnemizerEnabled] // does not spawn, tcrf can get it to spawn but it does nothing
        //[ObjectListIndex(0x161)]
        //[GroundVariants(0)]
        //[UnkillableAllVariants]
        //Frog2 = 0x147, // this doesn't exist in the actors by area list, not vanilla

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [FileID(296)]
        [ObjectListIndex(0x16A)]
        [GroundVariants(0x0C01,0x1402,0xFF03,0xFF01,0xFF00,0x0A01,0x0202,0x801,0xFF02)]
        [EnemizerScenesExcluded(Scene.GreatBayTemple)] // necessary to climb
        [EnemizerScenesPlacementBlock(Scene.SouthernSwampClear)] // crash transitioning witch shop room
        // termina field, ff00 gbt waterchu, the rest are assumed respawn until proven otherwise
        [UnkillableVariants(0xFF03,0xFF01,0xFF00,    0x0C01,0x1402,0x0A01,0x0202,0x801,0xFF02)]
        ChuChu = 0x14A,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [FileID(297)]
        [ObjectListIndex(0x16B)]
        [WaterVariants(0x0F00,0x0300)]
        [SinglePerRoomMax(0x0F00, 0x0300)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp)] // massive lag
        Desbreko = 0x14B, // dead fish swarm from pirates fortress

        [ActorizerEnabled]
        [ObjectListIndex(0x16C)]
        [GroundVariants(0)]
        [SinglePerRoomMax(0)]
        [EnemizerScenesExcluded(Scene.EastClockTown)]
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall, Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTower)] // big blocking
        Bell = 0x14E,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1250)]
        [FileID(307)]
        [ObjectListIndex(0x171)]
        [GroundVariants(0)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slowing enemies
        Nejiron = 0x155,// Rolling exploding rock in Ikana

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [FileID(313)]
        [ObjectListIndex(0x172)]
        [FlyingVariants(0xFF34, 0xFF02, 0xFF03, 0x019F, 0x0102, 0x0103)]
        [WallVariants(0xFF9F)]
        // one of these is sit on the wall bat from rtss: FF03/01/9F
        [SinglePerRoomMax(0xFF34)] // swarm
        //[EnemizerScenesExcluded(Scene.InvertedStoneTower)] // think this is here for death, but death has mini-death for his bats...?
        BadBat = 0x15B,

        [ActorInitVarOffset(0x37D0)]
        [FileID(315)]
        [ObjectListIndex(0x178)]
        Wizrobe = 0x15D,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1830)]
        [FileID(322)]
        [ObjectListIndex(0x17A)]
        [FlyingVariants(0x9605, 0x3205, 0x6405, 0x8C05, 0xFA01, 0xFA00)]
        // this variety is slow spawn, meaning you have to walk up to it: 0x2800, 0x3200, 0xC200, 0xFA00
        [GroundVariants(0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0xFF01, 0xFF05, 0xC200)] 
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [UnkillableVariants(0x6404,0x7804, 0x9605,0x3205,0x6405,  0x8C05, 0xFF05)] // respawning
        Bo = 0x164, //boe, small ball of snow or soot

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [FileID(331)]
        [ObjectListIndex(0x181)]
        // the snowhead version that doesnt aggro is 01
        [GroundVariants(0x8C, 0x28, 0x3C, 0x46, 0x32, 0x1, 0x8023, 0x5, 0x14, 0x8028, 0x8014)]
        [WallVariants(0x1)] // peaceful, just wants cheese
        // the values on the left are tested respawning, the values on the right are untested, conservative assume respawning
        [UnkillableVariants(0x8014,0x8028,0x8023,   0x0032,0x0005,0x0014)] // respawning
        RealBombchu = 0x16F,

        [EnemizerEnabled]
        [ObjectListIndex(0x185)]
        [FlyingVariants(0, 1, 2, 3, 4, 5)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(0, 1, 2, 3, 4, 5)]
        [EnemizerScenesExcluded(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        [EnemizerScenesPlacementBlock(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        Tingle = 0x176,

        //[ActorizerEnabled] // walks forever in a straight line, until we can keep them on a path they are a boring enemy
        [ObjectListIndex(0x135)]
        [PatrolVariants(0x12FF)]
        [UnkillableAllVariants]
        DekuPatrolGuard = 0x17A,

        [EnemizerEnabled] // biggest issue: they dont really attack, this isn't the version that spawns over and over
        [ActorInitVarOffset(0x1C6C)]
        [FileID(346)]
        [ObjectListIndex(0x16B)]
        // A2 might be the ones that respawn over and over, the "Encounter"
        // 82 and 62 are found in the map room, both just kinda spin, never engages
        // zora cape has 000, same as the other two
        [WaterVariants( 0xA2, 0x82, 0x62, 0 )]
        SkullFish = 0x180,

        [ActorizerEnabled]
        [FileID(349)]
        [ObjectListIndex(0x1)] // obj 1: gameplay keep, but can't set that
        [GroundVariants(0x7F, 0x17F)] // both crash for some reason
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Woodfall, Scene.DekuPalace, Scene.WoodfallTemple, Scene.OdolwasLair, 
            Scene.EastClockTown, Scene.NorthClockTown, Scene.IkanaCastle, Scene.SnowheadTemple)] 
        DekuFlower = 0x183,

        //[EnemizerEnabled] // AI gets confused, backwalks forever, pathing?
        [ActorInitVarOffset(0x445C)]
        [FileID(250)]
        [ObjectListIndex(0x18D)]
        [PatrolVariants(0x700)]
        [SinglePerRoomMax(0x700)]
        [EnemizerScenesExcluded(Scene.InvertedStoneTower, Scene.StoneTowerTemple)]
        Eyegore = 0x184,// walking laser cyclops in inverted stone tower

        [ActorizerEnabled]
        [ObjectListIndex(0x18C)]
        [WallVariants(0x907F, 0xA07F)]
        [UnkillableAllVariants]
        Clock = 0x19C,

        //[ActorizerEnabled] // broken: 0 doesnt spawn, and the rest explode almost instantly
        [FileID(381)]
        [ObjectListIndex(0x19B)]
        //[FlyingVariants(0)] // doesn't spawn unless in the ranch for some reason, shame too this is the one in the ranch
        [FlyingVariants(0x8050,0x805A,0x8064)] // explodes randomly when entering scene
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RomaniRanch)] // dont replace actual romani balloons
        PoeBalloon = 0x1A6,

        [FileID(0x1A8)]
        [ObjectListIndex(0x19E)]
        //[WaterVariants( unk )]
        BigOcto = 0x1A8,

        //[EnemizerEnabled]
        [ObjectListIndex(0xE)]
        // snowhead : 0x5E00,0x6000, 0x5800,0x5600, GreatBay: 0x6000
        // huh? these repeat per dungeon? 
        [FlyingVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        [GroundVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(0x5E00, 0x6000, 0x5800, 0x5600)]
        [EnemizerScenesExcluded(Scene.Woodfall, Scene.Snowhead, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        ElfBubble = 0x1B1,

        [ActorizerEnabled] // weird that he is so rare though
        [ObjectListIndex(0x1A3)]
        [GroundVariants(0)] // 0 is clocktower
        [UnkillableAllVariants]
        [SinglePerRoomMax(0)]
        HappyMaskSalesman = 0x1B5,

        Lillypad = 0x1B9,

        //[EnemizerEnabled]
        [ActorInitVarOffset(0x1FF0)]
        [FileID(406)]
        [ObjectListIndex(0x1A6)]
        [GroundVariants(0x0)]
        [EnemizerScenesExcluded(Scene.WoodfallTemple)] // req for gekko miniboss, do not touch until fix
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        Snapper = 0x1BA,

        //[ActorizerEnabled] //busted
        [ObjectListIndex(0x1AF)]
        // 1, 0xFFF3 do not load, 10 just crashes
        [GroundVariants(0x10)] // unknown which ones do what we want (FFF3, 01, 10) 814 walking about outside
        // unkillable
        // only one
        //[EnemizerScenesExcluded(0x30)]
        Dampe = 0x1CA,

        [ActorInitVarOffset(0x1FD0)]
        [FileID(426)]
        [ObjectListIndex(0x1B5)]
        // good candidate for wall enemy
        Dexihand = 0x1D1,// ???'s water logged brother

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2940)]
        [FileID(435)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0x1E9, 0x1F4, 0x208, 0x214, 0x224, 0x236, 0x247, 0x253, 0x262, 0x275, 0x283, 0x291, 0x2A0)]
        [UnkillableVariants(0x1E9, 0x1F4, 0x208, 0x214, 0x224, 0x236, 0x247, 0x253, 0x262, 0x275, 0x283, 0x291, 0x2A0)] // do they trigger flags to kill?
        [EnemizerScenesExcluded(Scene.BeneathTheWell, Scene.IkanaCanyon)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows down player too much in a race setting
        GibdoWell = 0x1DA,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [FileID(446)]
        [ObjectListIndex(0x1C4)]
        [GroundVariants(0xFF00, 0xFF01, 0, 1)]
        [SinglePerRoomMax(1, 0xF001)]// limit the bigger one
        Eeno = 0x1E6,// Snowmen in showhead
        
        [EnemizerEnabled]
        [ActorInitVarOffset(0x36A0)]
        [FileID(448)]
        [ObjectListIndex(0x1C5)]
        //0x100 is red, 0x200 is blue, 0x300 is green, 00 is purple, however, its difficult to fight more than 2
        [FlyingVariants(0x300, 0x200, 0x100)]
        [GroundVariants(0x300, 0x200, 0x100, 0)]
        [SinglePerRoomMax(0, 0x100, 0x200, 0x300)] // only one per
        // no scene exclusion necessary, get spawned by the poe sisters minigame but they aren't actors in the scene to be randomized
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
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
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WestClockTown, Scene.SouthClockTown, Scene.NorthClockTown, Scene.EastClockTown)]
        Postbox = 0x1F2,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [FileID(459)]
        [ObjectListIndex(0x1C3)]
        [FlyingVariants(0x00FF)]
        [GroundVariants(0x00FF)] // this is fine because the only vanilla instance is excluded, so this doesn't describe spawns too
        [EnemizerScenesExcluded(Scene.InvertedStoneTower)]
        Poe = 0x1F3,

        SmallSnowball = 0x1F9,

        //[ActorizerEnabled] //4x can spawn without issue
        [ObjectListIndex(0x1DF)]
        [GroundVariants(0x1400)] // all other versions are 0x13** or 0x1402
        [SinglePerRoomMax(0x1400)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.GoronShrine)]
        GoronKid = 0x201,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [FileID(475)]
        [ObjectListIndex(0x1EB)]
        [FlyingVariants(0x0)]
        [GroundVariants(0x0)] // spawns really low to ground in vanilla
        [EnemizerScenesExcluded(Scene.PiratesFortressRooms)] // pirate beehive cutscene
        GiantBeee = 0x204,

        //[ActorizerEnabled] // spawns but invisible, can hit it but cannot see it
        [ObjectListIndex(0x1EE)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        Gong = 0x207,

        [EnemizerEnabled]
        [FileID(479)]
        [ObjectListIndex(0x1F1)]
        //[FlyingVariants(0xFF00, 1, 0x102)] // 1 is a possible type? well: ff00
        [FlyingVariants(1)] // 1 is a possible type? well: ff00
        [EnemizerScenesExcluded(Scene.BeneathTheWell, Scene.BeneathGraveyard)] // well and dampe house must be vanilla for scoopsanity
        [DoublePerRoomMax(0xFF00, 1, 0x102)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp)] // they either dont spawn, or when they appear they lock your controls, bad
        BigPoe = 0x208,

        //[ActorizerEnabled] // just the head visible? meh? need to increase size too I think
        [ObjectListIndex(0x1F2)]
        [WallVariants(0x907F, 0xA07F)]
        [UnkillableAllVariants]
        // maybe dont remove originals
        CowFigurine = 0x20A,

        //[ActorizerEnabled] // likes to crash
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        [GroundVariants(0xFF03)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(0xFF03)]
        Seth1 = 0x20B, // the green shirt guy, "Seth"? spiderhouses

        [ActorizerEnabled]
        [ObjectListIndex(0x1F5)]
        [WallVariants(0x3F)] // 3F has no cutscene, I think
        [UnkillableAllVariants]
        SkullKidPainting = 0x210,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1C50)]
        [FileID(493)]
        [ObjectListIndex(0x201)]
        [GroundVariants(0xFF,0x80FF)]
        Leever = 0x216,

        //[EnemizerEnabled] // cutscene is broken without camera placement, player stuck in place
        [ObjectListIndex(0x204)]
        //[GroundVariants(0x24B)] // 3 different versions
        [GroundVariants(0x24B)]
        //[EnemizerScenesExcluded(0x23)] // do not remove original, for now
        PirateColonel = 0x21D,

        //[EnemizerEnabled]
        [ObjectListIndex(0x12E)]
        [PatrolVariants(0xEA)]
        [UnkillableAllVariants]
        PatrollingPirate = 0x21E,

        //[ActorizerEnabled] // does not spawn? boo
        // did I maybe use the wrong vars?
        [ObjectListIndex(0x211)]
        [GroundVariants(1)]
        [WaterVariants(1)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.ZoraHallRooms)]
        [SinglePerRoomMax(1)]
        Japas = 0x231,

        //[EnemizerEnabled] // we already have well versions
        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [FileID(524)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0, 0x81, 0x82, 0x83, 0x84, 0x85)]
        [UnkillableVariants(0, 0x81, 0x82, 0x83, 0x84, 0x85)]
        [EnemizerScenesExcluded(Scene.IkanaCanyon)] // dont replace the train
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        GibdoIkana = 0x235,

        //[ActorizerEnabled] // boring since its hidden unless you wear one often junk mask
        [ObjectListIndex(1)]
        [GroundVariants(0x7F)]
        [UnkillableAllVariants]
        MushroomCloud = 0x23B,

        [ActorizerEnabled]
        [ObjectListIndex(0xA1)]
        // 9 is the one that sells you kegs
        [GroundVariants(9)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(9)]
        [EnemizerScenesExcluded(Scene.BombShop)]
        Goron = 0x242,

        //[ActorizerEnabled] // unless I write dayonly/nightonly, this is too flukey
        [ObjectListIndex(0xFF)]
        // 00 is the version from the inn, "dont talk to her shes thinking" meaning the rosa sister
        // 2 doesn't ever seen to spawn, day or night, think its a fluke
        [GroundVariants(0x2)] // 01 is laundry pool, but he only spawns at night, ignoring actor time spawn settings for a scene
        [UnkillableAllVariants]
        //[EnemizerScenesExcluded(0x15, 0x70, 0x61)]
        [EnemizerScenesExcluded(Scene.StockPotInn, Scene.LaundryPool, Scene.MilkBar)] // think him being in milkbar is a credits thing
        [DoublePerRoomMax(0, 1, 2)]
        GuruGuru = 0x248,

        //[ActorizerEnabled] // doesn't spawn with a flower, looks silly
        // HEY the other actors, that make flowers, we can use them for dual deku, would require custom code to put them on top of each other
        [FileID(566)]
        [ObjectListIndex(0x12B)]
        [GroundVariants(0x584)]
        [UnkillableAllVariants] // I think?
        SleepingScrub = 0x25F,

        //[ActorizerEnabled] // spawned for me, but not streamers? weird time dependencies?
        [FileID(267)]
        [ObjectListIndex(0x23F)]
        [FlyingVariants(7, 5)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.GreatBayCoast, Scene.ZoraCape)]
        Seagulls = 0x267,

        //[ActorizerEnabled] // broken: if you pop it, it locks you in a never ending cutscene
        [FileID(601)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(0)]
        //[UnkillableAllVariants]  // untested
        MajoraBalloonNCT = 0x282,

        [ActorizerEnabled] // shows up too much tho, because small object
        [FileID(608)]
        [ObjectListIndex(0x25B)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [SinglePerRoomMax(0)]
        ButlersSon = 0x289,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2E30)]
        [FileID(616)]
        [ObjectListIndex(0x22)]
        //[FlyingVariants(0, 1)] // two? one that steals and one that doesn't?
        [FlyingVariants(0)] // zero seems safe, does not steal sword or anything, 1 does not spawn
        [SinglePerRoomMax(0)]
        [EnemizerScenesExcluded(Scene.TerminaField)] // do not remove original
        Takkuri = 0x291,

        [FileID(1114)]
        [ObjectListIndex(0)]
        [GroundVariants(0)]
        [FlyingVariants(0)]
        [WaterVariants(0)]
        [UnkillableAllVariants]
        Empty = -1

    }
}
