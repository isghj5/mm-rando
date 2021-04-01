#include <z64.h>
#include "Misc.h"
#include "Player.h"

// returns false if using vanilla layout and cutscene should start
bool StrayFairyGroup_GiveReward(Actor* actor, GlobalContext* ctxt, void* nextState) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return false;
    }

    if (Player_CanReceiveItem(ctxt)) {
        u16 giIndex = actor->params >> 9;
        if (giIndex == 0 && gSaveContext.perm.currentForm == 4) { // Town Fairy + Human
            giIndex = 5;
        }
        giIndex += 0x12C;
        if (z2_SetGetItemLongrange(actor, ctxt, giIndex)) {
            void** nextStatePointer = (void**)(((u8*)actor) + 0x14C);
            *nextStatePointer = nextState;
            z2_remove_generic_flag(ctxt, actor->params >> 9);
        }
    }

    return true;
}
