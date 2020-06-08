#ifndef LOADED_MODELS_H
#define LOADED_MODELS_H

#include <stdbool.h>
#include "mmr.h"
#include "models.h"
#include "z2.h"

bool loaded_models_add_actor_model(struct model model, void *extra, z2_actor_t *actor);
bool loaded_models_clear_actor_models(void);
bool loaded_models_get_actor_model(struct model *model, void **extra, z2_actor_t *actor);
void loaded_models_remove_actor_model(z2_actor_t *actor);

#endif // LOADED_MODELS_H
