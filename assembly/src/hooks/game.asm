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
