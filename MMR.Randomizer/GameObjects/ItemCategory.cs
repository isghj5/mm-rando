using System.ComponentModel;

namespace MMR.Randomizer.GameObjects
{
    public enum ItemCategory
    {
        Fake = -1,
        None,

        [Description("Randomize the Song of Soaring.")]
        SongOfSoaring,

        [Description("Randomize Pieces of Heart and Heart Containers.")]
        PiecesOfHeart,

        [Description("Randomize small keys and boss keys.")]
        DungeonKeys,

        [Description("Randomize Great Fairy rewards including Magic Power, Great Spin Attack, Extended Magic Power, Double Defense, Great Fairy's Sword and Great Fairy's Mask.")]
        GreatFairyRewards,

        [Description("Randomize shops including Milk Bar, Tingle and Scrubs.")]
        ShopItems,

        [Description("Randomize miscellaneous items. Currently this only includes the Seahorse.")]
        Misc,

        [Description("Randomize bottle scoops.")]
        CaughtBottleContents,

        [Description("Randomize locations that require glitches. Includes:\n\n* The Deku Nut chest beneath Clock Town.")]
        GlitchesRequired,

        [Description("Randomize milk from cows.\n\nOne inaccessible ranch cow is not included for Casual logic.")]
        CowMilk,

        [Description("Randomize starting Sword, Shield, and two Heart Containers.")]
        CrazyStartingItems,

        [Description("Randomize golden skulltula tokens. Tokens will not reset to 0 after Song of Time.")]
        SkulltulaTokens,

        [Description("Randomize stray fairies including the Clock Town stray fairy. Stray fairies will not reset to 0 after Song of Time.")]
        StrayFairies,

        [Description("Randomize Gold Rupees from chests, rewarded by NPCs and dropped by enemies.")]
        GoldRupees,

        [Description("Randomize Silver Rupees from chests.")]
        SilverRupees,

        [Description("Randomize Purple Rupees from chests, rewarded by NPCs and from jars.")]
        PurpleRupees,

        [Description("Randomize Red Rupees from chests, spawned by events, rewarded by NPCs, from beehives, rocks, soft soil, crates, keaton grass and freestanding/invisible ones.")]
        RedRupees,

        [Description("Randomize Blue Rupees from chests, spawned by events, rewarded by NPCs, from jars, rocks, soft soil, snowballs, beehives, crates and freestanding/invisible ones.")]
        BlueRupees,

        [Description("Randomize Green Rupees spawned by events and hit spots, from jars, crates, grass, keaton grass and freestanding/invisible ones.")]
        GreenRupees,

        [Description("Randomize the recovery hearts in Pirates' Fortress.")]
        RecoveryHearts,

        [Description("Randomize ammo (including Bombs, Arrows, Deku Nuts, Deku Sticks and Magic Bean) from chests, soft soil, jars, crates, snowballs, barrels and grass.")]
        Ammo,

        [Description("Randomize large and small magic jars from jars, crates, snowballs and grass.")]
        MagicJars,

        [Description("Randomize Bombchu.")]
        Bombchu,
    }
}
