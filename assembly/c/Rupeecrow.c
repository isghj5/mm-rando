#include <z64.h>
#include "BaseRupee.h"
#include "MMR.h"

ActorEnItem00* Rupeecrow_AfterRupeeSpawn(GlobalContext* ctxt, ActorEnRuppecrow* actor, ActorEnItem00* item) {
    u16 giIndex;
    if ((item->base.params & 0x03) == 1) {
        // blue rupee
        giIndex = 0x392 + (actor->rupeeSpawnCount / 5);
    } else {
        // green or red rupee
        giIndex = 0x37E + actor->rupeeSpawnCount;
    }
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    return item;
}
