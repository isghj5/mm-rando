using System;

namespace MMR.Randomizer.Models
{
    [Flags]
    public enum SequencePlayState : ushort
    {
        None = 0,
        FierceDeity = 1,
        Goron = 1 << 1,
        Zora = 1 << 2,
        Deku = 1 << 3,
        Human = 1 << 4,
        All = Human | Goron | Zora | Deku | FierceDeity,

        Outdoors = 1 << 5,
        Indoors = 1 << 6,
        Cave = 1 << 7,
        Epona = 1 << 8,
        Swim = 1 << 9,
        SpikeRolling = 1 << 10,
        Combat = 1 << 11,
        CriticalHealth = 1 << 12,
    }
}
