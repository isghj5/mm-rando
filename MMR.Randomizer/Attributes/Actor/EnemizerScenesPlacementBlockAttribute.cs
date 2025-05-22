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

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class EnemizerSceneBlockSensitiveAttribute : Attribute
    {
        public GameObjects.Actor OriginalEnemy { get; private set; }
        public List<int> SpecificMapIndexes { get; private set; }
        public List<GameObjects.Item> BlockedChecks { get; private set; }

        public EnemizerSceneBlockSensitiveAttribute(GameObjects.Actor originalEnemy)
        {
            OriginalEnemy = originalEnemy;
            SpecificMapIndexes = new List<int>() { -1 };
        }

        public EnemizerSceneBlockSensitiveAttribute(GameObjects.Actor originalEnemy, int blockedIndex, params int[] blockedIndexes)
        {
            OriginalEnemy = originalEnemy;
            var blockedEnemies = new List<int>() { blockedIndex };
            if (blockedIndexes.Length > 0)
            {
                blockedEnemies.AddRange(blockedIndexes);
            }
            SpecificMapIndexes = blockedEnemies;
        }

        public EnemizerSceneBlockSensitiveAttribute(GameObjects.Actor originalEnemy, GameObjects.Item check, params GameObjects.Item[] blockedChecks)
        {
            OriginalEnemy = originalEnemy;
            var checks = new List<GameObjects.Item>() { check };
            if (blockedChecks.Length > 0)
            {
                checks.AddRange(blockedChecks);
            }
            BlockedChecks = checks;
            SpecificMapIndexes = new List<int>() { -1 };
        }

    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class ActorizerSceneCreditsActor : Attribute
    {
        public List<GameObjects.Actor> CreditsActors;
        public int Room = -1;

        public ActorizerSceneCreditsActor(GameObjects.Actor creditsActor,  params GameObjects.Actor[] additionalCreditsActors)
        {
            var actors = new List<GameObjects.Actor> { creditsActor };
            if (additionalCreditsActors.Length > 0)
            {
                actors.AddRange(additionalCreditsActors);
            }
            CreditsActors = actors;
        }

        public ActorizerSceneCreditsActor(int room, GameObjects.Actor creditsActor, params GameObjects.Actor[] additionalCreditsActors)
            : this(creditsActor, additionalCreditsActors) 
        {
            this.Room = room;
        }

    }


}
