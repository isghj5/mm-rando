#include "misc.h"
#include "z2.h"

union song_state_results {
    struct {
        u8 action;
        u8 unk_0x1F0A;
        u8 frame_count;
        u8 unused;
    };
    u32 value;
};

/**
 * Hook function to handle advancing the song state machine to song playback.
 **/
u32 song_state_handle_playback(z2_game_t *game, z2_msgbox_ctxt_t *ctxt) {
    s8 song = ctxt->song_ctxt->frames[0].stored_song;
    if (song == 3 && MISC_CONFIG.elegy_speedup) {
        // Process state for Elegy of Emptiness
        // Disable sfx being "dampened" (normally action 0x17 would do this before advancing to 0x18)
        z2_ToggleSfxDampen(0);
        // Code for action 0x18 will stop early unless this value is 0x32.
        // ctxt->unk_0x202C = 0x32;
        // Skip past song playback & message box, to state 0x18
        union song_state_results results = {
            .action = 0x18,
            .unk_0x1F0A = 3,
            .frame_count = 2,
        };
        return results.value;
    } else {
        // Vanilla behavior, prepare for song playback.
        union song_state_results results = {
            .action = 0x12,
            .unk_0x1F0A = 3,
            .frame_count = 10,
        };
        return results.value;
    }
}

/**
 * Hook function used to handle advancing the song state to song playback (for preset songs).
 **/
u32 song_state_handle_preset_playback(z2_game_t *game, z2_msgbox_ctxt_t *ctxt) {
    s8 song = ctxt->playback_song;
    // Song Ids for Sound Check: Link = 0xF, Goron = 0x10, Zora = 0x11, Deku = 0x12
    if (MISC_CONFIG.speedups.sound_check && (0xF <= song && song <= 0x12)) {
        // Restore sfx volume.
        z2_ToggleSfxDampen(0);
        // Restore player control.
        ctxt->unk_0x202A = 4;
        // Skip past song playback.
        union song_state_results results = {
            .action = 0x43,
            .unk_0x1F0A = 3,
            .frame_count = 1,
        };
        return results.value;
    } else {
        // Vanilla behavior, prepare for song playback.
        union song_state_results results = {
            .action = 0x12,
            .unk_0x1F0A = 3,
            .frame_count = 1,
        };
        return results.value;
    }
}
