#include <stdbool.h>
#include <z64.h>
#include "MMR.h"
#include "QuestItems.h"
#include "SaveFile.h"

static bool IsOwlSaveSize(size_t size) {
    return size == 0x3CA0;
}

typedef struct {
    u8 originalId;
    u8 newId;
} ResettingItem; // size = 0x2

// TODO enum for ItemToReceive
#define resettingItemsLength 6
static ResettingItem resettingItems[resettingItemsLength] = {
    {
        .originalId = 0xA, // Magic Bean
        .newId = 0xA,
    },
    {
        .originalId = 0xC, // Powder Keg
        .newId = 0xC,
    },
    {
        .originalId = 0x11, // Bottle with Red Potion
        .newId = 0x13, // Red Potion
    },
    {
        .originalId = 0x18, // Bottle with Milk
        .newId = 0xA0, // Milk
    },
    {
        .originalId = 0x22, // Bottle with Gold Dust
        .newId = 0xA1, // Gold Dust
    },
    {
        .originalId = 0x25, // Bottle with Chateau Romani
        .newId = 0x9F, // Chateau Romani
    },
};

static void Savedata_SetStartingItems(GlobalContext* ctxt) {
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

static void Savedata_ResetStartingItems(GlobalContext* ctxt) {
    // Give extra starting items.
    for (u8 i = 0; i < MMR_CONFIG.extraStartingItems.length; i++) {
        u8 originalId = MMR_CONFIG.extraStartingItems.ids[i];

        for (u8 i = 0; i < resettingItemsLength; i++) {
            if (resettingItems[i].originalId == originalId) {
                z2_GiveItem(ctxt, resettingItems[i].newId);
                break;
            }
        }
    }
}

void Savedata_AfterFileInit(GlobalContext* ctxt) {
    Savedata_SetStartingItems(ctxt);
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
    Savedata_ResetStartingItems(ctxt);
}

/**
 * Hook function called after writing SaveContext to buffer, which is to be written to flash.
 **/
void Savedata_AfterWrite(u8* buffer, SaveContext* file, size_t size, bool owlSave) {
    u32 offset = SaveFile_GetFlashSectionOffset(owlSave);
    u8* dest = buffer + offset;
    SaveFile_Write(dest);
}
