using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Patcher for assembly patch file.
    /// </summary>
    public class Patcher
    {
        private AlvReader.Entry[] _data;

        /// <summary>
        /// Address of the end of the MMFile table.
        /// </summary>
        const uint TABLE_END = 0x20700;

        /// <summary>
        /// Apply patches using <see cref="Symbols"/> loaded from the internal resource.
        /// </summary>
        /// <param name="options">Options</param>
        public void Apply(AsmOptionsGameplay options)
        {
            Apply(Symbols.Load(), options);
        }

        /// <summary>
        /// Apply patches.
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="options">Options</param>
        public void Apply(Symbols symbols, AsmOptionsGameplay options)
        {
            // Write patch data to existing MMFiles
            WriteToROM(symbols);

            // For our custom data, create and insert our own MMFile
            var file = CreateMMFile(symbols);
            RomUtils.AppendFile(file);

            // Create the pause menu payload MMFile
            var kaleidoFile = CreateMMKaleidoFile(symbols);
            RomUtils.AppendFile(kaleidoFile);

            // Encode Symbols into a special MMFile and insert that too
            var symbolsFile = symbols.CreateMMFile();
            RomUtils.AppendFile(symbolsFile);

            // Write subset of configuration (hardcoded into patch)
            symbols.ApplyConfiguration(options);
        }

        /// <summary>
        /// Generate the bytes for the <see cref="MMFile"/>.
        /// </summary>
        /// <param name="start">Start of virtual file</param>
        /// <returns>Bytes</returns>
        public byte[] GetFileData(uint start, uint length)
        {
            var bytes = new byte[length];
            var memory = new Memory<byte>(bytes);
            foreach (var data in _data)
            {
                if (start <= data.Address && data.Address < start + length)
                {
                    // Get offset relative to MMFile start.
                    var offset = data.Address - start;
                    data.Data.CopyTo(memory.Slice((int)offset));
                }
            }
            return bytes;
        }

        /// <summary>
        /// Create a <see cref="MMFile"/> from patch data.
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <returns>MMFile</returns>
        public MMFile CreateMMFile(Symbols symbols)
        {
            var start = symbols.PayloadStart;
            var end = symbols.PayloadEnd;

            var data = GetFileData(start, end - start);

            var file = new MMFile
            {
                Addr = (int)start,
                End = (int)start + data.Length,
                IsCompressed = false,
                IsStatic = true,
                Data = data,
            };

            return file;
        }

        public MMFile CreateMMKaleidoFile(Symbols symbols)
        {
            var start = symbols.KaleidoPayloadStart;
            var end = symbols.KaleidoPayloadEnd;

            var data = GetFileData(start, end - start);

            var file = new MMFile
            {
                Addr = (int)start,
                End = (int)start + data.Length,
                IsCompressed = false,
                IsStatic = true,
                Data = data,
            };

            return file;
        }

        /// <summary>
        /// Get whether or not an address is relevant to be included in the patch data.
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns>true if relevant, false if not.</returns>
        public static bool IsAddressRelevant(uint address)
        {
            // If patch address is before or within MMFile table, ignore.
            return TABLE_END <= address;
        }

        /// <summary>
        /// Create <see cref="Patcher"/> from ALV data.
        /// </summary>
        /// <param name="rawBytes">ALV raw bytes</param>
        /// <returns><see cref="Patcher"/>.</returns>
        public static Patcher FromAlv(byte[] rawBytes)
        {
            var list = new List<AlvReader.Entry>();
            var alvReader = new AlvReader(rawBytes);
            foreach (var entry in alvReader)
            {
                if (IsAddressRelevant(entry.Address))
                    list.Add(entry);
            }
            var patcher = new Patcher();
            patcher._data = list.ToArray();
            return patcher;
        }

        /// <summary>
        /// Create <see cref="Patcher"/> from GZip-compressed ALV data.
        /// </summary>
        /// <param name="compressedBytes">GZip-compressed ALV data</param>
        /// <returns><see cref="Patcher"/>.</returns>
        public static Patcher FromCompressedAlv(byte[] compressedBytes)
        {
            var decompressedBytes = CompressionUtils.GZipDecompress(compressedBytes);
            return FromAlv(decompressedBytes);
        }

        /// <summary>
        /// Load a <see cref="Patcher"/> from the default resource file.
        /// </summary>
        /// <returns>Patcher</returns>
        public static Patcher Load()
        {
            return FromCompressedAlv(Resources.asm.rom_patch);
        }

        /// <summary>
        /// Patch existing <see cref="MMFile"/> files.
        /// </summary>
        public void WriteToROM(Symbols symbols)
        {
            foreach (var data in _data)
                if (TABLE_END <= data.Address && data.Address < symbols.PayloadStart)
                    ReadWriteUtils.WriteToROM((int)data.Address, data.Data);
        }
    }
}
