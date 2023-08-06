#include <z64.h>

void Arrow_AfterDraw(GlobalContext* ctxt, ActorEnArrow* this, EnArrowUnkStruct* arg2) {
    // Displaced code:
    z2_Matrix_MultVec3f(&arg2->unk_48, &this->unk_234);
    // End Displaced code

    if (this->base.child != NULL && this->base.child->id == ACTOR_EN_BOM) {
        ActorEnBom* bomb = (ActorEnBom*)this->base.child;
        if (bomb->timer != 0 && bomb->base.params == 0) {
            Vec3f tip;
            Vec3f temp = { 64.0f, -64.0f, 1000.0f };
            z2_Matrix_MultVec3f(&temp, &tip);
            f32 yDiff = tip.y - bomb->base.currPosRot.pos.y;
            z2_Math_Vec3f_Copy(&bomb->base.currPosRot.pos, &tip);
            f32 projectedWaterSurfaceDist = bomb->base.waterSurfaceDist - yDiff;

            if (projectedWaterSurfaceDist >= 20.0f) { // bomb will be underwater
                this->collider.body.toucher.dmgFlags = 0x20; // restore arrow damage to normal
                bomb->base.parent = NULL;
                this->base.child = NULL;
                z2_SetActorSize(&bomb->base, 0.01);
            }
        }
    }
}

bool Arrow_OnHit(GlobalContext* ctxt, ActorEnArrow* this, bool hitActor) {
    // Displaced code:
    z2_EffectSsHitmark_SpawnCustomScale(ctxt, 0, 150, &this->base.currPosRot.pos);
    // End Displaced code

    if (this->unk_262 || (hitActor && this->collider.body.atHitInfo->elemType != 4)) { // not a ghost hit
        if (this->base.child != NULL && this->base.child->id == ACTOR_EN_BOM) {
            ActorEnBom* bomb = (ActorEnBom*)this->base.child;
            if (hitActor) {
                z2_Math_Vec3s_ToVec3f(&bomb->base.currPosRot.pos, &this->collider.body.bumper.hitPos);
            } else {
                z2_Math_Vec3f_Copy(&bomb->base.currPosRot.pos, &this->base.currPosRot.pos);
            }

            bomb->base.parent = NULL;
            z2_ActorUnload(&this->base);

            bomb->timer = 0;

            // Let the bomb do the damage
            this->unk_262 = 0;
            return false;
        }
    }

    return hitActor;
}
