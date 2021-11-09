using Be.IO;
using MMR.Common.Extensions;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System.IO;

namespace MMR.Randomizer.Asm
{
    public struct MusicConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public int? SequenceMaskFileIndex;

        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] ToBytes()
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new BeBinaryWriter(memoryStream))
            {
                writer.WriteUInt32(this.Version);
                writer.Write(this.SequenceMaskFileIndex ?? 0);

                return memoryStream.ToArray();
            }
        }
    }

    public class MusicConfig : AsmConfig
    {
        public int? SequenceMaskFileIndex { get; set; }

        public const byte SEQUENCE_DATA_SIZE = 0x20;

        public void FinalizeSettings(CosmeticSettings settings)
        {
            if (settings.Music == Models.Music.Random)
            {
                SequenceMaskFileIndex = RomUtils.AppendFile(new byte[0x80 * SEQUENCE_DATA_SIZE]);
            }
            else
            {
                SequenceMaskFileIndex = null;
            }
        }

        public override IAsmConfigStruct ToStruct(uint version)
        {
            return new MusicConfigStruct
            {
                Version = version,
                SequenceMaskFileIndex = this.SequenceMaskFileIndex,
            };
        }
    }
}
