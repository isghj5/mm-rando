#include <z64.h>
#include "BaseRupee.h"
#include "Misc.h"

static ColorRGBA8 sparklePrimColor = { 255, 255, 127, 0 };
static ColorRGBA8 sparkleEnvColor = { 255, 255, 255, 0 };
static Vec3f sparkleVelocity = { 0.0f, 0.1f, 0.0f };
static Vec3f sparkleAcceleration = { 0.0f, 0.01f, 0.0f };

ActorEnItem00* HitTag_RupeeSpawn(GlobalContext* ctxt, Actor* actor, u8 count) {
    ActorEnItem00* item = z2_fixed_drop_spawn(ctxt, &actor->currPosRot.pos, 0);
    u16 tagId = actor->params & 0x1F;
    u16 giIndex = 0x3C5 + tagId*3 + count;
    Rupee_CheckAndSetGiIndex(&item->base, ctxt, giIndex);
    return item;
}

void HitTag_Draw(Actor* actor, GlobalContext* ctxt) {
    if (MISC_CONFIG.flags.hiddenRupeesSparkle && (ctxt->sceneFrameCount & 1) == 0) {
        Vec3f pos;
        pos.x = actor->currPosRot.pos.x + ((z2_Rand_ZeroOne() - 0.5f) * 10.0f);
        pos.y = actor->currPosRot.pos.y + ((z2_Rand_ZeroOne() - 0.5f) * 10.0f);
        pos.z = actor->currPosRot.pos.z + ((z2_Rand_ZeroOne() - 0.5f) * 10.0f);
        z2_EffectSsKiraKira_SpawnSmall(ctxt, &pos, &sparkleVelocity, &sparkleAcceleration, &sparklePrimColor, &sparkleEnvColor);
    }
}
