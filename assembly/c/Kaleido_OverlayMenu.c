#include <stdbool.h>
#include <z64.h>
#include "Sprite.h"
#include "Text.h"

// Whether or not the overlay menu is enabled.
//static bool gEnable = true;

// Clock Town stray fairy icon image buffer, written to by randomizer.
u8 _Alignas (8) TOWN_FAIRY_BYTES[0xC00] = { 0 };

// Gold Skulltula HUD icon.
static Sprite gSkulltulaIcon = {
    NULL, 24, 24, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

// Dungeon stray fairy icons.
static Sprite gSpriteFairy = {
    NULL, 32, 24, 4,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

static void Kaleido_LoadAndDrawStrayFairyIconFlipped(DispBuf* db, Sprite* sprite, int tileIndex, int left, int top, int width, int height) {
    const int widthFactor = (1<<10) * sprite->tileW / width;
    const int heightFactor = (1<<10) * sprite->tileH / height;
    gDPLoadTextureBlock(db->p++,
            sprite->buf + (tileIndex * Sprite_GetBytesPerTile(sprite)),
            sprite->imFmt, sprite->imSiz,
            sprite->tileW, sprite->tileH,
            0, G_TX_NOMIRROR | G_TX_WRAP, G_TX_NOMIRROR | G_TX_WRAP, G_TX_NOMASK,
            G_TX_NOMASK, G_TX_NOLOD, G_TX_NOLOD
    );
    gSPTextureRectangle(db->p++,
            left<<2, top<<2,
            (left + width)<<2, (top + height)<<2,
            G_TX_RENDERTILE,
            0, 0,
            widthFactor, heightFactor);
}

// Clock Town stray fairy icon.
static Sprite gTownFairyIcon = {
    TOWN_FAIRY_BYTES, 32, 24, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

struct DungeonEntry {
    u8 index;
    u8 remains;
    u8 isDungeon;
    u8 hasFairies;
    u8 hasTokens;
    char name[9];
};

static int gDungeonCount = 7;

static struct DungeonEntry gDungeons[7] = {
    { 0, 0x5D, 1, 1, 0, "Woodfall" },
    { 1, 0x5E, 1, 1, 0, "Snowhead" },
    { 2, 0x5F, 1, 1, 0, "GreatBay" },
    { 3, 0x60, 1, 1, 0, "StoneTwr" },
    { 4, 0,    0, 1, 0, "ClockTwn" },
    { 5, 0,    0, 0, 1, "Swamp" },
    { 6, 0,    0, 0, 2, "Ocean" },
};

/**
 * Get text for a specific amount, with a limited digit count (1 or 2).
 **/
static void Kaleido_GetCountText(int amount, char* c, int digits) {
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
static bool Kaleido_HasRemains(u8 index) {
    return (gSaveContext.perm.inv.questStatus.value & (1 << index)) != 0;
}

/**
 * Try to draw overlay menu.
 **/
void Kaleido_OverlayMenu_Draw(GlobalContext* ctxt) {

    DispBuf* db = &ctxt->state.gfxCtx->overlay;
    db->p = db->buf;

    // Call setup display list.
    gSPDisplayList(db->p++, &gSetupDb);

    // General variables.
    int iconSize = 16;
    int padding = 1;
    int rows = 10;

    // Background rectangle.
    int bgWidth =
        (7 * iconSize) + 4 +
        (9 * gSpriteFont.tileW) +
        (9 * padding);
    int bgHeight = (rows * iconSize) + ((rows + 1) * padding);
    int bgLeft = (SCREEN_WIDTH - bgWidth) / 2;
    int bgTop = (SCREEN_HEIGHT - bgHeight) / 2;

    // Left & top starting positions for drawing columns.
    int left = bgLeft + padding;
    int startTop = bgTop + padding;

    // Update sprite pointers.
    gSpriteIcon.buf = ctxt->pauseCtx.iconItemStatic;
    gSpriteIcon24.buf = ctxt->pauseCtx.iconItem24;
    gSkulltulaIcon.buf = (u8*)ctxt->interfaceCtx.parameterStatic +0x31E0;
    gSpriteFairy.buf = (u8*)ctxt->interfaceCtx.parameterStatic +0x8998;

    // Draw background.
    gDPSetCombineMode(db->p++, G_CC_PRIMITIVE, G_CC_PRIMITIVE);
    gDPSetPrimColor(db->p++, 0, 0, 0x00, 0x00, 0x00, 0xD0);
    gSPTextureRectangle(db->p++,
        bgLeft << 2, bgTop << 2,
        (bgLeft + bgWidth) << 2, (bgTop + bgHeight) << 2,
        0,
        0, 0,
        1 << 10, 1 << 10);
    gDPPipeSync(db->p++);

    // Draw legend panel background.
    int legendLeft = bgLeft + iconSize + (9 * gSpriteFont.tileW) + (padding * 3);
    int legendTop = bgTop - (iconSize + (padding * 3));
    int legendWidth = (iconSize * 4) + (padding * 5);
    int legendHeight = iconSize + (padding * 2);
    gDPSetPrimColor(db->p++, 0, 0, 0x00, 0x00, 0x00, 0xA0);
    gSPTextureRectangle(db->p++,
        legendLeft << 2, legendTop << 2,
        (legendLeft + legendWidth) << 2, (legendTop + legendHeight) << 2,
        0,
        0, 0,
        1 << 10, 1 << 10);
    gDPPipeSync(db->p++);
    gDPSetCombineMode(db->p++, G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM);

    gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, 0xFF);

    // Draw legend panel icons: Small Key, Boss Key, Map, Compass.
    int legendIcons[4] = { 10, 6, 8, 7, };
    for (int i = 0; i < 4; i++) {
        int index = legendIcons[i];
        int lleft = legendLeft + ((iconSize + padding) * i);
        int top = legendTop + padding;
        Sprite_Load(db, &gSpriteIcon24, index, 1);
        Sprite_Draw(db, &gSpriteIcon24, 0, lleft, top, iconSize, iconSize);
    }

    // Draw remains.
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        if (d->isDungeon && Kaleido_HasRemains(d->index)) {
            int top = startTop + ((iconSize + padding) * i);
            Sprite_Load(db, &gSpriteIcon, d->remains, 1);
            Sprite_Draw(db, &gSpriteIcon, 0, left, top, iconSize, iconSize);
        }
    }
    left += iconSize + padding;

    // Draw names.
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        int top = startTop + ((iconSize + padding) * i) + 1;
        Text_Print(d->name, left, top);
    }
    left += (9 * gSpriteFont.tileW) + padding;

    // Draw small keys.
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        if (d->isDungeon) {
            // Get key count for dungeon.
            u8 keys = gSaveContext.perm.inv.dungeonKeys[d->index];
            // Get key count as text.
            char count[2] = "0";
            Kaleido_GetCountText(keys, count, 1);
            // Draw key count as text.
            int top = startTop + ((iconSize + padding) * i) + 1;
            Text_Print(count, left + 4, top);
        }
    }
    left += iconSize + padding;

    // Draw boss keys.
    Sprite_Load(db, &gSpriteIcon24, 6, 1);
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        if (d->isDungeon) {
            if (gSaveContext.perm.inv.dungeonItems[d->index].bossKey) {
                int top = startTop + ((iconSize + padding) * i);
                Sprite_Draw(db, &gSpriteIcon24, 0, left, top, iconSize, iconSize);
            }
        }
    }
    left += iconSize + padding;

    // Draw maps.
    Sprite_Load(db, &gSpriteIcon24, 8, 1);
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        if (d->isDungeon) {
            if (gSaveContext.perm.inv.dungeonItems[d->index].map) {
                int top = startTop + ((iconSize + padding) * i);
                Sprite_Draw(db, &gSpriteIcon24, 0, left, top, iconSize, iconSize);
            }
        }
    }
    left += iconSize + padding;

    // Draw compasses.
    Sprite_Load(db, &gSpriteIcon24, 7, 1);
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        if (d->isDungeon) {
            if (gSaveContext.perm.inv.dungeonItems[d->index].compass) {
                int top = startTop + ((iconSize + padding) * i);
                Sprite_Draw(db, &gSpriteIcon24, 0, left, top, iconSize, iconSize);
            }
        }
    }
    left += iconSize + padding;

    // Draw stray fairy, skulltula token icons.
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        int top = startTop + ((iconSize + padding) * i) - 2;
        if (d->hasFairies) {
            // Draw dungeon fairy icons (32-bit RGBA). Otherwise, draw Clock Town fairy icon.
            if (d->isDungeon) {
                Kaleido_LoadAndDrawStrayFairyIconFlipped(db, &gSpriteFairy, d->index, left, top, 20, 15);
            } else {
                // Draw Clock Town fairy icon.
                Sprite_Load(db, &gTownFairyIcon, 0, 1);
                Sprite_Draw(db, &gTownFairyIcon, 0, left, top, 20, 15);
            }
        } else if (d->hasTokens) {
            // Draw skulltula token icon.
            Sprite_Load(db, &gSkulltulaIcon, 0, 1);
            Sprite_DrawCropped(db, &gSkulltulaIcon, 0, left + 2, top, 16, 12, CROP(0, 6));
        }
    }
    left += 20 + padding;

    // Draw stray fairy count, skulltula token count.
    for (int i = 0; i < gDungeonCount; i++) {
        struct DungeonEntry* d = &gDungeons[i];
        int top = startTop + ((iconSize + padding) * i);
        // Get total count and maximum count for stray fairies or skulltula tokens.
        int total = 0;
        int maximum = 0;
        if (d->hasFairies) {
            // Get stray fairy count for dungeon or town.
            if (d->isDungeon) {
                // Get fairy count for dungeon.
                total = gSaveContext.perm.inv.strayFairies[d->index];
                maximum = 15;
            } else {
                total = gSaveContext.perm.weekEventReg.hasTownFairy ? 1 : 0;
                maximum = 1;
            }
        } else if (d->hasTokens) {
            // Get skulltula token count.
            total = gSaveContext.perm.skullTokens[0];
            if (d->hasTokens == 2) {
                total = gSaveContext.perm.skullTokens[1];
            }
            maximum = 30;
        }
        // Display count as text.
        if (d->hasFairies || d->hasTokens) {
            // Get count as text.
            char count[3] = " 0";
            Kaleido_GetCountText(total, count, 2);
            // Draw fairy/token count as text.
            if (total >= maximum) {
                // Use green text if at maximum.
                ColorRGBA8 color = { 0x78, 0xFF, 0x00, 0xFF };
                Text_PrintWithColor(count, left, top, color);
            } else {
                Text_Print(count, left, top);
            }
        }
    }

    // Flush text and finish.
    Text_Flush(db);
    gDPFullSync(db->p++);
    gSPEndDisplayList(db->p++);
}
