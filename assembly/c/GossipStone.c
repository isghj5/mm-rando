#include <z64.h>
#include "BaseRupee.h"

const static u16 sBaseGiIndex = 0x4A4;

u16 GossipStone_GetGiIndex(ActorEnGs* actor, GlobalContext* ctxt) {
    u16 giIndex = 0;
    switch (ctxt->sceneNum) {
        case SCENE_F01C: // Cucco Shack
            giIndex = sBaseGiIndex;
            break;
        case SCENE_F01_B: // Doggy Racetrack
            giIndex = sBaseGiIndex + 1;
            break;
        case SCENE_30GYOSON: // Great Bay Coast
            giIndex = sBaseGiIndex + 2;
            break;
        case SCENE_KAKUSIANA: // Grottos
            giIndex = sBaseGiIndex + 3;
            break;
        case SCENE_IKANA: // Ikana Canyon
            giIndex = sBaseGiIndex + 4 + actor->switchFlag - 0x4A;
            break;
        case SCENE_ROMANYMAE: // Milk Road
            giIndex = sBaseGiIndex + 7;
            break;
        case SCENE_10YUKIYAMANOMURA2: // Mountain Village (Spring)
            giIndex = sBaseGiIndex + 8 + actor->switchFlag - 0x5E;
            break;
        case SCENE_13HUBUKINOMITI: // Path to Mountain Village
            giIndex = sBaseGiIndex + 10;
            break;
        case SCENE_IKANAMAE: // Road to Ikana
            giIndex = sBaseGiIndex + 11;
            break;
        case SCENE_24KEMONOMITI: // Road to Southern Swamp
            giIndex = sBaseGiIndex + 12;
            break;
        case SCENE_F01: // Romani Ranch
            giIndex = sBaseGiIndex + 13 + actor->switchFlag - 0x5D;
            break;
        case SCENE_20SICHITAI: // Southern Swamp (Poison)
        case SCENE_20SICHITAI2: // Southern Swamp (Clear)
            giIndex = sBaseGiIndex + 16;
            break;
        case SCENE_KINSTA1: // Swamp Spider House
            giIndex = sBaseGiIndex + 17;
            break;
        case SCENE_00KEIKOKU: // Termina Field
            giIndex = sBaseGiIndex + 18 + actor->switchFlag - 0x5A;
            break;
        case SCENE_LAST_DEKU: // Deku Trial
            giIndex = sBaseGiIndex + 24 + actor->switchFlag - 2;
            break;
        case SCENE_LAST_GORON: // Goron Trial
            giIndex = sBaseGiIndex + 29 + actor->switchFlag - 2;
            break;
        case SCENE_LAST_LINK: // Link Trial
            giIndex = sBaseGiIndex + 34 + actor->switchFlag - 2;
            break;
        case SCENE_LAST_ZORA: // Zora Trial
            giIndex = sBaseGiIndex + 39 + actor->switchFlag - 2;
            break;
        case SCENE_31MISAKI: // Zora Cape
            giIndex = sBaseGiIndex + 44;
            break;
    }
    return giIndex;
}

ActorEnElf* GossipStone_FairySpawn(ActorEnGs* actor, GlobalContext* ctxt, s16 actorId, f32 posX, f32 posY, f32 posZ, s16 rotX,
                   s16 rotY, s16 rotZ, s32 params) {
    ActorEnElf* fairy = (ActorEnElf*)z2_SpawnActor(&ctxt->actorCtx, ctxt, actorId, posX, posY, posZ, rotX, rotY, rotZ, params);
    u16 giIndex = GossipStone_GetGiIndex(actor, ctxt);
    if (giIndex > 0) {
        Rupee_CheckAndSetGiIndex(&fairy->base, ctxt, giIndex);
    }
    return fairy;
}
