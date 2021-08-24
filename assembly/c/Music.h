#ifndef MUSIC_H
#define MUSIC_H

#include <z64.h>

void Music_Update(GlobalContext* ctxt);

// Magic number for MusicConfig: "MUSI"
#define MUSIC_CONFIG_MAGIC 0x4D555349

typedef struct MusicState {
    u8 loadedSequenceId;
    u8 currentState;
    u16 forceMute;
    u8 muteMask[0x10];
} MusicState;

struct MusicConfig {
    /* 0x000 */ u32 magic;
    /* 0x004 */ u32 version;
    /* 0x008 */ u32 sequenceMaskFileIndex;
}; // size = 0xC

extern struct MusicConfig MUSIC_CONFIG;

#endif // MUSIC_H
