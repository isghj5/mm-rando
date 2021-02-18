using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class InstrumentExtensions
    {
        public static byte Id(this Instrument instrument)
        {
            return instrument.GetAttribute<IdAttribute>().Id;
        }
    }
}
