using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StartingItemAttribute : Attribute
    {
        public int Address { get; }
        public byte Value { get; }
        public bool IsAdditional { get; }

        public StartingItemAttribute(int address, byte value, bool isAdditional = false)
        {
            Address = address;
            Value = value;
            IsAdditional = isAdditional;
        }
    }
}
