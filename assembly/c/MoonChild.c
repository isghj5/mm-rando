#include <z64.h>
#include "Misc.h"
#include "Player.h"
#include "GiantMask.h"
#include "Reloc.h"

static u16 sTimeResetTimer = 0;
static u16 sTriggerSaveTimer = 0;
static s16 sSubCamId;
static Vec3f sSubCamEye;
static Vec3f sSubCamAt;
static Vec3f sSubCamUp;

void EffStk_CustomAction(ActorEffStk* effStk, GlobalContext* ctxt) {
    if (effStk->unk146 < 0x3C00) {
        effStk->unk146 += 0x400;
        effStk->unk148 = z2_Math_SinS(effStk->unk146) * -630.0f;
    }
    effStk->unk144++;
}

void MoonChild_UpdateTimeReset(ActorEnJs* this, GlobalContext* ctxt) {
    if (z2_SkelAnime_Update(&this->skelAnime)) {
        z2_Animation_MorphToLoop(&this->skelAnime, (AnimationHeader*)0x06017E98, 0.0f); // gMoonChildStandingAnim
    }

    ActorPlayer* player = GET_PLAYER(ctxt);

    Vec3f subCamEyeOffset;

    if (sTimeResetTimer > 0) {
        sTimeResetTimer--;
    }

    Vec3f* focus;
    s16 rotation;
    f32 scale;
    s16 cameraAngle = 0x8000;

    if (sTimeResetTimer > 150) {
        focus = &this->base.currPosRot.pos;
        rotation = this->base.shape.rot.y;
        scale = 80.0f;
    } else if (sTimeResetTimer > 140) {
        focus = &this->base.currPosRot.pos;
        rotation = this->base.shape.rot.y;
        scale = 60.0f;
        cameraAngle = 0x7000;
    } else if (sTimeResetTimer > 130) {
        focus = &this->base.currPosRot.pos;
        rotation = this->base.shape.rot.y;
        scale = 40.0f;
        cameraAngle = 0x9000;
    } else if (sTimeResetTimer > 120) {
        focus = &this->base.currPosRot.pos;
        rotation = this->base.shape.rot.y;
        scale = 30.0f;
        cameraAngle = 0x7000;
    } else {
        focus = &player->base.currPosRot.pos;
        rotation = player->base.shape.rot.y + 0x2000;
        scale = 0.0f + sTimeResetTimer;
    }

    if (sTimeResetTimer == 120) {
        z2_Play_EnableMotionBlur(150);
        z2_Player_PlayAnimationOnce(ctxt, player, (void*)0x0400CF68);
    } else if (sTimeResetTimer < 120) {
        if (z2_PlayerAnimation_Update(ctxt, &player->skelAnime)) {
            z2_Player_PlayAnimationLoop(ctxt, player, (void*)0x0400CF70);

            u16 spawn = *(u16*)0x80145342; // maybe better to pass in as a config (for entrance rando future proofing)
            ctxt->warpDestination = spawn;
            ctxt->warpType = 20;

            sTriggerSaveTimer = 53;
        }
    }

    if (sTriggerSaveTimer > 0) {
        sTriggerSaveTimer--;

        if (sTriggerSaveTimer == 0) {
            // eventDayCount = 0
            gSaveContext.perm.day = 0;
            gSaveContext.perm.time = 0x3FFF;
            GiantMask_MarkReset();
            if (gSaveContext.perm.mask == 0x14) {
                gSaveContext.perm.mask = 0;
            }
            z2_Sram_SaveSpecialNewDay(ctxt);
        }
    }

    z2_Matrix_RotateY(rotation, 0); // MTXMODE_NEW
    z2_Matrix_GetStateTranslationAndScaledZ(scale, &subCamEyeOffset);

    f32 sSubCamUpRotZ = z2_Math_SinS(cameraAngle) * 1.0f; // sSubCamUpRotZScale
    z2_Matrix_InsertZRotation_f(sSubCamUpRotZ, 1); // MTXMODE_APPLY
    z2_Matrix_GetStateTranslationAndScaledY(1.0f, &sSubCamUp);

    sSubCamEye.x = focus->x + subCamEyeOffset.x;
    sSubCamEye.y = focus->y + subCamEyeOffset.y + 38.0f; // + sSubCamEyeOffsetY;
    sSubCamEye.z = focus->z + subCamEyeOffset.z;

    sSubCamAt.x = focus->x;
    sSubCamAt.y = focus->y + 38.0f; // + sSubCamAtOffsetY;
    sSubCamAt.z = focus->z;

    z2_Play_CameraSetAtEyeUp(ctxt, sSubCamId, &sSubCamAt, &sSubCamEye, &sSubCamUp);
}

void MoonChild_HandleWillYouPlayYes(GlobalContext* ctxt, u16 currentTextId) {
    u16 nextTextId;

    if (currentTextId == 0x2203) {
        nextTextId = 0x2205;
    } else {
        nextTextId = !MISC_CONFIG.internal.victoryCantFightMajora || Player_CheckVictory() ? 0x21FE : 0x21FD;
    }

    z2_Message_ContinueTextbox(ctxt, nextTextId);
}

void MoonChild_BeginTimeReset(ActorEnJs* this, GlobalContext* ctxt) {
    z2_MessageClose(ctxt);

    sTimeResetTimer = 161;

    ActorEffStk* effStk = (ActorEffStk*) z2_SpawnActor(&ctxt->actorCtx, ctxt, ACTOR_EFF_STK, this->base.currPosRot.pos.x, this->base.currPosRot.pos.y, this->base.currPosRot.pos.z, 0, 0, 0, 0);
    if (effStk) {
        effStk->actionFunc = EffStk_CustomAction;
    } else {
        z2_SpawnActor(&ctxt->actorCtx, ctxt, ACTOR_OCEFF_WIPE, this->base.currPosRot.pos.x, this->base.currPosRot.pos.y, this->base.currPosRot.pos.z, 0, 0, 0, 0);
    }

    z2_PlaySfx(0x4851); // NA_SE_SY_STALKIDS_PSYCHO

    sSubCamId = z2_Play_CreateSubCamera(ctxt);
    z2_Play_CameraChangeStatus(ctxt, 0, 1); // CAM_ID_MAIN, CAM_STATUS_WAIT
    z2_Play_CameraChangeStatus(ctxt, sSubCamId, 7); // CAM_STATUS_ACTIVE

    gSaveContext.extra.buttonsState.transitionState = 1; // HUD_VISIBILITY_NONE

    this->actionFunc = MoonChild_UpdateTimeReset;

    GET_PLAYER(ctxt)->stateFlags.state1 |= PLAYER_STATE1_TIME_STOP_2;
}

s32 MoonChild_OverrideLimbDraw(GlobalContext* ctxt, s32 limbIndex, Gfx** dList, Vec3f* pos, Vec3s* rot, ActorEnJs* this) {
    if (limbIndex == 10 && this->maskType == 0) { // MOONCHILD_LIMB_HEAD
        if (sTimeResetTimer > 0) {
            if (sTimeResetTimer % 4 == 0) {
                rot->y += 0x333;
            } else if (sTimeResetTimer % 4 == 1) {
                rot->y += 0x666;
            } else if (sTimeResetTimer % 4 == 2) {
                rot->y -= 0x333;
            } else {
                rot->y -= 0x666;
            }
        }
    }

    return false;
}

bool MoonChild_StartTextboxAfterGiveItem(GlobalContext* ctxt, Actor* actor) {
    bool victory = !MISC_CONFIG.internal.victoryCantFightMajora || Player_CheckVictory();
    u16 textId = victory ? 0x2208 : 0x21FD;
    z2_ShowMessage(ctxt, textId, actor);
    return victory;
}
