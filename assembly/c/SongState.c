#include <z64.h>
#include "Misc.h"

union SongStateResults {
    struct {
        u8 action;
        u8 unk1F0A;
        u8 frameCount;
        u8 unused;
    };
    u32 value;
};

/**
 * Hook function to handle advancing the song state machine to song playback.
 **/
u32 SongState_HandlePlayback(GlobalContext* ctxt, MessageContext* msgCtxt) {
    s8 song = msgCtxt->songInfo->frameInfo[0].storedSong;
    if (song == 3 && MISC_CONFIG.flags.elegySpeedup) {
        // Process state for Elegy of Emptiness
        // Disable sfx being "dampened" (normally action 0x17 would do this before advancing to 0x18)
        z2_ToggleSfxDampen(0);
        // Code for action 0x18 will stop early unless this value is 0x32.
        // msgCtxt->unk202C = 0x32;
        // Skip past song playback & message box, to state 0x18
        union SongStateResults results = {
            .action = 0x18,
            .unk1F0A = 3,
            .frameCount = 2,
        };
        return results.value;
    } else {
        // Vanilla behavior, prepare for song playback.
        union SongStateResults results = {
            .action = 0x12,
            .unk1F0A = 3,
            .frameCount = 10,
        };
        return results.value;
    }
}

/**
 * Hook function used to handle advancing the song state to song playback (for preset songs).
 **/
u32 SongState_HandlePresetPlayback(GlobalContext* ctxt, MessageContext* msgCtxt) {
    s8 song = msgCtxt->playbackSong;
    // Song Ids for Sound Check: Link = 0xF, Goron = 0x10, Zora = 0x11, Deku = 0x12
    if (MISC_CONFIG.speedups.soundCheck && (0xF <= song && song <= 0x12)) {
        // Restore sfx volume.
        z2_ToggleSfxDampen(0);
        // Restore player control.
        msgCtxt->unk1202A = 4;
        // Skip past song playback.
        union SongStateResults results = {
            .action = 0x43,
            .unk1F0A = 3,
            .frameCount = 1,
        };
        return results.value;
    } else {
        // Vanilla behavior, prepare for song playback.
        union SongStateResults results = {
            .action = 0x12,
            .unk1F0A = 3,
            .frameCount = 1,
        };
        return results.value;
    }
}
