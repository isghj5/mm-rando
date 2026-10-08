using System;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  This actor guards the given entrance. If the actor is removed, everything behind
    ///  the entrance becomes unreachable. Actorizer must keep the actor whenever a non-junk
    ///  item is currently placed behind the entrance.
    ///  The entrance's destination moves under entrance randomization, so this is resolved
    ///  at runtime against the live item list (see JunkDetection.FirstNonJunkItemInEntrance).
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class GatesEntranceAttribute : Attribute
    {
        public GameObjects.Item Entrance { get; }

        public GatesEntranceAttribute(GameObjects.Item entrance)
        {
            Entrance = entrance;
        }
    }
}
