using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class FreePlayInstrumentExtensions
    {
        public static byte Id(this FreePlayInstrument freePlayInstrument)
        {
            return freePlayInstrument.GetAttribute<IdAttribute>().Id;
        }

        public static PlaybackInstrument PlaybackInstrumentPair(this FreePlayInstrument freePlayInstrument)
        {
            return freePlayInstrument.GetAttribute<PlaybackInstrumentPairAttribute>().Pair;
        }
    }
}
