using MMR.Randomizer.Models;
using MMR.Randomizer.Utils;

namespace MMR.Randomizer.Extensions
{
    public static class ItemObjectExtensions
    {
        public static string DisplayName(this ItemObject itemObject)
        {
            return itemObject.Mimic?.FakeName ?? itemObject.Mimic?.Item.Name() ?? itemObject.Item.Name();
        }

        public static string AlternateName(this ItemObject itemObject)
        {
            return MessageUtils.GetAlternateName(itemObject.DisplayName());
        }
    }
}
