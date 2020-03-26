#ifndef MODELS_H
#define MODELS_H

#include "z2.h"

struct model {
    u16 object_id;
    u8 graphic_id;
};

void models_after_actor_dtor(z2_actor_t *actor);
void models_clear_object_heap(void);
void models_init(void);

#endif // MODELS_H
