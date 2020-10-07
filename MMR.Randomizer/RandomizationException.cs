using System;

namespace MMR.Randomizer
{
    public class RandomizationException : Exception
    {
        public RandomizationException(string message) : base(message)
        {
        }
    }
    public class ROMOverflow : Exception
    {
        public ROMOverflow(string message) : base(message)
        {
        }
    }

}
