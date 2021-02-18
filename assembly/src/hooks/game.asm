;==================================================================================================
; After Game Update Hook
;==================================================================================================

.headersize(G_CODE_RAM - G_CODE_FILE)

; Replaces:
;   addiu   sp, sp, 0x18
;   jr      ra
;   nop
.org 0x801744EC
    lw      a0, 0x0018 (sp)
    j       game_after_update
    addiu   sp, sp, 0x18

;==================================================================================================
; Display Buffer Hooks
;==================================================================================================

.headersize(G_CODE_RAM - G_CODE_FILE)

; Hook called after preparing display buffers for writing.
; Replaces:
;   jr      ra
;   addiu   sp, sp, 0x58
.org 0x80173F90
    j       game_after_prepare_display_buffers_hook
    addiu   sp, sp, 0x58
