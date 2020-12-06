using MMR.Randomizer.Models.Vectors;

namespace MMR.Randomizer.Models.Rom
{
    public class Actor
    {
        //not fully reading actor data
        //probably won't need to

        public int m;
        public int n; // actor list index
        public vec16 p = new vec16();
        public vec16 r = new vec16();
        public int v; // varient, 2 bytes that changes the nature of enemies with two or more varients
    }
}
