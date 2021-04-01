;==================================================================================================
; After Game Update Hook
;==================================================================================================

.headersize G_CODE_DELTA

; Replaces:
;   addiu   sp, sp, 0x18
;   jr      ra
;   nop
.org 0x801744EC
    lw      a0, 0x0018 (sp)
    j       Game_AfterUpdate
    addiu   sp, sp, 0x18

;==================================================================================================
; Display Buffer Hooks
;==================================================================================================

.headersize G_CODE_DELTA

; Hook called after preparing display buffers for writing.
; Replaces:
;   jr      ra
;   addiu   sp, sp, 0x58
.org 0x80173F90
    j       Game_AfterPrepareDisplayBuffers_Hook
    addiu   sp, sp, 0x58
