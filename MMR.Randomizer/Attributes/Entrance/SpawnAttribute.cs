using MMR.Randomizer.GameObjects;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class SpawnAttribute : Attribute
    {
        public ushort SpawnId { get; private set; }
        public SpawnAttribute(Scene scene, byte spawnIndex)
        {
            SpawnId = (ushort)(((byte)scene << 9) + (spawnIndex << 4));
        }

        public SpawnAttribute(ushort spawnId)
        {
            SpawnId = spawnId;
        }
    }
}
