using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Scene
    {
        public int File;    // DMA Filetable index
        public int Number;  // Scene table index
        public GameObjects.Scene SceneEnum;  // Gameobject scene enum value
        public List<Map> Maps = new List<Map>();
    }
}
