#include <z64.h>

/**
 * Helper function to check if a given actor pointer exists in the actor list.
 **/
bool ActorHelper_DoesActorExist(const Actor* target, const GlobalContext* ctxt) {
    // Iterate actor list entries.
    for (int i = 0; i < ACTOR_LIST_ENTRIES; i++) {
        const s32 count = ctxt->actorCtx.actorList[i].length;
        const Actor* cur = ctxt->actorCtx.actorList[i].first;
        // Iterate actor linked list for each entry.
        for (s32 j = 0; j < count; j++, cur = cur->next) {
            if (cur == target) {
                return true;
            }
        }
    }
    return false;
}
