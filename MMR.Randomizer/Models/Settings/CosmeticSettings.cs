﻿using MMR.Randomizer.Asm;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Colors;
using System.Collections.Generic;
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
        public string HeartsSelection { get; set; } = ColorSelectionManager.Hearts.GetItems()[0].Name;

        /// <summary>
        /// Magic color selection used for HUD color override.
        /// </summary>
        public string MagicSelection { get; set; } = ColorSelectionManager.MagicMeter.GetItems()[0].Name;

        /// <summary>
        /// Whether or not to perform hue shift for colors of miscellaneous UI elements.
        /// </summary>
        public bool ShiftHueMiscUI { get; set; }

        /// <summary>
        /// Randomize sound effects
        /// </summary>
        public bool RandomizeSounds { get; set; }

        /// <summary>
        /// Replaces Tatl's colors
        /// </summary>
        public TatlColorSchema TatlColorSchema { get; set; }

        /// <summary>
        /// Randomize background music (includes bgm from other video games)
        /// </summary>
        public Music Music { get; set; }

        /// <summary>
        /// Default Z-Targeting style to Hold
        /// </summary>
        public bool EnableHoldZTargeting { get; set; }

        /// <summary>
        /// Enables playing BGM at night for scenes that switch to night sfx
        /// </summary>
        public bool EnableNightBGM { get; set; }

        /// <summary>
        /// Enabling this makes the randomizer not remove the pictobox antialiasing function.
        /// Pictobox antialiasing is what makes the Pictobox incredibly slow on emulators.
        /// </summary>
        public bool KeepPictoboxAntialiasing { get; set; }

        /// <summary>
        /// Sets the Low health beeping sfx
        /// </summary>
        public LowHealthSFX LowHealthSFX { get; set; }

        public CombatMusic DisableCombatMusic { get; set; }

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

        public bool RainbowTunic
        {
            get { return this.AsmOptions.WorldColorsConfig.Flags.RainbowTunic; }
            set { this.AsmOptions.WorldColorsConfig.Flags.RainbowTunic = value; }
        }

        public bool BombTrapsRandomizeTunicColor
        {
            get { return this.AsmOptions.WorldColorsConfig.Flags.BombTrapsRandomizeTunicColor; }
            set { this.AsmOptions.WorldColorsConfig.Flags.BombTrapsRandomizeTunicColor = value; }
        }

        /// <summary>
        /// Minor music such as indoors and grottos will not play. Background music that is already playing will instead continue.
        /// </summary>
        public bool RemoveMinorMusic
        {
            get { return this.AsmOptions.MusicConfig.Flags.RemoveMinorMusic; }
            set { this.AsmOptions.MusicConfig.Flags.RemoveMinorMusic = value; }
        }

        /// <summary>
        /// When a new track starts playing in-game, show the name of the track at the bottom left of the screen.
        /// </summary>
        public bool ShowTrackName
        {
            get { return this.AsmOptions.MusicConfig.Flags.ShowTrackName; }
            set { this.AsmOptions.MusicConfig.Flags.ShowTrackName = value; }
        }

        /// <summary>
        /// Replace item fanfares and swamp shooting gallery fanfares with sound effects.
        /// </summary>
        public bool DisableFanfares
        {
            get { return this.AsmOptions.MusicConfig.Flags.DisableFanfares; }
            set { this.AsmOptions.MusicConfig.Flags.DisableFanfares = value; }
        }

        #endregion
    }
}
