#include <stdbool.h>
#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "Player.h"
#include "BaseRupee.h"
#include "macro.h"

bool gShouldForceItemSpawn = false;

void Item00_Constructor(ActorEnItem00* actor, GlobalContext* ctxt) {
    if (actor->collectableFlag != 0) {
        u16 giIndex = Rupee_CollectableFlagToGiIndex(actor->collectableFlag);
        if (giIndex > 0) {
            Rupee_SetGiIndex(&actor->base, giIndex);
            u16 drawGiIndex = MMR_GetNewGiIndex(ctxt, 0, giIndex, false);
            Rupee_SetDrawGiIndex(&actor->base, drawGiIndex);
        }
    }
}

bool Item00_GiveItem(ActorEnItem00* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetGiIndex(&actor->base);
    if (giIndex == 0) {
        return false;
    }
    actor->pickedUp = true;
    if (!MMR_GiveItem(ctxt, &actor->base, giIndex)) {
        Player_Pause(ctxt);
    }
    return true;
}

u16 Item00_GetDespawnDelayAmount(ActorEnItem00* actor) {
    u16 giIndex = Rupee_GetDrawGiIndex(&actor->base);
    if (giIndex > 0) {
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->item == 0xB0) {
            // Ice Trap
            return 0x40;
        }
    }
    return 0xF;
}

void Item00_BeforeBeingPickedUp(ActorEnItem00* actor, GlobalContext* ctxt) {
    u16 giIndex = Rupee_GetDrawGiIndex(&actor->base);
    if (giIndex > 0) {
        GetItemEntry* entry = MMR_GetGiEntry(giIndex);
        if (entry->item == 0xB0) {
            // Ice Trap
            z2_PlayLoopingSfxAtActor(&actor->base, 0x31A4);
        }
    }
}

s8 Item00_CanBeSpawned(u16 params) {
    s8 result = params & 0xFF;
    if (gShouldForceItemSpawn) {
        return result;
    }
    u16 collectableFlag = (params >> 8) & 0x7F;
    if (collectableFlag > 0) {
        u16 giIndex = Rupee_CollectableFlagToGiIndex(collectableFlag);
        if (giIndex > 0) {
            return result;
        }
    }
    return z2_item_can_be_spawned(params & 0xFF);
}

s16 Item00_GetAlteredDropId(s16 dropId) {
    if ((((dropId == ITEM00_BOMBS_A) || (dropId == ITEM00_BOMBS_0) || (dropId == ITEM00_BOMBS_B)) &&
         (INV_CONTENT(ITEM_BOMB) == ITEM_NONE)) ||
        (((dropId == ITEM00_ARROWS_10) || (dropId == ITEM00_ARROWS_30) || (dropId == ITEM00_ARROWS_40) ||
          (dropId == ITEM00_ARROWS_50)) &&
         (INV_CONTENT(ITEM_BOW) == ITEM_NONE)) ||
        (((dropId == ITEM00_MAGIC_LARGE) || (dropId == ITEM00_MAGIC_SMALL)) &&
         (gSaveContext.perm.unk24.magicLevel == 0))) {
        return ITEM00_NO_DROP;
    }

    if (dropId == ITEM00_RECOVERY_HEART) {
        if (((void)0, gSaveContext.perm.unk24.maxLife) == ((void)0, gSaveContext.perm.unk24.currentLife) || MISC_CONFIG.flags.fewerHealthDrops) {
            return ITEM00_RUPEE_GREEN;
        }
    }

    if (dropId == ITEM00_BOMBS_A && MISC_CONFIG.flags.bombchuDrops && INV_CONTENT(ITEM_BOMBCHU) != ITEM_NONE) {
        u8 bombCount = AMMO(ITEM_BOMB);
        u8 bombchuCount = AMMO(ITEM_BOMBCHU);
        if (bombCount > 15 && bombchuCount > 15) {
            if (z2_Rand_ZeroOne() < 0.5f) {
                return dropId;
            }

            return ITEM00_BOMBS_0; // altered to be 5 Bombchu
        }

        if (bombCount <= bombchuCount) {
            return dropId;
        }

        return ITEM00_BOMBS_0; // altered to be 5 Bombchu
    }

    return dropId;
}
