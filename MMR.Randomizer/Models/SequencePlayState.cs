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

        Day = 1 << 5,
        Night = 1 << 6,
        Outdoors = 1 << 7,
        Indoors = 1 << 8,
        Cave = 1 << 9,
        Epona = 1 << 10,
        Swim = 1 << 11,
        SpikeRolling = 1 << 12,
        Combat = 1 << 13,
        CriticalHealth = 1 << 14,
    }
}
