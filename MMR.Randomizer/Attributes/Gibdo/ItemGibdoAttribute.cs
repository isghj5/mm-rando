using System;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemGibdoAttribute : Attribute
    {
        public byte ItemAction { get; }
        public byte Item { get; }
        public ushort MessageId { get; set; }
        public string CustomMessage { get; set; }
        public ushort Data { get; set; }

        public ItemGibdoAttribute(byte itemAction, byte item)
        {
            ItemAction = itemAction;
            Item = item;
        }
    }
}
