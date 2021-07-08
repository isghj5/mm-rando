#ifndef QUEST_ITEM_STORAGE_H
#define QUEST_ITEM_STORAGE_H

#include <stdbool.h>
#include <z64.h>

struct QuestItemStorage {
    /* 0x00 */ u8 slots[3][6];
}; // size = 0x12

void QuestItemStorage_Clear(struct QuestItemStorage* storage);
bool QuestItemStorage_GetSlot(int* slot, int* idx, u8 item);
bool QuestItemStorage_Has(const struct QuestItemStorage* storage, u8 item);
u8 QuestItemStorage_Next(const struct QuestItemStorage* storage, u8 item);
bool QuestItemStorage_Put(struct QuestItemStorage* storage, u8 item);
bool QuestItemStorage_Remove(struct QuestItemStorage* storage, u8 item);

#endif // QUEST_ITEM_STORAGE_H
