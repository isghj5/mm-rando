#ifndef EXTERNAL_EFFECTS_H
#define EXTERNAL_EFFECTS_H

#include "z2.h"

// Magic number for external_effects: "EXFX"
#define EXTERNAL_EFFECTS_MAGIC 0x45584658

typedef struct {
    u32 magic;
    u32 version;

    // Effects added in version 0
    u8 camera_overlook;
    u8 chateau;
    u8 fairy;
    u8 freeze;
    u8 ice_physics;
    u8 jinx;
    u8 no_z;
    u8 reverse_controls;
} external_effects_t;

void external_effects_handle(z2_link_t *link, z2_game_t *game);

#endif // EXTERNAL_EFFECTS_H
