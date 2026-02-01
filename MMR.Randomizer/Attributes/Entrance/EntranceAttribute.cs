using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class EntranceAttribute : Attribute
    {
        public GameObjects.Entrance Entrance { get; }
        public GameObjects.Entrance? Pair { get; }

        public EntranceAttribute(GameObjects.Entrance entrance)
        {
            Entrance = entrance;
        }

        public EntranceAttribute(GameObjects.Entrance entrance, GameObjects.Entrance pair)
        {
            Entrance = entrance;
            Pair = pair;
        }
    }
}
