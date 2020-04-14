#ifndef QUEST_ITEM_STORAGE_H
#define QUEST_ITEM_STORAGE_H

#include <stdbool.h>
#include "types.h"

struct quest_item_storage {
    u8 slots[3][0x06];
};

void quest_item_storage_clear(struct quest_item_storage *storage);
bool quest_item_storage_get_slot(int *slot, int *idx, u8 item);
bool quest_item_storage_has(const struct quest_item_storage *storage, u8 item);
u8 quest_item_storage_next(const struct quest_item_storage *storage, u8 item);
bool quest_item_storage_put(struct quest_item_storage *storage, u8 item);
bool quest_item_storage_remove(struct quest_item_storage *storage, u8 item);

#endif // QUEST_ITEM_STORAGE_H
