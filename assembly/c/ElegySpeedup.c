#include <stdbool.h>
#include <z64.h>
#include "Misc.h"

union ElegyLockParams {
    struct {
        u16 frameCount;
        u16 spawnFrame;
    };
    u32 value;
};

/**
 * Hook function called to get the movement speed of the Stone Tower blocks.
 **/
f32 ElegySpeedup_GetBlockSpeed(Actor* actor, GlobalContext* ctxt, int type) {
    if (type == 0) {
        // Get speed when moving into new spot.
        if (!MISC_CONFIG.flags.elegySpeedup) {
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
u32 ElegySpeedup_GetLockParams(ActorPlayer* player, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.elegySpeedup) {
        union ElegyLockParams result = {
            .frameCount = 0x5B,
            .spawnFrame = 0x0A,
        };
        return result.value;
    } else {
        union ElegyLockParams result = {
            .frameCount = 0x01,
            .spawnFrame = 0x01,
        };
        return result.value;
    }
}

/**
 * Hook function called to get the number of frames before beginning to fade out the current Elegy
 * statue when moving it to a different position.
 **/
u16 ElegySpeedup_GetStatueDespawnCounter(GlobalContext* ctxt, ActorPlayer* player) {
    if (!MISC_CONFIG.flags.elegySpeedup) {
        return 0x14;
    } else {
        return 0x01;
    }
}

/**
 * Hook function called to get the speed at which the Elegy statue fades in or out.
 **/
u16 ElegySpeedup_GetStatueFadeSpeed(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.elegySpeedup) {
        return 0x8;
    } else {
        return 0x20;
    }
}

/**
 * Hook function called to check whether or not the camera should update for the Elegy cutscene.
 **/
bool ElegySpeedup_ShouldUpdateCamera(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.elegySpeedup) {
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function called to check whether or not the scene should darken for the Elegy cutscene.
 **/
bool ElegySpeedup_ShouldDarken(Actor* actor, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.elegySpeedup) {
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function called to sync the Elegy effect's position with that of its respective statue.
 **/
void ElegySpeedup_UpdateEffectPosition(Actor* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.elegySpeedup) {
        // Get Elegy statue actor from main actor context struct.
        u8 formIndex = actor->params & 7;
        Actor* statue = ctxt->actorCtx.elegyStatues[formIndex];
        if (statue != NULL) {
            // Sync position of Eff_Change effect with the statue.
            actor->currPosRot.pos = statue->currPosRot.pos;
        }
    }
}
