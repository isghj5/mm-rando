using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System;
using System.Drawing;

namespace MMR.Randomizer.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ColorSpaceConverter"/>.
    /// </summary>
    public static class ColorSpaceConverterExtensions
    {
        /// <summary>
        /// Translate a given <see cref="Color"/> as <see cref="Hsv"/> values.
        /// </summary>
        /// <param name="converter">Converter.</param>
        /// <param name="color">Source <see cref="Color"/>.</param>
        /// <param name="func">Translate function.</param>
        /// <returns>Translated <see cref="Color"/>.</returns>
        public static Color TranslateHsv(this ColorSpaceConverter converter, Color color, Func<Hsv, Hsv> func)
        {
            var hsv = converter.ToHsv(ToRgb(color));
            var adjustedHsv = func(hsv);
            return ToColor(converter.ToRgb(adjustedHsv));
        }

        /// <summary>
        /// Convert <see cref="Rgb"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="converter">Converter.</param>
        /// <param name="rgb">Source <see cref="Rgb"/>.</param>
        /// <returns>Converted <see cref="Color"/>.</returns>
        public static Color ToColor(this ColorSpaceConverter converter, Rgb rgb)
        {
            return ToColor(rgb);
        }

        /// <summary>
        /// Convert <see cref="Hsv"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="converter">Converter.</param>
        /// <param name="hsv">Source <see cref="Hsv"/>.</param>
        /// <returns>Converted <see cref="Color"/>.</returns>
        public static Color ToColor(this ColorSpaceConverter converter, Hsv hsv)
        {
            return ToColor(converter.ToRgb(hsv));
        }

        /// <summary>
        /// Convert <see cref="Color"/> to <see cref="Hsv"/>.
        /// </summary>
        /// <param name="converter">Converter.</param>
        /// <param name="color">Source <see cref="Color"/>.</param>
        /// <returns>Converted <see cref="Hsv"/>.</returns>
        public static Hsv ToHsv(this ColorSpaceConverter converter, Color color)
        {
            return converter.ToHsv(ToRgb(color));
        }

        /// <summary>
        /// Convert <see cref="Color"/> to <see cref="Rgb"/>.
        /// </summary>
        /// <param name="converter">Converter.</param>
        /// <param name="color">Source <see cref="Color"/>.</param>
        /// <returns>Converted <see cref="Rgb"/>.</returns>
        public static Rgb ToRgb(this ColorSpaceConverter converter, Color color)
        {
            return ToRgb(color);
        }

        static Color ToColor(Rgb rgb)
        {
            return Color.FromArgb((byte)(rgb.R * 255f), (byte)(rgb.G * 255f), (byte)(rgb.B * 255f));
        }

        static Rgb ToRgb(Color color)
        {
            return new Rgb(color.R / 255f, color.G / 255f, color.B / 255f);
        }
    }
}
