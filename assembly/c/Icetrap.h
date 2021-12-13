#ifndef ICETRAP_H
#define ICETRAP_H

#include <stdbool.h>
#include <z64.h>

bool Icetrap_Give(ActorPlayer* player, GlobalContext* ctxt);
void Icetrap_PushPending(u8 type);

#endif // ICETRAP_H
