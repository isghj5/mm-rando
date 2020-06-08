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
    if ((z2_file.buttons_state.state == Z2_BUTTONS_STATE_TRANSITION) ||
        (z2_file.buttons_state.state == Z2_BUTTONS_STATE_TRANSITION_2) ||
        (link->action_state1 & Z2_ACTION_STATE1_TIME_STOP) != 0)
        return false;

    if (g_pending_freezes && link->frozen_timer == 0) {
        g_pending_freezes -= 1;
        z2_LinkInvincibility(link, 0x14);
        z2_LinkDamage(game, link, Z2_DAMAGE_EFFECT_FREEZE, 0x40800000);
        return true;
    }

    return false;
}
