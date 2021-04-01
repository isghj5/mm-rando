using System;
using System.IO;

namespace MMR.Common.Extensions
{
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Read the exact amount of bytes required to fill the provided buffer, or throw an <see cref="EndOfStreamException"/> if end of stream is reached before finishing read.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="buffer">Buffer to fill.</param>
        /// <exception cref="EndOfStreamException"></exception>
        public static void ReadExact(this BinaryReader reader, Span<byte> buffer)
        {
            var fullLength = buffer.Length;
            while (buffer.Length > 0)
            {
                var amount = reader.Read(buffer);
                if (amount == 0)
                {
                    throw new EndOfStreamException(string.Format("Reached end of stream while attempting to read exact amount of bytes: {0}", fullLength));
                }
                buffer = buffer.Slice(amount);
            }
        }
    }
}
