using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// if not enabled, disables the enemy from being randomized.
    /// </summary>
    class ActorChangeEnableAttribute : Attribute
    {
        public bool Enabled = false;

        public ActorChangeEnableAttribute() 
        {
            Enabled = true;
        }
    }

    /// <summary>
    ///  if not enabled, disables actors from being randomized.
    /// </summary>
    class ActorizerEnabledAttribute : ActorChangeEnableAttribute
    {
        public ActorizerEnabledAttribute() : base() { }
    };
    class EnemizerEnabledAttribute : ActorChangeEnableAttribute
    {
        public EnemizerEnabledAttribute() : base() { }
    };
}
