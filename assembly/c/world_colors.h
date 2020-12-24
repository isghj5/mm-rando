#ifndef WORLD_COLORS_H
#define WORLD_COLORS_H

#include "color.h"

#define WORLD_COLOR_CONFIG_MAGIC 0x57524C44

struct world_color_config {
    u32 magic;
    u32 version;
    rgbs_t goronEnergyPunch;
    rgbs_t goronEnergyRolling;
    rgbs_t swordChargeEnergyBluEnv;
    rgbs_t swordChargeEnergyBluPri;
    rgbs_t swordChargeEnergyRedEnv;
    rgbs_t swordChargeEnergyRedPri;
    rgbs_t swordChargeSparksBlu;
    rgbs_t swordChargeSparksRed;
    rgbs_t swordSlashEnergyBluPri;
    rgbs_t swordSlashEnergyRedPri;
    rgbs_t swordBeamEnergyEnv;
    rgbs_t swordBeamEnergyPri;
    rgbs_t swordBeamDamageEnv;
    rgbs_t blueBubble;
};

void WorldColors_Init(void);

#endif // WORLD_COLORS_H
