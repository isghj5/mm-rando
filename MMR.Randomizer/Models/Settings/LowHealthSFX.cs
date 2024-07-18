using MMR.Randomizer.Attributes;
using MMR.Randomizer.Models.SoundEffects;

namespace MMR.Randomizer.Models.Settings
{
    public enum LowHealthSFX
    {
        Default = 0, // vanilla behavior
        Disabled = 1, // no sound at all
        Random = 2,

        // specific sfx
        [Value(SoundEffect.LittleChickChirp)]
        CuccoChicks,

        [Value(SoundEffect.CuccoClucking)]
        CuccoClucking,

        [Value(SoundEffect.ChildLinkPantLowHealth)]
        LinkPanting,

        [Value(SoundEffect.DogBark)]
        DogBark,

        [Value(SoundEffect.SilverRupeeGet)]
        SilverRupee,

        [Value(SoundEffect.TatlDashNormal)]
        TatlDash,

        [Value(SoundEffect.TatlMessage)]
        TatlMessage,

        [Value(SoundEffect.MikauBaybee)]
        MikauBaby,

        [Value(SoundEffect.CowMoo)]
        CowMooing,

        [Value(SoundEffect.SecretLadderAppears)]
        LadderWarp,

        [Value(SoundEffect.SwampTouristProprietorHehHeh)]
        AmusedFather,

        [Value(SoundEffect.TingleChuckle)]
        TingleChuckle,

    }
}
