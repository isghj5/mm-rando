using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Scene
    {

        public enum SceneSpecialObject
        {
            /// a scene can choose to load object 2 or 3, or none, in the scene header
            None = 0,  FieldKeep = 2,  DungeonKeep = 3
        }


        public int File;    // DMA Filetable index
        public int Number;  // Scene table index
        public GameObjects.Scene SceneEnum;  // Gameobject scene enum value
        public List<Map> Maps = new List<Map>();
        public List<SceneSetup> Setups { get; set; } = new List<SceneSetup>();
        public SceneSpecialObject SpecialObject;

        public bool HasDungeonObject()
        {
            return SpecialObject == SceneSpecialObject.DungeonKeep;
        }

        public bool HasFieldObject()
        {
            return SpecialObject == SceneSpecialObject.FieldKeep;
        }

    }

    public class SceneSetup
    {
        public int? ExitListAddress { get; set; }
        public int? CutsceneListAddress { get; set; }
    }
}
