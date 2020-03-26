#include "util.h"
#include "z2.h"

extern char G_C_HEAP;
char *heap_next = NULL;

void heap_init() {
    heap_next = &G_C_HEAP;
}

void *heap_alloc(int bytes) {
    int rem = bytes % 16;
    if (rem) bytes += 16 - rem;

    void *result = heap_next;
    heap_next += bytes;
    return result;
}

void file_init(file_t *file) {
    file->buf = heap_alloc(file->size);
    z2_ReadFile(file->buf, file->vrom_start, file->size);
}
