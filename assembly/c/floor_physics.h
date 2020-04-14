#ifndef FLOOR_PHYSICS_H
#define FLOOR_PHYSICS_H

#include <stdbool.h>
#include "types.h"

void floor_physics_override_type(bool enabled, u32 type);
u32 floor_physics_get_override_type(void *arg0, void *arg1, u8 arg2);

#endif // FLOOR_PHYSICS_H
