#include <stdbool.h>
#include "gfx.h"
#include "text.h"
#include "z2.h"

// Whether or not the overlay menu is enabled.
static bool g_enable = true;

// Gold Skulltula HUD icon.
static sprite_t skulltula_icon = {
    NULL, 24, 18, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

struct dungeon_entry {
    u8 index;
    u8 remains;
    char name[12];
};

struct dungeon_entry dungeons[4] = {
    { 0, 0x5D, "Woodfall" },
    { 1, 0x5E, "Snowhead" },
    { 2, 0x5F, "GreatBay" },
    { 3, 0x60, "StnTower" },
};

static int g_dungeon_count = 4;

/**
 * Get text for a specific amount, with a limited digit count (1 or 2).
 **/
static void get_count_text(int amount, char *c, int digits) {
    if (digits == 1) {
        // Get text for 1 digit, max of 9.
        if (amount > 9) {
            amount = 9;
        }
        c[0] = '0' + amount;
        c[1] = '\0';
    } else if (digits == 2) {
        // Get text for 2 digits, max of 99.
        if (amount >= 10) {
            if (amount > 99) {
                amount = 99;
            }
            c[0] = '0' + (amount / 10);
        }
        c[1] = '0' + amount % 10;
        c[2] = '\0';
    }
}

/**
 * Whether or not the player has boss remains for a specific dungeon index.
 **/
static bool has_remains(u8 index) {
    return (z2_file.quest_status & (1 << index)) != 0;
}

/**
 * Whether or not the overlay menu should display.
 **/
static bool overlay_menu_should_draw(z2_game_t *game) {
    return game->pause_ctxt.state == 6 &&
        game->pause_ctxt.switching_screen == 0 &&
        (game->pause_ctxt.screen_idx == 0 || game->pause_ctxt.screen_idx == 3) &&
        (game->common.input[0].raw.pad.l || game->common.input[0].raw.pad.du);
}

/**
 * Try to draw overlay menu.
 **/
void overlay_menu_draw(z2_game_t *game) {
    if (!g_enable || !overlay_menu_should_draw(game)) {
        return;
    }

    z2_disp_buf_t *db = &(game->common.gfx->overlay);
    db->p = db->buf;

    // Call setup display list.
    gSPDisplayList(db->p++, &setup_db);

    // General variables.
    int icon_size = 16;
    int padding = 1;
    int rows = 10;

    // Background rectangle.
    int bg_width =
        (7 * icon_size) + 4 +
        (9 * font_sprite.tile_w) +
        (9 * padding);
    int bg_height = (rows * icon_size) + ((rows + 1) * padding);
    int bg_left = (Z2_SCREEN_WIDTH - bg_width) / 2;
    int bg_top = (Z2_SCREEN_HEIGHT - bg_height) / 2;

    // Left & top starting positions for drawing columns.
    int left = bg_left + padding;
    int start_top = bg_top + padding;

    // Update sprite pointers.
    icon_sprite.buf = game->pause_ctxt.icon_item_static;
    icon_24_sprite.buf = game->pause_ctxt.icon_item_24;
    // icon_map_sprite.buf = game->pause_ctxt.icon_item_map;
    skulltula_icon.buf = (u8*)game->hud_ctxt.parameter_static +0x31E0;

    // Draw background.
    gDPSetCombineMode(db->p++, G_CC_PRIMITIVE, G_CC_PRIMITIVE);
    gDPSetPrimColor(db->p++, 0, 0, 0x00, 0x00, 0x00, 0xD0);
    gSPTextureRectangle(db->p++,
        bg_left << 2, bg_top << 2,
        (bg_left + bg_width) << 2, (bg_top + bg_height) << 2,
        0,
        0, 0,
        1 << 10, 1 << 10);
    gDPPipeSync(db->p++);

    // Draw legend panel background.
    int legend_left = bg_left + icon_size + (9 * font_sprite.tile_w) + (padding * 3);
    int legend_top = bg_top - (icon_size + (padding * 3));
    int legend_width = (icon_size * 4) + (padding * 5);
    int legend_height = icon_size + (padding * 2);
    gDPSetPrimColor(db->p++, 0, 0, 0x00, 0x00, 0x00, 0xA0);
    gSPTextureRectangle(db->p++,
        legend_left << 2, legend_top << 2,
        (legend_left + legend_width) << 2, (legend_top + legend_height) << 2,
        0,
        0, 0,
        1 << 10, 1 << 10);
    gDPPipeSync(db->p++);
    gDPSetCombineMode(db->p++, G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM);

    gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, 0xFF);

    // Draw legend panel icons: Small Key, Boss Key, Map, Compass.
    int legend_icons[4] = { 10, 6, 8, 7, };
    for (int i = 0; i < 4; i++) {
        int index = legend_icons[i];
        int lleft = legend_left + ((icon_size + padding) * i);
        int top = legend_top + padding;
        sprite_load(db, &icon_24_sprite, index, 1);
        sprite_draw(db, &icon_24_sprite, 0, lleft, top, icon_size, icon_size);
    }

    // Draw remains.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (has_remains(d->index)) {
            int top = start_top + ((icon_size + padding) * i);
            sprite_load(db, &icon_sprite, d->remains, 1);
            sprite_draw(db, &icon_sprite, 0, left, top, icon_size, icon_size);
        }
    }
    left += icon_size + padding;

    // Draw names.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        int top = start_top + ((icon_size + padding) * i) + 1;
        text_print(d->name, left, top);
    }
    left += (9 * font_sprite.tile_w) + padding;

    // Draw small keys.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        // Get key count for dungeon.
        u8 keys = z2_file.dungeon_keys[d->index];
        // Get key count as text.
        char count[2] = "0";
        get_count_text(keys, count, 1);
        // Draw key count as text.
        int top = start_top + ((icon_size + padding) * i) + 1;
        text_print(count, left + 4, top);
    }
    left += icon_size + padding;

    // Draw boss keys.
    sprite_load(db, &icon_24_sprite, 6, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (z2_file.dungeon_items[d->index].boss_key) {
            int top = start_top + ((icon_size + padding) * i);
            sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
        }
    }
    left += icon_size + padding;

    // Draw maps.
    sprite_load(db, &icon_24_sprite, 8, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (z2_file.dungeon_items[d->index].map) {
            int top = start_top + ((icon_size + padding) * i);
            sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
        }
    }
    left += icon_size + padding;

    // Draw compasses.
    sprite_load(db, &icon_24_sprite, 7, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (z2_file.dungeon_items[d->index].compass) {
            int top = start_top + ((icon_size + padding) * i);
            sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
        }
    }
    left += icon_size + padding;

    // Draw stray fairy icon.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        int top = start_top + ((icon_size + padding) * i) - 2;
        sprite_load(db, &fairy_sprite, d->index, 1);
        sprite_draw(db, &fairy_sprite, 0, left, top, 20, 15);
    }
    left += 20 + padding;

    // Draw stray fairy count.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        // Get fairy count for dungeon.
        u8 fairies = z2_file.stray_fairies[d->index];
        char count[3] = " 0";
        get_count_text(fairies, count, 2);
        // Draw fairy count as text.
        int top = start_top + ((icon_size + padding) * i);
        if (fairies >= 15) {
            // Use green text if at maximum.
            z2_color_rgba8_t color = { 0x78, 0xFF, 0x00, 0xFF };
            text_print_with_color(count, left, top, color);
        } else {
            text_print(count, left, top);
        }
    }

    // Draw skulltula token counts.
    const char * spider_houses[2] = { "Swamp", "Ocean" };
    int left2 = bg_left + padding;
    int top2 = bg_top + (icon_size * 5) + (padding * 6);
    sprite_load(db, &skulltula_icon, 0, 1);
    for (int i = 0; i < 2; i++) {
        int butts = left2 + 6 + ((font_sprite.tile_w * 8) + (icon_size * 2) + (padding * 3)) * i;
        // Draw skulltula token icon.
        sprite_draw(db, &skulltula_icon, 0, butts, top2, 16, 12);
        butts += icon_size + padding;
        // Draw spider house name.
        text_print(spider_houses[i], butts, top2);
        butts += (font_sprite.tile_w * 6) + padding;
        // Get skulltula token count.
        int tokens = z2_file.skull_tokens_1;
        if (i == 1) {
            tokens = z2_file.skull_tokens_2;
        }
        // Get count as text.
        char count[3] = " 0";
        get_count_text(tokens, count, 2);
        // Draw token count as text.
        if (tokens >= 30) {
            // Use green text if at maximum.
            z2_color_rgba8_t color = { 0x78, 0xFF, 0x00, 0xFF };
            text_print_with_color(count, butts, top2, color);
        } else {
            text_print(count, butts, top2);
        }
    }

    // Flush text and finish.
    text_flush(db);
    gDPFullSync(db->p++);
    gSPEndDisplayList(db->p++);
}
