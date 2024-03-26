using System;

namespace MMR.Randomizer.Attributes.Enemy
{
    public class ObjectIdAttribute : Attribute
    {
        public ushort Id { get; }
        public ObjectIdAttribute(ushort id)
        {
            Id = id;
        }
    }
}
