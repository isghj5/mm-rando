#include <stdbool.h>
#include <z64.h>
#include "macro.h"
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
#include "SaveFile.h"

static bool sSwimmingTransformation = false;
static u8 sInstantTranformTimer = 0;

static bool sFuncPointersInitialized = false;
static PlayerActionFunc sPlayer_Idle = NULL;
static PlayerActionFunc sPlayer_BackwalkBraking = NULL;
static PlayerActionFunc sPlayer_Falling = NULL;
static PlayerActionFunc sPlayer_Action_43 = NULL;
static PlayerActionFunc sPlayer_Action_52 = NULL;
static PlayerActionFunc sPlayer_Action_53 = NULL;
static PlayerActionFunc sPlayer_Action_54 = NULL;
static PlayerActionFunc sPlayer_Action_55 = NULL;
static PlayerActionFunc sPlayer_Action_56 = NULL;
static PlayerActionFunc sPlayer_Action_57 = NULL;
static PlayerActionFunc sPlayer_Action_58 = NULL;
static PlayerActionFunc sPlayer_Action_59 = NULL;
static PlayerActionFunc sPlayer_Action_60 = NULL;
static PlayerActionFunc sPlayer_Action_62 = NULL;
static PlayerActionFunc sPlayer_Action_61 = NULL;
static PlayerActionFunc sPlayer_Action_82 = NULL;
static PlayerActionFunc sPlayer_Action_93 = NULL;
static PlayerActionFunc sPlayer_Action_96 = NULL;
static PlayerUpperActionFunc sPlayer_UpperAction_CarryAboveHead = NULL;

void Player_InitFuncPointers() {
    if (sFuncPointersInitialized) {
        return;
    }
    sPlayer_Idle = z2_Player_Action_4;
    sPlayer_BackwalkBraking = z2_Player_Action_8;
    sPlayer_Falling = z2_Player_Action_25;
    sPlayer_Action_43 = z2_Player_Action_43;
    sPlayer_Action_52 = z2_Player_Action_52;
    sPlayer_Action_53 = z2_Player_Action_53;
    sPlayer_Action_54 = z2_Player_Action_54;
    sPlayer_Action_55 = z2_Player_Action_55;
    sPlayer_Action_56 = z2_Player_Action_56;
    sPlayer_Action_57 = z2_Player_Action_57;
    sPlayer_Action_58 = z2_Player_Action_58;
    sPlayer_Action_59 = z2_Player_Action_59;
    sPlayer_Action_60 = z2_Player_Action_60;
    sPlayer_Action_62 = z2_Player_Action_62;
    sPlayer_Action_61 = z2_Player_Action_61;
    sPlayer_Action_82 = z2_Player_Action_82;
    sPlayer_Action_93 = z2_Player_Action_93;
    sPlayer_Action_96 = z2_Player_Action_96;
    sPlayer_UpperAction_CarryAboveHead = z2_Player_UpperAction_CarryAboveHead;
    sFuncPointersInitialized = true;
}

bool Player_BeforeDamageProcess(ActorPlayer* player, GlobalContext* ctxt) {
    return Icetrap_Give(player, ctxt);
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
        if (player->stateFlags.state3 & PLAYER_STATE3_JUMP_ATTACK) {
            player->base.parent = NULL;
        }
        Actor* door = player->doorActor;
        if (door) {
            if (player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP) {
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
                }
            } else if (door->id == ACTOR_EN_DOOR) {
                KnobDoorActor* doorHandle = (KnobDoorActor*)door;
                if (doorHandle->playOpenAnim) {
                    z2_Player_StopCutscene(player);
                }
            }
        }
    }
}

bool Player_HasCustomVictoryCondition() {
    return MISC_CONFIG.internal.victoryFairies
        || MISC_CONFIG.internal.victorySkullTokens
        || MISC_CONFIG.internal.victoryNonTransformMasks
        || MISC_CONFIG.internal.victoryTransformMasks
        || MISC_CONFIG.internal.victoryNotebook
        || MISC_CONFIG.internal.victoryHearts
        || MISC_CONFIG.internal.victoryBossRemainsCount;
}

bool Player_CheckVictory() {
    if (MISC_CONFIG.internal.victoryFairies) {
        if (!MISC_CONFIG.flags.fairyChests) {
            if (gSaveContext.perm.inv.strayFairies[0] < 15
                || gSaveContext.perm.inv.strayFairies[1] < 15
                || gSaveContext.perm.inv.strayFairies[2] < 15
                || gSaveContext.perm.inv.strayFairies[3] < 15) {
                return false;
            }
        }
    }
    if (MISC_CONFIG.internal.victorySkullTokens) {
        if (gSaveContext.perm.skullTokens[0] < 30
            || gSaveContext.perm.skullTokens[1] < 30) {
            return false;
        }
    }
    if (MISC_CONFIG.internal.victoryNonTransformMasks) {
        for (u32 i = 0; i < ARRAY_COUNT(gSaveContext.perm.inv.masks); i++) {
            if (i%6 != 5 && gSaveContext.perm.inv.masks[i] == 0xFF) {
                return false;
            }
        }
    }
    if (MISC_CONFIG.internal.victoryTransformMasks) {
        for (u32 i = 5; i < ARRAY_COUNT(gSaveContext.perm.inv.masks); i += 6) {
            if (gSaveContext.perm.inv.masks[i] == 0xFF) {
                return false;
            }
        }
    }
    if (MISC_CONFIG.internal.victoryNotebook) {
        if (!gSaveContext.perm.inv.questStatus.bombersNotebook) {
            return false;
        }
        u8 eventIndex = 66;
        u8 eventBit = 0;
        for (u8 i = 0; i < 0x37; i++) {
            if (!(gSaveContext.perm.weekEventReg.bytes[eventIndex] & (1 << eventBit))) {
                return false;
            }
            eventBit++;
            if (eventBit > 7) {
                eventBit = 0;
                eventIndex++;
            }
        }
    }
    if (MISC_CONFIG.internal.victoryHearts) {
        if (gSaveContext.perm.unk24.maxLife < 0x140) {
            return false;
        }
    }
    if (MISC_CONFIG.internal.victoryBossRemainsCount) {
        u8 count = 0;
        if (gSaveContext.perm.inv.questStatus.odolwasRemains) {
            count++;
        }
        if (gSaveContext.perm.inv.questStatus.gohtsRemains) {
            count++;
        }
        if (gSaveContext.perm.inv.questStatus.gyorgsRemains) {
            count++;
        }
        if (gSaveContext.perm.inv.questStatus.twinmoldsRemains) {
            count++;
        }
        if (count < MISC_CONFIG.internal.victoryBossRemainsCount) {
            return false;
        }
    }
    return true;
}

void Player_CheckVictoryAndWarp(ActorPlayer* player, GlobalContext* ctxt) {
    if (MISC_CONFIG.internal.victoryDirectToCredits
        && !SAVE_FILE_CONFIG.flags.creditsSeen
        && !(player->stateFlags.state1 & 0x20000000) // TODO better state checking
        && Player_HasCustomVictoryCondition()
        && Player_CheckVictory()) {
        SAVE_FILE_CONFIG.flags.creditsSeen = true;
        ctxt->warpDestination = 0x5400;
        gSaveContext.extra.nextCutsceneIndex = 0xFFF7;
        ctxt->warpType = 20; // TRANS_TRIGGER_START
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
    Player_CheckVictoryAndWarp(player, ctxt);
    if (sInstantTranformTimer) {
        sInstantTranformTimer--;
    }
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
    u16 currentAnimationId = (u32)player->skelAnime.linkAnimetionSeg;
    switch (currentAnimationId) {
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
    bool frozen = player->skelAnime.linkAnimetionSeg == NULL;
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
bool Player_ShouldPreventRestoringSwimState(ActorPlayer* player, GlobalContext* ctxt, PlayerActionFunc function) {
    return function == sPlayer_Action_82;
}

static u32 lastClimbFrame = 0;
static u32 startClimbingTimer = 5;
u32 Player_GetCollisionType(ActorPlayer* player, GlobalContext* ctxt, u32 collisionType) {
    if (MISC_CONFIG.flags.instantTransform && player->base.draw == NULL) {
        // Player is transforming, don't start climbing or the game will crash.
        return 0;
    }

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

void Player_StartTransformation(GlobalContext* ctxt, ActorPlayer* this, s8 actionParam) {
    if (sInstantTranformTimer) {
        return;
    }

    if (!MISC_CONFIG.flags.instantTransform
        || actionParam < PLAYER_IA_MASK_FIERCE_DEITY
        || actionParam > PLAYER_IA_MASK_DEKU
        || this->animTimer != 0
        || (this->talkActor != NULL && this->talkActor->flags & 0x10000)
        || (this->stateFlags.state1 & PLAYER_STATE1_TIME_STOP)
        || (this->stateFlags.state2 & PLAYER_STATE2_DIVING)
        || (this->currentBoots == 4 && this->prevBoots == 5)
        || (this->actionFunc == sPlayer_Action_93)
        || ((u16)(u32)this->skelAnime.linkAnimetionSeg) == 0xE260
            && this->skelAnime.animPlaybackSpeed != 2.0f/3.0f
            && this->skelAnime.animCurrentFrame == 1.5f) {
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
    this->swordActive = 0;

    this->stateFlags.state1 |= PLAYER_STATE1_TIME_STOP_3;

    // really hacky, but necessary to prevent certain softlocks. unkAA5 gets reset back to 0 after transformation.
    this->heldItemActionParam = 0;
    this->unkAA5 = 5;
    this->base.shape.rot.x = 0;

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
        if (this->actionFunc == sPlayer_BackwalkBraking
            || this->actionFunc == sPlayer_Action_96
            || (this->actionFunc == sPlayer_Idle && this->frozenTimer != 0)) {
            z2_Player_func_8083692C(this, ctxt);
        }
        this->stateFlags.state1 &= ~PLAYER_STATE1_TIME_STOP_3;
        this->animTimer = 0;
        sInstantTranformTimer = 2;
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

s32 Player_BeforeActionChange_13(ActorPlayer* this, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.instantTransform && this->base.draw == NULL) {
        return 1;
    }
    if (this->unkAA5 == 0) {
        return -1;
    }
    return 0;
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

void Player_HandleFormSpeed(GlobalContext* ctxt, ActorPlayer* player, f32* speed) {
    // Displaced code:
    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        *speed = *speed * 1.5;
    }
    // End displaced code

    *speed *= GiantMask_GetSimpleScaleModifier();
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
    f32 result = 41.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

// 806F25C0
f32 Player_GetLedgeClimbFactor2() {
    // Displaced code:
    f32 result = 59.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

// 806F2600
f32 Player_GetInvertedLedgeClimbFactor() {
    return 100.0f / GiantMask_GetSimpleScaleModifier();
}

// 806F275C
void Player_ModifyLedgeJumpWallHeight(f32* wallHeight) {
    *wallHeight *= GiantMask_GetSimpleInvertedScaleModifier();
}

// 8070AA40
f32 Player_ModifyWallJumpSpeed(ActorPlayer* this, f32 speed) {
    // VelocityY is multiplied later, but this speed is based on wall height
    speed *= GiantMask_GetSimpleInvertedScaleModifier();

    // Displaced code:
    if (this->form == PLAYER_FORM_HUMAN) {
        speed += 1.0f;
    }
    // End displaced code

    return speed;
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

void Player_SetVelocityY(ActorPlayer* player, f32 yVelocity) {
    yVelocity *= GiantMask_GetSimpleScaleModifier();

    player->base.velocity.y = yVelocity;
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

s32 Player_HandleZoraSwimInputVelocity(f32* pValue, f32 target, f32 incrStep, f32 decrStep) {
    const f32 modifier = GiantMask_GetSimpleScaleModifier();
    incrStep *= modifier;
    // decrStep *= modifier;
    target *= modifier;
    return z2_Math_AsymStepToF(pValue, target, incrStep, decrStep);
}

f32 Player_GetZoraDiveMaxSpeed() {
    // Displaced code:
    f32 result = 13.5f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetZoraDiveGravity() {
    // Displaced code:
    f32 result = -1.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
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

u8 Player_GetMaskOnLoad(ActorPlayer* player, GlobalContext* ctxt) {
    u8 result = gSaveContext.perm.mask;
    if (player->form != PLAYER_FORM_HUMAN) {
        result = player->form + 0x15; // PLAYER_MASK_FIERCE_DEITY
        gSaveContext.perm.mask = 0; // PLAYER_MASK_NONE
    }
    s32 voidFlag = gSaveContext.extra.voidFlag;
    bool shouldResetGiantSize = ctxt->sceneNum == SCENE_INISIE_BS || voidFlag == -5 || voidFlag == 1 || voidFlag == -7;
    if (result == 0x14) {
        if (MISC_CONFIG.flags.giantMaskAnywhere) {
            if (shouldResetGiantSize) {
                gSaveContext.perm.mask = 0;
                result = 0;
            } else {
                gSaveContext.extra.magicConsumeState = 0x0C;
            }
        } else {
            gSaveContext.perm.mask = 0;
            result = 0;
        }
    }

    if (MISC_CONFIG.flags.giantMaskAnywhere) {
        if (shouldResetGiantSize) {
            GiantMask_MarkReset();
        }
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

    // Goron Mask is 0x4F for some reason
    if (item == 0x4F && MISC_CONFIG.flags.ironGoron && (!isGiant || player->mask != 0x14)) {
        return false;
    }

    // Giant's Mask is 0x4D for some reason
    if (item == 0x4D && isGiant && player->form == PLAYER_FORM_HUMAN && MISC_CONFIG.flags.giantMaskAnywhere) {
        return false;
    }
    return true;
}

f32 Player_GetGoronMaxRoll() {
    // Displaced code:
    f32 result = 18.0f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

s32 Player_StepGoronRollRotation(s16* pValue, s32 target, s16 incrStep, s16 decrStep) {
    return z2_Math_AsymStepToS(pValue, target * GiantMask_GetSimpleInvertedScaleModifier(), incrStep, decrStep);
}

f32 Player_GetGoronInitialRollRotatationMultiplier(ActorPlayer* this) {
    return GiantMask_GetSimpleInvertedScaleModifier() * 500.0f;
}

f32 Player_GetGoronRollYawStepMultiplier(ActorPlayer* this) {
    return GiantMask_GetSimpleInvertedScaleModifier() * 20.0f;
}

f32 Player_GetGoronRollReboundSpeedThreshold() {
    return GiantMask_GetSimpleScaleModifier() * 12.0f;
}

void Player_HandleGoronRollSlopeAdjustment(Vec3f* slopeNormal, f32* deltaMovementX, f32* deltaMovementZ, f32* deltaMovementXZ) {
    f32 multiplier = GiantMask_GetSimpleScaleModifier();
    f32 deltaMovementWithSlopeX = (0.6f * multiplier * slopeNormal->x) + *deltaMovementX;
    f32 deltaMovementWithSlopeY = (0.6f * multiplier * slopeNormal->z) + *deltaMovementZ;
    f32 deltaMovementWithSlopeXZ = z2_sqrtf(SQ(deltaMovementWithSlopeX) + SQ(deltaMovementWithSlopeY));

    if ((deltaMovementWithSlopeXZ < *deltaMovementXZ) || (deltaMovementWithSlopeXZ < 6.0f * multiplier)) {
        *deltaMovementX = deltaMovementWithSlopeX;
        *deltaMovementZ = deltaMovementWithSlopeY;
        *deltaMovementXZ = deltaMovementWithSlopeXZ;
    }
}

f32 Player_GetGoronRollAutoRollThreshold() {
    // Displaced code:
    f32 result = 0.3f;
    // End displaced code

    result *= GiantMask_GetSimpleScaleModifier();

    return result;
}

f32 Player_GetGoronRollSoundFactor() {
    // Displaced code:
    f32 result = 800.0f;
    // End displaced code

    result *= GiantMask_GetSimpleInvertedScaleModifier();

    return result;
}

bool Player_IsAboveBonkThreshold(GlobalContext* ctxt, ActorPlayer* player, f32* velocity, f32 threshold) {
    return *velocity > (threshold * GiantMask_GetSimpleScaleModifier());
}

void Player_SetGoronPoundGravity(ActorPlayer* this, f32* gravityAtPeak) {
    f32 gravity;
    f32 peakThreshold = -GiantMask_GetSimpleScaleModifier();
    if (this->base.velocity.y > peakThreshold) {
        gravity = *gravityAtPeak;
    } else {
        this->unk_3D0.unk_00 = 1;
        gravity = -10.0f;
    }
    this->base.gravity = gravity * GiantMask_GetSimpleScaleModifier();
}

Actor* Player_SpawnCrater(ActorContext* actorCtxt, GlobalContext* ctxt, u16 id, f32 x, f32 y, f32 z, u16 rx, u16 ry, u16 rz, u16 params) {
    if (GiantMask_IsGiant()) {
        if (params == 0) {
            params = 0x2400;
        } else if (params == 500) {
            params = 5000;
        }
    }
    return z2_SpawnActor(actorCtxt, ctxt, id, x, y, z, rx, ry, rz, params);
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
    if (GiantMask_IsGiant()) {
        return DMG_POWDER_KEG | DMG_EXPLOSIVES | DMG_GORON_PUNCH | DMG_UNK_0x1E; // DMG_UNK_0x1E is used to trigger different damage calculation algorithm.
    }
    return dmgFlags;
}

bool Player_ShouldBeKnockedOver(GlobalContext* ctxt, ActorPlayer* player, s32 damageType) {
    if (GiantMask_IsGiant()) {
        return false;
    }

    if (MISC_CONFIG.flags.takeDamageOnEpona && (player->stateFlags.state1 & PLAYER_STATE1_EPONA)) {
        s32* gHorseIsMounted = (s32*)0x801BDA9C;
        z2_Camera_ChangeSetting(z2_Play_GetCamera(ctxt, 0), 1); // CAM_ID_MAIN, CAM_SET_NORMAL0
        player->stateFlags.state1 &= ~PLAYER_STATE1_EPONA;
        player->base.parent = NULL;
        *gHorseIsMounted = false;
        return true;
    }

    // Displaced code:
    return damageType == 1
        || damageType == 2
        || !(player->base.bgcheckFlags & 1) // BGCHECKFLAG_GROUND
        || (player->stateFlags.state1 & (PLAYER_STATE1_LEDGE_CLIMB | PLAYER_STATE1_LEDGE_HANG | PLAYER_STATE1_CLIMB_UP | PLAYER_STATE1_LADDER));
}

bool Player_ShouldSkipParentDamageCheck(ActorPlayer* player) {
    return MISC_CONFIG.flags.takeDamageOnEpona
        && (player->stateFlags.state1 & PLAYER_STATE1_EPONA)
        && (WEEKEVENTREG(92) & 7) == 0 // Not Gorman Race
        && (player->actionFunc == sPlayer_Action_52 || player->actionFunc == sPlayer_Action_53); // Mounting/Mounted/Dismounting
}

bool Player_CantBeGrabbed(GlobalContext* ctxt, ActorPlayer* player) {
    if (GiantMask_IsGiant()) {
        return true;
    }

    // Displaced code:
    return z2_Player_InBlockingCsMode(ctxt, player);
}

f32 Player_GetLinearVelocityForLimbRotation(ActorPlayer* player) {
    return player->linearVelocity * GiantMask_GetSimpleInvertedScaleModifier();
}

void Player_SetGiantMaskTransformationState(GlobalContext* ctxt, ActorPlayer* player) {
    u32 newState = PLAYER_STATE1_TIME_STOP | PLAYER_STATE1_GIANT_MASK;
    if (MISC_CONFIG.flags.giantMaskAnywhere && ctxt->sceneNum != SCENE_INISIE_BS) {
        newState |= PLAYER_STATE1_SPECIAL_2;
    }
    player->stateFlags.state1 |= newState;
}

static const u8* sAudioBaseFilter = (u8*)0x801D66E0;

void Player_HandleIronGoronLand(GlobalContext* ctxt, ActorPlayer* player) {
    if (player->base.id != 0) {
        return;
    }

    if (player->form == PLAYER_FORM_GORON && MISC_CONFIG.flags.ironGoron && *sAudioBaseFilter == 0x20) {
        player->stateFlags.state2 &= ~PLAYER_STATE2_DIVING_2;
        z2_PerformEnterWaterEffects(ctxt, player);
        z2_Player_SetBootData(ctxt, player);
    }

    // Displaced code
    z2_801A3E38(0);
}

s8 Player_HandleGoronInWater(GlobalContext* ctxt, ActorPlayer* player) {
    if (player->form == PLAYER_FORM_GORON) {
        if (MISC_CONFIG.flags.ironGoron) {
            if (!(player->stateFlags.state2 & PLAYER_STATE2_DIVING_2)) {
                if ((sPlayer_Action_43 != player->actionFunc) && (sPlayer_Action_61 != player->actionFunc) &&
                    (sPlayer_Action_62 != player->actionFunc) && (sPlayer_Action_54 != player->actionFunc) &&
                    (sPlayer_Action_57 != player->actionFunc) && (sPlayer_Action_58 != player->actionFunc) &&
                    (sPlayer_Action_59 != player->actionFunc) && (sPlayer_Action_60 != player->actionFunc) &&
                    (sPlayer_Action_55 != player->actionFunc) && (sPlayer_Action_56 != player->actionFunc)) {
                    z2_Player_func_8083B930(ctxt, player);
                    player->stateFlags.state1 &= ~PLAYER_STATE1_SWIM;
                }
            }
            return 1;
        }
        return 0;
    }
    return -1;
}

bool Player_ShouldResetUnderwaterTimer(GlobalContext* ctxt, ActorPlayer* player) {
    if (player->form == PLAYER_FORM_GORON && (player->stateFlags.state1 & PLAYER_STATE1_SWIM) && MISC_CONFIG.flags.ironGoron) {
        player->stateFlags.state1 &= ~PLAYER_STATE1_SWIM;
    }

    // Displaced code
    z2_801A3E38(0x20);

    return player->form == PLAYER_FORM_ZORA || (player->form == PLAYER_FORM_GORON && MISC_CONFIG.flags.ironGoron);
}

Actor* Player_GetGoronPunchCollisionActor(CollisionContext* colCtx, s32 bgId) {
    Actor* actor = z2_DynaPoly_GetActor(colCtx, bgId);

    // Let Iron Goron punch open the planks blocking Pirates Fortress Exterior.
    if (MISC_CONFIG.flags.ironGoron && actor->id == ACTOR_OBJ_TARU && (actor->params & 0x80)) {
        actor->initPosRot.rot.z = 1;
    }

    return actor;
}

bool Player_ShouldNotSetGlobalVoidFlag(CollisionContext* colCtx, BgPolygon* poly, s32 bgId) {
    return gSaveContext.extra.voidFlag == -7 || z2_SurfaceType_IsWallDamage(colCtx, poly, bgId);
}

Actor* Player_GetHittingActor(ActorPlayer* player) {
    if (MISC_CONFIG.flags.takeDamageOnEpona && player->base.colChkInfo.acHitEffect == 2 && (player->stateFlags.state1 & PLAYER_STATE1_EPONA)) {
        player->base.colChkInfo.acHitEffect = 0;
        if (player->base.colChkInfo.damage == 0) {
            player->base.colChkInfo.damage = 4;
        }
    }
    if (player->collisionCylinder.base.flagsAC & AC_HIT) {
        return player->collisionCylinder.base.collisionAC;
    }
    if (MISC_CONFIG.flags.takeDamageOnShield) {
        if (player->shieldQuad.base.flagsAC & AC_HIT) {
            return player->shieldQuad.base.collisionAC;
        }
        if (player->shieldCylinder.base.flagsAC & AC_HIT) {
            return player->shieldCylinder.base.collisionAC;
        }
    }
    return NULL;
}

void Player_ForceInflictDamage(GlobalContext* ctxt, ActorPlayer* player, s32 damage) {
    u32 flag = player->stateFlags.state2 & PLAYER_STATE2_LIFT_ACTOR;
    player->stateFlags.state2 |= PLAYER_STATE2_LIFT_ACTOR;
    z2_Player_InflictDamage(ctxt, -16);
    if (!flag) {
        player->stateFlags.state2 &= ~PLAYER_STATE2_LIFT_ACTOR;
    }
}

void Player_OnMinorVoid(GlobalContext* ctxt, ActorPlayer* player) {
    // Displaced code:
    z2_Player_SetEquipmentData(ctxt, player);
    // End displaced code

    if (MISC_CONFIG.flags.takeDamageFromVoid) {
        Player_ForceInflictDamage(ctxt, player, -16);
        player->unk_D6A = -2;
    }
}

void Player_OnDekuWaterVoid(GlobalContext* ctxt, ActorPlayer* player) {
    // Displaced code:
    z2_PerformEnterWaterEffects(ctxt, player);
    // End displaced code

    if (ctxt->warpType) {
        Player_ForceInflictDamage(ctxt, player, -16);
    }
}

void Player_VoidExit(u16 sfxId) {
    z2_PlaySfx_2(sfxId);

    if (MISC_CONFIG.flags.takeDamageFromVoid && gGlobalContext.sceneNum != SCENE_BOTI) {
        Player_ForceInflictDamage(&gGlobalContext, GET_PLAYER(&gGlobalContext), -16);
    }
}
