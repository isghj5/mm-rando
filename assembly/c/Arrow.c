#include <z64.h>

void Arrow_AfterDraw(GlobalContext* ctxt, ActorEnArrow* this, EnArrowUnkStruct* arg2) {
    // Displaced code:
    z2_Matrix_MultVec3f(&arg2->unk_48, &this->unk_234);
    // End Displaced code

    if (this->base.child != NULL && this->base.child->id == ACTOR_EN_BOM) {
        ActorEnBom* bomb = (ActorEnBom*)this->base.child;
        Vec3f tip;
        Vec3f temp = { 64.0f, -64.0f, 1000.0f };
        z2_Matrix_MultVec3f(&temp, &tip);
        z2_Math_Vec3f_Copy(&bomb->base.currPosRot.pos, &tip);
    }
}
