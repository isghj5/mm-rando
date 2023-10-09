#include <z64.h>
#include "GiantMask.h"

f32 Camera_PlayerGetHeight(ActorPlayer* player) {
    f32 result;
    if (player->stateFlags.state1 & PLAYER_STATE1_EPONA) {
        result = 32.0f;
    } else {
        result = 0.0f;
    }

    result += z2_Player_GetHeight_WithoutEpona(player);

    return result * GiantMask_GetScaleModifier();
}
