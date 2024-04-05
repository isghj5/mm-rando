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
        ActorEnHorse* horse = (ActorEnHorse*)actor;
        if (horse->base.child != NULL && horse->base.child->id == ACTOR_PLAYER && horse->base.child->parent != horse && (horse->base.bgcheckFlags & 1)) { // BGCHECKFLAG_GROUND
            ActorPlayer* player = (ActorPlayer*)horse->base.child;
            if (!(player->stateFlags.state1 & PLAYER_STATE1_EPONA)) {
                if (!horse->postDrawFunc) {
                    horse->base.child = NULL;
                    horse->playerControlled = false;
                    horse->stateFlags &= ~ENHORSE_UNRIDEABLE;
                    horse->action = ENHORSE_ACTION_IDLE;
                    horse->animIndex = ENHORSE_ANIM_WHINNY;
                    horse->colliderCylinder1.base.ocFlags1 |= OC1_ON;
                    horse->colliderCylinder2.base.ocFlags1 |= OC1_ON;
                    horse->colliderJntSph.base.ocFlags1 |= OC1_ON;
                    gSaveContext.perm.horseData.sceneId = ctxt->sceneNum;
                    gSaveContext.perm.horseData.pos.x = horse->base.currPosRot.pos.x;
                    gSaveContext.perm.horseData.pos.y = horse->base.currPosRot.pos.y;
                    gSaveContext.perm.horseData.pos.z = horse->base.currPosRot.pos.z;
                    gSaveContext.perm.horseData.yaw = horse->base.shape.rot.y;
                }
            }
        }
    }
}
