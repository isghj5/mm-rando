#ifndef MISC_H
#define MISC_H

#include <z64.h>

enum CritWiggleState {
    CRIT_WIGGLE_DEFAULT,
    CRIT_WIGGLE_ALWAYS_ON,
    CRIT_WIGGLE_ALWAYS_OFF,
};

enum QuestConsumeState {
    QUEST_CONSUME_DEFAULT,
    QUEST_CONSUME_ALWAYS,
    QUEST_CONSUME_NEVER,
};

enum AutoInvertState {
    AUTO_INVERT_NEVER,
    AUTO_INVERT_FIRST_CYCLE,
    AUTO_INVERT_ALWAYS,
};

enum ChestGameMiniMapState {
    CHESTGAME_MINIMAP_OFF,
    CHESTGAME_MINIMAP_MINIMAL,
    CHESTGAME_MINIMAP_CONDITIONAL,
    CHESTGAME_MINIMAP_SPOILER,
};

// Magic number for misc_config: "MISC"
#define MISC_CONFIG_MAGIC 0x4D495343

typedef struct {
    // Version 0 flags
    u32 critWiggle          : 2;
    u32 drawHash            : 1;
    u32 fastPush            : 1;
    u32 ocarinaUnderwater   : 1;
    u32 questItemStorage    : 1;
    // Version 1 flags
    u32 closeCows           : 1;
    u32 questConsume        : 2;
    u32 arrowCycle          : 1;
    u32 arrowMagicShow      : 1;
    // Version 2 flags
    u32 elegySpeedup        : 1;
    u32 continuousDekuHop   : 1;
    u32 progressiveUpgrades : 1;
    u32 iceTrapQuirks       : 1;
    u32 mikauEarlyBeach     : 1;
    u32 fairyChests         : 1;
    u32 targetHealth        : 1;
    u32 climbAnything       : 1;
    u32 freeScarecrow       : 1;
    u32 fillWallet          : 1;
    u32 autoInvert          : 2;
    u32 hiddenRupeesSparkle : 1;
    u32 saferGlitches       : 1;
    u32 bombchuDrops        : 1;
    u32 instantTransform    : 1;
    u32 bombArrows          : 1;
    u32 giantMaskAnywhere   : 1;
    u32 fewerHealthDrops    : 1;
    u32 ironGoron           : 1;
    u32 easyFrameByFrame    : 1;
    u32 fairyMaskShimmer    : 1;
    u32 skullTokenSounds    : 1;
    u32 takeDamageOnEpona   : 1;
    u32 takeDamageOnShield  : 1;
    u32 takeDamageFromVoid  : 1;
    u32 oceanTokensRandomized : 1;
    u32 moonCrashFileErase  : 1;
    u32                     : 25;
} MiscFlags;

typedef union {
    u8 bytes[16];
    u32 value;
    u32 words[4];
} MiscHash;

typedef struct {
    // Version 1 flags
    u32 vanillaLayout             : 1;
    u32 victoryDirectToCredits    : 1;
    u32 victoryCantFightMajora    : 1;
    u32 victoryFairies            : 1;
    u32 victorySkullTokens        : 1;
    u32 victoryNonTransformMasks  : 1;
    u32 victoryTransformMasks     : 1;
    u32 victoryNotebook           : 1;
    u32 victoryHearts             : 1;
    u32 victoryBossRemainsCount   : 3;
    u32                           : 20;
} MiscInternal;

typedef struct {
    // Version 3 flags
    u32 soundCheck          : 1;
    u32 blastMaskThief      : 1;
    u32 fishermanGame       : 1;
    u32 boatArchery         : 1;
    u32 donGero             : 1;
    u32 fastBankRupees      : 1;
    u32 doubleArchery       : 1;
    u32 multiBank           : 1;
    u32 shortChestOpening   : 1;
    u32 chestGameMinimap    : 2;
    u32 skipGiantsCutscene  : 1;
    u32 oathHint            : 1;
    u32                     : 19;
} MiscSpeedups;

typedef struct {
    u16 collectableTableFileIndex;
    u16 bankWithdrawFee;
} MiscShorts;

typedef struct {
    u8 npcKafeiReplaceMask;
    u8 requiredBossRemains;
    u8 pad[2];
} MiscBytes;

typedef struct {
    u32 freestanding        : 1;
    u32 drawDonGeroMask     : 1;
    u32 drawPostmanHat      : 1;
    u32 drawMaskOfTruth     : 1;
    u32 drawGaroMask        : 1;
    u32 drawPendant         : 1;
    u32 shopModels          : 1;
    u32                     : 25;
} MiscDrawFlags;

typedef struct {
    /* 0x00 */ u16 oldObjectId;
    /* 0x02 */ u8 oldGraphicId;
    /* 0x03 */ u8 padding03;
    /* 0x04 */ u16 newObjectId;
    /* 0x06 */ u16 padding06;
    /* 0x08 */ u32 displayListOffset;
} MiscSmithyModel; // size = 0xC;

struct MiscConfig {
    /* 0x00 */ u32 magic;
    /* 0x04 */ u32 version;
    /* 0x08 */ MiscHash hash;
    /* 0x18 */ MiscFlags flags;
    /* 0x20 */ MiscInternal internal;
    /* 0x24 */ MiscSpeedups speedups;
    /* 0x28 */ MiscShorts shorts;
    /* 0x2C */ MiscBytes MMRbytes;
    /* 0x30 */ MiscDrawFlags drawFlags;
    /* 0x34 */ MiscSmithyModel smithyModels[10];
}; // size = 0x34

extern struct MiscConfig MISC_CONFIG;

void Misc_Init(void);

#endif // MISC_H
