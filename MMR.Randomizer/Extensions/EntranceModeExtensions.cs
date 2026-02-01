using MMR.Common.Extensions;
using MMR.Randomizer.Attributes.Entrance;
using MMR.Randomizer.Models.Settings;

namespace MMR.Randomizer.Extensions
{
    public static class EntranceModeExtensions
    {
        public static EntranceType? EntranceType(this EntranceMode entranceMode)
        {
            return entranceMode.GetAttribute<EntranceTypeAttribute>()?.Type;
        }
    }
}
