#ifndef SAVE_FILE_H
#define SAVE_FILE_H

#include <stdbool.h>
#include <z64.h>
#include "QuestItemStorage.h"

#define SAVE_FILE_CONFIG_MAGIC 0x53415645

#define SAVE_FILE_OFFSET_NEW 0x100C
#define SAVE_FILE_OFFSET_OWL 0x3CA0

struct SaveFileConfig {
    /* 0x00 */ u32 magic;
    /* 0x04 */ u32 version;
    /* 0x08 */ struct QuestItemStorage questStorage;
}; // size = 0x1A

extern struct SaveFileConfig SAVE_FILE_CONFIG;

void SaveFile_Clear(void);
u32 SaveFile_GetFlashSectionOffset(bool owlSave);
bool SaveFile_Read(const void* src);
void SaveFile_Write(void* dest);

#endif // SAVE_FILE_H
