#ifndef LINHEAP_H
#define LINHEAP_H

#include <stdbool.h>
#include <stddef.h>
#include "types.h"

/**
 * "Revolving linear heap" implementation.
 **/
struct linheap {
    u8 *advance;
    u8 *buffer;
    u8 *cur1; // Pointer to next write between: [origin, end)
    u8 *cur2; // Pointer to next write between: [0, origin)
    u8 *origin;
    size_t size;
    bool cycleable; // Whether or not heap can allocate at both buffer end and start during advance state.
};

void * linheap_alloc(struct linheap *heap, size_t size);
void linheap_clear(struct linheap *heap);
void linheap_finish_advance(struct linheap *heap);
void linheap_init(struct linheap *heap, void *base);
bool linheap_is_allocated(const struct linheap *heap, void *address);
bool linheap_is_empty(const struct linheap *heap);
void linheap_prepare_advance(struct linheap *heap);
void linheap_revert_advance(struct linheap *heap);

#endif // LINHEAP_H
