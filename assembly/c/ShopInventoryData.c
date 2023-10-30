#include <stdbool.h>
#include <z64.h>
#include "MMR.h"
#include "Misc.h"
#include "enums.h"

static bool DoesItemRequireBottle(u8 item) {
    if (item >= ITEM_RED_POTION
    && item <= ITEM_EMPTY_BOTTLE_2
    && item != ITEM_MILK_BOTTLE
    && item != ITEM_MILK_HALF_BOTTLE
    && item != ITEM_GOLD_DUST_BOTTLE
    && item != ITEM_CHATEAU_ROMANI_BOTTLE
    && item != ITEM_EEL_BOTTLE) {
        return true;
    }

    if (item >= ITEM_CHATEAU_ROMANI && item <= ITEM_SEAHORSE2) {
        return true;
    }

    return false;
}

static bool IsItemInstant(u8 item) {
    if (item >= ITEM_BOMB && item <= ITEM_MAGIC_BEAN) {
        return z2_IsItemKnown(item) != 0xFF;
    }
    if (item == ITEM_POWDER_KEG) {
        return z2_IsItemKnown(item) != 0xFF;
    }
    if (DoesItemRequireBottle(item)) {
        return true;
    }
    if (item == ITEM_MAGIC_JAR || item == ITEM_MAGIC_JAR_LARGE) {
        return true;
    }
    if (item >= ITEM_HEART && item <= ITEM_GOLD_RUPEE) {
        return true;
    }
    if (item >= ITEM_PICKUP_DEKU_STICKS_5 && item <= ITEM_PICKUP_ARROWS_50) {
        return z2_IsItemKnown(item) != 0xFF;
    }
    if (item >= ITEM_PICKUP_BOMBCHU_20 && item <= ITEM_PICKUP_BOMBCHU_5) {
        return z2_IsItemKnown(ITEM_BOMBCHU) != 0xFF;
    }
    return false;
}

static bool IsItemAtMaxCapacity(u8 item) {
    if (item == ITEM_HEART) {
        return gSaveContext.perm.unk24.currentLife >= gSaveContext.perm.unk24.maxLife;
    }
    if (item == ITEM_MAGIC_JAR || item == ITEM_MAGIC_JAR_LARGE) {
        return gSaveContext.perm.unk24.currentMagic >= gSaveContext.perm.unk24.magicLevel * 0x30;
    }
    if (item >= ITEM_GREEN_RUPEE && item <= ITEM_GOLD_RUPEE) {
        u16 capacity = gItemUpgradeCapacity.walletCapacity[gSaveContext.perm.inv.upgrades.wallet];
        return gSaveContext.perm.unk24.rupees >= capacity;
    }
    if (item >= ITEM_PICKUP_ARROWS_10 && item <= ITEM_PICKUP_ARROWS_50) {
        u16 capacity = gItemUpgradeCapacity.arrowCapacity[gSaveContext.perm.inv.upgrades.quiver];
        return gSaveContext.perm.inv.quantities[ITEM_BOW] >= capacity;
    }
    if (item == ITEM_BOMB || (item >= ITEM_PICKUP_BOMBS_5 && item <= ITEM_PICKUP_BOMBS_30)) {
        u16 capacity = gItemUpgradeCapacity.bombCapacity[gSaveContext.perm.inv.upgrades.bombBag];
        return gSaveContext.perm.inv.quantities[ITEM_BOMB] >= capacity;
    }
    if (item == ITEM_BOMBCHU || (item >= ITEM_PICKUP_BOMBCHU_20 && item <= ITEM_PICKUP_BOMBCHU_5)) {
        u16 capacity = gItemUpgradeCapacity.bombCapacity[gSaveContext.perm.inv.upgrades.bombBag];
        return gSaveContext.perm.inv.quantities[ITEM_BOMBCHU] >= capacity;
    }
    if (item == ITEM_DEKU_STICK || (item >= ITEM_PICKUP_DEKU_STICKS_5 && item <= ITEM_PICKUP_DEKU_STICKS_10)) {
        u16 capacity = gItemUpgradeCapacity.stickCapacity[gSaveContext.perm.inv.upgrades.dekuStick];
        return gSaveContext.perm.inv.quantities[ITEM_DEKU_STICK] >= capacity;
    }
    if (item == ITEM_DEKU_NUT || (item >= ITEM_PICKUP_DEKU_NUTS_5 && item <= ITEM_PICKUP_DEKU_NUTS_10)) {
        u16 capacity = gItemUpgradeCapacity.nutCapacity[gSaveContext.perm.inv.upgrades.dekuNut];
        return gSaveContext.perm.inv.quantities[ITEM_DEKU_NUT] >= capacity;
    }
    if (item == ITEM_MAGIC_BEAN) {
        u16 capacity = 20;
        return gSaveContext.perm.inv.quantities[ITEM_MAGIC_BEAN] >= capacity;
    }
    if (item == ITEM_POWDER_KEG) {
        u16 capacity = 1;
        return gSaveContext.perm.inv.quantities[ITEM_POWDER_KEG] >= capacity;
    }
    return false;
}

u8 ShopInventoryData_CheckPurchase(GlobalContext* ctxt, ActorEnGirlA* actor) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        return actor->checkPurchase(ctxt, actor);
    }

    if (gSaveContext.perm.unk24.rupees < ctxt->msgCtx.messageCost) {
        return 4;
    }

    if (MISC_CONFIG.drawFlags.shopModels) {
        u16 giIndex = MMR_GetNewGiIndex(ctxt, NULL, actor->giIndex, false);
        GetItemEntry* giEntry = MMR_GetGiEntry(giIndex);
        u8 item = giEntry->item;

        if (IsItemAtMaxCapacity(item)) {
            return 2;
        }

        if (DoesItemRequireBottle(item) && !z2_HasEmptyBottle()) {
            return 3;
        }

        if (IsItemInstant(item)) {
            return 1;
        }
    }

    return 0;
}

void ShopInventoryData_HandleInstantPurchase(GlobalContext* ctxt, ActorEnGirlA* actor) {
    if (MISC_CONFIG.internal.vanillaLayout) {
        actor->handleInstantPurchase(ctxt, actor);
        return;
    }
    u16 giIndex = MMR_GetNewGiIndex(ctxt, NULL, actor->giIndex, true);
    GetItemEntry* giEntry = MMR_GetGiEntry(giIndex);
    *MMR_GetItemEntryContext = *giEntry;
    z2_GiveItem(ctxt, giEntry->item);
    z2_AddRupees(-ctxt->msgCtx.messageCost);
}
