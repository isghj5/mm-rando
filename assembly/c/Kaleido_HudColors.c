#include <stdbool.h>
#include <z64.h>
#include "Color.h"
#include "HudColors.h"
#include "Reloc.h"

struct PauseCursorColors {
    /* 0x00 */ ColorRGB16 defaultInner1;
    /* 0x06 */ ColorRGB16 defaultInner2;
    /* 0x0C */ ColorRGB16 yellowInner1;
    /* 0x12 */ ColorRGB16 yellowInner2;
    /* 0x18 */ ColorRGB16 blueInner1;
    /* 0x1E */ ColorRGB16 blueInner2;
    /* 0x24 */ ColorRGB16 defaultOuter1;
    /* 0x2A */ ColorRGB16 defaultOuter2;
    /* 0x30 */ ColorRGB16 yellowOuter1;
    /* 0x36 */ ColorRGB16 yellowOuter2;
    /* 0x3C */ ColorRGB16 blueOuter1;
    /* 0x42 */ ColorRGB16 blueOuter2;
}; // size = 0x48

static void Kaleido_HudColors_ColorTo16(ColorRGB16* dest, Color src) {
    dest->r = src.r;
    dest->g = src.g;
    dest->b = src.b;
}

/**
 * Hook function used to get the "A" or "C" button color when used as a song note icon.
 **/
u32 Kaleido_HudColors_GetNoteButtonColor(u8 index, u8 alpha) {
    if (index == 0) {
        return Color_ConvertToIntWithAlpha(HUD_COLOR_CONFIG.noteA1, alpha);
    } else {
        return Color_ConvertToIntWithAlpha(HUD_COLOR_CONFIG.noteC1, alpha);
    }
}

/**
 * Hook function used to get the pause menu primary border color.
 **/
u32 Kaleido_HudColors_GetMenuBorder1Color(void) {
    return Color_ConvertToIntWithAlpha(HUD_COLOR_CONFIG.menuBorder1, 0xFF);
}

/**
 * Hook function used to get the pause menu secondary border color.
 **/
u32 Kaleido_HudColors_GetMenuBorder2Color(void) {
    return Color_ConvertToIntWithAlpha(HUD_COLOR_CONFIG.menuBorder2, 0xFF);
}

/**
 * Hook function used to get the pause menu subtitle text color.
 **/
u32 Kaleido_HudColors_GetMenuSubtitleTextColor(void) {
    return Color_ConvertToIntWithAlpha(HUD_COLOR_CONFIG.menuSubtitleText, 0xFF);
}

/**
 * Helper function for updating the pause menu colors.
 **/
void Kaleido_HudColors_UpdatePauseMenuColors(GlobalContext* ctxt) {
    // Only try to update colors if kaleido_scope is loaded.
    if (s801D0B70.kaleidoScope.loadedRamAddr != NULL) {
        // Resolve address of colors in kaleido_scope (pause) data.
        u32 vram = 0x808160A0 + 0x158A8;
        void* addr = Reloc_ResolvePlayerOverlay(&s801D0B70.kaleidoScope, vram);
        if (addr != NULL) {
            // Update pause menu cursor icon colors.
            struct PauseCursorColors* colors = (struct PauseCursorColors*)addr;
            Kaleido_HudColors_ColorTo16(&colors->blueInner1, HUD_COLOR_CONFIG.menuAInner1);
            Kaleido_HudColors_ColorTo16(&colors->blueInner2, HUD_COLOR_CONFIG.menuAInner2);
            Kaleido_HudColors_ColorTo16(&colors->blueOuter1, HUD_COLOR_CONFIG.menuAOuter1);
            Kaleido_HudColors_ColorTo16(&colors->blueOuter2, HUD_COLOR_CONFIG.menuAOuter2);
            Kaleido_HudColors_ColorTo16(&colors->yellowInner1, HUD_COLOR_CONFIG.menuCInner1);
            Kaleido_HudColors_ColorTo16(&colors->yellowInner2, HUD_COLOR_CONFIG.menuCInner2);
            Kaleido_HudColors_ColorTo16(&colors->yellowOuter1, HUD_COLOR_CONFIG.menuCOuter1);
            Kaleido_HudColors_ColorTo16(&colors->yellowOuter2, HUD_COLOR_CONFIG.menuCOuter2);
        }

        u8* bgDList = (u8*)ctxt->pauseCtx.bgDList;
        if (bgDList != NULL) {
            // Update pause menu subtitle icon colors.
            ColorRGBA8* subtitleC = (ColorRGBA8*)(bgDList + 0x13C);
            ColorRGBA8* subtitleA = (ColorRGBA8*)(bgDList + 0x194);
            subtitleA->r = HUD_COLOR_CONFIG.pauseTitleA.r;
            subtitleA->g = HUD_COLOR_CONFIG.pauseTitleA.g;
            subtitleA->b = HUD_COLOR_CONFIG.pauseTitleA.b;
            subtitleC->r = HUD_COLOR_CONFIG.pauseTitleC.r;
            subtitleC->g = HUD_COLOR_CONFIG.pauseTitleC.g;
            subtitleC->b = HUD_COLOR_CONFIG.pauseTitleC.b;
        }
    }
}
