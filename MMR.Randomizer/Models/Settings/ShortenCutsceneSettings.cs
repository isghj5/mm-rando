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
            | ShortenCutsceneGeneral.GoronVillageOwl
            | ShortenCutsceneGeneral.AutomaticCredits
            | ShortenCutsceneGeneral.PrincessDelivery
            | ShortenCutsceneGeneral.SunMask
            | ShortenCutsceneGeneral.Tingle
            | ShortenCutsceneGeneral.JimRunning
            | ShortenCutsceneGeneral.Kotake
            | ShortenCutsceneGeneral.HoldAText
            | ShortenCutsceneGeneral.EverythingElse
            ;

        public ShortenCutsceneBossIntro BossIntros { get; set; } =
            ShortenCutsceneBossIntro.Odolwa
            | ShortenCutsceneBossIntro.Goht
            | ShortenCutsceneBossIntro.Gyorg
            | ShortenCutsceneBossIntro.Majora
            | ShortenCutsceneBossIntro.IgosDuIkana
            | ShortenCutsceneBossIntro.Gomess;
    }
}
