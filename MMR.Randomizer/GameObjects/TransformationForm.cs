using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum TransformationForm
    {
        [Id(0x04)]
        [DefaultInstrument(Instrument.Ocarina)]
        Human,

        [DefaultInstrument(Instrument.DekuPipes)]
        [Id(0x03)]
        Deku,

        [DefaultInstrument(Instrument.Drums)]
        [Id(0x01)]
        Goron,

        [DefaultInstrument(Instrument.Guitar)]
        [Id(0x02)]
        Zora,

        [Id(0x00)]
        FierceDeity,
    }
}
