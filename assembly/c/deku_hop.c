#include <stdbool.h>
#include "misc.h"
#include "z2.h"

struct deku_hop_state {
    u8 last_hop;
};

static struct deku_hop_state g_deku_hop_state = {
    .last_hop = 5,
};

void deku_hop_handle(z2_link_t *link, z2_game_t *game) {
    if (!MISC_CONFIG.continuous_deku_hop) {
        return;
    }

    if (link->deku_hop_counter < 5 && link->deku_hop_counter > 0 && link->deku_hop_counter != g_deku_hop_state.last_hop) {
        if (game->common.input[0].pad_pressed.a) {
            game->common.input[0].pad_pressed.a = 0;
            link->deku_hop_counter++;
            g_deku_hop_state.last_hop = link->deku_hop_counter;
        }
    } else {
        g_deku_hop_state.last_hop = link->deku_hop_counter;
    }
}
