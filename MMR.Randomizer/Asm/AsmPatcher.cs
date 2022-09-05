using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Patcher for assembly patch file.
    /// </summary>
    public class AsmPatcher
    {
        private AlvReader.Entry[] _data;

        /// <summary>
        /// Address of the end of the MMFile table.
        /// </summary>
        const uint TABLE_END = 0x20700;

        /// <summary>
        /// Apply patches.
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns>Index of appended file.</returns>
        public int Apply(AsmSymbols symbols)
        {
            // Write patch data to existing MMFiles
            WriteToROM(symbols);

            // For our custom data, create and insert our own MMFile
            return CreateMMFile(symbols);
        }

        /// <summary>
        /// Generate the bytes for the <see cref="MMFile"/>.
        /// </summary>
        /// <param name="start">Start of virtual file</param>
        /// <returns></returns>
        public byte[] GetFileData(uint start, uint length)
        {
            var bytes = new byte[length];
            var memory = new Memory<byte>(bytes);
            foreach (var data in _data)
            {
                if (start <= data.Address)
                {
                    // Get offset relative to MMFile start.
                    var offset = data.Address - start;
                    data.Data.CopyTo(memory.Slice((int)offset));
                }
            }
            return bytes;
        }

        /// <summary>
        /// Create a <see cref="MMFile"/> from patch data and return the file index.
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public int CreateMMFile(AsmSymbols symbols)
        {
            var start = symbols.PayloadStart;
            var end = symbols.PayloadEnd;
            var data = GetFileData(start, end - start);
            return RomUtils.AppendFile(data);
        }

        /// <summary>
        /// Get whether or not an address is relevant to be included in the patch data.
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns></returns>
        public static bool IsAddressRelevant(uint address)
        {
            // If patch address is before or within MMFile table, ignore.
            return TABLE_END <= address;
        }

        /// <summary>
        /// Create <see cref="AsmPatcher"/> from ALV data.
        /// </summary>
        /// <param name="rawBytes">ALV raw bytes</param>
        /// <returns></returns>
        public static AsmPatcher FromAlv(byte[] rawBytes)
        {
            var list = new List<AlvReader.Entry>();
            var alvReader = new AlvReader(rawBytes);
            foreach (var entry in alvReader)
            {
                if (IsAddressRelevant(entry.Address))
                    list.Add(entry);
            }
            var patcher = new AsmPatcher();
            patcher._data = list.ToArray();
            return patcher;
        }

        /// <summary>
        /// Create <see cref="AsmPatcher"/> from GZip-compressed ALV data.
        /// </summary>
        /// <param name="compressedBytes">GZip-compressed ALV data</param>
        /// <returns></returns>
        public static AsmPatcher FromCompressedAlv(byte[] compressedBytes)
        {
            var decompressedBytes = CompressionUtils.GZipDecompress(compressedBytes);
            return FromAlv(decompressedBytes);
        }

        /// <summary>
        /// Load a <see cref="AsmPatcher"/> from the default resource file.
        /// </summary>
        /// <returns></returns>
        public static AsmPatcher Load()
        {
            return FromCompressedAlv(Resources.asm.rom_patch);
        }

        /// <summary>
        /// Patch existing <see cref="MMFile"/> files.
        /// </summary>
        public void WriteToROM(AsmSymbols symbols)
        {
            foreach (var data in _data)
                if (TABLE_END <= data.Address && data.Address < symbols.PayloadStart)
                    ReadWriteUtils.WriteToROM((int)data.Address, data.Data);
        }
    }
}
