;==================================================================================================
; Song A + C Button Note Colors (Pause Menu)
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Get "A" button note color (when not playing).
; RDRAM: 0x8074C2AC, offset: 0x138C
; Replaces:
;   lui     t8, 0x5096
;   ori     t8, t8, 0xFFC8
.org 0x8081742C
    jal     Kaleido_HudColors_Pause1GetNoteAColor_Hook
    ori     at, r0, 0

; Get "C" button note color (when not playing).
; RDRAM: 0x8074C2D0, offset: 0x13B0
; Replaces:
;   lui     t9, 0xFFFF
;   ori     t9, t9, 0x32C8
.org 0x80817450
    jal     Kaleido_HudColors_Pause1GetNoteCColor_Hook
    ori     at, r0, 1

; Get "A" button note color (when playing).
; RDRAM: 0x8074C0D4, offset: 0x11B4
; Replaces:
;   lui     at, 0x5096
;   ori     at, at, 0xFF00
.org 0x80817254
    jal     Kaleido_HudColors_Pause2GetNoteColor_Hook
    ori     at, r0, 0

; Get "C" button note color (when playing).
; RDRAM: 0x8074C104, offset: 0x11E4
; Replaces:
;   lui     at, 0xFFFF
;   ori     at, at, 0x3200
.org 0x80817284
    jal     Kaleido_HudColors_Pause2GetNoteColor_Hook
    ori     at, r0, 1

; Get "A" button note color (when replaying).
; RDRAM: 0x8074C4D4, offset: 0x15B4
; Replaces:
;   lui     at, 0x5096
;   ori     at, at, 0xFF00
.org 0x80817654
    jal     Kaleido_HudColors_Pause2GetNoteColor_Hook
    ori     at, r0, 0

; Get "C" button note color (when replaying).
; RDRAM: 0x8074C504, offset: 0x15E4
; Replaces:
;   lui     at, 0xFFFF
;   ori     at, at, 0x3200
.org 0x80817684
    jal     Kaleido_HudColors_Pause2GetNoteColor_Hook
    ori     at, r0, 1

;==================================================================================================
; Pause Menu Border Primary Color
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t8, 0xB4B4
;   ori     t8, t8, 0x78FF
.org 0x808227C4
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t8, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t7, 0xB4B4
;   ori     t7, t7, 0x78FF
.org 0x808228EC
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t7, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t9, 0xB4B4
;   ori     t9, t9, 0x78FF
.org 0x80822A94
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t9, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t9, 0xB4B4
;   ori     t9, t9, 0x78FF
.org 0x80822BE0
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t9, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t7, 0xB4B4
;   ori     t7, t7, 0x78FF
.org 0x80822D38
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t7, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t7, 0xB4B4
;   ori     t7, t7, 0x78FF
.org 0x80822E54
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t7, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t8, 0xB4B4
;   ori     t8, t8, 0x78FF
.org 0x8082312C
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t8, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t6, 0xB4B4
;   ori     t6, t6, 0x78FF
.org 0x80823264
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t6, v1, r0

; Replaces:
;   lw      v0, 0x02B0 (s0)
;   lui     t4, 0xB4B4
;   ori     t4, t4, 0x78FF
.org 0x80824928
    jal     Kaleido_HudColors_GetMenuBorder1Color_Hook
    lw      v0, 0x02B0 (s0)
    or      t4, v1, r0

;==================================================================================================
; Pause Menu Border Secondary Color
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Color used for bottom panel, Z/R buttons when not selected.
; Replaces:
;   lw      v1, 0x00B4 (sp)
;   lw      t0, 0x002C (sp)
;   lw      ra, 0x00C0 (sp)
;   sw      v0, 0x0004 (v1)
;   lui     a3, 0xFA00
;   lw      v0, 0x02B0 (ra)
;   lui     t9, 0x968C
;   ori     t9, t9, 0x5AFF
.org 0x80823C30
.area 0x20
    lw      v1, 0x00B4 (sp)
    jal     Kaleido_HudColors_GetMenuBorder2Color1_Hook
    sw      v0, 0x0004 (v1)
    or      t9, v0, r0 ;; Move color result to T9.
    lw      t0, 0x002C (sp)
    lw      ra, 0x00C0 (sp)
    lui     a3, 0xFA00
    lw      v0, 0x02B0 (ra)
.endarea

; Z/R button colors when Z selected (part 1).
; Replaces:
;   lui     at, 0x968C
;   addiu   t8, v0, 0x0008
;   sw      t8, 0x02B0 (ra)
;   sw      a3, 0x0000 (v0)
.org 0x80823CD0
    jal     Kaleido_HudColors_GetMenuBorder2Color2_Hook
    sw      a3, 0x0000 (v0)
    lw      ra, 0x00C0 (sp) ;; Restore RA.
    addiu   t8, v0, 0x0008

; Z/R button colors when Z selected (part 2).
; Replaces:
;   ori     at, at, 0x5A00
.org 0x80823CE4
    sw      t8, 0x02B0 (ra)

; R button color when R selected.
; Replaces:
;   lui     at, 0x968C
;   ori     at, at, 0x5A00
;   andi    a0, a0, 0x00FF
.org 0x80823D40
    jal     Kaleido_HudColors_GetMenuBorder2Color2_Hook
    andi    a0, a0, 0x00FF
    lw      ra, 0x00C0 (sp) ;; Restore RA.

; Bottom panel during Song of Soaring map selection.
; Replaces:
;   lw      v1, 0x006C (sp)
;   lw      t0, 0x0030 (sp)
;   lui     a0, 0xFA00
;   sw      v0, 0x0004 (v1)
;   lw      v0, 0x02B0 (s0)
;   lui     t6, 0x968C
;   ori     t6, t6, 0x5AFF
.org 0x80825454
.area 0x1C
    lw      v1, 0x006C (sp)
    jal     Kaleido_HudColors_GetMenuBorder2Color1_Hook
    sw      v0, 0x0004 (v1)
    lw      t0, 0x0030 (sp)
    lui     a0, 0xFA00
    or      t6, v0, r0 ;; Store result in T6.
    lw      v0, 0x02B0 (s0)
.endarea

;==================================================================================================
; Pause Menu Subtitle Text Color
;==================================================================================================

.headersize G_KALEIDO_SCOPE_DELTA

; Color of pause menu subtitle text (when colored).
; Replaces:
;   lui     t6, 0xFFC8
;   ori     t6, t6, 0x00FF
;   addiu   t7, v0, 0x0008
.org 0x80824284
    jal     Kaleido_HudColors_GetMenuSubtitleTextColor_Hook
    addiu   t7, v0, 0x0008
    lw      ra, 0x00C0 (sp) ;; Restore RA.
