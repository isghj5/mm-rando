using System.IO;
using System.IO.Compression;

namespace MMR.Randomizer.Utils
{
    public static class CompressionUtils
    {
        /// <summary>
        /// Fully decompress GZip-compressed data, and return the result.
        /// </summary>
        /// <param name="gzBytes">GZip-compressed data</param>
        /// <returns>Decompressed bytes.</returns>
        public static byte[] GZipDecompress(byte[] gzBytes)
        {
            using (var inputStream = new MemoryStream(gzBytes))
            using (var decompressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                decompressionStream.CopyTo(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}
