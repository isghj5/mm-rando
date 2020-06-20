using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GossipCombineOrderAttribute : Attribute
    {
        public int Order { get; }

        public GossipCombineOrderAttribute(int order)
        {
            Order = order;
        }
    }
}
