using System;

namespace MMR.Randomizer.Attributes.Actor
{
    public class ActorInitVarOffsetAttribute : Attribute
    {
        /// <summary>
        ///  this is the actor init variable offset 
        ///    the location in the file after decompression where the actor init variables are
        /// </summary>

        public int Offset { get; set; }

        public ActorInitVarOffsetAttribute(int offset)
        {
            Offset = offset;
        }

    }
}
