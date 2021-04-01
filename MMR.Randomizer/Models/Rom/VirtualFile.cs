using System;
using System.Buffers.Binary;

namespace MMR.Randomizer.Models.Rom
{
    public readonly partial struct VirtualFile
    {
        /// <summary>
        /// Virtual start address.
        /// </summary>
        public readonly uint VirtualStart;

        /// <summary>
        /// Virtual end address.
        /// </summary>
        public readonly uint VirtualEnd;

        /// <summary>
        /// Physical start address.
        /// </summary>
        public readonly uint PhysicalStart;

        /// <summary>
        /// Physical end address, may be 0 if <see cref="VirtualFile"/> is decompressed.
        /// </summary>
        public readonly uint PhysicalEnd;

        /// <summary>
        /// Whether or not this <see cref="VirtualFile"/> contains compressed data.
        /// </summary>
        public readonly bool IsCompressed => !IsIgnored && PhysicalEnd != 0;

        /// <summary>
        /// Whether or not this <see cref="VirtualFile"/> is empty.
        /// </summary>
        public readonly bool IsEmpty => VirtualStart == 0 && VirtualEnd == 0;

        /// <summary>
        /// Checks if physical start and end addresses are both <c>0xFFFFFFFF</c>, indicating the file is ignored.
        /// </summary>
        public readonly bool IsIgnored => PhysicalStart == 0xFFFFFFFF && PhysicalEnd == 0xFFFFFFFF;

        public VirtualFile(uint vstart, uint vend, uint pstart, uint pend)
        {
            VirtualStart = vstart;
            VirtualEnd = vend;
            PhysicalStart = pstart;
            PhysicalEnd = pend;
        }

        /// <summary>
        /// Read a <see cref="VirtualFile"/> from a <see cref="ReadOnlySpan{T}"/>.
        /// </summary>
        /// <param name="span">Memory to read from.</param>
        /// <returns></returns>
        public static VirtualFile Read(ReadOnlySpan<byte> span)
        {
            var word1 = BinaryPrimitives.ReadUInt32BigEndian(span);
            var word2 = BinaryPrimitives.ReadUInt32BigEndian(span.Slice(0x4));
            var word3 = BinaryPrimitives.ReadUInt32BigEndian(span.Slice(0x8));
            var word4 = BinaryPrimitives.ReadUInt32BigEndian(span.Slice(0xC));
            return new VirtualFile(word1, word2, word3, word4);
        }

        /// <summary>
        /// Get the <see cref="ValueRange"/> of the file data relative to ROM start.
        /// </summary>
        /// <remarks>Certain files may return a range outside of the ROM bounds.</remarks>
        /// <returns></returns>
        public ValueRange ToRange()
        {
            if (IsCompressed || IsIgnored)
            {
                return new ValueRange(PhysicalStart, PhysicalEnd);
            }
            else
            {
                var length = VirtualEnd - VirtualStart;
                return new ValueRange(PhysicalStart, PhysicalStart + length);
            }
        }

        /// <summary>
        /// Get whether or not a given virtual address is within this file's data.
        /// </summary>
        /// <param name="addr">Virtual address.</param>
        /// <returns></returns>
        public readonly bool Within(uint addr) => VirtualStart <= addr && addr < VirtualEnd;
    }
}
