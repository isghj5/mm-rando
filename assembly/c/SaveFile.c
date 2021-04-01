#include <stdbool.h>
#include "QuestItemStorage.h"
#include "SaveFile.h"

struct SaveFileConfig SAVE_FILE_CONFIG = {
    .magic = SAVE_FILE_CONFIG_MAGIC,
    .version = 0,
};

static void InitStruct(struct SaveFileConfig* config) {
    config->magic = SAVE_FILE_CONFIG_MAGIC;
    config->version = 0;
    QuestItemStorage_Clear(&config->questStorage);
}

static bool TryCopyStruct(struct SaveFileConfig* dest, const void* srcBytes) {
    struct SaveFileConfig* src = (struct SaveFileConfig*)srcBytes;
    if (src->magic == SAVE_FILE_CONFIG_MAGIC) {
        // If magic matches, copy over struct data
        z2_memcpy(dest, srcBytes, sizeof(*dest));
        return true;
    } else {
        // Otherwise, initialize struct
        InitStruct(dest);
        return false;
    }
}

/**
 * Clear save file config data.
 **/
void SaveFile_Clear(void) {
    InitStruct(&SAVE_FILE_CONFIG);
}

/**
 * Get the offset of custom save file struct in the save flash data section.
 **/
u32 SaveFile_GetFlashSectionOffset(bool owlSave) {
    if (owlSave) {
        return SAVE_FILE_OFFSET_OWL;
    } else {
        return SAVE_FILE_OFFSET_NEW;
    }
}

/**
 * Read existing save file struct data into our struct.
 **/
bool SaveFile_Read(const void* src) {
    return TryCopyStruct(&SAVE_FILE_CONFIG, src);
}

/**
 * Write save file config to a destination buffer.
 **/
void SaveFile_Write(void* dest) {
    z2_memcpy(dest, &SAVE_FILE_CONFIG, sizeof(SAVE_FILE_CONFIG));
}
