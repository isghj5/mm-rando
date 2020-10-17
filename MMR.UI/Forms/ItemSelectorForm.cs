﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MMR.UI.Forms
{
    public partial class ItemSelectorForm : Form
    {
        private readonly static string[] DEFAULT_ITEM_NAMES = new string[] 
        {
            "Deku Mask",
            "Hero's Bow",
            "Fire Arrow",
            "Ice Arrow",
            "Light Arrow",
            "Bomb Bag (20)",
            "Magic Bean",
            "Powder Keg",
            "Pictobox",
            "Lens of Truth",
            "Hookshot",
            "Great Fairy Magic Meter",
            "Great Fairy Spin Attack",
            "Great Fairy Extended Magic",
            "Great Fairy Double Defense",
            "Great Fairy's Sword",
            "Witch Bottle",
            "Aliens Bottle",
            "Goron Race Bottle",
            "Beaver Race Bottle",
            "Dampe Bottle",
            "Chateau Bottle",
            "Bombers' Notebook",
            "Razor Sword",
            "Gilded Sword",
            "Mirror Shield",
            "Town Archery Quiver (40)",
            "Swamp Archery Quiver (50)",
            "Town Bomb Bag (30)",
            "Mountain Bomb Bag (40)",
            "Town Wallet (200)",
            "Ocean Wallet (500)",
            "Moon's Tear",
            "Land Title Deed",
            "Swamp Title Deed",
            "Mountain Title Deed",
            "Ocean Title Deed",
            "Room Key",
            "Letter to Kafei",
            "Pendant of Memories",
            "Letter to Mama",
            "Mayor Dotour HP",
            "Postman HP",
            "Rosa Sisters HP",
            "??? HP",
            "Grandma Short Story HP",
            "Grandma Long Story HP",
            "Keaton Quiz HP",
            "Deku Playground HP",
            "Town Archery HP",
            "Honey and Darling HP",
            "Swordsman's School HP",
            "Postbox HP",
            "Termina Field Gossips HP",
            "Termina Field Business Scrub HP",
            "Swamp Archery HP",
            "Pictograph Contest HP",
            "Boat Archery HP",
            "Frog Choir HP",
            "Beaver Race HP",
            "Seahorse HP",
            "Fisherman Game HP",
            "Evan HP",
            "Dog Race HP",
            "Poe Hut HP",
            "Treasure Chest Game HP",
            "Peahat Grotto HP",
            "Dodongo Grotto HP",
            "Woodfall Chest HP",
            "Twin Islands Chest HP",
            "Ocean Spider House HP",
            "Graveyard Iron Knuckle HP",
            "Postman's Hat",
            "All Night Mask",
            "Blast Mask",
            "Stone Mask",
            "Great Fairy's Mask",
            "Keaton Mask",
            "Bremen Mask",
            "Bunny Hood",
            "Don Gero's Mask",
            "Mask of Scents",
            "Romani Mask",
            "Circus Leader's Mask",
            "Kafei's Mask",
            "Couple's Mask",
            "Mask of Truth",
            "Kamaro's Mask",
            "Gibdo Mask",
            "Garo Mask",
            "Captain's Hat",
            "Giant's Mask",
            "Goron Mask",
            "Zora Mask",
            "Ocarina of Time",
            "Song of Time",
            "Song of Healing",
            "Song of Soaring",
            "Epona's Song",
            "Song of Storms",
            "Sonata of Awakening",
            "Goron Lullaby",
            "New Wave Bossa Nova",
            "Elegy of Emptiness",
            "Oath to Order",
            "Poison swamp access",
            "Woodfall Temple access",
            "Woodfall clear",
            "North access",
            "Snowhead Temple access",
            "Snowhead clear",
            "Epona access",
            "West access",
            "Pirates' Fortress access",
            "Great Bay Temple access",
            "Great Bay clear",
            "East access",
            "Ikana Canyon access",
            "Stone Tower Temple access",
            "Inverted Stone Tower Temple access",
            "Ikana clear",
            "Explosives",
            "Arrows",
            "(Unused)",
            "(Unused)",
            "(Unused)",
            "(Unused)",
            "(Unused)",
            "Woodfall Map",
            "Woodfall Compass",
            "Woodfall Boss Key",
            "Woodfall Key 1",
            "Snowhead Map",
            "Snowhead Compass",
            "Snowhead Boss Key",
            "Snowhead Key 1 - block room",
            "Snowhead Key 2 - icicle room",
            "Snowhead Key 3 - bridge room",
            "Great Bay Map",
            "Great Bay Compass",
            "Great Bay Boss Key",
            "Great Bay Key 1",
            "Stone Tower Map",
            "Stone Tower Compass",
            "Stone Tower Boss Key",
            "Stone Tower Key 1 - armos room",
            "Stone Tower Key 2 - eyegore room",
            "Stone Tower Key 3 - updraft room",
            "Stone Tower Key 4 - death armos maze",
            "Trading Post Red Potion",
            "Trading Post Green Potion",
            "Trading Post Shield",
            "Trading Post Fairy",
            "Trading Post Stick",
            "Trading Post Arrow 30",
            "Trading Post Nut 10",
            "Trading Post Arrow 50",
            "Witch Shop Blue Potion",
            "Witch Shop Red Potion",
            "Witch Shop Green Potion",
            "Bomb Shop Bomb 10",
            "Bomb Shop Chu 10",
            "Goron Shop Bomb 10",
            "Goron Shop Arrow 10",
            "Goron Shop Red Potion",
            "Zora Shop Shield",
            "Zora Shop Arrow 10",
            "Zora Shop Red Potion",
            "Bottle: Fairy",
            "Bottle: Deku Princess",
            "Bottle: Fish",
            "Bottle: Bug",
            "Bottle: Poe",
            "Bottle: Big Poe",
            "Bottle: Spring Water",
            "Bottle: Hot Spring Water",
            "Bottle: Zora Egg",
            "Bottle: Mushroom",
            "Lens Cave 20r",
            "Lens Cave 50r",
            "Bean Grotto 20r",
            "HSW Grotto 20r",
            "Graveyard Bad Bats",
            "Ikana Grotto",
            "PF 20r Lower",
            "PF 20r Upper",
            "PF Tank 20r",
            "PF Guard Room 100r",
            "PF HP Room 20r",
            "PF HP Room 5r",
            "PF Maze 20r",
            "PR 20r (1)",
            "PR 20r (2)",
            "Bombers' Hideout 100r",
            "Termina Bombchu Grotto",
            "Termina 20r Grotto",
            "Termina Underwater 20r",
            "Termina Grass 20r",
            "Termina Stump 20r",
            "Great Bay Coast Grotto",
            "Great Bay Cape Ledge (1)",
            "Great Bay Cape Ledge (2)",
            "Great Bay Cape Grotto",
            "Great Bay Cape Underwater",
            "PF Exterior 20r (1)",
            "PF Exterior 20r (2)",
            "PF Exterior 20r (3)",
            "Path to Swamp Grotto",
            "Doggy Racetrack 50r",
            "Graveyard Grotto",
            "Swamp Grotto",
            "Woodfall 5r",
            "Woodfall 20r",
            "Well Right Path 50r",
            "Well Left Path 50r",
            "Mountain Village Chest (Spring)",
            "Mountain Village Grotto (Spring) 20r",
            "Path to Ikana 20r",
            "Path to Ikana Grotto",
            "Stone Tower 100r",
            "Stone Tower Bombchu 10",
            "Stone Tower Magic Bean",
            "Path to Snowhead Grotto",
            "Twin Islands 20r",
            "Secret Shrine HP",
            "Secret Shrine Dinolfos",
            "Secret Shrine Wizzrobe",
            "Secret Shrine Wart",
            "Secret Shrine Garo Master",
            "Inn Staff Room",
            "Inn Guest Room",
            "Mystery Woods Grotto",
            "East Clock Town 100r",
            "South Clock Town 20r",
            "South Clock Town 50r",
            "Bank HP",
            "South Clock Town HP",
            "North Clock Town HP",
            "Path to Swamp HP",
            "Swamp Scrub HP",
            "Deku Palace HP",
            "Goron Village Scrub HP",
            "Bio Baba Grotto HP",
            "Lab Fish HP",
            "Great Bay Like-Like HP",
            "Pirates' Fortress HP",
            "Zora Hall Scrub HP",
            "Path to Snowhead HP",
            "Great Bay Coast HP",
            "Ikana Scrub HP",
            "Ikana Castle HP",
            "Odolwa Heart Container",
            "Goht Heart Container",
            "Gyorg Heart Container",
            "Twinmold Heart Container",
            "Map: Clock Town",
            "Map: Woodfall",
            "Map: Snowhead",
            "Map: Romani Ranch",
            "Map: Great Bay",
            "Map: Stone Tower",
            "Goron Racetrack Grotto",
            "Ikana Scrub 200r",
            "One Mask",
            "Two Masks",
            "Three Masks",
            "Four Masks",
            "Moon Access",
            "Deku Trial HP",
            "Goron Trial HP",
            "Zora Trial HP",
            "Link Trial HP",
            "Fierce Deity's Mask",
            "Link Trial 30 Arrows",
            "Link Trial 10 Bombchu",
            "Pre-Clocktown 10 Deku Nuts",
            "Starting Sword",
            "Starting Shield",
            "Starting Heart 1",
            "Starting Heart 2",
            "Ranch Cow #1 Milk",
            "Ranch Cow #2 Milk",
            "Ranch Cow #3 Milk",
            "Well Cow Milk",
            "Termina Grotto Cow #1 Milk",
            "Termina Grotto Cow #2 Milk",
            "Great Bay Coast Grotto Cow #1 Milk",
            "Great Bay Coast Grotto Cow #2 Milk",
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

        private static string[] ITEM_NAMES = DEFAULT_ITEM_NAMES.ToArray();

        public static void AddItem(string itemName)
        {
            var newList = ITEM_NAMES.ToList();
            newList.Add(itemName);
            ITEM_NAMES = newList.ToArray();
        }

        public static void RenameItem(int index, string itemName)
        {
            ITEM_NAMES[index] = itemName;
        }

        public static void RemoveItem(int index)
        {
            ITEM_NAMES = ITEM_NAMES.Where((_, i) => i != index).ToArray();
        }

        public static void ResetItems()
        {
            ITEM_NAMES = DEFAULT_ITEM_NAMES.ToArray();
        }

        public static List<int> ReturnItems;

        private List<string> _filteredItems;
        private List<int> _selectedItems;

        public ItemSelectorForm(List<int> selectedItems = null, bool checkboxes = true, List<int> highlightedItems = null)
        {
            InitializeComponent();
            _filteredItems = ITEM_NAMES.Where((item, index) => highlightedItems?.Contains(index) ?? true).ToList();
            _selectedItems = selectedItems?.ToList() ?? new List<int>();
            UpdateItems();
            this.ActiveControl = textBoxFilter;
            lItems.CheckBoxes = checkboxes;
        }

        private void UpdateItems()
        {
            lItems.Clear();
            foreach (var filteredItem in _filteredItems)
            {
                var item = new ListViewItem(filteredItem);
                item.Checked = _selectedItems.Contains(Array.IndexOf(ITEM_NAMES, filteredItem));
                lItems.Items.Add(item);
            }
        }

        private void bDone_Click(object sender, EventArgs e)
        {
            ReturnItems = _selectedItems.ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            var filter = textBoxFilter.Text.ToLower();
            _filteredItems = ITEM_NAMES.Where(item => item.ToLower().Contains(filter)).ToList();
            UpdateItems();
        }

        private void lItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lItems.CheckBoxes)
            {
                return;
            }
            ReturnItems = new List<int> { Array.IndexOf(ITEM_NAMES, _filteredItems[lItems.SelectedIndices.Cast<int>().First()]) };
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var index = Array.IndexOf(ITEM_NAMES, e.Item.Text);
            if (e.Item.Checked)
            {
                _selectedItems.Add(index);
            }
            else
            {
                _selectedItems.Remove(index);
            }
        }
    }
}
