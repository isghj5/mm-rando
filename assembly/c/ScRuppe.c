#include <z64.h>
#include "MMR.h"
#include "Player.h"
#include "BaseRupee.h"

void ScRuppe_Constructor(ActorEnScRuppe* actor, GlobalContext* ctxt) {
    u16 giIndex = 0x362;
    // There's only two instances of this actor in the game. They'll both give the same item.
    // If more are discovered, this will have to be updated.

    Rupee_CheckAndSetGiIndex(&actor->base, ctxt, giIndex);
}

bool ScRuppe_GiveItem(ActorEnScRuppe* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetGiIndex(&actor->base);
    if (giIndex == 0) {
        return false;
    }
    if (MMR_GiveItem(ctxt, &actor->base, giIndex)) {
        Rupee_SetGiIndex(&actor->base, 0);
    }
    return true;
}

u16 ScRuppe_BeforeDisappearing(ActorEnScRuppe* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetDrawGiIndex(&actor->base);
    if (giIndex > 0) {
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->item == 0xB0) {
            // Ice Trap
            if (actor->disappearCountdown < 0x40) {
                z2_PlayLoopingSfxAtActor(&actor->base, 0x31A4);
            }
            return actor->disappearCountdown < 0x40;
        }
    }
    return actor->disappearCountdown < 0x1F;
}
