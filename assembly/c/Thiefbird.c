#include <z64.h>
#include "BaseRupee.h"

ActorEnItem00* Thiefbird_RupeeSpawn(GlobalContext* ctxt, Vec3f* position, u16 type) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, position, type);
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, 0x40D);
    return item;
}
