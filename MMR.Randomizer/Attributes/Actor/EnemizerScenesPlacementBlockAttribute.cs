using MMR.Randomizer.Extensions;
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

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class EnemizerSceneEnemyReplacementBlockAttribute : Attribute
    {
        public GameObjects.Actor OriginalEnemy { get; private set; }
        public List<GameObjects.Actor> BlockedReplacements { get; private set; }

        public EnemizerSceneEnemyReplacementBlockAttribute(GameObjects.Actor originalEnemy, GameObjects.Actor blockedReplacement, params GameObjects.Actor[] blockedReplacements)
        {
            OriginalEnemy = originalEnemy;
            var blockedEnemies = new List<GameObjects.Actor>() { blockedReplacement };
            if (blockedReplacements.Length > 0)
            {
                blockedEnemies.AddRange(blockedReplacements);
            }
            BlockedReplacements = blockedEnemies;
        }
    }
}
