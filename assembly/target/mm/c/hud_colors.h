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
};

void hud_colors_init();
void hud_colors_main_menu_init();

#endif // HUD_COLORS_H
