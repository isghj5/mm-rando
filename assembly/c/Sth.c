#include <z64.h>

void Sth_StartRewardTextbox(GlobalContext* ctxt) {
    u16 textId = 0x1135;
    if (z2_Inventory_GetSkullTokenCount(ctxt->sceneNum) >= 30) {
        textId = 0x1136;
    }
    z2_Message_ContinueTextbox(ctxt, textId);
}
