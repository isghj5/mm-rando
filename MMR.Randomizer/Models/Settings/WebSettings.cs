using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes.Setting;
using MMR.Randomizer.Models.Colors;
using MMR.Randomizer.Models.Rom;
using System.Collections.Generic;
using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    public class WebSettings
    {
        /// <summary>
        /// User Music Tracks.
        /// </summary>
        [Description("Upload custom music files in MMR's dedicated .mmrs format.\nYou can upload individual files or directories consisting of multiple\ncustom sequences. Implementation into the seed is controlled by\nthe CosmeticSettings.Music setting.\nNote: Multiple directories at once can only be uploaded by dragging them\nonto the text field.")]
        public List<SequenceInfo> SequencesAvailable { get; set; } = new List<SequenceInfo>();       
    }
}
