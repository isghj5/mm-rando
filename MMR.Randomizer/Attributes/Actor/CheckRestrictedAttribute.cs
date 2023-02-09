using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  Stores a check that is accessed by this actor, removing this actor would break this check.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class CheckRestrictedAttribute : Attribute
    {
        public List<GameObjects.Item> Checks { get; private set; }
        public int Variant = GameObjects.ActorConst.ANY_VARIANT;
        public GameObjects.Scene Scene = (GameObjects.Scene) (GameObjects.ActorConst.ANY_SCENE); // fake value


        // regular version, checks only for all variants and all scenes
        public CheckRestrictedAttribute(GameObjects.Item check, params GameObjects.Item[] additionalChecks)
        {
            var c = new List<GameObjects.Item> { check };
            if (additionalChecks.Length > 0)
            {
                c.AddRange(additionalChecks);
            }
            Checks = c;
        }

        // Sometimes we want to restrict to scenes or to a specific variant
        public CheckRestrictedAttribute(GameObjects.Scene scene, int variant,
            GameObjects.Item check, params GameObjects.Item[] additionalChecks)
        {
            var c = new List<GameObjects.Item> { check };
            if (additionalChecks.Length > 0)
            {
                c.AddRange(additionalChecks);
            }
            Checks = c;
            this.Scene = scene;
            this.Variant = variant;
        }

    }
}
