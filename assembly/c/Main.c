#include <stdbool.h>
#include <z64.h>
#include "ActorExt.h"
#include "Dpad.h"
#include "HudColors.h"
#include "Misc.h"
#include "MMR.h"
#include "Models.h"
#include "Sprite.h"
#include "Text.h"
#include "Util.h"
#include "WorldColors.h"

void Main_CInit(void) {
    Util_HeapInit();
    Sprite_Init();
    Dpad_Init();
    HudColors_Init();
    ActorExt_Init();
    Models_Init();
    MMR_Init();
    Misc_Init();
    Text_Init();
    WorldColors_Init();
}
