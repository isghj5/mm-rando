using System;
using System.Collections.Generic;
using System.IO;

namespace MMR.Randomizer.Models.Rom
{
    public class RomFile
    {
        /// <summary>
        /// Expected offset of <c>dmadata</c> table.
        /// </summary>
        public const int DmaTableOffset = 0x1A500;

        /// <summary>
        /// Expected length of ROM file (32 MiB).
        /// </summary>
        public const int ExpectedLength = 0x2000000;

        /// <summary>
        /// Underlying buffer containing ROM data.
        /// </summary>
        public Memory<byte> Buffer { get; private set; }

        /// <summary>
        /// Parsed <see cref="VirtualFile"/> list.
        /// </summary>
        public VirtualFile[] Files { get; private set; } = new VirtualFile[0];

        /// <summary>
        /// Construct and allocate a buffer for holding ROM contents.
        /// </summary>
        public RomFile() => Buffer = new byte[ExpectedLength];

        /// <summary>
        /// Read a <see cref="RomFile"/> from a given file path.
        /// </summary>
        /// <param name="filepath">Path to ROM file.</param>
        /// <returns></returns>
        public static RomFile From(string filepath)
        {
            using (var stream = File.OpenRead(filepath))
            {
                return From(stream);
            }
        }

        /// <summary>
        /// Read a <see cref="RomFile"/> from a given <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        /// <returns></returns>
        public static RomFile From(Stream stream)
        {
            var rom = new RomFile();
            rom.ReadFrom(stream);
            return rom;
        }

        /// <summary>
        /// Read ROM data from given <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">Stream to read from.</param>
        void ReadFrom(Stream stream)
        {
            var amount = stream.Read(this.Buffer.Span);
            if (amount != this.Buffer.Length)
            {
                // Throw Exception?
            }
            this.Files = ReadFileTable();
        }

        /// <summary>
        /// Read <see cref="VirtualFile"/> table (dmadata) from ROM data.
        /// </summary>
        /// <param name="offset">Table offset.</param>
        /// <returns><see cref="VirtualFile"/> entries in table.</returns>
        VirtualFile[] ReadFileTable(int offset = DmaTableOffset)
        {
            int end = this.Buffer.Length;
            var list = new List<VirtualFile>();
            for (var index = offset; index < end; index += 0x10)
            {
                var span = Buffer.Span.Slice(index);
                var vfile = VirtualFile.Read(span);
                if (vfile.VirtualStart == (uint)offset)
                {
                    end = (int)vfile.VirtualEnd;
                }
                list.Add(vfile);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Get a slice of the ROM data for a specific <see cref="VirtualFile"/>.
        /// </summary>
        /// <param name="file">File used to get slice.</param>
        /// <returns></returns>
        public Span<byte> Slice(VirtualFile file)
        {
            if (file.IsIgnored)
            {
                throw new ArgumentException("File must not be Ignored to get slice of data.", "file");
            }
            var range = file.ToRange();
            return Buffer.Span.Slice((int)range.Start, (int)range.Length);
        }
    }
}
