#ifndef BUTTONS_H
#define BUTTONS_H

#include <stdbool.h>
#include <z64.h>

bool Buttons_CheckCItemUsable(GlobalContext* ctxt, u8 c);
void Buttons_CheckItemUsability(bool* dest, GlobalContext* ctxt, u8 b, u8 cLeft, u8 cDown, u8 cRight);

#endif // BUTTONS_H
