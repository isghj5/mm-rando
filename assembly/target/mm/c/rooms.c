#include "models.h"
#include "z2.h"

void room_after_load(z2_game_t *game, z2_room_ctxt_t *room_ctxt, u32 room_idx) {
    // Clear the models object heap
    models_clear_object_heap();
}

void room_after_unload(z2_game_t *game, z2_room_ctxt_t *room_ctxt) {
}

void room_before_load(z2_game_t *game, z2_room_ctxt_t *room_ctxt, u32 room_idx) {
}

void room_before_unload(z2_game_t *game, z2_room_ctxt_t *room_ctxt) {
}
