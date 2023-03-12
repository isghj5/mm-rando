using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum RegionArea
    {
        None,

        [RegionName("town", "in")]
        Town,

        [RegionName("the swamp", "in")]
        Swamp,

        [RegionName("the mountains", "in")]
        Mountain,

        [RegionName("the ranch", "in")]
        Ranch,

        [RegionName("the ocean", "in")]
        Ocean,

        [RegionName("the canyon", "in")]
        Canyon,
    }
}
