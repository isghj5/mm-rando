using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// some enemies have varities that can respawn, and some "kill all enemies to continue" rooms get confused by respawning enemies
    /// </summary>

    class RespawningVarientsAttribute : Attribute
    {
        public List<int> Variants { get; private set; }

        public RespawningVarientsAttribute(int varient, params int[] additionalVarients)
        {
            var v = new List<int> { varient };
            if (additionalVarients.Length > 0)
            {
                v.AddRange(additionalVarients);
            }
            Variants = v;
        }
    }
}
