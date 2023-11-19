using MMR.Common.Utils;
using MMR.Randomizer.Asm;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
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
        public string UserLogicFileName { get; set; } = "";

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
        public bool CloseCows
        {
            get { return this.AsmOptions.MiscConfig.Flags.CloseCows; }
            set { this.AsmOptions.MiscConfig.Flags.CloseCows = value; }
        }

        /// <summary>
        /// Whether or not to enable cycling arrow types while using the bow.
        /// </summary>
        public bool ArrowCycling {
            get { return this.AsmOptions.MiscConfig.Flags.ArrowCycling; }
            set { this.AsmOptions.MiscConfig.Flags.ArrowCycling = value; }
        }

        /// <summary>
        /// Whether or not to disable crit wiggle.
        /// </summary>
        public bool CritWiggleDisable {
            get { return this.AsmOptions.MiscConfig.Flags.CritWiggleDisable; }
            set { this.AsmOptions.MiscConfig.Flags.CritWiggleDisable = value; }
        }

        /// <summary>
        /// Whether or not to draw hash icons on the file select screen.
        /// </summary>
        public bool DrawHash {
            get { return this.AsmOptions.MiscConfig.Flags.DrawHash; }
            set { this.AsmOptions.MiscConfig.Flags.DrawHash = value; }
        }

        /// <summary>
        /// Whether or not to apply Elegy of Emptiness speedups.
        /// </summary>
        public bool ElegySpeedup {
            get { return this.AsmOptions.MiscConfig.Flags.ElegySpeedup; }
            set { this.AsmOptions.MiscConfig.Flags.ElegySpeedup = value; }
        }

        /// <summary>
        /// Whether or not to enable faster pushing and pulling speeds.
        /// </summary>
        public bool FastPush {
            get { return this.AsmOptions.MiscConfig.Flags.FastPush; }
            set { this.AsmOptions.MiscConfig.Flags.FastPush = value; }
        }

        /// <summary>
        /// Whether or not traps should behave slightly differently from other items in certain situations.
        /// </summary>
        public bool TrapQuirks {
            get { return this.AsmOptions.MiscConfig.Flags.TrapQuirks; }
            set { this.AsmOptions.MiscConfig.Flags.TrapQuirks = value; }
        }

        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        public bool UpdateWorldModels {
            get { return this.AsmOptions.MiscConfig.DrawFlags.FreestandingModels; }
            set { this.AsmOptions.MiscConfig.DrawFlags.FreestandingModels = value; }
        }

        /// <summary>
        /// Whether or not to allow using the ocarina underwater.
        /// </summary>
        public bool OcarinaUnderwater {
            get { return this.AsmOptions.MiscConfig.Flags.OcarinaUnderwater; }
            set { this.AsmOptions.MiscConfig.Flags.OcarinaUnderwater = value; }
        }

        /// <summary>
        /// Whether or not to enable Quest Item Storage.
        /// </summary>
        public bool QuestItemStorage {
            get { return this.AsmOptions.MiscConfig.Flags.QuestItemStorage; }
            set { this.AsmOptions.MiscConfig.Flags.QuestItemStorage = value; }
        }

        /// <summary>
        /// Whether or not to enable Continuous Deku Hopping.
        /// </summary>
        public bool ContinuousDekuHopping
        {
            get { return this.AsmOptions.MiscConfig.Flags.ContinuousDekuHopping; }
            set { this.AsmOptions.MiscConfig.Flags.ContinuousDekuHopping = value; }
        }

        public bool IronGoron
        {
            get { return this.AsmOptions.MiscConfig.Flags.IronGoron; }
            set { this.AsmOptions.MiscConfig.Flags.IronGoron = value; }
        }

        /// <summary>
        /// Updates shop models and text
        /// </summary>
        public bool UpdateShopAppearance
        {
            get { return this.AsmOptions.MiscConfig.DrawFlags.ShopModels; }
            set { this.AsmOptions.MiscConfig.DrawFlags.ShopModels = value; }
        }

        /// <summary>
        /// Updates shop models and text
        /// </summary>
        public bool ProgressiveUpgrades
        {
            get { return this.AsmOptions.MiscConfig.Flags.ProgressiveUpgrades; }
            set { this.AsmOptions.MiscConfig.Flags.ProgressiveUpgrades = value; }
        }

        public bool TargetHealthBar
        {
            get { return this.AsmOptions.MiscConfig.Flags.TargetHealth; }
            set { this.AsmOptions.MiscConfig.Flags.TargetHealth = value; }
        }

        public bool ClimbMostSurfaces
        {
            get { return this.AsmOptions.MiscConfig.Flags.ClimbAnything; }
            set { this.AsmOptions.MiscConfig.Flags.ClimbAnything = value; }
        }

        /// <summary>
        /// Whether or not to enable spawning scarecrow without Scarecrow's Song.
        /// </summary>
        public bool FreeScarecrow
        {
            get { return this.AsmOptions.MiscConfig.Flags.FreeScarecrow; }
            set { this.AsmOptions.MiscConfig.Flags.FreeScarecrow = value; }
        }

        public bool FillWallet
        {
            get { return this.AsmOptions.MiscConfig.Flags.FillWallet; }
            set { this.AsmOptions.MiscConfig.Flags.FillWallet = value; }
        }

        public AutoInvertState AutoInvert
        {
            get { return this.AsmOptions.MiscConfig.Flags.AutoInvert; }
            set { this.AsmOptions.MiscConfig.Flags.AutoInvert = value; }
        }

        public bool GiantMaskAnywhere
        {
            get { return this.AsmOptions.MiscConfig.Flags.GiantMaskAnywhere; }
            set { this.AsmOptions.MiscConfig.Flags.GiantMaskAnywhere = value; }
        }

        public bool DoubleArcheryRewards
        {
            get { return this.AsmOptions.MiscConfig.Speedups.DoubleArcheryRewards; }
            set { this.AsmOptions.MiscConfig.Speedups.DoubleArcheryRewards = value; }
        }

        public bool HiddenRupeesSparkle
        {
            get { return this.AsmOptions.MiscConfig.Flags.HiddenRupeesSparkle; }
            set { this.AsmOptions.MiscConfig.Flags.HiddenRupeesSparkle = value; }
        }

        public ChestGameMinimapState ChestGameMinimap
        {
            get { return this.AsmOptions.MiscConfig.Speedups.ChestGameMinimap; }
            set { this.AsmOptions.MiscConfig.Speedups.ChestGameMinimap = value; }
        }

        public bool SaferGlitches
        {
            get { return this.AsmOptions.MiscConfig.Flags.SaferGlitches; }
            set { this.AsmOptions.MiscConfig.Flags.SaferGlitches = value; }
        }

        public bool BombchuDrops
        {
            get { return this.AsmOptions.MiscConfig.Flags.BombchuDrops; }
            set { this.AsmOptions.MiscConfig.Flags.BombchuDrops = value; }
        }

        public bool InstantTransform
        {
            get { return this.AsmOptions.MiscConfig.Flags.InstantTransform; }
            set { this.AsmOptions.MiscConfig.Flags.InstantTransform = value; }
        }

        public bool BombArrows
        {
            get { return this.AsmOptions.MiscConfig.Flags.BombArrows; }
            set { this.AsmOptions.MiscConfig.Flags.BombArrows = value; }
        }

        public bool FewerHealthDrops
        {
            get { return this.AsmOptions.MiscConfig.Flags.FewerHealthDrops; }
            set { this.AsmOptions.MiscConfig.Flags.FewerHealthDrops = value; }
        }

        public bool EasyFrameByFrame
        {
            get { return this.AsmOptions.MiscConfig.Flags.EasyFrameByFrame; }
            set { this.AsmOptions.MiscConfig.Flags.EasyFrameByFrame = value; }
        }

        public bool FairyMaskShimmer
        {
            get { return this.AsmOptions.MiscConfig.Flags.FairyMaskShimmer; }
            set { this.AsmOptions.MiscConfig.Flags.FairyMaskShimmer = value; }
        }

        public bool SkulltulaTokenSounds
        {
            get { return this.AsmOptions.MiscConfig.Flags.SkulltulaTokenSounds; }
            set { this.AsmOptions.MiscConfig.Flags.SkulltulaTokenSounds = value; }
        }

        /// <summary>
        /// How many boss remains are required to proceed through the final Giants cutscene.
        /// </summary>
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
        public LogicMode LogicMode { get; set; }

        public ItemPlacement ItemPlacement { get; set; } = ItemPlacement.Bespoke;

        public HashSet<string> EnabledTricks { get; set; } = new HashSet<string>
        {
            "Exit Ocean Spider House without Goron",
            "Lensless Chests",
            "Lensless Walking",
            "Lensless Walls/Ceilings",
            "Pinnacle Rock without Seahorse",
            "Run Through Poisoned Water",
            "WFT 2nd Floor Skip",
        };

        /// <summary>
        /// Add songs to the randomization pool
        /// </summary>
        public bool AddSongs { get; set; }

        /// <summary>
        /// Randomize which dungeon you appear in when entering one
        /// </summary>
        public bool RandomizeDungeonEntrances { get; set; }

        /// <summary>
        /// Randomize which boss room you appear in when entering one
        /// </summary>
        public bool RandomizeBossRooms { get; set; }

        /// <summary>
        /// (Beta) Randomize enemies
        /// </summary>
        public bool RandomizeEnemies { get; set; }

        /// <summary>
        /// Set how starting items are randomized
        /// </summary>
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
        public string CustomItemListString { get; set; } = "-------------------------40c-80000000----21ffff-ffffffff-ffffffff-f0000000-7bbeeffa-7fffffff-e6f1fffe-ffffffff";

        /// <summary>
        ///  Custom starting item list selections
        /// </summary>
        [JsonIgnore]
        public List<Item> CustomStartingItemList { get; set; } = new List<Item>();

        /// <summary>
        ///  Custom starting item list string
        /// </summary>
        public string CustomStartingItemListString { get; set; } = "--1fbfc-5800000-";

        /// <summary>
        /// List of locations that must be randomized to junk
        /// </summary>
        [JsonIgnore]
        public List<GameObjects.Item> CustomJunkLocations { get; set; } = new List<GameObjects.Item>();

        /// <summary>
        ///  Custom junk location string
        /// </summary>
        public string CustomJunkLocationsString { get; set; } = "------------------------------200000-----400000--f000";

        /// <summary>
        /// Defines number of traps.
        /// </summary>
        public TrapAmount TrapAmount { get; set; }

        /// <summary>
        /// The weighting to give different types of traps.
        /// </summary>
        public Dictionary<TrapType, int> TrapWeights { get; set; } = new Dictionary<TrapType, int>();

        /// <summary>
        /// Defines appearance pool for visible traps.
        /// </summary>
        public TrapAppearance TrapAppearance { get; set; }

        #endregion

        #region Gimmicks

        /// <summary>
        /// Modifies the damage value when Link is damaged
        /// </summary>
        public DamageMode DamageMode { get; set; }

        /// <summary>
        /// Adds an additional effect when Link is damaged
        /// </summary>
        public DamageEffect DamageEffect { get; set; }

        /// <summary>
        /// Modifies Link's movement
        /// </summary>
        public MovementMode MovementMode { get; set; }

        /// <summary>
        /// Sets the type of floor globally
        /// </summary>
        public FloorType FloorType { get; set; }

        public NutAndStickDrops NutandStickDrops { get; set; }

        /// <summary>
        /// Sets the clock speed.
        /// </summary>
        public ClockSpeed ClockSpeed { get; set; } = ClockSpeed.Default;

        /// <summary>
        /// Hides the clock UI.
        /// </summary>
        public bool HideClock { get; set; }

        /// <summary>
        /// Increases or decreases the cooldown of using the blast mask
        /// </summary>
        public BlastMaskCooldown BlastMaskCooldown { get; set; }

        /// <summary>
        /// Enables Sun's Song
        /// </summary>
        public bool EnableSunsSong { get; set; }
        
        /// <summary>
        /// Allow's using Fierce Deity's Mask anywhere
        /// </summary>
        public bool AllowFierceDeityAnywhere { get; set; }
      
        /// <summary>
        /// Arrows, Bombs, and Bombchu will not be provided. You must bring your own. Logic Modes other than No Logic will account for this.
        /// </summary>
        public bool ByoAmmo { get; set; }

        /// <summary>
        /// Dying causes the moon to crash, with all that that implies.
        /// </summary>
        public bool DeathMoonCrash { get; set; }

        public bool HookshotAnySurface { get; set; }

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
        public bool QuickTextEnabled { get; set; } = true;

        /// <summary>
        /// Replaces Link's default model
        /// </summary>
        public Character Character { get; set; }

        /// <summary>
        /// Method to write the gossip stone hints.
        /// </summary>
        public GossipHintStyle GossipHintStyle { get; set; } = GossipHintStyle.Competitive;

        public GossipHintStyle GaroHintStyle { get; set; }

        public bool MixGossipAndGaroHints { get; set; }

        /// <summary>
        /// FrEe HiNtS FoR WeEnIeS
        /// </summary>
        public bool FreeHints { get; set; } = true;

        public bool FreeGaroHints { get; set; }

        /// <summary>
        /// Clear hints
        /// </summary>
        public bool ClearHints { get; set; } = true;

        public bool ClearGaroHints { get; set; }

        public bool TolerantGossipStones { get; set; } = true;

        public bool HintsIndicateImportance { get; set; }

        public int? OverrideNumberOfRequiredGossipHints { get; set; }

        public int? OverrideNumberOfNonRequiredGossipHints { get; set; }

        public int? OverrideMaxNumberOfClockTownGossipHints { get; set; }

        public int? OverrideNumberOfRequiredGaroHints { get; set; }

        public int? OverrideNumberOfNonRequiredGaroHints { get; set; }

        public int? OverrideMaxNumberOfClockTownGaroHints { get; set; }

        public List<List<Item>> OverrideHintPriorities { get; set; }

        public HashSet<int> OverrideImportanceIndicatorTiers { get; set; }

        public List<int> OverrideHintItemCaps { get; set; }

        /// <summary>
        /// Prevent downgrades
        /// </summary>
        public bool PreventDowngrades { get; set; } = true;

        /// <summary>
        /// Updates chest appearance to match contents
        /// </summary>
        public bool UpdateChests { get; set; }

        /// <summary>
        /// Updates NPC Text when referring to items and locations
        /// </summary>
        public bool UpdateNPCText { get; set; }

        /// <summary>
        /// Change epona B button behavior to prevent player losing sword if they don't have a bow.
        /// </summary>
        public bool FixEponaSword { get; set; } = true;

        /// <summary>
        /// Enables Pictobox prompt text to display the picture subject depending on flags.
        /// </summary>
        public bool EnablePictoboxSubject { get; set; } = true;

        public bool LenientGoronSpikes { get; set; }

        public bool KeepQuestTradeThroughTime { get; set; }

        public bool ImprovedCamera { get; set; }

        #endregion

        #region Speedups

        /// <summary>
        /// Change beavers so the player doesn't have to race the younger beaver.
        /// </summary>
        public bool SpeedupBeavers { get; set; } = true;

        /// <summary>
        /// Change the dampe flames to always have 2 on ground floor and one up the ladder.
        /// </summary>
        public bool SpeedupDampe { get; set; } = true;

        /// <summary>
        /// Change dog race to make gold dog always win if the player has the Mask of Truth
        /// </summary>
        public bool SpeedupDogRace { get; set; } = true;

        /// <summary>
        /// Change the Lab Fish to only need to be fed one fish.
        /// </summary>
        public bool SpeedupLabFish { get; set; } = true;

        /// <summary>
        /// Change the Bank reward thresholds to 200/500/1000 instead of 200/1000/5000.
        /// </summary>
        public bool SpeedupBank { get; set; } = true;

        /// <summary>
        /// Show the baby cuccos on the minimap.
        /// </summary>
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
            return null;
        }

        #endregion
    }
}
