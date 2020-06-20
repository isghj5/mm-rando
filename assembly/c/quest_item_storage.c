#include <stdbool.h>
#include "quest_item_storage.h"
#include "z2.h"

void quest_item_storage_clear(struct quest_item_storage *storage) {
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 6; j++) {
            storage->slots[i][j] = 0;
        }
    }
}

bool quest_item_storage_get_slot(int *slot, int *idx, u8 item) {
    switch (item) {
        case Z2_ITEM_MOON_TEAR:
        case Z2_ITEM_TOWN_DEED:
        case Z2_ITEM_SWAMP_DEED:
        case Z2_ITEM_MOUNTAIN_DEED:
        case Z2_ITEM_OCEAN_DEED:
            *slot = 0;
            *idx = item - Z2_ITEM_MOON_TEAR;
            return true;
        case Z2_ITEM_ROOM_KEY:
        case Z2_ITEM_MAMA_LETTER:
            *slot = 1;
            *idx = item - Z2_ITEM_ROOM_KEY;
            return true;
        case Z2_ITEM_KAFEI_LETTER:
        case Z2_ITEM_PENDANT:
            *slot = 2;
            *idx = item - Z2_ITEM_KAFEI_LETTER;
            return true;
        default:
            return false;
    }
}

bool quest_item_storage_has(const struct quest_item_storage *storage, u8 item) {
    int idx, slot;
    if (quest_item_storage_get_slot(&slot, &idx, item)) {
        return storage->slots[slot][idx] == item;
    } else {
        return false;
    }
}

u8 quest_item_storage_next(const struct quest_item_storage *storage, u8 item) {
    int idx, slot;
    if (quest_item_storage_get_slot(&slot, &idx, item)) {
        for (int i = 1; i < 6; i++) {
            int nextidx = (idx + i) % 6;
            u8 next = storage->slots[slot][nextidx];
            if (next != 0) {
                return next;
            }
        }
    }
    return Z2_ITEM_NONE;
}

bool quest_item_storage_put(struct quest_item_storage *storage, u8 item) {
    int idx, slot;
    if (quest_item_storage_get_slot(&slot, &idx, item)) {
        storage->slots[slot][idx] = item;
        return true;
    } else {
        return false;
    }
}

bool quest_item_storage_remove(struct quest_item_storage *storage, u8 item) {
    int idx, slot;
    if (quest_item_storage_get_slot(&slot, &idx, item)) {
        storage->slots[slot][idx] = 0;
        return true;
    } else {
        return false;
    }
}
