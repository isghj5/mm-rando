using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ExitActorParamsAttribute : Attribute
    {
        public byte SceneId { get; }
        public byte SetupIndex { get; }
        public byte RoomIndex { get; }
        public byte ActorIndex { get; }

        public ExitActorParamsAttribute(Scene scene, byte setupIndex, byte roomIndex, byte actorIndex)
        {
            SceneId = scene.Id();
            SetupIndex = setupIndex;
            RoomIndex = roomIndex;
            ActorIndex = actorIndex;
        }
    }
}
