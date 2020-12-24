using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// Core options used for Asm.
    /// </summary>
    public class AsmOptionsGameplay
    {
        /// <summary>
        /// Miscellaneous configuration.
        /// </summary>
        public MiscConfig MiscConfig { get; set; } = new MiscConfig();

        /// <summary>
        /// Miscellaneous configuration.
        /// </summary>
        public MMRConfig MMRConfig { get; set; } = new MMRConfig();
    }

    /// <summary>
    /// Post-patch options used for Asm.
    /// </summary>
    public class AsmOptionsCosmetic
    {
        /// <summary>
        /// Hash bytes.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// D-Pad configuration.
        /// </summary>
        public DPadConfig DPadConfig { get; set; } = new DPadConfig();

        /// <summary>
        /// HUD colors configuration.
        /// </summary>
        public HudColorsConfig HudColorsConfig { get; set; } = new HudColorsConfig();

        /// <summary>
        /// World colors configuration.
        /// </summary>
        public WorldColorsConfig WorldColorsConfig { get; set; } = new WorldColorsConfig();

        /// <summary>
        /// Finalize all settings given a <see cref="CosmeticSettings"/>.
        /// </summary>
        /// <param name="settings">Cosmetic settings.</param>
        public void FinalizeSettings(CosmeticSettings settings)
        {
            WorldColorsConfig.FinalizeSettings(settings);
        }
    }
}
