#include <z64.h>
#include "Misc.h"

/**
 * Hook function which checks if Scarecrow actor should "activate" and prepare for spawning.
 **/
bool Scarecrow_ShouldActivate(ActorEnKakasi* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.freeScarecrow) {
        ActorPlayer *player = GET_PLAYER(ctxt);
        // At this point the player should be within range of Scarecrow, so assign as cutscene actor.
        // Scarecrow should already have cutscene Id field set.
        player->ocarinaCutsceneActor = &(actor->base);
        if (player->stateFlags.state2 & PLAYER_STATE2_OCARINA) {
            return true;
        }
        return false;
    } else {
        // Default behavior.
        return ctxt->msgCtx.unk1202A == 0xD;
    }
}

/**
 * Hook function which checks if Scarecrow actor should spawn and begin actor cutscene.
 **/
bool Scarecrow_ShouldSpawn(ActorEnKakasi* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.freeScarecrow) {
        ActorPlayer *player = GET_PLAYER(ctxt);
        // Check if pulling out ocarina is complete (outside of typical actor cutscene).
        // Logic inferred from player_actor code. See (vanilla RDRAM): 0x8076FB2C
        bool condition1 = (player->unkAA5 == 4) || (player->unk241 != 0) || (player->frozenTimer != 0);
        bool condition2 = (player->frozenTimer != 0) && (ctxt->msgCtx.unk1202A == 4);
        if (condition1 && condition2) {
            return true;
        } else {
            return false;
        }
    } else {
        // This is checking an extra condition for Free Scarecrow, so always return true if disabled.
        return true;
    }
}
