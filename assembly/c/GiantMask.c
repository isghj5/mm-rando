#include "GiantMask.h"
#include "Misc.h"
#include "macro.h"
#include "controller.h"
#include "Reloc.h"
#include "Camera.h"
#include "SaveFile.h"

static void GiantMask_FormProperties_Grow(PlayerFormProperties* formProperties) {
    formProperties->unk_00 *= 10.0;
    formProperties->unk_0C *= 10.0;
    formProperties->unk_10 *= 10.0;
    formProperties->unk_14 *= 10.0;
    formProperties->unk_18 *= 10.0;
    formProperties->unk_1C *= 10.0;
    formProperties->unk_24 *= 10.0;
    formProperties->unk_28 *= 10.0;
    formProperties->unk_2C *= 10.0;
    formProperties->unk_30 *= 10.0;
    formProperties->unk_34 *= 10.0;
    formProperties->unk_38 *= 10.0;
    formProperties->unk_3C *= 10.0;
    formProperties->unk_40 *= 10.0;
}

static void GiantMask_FormProperties_Shrink(PlayerFormProperties* formProperties) {
    formProperties->unk_00 *= 0.1;
    formProperties->unk_0C *= 0.1;
    formProperties->unk_10 *= 0.1;
    formProperties->unk_14 *= 0.1;
    formProperties->unk_18 *= 0.1;
    formProperties->unk_1C *= 0.1;
    formProperties->unk_24 *= 0.1;
    formProperties->unk_28 *= 0.1;
    formProperties->unk_2C *= 0.1;
    formProperties->unk_30 *= 0.1;
    formProperties->unk_34 *= 0.1;
    formProperties->unk_38 *= 0.1;
    formProperties->unk_3C *= 0.1;
    formProperties->unk_40 *= 0.1;
}

static void GiantMask_Reg_Grow() {
    //REG(27);        // turning circle
    REG(48) *= 10; // slow backwalk animation threshold
    REG(19) *= 10;  // run acceleration // deceleration needs hook 806F9FE8
    // REG(30) /= 10; // base sidewalk animation speed
    REG(32) /= 10; // sidewalk animation speed multiplier
    // REG(34) *= 10; // ? unused ?
    // REG(35) *= 10; // base slow backwalk
    REG(36) /= 10; // slow backwalk animation speed multiplier
    REG(37) /= 10; // walk speed threshold
    REG(38) /= 10; // walk animation speed multiplier
    // REG(39) *= 10; // base walk animation speed
    REG(43) *= 10;  // idle deceleration
    REG(45) *= 10;  // running speed
    REG(68) *= 10;  // gravity
    // REG(69) *= 10;  // jump strength
    IREG(66) *= 10; // baby jump threshold
    // IREG(67) *= 10; // normal jump speed
    // IREG(68) *= 10; // baby jump base speed
    IREG(69) /= 10; // baby jump speed multiplier
    MREG(95) /= 10; // bow sidewalk animation?
}

static void GiantMask_SetTransformationFlash(GlobalContext* globalCtx, u8 r, u8 g, u8 b, u8 a) {
    MREG(64) = 1;
    MREG(65) = r;
    MREG(66) = g;
    MREG(67) = b;
    MREG(68) = a;
}

static void GiantMask_SetTransformationFlashAlpha(GlobalContext* globalCtx, u8 alpha) {
    MREG(68) = alpha;
}

static void GiantMask_DisableTransformationFlash(GlobalContext* globalCtx) {
    MREG(64) = 0;
}

/* 0x1D14 */ static u32 sGiantsMaskCsTimer;
/* 0x1D18 */ static s16 sGiantsMaskCsState;
/* 0x1D22 */ static s16 sSubCamId;
/* 0x1D24 */ static Vec3f sSubCamEye;
/* 0x1D30 */ static Vec3f sSubCamAt;
/* 0x1D3C */ static Vec3f sSubCamUp;
/* 0x1D54 */ static f32 sSubCamUpRotZ;
/* 0x1D58 */ static f32 sSubCamUpRotZScale;
/* 0x1D5C */ static f32 sSubCamAtVel;
/* 0x1D64 */ static f32 sSubCamDistZFromPlayer;
/* 0x1D68 */ const static f32 sSubCamEyeOffsetY = 10.0f;
/* 0x1D6C */ static f32 sSubCamAtOffsetY;
             static f32 sSubCamAtOffsetTargetY;
/* 0x1D70 */ static f32 sPlayerScale = 0.01f;
/* 0x1D78 */ static u8 sGiantsMaskCsFlashState;
/* 0x1D7A */ static s16 sGiantsMaskCsFlashAlpha;

static bool sHasSeenGrowCutscene;
static bool sHasSeenShrinkCutscene;
static f32 sNextScaleFactor = 10.0f;

static bool sShouldReset = false;

void GiantMask_Handle(ActorPlayer* player, GlobalContext* globalCtx) {
    if (!MISC_CONFIG.flags.giantMaskAnywhere) {
        return;
    }

    if (globalCtx->sceneNum == SCENE_INISIE_BS) {
        return;
    }

    s16 i;
    Vec3f subCamEyeOffset;
    bool performMainSwitch = false;
    s16 alpha;

    sGiantsMaskCsTimer++;

    switch (sGiantsMaskCsState) {
        case 0:
            if (player->stateFlags.state1 & PLAYER_STATE1_GIANT_MASK) {
                bool isShielding = player->stateFlags.state1 & PLAYER_STATE1_SHIELD;
                if (!(player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP) || isShielding) {
                    if (isShielding) {
                        player->heldItemActionParam = 0;
                    }
                    z2_PlayerWaitForGiantMask(globalCtx, player);
                    if (isShielding) {
                        player->heldItemActionParam = -1;
                    }
                }
                // z2_800EA0D4(globalCtx, &globalCtx->csCtx);
                sSubCamId = z2_Play_CreateSubCamera(globalCtx);
                z2_Play_CameraChangeStatus(globalCtx, 0, 1); // CAM_ID_MAIN, CAM_STATUS_WAIT
                z2_Play_CameraChangeStatus(globalCtx, sSubCamId, 7); // CAM_STATUS_ACTIVE
                z2_Play_EnableMotionBlur(150); // enable motion blur
                sGiantsMaskCsTimer = 0;
                sSubCamAtVel = 0.0f;
                sSubCamUpRotZScale = 0.0f;
                f32 playerHeight = Camera_PlayerGetHeight(player);
                if (!GiantMask_IsGiant()) {
                    sGiantsMaskCsState = 1;
                    sSubCamDistZFromPlayer = 60.0f;
                    sSubCamAtOffsetY = playerHeight * 0.53f; // 23.0f;
                    sSubCamAtOffsetTargetY = playerHeight * 6.2f; // 273.0f;
                    sPlayerScale = 0.01f;
                    goto maskOn;
                } else {
                    sGiantsMaskCsState = 10;
                    sSubCamDistZFromPlayer = 200.0f;
                    sSubCamAtOffsetY = playerHeight * 0.62f; // 273.0f;
                    sSubCamAtOffsetTargetY = playerHeight * 0.053f; // 23.0f;
                    sPlayerScale = 0.1f;
                    goto maskOff;
                }
            }
            break;

        case 1:
            if ((sGiantsMaskCsTimer < 80U) && sHasSeenGrowCutscene &&
                CHECK_BTN_ANY(CONTROLLER1(globalCtx)->pressEdge.buttons.value,
                              BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
                sGiantsMaskCsState++;
                sGiantsMaskCsFlashState = 1;
                sGiantsMaskCsTimer = 0;
            } else {
            maskOn:
                if (sGiantsMaskCsTimer >= 50U) {
                    if (sGiantsMaskCsTimer == (u32)(BREG(43) + 60)) {
                        z2_PlaySfx(0x9C5); // NA_SE_PL_TRANSFORM_GIANT
                    }
                    Math_ApproachF(&sSubCamDistZFromPlayer, 200.0f, 0.1f, sSubCamAtVel * 640.0f);
                    Math_ApproachF(&sSubCamAtOffsetY, sSubCamAtOffsetTargetY, 0.1f, sSubCamAtVel * 150.0f);
                    Math_ApproachF(&sPlayerScale, 0.1f, 0.2f, sSubCamAtVel * 0.1f);
                    Math_ApproachF(&sSubCamAtVel, 1.0f, 1.0f, 0.001f);
                } else {
                    Math_ApproachF(&sSubCamDistZFromPlayer, 30.0f, 0.1f, 1.0f);
                }

                if (sGiantsMaskCsTimer > 50U) {
                    Math_ApproachZeroF(&sSubCamUpRotZScale, 1.0f, 0.06f);
                } else {
                    Math_ApproachF(&sSubCamUpRotZScale, 0.4f, 1.0f, 0.02f);
                }

                if (sGiantsMaskCsTimer == 107U) {
                    sGiantsMaskCsFlashState = 1;
                }

                if (sGiantsMaskCsTimer < 121U) {
                    break;
                }

                performMainSwitch = true;
                sHasSeenGrowCutscene = true;
                goto done;
            }
            break;

        case 2:
            if (sGiantsMaskCsTimer < 8U) {
                break;
            }
            performMainSwitch = true;
            goto done;

        case 10:
            if ((sGiantsMaskCsTimer < 30U) && sHasSeenShrinkCutscene &&
                CHECK_BTN_ANY(CONTROLLER1(globalCtx)->pressEdge.buttons.value,
                              BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
                sGiantsMaskCsState++;
                sGiantsMaskCsFlashState = 1;
                sGiantsMaskCsTimer = 0;
                break;
            }

        maskOff:
            if (sGiantsMaskCsTimer != 0U) {
                if (sGiantsMaskCsTimer == (u32)(BREG(44) + 10)) {
                    z2_PlaySfx(0x9C6); // NA_SE_PL_TRANSFORM_NORMAL
                }
                Math_ApproachF(&sSubCamDistZFromPlayer, 60.0f, 0.1f, sSubCamAtVel * 640.0f);
                Math_ApproachF(&sSubCamAtOffsetY, sSubCamAtOffsetTargetY, 0.1f, sSubCamAtVel * 150.0f);
                Math_ApproachF(&sPlayerScale, 0.01f, 0.1f, 0.003f);
                Math_ApproachF(&sSubCamAtVel, 2.0f, 1.0f, 0.01f);
            }

            if (sGiantsMaskCsTimer == 42U) {
                sGiantsMaskCsFlashState = 1;
            }

            if (sGiantsMaskCsTimer > 50U) {
                performMainSwitch = true;
                sHasSeenShrinkCutscene = true;
                goto done;
            }
            break;

        case 11:
            if (sGiantsMaskCsTimer < 8U) {
                break;
            }
            performMainSwitch = true;

        done:
        case 20:
            // sGiantsMaskCsState = 0;
            sGiantsMaskCsState = 21;
            z2_80169AFC(globalCtx, sSubCamId, 0);
            sSubCamId = 0;
            // z2_800EA0EC(globalCtx, &globalCtx->csCtx);
            //actor.flags |= 1;
            player->stateFlags.state1 &= ~PLAYER_STATE1_GIANT_MASK;
            //sPlayerScale = 0.01f;
            z2_Play_DisableMotionBlur();
            player->swordActive = 0;
            break;
        case 21:
            sGiantsMaskCsState = 0;
            if (GiantMask_IsGiant()) {
                GiantMask_Reg_Grow();
            }
            break;
    }

    if (performMainSwitch) {
        GiantMask_SetIsGiant(!GiantMask_IsGiant());
        if (!GiantMask_IsGiant()) {
            sPlayerScale = 0.01f;
            sNextScaleFactor = 10.0f;
        } else {
            sPlayerScale = 0.1f;
            sNextScaleFactor = 0.1f;
        }
    }

    if (player->mask == 0x14 && player->currentBoots == 1) {
        gSaveContext.extra.magicConsumeState = 0xC;
        player->currentBoots = 2;
        z2_Player_SetBootData(globalCtx, player);
    } else if (player->mask != 0x14 && player->currentBoots == 2) {
        player->currentBoots = 1;
        z2_Player_SetBootData(globalCtx, player);
    }

    if (GiantMask_IsGiant()) {
        if (player->formProperties->unk_00 < 200.0) {
            GiantMask_FormProperties_Grow(player->formProperties);
            player->base.flags |= (1 << 17); // ACTOR_FLAG_CAN_PRESS_HEAVY_SWITCH
        }
        if (REG(68) > -200) {
            GiantMask_Reg_Grow();
        }
    }
    else if (player->formProperties->unk_00 >= 200.0) {
        GiantMask_FormProperties_Shrink(player->formProperties);
        player->base.flags &= ~(1 << 17); // ~ACTOR_FLAG_CAN_PRESS_HEAVY_SWITCH
    }

    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        z2_SetActorSize(&player->base, sPlayerScale * 1.5f);
    } else {
        z2_SetActorSize(&player->base, sPlayerScale);
    }

    switch (sGiantsMaskCsFlashState) {
        case 0:
            break;

        case 1:
            sGiantsMaskCsFlashAlpha = 0;
            GiantMask_SetTransformationFlash(globalCtx, 255, 255, 255, 0);
            sGiantsMaskCsFlashState = 2;
            z2_PlaySfx(0x484F); // NA_SE_SY_TRANSFORM_MASK_FLASH

        case 2:
            sGiantsMaskCsFlashAlpha += 40;
            if (sGiantsMaskCsFlashAlpha >= 400) {
                sGiantsMaskCsFlashState = 3;
            }
            alpha = sGiantsMaskCsFlashAlpha;
            if (alpha > 255) {
                alpha = 255;
            }
            GiantMask_SetTransformationFlashAlpha(globalCtx, alpha);
            break;

        case 3:
            sGiantsMaskCsFlashAlpha -= 40;
            if (sGiantsMaskCsFlashAlpha <= 0) {
                sGiantsMaskCsFlashAlpha = 0;
                sGiantsMaskCsFlashState = 0;
                GiantMask_DisableTransformationFlash(globalCtx);
            } else {
                alpha = sGiantsMaskCsFlashAlpha;
                if (alpha > 255) {
                    alpha = 255;
                }
                GiantMask_SetTransformationFlashAlpha(globalCtx, alpha);
            }
            break;
    }

    if ((sGiantsMaskCsState != 0) && (sSubCamId != 0)) {
        // prevent all other cutscenes from starting
        z2_ActorCutscene_ClearNextCutscenes();

        z2_Matrix_RotateY(player->base.shape.rot.y, 0); // MTXMODE_NEW
        z2_Matrix_GetStateTranslationAndScaledZ(sSubCamDistZFromPlayer, &subCamEyeOffset);

        sSubCamEye.x = player->base.currPosRot.pos.x + subCamEyeOffset.x;
        sSubCamEye.y = player->base.currPosRot.pos.y + subCamEyeOffset.y + sSubCamEyeOffsetY;
        sSubCamEye.z = player->base.currPosRot.pos.z + subCamEyeOffset.z;

        sSubCamAt.x = player->base.currPosRot.pos.x;
        sSubCamAt.y = player->base.currPosRot.pos.y + sSubCamAtOffsetY;
        sSubCamAt.z = player->base.currPosRot.pos.z;

        sSubCamUpRotZ = z2_Math_SinS(sGiantsMaskCsTimer * 1512) * sSubCamUpRotZScale;
        z2_Matrix_InsertZRotation_f(sSubCamUpRotZ, 1); // MTXMODE_APPLY
        z2_Matrix_GetStateTranslationAndScaledY(1.0f, &sSubCamUp);
        z2_Play_CameraSetAtEyeUp(globalCtx, sSubCamId, &sSubCamAt, &sSubCamEye, &sSubCamUp);
        ShrinkWindow_SetLetterboxTarget(0x1B);
    }
}

f32 GiantMask_GetScaleModifier() {
    return sPlayerScale * 100.0f;
}

f32 GiantMask_GetSimpleScaleModifier() {
    return GiantMask_IsGiant() ? 10.0f : 1.0f;
}

f32 GiantMask_GetSimpleInvertedScaleModifier() {
    return GiantMask_IsGiant() ? 0.1f : 1.0f;
}

f32 GiantMask_GetNextScaleFactor() {
    return sNextScaleFactor;
}

f32 GiantMask_GetFloorHeightCheckDelta(GlobalContext* globalCtx, Actor* actor, Vec3f* pos, s32 flags) {
    // Displaced code:
    f32 result = (flags & 0x800) ? 10.0f : 50.0f;
    // End displaced code

    if (actor->id == ACTOR_PLAYER) {
        result *= GiantMask_GetSimpleScaleModifier();
    }
    return result;
}

f32 GiantMask_GetLedgeWalkOffHeight(Actor* actor) {
    // Displaced code:
    f32 result = -11.0;
    // End displaced code

    if (actor->id == ACTOR_PLAYER) {
        result *= GiantMask_GetSimpleScaleModifier();
    }

    return result;
}

f32 GiantMask_Math_SmoothStepToF(f32* pValue, f32 target, f32 fraction, f32 step, f32 minStep) {
    const f32 modifier = GiantMask_GetSimpleScaleModifier();

    target *= modifier;
    step *= modifier;
    minStep *= modifier;

    return z2_Math_SmoothStepToF(pValue, target, fraction, step, minStep);
}

f32 GiantMask_Math_StepToF(f32* pValue, f32 target, f32 step) {
    return z2_Math_StepToF(pValue, target, step * GiantMask_GetSimpleScaleModifier());
}

f32 GiantMask_GetScaledFloat(f32 value) {
    return value * GiantMask_GetSimpleScaleModifier();
}

f32 GiantMask_GetInvertedScaledFloat(f32 value) {
    return value * GiantMask_GetSimpleInvertedScaleModifier();
}

void GiantMask_AdjustSpinAttackHeight(Actor* actor, ColCylinder* collider) {
    collider->params.height *= GiantMask_GetSimpleScaleModifier();

    // Displaced code:
    z2_Collider_UpdateCylinder(actor, collider);
    // End displaced code
}

bool GiantMask_IsGiant() {
    return SAVE_FILE_CONFIG.flags.isGiant;
}

void GiantMask_SetIsGiant(bool value) {
    SAVE_FILE_CONFIG.flags.isGiant = value;
}

void GiantMask_MarkReset() {
    sShouldReset = true;
}

void GiantMask_TryReset() {
    if (sShouldReset) {
        sPlayerScale = 0.01f;
        GiantMask_SetIsGiant(false);
        sNextScaleFactor = 10.0f;
        sShouldReset = false;
    }
}

void GiantMask_ClearState() {
    sGiantsMaskCsState = 0;
    sSubCamId = 0;
    sPlayerScale = GiantMask_IsGiant() ? 0.1f : 0.01f;
}

f32 GiantMask_GetHitDistance(Vec3f* position, Actor* hittingActor) {
    if (hittingActor->id == ACTOR_PLAYER) {
        if (GiantMask_IsGiant()) {
            return 0.0f;
        }
    }
    return z2_Math3D_Vec3fDistSq(position, &hittingActor->currPosRot.pos);
}

/*
C Buttons Anywhere
E9738CE0 595A
E9738CDE 595A
*/

// 80382530 REG

// TODO
// fall height damage // function at 806F43A0
// climb out of water 80700C50 1770, 80701248 43FA, 80701260 4348
