#include <stdbool.h>
#include <z64.h>
#include "Reloc.h"
#include "WorldColors.h"

// TODO rename file to Trap.c or DamageTrap.c

static u8 gPendingKnockdowns = 0;
static u8 gPendingPokes = 0;
static u8 gPendingFreezes = 0;
static u8 gPendingShocks = 0;
static u8 gPendingBombTraps = 0;

void Icetrap_PushPending(u8 type) {
    switch (type) {
        case DAMAGE_EFFECT_FLY_BACK:
            if (gPendingKnockdowns < 0xFF) {
                gPendingKnockdowns++;
            }
            break;
        case DAMAGE_EFFECT_FLY_BACK_2:
            if (gPendingPokes < 0xFF) {
                gPendingPokes++;
            }
            break;
        case DAMAGE_EFFECT_FREEZE:
            if (gPendingFreezes < 0xFF) {
                gPendingFreezes++;
            }
            break;
        case DAMAGE_EFFECT_ELECTRIC:
            if (gPendingShocks < 0xFF) {
                gPendingShocks++;
            }
            break;
        case DAMAGE_EFFECT_BOMBTRAP:
            if (gPendingBombTraps < 0xFF) {
                gPendingBombTraps++;
            }
            break;
    }
}

bool Icetrap_Give(ActorPlayer* player, GlobalContext* ctxt) {
    // Ensure this is the Player actor, and not Kafei.
    if (player->base.id != 0) {
        return false;
    }

    // If player is receiving item from shop.
    if (player->getItem && player->givingActor && (player->givingActor->id == ACTOR_EN_OSSAN || player->givingActor->id == ACTOR_EN_TRT || player->givingActor->id == ACTOR_EN_SOB1)) {
        return false;
    }

    u32 mask1 = PLAYER_STATE1_TIME_STOP |
                PLAYER_STATE1_TIME_STOP_2 |
                PLAYER_STATE1_DAMAGED;

    // Return early if Link is in certain state.
    if ((player->stateFlags.state1 & mask1) != 0) {
        return false;
    }

    u32 damageEffectType = 0;
    u32 trapBomb = 0;
    if (gPendingKnockdowns) {
        gPendingKnockdowns--;
        damageEffectType = DAMAGE_EFFECT_FLY_BACK;
    } else if (gPendingPokes) {
        gPendingPokes--;
        damageEffectType = DAMAGE_EFFECT_FLY_BACK_2;
    } else if (gPendingFreezes) {
        gPendingFreezes--;
        damageEffectType = DAMAGE_EFFECT_FREEZE;
    } else if (gPendingShocks) {
        gPendingShocks--;
        damageEffectType = DAMAGE_EFFECT_ELECTRIC;
    } else if (gPendingBombTraps) {
        gPendingBombTraps--;
        trapBomb = 1;
    }

    if (damageEffectType) {
        z2_LinkInvincibility(player, 0x14);
        z2_LinkDamage(ctxt, player, damageEffectType, 4.0, 4.0, player->base.shape.rot.y);
        return true;
    } else if (trapBomb) {
        ActorEnBom* bomb = (ActorEnBom*) z2_SpawnActor(&ctxt->actorCtx, ctxt, ACTOR_EN_BOM, player->base.currPosRot.pos.x, player->base.currPosRot.pos.y, player->base.currPosRot.pos.z, 0, 0, 0, 0);
        if (bomb) {
            // bomb->collider1.base.flagsAT &= ~8; // AT_TYPE_PLAYER Disable player-aligned damage
            bomb->timer = 0;
            z2_PlaySfx(0x3A76);
            if (WORLD_COLOR_CONFIG.flags.bombTrapTunicColor) {
                WorldColors_RandomizeTunic(player);
            }
            return true;
        }
    } else {
        return false;
    }
}
