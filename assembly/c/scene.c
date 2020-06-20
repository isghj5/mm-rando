#include "actor_ext.h"
#include "loaded_models.h"
#include "models.h"
#include "z2.h"

/**
 * Hook function used after the scene initialize function has been called.
 **/
void scene_after_init(z2_game_t *game) {
    // Set all actor ext heap entries to clear
    actor_ext_clear();
    // Clear the models object heap
    models_clear_object_heap();
    // Clear loaded actor model info
    loaded_models_clear_actor_models();
}
