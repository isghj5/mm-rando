using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Dungeon Navigation Mode")]
    public enum DungeonNavigationMode
    {
        Default,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinRegion)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into a location within the same region.")]
        KeepWithinDungeon = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into a location in or near the temple.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into an overworld location.")]
        KeepWithinOverworld = 1 << 3,
    }
}
