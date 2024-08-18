#include <z64.h>
#include "Misc.h"
#include "MMR.h"
#include "Util.h"
#include "enums.h"
#include "Items.h"
#include "Music.h"
#include "SaveFile.h"

struct MMRConfig MMR_CONFIG = {
    .magic = MMR_CONFIG_MAGIC,
    .version = 1,
    .locations = {
        .rupeeRepeatableLength = 0x80,
        .bottleRedPotion = 0x59,
        .bottleChateau = 0x6F,
        .bottleMilk = 0x60,
        .bottleGoldDust = 0x6A,
        .swordKokiri = 0x37,
        .swordRazor = 0x38,
        .swordGilded = 0x39,
        .magicSmall = 0x12C,
        .magicLarge = 0x12E,
        .walletAdult = 0x08,
        .walletGiant = 0x09,
        .walletRoyal = 0x44D,
        .bombBagSmall = 0x1B,
        .bombBagBig = 0x1C,
        .bombBagBiggest = 0x1D,
        .quiverSmall = 0x22,
        .quiverLarge = 0x23,
        .quiverLargest = 0x24,
        .lullaby = 0x74,
        .lullabyIntro = 0x44E,
    },
};

static GetItemEntry* gGiTable = NULL;

static void LoadGiTable(void) {
    // Use gi-table file index to get dmadata entry.
    u32 index = MMR_GiTableFileIndex;
    DmaEntry entry = dmadata[index];
    u32 size = entry.vromEnd - entry.vromStart;
    // Load the gi-table table from file into buffer.
    gGiTable = (GetItemEntry*)Util_HeapAlloc(size);
    z2_ReadFile(gGiTable, entry.vromStart, size);
}

GetItemEntry* MMR_GetGiEntry(u16 index) {
    return &gGiTable[index - 1];
}

u8* MMR_GiFlag(u16 giIndex) {
    u8* address = (u8*)(&gSaveContext.extra.cycleSceneFlags);
    // skip scenes' Clear flags, because they don't get saved
    if (giIndex >= 0x60) {
        address += 4;
    }
    if (giIndex >= 0xE0) {
        address += 4;
    }
    if (giIndex >= 0x160) {
        address += 4;
    }
    if (giIndex >= 0x1E0) {
        address += 4;
    }
    if (giIndex >= 0x260) {
        address += 4;
    }
    if (giIndex >= 0x2E0) {
        address += 4;
    }
    if (giIndex >= 0x360) {
        address += 4;
    }
    if (giIndex >= 0x380) { // skip scene 7 (Grottos) and scene 8 (Cutscene Map)
        address += 0x28;
    }
    if (giIndex >= 0x3E0) {
        address += 4;
    }
    if (giIndex >= 0x400) { // skip scenes 0xA through 0xD (Magic Hag's Potion Shop, Majora's Lair, Beneath the Graveyard, Curiosity Shop)
        address += 0x50;
    }
    if (giIndex >= 0x460) {
        address += 4;
    }
    if (giIndex >= 0x4E0) {
        address += 4;
    }
    // if (giIndex >= 0x560) { address += 4; } // next threshold is giIndex 0x560
    address += (giIndex >> 3);
    return address;
}

// TODO remove function from MMR mod files.
bool MMR_GetGiFlag(u16 giIndex) {
    u8 bit = giIndex & 7;
    u8* byte = MMR_GiFlag(giIndex);
    return (*byte >> bit) & 1;
}

// TODO remove function from MMR mod files.
void MMR_SetGiFlag(u16 giIndex) {
    u8 bit = giIndex & 7;
    u8* byte = MMR_GiFlag(giIndex);
    *byte |= (1 << bit);
}

bool MMR_CheckBottleAndGetGiFlag(u16 giIndex, u16* newGiIndex) {
    if (giIndex == MMR_CONFIG.locations.bottleRedPotion) {
        giIndex = 0x5B;
    } else if (giIndex == MMR_CONFIG.locations.bottleChateau) {
        giIndex = 0x91;
    } else if (giIndex == MMR_CONFIG.locations.bottleMilk) {
        giIndex = 0x92;
    } else if (giIndex == MMR_CONFIG.locations.bottleGoldDust) {
        giIndex = 0x93;
    }
    *newGiIndex = giIndex;
    return MMR_GetGiFlag(giIndex);
}

u16 MMR_CheckProgressiveUpgrades(u16 giIndex, bool grant) {
    if (giIndex == MMR_CONFIG.locations.swordKokiri || giIndex == MMR_CONFIG.locations.swordRazor || giIndex == MMR_CONFIG.locations.swordGilded) {
        u16 swordLevel = gSaveContext.perm.unk4C.equipment.sword;
        if (swordLevel == 0) {
            switch (gSaveContext.perm.stolenItem) {
                case ITEM_KOKIRI_SWORD:
                    swordLevel = 1;
                    break;
                case ITEM_RAZOR_SWORD:
                    swordLevel = 2;
                    break;
                case ITEM_GILDED_SWORD:
                    swordLevel = 3;
                    break;
            }
        }
        if (swordLevel == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.swordKokiri) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.swordKokiri = giIndex;
            }
            return MMR_CONFIG.locations.swordKokiri;
        }
        if (swordLevel == 1 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.swordRazor) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.swordRazor = giIndex;
            }
            return MMR_CONFIG.locations.swordRazor;
        }
        return MMR_CONFIG.locations.swordGilded;
    }
    if (giIndex == MMR_CONFIG.locations.magicSmall || giIndex == MMR_CONFIG.locations.magicLarge) {
        if (gSaveContext.perm.unk24.hasMagic == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.magicSmall) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.magicSmall = giIndex;
            }
            return MMR_CONFIG.locations.magicSmall;
        }
        return MMR_CONFIG.locations.magicLarge;
    }
    if (giIndex == MMR_CONFIG.locations.walletAdult || giIndex == MMR_CONFIG.locations.walletGiant || giIndex == MMR_CONFIG.locations.walletRoyal) {
        if (gSaveContext.perm.inv.upgrades.wallet == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.walletAdult) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.walletAdult = giIndex;
            }
            return MMR_CONFIG.locations.walletAdult;
        } else if (gSaveContext.perm.inv.upgrades.wallet == 1 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.walletGiant) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.walletGiant = giIndex;
            }
            return MMR_CONFIG.locations.walletGiant;
        }
        return MMR_CONFIG.locations.walletRoyal;
    }
    if (giIndex == MMR_CONFIG.locations.bombBagSmall || giIndex == MMR_CONFIG.locations.bombBagBig || giIndex == MMR_CONFIG.locations.bombBagBiggest) {
        if (gSaveContext.perm.inv.upgrades.bombBag == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.bombBagSmall) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.bombBagSmall = giIndex;
            }
            return MMR_CONFIG.locations.bombBagSmall;
        }
        if (gSaveContext.perm.inv.upgrades.bombBag == 1 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.bombBagBig) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.bombBagBig = giIndex;
            }
            return MMR_CONFIG.locations.bombBagBig;
        }
        return MMR_CONFIG.locations.bombBagBiggest;
    }
    if (giIndex == MMR_CONFIG.locations.quiverSmall || giIndex == MMR_CONFIG.locations.quiverLarge || giIndex == MMR_CONFIG.locations.quiverLargest) {
        if (gSaveContext.perm.inv.upgrades.quiver == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.quiverSmall) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.quiverSmall = giIndex;
            }
            return MMR_CONFIG.locations.quiverSmall;
        }
        if (gSaveContext.perm.inv.upgrades.quiver == 1 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.quiverLarge) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.quiverLarge = giIndex;
            }
            return MMR_CONFIG.locations.quiverLarge;
        }
        return MMR_CONFIG.locations.quiverLargest;
    }
    if (giIndex == MMR_CONFIG.locations.lullabyIntro || giIndex == MMR_CONFIG.locations.lullaby) {
        if (gSaveContext.perm.inv.questStatus.lullabyIntro == 0 || giIndex == SAVE_FILE_CONFIG.spentUpgrades.lullabyIntro) {
            if (grant) {
                SAVE_FILE_CONFIG.spentUpgrades.lullabyIntro = giIndex;
            }
            return MMR_CONFIG.locations.lullabyIntro;
        }
        return MMR_CONFIG.locations.lullaby;
    }
    return giIndex;
}

#define cycleRepeatableItemsLength 38
static u8 cycleRepeatableItems[cycleRepeatableItemsLength] = {
    ITEM_BOMB,
    ITEM_BOMBCHU,
    ITEM_DEKU_STICK,
    ITEM_DEKU_NUT,
    ITEM_MAGIC_BEAN,
    ITEM_POWDER_KEG,
    ITEM_RED_POTION,
    ITEM_GREEN_POTION,
    ITEM_BLUE_POTION,
    ITEM_FAIRY,
    ITEM_HERO_SHIELD,
    ITEM_MAGIC_JAR,
    ITEM_MAGIC_JAR_LARGE,
    ITEM_HEART,
    ITEM_PICKUP_DEKU_STICKS_5,
    ITEM_PICKUP_DEKU_STICKS_10,
    ITEM_PICKUP_DEKU_NUTS_5,
    ITEM_PICKUP_DEKU_NUTS_10,
    ITEM_PICKUP_BOMBS_5,
    ITEM_PICKUP_BOMBS_10,
    ITEM_PICKUP_BOMBS_20,
    ITEM_PICKUP_BOMBS_30,
    ITEM_PICKUP_ARROWS_10,
    ITEM_PICKUP_ARROWS_30,
    ITEM_PICKUP_ARROWS_40,
    ITEM_PICKUP_ARROWS_50,
    ITEM_PICKUP_BOMBCHU_20,
    ITEM_PICKUP_BOMBCHU_10,
    ITEM_PICKUP_BOMBCHU_1,
    ITEM_PICKUP_BOMBCHU_5,
    ITEM_CHATEAU_ROMANI,
    ITEM_MILK,
    ITEM_GOLD_DUST,
    CUSTOM_ITEM_ICE_TRAP,
    CUSTOM_ITEM_BOMBTRAP,
    CUSTOM_ITEM_RUPOOR,
    CUSTOM_ITEM_NOTHING,
    0xFF, // ? Stray Fairy ?
};
bool MMR_IsCycleRepeatable(u16 giIndex) {
    switch (giIndex) {
        case 0x9B: // GI_SWORD_GREAT_FAIRY_STOLEN
        case 0x9C: // GI_SWORD_KOKIRI_STOLEN
        case 0x9D: // GI_SWORD_RAZOR_STOLEN
        case 0x9E: // GI_SWORD_GILDED_STOLEN
        case 0xA9: // GI_BOTTLE_STOLEN
            return true;
    }
    GetItemEntry* entry = MMR_GetGiEntry(giIndex);
    if (entry->item >= 0x28 && entry->item <= 0x30) {
        // Trade/Quest items
        return MISC_CONFIG.flags.questItemStorage == 0;
    }
    if (entry->item >= 0x84 && entry->item <= 0x8A) {
        // Rupees
        if (giIndex >= 0x1CC) {
            // Collectable Drops
            return true;
        }
        for (u8 i = 0; i < MMR_CONFIG.locations.rupeeRepeatableLength; i++) {
            if (MMR_CONFIG.locations.rupeeRepeatable[i] == giIndex) {
                return true;
            }
        }
    }
    for (u8 i = 0; i < cycleRepeatableItemsLength; i++) {
        if (entry->item == cycleRepeatableItems[i]) {
            return true;
        }
    }
    return false;
}

u16 MMR_GetNewGiIndex(GlobalContext* ctxt, Actor* actor, u16 giIndex, bool grant) {
    if (gSaveContext.perm.cutscene != 0) {
        grant = false;
    }
    u16 newGiIndex = giIndex;
    bool flagged;
    if (gSaveContext.extra.titleSetupIndex != 0) {
        flagged = false;
        grant = false;
    } else {
        flagged = MMR_CheckBottleAndGetGiFlag(giIndex, &newGiIndex);
    }
    if (!flagged) {
        if (grant) {
            MMR_SetGiFlag(newGiIndex);
        }
        newGiIndex = giIndex;
        if (MISC_CONFIG.flags.progressiveUpgrades) {
            newGiIndex = MMR_CheckProgressiveUpgrades(newGiIndex, grant);
        }
    } else {
        bool isCycleRepeatable = MMR_IsCycleRepeatable(newGiIndex);
        if (!isCycleRepeatable) {
            newGiIndex = MISC_CONFIG.flags.fewerHealthDrops
                ? 0x01 // Green Rupee
                : 0x0A; // Recovery Heart
        }
    }
    if (grant) {
        MMR_SetGiFlag(giIndex);
        if (actor == (Actor*)GET_PLAYER(ctxt)) {
            GET_PLAYER(ctxt)->getItem = newGiIndex;
        }
    }
    return newGiIndex;
}

GetItemEntry* MMR_GetNewGiEntry(u16 giIndex) {
    u16 newGiIndex = MMR_GetNewGiIndex(NULL, NULL, giIndex, false);
    return MMR_GetGiEntry(newGiIndex);
}

static u16 gFanfares[] = { 0x4831, 0x4855, 0x0922, 0x0924, 0x0037, 0x0039, 0x0052 };

#define ITEM_QUEUE_LENGTH 4
static u16 itemQueue[ITEM_QUEUE_LENGTH] = { 0, 0, 0, 0 };
static s16 forceProcessIndex = -1;
static u16 lastProcessedGiIndex = 0;

void MMR_ProcessItem(GlobalContext* ctxt, u16 giIndex, bool continueTextbox) {
    // TODO ideally instead of forcing MMR_GetNewGiIndex to not set grant to false by temporarily
    // setting the cutscene to 0, the `grant` parameter should be removed and assumed false in the function,
    // and the granting functionality should be done here, since this grants the item for real even
    // if `grant` is reset to false.
    u32 tempCutscene = gSaveContext.perm.cutscene;
    gSaveContext.perm.cutscene = 0;
    giIndex = MMR_GetNewGiIndex(ctxt, NULL, giIndex, true);
    gSaveContext.perm.cutscene = tempCutscene;

    GetItemEntry* entry = MMR_GetGiEntry(giIndex);
    *MMR_GetItemEntryContext = *entry;
    if (continueTextbox) {
        z2_Message_ContinueTextbox(ctxt, entry->message);
    } else {
        z2_ShowMessage(ctxt, entry->message, NULL);
    }
    u8 soundType = entry->type & 0x0F;
    if (MUSIC_CONFIG.flags.disableFanfares && soundType > 1) {
        z2_PlaySfx(0x4824); // NA_SE_SY_GET_ITEM
    } else {
        u16 fanfare = gFanfares[soundType];
        if (soundType < 2) {
            z2_PlaySfx(fanfare);
        } else {
            z2_SetBGM2(fanfare);
        }
    }
    z2_GiveItem(ctxt, entry->item);
}

u16 MMR_GetProcessingItemGiIndex(GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (player) {
        if (forceProcessIndex == 0) {
            player->stateFlags.state1 |= PLAYER_STATE1_TIME_STOP_2;
        }
        if (player->stateFlags.state1 & (PLAYER_STATE1_GROTTO_IN | PLAYER_STATE1_TIME_STOP_2)) {
            return itemQueue[0];
        }
    }
    return 0;
}

void MMR_ClearItemQueue() {
    for (u8 i = 0; i < ITEM_QUEUE_LENGTH; i++) {
        itemQueue[i] = 0;
    }
    forceProcessIndex = -1;
}

void MMR_ProcessItemQueue(GlobalContext* ctxt) {
    u16 giIndex = MMR_GetProcessingItemGiIndex(ctxt);
    if (giIndex) {
        u8 messageState = z2_GetMessageState(&ctxt->msgCtx);
        if (!messageState) {
            MMR_ProcessItem(ctxt, giIndex, false);
            lastProcessedGiIndex = giIndex;
        } else if (messageState == 0x02 && giIndex == lastProcessedGiIndex) {
            // Closing
            lastProcessedGiIndex = 0;
            for (u8 i = 0; i < ITEM_QUEUE_LENGTH - 1; i++) {
                itemQueue[i] = itemQueue[i + 1];
            }
            if (forceProcessIndex >= 0) {
                forceProcessIndex--;
            }
            if (!itemQueue[0]) {
                ActorPlayer* player = GET_PLAYER(ctxt);
                player->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_2;
            }
        }
    }
}

u32 MMR_GetMinorItemSfxId(u8 item) {
    if (item >= ITEM_GREEN_RUPEE && item <= ITEM_GOLD_RUPEE) {
        return 0x4803; // Rupee
    }
    if (item == ITEM_HEART) {
        return 0x480B; // Recovery Heart
    }
    if (item >= ITEM_BOMB && item <= ITEM_MAGIC_BEAN && z2_IsItemKnown(item) != 0xFF) {
        return 0x4824;
    }
    if (item >= ITEM_PICKUP_DEKU_STICKS_5 && item <= ITEM_PICKUP_ARROWS_50 && z2_IsItemKnown(item) != 0xFF) {
        return 0x4824;
    }
    if (item >= ITEM_PICKUP_BOMBCHU_20 && item <= ITEM_PICKUP_BOMBCHU_5 && z2_IsItemKnown(ITEM_BOMBCHU) != 0xFF) {
        return 0x4824;
    }
    if (item == ITEM_MAGIC_JAR || item == ITEM_MAGIC_JAR_LARGE || item == CUSTOM_ITEM_CRIMSON_RUPEE || item == CUSTOM_ITEM_RUPOOR) {
        return 0x4824;
    }
    if (item == CUSTOM_ITEM_ICE_TRAP) {
        return 0x31A4;
    }
    if (item == CUSTOM_ITEM_BOMBTRAP) {
        return 0x3A76;
    }
    return 0;
}

void MMR_GiveItemToHold(Actor* actor, GlobalContext* ctxt, u16 giIndex) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    player->stateFlags.state1 |= PLAYER_STATE1_HOLD;
    player->getItem = giIndex;
    player->givingActor = actor;
}

bool MMR_IsActorFreestanding(s16 id) {
    if (id == 0xE || id == 0x25A || id == 0x1D2) {
        return true;
    }
    return false;
}

bool MMR_GiveItemIfMinor(GlobalContext* ctxt, Actor* actor, u16 giIndex) {
    bool isActorFreestanding = MMR_IsActorFreestanding(actor->id);
    giIndex = MMR_GetNewGiIndex(ctxt, NULL, giIndex, false);
    GetItemEntry* entry = MMR_GetGiEntry(giIndex);
    u32 minorItemSfxId = MMR_GetMinorItemSfxId(entry->item);

    if (minorItemSfxId) {
        if (minorItemSfxId == 0x31A4) {
            if (isActorFreestanding) {
                actor->draw = NULL;
            }
            z2_PlayLoopingSfxAtActor(actor, minorItemSfxId);
            z2_GiveItem(ctxt, entry->item);
            return true;
        }

        if (minorItemSfxId == 0x3A76) {
            if (isActorFreestanding) {
                actor->draw = NULL;
            }
            // Have IceTrap_Give handle sfx playback for this
            //z2_PlaySfx(minorItemSfxId);
            z2_GiveItem(ctxt, entry->item);
            return true;
        }

        if (!isActorFreestanding || MISC_CONFIG.drawFlags.freestanding) {
            z2_PlaySfx(minorItemSfxId);
            z2_GiveItem(ctxt, entry->item);
            return true;
        }
    }

    return false;
}

void MMR_QueueItem(u16 giIndex, bool forceProcess) {
    for (u8 i = 0; i < ITEM_QUEUE_LENGTH; i++) {
        if (itemQueue[i] == 0) {
            itemQueue[i] = giIndex;
            if (forceProcess) {
                forceProcessIndex = i;
            }
            break;
        }
    }
}

bool MMR_GiveItem(GlobalContext* ctxt, Actor* actor, u16 giIndex) {
    bool result = MMR_GiveItemIfMinor(ctxt, actor, giIndex);
    if (!result) {
        MMR_QueueItem(giIndex, false);
    }
    return result;
}

bool MMR_IsRecoveryHeart(u16 giIndex) {
    // Check that resolved get-item index does not evaluate to recovery heart (0xA).
    return MMR_GetNewGiIndex(NULL, NULL, giIndex, false) == 0xA;
}

void MMR_Init(void) {
    // If using vanilla layout, gi-table mod file is not included.
    if (!MISC_CONFIG.internal.vanillaLayout) {
        LoadGiTable();
    }
}
