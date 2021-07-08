#include <z64.h>
#include "BaseRupee.h"
#include "MMR.h"

ActorEnItem00* MusicStaff_SpawnRupee(GlobalContext* ctxt, Vec3f* position, u16 type, u8 giIndexOffset) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, position, type);
    u16 giIndex = 0x3A4 + giIndexOffset;
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    return item;
}
