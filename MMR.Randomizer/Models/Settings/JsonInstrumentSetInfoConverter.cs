using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using MMR.Randomizer.Models.Rom;

namespace MMR.Randomizer.Models.Settings
{
    public class JsonInstrumentSetInfoConverter : JsonConverter<InstrumentSetInfo>
    {
        public class InstrumentSetInfoStub
        {
            public int BankBinaryLength { get; set; } = 0;
            public int BankSlot { get; set; }
            public int BankMetaDataLength { get; set; } = 0;
            public int Modified { get; set; } = 0;
            public List<byte> HashBinary { get; set; } = null;
            public List<SequenceSoundSampleBinaryData> InstrumentSamples { get; set; } = null;
        }

        public override InstrumentSetInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stub = JsonSerializer.Deserialize<InstrumentSetInfoStub>(ref reader, options);

            return new InstrumentSetInfo
            {
                BankBinary = new byte[stub.BankBinaryLength],
                BankSlot = stub.BankSlot,
                BankMetaData = new byte[stub.BankMetaDataLength],
                Modified = stub.Modified,
                Hash = BitConverter.ToInt64(stub.HashBinary.ToArray(), 0),
                InstrumentSamples = stub.InstrumentSamples
            };
        }

        public override void Write(Utf8JsonWriter writer, InstrumentSetInfo value, JsonSerializerOptions options)
        {
            //Use the default serialization logic
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
