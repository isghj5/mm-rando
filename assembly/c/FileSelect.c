#include <z64.h>
#include "Dpad.h"
#include "FileSelect.h"
#include "HudColors.h"
#include "Misc.h"
#include "SaveFile.h"
#include "Sprite.h"
#include "Util.h"

// Enables using Z to refresh file select hash icons.
// #define FILE_HASH_DEBUG

// Number of icons to display using hash value.
const static int gIconCount = 5;

struct HashIcons HASH_ICONS = {
    .version = 0,
    .count = HASH_SYMBOL_COUNT,
    // Texture indexes to use for hash icons.
    .symbols = {
        ITEM_OCARINA,
        ITEM_BOW,
        ITEM_FIRE_ARROW,
        ITEM_ICE_ARROW,
        ITEM_LIGHT_ARROW,
        ITEM_BOMB,
        ITEM_BOMBCHU,
        ITEM_DEKU_STICK,
        ITEM_DEKU_NUT,
        ITEM_MAGIC_BEAN,
        ITEM_POWDER_KEG,
        ITEM_PICTOGRAPH,
        ITEM_LENS,
        ITEM_HOOKSHOT,
        ITEM_FAIRY_SWORD,
        ITEM_EMPTY_BOTTLE,
        ITEM_DEKU_PRINCESS,
        ITEM_BUGS,
        ITEM_BIG_POE,
        ITEM_HOT_WATER,
        ITEM_ZORA_EGG,
        ITEM_GOLD_DUST,
        ITEM_MUSHROOM,
        ITEM_SEAHORSE,
        ITEM_CHATEAU_ROMANI,
        ITEM_MOON_TEAR,
        ITEM_TOWN_DEED,
        ITEM_ROOM_KEY,
        ITEM_MAMA_LETTER,
        ITEM_KAFEI_LETTER,
        ITEM_PENDANT,
        ITEM_MAP,
        ITEM_DEKU_MASK,
        ITEM_GORON_MASK,
        ITEM_ZORA_MASK,
        ITEM_FIERCE_DEITY_MASK,
        ITEM_MASK_OF_TRUTH,
        ITEM_KAFEI_MASK,
        ITEM_ALL_NIGHT_MASK,
        ITEM_BUNNY_HOOD,
        ITEM_KEATON_MASK,
        ITEM_GARO_MASK,
        ITEM_ROMANI_MASK,
        ITEM_CIRCUS_LEADER_MASK,
        ITEM_POSTMAN_HAT,
        ITEM_COUPLE_MASK,
        ITEM_GREAT_FAIRY_MASK,
        ITEM_GIBDO_MASK,
        ITEM_DON_GERO_MASK,
        ITEM_KAMARO_MASK,
        ITEM_CAPTAIN_HAT,
        ITEM_STONE_MASK,
        ITEM_BREMEN_MASK,
        ITEM_BLAST_MASK,
        ITEM_MASK_OF_SCENTS,
        ITEM_GIANT_MASK,
        ITEM_KOKIRI_SWORD,
        ITEM_GILDED_SWORD,
        ITEM_HELIX_SWORD,
        ITEM_HERO_SHIELD,
        ITEM_MIRROR_SHIELD,
        ITEM_QUIVER_40,
        ITEM_ADULT_WALLET,
        0x61, // Bomber's Notebook
    },
};

static void LoadTexture(u8* buf, int idx, int length, u8 item) {
    u32 phys = z2_GetFilePhysAddr(ItemTextureFileVROM);
    u8* dest = buf + (idx * length);
    z2_LoadFileFromArchive(phys, item, dest, length);
}

static void UpdateTextures(void* buf, int count, int length, u32 hash) {
    for (int i = 0; i < count; i++, hash <<= 6) {
        u32 sym = (hash >> 26);
        u8 item = HASH_ICONS.symbols[sym];
        LoadTexture(buf, i, length, item);
    }
}

static void UpdateTexturesFromSprite(Sprite* sprite, int count, u32 hash) {
    int tilelen = Sprite_GetBytesPerTile(sprite);
    UpdateTextures(sprite->buf, count, tilelen, hash);
}

void FileSelect_HookAfterCtor(GlobalContext* ctxt) {
    // Consider D-Pad item textures cleared so they are reloaded next time
    Dpad_ClearItemTextures();
    // Clear data relevant to save file (including quest item storage).
    SaveFile_Clear();
    // Write icon textures
    Sprite* sprite = Sprite_GetItemTexturesSprite();
    if (sprite->buf != NULL) {
        u32 hash = MISC_CONFIG.hash.value;
        UpdateTexturesFromSprite(sprite, gIconCount, hash);
    }
}

void FileSelect_HookAfterDtor(GlobalContext* ctxt) {
    // Unused.
}

void FileSelect_BeforeDraw(GlobalContext* ctxt) {
    // Update colors for HUD elements on file select
    HudColors_FileChooseInit();

#ifdef FILE_HASH_DEBUG
    // Generate next seed
    z2_RngInt();

    // When pressing Z, update file hash to random new value
    Sprite* sprite = Sprite_GetItemTexturesSprite();
    InputPad padPressed = ctxt->state.input[0].pressEdge.buttons;
    if (padPressed.z && MISC_CONFIG.flags.drawHash && sprite->buf != NULL) {
        MISC_CONFIG.hash.value = z2_RngInt();
        UpdateTexturesFromSprite(sprite, gIconCount, MISC_CONFIG.hash.value);
        z2_PlaySfx(0x483B);
    }
#endif
}

void FileSelect_DrawHash(GlobalContext* ctxt) {
    int iconSize = 24;
    int padding = 8;
    int width = (gIconCount * iconSize) + ((gIconCount - 1) * padding);
    int left = (SCREEN_WIDTH - width) / 2;
    int top = 12;

    if (MISC_CONFIG.flags.drawHash) {
        DispBuf* db = &ctxt->state.gfxCtx->polyOpa;

        // Call setup display list
        gSPDisplayList(db->p++, &gSetupDb);

        gDPPipeSync(db->p++);
        gDPSetCombineMode(db->p++, G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM);
        gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, 0xFF);

        Sprite* sprite = Sprite_GetItemTexturesSprite();
        for (int i = 0; i < gIconCount; i++) {
            Sprite_Load(db, sprite, i, 1);
            Sprite_Draw(db, sprite, 0, left, top, iconSize, iconSize);
            left += iconSize + padding;
        }
    }
}
