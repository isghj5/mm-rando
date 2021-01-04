using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  each enemy actor has varients that share so much of their code and data that they are bundled together has a varient
    ///  example: all the chuchus are one actor, white and black bo are varients of one actor
    /// </summary>

    public class ActorVariantsAttribute : Attribute
    {
        public List<int> Variants { get; set; }

        public ActorVariantsAttribute(int variant, params int[] additionalVarients)
        {
            var v = new List<int> { variant };
            if(additionalVarients.Length > 0)
            {
                v.AddRange(additionalVarients);
            }
            Variants = v;
        }
    }

    /// <summary>
    /// some enemies have variants that automaticallly respawn forever, 
    ///  these get in the way of puzzle rooms that require you clear all enemies
    /// </summary>

    public class UnkillableVariantsAttribute : ActorVariantsAttribute
    {
        public UnkillableVariantsAttribute(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

/// <summary>
/// some enemies can spawn in multiple situations, likelike can spawn on the beach or ocean bottom, so two types
/// I don't really want to add one of these times the different types, even though we need them, so inheritance
/// </summary>

// todo: consider splitting these up further, several enemies hang/perch from trees, 
//  some enemies spawn on the surface or bottom of water, and skulltullas dont really _fly_

    public class FlyingVariantsAttribute : ActorVariantsAttribute
    {
        public FlyingVariantsAttribute(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

    public class GroundVariantsAttribute : ActorVariantsAttribute
    {
        public GroundVariantsAttribute(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

    public class WaterVariantsAttribute : ActorVariantsAttribute
    {
        public WaterVariantsAttribute(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

    // we often want to place restrictions on how many of an enemy can possibly spawn

    public class SinglePerRoomMax : ActorVariantsAttribute
    {

        public SinglePerRoomMax(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

    public class DoublePerRoomMax : ActorVariantsAttribute
    {

        public DoublePerRoomMax(int variant, params int[] additionalVarients) : base(variant, additionalVarients) { }
    }

}
