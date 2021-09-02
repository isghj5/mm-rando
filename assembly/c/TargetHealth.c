#include <stdbool.h>
#include <z64.h>

static void TargetHealth_Draw(GlobalContext* ctxt, Vec3f* pos, ColorRGBA8* color, u8 health, u8 maxHealth) {
    s16 x = (s16)(160.0 + pos->x) - 0x1C;
    s16 y = (s16)(120.0 - pos->y) - 0x20;
    u16 h = 8;
    u16 fullWidth = 0x3A;
    u16 minX = 2;
    u16 maxX = 320 - fullWidth;
    if (x < minX) {
        x = minX;
    } else if (x > maxX) {
        x = maxX;
    }

    u16 minY = 0;
    u16 maxY = 240 - h;
    if (y < minY) {
        y = minY;
    } else if (y > maxY) {
        y = maxY;
    }

    GraphicsContext* gfxCtx = ctxt->state.gfxCtx;
    z2_8012C654(gfxCtx);
    gDPSetEnvColor(gfxCtx->overlay.p++, 0x64, 0x32, 0x32, 0xFF);

    u16 wf = 0x800;
    u16 hf = 0x800;
    gfxCtx->overlay.p = z2_8010CFBC(gfxCtx->overlay.p, 0x02004DA0, 8, 16, x, y, 4, h, wf, hf, 0xFF, 0xFF, 0xFF, 0xFF); // [

    gfxCtx->overlay.p = z2_8010CFBC(gfxCtx->overlay.p, 0x02004E20, 24, 16, x + 4, y, 0x30, h, wf, hf, 0xFF, 0xFF, 0xFF, 0xFF); // =

    gfxCtx->overlay.p = z2_8010D480(gfxCtx->overlay.p, 0x02004DA0, 8, 16, x + 0x30 + 4, y, 4, h, wf, hf, 0xFF, 0xFF, 0xFF, 0xFF, 3, 0x100); // ]

    gDPPipeSync(gfxCtx->overlay.p++);

    gDPSetCombineLERP(gfxCtx->overlay.p++, PRIMITIVE, ENVIRONMENT, TEXEL0, ENVIRONMENT, 0, 0, 0, PRIMITIVE, PRIMITIVE, ENVIRONMENT, TEXEL0, ENVIRONMENT, 0, 0, 0, PRIMITIVE);

    gDPSetEnvColor(gfxCtx->overlay.p++, color->r, color->g, color->b, 0xFF);

    gDPSetPrimColor(gfxCtx->overlay.p++, 0, 0, color->r, color->g, color->b, 0xFF);

    gDPFillRectangle(gfxCtx->overlay.p++, x + 4, y + 2, x + 4 + (u8)(48.0 * (f32)health / (f32)maxHealth), y + 2 + 3);
}

#define MaxHealthPtr(actor) ((s8*)(&actor->shape.pad17))

static s8 TargetHealth_GetMaxHealth(Actor* actor, u8* currentHealth) {
    s8 maxHealth = *MaxHealthPtr(actor);
    u8 health = actor->unkA0.health;
    if (maxHealth == 0 || maxHealth < health) {
        if (actor->parent) {
            return TargetHealth_GetMaxHealth(actor->parent, currentHealth);
        }

        maxHealth = health;

        if (maxHealth == 0) {
            maxHealth = -1;
        }
        *MaxHealthPtr(actor) = maxHealth;
    }
    *currentHealth = health;
    return maxHealth;
}

void TargetHealth_Draw_Hook(GlobalContext* ctxt, Vec3f* pos) {
    // TODO wearing mask of truth?
    TargetContext* targetContext = &ctxt->actorCtx.targetContext;
    bool isEnemy = targetContext->targetType == 5 || targetContext->targetType == 9;
    if (targetContext->targetLocked && isEnemy) {
        u8 currentHealth;
        s8 maxHealth = TargetHealth_GetMaxHealth(targetContext->targetLocked, &currentHealth);
        if (maxHealth > 0) {
            s8 count = targetContext->unk4C;
            TargetHealth_Draw(ctxt, pos, &targetContext->unk50[count].unk10, currentHealth, (u8)maxHealth);
        }
    }

    // Displaced Code
    ctxt->state.gfxCtx->overlay.p = z2_Gfx_CallSetupDL(ctxt->state.gfxCtx->overlay.p, 0x39);
}
