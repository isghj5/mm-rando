using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum FreePlayInstrument
    {
        [PlaybackInstrumentPair(PlaybackInstrument.SyncedRandom)]
        Random,

        [Id(1)]
        [PlaybackInstrumentPair(PlaybackInstrument.Ocarina)]
        Ocarina,

        [Id(2)]
        [PlaybackInstrumentPair(PlaybackInstrument.FemaleVoice)]
        FemaleVoice,

        [Id(3)]
        [PlaybackInstrumentPair(PlaybackInstrument.WhistlingFlute)]
        WhistlingFlute,

        [Id(4)]
        [PlaybackInstrumentPair(PlaybackInstrument.Harp)]
        Harp,

        [Id(5)]
        [PlaybackInstrumentPair(PlaybackInstrument.IkanaKing)]
        IkanaKing,

        [Id(6)]
        [PlaybackInstrumentPair(PlaybackInstrument.Tatl)]
        Tatl,

        [Id(7)]
        [PlaybackInstrumentPair(PlaybackInstrument.Drums)]
        Drums,

        [Id(8)]
        [PlaybackInstrumentPair(PlaybackInstrument.Guitar)]
        Guitar,

        [Id(9)]
        [PlaybackInstrumentPair(PlaybackInstrument.DekuPipes)]
        DekuPipes,

        [Id(0xA)]
        [PlaybackInstrumentPair(PlaybackInstrument.Monkey)]
        Monkey,

        //[Id(0xB)]
        //PipesAgain,

        [Id(0xC)]
        [PlaybackInstrumentPair(PlaybackInstrument.GoronElderDrum)]
        GoronElderDrum,

        [Id(0xD)]
        [PlaybackInstrumentPair(PlaybackInstrument.Piano)]
        Piano,

        [Id(0xE)]
        [PlaybackInstrumentPair(PlaybackInstrument.BassGuitar)]
        BassGuitar,

        [Id(0xF)]
        [PlaybackInstrumentPair(PlaybackInstrument.BabySinging)]
        BabySinging,

        //[Id(0x10)]
        //AmplifedGuitar,


    }
}
