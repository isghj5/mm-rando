using MMR.Randomizer.Attributes;

namespace MMR.Randomizer.GameObjects
{
    public enum Enemy
    {

        // actor init variables were calculated by hand, 
        //  using the actor overlay start location in vrom to search the actor overlay table for the actor entry
        //  calc the diff between the vram start and actor init var ram location, from table values
        //  apply diff to rom location to find actorinitvar on rom

        [ActorInitVariable(0xE9B630)] 
        ChuChu,

        [ActorInitVariable(0xD2EF50)]
        Dekubaba,

        [ActorInitVariable(0xD6CF40)]
        WitheredDekubaba,

        [ActorInitVariable(0xE5CE50)]
        BioBaba,

        [ActorInitVariable(0xD10CD0)]
        Tektite,

        [ActorInitVariable(0xEC1EC0)]
        BombChu,

        [ActorInitVariable(0xD762F0)]
        LikeLike,

        [ActorInitVariable(0xE0EDD0)]
        Guay,

        [ActorInitVariable(0xCF5670)]
        Keese,

        [ActorInitVariable(0xEDD65C)]
        SkullFish,

        [ActorInitVariable(0xE06ED0)]
        Wolfos,

        [ActorInitVariable(0xEAE4D0)]
        BadBat,

        [ActorInitVariable(0xFE3660)]
        Leaver,

        [ActorInitVariable(0xEB91E0)]
        Bo,

        [ActorInitVariable(0xD5EFB0)]
        ShellBlade,

        [ActorInitVariable(0xD3D970)]
        BlueBubble,

        [ActorInitVariable(0xD47670)]
        Beamos,

        [ActorInitVariable(0xDA7780)]
        Freeza,

        [ActorInitVariable(0xD2B260)]
        Armos,

        [ActorInitVariable(0xD18370)]
        Dinofos,

        [ActorInitVariable(0xE1BA6C)]
        Dragonfly,

        [ActorInitVariable(0xF7ECE0)]
        Eeno,

        [ActorInitVariable(0xFC0384)]
        Bee,

        [ActorInitVariable(0xF86944)]
        HipLoop,         // the beetle that charges at you with a mask

        [ActorInitVariable(0xCF0560)]
        Wallmaster,

        [ActorInitVariable(0xD4DA50)]
        FloorMaster,

        [ActorInitVariable(0xE93250)]
        Snapper,

        [ActorInitVariable(0xD39100)]
        MadScrub,

        [ActorInitVariable(0xD217A0)]
        Skulltulla,

        [ActorInitVariable(0xD55B90)]
        Skullwalla,

        [ActorInitVariable(0xD287F0)]
        DeathArmos,

        [ActorInitVariable(0xD51290)]
        Readead,

        [ActorInitVariable(0x100D1E0)]
        GibdoIkana,

        [ActorInitVariable(0xF664F0)]
        GibdoWell,

        [ActorInitVariable(0xDA44EC)]
        FlyingPot,

        [ActorInitVariable(0xEA5E20)]
        Nejiron,

        [ActorInitVariable(0xD23630)]
        Peahat,

        [ActorInitVariable(0xCF32D0)]
        Dodongo,

        [ActorInitVariable(0x1078520)]
        Takkuri,
    }
}
