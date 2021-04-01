#ifndef ACTOR_H
#define ACTOR_H

#include <z64.h>

void Actor_AfterDtor(Actor* actor, GlobalContext *ctxt);
Actor* Actor_Spawn(GlobalContext* ctxt, u8 id, Vec3f pos, Vec3s rot, u16 params);

#endif // ACTOR_H
