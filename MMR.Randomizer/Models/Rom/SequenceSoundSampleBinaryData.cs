
namespace MMR.Randomizer.Models.Rom
{
    public class SequenceSoundSampleBinaryData
    {
        public byte[] BinaryData { get; set; } = null;

        public uint Addr { get; set; } = 0;
        public uint Marker { get; set; } = 0;

        public long Hash { get; set; } = 0;
    }
}
