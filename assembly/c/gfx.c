#include "gfx.h"
#include "util.h"

extern char DPAD_TEXTURE;
#define dpad_texture_raw ((u8 *)&DPAD_TEXTURE)

Gfx setup_db[] =
{
    gsDPPipeSync(),

    gsSPLoadGeometryMode(0),
    gsDPSetScissor(G_SC_NON_INTERLACE,
                  0, 0, Z2_SCREEN_WIDTH, Z2_SCREEN_HEIGHT),

    gsDPSetOtherMode(G_AD_DISABLE | G_CD_DISABLE |
        G_CK_NONE | G_TC_FILT |
        G_TD_CLAMP | G_TP_NONE |
        G_TL_TILE | G_TT_NONE |
        G_PM_NPRIMITIVE | G_CYC_1CYCLE |
        G_TF_BILERP, // HI
        G_AC_NONE | G_ZS_PRIM |
        G_RM_XLU_SURF | G_RM_XLU_SURF2), // LO

    gsSPEndDisplayList()
};

sprite_t dpad_sprite = {
    NULL, 32, 32, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

// Sprite containing 5 item textures.
// Depending on the game state, this is used for either the file select hash icons, or the d-pad icons.
static sprite_t g_item_textures_sprite = {
    NULL, 32, 32, 5,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

int sprite_bytes_per_tile(sprite_t *sprite) {
    return sprite->tile_w * sprite->tile_h * sprite->bytes_per_texel;
}

int sprite_bytes(sprite_t *sprite) {
    return sprite->tile_count * sprite_bytes_per_tile(sprite);
}

void sprite_load(z2_disp_buf_t *db, sprite_t *sprite,
        int start_tile, int tile_count) {
    int width = sprite->tile_w;
    int height = sprite->tile_h * tile_count;
    gDPLoadTextureTile(db->p++,
            sprite->buf + (start_tile * sprite_bytes_per_tile(sprite)),
            sprite->im_fmt, sprite->im_siz,
            width, height,
            0, 0,
            width - 1, height - 1,
            0,
            G_TX_WRAP, G_TX_WRAP,
            G_TX_NOMASK, G_TX_NOMASK,
            G_TX_NOLOD, G_TX_NOLOD);
}

void sprite_draw(z2_disp_buf_t *db, sprite_t *sprite, int tile_index,
        int left, int top, int width, int height) {
    int width_factor = (1<<10) * sprite->tile_w / width;
    int height_factor = (1<<10) * sprite->tile_h / height;

    gSPTextureRectangle(db->p++,
            left<<2, top<<2,
            (left + width)<<2, (top + height)<<2,
            0,
            0, (tile_index * sprite->tile_h)<<5,
            width_factor, height_factor);
}

sprite_t* gfx_get_item_textures_sprite(void) {
    return &g_item_textures_sprite;
}

void gfx_init() {
    dpad_sprite.buf = dpad_texture_raw;

    // Allocate space for item textures
    int size = sprite_bytes(&g_item_textures_sprite);
    g_item_textures_sprite.buf = heap_alloc(size);
}
