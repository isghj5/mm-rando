#include <z64.h>
#include "ExtendedObjects.h"
#include "Objheap.h"
#include "THeap.h"

static const u16 gMaxFrames = 3;

static void LoadObjectFile(u32 objectId, u8* buf) {
    ObjectFileTableEntry* entry = ExtendedObjects_Get((s16)objectId);
    u32 vromStart = entry->vromStart;
    u32 size = entry->vromEnd - vromStart;
    z2_ReadFile(buf, vromStart, size);
}

static size_t GetObjectSize(u32 objectId) {
    ObjectFileTableEntry info = *ExtendedObjects_Get((s16)objectId);
    return (size_t)(info.vromEnd - info.vromStart);
}

static void ItemClear(struct ObjheapItem* obj) {
    obj->buf = NULL;
    obj->objectId = 0;
    obj->frames = 0;
}

static void ItemLoadObject(struct ObjheapItem* object, u32 objectId) {
    object->objectId = objectId;
    LoadObjectFile(objectId, object->buf);
}

/**
 * Return pointer to data of object by Id, or allocate if necessary.
 **/
struct ObjheapItem* Objheap_Allocate(struct Objheap* heap, u32 objectId) {
    // Do initial pass to check if object is already loaded for the specific room.
    struct ObjheapItem *unused = NULL;
    for (size_t i = 0; i < heap->count; i++) {
        struct ObjheapItem* obj = &heap->objs[i];
        if (obj->objectId == objectId) {
            obj->frames = 0;
            return obj;
        } else if (obj->objectId == 0 && unused == NULL) {
            // Remember for allocating if object not found.
            unused = obj;
        }
    }
    if (unused != NULL) {
        struct ObjheapItem* obj = unused;
        size_t objsize = GetObjectSize(objectId);
        // Ensure heap could successfully allocate enough space.
        obj->buf = THeap_Alloc(&heap->heap, objsize);
        if (obj->buf != NULL) {
            obj->frames = 0;
            // Load object from file.
            ItemLoadObject(obj, objectId);
            return obj;
        }
    }
    return NULL;
}

/**
 * Clear underlying THeap and all objheap item entries.
 **/
void Objheap_Clear(struct Objheap* heap) {
    for (size_t i = 0; i < heap->count; i++) {
        ItemClear(&heap->objs[i]);
    }
    THeap_Clear(&heap->heap);
}

/**
 * Initialize objheap with underlying heap buffer and provided item array.
 **/
void Objheap_Init(struct Objheap* heap, void* base, size_t size, struct ObjheapItem* objs, size_t count) {
    THeap_Init(&heap->heap, base, (u32)size);
    heap->objs = objs;
    heap->count = count;
    for (size_t i = 0; i < count; i++) {
        ItemClear(&objs[i]);
    }
}

/**
 * ...
 **/
void Objheap_NextFrame(struct Objheap* heap) {
    for (size_t i = 0; i < heap->count; i++) {
        struct ObjheapItem* obj = &heap->objs[i];
        if (obj->buf != NULL) {
            obj->frames++;
            // Check if current object data should be freed.
            if (obj->frames >= gMaxFrames) {
                THeap_Free(&heap->heap, obj->buf);
                ItemClear(obj);
            }
        }
    }
}
