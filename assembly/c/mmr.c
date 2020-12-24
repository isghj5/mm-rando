#include "misc.h"
#include "mmr.h"
#include "util.h"

struct mmr_config MMR_CONFIG = {
    .magic = MMR_CONFIG_MAGIC,
    .version = 1,
    .cycle_repeatable_locations_length = 0x80,

    .location_bottle_red_potion = 0x59,
    .location_bottle_chateau = 0x6F,
    .location_bottle_milk = 0x60,
    .location_bottle_gold_dust = 0x6A,

    .location_sword_kokiri = 0x37,
    .location_sword_razor = 0x38,
    .location_sword_gilded = 0x39,

    .location_magic_small = 0x12C,
    .location_magic_large = 0x12E,

    .location_wallet_adult = 0x08,
    .location_wallet_giant = 0x09,

    .location_bomb_bag_small = 0x1B,
    .location_bomb_bag_big = 0x1C,
    .location_bomb_bag_biggest = 0x1D,

    .location_quiver_small = 0x22,
    .location_quiver_large = 0x23,
    .location_quiver_largest = 0x24,
};

static mmr_gi_t *g_gi_table = NULL;

static void mmr_load_gi_table(void) {
    // Use gi-table file index to get dmadata entry.
    u32 index = mmr_GiTableFileIndex;
    z2_file_table_t entry = z2_file_table[index];
    u32 size = entry.vrom_end - entry.vrom_start;

    // Load the gi-table table from file into buffer.
    g_gi_table = (mmr_gi_t *)heap_alloc(size);
    z2_ReadFile(g_gi_table, entry.vrom_start, size);
}

mmr_gi_t * mmr_get_gi_entry(u16 index) {
    return &g_gi_table[index - 1];
}

bool mmr_CheckBottleAndGetGiFlag(u16 gi_index, u16 *new_gi_index) {
    if (gi_index == MMR_CONFIG.location_bottle_red_potion) {
        gi_index = 0x5B;
    } else if (gi_index == MMR_CONFIG.location_bottle_chateau) {
        gi_index = 0x91;
    } else if (gi_index == MMR_CONFIG.location_bottle_milk) {
        gi_index = 0x92;
    } else if (gi_index == MMR_CONFIG.location_bottle_gold_dust) {
        gi_index = 0x93;
    }
    *new_gi_index = gi_index;
    return mmr_GetGiFlag(gi_index);
}

u16 mmr_CheckProgressiveUpgrades(u16 gi_index) {
    if (gi_index == MMR_CONFIG.location_sword_kokiri || gi_index == MMR_CONFIG.location_sword_razor || gi_index == MMR_CONFIG.location_sword_gilded) {
        if (z2_file.sword == 0) {
            return MMR_CONFIG.location_sword_kokiri;
        }
        if (z2_file.sword == 1) {
            return MMR_CONFIG.location_sword_razor;
        }
        return MMR_CONFIG.location_sword_gilded;
    }
    if (gi_index == MMR_CONFIG.location_magic_small || gi_index == MMR_CONFIG.location_magic_large) {
        if (z2_file.has_magic == 0) {
            return MMR_CONFIG.location_magic_small;
        }
        return MMR_CONFIG.location_magic_large;
    }
    if (gi_index == MMR_CONFIG.location_wallet_adult || gi_index == MMR_CONFIG.location_wallet_giant) {
        if (z2_file.wallet_upgrade == 0) {
            return MMR_CONFIG.location_wallet_adult;
        }
        return MMR_CONFIG.location_wallet_giant;
    }
    if (gi_index == MMR_CONFIG.location_bomb_bag_small || gi_index == MMR_CONFIG.location_bomb_bag_big || gi_index == MMR_CONFIG.location_bomb_bag_biggest) {
        if (z2_file.bomb_bag == 0) {
            return MMR_CONFIG.location_bomb_bag_small;
        }
        if (z2_file.bomb_bag == 1) {
            return MMR_CONFIG.location_bomb_bag_big;
        }
        return MMR_CONFIG.location_bomb_bag_biggest;
    }
    if (gi_index == MMR_CONFIG.location_quiver_small || gi_index == MMR_CONFIG.location_quiver_large || gi_index == MMR_CONFIG.location_quiver_largest) {
        if (z2_file.quiver == 0) {
            return MMR_CONFIG.location_quiver_small;
        }
        if (z2_file.quiver == 1) {
            return MMR_CONFIG.location_quiver_large;
        }
        return MMR_CONFIG.location_quiver_largest;
    }
    return gi_index;
}

u16 mmr_GetNewGiIndex(z2_game_t *game, z2_actor_t *actor, u16 gi_index, bool grant) {
    if (z2_file.cutscene_id != 0) {
        grant = false;
    }
    u16 new_gi_index = gi_index;
    bool flagged;
    if (z2_file.title_screen_mod != 0) {
        flagged = false;
        grant = false;
    } else {
        flagged = mmr_CheckBottleAndGetGiFlag(gi_index, &new_gi_index);
    }
    if (!flagged) {
        if (grant) {
            mmr_SetGiFlag(new_gi_index);
        }
        new_gi_index = gi_index;
        if (MISC_CONFIG.progressive_upgrades)
        {
            new_gi_index = mmr_CheckProgressiveUpgrades(new_gi_index);
        }
    } else {
        bool is_cycle_repeatable = false;
        for (u8 i = 0; i < MMR_CONFIG.cycle_repeatable_locations_length; i++) {
            if (MMR_CONFIG.cycle_repeatable_locations[i] == new_gi_index) {
                is_cycle_repeatable = true;
                break;
            }
        }
        if (!is_cycle_repeatable) {
            new_gi_index = 0x0A; // Recovery Heart
        }
    }
    if (grant) {
        mmr_SetGiFlag(gi_index);
    }
    if (actor == Z2_LINK(game)) {
        Z2_LINK(game)->get_item = new_gi_index;
    }
    return new_gi_index;
}

void mmr_init(void) {
    // If using vanilla layout, gi-table mod file is not included.
    if (!MISC_CONFIG.vanilla_layout) {
        mmr_load_gi_table();
    }
}
