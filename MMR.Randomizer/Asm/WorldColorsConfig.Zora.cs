using MMR.Randomizer.Extensions;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using System;
using System.Drawing;

namespace MMR.Randomizer.Asm
{
    public partial class WorldColorsConfig
    {
        /// <summary>
        /// Patch Zora object data to write new energy colors.
        /// </summary>
        /// <param name="data">Zora object data.</param>
        void PatchZoraEnergyColors(Span<byte> data)
        {
            // Offset to DList containing color instructions.
            var offset = 0x11760;

            // Write first pair of prim/env colors.
            Colors.ZoraEnergyPrim1.ToBytesRGB(0x82).CopyTo(data.Slice(offset + 0x1C));
            Colors.ZoraEnergyEnv1.ToBytesRGB(0xFF).CopyTo(data.Slice(offset + 0x24));

            // Write second pair of prim/env colors.
            Colors.ZoraEnergyPrim2.ToBytesRGB(0x82).CopyTo(data.Slice(offset + 0x19C));
            Colors.ZoraEnergyEnv2.ToBytesRGB(0xFF).CopyTo(data.Slice(offset + 0x1A4));
        }

        /// <summary>
        /// Update Zora energy colors from the given base color.
        /// </summary>
        /// <param name="color">Base color.</param>
        void SetZoraEnergyColors(Color color)
        {
            var converter = new ColorSpaceConverter();
            var darker = converter.TranslateHsv(color, hsv => new Hsv((hsv.H + 35f) % 360f, hsv.S, hsv.V * 0.392f));

            Colors.ZoraEnergyEnv1 = darker;
            Colors.ZoraEnergyEnv2 = color;
            Colors.ZoraEnergyPrim1 = color;
            // Use white for safety. Todo: Add option to use black for neat effect?
            Colors.ZoraEnergyPrim2 = Color.White;
        }
    }
}
