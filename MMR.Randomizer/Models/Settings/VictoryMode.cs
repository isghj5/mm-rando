using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Victory Mode")]
    public enum VictoryMode
    {
        Default,

        [Description("Once the custom victory condition(s) is/are fulfilled, begin the credits as soon as possible. Otherwise, you must fulfill the condition(s) and then defeat Majora.")]
        DirectToCredits = 1 << 0,

        [Description("When enabled, speaking to the Moon Child who is wearing Majora's Mask will send you back in time if you haven't fufilled the victory condition(s). When disabled, defeating Majora will send you back in time if you haven't fulfilled the victory condition(s).")]
        CantFightMajora = 1 << 1,

        [Description("Have 15 of each dungeon stray fairy.")]
        Fairies = 1 << 2,

        [Description("Have 30 of each type of skulltula token.")]
        SkullTokens = 1 << 3,

        [Description("Have all 20 non-transformation masks.")]
        NonTransformationMasks = 1 << 4,

        [Description("Have all 4 transformation masks.")]
        TransformationMasks = 1 << 5,

        [Description("Have all the notebook entries.")]
        Notebook = 1 << 6,

        [Description("Have all 20 hearts.")]
        Hearts = 1 << 7,

        [Description("Have one of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        OneBossRemains = 1 << 8,

        [Description("Have two of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        TwoBossRemains = 1 << 9,

        [Description("Have three of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        ThreeBossRemains = 1 << 10,

        [Description("Have all the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        FourBossRemains = 1 << 11,

        // TODO MoonTrials
    }
}
