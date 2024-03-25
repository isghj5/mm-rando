using System;

namespace MMR.Randomizer.Attributes.Enemy
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class VariableAttribute : Attribute
    {
        public ushort Variable { get; }
        public VariableAttribute(ushort variable)
        {
            Variable = variable;
        }
    }
}
