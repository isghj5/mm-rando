using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// the weight of the chance of an actor being placed in a scene using one object
    /// </summary>

    public class PlacementWeight : Attribute
    {
        public int Weight;

        public PlacementWeight(int weight)
        {
            this.Weight = weight;
        }

    }

}
