#include "misc.h"
#include "mmr.h"
#include "util.h"

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

mmr_gi_t * mmr_get_gi_entry(u32 index) {
    return &g_gi_table[index - 1];
}

void mmr_init(void) {
    // If using vanilla layout, gi-table mod file is not included.
    if (!MISC_CONFIG.vanilla_layout) {
        mmr_load_gi_table();
    }
}
