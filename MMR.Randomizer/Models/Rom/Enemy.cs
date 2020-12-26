using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Enemy
    {
        public string Name = ""; // for debug mostly, got real sick of looking up each and every actor index
        public int Actor;
        public int Object;
        public int ObjectSize;
        public List<int> Variables = new List<int>();
        public bool MustNotRespawn = false;
        public int Room;
        public int RoomActorIndex; // the position in the room actor list
        public int Type; // ?
        public int Stationary;
        public List<int> SceneExclude = new List<int>();
    }
}
