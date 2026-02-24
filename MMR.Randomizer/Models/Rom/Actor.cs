using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using System.Linq;
using MMR.Randomizer.Utils;
using MMR.Randomizer.Models.Vectors;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.Models.Rom
{
    [System.Diagnostics.DebuggerDisplay("[{Name}][{ActorId.ToString(\"X4\")}] from [{OldName}][{OldVariant.ToString(\"X4\")}]")]
    public class Actor
    {
        // this is instance data, per actor, per scene.
        //  sometimes built from romdate, sometimes generated from enums
        // if you want metadata about actor types use the enum Gameobjects.Actor

        public string Name = ""; // for debug mostly, got real sick of looking up each and every actor index
        public string OldName = ""; // for debug mostly, got real sick of looking up each and every actor index
        [System.Diagnostics.DebuggerDisplay("{ActorId.ToString(\"X3\")}")]
        public int ActorId; // in-game actor list index
        public GameObjects.Actor ActorEnum; // enumerator with metadata about the actor and actor extensions
        public GameObjects.Actor OldActorEnum; // enumerator with metadata about the actor and actor extensions
        [System.Diagnostics.DebuggerDisplay("{ObjectId.ToString(\"X3\")}")]
        public int ObjectId; // in-game object list index
        public int OldObjectId; // in-game object list index
        public int ActorIdFlags; // contain rotation ignoring flags, that convert xyz rotation for parameter use instead
        public List<int> Variants { get; private set; }
        //public List<int> Variants = new List<int> { 0 };
        public List<List<int>> SortedVariants = null;
        public int OldVariant;
        public bool MustNotRespawn = false;
        public ActorType Type; 
        public bool IsCompanion = false;
        public bool previouslyMovedCompanion = false;
        public bool Blockable = true;

        // used for vanilla actors (not for replacements)
        public int ActorSize; // todo
        public int ObjectSize; // read by enemizer at scene actor reading
        public (int poly, int vert) DynaLoad =  (0, 0);  // dyna load per actor, can be overwritten by injected actor
        public int Room;           // the room the actor is in in the scene
        public int RoomActorIndex; // the actor index of the room the actor is in
        public vec16 Position = new vec16();
        public vec16 Rotation = new vec16();

        public List<GameObjects.Scene> SceneExclude = new List<GameObjects.Scene>();

        //public int sceneID; // do we still need this?
        public bool modified = false;

        // we no longer want to pull this stuff every actor, we want this stuff static
        // also because we might want to add it it with actor inject, cant inject into enum

        public InjectedActor InjectedActor;
        public OnlyOneActorPerRoom OnlyOnePerRoom;
        public List<GameObjects.Scene> BlockedScenes = new List<GameObjects.Scene>();
        public List<int> RespawningVariants = new List<int>();
        public List<int> UnplaceableVariants = new List<int>();
        public List<VariantsWithRoomMax> VariantsWithRoomMax = new List<VariantsWithRoomMax>();

        public Actor() { } // default, used when building from scene/room read

        // TODO we should consolidate all of this actor constructor/update code

        public Actor(GameObjects.Actor actor, InjectedActor injectedData = null)
        {
            // converted from enum, used for building replacement candidate actors

            this.Name = this.OldName = actor.ToString();
            this.ActorId =  (int) actor;
            this.ActorEnum = this.OldActorEnum = actor;
            this.ObjectId = this.OldObjectId = actor.ObjectIndex();
            this.ObjectSize = ObjUtils.GetObjSize(actor.ObjectIndex());
            this.Rotation = new vec16();

            this.SceneExclude = actor.ScenesRandomizationExcluded();
            this.BlockedScenes = actor.BlockedScenes();
            this.SortedVariants = BuildVariantList(actor);
            this.Variants = SortedVariants.SelectMany(u => u).ToList();
            this.VariantsWithRoomMax = actor.GetAttributes<VariantsWithRoomMax>().ToList();
            this.OnlyOnePerRoom = actor.GetAttribute<OnlyOneActorPerRoom>();
            this.RespawningVariants = actor.RespawningVariants();
            this.UnplaceableVariants = actor.GetUnPlacableVariants();

            var dynaProperties = actor.GetAttribute<DynaAttributes>();
            if (dynaProperties != null)
            {
                this.DynaLoad.poly = dynaProperties.Polygons;
                this.DynaLoad.vert = dynaProperties.Verticies;
            }

            // missing injected actor stuff
            if (injectedData != null)
                this.UpdateActor(injectedData);
        }

        public Actor(InjectedActor injected, string name)
        {
            // create actor from injected actor, for brand new actors

            this.Name = this.OldName = name;
            this.ActorId = injected.ActorId;
            this.ObjectId = injected.ObjectId;

            // this might break things, but required for actor placement blocking
            this.ActorEnum = this.OldActorEnum = (GameObjects.Actor) injected.ActorId;
            this.BlockedScenes = this.ActorEnum.BlockedScenes();

            this.SortedVariants = new List<List<int>>()
            {
                injected.waterVariants,
                injected.waterTopVariants,
                injected.waterBottomVariants,
                injected.groundVariants,
                injected.flyingVariants,
                injected.wallVariants,
                injected.perchingVariants,
                new List<int>(),
                new List<int>(),
            };

            // wasnt there a list of lists to static list we had?
            this.Variants = SortedVariants.SelectMany(x => x).ToList();
            this.VariantsWithRoomMax = injected.limitedVariants;
            this.UnplaceableVariants = this.ActorEnum.GetUnPlacableVariants();
            this.OnlyOnePerRoom = injected.onlyOnePerRoom;
            this.InjectedActor = injected;
            
            var dynaProperties = this.ActorEnum.GetAttribute<DynaAttributes>();
            if (dynaProperties != null)
            {
                this.DynaLoad.poly = dynaProperties.Polygons;
                this.DynaLoad.vert = dynaProperties.Verticies;
            }

            if (injected.DynaLoad.poly != -1) // custom new dyna
            {
                this.DynaLoad.poly = injected.DynaLoad.poly;
            }
            if (injected.DynaLoad.vert != -1) // custom new dyna
            {
                this.DynaLoad.vert = injected.DynaLoad.vert;
            }

        }

        public static List<List<int>> BuildVariantList(GameObjects.Actor actor)
        {
            // creates a list of lists of variants per type from attributes in the enumerator
            var wattr = actor.GetAttribute<WaterVariantsAttribute>();
            var wlist = (wattr == null) ? new List<int>() : wattr.Variants;
            var wtattr = actor.GetAttribute<WaterTopVariantsAttribute>();
            var wtlist = (wtattr == null) ? new List<int>() : wtattr.Variants;
            var wbattr = actor.GetAttribute<WaterBottomVariantsAttribute>();
            var wblist = (wbattr == null) ? new List<int>() : wbattr.Variants;
            var gattr = actor.GetAttribute<GroundVariantsAttribute>();
            var glist = ((gattr == null) ? new List<int>() : gattr.Variants);
            var fattr = actor.GetAttribute<FlyingVariantsAttribute>();
            var flist = ((fattr == null) ? new List<int>() : fattr.Variants);
            var wlattr = actor.GetAttribute<WallVariantsAttribute>();
            var wllist = ((wlattr == null) ? new List<int>() : wlattr.Variants);
            var perattr = actor.GetAttribute<PerchingVariantsAttribute>();
            var perlist = ((perattr == null) ? new List<int>() : perattr.Variants);
            var celattr = actor.GetAttribute<CeilingVariantsAttribute>();
            var cellist = ((celattr == null) ? new List<int>() : celattr.Variants);
            var pattr = actor.GetAttribute<PathingVariantsAttribute>();
            var plist = ((pattr == null) ? new List<int>() : pattr.Variants);
            var newList = new List<List<int>>()
            {
                wlist,
                wtlist,
                wblist,
                glist,
                flist,
                wllist,
                perlist,
                cellist,
                plist
            };
            return newList;
        }

        public Actor CopyActor()
        {
            // deep copy, well, at least some of these will be modified

            Actor newActor = new Actor();

            newActor.Name = this.Name;
            newActor.ActorId = this.ActorId;
            newActor.ActorEnum = this.ActorEnum;
            newActor.OldActorEnum = this.OldActorEnum;
            newActor.ObjectId = this.ObjectId;
            newActor.OldObjectId = this.OldObjectId;
            newActor.ObjectSize = this.ObjectSize;
            newActor.Rotation = this.Rotation;

            newActor.SceneExclude = this.SceneExclude;
            newActor.BlockedScenes = this.BlockedScenes;

            var newVariantsList = new List<List<int>>();
            for(int i = 0; i < this.SortedVariants.Count; i++) // per variant type (water, ground, pathing, ect)
            {
                var specificVariantList = this.SortedVariants[i];
                var newVariantList = new List<int>();
                for (int j = 0; j < specificVariantList.Count; j++) // per variant in type
                {
                    newVariantList.Add(specificVariantList[j]); 
                }
                newVariantsList.Add(newVariantList);
            }
            newActor.SortedVariants = newVariantsList;

            Debug.Assert(newActor.SortedVariants != null);

            //newActor.Variants = newActor.SortedVariants.SelectMany(u => u).ToList(); // might as well start with all
            newActor.Variants = this.Variants; // worried that this was previously separate for a reason, dont rebuild
            newActor.OnlyOnePerRoom = this.OnlyOnePerRoom;
            newActor.VariantsWithRoomMax = this.VariantsWithRoomMax.ToList();

            if (this.RespawningVariants != null)
            {
                var newRespawningVariants = new List<int>();
                for (int i = 0; i < this.RespawningVariants.Count; i++)
                {
                    newRespawningVariants.Add(this.RespawningVariants[i]);
                }
                newActor.RespawningVariants = newRespawningVariants;
            }

            newActor.InjectedActor = this.InjectedActor;
            newActor.UnplaceableVariants = this.UnplaceableVariants;

            newActor.DynaLoad = this.DynaLoad;

            return newActor;
        }


        public static List<Actor> CopyActorList(List<Actor> originalList)
        {
            // there's probably a newer c# lamda/predicate means of doing this but cant think of it right now

            var actorList = new List<Actor>();
            for(int i = 0; i < originalList.Count; i++) // for faster than foreach in c#
            {
                var newActor = originalList[i].CopyActor();
                actorList.Add(newActor); // we want to COPY the object, use Actor contructor
            }

            return actorList;
        }

        public void ChangeActor(GameObjects.Actor newActorType, int vars = -1, bool modifyOld = false)
        {
            /// this is used to fix slots for actor/enemizer before randomization
            ///   ergo: to tweak what we read from the ROM so randomization works how it's intended later

            this.ActorEnum   = newActorType;
            this.Name        = newActorType.ToString();
            this.ActorId     = (int)newActorType;
            this.ObjectId    = newActorType.ObjectIndex();

            if (modifyOld)
            {
                this.OldVariant     = vars;
                this.OldActorEnum   = newActorType;
                this.OldName        = newActorType.ToString();
                this.OldObjectId    = this.ObjectId;

                this.SortedVariants = BuildVariantList(newActorType);
                this.Variants = SortedVariants.SelectMany(u => u).ToList();
                this.ActorIdFlags &= ~0xE000; // the flags that specify that the actor should ignore rotations, thats going to be actor specific not spawn specific
            }

            if (Variants.Count == 0)
            {
                Variants = new List<int>(1){ 0 };
            }

            if (vars != -1)
            {
                Variants[0] = vars;
            }

            this.OnlyOnePerRoom = newActorType.GetAttribute<OnlyOneActorPerRoom>();

            // if the new is injected, pull from injected
            // TODO? right now we dont have any actors we are injecting that change vanilla behavior, in fact we want to avoid
            // if not injected, pull from enum
            var dynaProperties = this.ActorEnum.GetAttribute<DynaAttributes>();
            if (dynaProperties != null)
            {
                this.DynaLoad.poly = dynaProperties.Polygons;
                this.DynaLoad.vert = dynaProperties.Verticies;
            }
            else
            {
                this.DynaLoad.poly = 0; // none, values are to be reset
                this.DynaLoad.vert = 0;
            }
        }

        public void ChangeActor(Actor otherActor, int vars = -1)
        {
            /// this is used by actor/enemizer during normal randomization
            ///   ergo: Randomizing one actor into another, and Trimming an actor if too many

            this.ActorEnum      = otherActor.ActorEnum;
            this.Name           = otherActor.Name;
            this.ActorId        = otherActor.ActorId;
            this.ObjectId       = otherActor.ObjectId;
            //this.SortedVariants    = otherActor.SortedVariants; // other parts of the rando assume this is static

            if (vars != -1)
            {
                Variants[0] = vars;
            }

            this.DynaLoad = otherActor.DynaLoad;

            this.OnlyOnePerRoom = otherActor.OnlyOnePerRoom;

            if (otherActor.InjectedActor != null)
                this.UpdateActor(otherActor.InjectedActor);

            this.VariantsWithRoomMax = otherActor.VariantsWithRoomMax.ToList();
        }

        public void UpdateActor(InjectedActor injectedActor)
        {
            /// this function exists for actor injection

            this.InjectedActor = injectedActor;

            this.OnlyOnePerRoom = injectedActor.onlyOnePerRoom;

            // should we add or replace variants? for now we add
            this.Variants.AddRange(injectedActor.waterVariants);
            this.Variants.AddRange(injectedActor.waterBottomVariants);
            this.Variants.AddRange(injectedActor.waterTopVariants);
            this.Variants.AddRange(injectedActor.groundVariants);
            this.Variants.AddRange(injectedActor.flyingVariants);
            this.Variants.AddRange(injectedActor.wallVariants);
            this.Variants.AddRange(injectedActor.perchingVariants);
            this.Variants = this.Variants.Distinct().ToList(); // if variant copies with limits we can double dip, also bloats loops

            if (this.RespawningVariants == null)
            {
                this.RespawningVariants = new List<int>();
            }
            this.RespawningVariants.AddRange(injectedActor.respawningVariants);

            if (injectedActor.DynaLoad.poly != -1)
            {
                this.DynaLoad.poly = injectedActor.DynaLoad.poly;
            }
            if (injectedActor.DynaLoad.vert != -1)
            {
                this.DynaLoad.vert = injectedActor.DynaLoad.vert;
            }

            void AddToSpecificSubtype(ActorType type, List<int> newList)
            {
                var variantSubList = this.SortedVariants[(int)type - 1];
                variantSubList.AddRange(newList.Except(variantSubList));
            }

            this.VariantsWithRoomMax.AddRange(injectedActor.limitedVariants);
            AddToSpecificSubtype(ActorType.Water, injectedActor.waterVariants);
            AddToSpecificSubtype(ActorType.WaterTop, injectedActor.waterTopVariants);
            AddToSpecificSubtype(ActorType.WaterBottom, injectedActor.waterBottomVariants);
            AddToSpecificSubtype(ActorType.Ground, injectedActor.groundVariants);
            AddToSpecificSubtype(ActorType.Flying, injectedActor.flyingVariants);
            AddToSpecificSubtype(ActorType.Wall, injectedActor.wallVariants);
            AddToSpecificSubtype(ActorType.Perching, injectedActor.perchingVariants);
        }

        // TODO: I forgot this exists, should be renamed and other code agressively use this
        // change actor is more likely to be used
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ChangeVariant(int variant)
        {
            /// deep change: changes old variant as well
            this.OldVariant = this.Variants[0] = variant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetVariants(List<int> variants)
        {
            // this is here so I can watch for variant corruption, because not sure how to breakpoint
            // currentScene is there for debugging, 
            this.Variants = variants;
        }

        public void TrimVariantsList()
        {
            if (this.SortedVariants != null)
            {
                for (int c = 0; c < this.SortedVariants.Count; c++)
                {
                    this.SortedVariants[c] = this.SortedVariants[c].Except(this.Variants).ToList();
                }
            }
        }

        // should this function also be checking for other compatibility issues like respawning? currently elsewhere
        public List<int> CompatibleVariants(Actor otherActor, Random rng, bool roomIsClearPuzzleRoom = false)
        {
            /// with mixed types, typing could be messy, keep it hidden here
            /// EG. like like can spawn on the sand (land), but also on the bottom of GBC (water floor)
            /// so we need to know what type the actor we are replacing is, and check if any otherActor variants can replace it

            if (this.SortedVariants == null || otherActor.SortedVariants == null)
            {
                throw new Exception("Compare Variants: broken actor variants listoflist");
            }

            // randomly select a type, check if they have matching types

            var listOfVariantTypes = Enum.GetValues(typeof(ActorType)).Cast<ActorType>().ToList();
            listOfVariantTypes.Remove(ActorType.Unset);
            // we randomize the type list because some actors have multiple, if we didnt randomize it would always default to first sequential type
            listOfVariantTypes = listOfVariantTypes.OrderBy(u => rng.Next()).ToList();

            // TODO attempt to make multiple-type compatible variant list return instead of just one

            // sequentially traverse random types
            foreach (var randomVariantType in listOfVariantTypes)
            {
                // pull the variants for our random type
                #if DEBUG
                var DEBUG_type = randomVariantType - 1;
                #endif
                List<int> ourVariants   = this.SortedVariants[ (int) randomVariantType - 1].ToList();
                List<int> theirVariants = otherActor.SortedVariants[ (int) randomVariantType - 1].ToList();

                if (ourVariants.Count == 0 && theirVariants.Count == 0) continue;

                bool ourVariantMatches = ourVariants.Contains(this.OldVariant);

                // large chance of pathing enemies allowing ground or flying replacements for pathing
                if (randomVariantType == ActorType.Pathing  && ourVariantMatches && theirVariants.Count == 0 && rng.Next(100) < 80)
                {
                    var possibleReplacementTypeList = new List<ActorType> { ActorType.Flying, ActorType.Ground };
                    possibleReplacementTypeList = possibleReplacementTypeList.OrderBy(u => rng.Next()).ToList();

                    foreach( var type in possibleReplacementTypeList)
                    {
                        theirVariants = otherActor.SortedVariants[(int)type - 1];

                        if (theirVariants.Count != 0) break;
                    }
                }

                // some chance of ground enemies allowing flying replacements
                if (randomVariantType == ActorType.Ground
                    && ourVariantMatches && rng.Next(100) < 45)
                {
                    var theirFlyingVariants = otherActor.SortedVariants[ (int) ActorType.Flying - 1];
                    if (theirFlyingVariants.Count != 0)
                    {
                        theirVariants.AddRange(theirFlyingVariants);
                    }
                }

                // we allow regular old water types on bottom or top, fish can swim at any level for instance
                // TODO we should allow top and bottom most of the time, we just need to height adjust
                if ((randomVariantType == ActorType.WaterBottom || randomVariantType == ActorType.WaterTop)
                    && ourVariantMatches)
                {
                    var theirWaterVariants = otherActor.SortedVariants[(int)ActorType.Water - 1];
                    if (theirWaterVariants.Count != 0)
                    {
                        theirVariants.AddRange(theirWaterVariants);
                    }
                }

                // we allow flying on perching and cieling types because there are so few perching/ceiling actors and perching locations
                if ((randomVariantType == ActorType.Perching || (randomVariantType == ActorType.Ceiling && rng.Next(100) < 65))
                    && ourVariantMatches)
                {
                    var theirFlyingVariants = otherActor.SortedVariants[(int)ActorType.Flying - 1];
                    if (theirFlyingVariants.Count != 0)
                    {
                        theirVariants.AddRange(theirFlyingVariants);
                    }
                }

                // if we dont have the required variants still, even with optional substitution above, we skip to next type
                if (ourVariants.Count == 0 && theirVariants.Count == 0) continue;
                
                var compatibleVariants = theirVariants;

                // make sure their variants aren't un-placable either
                var zeroPlacementVarieties = otherActor.UnplaceableVariants;
                if (zeroPlacementVarieties != null)
                {
                    //compatibleVariants = compatibleVariants.FindAll(u => !zeroPlacementVarieties.Contains(u));
                    compatibleVariants.RemoveAll(u => zeroPlacementVarieties.Contains(u));
                }

                if (compatibleVariants.Count == 0 || !ourVariants.Contains(this.OldVariant)) continue;

                if (this.Blockable == false)
                {
                    if (otherActor.ActorEnum.GetAttribute<BlockingVariantsAll>() != null)
                    {
                        return null; // test actor is always blocking, oldactor cannot be blocked, continue to next actor
                    }
                    else
                    {
                        compatibleVariants = otherActor.FilterBlockingTypes(compatibleVariants);
                    }
                }

                var respawningVariants = otherActor.RespawningVariants;
                if ((this.MustNotRespawn || roomIsClearPuzzleRoom) && respawningVariants != null)
                {
                    compatibleVariants.RemoveAll(variant => respawningVariants.Contains(variant));
                }

                if (compatibleVariants.Count != 0)
                    return compatibleVariants; // return with first compatible variants, not all
                
            }

            return null; // none found
        }

        // these are here because there are two actortypes now, TODO merge
        // we need to know the NEW ENUM TOO damn it
        public List<int> GetGroundVariants()
        {
            return this.SortedVariants[(int)ActorType.Ground - 1];
        }
        public List<int> GetWaterVariants()
        {
            return this.SortedVariants[(int)ActorType.Water - 1];
        }
        public List<int> GetWaterBottomVariants()
        {
            return this.SortedVariants[(int)ActorType.WaterBottom - 1];
        }
        public List<int> GetWaterTopVariants()
        {
            return this.SortedVariants[(int)ActorType.WaterTop - 1];
        }

        public List<int> GetFlyingVariants()
        {
            return this.SortedVariants[(int)ActorType.Flying - 1];
        }
        public List<int> GetCeilingVariants()
        {
            return this.SortedVariants[(int)ActorType.Ceiling - 1];
        }

        public List<int> GetWallVariants()
        {
            return this.SortedVariants[(int)ActorType.Wall - 1];
        }

        public List<int> GetPathingVariants()
        {
            return this.SortedVariants[(int)ActorType.Pathing - 1];
        }

        public bool IsNewChoiceFlying()
        {
            // check the enum first, we should always have one 
            var enumResult = this.ActorEnum.isFlyingVariant(this.Variants[0]);
            if (enumResult) return true;

            // else, check the injected actor data
            if (this.InjectedActor != null)
            {
                return this.InjectedActor.flyingVariants.Contains(this.Variants[0]);
            }

            return false;
        }


        // todo remove this as it should be built at enum->actor time
        public List<int> UnkillableVariants()
        {
            // TODO finish converting this from enum to actor base type

            var actor = this.ActorEnum;
            if (this.InjectedActor != null) // injected actors have different rules
            {
                if (this.InjectedActor.unkillableAttr != null)
                {                    // todo: specific injected actor unkillable variants
                    return this.Variants;
                }
                else
                {
                    return null;
                }
            }

            if (actor != GameObjects.Actor.NULL) // vanilla enemies
            {
                if (actor.GetAttribute<UnkillableAllVariantsAttribute>() != null) // all are unkillable
                {
                    return this.Variants;
                }
                else
                {
                    return actor.GetAttribute<UnkillableVariantsAttribute>()?.Variants;
                }
            }
            
            return this.Variants;
        }

        public List<int> KillableVariants(List<int> acceptableVariants = null)
        {
            var allRespawning = this.ActorEnum.GetAttribute<RespawningAllVariantsAttribute>();
            if (allRespawning != null)
            {
                return new List<int>();
            }

            var killableVariants = acceptableVariants != null ? acceptableVariants : this.Variants;
            var unkillableVariants = this.UnkillableVariants();
            var respawningVariants = this.RespawningVariants;
            if (unkillableVariants != null && unkillableVariants.Count > 0)
            {
                killableVariants.RemoveAll(u => unkillableVariants.Contains(u));
            }
            if (respawningVariants != null && respawningVariants.Count > 0)
            {
                killableVariants.RemoveAll(u => respawningVariants.Contains(u));
            }
            return killableVariants;
        }

        public int VariantMaxCountPerRoom(int queryVariant = 0x0000)
        {
            if (this.OnlyOnePerRoom != null)
            {
                return 1;
            }

            var limitedVariants = this.VariantsWithRoomMax;
            if (limitedVariants != null)
            {
                foreach (var variant in limitedVariants)
                {
                    if (variant.Variants.Contains(queryVariant))
                    {
                        return variant.RoomMax;
                    }
                }
            }

            return -1; // no restriction
        }

        public bool NoPlacableVariants()
        {
            // sometimes you want to check for actors that have variants that are allowed to be placed 0 times
            // some are bugged, some you want pulled into the rando but not placed anywhere

            if (SortedVariants == null || SortedVariants.Count == 0)
            {
                return true;
            }
            //var variantCount = SortedVariants[0].Count + SortedVariants[1].Count + SortedVariants[2].Count + SortedVariants[3].Count + SortedVariants[4].Count;
            var variantCount = SortedVariants.Sum(childList => childList.Count);
            if (variantCount == 0)
            {
                return true;
            }

            foreach (var variantList in SortedVariants)
            {
                for (int i = 0; i < variantList.Count(); i++)
                {
                    var variant = variantList[i];
                    var max = this.VariantMaxCountPerRoom(variant);
                    // if -1, no max. if 1 or greater, does not quality as zero
                    if (max != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<int> CreditsBlockedVariants()
        {
            var allCreditsVariantsBlockedAttr = this.ActorEnum.GetAttribute<CreditsBlockedAllVariantsAttribute>();
            if (allCreditsVariantsBlockedAttr != null)
            {
                return this.Variants;
            }

            var creditsBlockedVariants = this.ActorEnum.GetAttribute<CreditsBlockedVariantsAttribute>();
            if (creditsBlockedVariants != null)
            {
                return creditsBlockedVariants.Variants;
            }


            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasVariantsWithRoomLimits()
        {
            return VariantsWithRoomMax != null || OnlyOnePerRoom != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetTimeFlags()
        {
            // 10 time flags, day and night for days 0 through 4, split in the flags section of the rotation shorts
            return ((this.Rotation.x & 0x3) << 7) | (this.Rotation.z & 0x7F);
        }

        // TODO merge these two, but for right now they are used differently in different places
        public List<int> RemoveBlockingTypes()
        {
            var blockingTypeVariants = this.ActorEnum.GetAttribute<BlockingVariantsAttribute>();
            if (blockingTypeVariants != null)
            {
                this.Variants = this.Variants.Where(var => ! blockingTypeVariants.Variants.Contains(var)).ToList();
            }

            return this.Variants;
        }

        public List<int> RemoveEasyEmemies()
        {
            var allDifficult = this.ActorEnum.GetAttribute<DifficultAllVariantsAttribute>();
            if (allDifficult != null) return this.Variants;

            var difficultTypeVariants = this.ActorEnum.GetAttribute<DifficultVariantsAttribute>();
            if (difficultTypeVariants != null)
            {
                this.Variants = this.Variants.Where(var => !difficultTypeVariants.Variants.Contains(var)).ToList();
            }

            return this.Variants;
        }


        public List<int>FilterBlockingTypes(List<int> PreviousCandidateVariants)
        {
            var blockingTypeVariants = this.ActorEnum.GetAttribute<BlockingVariantsAttribute>();
            if (blockingTypeVariants != null)
            {
                PreviousCandidateVariants = PreviousCandidateVariants.Where(var => !blockingTypeVariants.Variants.Contains(var)).ToList();
            }

            return PreviousCandidateVariants;
        }


        public int GetPlacementWeight(int baseline = 100)
        {
            /// returns a weight for a given actor to be placed in a scene

            var weightAttribute = this.ActorEnum.GetAttribute<PlacementWeight>();
            if (weightAttribute != null)
            {
                return weightAttribute.Weight;
            }

            // if baseline is not 100, adjust values to 100

            return 100; // default percent: 100%
        }

        public void ChangeXRotation(int newRotation)
        {
            this.Rotation.x = ActorUtils.MergeRotationAndFlags(newRotation, flags: this.Rotation.x);
        }
        public void ChangeYRotation(int newRotation)
        {
            this.Rotation.y = ActorUtils.MergeRotationAndFlags(newRotation, flags: this.Rotation.y);
        }
        public void ChangeZRotation(int newRotation)
        {
            this.Rotation.z = ActorUtils.MergeRotationAndFlags(newRotation, flags: this.Rotation.z);
        }

        public int GetRemovalChance()
        {
            /// actors/enemies shouldnt always be removed, there is now a chance to be intentionally left vanilla

            var removalChanceAttr = this.OldActorEnum.GetAttribute<RemovalChance>();
            if (removalChanceAttr != null)
            {
                return removalChanceAttr.Weight;
            }

            return 100; // default
        }

        public void UpdateBlockable(GameObjects.Scene scene)
        {
            /// moving from actor extension
            /// checks if a specific actor in a scene at scene read time is blockable

            // quickly check if the scene cares
            var sceneBlockingConditions = scene.GetAttributes<EnemizerSceneBlockSensitiveAttribute>().ToList();

            var validCondition = sceneBlockingConditions.Find(actor => actor.OriginalEnemy == this.ActorEnum);
            if (validCondition != null)
            {

                if (validCondition.BlockedChecks != null)
                {
                    foreach (var item in validCondition.BlockedChecks.ToList())
                    {
                        // removed by request of beta testers: having actors block checks because the check is junk is too bug-like and annoying
                        //if (Enemies.IsActorizerJunk(item))
                        //{
                        //    validCondition.BlockedChecks.Remove(item);
                        //}
                    }

                    if (validCondition.BlockedChecks.Count == 0)
                    {
                        // to be in here, you have to have had a list, now that its empty, we consider all items junk, and this actor can be blockable
                        this.Blockable = true;
                        return;
                    }
                }

                /// this scene has this actor as a block statement
                if (validCondition.SpecificMapIndexes.Count == 1 && validCondition.SpecificMapIndexes[0] == -1) // -1 meaning we dont care
                {
                    this.Blockable = false;
                    return;
                }
                // else, check if this is one of the blocked positions specifically listed
                var sensitivePositions = validCondition.SpecificMapIndexes;
                if (sensitivePositions.Contains(this.RoomActorIndex))
                {
                    this.Blockable = false;
                    return;
                }
            }

            this.Blockable = true; // no block reasons listed, all conditions cleared
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ChangedToNewActor(GameObjects.Actor actor)
        {
            // is this actor changed to a new specific actor, and wasnt previously the same actor type
            return this.ActorEnum == actor && this.OldActorEnum != actor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CurrentVariantIsType(ActorType type)
        {
            // "is this actor, after randomization, of a certain type" is tested too much, might as well separate it

            var listOfVariantsThatMatchType = this.SortedVariants[ (int) type - 1]; // type list starts with "unset"
            return listOfVariantsThatMatchType != null && listOfVariantsThatMatchType.Contains(this.Variants[0]);
        }

        public bool OldVariantIsType(ActorType type)
        {
            // "is this actor, before randomization, of a certain type" is tested too much, might as well separate it

            var listOfVariantsThatMatchType = this.SortedVariants[(int)type - 1]; // type list starts with "unset"
            return listOfVariantsThatMatchType != null && listOfVariantsThatMatchType.Contains(this.OldVariant);
        }


    }
}
