#ifndef OBJHEAP_H
#define OBJHEAP_H

#include <z64.h>
#include "THeap.h"

struct ObjheapItem {
    u8* buf;
    u16 objectId;
    u16 frames;
};

struct Objheap {
    THeap heap;
    struct ObjheapItem* objs;
    size_t count;
};

struct ObjheapItem * Objheap_Allocate(struct Objheap* heap, u32 objectId);
void Objheap_Clear(struct Objheap* heap);
void Objheap_Init(struct Objheap* heap, void* base, size_t size, struct ObjheapItem* objs, size_t count);
void Objheap_NextFrame(struct Objheap* heap);

#endif // OBJHEAP_H
