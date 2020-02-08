using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ExitAttribute : Attribute
    {
        public int SceneId { get; private set; }
        public byte ExitIndex { get; private set; }

        public ExitAttribute(Scene scene, byte exitIndex)
        {
            SceneId = scene.Id();
            ExitIndex = exitIndex;
        }
    }
}
