using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Entrance
{
    class FairyDroppingEnemiesAttribute : Attribute
    {
        /// <summary>
        /// scene attribute: enemies in the scene that drop stray fairies when they die
        /// </summary>

        public List<GameObjects.Actor> Enemies { get; set; }

        public FairyDroppingEnemiesAttribute(GameObjects.Actor enemy, params GameObjects.Actor[] additionalEnemies)
        {
            var e = new List<GameObjects.Actor> { enemy };
            if (additionalEnemies.Length > 0)
            {
                e.AddRange(additionalEnemies);
            }
            Enemies = e;
        }

    }
}
