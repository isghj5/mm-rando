using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMR.Randomizer.Models.Rom
{
    public class Enemy
    {
        public int Actor;
        public int Object;
        public int ObjectSize;
        public List<ushort> Variables = new List<ushort>();
        public int Type;
        public bool Stationary;
        public List<byte> ForbidFromScene = new List<byte>();
        public List<byte> ForbidToScene = new List<byte>();
    }
}
