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
    [System.Diagnostics.DebuggerDisplay("[{Name}][{ActorID.ToString(\"X3\")}]")]
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
        public int ActorIDFlags; // we just want to keep them when re-writing, but I'm not sure they even matter
        public List<int> Variants = new List<int> { 0 };
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
        public List<GameObjects.Scene> BlockedScenes = null;
        public List<int> RespawningVariants = null;
        public List<int> UnplaceableVariants = null;
        public List<VariantsWithRoomMax> VariantsWithRoomMax;
        public List<List<int>> AllVariants = null;

        public Actor() { } // default, used when building from scene/room read

        public Actor(GameObjects.Actor actor)
        {
            // converted from enum, used for building replacement candidate actors

            Name = actor.ToString();
            ActorID = (int)actor;
            ActorEnum = actor;
            ObjectID = actor.ObjectIndex();
            ObjectSize = ObjUtils.GetObjSize(actor.ObjectIndex());
            Rotation = new vec16();

            SceneExclude = actor.ScenesRandomizationExcluded();
            BlockedScenes = actor.BlockedScenes();
            AllVariants = BuildVariantList(actor);
            Variants = AllVariants.SelectMany(u => u).ToList(); // might as well start with all
            RespawningVariants = actor.RespawningVariants();
        }

        public Actor(InjectedActor injected, string name)
        {
            // create actor from injected actor

            InjectedActor = injected;
            Name = OldName = name;
            ActorID = injected.actorID;
            ObjectID = injected.objID;
            // for now injected actors can only be of type ground
            Variants = injected.groundVariants;
            AllVariants = new List<List<int>>()
            {
                new List<int>(),
                Variants,
                new List<int>(),
                new List<int>(),
                new List<int>(),
            };
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

            if (this.RespawningVariants != null)
            {
                var newRespawningVariants = new List<int>();
                for (int i = 0; i < this.RespawningVariants.Count; i++)
                {
                    newRespawningVariants.Add(this.RespawningVariants[i]);
                }
                newActor.RespawningVariants = newRespawningVariants;
            }

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

        public void ChangeActor(GameObjects.Actor newActorType, int vars = -1)
        {
            /// this is for actor slots, changing the actor for re-writing the slot

            ActorEnum   = newActorType;
            Name        = newActorType.ToString();
            ActorID     = (int)newActorType;
            ObjectID    = newActorType.ObjectIndex();
            if (vars != -1)
            {
                Variants[0] = vars;
            }
        } // */

        public void ChangeActor(Actor otherActor, int vars)
        {
            /// this is for actor slots, changing the actor for re-writing the slot

            ActorEnum = otherActor.ActorEnum;
            Name = otherActor.Name;
            ActorID = otherActor.ActorID;
            ObjectID = otherActor.ObjectID;
            InjectedActor = otherActor.InjectedActor;
            if (vars != -1)
            {
                Variants[0] = vars;
            }
        }

        // todo remove oldActorVariant no longer used
        public List<int> CompatibleVariants(Actor otherActor, int oldActorVariant, Random rng)
        {
            // with mixed types, typing could be messy, keep it hidden here
            // EG. like like can spawn on the sand (land), but also on the bottom of GBC (water floor)
            // so we need to know what type the actor we are replacing is, and check if any otherActor variants can replace it

            if (this.AllVariants == null || otherActor.AllVariants == null)
            {
                throw new Exception("Compare Variants: broken actor variants listoflist");
            }

            // randomly select a type, check if they have matching types

            var listOfVariantTypes = Enum.GetValues(typeof(ActorType)).Cast<ActorType>().ToList();
            listOfVariantTypes.Remove(ActorType.Unset);
            listOfVariantTypes = listOfVariantTypes.OrderBy(u => rng.Next()).ToList(); // random sort in case it has multiple types
            foreach (var randomVariantType in listOfVariantTypes)
            {
                List<int> ourVariants = this.AllVariants[(int)randomVariantType - 1].ToList();
                List<int> theirVariants = otherActor.AllVariants[(int)randomVariantType - 1].ToList();

                // large chance of pathing enemies allowing ground or flying replacements
                if (randomVariantType == ActorType.Pathing
                    && ourVariants.Contains(oldActorVariant) && theirVariants.Count == 0 && rng.Next(100) < 80)
                {
                    // todo could make this random
                    theirVariants = otherActor.AllVariants[(int) ActorType.Flying -1];
                    if (theirVariants.Count == 0)
                    {
                        theirVariants = otherActor.AllVariants[(int) ActorType.Ground -1];
                    }
                }

                // small chance of ground enemies allowing flying replacements
                //if (randomVariantType == ActorType.Ground && ourAttr != null && otherAttr == null && rng.Next(100) < 30)
                if (randomVariantType == ActorType.Ground
                    && ourVariants.Contains(oldActorVariant) && theirVariants.Count == 0 && rng.Next(100) < 30)
                {
                    theirVariants = otherActor.AllVariants[(int) ActorType.Flying -1];
                }

                //if ((ourAttr != null && otherAttr != null) // both have same type
                if (ourVariants.Count > 0 && theirVariants.Count > 0) // both have same type
                {
                    var compatibleVariants = theirVariants;

                    // make sure their variants aren't un-placable either
                    var zeroPlacementVarieties = otherActor.UnplaceableVariants;
                    if (zeroPlacementVarieties != null)
                    {
                        compatibleVariants = compatibleVariants.FindAll(u => !zeroPlacementVarieties.Contains(u));
                    }
                    // our old actor variant was this type
                    if (compatibleVariants.Count > 0 && ourVariants.Count > 0
                        && ourVariants.Contains(this.OldVariant)) // old actor had to actually be this attribute
                    {
                        return compatibleVariants;
                    }
                }
            }
            return null;
        }

        // todo remove this as it should be built at enum->actor time
        public List<int> UnkillableVariants()
        {
            // todo finish converting this from enum to actor base type

            var actor = this.ActorEnum;
            if (actor != GameObjects.Actor.NULL)
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
            // todo: injected actor unkillable variants
            
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

        public int GetRandomVariant(Random rng)
        {
            return 4;
        }

        public static int VariantMaxCountPerRoom(Actor actor, int queryVariant)
        {
            if (actor.OnlyOnePerRoom != null)
            {
                return 1;
            }

            var limitedVariants = actor.VariantsWithRoomMax;
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
                    var max = VariantMaxCountPerRoom(this, i);
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
    }
}
