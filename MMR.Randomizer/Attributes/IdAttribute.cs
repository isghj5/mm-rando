using System;

namespace MMR.Randomizer.Attributes
{
    public class IdAttribute : Attribute
    {
        public byte Id { get; }

        public IdAttribute(byte id)
        {
            Id = id;
        }
    }
}
