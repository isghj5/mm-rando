﻿using MMR.Randomizer.LogicMigrator;
using MMR.Randomizer.Properties;
using MMR.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMR.UI.Forms
{
    public partial class LogicEditorForm : Form
    {
        private static readonly string[] DEFAULT_ITEM_NAMES = new string[] { "Deku Mask", "Hero's Bow", "Fire Arrow", "Ice Arrow", "Light Arrow", "Bomb Bag (20)", "Magic Bean", 
        "Powder Keg", "Pictobox", "Lens of Truth", "Hookshot", "Great Fairy Magic Meter", "Great Fairy Spin Attack", "Great Fairy Extended Magic", "Great Fairy Double Defense",
        "Great Fairy's Sword", "Witch Bottle", "Aliens Bottle", "Goron Race Bottle", "Beaver Race Bottle", "Dampe Bottle", "Chateau Bottle", "Bombers' Notebook", "Razor Sword",
        "Gilded Sword", "Mirror Shield", "Town Archery Quiver (40)", "Swamp Archery Quiver (50)", "Town Bomb Bag (30)", "Mountain Bomb Bag (40)", "Town Wallet (200)",
        "Ocean Wallet (500)", "Moon's Tear", "Land Title Deed", "Swamp Title Deed", "Mountain Title Deed", "Ocean Title Deed", "Room Key", "Letter to Kafei", "Pendant of Memories",
        "Letter to Mama", "Mayor Dotour HP", "Postman HP", "Rosa Sisters HP", "??? HP", "Grandma Short Story HP", "Grandma Long Story HP",
        "Keaton Quiz HP", "Deku Playground HP", "Town Archery HP", "Honey and Darling HP", "Swordsman's School HP", "Postbox HP",
        "Termina Field Gossips HP", "Termina Field Business Scrub HP", "Swamp Archery HP", "Pictograph Contest HP", "Boat Archery HP",
        "Frog Choir HP", "Beaver Race HP", "Seahorse HP", "Fisherman Game HP", "Evan HP", "Dog Race HP", "Poe Hut HP", 
        "Treasure Chest Game HP", "Peahat Grotto HP", "Dodongo Grotto HP", "Woodfall Chest HP", "Twin Islands Chest HP",
        "Ocean Spider House HP", "Graveyard Iron Knuckle HP", "Postman's Hat", "All Night Mask", "Blast Mask", "Stone Mask", "Great Fairy's Mask",
        "Keaton Mask", "Bremen Mask", "Bunny Hood", "Don Gero's Mask", "Mask of Scents", "Romani Mask", "Circus Leader's Mask", "Kafei's Mask",
        "Couple's Mask", "Mask of Truth", "Kamaro's Mask", "Gibdo Mask", "Garo Mask", "Captain's Hat", "Giant's Mask", "Goron Mask", "Zora Mask",
        "Ocarina of Time", "Song of Time", "Song of Healing", "Song of Soaring", "Epona's Song", "Song of Storms", "Sonata of Awakening", "Goron Lullaby",
        "New Wave Bossa Nova", "Elegy of Emptiness", "Oath to Order", "Poison swamp access", "Woodfall Temple access", "Woodfall clear", "North access",
        "Snowhead Temple access", "Snowhead clear", "Epona access", "West access", "Pirates' Fortress access", "Great Bay Temple access", "Great Bay clear", "East access",
        "Ikana Canyon access", "Stone Tower Temple access", "Inverted Stone Tower Temple access", "Ikana clear", "Explosives", "Arrows", "(Unused)", "(Unused)", 
        "(Unused)", "(Unused)", "(Unused)",
        "Woodfall Map", "Woodfall Compass", "Woodfall Boss Key", "Woodfall Key 1", "Snowhead Map", "Snowhead Compass", "Snowhead Boss Key",
        "Snowhead Key 1 - block room", "Snowhead Key 2 - icicle room", "Snowhead Key 3 - bridge room", "Great Bay Map", "Great Bay Compass", "Great Bay Boss Key", "Great Bay Key 1",
        "Stone Tower Map", "Stone Tower Compass", "Stone Tower Boss Key", "Stone Tower Key 1 - armos room", "Stone Tower Key 2 - eyegore room", "Stone Tower Key 3 - updraft room",
        "Stone Tower Key 4 - death armos maze", "Trading Post Red Potion", "Trading Post Green Potion", "Trading Post Shield", "Trading Post Fairy",
        "Trading Post Stick", "Trading Post Arrow 30", "Trading Post Nut 10", "Trading Post Arrow 50", "Witch Shop Blue Potion",
        "Witch Shop Red Potion", "Witch Shop Green Potion", "Bomb Shop Bomb 10", "Bomb Shop Chu 10", "Goron Shop Bomb 10", "Goron Shop Arrow 10",
        "Goron Shop Red Potion", "Zora Shop Shield", "Zora Shop Arrow 10", "Zora Shop Red Potion", "Bottle: Fairy", "Bottle: Deku Princess",
        "Bottle: Fish", "Bottle: Bug", "Bottle: Poe", "Bottle: Big Poe", "Bottle: Spring Water", "Bottle: Hot Spring Water", "Bottle: Zora Egg",
        "Bottle: Mushroom", "Lens Cave 20r", "Lens Cave 50r", "Bean Grotto 20r", "HSW Grotto 20r", "Graveyard Bad Bats", "Ikana Grotto",
        "PF 20r Lower", "PF 20r Upper", "PF Tank 20r", "PF Guard Room 100r", "PF HP Room 20r", "PF HP Room 5r", "PF Maze 20r", "PR 20r (1)", "PR 20r (2)",
        "Bombers' Hideout 100r", "Termina Bombchu Grotto", "Termina 20r Grotto", "Termina Underwater 20r", "Termina Grass 20r", "Termina Stump 20r",
        "Great Bay Coast Grotto", "Great Bay Cape Ledge (1)", "Great Bay Cape Ledge (2)", "Great Bay Cape Grotto", "Great Bay Cape Underwater", 
        "PF Exterior 20r (1)", "PF Exterior 20r (2)", "PF Exterior 20r (3)", "Path to Swamp Grotto", "Doggy Racetrack 50r", "Graveyard Grotto",
        "Swamp Grotto", "Woodfall 5r", "Woodfall 20r", "Well Right Path 50r", "Well Left Path 50r", "Mountain Village Chest (Spring)",
        "Mountain Village Grotto (Spring) 20r", "Path to Ikana 20r", "Path to Ikana Grotto", "Stone Tower 100r", "Stone Tower Bombchu 10",
        "Stone Tower Magic Bean", "Path to Snowhead Grotto", "Twin Islands 20r", "Secret Shrine HP", "Secret Shrine Dinolfos",
        "Secret Shrine Wizzrobe", "Secret Shrine Wart", "Secret Shrine Garo Master", "Inn Staff Room", "Inn Guest Room", "Mystery Woods Grotto",
        "East Clock Town 100r", "South Clock Town 20r", "South Clock Town 50r", "Bank HP", "South Clock Town HP", "North Clock Town HP",
        "Path to Swamp HP", "Swamp Scrub HP", "Deku Palace HP", "Goron Village Scrub HP", "Bio Baba Grotto HP", "Lab Fish HP", "Great Bay Like-Like HP",
        "Pirates' Fortress HP", "Zora Hall Scrub HP", "Path to Snowhead HP", "Great Bay Coast HP", "Ikana Scrub HP", "Ikana Castle HP", 
        "Odolwa Heart Container", "Goht Heart Container", "Gyorg Heart Container", "Twinmold Heart Container", "Map: Clock Town", "Map: Woodfall",
        "Map: Snowhead", "Map: Romani Ranch", "Map: Great Bay", "Map: Stone Tower", "Goron Racetrack Grotto", "Ikana Scrub 200r", "One Mask", "Two Masks",
        "Three Masks", "Four Masks", "Moon Access", "Deku Trial HP", "Goron Trial HP", "Zora Trial HP", "Link Trial HP", "Fierce Deity's Mask",
        "Link Trial 30 Arrows", "Link Trial 10 Bombchu", "Pre-Clocktown 10 Deku Nuts", "Starting Sword", "Starting Shield", "Starting Heart 1", "Starting Heart 2",
        "Ranch Cow #1 Milk", "Ranch Cow #2 Milk", "Ranch Cow #3 Milk", "Well Cow Milk", "Termina Grotto Cow #1 Milk", "Termina Grotto Cow #2 Milk",
        "Great Bay Coast Grotto Cow #1 Milk", "Great Bay Coast Grotto Cow #2 Milk",
        "Swamp Skulltula Main Room Near Ceiling", "Swamp Skulltula Gold Room Near Ceiling", "Swamp Skulltula Monument Room Torch", "Swamp Skulltula Gold Room Pillar", "Swamp Skulltula Pot Room Jar",

        "Swamp Skulltula Tree Room Grass 1", "Swamp Skulltula Tree Room Grass 2", "Swamp Skulltula Main Room Water", "Swamp Skulltula Main Room Lower Left Soft Soil", "Swamp Skulltula Monument Room Crate 1",

        "Swamp Skulltula Main Room Upper Soft Soil", "Swamp Skulltula Main Room Lower Right Soft Soil", "Swamp Skulltula Monument Room Lower Wall", "Swamp Skulltula Monument Room On Monument", "Swamp Skulltula Main Room Pillar",

        "Swamp Skulltula Pot Room Pot 1", "Swamp Skulltula Pot Room Pot 2", "Swamp Skulltula Gold Room Hive", "Swamp Skulltula Main Room Upper Pillar", "Swamp Skulltula Pot Room Behind Vines",

        "Swamp Skulltula Tree Room Tree 1", "Swamp Skulltula Pot Room Wall", "Swamp Skulltula Pot Room Hive 1", "Swamp Skulltula Tree Room Tree 2", "Swamp Skulltula Gold Room Wall",

        "Swamp Skulltula Tree Room Hive", "Swamp Skulltula Monument Room Crate 2", "Swamp Skulltula Pot Room Hive 2", "Swamp Skulltula Tree Room Tree 3", "Swamp Skulltula Main Room Jar",

        "Ocean Skulltula Storage Room Behind Boat", "Ocean Skulltula Library Hole Behind Picture", "Ocean Skulltula Library Hole Behind Cabinet", "Ocean Skulltula Library On Corner Bookshelf", "Ocean Skulltula 2nd Room Ceiling Edge",

        "Ocean Skulltula 2nd Room Ceiling Plank", "Ocean Skulltula Colored Skulls Ceiling Edge", "Ocean Skulltula Library Ceiling Edge", "Ocean Skulltula Storage Room Ceiling Web", "Ocean Skulltula Storage Room Behind Crate",

        "Ocean Skulltula 2nd Room Jar", "Ocean Skulltula Entrance Right Wall", "Ocean Skulltula Entrance Left Wall", "Ocean Skulltula 2nd Room Webbed Hole", "Ocean Skulltula Entrance Web",

        "Ocean Skulltula Colored Skulls Chandelier 1", "Ocean Skulltula Colored Skulls Chandelier 2", "Ocean Skulltula Colored Skulls Chandelier 3", "Ocean Skulltula Colored Skulls Behind Picture", "Ocean Skulltula Library Behind Picture",

        "Ocean Skulltula Library Behind Bookcase 1", "Ocean Skulltula Storage Room Crate", "Ocean Skulltula 2nd Room Webbed Pot", "Ocean Skulltula 2nd Room Upper Pot", "Ocean Skulltula Colored Skulls Pot",

        "Ocean Skulltula Storage Room Jar", "Ocean Skulltula 2nd Room Lower Pot", "Ocean Skulltula Library Behind Bookcase 2", "Ocean Skulltula 2nd Room Behind Skull 1", "Ocean Skulltula 2nd Room Behind Skull 2",

        "Clock Town Stray Fairy",
        "Woodfall Pre-Boss Lower Right Bubble",
        "Woodfall Entrance Fairy",
        "Woodfall Pre-Boss Upper Left Bubble",
        "Woodfall Pre-Boss Pillar Bubble",
        "Woodfall Deku Baba",
        "Woodfall Poison Water Bubble",
        "Woodfall Main Room Bubble",
        "Woodfall Skulltula",
        "Woodfall Pre-Boss Upper Right Bubble",
        "Woodfall Main Room Switch",
        "Woodfall Entrance Platform",
        "Woodfall Dark Room",
        "Woodfall Jar Fairy",
        "Woodfall Bridge Room Hive",
        "Woodfall Platform Room Hive",
        "Snowhead Snow Room Bubble",
        "Snowhead Ceiling Bubble",
        "Snowhead Dinolfos 1",
        "Snowhead Bridge Room Ledge Bubble",
        "Snowhead Bridge Room Pillar Bubble",
        "Snowhead Dinolfos 2",
        "Snowhead Map Room Fairy",
        "Snowhead Map Room Ledge",
        "Snowhead Basement",
        "Snowhead Twin Block",
        "Snowhead Icicle Room Wall",
        "Snowhead Main Room Wall",
        "Snowhead Pillar Freezards",
        "Snowhead Ice Puzzle",
        "Snowhead Crate",
        "Great Bay Skulltula",
        "Great Bay Pre-Boss Room Underwater Bubble",
        "Great Bay Water Control Room Underwater Bubble",
        "Great Bay Pre-Boss Room Bubble",
        "Great Bay Waterwheel Room Upper",
        "Great Bay Green Valve",
        "Great Bay Seesaw Room",
        "Great Bay Waterwheel Room Lower",
        "Great Bay Entrance Torches",
        "Great Bay Bio Babas",
        "Great Bay Underwater Barrel",
        "Great Bay Whirlpool Jar",
        "Great Bay Whirlpool Barrel",
        "Great Bay Dexihands Jar",
        "Great Bay Ledge Jar",
        "Stone Tower Mirror Sun Block",
        "Stone Tower Eyegore",
        "Stone Tower Lava Room Fire Ring",
        "Stone Tower Updraft Fire Ring",
        "Stone Tower Mirror Sun Switch",
        "Stone Tower Boss Warp",
        "Stone Tower Wizzrobe",
        "Stone Tower Death Armos",
        "Stone Tower Updraft Frozen Eye",
        "Stone Tower Thin Bridge",
        "Stone Tower Basement Ledge",
        "Stone Tower Statue Eye",
        "Stone Tower Underwater",
        "Stone Tower Bridge Crystal",
        "Stone Tower Lava Room Ledge",
        "Lottery 50r", "Bank 5r", "Milk Bar Chateau", "Milk Bar Milk", "Deku Playground 50r", "Honey and Darling 50r", "Kotake Mushroom Sale 20r", "Pictograph Contest 5r",
        "Pictograph Contest 20r", "Swamp Scrub Magic Bean", "Ocean Scrub Green Potion", "Canyon Scrub Blue Potion", "Zora Hall Stage Lights 5r", "Gorman Bros Purchase Milk",
        "Ocean Spider House 50r", "Ocean Spider House 20r", "Lulu Pictograph 5r", "Lulu Pictograph 20r", "Treasure Chest Game 50r", "Treasure Chest Game 20r",
        "Treasure Chest Game Deku Nuts", "Curiosity Shop 5r", "Curiosity Shop 20r", "Curiosity Shop 50r", "Curiosity Shop 200r", "Seahorse",

        "EntranceMayorsResidenceFromEastClockTown",

        "EntranceMajorasLairFromTheMoon",

        "EntranceMagicHagsPotionShopFromSouthernSwamp",

        "EntranceRanchBarnFromRomaniRanch",
        "EntranceRanchHouseFromRomaniRanch",


        "EntranceHoneyDarlingsShopFromEastClockTown",

        "EntranceBeneathGraveyardFromIkanaGraveyardNight2",
        "EntranceBeneathGraveyardFromIkanaGraveyardNight1",

        "EntranceSouthernSwampFromRoadtoSouthernSwamp",
        "EntranceSouthernSwampFromTouristInformation",
        "EntranceSouthernSwampFromWoodfall",
        "EntranceSouthernSwampFromDekuPalaceLower",
        "EntranceSouthernSwampFromDekuPalaceUpper",
        "EntranceSouthernSwampFromMagicHagsPotionShop",

        "EntranceSouthernSwampFromWoodsofMystery",
        "EntranceSouthernSwampFromSwampSpiderHouse",
        "EntranceSouthernSwampFromIkanaCanyon",
        "EntranceSouthernSwampFromOwlStatue",

        "EntranceCuriosityShopFromWestClockTown",
        "EntranceKafeisHideoutFromLaundryPool",
        "EntranceCuriosityShopFromKafeisHideout",
        "EntranceKafeisHideoutFromCuriosityShop",

        "EntranceGrottoGossipOceanFromTerminaField",
        "EntranceGrottoGossipSwampFromTerminaField",
        "EntranceGrottoGossipCanyonFromTerminaField",
        "EntranceGrottoGossipMountainFromTerminaField",
        "EntranceGrottoHotSpringWaterFromTwinIslands",
        "EntranceGrottoPalaceStraightFromDekuPalaceA",
        "EntranceGrottoDodongoFromTerminaField",
        "EntranceGrottoPalaceVinesFromDekuPalaceLower",
        "EntranceGrottoDekuMerchantFromTerminaField",
        "EntranceGrottoBioBabaFromTerminaField",
        "EntranceGrottoBeanSellerFromDekuPalace",
        "EntranceGrottoPeahatFromTerminaField",
        "EntranceGrottoPalaceStraightFromDekuPalaceB",
        "EntranceGrottoPalaceVinesFromDekuPalaceUpper",
        "EntranceGrottoLensCaveFromGoronVillage",

        "EntranceIkanaCanyonFromRoadtoIkana",
        "EntranceIkanaCanyonFromPoeHut",
        "EntranceIkanaCanyonFromMusicBoxHouse",
        "EntranceIkanaCanyonFromStoneTower",
        "EntranceIkanaCanyonFromOwlStatue",
        "EntranceIkanaCanyonFromBeneaththeWell",
        "EntranceIkanaCanyonFromSakonsHideout",
        "EntranceIkanaCanyonFromIkanaClear",
        "EntranceIkanaCanyonFromAncientCastleofIkanaCourtyard",


        "EntranceIkanaCanyonFromFairysFountain",
        "EntranceIkanaCanyonFromSecretShrine",
        "EntranceIkanaCanyonFromSpringWaterCave",
        "EntranceSpringWaterCaveFromIkanaCanyon",

        "EntrancePiratesFortressFromMainEntrance",
        "EntrancePiratesFortressFromHookshotRoomLower",
        "EntrancePiratesFortressFromHookshotRoomUpper",
        "EntrancePiratesFortressFromGuardRoomFront",
        "EntrancePiratesFortressFromGuardRoomBack",
        "EntrancePiratesFortressFromBarrelMazeFront",
        "EntrancePiratesFortressFromBarrelMazeBack",
        "EntrancePiratesFortressFromOnePatrolFront",
        "EntrancePiratesFortressFromOnePatrolBack",
        "EntrancePiratesFortressFromTelescope",
        "EntrancePiratesFortressFromPiratesFortressExteriorBalcony",

        "EntranceMilkBarFromEastClockTown",

        "EntranceStoneTowerTempleFromStoneTower",

        "EntranceTreasureChestShopFromEastClockTown",


        "EntranceStoneTowerTempleInvertedFromStoneTowerInverted",
        "EntranceStoneTowerTempleInvertedBossRoomFromStoneTowerTempleInverted",

        "EntranceClockTowerRooftopFromSouthClockTown",



        "EntranceBeforethePortaltoTerminaFromClockTowerInterior",
        "EntranceBeforethePortaltoTerminaFromBeforethePortaltoTermina",

        "EntranceWoodfallTempleFromWoodfall",
        "EntranceWoodfallTemplePrisonFromOdolwasLair",
        "EntranceWoodfallTemplePrisonFromWoodfall",

        "EntrancePathtoMountainVillageFromTerminaField",
        "EntrancePathtoMountainVillageFromMountainVillage",

        "EntranceAncientCastleofIkanaCourtyardFromBeneaththeWell",
        "EntranceAncientCastleofIkanaCourtyardFromIkanaCanyon",
        "EntranceAncientCastleofIkanaCourtyardFromAncientCastleofIkana",
        "EntranceAncientCastleofIkanaFromCourtyard",
        "EntranceAncientCastleofIkanaFromBlockHole",
        "EntranceAncientCastleofIkanaFromKegHole",
        "EntranceAncientCastleofIkanaFromIgosduIkanasLair",

        "EntranceDekuScrubPlaygroundFromNorthClockTown",


        "EntranceOdolwasLairFromWoodfallTemple",

        "EntranceTownShootingGalleryFromEastClockTown",

        "EntranceSnowheadTempleFromSnowhead",

        "EntranceMilkRoadFromTerminaField",
        "EntranceMilkRoadFromRomaniRanch",
        "EntranceMilkRoadFromGormanRacetrackTrack",
        "EntranceMilkRoadFromGormanRacetrackMain",
        "EntranceMilkRoadFromOwlStatue",



        "EntrancePiratesFortressHookshotRoomFromLowerDoor",
        "EntrancePiratesFortressHookshotRoomFromUpperDoor",
        "EntrancePiratesFortressGuardRoomFromFrontDoor",
        "EntrancePiratesFortressGuardRoomFromBackDoor",
        "EntrancePiratesFortressBarrelMazeFromFrontDoor",
        "EntrancePiratesFortressBarrelMazeFromBackDoor",
        "EntrancePiratesFortressOneGuardFrontFromPiratesFortress",
        "EntrancePiratesFortressOneGuardRearFromPiratesFortress",
        "EntrancePiratesFortressSewerFromTelescope",
        "EntrancePiratesFortressSewerFromWater",
        "EntrancePiratesFortressSewerFromRear",

        "EntranceSwampShootingGalleryFromRoadtoSouthernSwamp",

        "EntrancePinnacleRockFromGreatBayCoast",
        "EntrancePinnacleRockFromPinnacleRock",

        "EntranceFairysFountainFromNorthClockTown",
        "EntranceFairysFountainFromWoodfall",
        "EntranceFairysFountainFromSnowhead",
        "EntranceFairysFountainFromZoraCape",
        "EntranceFairysFountainFromIkanaCanyon",

        "EntranceSwampSpiderHouseFromSouthernSwamp",

        "EntranceOceansideSpiderHouseFromGreatBayCoast",

        "EntranceAstralObservatoryFromEastClockTown",
        "EntranceAstralObservatoryFromTerminaField",
        "EntranceAstralObservatoryFromTelescope",

        "EntranceDekuTrialFromTheMoon",

        "EntranceDekuPalaceFromSouthernSwampLower",
        "EntranceDekuPalaceFromDekuPalace",
        "EntranceDekuPalaceFromDekuKingsChamberMain",
        "EntranceDekuPalaceFromDekuKingsChamberGardenWest",
        "EntranceDekuPalaceFromDekuShrine",
        "EntranceDekuPalaceFromSouthernSwampUpper",
        "EntranceDekuPalaceGardenWestFromPalaceVinesGrotto",
        "EntranceDekuPalaceGardenWestFromPalaceStraightGrotto",
        "EntranceDekuPalaceGardenEastFromPalaceStraightGrotto",
        "EntranceDekuPalaceGardenEastFromBeanSellerGrotto",
        "EntranceDekuPalaceGardenEastFromPalaceVinesGrotto",

        "EntranceMountainSmithyFromMountainVillage",

        "EntranceTerminaFieldFromWestClockTown",
        "EntranceTerminaFieldFromRoadtoSouthernSwamp",
        "EntranceTerminaFieldFromGreatBayCoast",
        "EntranceTerminaFieldFromPathtoMountainVillage",
        "EntranceTerminaFieldFromRoadtoIkana",
        "EntranceTerminaFieldFromMilkRoad",
        "EntranceTerminaFieldFromSouthClockTown",
        "EntranceTerminaFieldFromEastClockTown",
        "EntranceTerminaFieldFromNorthClockTown",
        "EntranceTerminaFieldFromAstralObservatory",
        "EntranceTerminaFieldFromAstralObservatoryTelescope",

        "EntranceTerminaFieldFromCremiaEscort",


        "EntrancePostOfficeFromWestClockTown",

        "EntranceMarineResearchLabFromGreatBayCoast",

        "EntranceDampsHouseFromIkanaGraveyardGrave",
        "EntranceDampsHouseFromIkanaGraveyardDoor",

        "EntranceGoronShrineFromGoronVillage",
        "EntranceGoronShrineFromGoronShop",


        "EntranceZoraHallFromZoraCapeLand",
        "EntranceZoraHallFromZoraCapeWater",
        "EntranceZoraHallFromZoraShop",
        "EntranceZoraHallFromLulusRoom",
        "EntranceZoraHallFromEvansRoom",
        "EntranceZoraHallFromJapasRoom",
        "EntranceZoraHallFromMikauTijosRoom",

        "EntranceTradingPostFromWestClockTown",

        "EntranceRomaniRanchFromMilkRoad",

        "EntranceRomaniRanchFromBarn",
        "EntranceRomaniRanchFromRanchHouse",
        "EntranceRomaniRanchFromCuccoShack",
        "EntranceRomaniRanchFromDoggyRacetrack",


        "EntranceTwinmoldsLairFromStoneTowerTempleInverted",

        "EntranceGreatBayCoastFromTerminaField",
        "EntranceGreatBayCoastFromZoraCape",
        "EntranceGreatBayCoastFromGreatBayCoastBeach",
        "EntranceGreatBayCoastFromPinnacleRock",
        "EntranceGreatBayCoastFromFishermansHut",
        "EntranceGreatBayCoastFromPiratesFortress",
        "EntranceGreatBayCoastFromGreatBayCoastNearFortress",
        "EntranceGreatBayCoastFromMarineResearchLab",
        "EntranceGreatBayCoastFromOceansideSpiderHouse",
        "EntranceGreatBayCoastFromOwlStatue",
        "EntranceGreatBayCoastFromPiratesFortressThrownOut",


        "EntranceZoraCapeFromGreatBayCoast",
        "EntranceZoraCapeFromZoraHallLand",
        "EntranceZoraCapeFromZoraHallWater",
        "EntranceZoraCapeFromZoraCape",
        "EntranceZoraCapeFromWaterfallRapids",
        "EntranceZoraCapeFromFairysFountain",
        "EntranceZoraCapeFromOwlStatue",
        "EntranceZoraCapeFromGreatBayTemple",
        "EntranceZoraCapeFromGreatBayTempleClear",

        "EntranceLotteryShopFromWestClockTown",

        "EntrancePiratesFortressExteriorFromGreatBayCoast",
        "EntrancePiratesFortressExteriorFromPiratesFortressMain",
        "EntrancePiratesFortressExteriorFromPiratesFortressSewerMain",
        "EntrancePiratesFortressExteriorFromPiratesFortressSewerExhaust",
        "EntrancePiratesFortressExteriorFromThrownOut",
        "EntrancePiratesFortressExteriorFromPiratesFortressBalcony",
        "EntrancePiratesFortressExteriorFromPiratesFortressSewerDoor",

        "EntranceFishermansHutFromGreatBayCoast",

        "EntranceGoronShopFromGoronVillage",

        "EntranceDekuKingsChamberFromDekuPalace",
        "EntranceDekuKingsChamberFromDekuPalaceWestGarden",


        "EntranceGoronTrialFromTheMoon",

        "EntranceRoadtoSouthernSwampFromTerminaField",
        "EntranceRoadtoSouthernSwampFromSouthernSwamp",
        "EntranceRoadtoSouthernSwampFromSwampShootingGallery",

        "EntranceDoggyRacetrackFromRomaniRanch",

        "EntranceCuccoShackFromRomaniRanch",


        "EntranceIkanaGraveyardFromRoadtoIkana",
        "EntranceIkanaGraveyardFromDay3Grave",
        "EntranceIkanaGraveyardFromDay2Grave",
        "EntranceIkanaGraveyardFromDay1Grave",
        "EntranceIkanaGraveyardFromDampesHouse",


        "EntranceGohtsLairFromSnowheadTemple",

        "EntranceWoodfallFromSouthernSwamp",
        "EntranceWoodfallFromWoodfallTempleEntrance",
        "EntranceWoodfallFromFairysFountain",
        "EntranceWoodfallFromWoodfallTempleExit",
        "EntranceWoodfallFromOwlStatue",

        "EntranceZoraTrialFromTheMoon",
        "EntranceZoraTrialFromZoraTrial",

        "EntranceGoronVillageFromPathtoGoronVillage",
        "EntranceGoronVillageFromGoronShrine",
        "EntranceGoronVillageFromLonePeakShrine",

        "EntranceGreatBayTempleFromZoraCape",

        "EntranceWaterfallRapidsFromZoraCape",



        "EntranceBeneaththeWellFromIkanaCanyon",
        "EntranceBeneaththeWellFromAncientCastleofIkana",

        "EntranceZoraHallRoomsMikauTijosRoomFromZoraHall",
        "EntranceZoraHallRoomsJapasRoomFromZoraHall",
        "EntranceZoraHallRoomsLulusRoomFromZoraHall",
        "EntranceZoraHallRoomsEvansRoomFromZoraHall",
        "EntranceZoraHallRoomsJapasRoomFromJapasRoom",
        "EntranceZoraHallRoomsZoraShopFromZoraHall",


        "EntranceGoronGraveyardFromMountainVillage",


        "EntranceSakonsHideoutFromIkanaCanyon",

        "EntranceMountainVillageFromMountainSmithy",
        "EntranceMountainVillageFromPathtoGoronVillage",
        "EntranceMountainVillageFromGoronGraveyard",
        "EntranceMountainVillageFromPathtoSnowhead",

        "EntranceMountainVillageFromPathtoMountainVillage",
        "EntranceMountainVillageFromSnowheadClear",
        "EntranceMountainVillageFromOwlStatue",

        "EntrancePoeHutFromIkanaCanyon",



        "EntranceDekuShrineFromDekuPalace",

        "EntranceRoadtoIkanaFromTerminaField",
        "EntranceRoadtoIkanaFromIkanaCanyon",
        "EntranceRoadtoIkanaFromIkanaGraveyard",

        "EntranceSwordsmansSchoolFromWestClockTown",

        "EntranceMusicBoxHouseFromIkanaCanyon",

        "EntranceIgosduIkanasLairFromAncientCastleofIkana",

        "EntranceTouristInformationFromSouthernSwamp",

        "EntranceStoneTowerFromIkanaCanyon",

        "EntranceStoneTowerFromStoneTowerTemple",
        "EntranceStoneTowerFromOwlStatue",




        "EntrancePathtoSnowheadFromMountainVillage",
        "EntrancePathtoSnowheadFromSnowhead",


        "EntranceSnowheadFromPathtoSnowhead",
        "EntranceSnowheadFromSnowheadTemple",
        "EntranceSnowheadFromFairysFountain",
        "EntranceSnowheadFromOwlStatue",


        "EntrancePathtoGoronVillageFromMountainVillage",
        "EntrancePathtoGoronVillageFromGoronVillage",
        "EntrancePathtoGoronVillageFromGoronRacetrack",

        "EntranceGyorgsLairFromGreatBayTemple",

        "EntranceSecretShrineFromIkanaCanyon",

        "EntranceStockPotInnLowerFromEastClockTown",
        "EntranceStockPotInnUpperFromEastClockTown",





        "EntranceClockTowerInteriorFromBeforethePortaltoTermina",
        "EntranceClockTowerInteriorFromSouthClockTown",






        "EntranceWoodsofMysteryFromSouthernSwamp",




        "EntranceLinkTrialFromTheMoon",

        "EntranceTheMoonFromClockTowerRooftop",
        "EntranceTheMoonFromDekuTrial",
        "EntranceTheMoonFromGoronTrial",
        "EntranceTheMoonFromZoraTrial",
        "EntranceTheMoonFromLinkTrial",

        "EntranceBombShopFromWestClockTown",



        "EntranceGormanTrackFromMilkRoadMain",
        "EntranceGormanTrackFromMilkRoadGated",


        "EntranceGoronRacetrackFromPathtoMountainVillage",



        "EntranceEastClockTownFromTerminaField",
        "EntranceEastClockTownFromSouthClockTownNorthern",
        "EntranceEastClockTownFromAstralObservatory",
        "EntranceEastClockTownFromSouthClockTownSouthern",
        "EntranceEastClockTownFromTreasureChestShop",
        "EntranceEastClockTownFromNorthClockTown",
        "EntranceEastClockTownFromHoneyDarlingsShop",
        "EntranceEastClockTownFromMayorsResidence",
        "EntranceEastClockTownFromShootingGalleryClockTown",
        "EntranceEastClockTownFromStockPotInnLower",
        "EntranceEastClockTownFromStockPotInnUpper",
        "EntranceEastClockTownFromMilkBar",

        "EntranceWestClockTownFromTerminaField",
        "EntranceWestClockTownFromSouthClockTownSouthern",
        "EntranceWestClockTownFromSouthClockTownNorthern",
        "EntranceWestClockTownFromSwordsmansSchool",
        "EntranceWestClockTownFromCuriosityShop",
        "EntranceWestClockTownFromTradingPost",
        "EntranceWestClockTownFromBombShop",
        "EntranceWestClockTownFromPostOffice",
        "EntranceWestClockTownFromLotteryShop",

        "EntranceNorthClockTownFromTerminaField",
        "EntranceNorthClockTownFromEastClockTown",
        "EntranceNorthClockTownFromSouthClockTown",
        "EntranceNorthClockTownFromFairysFountain",
        "EntranceNorthClockTownFromDekuScrubPlayground",



        "EntranceSouthClockTownFromClockTowerInterior",
        "EntranceSouthClockTownFromTerminaField",
        "EntranceSouthClockTownFromEastClockTownNorthern",
        "EntranceSouthClockTownFromWestClockTownNorthern",
        "EntranceSouthClockTownFromNorthClockTown",
        "EntranceSouthClockTownFromWestClockTownSouthern",
        "EntranceSouthClockTownFromLaundryPool",
        "EntranceSouthClockTownFromEastClockTownSouthern",
        "EntranceSouthClockTownFromClockTowerRooftop",
        "EntranceSouthClockTownFromOwlStatue",

        "EntranceLaundryPoolFromSouthClockTown",
        "EntranceLaundryPoolFromKafeisHideout",

        "EntranceStoneTowerInvertedFromStoneTowerTempleInverted",

        "Great Bay Owl", "Zora Cape Owl", "Snowhead Owl", "Mountain Village Owl", "Clock Town Owl", "Milk Road Owl", "Woodfall Owl", "Southern Swamp Owl", "Ikana Canyon Owl", "Stone Tower Owl",

        "GossipTerminaSouth",
        "GossipSwampPotionShop",
        "GossipMountainSpringPath",
        "GossipMountainPath",
        "GossipOceanZoraGame",
        "GossipCanyonRoad",
        "GossipCanyonDock",
        "GossipCanyonSpiritHouse",
        "GossipTerminaMilk",
        "GossipTerminaWest",
        "GossipTerminaNorth",
        "GossipTerminaEast",
        "GossipRanchTree",
        "GossipRanchBarn",
        "GossipMilkRoad",
        "GossipOceanFortress",
        "GossipSwampRoad",
        "GossipTerminaObservatory",
        "GossipRanchCuccoShack",
        "GossipRanchRacetrack",
        "GossipRanchEntrance",
        "GossipCanyonRavine",
        "GossipMountainSpringFrog",
        "GossipSwampSpiderHouse",
        "GossipTerminaGossipLarge",
        "GossipTerminaGossipGuitar",
        "GossipTerminaGossipPipes",
        "GossipTerminaGossipDrums",
        };

        string[] ITEM_NAMES = DEFAULT_ITEM_NAMES.ToArray();

        bool updating = false;
        int n;
        
        public class ItemLogic
        {
            public List<int> Dependence = new List<int>();
            public List<List<int>> Conditional = new List<List<int>>();
            public int Time_Needed = new int();
            public int Time_Available = new int();
            public bool IsTrick = false;
            public string TrickTooltip = null;
        }

        List<ItemLogic> ItemList;

        private void CheckOnTrue(CheckBox a, int n, int m)
        {
            if (((n >> m) & 1) > 0)
            {
                a.Checked = true;
            }
            else
            {
                a.Checked = false;
            };
        }

        private void FillDependence(int n)
        {
            lRequired.Items.Clear();
            for (int i = 0; i < ItemList[n].Dependence.Count; i++)
            {
                lRequired.Items.Add(ITEM_NAMES[ItemList[n].Dependence[i]]);
            };
        }

        private void UpdateDependence(int n)
        {
            ItemSelectorForm ItemSelect = new ItemSelectorForm();
            DialogResult R = ItemSelect.ShowDialog();
            if (R == DialogResult.OK)
            {
                List<int> Returned = ItemSelectorForm.ReturnItems;
                if (Returned.Count == 0)
                {
                    return;
                };
                for (int i = 0; i < Returned.Count; i++)
                {
                    if (!ItemList[n].Dependence.Contains(Returned[i]))
                    {
                        ItemList[n].Dependence.Add(Returned[i]);
                    };
                };
                FillDependence(n);
            };
        }

        private void FillConditional(int n)
        {
            lConditional.Items.Clear();
            for (int i = 0; i < ItemList[n].Conditional.Count; i++)
            {
                string l = "";
                for (int j = 0; j < ItemList[n].Conditional[i].Count; j++)
                {
                    l += ITEM_NAMES[ItemList[n].Conditional[i][j]];
                    if (j != ItemList[n].Conditional[i].Count - 1)
                    {
                        l += ",";
                    };
                };
                lConditional.Items.Add(l);
            };
        }

        private void UpdateConditional(int n, int? conditionalIndex = null)
        {
            List<int> selectedItems = null;
            if (conditionalIndex.HasValue)
            {
                selectedItems = ItemList[n].Conditional[conditionalIndex.Value];
            }
            ItemSelectorForm ItemSelect = new ItemSelectorForm(selectedItems);
            DialogResult R = ItemSelect.ShowDialog();
            if (R == DialogResult.OK)
            {
                List<int> Returned = ItemSelectorForm.ReturnItems;
                if (Returned.Count == 0)
                {
                    return;
                };
                if (conditionalIndex.HasValue)
                {
                    ItemList[n].Conditional[conditionalIndex.Value] = Returned;
                }
                else
                {
                    ItemList[n].Conditional.Add(Returned);
                }
                FillConditional(n);
            };
        }

        private void FillTime(int n)
        {
            CheckOnTrue(cNDay1, ItemList[n].Time_Needed, 0);
            CheckOnTrue(cNNight1, ItemList[n].Time_Needed, 1);
            CheckOnTrue(cNDay2, ItemList[n].Time_Needed, 2);
            CheckOnTrue(cNNight2, ItemList[n].Time_Needed, 3);
            CheckOnTrue(cNDay3, ItemList[n].Time_Needed, 4);
            CheckOnTrue(cNNight3, ItemList[n].Time_Needed, 5);
            CheckOnTrue(cADay1, ItemList[n].Time_Available, 0);
            CheckOnTrue(cANight1, ItemList[n].Time_Available, 1);
            CheckOnTrue(cADay2, ItemList[n].Time_Available, 2);
            CheckOnTrue(cANight2, ItemList[n].Time_Available, 3);
            CheckOnTrue(cADay3, ItemList[n].Time_Available, 4);
            CheckOnTrue(cANight3, ItemList[n].Time_Available, 5);
        }

        private void UpdateTime(int n)
        {
            int Av = 0;
            if (cADay1.Checked) { Av += 1; };
            if (cANight1.Checked) { Av += 2; };
            if (cADay2.Checked) { Av += 4; };
            if (cANight2.Checked) { Av += 8; };
            if (cADay3.Checked) { Av += 16; };
            if (cANight3.Checked) { Av += 32; };
            ItemList[n].Time_Available = Av;
            int Ne = 0;
            if (cNDay1.Checked) { Ne += 1; };
            if (cNNight1.Checked) { Ne += 2; };
            if (cNDay2.Checked) { Ne += 4; };
            if (cNNight2.Checked) { Ne += 8; };
            if (cNDay3.Checked) { Ne += 16; };
            if (cNNight3.Checked) { Ne += 32; };
            ItemList[n].Time_Needed = Ne;
        }

        private void FillTrick(int n)
        {
            cTrick.Checked = ItemList[n].IsTrick;
            tTrickDescription.Text = ItemList[n].TrickTooltip ?? "(optional tooltip)";
            tTrickDescription.ForeColor = ItemList[n].TrickTooltip != null ? SystemColors.WindowText : SystemColors.WindowFrame;
        }

        public LogicEditorForm()
        {
            InitializeComponent();
            nItem.Minimum = 0;
            nItem.Maximum = ITEM_NAMES.Length - 1;
            ItemList = new List<ItemLogic>();
            for (int i = 0; i < ITEM_NAMES.Length; i++)
            {
                ItemLogic l = new ItemLogic();
                ItemList.Add(l);
            };
            nItem.Value = 0;
        }

        private void fLogicEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            };
        }

        private void nItem_ValueChanged(object sender, EventArgs e)
        {
            SetIndex((int)nItem.Value);
        }

        private void SetIndex(int index)
        {
            n = index;
            lIName.Text = ITEM_NAMES[n];
            updating = true;
            FillDependence(n);
            FillConditional(n);
            FillTime(n);
            FillTrick(n);
            bRenameItem.Visible = index >= DEFAULT_ITEM_NAMES.Length;
            bDeleteItem.Visible = index >= DEFAULT_ITEM_NAMES.Length;
            cTrick.Visible = index >= DEFAULT_ITEM_NAMES.Length;
            tTrickDescription.Visible = index >= DEFAULT_ITEM_NAMES.Length;
            updating = false;
        }

        private void cNDay1_CheckedChanged(object sender, EventArgs e)
        {
            if (updating)
            {
                return;
            };
            UpdateTime(n);
        }

        private void bReqAdd_Click(object sender, EventArgs e)
        {
            UpdateDependence(n);
        }

        private void bConAdd_Click(object sender, EventArgs e)
        {
            UpdateConditional(n);
        }

        private void bReqClear_Click(object sender, EventArgs e)
        {
            if (lRequired.SelectedIndex != -1)
            {
                ItemList[n].Dependence.RemoveAt(lRequired.SelectedIndex);
                FillDependence(n);
            };
        }

        private void bConClear_Click(object sender, EventArgs e)
        {
            if (lConditional.SelectedIndex != -1)
            {
                ItemList[n].Conditional.RemoveAt(lConditional.SelectedIndex);
                FillConditional(n);
            };
        }

        private void mNew_Click(object sender, EventArgs e)
        {
            ItemSelectorForm.ResetItems();
            ITEM_NAMES = DEFAULT_ITEM_NAMES.ToArray();
            nItem.Minimum = 0;
            nItem.Maximum = ITEM_NAMES.Length - 1;
            ItemList = new List<ItemLogic>();
            for (int i = 0; i < ITEM_NAMES.Length; i++)
            {
                ItemLogic l = new ItemLogic();
                ItemList.Add(l);
            };
            nItem.Value = 1;
            nItem.Value = 0;
        }

        private void mImport_Click(object sender, EventArgs e)
        {
            if (openLogic.ShowDialog() == DialogResult.OK)
            {
                using (var logicFile = new StreamReader(File.OpenRead(openLogic.FileName)))
                {
                    var logicString = logicFile.ReadToEnd();
                    LoadLogic(logicString);
                }
            }
        }

        private void mSave_Click(object sender, EventArgs e)
        {
            if (saveLogic.ShowDialog() == DialogResult.OK)
            {
                var builder = new StringBuilder();
                builder.AppendLine($"-version {Migrator.CurrentVersion}");
                for (int i = 0; i < ItemList.Count; i++)
                {
                    var itemLogic = ItemList[i];

                    if (itemLogic.Dependence.Any(item => ItemList[item].IsTrick))
                    {
                        MessageBox.Show($"Dependencies of {ITEM_NAMES[i]} are not valid. Cannot have tricks as Dependencies.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (itemLogic.Conditional.Any() && itemLogic.Conditional.All(c => c.Any(item => ItemList[item].IsTrick)))
                    {
                        MessageBox.Show($"Conditionals of {ITEM_NAMES[i]} are not valid. Must have at least one conditional that isn't a trick.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    builder.AppendLine("- " + ITEM_NAMES[i]);
                    for (int j = 0; j < itemLogic.Dependence.Count; j++)
                    {
                        builder.Append(itemLogic.Dependence[j].ToString());
                        if (j != itemLogic.Dependence.Count - 1)
                        {
                            builder.Append(",");
                        }
                    }
                    builder.AppendLine();
                    for (int j = 0; j < itemLogic.Conditional.Count; j++)
                    {
                        for (int k = 0; k < itemLogic.Conditional[j].Count; k++)
                        {
                            builder.Append(itemLogic.Conditional[j][k].ToString());
                            if (k != itemLogic.Conditional[j].Count - 1)
                            {
                                builder.Append(",");
                            }
                        }
                        if (j != itemLogic.Conditional.Count - 1)
                        {
                            builder.Append(";");
                        }
                    }
                    builder.AppendLine();
                    builder.AppendLine(itemLogic.Time_Needed.ToString());
                    builder.AppendLine(itemLogic.Time_Available.ToString());

                    var lastLine = "";
                    if (itemLogic.IsTrick)
                    {
                        lastLine += ";";
                        if (itemLogic.TrickTooltip != null)
                        {
                            lastLine += itemLogic.TrickTooltip;
                        }
                    }
                    if (i != ItemList.Count - 1)
                    {
                        builder.AppendLine(lastLine);
                    }
                    else
                    {
                        builder.Append(lastLine);
                    }
                }

                using (var writer = new StreamWriter(File.Open(saveLogic.FileName, FileMode.Create)))
                {
                    writer.Write(builder);
                }
            }
        }

        private void btn_new_item_Click(object sender, EventArgs e)
        {
            using (var newItemForm = new NewItemForm())
            {
                var result = newItemForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var newList = ITEM_NAMES.ToList();
                    newList.Add(newItemForm.ReturnValue);
                    ITEM_NAMES = newList.ToArray();
                    nItem.Maximum = ITEM_NAMES.Length - 1;
                    ItemList.Add(new ItemLogic());
                    nItem.Value = nItem.Maximum;
                    ItemSelectorForm.AddItem(newItemForm.ReturnValue);
                }
            }
        }

        private void lConditional_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = lConditional.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var conditions = ItemList[n].Conditional[index];
                if (conditions.Count == 1)
                {
                    nItem.Value = conditions[0];
                }
                else
                {
                    var itemSelect = new ItemSelectorForm(checkboxes: false, highlightedItems: conditions);
                    var result = itemSelect.ShowDialog();
                    if (result == DialogResult.OK && ItemSelectorForm.ReturnItems.Any())
                    {
                        var itemIndex = ItemSelectorForm.ReturnItems.First();
                        nItem.Value = itemIndex;
                    }
                }
            }
        }

        private void button_goto_Click(object sender, EventArgs e)
        {
            var itemSelect = new ItemSelectorForm(checkboxes: false);
            var result = itemSelect.ShowDialog();
            if (result == DialogResult.OK && ItemSelectorForm.ReturnItems.Any())
            {
                var itemIndex = ItemSelectorForm.ReturnItems.First();
                nItem.Value = itemIndex;
            }
        }

        private void lRequired_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var index = lRequired.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var gotoItemIndex = ItemList[n].Dependence[index];
                nItem.Value = gotoItemIndex;
            }
        }

        private void bConEdit_MouseClick(object sender, MouseEventArgs e)
        {
            var index = lConditional.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                UpdateConditional(n, index);
            }
        }

        private void bConClone_Click(object sender, EventArgs e)
        {
            var index = lConditional.SelectedIndex;
            if (index != ListBox.NoMatches)
            {
                ItemList[n].Conditional.Insert(index + 1, ItemList[n].Conditional[index]);
                FillConditional(n);
            }
        }

        private void casualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadLogic(Resources.REQ_CASUAL);
        }

        private void glitchedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoadLogic(Resources.REQ_GLITCH);
        }

        private void LoadLogic(string logicString)
        {
            logicString = Migrator.ApplyMigrations(logicString);
            ItemList = new List<ItemLogic>();
            string[] lines = logicString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            ItemSelectorForm.ResetItems();
            ITEM_NAMES = DEFAULT_ITEM_NAMES.ToArray();
            int i = 0;
            while (true)
            {
                if (i == lines.Length)
                {
                    break;
                }
                if (lines[i].StartsWith("#"))
                {
                    i++;
                    continue;
                }
                if (lines[i].Contains("-"))
                {
                    var itemName = lines[i].Substring(2);
                    if (ItemList.Count >= ITEM_NAMES.Length)
                    {
                        var newList = ITEM_NAMES.ToList();
                        newList.Add(itemName);
                        ItemSelectorForm.AddItem(itemName);
                        ITEM_NAMES = newList.ToArray();
                    }
                    i++;
                    continue;
                }
                else
                {
                    ItemLogic l = new ItemLogic();
                    l.Dependence = new List<int>();
                    if (lines[i] != "")
                    {
                        foreach (string j in lines[i].Split(','))
                        {
                            l.Dependence.Add(Convert.ToInt32(j));
                        };
                    };
                    l.Conditional = new List<List<int>>();
                    if (lines[i + 1] != "")
                    {
                        foreach (string j in lines[i + 1].Split(';'))
                        {
                            List<int> c = new List<int>();
                            foreach (string k in j.Split(','))
                            {
                                c.Add(Convert.ToInt32(k));
                            };
                            l.Conditional.Add(c);
                        };
                    };
                    l.Time_Needed = Convert.ToInt32(lines[i + 2]);
                    l.Time_Available = Convert.ToInt32(lines[i + 3]);
                    var trickInfo = lines[i + 4].Split(new char[] { ';' }, 2);
                    l.IsTrick = trickInfo.Length > 1;
                    l.TrickTooltip = l.IsTrick ? trickInfo[1] : null;
                    if (string.IsNullOrWhiteSpace(l.TrickTooltip))
                    {
                        l.TrickTooltip = null;
                    }
                    ItemList.Add(l);
                    i += 5;
                };
            };
            
            nItem.Maximum = ITEM_NAMES.Length - 1;
            SetIndex((int)nItem.Value);
        }

        private void bRenameItem_Click(object sender, EventArgs e)
        {
            using (var newItemForm = new NewItemForm())
            {
                var result = newItemForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ITEM_NAMES[n] = newItemForm.ReturnValue;
                    ItemSelectorForm.RenameItem(n, newItemForm.ReturnValue);
                    lIName.Text = newItemForm.ReturnValue;
                }
            }
        }

        private void bDeleteItem_Click(object sender, EventArgs e)
        {
            string message;
            string caption;
            MessageBoxButtons buttons;
            var usedBy = ItemList.Where(il => il.Dependence.Contains(n) || il.Conditional.Any(c => c.Contains(n))).ToList();
            if (usedBy.Any())
            {
                // in use
                caption = "Error";
                message = "This item is in use by:\n"+ string.Join("\n", usedBy.Take(5).Select(il => "* " + ITEM_NAMES[ItemList.IndexOf(il)]));
                if (usedBy.Count > 5)
                {
                    message += $"\nand {usedBy.Count-5} other{(usedBy.Count > 6 ? "s" : "")}.";
                }
                buttons = MessageBoxButtons.OK;
            }
            else
            {
                // not in use
                caption = "Warning";
                message = "Are you sure you want to delete this item?";
                buttons = MessageBoxButtons.YesNo;
            }
            var result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                ItemList.RemoveAt(n);
                ITEM_NAMES = ITEM_NAMES.Where((_, i) => i != n).ToArray();
                ItemSelectorForm.RemoveItem(n);
                foreach (var itemLogic in ItemList)
                {
                    for (var i = 0; i < itemLogic.Dependence.Count; i++)
                    {
                        if (itemLogic.Dependence[i] > n)
                        {
                            itemLogic.Dependence[i]--;
                        }
                    }
                    foreach (var conditionals in itemLogic.Conditional)
                    {
                        for (var i = 0; i < conditionals.Count; i++)
                        {
                            if (conditionals[i] > n)
                            {
                                conditionals[i]--;
                            }
                        }
                    }
                }
                SetIndex(n);
            }
        }

        private const string DEFAULT_TRICK_TOOLTIP = "(optional tooltip)";

        private void tTrickDescription_TextChanged(object sender, EventArgs e)
        {
            ItemList[n].TrickTooltip = string.IsNullOrWhiteSpace(tTrickDescription.Text) || tTrickDescription.Text == DEFAULT_TRICK_TOOLTIP
                ? null
                : tTrickDescription.Text;
        }

        private void tTrickDescription_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemList[n].TrickTooltip))
            {
                tTrickDescription.Text = string.Empty;
                tTrickDescription.ForeColor = SystemColors.WindowText;
            }
        }

        private void cTrick_CheckedChanged(object sender, EventArgs e)
        {
            ItemList[n].IsTrick = cTrick.Checked;
        }

        private void tTrickDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemList[n].TrickTooltip))
            {
                tTrickDescription.Text = DEFAULT_TRICK_TOOLTIP;
                tTrickDescription.ForeColor = SystemColors.WindowFrame;
            }
        }
    }
}
