using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum Region
    {
        [RegionName("Misc"), RegionArea(RegionArea.None)]
        Misc,

        [RegionName("Bottle Catch"), RegionArea(RegionArea.None)]
        BottleCatch,

        [RegionName("Beneath Clocktown"), RegionArea(RegionArea.Town)]
        BeneathClocktown,

        [RegionName("Clock Tower Roof"), RegionArea(RegionArea.Town)]
        ClockTowerRoof,

        [RegionName("South Clock Town"), RegionArea(RegionArea.Town)]
        SouthClockTown,

        [RegionName("North Clock Town"), RegionArea(RegionArea.Town)]
        NorthClockTown,

        [RegionName("Deku Playground Items"), RegionArea(RegionArea.Town)]
        DekuPlaygroundItems,

        [RegionName("East Clock Town"), RegionArea(RegionArea.Town)]
        EastClockTown,

        [RegionName("Stock Pot Inn"), RegionArea(RegionArea.Town)]
        StockPotInn,

        [RegionName("West Clock Town"), RegionArea(RegionArea.Town)]
        WestClockTown,

        [RegionName("Laundry Pool"), RegionArea(RegionArea.Town)]
        LaundryPool,

        [RegionName("Termina Field"), RegionArea(RegionArea.Town)]
        TerminaField,

        [RegionName("Road to Southern Swamp"), RegionArea(RegionArea.Town)]
        RoadToSouthernSwamp,

        [RegionName("Southern Swamp"), RegionArea(RegionArea.Swamp)]
        SouthernSwamp,

        [RegionName("Swamp Spider House Items"), RegionArea(RegionArea.Swamp)]
        SwampSpiderHouseItems,

        [RegionName("Deku Palace"), RegionArea(RegionArea.Swamp)]
        DekuPalace,

        [RegionName("Butler Race Items"), RegionArea(RegionArea.Swamp)]
        ButlerRaceItems,

        [RegionName("Woodfall"), RegionArea(RegionArea.Swamp)]
        Woodfall,

        [RegionName("Woodfall Temple"), RegionArea(RegionArea.Swamp)]
        WoodfallTemple,

        [RegionName("Path to Mountain Village"), RegionArea(RegionArea.Town)]
        PathToMountainVillage,

        [RegionName("Mountain Village"), RegionArea(RegionArea.Mountain)]
        MountainVillage,

        [RegionName("Twin Islands"), RegionArea(RegionArea.Mountain)]
        TwinIslands,

        [RegionName("Goron Race Items"), RegionArea(RegionArea.Mountain)]
        GoronRaceItems,

        [RegionName("Goron Village"), RegionArea(RegionArea.Mountain)]
        GoronVillage,

        [RegionName("Path to Snowhead"), RegionArea(RegionArea.Mountain)]
        PathToSnowhead,

        [RegionName("Snowhead"), RegionArea(RegionArea.Mountain)]
        Snowhead,

        [RegionName("Snowhead Temple"), RegionArea(RegionArea.Mountain)]
        SnowheadTemple,

        [RegionName("Milk Road"), RegionArea(RegionArea.Ranch)]
        MilkRoad,

        [RegionName("Romani Ranch"), RegionArea(RegionArea.Ranch)]
        RomaniRanch,

        [RegionName("Great Bay Coast"), RegionArea(RegionArea.Ocean)]
        GreatBayCoast,

        [RegionName("Ocean Spider House Items"), RegionArea(RegionArea.Ocean)]
        OceanSpiderHouseItems,

        [RegionName("Zora Cape"), RegionArea(RegionArea.Ocean)]
        ZoraCape,

        [RegionName("Zora Hall"), RegionArea(RegionArea.Ocean)]
        ZoraHall,

        [RegionName("Pirates' Fortress Exterior"), RegionArea(RegionArea.Ocean)]
        PiratesFortressExterior,

        [RegionName("Pirates' Fortress Sewer"), RegionArea(RegionArea.Ocean)]
        PiratesFortressSewer,

        [RegionName("Pirates' Fortress Interior"), RegionArea(RegionArea.Ocean)]
        PiratesFortressInterior,

        [RegionName("Pinnacle Rock"), RegionArea(RegionArea.Ocean)]
        PinnacleRock,

        [RegionName("Great Bay Temple"), RegionArea(RegionArea.Ocean)]
        GreatBayTemple,

        [RegionName("Road to Ikana"), RegionArea(RegionArea.Town)]
        RoadToIkana,

        [RegionName("Ikana Graveyard"), RegionArea(RegionArea.Town)]
        IkanaGraveyard,

        [RegionName("Ikana Canyon"), RegionArea(RegionArea.Canyon)]
        IkanaCanyon,

        [RegionName("Beneath the Well"), RegionArea(RegionArea.Canyon)]
        BeneathTheWell,

        [RegionName("Ikana Castle"), RegionArea(RegionArea.Canyon)]
        IkanaCastle,

        [RegionName("Stone Tower"), RegionArea(RegionArea.Canyon)]
        StoneTower,

        [RegionName("Stone Tower Temple"), RegionArea(RegionArea.Canyon)]
        StoneTowerTemple,

        [RegionName("Secret Shrine"), RegionArea(RegionArea.Canyon)]
        SecretShrine,

        [RegionName("The Moon"), RegionArea(RegionArea.None)]
        TheMoon,
    }
}
