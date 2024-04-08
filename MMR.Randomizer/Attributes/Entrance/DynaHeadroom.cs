using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class DynaHeadroom : Attribute
    {
        /// <summary>
        /// Defines how much headroom for dynapoly actors exists, as a delta over baseline
        /// </summary>
        /// <param name="Polygon"></param>
        /// <param name="Verticies"></param>

        public int Polygon;
        public int Verticies;
        public int Room;

        public DynaHeadroom(int polygon, int verticies, int room = -1)
        {
            this.Polygon = polygon;
            this.Verticies = verticies;
            this.Room = room;
        }
    }
}
