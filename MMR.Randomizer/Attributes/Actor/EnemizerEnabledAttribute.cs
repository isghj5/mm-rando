using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// if not enabled, disables the enemy from being randomized. hopefully in the future only used for debug
    /// </summary>
    class EnemizerEnabledAttribute : Attribute
    {
        public bool Enabled = false;

        public EnemizerEnabledAttribute() 
        {
            Enabled = true;
        }
    }
}
