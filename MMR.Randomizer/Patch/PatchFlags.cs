using System;

namespace MMR.Randomizer.Patch
{
    [Flags]
    public enum PatchFlags : ushort
    {
        None = 0,
        IsStatic = 1,
    }
}
