#ifndef RELOC_H
#define RELOC_H

#include "z2.h"

#define GET_RELOC_FUNC(NAME, OVL) ((NAME##_proc)get_ram_from_player_ovl_vram(&OVL, NAME##_vram))
#define GET_RELOC_PAUSE_FUNC(NAME)  GET_RELOC_FUNC(NAME, z2_0x801D0B70.pause_ovl)
#define GET_RELOC_PLAYER_FUNC(NAME) GET_RELOC_FUNC(NAME, z2_0x801D0B70.player_ovl)

/* Macros for resolving RAM addresses from gamestate entries */
#define GET_GS_RELOC_TYPE(NAME, GS) (*(NAME##_t *)get_ram_from_gamestate_vram(&(GS), (NAME##_vram)))

/* Relocatable player functions */
#define z2_LinkDamage        GET_RELOC_PLAYER_FUNC(z2_LinkDamage)
#define z2_LinkInvincibility GET_RELOC_PLAYER_FUNC(z2_LinkInvincibility)
#define z2_UseItem           GET_RELOC_PLAYER_FUNC(z2_UseItem)

/* Relocatable pause menu functions */
#define z2_PauseDrawItemIcon GET_RELOC_PAUSE_FUNC(z2_PauseDrawItemIcon)

/* Relocatable file select data */
#define z2_file_select_ctxt  GET_GS_RELOC_TYPE(z2_file_select_ctxt, z2_gamestate.file_select)

void * get_ram_from_gamestate_vram(z2_gamestate_table_t *gs, u32 vram);
void * get_ram_from_player_ovl_vram(z2_player_ovl_table_t *ovl, u32 vram);

#endif // RELOC_H
