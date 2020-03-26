#include "dpad.h"
#include "file_select.h"
#include "gfx.h"
#include "hud_colors.h"
#include "misc.h"
#include "save_file.h"
#include "util.h"
#include "z2.h"

// Enables using Z to refresh file select hash icons.
// #define FILE_HASH_DEBUG

// Number of icons to display using hash value.
const static int g_icon_count = 5;

struct hash_icons HASH_ICONS = {
    .version = 0,
    .count = HASH_SYMBOL_COUNT,
    // Texture indexes to use for hash icons.
    .symbols = {
        Z2_ITEM_OCARINA,
        Z2_ITEM_BOW,
        Z2_ITEM_FIRE_ARROW,
        Z2_ITEM_ICE_ARROW,
        Z2_ITEM_LIGHT_ARROW,
        Z2_ITEM_BOMB,
        Z2_ITEM_BOMBCHU,
        Z2_ITEM_DEKU_STICK,
        Z2_ITEM_DEKU_NUT,
        Z2_ITEM_MAGIC_BEAN,
        Z2_ITEM_POWDER_KEG,
        Z2_ITEM_PICTOGRAPH,
        Z2_ITEM_LENS,
        Z2_ITEM_HOOKSHOT,
        Z2_ITEM_FAIRY_SWORD,
        Z2_ITEM_EMPTY_BOTTLE,
        Z2_ITEM_DEKU_PRINCESS,
        Z2_ITEM_BUGS,
        Z2_ITEM_BIG_POE,
        Z2_ITEM_HOT_WATER,
        Z2_ITEM_ZORA_EGG,
        Z2_ITEM_GOLD_DUST,
        Z2_ITEM_MUSHROOM,
        Z2_ITEM_SEAHORSE,
        Z2_ITEM_CHATEAU_ROMANI,
        Z2_ITEM_MOON_TEAR,
        Z2_ITEM_TOWN_DEED,
        Z2_ITEM_ROOM_KEY,
        Z2_ITEM_MAMA_LETTER,
        Z2_ITEM_KAFEI_LETTER,
        Z2_ITEM_PENDANT,
        Z2_ITEM_MAP,
        Z2_ITEM_DEKU_MASK,
        Z2_ITEM_GORON_MASK,
        Z2_ITEM_ZORA_MASK,
        Z2_ITEM_FIERCE_DEITY_MASK,
        Z2_ITEM_MASK_OF_TRUTH,
        Z2_ITEM_KAFEI_MASK,
        Z2_ITEM_ALL_NIGHT_MASK,
        Z2_ITEM_BUNNY_HOOD,
        Z2_ITEM_KEATON_MASK,
        Z2_ITEM_GARO_MASK,
        Z2_ITEM_ROMANI_MASK,
        Z2_ITEM_CIRCUS_LEADER_MASK,
        Z2_ITEM_POSTMAN_HAT,
        Z2_ITEM_COUPLE_MASK,
        Z2_ITEM_GREAT_FAIRY_MASK,
        Z2_ITEM_GIBDO_MASK,
        Z2_ITEM_DON_GERO_MASK,
        Z2_ITEM_KAMARO_MASK,
        Z2_ITEM_CAPTAIN_HAT,
        Z2_ITEM_STONE_MASK,
        Z2_ITEM_BREMEN_MASK,
        Z2_ITEM_BLAST_MASK,
        Z2_ITEM_MASK_OF_SCENTS,
        Z2_ITEM_GIANT_MASK,
        Z2_ITEM_KOKIRI_SWORD,
        Z2_ITEM_GILDED_SWORD,
        Z2_ITEM_HELIX_SWORD,
        Z2_ITEM_HERO_SHIELD,
        Z2_ITEM_MIRROR_SHIELD,
        Z2_ITEM_QUIVER_40,
        Z2_ITEM_ADULT_WALLET,
        0x61, // Bomber's Notebook
    },
};

static void load_texture(u8 *buf, int idx, int length, u8 item) {
    u32 phys = z2_GetFilePhysAddr(z2_item_texture_file);
    u8 *dest = buf + (idx * length);
    z2_LoadFileFromArchive(phys, item, dest, length);
}

static void update_textures(void *buf, int count, int length, u32 hash) {
    for (int i = 0; i < count; i++, hash <<= 6) {
        u32 sym = (hash >> 26);
        u8 item = HASH_ICONS.symbols[sym];
        load_texture(buf, i, length, item);
    }
}

static void update_textures_from_sprite(sprite_t *sprite, int count, u32 hash) {
    int tilelen = sprite_bytes_per_tile(sprite);
    update_textures(sprite->buf, count, tilelen, hash);
}

void file_select_hook_after_ctor(z2_game_t *game) {
    // Consider D-Pad item textures cleared so they are reloaded next time
    dpad_clear_item_textures();

    // Clear data relevant to save file (including quest item storage).
    save_file_clear();

    // Write icon textures
    sprite_t *sprite = gfx_get_item_textures_sprite();
    if (sprite->buf != NULL) {
        struct misc_config *config = misc_get_config();
        u32 hash = config->hash.value;
        update_textures_from_sprite(sprite, g_icon_count, hash);
    }
}

void file_select_hook_after_dtor(z2_game_t *game) {
}

void file_select_before_draw(z2_game_t *game) {
    // Update colors for HUD elements on file select
    hud_colors_main_menu_init();

#ifdef FILE_HASH_DEBUG
    // Generate next seed
    z2_RngInt();

    // When pressing Z, update file hash to random new value
    sprite_t *sprite = gfx_get_item_textures_sprite();
    struct misc_config *config = misc_get_config();
    z2_pad_t pad_pressed = game->common.input[0].pad_pressed;
    if (pad_pressed.z && config->draw_hash && sprite->buf != NULL) {
        config->hash.value = z2_RngInt();
        update_textures_from_sprite(sprite, g_icon_count, config->hash.value);
        z2_PlaySfx(0x483B);
    }
#endif
}

void file_select_draw_hash(z2_game_t *game) {
    int icon_size = 24;
    int padding = 8;
    int width = (g_icon_count * icon_size) + ((g_icon_count - 1) * padding);
    int left = (Z2_SCREEN_WIDTH - width) / 2;
    int top = 12;

    struct misc_config *config = misc_get_config();
    if (config->draw_hash) {
        z2_disp_buf_t *db = &(game->common.gfx->poly_opa);

        // Call setup display list
        gSPDisplayList(db->p++, &setup_db);

        gDPPipeSync(db->p++);
        gDPSetCombineMode(db->p++, G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM);
        gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, 0xFF);

        sprite_t *sprite = gfx_get_item_textures_sprite();
        for (int i = 0; i < g_icon_count; i++) {
            sprite_load(db, sprite, i, 1);
            sprite_draw(db, sprite, 0, left, top, icon_size, icon_size);
            left += icon_size + padding;
        }
    }
}
