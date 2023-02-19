#ifndef WORLD_COLORS_H
#define WORLD_COLORS_H

#include <z64.h>

#define WORLD_COLOR_CONFIG_MAGIC 0x57524C44

typedef struct {
    u32 rainbowTunic        : 1;
    u32 bombTrapTunicColor  : 1;
    u32                     : 30;
} WorldColorFlags;

struct WorldColorConfig {
    u32 magic;
    u32 version;
    Color goronEnergyPunch;
    Color goronEnergyRolling;
    Color swordChargeEnergyBluEnv;
    Color swordChargeEnergyBluPri;
    Color swordChargeEnergyRedEnv;
    Color swordChargeEnergyRedPri;
    Color swordChargeSparksBlu;
    Color swordChargeSparksRed;
    Color swordSlashEnergyBluPri;
    Color swordSlashEnergyRedPri;
    Color swordBeamEnergyEnv;
    Color swordBeamEnergyPri;
    Color swordBeamDamageEnv;
    Color blueBubble;
    Color fireArrowEffectEnv;
    Color fireArrowEffectPri;
    Color iceArrowEffectEnv;
    Color iceArrowEffectPri;
    Color lightArrowEffectEnv;
    Color lightArrowEffectPri;
    Color formTunic[5];
    WorldColorFlags flags;
};

extern struct WorldColorConfig WORLD_COLOR_CONFIG;

void WorldColors_Init(void);
void WorldColors_RandomizeTunic(ActorPlayer* actor);
void WorldColors_CycleTunic(GlobalContext* ctxt);

#endif // WORLD_COLORS_H
