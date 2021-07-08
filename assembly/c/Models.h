#ifndef MODELS_H
#define MODELS_H

#include <z64.h>

struct Model {
    u16 objectId;
    u8 graphicId;
};

void Models_AfterActorDtor(Actor* actor);
void Models_AfterPrepareDisplayBuffers(GraphicsContext* gfx);
void Models_ClearObjectHeap(void);
void Models_Init(void);

#endif // MODELS_H
