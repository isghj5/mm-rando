﻿using MMR.Randomizer.Utils;
using Newtonsoft.Json;
using System.IO;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Crit wiggle state value.
    /// </summary>
    public enum CritWiggleState : byte
    {
        Default,
        AlwaysOn,
        AlwaysOff,
    }

    public enum QuestConsumeState : byte
    {
        Default,
        Always,
        Never,
    }

    /// <summary>
    /// Miscellaneous flags.
    /// </summary>
    public class MiscFlags
    {
        /// <summary>
        /// Whether or not to enable cycling arrow types while using the bow.
        /// </summary>
        public bool ArrowCycling { get; set; }

        /// <summary>
        /// Whether or not to show magic being consumed ahead of time when using elemental arrows.
        /// </summary>
        public bool ArrowMagic => ArrowCycling;

        /// <summary>
        /// Behaviour of crit wiggle.
        /// </summary>
        public CritWiggleState CritWiggle { get; set; }

        /// <summary>
        /// Whether or not to disable crit wiggle (alternative state is default behaviour).
        /// </summary>
        public bool CritWiggleDisable {
            get { return (this.CritWiggle == CritWiggleState.AlwaysOff) ? true : false; }
            set { this.CritWiggle = (value ? CritWiggleState.AlwaysOff : CritWiggleState.Default); }
        }

        /// <summary>
        /// Whether or not to use the closest cow to the player when giving an item (not yet implemented).
        /// </summary>
        public bool CloseCows { get; set; }

        /// <summary>
        /// Whether or not to draw hash icons on the file select screen.
        /// </summary>
        public bool DrawHash { get; set; } = true;

        /// <summary>
        /// Whether or not to enable faster pushing and pulling speeds.
        /// </summary>
        public bool FastPush { get; set; }

        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        public bool FreestandingModels { get; set; } = true;

        /// <summary>
        /// Whether or not to allow using the ocarina underwater.
        /// </summary>
        public bool OcarinaUnderwater { get; set; }

        /// <summary>
        /// Behaviour of how quest items should be consumed.
        /// </summary>
        public QuestConsumeState QuestConsume => this.QuestItemStorage ? QuestConsumeState.Always : QuestConsumeState.Default;

        /// <summary>
        /// Whether or not to enable Quest Item Storage.
        /// </summary>
        public bool QuestItemStorage { get; set; } = true;

        public MiscFlags()
        {
        }

        public MiscFlags(uint flags)
        {
            Load(flags);
        }

        /// <summary>
        /// Load from a <see cref="uint"/> integer.
        /// </summary>
        /// <param name="flags">Flags integer</param>
        void Load(uint flags)
        {
            this.CritWiggle = (CritWiggleState)(flags >> 30);
            this.DrawHash = ((flags >> 29) & 1) == 1;
            this.FastPush = ((flags >> 28) & 1) == 1;
            this.OcarinaUnderwater = ((flags >> 27) & 1) == 1;
            this.QuestItemStorage = ((flags >> 26) & 1) == 1;
            this.CloseCows = ((flags >> 25) & 1) == 1;
            this.FreestandingModels = ((flags >> 24) & 1) == 1;
            this.ArrowCycling = ((flags >> 21) & 1) == 1;
        }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (((uint)this.CritWiggle) & 3) << 30;
            flags |= (this.DrawHash ? (uint)1 : 0) << 29;
            flags |= (this.FastPush ? (uint)1 : 0) << 28;
            flags |= (this.OcarinaUnderwater ? (uint)1 : 0) << 27;
            flags |= (this.QuestItemStorage ? (uint)1 : 0) << 26;
            flags |= (this.CloseCows ? (uint)1 : 0) << 25;
            flags |= (this.FreestandingModels ? (uint)1 : 0) << 24;
            flags |= (((uint)this.QuestConsume) & 3) << 22;
            flags |= (this.ArrowCycling ? (uint)1 : 0) << 21;
            flags |= (this.ArrowMagic ? (uint)1 : 0) << 20;
            return flags;
        }
    }

    /// <summary>
    /// Internal flags.
    /// </summary>
    public class InternalFlags
    {
        /// <summary>
        /// Whether or not Vanilla Layout is being used, which determines if certain mod files are included.
        /// </summary>
        public bool VanillaLayout { get; set; }

        /// <summary>
        /// Convert to a <see cref="uint"/> integer.
        /// </summary>
        /// <returns>Integer</returns>
        public uint ToInt()
        {
            uint flags = 0;
            flags |= (this.VanillaLayout ? (uint)1 : 0) << 31;
            return flags;
        }
    }

    /// <summary>
    /// Miscellaneous configuration structure.
    /// </summary>
    public struct MiscConfigStruct : IAsmConfigStruct
    {
        public uint Version;
        public byte[] Hash;
        public uint Flags;
        public uint InternalFlags;

        /// <summary>
        /// Convert to bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] ToBytes()
        {
            using (var memStream = new MemoryStream())
            using (var writer = new BinaryWriter(memStream))
            {
                ReadWriteUtils.WriteU32(writer, this.Version);

                // Version 0
                writer.Write(this.Hash);
                writer.Write(ReadWriteUtils.Byteswap32(this.Flags));

                // Version 1
                if (this.Version >= 1)
                {
                    writer.Write(ReadWriteUtils.Byteswap32(this.InternalFlags));
                }

                return memStream.ToArray();
            }
        }
    }

    /// <summary>
    /// Miscellaneous configuration.
    /// </summary>
    public class MiscConfig : AsmConfig
    {
        /// <summary>
        /// Hash bytes.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// Flags.
        /// </summary>
        public MiscFlags Flags { get; set; }

        /// <summary>
        /// Internal flags.
        /// </summary>
        public InternalFlags InternalFlags { get; set; }

        public MiscConfig()
            : this(new byte[0], new MiscFlags(), new InternalFlags())
        {
        }

        public MiscConfig(byte[] hash, MiscFlags flags, InternalFlags internalFlags)
        {
            this.Hash = hash;
            this.Flags = flags;
            this.InternalFlags = internalFlags;
        }

        /// <summary>
        /// Get a <see cref="MiscConfigStruct"/> representation.
        /// </summary>
        /// <param name="version">Structure version</param>
        /// <returns>Configuration structure</returns>
        public override IAsmConfigStruct ToStruct(uint version)
        {
            var hash = ReadWriteUtils.CopyBytes(this.Hash, 0x10);

            return new MiscConfigStruct
            {
                Version = version,
                Hash = hash,
                Flags = this.Flags.ToInt(),
                InternalFlags = this.InternalFlags.ToInt(),
            };
        }
    }
}
