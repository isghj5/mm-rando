#include <stdbool.h>
#include "z2.h"

static bool g_floor_physics_override_type = false;
static u32 g_floor_physics_override_type_value = 0;

void floor_physics_override_type(bool enabled, u32 type) {
    g_floor_physics_override_type = enabled;
    g_floor_physics_override_type_value = type;
}

u32 floor_physics_get_override_type(void *arg0, void *arg1, u8 arg2) {
    if (g_floor_physics_override_type)
        return g_floor_physics_override_type_value;
    else
        return z2_GetFloorPhysicsType(arg0, arg1, arg2);
}
