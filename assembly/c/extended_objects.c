#include "extended_objects.h"
#include "z2.h"

z2_obj_file_t EXT_OBJECTS[EXT_OBJECT_COUNT] = { 0 };

/**
 * Helper function to get object file info given an index, regardless of whether it is in the
 * normal object table or the extended object table.
 **/
z2_obj_file_t * extended_objects_get(s16 index) {
    if (index < 0x283) {
        return &z2_obj_table[index];
    } else {
        return &EXT_OBJECTS[index - 0x282];
    }
}
