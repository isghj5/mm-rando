using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Builder for object data bundled into a single buffer.
    /// </summary>
    public class ObjectBundle
    {
        /// <summary>
        /// Current total size of all data.
        /// </summary>
        public uint Size { get; private set; }

        /// <summary>
        /// Object data chunks.
        /// </summary>
        List<byte[]> _chunks = new List<byte[]>();

        /// <summary>
        /// Append an object as bytes.
        /// </summary>
        /// <param name="data">Bytes</param>
        /// <returns>Tuple containing start and end offsets of object data.</returns>
        public (uint, uint) Append(byte[] data)
        {
            int remaining = data.Length % 0x10;
            if (remaining != 0)
            {
                var temp = new byte[data.Length + remaining];
                Array.Copy(data, temp, data.Length);
                data = temp;
            }

            uint offset = this.Size;

            // Append data and update total size
            _chunks.Add(data);
            this.Size += (uint)data.Length;

            return (offset, this.Size);
        }

        /// <summary>
        /// Get full data as bytes.
        /// </summary>
        /// <returns>Bytes</returns>
        public byte[] GetFull()
        {
            var result = new byte[this.Size];
            uint offset = 0;
            foreach (var chunk in _chunks)
            {
                Array.Copy(chunk, 0, result, offset, chunk.Length);
                offset += (uint)chunk.Length;
            }
            return result;
        }
    }
}
