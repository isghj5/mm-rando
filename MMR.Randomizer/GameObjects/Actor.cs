using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Actor;

namespace MMR.Randomizer.GameObjects
{
    public static class ActorConst
    {
        public const int ANY_VARIANT = 0xFFFFF; // variants are s16, this is too long to be a valid value
        public const int ANY_SCENE = -1;
    }

    //[System.Diagnostics.DebuggerDisplay("{ActorExtensions.GetActorName(this)}")] // useless for enums
    public enum Actor
    {
        /// the main enumator value is the vanilla actor list ID

        // warning: companion actors can bypass variants that exist, you might remove a variant but it still exists as a companion
        // fixing this is a performance loss, just dont be stupid

        //[ActorizerEnabled] // crashes, probably because there are now two players
        [FileID(38)]
        //[GroundVariants(0x0E02)] // sitting in clocktown?
        Player = 0x0,
        NULL = 0x0, // since we cant use player actor normally, leave this as NULL

        [FileID(39)]
        [ObjectListIndex(1)]
        [GroundVariants(0x7FF, 0xFFFF)] // params: > 0 and else, -1
        // not stalfos in this game
        // like, goron punch crator, or moon tear
        En_Test = 0x1, // En_Test

        // invididual shop items
        [FileID(40)]
        [ObjectListIndex(0x1)]
        En_GirlA = 0x2, // En_GirlA

        // enemy body parts spawned during death?
        [FileID(41)]
        [ObjectListIndex(0x1)]
        En_Part = 0x3, // En_Part

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
        [ForbidFromScene(Scene.Woodfall)]
        Flame = 0x4, // En_Light

        // real fake doors
        //[ActorizerEnabled] // whiners complaining that if you open them you softlock. sounds like a perfect mimick door to me
        [FileID(43)]
        [ObjectListIndex(0x231)]// different doors have different variables for different objects, unless I program multiple objects only one can be used
        [DynaAttributes(12, 8)]
        [WallVariants(0x7F)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.ZoraHall)]
        Door = 0x5, // En_Door

        [ActorizerEnabled]
        [FileID(44)]
        [ObjectListIndex(0xC)]
        [DynaAttributes(12, 8)]
        [CheckRestricted(Scene.RoadToIkana, variant: ActorConst.ANY_VARIANT, // 0x5080,
            Item.ChestToIkanaRedRupee)]
        [CheckRestricted(Scene.EastClockTown, variant: ActorConst.ANY_VARIANT, // 0x50CA,
            Item.ChestEastClockTownSilverRupee)]
        [CheckRestricted(Scene.SouthClockTown, variant: ActorConst.ANY_VARIANT, Item.ChestSouthClockTownPurpleRupee, Item.ChestSouthClockTownRedRupee)]
        // these three are from inverted stone tower, however when placed in TF, 2/3 were invisible chests
        // type: 0x7 seems to be enemy clear, also type 1, 0x5 is woodentype, 0xC is switch activated
        // 0xF000 is type, 0x001F are chest flags, 0x0FE0 would be the item then
        // gomess is 0x27BE, which does not spawn util you kill him, so obviously the top byte is NOT that simple in MM, snowhead is 27BE
        // dont use CM as reference, rando changes how the chests work for item rando to work
        [GroundVariants(0x57BE, 0x59DD, 0x56BF, 0x5FDE, 0x5579,
            0x561E, 0x5C79, 0x5991, 0x5B58, //0x5A1E,
            0x5080, // road to ikana
            0x50CA, // east clock town
            0x50A1, // south clock town
            //0xBAEE, // Invisible with switch activation, this one should be rare (0x10--(large gold) + 0x--11(spawn on switch clear))
            0x0AFB, 0x099C)] // two free, the rest are gold invisible
        [WaterBottomVariants(0x57BE, 0x59DD, 0x56BF, 0x5FDE, 0x5579,
            0x561E, 0x5C79, 0x5991, 0x5B58, //0x5A1E,
            0xBA1E, // switch activated
            0x0AFB, 0x099C)] // two free, the rest are gold invisible
        [VariantsWithRoomMax(max: 1, variant: 0x57BE, 0x59DD, 0x56BF, 0x5FDE, 0x5579,
            0x561E, 0x5C79, 0x5991, 0x5B58, 0x5A1E,
            0x50CA, 0x50A1,
            0x0AFB, 0x099C)] // brown, harder to see in perpheral vision, not invisible
        [VariantsWithRoomMax(max: 0,
            0x5080 // road to ikana
            )]
        //[VariantsWithRoomMax(max: 1, variant: )] // vanilla we do not want to re-place in the world
        [UnkillableAllVariants]
        //[AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
        //    variant: 0x3F5F)] // can place around chests
        //[AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
        //    variant: 2, 9)] // fairies around chests make sense, just not a full fairy fountain
        [ForbidFromScene(Scene.InvertedStoneTower,
            Scene.TerminaField, Scene.PiratesFortressRooms, Scene.PiratesFortress, Scene.PiratesFortressExterior, Scene.TwinIslandsSpring,
            Scene.SouthClockTown, Scene.EastClockTown, Scene.RoadToIkana, // DO NOT RANDOMIZE: itemizer changes params, can fuck with replacement actor
            Scene.Woodfall)]
        [SwitchFlagsPlacementZRot]
        [TreasureFlagsPlacement(mask: 0x1F, shift: 0)]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard, Scene.SouthernSwamp, Scene.SouthernSwampClear, // asummed dyna crash
        //    Scene.StoneTower)]
        [PlacementWeight(50)]
        TreasureChest = 0x6, // En_Box

        [FileID(45)]
        [ObjectListIndex(0x128)]
        PametFrog = 0x7, // En_Pammetfrog the frogminiboss

        [EnemizerEnabled]
        [FileID(46)]
        [ActorInitVarOffset(0x2A60)]
        [ObjectListIndex(0x5)]
        [CheckRestricted(Scene.SouthernSwampClear, ActorConst.ANY_VARIANT, Item.HeartPieceBoatArchery)]
        [WaterTopVariants(0xFF00)] // all vanilla types are the same, however param 0xFF00 and 0xFF are parameters of unkown type
        //[WaterBottomVariants(0xFF01)] // not safe check the params for safe params first
        [ForbidFromScene(//Scene.IkanaCanyon,
            Scene.GreatBayTemple)]
        Octarok = 0x8, // En_Okuta

        [FileID(47)]
        [ObjectListIndex(0x1)]
        BombAndKeg = 0x9, // En_Bom

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B30)]
        [FileID(48)]
        [ObjectListIndex(0x9)]
        //[GroundVariants(1)] //issue: also used for replacement, puts ground enemies in air so we cannot
        [FlyingVariants(1)]
        // 81 is trapped in ice, floats back up to the ceiling after melting
        [GroundVariants(0x81)]
        [BlockingVariants(0x81)]
        [VariantsWithRoomMax(max: 5, variant: 1)] // have to limit because it can block and I don't have variant blocking
        [DifficultAllVariants]
        [RespawningVariants(variant: 0x81)] // if they fly away after melt they might not come down (bug), so not killable
        [SwitchFlagsPlacement(mask: 0xFF, shift: 8)]
        //[EnemizerScenesPlacementBlock(Scene.TerminaField)] // temporary, melting them can unmelt the north ice block, but why
        WallMaster = 0xA, // En_Wallmas

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A40)]
        [FileID(49)]
        [ObjectListIndex(0xA)]
        [GroundVariants(0x0, 0x1)]
        [DifficultVariants(0x1)]
        [VariantsWithRoomMax(max: 1, variant: 0x1)]
        [VariantsWithRoomMax(max: 2, variant: 0x0)] // 3 is enough, it can lag rooms as is
        [CompanionActor(Flame, ourVariant: -1, variant: 0x7F4)] // they're teething
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // too big, can block the butler race
        Dodongo = 0xB, // En_Dodongo

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1D60)]
        [FileID(50)]
        [ObjectListIndex(0xB)]
        // params type: 0 is fire, 2 is normal, 3 is perched, 4 is ice
        // 0x8000 is invisible
        // type 2 can be perching? I saw 2 and 8002 in a grotto sittin in the air
        [FlyingVariants(0x0, 0x2, 0x04, 0x8000, 0x8002, 0x8004)] // which ones are fire and ice?
        [PerchingVariants(0x8103, 0x103)] // 0x100 is not a valid vanilla value, 0x7FFF is type, but the game uses 0xF range, so I modded
        [WallVariants(0x8003, 0x3)] // will take off and attack within 120 units distance (xz)
        [DifficultVariants(0x8000, 0x4, 0x8004, 0x8002)]
        [FlyingToGroundHeightAdjustment(150)]
        Keese = 0xC, // En_Firefly

        // cannot be easily randomized away from horse because horse object is not part of the scene list
        //[ActorizerEnabled]
        [FileID(51)]
        [ObjectListIndex(1)]
        // 400E is child cutscene, should just sit there and neigh
        // Gorman track has a lot??
        //  0, 0100, 96FF, 4605, 3c05, 4605, 9605,5005
        // 96FF/3c05 did not spawn in field, 5005/4605 are ridable epona, both can exist and are ridable
        // experimental: 9605 did not spawn, neither did 0x3C00, 
        // 46FF did spawn just fine
        // 0/100 are crash I think
        [GroundVariants(0x400E, // new day cutscene, standing behind link
            0x4600, 0x5005)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:0, variant: 0x400E, 0x4600, 0x5005)] // cannot place: weird crashes and things, but we can remove
        // if you leave or enter a room after spawning epona you crash, not sure why, but so far the known areas are all dungeons
        // also you crash if you enter a diffent room (southern swamp) without epona song
        //[EnemizerScenesPlacementBlock(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple)]
        [ForbidFromScene(Scene.RomaniRanch)]
        //[OnlyOneActorPerRoom]
        Horse = 0xD, // En_Horse

        [ObjectListIndex(0x1)]
        [UnkillableAllVariants]
        [ActorInstanceSize(0x1A8)]
        Item00 = 0xE, // En_Item00

        [FileID(52)]
        [ObjectListIndex(0x1)]
        Arrow = 0xF, // En_Arrow

        [ActorizerEnabled]
        [FileID(53)]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        // TODO find more, there are a lot of params here
        // 4 is group of fairies out of a fountain, 6 is spawned by 4
        // 7 is large healing fairy, 9 is yellow fairy that sets 1000 (unused) A is also yellow, does not set 1000
        // 4 has been removed because its unlikely to make sense for the location, and if its in TF it might contribute hard to TFG
        [GroundVariants(2, 7, 9)]
        [FlyingVariants(2, 7, 9)]
        //[VariantsWithRoomMax(max: 1, variant: 4)] // don't create too many fairy fountains
        [VariantsWithRoomMax(max: 2, variant: 7)] // maybe limit the secret menu fairies
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.BeneathTheWell)] // dont remove from well
        Fairy = 0x10, // En_Elf

        [ActorizerEnabled] // now that they are testy, lets count them as enemies
        [FileID(54)]
        [ObjectListIndex(0xF)]
        // all variants less than zero get turned into zero, so we can add ones 
        [GroundVariants(0x0, 0xFFFF)] // FFFF is in ranch barn
        [FlyingVariants(0xFEEE)] // non-vanilla, want to see how they do if they spawn on flying, do they fall from the sky like normal?
        [VariantsWithRoomMax(max: 6, variant: 0xFFFF, 0x0, 0xFEEE)]
        [UnkillableAllVariants]
        // I would like a flying variant, but they seem to drop like a rock instead of float down
        //[ForbidFromScene(0x15, Scene.AstralObservatory, 0x35, 0x42, 0x10)]
        [ForbidFromScene(Scene.AstralObservatory, Scene.RomaniRanch, Scene.CuccoShack, Scene.MilkBar)]
        FriendlyCucco = 0x11, // En_Niw

        [EnemizerEnabled]
        [FileID(55)]
        [ActorInitVarOffset(0x32C0)]
        [ObjectListIndex(0x12)]
        // FE are road to mountain village type AND greatbay type
        // FD is underground, FE can be both but now, for detection, FD is water only
        // FF does not exist in MM vanilla, red variety
        [GroundVariants(0xFFFD, 0xFFFF)]
        [WaterTopVariants(0xFFFE)]
        Tektite = 0x12, // En_Tite

        Empty13 = 0x13,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x24E0)]
        [FileID(56)]
        [ObjectListIndex(0x14)]
        // 0 is the big one in peahat grotto, 1 is the little ones normally only spawned by the big one if you hit them at night
        [GroundVariants(0)]
        [FlyingVariants(1)]
        [DifficultVariants(0, 1)] // honestly the little ones hit for a health and can snipe the player without a shield, worthy
        [VariantsWithRoomMax(max: 3, variant: 0)]
        [VariantsWithRoomMax(max: 7, variant: 1)] // lag, not difficulty
        [BlockingVariants(0)]
        //[EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall)] // too big, can block the butler race
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // too big, can block the butler race
        Peahat = 0x14, // En_Peehat

        // this is single butterfly spawned directly only on the moon
        [ActorizerEnabled]
        [FileID(119)]
        [ObjectListIndex(0x2)]
        // 1 and 2 are vanilla on the moon, 0 is set in init if -1 is params, types unknown
        [GroundVariants(0, 1, 2)]
        [FlyingToGroundHeightAdjustment(100)] // does this even work properly like this? might have to make the separate actor
        [UnkillableAllVariants]
        Butterfly = 0x15, // En_Butte

        [FileID(118)]
        [ObjectListIndex(0x1)]
        Insect = 0x16, // En_Insect

        [ActorizerEnabled] // this is just one fish, if you want a school summon a differemnt mure actor
        [FileID(120)]
        [CheckRestricted(Scene.Grottos, ActorConst.ANY_VARIANT,
            Item.BottleCatchFish
        )]
        [ObjectListIndex(1)]
        // type 0 and 1 are slightly different? I think 0 is dropped from a bottle, a lot of other entries are 1
        // 2 is unk
        [WaterVariants(0, 1)]
        [VariantsWithRoomMax(max:0, variant:0)]
        [UnkillableAllVariants]
        Fish = 0x17, // En_Fish

        // a door type actor
        [FileID(57)]
        [ObjectListIndex(0x1)]
        En_Holl = 0x18, // En_Holl

        // warning: can crash if put on an actor that has cutscene data, fixed by removing cutscene data in ::FixBrokenActorSpawnCutscenes
        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A70)]
        [FileID(58)]
        [ObjectListIndex(0x17)]
        [GroundVariants(0)]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 2, variant: 0)]
        //[ForbidFromScene(Scene.SecretShrine)] // issue: spawn is too high, needs to be lowered
        Dinofos = 0x19, // En_Dinofos

        // Used in snowhead and gorman race
        [ActorizerEnabled]
        [FileID(59)]
        [ObjectListIndex(0x5F)]
        [DynaAttributes(8, 8)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max: 10, variant: 0x0)] // Dyna
        [ForbidFromScene(Scene.Snowhead)] // requested I leave it alone for lullaby skip
        [UnkillableAllVariants]
        [PlacementWeight(90)]
        Flagpole = 0x1A, // En_Hata

        // empty actor, does nothing
        [FileID(60)]
        [ObjectListIndex(0x19)]
        En_Zl1 = 0x1B, // En_Zl1

        // ??? cutscene actors?
        [FileID(61)]
        [ObjectListIndex(0x1)]
        Viewer = 0x1C, // En_Viewer

        [EnemizerEnabled]
        [ActorInitVarOffset(0x5C8)]
        [FileID(62)]
        [ObjectListIndex(0xE)] // object re-used in giants chamber DemoKankyo
        // There are no params, and this actor is unused in vanilla placement
        // This actor is modified by custom MMRA, type 0 is now random count, -1 is single
        [FlyingVariants(0xFFFF)] // 0 works, but OOT used FFFF
        [GroundVariants(0xFFFF)] // 0 works, but OOT used FFFF
        [DifficultVariants(0x2)]
        [PlacementWeight(90)]
        Shabom = 0x1D, // En_Bubble, the flying bubbles from Jabu Jabu, exist only in giants cutscenes

        [FileID(63)]
        [ObjectListIndex(0x1)] // FAKE, multi-object
        // multiple door objects::
        /*  { gBossDoorDL, NULL, 130, 12, 50, 15 },
            { gameplay_keep_DL_077990, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_numa_obj_DL_007150, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_hakugin_obj_DL_000128, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { gGreatBayTempleObjectDoorDL, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_ikana_obj_DL_014A40, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_redead_obj_DL_0001A0, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_ikninside_obj_DL_004440, object_ikninside_obj_DL_005260, 130, 0, 20, 15 },
            { object_random_obj_DL_000190, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_kinsta1_obj_DL_000198, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_kaizoku_obj_DL_0001A0, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
            { object_last_obj_DL_0039C0, gameplay_keep_DL_078A80, 130, 12, 20, 15 },
        */
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        DoorShutter = 0x1E, // Door_Shutter

        Empty1F = 0x1F,

        [FileID(64)]
        [ObjectListIndex(0x1)]
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

        // regular frog as part of the don gero miniquest
        [ActorizerEnabled]
        [FileID(66)]
        [ObjectListIndex(0xBC)]
        [CheckRestricted(Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT, // 0x3,
            Item.HeartPieceChoir, Item.FrogSwamp)]
        [CheckRestricted(Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT, // 0x3,
            Item.HeartPieceChoir, Item.FrogSwamp)]
        [CheckRestricted(Scene.LaundryPool, variant: ActorConst.ANY_VARIANT, // 0x4,
            Item.HeartPieceChoir, Item.FrogLaundryPool)]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT, Item.HeartPieceChoir)]
        [GroundVariants(1, 2, 3, 4,
            0xF,
            0xF0, 0xF1, 0xF2, 0xF3, 0xF4)] // 3 is southern swamp, 4 is laundry pool, the versions in teh mountaion have the F flag, think the rest are numbered
        [VariantsWithRoomMax(max: 1, variant: 1, 2, 3, 4, 0xF)]
        [VariantsWithRoomMax(max: 0, variant: 0xF0, 0xF1, 0xF2, 0xF3, 0xF4)] // spring only
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.LaundryPool)]
        RegularFrogs = 0x22, // En_Minifrog

        Empty23 = 0x23,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2540)]
        [FileID(67)]
        [ObjectListIndex(0x20)]
        // 0x3F params is a switch flag, so long as its 3F switch flag is ignored tho so not a huge deal
        // 0x1C0 & == 1 requires lens (0x7F)
        [CeilingVariants(0xEF, // not vanilla? where did I get this? what is 1C = 3 even do?
            0x7F, // invisible grave2
            0x3F, // spider gossip grotto, woodfall, greatbaytemple, all over the place
            0x4)] // 4 is in the astral observatory, and has a spawn kill flag, so don't use
        [RespawningVariants(0x4)] // doesn't respawn after death, so dont put where respawning enemies are bad either
        [VariantsWithRoomMax(max: 0, variant: 4)] // if this actor hides an item, could be annoying going back in time to reset, so do not place
        //[FlyingToGroundHeightAdjustment(100)] // no longer flying type, that was weird
        [ForbidFromScene(Scene.OceanSpiderHouse)] // shared object with goldskulltula, cannot change without modification
        //[EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.Snowhead, // in the air, bit weird
        //    Scene.MountainVillageSpring, Scene.TwinIslandsSpring)] // not a problem, just weird seeing them fly like that
        [SwitchFlagsPlacement(mask: 0x3F, shift: 0)]
        Skulltula = 0x24, // En_St

        // In order to split the skullwalltula and skulltula in the spider gossip stone grotto into two objects,
        // I have to make a new actor that uses a fake object
        // also splitting ocean spiderhouse skulltulla and gold skulltula
        [EnemizerEnabled]
        [ObjectListIndex(0x22D)] // empty object
        [ActorInitVarOffset(0x2540)]
        [FileID(67)] // actual file of skulltula in case it wasnts to know things like how big it is
        [FlyingVariants(1)] // going to mark it flying for now
        [CeilingVariants(0)]
        [VariantsWithRoomMax(max: 0, variant: 0, 1)] // don't actually place dummy actor
        SkulltulaDummy = 0x25, // fake
        //Empty25 = 0x25, // originally empty

        [ActorizerEnabled]
        // FILE MISSING (always loaded)
        //[ObjectListIndex(0xFC)] // the spreadsheet thinks this is free but I dont think so, think its a multi-object like tsubo
        [ObjectListIndex(0x1)]
        [ActorInstanceSize(0x1A8)]
        // 0xFF00 is text ID space
        [GroundVariants(0x400A,  0x2C09, 0x2D0A, 0x2409, 0x2909, // great bay
            0x350A, 0x370A, 0x390A,
            0x400A, 0x420A,// milkroad
            0x3B0A, 0x080A, 0x0D0A, 0x360A)]
        [UnkillableAllVariants]
        PointedSign = 0x26, // En_A_Obj

        [FileID(68)]
        [ObjectListIndex(0x1)]
        Obj_Wturn = 0x27, // Obj_Wturn

        [FileID(69)]
        [ObjectListIndex(0x1)]
        RiverSound = 0x28, // En_River_Sound

        Empty29 = 0x29,

        [ActorizerEnabled]
        [FileID(70)]
        // dual object actor
        [ObjectListIndex(0x1AB)] // regular OSN, the other one is ANI
        [CheckRestricted(Item.ShopItemTradingPostArrow30, Item.ShopItemTradingPostArrow50,
            Item.ShopItemTradingPostFairy, Item.ShopItemTradingPostGreenPotion,
            Item.ShopItemTradingPostNut10, Item.ShopItemTradingPostRedPotion,
            Item.ShopItemTradingPostShield, Item.ShopItemTradingPostStick)]
        [GroundVariants(0, 1)]
        [VariantsWithRoomMax(max: 0, variant: 0, 1)]
        [UnkillableAllVariants]
        TradingPostShop = 0x2A, // En_Ossan

        Empty2B = 0x2B,
        Empty2C = 0x2C,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1CC0)]
        [FileID(71)]
        [ObjectListIndex(0x1D)]
        // the first byte is path, if its FF it should sit in one spot instead of pathing, which means no crash
        //   would be better if it patroled or something but better that crash at least
        //[FlyingVariants(0xFF)]
        //[GroundVariants(0xFF)]
        [PathingVariants(1, 2, 3, 4, 7)]
        [RespawningAllVariants] // they do NOT respawn, this is temporary: light arrow req makes them difficult to kill early in the game
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 10, variant: 0xFF)]
        [FlyingToGroundHeightAdjustment(100)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.SouthernSwampClear)] // unknown crash reason
        DeathArmos = 0x2D, // En_Famos

        Empty2E = 0x2E,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x1240)]
        [FileID(72)]
        [ObjectListIndex(0x2A)]
        // 0 is active bombflower bomb
        [GroundVariants(0xFFFF)]
        [VariantsWithRoomMax(max: 8, variant: 0xFFFF)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.OdolwasLair)]//Scene.GoronRacetrack)] // some poor unfortunate souls asked for a more chaotic race
        BombFlower = 0x2F, // En_Bombf

        Empty30 = 0x30,
        Empty31 = 0x31,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1380)] // is this wrong? I see < 1100 for size
        [ActorInstanceSize(0x380)]
        [FileID(73)] // 0x49
        [ObjectListIndex(0x30)]
        // armos has no params, dont know why these are vanilla
        [GroundVariants(0xFFFF, 0x7F)]
        [WaterBottomVariants(0x777)]
        //[VariantsWithRoomMax(max: 7, variant: 0xFFFF, 0x7F)] // weirdly high cpu usage, not a low as other still enemies
        //[VariantsWithRoomMax(max: 0, variant: 0xFFFF, 0x7F, 0x777)] // disabled until I can figure out what caused the corruption
        Armos = 0x32, // En_Am

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3A10)]
        [FileID(74)]
        [ObjectListIndex(0x31)]
        [GroundVariants(1, 0)] // 0 regular, 1 is big one from OOT forest temple
        [DifficultVariants(1)]
        [VariantsWithRoomMax(max: 1, variant: 1)]
        [VariantsWithRoomMax(max: 8, variant: 0)]
        DekuBaba = 0x33, // En_Dekubaba

        [FileID(75)]
        [ObjectListIndex(0x1)]
        En_M_Fire1 = 0x34, // En_M_Fire1

        [FileID(76)]
        [ObjectListIndex(0x1)]
        En_M_Thunder = 0x35, // En_M_Thunder

        // so this is aparently many weird actors???
        // uh oh, this one might be the biggest reason yet to have multiple object actors
        //[ActorizerEnabled]
        [FileID(77)]
        // I wanted the mailman bag on the wall for wall variants, but their position is deep into the shower/closet, bad for placment
        // good candidate for new actor
        //[ObjectListIndex(0x208)]
        //[WallVariants(0x204)] // 0x204 is the hanging mailman backpack
        //[ObjectListIndex(0x217)] // works but a big lame, no sound, no effects, just a wall of animating texture 
        //[GroundVariants(0x6)] // storm around GBT
        [ObjectListIndex(0x195)] // monkey??
        [GroundVariants(0x8)] // pot of boiling water for monkey
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.PostOffice)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        Bg_Breakwall = 0x36, // Bg_Breakwall

        Empty37 = 0x37,

        // Boss blue warp
        [ActorizerEnabled] // kinda boring, needs to be modified to it can appear after you clear the room and take you places that are interesting
        [ObjectListIndex(0x3E)]
        [FileID(78)]
        [DynaAttributes(35, 30)]
        // params: type is 0xFF, address offset for type 0 is 0xFF00
        [GroundVariants(0x0)] // the 101 and above are for warp TO bosses
        [VariantsWithRoomMax(max: 1, variant: 0x0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [ForbidFromScene(Scene.DekuTrial, Scene.GoronTrial, Scene.LinkTrial, Scene.ZoraTrial)]
        [PlacementWeight(15)]
        WarpDoor = 0x38, // Door_Warp1

        [ActorizerEnabled]
        [ObjectListIndex(0x80)]
        [FileID(79)]
        [CheckRestricted(Scene.SouthClockTown, variant: ActorConst.ANY_VARIANT, // 0x287F,
            check: Item.CollectableSouthClockTownHitTag1, Item.CollectableSouthClockTownHitTag2, Item.CollectableSouthClockTownHitTag3)]
        [CheckRestricted(Scene.GoronShrine, variant: ActorConst.ANY_VARIANT,
            check: Item.MaskDonGero)]
        //[CheckRestricted(Scene.DekuShrine, variant: ActorConst.ANY_VARIANT,
        //    check: )]
        [GroundVariants(
            0x287F, // east/south clocktown
            0x159E, 0x0794,  // goron shrine
            0x0289, // gold pirate torches
            0x287F, // ghost hut
            0x1180, // below graveyard
            0x207F
        )]
        [CompanionActor(MothSwarm, ourVariant: -1, variant: 1, 2, 3, 4, 7)] // todo select specific variants that are lit
        [CompanionActor(Keese, ourVariant: -1, variant: 0x0, 0x2, 0x8002, 0x8004)] // todo select specific variants that are lit
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [AlignedCompanionActor(MothSwarm, CompanionAlignment.Above, ourVariant: -1,
           variant: 1, 2, 3, 4, 7)] // they're free, and they are moths, makes sense
        [AlignedCompanionActor(Keese, CompanionAlignment.Above, ourVariant: -1,
            variant: 0x0, 0x2, 0x8002, 0x8004)] // todo select specific variants that are lit
        //Scene.SouthClockTown
        [ForbidFromScene(Scene.WoodfallTemple,
            Scene.SouthernSwamp, Scene.SouthernSwampClear, // kinda important for entering the swamp spiderhouse
            Scene.DekuShrine, // TODO re-enable if we get all of the check list fleshed out, but people use this for weirdshot
            Scene.WestClockTown, //Scene.SouthernSwampClear,
            Scene.WoodfallTemple, // assumed the giant flower in the middle needs the object even if all of hte other uses could be elimnated
            Scene.SnowheadTemple, Scene.BeneathGraveyard, Scene.GreatBayCoast,
            Scene.GreatBayTemple, Scene.OceanSpiderHouse,
            Scene.BeneathTheWell,
            Scene.PiratesFortressRooms, Scene.PoeHut)]
        [UnkillableAllVariants]
        Torch = 0x39, // Obj_Syokudai

        [FileID(80)]
        [ObjectListIndex(0x96)]
        Item_B_Heart = 0x3A, // Item_B_Heart

        [EnemizerEnabled]
        [ActorInstanceSize(0x2C8)]
        [ActorInitVarOffset(0x1D30)]
        [FileID(81)]
        [ObjectListIndex(0x40)]
        // 0 runs away
        // 1 is passive
        // 2 is hit once and die
        [GroundVariants(0xFF02, 0xFF00, 0xFF01)]
        [RespawningVariants(0xFF01)] // doesnt seem to stop enemies, so it counts as enemy
        [CompanionActor(DekuFlower, ourVariant: -1, variant: 0x7F, 0x17F)] // do you think they make them or trade like hermitcrabs?
        [ForbidFromScene(Scene.Woodfall)]//, Scene.DekuPalace)]
        MadShrub = 0x3B, // En_Dekunuts

        [EnemizerEnabled]
        [ActorInstanceSize(0x464)]
        [ActorInitVarOffset(0x1AF0)]
        [FileID(82)]
        [ObjectListIndex(0x51)]
        [GroundVariants(0)]
        [CompanionActor(Flame, ourVariant: -1, variant: 0x7F4)]
        [AlignedCompanionActor(Flame, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x7F4)] // I'll just put this over with the rest of the fire
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // visible waiting below the bridges
        RedBubble = 0x3C, // En_Bbfall

        [FileID(83)]
        [ObjectListIndex(0x1)]
        Arms_Hook = 0x3D, // Arms_Hook

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1A40)]
        [FileID(84)]
        [ObjectListIndex(0x51)]
        [FlyingVariants(0xFFFF)] // vanilla has no params, these are what they used on themap
        [GroundVariants(1)] // no reason it cannot be put on land, right?
        [VariantsWithRoomMax(max: 8, variant: 0xFFFF)]
        [FlyingToGroundHeightAdjustment(50)]
        [CompanionActor(Flame, ourVariant: -1, 0x7FE)] // blue flames
        [RespawningAllVariants]
        BlueBubble = 0x3E, // En_Bb

        [FileID(85)]
        [ObjectListIndex(0x17E)]
        Bg_Keikoku_Spr = 0x3F, // Bg_Keikoku_Spr

        Empty40 = 0x40,

        [ActorizerEnabled]
        [ActorInstanceSize(0x1A0)]
        [ActorInitVarOffset(0x1A0)]
        [FileID(86)]
        [ObjectListIndex(0x61)]
        // weirdly this is NOT dynapoly actor
        [CheckRestricted(Scene.TerminaField, variant: GameObjects.ActorConst.ANY_VARIANT, Item.CollectableTerminaFieldTreeItem1)]
        // bush: 0xFF0B, small tree: 0xFF02
        // big tree: 0xFF00, big tree with shop man in it: 0x0A1A
        // 17,18 are leaf particles
        [GroundVariants(0xFF00, 0xFF01, 0xFF02,  // normal vanilla varieties
            0xFF0B, 0xFF0D, // Bushes
            0x0A1A, 0xFF1A, // spawns with EnAni in the tree (if the object exists)
            0xA, // really big ugly tree from OOT adult kakariko?
            0xF, // even bigger bush, was this EVER used? even in OOT?
            0x11, 0x12, // "Black" bushes, both big and small
            0x7)] // yellow tree, fall colors? dying?
        [VariantsWithRoomMax(max: 0, variant: 0xFF0D)] // 0xFF0D crashes TF do not use (is from the cucco shack)
        [VariantsWithRoomMax(max: 1, variant: 0xA1A, 0xFF1A)] // has EnAni, more than one is odd
        [VariantsWithRoomMax(max: 1, variant: 0xA)] // UGLY is also BG
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.InFront, ourVariant: -1, variant: 0x8200, 0xA200)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        Treee = 0x41, // En_Wood2

        Empty42 = 0x42,

        //[EnemizerEnabled] //hardcoded values for his entrance spawn make the camera wonky, and his color darkening is wack, also hardware crash
        [FileID(87)]
        [ActorInstanceSize(0x938 + (0x2A0 * 25))] // 0x938 // oversized because he spawns little bats and can easily overblow ram
        [ObjectListIndex(0x52)]
        [GroundVariants(0)] // can fly, but weirdly is very bad at changing height if you fight in a multi-level area
        [OnlyOneActorPerRoom] // only fight her if you fight only one
        [RespawningAllVariants] // is NOT unkillable, but assume never have light arrows until the last second of a run, do not place where can block an item
        [ForbidFromScene(Scene.InvertedStoneTowerTemple)] // lets not randomize his normal spawn
        // good candidate for night and dungeon spawn only
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GreatBayCoast, Scene.ZoraCape, Scene.RoadToIkana,
            Scene.SouthernSwamp, Scene.WoodsOfMystery, Scene.Woodfall, Scene.TwinIslandsSpring, Scene.PathToSnowhead,
            Scene.Snowhead, Scene.GoronVillage, Scene.DekuShrine, Scene.StoneTower)]
        Gomess = 0x43, // En_Death 🤘

        [FileID(88)]
        [ObjectListIndex(0x52)]
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
        [ObjectListIndex(0x1)]
        Demo_Effect = 0x48, // Demo_Effect

        // MULTIPLE OBJECT ACTOR
        //[ActorizerEnabled]
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
        /* [EnemizerScenesPlacementBlock(Scene.Snowhead, Scene.TwinIslands, Scene.MountainVillage, Scene.GoronVillage, Scene.PathToMountainVillage, Scene.PathToSnowhead,
            Scene.GoronShrine, Scene.MountainSmithy, Scene.GoronRacetrack,  // no snow, but entering from snowy area is also crash
            Scene.SnowheadTemple, Scene.WoodfallTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)] // with randomized dungeons, entering from snowy area
        */
        [PlacementWeight(60)]
        Demo_Kankyo = 0x49, // lost woods living fairy dust

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3200)]
        [FileID(92)]
        [ObjectListIndex(0x9)]
        // 0x0 is regular, 0x8000 is invisible
        // 0x10 wont spawn, 0x20 seems normal, 0x40?
        [GroundVariants(0, 0x8000)]
        //[VariantsWithRoomMax(max:1, variant:0x8000)]
        [VariantsWithRoomMax(max: 3, variant: 0)]
        [DifficultAllVariants]
        FloorMaster = 0x4A, // En_Floormas

        Empty4B = 0x4B,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x32A0)]
        [FileID(93)]
        [ObjectListIndex(0x75)]
        // 0x8000 removes the freeze timer, like market town in OOT
        // 3 is invisible, 2 is squatting
        // 4 spawns inside of an ice block
        // 0x5/6/7 dance if the player wears a mask
        // 0xFFFE is -2 = gibdo, does not stun
        // 0xFFFE is -3 = gibdo that rises out of the coffin, from OOT, does not stun and looks weird so we wont use until I find a way to combine with a fake chest
        [GroundVariants(0x7F07, 0x7F05, 0x7F06, 0x7F03, 0x7F04, 0x8005, 0x8006, 0x8007, 0x8003, 0xFFFE)]
        //[WaterBottomVariants(0x7F07, 0x7F05, 0x7F06, 0x7F03, 0x7F04, 0x8005, 0x8006, 0x8007, 0x8003, 0xFFFE)] // you idiot this means you can put water enemies there
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 3, variant: 0x7F07, 0x7F05, 0x7F06, 0x7F02, 0x8005, 0x8006, 0x8007, 0x7F04)]
        [VariantsWithRoomMax(max: 1, variant: 0x7F03, 0x8003, 0xFFFE)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)] // 0xFF00 is read, but only 0x7F of that gets set on death, never checked
        [ForbidFromScene(Scene.IkanaCanyon, Scene.BeneathTheWell)] // gibdo locations, but share the same object so gets detected
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slows us down too much
        ReDead = 0x4C, // En_Rd

        [ActorizerEnabled]
        [FileID(94)]
        [ObjectListIndex(0x5C)]
        [DynaAttributes(12, 8)]
        [SwitchFlagsPlacement(mask: 0x7E, shift: 9)]
        //[GroundVariants(0)] // params are ignored, uses params as a variable for setting
        [FlyingVariants(0)]
        [VariantsWithRoomMax(max: 3, variant: 0)] // too much Bg is crash
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [FlyingToGroundHeightAdjustment(275)]
        [EnemizerScenesPlacementBlock(
            Scene.Grottos, Scene.AstralObservatory, Scene.ZoraHallRooms, Scene.PiratesFortressRooms, Scene.DekuPalace,
            Scene.DekuShrine, Scene.GoronRacetrack, Scene.WaterfallRapids, //Scene.GormanRaceTrack,
            /* Scene.RoadToIkana,*/ Scene.IkanaCastle, Scene.BeneathGraveyard, Scene.DampesHouse,
            Scene.SwampSpiderHouse, Scene.OceanSpiderHouse,
            Scene.StockPotInn, Scene.GoronShrine, Scene.DekuShrine, /*Scene.ZoraHall,*/ Scene.TradingPost, Scene.MayorsResidence,
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple,
            //Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple, Scene.SouthernSwamp, // dyna crash, remove if we get dyna overload detection working
            Scene.BeneathTheWell//,
            /*Scene.IkanaGraveyard, Scene.StoneTower */)] // dyna crash
        //*/
        //[EnemizerScenesPlacementBlock(Scene.DekuPalace, Scene.BeneathTheWell, Scene.BeneathGraveyard, Scene.RoadToIkana, Scene.StoneTower)]
        UnusedStoneTowerStoneElevator = 0x4D, // Bg_F40_Flift

        //[ActorizerEnabled] // TODO
        // Has no File, burried in [code] file
        [ActorInstanceSize(0)] // unknown, never seen though
        [ObjectListIndex(0x1)]
        //[GroundVariants]
        Bg_Heavy_Block = 0x4E, // Bg_Heavy_Block

        [EnemizerEnabled]
        [FileID(95)]
        [ObjectListIndex(0x1)] // gameplay_keep obj 1
        [CheckRestricted(Scene.TerminaField, ActorConst.ANY_VARIANT, Item.CollectableTerminaFieldButterflyFairy1)] // TODO which is it?
        // TODO finish separating them
        [CheckRestricted(Scene.Grottos, 0x5323, Item.BottleCatchBug)] // north gossip grotto
        [CheckRestricted(Scene.Grottos, 0x2323, Item.BottleCatchBug)] // west gossip grotto
        [CheckRestricted(Scene.Grottos, 0x6322, Item.BottleCatchFish)] // regular grotto, TODO do we want to force a fish in a unique place instead?
        [CheckRestricted(Scene.Grottos, 0x2324, Item.CollectableGrottosOceanGossipStonesButterflyFairy1)]
        [CheckRestricted(Scene.Grottos, 0x4324, Item.CollectableGrottosMagicBeanSellerSGrottoButterflyFairy1)]
        [CheckRestricted(Scene.Grottos, variant: 0x3324,
            Item.CollectableGrottosCowGrottoButterflyFairy1, Item.CollectableGrottosCowGrottoButterflyFairy2
            )]
        [CheckRestricted(Scene.MountainVillageSpring, 0x4324,
            Item.CollectableMountainVillageWinterMountainVillageSpringButterflyFairy1)]
        [CheckRestricted(Scene.MountainVillageSpring, 0x5324,
            Item.CollectableMountainVillageWinterMountainVillageSpringButterflyFairy2)]
        [CheckRestricted(Scene.GreatBayCoast, variant: 0x2324, Item.CollectableGreatBayCoastButterflyFairy1)]
        [GroundVariants(0x3323, 0x4324, 0x5323)] // beatles on the floor
        // they dont stick to the wall, they climb in the air
        //[WallVariants(0x3323, 0x2324, 0x4324, 0x5323)] // beatles on the... wall?
        [FlyingVariants(0x2323, 0x2324, 0x4324)] // butterlies in the air
        [WaterVariants(0x6322)] // fish swimming in the water
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 2, 0x3323, 0x2324, 0x4324)]
        BugsFishButterfly = 0x4F, // Obj_Mure // includes bugs and fish and butterflies

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3080)]
        [FileID(96)]
        [ObjectListIndex(0x20)]
        // swamphouse: 0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F
        // ocean spiderhouse: 0xFF3F, 0x1B, 0x317, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0x777, 0x57B, 0xFF0B, 0xFF0F, 0x223, 0x11F,
        // some spiders crash, probably because it follows a path, 223, 113, 1B, 909
        // regular wall skulltula that attacks you: FFFC
        // issue: cannot know what the vars for the ones in the pots/hives/trees/bonk are, dirt is weirdly visible in scenetatl
        // known fine: FF53, FF3B, FF76
        [WallVariants(0xFF53, 0xFF07, 0xFF56, 0xFF62, 0xFF76, 0xFF03,
            0xFF3F, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B, 0xFF0F, 0xFFFC,
            0xFF2B // ocean spiderhouse
            )]
        [PathingVariants(0xEF, 0x7F, 4, 0x55B, 0x637, 0x113, 0x91F, 0x909, 0xB0C, 0xC0F)]
        [GroundVariants(0xFF53, 0xFF53, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B)] // pathing type with path disabled, why is this so rare?
        [PathingTypeVarsPlacement(mask: 0xFF00, shift: 8)]
        [VariantsWithRoomMax(max: 1,
            0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F,
            0xFF3F, 0x317, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0x777, 0x57B, 0xFF0B, 0xFF0F, 0x11F)]
        // we want to put them on the ceiling, possibly
        [CeilingVariants(0xFF53, 0xFF07, 0xFF56, 0xFF62, 0xFF76, 0xFF03,
            0xFF3F, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B, 0xFF0F, 0xFFFC)] // WARNING: right now we do not remove them from their old place, but if we did this would put ceiling actors on the wall
        [RespawningVariants(0xFF53, 0xFF07, 0xFF56, 0xFF62, 0xFF76, 0xFF03,
            0xFF3F, 0xFF3B, 0xFF5D, 0xFF61, 0xFF6D, 0xFF0B, 0xFF0F, 0xFFFC)] // mark all ceiling types as respawning to avoid being placed on kill skulltula to get skull token enemies
        [ForbidFromScene(Scene.SwampSpiderHouse, Scene.OceanSpiderHouse)] // dont remove old spiders, the new ones might not be gettable
        [TreasureFlagsPlacement(mask: 0x3F, shift: 2)] // for some reason it collides with path sometimes, TODO figure this out
        GoldSkulltula = 0x50, // En_Sw "Skullwalltulla"

        //[ActorizerEnabled] // wont snow with obj 1 or 0x1D8 might need weathertag
        [ObjectListIndex(1)] // gameplay_keep 1
        [FileID(97)]
        // 0 is rain, but something else controls the rain counter not this actor
        // 2 is the murk from the ocean, but full time
        // 1 and 3 are snow, but you need to set E2 to non-zero
        // 4 kill itself it not in the giants chamber
        [FlyingVariants(3)]
        //[GroundVariants(3)] // 3 is snow
        [UnkillableAllVariants]
        ObjectKankyo = 0x51, // Object_Kankyo

        Empty52 = 0x52,
        Empty53 = 0x53,

        // child epona from OOT, ununsed in MM at all, this is NOT regular epona,
        // does nothing and has a semi-broken AI and collider, bit weird to find her
        //  I modified her so she at least starts with a better AI than stand perfectly still, new actor file
        //[ActorizerEnabled]
        [FileID(98)]
        [ObjectListIndex(0x7D)]
        // issue: the game tries to malloc space for a skin skeleton, the default size is WRONG
        // not sure about the actual size tho, so going with overkill size (I hope)
        [ActorInstanceSize(0x1500)]
        [GroundVariants(0)] // no vanilla params at all
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.RomaniRanch, // skin crash, not sure why I thought I fixed that
            Scene.GormanRaceTrack)] // skeleton update crash... why
        En_Horse_Link_Child = 0x54, // En_Horse_Link_Child

        [ActorizerEnabled]
        [ActorInstanceSize(0x194)]
        [ObjectListIndex(2)] // field_keep, obj 2
        [FileID(99)]
        //[CheckRestricted(Scene.NorthClockTown, ActorConst.ANY_VARIANT,
        //    item:Item.)] //TODO finish this
        [ForbidFromScene(Scene.TerminaField, // too many here, and we want the hints
            Scene.NorthClockTown, // so many checks
            Scene.LaundryPool, Scene.MountainVillage)] // old joke
        //[CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT,
        //    check: Item.)]
        [CheckRestricted(Scene.RoadToSouthernSwamp, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestToSwampGrotto)]
        [CheckRestricted(Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestSwampGrotto)]
        [CheckRestricted(Scene.WoodsOfMystery, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestWoodsGrotto)]
        [CheckRestricted(Scene.DekuPalace, variant: ActorConst.ANY_VARIANT,
            check: Item.ItemMagicBean, Item.CollectableGrottosMagicBeanSellerSGrottoButterflyFairy1,Item.CollectableBeanGrottoSoftSoil1, Item.ChestBeanGrottoRedRupee)]
        [CheckRestricted(Scene.PathToSnowhead, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestToSnowheadGrotto)]
        [CheckRestricted(Scene.TwinIslands, variant: ActorConst.ANY_VARIANT,
            check: Item.HeartPieceTwinIslandsChest, Item.ChestHotSpringGrottoRedRupee, Item.BottleCatchHotSpringWater)]
        [CheckRestricted(Scene.TwinIslandsSpring, variant: ActorConst.ANY_VARIANT,
            check: Item.HeartPieceTwinIslandsChest)]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestMountainVillageGrottoRedRupee)]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestGreatBayCoastGrotto,
                   Item.ItemCoastGrottoCowMilk1, Item.ItemCoastGrottoCowMilk2, Item.CollectableGrottosCowGrottoButterflyFairy2)]
        [CheckRestricted(Scene.ZoraCape, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestGreatBayCapeGrotto)]
        [CheckRestricted(Scene.RoadToIkana, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestToIkanaGrotto)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestGraveyardGrotto)]
        [CheckRestricted(Scene.IkanaCanyon, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestIkanaSecretShrineGrotto)]
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
        [AlignedCompanionActor(Obj_Dowsing, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x110)] // rumble
        [AlignedCompanionActor(Butterfly, CompanionAlignment.Above, ourVariant: -1,
            variant: 0, 1, 2)]
        [AlignedCompanionActor(IshiRock, CompanionAlignment.Above, ourVariant: -1,
            variant: 0xA1, 0xFE01)] // everyone loves a good hidden grotto under a rock
        [BlockingVariantsAll] // might turn this off again, but at can cause issues, esp in deku palace and races
        //[ForbidFromScene(Scene.RoadToIkana, Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.PathToSnowhead,
        //    Scene.TerminaField)]
        GrottoHole = 0x55, // Door_Ana

        Empty56 = 0x56,
        Empty57 = 0x57,
        Empty58 = 0x58,
        Empty59 = 0x59,
        Empty5A = 0x5A,

        // spawner for some enemies
        [FileID(100)]
        [ObjectListIndex(0x1)]
        En_Encount1 = 0x5B, // En_Encount1

        [FileID(101)]
        [ObjectListIndex(0xC)]
        TreasureChestLight = 0x5C, // Demo_Tre_Lgt

        Empty5D = 0x5D,
        Empty5E = 0x5E,

        //[ActorizerEnabled] // even one of them can overrun dyna in woodfall when you spawn the temple because of dyna
        // we really dont need dyna here, it exists to stop the player from climbing the ladder in sewer only
        // going to use new bombal without cutscene instead since that one is NOT dyna
        [FileID(102)]
        [ObjectListIndex(0x280)]
        [DynaAttributes(186, 101)] // holy shit, no wonder I could never put it anywhere
        [FlyingVariants(1)]
        //[GroundVariants(1)] // testing
        // needs limits because it can overload the dyna
        [VariantsWithRoomMax(max: 5, variant: 0, 1)]
        [FlyingToGroundHeightAdjustment(150)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [UnkillableAllVariants]
        MajoraBalloonSewer = 0x5F, // En_Encount2

        // empty actor, does nothing
        [FileID(103)]
        [ObjectListIndex(0x70)]
        En_Fire_Rock = 0x60, // En_Fire_Rock

        [FileID(104)]
        [ObjectListIndex(0x88)]
        //[DynaAttributes] // multiple: the rotating room, the stone door has TWO
        TwistyTunnelClockTower = 0x61, // Bg_Ctower_Rot

        // unused ?
        [FileID(105)]
        [ObjectListIndex(0x87)]
        ReflectableLightRay = 0x62, // Mir_Ray

        Empty63 = 0x63,

        // tag: clam
        [EnemizerEnabled]
        [ActorInitVarOffset(0xF00)]
        [FileID(106)]
        [ObjectListIndex(0x8E)]
        // works on ground too but cannot add ground without ground enemies showing up in fish tank
        // there is a bug sometimes on ground it loses its collider (in a small scene)
        //[GroundVariants]
        [WaterBottomVariants(0)]
        Shellblade = 0x64, // En_Sb

        // miniboss
        [FileID(107)]
        [ObjectListIndex(0x128)]
        MadJelly = 0x65, // En_Bigslime

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1B80)]
        [FileID(108)]
        [ObjectListIndex(0x31)]
        // 0 is weird, unattackable just spins forever
        // 1 is stiff stick, 2 is mini
        [GroundVariants(0, 1, 2)] // both stiff and mini
        [VariantsWithRoomMax(max: 0, variant: 0x0)]
        [RespawningAllVariants] // they grow back, dont count as killable
        DekuBabaWithered = 0x66, // En_Karebaba

        // TODO fix so we can spawn them in the world
        // tag: the brothers
        [ActorizerEnabled]
        [FileID(109)]
        [ObjectListIndex(0x99)]
        [CheckRestricted(Item.ShopItemGormanBrosMilk, Item.MaskGaro,
            Item.NotebookMeetGormanBrothers, Item.NotebookDefeatGormanBrothers)]
        [GroundVariants(0xFE03, 0xFE04)]
        [VariantsWithRoomMax(max: 0, 0xFE03, 0xFE04)] // inf loop if only one and not two
        //[ForbidFromScene(Scene.GormanTrack)] // if they are missing it crashes cremia game
        [UnkillableAllVariants]
        GormanBros = 0x67, // En_In

        Empty68 = 0x68,

        [ActorizerEnabled] // works but her object is huge, and you cant talk or interact with her, which kinda sucks
        [ActorInstanceSize(0x314)]
        [FileID(304)]
        [ObjectListIndex(0xA2)]
        // her code takes her params and passes it to another func as (params * 0x7E00 >> 9)
        // 200 does not spawn? 400 is slightly tilted to one side (might be the leever actually)
        // 800 also does not spawn
        //[GroundVariants(0x0A00)]//(0x7E00)]
        [WaterBottomVariants(0)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        Ruto = 0x69, // En_Ru

        [FileID(110)]
        [ObjectListIndex(1)]
        RunningBombChu = 0x6A, // En_Bom_Chu

        [FileID(111)]
        [ObjectListIndex(0x191)]
        // multiple think this is all horse race obstacles
        //[DynaAttributes(2,4)] // 03918
        //[DynaAttributes(26,20)] // 2Fb8
        //[DynaAttributes()] // third one isn't even in the object its in the overlay 10c8
        En_Horse_Game_Check = 0x6B, // En_Horse_Game_Check

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2330)]
        [FileID(112)]
        [ObjectListIndex(0xAB)]
        [CheckRestricted(Scene.ZoraCape, variant:ActorConst.ANY_VARIANT, Item.HeartPieceGreatBayCapeLikeLike)]
        // 2 is ocean bottom, 0 is one in shallow shore water, 3 is land and one in shallow water
        [WaterBottomVariants(0, 2)]
        [GroundVariants(3)]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 3, variant: 0, 2)]
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

        // ?? unused?
        // has no draw function, checks for a switch flag and if the player is near, but doesnt seem to do anything
        [FileID(113)]
        [ObjectListIndex(0x1)]
        En_Fr = 0x73, // En_Fr

        Empty74 = 0x74,
        Empty75 = 0x75,
        Empty76 = 0x76,
        Empty77 = 0x77,
        Empty78 = 0x78,

        // whoever said this doesnt exist in english was a fucking liar
        // this actor is too hardcoded to do anything with, if we had a small water scene to fish in...
        //[ActorizerEnabled]
        [ObjectListIndex(0x124)]
        [FileID(114)]
        // params: FFFF is the main all-in-one
        // < 100 is owner
        // 100->115 is per-single fish
        // 200 is something, it causes splashes you can hear but if you get close to it it crashes
        [GroundVariants(0xFFFF)] // assumption, the main vars will be the man in the hat
        [WaterVariants(0xFFFF)] // assumption, the main vars will be the man in the hat
        [UnkillableAllVariants]
        OOTFishing = 0x79, // En_Fishing

        // TODO
        [FileID(115)]
        [ObjectListIndex(0003)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        [UnkillableAllVariants]
        [PlacementWeight(30)]
        PushableBlock = 0x7A, // Obj_Oshihiki

        [FileID(116)]
        [ObjectListIndex(0001)]
        Eff_Dust = 0x7B, // Eff_Dust

        [FileID(117)]
        [ActorInstanceSize(0x16C)]
        [ObjectListIndex(0001)]
        HorseJumpingFence = 0x7C, // Bg_Umajump

        [FileID(122)]
        [ObjectListIndex(0001)]
        Arrow_Fire = 0x7D, // Arrow_Fire

        [FileID(123)]
        [ObjectListIndex(0001)]
        Arrow_Ice = 0x7E, // Arrow_Ice

        [FileID(124)]
        [ObjectListIndex(0001)]
        Arrow_Light = 0x7F, // Arrow_Light

        // this actor is fire arrows, bottles, and rupees you can find in the world, ununsed from OoT chest (lens) game
        // I'm not using vanill anymore, use the mmra
        // free bottle? if I can make it rare...
        //    all of these require specific objects, annoying..
        // I can pick up bottles but they are shuffled getitems, so they are random..
        // the free standing rupees are untouchable, and require lens, but if you find them its still interesting
        //  need to fix so you can actually touch them though, as they have no pickup
        //[ActorizerEnabledFreeOnly]
        //[ActorizerEnabled]
        [FileID(121)]
        //[DynaAttributes] // this object has a dyna in the code file... but for what? nothing we spawn anyway
        //[ObjectListIndex(1)] // multi-object actor uses 1 as its defined object, ignore
        //[ObjectListIndex(0x9E)] // GI_BOTTLE
        //[ObjectListIndex(0x13F)] // GI_RUPY
        //[GroundVariants(0)] // should be bottle, doesnt work without object
        //[OnlyOneActorPerRoom]
        //[EnemizerScenesPlacementBlock(Scene.TerminaField)] // it would always get placed in termina field, bad
        Item_Etcetera = 0x80, // Item_Etcetera

        [ActorizerEnabled] // not sure this works with object 1, will test later
        [FileID(125)]
        [ObjectListIndex(0x16F)] // multi-object, could be dangeon keep instead
        // object_kibako is not dyna at least
        [CheckRestricted(Scene.RomaniRanch, variant: ActorConst.ANY_VARIANT, Item.MaskRomani)] // this might be required for objUm weirdly...
        [CheckRestricted(Scene.EastClockTown, variant: ActorConst.ANY_VARIANT, Item.CollectableEastClockTownWoodenCrateSmall1)] //wasnt there a second one
        [CheckRestricted(Scene.LaundryPool, variant: ActorConst.ANY_VARIANT, Item.CollectableLaundryPoolWoodenCrateSmall1)]
        [CheckRestricted(Scene.GreatBayTemple, variant: ActorConst.ANY_VARIANT, Item.ItemIceArrow)] // in case we really want these for the fight
        [GroundVariants(0x000B, 0x001E, 0x001F, 0x000F, 0x0015,  // stone tower
            0x060B, 0x200B, // inverted stone tower
            0xFF1F, // romani ranch
            0xA002, // laundry pool
            0x8181, 0x828A)] // east clock town
        //[VariantsWithRoomMax(max: 5, variant: -1)] 
        //[VariantsWithRoomMax(max: 0, variant: 0xA002, 0x8181, 0x828A)] // items
        [UnkillableAllVariants]
        SmallWoodenBox = 0x81, // Obj_Kibako

        // MULTIPLE OBJECT ACTOR, can use object_tsubo object, dungeon keep, and green pot object
        [ActorizerEnabled]
        [FileID(126)]
        //[ObjectListIndex(0x1)] // this is a lie, the pot DETECTS multiple objects but does NOT exist in gameplay keep
        [ObjectListIndex(0xF9)]
        // TODO randomize some more of these
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT, Item.CollectableTerminaFieldPot1)] // only one after all
        [CheckRestricted(Scene.SwordsmansSchool, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSwordsmanSSchoolPot1, Item.CollectableSwordsmanSSchoolPot2, Item.CollectableSwordsmanSSchoolPot3, Item.CollectableSwordsmanSSchoolPot4, Item.CollectableSwordsmanSSchoolPot5)]
        [CheckRestricted(Scene.DoggyRacetrack, variant: ActorConst.ANY_VARIANT,
            Item.CollectableDoggyRacetrackPot1, Item.CollectableDoggyRacetrackPot2, Item.CollectableDoggyRacetrackPot3, Item.CollectableDoggyRacetrackPot4)]
        [CheckRestricted(Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot1, Item.CollectableSouthernSwampPoisonedMagicHagsPotionShopExteriorPot2)]
        [CheckRestricted(Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot1, Item.CollectableSouthernSwampClearMagicHagsPotionShopExteriorPot2)]
        [CheckRestricted(Scene.SwampSpiderHouse, variant: 0x0005, // swamp spiderhouse
            Item.CollectibleSwampSpiderToken5)]
        [CheckRestricted(Scene.SwampSpiderHouse, variant: 0x001E,
            Item.CollectibleSwampSpiderToken30)]
        [CheckRestricted(Scene.DekuPalace, variant: ActorConst.ANY_VARIANT,
            Item.CollectableDekuPalaceEastInnerGardenPot1, Item.CollectableDekuPalaceEastInnerGardenPot2)]
        [CheckRestricted(Scene.DekuShrine, variant: ActorConst.ANY_VARIANT, Item.CollectableDekuShrineGreyBoulderRoomPot1)]
        [CheckRestricted(Scene.Woodfall, variant: ActorConst.ANY_VARIANT,
            Item.CollectableWoodfallPot1, Item.CollectableWoodfallPot2, Item.CollectableWoodfallPot3)]
        //[CheckRestricted(Scene.WoodfallTemple, variant: ActorConst.ANY_VARIANT,
        //    Item.CollectableWoodfallTempleGekkoRoomPot1, Item.CollectableWoodfallTempleGekkoRoomPot2, Item.CollectableWoodfallTempleGekkoRoomPot3, Item.CollectableWoodfallTempleGekkoRoomPot4)]
        [CheckRestricted(Scene.WoodfallTemple, variant: 0x4C02, Item.CollectableWoodfallTempleGekkoRoomPot1)]
        [CheckRestricted(Scene.WoodfallTemple, variant: 0x4E02, Item.CollectableWoodfallTempleGekkoRoomPot2)]
        [CheckRestricted(Scene.WoodfallTemple, variant: 0x5002, Item.CollectableWoodfallTempleGekkoRoomPot3)]
        [CheckRestricted(Scene.WoodfallTemple, variant: 0x5202, Item.CollectableWoodfallTempleGekkoRoomPot4)]
        [CheckRestricted(Scene.MountainVillage, variant: ActorConst.ANY_VARIANT, Item.CollectableMountainVillageWinterPot1)]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT, Item.CollectableMountainVillageSpringPot1)]
        [CheckRestricted(Scene.GoronShrine, variant: ActorConst.ANY_VARIANT,
            Item.CollectableGoronShrineGoronKidSRoomPot1, Item.CollectableGoronShrineGoronKidSRoomPot2, Item.CollectableGoronShrineGoronKidSRoomPot3,
            Item.CollectableGoronShrineMainRoomPot1, Item.CollectableGoronShrineMainRoomPot2, Item.CollectableGoronShrineMainRoomPot3,
            Item.CollectableGoronShrineMainRoomPot4, Item.CollectableGoronShrineMainRoomPot5, Item.CollectableGoronShrineMainRoomPot6,
            Item.CollectableGoronShrineMainRoomPot7, Item.CollectableGoronShrineMainRoomPot8
        )]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT,
            Item.CollectableGreatBayCoastPot1, Item.CollectableGreatBayCoastPot2, Item.CollectableGreatBayCoastPot3,
            Item.CollectableGreatBayCoastPot4, Item.CollectableGreatBayCoastPot5, Item.CollectableGreatBayCoastPot6,
            Item.CollectableGreatBayCoastPot7, Item.CollectableGreatBayCoastPot8, Item.CollectableGreatBayCoastPot9,
            Item.CollectableGreatBayCoastPot10, Item.CollectableGreatBayCoastPot11
        )]
        ///* 
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x601E, Item.CollectableOceansideSpiderHouseEntrancePot1)] 
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x621E, Item.CollectableOceansideSpiderHouseEntrancePot2)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x5C0E, Item.CollectableOceansideSpiderHouseEntrancePot3)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x018A, Item.CollectableOceansideSpiderHouseMainRoomPot1)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0xB, Item.CollectableOceansideSpiderHouseMainRoomPot2)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x741E, Item.CollectableOceansideSpiderHouseMaskRoomPot1)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: 0x761E, Item.CollectableOceansideSpiderHouseMaskRoomPot2)]
        // */
        /*[CheckRestricted(Scene.OceanSpiderHouse, variant: ActorConst.ANY_VARIANT,
            Item.CollectableOceansideSpiderHouseEntrancePot1, Item.CollectableOceansideSpiderHouseEntrancePot2, Item.CollectableOceansideSpiderHouseEntrancePot3,
            Item.CollectableOceansideSpiderHouseMainRoomPot1, Item.CollectableOceansideSpiderHouseMainRoomPot2,
            Item.CollectableOceansideSpiderHouseMaskRoomPot1, Item.CollectableOceansideSpiderHouseMaskRoomPot2)] */
        // these are dungeon object, we can specify per variant
        [CheckRestricted(Scene.PiratesFortressRooms, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePiratesFortressInterior100RupeeEggRoomPot1,
            Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartPot1,
            Item.CollectablePiratesFortressInteriorTwinBarrelEggRoomPot1,
            Item.CollectablePiratesFortressInteriorTelescopeRoomPot1, Item.CollectablePiratesFortressInteriorTelescopeRoomPot2,
            Item.CollectablePiratesFortressInteriorWaterCurrentRoomPot1,
            Item.CollectablePiratesFortressInteriorBarrelRoomEggPot1, Item.CollectablePiratesFortressInteriorBarrelRoomEggPot2,
            Item.CollectablePiratesFortressInteriorHookshotRoomPot1, Item.CollectablePiratesFortressInteriorHookshotRoomPot2
        )]
        [CheckRestricted(Scene.PinnacleRock, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePinnacleRockPot1, Item.CollectablePinnacleRockPot2, Item.CollectablePinnacleRockPot3, Item.CollectablePinnacleRockPot4)]
        [CheckRestricted(Scene.ZoraCape, variant: ActorConst.ANY_VARIANT, Item.CollectableZoraCapeJarGame1,
            Item.CollectableZoraCapePot1, Item.CollectableZoraCapePot2, Item.CollectableZoraCapePot3, Item.CollectableZoraCapePot4, Item.CollectableZoraCapePot5)]
        [CheckRestricted(Scene.RoadToIkana, variant: ActorConst.ANY_VARIANT, Item.CollectableRoadToIkanaPot1)]
        [CheckRestricted(Scene.BeneathGraveyard, variant: ActorConst.ANY_VARIANT,
            Item.CollectableBeneathTheGraveyardBadBatRoomPot1, Item.CollectableBeneathTheGraveyardInvisibleRoomPot1,
            Item.CollectableBeneathTheGraveyardMainAreaPot1, Item.CollectableBeneathTheGraveyardMainAreaPot2)]
        [CheckRestricted(Scene.DampesHouse, variant: ActorConst.ANY_VARIANT,
            Item.CollectableDampesHouseBasementPot1, Item.CollectableDampesHouseBasementPot2,
            Item.CollectableDampesHouseBasementPot3, Item.CollectableDampesHouseBasementPot4,
            Item.CollectableDampesHouseBasementPot5, Item.CollectableDampesHouseBasementPot6,
            Item.CollectableDampesHouseBasementPot7, Item.CollectableDampesHouseBasementPot8)]
        [CheckRestricted(Scene.IkanaCastle, variant: ActorConst.ANY_VARIANT,
            Item.CollectableAncientCastleOfIkana1FWestStaircasePot1, Item.CollectableAncientCastleOfIkanaCastleExteriorPot1, Item.CollectableAncientCastleOfIkanaFireCeilingRoomPot1,
            Item.CollectableAncientCastleOfIkanaHoleRoomPot1, Item.CollectableAncientCastleOfIkanaHoleRoomPot2, Item.CollectableAncientCastleOfIkanaHoleRoomPot3, Item.CollectableAncientCastleOfIkanaHoleRoomPot4)]
        [CheckRestricted(Scene.IgosDuIkanasLair, variant: ActorConst.ANY_VARIANT,
            Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot1, Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot2, Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot3,
            Item.CollectableIgosDuIkanaSLairPreBossRoomPot1, Item.CollectableIgosDuIkanaSLairPreBossRoomPot2, Item.CollectableIgosDuIkanaSLairPreBossRoomPot3)]
        [CheckRestricted(Scene.StoneTower, variant: ActorConst.ANY_VARIANT,
            Item.CollectableStoneTowerPot1, Item.CollectableStoneTowerPot2, Item.CollectableStoneTowerPot3, Item.CollectableStoneTowerPot4, Item.CollectableStoneTowerPot5,
            Item.CollectableStoneTowerPot6, Item.CollectableStoneTowerPot7, Item.CollectableStoneTowerPot8, Item.CollectableStoneTowerPot9, Item.CollectableStoneTowerPot10, 
            Item.CollectableStoneTowerPot11, Item.CollectableStoneTowerPot12, Item.CollectableStoneTowerPot13, Item.CollectableStoneTowerPot14)]
        // cannot randomize temple pots yet, uses dungeon keep objects, this will come later
        [CheckRestricted(Scene.InvertedStoneTower, variant: ActorConst.ANY_VARIANT, Item.CollectableStoneTowerInvertedStoneTowerFlippedPot1, Item.CollectableStoneTowerInvertedStoneTowerFlippedPot2, Item.CollectableStoneTowerInvertedStoneTowerFlippedPot3)]
        [CheckRestricted(Scene.StoneTowerTemple, variant: ActorConst.ANY_VARIANT, Item.CollectableStoneTowerTempleInvertedWizzrobeRoomPot1)]
        [CheckRestricted(Scene.SecretShrine, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSecretShrineMainRoomPot1, Item.CollectableSecretShrineMainRoomPot2, Item.CollectableSecretShrineMainRoomPot3, Item.CollectableSecretShrineMainRoomPot4,
            Item.CollectableSecretShrineMainRoomPot5)]
        // 0xF9 is pot and pot shard
        // according to CM, 0x100 is available everywhere as a pot, where 0x3F defines the drop item
        // so 1F is arrows, F is magic, B is three small rups? 7 is huge 200 rup, 17 is empty
        // 0xA is one rup, 1A and 14 are empty 04 is 20 rupes, 
        // 100 is empty, 110 is fairy, 10E is stick//////
        // 115 is 5 bombs, 105 is tall dodongo 50 rup, 106 is empty, 116 is empty
        // 101 is one rup, 111 SKULL TOKEN POT??!? 102 was 5 rups 112 empty
        // 103 empty, 113 is 10 deku nuts, 104 is red rup, 114 is empty
        //[GroundVariants(0x110)] // testing // 115 101 106 10E 10F
        [GroundVariants(0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E, // good variety to place
            0x30A, // trading post (has nothing important
            0x202, 0x602, 0x802, 0xA02, 0xC02, // swords school
            0xF0A, // buisness scrub grotto
            0x0302, 0x0502, 0x0702, 0x0902, // doggy racetrack
            0x4B0A, 0x4D02, 0x4F01, // southern swamp
            0x410A, 0x4302, 0x4501, // southern swamp clear
            0x650E, 0x670E, // deku palace
            0x4310, 0x413, 0x4119, 0x4528, 0x4516, 0x4513,// woodfall
            0xFE01, // deku shrine
            0x4D10, 0xFF04, 0x4D10,// mountain village spring
            0xC719, 0xC90E, 0xCB0E, 0xCD19, 0xCF0F, 0xD10F, 0xD30F, 0xD519, 0xC119, 0xC319, 0xC50F, 0xC709, // goron shrine
            0x410E, 0x450A, 0x470A, 0x490A, 0x4B0A, 0x4D0E, 0x530A, 0x550A, 0x570E, 0x590A, 0x5B0E, // pinnacle rock
            0x471E, 0x590E, 0x531E, 0x4114, 0x4314, 0x4B1E, 0x4D0A, 0x5D0A, 0x5F0A, 0x5F03, 0x5714, // beneath the graveyard
            0x0B01, 0x0D01, 0x0F1E, 0x110A, 0x1301, 0x151E, 0x1705, 0x191E, 0x1B1E, 0x1D0A, // damps house
            0x7C10, 0x7E0B, 0x800B, 0x820E, 0x840B, 0x9C10, 0x9E0E, 0xA010, 0xB40E, 0x860B, 0x8813, 0x8A0B, 0x8C0B,// ikana castle
            0x430A, 0x450E, 0x4710, 0x4B10, 0x4D14, 0x4F0A, 0x5114, 0x5314, 0x570A, 0x5910, 0x5B14, 0x5D0E, 0x5F1E, 0x610A, 0x630E, // stone tower
            0x6514, 0x671E, 0x690E, 0x6B0A, 0x6D0A, 0x6F15, 0x711F, 0x7610, 0x750F, // stone tower (cont)
            0x460B, 0x4610, 0x018D, // stone tower temple
            0x6F0B, 0x710F, 0x7310, 0x7515, 0x770B, // inverted stone tower (is a field scene, but why)
            0xC00B, 0xC21E, 0xC40E, 0xFE0E, 0xFC0B, 0xFA1E, 0xF81E, 0xF81E, 0xF60E, 0xF410, // secret shrine
            0x4110)] // terminafield pot
        [VariantsWithRoomMax(max:0, variant: 0x460B, 0x4610, 0x018D, // stone tower temple (dungeon keep)
            0xFE01, // deku shrine (dungeon keep)
            0x1E, 0x5, // spiderhouse clay pots with spiders
            0x202, 0x602, 0x802, 0xA02, 0xC02)] // swords school, these are dungeon_keep pots cannot place without the object
        [ForbidFromScene( Scene.GoronRacetrack, // these are green pots they use a different object 
            Scene.SecretShrine, Scene.IkanaCastle, Scene.IgosDuIkanasLair, // dungeon pots, but treasure flags updater still messes with it
            Scene.DekuShrine,  Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple,
            Scene.AstralObservatory, Scene.GoronTrial, Scene.LinkTrial,
            Scene.MajorasLair)] // we want them for the fight
        [UnkillableAllVariants]
        [TreasureFlagsPlacement(mask: 0x1F, shift: 0)] // 0x3FC
        ClayPot = 0x82, // Obj_Tsubo

        Empty83 = 0x83,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x26EC)]
        [FileID(127)]
        [ObjectListIndex(0xD8)]
        // 0xFF(decrement) is armor type, the upper byte is completely unused and is even cleared
        [GroundVariants(0xFF03, 0xFF02, 0xFF01)]
        [WaterBottomVariants(0x0103, 0x0102, 0x0101)] // non vanilla
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 2, 0xFF03, 0xFF02, 0xFF01)]
        [VariantsWithRoomMax(max: 1, 0x0103, 0x0102, 0x0101)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.SouthernSwampClear)] // can crash the scheduler, no I dont know why
        IronKnuckle = 0x84, // En_Ik

        Empty85 = 0x85,
        Empty86 = 0x86,
        Empty87 = 0x87,
        Empty88 = 0x88,

        // empty, does nothing
        [FileID(128)]
        [ObjectListIndex(0153)]
        Demo_Shd = 0x89, // Demo_Shd

        [ActorizerEnabled]
        [FileID(129)]
        [ObjectListIndex(0x134)] // 0x86
        [GroundVariants(
            0x8000, 0x8001, 0x8002, 0x8003, // assumed hostile?
            0x4000, 0x4001, 0x4002, 0x4003,
            0x0, 0x1, 0x2, 0x3 // still aggro, kicks you out from across the map
            )]
        // kickout params
        // cannot place anywhere yet since the actor is hard coded based on temple clear, and the regular one is agressive
        [VariantsWithRoomMax(max:0, 
            0x8000, 0x8001, 0x8002, 0x8003, // attentive to the king
            0x4000, 0x4001, 0x4002, 0x4003, // dancing around the fire I think
            0x0, 0x1, 0x2, 0x3
        )]
        [UnkillableAllVariants]
        DekuPalaceChamberGuard = 0x8A, // En_Dns

        [FileID(130)]
        [ObjectListIndex(0001)]
        ProximityTatlTalkingSpot = 0x8B, // Elf_Msg

        [EnemizerEnabled]
        [FileID(131)]
        [ObjectListIndex(3)]
        // 3 is vanilla wall of flames, 0 spits a single flame that chases you
        [WallVariants(0, 3)]
        [UnkillableAllVariants]
        FlameEyeTrap = 0x8C, // En_Honotrap

        [EnemizerEnabled]
        [FileID(132)]
        [ActorInitVarOffset(0xC5C)]
        [ObjectListIndex(3)] // dungeon_keep, obj 3
        // params: 0x3F is item to drop, 0x7F00 is switch flags to set upon kill (the flying pot does NOT check to stop spawning on flag tho)
        [GroundVariants(0x15, 0x01, 0x06, 0x0E, 0x0F, 0x00)] // actually spawns thank god, only in dungeons though, but outside its just an empty space so thats fine
        //[UnkillableAllVariants] // starts as non-enemy, converts to enemy when it attacks, sooo could be killable
        FlyingPot = 0x8D, // En_Tubo_Trap

        // iceblock used for frozen enemies and a few locations without an object
        [ActorizerEnabled]
        [FileID(133)]
        [ObjectListIndex(1)]
        [ForbidFromScene(Scene.MountainSmithy, Scene.SnowheadTemple, Scene.TerminaField)] // TODO figure out if I want to split these or not
        [CheckRestricted(Scene.TwinIslands, ActorConst.ANY_VARIANT,
            check: Item.HeartPieceTwinIslandsChest, Item.ChestHotSpringGrottoRedRupee, Item.BottleCatchHotSpringWater)]
        [CheckRestricted(Scene.GoronVillage, ActorConst.ANY_VARIANT,
            check: Item.ItemPowderKeg)]
        //[CheckRestricted(Scene.MountainSmithy, ActorConst.ANY_VARIANT,
        //    check: )]
        [CheckRestricted(Scene.GreatBayTemple, ActorConst.ANY_VARIANT,
            check: Item.ItemGreatBayBossKey, Item.FrogGreatBayTemple)]
        //params 0xFF is size
        // 0xFF00 is switch flag, for things like
        // if 0xFFXX then dont check flags
        // 64 is size of HSW, 0xC8 in one of snowhead rooms, AA is compass room?
        // ice block room has 0x64 and 0x96, 0xFF in goron block puzzle room
        // smithy uses size 0x78, 0x10 is smol
        [GroundVariants(0xFF10, 0xFF20, 0xFF64, 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        [BlockingVariants(0xFF64, 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        // all restricted because they add colliders which limits our BGcheck options for other things
        [VariantsWithRoomMax(max: 1, variant: 0xFF10, 0xFF20, 0xFF64, 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        //[VariantsWithRoomMax(max: 1, variant: 0xFFC8, 0xFF96, 0xFF78)]
        [PlacementWeight(60)]
        RegularIceBlock = 0x8E, // Obj_Ice_Poly

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2240)]
        [FileID(134)]
        [ObjectListIndex(0xE4)]
        [GroundVariants(02, 0x2001, 0x300F, 0x100F)]
        [VariantsWithRoomMax(max: 2, variant: 02, 0x2001, 0x300F, 0x100F)] // 8 is more than enough
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // Slowing enemies
        Freezard = 0x8F, // En_Fz

        // damn it, another multi-object actor
        [ActorizerEnabled]
        [ActorInstanceSize(0x19C)]
        [FileID(135)]
        [ObjectListIndex(0xF8)] // type 0 (0x3) is FIELD_KEEP, handled in code because we dont have working code for multi-object
        [CheckRestricted(Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT, Item.CollectableSouthernSwampClearCentralSwampGrass1, Item.CollectableSouthernSwampClearCentralSwampGrass2)]
        [CheckRestricted(Scene.MilkRoad, variant: ActorConst.ANY_VARIANT, Item.CollectableMilkRoadGrass1, Item.CollectableMilkRoadGrass2, Item.CollectableMilkRoadGrass3)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: ActorConst.ANY_VARIANT,
            Item.CollectableIkanaGraveyardIkanaGraveyardLowerGrass1, Item.CollectableIkanaGraveyardIkanaGraveyardLowerGrass2, Item.CollectableIkanaGraveyardIkanaGraveyardLowerGrass3)]
        // 0 uses field keep to draw regular grass, like ObjGrassUnit
        // 1 creates a grass circle in termina field, 0 is grotto grass single
        // 642B is a smaller cuttable grass from the ground in secret 
        //[GroundVariants(0, 1)]
        [GroundVariants(
            0, // gossip stones use this, but its field_keep versions
            0x0800, // single in woods of mystery, field_keep
            0x0600, 0x700, 0xC00, 0xD00, // woodfall temple
            0x0610, // greay bay coast
            0x0E00, 0x0E10, 0x0010, // secret jgrotto?
            0x634F, 0x642B, 0x654F, 0x662B, 0x672B, 0x682B, 0x602B, 0x614F, 0x622B, 0x634F, 0x602B, // secret shrine (1)
            0x617B, 0x622B, 0x637B, 0x642B, 0x602B, 0x617B, 0x622B, 0x632B, 0x642B, 0x657B, 0x662B, // secret shrine (2)
            0x677B, 0x602F, 0x612B, 0x627B, 0x634F, 0x642B, 0x654F, // secret shrine (3)
            0x203F, 0x2157, 0x227F, 0x2343, 0x463F, 0x4757, 0x487F, 0x4943, // ikana canyon
            0x2507, 0x272B, 0x282B, 0x263B, 0x297B, 0x0203, 0x0403, 0x0107, 0x032B, 0x053B, // graveyard
            0x2043, 0x217F, 0x223F, //milkroad
            // TODO FINISH
            0x23FF, 0x24FF, 0x2667, 0x2743 // southern swamp?
        )]
        [VariantsWithRoomMax(max:0, 0x0800,0x0600, 0x700, 0xC00, 0xD00, 0x0610)]
        [UnkillableAllVariants]
        //[RespawningAllVariants] // some of them come back over and over, but its a PROP type actor
        [AlignedCompanionActor(Actor.Fairy, CompanionAlignment.OnTop, ourVariant: 1, variant: 2, 7, 9)] // fairies love grass
        // for now, until I can identify which ones have drops we need to be careful of, going to block all randimization
        [ForbidFromScene(Scene.SouthernSwamp, Scene.OdolwasLair,
            Scene.IkanaCastle, Scene.StoneTowerTemple, Scene.Woodfall, Scene.GreatBayCoast,
            Scene.SecretShrine, Scene.MountainVillageSpring, //Scene.WoodsOfMystery,
            Scene.LaundryPool, Scene.SnowheadTemple, Scene.RoadToSouthernSwamp,
            //Scene.MilkRoad,
            Scene.IkanaCanyon,
            //Scene.Grottos,
            Scene.BeneathTheWell,
            Scene.WoodfallTemple)]
        [PlacementWeight(75)] // object is tiny, the weight is gonna need to be small because of how common it is
        TallGrass = 0x90, // En_Kusa

        // this one might be a pain... without modification it looks like the actor wants to be doubled up on top of itself
        [ActorizerEnabled]
        [FileID(136)]
        [ObjectListIndex(0xEE)]
        [DynaAttributes(6,8)]
        // TODO add secret shrine and swamp spiderhouse
        [CheckRestricted(Scene.RomaniRanch, variant: ActorConst.ANY_VARIANT, Item.CollectableRomaniRanchSoftSoil1, Item.CollectableRomaniRanchSoftSoil2)]
        [CheckRestricted(Scene.Grottos, variant: ActorConst.ANY_VARIANT, Item.CollectableBeanGrottoSoftSoil1, Item.ChestBeanGrottoRedRupee)]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT, Item.CollectableGreatBayCoastSoftSoil1, Item.HeartPieceGreatBayCoast)]
        [CheckRestricted(Scene.DoggyRacetrack, variant: ActorConst.ANY_VARIANT, Item.CollectableDoggyRacetrackSoftSoil1)]
        [CheckRestricted(Scene.SecretShrine, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSecretShrineSoftSoil1,
            Item.CollectableSecretShrineEntranceRoomItem1, Item.CollectableSecretShrineEntranceRoomItem2, Item.CollectableSecretShrineEntranceRoomItem3,
            Item.CollectableSecretShrineEntranceRoomItem4, Item.CollectableSecretShrineEntranceRoomItem5, Item.CollectableSecretShrineEntranceRoomItem6,
            Item.CollectableSecretShrineEntranceRoomItem7, Item.CollectableSecretShrineEntranceRoomItem8, Item.CollectableSecretShrineEntranceRoomItem9,
            Item.CollectableSecretShrineEntranceRoomItem10, Item.CollectableSecretShrineEntranceRoomItem11, Item.CollectableSecretShrineEntranceRoomItem12,
            Item.CollectableSecretShrineEntranceRoomItem13, Item.CollectableSecretShrineEntranceRoomItem14, Item.CollectableSecretShrineEntranceRoomItem15,
            Item.CollectableSecretShrineEntranceRoomItem16, Item.CollectableSecretShrineEntranceRoomItem17
        )]
        [CheckRestricted(Scene.SwampSpiderHouse, variant: ActorConst.ANY_VARIANT, Item.CollectableSwampSpiderHouseSoftSoil1, Item.CollectableSwampSpiderHouseSoftSoil2,
            Item.CollectibleSwampSpiderToken2, Item.CollectibleSwampSpiderToken13, // ride bean to reach
            Item.CollectibleSwampSpiderToken9, Item.CollectibleSwampSpiderToken11, Item.CollectibleSwampSpiderToken12 // in soil
        )]
        [CheckRestricted(Scene.DekuPalace, variant: ActorConst.ANY_VARIANT, Item.SongSonata,
            Item.CollectableDekuPalaceSoftSoil1, Item.CollectableDekuPalaceEastInnerGardenPot1, Item.CollectableDekuPalaceEastInnerGardenPot2)]
        [CheckRestricted(Scene.InvertedStoneTower, variant: ActorConst.ANY_VARIANT, Item.CollectableStoneTowerSoftSoil1, Item.CollectableStoneTowerSoftSoil2,
            Item.CollectableStoneTowerInvertedStoneTowerFlippedPot1, Item.CollectableStoneTowerInvertedStoneTowerFlippedPot2, Item.CollectableStoneTowerInvertedStoneTowerFlippedPot3,
            Item.ChestInvertedStoneTowerBean, Item.ChestInvertedStoneTowerBombchu10, Item.ChestInvertedStoneTowerSilverRupee)]
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT,
            Item.CollectableTerminaFieldPot1,
            Item.ChestTerminaStumpRedRupee,
            Item.CollectableTerminaFieldSoftSoil1, Item.CollectableTerminaFieldSoftSoil2, Item.CollectableTerminaFieldSoftSoil3, Item.CollectableTerminaFieldSoftSoil4
        )]
        // some of these are pathing type, however they will need flying pathing type to make snese
        // 8 is unused crack in the wall, only exists in unused ranch setup
        // uses Z rot as a param, unknown use
        // 0xC000 unk, can change draw type
        // 0x80 determins if switch flags are active, great..
        // TODO which of these are invisble and require action?
        [GroundVariants(0x4000, 0x8000,
            0x4101, 0x0102, // bean grotto
            0x0304, 0x4203, 0x4080, 0x1,// swamp spiderhouse
            0x4D19, 0x061A, 0x071A, // great bay coast
            0x4203, 0x0D04, // deku palace
            0x0101, // doggy race track
            0x0401, 0x0504, // inverted stone tower
            0x4305, 0x460B, 0x478E, 0x0506, 0x070C, 0x060F // termina field
        )]
        [WallVariants(0x4000, 0x8000,
            0x468C, 0x4509, 0x480F,  // swamp spiderhouse
            0x4A94)] // ranch and termina field
        [UnkillableAllVariants]
        // c and 0 crash without a path to follow, but with a path they instant kill which is odd...
        // havent documented enough to know why
        //[PathingVariants(0x4000)] // TODO figure out if I even can get this to work
        [PathingTypeVarsPlacement(mask: 0x3F, shift: 8)] // 0x3F00
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [VariantsWithRoomMax(max:0, variant: 0x4000, 0x8000, // one of these crashes, not sure which yet TODO later
            0x0102, // bean grotto
            0x304, 0x4203, 0x001,// swamp spiderhouse
            0x061A, 0x071A, // great bay coast
            0x0D04, // deku palace
            0x0101, // doggy race track
            0x0401, 0x0504, // inverted stone tower
            0x0506, 0x070C, 0x060F // termina field
        )]
        [AlignedCompanionActor(GoldSkulltula, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0xFF53, 0x55B, 0x637, 0xFF07, 0x113, 0x21B, 0x91F, 0xFF56, 0xFF62, 0xFF76, 0xFF03, 0x909, 0xB0C, 0xC0F)]
        //[ForbidFromScene(Scene.SwampSpiderHouse )] // dont want to mess with this by accident until I know it has proper logic
        SoftSoilAndBeans = 0x91, // Obj_Bean

        [ActorizerEnabled]
        [FileID(137)]
        [ObjectListIndex(0x12A)]
        // funny enough, not dynapoly
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT,
            check: Item.HeartPieceTerminaGossipStones, Item.HeartPieceZoraGrotto, Item.CollectableGrottosOceanHeartPieceGrottoBeehive1, Item.CollectableGrottosOceanGossipStonesButterflyFairy1)]
        [CheckRestricted(Scene.Grottos, variant: ActorConst.ANY_VARIANT,//0x118,
            check: Item.ChestHotSpringGrottoRedRupee, Item.ChestLensCavePurpleRupee)] // hot spring grotto
        // dont think per variant is working for multiple per room yet once it is these should be split
        //[CheckRestricted(Scene.Grottos, variant: 0x104,
        //    check: Item.ChestLensCavePurpleRupee)] // bomb grotto
        [CheckRestricted(Scene.SwampSpiderHouse, variant: ActorConst.ANY_VARIANT,
            check: Item.CollectibleSwampSpiderToken13, Item.CollectableSwampSpiderHouseSoftSoil2)]
        [CheckRestricted(Scene.ZoraCape, variant: ActorConst.ANY_VARIANT,
            check: Item.ChestGreatBayCapeGrotto, Item.FairyDoubleDefense)]
        // 0x0114-8 are the bombable rocks in hotspring water
        // params: 0x100 is the big bombable one only, no goron punch
        // 0x8000 creates a Good Job jingle when you break it
        // 0x7F is switch flags
        [GroundVariants(0x807F, 0x8004, 0x8002, // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
            0xE, // swamp spiderhouse
            0x0114, 0x0115, 0x0116, 0x0117, 0x0118,
            0x0102, 0x103, 0x104, 0x105, 0x106, // road to ikana
            0x101, 0x100, // cape covering the fairy hole
            0x0114, 0x0115, 0x0116, 0x0117, 0x0118, // hotspring water
            0x8003, 0x807F)]
        [FlyingVariants(0x44, 0x8044)] // does not exist, for fun placement
        [WaterBottomVariants(0x07F, // exists under a sign in the deku palace
            0x8077)] // does not exist, used for the bottom of the ocean signs in pinnacle rock (hack)
        [VariantsWithRoomMax(max: 3, variant: 0x807F, 0x8004)] // one of these when you break it gives a jingle, you found a puzzle, kind of jingle
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x7000, 0xC000, 0xE000, 0xF000, 0xD000)] // regular unhidden grottos
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        //[ForbidFromScene(Scene.Grottos)] //Scene.ZoraCape, Scene.GreatBayCoast
        //[EnemizerScenesPlacementBlock(// Scene.IkanaGraveyard, Scene.SouthernSwamp, Scene.SouthernSwampClear 
        //    /* Scene.Woodfall, Scene.DekuShrine */)] // blocking enemies
        [BlockingVariantsAll] // especially the hotwater rocks
        [PlacementWeight(90)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        Bombiwa = 0x92, // Obj_Bombiwa

        // multiple different kinds of switches:
        // floor switches, glass, eyeball, ect
        [ActorizerEnabled]
        [FileID(138)]
        [ObjectListIndex(3)] // bleh, always with the dangeon object
        [DynaAttributes(12,12)]
        //[ObjectListIndex(0x4B)] // fake for object force testing
        // params are filled
        // type is 0x7 range,0/1 are floor switches, 2 is eye switch, 3 and 4 are crystal, 5 is draw again
        // subtype describes if its set once or toggle or if it resets once you step off
        // z rot & 1 used for color, but only for the floor switches in sakons hideout (lol)
        [GroundVariants(0x0, 0x20, 0x1, 0x43, 0x14, 0x5)]
        // TODO get wall rotations working so I can just set some on the wall, wall crystal switches make sense
        [WallVariants(0x2,
            0x902, // stone tower temple
            0x1D82 // stone tower temple
        )]
        [WaterBottomVariants(0x0, 0x1, 0x3, 0x4)]
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        [VariantsWithRoomMax(max:0, variant: 0x2, 0x902, 0x1D82)] // wall types, currently they are the only actor that can be put on free wall spots that break (itemizer overwiting vars)
        [ForbidFromScene(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple,
            Scene.BeneathTheWell, Scene.DekuShrine, Scene.IkanaCastle, Scene.PiratesFortressRooms, Scene.SwampSpiderHouse)]
        ObjSwitch = 0x93, // Obj_Switch

        Empty94 = 0x94, // Empty94

        [FileID(139)]
        [DynaAttributes(12, 8)]
        [ObjectListIndex(0xED)]
        Obj_Lift = 0x95, // Obj_Lift

        // tiny hookshot wall block, and the pillar you can hookshot, but unused?
        [ActorizerEnabled]
        [FileID(140)]
        [ObjectListIndex(0xEC)] // really? its not deungeon keep?
        [DynaAttributes(18, 12)] // this is for the post, the wall targets are only 2/4, but not making a different version per-variant
        // looking at the code, this object and this actor have a post version, &3 = 0 or = 1
        // except 1 by itself does not spawn, 
        [GroundVariants(0x0)] // blue
        [WallVariants(0x42, // stone tower basement
            0xC2, // pirates fortress exterior
            0x82, // great bay temple
            0x0002)] // ocean spiderhouse entrance
        [VariantsWithRoomMax(max:10, variant: 0x0)] // bit boring
        [ForbidFromScene(Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple, Scene.OceanSpiderHouse,
                         Scene.GreatBayTemple, Scene.PiratesFortressExterior)]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard, Scene.SouthernSwamp, Scene.SouthernSwampClear, // asummed dyna crash
        //    Scene.StoneTower)]
        [UnkillableAllVariants]
        HookshotWallAndPillar = 0x96, // Obj_Hsblock

        // ??
        [FileID(141)]
        [ObjectListIndex(0001)]
        En_Okarina_Tag = 0x97, // En_Okarina_Tag

        Empty98 = 0x98,

        // one day I need to try turning these things into pathing actors
        [FileID(142)]
        [ObjectListIndex(0xEF)]
        RollingBoulder = 0x99, // En_Goroiwa

        Empty9A = 0x9A,
        Empty9B = 0x9B,

        [ActorizerEnabled]
        [ObjectListIndex(0xF1)]
        [FileID(143)]
        [CheckRestricted(Scene.MayorsResidence, variant: ActorConst.ANY_VARIANT, Item.HeartPieceNotebookMayor, Item.NotebookMeetMayorDotour, Item.NotebookDotoursThanks)]
        // 1 scoffing at poster, 2 is shouting at the sky looker
        // 0x03 is a walking type
        [GroundVariants(1, 2,
            0)] // in mayor meeting
        [VariantsWithRoomMax(max: 0, variant: 0)]
        [PathingVariants(0x603, 0x503,
            0x0303// in southclock town doing what?
        )]
        [PathingTypeVarsPlacement(mask: 0xFF00, shift: 8)]
        //[AlignedCompanionActor(VariousWorldSounds2, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x0090)]
        [UnkillableAllVariants]
        [PlacementWeight(50)] // boring
        Carpenter = 0x9C, // En_Daiku

        // tag: lemons
        // multiple issues with placing in the world in actorizer:
        // they want a second object (Adult chicken) so they can transform during the train
        // they will dissapear from the overworld after you heal grog, need custom code to fix
        // when placed in the world if there are more than 10 they just stand still
        // they dont interact with the breman mask with less than 10 or without grog maybe
        [ActorizerEnabled]
        [FileID(144)]
        [ObjectListIndex(0xF2)]
        [CheckRestricted(Item.MaskBunnyHood, Item.NotebookMeetGrog, Item.NotebookGrogsThanks)]
        [GroundVariants(0x0FFF)]
        [VariantsWithRoomMax(max: 9, variant: 0x0FFF)] // 10 without grog is probably broken
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.CuccoShack)]
        CuccoChick = 0x9D, // En_Nwc

        // unsused, might have been part of lens on chest effect they wanted to do like OOT
        [FileID(145)]
        [ObjectListIndex(0001)]
        [TreasureFlagsPlacement(mask: 0x1F, shift: 8)]
        Unused_Item_Inbox = 0x9E, // Item_Inbox

        // pirate that tells leader they cant get near the eggs because of seasnakes
        // we can use though as-is though it seems
        // white shirt gerudo
        [ActorizerEnabled]
        [FileID(146)]
        [ObjectListIndex(0xE6)]
        // params: 0xFC00 is paths, 0xF is type?
        // 1/2/3 are all standing around but with different... hair styles
        // 0 is pointing in the distance
        [GroundVariants(0,
            1, 2, 3)]
        //[PathingVariants()]
        // pathing 0xFC00 >> A
        // this is the cutscene version, the one that is most likely to break
        // in some testing it just dissapears, but can it break?
        [VariantsWithRoomMax(max: 0, variant: 0)]
        [AlignedCompanionActor(Cow, CompanionAlignment.InFront, ourVariant:-1)]
        //[VariantsWithRoomMax(max: 0, variant: 1,2,3)] // testing
        [UnkillableAllVariants]
        CutscenePirate = 0x9F, // En_Ge1

        [FileID(147)]
        [ObjectListIndex(0001)]
        Obj_Blockstop = 0xA0, // Obj_Blockstop

        [FileID(148)]
        [ObjectListIndex(0001)]
        ShadowLink = 0xA1, // En_Sda

        // in MM this is NOT arwing, its a multi-use bomb effect actor
        // multiple explosion visual effects, light arrows, stuff like that
        [FileID(149)]
        [ObjectListIndex(0x1)]
        En_Clear_Tag = 0xA2, // En_Clear_Tag

        EmptyA3 = 0xA3,

        [ActorizerEnabled]
        [FileID(150)]
        [ObjectListIndex(0x248)]
        // FF is milkbar, 00 is walking through the reception area
        // 3 exists in granny's room in the inn, for title sequence.. so you can hear him walking toward the door???
        // 3 is also present in east clock town
        [CheckRestricted(Item.NotebookMeetGorman, Item.MaskCircusLeader, Item.NotebookMovingGorman)]
        [GroundVariants(0xFF,
            3,0)] // pathing we move
        //[sitting type]
        //[PathingVariants(3, 0)] // while he is technically pathing, his pathing data is always the exit door short path, boring
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        // behavior too complicated, disable placement anywhere
        [VariantsWithRoomMax(max: 0, variant: 0, 3, 0xFF)]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.MilkBar)]
        Gorman = 0xA4, // En_Gm

        [ActorizerEnabled]
        [FileID(151)]
        [ObjectListIndex(0xF4)]
        /* [CheckRestricted(Item.ItemMagicBean, // this got complicated, instead we need to check if the reward is important
            Item.ChestTerminaStumpRedRupee,
            Item.ChestInvertedStoneTowerBean, Item.ChestInvertedStoneTowerBombchu10, Item.ChestInvertedStoneTowerSilverRupee,
            Item.CollectableDekuPalaceEastInnerGardenPot1, Item.CollectableDekuPalaceEastInnerGardenPot2,
            Item.SongSonata,
            Item.HeartPieceGreatBayCoast,
            Item.CollectableSecretShrineEntranceRoomItem1, Item.CollectableSecretShrineEntranceRoomItem2, Item.CollectableSecretShrineEntranceRoomItem3,
            Item.CollectableSecretShrineEntranceRoomItem4, Item.CollectableSecretShrineEntranceRoomItem5, Item.CollectableSecretShrineEntranceRoomItem6,
            Item.CollectableSecretShrineEntranceRoomItem7, Item.CollectableSecretShrineEntranceRoomItem8, Item.CollectableSecretShrineEntranceRoomItem9,
            Item.CollectableSecretShrineEntranceRoomItem10, Item.CollectableSecretShrineEntranceRoomItem11, Item.CollectableSecretShrineEntranceRoomItem12,
            Item.CollectableSecretShrineEntranceRoomItem14, Item.CollectableSecretShrineEntranceRoomItem15, Item.CollectableSecretShrineEntranceRoomItem16,
            Item.CollectableSecretShrineEntranceRoomItem17,
            Item.ChestWellRightPurpleRupee, Item.ItemWellCowMilk,
            Item.CollectableAncientCastleOfIkanaCastleExteriorGrass1, Item.CollectableAncientCastleOfIkanaCastleExteriorGrass2, Item.CollectableAncientCastleOfIkanaCastleExteriorGrass3, Item.CollectableAncientCastleOfIkanaCastleExteriorGrass4,
            Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot1, Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot2, Item.CollectableIgosDuIkanaSLairIgosDuIkanaSRoomPot3,
            Item.CollectableIgosDuIkanaSLairPreBossRoomPot1, Item.CollectableIgosDuIkanaSLairPreBossRoomPot2,
            Item.CollectableAncientCastleOfIkana1FWestStaircasePot1,
            Item.CollectableAncientCastleOfIkanaFireCeilingRoomPot1,
            Item.CollectableAncientCastleOfIkanaHoleRoomPot1, Item.CollectableAncientCastleOfIkanaHoleRoomPot2, Item.CollectableAncientCastleOfIkanaHoleRoomPot3, Item.CollectableAncientCastleOfIkanaHoleRoomPot4,
            Item.CollectableStoneTowerInvertedStoneTowerFlippedPot1,Item.CollectableStoneTowerInvertedStoneTowerFlippedPot2,Item.CollectableStoneTowerInvertedStoneTowerFlippedPot3,
            Item.CollectibleSwampSpiderToken13, Item.CollectibleSwampSpiderToken25
            )] // */
        [GroundVariants(0)]
        [WaterBottomVariants(0)] // testing
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.Grottos)]
        BeanSeller = 0xA5, // En_Ms

        [ActorizerEnabled]
        [FileID(152)]
        [ObjectListIndex(0xF5)]
        [CheckRestricted(Item.MaskBunnyHood, Item.NotebookMeetGrog, Item.NotebookGrogsThanks)]
        [GroundVariants(0xFE01)] // vanilla, his actor doesnt use these though, might be garbage or might be used by some other actor
        [VariantsWithRoomMax(max: 1, variant: 0xFE01)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.CuccoShack)]
        Grog = 0xA6, // En_Hs

        [ActorizerEnabled]
        [FileID(153)]
        [ObjectListIndex(0x17F)]
        [DynaAttributes(55,35)] // yikes
        [CheckRestricted(scene: Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT, Item.HeartPieceBoatArchery)]
        // problem being we would have to check ALL checks after too many
        //[CheckRestricted(scene: Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT, Item.)]
        //[] path are 0xFF, bleh, paths, hope they dont crash without ANY path
        [WaterTopVariants(0x01)] // only vanilla version
        [VariantsWithRoomMax(max: 0, variant: 0x01)] // TODO try making version without paths as mmra
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.SouthernSwamp)]
        SwampBoat = 0xA7, // Bg_Ingate

        [ActorizerEnabled]
        [FileID(154)]
        [ObjectListIndex(0xFC)]
        [GroundVariants(0x5, 0x13, 0x14, 0x15, 0x19, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, // north
           0x17, 0x18, 0x4, // spring village
           0x3D, 0x11, 0x2, // , NCT, ranch
           0x3C, // termina field
           0x3, // path to mountain village
           0xA, // road to southern swamp
           0x3F, // east clock town
           0x1A, // twin islands spring
           0x21, 0x22, 0x23, 0x25, 0x26, 0x27, 0x28, 0x12, 0x2A, 0x2B, // great ocean
           0x7, 0x9, 0xB, 0xC, 0xE, 0x2A, 0x38, 0x3A, // swamp
           0x16, // mountain village spring
           0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34 // ikana
        )]
        /* // problem now is that we need to detect the ones only on the ocean bottom 
        [WaterBottomVariants(0x5, 0x13, 0x14, 0x15, 0x19, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, // north
            )]
        */
        [VariantsWithRoomMax(max: 1, variant:
           0x5, 0x13, 0x14, 0x15, 0x19, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20, // north
           0x17, 0x18, 0x4, // spring village
           0x3D, 0x11, 0x2, // , NCT, ranch
           0x3C, // termina field
           0x3, // path to mountain village
           0xA, // road to southern swamp
           0x3F, // east clock town
           0x1A, // twin islands spring
           0x21, 0x22, 0x23, 0x25, 0x26, 0x27, 0x28, 0x12, 0x2A, 0x2B, // great ocean
           0x7, 0x9, 0xB, 0xC, 0xE, 0x2A, 0x38, 0x3A, // swamp
           0x16, // mountain village spring
           0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33, 0x34 // ikana
        )]
        [UnkillableAllVariants]
        [PlacementWeight(60)]
        SquareSign = 0xA8, // En_Kanban

        EmptyA9 = 0xA9,

        // these are temporary, spawned by En_Niw
        [FileID(155)]
        [ObjectListIndex(0xF)]
        GangCucco = 0xAA, // En_Attack_Niw

        EmptyAB = 0xAB,
        EmptyAC = 0xAC,
        EmptyAD = 0xAD,

        [ActorizerEnabled]
        [FileID(156)]
        [ObjectListIndex(0xFE)]
        [CheckRestricted(Item.SongNewWaveBossaNova)]
        [GroundVariants(0xFFFF)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [PlacementWeight(80)]
        //[ForbidFromScene(Scene.MarineLab)]
        Scientist = 0xAE, // En_Mk

        [ActorizerEnabled]
        [FileID(157)]
        [ObjectListIndex(0xFD)]
        [CheckRestricted(Scene.GoronVillage, variant: ActorConst.ANY_VARIANT, Item.ItemLens, Item.ChestLensCaveRedRupee, Item.ChestLensCavePurpleRupee)]
        // path is 0xF000, if you set to max (F) then its none-pathing and just auto flies away
        [PerchingVariants(0xF18B, // southern swamp // and clear swamp??? he was there??
            0xF000,
            0xF180,
            0x2102, 0x1102, 0x0102)] // three different days of goron village
        // type: 1 is unused monkey text, broken, 3 is soaring hint, 2 is lens cave, 30 is falling feathers I think
        [GroundVariants(0xF180, // soaring hint version, works without path sweet
            0xF000)] // just sits there and stares at you, neat
        //[FlyingVariants()] // if we could make sure flying is never used alone, putting some falling feathers could be cool
        // variant 1
        //[GroundVariants(0xF080)] // instant talks to you with monkey dialgoue but talking doesnt end: softlock
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [ForbidFromScene(Scene.SouthernSwamp)] // since we want the hint
        [VariantsWithRoomMax(max: 1, variant: 0xF180)] // only want to waste one slot on the hint owl
        [VariantsWithRoomMax(max: 0, variant: 0x2102, 0x1102, 0x0102)] // these are pathing, do not place
        [VariantsWithRoomMax(max: 10, variant: 0xF000)]
        [UnkillableAllVariants]
        En_Owl = 0xAF, // En_Owl

        // MULTIPLE OBJECT ACTOR
        [ActorizerEnabled]
        [FileID(158)]
        [ActorInstanceSize(0x198)]
        [ObjectListIndex(0x2)] // pick up rock version
        [ForbidFromScene(Scene.InvertedStoneTower, Scene.SwampSpiderHouse)] // we want the bugs, I think its too much to ask players to leave to get bugs
        [CheckRestricted(Scene.TerminaField, 0x2844, Item.CollectableTerminaFieldRock1)]
        [CheckRestricted(Scene.TerminaField, 0x2A44, Item.CollectableTerminaFieldRock2)]
        [CheckRestricted(Scene.TerminaField, 0x2014, Item.CollectableTerminaFieldRock3)]
        [CheckRestricted(Scene.TerminaField, 0x2214, Item.CollectableTerminaFieldRock4)]
        [CheckRestricted(Scene.TerminaField, 0x2414, Item.CollectableTerminaFieldRock5)]
        [CheckRestricted(Scene.TerminaField, 0x2C14, Item.CollectableTerminaFieldRock6)]
        [CheckRestricted(Scene.TerminaField, 0x1E14, Item.CollectableTerminaFieldRock7)]
        [CheckRestricted(Scene.TerminaField, 0x1A24, Item.CollectableTerminaFieldRock8)]
        [CheckRestricted(Scene.TerminaField, 0x1C24, Item.CollectableTerminaFieldRock9)]
        [CheckRestricted(Scene.RomaniRanch, 0x3C, Item.CollectableRomaniRanchInvisibleItem6)]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT,
            Item.CollectableMountainVillageWinterMountainVillageSpringItem1)]
        [CheckRestricted(Scene.GreatBayCoast, variant: 0x32,
            Item.CollectableGreatBayCoastSoftSoil1)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: 0x4014, Item.CollectableIkanaGraveyardIkanaGraveyardUpperRock1)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: 0x4234, Item.CollectableIkanaGraveyardIkanaGraveyardUpperRock2)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: 0x4624, Item.CollectableIkanaGraveyardIkanaGraveyardUpperRock3)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: 0x4214, Item.CollectableIkanaGraveyardIkanaGraveyardUpperRock4)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: 0x4814, Item.CollectableIkanaGraveyardIkanaGraveyardUpperRock5)]
        //[ObjectListIndex(0x1F6)] // NEVER USED IN MM, damn thing lied to me, even the boulders are object 2
        // it actually uses one of two objects: gameplay_field or object_ishi, which is only in SSHouse
        //6a does not load
        // params:
        // 3 >> & 1 uses object_ishi instead of gameplay_field
        // sooo what is 0x1FE?
        // & F0 is drop type on destroy, 
        // 1 is silver boulder,, 0xFE01 are the silver boulder in zora cape
        // 0xFE00 is switch flags
        // 0x1F2 you get bugs if you pick it up, best version
        // A1 is boulder type (1) and A drop table
        [GroundVariants(0xFF00, 0xFF70, 0xFFA0, 0xFFB0, // non vanilla good drop tables
            0x1F2, 0xA1)]
        [WaterBottomVariants(0xFE01, // silver boulder
            0xFEF0)] // regular small rock (like in pinaccle)
        [WallVariants( 0xFF00, 0xFF70, 0xFFA0, 0xFFB0, // non vanilla good drop tables
            0x2A44, 0x2014, 0x2214, 0x2414, 0x2C14, 0x1E14, 0x1A24, 0x1C24, // tf wall
            0x4814, 0x4214, 0x4424, 0x4014, 0x4624)] // ikana graveyard
        [VariantsWithRoomMax(max: 3, variant: 0xFF00, 0xFF70, 0xFFA0, 0xFFB0,
            0x4814, 0x4214, 0x4424, 0x4014, 0x4624)]
        [BlockingVariants(0xA1, 0xFE01)] // boulder types
        [UnkillableAllVariants] // not enemy actor group, no fairy no clear room
        //[ForbidFromScene(Scene.TerminaField)] // dont replace them in TF
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x3F5F)]
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.OnTop, ourVariant: 1,
            variant: 0x6033, 0x603B, 0x6018, 0x605C, 0x8000, 0xA000, 0x7000, 0xC000, 0xE000, 0xF000, 0xD000)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        IshiRock = 0xB0, // En_Ishi

        [ActorizerEnabled] // works but a bit lame
        [FileID(159)]
        [ObjectListIndex(0x1BA)]
        [GroundVariants(0)]
        [PlacementWeight(30)]
        [UnkillableAllVariants]
        OrangeGraveyardFlower = 0xB1, // Obj_Hana

        [ActorizerEnabled]
        [FileID(160)]
        [ObjectListIndex(0xF7)]
        // xx30 is fake type, burn away if you hit them
        // we're NOT going to be includeing the flipping switches, in part because they are invisble and you wouldn't know they are there anyway
        // (they use the scene wall as their shape/texture)
        [WallVariants(0xB00, 0x1200, 0x1130)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.StoneTower, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple, Scene.SecretShrine)]
        SunSwitch = 0xB2, // Obj_Lightswitch

        [ActorizerEnabled]
        [FileID(161)]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1
        // 801, opening scene grass, 0x1FXX are ranch and TF
        // 0402 is ikana graveyard rock circle
        [CheckRestricted(Scene.IkanaGraveyard, variant:0x402, Item.ChestGraveyardGrotto)]
        [GroundVariants(0x801, 0x1F02, 0x1F00, 0x0402)]
        [WaterBottomVariants(0x0402)]
        [AlignedCompanionActor(Shiro, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0)] // shiro likes his rock friends
        [AlignedCompanionActor(BugsFishButterfly, CompanionAlignment.Above, ourVariant: -1,
            variant: 0x2324, 0x4324)] // butterflies over the bushes
        [AlignedCompanionActor(GrottoHole, CompanionAlignment.OnTop, ourVariant: 0402,
            variant: 0x8200, 0xA200, // secret japanese grottos, hidden
            0x6233, 0x623B, 0x6218, 0x625C)] // grottos that might hold checks, also hidden
        [AlignedCompanionActor(Shot_Sun, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x41)] // fairies love grass
        [ForbidFromScene(Scene.RoadToIkana)] // its right on top of shirou which gets confusing if two actors are on top of each other, visually it makes sense to leave this as a land mark
        [UnkillableAllVariants]
        GrassRockCluster = 0xB3, // Obj_Mure2

        EmptyB4 = 0xB4,

        [ActorizerEnabled]
        [FileID(162)]
        [ObjectListIndex(0x140)]
        [CheckRestricted(Item.MundaneItemHoneyAndDarlingPurpleRupee, Item.HeartPieceHoneyAndDarling)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(0, 0)] // crash on spawn without its platform
        [UnkillableAllVariants]
        HoneyAndDarling = 0xB5, // En_Fu

        EmptyB6 = 0xB6,
        EmptyB7 = 0xB7,

        // unused water vortex from water temple
        //[ActorizerEnabled] // we have a modified fix one now, adjusts height to water
        [FileID(163)]
        [WaterTopVariants(0)]
        [ObjectListIndex(0x106)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        En_Stream = 0xB8, // En_Stream

        //[ActorizerEnabled] // behavior is weird, hard to randomly place anywhere
        [FileID(164)]
        [ObjectListIndex(0x1)] // gamplaykeep obj 1
        [GroundVariants(1)] // neither 0 nor 1 work
        [UnkillableAllVariants]
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
        [CheckRestricted(Item.CollectableTerminaFieldTreeItem1)]
        // 0 does nothing just stands there and stares at you
        // 1 is climbing in the tree trying to get rups
        [GroundVariants(0)]
        [WaterBottomVariants(0)] // more fun
        //[WallVariants(1)] // facing the wrong way and no bonk, so not that interesting
        [VariantsWithRoomMax(max: 3, variant: 0)]
        //[EnemizerScenesPlacementBlock(Scene.RomaniRanch, Scene.Woodfall, Scene.DekuShrine)] // standing variant has really large collider
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // now that we can recycle objects, this scene will always have too many of him, its obnoxious
        [UnkillableAllVariants]
        En_Ani = 0xBD, // En_Ani

        EmptyBE = 0xBE,

        [ActorizerEnabled] // warp addresses are offsets, dangerous until we can hard code
        [FileID(167)]
        [ObjectListIndex(0x271)]
        [WaterBottomVariants(0x11, 0x422, 0x833, 0xC44)] // think this would be funny
        [PathingVariants(0x11, 0x422, 0x833, 0xC44)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [VariantsWithRoomMax(max: 1, variant: 0x11, 0x422, 0x833, 0xC44)]
        [PathingKickoutAddrVarsPlacement(mask: 0x3F0, shift: 4)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.TheMoon)]
        [PlacementWeight(5)]
        MoonChild = 0xBF, // En_Js

        EmptyC0 = 0xC0,
        EmptyC1 = 0xC1,
        EmptyC2 = 0xC2,
        EmptyC3 = 0xC3,

        [FileID(168)]
        [ObjectListIndex(0x1)]
        En_Okarina_Effect = 0xC4, // En_Okarina_Effect

        // yeah it just takes you back to file select if you hit two buttons, bad
        [FileID(169)]
        [ObjectListIndex(0x115)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        TitleLogo = 0xC5, // En_Mag

        [FileID(170)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        Elf_Msg2 = 0xC6, // Elf_Msg2

        // unused floating/flying stone
        [ActorizerEnabled]
        [FileID(171)]
        [ObjectListIndex(0x5C)]
        [DynaAttributes(28,16)]
        //[GroundVariants(0)]
        [FlyingVariants(0)]
        [VariantsWithRoomMax(max: 7, variant: 0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [FlyingToGroundHeightAdjustment(275)]
        // TODO go through all of these and recheck now that we can modify the height correctly
        [EnemizerScenesPlacementBlock(//Scene.DekuPalace,
            Scene.Grottos, Scene.AstralObservatory, Scene.ZoraHallRooms, Scene.DampesHouse, Scene.PiratesFortressRooms,
            Scene.GoronRacetrack, Scene.WaterfallRapids, Scene.GormanRaceTrack, Scene.RoadToIkana, Scene.IkanaCastle, Scene.BeneathGraveyard,
            Scene.SwampSpiderHouse, Scene.OceanSpiderHouse, Scene.GoronShrine, Scene.DekuShrine, // Scene.ZoraHall,
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple,
            Scene.StockPotInn, Scene.TradingPost, Scene.MayorsResidence,
            Scene.BeneathTheWell//,
            /* Scene.IkanaGraveyard, Scene.StoneTower */)] // dyna crash
        //[SwitchFlagsPlacement(mask: 0xFF, shift: 0)]
        UnusedStoneTowerPlatform = 0xC7, // Bg_F40_Swlift

        EmptyC8 = 0xC8,
        EmptyC9 = 0xC9,

        [ActorizerEnabled]
        [FileID(172)]
        [ActorInstanceSize(0x2A0)]
        [ObjectListIndex(0x11D)]
        [CheckRestricted(Scene.MountainVillage, variant: ActorConst.ANY_VARIANT, Item.CollectableMountainVillageWinterPot1)]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT, Item.CollectableMountainVillageSpringPot1)]
        [CheckRestricted(Scene.RoadToIkana, variant: ActorConst.ANY_VARIANT, Item.CollectableRoadToIkanaPot1)]
        // snowhead temple
        [CheckRestricted(Scene.PathToSnowhead, variant: ActorConst.ANY_VARIANT, Item.HeartPieceToSnowhead)]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT, Item.HeartPieceGreatBayCoast)]
        [CheckRestricted(Scene.StoneTower, variant: ActorConst.ANY_VARIANT,
            Item.CollectableStoneTowerPot1, Item.CollectableStoneTowerPot2,
            Item.CollectableStoneTowerPot6, Item.CollectableStoneTowerPot7, Item.CollectableStoneTowerPot8, Item.CollectableStoneTowerPot9,
            Item.CollectableStoneTowerPot10, Item.CollectableStoneTowerPot11, Item.CollectableStoneTowerPot12, Item.CollectableStoneTowerPot13)]
        [CheckRestricted(Scene.TwinIslandsSpring, variant: ActorConst.ANY_VARIANT, Item.ItemBottleGoronRace,
            Item.CollectableGoronRacetrackPot1, Item.CollectableGoronRacetrackPot2, Item.CollectableGoronRacetrackPot3,
            Item.CollectableGoronRacetrackPot4, Item.CollectableGoronRacetrackPot5, Item.CollectableGoronRacetrackPot6,
            Item.CollectableGoronRacetrackPot7, Item.CollectableGoronRacetrackPot8, Item.CollectableGoronRacetrackPot9, Item.CollectableGoronRacetrackPot10,
            Item.CollectableGoronRacetrackPot11, Item.CollectableGoronRacetrackPot12, Item.CollectableGoronRacetrackPot13,
            Item.CollectableGoronRacetrackPot14, Item.CollectableGoronRacetrackPot15, Item.CollectableGoronRacetrackPot16,
            Item.CollectableGoronRacetrackPot17, Item.CollectableGoronRacetrackPot18, Item.CollectableGoronRacetrackPot19, Item.CollectableGoronRacetrackPot20,
            Item.CollectableGoronRacetrackPot21, Item.CollectableGoronRacetrackPot22, Item.CollectableGoronRacetrackPot23,
            Item.CollectableGoronRacetrackPot24, Item.CollectableGoronRacetrackPot25, Item.CollectableGoronRacetrackPot26,
            Item.CollectableGoronRacetrackPot27, Item.CollectableGoronRacetrackPot28, Item.CollectableGoronRacetrackPot29, Item.CollectableGoronRacetrackPot30
            )]
        [CheckRestricted(Scene.TwinIslands, variant: ActorConst.ANY_VARIANT, Item.ItemBottleGoronRace, Item.ChestToGoronRaceGrotto,
            Item.CollectableGoronRacetrackPot1, Item.CollectableGoronRacetrackPot2, Item.CollectableGoronRacetrackPot3,
            Item.CollectableGoronRacetrackPot4, Item.CollectableGoronRacetrackPot5, Item.CollectableGoronRacetrackPot6,
            Item.CollectableGoronRacetrackPot7, Item.CollectableGoronRacetrackPot8, Item.CollectableGoronRacetrackPot9, Item.CollectableGoronRacetrackPot10,
            Item.CollectableGoronRacetrackPot11, Item.CollectableGoronRacetrackPot12, Item.CollectableGoronRacetrackPot13,
            Item.CollectableGoronRacetrackPot14, Item.CollectableGoronRacetrackPot15, Item.CollectableGoronRacetrackPot16,
            Item.CollectableGoronRacetrackPot17, Item.CollectableGoronRacetrackPot18, Item.CollectableGoronRacetrackPot19, Item.CollectableGoronRacetrackPot20,
            Item.CollectableGoronRacetrackPot21, Item.CollectableGoronRacetrackPot22, Item.CollectableGoronRacetrackPot23,
            Item.CollectableGoronRacetrackPot24, Item.CollectableGoronRacetrackPot25, Item.CollectableGoronRacetrackPot26,
            Item.CollectableGoronRacetrackPot27, Item.CollectableGoronRacetrackPot28, Item.CollectableGoronRacetrackPot29, Item.CollectableGoronRacetrackPot30
            )]
        //0x1200, 0x1B00, 0x2800 spawns from the ground after you play a song
        // versions: 1200, 1B00, 2800 shows up a lot, 2D00 stonetower, 3200 zora cape, 0x11D is zora cape
        // trading post version is 1
        // wish I could spawn the ones that dance so they are always dancing when the player gets there
        [GroundVariants( 0x3200, 0x2D00, 0x0F00, 0x1E00,
            1, 0x2800, 0x11D)]
        [WaterBottomVariants(1)]
        [VariantsWithRoomMax(max: 5, variant: 1)]
        // below ground is kinda boring..., we want above ground placement only
        [VariantsWithRoomMax(max: 0, variant: 0x11D, 0x0F00, 0x2800, 0x2D00, 0x3200, 0x1E00)]
        // except I'm okay with a few of them because then the player might stumble on one pulling out an ocarina
        // turns out if the player is glitching with ocarina items it can softlock
        //[VariantsWithRoomMax(max: 1, variant: 0x3200, 0x1E00)]
        [UnkillableAllVariants]
        // crash: if you teach song to him in TF the ice block cutscene triggers
        // if you try to teach him a song with more than one it can lock
        //[EnemizerScenesPlacementBlock(Scene.TradingPost, Scene.TerminaField)]
        [ForbidFromScene(Scene.TradingPost, // he now hints song of time, would have to hard code check if hes missing
            Scene.SnowheadTemple // difficult to identify if anything is important after, TODO
        )]//, Scene.AstralObservatory)] // re-disable this if playing Entrando
        Scarecrow = 0xCA, // En_Kakasi

        // think these control the push blocks in the sewer zora push puzzle
        // 0x85, 0x107
        [FileID(173)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)] // this actor can have switch flags on zrotation, but not used in vanilla
        [TreasureFlagsPlacement(mask: 0x1F, shift: 2)]
        PushableBlockSwitchFlagsHandler = 0xCB, // Obj_Makeoshihiki

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

        // this is related to giving fire arrows in oot, but also gives a fairy on sunsong
        //[ActorizerEnabled] // we want as companion only
        [FileID(176)]
        [ObjectListIndex(0x1)]
        //[ObjectListIndex(0x11D)] // testing with object
        // 40 is sun shot, 41 is storms fairy
        [GroundVariants(0x41)] // companion only
        //[FlyingVariants(0x0)] // "else" variant: gives item
        [VariantsWithRoomMax(max: 1, variant: 0x41, 0x40)] // invisible actor shouldnt get too many placements
        [UnkillableAllVariants]
        Shot_Sun = 0xD0, // Shot_Sun

        EmptyD1 = 0xD1,
        EmptyD2 = 0xD2,

        [FileID(177)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        Obj_Roomtimer = 0xD3, // Obj_Roomtimer

        [ActorizerEnabled]
        [FileID(178)]
        [ObjectListIndex(0x127)]
        [CheckRestricted(Item.MaskTruth)]
        [FlyingVariants(0x0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        CursedSpiderMan = 0xD4, // En_Ssh

        EmptyD5 = 0xD5, // EmptyD5

        [FileID(179)]
        [ObjectListIndex(0x1)]
        SongOfTimeOcarinaEffect = 0xD6, // Oceff_Wipe

        [FileID(209)]
        [ObjectListIndex(0x1)]
        Oceff_Storm = 0xD7, // Oceff_Storm

        // ? cutscene related?
        [FileID(210)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0xFF, shift: 0)]
        Obj_Demo = 0xD8, // Obj_Demo

        [FileID(211)]
        [ObjectListIndex(0x128)]
        En_Minislime = 0xD9, // En_Minislime

        [FileID(212)]
        [ObjectListIndex(0x1)]
        DekuNutProjectile = 0xDA, // En_Nutsball

        EmptyDB = 0xDB,
        EmptyDC = 0xDC,
        EmptyDD = 0xDD,
        EmptyDE = 0xDE,

        [FileID(213)]
        [ObjectListIndex(0x1)]
        EponaSongOcarinaEffect = 0xDF, // Oceff_Wipe2

        [FileID(214)]
        [ObjectListIndex(0x1)]
        SariaSongOcarinaEffects = 0xE0, // Oceff_Wipe3

        EmptyE1 = 0xE1,

        [ActorizerEnabled]
        [FileID(215)]
        [ObjectListIndex(0x132)]
        // 3FF is SCT dog, 0x22BF is spiderhouse dog, makes no sense if use mask
        // both frolicing dogs(EnDg) and racing dogs spawned by mamuyan
        // from mamuyan code we know the colors are 1-E shifted right by 5
        // colors: (white, brown, dark grey, bluedog, gold)
        // 0x001F params are unknown, they aren't checked in init
        [GroundVariants(0x20, 0x40, 0x60, 0x80, 0x120,
            0x29F, 0xA9F, // ranch
            0x03FF, 0x019F, 0x02BF)]
        [PathingVariants(0x019F, 0x0D9F, 0x03FF, 0x22BF,
            0x20, 0x40, 0x60, 0x80, 0x120)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 1, variant: 0x20, 0x40, 0x60, 0x80, 0x120,
            0x22BF, 0x03FF, 0x019F, 0x02BF, 0xD9F)] // this many dogs is enough honestly
        [VariantsWithRoomMax(max: 0, variant: 0x29F, 0xA9F)] // too high of a path
        [ForbidFromScene(//Scene.RanchBuildings,
            Scene.RomaniRanch)]//, Scene.SouthClockTown)]//, Scene.SwampSpiderHouse)]
        // dog safe areas: TF, roadtoSS, SS, SSC, deku palace, sspiderhouse
        // path to mountain village
        // now that I know what the path vars is, any area with at least one path per room should be safe for index:0 dogs
        // these used to be banned, but we should be able to use them now:
        // DekuShrine RoadToIkana GoronVillage
        [EnemizerScenesPlacementBlock(Scene.ClockTowerInterior, // cursed if put on hms
            Scene.Woodfall, // they fall off into the water and quietly swim, lame?
            Scene.MountainVillageSpring, Scene.RanchBuildings)] // crash because not enough paths
        Dog = 0xE2, // En_Dg

        [FileID(216)]
        [ObjectListIndex(0x20)]
        GoldSkultulaToken = 0xE3, // En_Si

        [ActorizerEnabled]
        [FileID(217)]
        [ObjectListIndex(0x1B9)]
        [CheckRestricted(Scene.SwampSpiderHouse, variant: ActorConst.ANY_VARIANT,
             Item.CollectibleSwampSpiderToken18, Item.CollectibleSwampSpiderToken23, Item.CollectibleSwampSpiderToken26, Item.CollectibleSwampSpiderToken28)]
        [CheckRestricted(Scene.Grottos, variant: ActorConst.ANY_VARIANT,
            Item.HeartPieceZoraGrotto, Item.CollectableGrottosOceanHeartPieceGrottoBeehive1)]
        [CheckRestricted(Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSouthernSwampPoisonedCentralSwampBeehive1)]
        [CheckRestricted(Scene.WoodfallTemple, variant: ActorConst.ANY_VARIANT,
            Item.CollectibleStrayFairyWoodfall14, Item.CollectibleStrayFairyWoodfall15, Item.CollectableWoodfallTempleEntranceRoomBeehive1)]
        // params:
        // 0x1F can become the skulltula params, 0x3FC is treasure flags for treasure types
        // 0x8000 and 0x80 are flags
        // known vars:
        // 90 is bee that spawns cutscene in pirates fortress
        // 0x82, 0x7F0E, 0x7F3F, 0x8017, 0x801C, 0x8012,  // swamp spiderhouse
        // 0x81, 0x1E11, 0x1D11, 0x2002, // woodfall temple
        // 0x020C, 0x0302, 0x0403, // zora grotto
        // 0x0083,  // cow grotto
        // 0x82,  // ocean grotto
        // 0x81, // mountain village
        // 0x2804, // poisoned swamp
        // variant issue: 81 is a ceiling AND a wall type
        [WallVariants(
            0x81, // mountain village spring is in a tree
            0x1E11 // wft elevator room
            )]
        [CeilingVariants(0x81,  0x83,
            0x82, 0x7F0A, 0x7F0E, 0x801A, 0x7F3F, 0x8017, 0x801C, 0x8012,  // swamp spiderhouse
            0x2002, // wft entrance room
            0x81, 0x1D11, // wft bridge room
            0x2804, // poisoned swamp
            0x20C, 0x403, 0x302, // zora grotto
            0x83// cow grotto
            )]
        //[VariantsWithRoomMax(max:0, variant:)]
        [ForbidFromScene(
            //Scene.WoodfallTemple,
            //Scene.Grottos,
            //Scene.SwampSpiderHouse,
            //Scene.SouthernSwamp,
            Scene.PiratesFortressRooms // required for cutscene to get actor to leave, for now
            )]
        [UnkillableAllVariants]
        // uses weekeventarg 83_02
        [TreasureFlagsPlacement(mask: 0xFF, shift: 2)] // 0x3FC
        HoneyComb = 0xE4, // Obj_Comb

        // now that we have dyna limits, this could be randomized because it will be rare because of dyna limits anyway
        [ActorizerEnabled]
        [FileID(218)]
        [ObjectListIndex(0x133)]
        [DynaAttributes(10, 8)]
        [CheckRestricted(Scene.SwampSpiderHouse, variant: ActorConst.ANY_VARIANT, Item.CollectibleSwampSpiderToken10, Item.CollectibleSwampSpiderToken27)]
        [CheckRestricted(Scene.RomaniRanch, variant: ActorConst.ANY_VARIANT, // 0x7F1F,
            Item.CollectableRomaniRanchWoodenCrateLarge1)]
        [CheckRestricted(Scene.CuccoShack, variant: ActorConst.ANY_VARIANT, // 0x7F1F,
            Item.CollectableCuccoShackWoodenCrateLarge1)]
        // not always active, only sometimes:q
        [TreasureFlagsPlacement(mask: 0x1F, shift: 2)]
        [GroundVariants(0x7F3F, // buisness scrub and pirates fortress
            //0x1E11, // sht compass room // fairy crate
            0x7F3F, // pirates fortess
            0x000B, // sht bridge room
            0x000F, // sht goron switch room
            0x7F1F,// romani ranch
            // there are more TODO add them from spider house and a few other places
            0x2604)] // stt mirror room
        [ForbidFromScene(Scene.SnowheadTemple,
            //Scene.SwampSpiderHouse,
            Scene.StoneTowerTemple,
            Scene.Grottos, // buisness scrib?>
            Scene.OceanSpiderHouse, Scene.GreatBayTemple)]
        [UnkillableAllVariants]
        [PlacementWeight(55)]
        LargeWoodenCrate = 0xE5, // Obj_Kibako2

        EmptyE6 = 0xE6, // EmptyE6

        // in vanilla this was an empty actor with almost no code
        // going to try to force this into a new actor as a workaround
        [FileID(219)]
        [ObjectListIndex(0x199)] // modified for twig, OG was empty actor
        Unused_En_Hs2 = 0xE7, // En_Hs2

        //[ActorizerEnabled] // probably hard coded by zoey for rupee rando
        [FileID(220)]
        [ObjectListIndex(0x1)]
        //[GroundVariants()]
        GroupRupeeSpawner = 0xE8, // Obj_Mure3

        // honey and darling from the credits
        // this... cutscene actor has a damage table..
        [ActorizerEnabled]
        [FileID(221)]
        [ObjectListIndex(0x140)]
        [GroundVariants(0)] // credits version with no dialogue but at least htey have a collider?
        [VariantsWithRoomMax(max:5, variant:0)]
        [UnkillableAllVariants]
        HoneyAndDarlingCredits = 0xE9, // En_Tg

        EmptyEA = 0xEA,
        EmptyEB = 0xEB,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3E40)]
        [FileID(222)]
        [ObjectListIndex(0x141)]
        // params: 0x1 is winter coat, 0x80 is with ice block
        // ice block versions are limited because they are complicated collision and really long draw distance
        [GroundVariants(0xFF01, 0xFF81, 0xFF00, 0xFF80)]
        // they don't respawn, but if placed on a spawn next to a wall they can get pushed through the wall and become unkillable
        // so we do not want to put them in rooms where you have to clear all enemies
        [RespawningVariants(0xFF80, 0xFF81)]
        [BlockingVariants(0xFF80, 0xFF81)] // iceblock they spawn
        [VariantsWithRoomMax(max: 1, variant: 0xFF81)]
        [VariantsWithRoomMax(max: 1, variant: 0xFF80)]
        Wolfos = 0xEC, // En_Wf

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D60)]
        [FileID(223)]
        [ObjectListIndex(0x142)]
        // 0x0042 is swinging from tree, looks stupid if spawns in the ground,
        // 0x0022 is sitting on the edge of a bookcase, looks weird on the ground
        [PerchingVariants(0x42, 0x22)]
        [GroundVariants(0x0032)] // 0x32: sitting around the fire
        [CompanionActor(Flame, ourVariant: -1, variant: 0x7F4)] // they like fire in this game
        [ForbidFromScene(Scene.IkanaGraveyard, Scene.OceanSpiderHouse)]
        [UnkillableAllVariants]
        Stalchild = 0xED, // En_Skb

        EmptyEE = 0xEE,

        [ActorizerEnabled]
        [ActorInitVarOffset(0x28F0)]
        [FileID(224)]
        [ObjectListIndex(0x143)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 5)]
        //params: 0x0FE0 is switch flags (?) 0xF000 is sizetype, 0x001F is dialogue to use (params + 0x20D3 to get textId)
        // first row: all moon mask hints, which are free but since they are mask hints they are often worthless (0x1000 someetimes added)
        // second row: overworld variants: center, south north west east order
        [GroundVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62,
            0x3BEE, 0x3BE8, 0x3BC9, 0x3BAA, 0x3B80, 0x3B6B, 0x3B51, 0x3BB4, 0x3BCC, 0x3BED, 0x3BF0, 0x3BF3, 0x3BF2, // center
            0x3BF0, 0x3C01, 0x3BD7, // south
            0x3BE3, 0x3BF6, 0x3BF6, 0x3716, 0x3BC2, 0x3722, // north
            0x38CF, 0x3BE4, // west
            0x3425, 0x3946, 0x3967, 0xFF, 0x3986, 0x3995, 0x3955)] // east
        //[GroundVariants(0xD7)]
        [WallVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62,
            0xEE, 0xE8, 0xC9, 0xAA, 0x80, 0x6B, 0x51, 0xB4, 0xCC, 0xED, 0xF0, 0xF3, 0xF2,
            0x3BF0, 0x3C01, 0x3BD7,
            0x3BE3, 0x3BF6, 0x3BF6, 0x3716, 0x3BC2, 0x3722,
            0xCF, 0xE4,
            0x25, 0x46, 0x67, 0xFF, 0x86, 0x95, 0x55)]
        [WaterBottomVariants(0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62,
            0xEE, 0xE8, 0xC9, 0xAA, 0x80, 0x6B, 0x51, 0xB4, 0xCC, 0xED, 0xF0, 0xF3, 0xF2,
            0x3BF0, 0x3C01, 0x3BD7,
            0x3BE3, 0x3BF6, 0x3BF6, 0x3716, 0x3BC2, 0x3722,
            0xCF, 0xE4,
            0x25, 0x46, 0x67, 0xFF, 0x86, 0x95, 0x55)]
        // might need to come up with a version of this where we just say "each version can show up only once" without being explicit
        [VariantsWithRoomMax(max: 1, 0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62,
            0xEE, 0xE8, 0xC9, 0xAA, 0x80, 0x6B, 0x51, 0xB4, 0xCC, 0xED, 0xF0, 0xF3, 0xF2,
            0xF0, 0x01, 0xD7,
            0xE3, 0xF6, 0xF6, 0x16, 0xC2, 0x22,
            0xCF, 0xE4,
            0x25, 0x46, 0x67, 0xFF, 0x86, 0x95, 0x55,
            0x46, 0x67, 0x88, 0xA9, 0xCA, 0x4B, 0x6C, 0x8D, 0xAE, 0xCF, 0x50, 0x71, 0x92, 0xB3, 0xD4, 0x83, 0xA4, 0xC5, 0x41, 0x62,
            0x3BEE, 0x3BE8, 0x3BC9, 0x3BAA, 0x3B80, 0x3B6B, 0x3B51, 0x3BB4, 0x3BCC, 0x3BED, 0x3BF0, 0x3BF3, 0x3BF2, // center
            0x3BF0, 0x3C01, 0x3BD7, // south
            0x3BE3, 0x3BF6, 0x3BF6, 0x3716, 0x3BC2, 0x3722, // north
            0x38CF, 0x3BE4, // west
            0x3425, 0x3946, 0x3967, 0xFF, 0x3986, 0x3995, 0x3955)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1, variant: 2, 7, 9)]
        [AlignedCompanionActor(GrassRockCluster, CompanionAlignment.Above, ourVariant: -1, variant: 0x0402)] // rock circle like oot
        [ForbidFromScene(Scene.TerminaField, Scene.RoadToSouthernSwamp, Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.SwampSpiderHouse,
            Scene.MilkRoad, Scene.RomaniRanch, Scene.CuccoShack, Scene.DoggyRacetrack,
            Scene.PathToMountainVillage, Scene.ZoraCape, Scene.GreatBayCoast, Scene.MountainVillageSpring, // Scene.MountainVillage,
            Scene.IkanaCanyon, Scene.RoadToIkana, Scene.LinkTrial, Scene.DekuTrial, Scene.GoronTrial, Scene.ZoraTrial)] // don't replace the originals as we might need for hints
        //[ForbidFromScene(Scene.LinkTrial)] // supposidly, you can play storms on the gossip stone to open the door instead of bombchu
        //[EnemizerScenesPlacementBlock(Scene.ClockTowerInterior)] // crash (reason unk)
        GossipStone = 0xEF, // En_Gs

        //[ActorizerEnabled] // best used as a companion instead of being its own actor
        [FileID(225)]
        [ObjectListIndex(0x1)]
        // params: if top is 01, bottom byte is sequence ID with shop music
        //[GroundVariants(0x144)] // 144 is directional shop music with filter
        [GroundVariants(0x90)] // 90 is carpenters hammering away in SCT
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        VariousWorldSounds2 = 0xF0, // Obj_Sound

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1520)]
        [FileID(226)]
        [ObjectListIndex(0x6)]
        // special big one is type 1 but that is never read at init, only accessed on respawn function, so only used internally
        // need mmra to access biggo-crow
        [FlyingVariants(0)]
        [RespawningVariants(0,1)]
        [DifficultVariants(2)]
        [FlyingToGroundHeightAdjustment(150)]
        [VariantsWithRoomMax(max: 7, variant: 0)]
        Guay = 0xF1, // En_Crow

        EmptyF2 = 0xF2,

        [ActorizerEnabled]
        [FileID(227)]
        [ObjectListIndex(0x146)]
        // bugged: milksanity breaks these actors params and sometimes their types
        //[CheckRestricted(Scene.BeneathTheWell, variant: 0x0, Item.ItemWellCowMilk)]
        //[CheckRestricted(Scene.RanchBuildings, variant: 0x2, Item.ItemRanchBarnMainCowMilk, Item.ItemRanchBarnOtherCowMilk1, Item.ItemRanchBarnOtherCowMilk2)]
        //[CheckRestricted(Scene.RomaniRanch, variant: 0x2, Item.ItemRanchBarnMainCowMilk, Item.ItemRanchBarnOtherCowMilk1, Item.ItemRanchBarnOtherCowMilk2)]
        //[CheckRestricted(Scene.Grottos, variant: 0x0, Item.ItemCoastGrottoCowMilk1, Item.ItemCoastGrottoCowMilk2, Item.ItemTerminaGrottoCowMilk1, Item.ItemTerminaGrottoCowMilk2)]
        [GroundVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [WallVariants(0, 2)]  // 2 is from romani ranch, 0 is cow grotto, well is also 0
        [WaterBottomVariants(0)] // silly
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [ForbidFromScene(Scene.RanchBuildings, Scene.RomaniRanch, Scene.Grottos, Scene.BeneathTheWell)]
        //[ForbidFromScene(Scene.Grottos)]
        //[EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.DekuShrine)] // blocking the way
        Cow = 0xF3, // En_Cow

        EmptyF4 = 0xF4,
        EmptyF5 = 0xF5,

        [FileID(228)]
        [ObjectListIndex(0x1)]
        ScarecrowSongOcarinaEffects = 0xF6, // Oceff_Wipe4

        [ObjectListIndex(0x0)]
        EmptyF7 = 0xF7, // EmptyF7

        // unused walking zora
        [ActorizerEnabled]
        [FileID(229)]
        [ObjectListIndex(0xD0)]
        // hmm, params are 0x7E00 >> 9 and thats it. path?
        // looks like -1 (7E) works as a path disable for this actor too
        [GroundVariants(0x7E00)]
        [WaterBottomVariants(0x7E00)]
        [PathingVariants(0x0)]
        [PathingTypeVarsPlacement(mask: 0x7F00, shift: 9)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        MuteZora = 0xF8, // En_Zo

        // swamp spiderhouse soil 
        [FileID(231)]
        [ObjectListIndex(0x1)]
        //[TreasureFlagsPlacement(mask: 0xFF, shift: 2)] // 0x3FC
        Obj_Makekinsuta = 0xF9, // Obj_Makekinsuta

        //[ActorizerEnabled] // she kicks you out like guards but without caring about direction/proximity
        [FileID(232)]
        [ObjectListIndex(0x130)]
        [GroundVariants(0xCB1)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.PiratesFortressRooms)]
        Aviel = 0xFA, // En_Ge3, the Pirate Leader

        EmptyFB = 0xFB,

        // todo randomize just so palace has more actors
        [ActorizerEnabled]
        [FileID(233)]
        [ObjectListIndex(0x2)]
        [CheckRestricted(Scene.RoadToIkana, variant:ActorConst.ANY_VARIANT, Item.ChestToIkanaGrotto)]
        // parameters unknown
        [WaterBottomVariants(0)]
        [GroundVariants(0xFF)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [BlockingVariantsAll]
        [PlacementWeight(65)] // bit boring
        BronzeBoulder = 0xFC, // Obj_Hamishi

        // glitchy early version of skullkid
        [FileID(234)]
        [ObjectListIndex(0x192)]
        // cutscene actor of some sort, if it doesnt crash it loads a tpose skullkid with missing model parts
        Unused_En_Zl4 = 0xFD, // En_Zl4

        [ActorizerEnabled] // we dont want as an actual actor, we want as a companion
        // why is the letter of all things in gameplay_keep? maybe its the same texture of LTK?
        [FileID(235)]
        [ObjectListIndex(0x1CB)] // gameplay_keep obj 1, but I dont want it everywhere, I just want with mailbox
        //[GroundVariants(0)]
        [WaterBottomVariants(0)]
        [VariantsWithRoomMax(max: 5, variant: 0)]
        [UnkillableAllVariants]
        LetterToPostman = 0xFE,

        EmptyFF = 0xFF,

        [FileID(236)]
        [ObjectListIndex(0x1)]
        Door_Spiral = 0x100, // Door_Spiral

        Empty101 = 0x101,

        // puzzle block used in sakon's hideout I think
        [ActorizerEnabled]
        [FileID(237)]
        [ObjectListIndex(0x3)] // double object, the other one is object_secom (sakons hideout)
        // params: 1000 uses alt object instead of dangeon, 0x700 determins minor color, 0x7F is switch flag
        // rotz is used as a unknown param
        [GroundVariants(0x000, 0x100, 0x200, 0x300, 0x400, 0x500, 0x600, 0x700)]
        [WaterBottomVariants(0x000, 0x100, 0x200, 0x300, 0x400, 0x500, 0x600, 0x700)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        //[OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max: 1, variant: 0x000, 0x100, 0x200, 0x300, 0x400, 0x500, 0x600, 0x700)]
        [BlockingVariantsAll]
        [UnkillableAllVariants]
        [PlacementWeight(50)] // boring: its just a block
        PuzzleBlock = 0x102, // Obj_Pzlblock

        //[ActorizerEnabled] // doesnt spawn without 2 node path, if you remove the code to allow for more:crash
        [FileID(238)]
        [ObjectListIndex(0x64)]
        [PathingVariants(0x405, 0x406, 0x407, 0x408)]
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [UnkillableAllVariants]
        ThornTrap = 0x103, // Obj_Toge "Thorn"

        Empty104 = 0x104,

        [EnemizerEnabled]
        [FileID(239)]
        [ObjectListIndex(0x30)]
        [DynaAttributes(20, 13)]
        // 0x7F is switch flag (what is being switched?)
        // oh no z rotation is a parameter.... and there appear to be at least two based on xz rotation
        // 0x7E parameter is switch flag... for what I have no idea, but it seems we cannot set it without triggering a sfx at least
        [GroundVariants(0x7E, 0x7F)]
        [WaterBottomVariants(0x77)]
        [VariantsWithRoomMax(max: 10, variant: 0x7E)] // 11 overloaded gorman race track
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard)] // too much dyna
        ArmosStatue = 0x105, // Obj_Armos

        [ActorizerEnabled]
        [FileID(240)]
        [ObjectListIndex(0x14F)]
        [GroundVariants(0x0)]
        [WaterBottomVariants(0x0)]
        [VariantsWithRoomMax(max: 5, variant: 0)] // culling distance is too long
        //[EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Woodfall)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        Bumper = 0x106, // Obj_Boyo

        Empty107 = 0x107,
        Empty108 = 0x108,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2A7C)]
        [FileID(241)]
        [ObjectListIndex(0x14E)]
        [FlyingVariants(0, 2, 3)]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 2, 0, 2, 3)]
        [FlyingToGroundHeightAdjustment(150)]
        DragonFly = 0x109, // En_Grasshopper

        Empty10A = 0x10A,

        // single unit of grass
        [FileID(242)]
        [ObjectListIndex(0x2)]
        Obj_Grass = 0x10B, // Obj_Grass

        // the version of grass you hold over your head, its separate for some reason
        [FileID(243)]
        [ObjectListIndex(0x2)]
        Obj_Grass_Carry = 0x10C, // Obj_Grass_Carry

        // this is random grass patch, instead of center surrounded with circle
        [ActorizerEnabled]
        [FileID(244)]
        [ObjectListIndex(0x2)]
        // params: 0x1F00 is drop table index, and 0x0001 is shape, 0 is octagon, 1 is loose shuffled
        // all the regular drop tables that can be attached to grass
        [GroundVariants(0x0, 0x1, 0x100, 0x101, 0x600, 0x601, 0x800, 0x801, 0xA00, 0xA01, 0xB00, 0xB01, 0xC00, 0xC01,
            0x01, 0x21, 0x31, 0x41, 0x11,  // just regular drop table, multiple for higher chance
            0x400, 0x401, // ikana rocks, seems reasonable
            0xF00, 0xF01, // tektite, weirdly this is the nost variable of all the drop tables
            0x901, // chance of lots of money, as this is the drop table for money enemies
            0x1F01, // I put this in peahat grotto
            0x300, 0x301)] // this drop table is unused according to mzxrules, but looks balanced
        [UnkillableAllVariants]
        [AlignedCompanionActor(Shot_Sun, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x41)] // fairies love grass
        [AlignedCompanionActor(Fairy, CompanionAlignment.OnTop, ourVariant: -1, variant: 2, 7, 9)] // fairies love grass
        [AlignedCompanionActor(Butterfly, CompanionAlignment.OnTop, ourVariant: -1, variant: 0, 1, 2)] // butterflies love grass
        NaturalPatchOfGrass = 0x10D, // Obj_Grass_Unit

        Empty10E = 0x10E, // Empty10E
        Empty10F = 0x10F, // Empty10F

        [FileID(245)]
        [ObjectListIndex(0x153)]
        Bg_Fire_Wall = 0x110, // Bg_Fire_Wall

        // unused actor, now used by a new injected actor
        [FileID(246)]
        [DynaAttributes(12, 8)]
        [EnemizerScenesPlacementBlock(Scene.PinnacleRock//, // super annoying warping the player all the way back
            /*Scene.StoneTower, Scene.SouthernSwamp, Scene.SouthernSwampClear */)] // dyna crash possible
        [PlacementWeight(50)]
        Mimi = 0x111, // En_Bu

        //[EnemizerEnabled] //crash
        //[ActorInitVarOffset(0x3688)]
        [FileID(247)]
        [ObjectListIndex(0x23B)]
        [GroundVariants(0x2243)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        GaroSpawner = 0x112, // En_Encount3

        [FileID(248)]
        [ObjectListIndex(0x155)]
        [GroundVariants(0x2243)]
        Garo = 0x113, // En_Jso

        // not that interesting, cutscene calapsing bridge
        [ActorizerEnabled]
        [FileID(249)]
        [ObjectListIndex(0xED)]
        [DynaAttributes(12,8)]
        [BlockingVariantsAll]
        [FlyingVariants(0)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(//Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.StoneTower, // dyna crash
            Scene.TradingPost)]  // might block door?
        [PlacementWeight(50)] // waaay too common
        UnusedFallingBridge = 0x114, // Obj_Chikuwa

        // TODO would not spawn because I mistyped the ID try again
        //[EnemizerEnabled] // wont spawn, just spawns green tatl points but no actual knight
        [ObjectListIndex(0x156)]
        [FileID(250)]
        // params 0 vanilla
        // init checks if 64, C8, CA, 23
        [GroundVariants(0x23)]
        [UnkillableAllVariants] // assumption: need mirror shield
        SkeleKnight = 0x115, // En_Knight

        [ActorizerEnabled] // used in the moon
        [FileID(251)]
        [ObjectListIndex(3)] // 3 if you want the visible one, from Goron Trial
        [DynaAttributes(28,18)]
        // params: 0x8000 is invisbile (deku playground exit)
        // 0x03C0 is unknown, it must be set to max for it to work, non-x just shows a tatl spot and does nothing else
        // 0x3F is scene exit list index
        [GroundVariants(0x3C0)] // zero index, always safe
        [WaterBottomVariants(0x3C0)]
        [ForbidFromScene(Scene.GoronTrial, // needed
            Scene.DekuPlayground // door to leave
        )]
        [EnemizerScenesPlacementBlock(Scene.IkanaCastle)] // wrongly warps you to the boss room but its not loaded
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [PlacementWeight(90)]
        WarpToTrialEntrance = 0x116, // En_Warp_tag

        // dog race owner
        [FileID(252)]
        [ObjectListIndex(0xD7)]
        MammauYan = 0x117, // En_Aob_01

        // these are empty actors, they do nothing
        [FileID(253)]
        [ObjectListIndex(0x1)]
        Unused_En_Boj_01 = 0x118, // En_Boj_01
        [FileID(254)]
        [ObjectListIndex(0x1)]
        Unused_En_Boj_02 = 0x119, // En_Boj_02
        [FileID(255)]
        [ObjectListIndex(0x1)]
        Unused_En_Boj_03 = 0x11A, // En_Boj_03

        [FileID(256)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        StalchildSpawner = 0x11B, // En_Encount4

        // this is the version that spawns in east clock town to give you the book if you knew the password
        [ActorizerEnabled]
        [FileID(257)]
        [ObjectListIndex(0x110)]
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2,
            Item.NotebookMeetBombers, Item.NotebookLearnBombersCode
            )] // this is duplicated in multiple places
        [GroundVariants(0x510, 0x10)]
        [VariantsWithRoomMax(max: 0, variant: 0x510, 0x10)] // does not spawn except in cutscenes, dont place it will be empty
        [UnkillableAllVariants]
        JimboTheStalker = 0x11C, // En_Bom_Bowl_Man

        [ActorizerEnabled]
        [FileID(258)]
        [ObjectListIndex(0x1AC)]
        [CheckRestricted(Scene.SwampShootingGallery, ActorConst.ANY_VARIANT, //0x000F,
            Item.UpgradeBiggestQuiver, Item.HeartPieceSwampArchery)]
        [CheckRestricted(Scene.TownShootingGallery, ActorConst.ANY_VARIANT, //0xFF01,
            Item.UpgradeBigQuiver, Item.HeartPieceTownArchery)] // town
        [GroundVariants(0x000F, 0xFF01,
            0xFF0F // secret unused version in the milkbar
            )]
        // 0xFF0F is found in milkbar??
        [VariantsWithRoomMax(max: 3, variant: 0x000F, 0xFF01)]
        [VariantsWithRoomMax(max: 0, variant: 0xFF0F)] // probably busted
        [UnkillableAllVariants]
        [PlacementWeight(50)]
        ArcheryMiniGameMan = 0x11D, // En_Syateki_Man

        Empty11E = 0x11E,

        [ActorizerEnabled]
        [FileID(259)]
        [ObjectListIndex(0x157)]
        [DynaAttributes(13,10)]
        [GroundVariants(0x700, 0xD00, 0xA00, // greatbaytemple
                        0x003F, // goron trial
                        0x1000, 0x0B00, 0x0C00, 0x600, 0x002B)] // snowheattemple
        [CeilingVariants(0xFF01, 0xFF00, 0xFF02 )]
        //[EnemizerScenesPlacementBlock(Scene.GormanTrack, // dyna crash on trees
        //    Scene.IkanaGraveyard, Scene.SouthernSwamp, Scene.StoneTower)] // assumed same as above
        [VariantsWithRoomMax(max: 10, variant: 0x1000, 0x0B00, 0x0C00, 0x600, 0x002B, 0x003F, 0x700, 0xD00, 0xA00)] // limit because of dyna
        [VariantsWithRoomMax(max: 10, variant: 0xFF01, 0xFF00, 0xFF02)] // still dyna
        [UnkillableAllVariants]
        // we need to have weights per variant, we want to downweight mites but not tites
        IceCavernStelagtite = 0x11F, // Bg_Icicle // also stalagmite

        // what
        [FileID(260)]
        [ObjectListIndex(0x6)]
        En_Syateki_Crow = 0x120, // En_Syateki_Crow

        // empty
        [FileID(261)]
        //[ObjectListIndex(0x1)]
        [ObjectListIndex(0x184)] // fake object because this test actor is being a bitch
        En_Boj_04 = 0x121, // En_Boj_04

        // broken actor, needs two objects (animation is in another object) such a pain
        // we have replaced this actor with an injected replacement that doesnt require EnHy
        //[ActorizerEnabled]
        [FileID(262)]
        [ObjectListIndex(0xDA)]
        // needs anime object and cne object, pain
        //[GroundVariants(0x7E00)]
        //[PathingVariants(0x0)] // dont trust this anymore, wait for actor fix
        // 0 is vanessa, 1 is orange
        [CompanionActor(Flame, ourVariant: 0, 0x7FE)] // blue flames
        // todo find slightly off color flames
        [PathingTypeVarsPlacement(mask: 0x7E00, shift: 9)] // still valid with injected actor
        [OnlyOneActorPerRoom] // not sure if I need this, but with companions..
        [UnkillableAllVariants]
        BetaVampireGirl = 0x122, // En_Cne_01

        // beta bomb shop grandma
        [ActorizerEnabled]
        [FileID(263)]
        // enhy nonsense, it has to load its inner-actor thing first, so its set to gameplay keep
        [ObjectListIndex(0xDF)] // 1
        // params: 0x007E is path range, 0x7E is max
        [PathingVariants(0x0)]
        [PathingTypeVarsPlacement(mask: 0x7E00, shift: 9)]
        [GroundVariants(0x7E00)] // todo check if 0x7F is a thing
        // Pathing Variants todo
        [VariantsWithRoomMax(max: 7, variant: 0x7E00)] // lag, probably from the realtime shadow generation
        [UnkillableAllVariants]
        [PlacementWeight(75)]
        BabaIsUnused = 0x123, // En_Bba_01

        // wont spawn if you place him outside of his observatory, needs modification
        // the astral observatory viewer
        //[ActorizerEnabled] // TODO randomize this only if casual logic too lazy to do that tonight tho
        [FileID(264)]
        [ObjectListIndex(0xDE)]
        [CheckRestricted(Item.TradeItemMoonTear, Item.HeartPieceTerminaBusinessScrub, Item.CollectableTerminaFieldTelescopeGuay1)]
        [GroundVariants(0xFFFF)]
        [VariantsWithRoomMax(max: 0, variant: 0xFFFF)]
        [UnkillableAllVariants]
        Shikashi = 0x124, // En_Bji_01

        [ActorizerEnabled]
        [FileID(265)]
        [ObjectListIndex(0x158)]
        // 0xFF == 0 or 1 is different case
        // 0x7F00 >> 8 is flags
        [WallVariants(0x7E01, 0x7D01, 0x7C01)]
        [CeilingVariants(0x7E01, 0x7D01, 0x7C01)]
        //[GroundVariants(0x7E00, 0x7D00, 0x7C00)] // work but too low mostly hidden by ground
        [VariantsWithRoomMax(max: 1, 0x7E01, 0x7D01, 0x7C01)]
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        SpiderWeb = 0x125, // Bg_Spdweb

        Empty126 = 0x126,
        Empty127 = 0x127,

        [FileID(266)]
        [ObjectListIndex(0x1)]
        GoronraceMinigameController = 0x128, // En_Mt_tag

        //[EnemizerEnabled]
        [FileID(267)]
        [ObjectListIndex(0x15A)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 1, variant: 0)]
        [ForbidFromScene(Scene.OdolwasLair)]
        Odolwa = 0x129, // Boss_01

        [FileID(268)]
        [ObjectListIndex(0x15B)]
        Twinmold = 0x12A, // Boss_02

        [FileID(269)]
        [ObjectListIndex(0x15C)]
        Gyorg = 0x12B, // Boss_03

        // can we randomize him?
        // turns out he spawns on the floor in gbt
        [EnemizerEnabled]
        [FileID(270)]
        [ObjectListIndex(0x15D)]
        [GroundVariants(0)] // his placement is on the ground, cutscene?
        [VariantsWithRoomMax(max:0, variant:0)] // spawning behavior is weird and can spawn out of bounds
        //[OnlyOneActorPerRoom]
        [DifficultAllVariants]
        Wart = 0x12C, // Boss_04

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3760)]
        [FileID(271)]
        [ObjectListIndex(0x15E)]
        [DynaAttributes(10, 8, variant:0x0000, 0x0001)]  // only the variants on top, maybe this really needs a variant..
        [CheckRestricted(Scene.GreatBayTemple, variant: ActorConst.ANY_VARIANT,
            Item.CollectibleStrayFairyGreatBay10)] // biobaba room
        // 0x1 is the one that hangs from the ceiling in GBT
        // TODO if I get wall sideways working with dexihand, do it for baba too
        [WaterTopVariants( 0x0002, // without a head to bite you
            0x0000)] // regular
        // is there a watter bottom version by default?
        [WaterBottomVariants(4)]
        [CeilingVariants(0x0001)] // they're in the ceiling now
        [RespawningVariants(0x0001)] // doesn't respawn, but stray faries attach themselves to the lilypad
        //[ForbidFromScene(Scene.GreatBayTemple)] // need their lilipads to reach compass chest and fairy chest
        [UnkillableVariants(0)]
        BioDekuBaba = 0x12D, // Boss_05 // biobaba

        //[EnemizerEnabled]
        [FileID(272)]
        [ObjectListIndex(0x156)]
        //[GroundVariants(0)]
        [UnkillableAllVariants]
        KingIkanaController = 0x12E, //Boss_06

        [FileID(273)]
        [ObjectListIndex(0x160)]
        Majora = 0x12F, // Boss_07

        //[ActorizerEnabled] // even cutscene version doesn't spawn, might be a rando thing, modification untested assumed difficult
        [FileID(274)]
        [ObjectListIndex(0x8)]
        //[CheckRestricted(Scene.Great)]
        // doesn't have real BG right?
        //[GroundVariants(0xFE0F)] // cutscene version that is supposed to be teaching link defense
        [GroundVariants(0x201, 0x402, 0x602, 804)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        [UnkillableAllVariants]
        GreatFairy = 0x130, // Bg_Dy_Yoseizo

        Empty131 = 0x131,

        // empty actor, does nothing
        [FileID(275)]
        [ObjectListIndex(0x1)]
        En_Boj_05 = 0x132, // En_Boj_05

        Empty133 = 0x133,
        Empty134 = 0x134,

        //[ActorizerEnabled] // wont spawn because the required item objects are likely missing
        [FileID(276)]
        [ObjectListIndex(0xD0)]
        [CheckRestricted(Scene.BombShop, variant: ActorConst.ANY_VARIANT, // 02,
            Item.ShopItemBombsBomb10, Item.ShopItemBombsBombchu10, Item.ItemBombBag, Item.UpgradeBigBombBag)]
        [CheckRestricted(Scene.ZoraHallRooms, variant: ActorConst.ANY_VARIANT, // 0x3E0,
            Item.ShopItemZoraArrow10, Item.ShopItemZoraRedPotion, Item.ShopItemZoraShield)]
        [CheckRestricted(Scene.GoronShop, variant: ActorConst.ANY_VARIANT, // 0x3E1,
            Item.ShopItemGoronArrow10, Item.ShopItemGoronBomb10, Item.ShopItemGoronRedPotion)]
        // 2 is bombshop, 3E0 is zora shopf
        [GroundVariants(0x3E0, // zora shop
            0x3E1, // goron shop
            0x2)] // bomb shop
        [UnkillableAllVariants]
        ShopSeller = 0x135, // En_Sob1

        Empty136 = 0x136,
        Empty137 = 0x137,

        [ActorizerEnabled]
        [FileID(277)]
        [ObjectListIndex(0xA1)]
        [ActorInstanceSize(0xB78)]
        //[CheckRestricted(Scene.MountainVillage, variant:0x7F94, check:Item.MaskDonGero)] // share object with the sirloin goron
        // 8 is smithy goron; blocked because he is too big
        // 7F85: standing outside of shop (complaining about noise)
        // racetrack gorons
        // 0x7FA1: jumping goron, 0x7FC1 stretching goron pair
        // 0x7F81: single leg stretch goron, 0x7F81 single amr stretch goron
        //[GroundVariants(0x8, 0x7FE2)]
        [GroundVariants(//0x8, // smithy goron
            0x7FE2, 0x7F85, 0x7F86, 0x7F87,
            0x7FA1, 0x7FC1, 0x7F81, 0x7FF2, // racetrack
            0x7F82, 0x7F92, // praising darmani in cutscene
            0x7F84, 0x7F94)] // outside of darmani's grave
        [VariantsWithRoomMax(max: 1,
            0x7FE2, 0x7F85, 0x7F86, 0x7F87,
            0x7F82, 0x7F92)]
        [VariantsWithRoomMax(max: 0, variant: 0x8, // too big
            0x7F84, 0x7F94, // the two outside of darmani race need to be on the same thing
            0x7F82, 0x7F92)] // crash? reason unknown
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.GoronVillage, Scene.GoronVillageSpring)] // dont randomize smithy
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1,
            variant: 0x3F5F)]
        GoGoron = 0x138, // En_Go

        Empty139 = 0x139,

        [EnemizerEnabled]
        [FileID(278)]
        [ObjectListIndex(0x161)]
        [DynaAttributes(12,8)]
        [WaterTopVariants(0xF81, 0xF82, 0xF83, 0xF84, 0xF85, 0xF86, 0xF87)]
        [VariantsWithRoomMax(max:2, variant: 0xF81, 0xF82, 0xF83, 0xF84, 0xF85, 0xF86, 0xF87)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [ForbidFromScene(Scene.WoodfallTemple)]
        Raft = 0x13A, // En_Raf carniverous raft, woodfall temple

        [FileID(279)]
        [ObjectListIndex(0x162)]
        UnusedStoneTowerSmoke = 0x13B, // Obj_Funen

        [FileID(280)]
        [ObjectListIndex(0x163)]
        [SwitchFlagsPlacementXRot]
        Obj_Raillift = 0x13C, // Obj_Raillift

        [FileID(281)]
        [ObjectListIndex(0x164)]
        // closed is smaller?? (96,48)
        [DynaAttributes(136,72)] // big yikes (opened)
        WoodfallTempleWoodenFlower = 0x13D, // Bg_Numa_Hana

        [ActorizerEnabled] // big object, collector flag, boring actor
        [FileID(282)]
        [ObjectListIndex(0x165)]
        [CheckRestricted(Scene.CuccoShack, variant: ActorConst.ANY_VARIANT, Item.CollectableCuccoShackPottedPlant1)]
        // 0xXX is the item to drop, 0x7X00 is collecable flag
        // thankfully if collectable flag is 00 it gets ignored and you can re-collect over and over again
        // A is hearts or green rup if full health
        // following that: blue rup, red, three hearts, small magic, flexible,
        [GroundVariants(0x0A, 0x02, 0x04, 0x0B, 0x0E, 0x10, 0x13, 0x15, 0x16, 0x1E,
            0x010A, // smitty
            0x030A, 0x020A, // road to swamp
            0x7F3F, 0x0B02, // cucco shack
            0x0C0A, 0x0D0A // spring
        )]
        [VariantsWithRoomMax(max: 2, 0x0A, 0x02, 0x04, 0x0B, 0x0E, 0x10, 0x13, 0x15, 0x16, 0x1E)]
        [VariantsWithRoomMax(max: 0, 0x010A, 0x020A, 0x030A, 0x0B02, 0x0C0A, 0x0D0A, 0x7F3F)] // wont respawn
        [UnkillableAllVariants]
        [PlacementWeight(50)]
        PottedPlant = 0x13E, // Obj_Flowerpot

        // probably follows paths like the spike traps
        [FileID(283)]
        [ObjectListIndex(0x166)]
        Obj_Spinyroll = 0x13F, // Obj_Spinyroll

        // "boss mask cutscene objects"
        [FileID(284)]
        [ObjectListIndex(0x1CC)]
        Dm_Hina = 0x140, // Dm_Hina

        [FileID(285)]
        [ObjectListIndex(0x141)]
        ShootingGalleryDekuWolfos = 0x141, // En_Syateki_Wf

        [ActorizerEnabled]
        [FileID(286)]
        [ObjectListIndex(0x3)]
        [GroundVariants(0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        PushableBlockOnIce = 0x142, // Obj_Skateblock

        // assuming this is the block ice arrows spawns
        [FileID(288)]
        [ObjectListIndex(0x167)]
        [DynaAttributes(12, 8)]
        Obj_Iceblock = 0x143, // Obj_Iceblock

        [FileID(289)]
        [ObjectListIndex(0x1A6)]
        En_Bigpamet = 0x144, // En_Bigpamet

        [FileID(291)]
        [ObjectListIndex(0x40)]
        ShootingGalleryDekuScrub = 0x145, // En_Syateki_Dekunuts

        [FileID(292)]
        [ObjectListIndex(0x1)]
        // switch flag checks a different flag (rot.y) then it sets...? why
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        Elf_Msg3 = 0x146, // Elf_Msg3

        [ActorizerEnabled] // cannot talk to them BUT YOU CAN KILL THEM :D
        [FileID(293)]
        [ObjectListIndex(0xBC)]
        [GroundVariants(0, 1, 2, 3, 4, 5)] // 6 colors
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [UnkillableAllVariants] // animated kill but not enemy category
        ImposterFrog = 0x147, // En_Fg  // unused beta frog

        // not even sure if its used, empty draw function, almost no code
        [FileID(294)]
        [ObjectListIndex(0x169)]
        Dm_Ravine = 0x148, // Dm_Ravine

        // Dm_Sa was saria in OOT but in MM its an unused skullkid prototype
        [FileID(295)]
        [ObjectListIndex(0x192)]
        BrokenSkullkid = 0x149, // Dm_Sa

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2D30)]
        [ActorInstanceSize(0x208)]
        [FileID(296)]
        [ObjectListIndex(0x16A)]
        // 0xXX00 is respawn time, 0xFF gets turned into 0, which is the shortest value
        // 0xX is type, 0 is empty water, 1 is magic, 2 is arrows, 3 is hearts,
        // if you use 0x5 it shows green chuchu with blue tatl hint, but the texture is corrupt and flashes
        [GroundVariants(0x0C01, 0x1402, 0xFF03, 0xFF01, 0xFF00, 0x0A01, 0x0202, 0x801, 0xFF02, 0x0103, 0x0203)]
        //[GroundVariants(0x0005)]
        [ForbidFromScene(Scene.GreatBayTemple, Scene.InvertedStoneTowerTemple)] // necessary to climb
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.SouthernSwampClear)] // crash transitioning witch shop room
        // all variants respawn until proven otherwise
        //[RespawningVariants(0xFF03,0xFF01,0xFF00, 0xFF03, 0x0A01,   0x0C01,0x1402,0x0202,0x801,0xFF02)]
        [RespawningAllVariants]
        ChuChu = 0x14A, // En_Slime

        [EnemizerEnabled]
        [ActorInitVarOffset(0x16C4)]
        [FileID(297)]
        [ObjectListIndex(0x16B)]
        [WaterVariants(0x0F00, 0x0300)]
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.ZoraCape, Scene.GreatBayCoast, Scene.IkanaCanyon)] // massive lag
        Desbreko = 0x14B, // En_Pr (Pirana?)

        [FileID(298)]
        [ObjectListIndex(0x16D)]
        UnusedClockTowerSpotlight = 0x14C, // Obj_Toudai

        [FileID(299)]
        [ObjectListIndex(0x16D)]
        ClockTownSmokingChimney = 0x14D, // Obj_Entotu

        [ActorizerEnabled]
        [FileID(300)]
        [ObjectListIndex(0x16C)]
        [DynaAttributes(33, 20)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 8, variant: 0)] // too many is boring
        //[ForbidFromScene(Scene.EastClockTown)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [PlacementWeight(75)]
        StockpotBell = 0x14E, // Obj_Bell

        [ActorizerEnabled] // need to replace if you replace the shooting gallery man
        [FileID(301)]
        [ObjectListIndex(0x5)]
        [CheckRestricted(Item.UpgradeBigQuiver, Item.HeartPieceTownArchery)]
        [WaterVariants(0xFFF0, 0xFFF1, 0xFFF2, 0xFFF3, 0xFFF4, 0xFFF5, 0xFFF6, 0xFFF7, 0xFFF8)]
        [GroundVariants(0xFFF0, 0xFFF1, 0xFFF2, 0xFFF3, 0xFFF4, 0xFFF5, 0xFFF6, 0xFFF7, 0xFFF8)]
        // likely dont work without the shooting man anyway
        [VariantsWithRoomMax(max: 0, variant: 0xFFF0, 0xFFF1, 0xFFF2, 0xFFF3, 0xFFF4, 0xFFF5, 0xFFF6, 0xFFF7, 0xFFF8, // vanilla params
            0x00FF // non-vanilla params I've seeen used before, not sure how, but we cannot place
        )]
        [UnkillableAllVariants]
        ShootingGalleryOctorok = 0x14F, // En_Syateki_Okuta

        Empty150 = 0x150,

        [FileID(302)]
        [ObjectListIndex(0x16D)]
        Obj_Shutter = 0x151, // Obj_Shutter

        // made a custom replacement so she has some interaction instead of just using vanilla as this is cutscene only
        //[ActorizerEnabled]
        [FileID(303)]
        [ObjectListIndex(0x14B)]
        [WaterBottomVariants(0)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(Scene.MountainVillageSpring)] // her new actor plays flute, this can break frog choir if close enough
        [PlacementWeight(90)]
        CutsceneZelda = 0x0152, // Dm_Zl

        [FileID(305)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0x9)]
        GreatFairyPieceSwarm = 0x153, // En_Elfgrp

        // todo test this one
        [FileID(306)]
        [ObjectListIndex(0x19F)]
        CreditsRotatingMasks = 0x154, // Dm_Tsg

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1250)]
        [FileID(307)]
        [ObjectListIndex(0x171)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 10, variant: 0)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // slowing enemies
        Nejiron = 0x155, // Rolling exploding rock in Ikana

        [FileID(308)]
        [ObjectListIndex(0x166)]
        SpikedRollers = 0x156, // Obj_Vspinyroll

        [FileID(309)]
        [ObjectListIndex(0x16D)]
        RomaniRanchChimneySmoke = 0x157, // Obj_Smork

        // "objects affected by lens"? wonder whats in here
        [FileID(310)]
        [ObjectListIndex(0x1)]
        En_Test2 = 0x158, // En_Test2

        [ActorizerEnabled]
        [FileID(311)]
        [ObjectListIndex(0x1C)]
        [CheckRestricted(Scene.IkanaCanyon, variant: ActorConst.ANY_VARIANT, check: Item.MaskCouple, Item.NotebookMeetKafei,
            Item.NotebookEscapeFromSakonSHideout, Item.NotebookUniteAnjuAndKafei)]
        [CheckRestricted(Scene.EastClockTown, variant: ActorConst.ANY_VARIANT, check: Item.MaskCouple)]
        [CheckRestricted(Scene.SouthClockTown, variant: ActorConst.ANY_VARIANT, // 0x1E3,
            check: Item.MaskCouple, Item.TradeItemPendant, Item.MaskKeaton, Item.TradeItemMamaLetter)]
        [CheckRestricted(Scene.LaundryPool, variant: ActorConst.ANY_VARIANT, check: Item.MaskCouple, Item.TradeItemPendant, Item.MaskKeaton, Item.TradeItemMamaLetter)]
        [CheckRestricted(Scene.CuriosityShop, variant: ActorConst.ANY_VARIANT, Item.MaskCouple, Item.TradeItemPendant, Item.MaskKeaton, Item.TradeItemMamaLetter,
            Item.NotebookMeetKafei, Item.NotebookUniteAnjuAndKafei, Item.NotebookPromiseKafei,
            Item.NotebookMeetCuriosityShopMan, Item.NotebookCuriosityShopManSGift, Item.NotebookPromiseCuriosityShopMan)] // can't meet him without kafei I dont think
        // E2 is hidden in ikana somewhere?? since its path its prob running after final hours or something
        [PathingVariants(0x100, 0x1E2, 0x1E3, 0x1E4, 0x1E0)]
        [PathingTypeVarsPlacement(mask: 0x1F, shift: 0)]
        // assumed all are hardcoded to hell
        [VariantsWithRoomMax(max: 0, variant: 0x100, 0x1E2, 0x1E3, 0x1E4, 0x1E0)] // assumed to be hardcoded
        [UnkillableAllVariants]
        Kafei = 0x159, // En_Test3

        [FileID(312)]
        [ObjectListIndex(0x1)]
        ThreeDayTimer = 0x15A, // En_Test4

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1500)]
        [FileID(313)]
        [ObjectListIndex(0x172)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: ActorConst.ANY_VARIANT, Item.CollectableIkanaGraveyardDay2Bats1)]
        // what is the variant?
        [FlyingVariants(0xFF34, // big cluster
            0xFF02, 0xFF03, 0x0102, 0x0103, // graveyard
            0xFF01)]
        // using irrelevant switch flags to distinquish the fake perching types
        [PerchingVariants(0xFF9F, // bat tree
            0x019F)] // 19F graveyard
        [WallVariants(0xFF9F, // bat tree
            0x029F)] // 
        [DifficultVariants(0xFF34)]
        [VariantsWithRoomMax(max: 1, 0xFF34)] // swarm
        [FlyingToGroundHeightAdjustment(150)]
        //[ForbidFromScene(Scene.IkanaGraveyard)] // need bats for dampe day 2 check
        // switch flags are only for the graveyard, no other version uses it
        // hardcoded to use only in that scene too, so canno't use for anything else without modifying
        //[SwitchFlagsPlacement(mask: 0xFF, shift: 8)]
        BadBat = 0x15B, // En_Bat

        // can hold many different types of graves or stones containing.. nothing bit broke
        // has BG crashed in east clock town before
        [ActorizerEnabled] // this works fine but shows up waay too often
        // there are actaually 3 others, but they are three separate objects, so hard to program
        [FileID(314)]
        [ObjectListIndex(0x173)]
        [DynaAttributes(18,12)] // this is multiple object, this one is triforce
        // spreadsheet thinks 0x206 could be it
        [GroundVariants(0)]
        [WaterBottomVariants(0)]
        [VariantsWithRoomMax(max: 5, variant: 0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [PlacementWeight(80)]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        // might be used for mikau grave, but also beta actors that teach songs...??
        MagicSlab = 0x15C, // En_Sekihi

        // we can't place without a way to ensure the enemy is only placed in places that have multiple spots, but fuck this guy get rid of em
        [EnemizerEnabled]
        [ActorInitVarOffset(0x37D0)]
        [FileID(315)]
        [ObjectListIndex(0x178)]
        [GroundVariants(0x007F, // ikana castle
                        0x017F, // sht miniboss
                        0x027F  // istt
            )]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max:0, variant: 0x007F, 0x017F, 0x027F)] // wont work without their blocks
        Wizrobe = 0x15D, // En_Wiz

        [EnemizerEnabled]
        [FileID(316)]
        [ObjectListIndex(0x178)]
        [DynaAttributes(10,8)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max:0, variant:0)]
        WizrobeSpawnBlock = 0x15E, // En_Wiz_Brock

        [FileID(317)]
        [ObjectListIndex(0x178)]
        En_Wiz_Fire = 0x15F, // En_Wiz_Fire

        [FileID(318)]
        [ObjectListIndex(0x1)]
        EllegyStatueSpawnEffect = 0x160, // Eff_Change

        [FileID(319)]
        [ObjectListIndex(0x26C)]
        GiantsChamberColumn = 0x161, // Dm_Statue

        //[ActorizerEnabled] // this actor was turned into a companion instead, would show up too often because its gameplaykeep
        [FileID(320)]
        [ActorInstanceSize(0x2AC)] // 1AC, raised to reduce chance of getting
        [ObjectListIndex(0x1)]
        //[GroundVariants(0x4560)] // works but no sound?
        // 0x800B is keeta
        // 0x3F5F smaller than the other two,
        // 3F60 woodfall small
        [GroundVariants(0x3F5F)]
        [VariantsWithRoomMax(max: 1, variant: 0x3F5F)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall)] // hiploop only, bad spot for a fire
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        // possible second switch at 0x3F8
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [TreasureFlagsPlacement(mask: 0x1F, shift: 8)] // 0x3FC
        CircleOfFire = 0x162, // Obj_Fireshield // tag: FireRing

        // wont spawn without the switch flag, goes counter to our switch flag code currently
        //[ActorizerEnabled]
        [FileID(321)]
        [ObjectListIndex(0x179)]
        //[WallVariants(0)]
        [SwitchFlagsPlacement(mask: 0xFF, shift: 8)]
        Bg_Ladder = 0x163, // Bg_Ladder

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1830)]
        [FileID(322)]
        [ObjectListIndex(0x17A)]
        // params: 0x1 is white vs black
        // 0x2 and 0x4 are flags, 4 might be respawning
        // 2 flag unknown? neat
        // 0xFF00 is something else if 0 or 255, else
        [CeilingVariants(0xFF05, // sht goron button puzzle lens cieling bubble
            0xFA01, 0xFA00)] // spring tunnel to darmani grave
        [PerchingVariants(0x9605, 0x6405, 0x3205)] // snow falling from tree in pathtomountain
        [FlyingVariants( 0x8C05//, // falling as snow onto the ramp in snowhead
            )]
        // this variety is slow spawn, meaning you have to walk up to it: 0x2800, 0x3200, 0xC200, 0xFA00
        [GroundVariants(0xFF00, 0x6404, 0x7804, // stt (ff00 is also in secret grotto)
            0x7800, 0x2800, 0x3200, // wft
            0xFF01, // sht
            0xC200)] // ocean spiderhouse
        // 9605,3205,6405 all respawn in path to mountain village, 8C05 is snowhead, 6404 and 7804 are stone tower
        [RespawningVariants(0x6404, 0x7804, 0x9605, 0x3205, 0x6405, 0x8C05, 0xFF05//, // actually respawning
            /* 0x2800, 0x3200, 0xC200, 0xFA00 */)] // these four dont respawn, but they are invisbile until you are right on top of them, then they materialize, so hidden
        //[PerchingVariants(0x2808, 0x3208, 0xC208, 0xFA08)] // non vanilla using non existent 0x8 flag to hide from vanilla code
        [VariantsWithRoomMax(max: 2, variant: 0x9605, 0x3205, 0x6405, 0x8C05, 0xFA01, 0xFA00)]
        [VariantsWithRoomMax(max: 1, variant: 0xFF00, 0x6404, 0x7804, 0x7800, 0x2800, 0x3200, 0xFF01, 0xFF05, 0xC200)]
        [CompanionActor(ClayPot, ourVariant: -1, variant: 0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)]
        Bo = 0x164, // En_Mkk, boe, small ball of snow or soot

        [FileID(323)]
        [ObjectListIndex(0x1)]
        Demo_Getitem = 0x165, // Demo_Getitem

        Empty166 = 0x166,

        // unused scene sized cutscene actor
        [FileID(324)]
        [ObjectListIndex(0x189)]
        [UnkillableAllVariants]
        En_Dnb = 0x167, // En_Dnb

        [FileID(325)]
        [ObjectListIndex(0x224)]
        KoumeInKiosk = 0x168, // En_Dnh

        // TODO figure out why the hell its crashing
        //   working theory: the objects being shuffled when moving from one room to another breaks object code
        // code suggests even more params might exist than are used in vanilla
        [ActorizerEnabled]
        [FileID(326)]
        [ObjectListIndex(0x40)] // 1? nah it uses something else
        // 0x2 is smaller scrub that surrounds link in the cutscene
        // params 0x3C nad 0x3 are main params
        //  0x3 is the object it uses
        //  one is the regular leaves pkmn, one is the old hint one from OOT reused for yellow flower, one is the tall one
        // 0x6 is big one in nightmare cutscene that link waves to
        [GroundVariants(0x2, 0x6)]
        [VariantsWithRoomMax(max: 1, variant: 0x6, 0x2)]
        // crash on transition to witches area in swamp and secretary room in mayor's residence
        // Update crashes trying to update the skeleton, null pointer, reason unknown
        // seems to be room transition related, for now ban from any place where rooms change over
        [EnemizerScenesPlacementBlock(Scene.SouthernSwampClear, Scene.SouthernSwamp, Scene.MayorsResidence, Scene.StockPotInn,
            Scene.OceanSpiderHouse, Scene.DekuPalace, Scene.SwampSpiderHouse, Scene.DekuShrine, Scene.IkanaCanyon, Scene.BeneathTheWell,
            Scene.PiratesFortressRooms,
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.OceanSpiderHouse, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        [UnkillableAllVariants]
        [PlacementWeight(80)]
        HallucinationScrub = 0x169, // En_Dnk

        [ActorizerEnabled]
        [FileID(327)]
        [ObjectListIndex(0x18B)]
        [CheckRestricted(Item.MaskScents,
            Item.CollectableDekuShrineDekuButlerSRoomItem1, Item.CollectableDekuShrineDekuButlerSRoomItem2, Item.CollectableDekuShrineDekuButlerSRoomItem3,
            Item.CollectableDekuShrineDekuButlerSRoomItem4, Item.CollectableDekuShrineDekuButlerSRoomItem5, Item.CollectableDekuShrineDekuButlerSRoomItem6,
            Item.CollectableDekuShrineDekuButlerSRoomItem7, Item.CollectableDekuShrineDekuButlerSRoomItem8, Item.CollectableDekuShrineDekuButlerSRoomItem9,
            Item.CollectableDekuShrineDekuButlerSRoomItem10,
            Item.CollectableDekuShrineGreyBoulderRoomPot1,
            Item.CollectableDekuShrineGiantRoomFloor1Item1, Item.CollectableDekuShrineGiantRoomFloor1Item2,
            Item.CollectableDekuShrineGiantRoomFloor1Item3, Item.CollectableDekuShrineGiantRoomFloor1Item4,
            Item.CollectableDekuShrineGiantRoomFloor1Item5, Item.CollectableDekuShrineGiantRoomFloor1Item6,
            Item.CollectableDekuShrineGiantRoomFloor1Item7, Item.CollectableDekuShrineGiantRoomFloor1Item8,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem1, Item.CollectableDekuShrineRoomBeforeFlameWallsItem2,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem3, Item.CollectableDekuShrineRoomBeforeFlameWallsItem4,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem5, Item.CollectableDekuShrineRoomBeforeFlameWallsItem6,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem1, Item.CollectableDekuShrineWaterRoomWithPlatformsItem2,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem3, Item.CollectableDekuShrineWaterRoomWithPlatformsItem4,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem5, Item.CollectableDekuShrineWaterRoomWithPlatformsItem6)]
        [GroundVariants(0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        //[PlacementWeight(60)] // testing
        DekuKing = 0x16A, // En_Dnq

        Empty16B = 0x16B,
        [FileID(328)]
        [ObjectListIndex(0x17E)]
        [DynaAttributes(4,8)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        TerminaFieldSpikedFence = 0x16C, // Bg_Keikoku_Saku

        // too big to go most places, doesn't have texture on backend so thats weird
        [FileID(329)]
        [ObjectListIndex(0x12A)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        BigBoulder = 0x16D, // Obj_Hugebombiwa

        // empty actor, does nothing
        [FileID(330)]
        [ObjectListIndex(0xB)]
        En_Firefly2 = 0x16E, // En_Firefly2

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2290)]
        [ActorInstanceSize(0x2C0)]
        [FileID(331)]
        [ObjectListIndex(0x181)]
        // the snowhead version that doesnt aggro is 01
        // turns out the reason it doesnt agro is that param is agro range, its so short its inside of the actor
        [GroundVariants(0x8C, 0x28, 0x3C, 0x46, 0x32, 0x1, 0x8023, 0x5, 0x14, 0x8028, 0x8014)]
        [WallVariants(0x1)] // peaceful, just wants cheese
        [PerchingVariants(0x2)]
        [RespawningVariants(0x8014, 0x8028, 0x8023, // tested respawning
            0x0032, 0x0005, 0x0014)] // untesed, assumed respawning because I'm lazy for now
        [VariantsWithRoomMax(max: 5, variant: 0x8C, 0x28, 0x3C, 0x46, 0x32, 0x1, 0x5, 0x14)]
        [VariantsWithRoomMax(max: 1, variant: 0x8014, 0x8028, 0x8023, 0x0032, 0x0005, 0x0014)]
        [CompanionActor(ClayPot, ourVariant: -1, variant: 0x10B, 0x115, 0x106, 0x101, 0x102, 0x10F, 0x115, 0x11F, 0x113, 0x110, 0x10E)]
        RealBombchu = 0x16F, // En_Rat

        // both the dripping water from the roof of caves, but also falling flame rocks from ISTT lava
        [ActorizerEnabled]
        [FileID(332)]
        [ObjectListIndex(0x182)] // this is its own special object? weird
        // this actor is also effects, mostly used in gyorg fight
        [CeilingVariants(0x00FF, // water drip spawner
            0x0001)] // fire rock spawner
        [UnkillableAllVariants]
        CeilingSpawner = 0x170, // En_Water_Effect

        [ActorizerEnabled]
        [FileID(333)]
        // this actor requires field_keep for the grass, and keaton for the keaton itself
        //[ObjectListIndex(2)] // requires field keep
        [ObjectListIndex(0x264)] // keaton object itself
        // now that we allign the object, we can remove the original, that means we need to take care of the check logic
        [CheckRestricted(Scene.NorthClockTown, variant: ActorConst.ANY_VARIANT, Item.HeartPieceKeatonQuiz,
            Item.CollectableNorthClockTownKeatonGrass1, Item.CollectableNorthClockTownKeatonGrass2, Item.CollectableNorthClockTownKeatonGrass3,
            Item.CollectableNorthClockTownKeatonGrass4, Item.CollectableNorthClockTownKeatonGrass5, Item.CollectableNorthClockTownKeatonGrass6,
            Item.CollectableNorthClockTownKeatonGrass7, Item.CollectableNorthClockTownKeatonGrass8, Item.CollectableNorthClockTownKeatonGrass9
        )]
        [CheckRestricted(Scene.MilkRoad, variant: ActorConst.ANY_VARIANT, Item.HeartPieceKeatonQuiz,
            Item.CollectableMilkRoadKeatonGrass1, Item.CollectableMilkRoadKeatonGrass2, Item.CollectableMilkRoadKeatonGrass3,
            Item.CollectableMilkRoadKeatonGrass4, Item.CollectableMilkRoadKeatonGrass5, Item.CollectableMilkRoadKeatonGrass6,
            Item.CollectableMilkRoadKeatonGrass7, Item.CollectableMilkRoadKeatonGrass8, Item.CollectableMilkRoadKeatonGrass9
        )]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT, Item.HeartPieceKeatonQuiz,
            Item.CollectableMountainVillageSpringKeatonGrass1, Item.CollectableMountainVillageSpringKeatonGrass2, Item.CollectableMountainVillageSpringKeatonGrass3,
            Item.CollectableMountainVillageSpringKeatonGrass4, Item.CollectableMountainVillageSpringKeatonGrass5, Item.CollectableMountainVillageSpringKeatonGrass6,
            Item.CollectableMountainVillageSpringKeatonGrass7, Item.CollectableMountainVillageSpringKeatonGrass8, Item.CollectableMountainVillageSpringKeatonGrass9
        )]
        [AlignedCompanionActor(BugsFishButterfly, CompanionAlignment.Above, ourVariant: -1,
            variant: 0x2324, 0x4324)] // butterflies over the bushes
        [GroundVariants(0x7F00, 0x400, 0x1F00)] //400 is milkroad, 7F00 is opening area, spring is 1F00
        [UnkillableAllVariants] // untested
        [EnemizerScenesPlacementBlock( // since this requires field keep, I have to block from other areas (since we dont have dual object requirements)
            Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple,
            Scene.PiratesFortress, Scene.PiratesFortressRooms, Scene.PiratesFortressExterior,
            Scene.BeneathTheWell, Scene.SecretShrine, Scene.IkanaCastle, Scene.IgosDuIkanasLair,
            Scene.SwampSpiderHouse, Scene.OceanSpiderHouse,
            Scene.DekuPlayground, // weird, has the same theme as the other grottos but only fake scene grass
            Scene.DekuTrial, Scene.GoronTrial, Scene.ZoraTrial, Scene.LinkTrial,
            Scene.SwordsmansSchool, Scene.ZoraHall, Scene.GoronRacetrack,
            // these are missing special object at all
            Scene.SPOT00, Scene.HoneyDarling, Scene.MayorsResidence, Scene.MilkBar,
            Scene.FishermansHut, Scene.PoeHut, Scene.MusicBoxHouse, Scene.StockPotInn, Scene.BombShop, Scene.GiantsChamber,
            Scene.TreasureChestShop, Scene.LotteryShop, Scene.GoronShop, Scene.SakonsHideout, Scene.DekuShrine,
            Scene.OdolwasLair, Scene.GohtsLair, Scene.GyorgsLair, Scene.TwinmoldsLair,
            Scene.SouthClockTown, Scene.WestClockTown, Scene.EastClockTown,
            Scene.TerminaField // lag
        )]
        // all field_keep scenes, shouldnt be randomized because of missing object but still exists in the list for some reason...
        [ForbidFromScene(Scene.RomaniRanch, Scene.Grottos, Scene.TerminaField)]
        //[PlacementWeight]
        KeatonGrass = 0x171, // En_Kusa2

        [FileID(334)]
        [ObjectListIndex(0x153)]
        ProximityFire = 0x172, // Bg_Spout_Fire

        Empty173 = 0x173,

        [FileID(290)]
        [ObjectListIndex(0x184)]
        // the most convoluted switch flags in the world
        GBTWaterwheelsandPushblocks = 0x174, // Bg_Dblue_Movebg

        // great fairy beam
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
        [FlyingToGroundHeightAdjustment(150)]
        //[ForbidFromScene(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        //[EnemizerScenesPlacementBlock(Scene.RoadToSouthernSwamp, Scene.TwinIslands, Scene.TwinIslandsSpring, Scene.NorthClockTown, Scene.MilkRoad, Scene.GreatBayCoast, Scene.IkanaCanyon)]
        Tingle = 0x176, // En_Bal

        [ActorizerEnabled]
        [FileID(337)]
        [ObjectListIndex(0xE3)]
        [GroundVariants(0xFF)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.WestClockTown)] // his object is shared with at least two other actors, cannot remove without removing them too
        Banker = 0x177, // En_Ginko_Man

        [ActorizerEnabled]
        [FileID(338)]
        [ObjectListIndex(0x186)]
        [GroundVariants(0xFFFF)]
        [OnlyOneActorPerRoom]
        [BlockingVariantsAll]
        [UnkillableAllVariants]
        [PlacementWeight(55)]
        PirateTelescope = 0x178, // En_Warp_Uzu

        [ActorizerEnabled]
        // flying ice platforms leading to lens cave
        [FileID(339)]
        [ObjectListIndex(0x187)]
        [DynaAttributes(22,13)]
        // this is not enough: this does NOT take into account scoop sanity
        [CheckRestricted( scene:Scene.MountainVillage, variant: ActorConst.ANY_VARIANT, check: Item.MaskGoron,
            Item.CollectableMountainVillageWinterSmallSnowball3, Item.CollectableMountainVillageWinterSmallSnowball4,
            Item.BottleCatchHotSpringWater)]
        // */
        // parameters unknown, they are not even and not time (time of spawn is a different parameter)
        // size and a few other things should be &0x3, so 0x1FFE is 2, D is 1, 4 and 0 are 0
        [WaterTopVariants(0x1FFE, 0x1FFD,
            0x1FFC, // unused &3 = 0 version
            0x1000, 0x1004)] // for replacement, we dont have water top pathing yet
        //[PathingVariants(0x1FFD, 0x1FFE)] // 0x7F >> 2, 0x1FC
        //[PathingTypeVarsPlacement(mask:0x7F, shift:2)]
        // TODO should we consider putting them on water top?
        // don't put too many in the world might run into BG issues
        [VariantsWithRoomMax(max: 0, variant: 0x1000, 0x1004)] // pathing type (path zero), and we dont want them to path
        [VariantsWithRoomMax(max: 6, variant: 0x1FFE, 0x1FFD, 0x1FFC)]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.MountainVillage)] // IF I can't detect when the ice is important, enable this
        IceBlockWaterPlatforms = 0x179, // Obj_Driftice

        [ActorizerEnabled]
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

        // 1/3 bugs that could spawn from a bottle, dig away after a few seconds
        [FileID(341)]
        [ObjectListIndex(0x1)]
        Mushi = 0x17B, // En_Mushi2

        //[ActorizerEnabled] // no point, if it spawns on the ground its too big and you can't tell you are inside of it
        // and enemies are so far away they are culled so you cant see them
        [FileID(342)]
        [ObjectListIndex(0x188)] // or 223? two different objects can work, makes it hard to cath
        [FlyingVariants(0x37F)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        Moon1 = 0x17C, // En_Fall

        [ActorizerEnabled]
        [FileID(343)]
        [ObjectListIndex(0x107)]
        [CheckRestricted(Item.HeartPieceNotebookPostman, Item.ItemBottleMadameAroma, Item.MaskPostmanHat,
            Item.NotebookMeetPostman, Item.NotebookPostmansGame, Item.NotebookPostmansFreedom)]
        [GroundVariants(0)] // no params
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.PostOffice)]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        BedroomPostman = 0x17D, // En_Mm3

        [FileID(344)]
        [ObjectListIndex(0x18A)]
        [DynaAttributes(6,8)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 7)]
        DekuRaceDoor = 0x17E, // Bg_Crace_Movebg

        // todo come back and figure out how to spawn regular 
        [ActorizerEnabled]
        [FileID(345)]
        [ObjectListIndex(0x1F3)]
        // dont specify only to the 
        [CheckRestricted(Item.MaskScents,
            Item.CollectableDekuShrineDekuButlerSRoomItem1, Item.CollectableDekuShrineDekuButlerSRoomItem2, Item.CollectableDekuShrineDekuButlerSRoomItem3,
            Item.CollectableDekuShrineDekuButlerSRoomItem4, Item.CollectableDekuShrineDekuButlerSRoomItem5, Item.CollectableDekuShrineDekuButlerSRoomItem6,
            Item.CollectableDekuShrineDekuButlerSRoomItem7, Item.CollectableDekuShrineDekuButlerSRoomItem8, Item.CollectableDekuShrineDekuButlerSRoomItem9,
            Item.CollectableDekuShrineDekuButlerSRoomItem10,
            Item.CollectableDekuShrineGreyBoulderRoomPot1,
            Item.CollectableDekuShrineGiantRoomFloor1Item1, Item.CollectableDekuShrineGiantRoomFloor1Item2,
            Item.CollectableDekuShrineGiantRoomFloor1Item3, Item.CollectableDekuShrineGiantRoomFloor1Item4,
            Item.CollectableDekuShrineGiantRoomFloor1Item5, Item.CollectableDekuShrineGiantRoomFloor1Item6,
            Item.CollectableDekuShrineGiantRoomFloor1Item7, Item.CollectableDekuShrineGiantRoomFloor1Item8,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem1, Item.CollectableDekuShrineRoomBeforeFlameWallsItem2,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem3, Item.CollectableDekuShrineRoomBeforeFlameWallsItem4,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem5, Item.CollectableDekuShrineRoomBeforeFlameWallsItem6,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem1, Item.CollectableDekuShrineWaterRoomWithPlatformsItem2,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem3, Item.CollectableDekuShrineWaterRoomWithPlatformsItem4,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem5, Item.CollectableDekuShrineWaterRoomWithPlatformsItem6)]
        // params 0xC000 is type, 0 and 1 are params, "other" is in front of his dead son
        // 0x2F8,
        // 0x7F,
        // 0xF ???
        // 0x8000 is cutscene version, 0x7FFF is main hall but doesn't spawn?
        //[GroundVariants(0x2000)]
        [GroundVariants(
            0x7FFF, // standing next to the king
            0x8000  // credits: in front of his son
        )]
        [PathingVariants(0x0001)] // inside of the race shrine
        [PathingTypeVarsPlacement(mask:0x7F, shift:0)]
        // good candidate for aligned front companions, to his son
        [VariantsWithRoomMax(max:0, variant: 0x8000,0x7FFF, 0x0001)] // none of the vanila variants spawn out of location or out of event
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 3)] // 0x3F8
        Butler = 0x17F, // En_Dno

        [EnemizerEnabled] // biggest issue: they dont really attack, this isn't the version that spawns over and over
        [ActorInitVarOffset(0x1C6C)]
        [FileID(346)]
        [ObjectListIndex(0x16B)]
        // A2 might be the ones that respawn over and over, the "Encounter"
        // 82 and 62 are found in the map room, both just kinda spin, never engages
        // zora cape has 000, same as the other two
        [WaterVariants(0xA2, 0x82, 0x62, 0)]
        SkullFish = 0x180, // En_Pr2

        [FileID(347)]
        [ObjectListIndex(0x16B)]
        DefeatedSkullfish = 0x181, // En_Prz

        // turns out his spawn in STT is on the ground, so cutscene actor?
        [EnemizerEnabled]
        [FileID(348)]
        [ObjectListIndex(0x155)]
        [GroundVariants(0, // stt light arrow fight
            1)]  // the other fights
        [DifficultAllVariants]
        [OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max:0, variant:0)] // cutscene variant is hardcoded
        [PlacementWeight(75)]
        //[ForbidFromScene(Scene.StoneTowerTemple)]
        GaroMaster = 0x182, // En_Jso2

        [EnemizerEnabled] // free enemy, placed in places where enemies are normally
        [FileID(349)]
        [ObjectListIndex(0x1)] // obj 1: gameplay keep, but can't set that
        [CheckRestricted(Scene.SouthClockTown, variant: ActorConst.ANY_VARIANT, Item.ChestSouthClockTownPurpleRupee)]
        [CheckRestricted(Scene.ZoraHallRooms, variant: ActorConst.ANY_VARIANT, Item.HeartPieceZoraHallScrub, Item.TradeItemOceanDeed, Item.ShopItemBusinessScrubGreenPotion)]
        //[ObjectListIndex(0xF3)] // TESTING
        [DynaAttributes(12,12)] // both gold and pink flowers have the same count
        [GroundVariants(0x7F, 0x17F)] // 7F is regular, 17F is big yellow
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.SouthernSwamp, Scene.Woodfall, Scene.DekuPalace, Scene.WoodfallTemple, Scene.OdolwasLair,
            Scene.GoronVillage, Scene.IkanaCanyon, 
            Scene.DekuPlayground, Scene.SwampSpiderHouse, Scene.DekuTrial,
            Scene.InvertedStoneTowerTemple, Scene.DekuPalace,
            Scene.StoneTowerTemple, Scene.GoronVillageSpring, Scene.GoronVillage,
            Scene.EastClockTown,  Scene.IkanaCastle, Scene.SnowheadTemple)] // Scene.NorthClockTown, ???
        DekuFlower = 0x183, // Obj_Etcetera

        [EnemizerEnabled] // AI gets confused, backwalks forever, pathing?
        [ActorInitVarOffset(0x445C)]
        [FileID(350)]
        [ObjectListIndex(0x18D)]
        // params: 7x >> 6 is switch, 0x3F is unk
        [PathingVariants(0x700, 0x940)]
        [PathingTypeVarsPlacement(mask: 0x3F, shift: 0)]
        [DifficultAllVariants]
        [OnlyOneActorPerRoom]
        [BlockingVariantsAll] // until we can fix his pathing, he will just sit there as a statue most of the time
        [ForbidFromScene(Scene.InvertedStoneTowerTemple, Scene.StoneTowerTemple)]
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // nothing wrong, just no place to put and huge object slows generation down
        [SwitchFlagsPlacement(mask: 0x7F, shift: 6)]
        Eyegore = 0x184, // En_Egol

        [EnemizerEnabled]
        [FileID(351)]
        [ObjectListIndex(0xBB)]
        // type: 0x3000: 0 is path, 1 air 2 water
        [WaterBottomVariants(0x2002, 0x2003, 0x2004, 0x2005, 0x2006, 0x200B, 0x200C, 0x200D)]
        [CeilingVariants(0x1014, 0x1016, 0x1017, 0x1018, 
            0x101E, 0x100D, 0x1011, 0x1019
            )] // loads more, think there are flags here
        [PerchingVariants(0x1012)] // non-vanilla link speed 12, attempting to perch
        [PathingVariants(0x0000)] // pathing type? requires us to introduce paths which might confuse our rando tho
        [PathingTypeVarsPlacement(mask:0xFF, shift:0)]
        // if I had a hanging from cieling thing like spiders this would work fine
        //[WallVariants(0x100D,  0x110E, 0x1011, 0x1014, 0x1016, 0x1017, 0x1019)]
        [UnkillableAllVariants] // actorcat PROP, not detected as enemy
        [FlyingToGroundHeightAdjustment(300)]
        [VariantsWithRoomMax(max:0, 0x101E, 0x100D, 0x1011, 0x1019, 0x1014)] // until I can get cieling detection modification, this is weird
        //[ForbidFromScene(Scene.InvertedStoneTowerTemple, Scene.StoneTowerTemple)]
        SpikedMine = 0x185, // Obj_Mine

        [FileID(352)]
        [ObjectListIndex(0x1)]
        Obj_Purify = 0x186, // Obj_Purify

        [ActorizerEnabled]
        [FileID(353)]
        [ObjectListIndex(0x18E)]
        [CheckRestricted(Item.ItemPictobox)]
        [GroundVariants(0xFF)] // path = invalid
        [PathingVariants(0x6, 0x3, 0x2)]
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [OnlyOneActorPerRoom]
        //[VariantsWithRoomMax(max:0, variant: 0xFF)]
        [ForbidFromScene(Scene.WoodsOfMystery)] // for now, until I can detect items required for boat race
        [UnkillableAllVariants]
        InjuredKoume = 0x187, // En_Tru

        // TODO get these two witches working in the world
        [ActorizerEnabled]
        [FileID(354)]
        [ObjectListIndex(0x18F)]
        [CheckRestricted(Item.ShopItemWitchBluePotion, Item.ShopItemWitchGreenPotion, Item.ShopItemWitchRedPotion,
            Item.ItemBottleWitch,
            Item.MundaneItemKotakeMushroomSaleRedRupee)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(0, 0)] // no collider no interaction
        [UnkillableAllVariants]
        ShopKeepKotake = 0x188, // En_Trt

        Empty189 = 0x189,
        Empty18A = 0x18A,


        [FileID(355)]
        [ObjectListIndex(0x1)]
        SpringWater = 0x18B, // En_Test5

        [FileID(356)]
        [ObjectListIndex(0x1)]
        SongOfTimeEffects = 0x18C, // En_Test6

        //todo 
        [FileID(357)]
        [ObjectListIndex(0x198)]
        // params: f0ff/f1ff is standing after getting the final item
        //f603 is sitting on the bottom of the ocean
        // assumed pathless are 0xF1FF-0xF5FF
        // params: 0xF00 is type, 0xFF is path
        //[WaterVariants] //woried they are pathing
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        AngryBeavers = 0x18D, // En_Az

        [FileID(358)]
        [ObjectListIndex(0x18D)]
        EyeGoreRubble = 0x18E, // En_Estone

        [FileID(359)]
        [ObjectListIndex(0x190)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        SnowHeadCentralPillar = 0x18F, // Bg_Hakugin_Post

        // these are trees and the ground as part of the opening cutscene
        //[ActorizerEnabled] // disabled because we have a custom replacement with actual collider and tree bonk
        [FileID(360)]
        [ObjectListIndex(0x169)]
        //[DynaAttributes()] // ground surface one only
        // 0xFF is type, zero is the floor, the other three are trees
        // 0xFF00 is... unknown passed to another function
        // 0102 exists, 0202, 0103, FF00
        //[GroundVariants(1,2,3)]
        //[GroundVariants(0x1,2,3)]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [ForbidFromScene(Scene.LostWoods)]
        LostWoodsCutsceneTrees = 0x190, // Dm_Opstage

        // requires like 3 objects wtf
        //0x192 0x1BE,0x277);
        [FileID(361)]
        [ObjectListIndex(0x192)]
        [GroundVariants(0)] // top of clocktower in intro cutscene, also new day cutscene
        [ForbidFromScene(Scene.TerminaField)] // WARNING: not just the tower in the telescope but credits
        // != 1 is a param, 1 is a param
        SkullkidCutscene = 0x191, // Dm_Stk

        // todo attempt randomize
        [FileID(362)]
        [ObjectListIndex(0x1C8)]
        TatlTaelCutsceneActors = 0x192, // Dm_Char00

        // todo attempt randomize
        [FileID(363)]
        [ObjectListIndex(0x1A2)]
        //[DynaAttributes()] // multiple: the temple itself, regular and poison are different?, the ramp that shows up after the clear, 
        RisingWoodfallTemple = 0x193, // Dm_Char01

        [FileID(364)]
        [ObjectListIndex(0x1BE)]
        SkullKidsOcarina = 0x194, // Dm_Char02

        [FileID(365)]
        [ObjectListIndex(0x1A3)]
        DekuMaskCutsceneFalling = 0x195, // Dm_Char03

        //[ActorizerEnabled]
        [FileID(366)]
        [ObjectListIndex(0x77)] // 1
        // 0 is tatl regular
        [FlyingVariants(0)] // unk
        [GroundVariants(0)] // unk
        [VariantsWithRoomMax(max: 1, variant: 0)]
        [UnkillableAllVariants]
        Dm_Char04 = 0x196, // Dm_Char04

        [FileID(367)]
        [ObjectListIndex(0x213)]
        CreditsMaskObjects = 0x197, // Dm_Char05

        [FileID(368)]
        [ObjectListIndex(0x1E6)]
        SnowheadMountainsSpringTransitionEffects = 0x198, // Dm_Char06

        // unknown
        [FileID(369)]
        [ObjectListIndex(0x212)]
        MilkBarCutsceneObjects  = 0x199, // Dm_Char07

        // cannot put places without modification because he actionFunc=null crashes, hardcoded to the two scenes
        //[ActorizerEnabled]
        [FileID(370)]
        [ObjectListIndex(0x229)]
        [DynaAttributes(105,65)] // has two: asleep(105,65) and awake(12,8), also has a HUGE unused one
        [GroundVariants(0)]
        [WaterBottomVariants(0)]
        [VariantsWithRoomMax(max:1, variant:0)]
        [UnkillableAllVariants]
        LargeGreatBayTurtle = 0x19A, // Dm_Char08

        // todo reattempt: could not spawn
        [FileID(371)]
        // params: 0xF, 0x100
        [ObjectListIndex(0x1EB)]
        PiratesFortressCutsceneCharacters = 0x19B, // Dm_Char09

        [ActorizerEnabled]
        [FileID(372)]
        [ObjectListIndex(0x18C)]
        [WallVariants(0x907F, 0xA07F)]
        [UnkillableAllVariants]
        Clock = 0x19C, // Obj_Tokeidai

        Empty19D = 0x19D,

        // the only one we can put down is the credits version that basically tposes, make this a modified actor instead
        // but we want to remove the other one for peaking possibly, for now we can do both even if the later is boring
        [ActorizerEnabled]
        [FileID(373)]
        [ObjectListIndex(0x195)]
        [CheckRestricted(scene:Scene.DekuKingChamber, variant: ActorConst.ANY_VARIANT, // 0x2FF,
            Item.SongSonata)]
        // params
        // type (0x780) >> 7 // holy shit why
        [GroundVariants(0xFD7F, // credits monk, wish it did more than tpose...
                        0xFC7F, // standing in front of the deku king
                        0xFCFF, // also standing in front of the king? what the hell
                        0x6181 // near deku king
                      /*0x7B7F*/ )] // roast
        [WallVariants(0x02FF)] // tied to a pole
        [PathingVariants(
            0x0082, // near entrance/exit of woods of mystery
            0x1882, // near entrance/exit of swamp
            0x2210 // near entrance to deku palace
        )]
        [PathingTypeVarsPlacement(mask:0xF800, shift:11)] // unk test
        [SwitchFlagsPlacement(mask:0x7F, shift:0)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 0, 0xFC7F, 0xFCFF,
            0x82, 0x1882, 0x2210, 0x6182)] // situationally appear, otherwise invisible, also assume path
        [EnemizerScenesPlacementBlock(Scene.WoodsOfMystery)] // freaks out vanilla monkeys, inf loop in "EnMnk_AlreadyExists"
        [PlacementWeight(60)]
        Monkey = 0x19E, // En_Mnk

        // assumed spawned rock from eyegore ground slam
        [FileID(374)]
        [ObjectListIndex(0x18D)]
        [DynaAttributes(10,8)]
        EyegoreBlock = 0x19F, // En_Egblock

        [ActorizerEnabled]
        [FileID(375)]
        [ObjectListIndex(0x135)]
        [GroundVariants(0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        [ForbidFromScene(Scene.DekuPalace)] // do not remove original for now
        PalaceGuardDeku = 0x1A0, // En_Guard_Nuts

        [FileID(376)]
        [ObjectListIndex(0x190)]
        Bg_Hakugin_Bombwall = 0x1A1, // Bg_Hakugin_Bombwall

        [FileID(377)]
        [ObjectListIndex(0x197)]
        ClocktowerSwingingDoors = 0x1A2, // Obj_Tokei_Tobira

        // the ones you raise with goron pound
        [FileID(378)]
        [ObjectListIndex(0x190)]
        [DynaAttributes(10,8)]
        RaisableSnowheadPillar = 0x1A3, // Bg_Hakugin_Elvpole

        [ActorizerEnabled] // regular romani
        [FileID(379)]
        [ObjectListIndex(0xB7)] // 100 and FF00
        // cremia shows up if you repel the aliens even if romani is gone
        [CheckRestricted(Item.SongEpona, Item.ItemBottleAliens, Item.NotebookPromiseRomani, Item.NotebookSaveTheCows)]
        //[CheckRestricted(Scene.RanchBuildings, variant:ActorConst.ANY_VARIANT, check: Item.Notebook)]
        [PathingVariants(0xFF00, 0x100)]
        [PathingTypeVarsPlacement(mask: 0xFF00, shift: 8)]
        [UnkillableAllVariants]
        // TODO fix this later
        [ForbidFromScene(Scene.RomaniRanch)] // if removed, and another romani teleports player, game is stuck
        RomaniWithBow = 0x1A4, // En_Ma4

        //[ActorizerEnabled] // none of the types will spawn out of minigame
        // cannot turn into mmra, need to modify the actor to speed up, but even with shifting the actor will not draw, reason unknown
        [FileID(380)]
        [ObjectListIndex(0x199)]
        [DynaAttributes(32, 32)]
        // 0xF are type, 1 is ring 2 is wall
        //[WaterVariants(0x2)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.WaterfallRapids)] // do not remove the original, yet
        ZoraRaceRing = 0x1A5, // En_Twig

        [ActorizerEnabled]
        [ActorInstanceSize(0x330)] // 274 but fake increase size to reduce frequency
        [FileID(381)]
        [ObjectListIndex(0x19B)]
        // vars: in vanilla 0 only spawns if it can find romani, 0x80XX are timed fuse baloons for credits scene
        // we have fixed 0x0 so that it doesn't auto-despawn, however, since the code handles her not being available anyway
        [GroundVariants(0x0)]
        [FlyingVariants(0x0)]
        [VariantsWithRoomMax(max: 6, variant: 0x0)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.RomaniRanch)] // dont replace actual romani balloons
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // long draw distance means they can overflow actor spawn
        PoeBalloon = 0x1A6, // En_Po_Fusen

        // some type of wooden door
        [FileID(382)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        En_Door_Etc = 0x1A7, // En_Door_Etc

        [FileID(383)]
        [ObjectListIndex(0x19E)]
        [ForbidFromScene(Scene.SouthernSwamp, Scene.DekuPalace)]
        BigOcto = 0x1A8, // En_Bigokuta

        // requires ice surface type
        [FileID(384)]
        [ObjectListIndex(0x1E7)]
        [DynaAttributes(22, 13)]
        IcePlatform = 0x1A9, // Bg_Icefloe

        [FileID(391)]
        [ObjectListIndex(0x163)]
        Obj_Ocarinalift = 0x1AA, // Obj_Ocarinalift

        // song of soaring teaching block
        [FileID(392)]
        [ObjectListIndex(0x1)] // no draw func
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        En_Time_Tag = 0x1AB, // En_Time_Tag

        [FileID(393)]
        [ObjectListIndex(0x19F)]
        Bg_Open_Shutter = 0x1AC, // Bg_Open_Shutter

        // dont work probably because they need to turn ON for the cutscene
        //[ActorizerEnabled]
        [FileID(394)]
        [ObjectListIndex(0x19F)]
        [WallVariants(0)]
        [UnkillableAllVariants]
        SkullkidDekuCurseStarSpotlights = 0x1AD, // Bg_Open_Spot

        [FileID(395)]
        [ObjectListIndex(0x1A0)]
        [DynaAttributes(58, 41)]
        HoneyAndDarlingRotationPlatform = 0x1AE, // Bg_Fu_Kaiten

        [FileID(396)]
        [ObjectListIndex(0x1)]
        // vars 1 is hot water
        BottleWaterDrop = 0x1AF, // Obj_Aqua

        [FileID(397)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        StrayFairy = 0x1B0, // En_Elforg

        [ActorizerEnabled] // don't give actual items, sadly, just jape you into thinking they are items
        [FileID(398)]
        [ObjectListIndex(0xE)]
        // snowhead : 0x5E00,0x6000, 0x5800,0x5600, GreatBay: 0x6000
        // huh? these repeat per dungeon? 
        //[FlyingVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        //[GroundVariants(0x5E00, 0x6000, 0x5800, 0x5600)]
        [CeilingVariants(0x5E00, 0x6000, 0x5800, 0x5600)] // not really a thing, but lets see if we can't do this as a joke
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)] // 0xFE00
        [ForbidFromScene(Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)]
        ElfBubble = 0x1B1, // En_Elfbub

        Empty1B2 = 0x1B2,

        // probably an object check missing somewhere
        //[ActorizerEnabled] // does not spawn
        [FileID(399)]
        [ObjectListIndex(0x1A1)]
        [DynaAttributes(24,14)]
        [WallVariants(0x0)] // unk because spawned by H+D
        [UnkillableAllVariants] // not enemy type, right?
        Target = 0x1B3, // En_Fu_Mato

        //[ActorizerEnabled] // does not spawn
        [FileID(400)]
        [ObjectListIndex(0x1A1)]
        [DynaAttributes(44,24)] // welp
        [WallVariants(0x1)] // unk because spawned by H+D
        [UnkillableAllVariants] // not enemy type, right?
        BombBasket = 0x1B4, // En_Fu_Kago

        [ActorizerEnabled] // hes really rare because his ram requirements are huge
        [FileID(401)]
        [ObjectListIndex(0x1A3)]
        // there are other params forthis actor, todo explor
        [GroundVariants(0, // 0 is clocktower,
            0x2, //  2 is wiped out
            0x3)]  // in the cutscenes??
        [VariantsWithRoomMax(max: 1, variant: 0x2)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        // todo add fairies and butterflies and bees
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        [AlignedCompanionActor(Butterfly, CompanionAlignment.Above, ourVariant: -1,
            variant: 0, 1, 2)]
        [AlignedCompanionActor(GiantBeee, CompanionAlignment.Above, ourVariant: 0x2,
            variant: 0, 1, 2, 3, 4, 5)]
        [EnemizerScenesPlacementBlock(Scene.TerminaField)] // TF has object size issues, this is the largest object, this is here just to speed up
        HappyMaskSalesman = 0x1B5, // En_Osn

        //[ActorizerEnabled] // issue: cannot place organ because its waiting for the cutscene part to appear, also no hitbox
        //  instead we modified it and inject changes to get it working
        [FileID(402)]
        [ObjectListIndex(0x88)]
        [DynaAttributes(14,14)]
        [BlockingVariantsAll]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard, // dyna crash possible
        //    Scene.StoneTower, Scene.DekuPlayground)] // dyna crash possible
        ClocktowerGearsAndOrgan = 0x1B6, // Bg_Ctower_Gear

        [ActorizerEnabled]
        [FileID(403)]
        [ObjectListIndex(0x18F)]
        [CheckRestricted(Item.ItemBottleWitch,
            Item.MundaneItemKotakeMushroomSaleRedRupee,
            Item.ShopItemWitchBluePotion, Item.ShopItemWitchGreenPotion, Item.ShopItemWitchRedPotion)] // because people can play without SOT, she can become unavailable in shop
        // nothing in the other params other than path, the starting animation and stuff are all hardcoded to entrance
        [PathingVariants(0x2400, 0x2000)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        // disabled since talking is softlock, need to figure that out
        [VariantsWithRoomMax(max: 0, variant: 0x2400, 0x2000)]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.WoodsOfMystery, Scene.SouthernSwamp)]
        KotakeOnBroom = 0x1B7, // En_Trt2

        //[ActorizerEnabled] // kinda want to use as a test because of 2/4
        [FileID(404)]
        [ObjectListIndex(0x1A4)]
        [DynaAttributes(2,4)]
        ClockTowerDoorAndStairs = 0x1B8, // Obj_Tokei_Step

        [ActorizerEnabled]
        [FileID(405)]
        [ObjectListIndex(0x1A5)]
        [DynaAttributes(12, 8)]
        // params is == 0 and else
        [WaterTopVariants(0, 1)]
        [CheckRestricted(Item.HeartPieceBoatArchery)]
        [UnkillableAllVariants]
        // this could work but is a headache, we need to make sure all checks that are reachable by deku and nut are included...
        //[CheckRestricted(Scene.DekuPalace, ActorConst.ANY_VARIANT,Item.MaskScents)]
        [CheckRestricted(Scene.Woodfall, ActorConst.ANY_VARIANT, Item.ChestWoodfallBlueRupee, Item.ChestWoodfallRedRupee,
            Item.CollectableWoodfallItem1, Item.CollectableWoodfallPot1, Item.CollectableWoodfallPot2)]
        [ForbidFromScene(Scene.SouthernSwamp, // no easy way to identify if we need deku hopping
            Scene.DekuPalace)] // see above
        Lilypad = 0x1B9, // Bg_Lotus

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1FF0)]
        [FileID(406)]
        [ObjectListIndex(0x1A6)]
        [GroundVariants(0x0)]
        [DifficultVariants(0)]
        [CompanionActor(DekuFlower, ourVariant: -1, variant: 0x7F)]
        //[ForbidFromScene(Scene.WoodfallTemple)] // req for gekko miniboss, do not touch until fix
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        Snapper = 0x1BA, // En_Kame

        // ohhh this would be so stupid
        [FileID(407)]
        [ObjectListIndex(0x1B4)]
        TreasureShopRisingWall = 0x1BB, // Obj_Takaraya_Wall

        [FileID(408)]
        [ObjectListIndex(0x1A0)]
        // has dyna but its a water box, dont care
        HoneyAndDarlingWaterLevel = 0x1BC, // Bg_Fu_Mizu

        // wrong one, the one we want is burrowed, but he also does NOT come with a flower, its secondary
        [FileID(409)]
        [ObjectListIndex(0x1E5)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.SouthClockTown, Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.GoronVillage, Scene.GoronVillageSpring, Scene.ZoraHallRooms, Scene.IkanaCanyon)]
        FlyingBuisinessScrub = 0x1BD, // En_Sellnuts

        [ActorizerEnabled] // use as test for dyna with its tiny 2/4
        [FileID(410)]
        [ObjectListIndex(0x1A7)]
        [DynaAttributes(2,4)]
        [GroundVariants(0x7F)]
        [VariantsWithRoomMax(max:0, variant:0x7F)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        SwampSpiderHouseCutableIvy = 0x1BE, // Bg_Dkjail_Ivy

        Empty1BF = 0x1BF,

        [ActorizerEnabled]
        [FileID(411)]
        [ObjectListIndex(0x1A8)]
        [DynaAttributes(10,8)]
        [CheckRestricted(Scene.GoronVillage, variant: ActorConst.ANY_VARIANT, Item.ItemLens, Item.ChestLensCaveRedRupee, Item.ChestLensCavePurpleRupee)]
        [CheckRestricted(Scene.PathToSnowhead, variant: ActorConst.ANY_VARIANT, Item.HeartPieceToSnowhead)]
        [CheckRestricted(Scene.IkanaCastle, variant: ActorConst.ANY_VARIANT, Item.SongElegy,
            Item.CollectableAncientCastleOfIkana1FWestStaircasePot1, Item.CollectableAncientCastleOfIkanaFireCeilingRoomPot1,
            Item.CollectableAncientCastleOfIkanaHoleRoomPot1, Item.CollectableAncientCastleOfIkanaHoleRoomPot2,
            Item.CollectableAncientCastleOfIkanaHoleRoomPot3, Item.CollectableAncientCastleOfIkanaHoleRoomPot4,
            Item.HeartPieceIkana)]
        [FlyingVariants(0x0)]
        [VariantsWithRoomMax(max: 0, variant: 0x0)] // invisible, not even seen just looks empty, unless I can move actors to sit ontop of them or something
        [UnkillableAllVariants]
        FlyingLensCaveIcePlatforms = 0x1C0, // Obj_Visiblock

        [ActorizerEnabled]
        [FileID(412)]
        [ObjectListIndex(0x129)]
        [CheckRestricted(Item.HeartPieceTreasureChestGame, Item.MundaneItemTreasureChestGameDekuNuts, Item.MundaneItemTreasureChestGamePurpleRupee, Item.MundaneItemTreasureChestGameRedRupee)]
        [GroundVariants(0xFFFF, 0)] // zero is regular, -1 is credits?
        //[GroundVariants(0)] // zero is regular, -1 is credits?
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)]
        //[AlignedCompanionActor(VariousWorldSounds2, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x0146)] // treasure chest shop music
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown)] // can bug out the scene transit into skullkid
        //[ForbidFromScene(Scene.TreasureChestShop)]
        // switch flags
        // manually sunsets switch 5, sets 5 separate switches based on player form, but I think these are all as a result of willing the game
        [PlacementWeight(75)]
        Takaraya = 0x1C1, // En_Takaraya, "Bombchu girl"

        [ActorizerEnabled]
        [FileID(413)]
        [ObjectListIndex(0x1A9)]
        [CheckRestricted(Item.MundaneItemSeahorse)]
        // type: 100 and else
        [GroundVariants(0x001, // regular
            0x100, 0x101)] // talking spots I think
        [VariantsWithRoomMax(max:0, variant:0x101, 0x100)] // doesn't spawn correctly
        [UnkillableAllVariants]
        GreatBayFisherman = 0x1C2, // En_Tsn

        //[ActorizerEnabled] // we have a better version as a custom actor now, less stupid
        [FileID(414)]
        [ObjectListIndex(0x1AA)]
        [GroundVariants(0)] // just stands around
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        OOTPotionShopMan = 0x1C3, // En_Ds2n

        [ActorizerEnabled]
        [FileID(415)]
        [ObjectListIndex(0x1AB)]
        [CheckRestricted(Scene.CuriosityShop, variant: ActorConst.ANY_VARIANT,
            Item.MaskKeaton, Item.TradeItemMamaLetter,
            Item.MaskAllNight,
            Item.MundaneItemCuriosityShopBlueRupee, Item.MundaneItemCuriosityShopGoldRupee, Item.MundaneItemCuriosityShopPurpleRupee, Item.MundaneItemCuriosityShopRedRupee,
            Item.NotebookMeetCuriosityShopMan, Item.NotebookCuriosityShopManSGift, Item.NotebookPromiseCuriosityShopMan, Item.NotebookPurchaseCuriosityShopItem)]
        [GroundVariants(0x0, // inside of shop
            0x1)] // wedding, and giving two items behind shop
        [OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max: 0, variant: 0x0, 0x1)] // wedding version is the same as day 3, gives too many checks
        [UnkillableAllVariants]
        CuriosityShopMan = 0x1C4, // En_Fsn

        [ActorizerEnabled]
        [FileID(416)]
        [ObjectListIndex(0x1AC)]
        // this is not enough because he is boat master
        [CheckRestricted(Item.HeartPiecePictobox, Item.MundaneItemPictographContestBlueRupee, Item.MundaneItemPictographContestRedRupee)]
        [GroundVariants(0)] // he has LEGS :O
        //[VariantsWithRoomMax(0, 0)]
        [ForbidFromScene(Scene.TouristCenter)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        [PlacementWeight(75)]
        SwampTouristGuide = 0x1C5, // En_Shn

        Empty1C6 = 0x1C6,

        [ActorizerEnabled]
        [FileID(417)]
        [ObjectListIndex(0x1B6)]
        [GroundVariants(0x7F, 0x307F, 0x207F, 0x107F)]
        [BlockingVariantsAll]
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        GateSoldier = 0x1C7,

        // this is both the hanging iceccyle in termina field and the ones in the temple 
        [ActorizerEnabled]
        [ObjectListIndex(0x1AD)]
        [FileID(418)]
        [CheckRestricted(Item.ItemSnowheadKey2,
                         Item.CollectableSnowheadTempleIceBlockRoomItem1,
                         Item.CollectableSnowheadTempleIceBlockRoomItem2,
                         Item.CollectableSnowheadTempleIceBlockRoomItem3, // should be just for the sht scene but thats the only place it shows up
                         Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball3,
                         Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball4,
                         Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball5
            )] 
        // FF01 is the ice blocking the path north
        // 0x5AXX seems to be the blocking path ice walls from snowhead temple
        //[CeilingVariants(0x5A00)] // we dont want t remove, and placing them on the floor 
        [CeilingVariants(0x5A08, 0x5A0A, 0x5A0C, 0x5A0D, 0x5A0E // snowhead
            )]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [VariantsWithRoomMax(max:0, variant:0x5A00)]
        [ForbidFromScene(Scene.TerminaField)]
        LargeBreakableIceDrop = 0x1C8, // Obj_BigIcicle

        [ActorizerEnabled]
        [FileID(419)]
        [ObjectListIndex(0x1E5)]
        [CheckRestricted(Scene.DekuPlayground, variant: ActorConst.ANY_VARIANT,
            Item.CollectableDekuPlaygroundItem1, Item.CollectableDekuPlaygroundItem2, Item.CollectableDekuPlaygroundItem3,
            Item.CollectableDekuPlaygroundItem4, Item.CollectableDekuPlaygroundItem5, Item.CollectableDekuPlaygroundItem6,
            Item.CollectableDekuPlaygroundItem7, Item.CollectableDekuPlaygroundItem8, Item.CollectableDekuPlaygroundItem9,
            Item.CollectableDekuPlaygroundItem10, Item.CollectableDekuPlaygroundItem11, Item.CollectableDekuPlaygroundItem12,
            Item.CollectableDekuPlaygroundItem13, Item.CollectableDekuPlaygroundItem14, Item.CollectableDekuPlaygroundItem15,
            Item.CollectableDekuPlaygroundItem16, Item.CollectableDekuPlaygroundItem17, Item.CollectableDekuPlaygroundItem18,
            Item.MundaneItemDekuPlaygroundPurpleRupee, Item.HeartPieceDekuPlayground)]
        [GroundVariants(0x7FF, 0x6FF)]
        [VariantsWithRoomMax(max: 0, variant: 0x7FF, 0x6FF)] // if you actually talk to them, and start the game, they cutscene hardlock
        [UnkillableAllVariants]
        DekuPlaygroundKeepers = 0x1C9, // En_Lift_Nuts

        [ActorizerEnabled]
        [FileID(420)]
        [ObjectListIndex(0x1AF)]
        [CheckRestricted(Scene.IkanaGraveyard, variant: ActorConst.ANY_VARIANT, // 0x814,
            Item.CollectableIkanaGraveyardDay2Bats1)]
        [CheckRestricted(Scene.DampesHouse, variant: ActorConst.ANY_VARIANT, // 0x10,
            Item.ItemBottleDampe)]
        [GroundVariants(0x1, 0x10, 0xFFF3,  // basement
            0x12, // trembling
            0x814)] // outside
        [VariantsWithRoomMax(max: 0, variant: 0x1, 0x814)] //assumption is that hes too hardcoded to put places for now
        [VariantsWithRoomMax(max: 0, variant: 0xFFF3, 0x814)] // dont put the digg spots without a digger its just weird, thats weird
        [SwitchFlagsPlacement(mask: 0x7F, shift: 4)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 0, variant: 0x1, 0x10, 0xFFF3, 0x12, 0x814)] // too hard coded right now to work correct
        Dampe = 0x1CA, // En_Tk

        Empty1CB = 0x1CB,

        // is this the west clocktown stairs?
        [FileID(421)]
        [ObjectListIndex(0x1B0)]
        Bg_Market_Step = 0x1CC, // Bg_Market_Step

        [ActorizerEnabled]
        [FileID(422)]
        [ObjectListIndex(0x163)]
        [CheckRestricted(Scene.DekuPlayground, variant: ActorConst.ANY_VARIANT,
            Item.CollectableDekuPlaygroundItem1, Item.CollectableDekuPlaygroundItem2, Item.CollectableDekuPlaygroundItem3,
            Item.CollectableDekuPlaygroundItem4, Item.CollectableDekuPlaygroundItem5, Item.CollectableDekuPlaygroundItem6,
            Item.CollectableDekuPlaygroundItem7, Item.CollectableDekuPlaygroundItem8, Item.CollectableDekuPlaygroundItem9,
            Item.CollectableDekuPlaygroundItem10, Item.CollectableDekuPlaygroundItem11, Item.CollectableDekuPlaygroundItem12,
            Item.CollectableDekuPlaygroundItem13, Item.CollectableDekuPlaygroundItem14, Item.CollectableDekuPlaygroundItem15,
            Item.CollectableDekuPlaygroundItem16, Item.CollectableDekuPlaygroundItem17, Item.CollectableDekuPlaygroundItem18,
            Item.MundaneItemDekuPlaygroundPurpleRupee, Item.HeartPieceDekuPlayground)]
        [FlyingVariants(0x8000, 0x8001, 0x8002, 0x8003, 0x8004, 0x9005, // pathing types too
                        0x8008, 0x8009, 0x800A, 0x800B, 0x800C, 0x900D,
                        0x800E, 0x800F, 0x8010, 0x8011, 0x9012, 0x8013)]
        // we are not putting these in the world, only replacing them
        // why? because they all use paths, but they are flying, this is an annoying double type that we dont account for normally
        [VariantsWithRoomMax(max: 0,
            0x8000, 0x8001, 0x8002, 0x8003, 0x8004, 0x9005,
            0x8008, 0x8009, 0x800A, 0x800B, 0x800C, 0x900D,
            0x800E, 0x800F, 0x8010, 0x8011, 0x9012, 0x8013)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        DekuPlayGroundGameElevator = 0x1CD, // Obj_Lupygamelift

        // the effects in the cutscene, where link is surrounded by feathers
        [FileID(423)]
        [ObjectListIndex(0x1)]
        SoaringEffects = 0x1CE, // En_Test7

        //[ActorizerEnabled]
        // this actor CRASHES if you hit it with a light arrow
        // more specifically it spawns Demo_Effect which crashes trying to draw its curv skeleton, reason unknown
        [FileID(424)]
        [ObjectListIndex(0x1B3)]
        [DynaAttributes(12, 8)]
        [GroundVariants(0x101, 0x201)]
        [WaterBottomVariants(0x1)] // dont normally show up down there but its fine
        [SwitchFlagsPlacement(mask: 0xF00, shift: 8)]
        [UnkillableAllVariants] // I think...?
        [BlockingVariantsAll]
        Lightblock = 0x1CF, // Obj_Lightblock

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
        // looks like two params 0x378 and 0x7F and both are some form of animation speed...??
        [WaterVariants(0x3FA8)] // stt (underwater wall)
        [WallVariants(0x1932, // greatbay
            0x3FFF)] // istt hanging from wall
        [GroundVariants(
            0x191E)] // below well
        [PerchingVariants(0x2034, 0x3EFE)] // non-vanilla variants so they can show up on perchest
        [VariantsWithRoomMax(max: 8, variant: 0x1932, 0x3FFF)]
        [VariantsWithRoomMax(max: 0, variant: 0x3FA8)] // do not place water variant because dont hav a water wall type yet, which is what this really is, putting in water floats in the water column
        [ForbidFromScene(Scene.StoneTowerTemple)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.GoronRacetrack)]
        Dexihand = 0x1D1, // En_WdHand : ???'s water logged brother

        [FileID(427)]
        [ObjectListIndex(0x1)]
        DekuPlayGroundGameRupee = 0x1D2, // En_Gamelupy

        [FileID(428)]
        [ObjectListIndex(0x1)] // what?
        [DynaAttributes(12,8)]
        DampeHouseElevator = 0x1D3, // Bg_Danpei_Movebg

        [ActorizerEnabled]
        [FileID(429)]
        [ObjectListIndex(0x1B7)]
        // 0xF80 is the drop table
        [GroundVariants(
            0x0100, // only vanilla param weirdly
            0, 0x0080, 0x0180, 0x0200, 0x0280, 0x0380, 0x0400)] 
        [UnkillableAllVariants]
        [PlacementWeight(20)]
        SnowCoveredTrees = 0x1D4, // En_Snowwd

        // I suspect since he has so few vars that he will be hard coded, and req decomp to fix
        // TODO add more options to randomize some but not all of them based on checks
        [ActorizerEnabled]
        [FileID(430)]
        [ObjectListIndex(0x107)]
        [CheckRestricted(Item.HeartPieceNotebookPostman, Item.ItemBottleMadameAroma, Item.MaskPostmanHat,
            Item.NotebookMeetPostman, Item.NotebookPostmansFreedom)]
        // this could be pathing, but the paths are part of the schedule
        [GroundVariants(0x1, // inn, milkbar
            0x2, // southclocktown
            0x0)] // patroling around the world
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        //[VariantsWithRoomMax()]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:0, variant:0, 1, 2)] // do not place: hardcoded up the wazzo
        //[ForbidFromScene(Scene.WestClockTown, Scene.EastClockTown, Scene.NorthClockTown, Scene.SouthClockTown)]
        PostMan = 0x1D5, // En_Pm

        [FileID(431)]
        [ObjectListIndex(0x1)]
        Songwall = 0x1D6, // En_Gakufu

        [FileID(432)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        Elf_Msg4 = 0x1D7, // Elf_Msg4

        [FileID(433)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        Elf_Msg5 = 0x1D8, // Elf_Msg5

        // HP from labfish, Bomb from garo master boss
        [FileID(434)]
        [ObjectListIndex(0x1)]
        RandomSpawnedItems = 0x1D9, // En_Col_Man

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2940)]
        [FileID(435)]
        [ObjectListIndex(0x75)]
        // issue, we dont know how to check if the scoop is important or not, could be a lot of checks
        //[CheckRestricted(Scene.BeneathTheWell, variant: ActorConst.ANY_VARIANT, Item.Curi)]
        // params: switch flag(xFF0) and item used(0xF)
        // 8 is bigpo, 0 is blue pot, 7 is hot spring, 9 is milk
        [GroundVariants(1, 2, 3, 4, 5, 6)]
        [RespawningAllVariants] // only to stop them from showing up in places where you need to kill them, since its a hastle
        [ForbidFromScene(Scene.BeneathTheWell, Scene.IkanaCanyon)]
        [EnemizerScenesPlacementBlock(Scene.DekuShrine, Scene.Grottos,
            Scene.GoronRacetrack, Scene.GoronRacetrack)] // slows down player too much in a race setting
        [SwitchFlagsPlacement(mask: 0xFF, shift: 4)]
        [BlockingVariantsAll]
        GibdoWell = 0x1DA, // En_Talk_Gibud

        //[ActorizerEnabled] // had to modify him to make him more interesting, is now an MMRA
        [FileID(436)]
        [ObjectListIndex(0x1B8)]
        // 0xFE01-4 are in termina field setup 1, assumption being the cutscene
        // 0xFE08-B are in st 6
        // chamber are 0xFE08 and 0xFE0F
        //0xFE0F doesnt spawn, assumping its the one that is walking away after tatl pisses them off
        //[GroundVariants(0xFE08)]
        [EnemizerScenesPlacementBlock(Scene.AstralObservatory, Scene.Grottos, Scene.IkanaCastle,
            Scene.WestClockTown, Scene.EastClockTown, Scene.NorthClockTown, Scene.SouthClockTown, Scene.LaundryPool,
            Scene.TouristCenter, Scene.DekuKingChamber, Scene.DekuShrine, Scene.MountainSmithy, Scene.GoronGrave, Scene.GoronShrine, Scene.FishermansHut, Scene.ZoraHall, Scene.MarineLab, Scene.SecretShrine, Scene.IkanaCastle, Scene.IgosDuIkanasLair, Scene.SwordsmansSchool,
            Scene.TradingPost, Scene.BombShop, Scene.PotionShop, Scene.GoronShop, Scene.ZoraHallRooms, Scene.TreasureChestShop, Scene.SwampShootingGallery, Scene.TownShootingGallery, Scene.BeneathTheWell,
            Scene.HoneyDarling, Scene.PostOffice, Scene.MayorsResidence, Scene.StockPotInn
            )]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        Giant = 0x1DB, // En_Giant

        [ActorizerEnabled]
        [CheckRestricted(Scene.TwinIslands, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToGoronVillageWinterSmallSnowball1, Item.CollectablePathToGoronVillageWinterSmallSnowball2, Item.CollectablePathToGoronVillageWinterSmallSnowball3, //small
            Item.CollectablePathToGoronVillageWinterLargeSnowball1, Item.CollectablePathToGoronVillageWinterLargeSnowball2, Item.CollectablePathToGoronVillageWinterLargeSnowball3, // large
            Item.CollectablePathToGoronVillageWinterLargeSnowball4, Item.CollectablePathToGoronVillageWinterLargeSnowball5, Item.CollectablePathToGoronVillageWinterLargeSnowball6,
            Item.CollectablePathToGoronVillageWinterLargeSnowball7, Item.CollectablePathToGoronVillageWinterLargeSnowball8, Item.CollectablePathToGoronVillageWinterLargeSnowball9,
            Item.CollectablePathToGoronVillageWinterLargeSnowball10, Item.CollectablePathToGoronVillageWinterLargeSnowball11, Item.CollectablePathToGoronVillageWinterLargeSnowball12,
            Item.CollectablePathToGoronVillageWinterLargeSnowball13, Item.CollectablePathToGoronVillageWinterLargeSnowball14,
            Item.SongLullabyIntro
        )]
        [CheckRestricted(Scene.GoronVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectableGoronVillageWinterLargeSnowball1, Item.CollectableGoronVillageWinterLargeSnowball2, // small
            Item.CollectableGoronVillageWinterLargeSnowball3, Item.CollectableGoronVillageWinterLargeSnowball4,
            Item.CollectableGoronVillageWinterLargeSnowball5, Item.CollectableGoronVillageWinterLargeSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball1, Item.CollectableGoronVillageWinterSmallSnowball2, Item.CollectableGoronVillageWinterSmallSnowball3, // large
            Item.CollectableGoronVillageWinterSmallSnowball4, Item.CollectableGoronVillageWinterSmallSnowball5, Item.CollectableGoronVillageWinterSmallSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball7, Item.CollectableGoronVillageWinterSmallSnowball8, Item.CollectableGoronVillageWinterSmallSnowball9,
            Item.CollectableGoronVillageWinterSmallSnowball10)]
        /* [CheckRestricted(Scene.PathToMountainVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToMountainVillageSmallSnowball1, Item.CollectablePathToMountainVillageSmallSnowball2, // small
            Item.CollectablePathToMountainVillageSmallSnowball3, Item.CollectablePathToMountainVillageSmallSnowball4,
            // because it blocks access, at least check a few winter checks that dont require bombs or goron (single sphere influence)
            Item.ItemLens, Item.ChestLensCavePurpleRupee, Item.ChestLensCaveRedRupee,
            Item.ShopItemGoronRedPotion, Item.ShopItemGoronBomb10, Item.ShopItemGoronArrow10,
            Item.ItemTingleMapSnowhead, Item.ItemTingleMapRanch,
            Item.CollectableMountainVillageWinterPot1,
            Item.CollectableGoronVillageWinterSmallSnowball1, Item.CollectableGoronVillageWinterSmallSnowball2, Item.CollectableGoronVillageWinterSmallSnowball3, // large
            Item.CollectableGoronVillageWinterSmallSnowball4, Item.CollectableGoronVillageWinterSmallSnowball5, Item.CollectableGoronVillageWinterSmallSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball7, Item.CollectableGoronVillageWinterSmallSnowball8, Item.CollectableGoronVillageWinterSmallSnowball9,
            Item.CollectableMountainVillageWinterSmallSnowball1, Item.CollectableMountainVillageWinterSmallSnowball2, // small
            Item.CollectableMountainVillageWinterSmallSnowball3, Item.CollectableMountainVillageWinterSmallSnowball4,
            Item.CollectableMountainVillageWinterSmallSnowball5, Item.CollectableMountainVillageWinterSmallSnowball6,
            Item.CollectableMountainVillageWinterSmallSnowball7, Item.CollectableMountainVillageWinterSmallSnowball8
        )] // */
        [CheckRestricted(Scene.MountainVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectableMountainVillageWinterSmallSnowball1, Item.CollectableMountainVillageWinterSmallSnowball2, // small
            Item.CollectableMountainVillageWinterSmallSnowball3, Item.CollectableMountainVillageWinterSmallSnowball4,
            Item.CollectableMountainVillageWinterSmallSnowball5, Item.CollectableMountainVillageWinterSmallSnowball6,
            Item.CollectableMountainVillageWinterSmallSnowball7, Item.CollectableMountainVillageWinterSmallSnowball8,
            Item.CollectableMountainVillageWinterLargeSnowball1, Item.CollectableMountainVillageWinterLargeSnowball2,
            Item.CollectableMountainVillageWinterLargeSnowball3, Item.CollectableMountainVillageWinterLargeSnowball4,
            Item.SongLullabyIntro
        )]
        [CheckRestricted(Scene.PathToSnowhead, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToSnowheadSmallSnowball1, Item.CollectablePathToSnowheadSmallSnowball2, // small
            Item.CollectablePathToSnowheadSmallSnowball3, Item.CollectablePathToSnowheadSmallSnowball4,
            Item.CollectablePathToSnowheadLargeSnowball1, Item.CollectablePathToSnowheadLargeSnowball2, // large
            Item.CollectablePathToSnowheadLargeSnowball3, Item.CollectablePathToSnowheadLargeSnowball4
        )]
        [CheckRestricted(Scene.Snowhead, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSnowheadSmallSnowball1, Item.CollectableSnowheadSmallSnowball2, // small
            Item.CollectableSnowheadSmallSnowball3, Item.CollectableSnowheadSmallSnowball10,
            Item.CollectableSnowheadLargeSnowball1, Item.CollectableSnowheadLargeSnowball2, // large
            Item.CollectableSnowheadLargeSnowball3, Item.CollectableSnowheadLargeSnowball4,
            Item.CollectableSnowheadLargeSnowball5, Item.CollectableSnowheadLargeSnowball6
        )]
        [CheckRestricted(Scene.SnowheadTemple, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball1, Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball2, // small
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball3, Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball4,
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball5
        )]
        [FileID(437)]
        [ObjectListIndex(0xEF)]
        [SwitchFlagsPlacement(mask: 0x3F, shift: 0)]
        //ground variants
        [GroundVariants(
            0x0012, 0x7F3F, 0xFF00, 0x0, // path to mountain (small)
            0x240A, 0x250A, 0x260A, 0x270A, 0x280A, 0x290A, 0x2A0A, 0x2B0A, // path to mountain village
            0x0C0E, 0x2802, 0x2902, 0x2A0E, 0x2B0A, 0x2C0E, 0x2F0A, 0x300A,  // mountain village winter
            0x080E, 0x040E, 0x200E, 0x210A, 0x220E, 0x230E, 0x240E, 0x250E, 0x270E, // path to goron village winter (twin islands)
            0x2B0E, 0x2C0E, 0x2D0A, 0x2E0E, 0x2F0A, 0x300E, 0x310E, 0x320A, 0x330E, 0x340A, 0x350E, // small 20x360E, 0x370A, 0x380A, 0x3
            0x2002, 0x2202, 0x2402, 0x210E, 0x230E, 0x250E, // goron village winter
            0x600E, 0x610E, 0x620E, 0x630E, // path to snowhead
            0x240E, 0x250E, 0x260E, 0x270E, 0x280E, 0x290E, // snowhead
            0xB, 0xF // snowhead temple
        )]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.PathToMountainVillage)]
        [BlockingVariantsAll]
        [PlacementWeight(20)]
        LargeSnowball = 0x1DC, // Obj_Snowball

        [FileID(438)]
        [ObjectListIndex(0x1BB)]
        Goht = 0x1DD, // Boss_Hakugin

        [ActorizerEnabled]
        [FileID(439)]
        [ObjectListIndex(0x144)]
        [CheckRestricted(Scene.PoeHut, variant: ActorConst.ANY_VARIANT, // 0x0,
            Item.HeartPiecePoeHut)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max: 0, variant: 0x0)]
        [SwitchFlagsPlacement(mask: 0xFF, shift: 3)] // 0x7F8
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.IkanaCanyon, Scene.SecretShrine)]
        SpiritHouseOwner = 0x1DE, // En_Gb2

        // unused monkey instrument prompt
        [FileID(440)]
        [ObjectListIndex(0x1)]
        Unused_EnOnpuman = 0x1DF, // En_Onpuman

        [FileID(441)]
        [ObjectListIndex(0x1BF)]
        [DynaAttributes(6,8)]
        GoronShrineDoor = 0x1E0, // Bg_Tobira01

        // Unused seahourse spawner
        [FileID(442)]
        [ObjectListIndex(0x1)]
        Unused_EnTagObj = 0x1E1, // En_Tag_Obj

        [FileID(443)]
        [ObjectListIndex(0x1C1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        DinnerPlateProp = 0x1E2, // Obj_Dhouse

        [ActorizerEnabled]
        [FileID(444)]
        [ObjectListIndex(0x1C2)]
        [DynaAttributes(24,14)]
        // FFFF is extra?
        // 600 is night one, 702 is night 2, 801, is night 3
        // params: 0xFF00 is switch flags, 0xFF is text ID (hard coded)
        // 0x3 is talk point for captain keeta's sign, just the point though his sign is part of the ground mesh
        // only one 0x600 can exist without crashing
        //[GroundVariants(0xFFFF, 0x600, 0x702, 0x801)]
        [GroundVariants(0xFF00, 0xFF02, 0xFF01)]
        [WaterBottomVariants(0xFF00, 0xFF02, 0xFF01)]
        //[VariantsWithRoomMax(max:1, 0xFFFF, 0x600, 0x702, 0x801)]
        [OnlyOneActorPerRoom] // issue: three on redead in stonetower is crash, but not two, not worth issue
        [UnkillableAllVariants] // actually I'm not sure, it has health
        [BlockingVariantsAll]
        [ForbidFromScene(Scene.IkanaGraveyard)]
        [EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.SouthernSwamp)] // crash cause is unknown, mtx calculation crash
        //[SwitchFlagsPlacement(mask: 0xFF, shift: 8)] // not actually used by vanilla variants, we can ignore
        IkanaGravestone = 0x1E3, // Obj_Hakaisi

        [FileID(445)]
        [ObjectListIndex(0x1C7)]
        GoronLinkPoundSwitch = 0x1E4, // Bg_Hakugin_Switch

        Empty1E5 = 0x1E5,

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2EE0)]
        [ActorInstanceSize(0x378)]
        [FileID(446)]
        [ObjectListIndex(0x1C4)]
        [GroundVariants(0xFF00, 0xFF01, 0, 1)]
        [DifficultVariants(1,0xF001)]
        [VariantsWithRoomMax(max: 1, variant: 1, 0xF001)] // limit the bigger one
        Eeno = 0x1E6, // En_Snowman

        // gold skull bonk detector
        [FileID(447)]
        [ObjectListIndex(0x1)]
        SkulltulaBonkDetector = 0x1E7, // TG_Sw

        [EnemizerEnabled]
        [ActorInitVarOffset(0x36A0)]
        [FileID(448)]
        [ObjectListIndex(0x1C5)]
        //0x100 is red, 0x200 is blue, 0x300 is green, 00 is purple, however, its difficult to fight more than 2
        //[FlyingVariants(0x300, 0x200, 0x100)] // not meg because I think she shows up too frequently
        [GroundVariants(0x300, 0x200, 0x100, 0x000)]
        [RespawningVariants(0x0)] // meg does NOT respawn but her death doesnt always trigger kills rooms, not sure why
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 1, variant: 0x000, 0x100, 0x200, 0x300)] // only one per
        [CompanionActor(Flame, ourVariant: 0x000, variant: 0xD)]      // meg gets purple flames // NOT DONE
        [CompanionActor(Flame, ourVariant: 0x100, variant: 0x4, 0x5)] // jo gets red flames
        [CompanionActor(Flame, ourVariant: 0x200, variant: 0x7FE)]    // beth gets blue flames
        [CompanionActor(Flame, ourVariant: 0x300, variant: 0x3)]      // amy gets green flames
        // no scene exclusion necessary, get spawned by the poe sisters minigame but they aren't actors in the scene to be randomized
        [EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        [PlacementWeight(85)]
        PoeSisters = 0x1E8, // En_Po_Sisters

        [EnemizerEnabled]
        [ActorInitVarOffset(0x3794)]
        [FileID(449)]
        [ObjectListIndex(0x1C6)]
        [GroundVariants(0, 0x0101)] // 101 is non-armored version
        [DifficultVariants(0)]
        [EnemizerScenesPlacementBlock(Scene.TownShootingGallery)]
        Hiploop = 0x1E9, // En_Pp  // Charging beetle in Woodfall

        [FileID(450)]
        [ObjectListIndex(0x1BB)]
        GohtKickedUpRocks = 0x1EA, // En_Hakurock

        [FileID(451)]
        [ObjectListIndex(0x1)] // doubt
        Fireworks = 0x1EB, // En_Hanabi

        // rumble controller near it, good for hidden grottos
        [ActorizerEnabled]
        [FileID(452)]
        //[ObjectListIndex(0x1)]
        [ObjectListIndex(0x2)] // not actually limited, but we only use it for hidden grottos so its fieldkeep only
        // 0x7F are flags, whether treasure or switch flags, depending on type
        // type is >> 7, so 0x1F0 1 is collectible, 2 is chest, 3 is switch, think we want switch flag since those are much more open
        [GroundVariants(0x110)] // checks for switch flag
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)] // technically correct, the vanilla game never uses this actor
        Obj_Dowsing = 0x1EC, // Obj_Dowsing

        // wind in ISTT and water current in PFInterior
        [FileID(453)]
        [ObjectListIndex(0x1)]
        WindAndCurrent = 0x1ED, // Obj_Wind

        [ActorizerEnabled] // thank god for m2c
        [FileID(454)]
        [ObjectListIndex(0x132)]
        [PathingVariants(0x19F, 0xD9F, 0x3FF, 0x22BF,
            0x20, 0x40, 0x60, 0x80, 0x120)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [UnkillableAllVariants]
        RaceDog = 0x1EE, // En_Racedog

        [ActorizerEnabled] // does not spawn, again with the hardcoded nonsense
        [FileID(455)]
        [ObjectListIndex(0x10F)]
        [CheckRestricted(Item.HeartPieceSwordsmanSchool,
            Item.CollectableSwordsmanSSchoolPot1, Item.CollectableSwordsmanSSchoolPot2, Item.CollectableSwordsmanSSchoolPot3, Item.CollectableSwordsmanSSchoolPot4, Item.CollectableSwordsmanSSchoolPot5 )]
        [GroundVariants(0xFF01, // shivering at night
            0)] // regular
        [VariantsWithRoomMax(max:0, variant:0xFF01, 0)] // does not spawn, missing... objects?
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.SwordsmansSchool)] // dont remove
        KendoSensei = 0x1EF, // En_Kendo_Js

        [FileID(456)]
        [ObjectListIndex(0x1C9)]
        [DynaAttributes(19, 12)]
        CaptainKeepaGatePost = 0x1F0, // Bg_Botihasira

        [ActorizerEnabled]
        [FileID(457)]
        [ActorInstanceSize(0x2018)]
        [ObjectListIndex(0x1D7)]
        [CheckRestricted(Item.HeartPieceLabFish)]
        [WaterVariants(0)]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.MarineLab)]
        //[EnemizerScenesPlacementBlock(Scene.GreatBayCoast, Scene.ZoraCape)] // issue: if both fish and labfish spawn, they eat, and cutscene locks
        LabFish = 0x1F1, // En_Fish2

        [ActorizerEnabled]
        [ActorInitVarOffset(0xC68)]
        [FileID(458)]
        [ObjectListIndex(0x1CB)]
        // for now, with no entrando, just randomize all but one
        //[CheckRestricted(Item.TradeItemMamaLetter, Item.MaskKeaton, Item.HeartPiecePostBox, Item.MaskCouple)]
        [GroundVariants(0, 1, 2, 3, 4)]
        //[WaterBottomVariants(-1, -2, -3, -4)] // I want to put them on the bottom, but I dont want the game to think they are vanilla water either..
        [WaterBottomVariants(0x101, 0x102, 0x103, 0x104)] // I want to put them on the bottom, but I dont want the game to think they are vanilla water either..
        [CompanionActor(LetterToPostman, ourVariant: -1, variant: 0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        // I once got a crash putting type 1 into eastclock town, crashed on the schedule code
        // not sure, but might be beacuse we put the wrong one in the wrong area, TODO investigate
        [EnemizerScenesPlacementBlock(Scene.WestClockTown, Scene.SouthClockTown, Scene.NorthClockTown, Scene.EastClockTown)]
        //[ForbidFromScene(Scene.WestClockTown, Scene.SouthClockTown, Scene.NorthClockTown, Scene.EastClockTown)]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        Postbox = 0x1F2, // En_Pst

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2F70)]
        [FileID(459)]
        [ObjectListIndex(0x1C3)]
        [CheckRestricted(Item.BottleCatchPoe)] // only one in the istt
        [FlyingVariants(0x00FF)]
        // FF is in the game, in OOT 02 was a composer brother, but in MM 0-6 are the same as FF
        [GroundVariants(0x01FF)] // non-vanilla, params doesnt seem to matter for this actor
        [WaterBottomVariants(0x01FF)]
        [DifficultAllVariants]
        [CompanionActor(Flame, ourVariant: -1, 0x7FE)] // blue flames
        //[ForbidFromScene(Scene.InvertedStoneTowerTemple)] // only if the scoop check isnt working
        Poe = 0x1F3, // En_Poh

        [FileID(460)]
        [ObjectListIndex(0x1CD)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        Obj_Spidertent = 0x1F4, // Obj_Spidertent

        [ActorizerEnabled]
        [FileID(461)]
        [ObjectListIndex(0x1CE)]
        [CheckRestricted(Item.BottleCatchEgg)]
        // 2000 is hookshot room, 1A00 is twin barrel, 0x1E00 is barrel room, 1C00 is last room
        // the three at pinnacle rock are 0x1400, 0x1600, 0x1800
        // 0 loaded fine, what happens if we load smaller values? can we have 3 or more?
        [WaterBottomVariants(0x0, 0x1000, 0x1200, 0x1300, 0x1500, 0x1700, // non-vanilla
            0x1400, 0x1600, 0x1800, // pinnacle
            0x1C00, 0x1A00, 0x1E00, 0x2000)] // pirates
        [VariantsWithRoomMax(max: 1, variant: 0x0, 0x1000, 0x1200, 0x1300, 0x1500, 0x1700)]
        [VariantsWithRoomMax(max: 0, variant: 0x0, 0x1400, 0x1600, 0x1500, 0x1C00, 0x1A00, 0x1E00, 0x2000)]
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        [EnemizerScenesPlacementBlock(Scene.PinnacleRock)] // now that we have signs randomized, its almost garenteeded to happen
        ZoraEgg = 0x1F5, // En_Zoraegg

        [ActorizerEnabled]
        [FileID(462)]
        [ObjectListIndex(0x1CF)]
        [CheckRestricted(Scene.MountainSmithy, variant: ActorConst.ANY_VARIANT, Item.UpgradeRazorSword, Item.UpgradeGildedSword)]
        [GroundVariants( 0xFE1F, // credits
            0x200)] // vacation
        [VariantsWithRoomMax(max:8, variant:0x200)] // no reason except it can be creepy
        [UnkillableAllVariants]
        Zubora = 0x1F6, // En_Kbt

        [FileID(463)]
        [ObjectListIndex(0x1D0)]
        DarmanisGhost1 = 0x1F7, // En_Gg

        [FileID(464)]
        [ObjectListIndex(0x1D1)]
        SwordsmanSchoolLog = 0x1F8, // En_Maruta

        [ActorizerEnabled]
        [CheckRestricted(Scene.TwinIslands, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToGoronVillageWinterSmallSnowball1, Item.CollectablePathToGoronVillageWinterSmallSnowball2, Item.CollectablePathToGoronVillageWinterSmallSnowball3, //small
            Item.CollectablePathToGoronVillageWinterLargeSnowball1, Item.CollectablePathToGoronVillageWinterLargeSnowball2, Item.CollectablePathToGoronVillageWinterLargeSnowball3, // large
            Item.CollectablePathToGoronVillageWinterLargeSnowball4, Item.CollectablePathToGoronVillageWinterLargeSnowball5, Item.CollectablePathToGoronVillageWinterLargeSnowball6,
            Item.CollectablePathToGoronVillageWinterLargeSnowball7, Item.CollectablePathToGoronVillageWinterLargeSnowball8, Item.CollectablePathToGoronVillageWinterLargeSnowball9,
            Item.CollectablePathToGoronVillageWinterLargeSnowball10, Item.CollectablePathToGoronVillageWinterLargeSnowball11, Item.CollectablePathToGoronVillageWinterLargeSnowball12,
            Item.CollectablePathToGoronVillageWinterLargeSnowball13, Item.CollectablePathToGoronVillageWinterLargeSnowball14,
            Item.SongLullabyIntro
        )]
        [CheckRestricted(Scene.GoronVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectableGoronVillageWinterLargeSnowball1, Item.CollectableGoronVillageWinterLargeSnowball2, // small
            Item.CollectableGoronVillageWinterLargeSnowball3, Item.CollectableGoronVillageWinterLargeSnowball4,
            Item.CollectableGoronVillageWinterLargeSnowball5, Item.CollectableGoronVillageWinterLargeSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball1, Item.CollectableGoronVillageWinterSmallSnowball2, Item.CollectableGoronVillageWinterSmallSnowball3, // large
            Item.CollectableGoronVillageWinterSmallSnowball4, Item.CollectableGoronVillageWinterSmallSnowball5, Item.CollectableGoronVillageWinterSmallSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball7, Item.CollectableGoronVillageWinterSmallSnowball8, Item.CollectableGoronVillageWinterSmallSnowball9,
            Item.CollectableGoronVillageWinterSmallSnowball10)]
        [CheckRestricted(Scene.PathToMountainVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToMountainVillageSmallSnowball1, Item.CollectablePathToMountainVillageSmallSnowball2, // small
            Item.CollectablePathToMountainVillageSmallSnowball3, Item.CollectablePathToMountainVillageSmallSnowball4
            /*
            // because it blocks access, at least check a few winter checks that dont require bombs or goron (single sphere influence)
            Item.ItemLens, Item.ChestLensCavePurpleRupee, Item.ChestLensCaveRedRupee,
            Item.ShopItemGoronRedPotion, Item.ShopItemGoronBomb10, Item.ShopItemGoronArrow10,
            Item.ItemTingleMapSnowhead, Item.ItemTingleMapRanch,
            Item.CollectableMountainVillageWinterPot1,
            Item.CollectableGoronVillageWinterSmallSnowball1, Item.CollectableGoronVillageWinterSmallSnowball2, Item.CollectableGoronVillageWinterSmallSnowball3, // large
            Item.CollectableGoronVillageWinterSmallSnowball4, Item.CollectableGoronVillageWinterSmallSnowball5, Item.CollectableGoronVillageWinterSmallSnowball6,
            Item.CollectableGoronVillageWinterSmallSnowball7, Item.CollectableGoronVillageWinterSmallSnowball8, Item.CollectableGoronVillageWinterSmallSnowball9,
            Item.CollectableMountainVillageWinterSmallSnowball1, Item.CollectableMountainVillageWinterSmallSnowball2, // small
            Item.CollectableMountainVillageWinterSmallSnowball3, Item.CollectableMountainVillageWinterSmallSnowball4,
            Item.CollectableMountainVillageWinterSmallSnowball5, Item.CollectableMountainVillageWinterSmallSnowball6,
            Item.CollectableMountainVillageWinterSmallSnowball7, Item.CollectableMountainVillageWinterSmallSnowball8
            // */
        )]
        [CheckRestricted(Scene.MountainVillage, variant: ActorConst.ANY_VARIANT,
            Item.CollectableMountainVillageWinterSmallSnowball1, Item.CollectableMountainVillageWinterSmallSnowball2, // small
            Item.CollectableMountainVillageWinterSmallSnowball3, Item.CollectableMountainVillageWinterSmallSnowball4,
            Item.CollectableMountainVillageWinterSmallSnowball5, Item.CollectableMountainVillageWinterSmallSnowball6,
            Item.CollectableMountainVillageWinterSmallSnowball7, Item.CollectableMountainVillageWinterSmallSnowball8,
            Item.CollectableMountainVillageWinterLargeSnowball1, Item.CollectableMountainVillageWinterLargeSnowball2,
            Item.CollectableMountainVillageWinterLargeSnowball3, Item.CollectableMountainVillageWinterLargeSnowball4,
            Item.SongLullabyIntro
        )]
        [CheckRestricted(Scene.MountainVillageSpring, variant: ActorConst.ANY_VARIANT,
            Item.CollectableMountainVillageSpringSmallSnowball1, Item.CollectableMountainVillageSpringSmallSnowball2, // small
            Item.CollectableMountainVillageSpringSmallSnowball3, Item.CollectableMountainVillageSpringSmallSnowball4
        )]
        [CheckRestricted(Scene.PathToSnowhead, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePathToSnowheadSmallSnowball1, Item.CollectablePathToSnowheadSmallSnowball2, // small
            Item.CollectablePathToSnowheadSmallSnowball3, Item.CollectablePathToSnowheadSmallSnowball4,
            Item.CollectablePathToSnowheadLargeSnowball1, Item.CollectablePathToSnowheadLargeSnowball2, // large
            Item.CollectablePathToSnowheadLargeSnowball3, Item.CollectablePathToSnowheadLargeSnowball4
        )]
        [CheckRestricted(Scene.Snowhead, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSnowheadSmallSnowball1, Item.CollectableSnowheadSmallSnowball2, // small
            Item.CollectableSnowheadSmallSnowball3, Item.CollectableSnowheadSmallSnowball10,
            Item.CollectableSnowheadLargeSnowball1, Item.CollectableSnowheadLargeSnowball2, // large
            Item.CollectableSnowheadLargeSnowball3, Item.CollectableSnowheadLargeSnowball4,
            Item.CollectableSnowheadLargeSnowball5, Item.CollectableSnowheadLargeSnowball6
        )]
        [CheckRestricted(Scene.SnowheadTemple, variant: ActorConst.ANY_VARIANT,
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball1, Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball2, // small
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball3, Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball4,
            Item.CollectableSnowheadTempleIceBlockRoomSmallSnowball5
        )] // */
        [FileID(465)]
        [ObjectListIndex(0xEF)]
        [GroundVariants(
            0x7F3F, // multi
            0x203F, 0x210A, 0x220A, 0x230E, // path to mountain village
            0x200A, 0x210E, 0x220E, 0x230A, 0x240A, 0x250E, 0x260A,
            0x210E, 0x2222, 0x250A, 0x260A, 0x270E, 0x2D0F, 0x2E0F, 0x3415, 0x351F, 0x3610, 0x370F, // mountain village
            0x250A, 0x2915, 0x2A1F, 0x2B10, 0x2C0F,  // mountain village spring
            0x360E, 0x370A, 0x380E, 0x390E, 0x3A0A, 0x3B0A, // path to goron village (twin islands)
            0x290A, 0x2A0A, 0x2E0E, 0x320E, 0x2D13, 0x3113, 0x2B15, 0x3015, 0x2C19, 0x2F19, 0x331E, 0x341E, // goron village
            0x200F, 0x210F, 0x220F, 0x230F, // path to snowhead
            0x2015, 0x211F, 0x2210, 0x230F, 0x201F, 0x2110, 0x220F, 0x230A, 0x250E, 0x260A, 0x270E, 0x280E, 0x2915, // snowhead
            0xE, 0xA, 0xB, 0x1E, 0x201E, 0x211E, 0x2202, 0x2302, 0x2402 //snowhead temple
        )]
        //[SwitchFlagsPlacement()] // does not appear to have switch flags
        [UnkillableAllVariants]
        [PlacementWeight(30)]
        SmallSnowball = 0x1F9, // Obj_Snowball2

        [FileID(466)]
        [ObjectListIndex(0x1D0)]
        DarkmanisGhost2 = 0x1FA, // En_Gg2

        [ActorizerEnabled]
        [FileID(467)]
        [ObjectListIndex(0x1D2)]
        [DynaAttributes(34, 20)]
        // no params, again with the weird vanilla param data
        [GroundVariants(0xFF)]
        //[WaterBottomVariants(0x77)] // think this is an issue, getting weird waterblock crashes
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [CheckRestricted(Item.MaskGoron, Item.ChestHotSpringGrottoRedRupee,
            Item.UpgradeRazorSword, Item.UpgradeGildedSword,
            Item.BottleCatchHotSpringWater,
            Item.ItemPowderKeg)]
        [OnlyOneActorPerRoom] // dyna crash hazard
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        //[EnemizerScenesPlacementBlock(Scene.SouthernSwamp, Scene.SouthernSwampClear)] // dyna crash
        DarmaniGrave = 0x1FB, // Obj_Ghaka

        [ActorizerEnabled]
        [FileID(468)]
        [ObjectListIndex(0x1D4)]
        // we want to keep her object in the deku king chamber too
        [CheckRestricted(/*Scene.WoodfallTemple, variant:-1,*/ Item.BottleCatchPrincess, Item.MaskScents,
            Item.CollectableDekuShrineDekuButlerSRoomItem1, Item.CollectableDekuShrineDekuButlerSRoomItem2, Item.CollectableDekuShrineDekuButlerSRoomItem3,
            Item.CollectableDekuShrineDekuButlerSRoomItem4, Item.CollectableDekuShrineDekuButlerSRoomItem5, Item.CollectableDekuShrineDekuButlerSRoomItem6,
            Item.CollectableDekuShrineDekuButlerSRoomItem7, Item.CollectableDekuShrineDekuButlerSRoomItem8, Item.CollectableDekuShrineDekuButlerSRoomItem9,
            Item.CollectableDekuShrineDekuButlerSRoomItem10,
            Item.CollectableDekuShrineGreyBoulderRoomPot1,
            Item.CollectableDekuShrineGiantRoomFloor1Item1, Item.CollectableDekuShrineGiantRoomFloor1Item2,
            Item.CollectableDekuShrineGiantRoomFloor1Item3, Item.CollectableDekuShrineGiantRoomFloor1Item4,
            Item.CollectableDekuShrineGiantRoomFloor1Item5, Item.CollectableDekuShrineGiantRoomFloor1Item6,
            Item.CollectableDekuShrineGiantRoomFloor1Item7, Item.CollectableDekuShrineGiantRoomFloor1Item8,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem1, Item.CollectableDekuShrineRoomBeforeFlameWallsItem2,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem3, Item.CollectableDekuShrineRoomBeforeFlameWallsItem4,
            Item.CollectableDekuShrineRoomBeforeFlameWallsItem5, Item.CollectableDekuShrineRoomBeforeFlameWallsItem6,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem1, Item.CollectableDekuShrineWaterRoomWithPlatformsItem2,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem3, Item.CollectableDekuShrineWaterRoomWithPlatformsItem4,
            Item.CollectableDekuShrineWaterRoomWithPlatformsItem5, Item.CollectableDekuShrineWaterRoomWithPlatformsItem6)]
        // 0 is inside of tree
        // 2 is post-woodfall sitting in royal chamber, does not spawn until after a flag is set
        [GroundVariants(
            0x0, // regular one in the tree
            0x2 // credits cutscene
            )]
        [OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max:0,
            variant:0x2 // doesn't spawn until you save monkey
            )]
        [UnkillableAllVariants]
        //[ForbidFromScene(/*Scene.WoodfallTemple,*/ Scene.DekuKingChamber)] // if her object is not in the king chamber no cutscene after bottle delivery
        [PlacementWeight(80)]
        DekuPrincess = 0x1FC, // En_Dnp

        [ActorizerEnabled]
        [FileID(469)]
        [ObjectListIndex(0x1D5)]
        [GroundVariants(0xFFFF)]
        [VariantsWithRoomMax(max: 0, variant: 0xFFFF)]
        [ForbidFromScene(Scene.Snowhead)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        SnowheadBiggoron = 0x1FD, // En_Dai

        // visible glitch when clashing with existing water means seizure like effect, bad until I can remove that
        //[ActorizerEnabled]
        [FileID(470)]
        [ObjectListIndex(0x1D3)]
        //[DynaAttributes(0,0)] // this is a water box type dyna, we dont care about these
        // 0 starts the cutscene, 1 is after cutscene
        [WaterTopVariants(0x1)]
        [UnkillableAllVariants]
        [OnlyOneActorPerRoom]
        GoronHotSpringWater = 0x1FE, // Bg_Goron_Oyu

        [ActorizerEnabled]
        [FileID(471)]
        [ObjectListIndex(0x1D6)]
        [CheckRestricted(Scene.MountainSmithy, variant: ActorConst.ANY_VARIANT, Item.UpgradeRazorSword, Item.UpgradeGildedSword)]
        [GroundVariants( 0xFE1F, // credits in milkbar
            0x200)] // regular
        //[VariantsWithRoomMax(max:0, variant: 0x200)] // suspected double actor, without the other I doubt this works at all
        [SwitchFlagsPlacement(mask: 0xFE, shift: 9)]
        [PathingKickoutAddrVarsPlacement(mask:0x1F, shift:0)] // why oh why did this stupid actor need a selectable exit kickout
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(Scene.MilkBar)] // can interupt balad of the windfish performance
        [UnkillableAllVariants]
        [PlacementWeight(80)]
        GaboraBlacksmith = 0x1FF, // En_Kgy

        // the wackest actor that controls the whole alien invasion event, and a lot of stuff at ranch
        [FileID(472)]
        [ObjectListIndex(0x1)] // multiple object
        AllAlienEventActors = 0x200, // En_Invadepoh

        [ActorizerEnabled]
        [FileID(473)]
        [ObjectListIndex(0x1DF)]
        [CheckRestricted(Scene.GoronShrine, variant: ActorConst.ANY_VARIANT, // 0x3FF1,
            Item.SongLullaby, Item.MaskDonGero,
            Item.SongLullabyIntro)] // have to talk to kid to get intro from leader
        [CheckRestricted(Scene.GoronRacetrack, variant: ActorConst.ANY_VARIANT, // 0x3FF1,
            Item.ItemBottleGoronRace)]
        [GroundVariants(
            0x1400, // regular one in the shrine throne room
            // 0x1402, // loud one you can hear making sfx from the main room of shrine
            0x1401, // standing around in spring, does not spawn without dungeon clear (why tho, you cannot reach spring without dungeon clear...)
            0x3FF1, // race starter
            0x3F00)] // cutscene version, if spawned in world just cries like normal and talkable
        [PerchingVariants(0x1400)] // regular one in the shrine should be crying in a tree
        // 0x3FF1 does not spawn in winter, even in other scenes
        //[GroundVariants(0x3FF1)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        // in 1.16 this was rescinded, we can now place it in the world again
        [VariantsWithRoomMax(max: 0, variant: 0x1401, 0x3FF1, 0x1402)] // softlock if you enter the song teach cutscene, which in rando is proximity
        //VariantsWithRoomMax(max: 0, variant: 0x1400)] // holy shit this is annoying nvm
        //[ForbidFromScene(Scene.GoronShrine, Scene.GoronRacetrack, Scene.TwinIslandsSpring)]
        [SwitchFlagsPlacement(mask: 0x3F, shift: 8)]
        [PlacementWeight(77)]
        GoronKid = 0x201, // En_Gk, baby goron, child goron

        [ActorizerEnabled]
        [FileID(474)]
        [ActorInstanceSize(0x3C8)]
        [ObjectListIndex(0xE2)]
        // to nuke ONLY in stockpot, hardcoded
        [CheckRestricted(Scene.StockPotInn, variant: ActorConst.ANY_VARIANT,
            Item.TradeItemRoomKey, Item.TradeItemKafeiLetter, Item.MaskCouple,
            Item.NotebookMeetAnju, Item.NotebookInnReservation, Item.NotebookPromiseAnjuDelivery,
            Item.NotebookPromiseAnjuMeeting, Item.NotebookDeliverPendant, Item.NotebookUniteAnjuAndKafei)]
        [CheckRestricted(Scene.LaundryPool, variant: ActorConst.ANY_VARIANT, //0x8001,
            Item.NotebookMeetAnju)]
        [CheckRestricted(Scene.RanchBuildings, variant: ActorConst.ANY_VARIANT, // 0x8001,
            Item.NotebookMeetAnju)]
        // 8001 is pathing to laundrypool, also sitting on bed in ranch day 3
        [GroundVariants(2 // inn
            )]
        [PathingVariants(0x8001)] // really a pathing variant (walking through east/south to go see the laundry pool
        [PathingTypeVarsPlacement(mask: 0xFF, shift: 0)]
        [VariantsWithRoomMax(max: 0, variant: 0x8001)] // too hard coded to do anything with
        [VariantsWithRoomMax(max: 0, variant: 2)] // too hard coded to do anything with
        // dont remove from laundrypool, its the only way to see link mask in the wild, and its a trip
        [ForbidFromScene(Scene.LaundryPool/*, Scene.StockPotInn */)]
        [UnkillableAllVariants]
        Anju = 0x202, // En_An

        Empty203 = 0x203,

        [EnemizerEnabled]
        [ActorInitVarOffset(0xAD4)]
        [FileID(475)]
        [ObjectListIndex(0x1EB)]
        // 0 is the lame bee that just spins in circles, 1/2 are aggressive and charge at you
        [FlyingVariants(0, 1, 2, 3, 4, 5)] // vanilla 0 in mountain village
        [GroundVariants(0, 1, 2, 3, 4, 5)]
        [VariantsWithRoomMax(max: 4, variant: 0, 1, 2, 3, 4, 5)]
        [RespawningVariants(0)] // marked respawned to avoid: being placed on flying fairy enemy, because it doesnt come down, and boss rooms (boring)
        [ForbidFromScene(Scene.PiratesFortressRooms)] // pirate beehive cutscene
        [PlacementWeight(85)]
        GiantBeee = 0x204, // En_Bee

        [ActorizerEnabled]
        [FileID(476)]
        [CheckRestricted(Item.HeartPieceSeaHorse, Item.SongNewWaveBossaNova,
            Item.ChestPinacleRockRedRupee1, Item.ChestPinacleRockRedRupee2,
            Item.CollectablePinnacleRockPot1, Item.CollectablePinnacleRockPot2,
            Item.CollectablePinnacleRockPot3, Item.CollectablePinnacleRockPot4)]
        [ObjectListIndex(0x1EC)]
        [WaterBottomVariants(0x2001)]
        [VariantsWithRoomMax(max: 0, variant: 0x2001)] // placement gets conflicated TODO figure out
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 3)] // 0x3F8
        Seahorse = 0x205, // En_Ot

        // this is spawned by another actor apparently, dont use this
        [FileID(477)]
        [ObjectListIndex(0x1ED)]
        DeepPython = 0x206, // En_Dragon

        // bad for ground variant because this actor only draws chain + gong, not the frame, that is part of the scene mesh
        [ActorizerEnabled] // spawns but invisible, can hit it but cannot see it in TF
        // hmm, sword school special object is dungeon_keep
        [FileID(478)]
        [ObjectListIndex(0x1EE)]
        [WallVariants(0)] // none
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.SwordsmansSchool)] // object used for multiple actors
        Gong = 0x207, // Obj_Dora

        [EnemizerEnabled]
        [FileID(479)]
        [ObjectListIndex(0x1F1)]
        [CheckRestricted(Scene.BeneathTheWell, variant: ActorConst.ANY_VARIANT, // 0xFF00,
            Item.BottleCatchBigPoe)]
        [CheckRestricted(Scene.DampesHouse, variant: ActorConst.ANY_VARIANT, // 0xFF01,
            Item.BottleCatchBigPoe, Item.ItemBottleDampe)]
        // params: 0xFF00 is switch flags, if switch flag is exactly 0xFF then switch flags are ignored
        //    0xFF is type, where 0 is well, 1 is suppmoned in dampe house, 2/3/4 are dampe fire subtypes
        // we should be able to use 0xFF00... but rando changes something that makes dampe po spawn instant and well po has a cutscene
        [FlyingVariants(0xFF01, // vanilla
            0x1, 0x0102, // damps house
            0xFF00, // well poe
            0x1301)] // not vanilla, maybe we can actually place killable variants?
        [GroundVariants(0xFF01,
                        0x1301)] // not vanilla, maybe we can actually place killable variants?
        //[ForbidFromScene(Scene.BeneathTheWell, Scene.DampesHouse)] // well and dampe house must be vanilla for scoopsanity
        //[OnlyOneActorPerRoom]
        [DifficultAllVariants]
        [VariantsWithRoomMax(max: 0, variant: 0xFF00, 0x1, 0x0102)]
        [VariantsWithRoomMax(max: 1, variant: 0xFF01,
            0x1301)] // non-vanilla
        //[RespawningVariants(0x1301)] // if we got killable versions working, this would go back on
        //[UnkillableAllVariants] // only 1, the one with a no-respawn flag, spawns readily, so for now, assume the player kills one and can't kill another
        [CompanionActor(Flame, ourVariant: -1, 0x7FE)] // blue flames for ghast
        [EnemizerScenesPlacementBlock(Scene.TerminaField, // very annoying
            Scene.SwampSpiderHouse, Scene.OceanSpiderHouse, // annoying
            // TODO how old is this? is this before I knew about the cutscene version?
            Scene.SouthernSwamp, Scene.StoneTower)] // they either dont spawn, or when they appear they lock your controls, bad
        [SwitchFlagsPlacement(mask: 0xFF, shift: 8)]
        [PlacementWeight(55)]
        BigPoe = 0x208, // En_Bigpo

        // this is the "door" sign that you cut to find him final night, this is NOT the kanban he puts out saying hes gone away
        [ActorizerEnabled]
        [FileID(480)]
        [ObjectListIndex(0x1EE)]
        [WallVariants(0)]
        //[GroundVariants(0)] // vanilla, low to the ground
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.SwordsmansSchool)] // object is also used for gong, messes with rupee rando
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
        [ObjectListIndex(0x26A)] // ocean spiderhouse version
        //[ObjectListIndex(0xD9)] // swamp spiderhouse version
        //[CheckRestricted(Scene.SwampSpiderHouse, variant: 0xFF02, Item.MaskTruth)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: ActorConst.ANY_VARIANT, // 0xFE04,
            Item.UpgradeGiantWallet, Item.MundaneItemOceanSpiderHouseDay2PurpleRupee, Item.MundaneItemOceanSpiderHouseDay3RedRupee)]
        [CheckRestricted(Scene.OceanSpiderHouse, variant: ActorConst.ANY_VARIANT, // 0xFE05,
            Item.UpgradeGiantWallet, Item.MundaneItemOceanSpiderHouseDay2PurpleRupee, Item.MundaneItemOceanSpiderHouseDay3RedRupee)]
        //[CheckRestricted(Item.MaskTruth)] // but we want to only change the one that is spiderhouse... hardcoded
        // 0xFF02 is cured swamp spiderhouse (Object: 0xD9)
        // FE04/5 is oceanspiderhouse before and after wallet
        // FE03 is in SCT, he stares up at the moon, except doesn't know where the moon is, can face the wrong way
        // FE01 doesn't want to spawn, hmm, 02 is swamp spiderhouse, likely doesn't want to spawn either until house is cleared
        [GroundVariants(0xFE03, // staring at the moon
            0xFF02, // swamp spiderhouse
            0xFE04, 0xFE05)] // ocean spiderhouse
        [VariantsWithRoomMax(max: 0, variant: 0xFF02, 0xFE04, 0xFE05)] // temp disable, as double object actor is broken
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        // now that he gives hints we cannot remove him
        // his INTRO version is a fakeout now, handled in :: SwapIntroSeth()
        [ForbidFromScene(Scene.SouthClockTown)]
        [PlacementWeight(80)]
        // looking at moon, don't place him underground
        //[EnemizerScenesPlacementBlock(Scene.Grottos, Scene.InvertedStoneTower, Scene.BeneathGraveyard, Scene.BeneathTheWell,
        //    Scene.GoronShrine, Scene.IkanaCastle, Scene.OceanSpiderHouse, Scene.SwampSpiderHouse,
        //    Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.InvertedStoneTowerTemple, Scene.Woodfall)]
        Seth1 = 0x20B, // En_Sth, the green shirt guy from SCT that runs to Oceanspiderhouse to hide, also cured guy in Swamp spiderhouse

        // apparently this is what spawns the dragons
        [ActorizerEnabled]
        [FileID(483)]
        [ObjectListIndex(0x1F4)] //???
        //[Extra Object: Dragon]
        [CheckRestricted(Item.HeartPieceSeaHorse)]
        [DynaAttributes(2,4)] // ??
        [WaterVariants(0,1,2,3,4,5,6,7)]
        [VariantsWithRoomMax(max:0, variant: 0, 1, 2, 3, 4, 5, 6, 7)] // dont place it would be really stupid I bet
        DragonSpawner = 0x20C, // Bg_Sinkai_Kabe

        [FileID(484)]
        [ObjectListIndex(0x1E0)]
        [DynaAttributes(2,4)]
        FlatsTombCurtain = 0x20D, // Bg_Haka_Curtain

        //[ActorizerEnabled] // TODO disable, only enabled so I can test dyna
        [FileID(485)]
        [ObjectListIndex(0x1F5)]
        [DynaAttributes(3,5)] // weird
        OceanSpiderhouseBombableWall = 0x20E, // Bg_Kin2_Bombwall

        [FileID(486)]
        [ObjectListIndex(0x1F5)]
        [DynaAttributes(2,4)]
        OceanSpiderhouseGrate = 0x20F, // Bg_Kin2_Fence

        [ActorizerEnabled]
        [FileID(487)]
        [ObjectListIndex(0x1F5)]
        [DynaAttributes(2, 4)]
        // only 3FC is used, some sort of flag
        [WallVariants(0x3F)] // 3F has no cutscene, no camera concerns
        [GroundVariants(0x803F)] // kinda silly
        [WaterBottomVariants(0x803F)] // kinda silly
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.OceanSpiderHouse)] // object is shared with multiple actors in this scene, breaks whole area to remove
        [TreasureFlagsPlacement(mask: 0x7F, shift: 2)]
        [PlacementWeight(90)]
        SkullKidPainting = 0x210, // Bg_Kin2_Picture

        [ActorizerEnabledFreeOnly] // big object, boring actor
        [FileID(488)]
        [ObjectListIndex(0x1F5)]
        [DynaAttributes(18, 12)]
        [GroundVariants(0x1, // big shelf
            0x0)] // little shelf
        [BlockingVariants(0x1)]
        [VariantsWithRoomMax(max:2, variant: 0, 1)] // dyna I think, have to limit for now
        [ForbidFromScene(Scene.OceanSpiderHouse)] // object is shared with multiple actors in this scene, breaks whole area to remove
        [UnkillableAllVariants]
        OceanSpiderhouseMovableShelf = 0x211, // Bg_Kin2_Shelf // tag: bookshelf

        // kinda want to try randomizing, but I need to check against ALL checks in the graveyard, kinda hard to do
        [FileID(489)]
        [ObjectListIndex(0x142)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        StalChildrenCircle = 0x212, // En_Rail_Skb

        [ActorizerEnabled]
        [FileID(490)]
        [ObjectListIndex(0x1F8)]
        // his actor isnt here, gets spanwed by snowball, this is just for refernece
        [CheckRestricted(Scene.TwinIslands, variant: ActorConst.ANY_VARIANT, Item.SongLullabyIntro)]
        // 1 is standing in the hall during spring
        [GroundVariants(1)]
        [OnlyOneActorPerRoom]
        [ForbidFromScene(Scene.GoronShrine)] // remove and it crashes, dont know why (suspected missing object actor that needs it)
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        GoronElder = 0x213, // En_Jg
        
        //[ActorizerEnabled]
        [FileID(491)]
        [ObjectListIndex(0x18E)]
        [CheckRestricted(scene: Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT, Item.HeartPieceBoatArchery)]
        [UnkillableAllVariants]
        BoatArcheryKoume = 0x214, // En_Tru_Mt

        // multi-object actor with annoying crashing characteristics, leave alone
        [FileID(492)]
        [ObjectListIndex(0x1FC)]
        CreamiaCariage = 0x215, // Obj_Um

        [EnemizerEnabled]
        [ActorInitVarOffset(0x1C50)]
        [FileID(493)]
        [ObjectListIndex(0x201)]
        [GroundVariants(0xFF, 0x80FF)] // does this include the really big one?
        [PlacementWeight(90)]
        Leever = 0x216, // En_Neo_Reeba

        // unused actor, the object is loaded into milkbar but the actor is never spawned
        [ActorizerEnabled]
        [FileID(494)]
        [DynaAttributes(20,14)]
        [ObjectListIndex(0x202)]
        [GroundVariants(0)] // no params
        //[VariantsWithRoomMax(max:10, variant:0)]
        [UnkillableAllVariants]
        [PlacementWeight(45)] // new actor, for now lets leave high
        MilkbarChairs = 0x217, // Bg_Mbar_Chair

        [FileID(495)]
        [ObjectListIndex(0x3)]
        StoneTowerRotatingRoomPushBlock = 0x218, // Bg_Ikana_Block

        [ActorizerEnabled]
        [FileID(496)]
        [ObjectListIndex(0x203)]
        [DynaAttributes(30,20)]
        [WallVariants(0)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple)] // usually needed to complete the temple, and we cannot right now detect if light arrow is used
        [EnemizerScenesPlacementBlock(//Scene.IkanaGraveyard, // too much dyna
                                      Scene.TerminaField)] // no place to put, just wastes generation time
        StoneTowerMirror = 0x219, // Bg_Ikana_Mirror

        [FileID(497)]
        [ObjectListIndex(0x203)]
        // double because fuck you, gonna have to do it manually
        //[SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        //[SwitchFlagsPlacement(mask: 0xFE, shift: 0)]
        StoneTowerFlippingRoom = 0x21A, // Bg_Ikana_Rotaryroom

        // todo
        [FileID(498)]
        [ObjectListIndex(0x184)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        GBTSeaSaw = 0x21B, // Bg_Dblue_Balance

        // lol
        [ActorizerEnabled]
        [FileID(499)]
        [ObjectListIndex(0x184)]
        [CeilingVariants(0x3)] // the one in greatbay temple above the door, just going to use one
        [SwitchFlagsPlacement(mask:0x7F, shift:0)]
        [ForbidFromScene(Scene.GreatBayTemple)]
        [UnkillableAllVariants]
        GBTFreezableWaterfall = 0x21C, // Bg_Dblue_Waterfall

        //[EnemizerEnabled] // cutscene is broken without camera placement, player stuck in place
        [FileID(500)]
        [ObjectListIndex(0x204)]
        //[GroundVariants(0x24B)] // 3 different versions
        [GroundVariants(0x24B)]
        //[ForbidFromScene(0x23)] // do not remove original, for now
        PirateColonel = 0x21D, // En_Kaizoku

        // TODO make the one that just looks at you a non-enemy type in the replacement
        [ActorizerEnabled]
        [FileID(501)]
        [ObjectListIndex(0x12E)]
        [CheckRestricted(Scene.PiratesFortressRooms, variant: ActorConst.ANY_VARIANT, Item.HeartPiecePiratesFortress)]
        // vanilla variants:
        // Aviels room: 0xCB1
        // path == 0x3F ignores path, just stands in one spot
        // in testing, 2 is not bonkable... so why does FC01 not bonk?, should be type 0
        // 0xE0 >> 5 is type
        // 0 GERUDO_PURPLE_TYPE_CUTSCENE, // CANNOT BONK in some cases
        // 1 (2) GERUDO_PURPLE_TYPE_BOAT_SENTRY, //!< on boats
        // 2 (4) GERUDO_PURPLE_TYPE_AVEIL_GUARD,
        // 7 (E) GERUDO_PURPLE_TYPE_FORTRESS = 7 //!< In both courtyard and rooms
        // 0x02 always looks forward for boats or something, FC00 will hear you and turn to look at you
        [PathingVariants(0x1F, 0xEA, 0x04EA, 0x81F, 0x8EA, 0xC1F, 0xCEA, 0x101F, 0x104B, 0x10EA,
                0x14EA, 0x18EA,
            0x284B, 0x144B, // hookshotroom
            0x28EB, 0x30EB, 0x34EB, 0x38EB, 0x3CEB, 0x4C24)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        //[GroundVariants(0xFC20, 0xFC40 /*, 0xFCE0 */)]
        [VariantsWithRoomMax(max: 0, variant: 0x4C24, 0xFC00, 0xFC01, 0x81F, 0xC1F)] // only type 7 (0xE0) should be bonkable
        [VariantsWithRoomMax(max: 1, variant: 0xFC00)]
        [VariantsWithRoomMax(max: 1, variant: 0x1F, 0xEA, 0x04EA, 0x8EA, 0xCEA, 0x101F, 0x104B, 0x10EA,
                0x144B, 0x14EA, 0x18EA, 0x284B, 0x28EB, 0x30EB, 0x34EB, 0x38EB, 0x3CEB, 0x4C24)]
        // if kickout is 1F it does nothing? interesting
        [PathingKickoutAddrVarsPlacement(mask: 0x1F, shift: 0x0)]
        [RespawningAllVariants] // think they count as enemy, but they dont die they get back up, so can't put places
        //[ForbidFromScene(Scene.PiratesFortressRooms)] // because the ones in the hookshot room need to stay around
        // getting kicked out is a pain
        [EnemizerScenesPlacementBlock(Scene.SouthClockTown,
            Scene.SwampSpiderHouse, Scene.MayorsResidence, Scene.RanchBuildings,
            Scene.DekuPlayground, Scene.DekuShrine, Scene.TradingPost)]
        PatrollingPirate = 0x21E, // En_Ge2

        [ActorizerEnabled] // romani talking to cremia and dinner and sleeping in bed
        [FileID(502)]
        [ObjectListIndex(0xB7)]
        //[CheckRestricted(Scene.RanchBuildings, variant:ActorConst.ANY_VARIANT, check: Item.Notebook)]
        // 0xF000 is type, 0x00FF range is ignored by her actual code?
        // 0xF000 is just standing there, wont talk or do anything, it falls under "default"
        // 0x1000 is sitting at a table, we want to replace with the mmra version that comes with a box
        // 0x2000 is asleep night 2 in bed
        // 0x3000 is credits cutscene, except without the cutscene they just stare in a straight line
        //[GroundVariants(0x1000, 0x2000, 0xF000, 0x30FF)]
        [PerchingVariants(0x1000)] // chair is a type of perch
        [VariantsWithRoomMax(max: 1, variant: 0x2000)] // one asleep to hint the others are out of body
        [VariantsWithRoomMax(max: 0, variant: 0x30FF)] // boring cutscene version just stares into the distance, do not re-shuffle
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.RanchBuildings)]
        [EnemizerScenesPlacementBlock(Scene.MountainVillageSpring)] // her new actor sings, this can break frog choir if close enough
        RomaniYts = 0x21F, // En_Ma_Yts

        // todo flesh this actor out
        [ActorizerEnabled]
        [FileID(503)]
        [ObjectListIndex(0xA7)]
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT, //  0x40FF,
            Item.MaskRomani,
            Item.NotebookMeetCremia, Item.NotebookDefeatGormanBrothers, Item.NotebookProtectMilkDelivery)]
        [CheckRestricted(Scene.RomaniRanch, variant: ActorConst.ANY_VARIANT, Item.MaskRomani,
            Item.NotebookMeetCremia, Item.NotebookDefeatGormanBrothers, Item.NotebookProtectMilkDelivery)]
        [CheckRestricted(Scene.RanchBuildings, variant: ActorConst.ANY_VARIANT, Item.MaskRomani,
            Item.NotebookMeetCremia, Item.NotebookDefeatGormanBrothers, Item.NotebookProtectMilkDelivery)]
        // 0x10FF and 0x11FF are in barn, dialogue focused and timegated i bet
        // 0x20FF is in the homestead, sitting at table? probably timegated 
        [GroundVariants(0, // standing around day 1 is type 0
            0x40FF, // wedding
            0x30FF, // standing in front of ranch, final night walking?
            0x00FF)] // bottom 0xFF is unknown, not used in code?
        [PerchingVariants(0x20FF)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 0, 0, 0x00FF, 0x30FF)]
        [VariantsWithRoomMax(max: 1, 0x40FF)]
        [PlacementWeight(50)]
        Cremia = 0x220, // En_Ma_Yto

        [FileID(504)]
        [ObjectListIndex(0x205)]
        [DynaAttributes(10,8)] // has two, the other is 8/8
        SCTPillar = 0x221, // Obj_Tokei_Turret

        // the elevator that raise you out of water, cycling up and down
        [FileID(505)]
        [ObjectListIndex(0x184)]
        GreatBayTempleElevator = 0x222, // Bg_Dblue_Elevator

        // lame: saves take you to the real spawn, owl soar takes you to the real spawn, this only lets us activate and save warp
        [ActorizerEnabled]
        [FileID(506)]
        [ObjectListIndex(0x170)]
        // not dyna?
        // 0 is great bay coast, 1 is cape, 2 is snowhead, 3 is mountain village, 4 is SCT,
        //5 is milk road, 6 is woodfall, 7 is southern swamp, 8 is ikana canyon, 9 is stonetower
        // F is WCT, is also found in woodfall, cleared swamp?
        [GroundVariants(0, 1, 3, 2, 6, 5, 7, 8, 9, 0xF)]
        [WaterBottomVariants(0, 1, 3, 2, 6, 5, 7, 8, 9, 0xF)] // already have vanilla replacement blocked
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [ForbidFromScene(Scene.SouthClockTown, Scene.MilkRoad, Scene.WestClockTown,
             Scene.Woodfall, Scene.SouthernSwamp, Scene.SouthernSwampClear, Scene.MountainVillage, Scene.MountainVillageSpring, Scene.Snowhead,
             Scene.GreatBayCoast, Scene.ZoraCape, Scene.IkanaCanyon, Scene.StoneTower, Scene.InvertedStoneTower)]
        [PlacementWeight(90)]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard)] // assumed dyna overflow
        OwlStatue = 0x223, // Obj_Warpstone

        [ActorizerEnabled]
        [FileID(507)]
        [ObjectListIndex(0x206)]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT, Item.MaskZora)]
        [WaterTopVariants(0x80F, 0xC0F, 0x100F)]
        [VariantsWithRoomMax(max:0, variant: 0x80F, 0xC0F, 0x100F)] // do not place, they are pathing types
        [UnkillableAllVariants]
        Mikau = 0x224, // En_Zog

        [FileID(508)]
        [ObjectListIndex(0x207)]
        DekuMoonTrialRotatingPlatforms = 0x225, // Obj_Rotlift

        [FileID(509)]
        [ObjectListIndex(0x1F8)]
        GoronElderDrum = 0x226, // Obj_Jg_Gakki

        [FileID(510)]
        [ObjectListIndex(0x20C)]
        TwinmoldArenaController = 0x227, // Bg_Inibs_Movebg

        [ActorizerEnabled]
        [FileID(511)]
        [ObjectListIndex(0xD0)]
        // 0xXX00 is path, 0x00YY is type
        // type 0 is the one asking if you got a bottle from beavers on beach
        // 2 is standing guard in front of mikaus room
        // 3/4 probably also guarding door
        // 5 is creep trying to break into lulus room
        // 6 is sitting waiting for the rehersal
        // FC08 is the guitar tuner, FC07 is the picture buyer
        // 09 is lights are off guy
        // 0x12 is missing, checks for a flag makes snese
        // 0x13/14/15 is jamming at the jazz session cutscene
        // 0x140A is near the entrance
        // FC11 failed to spawn, TODO lookup what it is supposed to be doing
        // removed 0xFC07 because I cannot right now stop them being placed and randoed at the same time (creeper)
        [WaterBottomVariants(0xFC00, 0xFC08, 0xFC06, 0xFC13, 0xFC14, 0xFC15)] // no reason we cant talk to them underwater I dont think
        [GroundVariants( 0xFC08, 0xFC13, 0xFC14, 0xFC15)]
        [PerchingVariants(0xFC06, 0xFC0B, 0xFC0C, 0xFC0D, 0xFC0E, 0xFC0F, 0xFC10, 0xFC11 )] // on cliff edge
        //[SittingType]
        // TODO finish making these both underwater and above water where possible
        [PathingVariants(0x140A, 0xFC05, 0x2, 0x3, 0x4, 0x416, 0x16, 0x816)]
        [PathingTypeVarsPlacement(mask: 0xFC00, shift: 10)]
        [VariantsWithRoomMax(max: 1, variant: 0x140A, 0xFC05, 0x2, 0x3, 0x4)]
        [VariantsWithRoomMax(max: 1, variant: 0xFC08, 0xFC07, 0xFC06, 0xFC13, 0xFC14, 0xFC15, 0xFC00)]
        [ForbidFromScene(Scene.ZoraCape)]//, Scene.ZoraHall)]
        [PlacementWeight(80)]
        [UnkillableAllVariants]
        RegularZora = 0x228, // En_Zot

        // north clocktown tree
        [ActorizerEnabled]
        [FileID(512)]
        [ObjectListIndex(0x20D)]
        [DynaAttributes(31,18)] // this is just the top of the tree, the bottom is a soft collider
        // both 0 and 0xFF on oposite sides
        [CheckRestricted(Scene.NorthClockTown, variant: ActorConst.ANY_VARIANT, // 0x0
            Item.HeartPieceNorthClockTown)]
        [GroundVariants(0x0, 0xFF, 0x80FF)]
        [VariantsWithRoomMax(max: 1, variant: 0x0, 0xFF, 0x80FF)]
        //[EnemizerScenesPlacementBlock(//Scene.SouthernSwampClear,// known dyna issues
        //    Scene.StoneTower, Scene.StoneTowerTemple, Scene.SouthernSwamp)] // assumed issues
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        UglyTree = 0x229, // Obj_Tree

        // unused in vanilla, its just a mesh elevator like fire temple
        [ActorizerEnabled]
        [FileID(513)]
        [ObjectListIndex(0x20E)]
        [DynaAttributes(12,8)]
        [FlyingToGroundHeightAdjustment(15)]
        [FlyingVariants(0x0)]
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(variant: 0, max: 3)] // dyna should be detecable now, we can add more
        [EnemizerScenesPlacementBlock(//Scene.StoneTower, Scene.IkanaGraveyard, // too much dyna
            //Scene.SouthernSwamp, Scene.SouthernSwampClear,
            Scene.GormanRaceTrack, Scene.DekuTrial)] // blocking potentially
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        //[OnlyOneActorPerRoom] // probably dyna crash to be worried about
        UnusedPirateElevator = 0x22A, // Obj_Y2lift
        
        [FileID(514)]
        [ObjectListIndex(0x20E)]
        PiratesFortressSlidingGate = 0x22B, // Obj_Y2shutter

        // REQUIRES path, because if the player gets on it starts following path
        // using mmra instead; except its busted in 1.16
        [FileID(515)]
        [ObjectListIndex(0x20E)]
        [UnkillableAllVariants]
        Obj_Boat = 0x22C, // Obj_Boat

        [ActorizerEnabled]
        [FileID(516)]
        [ObjectListIndex(0x250)]
        [CheckRestricted(Scene.GreatBayTemple, variant: ActorConst.ANY_VARIANT,
            Item.CollectableGreatBayTempleEntranceRoomBarrel1,
            Item.CollectableGreatBayTempleBlueChuchuValveRoomBarrel1, Item.CollectableGreatBayTempleBlueChuchuValveRoomBarrel2,
            Item.CollectibleStrayFairyGreatBay11, Item.CollectibleStrayFairyGreatBay13,
            Item.CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel1, Item.CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel2,
            Item.CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel1, Item.CollectableGreatBayTempleTopmostRoomWithGreenValveBarrel2)]
        [CheckRestricted(Scene.PiratesFortress, variant: ActorConst.ANY_VARIANT,
            Item.CollectablePiratesFortressInteriorBarrelRoomEggPot1, // just incase colliding actor that blocks pot destruction
            Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem1, Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem2,
            Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem3, Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem4, Item.CollectablePiratesFortressInteriorCellRoomWithPieceOfHeartItem5,
            Item.CollectablePiratesFortressInteriorTelescopeRoomItem1, Item.CollectablePiratesFortressInteriorTelescopeRoomItem2, Item.CollectablePiratesFortressInteriorTelescopeRoomItem3)]
        // params:
        // vanilla: 0x7F3F,
        // oh god params are crraz
        // for dropping barrels, drop random item, 0x3F becomes drop index values
        // 0x1FF is a PF door
        [GroundVariants(0x8710, 0x8711,
            0x7F3F)] // pirates fort
        [WaterBottomVariants(0x8710, 0x8711)] // 16 is flexible, 17 is big fairy
        [UnkillableAllVariants]
        // switch flags
        //[SwitchFlagsPlacement(mask: 0x7F, shift: 0)] // this is only for half of the barrels, lets hand pick these and hope for the best
        [TreasureFlagsPlacement(0x7F, shift:8)]
        [ForbidFromScene(Scene.PiratesFortressExterior)] // needed for a glitch I think
        [PlacementWeight(40)]
        WoodenBarrel = 0x22D, // Obj_Taru
        
        [FileID(517)]
        [ObjectListIndex(0x23D)]
        // special additive switch flag, bleh..
        PFSwitchActivatedGeyser = 0x22E, // Obj_Hunsui

        // Boat archery targets
        [ActorizerEnabled]
        [FileID(518)]
        [ObjectListIndex(0x18E)]
        [WallVariants(0x0)] // no params in code, -1 is used to spawn in vanilla
        [UnkillableAllVariants]
        BoatCruiseTarget = 0x22F, // En_Jc_Mato

        // ?
        [FileID(519)]
        [ObjectListIndex(0x87)]
        MirrorShieldAndGlow = 0x230, // Mir_Ray3

        [ActorizerEnabled] // BUG: do not teach him song or cursed
        [FileID(520)]
        [ObjectListIndex(0x211)]
        [GroundVariants(//0, // this... works? but is not vanilla? where did I get this variant?
            0x1, // concert in zora hall
            0xF)] // credits version in milkbar, and the one in his room
        [WaterBottomVariants( 2, // cutscene version for mikau healing cutscene
            0)] // non vanilla, we dont need to put ocean things in his room
        [VariantsWithRoomMax(max:0,
            0x1)] // wont spawn until after you clear the temple
        [UnkillableAllVariants]
        [AlignedCompanionActor(CircleOfFire, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x3F5F)] // FIRE AND DARKNESS
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        [ForbidFromScene(Scene.ZoraHallRooms)]
        [OnlyOneActorPerRoom]
        [PlacementWeight(75)]
        Japas = 0x231, // En_Zob

        [FileID(521)]
        [ObjectListIndex(0x1)]
        Elf_Msg6 = 0x232, // Elf_Msg6

        // this appears to be more than just peek hole, the actor gets used for other things
        //[ActorizerEnabled]
        // this actor does NOT have the face as a visibleobject, its invisible actor, the mask is part of the scene wall
        // sooo can we use it for something else
        [FileID(522)]
        [ObjectListIndex(1)]
        [WallVariants(0)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.CuriosityShop)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        PeekHole = 0x233, // Obj_Nozoki

        [ActorizerEnabled]
        [FileID(523)]
        [ObjectListIndex(0x23A)]
        [CheckRestricted(Scene.MilkBar, variant: ActorConst.ANY_VARIANT, Item.MaskCircusLeader, Item.NotebookMeetToto, Item.NotebookMovingGorman)]
        [CheckRestricted(Scene.MayorsResidence, variant: ActorConst.ANY_VARIANT, Item.NotebookMeetToto)]
        [GroundVariants(0x050B, 0x3FFF)] // sitting in mayors office // TODO which one
        [PerchingVariants(0x050B, 0x3FFF)] // sitting in mayors office
        [VariantsWithRoomMax(max:0, variant:0x050B, 0x3FFF)] // we dont want a sitting npc to be placed places, just replace, also talking to is softlock, and we cannot modify because rando needs this actor for things
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask:0x7F, shift:0)]
        Toto = 0x234, // En_Toto // manager zora band member

        [EnemizerEnabled] // does not spawn outside of ikana
        [ActorInitVarOffset(0x2CA0)]  // combat music disable does not work
        [FileID(524)]
        [ObjectListIndex(0x75)]
        [CheckRestricted(Item.MaskGibdo)]
        [PathingVariants(0, 0x81, 0x82, 0x83, 0x84, 0x85)]
        [PathingTypeVarsPlacement(mask:0xFF00, shift:8)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        //[ForbidFromScene(Scene.IkanaCanyon)] // dont replace the train
        //[EnemizerScenesPlacementBlock(Scene.DekuShrine)] // might block everything
        GibdoIkana = 0x235, // En_Railgibud

        [ActorizerEnabled] // does not spawn outside of ikana
        [FileID(525)]
        [CheckRestricted(Item.MaskBlast, Item.MaskAllNight, Item.NotebookMeetOldLady, Item.NotebookSaveOldLady)]
        [ObjectListIndex(0xDF)]
        // hard coded to stand still in the shop, not a separate parameter
        [PathingVariants(0x2FF)]
        [PathingTypeVarsPlacement(mask: 0x3F00, shift: 8)]
        [VariantsWithRoomMax(max:0, variant: 0x2FF)] // probably time gated to hell
        [UnkillableAllVariants]
        BombShopLady = 0x236, // En_Baba , grandma

        [ActorizerEnabled] // does not spawn, even the daytime frollicing one
        // both of his vars are paths, sooo I'm guessing his behavior is hard coded
        [ObjectListIndex(0xE3)]
        [FileID(526)]
        [CheckRestricted(Scene.NorthClockTown, variant: ActorConst.ANY_VARIANT, // 0x83FF,
            Item.MaskBlast, Item.MaskAllNight, Item.NotebookMeetOldLady, Item.NotebookSaveOldLady,
            Item.MaskCouple, Item.NotebookEscapeFromSakonSHideout)]
        [CheckRestricted(Scene.IkanaCanyon, variant: ActorConst.ANY_VARIANT, // 0x85FF,
            Item.MaskCouple, Item.NotebookEscapeFromSakonSHideout, Item.NotebookUniteAnjuAndKafei)]
        [CheckRestricted(Scene.CuriosityShop, variant: ActorConst.ANY_VARIANT, // 0x83FF,
            Item.MaskBlast, Item.UpgradeBigBombBag,
            Item.MundaneItemCuriosityShopBlueRupee, Item.MundaneItemCuriosityShopRedRupee, Item.MundaneItemCuriosityShopPurpleRupee, Item.MundaneItemCuriosityShopGoldRupee)]
        // cannot remove because he shares objects with the bank
        //[CheckRestricted(Scene.WestClockTown, variant: 0x83FF, Item.MaskBlast, Item.MaskAllNight)]
        // can't replace the one in west clocktown without killing bank
        // can't replace the one in ikana without killing the kafei quest (even if they are different rooms)
        [PathingVariants(0x85FF, 0x83FF)]
        [PathingTypeVarsPlacement(mask:0x7E00, shift:9)]
        [VariantsWithRoomMax(max:0, variant: 0x85FF, 0x83FF)]
        [UnkillableAllVariants]
        [ForbidFromScene(/* Scene.NorthClockTown, */ Scene.WestClockTown/* , Scene.IkanaCanyon*/)]
        Sakon = 0x237, // En_Suttari

        [ActorizerEnabled]
        [FileID(527)]
        [ObjectListIndex(0x216)]
        [GroundVariants(0xFE0F)]
        [WaterBottomVariants(0xFE0F, 0xFE02, 0xFE01)]
        [OnlyOneActorPerRoom]
        [BlockingVariantsAll]
        [UnkillableAllVariants]
        [PlacementWeight(75)]
        Tijo = 0x238, // En_Zod // drummer zora band member

        //[ActorizerEnabled]
        [FileID(528)]
        [ObjectListIndex(0x263)]
        [WallVariants(0xFF)]
        [GroundVariants(0xFF)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.LotteryShop)]
        // not working??
        //[AlignedCompanionActor(VariousWorldSounds2, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x0144)] // lottery music
        LotteryKiosk = 0x239, // En_Kujiya

        [ActorizerEnabled] 
        [FileID(529)]
        [ObjectListIndex(0xA1)]
        [CheckRestricted(check: Item.MaskDonGero)]
        // does not have or use params, separate for water
        [GroundVariants(0x0)]
        [WaterBottomVariants(0x1)]
        [OnlyOneActorPerRoom]
        [EnemizerScenesPlacementBlock(Scene.MilkRoad)] // he can detect keg and try to talk to you about it, also small bombs I bet
        [UnkillableAllVariants]
        GoronWithGeroMask = 0x23A, // En_Geg : HungryGoron, sirloin goron, "Hugo"

        [ActorizerEnabled]
        [FileID(530)]
        [ObjectListIndex(1)]
        [CheckRestricted(Item.BottleCatchMushroom)]
        [GroundVariants( 0x1, 0x2, 0x5, 0x6, 0x8, 0x9,
            0xA, 0xB, 0xC, 0xD, 0xE, 0xF,
            0x10, 0x11, 0x12, 0x13, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1F, 
            0x7F)]
        //[VariantsWithRoomMax(max:0, variant:0x7F)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [PlacementWeight(60)] // dont waste too many spots
        MushroomCloud = 0x23B, // Obj_Kinoko

        [ActorizerEnabled]
        [FileID(531)]
        [ObjectListIndex(0x218)]
        [CheckRestricted(Scene.GreatBayCoast, variant: ActorConst.ANY_VARIANT, Item.HeartPieceFishermanGame)]
        [DynaAttributes(16,12)]
        [GroundVariants(0x8000, 0x0)]
        [VariantsWithRoomMax(max: 10, 0x8000, 0x0)]
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.ZoraCape)] // giant turtle requires palm trees to function, if we remove these it crashes
        [PlacementWeight(60)]
        PalmTree = 0x23C, // Obj_Yasi
 
        [EnemizerEnabled]
        [FileID(532)]
        [ObjectListIndex(1)] // even thought this enemy is only in one temple, its a gameplay_keep actor?
        // woodfall swarms include: 1,2,3,4,7,A, sure is a lot of variety for a one-off variant
        [FlyingVariants(1,2,3,4,7)] // A would be 8+4?
        //[AlignedCompanionActor(Torch, CompanionAlignment.Above,
        //    variant: )] // 
        [OnlyOneActorPerRoom]
        [FlyingToGroundHeightAdjustment(50)]
        [RespawningAllVariants] // they do NOT respawn, but they do block clear all rooms
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.Snowhead)] // can mess with hover
        MothSwarm = 0x23D,  // En_Tanron1

        [FileID(533)]
        [ObjectListIndex(0x15D)]
        WartsBubbles = 0x23E, // En_Tanron2

        //[EnemizerEnabled] // just crashes, probably wants to be spawned with parent big fish gyorg
        [FileID(534)]
        [ObjectListIndex(0x15C)] // this is gyorgs object, probably too big, but our code would handle that
        //[WaterVariants(0)] // vars unknown, 0x0 crashes
        GyorgSpawn = 0x23F, // En_Tanron3

        [ActorizerEnabled]
        [FileID(535)]
        [ObjectListIndex(0x21E)]
        //[DynaAttributes()] is NOT dyna weirdly
        [CheckRestricted(Item.MaskDonGero)]
        [CeilingVariants(0)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        //[EnemizerScenesPlacementBlock(Scene.IkanaGraveyard)]
        [OnlyOneActorPerRoom]
        [PlacementWeight(10)] // silly, lets keep it very rare
        GoronShrineChandelier = 0x240, // Obj_Chan

        [ActorizerEnabled]
        [FileID(536)]
        [ObjectListIndex(0x220)]
        [CheckRestricted(Scene.ZoraHallRooms, variant: ActorConst.ANY_VARIANT, Item.HeartPieceEvan)]
        // 0xF is type (1,2 and else) the 0xFEXX param does NOTHING wtf
        [GroundVariants(
            //0xFE01, // zora hall one, concert after the thing, does not spawn regularlly
            //0xFE02//, // cutscene version (mikau's healing)
            0xFE0F // both in a cutscene scene and in the milkbar cutscene
        )]
        [WaterBottomVariants(0xFE02, // dark cutscene version, perfect for dark water bottom shinanigans
            0xF)] // also, do not put regular variant as water our typing system is dumb, doesnt know which is which
        [VariantsWithRoomMax(max:1, variant: 0xFE0F/*, 0xFE0F*/)]
        [UnkillableAllVariants]
        [PlacementWeight(30)]
        Evan = 0x241, // En_Zos

        [ActorizerEnabled]
        [FileID(537)]        
        [ObjectListIndex(0xA1)]
        [CheckRestricted(Scene.BombShop, variant:ActorConst.ANY_VARIANT, Item.SongEpona, Item.ItemBottleAliens, Item.MaskCircusLeader)]
        // 9 is the one that sells you kegs
        // 1 and 1E0 just stand around talking
        // assumption 0xF is talking ID
        [GroundVariants(0x1E0, 1, 2, 3, 4, 5, 6, 7, 8, 9)]
        [VariantsWithRoomMax( max: 1,
            0x1E0, 1, 2, 3, 4, 6, 8, 9)]
        [VariantsWithRoomMax(max: 0, 
            5, 7)] // these are tposing types
        [UnkillableAllVariants]
        // we dont have logic to check if this is important enough I guess
        [ForbidFromScene(Scene.BombShop)]//, Scene.GoronShrine)]
        [AlignedCompanionActor(Fairy, CompanionAlignment.Above, ourVariant: -1,
            variant: 2, 9)]
        GoronSGoro = 0x242, // En_S_Goro

        [ActorizerEnabled] 
        [FileID(538)]
        [ObjectListIndex(0x4)]
        [CheckRestricted(Scene.StockPotInn, variant: ActorConst.ANY_VARIANT, Item.HeartPieceNotebookGran1, Item.HeartPieceNotebookGran2,
            Item.NotebookMeetAnjusGrandmother, Item.NotebookGrandmaLongStory, Item.NotebookGrandmaShortStory)]
        [CheckRestricted(Scene.RanchBuildings, variant: ActorConst.ANY_VARIANT, Item.NotebookMeetAnjusGrandmother)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 0, variant: 0)] // does not spawn, time varibles? second required object?
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        //[ForbidFromScene(Scene.StockPotInn)]
        [PlacementWeight(80)]
        AnjusGrandma = 0x243, // En_Nb

        // issue: they dont spawn without at least a pair, this can lead to areas where you get no spawns at all
        [ActorizerEnabled]
        [FileID(539)]
        [ObjectListIndex(0xE3)]
        [GroundVariants(1,0)]
        [PerchingVariants(1)]
        [VariantsWithRoomMax(max: 0, variant:1,0)] // actually schedule locked stupidly, going to stop for now hes been a source of issues
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.RanchBuildings, Scene.ClockTowerInterior,
            Scene.HoneyDarling, Scene.PostOffice, Scene.MayorsResidence, Scene.TreasureChestShop,
            Scene.MarineLab, Scene.AstralObservatory,
            Scene.TownShootingGallery, Scene.SwampShootingGallery, Scene.PotionShop, Scene.GoronShop, Scene.ZoraHallRooms, 
            Scene.PoeHut, Scene.MusicBoxHouse, Scene.BeneathGraveyard)]
        Jugglers = 0x244, // En_Ja

        /// ?? TODO
        [FileID(540)]
        [ObjectListIndex(0x5C)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        [BlockingVariantsAll]
        StoneTowerBlock = 0x245, // Bg_F40_Block

        // todo switches
        [ActorizerEnabled]
        [FileID(541)]
        [ObjectListIndex(0x222)]
        [DynaAttributes(12, 12)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        // we dont want to remove vanilla, use 0 as variant
        [GroundVariants(0)]
        [VariantsWithRoomMax(max:3, variant:0)] // limit because of dyna (untested)
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.StoneTower, Scene.InvertedStoneTower)]
        [PlacementWeight(40)]
        ElegyStatueSwitch = 0x246, // Bg_F40_Switch

        // probably only cutscene actor in this game
        [FileID(542)]
        [ObjectListIndex(0x5D)]
        En_Po_Composer = 0x247, // En_Po_Composer

        [ActorizerEnabled]
        [FileID(543)]
        [ObjectListIndex(0xFF)]
        [CheckRestricted(Scene.LaundryPool, variant: ActorConst.ANY_VARIANT, // 0x01,
            Item.MaskBremen,
            Item.NotebookMeetGuruGuru, Item.NotebookGuruGuru)]
        // 00 is the version from the inn, "dont talk to her shes thinking" meaning the rosa sister
        // 01 is laundry pool, but he only spawns at night, ignoring actor time spawn settings for a scene
        // 02 is the music-only one that spawns so you can hear him through the walls of the inn
        [GroundVariants(0x0, 0x1, 0x2)]
        [VariantsWithRoomMax(max:0, variant:1)]
        [UnkillableAllVariants]
        [BlockingVariantsAll]
        [OnlyOneActorPerRoom] // if two of them are near to each other, and player appears near his nearby music can break
        //[ForbidFromScene(Scene.StockPotInn, Scene.LaundryPool, Scene.MilkBar)] // think him being in milkbar is a credits thing
        [EnemizerScenesPlacementBlock(Scene.MountainVillageSpring)] // his music can break Frog Choir
        GuruGuru = 0x248, // En_GuruGuru

        [FileID(544)]
        [ObjectListIndex(0x1)]
        SonataEffects = 0x249, // Oceff_Wipe5

        [ActorizerEnabled]
        [FileID(545)]
        [ObjectListIndex(0x1B6)]
        [CheckRestricted(Item.NotebookMeetShiro, Item.NotebookSaveInvisibleSoldier, Item.MaskStone)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 1, variant:0)]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.RoadToIkana)]
        [EnemizerScenesPlacementBlock(Scene.Woodfall, Scene.SouthernSwamp, Scene.SouthernSwampClear)] // the scene has lens reversed, so you can see him render without lens, but if you use lens he disspears
        Shiro = 0x24A, // En_Stone_heishi

        [FileID(546)]
        [ObjectListIndex(0x1)]
        SongOfSoaringEffects = 0x24B, // Oceff_Wipe6

        [ActorizerEnabled] // enabled so we can get the object space back and so we can see weird shit in the telescope
        [FileID(547)]
        [ObjectListIndex(0x1E5)]
        [CheckRestricted(Item.HeartPieceTerminaBusinessScrub)]
        [GroundVariants(0x23)] // in the grotto selling stuff
        [FlyingVariants(0x1F)] // might be pathing but I dont really care until we have flying path types
        [VariantsWithRoomMax(max:0, variant: 0x1F)] // assume pathing or whatever wack shit
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        FlyingFieldScrub = 0x24C, // En_Scopenuts

        [ActorizerEnabled]
        [FileID(548)]
        [ObjectListIndex(0x6)]
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT, Item.CollectableTerminaFieldTelescopeGuay1,
            Item.CollectableTerminaFieldGuay1, Item.CollectableTerminaFieldGuay2, Item.CollectableTerminaFieldGuay3,
            Item.CollectableTerminaFieldGuay4, Item.CollectableTerminaFieldGuay5, Item.CollectableTerminaFieldGuay6,
            Item.CollectableTerminaFieldGuay7, Item.CollectableTerminaFieldGuay8, Item.CollectableTerminaFieldGuay9, Item.CollectableTerminaFieldGuay10,
            Item.CollectableTerminaFieldGuay11, Item.CollectableTerminaFieldGuay12, Item.CollectableTerminaFieldGuay13,
            Item.CollectableTerminaFieldGuay14, Item.CollectableTerminaFieldGuay15, Item.CollectableTerminaFieldGuay16,
            Item.CollectableTerminaFieldGuay17, Item.CollectableTerminaFieldGuay18, Item.CollectableTerminaFieldGuay19, Item.CollectableTerminaFieldGuay20,
            Item.CollectableTerminaFieldGuay21, Item.CollectableTerminaFieldGuay22, Item.CollectableTerminaFieldGuay23
            )]
        [FlyingVariants(0x5A2, 0x0922)]
        [VariantsWithRoomMax(max: 0, variant: 0x5A2, 0x0922)] // flying pathing
        [UnkillableAllVariants]
        TelescopeGuay = 0x24D, // En_Scopecrow
        
        [FileID(549)]
        [ObjectListIndex(0x1)]
        SongOfHealingEffects = 0x24E, // Oceff_Wipe7
        
        [FileID(550)]
        [ObjectListIndex(0x229)]
        TurtleWave = 0x24F, // Eff_Kamejima_Wave

        // shared object with below
        [FileID(551)]
        [ObjectListIndex(0x22A)]
        PamelasFatherCursed = 0x250, // En_Hg

        // cannot place without mmra
        [ActorizerEnabled]
        [FileID(552)]
        [ObjectListIndex(0x22A)]
        [CheckRestricted(Item.MaskGibdo)]
        [GroundVariants(0xFF00)] // vanilla params even tho we dont have vanilla params for him
        [VariantsWithRoomMax(max: 0, variant: 0xFF00)] // don't place it wont spawn, need replacement mmra for placement
        PamelasFatherCured = 0x251, // En_Hgo

        [ActorizerEnabled]
        [FileID(553)]
        [ObjectListIndex(0x22B)]
        [CheckRestricted(Item.MundaneItemLuluBadPictographBlueRupee, Item.MundaneItemLuluGoodPictographRedRupee)]
        // E01 is rehersal
        [GroundVariants(0xFE0F,
            0xFE02)] // dark one from the cutscene
        [WaterBottomVariants(0xFE0F)]
        [VariantsWithRoomMax(max:0, variant:0xE01)] // failure to spawn
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.ZoraCape)]
        Lulu = 0x252, // Ee_Zov

        [ActorizerEnabled]
        [FileID(554)]
        [ObjectListIndex(0x7)]
        [GroundVariants(0,2)] // these are vanilla params, but weirdly they dont get used by the actor code
        //[VariantsWithRoomMax(max:0, variant: 0, 2)]// placable but does nothing and is waaaay too common
        [PlacementWeight(10)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        AnjusMother = 0x253, // En_Ah
        
        [FileID(555)]
        [ObjectListIndex(0x22C)]
        [DynaAttributes(8,8)] // oh whoa there are two of them? both the same size
        PamelaHouseCloset = 0x254, // Obj_Hgdoor
        
        [FileID(556)]
        [ObjectListIndex(0x203)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        STTBombableFloor = 0x255, // Bg_Ikana_Bombwall
        
        [FileID(557)]
        [ObjectListIndex(0x203)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        STTLargeLightRay = 0x256, // Bg_Ikana_Ray
        
        [FileID(558)]
        [ObjectListIndex(0x203)]
        STTMetalDoor = 0x257, // Bg_Ikana_Shutter
        
        [FileID(559)]
        [ObjectListIndex(0x1E0)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        Bg_Haka_Bombwall = 0x258, // Bg_Haka_Bombwall
        
        [FileID(560)]
        [ObjectListIndex(0x1E0)]
        FlatsTomb = 0x259, // Bg_Haka_Tomb

        // todo
        [FileID(561)]
        [ObjectListIndex(0x1)]
        GiantRupee = 0x25A, // En_Sc_Ruppe
        
        [FileID(562)]
        [ObjectListIndex(0x237)]
        // has two dyna but we dont care
        SharpsCave = 0x25B, // Bg_Iknv_Doukutu

        // waterwheel at the house, sakon's hideout door, and an unused stonetower door
        [FileID(563)]
        //[DynaAttributes] multiple, but unless we randomize it doesnt matter to us right now
        [ObjectListIndex(0x237)]
        IkanaThings = 0x25C, // Bg_Iknv_Obj

        // probably pathing
        [FileID(564)]
        [ObjectListIndex(0x238)]
        Pamela = 0x25D, // En_Pamera

        [ActorizerEnabled]
        [FileID(565)]
        [ObjectListIndex(0x239)]
        [DynaAttributes(24, 16)]
        // road to ikana is 1007
        // 0xF000, and 0x7F is switchflag, so zero is all we get
        [GroundVariants(0)]
        [VariantsWithRoomMax(max:1, variant:0)]
        [UnkillableAllVariants]
        //[EnemizerScenesPlacementBlock(//Scene.TerminaField, // boring
            //Scene.GreatBayCoast,
            //Scene.SouthernSwamp)] // dyna crash suspect, even if not in the second room
        [ForbidFromScene(Scene.IkanaCanyon, Scene.RoadToIkana)] // do not remove original
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [PlacementWeight(50)] // boring
        IkanaCanyonHookshotStump = 0x25E, // Obj_HsStump

        [ActorizerEnabled]
        [FileID(566)]
        [ObjectListIndex(0x12B)]
        [GroundVariants(0x594)]
        [VariantsWithRoomMax(max:0, variant:0x594)] // we dont place vanilla in the world because the flower is separate, looks silly
        [PathingTypeVarsPlacement(mask:0x7F, shift:0)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        [CompanionActor(DekuFlower, ourVariant: -1, variant: 0x17F)]
        [CompanionActor(GrassRockCluster, ourVariant: -1, variant: 0x801)]
        [CompanionActor(Butterfly, ourVariant: -1, variant: 1, 2)]
        [CompanionActor(Fairy, ourVariant: -1, variant: 2, 7, 9)]
        [RespawningAllVariants] // unkillable I think? even if it wasn't, requiring sonata to kill
        [BlockingVariantsAll]
        SleepingScrub = 0x25F, // En_Hidden_Nuts

        [ActorizerEnabled]
        [FileID(567)]
        [ObjectListIndex(0xD0)]
        //[WaterTopVariants(0,1)] // these are not actually at the water surface, but 10ft below
        [WaterVariants(0,1)]
        [UnkillableAllVariants]
        SwimmingZora = 0x260, // En_Zow

        // multiple talk spots but also a hit spot? hmm
        [ActorizerEnabled]
        [FileID(568)]
        [ObjectListIndex(0x1)]
        [WallVariants(
            0xFE00, // zora band poster
            0xFE01, // construction recruitment poster
            // TODO why are there gaps?
            0xFE03, // treasure chest game poster
            0xFE06, // soldier recruitment poster
            0xFE07, // laundry pool bell
            0xFE0A, // sword school sign
            0xFE0C, // honey and darling sign
            0xFE0D, // postoffice sign
            0xFE0E, // bombshop sign
            0xFE0F, // blackmarket sign
            0xFE10, // trading post sign
            0xFE11, // stockpot inn sign
            0xFE12, // mayors residence sign
            0xFE13, // lottery sign
            0xFE14, // bank sign
            0xFE15, // town archery sign
            0xFE18  // bank poster
        )]
        [UnkillableAllVariants]
        WallTalkSpot = 0x261, // En_Talk

        [ActorizerEnabled]
        [FileID(569)]
        [ObjectListIndex(0xD)]
        [CheckRestricted(Scene.MayorsResidence, variant: ActorConst.ANY_VARIANT,
            Item.MaskKafei,
            Item.NotebookMeetMadameAroma, Item.NotebookPromiseMadameAroma)]
        [CheckRestricted(Scene.MilkBar, variant: ActorConst.ANY_VARIANT,
            Item.ItemBottleMadameAroma,
            Item.NotebookMeetMadameAroma, Item.NotebookDeliverLetterToMama)]
        [GroundVariants(0)]
        [PerchingVariants(0)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        [PlacementWeight(60)]
        MadamAroma = 0x262, // En_Al

        [ActorizerEnabled] // does not spawn, reason unknown
        [FileID(570)]
        [ObjectListIndex(0x13)]
        [CheckRestricted(Item.ShopItemMilkBarChateau, Item.ShopItemMilkBarMilk)]
        [GroundVariants(0)] // might be time gated though
        [VariantsWithRoomMax(max:0, 0)] // does not spawn, heavily scheduled is the issue I think
        [UnkillableAllVariants]
        Bartender = 0x263, // En_Tab

        [FileID(571)]
        [ObjectListIndex(0xE3)]
        BombbagStolenBySakon = 0x264, // En_Nimotsu

        // I want to put these everywhere as an empty actor you can hit by accident, but that adds a lot of colliders
        // you idiot, you can just make them one per room, thats fine
        [ActorizerEnabled]
        [FileID(572)]
        [ObjectListIndex(0x1)]
        [ForbidFromScene(Scene.TerminaField)]
        //[CheckRestricted(Scene.TerminaField)] // TODO
        // TODO decouple the specific actors per object based on variant
        [CheckRestricted(Scene.SouthClockTown, ActorConst.ANY_VARIANT,
            Item.CollectableSouthClockTownHitTag1, Item.CollectableSouthClockTownHitTag2, Item.CollectableSouthClockTownHitTag3)]
        [CheckRestricted(Scene.EastClockTown, 0xFE00, // both are 0xFE00, uhhhh
            Item.CollectableEastClockTownHitTag1, Item.CollectableEastClockTownHitTag2, Item.CollectableEastClockTownHitTag3,
            Item.CollectableEastClockTownHitTag4, Item.CollectableEastClockTownHitTag5, Item.CollectableEastClockTownHitTag6,
            Item.CollectableEastClockTownHitTag7, Item.CollectableEastClockTownHitTag8, Item.CollectableEastClockTownHitTag9,
            Item.ChestEastClockTownSilverRupee)]
        [CheckRestricted(Scene.StockPotInn, ActorConst.ANY_VARIANT,
            Item.CollectableStockPotInnHitTag1, Item.CollectableStockPotInnHitTag2, Item.CollectableStockPotInnHitTag3)]
        [CheckRestricted(Scene.SwampSpiderHouse, ActorConst.ANY_VARIANT,
            Item.CollectableSwampSpiderHouseHitTag1, Item.CollectableSwampSpiderHouseHitTag2, Item.CollectableSwampSpiderHouseHitTag3,
            Item.CollectableSwampSpiderHouseHitTag4, Item.CollectableSwampSpiderHouseHitTag5, Item.CollectableSwampSpiderHouseHitTag6,
            Item.CollectableSwampSpiderHouseHitTag7, Item.CollectableSwampSpiderHouseHitTag8, Item.CollectableSwampSpiderHouseHitTag9,
            Item.CollectableSwampSpiderHouseHitTag10, Item.CollectableSwampSpiderHouseHitTag11, Item.CollectableSwampSpiderHouseHitTag12)]
        [CheckRestricted(Scene.CuccoShack, ActorConst.ANY_VARIANT,
            Item.CollectableCuccoShackHitTag1, Item.CollectableCuccoShackHitTag2, Item.CollectableCuccoShackHitTag3,
            Item.CollectableCuccoShackHitTag4, Item.CollectableCuccoShackHitTag5, Item.CollectableCuccoShackHitTag6)]
        [CheckRestricted(Scene.OceanSpiderHouse, ActorConst.ANY_VARIANT,
            Item.CollectableOceansideSpiderHouseHitTag1, Item.CollectableOceansideSpiderHouseHitTag2, Item.CollectableOceansideSpiderHouseHitTag3,
            Item.CollectableOceansideSpiderHouseHitTag4, Item.CollectableOceansideSpiderHouseHitTag5, Item.CollectableOceansideSpiderHouseHitTag6,
            Item.CollectableOceansideSpiderHouseHitTag7, Item.CollectableOceansideSpiderHouseHitTag8, Item.CollectableOceansideSpiderHouseHitTag9)]
        [CheckRestricted(Scene.PiratesFortressRooms, ActorConst.ANY_VARIANT,
            Item.CollectablePiratesFortressInteriorHookshotRoomHitTag1, Item.CollectablePiratesFortressInteriorHookshotRoomHitTag2, Item.CollectablePiratesFortressInteriorHookshotRoomHitTag3)]
        [CheckRestricted(Scene.PiratesFortress, ActorConst.ANY_VARIANT,
            Item.CollectablePiratesFortressHitTag1, Item.CollectablePiratesFortressHitTag2, Item.CollectablePiratesFortressHitTag3,
        Item.CollectablePiratesFortressHitTag4, Item.CollectablePiratesFortressHitTag5, Item.CollectablePiratesFortressHitTag6)]
        [CheckRestricted(Scene.IkanaGraveyard, ActorConst.ANY_VARIANT,
            Item.CollectableIkanaGraveyardHitTag1, Item.CollectableIkanaGraveyardHitTag2, Item.CollectableIkanaGraveyardHitTag3,
            Item.CollectableIkanaGraveyardHitTag4, Item.CollectableIkanaGraveyardHitTag5, Item.CollectableIkanaGraveyardHitTag6,
            Item.CollectableIkanaGraveyardHitTag7, Item.CollectableIkanaGraveyardHitTag8, Item.CollectableIkanaGraveyardHitTag9,
            Item.CollectableIkanaGraveyardHitTag10, Item.CollectableIkanaGraveyardHitTag11, Item.CollectableIkanaGraveyardHitTag12)]
        // zoey made new ones for the hitspot rando, these are values read from ram of a working seed
        [WallVariants(
            0xFE00, // vanilla value
            0xFF01, 0xFE02, 0xFE03, 0xFE04, 0xFE05, 0xFE06, 0xFE07, 0xFE08, 0xFE09, 0xFE0A, 0xFE0B, 0xFE0C, 0xFE0D, 0xFE0E, 0xFE0F, // new zoey values
            0xFE10, 0xFE11, 0xFE12, 0xFE13, 0xFE14, 0xFE15, 0xFE16, 0xFE17
            // dont know which one is which tho, so can't use per-variant yet
            // esp if my debugging is wrong
        )]
        [CeilingVariants(0xFC00)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        [OnlyOneActorPerRoom]
        [PlacementWeight(30)] // TODO
        HitSpot = 0x265, // En_Hit_Tag // hittag

        [ActorizerEnabled]
        [FileID(573)]
        [ObjectListIndex(0x6)]
        [CheckRestricted(Scene.TerminaField, variant: ActorConst.ANY_VARIANT, Item.CollectableTerminaFieldTelescopeGuay1,
            Item.CollectableTerminaFieldGuay1, Item.CollectableTerminaFieldGuay2, Item.CollectableTerminaFieldGuay3,
            Item.CollectableTerminaFieldGuay4, Item.CollectableTerminaFieldGuay5, Item.CollectableTerminaFieldGuay6,
            Item.CollectableTerminaFieldGuay7, Item.CollectableTerminaFieldGuay8, Item.CollectableTerminaFieldGuay9, Item.CollectableTerminaFieldGuay10,
            Item.CollectableTerminaFieldGuay11, Item.CollectableTerminaFieldGuay12, Item.CollectableTerminaFieldGuay13,
            Item.CollectableTerminaFieldGuay14, Item.CollectableTerminaFieldGuay15, Item.CollectableTerminaFieldGuay16,
            Item.CollectableTerminaFieldGuay17, Item.CollectableTerminaFieldGuay18, Item.CollectableTerminaFieldGuay19, Item.CollectableTerminaFieldGuay20,
            Item.CollectableTerminaFieldGuay21, Item.CollectableTerminaFieldGuay22, Item.CollectableTerminaFieldGuay23
            )]
        [FlyingVariants(0xC00)]
        [VariantsWithRoomMax(max:0, variant:0xC00 )]
        [UnkillableAllVariants] // assumption
        TFGuay = 0x266, // En_Ruppecrow

        [ActorizerEnabled] // spawned for me, but not streamers? weird time dependencies?
        [FileID(574)]
        [ObjectListIndex(0x23F)]
        // params is number of gulls
        // exception: 0x100 is following player
        [FlyingVariants(7, 5)]
        [UnkillableAllVariants]
        [FlyingToGroundHeightAdjustment(500)]
        [VariantsWithRoomMax(max: 2, variant: 7, 5)] // > severe lag over 10
        //[ForbidFromScene(Scene.GreatBayCoast, Scene.ZoraCape)]
        Seagulls = 0x267, // En_Tanron4

        // wont spawn, actor not documented, some weird morita bullshit
        [FileID(575)]
        [ObjectListIndex(0x15B)]
        DestructablePartsOfTwinmoldsArena = 0x268, // En_Tanron5

        //[ActorizerEnabled] // this actor is EMPTY the code has nothing in it
        [FileID(576)]
        [ObjectListIndex(0x1EB)]
        Unused_En_Tanron6 = 0x269, // En_Tanron6

        // TODO: make this version a companion instead, so we have fewer of these guys placed everywhere
        [ActorizerEnabled]
        [FileID(577)]
        [ObjectListIndex(0xF1)]
        // params: 0x7F is switch flags
        // params: 0x8000 is day 3 version
        // params: 0x1f80 is path
        // while this guy stands around and you dont think of him as a pathing actor, bombs make him "flee", path is used
        [PathingVariants(0x201, 0x9FFF)]
        [PathingTypeVarsPlacement(mask: 0x1F80, shift: 7)]
        // restrict if not
        [UnkillableAllVariants]
        [PlacementWeight(40)]
        MilkroadCarpenter = 0x26A, // En_Daiku2

        [ActorizerEnabled]
        [FileID(578)]
        [ObjectListIndex(0xF0)]
        [CheckRestricted(Item.HeartPieceNotebookMayor, Item.NotebookMeetMayorDotour, Item.NotebookDotoursThanks)]
        [GroundVariants(0,
            1)] // mayor version
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:0, variant:0, // hard coded only spawn final night
            1)] // mayors version
        //[ForbidFromScene(Scene.MayorsResidence)]
        [PlacementWeight(75)]
        Mutoh = 0x26B, // En_Muto

        [ActorizerEnabled]
        [FileID(579)]
        [ObjectListIndex(0x247)]
        [CheckRestricted(Item.HeartPieceNotebookMayor, Item.NotebookMeetMayorDotour, Item.NotebookDotoursThanks)]
        [GroundVariants(0, 0x1)] // mayors office
        [VariantsWithRoomMax(max:0, variant: 0, 0x1)] // probably locked into the dialogue and cutscene
        [UnkillableAllVariants]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        Viscen = 0x26C, // En_Baisen

        [ActorizerEnabled]
        [FileID(580)]
        [ObjectListIndex(0x1B6)]
        [CheckRestricted(Item.HeartPieceNotebookMayor, Item.NotebookMeetMayorDotour, Item.NotebookDotoursThanks)]
        [GroundVariants(0, 0x1)]
        [VariantsWithRoomMax(max:0, variant:0, 0x1)]
        [UnkillableAllVariants]
        MayorsResitenceGuard = 0x26D, // En_Heishi

        // bit lame as he doesnt do anything, but he exists
        [ActorizerEnabled]
        [FileID(581)]
        [ObjectListIndex(0x1B6)]
        [GroundVariants(0)] // no params
        [VariantsWithRoomMax(max: 5, variant:0)]
        [UnkillableAllVariants]
        [EnemizerScenesPlacementBlock(Scene.Woodfall)]
        [PlacementWeight(30)]
        UnusedShiro = 0x26E, // En_Demo_heishi

        [ActorizerEnabled]
        [FileID(582)]
        [ObjectListIndex(0x241)]
        [CheckRestricted(Item.HeartPieceNotebookMayor, Item.NotebookMeetMayorDotour, Item.NotebookDotoursThanks)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(0,0)] // talking to him without the rest of his group is crash
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        Dotour = 0x26F, // En_Dt

        [ActorizerEnabled]
        [FileID(583)]
        [ObjectListIndex(0x243)]
        [UnkillableAllVariants]
        // params doesnt seem to be used
        [GroundVariants(0xF)] // only one toof
        [WaterBottomVariants(0xE)]
        [VariantsWithRoomMax(max: 10, variant: 0xF)]
        [ForbidFromScene(Scene.LaundryPool)]
        [PlacementWeight(90)]
        LaundryPoolBell = 0x270, // En_Cha

        // time locked spawn, need to replace
        [FileID(584)]
        [ObjectListIndex(0x244)]
        CremiasCooking = 0x271, // Obj_Dinner
        
        [FileID(585)]
        [ObjectListIndex(0x246)]
        Eff_Lastday = 0x272, // Eff_Lastday

        // todo randomize
        [FileID(586)]
        [ObjectListIndex(0x203)]
        [DynaAttributes(22,16)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        PunchableStoneTowerPillars = 0x273, // Bg_Ikana_Dharma

        [ActorizerEnabled]
        [FileID(587)]
        [ObjectListIndex(0x1E5)]
        [CheckRestricted(Scene.SouthClockTown, variant: ActorConst.ANY_VARIANT, // 0xFC05,
            Item.ShopItemBusinessScrubMagicBean, Item.TradeItemLandDeed, Item.ChestSouthClockTownPurpleRupee)]
        [CheckRestricted(Scene.SouthernSwamp, variant: ActorConst.ANY_VARIANT,
            Item.ShopItemBusinessScrubMagicBean, Item.TradeItemSwampDeed, Item.HeartPieceSwampScrub, Item.UpgradeBiggestBombBag)]
        [CheckRestricted(Scene.SouthernSwampClear, variant: ActorConst.ANY_VARIANT, Item.ShopItemBusinessScrubMagicBean, Item.HeartPieceSwampScrub, Item.UpgradeBiggestBombBag)]
        [CheckRestricted(Scene.GoronVillage, variant: ActorConst.ANY_VARIANT,
            Item.UpgradeBiggestBombBag, Item.TradeItemMountainDeed, Item.ShopItemBusinessScrubGreenPotion, Item.HeartPieceGoronVillageScrub)]
        [CheckRestricted(Scene.ZoraHallRooms, variant: ActorConst.ANY_VARIANT,
            Item.ShopItemBusinessScrubGreenPotion, Item.TradeItemOceanDeed, Item.HeartPieceZoraHallScrub, Item.ShopItemBusinessScrubBluePotion)]
        [CheckRestricted(Scene.IkanaCanyon, variant: ActorConst.ANY_VARIANT,
            Item.ShopItemBusinessScrubBluePotion, Item.IkanaScrubGoldRupee, Item.HeartPieceIkana, Item.CollectableIkanaCanyonSakonSHideoutAreaGossipFairy1)]
        // 0xFC08, 0x1000 are clear swamp
        //0x4 is a flag, meaning the actor has a path, checks if 0xFC00 is a path or not and self terminates
        //[GroundVariants(0xFC08, 0x1000, 0xFC04, 0xFC07, 0x1001, 0x0402, 0xFC06, 0x0001, 0x1800, 0x1003)]
        [PathingVariants(0xFC08,  0xFC04, 0x1001, 0x1003,
            0x1800, 0x7F, // southern swamp
            0x1000, 0xFC05, // clear southern swamp
            0xFC06, 0x0001, // goron village winter
            0xFC07, 0x0402, // zora halls
            0x1403, 0x0003  // ikana canyon
            )]
        [PathingTypeVarsPlacement(mask:0x3F, shift:10)]
        [OnlyOneActorPerRoom]
        [VariantsWithRoomMax(max: 0, variant: 0xFC08, 0xFC04, 0x1001, 0x1003,
            0x1800, 0x7F, // southern swamp
            0x1000, 0xFC05, // clear southern swamp
            0xFC06, 0x0001, // goron village winter
            0x1403, 0x0003,
            0xFC07, 0x0402)]
        [UnkillableAllVariants]
        [AlignedCompanionActor(DekuFlower, CompanionAlignment.OnTop, ourVariant: -1, variant: 0x017F)] // treasure chest shop music
        //[ForbidFromScene( //Scene.SouthernSwamp, Scene.SouthernSwampClear,
            //Scene.SouthClockTown,
            //Scene.GoronVillage,
            //Scene.GoronVillageSpring,
            //Scene.ZoraHallRooms,
            //Scene.IkanaCanyon)]
        BuisnessScrub = 0x274, // En_AkinDonuts

        [FileID(588)]
        [ObjectListIndex(0x1BE)]
        SkullkidEffects = 0x275, // Eff_Stk

        // todo: move his position to better spots as a better actor
        //[ActorizerEnabled]
        [FileID(589)]
        //[ObjectListIndex(0x1E5)] // where did this come from?
        [ObjectListIndex(0x1D5)] // OBJECT_DAI same object as big goron
        [GroundVariants(0x4)] // all vanilla are 0x4, even though the actor checks 0xFF range
        [VariantsWithRoomMax(max:0, variant:0x4)] // schedule locked, wont spawn in weird locations
        [ForbidFromScene(Scene.EastClockTown)] // sound effect rando is too important
        [UnkillableAllVariants]
        // do we need to stop him being randomized?
        LinkTheGoro = 0x276, // En_Ig

        [FileID(590)]
        [ObjectListIndex(0xA1)]
        RacingGoron = 0x277, // En_Rg

        // heads only spawn for a single frame then vanish
        // TODO candidate for gossip stone actor
        [FileID(591)]
        [ObjectListIndex(0x249)]
        // 0 1 2 are the heads
        IgosFloatingHead = 0x278, // En_Osk

        //[ActorizerEnabled] // fixed version that doesnt crash is a separate mmra now
        [FileID(592)]
        [ObjectListIndex(0x26A)] // double object actor
        [GroundVariants(0x0)]
        [VariantsWithRoomMax(max:3, variant:0x0)] // temp disable as double object actors are borken
        [UnkillableAllVariants]
        [ForbidFromScene(Scene.TerminaField)]
        // This is the seth you see in the telescope on grottos, same animation as cured skultula man in kak
        Seth2 = 0x279, // En_Sth2

        // hard coded to be early morning only
        [ActorizerEnabled]
        [FileID(593)]
        [ObjectListIndex(0x24A)]
        [CheckRestricted(Item.MaskKamaro, Item.NotebookMeetKamaro, Item.NotebookPromiseKamaro)]
        [GroundVariants(0xF)]
        [VariantsWithRoomMax(max:0, variant:0xF)] // night only actor, placing just means an empty placement
        //[OnlyOneActorPerRoom] // for now, limit to one instead of limit to none for the rare chance of finding themsomehwere
        [UnkillableAllVariants]
        Kamaro = 0x27A, // En_Yb

        [ActorizerEnabled]
        [ObjectListIndex(0x24B)]
        [FileID(594)]
        [CheckRestricted(Scene.WestClockTown, variant: ActorConst.ANY_VARIANT, // 0x7E02,
            Item.HeartPieceNotebookRosa, Item.NotebookMeetRosaSisters, Item.NotebookRosaSistersThanks)]
        [CheckRestricted(Scene.StockPotInn, variant: ActorConst.ANY_VARIANT, Item.NotebookMeetRosaSisters)]
        // 0xA00 is lobby pacing
        // params: 8000 is a talking flag, 0x7E00 >> 9 is pathing, 0x7E00 is non-pathing though, the one value
        [GroundVariants(0x7E01, 0xFE00, 0xFE01,
            0x7E02, // dancing in torch light
            0xFE02)]
        [PathingVariants(0xA00, 0x8000)]
        [PathingTypeVarsPlacement(mask: 0x7E00, shift:9)]
        //[OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        //[ForbidFromScene(Scene.WestClockTown)]
        RosaSisters = 0x27B, // En_Rz

        [FileID(595)]
        [ObjectListIndex(0x1)]
        En_Scopecoin = 0x27C, // En_Scopecoin

        [ActorizerEnabled]
        [FileID(596)]
        [ObjectListIndex(0x24F)]
        [CheckRestricted(Item.HeartPieceNotebookHand, Item.NotebookMeetToiletHand, Item.NotebookToiletHandSThanks)]
        [GroundVariants(0x0)]
        [WaterBottomVariants(0x0)]
        [VariantsWithRoomMax(max:0, variant:0x0)] // time gated is kind lame bruh
        [UnkillableAllVariants]
        MysteryHand = 0x27D, // En_Bjt

        [ActorizerEnabled]
        [FileID(597)]
        [ObjectListIndex(0x110)]
        //[CheckRestricted(Item.ItemNotebook)]
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2,
            Item.NotebookMeetBombers, Item.NotebookLearnBombersCode
            )] // this is duplicated in multiple places
        [GroundVariants(0xFF00)]
        [VariantsWithRoomMax(max:0, variant: 0xFF00)] // assumption: cannot be placed in other places because he looks for the balloon
        [UnkillableAllVariants]
        JimTheBomber = 0x27E, // En_Bomjima

        [ActorizerEnabled]
        [FileID(598)]
        [ObjectListIndex(0x110)]
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2,
            Item.NotebookMeetBombers, Item.NotebookLearnBombersCode 
            )] // this is duplicated in multiple places
        [GroundVariants(0x0B11, 0x0B22, 0x50F, 0x0513, 0x0910)]
        // while you CAN put them in new places, until I write some code to have them spawn SOMETHING when the game is not active, this is disabled
        [VariantsWithRoomMax(max:0, variant: 0x0B11, 0x0B22, 0x50F, 0x0513, 0x0910)]
        [OnlyOneActorPerRoom]
        [UnkillableAllVariants]
        BombersYouChase = 0x27F, // En_Bomjimb

        [ActorizerEnabled]
        [FileID(599)]
        [ObjectListIndex(0x110)]
        // These are the requirements to randomize the regular bombers in the game, this guy shares the object
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2,
            Item.NotebookMeetBombers, Item.NotebookLearnBombersCode
            )] // this is duplicated in multiple 
        [GroundVariants(0x0, 0x01, 0x2, 0x3, 0x4, 0x10, 0x11, 0x12, 0x13, 0x14)]
        [VariantsWithRoomMax(max:0 , // for some reason these guys can break the game's ability to draw certain things also crash
            variant: 0x0, 0x01, 0x2, 0x3, 0x4, 0x10, 0x11, 0x12, 0x13, 0x14)]
        [PlacementWeight(75)]
        [UnkillableAllVariants]
        BombersBlueHat = 0x280, // En_Bombers

        [ActorizerEnabled]
        [FileID(600)]
        [ObjectListIndex(0x110)]
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2
            )]
        [GroundVariants(0x0)]
        [OnlyOneActorPerRoom] // honestly tempted to not re-randomize him at all, kinda empty and does nothing
        [PlacementWeight(75)]
        [UnkillableAllVariants]
        //[BlockingVariantsAll] // maybe we can try without this, hes actually kinda small
        BomberHideoutGuard = 0x281, // En_Bombers2

        [ActorizerEnabled]
        [FileID(601)]
        [ObjectListIndex(0x280)]
        [CheckRestricted(Item.ItemNotebook,
            Item.ChestBomberHideoutSilverRupee,
            Item.TradeItemMoonTear,
            Item.CollectableTerminaFieldTelescopeGuay1,
            Item.HeartPieceTerminaBusinessScrub,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot1, Item.CollectableAstralObservatoryObservatoryBombersHideoutPot2,
            Item.CollectableAstralObservatoryObservatoryBombersHideoutPot3,
            Item.CollectableAstralObservatorySewerPot1, Item.CollectableAstralObservatorySewerPot2,
            Item.NotebookMeetBombers, Item.NotebookLearnBombersCode
            )] // this is duplicated in multiple places
        [FlyingVariants(0)]
        [FlyingToGroundHeightAdjustment(200)]
        [VariantsWithRoomMax(max:0, variant:0)] // because of the unpoppable without cutscene thing, cannot put places
        [UnkillableAllVariants]  // untested, assumed
        MajoraBalloonNCT = 0x282,

        [FileID(602)]
        [ObjectListIndex(0x1B1)]
        Obj_Moon_Stone = 0x283, // Obj_Moon_Stone

        // the talking points around his basement, giving hints about gibdo
        [FileID(603)]
        [ObjectListIndex(0x1)]
        PamelaHouseFatherInteractions = 0x284, // Obj_Mu_Pict

        // keg breaking ceiling in ikana, no use for it
        [FileID(604)]
        [ObjectListIndex(0x236)]
        [DynaAttributes(8,9)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 9)]
        IkanaCastleKegCieling = 0x285, // Bg_Ikninside

        // assumed to be the stage lights in the Mikau healing thing
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
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        [AlignedCompanionActor(Butler, CompanionAlignment.InFront, ourVariant: -1, variant: 0x8000)]
        [OnlyOneActorPerRoom]
        ButlersSon = 0x289, // En_Nnh

        [FileID(609)]
        [ObjectListIndex(0x260)]
        [DynaAttributes(2,4)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 8)]
        UnderwaterGrate = 0x28A, // Obj_Kzsaku

        [ActorizerEnabled]
        [FileID(610)]
        [ObjectListIndex(0x261)]
        //[SmallVariants]
        //[TableVariants]
        // I think 2 is time gated behind delivery, so dont place
        [GroundVariants(0x0, 0x1, 0x2)]
        [VariantsWithRoomMax(max:0, 0x2)]
        [UnkillableAllVariants]
        [PlacementWeight(30)]
        Milkjar = 0x28B, // Obj_Milk_Bin

        // spawned by the grass itself, no point actorizing as it dissapears if the player is not wearing the mask I think
        [FileID(611)]
        [ObjectListIndex(0x264)]
        Keaton = 0x28C, // En_Kitan
        
        [FileID(612)]
        [ObjectListIndex(0x267)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        IkanaCastleCheckeredCeiling = 0x28D, // Bg_Astr_Bombwall
        
        [FileID(613)]
        [ObjectListIndex(0x236)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        HotCheckeredCeiling = 0x28E, // Bg_Iknin_Susceil
        
        [FileID(614)]
        [ObjectListIndex(0x268)]
        CptKeeta = 0x28F, // En_Bsb

        [ActorizerEnabled]
        [FileID(615)]
        [ObjectListIndex(0x129)]
        [GroundVariants(5)]
        [UnkillableAllVariants]
        [PlacementWeight(60)]
        Secretary = 0x290, // En_Recepgirl, Receptionist

        [EnemizerEnabled]
        [ActorInitVarOffset(0x2E30)]
        [FileID(616)]
        [ObjectListIndex(0x22)]
        [CheckRestricted(Item.CollectableTerminaFieldEnemy1)]
        //[FlyingVariants(0, 1)] // two? one that steals and one that doesn't?
        [FlyingVariants(0)] // zero seems safe, does not steal sword or anything, 1 does not spawn
        [DifficultAllVariants]
        [OnlyOneActorPerRoom]
        [FlyingToGroundHeightAdjustment(100)]
        //[ForbidFromScene(Scene.TerminaField)] // do not remove original, esp with rupeeland coming soon
        [PlacementWeight(75)]
        Takkuri = 0x291, // En_Thiefbird

        //todo
        [FileID(617)]
        [ObjectListIndex(0x1A9)]
        FishingGameFisherman = 0x292, // En_Jgame_Tsn
        
        [FileID(618)]
        [ObjectListIndex(0x80)]
        FishingGameTorch = 0x293, // Obj_Jgame_Light

        // the windows in the stock pot inn
        [ActorizerEnabled]
        [FileID(619)]
        [ObjectListIndex(0x26E)]
        [WallVariants(0x2)]
        [UnkillableAllVariants]
        Windows = 0x294, // Obj_Yado

        [FileID(620)]
        [ObjectListIndex(0x26F)]
        IkanaCanyonClearEffect = 0x295, // Demo_Syoten
        
        [FileID(621)]
        [ObjectListIndex(0x270)]
        Demo_Moonend = 0x296, // Demo_Moonend

        // the pillars you hookshot in stonetower and pirates fortress
        [ActorizerEnabled]
        [FileID(622)]
        [ObjectListIndex(0x27F)]
        [DynaAttributes(18,16)]
        [GroundVariants(0x0)] // all the same
        [ForbidFromScene(Scene.PiratesFortress, Scene.StoneTower, Scene.GreatBayCoast)]
        // we could remove from a few places like greatbaycoast
        [UnkillableAllVariants]
        [PlacementWeight(40)] // boring
        RainbowHookshotPillar = 0x297, // Bg_Lbfshot

        // the bombchu walls you blow up in the link trial
        [FileID(623)]
        [ObjectListIndex(0x234)]
        LinkTrialBombWall = 0x298, // Bg_Last_Bwall

        [ActorizerEnabled]
        [FileID(624)]
        [ObjectListIndex(0x15)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max: 9, variant:0)] // too many will lag
        [UnkillableAllVariants]
        [CompanionActor(Flame, ourVariant: -1, 0x7FE)] // blue flames
        [EnemizerScenesPlacementBlock(Scene.TerminaField, Scene.GoronRacetrack)]
        [AlignedCompanionActor(RegularIceBlock, CompanionAlignment.OnTop, ourVariant: 0, variant: 0xFF78, 0xFF96, 0xFFC8, 0xFFFF)]
        [PlacementWeight(85)]
        AnjuWeddingDress = 0x299, // En_And "En_An + dress"

        [FileID(625)]
        [ObjectListIndex(0x1)]
        En_Invadepoh_Demo = 0x29A, // En_Invadepoh_Demo
        
        [FileID(626)]
        [ObjectListIndex(0x273)]
        Obj_Danpeilift = 0x29B, // Obj_Danpeilift
        
        [FileID(627)]
        [ObjectListIndex(0x269)]
        MoonWarpBeam = 0x29C, // En_Fall2
        
        [FileID(628)]
        [ObjectListIndex(0xD)]
        MadamAromaCutscene = 0x29D, // Dm_Al

        // Dm_Gm is a complete duplicate of this according to darkeye
        [FileID(629)]
        [ObjectListIndex(0xE2)]
        AnjuCutscene = 0x29E, // Dm_An

        [ActorizerEnabled]
        [FileID(630)]
        [ObjectListIndex(0x7)]
        [GroundVariants(0x0)]
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max:0, variant:0x0)] // super common and boring: does nothing until I can fix
        [CompanionActor(Flame, ourVariant: -1, variant: 0x7F4)] // red flames
        AnjuMotherWedding = 0x29F, // Dm_Ah

        [ActorizerEnabled]
        [FileID(631)]
        [ObjectListIndex(0x4)]
        [GroundVariants(0x0)]
        [PerchingVariants(0)] // grandma pls
        [UnkillableAllVariants]
        [CompanionActor(Flame, ourVariant: -1, 0x7FE)] // blue flames
        [PlacementWeight(65)] // until she does something this is a kinda boring actor
        AnjusGrandmaCredits = 0x2A0, // Dm_Nb

        //[ActorizerEnabled]
        [FileID(632)]
        [ObjectListIndex(0x18)] // might also need the sunmask object
        // 0 does not spawn, might need another object
        [GroundVariants(0)]
        [UnkillableAllVariants]
        DressMannequin = 0x2A1, // En_Drs

        //todo
        [FileID(633)]
        [ObjectListIndex(0x241)]
        MajorDotourAtWedding = 0x2A2, // En_Ending_Hero
        
        [FileID(634)]
        [ObjectListIndex(0x185)]
        CutsceneTingle = 0x2A3, // Dm_Bal
        
        [FileID(635)]
        [ObjectListIndex(0x185)]
        CutsceneTingleConfetti = 0x2A4, // En_Paper

        // seriously? this is a different actor?
        // they are all sitting down though, so boring for now until we add actor moving for ledge sitting
        [ActorizerEnabled]
        [FileID(636)]
        [ObjectListIndex(0x142)]
        [CheckRestricted(Item.HeartPieceOceanSpiderHouse)]
        [PerchingVariants(0xF001, 0xF002, 0xF003, 0xF004, 0xF005, 0xF006)]
        [GroundVariants(0xF001, 0xF002, 0xF003, 0xF004, 0xF005, 0xF006)]
        [VariantsWithRoomMax(max: 1, variant:0xF001, 0xF002, 0xF003, 0xF004, 0xF005, 0xF006)]
        [UnkillableAllVariants] // not enemy actor category
        StalchildHintGiver = 0x2A5, // En_Hint_Skb

        // ??
        [FileID(637)]
        [ObjectListIndex(0x1)]
        Dm_Tag = 0x2A6, // Dm_Tag

        //[ActorizerEnabled] // did not spawn
        [FileID(638)]
        [ObjectListIndex(0x2A7)] // for some reason my obj size lookup code thinks this is HUGE
        [FlyingVariants(0xF)] // these might be pathing actors
        [UnkillableAllVariants]
        MoonBirdsBrown = 0x2A7, // En_Bh

        [ActorizerEnabled]
        [FileID(639)]
        [ObjectListIndex(0x247)]
        [GroundVariants(0)] // wedding
        [UnkillableAllVariants]
        [PlacementWeight(15)] // kinda boring until I can fix him
        ViscenMoonLeaveCutscene = 0x2A8, // En_Ending_Hero2 // captain

        [ActorizerEnabled]
        [FileID(640)]
        [ObjectListIndex(0xF0)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max:0, variant:0)] // too common and does nothing, and we have a real mutoh we can put places
        [UnkillableAllVariants]
        MutoMoonLeaveCutscene = 0x2A9, // En_Ending_Hero3

        [ActorizerEnabled]
        [FileID(641)]
        [ObjectListIndex(0x1B6)]
        [GroundVariants(0)]
        [VariantsWithRoomMax(max:0, variant:0)]
        [UnkillableAllVariants]
        [PlacementWeight(80)]
        SoldierMoonLeaveCutscene = 0x2AA, // En_Ending_Hero4

        [ActorizerEnabled]
        [FileID(642)]
        [ObjectListIndex(0xF1)]
        [GroundVariants(0x0, 0x1, 0x2, 0x3, 0x4)]
        [VariantsWithRoomMax(max: 0, variant: 0x0, 0x1, 0x2, 0x3, 0x4)] // often fail to spawn
        [UnkillableAllVariants]
        [PlacementWeight(30)]
        // placable?
        CarpentersFromCutscene = 0x2AB, // En_Ending_Hero5

        // ???
        [FileID(643)]
        [ObjectListIndex(0x1)]
        En_Ending_Hero6 = 0x2AC, // En_Ending_Hero6

        // duplicate of anju cutscene?
        // requires three objects: her own, OBJECT_AN4, OBJECT_MSMO,
        [FileID(644)]
        [ObjectListIndex(0xE2)]
        Unused_Dm_Gm = 0x2AD, // Dm_Gm

        // TODO: I should be able to replace these if the beans on top are randomized, just gotta move over the checks
        [FileID(645)]
        [ObjectListIndex(0x1)]
        [SwitchFlagsPlacement(mask: 0x7F, shift: 0)]
        SpawnsItemFromSoil = 0x2AE, // Obj_Swprize

        // this is the actor you have to walk into , not the one you hit with ranged attacks
        [ActorizerEnabled]
        [FileID(646)]
        [ObjectListIndex(0x1)]
        [CheckRestricted(Scene.TerminaField, 0x45, Item.CollectableTerminaFieldInvisibleItem1)] // hollow stump goron jump
        [CheckRestricted(Scene.TerminaField, 0x4C, Item.CollectableTerminaFieldInvisibleItem2)] // grasses
        [CheckRestricted(Scene.TerminaField, 0x5C, Item.CollectableTerminaFieldInvisibleItem3)]
        [CheckRestricted(Scene.TerminaField, 0x60, Item.CollectableTerminaFieldInvisibleItem4)]
        [CheckRestricted(Scene.TerminaField, 0x64, Item.CollectableTerminaFieldInvisibleItem5)]
        [CheckRestricted(Scene.TerminaField, 0x68, Item.CollectableTerminaFieldInvisibleItem6)]
        [CheckRestricted(Scene.TerminaField, 0x6D, Item.CollectableTerminaFieldInvisibleItem7)] // water fountains
        [CheckRestricted(Scene.TerminaField, 0x71, Item.CollectableTerminaFieldInvisibleItem8)]
        [CheckRestricted(Scene.TerminaField, 0x75, Item.CollectableTerminaFieldInvisibleItem9)]  // goron jump ocean ramp
        [CheckRestricted(Scene.TerminaField, 0x79, Item.CollectableTerminaFieldInvisibleItem10)] // goron jump ocean chest
        [CheckRestricted(Scene.TerminaField, 0x7D, Item.CollectableTerminaFieldInvisibleItem11)] // goron jump snowfield
        [CheckRestricted(Scene.RomaniRanch, 0x29, Item.CollectableRomaniRanchInvisibleItem1)] // ranch fence items
        [CheckRestricted(Scene.RomaniRanch, 0x2D, Item.CollectableRomaniRanchInvisibleItem2)]
        [CheckRestricted(Scene.RomaniRanch, 0x30, Item.CollectableRomaniRanchInvisibleItem3)]
        [CheckRestricted(Scene.RomaniRanch, 0x34, Item.CollectableRomaniRanchInvisibleItem4)]
        [CheckRestricted(Scene.RomaniRanch, 0x38, Item.CollectableRomaniRanchInvisibleItem5)]
        [CheckRestricted(Scene.RomaniRanch, 0x3C, Item.CollectableRomaniRanchInvisibleItem6)]
        // TODO find at least one that gives rup without items
        [FlyingVariants(0x45, 0x79, 0x7D, 0x75)] // tf goron jumps
        [PerchingVariants(0x29, 0x2D, 0x30, 0x34, 0x38, 0x3C)] // romani ranch fence
        [WaterBottomVariants(0x6D, 0x71)] // tf water fountains
        [GroundVariants(
            0x4C, 0x5C, 0x60, 0x64, 0x68, // tf grass
            0x6D, 0x71, // tf water fountains
            0x29, 0x2D, 0x30, 0x34, 0x38, 0x3C // romani ranch fence
        )] // todo search
        // TODO find one that is non-vanilla for wall
        [OnlyOneActorPerRoom] // as they give items or money, limit to one
        [UnkillableAllVariants]
        [PlacementWeight(40)] // free stuff, make it more rare
        En_Invisible_Ruppe = 0x2AF, // En_Invisible_Ruppe
        
        [FileID(647)]
        [ObjectListIndex(0x281)]
        EndingStumpAndLighting = 0x2B0, // Obj_Ending

        [ActorizerEnabled]
        [FileID(648)]
        [ObjectListIndex(0x12C)]
        [GroundVariants(0)] // no params I bet
        [UnkillableAllVariants]
        [VariantsWithRoomMax(max: 1, variant: 0)] // need to test
        [PlacementWeight(5)] // bit boring
        CreditsBombShopMan = 0x2B1, // En_Rsn

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
        Water,
        WaterTop,       // added in 52
        WaterBottom,    // added in 52
        Ground,
        Flying,
        Wall,
        Perching,       // added in 56
        Ceiling,        // added in 61
        Pathing,
    }
}
