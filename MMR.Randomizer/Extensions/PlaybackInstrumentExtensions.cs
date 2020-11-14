using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class PlaybackInstrumentExtensions
    {
        public static byte Id(this PlaybackInstrument playbackInstrument)
        {
            return playbackInstrument.GetAttribute<IdAttribute>().Id;
        }
    }
}
