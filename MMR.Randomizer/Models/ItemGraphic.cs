using System;

namespace MMR.Randomizer.Models
{
    public struct ItemGraphic : IEquatable<ItemGraphic>
    {
        /// <summary>
        /// Object Id used for drawing.
        /// </summary>
        public ushort ObjectId { get; }

        /// <summary>
        /// Graphic index used for drawing.
        /// </summary>
        public byte GraphicId { get; }

        public ItemGraphic(ushort objectId, byte graphicId)
        {
            this.GraphicId = graphicId;
            this.ObjectId = objectId;
        }

        public override int GetHashCode()
        {
            return (((int)GraphicId << 16) | (int)ObjectId);
        }

        public override bool Equals(object obj)
        {
            return obj is ItemGraphic && Equals((ItemGraphic)obj);
        }

        public bool Equals(ItemGraphic g)
        {
            return this == g;
        }

        public static bool operator ==(ItemGraphic lhs, ItemGraphic rhs)
        {
            return lhs.GraphicId == rhs.GraphicId && lhs.ObjectId == rhs.ObjectId;
        }

        public static bool operator !=(ItemGraphic lhs, ItemGraphic rhs)
        {
            return !(lhs == rhs);
        }
    }
}
