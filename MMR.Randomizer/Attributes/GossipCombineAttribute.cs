using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class GossipCombineAttribute : Attribute
    {
        public Item OtherItem { get; }
        public string CombinedName { get; }

        public GossipCombineAttribute(Item otherItem, string combinedName = null)
        {
            OtherItem = otherItem;
            CombinedName = combinedName;
        }
    }
}
