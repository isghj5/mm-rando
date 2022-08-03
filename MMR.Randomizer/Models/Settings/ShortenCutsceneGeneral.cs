using System;
using System.ComponentModel;
using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.Models.Settings
{
    [Flags]
    public enum ShortenCutsceneGeneral
    {
        None = 0,

        [Description("You don't have to wait for Sakon to leave.")]
        BlastMaskThief = 1 << 0,

        [Description("The minigame will end as soon as you have the required 20 points.")]
        BoatArchery = 1 << 1,

        [Description("The minigame will end as soon as you have the required 20 points.")]
        FishermanGame = 1 << 2,

        [Description("Replaying of the music is shortened.")]
        MilkBarPerformance = 1 << 3,

        [Description("The Hungry Goron doesn't interrupt you when you approach, and you don't have to watch him roll away.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_don_gero))]
        HungryGoron = 1 << 4,

        [Description("Remove most instances of Tatl interrupting your gameplay.")]
        [HackContent(nameof(Resources.mods.remove_tatl_interrupts))]
        TatlInterrupts = 1 << 5,

        [Description("Skips the irrelevant bank text. Allows using Z/R to set deposit/withdraw amount to min/max.")]
        [HackContent(nameof(Resources.mods.faster_bank_text))]
        FasterBankText = 1 << 6,

        [Description("The owl in Goron Village will no longer trigger dialog when you approach. You must target it and talk to it if you want it to reveal the path.")]
        [HackContent(nameof(Resources.mods.shorten_cutscene_gv_owl))]
        GoronVillageOwl = 1 << 7,

        [Description("The dialog of the credits will proceed automatically.")]
        AutomaticCredits = 1 << 8,

        [Description("All chests will open using the short animation.")]
        ShortChestOpening = 1 << 9,

        [Description("Various cutscenes are skipped or otherwise shortened.")]
        [HackContent(nameof(Resources.mods.short_cutscenes))]
        EverythingElse = 1 << 31,
    }
}
