#include <z64.h>
#include "Util.h"

f32 CollisionCheck_GetDamageAndEffectOnBumper(ColCommon* at, ColBodyInfo* atInfo, ColCommon* ac, ColBodyInfo* acInfo, u32* effect) {
    static f32 damageMultipliers[] = {
        0.0f, 1.0f, 2.0f, 0.5f, 0.25f, 3.0f, 4.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f,
    };
    u32 dmgFlags;
    s32 i;
    f32 damage;

    dmgFlags = atInfo->toucher.dmgFlags;
    ActorDamageChart* damageTable = ac->actor->colChkInfo.damageTable;

    if (dmgFlags & DMG_UNK_0x1E && damageTable != NULL) {
        *effect = 0;
        damage = z2_CollisionCheck_GetToucherDamage(at, atInfo, ac, acInfo);

        u8 highPriorityEffect = 0xFF;
        switch (ac->actor->id) {
            case ACTOR_EN_BSB: // Keeta
                highPriorityEffect = 0xD;
                break;
        }

        f32 highestMultiplier = 0;

        for (i = 0; i != ARRAY_COUNT(damageTable->attack); i++) {
            if ((dmgFlags & 1) == 1) {
                ActorDamageByte attack = damageTable->attack[i];
                if (attack.damage) {
                    f32 checkMultiplier = damageMultipliers[attack.damage];
                    bool shouldApply = false;
                    if (checkMultiplier > highestMultiplier || (checkMultiplier == highestMultiplier && *effect == 0)) {
                        shouldApply = true;
                    }

                    if (highPriorityEffect != 0xFF) {
                        if (*effect != highPriorityEffect && attack.effect == highPriorityEffect) {
                            shouldApply = true;
                        } else if (*effect == highPriorityEffect && attack.effect != highPriorityEffect) {
                            shouldApply = false;
                        }
                    }

                    if (shouldApply) {
                        highestMultiplier = checkMultiplier;
                        *effect = attack.effect;
                    }
                }
            }
            dmgFlags >>= 1;
        }

        return damage * highestMultiplier;
    }

    return z2_CollisionCheck_GetDamageAndEffectOnBumper(at, atInfo, ac, acInfo, effect);
}
