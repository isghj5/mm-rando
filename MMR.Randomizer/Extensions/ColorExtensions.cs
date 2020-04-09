using System.Drawing;

namespace MMR.Randomizer.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Get color RGB values as bytes.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Bytes</returns>
        public static byte[] ToBytesRGB(this Color color)
        {
            return new byte[] { color.R, color.G, color.B, };
        }
    }
}
