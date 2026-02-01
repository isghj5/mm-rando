using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ItemGibdoLogicRequirementsAttribute : Attribute
    {
        public ReadOnlyCollection<GameObjects.Item> Items { get; }
        public int MinAmount { get; }
        public int MaxAmount { get; }

        public ItemGibdoLogicRequirementsAttribute(params GameObjects.Item[] items)
        {
            Items = items.ToList().AsReadOnly();
        }

        public ItemGibdoLogicRequirementsAttribute(int minAmount, int maxAmount, params GameObjects.Item[] items)
        {
            Items = items.ToList().AsReadOnly();
            MinAmount = minAmount;
            MaxAmount = maxAmount;
        }
    }
}
