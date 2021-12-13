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
        public TimeOfDay TimeSetup { get; set; }
        public bool IsTrick { get; set; }

        private string _trickTooltip;
        public string TrickTooltip
        {
            get
            {
                return IsTrick ? _trickTooltip : null;
            }
            set
            {
                _trickTooltip = value;
            }
        }

        [JsonIgnore]
        public bool IsMultiLocation { get; set; }
    }
}
