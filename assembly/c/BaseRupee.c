#include <z64.h>
#include "MMR.h"
#include "Misc.h"

static u16 collectableTable[0x80];

u16 GetTweakedCollectableSceneIndex(u16 sceneIndex) {
    switch (sceneIndex) {
        case 0x1C: // Path to Mountain Village
            if (gSaveContext.perm.weekEventReg.mountainCleared) {
                return 0x71;
            }
            break;
        case 0x5C: // Snowhead
            if (gSaveContext.perm.weekEventReg.mountainCleared) {
                return 0x72;
            }
            break;
    }
    return sceneIndex;
}

void Rupee_LoadCollectableTable(GlobalContext* ctxt) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return;
    }

    u16 sceneIndex = GetTweakedCollectableSceneIndex(ctxt->sceneNum);

    u32 index = MISC_CONFIG.shorts.collectableTableFileIndex;
    DmaEntry entry = dmadata[index];

    u32 start = entry.romStart + (sceneIndex * 0x100);

    z2_RomToRam(start, &collectableTable, sizeof(collectableTable));
}

u16 Rupee_CollectableFlagToGiIndex(u16 collectableFlag) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return 0;
    }

    return collectableTable[collectableFlag];
}

// TODO pick a definitely unused part of the actor memory
// So far, none of the actors using this have feet, so we're okay to use this.
void Rupee_SetGiIndex(Actor* actor, u16 giIndex) {
    u16* pointer = (u16*)(&actor->shape.feetPos[1]);
    *pointer = giIndex;
}

u16 Rupee_GetGiIndex(Actor* actor) {
    u16* pointer = (u16*)(&actor->shape.feetPos[1]);
    return *pointer;
}

void Rupee_SetDrawGiIndex(Actor* actor, u16 drawGiIndex) {
    u16* pointer = (u16*)(&actor->shape.feetPos[1]);
    pointer++;
    *pointer = drawGiIndex;
}

u16 Rupee_GetDrawGiIndex(Actor* actor) {
    u16* pointer = (u16*)(&actor->shape.feetPos[1]);
    pointer++;
    return *pointer;
}

void Rupee_CheckAndSetGiIndex(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return;
    }

    GetItemEntry* entry = MMR_GetGiEntry(giIndex);
    if (entry->message != 0) {
        Rupee_SetGiIndex(actor, giIndex);
        u16 drawGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
        Rupee_SetDrawGiIndex(actor, drawGiIndex);
    }
}
