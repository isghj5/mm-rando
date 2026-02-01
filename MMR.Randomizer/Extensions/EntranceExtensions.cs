using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Extensions
{
    public static class EntranceExtensions
    {
        public static ushort? SpawnId(this Entrance entrance)
        {
            return entrance.GetAttribute<SpawnAttribute>()?.SpawnId;
        }

        public static SpawnActorParamsAttribute SpawnActorParams(this Entrance entrance)
        {
            return entrance.GetAttribute<SpawnActorParamsAttribute>();
        }

        public static IEnumerable<Tuple<int, byte>> ExitIndices(this Entrance entrance)
        {
            return entrance.GetAttributes<ExitAttribute>().Select(ea => new Tuple<int, byte>(ea.SceneId, ea.ExitIndex));
        }

        public static IEnumerable<Tuple<int, byte, byte>> ExitCutscenes(this Entrance entrance)
        {
            return entrance.GetAttributes<ExitCutsceneAttribute>().Select(eca => new Tuple<int, byte, byte>(eca.SceneId, eca.SetupIndex, eca.CutsceneIndex));
        }

        public static IEnumerable<int> ExitAddresses(this Entrance entrance)
        {
            return entrance.GetAttributes<ExitAddressAttribute>().Select(eaa => eaa.Address);
        }

        public static IEnumerable<ExitActorParamsAttribute> ExitActorParams(this Entrance entrance)
        {
            return entrance.GetAttributes<ExitActorParamsAttribute>();
        }

        public static ExitPolygonTypeAttribute ExitPolygonType(this Entrance entrance)
        {
            return entrance.GetAttribute<ExitPolygonTypeAttribute>();
        }

        public static IEnumerable<byte[]> HackContent(this Entrance entrance, bool isSame)
        {
            var hackContentAttributes = entrance.GetAttributes<HackContentAttribute>();
            if (isSame)
            {
                hackContentAttributes = hackContentAttributes.Where(h => !h.ApplyOnlyIfItemIsDifferent);
            }
            return hackContentAttributes.Select(h => h.HackContent);
        }
    }
}
