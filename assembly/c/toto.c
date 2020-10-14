#include <stdbool.h>
#include "misc.h"
#include "z2.h"

/**
 * Helper function for advancing to the next actor cutscene. Is basically a copy of an existing
 * function within En_Toto.
 **/
static int toto_advance_actor_cutscene(z2_en_toto_t *toto, z2_game_t *game) {
    if (z2_ActorCutscene_GetCanPlayNext(toto->actor_cutscene)) {
        z2_actor_t *link = &(Z2_LINK(game)->common);
        z2_ActorCutscene_StartAndSetUnkLinkFields(toto->actor_cutscene, link);
        return 1;
    } else {
        z2_ActorCutscene_SetIntentToPlay(toto->actor_cutscene);
        return 0;
    }
}

/**
 * Hook function used to check whether or not to skip setting up for the "formal replay" part of
 * the actor cutscenes.
 **/
bool toto_should_skip_formal_replay(z2_en_toto_t *toto, z2_game_t *game) {
    //bool finished = (toto->song_flags & 0xF) == 0xF;
    //return MISC_CONFIG.speedups.sound_check && !finished;
    return false;
}

/**
 * Hook function used for advancing actor cutscene while index is 0xC (preparing for "formal replay").
 **/
int toto_prepare_formal_replay(z2_en_toto_t *toto, z2_game_t *game) {
    if (toto_should_skip_formal_replay(toto, game)) {
        // End actor cutscenes prematurely if song not complete.
        toto->func_index = 0;
        toto->frame_count = 0;
        z2_ActorCutscene_End();
        if (toto->stagelights) {
            z2_ActorUnload(toto->stagelights);
        }
        return 0;
    } else {
        // Default behavior.
        return toto_advance_actor_cutscene(toto, game);
    }
}

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
