using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class PlaybackInstrumentPairAttribute : Attribute
    {
        public PlaybackInstrument Pair { get; }

        public PlaybackInstrumentPairAttribute(PlaybackInstrument pair)
        {
            Pair = pair;
        }
    }
}
