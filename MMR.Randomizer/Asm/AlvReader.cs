using Be.IO;
using Microsoft.Toolkit.HighPerformance.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Reader for ALV (address-length-value) data.
    /// </summary>
    public class AlvReader : IEnumerable<AlvReader.Entry>
    {
        /// <summary>
        /// ALV entry, containing address and data.
        /// </summary>
        public struct Entry
        {
            /// <summary>
            /// Address of data.
            /// </summary>
            public uint Address { get; }

            /// <summary>
            /// Data.
            /// </summary>
            public ReadOnlyMemory<byte> Data { get; }

            public Entry(uint address, ReadOnlyMemory<byte> data) => (Address, Data) = (address, data);
        }

        /// <inheritdoc/>
        public class Enumerator : IEnumerator<Entry>
        {
            /// <inheritdoc/>
            object IEnumerator.Current => this.CurrentEntry.Value;

            /// <inheritdoc/>
            Entry IEnumerator<Entry>.Current => this.CurrentEntry.Value;

            /// <summary>
            /// Current <see cref="Entry"/>, if any.
            /// </summary>
            public Entry? CurrentEntry { get; private set; }

            /// <summary>
            /// Underlying reader.
            /// </summary>
            private AlvReader _reader { get; }

            public Enumerator(AlvReader reader) => _reader = reader;

            /// <inheritdoc/>
            public void Dispose()
            {
                // Should not need to dispose of anything?
            }

            /// <inheritdoc/>
            public bool MoveNext()
            {
                var (success, entry) = _reader.ReadEntry();
                this.CurrentEntry = success ? new Entry?(entry) : new Entry?();
                return success;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                // Reset underlying reader.
                _reader.Reset();
            }
        }

        /// <summary>
        /// Full bytes.
        /// </summary>
        public ReadOnlyMemory<byte> Full { get; }

        /// <summary>
        /// Current offset.
        /// </summary>
        public int Offset { get; private set; }

        public AlvReader(byte[] full) => Full = new ReadOnlyMemory<byte>(full);

        /// <inheritdoc/>
        public IEnumerator<Entry> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Read next entry at current offset.
        /// </summary>
        /// <returns>Value tuple with success and entry.</returns>
        public (bool, Entry) ReadEntry()
        {
            var (success, entry, offset) = this.ReadEntry(this.Offset);
            this.Offset = offset;
            return (success, entry);
        }

        /// <summary>
        /// Read <see cref="Entry"/> at specified offset.
        /// </summary>
        /// <param name="offset">Offset to read at</param>
        /// <returns>Value tuple with success, entry and new offset.</returns>
        public (bool, Entry, int) ReadEntry(int offset)
        {
            var slice = this.Full.Slice(offset);
            // If at end of data, return success as false.
            if (slice.Length == 0)
            {
                return (false, new Entry(), offset);
            }
            // Read entry.
            using (var memoryStream = slice.AsStream())
            using (var reader = new BeBinaryReader(memoryStream))
            {
                var addr = reader.ReadUInt32();
                var length = reader.ReadInt32();
                var dataSlice = slice.Slice(8, length);
                var newOffset = offset + 8 + length;
                return (true, new Entry(addr, dataSlice), newOffset);
            }
        }

        /// <summary>
        /// Reset offset to 0.
        /// </summary>
        public void Reset()
        {
            this.Offset = 0;
        }
    }
}
