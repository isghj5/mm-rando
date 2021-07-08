#ifndef COLOR_CONVERT_H
#define COLOR_CONVERT_H

typedef struct ColorRgbF {
    /* 0x00 */ double r; // A fraction between 0 and 1.
    /* 0x08 */ double g; // A fraction between 0 and 1.
    /* 0x10 */ double b; // A fraction between 0 and 1.
} ColorRgbF; // size = 0x18

typedef struct ColorHsvF {
    /* 0x00 */ double h; // Angle in degrees.
    /* 0x08 */ double s; // A fraction between 0 and 1.
    /* 0x10 */ double v; // A fraction between 0 and 1.
} ColorHsvF; // size = 0x18

ColorRgbF ColorConvert_HsvToRgb(ColorHsvF in);
ColorHsvF ColorConvert_RgbToHsv(ColorRgbF in);

#endif // COLOR_CONVERT_H
