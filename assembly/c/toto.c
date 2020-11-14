#include <stdbool.h>
#include "misc.h"
#include "z2.h"

/**
 * Hook function used before function which may advance Toto cutscene state after formal song
 * replay, when actor cutscene index is 0xD.
 *
 * Used to reduce cutscene BGM by half for Sound Check, unless the song is complete.
 **/
void toto_before_advance_formal_replay(z2_en_toto_t *toto, z2_game_t *game) {
    bool finished = (toto->song_flags & 0xF) == 0xF;
    if (MISC_CONFIG.speedups.sound_check && !finished) {
        // Cut Sound Check song BGM in half, so it doesn't loop (default frame count is 0x300).
        // This is very hacky, should probably clean up later. Field is offset 0x10 of a struct, which
        // is size 0x160 located at 0x80205230. Likely audio channel state as there are multiple?
        u16 *bgm_frames = (u16*)(0x80205230 + 0x10);
        if (*bgm_frames > 0x180) {
            *bgm_frames -= 0x180;
        }
    }
}

/**
 * Hook function used when advancing Toto cutscene state after formal song replay, when actor
 * cutscene index is 0xD.
 *
 * Used to end actor cutscene early if song is incomplete.
 **/
int toto_handle_advance_formal_replay(z2_en_toto_t *toto, z2_game_t *game) {
    bool finished = (toto->song_flags & 0xF) == 0xF;
    if (MISC_CONFIG.speedups.sound_check && !finished) {
        toto->func_index = 0;
        z2_ActorCutscene_End();
        return 0;
    } else {
        return 1;
    }
}
