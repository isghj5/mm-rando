using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    public enum Entrance
    {
        [Exit(Scene.InvertedStoneTower, 0)]
        [Spawn(Scene.InvertedStoneTowerTemple, 0)]
        EntranceStoneTowerTempleInvertedFromStoneTowerInverted,

        [Exit(Scene.Woodfall, 1)]
        [Spawn(Scene.WoodfallTemple, 0)]
        EntranceWoodfallTempleFromWoodfall,

        [Exit(Scene.Snowhead, 1)]
        [Spawn(Scene.SnowheadTemple, 0)]
        EntranceSnowheadTempleFromSnowhead, // should use the No Intro version if shorten cutscenes is enabled

        [Exit(Scene.GreatBayTemple, 0)]
        [ExitAddress(0xF155BA)]
        [Spawn(Scene.ZoraCape, 7)]
        EntranceZoraCapeFromGreatBayTemple,

        [Exit(Scene.WoodfallTemple, 0)]
        [Spawn(Scene.Woodfall, 1)]
        EntranceWoodfallFromWoodfallTempleEntrance,

        [ExitCutscene(Scene.ZoraCape, 0, 2)]
        [ExitCutscene(Scene.ZoraCape, 1, 2)]
        [ExitCutscene(Scene.GreatBayCutscene, 0, 0)]
        [Spawn(Scene.GreatBayTemple, 0)]
        EntranceGreatBayTempleFromZoraCape,

        [Exit(Scene.InvertedStoneTowerTemple, 0)]
        [Spawn(Scene.InvertedStoneTower, 1)]
        EntranceStoneTowerInvertedFromStoneTowerTempleInverted,

        [Exit(Scene.SnowheadTemple, 0)]
        [Spawn(Scene.Snowhead, 1)]
        EntranceSnowheadFromSnowheadTemple,

        [Spawn(Scene.MountainVillage, 7)]
        EntranceMountainVillageFromSnowheadClear, // one way

        [Spawn(Scene.ZoraCape, 8)]
        EntranceZoraCapeFromGreatBayTempleClear, // one way

        [Spawn(Scene.IkanaCanyon, 7)]
        EntranceIkanaCanyonFromIkanaClear, // one way

        [Spawn(Scene.WoodfallTemple, 1)]
        EntranceWoodfallTemplePrisonFromOdolwasLair, // one way

        [Exit(Scene.WoodfallTemple, 1)]
        [Spawn(Scene.OdolwasLair, 0)]
        EntranceOdolwasLairFromWoodfallTemple, // one way

        [Exit(Scene.SnowheadTemple, 1)]
        [Spawn(Scene.GohtsLair, 0)]
        EntranceGohtsLairFromSnowheadTemple, // one way

        [Exit(Scene.GreatBayTemple, 1)]
        [Spawn(Scene.GyorgsLair, 0)]
        EntranceGyorgsLairFromGreatBayTemple, // one way

        [Exit(Scene.InvertedStoneTowerTemple, 1)]
        [Spawn(Scene.TwinmoldsLair, 0)]
        EntranceTwinmoldsLairFromStoneTowerTempleInverted, // one way

        [ExitActorParams(Scene.TerminaField, 0, 0, 70)]
        //[ExitActorParams(Scene.TwinIslands, 0, 0, 1)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 0)]
        [Spawn(Scene.Grottos, 0)]
        EntranceGrottoGossipOcean,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 2, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoGossipOcean,

        [ExitActorParams(Scene.TerminaField, 0, 0, 73)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 1)]
        [Spawn(Scene.Grottos, 1)]
        EntranceGrottoGossipSwamp,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 4, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoGossipSwamp,

        [ExitActorParams(Scene.TerminaField, 0, 0, 76)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 2)]
        [Spawn(Scene.Grottos, 2)]
        EntranceGrottoGossipCanyon,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 6, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoGossipCanyon,

        [ExitActorParams(Scene.TerminaField, 0, 0, 74)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 3)]
        [Spawn(Scene.Grottos, 3)]
        EntranceGrottoGossipMountain,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 8, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoGossipMountain,

        [ExitActorParams(Scene.GreatBayCoast, 0, 0, 59)]
        [ExitActorParams(Scene.GreatBayCoast, 1, 0, 36)]
        [SpawnActorParams(0x2055, 0x17, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericGreatBayCoast,

        [ExitActorParams(Scene.MountainVillageSpring, 0, 1, 23)]
        [ExitActorParams(Scene.MountainVillageSpring, 1, 1, 2)]
        [SpawnActorParams(0x2055, 0x1B, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericMountainVillageSpring,

        [ExitActorParams(Scene.SouthernSwamp, 0, 1, 35)]
        [ExitActorParams(Scene.SouthernSwampClear, 0, 1, 27)]
        [SpawnActorParams(0x2055, 0x1D, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericSouthernSwamp,

        [ExitActorParams(Scene.RoadToSouthernSwamp, 0, 0, 45)]
        [SpawnActorParams(0x2055, 0x1E, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericRoadToSwamp,

        [ExitActorParams(Scene.TerminaField, 0, 0, 78)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericTerminaFieldGrass,

        [ExitActorParams(Scene.IkanaCanyon, 0, 2, 0)]
        [ExitActorParams(Scene.IkanaCanyon, 1, 2, 0)]
        [ExitActorParams(Scene.IkanaCanyon, 3, 2, 0)]
        [SpawnActorParams(0x2055, 0x14, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericIkanaCanyon,

        [ExitActorParams(Scene.WoodsOfMystery, 0, 2, 0)]
        [SpawnActorParams(0x2055, 0x1C, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericWoodsOfMystery,

        [ExitActorParams(Scene.ZoraCape, 0, 0, 61)]
        [ExitActorParams(Scene.ZoraCape, 1, 0, 56)]
        [SpawnActorParams(0x2055, 0x15, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericZoraCape,

        [ExitActorParams(Scene.RoadToIkana, 0, 0, 75)]
        [SpawnActorParams(0x2055, 0x16, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericRoadToIkana,

        [ExitActorParams(Scene.TerminaField, 0, 0, 77)]
        [SpawnActorParams(0x2055, 0x1A, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericTerminaFieldPillar,

        [ExitActorParams(Scene.IkanaGraveyard, 0, 1, 46)]
        [ExitActorParams(Scene.IkanaGraveyard, 1, 1, 3)]
        [SpawnActorParams(0x2055, 0x18, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericIkanaGraveyard,

        [ExitActorParams(Scene.PathToSnowhead, 0, 0, 4)]
        [ExitActorParams(Scene.PathToSnowhead, 1, 0, 16)]
        [SpawnActorParams(0x2055, 0x13, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericPathToSnowhead,

        [ExitActorParams(Scene.TwinIslands, 0, 0, 39)]
        [ExitActorParams(Scene.TwinIslandsSpring, 0, 0, 23)]
        [SpawnActorParams(0x2055, 0x19, 0x1F, 0, 0, 4)]
        EntranceGrottoGenericTwinIslands,

        [ExitActorParams(Scene.TwinIslands, 0, 0, 0)]
        [ExitActorParams(Scene.TwinIslandsSpring, 0, 0, 29)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 5)]
        [Spawn(Scene.Grottos, 5)]
        EntranceGrottoHotSpring,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 45, 0, 6, 7)]
        EntranceTwinIslandsFromGrottoHotSpring,

        [ExitActorParams(Scene.TerminaField, 0, 0, 71)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 7)]
        [Spawn(Scene.Grottos, 7)]
        EntranceGrottoDodongo,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 23, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoDodongo,

        [ExitActorParams(Scene.TerminaField, 0, 0, 75)]
        [ExitActorParams(Scene.TerminaField, 5, 0, 8)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 9)]
        [Spawn(Scene.Grottos, 9)]
        EntranceGrottoDekuMerchant,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 30, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoDekuMerchant,

        [ExitActorParams(Scene.GreatBayCoast, 0, 0, 14)]
        [ExitActorParams(Scene.GreatBayCoast, 1, 0, 37)]
        [SpawnActorParams(0x2055, 0x02, 0x1F, 0, 0, 10)]
        EntranceGrottoCowGreatBayCoast,

        [ExitActorParams(Scene.TerminaField, 0, 0, 79)]
        [SpawnActorParams(0x2055, 0x00, 0x1F, 0, 0, 10)]
        EntranceGrottoCowTerminaField,

        [ExitActorParams(Scene.TerminaField, 0, 0, 69)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 11)]
        [Spawn(Scene.Grottos, 11)]
        EntranceGrottoBioBaba,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 36, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoBioBaba,

        [ExitActorParams(Scene.TerminaField, 0, 0, 72)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 13)]
        [Spawn(Scene.Grottos, 13)]
        EntranceGrottoPeahat,

        [Spawn(0xFFFF)]
        [ExitPolygonType(Scene.Grottos, 40, 0, 6, 7)]
        EntranceTerminaFieldFromGrottoPeahat,

        [Spawn(0xFFFF)]
        EntranceGrottoReturn,

        [Exit(Scene.DekuPalace, 6)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 12)]
        [Spawn(Scene.Grottos, 12)]
        EntranceGrottoBeanSeller,

        [Spawn(Scene.DekuPalace, 9)]
        [ExitPolygonType(Scene.Grottos, 37, 0, 6, 7)]
        EntranceDekuPalaceFromBeanSellerGrotto,

        [Exit(Scene.NorthClockTown, 4)]
        [SpawnActorParams(0x2055, 0x1F, 0x1F, 0, 0, 6)]
        [Spawn(Scene.DekuPlayground, 0)]
        EntranceGrottoDekuPlayground,

        [Exit(Scene.DekuPlayground, 0)]
        [Spawn(Scene.NorthClockTown, 4)]
        EntranceNorthClockTownFromDekuPlayground,

        [Exit(Scene.EastClockTown, 7)]
        [Spawn(Scene.MayorsResidence, 0)]
        EntranceMayorsResidenceFromEastClockTown,

        //[Exit(Scene.TheMoon, 0)]
        //[Spawn(Scene.MajorasLair, 0)]
        //EntranceMajorasLairFromTheMoon, // one way

        [Exit(Scene.SouthernSwamp, 5)]
        [Spawn(Scene.PotionShop, 0)]
        EntranceMagicHagsPotionShopFromSouthernSwamp,

        [Exit(Scene.RomaniRanch, 2)]
        [Spawn(Scene.RanchBuildings, 0)]
        EntranceRanchBarnFromRomaniRanch,

        [Exit(Scene.RomaniRanch, 3)]
        [Spawn(Scene.RanchBuildings, 1)]
        EntranceRanchHouseFromRomaniRanch,

        //EntranceRanchHouseBarnFromCrash, // maybe during abduction cutscene

        [Exit(Scene.EastClockTown, 6)]
        [Spawn(Scene.HoneyDarling, 0)]
        EntranceHoneyDarlingsShopFromEastClockTown,


        //[Exit(Scene.IkanaGraveyard, 2)]
        //[Spawn(Scene.BeneathGraveyard, 0)]
        //EntranceBeneathGraveyardFromIkanaGraveyardNight2,

        //[Exit(Scene.IkanaGraveyard, 3)]
        //[Spawn(Scene.BeneathGraveyard, 1)]
        //EntranceBeneathGraveyardFromIkanaGraveyardNight1,

        //[Exit(Scene.RoadToSouthernSwamp, 1)]
        //[Spawn(Scene.SouthernSwamp, 0)]
        //EntranceSouthernSwampFromRoadtoSouthernSwamp,

        [Exit(Scene.TouristCenter, 0)]
        [Spawn(Scene.SouthernSwamp, 1)]
        EntranceSouthernSwampFromTouristInformation,

        //[Exit(Scene.Woodfall, 0)]
        //[Spawn(Scene.SouthernSwamp, 2)]
        //EntranceSouthernSwampFromWoodfall,

        //[Exit(Scene.DekuPalace, 0)]
        //[Spawn(Scene.SouthernSwamp, 3)]
        //EntranceSouthernSwampFromDekuPalaceLower,

        //[Exit(Scene.DekuPalace, 9)]
        //[Spawn(Scene.SouthernSwamp, 4)]
        //EntranceSouthernSwampFromDekuPalaceUpper,

        [Exit(Scene.PotionShop, 0)]
        [ExitAddress(0xEF55C6)] // kicked out when witch is out. to clear swamp
        [ExitAddress(0xEF55AE)] // kicked out when witch is out. to poison swamp
        [Spawn(Scene.SouthernSwamp, 5)]
        EntranceSouthernSwampFromMagicHagsPotionShop,

        //[Exit(Scene.SouthernSwamp, 6)]
        //[ExitAddress(0xEBA86E)] // only boat archery? from witch?
        //[ExitAddress(0xEBA882)] // only boat ride?    from witch?
        //[ExitAddress(0xF4BBA2)] // boat ride          from guy
        //[Spawn(Scene.SouthernSwamp, 6)]
        //EntranceBoatArcheryFromTouristInformation, // photo cruise / boat archery

        [Exit(Scene.WoodsOfMystery, 0)]
        [Spawn(Scene.SouthernSwamp, 7)]
        EntranceSouthernSwampFromWoodsofMystery,

        [Exit(Scene.SwampSpiderHouse, 0)]
        [Spawn(Scene.SouthernSwamp, 8)]
        EntranceSouthernSwampFromSwampSpiderHouse,

        //[Exit(Scene.IkanaCanyon, 4)]
        //[Spawn(Scene.SouthernSwamp, 9)]
        //EntranceSouthernSwampFromIkanaCanyon, // one way

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x0E)]
        //[Spawn(Scene.SouthernSwamp, 10)]
        //EntranceSouthernSwampFromOwlStatue, // one way

        [Exit(Scene.WestClockTown, 4)]
        [Spawn(Scene.CuriosityShop, 0)]
        EntranceCuriosityShopFromWestClockTown,

        [Exit(Scene.LaundryPool, 2)]
        [Spawn(Scene.CuriosityShop, 1)]
        EntranceKafeisHideoutFromLaundryPool,

        //[ExitAddress(0x100853A)]
        //[Spawn(Scene.CuriosityShop, 2)]
        //EntranceCuriosityShopFromKafeisHideout, // peep hole

        //[ExitAddress(0xCB5102)]
        //[Spawn(Scene.CuriosityShop, 3)]
        //EntranceKafeisHideoutFromCuriosityShop, // peep hole

        [Exit(Scene.GoronVillage, 3)]
        [Spawn(Scene.Grottos, 16)]
        EntranceGrottoLensCaveFromGoronVillage,

        //[Exit(Scene.RoadToIkana, 1)]
        //[Spawn(Scene.IkanaCanyon, 0)]
        //EntranceIkanaCanyonFromRoadtoIkana,

        [Exit(Scene.PoeHut, 0)]
        [Spawn(Scene.IkanaCanyon, 1)]
        EntranceIkanaCanyonFromPoeHut,

        [Exit(Scene.MusicBoxHouse, 0)]
        [ExitAddress(0x103EC1E)] // not sure
        [ExitCutscene(Scene.MusicBoxHouse, 0, 1)] // kicked out by pamela?
        [ExitCutscene(Scene.MusicBoxHouse, 0, 3)] // kicked out by pamela?
        [Spawn(Scene.IkanaCanyon, 2)]
        EntranceIkanaCanyonFromMusicBoxHouse,

        //[Exit(Scene.StoneTower, 0)]
        //[Spawn(Scene.IkanaCanyon, 3)]
        //EntranceIkanaCanyonFromStoneTower,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x10)]
        //[Spawn(Scene.IkanaCanyon, 4)]
        //EntranceIkanaCanyonFromOwlStatue, // one way

        //[Exit(Scene.BeneathTheWell, 0)]
        //[Spawn(Scene.IkanaCanyon, 5)]
        //EntranceIkanaCanyonFromBeneaththeWell,

        [Exit(Scene.SakonsHideout, 0)]
        [ExitAddress(0xC00416)] // todo check kafei still appears in ECT/Inn and remove the cutscene id.
        [ExitAddress(0xF57732)] // exit via song of soaring
        [ExitCutscene(Scene.SakonsHideout, 0, 1)]
        [Spawn(Scene.IkanaCanyon, 6)]
        EntranceIkanaCanyonFromSakonsHideout,

        //[Exit(Scene.IkanaCastle, 1)]
        //[Spawn(Scene.IkanaCanyon, 8)]
        //EntranceIkanaCanyonFromAncientCastleofIkanaCourtyard,

        //EntranceCutsceneIkanaCanyonFromSpringWaterCave, // one way

        //EntranceSpringWaterCaveFromMusicBoxCutscene, // one way

        [Exit(Scene.FairyFountain, 4)]
        [Spawn(Scene.IkanaCanyon, 11)]
        EntranceIkanaCanyonFromFairysFountain,

        [Exit(Scene.SecretShrine, 0)]
        [Spawn(Scene.IkanaCanyon, 12)]
        EntranceIkanaCanyonFromSecretShrine,

        [Exit(Scene.IkanaCanyon, 11)]
        [Spawn(Scene.IkanaCanyon, 13)]
        EntranceIkanaCanyonFromSpringWaterCave,

        [Exit(Scene.IkanaCanyon, 10)]
        [Spawn(Scene.IkanaCanyon, 14)]
        EntranceSpringWaterCaveFromIkanaCanyon,


        //[Exit(Scene.PiratesFortressExterior, 1)]
        //[Spawn(Scene.PiratesFortress, 0)]
        //EntrancePiratesFortressFromMainEntrance,

        //[Exit(Scene.PiratesFortressRooms, 0)]
        //[Spawn(Scene.PiratesFortress, 1)]
        //EntrancePiratesFortressFromHookshotRoomLower,

        //[Exit(Scene.PiratesFortressRooms, 1)]
        //[Spawn(Scene.PiratesFortress, 2)]
        //EntrancePiratesFortressFromHookshotRoomUpper,

        //[Exit(Scene.PiratesFortressRooms, 2)]
        //[Spawn(Scene.PiratesFortress, 3)]
        //EntrancePiratesFortressFromGuardRoomFront,

        //[Exit(Scene.PiratesFortressRooms, 3)]
        //[Spawn(Scene.PiratesFortress, 4)]
        //EntrancePiratesFortressFromGuardRoomBack,

        //[Exit(Scene.PiratesFortressRooms, 4)]
        //[Spawn(Scene.PiratesFortress, 5)]
        //EntrancePiratesFortressFromBarrelMazeFront,

        //[Exit(Scene.PiratesFortressRooms, 5)]
        //[Spawn(Scene.PiratesFortress, 6)]
        //EntrancePiratesFortressFromBarrelMazeBack,

        //[Exit(Scene.PiratesFortressRooms, 6)]
        //[Spawn(Scene.PiratesFortress, 7)]
        //EntrancePiratesFortressFromOnePatrolFront,

        //[Exit(Scene.PiratesFortressRooms, 7)]
        //[Spawn(Scene.PiratesFortress, 8)]
        //EntrancePiratesFortressFromOnePatrolBack,

        // EntrancePiratesFortressUnused

        //[ExitAddress(0xECE51A)]
        //[Spawn(Scene.PiratesFortress, 10)]
        //EntrancePiratesFortressFromTelescope,

        // EntrancePiratesFortressUnused

        //[Exit(Scene.PiratesFortressExterior, 5)]
        //[Spawn(Scene.PiratesFortress, 12)]
        //EntrancePiratesFortressFromPiratesFortressExteriorBalcony,


        [Exit(Scene.EastClockTown, 12)]
        [Spawn(Scene.MilkBar, 0)]
        EntranceMilkBarFromEastClockTown,


        [Exit(Scene.StoneTower, 1)]
        [Spawn(Scene.StoneTowerTemple, 0)]
        EntranceStoneTowerTempleFromStoneTower, // should use the No Intro version if shorten cutscenes is enabled


        [Exit(Scene.EastClockTown, 4)]
        [Spawn(Scene.TreasureChestShop, 0)]
        EntranceTreasureChestShopFromEastClockTown,

        //[ExitAddress(0xF449DA)]
        //[Spawn(Scene.TreasureChestShop, 1)]
        //EntranceTreasureChestShopFromTreasureChestShop, // after opening the chest

        //[Exit(Scene.SouthClockTown, 8)]
        ////[ExitAddress(0xED4ABE)] // todo check en_fall
        //[Spawn(Scene.ClockTowerRoof, 0)]
        //EntranceClockTowerRooftopFromSouthClockTown, // todo one way?

        //EntranceClockTowerRooftopFromClockTowerRooftop, // after receiving-song-of-time cutscene // one way


        //EntranceBeforethePortaltoTerminaFromLostWoods, // todo accessible via wrong-warp?

        //[Exit(Scene.ClockTowerInterior, 0)]
        //[Spawn(Scene.BeforeThePortalToTermina, 1)]
        //EntranceBeforethePortaltoTerminaFromClockTowerInterior, // glitched logic only

        //[Exit(Scene.BeforeThePortalToTermina, 2)]
        //[Spawn(Scene.BeforeThePortalToTermina, 3)]
        //EntranceBeforethePortaltoTerminaFromBeforethePortaltoTermina, // void respawn // one way?

        //[Exit(Scene.Woodfall, 3)]
        //[Spawn(Scene.WoodfallTemple, 2)]
        //EntranceWoodfallTemplePrisonFromWoodfall,


        //[Exit(Scene.TerminaField, 3)]
        //[Spawn(Scene.PathToMountainVillage, 0)]
        //EntrancePathtoMountainVillageFromTerminaField,

        //[Exit(Scene.MountainVillage, 0)]
        //[Spawn(Scene.PathToMountainVillage, 1)]
        //EntrancePathtoMountainVillageFromMountainVillage,


        //[Exit(Scene.BeneathTheWell, 1)]
        //[Spawn(Scene.IkanaCastle, 0)]
        //EntranceAncientCastleofIkanaCourtyardFromBeneaththeWell,

        //[Exit(Scene.IkanaCanyon, 7)]
        //[Spawn(Scene.IkanaCastle, 1)]
        //EntranceAncientCastleofIkanaCourtyardFromIkanaCanyon,

        //[Exit(Scene.IkanaCastle, 3)]
        //[Spawn(Scene.IkanaCastle, 2)]
        //EntranceAncientCastleofIkanaCourtyardFromAncientCastleofIkana,

        //[Exit(Scene.IkanaCastle, 2)]
        //[Spawn(Scene.IkanaCastle, 3)]
        //EntranceAncientCastleofIkanaFromCourtyard,

        //[Exit(Scene.IkanaCastle, 5)]
        //[Spawn(Scene.IkanaCastle, 5)]
        //EntranceAncientCastleofIkanaFromBlockHole, // one way

        //[Exit(Scene.IkanaCastle, 4)]
        //[Spawn(Scene.IkanaCastle, 4)]
        //EntranceAncientCastleofIkanaFromKegHole, // one way

        //[Exit(Scene.IgosDuIkanasLair, 0)]
        //[Spawn(Scene.IkanaCastle, 6)]
        //EntranceAncientCastleofIkanaFromIgosduIkanasLair,


        [Exit(Scene.EastClockTown, 8)]
        [Spawn(Scene.TownShootingGallery, 0)]
        EntranceTownShootingGalleryFromEastClockTown,


        //[Exit(Scene.TerminaField, 5)]
        //[Spawn(Scene.MilkRoad, 0)]
        //EntranceMilkRoadFromTerminaField,

        //[Exit(Scene.RomaniRanch, 0)]
        //[Spawn(Scene.MilkRoad, 1)]
        //EntranceMilkRoadFromRomaniRanch,

        //[Exit(Scene.GormanTrack, 1)]
        //[Spawn(Scene.MilkRoad, 2)]
        //EntranceMilkRoadFromGormanRacetrackTrack,

        //[Exit(Scene.GormanTrack, 0)]
        //[Spawn(Scene.MilkRoad, 3)]
        //EntranceMilkRoadFromGormanRacetrackMain,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x0A)]
        //[Spawn(Scene.MilkRoad, 4)]
        //EntranceMilkRoadFromOwlStatue, // one way

        //[ExitAddress(0xFDF11A)]
        //[Spawn(Scene.MilkRoad, 5)]
        //EntranceMilkRoadFrom, // maybe during cremia escort?

        //[ExitAddress(0xFDF6E2)]
        //[Spawn(Scene.MilkRoad, 6)]
        //EntranceMilkRoadFrom, // maybe during cremia escort?


        //[Exit(Scene.PiratesFortress, 1)]
        //[Spawn(Scene.PiratesFortressRooms, 0)]
        //EntrancePiratesFortressHookshotRoomFromLowerDoor,

        //[Exit(Scene.PiratesFortress, 2)]
        //[Spawn(Scene.PiratesFortressRooms, 1)]
        //EntrancePiratesFortressHookshotRoomFromUpperDoor,

        //[Exit(Scene.PiratesFortress, 3)]
        //[Spawn(Scene.PiratesFortressRooms, 2)]
        //EntrancePiratesFortressGuardRoomFromFrontDoor,

        //[Exit(Scene.PiratesFortress, 4)]
        //[Spawn(Scene.PiratesFortressRooms, 3)]
        //EntrancePiratesFortressGuardRoomFromBackDoor,

        //[Exit(Scene.PiratesFortress, 5)]
        //[Spawn(Scene.PiratesFortressRooms, 4)]
        //EntrancePiratesFortressBarrelMazeFromFrontDoor,

        //[Exit(Scene.PiratesFortress, 6)]
        //[Spawn(Scene.PiratesFortressRooms, 5)]
        //EntrancePiratesFortressBarrelMazeFromBackDoor,

        //[Exit(Scene.PiratesFortress, 7)]
        //[Spawn(Scene.PiratesFortressRooms, 6)]
        //EntrancePiratesFortressOneGuardFrontFromPiratesFortress,

        //[Exit(Scene.PiratesFortress, 8)]
        //[Spawn(Scene.PiratesFortressRooms, 7)]
        //EntrancePiratesFortressOneGuardRearFromPiratesFortress,

        //[Exit(Scene.PiratesFortress, 9)]
        //[ExitAddress(0xCB5106)]
        //[Spawn(Scene.PiratesFortressRooms, 8)]
        //EntrancePiratesFortressSewerFromTelescope,

        //[Exit(Scene.PiratesFortressExterior, 2)]
        //[Spawn(Scene.PiratesFortressRooms, 9)]
        //EntrancePiratesFortressSewerFromWater,

        //[Exit(Scene.PiratesFortressExterior, 3)]
        //[Spawn(Scene.PiratesFortressRooms, 10)]
        //EntrancePiratesFortressSewerFromRear,


        [Exit(Scene.RoadToSouthernSwamp, 2)]
        [Spawn(Scene.SwampShootingGallery, 0)]
        EntranceSwampShootingGalleryFromRoadtoSouthernSwamp,


        [Exit(Scene.GreatBayCoast, 3)]
        [Spawn(Scene.PinnacleRock, 0)]
        EntrancePinnacleRockFromGreatBayCoast,

        //[Exit(Scene.PinnacleRock, 1)]
        //[Spawn(Scene.PinnacleRock, 1)]
        //EntrancePinnacleRockFromPinnacleRock, // void respawn // one way?


        [Exit(Scene.NorthClockTown, 3)]
        [Spawn(Scene.FairyFountain, 0)]
        EntranceFairysFountainFromNorthClockTown,

        [Exit(Scene.Woodfall, 2)]
        [Spawn(Scene.FairyFountain, 1)]
        EntranceFairysFountainFromWoodfall,

        [Exit(Scene.Snowhead, 2)]
        [Spawn(Scene.FairyFountain, 2)]
        EntranceFairysFountainFromSnowhead,

        [Exit(Scene.ZoraCape, 5)]
        [Spawn(Scene.FairyFountain, 3)]
        EntranceFairysFountainFromZoraCape,

        [Exit(Scene.IkanaCanyon, 8)]
        [Spawn(Scene.FairyFountain, 4)]
        EntranceFairysFountainFromIkanaCanyon,


        [Exit(Scene.SouthernSwamp, 8)]
        [Spawn(Scene.SwampSpiderHouse, 0)]
        EntranceSwampSpiderHouseFromSouthernSwamp,


        [Exit(Scene.GreatBayCoast, 8)]
        [Spawn(Scene.OceanSpiderHouse, 0)]
        EntranceOceansideSpiderHouseFromGreatBayCoast,


        //[Exit(Scene.EastClockTown, 2)]
        //[Spawn(Scene.AstralObservatory, 0)]
        //EntranceAstralObservatoryFromEastClockTown,

        //[Exit(Scene.TerminaField, 9)]
        //[Spawn(Scene.AstralObservatory, 1)]
        //EntranceAstralObservatoryFromTerminaField,

        //[ExitAddress(0xCB50E2)]
        //[Spawn(Scene.AstralObservatory, 2)]
        //EntranceAstralObservatoryFromTelescope,


        //[Exit(Scene.TheMoon, 1)]
        //[Spawn(Scene.DekuTrial, 0)]
        //EntranceDekuTrialFromTheMoon,


        //[Exit(Scene.SouthernSwamp, 3)]
        //[Spawn(Scene.DekuPalace, 0)]
        //EntranceDekuPalaceFromSouthernSwampLower,

        //[ExitAddress(0xDA050A)] // thrown out of king's chamber
        //[ExitCutscene(Scene.DekuKingChamber, 0, 3)] // after receiving sonata check
        //[ExitCutscene(Scene.DekuKingChamber, 0, 6)] // after receiving sonata check
        //[ExitAddress(0xED02E6)] // thrown out by patrol guards. must apply fix-deku-patrol-exit mod for this to work
        //[ExitAddress(0xF1D166)] // after sonata check - overwrites code from replace-gi-table
        //[Spawn(Scene.DekuPalace, 1)]
        //[HackContent(nameof(Resources.mods.fix_deku_patrol_exit))]
        //EntranceDekuPalaceFromDekuPalace, // thrown out // one way

        //[Exit(Scene.DekuKingChamber, 0)]
        //[Spawn(Scene.DekuPalace, 2)]
        //EntranceDekuPalaceFromDekuKingsChamberMain,

        //[Exit(Scene.DekuKingChamber, 1)]
        //[Spawn(Scene.DekuPalace, 3)]
        //EntranceDekuPalaceFromDekuKingsChamberGardenWest,

        [Exit(Scene.DekuShrine, 0)]
        [Exit(Scene.DekuShrine, 2)]
        [Spawn(Scene.DekuPalace, 4)]
        EntranceDekuPalaceFromDekuShrine,

        //[Exit(Scene.SouthernSwamp, 4)]
        //[Spawn(Scene.DekuPalace, 5)]
        //EntranceDekuPalaceFromSouthernSwampUpper,


        [Exit(Scene.MountainVillage, 1)]
        [Spawn(Scene.MountainSmithy, 0)]
        EntranceMountainSmithyFromMountainVillage,


        //[Exit(Scene.WestClockTown, 0)]
        //[Spawn(Scene.TerminaField, 0)]
        //EntranceTerminaFieldFromWestClockTown,

        //[Exit(Scene.RoadToSouthernSwamp, 0)]
        //[Spawn(Scene.TerminaField, 1)]
        //EntranceTerminaFieldFromRoadtoSouthernSwamp,

        //[Exit(Scene.GreatBayCoast, 0)]
        //[Spawn(Scene.TerminaField, 2)]
        //EntranceTerminaFieldFromGreatBayCoast,

        //[Exit(Scene.PathToMountainVillage, 0)]
        //[Spawn(Scene.TerminaField, 3)]
        //EntranceTerminaFieldFromPathtoMountainVillage,

        //[Exit(Scene.RoadToIkana, 0)]
        //[Spawn(Scene.TerminaField, 4)]
        //EntranceTerminaFieldFromRoadtoIkana,

        //[Exit(Scene.MilkRoad, 0)]
        //[Spawn(Scene.TerminaField, 5)]
        //EntranceTerminaFieldFromMilkRoad,

        //[Exit(Scene.SouthClockTown, 1)]
        //[Spawn(Scene.TerminaField, 6)]
        //EntranceTerminaFieldFromSouthClockTown,

        //[Exit(Scene.EastClockTown, 0)]
        //[Spawn(Scene.TerminaField, 7)]
        //EntranceTerminaFieldFromEastClockTown,

        //[Exit(Scene.NorthClockTown, 0)]
        //[Spawn(Scene.TerminaField, 8)]
        //EntranceTerminaFieldFromNorthClockTown,

        //[Exit(Scene.AstralObservatory, 1)]
        //[Spawn(Scene.TerminaField, 9)]
        //EntranceTerminaFieldFromAstralObservatory,

        //[ExitAddress(0xE3EB82)]
        //[Spawn(Scene.TerminaField, 10)]
        //[HackContent(nameof(Resources.mods.fix_telescope_music))]
        //EntranceTerminaFieldFromAstralObservatoryTelescope,

        ////EntranceTerminaFieldFromTerminaField, // todo moon crash - accessible via precise exit from telescope // one way

        //[ExitAddress(0xFF573E)]
        //[Spawn(Scene.TerminaField, 13)]
        //EntranceTerminaFieldFromCremiaEscort, // one way

        ////EntranceTerminaFieldFromTerminaField, // after tatl/tael/skullkid cutscene // one way


        [Exit(Scene.WestClockTown, 7)]
        [Spawn(Scene.PostOffice, 0)]
        EntrancePostOfficeFromWestClockTown,


        [Exit(Scene.GreatBayCoast, 7)]
        [Spawn(Scene.MarineLab, 0)]
        EntranceMarineResearchLabFromGreatBayCoast,


        //[Exit(Scene.IkanaGraveyard, 1)]
        //[Spawn(Scene.DampesHouse, 0)]
        //EntranceDampesHouseFromIkanaGraveyardGrave,

        //[Exit(Scene.IkanaGraveyard, 4)]
        //[Spawn(Scene.DampesHouse, 1)]
        //EntranceDampesHouseFromIkanaGraveyardDoor, // glitched logic only


        //[Exit(Scene.GoronVillage, 2)]
        //[Spawn(Scene.GoronShrine, 0)]
        //EntranceGoronShrineFromGoronVillage,

        [Exit(Scene.GoronShop, 0)]
        [Spawn(Scene.GoronShrine, 1)]
        EntranceGoronShrineFromGoronShop,

        //EntranceGoronShrineFromGoronShrine, // after lullaby cutscene // one way


        //[Exit(Scene.ZoraCape, 2)]
        //[Spawn(Scene.ZoraHall, 1)]
        //EntranceZoraHallFromZoraCapeLand,

        //[Exit(Scene.ZoraCape, 1)]
        //[Spawn(Scene.ZoraHall, 0)]
        //EntranceZoraHallFromZoraCapeWater,

        [Exit(Scene.ZoraHallRooms, 4)]
        [Spawn(Scene.ZoraHall, 2)]
        EntranceZoraHallFromZoraShop,

        [Exit(Scene.ZoraHallRooms, 2)]
        [Spawn(Scene.ZoraHall, 3)]
        EntranceZoraHallFromLulusRoom,

        [Exit(Scene.ZoraHallRooms, 3)]
        [Spawn(Scene.ZoraHall, 4)]
        EntranceZoraHallFromEvansRoom,

        [Exit(Scene.ZoraHallRooms, 1)]
        [Spawn(Scene.ZoraHall, 5)]
        EntranceZoraHallFromJapasRoom,

        [Exit(Scene.ZoraHallRooms, 0)]
        [Spawn(Scene.ZoraHall, 6)]
        EntranceZoraHallFromMikauTijosRoom,


        [Exit(Scene.WestClockTown, 5)]
        [Spawn(Scene.TradingPost, 0)]
        EntranceTradingPostFromWestClockTown,


        //[Exit(Scene.MilkRoad, 1)]
        //[Spawn(Scene.RomaniRanch, 0)]
        //EntranceRomaniRanchFromMilkRoad,

        //[ExitAddress(0xF24E86)]
        //[Spawn(Scene.RomaniRanch, 1)]
        //EntranceRomaniRanchFromRomaniRanch, // after minigame

        [Exit(Scene.RanchBuildings, 0)]
        [Spawn(Scene.RomaniRanch, 2)]
        EntranceRomaniRanchFromBarn,

        [Exit(Scene.RanchBuildings, 1)]
        [Spawn(Scene.RomaniRanch, 3)]
        EntranceRomaniRanchFromRanchHouse,

        [Exit(Scene.CuccoShack, 0)]
        [Spawn(Scene.RomaniRanch, 4)]
        EntranceRomaniRanchFromCuccoShack,

        [Exit(Scene.DoggyRacetrack, 0)]
        [Spawn(Scene.RomaniRanch, 5)]
        EntranceRomaniRanchFromDoggyRacetrack,

        // todo add after aliens defense entrance


        //[Exit(Scene.TerminaField, 2)]
        //[Spawn(Scene.GreatBayCoast, 0)]
        //EntranceGreatBayCoastFromTerminaField,

        //[Exit(Scene.ZoraCape, 0)]
        //[Spawn(Scene.GreatBayCoast, 1)]
        //EntranceGreatBayCoastFromZoraCape,

        //[Exit(Scene.GreatBayCoast, 2)]
        //[Spawn(Scene.GreatBayCoast, 2)]
        //EntranceGreatBayCoastFromGreatBayCoastBeach, // void respawn beach // one way?

        [Exit(Scene.PinnacleRock, 0)]
        [Spawn(Scene.GreatBayCoast, 3)]
        EntranceGreatBayCoastFromPinnacleRock,

        [Exit(Scene.FishermansHut, 0)]
        [Spawn(Scene.GreatBayCoast, 4)]
        EntranceGreatBayCoastFromFishermansHut,

        //[Exit(Scene.PiratesFortressExterior, 0)]
        //[Spawn(Scene.GreatBayCoast, 5)]
        //EntranceGreatBayCoastFromPiratesFortress,

        //[Exit(Scene.GreatBayCoast, 6)]
        //[Spawn(Scene.GreatBayCoast, 6)]
        //EntranceGreatBayCoastFromGreatBayCoastNearFortress, // void respawn near fortress // one way?

        [Exit(Scene.MarineLab, 0)]
        [Spawn(Scene.GreatBayCoast, 7)]
        EntranceGreatBayCoastFromMarineResearchLab,

        [Exit(Scene.OceanSpiderHouse, 0)]
        [Spawn(Scene.GreatBayCoast, 8)]
        EntranceGreatBayCoastFromOceansideSpiderHouse,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x00)]
        //[Spawn(Scene.GreatBayCoast, 11)]
        //EntranceGreatBayCoastFromOwlStatue, // one way

        //[Exit(Scene.PiratesFortressExterior, 4)]
        //[Spawn(Scene.GreatBayCoast, 12)]
        //EntranceGreatBayCoastFromPiratesFortressThrownOut, // one way

        //[ExitAddress(0x1079476)]
        //[Spawn(Scene.GreatBayCoast, 13)]
        //EntranceGreatBayCoastFromZoraCape, // after fisherman minigame


        //[Exit(Scene.GreatBayCoast, 1)]
        //[Spawn(Scene.ZoraCape, 0)]
        //EntranceZoraCapeFromGreatBayCoast,

        //[Exit(Scene.ZoraHall, 1)]
        //[Spawn(Scene.ZoraCape, 2)]
        //EntranceZoraCapeFromZoraHallLand,

        //[Exit(Scene.ZoraHall, 0)]
        //[Spawn(Scene.ZoraCape, 1)]
        //EntranceZoraCapeFromZoraHallWater,

        //[Exit(Scene.ZoraCape, 3)]
        //[Spawn(Scene.ZoraCape, 3)]
        //EntranceZoraCapeFromZoraCape, // void respawn // one way?

        [Exit(Scene.WaterfallRapids, 0)]
        [Spawn(Scene.ZoraCape, 4)]
        EntranceZoraCapeFromWaterfallRapids,

        [Exit(Scene.FairyFountain, 3)]
        [Spawn(Scene.ZoraCape, 5)]
        EntranceZoraCapeFromFairysFountain,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x02)]
        //[Spawn(Scene.ZoraCape, 6)]
        //EntranceZoraCapeFromOwlStatue, // one way


        [Exit(Scene.WestClockTown, 8)]
        [Spawn(Scene.LotteryShop, 0)]
        EntranceLotteryShopFromWestClockTown,


        //[Exit(Scene.GreatBayCoast, 5)]
        //[Spawn(Scene.PiratesFortressExterior, 0)]
        //EntrancePiratesFortressExteriorFromGreatBayCoast,

        //[Exit(Scene.PiratesFortress, 0)]
        //[Spawn(Scene.PiratesFortressExterior, 1)]
        //EntrancePiratesFortressExteriorFromPiratesFortressMain,

        //[Exit(Scene.PiratesFortressRooms, 9)]
        //[Spawn(Scene.PiratesFortressExterior, 2)]
        //EntrancePiratesFortressExteriorFromPiratesFortressSewerMain,

        //[Exit(Scene.PiratesFortressRooms, 10)]
        //[Spawn(Scene.PiratesFortressExterior, 3)]
        //EntrancePiratesFortressExteriorFromPiratesFortressSewerExhaust, // one way?

        //[Exit(Scene.PiratesFortress, 10)]
        //[Exit(Scene.PiratesFortressRooms, 11)]
        //[Spawn(Scene.PiratesFortressExterior, 4)]
        //EntrancePiratesFortressExteriorFromThrownOut, // one way

        //[Exit(Scene.PiratesFortress, 12)]
        //[Spawn(Scene.PiratesFortressExterior, 5)]
        //EntrancePiratesFortressExteriorFromPiratesFortressBalcony,

        //[Exit(Scene.PiratesFortressRooms, 12)]
        //[Spawn(Scene.PiratesFortressExterior, 6)]
        //EntrancePiratesFortressExteriorFromPiratesFortressSewerDoor,


        [Exit(Scene.GreatBayCoast, 4)]
        [Spawn(Scene.FishermansHut, 0)]
        EntranceFishermansHutFromGreatBayCoast,


        [Exit(Scene.GoronShrine, 1)]
        [Spawn(Scene.GoronShop, 0)]
        EntranceGoronShopFromGoronVillage,


        //[Exit(Scene.DekuPalace, 1)]
        //[Spawn(Scene.DekuKingChamber, 0)]
        //EntranceDekuKingsChamberFromDekuPalace,

        //[Exit(Scene.DekuPalace, 2)]
        //[Spawn(Scene.DekuKingChamber, 1)]
        //EntranceDekuKingsChamberFromDekuPalaceWestGarden,

        //EntranceDekuKingsChamberFromDekuKingsChamber, // cutscene monkey being released // one way


        //[Exit(Scene.TheMoon, 2)]
        //[Spawn(Scene.GoronTrial, 0)]
        //EntranceGoronTrialFromTheMoon,


        //[Exit(Scene.TerminaField, 1)]
        //[Spawn(Scene.RoadToSouthernSwamp, 0)]
        //EntranceRoadtoSouthernSwampFromTerminaField,

        //[Exit(Scene.SouthernSwamp, 0)]
        //[Spawn(Scene.RoadToSouthernSwamp, 1)]
        //EntranceRoadtoSouthernSwampFromSouthernSwamp,

        [Exit(Scene.SwampShootingGallery, 0)]
        [Spawn(Scene.RoadToSouthernSwamp, 2)]
        EntranceRoadtoSouthernSwampFromSwampShootingGallery,


        [Exit(Scene.RomaniRanch, 5)]
        [Spawn(Scene.DoggyRacetrack, 0)]
        EntranceDoggyRacetrackFromRomaniRanch,


        [Exit(Scene.RomaniRanch, 4)]
        [Spawn(Scene.CuccoShack, 0)]
        EntranceCuccoShackFromRomaniRanch,

        //EntranceCuccoShackFromCuccoShack, // after chickens grow up // one way


        //[Exit(Scene.RoadToIkana, 2)]
        //[Spawn(Scene.IkanaGraveyard, 0)]
        //EntranceIkanaGraveyardFromRoadtoIkana,

        //[Exit(Scene.DampesHouse, 0)]
        //[Spawn(Scene.IkanaGraveyard, 1)]
        //EntranceIkanaGraveyardFromDay3Grave, // exit only

        //[Exit(Scene.BeneathGraveyard, 0)]
        //[Spawn(Scene.IkanaGraveyard, 2)]
        //EntranceIkanaGraveyardFromDay2Grave,

        //[Exit(Scene.BeneathGraveyard, 1)]
        //[Spawn(Scene.IkanaGraveyard, 3)]
        //EntranceIkanaGraveyardFromDay1Grave,

        //[Exit(Scene.DampesHouse, 1)]
        //[Spawn(Scene.IkanaGraveyard, 4)]
        //EntranceIkanaGraveyardFromDampesHouse,

        //EntranceIkanaGraveyardFromIkanaGraveyard, // captain keeta defeated // one way


        //[Exit(Scene.SouthernSwamp, 2)]
        //[Spawn(Scene.Woodfall, 0)]
        //EntranceWoodfallFromSouthernSwamp,

        [Exit(Scene.FairyFountain, 1)]
        [Spawn(Scene.Woodfall, 2)]
        EntranceWoodfallFromFairysFountain,

        //[Exit(Scene.WoodfallTemple, 2)]
        //[Spawn(Scene.Woodfall, 3)]
        //EntranceWoodfallFromWoodfallTempleExit,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x0C)]
        //[Spawn(Scene.Woodfall, 4)]
        //EntranceWoodfallFromOwlStatue, // one way


        //[Exit(Scene.TheMoon, 3)]
        //[Spawn(Scene.ZoraTrial, 0)]
        //EntranceZoraTrialFromTheMoon,

        //[Exit(Scene.ZoraTrial, 1)]
        //[Spawn(Scene.ZoraTrial, 1)]
        //EntranceZoraTrialFromZoraTrial, // void respawn // one way?


        //[Exit(Scene.TwinIslands, 1)]
        //[Spawn(Scene.GoronVillage, 0)]
        //EntranceGoronVillageFromPathtoGoronVillage,

        //[Exit(Scene.GoronShrine, 0)]
        //[Spawn(Scene.GoronVillage, 2)]
        //EntranceGoronVillageFromGoronShrine,

        [Exit(Scene.Grottos, 1)]
        [Spawn(Scene.GoronVillage, 3)]
        EntranceGoronVillageFromLensCave,


        [Exit(Scene.ZoraCape, 4)]
        [Spawn(Scene.WaterfallRapids, 0)]
        EntranceWaterfallRapidsFromZoraCape,

        //EntranceWaterfallRapidsFromWaterfallRapids, // beaver race start // one way

        //EntranceWaterfallRapidsFromWaterfallRapids, // beaver race end // one way


        //[Exit(Scene.IkanaCanyon, 5)]
        //[Spawn(Scene.BeneathTheWell, 0)]
        //EntranceBeneaththeWellFromIkanaCanyon,

        //[Exit(Scene.IkanaCastle, 0)]
        //[Spawn(Scene.BeneathTheWell, 1)]
        //EntranceBeneaththeWellFromAncientCastleofIkana,


        [Exit(Scene.ZoraHall, 6)]
        [Spawn(Scene.ZoraHallRooms, 0)]
        EntranceZoraHallRoomsMikauTijosRoomFromZoraHall,

        [Exit(Scene.ZoraHall, 5)]
        [Spawn(Scene.ZoraHallRooms, 1)]
        EntranceZoraHallRoomsJapasRoomFromZoraHall,

        [Exit(Scene.ZoraHall, 3)]
        [Spawn(Scene.ZoraHallRooms, 2)]
        EntranceZoraHallRoomsLulusRoomFromZoraHall,

        [Exit(Scene.ZoraHall, 4)]
        [Spawn(Scene.ZoraHallRooms, 3)]
        EntranceZoraHallRoomsEvansRoomFromZoraHall,

        [ExitCutscene(Scene.ZoraHall, 1, 0)]
        [Spawn(Scene.ZoraHallRooms, 4)]
        EntranceZoraHallRoomsJapasRoomFromJapasRoom, // after jam session // one way?

        [Exit(Scene.ZoraHall, 2)]
        [Spawn(Scene.ZoraHallRooms, 5)]
        EntranceZoraHallRoomsZoraShopFromZoraHall,

        //EntranceZoraHallRoomsEvansRoomFromEvansRoom, // after song cutscene


        [Exit(Scene.MountainVillage, 3)]
        [Spawn(Scene.GoronGrave, 0)]
        EntranceGoronGraveyardFromMountainVillage,

        //EntranceGoronGraveyardFromGoronGraveyard, // after darmina cutscene


        [Exit(Scene.IkanaCanyon, 6)]
        [Spawn(Scene.SakonsHideout, 0)]
        EntranceSakonsHideoutFromIkanaCanyon,


        [Exit(Scene.MountainSmithy, 0)]
        [Spawn(Scene.MountainVillage, 1)]
        EntranceMountainVillageFromMountainSmithy,

        //[Exit(Scene.TwinIslands, 0)]
        //[Spawn(Scene.MountainVillage, 2)]
        //EntranceMountainVillageFromPathtoGoronVillage,

        [Exit(Scene.GoronGrave, 0)]
        [Spawn(Scene.MountainVillage, 3)]
        EntranceMountainVillageFromGoronGraveyard,

        //[Exit(Scene.PathToSnowhead, 0)]
        //[Spawn(Scene.MountainVillage, 4)]
        //EntranceMountainVillageFromPathtoSnowhead,

        //EntranceMountainVillageFromBehindWaterfall, // unused // one way

        //[Exit(Scene.PathToMountainVillage, 1)]
        //[Spawn(Scene.MountainVillage, 6)]
        //EntranceMountainVillageFromPathtoMountainVillage,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x06)]
        //[Spawn(Scene.MountainVillage, 8)]
        //EntranceMountainVillageFromOwlStatue, // one way


        [Exit(Scene.IkanaCanyon, 1)]
        [Spawn(Scene.PoeHut, 0)]
        EntrancePoeHutFromIkanaCanyon,

        //[ExitAddress(0xF7514A)]
        //[Spawn(Scene.PoeHut, 1)]
        //EntrancePoeHutFromPoeHut, // fighting poes

        //[ExitAddress(0xF7519A)]
        //[Spawn(Scene.PoeHut, 2)]
        //EntrancePoeHutFromPoeHut, // after fighting poes


        [Exit(Scene.DekuPalace, 3)]
        [Spawn(Scene.DekuShrine, 0)]
        EntranceDekuShrineFromDekuPalace,


        //[Exit(Scene.TerminaField, 4)]
        //[Spawn(Scene.RoadToIkana, 0)]
        //EntranceRoadtoIkanaFromTerminaField,

        //[Exit(Scene.IkanaCanyon, 0)]
        //[Spawn(Scene.RoadToIkana, 1)]
        //EntranceRoadtoIkanaFromIkanaCanyon,

        //[Exit(Scene.IkanaGraveyard, 0)]
        //[Spawn(Scene.RoadToIkana, 2)]
        //EntranceRoadtoIkanaFromIkanaGraveyard,


        [Exit(Scene.WestClockTown, 3)]
        [Spawn(Scene.SwordsmansSchool, 0)]
        EntranceSwordsmansSchoolFromWestClockTown,


        [Exit(Scene.IkanaCanyon, 2)]
        [Spawn(Scene.MusicBoxHouse, 0)]
        EntranceMusicBoxHouseFromIkanaCanyon,


        //[Exit(Scene.IkanaCastle, 6)]
        //[Spawn(Scene.IgosDuIkanasLair, 0)]
        //EntranceIgosduIkanasLairFromAncientCastleofIkana,


        [Exit(Scene.SouthernSwamp, 1)]
        [Spawn(Scene.TouristCenter, 0)]
        EntranceTouristInformationFromSouthernSwamp,

        //[ExitAddress(0xCB61AE)]
        //[ExitAddress(0xDC70B2)]
        //[ExitAddress(0xFDBD7A)]
        //[Spawn(Scene.TouristCenter, 1)]
        //EntranceTouristInformationFromWitchBoatRide,

        //[ExitAddress(0xCB61A2)]
        //[ExitAddress(0xDC7092)]
        //[Spawn(Scene.TouristCenter, 2)]
        //EntranceTouristInformationFromPictoBoatRide,


        //[Exit(Scene.IkanaCanyon, 3)]
        //[Spawn(Scene.StoneTower, 0)]
        //EntranceStoneTowerFromIkanaCanyon,

        //[ExitAddress(0xD21F3A)]
        //[Spawn(Scene.StoneTower, 1)]
        //EntranceStoneTowerFromFlipSwitch, // after flip

        [Exit(Scene.StoneTowerTemple, 0)]
        [Spawn(Scene.StoneTower, 2)]
        EntranceStoneTowerFromStoneTowerTemple,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x12)]
        //[Spawn(Scene.StoneTower, 3)]
        //EntranceStoneTowerFromOwlStatue, // one way


        //[ExitAddress(0xD21F4E)]
        //[Spawn(Scene.InvertedStoneTower, 0)]
        //EntranceStoneTowerInvertedFromFlipSwitch,

        //[Exit(Scene.InvertedStoneTowerTemple, 0)]
        //[Spawn(Scene.InvertedStoneTower, 1)]
        //EntranceStoneTowerInvertedFromStoneTowerTemple, // from temple?


        //[Exit(Scene.MountainVillage, 4)]
        //[Spawn(Scene.PathToSnowhead, 0)]
        //EntrancePathtoSnowheadFromMountainVillage,

        //[Exit(Scene.Snowhead, 0)]
        //[Spawn(Scene.PathToSnowhead, 1)]
        //EntrancePathtoSnowheadFromSnowhead,

        // todo // void respawn // one way


        //[Exit(Scene.PathToSnowhead, 1)]
        //[Spawn(Scene.Snowhead, 0)]
        //EntranceSnowheadFromPathtoSnowhead,

        //[Exit(Scene.SnowheadTemple, 0)]
        //[Spawn(Scene.Snowhead, 1)]
        //EntranceSnowheadFromSnowheadTemple,

        [Exit(Scene.FairyFountain, 2)]
        [Spawn(Scene.Snowhead, 2)]
        EntranceSnowheadFromFairysFountain,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x04)]
        //[Spawn(Scene.Snowhead, 3)]
        //EntranceSnowheadFromOwlStatue, // one way

        //[Exit(Scene.Snowhead, 3)]
        //[Spawn(Scene.Snowhead, 0)]
        // EntranceSnowheadFromSnowhead, // void respawn // one way


        //[Exit(Scene.MountainVillage, 2)]
        //[Spawn(Scene.TwinIslands, 0)]
        //EntrancePathtoGoronVillageFromMountainVillage,

        //[Exit(Scene.GoronVillage, 0)]
        //[Spawn(Scene.TwinIslands, 1)]
        //EntrancePathtoGoronVillageFromGoronVillage,

        [Exit(Scene.GoronRacetrack, 0)]
        [Spawn(Scene.TwinIslands, 2)]
        EntrancePathtoGoronVillageFromGoronRacetrack,


        [Exit(Scene.IkanaCanyon, 9)]
        [Spawn(Scene.SecretShrine, 0)]
        EntranceSecretShrineFromIkanaCanyon,

        //[Exit(Scene.EastClockTown, 9)]
        //[Spawn(Scene.StockPotInn, 0)]
        //EntranceStockPotInnLowerFromEastClockTown,

        //[Exit(Scene.EastClockTown, 10)]
        //[Spawn(Scene.StockPotInn, 1)]
        //EntranceStockPotInnUpperFromEastClockTown,

        //[ExitAddress(0x10253E6)]
        //[Spawn(Scene.StockPotInn, 2)]
        //EntranceStockPotInnFromStockPotInn, // after grandma story // one way

        //[ExitAddress(0xFB9D8E)]
        //[Spawn(Scene.StockPotInn, 3)]
        //EntranceStockPotInnFromStockPotInn, // after midnight meeting // one way

        //[ExitAddress(0x1087702)]
        //[Spawn(Scene.StockPotInn, 4)]
        //EntranceStockPotInnFromStockPotInn, // eavesdropping on anju

        //[ExitAddress(0x1087666)]
        //[Spawn(Scene.StockPotInn, 5)]
        //EntranceStockPotInnFromStockPotInn, // after eavesdropping on anju


        // EntranceGreatBayCutsceneFromZoraCape, // pirates going to temple?


        //[Exit(Scene.BeforeThePortalToTermina, 1)]
        //[Spawn(Scene.ClockTowerInterior, 0)]
        //EntranceClockTowerInteriorFromBeforethePortaltoTermina,

        //[Exit(Scene.SouthClockTown, 0)]
        //[Spawn(Scene.ClockTowerInterior, 1)]
        //EntranceClockTowerInteriorFromSouthClockTown,

        //EntranceClockTowerInteriorFrom, // cutscenes
        //EntranceClockTowerInteriorFrom, // cutscenes
        //EntranceClockTowerInteriorFrom, // cutscenes
        //EntranceClockTowerInteriorFrom, // cutscenes
        //EntranceClockTowerInteriorFrom, // cutscenes


        [Exit(Scene.SouthernSwamp, 7)]
        [Spawn(Scene.WoodsOfMystery, 0)]
        EntranceWoodsofMysteryFromSouthernSwamp,


        //EntranceLostWoodsFrom, // cutscenes
        //EntranceLostWoodsFrom, // cutscenes


        //[Exit(Scene.TheMoon, 4)]
        //[Spawn(Scene.LinkTrial, 0)]
        //EntranceLinkTrialFromTheMoon,


        //[ExitCutscene(Scene.ClockTowerRoof, 2, 0)]
        //[ExitCutscene(Scene.ClockTowerRoof, 2, 1)]
        //[ExitCutscene(Scene.ClockTowerRoof, 2, 2)]
        //[Spawn(Scene.TheMoon, 0)]
        //EntranceTheMoonFromClockTowerRooftop, // one way

        //[Exit(Scene.DekuTrial, 0)]
        //[Spawn(Scene.TheMoon, 0)]
        //EntranceTheMoonFromDekuTrial,

        //[Exit(Scene.GoronTrial, 0)]
        //[Spawn(Scene.TheMoon, 0)]
        //EntranceTheMoonFromGoronTrial,

        //[Exit(Scene.ZoraTrial, 0)]
        //[Spawn(Scene.TheMoon, 0)]
        //EntranceTheMoonFromZoraTrial,

        //[Exit(Scene.LinkTrial, 0)]
        //[Spawn(Scene.TheMoon, 0)]
        //EntranceTheMoonFromLinkTrial,


        [Exit(Scene.WestClockTown, 6)]
        [Spawn(Scene.BombShop, 0)]
        EntranceBombShopFromWestClockTown,


        //EntranceGiantsChamberFrom, // cutscene // one way?


        //[Exit(Scene.MilkRoad, 3)]
        //[Spawn(Scene.GormanTrack, 0)]
        //EntranceGormanTrackFromMilkRoadMain,

        //[Exit(Scene.MilkRoad, 2)]
        //[Spawn(Scene.GormanTrack, 3)]
        //EntranceGormanTrackFromMilkRoadGated,

        //[ExitAddress(0xD7312E)]
        //[Spawn(Scene.GormanTrack, 2)]
        //EntranceGormanTrackFromGormanBrosRace,

        //[ExitAddress(0xFDF476)]
        //[ExitCutscene(Scene.RomaniRanch, 0, 2)]
        //[Spawn(Scene.GormanTrack, 4)]
        //EntranceGormanTrackFromCremiaEscort,

        //[ExitAddress(0xD6EBD6)]
        //[ExitAddress(0xD6F46E)]
        //[Spawn(Scene.GormanTrack, 5)]
        //EntranceGormanTrackRaceFromGormanTrack,


        [Exit(Scene.TwinIslands, 2)]
        [Spawn(Scene.GoronRacetrack, 0)]
        EntranceGoronRacetrackFromPathtoGoronVillage,

        //[ExitAddress(0xE413AE)]
        //[ExitAddress(0xFB6D62)]
        //[Spawn(Scene.GoronRacetrack, 1)]
        //EntranceGoronRacetrackFromGoronRacetrack, // race start // one way

        //[ExitAddress(0xE40D8E)]
        //[Spawn(Scene.GoronRacetrack, 2)]
        //EntranceGoronRacetrackFromGoronRacetrack, // race end // one way


        //[Exit(Scene.TerminaField, 7)]
        //[Spawn(Scene.EastClockTown, 0)]
        //EntranceEastClockTownFromTerminaField,

        //[Exit(Scene.SouthClockTown, 2)]
        //[Spawn(Scene.EastClockTown, 3)]
        //EntranceEastClockTownFromSouthClockTownNorthern,

        //[Exit(Scene.AstralObservatory, 0)]
        //[Spawn(Scene.EastClockTown, 2)]
        //EntranceEastClockTownFromAstralObservatory,

        //[Exit(Scene.SouthClockTown, 7)]
        //[Spawn(Scene.EastClockTown, 1)]
        //EntranceEastClockTownFromSouthClockTownSouthern,

        [Exit(Scene.TreasureChestShop, 0)]
        [Spawn(Scene.EastClockTown, 4)]
        EntranceEastClockTownFromTreasureChestShop,

        //[Exit(Scene.NorthClockTown, 1)]
        //[Spawn(Scene.EastClockTown, 5)]
        //EntranceEastClockTownFromNorthClockTown,

        [Exit(Scene.HoneyDarling, 0)]
        [Spawn(Scene.EastClockTown, 6)]
        EntranceEastClockTownFromHoneyDarlingsShop,

        [Exit(Scene.MayorsResidence, 0)]
        [Spawn(Scene.EastClockTown, 7)]
        EntranceEastClockTownFromMayorsResidence,

        [Exit(Scene.TownShootingGallery, 0)]
        [Spawn(Scene.EastClockTown, 8)]
        EntranceEastClockTownFromShootingGalleryClockTown,

        //[Exit(Scene.StockPotInn, 0)]
        //[Spawn(Scene.EastClockTown, 9)]
        //EntranceEastClockTownFromStockPotInnLower,

        //[Exit(Scene.StockPotInn, 1)]
        //[Spawn(Scene.EastClockTown, 10)]
        //EntranceEastClockTownFromStockPotInnUpper,

        [Exit(Scene.MilkBar, 0)]
        [Spawn(Scene.EastClockTown, 11)]
        EntranceEastClockTownFromMilkBar,


        //[Exit(Scene.TerminaField, 0)]
        //[Spawn(Scene.WestClockTown, 0)]
        //EntranceWestClockTownFromTerminaField,

        //[Exit(Scene.SouthClockTown, 5)]
        //[Spawn(Scene.WestClockTown, 1)]
        //EntranceWestClockTownFromSouthClockTownSouthern,

        //[Exit(Scene.SouthClockTown, 3)]
        //[Spawn(Scene.WestClockTown, 2)]
        //EntranceWestClockTownFromSouthClockTownNorthern,

        [Exit(Scene.SwordsmansSchool, 0)]
        [Spawn(Scene.WestClockTown, 3)]
        EntranceWestClockTownFromSwordsmansSchool,

        [Exit(Scene.CuriosityShop, 0)]
        [Spawn(Scene.WestClockTown, 4)]
        EntranceWestClockTownFromCuriosityShop,

        [Exit(Scene.TradingPost, 0)]
        [Spawn(Scene.WestClockTown, 5)]
        EntranceWestClockTownFromTradingPost,

        [Exit(Scene.BombShop, 0)]
        [Spawn(Scene.WestClockTown, 6)]
        EntranceWestClockTownFromBombShop,

        [Exit(Scene.PostOffice, 0)]
        [Spawn(Scene.WestClockTown, 7)]
        EntranceWestClockTownFromPostOffice,

        [Exit(Scene.LotteryShop, 0)]
        [Spawn(Scene.WestClockTown, 8)]
        EntranceWestClockTownFromLotteryShop,


        //[Exit(Scene.TerminaField, 8)]
        //[Spawn(Scene.NorthClockTown, 0)]
        //EntranceNorthClockTownFromTerminaField,

        //[Exit(Scene.EastClockTown, 5)]
        //[Spawn(Scene.NorthClockTown, 1)]
        //EntranceNorthClockTownFromEastClockTown,

        //[Exit(Scene.SouthClockTown, 4)]
        //[Spawn(Scene.NorthClockTown, 2)]
        //EntranceNorthClockTownFromSouthClockTown,

        [Exit(Scene.FairyFountain, 0)]
        [Spawn(Scene.NorthClockTown, 3)]
        EntranceNorthClockTownFromFairysFountain,

        //EntranceNorthClockTownFromBombersGame, // after catching kids // one way

        //EntranceNorthClockTownFromSavingLady, // after saving old lady // one way


        //[Exit(Scene.ClockTowerInterior, 1)]
        //[Spawn(Scene.SouthClockTown, 0)]
        //EntranceSouthClockTownFromClockTowerInterior, // spawn point

        //[Exit(Scene.TerminaField, 6)]
        //[Spawn(Scene.SouthClockTown, 1)]
        //EntranceSouthClockTownFromTerminaField,

        //[Exit(Scene.EastClockTown, 3)]
        //[Spawn(Scene.SouthClockTown, 2)]
        //EntranceSouthClockTownFromEastClockTownNorthern,

        //[Exit(Scene.WestClockTown, 2)]
        //[Spawn(Scene.SouthClockTown, 3)]
        //EntranceSouthClockTownFromWestClockTownNorthern,

        //[Exit(Scene.NorthClockTown, 2)]
        //[Spawn(Scene.SouthClockTown, 4)]
        //EntranceSouthClockTownFromNorthClockTown,

        //[Exit(Scene.WestClockTown, 1)]
        //[Spawn(Scene.SouthClockTown, 5)]
        //EntranceSouthClockTownFromWestClockTownSouthern,

        //[Exit(Scene.LaundryPool, 0)]
        //[Spawn(Scene.SouthClockTown, 6)]
        //EntranceSouthClockTownFromLaundryPool,

        //[Exit(Scene.EastClockTown, 1)]
        //[Spawn(Scene.SouthClockTown, 7)]
        //EntranceSouthClockTownFromEastClockTownSouthern,

        //[Spawn(Scene.SouthClockTown, 8)]
        //EntranceSouthClockTownFromClockTowerRooftop,

        //[ExitAddress((int)ExitAddressAttribute.BaseAddress.SongOfSoaring + 0x08)]
        //[Spawn(Scene.SouthClockTown, 9)]
        //EntranceSouthClockTownFromOwlStatue, // one way


        //[Exit(Scene.SouthClockTown, 6)]
        //[Spawn(Scene.LaundryPool, 0)]
        //EntranceLaundryPoolFromSouthClockTown,

        //[Exit(Scene.CuriosityShop, 1)]
        //[Spawn(Scene.LaundryPool, 1)]
        //EntranceLaundryPoolFromKafeisHideout,
    }
}
