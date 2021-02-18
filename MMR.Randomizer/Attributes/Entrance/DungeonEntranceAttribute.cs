using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class DungeonEntranceAttribute : Attribute
    {
        public DungeonEntrance Entrance { get; }
        public DungeonEntrance? Pair { get; }

        public DungeonEntranceAttribute(DungeonEntrance entrance)
        {
            Entrance = entrance;
        }

        public DungeonEntranceAttribute(DungeonEntrance entrance, DungeonEntrance pair)
        {
            Entrance = entrance;
            Pair = pair;
        }
    }
}
