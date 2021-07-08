#include <stdbool.h>
#include <z64.h>
#include "Models.h"

#define LOADED_ACTOR_MODEL_SLOTS 8

struct LoadedActorModel {
    struct Model model;
    bool used;
    s16 id;
    u16 variable;
    void* extra;
};

static struct LoadedActorModel gLoadedActorModels[LOADED_ACTOR_MODEL_SLOTS];

static void ClearActorModel(struct LoadedActorModel* loaded) {
    loaded->used = false;
    loaded->id = 0;
    loaded->variable = 0;
    loaded->extra = NULL;
}

bool LoadedModels_GetActorModel(struct Model* model, void** extra, Actor* actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct LoadedActorModel loaded = gLoadedActorModels[i];
        if (loaded.used && loaded.id == actor->id && loaded.variable == actor->params) {
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

bool LoadedModels_AddActorModel(struct Model model, void* extra, Actor* actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct LoadedActorModel* loaded = &gLoadedActorModels[i];
        if (!loaded->used) {
            loaded->used = true;
            loaded->id = actor->id;
            loaded->variable = actor->params;
            loaded->model = model;
            loaded->extra = extra;
            return true;
        }
    }
    return false;
}

bool LoadedModels_ClearActorModels(void) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct LoadedActorModel* loaded = &gLoadedActorModels[i];
        ClearActorModel(loaded);
    }
}

void LoadedModels_RemoveActorModel(Actor* actor) {
    for (int i = 0; i < LOADED_ACTOR_MODEL_SLOTS; i++) {
        struct LoadedActorModel* loaded = &gLoadedActorModels[i];
        if (loaded->used && loaded->id == actor->id && loaded->variable == actor->params) {
            ClearActorModel(loaded);
        }
    }
}
