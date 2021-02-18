#include <stdbool.h>
#include "reloc.h"
#include "z2.h"

static u8 g_pending_freezes = 0;

bool icetrap_is_pending() {
    return g_pending_freezes > 0;
}

void icetrap_push_pending() {
    if (g_pending_freezes < 0xFF)
        g_pending_freezes += 1;
}

bool icetrap_give(z2_link_t *link, z2_game_t *game) {
    // Ensure this is the Player actor, and not Kafei.
    if (link->common.id != 0) {
        return false;
    }

    u32 mask1 = Z2_ACTION_STATE1_TIME_STOP |
                Z2_ACTION_STATE1_TIME_STOP_2;

    // Return early if Link is in certain state.
    if ((link->action_state1 & mask1) != 0) {
        return false;
    }

    if (g_pending_freezes) {
        g_pending_freezes -= 1;
        z2_LinkInvincibility(link, 0x14);
        z2_LinkDamage(game, link, Z2_DAMAGE_EFFECT_FREEZE, 0x40800000);
        return true;
    } else {
        return false;
    }
}
