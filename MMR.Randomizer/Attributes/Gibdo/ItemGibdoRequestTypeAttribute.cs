using System;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemGibdoRequestTypeAttribute : Attribute
    {
        public GibdoRequestType RequestType { get; }

        public ItemGibdoRequestTypeAttribute(GibdoRequestType requestType)
        {
            RequestType = requestType;
        }

        public enum GibdoRequestType
        {
            Ammo = 0,
            Bottle = 1,
            QuestTrade = 2,
            Photo = 3,
            Mask = 4,
        }
    }
}
