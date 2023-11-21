#include <z64.h>
#include "BaseRupee.h"
#include "MMR.h"
#include "Misc.h"

// TODO maybe find a better way to force an item to spawn.
extern bool gShouldForceItemSpawn;

u16 SoftSoilPrize_GetGiIndex(GlobalContext* ctxt, Actor* actor) {
    u16 giIndex = 0;
    if (!MISC_CONFIG.internal.vanillaLayout) {
        u8 flag = actor->params & 0x7F;
        switch (ctxt->sceneNum) {
            case SCENE_KAKUSIANA: // Grottos
                giIndex = 0x35E; // Bean Grotto
                break;
            case SCENE_KINSTA1: // Swamp Spider House
                if (flag == 0) {
                    giIndex = 0x37A; // Rock
                } else {
                    giIndex = 0x379; // Gold Room
                }
                break;
            case SCENE_22DEKUCITY: // Deku Palace
                giIndex = 0x36C;
                break;
            case SCENE_00KEIKOKU: // Termina Field
                if (flag == 0xE) {
                    giIndex = 0x37B; // Stump
                } else if (flag == 0x5) {
                    giIndex = 0x37C; // Observatory
                } else if (flag == 0x14) {
                    giIndex = 0x37D; // South Wall
                } else {
                    giIndex = 0x37E; // Pillar
                }
                break;
            case SCENE_F01: // Romani Ranch
                if (gSaveContext.perm.day <= 1) {
                    giIndex = 0x376;
                } else {
                    giIndex = 0x370;
                }
                break;
            case SCENE_30GYOSON: // Great Bay Coast
                giIndex = 0x36E;
                break;
            case SCENE_F01_B: // Doggy Racetrack
                giIndex = 0x36D;
                break;
            case SCENE_F41: // Stone Tower (Inverted)
                if (flag == 0) {
                    giIndex = 0x377; // Lower
                } else {
                    giIndex = 0x378; // Upper
                }
                break;
            case SCENE_RANDOM: // Secret Shrine
                giIndex = 0x36F;
                break;
        }
    }
    return giIndex;
}

ActorEnItem00* SoftSoilPrize_ItemSpawn(GlobalContext* ctxt, Actor* actor, u16 type) {
    u16 giIndex = SoftSoilPrize_GetGiIndex(ctxt, actor);

    if (giIndex > 0) {
        // TODO move somewhere common
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->message != 0) {
            // is randomized
            gShouldForceItemSpawn = true;
        }
    }

    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, &actor->currPosRot.pos, type);
    gShouldForceItemSpawn = false;
    if (item == NULL) {
        return item;
    }
    if (giIndex > 0) {
        Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
        if (MISC_CONFIG.drawFlags.freestanding) {
            z2_SetActorSize(&item->base, 0.015);
            item->targetSize = 0.015;
            z2_SetShape(&item->base.shape, 750, (void*)0x800B3FC0, 6);
        }
    }
    return item;
}
