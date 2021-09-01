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
    // todo consider breaking this up into slots and replacement actor subtypes

    public class Actor
    {
        // this is instance data, per actor, per scene.
        //  sometimes built from romdate, sometimes generated from enums
        // if you want metadata about actor types use the enum Gameobjects.Actor

        public string Name = ""; // for debug mostly, got real sick of looking up each and every actor index
        public string OldName = ""; // for debug mostly, got real sick of looking up each and every actor index
        public int ActorID; // in-game actor list index
        public GameObjects.Actor ActorEnum; // enumerator with metadata about the actor and actor extensions
        public GameObjects.Actor OldActorEnum; // enumerator with metadata about the actor and actor extensions
        public int ObjectID; // in-game object list index
        public int ActorSize; // todo
        public int ObjectSize; // read by enemizer at scene actor reading
        public int ActorIDFlags; // we just want to keep them when re-writing, but I'm not sure they even matter
        public List<int> Variants = new List<int> { 0 };
        public int OldVariant;
        public bool MustNotRespawn = false;
        public int Room;           // this specific actor, which map/room was it in
        public int RoomActorIndex; // the index of this actor in its room's actor list
        public ActorType Type; 
        //public int Stationary; // Deathbasket used to use this, I dont see the point except around water
        public vec16 Position = new vec16();
        public vec16 Rotation = new vec16();
        public bool IsCompanion = false;
        public bool previouslyMovedCompanion = false;

        public List<GameObjects.Scene> SceneExclude = new List<GameObjects.Scene>();

        public int sceneID; // do we still need this?
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

        public Actor() { }

        public Actor(InjectedActor injected, string name)
        {
            InjectedActor = injected;
            Name = OldName = name;
            ActorID = injected.actorID;
            ObjectID = injected.objID;
            // variants?
        }

        public Actor(GameObjects.Actor actor)
        {
            Name = actor.ToString();
            ActorID = (int)actor;
            ActorEnum = actor;
            ObjectID = actor.ObjectIndex();
            ObjectSize = ObjUtils.GetObjSize(actor.ObjectIndex());
            Variants = actor.AllVariants();
            Rotation = new vec16();

            SceneExclude = actor.ScenesRandomizationExcluded();
            BlockedScenes = actor.BlockedScenes();
            AllVariants = BuildVariantList(actor);
            Variants = AllVariants.SelectMany(u => u).ToList();
            RespawningVariants = actor.RespawningVariants();
        }

        public static List<List<int>> BuildVariantList(GameObjects.Actor actor)
        {
            //var newList = new List<List<int>>();
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
        }

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


        public List<int> CompatibleVariants(Actor otherActor, int oldActorVariant, Random rng)
        {
            // with mixed types, typing could be messy, keep it hidden here
            // EG. like like can spawn on the sand (land), but also on the bottom of GBC (water floor)

            if (this.AllVariants == null || otherActor.AllVariants == null)
            {
                throw new Exception("Compare Variants: broken actor variants listoflist");
            }

            var listOfVariantTypes = Enum.GetValues(typeof(ActorType)).Cast<ActorType>().ToList();
            listOfVariantTypes.Remove(ActorType.Unset);
            listOfVariantTypes = listOfVariantTypes.OrderBy(u => rng.Next()).ToList(); // random sort in case it has multiple types
            foreach (var randomVariant in listOfVariantTypes)
            {
                List<int> ourVariants   = this.AllVariants[(int)randomVariant - 1];
                List<int> theirVariants = otherActor.AllVariants[(int)randomVariant - 1];

                // large chance of pathing enemies allowing ground or flying
                //if (randomVariant == ActorType.Pathing && ourVariants != null && otherAttr == null && rng.Next(100) < 80)
                if (randomVariant == ActorType.Pathing && ourVariants.Count > 0 && theirVariants.Count == 0 && rng.Next(100) < 80)
                {
                    theirVariants = otherActor.AllVariants[(int) ActorType.Flying];
                    if (theirVariants.Count == 0)
                    {
                        theirVariants = otherActor.AllVariants[(int) ActorType.Ground];
                    }
                }

                // small chance of ground enemies allowing flying
                //if (randomVariant == ActorType.Ground && ourAttr != null && otherAttr == null && rng.Next(100) < 30)
                if (randomVariant == ActorType.Ground && ourVariants.Count > 0 && theirVariants.Count == 0 && rng.Next(100) < 30)
                {
                    theirVariants = otherActor.AllVariants[(int) ActorType.Flying];
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
                        && ourVariants.Contains(oldActorVariant)) // old actor had to actually be this attribute
                    {
                        return compatibleVariants;
                    }
                }
            }
            return null;
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
