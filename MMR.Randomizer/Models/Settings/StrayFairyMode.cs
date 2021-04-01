using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Dungeon Fairy Mode")]
    public enum StrayFairyMode
    {
        Default,

        [Description("Stray Fairies in the item pool will be replaced with other items. Non-chest fairies (roaming, bubbles, beehives, etc.) are removed. Chests that ordinarily have a Stray Fairy will behave like normal chests.")]
        [HackContent(nameof(Resources.mods.fairies_chests_only))]
        ChestsOnly,

        [Description("Randomization algorithm will place any randomized Stray Fairies into a location within the same region, even if the Stray Fairy has been replaced via another Dungeon Fairy Mode.")]
        KeepWithinDungeon,
    }
}
