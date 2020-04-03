using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions
{
    public static class SceneExtensions
    {
        public static byte Id(this Scene scene)
        {
            return scene.GetAttribute<SceneInternalIdAttribute>().InternalId;
        }
    }
}
