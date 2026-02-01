#include <z64.h>

// TODO maybe just call Player_808354A4 instead
void WarpTag_HandleExit(GlobalContext* ctxt) {
    if (ctxt->warpDestination == 0xFFFF) {
        gSaveContext.extra.voidFlag = 4;
        ctxt->warpDestination = gSaveContext.extra.respawn[RESPAWN_MODE_GROTTO_RETURN].entrance;
        ctxt->transitionType = 3;
        gSaveContext.extra.nextTransitionType = 3;
    } else {
        z2_Scene_SetExitFade(ctxt);
    }
}
