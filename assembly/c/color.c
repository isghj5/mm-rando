#include "color.h"
#include "color_convert.h"
#include "types.h"
#include "z2.h"

static double enforce_hue_boundary(double hue) {
    if (hue < 0.0)
        hue = 0.0;
    else if (hue > 360.0)
        hue = 360.0;
    return hue;
}

static double get_random_double(u32 roof) {
    // This is really lazy and re-do later maybe.
    u32 value = z2_RngInt() % roof;
    return (double)value;
}

static rgbf_t rgb_to_rgbf(rgb_t in) {
    rgbf_t result;
    result.r = (double)in.r / 255.0;
    result.g = (double)in.g / 255.0;
    result.b = (double)in.b / 255.0;
    return result;
}

static rgb_t rgbf_to_rgb(rgbf_t in) {
    rgb_t result;
    result.r = (u8)(in.r * 255.0);
    result.g = (u8)(in.g * 255.0);
    result.b = (u8)(in.b * 255.0);
    return result;
}

rgb_t color_randomize_hue(rgb_t color) {
    double hue = get_random_double(360);
    return color_set_hue(color, hue);
}

u32 color_rgb2int(rgb_t color, u8 alpha) {
    return (color.r << 24) | (color.g << 16) | (color.b << 8) | alpha;
}

rgb_t color_set_hue(rgb_t color, double hue) {
    // Enforce hue boundary range.
    hue = enforce_hue_boundary(hue);

    // Get HSV (float) values from RGB (int).
    rgbf_t temp = rgb_to_rgbf(color);
    hsvf_t hsv = color_convert_rgb2hsv(temp);

    // Set hue value and convert back to RGB (int).
    hsv.h = hue;
    rgbf_t rgb = color_convert_hsv2rgb(hsv);
    return rgbf_to_rgb(rgb);
}
