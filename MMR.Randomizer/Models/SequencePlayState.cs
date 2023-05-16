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

        Indoors = 1 << 5,
        Cave = 1 << 6,
        Epona = 1 << 7,
        Swim = 1 << 8,
        SpikeRolling = 1 << 9,
        Combat = 1 << 10,
        CriticalHealth = 1 << 11,
    }
}
