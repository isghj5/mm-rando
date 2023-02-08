#include <z64.h>
#include "ActorExt.h"
#include "Color.h"
#include "WorldColors.h"

struct WorldColorConfig WORLD_COLOR_CONFIG = {
    .magic = WORLD_COLOR_CONFIG_MAGIC,
    .version = 0,
    .goronEnergyPunch        = { 0xFF, 0x00, 0x00, },
    .goronEnergyRolling      = { 0x9B, 0x00, 0x00, },
    .swordChargeEnergyBluEnv = { 0x00, 0x64, 0xFF, },
    .swordChargeEnergyBluPri = { 0xAA, 0xFF, 0xFF, },
    .swordChargeEnergyRedEnv = { 0xFF, 0x64, 0x00, },
    .swordChargeEnergyRedPri = { 0xFF, 0xFF, 0xAA, },
    .swordChargeSparksBlu    = { 0x00, 0x00, 0xFF, },
    .swordChargeSparksRed    = { 0xFF, 0x00, 0x00, },
    .swordSlashEnergyBluPri  = { 0xAA, 0xFF, 0xFF, },
    .swordSlashEnergyRedPri  = { 0xFF, 0xFF, 0xAA, },
    .swordBeamEnergyEnv      = { 0x00, 0x64, 0xFF, },
    .swordBeamEnergyPri      = { 0xAA, 0xFF, 0xFF, },
    .swordBeamDamageEnv      = { 0x00, 0xFF, 0xFF, },
    .blueBubble              = { 0x00, 0x00, 0xFF, },
    .fireArrowEffectEnv      = { 0xFF, 0x00, 0x00, },
    .fireArrowEffectPri      = { 0xFF, 0xC8, 0x00, },
    .iceArrowEffectEnv       = { 0x00, 0x00, 0xFF, },
    .iceArrowEffectPri       = { 0xAA, 0xFF, 0xFF, },
    .lightArrowEffectEnv     = { 0xFF, 0xFF, 0x00, },
    .lightArrowEffectPri     = { 0xFF, 0xFF, 0xAA, },
    .fierceDeityTunic        = { 0xBD, 0xB5, 0xAD, },
    .goronTunic              = { 0x1E, 0x69, 0x1B, },
    .zoraTunic               = { 0x1E, 0x69, 0x1B, },
    .dekuTunic               = { 0x1E, 0x69, 0x1B, },
    .humanTunic              = { 0x1E, 0x69, 0x1B, },
};

u32 WorldColors_GetBlueBubbleColor(Actor* actor, GlobalContext* ctxt) {
    Color color = WORLD_COLOR_CONFIG.blueBubble;
    if ((color.a & COLOR_SPECIAL_INSTANCE) != 0) {
        bool created = false;
        struct ActorExt* ext = ActorExt_Setup(actor, &created);
        if (ext != NULL) {
            if (created) {
                ext->color.rgb = Color_RandomizeHue(color.rgb);
            }
            return Color_ConvertToIntWithAlpha(ext->color, 0);
        }
    }
    return Color_ConvertToIntWithAlpha(color.rgb, 0);
}

void WorldColors_SetPlayerEnvColor(GlobalContext* ctxt, void** skeleton, Vec3s* jointTable, s32 dListCount,
                                    void* overrideLimbDraw, void* postLimbDraw, Actor* actor, s32 lod) {

    Color linkColor;
    u8 linkForm = ((ActorPlayer*)actor)->form;

    switch (linkForm) {
    case 0:
        linkColor = WORLD_COLOR_CONFIG.fierceDeityTunic;
        break;
    case 1:
        linkColor = WORLD_COLOR_CONFIG.goronTunic;
        break;
    case 2:
        linkColor = WORLD_COLOR_CONFIG.zoraTunic;
        break;
    case 3:
        linkColor = WORLD_COLOR_CONFIG.dekuTunic;
        break;
    case 4:
        linkColor = WORLD_COLOR_CONFIG.humanTunic;
        break;
    }

    DispBuf* opa = &ctxt->state.gfxCtx->polyOpa;
    gDPSetEnvColor(opa->p++, linkColor.r, linkColor.g, linkColor.b, 0xFF);
    z2_SkelAnime_DrawFlexLod(ctxt, skeleton, jointTable, dListCount, overrideLimbDraw, postLimbDraw, actor, lod);
}

void WorldColors_PlayerColorAfterMask(GraphicsContext* gfxCtx, s32 maskIDMinusOne, PlayerMaskDList* maskDList) {
    u32 dl = maskDList->maskDListEntry[maskIDMinusOne];
    gSPDisplayList(gfxCtx->polyOpa.p++, dl);
    gDPSetEnvColor(gfxCtx->polyOpa.p++, WORLD_COLOR_CONFIG.humanTunic.r, WORLD_COLOR_CONFIG.humanTunic.g, WORLD_COLOR_CONFIG.humanTunic.b, 0xFF);
}

void WorldColors_ZoraBoomerangColor(GraphicsContext* gfxCtx, s32 finDL) {
    gDPSetEnvColor(gfxCtx->polyOpa.p++, WORLD_COLOR_CONFIG.zoraTunic.r, WORLD_COLOR_CONFIG.zoraTunic.g, WORLD_COLOR_CONFIG.zoraTunic.b, 0xFF);
    gSPDisplayList(gfxCtx->polyOpa.p++, finDL);
}

void WorldColors_RandomizeTunic(ActorPlayer* actor) {
    u8 linkForm = actor->form;
    u32 randColor = z2_RngInt();
    Color tunicColor;
    tunicColor.r = randColor >> 24;
    tunicColor.g = randColor >> 16;
    tunicColor.b = randColor >> 8;

    switch (linkForm) {
    case 0:
        WORLD_COLOR_CONFIG.fierceDeityTunic = tunicColor;
        break;
    case 1:
        WORLD_COLOR_CONFIG.goronTunic = tunicColor;
        break;
    case 2:
        WORLD_COLOR_CONFIG.zoraTunic = tunicColor;
        break;
    case 3:
        WORLD_COLOR_CONFIG.dekuTunic = tunicColor;
        break;
    case 4:
        WORLD_COLOR_CONFIG.humanTunic = tunicColor;
        break;
    }
}

void WorldColors_Init(void) {
    // Set alpha values for specific colors.
    WORLD_COLOR_CONFIG.swordChargeEnergyBluEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordChargeEnergyRedEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordBeamEnergyEnv.a = 0x80;
    WORLD_COLOR_CONFIG.iceArrowEffectEnv.a = 0x80;
    WORLD_COLOR_CONFIG.lightArrowEffectEnv.a = 0x80;
}
