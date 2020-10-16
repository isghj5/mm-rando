#ifndef GFX_H
#define GFX_H

#include "z2.h"

extern Gfx setup_db[];

typedef struct {
    u8 *buf;
    u16 tile_w;
    u16 tile_h;
    u16 tile_count;
    u8 im_fmt;
    u8 im_siz;
    u8 bytes_per_texel;
} sprite_t;

//sprite_t items_sprite;
sprite_t dpad_sprite;
sprite_t font_sprite;
sprite_t icon_sprite;
sprite_t icon_24_sprite;
sprite_t fairy_sprite;

void gfx_init();
sprite_t* gfx_get_item_textures_sprite(void);

int sprite_bytes_per_tile(sprite_t *sprite);
void sprite_load(z2_disp_buf_t *db, sprite_t *sprite,
        int start_tile, int tile_count);
void sprite_draw(z2_disp_buf_t *db, sprite_t *sprite, int tile_index,
        int left, int top, int width, int height);

#endif // GFX_H
