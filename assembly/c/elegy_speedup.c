#include <stdbool.h>
#include "misc.h"
#include "z2.h"

union elegy_lock_params {
    struct {
        u16 frame_count;
        u16 spawn_frame;
    };
    u32 value;
};

/**
 * Hook function called to get the movement speed of the Stone Tower blocks.
 **/
f32 elegy_speedup_get_block_speed(z2_actor_t *actor, z2_game_t *game, int type) {
    if (type == 0) {
        // Get speed when moving into new spot.
        if (!MISC_CONFIG.elegy_speedup) {
            return 20.0;
        } else {
            return 40.0;
        }
    } else {
        // Get speed when moving back to original spot.
        return 40.0;
    }
}

/**
 * Hook function called to get number of frames to lock input during Elegy cutscene, and which
 * frame the statue should begin spawning on.
 **/
u32 elegy_speedup_get_lock_params(z2_link_t *link, z2_game_t *game) {
    if (!MISC_CONFIG.elegy_speedup) {
        union elegy_lock_params result = {
            .frame_count = 0x5B,
            .spawn_frame = 0x0A,
        };
        return result.value;
    } else {
        union elegy_lock_params result = {
            .frame_count = 0x01,
            .spawn_frame = 0x01,
        };
        return result.value;
    }
}

/**
 * Hook function called to get the number of frames before beginning to fade out the current Elegy
 * statue when moving it to a different position.
 **/
u16 elegy_speedup_get_statue_despawn_counter(z2_game_t *game, z2_link_t *link) {
    if (!MISC_CONFIG.elegy_speedup) {
        return 0x14;
    } else {
        return 0x01;
    }
}

/**
 * Hook function called to get the speed at which the Elegy statue fades in or out.
 **/
u16 elegy_speedup_get_statue_fade_speed(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.elegy_speedup) {
        return 0x8;
    } else {
        return 0x20;
    }
}

/**
 * Hook function called to check whether or not the camera should update for the Elegy cutscene.
 **/
bool elegy_speedup_should_update_camera(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.elegy_speedup) {
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function called to check whether or not the scene should darken for the Elegy cutscene.
 **/
bool elegy_speedup_should_darken(z2_actor_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.elegy_speedup) {
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function called to sync the Elegy effect's position with that of its respective statue.
 **/
void elegy_speedup_update_effect_position(z2_actor_t *actor, z2_game_t *game) {
    if (MISC_CONFIG.elegy_speedup) {
        // Get Elegy statue actor from main actor context struct.
        u8 form_index = actor->variable & 7;
        z2_actor_t *statue = game->actor_ctxt.elegy_statues[form_index];
        if (statue != NULL) {
            // Sync position of Eff_Change effect with the statue.
            actor->pos_2 = statue->pos_2;
        }
    }
}
