#include "icetrap.h"
#include "items.h"
#include "quest_items.h"
#include "z2.h"

/**
 * Helper function used to process receiving a custom item.
 **/
static void items_handle_custom_item(z2_game_t *game, u8 item) {
    if (item == Z2_ICE_TRAP) {
        icetrap_push_pending();
    }
}

/**
 * Hook function called after receiving an item.
 *
 * Used to add items into quest storage.
 **/
void items_after_receive(z2_game_t *game, u8 item) {
    // Handle receival quest item.
    quest_items_after_receive(item);
    // Handle custom items.
    items_handle_custom_item(game, item);
}

/**
 * Hook function called after removing an item from the inventory and buttons.
 *
 * Used to remove items from quest storage and cycle to the next storage item in the inventory.
 **/
void items_after_removal(s16 item, s16 slot) {
    // Handle removal of quest item.
    quest_items_after_removal((u8)item, (u8)slot);
}
