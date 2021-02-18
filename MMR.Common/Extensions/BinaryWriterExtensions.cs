using System.IO;

namespace MMR.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="BinaryWriter"/>.
    /// </summary>
    public static class BinaryWriterExtensions
    {
        /// <inheritdoc cref="BinaryWriter.Write(byte)"/>
        public static void WriteByte(this BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(byte[])"/>
        public static void WriteBytes(this BinaryWriter writer, byte[] buffer)
        {
            writer.Write(buffer);
        }

        /// <inheritdoc cref="BinaryWriter.Write(byte[], int, int)"/>
        public static void WriteBytes(this BinaryWriter writer, byte[] buffer, int index, int count)
        {
            writer.Write(buffer, index, count);
        }

        /// <inheritdoc cref="BinaryWriter.Write(char)"/>
        public static void WriteChar(this BinaryWriter writer, char ch)
        {
            writer.Write(ch);
        }

        /// <inheritdoc cref="BinaryWriter.Write(char[])"/>
        public static void WriteChars(this BinaryWriter writer, char[] chars)
        {
            writer.Write(chars);
        }

        /// <inheritdoc cref="BinaryWriter.Write(char[], int, int)"/>
        public static void WriteChars(this BinaryWriter writer, char[] chars, int index, int count)
        {
            writer.Write(chars, index, count);
        }

        /// <inheritdoc cref="BinaryWriter.Write(decimal)"/>
        public static void WriteDecimal(this BinaryWriter writer, decimal value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(double)"/>
        public static void WriteDouble(this BinaryWriter writer, double value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(float)"/>
        public static void WriteFloat(this BinaryWriter writer, float value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(short)"/>
        public static void WriteInt16(this BinaryWriter writer, short value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(int)"/>
        public static void WriteInt32(this BinaryWriter writer, int value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(long)"/>
        public static void WriteInt64(this BinaryWriter writer, long value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(sbyte)"/>
        public static void WriteSByte(this BinaryWriter writer, sbyte value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ushort)"/>
        public static void WriteUInt16(this BinaryWriter writer, ushort value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(uint)"/>
        public static void WriteUInt32(this BinaryWriter writer, uint value)
        {
            writer.Write(value);
        }

        /// <inheritdoc cref="BinaryWriter.Write(ulong)"/>
        public static void WriteUInt64(this BinaryWriter writer, ulong value)
        {
            writer.Write(value);
        }
    }
}
