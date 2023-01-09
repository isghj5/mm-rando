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

        [Description("Randomization algorithm will place any randomized Boss Remains into a location within the same region.")]
        KeepWithinDungeon = 1 << 1,

        [Description("Randomization algorithm will place any randomized Boss Remains into an overworld (non-dungeon) location near the temple.")]
        KeepWithinArea = 1 << 2,
    }
}
