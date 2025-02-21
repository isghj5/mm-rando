#include <z64.h>
#include "Models.h"
#include "Kaleido_OverlayMenu.h"
#include "MMR.h"
#include "Dpad.h"
#include "Music.h"
#include "WorldColors.h"

// Whether or not the overlay menu is enabled.
static bool gEnable = true;

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

const static u16 respawnTextId = 0x9002;

static void CheckRespawn(GlobalContext* ctxt) {
    if (ctxt->pauseCtx.state != 6 || ctxt->pauseCtx.switchingScreen) {
        return;
    }

    InputPad curButtons = ctxt->state.input[0].current.buttons;

    switch (z2_GetMessageState(&ctxt->msgCtx)) {
        case 0: // TEXT_STATE_NONE
            if (curButtons.z && curButtons.r && curButtons.a && ctxt->state.input[0].pressEdge.buttons.s) {
                ctxt->state.input[0].pressEdge.buttons.s = 0;
                if (GET_PLAYER(ctxt)->stateFlags.state1 & PLAYER_STATE1_EPONA) {
                    z2_PlaySfx(0x4806); // NA_SE_SY_ERROR
                    return;
                } else {
                    z2_ShowMessage(ctxt, respawnTextId, NULL);
                }
            }
            break;
        case 4: // TEXT_STATE_CHOICE
            if (z2_MessageShouldAdvance(ctxt) && ctxt->msgCtx.currentMessageId == respawnTextId) {
                z2_MessageClose(ctxt);
                if (ctxt->msgCtx.selection == 0) {
                    z2_PlaySfxDecide();
                    u16 spawn = *(u16*)0x80145342; // maybe better to pass in as a config (for entrance rando future proofing)
                    ctxt->warpDestination = spawn;
                    ctxt->warpType = 1;
                } else {
                    z2_PlaySfxCancel();
                }
            }
            break;
    }
    if (ctxt->msgCtx.currentMessageId == respawnTextId) {
        ctxt->state.input[0].current.buttons.value = 0;
        ctxt->state.input[0].last.buttons.value = 0;
        ctxt->state.input[0].pressEdge.buttons.value = 0;
        ctxt->state.input[0].releaseEdge.buttons.value = 0;
        ctxt->state.input[0].current.xAxis = 0;
        ctxt->state.input[0].last.xAxis = 0;
        ctxt->state.input[0].pressEdge.xAxis = 0;
        ctxt->state.input[0].releaseEdge.xAxis = 0;
    }
}


static bool ShouldDraw(GlobalContext* ctxt) {
    return ctxt->pauseCtx.state == 6 &&
        ctxt->pauseCtx.switchingScreen == 0 &&
        (ctxt->pauseCtx.screenIndex == 0 || ctxt->pauseCtx.screenIndex == 3) &&
        (ctxt->state.input[0].current.buttons.l || ctxt->state.input[0].current.buttons.du);
}

/**
 * Whether or not the overlay menu should display.
 **/
void Game_ShouldDrawOverlayMenu(GlobalContext* ctxt) {
	if (gEnable && ShouldDraw(ctxt)) {
        // The seperate payload that holds this should be loaded by the time this is reached
        Kaleido_OverlayMenu_Draw(ctxt);
    }
}

/**
 * Hook function called after game processes next frame.
 **/
void Game_AfterUpdate(GlobalContext* ctxt) {
    Game_ShouldDrawOverlayMenu(ctxt);
    Music_Update(ctxt);
    if (Game_IsPlayerActor()) {
        MMR_ProcessItemQueue(ctxt);
        WorldColors_CycleTunic(ctxt);
    } else {
        // TODO Properly prevent inputs during respawn combo
        // TODO Fix HUD visibility settings after respawning
        // CheckRespawn(ctxt);
    }
}

void Game_DrawOverlay(GlobalContext* ctxt) {
    Dpad_Draw(ctxt);
    Music_Draw(ctxt);
}
