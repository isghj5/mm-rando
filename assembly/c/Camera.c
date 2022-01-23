#include <z64.h>
#include "GiantMask.h"

// f32 Camera_GetHumanBaseDistance(ActorPlayer* player) {
//     f32 result = 44.0;
//     if (gSaveContext.perm.mask == 0x14) {
//         result = 704.0;
//     }
//     if (player->stateFlags.state1 & PLAYER_STATE1_EPONA) {
//         result += 32.0;
//     }
//     return result;
// }

f32 Camera_PlayerGetExtraHeight(ActorPlayer* player) {
    f32 result;
    if (player->stateFlags.state1 & PLAYER_STATE1_EPONA) {
        result = 32.0f;
    } else {
        result = 0.0f;
    }

    switch (player->form) {
        default:
        case PLAYER_FORM_FIERCE_DEITY:
            result += 124.0f;
            break;
        case PLAYER_FORM_GORON:
            // (player->stateFlags3 & 0x1000): being curled?
            result += ((player->stateFlags.state3 & 0x1000) ? 34.0f : 80.0f);
            break;
        case PLAYER_FORM_ZORA:
            result += 68.0f;
            break;
        case PLAYER_FORM_DEKU:
            result += 36.0f;
            break;
        case PLAYER_FORM_HUMAN:
            result += 44.0f;
            break;
    }

    // if (gSaveContext.perm.mask == 0x14) { // todo get modifier from GiantMask.c
    //     result += 440.0;
    // }

    return (result * GiantMask_GetScaleModifier() * 100.0f) - result;
}
