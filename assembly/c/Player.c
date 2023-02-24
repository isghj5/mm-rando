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
#include "GiantMask.h"

bool Player_BeforeDamageProcess(ActorPlayer* player, GlobalContext* ctxt) {
    return Icetrap_Give(player, ctxt);
}

void Player_BeforeUpdate(ActorPlayer* player, GlobalContext* ctxt) {
    Dpad_BeforePlayerActorUpdate(player, ctxt);
    ExternalEffects_Handle(player, ctxt);
    ArrowCycle_Handle(player, ctxt);
    ArrowMagic_Handle(player, ctxt);
    DekuHop_Handle(player, ctxt);
    GiantMask_Handle(player, ctxt);
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

void Player_StartTransformation(GlobalContext* ctxt, ActorPlayer* this, s8 actionParam) {
    if (!MISC_CONFIG.flags.instantTransform || actionParam < PLAYER_IA_MASK_FIERCE_DEITY || actionParam > PLAYER_IA_MASK_DEKU) {
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
}

bool Player_AfterTransformInit() {
    return MISC_CONFIG.flags.instantTransform;
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
