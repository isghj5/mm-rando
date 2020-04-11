using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Extensions;
using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class SpawnAttribute : Attribute
    {
        public Scene Scene { get; private set; }
        public ushort SpawnId { get; private set; }
        public SpawnAttribute(Scene scene, byte spawnIndex)
        {
            Scene = scene;
            SpawnId = (ushort)(((byte)scene << 9) + (spawnIndex << 4));
        }
    }
}
