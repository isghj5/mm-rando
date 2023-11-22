#ifndef GIANT_MASK_H
#define GIANT_MASK_H

#include <stdbool.h>
#include <z64.h>

void GiantMask_Handle(ActorPlayer* player, GlobalContext* globalCtx);
f32 GiantMask_GetScaleModifier();
f32 GiantMask_GetSimpleScaleModifier();
f32 GiantMask_GetSimpleInvertedScaleModifier();
f32 GiantMask_GetNextScaleFactor();
bool GiantMask_IsGiant();
void GiantMask_SetIsGiant(bool value);
void GiantMask_MarkReset();
void GiantMask_TryReset();
void GiantMask_ClearState();

#endif // GIANT_MASK_H
