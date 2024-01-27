#include <z64.h>
#include "GiantMask.h"

f32 DoorShutter_GetMaxZDistToOpen(ActorPlayer* player) {
    if (GiantMask_IsGiant()) {
        return 0.0f;
    }
    f32 zDist = 50.0f;
    if (player->form == PLAYER_FORM_FIERCE_DEITY) { // TODO && FDAnywhere setting is enabled
        zDist = 63.0f;
    }
    return zDist;
}
