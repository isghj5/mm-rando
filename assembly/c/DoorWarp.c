#include <z64.h>
#include "Misc.h"
#include "MMR.h"

static void SetCutscene(ActorDoorWarp1* actor) {
    u8 count = 0;
    if (gSaveContext.perm.inv.questStatus.odolwasRemains) {
        count++;
    }
    if (gSaveContext.perm.inv.questStatus.gohtsRemains) {
        count++;
    }
    if (gSaveContext.perm.inv.questStatus.gyorgsRemains) {
        count++;
    }
    if (gSaveContext.perm.inv.questStatus.twinmoldsRemains) {
        count++;
    }

    if (MISC_CONFIG.speedups.skipGiantsCutscene) {
        count = MISC_CONFIG.speedups.oathHint && !gSaveContext.perm.inv.questStatus.oathToOrder && count >= MISC_CONFIG.MMRbytes.requiredBossRemains ? 4 : 0;
    } else if (count >= MISC_CONFIG.MMRbytes.requiredBossRemains) {
        count = 4;
    }

    u8 oldCount = gSaveContext.perm.unk_ECC[1] & 0xFF;
    if (count > oldCount) {
        actor->unk_202 = count;
        gSaveContext.perm.unk_ECC[1] = (gSaveContext.perm.unk_ECC[1] & 0xFFFFFF00) | count;
    } else {
        actor->unk_202 = 0;
    }
}

void DoorWarp_GiveItem(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    actor->warpTimer--;
    if (actor->warpTimer == 0) {
        if (!MISC_CONFIG.internal.vanillaLayout && !MMR_GetGiFlag(0x77)) {
            MMR_ProcessItem(ctxt, 0x77, false);
        }

        SetCutscene(actor);
    }
}

void DoorWarp_GiveItem2(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    if (ctxt->interfaceCtx.restrictionFlags[4] && actor->warpTimer2 == 0x90 && !MISC_CONFIG.internal.vanillaLayout) {
        if (!MMR_GetGiFlag(0x77)) {
            MMR_ProcessItem(ctxt, 0x77, false);
        }

        SetCutscene(actor);
    }
}

u8 DoorWarp_GetSpawnItem(ActorDoorWarp1* actor, GlobalContext* ctxt) {
    u8 result = 0;
    switch (ctxt->sceneNum) {
        case SCENE_MITURIN_BS:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.odolwasRemains) {
                    result = 1;
                }
            } else if (!MMR_GetGiFlag(0x448)) {
                result = 1;
            }
            break;
        case SCENE_HAKUGIN_BS:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.gohtsRemains) {
                    result = 2;
                }
            } else if (!MMR_GetGiFlag(0x449)) {
                result = 2;
            }
            break;
        case SCENE_SEA_BS:
            if (MISC_CONFIG.internal.vanillaLayout) {
                if (!gSaveContext.perm.inv.questStatus.gyorgsRemains) {
                    result = 3;
                }
            } else if (!MMR_GetGiFlag(0x44A)) {
                result = 3;
            }
            break;
        case SCENE_INISIE_BS:
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
