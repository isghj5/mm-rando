#include <stdbool.h>
#include <z64.h>

void Hacks_DrawBButtonIconColorFix(GlobalContext* ctxt) {
    // Clear the Env color before drawing amounts/text.
    DispBuf* db = &ctxt->state.gfxCtx->overlay;
    gDPSetEnvColor(db->p++, 0x00, 0x00, 0x00, 0xFF);
    z2_DrawBButtonIcon(ctxt);
}

void Hacks_DrawCButtonIconsColorFix(GlobalContext* ctxt) {
    // Clear the Env color before drawing amounts/text.
    DispBuf* db = &ctxt->state.gfxCtx->overlay;
    gDPSetEnvColor(db->p++, 0x00, 0x00, 0x00, 0xFF);
    z2_DrawCButtonIcons(ctxt);
}
