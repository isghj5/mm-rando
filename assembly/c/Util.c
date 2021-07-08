#include <z64.h>
#include "Util.h"

extern char G_C_HEAP;
char* gHeapNext = NULL;

void Util_HeapInit(void) {
    gHeapNext = &G_C_HEAP;
}

void* Util_HeapAlloc(int bytes) {
    int rem = bytes % 16;
    if (rem) {
        bytes += 16 - rem;
    }
    void* result = gHeapNext;
    gHeapNext += bytes;
    return result;
}

void Util_FileInit(UtilFile* file) {
    file->buf = Util_HeapAlloc(file->size);
    z2_ReadFile(file->buf, file->vromStart, file->size);
}
