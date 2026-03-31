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
using System.Collections.Immutable;

// dotnet 4.5 req
using System.Runtime.CompilerServices;
using MMR.Randomizer.Enemizer;
//using MMR.Randomizer.Attributes;

// todo rename this actorutils.cs and move to MMR.Randomizer/Utils/


namespace MMR.Randomizer
{
    [System.Diagnostics.DebuggerDisplay("{OldV} -> {NewV}")]
    public class ValueSwap
    {
        /// <summary>
        ///  This class exists to keep track of objects we swap in the object list
        /// </summary>
        public int OldV;
        public int NewV;
        public int ChosenV; // Copy of NewV, first pass result, but we might change NewV to something else if duplicate

        public ValueSwap(){ }

        public ValueSwap(int oldV, int newV)
        {
            this.OldV = oldV;
            this.NewV = this.ChosenV = newV;
        }

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
        public (int poly, int vert) DynaLoad = (-1, -1); // Does the actor use the dyna system, which is a limited buffer size we cannot overflow

        // if all new actor, we meed to know where the old vram start was when we shift VRAM for the actor
        public uint buildVramStart = 0;
        // init vars are located somewhere in .data, we want to know where exactly because its hard coded in overlay table
        public uint initVarsLocation = 0;

        // TODO make this match regular actors??
        public List<int> groundVariants = new List<int>();
        public List<int> flyingVariants = new List<int>();
        public List<int> waterVariants = new List<int>();
        public List<int> waterTopVariants = new List<int>();
        public List<int> waterBottomVariants = new List<int>();
        public List<int> perchingVariants = new List<int>();
        public List<int> wallVariants = new List<int>();
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
        public const int SMALLEST_OBJ = 0xF3; // 0x10 size, smallest vanilla object I could find

        public static List<GameObjects.Actor> VanillaEnemyList { get; set; }
        public static List<Actor> ReplacementCandidateList { get; set; }
        public static List<Actor> FreeCandidateList { get; set; }
        public static List<Actor> FreeOnlyCandidateList { get; set; } // not worthy by themselves, only if object was already selected
        private static Mutex _LogMutex = new Mutex();
        private static bool ACTORSENABLED = true; 
        private static Random _seedRNG;
        private static Models.RandomizedResult _randomized;
        private static OutputSettings _outputSettings;
        private static CosmeticSettings _cosmeticSettings;
        private static StringBuilder _syncedLog;

        // these have to be separate from Actor Enum for now beacuse they are for special objects, not regular types, can't mix
        static int[] clayPotDungeonVariants = {
            0xB, // multiple
            0x1E, 0x5, // swamp spiderhouse spider pots
            0x4C02, 0x4E02, 0x5002, 0x5202, // wft
            0x5C0E, 0x601E, 0x621E, 0x4C0E, 0x660E, 0x741E, 0x5A0A, // ospiderhouse
            0x761E, 0x001A, 0x400A, 0x0186, 0x018A, 0x680A, 0x6E0A, 0x700A, 0x720E, // ospiderhouse
            0x5A1E, 0x5C1E, 0x400B, 0x420A, 0x521F, 0x440B, 0x4602, 0x561E,         // pirate bay rooms
            0x5013, 0x581E, 0x480B, 0x4A1E, 0x101F, 0x1203, 0x480B, 0x541E, 0x4E0B, // pirate bay rooms
            0x4015, 0x4215, 0x4415, 0x4615, 0x4815, // botw (pots with checks)
            0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, 0xFE3F, // botw (regular pots)
            0x0186, 0x0187, 0x018A, 0x018C, 0x018A, 0x440A, 0x460A, 0x480A, 0x440B, 0x018B, 0x000F, 0x4210, 0x0015, 0x001E, // istt
            0x018A, 0x000F, 0x3811, 0x0015, 0x001E, 0x4210, 0x000A, 0x001E, 0x4C02, 0x4E02, 0x5002, 0x5202, // wft
            0xC00B, 0xC21E, 0xC40E, 0xFE0E, 0xFC0B, 0xFA1E, 0xF81E, 0xF81E, 0xF60E, 0xF410, // secret shrine,
            0xFE0F, 0xFE0B, 0xFE0E, 0xFE03 // non-vanilla
        };

        // params: 0x3 is type, 0,2,3 are field grass (1 is tall re-growing grass that requires object)
        //  type 0: 0x7F00 is item (random) collectable from table,
        //    0xC000 just disables item drop??
        // the 0x10 param drops a bugs actor on the ground too
        // type 2 grass drops items on 0xFC instead, and its not random from a table but guarenteed?
        static int[] tallGrassFieldObjectVariants = {
            0x0,    // termina field mixed drop table
            0x0500, // empty dt
            0x0600, // hearts and flexible dt
            0x0700, // all hearts dt
            0x0800, // quarter chance small rup dt
            0x0C00, // half chance magic dt
            0x0D00, // all magic dt
            0x0E00, // sticks nuts flexible dt
            0x0010, // above but with bugs
            0x0610,
            0x0E10,
            // non-vanilla added for variety
            0x0A10, // magic and arrows
            0x0110, // mixed swamp bushes
            0x0210, // mountain village drop table
            0x0310, // unused drop table
            0x0A10, // magic and arrows
            0x0B10, // bombs
            0x0F10, // almost full mixed 
        };

        public static void PrepareEnemyLists()
        {
            // list of actor slots to use
            VanillaEnemyList = Enum.GetValues(typeof(GameObjects.Actor)).Cast<GameObjects.Actor>()
                            .Where(act => act.ObjectIndex() > 3
                                && (act.IsEnemyRandomized() || (ACTORSENABLED && act.IsActorRandomized()))) // both
                            .ToList();

            var EnemiesOnly = VanillaEnemyList
                            .Where(act => act.IsEnemyRandomized())
                            .ToList(); //*/

            // special request for enemizer: do not randomize bigocto
            if ( ! ACTORSENABLED)
            {
                VanillaEnemyList.Remove(GameObjects.Actor.BigOcto);
            }

            // list of replacement actors we can use to replace with
            // for now they are the same, in the future players will control how they load
            ReplacementCandidateList = new List<Actor>();
            //foreach (var actor in EnemiesOnly) // for use with enemies only
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
            FreeCandidateList = freeCandidates.Select(act => new Actor(act, InjectedActors.Find(i => i.ActorId == (int)act))).ToList();


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

        private static void CheckForHardToFindBugsPre(SceneEnemizerData thisSceneData)
        {
            /// attempt to catch some odd issues that are rarely reproducable

            var settingsPrompt = "If you get this error message, please submit to Isghj: \n Settings file, and this seed:";

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.PiratesFortressRooms && thisSceneData.Actors.Any(a => a.OldActorEnum == GameObjects.Actor.PirateColonel))
            {
                var pirates = thisSceneData.Actors.FindAll(act => act.OldActorEnum == GameObjects.Actor.PirateColonel);
                throw new Exception("Pirates should not be randomized.\n" + settingsPrompt); // jan 2026
            }
            
        }

        private static void CheckForHardToFindBugsPost(SceneEnemizerData thisSceneData)
        {
            /// attempt to catch some odd issues that are rarely reproducable

            var settingsPrompt = "If you get this error message, please submit to Isghj: \n Settings file, and this seed:";

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.BeneathGraveyard)
            {
                var goldSkulltula = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.GoldSkulltula &&  (act.Variants[0] & 0xFF00) < 0xFF00);
                if (goldSkulltula.Count > 0)
                    throw new Exception("Skulls should never be pathing in grave.\n" + settingsPrompt); // jan 2026
            }

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.DoggyRacetrack)
            {
                var pots = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.RegularIceBlock && act.OldActorEnum == GameObjects.Actor.ClayPot );
                if (pots.Count > 0)
                {
                    throw new Exception("Pots should not be ice.\n" + settingsPrompt); // jan 2026

                }
            }

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

        // todo: this function is big and complex enough to break apart
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
                mapActor.SortedVariants = Actor.BuildVariantList(matchingEnemy);
                //mapActor.Blockable = mapActor.ActorEnum.IsBlockable(scene.SceneEnum, mapActor.RoomActorIndex);
                mapActor.UpdateBlockable(scene.SceneEnum);
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
                        var importantItem = ObjectIsCheckBlocked(scene.SceneEnum, targetActor.ActorEnum, targetActor.OldVariant);
                        if (importantItem != null)
                            if (importantItem != null)
                            {
                                thisSceneData.Log.AppendLine($" tallgrass r[{targetActor.Room}]v[{targetActor.OldVariant}]" +
                                    $" replacement blocked by [{(int)importantItem}]");
                                return false;
                            }

                        FixActorLastSecond(targetActor, targetActor.OldActorEnum, mapIndex, actorIndex);
                        targetActor.Variants.AddRange(tallGrassFieldObjectVariants);
                        targetActor.SortedVariants[(int)GameObjects.ActorType.Ground] = targetActor.Variants; // have to update the types for variant compatiblity later
                        return true;
                    }
                    else
                    {
                        log.Append($" in scene [{scene.SceneEnum}][{mapIndex}]" +
                                   $" actor was skipped over: [0x{targetActor.OldVariant.ToString("X4")}][{targetActor.ActorEnum}]\n");

                    }
                }
                if (thisSceneData.Scene.SpecialObject == Scene.SceneSpecialObject.DungeonKeep
                 && targetActor.OldActorEnum == GameObjects.Actor.ClayPot)
                {

                    // special case: claypot is multi object: one uses dungeon keep to hold its assets
                    //  until we have multi-object code, this needs a special case or rando ignores it
                    if (clayPotDungeonVariants.Contains(targetActor.OldVariant))
                    {
                        var importantItem = ObjectIsCheckBlocked(scene.SceneEnum, targetActor.ActorEnum, targetActor.OldVariant);
                        if (importantItem != null)
                        {
                            thisSceneData.Log.AppendLine($" claypot r[{targetActor.Room}]v[{targetActor.OldVariant}]" +
                                $"  replacement blocked by [{(int)importantItem}]");
                            return false;
                        }

                        FixActorLastSecond(targetActor, targetActor.OldActorEnum, mapIndex, actorIndex);
                        targetActor.Variants.AddRange(clayPotDungeonVariants);
                        targetActor.SortedVariants[(int)GameObjects.ActorType.Ground] = targetActor.Variants; // have to update the types for variant compatiblity later
                        return true;
                    }
                    else
                    {
                        log.Append($" in scene [{scene.SceneEnum}][{mapIndex}]" +
                                   $" actor was skipped over: [0x{targetActor.OldVariant.ToString("X4")}][{targetActor.ActorEnum}]\n");

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
                    var matchingEnemy = VanillaEnemyList.Find(act => act == mapActor.OldActorEnum);
                    if (matchingEnemy > 0)
                    {
                        // note: injected actor data is added later, this happens before injection
                        var listOfAcceptableVariants = matchingEnemy.GenerateVariantsFromEnum();

                        // TODO: check if the specific actor can be randomized, required before continue:
                        // actor separation, scene reconstruction, object list extension,  

                        if (matchingEnemy.ScenesRandomizationExcluded().Contains(scene.SceneEnum))
                            continue;

                        if (SpecialMultiObjectCases(mapActor, mapIndex, actorIndex))
                        {
                            sceneObjectlessActors.Add(mapActor);
                            continue;
                        }

                        var itemRestriction = ObjectIsCheckBlocked(scene.SceneEnum, mapActor.ActorEnum, mapActor.OldVariant);
                        if (itemRestriction != null )
                        {

                            #if DEBUG
                            var itemText = $"[{ itemRestriction }]";
                            #else
                            var itemText = $"[{ (int) itemRestriction}]"; // hiding the item in case players need to glance the log they don't get to see the item by name
                            #endif

                            log.AppendLine($" in scene (O!) [{scene.SceneEnum}]m[{mapIndex}]r[{mapActor.RoomActorIndex}]v[{mapActor.OldVariant.ToString("X4")}]" +
                                $" actor:[0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}] removal blocked by item " + itemText);
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
                    else // non-object based actors, test if standalone actor
                    {
                        // regular butterfly is only on moon
                        GameObjects.Actor[] commonScoopableActors = new GameObjects.Actor[] {
                                    GameObjects.Actor.MushroomCloud, GameObjects.Actor.BugsFishButterfly, GameObjects.Actor.Fish
                        };

                        var matchingStandaloneActor = FreeCandidateList.Find(act => act.ActorEnum == mapActor.OldActorEnum);
                        if (matchingStandaloneActor != null)
                        {


                            var sceneRestrictions = mapActor.OldActorEnum.GetAttribute<ForbidFromSceneAttribute>();
                            if (sceneRestrictions != null && sceneRestrictions.ScenesExcluded.Contains(thisSceneData.Scene.SceneEnum))
                                continue; // not valid to consider this actor

                            var itemRestriction = ObjectIsCheckBlocked(scene.SceneEnum, mapActor.ActorEnum, mapActor.OldVariant);
                            var chanceOfRandomization = (_randomized.Settings.LogicMode == LogicMode.NoLogic) ? (90) : (60);
                            var randomRoll = thisSceneData.RNG.Next(100);
                            // if common scoopable actor, some are allowed but not all, for now lets make it random
                            if (itemRestriction != null && (commonScoopableActors.Contains(mapActor.OldActorEnum)
                                && itemRestriction.ToString().Contains("BottleCatch")
                                && randomRoll < chanceOfRandomization))
                            {
                                #if DEBUG
                                var itemText = $"[{ itemRestriction.ToString() }]";
                                #else
                                var itemText = $"[{ (int) itemRestriction}]";
                                #endif
                                log.AppendLine($" in scene [{scene.SceneEnum}]m[{mapIndex}]r[{mapActor.RoomActorIndex}]" +
                                    $" common scoopable actor: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}] skipped the restriction: " +
                                    itemText);
                            }
                            else if (itemRestriction != null)
                            {

                                #if DEBUG
                                var itemText = $"blocked by item [{ itemRestriction }]";
                                #else
                                var itemText = $"blocked by item [{ (int) itemRestriction}]";
                                #endif

                                log.AppendLine($" in scene [{scene.SceneEnum}]m[{mapIndex}]r[{mapActor.RoomActorIndex}]v[{mapActor.OldVariant.ToString("X4")}]" +
                                    $" actor: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}] was " + itemText);
                                continue;
                            }

                            if ( ! matchingStandaloneActor.SortedVariants.Any(subArray => subArray.Contains(mapActor.OldVariant)))
                            {
                                log.AppendLine($" in scene [{scene.SceneEnum}][{mapIndex}] standalone was skipped over: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}]");
                                continue; // non valid
                            }

                            var replacementChance = matchingStandaloneActor.GetRemovalChance();
                            if (randomRoll > replacementChance)
                            {
                                log.AppendLine($" in scene [{scene.SceneEnum}][{mapIndex}] standalone was randomly ignored: [0x{mapActor.OldVariant.ToString("X4")}][{mapActor.ActorEnum}]");
                                continue; // blocked by roll
                            }

                            FixActorLastSecond(mapActor, matchingStandaloneActor.ActorEnum, mapIndex, actorIndex);

                            sceneObjectlessActors.Add(mapActor);
                        }
                    }
                }

            }
            thisSceneData.Actors = sceneEnemyList;
            thisSceneData.Actors.AddRange(sceneObjectlessActors); // might want to rethink this eventually
            thisSceneData.StandaloneActors = sceneObjectlessActors;
        }

        


        // todo move to actorutils
        // TODO rename to ACTOR is check blocked, as we will soon need to do this for actors not whole actor objects
        // for now its just the objectlessactors, checkrestricted
        public static GameObjects.Item? ObjectIsCheckBlocked(GameObjects.Scene sceneEnum, GameObjects.Actor testActor, int variant = -1)
        {
            /// checks if randomizing the actor would interfere with getting access to a check
            /// and then checks if the item is junk, before allowing randimization
            /// tags: itemblocked, item restricted, check restricted
            /// ISSUE: if this is called from object culling, variant can break for us instead of help us
            const GameObjects.Scene ANYSCENE = (GameObjects.Scene)GameObjects.ActorConst.ANY_SCENE;

            var checkRestrictedAttr = testActor.GetAttributes<CheckRestrictedAttribute>();
            if (checkRestrictedAttr != null && checkRestrictedAttr.Count() > 0) // actor has check restrictions
            {
                var reducedList = checkRestrictedAttr.ToList().FindAll(attr => attr.Scene == sceneEnum || (int)attr.Scene == -1);

                foreach (var restriction in reducedList) // can have multiple rules
                {
                    if (restriction.Scene != ANYSCENE && restriction.Scene != sceneEnum) continue;

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
                        var itemIsNotJunk = !JunkDetection.IsActorizerJunk(itemInCheck);
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
                switch (sceneEnum)
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
                if (!JunkDetection.IsActorizerJunk(map1))
                {
                    return map1; // we need to keep this tingle because their items are actual not-junk
                }
                if (!JunkDetection.IsActorizerJunk(map2))
                {
                    return map2; // we need to keep this tingle because their items are actual not-junk
                }
                // if heartpiece on picture is required, one of them has to remain regardless of their items
                if (strawPulled && !JunkDetection.IsActorizerCheckJunk(GameObjects.Item.HeartPiecePictobox))
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
                if (_randomized.Settings.LogicMode == LogicMode.NoLogic || _randomized.ImportantLocations != null
                      && _randomized.ImportantLocations.Union(checksPostBoxLeadsTo).Count() > 0)
                {
                    // if we need a mailbox, keep one
                    var shortStrawPostbox = _randomized.Seed % 3;
                    GameObjects.Scene[] postboxScenes = { GameObjects.Scene.NorthClockTown, GameObjects.Scene.SouthClockTown, GameObjects.Scene.EastClockTown };
                    if (postboxScenes[shortStrawPostbox] == sceneEnum)
                    {
                        return GameObjects.Item.MaskPostmanHat; // to symbolize what is happening only in the debug output
                    }

                }// else: randomize all
            }
            if (_randomized.Settings.FreeScarecrow == false && testActor == GameObjects.Actor.Scarecrow &&
                (sceneEnum == GameObjects.Scene.TradingPost || sceneEnum == GameObjects.Scene.AstralObservatory))
            {
                // only two scenes, one is even one is odd, lets use the seed and the scene ID
                int sceneChosen = ((int)sceneEnum + _randomized.Seed) & 1;
                if (sceneChosen == 1)
                {
                    return GameObjects.Item.SongOath; // there is no scarecrow song to use as a value, will just use this
                }
            }

            // MMR now offers hints at more actors if we add additional win conditions, if those conditions are active we need to avoid randomizing actors
            if (_randomized.Settings.VictoryMode.HasFlag(VictoryMode.SkullTokens))
            {
                if (sceneEnum == GameObjects.Scene.OceanSpiderHouse && testActor == GameObjects.Actor.Seth1)
                {
                    return GameObjects.Item.OtherKillMajora;
                }
                if (sceneEnum == GameObjects.Scene.SwampSpiderHouse && testActor == GameObjects.Actor.CursedSpiderMan)
                {
                    return GameObjects.Item.OtherKillMajora;
                }
            }
            if (_randomized.Settings.VictoryMode.HasFlag(VictoryMode.Fairies))
            {
                if (sceneEnum == GameObjects.Scene.FairyFountain && testActor == GameObjects.Actor.GreatFairy)
                {
                    return GameObjects.Item.OtherKillMajora;
                }
            }
            // todo: add happy mask salesman


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

                        var importantItem = ObjectIsCheckBlocked(scene.SceneEnum, matchingEnum);
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

        private static void EnemizerEarlyFixes()
        {
            /// Changes before actor/enemy randomization, Itemizer is already done

            BlockBabyGoronIfNoSFXRando();

            // modify actor to work
            EnablePoFusenAnywhere();
            FixScarecrowTalk();
            ShortenChickenPatience();
            ModifyFireflyKeeseForPerching();
            //FixSeth2();
            AllowGuruGuruOutside();
            FixSilverIshi();
            FixBabaAndDragonflyShadows();
            FixCuccoChicks();
            //FixStreamSfxVolume();
            FixBomberKidsGameFinishWarp();
            FixArmosSpawnPos();
            ExtendGrottoDirectIndexByte();
            FixInjuredKoume();

            SceneModification.ModifyScenesForEnemizer(_randomized, ACTORSENABLED, _seedRNG, _syncedLog);

            EnableAllCreditsCutScenes();

            EnableAllFormItems(); // let players use items in odd places to fight new nemanies

        }

        public static void EnemizerItemFixes()
        {
            // if itemizer changes something, we need to test first before actors are shuffled

            // cows in the cow grotto are changed to entorch by zoey 
            var grottosScene = RomData.SceneList.Find(scene => scene.File == GameObjects.Scene.Grottos.FileID());
            var cowGrotto = grottosScene.Maps[10];
            var cow1 = cowGrotto.Actors[3];
            var cow2 = cowGrotto.Actors[7];

            if (cow1.ActorEnum == GameObjects.Actor.GrottoChest)
            {
                try { 
                    // manually check if restrictions apply
                    if (JunkDetection.IsActorizerCheckJunk(GameObjects.Item.ItemTerminaGrottoCowMilk1) && JunkDetection.IsActorizerJunk(GameObjects.Item.ItemTerminaGrottoCowMilk2)
                        && JunkDetection.IsActorizerJunk(GameObjects.Item.ItemCoastGrottoCowMilk1) && JunkDetection.IsActorizerJunk(GameObjects.Item.ItemCoastGrottoCowMilk2))
                    {
                        cow1.ChangeActor(GameObjects.Actor.Cow, vars: 0, modifyOld: true);
                        cow2.ChangeActor(GameObjects.Actor.Cow, vars: 0, modifyOld: true);
                    }

                }catch(Exception e){
                    throw new Exception("COW SANITY EVENT:\n" + e.Message);
                }
            }
        }

        public static void EnemizerLateFixes()
        {
            /// changes after randomization, actors objects already written, at this point we can detect IF an actor was randomized
            /// ie: was not randomized because of item, or chance of not randomizing

            FixKafeiPlacements();
            SceneModification.MoveActorsIfRandomized(ACTORSENABLED);

            // if eyegore in the temples is removed, the door behind will not open
            var isttScene = RomData.SceneList.Find(scene => scene.SceneEnum == GameObjects.Scene.InvertedStoneTowerTemple);
            var egol = isttScene.Maps[1].Actors[3];
            if (egol.ActorEnum != GameObjects.Actor.Eyegore)
            {
                // dnf, just got burried in todo list
            }
           
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
        
        public static void FixArmosSpawnPos()
        {
            /// for some reason armos changes its home and world position based on y rotation in init
            //
            // this->actor.home.pos.x -= 9.0f * Math_SinS(this->actor.shape.rot.y);
            // this->actor.home.pos.z -= 9.0f * Math_CosS(this->actor.shape.rot.y);
            // this->actor.world.pos.x = this->actor.home.pos.x;
            // this->actor.world.pos.z = this->actor.home.pos.z;
            /// and it makes no sense, breaks strayfairies because they need to be at the same spot, removing

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

        public static void DisableAllLocationRestrictions()
        {
            /// because, sometimes, enemies can be placed inside, all rules of society have shattered

            // 19 = top of clock tower: if you can soar out its a "problem" (shrug)
            // 54 = sword school: hookshot can lock the player
            var sceneSkipList = new List<int> { (int)GameObjects.Scene.ClockTowerRoof, (int)GameObjects.Scene.SwordsmansSchool };

            var witchShopScene = RomData.SceneList.Find(s => s.SceneEnum == GameObjects.Scene.PotionShop);
            if (witchShopScene.Maps[0].Actors[0].ActorEnum == GameObjects.Actor.ShopKeepKotake)
            {
                // if the player gives bottle as FD it can overwrite ocarina
                sceneSkipList.Add((int)GameObjects.Scene.PotionShop);
            }

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


        private static void SwitchSkullfishBackToEncount1(SceneEnemizerData thisSceneData)
        {
            /// in order for the ocean skullfish to spawn over and over and harrass the player
            /// we have to use a different spawning actor, EnEncount1
            /// here, we take a special case of parameters I made up, and switch it back
            /// also, skullfish parameters use rotation heavily, modify them here too

            // TODO why is this a last second change? this could have been a pre-fix in Earlyfixes I think? re-examine

            if (!thisSceneData.Objects.Contains((int)GameObjects.Actor.SkullFish.ObjectIndex())) return;

            for (int a = 0; a < thisSceneData.Actors.Count; a++)
            {
                var actor = thisSceneData.Actors[a];

                if (actor.ActorEnum == GameObjects.Actor.SkullFish)
                {
                    var validDropIds = new int[] {
                        0x3, 0x11, 0x7, // stone tower temple
                        0, 0xE, 0x7, 0xD, // gbt
                        0x1, // encount in gbt
                        // cape is always drop nothing, lame
                    };
                    var nextDropId = validDropIds[thisSceneData.RNG.Next(validDropIds.Length)];

                    if (actor.Variants[0] == 0xFFFF) // encount swap
                    {
                        actor.ChangeActor(GameObjects.Actor.En_Encount1, vars: 0x105E);
                        actor.ChangeXRotation(0x38); // x rotation is the rate at which they re-spawn, 0x28 is the fast one near the cape, 0x6Xsomething other places
                        actor.ChangeYRotation((nextDropId - 1));    // y rotation is item drop pool index
                        actor.ChangeZRotation(0x32); // z rotation is agro range, cape is 0x32

                    }
                    else
                    {
                        // z rotation for non encount types (PR2 type 2) is drop table
                        // reminder, its (z-rot -1) to get index, as zero is ignore case
                        actor.ChangeZRotation(nextDropId - 1);
                    }

                    // in order for an actor to get the rotation raw, instead of converted, we need to set a flag for each parameter
                    actor.ActorIdFlags |= 0x8000 | 0x4000 | 0x2000;
                }
            }
        }


        private static void ExtendGrottoDirectIndexByte()
        {
            /// in MM the top nibble of the grotto variable is never used (0xF000)
            /// but in the vanilla code it be detected and used as a grotto warp index of the static grottos entrances array (-1)
            ///  MM normally uses the z rotation instead to index warp, but we can use either or
            /// however, only the 3 lower bits of this nibble are used, the code ANDS with 7
            /// why? the fourth bit isn't ever used by any grotto, and looking at the code shows it is never used
            /// so here, we set the ANDI 7 to F instead, allowing us extended access to the entrance array
            // TODO and by 0xF800 and shift less to get more range, requires re-writting all variants in actor list
            var grotholeFID = GameObjects.Actor.GrottoHole.FileListIndex();
            RomUtils.CheckCompressed(grotholeFID);
            RomData.MMFileList[grotholeFID].Data[0x2FF] = 0xF; // ANDI 0x7 -> ANDI 0xF
        }

        // grotto entrance list is extra, lets add some
   
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

            RomUtils.CheckCompressed(GameObjects.Actor.FriendlyCucco.FileListIndex());
            var niwData = RomData.MMFileList[GameObjects.Actor.FriendlyCucco.FileListIndex()].Data;
            // both of these changes made in EnNiw_Init
            ReadWriteUtils.Arr_WriteU32(niwData, 0x24A8, 0x40000000); // 9.9 -> 2 in f32 (in rodata)
            ReadWriteUtils.Arr_WriteU16(niwData, 0x156, 0x3F80); // 10 -> 1 in f32 (first short only as literal hardcoded)
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


        public static void FixThornTraps()
        {
            // dnf: never went back to thorn enemies, was waiting for decomp
        }

        public static void FixSeth2()
        {
            /// seth 2, the guy waving his arms in the termina field telescope, like oot spiderhouse
            /// his init code checks for a value, and does not spawn if the value is different than expected
            if ( ! ReplacementListContains(GameObjects.Actor.Seth2)) return;

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

        private static void FixInjuredKoume()
        {
            /// Injured koume in the woods of mystery, her code checks if she is in the woods of mystery and self culls
            if ( ! VanillaEnemyList.Contains(GameObjects.Actor.InjuredKoume)) return;

            var koumeFID = GameObjects.Actor.InjuredKoume.FileListIndex();
            RomUtils.CheckCompressed(koumeFID);
            var koumeData = RomData.MMFileList[koumeFID].Data;
            // the code check is entrance, and then moves to kill, so just remove the branch
            ReadWriteUtils.Arr_WriteU32(koumeData, 0x2D38, 0x00000000); // BNE to actor kill -> NOP

            // we should also check if 
        }



        public static void RandomizePerGrottoActor(SceneEnemizerData thisSceneData)
        {
            if (thisSceneData.Scene.SceneEnum != GameObjects.Scene.Grottos) return;

            /// the generic grotto in MM is reused 13 times, where the only difference is the chest
            /// the chest uses code to look up which grotto it is in to change its contents, we can do this with actors too
            ///  I created a new custom actor, because entorch is already overloaded
            ///  and I also expanded the generic grotto room object list to have 14 objects (the original 2, box and dekubaba)

            // TODO if grotto scene or grotto actor is missing, abort

            // randomly select 13 different ground/flying/ceiling? actors
            var newObjectList = new List<int>[13];
            for (int o = 0; o < 13; o++)
            {

            }
              // check to make sure all four of those objects are small enough to fit
              // custom code to control which grottos are limited by actor placement
              // custom code to turn freezard in the front OFF or turn around, or move back? also dino


            // update array of actors in the custom grotto actor

            // update objects in the thing
            

            // somehow print the actor results for our randomization to log
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
                if (box.Variants[0] > 4) // non-vanilla is greater than 4, but vanilla requires specific numbers
                    box.Variants[0] &= 0x4; // revert to vanilla after choosing
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

        private static void FixNewGrottoZRotation(SceneEnemizerData thisSceneData)
        {
            /// grottos that have an upper byte of 0x0 with a type of 0x000 or 0x200 are z rotation reading types,
            /// because they use the lower byte for item/chest characteristic (en_torch)
            /// so we have to update the zrotation to match

            var allGrottos = thisSceneData.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.GrottoHole);
            for(int a = 0; a < allGrottos.Count(); a++)
            {
                var actor = allGrottos[a];
                var variant = actor.Variants[0];
                var type = (variant & 0x300) >> 8;
                var upperAddress = variant & 0xF000;
                if (upperAddress == 0 && (type == 0 || type == 2))
                {
                    var lowerByte = variant & 0xFF; // item/chest passed to regular grotto
                    // 0xFF lower byte is cow grotto, no items passed, we want address A
                    // else: regular grotto, we want address 4
                    var newRotation = 0;
                    switch (lowerByte)
                    {
                        case 0xFF:
                            newRotation = 0xA; // cow grotto needs entrance A
                            break;
                        case 0x1F:             // tf grottos, shouldnt be placed because we dont know the rotation they used, this is backup
                            newRotation = 0x1;
                            break;
                        default:
                            newRotation = 0x4; // other assumes generic grotto
                            break;
                    }

                    actor.ChangeZRotation(newRotation);
                    actor.ActorIdFlags |= 0x2000; // do not convert Z rotation into 360 angle, leave alone to use as parameter
                }
            }
        }

        public static void FixSpecificActorRotations(SceneEnemizerData thisSceneData)
        {
            // several actors need to have their rotations fixed after being placed

            for(int a = 0; a < thisSceneData.Actors.Count; a++)
            {
                var testActor = thisSceneData.Actors[a];

                var wallVariants = testActor.GetWallVariants();
                if (testActor.ChangedToNewActor(GameObjects.Actor.Dexihand))
                {
                    // for now I want this manually just for dexihand: rotate forward a touch because its on a wall
                    if (testActor.CurrentVariantIsType(GameObjects.ActorType.Wall))
                    {
                        testActor.ChangeXRotation(60); // pitch rotation down a bit
                        continue;
                    }
                    // if dexihand is on ceiling, rotate so its dangling properly
                    if (testActor.CurrentVariantIsType(GameObjects.ActorType.Ceiling))
                    {
                        testActor.ChangeXRotation(180); // full rotation
                        continue;
                    }

                }

                // rotate darmani grave to face forward, for some reason the actor is rotated 180
                if (testActor.ChangedToNewActor(GameObjects.Actor.DarmaniGrave))
                {
                    testActor.ChangeYRotation(180); // pitch rotation down a bit
                }


            }
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

            // except I now use a custom mmra actor to replace this so that they match water height, this now hits the wrong spot

            //if (!ReplacementListContains(GameObjects.Actor.En_Stream)) return;

            var streamFid = GameObjects.Actor.En_Stream.FileListIndex();
            RomUtils.CheckCompressed(streamFid);
            var streamData = RomData.MMFileList[streamFid].Data;
            ReadWriteUtils.Arr_WriteU32(streamData, 0x39C, 0x0C02E3B2); // jal func_800B8FE8() -> Actor_PlaySfxAtPos()
        }

        


        private static void AllowGuruGuruOutside()
        {
            /// guruguru's actor spawns or kills itself based on time flags, ignoring that the spawn points themselves have timeflags
            /// if we want guruguru to be placed in the world without being restricted to day/night only (which is lame) we have to stop this
            if ( ! ReplacementListContains(GameObjects.Actor.GuruGuru)) return;

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

            if ( ! ReplacementListContains(GameObjects.Actor.BabaIsUnused)) return;

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

        private static void BlockBabyGoronIfNoSFXRando()
        {
            /// the baby crying is very annoying and loud, do not allow

            if (!_cosmeticSettings.RandomizeSounds) // if not sfx rando
            {
                var bab = ReplacementCandidateList.Find(act => act.ActorEnum == GameObjects.Actor.GoronKid);
                ReplacementCandidateList.Remove(bab);
            }
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
            // todo is this a duplicate of the other function I just wrote?

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
                enemy.Rotation.z = ActorUtils.MergeRotationAndFlags(rotation: newIndex - 1, flags: 0x7F);
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
                    if (actor.ActorEnum == GameObjects.Actor.DekuPatrolGuard
                     && thisSceneData.Scene.SceneEnum == GameObjects.Scene.PiratesFortressRooms)
                    {
                        kickoutAddr = 10; // upper locked room of sewer, best spot I got since I can't guarentee the player can swim, without changing how the guard kickout works
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

        private static void UpdateDynaEdgeCases(SceneEnemizerData thisSceneData)
        {
            // there are a few weird edge cases in dyna, we need to adress them

            // if we have punchable ikana towers, those are dyna general number times the segment count
            if (thisSceneData.ChosenReplacementObjects.Any(swap => swap.NewV == GameObjects.Actor.PunchableStoneTowerPillars.ObjectIndex()))
            {
                var punchable = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.PunchableStoneTowerPillars);
                for (int i = 0; i < punchable.Count; i++)
                {
                    var punchableActor = punchable[i];
                    int numSegments = (punchableActor.Variants[0] & 0xF) + 1;
                    punchableActor.DynaLoad.poly = numSegments * punchableActor.DynaLoad.poly;
                    punchableActor.DynaLoad.vert = numSegments * punchableActor.DynaLoad.vert;
                }
            }

            if (thisSceneData.ChosenReplacementObjects.Any(swap => swap.NewV == GameObjects.Actor.WarpDoor.ObjectIndex()))
            {
                var warpdoors = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.WarpDoor);
                for (int i = 0; i < warpdoors.Count; i++)
                {
                    var door = warpdoors[i];
                    if (door.Variants[0] == 0)
                    {
                        door.DynaLoad.poly = 0;
                        door.DynaLoad.vert = 0;
                    }
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

            void RemoveObjectKankyo(Map map, List<Actor> roomFreeActors, String logAppend)
            {
                var objectKankyoSearch = map.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.ObjectKankyo);

                foreach (var objKankyo in objectKankyoSearch)
                {
                    var blockedActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(objKankyo.OldActorEnum);
                    blockedActors.Add(objKankyo.ActorEnum);
                    List<Actor> acceptableReplacementFreeActors = roomFreeActors.FindAll(a => !blockedActors.Contains(a.ActorEnum)).ToList();

                    EmptyOrFreeActor(thisSceneData, objKankyo, map.Actors, acceptableReplacementFreeActors,
                        roomIsClearPuzzleRoom: true); // for now marking this true just because I dont want to re-calculate this since its in the wrong spot,
                    thisSceneData.Log.AppendLine(logAppend);
                }
            }

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
                for (int a = 0; a < nightUniqueList.Count; a++)
                {
                    var uniqueActor = new List<Actor> { nightUniqueList[a] };
                    var specificActorList = nightActorList.FindAll(act => act.ActorEnum == uniqueActor[0].ActorEnum);
                    TrimAllActors(thisSceneData, uniqueActor, specificActorList, allowLimits: false);
                }
            }

            // special case: now that we enabled snow everywhere, we can't let it spawn with giant+bubble, its too many particles
            for (int m = 0; m < thisSceneData.Scene.Maps.Count; ++m)
            {
                var map = thisSceneData.Scene.Maps[m];
                var roomFreeActors = GetRoomFreeActors(thisSceneData, m);

                if (map.Objects.Contains(GameObjects.Actor.Shabom.ObjectIndex()) && map.Objects.Contains(GameObjects.Actor.Giant.ObjectIndex()))
                {
                    RemoveObjectKankyo(map, roomFreeActors, " -*- trimming object kankyo because of rare double object");
                    
                }

                // used for rain detection if we re-enable rain in TF, but for now its disabled due to lag
                /*
                var sceneEnum = thisSceneData.Scene.SceneEnum;
                if (sceneEnum == GameObjects.Scene.TerminaField // rain scenes, just disable the actors spawn on day 2 and you're fine
                 || sceneEnum == GameObjects.Scene.DekuPalace
                 || sceneEnum == GameObjects.Scene.RomaniRanch
                 || sceneEnum == GameObjects.Scene.GreatBayCoast
                 || sceneEnum == GameObjects.Scene.DoggyRacetrack)
                {
                    var objectKankyoSearch = map.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.ObjectKankyo);

                    foreach (var objKankyo in objectKankyoSearch)
                    {
                        ActorUtils.SetActorSpawnTimeFlags(objKankyo, 0x3CF); // off for day/night 2
                    }
                    return;

                }
                if (_randomized.Settings.LogicMode == LogicMode.NoLogic)
                {
                    var vanillaBeans = map.Actors.FindAll(act => act.OldActorEnum == GameObjects.Actor.SoftSoilAndBeans && act.ActorEnum == GameObjects.Actor.SoftSoilAndBeans);
                    if (vanillaBeans != null && vanillaBeans.Count > 0)
                    {
                        RemoveObjectKankyo(map, roomFreeActors, " -*- trimming object kankyo because of vanilla beans in no logic reduction");
                        return;
                    }

                }
                else // logic exists
                {
                    var allSphereItems = _randomized.Spheres.SelectMany(u => u).ToList();
                    var stormsSearch = allSphereItems.FindAll(item => item.Item == GameObjects.Item.SongStorms.Name());
                    if (stormsSearch != null && stormsSearch.Count() > 0)
                    {
                        RemoveObjectKankyo(map, roomFreeActors, " -*- trimming object kankyo because of storms");

                    }
                } // */
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
            /// stray fairies that spawn from enemy kills are positioned to be right on top of the same enemy

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

            (GameObjects.Scene sceneName, int count)[] scenesToForce = new (GameObjects.Scene sceneName, int count)[]{
                (GameObjects.Scene.TerminaField,5),
                (GameObjects.Scene.GreatBayCoast,3),
                (GameObjects.Scene.IkanaGraveyard, 1),
                (GameObjects.Scene.ZoraCape,2),
                (GameObjects.Scene.RoadToIkana,1),
                (GameObjects.Scene.RoadToSouthernSwamp,2),
                (GameObjects.Scene.IkanaCanyon,1),
            };

            bool AttemptBushPlacement()
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
                var replacementCandidates = firstRestrictions.FindAll(act => thisSceneData.StandaloneActors.Contains(act)
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
                    for (int i = 0; i < bucketList.Count; i++)
                    {
                        var newList = bucketList[i];
                        var oldList = bucketList[largestIndex];
                        if (newList.count > oldList.count)
                        {
                            largestIndex = i;
                        }
                    }
                    replacementCandidates = firstRestrictions.FindAll(act => act.ActorId == bucketList[largestIndex].type);
                }

                if (replacementCandidates.Count == 0)//Debug.Assert(replacementCandidates.Count > 0);
                {
                    thisSceneData.Log.AppendLine($"Could not place supply bush in scene [{thisSceneData.Scene.SceneEnum}]");
                    //throw new Exception("Could not place supply bush, please try another seed.");
                    return false;
                }

                // change them to a bush containing things
                var actorChoice = replacementCandidates[thisSceneData.RNG.Next(replacementCandidates.Count)];

                thisSceneData.Log.AppendLine($" +++ BUSH SUPPLIES at index:[{actorChoice.RoomActorIndex}]"
                           + $" replacing new choice [{actorChoice.Name}][{actorChoice.Variants[0].ToString("X4")}]"
                           + $" where old actor was [{actorChoice.OldName}][{actorChoice.OldVariant.ToString("X4")}] ");

                // dont need to modify old as this happens dead last
                actorChoice.ChangeActor(GameObjects.Actor.NaturalPatchOfGrass, vars: 0x0001, modifyOld: false);
                actorChoice.Position.y += 50; // just in case the previous actor is more under the floor than exactly on the floor, bushes could fall through
                ActorUtils.SetActorSpawnTimeFlags(actorChoice);

                return true;
            }


            if (ACTORSENABLED)
            {
                var sceneSearch = scenesToForce.Where(tuple => tuple.sceneName == thisSceneData.Scene.SceneEnum).ToArray();
                if (sceneSearch.Count() > 0)
                {
                    Debug.Assert(sceneSearch[0].count > 0);
                    for(int i = 0; i < sceneSearch[0].count; i++)
                    {
                        AttemptBushPlacement();
                    }
                }
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

                //var flyingVariants = testActor.GetFlyingVariants(); // BROKEN, we want the new actor this checks the old Variants list
                var newVariantIsFlying = testActor.IsNewChoiceFlying();
                // if previous spawn was ground and the replacement actor has an attribute, adjust height
                // bug: type for bee in mountain spring is FLYING, should be ground, todo fix
                if (newVariantIsFlying && 
                    (testActor.OldVariantIsType(GameObjects.ActorType.Ground) // previous ground
                     || testActor.OldVariantIsType(GameObjects.ActorType.Pathing) // previous pathing(ground)
                     || testActor.OldVariantIsType(GameObjects.ActorType.WaterTop) // water surface too
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

                // lower swimming off the surface
                var waterVariants = testActor.GetWaterVariants();
                if (testActor.CurrentVariantIsType(GameObjects.ActorType.Water) &&
                    testActor.OldVariantIsType(GameObjects.ActorType.WaterTop)) 
                {
                    short randomHeight = (short)(10 + _seedRNG.Next(20));
                    testActor.Position.y -= randomHeight; // always lower flying enemies on ceiling placement, its usually way too high
                    log.AppendLine($" - lowered height of actor [{testActor.Name}] by [{randomHeight}] to lower below water surface");
                    UpdateStrayFairyHeight(testActor);
                }

                // raise swimming off the floor
                //var waterVariants = testActor.ActorEnum.GetAttribute<WaterVariantsAttribute>();
                if (testActor.CurrentVariantIsType(GameObjects.ActorType.Water) &&
                    testActor.OldVariantIsType(GameObjects.ActorType.WaterBottom)) 
                {
                    short randomHeight = (short)(10 + _seedRNG.Next(70));
                    testActor.Position.y += randomHeight; // always lower flying enemies on ceiling placement, its usually way too high
                    log.AppendLine($" - raised height of actor [{testActor.Name}] by [{randomHeight}] to above water bottom");
                    UpdateStrayFairyHeight(testActor);
                }

                var oldCeilingVariants = testActor.GetCeilingVariants();
                if (newVariantIsFlying && // chosen variant is flying
                    testActor.OldVariantIsType(GameObjects.ActorType.Ceiling))
                {
                    short randomHeight = (short)(50 + (_seedRNG.Next() % 50));
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

                var wallVariants = testActor.GetWallVariants();
                // special case: monkey spawns with an extra height offset from the floor, not at the location of the visible model
                if (testActor.ActorEnum == GameObjects.Actor.Monkey && testActor.Variants[0] == 0x02FF
                    && testActor.CurrentVariantIsType(GameObjects.ActorType.Wall))
                {
                    testActor.Position.y -= 90; // too high annoyingly
                }
                // special case: woodfall wooden flower spawns in the ground, needs to be raised
                if (testActor.ChangedToNewActor(GameObjects.Actor.WoodfallTempleWoodenFlower))
                {
                    testActor.Position.y += 100;
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
            //claimedSwitchFlags.Add(123); // debug
            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.Grottos)
            {
                // grotto have an extra list of switch flags:
                // the grotto door actor passes through the item/flag for the en_torch to use
                //  regular actor rando cannot detect this because the en_torch has no params, and gets its info through special grotto data
                var listOfGrottoVariants = GameObjects.Actor.GrottoHole.GetAttribute<GroundVariantsAttribute>().Variants;
                // we only want item/chest grottos, those are all type 0 (of param space 0xF000)
                listOfGrottoVariants.RemoveAll(variant => (variant & 0xF000) > 0);
                // 0x300 are adjacent grottos like deku playground
                listOfGrottoVariants.RemoveAll(variant => (variant & 0x0300) >= 0x300);
                listOfGrottoVariants.Remove(0); // one of the gossip stone grottos
                listOfGrottoVariants.Remove(0xFF); // cow grottos
                thisSceneData.Log.Append(" GROTTOS have switch flags from grotto entrances: \n[");
                foreach (var variant in listOfGrottoVariants)
                {
                    var switchFlag = variant & 0x1F;
                    claimedSwitchFlags.Add(switchFlag);
                    thisSceneData.Log.Append($"{variant.ToString("X4")}({switchFlag}),");

                }
                thisSceneData.Log.Append("]\n");
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
                usableSwitches.AddRange(Enumerable.Range(1, 0x7E)); // 0x7F is popular non-valid value and should probably be avoided
                usableSwitches.RemoveAll(sflag => claimedSwitchFlags.Contains(sflag));
                var reservedSceneFlags = SceneUtils.GetSceneReservedFlags(thisSceneData.Scene.SceneEnum);
                if (reservedSceneFlags != null)
                {
                    usableSwitches.RemoveAll(sflag => reservedSceneFlags.Contains(sflag));
                }
                usableSwitches.Reverse(); // we want to start at 0x7F and decend, under the assumption that they always used lower values
            }

            CreateUsableSwitchesList();

            // generate switch flag actors
            var actorsWithSwitchFlags = thisSceneData.Actors.ToList();
            var actorsWithSendFlags = new List<Actor>();
            for (int i = thisSceneData.Actors.Count - 1; i >= 0; i--) // reverse should let us use remoteat which should be faster
            {
                var actor = thisSceneData.Actors[i];
                var attr = actor.ActorEnum.GetAttribute<SwitchFlagsPlacementAttribute>();
                if (attr == null)
                {
                    actorsWithSwitchFlags.RemoveAt(i);
                } else if (attr.flagType == SwitchTrigger.Sends || attr.flagType == SwitchTrigger.SendsAndRecieves)
                {
                    actorsWithSendFlags.Add(actor);
                }
            }

            if (actorsWithSwitchFlags.Count == 0) return; // nothing to do here now

            // if there is both new chests and a new switching actor
            var newChestActors = thisSceneData.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.TreasureChest);
            int[] switchChestFlags = new int[thisSceneData.Scene.Maps.Count]; // no point saving the chest with the flag for now, just an int array is fine
            for (var i = 0; i < switchChestFlags.Length; i++) { switchChestFlags[i] = -1; }
            if (newChestActors.Count > 0)
            {
                for (int roomNum = 0; roomNum < thisSceneData.Scene.Maps.Count; roomNum++)
                {
                    var roomChests = newChestActors.FindAll(a => a.Room == roomNum);
                    var roomSwitches = actorsWithSendFlags.FindAll(a => a.Room == roomNum);
                    if (roomChests.Count > 0 && roomSwitches.Count > 0)
                    {
                        var randomChest = roomChests.Random(thisSceneData.RNG);
                        // change switch type to appear on switch
                        randomChest.Variants[0] &= 0x0FFF; // changing type to switch to activate
                        // zoey changed the upper byte to XX YY size and behavior
                        randomChest.Variants[0] |= 0xB000; // 0x2 should large gold, 0x3 is set on switch flag [10 11]

                        // in case this actor's last slot only spawned at night or something stupid, set it to always spawn
                        ActorUtils.SetActorSpawnTimeFlags(randomChest);
                        // chest has full switch flag range because it uses the full zrot, the sending trigger actor may not
                        // but so far, all sending actors I've found have 0x7F range anyway
                        var newSwitchChestFlag = usableSwitches[thisSceneData.RNG.Next(usableSwitches.Count)];
                        usableSwitches.Remove(newSwitchChestFlag);
                        switchChestFlags[randomChest.Room] = newSwitchChestFlag;
                        ActorUtils.SetActorSwitchFlags(randomChest, (short) newSwitchChestFlag);
                        log.AppendLine($" +++ WE FOUND SWITCH CHEST in room [{roomNum}], chest actor spot [{randomChest.RoomActorIndex}] +++");
                        log.AppendLine($"   had switch flags modified to [{newSwitchChestFlag}][{randomChest.Rotation.z.ToString("X4")}]");
                        actorsWithSwitchFlags.Remove(randomChest); // dont double dip, this actor is set
                        randomChest.ActorIdFlags |= 0x2000; // do not convert z rotation, we need it for chests
                        randomChest.Rotation.y |= 0x7F; // set cutscene value to -1 to allow the chest to appear without a working cutscene
                    }
                    for (int c = 0; c < roomChests.Count; c++)
                    {
                        var chest = roomChests[c];
                        chest.ActorIdFlags |= 0x2000; // set flag to prevent Z rotation conversion
                    }
                }
            }

            // check for all actors that listen for flag sends
            List<(Actor act, int flag)> recievesList = new List<(Actor act, int flag)> { };
            for (int actorIndex = 0; actorIndex < actorsWithSwitchFlags.Count; actorIndex++) {
                var actor = actorsWithSwitchFlags[actorIndex];
                var attr = actor.ActorEnum.GetAttribute<SwitchFlagsPlacementAttribute>();
                var thisRoomHiddenChestFlag = switchChestFlags[actor.Room];
                if (thisRoomHiddenChestFlag != -1) { continue; } // chest takes priority

                    // have to have attribute by here, its not null, I'm not checking for cosmic radiation damage
                if (attr.flagType == SwitchTrigger.Receives || attr.flagType == SwitchTrigger.SendsAndRecieves)
                {
                    var newSwitch = usableSwitches[0];

                    ActorUtils.SetActorSwitchFlags(actor, (short)newSwitch);
                    usableSwitches.RemoveAt(0);
                    log.AppendLine($" ++ i[{actorIndex}][{actor.ActorEnum}] had recieve flag modified to [{newSwitch}] ++");

                    recievesList.Add((actor, newSwitch));
                    actorsWithSwitchFlags.Remove(actor);
                }
            }
            // finally, all of the rest
            for (int actorIndex = 0; actorIndex < actorsWithSwitchFlags.Count; actorIndex++)
            {
                var actor = actorsWithSwitchFlags[actorIndex];
                var switchFlagsAttr = actor.ActorEnum.GetAttribute<SwitchFlagsPlacementAttribute>();
                var switchFlags = ActorUtils.GetActorSwitchFlags(actor, (short)actor.Variants[0]);

                if (usableSwitches.Count == 0) // we ran out, recreate list
                {
                    CreateUsableSwitchesList();
                }

                if (switchFlagsAttr.flagType == SwitchTrigger.Sends || switchFlagsAttr.flagType == SwitchTrigger.SendsAndRecieves) {
                    var thisRoomHiddenChestFlag = switchChestFlags[actor.Room];
                    if (thisRoomHiddenChestFlag != -1) 
                    {
                        // if there is a chest we want all switches to activate,
                        // because its rare and we dont want the player to miss it because only one switch activates it
                        ActorUtils.SetActorSwitchFlags(actor, (short)thisRoomHiddenChestFlag);
                        log.AppendLine($" ++ Chest trigger actor set: [{actor.ActorEnum}]r[{actor.Room}]v[{actor.Variants[0].ToString("X4")}], at spawn [{actor.RoomActorIndex}] ++");
                        continue;
                    }
                    else if (recievesList.Count() > 0) // other receive flag actors exist, yes I know this should be merged, but chest is important
                    {
                        var randomRecieveSwitchFlagActor = recievesList[thisSceneData.RNG.Next(recievesList.Count())];
                        ActorUtils.SetActorSwitchFlags(actor, (short) randomRecieveSwitchFlagActor.flag);
                        log.AppendLine($" ++ Send trigger actor set: [{actor.ActorEnum}]r[{actor.Room}]v[{actor.Variants[0].ToString("X4")}], at spawn [{actor.RoomActorIndex}] ++");
                        log.AppendLine($"   ++ to target actor : [{randomRecieveSwitchFlagActor.act.ActorEnum}]r[{randomRecieveSwitchFlagActor.act.Room}]v[{randomRecieveSwitchFlagActor.act.Variants[0].ToString("X4")}], at spawn [{randomRecieveSwitchFlagActor.act.RoomActorIndex}] ++");
                        continue;
                    }
                }

                if (usableSwitches.Contains(switchFlags)) // not detected in vanilla, leave as is and claim
                {
                    usableSwitches.Remove(switchFlags);
                    log.AppendLine($" = i[{actor.RoomActorIndex}][{actor.ActorEnum}] had switch flags which were not detect as used, and claimed switch [{switchFlags}]=");
                }
                else // we have switch flag and we have a collision, we need to change it
                {
                    var newSwitch = usableSwitches[0];
                    ActorUtils.SetActorSwitchFlags(actor, (short) newSwitch);
                    usableSwitches.RemoveAt(0);
                    log.AppendLine($" + i[{actor.RoomActorIndex}][{actor.ActorEnum}] had switch flags modified to [{newSwitch}] to avoid conflicts with others +");
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
            //usableTreasureFlags.Remove(0x1D); // testing
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
                    log.AppendLine($" +++ [{actor.RoomActorIndex}][{actor.ActorEnum}] had treasure flags that didn't collide, leaving alone with switch [{treasureFlags}] +++");

                }
                else // we have switch flag and we have a collision, we need to change it
                {
                    var newSwitch = usableTreasureFlags[0];
                    ActorUtils.SetActorTreasureFlags(actor, (short)newSwitch);
                    usableTreasureFlags.Remove(newSwitch);
                    log.AppendLine($" +++ [{actor.RoomActorIndex}][{actor.ActorEnum}] had treasure flags modified to [{newSwitch}] +++");
                }
            }
        }

        private static void EnsureOnlyOneKankyo(SceneEnemizerData thisSceneData)
        {
            // temp, makes sure demo kankyo and object kankyo cannot be in the same area
            var objSearch = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.ObjectKankyo);
            var demoSearch = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.Demo_Kankyo);
            if (objSearch.Count > 0 && demoSearch.Count > 0)
            {
                for(int i = 0; i < demoSearch.Count; ++i)
                {
                    demoSearch[i].ChangeActor(GameObjects.Actor.Empty, 0x0);
                }
            }
        }

        private static void FixKaizokuType(SceneEnemizerData thisSceneData)
        {
            /// Kaizoku actor colors are linked to their Z rotation instead of their params
            var objSearch = thisSceneData.Actors.FindAll(act => act.ActorEnum == GameObjects.Actor.PirateColonel);
            if (objSearch.Count > 0)
            {
                for (int i = 0; i < objSearch.Count; ++i)
                {
                    var targetActor = objSearch[i];
                    if (targetActor.Variants[0] == 0x24B)
                    {
                        targetActor.ChangeZRotation(0);
                        targetActor.Variants[0] &= ~0x3F; // clear the exit index
                    }
                    else if (targetActor.Variants[0] == 0x20B) {
                        targetActor.ChangeZRotation(1);
                        targetActor.Variants[0] &= ~0x3F;
                    }
                    else if (targetActor.Variants[0] == 0x2CB)
                    {
                        targetActor.ChangeZRotation(2);
                        targetActor.Variants[0] &= ~0x3F;
                    }
                    targetActor.ActorIdFlags |= 0x2000; // set flag to not-convert z rotation
                }
            }
        }

        private static void ForceWaterCeilingSpawnerInGBT(SceneEnemizerData thisSceneData)
        {
            if (thisSceneData.Scene.SceneEnum != GameObjects.Scene.GreatBayTemple) return;

            foreach (var act in thisSceneData.Actors.FindAll(a => a.ActorEnum == GameObjects.Actor.CeilingSpawner))
            {
                act.Variants[0] = 0x00FF; // force to be water instead of fire, because fire can softlock swimming zora link
            }

        }

        private static void SetZerothAndFourthDayFlagsForAllActors(SceneEnemizerData thisSceneData)
        {
            for (int i = 0; i < thisSceneData.Actors.Count; i++){
                var act = thisSceneData.Actors[i];

                ActorUtils.SetActorSpawnTimeFor04Day(act);
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
                            var newActor = ReplacementCandidateList.Find(act => act.ActorEnum == replacement);
                            Debug.Assert(newActor != null); // cannot find actor, enemizer?

                            thisSceneData.AcceptableCandidates.Add(newActor);
                            thisSceneData.CandidatesPerObject[objectIndex].Add(newActor);

                            // we need to trim variants to only compatible variants, which was in getmatchpool we skipped
                            var actorInObject = thisSceneData.ActorsPerObject[objectIndex][0];
                            var isClearEnemyPuzzleRoom = thisSceneData.Scene.SceneEnum.IsClearEnemyPuzzleRoom(actorInObject.Room);
                            for (int i = 0; i < 1000; ++i) 
                            {
                                // rng allows for weird types, in this case we want to force, so use loop, to get this code you should know what you are doing
                                var acceptableVariants = actorInObject.CompatibleVariants(newActor, thisSceneData.RNG, isClearEnemyPuzzleRoom);
                                if (acceptableVariants != null)
                                {
                                    newActor.SetVariants(acceptableVariants);
                                    break; 
                                }
                                Debug.Assert(newActor.Variants != null && newActor.Variants.Count > 0);
                            }
                        }
                        return true;
                    }
                    return false;
                }

                //if (TestHardSetObject(GameObjects.Scene.TerminaField, GameObjects.Actor.Leever, GameObjects.Actor.AnjusGrandmaCredits)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthClockTown, GameObjects.Actor.BuisnessScrub, GameObjects.Actor.BeanSeller)) continue;
                //if (TestHardSetObject(GameObjects.Scene.Grottos, GameObjects.Actor.SoftSoilAndBeans, GameObjects.Actor.PunchableStoneTowerPillars)) continue;
                //if (TestHardSetObject(GameObjects.Scene.Grottos, GameObjects.Actor.Peahat, GameObjects.Actor.Freezard)) continue;
                if (TestHardSetObject(GameObjects.Scene.DoggyRacetrack, GameObjects.Actor.ClayPot, GameObjects.Actor.BedroomPostman)) continue;
                //if (TestHardSetObject(GameObjects.Scene.ClockTowerInterior, GameObjects.Actor.HappyMaskSalesman, GameObjects.Actor.SkeleKnight)) continue;

                //if (TestHardSetObject(GameObjects.Scene.ZoraHall, GameObjects.Actor.RegularZora, GameObjects.Actor.DragonFly)) continue;
                //if (TestHardSetObject(GameObjects.Scene.OceanSpiderHouse, GameObjects.Actor.Seth1, GameObjects.Actor.BeanSeller)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthernSwamp, GameObjects.Actor.SquareSign, GameObjects.Actor.BeanSeller)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthernSwampClear, GameObjects.Actor.En_Owl, GameObjects.Actor.UnusedStoneTowerStoneElevator)) continue;
                //if (TestHardSetObject(GameObjects.Scene.SouthernSwampClear, GameObjects.Actor.RegularFrogs, GameObjects.Actor.ClocktowerGearsAndOrgan)) continue;
                //if (TestHardSetObject(GameObjects.Scene.GoronShrine, GameObjects.Actor.Torch, GameObjects.Actor.LostWoodsCutsceneTrees)) continue;
                //if (TestHardSetObject(GameObjects.Scene.BeneathGraveyard, GameObjects.Actor.CeilingSpawner, GameObjects.Actor.Dexihand)) continue;
                //if (TestHardSetObject(GameObjects.Scene.StockPotInn, GameObjects.Actor.Gorman, GameObjects.Actor.HookshotWallAndPillar)) continue;
                //if (TestHardSetObject(GameObjects.Scene.PoeHut, GameObjects.Actor.SpiritHouseOwner, GameObjects.Actor.PirateColonel)) continue;
                //if (TestHardSetObject(GameObjects.Scene.RoadToSouthernSwamp, GameObjects.Actor.SquareSign, GameObjects.Actor.Carpenter)) continue;
                //if (TestHardSetObject(GameObjects.Scene.GreatBayCoast, GameObjects.Actor.SwimmingZora, GameObjects.Actor.LabFish)) continue;
                //if (TestHardSetObject(GameObjects.Scene.DekuPalace, GameObjects.Actor.Torch, GameObjects.Actor.BeanSeller)) continue;

                if (TestHardSetObject(GameObjects.Scene.SPOT00, GameObjects.Actor.Evan, GameObjects.Actor.IronKnuckle)) continue;
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
                            compatibilityTestActor.SetVariants(compatibleVariants);
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
                        var pathingVariants = candidate.SortedVariants[(int)GameObjects.ActorType.Pathing - 1];
                        if (pathingVariants != null && pathingVariants.Count > 0)
                        {
                            var groundVariants = candidate.SortedVariants[(int)GameObjects.ActorType.Ground - 1];

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
            var earlyReducedCandidateList = Actor.CopyActorList(thisSceneData.AcceptableCandidates);
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
                ReplacementListRemove(earlyReducedCandidateList, blockedActor);
            }

            // pre-load credit limited actor placements
            List<GameObjects.Actor> sceneCreditsActors = null;
            var creditsLimitations = thisSceneData.Scene.SceneEnum.GetAttributes<Attributes.ActorizerSceneCreditsActor>().ToList();
            if (creditsLimitations != null)
            {
                for (int a = 0; a < creditsLimitations.Count; a++)
                {
                    var limitedAttr = creditsLimitations[a];
                    if (limitedAttr.Room == -1 || limitedAttr.Room == oldActors[0].Room)
                    {
                        sceneCreditsActors = limitedAttr.CreditsActors;
                    }
                }
            }

            // TODO does this NEED to be a double loop? does anything change per enemy copy that we should worry about?
            for (var oldActorIndex = 0; oldActorIndex < oldActors.Count; oldActorIndex++) // this is all copies of an enemy in a scene, so all bo or all guay
            {
                var oldActor = oldActors[oldActorIndex];
                List<Actor> lateReducedCandidateList = earlyReducedCandidateList.ToList();

                // the enemy we got from the scene has the specific variant number, the general game object has all
                foreach (var candidateEnemy in lateReducedCandidateList)
                {
                    // if current test actor not already in the new pool
                    //   TODO why would we get duplicates this late? shouldnt the candidates be unique list?
                    if (enemyMatchesPool.Any(act => act.ActorId == candidateEnemy.ActorId)) continue;

                    var compatibleVariants = oldActor.CompatibleVariants(candidateEnemy, thisSceneData.RNG);
                    if (compatibleVariants == null || compatibleVariants.Count == 0) continue;

                    var newEnemy = candidateEnemy.CopyActor();

                    if (sceneCreditsActors != null && sceneCreditsActors.Contains(oldActor.ActorEnum)) // scene demands we check
                    {
                        var candidateCreditsBlockedVariants = candidateEnemy.CreditsBlockedVariants();

                        if (candidateCreditsBlockedVariants != null)
                        {
                            newEnemy.SetVariants(newEnemy.Variants.Except(candidateCreditsBlockedVariants).ToList());
                            newEnemy.TrimVariantsList();
                            if (newEnemy.Variants.Count == 0)
                                continue; // nothing more to do
                        }
                    }

                    // reduce varieties to meet killable requirements
                    if (MustBeKillable)
                    {
                        newEnemy.SetVariants(candidateEnemy.KillableVariants(compatibleVariants)); // reduce to available
                        newEnemy.TrimVariantsList();
                        if (newEnemy.Variants.Count == 0)
                            continue; // can't put this enemy here: it has no non-respawning variants

                        // if the actor is in a kill all enemy room, reduce the chances of boring enemies from showing up here
                        if ((oldActor.MustNotRespawn
                            && !(thisSceneData.Scene.SceneEnum == GameObjects.Scene.WoodfallTemple && oldActor.Room == 9) // dark room exception
                            && !containsFairyDroppingEnemy) && _seedRNG.Next(100) < 25)
                        {
                            newEnemy.RemoveEasyEmemies();
                            if (newEnemy.Variants.Count == 0) // TODO refactor this into the overall flow
                                continue;
                        }

                    }
                    else if (oldActor.Blockable == false)
                    {
                        if (newEnemy.ActorEnum.GetAttribute<BlockingVariantsAll>() != null) {
                            continue;
                        }
                        else
                        {
                            newEnemy.SetVariants(compatibleVariants);
                            newEnemy.TrimVariantsList();
                            newEnemy.RemoveBlockingTypes();
                            if (newEnemy.Variants.Count == 0) // TODO refactor this into the overall flow
                                continue;
                        }
                    }
                    else
                    {
                        newEnemy.SetVariants(compatibleVariants);
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
            // we use it for day/night final trim though, need to rethink how much of this is necessary? 
            var restrictedActors = candidateAndCompanionGroup.FindAll(act => act.HasVariantsWithRoomLimits() || act.OnlyOnePerRoom != null);
            for (int actorIndex = 0; actorIndex < restrictedActors.Count; ++actorIndex)
            {
                var problemActor = restrictedActors[actorIndex];
                var blockedActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(problemActor.OldActorEnum);

                // we need to split enemies per room
                for (int roomIndex = 0; roomIndex < thisSceneData.Scene.Maps.Count; ++roomIndex)
                {
                    var roomActors = knownChangedActorList.FindAll(act => act.Room == roomIndex && act.ActorId == problemActor.ActorId);
                    //var roomActors = thisSceneData.Actors.FindAll(act => act.Room == roomIndex && act.ActorId == problemActor.ActorId); // why was this abandoned?
                    if (roomActors.Count == 0) continue; // nothing to trim: no actors in this room
                    var roomIsClearPuzzleRoom = thisSceneData.Scene.SceneEnum.IsClearEnemyPuzzleRoom(roomIndex);
                    var candidates = GetRoomFreeActors(thisSceneData, roomIndex);
                    candidates.RemoveAll(u => blockedActors.Contains(u.ActorEnum));
                    if (!allowLimits)
                    {
                        // assume final pass, don't even bother adding limited actors
                        candidates.RemoveAll(u => u.OnlyOnePerRoom != null );
                        candidates.RemoveAll(u => u.HasVariantsWithRoomLimits() );
                    }

                    if (problemActor.OnlyOnePerRoom != null)
                    {
                        // all actors merged together into one list in the function
                        TrimSpecificActor(thisSceneData, problemActor, roomActors, candidates, roomIsClearPuzzleRoom);
                    }
                    else
                    {
                        var limitedVariants = problemActor.Variants.FindAll(act => problemActor.VariantMaxCountPerRoom(act) >= 0);
                        foreach (var variant in limitedVariants)
                        {
                            // per actor/variant combo
                            TrimSpecificActor(thisSceneData, problemActor, roomActors, candidates, roomIsClearPuzzleRoom, variant: variant);
                        }
                    }
                }
            } // end for trim restricted actors
        }

        public static void TrimSpecificActor(SceneEnemizerData thisSceneData, Actor actorType, List<Actor> roomActors, List<Actor> replacementCandidates,
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
                var freeActorSearch = replacementCandidates.Find(act => act.ActorId == actorType.ActorId);
                if (freeActorSearch != null)
                {
                    replacementCandidates.Remove(freeActorSearch);
                }

                Debug.Assert(roomActors.Count > 0);

                // kill the rest since max is reached
                // we want to limit replacements here above the per-actor function to save re-doing it
                var blockedActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(roomActors[0].OldActorEnum);
                List<Actor> acceptableReplacementFreeActors = replacementCandidates.FindAll(a => !blockedActors.Contains(a.ActorEnum)).ToList();
                foreach (var enemy in trimCandidates) // for all specific actor in actorType
                {
                    var enemyIndex = roomActors.IndexOf(enemy);
                    EmptyOrFreeActor(thisSceneData, enemy, roomActors, acceptableReplacementFreeActors, roomIsClearPuzzleRoom);
                }
            } // end If Room has Actors with Variants we want to trim
        } // end TrimSpecificActor

        private static GameObjects.Scene[] badRoomScenes =
        {
            /*
            GameObjects.Scene.WoodfallTemple,
            GameObjects.Scene.SnowheadTemple,
            GameObjects.Scene.GreatBayTemple,
            GameObjects.Scene.StoneTowerTemple,
            GameObjects.Scene.InvertedStoneTowerTemple,
            GameObjects.Scene.MountainVillageSpring,
            GameObjects.Scene.DekuPalace
            */
        };


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

                // issue with dulpicate removal: it can throw off the order of other objects later in the list for this room

                if ( ! badRoomScenes.Contains(thisSceneData.Scene.SceneEnum))
                {
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
                newDungeonOnlyPot.SetVariants(clayPotDungeonVariants.ToList());
                newDungeonOnlyPot.SortedVariants[(int)GameObjects.ActorType.Ground] = newDungeonOnlyPot.Variants;

                sceneFreeActors.Add(newDungeonOnlyPot);
            }
            // todo do this for tall grass too
            if (VanillaEnemyList.Contains(GameObjects.Actor.TallGrass) && sceneIsField)
            {
                var newFieldTallGrass = new Actor(GameObjects.Actor.TallGrass);
                newFieldTallGrass.SetVariants(tallGrassFieldObjectVariants.ToList());
                newFieldTallGrass.SortedVariants[(int)GameObjects.ActorType.Ground - 1] = newFieldTallGrass.Variants;
                // weirdly, the code checks if the bushes are underwater and applies water sway to them, this is intended by the forfathers
                newFieldTallGrass.SortedVariants[(int)GameObjects.ActorType.WaterBottom - 1] = newFieldTallGrass.Variants;
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
                    iceblock.SetVariants(newVariants);
                    iceblock.SortedVariants[(int)GameObjects.ActorType.Ground - 1] = newVariants;

                }
            }

            // issue: so far sceneFreeActors uses reference copy to the global FreeCandidatesList, changes to each actor affect global, not cool
            var convertedList = new List<Actor>(sceneFreeActors.Count);
            for (int a = 0; a < sceneFreeActors.Count; a++)
            {
                convertedList.Add(sceneFreeActors[a].CopyActor());
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

                    var sceneReplacementRestrictions = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(targetActors[0].ActorEnum);

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
                        {
                            // TODO: this companion checks for scene objects but scene objects is shifting, is it too early?
                            continue;
                        }

                        var companionType = companion.Companion;
                        // if its banned on this actor slot, also avoid
                        var blockedReplacementActors = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(actor.OldActorEnum);
                        if (blockedReplacementActors.Contains(companionType)) // blocked from being used as replacement
                        {
                            continue; // cannot use
                        }

                        // if candidate is blocked from being put in this spot, ignore this possibility
                        if (sceneReplacementRestrictions != null && sceneReplacementRestrictions.Contains(companionType)) continue;

                        /*if (objectHasBlockingSensitivity && companionType.IsBlockingActor()) // actor is blocking type, physically
                        {
                            continue; // cannot use
                        } // */

                        var newCompanion = new Actor(companionType);
                        newCompanion.SetVariants(companion.Variants);
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

            if (actorsWithCompanions.Count <= 2) return; ///ZZZ

            for (int i = 0; i < actorsWithCompanions.Count; ++i)
            {
                var mainActor = actorsWithCompanions[i];
                var mainActorEnum = (GameObjects.Actor)mainActor.ActorId;
                var companions = mainActorEnum.GetAttributes<AlignedCompanionActorAttribute>().ToList();
                var scenePlacementRestrictions = thisSceneData.Scene.SceneEnum.GetBlockedReplacementActors(mainActor.ActorEnum);
                foreach (var companion in companions)
                {
                    var companionEnum = companion.Companion;
                    // todo detection of ourVars too
                    // scan for companions that can be moved
                    // for now, assume all previously used companions must be left untouched, no shuffling
                    var eligibleCompanions = thisSceneData.Actors.FindAll(act =>
                           act.ActorId == (int) companionEnum                    // correct actor
                        && mainActor.Room == act.Room                            // both in the same room
                        && act.previouslyMovedCompanion == false                 // not already used
                        && companion.Variants.Contains(act.Variants[0])          // acceptable variant
                        && ! scenePlacementRestrictions.Contains(companionEnum)  // the companion wasnt blocked from being put in this location
                    ); 

                    if (mainActor.Blockable == false)
                    {
                        eligibleCompanions.RemoveAll(comp => comp.ActorEnum.IsBlockingActor(variant: comp.Variants[0])); // blocking actor sensitive spots
                    }

                    if (eligibleCompanions != null && eligibleCompanions.Count > 0)
                    {
                        var randomCompanion = eligibleCompanions[thisSceneData.RNG.Next(eligibleCompanions.Count)];
                        // first move on top, then adjust
                        randomCompanion.Position.x = mainActor.Position.x;
                        randomCompanion.Position.y = (short)(mainActor.Position.y + companion.RelativePosition.y);
                        randomCompanion.Position.z = mainActor.Position.z;

                        // todo: use x and z, with actor rotation, to figure out where to move the actors to in the event of "tupe: in front"

                        if (companion.RelativePosition.x == 50) // inFrontType
                        {
                            //(rotation & 0x1FF) << 7)
                            ushort mainActorRawYaw = (ushort)((mainActor.Rotation.y) >> 7 & 0x1FF);
                            double mainActorYaw = (float)(mainActorRawYaw * (Math.PI / 180));
                            double cosYaw = Math.Cos(mainActorYaw);
                            double sinYaw = Math.Sin(mainActorYaw);

                            randomCompanion.Position.x += (short)(companion.RelativePosition.x * sinYaw);
                            randomCompanion.Position.z += (short)(companion.RelativePosition.z * cosYaw);

                            randomCompanion.ChangeYRotation((mainActorRawYaw + 180) % 360); // inverse of the other actor rotation for now
                        }

                        // error: some rooms change actors layouts, we need to match the spawn flags for moved actors to match
                        randomCompanion.Rotation.x &= ~0x7F; // clear old spawn flags
                        randomCompanion.Rotation.x |= (short)(mainActor.Rotation.x & 0x7F); // pull flags from main and write to companion rotation
                        randomCompanion.Rotation.z &= ~0x7F; // clear old spawn flags
                        randomCompanion.Rotation.z |= (short)(mainActor.Rotation.z & 0x7F); // pull flags from main and write to companion rotation

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
            AddAniObjectIfTerminaFieldTree(thisSceneData);
            RemoveScarecrowFromTradingPostIfSOTRandomized(thisSceneData);
            RemoveStalagmiteFromSnowheadIfTrick(thisSceneData);
            ChangeGravePotsIfRandomized(thisSceneData);
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
                    newObject = freeObjList[thisSceneData.RNG.Next() % (freeObjList.Count - 1)];
                }

                // for now we just bypass rando and set it manually
                thisSceneData.Scene.Maps[0].Objects[6] = newObject;
            }
        }

        private static void RemoveScarecrowFromTradingPostIfSOTRandomized(SceneEnemizerData thisSceneData)
        {
            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.TradingPost)
            {
                var songOfTimeShuffled = _randomized.Settings.CustomItemList.Contains(GameObjects.Item.SongTime);
                var songOfTimeStarting = _randomized.Settings.CustomStartingItemList.Contains(GameObjects.Item.SongTime);
                if (songOfTimeShuffled && !songOfTimeStarting)
                {
                    thisSceneData.Actors.RemoveAll(act => act.ActorEnum == GameObjects.Actor.Scarecrow);
                    thisSceneData.Objects.RemoveAll(obj => obj == GameObjects.Actor.Scarecrow.ObjectIndex());
                }
            }

        }

        private static void RemoveStalagmiteFromSnowheadIfTrick(SceneEnemizerData thisSceneData)
        {
            // the boss key skip trick needs the objects to be there

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.SnowheadTemple)
            {
                var SHTBossKeySkipTrickEnabled = _randomized.Settings.EnabledTricks.Contains("SHT BK Skip");
                if (SHTBossKeySkipTrickEnabled)
                {
                    thisSceneData.Actors.RemoveAll(act => act.ActorEnum == GameObjects.Actor.IceCavernStelagtite);
                    thisSceneData.Objects.RemoveAll(obj => obj == GameObjects.Actor.IceCavernStelagtite.ObjectIndex());
                }
            }
        }

        private static void ChangeGravePotsIfRandomized(SceneEnemizerData thisSceneData)
        {
            // some of the pots are copies of water bottom pots, we need to change them to match,
            // but only AFTER we know we want to randomize them because they don't have valid items

            if (thisSceneData.Scene.SceneEnum == GameObjects.Scene.BeneathGraveyard)
            {
                // wont be in the valid Actors list if not being randomized
                var troublePot = thisSceneData.Actors.Find(act => act.OldActorEnum == GameObjects.Actor.ClayPot && act.OldVariant == 0x450A);
                if (troublePot != null)
                {
                    troublePot.OldVariant = troublePot.Variants[0] = 0x451A; // same drop, different flag

                    var secondPot = thisSceneData.Scene.Maps[1].Actors[5]; // 550A
                    secondPot.OldVariant = troublePot.Variants[0] = 0x551A;
                }
            }
        }

        private static void TrimSceneAcceptableCandidateList(SceneEnemizerData thisSceneData)
        {
            // some scenes are blocked from having enemy placements, do this ONCE before GetMatchPool, which would do it per-enemy
            thisSceneData.AcceptableCandidates = ReplacementCandidateList.FindAll(act => !act.ActorEnum.BlockedScenes().Contains(thisSceneData.Scene.SceneEnum))
                                                                         .FindAll(act => !act.NoPlacableVariants());

            //thisSceneData.AcceptableCandidates.RemoveAll(act => act.NoPlacableVariants());
            //var carpenter = thisSceneData.AcceptableCandidates.Find(act => act.ActorEnum == GameObjects.Actor.Carpenter);
            //Debug.Assert(! carpenter.Variants.Contains(6));

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
                    //#if DEBUG
                    //thisSceneData.Log.AppendLine($" (-) actor rng weight trimmed from scene placement: [{actor.Name}]");
                    //#endif
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
                _LogMutex.WaitOne(); // with paralel, thread safety
                using (StreamWriter sw = new StreamWriter(_outputSettings.OutputROMFilename + "_EnemizerLog.txt", append: true))
                {
                    sw.WriteLine(""); // spacer from last flush
                    sw.Write(thisSceneData.Log);
                }
                _LogMutex.ReleaseMutex();
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
            if (thisSceneData.Objects.Count == 0)
                return;
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


            CheckForHardToFindBugsPre(thisSceneData);

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
                    var chosenCandidatesForThisObject = thisSceneData.CandidatesPerObject[objectIndex];
                    List<Actor> subMatches = chosenCandidatesForThisObject.FindAll(act => act.ObjectId == chosenObject);

                    #if DEBUG
                    var original_object = VanillaEnemyList.Find(act => act.ObjectIndex() == thisSceneData.ChosenReplacementObjects[objectIndex].OldV);
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
                UpdateDynaEdgeCases(thisSceneData);
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
            if (scene.SceneEnum == GameObjects.Scene.Grottos) // force specific actor/variant for debugging
            {
                // if you want to force object here, use ChosenReplacementObjectsPerMap

                //thisSceneData.Actors[35].ChangeActor(GameObjects.Actor.En_Invisible_Ruppe, vars: 0x01D0); // hitspot
                //var target = thisSceneData.Scene.Maps[0].Actors[23];
                // 23 to 25
                //target.ChangeActor(GameObjects.Actor.ObjSwitch, vars: 0x7C14); // crashes
                //target.ChangeActor(GameObjects.Actor.ObjSwitch, vars: 0x7C04); // 2 also crashes
                //thisSceneData.Scene.Maps[13].Actors[0].ChangeActor(GameObjects.Actor.ReDead, vars: 0x7804);
                //thisSceneData.Scene.Maps[13].Actors[3].ChangeActor(GameObjects.Actor.ReDead, vars: 0xF7FE);
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
            SwitchSkullfishBackToEncount1(thisSceneData);
            FixSnowballActorSpawns(thisSceneData);
            FixNewGrottoZRotation(thisSceneData);
            FixSpecificActorRotations(thisSceneData);
            EnsureOnlyOneKankyo(thisSceneData);
            FixKaizokuType(thisSceneData);
            ForceWaterCeilingSpawnerInGBT(thisSceneData); // todo move to late fixes
            SetZerothAndFourthDayFlagsForAllActors(thisSceneData);
            // the following modify Variant which can confuse typing system
            FixPathingVars(thisSceneData); // any patrolling types need their vars fixed
            FixKickoutEnemyVars(thisSceneData); // and same with the two actors that have kickout addresses
            FixTreasureFlagVars(thisSceneData, flagLog);
            FixSwitchFlagVars(thisSceneData, flagLog); // swapped to be even lower 86

            // print debug actor locations
            WriteOutput("####################################################### ");
            for (int a = 0; a < thisSceneData.Actors.Count; a++)
            {
                var actor = thisSceneData.Actors[a];
                string dsize = actor.DynaLoad.poly > 0 ? $" dyn: [{actor.DynaLoad.poly}/{actor.DynaLoad.vert}]" : "";
                var actorNameData = $"  Old actor:[{thisSceneData.Scene.SceneEnum}]r[{actor.Room.ToString("D2")}]n[{actor.RoomActorIndex.ToString("D3")}]a[{actor.OldName}]v[0x{actor.OldVariant.ToString("X4")}]";
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

            CheckForHardToFindBugsPre(thisSceneData);

            // realign all scene companion actors
            MoveAlignedCompanionActors(thisSceneData);

            SetSceneEnemyObjects(scene, thisSceneData.ChosenReplacementObjectsPerMap);
            SceneUtils.UpdateScene(scene); // writes scene actors back to binary

            WriteOutput($" time to complete randomizing [{scene.SceneEnum}]: " + GET_TIME(thisSceneData.StartTime) + "ms");
            WriteOutput($" ending timestamp : [{DateTime.Now.ToString("hh:mm:ss.fff tt")}]");
            FlushLog();
        } // SwapSceneEnemies

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
                if (command == "perching_variants")
                {
                    var newPerchingVariants = valueStr.Split(",").ToList();
                    var newPerchingVariantsShort = newPerchingVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.perchingVariants = newPerchingVariantsShort;
                    continue;
                }
                if (command == "wall_variants")
                {
                    var newWallVariants = valueStr.Split(",").ToList();
                    var newWallVariantsShort = newWallVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.wallVariants = newWallVariantsShort;
                    continue;
                }
                if (command == "respawning_variants")
                {
                    var newRespawningVariants = valueStr.Split(",").ToList();
                    var newRespawningVariantsShort = newRespawningVariants.Select(variant => Convert.ToInt32(variant.Trim(), 16)).ToList();

                    newInjectedActor.respawningVariants = newRespawningVariantsShort;
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
                // this is a list of broken actors that we cannot use, they have been removed but I don't trust users to remove the file and not just overwrite a previous directory
                if (filePath.Contains("SafeBoat.mmra")
                 || filePath.Contains("FairySpot.mmra") // is missing a variant, and was not working, not even sure what it was doing, TODo
                 || filePath.Contains("BabaIsLoaded.mmra") // talk locking, lost the code, have to disable because no time to rewrite
                 || filePath.Contains("HairyGrog.mmra") // my code overrites zoeys item changes, I can't fix without breaking my code for enemies
                 || filePath.Contains("Dinofos"))
                {
                    //throw new Exception("SafeBoat.mmra no longer works in actorizer 1.16, \n remove the file from MMR/actors and start a new seed.");
                    continue;
                }

                if (_randomized.Settings.Character == Character.AdultLink && filePath.Contains("Anope.mmra"))
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
                                replacementEnemySearch = new Actor(injectedActor, Path.GetFileName(filePath));
                                ReplacementCandidateList.Add(replacementEnemySearch);
                            }

                            if (injectedActor.ObjectId <= 3)
                            {
                                var freeCandidateSearch = FreeCandidateList.Find(act => act.ActorId == injectedActor.ActorId);
                                if (freeCandidateSearch == null)
                                {
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
                                RomData.MMFileList[newFID].IsReadOnly = true;
                                // injectedActor.overlayBin = overlayData; // we dont save bin if its a previous file
                            }

                            // wait isnt this bad? we dont compress the actor again? is this just a work around?
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
            // this is called from romutils.cs right before we build the rom
            /// if overlays have grown, we need to modify their overlay table to use the right values for the new files
            /// every time you move an overlay you need to relocate the vram addresses, so instead of shifting all of them
            ///  we just move the new larger files to the end and leave a hole behind for now

            // TODO can we _detect_ this value by looking at rando is already doing?
            // 0x80C260A0 <- known last vanilla vram value
            const uint theEndOfTakenVRAM = 0x80D00000; // changed to make it visually obvious this is a new actor
            // can't even remember why I raised it
            //const uint theEndOfTakenVRAM = 0x80CA0000; // TODO change back to lower
            //const int theEndOfTakenVROM = 0x03100000; // 0x02EE7XXX <- actual
            // maybe if I set it longer away I can skip the extra samples getting corrupted, probably not
            //const int theEndOfTakenVROM = 0x03400000; // stable, was used for like 30 actorizer versions
            const int theEndOfTakenVROM = 0x05000000; // stable, was used for like 30 actorizer versions
            // WARNING: 0x03880000 is above us, which is Rebbacus's overlay file that was moved, we need to keep that in mind

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

        public static void ReadActors(OutputSettings outputSettings, CosmeticSettings cosmeticSettings, Models.RandomizedResult randomized)
        {
            _seedRNG = new Random(randomized.Seed);
            _randomized = randomized;
            _outputSettings = outputSettings;
            _cosmeticSettings = cosmeticSettings;
            _syncedLog = new StringBuilder();

            PrepareEnemyLists();
            JunkDetection.PrepareJunkItems(randomized);


            SceneUtils.ReadExternalSceneFiles();
            SceneUtils.ReadSceneTable();
            SceneUtils.GetSceneHeaders();
            SceneUtils.GetMaps();
            SceneUtils.GetMapHeaders();
            SceneUtils.GetActors();

            #if DEBUG
            var shop = RomData.SceneList.Find(s => s.SceneEnum == GameObjects.Scene.BombShop);
            var clock = shop.Maps[0].Actors[4];
            watchActor = clock;
            #endif

            EnemizerEarlyFixes(); // before we randomize ; moved up
        }

        private static Actor watchActor = null;

        public static void ShuffleEnemies()
        {
            try
            {
                DateTime enemizerStartTime = DateTime.Now;

                ScanForMMRA(directory: "actors");
                InjectNewActors();

                // for dingus that want moonwarp, re-enable dekupalace
                var SceneSkip = new GameObjects.Scene[] { //};
                    GameObjects.Scene.SakonsHideout // issue: the whole gaunlet is one long room, with two clear enemy room puzles
                    };// , GameObjects.Scene.DekuPalace };

                EnemizerItemFixes(); // before we randomize ; moved up

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
                int seed = _randomized.Seed;

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

                EnemizerLateFixes(); // fix IF randomized
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
                    sw.Write(_syncedLog.ToString());
                    sw.Write("Enemizer version: Isghj's Actorizer Test 97.3\n");
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
            this.newMapList = new List<MapEnemiesCollection>();
            // I like foreach better but its waaaay slower
            for (int m = 0; m < scene.Maps.Count; ++m)
            {
                var map = scene.Maps[m];

                var newObjList = newObjects[m];
                this.newMapList.Add(new MapEnemiesCollection(map.Actors, newObjList, scene));
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
