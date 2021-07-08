#include <stdbool.h>
#include <z64.h>
#include "Misc.h"
#include "Reloc.h"

// Function in Bg_Ingate used to update the actor's movement. It will also update the flags when its path is finished.
#define UpdateCanoeMovementVRAM 0x80953BEC
typedef void(*UpdateCanoeMovementFunc)(ActorBgIngate* actor);

/**
 * Helper function used to resolve and call function for processing boat movement along its path.
 **/
static void ProcessMovement(ActorBgIngate* actor) {
    ActorOverlay* entry = &gActorOverlayTable[0xA7];
    UpdateCanoeMovementFunc function = Reloc_ResolveActorOverlay(entry, UpdateCanoeMovementVRAM);
    if (function != NULL) {
        function(actor);
    }
}

/**
 * Hook function used to continue moving boat if needed during frame when processing boat archery end.
 **/
void BoatCruise_BeforeCruiseEnd(ActorBgIngate* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.speedups.boatArchery && actor->flags == 0x19) {
        ProcessMovement(actor);
    }
}

/**
 * Hook function used to get speed of boat during cruise or archery.
 **/
s16 BoatCruise_GetBoatSpeed(ActorBgIngate* actor, int mode) {
    if (mode == 0) {
        // Boat Cruise
        if (!MISC_CONFIG.speedups.boatArchery) {
            return 4;
        } else {
            return 8;
        }
    } else {
        // Boat Archery
        return 1;
    }
}

/**
 * Hook function used to continue moving boat if needed when "idling" after ending boat archery.
 **/
void BoatCruise_HandleIdle(ActorBgIngate* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.speedups.boatArchery && actor->flags == 0x19) {
        ProcessMovement(actor);
    }
}

/**
 * Hook function used to check if archery should be ended.
 *
 * If the speedup is enabled, checks if the player has enough points to end the game early.
 **/
bool BoatCruise_ShouldEndArchery(ActorBgIngate* actor, GlobalContext* ctxt) {
    bool finished = (actor->flags & 2) == 2;
    if (MISC_CONFIG.speedups.boatArchery) {
        return finished || (((gSaveContext.owl.eventInf[3] & 0x20) != 0) && (gSaveContext.extra.minigameCounter[0] >= 20));
    } else {
        return finished;
    }
}
