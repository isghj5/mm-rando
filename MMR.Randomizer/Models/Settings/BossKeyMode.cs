using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Boss Key Mode")]
    public enum BossKeyMode
    {
        Default,

        [Description("Boss doors will always be open. Boss Keys in the item pool will be replaced with other items.")]
        [HackContent(nameof(Resources.mods.key_boss_open))]
        DoorsOpen,

        [Description("Randomization algorithm will place any randomized Boss Keys into a location within the same region, even if the Boss Key has been replaced via another Boss Key Mode.")]
        KeepWithinDungeon,
    }
}
