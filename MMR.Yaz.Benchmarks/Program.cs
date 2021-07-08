using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MMR.Randomizer.Utils;
using MzxYaz = MMR.Randomizer.Utils.Mzxrules.Yaz;
using System;
using System.IO;

namespace MMR.Yaz.Benchmarks
{
    [MemoryDiagnoser]
    public class ExistingDecodeComparison
    {
        [Params(40, 100, 1000, 10000)]
        public int N { get; set; }

        [Params(true, false)]
        public bool UseRandom { get; set; }

        private byte[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var input = new byte[N];
            if (UseRandom)
            {
                new Random(42).NextBytes(input);
            }

            var newSize = MzxYaz.Encode(input, input.Length, out var result);
            if (newSize >= 0)
            {
                data = new byte[newSize];
                Buffer.BlockCopy(result, 0, data, 0, data.Length);
            }
        }

        [Benchmark]
        public byte[] DecodeUtils1()
        {
            return Yaz0Utils.Decompress(data);
        }

        [Benchmark]
        public byte[] DecodeMzx1()
        {
            var compressed = data;
            using (var memoryStream = new MemoryStream(compressed))
            {
                return MzxYaz.Decode(memoryStream, compressed.Length);
            }
        }
    }

    [MemoryDiagnoser]
    public class ExistingEncodeComparison
    {
        [Params(40, 100, 1000, 10000)]
        public int N { get; set; }

        [Params(true, false)]
        public bool UseRandom { get; set; }

        private byte[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            data = new byte[N];
            if (UseRandom)
            {
                new Random(42).NextBytes(data);
            }
        }

        [Benchmark]
        public byte[] EncodeUtils1()
        {
            return Yaz0Utils.Compress(data);
        }

        [Benchmark]
        public byte[] EncodeMzx1()
        {
            MzxYaz.Encode(data, data.Length, out var result);
            return result;
        }
    }

    public class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
