using MMR.Common.Utils;
using MMR.Randomizer.Asm;
using MMR.Randomizer.Attributes.Setting;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace MMR.Randomizer.Models.Settings
{

    public class GameplaySettings
    {
        #region General settings

        /// <summary>
        /// Filepath to the input logic file
        /// </summary>
        [SettingType("File")]
        [Description("Path to a custom logic file.")]
        public string UserLogicFileName { get; set; } = "";

        [SettingIgnore]
        public string Logic { get; set; }

        /// <summary>
        /// Options for the Asm <see cref="Patcher"/>.
        /// </summary>
        [JsonIgnore]
        public AsmOptionsGameplay AsmOptions { get; set; } = new AsmOptionsGameplay();

        #endregion

        #region Asm Getters / Setters

        /// <summary>
        /// Whether or not to change Cow response behavior.
        /// </summary>
        [Description("When playing Epona's Song for a group of cows, the closest cow will respond, instead of the default behavior.")]
        public bool CloseCows
        {
            get { return this.AsmOptions.MiscConfig.Flags.CloseCows; }
            set { this.AsmOptions.MiscConfig.Flags.CloseCows = value; }
        }

        /// <summary>
        /// Whether or not to enable cycling arrow types while using the bow.
        /// </summary>
        [Description("Cycle through arrow types when pressing R while an arrow is out when using the bow.")]
        public bool ArrowCycling {
            get { return this.AsmOptions.MiscConfig.Flags.ArrowCycling; }
            set { this.AsmOptions.MiscConfig.Flags.ArrowCycling = value; }
        }

        /// <summary>
        /// Whether or not to disable crit wiggle.
        /// </summary>
        [DisplayName("Disable Crit Wiggle")]
        [Description("Disable crit wiggle movement modification when 1 heart of health or less.")]
        public bool CritWiggleDisable {
            get { return this.AsmOptions.MiscConfig.Flags.CritWiggleDisable; }
            set { this.AsmOptions.MiscConfig.Flags.CritWiggleDisable = value; }
        }

        /// <summary>
        /// Whether or not to draw hash icons on the file select screen.
        /// </summary>
        [SettingName("Hash Icons .png")]
        [Description("Draw hash icons on the File Select screen.")]
        public bool DrawHash {
            get { return this.AsmOptions.MiscConfig.Flags.DrawHash; }
            set { this.AsmOptions.MiscConfig.Flags.DrawHash = value; }
        }

        /// <summary>
        /// Whether or not to apply Elegy of Emptiness speedups.
        /// </summary>
        [Description("Applies various Elegy of Emptiness speedups.")]
        public bool ElegySpeedup {
            get { return this.AsmOptions.MiscConfig.Flags.ElegySpeedup; }
            set { this.AsmOptions.MiscConfig.Flags.ElegySpeedup = value; }
        }

        /// <summary>
        /// Whether or not to enable faster pushing and pulling speeds.
        /// </summary>
        [Description("Increase the speed of pushing and pulling blocks and faucets.")]
        public bool FastPush {
            get { return this.AsmOptions.MiscConfig.Flags.FastPush; }
            set { this.AsmOptions.MiscConfig.Flags.FastPush = value; }
        }

        /// <summary>
        /// Whether or not traps should behave slightly differently from other items in certain situations.
        /// </summary>
        [Description("Ice traps will behave slightly differently from other items in certain situations.")]
        public bool TrapQuirks {
            get { return this.AsmOptions.MiscConfig.Flags.TrapQuirks; }
            set { this.AsmOptions.MiscConfig.Flags.TrapQuirks = value; }
        }

        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        [Description("Show world models as their actual item instead of the original item. This includes Pieces of Heart, Heart Containers, Skulltula Tokens, Stray Fairies, Moon's Tear and the Seahorse.")]
        public bool UpdateWorldModels {
            get { return this.AsmOptions.MiscConfig.DrawFlags.FreestandingModels; }
            set { this.AsmOptions.MiscConfig.DrawFlags.FreestandingModels = value; }
        }

        /// <summary>
        /// Whether or not to allow using the ocarina underwater.
        /// </summary>
        [Description("Enable using the ocarina underwater.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool OcarinaUnderwater {
            get { return this.AsmOptions.MiscConfig.Flags.OcarinaUnderwater; }
            set { this.AsmOptions.MiscConfig.Flags.OcarinaUnderwater = value; }
        }

        /// <summary>
        /// Whether or not to enable Quest Item Storage.
        /// </summary>
        [Description("Enable Quest Item Storage, which allows for storing multiple quest items in their dedicated inventory slot. Quest items will also always be consumed when used.")]
        [SettingExclude(false, nameof(KeepQuestTradeThroughTime))]
        public bool QuestItemStorage {
            get { return this.AsmOptions.MiscConfig.Flags.QuestItemStorage; }
            set { this.AsmOptions.MiscConfig.Flags.QuestItemStorage = value; }
        }

        /// <summary>
        /// Whether or not to enable Continuous Deku Hopping.
        /// </summary>
        [Description("Press A while hopping across water to keep hopping.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool ContinuousDekuHopping
        {
            get { return this.AsmOptions.MiscConfig.Flags.ContinuousDekuHopping; }
            set { this.AsmOptions.MiscConfig.Flags.ContinuousDekuHopping = value; }
        }

        [Description("Goron Link will sink in water instead of drowning.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool IronGoron
        {
            get { return this.AsmOptions.MiscConfig.Flags.IronGoron; }
            set { this.AsmOptions.MiscConfig.Flags.IronGoron = value; }
        }

        /// <summary>
        /// Updates shop models and text
        /// </summary>
        [Description("Shops models and text will be updated to match the item they give.")]
        public bool UpdateShopAppearance
        {
            get { return this.AsmOptions.MiscConfig.DrawFlags.ShopModels; }
            set { this.AsmOptions.MiscConfig.DrawFlags.ShopModels = value; }
        }

        /// <summary>
        /// Updates shop models and text
        /// </summary>
        [Description("Enable swords, wallets, magic, bomb bags, quivers and the Goron Lullaby to be found in the intended order.")]
        public bool ProgressiveUpgrades
        {
            get { return this.AsmOptions.MiscConfig.Flags.ProgressiveUpgrades; }
            set { this.AsmOptions.MiscConfig.Flags.ProgressiveUpgrades = value; }
        }

        [Description("Targeting an enemy shows their health bar.")]
        public bool TargetHealthBar
        {
            get { return this.AsmOptions.MiscConfig.Flags.TargetHealth; }
            set { this.AsmOptions.MiscConfig.Flags.TargetHealth = value; }
        }

        [Description("Link can climb most surfaces.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool ClimbMostSurfaces
        {
            get { return this.AsmOptions.MiscConfig.Flags.ClimbAnything; }
            set { this.AsmOptions.MiscConfig.Flags.ClimbAnything = value; }
        }

        /// <summary>
        /// Whether or not to enable spawning scarecrow without Scarecrow's Song.
        /// </summary>
        [Description("Spawn scarecrow automatically when using ocarina if within range.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool FreeScarecrow
        {
            get { return this.AsmOptions.MiscConfig.Flags.FreeScarecrow; }
            set { this.AsmOptions.MiscConfig.Flags.FreeScarecrow = value; }
        }

        [Description("Fills wallet with max rupees upon finding a wallet upgrade.")]
        public bool FillWallet
        {
            get { return this.AsmOptions.MiscConfig.Flags.FillWallet; }
            set { this.AsmOptions.MiscConfig.Flags.FillWallet = value; }
        }

        [Description("Auto-invert time at the start of a cycle.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public AutoInvertState AutoInvert
        {
            get { return this.AsmOptions.MiscConfig.Flags.AutoInvert; }
            set { this.AsmOptions.MiscConfig.Flags.AutoInvert = value; }
        }

        [Description("Allows the Giant's Mask to be used anywhere with a high enough (or no) ceiling.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool GiantMaskAnywhere
        {
            get { return this.AsmOptions.MiscConfig.Flags.GiantMaskAnywhere; }
            set { this.AsmOptions.MiscConfig.Flags.GiantMaskAnywhere = value; }
        }

        [Description("Grant both archery rewards with a sufficient score.")]
        public bool DoubleArcheryRewards
        {
            get { return this.AsmOptions.MiscConfig.Speedups.DoubleArcheryRewards; }
            set { this.AsmOptions.MiscConfig.Speedups.DoubleArcheryRewards = value; }
        }

        [Description("Hit Tags and Invisible Rupees will emit a sparkle.")]
        public bool HiddenRupeesSparkle
        {
            get { return this.AsmOptions.MiscConfig.Flags.HiddenRupeesSparkle; }
            set { this.AsmOptions.MiscConfig.Flags.HiddenRupeesSparkle = value; }
        }

        [Description("Draws a minimap during the Treasure Chest Game if you have the Map of Clock Town:\n\n - Off: No minimap, default vanilla behaviour.\n - Minimal: Minimap is displayed, blocks appear on minimap when triggered.\n - Conditional Spoiler: Minimal behaviour, and if the Mask of Truth is aquired along with Map of Clock Town, spoil the maze layout.\n - Spoiler: Only Map of Clock Town needed to spoil the maze layout.")]
        public ChestGameMinimapState ChestGameMinimap
        {
            get { return this.AsmOptions.MiscConfig.Speedups.ChestGameMinimap; }
            set { this.AsmOptions.MiscConfig.Speedups.ChestGameMinimap = value; }
        }

        [Description("Makes it safer to use glitches:\n - Prevents HESS crash\n - Prevents Weirdshot crash\n - Prevents Action Swap crash\n - Prevents Song of Double Time softlock during 0th or 4th day\n - Prevents Tatl text softlock on 0th of 4th day\n - Prevents 0th day file deletion\n - Prevents hookslide crash\n - Prevents softlocks when using Remote Hookshot\n - Prevents 0th day Goron Bow crash\n - Applies safety fixes for Fierce Deity even if Fierce Deity Anywhere is not enabled\n - Index warp no longer crashes or softlocks (but you won't be able to use it to access the Debug Menu)\n - Prevents softlocks when interrupting mask transformations\n - Mayor is removed on 4th day\n - Deku Playground Employees are removed on 4th day\n - Prevents Gossip Stone time from crashing on 4th day\n - Prevents Town Shooting Gallery from crashing on 0th day and 4th day\n - TODO more...")]
        public bool SaferGlitches
        {
            get { return this.AsmOptions.MiscConfig.Flags.SaferGlitches; }
            set { this.AsmOptions.MiscConfig.Flags.SaferGlitches = value; }
        }

        [Description("If you have found Bombchu, then any random Bomb drop or fixed non-randomized Bomb drop will have a chance to drop Bombchu instead. Where relevant, Bombchu packs of 1 and 5 will be in logic in addition to packs of 10.")]
        public bool BombchuDrops
        {
            get { return this.AsmOptions.MiscConfig.Flags.BombchuDrops; }
            set { this.AsmOptions.MiscConfig.Flags.BombchuDrops = value; }
        }

        [Description("Transforming using Deku Mask, Goron Mask, Zora Mask and Fierce Deity's Mask will be almost instant. These items can no longer be used as \"cutscene items\".")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool InstantTransform
        {
            get { return this.AsmOptions.MiscConfig.Flags.InstantTransform; }
            set { this.AsmOptions.MiscConfig.Flags.InstantTransform = value; }
        }

        [Description("Use a bomb while an arrow is out when using the bow to attach the bomb to the tip of the arrow.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool BombArrows
        {
            get { return this.AsmOptions.MiscConfig.Flags.BombArrows; }
            set { this.AsmOptions.MiscConfig.Flags.BombArrows = value; }
        }

        [Description("Recovery Hearts will not drop, and re-acquiring random items will turn into Green Rupees instead. Fairies will not heal except on death.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool FewerHealthDrops
        {
            get { return this.AsmOptions.MiscConfig.Flags.FewerHealthDrops; }
            set { this.AsmOptions.MiscConfig.Flags.FewerHealthDrops = value; }
        }

        [Description("Hold Start while unpausing to pause again after one frame passes.")]
        public bool EasyFrameByFrame
        {
            get { return this.AsmOptions.MiscConfig.Flags.EasyFrameByFrame; }
            set { this.AsmOptions.MiscConfig.Flags.EasyFrameByFrame = value; }
        }

        [Description("Nearby stray fairies, even randomized ones, will cause the Great Fairy Mask to shimmer.")]
        public bool FairyMaskShimmer
        {
            get { return this.AsmOptions.MiscConfig.Flags.FairyMaskShimmer; }
            set { this.AsmOptions.MiscConfig.Flags.FairyMaskShimmer = value; }
        }

        [Description("Nearby skulltula tokens, even randomized ones, will emit a spider crawling sound.")]
        public bool SkulltulaTokenSounds
        {
            get { return this.AsmOptions.MiscConfig.Flags.SkulltulaTokenSounds; }
            set { this.AsmOptions.MiscConfig.Flags.SkulltulaTokenSounds = value; }
        }

        [Description("Instead of being immune to damage while riding Epona, Link will take damage and be thrown off.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageOnEpona
        {
            get { return this.AsmOptions.MiscConfig.Flags.TakeDamageOnEpona; }
            set { this.AsmOptions.MiscConfig.Flags.TakeDamageOnEpona = value; }
        }

        [Description("Link will take damage when being hit on his shield, and can't recoil off damage to the shield.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageWhileShielding
        {
            get { return this.AsmOptions.MiscConfig.Flags.TakeDamageWhileShielding; }
            set { this.AsmOptions.MiscConfig.Flags.TakeDamageWhileShielding = value; }
        }

        [Description("Link will take damage when falling into voids or voiding out in water.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageFromVoid
        {
            get { return this.AsmOptions.MiscConfig.Flags.TakeDamageFromVoid; }
            set { this.AsmOptions.MiscConfig.Flags.TakeDamageFromVoid = value; }
        }

        [Description("Dogs will damage Deku Link.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageFromDog { get; set; }

        [Description("Link will take damage when being hit by Gorons during the Goron Race.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageFromGorons { get; set; }

        [Description("Getting thrown out after being caught by guards will deal damage. Being thrown out after getting the reward from the Imprisoned Monkey will not deal damage.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageGettingCaught { get; set; }

        [Description("Gibdos will deal damage immediately after grabbing Link.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageFromGibdosFaster { get; set; }

        [Description("Link will take damage from Dexihands.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool TakeDamageFromDexihands { get; set; }

        /// <summary>
        /// How many boss remains are required to proceed through the final Giants cutscene.
        /// </summary>
        [Description("Set the number of Boss Remains required to proceed through the final Giants cutscene.")]
        [Range(0, 4)]
        public byte RequiredBossRemains
        {
            get { return this.AsmOptions.MiscConfig.MMRBytes.RequiredBossRemains; }
            set { this.AsmOptions.MiscConfig.MMRBytes.RequiredBossRemains = value; }
        }

        #endregion

        #region Random Elements

        /// <summary>
        /// Selected mode of logic (affects randomization rules)
        /// </summary>
        [Description("Select mode of logic:\n - Casual: The randomization logic ensures that the game can be beaten casually.\n - Glitched: The randomization logic allows for placement of items that are only obtainable using known glitches.\n - Vanilla Layout: All items are left vanilla.\n - User Logic: Upload your own custom logic to be used in the randomization.\n - No Logic: Completely random, no guarantee the game is beatable. Uses Glitched logic with all tricks enabled for HTML tracker and Blitz junk location calculation.")]
        [SettingExclude(LogicMode.Casual, nameof(UserLogicFileName))]
        [SettingExclude(LogicMode.Glitched, nameof(UserLogicFileName))]
        [SettingExclude(LogicMode.Vanilla,
            nameof(UserLogicFileName),
            nameof(AddSongs),
            nameof(ItemPlacement),
            nameof(ProgressiveUpgrades),
            nameof(CustomJunkLocationsString),
            nameof(CustomItemListString),
            nameof(RandomizeDungeonEntrances),
            nameof(RandomizeBossRooms),
            nameof(StartingItemMode),
            nameof(RequiredBossRemains),
            nameof(VictoryMode),
            nameof(PriceMode),
            nameof(BossRemainsMode),
            nameof(BossKeyMode),
            nameof(SmallKeyMode),
            nameof(StrayFairyMode),
            nameof(DungeonNavigationMode),
            nameof(TrapAmount),
            nameof(TrapAppearance),
            nameof(TrapQuirks),
            nameof(TrapWeights),
            nameof(GossipHintStyle),
            nameof(GaroHintStyle),
            nameof(ClearHints),
            nameof(ClearGaroHints),
            nameof(ImportanceCount),
            nameof(ImportanceCountGaro),
            nameof(OverrideMaxNumberOfClockTownGossipHints),
            nameof(OverrideMaxNumberOfClockTownGaroHints),
            nameof(OverrideNumberOfRequiredGossipHints),
            nameof(OverrideNumberOfRequiredGaroHints),
            nameof(OverrideNumberOfNonRequiredGossipHints),
            nameof(OverrideNumberOfNonRequiredGaroHints),
            nameof(HintsIndicateImportance),
            nameof(MixGossipAndGaroHints),
            nameof(RemainsHint),
            nameof(OathHint),
            nameof(FairyAndSkullHint),
            nameof(UpdateShopAppearance),
            nameof(UpdateChests),
            nameof(UpdateWorldModels),
            nameof(UpdateNPCText),
            nameof(PreventDowngrades),
            nameof(FairyMaskShimmer),
            nameof(SkulltulaTokenSounds),
            nameof(EnabledTricks)
        )]
        [SettingExclude(LogicMode.NoLogic, nameof(UserLogicFileName), nameof(EnabledTricks))]
        public LogicMode LogicMode { get; set; }
        
        [Description("Select the order that items are placed. If you don't know what this does, just use Random.")]
        public ItemPlacement ItemPlacement { get; set; } = ItemPlacement.Bespoke;

        public HashSet<string> EnabledTricks { get; set; } = new HashSet<string>
        {
            "Exit OSH Without Goron",
            "Lensless Chests",
            "Day 2 Grave Without Lens of Truth",
            "SHT Lensless Walls/Ceilings",
            "Pinnacle Rock without Seahorse",
            "Run Through Poisoned Water",
            "WFT 2nd Floor With Hookshot",
        };

        /// <summary>
        /// Add songs to the randomization pool
        /// </summary>
        [Description("Enable songs being placed among items in the randomization pool.")]
        public bool AddSongs { get; set; }

        /// <summary>
        /// Randomize which dungeon you appear in when entering one
        /// </summary>
        [Description("Enable randomization of dungeon entrances. \n\nStone Tower Temple is always vanilla, but Inverted Stone Tower Temple is randomized.")]
        public bool RandomizeDungeonEntrances { get; set; }

        /// <summary>
        /// Randomize which boss room you appear in when entering one
        /// </summary>
        [Description("Enable randomization of boss rooms. The boss door texture will match the boss behind the door.")]
        public bool RandomizeBossRooms { get; set; }

        /// <summary>
        /// (Beta) Randomize enemies
        /// </summary>
        [Description("Enable randomization of enemies. May cause softlocks in some circumstances, use at your own risk.")]
        public bool RandomizeEnemies { get; set; }

        /// <summary>
        /// Set how starting items are randomized
        /// </summary>
        [Description("Select a starting item mode:\n\nNone - You will not start with any randomized starting items.\nRandom - You will start with randomized starting items.\nAllow Temporary Items - You will start with randomized starting items including Keg, Magic Bean and Bottles with X.")]
        public StartingItemMode StartingItemMode { get; set; }

        public SmallKeyMode SmallKeyMode { get; set; } = SmallKeyMode.DoorsOpen;

        public BossKeyMode BossKeyMode { get; set; }

        public StrayFairyMode StrayFairyMode { get; set; }

        public BossRemainsMode BossRemainsMode { get; set; }

        public PriceMode PriceMode { get; set; }

        public DungeonNavigationMode DungeonNavigationMode { get; set; }

        public VictoryMode VictoryMode { get; set; }


        /// <summary>
        ///  Custom item list selections
        /// </summary>
        [JsonIgnore]
        public HashSet<Item> CustomItemList { get; set; } = new HashSet<Item>();

        public List<ItemCategory> ItemCategoriesRandomized { get; set; }

        public List<LocationCategory> LocationCategoriesRandomized { get; set; }

        public List<ClassicCategory> ClassicCategoriesRandomized { get; set; }

        /// <summary>
        ///  Custom item list string
        /// </summary>
        [SettingItemList(nameof(ItemUtils.AllLocations), true, true, nameof(ItemExtensions.ItemCategory), nameof(ItemExtensions.LocationCategory), nameof(ItemExtensions.ClassicCategory))]
        public string CustomItemListString { get; set; } = "-------------------------40c-80000000----21ffff-ffffffff-ffffffff-f0000000-7bbeeffa-7fffffff-e6f1fffe-ffffffff";

        /// <summary>
        ///  Custom starting item list selections
        /// </summary>
        [JsonIgnore]
        public List<Item> CustomStartingItemList { get; set; } = new List<Item>();

        /// <summary>
        ///  Custom starting item list string
        /// </summary>
        [SettingItemList(nameof(ItemUtils.CustomStartingItems), true, false, nameof(ItemExtensions.ItemCategory))]
        public string CustomStartingItemListString { get; set; } = "--1fbfc-5800000-";

        /// <summary>
        /// List of locations that must be randomized to junk
        /// </summary>
        [JsonIgnore]
        public List<GameObjects.Item> CustomJunkLocations { get; set; } = new List<GameObjects.Item>();

        /// <summary>
        ///  Custom junk location string
        /// </summary>
        [SettingItemList(nameof(ItemUtils.AllLocations), false, true, nameof(ItemExtensions.Regions))]
        public string CustomJunkLocationsString { get; set; } = "------------------------------200000-----400000--f000";

        /// <summary>
        /// Defines number of traps.
        /// </summary>
        [Description("Amount of ice traps to be added to pool by replacing junk items.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public TrapAmount TrapAmount { get; set; }

        /// <summary>
        /// The weighting to give different types of traps.
        /// </summary>
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public Dictionary<TrapType, int> TrapWeights { get; set; } = new Dictionary<TrapType, int>();

        /// <summary>
        /// Defines appearance pool for visible traps.
        /// </summary>
        [Description("Appearance of ice traps in pool for world models.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public TrapAppearance TrapAppearance { get; set; }

        #endregion

        #region Gimmicks

        /// <summary>
        /// Modifies the damage value when Link is damaged
        /// </summary>
        [Description("Select a damage mode, affecting how much damage Link takes:\n\n - Default: Link takes normal damage.\n - 2x: Link takes double damage.\n - 4x: Link takes quadruple damage.\n - 1-hit KO: Any damage kills Link.\n - Doom: Hardcore mode. Link's hearts are slowly being drained continuously.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public DamageMode DamageMode { get; set; }

        /// <summary>
        /// Adds an additional effect when Link is damaged
        /// </summary>
        [Description("Select an effect to occur whenever Link is being damaged:\n\n - Default: Vanilla effects occur.\n - Fire: All damage burns Link.\n - Ice: All damage freezes Link.\n - Shock: All damage shocks link.\n - Knockdown: All damage knocks Link down.\n - Random: Any random effect of the above.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public DamageEffect DamageEffect { get; set; }

        /// <summary>
        /// Modifies Link's movement
        /// </summary>
        [Description("Select a movement modifier:\n\n - Default: No movement modifier.\n - High speed: Link moves at a much higher velocity.\n - Super low gravity: Link can jump very high.\n - Low gravity: Link can jump high.\n - High gravity: Link can barely jump.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public MovementMode MovementMode { get; set; }

        /// <summary>
        /// Sets the type of floor globally
        /// </summary>
        [Description("Select a floortype for every floor ingame:\n\n - Default: Vanilla floortypes.\n - Sand: Link sinks slowly into every floor, affecting movement speed.\n - Ice: Every floor is slippery.\n - Snow: Similar to sand. \n - Random: Any random floortypes of the above.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public FloorType FloorType { get; set; }

        [Description("Adds Deku nuts and Deku sticks to drop tables in the field:\n\n - Default: No change, vanilla behavior.\n - Light: one stick and nut 1/16 chance termina bush.\n - Medium: More nuts, twice the chance\n - Extra: More sticks, more nuts, more drop locations.\n - Mayhem: You're crazy in the coconut!")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public NutAndStickDrops NutandStickDrops { get; set; }

        /// <summary>
        /// Sets the clock speed.
        /// </summary>
        [Description("Modify the speed of time.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public ClockSpeed ClockSpeed { get; set; } = ClockSpeed.Default;

        /// <summary>
        /// Hides the clock UI.
        /// </summary>
        [Description("Clock UI will be hidden.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool HideClock { get; set; }

        /// <summary>
        /// Increases or decreases the cooldown of using the blast mask
        /// </summary>
        [Description("Adjust the cooldown timer after using the Blast Mask.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public BlastMaskCooldown BlastMaskCooldown { get; set; }

        /// <summary>
        /// Enables Sun's Song
        /// </summary>
        [Description("Enable using the Sun's Song, which speeds up time to 400 units per frame (normal time speed is 3 units per frame) until dawn or dusk or a loading zone.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool EnableSunsSong { get; set; }

        /// <summary>
        /// Allow's using Fierce Deity's Mask anywhere
        /// </summary>
        [Description("Allow the Fierce Deity's Mask to be used anywhere. Also addresses some softlocks caused by Fierce Deity.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool AllowFierceDeityAnywhere { get; set; }

        /// <summary>
        /// Arrows, Bombs, and Bombchu will not be provided. You must bring your own. Logic Modes other than No Logic will account for this.
        /// </summary>
        [Description("Arrows, Bombs, and Bombchu will not be provided for minigames. You must bring your own. Logic Modes other than No Logic will account for this.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool ByoAmmo { get; set; }

        /// <summary>
        /// Dying causes the moon to crash, with all that that implies.
        /// </summary>
        [Description("Dying causes the moon to crash, with all that that implies.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool DeathMoonCrash { get; set; }

        /// <summary>
        /// Dying causes the moon to crash, with all that that implies.
        /// </summary>
        [Description("If the moon crashes, your save files will be erased.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool MoonCrashErasesFile
        {
            get { return this.AsmOptions.MiscConfig.Flags.MoonCrashFileErase; }
            set { this.AsmOptions.MiscConfig.Flags.MoonCrashFileErase = value; }
        }

        [Description("Hookshot can hook to any surface.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool HookshotAnySurface { get; set; }

        [Description("Entering the trials on the Moon will require masks, as per the vanilla behavior, but this is not considered by logic. Without this enabled, the trials will not require any masks to enter.")]
        [SettingTab(SettingTabAttribute.Type.Gimmicks)]
        public bool VanillaMoonTrialAccess { get; set; }

        #endregion

        #region Comfort / Cosmetics

        /// <summary>
        /// Certain cutscenes will play shorter, or will be skipped
        /// </summary>
        public ShortenCutsceneSettings ShortenCutsceneSettings { get; set; }

        /// <summary>
        /// Text is fast-forwarded
        /// </summary>
        [Description("Enable quick text. Dialogs are fast-forwarded to choices/end of dialog.")]
        public bool QuickTextEnabled { get; set; } = true;

        /// <summary>
        /// Replaces Link's default model
        /// </summary>
        [Description("Select a character model to replace Link's default model.")]
        public Character Character { get; set; }

        /// <summary>
        /// Method to write the gossip stone hints.
        /// </summary>
        [Description("Select a Gossip Stone hint style\n\n - Default: Vanilla Gossip Stone hints.\n - Random: Hints will contain locations of random items.\n - Relevant: Hints will contain locations of items loosely related to the vanilla hint or the area.\n - Competitive: Guaranteed hints about time-consuming checks, and hints regarding importance or non-importance of regions")]
        [SettingExclude(GossipHintStyle.Default, nameof(ClearHints),
            nameof(ImportanceCount),
            nameof(OverrideNumberOfRequiredGossipHints),
            nameof(OverrideNumberOfNonRequiredGossipHints),
            nameof(OverrideMaxNumberOfClockTownGossipHints),
            nameof(MixGossipAndGaroHints)
        )]
        [SettingExclude(GossipHintStyle.Random,
            nameof(ImportanceCount),
            nameof(OverrideNumberOfRequiredGossipHints),
            nameof(OverrideNumberOfNonRequiredGossipHints),
            nameof(OverrideMaxNumberOfClockTownGossipHints),
            nameof(MixGossipAndGaroHints)
        )]
        [SettingExclude(GossipHintStyle.Relevant,
            nameof(ImportanceCount),
            nameof(OverrideNumberOfRequiredGossipHints),
            nameof(OverrideNumberOfNonRequiredGossipHints),
            nameof(OverrideMaxNumberOfClockTownGossipHints),
            nameof(MixGossipAndGaroHints)
        )]
        public GossipHintStyle GossipHintStyle { get; set; } = GossipHintStyle.Competitive;

        [Description("Select a Garo hint style\n\n - Default: Vanilla Garo hints.\n - Random: Hints will contain locations of random items.\n - Relevant: Hints will contain locations of items loosely related to the vanilla hint or the area.\n - Competitive: Guaranteed hints about time-consuming checks, and hints regarding importance or non-importance of regions.")]
        [SettingExclude(GossipHintStyle.Default, nameof(ClearHints),
            nameof(ImportanceCountGaro),
            nameof(OverrideNumberOfRequiredGaroHints),
            nameof(OverrideNumberOfNonRequiredGaroHints),
            nameof(OverrideMaxNumberOfClockTownGaroHints),
            nameof(MixGossipAndGaroHints)
        )]
        [SettingExclude(GossipHintStyle.Random,
            nameof(ImportanceCountGaro),
            nameof(OverrideNumberOfRequiredGaroHints),
            nameof(OverrideNumberOfNonRequiredGaroHints),
            nameof(OverrideMaxNumberOfClockTownGaroHints),
            nameof(MixGossipAndGaroHints)
        )]
        [SettingExclude(GossipHintStyle.Relevant,
            nameof(ImportanceCountGaro),
            nameof(OverrideNumberOfRequiredGaroHints),
            nameof(OverrideNumberOfNonRequiredGaroHints),
            nameof(OverrideMaxNumberOfClockTownGaroHints),
            nameof(MixGossipAndGaroHints)
        )]
        public GossipHintStyle GaroHintStyle { get; set; }

        [Description("Garo hints distribution and gossip hint distribution will be mixed together.")]
        public bool MixGossipAndGaroHints { get; set; }

        /// <summary>
        /// FrEe HiNtS FoR WeEnIeS
        /// </summary>
        [Description("Enable reading gossip stone hints without requiring the Mask of Truth.")]
        public bool FreeHints { get; set; } = true;

        [Description("Enable fighting Garos by speaking to Tatl instead of wearing the Garo's Mask.")]
        public bool FreeGaroHints { get; set; }

        /// <summary>
        /// Clear hints
        /// </summary>
        [Description("Gossip stone hints will give clear item and location names.")]
        public bool ClearHints { get; set; } = true;

        [Description("Garo hints will give clear item and location names.")]
        public bool ClearGaroHints { get; set; }

        [Description("Gossip stone hints that normally give a Way of the Hero hint will instead tell you the number of important items in the region. Foolish hints will say zero important items. Foolish Except The Song hints will say one important song.")]
        public bool ImportanceCount { get; set; }

        [Description("Garo hints that normally give a Way of the Hero hint will instead tell you the number of important items in the region. Foolish hints will say zero important items. Foolish Except The Song hints will say one important song.")]
        public bool ImportanceCountGaro { get; set; }

        [Description("The angle at which Gossip Stones can be read will be more tolerant.")]
        public bool TolerantGossipStones { get; set; } = true;

        [Description("Location hints indicate the importance of the item.")]
        public bool HintsIndicateImportance { get; set; }

        [Description("Set the number of Way of the Hero hints that will appear on Gossip Stones.")]
        [Range(0, 14)]
        public int? OverrideNumberOfRequiredGossipHints { get; set; }

        [Description("Set the number of Foolish hints that will appear on Gossip Stones.")]
        [Range(0, 14)]
        public int? OverrideNumberOfNonRequiredGossipHints { get; set; }

        [Description("Set the maximum number of Way of the Hero hints on Gossip Stones that can be for a Clock Town region (including Laundry Pool).")]
        [Range(0, 7)]
        public int? OverrideMaxNumberOfClockTownGossipHints { get; set; }

        [Description("Set the number of Way of the Hero hints that will appear on Garos.")]
        [Range(0, 8)]
        public int? OverrideNumberOfRequiredGaroHints { get; set; }

        [Description("Set the number of Foolish hints that will appear on Garos.")]
        [Range(0, 8)]
        public int? OverrideNumberOfNonRequiredGaroHints { get; set; }

        [Description("Set the maximum number of Way of the Hero hints on Garos that can be for a Clock Town region (including Laundry Pool).")]
        [Range(0, 7)]
        public int? OverrideMaxNumberOfClockTownGaroHints { get; set; }

        [SettingIgnore]
        public List<List<Item>> OverrideHintPriorities { get; set; }

        [SettingIgnore]
        public HashSet<int> OverrideImportanceIndicatorTiers { get; set; }

        [SettingIgnore]
        public List<int> OverrideHintItemCaps { get; set; }

        /// <summary>
        /// Prevent downgrades
        /// </summary>
        [Description("Downgrading items will be prevented.")]
        public bool PreventDowngrades { get; set; } = true;

        /// <summary>
        /// Updates chest appearance to match contents
        /// </summary>
        [Description("Chest appearance will be updated to match the item they contain.")]
        public bool UpdateChests { get; set; }

        /// <summary>
        /// Updates NPC Text when referring to items and locations
        /// </summary>
        [SettingName("Update NPC Text")]
        [Description("NPC text that refers to items and their locations will be updated.")]
        public bool UpdateNPCText { get; set; }

        [Description("When you take the boss warp after defeating a temple boss, if you have enough boss remains to go to the moon and do not have Oath to Order then a cutscene will play in which the Giants reveal to you the region where the Oath to Order is located.\n\nIf Mix Songs With Items is disabled, they will instead reveal the region of the Powder Keg, Captain's Hat, Mirror Shield, Deku Mask, Goron Mask or Zora Mask, depending on where the Oath to Order is.")]
        public bool OathHint { get; set; }

        [Description("At the entrance to the Clock Tower Interior, Tatl will prompt to speak and will tell you the regions where the Boss Remains are located.")]
        public bool RemainsHint { get; set; }

        [Description("The cursed man in the Swamp Spider House, and the Green Shirt Man in the Ocean Spider House will tell you the regions of the remaining spider tokens. The Green Shirt Man in the Ocean Spider House will appear there when you blow up the entrance wall.\n\nEach fairy fountain (including Clock Town) will tell you the regions of the remaining Stray Fairies.")]
        public bool FairyAndSkullHint { get; set; }

        /// <summary>
        /// Change epona B button behavior to prevent player losing sword if they don't have a bow.
        /// </summary>
        [Description("Change Epona's B button behavior to prevent you from losing your sword if you don't have a bow.\nMay affect vanilla glitches that use Epona's B button.")]
        public bool FixEponaSword { get; set; } = true;

        /// <summary>
        /// Enables Pictobox prompt text to display the picture subject depending on flags.
        /// </summary>
        [Description("Display extra text showing which type of picture was captured by the Pictobox.")]
        public bool EnablePictoboxSubject { get; set; } = true;

        [Description("Goron spikes can charge midair and keep their charge. Minimum speed for goron spikes is removed.")]
        public bool LenientGoronSpikes { get; set; }

        [Description("Quest items will return to your inventory after Song of Time.")]
        public bool KeepQuestTradeThroughTime { get; set; }

        [Description("When the camera swings after grabbing a ledge, ensure the player's controls match Link's movement. Also makes the camera move to behind Link faster when Z-targeting.")]
        public bool ImprovedCamera { get; set; }

        #endregion

        #region Speedups

        /// <summary>
        /// Change beavers so the player doesn't have to race the younger beaver.
        /// </summary>
        [Description("Modify Beavers to not have to race the younger beaver.")]
        public bool SpeedupBeavers { get; set; } = true;

        /// <summary>
        /// Change the dampe flames to always have 2 on ground floor and one up the ladder.
        /// </summary>
        [Description("Change Dampe ghost flames to always have two on the ground floor and one up the ladder.")]
        public bool SpeedupDampe { get; set; } = true;

        /// <summary>
        /// Change dog race to make gold dog always win if the player has the Mask of Truth
        /// </summary>
        [Description("Make Gold Dog always win if you have the Mask of Truth.")]
        public bool SpeedupDogRace { get; set; } = true;

        /// <summary>
        /// Change the Lab Fish to only need to be fed one fish.
        /// </summary>
        [Description("Change Lab Fish to only need to be fed one fish.")]
        public bool SpeedupLabFish { get; set; } = true;

        /// <summary>
        /// Change the Bank reward thresholds to 200/500/1000 instead of 200/1000/5000.
        /// </summary>
        [Description("Change the Bank reward thresholds to 200/500/1000 instead of 200/1000/5000. Also reduces maximum bank capacity from 5000 to 1000.")]
        public bool SpeedupBank { get; set; } = true;

        /// <summary>
        /// Show the baby cuccos on the minimap.
        /// </summary>
        [Description("Makes the location of baby cuccos show on the minimap.")]
        public bool SpeedupBabyCuccos { get; set; }

        #endregion

        #region Functions

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public string Validate()
        {
            if (LogicMode == LogicMode.UserLogic && !File.Exists(UserLogicFileName))
            {
                return "User Logic not found or invalid, please load User Logic or change logic mode.";
            }
            if (CustomItemList == null)
            {
                return "Invalid custom item list.";
            }
            if (CustomStartingItemList == null)
            {
                return "Invalid custom starting item list.";
            }
            if (CustomJunkLocations == null)
            {
                return "Invalid junk locations list.";
            }
            if (KeepQuestTradeThroughTime && !QuestItemStorage)
            {
                return $"Must enable '{nameof(QuestItemStorage)}' if '{nameof(KeepQuestTradeThroughTime)}' is enabled.";
            }
            if (RequiredBossRemains < 0 || RequiredBossRemains > 4)
            {
                return $"{nameof(RequiredBossRemains)} must be between 0 and 4.";
            }
            if (VictoryMode != VictoryMode.Default && VictoryMode < VictoryMode.Fairies)
            {
                return "Must set some victory conditions or disable all victory modes.";
            }
            return null;
        }

        #endregion
    }
}
