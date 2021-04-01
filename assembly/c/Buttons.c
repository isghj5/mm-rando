#include <stdbool.h>
#include <z64.h>

void Buttons_CheckItemUsability(bool* dest, GlobalContext* ctxt, u8 b, u8 cLeft, u8 cDown, u8 cRight) {
    u8 previous[4], prevstates[5];

    // Backup modified fields
    ButtonsState buttonsState = gSaveContext.extra.buttonsState;

    // Backup button items
    for (int i = 0; i < 4; i++)
        previous[i] = gSaveContext.perm.unk4C.formButtonItems[0].buttons[i];

    // Backup button states
    for (int i = 0; i < 5; i++)
        prevstates[i] = gSaveContext.extra.buttonsUsable[i];

    gSaveContext.perm.unk4C.formButtonItems[0].b      = b;
    gSaveContext.perm.unk4C.formButtonItems[0].cLeft  = cLeft;
    gSaveContext.perm.unk4C.formButtonItems[0].cDown  = cDown;
    gSaveContext.perm.unk4C.formButtonItems[0].cRight = cRight;

    z2_UpdateButtonUsability(ctxt);

    // Set dest to enabled-states (which are either 0x00 or 0xFF)
    for (int i = 0; i < 4; i++)
        dest[i] = (gSaveContext.extra.buttonsUsable[i] == 0);

    // Restore button items
    for (int i = 0; i < 4; i++)
        gSaveContext.perm.unk4C.formButtonItems[0].buttons[i] = previous[i];

    // Restore button states
    for (int i = 0; i < 5; i++)
        gSaveContext.extra.buttonsUsable[i] = prevstates[i];

    // Restore modified fields
    gSaveContext.extra.buttonsState = buttonsState;
}

bool Buttons_CheckCItemUsable(GlobalContext* ctxt, u8 c) {
    bool dest[4];
    Buttons_CheckItemUsability(dest, ctxt, 0xFF, c, 0xFF, 0xFF);
    return dest[1];
}
