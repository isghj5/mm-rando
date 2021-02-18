using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StartingItemIdAttribute : Attribute
    {
        public byte ItemId { get; }

        public StartingItemIdAttribute(byte itemId)
        {
            ItemId = itemId;
        }
    }
}
