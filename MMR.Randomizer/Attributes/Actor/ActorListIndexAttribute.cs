using System;

namespace MMR.Randomizer.Attributes.Actor
{
    class ActorListIndexAttribute : Attribute
    {
        /// <summary>
        ///  this is the actor list index 
        ///    the game has one list for actors, this is where the actor lives in the list in vanilla
        /// </summary>

        public int Index { get; }

        public ActorListIndexAttribute(int index)
        {
            Index = index;
        }

    }
}
