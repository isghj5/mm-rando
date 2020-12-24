#ifndef RELOC_H
#define RELOC_H

#include "z2.h"

#define GET_RELOC_FUNC(NAME, OVL) ((NAME##_proc)reloc_resolve_player_ovl(&OVL, NAME##_vram))
#define GET_RELOC_PAUSE_FUNC(NAME)  GET_RELOC_FUNC(NAME, z2_0x801D0B70.pause_ovl)
#define GET_RELOC_PLAYER_FUNC(NAME) GET_RELOC_FUNC(NAME, z2_0x801D0B70.player_ovl)

/* Macros for resolving RAM addresses from gamestate entries */
#define GET_GS_RELOC_TYPE(NAME, GS) (*(NAME##_t *)reloc_resolve_gamestate(&(GS), (NAME##_vram)))

/* Relocatable player functions */
#define z2_LinkDamage        GET_RELOC_PLAYER_FUNC(z2_LinkDamage)
#define z2_LinkInvincibility GET_RELOC_PLAYER_FUNC(z2_LinkInvincibility)
#define z2_UseItem           GET_RELOC_PLAYER_FUNC(z2_UseItem)

#define z2_PerformEnterWaterEffects GET_RELOC_PLAYER_FUNC(z2_PerformEnterWaterEffects)
#define z2_PlayerHandleBuoyancy     GET_RELOC_PLAYER_FUNC(z2_PlayerHandleBuoyancy)

/* Relocatable pause menu functions */
#define z2_PauseDrawItemIcon GET_RELOC_PAUSE_FUNC(z2_PauseDrawItemIcon)

/* Relocatable file select data */
#define z2_file_select_ctxt  GET_GS_RELOC_TYPE(z2_file_select_ctxt, z2_gamestate.file_select)

void * reloc_resolve_actor_ovl(z2_actor_ovl_table_t *ovl, u32 vram);
z2_actor_init_t * reloc_resolve_actor_init(z2_actor_ovl_table_t *ovl);
void * reloc_resolve_gamestate(z2_gamestate_table_t *gs, u32 vram);
void * reloc_resolve_player_ovl(z2_player_ovl_table_t *ovl, u32 vram);

#endif // RELOC_H
