#ifndef FILE_SELECT_H
#define FILE_SELECT_H

#include <z64.h>

#define HASH_SYMBOL_COUNT 0x40

struct HashIcons {
    u32 version;
    u16 count;
    u8 symbols[HASH_SYMBOL_COUNT];
};

#endif // FILE_SELECT_H
