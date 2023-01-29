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

        [Description("Boss Remains will be placed on the reward for collecting 15 stray fairies of their dungeon. Great Fairy Rewards must be randomized for this to take effect.")]
        GreatFairyRewards = 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Boss Remains into any temple.")]
        KeepWithinTemples = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Boss Remains into a location in or near the temple its boss is in.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Boss Remains into an overworld location.")]
        KeepWithinOverworld = 1 << 3,
    }
}
