#include <stdbool.h>
#include "mmr.h"
#include "models.h"
#include "z2.h"

#define LOADED_ACTOR_MODEL_SLOTS 8

struct loaded_actor_model {
    struct model model;
    bool used;
    s16 id;
    u16 variable;
    void *extra;
};

static struct loaded_actor_model g_loaded_actor_models[LOADED_ACTOR_MODEL_SLOTS];

bool loaded_models_get_actor_model(struct model *model, void **extra, z2_actor_t *actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct loaded_actor_model loaded = g_loaded_actor_models[i];
        if (loaded.used && loaded.id == actor->id && loaded.variable == actor->variable) {
            if (model != NULL) {
                *model = loaded.model;
            }
            if (extra != NULL) {
                *extra = loaded.extra;
            }
            return true;
        }
    }

    return false;
}

bool loaded_models_add_actor_model(struct model model, void *extra, z2_actor_t *actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct loaded_actor_model *loaded = &g_loaded_actor_models[i];
        if (!loaded->used) {
            loaded->used = true;
            loaded->id = actor->id;
            loaded->variable = actor->variable;
            loaded->model = model;
            loaded->extra = extra;
            return true;
        }
    }

    return false;
}

static void loaded_models_clear_actor_model(struct loaded_actor_model *loaded) {
    loaded->used = false;
    loaded->id = 0;
    loaded->variable = 0;
    loaded->extra = NULL;
}

bool loaded_models_clear_actor_models(void) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct loaded_actor_model *loaded = &g_loaded_actor_models[i];
        loaded_models_clear_actor_model(loaded);
    }
}

void loaded_models_remove_actor_model(z2_actor_t *actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct loaded_actor_model *loaded = &g_loaded_actor_models[i];
        if (loaded->used && loaded->id == actor->id && loaded->variable == actor->variable) {
            loaded_models_clear_actor_model(loaded);
        }
    }
}
