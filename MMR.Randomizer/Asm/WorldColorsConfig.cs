using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Utils;
using System.Drawing;

namespace MMR.Randomizer.Asm
{
    /// <summary>
    /// World Colors configuration.
    /// </summary>
    public partial class WorldColorsConfig : AsmConfig
    {
        /// <summary>
        /// World color values.
        /// </summary>
        public WorldColors Colors { get; set; } = new WorldColors();

        /// <summary>
        /// Apply energy colors for a specific <see cref="TransformationForm"/>.
        /// </summary>
        /// <param name="form">Transformation form.</param>
        /// <param name="colors">Energy colors to apply.</param>
        void ApplyEnergyColors(TransformationForm form, Color[] colors)
        {
            if (form == TransformationForm.Human)
            {
                SetHumanEnergyColors(colors[0], colors[1]);
            }
            else if (form == TransformationForm.Deku)
            {
                SetDekuEnergyColors(colors[0]);
            }
            else if (form == TransformationForm.Goron)
            {
                var options = new GoronColorOptions(colors[0], colors[1], colors[2]);
                SetGoronEnergyColors(options);
            }
            else if (form == TransformationForm.Zora)
            {
                SetZoraEnergyColors(colors[0]);
            }
            else if (form == TransformationForm.FierceDeity)
            {
                SetFierceDeityEnergyColors(colors[0]);
            }
        }

        /// <summary>
        /// Finalize colors given a <see cref="CosmeticSettings"/>.
        /// </summary>
        /// <param name="settings">Cosmetic settings.</param>
        public void FinalizeSettings(CosmeticSettings settings)
        {
            foreach (var kvp in settings.UseEnergyColors)
            {
                var form = kvp.Key;
                var useColors = kvp.Value;
                if (useColors)
                {
                    // Get and apply energy colors for specific transformation form.
                    var colors = settings.EnergyColors[form];
                    ApplyEnergyColors(form, colors);
                }
            }
        }

        /// <summary>
        /// Patch object data for new color values.
        /// </summary>
        public void PatchObjects()
        {
            var playerActor = RomData.MMFileList[38];
            PatchHumanEnergyColors(ObjUtils.GetObjectData(1));
            PatchDekuEnergyColors(playerActor.Data);
            PatchGoronEnergyColors(ObjUtils.GetObjectData(0x14C));
            PatchZoraEnergyColors(ObjUtils.GetObjectData(0x14D));
            PatchFierceDeityEnergyColors(playerActor.Data);
        }

        public override IAsmConfigStruct ToStruct(uint version)
        {
            return new WorldColorsConfigStruct
            {
                Version = version,
                Colors = this.Colors.StructColors,
            };
        }
    }
}
