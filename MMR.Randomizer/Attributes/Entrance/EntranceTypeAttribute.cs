using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MMR.Randomizer.Attributes.Entrance
{
    public class EntranceTypeAttribute : Attribute
    {
        public EntranceType Type { get; }
        public ReadOnlyCollection<EntranceType> AdditionalTypes { get; }

        public EntranceTypeAttribute(EntranceType entranceType, params EntranceType[] additionalTypes)
        {
            Type = entranceType;
            AdditionalTypes = additionalTypes.ToList().AsReadOnly();
        }
    }

    public enum EntranceType
    {
        Interior,
        Overworld,
        InteriorExit,
        Permanent,
        Dungeon,
        Boss,
        Trial,
        DungeonExit,
        TrialExit,
        OwlWarp,
        Telescope,
        Grotto,
        VoidRespawn,
        FairyFountain,
    }
}
