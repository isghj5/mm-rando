#include <z64.h>
#include "BaseRupee.h"
#include "Player.h"

void InvisibleRupee_GiveItem(Actor* actor, GlobalContext* ctxt) {
    u16 type = actor->params & 0x03;
    Vec3f* pos = &GET_PLAYER(ctxt)->base.currPosRot.pos;
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, pos, type);

    u16 flag = actor->params >> 2;
    u16 giIndex = 0x34C + flag;
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
}
