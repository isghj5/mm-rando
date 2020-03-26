#include "z2.h"

void * get_ram_from_gamestate_vram(z2_gamestate_table_t *gs, u32 vram) {
    if (gs->ram && gs->vram_start <= vram && vram < gs->vram_end) {
        u32 offset = vram - gs->vram_start;
        return (void *)((char *)gs->ram + offset);
    } else {
        return NULL;
    }
}

void * get_ram_from_player_ovl_vram(z2_player_ovl_table_t *ovl, u32 vram) {
    if (ovl->ram && ovl->vram_start <= vram && vram < ovl->vram_end) {
        u32 offset = vram - ovl->vram_start;
        return (void *)((char *)ovl->ram + offset);
    } else {
        return NULL;
    }
}
