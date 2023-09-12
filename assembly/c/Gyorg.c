#include <z64.h>
#include "GiantMask.h"

bool Gyorg_ShouldStopCatchingPlayer(ActorBoss03* gyorg) {
    return gyorg->workTimer[0] == 0 // WORK_TIMER_CURRENT_ACTION
        || GiantMask_IsGiant();
}
