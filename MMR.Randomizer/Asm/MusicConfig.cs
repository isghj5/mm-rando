using Be.IO;
using MMR.Common.Extensions;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System.IO;

namespace MMR.Randomizer.Asm
{
    public class MusicFlags
    {
        /// <summary>
        /// Minor music such as indoors and grottos will not play. Background music that is already playing will instead continue.
        /// </summary>
        public bool RemoveMinorMusic { get; set; }

        public MusicFlags()
        {
        }

        public MusicFlags(uint flags)
        {
            Load(flags);
        }

        /// <summary>
        /// Load from a <see cref="uint"/> integer.
        /// </summary>
        /// <param name="flags">Flags integer</param>
        void Load(uint flags)
        {
            this.RemoveMinorMusic = ((flags >> 31) & 1) == 1;
        }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.RemoveMinorMusic ? (uint)1 : 0) << 31;
            return flags;
        }
    }

    public struct MusicConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public int? SequenceMaskFileIndex;
        public uint Flags;

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
                writer.WriteUInt32(this.Flags);

                return memoryStream.ToArray();
            }
        }
    }

    public class MusicConfig : AsmConfig
    {
        public int? SequenceMaskFileIndex { get; set; }

        public const byte SEQUENCE_DATA_SIZE = 0x20;

        public MusicFlags Flags { get; set; }

        public MusicConfig() : this(new MusicFlags())
        {
        }

        public MusicConfig(MusicFlags flags)
        {
            Flags = flags;
        }

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
                Flags = this.Flags.ToInt()
            };
        }
    }
}
