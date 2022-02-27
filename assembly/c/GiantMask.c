#include "GiantMask.h"
#include "Misc.h"
#include "macro.h"
#include "controller.h"
#include "Reloc.h"

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

static void GiantMask_Reg_Grow(bool fromGiantTransformation) {
    REG(19) *= 10;  // run acceleration // deceleration needs hook 806F9FE8
    //REG(27);        // turning circle
    REG(30) = 0x40; // sidewalk animation speed
    REG(32) = 0x40; // sidewalk animation speed
    REG(38) = 0x20; // walk animation speed
    REG(39) = 0x20; // walk animation speed
    REG(43) *= 10;  // idle deceleration
    REG(45) *= 10;  // running speed
    REG(68) *= 10;  // gravity
    IREG(67) *= 10; // jump strength // read at 806F48EC

    if (!fromGiantTransformation) {
        REG(19) *= 0.5;             // running acceleration
        REG(45) *= 7.0f / 11.0f;    // running speed
    }
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

/* 0x1D14 */ static u32 _cutsceneCounter;
/* 0x1D18 */ static s16 _transformationState;
/* 0x1D22 */ static s16 _transformationCameraId;
/* 0x1D24 */ static Vec3f _unk_1D24;
/* 0x1D30 */ static Vec3f _unk_1D30;
/* 0x1D3C */ static Vec3f _unk_1D3C;
/* 0x1D54 */ static f32 _unk_1D54;
/* 0x1D58 */ static f32 _unk_1D58;
/* 0x1D5C */ static f32 _unk_1D5C;
/* 0x1D64 */ static f32 _unk_1D64;
/* 0x1D68 */ static f32 _unk_1D68;
/* 0x1D6C */ static f32 _unk_1D6C;
/* 0x1D70 */ static f32 _scale = 0.01f;
/* 0x1D78 */ static u8 _transformationFlashState;
/* 0x1D7A */ static s16 _transformationFlashCounter;

static bool _hasSeenGrowCutscene;
static bool _hasSeenShrinkCutscene;
static bool _isGiant;
static f32 _nextScaleFactor = 10.0f;

static bool _shouldReset = false;

void GiantMask_Handle(ActorPlayer* player, GlobalContext* globalCtx) {
    if (!MISC_CONFIG.flags.giantMaskAnywhere) {
        return;
    }

    if (globalCtx->sceneNum == 0x36) {
        return;
    }

    s16 i;
    Vec3f sp58;
    u8 sp57 = 0;
    s16 transformationFlashOpacity;

    _cutsceneCounter++;

    switch (_transformationState) {
        case 0:
            if (player->stateFlags.state1 & PLAYER_STATE1_GIANT_MASK) {
                if (!(player->stateFlags.state1 & PLAYER_STATE1_TIME_STOP)) {
                    z2_PlayerWaitForGiantMask(globalCtx, player);
                }
                // z2_800EA0D4(globalCtx, &globalCtx->csCtx);
                _transformationCameraId = z2_Play_CreateSubCamera(globalCtx);
                z2_Play_CameraChangeStatus(globalCtx, 0, 1);
                z2_Play_CameraChangeStatus(globalCtx, _transformationCameraId, 7);
                z2_8016566C(150); // enable motion blur
                _cutsceneCounter = 0;
                _unk_1D5C = 0.0f;
                _unk_1D58 = 0.0f;
                if (!_isGiant) {
                    _transformationState = 1;
                    _unk_1D68 = 10.0f;
                    _unk_1D64 = 60.0f;
                    _unk_1D6C = 23.0f;
                    _scale = 0.01f;
                    goto label1;
                } else {
                    _transformationState = 10;
                    _unk_1D68 = 10.0f;
                    _unk_1D64 = 200.0f;
                    _unk_1D6C = 273.0f;
                    _scale = 0.1f;
                    goto label2;
                }
            }
            break;

        case 1:
            if ((_cutsceneCounter < 80U) && _hasSeenGrowCutscene &&
                CHECK_BTN_ANY(CONTROLLER1(globalCtx)->pressEdge.buttons.value,
                              BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
                _transformationState++;
                _transformationFlashState = 1;
                _cutsceneCounter = 0;
            } else {
            label1:
                if (_cutsceneCounter >= 50U) {
                    if (_cutsceneCounter == (u32)(BREG(43) + 60)) {
                        z2_PlaySfx(0x9C5); // NA_SE_PL_TRANSFORM_GIANT
                    }
                    Math_ApproachF(&_unk_1D64, 200.0f, 0.1f, _unk_1D5C * 640.0f);
                    Math_ApproachF(&_unk_1D6C, 273.0f, 0.1f, _unk_1D5C * 150.0f);
                    Math_ApproachF(&_scale, 0.1f, 0.2f, _unk_1D5C * 0.1f);
                    Math_ApproachF(&_unk_1D5C, 1.0f, 1.0f, 0.001f);
                } else {
                    Math_ApproachF(&_unk_1D64, 30.0f, 0.1f, 1.0f);
                }

                if (_cutsceneCounter > 50U) {
                    Math_ApproachZeroF(&_unk_1D58, 1.0f, 0.06f);
                } else {
                    Math_ApproachF(&_unk_1D58, 0.4f, 1.0f, 0.02f);
                }

                if (_cutsceneCounter == 107U) {
                    _transformationFlashState = 1;
                }

                if (_cutsceneCounter < 121U) {
                    break;
                }

                sp57 = 1;
                _hasSeenGrowCutscene = true;
                goto block_38;
            }
            break;

        case 2:
            if (_cutsceneCounter < 8U) {
                break;
            }
            sp57 = 1;
            goto block_38;

        case 10:
            if ((_cutsceneCounter < 30U) && _hasSeenShrinkCutscene &&
                CHECK_BTN_ANY(CONTROLLER1(globalCtx)->pressEdge.buttons.value,
                              BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
                _transformationState++;
                _transformationFlashState = 1;
                _cutsceneCounter = 0;
                break;
            }

        label2:
            if (_cutsceneCounter != 0U) {
                if (_cutsceneCounter == (u32)(BREG(44) + 10)) {
                    z2_PlaySfx(0x9C6); // NA_SE_PL_TRANSFORM_NORMAL
                }
                Math_ApproachF(&_unk_1D64, 60.0f, 0.1f, _unk_1D5C * 640.0f);
                Math_ApproachF(&_unk_1D6C, 23.0f, 0.1f, _unk_1D5C * 150.0f);
                Math_ApproachF(&_scale, 0.01f, 0.1f, 0.003f);
                Math_ApproachF(&_unk_1D5C, 2.0f, 1.0f, 0.01f);
            }

            if (_cutsceneCounter == 42U) {
                _transformationFlashState = 1;
            }

            if (_cutsceneCounter > 50U) {
                sp57 = 1;
                _hasSeenShrinkCutscene = true;
                goto block_38;
            }
            break;

        case 11:
            if (_cutsceneCounter < 8U) {
                break;
            }
            sp57 = 1;

        block_38:
        case 20:
            // _transformationState = 0;
            _transformationState = 21;
            z2_80169AFC(globalCtx, _transformationCameraId, 0);
            _transformationCameraId = 0;
            // z2_800EA0EC(globalCtx, &globalCtx->csCtx);
            //actor.flags |= 1;
            player->stateFlags.state1 &= ~PLAYER_STATE1_GIANT_MASK;
            //_scale = 0.01f;
            z2_80165690(); // disable motion blur
            break;
        case 21:
            _transformationState = 0;
            if (_isGiant) {
                GiantMask_Reg_Grow(player->mask == 0x14);
            }
            break;
    }

    if (_isGiant) {
        if (player->formProperties->unk_00 < 200.0) {
            GiantMask_FormProperties_Grow(player->formProperties);
        }
        if (REG(38) != 0x20) {
            GiantMask_Reg_Grow(false);
        }
    }
    if (!_isGiant && player->formProperties->unk_00 >= 200.0) {
        GiantMask_FormProperties_Shrink(player->formProperties);
    }

    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        z2_SetActorSize(&player->base, _scale * 1.5f);
    } else {
        z2_SetActorSize(&player->base, _scale);
    }

    if (sp57) {
        _isGiant = !_isGiant;
        if (!_isGiant) {
            _scale = 0.01f;
            _nextScaleFactor = 10.0f;
        } else {
            _scale = 0.1f;
            _nextScaleFactor = 0.1f;
        }
    }

    switch (_transformationFlashState) {
        case 0:
            break;

        case 1:
            _transformationFlashCounter = 0;
            GiantMask_SetTransformationFlash(globalCtx, 255, 255, 255, 0);
            _transformationFlashState = 2;
            z2_PlaySfx(0x484F); // NA_SE_SY_TRANSFORM_MASK_FLASH

        case 2:
            _transformationFlashCounter += 40;
            if (_transformationFlashCounter >= 400) {
                _transformationFlashState = 3;
            }
            transformationFlashOpacity = _transformationFlashCounter;
            if (transformationFlashOpacity > 255) {
                transformationFlashOpacity = 255;
            }
            GiantMask_SetTransformationFlashAlpha(globalCtx, transformationFlashOpacity);
            break;

        case 3:
            _transformationFlashCounter -= 40;
            if (_transformationFlashCounter <= 0) {
                _transformationFlashCounter = 0;
                _transformationFlashState = 0;
                GiantMask_DisableTransformationFlash(globalCtx);
            } else {
                transformationFlashOpacity = _transformationFlashCounter;
                if (transformationFlashOpacity > 255) {
                    transformationFlashOpacity = 255;
                }
                GiantMask_SetTransformationFlashAlpha(globalCtx, transformationFlashOpacity);
            }
            break;
    }

    if ((_transformationState != 0) && (_transformationCameraId != 0)) {
        z2_Matrix_RotateY(player->base.shape.rot.y, 0); // MTXMODE_NEW
        z2_Matrix_GetStateTranslationAndScaledZ(_unk_1D64, &sp58);

        _unk_1D24.x = player->base.currPosRot.pos.x + sp58.x;
        _unk_1D24.y = player->base.currPosRot.pos.y + sp58.y + _unk_1D68;
        _unk_1D24.z = player->base.currPosRot.pos.z + sp58.z;

        _unk_1D30.x = player->base.currPosRot.pos.x;
        _unk_1D30.y = player->base.currPosRot.pos.y + _unk_1D6C;
        _unk_1D30.z = player->base.currPosRot.pos.z;

        _unk_1D54 = z2_Math_Sins(_cutsceneCounter * 1512) * _unk_1D58;
        z2_Matrix_InsertZRotation_f(_unk_1D54, 1); // MTXMODE_APPLY
        z2_Matrix_GetStateTranslationAndScaledY(1.0f, &_unk_1D3C);
        z2_Play_CameraSetAtEyeUp(globalCtx, _transformationCameraId, &_unk_1D30, &_unk_1D24, &_unk_1D3C);
        ShrinkWindow_SetLetterboxTarget(0x1B);
    }
}

f32 GiantMask_GetScaleModifier() {
    return _scale * 100.0f;
}

f32 GiantMask_GetSimpleScaleModifier() {
    return _isGiant ? 10.0f : 1.0f;
}

f32 GiantMask_GetSimpleInvertedScaleModifier() {
    return _isGiant ? 0.1f : 1.0f;
}

f32 GiantMask_GetNextScaleFactor() {
    return _nextScaleFactor;
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

f32 GiantMask_GetBigOctoSpitVelocity() {
    // Displaced code:
    f32 result = 10.0f;
    // End displaced code

    if (_isGiant) {
        result *= 2.0f;
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

void GiantMask_AdjustSpinAttackHeight(Actor* actor, ColCylinder* collider) {
    collider->params.height *= GiantMask_GetSimpleScaleModifier();

    // Displaced code:
    z2_Collider_UpdateCylinder(actor, collider);
    // End displaced code
}

bool GiantMask_IsGiant() {
    return _isGiant;
}

void GiantMask_MarkReset() {
    _shouldReset = true;
}

void GiantMask_TryReset() {
    if (_shouldReset) {
        _scale = 0.01f;
        _isGiant = false;
        _nextScaleFactor = 10.0f;
        _shouldReset = false;
    }
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
