#ifndef COLOR_H
#define COLOR_H

#include "types.h"

enum color_special_flag {
    COLOR_SPECIAL_INSTANCE = 0x01,
};

typedef struct rgb_s {
    union {
        struct {
            u8 r;
            u8 g;
            u8 b;
        };
        u8     bytes[3];
    };
} rgb_t;

typedef struct rgbs_s {
    rgb_t rgb;
    u8 s;
} rgbs_t;

rgb_t color_randomize_hue(rgb_t color);
u32 color_rgb2int(rgb_t color, u8 alpha);
rgb_t color_set_hue(rgb_t color, double hue);

#endif // COLOR_H
