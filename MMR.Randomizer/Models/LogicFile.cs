using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models
{
    public class LogicFile
    {
        public int Version { get; set; }
        public List<JsonFormatLogicItem> Logic { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _jsonSerializerOptions);
        }

        public static LogicFile FromJson(string json)
        {
            var _logic = JsonSerializer.Deserialize<LogicFile>(json, _jsonSerializerOptions);
            foreach (var logicItem in _logic.Logic)
            {
                if (Enum.TryParse(logicItem.Id, out Item item))
                {
                    var multiLocationAttribute = item.GetAttribute<MultiLocationAttribute>();
                    if (multiLocationAttribute != null)
                    {
                        logicItem.RequiredItems.Clear();
                        logicItem.ConditionalItems.Clear();
                        logicItem.TimeAvailable = TimeOfDay.None;
                        logicItem.TimeNeeded = TimeOfDay.None;
                        logicItem.IsTrick = false;
                        logicItem.TrickTooltip = null;
                        foreach (var location in multiLocationAttribute.Locations)
                        {
                            logicItem.ConditionalItems.Add(new List<string> { location.ToString() });
                        }
                        logicItem.IsMultiLocation = true;
                    }
                }
            }

            return _logic;
        }

        private readonly static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            Converters =
            {
                new JsonColorConverter(),
                new JsonStringEnumConverter(),
            }
        };
    }
}
