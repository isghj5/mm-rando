using System;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class EntranceAttribute : Attribute
    {
        public EntranceType? Type { get; private set; }

        public EntranceAttribute(EntranceType entranceType)
        {
            Type = entranceType;
        }

        public EntranceAttribute()
        {

        }
    }

    public enum EntranceType
    {
        Interior,
        Overworld,
        DungeonEntrance,
        DungeonExit,
        Grotto,
        Trial,
        Telescope,
        VoidRespawn,
        BossRoom,
        OwlWarp,
        MajorasLair,
        CallGiants,
    }
}
