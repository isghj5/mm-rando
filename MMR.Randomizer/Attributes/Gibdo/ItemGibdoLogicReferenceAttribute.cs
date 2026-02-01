using System;

namespace MMR.Randomizer.Attributes.Gibdo
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ItemGibdoLogicReferenceAttribute : Attribute
    {
        public GameObjects.Item Reference { get; }

        public ItemGibdoLogicReferenceAttribute(GameObjects.Item reference)
        {
            Reference = reference;
        }
    }
}
