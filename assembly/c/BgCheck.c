#include <z64.h>
#include "GiantMask.h"

s32 SurfaceType_IsBurrowable(CollisionContext* colCtx, BgPolygon* poly, s32 bgId) {
    if (poly == NULL || GiantMask_IsGiant()) {
        return false;
    }

    // Displaced code:
    u32 flags;

    if (z2_BgCheck_GetCollisionHeader(colCtx, bgId) == NULL) {
        return true;
    }
    flags = poly->vertB & 0x4000;
    return !!flags;
}
