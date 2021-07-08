#ifndef BASE_RUPEE_H
#define BASE_RUPEE_H

#include <z64.h>

void Rupee_CheckAndSetGiIndex(Actor* actor, GlobalContext* ctxt, u16 giIndex);
u16 Rupee_GetDrawGiIndex(Actor* actor);
u16 Rupee_GetGiIndex(Actor* actor);
void Rupee_SetDrawGiIndex(Actor* actor, u16 drawGiIndex);
void Rupee_SetGiIndex(Actor* actor, u16 giIndex);

#endif // BASE_RUPEE_H
