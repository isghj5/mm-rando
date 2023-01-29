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

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into any temple.")]
        KeepWithinTemples = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into a location in or near the temple they're for.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into an overworld location.")]
        KeepWithinOverworld = 1 << 3,
    }
}
