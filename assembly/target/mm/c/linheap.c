#include <stdbool.h>
#include <stddef.h>
#include "linheap.h"

static size_t linheap_remaining(struct linheap *heap) {
    u8 *end = heap->start + heap->size;
    return (size_t)(end - heap->cur);
}

static bool linheap_has_space(struct linheap *heap, size_t size) {
    size_t remaining = linheap_remaining(heap);
    return size <= remaining;
}

void * linheap_alloc(struct linheap *heap, size_t size) {
    // 0x10 byte alignment
    size_t rem = size % 0x10;
    if (rem) {
        size += (0x10 - rem);
    }

    // Check if enough space is available before allocating
    if (linheap_has_space(heap, size)) {
        void *result = heap->cur;
        heap->cur += size;
        return result;
    } else {
        return NULL;
    }
}

void linheap_clear(struct linheap *heap) {
    heap->cur = heap->start;
}

void linheap_init(struct linheap *heap, void *base) {
    heap->start = heap->cur = (u8*)base;
}
