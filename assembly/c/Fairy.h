#ifndef FAIRY_H
#define FAIRY_H

#include <stdbool.h>
#include <z64.h>

bool Fairy_CanInteractWith(GlobalContext* ctxt, ActorPlayer* player);
bool Fairy_GetNextInstance(u16* inst, GlobalContext* ctxt);
void Fairy_ResetInstanceUsage(void);
Actor* Fairy_SpawnNextActor(GlobalContext* ctxt, Vec3f pos);

#endif // FAIRY_H
