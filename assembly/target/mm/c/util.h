#ifndef UTIL_H
#define UTIL_H

#include "types.h"

#define array_size(a) (sizeof(a) / sizeof(a[0]))

void heap_init();
void *heap_alloc(int bytes);

typedef struct {
    u8 *buf;
    u32 vrom_start;
    u32 size;
} file_t;

void file_init(file_t *file);

#endif // UTIL_H
