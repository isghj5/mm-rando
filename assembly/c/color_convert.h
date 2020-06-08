#ifndef COLOR_CONVERT_H
#define COLOR_CONVERT_H

typedef struct rgbf_s {
    double r;       // a fraction between 0 and 1
    double g;       // a fraction between 0 and 1
    double b;       // a fraction between 0 and 1
} rgbf_t;

typedef struct hsvf_s {
    double h;       // angle in degrees
    double s;       // a fraction between 0 and 1
    double v;       // a fraction between 0 and 1
} hsvf_t;

rgbf_t color_convert_hsv2rgb(hsvf_t in);
hsvf_t color_convert_rgb2hsv(rgbf_t in);

#endif // COLOR_CONVERT_H
