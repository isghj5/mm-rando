using System;

namespace MMR.Yaz.Benchmarks
{
    public static class DebugUtil
    {
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception($"Assert failed: {message}");
            }
        }
    }
}
