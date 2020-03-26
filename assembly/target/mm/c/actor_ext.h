#ifndef ACTOR_EXT_H
#define ACTOR_EXT_H

#include <stdbool.h>
#include "color.h"
#include "z2.h"

enum actor_ext_flag {
    ACTOR_EXT_USED = 0x80000000,
};

struct actor_ext {
    u32 flags;
    rgb_t color;
    u8 padding;
};

void actor_ext_after_actor_dtor(z2_actor_t *actor);
void actor_ext_clear(void);
struct actor_ext * actor_ext_find_free(void);
struct actor_ext * actor_ext_generic_decode(const z2_actor_t *actor);
void actor_ext_generic_encode(z2_actor_t *actor, const void *ext);
void actor_ext_init(void);
void actor_ext_set_free(struct actor_ext *ext);
struct actor_ext * actor_ext_setup(z2_actor_t *actor, bool *created);

#endif // ACTOR_EXT_H
