#ifndef MODELS_H
#define MODELS_H

#include "z2.h"

struct model {
    u16 object_id;
    u8 graphic_id;
};

void models_after_actor_dtor(z2_actor_t *actor);
void models_after_prepare_display_buffers(z2_gfx_t *gfx);
void models_clear_object_heap(void);
void models_init(void);
void models_prepare_after_room_unload(z2_game_t *game);
void models_prepare_before_room_load(z2_room_ctxt_t *room_ctxt, s8 room_index);

#endif // MODELS_H
