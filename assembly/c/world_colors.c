#include "actor_ext.h"
#include "color.h"
#include "types.h"
#include "world_colors.h"

struct world_color_config WORLD_COLOR_CONFIG = {
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

u32 WorldColors_GetBlueBubbleColor(z2_actor_t *actor, z2_game_t *game) {
    rgbs_t color = WORLD_COLOR_CONFIG.blueBubble;
    if ((color.s & COLOR_SPECIAL_INSTANCE) != 0) {
        bool created = false;
        struct actor_ext *ext = actor_ext_setup(actor, &created);
        if (ext != NULL) {
            if (created) {
                ext->color = color_randomize_hue(color.rgb);
            }
            return color_rgb2int(ext->color, 0);
        }
    }

    return color_rgb2int(color.rgb, 0);
}

void WorldColors_Init(void) {
    // Set alpha values for specific colors.
    WORLD_COLOR_CONFIG.swordChargeEnergyBluEnv.s = 0x80;
    WORLD_COLOR_CONFIG.swordChargeEnergyRedEnv.s = 0x80;
    WORLD_COLOR_CONFIG.swordBeamEnergyEnv.s = 0x80;
}
