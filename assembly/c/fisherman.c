#include <stdbool.h>
#include "misc.h"
#include "z2.h"

/**
 * Hook function used to check if the Fisherman's Game should end.
 *
 * If speedup is enabled, checks if player has enough points to end early.
 **/
bool fisherman_should_end_game(z2_actor_t *actor, z2_game_t *game, u32 timer_hi, u32 timer_lo) {
    bool timeout = (timer_hi == 0 && timer_lo == 0);
    return ((MISC_CONFIG.speedups.fisherman_game && z2_file.minigame_counter >= 20) || timeout);
}

/**
 * Hook function used to check if the timer satisfies giving the player a reward.
 *
 * If speedup is enabled the minigame ends early, so ignores the timer check.
 **/
bool fisherman_should_pass_timer_check(z2_actor_t *actor, z2_game_t *game, u32 timer_hi, u32 timer_lo) {
    if (MISC_CONFIG.speedups.fisherman_game) {
        return true;
    } else {
        return timer_hi == 0 && timer_lo == 0;
    }
}

/**
 * Helper function for checking if the boat is within a specific radius of a point near the
 * platform.
 **/
static bool fisherman_boat_is_near_platform(z2_obj_boat_t *boat, f32 distance) {
    // Check distance from specific point near platform.
    z2_xyzf_t near_platform = { -455.0, 0.0, -680.0 };
    return z2_Math_Vec3f_DistXZ(&boat->common.pos_2, &near_platform) < distance;
}

/**
 * Helper function for checking if the boat is nearing the end of its path.
 **/
static bool fisherman_boat_is_near_end(z2_obj_boat_t *boat) {
    // Check that boat is moving forwards and near end of path progress.
    // Don't check for moving backwards, boat always seems to dock fine when going backwards
    // regardless of speed.
    return boat->speed_multiplier > 0 && boat->path_progress >= 0xC;
}

/**
 * Hook function used to get the top speed of the boat.
 **/
f32 fisherman_boat_get_top_speed(z2_obj_boat_t *boat, z2_game_t *game) {
    if (MISC_CONFIG.speedups.fisherman_game && boat->common.variable == 0x47F) {
        // Use higher speed unless boat is near the platform or the end of its path.
        bool near_platform = fisherman_boat_is_near_platform(boat, 750.0);
        bool near_end = fisherman_boat_is_near_end(boat);
        if (!near_platform && !near_end) {
            return 7.5;
        }
    }

    return 3.0;
}

/**
 * Hook function used to get the acceleration speed of the boat.
 **/
f32 fisherman_boat_get_accel_speed(z2_obj_boat_t *boat, z2_game_t *game) {
    if (MISC_CONFIG.speedups.fisherman_game && boat->common.variable == 0x47F) {
        // If moving forwards, use higher acceleration until near end of path.
        // If moving backwards, always use higher acceleration.
        if (boat->speed_multiplier < 0 || boat->path_progress < 0xC) {
            return 0.2;
        }
    }

    return 0.05;
}
