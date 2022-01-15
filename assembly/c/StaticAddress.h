#ifndef STATIC_ADDRESS_H
#define STATIC_ADDRESS_H

#include "ExternalEffects.h"
#include "QuestItemStorage.h"

// Magic number for StaticAddressConfig: "STAD"
#define STATIC_ADDRESS_CONFIG_MAGIC 0x53544144

typedef struct {
    u32 magic;
    u32 version;
    ExternalEffectsConfig* externalEffectsConfig;
    struct QuestItemStorage* questItemStorage;
} StaticAddressConfig;

void StaticAddress_Init(void);

#endif // STATIC_ADDRESS_H
