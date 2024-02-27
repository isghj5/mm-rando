#include <z64.h>
#include <math.h>
#include "GiantMask.h"

bool Door_PlayerCloseEnoughToOpen(Actor* actor, Vec3f* offset, ActorPlayer* player) {
    if (GiantMask_IsGiant()) {
        return false;
    }
    z2_Actor_OffsetOfPointInActorCoords(actor, offset, &player->base.currPosRot.pos);
    f32 xyDist = 20.0f;
    f32 zDist = 50.0f;
    if (player->form == PLAYER_FORM_FIERCE_DEITY) { // TODO && FDAnywhere setting is enabled
        zDist = 63.0f;
    }
    return fabsf(offset->y) < xyDist && fabsf(offset->x) < xyDist && fabsf(offset->z) < zDist;
}
