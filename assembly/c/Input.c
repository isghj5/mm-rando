#include <z64.h>

Input gPlayUpdateInput;

void Input_BeforePlayUpdate(Input* input) {
    gPlayUpdateInput = *input;
}
