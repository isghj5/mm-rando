#include <stdbool.h>
#include <z64.h>
#include "Input.h"
#include "Misc.h"
#include "macro.h"

// This also prevents Weird Shot crash
s32 BugFixes_PreventHessCrash(Vec3s* jointTable) {
    // Displaced code
    s32 index = (jointTable[0x16].x & 0xF00) >> 8;

    // `index - 1` is an index into an array with only two elements, so it should only return 0, 1, or 2. values 3 through F crash the game
    if (!MISC_CONFIG.flags.saferGlitches || index < 3) { // TODO change to specifically fixHessCrash?
        return index;
    }
    return 0;
}

// Fail-safe to prevent other audio crashes
s32 BugFixes_PreventAudioCrash(SoundRequest* soundRequest) {
    u16 sfxId = soundRequest->sfxId;
    if (!MISC_CONFIG.flags.saferGlitches) {
        return sfxId;
    }

    s32 sfxBankId = ((sfxId & 0xF000) >> 12) & 0xFF;

    // SFX Banks only number 0 through 6, but code allows up to F
    if (sfxBankId < 7) {
        return sfxId;
    }

    return 0;
}

bool BugFixes_PreventHookslideCrash(Camera* camera) {
    if (MISC_CONFIG.flags.saferGlitches) {
        ActorPlayer* player = camera->player;
        if (player == GET_PLAYER(camera->ctxt) && player->base.floorPoly == NULL) {
            return false;
        }
    }
    return z2_Camera_IsHookArrival(camera);
}

// Prevents Action Swap Crash
// Prevents 1st person bow as goron crash
void BugFixes_PlayEmptyWeaponSound(ActorPlayer* player, s16* soundEffects) {
    s16 sfxIndex = player->unkB28;
    if (MISC_CONFIG.flags.saferGlitches && sfxIndex != 1 && sfxIndex != 2) {
        return;
    }
    z2_PlayPlayerSfx(player, soundEffects[sfxIndex]);
}

void BugFixes_SoaringCursorPoint(PauseContext* pauseCtx) {
    // Displaced code:
    if (gSaveContext.perm.unk24.owlsHit.clockTown) {
        pauseCtx->cells1.worldmap = 4;
    }
    // End displaced code

    if (MISC_CONFIG.flags.saferGlitches && pauseCtx->cells1.worldmap == -1) {
        pauseCtx->cells1.worldmap = 0;
    }
}
