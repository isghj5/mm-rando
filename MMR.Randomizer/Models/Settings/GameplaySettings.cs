using MMR.Randomizer.Asm;
using MMR.Randomizer.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// Use Custom Item list for the logic.
        /// </summary>
        public bool UseCustomItemList { get; set; }

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
        /// Whether or not to enable faster pushing and pulling speeds.
        /// </summary>
        public bool FastPush {
            get { return this.AsmOptions.MiscConfig.Flags.FastPush; }
            set { this.AsmOptions.MiscConfig.Flags.FastPush = value; }
        }

        /// <summary>
        /// Whether or not to enable freestanding models.
        /// </summary>
        public bool UpdateWorldModels {
            get { return this.AsmOptions.MiscConfig.Flags.FreestandingModels; }
            set { this.AsmOptions.MiscConfig.Flags.FreestandingModels = value; }
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

        #endregion

        #region Random Elements

        /// <summary>
        /// Selected mode of logic (affects randomization rules)
        /// </summary>
        public LogicMode LogicMode { get; set; }

        public List<int> EnabledTricks { get; set; } = new List<int>();

        /// <summary>
        /// Add songs to the randomization pool
        /// </summary>
        public bool AddSongs { get; set; }

        /// <summary>
        /// (KeySanity) Add dungeon items (maps, compasses, keys) to the randomization pool
        /// </summary>
        public bool AddDungeonItems { get; set; }

        /// <summary>
        /// Add shop items to the randomization pool
        /// </summary>
        public bool AddShopItems { get; set; }

        /// <summary>
        /// Add moon items to the randomization pool
        /// </summary>
        public bool AddMoonItems { get; set; }

        /// <summary>
        /// Add great fairy rewards to the randomization pool
        /// </summary>
        public bool AddFairyRewards { get; set; }

        /// <summary>
        /// Add everything else to the randomization pool
        /// </summary>
        public bool AddOther { get; set; } = true;

        /// <summary>
        /// Add pre-clocktown nut chest to the randomization pool
        /// </summary>
        public bool AddNutChest { get; set; }

        /// <summary>
        /// Add starting sword/shield/heart containers to the randomization pool
        /// </summary>
        public bool CrazyStartingItems { get; set; }

        /// <summary>
        /// Add cow milk to the randomization pool
        /// </summary>
        public bool AddCowMilk { get; set; }

        /// <summary>
        /// Add skulltula tokens to the randomization pool
        /// </summary>
        public bool AddSkulltulaTokens { get; set; }

        /// <summary>
        /// Add stray fairies to the randomization pool
        /// </summary>
        public bool AddStrayFairies { get; set; }

        /// <summary>
        /// Add mundane rewards to the randomization pool
        /// </summary>
        public bool AddMundaneRewards { get; set; }

        /// <summary>
        /// Randomize the content of a bottle when catching (e.g. catching a fairy puts poe in bottle)
        /// </summary>
        public bool RandomizeBottleCatchContents { get; set; }

        /// <summary>
        /// Exclude song of soaring from randomization (it will be found in vanilla location)
        /// </summary>
        public bool ExcludeSongOfSoaring { get; set; } = true;

        /// <summary>
        /// Randomize which dungeon you appear in when entering one
        /// </summary>
        public bool RandomizeDungeonEntrances { get; set; }

        /// <summary>
        /// (Beta) Randomize enemies
        /// </summary>
        public bool RandomizeEnemies { get; set; }

        /// <summary>
        /// Prevents player starting with any items that are randomized.
        /// </summary>
        public bool NoStartingItems { get; set; } = true;


        /// <summary>
        ///  Custom item list selections
        /// </summary>
        [JsonIgnore]
        public List<int> CustomItemList { get; set; } = new List<int>();

        /// <summary>
        ///  Custom item list string
        /// </summary>
        public string CustomItemListString { get; set; }

        /// <summary>
        ///  Custom starting item list selections
        /// </summary>
        [JsonIgnore]
        public List<GameObjects.Item> CustomStartingItemList { get; set; } = new List<GameObjects.Item>();

        /// <summary>
        ///  Custom starting item list string
        /// </summary>
        public string CustomStartingItemListString { get; set; }

        /// <summary>
        /// List of locations that must be randomized to junk
        /// </summary>
        [JsonIgnore]
        public List<GameObjects.Item> CustomJunkLocations { get; set; } = new List<GameObjects.Item>();

        /// <summary>
        ///  Custom junk location string
        /// </summary>
        public string CustomJunkLocationsString { get; set; }

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

        #endregion

        #region Comfort / Cosmetics

        /// <summary>
        /// Certain cutscenes will play shorter, or will be skipped
        /// </summary>
        public bool ShortenCutscenes { get; set; }

        /// <summary>
        /// Text is fast-forwarded
        /// </summary>
        public bool QuickTextEnabled { get; set; }

        /// <summary>
        /// Replaces Link's default model
        /// </summary>
        public Character Character { get; set; }

        /// <summary>
        /// Method to write the gossip stone hints.
        /// </summary>
        public GossipHintStyle GossipHintStyle { get; set; }

        /// <summary>
        /// FrEe HiNtS FoR WeEnIeS
        /// </summary>
        public bool FreeHints { get; set; }

        /// <summary>
        /// Clear hints
        /// </summary>
        public bool ClearHints { get; set; }

        /// <summary>
        /// Prevent downgrades
        /// </summary>
        public bool PreventDowngrades { get; set; } = true;

        /// <summary>
        /// Updates shop models and text
        /// </summary>
        public bool UpdateShopAppearance { get; set; } = true;

        /// <summary>
        /// Updates chest appearance to match contents
        /// </summary>
        public bool UpdateChests { get; set; } = true;

        /// <summary>
        /// Change epona B button behavior to prevent player losing sword if they don't have a bow.
        /// </summary>
        public bool FixEponaSword { get; set; } = true;

        #endregion

        #region Speedups

        /// <summary>
        /// Change beavers so the player doesn't have to race the younger beaver.
        /// </summary>
        public bool SpeedupBeavers { get; set; }

        /// <summary>
        /// Change the dampe flames to always have 2 on ground floor and one up the ladder.
        /// </summary>
        public bool SpeedupDampe { get; set; }

        /// <summary>
        /// Change dog race to make gold dog always win if the player has the Mask of Truth
        /// </summary>
        public bool SpeedupDogRace { get; set; }

        /// <summary>
        /// Change the Lab Fish to only need to be fed one fish.
        /// </summary>
        public bool SpeedupLabFish { get; set; }

        /// <summary>
        /// Change the Bank reward thresholds to 200/500/1000 instead of 200/1000/5000.
        /// </summary>
        public bool SpeedupBank { get; set; }

        #endregion

        #region Functions

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, _jsonSerializerSettings);
        }

        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new WritablePropertiesOnlyResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };

        static GameplaySettings()
        {
            _jsonSerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public string Validate()
        {
            if (LogicMode == LogicMode.UserLogic && !File.Exists(UserLogicFileName))
            {
                return "User Logic not found or invalid, please load User Logic or change logic mode.";
            }
            if (UseCustomItemList && CustomItemList == null)
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
            return null;
        }

        #endregion
    }
}
