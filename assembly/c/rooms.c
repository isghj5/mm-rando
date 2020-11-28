#include "models.h"
#include "z2.h"

void room_after_load(z2_game_t *game, z2_room_ctxt_t *room_ctxt, u32 room_idx) {
}

void room_after_unload(z2_game_t *game, z2_room_ctxt_t *room_ctxt) {
    // Prepare object data heap for previous room unload.
    models_prepare_after_room_unload(game);
}

void room_before_load(z2_game_t *game, z2_room_ctxt_t *room_ctxt, u32 room_idx) {
    // Prepare object data heap for new room load.
    models_prepare_before_room_load(room_ctxt, (s8)room_idx);
}

void room_before_unload(z2_game_t *game, z2_room_ctxt_t *room_ctxt) {
}
