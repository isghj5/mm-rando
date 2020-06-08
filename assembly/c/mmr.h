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

mmr_gi_t * mmr_get_gi_entry(u32 index);
void mmr_init(void);

/* Function Addresses */
#define mmr_CheckBottleAndGetGiFlag_addr 0x800B343C
#define mmr_LoadGiEntry_addr             0x801449A4
#define mmr_GetNewGiIndex_addr           0x801DC790

/* Function Prototypes */
typedef u32 (*mmr_GetNewGiIndex_proc)(u32 gi_index, bool grant);
typedef mmr_gi_t * (*mmr_LoadGiEntry_proc)(u32 gi_index);
// If index is for a bottle, checks a different flag.
typedef bool (*mmr_CheckBottleAndGetGiFlag_proc)(u32 gi_index);

/* Functions */
#define mmr_GetNewGiIndex ((mmr_GetNewGiIndex_proc) mmr_GetNewGiIndex_addr)
#define mmr_LoadGiEntry   ((mmr_LoadGiEntry_proc)   mmr_LoadGiEntry_addr)

/* Function Stubs (implemented in asm) */
extern u32 mmr_GetNewGiIndex_stub(z2_game_t *game, u32 gi_index, bool grant);

#endif // MMR_H
