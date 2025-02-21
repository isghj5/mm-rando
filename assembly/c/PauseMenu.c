#include <z64.h>
#include "HudColors.h"
#include "Misc.h"
#include "QuestItemStorage.h"
#include "QuestItems.h"
#include "Reloc.h"
#include "SaveFile.h"
#include "macro.h"
#include "controller.h"

static bool sHoldingStart = false;
bool PauseMenu_SetupUpdate_HasPressedStart(GlobalContext* ctxt) {
    Input* input = CONTROLLER1(ctxt);

    if (CHECK_BTN_ALL(input->pressEdge.buttons.value, BTN_START)) {
        return true;
    }

    if (MISC_CONFIG.flags.easyFrameByFrame) {
        if (CHECK_BTN_ALL(input->current.buttons.value, BTN_START)) {
            if (sHoldingStart) {
                sHoldingStart = false;
                return true;
            }
            sHoldingStart = true;
        } else {
            sHoldingStart = false;
        }
    }

    return false;
}
