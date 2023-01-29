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

        [Description("Boss doors will always be open. Boss Keys in the item pool will be replaced with other items. Other item placement restrictions will apply to the replacements too.")]
        [HackContent(nameof(Resources.mods.key_boss_open))]
        DoorsOpen = 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Boss Keys into any temple.")]
        KeepWithinTemples = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Boss Keys into a location in or near the temple it's used in.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Boss Keys into an overworld location.")]
        KeepWithinOverworld = 1 << 3,

        [Description("Boss Keys will go back in time with Link.")]
        [HackContent(nameof(Resources.mods.key_boss_sot))]
        KeepThroughTime = 1 << 4,
    }
}
