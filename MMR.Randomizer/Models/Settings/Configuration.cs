using MMR.Common.Utils;

namespace MMR.Randomizer.Models.Settings
{
    public class Configuration
    {
        public GameplaySettings GameplaySettings { get; set; }
        public CosmeticSettings CosmeticSettings { get; set; }
        public OutputSettings OutputSettings { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Configuration FromJson(string json)
        {
            return JsonSerializer.Deserialize<Configuration>(json);
        }
    }
}
