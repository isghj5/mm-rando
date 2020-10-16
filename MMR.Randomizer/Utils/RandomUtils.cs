using System;
using System.Drawing;

namespace MMR.Randomizer.Utils
{
    public class RandomUtils
    {
        /// <summary>
        /// Generate a random color.
        /// </summary>
        /// <param name="random">Random</param>
        /// <returns>Color</returns>
        public static Color GetRandomColor(Random random)
        {
            var bytes = new byte[3];
            random.NextBytes(bytes);
            return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }
    }
}
