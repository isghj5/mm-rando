using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Scene
    {
        public int File;
        public int Number;
        public List<Map> Maps = new List<Map>();
        public List<SceneSetup> Setups { get; set; } = new List<SceneSetup>();
    }

    public class SceneSetup
    {
        public int? ExitListAddress { get; set; }
        public int? CutsceneListAddress { get; set; }
    }
}
