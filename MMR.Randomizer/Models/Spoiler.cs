using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MMR.Randomizer.Models
{
    public class Spoiler
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Version { get; set; }

        public string SettingsString { get; set; }

        public List<SpoilerDungeonEntrance> DungeonEntrances { get; set; }

        public int Seed { get; set; }

        public List<SpoilerItem> ItemList { get; set; }

        public List<ItemLogic> Logic { get; set; }

        public Dictionary<GossipQuote, string> GossipHints { get; set; }

        public string LogicJson
        {
            get
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ItemLogic>));
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, Logic);
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public List<NameCostPair> MessageCosts { get; set; }

        public ReadOnlyCollection<Item> BlitzExtraItems { get; set; }
        public List<List<ItemLocationPair>> Spheres { get; set; }
    }
}

