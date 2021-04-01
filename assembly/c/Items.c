#include <z64.h>
#include "Icetrap.h"
#include "Items.h"
#include "QuestItems.h"

/**
 * Helper function used to process receiving a custom item.
 **/
static void HandleCustomItem(GlobalContext* ctxt, u8 item) {
    if (item == CUSTOM_ITEM_ICE_TRAP) {
        Icetrap_PushPending();
    }
}

/**
 * Hook function called after receiving an item.
 *
 * Used to add items into quest storage.
 **/
void Items_AfterReceive(GlobalContext* ctxt, u8 item) {
    // Handle receival quest item.
    QuestItems_AfterReceive(item);
    // Handle custom items.
    HandleCustomItem(ctxt, item);
}

/**
 * Hook function called after removing an item from the inventory and buttons.
 *
 * Used to remove items from quest storage and cycle to the next storage item in the inventory.
 **/
void Items_AfterRemoval(s16 item, s16 slot) {
    // Handle removal of quest item.
    QuestItems_AfterRemoval((u8)item, (u8)slot);
}
