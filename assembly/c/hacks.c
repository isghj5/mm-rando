#include <stdbool.h>
#include "z2.h"

void draw_b_button_icon_color_fix(z2_game_t *game) {
    // Clear the Env color before drawing amounts/text
    z2_disp_buf_t *db = &(game->common.gfx->overlay);
    gDPSetEnvColor(db->p++, 0x00, 0x00, 0x00, 0xFF);

    z2_DrawBButtonIcon(game);
}

void draw_c_button_icons_color_fix(z2_game_t *game) {
    // Clear the Env color before drawing amounts/text
    z2_disp_buf_t *db = &(game->common.gfx->overlay);
    gDPSetEnvColor(db->p++, 0x00, 0x00, 0x00, 0xFF);

    z2_DrawCButtonIcons(game);
}
