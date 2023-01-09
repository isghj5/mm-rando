using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Dungeon Navigation Mode")]
    public enum DungeonNavigationMode
    {
        Default,

        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into a location within the same region.")]
        KeepWithinDungeon = 1 << 1,

        [Description("Randomization algorithm will place any randomized Dungeon Maps and Compasses into an overworld (non-dungeon) location near the temple.")]
        KeepWithinArea = 1 << 2,
    }
}
