#ifndef ACTOR_H
#define ACTOR_H

#include "z2.h"

void actor_after_dtor(z2_actor_t *actor, z2_game_t *game);
z2_actor_t* actor_spawn(z2_game_t *game, u8 id, z2_xyzf_t pos, z2_rot_t rot, u16 var);

#endif // ACTOR_H
