using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Small Key Mode")]
    public enum SmallKeyMode
    {
        Default,

        [Description("Small Key doors will always be open. Small Keys in the item pool will be replaced with other items.")]
        [HackContent(nameof(Resources.mods.key_small_open))]
        DoorsOpen = 1,

        [Description("Randomization algorithm will place any randomized Small Keys into a location within the same region, even if the Small Key has been replaced via another Small Key Mode.")]
        KeepWithinDungeon = 1 << 1,

        [Description("Small Keys will go back in time with Link. Any used Small Keys will return to Link's inventory.")]
        KeepThroughTime = 1 << 2,

        [Description("Randomization algorithm will place any randomized Small Keys into an overworld (non-dungeon) location near the temple.")]
        KeepWithinArea = 1 << 3,
    }
}
