#include <z64.h>
#include "ActorExt.h"
#include "Models.h"
#include "TargetHealth.h"
#include "ItemDetector.h"

/**
 * Hook function called after an actor's deconstructor function has been called.
 **/
void Actor_AfterDtor(Actor* actor, GlobalContext* ctxt) {
    // Free any Actor Extended data this actor may point to.
    ActorExt_AfterActorDtor(actor);
    // Unload actor model information after dtor.
    Models_AfterActorDtor(actor);
}

void Actor_Init(Actor* actor, GlobalContext* ctxt) {
    actor->init(actor, ctxt);
    TargetHealth_AfterActorInit(actor, ctxt);
}

Actor* Actor_Spawn(GlobalContext* ctxt, u8 id, Vec3f pos, Vec3s rot, u16 params) {
    return z2_SpawnActor(&ctxt->actorCtx, ctxt, id, pos.x, pos.y, pos.z, rot.x, rot.y, rot.z, params);
}

void Actor_Update(Actor* actor, GlobalContext* ctxt) {
    actor->update(actor, ctxt);
    ItemDetector_AfterActorUpdate(actor, ctxt);
    if (actor->id == ACTOR_EN_HORSE) {
        if (actor->child != NULL && actor->child->id == ACTOR_PLAYER && actor->child->parent != actor && (actor->bgcheckFlags & 1)) { // BGCHECKFLAG_GROUND
            ActorPlayer* player = (ActorPlayer*)actor->child;
            if (!(player->stateFlags.state1 & PLAYER_STATE1_EPONA)) {
                s32 postDrawFunc = *(s32*)(((u8*)actor)+0x240);
                if (!postDrawFunc) {
                    s32* action = (s32*)(((u8*)actor)+0x144);
                    s32* animIndex = (s32*)(((u8*)actor)+0x20C);
                    s32* playerControlled = (s32*)(((u8*)actor)+0x208);
                    s32* stateFlags = (s32*)(((u8*)actor)+0x1E8);

                    actor->child = NULL;
                    *playerControlled = false;
                    *stateFlags &= ~(1 << 16); // ENHORSE_UNRIDEABLE
                    *action = 2; // ENHORSE_ACTION_IDLE
                    *animIndex = 1; // ENHORSE_ANIM_WHINNY
                    gSaveContext.perm.horseData.sceneId = ctxt->sceneNum;
                    gSaveContext.perm.horseData.pos.x = actor->currPosRot.pos.x;
                    gSaveContext.perm.horseData.pos.y = actor->currPosRot.pos.y;
                    gSaveContext.perm.horseData.pos.z = actor->currPosRot.pos.z;
                    gSaveContext.perm.horseData.yaw = actor->shape.rot.y;
                }
            }
        }
    }
}
