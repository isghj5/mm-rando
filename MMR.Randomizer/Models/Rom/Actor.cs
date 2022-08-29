using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using System.Linq;
using MMR.Randomizer.Utils;
using MMR.Randomizer.Models.Vectors;
using System.Collections.Generic;
using System;


namespace MMR.Randomizer.Models.Rom
{
    [System.Diagnostics.DebuggerDisplay("[{Name}][{ActorID}]")]
    public class Actor
    {
        // this is instance data, per actor, per scene.
        //  sometimes built from romdate, sometimes generated from enums
        // if you want metadata about actor types use the enum Gameobjects.Actor

        public string Name = ""; // for debug mostly, got real sick of looking up each and every actor index
        public string OldName = ""; // for debug mostly, got real sick of looking up each and every actor index
        [System.Diagnostics.DebuggerDisplay("{ActorID.ToString(\"X3\")}")]
        public int ActorID; // in-game actor list index
        public GameObjects.Actor ActorEnum; // enumerator with metadata about the actor and actor extensions
        public GameObjects.Actor OldActorEnum; // enumerator with metadata about the actor and actor extensions
        [System.Diagnostics.DebuggerDisplay("{ObjectID.ToString(\"X3\")}")]
        public int ObjectID; // in-game object list index
        public int OldObjectID; // in-game object list index
        public int ActorIDFlags; // we just want to keep them when re-writing, but I'm not sure they even matter
        public List<int> Variants = new List<int> { 0 };
        public List<List<int>> AllVariants = null;
        public int OldVariant;
        public bool MustNotRespawn = false;
        public ActorType Type; 
        public bool IsCompanion = false;
        public bool previouslyMovedCompanion = false;

        // used for vanilla actors (not for replacements)
        public int ActorSize; // todo
        public int ObjectSize; // read by enemizer at scene actor reading
        public int Room;           // this specific actor, which map/room was it in
        public int RoomActorIndex; // the index of this actor in its room's actor list
        //public int Stationary; // Deathbasket used to use this, I dont see the point except around water
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

        public Actor(GameObjects.Actor actor)
        {
            // converted from enum, used for building replacement candidate actors

            this.Name = actor.ToString();
            this.ActorID = (int)actor;
            this.ActorEnum = actor;
            this.ObjectID = this.OldObjectID = actor.ObjectIndex();
            this.ObjectSize = ObjUtils.GetObjSize(actor.ObjectIndex());
            this.Rotation = new vec16();

            this.SceneExclude = actor.ScenesRandomizationExcluded();
            this.BlockedScenes = actor.BlockedScenes();
            this.AllVariants = BuildVariantList(actor);
            this.Variants = AllVariants.SelectMany(u => u).ToList();
            this.VariantsWithRoomMax = actor.GetAttributes<VariantsWithRoomMax>().ToList();
            this.OnlyOnePerRoom = actor.GetAttribute<OnlyOneActorPerRoom>();
            this.RespawningVariants = actor.RespawningVariants();
        }

        public Actor(InjectedActor injected, string name)
        {
            // create actor from injected actor, for brand new actors

            this.Name = this.OldName = name;
            this.ActorID = injected.actorID;
            this.ObjectID = injected.objID;
            // for now injected actors can only be of type ground
            this.AllVariants = new List<List<int>>()
            {
                new List<int>(),
                injected.groundVariants,
                injected.flyingVariants,
                new List<int>(),
                new List<int>(),
            };

            // wasnt there a list of lists to static list we had?
            this.Variants = injected.groundVariants.Concat(injected.flyingVariants).ToList();
            this.VariantsWithRoomMax = injected.limitedVariants;
            this.OnlyOnePerRoom = injected.onlyOnePerRoom;
            this.InjectedActor = injected;
        }

        public static List<List<int>> BuildVariantList(GameObjects.Actor actor)
        {
            // creates a list of lists of variants per type from attributes in the enumerator
            var wattr = actor.GetAttribute<WaterVariantsAttribute>();
            var wlist = (wattr == null) ? new List<int>() : wattr.Variants;
            var gattr = actor.GetAttribute<GroundVariantsAttribute>();
            var glist = ((gattr == null) ? new List<int>() : gattr.Variants);
            var fattr = actor.GetAttribute<FlyingVariantsAttribute>();
            var flist = ((fattr == null) ? new List<int>() : fattr.Variants);
            var wlattr = actor.GetAttribute<WallVariantsAttribute>();
            var wllist = ((wlattr == null) ? new List<int>() : wlattr.Variants);
            var pattr = actor.GetAttribute<PathingVariantsAttribute>();
            var plist = ((pattr == null) ? new List<int>() : pattr.Variants);
            var newList = new List<List<int>>()
            {
                wlist,
                glist,
                flist,
                wllist,
                plist
            };
            return newList;
        }

        public Actor CopyActor()
        {
            // deep copy, well, at least some of these will be modified

            Actor newActor = new Actor();

            newActor.Name = this.Name;
            newActor.ActorID = this.ActorID;
            newActor.ActorEnum = this.ActorEnum;
            newActor.ObjectID = this.ObjectID;
            newActor.ObjectSize = this.ObjectID;
            newActor.Rotation = this.Rotation;

            newActor.SceneExclude = this.SceneExclude;
            newActor.BlockedScenes = this.BlockedScenes;

            var newVariantsList = new List<List<int>>();
            for(int i = 0; i < this.AllVariants.Count; i++) // per variant type (water, ground, pathing, ect)
            {
                var specificVariantList = this.AllVariants[i];
                var newVariantList = new List<int>();
                for (int j = 0; j < specificVariantList.Count; j++) // per variant in type
                {
                    newVariantList.Add(specificVariantList[j]); 
                }
                newVariantsList.Add(newVariantList);
            }
            newActor.AllVariants = newVariantsList;

            newActor.Variants = newActor.AllVariants.SelectMany(u => u).ToList(); // might as well start with all
            newActor.OnlyOnePerRoom = this.OnlyOnePerRoom;
            newActor.VariantsWithRoomMax = this.VariantsWithRoomMax;

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
            this.ActorID     = (int)newActorType;
            this.ObjectID    = newActorType.ObjectIndex();


            if (modifyOld)
            {
                this.OldVariant     = vars;
                this.OldActorEnum   = newActorType;
                this.OldName        = newActorType.ToString();
                this.OldObjectID    = this.ObjectID;

                this.AllVariants = BuildVariantList(newActorType);
                this.Variants = AllVariants.SelectMany(u => u).ToList();
            }

            if (Variants.Count == 0)
            {
                Variants = new List<int>(1){ 0 };
            }

            if (vars != -1)
            {
                Variants[0] = vars;
            }

        }

        public void ChangeActor(Actor otherActor, int vars = -1)
        {
            /// this is used by actor/enemizer during normal randomization
            ///   ergo: Randomizing one actor into another, and Trimming an actor if too many

            this.ActorEnum      = otherActor.ActorEnum;
            this.Name           = otherActor.Name;
            this.ActorID        = otherActor.ActorID;
            this.ObjectID       = otherActor.ObjectID;

            if (vars != -1)
            {
                Variants[0] = vars;
            }

            this.InjectedActor = otherActor.InjectedActor;
        }

        // todo remove oldActorVariant no longer used
        public List<int> CompatibleVariants(Actor otherActor, Random rng)
        {
            /// with mixed types, typing could be messy, keep it hidden here
            /// EG. like like can spawn on the sand (land), but also on the bottom of GBC (water floor)
            /// so we need to know what type the actor we are replacing is, and check if any otherActor variants can replace it

            if (this.AllVariants == null || otherActor.AllVariants == null)
            {
                throw new Exception("Compare Variants: broken actor variants listoflist");
            }

            // randomly select a type, check if they have matching types

            // TODO figure out how to make this once
            var listOfVariantTypes = Enum.GetValues(typeof(ActorType)).Cast<ActorType>().ToList();
            listOfVariantTypes.Remove(ActorType.Unset);
            // we randomize the type list because some actors have multiple, if we didnt randomize it would always default to first sequential type
            listOfVariantTypes = listOfVariantTypes.OrderBy(u => rng.Next()).ToList();

            // TODO attempt to make multiple-type compatible variant list return instead of just one

            // sequentially traverse random types
            foreach (var randomVariantType in listOfVariantTypes)
            {
                // pull the variants for our random type
                List<int> ourVariants   = this.AllVariants[(int)randomVariantType - 1].ToList();
                List<int> theirVariants = otherActor.AllVariants[(int)randomVariantType - 1].ToList();

                // large chance of pathing enemies allowing ground or flying replacements
                if (randomVariantType == ActorType.Pathing
                    && ourVariants.Contains(this.OldVariant) && theirVariants.Count == 0 && rng.Next(100) < 80)
                {
                    // TODO could make this random
                    theirVariants = otherActor.AllVariants[(int) ActorType.Flying - 1];
                    if (theirVariants.Count == 0)
                    {
                        theirVariants = otherActor.AllVariants[(int) ActorType.Ground - 1];
                    }
                }

                // small chance of ground enemies allowing flying replacements
                if (randomVariantType == ActorType.Ground
                    && ourVariants.Contains(this.OldVariant) && rng.Next(100) < 30)
                {
                    var theirFlyingVariants = otherActor.AllVariants[(int)ActorType.Flying - 1];
                    if (theirVariants.Count != 0)
                    {
                        theirVariants.AddRange(theirFlyingVariants);
                    }
                }

                if (ourVariants.Count > 0 && theirVariants.Count > 0) // both have same type
                {
                    var compatibleVariants = theirVariants;

                    // make sure their variants aren't un-placable either
                    var zeroPlacementVarieties = otherActor.UnplaceableVariants;
                    if (zeroPlacementVarieties != null)
                    {
                        //compatibleVariants = compatibleVariants.FindAll(u => !zeroPlacementVarieties.Contains(u));
                        compatibleVariants.RemoveAll(u => zeroPlacementVarieties.Contains(u));
                    }

                    if (compatibleVariants.Count > 0 && ourVariants.Contains(this.OldVariant))
                    {
                        return compatibleVariants; // return with first compatible variants, not all
                    }
                }
            }

            return null; // none found
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

        public int VariantMaxCountPerRoom(int queryVariant)
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

            if (AllVariants == null || AllVariants.Count == 0)
            {
                return true;
            }
            var variantCount = AllVariants[0].Count + AllVariants[1].Count + AllVariants[2].Count + AllVariants[3].Count + AllVariants[4].Count;
            if (variantCount == 0)
            {
                return true;
            }

            foreach (var variantList in AllVariants)
            {
                for (int i = 0; i < variantList.Count(); i++)
                {
                    var max = this.VariantMaxCountPerRoom(i);
                    // if -1, no max. if 1 or greater, does not quality as zero
                    if (max != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool HasVariantsWithRoomLimits()
        {
            return VariantsWithRoomMax != null || OnlyOnePerRoom != null;
        }

        public int GetTimeFlags()
        {
            // 10 time flags, day and night for days 0 through 4, split in the flags section of the rotation shorts
            return ((this.Rotation.x & 0x3) << 7) | (this.Rotation.z & 0x7F);
        }

        public void UpdateActor(InjectedActor injectedActor)
        {
            /// this function exists for actor injection

            this.InjectedActor = injectedActor;

            this.OnlyOnePerRoom = injectedActor.onlyOnePerRoom;

            // should we add or replace variants? for now we add
            this.Variants.AddRange(injectedActor.groundVariants);
            this.Variants.AddRange(injectedActor.flyingVariants);
            this.Variants = this.Variants.Distinct().ToList(); // if variant copies with limits we can double dip, also bloats loops

            if (this.RespawningVariants == null)
            {
                this.RespawningVariants = new List<int>();
            }
            this.RespawningVariants.AddRange(injectedActor.respawningVariants);

            this.VariantsWithRoomMax.AddRange(injectedActor.limitedVariants);
            var groundVariantEntry = this.AllVariants[(int)ActorType.Ground - 1];
            groundVariantEntry.AddRange(injectedActor.groundVariants.Except(groundVariantEntry));
            var flyingVariantEntry = this.AllVariants[(int)ActorType.Ground - 1];
            flyingVariantEntry.AddRange(injectedActor.flyingVariants.Except(flyingVariantEntry));
        }
    }
}
