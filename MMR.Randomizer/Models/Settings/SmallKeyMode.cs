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

        [Description("Small Key doors will always be open. Small Keys in the item pool will be replaced with other items. Other item placement restrictions will apply to the replacements too.")]
        [HackContent(nameof(Resources.mods.key_small_open))]
        DoorsOpen = 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Small Keys into any temple.")]
        KeepWithinTemples = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinArea)]
        [Description("Randomization algorithm will place any randomized Small Keys into a location in or near the temple they're used in.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Small Keys into an overworld location.")]
        KeepWithinOverworld = 1 << 3,

        [Description("Small Keys will go back in time with Link. Any used Small Keys will return to Link's inventory.")]
        KeepThroughTime = 1 << 4,
    }
}
