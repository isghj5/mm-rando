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

// Clock Town stray fairy icon image buffer, written to by randomizer.
u8 TOWN_FAIRY_BYTES[0xC00] = { 0 };

// Clock Town stray fairy icon.
static sprite_t town_fairy_icon = {
    TOWN_FAIRY_BYTES, 32, 24, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

struct dungeon_entry {
    u8 index;
    u8 remains;
    u8 is_dungeon;
    u8 has_fairies;
    u8 has_tokens;
    char name[9];
};

static struct dungeon_entry dungeons[7] = {
    { 0, 0x5D, 1, 1, 0, "Woodfall" },
    { 1, 0x5E, 1, 1, 0, "Snowhead" },
    { 2, 0x5F, 1, 1, 0, "GreatBay" },
    { 3, 0x60, 1, 1, 0, "StnTower" },
    { 4, 0,    0, 1, 0, "ClockTwn" },
    { 5, 0,    0, 0, 1, "Swamp" },
    { 6, 0,    0, 0, 2, "Ocean" },
};

static int g_dungeon_count = 7;

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
        if (d->is_dungeon && has_remains(d->index)) {
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
        if (d->is_dungeon) {
            // Get key count for dungeon.
            u8 keys = z2_file.dungeon_keys[d->index];
            // Get key count as text.
            char count[2] = "0";
            get_count_text(keys, count, 1);
            // Draw key count as text.
            int top = start_top + ((icon_size + padding) * i) + 1;
            text_print(count, left + 4, top);
        }
    }
    left += icon_size + padding;

    // Draw boss keys.
    sprite_load(db, &icon_24_sprite, 6, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (d->is_dungeon) {
            if (z2_file.dungeon_items[d->index].boss_key) {
                int top = start_top + ((icon_size + padding) * i);
                sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
            }
        }
    }
    left += icon_size + padding;

    // Draw maps.
    sprite_load(db, &icon_24_sprite, 8, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (d->is_dungeon) {
            if (z2_file.dungeon_items[d->index].map) {
                int top = start_top + ((icon_size + padding) * i);
                sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
            }
        }
    }
    left += icon_size + padding;

    // Draw compasses.
    sprite_load(db, &icon_24_sprite, 7, 1);
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        if (d->is_dungeon) {
            if (z2_file.dungeon_items[d->index].compass) {
                int top = start_top + ((icon_size + padding) * i);
                sprite_draw(db, &icon_24_sprite, 0, left, top, icon_size, icon_size);
            }
        }
    }
    left += icon_size + padding;

    // Draw stray fairy, skulltula token icons.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        int top = start_top + ((icon_size + padding) * i) - 2;
        if (d->has_fairies) {
            // Draw dungeon fairy icons (32-bit RGBA). Otherwise, draw Clock Town fairy icon.
            if (d->is_dungeon) {
                sprite_load(db, &fairy_sprite, d->index, 1);
                sprite_draw(db, &fairy_sprite, 0, left, top, 20, 15);
            } else {
                // Draw Clock Town fairy icon.
                sprite_load(db, &town_fairy_icon, 0, 1);
                sprite_draw(db, &town_fairy_icon, 0, left, top, 20, 15);
            }
        } else if (d->has_tokens) {
            // Draw skulltula token icon.
            sprite_load(db, &skulltula_icon, 0, 1);
            sprite_draw(db, &skulltula_icon, 0, left + 2, top, 16, 12);
        }
    }
    left += 20 + padding;

    // Draw stray fairy count, skulltula token count.
    for (int i = 0; i < g_dungeon_count; i++) {
        struct dungeon_entry *d = &(dungeons[i]);
        int top = start_top + ((icon_size + padding) * i);
        // Get total count and maximum count for stray fairies or skulltula tokens.
        int total = 0;
        int maximum = 0;
        if (d->has_fairies) {
            // Get stray fairy count for dungeon or town.
            if (d->is_dungeon) {
                // Get fairy count for dungeon.
                total = z2_file.stray_fairies[d->index];
                maximum = 15;
            } else {
                // Check for Clock Town fairy, flag: 0x801F0570 & 0x80
                bool has_town_fairy = (z2_file.week_event_inf.week_event_inf_bytes[8] & 0x80) != 0;
                total = has_town_fairy ? 1 : 0;
                maximum = 1;
            }
        } else if (d->has_tokens) {
            // Get skulltula token count.
            total = z2_file.skull_tokens_1;
            if (d->has_tokens == 2) {
                total = z2_file.skull_tokens_2;
            }
            maximum = 30;
        }
        // Display count as text.
        if (d->has_fairies || d->has_tokens) {
            // Get count as text.
            char count[3] = " 0";
            get_count_text(total, count, 2);
            // Draw fairy/token count as text.
            if (total >= maximum) {
                // Use green text if at maximum.
                z2_color_rgba8_t color = { 0x78, 0xFF, 0x00, 0xFF };
                text_print_with_color(count, left, top, color);
            } else {
                text_print(count, left, top);
            }
        }
    }

    // Flush text and finish.
    text_flush(db);
    gDPFullSync(db->p++);
    gSPEndDisplayList(db->p++);
}
