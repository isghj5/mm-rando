;==================================================================================================
; A, B, C buttons color hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Custom color for A button.
; Replaces:
;   andi    t4, t3, 0x00FF
;   or      t5, t4, at
.org 0x80118DFC ; In rom: 0xAFEE5C
    jal     HudColors_GetAButtonColor_Hook
    andi    t4, t3, 0x00FF

; Custom color for B button.
; Replaces:
;   addiu   t8, r0, 0x0078
;   ori     at, at, 0x69E8
;   addiu   t7, r0, 0x0064
;   addiu   t6, r0, 0x00FF
.org 0x801171A0 ; In rom: 0xAFD200
    jal     HudColors_GetBButtonColor_Hook
    ori     at, at, 0x69E8
    andi    t6, t6, 0x00FF
    andi    t8, t8, 0x00FF

; Custom colors for C buttons and Start button.
; Replaces:
;   lh      t8, 0x002A (sp)
;   or      t9, t6, t7
;   andi    t6, t8, 0x00FF
;   or      t7, t9, t6
.org 0x8010D40C ; rom: 0xAF346C
    sw      ra, -0x0004 (sp)
    jal     HudColors_GetCStartButtonColor_Hook
    lh      t8, 0x002A (sp)
    lw      ra, -0x0004 (sp)

; Custom color for C button triangle (left).
; Replaces:
;   lh      t6, 0x026A (t4)
;   andi    t8, t6, 0x00FF
;   or      t9, t8, t2
.org 0x801178F4 ; In rom: 0xAFD954
    nop
    jal     HudColors_GetCButtonTriangleColor_Hook
    lh      t6, 0x026A (t4)

; Custom color for C button triangle (bottom).
; Replaces:
;   lh      t6, 0x026C (t4)
;   andi    t8, t6, 0x00FF
;   or      t9, t8, t2
.org 0x80117928 ; In rom: 0xAFD988
    nop
    jal     HudColors_GetCButtonTriangleColor_Hook
    lh      t6, 0x026C (t4)

; Custom color for C button triangle (right).
; Replaces:
;   lh      t6, 0x026E (t4)
;   andi    t8, t6, 0x00FF
;   or      t9, t8, t2
.org 0x80117950 ; In rom: 0xAFD9B0
    nop
    jal     HudColors_GetCButtonTriangleColor_Hook
    lh      t6, 0x026E (t4)

;==================================================================================================
; Clock color hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Custom color for clock.
; Replaces:
;   lui     at, 0x00AA
;   ori     at, at, 0x6400
;   addiu   t9, v0, 0x0008
;   sw      t9, 0x02A0 (s0)
;   or      t8, ra, at
.org 0x8011A110 ; In rom: 0xB00170
    addiu   t9, v0, 0x0008
    sw      ra, -0x0004 (sp)
    jal     HudColors_GetClockEmblemColor_Hook
    sw      t9, 0x02A0 (s0)
    lw      ra, -0x0004 (sp)

; Custom color for inverted clock (red).
; Replaces:
;   addu    t0, t0, t2
;   lh      a0, 0xFBCC (a0)
;   lh      t0, 0xFBEC (t0)
;   lui     t5, 0x801C
.org 0x80119B38 ; In rom: 0xAFFB98
    sw      ra, -0x0004 (sp)
    jal     HudColors_GetInvertedClockEmblemColorR_Hook
    lh      a0, 0xFBCC (a0)
    lw      ra, -0x0004 (sp)

; Custom color for inverted clock (green).
; Replaces:
;   lh      t5, 0xFBD0 (t5)
;   lh      a2, 0xFBF0 (a2)
;   lui     a3, 0x801C
;   lui     t3, 0x801C
.org 0x80119C04 ; In rom: 0xAFFC64
    sw      ra, -0x0004 (sp)
    jal     HudColors_GetInvertedClockEmblemColorG_Hook
    lh      t5, 0xFBD0 (t5)
    lw      ra, -0x0004 (sp)

; Custom color for inverted clock (blue).
; Replaces:
;   lui     a0, 0x801C
;   addu    a0, a0, t2
;   lh      a3, 0xFBD4 (a3)
;   lh      a0, 0xFBF4 (a0)
.org 0x80119CC8 ; In rom: 0xAFFD28
    sw      ra, -0x0004 (sp)
    jal     HudColors_GetInvertedClockEmblemColorB_Hook
    lh      a3, 0xFBD4 (a3)
    lw      ra, -0x0004 (sp)

; Fix calculated color for inverted clock emblem.
; Replaces:
;   sll     t6, t8, 16
;   sll     t7, t9, 24
;   or      t9, t7, t6
;   ori     t8, t9, 0xFF00
.org 0x8011A078
    sw      ra, -0x0004 (sp)
    jal     HudColors_FixInvertedClockEmblemColorCalc_Hook
    sll     t6, t8, 16
    lw      ra, -0x0004 (sp)

; Custom color for clock emblem sun icon.
; Replaces:
;   lui     at, 0xFFFF
;   ori     at, at, 0x6E00
;   andi    t9, t7, 0x00FF
;   or      t6, t9, at
.org 0x8011A39C ; In rom: 0xB003FC
    or      a3, t7, r0
    jal     HudColors_GetClockEmblemSunColor_Hook
    swc1    f2, 0x01D0 (sp)
    lwc1    f2, 0x01D0 (sp)

; Custom color for clock emblem sun icon (blinking).
; Replaces:
;   lui     at, 0xFFFF
;   addiu   t8, v0, 0x0008
;   sw      t8, 0x02A0 (s0)
;   sw      t7, 0x0000 (v0)
;   lh      t9, 0x0000 (a1)
;   ori     at, at, 0x6E00
;   andi    t6, t9, 0x00FF
;   or      t8, t6, at
.org 0x8011A3C0 ; In rom: 0xB00420
    addiu   t8, v0, 0x0008
    sw      t8, 0x02A0 (s0)
    sw      t7, 0x0000 (v0)
    lh      a3, 0x0000 (a1)
    jal     HudColors_GetClockEmblemSunColor_Hook
    swc1    f2, 0x01D0 (sp)
    lwc1    f2, 0x01D0 (sp)
    ; Move return value from T6 to T8
    or      t8, t6, r0

; Custom color for clock sun icon.
; Replaces:
;   ori     at, at, 0x6E00
;   andi    t8, t6, 0x00FF
;   or      t7, t8, at
.org 0x8011A73C ; In rom: 0xB0079C
    jal     HudColors_GetClockSunColor_Hook
    sh      t0, 0x01C6 (sp)
    lh      t0, 0x01C6 (sp)

; Custom color for clock moon icon.
; Replaces:
;   ori     at, at, 0x3700
;   andi    t9, t7, 0x00FF
;   or      t6, t9, at
.org 0x8011A88C ; In rom: 0xB008EC
    jal     HudColors_GetClockMoonColor_Hook
    sh      t0, 0x01C6 (sp)
    lh      t0, 0x01C6 (sp)

;==================================================================================================
; Heart colors hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Custom colors for hearts.
; Replaces:
;   jal     0x8010069C
.org 0x80121534 ; In rom: 0xB07594
    jal     HudColors_UpdateHeartColors

;==================================================================================================
; Magic meter color hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Custom color for infinite magic (original: #0000C8)
; Replaces:
;   lh      t8, 0x0272 (t1)
;   andi    t9, t8, 0x00FF
;   ori     t6, t9, 0xC800
.org 0x80116E44 ; In rom: 0xAFCEA4
    jal     HudColors_GetMagicMeterColor_Hook
    ori     a0, r0, 1
    ; Restore RA from previous stack value
    lw      ra, -0x0004 (sp)

; Custom color for normal magic (original: #00C800)
; Replaces:
;   lh      t8, 0x0272 (t1)
;   andi    t9, t8, 0x00FF
;   or      t6, t9, at
.org 0x80116E74 ; In rom: 0xAFCED4
    jal     HudColors_GetMagicMeterColor_Hook
    ori     a0, r0, 0
    ; Restore RA from previous stack value
    lw      ra, -0x0004 (sp)

; Custom color for infinite magic (charging)
; Replaces:
;   lh      t7, 0x0272 (t1)
;   andi    t8, t7, 0x00FF
;   ori     t9, t8, 0xC800
.org 0x80116D74
    jal     HudColors_GetMagicMeterChargingColor_Hook
    ori     a0, r0, 1
    lw      ra, -0x0004 (sp)

; Custom color for normal magic (charging)
; Replaces:
;   lui     at, 0x00C8
;   addiu   t6, v0, 0x0008
;   sw      t6, 0x02A0 (v1)
;   sw      t0, 0x0000 (v0)
;   lh      t7, 0x0272 (t1)
;   andi    t8, t7, 0x00FF
;   or      t9, t8, at
.org 0x80116D90
.area 0x1C
    addiu   t6, v0, 0x0008
    sw      t6, 0x02A0 (v1)
    sw      t0, 0x0000 (v0)
    jal     HudColors_GetMagicMeterChargingColor_Hook
    ori     a0, r0, 0
    lw      ra, -0x0004 (sp)
    nop
.endarea

;==================================================================================================
; Map color hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Custom color for map (original: #00FFFFA0)
; Replaces:
;   andi    t6, t9, 0x00FF
;   or      t5, t8, t6
.org 0x801032F4 ; In rom: 0xAE9354
    jal     HudColors_GetMapColor_Hook
    nop

; Custom color for player cursor (original: #C8FF00)
; Replaces:
;   andi    t9, t5, 0x00FF
;   or      t7, t9, at
.org 0x80103E8C ; In rom: 0xAE9EEC
    jal     HudColors_GetMapPlayerCursorColor_Hook
    nop

; Custom color for entrance cursor (original: #C80000)
; Replaces:
;   andi    t4, t3, 0x00FF
;   or      t5, t4, at
.org 0x801063D0 ; In rom: 0xAEC430
    jal     HudColors_GetMapEntranceCursorColor_Hook
    nop

;==================================================================================================
; Song A + C Button Note Colors
;==================================================================================================

.headersize G_CODE_DELTA

; Write colors for A, C button note icons.
; Replaces:
;   jal     0x80147564
.org 0x80151F90
    jal     HudColors_UpdateButtonNoteColors

; Replaces:
;   jal     0x80147564
.org 0x80154614
    jal     HudColors_UpdateButtonNoteColors

; Replaces:
;   jal     0x80147564
.org 0x80155178
    jal     HudColors_UpdateButtonNoteColors

;==================================================================================================
; Song Score Lines Color
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   lui     at, 0x0001
;   addu    at, at, v0
;   sh      a0, 0x2034 (at) ;; Red = 0xFF
;   lui     at, 0x0001
;   addu    at, at, v0
;   sh      r0, 0x2036 (at) ;; Green = 0x00
;   lui     at, 0x0001
;   addu    at, at, v0
;   sh      r0, 0x2038 (at) ;; Blue = 0x00
.org 0x80150C2C
.area 0x24
    sw      v0, 0x0034 (sp) ;; Store V0 in reserved stack space.
    sw      v1, 0x0038 (sp) ;; Store V1 in reserved stack space.
    jal     HudColors_UpdateScoreLinesColor
    lw      a0, 0x0030 (sp) ;; A0 = GlobalContext.
    addiu   a0, r0, 0x00FF  ;; Restore: A0 = 0xFF.
    lw      v0, 0x0034 (sp) ;; Restore V0.
    lw      v1, 0x0038 (sp) ;; Restore V1.
    nop
    nop
.endarea

;==================================================================================================
; Song Score Note Color
;==================================================================================================

.headersize G_CODE_DELTA

@ScoreNoteColor equ (HUD_COLOR_CONFIG + 0xBC)

; Replaces:
;   or      v1, v0, r0       ;; V1 = V0.
;   lui     t7, 0xFF64       ;;
;   ori     t7, t7, 0x00FF   ;; T7 = 0xFF6400FF (color value).
;   sw      t7, 0x0004 (v1)  ;; Write lower 32-bits of SetPrimColor instruction (color value).
;   sw      t2, 0x0000 (v1)  ;; Write higher 32-bits of SetPrimColor instruction.
.org 0x80152B4C
    lui     t7, hi(@ScoreNoteColor)
    lw      t7, lo(@ScoreNoteColor) (t7)
    ori     t7, t7, 0x00FF   ;; Alpha = 0xFF.
    sw      t7, 0x0004 (v0)
    sw      t2, 0x0000 (v0)

;==================================================================================================
; Shop Cursor Color
;==================================================================================================

; Write cursor color for Trading Post.
.headersize G_EN_OSSAN_DELTA
.org 0x808AAAF8 ; Offset: 0x2A58
    addiu   a1, a0, 0x220 ;; A1 = Output array.
    mfc1    a2, f0        ;; A2 = Amount.
    j       HudColors_WriteShopCursorColor
    or      a3, r0, r0    ;; A3 = Shop type (0).

; Write cursor color for Curiosity Shop.
.headersize G_EN_FSN_DELTA
.org 0x80AE2D90 ; Offset: 0x1220
    addiu   a1, a0, 0x3B4  ;; A1 = Output array.
    mfc1    a2, f0         ;; A2 = Amount.
    j       HudColors_WriteShopCursorColor
    ori     a3, r0, 0x0001 ;; A3 = Shop type (1).

; Write cursor color for Bomb Shop, Goron Shop, Zora Shop.
.headersize G_EN_SOB1_DELTA
.org 0x80A0EFDC ; Offset: 0x27CC
    addiu   a1, a0, 0x30C  ;; A1 = Output array.
    mfc1    a2, f0         ;; A2 = Amount.
    j       HudColors_WriteShopCursorColor
    ori     a3, r0, 0x0002 ;; A3 = Shop type (2).

; Write cursor color for Potion Hut.
.headersize G_EN_TRT_DELTA
.org 0x80A8E56C ; Offset: 0x2DFC
    addiu   a1, a0, 0x3F0  ;; A1 = Output array.
    mfc1    a2, f0         ;; A2 = Amount.
    j       HudColors_WriteShopCursorColor
    ori     a3, r0, 0x0003 ;; A3 = Shop type (3).
