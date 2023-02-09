using System;

namespace MMR.Randomizer.Attributes.Actor
{
    class ActorInstanceSizeAttribute : Attribute
    {

        /// <summary>
        ///  Every actor instance takes up a block of ram containing all of its state, actor and additional 
        ///    we want to know how big it is for enemizer ram considerations,
        ///    we want to save it in the actor enum because its burried in the actor code, which means another file to compress later slowing us down
        /// </summary>

        public int Size { get; private set; }

        public ActorInstanceSizeAttribute(int size)
        {
            Size = size;
        }
    }
}
