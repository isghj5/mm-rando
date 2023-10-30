#include <z64.h>
#include "BaseRupee.h"

const static u16 sBaseGiIndex = 0x4D1;

u16 Bufferfly_GetGiIndex(ActorEnButte* actor, GlobalContext* ctxt) {
    u16 giIndex = 0;

    switch (ctxt->sceneNum) {
        case SCENE_30GYOSON: // Great Bay Coast
            giIndex = sBaseGiIndex;
            break;
        case SCENE_KAKUSIANA: // Grottos
            switch (ctxt->roomContext.currRoom.num) {
                case 0: // Ocean Gossip Stones
                    giIndex = sBaseGiIndex + 1;
                    break;
                case 12: // Magic Bean Salesman
                    giIndex = sBaseGiIndex + 2;
                    break;
                case 10: // Cows
                    switch (gSaveContext.extra.unk87) {
                        case 0: // Termina Field Cow Grotto
                            giIndex = sBaseGiIndex + 3;
                            break;
                        case 2: // Great Bay Coast Cow Grotto
                            giIndex = sBaseGiIndex + 4;
                            break;
                    }
                    break;
            }
            break;
        case SCENE_10YUKIYAMANOMURA2: // Mountain Village (Spring)
            if (actor->base.initPosRot.pos.x > 0.0f) {
                giIndex = sBaseGiIndex + 5;
            } else {
                giIndex = sBaseGiIndex + 6;
            }
            break;
        case SCENE_00KEIKOKU: // Termina Field
            giIndex = sBaseGiIndex + 7;
            break;
    }

    return giIndex;
}

ActorEnElf* Butterfly_FairySpawn(ActorEnButte* actor, GlobalContext* ctxt, s16 actorId, f32 posX, f32 posY, f32 posZ, s16 rotX,
                   s16 rotY, s16 rotZ, s32 params) {
    ActorEnElf* fairy = (ActorEnElf*)z2_SpawnActor(&ctxt->actorCtx, ctxt, actorId, posX, posY, posZ, rotX, rotY, rotZ, params);

    u16 giIndex = Bufferfly_GetGiIndex(actor, ctxt);

    if (giIndex > 0) {
        Rupee_CheckAndSetGiIndex(&fairy->base, ctxt, giIndex);
    }

    return fairy;
}
