#include <z64.h>
#include "Misc.h"
#include "MMR.h"

void DoorWarp_GiveItem(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    actor->warpTimer--;
    if (actor->warpTimer == 0 && !MISC_CONFIG.internal.vanillaLayout && !MMR_GetGiFlag(0x77)) {
        MMR_ProcessItem(ctxt, 0x77);
    }
}

void DoorWarp_GiveItem2(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    if (ctxt->interfaceCtx.restrictionFlags[4] && actor->warpTimer2 == 0x90 && !MISC_CONFIG.internal.vanillaLayout && !MMR_GetGiFlag(0x77)) {
        MMR_ProcessItem(ctxt, 0x77);
    }
}

u8 DoorWarp_GetSpawnItem(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    u8 result = 0;
    switch (ctxt->sceneNum) {
        case 0x1F:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.odolwasRemains) {
                    result = 1;
                }
            } else if (!MMR_GetGiFlag(0x448)) {
                result = 1;
            }
            break;
        case 0x44:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.gohtsRemains) {
                    result = 2;
                }
            } else if (!MMR_GetGiFlag(0x449)) {
                result = 2;
            }
            break;
        case 0x5F:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.gyorgsRemains) {
                    result = 3;
                }
            } else if (!MMR_GetGiFlag(0x44A)) {
                result = 3;
            }
            break;
        case 0x36:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.twinmoldsRemains) {
                    result = 4;
                }
            } else if (!MMR_GetGiFlag(0x44B)) {
                result = 4;
            }
            break;
    }
    return result;
}
