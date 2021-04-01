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
        /// Patch Player Actor file data to write Fierce Deity energy colors.
        /// </summary>
        /// <param name="playerActorData">Player Actor file data.</param>
        void PatchFierceDeityEnergyColors(Span<byte> playerActorData)
        {
            PatchFierceDeitySparkleColors(playerActorData);
        }

        /// <summary>
        /// Patch Fierce Deity sparkle colors.
        /// </summary>
        /// <param name="playerActorData">Player Actor file data.</param>
        void PatchFierceDeitySparkleColors(Span<byte> playerActorData)
        {
            var offset = 0x2F964;
            Colors.FierceDeitySparklesInner.ToBytesRGB(0).CopyTo(playerActorData.Slice(offset + 0));
            Colors.FierceDeitySparklesOuter.ToBytesRGB(0).CopyTo(playerActorData.Slice(offset + 4));
        }

        /// <summary>
        /// Update Fierce Deity energy colors from the given base color.
        /// </summary>
        /// <param name="color">Sword beam energy color.</param>
        void SetFierceDeityEnergyColors(Color color)
        {
            var converter = new ColorSpaceConverter();
            var adjusted = converter.TranslateHsv(color, static hsv => new Hsv(hsv.H, Increase(hsv.S), Increase(hsv.V)));

            // Update sword beam damage colors.
            Colors.SwordBeamDamageEnv = adjusted;
            // Update sword beam energy colors.
            Colors.SwordBeamEnergyEnv = color;
            Colors.SwordBeamEnergyPrim = Color.White;
            // Update sparkle colors.
            Colors.FierceDeitySparklesInner = color.Brighten(0.5f);
            Colors.FierceDeitySparklesOuter = color;

            static float Increase(float input, float mult = 3f) => Math.Min(input * mult, 1f);
        }
    }
}
