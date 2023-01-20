using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum Region
    {
        [RegionName("Misc"), RegionArea(RegionArea.None)]
        Misc,

        [RegionName("Bottle Catch"), RegionArea(RegionArea.None)]
        BottleCatch,

        [RegionName("Beneath Clocktown", ""), RegionArea(RegionArea.Town)]
        BeneathClocktown,

        [RegionName("Clock Tower Roof", "on the"), RegionArea(RegionArea.Town)]
        ClockTowerRoof,

        [RegionName("South Clock Town", "in"), RegionArea(RegionArea.Town)]
        SouthClockTown,

        [RegionName("North Clock Town", "in"), RegionArea(RegionArea.Town)]
        NorthClockTown,

        [RegionName("Deku Playground Items", "among the"), RegionArea(RegionArea.Town)]
        DekuPlaygroundItems,

        [RegionName("East Clock Town", "in"), RegionArea(RegionArea.Town)]
        EastClockTown,

        [RegionName("Stock Pot Inn", "in the"), RegionArea(RegionArea.Town)]
        StockPotInn,

        [RegionName("West Clock Town", "in"), RegionArea(RegionArea.Town)]
        WestClockTown,

        [RegionName("Laundry Pool", "in the"), RegionArea(RegionArea.Town)]
        LaundryPool,

        [RegionName("Termina Field", "in"), RegionArea(RegionArea.Town)]
        TerminaField,

        [RegionName("Road to Southern Swamp", "on the"), RegionArea(RegionArea.Town)]
        RoadToSouthernSwamp,

        [RegionName("Southern Swamp", "in"), RegionArea(RegionArea.Swamp)]
        SouthernSwamp,

        [RegionName("Swamp Spider House Items", "among the"), RegionArea(RegionArea.Swamp)]
        SwampSpiderHouseItems,

        [RegionName("Deku Palace", "in the"), RegionArea(RegionArea.Swamp)]
        DekuPalace,

        [RegionName("Butler Race Items", "among the"), RegionArea(RegionArea.Swamp)]
        ButlerRaceItems,

        [RegionName("Woodfall", "in"), RegionArea(RegionArea.Swamp)]
        Woodfall,

        [RegionName("Woodfall Temple", "in"), RegionArea(RegionArea.Swamp)]
        WoodfallTemple,

        [RegionName("Path to Mountain Village", "on the"), RegionArea(RegionArea.Town)]
        PathToMountainVillage,

        [RegionName("Mountain Village", "in the"), RegionArea(RegionArea.Mountain)]
        MountainVillage,

        [RegionName("Twin Islands", "in the"), RegionArea(RegionArea.Mountain)]
        TwinIslands,

        [RegionName("Goron Race Items", "among the"), RegionArea(RegionArea.Mountain)]
        GoronRaceItems,

        [RegionName("Goron Village", "in the"), RegionArea(RegionArea.Mountain)]
        GoronVillage,

        [RegionName("Path to Snowhead", "on the"), RegionArea(RegionArea.Mountain)]
        PathToSnowhead,

        [RegionName("Snowhead", "in"), RegionArea(RegionArea.Mountain)]
        Snowhead,

        [RegionName("Snowhead Temple", "in"), RegionArea(RegionArea.Mountain)]
        SnowheadTemple,

        [RegionName("Milk Road", "on"), RegionArea(RegionArea.Ranch)]
        MilkRoad,

        [RegionName("Romani Ranch", "in"), RegionArea(RegionArea.Ranch)]
        RomaniRanch,

        [RegionName("Great Bay Coast", "in"), RegionArea(RegionArea.Ocean)]
        GreatBayCoast,

        [RegionName("Ocean Spider House Items", "among the"), RegionArea(RegionArea.Ocean)]
        OceanSpiderHouseItems,

        [RegionName("Zora Cape", "in"), RegionArea(RegionArea.Ocean)]
        ZoraCape,

        [RegionName("Zora Hall", "in"), RegionArea(RegionArea.Ocean)]
        ZoraHall,

        [RegionName("Pirates' Fortress Exterior", "in"), RegionArea(RegionArea.Ocean)]
        PiratesFortressExterior,

        [RegionName("Pirates' Fortress Sewer", "in"), RegionArea(RegionArea.Ocean)]
        PiratesFortressSewer,

        [RegionName("Pirates' Fortress Interior", "in"), RegionArea(RegionArea.Ocean)]
        PiratesFortressInterior,

        [RegionName("Pinnacle Rock", "in"), RegionArea(RegionArea.Ocean)]
        PinnacleRock,

        [RegionName("Great Bay Temple", "in"), RegionArea(RegionArea.Ocean)]
        GreatBayTemple,

        [RegionName("Road to Ikana", "on the"), RegionArea(RegionArea.Town)]
        RoadToIkana,

        [RegionName("Ikana Graveyard", "in the"), RegionArea(RegionArea.Town)]
        IkanaGraveyard,

        [RegionName("Ikana Canyon", "in"), RegionArea(RegionArea.Canyon)]
        IkanaCanyon,

        [RegionName("Beneath the Well", ""), RegionArea(RegionArea.Canyon)]
        BeneathTheWell,

        [RegionName("Ikana Castle", "in"), RegionArea(RegionArea.Canyon)]
        IkanaCastle,

        [RegionName("Stone Tower", "in"), RegionArea(RegionArea.Canyon)]
        StoneTower,

        [RegionName("Stone Tower Temple", "in"), RegionArea(RegionArea.Canyon)]
        StoneTowerTemple,

        [RegionName("Secret Shrine", "in the"), RegionArea(RegionArea.Canyon)]
        SecretShrine,

        [RegionName("The Moon", "on"), RegionArea(RegionArea.None)]
        TheMoon,
    }
}
