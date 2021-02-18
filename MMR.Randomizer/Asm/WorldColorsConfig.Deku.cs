using MMR.Randomizer.Extensions;
using System;
using System.Drawing;

namespace MMR.Randomizer.Asm
{
    public partial class WorldColorsConfig
    {
        /// <summary>
        /// Patch Player Actor file data to write Deku energy colors.
        /// </summary>
        /// <param name="playerActorData">Player Actor file data.</param>
        void PatchDekuEnergyColors(Span<byte> playerActorData)
        {
            var offset = 0x2FEA0;
            // Write colors of dust effect caused by using deku flowers.
            Colors.DekuDustInner.ToBytesRGB(0xFF).CopyTo(playerActorData.Slice(offset + 0));
            Colors.DekuDustOuter.ToBytesRGB(0).CopyTo(playerActorData.Slice(offset + 4));
            // Write colors of sparkle effect when spinning.
            Colors.DekuSparklesInner.ToBytesRGB(0).CopyTo(playerActorData.Slice(offset + 8));
            Colors.DekuSparklesOuter.ToBytesRGB(0).CopyTo(playerActorData.Slice(offset + 0xC));
        }

        /// <summary>
        /// Update Deku energy colors from the given base color.
        /// </summary>
        /// <param name="color">Base color.</param>
        void SetDekuEnergyColors(Color color)
        {
            Colors.DekuDustInner = color.Brighten(0.2f);
            Colors.DekuDustOuter = color.Darken(0.4f);
            Colors.DekuSparklesInner = color.Brighten(0.7f);
            Colors.DekuSparklesOuter = color;
        }
    }
}
