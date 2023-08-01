#include <z64.h>
#include "BaseRupee.h"

ActorEnItem00* KeatonGrassCluster_RupeeSpawn(GlobalContext* ctxt, Vec3f* position, u16 count) {
    u16 type = count == 8 ? 2 : 0;
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, position, type);
    u16 giIndex = 0;
    s8 multiplier = -1;
    switch (ctxt->sceneNum) {
        case SCENE_ROMANYMAE: // Milk Road
            multiplier = 0;
            break;
        case SCENE_BACKTOWN: // North Clock Town
            multiplier = 1;
            break;
        case SCENE_10YUKIYAMANOMURA2: // Mountain Village (Spring)
            multiplier = 2;
            break;
    }
    if (multiplier >= 0) {
        u16 giIndex = 0x41E + (multiplier * 9) + count;
        Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    }
    return item;
}
