#include "z2.h"
#include "misc.h"

s16 don_gero_start_cutscene(s16 index, z2_actor_t *actor) {
    if (MISC_CONFIG.speedups.don_gero && index == 0x17) {
        u8 *address = (u8*)actor;
        u16 *rollingAddress = (u16*)(address + 0x4AA);
        *rollingAddress = 0x180;
    } else {
        z2_ActorCutscene_StartAndSetFlag(index, actor);
    }
}

z2_xyzf_t roll_targets[6] = {
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

z2_xyzf_t *don_gero_get_roll_target(s16 index) {
    if (index < 6) {
        return &roll_targets[index];
    }
    return NULL;
}
