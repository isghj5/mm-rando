#include "items.h"
#include "misc.h"
#include "mmr.h"
#include "z2.h"

/**
 * Hook function used to write the chest get-item index on init.
 **/
void chest_write_gi_index(z2_en_box_t *actor, z2_game_t *game) {
    if (!MISC_CONFIG.vanilla_layout) {
        u16 result;

        // Read from chest-table file to determine gi-table index, and write to actor field.
        u16 index = (actor->common.variable >> 5) & 0x7F;
        u32 prom = z2_file_table[mmr_ChestTableFileIndex].prom_start + (index * 2);
        z2_RomToRam(prom, &result, sizeof(result));
        actor->gi_index = result;

        // If ice trap chest, use special value instead of index.
        mmr_gi_t *entry = mmr_LoadGiEntry(actor->gi_index);
        if (entry->item == Z2_ICE_TRAP) {
            actor->gi_index = 0x76;
        }
    } else {
        // Vanilla Layout behavior
        actor->gi_index = (actor->common.variable >> 5) & 0x7F;
    }
}

/**
 * Hook function used to update the chest get-item index before opening.
 **/
void chest_update_gi_index(z2_en_box_t *actor, z2_game_t *game, bool grant) {
    if (!MISC_CONFIG.vanilla_layout) {
        // Resolve new gi-table index if not ice trap.
        if (actor->gi_index != 0x76) {
            actor->gi_index = mmr_GetNewGiIndex(actor->gi_index, grant);
        }
    }
}
