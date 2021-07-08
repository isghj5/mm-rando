#ifndef SPRITE_H
#define SPRITE_H

#include <z64.h>

extern Gfx gSetupDb[];

typedef struct {
    u8* buf;
    u16 tileW;
    u16 tileH;
    u16 tileCount;
    u8 imFmt;
    u8 imSiz;
    u8 bytesPerTexel;
} Sprite;

extern Sprite gSpriteDpad;
extern Sprite gSpriteFont;
extern Sprite gSpriteIcon;
extern Sprite gSpriteIcon24;
extern Sprite gSpriteFairy;

void Sprite_Draw(DispBuf* db, Sprite* sprite, int tileIndex, int left, int top, int width, int height);
int Sprite_GetBytesTotal(Sprite* sprite);
int Sprite_GetBytesPerTile(Sprite* sprite);
Sprite* Sprite_GetItemTexturesSprite(void);
void Sprite_Init(void);
void Sprite_Load(DispBuf* db, Sprite* sprite, int startTile, int tileCount);

#endif // SPRITE_H
