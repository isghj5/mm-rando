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

        [Description("Stray Fairies in the item pool will be replaced with other items. Other item placement restrictions will apply to the replacements too. Non-chest fairies (roaming, bubbles, beehives, etc.) are removed. Chests that ordinarily have a Stray Fairy will behave like normal chests.")]
        [HackContent(nameof(Resources.mods.fairies_chests_only))]
        ChestsOnly = 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinTemples)]
        [Description("Randomization algorithm will place any randomized Stray Fairies into any temple.")]
        KeepWithinTemples = 1 << 1,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepFairyWithinArea)]
        [Description("Randomization algorithm will place any randomized Stray Fairies into a location in or near the fairy fountain they originate from.")]
        KeepWithinArea = 1 << 2,

        [RestrictedPlacement(RestrictedPlacementAttribute.RestrictionType.KeepWithinOverworld)]
        [Description("Randomization algorithm will place any randomized Stray Fairies into an overworld location.")]
        KeepWithinOverworld = 1 << 3,
    }
}
