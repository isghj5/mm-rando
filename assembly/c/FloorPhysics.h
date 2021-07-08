#ifndef FLOOR_PHYSICS_H
#define FLOOR_PHYSICS_H

#include <stdbool.h>
#include <z64.h>

void FloorPhysics_OverrideType(bool enabled, u32 type);
u32 FloorPhysics_GetOverrideType(void* arg0, void* arg1, u8 arg2);

#endif // FLOOR_PHYSICS_H
