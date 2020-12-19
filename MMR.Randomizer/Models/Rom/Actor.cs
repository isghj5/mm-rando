using MMR.Randomizer.Models.Vectors;

namespace MMR.Randomizer.Models.Rom
{
    public class Actor
    {
        // this is instance data, per actor, per scene.
        // if you want metadata about actor types use Gameobjects.Actor

        //not fully reading actor data
        //probably won't need to

        // TODO rename these to non-single letter variable names once I figure out how many are needed

        public int      m; // some flags at the very start?
        public int      n; // actor list index
        public vec16    p = new vec16(); // not sure these even get used for anything
        public vec16    r = new vec16(); // not sure these even get used for anything
        public int      v; // variant (parameters), 2 bytes that changes the nature of enemies with two or more variants

        public GameObjects.Actor actor;
        public int sceneID;
    }
}
