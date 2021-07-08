#include <z64.h>
#include "Sprite.h"
#include "Util.h"

extern char DPAD_TEXTURE;
#define DpadTextureRaw ((u8*)&DPAD_TEXTURE)

extern u8 FONT_TEXTURE[];

Gfx gSetupDb[] = {
    gsDPPipeSync(),
    gsSPLoadGeometryMode(0),
    gsDPSetScissor(G_SC_NON_INTERLACE, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT),
    gsDPSetOtherMode(G_AD_DISABLE | G_CD_DISABLE |
        G_CK_NONE | G_TC_FILT |
        G_TD_CLAMP | G_TP_NONE |
        G_TL_TILE | G_TT_NONE |
        G_PM_NPRIMITIVE | G_CYC_1CYCLE |
        G_TF_BILERP, // HI
        G_AC_NONE | G_ZS_PRIM |
        G_RM_XLU_SURF | G_RM_XLU_SURF2), // LO
    gsSPEndDisplayList(),
};

Sprite gSpriteDpad = {
    NULL, 32, 32, 1,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

Sprite gSpriteFont = {
    NULL, 8, 14, 95,
    G_IM_FMT_IA, G_IM_SIZ_8b, 1
};

Sprite gSpriteIcon = {
    NULL, 32, 32, 97,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

Sprite gSpriteIcon24 = {
    NULL, 24, 24, 12,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

Sprite gSpriteFairy = {
    NULL, 32, 24, 4,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

// Sprite containing 5 item textures.
// Depending on the game state, this is used for either the file select hash icons, or the d-pad icons.
static Sprite gItemTexturesSprite = {
    NULL, 32, 32, 5,
    G_IM_FMT_RGBA, G_IM_SIZ_32b, 4
};

int Sprite_GetBytesPerTile(Sprite* sprite) {
    return sprite->tileW * sprite->tileH * sprite->bytesPerTexel;
}

int Sprite_GetBytesTotal(Sprite* sprite) {
    return sprite->tileCount * Sprite_GetBytesPerTile(sprite);
}

void Sprite_Load(DispBuf* db, Sprite* sprite, int startTile, int tileCount) {
    int width = sprite->tileW;
    int height = sprite->tileH * tileCount;
    gDPLoadTextureTile(db->p++,
            sprite->buf + (startTile * Sprite_GetBytesPerTile(sprite)),
            sprite->imFmt, sprite->imSiz,
            width, height,
            0, 0,
            width - 1, height - 1,
            0,
            G_TX_WRAP, G_TX_WRAP,
            G_TX_NOMASK, G_TX_NOMASK,
            G_TX_NOLOD, G_TX_NOLOD);
}

void Sprite_Draw(DispBuf* db, Sprite* sprite, int tileIndex, int left, int top, int width, int height) {
    int widthFactor = (1<<10) * sprite->tileW / width;
    int heightFactor = (1<<10) * sprite->tileH / height;
    gSPTextureRectangle(db->p++,
            left<<2, top<<2,
            (left + width)<<2, (top + height)<<2,
            0,
            0, (tileIndex * sprite->tileH)<<5,
            widthFactor, heightFactor);
}

Sprite* Sprite_GetItemTexturesSprite(void) {
    return &gItemTexturesSprite;
}

void Sprite_Init(void) {
    gSpriteDpad.buf = DpadTextureRaw;

    // Allocate space for item textures
    int size = Sprite_GetBytesTotal(&gItemTexturesSprite);
    gItemTexturesSprite.buf = Util_HeapAlloc(size);

    // Initialize fairy sprite.
    int fairyBytes = Sprite_GetBytesTotal(&gSpriteFairy);
    gSpriteFairy.buf = Util_HeapAlloc(fairyBytes);
    u8* temp = (u8*)0x80780000;
    const u32 dataOffset = 0x1B80;
    z2_DmaMgr_SendRequest0(temp, 0xA0A000, 0x4B80);
    z2_memcpy(gSpriteFairy.buf, temp + dataOffset, fairyBytes);

    // Initialize font texture buffer.
    int fontBytes = Sprite_GetBytesTotal(&gSpriteFont);
    gSpriteFont.buf = Util_HeapAlloc(fontBytes);
    for (int i = 0; i < fontBytes / 2; i++) {
        gSpriteFont.buf[2*i] = (FONT_TEXTURE[i] >> 4) | 0xF0;
        gSpriteFont.buf[2*i + 1] = FONT_TEXTURE[i] | 0xF0;
    }
}
