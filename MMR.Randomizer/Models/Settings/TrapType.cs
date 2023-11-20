using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    public enum TrapType
    {
        None,

        [Description("Ice traps freeze the player in ice, dealing damage over a few seconds or voiding Zora.")]
        Ice,

        [Description("Bomb traps explode in the player's position, dealing damage and knocking them back.")]
        Bomb,

        [Description("Rupoors take 10 rupees from your wallet.")]
        Rupoor,

        [Description("Literally nothing.")]
        Nothing,
    }
}
