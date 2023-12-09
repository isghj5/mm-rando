﻿using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;

namespace MMR.Randomizer.Models
{
    public class SpoilerItem
    {
        public int Id { get; }
        public string Name { get; }

        public int NewLocationId { get; }
        public string NewLocationName { get; }

        public Region Region { get; }

        public bool IsJunk { get; }

        public bool IsLocationJunked { get; }

        public bool IsImportant { get; }

        public bool IsRequired { get; }

        public bool IsImportantSong { get; }

        public ItemCategory ItemCategory { get; set; }

        public SpoilerItem(ItemObject itemObject, Region region, bool isRequired, bool isImportant, bool isLocationJunked, bool isImportantSong, bool progressiveUpgrades, ItemList itemList)
        {
            Id = itemObject.ID;
            Name = itemObject.NameOverride ?? itemObject.Item.ProgressiveUpgradeName(progressiveUpgrades) ?? itemObject.Name;
            NewLocationId = (int)itemObject.NewLocation.Value;
            NewLocationName = itemObject.NewLocation.Value.Location(itemList);
            Region = region;
            IsJunk = ItemUtils.IsJunk(itemObject.Item);
            IsLocationJunked = isLocationJunked;
            IsImportant = isImportant;
            IsRequired = isRequired;
            IsImportantSong = isImportantSong;
            ItemCategory = itemObject.Item.ItemCategory() ?? ItemCategory.None;
        }
    }
}
