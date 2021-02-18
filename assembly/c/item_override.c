#include <stdbool.h>
#include "z2.h"

#define ITEM_OVERRIDE_ENTRY_COUNT 0x200

struct item_override_entry {
    u16 graphic;
    u16 object;
};

// Item graphic override table, used by ice traps to mimic other items.
struct item_override_entry ITEM_OVERRIDE_ENTRIES[ITEM_OVERRIDE_ENTRY_COUNT] = { 0 };

// Full size of item graphic override table.
u32 ITEM_OVERRIDE_COUNT = ITEM_OVERRIDE_ENTRY_COUNT;

/**
 * Helper function to retrieve the item graphic override values for a specific get-item index.
 **/
bool item_override_get_graphic(u32 index, u16 *graphic, u16 *object) {
    if (index < ITEM_OVERRIDE_ENTRY_COUNT) {
        struct item_override_entry entry = ITEM_OVERRIDE_ENTRIES[index];
        if (entry.graphic == 0 && entry.object == 0) {
            return false;
        } else {
            *graphic = entry.graphic;
            *object = entry.object;
            return true;
        }
    } else {
        return false;
    }
}
