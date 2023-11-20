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

u16 Minifrog_GetGiIndex(ActorEnMinifrog* this, GlobalContext* ctxt) {
    if (this->frogIndex > 0 && this->frogIndex < ARRAY_COUNT(giIndices)) {
        return giIndices[this->frogIndex];
    }
    return 0;
}

void Minifrog_GiveReward(Actor* actor, GlobalContext* ctxt, s16 frogIndex) {
    if (frogIndex <= 0 || frogIndex >= ARRAY_COUNT(giIndices)) {
        return;
    }

    if (!MISC_CONFIG.internal.vanillaLayout) {
        u16 giIndex = giIndices[frogIndex];
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->message != 0) {
            ActorPlayer* player = GET_PLAYER(ctxt);
            MMR_GiveItemToHold(actor, ctxt, giIndex);
            return;
        }
    }

    SET_WEEKEVENTREG(isFrogReturnedFlags[frogIndex]);
}

bool Minifrog_HasGivenReward(ActorEnMinifrog* this, GlobalContext* ctxt) {
    if (this->frogIndex == 0 || this->frogIndex >= ARRAY_COUNT(giIndices)) {
        return true;
    }

    if (!MISC_CONFIG.internal.vanillaLayout) {
        u16 giIndex = giIndices[this->frogIndex];
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->message != 0) {
            return MMR_GetGiFlag(giIndex);
        }
    }

    return CHECK_WEEKEVENTREG(isFrogReturnedFlags[this->frogIndex]);
}

void Minifrog_WaitForDespawn(Actor* actor, GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (player->givingActor != actor) {
        z2_ActorUnload(actor);
    }
}
