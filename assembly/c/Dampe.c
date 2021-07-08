#include <z64.h>
#include "MMR.h"
#include "Misc.h"

void Dampe_GiveReward(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        z2_AddRupees(30);
    } else {
        if (!MMR_GiveItemIfMinor(ctxt, actor, giIndex)) {
            MMR_GiveItemToHold(actor, ctxt, giIndex);
        }
    }
}
