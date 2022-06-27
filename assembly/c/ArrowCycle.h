#ifndef ARROW_CYCLE_H
#define ARROW_CYCLE_H

#include <z64.h>

Actor* ArrowCycle_FindArrow(ActorPlayer* player, GlobalContext* ctxt);
s8 ArrowCycle_GetMagicCost(u8 index);
void ArrowCycle_Handle(ActorPlayer* player, GlobalContext* ctxt);

#endif // ARROW_CYCLE_H
