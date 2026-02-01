using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SpawnActorParamsAttribute : Attribute
    {
        public ushort ActorId { get; }
        public ushort Param { get; }
        public ushort ParamMask { get; }
        public ushort? RotXParam { get; }
        public ushort? RotYParam { get; }
        public ushort? RotZParam { get; }

        public SpawnActorParamsAttribute(ushort actorId, ushort param, ushort paramMask, ushort rotXParam, ushort rotYParam, ushort rotZParam)
        {
            ActorId = (ushort)(actorId & 0x1FFF);
            Param = param;
            ParamMask = paramMask;
            if ((actorId & 0x8000) != 0)
            {
                RotYParam = rotYParam;
            }
            if ((actorId & 0x4000) != 0)
            {
                RotXParam = rotXParam;
            }
            if ((actorId & 0x2000) != 0)
            {
                RotZParam = rotZParam;
            }
        }
    }
}
