namespace MMR.Randomizer.Models
{
#if DEBUG
    [System.Diagnostics.DebuggerDisplay("item[{Item}] from loc:[{Location}]")]
#endif
    public class ItemLocationPair
    {
        public string Item { get; set; }
        public string Location { get; set; }
    }
}

