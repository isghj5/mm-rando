using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ItemGibdoLogicConditionalAttribute : Attribute
    {
        public ReadOnlyCollection<GameObjects.Item> Items { get; }
        public int MinAmount { get; }
        public int MaxAmount { get; }

        public ItemGibdoLogicConditionalAttribute(params GameObjects.Item[] items)
        {
            Items = items.ToList().AsReadOnly();
        }

        public ItemGibdoLogicConditionalAttribute(int minAmount, int maxAmount, params GameObjects.Item[] items)
        {
            Items = items.ToList().AsReadOnly();
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }
    }
}
