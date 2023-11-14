using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Victory Mode")]
    public enum VictoryMode
    {
        Default,

        [Description("Once the custom victory condition(s) is/are fulfilled, begin the credits as soon as possible. Otherwise, you must fulfill the conditions and then defeat Majora.")]
        DirectToCredits = 1 << 0,

        [Description("Have 15 of each dungeon stray fairy.")]
        Fairies = 1 << 1,

        [Description("Have 30 of each type of skulltula token.")]
        SkullTokens = 1 << 2,

        [Description("Have all 20 non-transformation masks.")]
        NonTransformationMasks = 1 << 3,

        [Description("Have all 4 transformation masks.")]
        TransformationMasks = 1 << 4,

        [Description("Have all the notebook entries.")]
        Notebook = 1 << 5,

        [Description("Have all 20 hearts.")]
        Hearts = 1 << 6,
    }
}
