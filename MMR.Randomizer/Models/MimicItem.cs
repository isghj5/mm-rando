using MMR.Randomizer.Attributes;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Models
{
    public class MimicItem : IEquatable<MimicItem>
    {
        /// <summary>
        /// Underlying item used as mimic.
        /// </summary>
        public Item Item { get; }

        /// <summary>
        /// Fake shop name, if any.
        /// </summary>
        public string FakeName { get; set; }

        /// <summary>
        /// Chest type override.
        /// </summary>
        public ChestTypeAttribute.ChestType? ChestType { get; set; }

        public MimicItem(Item item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Resolve <see cref="ItemGraphic"/> from <see cref="RomData"/> get-item list using index.
        /// </summary>
        /// <returns>Resolved item graphic.</returns>
        public ItemGraphic ResolveGraphic()
        {
            var index = this.Item.GetItemIndex().Value;
            var getItem = RomData.GetItemList[index];
            return new ItemGraphic(getItem.Index, (ushort)getItem.Object);
        }

        public override int GetHashCode()
        {
            return Item.Name().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is MimicItem && Equals((MimicItem)obj);
        }

        public bool Equals(MimicItem other)
        {
            return Item.Name().Equals(other.Item.Name());
        }
    }
}
