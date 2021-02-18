using System;

namespace MMR.Randomizer
{
    public class ROMOverflowException : Exception
    {
        public ROMOverflowException(string sizeExceeded, string platformAffected = "anything")
            : base($"Rom has expanded past {sizeExceeded},{Environment.NewLine}" +
                $"and cannot be played on {platformAffected}.{Environment.NewLine}" +
                $"This is most likely caused by sound sample injection for music.{Environment.NewLine}" +
                $"Please try another seed, for a different music roll{Environment.NewLine}" +
                "or consider reducing how much custom sample music is used.")
        {
        }
    }

}
