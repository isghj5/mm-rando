#include "GiantMask.h"

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
    REG(30) = 0x40; // sidewalk animation speed
    REG(32) = 0x40; // sidewalk animation speed
    REG(38) = 0x20; // walk animation speed
    REG(39) = 0x20; // walk animation speed
    REG(45) *= 10;  // running speed
    REG(68) *= 10;  // gravity
    IREG(67) *= 10; // jump strength // read at 806F48EC

    if (!fromGiantTransformation) {
        REG(45) *= 7.0f / 11.0f;   // running speed
    }
}

static void func_809DA1D0(GlobalContext* globalCtx, u8 arg1, u8 arg2, u8 arg3, u8 arg4) {
    MREG(64) = 1;
    MREG(65) = arg1;
    MREG(66) = arg2;
    MREG(67) = arg3;
    MREG(68) = arg4;
}

static void func_809DA22C(GlobalContext* globalCtx, u8 arg1) {
    MREG(68) = arg1;
}

static void func_809DA24C(GlobalContext* globalCtx) {
    MREG(64) = 0;
}

/* 0x01AC */ f32 unk_01AC;
/* 0x1D14 */ u32 unk_1D14;
/* 0x1D18 */ s16 unk_1D18;
/* 0x1D1A */ s16 unk_1D1A;
/* 0x1D1C */ u32 unk_1D1C;
/* 0x1D20 */ s16 unk_1D20;
/* 0x1D22 */ s16 unk_1D22;
/* 0x1D24 */ Vec3f unk_1D24;
/* 0x1D30 */ Vec3f unk_1D30;
/* 0x1D3C */ Vec3f unk_1D3C;
/* 0x1D48 */ Vec3f unk_1D48;
/* 0x1D54 */ f32 unk_1D54;
/* 0x1D58 */ f32 unk_1D58;
/* 0x1D5C */ f32 unk_1D5C;
/* 0x1D60 */ UNK_TYPE1 unk1D60[0x4];
/* 0x1D64 */ f32 unk_1D64;
/* 0x1D68 */ f32 unk_1D68;
/* 0x1D6C */ f32 unk_1D6C;
/* 0x1D70 */ f32 unk_1D70 = 0.01f;
/* 0x1D74 */ f32 unk_1D74;
/* 0x1D78 */ u8 unk_1D78;
/* 0x1D7A */ s16 unk_1D7A;
/* 0x1D7C */ s16 unk_1D7C;
/* 0x1D7E */ s16 unk_1D7E;
static u8 D_809E0420;
static u8 D_809E0421;
static bool isGiant;
static u8 D_809E0430;
static f32 nextScaleFactor = 10.0f;

void GiantMask_Handle(ActorPlayer* player, GlobalContext* globalCtx) {
    s16 i;
    Actor* temp_a0_5;
    Vec3f sp58;
    u8 sp57 = 0;
    f32 phi_f0_2;
    s16 phi_v1;

    unk_1D14++;

    switch (unk_1D18) {
        case 0:
            if (player->stateFlags.state1 & PLAYER_STATE1_GIANT_MASK) {
                // z2_800EA0D4(globalCtx, &globalCtx->csCtx);
                // unk_1D22 = z2_801694DC(globalCtx);
                //z2_80169590(globalCtx, 0, 1);
                //z2_80169590(globalCtx, unk_1D22, 7);
                z2_8016566C(150); // motion blur
                unk_1D14 = 0;
                unk_1D5C = 0.0f;
                unk_1D58 = 0.0f;
                if (!isGiant) {
                    unk_1D18 = 1;
                    unk_1D68 = 10.0f;
                    unk_1D64 = 60.0f;
                    unk_1D6C = 23.0f;
                    unk_1D70 = 0.01f;
                    unk_1D74 = 0.0f;
                    goto label1;
                } else {
                    unk_1D18 = 10;
                    unk_1D68 = 10.0f;
                    unk_1D64 = 200.0f;
                    unk_1D6C = 273.0f;
                    unk_1D70 = 0.1f;
                    unk_1D74 = -100.0f;
                    sp57 = 1;
                    goto label2;
                }
            }
            break;

        case 1:
            // if ((unk_1D14 < 80U) && (D_809E0420 != 0) &&
            //     CHECK_BTN_ANY(CONTROLLER1(globalCtx)->press.button,
            //                   BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
            //     unk_1D18++;
            //     unk_1D78 = 1;
            //     unk_1D14 = 0;
            // } else {
            label1:
                if (unk_1D14 >= 50U) {
                    if (unk_1D14 == (u32)(BREG(43) + 60)) {
                        z2_PlaySfx(0x9C5); // NA_SE_PL_TRANSFORM_GIANT
                    }
                    Math_ApproachF(&unk_1D64, 200.0f, 0.1f, unk_1D5C * 640.0f);
                    Math_ApproachF(&unk_1D6C, 273.0f, 0.1f, unk_1D5C * 150.0f);
                    Math_ApproachF(&unk_1D70, 0.1f, 0.2f, unk_1D5C * 0.1f);
                    Math_ApproachF(&unk_1D74, -100.0f, 1.0f, unk_1D5C * 100.0f);
                    Math_ApproachF(&unk_1D5C, 1.0f, 1.0f, 0.001f);
                } else {
                    Math_ApproachF(&unk_1D64, 30.0f, 0.1f, 1.0f);
                }

                if (unk_1D14 > 50U) {
                    Math_ApproachZeroF(&unk_1D58, 1.0f, 0.06f);
                } else {
                    Math_ApproachF(&unk_1D58, 0.4f, 1.0f, 0.02f);
                }

                if (unk_1D14 == 107U) {
                    unk_1D78 = 1;
                }

                if (unk_1D14 < 121U) {
                    break;
                }

                sp57 = 1;
                D_809E0420 = 1;
                goto block_38;
            // }
            break;

        case 2:
            if (unk_1D14 < 8U) {
                break;
            }
            sp57 = 1;
            goto block_38;

        case 10:
            // if ((unk_1D14 < 30U) && (D_809E0421 != 0) &&
            //     CHECK_BTN_ANY(CONTROLLER1(globalCtx)->press.button,
            //                   BTN_A | BTN_B | BTN_CUP | BTN_CDOWN | BTN_CLEFT | BTN_CRIGHT)) {
            //     unk_1D18++;
            //     unk_1D78 = 1;
            //     unk_1D14 = 0;
            //     break;
            // }

        label2:
            if (unk_1D14 != 0U) {
                if (unk_1D14 == (u32)(BREG(44) + 10)) {
                    z2_PlaySfx(0x9C6); // NA_SE_PL_TRANSFORM_NORAML
                }
                Math_ApproachF(&unk_1D64, 60.0f, 0.1f, unk_1D5C * 640.0f);
                Math_ApproachF(&unk_1D6C, 23.0f, 0.1f, unk_1D5C * 150.0f);
                Math_ApproachF(&unk_1D70, 0.01f, 0.1f, 0.003f);
                Math_ApproachF(&unk_1D74, 0.0f, 1.0f, unk_1D5C * 100.0f);
                Math_ApproachF(&unk_1D5C, 2.0f, 1.0f, 0.01f);
            }

            if (unk_1D14 == 42U) {
                unk_1D78 = 1;
            }

            if (unk_1D14 > 50U) {
                D_809E0421 = 1;
                goto block_38;
            }
            break;

        case 11:
            if (unk_1D14 < 8U) {
                break;
            }

        block_38:
        case 20:
            // unk_1D18 = 0;
            unk_1D18 = 21;
            // z2_80169AFC(globalCtx, unk_1D22, 0);
            // unk_1D22 = 0;
            // z2_800EA0EC(globalCtx, &globalCtx->csCtx);
            //actor.flags |= 1;
            player->stateFlags.state1 &= ~PLAYER_STATE1_GIANT_MASK;
            //unk_1D70 = 0.01f;
            z2_80165690();
            break;
        case 21:
            unk_1D18 = 0;
            if (gSaveContext.perm.mask == 0x14) {
                GiantMask_Reg_Grow(true);
            }
            break;
    }

    if (unk_1D70 > 0.05) { // bit hacky, but we'll see
        if (player->formProperties->unk_00 < 200.0) {
            GiantMask_FormProperties_Grow(player->formProperties);
        }
        if (REG(38) != 0x20) {
            GiantMask_Reg_Grow(false);
        }
    }
    if (unk_1D70 <= 0.05 && player->formProperties->unk_00 >= 200.0) {
        GiantMask_FormProperties_Shrink(player->formProperties);
    }

    if (player->form == PLAYER_FORM_FIERCE_DEITY) {
        z2_SetActorSize(&player->base, unk_1D70 * 1.5f);
    } else {
        z2_SetActorSize(&player->base, unk_1D70);
    }

    if (sp57) {
        isGiant = !isGiant;
        if (!isGiant) {
            nextScaleFactor = 10.0f;
        } else {
            nextScaleFactor = 0.1f;
        }
    }

    switch (unk_1D78) {
        case 0:
            break;

        case 1:
            unk_1D7A = 0;
            func_809DA1D0(globalCtx, 255, 255, 255, 0);
            unk_1D78 = 2;
            z2_PlaySfx(0x484F); // NA_SE_SY_TRANSFORM_MASK_FLASH

        case 2:
            unk_1D7A += 40;
            if (unk_1D7A >= 400) {
                unk_1D78 = 3;
            }
            phi_v1 = unk_1D7A;
            if (phi_v1 > 255) {
                phi_v1 = 255;
            }
            func_809DA22C(globalCtx, phi_v1);
            break;

        case 3:
            unk_1D7A -= 40;
            if (unk_1D7A <= 0) {
                unk_1D7A = 0;
                unk_1D78 = 0;
                func_809DA24C(globalCtx);
            } else {
                phi_v1 = unk_1D7A;
                if (phi_v1 > 255) {
                    phi_v1 = 255;
                }
                func_809DA22C(globalCtx, phi_v1);
            }
            break;
    }

    if (unk_1D18 != 0) {
    //if ((unk_1D18 != 0) && (unk_1D22 != 0)) {
        // Matrix_RotateY(player->base.shape.rot.y, MTXMODE_NEW);
        // Matrix_GetStateTranslationAndScaledZ(unk_1D64, &sp58);

        // unk_1D24.x = player->base.currPosRot.pos.x + sp58.x;
        // unk_1D24.y = player->base.currPosRot.pos.y + sp58.y + unk_1D68;
        // unk_1D24.z = player->base.currPosRot.pos.z + sp58.z;

        // unk_1D30.x = player->base.currPosRot.pos.x;
        // unk_1D30.y = player->base.currPosRot.pos.y + unk_1D6C;
        // unk_1D30.z = player->base.currPosRot.pos.z;

        // unk_1D54 = Math_SinS(unk_1D14 * 1512) * unk_1D58;
        // Matrix_InsertZRotation_f(unk_1D54, MTXMODE_APPLY);
        // Matrix_GetStateTranslationAndScaledY(1.0f, &unk_1D3C);
        // z2_8016981C(globalCtx, unk_1D22, &unk_1D30, &unk_1D24, &unk_1D3C);
        ShrinkWindow_SetLetterboxTarget(0x1B);
    }
}

f32 GiantMask_GetScaleModifier() {
    return unk_1D70 * 100.0f;
}

f32 GiantMask_GetNextScaleFactor() {
    return nextScaleFactor;
}

f32 GiantMask_GetFloorHeightCheckDelta(GlobalContext* globalCtx, Actor* actor, Vec3f* pos, s32 flags) {
    // Displaced code:
    f32 result = (flags & 0x800) ? 10.0f : 50.0f;
    // End displaced code

    if (actor->id == ACTOR_PLAYER) {
        result *= GiantMask_GetScaleModifier();
    }
    return result;
}

f32 GiantMask_GetLedgeWalkOffHeight(Actor* actor) {
    // Displaced code:
    f32 result = -11.0;
    // End displaced code

    if (actor->id == ACTOR_PLAYER) {
        result *= GiantMask_GetScaleModifier();
    }

    return result;
}

/*
C Buttons Anywhere
E9738CE0 595A
E9738CDE 595A
*/

// 806FF248 form changing

// TODO hook 806FF120 (unequip giant mask between scenes) to keep it on if scale is 0.1. maybe also grow form properties then
// DONE TODO set 80382530 to 0x0020 0x0020 for walk animation cycle, and set 8038253E to 0xDAC for max speed // REG(38), REG(39), REG(45)
// DONE TODO hook 806F027C (max speed calculation)
// DONE TODO hook 807007F0 (checks wall collision height) // 4384C000
// DONE TODO hook 800B7698 (checks walking up ledges) // 43FA0000
// DONE TODO hook 800B7764 (check floor distance when jumping off ledge) // C2DC0000
// DONE TODO hook 80704BF8 (hardcoded distance to surface? 100.0)

// TODO
// ceiling transformation height
// sidehop/backflip momentum
// climbing out of water?
// dive distance
// fall height damage // function at 806F43A0
// spin attack radius
// big octo softlock
