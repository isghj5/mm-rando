using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ExitCutsceneAttribute : Attribute
    {
        public int SceneId { get; private set; }
        public byte SetupIndex { get; private set; }
        public byte CutsceneIndex { get; private set; }

        public ExitCutsceneAttribute(Scene scene, byte setupIndex, byte cutsceneIndex)
        {
            SceneId = scene.Id();
            SetupIndex = setupIndex;
            CutsceneIndex = cutsceneIndex;
        }
    }
}
