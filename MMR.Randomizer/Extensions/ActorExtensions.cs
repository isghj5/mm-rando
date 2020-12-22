using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using MMR.Randomizer.Utils;
using System;
using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.Extensions
{
    public static class ActorExtensions
    {
        public static int FileListIndex(this Actor actor)
        {
            return actor.GetAttribute<FileIDAttribute>().ID;
        }

        public static int ObjectIndex(this Actor actor)
        {
            return actor.GetAttribute<ObjectListIndexAttribute>().Index;
        }

        public static bool IsEnemyRandomized(this Actor actor)
        {
            return actor.GetAttribute<EnemizerEnabledAttribute>()?.Enabled ?? false;
        }

        public static bool IsActorRandomized(this Actor actor)
        {
            return actor.GetAttribute<ActorizerEnabledAttribute>()?.Enabled ?? false;
        }

        public static List<int> RespawningVariants(this Actor actor)
        {
            return actor.GetAttribute<RespawningVarientsAttribute>()?.Variants;
        }

        public static List<int> Variants(this Actor actor)
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
            var attrG = actor.GetAttribute<GroundVariantsAttribute>();
            if (attrG != null)
            {
                variants.AddRange(attrG.Variants);
            }

            return variants;
        }

        public static List<int> NonRespawningVariants(this Actor actor, List<int> acceptableVariants = null)
        {
            var nonRespawningVariants = acceptableVariants != null ? acceptableVariants : Variants(actor);
            var respawningVariants    = RespawningVariants(actor);
            if (respawningVariants != null && respawningVariants.Count > 0)
            {
                nonRespawningVariants.RemoveAll(u => respawningVariants.Contains(u));
            }
            return nonRespawningVariants;
        }

        public static List<int> ScenesRandomizationExcluded(this Actor actor)
        {
            return actor.GetAttribute<EnemizerScenesExcludedAttribute>()?.ScenesExcluded ?? new List<int>();
        }

        public static Models.Rom.Enemy ToEnemy(this Actor actor)
        {
            // turning static actor enum into modifiable enemy type
            return new Models.Rom.Enemy()
            {
                Name         = (actor).ToString(),
                Actor        = (int) actor,
                Object       = actor.ObjectIndex(),
                ObjectSize   = ObjUtils.GetObjSize(actor.ObjectIndex()),
                Variables    = actor.Variants(),
                SceneExclude = actor.ScenesRandomizationExcluded()
            };
        }

        public static List<int> CompatibleVariants(this Actor actor, Actor otherActor, Random rng, int oldActorVariant)
        {
            // with mixed types, typing could be messy, keep it hidden here
            // EG. like like can spawn on the sand on the surface, but also on the bottom of GBC

            // I'm sure theres a cleaner way, but everything I tried C# said no
            var listOfVariants = new List<byte>() {1 , 2 , 3 };
            listOfVariants.ForEach(u => rng.Next()); // random sort in case it has multiple types
            foreach( var variant in listOfVariants)
            {
                ActorVariantsAttribute ourAttr = null;
                ActorVariantsAttribute theirAttr = null;
                if (variant == 1) // water
                {
                    ourAttr = actor.GetAttribute < WaterVariantsAttribute >();
                    theirAttr = otherActor.GetAttribute< WaterVariantsAttribute >();
                }
                if (variant == 2) // ground
                {
                    ourAttr = actor.GetAttribute<GroundVariantsAttribute>();
                    theirAttr = otherActor.GetAttribute<GroundVariantsAttribute>();
                }
                if (variant == 3) // flying
                {
                    ourAttr = actor.GetAttribute<FlyingVariantsAttribute>();
                    theirAttr = otherActor.GetAttribute<FlyingVariantsAttribute>();
                }
                if (ourAttr != null && theirAttr != null) // both have same type
                {
                    var compatibleVariants = theirAttr.Variants;
                    // our old actor variant was this type
                    if (compatibleVariants.Count > 0 && ourAttr.Variants.Contains(oldActorVariant))
                    {
                        return compatibleVariants;
                    }
                }
            }

            return null;
        }

        public static int ActorInitOffset(this Actor actor)
        {
            return actor.GetAttribute<ActorInitVarOffsetAttribute>()?.Offset ?? -1;
        }
    }
}
