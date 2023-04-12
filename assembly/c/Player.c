#include <stdbool.h>
#include <z64.h>
#include "ArrowCycle.h"
#include "ArrowMagic.h"
#include "DekuHop.h"
#include "Dpad.h"
#include "ExternalEffects.h"
#include "Icetrap.h"
#include "Reloc.h"
#include "Misc.h"
#include "enums.h"
#include "Util.h"

bool Player_BeforeDamageProcess(ActorPlayer* player, GlobalContext* ctxt) {
    return Icetrap_Give(player, ctxt);
}

void Player_PreventDangerousStates(ActorPlayer* player) {
    if (!MISC_CONFIG.flags.saferGlitches) {
        return;
    }

    if (player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP_3) {
        if ((player->stateFlags.state1 & PLAYER_STATE1_AIR)
            || (player->stateFlags.state3 & PLAYER_STATE3_JUMP_ATTACK)) {
            player->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_3;
        }
    }
}

void Player_BeforeUpdate(ActorPlayer* player, GlobalContext* ctxt) {
    Dpad_BeforePlayerActorUpdate(player, ctxt);
    ExternalEffects_Handle(player, ctxt);
    ArrowCycle_Handle(player, ctxt);
    ArrowMagic_Handle(player, ctxt);
    DekuHop_Handle(player, ctxt);
    Player_PreventDangerousStates(player);
}

bool Player_CanReceiveItem(GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if ((player->stateFlags.state1 & PLAYER_STATE1_AIM) != 0) {
        return false;
    }
    bool result = false;
    switch (player->currentAnimation.id) {
        case 0xE208: // rolling - Goron
            result = ctxt->state.input[0].current.buttons.a != 0;
            break;
        case 0xDD28: // rolling - Human, Goron, Zora
        case 0xDF20: // idle - Human with sword
        case 0xDF28: // idle - Human, Deku
        case 0xE260: // idle - Goron
        case 0xE410: // idle - Zora
        case 0xD988: // idle - Fierce Deity
        case 0xD918: // walking with sword
        case 0xDE40: // walking - Human, Deku, Goron, Zora
        case 0xD920: // walking - Fierce Deity
        case 0xD800: // backwalking after backflip - Human, Goron, Zora, Fierce Deity
        case 0xDD18: // backwalking after backflip - Deku
        case 0xD928: // sidewalking - Fierce Deity
        case 0xDE78: // sidewalking - Human, Deku, Goron, Zora
            result = true;
            break;
    }
    return result;
}

void Player_Pause(GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (!(player->stateFlags.state1 & PLAYER_STATE1_GROTTO_IN)) {
        player->stateFlags.state1 |= PLAYER_STATE1_TIME_STOP_2;
    }
}

void Player_Unpause(GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    player->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_2;
}

/**
 * Helper function used to perform effects if entering water, and update the player swim flag.
 **/
static void HandleEnterWater(ActorPlayer* player, GlobalContext* ctxt) {
    // Check water distance to determine if in water.
    if (player->tableA68[11] < player->base.waterSurfaceDist) {
        // If swim flag not set, perform effects (sound + visual) for entering water.
        if ((player->stateFlags.state1 & PLAYER_STATE1_SWIM) == 0) {
            z2_PerformEnterWaterEffects(ctxt, player);
        }
        // Set swim flag.
        player->stateFlags.state1 |= PLAYER_STATE1_SWIM;
    }
}

/**
 * Helper function called to check if the player is in water.
 **/
static bool InWater(ActorPlayer* player) {
    return ((player->stateFlags.state1 & PLAYER_STATE1_SWIM) != 0 ||
            (player->tableA68[11] < player->base.waterSurfaceDist));
}

/**
 * Hook function called before handling "frozen" player state.
 **/
void Player_BeforeHandleFrozenState(ActorPlayer* player, GlobalContext* ctxt) {
    HandleEnterWater(player, ctxt);
    // If frozen while swimming, should float on water.
    if (InWater(player)) {
        z2_PlayerHandleBuoyancy(player);
    }
}

/**
 * Hook function called before handling "voiding" player state.
 **/
void Player_BeforeHandleVoidingState(ActorPlayer* player, GlobalContext* ctxt) {
    HandleEnterWater(player, ctxt);
    // Note: Later in the function of this hook, is where the "frozen" player state flag is set.
    // Since we can't check the player state flags, do the same check this function does instead.
    bool frozen = player->currentAnimation.value == 0;
    bool zora = player->form == 2;
    // If Zora is voiding (frozen) and swimming, should float in water.
    if (zora && frozen && InWater(player)) {
        z2_PlayerHandleBuoyancy(player);
    }
}

/**
 * Hook function to check whether or not freezing should void Zora.
 **/
bool Player_ShouldIceVoidZora(ActorPlayer* player, GlobalContext* ctxt) {
    switch (ctxt->sceneNum) {
        case 0x1F: // Odolwa Boss Room
        case 0x44: // Goht Boss Room
        case 0x5F: // Gyorg Boss Room
        case 0x36: // Twinmold Boss Room
            return false;
        default:
            return true;
    }
}

/**
 * Hook function called to determine if swim state should be prevented from being restored.
 **/
bool Player_ShouldPreventRestoringSwimState(ActorPlayer* player, GlobalContext* ctxt, void* function) {
    void* handleFrozen = Reloc_ResolvePlayerOverlay(&s801D0B70.playerActor, 0x808546D0); // Offset: 0x26C40
    return function == handleFrozen;
}

static u32 lastClimbFrame = 0;
static u32 startClimbingTimer = 5;
u32 Player_GetCollisionType(ActorPlayer* player, GlobalContext* ctxt, u32 collisionType) {
    if (!MISC_CONFIG.flags.climbAnything) {
        return collisionType;
    }

    if ((player->stateFlags.state1 & PLAYER_STATE1_SWIM) && !(player->stateFlags.state2 & PLAYER_STATE2_DIVING_2)) {
        return 8;
    }

    u32 currentClimbFrame = ctxt->state.frames;

    if (currentClimbFrame == lastClimbFrame + 1) {
        startClimbingTimer--;
    } else {
        startClimbingTimer = 5;
    }

    if (startClimbingTimer == 0) {
        return 8;
    }

    lastClimbFrame = currentClimbFrame;

    return collisionType;
}

static bool sSwimmingTransformation = false;

void Player_StartTransformation(GlobalContext* ctxt, ActorPlayer* this, s8 actionParam) {
    if (!MISC_CONFIG.flags.instantTransform
        || actionParam < PLAYER_IA_MASK_FIERCE_DEITY
        || actionParam > PLAYER_IA_MASK_DEKU
        || this->animTimer != 0
        || (this->talkActor != NULL && this->talkActor->flags & 0x10000)
        || (this->stateFlags.state1 & PLAYER_STATE1_TIME_STOP)
        || (this->stateFlags.state2 & PLAYER_STATE2_DIVING)
        || (this->currentBoots == 4 && this->prevBoots == 5)) {
        // Displaced code:
        this->heldItemActionParam = actionParam;
        this->unkAA5 = 5; // PLAYER_UNKAA5_5
        return;
    }

    u8 playerForm;
    s32 maskId = actionParam - PLAYER_IA_39;
    if (maskId == this->mask) {
        playerForm = PLAYER_FORM_HUMAN;
    } else {
        playerForm = actionParam - PLAYER_IA_MASK_FIERCE_DEITY;
    }

    gSaveContext.perm.currentForm = playerForm;
    this->base.update = (ActorFunc)0x8012301C;
    this->base.draw = NULL;
    this->animTimer = 1;

    this->stateFlags.state1 |= PLAYER_STATE1_TIME_STOP_3;

    if (this->stateFlags.state2 & PLAYER_STATE2_DIVING_2) {
        sSwimmingTransformation = true;
        this->stateFlags.state2 &= ~PLAYER_STATE2_DIVING_2;
    }
}

bool Player_AfterTransformInit(ActorPlayer* this) {
    if (sSwimmingTransformation) {
        this->stateFlags.state2 |= PLAYER_STATE2_DIVING_2;
        sSwimmingTransformation = false;
    }
    if (MISC_CONFIG.flags.instantTransform && !(this->stateFlags.state2 & PLAYER_STATE2_CLIMBING)) {
        this->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_3;
        this->animTimer = 0;
        return true;
    }
    return false;
}

void Player_UseHeldItem(GlobalContext* ctxt, ActorPlayer* player, u8 item, u8 actionParam) {
    if (MISC_CONFIG.flags.bombArrows && item == ITEM_BOMB) {
        ActorEnArrow* arrow = (ActorEnArrow*)ArrowCycle_FindArrow(player, ctxt);
        if (arrow != NULL) {
            if (arrow->base.child == NULL) {
                ActorEnBom* bomb = (ActorEnBom*) z2_Actor_SpawnAsChild(&ctxt->actorCtx, &arrow->base, ctxt, ACTOR_EN_BOM,
                                    arrow->base.currPosRot.pos.x, arrow->base.currPosRot.pos.y, arrow->base.currPosRot.pos.z,
                                    0, arrow->base.shape.rot.y, 0, 0);
                if (bomb != NULL) {
                    bomb->collider1.base.flagsAC &= ~8; // AC_TYPE_PLAYER Disable player-aligned damage
                    z2_SetActorSize(&bomb->base, 0.002);
                    z2_Inventory_ChangeAmmo(item, -1);
                }
            }
            *z2_D_80862B4C = 1;
            return;
        }
    }

    // Displaced code:
    player->heldItemId = item;
    player->stateFlags.state3 |= PLAYER_STATE3_PULL_ITEM;
}

void Player_CheckHeldItem(ActorPlayer* this, GlobalContext* ctxt, u16 curButtons, u16* checkButtons) {
    for (s32 i = 0; i < 4; i++) {
        if (CHECK_BTN_ALL(curButtons, checkButtons[i])) {
            s32 item = z2_Inventory_GetBtnItem(ctxt, this, i);
            if ((item < 0xFD) && (z2_Player_ItemToActionParam(this, item) == this->itemActionParam)) {
                *z2_D_80862B4C = 1;
                break;
            }
        }
    }
}
