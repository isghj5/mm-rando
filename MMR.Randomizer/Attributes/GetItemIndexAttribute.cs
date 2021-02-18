using System;

namespace MMR.Randomizer.Attributes
{
    public class GetItemIndexAttribute : Attribute
    {
        public ushort Index { get; private set; }

        public GetItemIndexAttribute(ushort index)
        {
            Index = index;
        }
    }
}
