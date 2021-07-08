namespace MMR.Randomizer.Models.Settings
{
    public class ShortenCutsceneSettings
    {
        public ShortenCutsceneGeneral General { get; set; } = 
            ShortenCutsceneGeneral.BlastMaskThief 
            | ShortenCutsceneGeneral.BoatArchery
            | ShortenCutsceneGeneral.FishermanGame
            | ShortenCutsceneGeneral.MilkBarPerformance
            | ShortenCutsceneGeneral.HungryGoron
            | ShortenCutsceneGeneral.TatlInterrupts
            | ShortenCutsceneGeneral.FasterBankText
            | ShortenCutsceneGeneral.EverythingElse
            ;

        public ShortenCutsceneBossIntro BossIntros { get; set; } =
            ShortenCutsceneBossIntro.Odolwa
            | ShortenCutsceneBossIntro.Goht
            | ShortenCutsceneBossIntro.Gyorg
            | ShortenCutsceneBossIntro.Twinmold
            | ShortenCutsceneBossIntro.Majora
            | ShortenCutsceneBossIntro.Wart
            | ShortenCutsceneBossIntro.IgosDuIkana
            | ShortenCutsceneBossIntro.Gomess;
    }
}
