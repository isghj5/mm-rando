using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.GameObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMR.Randomizer.Utils;

namespace MMR.Randomizer.Models
{
    public class RandomizedResult
    {
        public GameplaySettings Settings { get; private set; }
        public int Seed { get; set; }

        public ItemList ItemList { get; set; }
        public List<MessageEntry> GossipQuotes { get; set; }
        public List<ItemLogic> Logic { get; set; }
        public ReadOnlyCollection<Item> ImportantLocations { get; set; }
        public ReadOnlyCollection<Item> RequiredSongLocations { get; set; }
        public ReadOnlyCollection<Item> LocationsRequiredForMoonAccess { get; set; }
        public Dictionary<Item, LogicUtils.LogicPaths> CheckedImportanceLocations { get; set; }
        public ReadOnlyCollection<ItemObject> Traps { get; set; }
        public List<ushort?> MessageCosts { get; set; }
        public List<Item> BlitzExtraItems { get; set; }
        public int FileSelectSkybox { get; internal set; }
        public int FileSelectColor { get; internal set; }
        public int TitleLogoColor { get; internal set; }
        public List<List<ItemLocationPair>> Spheres { get; internal set; }

        public RandomizedResult(GameplaySettings settings, int seed)
        {
            Settings = settings;
            Seed = seed;
        }

    }
}
