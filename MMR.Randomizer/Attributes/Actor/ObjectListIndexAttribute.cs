using System;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes.Actor
{
    class ObjectListIndexAttribute : Attribute
    {
        /// <summary>
        ///  this is the object list index 
        ///    the game has one list for objects, this is where the actor lives in the list in vanilla
        /// </summary>

        public int Index => (int)ObjectValue;
        public GameObjects.Object ObjectValue { get; }

        public ObjectListIndexAttribute(GameObjects.Object obj)
        {
            ObjectValue = obj;
        }

        public ObjectListIndexAttribute(int objInt)
        {
            ObjectValue = (GameObjects.Object) objInt;
        }

    }
}
