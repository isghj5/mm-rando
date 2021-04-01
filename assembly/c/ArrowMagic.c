#include <stdbool.h>
#include <z64.h>
#include "ArrowCycle.h"
#include "Misc.h"

struct ArrowMagicState {
    Actor* arrow;
};

static struct ArrowMagicState gArrowMagicState = {
    .arrow = NULL,
};

static void UpdateState(struct ArrowMagicState* state, ActorPlayer* player, GlobalContext* ctxt) {
    Actor* arrow = state->arrow;
    // Check if arrow has been shot, speed is checked to ensure arrow isn't just put away.
    if (player->base.child == NULL && arrow != NULL && arrow->speedXZ != 0.0) {
        // Update state to consume magic once arrow is shot.
        if (gSaveContext.extra.magicConsumeState == 4) {
            gSaveContext.extra.magicConsumeState = 2;
        } else if (arrow->params == 7) {
            // Update magic consume cost, prevents Deku Bubble from taking more magic than expected
            // if used while an elemental arrow effect is still active.
            gSaveContext.extra.magicConsumeCost = 2;
            // Deku Bubble won't set magic consumption state to 0 later, so apply magic cost manually.
            if (!HasInfiniteMagic(gSaveContext)) {
                gSaveContext.perm.unk24.currentMagic -= gSaveContext.extra.magicConsumeCost;
                if (gSaveContext.perm.unk24.currentMagic < 0) {
                    gSaveContext.perm.unk24.currentMagic = 0;
                }
            }
        }
        state->arrow = NULL;
    } else {
        state->arrow = ArrowCycle_FindArrow(player, ctxt);
    }
}

/**
 * Hook function used to get the initial magic consumption state for an elemental arrow.
 **/
s16 ArrowMagic_GetInitialConsumeState(GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.arrowMagicShow) {
        return 4;
    } else {
        return 1;
    }
}

/**
 * Hook function used to check whether or not the magic cost of the current elemental arrow should
 * be written to RDRAM.
 **/
bool ArrowMagic_ShouldSetMagicCost(GlobalContext* ctxt, bool infMagic) {
    if (MISC_CONFIG.flags.arrowMagicShow) {
        // If showing magic cost, always set magic cost field for consistency.
        return true;
    } else {
        // Vanilla behavior, do not set magic cost field if infinite magic is active.
        return !infMagic;
    }
}

/**
 * Handle arrow magic consumption state.
 **/
void ArrowMagic_Handle(ActorPlayer* player, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.arrowMagicShow) {
        UpdateState(&gArrowMagicState, player, ctxt);
    }
}
