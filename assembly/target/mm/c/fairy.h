#ifndef FAIRY_H
#define FAIRY_H

#include <stdbool.h>
#include "z2.h"

bool can_interact_with_fairy(z2_game_t *game, z2_link_t *link);
bool get_next_fairy_instance(u16 *inst, z2_game_t *game);
void reset_fairy_instance_usage();
z2_actor_t* spawn_next_fairy_actor(z2_game_t *game, z2_xyzf_t pos);

#endif // FAIRY_H
