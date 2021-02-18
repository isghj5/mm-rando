#ifndef TEXT_H
#define TEXT_H

#include "z2.h"

void text_init(void);
void text_print(const char *s, int left, int top);
void text_print_with_color(const char *s, int left, int top, z2_color_rgba8_t color);
void text_flush(z2_disp_buf_t *db);

#endif // TEXT_H
