using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MMR.Randomizer.Models.Rom
{
    public class MessageTable
    {
        /// <summary>
        /// VROM range which contains all message table entries.
        /// </summary>
        public static readonly (uint, uint) MessageTableRange = (0xC5D0D8, 0xC66040);

        /// <summary>
        /// File index of file containing message data.
        /// </summary>
        public static readonly int MessageDataFile = 29;

        /// <summary>
        /// All messages in the table, mapped by Id.
        /// </summary>
        Dictionary<ushort, MessageEntry> Messages;

        /// <summary>
        /// Max number of entries in message table.
        /// </summary>
        public uint MaxEntryCount { get; }

        /// <summary>
        /// Maximum length of message data file, if any.
        /// </summary>
        public uint? MaxDataLength { get; } = null;

        MessageTable(Dictionary<ushort, MessageEntry> messages, uint? maxDataLength = null)
        {
            this.MaxDataLength = maxDataLength;
            this.MaxEntryCount = (uint)messages.Count;
            this.Messages = messages;
        }

        public MessageTable(uint maxEntryCount)
        {
            this.MaxEntryCount = maxEntryCount;
            this.Messages = new Dictionary<ushort, MessageEntry>((int)maxEntryCount);
        }

        /// <summary>
        /// Add new messages into the table.
        /// </summary>
        /// <param name="messages">Messages</param>
        public void AddMessage(params MessageEntry[] messages)
        {
            foreach (var message in messages)
            {
                this.Messages.Add(message.Id, message);
            }
        }

        /// <summary>
        /// Update all messages to apply "quick text" hack.
        /// </summary>
        public void ApplyQuickText()
        {
            var regex = new Regex("(?<!(?:\x1B|\x1C|\x1D|\x1E).?)(?:\x1F..|\x17|\x18)", RegexOptions.Singleline);
            foreach (var entry in this.Messages.Values)
            {
                entry.Message = regex.Replace(entry.Message, "");
            }
        }

        /// <summary>
        /// Build two <see cref="byte"/> arrays for the message table and message data respectively.
        /// </summary>
        /// <returns>Byte arrays</returns>
        public (byte[], byte[]) BuildData()
        {
            // Verify message count does not exceed maximum.
            if (this.Messages.Count > this.MaxEntryCount)
            {
                throw new Exception($"Message table contains {this.Messages.Count} records, when max is {this.MaxEntryCount}");
            }

            // Calculate total size and align to 0x10-byte boundary.
            var totalSum = this.Messages.Values.Sum(x => x.Size);
            var totalSize = (totalSum + 0xF) & -0x10;

            // Verify message data length does not exceed maximum.
            if (this.MaxDataLength != null && totalSize > this.MaxDataLength)
            {
                throw new Exception($"Message data bigger than 0x{this.MaxDataLength:X} bytes");
            }

            // Build full message data buffer.
            var dataBytes = new byte[totalSize];
            var tableBytes = new byte[(this.Messages.Count + 1) * 8];

            var index = 0;
            var offset = 0;
            foreach (var message in this.Messages.Values)
            {
                // Copy message table bytes.
                ReadWriteUtils.Arr_WriteU16(tableBytes, (index * 8), message.Id);
                ReadWriteUtils.Arr_WriteU32(tableBytes, (index * 8) + 4, (uint)offset | 0x0800_0000);

                // Copy message data bytes.
                var data = message.ToBytes();
                Buffer.BlockCopy(data, 0, dataBytes, offset, data.Length);

                index += 1;
                offset += data.Length;
            }

            // Write terminator record.
            ReadWriteUtils.Arr_WriteU16(tableBytes, (index * 8), 0xFFFF);
            ReadWriteUtils.Arr_WriteU32(tableBytes, (index * 8) + 4, 0);

            return (tableBytes, dataBytes);
        }

        /// <summary>
        /// Get existing message by Id, or null if none found.
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <returns>Message, or null if none found</returns>
        public MessageEntry GetMessage(ushort id)
        {
            if (this.Messages.ContainsKey(id))
            {
                return this.Messages[id];
            }
            return null;
        }

        /// <summary>
        /// Read the default <see cref="MessageTable"/>.
        /// </summary>
        /// <returns>Message table</returns>
        public static MessageTable ReadDefault()
        {
            // Read message table from default addresses in ROM.
            var range = MessageTableRange;
            var index = MessageDataFile;
            return ReadFromROM(range.Item1, range.Item2, index);
        }

        /// <summary>
        /// Read a <see cref="MessageTable"/> from ROM.
        /// </summary>
        /// <param name="start">Table start VROM address</param>
        /// <param name="end">Table end VROM address</param>
        /// <param name="fileIndex">File index of message data file</param>
        /// <returns><see cref="MessageTable"/> read from ROM.</returns>
        public static MessageTable ReadFromROM(uint start, uint end, int fileIndex)
        {
            uint length = (end - start) / 8;
            var entries = new Dictionary<ushort, MessageEntry>();

            for (uint i = 0; i < length; i++)
            {
                // Read MessageTable entry data (text Id and offset).
                int addr = (int)(start + (i * 8));
                var textId = ReadWriteUtils.ReadU16(addr);
                var offset = ReadWriteUtils.ReadU32(addr + 4) & 0xFFFFFF;

                // Check if terminator record.
                if (textId == 0xFFFF)
                {
                    break;
                }

                // Read MessageEntry from data file.
                var dataFile = RomData.MMFileList[fileIndex];
                var entry = MessageEntry.FromBytes(textId, dataFile.Data, (int)offset);

                entries.Add(textId, entry);
            }

            // Calculate maximum message data length from existing file.
            var maxDataLength = (uint)(RomData.MMFileList[fileIndex + 1].Addr - RomData.MMFileList[fileIndex].Addr);

            return new MessageTable(entries, maxDataLength);
        }

        /// <summary>
        /// Update with new <see cref="MessageEntry"/> message.
        /// </summary>
        /// <param name="message">Message</param>
        public void UpdateMessages(MessageEntry message)
        {
            if (this.Messages.ContainsKey(message.Id))
            {
                if (message.Header == null)
                {
                    message.UpdateHeader(this.Messages[message.Id].Header);
                }
                this.Messages[message.Id] = message;
            }
        }

        /// <summary>
        /// Update with multiple new <see cref="MessageEntry"/> messages.
        /// </summary>
        /// <param name="messages">Messages</param>
        public void UpdateMessages(IEnumerable<MessageEntry> messages)
        {
            foreach (var item in messages)
            {
                this.UpdateMessages(item);
            }
        }

        /// <summary>
        /// Overwrite default <see cref="MessageTable"/> in ROM along with message data.
        /// </summary>
        /// <param name="table"><see cref="MessageTable"/> to write</param>
        /// <param name="isQuickTextEnabled">Whether or not to apply "quick text" hacks</param>
        public static void WriteDefault(MessageTable table, bool isQuickTextEnabled)
        {
            // Apply "quick text" hack if specified.
            if (isQuickTextEnabled)
            {
                table.ApplyQuickText();
            }

            var (tableBytes, dataBytes) = table.BuildData();

            // Calculate offset of table in respective file.
            var tableStart = (int)MessageTableRange.Item1;
            var index = RomUtils.AddrToFile(tableStart);
            var tableFile = RomData.MMFileList[index];
            var tableOffset = tableStart - tableFile.Addr;

            // Write message table in-place.
            Buffer.BlockCopy(tableBytes, 0, tableFile.Data, tableOffset, tableBytes.Length);

            // Update message data file.
            var file = RomData.MMFileList[MessageDataFile];
            file.Data = dataBytes;
        }

        /// <summary>
        /// Write an extended <see cref="MessageTable"/> to a specific address, and append a new file with the message data.
        /// </summary>
        /// <param name="table"><see cref="MessageTable"/> to write</param>
        /// <param name="addr">Address to write table</param>
        /// <param name="addDummy">Whether or not to add a final dummy entry</param>
        /// <returns>Message data file index</returns>
        public static int WriteExtended(MessageTable table, uint addr, bool addDummy = true)
        {
            if (addDummy)
            {
                // Add unused dummy message entry, so that the previous table entry can calculate its data length at runtime.
                table.AddMessage(new MessageEntry(0xFFFE, "\u00BF"));
            }

            // Build message table and data bytes.
            var (tableBytes, dataBytes) = table.BuildData();

            // Write message table in-place.
            ReadWriteUtils.WriteToROM((int)addr, tableBytes);

            // Write extended message data as own file.
            var index = RomUtils.AppendFile(dataBytes);

            return index;
        }
    }
}
