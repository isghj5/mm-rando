#ifndef LINHEAP_C
#define LINHEAP_C

#include <stddef.h>
#include "types.h"

/**
 * Extremely basic "linear heap" implementation.
 **/
struct linheap {
    u8 *start;
    u8 *cur;
    size_t size;
};

void * linheap_alloc(struct linheap *heap, size_t size);
void linheap_clear(struct linheap *heap);
void linheap_init(struct linheap *heap, void *base);

#endif // LINHEAP_C
