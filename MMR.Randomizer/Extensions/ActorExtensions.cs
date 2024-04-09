using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using MMR.Randomizer.Utils;
using System;
using MMR.Randomizer.Attributes;
using System.Linq;

namespace MMR.Randomizer.Extensions
{
    public static class ActorExtensions
    {
        public static int FileListIndex(this Actor actor)
        {
            return actor.GetAttribute<FileIDAttribute>()?.ID ?? -1;
        }

        public static int ObjectIndex(this Actor actor)
        {
            return actor.GetAttribute<ObjectListIndexAttribute>()?.Index ?? -1;
        }

        public static bool IsEnemyRandomized(this Actor actor)
        {
            return actor.GetAttribute<EnemizerEnabledAttribute>()?.Enabled ?? false;
        }

        public static bool IsActorRandomized(this Actor actor)
        {
            return actor.GetAttribute<ActorizerEnabledAttribute>()?.Enabled ?? false;
        }

        public static bool IsActorFreeOnly(this Actor actor)
        {
            return actor.GetAttribute<ActorizerEnabledFreeOnlyAttribute>()?.Enabled ?? false;
        }


        public static List<int> UnkillableVariants(this Actor actor)
        {
            if (actor.GetAttribute<UnkillableAllVariantsAttribute>() != null) // all are unkillable
            {
                return AllVariants(actor);
            }
            else
            {
                return actor.GetAttribute<UnkillableVariantsAttribute>()?.Variants;
            }
        }

        public static List<int> RespawningVariants(this Actor actor)
        {
            if (actor.GetAttribute<RespawningAllVariantsAttribute>() != null) // all are respawning
            {
                return AllVariants(actor);
            }
            else
            {
                return actor.GetAttribute<RespawningVariantsAttribute>()?.Variants;
            }
        }


        public static List<int> AllVariants(this Actor actor)
        {
            var variants = new List<int>();
            var attrF = actor.GetAttribute<FlyingVariantsAttribute>();
            if (attrF != null)
            {
                variants.AddRange(attrF.Variants);
            }
            var attrW = actor.GetAttribute<WaterVariantsAttribute>();
            if (attrW != null)
            {
                variants.AddRange(attrW.Variants);
            }
            var wtattr = actor.GetAttribute<WaterTopVariantsAttribute>();
            if (wtattr != null)
            {
                variants.AddRange(wtattr.Variants);
            }
            var wbattr = actor.GetAttribute<WaterBottomVariantsAttribute>();
            if (wbattr != null)
            {
                variants.AddRange(wbattr.Variants);
            }
            var attrG = actor.GetAttribute<GroundVariantsAttribute>();
            if (attrG != null)
            {
                variants.AddRange(attrG.Variants);
            }
            var attrWall = actor.GetAttribute<WallVariantsAttribute>();
            if (attrWall != null)
            {
                variants.AddRange(attrWall.Variants);
            }
            var attrCeil = actor.GetAttribute<CeilingVariantsAttribute>();
            if (attrCeil != null)
            {
                variants.AddRange(attrCeil.Variants);
            }
            var attrPerch = actor.GetAttribute<PerchingVariantsAttribute>();
            if (attrPerch != null)
            {
                variants.AddRange(attrPerch.Variants);
            }
            var attrPatrol = actor.GetAttribute<PathingVariantsAttribute>();
            if (attrPatrol != null)
            {
                variants.AddRange(attrPatrol.Variants);
            }

            return variants;
        }

        public static List<int> KillableVariants(this Actor actor, List<int> acceptableVariants = null)
        {
            var killableVariants = acceptableVariants != null ? acceptableVariants : AllVariants(actor);
            var unkillableVariants = UnkillableVariants(actor);
            var respawningVariants = RespawningVariants(actor);
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

        public static List<Scene> ScenesRandomizationExcluded(this Actor actor)
        {
            return actor.GetAttribute<ForbidFromSceneAttribute>()?.ScenesExcluded ?? new List<Scene>();
        }

        public static List<int> GetUnPlacableVariants(this Actor actor)
        {
            // if variants with zero max exist, we cannot use them, look them up and return
            var limitedVariantsAttrs = actor.GetAttributes<VariantsWithRoomMax>();
            if (limitedVariantsAttrs == null)
            {
                return new List<int>();

            }

            var unplacable = new List<int>();
            foreach (var combo in limitedVariantsAttrs)
            {
                if (combo.RoomMax == 0)
                {
                    unplacable.AddRange(combo.Variants);
                }
            }

            return unplacable;
        }


        public static ActorType GetType(this Actor actor, int variant)
        {
            /// gets a rough type of the actor based on vars
            // this is going to be rough estimate, if pathing or wall will have priority

            // issue: there doesn't seem to be a way to have a list of Attributes and use them, maaan

            ActorVariantsAttribute pathingVariants = actor.GetAttribute<PathingVariantsAttribute>();
            if (pathingVariants != null)
            {
                // if their variant is in this list, return type
                if (pathingVariants.Variants.Contains(variant))
                {
                    return ActorType.Pathing;
                }
            }

            ActorVariantsAttribute wallVariants = actor.GetAttribute<WallVariantsAttribute>();
            if (wallVariants != null)
            {
                // if their variant is in this list, return type
                if (wallVariants.Variants.Contains(variant))
                {
                    return ActorType.Wall;
                }
            }

            ActorVariantsAttribute flyingVariants = actor.GetAttribute<FlyingVariantsAttribute>();
            if (flyingVariants != null)
            {
                // if their variant is in this list, return type
                if (flyingVariants.Variants.Contains(variant))
                {
                    return ActorType.Flying;
                }
            }

            ActorVariantsAttribute waterVariants = actor.GetAttribute<WaterVariantsAttribute>();
            ActorVariantsAttribute waterTVariants = actor.GetAttribute<WaterTopVariantsAttribute>();
            ActorVariantsAttribute waterBVariants = actor.GetAttribute<WaterBottomVariantsAttribute>();

            if (waterVariants != null)
            {
                // if their variant is in this list, return type
                if (waterVariants.Variants.Contains(variant))
                {
                    return ActorType.Water;
                }
            }
            if (waterTVariants != null)
            {
                // if their variant is in this list, return type
                if (waterTVariants.Variants.Contains(variant))
                {
                    return ActorType.Water;
                }
            }
            if (waterBVariants != null)
            {
                // if their variant is in this list, return type
                if (waterBVariants.Variants.Contains(variant))
                {
                    return ActorType.Water;
                }
            }


            ActorVariantsAttribute GroundVariants = actor.GetAttribute<GroundVariantsAttribute>();
            if (GroundVariants != null)
            {
                // if their variant is in this list, return type
                if (GroundVariants.Variants.Contains(variant))
                {
                    return ActorType.Ground;
                }
            }

            return ActorType.Unset;
        }

        public static bool IsGroundVariant(this Actor actor, int varient)
        {
            var groundAttribute = actor.GetAttribute<GroundVariantsAttribute>();
            if (groundAttribute != null)
            {
                return groundAttribute.Variants.Contains(varient);
            }
            return false;
        }

        public static bool IsWaterVariant(this Actor actor, int varient)
        {
            var groundAttribute = actor.GetAttribute<WaterVariantsAttribute>();
            if (groundAttribute != null)
            {
                return groundAttribute.Variants.Contains(varient);
            }
            return false;
        }

        public static bool isFlyingVariant(this Actor actor, int varient)
        {
            var groundAttribute = actor.GetAttribute<FlyingVariantsAttribute>();
            if (groundAttribute != null)
            {
                return groundAttribute.Variants.Contains(varient);
            }
            return false;
        }

        public static List<Scene> BlockedScenes(this Actor actor)
        {
            var blockedScenesAttribute = actor.GetAttribute<EnemizerScenesPlacementBlock>();
            if (blockedScenesAttribute != null)
            {
                return blockedScenesAttribute.ScenesBlocked;
            }
            return new List<Scene>();
        }

        public static int VariantMaxCountPerRoom(this Actor actor, int queryVariant)
        {
            if (actor.GetAttribute<OnlyOneActorPerRoom>() != null)
            {
                return 1;
            }

            var limitedVariants = actor.GetAttributes<VariantsWithRoomMax>();
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

        public static bool HasVariantsWithRoomLimits(this Actor actor)
        {
            return actor.GetAttributes<VariantsWithRoomMax>() != null
                || OnlyOnePerRoom(actor);
        }

        public static bool OnlyOnePerRoom(this Actor actor)
        {
            return actor.GetAttribute<OnlyOneActorPerRoom>() != null;
        }

        // as this is from the enum values, this happens before injected actors, if you see an injected actor getting snipped its fine
        public static bool NoPlacableVariants(this Actor actor)
        {
            var allVariantsAttrs = actor.GetAttributes<ActorVariantsAttribute>();
            if (allVariantsAttrs == null)
            {
                return true;
            }

            foreach (var attr in allVariantsAttrs)
            {
                var allVariants = attr.Variants;
                for (int i = 0; i < allVariants.Count(); i++)
                {
                    var max = VariantMaxCountPerRoom(actor, allVariants[i]);
                    // if -1, no max. if 1 or greater, does not quality as zero
                    if (max != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static bool HasOptionalCompanions(this Actor actor)
        {
            return actor.GetAttributes<AlignedCompanionActorAttribute>() != null;
        }

        public static int ActorInitOffset(this Actor actor)
        {
            return actor.GetAttribute<ActorInitVarOffsetAttribute>()?.Offset ?? -1;
        }

        public static bool IsBlockable(this Actor actor, Scene scene, int actorPos)
        {
            /// checks if a specific actor in a scene at scene read time is blockable

            // quickly check if the scene cares
            var sceneBlockingConditions = scene.GetAttributes<EnemizerSceneBlockSensitiveAttribute>();
            if (sceneBlockingConditions == null) return true;

            // does the scene have a case for this actor
            EnemizerSceneBlockSensitiveAttribute searchResult = null;
            foreach (var condition in sceneBlockingConditions)
            {
                if (condition.OriginalEnemy == actor)
                {
                    searchResult = condition;
                }
            }
            if (searchResult == null) return true;

            // if we want all block sensitive, we have what we need leave early
            if (searchResult.SpecificVariants.Count == 1 && searchResult.SpecificVariants[0] == -1) return false;

            // else, check if this is one of the blocked positions
            var sensitivePositions = searchResult.SpecificVariants;
            if (sensitivePositions.Contains(actorPos)) return false;

            return true;
        }

        public static bool IsBlockingActor(this Actor actor, int variant = 0xFFFFF)
        {
            if (actor.GetAttribute<BlockingVariantsAll>() != null)
            {
                return true;
            }

            var blockingVariantsAttr = actor.GetAttribute<BlockingVariantsAttribute>();
            if (blockingVariantsAttr != null)
            {
                var blockingVariants = blockingVariantsAttr.Variants;
                return blockingVariants.Contains(variant);
            }

            return false;
        }

        public static List<int> GetBlockingVariants(this Actor actor)
        {
            if (actor.GetAttribute<BlockingVariantsAll>() != null)
            {
                // all variants are blockable, return all
                return actor.AllVariants();
            }

            var blockingVariantsAttr = actor.GetAttribute<BlockingVariantsAttribute>();
            if (blockingVariantsAttr != null)
            {
                // only some are blocking, return those
                return blockingVariantsAttr.Variants;
            }

            return new List<int> { }; // non are blocking
        }
    }
}
