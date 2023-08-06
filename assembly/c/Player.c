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
#include "GiantMask.h"

bool Player_BeforeDamageProcess(ActorPlayer* player, GlobalContext* ctxt) {
    return Icetrap_Give(player, ctxt);
}

PlayerActionFunc sPlayer_Falling = NULL;
PlayerActionFunc sPlayer_BackwalkBraking = NULL;
PlayerUpperActionFunc sPlayer_UpperAction_CarryAboveHead = NULL;

void Player_InitFuncPointers() {
    if (sPlayer_Falling == NULL) {
        sPlayer_Falling = z2_Player_func_8084C16C;
    }
    if (sPlayer_BackwalkBraking == NULL) {
        sPlayer_BackwalkBraking = z2_Player_func_8084A884;
    }
    if (sPlayer_UpperAction_CarryAboveHead == NULL) {
        sPlayer_UpperAction_CarryAboveHead = z2_Player_UpperAction_CarryAboveHead;
    }
}

void Player_PreventDangerousStates(ActorPlayer* player) {
    if (!MISC_CONFIG.flags.saferGlitches) {
        return;
    }

    if (player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP_3) {
        if (((player->stateFlags.state1 & PLAYER_STATE1_AIR) && !(player->stateFlags.state1 & PLAYER_STATE1_LEDGE_CLIMB))
            || (player->stateFlags.state3 & PLAYER_STATE3_JUMP_ATTACK)) {
            player->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_3;
        }
    }

    // parent can be hookshot or epona
    // "&& player->base.parent->id == ACTOR_ARMS_HOOK" doesn't work because parent might be stale reference
    if (player->base.parent) {
        if ((player->stateFlags.state1 & PLAYER_STATE1_AIR) // might be redundant with the sPlayer_Falling check below
            || (player->stateFlags.state3 & PLAYER_STATE3_JUMP_ATTACK)
            || (player->actionFunc == sPlayer_Falling
                && !(player->stateFlags.state3 & PLAYER_STATE3_HOOK_ARRIVE_2))) {
            player->base.parent = NULL;
        }
        Actor* door = player->doorActor;
        if ((player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP) && door) {
            if (player->doorType == 4) { // PLAYER_DOORTYPE_STAIRCASE
                ActorDoorSpiral* doorStaircase = (ActorDoorSpiral*)door;
                if (doorStaircase->shouldClimb) {
                    player->base.parent = NULL;
                }
            } else if (player->doorType == 2) { // PLAYER_DOORTYPE_SLIDING
                SlidingDoorActor* doorSliding = (SlidingDoorActor*)door;
                if (doorSliding->unk_15C) {
                    player->base.parent = NULL;
                }
            } else {
                KnobDoorActor* doorHandle = (KnobDoorActor*)door;
                if (doorHandle->playOpenAnim) {
                    player->base.parent = NULL;
                }
            }
        }
    }
}

void Player_BeforeUpdate(ActorPlayer* player, GlobalContext* ctxt) {
    Player_InitFuncPointers();
    Dpad_BeforePlayerActorUpdate(player, ctxt);
    ExternalEffects_Handle(player, ctxt);
    ArrowCycle_Handle(player, ctxt);
    ArrowMagic_Handle(player, ctxt);
    DekuHop_Handle(player, ctxt);
    GiantMask_Handle(player, ctxt);
    Player_PreventDangerousStates(player);
}

bool Player_CanReceiveItem(GlobalContext* ctxt) {
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (player->stateFlags.state1 & (PLAYER_STATE1_AIM | PLAYER_STATE1_HOLD)) {
        return false;
    }
    if (player->upperActionFunc == sPlayer_UpperAction_CarryAboveHead) {
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
    if (player->formProperties->unk_2C < player->base.waterSurfaceDist) {
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
            (player->formProperties->unk_2C < player->base.waterSurfaceDist));
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
        case SCENE_MITURIN_BS: // Odolwa Boss Room
        case SCENE_HAKUGIN_BS: // Goht Boss Room
        case SCENE_SEA_BS: // Gyorg Boss Room
        case SCENE_INISIE_BS: // Twinmold Boss Room
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

    // really hacky, but necessary to prevent certain softlocks. unkAA5 gets reset back to 0 after transformation.
    this->heldItemActionParam = 0;
    this->unkAA5 = 5;

    if (this->stateFlags.state2 & PLAYER_STATE2_DIVING_2) {
        sSwimmingTransformation = true;
        this->stateFlags.state2 &= ~PLAYER_STATE2_DIVING_2;
    }
}

bool Player_AfterTransformInit(ActorPlayer* this, GlobalContext* ctxt) {
    if (sSwimmingTransformation) {
        this->stateFlags.state2 |= PLAYER_STATE2_DIVING_2;
        sSwimmingTransformation = false;
    }
    if (this->unkAA5 == 5) {
        this->unkAA5 = 0;
    }
    if (MISC_CONFIG.flags.instantTransform && !(this->stateFlags.state2 & PLAYER_STATE2_CLIMBING)) {
        if (this->actionFunc == sPlayer_BackwalkBraking) {
            z2_Player_func_8083692C(this, ctxt);
        }
        this->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_3;
        this->animTimer = 0;
        return true;
    }
    return false;
}

bool Player_HandleCutsceneItem(ActorPlayer* this, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.instantTransform && this->base.draw == NULL) {
        return true;
    }
    return z2_Player_func_80838A90(this, ctxt);
}

void Player_UseHeldItem(GlobalContext* ctxt, ActorPlayer* player, u8 item, u8 actionParam) {
    if (MISC_CONFIG.flags.bombArrows && item == ITEM_BOMB) {
        ActorEnArrow* arrow = (ActorEnArrow*)ArrowCycle_FindArrow(player, ctxt);
        if (arrow != NULL && arrow->base.params == 2) {
            if (arrow->base.child == NULL) {
                ActorEnBom* bomb = (ActorEnBom*) z2_Actor_SpawnAsChild(&ctxt->actorCtx, &arrow->base, ctxt, ACTOR_EN_BOM,
                                    arrow->base.currPosRot.pos.x, arrow->base.currPosRot.pos.y, arrow->base.currPosRot.pos.z,
                                    0, arrow->base.shape.rot.y, 0, 0);
                if (bomb != NULL) {
                    bomb->collider1.base.flagsAC &= ~8; // AC_TYPE_PLAYER Disable player-aligned damage
                    arrow->collider.body.toucher.dmgFlags = 8; // make arrow do explosive damage
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

void Player_UseExplosiveAmmo(s16 item, s16 ammoChange, GlobalContext* ctxt) {
    if (item == ITEM_POWDER_KEG && GET_PLAYER(ctxt)->base.init) {
        return;
    }
    z2_Inventory_ChangeAmmo(item, ammoChange);
}

static const f32 giantSpeedModifier = 7.0f / 11.0f;
void Player_HandleFormSpeed(GlobalContext* ctxt, ActorPlayer* player, f32* speed) {
    // Displaced code:
    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        *speed = *speed * 1.5;
    }
    // End displaced code

    *speed *= GiantMask_GetSimpleScaleModifier();

    if (player->mask == 0x14) {
        *speed *= giantSpeedModifier;
    }
}

f32 Player_GetWallCollisionHeight(ActorPlayer* player) {
    // Displaced code:
    f32 result = 26.8f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetDiveDepth() {
    // Displaced code:
    f32 result = 100.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetWaterSurfaceDistanceClimbHeight(ActorPlayer* player) {
    // Displaced code:
    f32 result;
    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        result = 80.0f;
    } else {
        result = 50.0f;
    }
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetLedgeClimbFactorFromSwim() {
    // Displaced code:
    f32 result = 60.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

// 806F25A8
f32 Player_GetLedgeClimbFactor() {
    // Displaced code:
    f32 result = 59.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

// 806F25C0
f32 Player_GetLedgeClimbFactor2() {
    // Displaced code:
    f32 result = 41.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

// 806F2600
f32 Player_GetInvertedLedgeClimbFactor() {
    return 100.0f / GiantMask_GetSimpleScaleModifier();
}

// 806F275C
f32 Player_GetLedgeJumpSpeed() {
    // Displaced code:
    f32 result = 5.5f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetMidAirJumpSlashHeight(f32* outAlternateHeight) {
    // Displaced code:
    f32 result = 3.0f;
    *outAlternateHeight = 4.5f;
    // End displaced code

    f32 modifier = GiantMask_GetSimpleScaleModifier();
    result *= modifier;
    *outAlternateHeight *= modifier;

    return result;
}

f32 Player_GetJumpHeightModifier(GlobalContext* ctxt, ActorPlayer* player) {
    f32 result = GiantMask_GetSimpleScaleModifier();

    // Displaced code:
    if (player->currentBoots == 2) {
        result *= 0.5f;
    }
    // End displaced code

    return result;
}

void Player_ModifyJumpVelocity(ActorPlayer* player) {
    player->linearVelocity *= GiantMask_GetSimpleScaleModifier();
}

f32 Player_GetJumpSlashHeight() {
    // Displaced code:
    f32 result = 5.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetNewMinVelocityY() {
    // Displaced code:
    f32 result = -20.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetRunDeceleration() {
    // Displaced code:
    f32 result = 1.5f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetBackwalkAcceleration() {
    // Displaced code:
    f32 result = 1.5f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

s32 Player_HandleInputVelocity(f32* pValue, f32 target, f32 incrStep, f32 decrStep) {
    const f32 modifier = GiantMask_GetSimpleScaleModifier();
    incrStep *= modifier;
    decrStep *= modifier;
    return z2_Math_AsymStepToF(pValue, target, incrStep, decrStep);
}

void Player_GetMidAirAcceleration(f32* increment, f32* decrement) {
    const f32 modifier = GiantMask_GetSimpleScaleModifier();
    *increment *= modifier;
    *decrement *= modifier;
}

// 807099F4
f32 Player_GetLedgeGrabDistance() {
    // Displaced code:
    f32 result = 150.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

void Player_AfterJumpSlashGravity(GlobalContext* ctxt, ActorPlayer* player) {
    player->base.gravity *= GiantMask_GetSimpleScaleModifier();
}

f32 Player_GetSpinChargeWalkSpeedFactor() {
    // Displaced code:
    f32 result = 1.5f;
    // End displaced code

    result *= (1.0f / GiantMask_GetSimpleScaleModifier());

    return result;
}

f32 Player_GetClimbXZDelta() {
    // Displaced code:
    f32 result = 50.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetClimbYDelta() {
    // Displaced code:
    f32 result = 26.8f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetDiveSpeed() {
    // Displaced code:
    f32 result = -2.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

bool Player_UseItem_CheckCeiling(CollisionContext* colCtx, f32* outY, Vec3f* pos, f32 checkHeight, BgPolygon** outPoly, s32* inItemIdOutBgId, ActorPlayer* player) {
    s32 itemId = *inItemIdOutBgId;
    if (itemId == ITEM_GIANT_MASK) {
        checkHeight *= GiantMask_GetNextScaleFactor();
    }
    return z2_BgCheck_EntityCheckCeiling(colCtx, outY, pos, checkHeight, outPoly, inItemIdOutBgId, &player->base);
}

void Player_AfterUpdateCollisionCylinder(ActorPlayer* player) {
    player->collisionCylinder.params.radius *= GiantMask_GetSimpleScaleModifier();
}

u8 Player_GetMaskOnLoad(ActorPlayer* player) {
    u8 result = gSaveContext.perm.mask;
    if (result == 0x14) {
        if (MISC_CONFIG.flags.giantMaskAnywhere) {
            gSaveContext.extra.magicConsumeState = 0x0C;
        } else {
            gSaveContext.perm.mask = 0;
            result = 0;
        }
    } else if (MISC_CONFIG.flags.giantMaskAnywhere) {
        GiantMask_TryReset();
    }
    return result;
}

bool Player_ShouldCheckItemUsabilityWhileSwimming(ActorPlayer* player, u8 item) {
    bool isGiant = GiantMask_IsGiant();

    // Zora Mask is 0x50 for some reason
    if (item == 0x50 && (!isGiant || player->mask != 0x14)) {
        return false;
    }

    // Giant's Mask is 0x4D for some reason
    if (item == 0x4D && isGiant && player->form == PLAYER_FORM_HUMAN && MISC_CONFIG.flags.giantMaskAnywhere) {
        return false;
    }
    return true;
}

f32 Player_ModifyGoronRollMultiplier(f32 multiplier) {
    return multiplier * GiantMask_GetSimpleInvertedScaleModifier();
}

f32 Player_GetGoronMaxRoll() {
    // Displaced code:
    f32 result = 18.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

void Player_AfterCrushed(void) {
    // Displaced Code:
    // Intentionally removed code
    // if (INV_CONTENT(ITEM_MASK_DEKU) == ITEM_MASK_DEKU) {
    gSaveContext.perm.currentForm = PLAYER_FORM_HUMAN;
    gSaveContext.perm.mask = 0; // PLAYER_MASK_NONE
    // }
    // End displaced code

    if (MISC_CONFIG.flags.giantMaskAnywhere) {
        GiantMask_MarkReset();
        GiantMask_TryReset();
    }
}

s32 Player_GetWeaponDamageFlags(ActorPlayer* player, s32 dmgFlags) {
    if (player->mask == 0x14 && GiantMask_IsGiant()) {
        return 0xC0000108; // DMG_POWDER_KEG | DMG_EXPLOSIVES | DMG_GORON_PUNCH | DMG_UNK_0x1E // DMG_UNK_0x1E is used to trigger different damage calculation algorithm.
    }
    return dmgFlags;
}

bool Player_ShouldBeKnockedOver(GlobalContext* ctxt, ActorPlayer* player, s32 damageType) {
    if (player->mask == 0x14 && GiantMask_IsGiant()) {
        return false;
    }

    // Displaced code:
    return damageType == 1
        || damageType == 2
        || !(player->base.bgcheckFlags & 1) // BGCHECKFLAG_GROUND
        || (player->stateFlags.state1 & (PLAYER_STATE1_LEDGE_CLIMB | PLAYER_STATE1_LEDGE_HANG | PLAYER_STATE1_CLIMB_UP | PLAYER_STATE1_LADDER));
}

bool Player_CantBeGrabbed(GlobalContext* ctxt, ActorPlayer* player) {
    if (player->mask == 0x14 && GiantMask_IsGiant()) {
        return true;
    }

    // Displaced code:
    return z2_Player_InBlockingCsMode(ctxt, player);
}
