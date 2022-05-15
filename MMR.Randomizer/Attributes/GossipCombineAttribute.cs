using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GossipCombineAttribute : Attribute
    {
        public IEnumerable<Item> OtherItems { get; }
        public string CombinedName { get; }

        public GossipCombineAttribute(string combinedName, Item otherItem, params Item[] additionalItems)
        {
            OtherItems = additionalItems.Append(otherItem);
            CombinedName = combinedName;
        }
    }
}
