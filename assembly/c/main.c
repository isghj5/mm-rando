#include <stdbool.h>
#include "actor_ext.h"
#include "arrow_cycle.h"
#include "arrow_magic.h"
#include "deku_hop.h"
#include "dpad.h"
#include "external_effects.h"
#include "gfx.h"
#include "hud_colors.h"
#include "icetrap.h"
#include "misc.h"
#include "mmr.h"
#include "models.h"
#include "text.h"
#include "util.h"
#include "world_colors.h"
#include "z2.h"

void c_init() {
    heap_init();
    gfx_init();
    dpad_init();
    hud_colors_init();
    actor_ext_init();
    models_init();
    mmr_init();
    misc_init();
    text_init();
    WorldColors_Init();
}

void before_player_actor_update(z2_link_t *link, z2_game_t *game) {
    dpad_before_player_actor_update(link, game);
    external_effects_handle(link, game);
    arrow_cycle_handle(link, game);
    arrow_magic_handle(link, game);
    deku_hop_handle(link, game);
}

bool before_damage_process(z2_link_t *link, z2_game_t *game) {
    return icetrap_give(link, game);
}

/**
 * Hook function called after preparing display buffers for writing during current frame.
 **/
void game_after_prepare_display_buffers(z2_gfx_t *gfx) {
    // Check if models objheap should finish advancing.
    models_after_prepare_display_buffers(gfx);
}
