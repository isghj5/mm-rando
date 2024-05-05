using static MMR.Randomizer.Models.SoundEffects.SoundEffectTag;

namespace MMR.Randomizer.Models.SoundEffects
{
    /// <summary>
    /// Sound effects used throughout the game. 
    /// The value of a sound effect corresponds to its Id
    /// 
    /// Extensive overview of sound ids can be found in this google spreadsheet:
    /// https://docs.google.com/spreadsheets/d/1YVJ7GdzNZUese6H8d40lzpxp8ZfWlt75c-lzBzKngmo/edit#gid=1343879920
    /// </summary>

    public enum SoundEffect
    {
        #region Player

        [Tags(Long)]
        PlayerBreakOutOfIce = 0x0075,

        // this is when you collect great fairy stray, not regular fairy
        //[Tags(Long)]
        //StrayFairyHeal = 0x00CF,  // this is inf loop, need to find a way to stop it going forever

        //[Tags(Short)]
        //DekuLinkBubbleShootSpit = 0x00E0, // this is just rude

        [Tags(Short)]
        DekuLinkFlowerPop = 0x00E3,

        [Tags(Short, Long, LowHpBeep)]
        GoronLinkPound = 0x00E8,

        #endregion

        #region Environment SFX

        [Tags(Long)]
        CuccoClucking = 0x2011,

        [Tags(Long)]
        CuccoCluckingRunningFromYou = 0x2012,

        [Replacable(0x00EABA46, 0x00EABC3A, 0x00EACACA)]
        [Tags(Long)]
        [ReplacableByTags(Long)]
        CuccoMorning = 0x2013,

        [Tags(Short)]
        TatlEmerges = 0x201B,

        [Tags(Short)]
        PhantomGannonSpikedFence = 0x203C, // left over from oot

        [Replacable(0x00D01186, 0x00CFE0A6, 0x00CF969A, 0x00CFA602, 0x00CFA816, 0x00CFA482, 0x00CF91E2, 0x00CFC43A)]
        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        EponaNeigh = 0x2044,

        // TODO turn this into a slot, good comedy hearing something weird living in the mailbox
        [Tags(Short)]
        PostBoxOpen = 0x204C, // opening a cash register

        [Tags(Short, LowHpBeep)]
        TatlHides = 0x205F,

        [Tags(Short, LowHpBeep)]
        FishFlop = 0x2069,

        [Tags(Short, LowHpBeep)]
        TatlAttacksDoor = 0x2072,

        [Tags(Long)] // very long
        TreasureChestAppear = 0x207B,

        [Tags(Short, LowHpBeep)]
        PotBreak = 0x2087,

        [Tags(Short, LowHpBeep)]
        BigStoneDoorOpensThud = 0x2093,

        [Tags(Long)]
        ToiletHandFlush = 0x2097,

        //[Tags(Long, Debug)]
        //MoonEarthQuake= 0x2098, // loops, damnit

        [Replacable(0x010504C0 + 0x14E, 0x010504C0 + 0x18A)]
        [Tags(Short)]
        [ReplacableByTags(Short, Long)]
        DoorBell = 0x209E,

        [Tags(Long)]
        BeehiveFall = 0x20A0,

        [Tags(Short)]
        WoodenCrateBreak = 0x20AA,

        [Replacable(0x00EABC4A, 0x00EACABE)]
        [Tags(Long)]
        [ReplacableByTags(Long)]
        WolfHowlEvening = 0x20AE,

        [Replacable(0xD1DCBA)]
        [Tags(Short)]
        [ReplacableByTags(Short)]
        FrogJumpCroak = 0x20B1,

        [Tags(Long)]
        JabuJabuDeepBreath = 0x20B6, // wow this is still in here??

        [Tags(Long)]
        FrogIncreaseSize = 0x20CC,

        [Tags(LowHpBeep, Short)]
        ToiletHandAppear = 0x20D0,

        [Tags(LowHpBeep, Short)]
        ToiletHandVanish = 0x20D1,

        [Replacable(0x00DFC776, 0xDFCC3A, 0xDFDCF6)]
        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        DogBark = 0x20D8,

        [Tags(Long)]
        CowMoo = 0x20DF,

        [Tags(Short, LowHpBeep)]
        SilverRupeeGet = 0x20E8, // from oot, red coin style puzzle

        [Tags(Short, LowHpBeep)]
        MonkeyJoy = 0x2101,

        [Tags(Short)]
        [ReplacableByTags(Short)]
        [Replacable(0xDFC84A, 0xDFE0FA)]
        DogGroan = 0x210B,

        [Replacable(0x00DFC7B6)]
        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        DogBarkAngry = 0x2110,

        [Replacable(0xDFC80A, 0xDFD5E6)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        DogWhine = 0x2113,

        [Tags(Short)]
        BeaverTalk1 = 0x2119,

        [Tags(Short)]
        BeaverTalk2 = 0x211A,

        [Tags(Short, LowHpBeep)]
        MonkeySad = 0x2121,

        [Tags(Short)] // maybe lowhpbeep
        TatlUrgentRing= 0x2125,    // notices skullkid left her behind

        [Tags(Long)]
        TatlSoundTheAlarm = 0x2126,

        [Tags(Short)]
        BomberWalking= 0x212C,

        [Tags(Short)]
        TatlSigh = 0x2135,  // fairies can't use doors

        [Tags(Short)]
        TatlFlyingBrake = 0x2136, // tatl has arrived

        //[Tags(Short, Debug)]
        //SmallFairyHeal = 0x2138, // looping, cannot use without stopping that

        [Tags(Long)]
        ClockTowerBounce = 0x2140,

        [Tags(Short, Long)]
        ClockTowerStairsDrop = 0x2145,

        //[Tags(Short, Debug)]
        //UFOAppear = 0x217b, // loops

        //[Tags(Long, Debug)] // ??? not used I don't think, I think this was meant for when you beat them but they went with fanfare instead
        //UFODash = 0x217c,

        [Tags(Short, LowHpBeep)]
        MilkPotDamage = 0x217E,

        [Tags(Long)]
        MoonStoneFalling = 0x2186,

        [Tags(Short, LowHpBeep)]
        BankStampHand = 0x2193,

        [Tags(Short, LowHpBeep)]
        LittleChickChirp = 0x2194,

        [Tags(Long)]
        SecretLadderAppears = 0x2197, // woodfall ladder

        //[Tags(Short, Debug)]
        //MoonEyesFlash = 0x219C, // unused I think

        //[Tags(Debug)]
        //MoonGrumble= 0x219D,

        //[Tags(Debug)]
        //MoonU2= 0x219E, // looping moon quake

        [Tags(Short, LowHpBeep)]
        TatlDashNormal = 0x219F,

        [Tags(Short, LowHpBeep)]
        STBlockCollide = 0x21A4, // CLANG

        //[Tags(Short, Debug)]
        //TatlAngryAtSakon = 0x21A5, // looping version of a previous sample

        //[Tags(Short, Debug)]
        //UFOLight = 0x21A8, // looping

        //[Tags(Short, Debug)]
        //FireworksLaunched = 0x21B9, // looping

        [Tags(Short)]
        SwordForgeClang = 0x21C2,

        #endregion

        #region Enemy SFX

        [Tags(Long)]
        DodongoExhale = 0x3001,

        [Tags(Short)]
        DodongoTakeDamage = 0x3003,

        [Tags(Short, LowHpBeep)]
        OdolwaTakeDamage = 0x3009,

        [Tags(Long)]
        OdolwaDefeated = 0x300B,

        [Tags(Short)]
        GaroAppear = 0x3012,

        //[Tags(Looping, Debug)]
        //OdolwaTaunt1 = 0x3015, // ooh-ah-la-dai, summoning bugs, looping

        //[Tags(Looping, Debug)]
        //OdolwaTaunt2 = 0x3016, // keh-laaaah-veh! taunting you to attack, looping

        //[Tags(Looping, Debug)]
        //OdolwaTaunt3 = 0x3017, // hey-dah-vah! summoning moths, looping

        [Tags(Long)]
        TwinMoldAppears = 0x3019,

        [Tags(Short, Long)]
        TwinMoldDamage = 0x301A,

        [Tags(Short)]
        DinolfosCry = 0x3029,

        [Tags(Short)]
        DinolfosAttack = 0x302A,

        [Tags(Short, LowHpBeep)]
        DinolfosLaugh = 0x302C, // dodgeing your arrows

        [Replacable(0xE89EA6, 0xE8A336, 0xE03C66, 0xE03E36, 0xE060FE)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        WolfosAppear = 0x303C,

        [Replacable(0xE041A2, 0xE04496, 0xE046B2, 0xE04876, 0xE0511A, 0xE05AD6, 0xE060BA, 0xE0628E, 0xE064A2)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        WolfosCry = 0x303E,

        [Replacable(0xE04AD6)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        WolfosAttack = 0x303F,

        [Replacable(0xE0523E)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        WolfosTakeDamage = 0x3043,

        [Replacable(0xE8A426, 0xE05B26)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        WolfosDie = 0x304B,

        [Tags(Short)]
        BombchuAim = 0x3055,

        [Tags(Short)]
        TektiteDamage = 0x306D,

        [Tags(Long)]
        TektiteDeath = 0x306E,

        [Tags(Short)]
        PoeAppear = 0x3073,

        [Tags(Short)]
        PoeDisappear = 0x3074,

        [Tags(Short)]
        PoeDamage = 0x3075,

        [Replacable(0xE95C6E, 0xE960FA, 0xD3809E, 0xD38502)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        MadScrubTakeDamage = 0x3080,

        [Replacable(0xE96196, 0xD386A6)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        MadScrubDie = 0x3081,

        [Tags(Short, LowHpBeep)]
        DekuFaint = 0x3082,

        [Tags(Short)]
        SkulltulaDead = 0x3085,

        [Tags(Short)]
        MajoraGrowHead = 0x3088,

        [Tags(Short)]
        DekuGuards = 0x3089,

        [Tags(Short)]
        AllDie = 0x308B,

        [Replacable(0xCEEF0A)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        WallmasterAim = 0x3090,

        [Tags(Short)]
        GoronCold = 0x30B4,

        [Replacable(0xE0DBBE, 0xE3BD7A, 0x00E3C002, 0x01048266, 0x01048416, 0x0104852E)]
        [Tags(Short)]
        [ReplacableByTags(Short)]
        GuayCroak = 0x30B6,

        [Tags(Short)]
        ChuchuBurst = 0x30BB, // Also used for powder keg goron's text

        [Replacable(0xCE57CE, 0xCE5A22)]
        [ReplacableByTags(Short)]
        [Tags(Short, LowHpBeep)]
        FrogRibbit = 0x30D2,

        [Replacable(0xCE50F2, 0xCE603A, 0xCE6F7E)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        FrogTakeDamage = 0x30D3,

        [Replacable(0xCE6026)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        FrogDie = 0x30D6,

        [Replacable(0xD4E78E, 0xD4E996, 0xD4EB9E, 0xD4EBF6, 0xD4EE1A, 0xD4EEAE, 0xD4F0DA, 0xD4F57A, 0xD4F81A, 0xD4FA26)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        RedeadMoan = 0x30E4,

        [Replacable(0xD4F3E2, 0xD4FF36)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        RedeadAim = 0x30E5,

        [Replacable(0xD4FD26)]
        [ReplacableByTags(Short)]
        [Tags(Short, LowHpBeep)]
        RedeadAttack = 0x30E8,

        [Tags(Short)]
        GoronKidSob = 0x30E9,

        [Replacable(0x00FB719E, 0x00FB72D6)]
        [Effect(0x000)]
        [Tags(Looping)]
        [ReplacableByTags(Long)]
        GoronKidCry = 0x30EA,

        [Replacable(0xE3C096)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        GuayDie = 0x30EB,

        [Tags(Short)]
        PoeLaugh = 0x30EC,

        [Tags(Short, Long)]
        SnapperShoutsGagrap = 0x30F3,

        [Tags(Short)]
        GoronWake = 0x30FC,

        [Tags(Short)]
        GoronSitStand = 0x30FD,

        [Tags(Short)]
        KotakePainShout = 0x3100, // don't you have something to help me???

        [Tags(Short)]
        KotakeDrinkFinished = 0x3101, // Hoy! as the a cloud of red potion leaves her mouth

        //[Tags(Short, Debug)]
        //KotakePotionSwallow = 0x3102, // potion drink swollow sound

        [Tags(Long)]
        KotakeWitchCackle = 0x3103,

        //[Tags(Short, Debug)]
        //KotakeSwirl = 0x3104, // looping, broom circling sfx before merging from OOT

        [Tags(Long)]
        KotakeBlastingOff = 0x3105,

        //[Tags(Long, Debug)]
        //KotakeConjureBroom = 0x3106, // looping, summon broom and then hovering on broom while talking

        [Tags(Long, Short)]
        BigGoronFallsAsleepFinal = 0x3107,

        [Tags(Long)]
        PoeLaughingRegular2 = 0x310A, // Poe laughing at you, regular version 2, slightly different, not skull kid though

        [Tags(Short)]
        SkullKidSurprised = 0x310D,

        //[Tags(Long, Debug)]
        //Unknown4 = 0x3111,  // kinda wet scraping sound

        [Tags(Short)]
        StallchildTakeDamage = 0x3112, // stallchild takes small damage

        //[Tags(Long, Debug)]
        //RegularEnemyDamage = 0x3114, // marked as for twinrova, but sounds like regular damage taken 

        [Tags(Long)]
        PoeLaughingRegular = 0x311A, // Poe laughing at you, regular version

        [Tags(Short)]
        GoronOh = 0x311C,

        [Tags(Long)]
        PoeLaughingSinister = 0x3122, // Poe laughing at you, sinister version (oot paintings)

        [Tags(Short)]
        IronKnuckleSwing = 0x3129,

        [Tags(Short)]
        FloorTapping = 0x3137,

        [Replacable(0xCE5E22, 0xCE717E)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        FrogBattleCry = 0x315C,

        [Tags(Short)]
        DekuSurprised = 0x318B,

        [Tags(Short)]
        DekuHappy = 0x318C,

        [Tags(Short)]
        DekuButler = 0x318F,

        [Tags(Long)]
        LikeLikeRoar = 0x3191, // sound of the like like trying to suck up link

        [Tags(Short, LowHpBeep)]
        PirateDamage = 0x3199,

        [Tags(Long)]
        PirateDead = 0x319A,

        [Replacable(0xCE5286)]
        [ReplacableByTags(Short)]
        [Tags(Short)]
        FrogVoice2 = 0x31A1,

        //[Tags(Looping)]
        //FreezardIceBreath = 0x31A4, // our code does not handle the automatic looping of this sample

        [Tags(Short, LowHpBeep)]
        FreezardDamage = 0x31A5,

        [Tags(Long)]
        FreezardDeath = 0x31A6,

        [Tags(Short)]
        DekuHurry = 0x31A7,

        [Tags(Short)]
        DekuKing = 0x31A8,

        [Tags(Short)]
        GoronSleepy = 0x31AD,

        //[Tags(Long, Debug)]
        //IronKnuckleUnknown = 0x31B0, // very short and quiet grunt

        [Tags(Short)]
        IronKnuckleDeathGroan = 0x31B1, // Iron Knuckle death 

        //[Tags(Short)]
        //MajoraWrathWhipSFX3 = 0x31CA,

        //[Tags(Short)]
        //MajoraWrathWhipSFX4 = 0x31CB,

        //[Tags(Long)]
        //MajoraWrathWhipEffect2 = 0x31CC,

        //[Tags(Long)]
        //MajoraWrathWhipEffect4 = 0x31CE,

        //[Tags(Long)]
        //MajoraWrathWhipSliding = 0x31CD, // Wrath Whip sliding on the ground

        [Tags(Long)]
        PeahatLiftoff = 0x31E7,

        [Tags(Short)]
        GoronCry = 0x31EB,

        //[Tags(Long)]
        //MajoraWrathWhip7 = 0x31EC,

        //[Tags(Long)]
        //MajoraWrathWhip8 = 0x31ED,

        //[Tags(Long)]
        //MajoraWrathWhip6 = 0x31EF,

        //[Tags(Long)]
        //MajoraWrathWhip5 = 0x31F1,

        //[Tags(Long)]
        //UnknownBugSfx = 0x31F2,

        [Tags(Long, Short)]
        KoumeShocked = 0x31FB, // skull kid kills koutake

        [Tags(Long)]
        ChuChuEmergeFromGround = 0x31FE,

        [Tags(Long)]
        KoumeSnoring = 0x31FF,

        [Tags(Short)]
        KoumeStartledAwake = 0x3200,

        [Tags(Long)]
        DekuLinkPanicScream = 0x3200, // when he sees his reflection

        [Tags(Short)]
        GoronSatisfied = 0x3204,

        [Tags(Short)]
        GoronYawn = 0x3218,

        [Replacable(0x1057882)]
        [ReplacableByTags(Long)] 
        [Tags(Long)]
        GoronSnore1 = 0x321A,

        [Replacable(0x10578A6)]
        [ReplacableByTags(Long)]
        //[Tags(Long)]           // this air sucking sound, we don't want it in the pool
        GoronSnore2 = 0x321B,

        //[Tags(Long)]
        //MajoraWrathSpinningTopLoop = 0x322C,

        [Tags(Short)]
        IgosShock = 0x322E,

        [Tags(Short, LowHpBeep)]
        IgosAttack = 0x3230,

        [Tags(Short)]
        IgosMinionLaugh3 = 0x3235,

        [Tags(Short)]
        IgosMinionLaugh2 = 0x3236,

        [Tags(Long)]
        IgosMinionLaugh = 0x3238,

        [Tags(Short)]
        KingIgosSwordSwing = 0x3239, // King igos swinging his sword at you

        [Tags(Long)]
        PoeLaugh2 = 0x3241, // laugh used by igos or his minions

        /*[Tags(Short)]
        MajoraWrathVengfulWhip = 0x3253,

        // too loud to use for lowhpbeep, if we could lower the volume it could work
        [Tags(Short)]
        MajoraWrathShortGaspPain = 0x3254,*/

        //[Tags(Short)]
        //MajoraWrathWhipHit = 0x3255,  // kinda annoying to hear in the world

        //[Tags(Short)]
        //MajoraWrathHorribleScreech = 0x3256,  // loud and annoying to hear

        [Tags(Long)]
        MajoraWrathLaughing = 0x3257,

        [Tags(Long, LowHpBeep)]
        MajoraWrathStunDamage = 0x3258, // damage that leads to a stun (arrow/beam)

        //[Tags(Long)]
        //MajoraWrathTakeDamage = 0x3259, // damage while in stun 

        [Tags(Long)]
        MajoraWrathDramaticDeath = 0x325A, // when the witch is splashed with water

        [Tags(Long)]
        MajoraIncantationTaunting = 0x3268,

        [Tags(Short, LowHpBeep)]
        UnknownShortQuack = 0x3269,

        [Tags(Short, LowHpBeep)]
        MajoraIncantationShortChant = 0x326A, // the first Coo of 'Coo CAh CAh, Coo CAh CAh, Coo CAh Coooo' chanting after transforming

        [Tags(Short, Long, LowHpBeep)]
        PirateLaugh = 0x3271, // smug laugh when they catch you pretending to be a stone

        [Tags(Short)]
        PirateCynical = 0x3272,

        //[Tags(Short, Long, Debug)]
        //PiratePain = 0x3273, // suffering damage after fight "Don't think this ends here -> escape"

        [Tags(Short)]
        PirateShout = 0x3274, // throws a deku nut to escape

        [Tags(Long)]
        SkullKidLaugh1 = 0x3275,

        [Tags(Long)]
        SkullKidLaugh2 = 0x3276,

        [Tags(Long)]
        SkullKidLaugh3 = 0x3277,

        [Tags(Short)]
        SkullKidCynical = 0x3279,

        [Tags(Short)]
        SkullKidAstonished = 0x327B,

        [Tags(Short)]
        SkullKidShy = 0x327E,

        [Tags(Short)]
        IgosMinionLaugh4 = 0x3288,

        [Tags(Short)]
        IgosMinionLaugh5 = 0x3289,

        [Tags(Short)]
        StalchildSurprised = 0x32A8,

        [Tags(Short, Long)]
        DeathScythe = 0x32AC,

        [Tags(Short, LowHpBeep)]
        DeathScythe2 = 0x32AD,

        [Tags(Long)]
        DeathLaughs = 0x32B0,

        [Tags(Short, Long)]
        DeathDamageTaken = 0x32B1,

        [Tags(Short)]
        GoronSorry = 0x32BB,

        [Tags(Short)]
        GoronCelebratingEverybody = 0x32BC,

        [Tags(Short)]
        GoronCelebratingSolo = 0x32BD,

        //[Tags(Short, Debug)]
        //DeathHeartBreak = 0x32B5, // I guess part of his death cinematic? not sure it was used

        [Tags(Short, Long)]
        CptKeetaAcceptingOrder = 0x32C5,

        [Tags(Long)]
        GaroMasterLaugh = 0x32C6,

        [Tags(Short)]
        KoumeHoi = 0x32C7,

        [Tags(Short)]
        GoronKidLaugh = 0x32C8,

        [Tags(Short)]
        GoronKidGreet = 0x32C9,

        [Tags(Short)]
        KoumeLaughing2 = 0x32CB, // off pitch koume laughing. these are marked as copies too

        [Tags(Long)]
        KoumeYeeheehee = 0x32CC,

        [Tags(Short)]
        Darmani1 = 0x32CD,

        [Tags(Short)]
        Darmani2 = 0x32CF,

        [Tags(Short)]
        DekuSirSir = 0x32D1,

        [Tags(Short)]
        DekuSir = 0x32D2,

        [Tags(Short)]
        Darmani3 = 0x32E8,

        [Tags(Short)]
        GoronComplain = 0x32FD,


        #endregion

        #region System SFX

        [Replacable(0x00B3D5C6)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        GetRupee = 0x4003,

        [Replacable(0x00C9294E, 0x00C96A12)]
        [Tags(SystemSound, Short, LowHpBeep)]
        [ReplacableByTags(SystemSound)]
        MenuSelect = 0x4008,

        [Replacable(0x00BABE6A)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        GetRecoveryHeart = 0x400B,

        [Tags(SystemSound, Short)]
        ZTargetAttention = 0x400C,

        [Tags(SystemSound, Short)]
        //[ReplacableByTags(Short, LowHpBeep)]
        CountDownWarning = 0x4019, // gossip stone countdown leading to take off

        CountDownWarningDire = 0x401A, // gossip stone countdown right before take off

        [Replacable(0x00B97E2A)]
        [Tags(LowHpBeep)]
        [ReplacableByTags(LowHpBeep)]
        LowHealthBeep = 0x401B,

        [Replacable(0x00DDE78E, 0x00DDF322, 0x1069EBA, 0x106788A)]
        [Tags(SystemSound)]
        [ReplacableByTags(Long)]
        TitleSelect = 0x4023,

        [Replacable(0x00B3D606)]
        [Tags(SystemSound, Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        GetSmallItem = 0x4024,

        [Replacable(0x00C86DE2, 0x00C7E8EA, 0x00C7EFD2, 0x00C80A62, 0x00C841EE, 0x00C84242, 0xC843BA, 0x00C84456, 0x00C8453E, 0xC8458A, 0x00C846FE, 0x00C84ABE, 0x00C86DE2, 0xC844DE, 0xC84A3E, 0xC8CE2A, 0xC84B3E, 0xC81312, 0xC7F92E)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        FileSelectCursor = 0x4039,

        [Replacable(0x00C83E1A)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        FileSelectTypeCharacter = 0x403A,

        [Replacable(0x00C83ABE, 0xC7E8B2, 0xC7EEE2, 0xC7F91E, 0xC7FFEA, 0xC80A2A, 0xC86B26, 0xC86ACE, 0xC86C7E, 0xC8407A, 0xC8CD8E, 0xC7EF16, 0xC84B76, 0xC84996)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        FileSelectDecideLong = 0x403B,

        [Replacable(0xC83C3E, 0xC83BEA, 0xC83FEA, 0xC8CD76, 0xC8CDE6, 0xC7E716, 0xC80886, 0xC7EE0A, 0xC7F80A, 0xC8120E, 0xC81982, 0xC83B4A)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        FileSelectCancel = 0x403C,

        [Replacable(0xC84076, 0xC86D7E, 0xC80A3A, 0xC7E8C2)]
        [Tags(SystemSound)]
        [ReplacableByTags(Short)]
        FileSelectError = 0x403D,

        [Tags(Short, LowHpBeep)]
        CuccoClock = 0x4046, // NA_SE_EV_FAIVE_LUPY_COUNT (gossip stone giving time of day)

        [Tags(Long)]
        [ReplacableByTags(Long)]
        BoatCruiseAnnouncement = 0x404E,

        [Tags(Long)]
        [ReplacableByTags(Long)]
        LotteryWinner = 0x4053,

        #endregion

        #region Voice SFX

        [Tags(Short)]
        FierceDeityLinkAttack = 0x6000,

        [Tags(Short)]
        FierceDeityLinkJumpAttack = 0x6001,

        [Tags(Short, LowHpBeep)]
        FierceDeityTakeDamage = 0x6005,

        [Tags(Short)]
        FierceDeityFrozen = 0x6006,

        [Tags(Long, LowHpBeep)]// for comedic effect
        FierceDeityFallLong = 0x6008,

        [Tags(LowHpBeep)]  // if we could raise the volume, it could have the short tag
        FierceDietyPantLowHealth = 0x6009,

        [Tags(Short)]
        ChildLinkAttack = 0x6020,

        [Tags(Short)]
        ChildLinkJumpAttack = 0x6021,

        [Tags(Short, LowHpBeep)]
        ChildLinkGrabLedge = 0x6023,

        [Tags(Short, LowHpBeep)]
        ChildLinkMountLedge = 0x6024,

        [Tags(Short, LowHpBeep)]
        ChildLinkTakeDamage = 0x6025,

        [Tags(LowHpBeep)] // its kinda quiet to use for anything short
        ChildLinkPantLowHealth = 0x6029,

        [Tags(Short, Long)]
        ChildLinkFallDamage = 0x603A,

        [Tags(Short, Long)]
        ChildLinkKnockedOffHorse = 0x603E,

        [Replacable(0x00BABCF6)]
        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        TatlEnemyAlert = 0x6043,

        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        TatlMessage = 0x6045,

        [Tags(Short)]
        TatlSwoosh = 0x6046,

        [Tags(Short)]
        TaelSwoosh = 0x6047,

        [ReplacableByTags(Short)]
        TatlNarration = 0x6050,

        [Tags(Short)]
        TaelStory = 0x6051,

        [Replacable(0xD6DA1E)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        GormanBrosLongYell = 0x6054,

        [Replacable(0xD6DC72, 0xD6FF12)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        GormanBrosLost = 0x6055,

        [Tags(Short, LowHpBeep)]
        GormanBrosWhip1 = 0x6056,

        [Tags(Short, LowHpBeep)]
        GormanBrosWhip2 = 0x6057,

        [Tags(Long)]
        GreatFairyAppears = 0x6058,

        [Tags(Short, LowHpBeep)]
        GreatFairyLaugh = 0x6059,

        [Tags(Short)]
        ReceptionistGiggle = 0x6078,

        [Replacable(0xD6DC82, 0xD6FF36)]
        [ReplacableByTags(Long)]
        [Tags(Long)]
        GormanBrosLaugh = 0x607C,

        [Tags(Short, LowHpBeep)]
        DekuFrozenDamage = 0x6086,

        [Tags(Short, LowHpBeep)]
        DekuFallShort = 0x6087,

        [Tags(Long)]
        DekuFallLong = 0x6088,

        [Tags(Short, Long)]
        DekuFallDamage = 0x609A,

        [Tags(Short, LowHpBeep)]
        DekuHorror = 0x6096,

        [Tags(Short, Long, LowHpBeep)]
        ZoraFallDamage = 0x60BA,

        [Tags(Short, LowHpBeep)]
        GoronPunch = 0x60C0,

        [Tags(Short, LowHpBeep)]
        GoronBonk = 0x60C4,

        [Tags(Short, LowHpBeep)]
        GoronFallShort = 0x60C7,

        [Tags(Long)]
        GoronFallLong = 0x60C8,

        [Tags(Short, Long)]
        GoronFallDamage = 0x60DA, // might also get reused as goron recognizing darmani

        [Tags(Short, LowHpBeep)]
        JimHeh = 0x6100,

        [Tags(Short, LowHpBeep)]
        BomberGiggle = 0x6101,

        [Tags(Short, LowHpBeep)]
        JimHuh = 0x6102,

        [Tags(Short, LowHpBeep)]
        BomberEhh = 0x6103,

        [Tags(Short)]
        BomberGuard = 0x6104,

        [Tags(Short)]
        BomberSurprise = 0x6105,

        //[Tags(Short, Debug)]
        //HagVoice1Unused = 0x6106,  // japanese iiya "No!" (suprised)

        [Tags(Long)]
        GrandmaRobbedShout = 0x6107, // Just as sakon contacts

        [Tags(Short)]
        GrandmaRobbedHelpMe = 0x6108, // as sakon is getting away "someone please stop him"

        [Tags(Short)]
        GrandmaRobbedAcceptance = 0x6109, // you failed to stop sakon, or blew him up, either way shes tired

        [Tags(Long)]
        GrandmaChuckle = 0x610A,

        [Tags(Short)]
        GrandmaOhIsee = 0x610B, // sounds like you explained something to her and she understands now

        [Tags(Short)]
        MadameAromaHello = 0x610D,

        [Tags(Short)]
        MadameAromaLaugh = 0x610E,

        [Tags(Short)]
        MamamuYanWhat = 0x610F,

        [Tags(Short)]
        MamamuYanHmph = 0x6110,

        [Tags(Short)]
        MamamuYanAnnoyed = 0x6111,

        [Tags(Short, LowHpBeep)]
        MamamuYanCelebratory = 0x6112,

        [Tags(Short)]
        MamamuYanRefund = 0x6113,

        [Tags(Short)]
        MamamuYanReject = 0x6114,

        [Tags(Short)]
        AveilFrustrated = 0x6115,

        [Tags(Short, Long, LowHpBeep)]
        AveilLaugh = 0x6116,

        [Tags(Short)]
        PirateScream1 = 0x6118,

        [Tags(Short)]
        PirateScream2 = 0x6119,

        //[Tags(Short)]
        //PirateScreamTerror = 0x611A, // ??? never heard in game?

        //[Tags(Short, Debug)]
        //PirateScreamLongYell = 0x611B, // ??? never heard in game?

        [Tags(Short)]
        RosaSigh1 = 0x611C,

        [Tags(Short)]
        RosaGiggle1 = 0x611D,

        [Tags(Short)]
        RosaSigh2 = 0x611E,

        [Tags(Short)]
        RosaGiggle2 = 0x611F,

        [Tags(Short)]
        RosaAnnoyed = 0x6120, // stop interupting our dance

        [Tags(Short)]
        RosaLaugh = 0x6121,

        [Tags(Short)]
        AnjuSigh1 = 0x6122,  // do you have a reservation?

        [Tags(Short)]
        AnjuSurprised = 0x6123,

        [Tags(Short)]
        AnjuSigh2 = 0x6124,  // you do have a reservation? that's good

        [Tags(Short)]
        AnjuShocked = 0x6125, // you're looking for kafei too?

        [Tags(Short, LowHpBeep)]
        CremiaInquisitive = 0x6126,

        [Tags(Short, LowHpBeep)]
        CremiaAnnoyed = 0x6127,

        [Tags(Short, LowHpBeep)]
        CremiaGiggle = 0x6128,

        [Tags(Short, LowHpBeep)]
        CremiaSurprised = 0x6129,

        [Tags(Short, Long)]
        ReceptionistMmHmm = 0x612A,

        [Tags(Short, Long)]
        ReceptionistSwoon = 0x612B,

        [Tags(Short, Long)]
        ReceptionistMmm = 0x612C,

        // too annoying for lowhpbeep
        [Tags(Short)]
        RomaniScream = 0x612D,

        [Tags(Short, LowHpBeep)]
        RomaniGiggle = 0x612E,

        [Tags(Short)]
        RomaniYeah = 0x612F,   // romani impressed with link's archery

        /*[Tags(Short)]
        PamelaScream = 0x6130,

        [Tags(Long)]
        PamelaFather = 0x6131,

        [Tags(Short, LowHpBeep)]
        PamelaSniffle = 0x6132,*/

        [Tags(Short, LowHpBeep)]
        DekuPrincessGasp = 0x6133, // unused I think? kind of a "taken aback" gasp

        [Tags(Short)]
        DekuPrincessGiggle = 0x6134, // I think this was unused, I don't remember hearing it in game

        [Tags(Short)]
        DekuPrincessHmph = 0x6135,  // more of a cough

        [Tags(Short)]
        DekuPrincessAngerBuilding = 0x6136, // second half of outburst at king, before jumping

        [Tags(Short)]
        DekuPrincessStressed = 0x6137, // bottle talking, and start of angry outburst at king

        [Tags(Short, Long)]
        StrayFairyHelpMe = 0x6138,

        [Tags(Short, LowHpBeep)]
        AnjuIrritated = 0x6139,

        [Tags(Short)]
        AnjuGiggle = 0x613A,

        [Tags(Short)]
        PamelaGasp = 0x613B,

        [Tags(Long)]
        RomaniHmmmm = 0x6140,

        [Tags(Short, LowHpBeep)]
        RomaniYesSisterSigh = 0x6141,

        [Tags(Short, Long)]
        DampeAfraid = 0x6143,

        [Tags(Short, Long)]
        DampeUrgh = 0x6144,

        [Tags(Short)]
        DampeQuestion = 0x6145,

        [Tags(Short)]
        ShikashiOh = 0x6146,

        [Tags(Long)]
        ShikashiLaugh = 0x6147,

        [Tags(Long)]
        MarineScientistDisgruntled = 0x6148,

        [Tags(Short)]
        MarineScientistHuh = 0x6149,

        [Tags(Long)]
        MarineScientistExcited = 0x614A,

        [Tags(Short, LowHpBeep)]
        SwampTouristProprietorHehHeh = 0x614B,

        [Tags(Short)]
        ShootingGalleryHuh = 0x614C,

        [Tags(Short)]
        ShootingGallerySurprised = 0x614D,

        [Tags(Short)]
        ShootingGalleryDisappointed = 0x614E,

        [Tags(Short, LowHpBeep)]
        MutohScoff = 0x614F,

        [Tags(Short)]
        TotoHemHaw = 0x6150,

        [Tags(Long, LowHpBeep)]
        CuriosityShopGuyHii = 0x6151,

        [Tags(Long, Short, LowHpBeep)]
        CuriosityShopGuyLaugh = 0x6152,

        [Tags(Long, Short)]
        CuriosityShopGuyRefuse = 0x6153, // we don't serve bunny people here

        [Tags(Short, Long)]
        KamaroOoohhh = 0x6154,

        [Tags(Short, Long)]
        KamaroAaagh = 0x6155,

        [Tags(Long, Short)]
        JuglerChuckleOhHoHoHo = 0x6156,

        [Tags(Short)]
        JuglerMyMy = 0x6157,

        [Tags(Long)]
        MayorSlowHmmm = 0x6158,

        [Tags(Long, LowHpBeep)]
        [ReplacableByTags(Long)]
        GuruGuruLalala = 0x6159,

        [Tags(Short)]
        SwordSchoolTrainerHmm = 0x615A,

        [Tags(Short)]
        SwordSchoolTrainerKaah = 0x615B,

        [Tags(Short)]
        SwordSchoolTrainerEhh = 0x615C,

        [Tags(Short)]
        DarlingLaugh = 0x615F,

        [Tags(Short)]
        ShootingGalleryDisappointed2 = 0x6160,

        [Tags(Short)]
        ZuboraShaddup = 0x615D,

        [Tags(Short)]
        ZuboraOh = 0x615E,

        [Tags(Short, LowHpBeep)]
        PamelaFatherGasp = 0x6161,

        [Tags(Short, LowHpBeep)]
        PamelaFatherPamela = 0x6162,

        [Tags(Short)]
        PamelaFatherSurprised = 0x6163,

        [Tags(Short)]
        PamelaFatherInterested = 0x6164,

        [Tags(Short, LowHpBeep)]
        GaboraUgogh = 0x6165,

        [Tags(Short, LowHpBeep)]
        GaboraHurrgh = 0x6166,

        [Tags(Short)]
        BeanManAhh = 0x6167,

        [Tags(Short)]
        BeanManMmm = 0x6168,

        [Tags(Short)]
        CarpenterScoff = 0x6169,

        [Tags(Short)]
        CarpenterSigh = 0x616A,

        [Tags(Short)]
        JapasHah = 0x616B,

        [Tags(Long, LowHpBeep)] // quiet enough to work for lowhp
        GrogSigh = 0x616C,

        [Tags(Long)] // too quiet to use in my opinion
        GrogHehHeh= 0x616D,


        [Tags(Short, LowHpBeep)]
        ViscenAhQuestion = 0x616E, // the guard in the meeting "have you not noticed the giant moon???"

        [Tags(Short, LowHpBeep)]
        [ReplacableByTags(Short)]
        PostmanGreetingYah = 0x616F,

        [Tags(Short)]
        DarlingChuckle = 0x6170,

        [Tags(Short)]
        DarlingMmm = 0x6171,

        [Tags(Short)]
        MikauDying1 = 0x6172,

        [Tags(Short)]
        MikauDying2 = 0x6173,

        [Tags(Short)]
        MikauOwww = 0x6174, // also used by toilet hand "p p paper!"

        [Tags(Long, Short, LowHpBeep)]
        MikauBaybee = 0x6175,

        [Tags(Short, Long, LowHpBeep)]
        MikauYay = 0x6176, // also used by toilet hand

        [Tags(Long)]
        TingleFall = 0x6177,

        [Tags(Short)]
        TingleGasp = 0x6179,

        [Tags(Short, Long)]
        TingleChuckle = 0x617A,

        [Tags(Long)]
        TingleHappy = 0x617B,

        [Tags(Long)]
        TingleKoolooLimpah = 0x617C,

        [Tags(Short)]
        HMSChuckle = 0x617D,  // you've met with a terrible fate, haven't you

        [Tags(Long)]
        HMSOhohoho = 0x617e,  // I'm sure you will persevere

        [Tags(Long)]
        HMSHoHoHo = 0x617F,  // I hope you don't mind but I've been following you

        [Tags(Short)]
        GormanAngryEh = 0x6180,  // EH?! bother, bother, I am busy

        [Tags(Long)]
        UnknownMaleSorrowOoohhhh = 0x6181,  // ??? Not sure this was ever used

        [Tags(Short, Long)]
        UnknownMaleCuriousQuestionHmmm = 0x6182,  // ??? Not sure this was ever used

        [Tags(Long)]
        UnknownMaleIThinkIUnderstandUmmmHmm = 0x6183,  // ??? Not sure this was ever used

        #endregion
    }
}
