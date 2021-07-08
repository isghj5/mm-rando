#include <z64.h>
#include "Misc.h"

/**
 * Hook function called to check whether or not to activate the beach cutscene when pushing Mikau to shore.
 **/
bool Mikau_ShouldActivateBeachCutscene(Actor* mikau, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.mikauEarlyBeach) {
        f32 difference = mikau->currPosRot.pos.y - mikau->floorHeight;
        return difference < 24.0;
    } else {
        // Vanilla: Check flag which is set when floor height is greater-than-or-equal to Y position.
        return mikau->bgcheckFlags & 1;
    }
}
