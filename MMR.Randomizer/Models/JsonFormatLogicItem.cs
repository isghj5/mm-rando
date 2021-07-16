using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models
{
    public class JsonFormatLogicItem
    {
        public string Id { get; set; }
        public List<string> RequiredItems { get; set; } = new List<string>();
        public List<List<string>> ConditionalItems { get; set; } = new List<List<string>>();
        public TimeOfDay TimeNeeded { get; set; }
        public TimeOfDay TimeAvailable { get; set; }
        public bool IsTrick { get; set; }
        public string TrickTooltip { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsMultiLocation { get; set; }
    }
}
