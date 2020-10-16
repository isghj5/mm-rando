using MMR.Randomizer.Models;
using System;

namespace MMR.Randomizer.Attributes
{
    public class ExclusiveItemAttribute : Attribute
    {
        public byte Item { get; private set; }
        public byte Flags { get; private set; }
        public byte Type { get; private set; }

        public ExclusiveItemAttribute(byte item, byte flags = 0, byte type = 0)
        {
            this.Item = item;
            this.Flags = flags;
            this.Type = type;
        }
    }

    public class ExclusiveItemGraphicAttribute : Attribute
    {
        public byte Graphic { get; private set; }
        public ushort Object { get; private set; }

        public ExclusiveItemGraphicAttribute(byte graphic, ushort obj)
        {
            this.Graphic = graphic;
            this.Object = obj;
        }
    }

    public class ExclusiveItemMessageAttribute : Attribute
    {
        public ushort Id { get; private set; }
        public string Message { get; private set; }

        public ExclusiveItemMessageAttribute(ushort id, string message = null)
        {
            this.Id = id;
            this.Message = message;
        }
    }
}
