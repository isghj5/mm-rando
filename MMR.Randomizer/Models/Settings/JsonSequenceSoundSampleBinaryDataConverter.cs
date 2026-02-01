using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using MMR.Randomizer.Models.Rom;

namespace MMR.Randomizer.Models.Settings
{
    public class JsonSequenceSoundSampleBinaryDataConverter : JsonConverter<SequenceSoundSampleBinaryData>
    {
        public class SequenceSoundSampleBinaryDataStub
        {
            public int BinaryDataLength { get; set; } = 0;
            public uint Addr { get; set; } = 0;
            public uint Marker { get; set; } = 0;
            public List<byte> HashBinary { get; set; } = null;
        }

        public override SequenceSoundSampleBinaryData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stub = JsonSerializer.Deserialize<SequenceSoundSampleBinaryDataStub>(ref reader, options);

            return new SequenceSoundSampleBinaryData
            {
                BinaryData = new byte[stub.BinaryDataLength],
                Addr = stub.Addr,
                Marker = stub.Marker,
                Hash = BitConverter.ToInt64(stub.HashBinary.ToArray(), 0)
            };
        }

        public override void Write(Utf8JsonWriter writer, SequenceSoundSampleBinaryData value, JsonSerializerOptions options)
        {
            //Use the default serialization logic
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
