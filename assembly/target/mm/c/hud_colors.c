#include <stdbool.h>
#include "hud_colors.h"
#include "reloc.h"
#include "z2.h"

// Uncomment to apply theme for testing
// #define USE_THEME

struct hud_color_config HUD_COLOR_CONFIG = {
    .magic = HUD_COLOR_CONFIG_MAGIC,
    .version = 0,

    // Version 0
    .button_a                = { 0x64, 0xC8, 0xFF },
    .button_b                = { 0x64, 0xFF, 0x78 },
    .button_c                = { 0xFF, 0xF0, 0x00 },
    .button_start            = { 0xFF, 0x82, 0x3C },
    .clock_emblem            = { 0x00, 0xAA, 0x64 },
    .clock_emblem_inverted_1 = { 0x64, 0xCD, 0xFF },
    .clock_emblem_inverted_2 = { 0x00, 0x9B, 0xFF },
    .clock_emblem_sun        = { 0xFF, 0xFF, 0x6E },
    .clock_moon              = { 0xFF, 0xFF, 0x37 },
    .clock_sun               = { 0xFF, 0x64, 0x6E },
    .heart                   = { 0xFF, 0x46, 0x32 },
    .heart_dd                = { 0xC8, 0x00, 0x00 },
    .magic_normal            = { 0x00, 0xC8, 0x00 },
    .magic_inf               = { 0x00, 0x00, 0xC8 },
    .map                     = { 0x00, 0xFF, 0xFF },
    .map_entrance_cursor     = { 0xC8, 0x00, 0x00 },
    .map_player_cursor       = { 0xC8, 0xFF, 0x00 },
    .rupee                   = {
                               { 0xC8, 0xFF, 0x64 },
                               { 0xAA, 0xAA, 0xFF },
                               { 0xFF, 0x69, 0x69 },
    },
};

static u32 color_rgb8_to_int(z2_color_rgb8_t color, u8 alpha) {
    return (color.r << 24) | (color.g << 16) | (color.b << 8) | alpha;
}

u32 hud_colors_get_magic_meter_color(bool inf) {
    u8 alpha = z2_game.hud_ctxt.rupees_alpha & 0xFF;
    if (inf) {
        return color_rgb8_to_int(HUD_COLOR_CONFIG.magic_inf, alpha);
    } else {
        return color_rgb8_to_int(HUD_COLOR_CONFIG.magic_normal, alpha);
    }
}

u32 hud_colors_get_map_color() {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.map, 0xA0);
}

u32 hud_colors_get_map_player_cursor_color() {
    u8 alpha = z2_game.hud_ctxt.minimap_alpha & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.map_player_cursor, alpha);
}

u32 hud_colors_get_map_entrance_cursor_color() {
    u8 alpha = z2_game.hud_ctxt.minimap_alpha & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.map_entrance_cursor, alpha);
}

u32 hud_colors_get_clock_emblem_color() {
    u8 alpha = (u8)(*(u16 *)(0x801BFB2C));
    return color_rgb8_to_int(HUD_COLOR_CONFIG.clock_emblem, alpha);
}

u16 hud_colors_get_clock_emblem_inverted_color(u8 idx) {
    z2_color_rgb8_t colors;
    s16 mode = *(s16 *)0x801BFBE8;

    if (idx > 2) {
        return 0;
    }

    // Mode should be either 0 (first color) or 1 (second color)
    if (!mode) {
        colors = HUD_COLOR_CONFIG.clock_emblem_inverted_1;
    } else {
        colors = HUD_COLOR_CONFIG.clock_emblem_inverted_2;
    }

    return colors.bytes[idx];
}

u32 hud_colors_get_clock_emblem_sun_color(u16 alpha) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.clock_emblem_sun, alpha & 0xFF);
}

u32 hud_colors_get_clock_sun_color() {
    u8 alpha = (*(u16 *)0x801BFB2C) & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.clock_sun, alpha);
}

u32 hud_colors_get_clock_moon_color() {
    u8 alpha = (*(u16 *)0x801BFB2C) & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.clock_moon, alpha);
}

u32 hud_colors_get_a_button_color() {
    u8 alpha = z2_game.hud_ctxt.a_alpha & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.button_a, alpha);
}

u32 hud_colors_get_b_button_color() {
    // Alpha won't be used but set it anyway
    u8 alpha = z2_game.hud_ctxt.b_alpha & 0xFF;
    return color_rgb8_to_int(HUD_COLOR_CONFIG.button_b, alpha);
}

u32 hud_colors_get_c_button_color(u16 alpha) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.button_c, alpha & 0xFF);
}

u32 hud_colors_get_start_button_color(u16 alpha) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.button_start, alpha & 0xFF);
}

void hud_colors_update_heart_colors(z2_game_t *game) {
    // Normal heart colors
    z2_color_rgb16_2_t *heart = &(z2_game.hud_ctxt.heart_inner_rgb);
    z2_color_rgb16_t *heart_beating = &(z2_game.hud_ctxt.heartbeat_inner_rgb);

    // Double defense heart colors
    z2_color_rgb16_t *heart_dd = &(z2_file.heart_dd_rgb);
    z2_color_rgb16_t *heart_dd_beating = &(z2_file.heart_dd_beating_rgb);

    // This function writes constant values to where the heart colors are stored.
    // It might also do other things.
    z2_WriteHeartColors(game);

    // Update heart colors (normal)
    heart->r1 = HUD_COLOR_CONFIG.heart.r;
    heart->g1 = HUD_COLOR_CONFIG.heart.g;
    heart->b1 = HUD_COLOR_CONFIG.heart.b;
    heart_beating->r = HUD_COLOR_CONFIG.heart.r;
    heart_beating->g = HUD_COLOR_CONFIG.heart.g;
    heart_beating->b = HUD_COLOR_CONFIG.heart.b;

    // Update heart colors (double defense)
    heart_dd->r = HUD_COLOR_CONFIG.heart_dd.r;
    heart_dd->g = HUD_COLOR_CONFIG.heart_dd.g;
    heart_dd->b = HUD_COLOR_CONFIG.heart_dd.b;
    heart_dd_beating->r = HUD_COLOR_CONFIG.heart_dd.r;
    heart_dd_beating->g = HUD_COLOR_CONFIG.heart_dd.g;
    heart_dd_beating->b = HUD_COLOR_CONFIG.heart_dd.b;
}

static void update_rupee_colors(u16 *rupee_colors) {
    for (int i = 0; i < 3; i++) {
        int idx = i * 3;
        rupee_colors[idx] = HUD_COLOR_CONFIG.rupee[i].r;
        rupee_colors[idx + 1] = HUD_COLOR_CONFIG.rupee[i].g;
        rupee_colors[idx + 2] = HUD_COLOR_CONFIG.rupee[i].b;
    }
}

static void apply_objectively_best_theme() {
#define APPLY(FIELD, R, G, B)     \
    HUD_COLOR_CONFIG.FIELD.r = R; \
    HUD_COLOR_CONFIG.FIELD.g = G; \
    HUD_COLOR_CONFIG.FIELD.b = B;

    APPLY(button_a,                0xFF, 0x60, 0xA0);
    APPLY(button_b,                0x58, 0xA4, 0xED);
    APPLY(button_c,                0xF0, 0xF0, 0xF0);
    APPLY(clock_emblem,            0xF0, 0xF0, 0xF0);
    APPLY(clock_emblem_inverted_1, 0xFF, 0xA0, 0xC0);
    APPLY(clock_emblem_inverted_2, 0xFF, 0xC0, 0xE0);
    APPLY(heart,                   0x58, 0xA4, 0xED);
    APPLY(heart_dd,                0x14, 0x87, 0xCD);
    APPLY(magic_normal,            0xFF, 0x60, 0xA0);
    APPLY(magic_inf,               0xFF, 0x80, 0xC0);
    APPLY(map,                     0xF0, 0xF0, 0xF0);
    APPLY(map_entrance_cursor,     0x38, 0x94, 0xDD);
    APPLY(map_player_cursor,       0xFF, 0x60, 0xA0);
    APPLY(rupee[0],                0xF0, 0xF0, 0xF0);
    APPLY(rupee[1],                0xFF, 0x60, 0xA0);
    APPLY(rupee[2],                0x58, 0xA4, 0xED);

#undef APPLY
}

void hud_colors_init() {
    u16 *rupee_colors = (u16 *)0x801BFD2C;

#ifdef USE_THEME
    // Apply theme
    apply_objectively_best_theme();
#endif // USE_THEME

    // The rupee colors never seem to get modified, so just update them once
    update_rupee_colors(rupee_colors);
}

void hud_colors_main_menu_init() {
    // Update rupee colors
    update_rupee_colors(z2_file_select_ctxt.rupee_colors);

    // Update hearts colors
    z2_file_select_ctxt.heart_rgb[0].r = HUD_COLOR_CONFIG.heart.r;
    z2_file_select_ctxt.heart_rgb[0].g = HUD_COLOR_CONFIG.heart.g;
    z2_file_select_ctxt.heart_rgb[0].b = HUD_COLOR_CONFIG.heart.b;
    z2_file_select_ctxt.heart_rgb[1].r = HUD_COLOR_CONFIG.heart_dd.r;
    z2_file_select_ctxt.heart_rgb[1].g = HUD_COLOR_CONFIG.heart_dd.g;
    z2_file_select_ctxt.heart_rgb[1].b = HUD_COLOR_CONFIG.heart_dd.b;
}
