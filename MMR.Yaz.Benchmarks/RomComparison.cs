using BenchmarkDotNet.Attributes;
using MMR.Randomizer.Models.Rom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MMR.Yaz.Benchmarks
{
    using MzxYaz = Randomizer.Utils.Mzxrules.Yaz;

    [MemoryDiagnoser]
    public class CodeFileBenchmarks
    {
        const uint CodeVirtualStart = 0xB3C000;
        byte[] CodeBytes { get; set; }
        VirtualFile CodeFile { get; set; }
        RomFile Rom { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Rom = RomFile.From(PathUtil.GetInputRomFilePath());
            CodeFile = Rom.Files.First(x => x.VirtualStart == CodeVirtualStart);
            var slice = Rom.Slice(CodeFile);
            CodeBytes = Yaz.Decode(slice);
        }

        [Benchmark]
        public byte[] EncodeMzx()
        {
            var length = MzxYaz.Encode(CodeBytes, CodeBytes.Length, out var encoded);
            return encoded;
        }

        [Benchmark]
        public byte[] EncodeNew()
        {
            var length = Yaz.Encode(CodeBytes, out var encoded);
            return encoded;
        }
    }

    [MemoryDiagnoser]
    public class RomHeapBenchmarks
    {
        RomFile Rom { get; set; }
        byte[][] Samples { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Rom = RomFile.From(PathUtil.GetInputRomFilePath());
            var samples = new List<byte[]>();
            foreach (var entry in Rom.Files)
            {
                if (entry.IsCompressed)
                {
                    var slice = Rom.Slice(entry);
                    var bytes = slice.ToArray();
                    samples.Add(bytes);
                }
            }
            Samples = samples.ToArray();
        }

        [Benchmark]
        public byte[] DecodeMzx()
        {
            byte[] result = null;

            foreach (var sample in Samples)
            {
                var compressed = sample;
                using (var memoryStream = new MemoryStream(compressed))
                {
                    result = MzxYaz.Decode(memoryStream, compressed.Length);
                }
            }

            return result;
        }

        [Benchmark]
        public byte[] DecodeNew()
        {
            byte[] result = null;

            foreach (var sample in Samples)
            {
                result = Yaz.Decode(sample.AsSpan());
            }

            return result;
        }

        [Benchmark]
        public byte[] EncodeMzx()
        {
            byte[] result = null;

            foreach (var sample in Samples)
            {
                var compressed = sample;
                using (var memoryStream = new MemoryStream(compressed))
                {
                    result = MzxYaz.Decode(memoryStream, compressed.Length);
                }
                var length = MzxYaz.Encode(result, result.Length, out var encoded);
                // DebugUtil.Assert(sample.Length == length, "Encoded length is same as decoded length");
                result = encoded;
            }

            return result;
        }

        [Benchmark]
        public byte[] EncodeNew()
        {
            byte[] result = null;

            foreach (var sample in Samples)
            {
                result = Yaz.Decode(sample.AsSpan());
                var length = Yaz.Encode(result, out var encoded);
                result = encoded;
            }

            return result;
        }
    }

    [MemoryDiagnoser]
    public class RomSpanBenchmarks
    {
        RomFile Rom { get; set; }
        VirtualFile[] Samples { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            Rom = RomFile.From(PathUtil.GetInputRomFilePath());
            Samples = Rom.Files.Where(x => x.IsCompressed).ToArray();
        }

        [Benchmark]
        public byte[] DecodeNew()
        {
            byte[] result = null;

            foreach (var sample in Samples)
            {
                var slice = Rom.Slice(sample);
                result = Yaz.Decode(slice);
            }

            return result;
        }

        [Benchmark]
        public int EncodeNew()
        {
            int result = 0;

            foreach (var sample in Samples)
            {
                var slice = Rom.Slice(sample);
                var decoded = Yaz.Decode(slice);
                result = Yaz.EncodeWithHeader(decoded, slice);
                // Fill remaining bytes with 0.
                slice.Slice(result).Fill(0);
            }

            return result;
        }
    }
}
