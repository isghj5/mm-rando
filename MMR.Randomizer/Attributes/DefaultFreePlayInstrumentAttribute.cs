using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class DefaultFreePlayInstrumentAttribute : Attribute
    {
        public FreePlayInstrument Default { get; }

        public DefaultFreePlayInstrumentAttribute(FreePlayInstrument _default)
        {
            Default = _default;
        }
    }
}
