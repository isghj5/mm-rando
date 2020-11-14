using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class DefaultInstrumentAttribute : Attribute
    {
        public Instrument Default { get; }

        public DefaultInstrumentAttribute(Instrument _default)
        {
            Default = _default;
        }
    }
}
