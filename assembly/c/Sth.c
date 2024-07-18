#include <z64.h>
#include "Misc.h"

void Sth_StartRewardTextbox(GlobalContext* ctxt) {
    u16 textId = 0x1135;
    if (z2_Inventory_GetSkullTokenCount(ctxt->sceneNum) >= 30) {
        textId = 0x1136;
    }
    z2_Message_ContinueTextbox(ctxt, textId);
}

bool Sth_ShouldSpawn(GlobalContext* ctxt) {
    return z2_Inventory_GetSkullTokenCount(ctxt->sceneNum) >= 30
        || (MISC_CONFIG.flags.oceanTokensRandomized && z2_get_generic_flag(ctxt, 0x19));
}
