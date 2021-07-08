#include <z64.h>
#include "Misc.h"

static Vec3f rollTargets[6] = {
    {
        .x = -550,
        .y = 8,
        .z = 550
    },
    {
        .x = 220,
        .y = 43,
        .z = 525
    },
    {
        .x = 960,
        .y = 90,
        .z = 525
    },
    {
        .x = 2060,
        .y = 16,
        .z = 925
    },
    {
        .x = 2710,
        .y = -40,
        .z = 1980
    },
    {
        .x = 3510,
        .y = -90,
        .z = 2380
    }
};

/**
 * Hook function used to get the roll target position of the goron.
 **/
Vec3f* DonGero_GetRollTarget(s16 index) {
    if (index < 6) {
        return &rollTargets[index];
    }
    return NULL;
}

/**
 * Hook function used to override starting the cutscene.
 **/
s16 DonGero_StartCutscene(s16 index, Actor* actor) {
    if (MISC_CONFIG.speedups.donGero && index == 0x17) {
        u8* address = (u8*)actor;
        u16* rollingAddress = (u16*)(address + 0x4AA);
        *rollingAddress = 0x180;
    } else {
        z2_ActorCutscene_StartAndSetFlag(index, actor);
    }
}
