using System;

namespace MMR.Randomizer.Models
{
    [Flags]
    public enum SequencePlayState : byte
    {
        None = 0,
        Human = 1,
        Goron = 1 << 1,
        Zora = 1 << 2,
        Deku = 1 << 3,
        All = Human | Goron | Zora | Deku,
    }
}
