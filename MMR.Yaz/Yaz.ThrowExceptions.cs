using System;

namespace MMR.Yaz
{
    public static partial class Yaz
    {
        private static void ThrowArgumentExceptionForMagic(string paramName)
        {
            throw new ArgumentException("Yaz0 magic number is not correct.", paramName);
        }

        private static void ThrowArgumentOutOfRangeExceptionForRecents(string paramName)
        {
            throw new ArgumentOutOfRangeException(paramName, "Recents buffer must be at least 256 in length.");
        }

        private static void ThrowArgumentOutOfRangeExceptionForDeltas(string paramName)
        {
            throw new ArgumentOutOfRangeException(paramName, "Deltas buffer cannot be larger than 0x1000.");
        }
    }
}
