#ifndef TEXT_H
#define TEXT_H

#include <z64.h>

void Text_Init(void);
void Text_Print(const char* s, int left, int top);
void Text_PrintWithColor(const char* s, int left, int top, ColorRGBA8 color);
void Text_Flush(DispBuf* db);

#endif // TEXT_H
