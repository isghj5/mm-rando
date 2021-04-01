#ifndef T_HEAP_H
#define T_HEAP_H

#include <assert.h>
#include <PR/ultratypes.h>

typedef struct THeapEntry THeapEntry;

struct THeapEntry {
    THeapEntry* prev;
    THeapEntry* next;
    u32 length;
    u32 flags;
};

// Enforce size for THeapEntry.
static_assert(sizeof(THeapEntry) == 0x10, "THeapEntry structure size should be exactly 0x10.");

typedef struct THeap {
    void* start;
    void* end;
    THeapEntry* first;
    THeapEntry* last;
} THeap;

void* THeap_Alloc(THeap* heap, u32 length);
void THeap_Clear(THeap* heap);
void THeap_Free(THeap* heap, void* address);
void* THeap_GetEntryData(const THeapEntry* entry);
u32 THeap_GetFullSize(const THeap* heap);
void THeap_Init(THeap* heap, void* start, u32 length);
THeapEntry* THeap_RawAlloc(THeap* heap, u32 length);
void THeap_RawFree(THeap* heap, THeapEntry* entry);

#endif // T_HEAP_H
