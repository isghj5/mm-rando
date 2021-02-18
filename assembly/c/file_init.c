#include "z2.h"
#include "mmr.h"

void file_after_init(z2_game_t *game) {
    for (u8 i = 0; i < 6; i++) {
        if (((MMR_CONFIG.extra_starting_maps.byte >> i) & 1) != 0) {
            z2_GiveMap(i);
        }
    }

    for (u8 i = 0; i < MMR_CONFIG.extra_starting_item_ids_length; i++) {
        z2_GiveItem(game, MMR_CONFIG.extra_starting_item_ids[i]);
    }
}
