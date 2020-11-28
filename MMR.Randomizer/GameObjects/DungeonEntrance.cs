using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;

namespace MMR.Randomizer.GameObjects
{
    public enum DungeonEntrance
    {
        [Region(Region.StoneTower)]
        [Exit(Scene.InvertedStoneTower, 0)]
        [Spawn(Scene.InvertedStoneTowerTemple, 0)]
        EntranceStoneTowerTempleInvertedFromStoneTowerInverted,

        [Region(Region.Woodfall)]
        [Exit(Scene.Woodfall, 1)]
        [Spawn(Scene.WoodfallTemple, 0)]
        EntranceWoodfallTempleFromWoodfall,

        [Region(Region.Snowhead)]
        [Exit(Scene.Snowhead, 1)]
        [Spawn(Scene.SnowheadTemple, 0)]
        EntranceSnowheadTempleFromSnowhead, // should use the No Intro version if shorten cutscenes is enabled

        [Region(Region.GreatBayTemple)]
        [Exit(Scene.GreatBayTemple, 0)]
        [ExitAddress(0xF155BA)]
        [Spawn(Scene.ZoraCape, 7)]
        EntranceZoraCapeFromGreatBayTemple,

        [Region(Region.WoodfallTemple)]
        [Exit(Scene.WoodfallTemple, 0)]
        [Spawn(Scene.Woodfall, 1)]
        EntranceWoodfallFromWoodfallTempleEntrance,

        [Region(Region.ZoraCape)]
        [ExitCutscene(Scene.ZoraCape, 0, 1)]
        [ExitCutscene(Scene.ZoraCape, 0, 2)]
        [ExitCutscene(Scene.ZoraCape, 1, 2)]
        [ExitCutscene(Scene.GreatBayCutscene, 0, 0)]
        [Spawn(Scene.GreatBayTemple, 0)]
        EntranceGreatBayTempleFromZoraCape,

        [Region(Region.StoneTower)]
        [Exit(Scene.InvertedStoneTowerTemple, 0)]
        [Spawn(Scene.InvertedStoneTower, 1)]
        EntranceStoneTowerInvertedFromStoneTowerTempleInverted,

        [Region(Region.SnowheadTemple)]
        [Exit(Scene.SnowheadTemple, 0)]
        [Spawn(Scene.Snowhead, 1)]
        EntranceSnowheadFromSnowheadTemple,

        [Region(Region.SnowheadTemple)]
        [Spawn(Scene.MountainVillage, 7)]
        EntranceMountainVillageFromSnowheadClear, // one way

        [Region(Region.GreatBayTemple)]
        [Spawn(Scene.ZoraCape, 8)]
        EntranceZoraCapeFromGreatBayTempleClear, // one way

        [Region(Region.StoneTowerTemple)]
        [Spawn(Scene.IkanaCanyon, 7)]
        EntranceIkanaCanyonFromIkanaClear, // one way

        [Region(Region.WoodfallTemple)]
        [Spawn(Scene.WoodfallTemple, 1)]
        EntranceWoodfallTemplePrisonFromOdolwasLair, // one way
    }
}
