using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Attributes.Actor
{
    public class RemovalChance : Attribute
    {
        /// <summary>
        ///  Chance of an actor being replaced and not being left vanilla
        /// </summary>
        /// <param name="weight"></param>

        public int Weight;

        public RemovalChance(int weight)
        {
            this.Weight = weight;
        }
    }

    public class RemovalChancePerVariant : RemovalChance
    {
        /// <summary>
        ///  Chance of an actor being replaced and not being left vanilla (with variant specificity)
        /// </summary>
        public List<int> Variants { get; private set; }

        public RemovalChancePerVariant(int weight, int variant, params int[] additionalVariants) : base(weight)
        {
            var v = new List<int> { variant };
            if (additionalVariants.Length > 0)
            {
                v.AddRange(additionalVariants);
            }
            Variants = v;
        }
    }

    // TODO consider adding one with scene specificity
}
