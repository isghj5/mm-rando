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
        /// Patch GameplayKeep object data to write new energy colors.
        /// </summary>
        /// <param name="data">GameplayKeep object data.</param>
        void PatchHumanEnergyColors(Span<byte> data)
        {
            PatchSwordSpinEnergyColors(data);
        }

        /// <summary>
        /// Patch colors used for sword spin energy effect.
        /// </summary>
        /// <param name="data">GameplayKeep object data.</param>
        void PatchSwordSpinEnergyColors(Span<byte> data)
        {
            // Write colors for blue sword spin energy, uses offsets of specific Display Lists.
            var blueOffset1 = 0x25850;
            Colors.SwordEnergyBlueEnv1.ToBytesRGB(0x80).CopyTo(data.Slice(blueOffset1 + 0xA4));
            var blueOffset2 = 0x25970;
            Colors.SwordEnergyBlueEnv2.ToBytesRGB(0x80).CopyTo(data.Slice(blueOffset2 + 0xA4));

            // Write colors for red sword spin energy, uses offsets of specific Display Lists.
            var redOffset1 = 0x25DD0;
            Colors.SwordEnergyRedEnv1.ToBytesRGB(0x80).CopyTo(data.Slice(redOffset1 + 0xA4));
            var redOffset2 = 0x25EF0;
            Colors.SwordEnergyRedEnv2.ToBytesRGB(0x80).CopyTo(data.Slice(redOffset2 + 0xA4));
        }

        /// <summary>
        /// Update Human energy colors from the given base colors.
        /// </summary>
        /// <param name="blueColor">Blue sword spin energy color.</param>
        /// <param name="redColor">Red sword spin energy color.</param>
        void SetHumanEnergyColors(Color blueColor, Color redColor)
        {
            var converter = new ColorSpaceConverter();
            var blueAdjusted = converter.TranslateHsv(blueColor, static hsv => new Hsv(hsv.H, Increase(hsv.S), Increase(hsv.V)));
            var redAdjusted = converter.TranslateHsv(redColor, static hsv => new Hsv(hsv.H, Increase(hsv.S), Increase(hsv.V)));

            // Update color for normal spin.
            Colors.SwordEnergyBlueEnv1 = blueColor;
            Colors.SwordEnergyBlueEnv2 = blueAdjusted;
            Colors.SwordEnergyBluePrim = Color.White;

            // Update color for greater spin.
            Colors.SwordEnergyRedEnv1 = redColor;
            Colors.SwordEnergyRedEnv2 = redAdjusted;
            Colors.SwordEnergyRedPrim = Color.White;

            // Update color for sword charge effect.
            Colors.SwordChargeBlueEnergyEnv = blueColor;
            Colors.SwordChargeBlueEnergyPrim = Color.White;
            Colors.SwordChargeRedEnergyEnv = redColor;
            Colors.SwordChargeRedEnergyPrim = Color.White;

            // Update color for sword charge sparks.
            Colors.SwordChargeSparksBlue = blueAdjusted;
            Colors.SwordChargeSparksRed = redAdjusted;

            static float Increase(float input, float mult = 3f) => Math.Min(input * mult, 1f);
        }
    }
}
