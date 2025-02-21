using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  each enemy actor has variants that share so much of their code and data that they are bundled together as an actor
    ///  example: all the chuchu variants are one actor, white and black bo are variants of one actor
    ///  we need ways to specify behavior not just to an actor but to their variants, which are sometimes unique
    /// </summary>

    public class ActorVariantsAttribute : Attribute
    {
        public List<int> Variants { get; private set; }

        public ActorVariantsAttribute(int variant, params int[] additionalVariants)
        {
            var v = new List<int> { variant };
            if(additionalVariants.Length > 0)
            {
                v.AddRange(additionalVariants);
            }
            Variants = v;
        }
    }

    /// <summary>
    /// Some Enemies are not killable
    ///  they present an issue if they block paths or have stray fairies in their bodies
    /// </summary>

    public class UnkillableVariantsAttribute : ActorVariantsAttribute
    {
        public UnkillableVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class UnkillableAllVariantsAttribute : Attribute { }

    /// <summary>
    /// Some enemies have variants that automaticallly respawn forever
    ///  these get in the way of puzzle rooms that require you clear all enemies
    ///  also, any enemy that appears to die, but prevents a clear room gets this flag
    /// </summary>

    public class RespawningVariantsAttribute : ActorVariantsAttribute
    {
        public RespawningVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class RespawningAllVariantsAttribute : Attribute { }

    /// <summary>
    /// Some enemies, when put into old mini-boss areas are so easy to fight the fight is a joke
    ///   this will hopefully combat this a bit by restricting how many easy enemies can be put places
    /// </summary>

    public class DifficultVariantsAttribute : ActorVariantsAttribute
    {
        public DifficultVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class DifficultAllVariantsAttribute : Attribute { }


    /// <summary>
    /// some enemies can spawn in multiple situations, likelike can spawn on the beach or ocean bottom, so two types
    /// I don't really want to add one of these times the different types, even though we need them, so inheritance
    /// </summary>

    public class FlyingVariantsAttribute : ActorVariantsAttribute
    {
        public FlyingVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class GroundVariantsAttribute : ActorVariantsAttribute
    {
        public GroundVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class WaterVariantsAttribute : ActorVariantsAttribute
    {
        public WaterVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class WaterTopVariantsAttribute : ActorVariantsAttribute
    {
        public WaterTopVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class WaterBottomVariantsAttribute : ActorVariantsAttribute
    {
        public WaterBottomVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class WallVariantsAttribute : ActorVariantsAttribute
    {
        public WallVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class PerchingVariantsAttribute : ActorVariantsAttribute
    {
        public PerchingVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class CeilingVariantsAttribute : ActorVariantsAttribute
    {
        public CeilingVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class PathingVariantsAttribute : ActorVariantsAttribute
    {
        public PathingVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    // blocking
    public class BlockingVariantsAttribute : ActorVariantsAttribute
    {
        public BlockingVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    // we often want to place restrictions on how many of an actor can possibly spawn

    public class OnlyOneActorPerRoom : Attribute
    {
        public bool OneMAX = true;
    }

    public class BlockingVariantsAll : Attribute
    {
        public bool AllBlock = true;
    }

    public class RequiresCompanionAttribute: Attribute
    {
        public RequiresCompanionAttribute(GameObjects.Actor companion, int number, int variant, params int[] additionalVariants) { }
    }

    public class CreditsBlockedVariantsAttribute : ActorVariantsAttribute
    {
        public CreditsBlockedVariantsAttribute(int variant, params int[] additionalVariants) : base(variant, additionalVariants) { }
    }

    public class CreditsBlockedAllVariantsAttribute : Attribute
    {
        public bool AllBlock = true;
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class VariantsWithRoomMax : Attribute
    {
        public int RoomMax { get; private set; }
        public List<int> Variants { get; private set; }

        public VariantsWithRoomMax(int max, int variant, params int[] additionalVariants) 
        {
            var v = new List<int> { variant };
            if (additionalVariants.Length > 0)
            {
                v.AddRange(additionalVariants);
            }
            Variants = v;
            RoomMax = max;
        }
    }

}
