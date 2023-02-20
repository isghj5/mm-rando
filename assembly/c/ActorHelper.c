#include <z64.h>

/**
 * Helper function to check if a given actor pointer exists in the actor list.
 **/
bool ActorHelper_DoesActorExist(const Actor* target, const GlobalContext* ctxt, u8 actorCategory) {
    const Actor* actor = ctxt->actorCtx.actorList[actorCategory].first;
    // Iterate actor linked list for each entry.
    while (actor != NULL) {
        if (actor == target) {
            return true;
        }
        actor = actor->next;
    }
    return false;
}
