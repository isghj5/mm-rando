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
    /* 0x04 */ u8 hasSequenceMaskFile;
    /* 0x05 */ u8 fileSelectMusicFormIndex;
    /* 0x06 */ u8 fileSelectMusicMiscIndex;
    /* 0x07 */ u8 pad27;
    /* 0x08 */ u8 playMask[0x10];
    /* 0x18 */ union {
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
    /* 0x19 */ u8 pad15[0xF]; // everdrive rounds down to 0x10s when loading from rom
} MusicState; // size = 0x28

typedef struct {
    u32 removeMinorMusic    : 1;
    u32                     : 31;
} MusicFlags;

struct MusicConfig {
    /* 0x000 */ u32 magic;
    /* 0x004 */ u32 version;
    /* 0x008 */ u32 sequenceMaskFileIndex;
    /* 0x00C */ MusicFlags flags;
}; // size = 0x10

extern struct MusicConfig MUSIC_CONFIG;

#endif // MUSIC_H
