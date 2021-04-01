#include <z64.h>
#include "MMR.h"

bool SpinAttack_ShouldSpinMainRun(Actor* actor, GlobalContext* ctxt) {
    if (MMR_GetProcessingItemGiIndex(ctxt)) {
        return false;
    }
    return true;
}