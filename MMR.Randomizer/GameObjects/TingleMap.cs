using System;

namespace MMR.Randomizer.GameObjects
{
    [Flags]
    public enum TingleMap : byte
    {
        None = 0,
        Town = 1,
        Swamp = 2,
        Mountain = 4,
        Ranch = 8,
        Ocean = 16,
        Canyon = 32,
    }
}
