#include <stdbool.h>
#include <z64.h>
#include "ActorHelper.h"
#include "ArrowCycle.h"
#include "Misc.h"
#include "Reloc.h"

struct ArrowCycleState {
    u16 frameDelay;
    s8 magicCost;
    Actor* arrow;
};

static struct ArrowCycleState gArrowCycleState = {
    .frameDelay = 0,
    .magicCost = 0,
    .arrow = NULL,
};

struct ArrowInfo {
    u8 item;
    u8 slot;
    u8 icon;
    u8 action;
    u16 var;
};

static struct ArrowInfo gArrows[4] = {
    { ITEM_BOW,         SLOT_BOW,         ITEM_BOW,             0x9, 0x2, },
    { ITEM_FIRE_ARROW,  SLOT_FIRE_ARROW,  ITEM_BOW_FIRE_ARROW,  0xA, 0x3, },
    { ITEM_ICE_ARROW,   SLOT_ICE_ARROW,   ITEM_BOW_ICE_ARROW,   0xB, 0x4, },
    { ITEM_LIGHT_ARROW, SLOT_LIGHT_ARROW, ITEM_BOW_LIGHT_ARROW, 0xC, 0x5, },
};

static const struct ArrowInfo* GetInfo(u16 variable) {
    for (int i = 0; i < 4; i++) {
        if (gArrows[i].var == variable) {
            return (const struct ArrowInfo*)&gArrows[i];
        }
    }
    return NULL;
}

static u16 GetNextArrowVariable(u16 variable) {
    switch (variable) {
        case 2: return 3; // Normal -> Fire
        case 3: return 4; // Fire -> Ice
        case 4: return 5; // Ice -> Light
        case 5: return 2; // Light -> Normal
        default: return variable;
    }
}

static s8 GetMagicCostByInfo(const struct ArrowInfo *info) {
    switch (info->item) {
        case ITEM_FIRE_ARROW:
        case ITEM_ICE_ARROW:
        case ITEM_LIGHT_ARROW:
            return ArrowCycle_GetMagicCost(info->item - 2);
        default:
            return 0;
    }
}

/**
 * Helper function for checking if the player has enough magic to switch to a different arrow type.
 **/
static bool HasEnoughMagic(s8 prevCost, s8 curCost) {
    if (MISC_CONFIG.flags.arrowMagicShow) {
        // If showing magic consumption, magic has not been consumed yet so only check current cost.
        return gSaveContext.perm.unk24.currentMagic >= curCost;
    } else {
        // If default behavior, magic has been consumed so check against difference.
        return gSaveContext.perm.unk24.currentMagic >= (curCost - prevCost);
    }
}

static const struct ArrowInfo* GetNextInfo(u16 variable) {
    // Get magic cost of current arrow type.
    s8 magicCost = GetMagicCostByInfo(GetInfo(variable));
    u16 current = variable;
    const struct ArrowInfo* info;
    for (int i = 0; i < 4; i++) {
        current = GetNextArrowVariable(current);
        info = GetInfo(current);
        const s8 magic = GetMagicCostByInfo(info);
        // Calculate difference in magic cost and ensure that the player has enough magic to switch.
        bool enoughMagic = HasEnoughMagic(magicCost, magic);
        if (info != NULL && info->item == gSaveContext.perm.inv.items[info->slot] && enoughMagic) {
            return info;
        }
    }
    return NULL;
}

/**
 * Call the En_Arrow actor constructor on an existing En_Arrow instance.
 *
 * This will re-copy the data used to draw the arrow trail color, and thus it will appear as it should.
 **/
static bool CallArrowActorCtor(Actor* arrow, GlobalContext* ctxt) {
    ActorOverlay* ovl = &gActorOverlayTable[ACTOR_EN_ARROW];
    ActorInit* init = Reloc_ResolveActorInit(ovl);
    if (init != NULL && init->init != NULL) {
        init->destroy(arrow, ctxt);
        init->init(arrow, ctxt);
        return true;
    } else {
        return false;
    }
}

static bool IsArrowItem(u8 item) {
    switch (item) {
        case ITEM_BOW:
        case ITEM_BOW_FIRE_ARROW:
        case ITEM_BOW_ICE_ARROW:
        case ITEM_BOW_LIGHT_ARROW:
            return true;
        default:
            return false;
    }
}

/**
 * Helper function used to update the arrow type on the current C button.
 **/
static void UpdateCButton(ActorPlayer* player, GlobalContext* ctxt, const struct ArrowInfo* info) {
    // Update the C button value & texture.
    gSaveContext.perm.unk4C.formButtonItems[0].buttons[player->itemButton] = info->icon;
    z2_ReloadButtonTexture(ctxt, player->itemButton);
    // Update player fields for new action type.
    player->itemActionParam = info->action;
    player->heldItemActionParam = info->action;
}

/**
 * Function called on delayed frame to finish processing the arrow cycle.
 **/
static void HandleFrameDelay(ActorPlayer* player, GlobalContext* ctxt, Actor* arrow) {
    // Sanity check: Ensure arrow is still an allocated actor after delay frame.
    if (!ActorHelper_DoesActorExist(arrow, ctxt, ACTORTYPE_ITEMACTION)) {
        return;
    }

    s16 prevEffectState = gSaveContext.extra.magicConsumeState;
    const struct ArrowInfo* curInfo = GetInfo(arrow->params);
    if (arrow != NULL && curInfo != NULL) {
        Actor* special = arrow->child;
        // Deconstruct and remove special arrow actor.
        if (special != NULL) {
            z2_ActorRemove(&ctxt->actorCtx, special, ctxt);
            arrow->child = NULL;
        }
        // Handle magic consume state.
        if (MISC_CONFIG.flags.arrowMagicShow) {
            if (curInfo->item != ITEM_BOW) {
                gSaveContext.extra.magicConsumeState = 4;
            }
        } else {
            // Make sure the game is aware that a special arrow effect is happening when switching
            // from normal arrow -> elemental arrow. Uses value 2 to make sure the magic cost is
            // consumed this frame.
            if (curInfo->item != ITEM_BOW) {
                gSaveContext.extra.magicConsumeState = 2;
            }
            // Refund magic cost of previous arrow type.
            if (prevEffectState >= 2 && !gSaveContext.perm.weekEventReg.hasInfiniteMagic) {
                gSaveContext.perm.unk24.currentMagic += gArrowCycleState.magicCost;
            }
        }
        // Set magic cost value to be subtracted when arrow effect state == 2.
        gSaveContext.extra.magicConsumeCost = GetMagicCostByInfo(curInfo);
    }
}

/**
 * Helper function used to find the En_Arrow actor if attached to player.
 **/
Actor* ArrowCycle_FindArrow(ActorPlayer* player, GlobalContext* ctxt) {
    Actor* attached = player->base.child;
    if (attached != NULL && attached->id == ACTOR_EN_ARROW && attached->parent == &player->base) {
        return attached;
    } else {
        return NULL;
    }
}

/**
 * Get the magic cost by index from the array in player_actor.
 **/
s8 ArrowCycle_GetMagicCost(u8 index) {
    if (index < 4) {
        // Vanilla RDRAM: 0x8077A448
        // 4-byte array for each cost: [Fire Arrow, Ice Arrow, Light Arrow, Deku Bubble]
        const s8* costArray = (const s8*)Reloc_ResolvePlayerOverlay(&s801D0B70.playerActor, 0x8085CFB8);
        return costArray[index];
    } else {
        return 0;
    }
}

/**
 * Handle arrow cycling.
 *
 * Note: A "delay frame" is necessary because we cannot free the special arrow actor immediately.
 * This is due to the double-buffer nature of the game's DLists, where each one is processed every
 * other frame, and if we free the actor immediately the next DList will still contain pointers to
 * data which requires the special arrow actor file to be loaded.
 *
 * Thus, we must set the draw pointer to NULL and let one frame process before freeing the actor.
 **/
void ArrowCycle_Handle(ActorPlayer* player, GlobalContext* ctxt) {
    // Check if processing arrow cycling on delay frame.
    if (gArrowCycleState.frameDelay >= 1) {
        HandleFrameDelay(player, ctxt, gArrowCycleState.arrow);
        gArrowCycleState.arrow = NULL;
        gArrowCycleState.frameDelay = 0;
        gArrowCycleState.magicCost = 0;
        return;
    }

    // Make sure arrow cycling is enabled.
    if (!MISC_CONFIG.flags.arrowCycle) {
        return;
    }

    // Check if buttons state is normal, otherwise we are possibly in a minigame or on Epona.
    if (gSaveContext.extra.buttonsState.state != BUTTONS_STATE_NORMAL) {
        return;
    }

    // Find the arrow currently attached to the player.
    Actor* arrow = ArrowCycle_FindArrow(player, ctxt);
    if (arrow == NULL) {
        return;
    }

    // Ensure arrow has an appropriate variable (cannot be a deku bubble).
    if (!(2 <= arrow->params && arrow->params < 6)) {
        return;
    }

    // Check if current button pressed corresponds to an arrow item.
    u8 selectedItem = gSaveContext.perm.unk4C.formButtonItems[0].buttons[player->itemButton];
    if (!IsArrowItem(selectedItem)) {
        return;
    }

    // Check if R is pressed.
    if (!ctxt->state.input[0].pressEdge.buttons.r) {
        return;
    }
    ctxt->state.input[0].pressEdge.buttons.r = 0;

    // Get info for arrow types.
    const struct ArrowInfo *curInfo, *nextInfo;
    curInfo = GetInfo(arrow->params);
    nextInfo = GetNextInfo(arrow->params);

    // Check if there is nothing to cycle to and return early.
    if (curInfo == NULL || nextInfo == NULL || curInfo->var == nextInfo->var) {
        // When not enough magic to cycle to anything else, switch C button back to normal bow.
        u8 item = gSaveContext.perm.unk4C.formButtonItems[0].buttons[player->itemButton];
        if (curInfo->var == 2 && item != ITEM_BOW && gSaveContext.perm.inv.items[SLOT_BOW] == ITEM_BOW) {
            UpdateCButton(player, ctxt, &gArrows[0]);
        }
        z2_PlaySfx(0x4806); // NA_SE_SY_ERROR
        return;
    }

    // Check if we are switching from normal arrow -> elemental arrow, and if so verify that an
    // existing effect is not active. Otherwise the game may crash by attempting to load the actor
    // code file for one effect while an existing effect is still processing.
    // This also prevents from switching when Lens of Truth is activated.
    if (curInfo->item == ITEM_BOW && gSaveContext.extra.magicConsumeState != 0) {
        z2_PlaySfx(0x4806); // NA_SE_SY_ERROR
        return;
    }

    // Update the existing actor variable.
    arrow->params = nextInfo->var;

    // Call arrow actor constructor so that the arrow trail color data is re-copied.
    CallArrowActorCtor(arrow, ctxt);

    // If found, NULL out special arrow actor's draw function to prevent it from writing to DList.
    Actor* special = arrow->child;
    if (special != NULL) {
        special->draw = NULL;
    }

    // Update C button.
    UpdateCButton(player, ctxt, nextInfo);

    // Prepare for finishing cycle next frame.
    gArrowCycleState.arrow = arrow;
    gArrowCycleState.frameDelay++;
    gArrowCycleState.magicCost = GetMagicCostByInfo(curInfo);

    // If cycling from normal arrow -> elemental arrow, reserve magic consume state.
    // This prevents using Lens between now and processing the delay frame, and thus prevents
    // mutating the magic consume state while Lens is active.
    if (curInfo->item == ITEM_BOW) {
        gSaveContext.extra.magicConsumeState = 3;
    }
}
