using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using SystemJsonSerializer = System.Text.Json.JsonSerializer;

namespace MMR.Common.Utils
{
    public static class JsonSerializer
    {
        public static TValue? Deserialize<TValue>(string json)
        {
            return SystemJsonSerializer.Deserialize<TValue>(json, _jsonSerializerOptions);
        }

        public static string Serialize<TValue>(TValue value)
        {
            return SystemJsonSerializer.Serialize(value, _jsonSerializerOptions);
        }

        private readonly static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonColorConverter(),
                new JsonStringEnumConverter(),
            }
        };

    }
}
