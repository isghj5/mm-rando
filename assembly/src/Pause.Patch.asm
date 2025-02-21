;==================================================================================================
; Pause Menu Setup Update
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   LHU     T7, 0x0020 (A2)
;   ADDIU   AT, R0, 0xEFFF
;   NOR     T8, T7, AT
;   BNEZL   T8, .+0x54
.org 0x800F4D78
    jal     PauseMenu_SetupUpdate_HasPressedStart_Hook
    nop
    nop
    beqzl   v0, .+0x54

;==================================================================================================
; Pause Menu - DMA Additional Code
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   jal     0x80163758
.org 0x80163B90
    jal     PauseMenu_DMAKaleidoPayload_Hook
