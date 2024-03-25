using System;

namespace MMR.Randomizer.Attributes.Enemy
{
    public class ActorIdAttribute : Attribute
    {
        public ushort Id { get; }
        public ActorIdAttribute(ushort id)
        {
            Id = id;
        }
    }
}
