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

        [Description("Finding a Dungeon Map will tell you the location of the dungeon.")]
        MapRevealsLocation = 1 << 4,

        [Description("Finding a Compass will tell you which boss is in the dungeon.")]
        CompassRevealsBoss = 1 << 5,
    }
}
