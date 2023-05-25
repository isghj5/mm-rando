#include <z64.h>
#include "ActorExt.h"
#include "Color.h"
#include "ColorConvert.h"
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
    .formTunic[0]            = { 0xBD, 0xB5, 0xAD, },
    .formTunic[1]            = { 0x1E, 0x69, 0x1B, },
    .formTunic[2]            = { 0x1E, 0x69, 0x1B, },
    .formTunic[3]            = { 0x1E, 0x69, 0x1B, },
    .formTunic[4]            = { 0x1E, 0x69, 0x1B, },
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

    linkColor = WORLD_COLOR_CONFIG.formTunic[linkForm];

    DispBuf* opa = &ctxt->state.gfxCtx->polyOpa;
    gDPSetEnvColor(opa->p++, linkColor.r, linkColor.g, linkColor.b, 0xFF);
    z2_SkelAnime_DrawFlexLod(ctxt, skeleton, jointTable, dListCount, overrideLimbDraw, postLimbDraw, actor, lod);
}

void WorldColors_PlayerColorAfterMask(GraphicsContext* gfxCtx, s32 maskIDMinusOne, PlayerMaskDList* maskDList) {
    u32 dl = maskDList->maskDListEntry[maskIDMinusOne];
    gSPDisplayList(gfxCtx->polyOpa.p++, dl);
    gDPSetEnvColor(gfxCtx->polyOpa.p++, WORLD_COLOR_CONFIG.formTunic[4].r,WORLD_COLOR_CONFIG.formTunic[4].g, WORLD_COLOR_CONFIG.formTunic[4].b, 0xFF);
}

void WorldColors_ZoraBoomerangColor(GraphicsContext* gfxCtx, s32 finDL) {
    gDPSetEnvColor(gfxCtx->polyOpa.p++, WORLD_COLOR_CONFIG.formTunic[2].r, WORLD_COLOR_CONFIG.formTunic[2].g, WORLD_COLOR_CONFIG.formTunic[2].b, 0xFF);
    gSPDisplayList(gfxCtx->polyOpa.p++, finDL);
}

void WorldColors_RandomizeTunic(ActorPlayer* actor) {
    u8 linkForm = actor->form;
    u32 randColor = z2_RngInt();
    Color tunicColor;
    tunicColor.r = randColor >> 24;
    tunicColor.g = randColor >> 16;
    tunicColor.b = randColor >> 8;

    WORLD_COLOR_CONFIG.formTunic[linkForm] = tunicColor;
}

static s16 sFormTunicHues[5] = {
    -1, -1, -1, -1, -1
};
void WorldColors_CycleTunic(GlobalContext* ctxt) {
    if (!WORLD_COLOR_CONFIG.flags.rainbowTunic) {
        return;
    }

    ActorPlayer* player = GET_PLAYER(ctxt);
    u8 linkForm = player->form;

    if (sFormTunicHues[linkForm] < 0) {
        sFormTunicHues[linkForm] = (s16)Color_GetHue(WORLD_COLOR_CONFIG.formTunic[linkForm].rgb);
    } else {
        sFormTunicHues[linkForm] = (sFormTunicHues[linkForm] + 1) % 360;
    }
    WORLD_COLOR_CONFIG.formTunic[linkForm].rgb = Color_SetHue(WORLD_COLOR_CONFIG.formTunic[linkForm].rgb, (f64)sFormTunicHues[linkForm]);
}

void WorldColors_Init(void) {
    // Set alpha values for specific colors.
    WORLD_COLOR_CONFIG.swordChargeEnergyBluEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordChargeEnergyRedEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordBeamEnergyEnv.a = 0x80;
    WORLD_COLOR_CONFIG.iceArrowEffectEnv.a = 0x80;
    WORLD_COLOR_CONFIG.lightArrowEffectEnv.a = 0x80;
}
