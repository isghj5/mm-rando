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
    /* 0x1A */ struct {
                    /* 0x1A */ u16 swordKokiri;
                    /* 0x1C */ u16 swordRazor;
                    /* 0x1E */ u16 magicSmall;
                    /* 0x20 */ u16 walletAdult;
                    /* 0x22 */ u16 walletGiant;
                    /* 0x24 */ u16 bombBagSmall;
                    /* 0x26 */ u16 bombBagBig;
                    /* 0x28 */ u16 quiverSmall;
                    /* 0x2A */ u16 quiverLarge;
                    /* 0x2C */ u16 lullabyIntro;
                } spentUpgrades;
    /* 0x2E */ struct {
        u16 creditsSeen         : 1;
        u16                     : 15;
    } flags;
}; // size = 0x30

extern struct SaveFileConfig SAVE_FILE_CONFIG;

void SaveFile_Clear(void);
u32 SaveFile_GetFlashSectionOffset(bool owlSave);
bool SaveFile_Read(const void* src);
void SaveFile_Write(void* dest);

#endif // SAVE_FILE_H
