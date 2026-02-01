#include <z64.h>
#include "BaseRupee.h"
#include "Misc.h"

const static u16 sBaseGiIndex = 0x4ED;

u16 ObjYasi_GetGiIndex(Actor* actor, GlobalContext* ctxt) {
    if ((actor->params & 0x4000) || !(actor->params & 0x8000)) {
        return 0;
    }

    u16 treeId = actor->params & 0x7;
    u16 giIndex = sBaseGiIndex + treeId;

    return giIndex;
}

ActorEnItem00* ObjYasi_ItemSpawn(GlobalContext* ctxt, Vec3f* spawnPos, u16 type, Actor* actor) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, spawnPos, type);

    u16 giIndex = ObjYasi_GetGiIndex(actor, ctxt);

    if (giIndex > 0) {
        if (Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex) && MISC_CONFIG.drawFlags.freestanding) {
            z2_SetActorSize(&item->base, 0.015f);
            item->targetSize = 0.015;
            z2_SetShape(&item->base.shape, 750, (void*)0x800B3FC0, 6);
        }
    }

    return item;
}
