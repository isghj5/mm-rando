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
        public static FreePlayInstrument? DefaultFreePlayInstrument(this TransformationForm form)
        {
            return form.GetAttribute<DefaultFreePlayInstrumentAttribute>()?.Default;
        }

        public static PlaybackInstrument? DefaultPlaybackInstrument(this TransformationForm form)
        {
            return form.GetAttribute<DefaultPlaybackInstrumentAttribute>()?.Default;
        }

        public static byte Id(this TransformationForm form)
        {
            return form.GetAttribute<IdAttribute>().Id;
        }
    }
}
