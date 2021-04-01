using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class ItemCategoryAttribute : Attribute
    {
        public ItemCategory ItemCategory { get; }

        public ItemCategoryAttribute(ItemCategory itemCategory)
        {
            ItemCategory = itemCategory;
        }
    }
}
