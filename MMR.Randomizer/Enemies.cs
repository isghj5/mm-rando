using Microsoft.Toolkit.HighPerformance.Extensions;
using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Actor;
using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Rom;
using MMR.Randomizer.Models.Settings;
using MMR.Randomizer.Models.Vectors;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// dotnet 4.5 req
using System.Runtime.CompilerServices;

// todo rename this actorutils.cs and move to MMR.Randomizer/Utils/


namespace MMR.Randomizer
{
    [System.Diagnostics.DebuggerDisplay("{OldV} -> {NewV}")]
    public class ValueSwap
    {
        // these are indexes of objects
        public int OldV;
        public int NewV;
        public int ChosenV; // Copy of NewV, first pass result, but we might change NewV to something else if duplicate
    }

    // this probably should either be in its own file or actor.cs
    [System.Diagnostics.DebuggerDisplay("[{filename}] 0x{ActorId.ToString(\"X3\")}:{fileID}")]
    public class InjectedActor
    {
        // when we inject a new actor theres some data we need
        // and some adjustments we need to make based on where it gets placed in vram
        public int ActorId   = 0;
        public int ObjectId  = 0;
        public int fileID    = 0;
        public int ObjectFid = 0;
        public (int poly, int vert) DynaLoad = (-1, -1);

        // if all new actor, we meed to know where the old vram start was when we shift VRAM for the actor
        public uint buildVramStart = 0;
        // init vars are located somewhere in .data, we want to know where exactly because its hard coded in overlay table
        public uint initVarsLocation = 0;

        public List<int> groundVariants = new List<int>();
        public List<int> flyingVariants = new List<int>();
        public List<int> waterVariants = new List<int>();
        public List<int> waterTopVariants = new List<int>();
        public List<int> waterBottomVariants = new List<int>();
        public List<int> respawningVariants = new List<int>();
        // variants with max
        public List<VariantsWithRoomMax> limitedVariants = new List<VariantsWithRoomMax>();
        public UnkillableAllVariantsAttribute unkillableAttr = null;
        public OnlyOneActorPerRoom onlyOnePerRoom = null;

        // should only be stored here if new actor
        public byte[] overlayBin;
        public uint overlayBinLen;
        public string filename = ""; // debugging
    }

    public class Enemies
    {
        public static List<InjectedActor> InjectedActors = new List<InjectedActor>();
        const int SMALLEST_OBJ = 0xF3; // 0x10 size, smallest vanilla object I could find

        private static List<GameObjects.Actor> VanillaEnemyList { get; set; }
        private static List<Actor> ReplacementCandidateList { get; set; }
        private static List<Actor> FreeCandidateList { get; set; }
        private static List<Actor> FreeOnlyCandidateList { get; set; } // not worthy by themselves, only if object was already selected
        // outer list is item.category, inner list is items
        private static List<GameObjects.ItemCategory> ActorizerKnownJunkCategories { get; set; }
        private static List<List<GameObjects.Item>> ActorizerKnownJunkItems { get; set; }
        private static Mutex EnemizerLogMutex = new Mutex();
        private static bool ACTORSENABLED = true;
        private static Random seedrng;
        private static Models.RandomizedResult _randomized;
        private static OutputSettings _outputSettings;
        private static CosmeticSettings _cosmeticSettings;

        // these have to be separate from Actor Enum for now beacuse they are for special objects, not regular types
        static int[] clayPotDungeonVariants = {
            0xB, // multiple
            0x1E, 0x5, // swamp spiderhouse spider pots
            0x4C02, 0x4E02, 0x5002, 0x5202, // wft
            0x5C0E, 0x601E, 0x621E, 0x4C0E, 0x660E, 0x741E, 0x5A0A, // ospiderhouse
            0x761E, 0x001A, 0x400A, 0x0186, 0x018A, 0x680A, 0x6E0A, 0x700A, 0x720E, // ospiderhouse
            0x5A1E, 0x5C1E, 0x400B, 0x420A, 0x521F, 0x440B, 0x4602, 0x561E,         // pirate bay rooms
            0x5013, 0x581E, 0x480B, 0x4A1E, 0x101F, 0x1203, 0x480B, 0x541E, 0x4E0B, // pirate bay rooms
            0x4015, 0x4215, 0x4415, 0x4615, 0x4815, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, // botw
            0x0186, 0x0187, 0x018A, 0x018C, 0x018A, 0x440A, 0x460A, 0x480A, 0x440B, 0x018B, 0x000F, 0x4210, 0x0015, 0x001E, // istt
            0x018A, 0x000F, 0x3811, 0x0015, 0x001E, 0x4210, 0x000A, 0x001E, 0x4C02, 0x4E02, 0x5002, 0x5202, // wft
            0xC00B, 0xC21E, 0xC40E, 0xFE0E, 0xFC0B, 0xFA1E, 0xF81E, 0xF81E, 0xF60E, 0xF410 // secret shrine
        };
        static int[] tallGrassFieldObjectVariants = {
            0x0, 0x800,
            0x500,
            0x0600, 0x700, 0xC00, 0xD00,
            0x0E00, 0x0E10, 0x0010,
            0x0610
        };

        public static void PrepareEnemyLists()
        {

            // list of slots to use
            VanillaEnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(act => act.ObjectIndex() > 3
                                && (act.IsEnemyRandomized() || (ACTORSENABLED && act.IsActorRandomized()))) // both
                            .ToList();

            /* var EnemiesOnly = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(act => act.ObjectIndex() > 3
                                && (act.IsEnemyRandomized()))
                            .ToList();
            //*/

            // list of replacement actors we can use to replace with
            // for now they are the same, in the future players will control how they load
            ReplacementCandidateList = new List<Actor>();
            //foreach (var actor in EnemiesOnly)
            foreach (var actor in VanillaEnemyList)
            {
                if (actor.NoPlacableVariants() == false)
                {
                    ReplacementCandidateList.Add(new Actor(actor, InjectedActors.Find(i => i.ActorId == (int) actor)));
                }
            }

            var freeCandidates = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                                .Where(act => act.ObjectIndex() <= 3
                                && (act.IsEnemyRandomized() || (ACTORSENABLED && act.IsActorRandomized())))
                                .ToList();

            // because this list needs to be re-evaluated per scene, start smaller here once
            FreeCandidateList = freeCandidates.Select(act => new Actor(act, InjectedActors.Find(i => i.ActorId == (int) act))).ToList();

            var freeOnlyCandidates = new List<GameObjects.Actor>();
            if (ACTORSENABLED)
            {
                freeOnlyCandidates = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                                            .Where(act => act.IsActorFreeOnly())
                                            .ToList();
            }

            // because this list needs to be re-evaluated per scene, start smaller here once
            FreeOnlyCandidateList = freeOnlyCandidates.Select(act => new Actor(act, InjectedActors.Find(i => i.ActorId == (int) act))).ToList();
        }

        private static void PrepareJunkSpiderTokens(List<(string, string)> allSphereItems) // tag: spiderhouse
        {
            /// TODO this can be simplified, it was more complex before I realized spheres are kinda useless
            List<GameObjects.Item> allSpiderTokens = _randomized.ItemList.FindAll(item => item.Item.ItemCategory() == GameObjects.ItemCategory.SkulltulaTokens).Select(u => u.Item).ToList();

            if ((_randomized.Settings.VictoryMode & Models.VictoryMode.SkullTokens) > 0)
                return; // victory mode for fairies is enabled, none are junk: leave early

            // some items we didnt consider junk for short depth checks, are junk here just because these are really late game
            var extendedJunkCategories = ActorizerKnownJunkCategories.ToList();
            extendedJunkCategories.Add(GameObjects.ItemCategory.Milk);
            extendedJunkCategories.Add(GameObjects.ItemCategory.SilverRupees);
            var swampSkullJunk = false;


            void AddTokens(string tokenSearch)
            {
                var tokensSearched = allSpiderTokens.FindAll(token => token.Name().Contains(tokenSearch)).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.SkulltulaTokens].AddRange(tokensSearched);
            }

            if (_randomized.Settings.LogicMode != Models.LogicMode.Casual)
            {
                var swampSkullReward = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MaskTruth).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(swampSkullReward.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    AddTokens("Swamp");
                    swampSkullJunk = true;
                }

                var oceanSkullReward1 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.UpgradeGiantWallet).Item;
                var oceanSkullReward2 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay2PurpleRupee).Item;
                var oceanSkullReward3 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay3RedRupee).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(oceanSkullReward1.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward2.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward3.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    AddTokens("Ocean");

                    if (swampSkullJunk) // both were junk, we can consider all of the tokens to be junk
                    {
                        ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.SkulltulaTokens);
                    }
                }

            }
            else // casual
            {
                // we have logic, just use the logic spheres

                var swampSkullReward = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MaskTruth).Item;
                if (extendedJunkCategories.Contains(swampSkullReward.ItemCategory() ?? GameObjects.ItemCategory.None))
                {
                    var swampTokenImportantSearch = allSphereItems.Any(u => u.Item1 == "Swamp Skulltula Spirit");
                    if (!swampTokenImportantSearch)
                    {
                        AddTokens("Swamp");
                        swampSkullJunk = true;
                    }
                }

                var oceanSkullReward1 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.UpgradeGiantWallet).Item;
                var oceanSkullReward2 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay2PurpleRupee).Item;
                var oceanSkullReward3 = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.MundaneItemOceanSpiderHouseDay3RedRupee).Item;
                // check if the reward is important, if not add them 
                if (extendedJunkCategories.Contains(oceanSkullReward1.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward2.ItemCategory() ?? GameObjects.ItemCategory.None)
                    && extendedJunkCategories.Contains(oceanSkullReward3.ItemCategory() ?? GameObjects.ItemCategory.None))
                {

                    var oceanTokenImportantSearch = allSphereItems.Any(u => u.Item1 == "Ocean Skulltula Spirit");
                    if (!oceanTokenImportantSearch)
                    {
                        AddTokens("Ocean");
                        if (swampSkullJunk) // both were junk, we can consider all of the tokens to be junk
                        {
                            ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.SkulltulaTokens);
                        }
                    }
                }
            }
        }

        private static void PrepareJunkStrayFairies(List<(string, string)> allSphereItems) // tag: strayfairy
        {
            var allFaires = _randomized.ItemList.FindAll(item => item.Item.ClassicCategory() == GameObjects.ClassicCategory.StrayFairies).Select(u => u.Item).ToList();

            if ((_randomized.Settings.VictoryMode & Models.VictoryMode.Fairies) > 0)
                return; // victory mode for fairies is enabled, none are junk: leave early

            void AddFairies(string tokenSearch)
            {
                var fairySearched = allFaires.FindAll(token => token.Name().Contains(tokenSearch)).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.StrayFairies].AddRange(fairySearched);
            }

            if (_randomized.Settings.LogicMode != Models.LogicMode.Casual)
            {
                var extendedJunkCategories = ActorizerKnownJunkCategories.ToList();
                extendedJunkCategories.Add(GameObjects.ItemCategory.Milk);
                extendedJunkCategories.Add(GameObjects.ItemCategory.SilverRupees);

                void AddBasedOnResult(GameObjects.Item item, string str)
                {
                    var reward = _randomized.ItemList.Find(i => i.NewLocation == item).Item;
                    // check if reward is junk, if so add all fairies 
                    if (extendedJunkCategories.Contains(reward.ItemCategory() ?? GameObjects.ItemCategory.None))
                    {
                        AddFairies(str);
                    }
                }

                AddBasedOnResult(GameObjects.Item.FairySpinAttack, "Woodfall");
                AddBasedOnResult(GameObjects.Item.FairyDoubleMagic, "Snowhead");
                AddBasedOnResult(GameObjects.Item.FairyDoubleDefense, "Great Bay");
                AddBasedOnResult(GameObjects.Item.ItemFairySword, "Stone Tower");
            }
            else // casual logic
            {
                // I used to do this but now that we have sphere its faster because the sphere list is smaller datasize
                //var woodfallFairyReward = _randomized.ItemList.Find(item => item.NewLocation == GameObjects.Item.FairySpinAttack).Item;
                //if (ItemUtils.IsJunk(woodfallFairyReward))

                void AddBasedOnSphere(string testToken, string searchToken)
                {
                    var search = allSphereItems.Any(u => u.Item1 == testToken);
                    // check if any of the fairies are considered important, if they aren't then they are junk 
                    if (!search)
                    {
                        AddFairies(searchToken);
                    }
                }

                AddBasedOnSphere("Woodfall Stray Fairy", "Woodfall");
                AddBasedOnSphere("Snowhead Stray Fairy", "Snowhead");
                AddBasedOnSphere("Great Bay Stray Fairy", "Great Bay");
                AddBasedOnSphere("Stone Tower Stray Fairy", "Stone Tower");
            }

            // test if all fairies are junk if so add to categories
            // TODO remove this and just use the item list, we have the item list after all
            var junkFairies = ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.StrayFairies];
            bool allFairiesJunk = junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyWoodfall1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairySnowhead1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyGreatBay1)
                                         && junkFairies.Contains(GameObjects.Item.CollectibleStrayFairyStoneTower1);
            if (allFairiesJunk)
            {
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.StrayFairies);
            }
        }

        private static void PrepareJunkNotebookEntries(List<(string, string)> allSphereItems)
        {
            /// Notebook entries are junk IF the settings do not specify getting all notebook is required to beat the seed

            if ((_randomized.Settings.VictoryMode & Models.VictoryMode.Notebook) > 0)
                return; // victory mode for notebook entries is enabled, none are junk: leave early

            void AddNotebookEntires()
            {
                var notebookEntries = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.NotebookEntries).Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.NotebookEntries].AddRange(notebookEntries);
            }

            if (_randomized.Settings.LogicMode != Models.LogicMode.Casual)
            {
                var entryRewards = _randomized.ItemList.FindAll(i =>  i.NewLocation.ToString().Contains("Notebook"));
                var nonJunkCount = 0;
                for (int i = 0; i < entryRewards.Count(); i++)
                {
                    var reward = entryRewards[i].Item;
                    var category = reward.ItemCategory() ?? GameObjects.ItemCategory.None;
                    if ( ! ActorizerKnownJunkCategories.Contains(category))
                    {
                        // we dont need to add the entries themselves they are already added to the junk list per-category, this is just for notebook itself
                        nonJunkCount++;
                    }
                }
                if (nonJunkCount > 0) // notebook leads to something and is not junk
                {
                    ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.MainInventory].Add(GameObjects.Item.ItemNotebook);
                    ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.NotebookEntries);
                }
            }
            else // casual logic
            {
                // check if any notebook entries are in the list of important items
                var notebookEntryImportantSearch = allSphereItems.Any(u => u.Item1.Contains("Notebook:"));
                if (!notebookEntryImportantSearch)
                {
                    AddNotebookEntires();

                    var notebookLocationSearch = allSphereItems.Any(u => u.Item2.Contains("Notebook")); // important items BEHIND notebook
                    if (!notebookLocationSearch)
                    {
                        ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.MainInventory].Add(GameObjects.Item.ItemNotebook);
                        ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.NotebookEntries);
                    }
                }
            }

        }

        private static void PrepareKegEntry(List<(string, string)> allSphereItems)
        {
            //var kegImportantSearch = allSphereItems.Any(u => u.Item1 == "Powder Keg");
            //if (!kegImportantSearch)
            //{
            //VanillaEnemyList.Add(GameObjects.Actor.)
            //}

        }

        private static void PrepareJunkScoopList(List<(string, string)> allSphereItems)
        {
            // if the scoops are vanilla they can never be considered junk
            if (_randomized.Settings.LogicMode == Models.LogicMode.Vanilla) return;
            // currently, we cannot discern if scoops are important or not in no logic
            if (_randomized.Settings.LogicMode == Models.LogicMode.NoLogic) return;

            var importantBottleItems = allSphereItems.FindAll(item => item.Item1.Contains("Bottle:"));

            // get all bottles as items that are not randomized for now we have to assume they are important
            var bottleCatches = _randomized.ItemList
                    .Where(item => item.DisplayName() != null && item.DisplayName().Contains("Bottle:"))
                    .ToList();
            var unrandomizedBottles = bottleCatches.Where(item => !item.IsRandomized).ToList();
            // add that list to importantBottleItems
            foreach (var itemstring in unrandomizedBottles)
                importantBottleItems.Add(("", itemstring.DisplayName()));

            // scoops are a special case, they dont count as junk items above since they are all in one category handle separatly
            // for all items in list of items that are scoop types
            //   check if each and every one is an important item
            var scoopItems = _randomized.ItemList.FindAll(item => item.Item.ItemCategory() == GameObjects.ItemCategory.ScoopedItems);
            var unImportantScoopIOs = scoopItems.FindAll(scoop => importantBottleItems.Count(important => important.Item2 == scoop.Item.Name()) == 0);
            List<GameObjects.Item> unimportantScoops = unImportantScoopIOs.Select(itemObj => itemObj.Item).ToList();

            ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.ScoopedItems].AddRange(unimportantScoops);
            
            if (unimportantScoops.Count() == bottleCatches.Count()) // if ALL scoops are unimportant
            {
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.ScoopedItems);
            }
        }

        private static void PrepareJunkHeartPieces()
        {
            // if not casual logic, we want to add these since those crazy people think hearts are junk
            if (_randomized.Settings.LogicMode != Models.LogicMode.Casual)
            {
                var heartPieces = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.PiecesOfHeart).Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.PiecesOfHeart].AddRange(heartPieces);

                var recoveryHearts = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.RecoveryHearts).Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.RecoveryHearts].AddRange(recoveryHearts);
                ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.PiecesOfHeart);
            }
        }

        private static void PrepareJunkRedRupee()
        {
            var redRupees = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.RedRupees).Select(itemObj => itemObj.Item).ToList();
            redRupees.Remove(GameObjects.Item.CollectableIkanaGraveyardDay2Bats1);
            ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.RedRupees].AddRange(redRupees);
            ActorizerKnownJunkCategories.Add(GameObjects.ItemCategory.RedRupees);
        }

        private static void PrepareJunkMapAndCompass()
        {
            // this does not work, without me knowing when they are junk or not TODO
            /// if the player does not get hints from these, they should count as junk, but dont know if thats a setting I can look up

            if (_randomized.Settings.LogicMode == Models.LogicMode.Vanilla
                || _randomized.Settings.LogicMode == Models.LogicMode.Casual)
            {
                return;
            }

            if (_randomized.Settings.RandomizeBossRooms == false)
            {
                var compass = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.Navigation
                                                                    && itemObj.Item.ToString().Contains("Compass"))
                                                  .Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.Navigation].AddRange(compass);
            }

            if (_randomized.Settings.RandomizeDungeonEntrances == false)
            {
                var maps = _randomized.ItemList.FindAll(itemObj => itemObj.Item.ItemCategory() == GameObjects.ItemCategory.Navigation
                                                                    && itemObj.Item.ToString().Contains("Map"))
                                                  .Select(itemObj => itemObj.Item).ToList();
                ActorizerKnownJunkItems[(int)GameObjects.ItemCategory.Navigation].AddRange(maps);
            }
        }

        private static void PrepareJunkItems()
        {
            /// Problem: IsJunk and ItemUtils.JunkItems aren't a good fit for actorizer replacing actors
            ///  solution: add more items that we check ourselves, and remove some junk items we want to allow

            var addedJunkItems = new List<GameObjects.Item>();

            // probably a better way to init a list of list to size, but not known
            ActorizerKnownJunkCategories = _actorizerDefaultJunkCategories.ToList(); // copy
            ActorizerKnownJunkItems = new List<List<GameObjects.Item>>(); // init
            foreach (var category in Enum.GetValues(typeof(GameObjects.ItemCategory)))
            {
                ActorizerKnownJunkItems.Add(new List<GameObjects.Item>());
            }

            var allSphereItems = new List<(string item, string location)>();
            if (_randomized.Settings.LogicMode == Models.LogicMode.Casual)
            {
                allSphereItems = _randomized.Spheres.SelectMany(u => u).ToList();
            }

            PrepareJunkHeartPieces(); // no logic only
            PrepareJunkRedRupee(); // crimson counts, and thats stupid and not fair, removing
            PrepareJunkScoopList(allSphereItems);
            PrepareJunkNotebookEntries(allSphereItems);
            // bug: because these lists are generated in linear, fairies dont know if spiders are junk
            //   currently spiders are put later just because junked fairy leads only to great fairies being randomized,
            //   which is often ignored, spiders are not
            PrepareJunkStrayFairies(allSphereItems);
            PrepareJunkSpiderTokens(allSphereItems);
            // all transformation and non-transofrmation mask <- already not considered junk
            // all boss remains <- already not considered junk

            // keg? not handle-able here
            // koume?

            // this should no longer be required now that we build the list of lists first
            //PrepareJunkOrganizeLists(addedJunkItems); // sets ActorizerKnownJunkItems
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReplacementListContains(GameObjects.Actor actor)
        {
            return ReplacementCandidateList.Find(act => act.ActorEnum == actor) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReplacementListRemove(List<Actor> replaceList, GameObjects.Actor actor)
        {
            // might be an easier one liner but this could get used a lot
            var removeActor = replaceList.Find(act => act.ActorEnum == actor);
            if (removeActor != null)
            {
                replaceList.Remove(removeActor);
            }
        }

        #region Read and Write Scene Actors and Objects

        public static void GetSceneEnemyActors(SceneEnemizerData thisSceneData)
        {
            /// Gets all actors in a scene, that we want to randomize
            /// this function is separate from object because actors and objects are a different list in the scene/room data

            var scene = thisSceneData.Scene;
            var log = thisSceneData.Log;

            void FixActorLastSecond(Actor mapActor, GameObjects.Actor matchingEnemy, int mapIndex, int actorIndex)
            {
                // since not all actors are usable, save doing some of this work only for actors we actually want to modify
                // do this only after we know this is an actor we want

                mapActor.Name = mapActor.ActorEnum.ToString();
                mapActor.ObjectSize = ObjUtils.GetObjSize(mapActor.ActorEnum.ObjectIndex());
                mapActor.RoomActorIndex = actorIndex;
                mapActor.MustNotRespawn = scene.SceneEnum.IsClearEnemyPuzzleRoom(mapIndex)
                                       || scene.SceneEnum.IsFairyDroppingEnemy(mapIndex, mapActor.RoomActorIndex);
                //Debug.Assert(actorNumber == scene.Maps[mapIndex].Actors.IndexOf(mapActor));
                // TODO: type lookup is not always accurate
                mapActor.Type = matchingEnemy.GetType(mapActor.OldVariant);
                mapActor.AllVariants = Actor.BuildVariantList(matchingEnemy);
                mapActor.Blockable = mapActor.ActorEnum.IsBlockable(scene.SceneEnum, mapActor.RoomActorIndex);
            }

            bool SpecialMultiObjectCases(Actor targetActor, int mapIndex, int actorIndex)
            {
                // some actors are special: can use multiple objects: these actors can use a special object

                if (thisSceneData.Scene.SpecialObject == Scene.SceneSpecialObject.FieldKeep
                 && targetActor.OldActorEnum == GameObjects.Actor.TallGrass)
                {

                    // special case: tall grass is multi object: one uses field_keep to draw a regular bush
                    //  until we have multi-object code, this needs a special case or rando ignores it
                    if (tallGrassFieldObjectVariants.Contains(targetActor.OldVariant))
                    {
                        var importantItem = ObjectIsCheckBlocked(scene, targetActor.ActorEnum, targetActor.OldVariant);
                        if (importantItem != null)
                            return false;

                        FixActorLastSecond(targetActor, targetActor.OldActorEnum, mapIndex, actorIndex);
                        targetActor.Variants.AddRange(tallGrassFieldObjectVariants);
                        targetActor.AllVariants[(int)GameObjects.ActorType.Ground] = targetActor.Variants; // have to update the types for variant compatiblity later
                        return true;
                    }
                }
                if (thisSceneData.Scene.SpecialObject == Scene.SceneSpecialObject.DungeonKeep
                 && targetActor.OldActorEnum == GameObjects.Actor.ClayPot)
                {

                    // special case: claypot is multi object: one uses dungeon keep to hold its assets
                    //  until we have multi-object code, this needs a special case or rando ignores it
                    if (clayPotDungeonVariants.Contains(targetActor.OldVariant))
                    {
                        var importantItem = ObjectIsCheckBlocked(scene, targetActor.ActorEnum, targetActor.OldVariant);
                        if (importantItem != null)
                            return false;

                        FixActorLastSecond(targetActor, targetActor.OldActorEnum, mapIndex, actorIndex);
                        targetActor.Variants.AddRange(clayPotDungeonVariants);
                        targetActor.AllVariants[(int)GameObjects.ActorType.Ground] = targetActor.Variants; // have to update the types for variant compatiblity later
                        return true;
                    }
                }

                return false;
            }

            var sceneEnemyList = new List<Actor>();
            var sceneObjectlessActors = new List<Actor>();
            for (int mapIndex = 0; mapIndex < scene.Maps.Count; ++mapIndex)
            {
                for (int actorIndex = 0; actorIndex < scene.Maps[mapIndex].Actors.Count; ++actorIndex) // (var mapActor in scene.Maps[mapIndex].Actors)
                {
                    var mapActor = scene.Maps[mapIndex].Actors[actorIndex];
                    var matchingEnemy = VanillaEnemyList.Find(act => (int)act == mapActor.ActorId);
                    if (matchingEnemy > 0)
                    {
                        var listOfAcceptableVariants = matchingEnemy.AllVariants(); 

                        // TODO: check if the specific actor can be randomized, required before continue:
                        // actor separation, scene reconstruction, object list extension,  

                        if (matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum))
                            continue;

                        if (SpecialMultiObjectCases(mapActor, mapIndex, actorIndex))
                        {
                            sceneObjectlessActors.Add(mapActor);
                            continue;
                        }

                        if (listOfAcceptableVariants.Contains(mapActor.OldVariant)) // regular actors
                        {
                            FixActorLastSecond(mapActor, matchingEnemy, mapIndex, actorIndex);

                            sceneEnemyList.Add(mapActor);
                        }
                        #if DEBUG
                        else
                        {
                            log.Append($" in scene [{scene.SceneEnum}][{mapIndex}]" +
                                $" actor was skipped over: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}]\n");
                        }
                        #endif
                    }
                    else // non-object based actors, standalone
                    {

                        void AddIfNoRestrictions(Actor testActor)
                        {
                            /// twas separated because I thought it would be reused
                            var sceneRestrictions = testActor.ActorEnum.GetAttribute<ForbidFromSceneAttribute>();
                            if (sceneRestrictions != null && sceneRestrictions.ScenesExcluded.Contains(thisSceneData.Scene.SceneEnum))
                                return; // continue // not valid to consider this actor

                            var importantItem = ObjectIsCheckBlocked(scene, testActor.ActorEnum, testActor.OldVariant);
                            if (importantItem != null)
                            {
                                #if DEBUG
                                var itemText = $"blocked by item [{ importantItem }]";
                                #else
                                var itemText = $"blocked by item [{ (int) importantItem}]";
                                #endif

                                log.AppendLine($" in scene [{scene.SceneEnum}]m[{mapIndex}]r[{mapActor.RoomActorIndex}]v[{mapActor.OldVariant.ToString("X4")}]" +
                                    $" actor: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}] was " + itemText);
                                return;
                            }

                            if ( ! testActor.Variants.Contains(mapActor.OldVariant))
                            {
                                log.AppendLine($" in scene [{scene.SceneEnum}][{mapIndex}] standalone was skipped over: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}]");
                                return; // non valid
                            }

                            FixActorLastSecond(testActor, testActor.ActorEnum, mapIndex, actorIndex);

                            sceneObjectlessActors.Add(mapActor);
                        }

                        var matchingFreeActor = FreeCandidateList.Find(act => act.ActorEnum == mapActor.ActorEnum);
                        if (matchingFreeActor != null)
                        {
                            AddIfNoRestrictions(mapActor);
                        }
                    }
                }

            }
            thisSceneData.Actors = sceneEnemyList;
            thisSceneData.Actors.AddRange(sceneObjectlessActors); // might want to rethink this eventually
            thisSceneData.StandaloneActors = sceneObjectlessActors;
        }

        // if one of these already exists somewhere in the logic I did not find it
        public static readonly List<GameObjects.ItemCategory> _actorizerDefaultJunkCategories = new List<GameObjects.ItemCategory>{
            GameObjects.ItemCategory.GreenRupees,
            GameObjects.ItemCategory.BlueRupees,
            //GameObjects.ItemCategory.RedRupees, // crimson rup in this list, removed by building into our own list
            GameObjects.ItemCategory.PurpleRupees,
            GameObjects.ItemCategory.Arrows,
            GameObjects.ItemCategory.Bombs,
            GameObjects.ItemCategory.DekuSticks,
            GameObjects.ItemCategory.DekuNuts,
            GameObjects.ItemCategory.Fairy,
            GameObjects.ItemCategory.GreenPotions,
            GameObjects.ItemCategory.None, // think this was mostly used for traps
            GameObjects.ItemCategory.MagicJars
        };

        private static bool IsActorizerJunk(GameObjects.Item itemInCheck)
        {
            /// problem: ItemUtils.IsJunk only cares about never-important items like rups
            ///  and ItemUtils.IsLogicalJunk cares about logic too strongly and can junk cool things like swords
            ///  goal: use IsJunk and add extra conditions that can happen

            // we need to build a list of known junk items and check that list here
            var category = itemInCheck.ItemCategory() ?? GameObjects.ItemCategory.None;
            var intCategory = (int)category;
            if (intCategory <= 0) return true; // zero is None, recovery heart is something below zero
            var specificCategory = ActorizerKnownJunkItems[intCategory];
            if (specificCategory.Contains(itemInCheck))
            {
                return true;
            }

            //return ItemUtils.IsJunk(itemInCheck);
            return ActorizerKnownJunkCategories.Contains(category);
        }

        // todo move to actorutils
        // TODO rename to ACTOR is check blocked, as we will soon need to do this for actors not whole actor objects
        // for now its just the objectlessactors, checkrestricted
        private static GameObjects.Item? ObjectIsCheckBlocked(Scene scene, GameObjects.Actor testActor, int variant = -1)
        {
            /// checks if randomizing the actor would interfere with getting access to a check
            /// and then checks if the item is junk, before allowing randimization
            /// tags: itemblocked, item restricted, check restricted
            /// ISSUE: if this is called from object culling, variant can break for us instead of help us
            const GameObjects.Scene ANYSCENE = (GameObjects.Scene)GameObjects.ActorConst.ANY_SCENE;

            var checkRestrictedAttr = testActor.GetAttributes<CheckRestrictedAttribute>();
            if (checkRestrictedAttr != null && checkRestrictedAttr.Count() > 0) // actor has check restrictions
            {
                var reducedList = checkRestrictedAttr.ToList().FindAll(attr => attr.Scene == scene.SceneEnum || (int) attr.Scene == -1);

                foreach (var restriction in reducedList) // can have multiple rules
                {
                    if (restriction.Scene != ANYSCENE && restriction.Scene != scene.SceneEnum) continue;

                    if (restriction.Variant != GameObjects.ActorConst.ANY_VARIANT && restriction.Variant != variant)
                        continue; // we dont care about this variant being restricted

                    var restrictedChecks = restriction.Checks;
                    for (int checkIndex = 0; checkIndex < restrictedChecks.Count; checkIndex++)
                    {
                        if (_randomized.ItemList == null) return GameObjects.Item.ChestPreClocktownDekuNut; // vanilla logic, Preclocktown nut is just for debug text output

                        // TODO: make it random rather than yes/no
                        var check = _randomized.ItemList.Find(item => item.NewLocation != null && item.NewLocation == restrictedChecks[checkIndex]);
                        var itemInCheck = check.Item;
                        //var itemIsNotJunk = (itemInCheck != GameObjects.Item.IceTrap) && (_actorizerDefaultJunkCategories.Contains((GameObjects.ItemCategory)itemInCheck.ItemCategory()) == false);
                        //var itemIsNotJunk = !ItemUtils.IsJunk(itemInCheck);
                        var itemIsNotJunk = !IsActorizerJunk(itemInCheck);
                        if (itemIsNotJunk)
                        {
                            return itemInCheck; // blocked
                        }
                    }

                }
            }

            // special edge cases for actors that would be hard to enum auto because of variants or scenes
            // TODO replace these eventually

            if (testActor == GameObjects.Actor.Tingle)
            {
                // TODO we need to make sure one of them sticks around IF we need the photo
                GameObjects.Item map1;
                GameObjects.Item map2;
                var shortStrawTingle = _randomized.Seed % 3;
                bool strawPulled = false;
                switch (scene.SceneEnum)
                {
                    default:
                    case GameObjects.Scene.NorthClockTown:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapTown).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapWoodfall).Item;
                        strawPulled = shortStrawTingle == 0;
                        break;
                    case GameObjects.Scene.RoadToSouthernSwamp:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapWoodfall).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapSnowhead).Item;
                        strawPulled = shortStrawTingle == 1;
                        break;
                    case GameObjects.Scene.TwinIslands:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapSnowhead).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapRanch).Item;
                        break;
                    case GameObjects.Scene.MilkRoad:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapRanch).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapGreatBay).Item;
                        strawPulled = shortStrawTingle == 2;
                        break;
                    case GameObjects.Scene.GreatBayCoast:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapGreatBay).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapStoneTower).Item;
                        break;
                    case GameObjects.Scene.IkanaCanyon:
                        map1 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapStoneTower).Item;
                        map2 = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemTingleMapTown).Item;
                        break;

                }
                if (!IsActorizerJunk(map1))
                {
                    return map1; // we need to keep this tingle because their items are actual not-junk
                }
                if (!IsActorizerJunk(map2))
                {
                    return map2; // we need to keep this tingle because their items are actual not-junk
                }
                // if heartpiece on picture is required, one of them has to remain regardless of their items
                if (strawPulled && !IsActorizerJunk(GameObjects.Item.HeartPiecePictobox))
                {
                    return GameObjects.Item.HeartPiecePictobox;
                }
            }
            if (testActor == GameObjects.Actor.Postbox)
            {
                GameObjects.Item[] checksPostBoxLeadsTo = {
                    GameObjects.Item.TradeItemMamaLetter,
                    GameObjects.Item.NotebookDeliverPendant,
                    GameObjects.Item.TradeItemPendant,
                    GameObjects.Item.NotebookPromiseKafei,
                    GameObjects.Item.MaskKeaton,
                    GameObjects.Item.HeartPiecePostBox,
                    GameObjects.Item.MaskCouple,
                    GameObjects.Item.NotebookDepositLetterToKafei,
                    GameObjects.Item.NotebookMeetKafei,
                    GameObjects.Item.NotebookCuriosityShopManSGift,
                    GameObjects.Item.NotebookMeetCuriosityShopMan,
                    GameObjects.Item.NotebookPromiseCuriosityShopMan,
                    GameObjects.Item.NotebookUniteAnjuAndKafei
                };
                if (_randomized.ImportantLocations != null && _randomized.ImportantLocations.Union(checksPostBoxLeadsTo).Count() > 0)
                {
                    // if we need a mailbox, keep one
                    var shortStrawPostbox = _randomized.Seed % 3;
                    GameObjects.Scene[] postboxScenes = { GameObjects.Scene.NorthClockTown, GameObjects.Scene.SouthClockTown, GameObjects.Scene.EastClockTown };
                    if (postboxScenes[shortStrawPostbox] == scene.SceneEnum)
                    {
                        return GameObjects.Item.MaskPostmanHat; // to symbolize what is happening only in the debug output
                    }

                }// else: randomize all
            }
            /* // issue: this COMPLETELY ignores bean seller is vanilla and does not show up in sphere list because -- ! ZOEY ! --
            if (testActor == GameObjects.Actor.BeanSeller
                && (_randomized.Settings.LogicMode != Models.LogicMode.NoLogic && _randomized.Settings.LogicMode != Models.LogicMode.Vanilla))
            {
                var freeBeanSample = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemMagicBean).Item;
                if (!IsActorizerJunk(freeBeanSample))
                {
                    return true;
                }
                // instead of checking the checks, we should check the items, in this case is bean important?
                var sphereItems = _randomized.Spheres.SelectMany(u => u).ToList();
                if (sphereItems.Any(u => u.Item1 == "Magic Bean"))
                {
                    return true; // bean is needed somewhere, cannot remove in case this is the requirement
                }
            } // */
            return null;
        }

        public static List<int> GetSceneEnemyObjects(SceneEnemizerData thisSceneData)
        {
            /// Gets all objects in a scene.
            /// this is separate from actor because actors and objects are a different list in the scene/room data
            var scene = thisSceneData.Scene;
            var objList = new List<int>();
            for (var m = 0; m < scene.Maps.Count(); m++)
            {
                var map = scene.Maps[m];
                for (var o = 0; o < map.Objects.Count(); o++)
                {
                    var obj = map.Objects[o];

                    if (objList.Contains(obj)) { continue; } // already known

                    Actor matchingEnemy = thisSceneData.Actors.Find(act => act.ObjectId == obj);
                    if (matchingEnemy == null) continue;

                    GameObjects.Actor matchingEnum = matchingEnemy.ActorEnum;
                    if (matchingEnum > 0                                                         // exists in the list of enemies we want to change
                       && !matchingEnum.ScenesRandomizationExcluded().Contains(scene.SceneEnum)) // not excluded from being extracted from this scene
                    {
                        var replacementChance = matchingEnemy.GetRemovalChance();

                        var importantItem = ObjectIsCheckBlocked(scene, matchingEnum);
                        if (importantItem != null)
                        {
                            #if DEBUG
                            var itemText = $" item [{ importantItem }]";
                            #else
                            var itemText = $" item [{ (int) importantItem}]";
                            #endif

                            thisSceneData.Actors.RemoveAll(act => act.ObjectId == obj);
                            thisSceneData.Log.AppendLine($" object [{matchingEnum}] replacement blocked by" + itemText);
                        }else if (replacementChance != 100
                               && thisSceneData.RNG.Next(100) > replacementChance)
                        {
                            thisSceneData.Actors.RemoveAll(act => act.ObjectId == obj);
                            thisSceneData.Log.AppendLine($" object [{matchingEnum}] replacement blocked by removal chance roll");
                        }
                        else
                        {
                            objList.Add(matchingEnum.ObjectIndex());
                        }
                        // else: ignore, the actors will remain vanilla
                    }
                }
            }
            return objList;
        }

        public static void SetSceneEnemyObjects(Scene scene, List<List<int>> newObjectsPerMap)
        {
            /// tag: write objets, write objects

            for (var m = 0; m < scene.Maps.Count; m++)
            {
                var objectsPerMap = newObjectsPerMap[m];
                var sceneMap = scene.Maps[m];
                for (int sceneObjIndex = 0; sceneObjIndex < objectsPerMap.Count; sceneObjIndex++)
                {
                    sceneMap.Objects[sceneObjIndex] = objectsPerMap[sceneObjIndex];
                }
            }
        }

#endregion

        private static void EnemizerEarlyFixes(Random rng)
        {
            /// Changes before randomization

            FixSpecificLikeLikeTypes();
            FixSpecificTektiteTypes();
            EnableDampeHouseWallMaster();
            EnableTwinIslandsSpringSkullfish();
            FixSouthernSwampDekuBaba(rng);
            FixRoadToSouthernSwampBadBat();
            NudgeFlyingEnemiesForTingle();
            FixScarecrowTalk();
            EnablePoFusenAnywhere();

            FixSpawnLocations();
            DistinguishLogicRequiredDekuFlowers();
            //DisableActorSpawnCutsceneData();

            ExtendGrottoDirectIndexByte();
            ShortenChickenPatience();
            //FixSeth2();
            AllowGuruGuruOutside();
            RemoveSTTUnusedPoe();
            FixSilverIshi();
            FixBabaAndDragonflyShadows();
            AddGrottoVariety();
            ChangeHotwaterGrottoDekuBabaIntoSomethingElse(rng);
            FixCuccoChicks();
            FixWoodfallTempleGekkoMiniboss();
            FixStreamSfxVolume();
            RepositionClockTownActors();
            ExpandGoronShineObjects();
            RandomlySwapOutZoraBandMember();
            ExpandGoronRaceObjects();
            SplitSpiderGrottoSkulltulaObject();
            SplitOceanSpiderhouseSpiderObject();
            FixDekuPalaceReceptionGuards();
            FixBomberKidsGameFinishWarp();
            ModifyAllGraveyardBatsToFly();
            FixInjuredKoume();
            RandomizePinnacleRockSigns();
            RandomizeDekuPalaceBombiwaSigns();
            RandomizeGrottoGossipStonesPerGrotto();
            SwapGreatFairies(rng);
            ModifyFireflyKeeseForPerching();
            SplitPirateSewerMines();
            SplitSnowheadTempleBo();
            BlockBabyGoronIfNoSFXRando();
            FixArmosSpawnPos();
            RandomizeTheSongMonkey();
            MoveTheISTTTunnelTransitionBack();
            FixSwordSchoolPotRandomization();
            SplitSceneSnowballIntoTwoActorObjects();
            SwapIntroSeth();
            SwapPiratesFortressBgBreakwall();
            SwapCreditsCremia();

            EnableAllCreditsCutScenes();

            Shinanigans();

        }

        public static void EnemizerLateFixes()
        {
            /// changes after randomization, actors objects already written, at this point we can detect IF an actor was randomized

            FixKafeiPlacements();
            MoveActorsIfRandomized();
        }

#region Static Enemizer Changes and Fixes

        public static void FixSpawnLocations()
        {
            /// in Enemizer some spawn locations are noticably buggy
            ///   example: one of the eeno in north termina field is too high above the ground, 
            ///    we never notice because it falls to the ground before we can get there normally
            ///    but if its a stationary enemy, like a dekubaba, it hovers in the air

            var terminafieldScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            terminafieldScene.Maps[0].Actors[144].Position.y = -245; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[16].Position.y = -209; // fixes the eeno that is way too high above ground
            terminafieldScene.Maps[0].Actors[17].Position.y = -185; // fixes the eeno that is too high above ground (bombchu explode)
            terminafieldScene.Maps[0].Actors[60].Position.y = -60;  // fixes the blue bubble that is too high
            terminafieldScene.Maps[0].Actors[107].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[110].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[121].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)
            terminafieldScene.Maps[0].Actors[153].Position.y = -280; // fixes the leever spawn is too low (bombchu explode)

            // the south field dekubaba to the east is facing south, because in vanilla its direction does not matter
            // rotate to face out of the field
            var southDekubaba = terminafieldScene.Maps[0].Actors[45];
            southDekubaba.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: southDekubaba.Rotation.y);
            southDekubaba = terminafieldScene.Maps[0].Actors[44];
            southDekubaba.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: southDekubaba.Rotation.y);

            // in STT, move the bombchu in the first room 
            //   backward several feet from the chest, so replacement cannot block the chest
            var stonetowertempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StoneTowerTemple.FileID());
            stonetowertempleScene.Maps[0].Actors[3].Position.z = -630;
            // biobaba in the right room spawns under the bridge, if octarock it pops up through the tile, move to the side of the bridge
            stonetowertempleScene.Maps[3].Actors[19].Position.x = 1530;

            // in WFT, the dinofos spawn is near the roof, lower
            var woodfalltempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WoodfallTemple.FileID());
            woodfalltempleScene.Maps[7].Actors[0].Position.y = -1208;

            // same in secret shrine, all three dinofos are in the air
            var secretShrineScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SecretShrine.FileID());
            secretShrineScene.Maps[2].Actors[0].Position.y = 0;
            secretShrineScene.Maps[2].Actors[1].Position.y = 0;
            secretShrineScene.Maps[2].Actors[2].Position.y = 0;

            // one of the snappers is right in front of the chest, if actorizer, that actor could be something that doesnt have to be killable, could block the chest
            woodfalltempleScene.Maps[6].Actors[1].Position.z = -55; // room 7, z was -25, 

            // in OSH, the storage room bo spawns in the air in front of the mirror, 
            //  but as a land enemy it should be placed on the ground for its replacements
            var oceanspiderhouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.OceanSpiderHouse.FileID());
            var storageroomBo = oceanspiderhouseScene.Maps[5].Actors[2];
            // lower to the floor 
            storageroomBo.Position = new vec16(-726, -118, -1651);

            // in GBT, the bombchus on the pipes are in bad spots to be replaced by something unpassable,
            // but most people dont notice where their original spawn even is so move them
            var greatbaytempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayTemple.FileID());
            // the bombchu along the green pipe in the double seesaw room needs to be moved in case its an unmovable enemy
            greatbaytempleScene.Maps[10].Actors[3].Position.z = 344; // new vec16(3525, -180, 630); // this was hard to open if chest
            // the bombchu along the red pipe in the pre-wart room needs the same kind of moving
            greatbaytempleScene.Maps[6].Actors[7].Position = new vec16(-1840, -570, -870);

            var linkTrialScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LinkTrial.FileID());
            linkTrialScene.Maps[1].Actors[0].Position.y = 1; // up high dinofos spawn, red bubble would spawn in the air, lower to ground

            var piratesFortressCourtyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortress.FileID());
            piratesFortressCourtyardScene.Maps[0].Actors[17].Position.x = 1267;
            piratesFortressCourtyardScene.Maps[0].Actors[17].Position.y = 319;
            piratesFortressCourtyardScene.Maps[0].Actors[20].Position.y = -200; // too high, can cause bombchu to explode

            // in pre-clocktown there is a keaton grass, but it doesn't work because there is no keaton object, but we can fix that
            var beforeClockTownFID = GameObjects.Scene.BeforeThePortalToTermina.FileID();
            var preclocktownScene = RomData.SceneList.Find(scene => scene.File == beforeClockTownFID);
            preclocktownScene.Maps[0].Objects.Add(GameObjects.Actor.Keaton.ObjectIndex());
            var clocktownroomData = RomData.MMFileList[beforeClockTownFID + 1].Data;
            clocktownroomData[0x31] = (byte) preclocktownScene.Maps[0].Objects.Count();

            if (ACTORSENABLED)
            {
                var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
                var torchRotation = dekuPalaceScene.Maps[2].Actors[26].Rotation.z;
                torchRotation = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: torchRotation); // reverse, so replacement isn't nose into the wall

                // change the torch in pirates fort exterior to all day, remove second one, or free 
                var piratesExteriorScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortressExterior.FileID());
                var nightTorch = piratesExteriorScene.Maps[0].Actors[15];
                nightTorch.Rotation.x |= 0x7F; // always spawn flags
                nightTorch.Rotation.z |= 0x7F;

                // day torch
                piratesExteriorScene.Maps[0].Actors[13].ChangeActor(GameObjects.Actor.Empty, modifyOld: true); // dangeon object so no grotto, empty for now
                // todo: 14/16 are also torches, we dont really need both here

                // this torch is too close to spider, constantly actors get stuck, move slightly out of the way
                var swampSpiderHouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SwampSpiderHouse.FileID());
                swampSpiderHouseScene.Maps[3].Actors[3].Position.x = -480;

                // one of the torches in swamp spider needs to be rotated to not face the wall
                var spiderTorch2 = swampSpiderHouseScene.Maps[3].Actors[2];
                spiderTorch2.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 135, flags: spiderTorch2.Rotation.y);

                // actually most of them do
                var spidertorch3 = swampSpiderHouseScene.Maps[5].Actors[1];
                spidertorch3.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180 - 45, flags: spidertorch3.Rotation.y);
                var spidertorch4 = swampSpiderHouseScene.Maps[5].Actors[4];
                spidertorch4.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180 + 45, flags: spidertorch4.Rotation.y);
                var spidertorch5 = swampSpiderHouseScene.Maps[5].Actors[2];
                spidertorch5.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 45, flags: spidertorch5.Rotation.y);
                var spidertorch6 = swampSpiderHouseScene.Maps[5].Actors[3];
                spidertorch6.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180 + 90 + 45, flags: spidertorch6.Rotation.y);


                var dekuPalace = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
                // the torches are really close to the hole, we can spread them wider a bit
                dekuPalace.Maps[1].Actors[26].Position.z -= 10; // left
                dekuPalace.Maps[1].Actors[28].Position.z -= 10; // left
                dekuPalace.Maps[1].Actors[27].Position.z += 10; // right
                dekuPalace.Maps[1].Actors[25].Position.z += 10; // right
                // deku bean torches north, rotate 
                dekuPalace.Maps[1].Actors[25].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[1].Actors[25].Rotation.y);
                dekuPalace.Maps[1].Actors[26].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[1].Actors[26].Rotation.y);
                dekuPalace.Maps[1].Actors[27].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[1].Actors[27].Rotation.y);
                dekuPalace.Maps[1].Actors[28].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[1].Actors[28].Rotation.y);

                // west side hp torches face... north? turn them to face the player
                dekuPalace.Maps[2].Actors[33].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: dekuPalace.Maps[2].Actors[33].Rotation.y);
                dekuPalace.Maps[2].Actors[34].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: dekuPalace.Maps[2].Actors[34].Rotation.y);
                // green rup torches face north as well
                dekuPalace.Maps[2].Actors[29].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[2].Actors[29].Rotation.y);
                dekuPalace.Maps[2].Actors[30].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[2].Actors[30].Rotation.y);
                dekuPalace.Maps[2].Actors[31].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[2].Actors[31].Rotation.y);
                dekuPalace.Maps[2].Actors[32].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: dekuPalace.Maps[2].Actors[32].Rotation.y);

                // Jim the bomber actually spawns within the tree to the north... move is spawn over a bit
                var northClockTown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.NorthClockTown.FileID());
                var jimDuringTheGame = northClockTown.Maps[0].Actors[26];
                jimDuringTheGame.Position.x = -740;
                jimDuringTheGame.Position.z = -1790;
                // and rotate to face outwards not toward the wall
                jimDuringTheGame.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: (180 - 20), flags: jimDuringTheGame.Rotation.y);

                // the tree itself needs to be rotated as its facing the wall
                northClockTown.Maps[0].Actors[21].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 135, northClockTown.Maps[0].Actors[21].Rotation.y);

                // jimbo in east clock town giving you the book is in an odd spot, move to the poster
                var eastClockTown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
                eastClockTown.Maps[0].Actors[46].Position = new vec16(1335, 203, -1639);

                // the "trees" in trading post including bushes are in weird places, move them around the fire and the table
                var tradingPost = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TradingPost.FileID());
                tradingPost.Maps[0].Actors[2].Position = new vec16(-189, 3, 76); // first right bush
                tradingPost.Maps[0].Actors[2].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 80, flags: tradingPost.Maps[0].Actors[2].Rotation.y);

                tradingPost.Maps[0].Actors[5].Position = new vec16(120, 27, -81); // next to table to fish case
                tradingPost.Maps[0].Actors[5].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: tradingPost.Maps[0].Actors[2].Rotation.y);

                // behind table should be facing table
                tradingPost.Maps[0].Actors[4].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 210, flags: tradingPost.Maps[0].Actors[2].Rotation.y);

                var tradingPostPot = tradingPost.Maps[0].Actors[8];
                tradingPostPot.Rotation.y = ActorUtils.MergeRotationAndFlags(270, tradingPostPot.Rotation.y); // rotate right toward player away from front wall

                // we cannot randomize gorman brothers without randomizing their chasing horse counterparts
                // except, this scene has an almost unused object: kanban, for the square sign you can only access if you go through the second fence
                // what if we turn that into the same actor as the tree, and turn the second object into a second ingo
                var gormanTrack = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GormanRaceTrack.FileID());
                gormanTrack.Maps[0].Objects[11] = GameObjects.Actor.GormanBros.ObjectIndex();
                gormanTrack.Maps[0].Actors[75].ChangeActor(GameObjects.Actor.Treee, vars: 0xFF02, modifyOld: true);

                // sakon in the curiosity shop can block the door, which must be avoided
                var curiosityShop = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.CuriosityShop.FileID());
                var sutari = curiosityShop.Maps[0].Actors[1];
                sutari.Position = new vec16(51, 3, -17); // move over to the side of the talking grate
                sutari.Rotation.y = ActorUtils.MergeRotationAndFlags(90 + 15, sutari.Rotation.y);

                // laundrypool wooden box is facing into the wall
                var laundrypoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LaundryPool.FileID());
                var woodenBox = laundrypoolScene.Maps[0].Actors[7];
                woodenBox.Rotation.y = ActorUtils.MergeRotationAndFlags(180, woodenBox.Rotation.y);

                var mayorsResitenceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MayorsResidence.FileID());
                var gormanInResidence = mayorsResitenceScene.Maps[0].Actors[1];
                gormanInResidence.Position = new vec16(77, 15, 148);
                gormanInResidence.Rotation.y = ActorUtils.MergeRotationAndFlags(180 + 90, gormanInResidence.Rotation.y);

                // this one is facing the door which is odd, turn to face madam
                gormanInResidence = mayorsResitenceScene.Maps[2].Actors[1];
                gormanInResidence.Rotation.y = ActorUtils.MergeRotationAndFlags(180 + 45, gormanInResidence.Rotation.y);

                // bombers hideout torch is facing a funny way
                var bombersHideoutScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.AstralObservatory.FileID());
                var blastWallTorch = bombersHideoutScene.Maps[0].Actors[15];
                blastWallTorch.Rotation.y = ActorUtils.MergeRotationAndFlags(270, blastWallTorch.Rotation.y); // face the bombable wall
                // and move a bit away from the far wall
                blastWallTorch.Position.z -= 40;

                // the gibdos in ikana canyon, two of them are basically on top of each other can lead to weird shinanigans
                var ikanaCanyonScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCanyon.FileID());
                var doubledGibdo = ikanaCanyonScene.Maps[0].Actors[64];
                doubledGibdo.Position = new vec16(-602, 400, 972);

                var milkbarScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
                milkbarScene.Maps[0].Objects[10] = GameObjects.Actor.ArcheryMiniGameMan.ObjectIndex();

                // the ceiling water drip effect actor was placed too close to the door, can softlock if it knocks the player away (skulltula)
                var underGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.BeneathGraveyard.FileID());
                underGraveyardScene.Maps[0].Actors[1].Position.x = 20; // facing door from hole, move back toward door
                underGraveyardScene.Maps[0].Actors[1].Position.z = 251; // facing door from hole, move left toward day 2

                // in blacksmith building, there are two pots that need to be rotated
                var mountainSmithyScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainSmithy.FileID());
                var leftSmithyPot = mountainSmithyScene.Maps[0].Actors[4];
                leftSmithyPot.Rotation.y = ActorUtils.MergeRotationAndFlags(180, leftSmithyPot.Rotation.y); ;
                var rightSmithyPot = mountainSmithyScene.Maps[0].Actors[8];
                rightSmithyPot.Rotation.y = ActorUtils.MergeRotationAndFlags(180, rightSmithyPot.Rotation.y);
                rightSmithyPot.Position.x = -70;
                rightSmithyPot.Position.z = 288;

                var mountainVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillage.FileID());
                var leftMountainVillagePot = mountainVillageScene.Maps[0].Actors[35];
                leftMountainVillagePot.Rotation.y = ActorUtils.MergeRotationAndFlags(270, leftSmithyPot.Rotation.y); ;
                var rightMountainPot = mountainVillageScene.Maps[0].Actors[36];
                rightMountainPot.Rotation.y = ActorUtils.MergeRotationAndFlags(270, rightMountainPot.Rotation.y);

                RotateTalkSpotsAndHitSpots();

                // trying to fix clock, nothing
                //var curiosityShopClock = curiosityShop.Maps[0].Actors[5];
                //curiosityShopClock.Position.x = -130;

                // in spring there are two torches on top of each other, which is weird, move the other one to face the first one
                //var mountainVillageSpring = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillageSpring.FileID());
                //var secondTorch = mountainVillageSpring.Maps[0].Actors[13];
                //secondTorch.Rotation.y = ActorUtils.MergeRotationAndFlags(180, secondTorch.Rotation.y);
                //secondTorch.Position.z -= 50;

            }
        }

        private static void RotateTalkSpotsAndHitSpots()
        {
            // lots of talk spots and hit spots have no rotation and need to be adjusted or they are half stuck in the wall weirdly

            var stockpotInnScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.StockPotInn.FileID());
            var stockpotinnmaskHitSpot = stockpotInnScene.Maps[0].Actors[14];
            ActorUtils.ClearActorRotationRestrictions(stockpotinnmaskHitSpot);
            stockpotinnmaskHitSpot.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: stockpotinnmaskHitSpot.Rotation.y);

            // if the clocktown talk points are randomized, we want to rotate them as they dont have set rotation
            // this shit does nothing because something funky is going on, the rotation is not what it is in vanilla and its being ignored????
            var westClocktownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WestClockTown.FileID());
            var curiosityshopSign = westClocktownScene.Maps[0].Actors[22];
            curiosityshopSign.ChangeYRotation(180 - 27);
            ActorUtils.ClearActorRotationRestrictions(curiosityshopSign);
            var tradingpostSign = westClocktownScene.Maps[0].Actors[9];
            tradingpostSign.Rotation.y = ActorUtils.MergeRotationAndFlags(180 - 45 , flags: tradingpostSign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(tradingpostSign);
            var bombshopSign = westClocktownScene.Maps[0].Actors[2];
            bombshopSign.Rotation.y = ActorUtils.MergeRotationAndFlags(180 - 71, flags: bombshopSign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(bombshopSign);
            bombshopSign.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING
            var lotterySign = westClocktownScene.Maps[0].Actors[25];
            lotterySign.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: lotterySign.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(lotterySign);

            var eastClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());

            var treasurePoster = eastClockTownScene.Maps[0].Actors[20];
            treasurePoster.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: treasurePoster.Rotation.y);
            treasurePoster.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: treasurePoster.Rotation.x);
            ActorUtils.ClearActorRotationRestrictions(treasurePoster);
            var constructionPoster = eastClockTownScene.Maps[0].Actors[17];
            constructionPoster.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: constructionPoster.Rotation.y);
            constructionPoster.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: constructionPoster.Rotation.x);
            ActorUtils.ClearActorRotationRestrictions(constructionPoster);
            var zoraPoster1 = eastClockTownScene.Maps[0].Actors[14];
            zoraPoster1.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: zoraPoster1.Rotation.x);
            zoraPoster1.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zoraPoster1.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster1);
            var zoraPoster2 = eastClockTownScene.Maps[0].Actors[15];
            zoraPoster2.Rotation.x = ActorUtils.MergeRotationAndFlags(0, flags: zoraPoster1.Rotation.x);
            zoraPoster2.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zoraPoster2.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster2);
            var zoraPoster3 = eastClockTownScene.Maps[0].Actors[16];
            zoraPoster3.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: zoraPoster3.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(zoraPoster3);

            var hitspotLeft = eastClockTownScene.Maps[0].Actors[42];
            hitspotLeft.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: hitspotLeft.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(hitspotLeft);
            hitspotLeft.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING

            var hitspotRight = eastClockTownScene.Maps[0].Actors[43];
            hitspotRight.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: hitspotRight.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(hitspotRight);
            hitspotRight.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING

            var basketSpot = eastClockTownScene.Maps[0].Actors[22];
            basketSpot.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: basketSpot.Rotation.y);
            ActorUtils.ClearActorRotationRestrictions(basketSpot);
            basketSpot.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING

            var archerySign = eastClockTownScene.Maps[0].Actors[24];
            archerySign.ChangeYRotation(270 - 45);
            archerySign.ChangeXRotation(0);
            ActorUtils.ClearActorRotationRestrictions(archerySign);
            archerySign.ChangeActor(GameObjects.Actor.Clock, vars: 0x907F); // DEBUGGING

        }


        private static void EnableAllFormItems()
        {
            /// let deku nut

            const int FORM_FD = 0; // let me use enum as int without a cast and I'll use it
            const int FORM_GORON = 1;
            //const int FORM_ZORA  = 2;
            const int FORM_DEKU = 3;
            //const int FORM_CHILD = 4;


            var codeFile = RomData.MMFileList[31].Data;
            var startLoc = 0x11C950; // offset to gPlayerFormItemRestrictions
            var endLoc = 0x11CB90; // this is wrong, includes some padding
            var formDataWidth = 0x72; // item bytes per form (yes each restriction is a byte not a bit, what a waste...)

            // start by enable everything
            var i = startLoc;
            while (i < endLoc)
            {
                // gPlayerFormItemRestrictions[GET_PLAYER_FORM][GET_CUR_FORM_BTN_ITEM(i)] // /* 11C950 801C2410 */
                // item enum: ItemId
                codeFile[i] = 0xFF; // this is overkill, it can be any value over 1, but this helps with visiblity
                i++;
            }

            // however there are some that are broken/bugged and should never be used
            for (int form = 0; form < 4; form++) // dont overwrite regular link which is form 5
            {
                // hookshot item is 0xF ( _can_ crash, cause unknown, pj64 doesnt crash so I cant even debug it)
                codeFile[startLoc + (form * formDataWidth) + 0xF] = 0x00;
                // bow item is 0x0 (buggy behavior that isn't useful)
                codeFile[startLoc + (form * formDataWidth) + 0x1] = 0x00;
                // elemental arrows are different items
                codeFile[startLoc + (form * formDataWidth) + 0x4A] = 0x00; // 2 3 and 4 arent valid here for some reason
                codeFile[startLoc + (form * formDataWidth) + 0x4B] = 0x00;
                codeFile[startLoc + (form * formDataWidth) + 0x4C] = 0x00;
            }

            // disable goron stick (he just punches which is counter int)
            codeFile[startLoc + (FORM_GORON * formDataWidth) + 0x8] = 0x00;

            // FD cannot use bow or stick
            codeFile[startLoc + (FORM_FD * formDataWidth) + 0x1] = 0x00;
            codeFile[startLoc + (FORM_FD * formDataWidth) + 0x8] = 0x00;

            // Dekulink can lock up if he gets a recoil while using sword/stick
            codeFile[startLoc + (FORM_DEKU * formDataWidth) + 0x8] = 0x00;
            codeFile[startLoc + (FORM_DEKU * formDataWidth) + 0x10] = 0x00;
        }


        private static void Shinanigans()
        {
            // the peahat grass drops NOTHING, this has bothered me for ages, here I change it
            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            grottosScene.Maps[13].Actors[1].ChangeActor(GameObjects.Actor.NaturalPatchOfGrass, vars: 0x1, modifyOld: true);

            if (ACTORSENABLED)
            {
                //turn around this torch, because if its bean man hes facing into the wall and it hurts me
                var laundryPoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.LaundryPool.FileID());
                var laundryPoolTorch = laundryPoolScene.Maps[0].Actors[2];
                laundryPoolTorch.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 135, flags: 0x7F);
                laundryPoolTorch.Rotation.x = 0x7F;
                laundryPoolTorch.Rotation.z = 0x7F;
                //laundryPoolScene.Maps[0].Actors[1].Rotation.z = ActorUtils.MergeRotationAndFlags(rotation: laundryPoolScene.Maps[0].Actors[1].Rotation.z, flags: 0x7F);

                // it was two torches, turn the other into a secret grotto, at least for now
                var randomGrotto = new List<ushort> { 0x6033, 0x603B, 0x6018, 0x605C, 0x8000, 0xA000, 0x7000, 0xC000, 0xE000, 0xF000, 0xD000 };
                var hiddenGrottos = new List<ushort> { 0x6233, 0x623B, 0x6218, 0x625C, 0x8200, 0xA200, 0x7200, 0xC200, 0xE200, 0xF200, 0xD200 };
                laundryPoolScene.Maps[0].Actors[1].ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[seedrng.Next(randomGrotto.Count)], modifyOld: true);
                laundryPoolScene.Maps[0].Actors[1].Rotation = new vec16(0x7F, 0x7F, 0x7F);
                laundryPoolScene.Maps[0].Actors[1].Position = new vec16(-1502, 35, 555); // old: new vec16(-1872, -120, 229);

                // winter village has a gossip stone actor, but no object, lets use the non-used flying darmani ghost object and add it to enemizer
                var winterVillage = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MountainVillage.FileID());
                winterVillage.Maps[0].Objects[5] = GameObjects.Actor.GossipStone.ObjectIndex();
                winterVillage.Maps[0].Actors[57].Variants[0] = 0x67; // the vars is for milkroad, change to a moon vars so it gets randomized
                winterVillage.Maps[0].Actors[57].Position.y = -15; // floating a bit in the air, lower to ground
                // note: if we need to add the ghost back in, the scene is using 13 objects so we can add one more back in

                // now that darmani ghost is gone, lets re=use the actor for secret grotto
                var newGrotto = winterVillage.Maps[0].Actors[2];
                newGrotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: randomGrotto[seedrng.Next(randomGrotto.Count)] & 0xFCFF, modifyOld: true);
                newGrotto.Position = new vec16(504, 365, 800);

                var terminafieldScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
                var elf6grotto = terminafieldScene.Maps[0].Actors[2];
                elf6grotto.Position = new vec16(-5539, -275, -701);
                elf6grotto.ChangeActor(GameObjects.Actor.GrottoHole, vars: hiddenGrottos[seedrng.Next(hiddenGrottos.Count)], modifyOld: true);

                // one of the torches in palace is facing into the wall, actors replacing it also face the same way, bad
                // one of these is not required and does nothing
                var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
                dekuPalaceScene.Maps[2].Actors[25].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: 0x7F);
                dekuPalaceScene.Maps[2].Actors[26].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: dekuPalaceScene.Maps[2].Actors[26].Rotation.y);

                // goron underwater mode
                // this has been superseeded by Zoeys underwater code instead, which should be better
                //var playerFile = RomData.MMFileList[GameObjects.Actor.Player.FileListIndex()].Data;
                // changes made to function func_8083BB4C
                //ReadWriteUtils.Arr_WriteU32(playerFile, Dest: 0xE20C, val: 0x00000000); // 80834140 -> NOP
                //ReadWriteUtils.Arr_WriteU32(playerFile, Dest: 0xE214, val: 0x00000000); // 
                //ReadWriteUtils.Arr_WriteU32(playerFile, Dest: 0xE220, val: 0x00000000); //

                if (seedrng.Next() % 10 >= 5)
                {
                    // I like secrets
                    var twinislandsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslands.FileID());
                    //twinislandsScene.Maps[0].Actors[1].Position = new vec16(-583, 140, -20); // place: next to tree, testing
                    twinislandsScene.Maps[0].Actors[1].Position = new vec16(349, -196, 970); // place: under the ice, sneaky like teh crabb
                                                                                             //twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x60CB; // set to unk check
                                                                                             // 300 is back to mountain village
                                                                                             // 303 is empty, it takes us to mayors office, which might mean we can put an address tehre 
                    twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x0303; // set to spring goron race?
                                                                             //twinislandsScene.Maps[0].Actors[1].Variants[0] = 0x7200; // invisible

                    // spring has ONE exit, which means pad space is free realestate
                    RomUtils.CheckCompressed(GameObjects.Scene.TwinIslands.FileID());
                    var twinislandsSceneData = RomData.MMFileList[GameObjects.Scene.TwinIslands.FileID()].Data;
                    twinislandsSceneData[0xD6] = 0xAE;
                    twinislandsSceneData[0xD7] = 0x50; // 50 is behind the waterfall 
                }

                EnableAllFormItems();

                // */
                // RecreateFishing();

                // can we just boost the dynapoly memory size?
                // data locations:
                // default 23000 is an ORI at 3da8, a4 for tope byte
                // IsSmallMemScene is F000 at 3d58
                // termina field is in data at sSceneMemList, not sure exact space
                //ReadWriteUtils.Arr_WriteU32(codeFile, 0x3DA8, 0x2);
                /*
                List<Actor> sorted = new List<Actor>();
                foreach (var actor in Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>())
                {
                    sorted.Add(new Actor(actor));
                }
                foreach ( var a in sorted.OrderBy(u => u.ObjectSize))
                {
                    Debug.WriteLine($"Actor {a.Name} has object size: {a.ObjectSize.ToString("X6")}");
                }
                int i = 4; */

            }

            // attempt faster breman march, testing
            //glabel D_8085E5A0
            // 030B10 8085E5A0 3ECCCCCD  .float 0.4
            RomUtils.CheckCompressed(38);
            var playerCodeFile = RomData.MMFileList[38].Data;
            // 0x40000000, 
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x030B10, val: 0x3FF33333); // change to 1.9, almost double running speed

            // what if all minor hats were as fast as bunny?
            // except without adding code we can only modify one line of code
            //  if (this->currentMask == PLAYER_MASK_BUNNY) {speedTarget *= 1.5f;
            // the closest I can think of is & 0xF which gets most but not all of them, which does shuffle some code around tho
            // 0x1D59C ofset == 0xCC5490 hard romaddr
            /*
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D59C, val: 0xC7A4002C);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A0, val: 0x3C013FC0);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A4, val: 0x3319000F);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5A8, val: 0x13200005);
            ReadWriteUtils.Arr_WriteU32(playerCodeFile, Dest: 0x1D5AC, val: 0x3C08801F);

            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5C0, val: 0x8D08);
            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5CC, val: 0x8509);
            ReadWriteUtils.Arr_WriteU16(playerCodeFile, Dest: 0x1D5D8, val: 0x4489);
            */

            // can we remove an object from ikana to increase object budget to have more stuff?
            var ikanaScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaCanyon.FileID());
            // nobody follows kafei its fine to remove his object from the main room
            ikanaScene.Maps[0].Objects[10] = SMALLEST_OBJ; // kafei
            ikanaScene.Maps[0].Objects[13] = SMALLEST_OBJ; // piece of heart, used in the east side but not here, we dont need here
            ikanaScene.Maps[0].Objects[18] = SMALLEST_OBJ; // flying scrub ( dont think it matters remove it from this area for most people)

            // if we remove the woodfall object from terminafield, we have more space for noticible actors and not a static backdrop woodfall
            // so far this has been here over a month and nobody has noticed I removed woodfall lol
            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            tfScene.Maps[0].Objects[0] = SMALLEST_OBJ;

            // HEAVY BOMB
            // you idiot, this is both kegs and regular bombs, you need to set the flag for just kegs with a code change or bombs are heavy too
            //RomUtils.CheckCompressed(GameObjects.Actor.PowderKeg.FileListIndex());
            //var kegFile = RomData.MMFileList[GameObjects.Actor.PowderKeg.FileListIndex()].Data;
            //kegFile[0x1FF5] |= 0x02; // add ACTOR_FLAG_20000, makes it heavy 

            // regular po cannot be hit by zora lightning, but can take arrow damage? this feelslike an oversight
            RomUtils.CheckCompressed(GameObjects.Actor.Poe.FileListIndex());
            var pohData = RomData.MMFileList[GameObjects.Actor.Poe.FileListIndex()].Data;
            pohData[0x3003] = 0x2;

            // to pointerize milk bar we have to change the obj_sound actor in themilkbar
            //SequenceUtils.ConvertSequenceSlotToPointer(seqSlotIndex: 0x56, substituteSlotIndex:0x1F, "mm-milk-bar-pointer"); // house
            // TODO is this even doing anything anymore? I thought I had to do all of this in music rando code now
            //var milkbarScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
            //milkbarScene.Maps[0].Actors[17].Variants[0] = 0x13C; // from 0x156, the pointer, to 3C the actual milkbar song

            // dinofos deserves a red dot on the minimap
            RomUtils.CheckCompressed(GameObjects.Actor.Dinofos.FileListIndex());
            var dinofosData = RomData.MMFileList[GameObjects.Actor.Dinofos.FileListIndex()].Data;
            dinofosData[0x3A74] |= 0x80; // set the 0x80000000 actor flag to enabled red dot on the minimap

            // bigpo deserves a red dot on the minimap
            RomUtils.CheckCompressed(GameObjects.Actor.BigPoe.FileListIndex());
            var biopoData = RomData.MMFileList[GameObjects.Actor.BigPoe.FileListIndex()].Data;
            biopoData[0x3A14] |= 0x80; // set the 0x80000000 actor flag to enabled red dot on the minimap


            //LightShinanigans();

            //PrintActorValues();
        }

        public static void LightShinanigans()
        {
            // tales of light and fox
            var scenesAsFiles = new List<Scene>();

            // grab all scenes
            for (int fileId = 0; fileId < RomData.MMFileList.Count; fileId++)
            {
                var search = RomData.SceneList.Find(scene => scene.File == fileId);
                if (search != null)
                {
                    scenesAsFiles.Add(search);
                }
            }

            // assumption: this is after enemizer so all scenes are decompressed already

            foreach (var scene in scenesAsFiles)
            {
                // for each scene change something 
                // scenes have LightSettings[] which are type EnvLightSettings[]
                // 0x0F  u8 fogColor[3];
                // 0x12 s16 fogNear; // ranges from 0-1000 (0: starts immediately, 1000: no fog), but is clamped to ENV_FOGNEAR_MAX
                // 0x14 s16 zFar; // Max depth (render distance) of the view as a whole. fogFar will always match zFar
                // except unless the scene list is a list per-room, which is odd we have room files, this shouldnt explain the dark room
                // there are two lights in the env light list that have a fog color of zero
                // 1 {  0x00, 0x00, 0x00, 0x45, 0x45, 0x45, 0x00, 0x00, 0x00, 0xBB, 0xBB, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                // 0x07E6, 0x1A90 },

                // 2 {0x00, 0x00, 0x00, 0x45, 0x45, 0x45, 0x00, 0x00, 0x00, 0xBB, 0xBB, 0xBB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                //0x0FE6, 0x1A90 },


                var sceneFid = scene.File;
                var sceneData = RomData.MMFileList[sceneFid].Data;
                var roomCount = 0;
                // scan scene header for values
                for (int i = 0; i < sceneData.Length; i += 0x8)
                {
                    // TODO abstract function that scans headers for an offset value as I keep making this shit
                    if (sceneData[i] == 0x14) break; // end of headers

                    if (sceneData[i] == 0x4) // room data, get room count
                        roomCount = sceneData[i + 1];

                    if (sceneData[i] == 0xF) // env light settings
                    {
                        // 16 each
                        var envLightCount = sceneData[i + 1];
                        var envLightCountOffset = ReadWriteUtils.Arr_ReadU32(sceneData, i + 4) & 0x00FFFFFF; // segment offset, dont need the 0x02
                        for (int light = 0; light < envLightCount; light++)
                        {
                            var offset = envLightCountOffset + (light * 0x16);
                            // start by changing the fog color and intesity make sure this shit is working
                            // AA 15 00 // and orange redish color that seems like glover
                            // 1 {  0x00, 0x00, 0x00, 0x45,
                            // 0x45, 0x45, 0x00, 0x00,
                            // 0x00, 0xBB, 0xBB, 0xBB,
                            // 0x00, 0x00, 0x00, 0x00,
                            // 0x00, 0x00,
                            // 0x07E6, 0x1A90 },

                            // write the exact same light as darkroom
                            // its dark but tatl alone is not enough to brighten it up, and the far fog is waaaay too low
                            // also need to fix the skybox during day, maybe fix tatl so she stays out near your head
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset, 0x00000045);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 4, 0x45454500);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 8, 0x00BBBBBB);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 0xC, 0x00000000);
                            ReadWriteUtils.Arr_WriteU32(sceneData, (int)offset + 0x10, 0x000007E6);
                            ReadWriteUtils.Arr_WriteU16(sceneData, (int)offset + 0x14, 0x1A90);

                            /*sceneData[offset + 0xF + 0] = 0; 
                            sceneData[offset + 0xF + 1] = 0;
                            sceneData[offset + 0xF + 2] = 0;

                            sceneData[offset + 0x12] = 0x12; // "fognear"
                            sceneData[offset + 0x13] = 0x12;

                            sceneData[offset + 0x14] = 0x66; // "zfar"
                            sceneData[offset + 0x15] = 0x66; */

                        }
                    }
                }

                // for each room in scene change those too
                // there arent any in room 09, there is room behavior but not room light
                // SCENE_CMD_ROOM_BEHAVIOR(curRoomUnk3, curRoomUnk2, curRoomUnk5, msgCtxunk12044, enablePosLights, kankyoContextUnkE2)
                // SCENE_CMD_ROOM_BEHAVIOR(0x01, 0x00, 0, 0, true, 0), // actually used in room 09
                for (int roomNum = 0; roomNum < roomCount; roomNum++)
                {
                    var roomData = RomData.MMFileList[sceneFid + roomNum].Data;
                    for (int i = 0; i < roomData.Length; i += 0x8)
                    {
                        // TODO abstract function that scans headers for an offset value as I keep making this shit
                        if (roomData[i] == 0x14) break; // end of headers

                        if (roomData[i] == 0x8)
                        {
                            // set point lights for all
                            roomData[i + 6] |= 0x8;

                            // set room behavior to match the dark room in woodfall
                            roomData[i + 1] = 0x01; // flag 1
                            roomData[i + 7] = 0x00; // flag 2 & 0xFF
                            break;
                        }
                    }
                }

            }
        }

        private static void MoveShopScrubsIfRandomized()
        {
            /// if we randomize the shop scrubs, then we have two of them sitting on top of each other, which is weird
            var southernSwamp = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());
            var swampScrub = southernSwamp.Maps[0].Actors[1]; // first is zero
            if (swampScrub.Name != GameObjects.Actor.BuisnessScrub.ToString())
            {
                var stationaryScrub = southernSwamp.Maps[0].Actors[0]; // needs to be rotated, naturally faces left down the swamp
                stationaryScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: stationaryScrub.Rotation.y);

                swampScrub.Position = new vec16(115, 170, 26);
                SceneUtils.UpdateScene(southernSwamp);
            }
            // TODO cleared swamp

            var goronvillage = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronVillage.FileID());
            var gvScrub = goronvillage.Maps[0].Actors[4]; // first is 3
            if (gvScrub.ActorEnum != GameObjects.Actor.BuisnessScrub)
            {
                gvScrub.Position = new vec16(168, -200, 400);
                gvScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(0, flags: gvScrub.Rotation.y); // turn back around to face the other guy
                SceneUtils.UpdateScene(goronvillage);
            }

            var zoraHallrooms = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHallRooms.FileID());
            var zorahallScrub = zoraHallrooms.Maps[2].Actors[1]; // first is zero
            if (zorahallScrub.ActorEnum != GameObjects.Actor.BuisnessScrub)
            {
                var stationaryScrub = zoraHallrooms.Maps[2].Actors[0]; // needs to be rotated, naturally faces the door
                stationaryScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: stationaryScrub.Rotation.y);

                zorahallScrub.Position = new vec16(-2113, 40, -71);
                zorahallScrub.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: zorahallScrub.Rotation.y);
                SceneUtils.UpdateScene(zoraHallrooms);
            }

        }

        private static void MovePostmanIfRandomized(Scene terminaField)
        {

            var westclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WestClockTown.FileID());
            var westclocktownPostMan = westclocktown.Maps[0].Actors[18];
            if (westclocktownPostMan.ActorEnum != GameObjects.Actor.PostMan)
            {
                westclocktownPostMan.Position = new vec16(-1523, 200, -1376); // move outside of the door
                SceneUtils.UpdateScene(westclocktown);

            }

            var eastclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
            var eastclocktownPostman = eastclocktown.Maps[0].Actors[12];
            if (eastclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                eastclocktownPostman.Position = new vec16(1150, 200, -1405); // move outside of the door
                // rot zero faces mostly to the east wall and a touch south, turn to face mayors
                eastclocktownPostman.Rotation.y = ActorUtils.MergeRotationAndFlags(90 + 45 + 30, flags: eastclocktownPostman.Rotation.y);
                SceneUtils.UpdateScene(eastclocktown);
            }

            var southclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var southclocktownPostman = southclocktown.Maps[0].Actors[6];
            if (southclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                southclocktownPostman.Position = new vec16(-1548, 200, -1097); // move into the visible
                SceneUtils.UpdateScene(southclocktown);
            }

            var northclocktown = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.NorthClockTown.FileID());
            var northclocktownPostman = northclocktown.Maps[0].Actors[20];
            if (northclocktownPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                northclocktownPostman.Position = new vec16(-31, 205, -1883); // move into the visible
                SceneUtils.UpdateScene(northclocktown);
            }

            // milkbar
            var milkbar = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.MilkBar.FileID());
            var milkbarPostman = milkbar.Maps[0].Actors[12];
            if (milkbarPostman.ActorEnum != GameObjects.Actor.PostMan)
            {
                milkbarPostman.Position = new vec16(55, 2, -172); // move next to the bar
                // his time flags are to be there on all days, but this might be confusing? do we reduce to night 3 and day0/4?
                milkbarPostman.Rotation.x &= ~1; // remove the day 1 day spawn flag
                milkbarPostman.Rotation.z &= ~0x78; // remove the day 1 night flag, and all of day 2 flags, and day 3 day flag, but not day 4 or day 3 night flags
                // turn slightly right to face bar
                milkbarPostman.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: milkbarPostman.Rotation.y);

                SceneUtils.UpdateScene(milkbar);
            }

            var milkbarGorman = milkbar.Maps[0].Actors[12];
            if (milkbarGorman.ActorEnum != GameObjects.Actor.PostMan)
            {
                milkbarGorman.Position = new vec16(57, 2, -87); // move next to the bar
                // turn slightly right to face bar
                milkbarGorman.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: milkbarGorman.Rotation.y);

                SceneUtils.UpdateScene(milkbar);
            }

        }

        public static void MoveActorsIfRandomized()
        {

            /// if ossan in trading post was randomized we want to move one of them, as there are two of the, assumed for late night
            var tradingpostScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TradingPost.FileID());
            var secondOssan = tradingpostScene.Maps[0].Actors[1];
            if (secondOssan.ActorEnum != GameObjects.Actor.TradingPostShop)
            {
                secondOssan.Position = new vec16(-35, 25, -154);
                SceneUtils.UpdateScene(tradingpostScene);
            }

            // if we randomize the bombiwa in the swamp spiderhouse, replacements with colliders can block bugs
            // for now, decided to just un-randomize

            // if we randomize cremia in the branch, the uma cart can crash, we need to change its type from ranch to termina field
            var romaniRanchScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RomaniRanch.FileID());
            var cremia = romaniRanchScene.Maps[0].Actors[2];
            if (cremia.ActorEnum != GameObjects.Actor.Cremia)
            {
                var cariageHorse = romaniRanchScene.Maps[0].Actors[34];
                //cariageHorse.Variants[0] = 0x0; // same as termina field, which doesnt have cremia on it
                //cariageHorse.ChangeActor(GameObjects.Actor.Dog, vars: 0x3FF); // this DOES NOTHING its too late the actor has already been written idiot
                var ranchRoom0Data = RomData.MMFileList[GameObjects.Scene.RomaniRanch.FileID() + 1].Data; // 1327
                //have to erase this actor directly
                ranchRoom0Data[0x2A4] = 0xFF; // this works, although would be cool if we could just change type
                ranchRoom0Data[0x2A5] = 0xFF;
                //ranchRoom0Data[0x2B2] = 0x0; // attempted change of variant type to zero, this does not work, best to remove the whole actor for now
                //ranchRoom0Data[0x2B3] = 0x0;

                // now that the cariage is gone we should try to remove the objects to make space for other things in the scene
                ReadWriteUtils.Arr_WriteU16(ranchRoom0Data, 0x74, SMALLEST_OBJ); // carriage
                ReadWriteUtils.Arr_WriteU16(ranchRoom0Data, 0x72, SMALLEST_OBJ); // object_ha is the donkey the cart uses
            }

            var terminaField = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            var terminaFieldScopeNuts = terminaField.Maps[0].Actors[210]; // buisness scrub
            if (terminaFieldScopeNuts.ActorEnum != GameObjects.Actor.FlyingFieldScrub)
            {
                terminaFieldScopeNuts.Position = new vec16(780, 760, 615); // move closer to the edge of ect so the player can see it
            }

            var terminaFieldWestGossipBombiwa = terminaField.Maps[0].Actors[198];
            if (terminaFieldWestGossipBombiwa.ActorEnum != GameObjects.Actor.Bombiwa) // assumption: currently both have to be randomized at the same time
            {
                terminaFieldWestGossipBombiwa.Position.z = -1727; // move back from sitting right on top of the grotto
                terminaField.Maps[0].Actors[199].Position.z = -642; // move back from sitting right on top of the grotto
            }
            SceneUtils.UpdateScene(terminaField);

            var roadToIkanaCanyonScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RoadToIkana.FileID());
            var roadToIkanaRedHamishi = roadToIkanaCanyonScene.Maps[0].Actors[5];
            if (roadToIkanaRedHamishi.ActorEnum != GameObjects.Actor.BronzeBoulder) // assumption: currently both have to be randomized at the same time
            {
                roadToIkanaRedHamishi.Position.z = -413; // move back from sitting right on top of the grotto
                // TODO change rotation?
            }
            SceneUtils.UpdateScene(roadToIkanaCanyonScene);

            var capeScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraCape.FileID());
            var capeHpLikelike = capeScene.Maps[0].Actors[18];
            if (capeHpLikelike.ActorEnum != GameObjects.Actor.LikeLike)
            {
                var newUnkillableVariants = capeHpLikelike.ActorEnum.UnkillableVariants();
                if (newUnkillableVariants != null && newUnkillableVariants.Contains(capeHpLikelike.Variants[0]))
                {
                    capeHpLikelike.Position.z = 4405; // move back from sitting on hp
                }
            }
            var capeGrottoBombiwa = capeScene.Maps[0].Actors[44];
            if (capeGrottoBombiwa.ActorEnum != GameObjects.Actor.Bombiwa)
            {
                var newUnkillableVariants = capeGrottoBombiwa.ActorEnum.UnkillableVariants();
                if (newUnkillableVariants != null && newUnkillableVariants.Contains(capeGrottoBombiwa.Variants[0]))
                {
                    capeGrottoBombiwa.Position.x = -463; // move to the left somewhat, worried its not pointed at the hole but not sure its worth the bother
                    capeGrottoBombiwa.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 180, flags: capeGrottoBombiwa.Rotation.y);
                }
            }
            SceneUtils.UpdateScene(capeScene);

            var ikanaGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaGraveyard.FileID());
            var graveyardGrottoRockCircle = ikanaGraveyardScene.Maps[1].Actors[44];
            if (graveyardGrottoRockCircle.ActorEnum != GameObjects.Actor.GrassRockCluster)
            {
                graveyardGrottoRockCircle.Position.z = -1877; // move back from sitting right on top of the grotto
            }
            SceneUtils.UpdateScene(ikanaGraveyardScene);

            var snowheadTempleScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SnowheadTemple.FileID());
            var snowheadTempleFireArrowWiz = snowheadTempleScene.Maps[6].Actors[0];
            if (snowheadTempleFireArrowWiz.ActorEnum != GameObjects.Actor.Wizrobe)
            {
                snowheadTempleFireArrowWiz.Position.x = -1140; // move back to center of the room, not sure why this guy is so close to the door normally
            }
            SceneUtils.UpdateScene(snowheadTempleScene);

            FixEvanRotation();
            MoveShopScrubsIfRandomized();
            MovePostmanIfRandomized(terminaField);
        }

        public static void DisableAllLocationRestrictions()
        {
            /// because, sometimes, enemies can be placed inside, all rules of society have shattered

            // 19 = top of clock tower: if you can soar out its a "problem" (shrug)
            // 54 = sword school: hookshot can lock the player
            var sceneSkipList = new List<int> { (int)GameObjects.Scene.ClockTowerRoof, (int)GameObjects.Scene.SwordsmansSchool };

            /// player item restrictions is a unique list in the code file (z_parameter)
            //var restrictionTableVRAMStart = 0x801BF6C0; // 0xC55C00 -> DC4 // offset: 119C00
            var tableOffset = 0x119C00;
            var codeFile = RomData.MMFileList[31].Data;
            while (tableOffset < 0x119DC4)
            {
                if (sceneSkipList.Contains(codeFile[tableOffset + 0]) == false)
                {
                    // 0 offset is the scene value
                    codeFile[tableOffset + 1] = 0x00;
                    codeFile[tableOffset + 2] = 0x00;
                    codeFile[tableOffset + 3] = 0x00;
                }

                tableOffset += 4;
            }
        }

        private static void FixScarecrowTalk()
        {
            /// scarecrow breaks if you try to teach him a song anywhere where he normally does not exist
            if (!ReplacementListContains(GameObjects.Actor.Scarecrow)) return;

            var scarecrowFID = GameObjects.Actor.Scarecrow.FileListIndex();
            RomUtils.CheckCompressed(scarecrowFID);
            var scarecrowFile = RomData.MMFileList[scarecrowFID].Data;

            // song teaching scarecrow gets stuck after song is done
            // the kakasi code tries to start a cutscene in stages per frame
            // first frame: tell game you want to start cutscene, second frame check if cs available to start... we never succeed here
            // so the code repeats going to the same spot over and over, never advancing
            // instead, we can just branch from that spot to the finish code

            ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x11E0, 0x1000000F); // branch F down past the if (if state == 1)

            // however thats not the only issue, if you teach a song in TF before breaking the ice block, it triggers the ice break cutscene
            // so we have to stop the cutscenes call
            // cutscene call for songteaching camera swinging
            // ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x1100, 0x00000000); // NOP the ActorCutscene_SetIntentToPlay

            // cutscene call after twirl
            // ReadWriteUtils.Arr_WriteU32(scarecrowFile, 0x1100, 0x00000000); // NOP the ActorCutscene_SetIntentToPlay

            // UNFINISHED: TODO keep going, I think I have to change one of the function straight to digging away and skip dialogue because that function is long
        }

        /// <summary>
        /// Moves the deku baba in southern swamp
        ///   why? beacuse they are positioned in the elbow and its visually jarring when they spawn/despawn on room swap
        ///   its already noticable in vanilla, but with mixed enemy rando it can cause whole new enemies to pop in and out
        /// </summary>
        public static void FixSouthernSwampDekuBaba(Random rng)
        {
            Scene southernswampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());

            // because this room is already borderline lag fest, turn one into a lilypad
            // actor 7 is the furthest back in the cave, unreachable
            //var newLilyPad = southernswampScene.Maps[0].Actors[6];
            //newLilyPad.ChangeActor(GameObjects.Actor.Lilypad, vars: 0, modifyOld: true);
            //newLilyPad.Position = new vec16(561, 0, 790); // placement: toward back wall behind tourist center
            // because of dyna limits, going to stop changing this to lily and instead leave as actor but still move
            var movedToFlower = southernswampScene.Maps[0].Actors[6];
            movedToFlower.Position = new vec16(2781, 57, 2390);
            movedToFlower.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 45, flags: movedToFlower.Rotation.y);

            var movedToTree = southernswampScene.Maps[0].Actors[4];
            movedToTree.Position = new vec16(2020, 22, 300); // placement: to the right as you approach witches, next to tree
            // rotation normal to wall behind it, turn to the right 90deg
            movedToTree.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: movedToTree.Rotation.y);

            // this actor normally faces the big oct, have them face away from the wall
            var nearSoaringStone = southernswampScene.Maps[0].Actors[44];
            nearSoaringStone.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: nearSoaringStone.Rotation.y);


            // witch area babas
            var movedToGrass = southernswampScene.Maps[2].Actors[2];
            movedToGrass.Position = new vec16(2910, 14, -1075); // placement: between the bushes along the wall
            // rotation normal to wall behind it, turn to the left 90deg
            movedToGrass.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: movedToGrass.Rotation.y);

            var movedToWaterFall = southernswampScene.Maps[2].Actors[3];
            movedToWaterFall.Position = new vec16(4240, -2, -1270); // placement: near waterfall

            // moving the clear swamp versions
            Scene clearSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwampClear.FileID());
            clearSwampScene.Maps[0].Actors[4].Position = new vec16(1686, 23, 416); // moved to pier
            clearSwampScene.Maps[0].Actors[6].Position = new vec16(1663, 5, -103); // moved out front a big
            // witch room
            clearSwampScene.Maps[2].Actors[2].Position = new vec16(3001, 8, -1070);
            clearSwampScene.Maps[2].Actors[3].Position = new vec16(4288, 11, -1312);

            //if (rng.Next() % 100 >= 50) // chance of watersurface vs waterbottom
            {
                // move the southern swamp octorok to the surface 
                southernswampScene.Maps[0].Actors[3].Position.y = 0; // set to water height
            }
            //else
            {
                // leave on the bottom but change the type
                // TODO add chance of floor bottom instead
            }
        }

        private static void FixRoadToSouthernSwampBadBat()
        {
            /// bad bat can randomize as a wall enemy or flying enemy, 
            ///   so move all flying ones to places where they can fit in as wall enemies or fly off
            ///   EXCEPT: right now I have an issue where they can be spiders with path because they can be wall enemies,
            ///   so for now change them to wall only

            // the bat at the top of the tree is in the way (takes off flies around)
            // move them to the further wall as a wall/flying enemy
            var roadtoswampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RoadToSouthernSwamp.FileID());
            var movedDownTreeBat = roadtoswampScene.Maps[0].Actors[7];
            movedDownTreeBat.Position = new vec16(927, -29, 2542); // placement: along the south east corner
            // match rotation with the wall
            movedDownTreeBat.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 225, flags: movedDownTreeBat.Rotation.y); ;
            ActorUtils.FlattenPitchRoll(movedDownTreeBat);
            movedDownTreeBat.ChangeVariant(0xFF9F); // change to perched on wall type

            // the bad bad on the tree is just far enough from the tree to cause a bombchu explosion, move closer
            var movedCloserToTreeBat = roadtoswampScene.Maps[0].Actors[8];
            movedCloserToTreeBat.Position.x = 422;

            // move corridor bat to the short cliff wall near swamp shooting galery
            var movedToCliffBat = roadtoswampScene.Maps[0].Actors[6];
            movedToCliffBat.Position = new vec16(2432, -40, 2871);
            // match rotation with the other tree sitting bat
            movedToCliffBat.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: movedToCliffBat.Rotation.y);
            movedDownTreeBat.ChangeVariant(0xFF9F); // change to perched on wall type

            // because the third bat was moved out of center corridor back, move one of the baba forward, we're basically swapping them
            var movedForwardDekuBaba = roadtoswampScene.Maps[0].Actors[14];
            movedForwardDekuBaba.Position.x = 1990;
            movedForwardDekuBaba.Position.z = 2594;
            movedForwardDekuBaba.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 195, flags: movedForwardDekuBaba.Rotation.y);
        }

        private static void FixSpecificLikeLikeTypes()
        {
            /// some likelikes dont follow the normal water/ground type variety, we want detection to correctly ID them
            ///  here we switch their types to match for replacement in enemizer auto-detection

            var coastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
            // coast: shallow water likelike along the pillars is ground, should be water
            coastScene.Maps[0].Actors[21].Variants[0] = 2;
            // coast: bottom of the ocean east is ground, should be water
            coastScene.Maps[0].Actors[24].Variants[0] = 2;
            // coast: tidepool likelike is water
            coastScene.Maps[0].Actors[20].Variants[0] = 2;

            // cleared coast likelikes
            coastScene.Maps[1].Actors[43].Variants[0] = 2;
            coastScene.Maps[1].Actors[44].Variants[0] = 2;
            coastScene.Maps[1].Actors[46].Variants[0] = 2;
        }

        private static void FixSpecificTektiteTypes()
        {
            var twinIslandsSpring = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            twinIslandsSpring.Maps[0].Actors[2].Variants[0] = 0xFFFD;
        }

        private static void EnableDampeHouseWallMaster()
        {
            /// dampe's house wallmaster is an enounter actor, not a regular wallmaster,
            ///  we have to switch it to regular enemy for enemizer shuffle to find and replace it

            var dampehouseScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DampesHouse.FileID());
            var wallmaster = dampehouseScene.Maps[0].Actors[0];
            // move to center of the main room,
            wallmaster.Position.z = 0x40;
            // previous encounter actor used rotation as parameters, flatten rotation now for replacement
            ActorUtils.FlattenPitchRoll(wallmaster);
            // change actor to wallmaster proper for enemizer detection
            wallmaster.ChangeActor(newActorType: GameObjects.Actor.WallMaster, vars: 0x1, modifyOld: true);
        }

        private static void EnableTwinIslandsSpringSkullfish()
        {
            /// the skullfish in twinislands spring are an encounter actor, not regular skullfish actors
            ///  we have to switch them to regular skullfish for enemizer shuffle to find and replace them
            /// also we move them out of the cave in case its a water surface enemy, and to spread them out
            ///  default they are all stacked on top of the cave chest 

            var twinislandsspringScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            var encounter1 = twinislandsspringScene.Maps[0].Actors[21];
            encounter1.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter1); // flatten encounter rotation (rotation parameters
            // move to just outside cave (east)
            encounter1.Position = new vec16(-317, 0, -881);

            var encounter2 = twinislandsspringScene.Maps[0].Actors[27];
            encounter2.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter2); // flatten encounter rotation (rotation parameters
            // move to just outside cave (west)
            encounter2.Position = new vec16(-200, 0, -890);

            var encounter3 = twinislandsspringScene.Maps[0].Actors[28];
            encounter3.ChangeActor(GameObjects.Actor.SkullFish, vars: 0, modifyOld: true);
            ActorUtils.FlattenPitchRoll(encounter3); // flatten encounter rotation (rotation parameters
            // move to near chest on the south side
            encounter3.Position = new vec16(300, 0, 700);
        }

        public static void NudgeFlyingEnemiesForTingle()
        {
            /// if tingle can be randomized, he can end up on any flying enemy in scenes that don't already have a tingle
            /// some of these scenes would drop him into water or off the cliff where he cannot be reached
            if (!ReplacementListContains(GameObjects.Actor.Tingle)) return;

            var woodfallexteriorScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Woodfall.FileID());
            var firstDragonfly = woodfallexteriorScene.Maps[0].Actors[4];
            firstDragonfly.Position.x = 990; // over a deku scrub
            firstDragonfly.Position.z = 690;

            var secondDragonfly = woodfallexteriorScene.Maps[0].Actors[5];
            secondDragonfly.Position.x = 615; // over a lillypad
            secondDragonfly.Position.z = -495;

            var lilypad = woodfallexteriorScene.Maps[0].Actors[37];
            lilypad.Position.x = 615; // move lilypad over
            lilypad.Position.z = -495;

            var coastScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GreatBayCoast.FileID());
            coastScene.Maps[0].Actors[17].Position.z = 3033; // edge the guay over the land just a bit

            // to prevent him from falling to abyss
            var snowheadKeese = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Snowhead.FileID()).Maps[0].Actors[0];
            snowheadKeese.Position.x = -758;
        }

        private static void ExtendGrottoDirectIndexByte()
        {
            /// in MM the top nibble of the grotto variable is never used, 
            /// but in the vanilla code it be detected and used as a grotto warp index of the static grottos entrances array (-1)
            /// MM normally uses the z rotation instead to index warp, but we can use either or
            /// however, only the 3 lower bits of this nibble are used, the code ANDS with 7
            /// why? the fourth bit isn't ever used by any grotto, and looking at the code shows it is never used
            /// so here, we set the ANDI 7 to F instead, allowing us extended access to the entrance array
            /// TODO and by 0xF800 and shift less to get more range, requires re-writting all
            var grotholeFID = GameObjects.Actor.GrottoHole.FileListIndex();
            RomUtils.CheckCompressed(grotholeFID);
            RomData.MMFileList[grotholeFID].Data[0x2FF] = 0xF; // ANDI 0x7 -> ANDI 0xF
        }

        private static void EnablePoFusenAnywhere()
        {
            /// the flying poe baloon romani uses to play her game doesn't spawn unless
            ///  1) it has an explosion fuse timer OR
            ///  2) it detects romani actor in the scene, so it can count baloon pops
            /// but the code that blocks the baloon if neither of these are true is nop-able,
            ///   and the rest of the code is designed to work without issue in this case

            if (!ReplacementListContains(GameObjects.Actor.PoeBalloon)) return;

            var enPoFusenFID = GameObjects.Actor.PoeBalloon.FileListIndex();
            RomUtils.CheckCompressed(enPoFusenFID);

            // nops the MarkForDeath function call, stops them from de-spawning
            ReadWriteUtils.Arr_WriteU32(RomData.MMFileList[enPoFusenFID].Data, Dest: 0xF4, val: 0x00000000);

            // because they can now show up in weird places, they need to be poppable more ways
            // I mean.. its a baloon, it should have always been really easy to pop
            RomData.MMFileList[enPoFusenFID].Data[0xB5D] = 0xF1; // stick
            RomData.MMFileList[enPoFusenFID].Data[0xB5F] = 0xF1; // bombs
            RomData.MMFileList[enPoFusenFID].Data[0xB60] = 0xF1; // zora fins
            RomData.MMFileList[enPoFusenFID].Data[0xB63] = 0xF1; // hookshot
            RomData.MMFileList[enPoFusenFID].Data[0xB65] = 0xF1; // swords
            RomData.MMFileList[enPoFusenFID].Data[0xB6C] = 0xF1; // deku bubble
            RomData.MMFileList[enPoFusenFID].Data[0xB6F] = 0xF1; // zora barier
            RomData.MMFileList[enPoFusenFID].Data[0xB72] = 0xF1; // bush throw
            RomData.MMFileList[enPoFusenFID].Data[0xB73] = 0xF1; // zora karate
            RomData.MMFileList[enPoFusenFID].Data[0xB75] = 0xF1; // fd beam
        }

        public static void ShortenChickenPatience()
        {
            /// Cuccos take too many hits before they get mad, let's shrink this
            /// niw health is `rand(0-9.9) + 10.0` (10-20 hits), lets replace with 0-2 + 1

            if (!ReplacementListContains(GameObjects.Actor.FriendlyCucco)) return;

            RomUtils.CheckCompressed(GameObjects.Actor.FriendlyCucco.FileListIndex());
            var niwData = RomData.MMFileList[GameObjects.Actor.FriendlyCucco.FileListIndex()].Data;
            // both of these changes made in EnNiw_Init
            ReadWriteUtils.Arr_WriteU32(niwData, 0x24A8, 0x40000000); // 9.9 -> 2 in f32 (in rodata)
            ReadWriteUtils.Arr_WriteU16(niwData, 0x156, 0x3F80); // 10 -> 1 in f32 (first short only as literal hardcoded)
        }

        public static void FixThornTraps()
        {
            // this is incomplete, fixing thorn traps will likely take rewriting code not just removing

            /// in thorn traps init code it checks if a path has only 2 nodes, if it has more or less than 2 it dies

            // let's just remove that jal
            var location = 0x3A8;// 234 * 4;
            RomUtils.CheckCompressed(GameObjects.Actor.ThornTrap.FileListIndex());
            var thornData = RomData.MMFileList[GameObjects.Actor.ThornTrap.FileListIndex()].Data;

            ReadWriteUtils.Arr_WriteU32(thornData, location, 0x00000000);
            ReadWriteUtils.Arr_WriteU32(thornData, 0x378, 0x00000000);
        }

        public static void FixSeth2()
        {
            /// seth 2, the guy waving his arms in the termina field telescope, like oot spiderhouse
            /// his init code checks for a value, and does not spawn if the value is different than expected
            if (!ReplacementListContains(GameObjects.Actor.Seth2)) return;

            var sethFid = GameObjects.Actor.Seth2.FileListIndex();
            RomUtils.CheckCompressed(sethFid);
            var sethData = RomData.MMFileList[sethFid].Data;
            //nopping the mark for death
            ReadWriteUtils.Arr_WriteU32(sethData, 0x88, 0x00000000);
            //nopping the early return
            ReadWriteUtils.Arr_WriteU32(sethData, 0x90, 0x00000000);

            //weirdly, even though the the telescope is a different SCENE, seth2 is found in the regular gamplay scene, his code just kills him
            // until I move him hes in a bad spot on top of grottos, for now just kill him
            // TODO: Free actor slots? 
            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            tfScene.Maps[0].Actors[28].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Actors[29].ChangeActor(GameObjects.Actor.Empty);
            tfScene.Maps[0].Objects[21] = GameObjects.Actor.Empty.ObjectIndex();
            //var map = tfScene.Maps[0];
        }

        public static void FixCuccoChicks()
        {
            /// this now gets overwritten by a rewritten cucco chick actor,
            /// this is left over in case the player does not have that actor

            // stop chicks from despawning if there is no object_niw (adult cucco) object
            var cuccoChickFID = GameObjects.Actor.CuccoChick.FileListIndex();
            RomUtils.CheckCompressed(cuccoChickFID);
            var cuccoChickData = RomData.MMFileList[cuccoChickFID].Data;
            // we need to branch past both the mark for death and the return (return before actor_update will just break the whole actor)
            ReadWriteUtils.Arr_WriteU32(cuccoChickData, 0x30, 0x10000005); // BGEZ -> B (branch always)
        }

        private static void FixDekuPalaceReceptionGuards()
        {
            /// if we randomize the patrolling guards in deku palace:
            /// we end up removing the object the front guards require to spawn
            /// however there is a (as far as I can tell) unused object in this scene we can swap
            /// object_dns which is the object used by the dancing deku guards in the king's chamber
            /// nothing seems to use their object in the regular palace scene, no idea why the object is there
            if (!ReplacementListContains(GameObjects.Actor.DekuPatrolGuard)) return;

            var frontGuardOID = GameObjects.Actor.DekuPatrolGuard.ObjectIndex();
            var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());

            /*if (!dekuPalaceScene.Maps[0].Objects.Contains(frontGuardOID))
            {
                // scene has already been written at this point, need to romhack it, faster than re-writing the whole scene file
                var dekuPalaceRoom1FID = GameObjects.Scene.DekuPalace.FileID() + 1;
                var dekuPalaceRoom1File = RomData.MMFileList[dekuPalaceRoom1FID].Data;
                ReadWriteUtils.Arr_WriteU16(dekuPalaceRoom1File, Dest: 0x4E, (ushort)frontGuardOID);
            } // */
            dekuPalaceScene.Maps[0].Objects[7] = frontGuardOID;
            dekuPalaceScene.Maps[1].Objects[7] = frontGuardOID;
            dekuPalaceScene.Maps[2].Objects[7] = frontGuardOID;
        }

        private static void FixBomberKidsGameFinishWarp()
        {
            /// for some weird reason, their warp is calculated in real time based on the player's position,
            /// the code is unknown, but.. it should always go to the same spot so we should be able to just replace it
            /// the saving kids warp is 0x6D50

            var bombjimbFid = GameObjects.Actor.BombersYouChase.FileListIndex();
            RomUtils.CheckCompressed(bombjimbFid);
            var bombjimbData = RomData.MMFileList[bombjimbFid].Data;
            // we want to replace the Entrance_CreateFromSpawn function call,
            // which would load the old entrance address into v0, with a manual load v0 with our warp
            ReadWriteUtils.Arr_WriteU32(bombjimbData, 0x1E88, 0x2402D650); // Jal Entrance_CreateFromSpawn -> Addiu V0, R0, 0xD650
            // sometimes uses the other entrance calculation where it gets it from the exit list
            // lets just jump past that
            ReadWriteUtils.Arr_WriteU32(bombjimbData, 0x1E28, 0x10000016); // BNEZ BREQ -> J to L80C02D24

        }

        private static void ModifyAllGraveyardBatsToFly() {
            /// some graveyard bats are wall types, and MMR enemizer still gets confused by multiple types,
            /// so we want to swap all of them to flying type
            if (!ReplacementListContains(GameObjects.Actor.Dampe)) return;

            // single flying bat, visible
            var newVariant = 0x0101;

            var ikanaGraveyardScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.IkanaGraveyard.FileID());

            ikanaGraveyardScene.Maps[0].Actors[22].ChangeVariant(newVariant);
            ikanaGraveyardScene.Maps[0].Actors[23].ChangeVariant(newVariant);
        }

        private static void FixInjuredKoume()
        {
            /// Injured koume in the woods of mystery, her code checks if she is in the woods of mystery and self culls
            if (!ReplacementListContains(GameObjects.Actor.InjuredKoume)) return;

            var koumeFID = GameObjects.Actor.InjuredKoume.FileListIndex();
            RomUtils.CheckCompressed(koumeFID);
            var koumeData = RomData.MMFileList[koumeFID].Data;
            // the code check is entrance, and then moves to kill, so just remove the branch
            ReadWriteUtils.Arr_WriteU32(koumeData, 0x2D38, 0x00000000); // BNE to actor kill -> NOP

            // we should also check if 
        }

        private static void RandomizePinnacleRockSigns()
        {
            /// these signs use gameplay_keep, so there is no sign to associate with them
            /// HOWEVER, there is a bombiwa object in the object list that doesnt seem to do anything, we can randomize it

            var listOfSignIds = new List<int> { 14, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43 };

            var pinnacleSceneActors = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PinnacleRock.FileID()).Maps[0].Actors;

            foreach (var aId in listOfSignIds)
            {
                pinnacleSceneActors[aId].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x8077, true);
                pinnacleSceneActors[aId].OldName = "WaypointSign"; // so the log doesnt say they are bombiwa, rename here
            }
        }

        private static void RandomizeDekuPalaceBombiwaSigns()
        {
            /// In deku palace, there are signs pointing you to the left and right across lilipads, on top of bombiwa
            /// leaving the signs while randomizing the bombiwa would be weird, so I am going to move the signs and turn them into bombiwa to add immersion

            if (!ReplacementListContains(GameObjects.Actor.Bombiwa)) return;

            var dekuPalaceActors = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID()).Maps[0].Actors;

            dekuPalaceActors[23].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x8077, modifyOld: true);
            dekuPalaceActors[23].OldName = "WayPointSignRight";
            dekuPalaceActors[23].Position = new vec16(1429, -40, 1583); // west to the right side

            dekuPalaceActors[24].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0x8077, modifyOld: true);
            dekuPalaceActors[24].OldName = "WayPointSignLeft ";
            dekuPalaceActors[24].Position = new vec16(-1297, -40, 1529); // east to the left side

            // not sure why, in scenetatl they have no ratation, but in-rando they have x/z rotations which is messing up the actors
            ActorUtils.FlattenPitchRoll(dekuPalaceActors[23]);
            ActorUtils.FlattenPitchRoll(dekuPalaceActors[24]);

            // actual bombiwa are really low in the water, raise to just below surface
            dekuPalaceActors[19].Position.y = -40;
            dekuPalaceActors[20].Position.y = -40;
        }

        private static List<(GameObjects.Actor actor, ushort vars)> shallowWaterReplacements = new List<(GameObjects.Actor actor, ushort vars)>
        {
            (GameObjects.Actor.LikeLike, 0x2),   // water bottom type
            (GameObjects.Actor.Octarok, 0xFF00), // water surface type
            (GameObjects.Actor.GoGoron, 0x7FC1)  // ground type (race track goron, stretching)
        };

        public static void ChangeHotwaterGrottoDekuBabaIntoSomethingElse(Random rng)
        {
            /// I want more variety, so I want the hot spring water grotto to have a different actor in it than regular grottos
            // using likelike as a replacement, sometimes rando will put water and sometimes land, and mikau can give us water surface actors

            // we want both ground or water types, so we are going to use multiple actors
            int randomValue = rng.Next(shallowWaterReplacements.Count);
            var coinTossResultActor = shallowWaterReplacements[randomValue];

            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            var hotspringDekuBaba = grottosScene.Maps[14].Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.DekuBabaWithered);
            foreach (var baba in hotspringDekuBaba)
            {
                baba.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                baba.OldName = "HotSpringBaba";
            }

            // from the perspective of the door
            var farEntry = hotspringDekuBaba[0];
            var leftEntry = hotspringDekuBaba[1];
            var rightEntry = hotspringDekuBaba[2];

            // move them into water
            farEntry.Position = new vec16(6936, -22, 824);
            leftEntry.Position = new vec16(6935, -24, 1072);
            rightEntry.Position = new vec16(7160, -24, 916);
            if (farEntry.ActorEnum == GameObjects.Actor.Mikau) // surface type, move up to water top
            {
                farEntry.Position.y = 0;
                leftEntry.Position.y = 0;
                rightEntry.Position.y = 0;
            }

            // baba have no face, so they don't get a rotation normally, they would all face the same direction,
            // turn them to face the center of the pool and each other
            // zero y rotation is facing the door
            farEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(30, flags: farEntry.Rotation.y);
            leftEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(90 + 60, flags: leftEntry.Rotation.y);
            rightEntry.Rotation.y = ActorUtils.MergeRotationAndFlags(360 - 45 - 30, flags: rightEntry.Rotation.y);

            // change object in the room to match new fake actors
            grottosScene.Maps[14].Objects[2] = (coinTossResultActor.actor).ObjectIndex();
        }

        private static void RandomizeGrottoGossipStonesPerGrotto()
        {
            /// each gossip stone grotto has enough object space to add or switch an object
            /// and then randomize three of the gossip stones to something new and random
            /// should be doable without breaking the gossip stone quest

            if (!ReplacementListContains(GameObjects.Actor.GossipStone)) return;

            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());

            void ChangeStones(Map map, int[] actorSlots, GameObjects.Actor actorType, int actorParam, string name)
            {
                for(int i = 0; i < actorSlots.Length; i++)
                {
                    var actor = map.Actors[actorSlots[i]];
                    actor.ChangeActor(actorType, actorParam, modifyOld: true);
                    actor.OldName = name;
                }
            }


            // west butterfly/comb grotto (middle right stone)
            var westGrotto = grottosScene.Maps[0];
            westGrotto.Objects[3] = GameObjects.Actor.Leever.ObjectIndex(); // unused deku baba object here, we can override
            int[] westStoneSlots = { 9, 10, 12 };
            ChangeStones(westGrotto, westStoneSlots, GameObjects.Actor.Leever, actorParam: 0xFF, "GossipStoneWest");

            // south spider grotto (far left stone)
            var southGrotto = grottosScene.Maps[1];
            southGrotto.Objects.Add(GameObjects.Actor.Armos.ObjectIndex()); // three objects in this scene, because of padding there is a fourth free spot without scene expansion
            var southGrottoRoomFile = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 2].Data; // room file
            southGrottoRoomFile[0x29] = 4; // update object list to load all four objects in-game
            int[] southGrottoStones = { 4, 5, 6 };
            ChangeStones(southGrotto, southGrottoStones, GameObjects.Actor.Armos, actorParam: 0x7F, "GossipStoneSouth");

            // east sandy grotto (far right stone)
            var eastGrotto = grottosScene.Maps[2];
            eastGrotto.Objects[1] = GameObjects.Actor.Wolfos.ObjectIndex(); // unused deku baba slot can be reused
            int[] eastStoneSlots = { 5, 6, 7 };
            ChangeStones(eastGrotto, eastStoneSlots, GameObjects.Actor.Wolfos, actorParam: 0xFF80, "GossipStoneEast");

            // north flooded grotto (middle left stone)
            var northGrotto = grottosScene.Maps[3];
            northGrotto.Objects[1] = GameObjects.Actor.Snapper.ObjectIndex(); // unused deku baba slot can be reused
            int[] northGrottoSlots = { 1, 3, 4 };
            ChangeStones(northGrotto, northGrottoSlots, GameObjects.Actor.Snapper, actorParam: 0, "GossipStoneNorth");

            void ChangeGossipHintType(Actor stone, int newHint)
            {
                stone.Variants[0] &= 0xFFF0; // remove previous bottom (text offset)
                stone.Variants[0] |= newHint;
            }

            /// the hint given by the big gossip stone is always the same hint, we have to change the hint variable
            /// where, the hint offset is +4 from the type 2 (regular hints) to use the same hint IDs with big type
            /// so hints 0, 1, 2 become 4, 5, 6
            //ChangeGossipHintType(southGrotto.Actors[3], 0x2); // already far left, leave alone
            ChangeGossipHintType(northGrotto.Actors[2], 4); // middel left
            ChangeGossipHintType(westGrotto.Actors[11], 5); // middle right
            ChangeGossipHintType(eastGrotto.Actors[8], 6); // far right

        }

        private static void SwapGreatFairies(Random rng)
        {
            /// actorizer is currently a little silly in that, if an actor/enemy is replaced, we replace the objects in other rooms of the same scene
            ///   which normally prevents us randomizing only one fairy since all fairy fountains are in the same scene they would all get dinged
            /// in order to randomize just one great fairy we need to do it piecemeal

            if (!ACTORSENABLED) return;

            var greatfairyFountainScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.FairyFountain.FileID());

            void ChangeGreatFairyActors(int mapIndex, int objectIndex, int actorIndex1, int actorIndex2, int actorIndex3,  string fairyName,
                vec16 pos1, vec16 pos2, vec16 pos3)
            {
                // shallow bath water means we have options for what to replace with, pick one
                int randomValue = rng.Next(shallowWaterReplacements.Count);
                var coinTossResultActor = shallowWaterReplacements[randomValue];

                var map = greatfairyFountainScene.Maps[mapIndex];
                var dyYosei = map.Actors[actorIndex1]; // placed to the left
                dyYosei.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                dyYosei.OldName = fairyName;
                dyYosei.Position = pos1;
                dyYosei.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: dyYosei.Rotation.y); // turn to face right


                var elfgroup = map.Actors[actorIndex2]; // placed to the right
                elfgroup.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                elfgroup.OldName = fairyName + "Cloud";
                elfgroup.Position = pos2;
                elfgroup.Rotation.y = ActorUtils.MergeRotationAndFlags(270, flags: dyYosei.Rotation.y); // turn to face left

                if (actorIndex3 != -1) // there isnt always a talk spot to randomize, only in ikana and town
                {
                    var talkalot = map.Actors[actorIndex3]; // placed in the back facing forward
                    talkalot.ChangeActor(coinTossResultActor.actor, vars: coinTossResultActor.vars, modifyOld: true);
                    talkalot.OldName = fairyName + "TalkSpot";
                    talkalot.Position = pos3;
                }

                map.Objects[objectIndex] = coinTossResultActor.actor.ObjectIndex();
            }

            var mask = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.MaskGreatFairy).Item;
            var magic = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.FairyMagic).Item;

            if (IsActorizerJunk(mask) && IsActorizerJunk(magic))
            {
                ChangeGreatFairyActors(mapIndex: 0, objectIndex: 0,
                            actorIndex1: 1, 2, 4,
                            "TownFairy",
                            pos1: new vec16(2289, -30, -750), new vec16(2523, -30, -750), new vec16(2412, -30, -929));
            }
            var spinattack = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.FairySpinAttack).Item;
            if (IsActorizerJunk(spinattack))
            {
                ChangeGreatFairyActors(mapIndex: 1, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "WoodfallFairy",
                            pos1: new vec16(1095, -30, -750), new vec16(1294, -30, -750), new vec16(2412, -30, -929));
            }
            var doubleHappiness = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.FairyDoubleMagic).Item;
            if (IsActorizerJunk(doubleHappiness))
            {
                ChangeGreatFairyActors(mapIndex: 2, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "SnowheadFairy",
                            pos1: new vec16(-102, -30, -750), new vec16(93, -30, -750), new vec16(2412, -30, -929));
            }
            var doubleBeef = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.FairyDoubleDefense).Item;
            if (IsActorizerJunk(doubleBeef))
            {
                ChangeGreatFairyActors(mapIndex: 3, objectIndex: 0,
                            actorIndex1: 0, 1, -1,
                            "GreatbayFairy",
                            pos1: new vec16(-1299, -30, -750), new vec16(-1098, -30, -750), new vec16(2412, -30, -929));
            }
            var bigGoronSword = _randomized.ItemList.Single(item => item.NewLocation == GameObjects.Item.ItemFairySword).Item;
            if (IsActorizerJunk(bigGoronSword))
            {
                ChangeGreatFairyActors(mapIndex: 4, objectIndex: 0,
                            actorIndex1: 0, 1, 3,
                            "IkanaFairy",
                            pos1: new vec16(-2481, -30, -750), new vec16(-2319, -30, -750), new vec16(-2407, -30, -872));
            }

        }


        public static void ModifyFireflyKeeseForPerching()
        {
            /// keese only have two params: type 0x7FFF and the 0x8000 flag which is lens sensitive
            /// except, I need to be able to tell rando which ones are perching and which are on the "wall"
            /// so I am changing the params erase code in init to & 0xF from & 0x7FFF for now since we only have 4 types anyway

            var fireflyFid = GameObjects.Actor.Keese.FileListIndex();
            RomUtils.CheckCompressed(fireflyFid);
            var fireflyData = RomData.MMFileList[fireflyFid].Data;

            fireflyData[0xC6] = 0x00; // 0x7F -> 00
            fireflyData[0xC6] = 0x0F; // 0xFF -> 0F
        }


        public static void FixKafeiPlacements()
        {
            if (!VanillaEnemyList.Contains(GameObjects.Actor.Kafei)) return;

            /// if Kafei is randomized, his default placements are silly, move them to be more natural
            var southClockTown = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.SouthClockTown);
            var sctKafei = southClockTown.Maps[0].Actors[2];
            if (sctKafei.ActorEnum != GameObjects.Actor.Kafei) // changed
            {
                // move to the bench so hes not lurking out of sight behind the laundry room area
                sctKafei.Position = new vec16(-615, 16, 425);
                sctKafei.Rotation.y = ActorUtils.MergeRotationAndFlags(90, flags: sctKafei.Rotation.y);
                SceneUtils.UpdateScene(southClockTown);
            }

            var eastClockTown = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.EastClockTown);
            var ectKafei = eastClockTown.Maps[0].Actors[2];
            if (ectKafei.ActorEnum != GameObjects.Actor.Kafei) // changed
            {
                // sitting just outside of town door, move inwards a bit
                ectKafei.Position = new vec16(1475, 60, -747);
                ectKafei.Rotation.y = ActorUtils.MergeRotationAndFlags(180, flags: sctKafei.Rotation.y);
                SceneUtils.UpdateScene(eastClockTown);
            }

            var laundryPool = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.LaundryPool);
            var lpKafei = laundryPool.Maps[0].Actors[9];
            if (lpKafei.ActorEnum != GameObjects.Actor.Kafei) // changed
            {
                // sitting beyond the path back to SCT, move to bridge
                lpKafei.Position = new vec16(-2080, -95, 582);
                SceneUtils.UpdateScene(laundryPool);
            }

            var ikanaCanyon = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.IkanaCanyon);
            var ikanaKafei = ikanaCanyon.Maps[4].Actors[9];
            if (ikanaKafei.ActorEnum != GameObjects.Actor.Kafei) // changed
            {
                // move to his favorite rock
                ikanaKafei.Position = new vec16(2523, -160, 5080);
                SceneUtils.UpdateScene(ikanaCanyon);
            }

        }

        public static void FixWaterPostboxes(SceneEnemizerData thisSceneData)
        {
            /// makes underwater post boxes have the correct vars
            /// this probably shouldnt be its own code I just want underwater postboxes without the vanilla vars thinking they can be water
            /// and un-willing right now to re-write the parameter system to specify vanilla or not

            //if ( ! thisSceneData.Objects.Contains(GameObjects.Actor.Postbox.ObjectIndex())) return;
            // that doesnt work if we are borrowing a vanilla un-touched object
            // lets skip short circuit, it's not that much faster to search all objects when we can search all actors, most areas have small lists

            foreach (var box in thisSceneData.Actors.FindAll(a => a.ActorId == (int)GameObjects.Actor.Postbox))
            {
                var oldVariant = box.Variants[0];
                if (box.Variants[0] > 4)
                    box.Variants[0] &= 0x4;
                Debug.Assert(box.Variants[0] <= 0x04 && box.Variants[0] >= 0);
            }
        }

        public static void FixSnowballActorSpawns(SceneEnemizerData thisSceneData)
        {
            /// The large snowballs can sometimes spawn an actor when you break them,
            /// but they are too stupid to handle the possibility of the actor object missing, crash
            /// but we cannot block them from spawning based on params because params is not used to specify
            /// instead, the parameter that controls snowball type is rotation.y, so we nullify it here per-scene where we add them

            var largeSnowballs = thisSceneData.Actors.FindAll(actor => actor.ActorEnum == GameObjects.Actor.LargeSnowball);
            if (largeSnowballs.Count > 0)
            {
                for(int i = 0; i < largeSnowballs.Count; i++)
                {
                    var snowball = largeSnowballs[i];
                    // where zero rotation (type 0) just drops an item, no actor
                    snowball.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 0, flags: snowball.Rotation.y);
                }
            }
        }

        public static void FixWoodfallTempleGekkoMiniboss()
        {
            /// we cannot randomize the snapper in woodfall temple without breaking the gekko miniboss
            /// beacuse he spawns a special snapper in this fight and he will de-spawn if he detects the object is missing
            /// add a second snapper object to the room so there is still one there

            var woodfallScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.WoodfallTemple.FileID());
            var gekkoRoom = woodfallScene.Maps[8];
            // we cannot remove the woodflower object used by the giant flower, it breaks the door, so probably used by the door for textures
            gekkoRoom.Objects[2] = 0x1A6; // previously: boss blue warp, now snapper

            // since we're changing objects and that will reload the whole list both ways anyway,
            //   might as well shrink it to reduce chances of overflow
            gekkoRoom.Objects[14] = SMALLEST_OBJ; // previously: bo
            gekkoRoom.Objects[15] = SMALLEST_OBJ; // previously: dragonfly
            gekkoRoom.Objects[16] = SMALLEST_OBJ; // previously: skulltula
        }

        public static void FixStreamSfxVolume()
        {
            /// EnStream is an unused actor leftover from OOT
            ///   it is the swirling water vortexes that if you swim into you will void out in OOT: Water Temple
            /// However this actor has a flaw: it calls a function to play a swirling water sfx
            ///   but it uses the wrong function to play the sfx, it plays the same volume from any distance which is really annoying
            /// so here we change it back to the default sfx function almost all actors use to fix it
            /// we are lucky that the old and new function takes the same parameters, so we can change just the jal
            ///   decomp tells me there are no other changes needed to swap them

            if (!ReplacementListContains(GameObjects.Actor.En_Stream)) return;

            var streamFid = GameObjects.Actor.En_Stream.FileListIndex();
            RomUtils.CheckCompressed(streamFid);
            var streamData = RomData.MMFileList[streamFid].Data;
            ReadWriteUtils.Arr_WriteU32(streamData, 0x39C, 0x0C02E3B2); // jal func_800B8FE8() -> Actor_PlaySfxAtPos()
        }

        public static void RepositionClockTownActors()
        {
            // if actors are rando'd then the carpenters probably are too, remove their sounds
            var southClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var carpenterSound = southClockTownScene.Maps[0].Actors[49];
            carpenterSound.ChangeActor(GameObjects.Actor.Carpenter, vars: 1, modifyOld: true); // non-pathing type

            // move to standing in front of the sign
            carpenterSound.Position.x = -423;
            carpenterSound.Position.z = -174; // move forward to muto placement
            // rotation toward the sign
            carpenterSound.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: carpenterSound.Rotation.y);
            // set time flags so that only shows on night 1 and day 4 (rotation was already x:0,z:0)
            carpenterSound.Rotation.x = 0x6; // all day 0
            carpenterSound.Rotation.z = 0x3 | 0x4 | 0x40; // all day 4, night 3, night 1

            // we can also hear the noises in west/east, those actors should also be removed
            var eastClockTownScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.EastClockTown.FileID());
            var carpenterSound2 = eastClockTownScene.Maps[0].Actors[63];

            // change into a cremia actor, his object is here wasted and unused, we could rando it
            carpenterSound2.ChangeActor(GameObjects.Actor.Cremia, vars: 0, modifyOld: true);
            carpenterSound2.Position = new vec16(1329, 102, -429);
            carpenterSound2.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 90, flags: carpenterSound2.Rotation.y);
            // set time flags so that only shows on night 1 and day 4 (rotation was already x:0,z:0)
            carpenterSound2.Rotation.x = 0x6; // all day 0
            carpenterSound2.Rotation.z = 0x3 | 0x10; // all day 4, night 2

            // however, while the cremia object and actor exist in setup 3, they do not in setup 1
            // thankfully there is a free space in the object list because odd count, one free space because of padding
            eastClockTownScene.Maps[0].Objects.Add(GameObjects.Actor.Cremia.ObjectIndex());
            var ECTData = RomData.MMFileList[eastClockTownScene.File + 1];
            ECTData.Data[0x31] = 0x1A; // increase objectlist number, how many it loads, by one

            // should we rando the tower?

            // anju's actor spawns behind the inn door, move her to be visible in sct
            var anju = eastClockTownScene.Maps[0].Actors[0];
            anju.Position = new vec16(153, 3, 246);
            anju.Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270, flags: anju.Rotation.y); // rotate to away from us

            // move next to mayors building
            // TODO bug this is not next to mayor building for some reason, next to inn
            var gorman = eastClockTownScene.Maps[0].Actors[4];
            gorman.Position = new vec16(1026, 200, -1947);
        }


        private static void AllowGuruGuruOutside()
        {
            /// guruguru's actor spawns or kills itself based on time flags, ignoring that the spawn points themselves have timeflags
            /// if we want guruguru to be placed in the world without being restricted to day/night only (which is lame) we have to stop this
            if (!ReplacementListContains(GameObjects.Actor.GuruGuru)) return;

            var guruFid = GameObjects.Actor.GuruGuru.FileListIndex();
            RomUtils.CheckCompressed(guruFid);
            var guruData = RomData.MMFileList[guruFid].Data;
            ReadWriteUtils.Arr_WriteU32(guruData, Dest: 0x104, val: 0x00000000); // BNE (if day, and not type 1, die) -> NOP

            // funny enough, type 0 (talkable during day) and type 2 (creates music through the walls)
            //  both are already time flag'd to not show up at night in the inn... so why did the code care?

            // BUT EVEN MORE FUNNY, this funny guy, he CHECKS NIGHT in his update function too WTF
            // jeez just branch past all that noise
            ReadWriteUtils.Arr_WriteU32(guruData, Dest: 0x9BC, val: 0x10000013); // BNEL (test night checks) -> B past it all to actionfunc
        }

        public static void RemoveSTTUnusedPoe()
        {
            /// not inverted, REGULAR stone tower has a poe object... why?
            /// we can recover some headroom by removing it
            ///   remember to delete this if I ever get free objects working instead

            var stonetowertempleScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.StoneTowerTemple);
            for (int i = 0; i < stonetowertempleScene.Maps.Count; ++i)
            {
                var room = stonetowertempleScene.Maps[i];
                var poeIndex = room.Objects.FindIndex(obj => obj == GameObjects.Actor.Poe.ObjectIndex());
                if (poeIndex > 0)
                {
                    room.Objects[poeIndex] = SMALLEST_OBJ;
                }
            }
        }

        public static void FixSilverIshi()
        {
            /// in MM the silver boulders that are pickupable by goron are ishi in field_keep object
            /// however, these boulders always check the scene SwitchFlags and set the flags when destroyed, so you cannot respawn them
            ///   considering nothing in vanilla needs these, and because
            ///   I'm worried about setting flags for something else, lets remove that

            var ishiFid = GameObjects.Actor.IshiRock.FileListIndex();
            RomUtils.CheckCompressed(ishiFid);
            var ishiData = RomData.MMFileList[ishiFid].Data;
            ReadWriteUtils.Arr_WriteU32(ishiData, Dest: 0x12CC, val: 0x00000000); // JAL (Actor_SetSwitchFlag) -> NOP
            // there is code to stop the boulder from dropping random good shit, we should have that
            ReadWriteUtils.Arr_WriteU32(ishiData, Dest: 0x8CC, val: 0x00000000); // BNEZ (If ! ishi param & 1) -> NOP
        }

        public static void FixBabaAndDragonflyShadows()
        {
            /// En_Bba_01 is an unused actor who appears to be the grandma from the bomb proprieters shop
            /// however she uses an expensive and barely used shadow draw function that makes a custom shadow to match her body shape
            /// we need to remove it since its totally broken, its the primary reason dragon flies lag so much
            /// also should make dragonfly better so do that too, since 99% of the time we cant see its shadow as its at y=0 (bug)

            /*
            var dragonflyFid = GameObjects.Actor.DragonFly.FileListIndex();
            RomUtils.CheckCompressed(dragonflyFid);
            var dragonflyData = RomData.MMFileList[dragonflyFid].Data;
            // similar to baba, we see a loop followed by a finishing function, we want to skip both in the main draw function
            ReadWriteUtils.Arr_WriteU32(dragonflyData, Dest: 0x2498, val: 0x10000018); // <irrelevant code> -> Jump to 24E4
            */

            if (!ReplacementListContains(GameObjects.Actor.BabaIsUnused)) return;

            var babaFid = GameObjects.Actor.BabaIsUnused.FileListIndex();
            RomUtils.CheckCompressed(babaFid);
            var babaData = RomData.MMFileList[babaFid].Data;
            // the end of the draw function must be skipped, so we branch past all of it to the end of the function
            ReadWriteUtils.Arr_WriteU32(babaData, Dest: 0xB34, val: 0x10000024); // <irrelevant code> -> Jump to 0xBC8 (beginning of register re-load)
        }

        /* private static void RecreateFishing()
        {

            /// fishing testing

            // to place in spring, we remove some  other actors and objects to get fishing working, as its huge

            var springTwinIslandsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TwinIslandsSpring.FileID());
            var springTwinIsleMap = springTwinIslandsScene.Maps[0];
            // wolfos
            //springTwinIsleMap.Actors[0].ChangeActor(GameObjects.Actor.Empty); // woflos one, we want him to become fisherman
            springTwinIsleMap.Actors[0].Position = new vec16(199, 100, 809); // move fisherman to spot in the lake -50
            springTwinIsleMap.Actors[0].Rotation.y = (short) ActorUtils.MergeRotationAndFlags(-270, 0x7F);
            springTwinIsleMap.Actors[0].ChangeActor(GameObjects.Actor.OOTFishing, 0x200); // 0xFFFF is the whole thing
            springTwinIsleMap.Objects[9] = GameObjects.Actor.OOTFishing.ObjectIndex();

            springTwinIsleMap.Actors[1].ChangeActor(GameObjects.Actor.Empty); // worthless one
            springTwinIsleMap.Actors[1].OldActorEnum = GameObjects.Actor.OOTFishing;

            // tektite
            springTwinIsleMap.Actors[2].ChangeActor(GameObjects.Actor.Empty); // one whole tek
            springTwinIsleMap.Objects[1] = GameObjects.Actor.Empty.ObjectIndex();

            // goron son
            springTwinIsleMap.Actors[20].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[6] = GameObjects.Actor.Empty.ObjectIndex();

            // guay
            springTwinIsleMap.Actors[5].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[6].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[7] = GameObjects.Actor.Empty.ObjectIndex();
            // keese // why is there a keese object here?
            springTwinIsleMap.Objects[0] = 0x1AB; // either empty or we could try to spawn the proprietor
            // skullfish encounter
            springTwinIsleMap.Actors[21].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[27].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Actors[28].ChangeActor(GameObjects.Actor.Empty);
            springTwinIsleMap.Objects[8] = GameObjects.Actor.Empty.ObjectIndex();

            // nothing left for enemizer to do so it wont write the scene, we have to do that here
            SceneUtils.UpdateScene(springTwinIslandsScene);

        } // */

        private static void AddGrottoVariety()
        {
            /// turns out the grottos have unused objects, some of them can be swapped
            ///   without affecting the original enemy placement, and gives us some variety

            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());

            // dodongo grotto has a blue icecycle object, switch to BO so we can get bo actors from jp grotto
            // TODO is this actually getting updated without an actor tho?f
            var dodongoGrottoObjectList = grottosScene.Maps[7].Objects;
            dodongoGrottoObjectList[2] = GameObjects.Actor.Bo.ObjectIndex();

            // peahat grotto has a deku baba object, switch to BO so we can get bo actors from jp grotto
            var peahatGrottoObjecList = grottosScene.Maps[13].Objects;
            peahatGrottoObjecList[2] = GameObjects.Actor.Bo.ObjectIndex();
            // there is a worthless mushroom here, lets make TWO peahats :]
            var newPeahat = grottosScene.Maps[13].Actors[3];
            newPeahat.ChangeActor(GameObjects.Actor.Peahat, vars: 0, modifyOld: true);
            //newPeahat.Position = new vec16(5010, -20, 600); // move over near peahat one
            newPeahat.Position = new vec16(5010, -20, 600); // move over near peahat one

            // straight jp grotto has only one object, padding of scene data means there is space for an object right behind it that we can use
            //  we can use the second object to give this area a chest by taking one of the useless mushrooms and changing it
            // expand object list to have both of our new objects, change dekubaba to dodongo to increase likelyhood of killable
            grottosScene.Maps[6].Objects = new List<int> { GameObjects.Actor.Peahat.ObjectIndex(),
                                                           GameObjects.Actor.TreasureChest.ObjectIndex() };
            // change dekubaba to dodongo so its killable to get the new chest
            grottosScene.Maps[6].Actors[2].ChangeActor(GameObjects.Actor.Peahat, vars: 0, modifyOld: true);
            grottosScene.Maps[6].Actors[2].OldName = grottosScene.Maps[6].Actors[2].Name = "JpGrottoEnemy";
            // we have to tell the room to load the extra object though
            var straightJPGrottoRoomFile = RomData.MMFileList[GameObjects.Scene.Grottos.FileID() + 7];
            straightJPGrottoRoomFile.Data[0x29] = 0x2; // setting object header object count from 1 to 2

            var newChestActor = grottosScene.Maps[6].Actors[7];
            // chest params: should be invisible until you kill the enemy, should not collide with any other chest flags in the scene, item: dont know
            // flag 1D, type 7, item 6D (unknown)
            newChestActor.ChangeActor(GameObjects.Actor.TreasureChest, 0x26ED, modifyOld: true);
            newChestActor.Position = new vec16(-230, 0, 1130); // move into the grass area
            newChestActor.Rotation.y = ActorUtils.MergeRotationAndFlags(90, grottosScene.Maps[6].Actors[7].Rotation.y); // rotate to face the center
            // turn the other useless mushroom into another buterfly for ambiance
            grottosScene.Maps[6].Actors[8].ChangeActor(GameObjects.Actor.Butterfly, 0x5324, modifyOld: true);
            grottosScene.Maps[6].Actors[8].Position.y = 58; // dont want spawning in the ground, we want flying around

            // biobaba grotto has a worthless dekubaba object, lets swap it for the ice block object so we can freeze the water
            grottosScene.Maps[11].Objects[3] = 0x1E7; // iceflowe
        }

        public static void ExpandGoronShineObjects()
        {
            /// we cannot randomize any goron in the shrine because they all use the same object
            ///   and for some reason it crashes if there isnt one there at all, unknown reason
            /// except both rooms use the same 5 objects, and object list is padded to word length
            ///   so there is a space object space in the list we can use, we can add a second goron object which we leave alone
            if (!ReplacementListContains(GameObjects.Actor.GoronSGoro)) return;

            var goronShrine = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronShrine.FileID());
            goronShrine.Maps[0].Objects = new List<int> {
                GameObjects.Actor.GoronSGoro.ObjectIndex(),
                GameObjects.Actor.GoronKid.ObjectIndex(),
                GameObjects.Actor.FishingGameTorch.ObjectIndex(),
                GameObjects.Actor.GoronShrineChandelier.ObjectIndex(),
                GameObjects.Actor.ClayPot.ObjectIndex(),
                GameObjects.Actor.GoGoron.ObjectIndex() // add a second Generic Goron
            };
            goronShrine.Maps[1].Objects = goronShrine.Maps[0].Objects.ToList(); // think this needs a copy or its a pointer to the same list

            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 6
            var goronShrineRoom0Data = RomData.MMFileList[GameObjects.Scene.GoronShrine.FileID() + 1].Data; // 1320
            var goronShrineRoom1Data = RomData.MMFileList[GameObjects.Scene.GoronShrine.FileID() + 2].Data;
            goronShrineRoom0Data[0x31] = 6;
            goronShrineRoom1Data[0x31] = 6;
        }

        public static void RandomlySwapOutZoraBandMember()
        {
            /// almost all zora in zora hall use the same object, so we cant swap any out without hitting them all
            /// except, all band member objects are present all the time even though they only show up outside for the concert
            /// so randomly choose one to turn into a duplicate zora object, so we can change one and leave the other for door zora
            ///   since most rando players dont care about the concert anyway, and wouldnt even notice one member missing
            if (!ReplacementListContains(GameObjects.Actor.RegularZora)) return;

            // 2:japas, 3:evan, 5:tijo, can't remove lulu or the concert is completely broken? meh
            var replacableBandObj = new int[] { 2, 3, 5, 4 };
            var randomObjListIndex = replacableBandObj[seedrng.Next(replacableBandObj.Length)];
            var zoraHallScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHall.FileID());
            zoraHallScene.Maps[0].Objects[randomObjListIndex] = GameObjects.Actor.RegularZora.ObjectIndex();

            // because of this change, the whole string of watchers are all active before the dungeon too,
            //   move some down below so its not so crouded
            zoraHallScene.Maps[0].Actors[29].Position = new vec16(376, 2, 676); // down by the water
            zoraHallScene.Maps[0].Actors[27].Position = new vec16(-448, 2, -408); // behind the water fall near lulu
            zoraHallScene.Maps[0].Actors[28].Position = new vec16(-1002, 179, 1089); // near front door
        }

        public static void ExpandGoronRaceObjects()
        {
            /// we cannot randomize any goron in the racetrack because they all use the same object
            ///   this breaks the race because the racegorons cannot load their assets if their object is missing
            /// except the one room uses 7 objects, odd number, and objects are padded in the room files to dma, so we can add one more
            if (!ReplacementListContains(GameObjects.Actor.GoGoron)) return;

            var goronRace = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.GoronRacetrack.FileID());
            goronRace.Maps[0].Objects.Add(GameObjects.Actor.GoGoron.ObjectIndex()); // add a second Generic Goron
            // spring is a different setup, both need the same objects
            goronRace.Maps[1].Objects.Add(GameObjects.Actor.GoGoron.ObjectIndex()); // add a second Generic Goron


            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 6
            var goronRaceRoom0Data = RomData.MMFileList[GameObjects.Scene.GoronRacetrack.FileID() + 1].Data; // 1508
            goronRaceRoom0Data[0x31] = 8; // increase object list to 8
            // the second setup in this scene has a different object list, need to modify that onne too (690 is headers)
            goronRaceRoom0Data[0x6B9] = 8; // increase object list to 8
        }

        public static void SplitSpiderGrottoSkulltulaObject()
        {
            /// in the spider grotto, we have a skullwalltula on the web and a skulltula hanging from the ceiling
            /// this scene room has 3 objects, one is dekubaba, wasted
            /// in order to split the actor, however, I have to change the actor to something else and give it a different object

            if (!ReplacementListContains(GameObjects.Actor.Skulltula)) return;

            var grottoScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            var spiderRoom = grottoScene.Maps[1];

            spiderRoom.Objects[2] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderRoom.Actors[1].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 0, modifyOld: true);
            spiderRoom.Actors[1].OldName = spiderRoom.Actors[1].Name = "Skulltula";
            spiderRoom.Actors[1].Position.y = 200; // way too high in the ceiling, bring down a touch
        }

        public static void SplitOceanSpiderhouseSpiderObject()
        {
            /// in the ocean spiderhouse there are two actors using the same object: gold skulltula and skulltula (big spider)
            /// we cannot randomize one without the other because they both use the same object
            /// except... if we change the actor and object out for dummy, we can trick rando to allow us to change them

            if (!ReplacementListContains(GameObjects.Actor.Skulltula)) return;

            var grottoScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.OceanSpiderHouse.FileID());
            var spiderChestRoom = grottoScene.Maps[4];

            // object 6 is Bo, its not the spider object but I think thats is safer to replace in this spot
            spiderChestRoom.Objects[6] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderChestRoom.Actors[0].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 0, modifyOld: true);
            spiderChestRoom.Actors[0].OldName = spiderChestRoom.Actors[0].Name = "SkullTulla";

            var spiderStorageRoom = grottoScene.Maps[5];

            // object 9 is Stalchild, its not the spider object but I think thats is safer to replace in this spot
            spiderStorageRoom.Objects[9] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
            spiderStorageRoom.Actors[1].ChangeActor(GameObjects.Actor.SkulltulaDummy, vars: 0, modifyOld: true);
            spiderStorageRoom.Actors[1].OldName = spiderStorageRoom.Actors[1].Name = "SkullTulla";
        }

        public static void SplitPirateSewerMines()
        {
            /// The mines in the pirate fort sewer are dual type, in room 10/11 they are underwater mines,
            /// in room 9 there are ceiling hanging mines
            /// right now, actorizer cannot handle them properly in this form (we get water types in the air or air types in the water)
            /// we need to split into two separate actors and two separate objects
            /// turning the ceiling mines into fake skulltula (ceiling type) and changing the object in that room to match

            var sewerScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortressRooms.FileID());
            var actors = sewerScene.Maps[9].Actors;

            foreach (var actor in actors)
            {
                if (actor.ActorEnum == GameObjects.Actor.SpikedMine)
                {
                    actor.ChangeActor(GameObjects.Actor.SkulltulaDummy, 0, modifyOld: true);
                    actor.OldName = actor.Name = "HangingMine";
                    // ceiling type should handle this by default now
                    //actor.Position.y -= 30; // touching the ceiling, lets drop a bit
                }
            }

            sewerScene.Maps[9].Objects[5] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();
        }

        private static void SplitSnowheadTempleBo()
        {
            /// the bo in sht are in two locations: floor in the entrance and hanging from the ceiling,
            /// this is an issue because there are almost none that are dual type
            /// split the two into different enemies for better type control

            var shtScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SnowheadTemple.FileID());
            var boActors = shtScene.Maps[8].Actors;

            foreach (var actor in boActors)
            {
                if (actor.ActorEnum == GameObjects.Actor.Bo)
                {
                    actor.ChangeActor(GameObjects.Actor.SkulltulaDummy, 0, modifyOld: true);
                    actor.OldName = actor.Name = "CeilingBo";
                }
            }

            // we need to change the object to match skulltula in our code so rando knows to change the object
            shtScene.Maps[8].Objects[15] = GameObjects.Actor.SkulltulaDummy.ObjectIndex();

        }

        public static void BlockBabyGoronIfNoSFXRando()
        {
            /// the baby crying is very annoying and loud, do not allow

            if (!_cosmeticSettings.RandomizeSounds) // if not sfx rando
            {
                var bab = ReplacementCandidateList.Find(act => act.ActorEnum == GameObjects.Actor.GoronKid);
                ReplacementCandidateList.Remove(bab);
            }
        }

        public static void FixArmosSpawnPos()
        {
            /// for some reason armos changes its home and world position based on y rotation in init
            //
            // this->actor.home.pos.x -= 9.0f * Math_SinS(this->actor.shape.rot.y);
            // this->actor.home.pos.z -= 9.0f * Math_CosS(this->actor.shape.rot.y);
            // this->actor.world.pos.x = this->actor.home.pos.x;
            // this->actor.world.pos.z = this->actor.home.pos.z;
            // and it makes no sense, removing

            RomUtils.CheckCompressed(GameObjects.Actor.Armos.FileListIndex());
            var armosData = RomData.MMFileList[GameObjects.Actor.Armos.FileListIndex()].Data;

            // the four writes (home.x home.z world.x, world.z)
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0E0, val: 0x0000000); // reminder: all zero instruction is NOP
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x104, val: 0x0000000);
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0FC, val: 0x0000000);
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x110, val: 0x0000000);

            // for good measure, lets nop some of these expensive floating instructions leading to the save too
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0D4, val: 0x0000000); // mul.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0D8, val: 0x0000000); // sub.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0F4, val: 0x0000000); // mul.s
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x100, val: 0x0000000); // sub.s

            // god this compiler sucks, it LOADS the value it just stored to re-save it to a new location,
            // instead of reusing the already populated register
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0CC, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0EC, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x0F0, val: 0x0000000); // lwc
            ReadWriteUtils.Arr_WriteU32(armosData, Dest: 0x108, val: 0x0000000); // lwc
        }

        private static void FixEvanRotation()
        {
            if (!ReplacementListContains(GameObjects.Actor.Evan)) return;

            // if evan is randomized, then his replacement is staring at the wall
            var zorahallRoomsScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.ZoraHallRooms.FileID());
            var evan = zorahallRoomsScene.Maps[3].Actors[0];

            /// if ossan in trading post was randomized we want to move one of them, as there are two of the, assumed for late night
            if (evan.ActorEnum != GameObjects.Actor.Evan)
            {
                evan.Rotation.y = ActorUtils.MergeRotationAndFlags(180 + 90 + 15, flags: evan.Rotation.y);
                SceneUtils.UpdateScene(zorahallRoomsScene);
            }

        }

        private static void RandomizeTheSongMonkey()
        {
            /// we normally cannot randomize just the song monkey in the deku king chamber scene
            /// because the object is needed for multiple monkeys
            /// but the scene uses 5 objects, and since they come in pairs that means there is a free space we can add another object, adding the monkey back in

            if (!ReplacementListContains(GameObjects.Actor.Monkey)) return;

            var dekuKingScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuKingChamber.FileID());
            dekuKingScene.Maps[0].Objects.Add(GameObjects.Actor.Monkey.ObjectIndex());
            // we have to tell the room to load the extra object though
            var dekuKingSceneMap0FileData = RomData.MMFileList[GameObjects.Scene.DekuKingChamber.FileID() + 1].Data;
            dekuKingSceneMap0FileData[0x31] = 0x6; // updating object header object count from 5 to 6

            // swamp monkey are annoying, we want to move them so they dont block things
            var southernSwampScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthernSwamp.FileID());
            southernSwampScene.Maps[2].Actors[10].Position = new vec16(3826, 15, -1320); // those near witch
            southernSwampScene.Maps[2].Actors[11].Position = new vec16(3729, 15, -1358);
            southernSwampScene.Maps[2].Actors[12].Position = new vec16(3619, 15, -1367);

            southernSwampScene.Maps[0].Actors[35].Position = new vec16(380, 64, -950); // near entrance
            southernSwampScene.Maps[0].Actors[35].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 0 + 30, flags: southernSwampScene.Maps[0].Actors[35].Rotation.y);
            southernSwampScene.Maps[0].Actors[35].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
            southernSwampScene.Maps[0].Actors[35].OldName = "Monkey(Near Road)";

            southernSwampScene.Maps[0].Actors[36].Position = new vec16(499, 58, -890);
            southernSwampScene.Maps[0].Actors[36].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 270 - 30, flags: southernSwampScene.Maps[0].Actors[36].Rotation.y);
            southernSwampScene.Maps[0].Actors[36].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
            southernSwampScene.Maps[0].Actors[36].OldName = "Monkey(Near Road)";

            southernSwampScene.Maps[0].Actors[37].Position = new vec16(399, 46, -828); // this one is weirdly alright as is for rotation
            southernSwampScene.Maps[0].Actors[37].ChangeActor(GameObjects.Actor.Bombiwa, vars: 0xE, modifyOld: true);
            southernSwampScene.Maps[0].Actors[37].OldName = "Monkey(Near Road)";

            // because we changed the monkey to bombiwa actor, we need to change the object to so that they will respond correctly
            southernSwampScene.Maps[0].Objects[2] = GameObjects.Actor.Bombiwa.ObjectIndex();

            // same with monkey near the deky palace entrance
            southernSwampScene.Maps[1].Actors[34].Position = new vec16(-681, 32, 4142);
            southernSwampScene.Maps[1].Actors[34].ChangeActor(GameObjects.Actor.Snapper, vars: 0x0, modifyOld: true);
            southernSwampScene.Maps[1].Actors[34].OldName = "Monkey(Palace Entrance)";
            southernSwampScene.Maps[1].Objects[2] = GameObjects.Actor.Bombiwa.ObjectIndex();

            var dekuPalaceScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.DekuPalace.FileID());
            dekuPalaceScene.Maps[0].Actors[11].Position = new vec16(-74, 0, 1466);
            dekuPalaceScene.Maps[0].Actors[11].Rotation.y = ActorUtils.MergeRotationAndFlags(rotation: 45, flags: dekuPalaceScene.Maps[0].Actors[11].Rotation.y);
        }

        public static void MoveTheISTTTunnelTransitionBack()
        {
            /// the room tranition for the scene is very close to the edge of the dexihand
            /// this presents a problem for enemizer if playing no hit rules

            var isttSceneData = RomData.MMFileList[GameObjects.Scene.InvertedStoneTowerTemple.FileID()].Data;
            isttSceneData[0xD7] = 0xBC; // 294 -> 2BC, from pos.x = 660 to 700
            var sceneClass = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.InvertedStoneTowerTemple.FileID());
            // move the switch a little up the hallway
            sceneClass.Maps[3].Actors[28].Position.x = 800;
        }

        private static void FixSwordSchoolPotRandomization()
        {
            /// we cannot randomize the pots in swordschool because its dungeon keep object pots,
            ///   that means those pots require dungeon keep which we cannot swap out, and actorizer quits early when it cannot find the object for these
            /// however the pots just need a regular pot object, its a small scene with space for one, and the object list has 7 objects
            ///   which means we can expand the list and add another pot object

            if (!ReplacementListContains(GameObjects.Actor.ClayPot)) return;

            var swordSchoolScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SwordsmansSchool.FileID());
            swordSchoolScene.Maps[0].Objects.Add(GameObjects.Actor.ClayPot.ObjectIndex()); // add clay pot object

            // room file header 0xB describes object list offset in the file, but also describes size to load into memory, need to increase to 8
            var swordSchoolRoom0 = RomData.MMFileList[GameObjects.Scene.SwordsmansSchool.FileID() + 1].Data; // 1434
            swordSchoolRoom0[0x29] = 8; // increase object list to 8
        }

        private static void SplitSceneSnowballIntoTwoActorObjects()
        {
            /// because the large snowballs in road to mountain village count as a logic gate, we dont want them randomized
            /// but not randomizing them means we never randomize the small snowballs, this is lame
            /// so we take the snapper object in the same room and replace it with another large snowball object, we're free

            // if small snowball is randomized
            if (!ReplacementListContains(GameObjects.Actor.SmallSnowball)) return;

            var roadToMountainVillageScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PathToMountainVillage.FileID());

            roadToMountainVillageScene.Maps[0].Objects[3] = GameObjects.Actor.LargeSnowball.ObjectIndex();

            // the other large snowballs that are not part of the roadblock can be randomized,
            // we just need to turn them into small snowballs so rando finds them
            var largeSnowballsToConvert = new List<int> { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 41, 42, 43, 44, 45, 46, 47, 48, };
            foreach (var index in largeSnowballsToConvert)
            {
                var snowball = roadToMountainVillageScene.Maps[0].Actors[index];
                snowball.ChangeActor(GameObjects.Actor.SmallSnowball, vars: 0x7F3F, modifyOld: true);
                snowball.OldName = snowball.Name = "RandomizedLargeSnowball";
            }

        }


        private static void SwapIntroSeth()
        {
            /// for actorizer, seth is a very visible part of the intro and we want to randomize
            ///  but we do not want to randomize the actual seth in sct because he hints the rewards for the spiderhouse, which is kinda important

            if (!ReplacementListContains(GameObjects.Actor.Seth1)) return;

            var sctScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.SouthClockTown.FileID());
            var introSeth = sctScene.Maps[3].Actors[2];
            introSeth.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
            introSeth.OldName = "IntroSeth";

            // change object
            sctScene.Maps[3].Objects[14] = GameObjects.Actor.DekuBaba.ObjectIndex();
        }

        private static void SwapPiratesFortressBgBreakwall()
        {
            /// BgBreakwall is an amalgamash actor that can use 10 different objects, its crazy
            /// in pirates fortress center square its used to make multiple un-breakable crates
            /// because of the multi-object behavior its easier to change the type here to match the crate,
            /// esp since we can't remove the breakwall object its used for doors here

            if (!ACTORSENABLED) return;

            var piratesFortressScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.PiratesFortress.FileID());
            for (int m = 0; m < piratesFortressScene.Maps.Count; m++)
            {
                var map = piratesFortressScene.Maps[m];
                for (int a = 0; a < map.Actors.Count; a++)
                {
                    var actor = map.Actors[a];
                    if (actor.ActorEnum == GameObjects.Actor.Bg_Breakwall)
                    {
                        actor.ChangeActor(GameObjects.Actor.LargeWoodenCrate, vars: 0x7F3F, modifyOld: true);
                        actor.OldName = "BgBreakwall";
                    }
                }
            }
        }

        private static void SwapCreditsCremia()
        {
            /// cremia in the credits is in the ranch, and the ranch cremia randomization is tied to actual checks
            /// we want to swap the cremia actor in the credits for variety, we have to change the actor and object to not confuse actorizer with the regular cremias

            if (!ReplacementListContains(GameObjects.Actor.Cremia)) return;

            var ranchScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.RomaniRanch.FileID());
            var creditsCremia = ranchScene.Maps[2].Actors[11];
            creditsCremia.ChangeActor(GameObjects.Actor.DekuBaba, vars: 0, modifyOld: true);
            creditsCremia.OldName = "CreditsCremia";

            // and change the object in just that map to match
            ranchScene.Maps[2].Objects[5] = GameObjects.Actor.DekuBaba.ObjectIndex();
        }

        private static void DistinguishLogicRequiredDekuFlowers()
        {
            // for objectless actorizer, some deku flowers must be held back because they require logic, but all deku flowers use the same params
            // but the 0xFF param space is unused, so we dont have to worry about changing it to mark our requirements

            var tfScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.TerminaField.FileID());
            var aboveCowGrottoFlower = tfScene.Maps[0].Actors[48];
            aboveCowGrottoFlower.OldVariant = aboveCowGrottoFlower.Variants[0] = 0x0077;
        }

        private static void EnableAllCreditsCutScenes()
        {
            /// some people enjoy seeing the actors randomized in the credits
            /// however this is determined if the player found the given mask for the cutscene
            /// we can disable this so that it always shows the cutscene irregardless if the mask exists in the inventory

            if (!ACTORSENABLED) return;

            var codeFile = RomData.MMFileList[31].Data; // file offset: 045C38 vram: 800EB6F8

            // the code is verbatim: if mask == mask: go to scene, else: to go cutscene scene instead
            // can just turn nop the branch-if-not-equal and always run the first block of code

            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045CE0, 0x00000000); // kamaro
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045D48, 0x00000000); // great fairy
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045DB0, 0x00000000); // romani mask
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045E18, 0x00000000); // blast mask
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045E80, 0x00000000); // circus leader
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045EE8, 0x00000000); // breman mask
            // section for ikana that doesnt care about mask showing the king
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045F84, 0x00000000); // couples
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x045FEC, 0x00000000); // bunny
            ReadWriteUtils.Arr_WriteU32(codeFile, 0x046054, 0x00000000); // postman
        }


#endregion

        public static List<GameObjects.Actor> GetSceneFairyDroppingEnemyTypes(SceneEnemizerData thisSceneData)
        {
            /// Reads the list of specific actors of fairies, checks the list of actors we read from the scene, gets the actor types for GetMatches
            /// why? because our object focused code needs to whittle the list of actors for a enemy replacement, 
            ///   but has to know if even one enemy is used for fairies that it cannot be unkillable
            /// doing that last second, per-enemy, would be expensive, so we need to check per-scene
            /// we COULD hard code these types into the scene data, but if someone in the distant future
            ///   doesn't realize they have to add both, might be a hard bug to find

            var actorsThatDropFairies = thisSceneData.Scene.SceneEnum.GetSceneFairyDroppingEnemies();
            var returnActorTypes = new List<GameObjects.Actor>();
            var actorList = thisSceneData.Actors;
            for (int actorNum = 0; actorNum < actorList.Count; ++actorNum)
            {
                for (int fairyRoom = 0; fairyRoom < actorsThatDropFairies.Count; ++fairyRoom)
                {
                    if (thisSceneData.Actors[actorNum].Room == actorsThatDropFairies[fairyRoom].roomNumber
                      && actorsThatDropFairies[fairyRoom].actorNumbers.Contains(actorList[actorNum].RoomActorIndex))
                    {
                        returnActorTypes.Add((GameObjects.Actor)actorList[actorNum].ActorId);
                    }
                }
            }
            return returnActorTypes;
        }

        public static void SetupGrottoActor(Actor enemy, int newVariant)
        {
            /// Grottos can get their address index from an array, where the index can be their Z rotation.
            ///   so we re-encoded variants to hold the data we want, check out the actor enum entry for more info
            ///   the lower two bytes are used to set the chest, but we have a chest grotto with upper byte index, so reuse for rotation here
            ///   the game does not use the top two bits of the second byte, so we use one as a flag for rotation type grottos
            ///   we also set the time flags to always, because it makes no sense for a hole to only exist day or night, holes are forever
            enemy.ChangeActor(GameObjects.Actor.GrottoHole, vars: newVariant);
            //if ((newVariant & 0x0400) != 0) // grotto that uses rotation to set value
            {
                int newIndex = newVariant & 0xF; // in vanilla the array is only 15 long
                enemy.Rotation.x = ActorUtils.MergeRotationAndFlags(rotation: 0, flags: 0x7F);
                enemy.Rotation.z = ActorUtils.MergeRotationAndFlags(rotation: newIndex, flags: 0x7F);//: enemy.Rotation.z);
            }
        }

        // can we move this to actorUtils?
        public static void FixPathingVars(SceneEnemizerData thisSceneData)
        {
            /// Pathing actors need to have their paths updated to match the previous actor.

            var chosenReplacementEnemies = thisSceneData.Actors;

            for (int i = 0; i < chosenReplacementEnemies.Count; i++)
            {
                Actor actor = chosenReplacementEnemies[i];
                var newType = actor.ActorEnum.GetType(actor.Variants[0]);

                if (!(actor.Type == GameObjects.ActorType.Pathing // set on scene actor load
                  && newType == GameObjects.ActorType.Pathing))  // pulled from replacement vars
                {
                    continue; // not pathing situation: do not update pathing values
                }

                var oldPathBehaviorAttr = actor.OldActorEnum.GetAttribute<PathingTypeVarsPlacementAttribute>();
                var newdoldPathBehaviorAttr = actor.ActorEnum.GetAttribute<PathingTypeVarsPlacementAttribute>();

                // retreive the path value from the old variant
                var oldVariant = actor.OldVariant;
                var oldPathShifted = (oldVariant & (oldPathBehaviorAttr.Mask)) >> oldPathBehaviorAttr.Shift;
                if (oldPathBehaviorAttr == null || newdoldPathBehaviorAttr == null)
                {
                    oldPathShifted = 0; // backup for actors not configured correctly
                }

                // clear the old path from this vars
                var newVarsWithoutPath = actor.Variants[0] & ~newdoldPathBehaviorAttr.Mask;

                // shift the path into the new location
                var newPath = oldPathShifted << newdoldPathBehaviorAttr.Shift;

                // set variant from cleaned old variant ORed against the new path
                actor.Variants[0] = newVarsWithoutPath | newPath;
            }
        }

        public static void FixKickoutEnemyVars(SceneEnemizerData thisSceneData)
        {
            /// Two actors in the game will attempt to catch and "kickout" the player: DekuPatrolGuard and PatrollingPirate
            /// Both actors need their kickout values to be something safe or even understandable, and not crashing.

            // separated from pathing since its only two actors and we want to change kick for variants that do not path
            var objectsContainKickoutActors = thisSceneData.ChosenReplacementObjects.Find(objSwap =>
                                                         objSwap.ChosenV == GameObjects.Actor.PatrollingPirate.ObjectIndex() ||
                                                         objSwap.ChosenV == GameObjects.Actor.DekuPatrolGuard.ObjectIndex()
                                                    ) != null;
            if (!objectsContainKickoutActors) { return; }

            for (int i = 0; i < thisSceneData.Actors.Count; i++)
            {
                Actor actor = thisSceneData.Actors[i];

                // note: the two actors use slightly different kickout methods
                // for now, pass ZERO to both actors (use the main exit per area, all areas have at least one)
                // it should give us a basic entrance to work with that wont crash anywhere where pathing enemies can exist
                var newKickoutAttr = actor.ActorEnum.GetAttribute<PathingKickoutAddrVarsPlacementAttribute>();
                if (newKickoutAttr != null)
                {
                    int kickoutAddr = 0; // safest bet, there should always be at least one exit address per scene
                    if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.ZoraHall)
                    {
                        kickoutAddr = 1; // zora hall exit 0 is out the water door, softlock if you dont have zora or enough health
                    }

                    // erase the kick location from the old vars
                    int kickoutMask = newKickoutAttr.Mask << newKickoutAttr.Shift;
                    var newVarsWithoutKick = actor.Variants[0] & ~(kickoutMask);

                    // replace with new address
                    var newVarsWithKick = newVarsWithoutKick | (kickoutAddr << newKickoutAttr.Shift);
                    actor.Variants[0] = newVarsWithKick;
                }
            }
        }


        public static void FixRedeadSpawnScew(SceneEnemizerData thisSceneData)
        {
            /// If a redead tries to spawn with a x or z rotation they can fall right through the floor once they start moving.
            /// We need to fix that, but too many possible spawns could have this actor, do it dynamically

            var redeadObjDetected = thisSceneData.ChosenReplacementObjects.Find(v => v.ChosenV == GameObjects.Actor.GibdoWell.ObjectIndex()) != null;

            if (!redeadObjDetected) return;

            for (int i = 0; i < thisSceneData.Actors.Count(); i++)
            {
                var testActor = thisSceneData.Actors[i];
                if (testActor.ActorEnum == GameObjects.Actor.ReDead || testActor.ActorEnum == GameObjects.Actor.GibdoWell)
                {
                    ActorUtils.FlattenPitchRoll(testActor);
                }
            }
        }

        private static bool TrimDynaActors(SceneEnemizerData thisSceneData, StringBuilder dynaLog)
        {
            /// too much dyna crashes the game, so we want to trim some of our dyna actors, removing them or turning them into something benign

            /// TODO move this to a better spot in the code

            void TrimSmaller(List<List<Actor>> shrinkTargets, List<List<Actor>> markForFinished)
            {
                // we want to randomize the list so that its not always the same order we remove actors by category, in case we have repeats
                //shrinkTargets = shrinkTargets.OrderBy(x => thisSceneData.RNG.Next()).ToList();
                // until such a time as we can detect (actor A exists in both lists and was removed earlier) this is a bit pre-mature, we always remove one from all lists for now

                // remove one from all of the list of lists
                for (int l = 0; l < shrinkTargets.Count; l++)
                {
                    //target actor list PerRoomTypeAndTime
                    var list = shrinkTargets[l];
                    if (list.Count <= 1) // in a previous loop we shrank this one too mininum already, ignore
                        continue; // this is probably no longer needed 

                    var randomlyChosenActor = list[thisSceneData.RNG.Next() % list.Count];
                    var currentRoom = randomlyChosenActor.Room;

                    dynaLog.AppendLine($" -- dyna overload trimmed actor [{randomlyChosenActor.Name}] on previous [{randomlyChosenActor.OldName}]" +
                                                $" in map [{currentRoom}] index [{randomlyChosenActor.RoomActorIndex}]");

                    var roomActors = thisSceneData.Actors.FindAll(a => a.Room == randomlyChosenActor.Room);

                    // there is a lot of shlock here that I didn't realize, hopefully doesn't slow us down too much
                    var blockedActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(randomlyChosenActor.OldActorEnum);
                    var roomFreeActors = GetRoomFreeActors(thisSceneData, randomlyChosenActor.Room);
                    // this is a hack, just assume if they have limits we shouldn't use them for this last second replacement
                    roomFreeActors.RemoveAll(actor => actor.DynaLoad.poly > 0
                                                    || (actor.Variants.Count() > 0 && actor.VariantMaxCountPerRoom(actor.Variants[0]) > 1));
                    List<Actor> acceptableReplacementFreeActors = roomFreeActors.FindAll(a => !blockedActors.Contains(a.ActorEnum)).ToList();
                    //var acceptableReplacementFreeActors = roomFreeActors.Except(blockedActors).ToList(); // damned default comparator
                    EmptyOrFreeActor(thisSceneData, randomlyChosenActor, roomActors, acceptableReplacementFreeActors,
                        roomIsClearPuzzleRoom: true); // for now marking this true just because I dont want to re-calculate this since its in the wrong spot,
                                                      // dont bother doing this for last second dyna removal
                                                        // we may have fucked up putting this in the wrong layer

                    dynaLog.AppendLine($" --  replaced with  [{randomlyChosenActor.Name}] ");

                    list.Remove(randomlyChosenActor);

                    if (list.Count <= 1) // too small to continue to remove, leave alone
                    {
                        markForFinished.Add(list);
                    }

                    // test if dyna is still an issue, if not remove list
                    var act = thisSceneData.ActorCollection;
                    act.SetNewActors(thisSceneData.Scene, thisSceneData.AllObjects); // have to update dyna values for the later functions to work

                    //act.newMapList[currentRoom].day.DynaPolySize
                    var dayOverloaded = act.isDynaOverLoaded(act.newMapList[currentRoom].day, act.oldMapList[currentRoom].day, currentRoom);
                    var nightOverloaded = act.isDynaOverLoaded(act.newMapList[currentRoom].night, act.oldMapList[currentRoom].night, currentRoom);
                    if (!dayOverloaded && !nightOverloaded)
                    {
                        markForFinished.Add(list); // this room should be done, so this list should be removed, it might still cull from the other actors in this room tho
                    }
                } // end for each list of lists
            } // end trim smaller

            void TrimPass(List<List<Actor>> shrinkTargets)
            {
                while (shrinkTargets.Count > 0)
                {
                    var markForFinished = new List<List<Actor>>();

                    TrimSmaller(shrinkTargets, markForFinished);

                    for (int l = 0; l < markForFinished.Count; l++)
                    {
                        shrinkTargets.Remove(markForFinished[l]);
                    }

                    // alt: we test once per pass instead of per actor removal
                }
            }

            // first pass: scan through large lists of large actors first, they are the biggest offenders
            /// shrinkableActorsList is a list of lists, where each list is all actors of the same type in the same room/day/night combo
            var shrinkableActorsList = thisSceneData.ActorCollection.GenerateShrinkableDynaList();
            shrinkableActorsList.RemoveAll(list => list.Count <= 13);
            TrimPass(shrinkableActorsList);

            shrinkableActorsList = thisSceneData.ActorCollection.GenerateShrinkableDynaList();
            shrinkableActorsList.RemoveAll(list => list.Count <= 5);
            TrimPass(shrinkableActorsList);

            // second pass: no, all of the large lists werent the issue, we still need to trim the small lists
            shrinkableActorsList = thisSceneData.ActorCollection.GenerateShrinkableDynaList();
            TrimPass(shrinkableActorsList);

            return false;
        }

        public static void FinalActorLimitTrim(SceneEnemizerData thisSceneData)
        {
            /// the final trim where we go through every actor that might be over their limit and randomly remove them
            /// this needs to happen because during the last two, we didnt dynamically keep track of actors being put back in

            for (int m = 0; m < thisSceneData.ActorCollection.newMapList.Count; m++)
            {
                var map = thisSceneData.ActorCollection.newMapList[m];

                // per day/night
                var dayActorList = thisSceneData.Actors.Intersect(map.day.oldActorList).ToList();
                var dayUniqueList = dayActorList.GroupBy(elem => elem.ActorEnum).Select(group => group.First()).ToList();
                dayUniqueList.RemoveAll(u => u.ActorEnum == GameObjects.Actor.Empty);
                var debugSpots = dayActorList.FindAll(act => act.OldActorEnum == GameObjects.Actor.HitSpot || act.OldActorEnum == GameObjects.Actor.WallTalkSpot);
                for (int a = 0; a < dayUniqueList.Count; a++)
                {
                    var uniqueActor = new List<Actor> { dayUniqueList[a] };
                    var specificActorList = dayActorList.FindAll(act => act.ActorEnum == uniqueActor[0].ActorEnum);
                    TrimAllActors(thisSceneData, uniqueActor, specificActorList, allowLimits: false);
                }

                var nightActorList = thisSceneData.Actors.Intersect(map.night.oldActorList).ToList();
                var nightUniqueList = nightActorList.GroupBy(elem => elem.ActorEnum).Select(group => group.First()).ToList();
                nightUniqueList.RemoveAll(u => u.ActorEnum == GameObjects.Actor.Empty);
                //TrimAllActors(thisSceneData, nightUniqueList, nightActorList, allowLimits: false);
                for (int a = 0; a < nightUniqueList.Count; a++)
                {
                    var uniqueActor = new List<Actor> { nightUniqueList[a] };
                    var specificActorList = nightActorList.FindAll(act => act.ActorEnum == uniqueActor[0].ActorEnum);
                    TrimAllActors(thisSceneData, uniqueActor, specificActorList, allowLimits: false);
                }
            }
        }

        public static void FixBrokenActorSpawnCutscenes(SceneEnemizerData thisSceneData)
        {
            /// Each Actor spawn gets one cutscene in the scene/room data
            /// if a dinofos is spawned, and has a cutscene from the room spawn data, it plays the cutscene
            ///   (supposed to be the drop from ceiling cutscene) but it breaks the game
            /// so we have to disable it for any new dinofos spawns to avoid
            /// also other trouble actors that can take that cutscene and do things we dont want
            /// tag: fix cutscene actors

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.ClockTowerInterior)
                return; // I think its funny that the cutscenes can activate HMS song of healing cutscene, so I want to leave this

            var listTroubleActorsObj = new List<int> {
                GameObjects.Actor.Dinofos.ObjectIndex(),
                GameObjects.Actor.Scarecrow.ObjectIndex(),
                GameObjects.Actor.PatrollingPirate.ObjectIndex(),
                GameObjects.Actor.GossipStone.ObjectIndex(),
                GameObjects.Actor.LabFish.ObjectIndex(),
                GameObjects.Actor.Lightblock.ObjectIndex(),
                GameObjects.Actor.SkullKidPainting.ObjectIndex(),
                GameObjects.Actor.LaundryPoolBell.ObjectIndex(),
                GameObjects.Actor.AnjusGrandmaCredits.ObjectIndex(),
                GameObjects.Actor.Japas.ObjectIndex(),
                GameObjects.Actor.Tingle.ObjectIndex(),
                GameObjects.Actor.SleepingScrub.ObjectIndex(),
                GameObjects.Actor.ElegyStatueSwitch.ObjectIndex(),
                GameObjects.Actor.Evan.ObjectIndex(),
                GameObjects.Actor.GaboraBlacksmith.ObjectIndex(),
                GameObjects.Actor.IronKnuckle.ObjectIndex(),
                GameObjects.Actor.En_Owl.ObjectIndex(),
                GameObjects.Actor.GoronWithGeroMask.ObjectIndex()
            };

            var actorObjectsDetected = thisSceneData.ChosenReplacementObjects.Find(v => listTroubleActorsObj.Contains(v.ChosenV)) != null;

            // if field, we can have grottos, which should be checked for too
            if (!actorObjectsDetected && thisSceneData.Scene.SpecialObject != Scene.SceneSpecialObject.FieldKeep) return;

            var listTroubleActors = new List<GameObjects.Actor> {
                GameObjects.Actor.Dinofos,
                GameObjects.Actor.Scarecrow,
                GameObjects.Actor.PatrollingPirate,
                GameObjects.Actor.Tingle,
                GameObjects.Actor.GrottoHole,
                GameObjects.Actor.GossipStone,
                GameObjects.Actor.LabFish,
                GameObjects.Actor.Lightblock,
                GameObjects.Actor.SkullKidPainting,
                GameObjects.Actor.LaundryPoolBell,
                GameObjects.Actor.AnjusGrandmaCredits,
                GameObjects.Actor.Japas,
                GameObjects.Actor.Tingle,
                GameObjects.Actor.SleepingScrub,
                GameObjects.Actor.ElegyStatueSwitch,
                GameObjects.Actor.Evan,
                GameObjects.Actor.GaboraBlacksmith,
                GameObjects.Actor.IronKnuckle,
                GameObjects.Actor.En_Owl,
                GameObjects.Actor.GoronWithGeroMask
            };

            for (int i = 0; i < thisSceneData.Actors.Count(); i++) // thisSceneData.Actors is only the actors we change
            {
                var testActor = thisSceneData.Actors[i];
                //if (listTroubleActors.Contains(testActor.ActorEnum)) // testing: what if we just remove cutscene for all of our placed actors
                {
                    // remove the spawn data by setting spawn to 0x7F (-1)
                    testActor.Rotation.y |= 0x7F;
                }
            }
        }


        public static Actor FindStrayFairy(SceneEnemizerData thisSceneData, int x, int z)
        {
            var scene = thisSceneData.Scene;
            for (int m = 0; m < scene.Maps.Count; m++)
            {
                var actors = scene.Maps[m].Actors;
                for (int a = 0; a < actors.Count; a++)
                {
                    var actor = actors[a];
                    if (actor.ActorEnum == GameObjects.Actor.StrayFairy && actor.Position.x == x && actor.Position.z == z)
                    {
                        return actor;
                    }
                }
            }

            return null;
        }

        public static void ActorizerForceDropHeavyGrassMinimum(SceneEnemizerData thisSceneData)
        {
            /// people are complaining that in high sanity they need at least one place where they can get drops of some kind

            GameObjects.Scene[] scenesToForce = new GameObjects.Scene[]{
                GameObjects.Scene.TerminaField,
                GameObjects.Scene.GreatBayCoast,
                GameObjects.Scene.IkanaCanyon,
                GameObjects.Scene.IkanaGraveyard 
            };

            if (ACTORSENABLED == true
             //&& _randomized.Settings.LogicMode != Models.LogicMode.NoLogic // requested off to test
             && scenesToForce.Contains(thisSceneData.Scene.SceneEnum))
            {
                #if DEBUG
                var debuggingActorList = thisSceneData.Actors;
                #endif

                var firstRestrictions = thisSceneData.Actors.FindAll(act => act.Type == GameObjects.ActorType.Ground || act.Type == GameObjects.ActorType.Pathing);
                if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.TerminaField || thisSceneData.Scene.SceneEnum == GameObjects.Scene.GreatBayCoast)
                {
                    firstRestrictions = firstRestrictions.FindAll(act => act.Room == 0); // these scenes have hidden second rooms that are harder to reach
                }
                if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.GreatBayCoast)
                {
                    //firstRestrictions.RemoveAll(act => act.ActorEnum == GameObjects.Actor.RainbowHookshotPillar); // out of range
                    firstRestrictions = firstRestrictions.FindAll(act => act.OldActorEnum == GameObjects.Actor.Leever); // tired of this alg keeps putting it in stupid locations ENOUGH
                }
                if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.IkanaCanyon)
                {
                    firstRestrictions.RemoveAll(act => act.OldActorEnum == GameObjects.Actor.Guay); // this should be flying not ground type, TODO fix
                }


                // find one actor that is either empty or standalone
                // and ground variety
                var replacementCandidates = firstRestrictions.FindAll(act =>   thisSceneData.StandaloneActors.Contains(act)
                                                                       || act.ActorEnum == GameObjects.Actor.Empty);

                if (replacementCandidates.Count == 0) // did not find empty or standalone ground types we could replace
                {
                    /// lets try actors that are special object
                    replacementCandidates = firstRestrictions.FindAll(act => act.ActorEnum.ObjectIndex() <= 3);
                }

                if (replacementCandidates.Count == 0) // did not find any cheap object actors
                {
                    /// lets try actors that have lots of copies, those can sometimes have too many
                    //var sortedGroups = firstRestrictions.OrderBy(x => x).GroupBy(x => x.ActorEnum);
                    //List<List<Actor>> bucketSortedList = sortedGroups.Select(g => g.ToList()).ToList(); // not working and I dont know this system enough to get it working, screw it writing it manually
                    List<(int type, int count)> bucketList = new List<(int type, int count)>();
                    foreach (var actor in firstRestrictions)
                    {
                        var searchIndex = bucketList.FindIndex(bucket => bucket.type == actor.ActorId);
                        if (searchIndex == -1) // not in bucketlist yet, add new bucket
                        {
                            bucketList.Add((actor.ActorId, 1));
                        }
                        else // previously existing bucket
                        {
                            var countPtr = bucketList[searchIndex].count; // can't inline increment tuple value, c# weirdness
                            countPtr++;
                        }
                    }

                    var largestIndex = 0;
                    for(int i = 0; i < bucketList.Count; i++)
                    {
                        var newList = bucketList[i];
                        var oldList = bucketList[largestIndex];
                        if (newList.count > oldList.count)
                        {
                            largestIndex = i;
                        }
                    }
                    replacementCandidates = firstRestrictions.FindAll( act => act.ActorId == bucketList[largestIndex].type);
                }

                if (replacementCandidates.Count == 0)//Debug.Assert(replacementCandidates.Count > 0);
                    throw new Exception("Could not place supply bush, please try another seed.");

                // change them to a bush containing things
                var actorChoice = replacementCandidates[thisSceneData.RNG.Next(replacementCandidates.Count)];

                thisSceneData.Log.AppendLine($" +++ BUSH SUPPLIES at index:[{actorChoice.RoomActorIndex}]"
                           + $" replacing new choice [{actorChoice.Name}][{actorChoice.Variants[0].ToString("X4")}]"
                           + $" where old actor was [{actorChoice.OldName}][{actorChoice.OldVariant.ToString("X4")}] ");

                // dont need to modify old as this happens dead last
                actorChoice.ChangeActor(GameObjects.Actor.NaturalPatchOfGrass, vars:0x0001, modifyOld: false);
                actorChoice.Position.y += 50; // just in case the previous actor is more under the floor than exactly on the floor, bushes could fall through
                actorChoice.Rotation.x = ActorUtils.MergeRotationAndFlags(rotation: 0, flags: 0x7F); // set to spawn all day all night every day
                actorChoice.Rotation.z = ActorUtils.MergeRotationAndFlags(rotation: 0, flags: 0x7F); // set to spawn all day all night every day
            }
        }


        public static void FixGroundToFlyingActorHeights(SceneEnemizerData thisSceneData, StringBuilder log)
        {
            /// For variety, I wanted to be able to put flying enemies where ground enemies used to be.
            /// (the inverse is also interesting in idea, but harder to apply without micro-types)
            ///   however, sometimes the swap is weird because the flying enemy is too close to the ground, or IN the ground
            /// So, for some flying types, they will have values to specify they should be automatically raised
            ///   a bit higher than their ground spawn which is almost always the floor

            //TODO this ONLY USES ENUM VARIANTS uhhh we shouldnt do this

            void UpdateStrayFairyHeight(Actor testActor)
            {
                if (thisSceneData.Scene.SceneEnum.IsFairyDroppingEnemy(roomNum: testActor.Room, actorNum: testActor.RoomActorIndex))
                {
                    var testStrayFairy = FindStrayFairy(thisSceneData, testActor.Position.x, testActor.Position.z);
                    if (testStrayFairy != null)
                    {
                        testStrayFairy.Position.y = testActor.Position.y;
                    }
                }
            }

            log.AppendLine(" Height adjustments: ");

            for (int actorIndex = 0; actorIndex < thisSceneData.Actors.Count(); actorIndex++)
            {
                var testActor = thisSceneData.Actors[actorIndex];

                var flyingVariants = testActor.ActorEnum.GetAttribute<FlyingVariantsAttribute>();
                var oldGroundVariants = testActor.OldActorEnum.GetAttribute<GroundVariantsAttribute>();
                var oldWaterSurfaceVariants = testActor.OldActorEnum.GetAttribute<WaterTopVariantsAttribute>();
                var oldPathVariants = testActor.OldActorEnum.GetAttribute<PathingVariantsAttribute>();
                // if previous spawn was ground and the replacement actor has an attribute, adjust height
                // bug: type for bee in mountain spring is FLYING, should be ground, todo fix
                if ((flyingVariants != null && flyingVariants.Variants.Contains(testActor.Variants[0])) && // chosen variant is flying
                    ((oldGroundVariants != null && oldGroundVariants.Variants.Contains(testActor.OldVariant)) // previous ground
                     || (oldPathVariants != null && oldPathVariants.Variants.Contains(testActor.OldVariant)) // previous pathing(ground)
                     || (oldWaterSurfaceVariants != null && oldWaterSurfaceVariants.Variants.Contains(testActor.OldVariant)) // water surface too
                     || (oldWaterSurfaceVariants != null && oldWaterSurfaceVariants.Variants.Contains(testActor.OldVariant)) // water surface too
                     || testActor.OldActorEnum == GameObjects.Actor.ClayPot // dungeon pots dont show up as ground types, need to be a special spot here
                     || testActor.OldActorEnum == GameObjects.Actor.TallGrass // field tall grass dont show up as ground types, need to be a special spot here
                      || testActor.OldActorEnum == GameObjects.Actor.BlueBubble)) // our new actor can fly
                {
                    // if attribute exists, we need to adjust
                    // todo we might want to add as injected actor, in which case this would be loading once
                    var attr = testActor.ActorEnum.GetAttribute<FlyingToGroundHeightAdjustmentAttribute>();
                    if (attr != null)
                    {
                        testActor.Position.y += (short)attr.Height;

                        log.AppendLine($" + adjusted height of actor [{testActor.Name}] by [{attr.Height}]");
                        UpdateStrayFairyHeight(testActor);
                    }
                }

                var waterVariants = testActor.ActorEnum.GetAttribute<WaterVariantsAttribute>();
                if ((waterVariants != null && waterVariants.Variants.Contains(testActor.Variants[0])) && // chosen variant is water (swimming)
                    (oldWaterSurfaceVariants != null && oldWaterSurfaceVariants.Variants.Contains(testActor.OldVariant))) // previous water surface 
                {
                    short randomHeight = (short)(10 + (seedrng.Next() % 20));
                    testActor.Position.y -= randomHeight; // always lower flying enemies on ceiling placement, its usually way too high
                    log.AppendLine($" - lowered height of actor [{testActor.Name}] by [{randomHeight}] to lower below water surface");
                    UpdateStrayFairyHeight(testActor);
                }

                var oldCeilingVariants = testActor.OldActorEnum.GetAttribute<CeilingVariantsAttribute>();
                if ((flyingVariants != null && flyingVariants.Variants.Contains(testActor.Variants[0])) && // chosen variant is flying
                    (oldCeilingVariants != null && oldCeilingVariants.Variants.Contains(testActor.OldVariant))) // previous ceiling 
                {
                    short randomHeight = (short)(50 + (seedrng.Next() % 50));
                    testActor.Position.y -= randomHeight; // always lower flying enemies on ceiling placement, its usually way too high
                    log.AppendLine($" - lowered height of actor [{testActor.Name}] by [{randomHeight}] from ceiling to fly");
                    UpdateStrayFairyHeight(testActor);
                }
                // special case: chain mine trap is too low from ceiling
                if(oldCeilingVariants != null && testActor.ActorEnum == GameObjects.Actor.SpikedMine)
                {
                    // chain is too long, this is annoying, raise the actor to be a tad higher so more of its chain is in the ceiling
                    testActor.Position.y += 100;
                }

                var wallVariants = testActor.OldActorEnum.GetAttribute<WallVariantsAttribute>();
                // for now I want this manually just for dexihand: rotate forward a touch because its on a wall
                if (testActor.ActorEnum == GameObjects.Actor.Dexihand && testActor.OldActorEnum != GameObjects.Actor.Dexihand
                    && wallVariants != null && wallVariants.Variants.Contains(testActor.OldVariant))
                {
                    testActor.Rotation.x = ActorUtils.MergeRotationAndFlags(60, flags: testActor.Rotation.x); // pitch rotation down a bit
                }
                // special case: monkey spawns with an extra height offset from the floor, not at the location of the visible model
                if (testActor.ActorEnum == GameObjects.Actor.Monkey && testActor.Variants[0] == 0x02FF
                    && wallVariants != null && wallVariants.Variants.Contains(testActor.OldVariant))
                {
                    testActor.Position.y -= 90; // too high annoyingly
                }


            }
            thisSceneData.Log.AppendLine(" ---------- ");
        }

        public static void FixSwitchFlagVars(SceneEnemizerData thisSceneData, StringBuilder log)
        {
            /// New actors can have switch flags, these are normally tailored to the scene so new actors could step on vanilla actors


            List<int> claimedSwitchFlags = new List<int>();
            for (int mapIndex = 0; mapIndex < thisSceneData.Scene.Maps.Count; ++mapIndex)
            {
                for (int actorNumber = 0; actorNumber < thisSceneData.Scene.Maps[mapIndex].Actors.Count; ++actorNumber)
                {
                    var mapActor = thisSceneData.Scene.Maps[mapIndex].Actors[actorNumber];
                    var flags = ActorUtils.GetActorSwitchFlags(mapActor, (short)mapActor.OldVariant);
                    if (flags >= 0)
                    {
                        claimedSwitchFlags.Add(flags);
                    }

                }
            }
            for (int doorNumber = 0; doorNumber < thisSceneData.Scene.Doors.Count; ++doorNumber)
            {
                var sceneDoor = thisSceneData.Scene.Doors[doorNumber];
                var flags = ActorUtils.GetActorSwitchFlags(sceneDoor, (short)sceneDoor.OldVariant);
                if (flags >= 0)
                {
                    claimedSwitchFlags.Add(flags);
                    thisSceneData.Log.AppendLine($"  [{doorNumber}][{sceneDoor.ActorEnum}] has flags: [{flags}]");
                }
            }

            var usableSwitches = new List<int>();
            void CreateUsableSwitchesList()
            {
                usableSwitches.AddRange(Enumerable.Range(1, 0x7E)); // 0x7F is popular non-valid value
                usableSwitches.RemoveAll(sflag => claimedSwitchFlags.Contains(sflag));
                usableSwitches.Reverse(); // we want to start at 0x7F and decend, under the assumption that they always used lower values

            }

            CreateUsableSwitchesList();

            var actorsWithSwitchFlags = thisSceneData.Actors.ToList();

            // if there is both new chests and a new switching actor
            var switchingActors = new List<GameObjects.Actor> // too lazy to add a rarely used attribute for just this
            {
                GameObjects.Actor.ElegyStatueSwitch,
                GameObjects.Actor.SunSwitch,
                GameObjects.Actor.GoronLinkPoundSwitch,
                GameObjects.Actor.ObjSwitch
            };
            var newSwitchActors = thisSceneData.Actors.FindAll(a => switchingActors.Contains(a.ActorEnum));
            var newChestActors = thisSceneData.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.TreasureChest);
            var switchChestFlag = -1;
            if (newSwitchActors.Count > 0 && newChestActors.Count > 0) // pretest
            {
                for (int roomNum = 0; roomNum < thisSceneData.Scene.Maps.Count; roomNum++)
                {

                    var roomChests = newChestActors.FindAll(a => a.Room == roomNum);
                    var roomSwitches = newSwitchActors.FindAll(a => a.Room == roomNum);
                    if (roomChests.Count > 0 && roomSwitches.Count > 0)
                    {
                        var randomChest = roomChests.Random(thisSceneData.RNG);
                        randomChest.Variants[0] &= 0x0FFF; // changing type to switch to activate
                        randomChest.Variants[0] |= 0x3000;
                        // in case this actor's last slot only spawned at night or something stupid, set it to always spawn
                        randomChest.Rotation.x = ActorUtils.MergeRotationAndFlags(randomChest.Rotation.x, 0x7F);
                        randomChest.Rotation.z = ActorUtils.MergeRotationAndFlags(randomChest.Rotation.z, 0x7F);
                        //switchChestFlag = usableSwitches[0]; // grab a usable switch flag
                        switchChestFlag = usableSwitches.Find(u => u == 0x66); // grab a usable switch flag
                        ActorUtils.SetActorSwitchFlags(randomChest, (short)switchChestFlag);
                        usableSwitches.Remove(switchChestFlag);
                        log.AppendLine($" +++ WE FOUND SWITCH CHEST in room [{roomNum}], had switch flags modified to [{randomChest.Rotation.z.ToString("X4")}] +++");
                        actorsWithSwitchFlags.Remove(randomChest);
                        randomChest.ActorIdFlags |= 0x2000; // do not convert z rotation, we need it for chests
                        randomChest.Rotation.y |= 0x7F; // set cutscene value to -1 to allow the chest to appear without a working cutscene

                        foreach (var switchActor in roomSwitches)
                        {
                            ActorUtils.SetActorSwitchFlags(switchActor, (short)switchChestFlag);
                            log.AppendLine($" +++ [{switchActor.ActorId}][{switchActor.ActorEnum}] had switch flags matched to [{switchChestFlag}] +++");
                            actorsWithSwitchFlags.Remove(switchActor);
                        }
                    }
                }
            }

            { // else; assign to unused switch per area

                // change all new actors with switch flags to some flag not yet used

                for (int actorIndex = 0; actorIndex < actorsWithSwitchFlags.Count; actorIndex++)
                {
                    var actor = actorsWithSwitchFlags[actorIndex];
                    var switchFlags = ActorUtils.GetActorSwitchFlags(actor, (short)actor.Variants[0]);

                    if (switchFlags == -1) continue; // some actors can set th switch flag to -1 and ignore

                    if (usableSwitches.Count == 0) // we ran out, recreate list
                    {
                        CreateUsableSwitchesList();
                    }

                    if (usableSwitches.Contains(switchFlags)) // not used yet, claim
                    {
                        usableSwitches.Remove(switchFlags);
                    }
                    else // we have switch flag and we have a collision, we need to change it
                    {
                        //if (switchChestFlag != -1)
                        //{
                        //   ActorUtils.SetActorSwitchFlags(actor, (short)switchChestFlag);
                        //} else
                        var newSwitch = usableSwitches[0];
                        
                        ActorUtils.SetActorSwitchFlags(actor, (short)newSwitch);
                        usableSwitches.Remove(newSwitch);
                        log.AppendLine($" +++ [{actorIndex}][{actor.ActorEnum}] had switch flags modified to [{newSwitch}] +++");

                    }
                }
            }
        }

        public static void FixTreasureFlagVars(SceneEnemizerData thisSceneData, StringBuilder log)
        {
            /// Like switch flags, we want to avoid stepping on previously existing treasure flags

            //thisSceneData.Log.AppendLine($"------------------------------------------------- ");
            //thisSceneData.Log.AppendLine($"  Treasure Flags: ");

            var claimedTreasureFlags = new List<int>();
            for (int mapIndex = 0; mapIndex < thisSceneData.Scene.Maps.Count; ++mapIndex)
            {
                //thisSceneData.Log.AppendLine($" ======( MAP {mapIndex.ToString("X2")} )======");
                for (int actorIndex = 0; actorIndex < thisSceneData.Scene.Maps[mapIndex].Actors.Count; ++actorIndex)
                {
                    var mapActor = thisSceneData.Scene.Maps[mapIndex].Actors[actorIndex];
                    var flags = ActorUtils.GetActorTreasureFlags(mapActor, (short)mapActor.OldVariant);
                    if (flags >= 0)
                    {
                        claimedTreasureFlags.Add(flags);
                        //thisSceneData.Log.AppendLine($"  [{actorIndex}][{mapActor.ActorEnum}] has flags: [{flags}]");
                    }
                }
            }

            var usableTreasureFlags = new List<int>();
            usableTreasureFlags.AddRange(Enumerable.Range(0, 31));
            usableTreasureFlags.RemoveAll(tflag => claimedTreasureFlags.Contains(tflag));
            usableTreasureFlags.Reverse(); // we want to start at 31 and decend, under the assumption that they always used lower values
            // Because of limited treasure flags, if we run out, just reuse the ones only our new actors are using
            var copyOfUsable = usableTreasureFlags.ToList();

            for (int actorIndex = 0; actorIndex < thisSceneData.Actors.Count; actorIndex++)
            {
                var actor = thisSceneData.Actors[actorIndex];

                if (usableTreasureFlags.Count == 0)
                {
                    // We ran out of new flags, just start over with the ones only our new actors were using
                    usableTreasureFlags = copyOfUsable.ToList();
                }

                var treasureFlags = ActorUtils.GetActorTreasureFlags(actor, (short)actor.Variants[0]);
                if (treasureFlags == -1) continue;
                if (usableTreasureFlags.Contains(treasureFlags))
                {
                    usableTreasureFlags.Remove(treasureFlags);
                }
                else // we have switch flag and we have a collision, we need to change it
                {
                    var newSwitch = usableTreasureFlags[0];
                    ActorUtils.SetActorTreasureFlags(actor, (short)newSwitch);
                    usableTreasureFlags.Remove(newSwitch);
                    log.AppendLine($" +++ [{actorIndex}][{actor.ActorEnum}] had treasure flags modified to [{newSwitch}] +++");
                }
            }
        }


        public static void ShuffleObjects(SceneEnemizerData thisSceneData)
        {
            /// Select replacement objects for the scene
            // TODO: turns out objects are per-room, we could do this per room not per scene

            thisSceneData.ChosenReplacementObjects = new List<ValueSwap>();
            int newObjectSize = 0;
            var newActorList = new List<int>();
            var previousObjectActors = new List<Actor>(); // already previously chosen, remove from the rest of the lists

            for (int objectIndex = 0; objectIndex < thisSceneData.Objects.Count; objectIndex++)
            {
                #region Object Forcing Debug
                //////////////////////////////////////////////////////
                ///////// debugging: force an object (enemy) /////////
                //////////////////////////////////////////////////////
                #if DEBUG

                bool TestHardSetObject(GameObjects.Scene targetScene, GameObjects.Actor target, GameObjects.Actor replacement)
                {
                    if (thisSceneData.Scene.File == targetScene.FileID() && thisSceneData.Objects[objectIndex] == target.ObjectIndex())
                    {
                        thisSceneData.ChosenReplacementObjects.Add(new ValueSwap()
                        {
                            OldV = thisSceneData.Objects[objectIndex],
                            NewV = replacement.ObjectIndex(),
                            ChosenV = replacement.ObjectIndex()
                        });
                        var cullCheck = thisSceneData.AcceptableCandidates.Find(act => act.ActorEnum == replacement);
                        if (cullCheck == null) // was weight excluded, need to re-add to test
                        {
                            var newActor = new Actor(replacement);
                            thisSceneData.AcceptableCandidates.Add(newActor);
                            thisSceneData.CandidatesPerObject[objectIndex].Add(newActor);
                        }
                        return true;
                    }
                    return false;
                }
                
                //if (TestHardSetObject(GameObjects.Scene.TerminaField, GameObjects.Actor.Leever, GameObjects.Actor.CreamiaCariage)) continue;
                //if (TestHardSetObject(GameObjects.Scene.ClockTowerInterior, GameObjects.Actor.HappyMaskSalesman, GameObjects.Actor.CreamiaCariage)) continue;
                //if (TestHardSetObject(GameObjects.Scene.Grottos, GameObjects.Actor.LikeLike, GameObjects.Actor.ReDead)) continue; ///ZZZZ
                if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Clock, GameObjects.Actor.CuttableIvyWall)) continue;

                if (TestHardSetObject(GameObjects.Scene.TerminaField, GameObjects.Actor.Leever, GameObjects.Actor.DeathArmos)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StoneTowerTemple, GameObjects.Actor.Nejiron, GameObjects.Actor.Peahat)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Clock, GameObjects.Actor.Keese)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Anju, GameObjects.Actor.StockpotBell)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.PostMan, GameObjects.Actor.HoneyAndDarlingCredits)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.RosaSisters, GameObjects.Actor.)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Gorman, GameObjects.Actor.HookshotWallAndPillar)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthernSwamp, GameObjects.Actor.DekuBaba, GameObjects.Actor.SkullKidPainting)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StoneTower, GameObjects.Actor.ClayPot, GameObjects.Actor.UnusedStoneTowerPlatform)) continue;
                //if (TestHardSetObject(GameObjects.Scene.GreatBayCoast, GameObjects.Actor.SwimmingZora, GameObjects.Actor.LabFish)) continue;
                //if (TestHardSetObject(GameObjects.Scene.DekuPalace, GameObjects.Actor.Torch, GameObjects.Actor.BeanSeller)) continue;

                if (TestHardSetObject(GameObjects.Scene.ClockTowerInterior, GameObjects.Actor.HappyMaskSalesman, GameObjects.Actor.IronKnuckle)) continue;
                #endif
                #endregion

                var reducedCandidateList = thisSceneData.CandidatesPerObject[objectIndex].ToList();
                foreach (var objectSwap in thisSceneData.ChosenReplacementObjects)
                {
                    // remove previously used objects: remove copies to increase variety
                    //reducedCandidateList.RemoveAll(act => u.ObjectID == objectSwap.NewV);
                    // should be faster to keep track of actors not objects
                    reducedCandidateList.RemoveAll(actor => previousObjectActors.Contains(actor));
                }
                if (reducedCandidateList.Count == 0) // rarely, there are no available objects left
                {
                    newObjectSize += 2 ^ 30; // should always error in the object size overflow detection code
                    continue; // this enemy was starved by previous options, force error and try again
                }

                // get random enemy from the possible random enemy matches
                Actor randomEnemy = reducedCandidateList[thisSceneData.RNG.Next(reducedCandidateList.Count)];

                // keep track of sizes between this new enemy combo and what used to be in this scene
                // objects below 4 are always loaded, don't count to our object limit
                if (randomEnemy.ObjectId > 3) // object 1 is gameplay_keep, 2 is field_keep, 3 is dungeon keep
                {
                    newObjectSize += randomEnemy.ObjectSize;
                }
                if (!newActorList.Contains(randomEnemy.ActorId))
                {
                    newActorList.Add(randomEnemy.ActorId);
                }

                // add random enemy to list
                var newReplacementObject = (new ValueSwap()
                {
                    OldV = thisSceneData.Objects[objectIndex],
                    ChosenV = randomEnemy.ObjectId,
                    NewV = randomEnemy.ObjectId
                });
                thisSceneData.ChosenReplacementObjects.Add(newReplacementObject);
                previousObjectActors.AddRange(reducedCandidateList);
            } // end for for each object
        }

        public static void ShuffleActors(SceneEnemizerData thisSceneData, int objectIndex, List<Actor> subMatches, List<Actor> candidateAndCompanionGroup, List<Actor> knownChangedActorList)
        {
            #region Special exception if building debug and this build requires actor that doesnt exist
            #if DEBUG

            if (subMatches.Count == 0)
            {
                throw new Exception(" SubMatches contain no actors for this chosen object.\n" +
                                    " If you built the debug version, go back to VisualStudio and build \"Release\" instead\n " +
                                    " Otherwise you probably forgot the actor isn't possible here.");
            }
            #endif
            #endregion

            for (int actorIndex = 0; actorIndex < thisSceneData.ActorsPerObject[objectIndex].Count(); actorIndex++)
            {
                var oldActor = thisSceneData.ActorsPerObject[objectIndex][actorIndex];
                var actorsPerRoomCount = thisSceneData.ActorsPerObject[objectIndex].FindAll(act => act.Room == oldActor.Room).Count();

                // this isn't really a loop, 99% of the time it matches on the first loop
                // leaving this for now because its faster than shuffling the list even if it looks stupid
                // eventually: replace with .Single().Where(conditions)
                Actor testActor;
                while (true)
                {
                    /// looking for a list of objects for the actors we chose that fit the actor types
                    testActor = subMatches[thisSceneData.RNG.Next(subMatches.Count)];

                    if (testActor.IsCompanion && (oldActor.MustNotRespawn || actorsPerRoomCount <= 2))
                    {
                        // so far all companions are unkillable, so we cannot put them in these rooms
                        // also if the room has no space for companions, dont use them here
                        continue;
                    }

                    break;
                }

                var newVariant = testActor.Variants[thisSceneData.RNG.Next(testActor.Variants.Count)]; // readability
                oldActor.ChangeActor(testActor, vars: newVariant);

                knownChangedActorList.Add(oldActor);
                var testSearch = candidateAndCompanionGroup.Find(act => act.ActorId == oldActor.ActorId);
                if (testSearch == null)
                {
                    candidateAndCompanionGroup.Add(testActor);
                }
            } // end foreach
        } // end function


        public static void ShuffleStandaloneActors(SceneEnemizerData thisSceneData)
        {
            /// this is the same as above but for the actors that previously did not have an object,
            /// so they can use ANY object require actor, or free actors

            var StandaloneActors = thisSceneData.StandaloneActors; // slots

            if (StandaloneActors == null) throw new Exception("StandaloneActors busted");

            // we need to split this per-room so we can can get a slimmer candidate list? maybe that can wait until its working optimize later

            // we need to generate a candidate list for all actors without an object just like regular? if we want blocking sensitivity yeah
            // sort the list of special actors into list of per type
            var allStandaloneActorsPerEnum = new List<List<Actor>>(); // same index for both, this is a list of all actors per type
            var allCandidatesPerStandalone = new List<List<Actor>>(); // all candidates for the type replacement
            var uniqueStandaloneActorTypes = thisSceneData.StandaloneActors.Select(act => act.OldActorEnum).Distinct().ToList();
            if (uniqueStandaloneActorTypes == null) throw new Exception("unique free actors busted");
            for ( int a = 0; a < uniqueStandaloneActorTypes.Count; a++)
            {
                var actorType = uniqueStandaloneActorTypes[a];
                var allActorInstances = thisSceneData.StandaloneActors.FindAll(act => act.OldActorEnum == actorType);
                allStandaloneActorsPerEnum.Add(allActorInstances);

                // this is ripped and modified from GenerateActorCandidates
                //var objectHasFairyDroppingEnemy = fairyDroppingActors.Any(act => act.ObjectIndex() == thisSceneData.Objects[objectIndex]);
                var objectHasBlockingSensitivity = allActorInstances.Any(actor => actor.Blockable == false);
                // get a list of matching actors that can fit in the place of the previous actor
                // assumed that we will never have a fairy dropping object-less actor, those were only enemies
                // issue: this doesnt account for which room we are in, this pool is roomless in consideration
                var newCandiateList = GetMatchPool(thisSceneData, allActorInstances, containsFairyDroppingEnemy:false, objectHasBlockingSensitivity);


                //allCandidatesPerStandalone.Add(newCandiateList);

                for (int actorIndex = 0; actorIndex < allActorInstances.Count(); actorIndex++)
                {
                    var oldActor = allActorInstances[actorIndex];
                    var oldActorRoomObjects = thisSceneData.AllObjects[oldActor.Room];
                    // since we know there is another check later, lets remove room limits from this consideration entirely
                    //var actorsPerRoomCount = allActorInstances.FindAll(act => act.Room == oldActor.Room).Count();

                    // get the objects for this room
                    // quickly grab the candidates for the available objects

                    // ZZZ
                    // TODO this is terribly broken, does not consider type or blocking restrictions
                    var candidatesPerActor = new List<Actor>();
                    for (int o = 0; o < oldActorRoomObjects.Count; o++)
                    {
                        var obj = oldActorRoomObjects[o];
                        var oldList = thisSceneData.AcceptableCandidates; // debug
                        //var actorsForThisObject = thisSceneData.AcceptableCandidates.FindAll(act => act.ObjectId == obj); // waay too permissive
                        var actorsForThisObject = newCandiateList.FindAll(act => act.ObjectId == obj);
                        // todo check if the replacment is banned on a per actor bassis

                        // assume not possible for free actors for now // TODO once we start splitting actor lists this is dangerous
                        //var objectHasFairyDroppingEnemy = fairyDroppingActors.Any(act => act.ObjectIndex() == thisSceneData.Objects[objectIndex]);
                        //var objectHasBlockingSensitivity = currentTargetActors.Any(actor => actor.Blockable == false);
                        // get a list of matching actors that can fit in the place of the previous actor
                        //var specificCandidateList = GetMatchPool(thisSceneData, thisSceneData.ActorsPerObject[objectIndex], containsFairyDroppingEnemy:false, objectHasBlockingSensitivity);
                        // except we already did this above? should we limit to one?

                        candidatesPerActor.AddRange(actorsForThisObject.ToList());
                    }
                    candidatesPerActor.AddRange(thisSceneData.SceneFreeActors.ToList());

                    // now we need to go through candidates and reduce to variants we can use
                    var trimmedCandidates = new List<Actor>();
                    for (int aa = 0; aa < candidatesPerActor.Count; aa++)
                    {
                        var compatibilityTestActor = candidatesPerActor[aa];
                        var compatibleVariants = oldActor.CompatibleVariants(compatibilityTestActor, thisSceneData.RNG); // do we want clear enemy room data?
                        if (compatibleVariants != null && compatibleVariants.Count > 0)
                        {
                            compatibilityTestActor.Variants = compatibleVariants;
                            trimmedCandidates.Add(compatibilityTestActor);
                        }
                    }

                    if (trimmedCandidates.Count == 0)
                        continue;

                    Debug.Assert(trimmedCandidates.Count > 1); // == 1, means our testing is super limiting (usually broken)

                    // this isn't really a loop, 99% of the time it matches on the first loop
                    // leaving this for now because its faster than shuffling the list even if it looks stupid
                    // eventually: replace with .Single().Where(conditions)
                    Actor testActor;
                    while (true)
                    {
                        /// looking for a list of objects for the actors we chose that fit the actor types
                        var randomIndex = thisSceneData.RNG.Next(trimmedCandidates.Count);
                        testActor = trimmedCandidates[randomIndex];

                        /* if (testActor.IsCompanion && (oldActor.MustNotRespawn || actorsPerRoomCount <= 2))
                        {
                            // so far all companions are unkillable, so we cannot put them in these rooms
                            // also if the room has no space for companions, dont use them here
                            continue;
                        } */

                        break;
                    }

                    if (testActor.Variants == null || testActor.Variants.Count == 0) throw new Exception($"variants busted:{testActor.Name}");

                    var newVariant = testActor.Variants[thisSceneData.RNG.Next(testActor.Variants.Count)];
                    oldActor.ChangeActor(testActor, vars: newVariant);

                } // end foreach instance
            } // end foreach unique actor type
        } // end function


        public static void GenerateActorCandidates(SceneEnemizerData thisSceneData, List<GameObjects.Actor> fairyDroppingActors)
        {
            /// Generate a matching set of possible replacement objects and enemies that we can use

            thisSceneData.ActorsPerObject = new List<List<Actor>>();
            for (int objectIndex = 0; objectIndex < thisSceneData.Objects.Count; objectIndex++)
            {
                // get a list of all enemies (in this room) that have the same OBJECT as our object that have an actor we also have
                var objId = thisSceneData.Objects[objectIndex];
                var currentTargetActors = thisSceneData.Actors.FindAll(act => act.OldObjectId == objId);
                Debug.Assert(currentTargetActors.Count > 0);
                thisSceneData.ActorsPerObject.Add(currentTargetActors);
                // we want to detect if this scene/actor combo can drop fairies early
                var objectHasFairyDroppingEnemy = fairyDroppingActors.Any(act => act.ObjectIndex() == thisSceneData.Objects[objectIndex]);
                var objectHasBlockingSensitivity = currentTargetActors.Any(actor => actor.Blockable == false);
                // get a list of matching actors that can fit in the place of the previous actor
                var newCandiateList = GetMatchPool(thisSceneData, thisSceneData.ActorsPerObject[objectIndex], objectHasFairyDroppingEnemy, objectHasBlockingSensitivity);

                // HOTFIX: TODO replace with something proper later
                // this is currently the only instance of ground+pathing getting replacement by only pathing, so handle it unique case
                if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.ZoraHall && objectIndex == 0) // object zora
                {
                    // for all candidates, check if they have only pathing and remove
                    foreach (var candidate in newCandiateList.ToArray())
                    {
                        var pathingVariants = candidate.AllVariants[(int)GameObjects.ActorType.Pathing - 1];
                        if (pathingVariants != null && pathingVariants.Count > 0)
                        {
                            var groundVariants = candidate.AllVariants[(int)GameObjects.ActorType.Ground - 1];

                            if (groundVariants == null || groundVariants.Count == 0)
                            {
                                newCandiateList.Remove(candidate);
                            }
                        }
                    }
                }

                thisSceneData.CandidatesPerObject.Add(newCandiateList);
            }
        }

        public static List<Actor> GetMatchPool(SceneEnemizerData thisSceneData, List<Actor> oldActors, bool containsFairyDroppingEnemy, bool hasBlockingSensitivity)
        {
            var reducedCandidateList = Actor.CopyActorList(thisSceneData.AcceptableCandidates);
            var enemyMatchesPool = new List<Actor>();

            // we cannot currently swap out specific enemies, so if ONE must be killable, all shared enemies must
            //  eg: one of the dragonflies in woodfall must be killable in the map room, so all in the dungeon must since we cannot isolate
            bool MustBeKillable = oldActors.Any(act => act.MustNotRespawn);

            if (containsFairyDroppingEnemy)
            {
                MustBeKillable = true; // we dont want respawning or unkillable enemies here
            }

            // this could be per-enemy, but right now its only used where enemies and objects match,
            // so to save cpu cycles do it once per object not per enemy
            // TODO: this only removes one actor, if one object can have multiple actors we should check all ofthem
            var oldActorEnum = oldActors[0].OldActorEnum;
            var blockedReplacementActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(oldActorEnum);
            for (var e = 0; e < blockedReplacementActors.Count; e++)
            {
                var blockedActor = blockedReplacementActors[e];
                ReplacementListRemove(reducedCandidateList, blockedActor);
            }

            // TODO does this NEED to be a double loop? does anything change per enemy copy that we should worry about?
            for (var oldActorIndex = 0; oldActorIndex < oldActors.Count; oldActorIndex++) // this is all copies of an enemy in a scene, so all bo or all guay
            {
                var oldActor = oldActors[oldActorIndex];

                // the enemy we got from the scene has the specific variant number, the general game object has all
                foreach (var candidateEnemy in reducedCandidateList)
                {
                    // if current test actor not already in the new pool
                    //   TODO why would we get duplicates this late? shouldnt the candidates be unique list?
                    if (enemyMatchesPool.Any(act => act.ActorId == candidateEnemy.ActorId)) continue;

                    var compatibleVariants = oldActor.CompatibleVariants(candidateEnemy, thisSceneData.RNG);

                    if (compatibleVariants == null || compatibleVariants.Count == 0) continue;

                    var newEnemy = candidateEnemy.CopyActor();

                    // reduce varieties to meet killable requirements
                    if (MustBeKillable)
                    {
                        newEnemy.Variants = candidateEnemy.KillableVariants(compatibleVariants); // reduce to available
                        if (newEnemy.Variants.Count == 0)
                        {
                            continue; // can't put this enemy here: it has no non-respawning variants
                        }

                        // if the actor is in a kill all enemy room, reduce the chances of boring enemies from showing up here
                        if ((oldActor.MustNotRespawn
                            && !(thisSceneData.Scene.SceneEnum == GameObjects.Scene.WoodfallTemple && oldActor.Room == 9) // dark room exception
                            && !containsFairyDroppingEnemy) && seedrng.Next(100) < 25)
                        {
                            newEnemy.RemoveEasyEmemies();
                            if (newEnemy.Variants.Count == 0) // TODO refactor this into the overall flow
                            {
                                continue;
                            }
                        }

                    }
                    else if (oldActor.Blockable == false)
                    {
                        if (newEnemy.ActorEnum.GetAttribute<BlockingVariantsAll>() != null)
                        {
                            continue;
                        }
                        else
                        {
                            newEnemy.Variants = compatibleVariants;
                            newEnemy.RemoveBlockingTypes();
                            if (newEnemy.Variants.Count == 0) // TODO refactor this into the overall flow
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        newEnemy.Variants = compatibleVariants;
                    }

                    // ACCEPTABLE
                    enemyMatchesPool.Add(newEnemy);
                } // for each candidate end
            } // for each slot end

            return enemyMatchesPool;
        }

        #region Trim and Free actors

        public static void TrimAllActors(SceneEnemizerData thisSceneData, List<Actor> candidateAndCompanionGroup, List<Actor> knownChangedActorList, bool allowLimits = true)
        {
            /// Actors can have maximum per-room variants, if these show up we should cull the extra over the max
            /// e.g some Dynapoly actors cannot be placed too many times because they overload the dynapoly system
            /// candidateAndCompanionGroup is the list of the object compatible actors, and candidates added to the actor
            //    should this include candidates? is that what we wanted?
            /// knownChangedActorList is all actors that were changed for this object that should have been changed to the candidates
            //    this seems pointless, we now have to contend with the possibility of all actors having every object, this has been depreicated
            var restrictedActors = candidateAndCompanionGroup.FindAll(act => act.HasVariantsWithRoomLimits() || act.OnlyOnePerRoom != null);
            for (int actorIndex = 0; actorIndex < restrictedActors.Count; ++actorIndex)
            {
                var problemActor = restrictedActors[actorIndex];

                // we need to split enemies per room
                for (int roomIndex = 0; roomIndex < thisSceneData.Scene.Maps.Count; ++roomIndex)
                {
                    //var roomActors = knownChangedActorList.FindAll(act => act.Room == roomIndex && act.ActorId == problemActor.ActorId); // old: now that we can swap outside of given object, this might no longer work right
                    var roomActors = knownChangedActorList.FindAll(act => act.Room == roomIndex && act.ActorId == problemActor.ActorId);
                    //var roomActors = thisSceneData.Actors.FindAll(act => act.Room == roomIndex && act.ActorId == problemActor.ActorId);
                    if (roomActors.Count == 0) continue; // nothing to trim: no actors in this room
                    var roomIsClearPuzzleRoom = thisSceneData.Scene.SceneEnum.IsClearEnemyPuzzleRoom(roomIndex);
                    var roomFreeActors = GetRoomFreeActors(thisSceneData, roomIndex);
                    if (!allowLimits)
                    {
                        roomFreeActors.RemoveAll(u => u.OnlyOnePerRoom != null || u.HasVariantsWithRoomLimits());
                    }

                    if (problemActor.OnlyOnePerRoom != null)
                    {
                        // all actors merged together into one list in the function
                        TrimSpecificActor(thisSceneData, problemActor, roomActors, roomFreeActors, roomIsClearPuzzleRoom);
                    }
                    else
                    {
                        var limitedVariants = problemActor.Variants.FindAll(act => problemActor.VariantMaxCountPerRoom(act) >= 0);
                        foreach (var variant in limitedVariants)
                        {
                            // per actor/variant combo
                            TrimSpecificActor(thisSceneData, problemActor, roomActors, roomFreeActors, roomIsClearPuzzleRoom, variant: variant);
                        }
                    }
                }
            } // end for trim restricted actors
        }

        public static void TrimSpecificActor(SceneEnemizerData thisSceneData, Actor actorType, List<Actor> roomActors, List<Actor> roomFreeActors,
                                           bool roomIsClearPuzzleRoom, int variant = -1)
        {
            /// actors with maximum counts have their extras trimmed off, replaced with empty, or free/extra actors, depending on randomRate


            List<Actor> trimCandidates;
            if (actorType.OnlyOnePerRoom != null)
            {
                trimCandidates = roomActors.ToList(); // all of variants of this actor are valid for trimming as one pool
            }
            else
            {
                trimCandidates = roomActors.FindAll(act => act.ActorEnum == actorType.ActorEnum &&
                                                            act.Variants[0] == variant);
            }

            if (trimCandidates != null && trimCandidates.Count > 1)
            {
                int variantMax = actorType.VariantMaxCountPerRoom(variant);
                int removedCount = 0;
                if (roomIsClearPuzzleRoom) // clear enemy room, only one enemy has to be killable
                {
                    // weirdly there isn't a single room in the game that has both a clear enemy to get item puzzle
                    // and a fairy dropping enemy, so we can separate easily
                    var randomEnemy = trimCandidates[thisSceneData.RNG.Next(trimCandidates.Count)];
                    trimCandidates.Remove(randomEnemy); // leave at least one enemy alone
                    removedCount++;
                }
                else // not clear puzzle room: protected enemies are fairy holding actors
                {
                    foreach (var protectedEnemy in trimCandidates.Where(act => act.MustNotRespawn == true).ToList())
                    {
                        // do not trim "mustnotrepawn" placements
                        trimCandidates.Remove(protectedEnemy); // we cannot remove any, fairies are sacred
                        removedCount++;
                    }
                }

                Debug.Assert(roomActors.Count > 0);

                // for now until I can be sure the code after this is working, always reserve one
                if (removedCount == 0)
                {
                    var randomChoice = thisSceneData.RNG.Next(trimCandidates.Count);
                    trimCandidates.RemoveAt(randomChoice);
                    removedCount += 1;
                }

                Debug.Assert(roomActors.Count > 0);

                // we have a max to want to limit to, here we pick how many up to that max can be saved from trim
                // we don't always want the max variant count, sometimes we want less, this is somewhat random
                var randomizedVariation = thisSceneData.RNG.Next(0, variantMax);
                //for (int i = removedCount; (i + extraCullChosen < variantMax) && (i < trimCandidates.Count); ++i)
                for (int i = removedCount; (i < randomizedVariation) && (i < trimCandidates.Count); ++i)
                {
                    // spare these actors from trim
                    trimCandidates.Remove(trimCandidates[thisSceneData.RNG.Next(trimCandidates.Count)]);
                }

                Debug.Assert(roomActors.Count > 0);

                // if the actor being trimmed is a free actor, remove from possible replacements
                // TODO this should really already happen before we get this far? can we assume we will never cross dip?
                var freeActorSearch = roomFreeActors.Find(act => act.ActorId == actorType.ActorId);
                if (freeActorSearch != null)
                {
                    roomFreeActors.Remove(freeActorSearch);
                }

                Debug.Assert(roomActors.Count > 0);

                // kill the rest since max is reached
                // we want to limit replacements here above the per-actor function to save re-doing it
                var blockedActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(roomActors[0].OldActorEnum);
                List<Actor> acceptableReplacementFreeActors = roomFreeActors.FindAll(a => !blockedActors.Contains(a.ActorEnum)).ToList();
                foreach (var enemy in trimCandidates) // for all specific actor in actorType
                {
                    var enemyIndex = roomActors.IndexOf(enemy);
                    EmptyOrFreeActor(thisSceneData, enemy, roomActors, acceptableReplacementFreeActors, roomIsClearPuzzleRoom);
                }
            } // end If Room has Actors with Variants we want to trim
        } // end TrimSpecificActor

        public static List<List<int>> TrimObjectList(SceneEnemizerData thisSceneData, StringBuilder log)
        {
            /// this function generates our enemizer chosenReplacementObjectsPerMap from our chosenReplacementObjects
            ///   also trims duplicate objects, replacing them with SMALLEST_OBJ

            var replacedObjects = new List<int>();
            var objectsPerMap = new List<List<int>>();
            var actors = thisSceneData.Actors;
            var scene = thisSceneData.Scene;

            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];
                var objList = map.Objects.ToList(); // copy the old list, since we're modifying

                // first pass: generate a list of all objects per map, and replace objects as we go from the swaps
                for (int swapIndex = 0; swapIndex < thisSceneData.ChosenReplacementObjects.Count; swapIndex++)
                {
                    var swap = thisSceneData.ChosenReplacementObjects[swapIndex];
                    var searchIndex = map.Objects.FindIndex(obj => obj == swap.OldV); // search original list so we dont catch the previous changes
                    if (searchIndex == -1) continue; // not all rooms will have the object, can ignore

                    objList[searchIndex] = swap.NewV;
                }

                // find all objects that have no duplicates
                var uniqueObjects = objList.Distinct().ToList();

                // if they are the same size, no duplicates, keep going to next map
                if (objList.Count != uniqueObjects.Count)
                {
                    // second pass: remove all duplicates
                    for (int u = 0; u < uniqueObjects.Count; u++)
                    {
                        var uniqueObj = uniqueObjects[u];
                        if (objList.Count(obj => obj == uniqueObj) > 1) // more than one exists, remove
                        {
                            // just remove first one, not sure if there is an advantage of changing one over the other
                            // consideration: if the object list order changes, the scene load hickups, but so long as wel always replace first...
                            // we dont want the first we want to remove the last, as removing the first introduces more object list re-loads
                            //var firstIndex = objList.FindIndex(obj => obj == uniqueObj);
                            //objList[firstIndex] = SMALLEST_OBJ;
                            var lastIndex = objList.FindLastIndex(obj => obj == uniqueObj);
                            objList[lastIndex] = SMALLEST_OBJ;
                        }
                    }
                }

                objectsPerMap.Add(objList);
            }

            if (replacedObjects.Count > 0)
            {
                var objectAsHexString = replacedObjects.Select(obj => obj.ToString("X3"));
                log.AppendLine($"Duplicate Objects: [{String.Join(", ", objectAsHexString)}]");
            }

            thisSceneData.ChosenReplacementObjectsPerMap = objectsPerMap;
            return objectsPerMap;
        }

        public static void GetSceneFreeActors(SceneEnemizerData thisSceneData)
        {
            /// some actors don't require unique objects, they can use objects that are generally loaded, we can use these almost anywhere
            ///  any actor that is object type 1 (gameplay_keep) is free to use anywhere
            ///  scenes can have a special object loaded by themselves, this is either dangeon_keep or field_keep, or none

            var scene = thisSceneData.Scene;
            var sceneIsDungeon = scene.HasDungeonObject();
            var sceneIsField = scene.HasFieldObject();
            var sceneFreeActors = FreeCandidateList.Where(act => (act.ObjectId == 1
                                                                || (sceneIsField && act.ObjectId == (int)Scene.SceneSpecialObject.FieldKeep)
                                                                || (sceneIsDungeon && act.ObjectId == (int)Scene.SceneSpecialObject.DungeonKeep))
                                                           && !(act.BlockedScenes != null && act.BlockedScenes.Contains(scene.SceneEnum))
                                                          ).ToList();

            // special cases: these actors have dual objects where one object is a special object
            // we have to add special versions for replacmeent to match the special object variants
            if (VanillaEnemyList.Contains(GameObjects.Actor.ClayPot) && sceneIsDungeon)
            {
                var newDungeonOnlyPot = new Actor(GameObjects.Actor.ClayPot);
                // todo trim variants
                newDungeonOnlyPot.Variants = clayPotDungeonVariants.ToList();
                newDungeonOnlyPot.AllVariants[(int)GameObjects.ActorType.Ground] = newDungeonOnlyPot.Variants;

                sceneFreeActors.Add(newDungeonOnlyPot);
            }
            // todo do this for tall grass too
            if (VanillaEnemyList.Contains(GameObjects.Actor.TallGrass) && sceneIsField)
            {
                var newFieldTallGrass = new Actor(GameObjects.Actor.TallGrass);
                newFieldTallGrass.Variants = tallGrassFieldObjectVariants.ToList();
                newFieldTallGrass.AllVariants[(int)GameObjects.ActorType.Ground] = newFieldTallGrass.Variants;
                // todo trim variants
                sceneFreeActors.Add(newFieldTallGrass);
            }
            // giant ice block is now a huge problem in regular grottos, remove them here instead of removing all blocking actors
            if (scene.SceneEnum == GameObjects.Scene.Grottos)
            {
                var iceblock = sceneFreeActors.Find(act => act.ActorEnum == GameObjects.Actor.RegularIceBlock);
                var blockingVariantsAttr = GameObjects.Actor.RegularIceBlock.GetAttribute<BlockingVariantsAttribute>();

                if (iceblock != null) 
                {
                    var newVariants = iceblock.Variants.ToList();
                    newVariants.RemoveAll(var => blockingVariantsAttr.Variants.Contains(var));
                    iceblock.Variants = newVariants;
                    iceblock.AllVariants[(int)GameObjects.ActorType.Ground - 1] = newVariants;

                }
            }

            thisSceneData.SceneFreeActors = sceneFreeActors;
            return;
        }

        public static List<Actor> GetRoomFreeActors(SceneEnemizerData thisScene, int thisRoomIndex)
        {
            var sceneFreeActors = thisScene.SceneFreeActors;
            var objectsInThisRoom = thisScene.ChosenReplacementObjectsPerMap[thisRoomIndex];

            // todo: can we conider if the actors are already saurated?
            var roomFreeActors = ReplacementCandidateList.Where(act => act.ObjectId >= 3
                                       && objectsInThisRoom.Contains(act.ObjectId)
                                       && !(act.BlockedScenes != null && act.BlockedScenes.Contains(thisScene.Scene.SceneEnum))
                                     ).ToList();

            var freeOnlyActors = FreeOnlyCandidateList.Where(act => objectsInThisRoom.Contains(act.ObjectId)
                                       && !(act.BlockedScenes != null && act.BlockedScenes.Contains(thisScene.Scene.SceneEnum))
                                     ).ToList();

            return sceneFreeActors.Union(roomFreeActors).Union(freeOnlyActors).ToList();
        }

        public static void EmptyOrFreeActor(SceneEnemizerData thisSceneData, Actor oldActor, List<Actor> currentRoomActorList,
                                            List<Actor> acceptableFreeActors, bool roomIsClearPuzzleRoom = false)
        {
            /// returns an actor that is either an empty actor or a free actor
            /// assuming one can be placed here beacuse it doesn't require a new unique object, or an object already exists

            // roll dice: either get a free actor, or empty
            if (thisSceneData.RNG.Next(100) < thisSceneData.FreeActorRate)
            {
                // pick random replacement by selecting random start of array and traversing sequentially until we find a match
                int randomStart = thisSceneData.RNG.Next(acceptableFreeActors.Count);
                for (int matchAttempt = 0; matchAttempt < acceptableFreeActors.Count; ++matchAttempt)
                {
                    /// check the old enemy for available co-actors,
                    /// remove if those already exist in the list at max size

                    int listIndex = (randomStart + matchAttempt) % acceptableFreeActors.Count;
                    var testEnemy = acceptableFreeActors[listIndex];

                    var testEnemyCompatibleVariants = oldActor.CompatibleVariants(testEnemy, thisSceneData.RNG, roomIsClearPuzzleRoom);
                    if (testEnemyCompatibleVariants == null || testEnemyCompatibleVariants.Count == 0) continue;  // no type compatibility, skip

                    // this should be working in compatibleVariants now
                    /*
                    if (oldActor.Blockable == false)
                    {
                        if (testEnemy.ActorEnum.GetAttribute<BlockingVariantsAll>() != null)
                        {
                            continue; // test actor is always blocking, oldactor cannot be blocked, continue to next actor
                        }
                        else
                        {
                            testEnemyCompatibleVariants = testEnemy.RemoveBlockingTypes();
                        }
                    } 

                    var respawningVariants = testEnemy.RespawningVariants;
                    if ((oldActor.MustNotRespawn || roomIsClearPuzzleRoom) && respawningVariants != null)
                    {
                        testEnemyCompatibleVariants.RemoveAll(variant => respawningVariants.Contains(variant));
                    }

                    if (testEnemyCompatibleVariants.Count == 0) continue;  // no variants remain, leave

                    // */

                    var enemyHasMaximums = testEnemy.HasVariantsWithRoomLimits();
                    var acceptableVariants = new List<int>();

                    if (enemyHasMaximums)
                    {
                        var enemiesInRoom = currentRoomActorList.FindAll(act => act.ActorId == testEnemy.ActorId);
                        if (enemiesInRoom.Count > 0)  // only test for specific variants if there are already some in the room
                        {
                            // find variant that is not maxed out
                            foreach (var variant in testEnemyCompatibleVariants)
                            {
                                // if the varient limit has not been reached
                                var variantMax = testEnemy.VariantMaxCountPerRoom(variant);
                                var variantCount = enemiesInRoom.Count(act => act.OldVariant == variant);
                                if (variantCount < variantMax)
                                {
                                    acceptableVariants.Add(variant);
                                }
                            }
                        }
                        else
                        {
                            acceptableVariants = testEnemyCompatibleVariants;
                        }
                    }
                    else
                    {
                        acceptableVariants = testEnemyCompatibleVariants;
                    }

                    if (acceptableVariants.Count > 0)
                    {
                        int randomVariant = acceptableVariants[thisSceneData.RNG.Next(acceptableVariants.Count)];
                        if (testEnemy.ActorEnum == GameObjects.Actor.GrottoHole)
                        {
                            SetupGrottoActor(oldActor, randomVariant);
                        }
                        else
                        {
                            oldActor.ChangeActor(testEnemy, vars: randomVariant);
                        }
                        return;
                    }
                }
            } // end We roll for Free Actor
            //else (and fallthrough): empty actor 

            oldActor.ChangeActor(GameObjects.Actor.Empty);
        }

        public static void AddCompanionsToCandidates(SceneEnemizerData thisSceneData, int objectIndex, List<Actor> candidates)
        {
            // for actors that have companions, add them now
            foreach (var actor in candidates.ToList())
            {
                var companionAttrs = actor.ActorEnum.GetAttributes<CompanionActorAttribute>();
                if (companionAttrs != null)
                {
                    var targetActors = thisSceneData.ActorsPerObject[objectIndex];

                    // if 4 or fewer total actors here, no companions, not enough regular actors anyway
                    // reminder: these are companions that fully mix into the actor list
                    if (targetActors.Count <= 3) continue;

                    // for now, we ignore the second element and focus only on the blocking for all objects
                    // we would need to change to per-actor candidates list to get around this
                    var objectHasBlockingSensitivity = targetActors.Any(actor => actor.Blockable == false);

                    foreach (var companion in companionAttrs)
                    {
                        // check if companion meets object requirements to exist here
                        var cObj = companion.Companion.ObjectIndex();
                        if (cObj != 1 // gameplay keep is everywhere
                            && cObj != actor.ObjectId // we share the same object we can assure it exists
                            && !thisSceneData.Objects.Contains(cObj)) // the scene's replacement objects will have our required object
                            continue;

                        var companionType = companion.Companion;
                        // if its banned on this actor slot, also avoid
                        var blockedReplacementActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(actor.OldActorEnum);
                        if (blockedReplacementActors.Contains(companionType)) // blocked from being used as replacement
                        {
                            continue; // cannot use
                        }

                        /*if (objectHasBlockingSensitivity && companionType.IsBlockingActor()) // actor is blocking type, physically
                        {
                            continue; // cannot use
                        } // */

                        var newCompanion = new Actor(companionType);
                        newCompanion.Variants = companion.Variants;
                        if (objectHasBlockingSensitivity)
                        {
                            var blockingVariants = companionType.GetBlockingVariants();
                            // probably some c# lamba way to do this in one line
                            foreach (var variant in blockingVariants)
                            {
                                if (newCompanion.Variants.Contains(variant))
                                    newCompanion.Variants.Remove(variant);
                            }
                        }

                        if (newCompanion.Variants.Count == 0) continue;

                        newCompanion.IsCompanion = true;
                        candidates.Add(newCompanion);
                    }
                }

                // New TuboTrap is dual object, but its like one of two actors (tsubo) so adding new general code is rough
                // assume the actor still using object 3 to free placement in dungeons, add to claypot
                if (actor.ActorEnum == GameObjects.Actor.ClayPot)
                {
                    var newCompanion = new Actor(GameObjects.Actor.FlyingPot);
                    newCompanion.IsCompanion = true;
                    candidates.Add(newCompanion);
                }
            }
        }

        // thisSceneData.Actors, thisSceneData.RNG, thisSceneData.Log
        //public static void MoveAlignedCompanionActors(List<Actor> changedEnemies, Random rng, StringBuilder log)
        public static void MoveAlignedCompanionActors(SceneEnemizerData thisSceneData)
        {
            /// Companion actors can sometimes be alligned to their host, to increase immersion
            /// e.g: putting hidden grottos inside of a stone circle
            /// e.g 2: putting butterflies over bushes

            var actorsWithCompanions = thisSceneData.Actors.FindAll(act => ((GameObjects.Actor)act.ActorId).HasOptionalCompanions())
                                                     .OrderBy(act => thisSceneData.RNG.Next()) // randomize list
                                                     .ToList();

            if (actorsWithCompanions.Count <= 2) return;

            for (int i = 0; i < actorsWithCompanions.Count; ++i)
            {
                var mainActor = actorsWithCompanions[i];
                var mainActorEnum = (GameObjects.Actor)mainActor.ActorId;
                var companions = mainActorEnum.GetAttributes<AlignedCompanionActorAttribute>().ToList();
                foreach (var companion in companions)
                {
                    var companionEnum = companion.Companion;
                    // todo detection of ourVars too
                    // scan for companions that can be moved
                    // for now, assume all previously used companions must be left untouched, no shuffling
                    var eligibleCompanions = thisSceneData.Actors.FindAll(act =>
                                                               act.ActorId == (int)companionEnum               // correct actor
                                                            && mainActor.Room == act.Room                       // both in the same room
                                                            && act.previouslyMovedCompanion == false            // not already used
                                                            && companion.Variants.Contains(act.Variants[0]));   // correct variant

                    if (mainActor.Blockable == false)
                    {
                        eligibleCompanions.RemoveAll(comp => comp.ActorEnum.IsBlockingActor(variant: comp.Variants[0])); // blocking actor sensitive spots
                    }

                    if (eligibleCompanions != null && eligibleCompanions.Count > 0)
                    {
                        var randomCompanion = eligibleCompanions[thisSceneData.RNG.Next(eligibleCompanions.Count)];
                        // first move on top, then adjust
                        randomCompanion.Position.x = mainActor.Position.x;
                        randomCompanion.Position.y = (short)(actorsWithCompanions[i].Position.y + companion.RelativePosition.y);
                        randomCompanion.Position.z = mainActor.Position.z;

                        // todo: use x and z, with actor rotation, to figure out where to move the actors to
                        thisSceneData.Log.AppendLine(
                            "Moved companion: [" + randomCompanion.Variants[0].ToString("X4")
                            + "][" + randomCompanion.ActorEnum.ToString()
                            + "] to actor: [" + mainActor.ActorEnum.ToString()
                            + "][" + randomCompanion.Variants[0].ToString("X4")
                            + "] at cords: [" + randomCompanion.Position.x + ","
                                            + randomCompanion.Position.y + ","
                                            + randomCompanion.Position.z + "]");
                        randomCompanion.previouslyMovedCompanion = true;
                    }
                }
            }
        }

#endregion

        private static void HandleUniqueSceneSpecialObjectBehaviors(SceneEnemizerData thisSceneData)
        {
            SplitSceneLikeLikesIntoTwoActorObjects(thisSceneData);
            AddAniObjectIfTerminaFieldTree(thisSceneData);
            AddExtraObjectToPiratesInterior(thisSceneData);
        }

        private static void TrimSceneAcceptableCandidateList(SceneEnemizerData thisSceneData)
        {
            // some scenes are blocked from having enemy placements, do this ONCE before GetMatchPool, which would do it per-enemy
            thisSceneData.AcceptableCandidates = ReplacementCandidateList.FindAll(act => !act.ActorEnum.BlockedScenes().Contains(thisSceneData.Scene.SceneEnum))
                                                                         .FindAll(act => !act.NoPlacableVariants());

            //thisSceneData.AcceptableCandidates.RemoveAll(act => act.NoPlacableVariants());

            // if the dyna limits for this scene are low, we might as well trim all actors that cannot ever be put here,
            // no point running code on them later
            var dynaLimitsAttributes = thisSceneData.Scene.SceneEnum.GetAttribute<DynaAttributes>();
            if (dynaLimitsAttributes != null)
            {
                var largeDynaActors = thisSceneData.AcceptableCandidates.FindAll(act => act.DynaLoad.poly > dynaLimitsAttributes.Polygons
                                                                                     || act.DynaLoad.vert > dynaLimitsAttributes.Verticies);
                thisSceneData.AcceptableCandidates = thisSceneData.AcceptableCandidates.Except(largeDynaActors).ToList();
            }

            thisSceneData.Log.AppendLine($" ---------------------------");

            // trim weights
            foreach (var actor in thisSceneData.AcceptableCandidates.ToList())
            {
                int actorPlacementWeight = actor.GetPlacementWeight();
                if (actorPlacementWeight != 100
                     && thisSceneData.RNG.Next(100) > actorPlacementWeight ) // under is pass, over is failure
                {
                    thisSceneData.AcceptableCandidates.Remove(actor);
                    #if DEBUG
                    thisSceneData.Log.AppendLine($" (-) actor rng weight trimmed from scene placement: [{actor.Name}]");
                    #endif
                }
            }

            // special cases
            if (thisSceneData.AcceptableCandidates.Any(a => a.ActorEnum == GameObjects.Actor.GaboraBlacksmith))
            {
                // we cannot place both the blacksmith and his acountaint in the same place, talking to one can BREAK, but almost always only does this if both are present
                // random coin toss, remove one
                var targetActorEnum = (thisSceneData.RNG.Next() % 2 == 1) ? (GameObjects.Actor.GaboraBlacksmith) : (GameObjects.Actor.Zubora);
                thisSceneData.AcceptableCandidates.RemoveAll(a => a.ActorEnum == targetActorEnum);
            }
        }

        private static void SplitSceneLikeLikesIntoTwoActorObjects(SceneEnemizerData thisSceneData)
        {
            /// Special case: likelikes need to be split into two objects because ground and water share one object 
            /// but no other enemies work as dual replacement, so we want to split into two actor groups for replacement

            if ((thisSceneData.Scene.File == GameObjects.Scene.ZoraCape.FileID() || thisSceneData.Scene.File == GameObjects.Scene.GreatBayCoast.FileID())
                && thisSceneData.Objects.Contains(GameObjects.Actor.LikeLike.ObjectIndex()))
            {
                // add shield object to list of objects we can swap out
                thisSceneData.Objects.Add(GameObjects.Actor.LikeLikeShield.ObjectIndex());
                // generate a candidate list for the second likelike
                for (int i = 0; i < thisSceneData.Actors.Count; ++i)
                {
                    // update object for all of the second likelikes, so they will use the second object
                    if (thisSceneData.Actors[i].ActorId == (int)GameObjects.Actor.LikeLike
                        && GameObjects.Actor.LikeLike.IsGroundVariant(thisSceneData.Actors[i].OldVariant))
                    {
                        var newLikeLike = thisSceneData.Actors[i];
                        newLikeLike.OldObjectId = newLikeLike.ObjectId = GameObjects.Actor.LikeLikeShield.ObjectIndex();
                    }
                }
            }
        }

        private static void AddAniObjectIfTerminaFieldTree(SceneEnemizerData thisSceneData)
        {
            /// because we randomized the tree, and the tree spawns ani, we should be able to add the ani object back into the list of objects
            /// otherwise we are wasting precious object list space on an object that will never be used

            if (thisSceneData.Scene.SceneEnum != GameObjects.Scene.TerminaField)
                return;

            if (thisSceneData.Objects.Contains(GameObjects.Actor.Treee.ObjectIndex()))
            {
                // if tree is randomized, then ani is dead, the object is re-usable
                // what we probably should do is re-allocate some actors from leever or something to make a new actor group
                // but for now, we will randomly change this object ahead of time to something that is likely to get us free actors
                // TODO I might make this randomly selected objects instead

                var freeObjList = new List<int>
                {
                    GameObjects.Actor.ClayPot.ObjectIndex(),
                    GameObjects.Actor.Postbox.ObjectIndex(),
                    GameObjects.Actor.BeanSeller.ObjectIndex(),
                    GameObjects.Actor.IronKnuckle.ObjectIndex(),
                    GameObjects.Actor.Dodongo.ObjectIndex(),
                    GameObjects.Actor.Scarecrow.ObjectIndex(),
                    GameObjects.Actor.FriendlyCucco.ObjectIndex(),
                    GameObjects.Actor.BombFlower.ObjectIndex(),
                    GameObjects.Actor.HappyMaskSalesman.ObjectIndex()
                };

                var newObject = SMALLEST_OBJ;
                if (thisSceneData.RNG.Next() % 10 > 5) // chance of fixed rare/random actor
                {
                    newObject = freeObjList[thisSceneData.RNG.Next() % (freeObjList.Count -1)];
                } 

                // for now we just bypass rando and set it manually
                thisSceneData.Scene.Maps[0].Objects[6] = newObject;
            }
        }

        private static void AddExtraObjectToPiratesInterior(SceneEnemizerData thisSceneData)
        {
            /// With enemizer/actorizer pirates interior is actually kinda dry and boring
            /// the scene has 11 objects, we can add another object to the scene to give enemizer some more free-object actors it can place
            /// also the scene has an unused object (that doesn't get used in enemizer now) we can swap out for something random

            if (thisSceneData.Scene.SceneEnum != GameObjects.Scene.PiratesFortress) return;

            // for now there should only be some chance of this happening in case the object budget is too close to call
            //if (thisSceneData.RNG.Next() % 10 > 2) // nvm seems fine
            {
                List<int> freeObjList = new List<int>
                {
                    GameObjects.Actor.ClayPot.ObjectIndex(), // flying clay pot for enemizer, actual clay pots for actorizer
                    GameObjects.Actor.IronKnuckle.ObjectIndex(),
                    GameObjects.Actor.LikeLike.ObjectIndex(),
                    GameObjects.Actor.DeathArmos.ObjectIndex(),
                    GameObjects.Actor.Guay.ObjectIndex(),
                    GameObjects.Actor.Nejiron.ObjectIndex(),
                    GameObjects.Actor.ReDead.ObjectIndex(),
                    GameObjects.Actor.RedBubble.ObjectIndex(), // both bubble types
                    GameObjects.Actor.PatrollingPirate.ObjectIndex(), // thats right, have a chance of putting some of them back in
                    GameObjects.Actor.DekuPatrolGuard.ObjectIndex(),
                };

                if (ACTORSENABLED)
                {
                    freeObjList.AddRange(
                        new List<int>{
                            GameObjects.Actor.Postbox.ObjectIndex(),
                            GameObjects.Actor.BeanSeller.ObjectIndex(),
                            GameObjects.Actor.Scarecrow.ObjectIndex(),
                            GameObjects.Actor.FriendlyCucco.ObjectIndex(),
                            GameObjects.Actor.HappyMaskSalesman.ObjectIndex(),
                            GameObjects.Actor.ImposterFrog.ObjectIndex(),
                            GameObjects.Actor.BombFlower.ObjectIndex()
                        }
                    ); ; ;
                }

                var newObject1 = freeObjList[thisSceneData.RNG.Next() % (freeObjList.Count - 1)];
                freeObjList.Remove(newObject1);
                var newObject2 = freeObjList[thisSceneData.RNG.Next() % (freeObjList.Count - 1)];
                Debug.WriteLine($"+ extra object for pirates fortress interior is [0x{newObject1.ToString("X")}]");
                Debug.WriteLine($"+ kaizoku object replacemnet for pirates fortress interior is [0x{newObject2.ToString("X")}]");

                thisSceneData.Scene.Maps[0].Objects.Add(newObject1);

                // have to update the scene data to load a larger object list in the game
                var pirateSceneData = RomData.MMFileList[GameObjects.Scene.PiratesFortress.FileID() + 1].Data;
                pirateSceneData[0x31] = 12;

                // for some dumb reason the kaizoku (pirate leutenant you fight in the inteior) is here too, we can change this to something useful since it never gets used
                thisSceneData.Scene.Maps[0].Objects[3] = newObject2;
            }
        }


        [System.Diagnostics.DebuggerDisplay("{Scene.SceneEnum.ToString()}")]
        public class SceneEnemizerData
        {
            // more and more of this stuff needs to be passed to each function, if I want to tame the big mess that is SwapSceneEnemies
            // All common data we have/use in randomizing actors per scene in one data struct

            public Scene Scene;
            public StringBuilder Log;
            public Random RNG;
            public DateTime StartTime;
            public List<Actor> Actors;
            public List<Actor> StandaloneActors; // without an object dependency
            public List<Actor> SceneFreeActors;
            public List<int> Objects;
            public List<List<int>> AllObjects;
            public List<ValueSwap> ChosenReplacementObjects;
            public List<List<int>> ChosenReplacementObjectsPerMap;
            public List<Actor> AcceptableCandidates;
            // outer layer is per object
            public List<List<Actor>> ActorsPerObject     = new List<List<Actor>>();   
            public List<List<Actor>> CandidatesPerObject = new List<List<Actor>>();
            public ActorsCollection ActorCollection = null; // used for ram space statistics
            public int FreeActorRate = 75; // percentage chance of getting a free actor instead of an empty actor during trim

            public SceneEnemizerData(Scene scene)
            {
                this.StartTime = DateTime.Now;
                this.Scene = scene;
                this.Log = new StringBuilder();
            }
        }

        public static void SwapSceneEnemies(Scene scene, int seed)
        {
            /// randomize all enemies/actors in a single scene

            // got tired of function with 10+ parameters, so now this thread has context to store all data in one place
            SceneEnemizerData thisSceneData = new SceneEnemizerData(scene);

            // issue: this function is called in paralel, if the order is different the Random object will be different and not seed-reproducable
            // instead of passing the Random instance, we pass seed and add it to the unique scene number to get a replicatable, but random, seed
            thisSceneData.RNG = new Random(seed + scene.File);

            #region Log Handling functions
            // spoiler log already written by this point, for now making a brand new one instead of appending
            void WriteOutput(string str, StringBuilder altLog = null)
            {
                if (altLog != null)
                    altLog.AppendLine(str);
                else
                    thisSceneData.Log.AppendLine(str);
            }
            void FlushLog()
            {
                EnemizerLogMutex.WaitOne(); // with paralel, thread safety
                using (StreamWriter sw = new StreamWriter(_outputSettings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.Write(thisSceneData.Log);
                }
                EnemizerLogMutex.ReleaseMutex();
            }

            string GET_TIME(DateTime log)
            {
                return ((DateTime.Now).Subtract(log).TotalMilliseconds).ToString();
            }

            if (scene.SceneEnum == GameObjects.Scene.TerminaField || scene.SceneEnum == GameObjects.Scene.IkanaCanyon)
            {
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal; // need more time than the other small scenes
            }
            WriteOutput($" starting timestamp : [{DateTime.Now.ToString("hh:mm:ss.fff tt")}]");
            #endregion

            GetSceneEnemyActors(thisSceneData);
            if (thisSceneData.Actors.Count == 0)
            {
                return; // if no enemies, no point in continuing
            }
            if (thisSceneData.Scene.HasDungeonObject()) // temp: if we have dungeon pots, our actor exclusion code doesnt work because its a dungeon object
            {
                var sceneExcludeAttr = GameObjects.Actor.ClayPot.GetAttribute<ForbidFromSceneAttribute>();
                if (sceneExcludeAttr != null && sceneExcludeAttr.ScenesExcluded.Contains(thisSceneData.Scene.SceneEnum))
                {
                    thisSceneData.Actors.RemoveAll(a => a.ActorEnum == GameObjects.Actor.ClayPot);
                }
            }

            WriteOutput("time to read scene enemies: " + GET_TIME(thisSceneData.StartTime) + "ms");

            thisSceneData.Objects = GetSceneEnemyObjects(thisSceneData);
            //var sceneObjectLimit = SceneUtils.GetSceneObjectBankSize(scene.SceneEnum); // er, this isnt used here anymore, why did intelesense not tell me?
            WriteOutput(" time to read scene objects: " + GET_TIME(thisSceneData.StartTime) + "ms");

            WriteOutput("=========================================================================");
            WriteOutput("For Scene: [" + scene.ToString() + "] with fid: " + scene.File + ", with sid: 0x" + scene.Number.ToString("X2"));
            WriteOutput("=========================================================================");
            // WriteOutput(" time to find scene name: " + GET_TIME(thisSceneData.StartTime) + "ms");

            // if actor does NOT exist, but object does, probably spawned by something else; remove from actors scheduled to randomize
            // TODO check for side objects that no longer need to exist and replace with possible alt objects
            // example: dinofos has a second object: dodongo, just for the fire breath dlist
            foreach (int obj in thisSceneData.Objects.ToList())
            {
                // find all actors we want to replace that use this object
                if ( (VanillaEnemyList.FindAll(act => act.ObjectIndex() == obj))
                                        // check if any of those actors are in our actors list
                                        .Any(actEnum => thisSceneData.Actors.Any(act => act.ActorId == (int) actEnum))
                                        == false )
                {
                    thisSceneData.Objects.Remove(obj);
                }
            }

            HandleUniqueSceneSpecialObjectBehaviors(thisSceneData);

            WriteOutput(" time to finish removing unnecessary objects: " + GET_TIME(thisSceneData.StartTime) + "ms");

            TrimSceneAcceptableCandidateList(thisSceneData);

            // we want to check for actor types that contain fairies per-scene for speed
            var fairyDroppingActors = GetSceneFairyDroppingEnemyTypes(thisSceneData);

            // we group enemies with objects because some objects can be reused for multiple enemies, potential minor boost to variety
            GenerateActorCandidates(thisSceneData, fairyDroppingActors);
            WriteOutput(" time to generate candidate list: " + GET_TIME(thisSceneData.StartTime) + "ms");

            // keeping track of RAM space usage is getting ugly, try some OO to clean it up
            thisSceneData.ActorCollection = new ActorsCollection(scene);
            WriteOutput(" time to separate map/time actors: " + GET_TIME(thisSceneData.StartTime) + "ms");

            GetSceneFreeActors(thisSceneData);

            int loopsCount = 0;
            int objectTooLargeCount = 0;
            var previousyAssignedCandidate = new List<Actor>();
            var bogoLog = new StringBuilder();
            var bogoStartTime = DateTime.Now;

            while (true) /// bogo sort, try to find an actor/object combos that fits in the space we took it out of
            {
                #region loopCounting
                /// preventing inf looping, and re-adjustments due to poor looping results not finding a solution
                //bogoLog.Clear();
                bogoStartTime = DateTime.Now;

                // if we've tried 5 seeds and no results, re-shuffle the candidate lists, maybe the rng was bad
                loopsCount++;
                if (loopsCount % 4 == 0)
                {
                    if (objectTooLargeCount > 0)
                    {
                        /// if we have run out of object space before, from now limit big object actor changes of getting picked to reduce likehood of next cycle
                        List<Actor> bigObjectActors = thisSceneData.AcceptableCandidates.FindAll(o => o.ObjectSize >= 0x6000); // 0x6000 is roughly the median
                        // remove one randomly
                        if (bigObjectActors.Count > 0)
                        {
                            var randomObject = bigObjectActors[thisSceneData.RNG.Next() % bigObjectActors.Count].ObjectId;
                            var actorsPerObject = thisSceneData.AcceptableCandidates.FindAll(a => a.ObjectId == randomObject);
                            foreach (var a in actorsPerObject)
                            {
                                thisSceneData.AcceptableCandidates.Remove(a);
                                WriteOutput($" % removing large actor to reduce time to build: [{a.Name}]]", bogoLog);
                            }
                            objectTooLargeCount = 0;

                        }
                    }

                    // reinit actorCandidatesLists because this RNG is bad
                    GenerateActorCandidates(thisSceneData, fairyDroppingActors);
                    WriteOutput($" re-generate candidates time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);
                }
                if (loopsCount >= 500) // inf loop catch
                {
                    // this shouldn't happen, un-ravel our weights

                }
                if (loopsCount >= 900) // inf loop catch
                {
                    var error = " No enemy combo could be found to fill this scene: " + scene.SceneEnum.ToString() + " w sid:" + scene.Number.ToString("X2");
                    WriteOutput(error);
                    WriteOutput("Failed Candidate List:");
                    foreach (var list in thisSceneData.CandidatesPerObject)
                    {
                        WriteOutput(" Enemy:");
                        foreach (var match in list)
                        {
                            WriteOutput("  Enemytype candidate: " + match.Name + " with vars: " + match.Variants[0].ToString("X2"));
                        }
                    }
                    thisSceneData.ActorCollection.PrintAllMapRamObjectOutput(thisSceneData.Log);
                    FlushLog();
                    throw new Exception(error);
                }
                if (loopsCount > 50 && thisSceneData.FreeActorRate > 0) // reduce free enemy rate 1 percentage per loop over 50
                {
                    thisSceneData.FreeActorRate--;
                }
                #endregion

                ShuffleObjects(thisSceneData);
                WriteOutput($" objects pick time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                // enemizer is not smart enough if the new chosen objects are copies, and the game allows objects to load twice
                // for now remove them here after objects are chosen, to reduce object size
                StringBuilder objectReplacementLog = new StringBuilder();
                thisSceneData.AllObjects = TrimObjectList(thisSceneData, objectReplacementLog);
                WriteOutput($" object trim time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                // check if objects fits now, because the rest can take awhile and at least for termina field we can check this waaaaay earlier
                thisSceneData.ActorCollection.SetNewActors(scene, thisSceneData.AllObjects);
                WriteOutput($" set new actors: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                var objectOverflowCheck = thisSceneData.ActorCollection.isObjectSizeAcceptable();
                if (objectOverflowCheck > 0){
                    WriteOutput($"---- bogo REJECTED: obj pre-check failed (size:{objectOverflowCheck}): [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);
                    objectTooLargeCount++;
                    continue; // not enough space, continue
                } else {
                    WriteOutput($" pre-checking object size: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);
                }

                // for each object, attempt to change actors 
                for (int objectIndex = 0; objectIndex < thisSceneData.ChosenReplacementObjects.Count; objectIndex++)
                {
                    // todo consider attempting to make this multithreaded at this upper level
                    //   issues: we would need to do a final actor trim pass after (edit: we do this now anyway, we still need an accurate type read)

                    var knownChangedActorList = new List<Actor>();
                    var chosenObject = thisSceneData.ChosenReplacementObjects[objectIndex].ChosenV;
                    List<Actor> subMatches = thisSceneData.CandidatesPerObject[objectIndex].FindAll(act => act.ObjectId == chosenObject);

                    #if DEBUG
                    var object_actor = VanillaEnemyList.Find(act => act.ObjectIndex() == chosenObject);
                    #endif
                    Debug.Assert(subMatches.Count > 0);

                    AddCompanionsToCandidates(thisSceneData, objectIndex, subMatches);
                    //WriteOutput($"  companions adding time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                    ShuffleActors(thisSceneData, objectIndex, subMatches, previousyAssignedCandidate, knownChangedActorList);
                    //WriteOutput($"  match time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                    TrimAllActors(thisSceneData, previousyAssignedCandidate, knownChangedActorList);
                    // WriteOutput($"  trim/free time: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                    previousyAssignedCandidate.Clear(); // TODO this might not be needed at all anymore
                } // end for actors per object
                WriteOutput($" exit per-object: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                // finally, randomize actors that have no objects (standalone)
                if (ACTORSENABLED)
                {
                    var knownChangedActorList = new List<Actor>();

                    // assuming we dont have free actors with companions

                    ShuffleStandaloneActors(thisSceneData/*, previousyAssignedCandidate*/);
                    WriteOutput($" exit sandalone randomize: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);
                }

                // this no longer works after object re-write, can just lead to rando thinking it has more objects than it does
                // for now, disable this and test without. I dont think it is needed anymore, now that we shuffle the available candidiates every x cycles
                //if (loopsCount >= 100)
                //{
                // if we are taking a really long time to find replacements, remove a couple optional actors/objects
                //CullOptionalActors(scene, thisSceneData.ChosenReplacementObjects, loopsCount);
                //WriteOutput(" cull optionals: " + GET_TIME(bogoStartTime) + "ms", bogoLog);
                //}

                // set objects and actors for isSizeAcceptable to use, and our debugging output
                thisSceneData.ActorCollection.SetNewActors(scene, thisSceneData.AllObjects ); // 30~70ms for this? hmm

                WriteOutput($" set for size check: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                // dyna overflow is a common crash concern, here we need to check if we overflow and shrink the dyna actor count
                var dynaLog = new StringBuilder();
                var dynatest = thisSceneData.ActorCollection.isDynaSizeAcceptable();
                if (dynatest != "acceptable")
                {
                    // we failed the first test, try removing some dyna actors to compensate
                    // now we need to try trimming the dyna to smaller size by reducing each dyna by one until it fits or doesnt

                    var dynaTrimSuccess = TrimDynaActors(thisSceneData, dynaLog);
                }

                WriteOutput($" set for dyna trim: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                // we need to do one last actor limit pass because we didnt keep track of limits and may have re-added more earlier during trimming
                FinalActorLimitTrim(thisSceneData);

                WriteOutput($" set after final actor trim: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                thisSceneData.ActorCollection.SetNewActors(scene, thisSceneData.AllObjects);

                WriteOutput($" set after second setnewactors for final data test: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                if (thisSceneData.ActorCollection.isSizeAcceptable(bogoLog)) // SUCCESS
                {
                    WriteOutput($" after isSizeAcceptable: [{GET_TIME(bogoStartTime)}ms][{GET_TIME(thisSceneData.StartTime)}ms]", bogoLog);

                    //thisSceneData.Log.Append(objectReplacementLog);
                    thisSceneData.Log.Append(dynaLog);
                    break; // done, break loop
                }
                // else: not small enough; reset loop and try again

            } // end while searching for compatible object/actors

            WriteOutput(" time to find matching candidates: " + GET_TIME(thisSceneData.StartTime) + "ms");
            WriteOutput(" Loops used for match candidate: " + loopsCount);

            #region Debugging: Actor Forcing
            #if DEBUG
            ////////////////////////////////////////////
            ///////   DEBUGGING: force an actor  ///////
            ////////////////////////////////////////////
            if (scene.SceneEnum == GameObjects.Scene.AstralObservatory) // force specific actor/variant for debugging
            {
                //thisSceneData.Actors[35].ChangeActor(GameObjects.Actor.En_Invisible_Ruppe, vars: 0x01D0); // hitspot
                //thisSceneData.Actors[12].ChangeActor(GameObjects.Actor.ObjSwitch, vars: 0x7504); // hitspot
                //thisSceneData.Scene.Maps[0].Actors[9].ChangeActor(GameObjects.Actor.Clock, vars: 0x907F);
                //thisSceneData.Scene.Maps[0].Actors[2].ChangeActor(GameObjects.Actor.Clock, vars: 0x907F);
            }
            /////////////////////////////
            #endif
            /////////////////////////////
            #endregion

            var flagLog = new StringBuilder();

            ActorizerForceDropHeavyGrassMinimum(thisSceneData);

            FixGroundToFlyingActorHeights(thisSceneData, flagLog); // putting flying actors on ground spawns can be weird
            FixRedeadSpawnScew(thisSceneData); // redeads don't like x/z rotation
            FixBrokenActorSpawnCutscenes(thisSceneData); // some actors dont like having bad cutscenes
            FixWaterPostboxes(thisSceneData);
            FixSnowballActorSpawns(thisSceneData);
            // the following modify Variant which can confuse typing system
            FixPathingVars(thisSceneData); // any patrolling types need their vars fixed
            FixKickoutEnemyVars(thisSceneData); // and same with the two actors that have kickout addresses
            FixSwitchFlagVars(thisSceneData, flagLog);
            FixTreasureFlagVars(thisSceneData, flagLog);

            // print debug actor locations
            WriteOutput("####################################################### ");
            for (int a = 0; a < thisSceneData.Actors.Count; a++)
            {
                var actor = thisSceneData.Actors[a];
                string dsize = actor.DynaLoad.poly > 0 ? $" dyn: [{actor.DynaLoad.poly}]" : "";
                #if DEBUG
                var actorNameData = $"  Old actor:[{thisSceneData.Scene.SceneEnum}]r[{actor.Room.ToString("D2")}]n[{actor.OldName}]v[0x{actor.OldVariant.ToString("X4")}]";
                #else
                var actorNameData = $"  Old actor:r[{actor.Room.ToString("D2")}]n[{actor.OldName}]v[0x{actor.OldVariant.ToString("X4")}] ";
                #endif
                WriteOutput(actorNameData +
                    $" replaced by new actor: [{actor.Variants[0].ToString("X4")}]" +
                    $"[{actor.Name}]"
                    + dsize);
            }

            WriteOutput("---------------------------------------------------------");
            thisSceneData.Log.Append(flagLog);
            WriteOutput("---------------------------------------------------------");
            thisSceneData.ActorCollection.PrintAllMapRamObjectOutput(thisSceneData.Log);
            WriteOutput("---------------------------------------------------------");
            thisSceneData.Log.Append(bogoLog);
            WriteOutput("####################################################### ");

            // realign all scene companion actors
            MoveAlignedCompanionActors(thisSceneData);

            SetSceneEnemyObjects(scene, thisSceneData.ChosenReplacementObjectsPerMap);
            SceneUtils.UpdateScene(scene); // writes scene actors back to binary

            WriteOutput($" time to complete randomizing [{scene.SceneEnum}]: " + GET_TIME(thisSceneData.StartTime) + "ms");
            WriteOutput($" ending timestamp : [{DateTime.Now.ToString("hh:mm:ss.fff tt")}]");
            FlushLog();
        }

        #region Actor Injection

        public static InjectedActor ParseMMRAMeta(string metaFile)
        {
            /// every MMRA comes with one meta file per bin, this contains metadata
            var vanillaActors = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList();
            var newInjectedActor = new InjectedActor();

            foreach (var line in metaFile.Split('\n'))
            {
                var asignment = line.Split('#')[0].Trim(); // remove comments

                if (asignment.Length == 0) // comment or empty line: ignore
                {
                    continue;
                }

                var asignmentSplit = asignment.Split('=');
                var command = asignmentSplit[0].Trim();
                if (command == "unkillable")
                {
                    newInjectedActor.unkillableAttr = new UnkillableAllVariantsAttribute();
                    continue;
                }
                if (command == "only_one_per_room")
                {
                    newInjectedActor.onlyOnePerRoom = new OnlyOneActorPerRoom();
                    continue;
                }

                string valueStr = asignmentSplit[1].Trim();

                if (command == "ground_variants")
                {
                    var newGroundVariants = valueStr.Split(",").ToList();
                    var newGroundVariantsShort = newGroundVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.groundVariants = newGroundVariantsShort;
                    continue;
                }
                if (command == "flying_variants")
                {
                    var newFlyingVariants = valueStr.Split(",").ToList();
                    var newFlyingVariantsShort = newFlyingVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.flyingVariants = newFlyingVariantsShort;
                    continue;
                }
                if (command == "water_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "watertop_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterTopVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "waterbottom_variants")
                {
                    var newWaterVariants = valueStr.Split(",").ToList();
                    var newWaterVariantsShort = newWaterVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.waterBottomVariants = newWaterVariantsShort;
                    continue;
                }
                if (command == "variant_with_max")
                {
                    var newLimitedVariant = valueStr.Split(",").ToList();
                    int max = Convert.ToInt32(newLimitedVariant[1].Trim(), 10);
                    int variant = Convert.ToInt32(newLimitedVariant[0].Trim(), 16);

                    newInjectedActor.limitedVariants.Add(new VariantsWithRoomMax(max, variant));
                    continue;
                }
                if (command == "dyna_load")
                {
                    var newDynaValuePair = valueStr.Split(",").ToList();
                    var intBase = newDynaValuePair[0].Contains("0x") ? 16 : 10;
                    newInjectedActor.DynaLoad.poly = Convert.ToInt32(newDynaValuePair[0].Trim(), intBase);
                    intBase = newDynaValuePair[0].Contains("0x") ? 16 : 10;
                    newInjectedActor.DynaLoad.vert = Convert.ToInt32(newDynaValuePair[1].Trim(), intBase);
                    continue;
                }


                var value = Convert.ToInt32(valueStr, fromBase: 16);
                if (command == "actor_id")
                {
                    newInjectedActor.ActorId = value;
                }
                else if (command == "obj_id")
                {
                    newInjectedActor.ObjectId = value;
                }
                else if (command == "file_id" || command == "actor_fid")
                {
                    newInjectedActor.fileID = Convert.ToInt32(valueStr, fromBase: 10);
                }
                else if (command == "object_fid")
                {
                    newInjectedActor.ObjectFid = Convert.ToInt32(valueStr, fromBase: 10);
                }

                var uvalue = Convert.ToUInt32(valueStr, fromBase: 16);

                if (command == "initvars_offset")
                {
                    newInjectedActor.initVarsLocation = uvalue;
                }
                else if (command == "vram_start")
                {
                    newInjectedActor.buildVramStart = uvalue;
                }
            } // for each line end

            // update actor init vars in our actor
            var actorGameObj = vanillaActors.Find(act => act.FileListIndex() == newInjectedActor.fileID);
            if (actorGameObj != 0)
            {
                var initVarsAttr = actorGameObj.GetAttribute<ActorInitVarOffsetAttribute>();
                if (initVarsAttr != null) // had one before, change now
                {
                    // untested, might not work
                    initVarsAttr.Offset = (int)newInjectedActor.initVarsLocation;
                }
            }

            return newInjectedActor;
        }

        public static List<string> GenerateMMRAFileList(string directory)
        {
            var directories = new List<string> { };

            directories.AddRange(Directory.GetDirectories(directory).ToList()); // depth 1
            foreach (string d in directories.ToList()) // another layer deep to be safe
            {
                List<String> deeperDirectories = Directory.GetDirectories(d).ToList();
                directories.AddRange(deeperDirectories); // depth 2
            }
            directories.Add(directory); // added after to avoid contamination

            var files = new List<string> { };

            foreach (var dir in directories)
            {
                files.AddRange(Directory.GetFiles(dir, "*.mmra"));
            }

            return files;
        }

        public static void ScanForMMRA(string directory)
        {
            // decomp lets us more easily modify actors now
            // for now, until cat/zoey figure out how to directly integrate the projects
            //   I will, instead, compile with decomp, and then extract the binaries and inject here
            // MMRA files: Majora Mask Rando Actor files, just zip files that contain binaries and extras later
            // ideas for extras: notes to tell rando where sound effects are to be replaced
            // function pointers to interconnect the code

            if ( ! Directory.Exists(directory)) return;
            // if actorizer is off, we need to not read any of these
            if (!_randomized.Settings.RandomizeEnemies) return; // right now actorizer/enemizer is the only system that uses this

            uint END_VANILLA_OBJ_SEGMENT = 0x01E5E600;

            InjectedActors.Clear(); // from last gen
            var codeFile = RomData.MMFileList[31].Data;
            var objectTableOffset = 0x11CC80;      

            foreach (string filePath in GenerateMMRAFileList(directory))
            {
                if (filePath.Contains("SafeBoat.mmra")
                 || filePath.Contains("FairySpot.mmra") // is missing a variant, and was not working, not even sure what it was doing, TODo
                 || filePath.Contains("Dinofos"))
                {
                    //throw new Exception("SafeBoat.mmra no longer works in actorizer 1.16, \n remove the file from MMR/actors and start a new seed.");
                    continue;
                }

                if (_randomized.Settings.Character == Models.Character.AdultLink && filePath.Contains("Anope.mmra"))
                {
                    continue; // this OOT epona replacement actor does not work with adult oot link mod because it replaces horse assets
                }

                try
                {
                    using (ZipArchive zip = ZipFile.OpenRead(filePath))
                    {

                        if (zip.Entries.Where(e => e.Name.Contains(".bin")).Count() == 0)
                        {
                            throw new Exception($"ERROR: cannot find a single binary actor in file {filePath}");
                        }

                        // per binary, since MMRA should support multiple binaries
                        foreach (ZipArchiveEntry binFile in zip.Entries.Where(e => e.Name.Contains(".bin")))
                        {
                            var filename = binFile.Name.Substring(0, binFile.Name.LastIndexOf(".bin"));

                            // read overlay binary data
                            int newBinLen = ((int) binFile.Length) + ((int) binFile.Length % 0x10); // dma padding
                            var overlayData = new byte[newBinLen];
                            binFile.Open().Read(overlayData, 0, overlayData.Length);

                            // the binary filename convention will be NOTES_name.bin

                            //var binFilenameSplit = binFile.Name.Split('_'); // everything before _ is a comment, readability, discard here
                            //var fileIDtext = binFilenameSplit.Length > 1 ? binFilenameSplit[binFilenameSplit.Length - 1] : binFile.Name;
                           
                            // read the associated meta file
                            var metaFileEntry = zip.GetEntry(filename + ".meta");
                            if (metaFileEntry == null) // meta not found
                                throw new Exception($"Could not find a meta for actor bin [{binFile.Name}]\n   in [{filePath}]");

                            var injectedActor = ParseMMRAMeta(new StreamReader(metaFileEntry.Open(), Encoding.Default).ReadToEnd());
                            injectedActor.filename = filePath; // debugging

                            // check for duplicate actor
                            var copyOvlFileSearch = InjectedActors.Find(act => act.fileID == injectedActor.fileID);
                            if (copyOvlFileSearch != null)
                            {
                                throw new Exception("\n\n" +
                                    "ERROR (Actor Inject):\n" +
                                    " Two separate actor files are trying to overwrite the same file.\n" +
                                    "File 1: " + injectedActor.filename + "\n" +
                                    "File 2: " + copyOvlFileSearch.filename + "\n\n" +
                                    "Please remove one before building another seed.\n");
                            }

                            // we need to inject actors if we find them
                            // TODO move this to a "load all objects" separate function where we rank them by size
                            // so we can re-use some old spots instead of just extending
                            // NOTE: this does not work
                            /* var objectFileEntry = zip.GetEntry(filename + ".object");
                            if (objectFileEntry != null) // object included
                            {
                                newBinLen = ((int)objectFileEntry.Length) + ((int)objectFileEntry.Length % 0x10); // dma padding
                                var objectData = new byte[newBinLen];
                                objectFileEntry.Open().Read(objectData, 0, objectData.Length);

                                RomData.MMFileList[injectedActor.ObjectFid].Data = objectData;
                                RomData.MMFileList[injectedActor.ObjectFid].WasEdited = true;

                                // we need to update the object table with the size of the new object
                                uint newSegmentROMStart = END_VANILLA_OBJ_SEGMENT;
                                uint newSegmentROMEnd = newSegmentROMStart + (uint) objectData.Length;
                                if (newSegmentROMEnd > 0x02000000)
                                {
                                    throw new Exception("Object segment overflow, reduce your actors that use custom objects");
                                }
                                END_VANILLA_OBJ_SEGMENT = newSegmentROMEnd;
                                ReadWriteUtils.Arr_WriteU32(codeFile, (objectTableOffset + (2 * 4 * injectedActor.ObjectId)), newSegmentROMStart);
                                ReadWriteUtils.Arr_WriteU32(codeFile, (objectTableOffset + (2 * 4 * injectedActor.ObjectId + 4)), newSegmentROMEnd);
                            } // */


                            InjectedActors.Add(injectedActor);

                            // we have to add the changes to our list of actors we are going to use in enemizer/actorizer
                            // behavior now differs between replacement actors and brand new
                            var replacementEnemySearch = ReplacementCandidateList.Find(act => act.ActorId == injectedActor.ActorId);
                            //var replacementListSearch = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList().Find(act => (int) act == injectedActor.ActorId);
                            if (replacementEnemySearch != null) // previous actor
                            {
                                replacementEnemySearch.UpdateActor(injectedActor);
                            }
                            //else (injectedActor.)
                            /* else if (injectedActor.fileID != 0)
                            {
                                // sometimes we want to inject an actor that wont be used by actorizer/enemizer,
                                // so it wont be in the list above, but its not marked as a new actor either
                                replacementEnemySearch = null;
                            } // */
                            else
                            {
                                replacementEnemySearch = new Actor(injectedActor, filename);
                                ReplacementCandidateList.Add(replacementEnemySearch);
                            }

                            if (injectedActor.ObjectId <= 3)
                            {
                                var freeCandidateSearch = FreeCandidateList.Find(act => act.ActorId == injectedActor.ActorId);
                                if (freeCandidateSearch == null)
                                {
                                    //FreeCandidateList.Add(replacementEnemySearch);
                                    FreeCandidateList.Add( new Actor(injectedActor, filename) );
                                }
                                else
                                {
                                    freeCandidateSearch.UpdateActor(injectedActor);
                                }
                            }

                            // experiment: lets not re-compress our actor and see what happens

                            // this is separate from the above because this lets us modify files not found in ReplacementCandidateList
                            // like demo_kankyo, which is a free actor and not a regular candidate
                            var newFID = (int) injectedActor.fileID;
                            injectedActor.overlayBinLen = (uint)overlayData.Length;
                            if (newFID == 0)
                            {
                                injectedActor.overlayBin = overlayData; // save bin for now
                            }
                            else
                            {
                                /// overwrite the file now
                                RomData.MMFileList[newFID].Data = overlayData;
                                // we CANNOT update the .end because it breaks MMR's romaddr->file+offset calculations
                                //   MMR will attempt to write romhacks for the following actor to our new bigger actor
                                //   we would have to rewrite half of rando to get around that
                                // thankfully, this updating end isn't actually necessary it seems, we can leave this vanilla
                                //RomData.MMFileList[newFID].End = RomData.MMFileList[newFID].Addr + newBinLen;
                                RomData.MMFileList[newFID].WasEdited = true;
                                // injectedActor.overlayBin = overlayData; // we dont save bin if its a previous file
                            }

                            RomData.MMFileList[newFID].IsCompressed = false;

                        } // foreach bin entry

                    }// zip as file end
                } // try end
                catch (Exception e)
                {
                    throw new Exception($"Error attempting to read archive: {filePath} -- \n" + e);
                }

            } // for each mmra end
        }

        public static void UpdateOverlayVRAMReloc(MMFile file, int[] sectionOffsets, uint newVRAMOffset)
        {
            /// Reloc: overlay c code is compiled with VRAM addresses already baked in,
            ///  these get adjusted when the overlay is loaded into RAM, to match the RAM locations
            ///  but when we inject this new overlay we move its VRAM to a different place, so its wrong
            ///  so now, we must re-apply the VRAM addresses so when the game shifts them into RAM it will have the correct values

            var relocSize = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
            // the table pointer at the end is an offset from the end, we need to swap it
            int tableOffset = (int)(file.Data.Length - relocSize);
            int relocEntryCountLocation = (int)(tableOffset + (4 * 4)); // first four ints are section sizes

            uint relocEntryCount = ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryCountLocation);
            var relocEntryLoc = relocEntryCountLocation + 4; // first overlayEntry immediately after reloc count
            var relocEntryEndLoc = relocEntryLoc + (relocEntryCount * 4);
            // traverse the whole relocation section, parse the changes, apply

            uint pointer = 0; // save outside of loop incase of multiple combos

            while (relocEntryLoc < relocEntryEndLoc)
            {
                // each overlayEntry in reloc is one nibble of shifted section, one nible of type, and 3 bytes of address
                // text section starts at 1 not 0
                var section = ((file.Data[relocEntryLoc] & 0xC0) >> 6) - 1;
                var sectionOffset = sectionOffsets[section];

                var commandType = (file.Data[relocEntryLoc] & 0xF);
                var commandTypeLookahead = (file.Data[relocEntryLoc + 4] & 0xF); // double command for LUI/ADDIU

                if (commandType == 0x5 /* R_MIPS_HI16 */ && commandTypeLookahead == 0x6) // LUI/ADDIU combo
                {
                    int luiLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;

                    // addu treats the last two bytes of our pointer as signed
                    // to fix this, the LUI command is given a carry over bit to fix it, we need to read and write knowing this
                    // combine the halves from asm back into one pointer
                    pointer = 0;
                    pointer = ((uint)ReadWriteUtils.Arr_ReadU16(file.Data, addiuLoc + 2));
                    int LUIDecr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0;
                    uint oldLuiData = ReadWriteUtils.Arr_ReadU16(file.Data, luiLoc + 2);
                    pointer |= ((uint)(oldLuiData - LUIDecr) << 16);

                    pointer += newVRAMOffset;

                    // separate the pointer again into halves and put back
                    int LUIIncr = ((pointer & 0xFFFF) > 0x8000) ? 1 : 0; // if the lower half is too big we have to add one to LUI
                    ushort luiPart = (ushort)(((pointer & 0xFFFF0000) >> 16) + LUIIncr);
                    ushort adduPart = (ushort)(pointer & 0xFFFF);
                    ReadWriteUtils.Arr_WriteU16(file.Data, luiLoc   + 2, luiPart);
                    ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, adduPart);

                    relocEntryLoc += 8;
                }
                else if (commandType == 0x6 /* R_MIPS_LO16 */) // another ADDIU after the first combo 
                {
                    int addiuLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc + 4)) & 0x00FFFFFF;
                    ushort adduPart = (ushort)(pointer & 0xFFFF);
                    ReadWriteUtils.Arr_WriteU16(file.Data, addiuLoc + 2, adduPart);

                    relocEntryLoc += 4; // another
                }
                else if (commandType == 0x4 /* R_MIPS_24 */) // JAL function calls
                {
                    int jalLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint jal = ReadWriteUtils.Arr_ReadU32(file.Data, jalLoc) & 0x00FFFFFF;
                    uint shiftedJal = jal << 2;
                    shiftedJal += newVRAMOffset;
                    shiftedJal = shiftedJal >> 2;
                    ReadWriteUtils.Arr_WriteU32(file.Data, jalLoc, 0x0C000000 | shiftedJal);

                    relocEntryLoc += 4;
                }
                else if (commandType == 0x2 /* R_MIPS_32 */) // Hard pointer (init/destroy/update/draw pointers can be here, also actual ptr in rodata)
                {
                    int ptrLoc = sectionOffset + ((int)ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc) & 0x00FFFFFF);
                    uint ptrValue = ReadWriteUtils.Arr_ReadU32(file.Data, ptrLoc);
                    ptrValue += newVRAMOffset;
                    ReadWriteUtils.Arr_WriteU32(file.Data, ptrLoc, ptrValue);

                    relocEntryLoc += 4;
                }
                else // unknown command? supposidly Z64 only uses these four although it could support more
                {
                    throw new Exception($"UpdateOverlayVRAMReloc: unknown reloc overlayEntry value:\n" +
                        $" {ReadWriteUtils.Arr_ReadU32(file.Data, relocEntryLoc).ToString("X")}");
                }
            } // end while (we havent reached the end of reloc)
        } // end UpdateOverlayVRAMReloc

        public static void UpdateActorOverlayTable()
        {
            // todo: check if enemizer is set, return if not

            // this is called from romutils.cs right before we build the rom
            /// if overlays have grown, we need to modify their overlay table to use the right values for the new files
            /// every time you move an overlay you need to relocate the vram addresses, so instead of shifting all of them
            ///  we just move the new larger files to the end and leave a hole behind for now

            // TODO can we _detect_ this value by looking at rando is already doing?
            const uint theEndOfTakenVRAM = 0x80C27000; // 0x80C260A0 <- actual
            // can't even remember why I raised it
            //const uint theEndOfTakenVRAM = 0x80CA0000; // TODO change back to lower
            //const int theEndOfTakenVROM = 0x03100000; // 0x02EE7XXX <- actual
            // maybe if I set it longer away I can skip the extra samples getting corrupted, probably not
            const int theEndOfTakenVROM = 0x03400000; // 0x02EE7XXX <- actual

            int actorOvlTblFID = RomUtils.GetFileIndexForWriting(Constants.Addresses.ActorOverlayTable);
            RomUtils.CheckCompressed(actorOvlTblFID);

            // the overlay table exists inside of another file, we need the offset to the table
            var actorOvlTblData = RomData.MMFileList[actorOvlTblFID].Data;
            int actorOvlTblOffset = Constants.Addresses.ActorOverlayTable - RomData.MMFileList[actorOvlTblFID].Addr;

            // generate a list of actors sorted by fid
            var actorList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>().ToList();
            actorList.Remove(GameObjects.Actor.Empty);
            actorList.Remove(GameObjects.Actor.NULL);
            actorList.RemoveAll(act => act.FileListIndex() < 38);
            var fidSortedActors = actorList.OrderBy(x => x.FileListIndex()).ToList();

            uint previousLastVRAMEnd = theEndOfTakenVRAM;
            int previousLastVROMEnd = theEndOfTakenVROM;

            foreach (var injectedActor in InjectedActors)
            {
                // TODO: where does actorid get set for new inject (whihc is currently busted)
                var ActorId = injectedActor.ActorId;
                var fileID = injectedActor.fileID;
                MMFile file = RomData.MMFileList[fileID];

                try
                {
                    int entryLoc = actorOvlTblOffset + (ActorId * 32); // overlay table is sorted by ActorId

                    uint oldVROMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x0);
                    uint oldVROMEnd = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x4);

                    // if build knows where VRAM used to start for this actor, use that
                    // else, use the old VRAM build for the given actor in this slot
                    uint oldVRAMStart = ReadWriteUtils.Arr_ReadU32(actorOvlTblData, entryLoc + 0x08);
                    oldVRAMStart = (injectedActor.buildVramStart != 0) ? (injectedActor.buildVramStart) : (oldVRAMStart);

                    // if it was edited, its not compressed, get new filesize, else diff old address values
                    var uncompresedVROMSize = (file.WasEdited) ? (file.Data.Length) : (file.End - file.Addr);

                    // for now since we have the space, just move all injected actors to the end, even if they are smaller
                    // TODO make a list of previously free holes we can stick stuff into and check that first before using the end
                    // could even do a hermit crab sort to get a list of smaller actors first and do this out of order
                    file.Addr = previousLastVROMEnd;
                    file.End = previousLastVROMEnd + uncompresedVROMSize;
                    previousLastVROMEnd = file.End;

                    // update VROM we have those values now
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0, (uint)file.Addr);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x4, (uint)file.End);

                    // now to update the reloc values of the overlay to match our new vrom location
                    // we know where in the overlay pointers exist that need to be updated for VROM->VRAM
                    // .reloc stores this info for us as a table of words that contain enough info to help us update
                    // the very last byte in the overlay is (from end) offset
                    //   of the table that declares size of text/data/rodata/bss
                    // following those is a count of the reloc entries, followed by the actual entries
                    var fileTableEndOffset = ReadWriteUtils.Arr_ReadU32(file.Data, file.Data.Length - 4);
                    // the table pointer at the end is an offset from the end, we need to swap it
                    int tableOffset = (int)(file.Data.Length - fileTableEndOffset);

                    // the section table only contains section sizes, we need to walk it to know the offsets
                    var sectionOffsets = new int[4];
                    sectionOffsets[0] = 0; // text (always at the start for our overlay system)
                    sectionOffsets[1] = sectionOffsets[0] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 0); // data
                    sectionOffsets[2] = sectionOffsets[1] + (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 4); // rodata
                    var bssSize = (int)ReadWriteUtils.Arr_ReadU32(file.Data, tableOffset + 8);
                    sectionOffsets[3] = sectionOffsets[2] + bssSize;

                    // have to move the overlay vram location assume its bigger
                    // calculate the new VRAM and offset for our new overlay VRAM location
                    //var newVRAMSize = sectionOffsets[3] + relocSize; // what the fuck is this
                    // the only increase in size of the vram is the BSS so just go with that
                    var newVRAMSize = injectedActor.overlayBinLen + bssSize;
                    // TODO check if we can place it in an old hole left behind by a previously moved actor
                    var newVRAMStart = previousLastVRAMEnd;
                    var newVRAMEnd = (uint)(newVRAMStart + newVRAMSize);
                    var newVRAMOffset = newVRAMStart - oldVRAMStart;

                    // all the pointers and vram locations in the file need to be updated too
                    UpdateOverlayVRAMReloc(file, sectionOffsets, newVRAMOffset);

                    uint newInitVarAddr = newVRAMStart + injectedActor.initVarsLocation;

                    // write the VRAM sections of the overlay table entry
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x08, newVRAMStart);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x0C, newVRAMEnd);
                    ReadWriteUtils.Arr_WriteU32(actorOvlTblData, entryLoc + 0x14, newInitVarAddr);

                    previousLastVRAMEnd = newVRAMEnd + (newVRAMEnd % 0x10); // not sure if dma padding matters here
                    RomData.MMFileList[fileID] = file;

                } catch (Exception e)
                {
                    throw new Exception($"Error during actor overlay table reorder of" +
                        $"  actor {ActorId} file {fileID}:\n" +
                        e.ToString());
                }
            }// end Foreach overlay in overlaylist
        } // end UpdateOverlayTable

        public static void InjectNewActors()
        {
            /// this might get merged back in with scan, and/or the pieces get moved back here
            /// we need to build an Actor from our injected actor, and finish injected actor conversions

            if (InjectedActors.Count == 0)  return;

            var freeOverlaySlots = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                        .Where(act => act.ToString().Contains("Empty")).ToList();

            // in case DMA is restricted, start with a list of known bunk files
            var freeFileSlots = new List<int>
            {
                // these files at the end of the vanilla DMA are unused in USA
                // but MMR might use them, do not
                // 1538, 1539, 1540, 1541, 1542, 1543, 1544, 1545, 1546, 1547, 1548, 1549, 1550, 1551,
                // unused actors or objects:
                GameObjects.Actor.UnusedClockTowerSpotlight.FileListIndex(),
                GameObjects.Actor.Obj_Ocarinalift.FileListIndex(),
                GameObjects.Actor.UnusedStoneTowerPlatform.FileListIndex(),
                GameObjects.Actor.Unused_En_Boj_01.FileListIndex(),  // empty actors with nothing in them
                GameObjects.Actor.Unused_En_Boj_02.FileListIndex(),
                GameObjects.Actor.Unused_En_Boj_03.FileListIndex(),
                GameObjects.Actor.En_Boj_04.FileListIndex(),
                GameObjects.Actor.En_Boj_05.FileListIndex(),
                //GameObjects.Actor.En_Stream.FileListIndex(), // is this really unused? we now use it in actorizer
                GameObjects.Actor.SariaSongOcarinaEffects.FileListIndex(), // should be lower down as we might need to use it later
                806, // OoT potion shop man (the first object, not the updated one they used in their unused actor)
                692, // OoT Child zelda (the first object, not the updated one they used in their 3 minute cutscene actor)
            };

            int GetUnusedFileID(InjectedActor injActor)
            {
                if (freeFileSlots.Count > 0)
                {
                    var f = freeFileSlots[0];
                    freeFileSlots.RemoveAt(0);
                    return f;
                } else // we have run out of known free file slots to use
                {
                    // back up, its broken though
                    //return RomUtils.AppendFile(injActor.overlayBin)
                    throw new Exception("We have run out of actors space to inject, please disable an actor in /actors");
                }
            }


            // note: this code does not work and is not reached, this is for brand new actors that dont have vanilla files
            // which is currently and always had been broken [dec/2023]
            foreach (var injectedActor in InjectedActors.FindAll(act => act.ActorId == (int) GameObjects.Actor.NULL))
            {
                /// brand new actors, not replacement
                if (injectedActor.buildVramStart == 0)
                {
                    throw new Exception("new actor missing starting vram:\n " + injectedActor.filename);
                }

                var newFileID = GetUnusedFileID(injectedActor); // todo change this back into hardcoded, its a static rom
                //var newFileID = RomUtils.AppendFile(injectedActor.overlayBin); // broken, wants to put our actor outside of romspace
                injectedActor.fileID = newFileID;
                injectedActor.ActorId = (int)freeOverlaySlots[0];
                freeOverlaySlots.RemoveAt(0);
                var file = RomData.MMFileList[newFileID];
                file.Data = injectedActor.overlayBin;
                file.WasEdited = true;
                file.IsCompressed = true; // assumption: all actors are compressed

                // update actor ID in overlay init vars, now that we know the new actor ID value
                ReadWriteUtils.Arr_WriteU16(file.Data, (int)injectedActor.initVarsLocation, (ushort)injectedActor.ActorId);

                var filenameSplit = injectedActor.filename.Split("\\");
                var newActorName = filenameSplit[filenameSplit.Length - 1];

                RomData.MMFileList[newFileID] = file;
                ReplacementCandidateList.Add(new Actor(injectedActor, newActorName));

                // TODO inject objects too, for actors that have custom objects

            } // end for each injected actor
        }

        #endregion

        public static void ShuffleEnemies(OutputSettings outputSettings, CosmeticSettings cosmeticSettings, Models.RandomizedResult randomized)
        {
            try
            {
                seedrng = new Random(randomized.Seed);
                _randomized = randomized;
                _outputSettings = outputSettings;
                _cosmeticSettings = cosmeticSettings;
                DateTime enemizerStartTime = DateTime.Now;

                // for dingus that want moonwarp, re-enable dekupalace
                var SceneSkip = new GameObjects.Scene[] { //};
                    //GameObjects.Scene.GiantsChamber,
                    GameObjects.Scene.SakonsHideout // issue: the whole gaunlet is one long room, with two clear enemy room puzles
                    };// , GameObjects.Scene.DekuPalace };

                PrepareEnemyLists();
                PrepareJunkItems();
                SceneUtils.ReadSceneTable();
                SceneUtils.GetSceneHeaders();
                SceneUtils.GetMaps();
                SceneUtils.GetMapHeaders();
                SceneUtils.GetActors();
                EnemizerEarlyFixes(seedrng);
                ScanForMMRA(directory: "actors");
                InjectNewActors();

                var newSceneList = RomData.SceneList;
                newSceneList.RemoveAll(scene => SceneSkip.Contains(scene.SceneEnum) );

                // if using parallel, move biggest scenes to the front so that we dont get stuck waiting at the end for one big scene with multiple dead cores idle
                // LIFO, biggest scenes at the back of this list of big scenes
                // this should be all scenes that took > 500ms on Isghj's computer during alpha ~dec 2020
                //  this is old, should be re-evaluated with different code
                foreach (var sceneIndex in new int[]{ 1442, 1353, 1258, 1358, 1449, 1291, 1224,  1522, 1388, 1165, 1421, 1431, 1241, 1222, 1330, 1208, 1451, 1332, 1446, 1310 }){
                    var item = newSceneList.Find(scene => scene.File == sceneIndex);
                    newSceneList.Remove(item);
                    newSceneList.Insert(0, item);
                }
                //int seed = random.Next(); // order is up to the cpu scheduler, to keep these matching the seed, set them all to start at the same value
                int seed = randomized.Seed;

                var previousThreadPriority = Thread.CurrentThread.Priority;
                Thread.CurrentThread.Priority = ThreadPriority.Lowest; // do not SLAM

                Parallel.ForEach(newSceneList.AsParallel().AsOrdered(), scene =>
                //foreach (var scene in newSceneList) // sequential for debugging only
                // ( debugger is too stupid, if you catch a breakpoint and then tell it to move to a new location, it can catch on a _different_ thread)
                {
                    SwapSceneEnemies(scene, seed);
                });
                //}

                Thread.CurrentThread.Priority = previousThreadPriority;

                EnemizerLateFixes();
                //LowerEnemiesResourceLoad();
                if (ACTORSENABLED)
                {
                    DisableAllLocationRestrictions();  //experimental
                }

                // write the final time and version last
                using (StreamWriter sw = new StreamWriter(_outputSettings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.WriteLine("Enemizer final completion time: " + ((DateTime.Now).Subtract(enemizerStartTime).TotalMilliseconds).ToString() + "ms ");
                    sw.Write("Enemizer version: Isghj's Actorizer Test 73.9\n");
                    sw.Write("seed: [ " + seed + " ]");
                }
            }
            catch (Exception e)
            {
                string innerExceptions = e.InnerException != null ? e.InnerException.ToString() : "";
                throw new Exception("Enemizer failed for this seed, please try another seed.\n\n" + e.Message + "\n" + innerExceptions);
            }
        }

    }


    /// <summary>
    ///  keeping track of enemizer ram size limits
    /// </summary>

    public class BaseEnemiesCollection
    {
        // sum of overlay code per actortype in this collection
        public int OverlayRamSize;
        // sum of all enemy instances struct ram requirements
        public int ActorInstanceSum;
        // sum of object size
        public List<int> ObjectList;
        public int ObjectRamSize;
        public int DynaPolySize;
        public int DynaVertSize;
        public int[] objectSizes; //debug
        // list of enemies that were used to make this
        public List<Actor> oldActorList = null;

        public BaseEnemiesCollection(List<Actor> actorList, List<int> objList, Scene s)
        {
            /// values per day/night

            oldActorList = actorList;
            //var distinctActors = actorList.Select(act => act).DistinctBy(act => act);
            var distinctActors = actorList.DistinctBy(act => act);
            OverlayRamSize = distinctActors.Select(x => ActorUtils.GetOvlCodeRamSize(x.ActorId)).Sum();
            ActorInstanceSum = actorList.Select(act => act.ActorId)
                                        .Select(act => ActorUtils.GetOvlInstanceRamSize(act, Enemies.InjectedActors)).Sum();
            this.ObjectList = objList;
            this.objectSizes = objList.Select(x => ObjUtils.GetObjSize(x)).ToArray();
            this.ObjectRamSize = objectSizes.Sum();

            this.CalculateDefaultObjectUse(s);

            this.UpdateDynaLoad(actorList);
        }

        public void UpdateDynaLoad(List<Actor> actorList)
        {
            this.DynaPolySize = 0;
            this.DynaVertSize = 0;
            for (int act = 0; act < actorList.Count; act++)
            {
                var actor = actorList[act];
                this.DynaPolySize += actor.DynaLoad.poly;
                this.DynaVertSize += actor.DynaLoad.vert;
            }
        }

        public void CalculateDefaultObjectUse(Scene s)
        {
            // now that we know the hard object bank limits, we need ALL data
            // in addition to the scene objects, we need the objects that are always loaded
            this.ObjectList.Append(1);
            this.ObjectRamSize += 0x925E0; // gameplay_keep
            this.ObjectList.Append(0x11);
            this.ObjectRamSize += 0x1E250; // the biggest link form object (child)
            // scenes can have special scene objects, which arent included in actor objects
            if (s.SpecialObject == Scene.SceneSpecialObject.FieldKeep)
            {
                this.ObjectRamSize += 0x9290; // field keep object
                this.ObjectList.Append(0x2);
                /// I still dont know why epona sometimes spawns before the objects from scene are loaded, assumption its field
                if (s.SceneEnum != GameObjects.Scene.IkanaCanyon)
                {
                    this.ObjectRamSize += 0xE4F0; // epona
                    this.ObjectList.Append(0x7D);
                }
            }
            else if (s.SpecialObject == Scene.SceneSpecialObject.DungeonKeep)
            {
                this.ObjectRamSize += 0x23280;
                this.ObjectList.Append(0x3);
            }
        }

    }

    /// <summary>
    /// These classes down here exist to attempt to book-keep the ovl/struct/object sizes of all room/day/night combos
    /// </summary>
    public class MapEnemiesCollection
    {
        public BaseEnemiesCollection day = null;
        public BaseEnemiesCollection night = null;

        public MapEnemiesCollection(List<Actor> actorList, List<int> objList, Scene scene)
        {
            // split enemies into day and night, init two types
            int dayFlagMask = 0x2AA; // nigth is just shifted to the right by one

            var dayActors = actorList.FindAll(act => (act.GetTimeFlags() & dayFlagMask) > 0);
            this.day = new BaseEnemiesCollection(dayActors, objList, scene);
            var nightActors = actorList.FindAll(act => (act.GetTimeFlags() & (dayFlagMask >> 1)) > 0);
            this.night = new BaseEnemiesCollection(nightActors, objList, scene);
#if DEBUG
            //var missingElements = actorList.Except(dayActors).Except(nightActors).ToList();
            //Debug.Assert(missingElements.Count == 0);
#endif
        }
    }

    public class ActorsCollection
    {
        // per scene: per old and new: per room : per night and day: an object size, an actor inst size, and a actor code size
        // for each scene we need to check all of them, this is getting complicated

        public List<MapEnemiesCollection> oldMapList;
        public List<MapEnemiesCollection> newMapList;
        public Scene Scene;
        public int sceneObjectLimit;


        public ActorsCollection(Scene scene)
        {
            this.Scene = scene;
            this.oldMapList = new List<MapEnemiesCollection>();
            this.sceneObjectLimit = SceneUtils.GetSceneObjectBankSize(scene.SceneEnum);
            for (int i = 0; i < scene.Maps.Count; ++i)
            {
                var map = scene.Maps[i];
                this.oldMapList.Add(new MapEnemiesCollection(map.Actors, map.Objects, scene));
            }
        }

        // init for new replacements
        // this doesnt set actors anywhere tho, just objects, misnomer?
        //public void SetNewActors(Scene scene, List<ValueSwap> newObjChanges)
        public void SetNewActors(Scene scene, List<List<int>> newObjects)
        {
            // this is the slowest part of our bogo sort, we need to try speeding it up

            this.newMapList = new List<MapEnemiesCollection>();
            // I like foreach better but its waaaay slower
            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];

                //if (newObjChanges == null)
                //{
                //    throw new Exception("SetNewActors: empty object list");
                //}
                {
                    /*
                    var newObjList = map.Objects.ToList(); // copy
                    // probably a way to search for this with a lambda, can't think of it right now
                    for (int valueSwap = 0; valueSwap < newObjChanges.Count; ++valueSwap)
                    {
                        for (int o = 0; o < newObjList.Count; ++o)
                        {
                            // if old object matches out value swap, swap
                            if (map.Objects[o] == newObjChanges[valueSwap].OldV)
                            {
                                newObjList[o] = newObjChanges[valueSwap].NewV;
                            }
                        }
                    } // */
                    var newObjList = newObjects[m];
                    this.newMapList.Add(new MapEnemiesCollection(map.Actors, newObjList, scene));
                }
            }
        }

        public List<List<Actor>> GenerateShrinkableDynaList()
        {
            var shrinkableActorList = new List<List<Actor>>();

            for (int m = 0; m < this.newMapList.Count; m++)
            {
                var map = this.newMapList[m];

                // compare headroom to actual
                if (isDynaOverLoaded(map.day, this.oldMapList[m].day, m))
                {
                    buildDynaShrinkableListPerMap(shrinkableActorList, map.day.oldActorList);
                }
                if (isDynaOverLoaded(map.night, this.oldMapList[m].night, m))
                {
                    buildDynaShrinkableListPerMap(shrinkableActorList, map.night.oldActorList);
                }
            }

            return shrinkableActorList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool isDynaOverLoaded(BaseEnemiesCollection newCollection, BaseEnemiesCollection oldCollection, int mapIndex)
        {
            var dynaHeadroomAttr = SceneUtils.GetSceneDynaAttributes(this.Scene.SceneEnum, mapIndex);
            if (dynaHeadroomAttr != null)
            {
                var dayPolyDiff = newCollection.DynaPolySize - oldCollection.DynaPolySize;
                var dayVertDiff = newCollection.DynaVertSize - oldCollection.DynaVertSize;
                return (dayPolyDiff > dynaHeadroomAttr.Polygon || dayVertDiff > dynaHeadroomAttr.Verticies);
            }
            return false; // not considered dyna limited
        }

        private void buildDynaShrinkableListPerMap(List<List<Actor>> shrinkableActorList, List<Actor> actorList)
        {
            // per night or day

            var uniqueActors = new HashSet<int>();
            for (int a = 0; a < actorList.Count; a++) // I can use this old list right? it should be pointers to the same actors
            {
                var actor = actorList[a];
                // if actor is dyna
                if (actor.DynaLoad.poly > 0 && ((int) actor.OldActorEnum != actor.ActorId))
                    uniqueActors.Add(actor.ActorId);
            }
            foreach (var actorId in uniqueActors) // can't use for here because of hashset limitation
            {
                var grouped = actorList.FindAll(a => a.ActorId == actorId && ((int)a.OldActorEnum != a.ActorId));
                if (grouped.Count > 1)
                {
                    // test if this is an area that is dyna overloaded

                    // if so, add to list
                    shrinkableActorList.Add(grouped);
                }
            }
        }
        

        private bool testDynaSize()
        {

            for(int m = 0; m < oldMapList.Count; ++m)
            {
                if (isDynaOverLoaded(this.newMapList[m].day, this.oldMapList[m].day, m))
                    return false;
                if (isDynaOverLoaded(this.newMapList[m].night, this.oldMapList[m].night, m))
                    return false;
            }

            return true;
        }

        
        public bool isSizeAcceptable(StringBuilder log)
        {
            // is the overall size for all maps of night and day equal

            var objectTest = isObjectSizeAcceptable();
            if (objectTest > 0)
            {
                log.AppendLine($" ---- bogo REJECTED: objects are too big (by {objectTest})" +
                    $"\n [{string.Join(",", this.newMapList[0].day.ObjectList)}]" +
                    $"\n [{string.Join(",", this.newMapList[0].day.objectSizes)}");
                return false;
            }

            var dynatest = testDynaSize();
            if (dynatest == false)
            {
                log.AppendLine($" ---- bogo REJECTED: dyna actors are too big, even after trim");
                return false;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                // pos diff is smaller
                var sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].day, newMapList[map].day);
                if (sizeTest == false) {
                    log.AppendLine($" ---- bogo REJECTED: map {map} does not meed RAM requirements for DAY");
                    return false;
                }

                sizeTest = CompareRamRequirements(this.Scene, oldMapList[map].night, newMapList[map].night);
                if (sizeTest == false) {
                    log.AppendLine($" ---- bogo REJECTED: map {map} does not meed RAM requirements for NIGHT");
                    return false;
                }

                // compare dyna requirements

            }
            return true; // all of them passed size test
        }

        public bool CompareRamRequirements(Scene scene, BaseEnemiesCollection oldCollection, BaseEnemiesCollection newCollection)
        {
            var dayOvlDiff  = oldCollection.OverlayRamSize   - newCollection.OverlayRamSize;
            var dayInstDiff = oldCollection.ActorInstanceSum - newCollection.ActorInstanceSum;

            // if the new size is smaller than the old size we should be dandy, if not...
            if (dayOvlDiff + dayInstDiff <= -0x100)
            {
                if (scene.SceneEnum == GameObjects.Scene.IkanaCanyon
                    && (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x64FFF)) // trying a bit higher for ikana canyon
                {
                    return false;
                }

                // SCT is 0x4FF90
                else if (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x4FFFF) // need to find new safe values
                {
                    return false;
                }
                // I can't rule out halucination scrubs are or are not the issue, their skeleton->action is broken, that sounds like corrupted heap
                if (scene.SceneEnum == GameObjects.Scene.DekuPalace
                    && (newCollection.OverlayRamSize + newCollection.ActorInstanceSum > 0x22000)) // need to find new safe values
                {
                    return false;
                }
            }

            return true;
        }

        public int isObjectSizeAcceptable(List<int> objects = null)
        {
            /// checks if the object load of the current object list will blow out the object space

            for (int map = 0; map < oldMapList.Count; ++map)
            {
                /* if (newMapList[map].day.ObjectRamSize > sceneObjectLimit || newMapList[map].night.ObjectRamSize > sceneObjectLimit)
                {
                    return false;
                }// */
                //var oldObjectSize = this.oldMapList[map].day.ObjectList.Sum();//this.ObjectList.Sum();
                int newObjectSize;
                if (objects != null)
                {
                    newObjectSize = objects.Sum();
                }
                else
                {
                    newObjectSize = this.newMapList[map].day.ObjectRamSize;
                }

                if (newObjectSize > this.sceneObjectLimit)
                {
                     return (newObjectSize - this.sceneObjectLimit);
                }
            }

            return 0;
        }

        public string isDynaSizeAcceptable()
        {
            //return "acceptable"; // temp testing
            for (int map = 0; map < oldMapList.Count; ++map)
            {
                // pull dynaheadroom for the scene, if there isnt one continue
                var dynaHeadroomAttr = SceneUtils.GetSceneDynaAttributes(this.Scene.SceneEnum, map);
                if (dynaHeadroomAttr == null) continue; // this room has none

                // compare headroom to actual
                var dayPolyDiff = this.newMapList[map].day.DynaPolySize - this.oldMapList[map].day.DynaPolySize;
                if (dayPolyDiff >  dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day poly: [{dayPolyDiff}]";
                }

                var dayVertDiff = this.newMapList[map].day.DynaVertSize - this.oldMapList[map].day.DynaVertSize;
                if (dayVertDiff > dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day vert: [{dayVertDiff}]";
                }

                var nightPolyDiff = this.newMapList[map].night.DynaPolySize - this.oldMapList[map].night.DynaPolySize;
                if (nightPolyDiff > dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day poly: [{nightPolyDiff}]";
                }

                var nightVertDiff = this.newMapList[map].night.DynaVertSize - this.oldMapList[map].night.DynaVertSize;
                if (nightVertDiff > dynaHeadroomAttr.Polygon)
                {
                    return $"map [{map}] day vert: [{nightVertDiff}]";
                }
            }

            return "acceptable";
        }


        // print to log function
        public void PrintAllMapRamObjectOutput(StringBuilder log)
        {
            void PrintCombineRatioNewOld(string text, int newv, int oldv){
                log.AppendLine(text + " ratio: [" + ((float) newv / (float) oldv).ToString("F4")
                    + "] newsize: [" + newv.ToString("X6") + "] oldsize: [" + oldv.ToString("X6") + "]");
            }
            void PrintCombineDeltaNewOld(string text, int newv, int oldv)
            {
                log.AppendLine(text + " delta: [" + (newv - oldv).ToString()
                    + "] newsize: [" + newv.ToString("X6") + "] oldsize: [" + oldv.ToString("X6") + "]");
            }



            if (newMapList == null)
            {
                log.AppendLine(" ERROR: New list was dead!");
                return;
            }

            for (int map = 0; map < oldMapList.Count; ++map) // per map
            {
                var newDTotal = newMapList[map].day.OverlayRamSize + newMapList[map].day.ActorInstanceSum;
                var oldDTotal = oldMapList[map].day.OverlayRamSize + oldMapList[map].day.ActorInstanceSum;
                var newNTotal = newMapList[map].night.OverlayRamSize + newMapList[map].night.ActorInstanceSum;
                var oldNTotal = oldMapList[map].night.OverlayRamSize + oldMapList[map].night.ActorInstanceSum;

                // PRINT EVERYTHING
                //if (newDTotal - oldDTotal + newNTotal - oldNTotal == 0) continue; // map was untouched, dont print

                log.AppendLine(" ======( Map " + map.ToString("X2") + " )======");

                PrintCombineRatioNewOld("  day:    overlay ", newMapList[map].day.OverlayRamSize,   oldMapList[map].day.OverlayRamSize);
                PrintCombineRatioNewOld("  day:    struct  ", newMapList[map].day.ActorInstanceSum, oldMapList[map].day.ActorInstanceSum);
                PrintCombineRatioNewOld("  day:    total  =", newDTotal, oldDTotal);

                PrintCombineRatioNewOld("  night:  overlay ", newMapList[map].night.OverlayRamSize,   oldMapList[map].night.OverlayRamSize);
                PrintCombineRatioNewOld("  night:  struct  ", newMapList[map].night.ActorInstanceSum, oldMapList[map].night.ActorInstanceSum);
                PrintCombineRatioNewOld("  night:  total  =", newNTotal, oldNTotal);

                log.AppendLine($"  ------------------------------------------------------ ");

                PrintCombineRatioNewOld("  day:    object  ", newMapList[map].day.ObjectRamSize, oldMapList[map].day.ObjectRamSize);
                PrintCombineRatioNewOld("  night:  object  ", newMapList[map].night.ObjectRamSize, oldMapList[map].night.ObjectRamSize);


                // print map objects size
                var hexString = "";
                for(int i = 0; i < newMapList[map].day.objectSizes.Length; i++)
                {
                    hexString += "0x" + newMapList[map].day.objectSizes[i].ToString("X") + " ";
                }
                var size = newMapList[map].day.objectSizes.Sum().ToString("X");
                var allSize = newMapList[map].day.ObjectRamSize.ToString("X");
                log.AppendLine($"   object sizes: [ {hexString}]");
                log.AppendLine($"    sum: [0x{size}] allsize: [0x{allSize}]");
                log.AppendLine($"  ------------------------------------------------------ ");

                PrintCombineDeltaNewOld("  day:    dyna poly  ", newMapList[map].day.DynaPolySize, oldMapList[map].day.DynaPolySize);
                PrintCombineDeltaNewOld("  day:    dyna vert  ", newMapList[map].day.DynaVertSize, oldMapList[map].day.DynaVertSize);
                PrintCombineDeltaNewOld("  night:  dyna poly  ", newMapList[map].night.DynaPolySize, oldMapList[map].night.DynaPolySize);
                PrintCombineDeltaNewOld("  night:  dyna vert  ", newMapList[map].night.DynaVertSize, oldMapList[map].night.DynaVertSize);

                log.AppendLine($" ------------------------------------------------- ");
            }
        } // end PrintAllMapRamObjectOutput
    } // end actorsCollection
}
