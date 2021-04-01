using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace MMR.Yaz
{
    public static partial class Yaz
    {
        /// <summary>
        /// Decode source buffer and return in a managed buffer.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <returns>Decoded data in managed buffer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Decode(ReadOnlySpan<byte> src)
        {
            var decodeSize = ReadDecodeSize(src);
            var dst = new byte[decodeSize];
            DecodeRaw(src.Slice(HeaderSize), dst, decodeSize);
            return dst;
        }

        /// <summary>
        /// Decode source buffer and write to provided destination buffer.
        /// </summary>
        /// <param name="src">Source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Decode(ReadOnlySpan<byte> src, Span<byte> dst)
        {
            DecodeRaw(src.Slice(HeaderSize), dst, ReadDecodeSize(src));
        }

        /// <summary>
        /// Decode raw source buffer (no header) and write to provided destination buffer.
        /// </summary>
        /// <param name="src">Raw source buffer.</param>
        /// <param name="dst">Destination buffer.</param>
        /// <param name="decodedSize">Decoded size.</param>
        public static void DecodeRaw(ReadOnlySpan<byte> src, Span<byte> dst, uint decodedSize)
        {
            int dstPlace = 0, srcPlace = 0;
            uint validBitCount = 0;
            byte curCodeByte = 0;

            while (dstPlace < decodedSize)
            {
                // Every 8 bytes is a "code byte," check if next one is now.
                if (validBitCount == 0)
                {
                    // Code byte contains 8 operations, 1 per bit: 0 = lookup, 1 = direct copy
                    curCodeByte = src[srcPlace++];

                    // If all bits set, do immediate 8-byte copy.
                    if (curCodeByte == 0xFF)
                    {
                        src.Slice(srcPlace, 8).CopyTo(dst.Slice(dstPlace, 8));
                        dstPlace += 8;
                        srcPlace += 8;
                        validBitCount = 0;
                        continue;
                    }
                    else
                    {
                        validBitCount = 8;
                    }
                }

                if ((curCodeByte & 0x80) != 0)
                {
                    // If current bit set, do direct byte copy from src to dst.
                    dst[dstPlace++] = src[srcPlace++];
                }
                else
                {
                    // Otherwise copy from previous decompressed data.
                    byte byte1 = src[srcPlace];
                    byte byte2 = src[srcPlace + 1];
                    srcPlace += 2;

                    // 12-bit distance (plus 1) to travel backwards
                    uint dist = (uint)(((byte1 & 0xF) << 8) | byte2);
                    uint copySource = (uint)dstPlace - (dist + 1);
                    // 4-bit number of bytes to copy, 0 is special.
                    uint numBytes = (uint)byte1 >> 4;

                    if (numBytes == 0)
                    {
                        // If 0, next byte + 0x12 is number to copy.
                        numBytes = (uint)(src[srcPlace++] + 0x12);
                    }
                    else
                    {
                        // If not 0, add 2 for number to copy.
                        numBytes += 2;
                    }

                    // Todo: Try using CopyTo here again with slices instead of loop.
                    for (uint i = 0; i < numBytes; i++)
                    {
                        dst[dstPlace++] = dst[(int)copySource++];
                    }
                }

                // Prepare to process next bit of code byte.
                curCodeByte <<= 1;
                validBitCount -= 1;
            }
        }

        /// <summary>
        /// Interpret decoded size of Yaz0 data.
        /// </summary>
        /// <param name="src">Source buffer with Yaz0 header.</param>
        /// <returns>Decoded size.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadDecodeSize(ReadOnlySpan<byte> src)
        {
            return BinaryPrimitives.ReadUInt32BigEndian(src.Slice(4));
        }
    }
}
