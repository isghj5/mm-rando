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

        /// <summary>
        /// When a new track starts playing in-game, show the name of the track at the bottom left of the screen.
        /// </summary>
        public bool ShowTrackName { get; set; }

        /// <summary>
        /// Replace item fanfares and swamp shooting gallery fanfares with sound effects.
        /// </summary>
        public bool DisableFanfares { get; set; }

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
            this.ShowTrackName = ((flags >> 30) & 1) == 1;
            this.DisableFanfares = ((flags >> 29) & 1) == 1;
        }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.RemoveMinorMusic ? (uint)1 : 0) << 31;
            flags |= (this.ShowTrackName ? (uint)1 : 0) << 30;
            flags |= (this.DisableFanfares ? (uint)1 : 0) << 29;
            return flags;
        }
    }

    public struct MusicConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public int? SequenceMaskFileIndex;
        public int? SequenceNamesFileIndex;
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
                writer.Write(this.SequenceNamesFileIndex ?? 0);
                writer.WriteUInt32(this.Flags);

                return memoryStream.ToArray();
            }
        }
    }

    public class MusicConfig : AsmConfig
    {
        public int? SequenceMaskFileIndex { get; set; }

        public int? SequenceNamesFileIndex { get; set; }

        public const byte NUM_SEQUENCES = 0x80;

        public const byte SEQUENCE_DATA_SIZE = 0x30;

        public const byte SEQUENCE_NAME_MAX_SIZE = 0x20;

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
                SequenceMaskFileIndex = RomUtils.AppendFile(new byte[NUM_SEQUENCES * SEQUENCE_DATA_SIZE]);
            }
            else
            {
                SequenceMaskFileIndex = null;
            }

            if (settings.Music == Models.Music.Random && settings.ShowTrackName)
            {
                SequenceNamesFileIndex = RomUtils.AppendFile(new byte[NUM_SEQUENCES * SEQUENCE_NAME_MAX_SIZE]);
            }
            else
            {
                SequenceNamesFileIndex = null;
            }
        }

        public override IAsmConfigStruct ToStruct(uint version)
        {
            return new MusicConfigStruct
            {
                Version = version,
                SequenceMaskFileIndex = this.SequenceMaskFileIndex,
                SequenceNamesFileIndex = this.SequenceNamesFileIndex,
                Flags = this.Flags.ToInt()
            };
        }
    }
}
