using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using MMR.Randomizer.Utils;
using System.Runtime.InteropServices;
using System;
using System.Runtime.CompilerServices;
using System.CodeDom;
using System.Linq;

namespace MMR.Randomizer.Extensions
{
    public static class ActorExtensions
    {
        public static int ActorIndex(this Actor actor)
        {
            return actor.GetAttribute<ActorListIndexAttribute>().Index;
        }

        public static int ObjectIndex(this Actor actor)
        {
            return actor.GetAttribute<ObjectListIndexAttribute>().Index;
        }

        public static bool IsEnemyRandomized(this Actor actor)
        {
            return actor.GetAttribute<EnemizerEnabledAttribute>()?.Enabled ?? false;
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
                variants.Union(attrF.Variants);
            }
            var attrW = actor.GetAttribute<WaterVariantsAttribute>();
            if (attrW != null)
            {
                variants.Union(attrW.Variants);
            }
            var attrG = actor.GetAttribute<GroundVariantsAttribute>();
            if (attrG != null)
            {
                variants.Union(attrG.Variants);
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
                Actor        = actor.ActorIndex(),
                Object       = actor.ObjectIndex(),
                ObjectSize   = ObjUtils.GetObjSize(actor.ObjectIndex()),
                Variables    = actor.Variants(),
                SceneExclude = actor.ScenesRandomizationExcluded()
            };
        }

        public static List<int> CompatibleVariants(this Actor actor, Actor otherActor, Random rng)
        {
            // with mixed types, typing could be messy, keep it hidden here
            // EG. like like can spawn on the sand on the surface, but also on the bottom of GBC

            // I'm sure theres a cleaner way, but everything I tried C# said no
            var listOfVariants = new List<byte>() {1 , 2 , 3 };
            listOfVariants.ForEach(u => rng.Next()); // random sort in case it has multiple types
            foreach( var variant in listOfVariants)
            {
                List<int> compatibleVariants = null;
                if (variant == 1) // water
                {
                    var ourAttr = actor.GetAttribute < WaterVariantsAttribute >();
                    var theirAttr = otherActor.GetAttribute< WaterVariantsAttribute >();
                    if (ourAttr != null && theirAttr != null)
                    {
                        compatibleVariants = (ourAttr.Variants).FindAll(u => theirAttr.Variants.Contains(u));
                    }
                }
                if (variant == 2) // ground
                {
                    var ourAttr = actor.GetAttribute<GroundVariantsAttribute>();
                    var theirAttr = otherActor.GetAttribute<GroundVariantsAttribute>();
                    if (ourAttr != null && theirAttr != null)
                    {
                        compatibleVariants = (ourAttr.Variants).FindAll(u => theirAttr.Variants.Contains(u));
                    }
                }
                if (variant == 3) // flying
                {
                    var ourAttr = actor.GetAttribute<FlyingVariantsAttribute>();
                    var theirAttr = otherActor.GetAttribute<FlyingVariantsAttribute>();
                    if (ourAttr != null && theirAttr != null)
                    {
                        compatibleVariants = (ourAttr.Variants).FindAll(u => theirAttr.Variants.Contains(u));
                    }
                }

                if (compatibleVariants != null && compatibleVariants.Count > 0)
                {
                    return compatibleVariants;
                }
            }

            return null;
        }
    }
}
