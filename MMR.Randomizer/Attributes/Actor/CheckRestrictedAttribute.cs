using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    ///  Stores a check that is accessed by this actor, removing this actor would break this check.
    /// </summary>

    class CheckRestrictedAttribute : Attribute
    {
        public List<GameObjects.Item> Checks { get; private set; }

        public CheckRestrictedAttribute(GameObjects.Item check, params GameObjects.Item[] additionalChecks)
        {
            var c = new List<GameObjects.Item> { check };
            if (additionalChecks.Length > 0)
            {
                c.AddRange(additionalChecks);
            }
            Checks = c;
        }
    }
}
