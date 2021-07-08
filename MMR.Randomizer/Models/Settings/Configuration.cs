using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models.Settings
{
    public class Configuration
    {
        public GameplaySettings GameplaySettings { get; set; }
        public CosmeticSettings CosmeticSettings { get; set; }
        public OutputSettings OutputSettings { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _jsonSerializerOptions);
        }

        public static Configuration FromJson(string json)
        {
            return JsonSerializer.Deserialize<Configuration>(json, _jsonSerializerOptions);
        }

        private readonly static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            Converters =
            {
                new JsonColorConverter(),
                new JsonStringEnumConverter(),
            }
        };
    }
}
