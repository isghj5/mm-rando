#include "misc.h"
#include "z2.h"

struct misc_config MISC_CONFIG = {
    .magic = MISC_CONFIG_MAGIC,
    .version = 0,

    // Version 0 flags
    .crit_wiggle = CRIT_WIGGLE_DEFAULT,
    .draw_hash = 1,
    .fast_push = 1,
    .ocarina_underwater = 1,
    .quest_item_storage = 1,

    // Version 1 flags
    .close_cows = 1,
    .freestanding = 1,
    .quest_consume = QUEST_CONSUME_DEFAULT,
};

union faucet_speed {
    struct {
        u16 acceleration;
        u16 max_velocity;
    };
    u32 all;
};

struct iceblock_speed {
    f32 initial;
    f32 additive;
    f32 clamp;
};

struct shelf_speed {
    f32 multiplier;
    f32 additive;
};

struct ikana_speed {
    f32 max_velocity;
    f32 initial;
};

struct misc_config* misc_get_config() {
    return &MISC_CONFIG;
}

bool misc_can_use_ocarina_underwater() {
    return MISC_CONFIG.ocarina_underwater != 0;
}

/**
 * Hook function to get speed of pushblock.
 **/
f32 misc_get_push_block_speed(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.fast_push) {
        return 2.0;
    } else {
        return 6.0;
    }
}

/**
 * Hook function to get speed of iceblock.
 **/
void misc_get_iceblock_push_speed(z2_actor_t *actor, z2_game_t *game, struct iceblock_speed *dest) {
    if (!MISC_CONFIG.fast_push) {
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
u32 misc_get_great_bay_temple_faucet_speed(z2_actor_t *actor, z2_game_t *game) {
    union faucet_speed result;
    if (!MISC_CONFIG.fast_push) {
        result.acceleration = 1;
        result.max_velocity = 5;
    } else {
        result.acceleration = 2;
        result.max_velocity = 30;
    }
    return result.all;
}

/**
 * Hook function to get speed of Oceanside Spider House shelves.
 **/
void misc_get_spider_house_shelves_speed(z2_actor_t *actor, z2_game_t *game, struct shelf_speed *dest, int shelf_type) {
    if (shelf_type == 0) {
        // Small shelves
        if (!MISC_CONFIG.fast_push) {
            dest->multiplier = 0.012;
            dest->additive = 0.014;
        } else {
            dest->multiplier = 0.036;
            dest->additive = 0.042;
        }
    } else {
        // Large shelves
        if (!MISC_CONFIG.fast_push) {
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
f32 misc_get_spider_house_shelves_outward_speed(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.fast_push) {
        return 0.022;
    } else {
        return 0.066;
    }
}

/**
 * Hook function to get speed of Ikana pushblock.
 **/
void misc_get_ikana_pushblock_speed(z2_actor_t *actor, z2_game_t *game, struct ikana_speed *dest) {
    if (!MISC_CONFIG.fast_push) {
        dest->max_velocity = 2.0;
        dest->initial = 0.4;
    } else {
        dest->max_velocity = 8.0;
        dest->initial = 1.6;
    }
}

/**
 * Hook function to get speed of Pzlblock actor (Woodfall Temple pushblock, Sakon's Hideout pushblocks).
 **/
f32 misc_get_pzlblock_speed(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.fast_push) {
        return 2.3;
    } else {
        return 4.6;
    }
}

/**
 * Hook function to get speed of Darmani's Gravestone.
 **/
u32 misc_get_gravestone_speed(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.fast_push) {
        return 1;
    } else {
        return 3;
    }
}

/**
 * Hook function to get speed multiplier used for pushing an actor in water (pushing Mikau to shore).
 **/
f32 misc_get_in_water_push_speed(z2_link_t *link, z2_actor_t *actor) {
    if (!MISC_CONFIG.fast_push) {
        return 0.5;
    } else {
        return 1.5;
    }
}

/**
 * Hook function to check whether or not to perform crit wiggle.
 **/
bool misc_crit_wiggle_check(z2_camera_t *camera, s16 health) {
    switch (MISC_CONFIG.crit_wiggle) {
        case CRIT_WIGGLE_ALWAYS_ON:
            return true;
        case CRIT_WIGGLE_ALWAYS_OFF:
            return false;
        case CRIT_WIGGLE_DEFAULT:
        default:
            return health <= 0x10;
    }
}
