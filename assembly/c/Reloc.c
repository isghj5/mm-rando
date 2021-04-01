#include <z64.h>

struct ResolveInfo {
    void* ram;
    u32 virtStart;
    u32 virtEnd;
};

#define CreateInfo(Ram, Start, End) { .ram = (Ram), .virtStart = (u32)(Start), .virtEnd = (u32)(End), }

static void* Resolve(struct ResolveInfo info, u32 vram) {
    if (info.ram && info.virtStart <= vram && vram < info.virtEnd) {
        u32 offset = vram - info.virtStart;
        return (void*)((char*)info.ram + offset);
    } else {
        return NULL;
    }
}

void* Reloc_ResolveActorOverlay(ActorOverlay* ovl, u32 vram) {
    struct ResolveInfo info = CreateInfo(ovl->loadedRamAddr, ovl->vramStart, ovl->vramEnd);
    return Resolve(info, vram);
}

ActorInit* Reloc_ResolveActorInit(ActorOverlay* ovl) {
    return Reloc_ResolveActorOverlay(ovl, (u32)ovl->initInfo);
}

void* Reloc_ResolveGameStateOverlay(GameStateOverlay* ovl, u32 vram) {
    struct ResolveInfo info = CreateInfo(ovl->loadedRamAddr, ovl->vramStart, ovl->vramEnd);
    return Resolve(info, vram);
}

void* Reloc_ResolvePlayerOverlay(PlayerOverlay* ovl, u32 vram) {
    struct ResolveInfo info = CreateInfo(ovl->loadedRamAddr, ovl->vramStart, ovl->vramEnd);
    return Resolve(info, vram);
}
