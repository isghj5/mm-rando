using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Setting;
using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    public enum DamageMode
    {
        Default,

        Double,

        Quadruple,

        Octuple,

        [SettingName("One-Hit KO")]
        OHKO,

        Doom,
    }
}
