using MMR.Randomizer.Attributes;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    [Flags]
    [Description("Victory")]
    public enum VictoryMode
    {
        Default,

        [Description("Once the custom victory condition(s) is/are fulfilled, begin the credits as soon as possible. Otherwise, you must fulfill the condition(s) and then defeat Majora.")]
        DirectToCredits = 1 << 0,

        [Description("When enabled, speaking to the Moon Child who is wearing Majora's Mask will send you back in time if you haven't fufilled the victory condition(s). When disabled, defeating Majora will send you back in time if you haven't fulfilled the victory condition(s).")]
        CantFightMajora = 1 << 1,

        [VictoryModeFlavorText("gather all the [RED]stray fairies[WHITE]")]
        [Description("Have 15 of each dungeon stray fairy.")]
        Fairies = 1 << 2,

        [VictoryModeFlavorText("gather all the [RED]golden skulltula tokens[WHITE]")]
        [Description("Have 30 of each type of skulltula token.")]
        SkullTokens = 1 << 3,

        [VictoryModeFlavorText("collect all [RED]non-transformation masks[WHITE]")]
        [Description("Have all 20 non-transformation masks.")]
        NonTransformationMasks = 1 << 4,

        [VictoryModeFlavorText("collect all [RED]transformation masks[WHITE]")]
        [Description("Have all 4 transformation masks.")]
        TransformationMasks = 1 << 5,

        [VictoryModeFlavorText("collect all the [RED]notebook entries[WHITE]")]
        [Description("Have all the notebook entries.")]
        Notebook = 1 << 6,

        [VictoryModeFlavorText("maximize your [RED]health[WHITE]")]
        [Description("Have all 20 hearts.")]
        Hearts = 1 << 7,

        [VictoryModeFlavorText("find [RED]one boss remains[WHITE]")]
        [Description("Have one of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        OneBossRemains = 1 << 8,

        [VictoryModeFlavorText("find [RED]two boss remains[WHITE]")]
        [Description("Have two of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        TwoBossRemains = 1 << 9,

        [VictoryModeFlavorText("find [RED]three boss remains[WHITE]")]
        [Description("Have three of the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        ThreeBossRemains = 1 << 10,

        [VictoryModeFlavorText("collect [RED]all boss remains[WHITE]")]
        [Description("Have all the boss remains. Only the highest boss remain victory condition enabled will be used.")]
        FourBossRemains = 1 << 11,

        // TODO MoonTrials
    }
}
