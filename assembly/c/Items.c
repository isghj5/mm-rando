#include <z64.h>
#include "Icetrap.h"
#include "Items.h"
#include "QuestItems.h"
#include "Misc.h"
#include "MMR.h"
#include "macro.h"
#include "enums.h"
#include "GiantMask.h"

static u16 isFrogReturnedFlags[] = {
    0, 0x2040, 0x2080, 0x2101, 0x2102,
};

/**
 * Helper function used to process receiving a custom item.
 **/
static void HandleCustomItem(GlobalContext* ctxt, u8 item) {
    switch (item) {
        case CUSTOM_ITEM_ROYAL_WALLET:
            gSaveContext.perm.inv.upgrades.wallet = 3;
            break;
        case CUSTOM_ITEM_ICE_TRAP:
            Icetrap_PushPending(DAMAGE_EFFECT_FREEZE);
            break;
        case CUSTOM_ITEM_BOMBTRAP:
            Icetrap_PushPending(DAMAGE_EFFECT_BOMBTRAP);
            break;
        case CUSTOM_ITEM_CRIMSON_RUPEE:
            z2_AddRupees(30);
            break;
        case CUSTOM_ITEM_RUPOOR:
            z2_AddRupees(-10);
            break;
        case CUSTOM_ITEM_NOTHING:
            // nothing
            break;
        case CUSTOM_ITEM_SPIN_ATTACK:
            gSaveContext.perm.weekEventReg.hasGreatSpin = true;
            break;
        case CUSTOM_ITEM_MAGIC_POWER:;
            // TODO allow downgrades if they're not disabled
            // technically not vanilla behavior, as in vanilla getting double magic doesn't grant you magic if you don't have it
            bool isDoubleMagic = MMR_GetItemEntryContext->type >> 4;
            s8 magicAmount = 0x30;
            if (isDoubleMagic) {
                magicAmount = 0x60;
                gSaveContext.perm.unk24.hasDoubleMagic = true;
                gSaveContext.perm.unk24.magicLevel = 0;
            }
            gSaveContext.perm.unk24.hasMagic = true;
            if (gSaveContext.perm.unk24.currentMagic < magicAmount) {
                gSaveContext.perm.unk24.currentMagic = magicAmount;
            }
            break;
        case CUSTOM_ITEM_DOUBLE_DEFENSE:
            gSaveContext.perm.unk24.hasDoubleDefense = true;
            gSaveContext.perm.inv.defenseHearts = gSaveContext.perm.unk24.maxLife / 0x10;
            break;
        case CUSTOM_ITEM_STRAY_FAIRY:;
            u8 type = MMR_GetItemEntryContext->type >> 4;
            if (type > 0) {
                gSaveContext.perm.inv.strayFairies[type-1]++;
            } else {
                gSaveContext.perm.weekEventReg.hasTownFairy = true;
            }
            break;
        case CUSTOM_ITEM_NOTEBOOK_ENTRY:;
            u16* sBombersNotebookEventWeekEventFlags = (u16*)0x801C6B28;
            u8 entryIndex = MMR_GetItemEntryContext->flag;
            SET_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[entryIndex]);
            switch (entryIndex) {
                case BOMBERS_NOTEBOOK_EVENT_PROMISED_TO_MEET_KAFEI:
                    SET_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[BOMBERS_NOTEBOOK_EVENT_RECEIVED_LETTER_TO_KAFEI]);
                    break;
                case BOMBERS_NOTEBOOK_EVENT_DEFENDED_AGAINST_THEM:
                    SET_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[BOMBERS_NOTEBOOK_EVENT_RECEIVED_MILK_BOTTLE]);
                    break;
                case BOMBERS_NOTEBOOK_EVENT_ESCORTED_CREMIA:
                    SET_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[BOMBERS_NOTEBOOK_EVENT_RECEIVED_ROMANIS_MASK]);
                    break;
                case BOMBERS_NOTEBOOK_EVENT_RECEIVED_BOMBERS_NOTEBOOK:
                    SET_WEEKEVENTREG(sBombersNotebookEventWeekEventFlags[BOMBERS_NOTEBOOK_EVENT_LEARNED_SECRET_CODE]);
                    break;
            }
            break;
        case CUSTOM_ITEM_FROG:;
            u8 frogIndex = MMR_GetItemEntryContext->type >> 4;
            SET_WEEKEVENTREG(isFrogReturnedFlags[frogIndex]);
            break;
    }
}

static void SetRupeeCount(u16 rupees) {
    gSaveContext.owl.rupeeCounter += rupees;
}

static void HandleHearts(u8 item) {
    switch (item) {
        case ITEM_HEART_PIECE:
        case ITEM_HEART_PIECE_2:
            if (gSaveContext.perm.inv.questStatus.heartPiece != 0) {
                break;
            }
            // Fallthrough
        case ITEM_HEART_CONTAINER:
            if (gSaveContext.perm.unk24.hasDoubleDefense) {
                gSaveContext.perm.inv.defenseHearts++;
            }
            break;
    }
}

/**
 * Helper function used to fill rupees based on wallet if enabled.
 **/
static void HandleFillWallet(u8 item) {
    if (!MISC_CONFIG.flags.fillWallet)
        return;

    switch (item) {
        case ITEM_ADULT_WALLET:
            SetRupeeCount(gItemUpgradeCapacity.walletCapacity[1]);
            break;
        case ITEM_GIANT_WALLET:
            SetRupeeCount(gItemUpgradeCapacity.walletCapacity[2]);
            break;
        case CUSTOM_ITEM_ROYAL_WALLET:
            SetRupeeCount(gItemUpgradeCapacity.walletCapacity[3]);
            break;
    }
}

/**
 * Hook function called after receiving an item.
 *
 * Used to add items into quest storage.
 **/
void Items_AfterReceive(GlobalContext* ctxt, u8 item) {
    // Handle receival quest item.
    QuestItems_AfterReceive(item);
    // Handle custom items.
    HandleCustomItem(ctxt, item);
    // Fill rupees if wallet upgrade.
    HandleFillWallet(item);
    HandleHearts(item);
}

/**
 * Hook function called after removing an item from the inventory and buttons.
 *
 * Used to remove items from quest storage and cycle to the next storage item in the inventory.
 **/
void Items_AfterRemoval(s16 item, s16 slot) {
    // Handle removal of quest item.
    QuestItems_AfterRemoval((u8)item, (u8)slot);
}

bool Items_ShouldCheckItemUsabilityWhileSwimming(GlobalContext* ctxt, u8 item) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    bool isGiant = GiantMask_IsGiant();
    if (item == ITEM_ZORA_MASK && (!isGiant || player->mask != 0x14)) {
        return false;
    }
    if (item == ITEM_GIANT_MASK && isGiant && player->form == PLAYER_FORM_HUMAN && MISC_CONFIG.flags.giantMaskAnywhere) {
        return false;
    }
    if (item == ITEM_GORON_MASK && MISC_CONFIG.flags.ironGoron && (!isGiant || player->mask != 0x14)) {
        return false;
    }
    return true;
}

bool Items_ShouldCheckBButtonUsabilityWhileSwimming() {
    u8 currentForm = gSaveContext.perm.currentForm;
    return !(currentForm == PLAYER_FORM_ZORA
        || (MISC_CONFIG.flags.ironGoron && currentForm == PLAYER_FORM_GORON));
}

static const u8* sAudioBaseFilter = (u8*)0x801D66E0;

s32 Items_GetUnderwaterHazard(ActorPlayer* player) {
    if (player->stateFlags.state1 & PLAYER_STATE1_SWIM) {
        if ((player->form == PLAYER_FORM_ZORA)
            && (player->currentBoots >= PLAYER_BOOTS_ZORA_UNDERWATER)
            && (player->base.bgcheckFlags & 1)) { // BGCHECKFLAG_GROUND
            return PLAYER_ENV_HAZARD_UNDERWATER_FLOOR;
        } else {
            return PLAYER_ENV_HAZARD_SWIMMING;
        }
    } else if (player->form == PLAYER_FORM_GORON
        && MISC_CONFIG.flags.ironGoron
        && *sAudioBaseFilter == 0x20) {
        return PLAYER_ENV_HAZARD_UNDERWATER_FLOOR;
    } else {
        return PLAYER_ENV_HAZARD_NONE;
    }
}
