using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GossipStoneAttribute : Attribute
    {
        public Item Item { get; }

        public GossipStoneAttribute(Item item)
        {
            Item = item;
        }
    }
}
