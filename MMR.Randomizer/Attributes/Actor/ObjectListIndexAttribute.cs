using System;

namespace MMR.Randomizer.Attributes.Actor
{
    class ObjectListIndexAttribute : Attribute
    {
        /// <summary>
        ///  this is the object list index 
        ///    the game has one list for objects, this is where the actor lives in the list in vanilla
        /// </summary>

        public int Index { get; }

        public ObjectListIndexAttribute(int index)
        {
            Index = index;
        }

    }
}
