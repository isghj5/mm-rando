#include <z64.h>
#include "ActorHelper.h"

void Bomb_UpdateWithParent(ActorEnBom* bomb, GlobalContext* ctxt) {
    // Displaced code:
    z2_Math_Vec3f_ToVec3s(&bomb->base.initPosRot.rot, &bomb->base.parent->currPosRot.pos);

    if (bomb->base.parent->id == ACTOR_EN_ARROW) {
        if (ActorHelper_DoesActorExist(bomb->base.parent, ctxt, ACTORTYPE_ITEMACTION)) {
            ActorEnArrow* arrow = (ActorEnArrow*)bomb->base.parent;

            if (arrow->actorBeingCarried != NULL || arrow->hitFlags != 0) {
                bomb->timer = 0;
            }
            if (bomb->timer == 67) {
                z2_SetActorSize(&bomb->base, 0.002);
            }
        } else {
            bomb->base.parent = NULL;
            z2_SetActorSize(&bomb->base, 0.01);
        }
    }
}
