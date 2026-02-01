#include <stdbool.h>
#include <z64.h>
#include "QuestItems.h"

typedef struct {
    u8 itemAction;
    u8 item;
    u8 amount;
    u8 requestType;
    u16 messageId;
    u16 data;
} EnTalkGibudRequestedItem;

#define GIBDO_REQUEST_TYPE_AMMO             0
#define GIBDO_REQUEST_TYPE_BOTTLE           1
#define GIBDO_REQUEST_TYPE_QUEST_TRADE      2
#define GIBDO_REQUEST_TYPE_PHOTO            3
#define GIBDO_REQUEST_TYPE_MASK             4

bool Gibdo_CheckPlayerHasItem(EnTalkGibudRequestedItem* requestedItem) {
    switch (requestedItem->requestType) {
        case GIBDO_REQUEST_TYPE_BOTTLE:
            return z2_Inventory_HasItemInBottle(requestedItem->item);
        case GIBDO_REQUEST_TYPE_PHOTO:
            return gSaveContext.perm.inv.questStatus.pictoboxPhoto && ((gSaveContext.perm.pictoFlags0 & 0xFFF) == requestedItem->data);
        default:
            // TODO do we need to bother checking if the player has the item in the inventory,
            // considering they've already successfully matched the required ItemAction?
            return true;
    }
}

void Gibdo_TakePlayerItem(GlobalContext* ctxt, ActorPlayer* player, EnTalkGibudRequestedItem* requestedItem) {
    switch (requestedItem->requestType) {
        case GIBDO_REQUEST_TYPE_BOTTLE:
            z2_Player_UpdateBottleHeld(ctxt, player, ITEM_EMPTY_BOTTLE, PLAYER_IA_BOTTLE);
            break;
        case GIBDO_REQUEST_TYPE_QUEST_TRADE:
            QuestItems_Remove(requestedItem->item);
            break;
        case GIBDO_REQUEST_TYPE_PHOTO:
            gSaveContext.perm.inv.questStatus.pictoboxPhoto = 0;
            break;
        default:
            break;
    }
}
