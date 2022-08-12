#ifndef Z64_AUDIO_H
#define Z64_AUDIO_H

#include <PR/ultratypes.h>
#include <z64math.h>

typedef struct {
    /* 0x00 */ u16 sfxId;
    /* 0x02 */ u8 token;
    /* 0x04 */ s8* reverbAdd;
    /* 0x08 */ Vec3f* pos;
    /* 0x0C */ f32* freqScale;
    /* 0x10 */ f32* vol;
} SoundRequest; // size = 0x14

#endif
