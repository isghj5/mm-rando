#ifndef MMR_H
#define MMR_H

#include <stdbool.h>
#include "z2.h"

#define mmr_ChestTableFileIndex (*(u32*)(0x80144968))
#define mmr_GiTableFileIndex (*(u32*)(0x8014496C))

/**
 * MMR get-item table entry.
 **/
typedef struct mmr_gi_s {
    u8 item;
    u8 flag;
    u8 graphic;
    u8 type;
    u16 message;
    u16 object;
} mmr_gi_t;

mmr_gi_t * mmr_get_gi_entry(u16 index);
void mmr_init(void);
u16 mmr_GetNewGiIndex(z2_game_t *game, z2_actor_t *actor, u16 gi_index, bool grant);

/* Function Addresses */
#define mmr_LoadGiEntry_addr             0x801449A4
#define mmr_GetGiFlag_addr               0x80144A28
#define mmr_SetGiFlag_addr               0x801449D4

/* Function Prototypes */
typedef bool (*mmr_GetGiFlag_proc)(u32 gi_index);
typedef bool (*mmr_SetGiFlag_proc)(u32 gi_index);
typedef mmr_gi_t * (*mmr_LoadGiEntry_proc)(u32 gi_index);

/* Functions */
#define mmr_LoadGiEntry   ((mmr_LoadGiEntry_proc)   mmr_LoadGiEntry_addr)
#define mmr_GetGiFlag     ((mmr_GetGiFlag_proc)     mmr_GetGiFlag_addr)
#define mmr_SetGiFlag     ((mmr_SetGiFlag_proc)     mmr_SetGiFlag_addr)

// Magic number for misc_config: "MMRC"
#define MMR_CONFIG_MAGIC 0x4D4D5243
/* Data about the MMR Get Item Table */
struct mmr_config {
    u32 magic;                              /* 0x0000 */
    u32 version;                            /* 0x0004 */
    u16 cycle_repeatable_locations[0x80];
    u16 cycle_repeatable_locations_length;

    u16 location_bottle_red_potion;
    u16 location_bottle_gold_dust;
    u16 location_bottle_milk;
    u16 location_bottle_chateau;

    u16 location_sword_kokiri;
    u16 location_sword_razor;
    u16 location_sword_gilded;

    u16 location_magic_small;
    u16 location_magic_large;

    u16 location_wallet_adult;
    u16 location_wallet_giant;

    u16 location_bomb_bag_small;
    u16 location_bomb_bag_big;
    u16 location_bomb_bag_biggest;

    u16 location_quiver_small;
    u16 location_quiver_large;
    u16 location_quiver_largest;
};

extern struct mmr_config MMR_CONFIG;

#endif // MMR_H
