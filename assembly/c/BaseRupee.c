#include <z64.h>
#include "MMR.h"
#include "Misc.h"

// TODO pick a definitely unused part of the actor memory
void Rupee_SetGiIndex(Actor* actor, u16 giIndex) {
    u16* pointer = (u16*)(&actor->unkE0);
    *pointer = giIndex;
}

u16 Rupee_GetGiIndex(Actor* actor) {
    u16* pointer = (u16*)(&actor->unkE0);
    return *pointer;
}

void Rupee_SetDrawGiIndex(Actor* actor, u16 drawGiIndex) {
    u16* pointer = (u16*)(&actor->unkE0);
    pointer++;
    *pointer = drawGiIndex;
}

u16 Rupee_GetDrawGiIndex(Actor* actor) {
    u16* pointer = (u16*)(&actor->unkE0);
    pointer++;
    return *pointer;
}

void Rupee_CheckAndSetGiIndex(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return;
    }
    
    GetItemEntry* entry = MMR_GetGiEntry(giIndex);
    if (entry->message != 0) {
        Rupee_SetGiIndex(actor, giIndex);
        u16 drawGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
        Rupee_SetDrawGiIndex(actor, drawGiIndex);
    }
}
