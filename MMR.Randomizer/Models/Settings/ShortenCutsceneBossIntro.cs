using System;
using System.ComponentModel;
using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.Models.Settings
{
    [Flags]
    public enum ShortenCutsceneBossIntro
    {
        None = 0,

        [Description("Odolwa is ready to fight you right away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_odolwa_intro))]
        Odolwa = 1 << 0,

        [Description("Link doesn't look around in surprise when you enter Goht's room.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_goht_intro))]
        Goht = 1 << 1,

        [Description("Gyorg is ready to fight you right away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_gyorg_intro))]
        Gyorg = 1 << 2,

        [Description("Twinmold is ready to fight you right away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_twinmold_intro))]
        Twinmold = 1 << 3,

        [Description("Majora is ready to fight you right away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_majora_intro))]
        Majora = 1 << 4,

        [Description("You don't have to look at Wart to get him down from the ceiling. It spawns on the ground.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_wart_intro))]
        Wart = 1 << 5,

        [Description("Igos du Ikana and his henchmen are ready to fight you right away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_igos_intro))]
        IgosDuIkana = 1 << 6,
    }
}
