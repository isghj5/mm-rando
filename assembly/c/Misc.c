#include <stdbool.h>
#include <z64.h>
#include "Misc.h"

struct MiscConfig MISC_CONFIG = {
    .magic = MISC_CONFIG_MAGIC,
    .version = 3,
};

union FaucetSpeed {
    struct {
        u16 acceleration;
        u16 maxVelocity;
    };
    u32 all;
};

struct IceblockSpeed {
    f32 initial;
    f32 additive;
    f32 clamp;
};

struct ShelfSpeed {
    f32 multiplier;
    f32 additive;
};

struct IkanaSpeed {
    f32 maxVelocity;
    f32 initial;
};

bool Misc_CanUseOcarinaUnderwater(void) {
    return MISC_CONFIG.flags.ocarinaUnderwater != 0;
}

/**
 * Hook function to get speed of pushblock.
 **/
f32 Misc_GetPushBlockSpeed(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.fastPush) {
        return 2.0;
    } else {
        return 6.0;
    }
}

/**
 * Hook function to get speed of iceblock.
 **/
void Misc_GetIceblockPushSpeed(Actor* actor, GlobalContext* ctxt, struct IceblockSpeed* dest) {
    if (!MISC_CONFIG.flags.fastPush) {
        dest->initial = 1.2;
        dest->additive = 2.8;
        dest->clamp = 3.5;
    } else {
        dest->initial = 3.6;
        dest->additive = 8.4;
        dest->clamp = 10.5;
    }
}

/**
 * Hook function to get speed of Great Bay Temple faucets.
 **/
u32 Misc_GetGreatBayTempleFaucetSpeed(Actor* actor, GlobalContext* ctxt) {
    union FaucetSpeed result;
    if (!MISC_CONFIG.flags.fastPush) {
        result.acceleration = 1;
        result.maxVelocity = 5;
    } else {
        result.acceleration = 2;
        result.maxVelocity = 30;
    }
    return result.all;
}

/**
 * Hook function to get speed of Oceanside Spider House shelves.
 **/
void Misc_GetSpiderHouseShelvesSpeed(Actor* actor, GlobalContext* ctxt, struct ShelfSpeed* dest, int shelfType) {
    if (shelfType == 0) {
        // Small shelves
        if (!MISC_CONFIG.flags.fastPush) {
            dest->multiplier = 0.012;
            dest->additive = 0.014;
        } else {
            dest->multiplier = 0.036;
            dest->additive = 0.042;
        }
    } else {
        // Large shelves
        if (!MISC_CONFIG.flags.fastPush) {
            dest->multiplier = 0.003;
            dest->additive = 0.009;
        } else {
            dest->multiplier = 0.009;
            dest->additive = 0.027;
        }
    }
}

/**
 * Hook function to get speed of Oceanside Spider House shelves (when pulled outward, pushed inward).
 **/
f32 Misc_GetSpiderHouseShelvesOutwardSpeed(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.fastPush) {
        return 0.022;
    } else {
        return 0.066;
    }
}

/**
 * Hook function to get speed of Ikana pushblock.
 **/
void Misc_GetIkanaPushblockSpeed(Actor* actor, GlobalContext* ctxt, struct IkanaSpeed* dest) {
    if (!MISC_CONFIG.flags.fastPush) {
        dest->maxVelocity = 2.0;
        dest->initial = 0.4;
    } else {
        dest->maxVelocity = 8.0;
        dest->initial = 1.6;
    }
}

/**
 * Hook function to get speed of Pzlblock actor (Woodfall Temple pushblock, Sakon's Hideout pushblocks).
 **/
f32 Misc_GetPzlblockSpeed(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.fastPush) {
        return 2.3;
    } else {
        return 4.6;
    }
}

/**
 * Hook function to get speed of Darmani's Gravestone.
 **/
u32 Misc_GetGravestoneSpeed(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.fastPush) {
        return 1;
    } else {
        return 3;
    }
}

/**
 * Hook function to get speed multiplier used for pushing an actor in water (pushing Mikau to shore).
 **/
f32 Misc_GetInWaterPushSpeed(ActorPlayer* player, Actor* actor) {
    if (!MISC_CONFIG.flags.fastPush) {
        return 0.5;
    } else {
        return 1.5;
    }
}

/**
 * Hook function to check whether or not to perform crit wiggle.
 **/
bool Misc_CritWiggleCheck(Camera* camera, s16 health) {
    switch (MISC_CONFIG.flags.critWiggle) {
        case CRIT_WIGGLE_ALWAYS_ON:
            return true;
        case CRIT_WIGGLE_ALWAYS_OFF:
            return false;
        case CRIT_WIGGLE_DEFAULT:
        default:
            return health <= 0x10;
    }
}

bool Misc_GetVanillaLayout(void) {
    return MISC_CONFIG.internal.vanillaLayout;
}

void Misc_Init(void) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        // Mod files with code required for freestanding models are not included if using vanilla layout.
        MISC_CONFIG.flags.freestanding = 0;
        MISC_CONFIG.flags.shopModels = 0;
    }
}
