#ifndef HUD_COLORS_H
#define HUD_COLORS_H

#include "z2.h"

// Magic number for hud_color_config: "HUDC"
#define HUD_COLOR_CONFIG_MAGIC 0x48554443

struct hud_color_config {
    u32 magic;
    u32 version;

    // Version 0
    z2_color_rgb8_t button_a;
    z2_color_rgb8_t button_b;
    z2_color_rgb8_t button_c;
    z2_color_rgb8_t button_start;
    z2_color_rgb8_t clock_emblem;
    z2_color_rgb8_t clock_emblem_inverted_1;
    z2_color_rgb8_t clock_emblem_inverted_2;
    z2_color_rgb8_t clock_emblem_sun;
    z2_color_rgb8_t clock_moon;
    z2_color_rgb8_t clock_sun;
    z2_color_rgb8_t heart;
    z2_color_rgb8_t heart_dd;
    z2_color_rgb8_t magic_normal;
    z2_color_rgb8_t magic_inf;
    z2_color_rgb8_t map;
    z2_color_rgb8_t map_entrance_cursor;
    z2_color_rgb8_t map_player_cursor;
    z2_color_rgb8_t rupee[3];

    // Version 1
    z2_color_rgb8_t button_icon_a;
    z2_color_rgb8_t button_icon_b;
    z2_color_rgb8_t button_icon_c;
    z2_color_rgb8_t menu_a_inner_1;
    z2_color_rgb8_t menu_a_inner_2;
    z2_color_rgb8_t menu_a_outer_1;
    z2_color_rgb8_t menu_a_outer_2;
    z2_color_rgb8_t menu_c_inner_1;
    z2_color_rgb8_t menu_c_inner_2;
    z2_color_rgb8_t menu_c_outer_1;
    z2_color_rgb8_t menu_c_outer_2;
    z2_color_rgb8_t note_a_1;
    z2_color_rgb8_t note_a_2;
    z2_color_rgb8_t note_a_shadow_1;
    z2_color_rgb8_t note_a_shadow_2;
    z2_color_rgb8_t note_c_1;
    z2_color_rgb8_t note_c_2;
    z2_color_rgb8_t note_c_shadow_1;
    z2_color_rgb8_t note_c_shadow_2;
    z2_color_rgb8_t pause_title_a;
    z2_color_rgb8_t pause_title_c;
    z2_color_rgb8_t prompt_1;
    z2_color_rgb8_t prompt_2;
    z2_color_rgb8_t prompt_glow;
    z2_color_rgb8_t score_lines;
    z2_color_rgb8_t score_note;
};

void hud_colors_init(void);
void hud_colors_main_menu_init(void);
void hud_colors_update_pause_menu_colors(z2_game_t *game);

#endif // HUD_COLORS_H
