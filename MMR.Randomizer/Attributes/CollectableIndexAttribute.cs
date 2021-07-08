using System;

namespace MMR.Randomizer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class CollectableIndexAttribute : Attribute
    {
        public ushort Index { get; }

        public CollectableIndexAttribute(ushort index)
        {
            Index = index;
        }
    }
}
