using System;

namespace MMR.Randomizer.Attributes.Actor
{
    class FlyingToGroundHeightAdjustmentAttribute : Attribute
    {
        /// <summary>
        /// If a [Flying] Type actor is placed on a ground type spawn, adjust their spawn height in the air
        /// </summary>

        public int Height { get; private set; }

        public FlyingToGroundHeightAdjustmentAttribute(int height)
        {
            Height = height;
        }
    }
}
