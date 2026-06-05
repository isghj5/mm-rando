namespace MMR.Randomizer.Enemizer
{
    /// <summary>
    ///  Tracks objects we swap in the object list during randomization.
    /// </summary>
    public class ValueSwap
    {
        public int OldV;
        public int NewV;
        public int ChosenV; // Copy of NewV, first pass result, but we might change NewV to something else if duplicate

        public ValueSwap() { }

        public ValueSwap(int oldV, int newV)
        {
            this.OldV = oldV;
            this.NewV = this.ChosenV = newV;
        }
    }
}
