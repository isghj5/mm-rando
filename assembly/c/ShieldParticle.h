#ifndef SHIELDPARTICLE_H
#define SHIELDPARTICLE_H

#include <z64.h>
#include <z64light.h>

typedef struct EffectShieldParticleElement {
    /* 0x00 */ f32 initialSpeed;
    /* 0x04 */ f32 endXChange;
    /* 0x08 */ f32 endX;
    /* 0x0C */ f32 startXChange;
    /* 0x10 */ f32 startX;
    /* 0x14 */ s16 yaw;
    /* 0x16 */ s16 pitch;
} EffectShieldParticleElement; // size = 0x18


typedef struct EffectShieldParticle {
    /* 0x000 */ EffectShieldParticleElement elements[16];
    /* 0x180 */ u8 numElements;
    /* 0x182 */ Vec3s position;
    ///* 0x188 */ Color_RGBA8 primColorStart;
    ///* 0x18C */ Color_RGBA8 envColorStart;
    ///* 0x190 */ Color_RGBA8 primColorMid;
    ///* 0x194 */ Color_RGBA8 envColorMid;
    ///* 0x198 */ Color_RGBA8 primColorEnd;
    ///* 0x19C */ Color_RGBA8 envColorEnd;
    /* 0x188 */ ColorRGBA8 primColorStart;
    /* 0x18C */ ColorRGBA8 envColorStart;
    /* 0x190 */ ColorRGBA8 primColorMid;
    /* 0x194 */ ColorRGBA8 envColorMid;
    /* 0x198 */ ColorRGBA8 primColorEnd;
    /* 0x19C */ ColorRGBA8 envColorEnd;
    /* 0x1A0 */ f32 deceleration;
    /* 0x1A4 */ char unk_1A4[0x04];
    /* 0x1A8 */ f32 maxInitialSpeed;
    /* 0x1AC */ f32 lengthCutoff;
    /* 0x1B0 */ u8 duration;
    /* 0x1B1 */ u8 timer;
    /* 0x1B2 */ LightInfo lightInfo;
    ///* 0x1C0 */ LightNode* lightNode;
    /* 0x1C0 */ z_Light* lightNode;
    /* 0x1C4 */ s32 lightDecay; // halves light radius every frame when set to 1
} EffectShieldParticle; // size = 0x1C8

void ShieldParticle_Destroy2(void* thisx);

#endif // SHIELDPARTICLE_H

