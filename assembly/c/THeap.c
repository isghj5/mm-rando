// Simplified implementation of a "traditional" heap.

#include "THeap.h"
#include <z64.h>

struct SearchResult {
    void* address;
    u32 length;
    THeapEntry* prev;
    THeapEntry* next;
};

static void SetSearchResult(struct SearchResult* result, void* address, u32 length, THeapEntry* prev, THeapEntry* next) {
    result->address = address;
    result->length = length;
    result->prev = prev;
    result->next = next;
}

static bool UpdateSearchResult(struct SearchResult* result, void* address, u32 length, THeapEntry* prev, THeapEntry* next) {
    if (result->address == NULL || length < result->length) {
        SetSearchResult(result, address, length, prev, next);
        return true;
    } else {
        return false;
    }
}

static u32 Align16(u32 value) {
    return (value + 0xF) & 0xFFFFFFF0;
}

/**
 * Get address immediately after specified allocated data.
 **/
static void* GetAfterEntry(const THeapEntry* entry) {
    u32 size = sizeof(*entry) + Align16(entry->length);
    return (void*)((u32)entry + size);
}

/**
 * Get amount of "unused space" between end of given THeapEntry and some address after.
 **/
static u32 GetUnusedSpace(const THeap* heap, const THeapEntry* entry, const void* next) {
    void* after = GetAfterEntry(entry);
    u32 delta;
    if (next != NULL) {
        delta = (u32)next - (u32)after;
    } else {
        delta = (u32)heap->end - (u32)after;
    }
    return delta;
}

/**
 * Initialize THeapEntry for given address and length.
 **/
static THeapEntry* InitEntry(void* address, u32 length) {
    THeapEntry* entry = (THeapEntry*)address;
    entry->prev = NULL;
    entry->next = NULL;
    entry->length = length;
    entry->flags = 0;
    return entry;
}

/**
 * Insert a THeapEntry into the heap doubly-linked list.
 **/
static THeapEntry* InsertEntry(THeap* heap, THeapEntry* prev, THeapEntry* next, void* address, u32 length) {
    THeapEntry* entry = InitEntry(address, length);
    if (heap->first == NULL || heap->first == next) {
        heap->first = entry;
    }
    if (heap->last == NULL || heap->last == prev) {
        heap->last = entry;
    }
    if (prev != NULL) {
        prev->next = entry;
        entry->prev = prev;
    }
    if (next != NULL) {
        next->prev = entry;
        entry->next = next;
    }
    return entry;
}

/**
 * Remove a THeapEntry from the heap doubly-linked list.
 **/
static void RemoveEntry(THeap* heap, THeapEntry* entry) {
    if (heap->first == entry) {
        heap->first = entry->next;
    }
    if (heap->last == entry) {
        heap->last = entry->prev;
    }
    if (entry->prev != NULL) {
        entry->prev->next = entry->next;
    }
    if (entry->next != NULL) {
        entry->next->prev = entry->prev;
    }
    entry->prev = NULL;
    entry->next = NULL;
}

/**
 * Calculate the full size of a heap entry, including the THeapEntry header structure.
 **/
static u32 CalcEntrySize(u32 length) {
    return sizeof(THeapEntry) + Align16(length);
}

/**
 * Get pointer to data after THeapEntry.
 **/
void* THeap_GetEntryData(const THeapEntry* entry) {
    return (void*)((u8*)entry + sizeof(*entry));
}

/**
 * Get full size of heap.
 **/
u32 THeap_GetFullSize(const THeap* heap) {
    return (u32)heap->end - (u32)heap->start;
}

/**
 * Allocate some data on the heap.
 **/
void* THeap_Alloc(THeap* heap, u32 length) {
    THeapEntry* entry = THeap_RawAlloc(heap, length);
    if (entry != NULL) {
        return THeap_GetEntryData(entry);
    } else {
        return NULL;
    }
}

/**
 * Allocate some data on the heap and return a pointer to the THeapEntry before the data.
 **/
THeapEntry* THeap_RawAlloc(THeap* heap, u32 length) {
    u32 size = CalcEntrySize(length);
    if (heap->first == NULL) {
        // If heap is empty, ensure heap is large enough for allocation.
        u32 unused = THeap_GetFullSize(heap);
        if (size <= unused) {
            THeapEntry* result = InsertEntry(heap, NULL, NULL, heap->start, length);
            return result;
        } else {
            return NULL;
        }
    } else {
        struct SearchResult search = { 0 };
        // If heap is not empty, find ideal empty space for allocation.
        // Begin by checking for unused space between heap start and first allocation.
        u32 unused = (u32)heap->first - (u32)heap->start;
        if (size <= unused) {
            UpdateSearchResult(&search, heap->start, unused, NULL, heap->first);
        }
        // Then check for unused space between each allocation.
        THeapEntry* cur = heap->first;
        while (cur != NULL) {
            if (cur->next != NULL) {
                // Get unused space between two allocations.
                unused = GetUnusedSpace(heap, cur, (const void*)cur->next);
                if (size <= unused) {
                    void* after = GetAfterEntry(cur);
                    UpdateSearchResult(&search, after, unused, cur, cur->next);
                }
            } else {
                // Get unused space between final allocation and heap end.
                unused = GetUnusedSpace(heap, cur, heap->end);
                if (size <= unused) {
                    void* after = GetAfterEntry(cur);
                    UpdateSearchResult(&search, after, unused, cur, NULL);
                }
            }
            cur = cur->next;
        }
        if (search.address != NULL) {
            THeapEntry* result = InsertEntry(heap, search.prev, search.next, search.address, length);
            return result;
        }
        return NULL;
    }
}

/**
 * "Clear" the heap.
 **/
void THeap_Clear(THeap* heap) {
    heap->first = NULL;
    heap->last = NULL;
}

/**
 * Free allocated heap data.
 **/
void THeap_Free(THeap* heap, void* address) {
    THeapEntry* entry = (THeapEntry*)((u32)address - sizeof(THeapEntry));
    THeap_RawFree(heap, entry);
}

/**
 * Free allocated heap data given a pointer to the corresponding THeapEntry.
 **/
void THeap_RawFree(THeap* heap, THeapEntry* entry) {
    RemoveEntry(heap, entry);
}

/**
 * Initialize a given THeap.
 **/
void THeap_Init(THeap* heap, void* start, u32 length) {
    heap->start = start;
    heap->end = (void*)((u32)start + length);
    heap->first = NULL;
    heap->last = NULL;
}
