using System;

namespace MMR.Randomizer.Attributes.Actor
{
    public class ActorInitVarOffsetAttribute : Attribute
    {
        /// <summary>
        ///  this is the actor init variable offset 
        ///    the location in the file after decompression where the actor init variables are
        ///    we save it because I haven't found a way to look it up, and detecting it from actor+padding+object is hit or miss
        /// </summary>

        public int Offset { get; set; }

        public ActorInitVarOffsetAttribute(int offset)
        {
            Offset = offset;
        }

    }
}
