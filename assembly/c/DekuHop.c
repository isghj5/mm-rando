#include <z64.h>
#include "Misc.h"

struct DekuHopState {
    u8 lastHop;
    u8 isUsingContinuousDekuHop;
};

static struct DekuHopState gDekuHopState = {
    .lastHop = 5,
    .isUsingContinuousDekuHop = 0,
};

void DekuHop_Handle(ActorPlayer* player, GlobalContext* ctxt) {
    if (!MISC_CONFIG.flags.continuousDekuHop) {
        return;
    }

    if (player->stateFlags.state3 & PLAYER_STATE3_DEKU_HOP) {
        if (player->dekuHopCounter < 5 && player->dekuHopCounter > 0 && player->dekuHopCounter != gDekuHopState.lastHop) {
            if (ctxt->state.input[0].pressEdge.buttons.a) {
                ctxt->state.input[0].pressEdge.buttons.a = 0;
                player->dekuHopCounter++;
                gDekuHopState.lastHop = player->dekuHopCounter;
                gDekuHopState.isUsingContinuousDekuHop = true;
            }
        } else {
            gDekuHopState.lastHop = player->dekuHopCounter;
        }
    } else {
        gDekuHopState.isUsingContinuousDekuHop = false;
    }

}

f32 DekuHop_GetSpeedModifier() {
    if (gDekuHopState.isUsingContinuousDekuHop) {
        return 1;
    }
    return 0.5;
}
