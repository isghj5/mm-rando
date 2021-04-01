#include <stdbool.h>
#include <z64.h>
#include "MMR.h"
#include "QuestItems.h"
#include "SaveFile.h"

static bool IsOwlSaveSize(size_t size) {
    return size == 0x3CA0;
}

void Savedata_AfterFileInit(GlobalContext* ctxt) {
    // Give extra starting maps.
    for (u8 i = 0; i < 6; i++) {
        if (((MMR_CONFIG.extraStartingMaps.value >> i) & 1) != 0) {
            z2_GiveMap(i);
        }
    }
    // Give extra starting items.
    for (u8 i = 0; i < MMR_CONFIG.extraStartingItems.length; i++) {
        z2_GiveItem(ctxt, MMR_CONFIG.extraStartingItems.ids[i]);
    }
}

/**
 * Hook function called after some savedata has been loaded into SaveContext.
 **/
void Savedata_AfterLoad(GlobalContext* ctxt, Camera* camera, SaveContext* file, const u8* buffer, size_t size) {
    // Read our struct from buffer with flash data
    bool owlSave = IsOwlSaveSize(size);
    u32 offset = SaveFile_GetFlashSectionOffset(owlSave);
    const u8* src = buffer + offset;
    SaveFile_Read(src);
}

/**
 * Hook function called after savedata prepared (inventory & flags cleared via Song of Time).
 **/
void Savedata_AfterPrepare(GlobalContext* ctxt) {
    QuestItems_AfterSongOfTimeClear();
}

/**
 * Hook function called after writing SaveContext to buffer, which is to be written to flash.
 **/
void Savedata_AfterWrite(u8* buffer, SaveContext* file, size_t size, bool owlSave) {
    u32 offset = SaveFile_GetFlashSectionOffset(owlSave);
    u8* dest = buffer + offset;
    SaveFile_Write(dest);
}
