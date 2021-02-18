using System.Drawing;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// World color values.
    /// </summary>
    public class WorldColors
    {
        public Color DekuDustInner { get; set; } = Color.FromArgb(0xFF, 0xFF, 0x37);
        public Color DekuDustOuter { get; set; } = Color.FromArgb(0x64, 0x32, 0x00);
        public Color DekuSparklesInner { get; set; } = Color.FromArgb(0xFF, 0xC8, 0xC8);
        public Color DekuSparklesOuter { get; set; } = Color.FromArgb(0xFF, 0xFF, 0x00);
        public Color FierceDeitySparklesInner { get; set; } = Color.FromArgb(0x64, 0xFF, 0xFF);
        public Color FierceDeitySparklesOuter { get; set; } = Color.FromArgb(0x00, 0x64, 0x64);
        public Color GoronPunchEnergyEnv1 { get; set; } = Color.FromArgb(0xFF, 0x00, 0x00);
        public Color GoronPunchEnergyEnv2 { get; set; } = Color.FromArgb(0xFF, 0x00, 0x00);
        public Color GoronPunchEnergyPrim { get; set; } = Color.FromArgb(0xFF, 0xC8, 0x32);
        public Color GoronRollInnerEnergyEnv { get; set; } = Color.FromArgb(0x9B, 0x00, 0x00);
        public Color GoronRollInnerEnergyPrim { get; set; } = Color.FromArgb(0xFF, 0x9B, 0x00);
        public Color GoronRollOuterEnergyEnv1 { get; set; } = Color.FromArgb(0x64, 0x00, 0x00);
        public Color GoronRollOuterEnergyEnv2 { get; set; } = Color.FromArgb(0xC8, 0x00, 0x00);
        public Color GoronRollOuterEnergyPrim1 { get; set; } = Color.FromArgb(0xFF, 0x00, 0x00);
        public Color GoronRollOuterEnergyPrim2 { get; set; } = Color.FromArgb(0xFF, 0x9B, 0x00);
        public Color SwordBeamDamageEnv { get; set; } = Color.FromArgb(0x00, 0xFF, 0xFF);
        public Color SwordBeamEnergyEnv { get; set; } = Color.FromArgb(0x00, 0x64, 0xFF);
        public Color SwordBeamEnergyPrim { get; set; } = Color.FromArgb(0xAA, 0xFF, 0xFF);
        public Color SwordChargeBlueEnergyEnv { get; set; } = Color.FromArgb(0x00, 0x64, 0xFF);
        public Color SwordChargeBlueEnergyPrim { get; set; } = Color.FromArgb(0xAA, 0xFF, 0xFF);
        public Color SwordChargeRedEnergyEnv { get; set; } = Color.FromArgb(0xFF, 0x64, 0x00);
        public Color SwordChargeRedEnergyPrim { get; set; } = Color.FromArgb(0xFF, 0xFF, 0xAA);
        public Color SwordChargeSparksBlue { get; set; } = Color.FromArgb(0x00, 0x00, 0xFF);
        public Color SwordChargeSparksRed { get; set; } = Color.FromArgb(0xFF, 0x00, 0x00);
        public Color SwordEnergyBlueEnv1 { get; set; } = Color.FromArgb(0x00, 0x00, 0xFF);
        public Color SwordEnergyBlueEnv2 { get; set; } = Color.FromArgb(0x00, 0x64, 0xFF);
        public Color SwordEnergyBluePrim { get; set; } = Color.FromArgb(0xAA, 0xFF, 0xFF);
        public Color SwordEnergyRedEnv1 { get; set; } = Color.FromArgb(0xFF, 0x00, 0x00);
        public Color SwordEnergyRedEnv2 { get; set; } = Color.FromArgb(0xFF, 0x64, 0x00);
        public Color SwordEnergyRedPrim { get; set; } = Color.FromArgb(0xFF, 0xFF, 0xAA);
        public Color ZoraEnergyEnv1 { get; set; } = Color.FromArgb(0x00, 0x00, 0x64);
        public Color ZoraEnergyEnv2 { get; set; } = Color.FromArgb(0x00, 0x96, 0xFF);
        public Color ZoraEnergyPrim1 { get; set; } = Color.FromArgb(0x00, 0x96, 0xFF);
        public Color ZoraEnergyPrim2 { get; set; } = Color.FromArgb(0xAA, 0xFF, 0xFF);
        public Color BlueBubble { get; set; } = Color.FromArgb(0x00, 0x00, 0xFF);

        /// <summary>
        /// Instance of <see cref="WorldColors"/> with default values.
        /// </summary>
        public static readonly WorldColors Defaults = new WorldColors();

        /// <summary>
        /// Get the default set of Human energy colors.
        /// </summary>
        public static Color[] DefaultHumanEnergyColors => new Color[]
        {
            Defaults.SwordEnergyBlueEnv2,
            Defaults.SwordEnergyRedEnv2,
        };

        /// <summary>
        /// Get the default set of Deku energy colors.
        /// </summary>
        public static Color[] DefaultDekuEnergyColors => new Color[]
        {
            Defaults.DekuSparklesOuter,
        };

        /// <summary>
        /// Get the default set of Goron energy colors.
        /// </summary>
        public static Color[] DefaultGoronEnergyColors => new Color[]
        {
            Defaults.GoronRollInnerEnergyPrim,
            Defaults.GoronRollOuterEnergyPrim1,
            Defaults.GoronRollOuterEnergyPrim2,
        };

        /// <summary>
        /// Get the default set of Zora energy colors.
        /// </summary>
        public static Color[] DefaultZoraEnergyColors => new Color[]
        {
            Defaults.ZoraEnergyPrim1,
        };

        /// <summary>
        /// Get the default set of Fierce Deity energy colors.
        /// </summary>
        public static Color[] DefaultFierceDeityEnergyColors => new Color[]
        {
            Defaults.SwordBeamEnergyEnv,
        };

        /// <summary>
        /// Get colors to write in <see cref="WorldColorsConfigStruct"/> structure.
        /// </summary>
        public Color[] StructColors => new Color[]
        {
            GoronPunchEnergyEnv1,
            GoronRollInnerEnergyEnv,
            SwordChargeBlueEnergyEnv,
            SwordChargeBlueEnergyPrim,
            SwordChargeRedEnergyEnv,
            SwordChargeRedEnergyPrim,
            SwordChargeSparksBlue,
            SwordChargeSparksRed,
            SwordEnergyBluePrim,
            SwordEnergyRedPrim,
            SwordBeamEnergyEnv,
            SwordBeamEnergyPrim,
            SwordBeamDamageEnv,
            BlueBubble,
        };
    }
}
