#include <z64.h>
#include "BaseRupee.h"

u16 KeatonGrassCluster_GetGiIndex(GlobalContext* ctxt, u16 count) {
    s8 multiplier = -1;
    switch (ctxt->sceneNum) {
        case 0x22: // Milk Road
            multiplier = 0;
            break;
        case 0x6E: // North Clock Town
            multiplier = 1;
            break;
        case 0x5A: // Mountain Village (Spring)
            multiplier = 2;
            break;
    }
    if (multiplier >= 0) {
        return 0x41E + (multiplier * 9) + count;
    }
    return 0;
}

ActorEnItem00* KeatonGrassCluster_RupeeSpawn(GlobalContext* ctxt, Vec3f* position, u16 count) {
    u16 type = count == 8 ? 2 : 0;
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, position, type);
    u16 giIndex = KeatonGrassCluster_GetGiIndex(ctxt, count);
    if (giIndex > 0) {
        Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    }
    return item;
}
