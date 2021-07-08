#include <z64.h>
#include "ExtendedObjects.h"

ObjectFileTableEntry EXT_OBJECTS[EXT_OBJECT_COUNT] = { 0 };

/**
 * Helper function to get object file info given an index, regardless of whether it is in the
 * normal object table or the extended object table.
 **/
ObjectFileTableEntry* ExtendedObjects_Get(s16 index) {
    if (index < 0x283) {
        return &gObjectFileTable[index];
    } else {
        return &EXT_OBJECTS[index - 0x282];
    }
}
