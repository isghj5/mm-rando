#include <z64.h>
#include "Color.h"
#include "ColorConvert.h"

static double EnforceHueBoundary(double hue) {
    if (hue < 0.0) {
        hue = 0.0;
    } else if (hue > 360.0) {
        hue = 360.0;
    }
    return hue;
}

static double GetRandomDouble(u32 roof) {
    // This is really lazy and re-do later maybe.
    u32 value = z2_RngInt() % roof;
    return (double)value;
}

static ColorRgbF ByteToFloat(ColorRGB8 in) {
    ColorRgbF result;
    result.r = (double)in.r / 255.0;
    result.g = (double)in.g / 255.0;
    result.b = (double)in.b / 255.0;
    return result;
}

static ColorRGB8 FloatToByte(ColorRgbF in) {
    ColorRGB8 result;
    result.r = (u8)(in.r * 255.0);
    result.g = (u8)(in.g * 255.0);
    result.b = (u8)(in.b * 255.0);
    return result;
}

ColorRGB8 Color_RandomizeHue(ColorRGB8 color) {
    double hue = GetRandomDouble(360);
    return Color_SetHue(color, hue);
}

ColorRGB8 Color_SetHue(ColorRGB8 color, double hue) {
    // Enforce hue boundary range.
    hue = EnforceHueBoundary(hue);
    // Get HSV (float) values from RGB (int).
    ColorRgbF temp = ByteToFloat(color);
    ColorHsvF hsv = ColorConvert_RgbToHsv(temp);
    // Set hue value and convert back to RGB (int).
    hsv.h = hue;
    ColorRgbF result = ColorConvert_HsvToRgb(hsv);
    return FloatToByte(result);
}
