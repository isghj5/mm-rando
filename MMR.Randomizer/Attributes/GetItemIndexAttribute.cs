using System;

namespace MMR.Randomizer.Attributes
{
    public class GetItemIndexAttribute : Attribute
    {
        public ushort Index { get; }

        public GetItemIndexAttribute(ushort index)
        {
            Index = index;
        }
    }
}
