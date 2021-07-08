using System.IO;

namespace MMR.Yaz.Benchmarks
{
    public static class PathUtil
    {
        public const string RomFilename = "Rom.z64";

        public static string FixFilePath(string filepath)
        {
            return Path.Join("..", "..", "..", "..", filepath);
        }

        public static string GetInputFilePath(string filename)
        {
            return FixFilePath(Path.Join("inputs", filename));
        }

        public static string GetInputRomFilePath()
        {
            return GetInputFilePath(RomFilename);
        }
    }
}
