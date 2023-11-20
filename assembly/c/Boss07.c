#include <z64.h>
#include "Player.h"
#include "GiantMask.h"

void Boss07_TriggerCredits(GlobalContext* ctxt) {
    if (Player_CheckVictory()) {
        ctxt->warpDestination = 0x5400;
        gSaveContext.extra.nextCutsceneIndex = 0xFFF7;
        ctxt->warpType = 20; // TRANS_TRIGGER_START
    } else {
        // eventDayCount = 0
        gSaveContext.perm.day = 0;
        gSaveContext.perm.time = 0x3FFF;
        GiantMask_MarkReset();
        if (gSaveContext.perm.mask == 0x14) {
            gSaveContext.perm.mask = 0;
        }
        z2_Sram_SaveSpecialNewDay(ctxt);
        u16 spawn = *(u16*)0x80145342; // maybe better to pass in as a config (for entrance rando future proofing)
        ctxt->warpDestination = spawn;
        ctxt->warpType = 20;
    }
}
