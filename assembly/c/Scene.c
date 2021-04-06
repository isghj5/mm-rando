#include <z64.h>
#include "ActorExt.h"
#include "LoadedModels.h"
#include "Models.h"
#include "Item00.h"

/**
 * Hook function used after the scene initialize function has been called.
 **/
void Scene_AfterInit(GlobalContext* ctxt) {
    // Set all actor ext heap entries to clear
    ActorExt_Clear();
    // Clear the models object heap
    Models_ClearObjectHeap();
    // Clear loaded actor model info
    LoadedModels_ClearActorModels();
    // Load Collectable Table info for the scene
    Item00_LoadCollectableTable(ctxt);
}
