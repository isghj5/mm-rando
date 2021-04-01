#include <stdbool.h>
#include <z64.h>
#include "Misc.h"

/**
 * Hook function used to check if the Fisherman's Game should end.
 *
 * If speedup is enabled, checks if player has enough points to end early.
 **/
bool Fisherman_ShouldEndGame(Actor* actor, GlobalContext* ctxt, u32 timerHi, u32 timerLo) {
    bool timeout = (timerHi == 0 && timerLo == 0);
    return ((MISC_CONFIG.speedups.fishermanGame && gSaveContext.extra.minigameCounter[0] >= 20) || timeout);
}

/**
 * Hook function used to check if the timer satisfies giving the player a reward.
 *
 * If speedup is enabled the minigame ends early, so ignores the timer check.
 **/
bool Fisherman_ShouldPassTimerCheck(Actor* actor, GlobalContext* ctxt, u32 timerHi, u32 timerLo) {
    if (MISC_CONFIG.speedups.fishermanGame) {
        return true;
    } else {
        return timerHi == 0 && timerLo == 0;
    }
}

/**
 * Helper function for checking if the boat is within a specific radius of a point near the
 * platform.
 **/
static bool BoatIsNearPlatform(ActorObjBoat* boat, f32 distance) {
    // Check distance from specific point near platform.
    Vec3f nearPlatform = { -455.0, 0.0, -680.0 };
    return z2_Math_Vec3f_DistXZ(&boat->base.currPosRot.pos, &nearPlatform) < distance;
}

/**
 * Helper function for checking if the boat is nearing the end of its path.
 **/
static bool BoatIsNearEnd(ActorObjBoat* boat) {
    // Check that boat is moving forwards and near end of path progress.
    // Don't check for moving backwards, boat always seems to dock fine when going backwards
    // regardless of speed.
    return boat->speedMultiplier > 0 && boat->pathProgress >= 0xC;
}

/**
 * Hook function used to get the top speed of the boat.
 **/
f32 Fisherman_BoatGetTopSpeed(ActorObjBoat* boat, GlobalContext* ctxt) {
    if (MISC_CONFIG.speedups.fishermanGame && boat->base.params == 0x47F) {
        // Use higher speed unless boat is near the platform or the end of its path.
        bool nearPlatform = BoatIsNearPlatform(boat, 750.0);
        bool nearEnd = BoatIsNearEnd(boat);
        if (!nearPlatform && !nearEnd) {
            return 7.5;
        }
    }
    return 3.0;
}

/**
 * Hook function used to get the acceleration speed of the boat.
 **/
f32 Fisherman_BoatGetAccelSpeed(ActorObjBoat* boat, GlobalContext* ctxt) {
    if (MISC_CONFIG.speedups.fishermanGame && boat->base.params == 0x47F) {
        // If moving forwards, use higher acceleration until near end of path.
        // If moving backwards, always use higher acceleration.
        if (boat->speedMultiplier < 0 || boat->pathProgress < 0xC) {
            return 0.2;
        }
    }
    return 0.05;
}
