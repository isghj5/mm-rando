#ifndef COLOR_H
#define COLOR_H

#include <z64.h>

#define Color_ConvertToInt(Color) (u32)(((Color).r << 24) | ((Color).g << 16) | ((Color).b << 8) | (Color).a)
#define Color_ConvertToIntWithAlpha(Color, Alpha) (u32)(((Color).r << 24) | ((Color).g << 16) | ((Color).b << 8) | ((Alpha) & 0xFF))

enum ColorSpecialFlag {
    COLOR_SPECIAL_INSTANCE = 0x01,
};

ColorRGB8 Color_RandomizeHue(ColorRGB8 color);
ColorRGB8 Color_SetHue(ColorRGB8 color, double hue);

#endif // COLOR_H
