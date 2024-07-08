using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Setting;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    public enum DamageMode
    {
        Default,

        Double,

        Quadruple,

        [SettingName("One")]
        OHKO,

        Doom
    }
}
