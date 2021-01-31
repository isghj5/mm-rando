using MMR.Randomizer.Models.Vectors;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// Some actor combinations make sense together
    ///  example: fire and red bubbles
    ///  for now this is limited to free actors
    /// </summary>
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class CompanionActorAttribute : Attribute
    {
        public static GameObjects.Actor Companion { get; private set; }
        public static List<int> Variants { get; private set; }

        public CompanionActorAttribute(GameObjects.Actor companion, int variant, params int[] additionalVariants)
        {
            Companion = companion;
            var v = new List<int> { variant };
            if (additionalVariants.Length > 0)
            {
                v.AddRange(additionalVariants);
            }
            Variants = v;
        }
    }

    /// <summary>
    /// Some actors don't make sense by themselves, they need a second actor for their existence in the world to not be weird
    ///  example: sleeping scrub from swamp spiderhouse does not spawn with his flower, he needs a flower or he is just floating in the air
    /// </summary>

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class ForcedCompanionActorAttribute : CompanionActorAttribute
    {
        public ForcedCompanionActorAttribute(GameObjects.Actor companion, int variant, params int[] additionalVariants) : base(companion, variant, additionalVariants) { }
    }

    /// <summary>
    /// Some Companions make the most sense when aligned compared to one their companion
    ///  example: fire in front of skullchild
    ///  example2: grotto hidden under bombiwa
    /// </summary>


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class AlignedCompanionActorAttribute: CompanionActorAttribute
    {
        public static vec16 RelativePosition { get; private set; }

        public AlignedCompanionActorAttribute(GameObjects.Actor companion, vec16 relativePosition, int variant, params int[] additionalVariants):
              base(companion, variant, additionalVariants)
        {
            RelativePosition = relativePosition;
        }

    }
}
