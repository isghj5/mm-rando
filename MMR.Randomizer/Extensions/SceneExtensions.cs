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
            var attr = scene.GetAttribute<FileIDAttribute>();
            if (attr != null) {
                return scene.GetAttribute<FileIDAttribute>().ID;
            } else
            {
                return -1; // empty slots have no fid
            }
        }

        public static bool IsClearEnemyPuzzleRoom(this Scene scene, int room)
        {
            var attr = scene.GetAttribute<ClearEnemyPuzzleRoomsAttribute>();
            return attr == null ? false : attr.PuzzleRooms.Contains(room);
        }

        public static bool IsFairyDroppingEnemy(this Scene scene, int roomNum, int actorNum)
        {
            var FairyDroppingEnemiesAttr = scene.GetAttributes<FairyDroppingEnemiesAttribute>();
            if (FairyDroppingEnemiesAttr != null)
            {
                foreach(var roomWithFairyEnemies in FairyDroppingEnemiesAttr)
                {
                    if (roomWithFairyEnemies.RoomNumber == roomNum)
                    {
                        return roomWithFairyEnemies.ActorNumbers.Contains(actorNum);
                    }
                }
            }

            return false;
        }

        public static List<(int roomNumber, List<int> actorNumbers)> GetSceneFairyDroppingEnemies(this Scene scene)
        {
            // returns a list of room:actorlist, where actors are their vanilla locations

            var fairyDroppingEnemiesAttr = scene.GetAttributes<FairyDroppingEnemiesAttribute>();
            if (fairyDroppingEnemiesAttr != null)
            {
                var listOfAllRoomsWithFairyActors = new List<(int roomNumber, List<int> actorNumber)>();
                foreach (var roomWithFairyEnemies in fairyDroppingEnemiesAttr)
                {
                    (int, List<int>) newRoom = (roomWithFairyEnemies.RoomNumber, roomWithFairyEnemies.ActorNumbers);
                    listOfAllRoomsWithFairyActors.Add(newRoom);
                }
                return listOfAllRoomsWithFairyActors;
            }
            return null;
        }

        public static int GetSceneObjLimit(this Scene scene)
        {
            // TODO make this a real attribute
            // cat says the lowest max heap size for any scene is 0x15900, aiming lower than that for spawned objects

            if (scene == Scene.GreatBayCoast)
                return 0x7F40; // crashs common at > 4.0x modifier, this is closer to 2.15 for safety

            if (scene == Scene.SnowheadTemple)
                return 0x20000; // crashing if reaching 25FFF, everything below 0x20000 was fine though

            return 0x12000; 
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
