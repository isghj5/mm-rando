using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum TransformationForm
    {
        [Id(0x04)]
        [DefaultFreePlayInstrument(FreePlayInstrument.Ocarina), DefaultPlaybackInstrument(PlaybackInstrument.Ocarina)]
        Human,

        [DefaultFreePlayInstrument(FreePlayInstrument.DekuPipes), DefaultPlaybackInstrument(PlaybackInstrument.DekuPipes)]
        [Id(0x03)]
        Deku,

        [DefaultFreePlayInstrument(FreePlayInstrument.Drums), DefaultPlaybackInstrument(PlaybackInstrument.Drums)]
        [Id(0x01)]
        Goron,

        [DefaultFreePlayInstrument(FreePlayInstrument.Guitar), DefaultPlaybackInstrument(PlaybackInstrument.Guitar)]
        [Id(0x02)]
        Zora,

        [Id(0x00)]
        FierceDeity,
    }
}
