#include "hud_colors.h"
#include "misc.h"
#include "quest_item_storage.h"
#include "quest_items.h"
#include "reloc.h"
#include "save_file.h"
#include "z2.h"

// Vertex buffers.
static Vtx g_vertex_bufs[(4 * 3) * 2];

// Vertex buffer pointers.
static Vtx* g_vertex[3] = {
    &g_vertex_bufs[(4 * 0) * 2],
    &g_vertex_bufs[(4 * 1) * 2],
    &g_vertex_bufs[(4 * 2) * 2],
};

static Vtx* get_vtx_buffer(z2_game_t *game, u32 vert_idx, int slot) {
    // Get vertex of current icon drawing to Item Select screen
    const Vtx *src_vtx = (game->pause_ctxt.vtx_buf + vert_idx);

    // Get dest Vtx (factor in frame counter)
    int framebufidx = (game->common.gfx->frame_cnt_1 & 1);
    Vtx *dst_vtx = g_vertex[slot] + (framebufidx * 4);

    // Copy source Vtx over to dest Vtx
    for (int i = 0; i < 4; i++) {
        dst_vtx[i] = src_vtx[i];
    }

    // Adjust X position
    dst_vtx[0].v.ob[0] += 0x10;
    dst_vtx[2].v.ob[0] += 0x10;

    // Adjust Y position
    dst_vtx[0].v.ob[1] -= 0x10;
    dst_vtx[1].v.ob[1] -= 0x10;

    return dst_vtx;
}

static void draw_icon(z2_gfx_t *gfx, const Vtx *vtx, u32 seg_addr, u16 width, u16 height, u16 qidx) {
    z2_disp_buf_t *db = &(gfx->poly_opa);
    // Instructions that happen before function
    gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, gfx->game->pause_ctxt.item_alpha & 0xFF);
    gSPVertex(db->p++, vtx, 4, 0); // Loads 4 vertices from RDRAM
    // Instructions that happen during function.
    gDPSetTextureImage(db->p++, G_IM_FMT_RGBA, G_IM_SIZ_32b, 1, (void*)seg_addr);
    gDPSetTile(db->p++, G_IM_FMT_RGBA, G_IM_SIZ_32b, 0, 0, G_TX_LOADTILE, 0, 0, 0, 0, 0, 0, 0);
    gDPLoadSync(db->p++);
    gDPLoadBlock(db->p++, 7, 0, 0, 0x400 - 1, 0x80);
    gDPPipeSync(db->p++);
    gDPSetTile(db->p++, G_IM_FMT_RGBA, G_IM_SIZ_32b, 8, 0, G_TX_RENDERTILE, 0, 0, 0, 0, 0, 0, 0);
    gDPSetTileSize(db->p++, 0, 0, 0, (width - 1) * 4, (height - 1) * 4);
    gSP1Quadrangle(db->p++, qidx + 0, qidx + 2, qidx + 3, qidx + 1, 0);
}

static void cycle_quest_item(z2_game_t *game, u8 item, u8 slot) {
    u8 orig = z2_file.inventory[slot];

    // Replace item in inventory
    z2_file.inventory[slot] = item;

    // Replace item in C buttons
    for (int i = 1; i < 4; i++) {
        if (orig != Z2_ITEM_NONE && z2_file.form_button_item[0].button_item[i] == orig) {
            z2_file.form_button_item[0].button_item[i] = item;
            z2_ReloadButtonTexture(game, i);
        }
    }

    // Play sound effect
    z2_PlaySfx(0x4808);
}

static bool is_quest_item_in_correct_slot(u8 item, int slot) {
    int cell;
    return quest_items_get_slot(&cell, item) && cell == slot;
}

static bool is_quest_item_with_storage_selected(z2_game_t *game) {
    // Get cell and selected item.
    s16 cell = game->pause_ctxt.cells_1.item;
    u8 item = z2_file.items[cell];

    // Check if on a quest item slot.
    bool quest = IS_QUEST_SLOT(cell);

    // Verify we are in the right cell for this item.
    bool correct_slot = is_quest_item_in_correct_slot(item, cell);

    // Check if there's a next item.
    u8 next = quest_item_storage_next(&SAVE_FILE_CONFIG.quest_storage, item);

    // Check if on "Z" or "R" side buttons.
    bool side = game->pause_ctxt.side_button != 0;

    return (quest && correct_slot && !side && item != Z2_ITEM_NONE && next != Z2_ITEM_NONE);
}

/**
 * Hook function called during the draw loop for item icons on the "Select Item" subscreen.
 *
 * Used to draw the next quest item in storage for quest item slots.
 **/
void pause_menu_select_item_draw_icon(z2_gfx_t *gfx, u8 item, u16 width, u16 height, int slot, u16 quad_idx, u32 vert_idx) {
    // Call original function to draw underlying item texture
    u32 orig_seg_addr = z2_item_segaddr_table[item];
    z2_PauseDrawItemIcon(gfx, orig_seg_addr, width, height, quad_idx);

    // If quest item storage, draw next quest item texture on bottom-right of current texture
    if (MISC_CONFIG.quest_item_storage && is_quest_item_in_correct_slot(item, slot)) {
        struct quest_item_storage *storage = &SAVE_FILE_CONFIG.quest_storage;
        if (quest_item_storage_has(storage, item)) {
            int sslot, unused;
            u8 next = quest_item_storage_next(storage, item);
            if (next != Z2_ITEM_NONE && quest_item_storage_get_slot(&sslot, &unused, next)) {
                u32 seg_addr = z2_item_segaddr_table[next];
                Vtx *vtx = get_vtx_buffer(gfx->game, vert_idx, sslot);
                draw_icon(gfx, vtx, seg_addr, width, height, quad_idx);
            }
        }
    }
}

/**
 * Hook function called after the main processing for the "Select Item" subscreen.
 *
 * Used to set the text on the A button to "Decide" for selecting quest items.
 **/
void pause_menu_select_item_subscreen_after_process(z2_game_t *game) {
    if (MISC_CONFIG.quest_item_storage) {
        u16 text = game->hud_ctxt.a_text_current;
        if (is_quest_item_with_storage_selected(game)) {
            // Set A button text to "Decide" (only if on "Info")
            if (text == Z2_BUTTON_TEXT_INFO) {
                z2_HudSetAButtonText(game, Z2_BUTTON_TEXT_DECIDE);
            }
        } else {
            // Set A button text to "Info" (only if on "Decide")
            if (text == Z2_BUTTON_TEXT_DECIDE) {
                z2_HudSetAButtonText(game, Z2_BUTTON_TEXT_INFO);
            }
        }
    }
}

/**
 * Hook function called before processing A button input on the "Select Item" subscreen.
 *
 * Checks if A button would be used to cycle quest items.
 **/
bool pause_menu_select_item_process_a_button(z2_game_t *game, u32 cur_val, u32 none_val) {
    if (MISC_CONFIG.quest_item_storage && is_quest_item_with_storage_selected(game)) {
        s16 cell = game->pause_ctxt.cells_1.item;
        if (cur_val != none_val) {
            u8 item = (u8)cur_val;
            // Check input for A button, and swap to next quest item.
            z2_pad_t pad = game->common.input->pad_pressed;
            u8 next = quest_item_storage_next(&SAVE_FILE_CONFIG.quest_storage, item);
            if (pad.a && next != Z2_ITEM_NONE) {
                game->common.input->pad_pressed.a = 0;
                cycle_quest_item(game, next, (u8)cell);
            }
        }
    }

    // Perform original check.
    return cur_val == none_val;
}

/**
 * Hook function called before determining if the A button should be shown as enabled or not.
 *
 * Checks if a quest item with storage is selected. If so, always show the A button as enabled.
 **/
bool pause_menu_select_item_show_a_button_enabled(z2_game_t *game) {
    if (MISC_CONFIG.quest_item_storage && is_quest_item_with_storage_selected(game)) {
        // If on a quest item with storage, show A button as enabled even during "Item Prompt."
        return true;
    } else {
        // Perform original check.
        return game->msgbox_ctxt.unk_0x1F10 == 0;
    }
}

/**
 * Hook function called while on pause menu before processing each frame.
 **/
void pause_menu_before_update(z2_game_t *game) {
    // Update pause menu colors.
    hud_colors_update_pause_menu_colors(game);
}
