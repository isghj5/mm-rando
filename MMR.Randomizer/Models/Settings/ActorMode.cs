using MMR.Randomizer.Attributes.Setting;

namespace MMR.Randomizer.Models.Settings
{
    public enum ActorMode
    {
        Default,

        Actorizer,

        Enemizer,

        [SettingName("Enemizer: Out For Blood")]
        EnemizerOutForBlood,

    }
}
