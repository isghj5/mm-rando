;==================================================================================================
; File Select Constructor & Destructor
;==================================================================================================

.headersize G_FILE_CHOOSE_DELTA

; Replaces:
;   jr      ra
.org 0x80813DE4
    j       FileSelect_HookAfterCtor

; Replaces:
;   jr      ra
.org 0x80813C90
    j       FileSelect_HookAfterDtor

;==================================================================================================
; File Select Before Draw
;==================================================================================================

.headersize G_FILE_CHOOSE_DELTA

; Replaces:
;   sw      s0, 0x0020 (sp)
;   or      s0, a0, r0
;   sw      ra, 0x0024 (sp)
.org 0x8081326C
    sw      ra, 0x0024 (sp)
    jal     FileSelect_BeforeDraw_Hook
    sw      s0, 0x0020 (sp)

;==================================================================================================
; File Select Hash
;==================================================================================================

.headersize G_FILE_CHOOSE_DELTA

; Replaces:
;   addiu   t6, v0, 0x0008
;   sw      t6, 0x02B0 (a1)
;   sw      r0, 0x0004 (v0)
.org 0x80813890
    sw      r0, 0x0004 (v0)
    jal     FileSelect_DrawHash_Hook
    addiu   t6, v0, 0x0008
