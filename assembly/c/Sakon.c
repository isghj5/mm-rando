#include <stdbool.h>
#include <z64.h>
#include "Misc.h"

/**
 * Hook function used to check whether or not the Sakon escape sequence should end.
 **/
bool Sakon_ShouldEndThiefEscape(ActorEnSuttari* sakon, GlobalContext* ctxt) {
    // Check if Sakon has dropped the luggage (with speedup) or has finished the escape sequence.
    return ((MISC_CONFIG.speedups.blastMaskThief && sakon->runningState == 6)
        || sakon->escapeStatus == -0x63);
}
