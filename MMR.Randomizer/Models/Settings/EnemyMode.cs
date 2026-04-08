using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    [Flags]
    [Description("Enemies")]
    public enum EnemyMode
    {
        Default,

        [Description("Enable randomization of enemies. May cause softlocks in some circumstances, use at your own risk.")]
        Randomized = 1,

        [Description("Double the rate at which enemies are updated, making them more difficult.")]
        Hyper = 1 << 1,
    }
}
