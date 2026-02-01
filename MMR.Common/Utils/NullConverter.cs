using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMR.Common.Utils
{
    public class NullConverter : JsonConverter<WriteableNull>
    {
        public override WriteableNull Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, WriteableNull value, JsonSerializerOptions options)
        {
            writer.WriteNullValue();
        }

        public override bool HandleNull => true;
    }

    public class WriteableNull
    {

    }
}
