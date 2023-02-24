#include <z64.h>
#include "Misc.h"
#include "MMR.h"

bool GoronElder_HasGivenReward(bool override) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return override || gSaveContext.perm.inv.questStatus.lullabyIntro || gSaveContext.perm.inv.questStatus.goronLullaby;
    }
    return override || MMR_GetGiFlag(0x44E);
}

void GoronElder_GiveReward(ActorEnJg* actor, GlobalContext* ctxt) {
    // Displaced code:
    actor->csAction = 99;
    actor->freezeTimer = 1000;

    if (MISC_CONFIG.internal.vanillaLayout) {
        return;
    }

    MMR_GiveItemToHold(&actor->base, ctxt, 0x44E);
}
