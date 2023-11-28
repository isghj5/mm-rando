using System;
using System.Linq;

namespace MMR.Randomizer.Models.Rom
{
    public class MessageEntry
    {
        /// <summary>
        /// Default header bytes for most messages.
        /// </summary>
        public static readonly byte[] DefaultHeader = new byte[] { 0x00, 0x00, 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };

        /// <summary>
        /// Message Id.
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// Message contents.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Exclude this message from quick text alteration.
        /// </summary>
        public bool ExcludeFromQuickText { get; set; }

        /// <summary>
        /// Indicate that this message should be removed from the main message table and transfer it to the extended message table.
        /// </summary>
        public bool ShouldTransferToExtendedMessageTable { get; set; }

        /// <summary>
        /// Message header bytes.
        /// </summary>
        public byte[] Header { get; set; }

        /// <summary>
        /// Offset into message data file.
        /// </summary>
        public int? Offset { get; set; } = null;

        /// <summary>
        /// Size of data in bytes, including trailing padding bytes uses for alignment.
        /// </summary>
        public int Size => (11 + Message.Length + 3) & -4;

        public MessageEntry()
        {
        }

        public MessageEntry(short id, string message)
            : this((ushort)id, message)
        {
        }

        public MessageEntry(ushort id, string message)
        {
            this.Id = id;
            this.Message = message;
            this.Header = DefaultHeader;
        }

        MessageEntry(ushort id, string message, byte[] header, int offset)
        {
            this.Id = id;
            this.Message = message;
            this.Offset = offset;
            this.UpdateHeader(header);
        }

        /// <summary>
        /// Update the header bytes and verify that the length is correct.
        /// </summary>
        /// <param name="header">Header bytes</param>
        public void UpdateHeader(byte[] header)
        {
            if (header.Length != 11)
                throw new Exception("MessageEntry header bytes must be exactly 11 bytes in length");
            this.Header = (byte[])header.Clone();
        }

        /// <summary>
        /// Read an existing <see cref="MessageEntry"/> from a <see cref="byte"/> array subset.
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <param name="buf">Byte array</param>
        /// <param name="offset">Offset into buffer to begin reading</param>
        /// <param name="size">Size of the message entry</param>
        /// <returns><see cref="MessageEntry"/> from bytes.</returns>
        public static MessageEntry FromBytes(ushort id, byte[] buf, int offset, int size)
        {
            // Copy initial 11 bytes of header.
            var header = new byte[11];
            Buffer.BlockCopy(buf, offset, header, 0, header.Length);

            var messageBuffer = new byte[size];
            Buffer.BlockCopy(buf, offset + 11, messageBuffer, 0, messageBuffer.Length);

            // Remove end padding
            for (var i = size; i > 0; i--)
            {
                if (messageBuffer[i - 1] != 0)
                {
                    size = i;
                    break;
                }
            }

            var message = new string(messageBuffer.Take(size).Select(b => (char)b).ToArray());

            return new MessageEntry(id, message, header, offset);
        }

        /// <summary>
        /// Get a <see cref="byte"/> array representation of this <see cref="MessageEntry"/>.
        /// </summary>
        /// <returns>Byte array.</returns>
        public byte[] ToBytes()
        {
            var header = this.GetHeaderBytes();
            var message = Array.ConvertAll(this.Message.ToCharArray(), x => (byte)x);
            // var size = (header.Length + message.Length + 3) & -4;
            var result = new byte[this.Size];
            Buffer.BlockCopy(header, 0, result, 0, header.Length);
            Buffer.BlockCopy(message, 0, result, header.Length, message.Length);
            return result;
        }

        /// <summary>
        /// Get header bytes.
        /// </summary>
        /// <returns>Header bytes.</returns>
        public byte[] GetHeaderBytes()
        {
            if (this.Header != null)
            {
                return this.Header;
            }
            else
            {
                return DefaultHeader;
            }
        }

        public bool Equals(MessageEntry entry) =>
            Id == entry.Id &&
            Header.SequenceEqual(entry.Header) &&
            Message.Equals(entry.Message);
    }
}
