#ifndef QUEST_ITEMS_H
#define QUEST_ITEMS_H

#include <stdbool.h>
#include "z2.h"

// Check if an item is a quest item.
#define IS_QUEST_ITEM(I) (Z2_ITEM_MOON_TEAR <= (I) && (I) <= Z2_ITEM_PENDANT)

// Check if an inventory slot is a quest items slot.
#define IS_QUEST_SLOT(S) ((S) == Z2_SLOT_QUEST1 || (S) == Z2_SLOT_QUEST2 || (S) == Z2_SLOT_QUEST3)

void quest_items_after_receive(u8 item);
void quest_items_after_removal(u8 item, u8 slot);
void quest_items_after_song_of_time_clear(void);
bool quest_items_get_slot(int *slot, u8 item);
bool quest_items_remove(u8 item);

#endif // QUEST_ITEMS_H
