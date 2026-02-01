using System;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemGibdoAttribute : Attribute
    {
        public byte ItemAction { get; }
        public byte Item { get; }
        public ushort MessageId { get; }
        public string CustomMessage { get; }
        public ushort Data { get; }

        public ItemGibdoAttribute(byte itemAction, byte item, ushort messageId, ushort data = 0)
        {
            ItemAction = itemAction;
            Item = item;
            MessageId = messageId;
            Data = data;
        }

        public ItemGibdoAttribute(byte itemAction, byte item, string customMessage, ushort messageId = default, ushort data = 0)
        {
            ItemAction = itemAction;
            Item = item;
            CustomMessage = customMessage;
            MessageId = messageId;
            Data = data;
        }
    }
}
