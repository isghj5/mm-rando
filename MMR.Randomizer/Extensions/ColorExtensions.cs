using System;
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

        /// <summary>
        /// Get color RGBA values as bytes with a specified alpha.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Bytes</returns>
        public static byte[] ToBytesRGB(this Color color, byte alpha)
        {
            return new byte[] { color.R, color.G, color.B, alpha, };
        }

        /// <summary>
        /// Get color RGBA values as bytes.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Bytes</returns>
        public static byte[] ToBytesRGBA(this Color color)
        {
            return new byte[] { color.R, color.G, color.B, color.A, };
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1.
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        /// <see cref="https://stackoverflow.com/a/12598573"/>
        public static Color ChangeBrightness(this Color color, float correctionFactor)
        {
            if (correctionFactor < -1.0 || 1.0 < correctionFactor)
            {
                throw new ArgumentOutOfRangeException(
                    "correctionFactor", correctionFactor, "Correction factor must be between -1.0 and 1.0");
            }

            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// Brighten a color by a specific amount.
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="amount">Amount to brighten</param>
        /// <returns>Brightened color</returns>
        public static Color Brighten(this Color color, float amount)
        {
            return ChangeBrightness(color, amount);
        }

        /// <summary>
        /// Darken a color by a specific amount.
        /// </summary>
        /// <param name="color">Color</param>
        /// <param name="amount">Amount to darken</param>
        /// <returns>Darkened color</returns>
        public static Color Darken(this Color color, float amount)
        {
            return ChangeBrightness(color, amount * -1.0f);
        }
    }
}
