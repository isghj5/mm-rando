#include <z64.h>
#include "BaseRupee.h"

ActorEnItem00* DekuScrubPlaygroundElevator_AfterSpawnRupee(GlobalContext* ctxt, Actor* spawner, ActorEnItem00* item) {
    u16 giIndexOffset = spawner->params & 0x1F;
    if (giIndexOffset > 5) {
        giIndexOffset -= 2;
    }

    Rupee_CheckAndSetGiIndex(&item->base, ctxt, 0x3B3 + giIndexOffset);
    return item;
}
