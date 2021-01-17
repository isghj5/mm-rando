using System;
using System.Collections.Generic;
using System.Text;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    class FairyDroppingEnemiesAttribute : Attribute
    {
        /// <summary>
        /// scene attribute: the room in the scene, and the vanilla actor numbers for actors that drop fairies
        ///   eg, [room 1] of woodfall has one dekubaba that drops a fairy, which is [number 4]
        /// </summary>

        public int RoomNumber { get; private set;}
        public List<int> ActorNumbers { get; set; }

        public FairyDroppingEnemiesAttribute(int roomNumber, int actorNumber, params int[] additionalActorNumbers )
        {
            RoomNumber = roomNumber;
            var newList = new List<int> { actorNumber };
            if (additionalActorNumbers.Length > 0)
            {
                newList.AddRange(additionalActorNumbers);
            }
            ActorNumbers = newList;
        }

    }
}
