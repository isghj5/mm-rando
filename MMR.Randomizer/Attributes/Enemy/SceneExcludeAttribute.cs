using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Enemy
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class SceneExcludeAttribute : Attribute
    {
        public Scene Scene { get; }
        public SceneExcludeAttribute(Scene scene)
        {
            Scene = scene;
        }
    }
}
