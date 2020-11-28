#ifndef OBJHEAP_H
#define OBJHEAP_H

#include "linheap.h"
#include "types.h"

struct objheap_item {
    u8 *buf;
    u16 object_id;
    s8 room;
};

struct objheap {
    struct linheap linheap;
    struct objheap_item *objs;
    size_t count;
    s8 cur_room;
    s8 nex_room;
    u8 op; // Prepared operation when data is safe to free.
};

struct objheap_item * objheap_allocate(struct objheap *heap, u32 object_id, s8 room);
void objheap_clear(struct objheap *heap);
void objheap_finish_advance(struct objheap *heap);
void objheap_flush_operation(struct objheap *heap);
void objheap_handle_room_unload(struct objheap *heap, s8 cur_room);
void objheap_init(struct objheap *heap, void *base, size_t size, struct objheap_item *objs, size_t count);
void objheap_init_room(struct objheap *heap, s8 room);
void objheap_prepare_advance(struct objheap *heap, s8 room);
void objheap_revert_advance(struct objheap *heap);

#endif // OBJHEAP_H
