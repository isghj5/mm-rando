using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    [System.Diagnostics.DebuggerDisplay("{SceneEnum.ToString()}:{File}")]
    public class Scene
    {

        public enum SceneSpecialObject
        {
            /// a scene can choose to load object 2 or 3, or none, in the scene header
            None = 0,  FieldKeep = 2,  DungeonKeep = 3
        }

        public int File;    // DMA Filetable index
        public int Number;  // Scene table index
        public SceneSpecialObject SpecialObject;
        public GameObjects.Scene SceneEnum;  // Gameobject scene enum value
        public List<Map> Maps = new List<Map>();
        public List<SceneSetup> Setups { get; set; } = new List<SceneSetup>();
        public List<Actor> Doors = new List<Actor>();

        public bool HasDungeonObject()
        {
            return SpecialObject == SceneSpecialObject.DungeonKeep;
        }

        public bool HasFieldObject()
        {
            return SpecialObject == SceneSpecialObject.FieldKeep;
        }

        public List<Actor> GetAllActors()
        {
            List<Actor> actorList = new List<Actor>();
            for(int mapIndex = 0; mapIndex < Maps.Count; ++mapIndex)
            {
                actorList.AddRange(Maps[mapIndex].Actors);
            }
            return actorList;
        }

        public string ToString()
        {
            return this.SceneEnum.ToString(); // just to shorten a touch
        }

    }

    public class SceneSetup
    {
        public int? ExitListAddress { get; set; }
        public int? CutsceneListAddress { get; set; }
    }
}
