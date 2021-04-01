using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace MMR.Yaz
{
    public static partial class Yaz
    {
        /// <summary>
        /// Size of Yaz0 header block.
        /// </summary>
        public const int HeaderSize = 0x10;

        /// <summary>
        /// Yaz0 magic number.
        /// </summary>
        public const uint Magic = 0x59617A30;

        /// <summary>
        /// Align given value to <c>0x10</c> boundary.
        /// </summary>
        /// <param name="value">Value to align.</param>
        /// <returns>Aligned value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int AlignTo16(int value)
        {
            return (value + 0xF) & -0x10;
        }

        /// <summary>
        /// Validate magic number value in header is expected.
        /// </summary>
        /// <param name="src">Source buffer with Yaz0 header</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ValidateMagic(ReadOnlySpan<byte> src)
        {
            if (BinaryPrimitives.ReadUInt32BigEndian(src) != Magic)
            {
                ThrowArgumentExceptionForMagic("src");
            }
        }
    }
}
