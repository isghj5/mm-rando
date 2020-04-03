using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class SceneInternalIdAttribute : Attribute
    {
        public byte InternalId { get; private set; }

        public SceneInternalIdAttribute(byte internalId)
        {
            InternalId = internalId;
        }
    }
}
