#include <stdbool.h>
#include <z64.h>
#include "Misc.h"

/**
 * Hook function used before function which may advance Toto cutscene state after formal song
 * replay, when actor cutscene index is 0xD.
 *
 * Used to reduce cutscene BGM by half for Sound Check, unless the song is complete.
 **/
void Toto_BeforeAdvanceFormalReplay(ActorEnToto* toto, GlobalContext* ctxt) {
    bool finished = (toto->songFlags & 0xF) == 0xF;
    if (MISC_CONFIG.speedups.soundCheck && !finished) {
        // Cut Sound Check song BGM in half, so it doesn't loop (default frame count is 0x300).
        // This is very hacky, should probably clean up later. Field is offset 0x10 of a struct, which
        // is size 0x160 located at 0x80205230. Likely audio channel state as there are multiple?
        u16* bgmFrames = (u16*)(0x80205230 + 0x10);
        if (*bgmFrames > 0x180) {
            *bgmFrames -= 0x180;
        }
    }
}

/**
 * Hook function used when advancing Toto cutscene state after formal song replay, when actor
 * cutscene index is 0xD.
 *
 * Used to end actor cutscene early if song is incomplete.
 **/
int Toto_HandleAdvanceFormalReplay(ActorEnToto* toto, GlobalContext* ctxt) {
    bool finished = (toto->songFlags & 0xF) == 0xF;
    if (MISC_CONFIG.speedups.soundCheck && !finished) {
        toto->funcIndex = 0;
        z2_ActorCutscene_End();
        return 0;
    } else {
        return 1;
    }
}
