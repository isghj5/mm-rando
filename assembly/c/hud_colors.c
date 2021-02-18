#include <stdbool.h>
#include "hud_colors.h"
#include "reloc.h"
#include "z2.h"

struct hud_color_config HUD_COLOR_CONFIG = {
    .magic = HUD_COLOR_CONFIG_MAGIC,
    .version = 3,

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

    // Version 1
    .button_icon_a           = { 0x50, 0x5A, 0xFF },
    .button_icon_b           = { 0x46, 0xFF, 0x50 },
    .button_icon_c           = { 0xFF, 0xFF, 0x32 },
    .menu_a_inner_1          = { 0x64, 0x96, 0xFF },
    .menu_a_inner_2          = { 0x64, 0xFF, 0xFF },
    .menu_a_outer_1          = { 0x00, 0x00, 0x64 },
    .menu_a_outer_2          = { 0x00, 0x96, 0xFF },
    .menu_c_inner_1          = { 0xFF, 0xFF, 0x00 },
    .menu_c_inner_2          = { 0xFF, 0xFF, 0x00 },
    .menu_c_outer_1          = { 0x00, 0x00, 0x00 },
    .menu_c_outer_2          = { 0xFF, 0xA0, 0x00 },
    .note_a_1                = { 0x50, 0x96, 0xFF },
    .note_a_2                = { 0x64, 0xC8, 0xFF },
    .note_a_shadow_1         = { 0x0A, 0x0A, 0x0A },
    .note_a_shadow_2         = { 0x32, 0x32, 0xFF },
    .note_c_1                = { 0xFF, 0xFF, 0x32 },
    .note_c_2                = { 0xFF, 0xFF, 0xB4 },
    .note_c_shadow_1         = { 0x0A, 0x0A, 0x0A },
    .note_c_shadow_2         = { 0x6E, 0x6E, 0x32 },
    .pause_title_a           = { 0x00, 0x64, 0xFF },
    .pause_title_c           = { 0xFF, 0x96, 0x00 },
    .prompt_1                = { 0x00, 0x50, 0xC8 },
    .prompt_2                = { 0x32, 0x82, 0xFF },
    .prompt_glow             = { 0x00, 0x82, 0xFF },
    .score_lines             = { 0xFF, 0x00, 0x00 },
    .score_note              = { 0xFF, 0x64, 0x00 },

    // Version 2
    .dpad                    = { 0x80, 0x80, 0x80 },

    // Version 3
    .menu_border_1           = { 0xB4, 0xB4, 0x78 },
    .menu_border_2           = { 0x96, 0x8C, 0x5A },
    .menu_subtitle_text      = { 0xFF, 0xC8, 0x00 },
    .shop_cursor_1           = { 0x00, 0x00, 0xFF },
    .shop_cursor_2           = { 0x00, 0x50, 0xFF },
};

struct pause_cursor_colors {
    z2_color_rgb16_t default_inner_1;  /* 0x0000 */
    z2_color_rgb16_t default_inner_2;  /* 0x0006 */
    z2_color_rgb16_t yellow_inner_1;   /* 0x000C */
    z2_color_rgb16_t yellow_inner_2;   /* 0x0012 */
    z2_color_rgb16_t blue_inner_1;     /* 0x0018 */
    z2_color_rgb16_t blue_inner_2;     /* 0x001E */
    z2_color_rgb16_t default_outer_1;  /* 0x0024 */
    z2_color_rgb16_t default_outer_2;  /* 0x002A */
    z2_color_rgb16_t yellow_outer_1;   /* 0x0030 */
    z2_color_rgb16_t yellow_outer_2;   /* 0x0036 */
    z2_color_rgb16_t blue_outer_1;     /* 0x003C */
    z2_color_rgb16_t blue_outer_2;     /* 0x0042 */
};

static u32 color_rgb8_to_int(z2_color_rgb8_t color, u8 alpha) {
    return (color.r << 24) | (color.g << 16) | (color.b << 8) | alpha;
}

static void rgb8_to_rgb16(z2_color_rgb16_t *dest, z2_color_rgb8_t src) {
    dest->r = src.r;
    dest->g = src.g;
    dest->b = src.b;
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

u32 hud_colors_get_a_button_color(u8 alpha) {
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

/**
 * Hook function used to get the "A" or "C" button color when used as a song note icon.
 **/
u32 hud_colors_get_note_button_color(u8 index, u8 alpha) {
    if (index == 0) {
        return color_rgb8_to_int(HUD_COLOR_CONFIG.note_a_1, alpha);
    } else {
        return color_rgb8_to_int(HUD_COLOR_CONFIG.note_c_1, alpha);
    }
}

/**
 * Hook function used to get the pause menu primary border color.
 **/
u32 hud_colors_get_menu_border_1_color(void) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.menu_border_1, 0xFF);
}

/**
 * Hook function used to get the pause menu secondary border color.
 **/
u32 hud_colors_get_menu_border_2_color(void) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.menu_border_2, 0xFF);
}

/**
 * Hook function used to get the pause menu subtitle text color.
 **/
u32 hud_colors_get_menu_subtitle_text_color(void) {
    return color_rgb8_to_int(HUD_COLOR_CONFIG.menu_subtitle_text, 0xFF);
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

/**
 * Hook function which writes button note colors to RDRAM.
 *
 * Calls original function which writes the colors, then overwrites certain colors with our own
 * (A & C button note colors when used in a song).
 **/
void hud_colors_update_button_note_colors(z2_game_t *game) {
    // Call original function.
    z2_InitButtonNoteColors(game);

    // Write "A" button colors (order in RDRAM is RBG instead of RGB).
    s16 *a_note_colors = (s16*)0x801F6B0C;
    a_note_colors[0] = HUD_COLOR_CONFIG.note_a_1.r;
    a_note_colors[1] = HUD_COLOR_CONFIG.note_a_1.b;
    a_note_colors[2] = HUD_COLOR_CONFIG.note_a_1.g;
    a_note_colors[3] = HUD_COLOR_CONFIG.note_a_shadow_1.r;
    a_note_colors[4] = HUD_COLOR_CONFIG.note_a_shadow_1.g;
    a_note_colors[5] = HUD_COLOR_CONFIG.note_a_shadow_1.b;

    // Write "C" button colors, same ordering as above.
    s16 *c_note_colors = (s16*)0x801F6B18;
    c_note_colors[0] = HUD_COLOR_CONFIG.note_c_1.r;
    c_note_colors[1] = HUD_COLOR_CONFIG.note_c_1.b;
    c_note_colors[2] = HUD_COLOR_CONFIG.note_c_1.g;
    c_note_colors[3] = HUD_COLOR_CONFIG.note_c_shadow_1.r;
    c_note_colors[4] = HUD_COLOR_CONFIG.note_c_shadow_1.g;
    c_note_colors[5] = HUD_COLOR_CONFIG.note_c_shadow_1.b;

    // Write "A" button colors used when animating.
    s16 *a_note_blink = (s16*)0x801D0334;
    a_note_blink[0] = HUD_COLOR_CONFIG.note_a_1.r;
    a_note_blink[1] = HUD_COLOR_CONFIG.note_a_1.g;
    a_note_blink[2] = HUD_COLOR_CONFIG.note_a_1.b;
    a_note_blink[3] = HUD_COLOR_CONFIG.note_a_2.r;
    a_note_blink[4] = HUD_COLOR_CONFIG.note_a_2.g;
    a_note_blink[5] = HUD_COLOR_CONFIG.note_a_2.b;
    a_note_blink[6] = HUD_COLOR_CONFIG.note_a_shadow_1.r;
    a_note_blink[7] = HUD_COLOR_CONFIG.note_a_shadow_1.g;
    a_note_blink[8] = HUD_COLOR_CONFIG.note_a_shadow_1.b;
    a_note_blink[9] = HUD_COLOR_CONFIG.note_a_shadow_2.r;
    a_note_blink[10] = HUD_COLOR_CONFIG.note_a_shadow_2.g;
    a_note_blink[11] = HUD_COLOR_CONFIG.note_a_shadow_2.b;

    // Write "C" button colors used when animating.
    s16 *c_note_blink = (s16*)0x801D034C;
    c_note_blink[0] = HUD_COLOR_CONFIG.note_c_1.r;
    c_note_blink[1] = HUD_COLOR_CONFIG.note_c_1.g;
    c_note_blink[2] = HUD_COLOR_CONFIG.note_c_1.b;
    c_note_blink[3] = HUD_COLOR_CONFIG.note_c_2.r;
    c_note_blink[4] = HUD_COLOR_CONFIG.note_c_2.g;
    c_note_blink[5] = HUD_COLOR_CONFIG.note_c_2.b;
    c_note_blink[6] = HUD_COLOR_CONFIG.note_c_shadow_1.r;
    c_note_blink[7] = HUD_COLOR_CONFIG.note_c_shadow_1.g;
    c_note_blink[8] = HUD_COLOR_CONFIG.note_c_shadow_1.b;
    c_note_blink[9] = HUD_COLOR_CONFIG.note_c_shadow_2.r;
    c_note_blink[10] = HUD_COLOR_CONFIG.note_c_shadow_2.g;
    c_note_blink[11] = HUD_COLOR_CONFIG.note_c_shadow_2.b;
}

/**
 * Helper function to update the prompt colors used for messageboxes.
 **/
static void hud_colors_update_msgbox_prompt_colors(bool initial) {
    // Update first color used by color cycler.
    z2_color_rgb16_t *prompt_1 = (z2_color_rgb16_t*)0x801CFCD8;
    prompt_1->r = HUD_COLOR_CONFIG.prompt_1.r;
    prompt_1->g = HUD_COLOR_CONFIG.prompt_1.g;
    prompt_1->b = HUD_COLOR_CONFIG.prompt_1.b;

    // Update second color used by color cycler.
    z2_color_rgb16_t *prompt_2 = (z2_color_rgb16_t*)0x801CFCDE;
    prompt_2->r = HUD_COLOR_CONFIG.prompt_2.r;
    prompt_2->g = HUD_COLOR_CONFIG.prompt_2.g;
    prompt_2->b = HUD_COLOR_CONFIG.prompt_2.b;

    // Update glow color.
    z2_color_rgb16_t *prompt_glow = (z2_color_rgb16_t*)0x801CFCEA;
    prompt_glow->r = HUD_COLOR_CONFIG.prompt_glow.r;
    prompt_glow->g = HUD_COLOR_CONFIG.prompt_glow.g;
    prompt_glow->b = HUD_COLOR_CONFIG.prompt_glow.b;

    // Update number cursor colors.
    z2_color_rgb16_t *number_cursor = (z2_color_rgb16_t*)0x801CFD10;
    rgb8_to_rgb16(&number_cursor[0], HUD_COLOR_CONFIG.prompt_1);
    rgb8_to_rgb16(&number_cursor[1], HUD_COLOR_CONFIG.prompt_2);
    rgb8_to_rgb16(&number_cursor[3], HUD_COLOR_CONFIG.prompt_glow);

    // Update initial values of color cycler.
    if (initial) {
        // Initial color of message prompt icon when cycled.
        s16 *prompt_init = (s16*)0x801CFCF0;
        prompt_init[0] = HUD_COLOR_CONFIG.prompt_1.r;
        prompt_init[2] = HUD_COLOR_CONFIG.prompt_1.g;
        prompt_init[4] = HUD_COLOR_CONFIG.prompt_1.b;

        // Initial color of number cursor when cycled.
        s16 *number_cursor_init = (s16*)0x801CFD28;
        number_cursor_init[0] = HUD_COLOR_CONFIG.prompt_1.r;
        number_cursor_init[2] = HUD_COLOR_CONFIG.prompt_1.g;
        number_cursor_init[4] = HUD_COLOR_CONFIG.prompt_1.b;
    }
}

/**
 * Helper function for updating the pause menu colors.
 **/
void hud_colors_update_pause_menu_colors(z2_game_t *game) {
    // Only try to update colors if kaleido_scope is loaded.
    if (z2_0x801D0B70.pause_ovl.ram != NULL) {
        // Resolve address of colors in kaleido_scope (pause) data.
        u32 vram = 0x808160A0 + 0x158A8;
        void *addr = reloc_resolve_player_ovl(&z2_0x801D0B70.pause_ovl, vram);
        if (addr != NULL) {
            // Update pause menu cursor icon colors.
            struct pause_cursor_colors *colors = (struct pause_cursor_colors *)addr;
            rgb8_to_rgb16(&colors->blue_inner_1, HUD_COLOR_CONFIG.menu_a_inner_1);
            rgb8_to_rgb16(&colors->blue_inner_2, HUD_COLOR_CONFIG.menu_a_inner_2);
            rgb8_to_rgb16(&colors->blue_outer_1, HUD_COLOR_CONFIG.menu_a_outer_1);
            rgb8_to_rgb16(&colors->blue_outer_2, HUD_COLOR_CONFIG.menu_a_outer_2);
            rgb8_to_rgb16(&colors->yellow_inner_1, HUD_COLOR_CONFIG.menu_c_inner_1);
            rgb8_to_rgb16(&colors->yellow_inner_2, HUD_COLOR_CONFIG.menu_c_inner_2);
            rgb8_to_rgb16(&colors->yellow_outer_1, HUD_COLOR_CONFIG.menu_c_outer_1);
            rgb8_to_rgb16(&colors->yellow_outer_2, HUD_COLOR_CONFIG.menu_c_outer_2);
        }

        u8 *bg_dlist = (u8*)game->pause_ctxt.bg_dlist;
        if (bg_dlist != NULL) {
            // Update pause menu subtitle icon colors.
            z2_color_rgb8_t *subtitle_c = (z2_color_rgb8_t*)(bg_dlist + 0x13C);
            z2_color_rgb8_t *subtitle_a = (z2_color_rgb8_t*)(bg_dlist + 0x194);
            subtitle_a->r = HUD_COLOR_CONFIG.pause_title_a.r;
            subtitle_a->g = HUD_COLOR_CONFIG.pause_title_a.g;
            subtitle_a->b = HUD_COLOR_CONFIG.pause_title_a.b;
            subtitle_c->r = HUD_COLOR_CONFIG.pause_title_c.r;
            subtitle_c->g = HUD_COLOR_CONFIG.pause_title_c.g;
            subtitle_c->b = HUD_COLOR_CONFIG.pause_title_c.b;
        }
    }
}

/**
 * Helper function for updating the colors of button icons in text.
 **/
static void hud_colors_update_text_button_icon_colors(void) {
    z2_color_rgb16_t *icon_a = (z2_color_rgb16_t *)0x801D0848;
    z2_color_rgb16_t *icon_b = (z2_color_rgb16_t *)0x801D0842;
    z2_color_rgb16_t *icon_c = (z2_color_rgb16_t *)0x801D084E;
    rgb8_to_rgb16(icon_a, HUD_COLOR_CONFIG.button_icon_a);
    rgb8_to_rgb16(icon_b, HUD_COLOR_CONFIG.button_icon_b);
    rgb8_to_rgb16(icon_c, HUD_COLOR_CONFIG.button_icon_c);
}

static void update_rupee_colors(u16 *rupee_colors) {
    for (int i = 0; i < 3; i++) {
        int idx = i * 3;
        rupee_colors[idx] = HUD_COLOR_CONFIG.rupee[i].r;
        rupee_colors[idx + 1] = HUD_COLOR_CONFIG.rupee[i].g;
        rupee_colors[idx + 2] = HUD_COLOR_CONFIG.rupee[i].b;
    }
}

void hud_colors_init(void) {
    u16 *rupee_colors = (u16 *)0x801BFD2C;
    // The rupee colors never seem to get modified, so just update them once
    update_rupee_colors(rupee_colors);
    // Update the message prompt colors.
    hud_colors_update_msgbox_prompt_colors(true);
    // Update the text button icon colors.
    hud_colors_update_text_button_icon_colors();
}

void hud_colors_main_menu_init(void) {
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

/**
 * Hook function called to write song score lines color to RDRAM.
 **/
void hud_colors_update_score_lines_color(z2_game_t *game) {
    // Update song score lines color.
    game->msgbox_ctxt.score_line_color.r = HUD_COLOR_CONFIG.score_lines.r;
    game->msgbox_ctxt.score_line_color.g = HUD_COLOR_CONFIG.score_lines.g;
    game->msgbox_ctxt.score_line_color.b = HUD_COLOR_CONFIG.score_lines.b;
}

/**
 * Hook function called to write shop cursor color values to an output array.
 **/
void HudColors_WriteShopCursorColor(z2_actor_t *actor, u32 *output, u32 amountBits, u32 shopType) {
    // Hack to have f32 argument without being weird?
    f32 amount = *(f32*)&amountBits;
    // Build array of RGB values.
    u32 colors1[3], colors2[3];
    for (int i = 0; i < 3; i++) {
        colors1[i] = HUD_COLOR_CONFIG.shop_cursor_1.bytes[i];
        colors2[i] = HUD_COLOR_CONFIG.shop_cursor_2.bytes[i];
    }
    // Calculate individual color values for RGB.
    for (int i = 0; i < 3; i++) {
        u32 c1 = colors1[i], c2 = colors2[i];
        if (c1 <= c2) {
            f32 delta = (f32)(c2 - c1) * amount;
            output[i] = (u32)delta + c1;
        } else {
            f32 delta = (f32)(c1 - c2) * amount;
            output[i] = (u32)delta + c2;
        }
    }
    // Use constant alpha.
    output[3] = 0xFF;
}
