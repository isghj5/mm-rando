using System;

namespace MMR.Randomizer.Attributes.Enemy
{
    public class ActorTypeAttribute : Attribute
    {
        public ActorType Type { get; }
        public ActorTypeAttribute(ActorType actorType)
        {
            Type = actorType;
        }

        public enum ActorType
        {
            Ground,
            Water,
            Air,
            Other,
            Respawn,
        }
    }
}
