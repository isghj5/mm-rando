using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    public class ItemPoolAttribute : Attribute
    {
        public ItemCategory ItemCategory { get; }
        public LocationCategory LocationCategory { get; }

        public ItemPoolAttribute(ItemCategory itemCategory, LocationCategory locationCategory)
        {
            ItemCategory = itemCategory;
            LocationCategory = locationCategory;
        }
    }
}
