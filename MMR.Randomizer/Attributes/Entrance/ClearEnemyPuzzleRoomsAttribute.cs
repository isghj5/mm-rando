using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Entrance
{
    /// <summary>
    /// some maps/rooms have puzzles where you must kill all enemies
    ///   maybe we could detect these dynamically if we knew the flags on chests and the composer curtain
    ///   for now, hard code them, since we dont have non-vanilla scenes yet anyway
    /// </summary>
    class ClearEnemyPuzzleRoomsAttribute : Attribute
    {
        public List<int> PuzzleRooms { get; private set; }

        public ClearEnemyPuzzleRoomsAttribute(int room, params int[] additionalRooms)
        {
            var scenes = new List<int> { room };
            if (additionalRooms.Length > 0)
            {
                scenes.AddRange(additionalRooms);
            }
            PuzzleRooms = scenes;
        }
    }
}
