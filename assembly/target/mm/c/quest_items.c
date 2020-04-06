#include <stdbool.h>
#include "misc.h"
#include "quest_items.h"
#include "quest_item_storage.h"
#include "save_file.h"
#include "z2.h"

static bool check_inventory_slot(u8 item, u8 slot) {
    if (z2_file.inventory[slot] == item) {
        return true;
    } else if (MISC_CONFIG.quest_item_storage && z2_file.inventory[slot] != Z2_ITEM_NONE) {
        return quest_item_storage_has(&SAVE_FILE_CONFIG.quest_storage, item);
    } else {
        return false;
    }
}

/**
 * Hook function called after an item has been received into the inventory.
 *
 * Used to add that item to storage as well, if it is a quest item.
 **/
void quest_items_after_receive(u8 item) {
    // Try to add quest item to storage.
    quest_item_storage_put(&SAVE_FILE_CONFIG.quest_storage, item);
}

/**
 * Hook function called after an item has been removed from the inventory.
 *
 * Used to remove that item from storage as well, if it is a quest item.
 **/
void quest_items_after_removal(u8 item, u8 slot) {
    struct quest_item_storage *storage = &SAVE_FILE_CONFIG.quest_storage;
    // Remove quest item from storage.
    if (quest_item_storage_remove(storage, item)) {
        // Set next item into inventory if any.
        if (MISC_CONFIG.quest_item_storage) {
            u8 next = quest_item_storage_next(storage, item);
            if (next != Z2_ITEM_NONE && IS_QUEST_SLOT(slot)) {
                z2_file.items[slot] = next;
            }
        }
    }
}

/**
 * Hook function called after Song of Time inventory clear.
 *
 * Used to clear quest item storage as well if necessary.
 **/
void quest_items_after_song_of_time_clear(void) {
    // After Song of Time, clear quest items in storage.
    if (MISC_CONFIG.quest_consume != QUEST_CONSUME_NEVER) {
        quest_item_storage_clear(&SAVE_FILE_CONFIG.quest_storage);
    }
}

/**
 * Hook function used to check if a quest item's inventory slot should be cleared during Song of Time.
 **/
void quest_items_clear_inventory_item_sot(u8 item, u8 slot) {
    if (MISC_CONFIG.quest_consume != QUEST_CONSUME_NEVER) {
        // Does not remove from quest item storage since a separate hook does that.
        // (The item Id passed to this function is not correct, so we couldn't do that anyways).
        z2_RemoveItem(item, slot);
    }
}

/**
 * Hook function to be used as a wrapper around existing calls to z2_RemoveItem which are used to
 * remove quest items.
 **/
void quest_items_try_remove_item(u8 item, u8 slot) {
    if (IS_QUEST_ITEM(item) && IS_QUEST_SLOT(slot)) {
        if (MISC_CONFIG.quest_consume != QUEST_CONSUME_NEVER) {
            z2_RemoveItem(item, slot);
            quest_item_storage_remove(&SAVE_FILE_CONFIG.quest_storage, item);
        }
    } else {
        z2_RemoveItem(item, slot);
    }
}

/**
 * Helper function for getting the inventory slot a quest item belongs to.
 **/
bool quest_items_get_slot(int *slot, u8 item) {
    int sslot, idx;
    if (quest_item_storage_get_slot(&sslot, &idx, item)) {
        *slot = ((sslot + 1) * 6) - 1;
        return true;
    } else {
        return false;
    }
}

/**
 * Hook function for checking if we possess the required item for using the door.
 *
 * Used for allowing door access if the quest item is not on current inventory slot, but is in storage.
 **/
bool quest_items_door_check(z2_game_t *game, u8 item, u8 slot) {
    return check_inventory_slot(item, slot);
}

/**
 * Hook function for checking if we possess the required item to not be kicked out of Stock Pot Inn.
 *
 * Used to avoid being kicked out if the quest item is not on current inventory slot, but is in storage.
 **/
bool quest_items_time_tag_check(z2_actor_t *actor, z2_game_t *game, u8 item, u8 slot) {
    return check_inventory_slot(item, slot);
}

/**
 * Hook function for checking if we possess the required item to be presented with a trade prompt.
 *
 * Used to still present prompts for Anju (for Pendant) and the Postman (for Letter to Mama) if
 * their respective items are not on the current inventory slot, but are in storage.
 **/
bool quest_items_fix_trade_prompt(z2_actor_t *actor, z2_game_t *game, u8 item, u8 slot) {
    return check_inventory_slot(item, slot);
}

/**
 * Helper function for removing quest items from both inventory and storage.
 **/
bool quest_items_remove(u8 item) {
    int slot;
    if (quest_items_get_slot(&slot, item)) {
        z2_RemoveItem(item, (u8)slot);
        return quest_item_storage_remove(&SAVE_FILE_CONFIG.quest_storage, item);
    } else {
        return false;
    }
}
