#include <stdbool.h>
#include <z64.h>

static bool gIsOverride = false;
static u32 gOverrideType = 0;

void FloorPhysics_OverrideType(bool enabled, u32 type) {
    gIsOverride = enabled;
    gOverrideType = type;
}

u32 FloorPhysics_GetOverrideType(void* arg0, void* arg1, u8 arg2) {
    if (gIsOverride) {
        return gOverrideType;
    } else {
        return z2_GetFloorPhysicsType(arg0, arg1, arg2);
    }
}
