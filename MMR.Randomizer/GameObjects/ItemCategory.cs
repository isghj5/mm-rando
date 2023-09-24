using System.ComponentModel;

namespace MMR.Randomizer.GameObjects
{
    public enum ItemCategory
    {
        Fake = -1,
        None,

        [Description("Randomize items on the main inventory screen other than trade items. Also includes Bombers' Notebook, Swords, Mirror Shield and Wallets.")]
        MainInventory,

        [Description("Randomize songs except Song of Soaring.")]
        Songs,

        [Description("Randomize the Song of Soaring.")]
        SongOfSoaring,

        [Description("Randomize Heart Containers.")]
        HeartContainers,

        [Description("Randomize Boss Remains.")]
        BossRemains,

        [Description("Randomize Pieces of Heart.")]
        PiecesOfHeart,

        [Description("Randomize Masks.")]
        Masks,

        [Description("Randomize Moon's Tear, Title Deeds, Letter to Kafei, Pendant of Memories, Room Key and Letter to Mama.")]
        TradeItems,

        [Description("Randomize Dungeon Maps/Compasses and overworld maps.")]
        Navigation,

        [Description("Randomize non-item Great Fairy rewards including Magic Power, Great Spin Attack, Extended Magic Power and Double Defense.")]
        MagicPowers,

        [Description("Randomize golden skulltula tokens. Tokens will not reset to 0 after Song of Time.")]
        SkulltulaTokens,

        [Description("Randomize stray fairies including the Clock Town stray fairy. Stray fairies will not reset to 0 after Song of Time.")]
        StrayFairies,

        [Description("Randomize small keys and boss keys.")]
        DungeonKeys,

        [Description("Randomize Gold Rupees.")]
        GoldRupees,

        [Description("Randomize Silver Rupees.")]
        SilverRupees,

        [Description("Randomize Purple Rupees.")]
        PurpleRupees,

        [Description("Randomize Red Rupees.")]
        RedRupees,

        [Description("Randomize Blue Rupees.")]
        BlueRupees,

        [Description("Randomize Green Rupees.")]
        GreenRupees,

        [Description("Randomize the recovery hearts in Pirates' Fortress.")]
        RecoveryHearts,

        [Description("Randomize large and small magic jars.")]
        MagicJars,

        [Description("Randomize Hero's Shields.")]
        Shields,

        [Description("Randomize Bombchu.")]
        Bombchu,

        [Description("Randomize Arrows.")]
        Arrows,

        [Description("Randomize Bombs.")]
        Bombs,

        [Description("Randomize Deku Nuts.")]
        DekuNuts,

        [Description("Randomize Deku Sticks.")]
        DekuSticks,

        [Description("Randomize Milk.")]
        Milk,

        [Description("Randomize Red Potions.")]
        RedPotions,

        [Description("Randomize Green Potions.")]
        GreenPotions,

        [Description("Randomize Blue Potions.")]
        BluePotions,

        [Description("Randomize the Chateau refill. Bottle with Chateau Romani is part of Main Inventory")]
        Chateau,

        [Description("Randomize the Seahorse.")]
        Seahorse,

        [Description("Randomize the Fairy purchase in the Trading Post.")]
        Fairy,

        [Description("Randomize bottle scoops.")]
        ScoopedItems,

        [Description("Randomize Bombers' Notebook entries.")]
        NotebookEntries,

        [Description("Randomize the frog choir members.")]
        Frogs,

        [Description("Randomize Ocarina and Song of Time.")]
        TimeTravel,

        [Description("Add the Royal Wallet to the item pool.")]
        RoyalWallet,
    }

    public enum LocationCategory
    {
        Fake = -1,
        None,

        [Description("Randomize chests that don't fit into the other categories.")]
        Chests,

        [Description("Randomize items rewarded by NPCs except Minigames.")]
        NpcRewards,

        [Description("Randomize freestanding items.")]
        Freestanding,

        [Description("Randomize purchases including shops, scrubs, tingle, bean man, milk bar and Gorman Bros.")]
        Purchases,

        [Description("Randomize starting items.")]
        StartingItems,

        [Description("Randomize items rewarded from minigames.")]
        Minigames,

        [Description("Randomize items earned by fighting bosses/minibosses.")]
        BossFights,

        [Description("Randomize items on The Moon.")]
        MoonItems,

        [Description("Randomize items spawned by enemies including freestanding Golden Skulltulas, enemies that normally spawn Stray Fairies and Takkuri.")]
        EnemySpawn,

        [Description("Randomize fixed dropped from grass. Only Keaton Grass and grass such as that near owl statues drop fixed items.")]
        Grass,

        [Description("Randomize fixed drops from jars including small jars and green jars.")]
        Jars,

        [Description("Randomize fixed drops from small and large crates.")]
        Crates,

        [Description("Randomize fixed drops from small snowballs.")]
        SmallSnowballs,

        [Description("Randomize fixed drops from large snowballs.")]
        LargeSnowballs,

        [Description("Randomize fixed drops from barrels. This includes items that already exist within barrels before they're destroyed.")]
        Barrels,

        [Description("Randomize fixed drops from beehives.")]
        Beehives,

        [Description("Randomize invisible items.")]
        InvisibleItems,

        [Description("Randomize items spawned by events including the Moon's Tear, the Sword School Gong, the Song Wall in Termina Field, the Telescope Guay and the Termina Field circling Guay.")]
        Events,

        [Description("Randomize items from soft soil.")]
        SoftSoil,

        [Description("Randomize items dropped by hitting specific spots in the game.")]
        HitSpots,

        [Description("Randomize fixed drops from rocks. Only rocks on walls drop fixed items. Also includes the item within the Red Rock in Mountain Village spring time.")]
        Rocks,

        [Description("Randomize bottle scoops.")]
        Scoops,

        [Description("Randomize Bombers' Notebook entries.")]
        NotebookEntries,

        [Description("Randomize fairies summoned at Gossip Stones and free floating fairies.")]
        Fairies,

        [Description("Randomize fairies that transform from butterflies.")]
        Butterflies,

        [Description("Randomize the frog choir members.")]
        Frogs,

        [Description("Randomize items that require glitches to collect.")]
        GlitchesRequired,
    }

    public enum ClassicCategory
    {
        Fake = -1,
        None,

        [Description("Randomize the base set of items.")]
        BaseItemPool,

        [Description("Randomize the Song of Soaring.")]
        SongOfSoaring,

        [Description("Enable keys, boss keys, maps and compasses being placed in the randomization pool.")]
        DungeonItems,

        [Description("Enable shop items being placed in the randomization pool.")]
        ShopItems,

        [Description("Enable captured bottle contents being randomized.")]
        CaughtBottleContents,

        [Description("Enable randomization of cow milk.")]
        CowMilk,

        [Description("Enable moon items being placed in the randomization pool.\n\nIncludes the four Moon Trial Heart Pieces, Fierce Deity's Mask and the two Link Trial chests.")]
        MoonItems,

        [Description("Enable great fairy rewards being placed in the randomization pool.\n\nIncludes Magic Power, Great Spin Attack, Extended Magic Power, Double Defense, Great Fairy's Sword and Great Fairy's Mask.")]
        GreatFairyRewards,

        [Description("Enable randomization of mundane rewards. See Help > Manual (F1) > Shuffles for details.")]
        MundaneRewards,

        [Description("Enable randomization of golden skulltula tokens. Tokens will not reset to 0 after Song of Time.")]
        SkulltulaTokens,

        [Description("Enable randomization of stray fairies. Stray fairies will not reset to 0 after Song of Time.")]
        StrayFairies,

        [Description("Enable randomization of starting Sword, Shield, and two Heart Containers.")]
        CrazyStartingItems,

        [Description("Enable miscellaneous items being placed in the randomization pool.\n\nAmong the miscellaneous items are:\nFreestanding heartpieces, overworld chests, (hidden) grotto chests, Tingle's maps and bank heartpiece.")]
        EverythingElse,

        [Description("Enable randomization of locations that require glitches, such as the pre-clocktown deku nut chest and the out of bounds deku palace rupee.")]
        GlitchesRequired,

        [Description("Randomize freestanding rupees.")]
        FreestandingRupees,

        [Description("Randomize fixed minor item drops.")]
        FixedMinorItemDrops,

        [Description("Randomize invisible rupees and hit tags.")]
        HiddenRupees,

        [Description("Randomize items from soft soil.")]
        SoftSoil,

        [Description("Randomize the boss remains.")]
        BossRemains,

        [Description("Randomize Bombers' Notebook entries.")]
        NotebookEntries,

        [Description("Randomize fairies summoned at Gossip Stones and free floating fairies.")]
        Fairies,

        [Description("Randomize fairies that transform from butterflies.")]
        Butterflies,

        [Description("Randomize the frog choir members.")]
        Frogs,

        [Description("Add the Royal Wallet (999 capacity) to the item pool.")]
        RoyalWallet,

        [Description("Randomize the Ocarina of Time and the Song of Time.")]
        OcarinaAndSongOfTime,
    }
}
