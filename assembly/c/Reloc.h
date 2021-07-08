#ifndef RELOC_H
#define RELOC_H

#include <z64.h>

// Macros for resolving functions present in PlayerOverlay memory regions.
#define Reloc_ResolvePlayerOverlayFunc(Name, Ovl) ((Name##_Func)Reloc_ResolvePlayerOverlay(&Ovl, Name##_VRAM))
#define Reloc_ResolvePlayerActorFunc(Name) Reloc_ResolvePlayerOverlayFunc(Name, s801D0B70.playerActor)
#define Reloc_ResolveKaleidoScopeFunc(Name) Reloc_ResolvePlayerOverlayFunc(Name, s801D0B70.kaleidoScope)

// Macros for resolving types present in GameStateOverlay memory regions.
#define Reloc_ResolveGameStateRelocType(Type, Vram, Gs) ((Type*)Reloc_ResolveGameStateOverlay(&(Gs), (Vram)))
#define Reloc_ResolveFileChooseData() Reloc_ResolveGameStateRelocType(FileChooseData, FileChooseDataVRAM, gGameStateInfo.fileSelect)

// Relocatable PlayerActor functions.
#define z2_LinkDamage               Reloc_ResolvePlayerActorFunc(z2_LinkDamage)
#define z2_LinkInvincibility        Reloc_ResolvePlayerActorFunc(z2_LinkInvincibility)
#define z2_PerformEnterWaterEffects Reloc_ResolvePlayerActorFunc(z2_PerformEnterWaterEffects)
#define z2_PlayerHandleBuoyancy     Reloc_ResolvePlayerActorFunc(z2_PlayerHandleBuoyancy)
#define z2_UseItem                  Reloc_ResolvePlayerActorFunc(z2_UseItem)

// Relocatable KaleidoScope functions.
#define z2_PauseDrawItemIcon        Reloc_ResolveKaleidoScopeFunc(z2_PauseDrawItemIcon)

void* Reloc_ResolveActorOverlay(ActorOverlay* ovl, u32 vram);
ActorInit* Reloc_ResolveActorInit(ActorOverlay* ovl);
void* Reloc_ResolveGameStateOverlay(GameStateOverlay* ovl, u32 vram);
void* Reloc_ResolvePlayerOverlay(PlayerOverlay* ovl, u32 vram);

#endif // RELOC_H
