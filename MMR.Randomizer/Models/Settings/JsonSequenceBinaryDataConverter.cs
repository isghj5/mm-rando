using System;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using MMR.Randomizer.Models.Rom;

namespace MMR.Randomizer.Models.Settings
{
    public class JsonSequenceBinaryDataConverter : JsonConverter<SequenceBinaryData>
    {
        public class SequenceBinaryDataStub
        {
            public int SequenceBinaryLength { get; set; } = 0;
            public InstrumentSetInfo InstrumentSet { get; set; } = null;
            public byte[] FormMask { get; internal set; }
        }

        public override SequenceBinaryData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stub = JsonSerializer.Deserialize<SequenceBinaryDataStub>(ref reader, options);

            return new SequenceBinaryData
            {
                SequenceBinary = new byte[stub.SequenceBinaryLength],
                InstrumentSet = stub.InstrumentSet,
                FormMask = stub.FormMask
            };
        }

        public override void Write(Utf8JsonWriter writer, SequenceBinaryData value, JsonSerializerOptions options)
        {
            //Use the default serialization logic
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
