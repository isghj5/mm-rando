using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Boss Remains Mode")]
    public enum BossRemainsMode
    {
        Default,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.GreatFairyRewards)]
        [Description("Boss Remains will be placed on the reward for collecting 15 stray fairies of their dungeon. Great Fairy Rewards must be randomized for this to take effect.")]
        GreatFairyRewards = 1,

        [Description("Boss Remains will be shuffled among each other. You still need to randomize them in the Item Randomization tab.")]
        ShuffleOnly = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Boss Remains into any temple.")]
        KeepWithinTemples = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Boss Remains into a location in or near the temple its boss is in.")]
        KeepWithinArea = 1 << 3,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Boss Remains into an overworld location.")]
        KeepWithinOverworld = 1 << 4,

        [Description("Start with 1 random boss remains. Locations in and after the temple with that boss will be forced to be junk.")]
        Blitz1 = 1 << 5,

        [Description("Start with 2 random boss remains. Locations in and after the temples with those bosses will be forced to be junk.")]
        Blitz2 = 1 << 6,

        [Description("Start with 3 random boss remains. Locations in and after the temples with those bosses will be forced to be junk.")]
        Blitz3 = 1 << 7,

        [Description("Start with all boss remains. Locations in and after temples will be forced to be junk.")]
        Blitz4 = 1 << 8,
    }
}
