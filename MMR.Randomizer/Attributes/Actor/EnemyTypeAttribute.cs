using System;

namespace MMR.Randomizer.Attributes.Actor
{
    /// <summary>
    /// Where does the enemy spawn, if you place some ground enemies in the sky or water they behave weird
    /// example: bombchus just explode instantly if they aren't on the ground, and chuchus sink like water doesnt exist
    /// </summary>
 
    public enum ActorType
    {
        Ground,
        Water,
        Flying
    }

    class EnemyTypeAttribute : Attribute
    {
        public ActorType Type;

        public EnemyTypeAttribute(ActorType type)
        {
            Type = type;
        }

    }
}
