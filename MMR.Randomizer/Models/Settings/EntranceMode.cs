using MMR.Randomizer.Attributes.Entrance;
using System;
using System.ComponentModel;

namespace MMR.Randomizer.Models.Settings
{
    [Flags]
    [Description("Entrances")]
    public enum EntranceMode
    {
        Default,

        [Description("Enable randomization of dungeon entrances. \n\nStone Tower Temple is always vanilla, but Inverted Stone Tower Temple is randomized.")]
        DungeonEntrances = 1,

        [Description("Enable randomization of boss rooms. The boss door texture will match the boss behind the door.")]
        BossRooms = 1 << 1,

        [EntranceType(EntranceType.Grotto)]
        [Description("Enable randomization of grottos. North Clock Town grotto and Deku Palace grotto will never be generic grottos or cow grottos.")]
        Grottos = 1 << 2,

        [EntranceType(EntranceType.Interior)]
        [Description("Enable randomization of simple interiors.")]
        SimpleInteriors = 1 << 3,
    }
}
