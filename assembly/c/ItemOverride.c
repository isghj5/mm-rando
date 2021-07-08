#include <stdbool.h>
#include <z64.h>

#define ITEM_OVERRIDE_ENTRY_COUNT 0x480

struct ItemOverrideEntry {
    u16 graphic;
    u16 object;
};

// Item graphic override table, used by ice traps to mimic other items.
struct ItemOverrideEntry ITEM_OVERRIDE_ENTRIES[ITEM_OVERRIDE_ENTRY_COUNT] = { 0 };

// Full size of item graphic override table.
u32 ITEM_OVERRIDE_COUNT = ITEM_OVERRIDE_ENTRY_COUNT;

/**
 * Helper function to retrieve the item graphic override values for a specific get-item index.
 **/
bool ItemOverride_GetGraphic(u32 index, u16* graphic, u16* object) {
    if (index < ITEM_OVERRIDE_ENTRY_COUNT) {
        struct ItemOverrideEntry entry = ITEM_OVERRIDE_ENTRIES[index];
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
