#include <z64.h>
#include "BaseRupee.h"

ActorEnItem00* HitTag_RupeeSpawn(GlobalContext* ctxt, Actor* actor, u8 count) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, &actor->currPosRot.pos, 0);
    u16 tagId = actor->params & 0x1F;
    u16 giIndex = 0x3C5 + tagId*3 + count;
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    return item;
}
