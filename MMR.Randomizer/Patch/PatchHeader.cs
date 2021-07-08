using System;
using System.Buffers.Binary;

namespace MMR.Randomizer.Patch
{
    /// <summary>
    /// Header for file entries in patch data.
    /// </summary>
    public readonly struct PatchHeader
    {
        /// <summary>
        /// Size of serialized <see cref="PatchHeader"/>.
        /// </summary>
        public const int Size = 0x10;

        /// <summary>
        /// Address of file.
        /// </summary>
        public readonly uint Address { get; }

        /// <summary>
        /// Command for this entry.
        /// </summary>
        public readonly PatchCommand Command { get; }

        /// <summary>
        /// Flags for this entry.
        /// </summary>
        public readonly PatchFlags Flags { get; }

        /// <summary>
        /// Index of file.
        /// </summary>
        public readonly uint Index { get; }

        /// <summary>
        /// Length of data.
        /// </summary>
        public readonly int Length { get; }

        public PatchHeader(uint index, uint address, int length, PatchCommand command, PatchFlags flags)
        {
            Index = index;
            Address = address;
            Length = length;
            Command = command;
            Flags = flags;
        }

        /// <summary>
        /// Create a <see cref="PatchHeader"/> for patching an existing file.
        /// </summary>
        /// <param name="index">File index.</param>
        /// <param name="address">File address.</param>
        /// <param name="length">Patch data length.</param>
        /// <param name="isStatic">Whether or not file is static.</param>
        /// <returns></returns>
        public static PatchHeader CreateExisting(uint index, uint address, int length, bool isStatic)
        {
            var flags = isStatic ? PatchFlags.IsStatic : PatchFlags.None;
            return new PatchHeader(index, address, length, PatchCommand.ExistingFile, flags);
        }

        /// <summary>
        /// Create a <see cref="PatchHeader"/> for adding a new file.
        /// </summary>
        /// <param name="index">File index.</param>
        /// <param name="address">File address.</param>
        /// <param name="length">File data length.</param>
        /// <param name="isStatic">Whether or not file is static.</param>
        /// <returns></returns>
        public static PatchHeader CreateNew(uint index, uint address, int length, bool isStatic)
        {
            var flags = isStatic ? PatchFlags.IsStatic : PatchFlags.None;
            return new PatchHeader(index, address, length, PatchCommand.NewFile, flags);
        }

        /// <summary>
        /// Read a <see cref="PatchHeader"/> from a <see cref="ReadOnlySpan{byte}"/>.
        /// </summary>
        /// <param name="data">Source to read.</param>
        /// <returns></returns>
        public static PatchHeader Read(ReadOnlySpan<byte> data)
        {
            var index = BinaryPrimitives.ReadUInt32BigEndian(data);
            var address = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(0x4));
            var length = BinaryPrimitives.ReadInt32BigEndian(data.Slice(0x8));
            var command = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(0xC));
            var flags = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(0xE));
            return new PatchHeader(index, address, length, (PatchCommand)command, (PatchFlags)flags);
        }

        /// <summary>
        /// Write this <see cref="PatchHeader"/> to a <see cref="Span{byte}"/>.
        /// </summary>
        /// <param name="data">Destination to write.</param>
        public readonly void Write(Span<byte> data)
        {
            BinaryPrimitives.WriteUInt32BigEndian(data, Index);
            BinaryPrimitives.WriteUInt32BigEndian(data.Slice(0x4), Address);
            BinaryPrimitives.WriteInt32BigEndian(data.Slice(0x8), Length);
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(0xC), (ushort)Command);
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(0xE), (ushort)Flags);
        }
    }
}
