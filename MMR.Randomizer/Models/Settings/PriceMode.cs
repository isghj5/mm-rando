using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models
{
    [Flags]
    [Description("Randomize Prices")]
    public enum PriceMode
    {
        None,

        [Description("Prices for item purchases will be randomized.")]
        Purchases = 1 << 0,

        [Description("Prices for minigames will be randomized.")]
        Minigames = 1 << 1,

        [Description("Prices for other miscellaneous spending will be randomized.")]
        Misc = 1 << 2,

        [Description("Price randomization will range from 1-999 instead of 1-500. Any shuffled or non-randomized price will double.")]
        AccountForRoyalWallet = 1 << 3,

        [Description("Prices will only be shuffled among each other. Must enable Puchases, Minigames or Misc for this setting to have any effect.")]
        ShuffleOnly = 1 << 4,
    }
}
