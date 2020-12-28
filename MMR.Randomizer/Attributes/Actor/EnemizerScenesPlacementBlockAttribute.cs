using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes
{
    /// <summary>
    /// this blocks the enemy from being randomized into certain scenes
    ///   example: for some reason dinofos if placed on iron knuckle in graves crashes the game
    /// </summary>

    class EnemizerScenesPlacementBlock : Attribute
    {
        public List<GameObjects.Scene> ScenesBlocked { get; private set; }

        public EnemizerScenesPlacementBlock(GameObjects.Scene scene, params GameObjects.Scene[] additionalScenes)
        {
            var scenes = new List<GameObjects.Scene> { scene };
            if (additionalScenes.Length > 0)
            {
                scenes.AddRange(additionalScenes);
            }
            ScenesBlocked = scenes;
        }

    }
}
