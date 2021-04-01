#ifndef UTIL_H
#define UTIL_H

#include <z64.h>

#define Util_ArraySize(Arr) (sizeof(Arr) / sizeof(Arr[0]))

typedef struct {
    u8* buf;
    u32 vromStart;
    u32 size;
} UtilFile;

void Util_FileInit(UtilFile* file);
void* Util_HeapAlloc(int bytes);
void Util_HeapInit(void);

#endif // UTIL_H
