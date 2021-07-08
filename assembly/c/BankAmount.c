#include <stdbool.h>
#include <z64.h>
#include "Input.h"
#include "Misc.h"

bool BankAmount_BeforeHandleInput(GlobalContext* ctxt) {
    if (!MISC_CONFIG.speedups.fastBankRupees) {
        return false;
    }

    if (gPlayUpdateInput.pressEdge.buttons.r || gPlayUpdateInput.pressEdge.buttons.z) {
        u16 amount;
        
        if (gPlayUpdateInput.pressEdge.buttons.r) {
            if (ctxt->msgCtx.currentMessageId == 0x450) {
                // depositing
                amount = gSaveContext.perm.unk24.rupees;
            } else if (ctxt->msgCtx.currentMessageId == 0x46E) {
                // withdrawing
                u16 bankRupees = (u16)(gSaveContext.perm.bankRupees & 0xFFFF);
                if (gSaveContext.perm.isNight) {
                    bankRupees -= 4;
                }
                u16 capacity = gItemUpgradeCapacity.walletCapacity[gSaveContext.perm.inv.upgrades.wallet];
                amount = capacity - gSaveContext.perm.unk24.rupees;
                if (amount > bankRupees) {
                    amount = bankRupees;
                }
            }
        } else if (gPlayUpdateInput.pressEdge.buttons.z) {
            amount = 0;
        }
        
        if (amount > 999) {
            amount = 999;
        }

        if (amount != ctxt->msgCtx.bankRupeesSelected) {
            u8 characters[3] = {
                '0' + (amount / 100),
                '0' + (amount / 10) % 10,
                '0' + (amount % 10)
            };
            
            for (u8 i = 0; i < 3; i++) {
                u8 character = characters[i];
                ctxt->msgCtx.currentMessageDisplayed[ctxt->msgCtx.selectionStartIndex + i] = character;
                z2_Kanfont_LoadAsciiChar(ctxt, character, ctxt->msgCtx.unk120C4 + (i << 7));
            }
            z2_PlaySfx(0x482F);

            return true;
        }
    }

    return false;
}
