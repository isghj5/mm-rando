using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class GossipQuoteExtensions
    {
        public static bool IsMoonGossipStone(this GossipQuote gossipQuote)
        {
            return gossipQuote.ToString().StartsWith("Moon");
        }

        public static bool IsGaroHint(this GossipQuote gossipQuote)
        {
            return gossipQuote.ToString().StartsWith("Garo");
        }
    }
}
