#ifndef MUSIC_H
#define MUSIC_H

#include <z64.h>

void Music_Update(GlobalContext* ctxt);

// Magic number for MusicConfig: "MUSI"
#define MUSIC_CONFIG_MAGIC 0x4D555349

typedef struct MusicState {
    /* 0x00 */ u8 loadedSequenceId;
    /* 0x01 */ u8 currentState;
    /* 0x02 */ u16 forceMute;
    /* 0x04 */ u8 playMask[0x10];
    /* 0x14 */ union {
        struct {
            u8 combat         : 1;
            u8 spikeRolling   : 1;
            u8 swimming       : 1;
            u8 epona          : 1;
            u8 deku           : 1;
            u8 zora           : 1;
            u8 goron          : 1;
            u8 human          : 1;
        };
        u8 value;
    } cumulativeStates;
    /* 0x15 */ u8 pad[3];
} MusicState; // size = 0x18

struct MusicConfig {
    /* 0x000 */ u32 magic;
    /* 0x004 */ u32 version;
    /* 0x008 */ u32 sequenceMaskFileIndex;
}; // size = 0xC

extern struct MusicConfig MUSIC_CONFIG;

#endif // MUSIC_H
