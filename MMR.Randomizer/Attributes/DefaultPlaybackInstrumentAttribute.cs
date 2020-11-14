using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class DefaultPlaybackInstrumentAttribute : Attribute
    {
        public PlaybackInstrument Default { get; }

        public DefaultPlaybackInstrumentAttribute(PlaybackInstrument _default)
        {
            Default = _default;
        }
    }
}
