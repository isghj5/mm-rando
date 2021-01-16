using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.GameObjects;
using System.CodeDom;
using System.Collections.Generic;

namespace MMR.Randomizer.Extensions
{
    public static class SceneExtensions
    {
        public static byte Id(this Scene scene)
        {
            return scene.GetAttribute<SceneInternalIdAttribute>().InternalId;
        }
        public static int FileID(this Scene scene)
        {
            return scene.GetAttribute<FileIDAttribute>().ID;
        }

        public static bool IsClearEnemyPuzzleRoom(this Scene scene, int room)
        {
            var attr = scene.GetAttribute<ClearEnemyPuzzleRoomsAttribute>();
            return attr == null ? false : attr.PuzzleRooms.Contains(room);
        }

        public static int GetSceneObjLimit(this Scene scene)
        {
            // TODO make this a real attribute
            // cat says the lowest max heap size for any scene is 0x15900, aiming lower than that for spawned objects

            if (scene == Scene.GreatBayCoast)
                return 0x7F40; // crashs common at > 4.0x modifier, this is closer to 2.15 for safety

            return 0x12000; 
        }

        public static List<Actor> GetSceneFairyDroppingEnemies(this Scene scene)
        {
            var attr = scene.GetAttribute<FairyDroppingEnemiesAttribute>();
            return attr == null ? new List<Actor>() : attr.Enemies;
        }

        public static bool IsDungeon(this Scene scene)
        {
            /// we need to know if object 3 gameplay_dangeon_keep is loaded, to use actors that require this actor

            // this is just a guess for now, the penalty of loading an actor that needs obj3 and doesn not have it is... an empty enemy not a big bug
            // TODO flesh out this list
            var listOfDungeons = new List<Scene>{ Scene.WoodfallTemple, Scene.SnowheadTemple, Scene.GreatBayTemple, Scene.StoneTowerTemple, Scene.InvertedStoneTowerTemple };
            return listOfDungeons.Contains(scene);
        }

        public static List<Actor> GetBlockedReplacementActors(this Scene scene, Actor replacedActor)
        {
            /// sometimes we want to stop actors in scene from being randomized to specific actors,
            ///  but we dont want to block from the whole scene, just one actor replacement
            ///  where OriginalEnemy is the vanilla enemy we need to replace
            ///  and BlockedReplacements is the list of enemies we cannot replace OriginalEnemy with 
            var enemyReplacementBlockedCombosAttr = scene.GetAttributes<EnemizerSceneEnemyReplacementBlockAttribute>();
            if (enemyReplacementBlockedCombosAttr != null)
            {
                foreach( var replacementEnemyBlockedAttr in enemyReplacementBlockedCombosAttr)
                {
                    if (replacementEnemyBlockedAttr != null && replacementEnemyBlockedAttr.OriginalEnemy == replacedActor)
                    {
                        return replacementEnemyBlockedAttr.BlockedReplacements;
                    }
                }
            }

            return new List<Actor>(); // no blocked enemy combos found, return empty list
        }
    }

}
