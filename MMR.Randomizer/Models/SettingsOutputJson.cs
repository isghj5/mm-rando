using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.Models
{
    public class SettingsOutputJson
    {
        public string Version { get; set; }

        public GameplaySettings Settings { get; set; }

        public int Seed { get; set; }
    }
}

