using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Entrance
{
    class ReservedFlag : Attribute
    {
        /// <summary>
        ///  Some scenes have flags we haven't found a clean way to auto detect yet
        ///  in this case: Snowhead temple center pillar and ice chunks is very complicated
        /// </summary>

        public List<int> Flags { get; private set; }

        public ReservedFlag(int flag, params int[] additionalFlags)
        {
            var flags = new List<int> { flag };
            if (additionalFlags.Length > 0)
            {
                flags.AddRange(additionalFlags);
            }
            Flags = flags;
        }

    }
}
