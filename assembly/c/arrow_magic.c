#include <stdbool.h>
#include "arrow_cycle.h"
#include "misc.h"
#include "z2.h"

struct arrow_magic_state {
    z2_actor_t *arrow;
};

static struct arrow_magic_state g_arrow_magic_state = {
    .arrow = NULL,
};

static void arrow_magic_update_state(struct arrow_magic_state *state, z2_link_t *link, z2_game_t *game) {
    z2_actor_t *arrow = state->arrow;
    // Check if arrow has been shot, speed is checked to ensure arrow isn't just put away.
    if (link->common.attached_b == NULL && arrow != NULL && arrow->xz_speed != 0.0) {
        // Update state to consume magic once arrow is shot.
        if (z2_file.magic_consume_state == 4) {
            z2_file.magic_consume_state = 2;
        } else if (arrow->variable == 7) {
            // Deku Bubble won't set magic consumption state to 0 later, so apply magic cost manually.
            if (!z2_file.week_event_inf.infinite_magic) {
                z2_file.current_magic -= z2_file.magic_consume_cost;
                if (z2_file.current_magic < 0) {
                    z2_file.current_magic = 0;
                }
            }
        }
        state->arrow = NULL;
    } else {
        state->arrow = arrow_cycle_find_arrow(link, game);
    }
}

/**
 * Hook function used to get the initial magic consumption state for an elemental arrow.
 **/
s16 arrow_magic_get_initial_consume_state(z2_game_t *game) {
    if (MISC_CONFIG.arrow_magic_show) {
        return 4;
    } else {
        return 1;
    }
}

/**
 * Hook function used to check whether or not the magic cost of the current elemental arrow should
 * be written to RDRAM.
 **/
bool arrow_magic_should_set_magic_cost(z2_game_t *game, bool inf_magic) {
    if (MISC_CONFIG.arrow_magic_show) {
        // If showing magic cost, always set magic cost field for consistency.
        return true;
    } else {
        // Vanilla behavior, do not set magic cost field if infinite magic is active.
        return !inf_magic;
    }
}

/**
 * Handle arrow magic consumption state.
 **/
void arrow_magic_handle(z2_link_t *link, z2_game_t *game) {
    if (MISC_CONFIG.arrow_magic_show) {
        arrow_magic_update_state(&g_arrow_magic_state, link, game);
    }
}
