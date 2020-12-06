using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  each enemy actor has varients that share so much of their code and data that they are bundled together has a varient
    ///  example: all the chuchus are one actor, white and black bo are varients of one actor
    /// </summary>

    class ActorVariantsAttribute : Attribute
    {
        public List<int> Variants { get; private set; }

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
}
