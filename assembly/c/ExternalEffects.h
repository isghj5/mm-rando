#ifndef EXTERNAL_EFFECTS_H
#define EXTERNAL_EFFECTS_H

#include <z64.h>

// Magic number for external_effects: "EXFX"
#define EXTERNAL_EFFECTS_MAGIC 0x45584658

typedef struct {
    u32 magic;
    u32 version;

    // Effects added in version 0
    u8 cameraOverlook;
    u8 chateau;
    u8 fairy;
    u8 freeze;
    u8 icePhysics;
    u8 jinx;
    u8 noZ;
    u8 reverseControls;
} ExternalEffectsConfig;

void ExternalEffects_Handle(ActorPlayer* player, GlobalContext* ctxt);

#endif // EXTERNAL_EFFECTS_H
