#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "Reloc.h"

// TODO: Replace with Misc flag(s).
static bool gGiveBothItems = true;

inline bool SwampReward1Flag() {
    return gSaveContext.perm.weekEventReg.bytes[59] & 0x10;
}

inline bool SwampReward2Flag() {
    return gSaveContext.perm.weekEventReg.bytes[32] & 2;
}

inline bool TownReward1Flag() {
    return gSaveContext.perm.weekEventReg.bytes[59] & 0x20;
}

inline bool TownReward2Flag() {
    return gSaveContext.perm.weekEventReg.bytes[32] & 4;
}

static inline bool ShouldGiveSwampReward2() {
    // NOTE: Assumes swamp archery double reward feature is enabled.
    if (MISC_CONFIG.internal.vanillaLayout) {
        return !SwampReward1Flag();
    } else {
        return !SwampReward1Flag() || !MMR_IsRecoveryHeart(0x24);
    }
}

static inline bool ShouldGiveTownReward2() {
    // NOTE: Assumes town archery double reward feature is enabled.
    if (MISC_CONFIG.internal.vanillaLayout) {
        return !TownReward1Flag();
    } else {
        return !TownReward1Flag() || !MMR_IsRecoveryHeart(0x23);
    }
}

/**
 * Action function used to handle giving second swamp archery reward.
 **/
static void SyatekiMan_Swamp_HandleGiveSecondItem(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    if (z2_800B84D0(&actor->base, ctxt)) {
        z2_ShowMessage(ctxt, 0xA34, &actor->base);
        actor->recentMessageId = 0xA34;
        actor->actionFunc = Reloc_ResolveActorOverlay(&gActorOverlayTable[ACTOR_EN_SYATEKI_MAN], 0x809C6E30);
        // Reset score so that we fallback to lesser reward next run.
        actor->currentScore = 0;
    } else {
        z2_800B85E0(&actor->base, ctxt, 500.0f, -1);
    }
}

/**
 * Hook function to determine the next action function after giving a swamp archery reward.
 **/
void* SyatekiMan_Swamp_DetermineActionFunctionAfterGiveItem(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    // Checks if we got the greater reward and don't have the flag for the lesser reward.
    if (gGiveBothItems && actor->currentScore >= 0x884 && ShouldGiveSwampReward2()) {
        return SyatekiMan_Swamp_HandleGiveSecondItem;
    } else {
        // Set default function pointer.
        return Reloc_ResolveActorOverlay(&gActorOverlayTable[ACTOR_EN_SYATEKI_MAN], 0x809C7C14);
    }
}

/**
 * Whether or not the quiver reward should be given.
 **/
static inline bool SyatekiMan_Swamp_ShouldGiveQuiverReward(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    if (gGiveBothItems) {
        // Enforce low score check for giving quiver reward.
        return !SwampReward1Flag() && actor->currentScore < 0x884;
    } else {
        // Default behavior, only give quiver reward if lesser reward flag not set.
        return !SwampReward1Flag();
    }
}

/**
 * Hook function to check if the quiver reward should be skipped in conditionals.
 **/
bool SyatekiMan_Swamp_ShouldNotGiveQuiverReward(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    return !SyatekiMan_Swamp_ShouldGiveQuiverReward(actor, ctxt);
}

/**
 * Action function used to handle giving second town archery reward.
 **/
static void SyatekiMan_Town_HandleGiveSecondItem(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    if (z2_800B84D0(&actor->base, ctxt)) {
        z2_ShowMessage(ctxt, 0x407, &actor->base);
        actor->recentMessageId = 0x407;
        actor->actionFunc = Reloc_ResolveActorOverlay(&gActorOverlayTable[ACTOR_EN_SYATEKI_MAN], 0x809C7990);
    } else {
        z2_800B85E0(&actor->base, ctxt, 500.0f, -1);
    }
}

/**
 * Hook function to determine the next action function after giving a town archery reward.
 **/
void* SyatekiMan_Town_DetermineActionFunctionAfterGiveItem(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    // Checks if we got the greater reward and don't have the flag for the lesser reward.
    if (gGiveBothItems && actor->recentMessageId != 0x407 && ShouldGiveTownReward2()) {
        return SyatekiMan_Town_HandleGiveSecondItem;
    } else {
        // Set default function pointer.
        return Reloc_ResolveActorOverlay(&gActorOverlayTable[ACTOR_EN_SYATEKI_MAN], 0x809C7EB4);
    }
}

/**
 * Hook function to check if the greater reward should be given for a perfect score.
 **/
bool SyatekiMan_Town_ShouldGiveGreaterReward(ActorEnSyatekiMan* actor, GlobalContext* ctxt) {
    if (gGiveBothItems) {
        // Always give greater reward if feature enabled.
        return true;
    } else {
        // Default behavior, only give greater reward if lesser reward has been received.
        return TownReward1Flag();
    }
}
