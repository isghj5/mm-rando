using System;

namespace MMR.Randomizer.Models
{
    [Flags]
    public enum TimeOfDay
    {
        None,
        Day1 = 1,
        Night1 = 2,
        Day2 = 4,
        Night2 = 8,
        Day3 = 16,
        Night3 = 32,
    }
}
