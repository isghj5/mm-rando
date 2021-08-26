using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Models.Vectors;
using System.Security.Cryptography.X509Certificates;

namespace MMR.Randomizer.GameObjects
{
    public enum Actor
    {
        /// the main enumator value is the vanilla actor list ID

        // warning: companion actors can bypass variants that exist, you might remove a variant but it still exists as a companion
        // fixing this is a performance loss, just dont be stupid

        //[ActorizerEnabled] // crashes, probably because there are now two players
        [FileID(38)]
        [GroundVariants(0x0E02)] // sitting in clocktown?
        Player = 0x0,

        [FileID(39)]
        [ObjectListIndex(1)]
        [GroundVariants(0x7FF, 0xFFFF)] // params: > 0 and else, -1
        // not stalfos in this game
        // like, goron punch crator, or moon tear
        En_Test = 0x1, // En_Test

        [FileID(40)]
        En_GirlA = 0x2,

        [FileID(41)]
        En_Part = 0x3,

        //[EnemizerEnabled] // we dont want as an actual actor, we want as a companion
        [FileID(42)]
        [ObjectListIndex(1)] // gameplay_keep obj 1
        // 0x83F0 is tiny candle light
        //[GroundVariants(0x83F0, 0x27F5)] // TODO finish checking the rest of possible variations
         // 0x7F4 is the bright yellow light of the graveyard smash
         // 0x7FF is cyan/blue flames of road to ikana, FE is goron graveyard blue,
         // 83F0 is dampe house candle
        [GroundVariants(0x7F4, 0x7FE)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Woodfall)]
        Flame = 0x4, // En_Light

        // real fake doors
        //[ActorizerEnabled] // whiners complaining that if you open them you softlock. sounds like a perfect mimick door to me
        // different doors have different variables for different objects, unless I program multiple objects only one can be used
        [FileID(43)]
        [ObjectListIndex(0x231)]
        [WallVariants(0x7F)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.ZoraHall)]
        Door = 0x5, // En_Door

        [ActorizerEnabled]
        [FileID(44)]
        [ObjectListIndex(0xC)]
        // these three are from inverted stone tower, however when placed in TF, 2/3 were invisible chests
        // addresses make no sense
        // the top nibble is type, 0x7 seems to be enemy clear, also type 1
        //0x5 is woodentype, 0xC is switch activated
        // gomess is 0x27BE, which does not spawn util you kill him, so obviously the top byte is NOT that simple in MM, snowhead is 27BE
        // the MM item notes are just wrong it seems, unless it being open breaks invisible
        [GroundVariants( 0x57BE, 0x59DD, 0x56BF, 0x5FDE, 0x5579,
            0x561E, 0x5C79, 0x5991, 0x5B58, 0x5A1E,
            0x0AFB, 0x099C)] // two free, the rest are gold invisible 
        [VariantsWithRoomMax( max:1, variant: 0x57BE, 0x59DD, 0x56BF, 0x5FDE, 0x5579,
            0x561E, 0x5C79,0x5991, 0x5B58, 0x5A1E,
            0x0AFB, 0x099C)] // brown, harder to see in perpheral vision, not invisible
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x3F5F)] // can place around chests
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)] // fairies around chests make sense, just not a full fairy fountain
        [EnemizerScenesExcluded(Scene.InvertedStoneTower)]
        [EnemizerScenesPlacementBlock(Scene.SwampSpiderHouse, Scene.OceanSpiderHouse, 
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        TreasureChest = 0x6, // En_Box

        [FileID(45)]
        PametFrog = 0x7, // En_Pammetfrog the frogminiboss

        [EnemizerEnabled]
        [FileID(46)]
        [ActorInitVarOffset(0x2A60)]
        [ObjectListIndex(0x5)]
        [WaterVariants(0xFF00)]
        [EnemizerScenesExcluded(Scene.IkanaCanyon, Scene.GreatBayTemple)]
        Octarok = 0x8, // En_Okuta

        [FileID(47)]
        PowderKeg = 0x9, // En_Bom

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B30)]
        [FileID(48)]
        [ObjectListIndex(0x9)]
        //[GroundVariants(1)] //issue: used for replacement, puts ground enemies in air
        [FlyingVariants(1)] 
        WallMaster = 0xA, // En_Wallmas

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A40)]
        [FileID(49)]
        [ObjectListIndex(0xA)]
        [GroundVariants(1, 0)]
        [VariantsWithRoomMax(max: 1, variant: 1)]
        [VariantsWithRoomMax(max: 2, variant: 0)] // 3 is enough, it can lag rooms as is
        [CompanionActor(Flame, variant: 0x7F4)] // they're teething
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // too big, can block the butler race
        Dodongo = 0xB, // En_Dodongo

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [FileID(50)]
        [ObjectListIndex(0xB)]
        [FlyingVariants(0x8003,0x04,0)] // which ones are fire and ice?
        Keese = 0xC, // En_Firefly

        //[ActorizerEnabled] // crashes and other weird issues
        [FileID(51)]
        [ObjectListIndex(1)]
        // 400E is child cutscene, should just sit there and neigh
        // Gorman track has a lot??
        //  0, 0100, 96FF, 4605, 3c05, 4605, 9605,5005
        // 96FF/3c05 did not spawn in field, 5005/4605 are ridable epona, both can exist and are ridable
        // experimental: 9605 did not spawn, neither did 0x3C00, 
        // 46FF did spawn just fine
        // 0/100 are crash I think
        [GroundVariants(0x400E, 0x4600, 0x5005)]
        [UnkillableAllVariants]
        // if you leave or enter a room after spawning epona you crash, not sure why, but so far the known areas are all dungeons
        // also you crash if you enter a diffent room (southern swamp) without epona song
        [EnemizerScenesPlacementBlock(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple)]
        //[OnlyOneActorPerRoom]
        Horse = 0xD, // En_Horse

        // NO FILE??? WHERE IS IT
        [ObjectListIndex(0x1)]
        [UnkillableAllVariants]
        Item00 = 0xE, // En_Item00

        [FileID(52)]
        Arrow = 0xF, // En_Arrow

        [ActorizerEnabled]
        [FileID(53)]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        // 4 is group of fairies out of a fountain, 7 is large healing fairy, 9 is yellow fairy (unused)
        // 4 has been removed because its unlikely to make sense for the location, and if its in TF it might contribute hard to TFG
        [GroundVariants(2, 7, 9)]
        [FlyingVariants(2, 7, 9)]
        //[VariantsWithRoomMax(max: 1, variant: 4)] // don't create too many fairy fountains
        [VariantsWithRoomMax(max: 2, variant: 7)] // maybe limit the secret menu fairies
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.BeneathTheWell)] // dont remove from well
        Fairy = 0x10, // En_Elf

        [EnemizerEnabled] // now that they are testy, lets count them as enemies
        [FileID(54)]
        [ObjectListIndex(0xF)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max: 10, variant:0xFFFF)]
        [UnkillableAllVariants]
        // I would like a flying variant, but they seem to drop like a rock instead of float down
        //[EnemizerScenesExcluded(0x15, Scene.AstralObservatory, 0x35, 0x42, 0x10)]
        [EnemizerScenesExcluded(Scene.AstralObservatory, Scene.RomaniRanch, Scene.CuccoShack, Scene.MilkBar)]
        FriendlyCucco = 0x11, // En_Niw

        [EnemizerEnabled]
        [FileID(55)]
        [ActorInitVarOffset(0x32C0)]
        [ObjectListIndex(0x12)]
        // FE are road to mountain village type AND greatbay type
        // FD is underground, FE can be both but now, for detection, FD is water only
        [GroundVariants(0xFFFD, 0xFFFF)] // FF does not exist in MM vanilla, red variety
        [WaterVariants(0xFFFE)] 
        Tektite = 0x12,

        Empty13 = 0x13,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [FileID(56)]
        [ObjectListIndex(0x14)]
        // 0 is the big one in peahat grotto, 1 is the little ones normally only spawned by the big one if you hit them at night
        [GroundVariants(0)]
        [FlyingVariants(1)]
        [VariantsWithRoomMax(max: 3, variant: 0)]
        [VariantsWithRoomMax(max: 7, variant: 1)] // lag, not difficulty
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall)] // too big, can block the butler race
        Peahat = 0x14, // En_Peehat

        [FileID(119)]
        Butterfly = 0x15, // En_Butte

        [FileID(118)]
        Insect = 0x16, // En_Insect

        //[ActorizerEnabled] // not sure what to believe anymore, labfish is a different actor, school of fish is different, lets turn this off
        [FileID(120)]
        [ObjectListIndex(0x16B)] // gameplay keep obj 1
        [WaterVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.MarineLab)]
        Fish = 0x17, // En_Fish

        [FileID(57)]
        En_Holl = 0x18, // En_Holl

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [FileID(58)]
        [ObjectListIndex(0x17)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 2, variant: 0)]
        [EnemizerScenesExcluded(Scene.SecretShrine)] // issue: spawn is too high, needs to be lowered
        [EnemizerScenesPlacementBlock(Scene.BeneathGraveyard, Scene.DekuShrine)] // crash in graveyard
        Dinofos = 0x19,

        [FileID(59)]
        Flagpole = 0x1A, // En_Hata

        [FileID(60)]
        ChildZelda = 0x1B, // En_Zl1

        [FileID(61)]
        Viewer = 0x1C, // En_Viewer

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [FileID(62)]
        [ObjectListIndex(0xE)]
        [FlyingVariants(0)] // 0 works, but OOT used FFFF
        [GroundVariants(0)] // 0 works, but OOT used FFFF
        [WaterVariants(0)] // 0 works, but OOT used FFFF
        //[EnemizerScenesExcluded(0x69)]
        Shabom = 0x1D,// the flying bubbles from Jabu Jabu, exist only in giants cutscenes

        [FileID(63)]
        Door_Shutter = 0x1E, // Door_Shutter

        Empty1F = 0x1F,

        [FileID(64)]
        ZoraFinBoomerang = 0x20, // En_Boom

        //[ActorizerEnabled] // disabled for now because crash if leaving grotto into scene with ben
        [FileID(65)]
        [ObjectListIndex(1)] // gameplay_keep obj 1
        [GroundVariants(0)] // 0 is ben
        [UnkillableAllVariants]
        // Ben seems to be cursed, if you enter a scene with him from a grotto it can crash (~90% chance?) 
        // but entering those same scenes from horizontal loading zones is fine
        /*[EnemizerScenesPlacementBlock(Scene.TerminaField,
            Scene.WoodsOfMystery, Scene.RoadToSouthernSwamp, Scene.SouthernSwamp, Scene.SouthernSwampClear,
            Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.MountainVillageSpring, Scene.PathToSnowhead,
            Scene.GreatBayCoast, Scene.ZoraCape, Scene.RoadToIkana, Scene.IkanaGraveyard, Scene.IkanaCanyon,
            Scene.Grottos)]
        */
        Ben = 0x21, // En_Torch2

        [ActorizerEnabled]
        [FileID(66)]
        [ObjectListIndex(0xBC)]
        [GroundVariants(1,2,3,4)] // 3 is southern swamp, 4 is laundry pool, the versions in teh mountaion have the F flag, think the rest are numbered
        [VariantsWithRoomMax(max: 1, variant: 1, 2, 3, 4)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.LaundryPool)]
        Frog1 = 0x22, // En_Minifrog

        Empty23 = 0x23,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [FileID(67)]
        [ObjectListIndex(0x20)]
        // 4 is in the astral observatory, and has a spawn kill flag, so don't use
        [FlyingVariants(0xEF, 0x7F, 0x3F, 0x4)]
        //[RespawningVariants(0x4)] // doesn't respawn after death, so dont put where respawning enemies are bad either
        [VariantsWithRoomMax(max: 0, variant: 4)] // just dont use, it might be more broken and a cause of a crash? doubt
        [EnemizerScenesExcluded(Scene.OceanSpiderHouse)] // shared object with goldskulltula, cannot change without
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.Snowhead, 
            Scene.MountainVillageSpring, Scene.TwinIslandsSpring)] // not a problem, just weird seeing them fly like that
        Skulltula = 0x24, // En_St

        Empty25 = 0x25,

        //[ActorizerEnabled]
        // FILE MISSING
        // issues: listing all the vars for all signs is going to be hell
        [ObjectListIndex(0xFC)] // the spreadsheet thinks this is free but I dont think so, think its a multi-object like tsubo
        //[GroundVariants()]
        [UnkillableAllVariants]
        Sign = 0x26, // En_A_Obj

        [FileID(68)]
        Obj_Wturn = 0x27, // Obj_Wturn

        [FileID(69)]
        RiverSound = 0x28, // En_River_Sound

        Empty29 = 0x29,

        [FileID(70)]
        Ossan = 0x2A, // En_Ossan

        Empty2B = 0x2B,
        Empty2C = 0x2C,

        [EnemizerEnabled] // sometimes crash, caused by paths I bet TODO rewrite him so he just wanders around a home instead
        [ActorInitVarOffset(0x1CC0)]
        [FileID(71)]
        [ObjectListIndex(0x1D)]
        // the first byte is path, if its FF it should sit in one spot instead of pathing, which means no crash
        //   would be better if it patroled or something but better that crash at least
        [FlyingVariants(0xFF)]
        [GroundVariants(0xFF)]
        [PathingVariants(1,2,3,4,7)]
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [VariantsWithRoomMax(max: 10, variant: 0xFF)]
        [RespawningAllVariants] // they do NOT respawn, this is temporary: light arrow req makes them difficult to kill early in the game
        //[EnemizerScenesExcluded(Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        // this old list of crash locations is for pathing versions, which we no longer use
        //[EnemizerScenesPlacementBlock(Scene.AstralObservatory, Scene.RoadToIkana, Scene.Woodfall, Scene.PathToMountainVillage, Scene.IkanaCastle,
        //    Scene.MountainVillageSpring, Scene.BeneathGraveyard, Scene.DekuShrine, Scene.IkanaGraveyard )] // known crash locations
        DeathArmos = 0x2D, // En_Famos

        Empty2E = 0x2E,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [FileID(72)]
        [ObjectListIndex(0x2A)]
        [GroundVariants(0xFFFF)]
        [VariantsWithRoomMax(max: 8, variant: 0xFFFF)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.OdolwasLair)]//Scene.GoronRacetrack)] // some poor unfortunate souls asked for a more chaotic race
        BombFlower = 0x2F, // En_Bombf

        Empty30 = 0x30,
        Empty31 = 0x31,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)]
        [FileID(73)]
        [ObjectListIndex(0x30)]
        [GroundVariants(0xFFFF, 0x7F)]
        [VariantsWithRoomMax(max: 7, variant: 0xFFFF, 0x7F)] // weirdly high cpu usage, not a low as other still enemies
        Armos = 0x32, // En_Am

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [FileID(74)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1,0)] // 0 regular, 1 is big one from OOT forest temple
        [VariantsWithRoomMax(max: 1, variant: 1)]
        [VariantsWithRoomMax(max: 8, variant: 0)]
        DekuBaba = 0x33, // En_Dekubaba

        [FileID(75)]
        En_M_Fire1 = 0x34, // En_M_Fire1

        [FileID(76)]        
        En_M_Thunder = 0x35, // En_M_Thunder

        [FileID(77)]
        Bg_Breakwall = 0x36, // Bg_Breakwall

        Empty37 = 0x37,

        [FileID(78)]
        [ObjectListIndex(0x3E)]
        Door_Warp1 = 0x38, // Door_Warp1

        [ActorizerEnabled]
        [ObjectListIndex(0x3E)]
        [FileID(79)]
        [GroundVariants(0x0)] // the 101 and above are for warp TO bosses
        [VariantsWithRoomMax(max: 1, variant:0x0)]
        [UnkillableAllVariants]
        WarpDoor = 0x38, // Door_Warp1

        [ActorizerEnabled]
        [ObjectListIndex(0x80)]
        [FileID(79)]
        // 0x1180 below graveyard
        // 0x289 gold pirate torches
        // 0x287F east clocktown
        [GroundVariants(0x1180, 0x289, 0x287F, 0x207F)]
        [CompanionActor(MothSwarm, variant: 1, 2, 3, 4, 7)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(MothSwarm, CompanionAlignment.Above, ourVariant: -1,
           variant: 1, 2, 3, 4, 7)] // they're free, and they are moths, makes sense
        [EnemizerScenesExcluded(Scene.WoodfallTemple, Scene.SouthernSwamp, Scene.SouthClockTown, Scene.DekuShrine, Scene.WestClockTown, Scene.SouthernSwampClear,
            Scene.SnowheadTemple, Scene.BeneathGraveyard, Scene.GreatBayCoast, Scene.GreatBayTemple, Scene.OceanSpiderHouse, Scene.BeneathTheWell, Scene.PiratesFortressRooms, Scene.PoeHut)]
        Torch = 0x39, // Obj_Syokudai

        [FileID(80)]
        [ObjectListIndex(0x96)]
        Item_B_Heart = 0x3A, // Item_B_Heart

        [EnemizerEnabled]
        [ActorInstanceSize(0x2C8)]
        [ActorInitVarOffset(0x1D30)]
        [FileID(81)]
        [ObjectListIndex(0x40)]
        [GroundVariants(0xFF02, 0xFF00, 0xFF01)]
        [UnkillableVariants(0xFF01)]
        [CompanionActor(DekuFlower, variant: 0x7F, 0x17F)] // do you think they make them or trade like hermitcrabs?
        [EnemizerScenesExcluded(Scene.Woodfall)]//, Scene.DekuPalace)]
        MadShrub = 0x3B, // En_Dekunuts

        [EnemizerEnabled]
        [ActorInstanceSize(0x464)]
        [ActorInitVarOffset(0x1AF0)]
        [FileID(82)]
        [ObjectListIndex(0x51)]
        [GroundVariants(0)]
        [CompanionActor(Flame, variant: 0x7F4)]
        [AlignedCompanionActor(Flame, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x7F4)] // I'll just put this over with the rest of the fire
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // visible waiting below the bridges
        RedBubble = 0x3C, // En_Bbfall

        [FileID(83)]
        Arms_Hook = 0x3D, // Arms_Hook

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [FileID(84)]
        [ObjectListIndex(0x51)]
        [FlyingVariants(0xFFFF)]
        [VariantsWithRoomMax(max: 8, variant: 0xFFFF)]
        [CompanionActor(Flame, 0x7FE)] // blue flames
        [UnkillableAllVariants] // respawning
        BlueBubble = 0x3E, // En_Bb

        [FileID(85)]
        Bg_Keikoku_Spr = 0x3F, // Bg_Keikoku_Spr

        Empty40 = 0x40,

        [ActorizerEnabled]
        [ActorInstanceSize(0x1A0)]
        [ActorInitVarOffset(0x1A0)]
        [FileID(86)]
        [ObjectListIndex(0x61)]
        // bush: 0xFF0B
        // small tree: 0xFF02
        // big tree: 0xFF00
        // tree with shop man in it: 
        [GroundVariants(0xFF0B, 0xFF02, 0xFF00, 0xFF01, 0xFF1A, 0x0A1A)]
        [VariantsWithRoomMax(max: 0, variant: 0xFF0D)]// 0xFF0D crashes TF do not use 0D is from the cucco shack
        [VariantsWithRoomMax(max: 1, variant: 0xA1A)]// has the shop keeper
        //[GroundVariants(0xFF01, 0xFF1A)] //testing
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.TerminaField, Scene.TwinIslandsSpring)] // need to keep in termina field for rupee rando
        Treee = 0x41, // En_Wood2

        Empty42 = 0x42,

        //[EnemizerEnabled] //hardcoded values for his entrance spawn make the camera wonky, and his color darkening is wack
        [FileID(87)]
        [ActorInstanceSize(0x938 + (0x2a0 * 22))] // 0x938 // oversized because he spawns little bats and can easily overblow ram
        // WARNING: this size aprox does NOT include the actor overlay size of the bats (En_Minideath)
        [ObjectListIndex(0x52)]
        [GroundVariants(0)] // can fly, but weirdly is very bad at changing height if you fight in a multi-level area
        [OnlyOneActorPerRoom] // only fight her if you fight only one
        [UnkillableAllVariants] // is NOT unkillable, but assume never have light arrows until the last second of a run, do not place where can block an item
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple)] // lets not randomize his normal spawn
        // good candidate for night and dungeon spawn only
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.RoadToIkana,
            Scene.SouthernSwamp, Scene.WoodsOfMystery, Scene.Woodfall, Scene.TwinIslandsSpring, Scene.PathToSnowhead, 
            Scene.Snowhead, Scene.GoronVillage, Scene.DekuShrine, Scene.StoneTower)]
        Gomess = 0x43, // En_Death 🤘

        [FileID(88)]
        MiniDeath = 0x44, // En_MiniDeath

        Empty45 = 0x45,
        Empty46 = 0x46,

        [EnemizerEnabled]
        //[ItemsReqRemove(Item.ItemBombBag)]
        [ActorInitVarOffset(0x1240)]
        [FileID(89)]
        [ObjectListIndex(0x6A)]
        [GroundVariants(0x600, 0x800, 0x500, 0xFF00, 0x300)] // all working varieties
        [UnkillableAllVariants] // not unkillable, but for now, stops them from showing up blocking clear to get checks, and fairies
        [VariantsWithRoomMax(max: 1, 0x600, 0x800, 0x500, 0xFF00, 0x300)] // 5 is plenty
        Beamos = 0x47, // En_Vm 

        [FileID(90)]
        Demo_Effect = 0x48, // Demo_Effect

        [ActorizerEnabled]
        [FileID(91)]
        [ActorInstanceSize(0x1650)]
        [ActorInitVarOffset(0x1F78)]
        [ObjectListIndex(0x1)]// gameplay_keep obj 1
        // variety 01 in giants crashes if placed in goron shrine
        // variety 02 in moon, spawns a lighter version of 00, less particles, no whole chains
        // concerned that having 0 AND 2 would crash, like 0 and snow
        [FlyingVariants(0)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        // variety 0 crashes scenes with snow weather, but not rain, weird
        // also crashes in snowhead, if its in central room crash, if its in first room wont crash on warp, but does crash on walking in front door
        // grottos are fine though, so is fade out warp, so only certain scene transitions are an issue wtf
        // WARNING: the lens grotto might be an issue if you ever randomize something in there that can have free enemies, 
        //   spiders are currently not limited so never get trimmed
        [EnemizerScenesPlacementBlock(Scene.Snowhead, Scene.TwinIslands, Scene.MountainVillage, Scene.GoronVillage, Scene.PathToMountainVillage, Scene.PathToSnowhead,
            Scene.GoronShrine, Scene.MountainSmithy, Scene.GoronRacetrack,  // no snow, but entering from snowy area is also crash
            Scene.SnowheadTemple, Scene.WoodfallTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)] // with randomized dungeons, entering from snowy area
        Demo_Kankyo = 0x49, // lost woods living fairy dust

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3200)]
        [FileID(92)]
        [ObjectListIndex(0x9)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 3, 0)]
        FloorMaster = 0x4A, // En_Floormas

        Empty4B = 0x4B,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32A0)]
        [FileID(93)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0x7F07,0x7F05,0x7F06)]
        [VariantsWithRoomMax(max: 5, variant: 0x7F07, 0x7F05, 0x7F06)]
        [EnemizerScenesExcluded(Scene.IkanaCanyon, Scene.BeneathTheWell)] // gibdo locations, but share the same object so gets detected
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows us down too much
        ReDead = 0x4C, // En_Rd

        [FileID(94)]
        Bg_F40_Flift = 0x4D, // Bg_F40_Flift

        // MISSING FILE??
        Bg_Heavy_Block = 0x4E, // Bg_Heavy_Block

        [EnemizerEnabled]
        [FileID(95)]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        [GroundVariants(0x3323, 0x2324, 0x4324)] // bettles on the floor
        [FlyingVariants(0x2324, 0x4324)] // butterlies in the air
        [WaterVariants(0x6322)] // fish swimming in the water
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 2, 0x3323, 0x2324, 0x4324)]
        Bugs = 0x4F, // Obj_Mure // includes bugs and fish and butterflies

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
            0xFF3F, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B, 0xFF0F, 0xFFFC)]
        [PathingVariants(0xEF, 0x7F, 4, 0x55B, 0x637, 0x113, 0x91F, 0x909, 0xB0C, 0xC0F)]
        [PathingTypeVarsPlacement(mask: 0xFF00, shift: 8)]
        [VariantsWithRoomMax(max: 1, 
            0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F,
            0xFF3F, 0x317, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0x777, 0x57B, 0xFF0B, 0xFF0F, 0x11F)]
        [EnemizerScenesExcluded(Scene.SwampSpiderHouse, Scene.OceanSpiderHouse)] // dont remove old spiders, the new ones might not be gettable
        GoldSkullTula = 0x50, // En_Sw "Skullwalltulla"

        // hmm, this fights with demo_kankyo, need to think of a way to keep these two apart
        //[ActorizerEnabled] // wont snow with obj 1 or 0x1D8 might need weathertag
        //[ObjectListIndex(1)] // gameplay_keep 1
        [FileID(97)]
        [ObjectListIndex(0x1D8)] // the fuck is this object??
        [FlyingVariants(3)] // 3 is snow
        [GroundVariants(3)] // 3 is snow
        [UnkillableAllVariants]
        Weather = 0x51, // Object_Kankyo

        Empty52 = 0x52,
        Empty53 = 0x53,

        [FileID(98)]
        En_Horse_Link_Child = 0x54, // En_Horse_Link_Child

        [ActorizerEnabled]
        [ActorInstanceSize(0x194)]
        [ObjectListIndex(2)] // overworld_keep, obj 2
        //[ObjectListIndex(0x34)] // testing
        [FileID(99)]
        // FF/299 is HSG, 233 is path to snowhead, 3B is mountain village spring grot 3D is swamp grotto, , 5C is mystery woods
        // 96 is goron rock grotto, 218/2B8? is graveyard grotto, 3E is road to swamp
        // 301 is ranch grotto? 214 is log cow grotto
        // 0x2XX flag is hidden, 0x3XX is related travel (one of the exits in this area, thats how deku playground works)
        // 310 crashed though in one instance, probably too high of a 300 number
        // x000 is the grotto index, except 1000 breaks for some reason, while 0,2,3,4 work for the gossip grottos
        // 7 is hot spring water grotto, 6 is A regular chest grotto,
        // in vanilla its & 7 so only three bits are read, the fourth bit does not seem to be, but we can turn it on
        // 8 becomes straight jgrot, 9 is dong grot, A is vines jgrot
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Grottos can use Z Rotation to set addresses to places, instead of variables
        //   because of this, I am going to rewrite a new variant system that we will re-parse when needed
        //   so we can store rotation and more data
        //   If 0x0800 flag, then this is a regular grotto type with a chest, and 0x10XX is the chest flag for the chest it uses
        //     else, 0x02 can mean its hidden or not
        //       and 0x0_XX is the cave type otherwise, listed below:
        // so if 0x0 variant, rotation is used, where rotation is :
        // 0 is grassy gossip grotto, 1 is spider grotto, 2 is sandy grotto, 3 is water grotto, 
        // 4 is generic grotto, 5 is HSG, 6 is straight jgrot, 7 is dong grot, 8 is vine jgrot,
        // 9 field scrub, A is a cow groto, B is biobaba, C bean, D peahat, E+ is mayor's house (fake)
        [GroundVariants(0x0, 0x2000, 0x3000, 0x4000, // stone grottos
            0x7000, 0xC000, 0xE000, 0xF000, 0xD000, // regular grottos
            0x8200, 0xA200, // secret japanese grottos, hidden
            0x6233, 0x623B, 0x6218, 0x625C)] // grottos that might hold checks, also hidden
        [VariantsWithRoomMax(max: 1, 
            0x0, 0x2000, 0x3000, 0x4000, // stone grottos
            0x7000, 0xC000, 0xE000, 0xF000, 0xD000, // regular grottos
            0x8200, 0xA200, // secret japanese grottos, hidden
            0x6233, 0x623B, 0x6218, 0x625C)] // grottos that might hold checks, also hidden
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [EnemizerScenesExcluded(Scene.RoadToIkana, Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.PathToSnowhead)]
        // as its obj is 2, shouldn't be available in dungeons, maybe not indoors either
        [EnemizerScenesPlacementBlock(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        GrottoHole = 0x55, // Door_Ana

        Empty56 = 0x56,
        Empty57 = 0x57,
        Empty58 = 0x58,
        Empty59 = 0x59,
        Empty5A = 0x5A,

        [FileID(100)]
        En_Encount1 = 0x5B, // En_Encount1

        [FileID(101)]
        Demo_Tre_Lgt = 0x5C, // Demo_Tre_Lgt

        Empty5D = 0x5D,
        Empty5E = 0x5E,

        //[ActorizerEnabled] // broken: crash, does NOT use paths
        // thought it might be that the actor's memory is just huge because it has 20 effects, but we measure that now // nope still crash
        [FileID(102)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(1)]
        [UnkillableAllVariants]
        MajoraBalloonSewer = 0x5F, // En_Encount2

        [FileID(103)]
        En_Fire_Rock = 0x60, // En_Fire_Rock

        [FileID(104)]
        Bg_Ctower_Rot = 0x61, // Bg_Ctower_Rot

        [FileID(105)]
        Mir_Ray = 0x62, // Mir_Ray


        Empty63 = 0x63,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [FileID(106)]
        [ObjectListIndex(0x8E)]
        [WaterVariants(0)] // works on ground too but cannot add ground without ground enemies showing up in fish tank
        Shellblade = 0x64, // En_Sb

        [FileID(107)]
        En_Bigslime = 0x65, // En_Bigslime

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [FileID(108)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1,2)] // both stiff and mini
        [UnkillableAllVariants] // they grow back, dont count as killable
        DekuBabaWithered = 0x66, // En_Karebaba

        // ingo?
        [FileID(109)]
        En_In = 0x67, // En_In

        Empty68 = 0x68,

        [ActorizerEnabled] // works but her object is huge, and you cant talk or interact with her, which kinda sucks
        [ActorInstanceSize(0x314)]
        [FileID(304)]
        [ObjectListIndex(0xA2)]
        // her code takes her params and passes it to another func as (params * 0x7E00 >> 9)
        // 200 does not spawn? 400 is slightly tilted to one side (might be the leever actually)
        // 800 also does not spawn
        //[GroundVariants(0x0A00)]//(0x7E00)]
        [WaterVariants(0)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        Ruto = 0x69, // En_Ru

        [FileID(110)]
        En_Bom_Chu = 0x6A, // En_Bom_Chu

        [FileID(111)]
        En_Horse_Game_Check = 0x6B, // En_Horse_Game_Check

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2330)]
        [FileID(112)]
        [ObjectListIndex(0xAB)]
        // 2 is ocean bottom, 0 is one in shallow shore water, 3 is land and one in shallow water
        [WaterVariants(0,2)]
        [GroundVariants(3)]
        [VariantsWithRoomMax(max: 3, variant: 0,2)]
        [VariantsWithRoomMax(max: 6, variant: 3)]
        //[GroundVariants(3)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows down the race
        LikeLike = 0x6C,

        //[EnemizerEnabled] // we dont actually want this detected automatically, this will be added per-likelike manually
        [ObjectListIndex(0xB3)] // this is really the shield, we're using it as the second likelike object
        LikeLikeShield = 0x28E, // 28E is a dummy actor ID, we only use it because it will never conflict with enemizer

        Empty6D = 0x6D,
        Empty6E = 0x6E,
        Empty6F = 0x6F,
        Empty70 = 0x70,
        Empty71 = 0x71,
        Empty72 = 0x72,

        [FileID(113)]
        En_Fr = 0x73, // En_Fr

        Empty74 = 0x74,
        Empty75 = 0x75,
        Empty76 = 0x76,
        Empty77 = 0x77,
        Empty78 = 0x78,

        //[ActorizerEnabled]
        [ObjectListIndex(0x124)]
        [FileID(114)]
        // params: FFFF is the main all-in-one
        // 100-MAX is per-single fish
        // 200 is something, it causes splashes you can hear but if you get close to it it crashes
        [GroundVariants(0xFFFF)] // assumption, the main vars will be the man in the hat
        [WaterVariants(0xFFFF)] // assumption, the main vars will be the man in the hat
        [UnkillableAllVariants]
        OOTFishing = 0x79,

        [FileID(115)]
        [ObjectListIndex(0003)]
        Obj_Oshihiki = 0x7A, // Obj_Oshihiki

        [FileID(116)]
        [ObjectListIndex(0001)]
        Eff_Dust = 0x7B, // Eff_Dust

        [FileID(117)]
        [ObjectListIndex(0001)]
        Bg_Umajump = 0x7C, // Bg_Umajump

        [FileID(122)]
        [ObjectListIndex(0001)]
        Arrow_Fire = 0x7D, // Arrow_Fire

        [FileID(123)]
        [ObjectListIndex(0001)]
        Arrow_Ice = 0x7E, // Arrow_Ice

        [FileID(124)]
        [ObjectListIndex(0001)]
        Arrow_Light = 0x7F, // Arrow_Light

        [FileID(121)]
        [ObjectListIndex(0001)]
        Item_Etcetera = 0x80, // Item_Etcetera

        [FileID(125)]
        [ObjectListIndex(0001)]
        Obj_Kibako = 0x81, // Obj_Kibako


        [ActorizerEnabled]
        // this is marked 2 and not 1 because 0x100 pots dont spawn in dungeons
        //[ObjectListIndex(0x1)] // this is a lie, the pot DETECTS multiple objects but does NOT exist in gameplay keep
        [ObjectListIndex(0xF9)]
        // 0xF9 is pot and pot shard
        [FileID(126)]
        // according to CM, 0x100 is available everywhere as a pot, where 0x3F defines the drop item
        // so 1F is arrows, F is magic, B is three small rups? 7 is huge 200 rup, 17 is empty
        // 0xA is one rup, 1A and 14 are empty 04 is 20 rupes, 
        // 100 is empty, 110 is fairy, 10E is stick//////
        // 115 is 5 bombs, 105 is tall dodongo 50 rup, 106 is empty, 116 is empty
        // 101 is one rup, 111 SKULL TOKEN POT??!? 102 was 5 rups 112 empty
        // 103 empty, 113 is 10 deku nuts, 104 is red rup, 114 is empty
        //[GroundVariants(0x110)] // testing // 115 101 106 10E 10F
        [GroundVariants(0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E, // good variety
            0x4110)] // TF pot
        [EnemizerScenesExcluded(Scene.MajorasLair, Scene.RoadToIkana)]
        [UnkillableAllVariants]
        ClayPot = 0x82, // Obj_Tsubo

        Empty83 = 0x83,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x26EC)]
        [FileID(127)]
        [ObjectListIndex(0xD8)]
        [GroundVariants(0xFF03, 0xFF02, 0xFF01)]
        [VariantsWithRoomMax(max:4, 0xFF03, 0xFF02, 0xFF01)]
        IronKnuckle = 0x84, // En_Ik

        Empty85 = 0x85,
        Empty86 = 0x86,
        Empty87 = 0x87,
        Empty88 = 0x88,

        [FileID(128)]
        [ObjectListIndex(0153)]
        Demo_Shd = 0x89, // Demo_Shd

        [FileID(129)]
        [ObjectListIndex(0134)]
        En_Dns = 0x8A, // En_Dns

        [FileID(130)]
        [ObjectListIndex(0001)]
        Elf_Msg = 0x8B, // Elf_Msg

        [FileID(131)]
        [ObjectListIndex(0003)]
        En_Honotrap = 0x8C, // En_Honotrap

        [EnemizerEnabled]
        [FileID(132)]
        [ActorInitVarOffset(0xC5C)]
        [ObjectListIndex(3)] // dungeon_keep, obj 3
        // and object 3 is so massive it never gets chosen even if we try to shove it into the object list
        //[GroundVariants(0x4015)]
        // 0 works, always empty
        [GroundVariants(0x115, 0x101, 0x106, 0x10E, 0x10F)] // actually spawns thank god, only in dungeons though, but outside its just an empty space so thats fine
        [UnkillableAllVariants] // unknown, harder to test since its a free enemy
        FlyingPot = 0x8D, // En_Tubo_Trap

        [FileID(133)]
        [ObjectListIndex(0001)]
        Obj_Ice_Poly = 0x8E, // Obj_Ice_Poly

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [FileID(134)]
        [ObjectListIndex(0xE4)]
        [GroundVariants(02, 0x2001, 0x300F, 0x100F)]
        [VariantsWithRoomMax(max: 2, variant: 02, 0x2001, 0x300F, 0x100F)] // 8 is more than enough
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // Slowing enemies
        Freezard = 0x8F, // En_Fz

        [EnemizerEnabled]
        [ActorInstanceSize(0x19C)]
        [FileID(135)]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        // 1 creates a grass circle in termina field, 0 is grotto grass single
        // 642B is a smaller cuttable grass from the ground in secret shrine
        [GroundVariants(0, 1)]
        [UnkillableAllVariants] // not enemy actor group
        [EnemizerScenesExcluded(Scene.Grottos)] // dont remove from peahat grotto
        GrassBush = 0x90, // En_Kusa

        [FileID(136)]
        [ObjectListIndex(0xEE)]
        Obj_Bean = 0x91, // Obj_Bean

        [ActorizerEnabled]
        [FileID(137)]
        [ObjectListIndex(0x12A)]
        [GroundVariants(0x807F, 0x8004, 0x8002)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [FlyingVariants(0x807F, 0x8004, 0x8002)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [VariantsWithRoomMax(max: 3, variant: 0x807F, 0x8004)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x7000, 0xC000, 0xE000, 0xF000, 0xD000)] // regular unhidden grottos
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        [EnemizerScenesExcluded(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.Grottos)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // blocking enemies
        Bombiwa = 0x92, // Obj_Bombiwa

        [FileID(138)]
        [ObjectListIndex(3)]
        Obj_Switch = 0x93, // Obj_Switch


        Empty94 = 0x94, // Empty94

        [FileID(139)]
        [ObjectListIndex(0xED)]
        Obj_Lift = 0x95, // Obj_Lift

        [FileID(140)]
        [ObjectListIndex(0xEC)]
        Obj_Hsblock = 0x96, // Obj_Hsblock

        [FileID(141)]
        [ObjectListIndex(0001)]
        En_Okarina_Tag = 0x97, // En_Okarina_Tag

        Empty98 = 0x98,

        [FileID(142)]
        [ObjectListIndex(0xEF)]
        En_Goroiwa = 0x99, // En_Goroiwa

        Empty9A = 0x9A,
        Empty9B = 0x9B,


        [ActorizerEnabled]
        [ObjectListIndex(0xF1)]
        [FileID(143)]
        // 1 scoffing at poster, 2 is shouting at the sky looker
        // 0X03 is a walking type
        [GroundVariants(1, 2)]
        [PathingVariants(0x603, 0x503)]
        [PathingTypeVarsPlacement(mask:0xFF00, shift:8)]
        [UnkillableAllVariants]
        Carpenter = 0x9C, // En_Daiku

        //[ActorizerEnabled] // does not spawn because code checks for chicken object
        [FileID(144)]
        [ObjectListIndex(0xF2)]
        [GroundVariants(0x0FFF)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.CuccoShack)]
        CuccoChick = 0x9D,

        [FileID(145)]
        [ObjectListIndex(0001)]
        Item_Inbox = 0x9E, // Item_Inbox

        [FileID(146)]
        [ObjectListIndex(0xE6)]
        En_Ge1 = 0x9F, // En_Ge1

        [FileID(147)]
        [ObjectListIndex(0001)]
        Obj_Blockstop = 0xA0, // Obj_Blockstop

        [FileID(148)]
        [ObjectListIndex(0001)]
        En_Sda = 0xA1, // En_Sda

        // in MM this is NOT arwing, its an multi-use effect
        // multiple explosion visual effects, light arrows, stuff like that
        [FileID(149)]
        [ObjectListIndex(0x1)]
        En_Clear_Tag = 0xA2, // En_Clear_Tag

        EmptyA3 = 0xA3,

        [ActorizerEnabled]
        [FileID(150)]
        [ObjectListIndex(0x248)]
        [GroundVariants(3, 0xFF, 0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.MilkBar)]
        Gorman = 0xA4, // En_Gm

        [ActorizerEnabled]
        [FileID(151)]
        [ObjectListIndex(0xF4)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Grottos)]
        BeanSeller = 0xA5,

        [FileID(152)]
        [ObjectListIndex(0xF5)]
        En_Hs = 0xA6, // En_Hs

        [FileID(153)]
        [ObjectListIndex(0x17F)]
        Bg_Ingate = 0xA7, // Bg_Ingate

        [FileID(154)]
        [ObjectListIndex(0xFC)]
        En_Kanban = 0xA8, // En_Kanban

        EmptyA9 = 0xA9,

        // these are temporary, spawned by En_Niw
        [FileID(155)]
        [ObjectListIndex(0xF)]
        En_Attack_Niw = 0xAA, // En_Attack_Niw

        EmptyAB = 0xAB,
        EmptyAC = 0xAC,
        EmptyAD = 0xAD,

        [ActorizerEnabled]
        [FileID(156)]
        [ObjectListIndex(0xFE)]
        [GroundVariants(0xFFFF)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [EnemizerScenesExcluded(Scene.MarineLab)]
        Scientist = 0xAE, // En_Mk

        [FileID(157)]
        [ObjectListIndex(0xFD)]
        En_Owl = 0xAF, // En_Owl

        [EnemizerEnabled]
        [FileID(158)]
        [ActorInstanceSize(0x198)]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1 // the rocks are free, you can take them home
        //6a does not load
        [GroundVariants(0x1F2)] // get bugs if pickup, best version
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        [EnemizerScenesExcluded(Scene.TerminaField)] // dont replace them in TF
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x3F5F)]
        Rock = 0xB0, // En_Ishi

        [FileID(159)]
        [ObjectListIndex(0x1BA)]
        Obj_Hana = 0xB1, // Obj_Hana

        [ActorizerEnabled]
        [FileID(160)]
        [ObjectListIndex(0xF7)]
        // xx30 is fake type, burn away if you hit them
        // we're NOT going to be includeing the flipping switches, in part because they are invisble and you wouldn't know they are there anyway
        // (they use the scene wall as their shape/texture)
        [WallVariants(0xB00, 0x1200, 0x1130)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.StoneTower, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple, Scene.SecretShrine)]
        SunSwitch = 0xB2, // Obj_Lightswitch

        [EnemizerEnabled]
        [FileID(161)]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1
        // 801, opening scene grass, 0x1FXX are ranch and TF
        // 0402 is ikana graveyard rock circle
        [GroundVariants(0x801, 0x1F02, 0x1F00, 0x0402)]
        [AlignedCompanionActor(Bugs, CompanionAlignment.Above, ourVariant: -1,
            variant: 0x2324, 0x4324)] // butterflies over the bushes
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.OnTop, ourVariant: 0402,
            variant: 0x8200, 0xA200, // secret japanese grottos, hidden
            0x6233, 0x623B, 0x6218, 0x625C)] // grottos that might hold checks, also hidden
        [UnkillableAllVariants]
        GrassRockCluster = 0xB3, // Obj_Mure2

        EmptyB4 = 0xB4,

        [FileID(162)]
        [ObjectListIndex(0x140)]
        HoneyAndDarling = 0xB5, // En_Fu

        EmptyB6 = 0xB6,
        EmptyB7 = 0xB7,

        [FileID(163)]
        [ObjectListIndex(0x106)]
        En_Stream = 0xB8, // En_Stream

        //[ActorizerEnabled] // does not load, wrong vars?
        [FileID(164)]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1
        [GroundVariants(1)] // neither 0 nor 1 work
        [UnkillableAllVariants]
        //[EnemizerScenesExcluded(Scene.GoronShrine)]
        RockSirloin = 0xB9, // En_Mm

        EmptyBA = 0xBA,
        EmptyBB = 0xBB,

        [ObjectListIndex(0x1)] // gamplay_keep obj 1
        [FileID(165)]
        // variants: 607 is the rain in road to southern swamp, which gets rainier as you approach swamp and dry toward termina field
        //  same for milk road, both are right next to the entrance that becomes more rainy
        // var 1 southern swamp (clear), 
        // a04: romani ranch, a36: zora cape/coast
        WeatherTag = 0xBC, // En_Weather_Tag

        [ActorizerEnabled]
        [FileID(166)]
        [ObjectListIndex(0xC2)]
        // 0 does nothing just stands there and stares at you
        // 1 is climbing in the tree trying to get rups
        [GroundVariants(0)]
        //[WallVariants(1)] // facing the wrong way and no bonk, so not that interesting
        [VariantsWithRoomMax(max:3, variant:0)]
        [EnemizerScenesPlacementBlock(Scene.RomaniRanch, Scene.Woodfall, Scene.DekuShrine)]
        [UnkillableAllVariants]
        En_Ani = 0xBD, // En_Ani

        EmptyBE = 0xBE,

        //[ActorizerEnabled] // warp addresses are offsets, dangerous until we can hard code
        [FileID(167)]
        [ObjectListIndex(0x271)]
        [PathingVariants(0x11, 0x422, 0x833, 0xC44)]
        [PathingTypeVarsPlacement(mask:0xFC00, shift:10)]
        [VariantsWithRoomMax(max:1, variant: 0x11, 0x422, 0x833, 0xC44)]
        [PathingKickoutAddrVarsPlacement(mask: 0x3F0, shift: 4)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.TheMoon)]
        MoonChild = 0xBF, // En_Js

        EmptyC0 = 0xC0,
        EmptyC1 = 0xC1,
        EmptyC2 = 0xC2,
        EmptyC3 = 0xC3,

        [FileID(168)]
        [ObjectListIndex(0x1)]
        En_Okarina_Effect = 0xC4, // En_Okarina_Effect

        //[ActorizerEnabled] // yeah it just takes you back to file select if you hit two buttons, bad
        [FileID(169)]
        [ObjectListIndex(0x115)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        TitleLogo = 0xC5, // En_Mag

        [FileID(170)]
        [ObjectListIndex(0x1)]
        Elf_Msg2 = 0xC6, // Elf_Msg2

        [FileID(171)]
        [ObjectListIndex(0x5C)]
        Bg_F40_Swlift = 0xC7, // Bg_F40_Swlift

        EmptyC8 = 0xC8,
        EmptyC9 = 0xC9,

        [ActorizerEnabled]
        [FileID(172)]
        [ActorInstanceSize(0x2A0)]
        [ObjectListIndex(0x11D)]
        //0x1200, 0x1B00, 0x2800 spawns from the ground after you play a song
        // versions: 1200, 1B00, 2800 shows up a lot, 2D00 stonetower, 3200 zora cape
        // trading post version is 1
        // wish I could spawn the ones that dance so they are always dancing when the player gets there
        [GroundVariants(1)]
        [VariantsWithRoomMax(max: 5, variant: 1)]
        [UnkillableAllVariants]
        // crash: if you teach song to him in TF the ice block cutscene triggers
        [EnemizerScenesPlacementBlock(Scene.TerminaField)]
        [EnemizerScenesExcluded(Scene.TradingPost)]//, Scene.AstralObservatory)] // re-disable this if playing Entrando
        Scarecrow = 0xCA, // En_Kakasi

        [FileID(173)]
        [ObjectListIndex(0x1)]
        Obj_Makeoshihiki = 0xCB, // Obj_Makeoshihiki

        [FileID(174)]
        [ObjectListIndex(0x1)]
        Oceff_Spot = 0xCC, // Oceff_Spot

        EmptyCD = 0xCD,

        //[ActorizerEnabled] // does not spawn, grotto does weird stuff to it, use TreasureChest instead
        [FileID(175)]
        [ObjectListIndex(0xC)] //same as chest, but with weird requirements
        //[GroundVariants(0)] // only option, looks like grotto params are saved and used instead
        [UnkillableAllVariants]
        GrottoChest = 0xCE, // En_Torch

        EmptyCF = 0xCF,

        [FileID(176)]
        [ObjectListIndex(0x1)]
        Shot_Sun = 0xD0, // Shot_Sun

        EmptyD1 = 0xD1,
        EmptyD2 = 0xD2,

        [FileID(177)]
        [ObjectListIndex(0x1)]
        Obj_Roomtimer = 0xD3, // Obj_Roomtimer

        [FileID(178)]
        [ObjectListIndex(0x127)]
        En_Ssh = 0xD4, // En_Ssh

        EmptyD5 = 0xD5, // EmptyD5

        [FileID(179)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe = 0xD6, // Oceff_Wipe

        [FileID(209)]
        [ObjectListIndex(0x1)]
        Oceff_Storm = 0xD7, // Oceff_Storm

        [FileID(210)]
        [ObjectListIndex(0x1)]
        Obj_Demo = 0xD8, // Obj_Demo

        [FileID(211)]
        [ObjectListIndex(0x128)]
        En_Minislime = 0xD9, // En_Minislime

        [FileID(212)]
        [ObjectListIndex(0x1)]
        En_Nutsball = 0xDA, // En_Nutsball

        EmptyDB = 0xDB,
        EmptyDC = 0xDC,
        EmptyDD = 0xDD,
        EmptyDE = 0xDE,

        [FileID(213)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe2 = 0xDF, // Oceff_Wipe2

        [FileID(214)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe3 = 0xE0, // Oceff_Wipe3

        EmptyE1 = 0xE1,

        [ActorizerEnabled]
        [FileID(215)]
        [ObjectListIndex(0x132)]
        // 3FF is SCT dog, 0x22BF is spiderhouse dog, makes no sense if use mask
        // 0xD9F is old ranch dog WORKS, racetrack dogs unknown, spawned by the game
        // in mamuyan: (phi_s0 << 5) | (arg0->unk1C & 0x7E00) unk1C seems static
        // from mamuyan code we know the colors are 1-E shifted right by 5
        // colors: (white, brown, dark grey, bluedog, gold)
        // 0x001F params are unknown, they aren't checked in init
        // D9F crashes in road to southernswamp turned into 19F
        //[GroundVariants(0xFC20)] // testing max path is no path // does not spawn, sad
        [GroundVariants(0x20, 0x40, 0x60, 0x80, 0x120,
            0x3FF, 0x19F, 0x02BF)]
        [PathingVariants(0x19F, 0xD9F, 0x3FF, 0x22BF,
            0x20, 0x40, 0x60, 0x80, 0x120)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift:10)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:2, 0x3FF, 0x19F, 0x02BF, 0x20, 0x40, 0x60, 0x80, 0x120)] // this many dogs is enough honestly
        [EnemizerScenesExcluded(Scene.RanchBuildings, Scene.RomaniRanch, Scene.SouthClockTown)]//, Scene.SwampSpiderHouse)]
        // dog safe areas: TF, roadtoSS, SS, SSC, deku palace, sspiderhouse
        // path to mountain village
        // now that I know what the path vars is, any area with at least one path per room should be safe
        // these used to be banned, but we should be able to use them now:
        //DekuShrine RoadToIkana GoronVillage
        [EnemizerScenesPlacementBlock(Scene.ClockTowerInterior, // cursed if put on hms
            Scene.Woodfall, // they fall off into the water and quietly swim, lame?
            Scene.MountainVillageSpring, Scene.RanchBuildings)]
        Dog = 0xE2, // En_Dg

        [FileID(216)]
        [ObjectListIndex(0x20)]
        En_Si = 0xE3, // En_Si

        [ActorizerEnabled]
        [FileID(217)]
        [ObjectListIndex(0x1B9)]
        [WallVariants(0x81, 0x82, 0x83)]
        [EnemizerScenesExcluded(Scene.WoodfallTemple, Scene.Grottos, Scene.SwampSpiderHouse, Scene.SouthernSwamp, Scene.PiratesFortressRooms)]
        [UnkillableAllVariants]
        HoneyComb = 0xE4, // Obj_Comb

        [FileID(218)]
        [ObjectListIndex(0x133)]
        LargeCrate = 0xE5, // Obj_Kibako2

        EmptyE6 = 0xE6, // EmptyE6

        [FileID(219)]
        [ObjectListIndex(0x1)]
        En_Hs2 = 0xE7, // En_Hs2

        [FileID(220)]
        [ObjectListIndex(0x1)]
        Obj_Mure3 = 0xE8, // Obj_Mure3

        [FileID(221)]
        [ObjectListIndex(0x140)]
        En_Tg = 0xE9, // En_Tg

        EmptyEA = 0xEA,
        EmptyEB = 0xEB,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3E40)]
        [FileID(222)]
        [ObjectListIndex(0x141)]
        //01 is winter coat, 0x800 is with ice block
        // ice block versions are limited because they are complicated collision and really long draw distance
        [GroundVariants(0xFF01, 0xFF81, 0xFF00, 0xFF80)]
        [VariantsWithRoomMax(max: 2, variant: 0xFF81)]
        [VariantsWithRoomMax(max: 1, variant: 0xFF80)]
        Wolfos = 0xEC, // En_Wf

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D60)]
        [FileID(223)]
        [ObjectListIndex(0x142)]
        // 0x0042 is swinging from tree, looks stupid if spawns in the ground, 22 is sitting on the edge of a bookcase, looks weird on the ground
        [GroundVariants(0x0032)]
        [CompanionActor(Flame, variant: 0x7F4)] // they like fire in this game
        [EnemizerScenesExcluded(Scene.IkanaGraveyard, Scene.OceanSpiderHouse)]
        //[UnkillableVariants(0x32)] the ones that circle the tombs, but dont respawn if placed anywhere else it seems, ignore
        Stalchild = 0xED,

        EmptyEE = 0xEE,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x28F0)]
        [FileID(224)]
        [ObjectListIndex(0x143)]
        // all moon mask hints, which are free but since they are mask hints they are often worthless
        [GroundVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62)]
        [WallVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.SouthernSwamp, Scene.MilkRoad,
            Scene.RomaniRanch, Scene.IkanaCanyon, Scene.LinkTrial)]
        [EnemizerScenesPlacementBlock(Scene.ClockTowerInterior)] // crash
        GossipStone = 0xEF,

        [FileID(225)]
        [ObjectListIndex(0x1)]
        Obj_Sound = 0xF0, // Obj_Sound

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [FileID(226)]
        [ObjectListIndex(0x6)]
        [FlyingVariants(0, 1)]
        [RespawningVariants(0, 1)] // weirdly, all versions of regular guay are respawning
        [VariantsWithRoomMax(max: 8, variant: 0, 1)]
        Guay = 0xF1,

        EmptyF2 = 0xF2,

        [ActorizerEnabled]
        [FileID(227)]
        [ObjectListIndex(0x146)]
        [GroundVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [WallVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RanchBuildings, Scene.RomaniRanch, Scene.Grottos, Scene.BeneathTheWell)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // blocking enemy
        Cow = 0xF3,

        EmptyF4 = 0xF4,
        EmptyF5 = 0xF5,

        [FileID(228)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe4 = 0xF6, // Oceff_Wipe4

        [ObjectListIndex(0x0)]
        EmptyF7 = 0xF7, // EmptyF7

        // unused walking zora
        [ActorizerEnabled]
        [FileID(229)]
        [ObjectListIndex(0xD0)]
        // hmm, params are 0x7E00 >> 9 and thats it. path?
        // looks like -1 (7E) works as a path disable for this actor too
        [GroundVariants(0x7E00)]
        [WaterVariants(0x7E00)]
        [PathingVariants(0x0)]
        [PathingTypeVarsPlacement(mask: 0x7F00, shift: 9)]
        [UnkillableAllVariants]
        MuteZora = 0xF8, // En_Zo

        [FileID(231)]
        [ObjectListIndex(0x1)]
        Obj_Makekinsuta = 0xF9, // Obj_Makekinsuta

        //[ActorizerEnabled] // she kicks you out like guards but without caring about direction/proximity
        [FileID(232)]
        [ObjectListIndex(0x130)]
        [GroundVariants(0xCB1)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.PiratesFortressRooms)]
        Aviel = 0xFA, // En_Ge3, the Pirate Leader

        EmptyFB = 0xFB,

        [FileID(233)]
        [ObjectListIndex(0x2)]
        Obj_Hamishi = 0xFC, // Obj_Hamishi

        [FileID(234)]
        [ObjectListIndex(0x192)]
        En_Zl4 = 0xFD, // En_Zl4

        //[ActorizerEnabled] // we dont want as an actual actor, we want as a companion
        // why is the letter of all things in gameplay_keep? maybe its the same texture of LTK?
        [FileID(235)]
        [ObjectListIndex(0x1CB)] // gameplay_keep obj 1
        [GroundVariants(0)]
        [UnkillableAllVariants]
        LetterToPostman = 0xFE,

        EmptyFF = 0xFF,

        [FileID(236)]
        [ObjectListIndex(0x1)]
        Door_Spiral = 0x100, // Door_Spiral

        Empty101 = 0x101,

        [FileID(237)]
        [ObjectListIndex(0x1)]
        Obj_Pzlblock = 0x102, // Obj_Pzlblock

        //[ActorizerEnabled] // doesnt spawn without 2 node path, if you remove the code to allow for more:crash
        [FileID(238)]
        [ObjectListIndex(0x64)]
        [PathingVariants(0x405, 0x406, 0x407, 0x408)]
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [UnkillableAllVariants]
        ThornTrap = 0x103, // Obj_Toge "Thorn"

        Empty104 = 0x104,

        [FileID(239)]
        [ObjectListIndex(0x30)]
        Obj_Armos = 0x105, // Obj_Armos

        [ActorizerEnabled]
        [FileID(240)]
        [ObjectListIndex(0x14F)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max: 5, variant:0)] // culling distance is too long
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall)]
        [UnkillableAllVariants]
        Bumper = 0x106, // Obj_Boyo

        Empty107 = 0x107,
        Empty108 = 0x108,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [FileID(241)]
        [ObjectListIndex(0x14E)]
        [FlyingVariants(0,2,3)]
        [VariantsWithRoomMax(max: 2, 0,2,3)]
        DragonFly = 0x109, //En_Grasshopper

        Empty10A = 0x10A,

        [FileID(242)]
        [ObjectListIndex(0x2)]
        Obj_Grass = 0x10B, // Obj_Grass

        [FileID(243)]
        [ObjectListIndex(0x2)]
        Obj_Grass_Carry = 0x10C, // Obj_Grass_Carry

        [FileID(244)]
        [ObjectListIndex(0x2)]
        Obj_Grass_Unit = 0x10D, // Obj_Grass_Unit

        Empty10E = 0x10E, // Empty10E
        Empty10F = 0x10F, // Empty10F

        [FileID(245)]
        [ObjectListIndex(0x153)]
        Bg_Fire_Wall = 0x110, // Bg_Fire_Wall

        [FileID(246)]
        [ObjectListIndex(0x1)]
        En_Bu = 0x111, // En_Bu

        //[EnemizerEnabled] //crash
        //[ActorInitVarOffset(0x3688)]
        [FileID(247)]
        //[ObjectListIndex(0x23B)]
        [GroundVariants(0x2243)]
        En_Encount3 = 0x112, // En_Encount3

        [FileID(248)]
        [ObjectListIndex(0x155)]
        [GroundVariants(0x2243)]
        Garo = 0x113, // En_Jso

        [FileID(249)]
        [ObjectListIndex(0xED)]
        Obj_Chikuwa = 0x114, // Obj_Chikuwa

        // TODO would not spawn because I mistyped the ID try again
        //[EnemizerEnabled] // wont spawn, just spawns green tatl points but no actual knight
        [ObjectListIndex(0x156)]
        [FileID(250)]
        // params 0 vanilla
        // init checks if 64, C8, CA, 23
        [GroundVariants(0x23)]
        [UnkillableAllVariants] // assumption: need mirror shield
        SkeleKnight = 0x115, // En_Knight

        // nvm not sure what this is
        [FileID(251)]
        // in actor obj set to 1, so might be multiple types
        [ObjectListIndex(0x3E)]
        //[GroundVariants(0x83C1)]
        [GroundVariants(0x3C1)]
        [EnemizerScenesExcluded(Scene.GoronTrial)]
        [UnkillableAllVariants]
        En_Warp_tag = 0x116, // En_Warp_tag

        [FileID(252)]
        [ObjectListIndex(0xD7)]
        En_Aob_01 = 0x117, // En_Aob_01
        [FileID(253)]
        [ObjectListIndex(0x1)]
        En_Boj_01 = 0x118, // En_Boj_01
        [FileID(254)]
        [ObjectListIndex(0x1)]
        En_Boj_02 = 0x119, // En_Boj_02
        [FileID(255)]
        [ObjectListIndex(0x1)]
        En_Boj_03 = 0x11A, // En_Boj_03

        [FileID(256)]
        [ObjectListIndex(0x1)]
        En_Encount4 = 0x11B, // En_Encount4
        [FileID(257)]
        [ObjectListIndex(0x110)]
        En_Bom_Bowl_Man = 0x11C, // En_Bom_Bowl_Man
        [FileID(258)]
        [ObjectListIndex(0x1AC)]
        En_Syateki_Man = 0x11D, // En_Syateki_Man

        Empty11E = 0x11E,

        [FileID(259)]
        [ObjectListIndex(0x157)]
        Bg_Icicle = 0x11F, // Bg_Icicle

        [FileID(260)]
        [ObjectListIndex(0x6)]
        En_Syateki_Crow = 0x120, // En_Syateki_Crow

        [FileID(261)]
        [ObjectListIndex(0x1)]
        En_Boj_04 = 0x121, // En_Boj_04

        [FileID(262)]
        [ObjectListIndex(0x1)]
        En_Cne_01 = 0x122, // En_Cne_01

        [FileID(263)]
        [ObjectListIndex(0x1)]
        En_Bba_01 = 0x123, // En_Bba_01

        [FileID(264)]
        [ObjectListIndex(0xDE)]
        En_Bji_01 = 0x124, // En_Bji_01

        [FileID(265)]
        [ObjectListIndex(0x158)]
        Bg_Spdweb = 0x125, // Bg_Spdweb

        Empty126 = 0x126,
        Empty127 = 0x127,

        [FileID(266)]
        [ObjectListIndex(0x1)]
        En_Mt_tag = 0x128, // En_Mt_tag

        //[EnemizerEnabled]
        [FileID(267)]
        [ObjectListIndex(0x15A)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max:1, variant:0)]
        [EnemizerScenesExcluded(Scene.OdolwasLair)]
        Odolwa = 0x129, // En_Boss01

        [FileID(268)]
        [ObjectListIndex(0x15B)]
        Boss_02 = 0x12A, // Boss_02
        [FileID(269)]
        [ObjectListIndex(0x15C)]
        Boss_03 = 0x12B, // Boss_03
        [FileID(270)]
        [ObjectListIndex(0x15D)]
        Boss_04 = 0x12C, // Boss_04

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3760)]
        [FileID(271)]
        [ObjectListIndex(0x15E)]
        // 0x1 is the one that hangs from the ceiling in GBT
        // TODO if I get wall sideways working with dexihand, do it for baba too
        [WaterVariants(04,02,0)]
        [EnemizerScenesExcluded(Scene.GreatBayTemple)] // need their lilipads to reach compass chest and fairy chest
        BioDekuBaba = 0x12D, // Boss_05

        //[EnemizerEnabled]
        [FileID(272)]
        [ObjectListIndex(0x156)]
        //[GroundVariants(0)]
        [UnkillableAllVariants]
        KingIkanaController = 0x12E, //Boss_06

        [FileID(273)]
        [ObjectListIndex(0x160)]
        Boss_07 = 0x12F, // Boss_07

        [FileID(274)]
        [ObjectListIndex(0x8)]
        Bg_Dy_Yoseizo = 0x130, // Bg_Dy_Yoseizo

        Empty131 = 0x131,

        [FileID(275)]
        [ObjectListIndex(0x1)]
        En_Boj_05 = 0x132, // En_Boj_05

        Empty133 = 0x133,
        Empty134 = 0x134,

        //[ActorizerEnabled] // wont spawn because the required item objects are likely missing
        [FileID(276)]
        [ObjectListIndex(0xD0)]
        [GroundVariants(0x3E0)]
        [UnkillableAllVariants]
        ZoraSeller = 0x135, // En_Sob1

        Empty136 = 0x136,
        Empty137 = 0x137,

        //[ActorizerEnabled] // field of effect is so HUG
        [FileID(277)]
        [ObjectListIndex(0xA1)]
        [ActorInstanceSize(0xB78)]
        // 8 is smithy goron, 0x7F84, 0x7F
        [GroundVariants(0x8, 0x7FE2)]
        [EnemizerScenesExcluded(Scene.GoronVillage, Scene.GoronVillageSpring)]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x3F5F)]
        GenericGoron = 0x138, // En_Go

        Empty139 = 0x139,

        //[EnemizerEnabled] // todo: try randomizing
        [FileID(278)]
        [ObjectListIndex(0x161)]
        Raft = 0x13A,// carniverous raft, woodfall

        [FileID(279)]
        [ObjectListIndex(0x162)]
        Obj_Funen = 0x13B, // Obj_Funen

        [FileID(280)]
        [ObjectListIndex(0x163)]
        Obj_Raillift = 0x13C, // Obj_Raillift

        [FileID(281)]
        [ObjectListIndex(0x164)]
        Bg_Numa_Hana = 0x13D, // Bg_Numa_Hana


        //[ActorizerEnabled] // weirdly huge actor, and boring
        [FileID(282)]
        [ObjectListIndex(0x165)]
        [UnkillableAllVariants]
        PottedPlant = 0x13E,

        [FileID(283)]
        [ObjectListIndex(0x166)]
        Obj_Spinyroll = 0x13F, // Obj_Spinyroll

        [FileID(284)]
        [ObjectListIndex(0x1CC)]
        Dm_Hina = 0x140, // Dm_Hina

        [FileID(285)]
        [ObjectListIndex(0x141)]
        En_Syateki_Wf = 0x141, // En_Syateki_Wf

        [FileID(286)]
        [ObjectListIndex(0x3)]
        Obj_Skateblock = 0x142, // Obj_Skateblock

        [FileID(288)]
        [ObjectListIndex(0x167)]
        Obj_Iceblock = 0x143, // Obj_Iceblock

        [FileID(289)]
        [ObjectListIndex(0x1A6)]
        En_Bigpamet = 0x144, // En_Bigpamet

        [FileID(291)]
        [ObjectListIndex(0x40)]
        En_Syateki_Dekunuts = 0x145, // En_Syateki_Dekunuts

        [FileID(292)]
        [ObjectListIndex(0x1)]
        Elf_Msg3 = 0x146, // Elf_Msg3


        [ActorizerEnabled] // cannot talk to them BUT YOU CAN KILL THEM :D
        [FileID(293)]
        [ObjectListIndex(0xBC)]
        [GroundVariants(0,1,2,3,4,5)] // 6 colors
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [UnkillableAllVariants] // animated kill but not enemy category
        ImposterFrog = 0x147, // En_Fg  // unused beta frog

        [FileID(294)]
        [ObjectListIndex(0x169)]
        Dm_Ravine = 0x148, // Dm_Ravine

        //[EnemizerEnabled] // crash, also object is huge so never gets put places anyway
        [FileID(294)]
        [ObjectListIndex(0x192)] // cutscene object shared with zelda and skullkid, so BIG
        [GroundVariants(0)]
        [UnkillableAllVariants]
        Dm_Sa = 0x149, // Dm_Sa // might be saria but because crash unk

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [ActorInstanceSize(0x208)]
        [FileID(296)]
        [ObjectListIndex(0x16A)]
        [GroundVariants(0x0C01,0x1402,0xFF03,0xFF01,0xFF00,0x0A01,0x0202,0x801,0xFF02)]
        [EnemizerScenesExcluded(Scene.GreatBayTemple, Scene.InvertedStoneTowerTemple)] // necessary to climb
        [EnemizerScenesPlacementBlock(Scene.SouthernSwampClear)] // crash transitioning witch shop room
        // termina field, ff00 gbt waterchu, the rest are assumed respawn until proven otherwise
        [RespawningVariants(0xFF03,0xFF01,0xFF00,   0x0C01,0x1402,0x0A01,0x0202,0x801,0xFF02)]
        ChuChu = 0x14A, // En_Slime

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [FileID(297)]
        [ObjectListIndex(0x16B)]
        [WaterVariants(0x0F00,0x0300)]
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp)] // massive lag
        Desbreko = 0x14B, // En_Pr (Pirana?)

        [FileID(298)]
        [ObjectListIndex(0x16D)]
        Obj_Toudai = 0x14C, // Obj_Toudai
        
        [FileID(299)]
        [ObjectListIndex(0x16D)]
        Obj_Entotu = 0x14D, // Obj_Entotu

        [ActorizerEnabled]
        [FileID(300)]
        [ObjectListIndex(0x16C)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 3, variant:0)]
        //[EnemizerScenesExcluded(Scene.EastClockTown)]
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall, Scene.LaundryPool,
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTower)] // big blocking
        StockpotBell = 0x14E,

        [FileID(301)]
        [ObjectListIndex(0x5)]
        En_Syateki_Okuta = 0x14F, // En_Syateki_Okuta
        
        Empty150 = 0x150,

        [FileID(302)]
        [ObjectListIndex(0x16D)]
        Obj_Shutter = 0x151, // Obj_Shutter

        // crashes in a lot of scenes, breaks them visually like gomess in others, cannot interact, doesn't have collisionbox
        //[ActorizerEnabled]
        [FileID(303)]
        [ObjectListIndex(0x14B)]
        // 0 is dark and crash
        //[GroundVariants(0xFFFF)] //unk
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        Zelda_152 = 0x0152,

        [FileID(305)]
        [ObjectListIndex(0x1)]
        En_Elfgrp = 0x153, // En_Elfgrp
        
        [FileID(306)]
        [ObjectListIndex(0x19F)]
        Dm_Tsg = 0x154, // Dm_Tsg

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1250)]
        [FileID(307)]
        [ObjectListIndex(0x171)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 3, variant: 0)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slowing enemies
        Nejiron = 0x155, // Rolling exploding rock in Ikana

        [FileID(308)]
        [ObjectListIndex(0x166)]
        Obj_Vspinyroll = 0x156, // Obj_Vspinyroll
        
        [FileID(309)]
        [ObjectListIndex(0x16D)]
        Obj_Smork = 0x157, // Obj_Smork
        
        [FileID(310)]
        [ObjectListIndex(0x1)]
        En_Test2 = 0x158, // En_Test2

        [FileID(311)]
        [ObjectListIndex(0x1C)]
        Kafei = 0x159, // En_Test3
        
        [FileID(312)]
        [ObjectListIndex(0x1)]
        ThreeDayTimer = 0x15A, // En_Test4

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [FileID(313)]
        [ObjectListIndex(0x172)]
        [FlyingVariants(0xFF34, 0xFF02, 0xFF03, 0x0102, 0x0103, 0xFF01)]
        [WallVariants(0xFF9F, 0x019F)]
        // one of these is sit on the wall bat from rtss: FF03/01/9F
        [VariantsWithRoomMax(max:1, 0xFF34)] // swarm
        [EnemizerScenesExcluded(Scene.IkanaGraveyard)] // need bats for dampe day 2 check
        BadBat = 0x15B,

        // can hold many different types of graves or stones containing.. nothing bit broke
        //[ActorizerEnabled] // this works fine but shows up waay too often
        // there are actaually 3 others, but they are three separate objects, so hard to program
        [FileID(314)]
        [ObjectListIndex(0x173)]
        // spreadsheet thinks 0x206 could be it
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 1, variant:0)]
        [UnkillableAllVariants]
        // might be used for mikau grave, but also beta actors that teach songs...??
        MagicSlab = 0x15C, // En_Sekihi

        [ActorInitVarOffset(0x37D0)]
        [FileID(315)]
        [ObjectListIndex(0x178)]
        Wizrobe = 0x15D, // En_Wiz

        [FileID(316)]
        [ObjectListIndex(0x178)]
        En_Wiz_Brock = 0x15E, // En_Wiz_Brock
        
        [FileID(317)]
        [ObjectListIndex(0x178)]
        En_Wiz_Fire = 0x15F, // En_Wiz_Fire
        
        [FileID(318)]
        [ObjectListIndex(0x1)]
        Eff_Change = 0x160, // Eff_Change
        
        [FileID(319)]
        [ObjectListIndex(0x26C)]
        Dm_Statue = 0x161, // Dm_Statue

        [ActorizerEnabled]
        [FileID(320)]
        [ActorInstanceSize(0x2AC)] // 1AC, raised to reduce chance of getting
        [ObjectListIndex(0x1)]
        //[GroundVariants(0x4560)] // works but no sound?
        // 0x800B is keeta
        // 0x3F5F smaller than the other two,
        // 3F60 woodfall small
        [GroundVariants(0x3F5F)]
        [VariantsWithRoomMax(max:1, variant: 0x3F5F)]
        [UnkillableAllVariants]
        CircleOfFire = 0x162, // Obj_Fireshield // tag: FireRing

        [FileID(321)]
        [ObjectListIndex(0x179)]
        Bg_Ladder = 0x163, // Bg_Ladder

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1830)]
        [FileID(322)]
        [ObjectListIndex(0x17A)]
        [FlyingVariants(0x9605, 0x3205, 0x6405, 0x8C05, 0xFA01, 0xFA00)]
        // this variety is slow spawn, meaning you have to walk up to it: 0x2800, 0x3200, 0xC200, 0xFA00
        [GroundVariants(0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0xFF01, 0xFF05, 0xC200)] 
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [RespawningVariants(0x6404,0x7804, 0x9605,0x3205,0x6405,  0x8C05, 0xFF05, // actually respawning
            0x2800, 0x3200, 0xC200, 0xFA00)] // these four dont respawn, but they are invisbile until you are right on top of them, then they materialize, so hidden
        [VariantsWithRoomMax(max: 2, variant: 0x9605, 0x3205, 0x6405, 0x8C05, 0xFA01, 0xFA00)]
        [VariantsWithRoomMax(max: 1, variant: 0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0xFF01, 0xFF05, 0xC200)]
        [CompanionActor(ClayPot, variant: 0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)]
        Bo = 0x164, //boe, small ball of snow or soot

        [FileID(323)]
        [ObjectListIndex(0x1)]
        Demo_Getitem = 0x165, // Demo_Getitem
        
        Empty166 = 0x166,

        [FileID(324)]
        [ObjectListIndex(0x189)]
        [UnkillableAllVariants]
        En_Dnb = 0x167, // En_Dnb
        
        [FileID(325)]
        [ObjectListIndex(0x224)]
        En_Dnh = 0x168, // En_Dnh

        // code suggests even more params might exist than are used in vanilla
        [ActorizerEnabled]
        [FileID(326)]
        [ObjectListIndex(0x40)] // 1? nah it uses something else
        // 2 is smaller scrub that surrounds link
        [GroundVariants(2,6)]
        [VariantsWithRoomMax(max:1, variant:6)]
        [UnkillableAllVariants]
        HallucinationScrub = 0x169, // En_Dnk
        
        [FileID(327)]
        [ObjectListIndex(0x18B)]
        En_Dnq = 0x16A, // En_Dnq
        
        Empty16B = 0x16B,
        [FileID(328)]
        [ObjectListIndex(0x17E)]
        Bg_Keikoku_Saku = 0x16C, // Bg_Keikoku_Saku
        
        [FileID(329)]
        [ObjectListIndex(0x12A)]
        Obj_Hugebombiwa = 0x16D, // Obj_Hugebombiwa

        [FileID(330)]
        [ObjectListIndex(0xB)]
        En_Firefly2 = 0x16E, // En_Firefly2

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [ActorInstanceSize(0x2C0)]
        [FileID(331)]
        [ObjectListIndex(0x181)]
        // the snowhead version that doesnt aggro is 01
        [GroundVariants(0x8C, 0x28, 0x3C, 0x46, 0x32, 0x1, 0x8023, 0x5, 0x14, 0x8028, 0x8014)]
        [WallVariants(0x1)] // peaceful, just wants cheese
        [RespawningVariants(0x8014,0x8028,0x8023, // tested respawning
            0x0032,0x0005,0x0014)] // untesed, assumed respawning because I'm lazy for now
        [VariantsWithRoomMax(max: 5, variant: 0x8C, 0x28, 0x3C, 0x46, 0x32, 0x1, 0x5, 0x14)]
        [VariantsWithRoomMax(max: 1, variant: 0x8014, 0x8028, 0x8023, 0x0032, 0x0005, 0x0014)]
        [CompanionActor(ClayPot, variant: 0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)]
        RealBombchu = 0x16F,

        [FileID(332)]
        [ObjectListIndex(0x182)]
        En_Water_Effect = 0x170, // En_Water_Effect

        [ActorizerEnabled]
        [FileID(333)]
        // reminder: even though its obj2, the actual keaton doesn't not spawn without its own object
        [ObjectListIndex(2)] // field_keep
        [AlignedCompanionActor(Bugs, CompanionAlignment.Above, ourVariant: -1,
            variant: 0x2324, 0x4324)] // butterflies over the bushes
        [GroundVariants(0x7F00, 0x400, 0x1F00)] //400 is milkroad, 7F00 is opening area, spring is 1F00
        [UnkillableAllVariants] // untested
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // lag
        KeatonGrass = 0x171, // En_Kusa2

        [FileID(334)]
        [ObjectListIndex(0x153)]
        Bg_Spout_Fire = 0x172, // Bg_Spout_Fire
        
        Empty173 = 0x173,
        [FileID(290)]
        [ObjectListIndex(0x184)]
        Bg_Dblue_Movebg = 0x174, // Bg_Dblue_Movebg
        
        [FileID(335)]
        [ObjectListIndex(0x8)]
        En_Dy_Extra = 0x175, // En_Dy_Extra

        [ActorizerEnabled]
        [FileID(336)]
        [ObjectListIndex(0x185)]
        [FlyingVariants(0, 1, 2, 3, 4, 5)]
        // works as a wall enemy, but cannot be marked wall in case other walls replace tingle
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        [EnemizerScenesPlacementBlock(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        Tingle = 0x176, // En_Bal

        [ActorizerEnabled]
        [FileID(337)]
        [ObjectListIndex(0xE3)]
        [GroundVariants(0xFF)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WestClockTown)]
        Banker = 0x177, // En_Ginko_Man

        [ActorizerEnabled]
        [FileID(338)]
        [ObjectListIndex(0x186)]
        [GroundVariants(0xFFFF)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        PirateTelescope = 0x178, // En_Warp_Uzu

        [FileID(339)]
        [ObjectListIndex(0x187)]
        Obj_Driftice = 0x179, // Obj_Driftice

        [EnemizerEnabled] // walks forever in a straight line, until we can keep them on a path they are a boring enemy
        [FileID(340)]
        [ObjectListIndex(0x135)]
        // variants 0x7F is the switch range (unused) where 7F tells the actor to ignore switches
        // 0x0F80 is the pathing range, and 0xF000 is the kickout entrance to use
        [PathingVariants(0x127F, 0x12FF, 0x137F, 0x13FF, 0x147F, 0x14FF, 0x157F, 0x15FF, 0x177F, 0x17FF, 0x187F)]
        [PathingTypeVarsPlacement(mask: 0x0F80, shift: 7)]
        [PathingKickoutAddrVarsPlacement(mask: 0xF, shift: 12)]
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown, Scene.SwampSpiderHouse)]
        [UnkillableAllVariants]
        DekuPatrolGuard = 0x17A, // En_Look_Nuts

        [FileID(341)]
        [ObjectListIndex(0x1)]
        En_Mushi2 = 0x17B, // En_Mushi2

        //[ActorizerEnabled] // no point, if it spawns on the ground its too big and you can't tell you are inside of it
        // and enemies are so far away they are culled so you cant see them
        [FileID(342)]
        [ObjectListIndex(0x188)] // or 223? two different objects can work, makes it hard to cath
        [FlyingVariants(0x37F)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        Moon1 = 0x17C, // En_Fall

        [FileID(343)]
        [ObjectListIndex(0x107)]
        En_Mm3 = 0x17D, // En_Mm3
        
        [FileID(344)]
        [ObjectListIndex(0x18A)]
        Bg_Crace_Movebg = 0x17E, // Bg_Crace_Movebg

        // todo come back and figure out how to spawn regular 
        //[ActorizerEnabled]
        [FileID(345)]
        [ObjectListIndex(0x1F3)]
        // params 0xC000, 0x2F8, 0x7F, 0xF ???
        // 0x8000 is cutscene version, 0x7FFF is main hall but doesn't spawn?
        //[GroundVariants(0x2000)]
        // good candidate for aligned front companions, to his son
        [UnkillableAllVariants]
        Butler = 0x17F, // En_Dno

        [EnemizerEnabled] // biggest issue: they dont really attack, this isn't the version that spawns over and over
        [ActorInitVarOffset(0x1C6C)]
        [FileID(346)]
        [ObjectListIndex(0x16B)]
        // A2 might be the ones that respawn over and over, the "Encounter"
        // 82 and 62 are found in the map room, both just kinda spin, never engages
        // zora cape has 000, same as the other two
        [WaterVariants( 0xA2, 0x82, 0x62, 0 )]
        SkullFish = 0x180, // En_Pr2


        [FileID(347)]
        [ObjectListIndex(0x16B)]
        En_Prz = 0x181, // En_Prz

        //[EnemizerEnabled]
        [FileID(348)]
        [ObjectListIndex(0x155)]
        [GroundVariants(0)]  // does not spawn, but if you approach where he SHOULD BE you lose camera control
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.StoneTowerTemple)]
        GaroMaster = 0x182, // En_Jso2


        [EnemizerEnabled] // free enemy, placed in places where enemies are normally
        [FileID(349)]
        [ObjectListIndex(0x1)] // obj 1: gameplay keep, but can't set that
        [GroundVariants(0x7F, 0x17F)] // 7F is regular, 17F is big yellow
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.Woodfall, Scene.DekuPalace, Scene.WoodfallTemple, Scene.OdolwasLair, 
            Scene.EastClockTown, Scene.NorthClockTown, Scene.IkanaCastle, Scene.SnowheadTemple)] 
        DekuFlower = 0x183,
       
        [EnemizerEnabled] // AI gets confused, backwalks forever, pathing?
        [ActorInitVarOffset(0x445C)]
        [FileID(350)]
        [ObjectListIndex(0x18D)]
        // params: 7x >> 6 is switch, 0x3F is unk
        [PathingVariants(0x700, 0x940)]
        [PathingTypeVarsPlacement(mask: 0x3F, shift: 0)]
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple, Scene.StoneTowerTemple)]
        Eyegore = 0x184, // En_Egol

        [EnemizerEnabled]
        [FileID(351)]
        [ObjectListIndex(0xBB)]
        [WaterVariants(0x2002, 0x2006, 0x200B, 0x2003, 0x2004, 0x2005, 0x200C)]
        // if I had a hanging from cieling thing like spiders this would work fine
        [WallVariants(0x100D,  0x110E, 0x1011, 0x1014, 0x1016, 0x1017, 0x1019)]
        [UnkillableAllVariants] // actorcat PROP, not detected as enemy
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple, Scene.StoneTowerTemple)]
        Mine = 0x185, // Obj_Mine

        [FileID(352)]
        [ObjectListIndex(0x1)]
        Obj_Purify = 0x186, // Obj_Purify
        
        [FileID(353)]
        [ObjectListIndex(0x18E)]
        En_Tru = 0x187, // En_Tru
        
        [FileID(354)]
        [ObjectListIndex(0x18F)]
        En_Trt = 0x188, // En_Trt
        
        Empty189 = 0x189,
        Empty18A = 0x18A,

        [FileID(355)]
        [ObjectListIndex(0x1)]
        En_Test5 = 0x18B, // En_Test5

        [FileID(356)]
        [ObjectListIndex(0x1)]
        En_Test6 = 0x18C, // En_Test6
        
        [FileID(357)]
        [ObjectListIndex(0x198)]
        En_Az = 0x18D, // En_Az
        
        [FileID(358)]
        [ObjectListIndex(0x18D)]
        En_Estone = 0x18E, // En_Estone
        
        [FileID(359)]
        [ObjectListIndex(0x190)]
        Bg_Hakugin_Post = 0x18F, // Bg_Hakugin_Post
        
        [FileID(360)]
        [ObjectListIndex(0x169)]
        Dm_Opstage = 0x190, // Dm_Opstage
        
        [FileID(361)]
        [ObjectListIndex(0x192)]
        Dm_Stk = 0x191, // Dm_Stk
        
        [FileID(362)]
        [ObjectListIndex(0x1C8)]
        Dm_Char00 = 0x192, // Dm_Char00
        
        [FileID(363)]
        [ObjectListIndex(0x1A2)]
        Dm_Char01 = 0x193, // Dm_Char01
        
        [FileID(364)]
        [ObjectListIndex(0x1BE)]
        Dm_Char02 = 0x194, // Dm_Char02
        
        [FileID(365)]
        [ObjectListIndex(0x1A3)]
        Dm_Char03 = 0x195, // Dm_Char03

        //[ActorizerEnabled]
        [FileID(366)]
        [ObjectListIndex(0x77)] // 1
        // 0 is tatl regular
        [FlyingVariants(0)] // unk
        [GroundVariants(0)] // unk
        [VariantsWithRoomMax(max: 1, variant:0)]
        [UnkillableAllVariants]
        Dm_Char4 = 0x196,

        [FileID(367)]
        [ObjectListIndex(0x213)]
        Dm_Char05 = 0x197, // Dm_Char05
        [FileID(368)]
        [ObjectListIndex(0x1E6)]
        Dm_Char06 = 0x198, // Dm_Char06
        [FileID(369)]
        [ObjectListIndex(0x212)]
        Dm_Char07 = 0x199, // Dm_Char07
        [FileID(370)]
        [ObjectListIndex(0x229)]
        Dm_Char08 = 0x19A, // Dm_Char08
        [FileID(371)]
        [ObjectListIndex(0x1EB)]
        Dm_Char09 = 0x19B, // Dm_Char09

        [ActorizerEnabled]
        [FileID(372)]
        [ObjectListIndex(0x18C)]
        [WallVariants(0x907F, 0xA07F)]
        [UnkillableAllVariants]
        Clock = 0x19C, // En_Tokeidai

        Empty19D = 0x19D,

        [FileID(373)]
        [ObjectListIndex(0x195)]
        En_Mnk = 0x19E, // En_Mnk
        
        [FileID(374)]
        [ObjectListIndex(0x18D)]
        En_Egblock = 0x19F, // En_Egblock

        [ActorizerEnabled]
        [FileID(375)]
        [ObjectListIndex(0x135)]
        [GroundVariants(0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        [EnemizerScenesExcluded(Scene.DekuPalace)] // do not remove original for now
        PalaceGuardDeku = 0x1A0, // En_Guard_Nuts

        [FileID(376)]
        [ObjectListIndex(0x190)]
        Bg_Hakugin_Bombwall = 0x1A1, // Bg_Hakugin_Bombwall
        
        [FileID(377)]
        [ObjectListIndex(0x197)]
        Obj_Tokei_Tobira = 0x1A2, // Obj_Tokei_Tobira
        
        [FileID(378)]
        [ObjectListIndex(0x190)]
        Bg_Hakugin_Elvpole = 0x1A3, // Bg_Hakugin_Elvpole

        [ActorizerEnabled] // crash in clocktower, too big for anywhere else
        // 100 does not spawn
        [FileID(379)]
        [ObjectListIndex(0xB7)] // 100 and FF00
        [PathingVariants(0xFF00)] // all vanilla are 0xB7
        [PathingTypeVarsPlacement(mask:0xFF00, shift:8)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RomaniRanch, Scene.RanchBuildings)]
        Romani1 = 0x1A4, // En_Ma4

        // twig?
        [FileID(380)]
        [ObjectListIndex(0x199)]
        En_Twig = 0x1A5, // En_Twig

        [ActorizerEnabled]
        [ActorInstanceSize(0x330)] // 274 but fake increase size to reduce frequency
        [FileID(381)]
        [ObjectListIndex(0x19B)]
        // vars: in vanilla 0 only spawns if it can find romani, 0x80XX are timed fuse baloons for credits scene
        // we have fixed 0x0 so that it doesn't auto-despawn, however, since the code handles her not being available anyway
        [GroundVariants(0x0)]
        [FlyingVariants(0x0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RomaniRanch)] // dont replace actual romani balloons
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // long draw distance means they can overflow actor spawn
        PoeBalloon = 0x1A6, // En_Po_Fusen

        [FileID(382)]
        [ObjectListIndex(0x1)]
        En_Door_Etc = 0x1A7, // En_Door_Etc
        
        [FileID(383)]
        [ObjectListIndex(0x19E)]
        En_Bigokuta = 0x1A8, // En_Bigokuta
        
        [FileID(384)]
        [ObjectListIndex(0x1E7)]
        Bg_Icefloe = 0x1A9, // Bg_Icefloe
        
        [FileID(391)]
        [ObjectListIndex(0x163)]
        Obj_Ocarinalift = 0x1AA, // Obj_Ocarinalift
        
        [FileID(392)]
        [ObjectListIndex(0x1)]
        En_Time_Tag = 0x1AB, // En_Time_Tag
        
        [FileID(393)]
        [ObjectListIndex(0x19F)]
        Bg_Open_Shutter = 0x1AC, // Bg_Open_Shutter
        
        [FileID(394)]
        [ObjectListIndex(0x19F)]
        Bg_Open_Spot = 0x1AD, // Bg_Open_Spot
        
        [FileID(395)]
        [ObjectListIndex(0x1A0)]
        Bg_Fu_Kaiten = 0x1AE, // Bg_Fu_Kaiten
        
        [FileID(396)]
        [ObjectListIndex(0x1)]
        Obj_Aqua = 0x1AF, // Obj_Aqua
        
        // elf or org?
        [FileID(397)]
        [ObjectListIndex(0x1)]
        En_Elforg = 0x1B0, // En_Elforg

        //[ActorizerEnabled] // don't give actual items, sadly, just jape you into thinking they are items
        [FileID(398)]
        [ObjectListIndex(0xE)]
        // snowhead : 0x5E00,0x6000, 0x5800,0x5600, GreatBay: 0x6000
        // huh? these repeat per dungeon? 
        [FlyingVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        [GroundVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.Woodfall, Scene.Snowhead, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        ElfBubble = 0x1B1,

        Empty1B2 = 0x1B2,



        // probably an object check missing somewhere
        //[ActorizerEnabled] // does not spawn
        [FileID(399)]
        [ObjectListIndex(0x1A1)]
        [WallVariants(0x0)] // unk because spawned by H+D
        [UnkillableAllVariants] // not enemy type, right?
        Target = 0x1B3, // En_Fu_Mato

        //[ActorizerEnabled] // does not spawn
        [FileID(400)]
        [ObjectListIndex(0x1A1)]
        [WallVariants(0x1)] // unk because spawned by H+D
        [UnkillableAllVariants] // not enemy type, right?
        BombBasket = 0x1B4, // En_Fu_Kago

        [ActorizerEnabled] // hes really rare because his ram requirements are huge
        [FileID(401)]
        [ObjectListIndex(0x1A3)]
        [GroundVariants(0)] // 0 is clocktower
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        HappyMaskSalesman = 0x1B5, // En_Osn

        [FileID(402)]
        [ObjectListIndex(0x88)]
        Bg_Ctower_Gear = 0x1B6, // Bg_Ctower_Gear

        [ActorizerEnabled]
        [FileID(403)]
        [ObjectListIndex(0x18F)]
        [PathingVariants(0x2400, 0x2000)]
        [PathingTypeVarsPlacement(mask:0xFC00, shift: 10)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WoodsOfMystery, Scene.SouthernSwamp)]
        KotakeOnBroom = 0x1B7, // En_Trt2

        [FileID(404)]
        [ObjectListIndex(0x1A4)]
        Obj_Tokei_Step = 0x1B8, // Obj_Tokei_Step
        
        [FileID(405)]
        [ObjectListIndex(0x1A5)]
        Lillypad = 0x1B9, // Bg_Lotus

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1FF0)]
        [FileID(406)]
        [ObjectListIndex(0x1A6)]
        [GroundVariants(0x0)]
        [CompanionActor(DekuFlower, variant: 0x7F)]
        [EnemizerScenesExcluded(Scene.WoodfallTemple)] // req for gekko miniboss, do not touch until fix
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        Snapper = 0x1BA, // En_Kame
        
        [FileID(407)]
        [ObjectListIndex(0x1B4)]
        Obj_Takaraya_Wall = 0x1BB, // Obj_Takaraya_Wall
        
        [FileID(408)]
        [ObjectListIndex(0x1A0)]
        Bg_Fu_Mizu = 0x1BC, // Bg_Fu_Mizu

        //[ActorizerEnabled] //wrong one, the one we want is burrowed, but he also does NOT come with a flower, its secondary
        [FileID(409)]
        [ObjectListIndex(0x1E5)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.SouthClockTown, Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.GoronVillage, Scene.GoronVillageSpring, Scene.ZoraHallRooms, Scene.IkanaCanyon)]
        BuisinessScrub = 0x1BD,

        [FileID(410)]
        [ObjectListIndex(0x1A7)]
        Bg_Dkjail_Ivy = 0x1BE, // Bg_Dkjail_Ivy
        
        Empty1BF = 0x1BF,

        [FileID(411)]
        [ObjectListIndex(0x1A8)]
        Obj_Visiblock = 0x1C0, // Obj_Visiblock

        [ActorizerEnabled]
        [FileID(412)]
        [ObjectListIndex(0x129)]
        [GroundVariants(0xFFFF)] // zero is regular, -1 is credits?
        //[GroundVariants(0)] // zero is regular, -1 is credits?
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)]
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown)]
        [EnemizerScenesExcluded(Scene.TreasureChestShop)]
        BombchuGirl = 0x1C1, // En_Takaraya
        
        [FileID(413)]
        [ObjectListIndex(0x1A9)]
        En_Tsn = 0x1C2, // En_Tsn
        
        [FileID(414)]
        [ObjectListIndex(0x1AA)]
        En_Ds2n = 0x1C3, // En_Ds2n
        
        [FileID(415)]
        [ObjectListIndex(0x1AB)]
        En_Fsn = 0x1C4, // En_Fsn
        
        [FileID(416)]
        [ObjectListIndex(0x1AC)]
        En_Shn = 0x1C5, // En_Shn

        Empty1C6 = 0x1C6,

        [ActorizerEnabled]
        [FileID(417)]
        [ObjectListIndex(0x1B6)]
        [GroundVariants(0x7F, 0x307F, 0x207F, 0x107F)]
        [UnkillableAllVariants]
        GateSoldier = 0x1C7,

        [ObjectListIndex(0x1AD)]
        [FileID(418)]
        // FF01 is the ice blocking the path north
        // 0x5AXX seems to be the blocking path ice walls from snowhead temple
        //[GroundVariants(0x5A00)] 
        [UnkillableAllVariants]
        IceBlock = 0x1C8, // Obj_BigIcicle
        
        [FileID(419)]
        [ObjectListIndex(0x1E5)]
        En_Lift_Nuts = 0x1C9, // En_Lift_Nuts

        //[ActorizerEnabled] //busted
        [FileID(420)]
        [ObjectListIndex(0x1AF)]
        Dampe = 0x1CA, // En_Tk

        Empty1CB = 0x1CB,
        [FileID(421)]
        [ObjectListIndex(0x1B0)]
        Bg_Market_Step = 0x1CC, // Bg_Market_Step
        
        [FileID(422)]
        [ObjectListIndex(0x163)]
        Obj_Lupygamelift = 0x1CD, // Obj_Lupygamelift
        
        [FileID(423)]
        [ObjectListIndex(0x1)]
        En_Test7 = 0x1CE, // En_Test7
        
        [FileID(424)]
        [ObjectListIndex(0x1B3)]
        Obj_Lightblock = 0x1CF, // Obj_Lightblock

        //[EnemizerEnabled] // no spawn, probably requires ikana king as parent
        [FileID(425)]
        [ObjectListIndex(0x1B5)]
        [WallVariants(0x1)]
        [GroundVariants(0x1)]
        Mir_Ray2 = 0x1D0, // Mir_Ray2

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1FD0)]
        [FileID(426)]
        [ObjectListIndex(0x1B5)]
        // dont know what the differences are
        [WallVariants(0x1932, 0x3FFF)] // dont know what the differences are
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple, Scene.GreatBayTemple, Scene.InvertedStoneTowerTemple)]
        Dexihand = 0x1D1,// ???'s water logged brother

        [FileID(427)]
        [ObjectListIndex(0x1)]
        En_Gamelupy = 0x1D2, // En_Gamelupy
        
        [FileID(428)]
        [ObjectListIndex(0x1)]
        Bg_Danpei_Movebg = 0x1D3, // Bg_Danpei_Movebg
        
        [FileID(429)]
        [ObjectListIndex(0x1B7)]
        En_Snowwd = 0x1D4, // En_Snowwd

        // I suspect since he has so few vars that he will be hard coded, and req decomp to fix
        //[ActorizerEnabled]
        [FileID(430)]
        [GroundVariants(0x0)] // 0: sitting in his room?
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WestClockTown, Scene.EastClockTown, Scene.NorthClockTown, Scene.SouthClockTown, Scene.PostOffice)]
        [ObjectListIndex(0x107)]
        PostMan = 0x1D5, // En_Pm

        [FileID(431)]
        [ObjectListIndex(0x1)]
        En_Gakufu = 0x1D6, // En_Gakufu
        
        [FileID(432)]
        [ObjectListIndex(0x1)]
        Elf_Msg4 = 0x1D7, // Elf_Msg4
        
        [FileID(433)]
        [ObjectListIndex(0x1)]
        Elf_Msg5 = 0x1D8, // Elf_Msg5
        
        [FileID(434)]
        [ObjectListIndex(0x1)]
        En_Col_Man = 0x1D9, // En_Col_Man

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2940)]
        [FileID(435)]
        [ObjectListIndex(0x75)]
        [GroundVariants(0x1E9, 0x1F4, 0x208, 0x214, 0x224, 0x236, 0x247, 0x253, 0x262, 0x275, 0x283, 0x291, 0x2A0)]
        [EnemizerScenesExcluded(Scene.BeneathTheWell, Scene.IkanaCanyon)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows down player too much in a race setting
        GibdoWell = 0x1DA,

        [FileID(436)]
        [ObjectListIndex(0x1B8)]
        En_Giant = 0x1DB, // En_Giant
        
        [FileID(437)]
        [ObjectListIndex(0xEF)]
        Obj_Snowball = 0x1DC, // Obj_Snowball
        
        [FileID(438)]
        [ObjectListIndex(0x1BB)]
        Boss_Hakugin = 0x1DD, // Boss_Hakugin
        
        [FileID(439)]
        [ObjectListIndex(0x144)]
        En_Gb2 = 0x1DE, // En_Gb2
        
        [FileID(440)]
        [ObjectListIndex(0x1)]
        En_Onpuman = 0x1DF, // En_Onpuman
        
        [FileID(441)]
        [ObjectListIndex(0x1BF)]
        Bg_Tobira01 = 0x1E0, // Bg_Tobira01
        
        [FileID(442)]
        [ObjectListIndex(0x1)]
        En_Tag_Obj = 0x1E1, // En_Tag_Obj
        
        [FileID(443)]
        [ObjectListIndex(0x1C1)]
        Obj_Dhouse = 0x1E2, // Obj_Dhouse

        [ActorizerEnabled]
        [FileID(444)]
        [ObjectListIndex(0x1C2)]
        // FFFF is extra?
        // 600 is night one, 702 is night 2, 801, is night 3
        // ffff crashes in TF
        // only one 0x600 can exist without crashing
        //[GroundVariants(0xFFFF, 0x600, 0x702, 0x801)]
        [GroundVariants(0xFFFF, 0x600, 0x702, 0x801)]
        //[VariantsWithRoomMax(max:1, 0xFFFF, 0x600, 0x702, 0x801)]
        [OnlyOneActorPerRoom] // issue: three on redead in stonetower is crash, but not two, not worth issue
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.IkanaGraveyard)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, // blocking enemies
            Scene.SouthernSwamp)] // 75% chance of crash, reason unk
        IkanaGravestone = 0x1E3,

        [FileID(445)]
        [ObjectListIndex(0x1C7)]
        Bg_Hakugin_Switch = 0x1E4, // Bg_Hakugin_Switch
        
        Empty1E5 = 0x1E5,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [ActorInstanceSize(0x378)]
        [FileID(446)]
        [ObjectListIndex(0x1C4)]
        [GroundVariants(0xFF00, 0xFF01, 0, 1)]
        [VariantsWithRoomMax(max:1, variant: 1, 0xF001)] // limit the bigger one
        Eeno = 0x1E6, // En_Snowman
        
        // gold skull bonk detector
        [FileID(447)]
        [ObjectListIndex(0x1)]
        TG_Sw = 0x1E7, // TG_Sw

        [EnemizerEnabled]
        [ActorInitVarOffset(0x36A0)]
        [FileID(448)]
        [ObjectListIndex(0x1C5)]
        //0x100 is red, 0x200 is blue, 0x300 is green, 00 is purple, however, its difficult to fight more than 2
        [FlyingVariants(0x300, 0x200, 0x100)]
        [GroundVariants(0x300, 0x200, 0x100, 0)]
        [VariantsWithRoomMax(max: 1, variant: 0, 0x100, 0x200, 0x300)] // only one per
        // no scene exclusion necessary, get spawned by the poe sisters minigame but they aren't actors in the scene to be randomized
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        PoeSisters = 0x1E8,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3794)]
        [FileID(449)]
        [ObjectListIndex(0x1C6)]
        [GroundVariants(0,0x0101)]
        Hiploop = 0x1E9, // Charging beetle in Woodfall

        [FileID(450)]
        [ObjectListIndex(0x1BB)]
        En_Hakurock = 0x1EA, // En_Hakurock
        
        [FileID(451)]
        [ObjectListIndex(0x1)] // doubt
        Fireworks = 0x1EB, // En_Hanabi

        [FileID(452)]
        [ObjectListIndex(0x1)]
        Obj_Dowsing = 0x1EC, // Obj_Dowsing
        
        [FileID(453)]
        [ObjectListIndex(0x1)]
        Obj_Wind = 0x1ED, // Obj_Wind

        [ActorizerEnabled] // thank god for m2c
        [FileID(454)]
        [ObjectListIndex(0x132)]
        [PathingVariants(0x19F, 0xD9F, 0x3FF, 0x22BF,
            0x20, 0x40, 0x60, 0x80, 0x120)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [UnkillableAllVariants]
        RaceDog = 0x1EE, // En_Racedog

        //[ActorizerEnabled] // does not spawn, again with the hardcoded nonsense
        [FileID(454)]
        [ObjectListIndex(0x10F)]
        [GroundVariants(0xFF01)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.SwordsmansSchool)] // dont remove
        KendoSensei = 0x1EF, // En_Kendo_Js

        [FileID(456)]
        [ObjectListIndex(0x1C9)]
        Bg_Botihasira = 0x1F0, // Bg_Botihasira

        [ActorizerEnabled]
        [FileID(457)]
        [ActorInstanceSize(0x2018)]
        [ObjectListIndex(0x1D7)]
        [WaterVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.MarineLab)]
        [EnemizerScenesPlacementBlock(Scene.GreatBayCoast, Scene.ZoraCape)] // issue: if both fish and labfish spawn, they eat, and cutscene locks
        LabFish = 0x1F1, // En_Fish2

        [ActorizerEnabled]
        [ActorInitVarOffset(0xC68)]
        [FileID(458)]
        [ObjectListIndex(0x1CB)]
        [GroundVariants(0,1,2,3)]
        [CompanionActor(LetterToPostman, variant: 0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WestClockTown, Scene.SouthClockTown, Scene.NorthClockTown, Scene.EastClockTown)]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        Postbox = 0x1F2, // En_Pst

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [FileID(459)]
        [ObjectListIndex(0x1C3)]
        [FlyingVariants(0x00FF)]
        // FF is in the game, in OOT 02 was a composer brother, but in MM 0-6 are the same as FF
        [GroundVariants(0x00FF)]
        [CompanionActor(Flame, 0x7FE)] // blue flames
        [EnemizerScenesExcluded(Scene.InvertedStoneTowerTemple)]
        Poe = 0x1F3, // En_Poh

        [FileID(460)]
        [ObjectListIndex(0x1CD)]
        Obj_Spidertent = 0x1F4, // Obj_Spidertent

        [ActorizerEnabled]
        [FileID(461)]
        [ObjectListIndex(0x1CE)]
        // 2000 is hookshot room, 1A00 is twin barrel, 0x1E00 is barrel room, 1C00 is last room
        // the three at pinnacle rock are 0x1400, 0x1600, 0x1800
        // 0 loaded fine, what happens if we load smaller values? can we have 3 or more?
        [WaterVariants(0x0, 0x1000, 0x1200, 0x1300, 0x1500, 0x1700)]
        [VariantsWithRoomMax(max:1, variant: 0x0, 0x1000, 0x1200, 0x1300, 0x1500, 0x1700)]
        [UnkillableAllVariants]
        ZoraEgg = 0x1F5, // En_Zoraegg

        [FileID(462)]
        [ObjectListIndex(0x1CF)]
        En_Kbt = 0x1F6, // En_Kbt
        
        [FileID(463)]
        [ObjectListIndex(0x1D0)]
        DarmanisGhost1 = 0x1F7, // En_Gg
        
        [FileID(464)]
        [ObjectListIndex(0x1D1)]
        En_Maruta = 0x1F8, // En_Maruta

        [FileID(465)]
        [ObjectListIndex(0xEF)]
        Obj_Snowball2 = 0x1F9, // Obj_Snowball2
        
        [FileID(466)]
        [ObjectListIndex(0x1D0)]
        DarkmanisGhost2 = 0x1FA, // En_Gg2

        [FileID(467)]
        [ObjectListIndex(0x1FB)]
        DarmaniGrave = 0x1FB, // Obj_Ghaka

        [ActorizerEnabled]
        [FileID(468)]
        [ObjectListIndex(0x1D4)]
        // 0 is inside of tree
        // 2 is post-woodfall sitting in royal chamber, does not spawn until after a flag is set
        [GroundVariants(0x0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WoodfallTemple, Scene.DekuKingChamber)] // if her object is not in the king chamber no cutscene after bottle delivery
        DekuPrincess = 0x1FC, // En_Dnp

        [FileID(469)]
        [ObjectListIndex(0x1D5)]
        En_Dai = 0x1FD, // En_Dai

        [FileID(470)]
        [ObjectListIndex(0x1D3)]
        Bg_Goron_Oyu = 0x1FE, // Bg_Goron_Oyu
        
        [FileID(471)]
        [ObjectListIndex(0x1D6)]
        En_Kgy = 0x1FF, // En_Kgy
        
        // the wackest actor that controls the whole alien invasion event, and a lot of stuff at ranch
        [FileID(472)]
        [ObjectListIndex(0x1)]
        En_Invadepoh = 0x200, // En_Invadepoh

        //[ActorizerEnabled] // softlock if you enter the song teach cutscene, which in rando is proximity
        [FileID(473)]
        [ObjectListIndex(0x1DF)]
        //[GroundVariants(0x1400)] // all other versions are 0x13** or 0x1402
        // 0x3FF1 does not spawn in winter, even in other scenes
        [GroundVariants(0x3FF1)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.GoronShrine, Scene.GoronRacetrack, Scene.TwinIslandsSpring)]
        GoronKid = 0x201,

        [FileID(474)]
        [ActorInstanceSize(0x3C8)]
        [ObjectListIndex(0xE2)]
        Anju = 0x202, // En_An

        Empty203 = 0x203,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [FileID(475)]
        [ObjectListIndex(0x1EB)]
        // 0 is the lame bee that just spins in circles, 1/2 are aggressive and charge at you
        [FlyingVariants(0,1,2,3,4,5)]
        [GroundVariants(0,1,2,3,4,5)]
        [VariantsWithRoomMax(max:4, variant: 0, 1, 2, 3, 4, 5)]
        [EnemizerScenesExcluded(Scene.PiratesFortressRooms)] // pirate beehive cutscene
        GiantBeee = 0x204,

        [FileID(476)]
        [ObjectListIndex(0x1EC)]
        En_Ot = 0x205, // En_Ot

        [FileID(477)]
        [ObjectListIndex(0x1ED)]
        DeepPython = 0x206, //En_Dragon

        //[ActorizerEnabled] // spawns but invisible, can hit it but cannot see it in TF
        // hmm, sword school special object is dungeon_keep
        [FileID(478)]
        [ObjectListIndex(0x1EE)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        Gong = 0x207,

        [EnemizerEnabled]
        [FileID(479)]
        [ObjectListIndex(0x1F1)]
        // think 0102 is the dig spots for dampe
        // 1 and FF00 both exist
        // 1 has a spawn flag that stops respawning once you kill one
        //  because of this, it's possible to kill one in TF, then need to kill one in a clear enemy puzzle room and it doesnt spawn
        //[FlyingVariants(0xFF00)] // 1 is a possible type? well: ff00
        [GroundVariants(1)] // looking at the code, FF should always spawn, and 2 is a special case, but what about the rest? crashes tho
        //[GroundVariants(0x1)] // looking at the code, FF should always spawn, and 2 is a special case, but what about the rest?
        [EnemizerScenesExcluded(Scene.BeneathTheWell, Scene.DampesHouse)] // well and dampe house must be vanilla for scoopsanity
        //[OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max:2, variant:1)]
        [UnkillableAllVariants] // only 1, the one with a no-respawn flag, spawns readily, so for now, assume the player kills one and can't kill another
        [CompanionActor(Flame, 0x7FE)] // blue flames
        [EnemizerScenesPlacementBlock( Scene.TerminaField, // suspected weird un-reproducable crashes always seems to happen when they are around
            Scene.SouthernSwamp, Scene.StoneTower)] // they either dont spawn, or when they appear they lock your controls, bad
        BigPoe = 0x208, // En_Bigpo

        [ActorizerEnabled]
        [FileID(480)]
        [ObjectListIndex(0x1EE)]
        // thought I could fake a var 1  but it actually does something????
        [WallVariants(0)]
        //[GroundVariants(0)] // vanilla, low to the ground
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.SwordsmansSchool)] // object is also used for gong, messes with rupee rando
        // should we randomize the orgiginal?
        SwordsmanSign = 0x209, // Obj_Kendo_Kanban

        //[ActorizerEnabled] // only the head not the whole cow, lame
        [FileID(481)]
        [ObjectListIndex(0x1F2)]
        [WallVariants(0x907F, 0xA07F)]
        [GroundVariants(0x907F, 0xA07F)]
        [UnkillableAllVariants]
        // maybe dont remove originals
        CowFigurine = 0x20A, // Obj_Hariko

        [ActorizerEnabled]
        [FileID(482)]
        [ObjectListIndex(0x26A)] // the spreadsheet says he is obj 1 but that is a mistake
        // 0xFF03 crashes on approach
        // FE04/5 doesn't spawn, probably until you finish the spiderhouse
        // FE03 is in SCT, he stares up at the moon, except doesn't know where the moon is, can face the wrong way
        // FE01 doesn't want to spawn, hmm, 02 is swamp spiderhouse, likely doesn't want to spawn either until house is cleared
        [GroundVariants(0xFE03)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        // looking at moon, don't place him underground
        [EnemizerScenesPlacementBlock(Scene.Grottos, Scene.InvertedStoneTower, Scene.BeneathGraveyard, Scene.BeneathTheWell,
            Scene.GoronShrine, Scene.IkanaCastle, Scene.OceanSpiderHouse, Scene.SwampSpiderHouse,
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.InvertedStoneTowerTemple, Scene.Woodfall)]
        Seth1 = 0x20B, // En_Set, the green shirt guy, "Seth"? spiderhouses, hands waving at you from telescope guy

        [FileID(483)]
        [ObjectListIndex(0x1F4)]
        Bg_Sinkai_Kabe = 0x20C, // Bg_Sinkai_Kabe
        
        [FileID(484)]
        [ObjectListIndex(0x1E0)]
        Bg_Haka_Curtain = 0x20D, // Bg_Haka_Curtain
        
        [FileID(485)]
        [ObjectListIndex(0x1F5)]
        Bg_Kin2_Bombwall = 0x20E, // Bg_Kin2_Bombwall
        
        [FileID(486)]
        [ObjectListIndex(0x1F5)]
        Bg_Kin2_Fence = 0x20F, // Bg_Kin2_Fence

        [ActorizerEnabled]
        [FileID(487)]
        [ObjectListIndex(0x1F5)]
        [WallVariants(0x3F)] // 3F has no cutscene, no camera concerns
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.OceanSpiderHouse)] // object is shared with multiple actors in this scene, breaks whole area
        SkullKidPainting = 0x210, // En_Kin2_Picture

        [FileID(488)]
        [ObjectListIndex(0x1F5)]
        Bg_Kin2_Shelf = 0x211, // Bg_Kin2_Shelf
        
        [FileID(489)]
        [ObjectListIndex(0x142)]
        En_Rail_Skb = 0x212, // En_Rail_Skb

        [ActorizerEnabled]
        [FileID(490)]
        [ObjectListIndex(0x1F8)]
        // 1 is standing in the hall during spring
        [GroundVariants(1)]
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.GoronShrine)] // remove and it crashes, dont know why
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        GoronElder = 0x213,

        [FileID(491)]
        [ObjectListIndex(0x18E)]
        En_Tru_Mt = 0x214, // En_Tru_Mt
        
        [FileID(492)]
        [ObjectListIndex(0x1FC)]
        Obj_Um = 0x215, // Obj_Um

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1C50)]
        [FileID(493)]
        [ObjectListIndex(0x201)]
        [GroundVariants(0xFF,0x80FF)] // does this include the really big one?
        Leever = 0x216, // En_Neo_Reeba

        [FileID(494)]
        [ObjectListIndex(0x202)]
        Bg_Mbar_Chair = 0x217, // Bg_Mbar_Chair
        
        [FileID(495)]
        [ObjectListIndex(0x3)]
        Bg_Ikana_Block = 0x218, // Bg_Ikana_Block

        [ActorizerEnabled]
        [FileID(496)]
        [ObjectListIndex(0x203)]
        [WallVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        StoneTowerMirror = 0x219, // Bg_Ikana_Mirror

        [FileID(497)]
        [ObjectListIndex(0x203)]
        Bg_Ikana_Rotaryroom = 0x21A, // Bg_Ikana_Rotaryroom
        
        [FileID(498)]
        [ObjectListIndex(0x184)]
        Bg_Dblue_Balance = 0x21B, // Bg_Dblue_Balance
        
        [FileID(499)]
        [ObjectListIndex(0x184)]
        Bg_Dblue_Waterfall = 0x21C, // Bg_Dblue_Waterfall

        //[EnemizerEnabled] // cutscene is broken without camera placement, player stuck in place
        [FileID(500)]
        [ObjectListIndex(0x204)]
        //[GroundVariants(0x24B)] // 3 different versions
        [GroundVariants(0x24B)]
        //[EnemizerScenesExcluded(0x23)] // do not remove original, for now
        PirateColonel = 0x21D,

        [EnemizerEnabled]
        [FileID(501)]
        [ObjectListIndex(0x12E)]
        [PathingVariants(0x1F, 0xEA, 0x04EA, 0x81F, 0x8EA, 0xC1F, 0xCEA, 0x101F, 0x104B, 0x10EA,
                0x144B, 0x14EA, 0x18EA, 0x284B, 0x28EB, 0x30EB, 0x34EB, 0x38EB, 0x3CEB, 0x4C24)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [PathingKickoutAddrVarsPlacement(mask:0x1F, shift: 0x0)]
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown, Scene.SwampSpiderHouse)]
        [EnemizerScenesExcluded(Scene.PiratesFortressRooms)] // because the ones in the hookshot room need to stay around
        PatrollingPirate = 0x21E, // En_Ge2

        // romani talking to cremia only
        [FileID(502)]
        [ObjectListIndex(0xB7)]
        En_Ma_Yts = 0x21F, // En_Ma_Yts

        [FileID(503)]
        [ObjectListIndex(0xA7)]
        En_Ma_Yto = 0x220, // En_Ma_Yto
        
        [FileID(504)]
        [ObjectListIndex(0x205)]
        Obj_Tokei_Turret = 0x221, // Obj_Tokei_Turret
        
        [FileID(505)]
        [ObjectListIndex(0x184)]
        Bg_Dblue_Elevator = 0x222, // Bg_Dblue_Elevator

        // lame: saves take you to the real spawn, owl soar takes you to the real spawn, this only lets us activate and save warp
        [ActorizerEnabled] 
        [FileID(506)]
        [ObjectListIndex(0x170)]
        // 0 is great bay coast, 1 is cape, 2 is snowhead, 3 is mountain village, 4 is SCT,
        //5 is milk road, 6 is woodfall, 7 is southern swamp, 8 is ikana canyon, 9 is stonetower
        // F is WCT, is also found in woodfall, cleared swamp?
        [GroundVariants(0, 1, 3, 2, 6, 5, 7, 8, 9, 0xF)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.SouthClockTown, Scene.MilkRoad, Scene.WestClockTown,
             Scene.Woodfall, Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.MountainVillage, Scene.MountainVillageSpring, Scene.Snowhead,
             Scene.GreatBayCoast, Scene.ZoraCape, Scene.IkanaCanyon, Scene.StoneTower, Scene.InvertedStoneTower)] 
        OwlStatue = 0x223, // Obj_Warpstone

        [FileID(507)]
        [ObjectListIndex(0x206)]
        En_Zog = 0x224, // En_Zog
        
        [FileID(508)]
        [ObjectListIndex(0x207)]
        Obj_Rotlift = 0x225, // Obj_Rotlift
        
        [FileID(509)]
        [ObjectListIndex(0x1F8)]
        Obj_Jg_Gakki = 0x226, // Obj_Jg_Gakki
        
        [FileID(510)]
        [ObjectListIndex(0x20C)]
        Bg_Inibs_Movebg = 0x227, // Bg_Inibs_Movebg

        [ActorizerEnabled]
        [FileID(511)]
        [ObjectListIndex(0xD0)]
        // 0xXX00 is path, 0x00YY is type
        // type 0 is the one asking if you got a bottle from beavers on beach
        // FC08 is the guitar tuner, FC07 is the picture buyer
        // 09 is lights are off guy
        // 2 is standing guard in front of mikaus room
        // 3/4 probably also guarding door
        // 5 is creep trying to break into lulus room
        // 6 is sitting waiting for the rehersal
        // 0x12 is missing, checks for a flag makes snese
        // 0x13/14/15 is jamming at the jazz session cutscene
        // 0x140A is near the entrance
        [WaterVariants(0xFC00, 0xFC08, 0xFC07, 0xFC06, 0xFC13, 0xFC14, 0xFC15)] // no reason we cant talk to them underwater I dont think
        [GroundVariants(0xFC08, 0xFC07, 0xFC06, 0xFC13, 0xFC14, 0xFC15)]
        //[GroundVariants(0xFC0B)] // testing dialogue without pathing
        [PathingVariants(0x140A, 0xFC05, 0x2, 0x3, 0x4)]
        [VariantsWithRoomMax(max: 1, variant: 0x2, 0x3, 0x4)]
        // TODO add pathing thing
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [EnemizerScenesExcluded(Scene.ZoraHall, Scene.ZoraCape)]
        [UnkillableAllVariants]
        RegularZora = 0x228, // En_Zot
        
        [FileID(512)]
        [ObjectListIndex(0x20D)]
        Obj_Tree = 0x229, // Obj_Tree
        
        [FileID(513)]
        [ObjectListIndex(0x20E)]
        Obj_Y2lift = 0x22A, // Obj_Y2lift
        
        [FileID(514)]
        [ObjectListIndex(0x20E)]
        Obj_Y2shutter = 0x22B, // Obj_Y2shutter
        
        [FileID(515)]
        [ObjectListIndex(0x20E)]
        Obj_Boat = 0x22C, // Obj_Boat

        [FileID(516)]
        [ObjectListIndex(0x250)]
        Obj_Taru = 0x22D, // Obj_Taru
        
        [FileID(517)]
        [ObjectListIndex(0x23D)]
        Obj_Hunsui = 0x22E, // Obj_Hunsui
        
        [FileID(518)]
        [ObjectListIndex(0x18E)]
        En_Jc_Mato = 0x22F, // En_Jc_Mato

        [FileID(519)]
        [ObjectListIndex(0x87)]
        Mir_Ray3 = 0x230, // Mir_Ray3

        [ActorizerEnabled] // BUG: do not teach him song or cursed
        [FileID(520)]
        [ObjectListIndex(0x211)]
        [GroundVariants(0)]
        [WaterVariants(0)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [EnemizerScenesExcluded(Scene.ZoraHallRooms)]
        [OnlyOneActorPerRoom]
        Japas = 0x231, // En_Zob

        [FileID(521)]
        [ObjectListIndex(0x1)]
        Elf_Msg6 = 0x232, // Elf_Msg6

        // this appears to be more than just peek hole, the actor gets used for other things
        //[ActorizerEnabled]
        // this actor does NOT have the face as a visibleobject, its invisible actor, the mask is part of the scene wall
        [FileID(522)]
        [ObjectListIndex(1)]
        [WallVariants(0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.CuriosityShop)]
        PeekHole = 0x233,

        [ActorizerEnabled]
        [FileID(523)]
        [ObjectListIndex(0x23A)]
        [GroundVariants(0x050B)]
        [VariantsWithRoomMax(max:0, variant:0x050B)] // we dont want a sitting npc to be placed places, just replace
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.MilkBar)]
        Toto = 0x234, // En_Toto // manager zora band member

        [EnemizerEnabled] // does not spawn outside of ikana
        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [FileID(524)]
        [ObjectListIndex(0x75)]
        [PathingVariants(0, 0x81, 0x82, 0x83, 0x84, 0x85)]
        [PathingTypeVarsPlacement(mask:0xFF00, shift:8)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.IkanaCanyon)] // dont replace the train
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        GibdoIkana = 0x235, // En_Railgibud

        [FileID(525)]
        [ObjectListIndex(0xDF)]
        En_Baba = 0x236, // En_Baba

        //[ActorizerEnabled] // does not spawn, even the daytime frollicing one
        // both of his vars are paths, sooo I'm guessing his behavior is hard coded
        [ObjectListIndex(0xE3)]
        [FileID(526)]
        // dont replace any: for one, this object is used by multiple actors
        // can't replace the one in west clocktown without killing bank
        // can't replace the one in ikana without killing the kafei quest (even if they are different rooms)
        [PathingVariants(0x85FF)]
        [PathingTypeVarsPlacement(mask:0x7E00, shift:9)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.NorthClockTown, Scene.WestClockTown, Scene.IkanaCanyon)]
        Sakon = 0x237, // En_Suttari

        [ActorizerEnabled]
        [FileID(527)]
        [ObjectListIndex(0x216)]
        [GroundVariants(0xFE0F)]
        [WaterVariants(0xFE0F, 0xFE02, 0xFE01)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        Tijo = 0x238, // En_Zod // drummer zora band member

        [ActorizerEnabled]
        [FileID(528)]
        [ObjectListIndex(0x263)]
        [WallVariants(0xFF)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.LotteryShop)]
        LotteryKiosk = 0x239, // En_Kujiya

        [FileID(529)]
        [ObjectListIndex(0xA1)]
        En_Geg = 0x23A, // En_Geg

        //[ActorizerEnabled] // boring since its hidden unless you wear one often junk mask, just decreases chances of noticable enemies
        [FileID(530)]
        [ObjectListIndex(1)]
        [GroundVariants(0x7F)]
        [UnkillableAllVariants]
        MushroomCloud = 0x23B, // Obj_Kinoko

        [FileID(531)]
        [ObjectListIndex(0x218)]
        Obj_Yasi = 0x23C, // Obj_Yasi
 
        [EnemizerEnabled]
        [FileID(532)]
        [ObjectListIndex(1)] // even thought this enemy is only in one temple, its a gameplay_keep actor?
        // woodfall swarms include: 1,2,3,4,7,A, sure is a lot of variety for a one-off variant
        [FlyingVariants(1,2,3,4,7)] // A would be 8+4?
        //[AlignedCompanionActor(Torch, CompanionAlignment.Above,
        //    variant: )] // 
        [OnlyOneActorPerRoom]
        [RespawningAllVariants] // they do NOT respawn, but they do block clear all rooms
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.Snowhead)]
        MothSwarm = 0x23D,  // En_Tanron1

        [FileID(533)]
        [ObjectListIndex(0x15D)]
        En_Tanron2 = 0x23E, // En_Tanron2

        //[EnemizerEnabled] // just crashes, probably wants to be spawned with parent big fish gyorg
        [FileID(534)]
        [ObjectListIndex(0x15C)] // this is gyorgs object, probably too big, but our code would handle that
        //[WaterVariants(0)] // vars unknown, 0x0 crashes
        GyorgSpawn = 0x23F, // En_Tanron3

        [FileID(535)]
        [ObjectListIndex(0x21E)]
        Obj_Chan = 0x240, // Obj_Chan
        
        [FileID(536)]
        [ObjectListIndex(0x220)]
        En_Zos = 0x241, // En_Zos

        [ActorizerEnabled]
        [FileID(537)]        
        [ObjectListIndex(0xA1)]
        // 9 is the one that sells you kegs
        [GroundVariants(9, 0x1E0, 1, 8)]
        [VariantsWithRoomMax( max: 1, 9, 0x1E0, 1)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.BombShop, Scene.GoronShrine)]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        Goron = 0x242, // En_S_Goro

        //[ActorizerEnabled] // does not spawn, time varibles? second required object?
        [FileID(538)]
        [ObjectListIndex(0x4)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 1, variant: 0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.StockPotInn)]
        AnjusGrandma = 0x243, // En_Nb

        [ActorizerEnabled]
        [FileID(539)]
        [ObjectListIndex(0xE3)]
        [GroundVariants(1,0)]
        [UnkillableAllVariants]
        Jugglers = 0x244, // En_Ja

        [FileID(540)]
        [ObjectListIndex(0x5C)]
        Bg_F40_Block = 0x245, // Bg_F40_Block
        
        [FileID(541)]
        [ObjectListIndex(0x222)]
        ElegyStatueSwitch = 0x246, // Bg_F40_Switch
        
        [FileID(542)]
        [ObjectListIndex(0x5D)]
        En_Po_Composer = 0x247, // En_Po_Composer

        [ActorizerEnabled]
        [ObjectListIndex(0xFF)]
        [FileID(543)]
        // 00 is the version from the inn, "dont talk to her shes thinking" meaning the rosa sister
        // 01 is laundry pool, but he only spawns at night, ignoring actor time spawn settings for a scene
        // 02 doesn't ever seen to spawn, day or night, think its a fluke
        [GroundVariants(0x0)] 
        [UnkillableAllVariants]
        //[EnemizerScenesExcluded(0x15, 0x70, 0x61)]
        [EnemizerScenesExcluded(Scene.StockPotInn, Scene.LaundryPool, Scene.MilkBar)] // think him being in milkbar is a credits thing
        GuruGuru = 0x248, // En_GuruGuru

        [FileID(544)]
        [ObjectListIndex(0x1)]
        SonataEffects = 0x249, // Oceff_Wipe5

        [ActorizerEnabled]
        [FileID(545)]
        [ObjectListIndex(0x1B6)]
        [GroundVariants(0)] //unk
        [VariantsWithRoomMax(max: 1, variant:0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.RoadToIkana)]
        Shiro = 0x24A, // En_Stone_heishi

        [FileID(546)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe6 = 0x24B, // Oceff_Wipe6
        
        [FileID(547)]
        [ObjectListIndex(0x1E5)]
        En_Scopenuts = 0x24C, // En_Scopenuts
        
        [FileID(548)]
        [ObjectListIndex(0x6)]
        En_Scopecrow = 0x24D, // En_Scopecrow
        
        [FileID(549)]
        [ObjectListIndex(0x1)]
        Oceff_Wipe7 = 0x24E, // Oceff_Wipe7
        
        [FileID(550)]
        [ObjectListIndex(0x229)]
        Eff_Kamejima_Wave = 0x24F, // Eff_Kamejima_Wave
        
        [FileID(551)]
        [ObjectListIndex(0x22A)]
        PamelasFatherCured = 0x250, // En_Hg
        
        [FileID(552)]
        [ObjectListIndex(0x22A)]
        PamelasFatherCursed = 0x251, // En_Hgo

        [ActorizerEnabled]
        [FileID(553)]
        [ObjectListIndex(0x22B)]
        // E01 is rehersal
        [GroundVariants(0xFE0F)]
        [WaterVariants(0xFE0F)]
        [VariantsWithRoomMax(max:0, variant:0xE01)] // failure to spawn
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.ZoraCape)]
        Lulu = 0x252, // Ee_Zov

        // probably boring actor with timed flags nonsense
        [FileID(554)]
        [ObjectListIndex(0x7)]
        AnjusMother = 0x253, // En_Ah
        
        [FileID(555)]
        [ObjectListIndex(0x22C)]
        Obj_Hgdoor = 0x254, // Obj_Hgdoor
        
        [FileID(556)]
        [ObjectListIndex(0x203)]
        Bg_Ikana_Bombwall = 0x255, // Bg_Ikana_Bombwall
        
        [FileID(557)]
        [ObjectListIndex(0x203)]
        Bg_Ikana_Ray = 0x256, // Bg_Ikana_Ray
        
        [FileID(558)]
        [ObjectListIndex(0x203)]
        Bg_Ikana_Shutter = 0x257, // Bg_Ikana_Shutter
        
        [FileID(559)]
        [ObjectListIndex(0x1E0)]
        Bg_Haka_Bombwall = 0x258, // Bg_Haka_Bombwall
        
        [FileID(560)]
        [ObjectListIndex(0x1E0)]
        Bg_Haka_Tomb = 0x259, // Bg_Haka_Tomb
        
        [FileID(561)]
        [ObjectListIndex(0x1)]
        En_Sc_Ruppe = 0x25A, // En_Sc_Ruppe
        
        [FileID(562)]
        [ObjectListIndex(0x237)]
        Bg_Iknv_Doukutu = 0x25B, // Bg_Iknv_Doukutu
        
        [FileID(563)]
        [ObjectListIndex(0x237)]
        Bg_Iknv_Obj = 0x25C, // Bg_Iknv_Obj

        [FileID(564)]
        [ObjectListIndex(0x238)]
        En_Pamera = 0x25D, // En_Pamera
        
        [FileID(565)]
        [ObjectListIndex(0x239)]
        Obj_HsStump = 0x25E, // Obj_HsStump

        //[ActorizerEnabled] // doesn't spawn with a flower, looks silly
        // HEY the other actors, that make flowers, we can use them for dual deku, would require custom code to put them on top of each other
        [FileID(566)]
        [ObjectListIndex(0x12B)]
        [GroundVariants(0x584)]
        [UnkillableAllVariants] // I think?
        SleepingScrub = 0x25F,

        [ActorizerEnabled]
        [FileID(567)]
        [ObjectListIndex(0xD0)]
        [WaterVariants(0,1)]
        [UnkillableAllVariants]
        SwimmingZora = 0x260, // En_Zow

        [FileID(568)]
        [ObjectListIndex(0x1)]
        En_Talk = 0x261, // En_Talk

        [FileID(569)]
        [ObjectListIndex(0xD)]
        En_Al = 0x262, // En_Al

        //[ActorizerEnabled] // does not spawn, reason unknown
        [FileID(570)]
        [ObjectListIndex(0x13)]
        [GroundVariants(0)] // might be time gated though
        [UnkillableAllVariants]
        Bartender = 0x263, // En_Tab

        [FileID(571)]
        [ObjectListIndex(0xE3)]
        En_Nimotsu = 0x264, // En_Nimotsu
        
        [FileID(572)]
        [ObjectListIndex(0x1)]
        En_Hit_Tag = 0x265, // En_Hit_Tag
        
        [FileID(573)]
        [ObjectListIndex(0x6)]
        En_Ruppecrow = 0x266, // En_Ruppecrow

        [ActorizerEnabled] // spawned for me, but not streamers? weird time dependencies?
        [FileID(574)]
        [ObjectListIndex(0x23F)]
        [FlyingVariants(7, 5)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 2, variant: 7, 5)] // > severe lag over 10
        //[EnemizerScenesExcluded(Scene.GreatBayCoast, Scene.ZoraCape)]
        Seagulls = 0x267, // En_Tanron4

        [FileID(575)]
        [ObjectListIndex(0x15B)]
        En_Tanron5 = 0x268, // En_Tanron5

        //[ActorizerEnabled] // didn't spawn with 0, don't know what vars work
        [FileID(576)]
        [ObjectListIndex(0x1EB)]
        // m2c suggests either param 1 or 2 could work
        // 1 spins in circles like giant bee, 2 did not spawn
        [FlyingVariants(2)]
        BeeSwarm = 0x269, // En_Tanron6

        [FileID(577)]
        [ObjectListIndex(0xF1)]
        En_Daiku2 = 0x26A, // En_Daiku2

        [ActorizerEnabled]
        [FileID(578)]
        [ObjectListIndex(0xF0)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:0, variant:0)] // hard coded only spawn final night
        [EnemizerScenesExcluded(Scene.MayorsResidence)]
        Mutoh = 0x26B,

        [FileID(579)]
        [ObjectListIndex(0x247)]
        En_Baisen = 0x26C, // En_Baisen
        
        [FileID(580)]
        [ObjectListIndex(0x1B6)]
        En_Heishi = 0x26D, // En_Heishi
        
        [FileID(581)]
        [ObjectListIndex(0x1B6)]
        En_Demo_heishi = 0x26E, // En_Demo_heishi
        
        [FileID(582)]
        [ObjectListIndex(0x241)]
        En_Dt = 0x26F, // En_Dt

        [ActorizerEnabled]
        [FileID(583)]
        [ObjectListIndex(0x243)]
        [UnkillableAllVariants]
        [GroundVariants(0xF)] // only one toof
        [VariantsWithRoomMax(max: 10, variant: 0xF)]
        [EnemizerScenesExcluded(Scene.LaundryPool)]
        LaundryPoolBell = 0x270, // En_Cha

        [FileID(584)]
        [ObjectListIndex(0x244)]
        Obj_Dinner = 0x271, // Obj_Dinner
        
        [FileID(585)]
        [ObjectListIndex(0x246)]
        Eff_Lastday = 0x272, // Eff_Lastday
        
        [FileID(586)]
        [ObjectListIndex(0x203)]
        Bg_Ikana_Dharma = 0x273, // Bg_Ikana_Dharma

        // without flower under him I bet he looks stupid
        [FileID(587)]
        [ObjectListIndex(0x1E5)]
        BuisnessScrub = 0x274, // En_AkinDonuts

        [FileID(588)]
        [ObjectListIndex(0x1BE)]
        Eff_Stk = 0x275, // Eff_Stk

        // todo: test randomizing
        [FileID(589)]
        [ObjectListIndex(0x1E5)]
        //ObjectListIndex(0x1D5)]
        LinkTheGoro = 0x276,

        [FileID(590)]
        [ObjectListIndex(0xA1)]
        En_Rg = 0x277, // En_Rg
        
        [FileID(591)]
        [ObjectListIndex(0x249)]
        En_Osk = 0x278, // En_Osk

        [ActorizerEnabled]
        [FileID(592)]
        [ObjectListIndex(0x26A)] // the spreadsheet says he is obj 1 but that is a mistake, his code is stupid
        // uhhh code has no params, where did FE01 come from?
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max:4, variant:0x0)]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.TerminaField)]
        // This is the seth you see in the telescope on grottos, same animation as cured skultula man in kak
        Seth2 = 0x279, // En_Sth2

        [FileID(593)]
        [ObjectListIndex(0x24A)]
        En_Yb = 0x27A, // En_Yb

        [ActorizerEnabled]
        [ObjectListIndex(0x24B)]
        [FileID(594)]
        // 0xA00 is lobby pacing
        // params: 8000 is a talking flag, 0x7E00 >> 9 is pathing, 0x7E00 is non-pathing though, the one value
        [GroundVariants(0x7E01, 0x8000, 0xFE01, 0x7E02, 0xFE02, 0x7E02)]
        [PathingVariants(0xA00, 0x7E01, 0x8000, 0xFE01, 0x7E02)]
        [PathingTypeVarsPlacement(0x7E00, 9)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesExcluded(Scene.WestClockTown)]
        RosaSisters = 0x27B, // En_Rz

        [FileID(595)]
        [ObjectListIndex(0x1)]
        En_Scopecoin = 0x27C, // En_Scopecoin
        
        [FileID(596)]
        [ObjectListIndex(0x24F)]
        En_Bjt = 0x27D, // En_Bjt
        
        [FileID(597)]
        [ObjectListIndex(0x110)]
        En_Bomjima = 0x27E, // En_Bomjima
        
        [FileID(598)]
        [ObjectListIndex(0x110)]
        En_Bomjimb = 0x27F, // En_Bomjimb
        
        [FileID(599)]
        [ObjectListIndex(0x110)]
        En_Bombers = 0x280, // En_Bombers
        
        [FileID(600)]
        [ObjectListIndex(0x110)]
        En_Bombers2 = 0x281, // En_Bombers2

        //[ActorizerEnabled] // broken: if you pop it, it locks you in a never ending cutscene
        [FileID(601)]
        [ObjectListIndex(0x280)]
        [FlyingVariants(0)]
        //[UnkillableAllVariants]  // untested
        MajoraBalloonNCT = 0x282,

        [FileID(602)]
        [ObjectListIndex(0x1B1)]
        Obj_Moon_Stone = 0x283, // Obj_Moon_Stone
        
        [FileID(603)]
        [ObjectListIndex(0x1)]
        Obj_Mu_Pict = 0x284, // Obj_Mu_Pict
        
        [FileID(604)]
        [ObjectListIndex(0x236)]
        Bg_Ikninside = 0x285, // Bg_Ikninside
        
        [FileID(605)]
        [ObjectListIndex(0x255)]
        Eff_Zoraband = 0x286, // Eff_Zoraband

        //[ActorizerEnabled] // crash, bg code? like majora baloon then
        [FileID(606)]
        [ObjectListIndex(0x256)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        GormanBuilding = 0x287, // Obj_Kepn_Koya
        
        [FileID(607)]
        [ObjectListIndex(0x257)]
        BarnRoof = 0x288, // Obj_Usiyane

        [ActorizerEnabled] // shows up too much tho, because small object
        [FileID(608)]
        [ObjectListIndex(0x25B)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 7)] // fairy fountain
        [OnlyOneActorPerRoom]
        ButlersSon = 0x289, // En_Nnh

        [FileID(609)]
        [ObjectListIndex(0x260)]
        Obj_Kzsaku = 0x28A, // Obj_Kzsaku
        
        [FileID(610)]
        [ObjectListIndex(0x261)]
        Obj_Milk_Bin = 0x28B, // Obj_Milk_Bin
        
        [FileID(611)]
        [ObjectListIndex(0x264)]
        En_Kitan = 0x28C, // En_Kitan
        
        [FileID(612)]
        [ObjectListIndex(0x267)]
        Bg_Astr_Bombwall = 0x28D, // Bg_Astr_Bombwall
        
        [FileID(613)]
        [ObjectListIndex(0x236)]
        Bg_Iknin_Susceil = 0x28E, // Bg_Iknin_Susceil
        
        [FileID(614)]
        [ObjectListIndex(0x268)]
        En_Bsb = 0x28F, // En_Bsb

        [ActorizerEnabled]
        [FileID(615)]
        [ObjectListIndex(0x129)]
        [GroundVariants(5)]
        [UnkillableAllVariants]
        Secretary = 0x290, // En_Recepgirl

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2E30)]
        [FileID(616)]
        [ObjectListIndex(0x22)]
        //[FlyingVariants(0, 1)] // two? one that steals and one that doesn't?
        [FlyingVariants(0)] // zero seems safe, does not steal sword or anything, 1 does not spawn
        [OnlyOneActorPerRoom]
        [EnemizerScenesExcluded(Scene.TerminaField)] // do not remove original, esp with rupeeland coming soon
        Takkuri = 0x291,

        [FileID(617)]
        [ObjectListIndex(0x1A9)]
        En_Jgame_Tsn = 0x292, // En_Jgame_Tsn
        
        [FileID(618)]
        [ObjectListIndex(0x80)]
        Obj_Jgame_Light = 0x293, // Obj_Jgame_Light

        [ActorizerEnabled]
        [FileID(619)]
        [ObjectListIndex(0x26E)]
        [WallVariants(0x2)]
        [UnkillableAllVariants]
        Windows = 0x294,

        [FileID(620)]
        [ObjectListIndex(0x26F)]
        Demo_Syoten = 0x295, // Demo_Syoten
        
        [FileID(621)]
        [ObjectListIndex(0x270)]
        Demo_Moonend = 0x296, // Demo_Moonend
        
        [FileID(622)]
        [ObjectListIndex(0x27F)]
        Bg_Lbfshot = 0x297, // Bg_Lbfshot
        
        [FileID(623)]
        [ObjectListIndex(0x234)]
        Bg_Last_Bwall = 0x298, // Bg_Last_Bwall

        [ActorizerEnabled]
        [FileID(624)]
        [ObjectListIndex(0x15)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [CompanionActor(Flame, 0x7FE)] // blue flames
        AnjuWeddingDress = 0x299,

        [FileID(625)]
        [ObjectListIndex(0x1)]
        En_Invadepoh_Demo = 0x29A, // En_Invadepoh_Demo
        
        [FileID(626)]
        [ObjectListIndex(0x273)]
        Obj_Danpeilift = 0x29B, // Obj_Danpeilift
        
        [FileID(627)]
        [ObjectListIndex(0x269)]
        En_Fall2 = 0x29C, // En_Fall2
        
        [FileID(628)]
        [ObjectListIndex(0xD)]
        Dm_Al = 0x29D, // Dm_Al
        
        [FileID(629)]
        [ObjectListIndex(0xE2)]
        Dm_An = 0x29E, // Dm_An
        
        [FileID(630)]
        [ObjectListIndex(0x7)]
        Dm_Ah = 0x29F, // Dm_Ah
        
        [FileID(631)]
        [ObjectListIndex(0x4)]
        Dm_Nb = 0x2A0, // Dm_Nb


        //[ActorizerEnabled]
        [FileID(632)]
        [ObjectListIndex(0x18)] // might also need the sunmask object
        // 0 does not spawn, might need another object
        [GroundVariants(0)]
        [UnkillableAllVariants]
        DressMannequin = 0x2A1,

        [FileID(633)]
        [ObjectListIndex(0x241)]
        En_Ending_Hero = 0x2A2, // En_Ending_Hero
        
        [FileID(634)]
        [ObjectListIndex(0x185)]
        Dm_Bal = 0x2A3, // Dm_Bal
        
        [FileID(635)]
        [ObjectListIndex(0x185)]
        En_Paper = 0x2A4, // En_Paper

        // seriously? this is a different actor?
        // they are all sitting down though, so boring for now until we add actor moving for ledge sitting
        [FileID(636)]
        [ObjectListIndex(0x142)]
        StalchildHintGiver = 0x2A5,

        [FileID(637)]
        [ObjectListIndex(0x1)]
        Dm_Tag = 0x2A6, // Dm_Tag

        //[ActorizerEnabled] // did not spawn
        [FileID(638)]
        [ObjectListIndex(0x2A7)] // for some reason my obj size lookup code thinks this is HUGE
        [FlyingVariants(0xF)] // these might be pathing actors
        [UnkillableAllVariants]
        MoonBirdsBrown = 0x2A7,

        [FileID(639)]
        [ObjectListIndex(0x247)]
        En_Ending_Hero2 = 0x2A8, // En_Ending_Hero2
        
        [FileID(640)]
        [ObjectListIndex(0xF0)]
        En_Ending_Hero3 = 0x2A9, // En_Ending_Hero3
        
        [FileID(641)]
        [ObjectListIndex(0x1B6)]
        En_Ending_Hero4 = 0x2AA, // En_Ending_Hero4
        
        [FileID(642)]
        [ObjectListIndex(0xF1)]
        En_Ending_Hero5 = 0x2AB, // En_Ending_Hero5
        
        [FileID(643)]
        [ObjectListIndex(0x1)]
        En_Ending_Hero6 = 0x2AC, // En_Ending_Hero6
        
        [FileID(644)]
        [ObjectListIndex(0xE2)]
        Dm_Gm = 0x2AD, // Dm_Gm
        
        [FileID(645)]
        [ObjectListIndex(0x1)]
        Obj_Swprize = 0x2AE, // Obj_Swprize
        
        //[ActorizerEnabled]
        [FileID(646)]
        [GroundVariants(0)] // todo search
        [ObjectListIndex(0x1)] // ooo free
        [UnkillableAllVariants]
        En_Invisible_Ruppe = 0x2AF, // En_Invisible_Ruppe
        
        [FileID(647)]
        [ObjectListIndex(0x281)]
        Obj_Ending = 0x2B0, // Obj_Ending

        [FileID(648)]
        [ObjectListIndex(0x12C)]
        En_Rsn = 0x2B1, // En_Rsn

/*
        [ActorizerEnabled]
        [ObjectListIndex(0x264)]
        [AlignedCompanionActor(KeatonGrass, CompanionAlignment.OnTop, ourVariant: -1,
           variant: 0x7F00, 0x400, 0x1F00)] // we want him to only show up if you kill grass right?
        // summoning 0 crashes, so does 0xFFFF
        [GroundVariants(0xFFFF)] // unkown, normally summoned by bushes?
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        // for now, don't place in really small scenes where there is almost never enough space for keaton and keaton grass to spawn normally
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown, Scene.ClockTowerInterior, Scene.NorthClockTown, Scene.MountainVillageSpring, Scene.TwinIslands, Scene.OceanSpiderHouse)]
        //Keaton = 0x289C, // En_Kitan
        Keaton = 0x2B1, // THIS IS A LIE, its a trick until I can force second objects to load, but we do NOT want the actual actor to load for keaton
        // 2b1 is really bomb shop proprietor
*/

        [FileID(1114)]
        //[ObjectListIndex(0)]
        [ObjectListIndex(0xF3)] // this object is size 0x10 its the smallest object
        [GroundVariants(0), FlyingVariants(0), WaterVariants(0)]
        [UnkillableAllVariants]
        Empty = -1
    }

    public enum ActorType
    {
        Unset   = 0,
        Water   = 1,
        Ground  = 2,
        Flying  = 3,
        Wall    = 4,
        Pathing = 5,
    }
}
