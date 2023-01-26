#include <z64.h>
#include "Misc.h"

static f32 ClearSignBit(f32 f) {
    union {
        f32 f;
        u32 u;
    } raw = { .f = f };
    raw.u &= 0x7FFFFFFF;
    return raw.f;
}

bool Scarecrow_CheckSongFlag(ActorEnKakasi* actor, GlobalContext* ctxt) {
    return MISC_CONFIG.flags.freeScarecrow || gSaveContext.perm.weekEventReg.scarecrowSongSet;
}

/**
 * Hook function which checks if Scarecrow actor should "activate" and prepare for spawning.
 **/
bool Scarecrow_ShouldActivate(ActorEnKakasi* actor, GlobalContext* ctxt) {
    const bool playedScarecrowSong = ctxt->msgCtx.unk1202A == 0xD;
    if (MISC_CONFIG.flags.freeScarecrow) {
        ActorPlayer *player = GET_PLAYER(ctxt);
        const bool usingOcarina = player->stateFlags.state2 & PLAYER_STATE2_OCARINA;
        // Y limit 565.0: Allows Twin Islands from ground, disallows Great Bay ledge from ground.
        const bool inRangeY = ClearSignBit(actor->base.yDistanceFromLink) < 565.0;
        const bool isOnLand = z2_801242DC(ctxt) < 2;

        if (!playedScarecrowSong && inRangeY && isOnLand) {
            // At this point the player should be within range of Scarecrow, so assign as ocarina cutscene actor.
            // Scarecrow should already have cutscene Id field set.
            player->ocarinaCutsceneActor = &(actor->base);
        }

        return playedScarecrowSong || (usingOcarina && inRangeY && isOnLand);
    } else {
        return playedScarecrowSong;
    }
}

/**
 * Hook function which checks if Scarecrow actor should spawn and begin actor cutscene.
 **/
bool Scarecrow_ShouldSpawn(ActorEnKakasi* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.freeScarecrow) {
        ActorPlayer *player = GET_PLAYER(ctxt);
        if (player->ocarinaCutsceneActor == &(actor->base)) {
            // Check if pulling out ocarina is complete (outside of typical actor cutscene).
            // Logic inferred from player_actor code. See (vanilla RDRAM): 0x8076FB2C
            return (player->frozenTimer != 0) && (ctxt->msgCtx.unk1202A == 4);
        }
    }
    // This is checking an extra condition for Free Scarecrow, so always return true if disabled.
    return true;
}
