#include <z64.h>
#include "Models.h"
#include "OverlayMenu.h"
#include "MMR.h"
#include "Music.h"

bool Game_IsPlayerActor(void) {
    return s801D0B70.selected == &s801D0B70.playerActor;
}

bool Game_IsKaleidoScope(void) {
    return s801D0B70.selected == &s801D0B70.kaleidoScope;
}

/**
 * Hook function called after preparing display buffers for writing during current frame.
 **/
void Game_AfterPrepareDisplayBuffers(GraphicsContext* gfx) {
    // Check if models objheap should finish advancing.
    Models_AfterPrepareDisplayBuffers(gfx);
}

/**
 * Hook function called after game processes next frame.
 **/
void Game_AfterUpdate(GlobalContext* ctxt) {
    OverlayMenu_Draw(ctxt);
    Music_Update(ctxt);
    if (Game_IsPlayerActor()) {
        MMR_ProcessItemQueue(ctxt);
    }
}
