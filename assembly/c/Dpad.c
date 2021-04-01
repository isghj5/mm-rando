#include <stdbool.h>
#include <z64.h>
#include "Buttons.h"
#include "Dpad.h"
#include "HudColors.h"
#include "Reloc.h"
#include "Sprite.h"
#include "Util.h"

// D-Pad configuration structure that can be set by a randomizer.
struct DpadConfig DPAD_CONFIG = {
    .magic = DPAD_CONFIG_MAGIC,
    .version = 0,
    .primary = { ITEM_NONE },
    .state = DPAD_STATE_DEFAULTS,
    .display = DPAD_DISPLAY_LEFT,
    .reserved = { 0 },
};

// Default D-Pad values that will be used if config values undefined.
const static u8 gDpadDefault[4] = {
    ITEM_DEKU_MASK,
    ITEM_ZORA_MASK,
    ITEM_OCARINA,
    ITEM_GORON_MASK,
};

// Indicates which item textures are currently loaded into our buffer.
static u8 gTextureItems[4] = {
    ITEM_NONE,
    ITEM_NONE,
    ITEM_NONE,
    ITEM_NONE,
};

// Position of D-Pad texture.
const static u16 gPosition[2][2] = {
    { 30,  60 },  // Left
    { 270, 75 },  // Right
};

// Positions of D-Pad item textures, relative to main texture.
const static s16 gPositions[4][2] = {
    { 1, -16 },
    { 15, 0 },
    { 1, 13 },
    { -15, 0 },
};

// Whether or not D-Pad items are usable, according to z2_UpdateButtonUsability.
static bool gUsable[4] = { false };

// Whether the previous frame was a "minigame" frame.
static bool gWasMinigame = false;

static bool HasInventoryItem(u8 item) {
    for (int i = 0; i < 0x18; i++) {
        if (gSaveContext.perm.inv.items[i] == item || gSaveContext.perm.inv.masks[i] == item) {
            return true;
        }
    }
    return false;
}

static bool HaveAny(const u8* dpad) {
    for (int i = 0; i < 4; i++) {
        if (HasInventoryItem(dpad[i])) {
            return true;
        }
    }
    return false;
}

static bool TryUseItem(GlobalContext* ctxt, ActorPlayer* player, u8 item) {
    // Try to find slot in item or mask inventories.
    if (HasInventoryItem(item)) {
        z2_UseItem(ctxt, player, item);
        return true;
    } else {
        return false;
    }
}

/**
 * Helper function used to check if C buttons are disabled due to the current entrance.
 **/
static bool AreCItemsDisabledByEntrance(GlobalContext* ctxt) {
    // Hardcoded entrance checks to disable C buttons, normally performed by function 0x80111CB4:
    // Id 0x8E10: Beaver Race
    // Id 0xD010: Goron Race
    // Checks execute state to prevent fading D-Pad when loading scene with entrance.
    return (gSaveContext.perm.entrance.value == 0x8E10 || gSaveContext.perm.entrance.value == 0xD010) && ctxt->state.running != 0;
}

static void GetDpadItemUsability(GlobalContext* ctxt, bool* dest) {
    for (int i = 0; i < 4; i++) {
        if (AreCItemsDisabledByEntrance(ctxt)) {
            dest[i] = false;
        } else {
            dest[i] = Buttons_CheckCItemUsable(ctxt, DPAD_CONFIG.primary.values[i]);
        }
    }
}

static bool IsAnyItemUsable(const u8* dpad, const bool* usable) {
    for (int i = 0; i < 4; i++) {
        if (HasInventoryItem(dpad[i]) && usable[i]) {
            return true;
        }
    }
    return false;
}

static void LoadTexture(u8* buf, int idx, int length, u8 item) {
    u32 phys = z2_GetFilePhysAddr(ItemTextureFileVROM);
    u8* dest = buf + (idx * length);
    z2_LoadFileFromArchive(phys, item, dest, length);
    gTextureItems[idx] = item;
}

static void LoadTextureFromSprite(Sprite* sprite, int idx, u8 item) {
    int tileLen = Sprite_GetBytesPerTile(sprite);
    LoadTexture(sprite->buf, idx, tileLen, item);
}

static u16 UpdateYPosition(u16 x, u16 y, u16 padding) {
    u16 heartCount = gSaveContext.perm.unk24.maxLife / 0x10;

    // Check if we have second row of hearts
    bool hearts = heartCount > 10;
    // Check if we have magic
    bool magic = gSaveContext.perm.unk24.hasMagic != 0;
    // Check if there's a timer
    bool timer = IS_TIMER_VISIBLE(gSaveContext.extra.timers[TIMER_INDEX_POE_SISTERS]) ||
                 IS_TIMER_VISIBLE(gSaveContext.extra.timers[TIMER_INDEX_TREASURE_CHEST_GAME]) ||
                 IS_TIMER_VISIBLE(gSaveContext.extra.timers[TIMER_INDEX_DROWNING]);

    // If on left-half of screen
    if (x < 160) {
        // Calculate a minimum y position based on heart rows and magic
        // This is to avoid the D-Pad textures interfering with the hearts/magic UI
        u16 minimum = 50 + padding;
        if (hearts)
            minimum += 10;
        if (magic)
            minimum += 16;
        if (timer) {
            if (magic) {
                minimum += 10;
            } else {
                minimum += 26;
            }
        }
        y = (y > minimum ? y : minimum);
    }

    return y;
}

static bool IsMinigameFrame(void) {
    bool result = false;

    if (gWasMinigame) {
        result = true;
    }

    // Note on state 6:
    // If on Epona, and holding "B" for bow, then press "A" while holding "B", the game state
    // will go from: 0xC, 0x6, 0x32, 0xC. Thus we need to mark 0x6 as a "minigame frame" as well.
    // Riding Epona to a new area: 0xC, 0x32, 0x6, 0x1, 0x1...
    //
    // Note on state 1 (transition):
    // In the Deku playground, can go from 0xC to 0x1 when cutscene-transitioning to the business scrub.
    // Thus, if the minigame state goes directly to the transition state, consider that a minigame frame.
    gWasMinigame = (gSaveContext.extra.buttonsState.state == BUTTONS_STATE_MINIGAME ||
                   (gWasMinigame && gSaveContext.extra.buttonsState.state == BUTTONS_STATE_TRANSITION) ||
                    gSaveContext.extra.buttonsState.state == 6);
    return result || gWasMinigame;
}

void Dpad_BeforePlayerActorUpdate(ActorPlayer* player, GlobalContext* ctxt) {
    // If disabled, do nothing
    if (DPAD_CONFIG.state == DPAD_STATE_DISABLED) {
        return;
    }

    // Update usability flags for later use in Dpad_Draw.
    GetDpadItemUsability(ctxt, gUsable);
}

void Dpad_ClearItemTextures(void) {
    for (int i = 0; i < 4; i++) {
        gTextureItems[i] = ITEM_NONE;
    }
}

void Dpad_Init(void) {
    // If using default values, overwrite DPAD_CONFIG items with defaults
    if (DPAD_CONFIG.state == DPAD_STATE_DEFAULTS) {
        for (int i = 0; i < 4; i++) {
            DPAD_CONFIG.primary.values[i] = gDpadDefault[i];
        }
    }
}

bool Dpad_IsEnabled(void) {
    return (DPAD_CONFIG.state == DPAD_STATE_ENABLED)
        || (DPAD_CONFIG.state == DPAD_STATE_DEFAULTS);
}

/**
 * Hook function used to check whether or not to call z2_UseItem.
 *
 * Checks D-Pad input for whether or not to use an item, and if so returns true to exit the caller
 * function early.
 **/
bool Dpad_Handle(ActorPlayer* player, GlobalContext* ctxt) {
    InputPad padPressed = ctxt->state.input[0].pressEdge.buttons;

    // If disabled, do nothing
    if (DPAD_CONFIG.state == DPAD_STATE_DISABLED) {
        return false;
    }

    // Check general buttons state to know if we can use C buttons at all
    // Note: After collecting a stray fairy (and possibly in other cases) the state flags are set
    // to 0 despite the game running normally.
    if (gSaveContext.extra.buttonsState.state != BUTTONS_STATE_NORMAL &&
        gSaveContext.extra.buttonsState.state != BUTTONS_STATE_BLACK_SCREEN) {
        return false;
    }

    // Make sure certain Link state flags are cleared before processing D-Pad input.
    u32 flags1 = PLAYER_STATE1_HOLD | PLAYER_STATE1_MOVE_SCENE | PLAYER_STATE1_EPONA;
    if ((player->stateFlags.state1 & flags1) != 0) {
        return false;
    }

    if (padPressed.du && gUsable[0]) {
        return TryUseItem(ctxt, player, DPAD_CONFIG.primary.du);
    } else if (padPressed.dr && gUsable[1]) {
        return TryUseItem(ctxt, player, DPAD_CONFIG.primary.dr);
    } else if (padPressed.dd && gUsable[2]) {
        return TryUseItem(ctxt, player, DPAD_CONFIG.primary.dd);
    } else if (padPressed.dl && gUsable[3]) {
        return TryUseItem(ctxt, player, DPAD_CONFIG.primary.dl);
    }

    return false;
}

/**
 * Hook function called directly before drawing triangles and item textures on C buttons.
 *
 * Draws D-Pad textures to the overlay display list.
 **/
void Dpad_Draw(GlobalContext* ctxt) {
    bool isMinigame = IsMinigameFrame();

    // If disabled or hiding, don't draw
    if (DPAD_CONFIG.state == DPAD_STATE_DISABLED || DPAD_CONFIG.display == DPAD_DISPLAY_NONE) {
        return;
    }

    // If we don't have any D-Pad items, draw nothing
    if (!HaveAny(DPAD_CONFIG.primary.values)) {
        return;
    }

    // Check for minigame frame, and do nothing unless transitioning into minigame
    // In which case the C-buttons alpha will be used instead for fade-in
    if (isMinigame && gSaveContext.extra.buttonsState.previousState != BUTTONS_STATE_MINIGAME) {
        return;
    }

    // Check if C button items are disabled for a specific entrance.
    // Used to prevent drawing D-Pad during 1 frame before Goron Race.
    if (AreCItemsDisabledByEntrance(ctxt) && ctxt->interfaceCtx.alphas.buttonCLeft == 0) {
        return;
    }

    // Use minimap alpha by default for fading textures out
    u8 primAlpha = ctxt->interfaceCtx.alphas.minimap & 0xFF;
    // If in minigame, the C buttons fade out and so should the D-Pad
    if (gSaveContext.extra.buttonsState.state == BUTTONS_STATE_MINIGAME ||
        gSaveContext.extra.buttonsState.state == BUTTONS_STATE_BOAT_ARCHERY ||
        gSaveContext.extra.buttonsState.state == BUTTONS_STATE_SWORDSMAN_GAME ||
        isMinigame) {
        primAlpha = ctxt->interfaceCtx.alphas.buttonCLeft & 0xFF;
    }

    // Check if any items shown on the D-Pad are usable
    // If none are, draw main D-Pad sprite faded
    if (!IsAnyItemUsable(DPAD_CONFIG.primary.values, gUsable) && primAlpha > 0x4A) {
        primAlpha = 0x4A;
    }

    // Show faded while flying as a Deku
    ActorPlayer* player = GET_PLAYER(ctxt);
    if (((player->stateFlags.state3 & PLAYER_STATE3_DEKU_AIR) != 0) && primAlpha > 0x4A) {
        primAlpha = 0x4A;
    }

    // Get index of main sprite position (left or right)
    u8 posIdx = (DPAD_CONFIG.display == DPAD_DISPLAY_LEFT) ? 0 : 1;

    // Main sprite position
    u16 x = gPosition[posIdx][0];
    u16 y = gPosition[posIdx][1];
    y = UpdateYPosition(x, y, 10);

    // Main sprite color
    Color color = HUD_COLOR_CONFIG.dpad;

    DispBuf* db = &ctxt->state.gfxCtx->overlay;
    gSPDisplayList(db->p++, &gSetupDb);
    gDPPipeSync(db->p++);
    gDPSetPrimColor(db->p++, 0, 0, color.r, color.g, color.b, primAlpha);
    gDPSetCombineMode(db->p++, G_CC_MODULATEIA_PRIM, G_CC_MODULATEIA_PRIM);
    Sprite_Load(db, &gSpriteDpad, 0, 1);
    Sprite_Draw(db, &gSpriteDpad, 0, x, y, 16, 16);

    Sprite* sprite = Sprite_GetItemTexturesSprite();
    for (int i = 0; i < 4; i++) {
        u8 value = DPAD_CONFIG.primary.values[i];

        // Calculate x/y from relative positions
        u16 ix = x + gPositions[i][0];
        u16 iy = y + gPositions[i][1];

        // Show nothing if not in inventory
        if (!HasInventoryItem(value))
            continue;

        // Draw faded
        u8 alpha = primAlpha;
        if (!gUsable[i] && alpha > 0x4A) {
            alpha = 0x4A;
        }
        gDPSetPrimColor(db->p++, 0, 0, 0xFF, 0xFF, 0xFF, alpha);

        // If D-Pad item has changed, load new texture on the fly.
        if (gTextureItems[i] != value) {
            LoadTextureFromSprite(sprite, i, value);
        }

        Sprite_Load(db, sprite, i, 1);
        Sprite_Draw(db, sprite, 0, ix, iy, 16, 16);
    }

    gDPPipeSync(db->p++);
}

/**
 * Hook function used to determine whether or not to skip the transformation cutscene based on input.
 *
 * Allows D-Pad input to also skip the cutscene if the D-Pad is enabled.
 **/
u16 Dpad_SkipTransformationCheck(ActorPlayer* player, GlobalContext* ctxt, u16 cur) {
    InputPad pad;
    pad.value = 0;

    // Set flags for original buttons: A, B, C buttons (0xC00F)
    pad.a = pad.b = pad.cd = pad.cl = pad.cr = pad.cu = 1;

    if (Dpad_IsEnabled()) {
        // Set flags for D-Pad (0xCF0F)
        pad.dd = pad.dl = pad.dr = pad.du = 1;
    }

    return cur & pad.value;
}
