#ifndef RELOC_H
#define RELOC_H

#include <z64.h>

// Macros for resolving functions present in PlayerOverlay memory regions.
#define Reloc_ResolvePlayerOverlayData(Type, Name, Ovl) ((Type*)Reloc_ResolvePlayerOverlay(&Ovl, Name##_VRAM))
#define Reloc_ResolvePlayerOverlayFunc(Type, Name, Ovl) ((Type)Reloc_ResolvePlayerOverlay(&Ovl, Name##_VRAM))
#define Reloc_ResolvePlayerActorFunc(Name) Reloc_ResolvePlayerOverlayFunc(Name##_Func, Name, s801D0B70.playerActor)
#define Reloc_ResolvePlayerActorData(Type, Name) Reloc_ResolvePlayerOverlayData(Type, Name, s801D0B70.playerActor)
#define Reloc_ResolveKaleidoScopeFunc(Name) Reloc_ResolvePlayerOverlayFunc(Name##_Func, Name, s801D0B70.kaleidoScope)
#define Reloc_ResolvePlayerUpperActionFunc(Name) Reloc_ResolvePlayerOverlayFunc(PlayerUpperActionFunc, Name, s801D0B70.playerActor)
#define Reloc_ResolvePlayerActionFunc(Name) Reloc_ResolvePlayerOverlayFunc(PlayerActionFunc, Name, s801D0B70.playerActor)

// Macros for resolving types present in GameStateOverlay memory regions.
#define Reloc_ResolveGameStateRelocType(Type, Vram, Gs) ((Type*)Reloc_ResolveGameStateOverlay(&(Gs), (Vram)))
#define Reloc_ResolveFileChooseData() Reloc_ResolveGameStateRelocType(FileChooseData, FileChooseDataVRAM, gGameStateInfo.fileSelect)

// Relocatable PlayerActor functions.
#define z2_LinkDamage               Reloc_ResolvePlayerActorFunc(z2_LinkDamage)
#define z2_LinkInvincibility        Reloc_ResolvePlayerActorFunc(z2_LinkInvincibility)
#define z2_PerformEnterWaterEffects Reloc_ResolvePlayerActorFunc(z2_PerformEnterWaterEffects)
#define z2_PlayerHandleBuoyancy     Reloc_ResolvePlayerActorFunc(z2_PlayerHandleBuoyancy)
#define z2_UseItem                  Reloc_ResolvePlayerActorFunc(z2_UseItem)
#define z2_Player_ItemToActionParam Reloc_ResolvePlayerActorFunc(z2_Player_ItemToActionParam)
#define z2_Player_func_8083692C     Reloc_ResolvePlayerActorFunc(z2_Player_func_8083692C)
#define z2_Player_func_80838A90     Reloc_ResolvePlayerActorFunc(z2_Player_func_80838A90)

// Relocatable PlayerActionFunc functions.
// might need to use this for Player_StartTransformation when checking boots:
#define z2_Player_func_80849FE0     Reloc_ResolvePlayerActionFunc(z2_Player_func_80849FE0)
#define z2_Player_func_8084A884     Reloc_ResolvePlayerActionFunc(z2_Player_func_8084A884)
#define z2_Player_func_8084C16C     Reloc_ResolvePlayerActionFunc(z2_Player_func_8084C16C)

// Relocatable PlayerUpperActionFunc functions.
#define z2_Player_UpperAction_CarryAboveHead    Reloc_ResolvePlayerUpperActionFunc(z2_Player_UpperAction_CarryAboveHead)

// Relocatable PlayerActor data
#define z2_D_80862B4C               Reloc_ResolvePlayerActorData(s32, z2_D_80862B4C)

// Relocatable KaleidoScope functions.
#define z2_PauseDrawItemIcon        Reloc_ResolveKaleidoScopeFunc(z2_PauseDrawItemIcon)

void* Reloc_ResolveActorOverlay(ActorOverlay* ovl, u32 vram);
ActorInit* Reloc_ResolveActorInit(ActorOverlay* ovl);
void* Reloc_ResolveGameStateOverlay(GameStateOverlay* ovl, u32 vram);
void* Reloc_ResolvePlayerOverlay(PlayerOverlay* ovl, u32 vram);

#endif // RELOC_H
