using System;

namespace MMR.Randomizer.Attributes
{
    public class ValueAttribute : Attribute
    {
        public object Value { get; }

        public ValueAttribute(object value)
        {
            Value = value;
        }
    }
}
