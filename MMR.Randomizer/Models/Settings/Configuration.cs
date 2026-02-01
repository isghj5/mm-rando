
namespace MMR.Randomizer.Models.Settings
{
    public class Configuration
    {
        public GameplaySettings GameplaySettings { get; set; }
        public CosmeticSettings CosmeticSettings { get; set; }
        public OutputSettings OutputSettings { get; set; }

        public WebSettings WebSettings { get; set; } = null;

        public override string ToString()
        {
            return JsonCustomSerializer.Serialize(this);
        }

        public static Configuration FromJson(string json)
        {
            return JsonCustomSerializer.Deserialize<Configuration>(json);
        }
    }
}
