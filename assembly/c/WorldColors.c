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

void WorldColors_Init(void) {
    // Set alpha values for specific colors.
    WORLD_COLOR_CONFIG.swordChargeEnergyBluEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordChargeEnergyRedEnv.a = 0x80;
    WORLD_COLOR_CONFIG.swordBeamEnergyEnv.a = 0x80;
}
