#ifndef MUSIC_H
#define MUSIC_H

#include <z64.h>

void Music_Draw(GlobalContext* ctxt);
void Music_Update(GlobalContext* ctxt);

// Magic number for MusicConfig: "MUSI"
#define MUSIC_CONFIG_MAGIC 0x4D555349

#define SEQUENCE_DATA_SIZE 0x30
#define SEQUENCE_NAME_MAX_SIZE 0x20

enum SequencePlayState {
    SEQUENCE_PLAY_STATE_NONE            = 0b0000000000000000,
    SEQUENCE_PLAY_STATE_FIERCE_DEITY    = 0b0000000000000001,
    SEQUENCE_PLAY_STATE_GORON           = 0b0000000000000010,
    SEQUENCE_PLAY_STATE_ZORA            = 0b0000000000000100,
    SEQUENCE_PLAY_STATE_DEKU            = 0b0000000000001000,
    SEQUENCE_PLAY_STATE_HUMAN           = 0b0000000000010000,
    SEQUENCE_PLAY_STATE_OUTDOORS        = 0b0000000000100000,
    SEQUENCE_PLAY_STATE_INDOORS         = 0b0000000001000000,
    SEQUENCE_PLAY_STATE_CAVE            = 0b0000000010000000,
    SEQUENCE_PLAY_STATE_EPONA           = 0b0000000100000000,
    SEQUENCE_PLAY_STATE_SWIM            = 0b0000001000000000,
    SEQUENCE_PLAY_STATE_SPIKE_ROLLING   = 0b0000010000000000,
    SEQUENCE_PLAY_STATE_COMBAT          = 0b0000100000000000,
    SEQUENCE_PLAY_STATE_CRITICAL_HEALTH = 0b0001000000000000
};

typedef struct MusicState {
    /* 0x00 */ u8 loadedSequenceId;
    /* 0x01 */ u8 hasSequenceMaskFile;
    /* 0x02 */ u8 fileSelectMusicFormIndex;
    /* 0x03 */ u8 fileSelectMusicMiscIndex;
    /* 0x04 */ u16 currentState;
    /* 0x06 */ u16 forceMute;
    /* 0x08 */ u16 playMask[0x10];
    /* 0x28 */ union {
        struct {
            u16                : 3;
            u16 criticalHealth : 1;
            u16 combat         : 1;
            u16 spikeRolling   : 1;
            u16 swimming       : 1;
            u16 epona          : 1;
            u16 cave           : 1;
            u16 indoors        : 1;
            u16 outdoors       : 1;
            u16 human          : 1;
            u16 deku           : 1;
            u16 zora           : 1;
            u16 goron          : 1;
            u16 fierceDeity    : 1;
        };
        u16 value;
    } cumulativeStates;
    /* 0x2A */ u8 pad2A[0xE]; // everdrive rounds down to 0x10s when loading from rom
} MusicState; // size = 0x38

typedef struct {
    u32 removeMinorMusic    : 1;
    u32 showTrackName       : 1;
    u32 disableFanfares     : 1;
    u32                     : 29;
} MusicFlags;

struct MusicConfig {
    /* 0x000 */ u32 magic;
    /* 0x004 */ u32 version;
    /* 0x008 */ u32 sequenceMaskFileIndex;
    /* 0x00C */ u32 sequenceNamesFileIndex;
    /* 0x010 */ MusicFlags flags;
}; // size = 0x14

extern struct MusicConfig MUSIC_CONFIG;

#endif // MUSIC_H
