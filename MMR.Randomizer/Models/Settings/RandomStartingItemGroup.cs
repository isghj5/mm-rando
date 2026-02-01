using MMR.Randomizer.Attributes.Setting;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;
using System.Collections.Generic;

namespace MMR.Randomizer.Models.Settings
{
    public class RandomStartingItemGroup
    {
        [SettingItemList(nameof(ItemUtils.CustomStartingItems), SettingItemListAttribute.LabelType.Name, SettingItemListAttribute.LabelType.None, nameof(ItemExtensions.ItemCategory))]
        public List<Item> Items { get; set; }

        public int Amount { get; set; }
    }
}
