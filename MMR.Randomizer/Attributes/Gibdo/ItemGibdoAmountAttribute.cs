using System;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemGibdoAmountAttribute : Attribute
    {
        public byte DefaultAmount { get; }
        public byte Min { get; }
        public byte Max { get; }

        public ItemGibdoAmountAttribute(byte defaultAmount, byte min, byte max)
        {
            DefaultAmount = defaultAmount;
            Min = min;
            Max = max;
        }
    }
}
