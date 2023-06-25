#include <z64.h>
#include "MMR.h"
#include "Misc.h"
#include "macro.h"
#include "Util.h"

static u16 isFrogReturnedFlags[] = {
    0, 0x2040, 0x2080, 0x2101, 0x2102,
};

static u16 giIndices[] = {
    0, 0x466, 0x46D, 0x46F, 0x473
};

void Minifrog_GiveReward(Actor* actor, GlobalContext* ctxt, s16 frogIndex) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        SET_WEEKEVENTREG(isFrogReturnedFlags[frogIndex]);
    } else {
        if (frogIndex > 0 && frogIndex < ARRAY_COUNT(giIndices)) {
            u16 giIndex = giIndices[frogIndex];
            ActorPlayer* player = GET_PLAYER(ctxt);
            MMR_GiveItemToHold(actor, ctxt, giIndex);
        }
    }
}

void Minifrog_WaitForDespawn(Actor* actor, GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (player->givingActor != actor) {
        z2_ActorUnload(actor);
    }
}
