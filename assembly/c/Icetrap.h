#ifndef ICETRAP_H
#define ICETRAP_H

#include <stdbool.h>
#include <z64.h>

bool Icetrap_Give(ActorPlayer* player, GlobalContext* ctxt);
bool Icetrap_IsPending(void);
void Icetrap_PushPending(void);

#endif // ICETRAP_H
