#ifndef _COLOR_H_
#define _COLOR_H_

#include <PR/ultratypes.h>

typedef struct {
    /* 0x0 */ u8 r;
    /* 0x1 */ u8 g;
    /* 0x2 */ u8 b;
} ColorRGB8; // size = 0x3

typedef struct {
    /* 0x0 */ u8 r;
    /* 0x1 */ u8 g;
    /* 0x2 */ u8 b;
    /* 0x3 */ u8 a;
} ColorRGBA8; // size = 0x4

typedef struct {
    /* 0x0 */ u16 r;
    /* 0x2 */ u16 g;
    /* 0x4 */ u16 b;
} ColorRGB16; // size = 0x6

typedef struct {
    /* 0x0 */ u16 r1;
    /* 0x2 */ u16 r2;
    /* 0x4 */ u16 g1;
    /* 0x6 */ u16 g2;
    /* 0x8 */ u16 b1;
    /* 0xA */ u16 b2;
} ColorRRGGBB16; // size = 0xC

// General 32-bit color value with 8-bit RGBA values.
typedef union Color {
    struct {
        /* 0x0 */ u8 r;
        /* 0x1 */ u8 g;
        /* 0x2 */ u8 b;
        /* 0x3 */ u8 a;
    };
    struct {
        /* 0x0 */ ColorRGB8 rgb;
        /* 0x3 */ u8 pad;
    };
    ColorRGBA8 rgba;
    u8 bytes[4];
} Color; // size = 0x4

typedef ColorRGB8 RGB8;
typedef ColorRGB8 RGB;
typedef ColorRGBA8 RGBA8;
typedef ColorRGB16 RGB16;
typedef ColorRRGGBB16 RRGGBB16;

#endif
