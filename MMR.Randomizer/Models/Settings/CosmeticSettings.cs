using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes.Setting;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Colors;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models.Settings
{

    public class CosmeticSettings
    {
        /// <summary>
        /// Options for the Asm <see cref="Patcher"/>.
        /// </summary>
        [JsonIgnore]
        public AsmOptionsCosmetic AsmOptions { get; set; } = new AsmOptionsCosmetic();

        /// <summary>
        /// Hearts color selection used for HUD color override.
        /// </summary>
        [Description("Hearts color selection used for HUD color override.")]
        [SettingType("Enum", typeof(ColorSelectionManager), nameof(ColorSelectionManager.Hearts))]
        [SettingExclude("Random Choice", nameof(HudColors) + "." + nameof(Asm.HudColors.Heart), nameof(HudColors) + "." + nameof(Asm.HudColors.HeartDD))]
        [SettingExclude("Completely Random", nameof(HudColors) + "." + nameof(Asm.HudColors.Heart), nameof(HudColors) + "." + nameof(Asm.HudColors.HeartDD))]
        public string HeartsSelection { get; set; } = ColorSelectionManager.Hearts.GetItems()[0].Name;

        /// <summary>
        /// Magic color selection used for HUD color override.
        /// </summary>
        [Description("Magic color selection used for HUD color override.")]
        [SettingType("Enum", typeof(ColorSelectionManager), nameof(ColorSelectionManager.MagicMeter))]
        [SettingExclude("Random Choice", nameof(HudColors) + "." + nameof(Asm.HudColors.Magic), nameof(HudColors) + "." + nameof(Asm.HudColors.MagicInf))]
        [SettingExclude("Completely Random", nameof(HudColors) + "." + nameof(Asm.HudColors.Magic), nameof(HudColors) + "." + nameof(Asm.HudColors.MagicInf))]
        public string MagicSelection { get; set; } = ColorSelectionManager.MagicMeter.GetItems()[0].Name;

        /// <summary>
        /// Whether or not to perform hue shift for colors of miscellaneous UI elements.
        /// </summary>
        [SettingName("Shift Hue Misc UI")]
        [Description("Shifts the color of miscellaneous UI elements.")]
        public bool ShiftHueMiscUI { get; set; }

        /// <summary>
        /// Randomize sound effects
        /// </summary>
        [Description("Randomize sound effects that are played throughout the game.")]
        public bool RandomizeSounds { get; set; }

        /// <summary>
        /// Replaces Tatl's colors
        /// </summary>
        [Description("Select a color scheme to replace Tatl's default color scheme.")]
        public TatlColorSchema TatlColorSchema { get; set; }

        /// <summary>
        /// Randomize background music (includes bgm from other video games)
        /// </summary>
        [Description("Select a music option\n\n - Default: Vanilla background music.\n - Random: Randomized background music.\n - None: No background music.")]
        [SettingExclude(Music.Default, nameof(MusicLuckRollChance))]
        [SettingExclude(Music.None, nameof(MusicLuckRollChance))]
        public Music Music { get; set; }

        /// <summary>
        /// Replaces Tatl's colors
        /// </summary>
        [Description("Select a camera style.\n\n - Default: Vanilla camera.\n - Responsive: Link's movement will always be relative to the direction the camera is facing.\n - Instant: When Z-Targeting to move the camera behind Link, it will do it very quickly. Camera panning when climbing ledges is also sped up.")]
        public CameraStyle CameraStyle { get; set; }

        /// <summary>
        /// Default Z-Targeting style to Hold
        /// </summary>
        [Description("Default Z-Targeting style to Hold.")]
        public bool EnableHoldZTargeting { get; set; }

        /// <summary>
        /// Enables playing BGM at night for scenes that switch to night sfx
        /// </summary>
        [SettingName("Enable Night BGM")]
        [Description("Enables playing daytime Background music during nighttime in the field.\n(Clocktown night music can be weird)")]
        public bool EnableNightBGM { get; set; }

        /// <summary>
        /// Enabling this makes the randomizer not remove the pictobox antialiasing function.
        /// Pictobox antialiasing is what makes the Pictobox incredibly slow on emulators.
        /// </summary>
        [Description("Remove anti-aliasing from the Pictobox pictures, which is what makes Pictobox on emulator so slow.")]
        public bool KeepPictoboxAntialiasing { get; set; }

        /// <summary>
        /// Sets the Low health beeping sfx
        /// </summary>
        [SettingName("Low Heatlh Sound")]
        [Description("Select a Low Health SFX setting:\n\n - Default: Vanilla sound.\n - Disabled: No sound will play.\n - Random: a random SFX will be chosen.\n - Specific SFX: a specific SFX will play as the low health sfx.")]
        public LowHealthSFX LowHealthSFX { get; set; }

        [Description("Disables combat music around all regular (non boss or miniboss) enemies in the game.")]
        public bool DisableCombatMusic { get; set; }

        [Description("Music Rando comes with a chance to accept a song from outside of its categories.\n - This controls the percentage chance of a Luck Roll allowing out-of-category music placement\n - This is per specific slot+song check\n - Only songs with their first category being a general category (0-16) are Luck Rollable.")]
        [Range(0.0, 100.0)]
        public decimal MusicLuckRollChance { get; set; } = 3.33m;

        public Dictionary<TransformationForm, bool> UseEnergyColors { get; set; } = new Dictionary<TransformationForm, bool>()
        {
            { TransformationForm.Human, false },
            { TransformationForm.Deku, false },
            { TransformationForm.Goron, false },
            { TransformationForm.Zora, false },
            { TransformationForm.FierceDeity, false },
        };

        public Dictionary<TransformationForm, Color[]> EnergyColors { get; set; } = new Dictionary<TransformationForm, Color[]>()
        {
            { TransformationForm.Human, WorldColors.DefaultHumanEnergyColors },
            { TransformationForm.Deku, WorldColors.DefaultDekuEnergyColors },
            { TransformationForm.Goron, WorldColors.DefaultGoronEnergyColors },
            { TransformationForm.Zora, WorldColors.DefaultZoraEnergyColors },
            { TransformationForm.FierceDeity, WorldColors.DefaultFierceDeityEnergyColors },
        };

        public Dictionary<TransformationForm, bool> UseTunicColors { get; set; } = new Dictionary<TransformationForm, bool>()
        {
            { TransformationForm.Human, false },
            { TransformationForm.Deku, false },
            { TransformationForm.Goron, false },
            { TransformationForm.Zora, false },
            { TransformationForm.FierceDeity, false }
        };

        public Dictionary<TransformationForm, Color> TunicColors { get; set; } = new Dictionary<TransformationForm, Color>()
        {
            // TODO unique default tunic colors
            { TransformationForm.Human, Color.FromArgb(0x1E, 0x69, 0x1B) },
            { TransformationForm.Deku, Color.FromArgb(0x1E, 0x69, 0x1B) },
            { TransformationForm.Goron, Color.FromArgb(0x1E, 0x69, 0x1B) },
            { TransformationForm.Zora, Color.FromArgb(0x1E, 0x69, 0x1B) },
            { TransformationForm.FierceDeity, Color.FromArgb(0xBD, 0xB5, 0xAD) }
        };

        public Dictionary<TransformationForm, Instrument> Instruments { get; set; } = new Dictionary<TransformationForm, Instrument>()
        {
            { TransformationForm.Human, TransformationForm.Human.DefaultInstrument().Value },
            { TransformationForm.Deku, TransformationForm.Deku.DefaultInstrument().Value },
            { TransformationForm.Goron, TransformationForm.Goron.DefaultInstrument().Value },
            { TransformationForm.Zora, TransformationForm.Zora.DefaultInstrument().Value },
        };

        #region Asm Getters / Setters

        /// <summary>
        /// D-Pad configuration.
        /// </summary>
        public DPadConfig DPad {
            get { return this.AsmOptions.DPadConfig; }
            set { this.AsmOptions.DPadConfig = value; }
        }

        /// <summary>
        /// HUD colors.
        /// </summary>
        public HudColors HudColors {
            get { return this.AsmOptions.HudColorsConfig.Colors; }
            set { this.AsmOptions.HudColorsConfig.Colors = value; }
        }

        [Description("Link's tunic color will slowly cycle its hue.")]
        public bool RainbowTunic
        {
            get { return this.AsmOptions.WorldColorsConfig.Flags.RainbowTunic; }
            set { this.AsmOptions.WorldColorsConfig.Flags.RainbowTunic = value; }
        }

        [Description("When you find a Bomb Trap, Link's tunic will randomly change color.")]
        public bool BombTrapsRandomizeTunicColor
        {
            get { return this.AsmOptions.WorldColorsConfig.Flags.BombTrapsRandomizeTunicColor; }
            set { this.AsmOptions.WorldColorsConfig.Flags.BombTrapsRandomizeTunicColor = value; }
        }

        /// <summary>
        /// Minor music such as indoors and grottos will not play. Background music that is already playing will instead continue.
        /// </summary>
        [Description("Minor music such as indoors and grottos will not play. Background music that is already playing will instead continue.")]
        public bool RemoveMinorMusic
        {
            get { return this.AsmOptions.MusicConfig.Flags.RemoveMinorMusic; }
            set { this.AsmOptions.MusicConfig.Flags.RemoveMinorMusic = value; }
        }

        /// <summary>
        /// When a new track starts playing in-game, show the name of the track at the bottom left of the screen.
        /// </summary>
        [Description("When a new track starts playing in-game, show the name of the track at the bottom left of the screen.")]
        public bool ShowTrackName
        {
            get { return this.AsmOptions.MusicConfig.Flags.ShowTrackName; }
            set { this.AsmOptions.MusicConfig.Flags.ShowTrackName = value; }
        }

        /// <summary>
        /// Replace item fanfares and swamp shooting gallery fanfares with sound effects.
        /// </summary>
        [Description("Replace item fanfares and swamp shooting gallery fanfares with sound effects.")]
        public bool DisableFanfares
        {
            get { return this.AsmOptions.MusicConfig.Flags.DisableFanfares; }
            set { this.AsmOptions.MusicConfig.Flags.DisableFanfares = value; }
        }

        #endregion
    }
}
