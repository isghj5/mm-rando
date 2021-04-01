#ifndef ACTOR_EXT_H
#define ACTOR_EXT_H

#include <stdbool.h>
#include <z64.h>

enum ActorExtFlag {
    ACTOR_EXT_USED = 0x80000000,
};

struct ActorExt {
    u32 flags;
    Color color;
    u8 padding;
};

void ActorExt_AfterActorDtor(Actor* actor);
void ActorExt_Clear(void);
struct ActorExt* ActorExt_FindFree(void);
void* ActorExt_GenericDecode(const Actor* actor);
void ActorExt_GenericEncode(Actor* actor, const void* ext);
void ActorExt_Init(void);
void ActorExt_SetFree(struct ActorExt* ext);
struct ActorExt* ActorExt_Setup(Actor* actor, bool* created);

#endif // ACTOR_EXT_H
