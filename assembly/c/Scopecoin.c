#include <z64.h>
#include "BaseRupee.h"
#include "MMR.h"

u16 Scopecoin_GetGiIndex(Actor* actor) {
    s16 flag = (actor->params & 0x7F0) >> 4;
    u16 giIndex = 0;
    if (flag == 0xA) {
        // Red Rupees in tree
        giIndex = 0x360;
    } else if (flag == 0x2) {
        // Red Rupee in pillar
        giIndex = 0x361;
    }
    
    if (giIndex > 0) {
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->message == 0) {
            giIndex = 0;
        }
    }
    return giIndex;
}

ActorEnItem00* Scopecoin_RupeeSpawn(GlobalContext* ctxt, Actor* actor, u16 type) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, &actor->currPosRot.pos, type);
    u16 giIndex = Scopecoin_GetGiIndex(actor);
    if (giIndex > 0) {
        Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    }
    return item;
}
