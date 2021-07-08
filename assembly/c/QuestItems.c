#include <stdbool.h>
#include <z64.h>
#include "Misc.h"
#include "QuestItems.h"
#include "QuestItemStorage.h"
#include "SaveFile.h"

static bool CheckInventorySlot(u8 item, u8 slot) {
    if (gSaveContext.perm.inv.items[slot] == item) {
        return true;
    } else if (MISC_CONFIG.flags.questItemStorage && gSaveContext.perm.inv.items[slot] != ITEM_NONE) {
        return QuestItemStorage_Has(&SAVE_FILE_CONFIG.questStorage, item);
    } else {
        return false;
    }
}

/**
 * Hook function called after an item has been received into the inventory.
 *
 * Used to add that item to storage as well, if it is a quest item.
 **/
void QuestItems_AfterReceive(u8 item) {
    // Try to add quest item to storage.
    QuestItemStorage_Put(&SAVE_FILE_CONFIG.questStorage, item);
}

/**
 * Hook function called after an item has been removed from the inventory.
 *
 * Used to remove that item from storage as well, if it is a quest item.
 **/
void QuestItems_AfterRemoval(u8 item, u8 slot) {
    struct QuestItemStorage* storage = &SAVE_FILE_CONFIG.questStorage;
    // Remove quest item from storage.
    if (QuestItemStorage_Remove(storage, item)) {
        // Set next item into inventory if any.
        if (MISC_CONFIG.flags.questItemStorage) {
            u8 next = QuestItemStorage_Next(storage, item);
            if (next != ITEM_NONE && QuestItems_IsQuestSlot(slot)) {
                gSaveContext.perm.inv.items[slot] = next;
            }
        }
    }
}

/**
 * Hook function called after Song of Time inventory clear.
 *
 * Used to clear quest item storage as well if necessary.
 **/
void QuestItems_AfterSongOfTimeClear(void) {
    // After Song of Time, clear quest items in storage.
    if (MISC_CONFIG.flags.questConsume != QUEST_CONSUME_NEVER) {
        QuestItemStorage_Clear(&SAVE_FILE_CONFIG.questStorage);
    }
}

/**
 * Hook function used to check if a quest item's inventory slot should be cleared during Song of Time.
 **/
void QuestItems_ClearInventoryItemSot(u8 item, u8 slot) {
    if (MISC_CONFIG.flags.questConsume != QUEST_CONSUME_NEVER) {
        // Does not remove from quest item storage since a separate hook does that.
        // (The item Id passed to this function is not correct, so we couldn't do that anyways).
        z2_RemoveItem(item, slot);
    }
}

/**
 * Hook function to be used as a wrapper around existing calls to z2_RemoveItem which are used to
 * remove quest items.
 **/
void QuestItems_TryRemoveItem(u8 item, u8 slot) {
    if (QuestItems_IsQuestItem(item) && QuestItems_IsQuestSlot(slot)) {
        if (MISC_CONFIG.flags.questConsume != QUEST_CONSUME_NEVER) {
            z2_RemoveItem(item, slot);
            QuestItemStorage_Remove(&SAVE_FILE_CONFIG.questStorage, item);
        }
    } else {
        z2_RemoveItem(item, slot);
    }
}

/**
 * Helper function for getting the inventory slot a quest item belongs to.
 **/
bool QuestItems_GetSlot(int* slot, u8 item) {
    int sslot, idx;
    if (QuestItemStorage_GetSlot(&sslot, &idx, item)) {
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
bool QuestItems_DoorCheck(GlobalContext* ctxt, u8 item, u8 slot) {
    return CheckInventorySlot(item, slot);
}

/**
 * Hook function for checking if we possess the required item to not be kicked out of Stock Pot Inn.
 *
 * Used to avoid being kicked out if the quest item is not on current inventory slot, but is in storage.
 **/
bool QuestItems_TimeTagCheck(Actor* actor, GlobalContext* ctxt, u8 item, u8 slot) {
    return CheckInventorySlot(item, slot);
}

/**
 * Hook function for checking if we possess the required item to be presented with a trade prompt.
 *
 * Used to still present prompts for Anju (for Pendant) and the Postman (for Letter to Mama) if
 * their respective items are not on the current inventory slot, but are in storage.
 **/
bool QuestItems_FixTradePrompt(Actor* actor, GlobalContext* ctxt, u8 item, u8 slot) {
    return CheckInventorySlot(item, slot);
}

/**
 * Helper function for removing quest items from both inventory and storage.
 **/
bool QuestItems_Remove(u8 item) {
    int slot;
    if (QuestItems_GetSlot(&slot, item)) {
        z2_RemoveItem(item, (u8)slot);
        return QuestItemStorage_Remove(&SAVE_FILE_CONFIG.questStorage, item);
    } else {
        return false;
    }
}
