using MMR.Randomizer.Attributes.Setting;
using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models.Settings
{

    public class OutputSettings
    {
        /// <summary>
        ///  Outputs n64 rom if true (default: true)
        /// </summary>
        [SettingName("N64 ROM .z64")]
        [Description("Output a randomized .z64 ROM that can be loaded into a N64 Emulator.")]
        public bool GenerateROM { get; set; } = true;

        /// <summary>
        ///  Outputs virtual channel if true
        /// </summary>
        [SettingName("Wii VC .wad")]
        [Description("Output a randomized .WAD file that can be loaded into a Wii Virtual Channel.")]
        public bool OutputVC { get; set; }

        /// <summary>
        /// Filepath to the input ROM
        /// </summary>
        [SettingName("Input ROM")]
        public string InputROMFilename { get; set; }

        /// <summary>
        /// Filepath to the input patch file
        /// </summary>
        [JsonIgnore]
        public string InputPatchFilename { get; set; }

        [JsonIgnore]
        public string OutputROMFilename { get; set; }

        /// <summary>
        /// Generate spoiler log on randomizing
        /// </summary>
        [SettingName("Spoiler log .txt")]
        [Description("Output a spoiler log.\n\n The spoiler log contains a list over all items, and their shuffled locations.\n In addition, the spoiler log contains version information, seed and settings string used in the randomization.")]
        public bool GenerateSpoilerLog { get; set; } = true;

        /// <summary>
        /// Generate HTML spoiler log on randomizing
        /// </summary>
        [SettingName("Item Tracker .html")]
        [Description("Output a html spoiler log (Requires spoiler log to be checked).\n\n Similar to the regular spoiler log, but readable in browsers. The locations/items are hidden by default, and hovering over them will make them visible.")]
        public bool GenerateHTMLLog { get; set; } = true;

        /// <summary>
        /// Generate patch file
        /// </summary>
        [SettingName("Patch .mmr")]
        [Description("Output a patch file that can be applied using the Patch settings tab to reproduce the same ROM.\nPatch file includes all settings except Tunic and Tatl color.")]
        public bool GeneratePatch { get; set; }

        public string Validate()
        {
            if (!GenerateROM && !OutputVC && (InputPatchFilename != null || (!GeneratePatch && !GenerateSpoilerLog && !GenerateHTMLLog)))
            {
                return "No output selected.";
            }
            if ((GenerateROM || GeneratePatch || OutputVC) && !File.Exists(InputROMFilename))
            {
                return "Input ROM not found, cannot generate output.";
            }
            if (InputPatchFilename != null && string.IsNullOrWhiteSpace(InputPatchFilename))
            {
                return "No patch selected.";
            }
            return null;
        }
    }
}
