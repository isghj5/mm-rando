using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// this blocks the enemy from being randomized in a certain scene
    ///   example: we dont want to randomize the octorocks that are used for freezing with ice arrows in ikana
    /// </summary>

    class ForbidFromSceneAttribute : Attribute
    {
       public List<GameObjects.Scene> ScenesExcluded { get; private set; }

        public ForbidFromSceneAttribute(GameObjects.Scene scene, params GameObjects.Scene[] additionalScenes)
        {
            var scenes = new List<GameObjects.Scene> { scene };
            if (additionalScenes.Length > 0)
            {
                scenes.AddRange(additionalScenes);
            }
            ScenesExcluded = scenes;
        }

    }
}
