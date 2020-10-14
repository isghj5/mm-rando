namespace MMR.Randomizer.Models
{
    public class SpoilerEntrance
    {
        public SpoilerEntrance(string OriginalEntrance, string ShuffledEntrance)
        {
            this.OriginalEntrance = OriginalEntrance;
            this.ShuffledEntrance = ShuffledEntrance;
        }
        public string OriginalEntrance { get; }
        public string ShuffledEntrance { get; }
    }
}
