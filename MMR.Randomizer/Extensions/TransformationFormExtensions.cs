using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Extensions
{
    public static class TransformationFormExtensions
    {
        public static Instrument? DefaultInstrument(this TransformationForm form)
        {
            return form.GetAttribute<DefaultInstrumentAttribute>()?.Default;
        }

        public static byte Id(this TransformationForm form)
        {
            return form.GetAttribute<IdAttribute>().Id;
        }
    }
}
