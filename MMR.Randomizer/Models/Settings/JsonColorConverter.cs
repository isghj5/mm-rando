using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models.Settings
{
    public class JsonColorConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // TODO: Can optimize this further by using ReadOnlySpan<char> to split without allocations.
            var text = reader.GetString();
            var tokens = text.Split(", ");
            if (tokens.Length == 3)
            {
                // Assume color values.
                var values = tokens.Select(static x => byte.Parse(x)).ToArray();
                return Color.FromArgb(values[0], values[1], values[2]);
            }
            else
            {
                // Assume color name.
                return Color.FromName(text);
            }
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(string.Format("{0}, {1}, {2}", value.R, value.G, value.B));
        }
    }
}
