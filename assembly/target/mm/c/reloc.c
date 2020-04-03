#include "z2.h"

struct vram_info {
    void *ram;
    u32 virt_start;
    u32 virt_end;
};

static void * resolve(struct vram_info info, u32 vram) {
    if (info.ram && info.virt_start <= vram && vram < info.virt_end) {
        u32 offset = vram - info.virt_start;
        return (void *)((char *)info.ram + offset);
    } else {
        return NULL;
    }
}

void * reloc_resolve_actor_ovl(z2_actor_ovl_table_t *ovl, u32 vram) {
    struct vram_info info = {
        .ram = ovl->ram,
        .virt_start = ovl->vram_start,
        .virt_end = ovl->vram_end,
    };

    return resolve(info, vram);
}

z2_actor_init_t * reloc_resolve_actor_init(z2_actor_ovl_table_t *ovl) {
    return reloc_resolve_actor_ovl(ovl, ovl->initialization);
}

void * reloc_resolve_gamestate(z2_gamestate_table_t *gs, u32 vram) {
    struct vram_info info = {
        .ram = gs->ram,
        .virt_start = gs->vram_start,
        .virt_end = gs->vram_end,
    };

    return resolve(info, vram);
}

void * reloc_resolve_player_ovl(z2_player_ovl_table_t *ovl, u32 vram) {
    struct vram_info info = {
        .ram = ovl->ram,
        .virt_start = ovl->vram_start,
        .virt_end = ovl->vram_end,
    };

    return resolve(info, vram);
}
