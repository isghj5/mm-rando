using System.Collections.Generic;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Attributes.Actor;
using MMR.Common.Extensions;
using MMR.Randomizer.Utils;

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

        public static ActorType EnemyType(this Actor actor)
        {
            return actor.GetAttribute<EnemyTypeAttribute>().Type;
        }

        public static List<int> RespawningVariants(this Actor actor)
        {
            return actor.GetAttribute<RespawningVarientsAttribute>()?.Variants;
        }

        public static List<int> Variants(this Actor actor)
        {
            return actor.GetAttribute<ActorVariantsAttribute>()?.Variants;
        }

        public static List<int> NonRespawningVariants(this Actor actor)
        {
            var nonRespawningVariants = Variants(actor);
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
                Type         = (int) actor.EnemyType(),
                SceneExclude = actor.ScenesRandomizationExcluded()
            };
        }
    }
}
