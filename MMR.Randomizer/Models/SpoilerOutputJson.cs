using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MMR.Randomizer.Models
{
    public class SpoilerOutputJson
    {
        public string Version { get; set; }

        public GameplaySettings Settings { get; set; }

        public int Seed { get; set; }

        public List<SpoilerDungeonEntrance> DungeonEntrances { get; set; }

        public Dictionary<string, List<SpoilerItem>> Items { get; set; }

        public Dictionary<GossipQuote, string> GossipHints { get; set; }

        public List<NameCostPair> Prices { get; set; }

        public ReadOnlyCollection<string> BlitzExtraItems { get; set; }
        public List<List<ItemLocationPair>> Playthrough { get; set; }
    }

    public class SpoilerDungeonEntrance
    {
        public string Entrance { get; set; }
        public string Destination { get; set; }
        public int EntranceId { get; }
        public int DestinationId { get; }

        public SpoilerDungeonEntrance(KeyValuePair<Item, Item> entrance)
        {
            Entrance = entrance.Key.Entrance();
            Destination = entrance.Value.Entrance();
            EntranceId = (int)entrance.Key;
            DestinationId = (int)entrance.Value;
        }
    }
}

