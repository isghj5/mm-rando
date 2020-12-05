using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// this blocks the enemy from being randomized in a certain scene
    ///   example: we dont want to randomize the octorocks that are used for freezing with ice arrows in ikana
    /// </summary>

    class EnemizerScenesExcludedAttribute : Attribute
    {
       public List<int> ScenesExcluded { get; private set; }

        public EnemizerScenesExcludedAttribute(int scene, params int[] additionalScenes)
        {
            var scenes = new List<int> { scene };
            if (additionalScenes.Length > 0)
            {
                scenes.AddRange(additionalScenes);
            }
            ScenesExcluded = scenes;
        }

    }
}
